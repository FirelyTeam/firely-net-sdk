/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Expansion
{
    public class SnapshotGenerator
    {
        private ArtifactResolver _resolver;

        public SnapshotGenerator()
        {
            _resolver = ArtifactResolver.CreateCachedDefault();
        }

        public SnapshotGenerator(ArtifactResolver resolver)
        {
            _resolver = resolver;
        }
        
        public void Generate(StructureDefinition structure)
        {
            if (structure.Differential == null) throw Error.Argument("structure", "structure does not contain a differential specification");
            if (structure.Type != StructureDefinition.StructureDefinitionType.Constraint) throw Error.Argument("structure", "structure is not a constraint but an " + structure.Type.ToString());
            if(structure.Base == null) throw Error.Argument("structure", "structure is a constraint, but no base has been specified");

            var differential = structure.Differential;

            var baseStructure = _resolver.GetStructureDefinition(structure.Base);

            if (baseStructure == null) throw Error.InvalidOperation("Could not locate the base StructureDefinition for url " + structure.Base);
            if (baseStructure.Snapshot == null) throw Error.InvalidOperation("Snapshot generator required the base at {0} to have a snapshot representation", structure.Base);

            var snapshot = (StructureDefinition.StructureDefinitionSnapshotComponent)baseStructure.Snapshot.DeepCopy();
            var snapNav = new ElementNavigator(snapshot.Element);
            snapNav.MoveToFirstChild();

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = new DifferentialTreeConstructor(differential.Element).MakeTree();
            var diffNav = new ElementNavigator(fullDifferential);
            diffNav.MoveToFirstChild();

            merge(snapNav, diffNav);
           
            structure.Snapshot = new StructureDefinition.StructureDefinitionSnapshotComponent() { Element = snapNav.ToListOfElements() };
        }


        private void merge(ElementNavigator snap, ElementNavigator diff)
        {
            (new ElementDefnMerger()).Merge(snap.Current, diff.Current);

            // If there are children, move into them, and recursively merge them
            if (diff.MoveToFirstChild())
            {
                if (!snap.HasChildren)
                {
                    // The differential moves into an element that has no children in the base.
                    // This is allowable if the base's element has a nameReference or a TypeRef,
                    // in which case needs to be expanded before we can move to the path indicated
                    // by the differential
                    expandBaseElement(snap, diff);
                }

                // Due to how MoveToFirstChild() works, we have to move to the first matching *child*
                // when entering the loop for the first time, after that we can look for the next
                // matching *sibling*.
                bool firstEntry = true;

                do
                {
                    if ((firstEntry && !snap.MoveToChild(diff.PathName)) ||
                        (!firstEntry && !snap.MoveToNext(diff.PathName)))
                        throw Error.InvalidOperation("Differential has a constraint for path '{0}', which does not exist in its base", diff.PathName);
                    firstEntry = false;

                    // Child found in both, merge them
                    if (countChildNameRepeats(diff) > 1 || diff.Current.IsExtension())
                    {
                        // The child in the diff repeats or we recognize it as an extension slice -> we're on the first element of a slice!
                        mergeSlice(snap, diff);
                    }
                    else
                        merge(snap, diff);
                }
                while (diff.MoveToNext());

                // After the merge, return the diff and snapho back to their original position
                diff.MoveToParent();
                snap.MoveToParent();
            }
        }


        private void expandBaseElement(ElementNavigator snap, ElementNavigator diff)
        {
            snap.ExpandElement(_resolver);

            if (!snap.HasChildren)
            {
                // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                throw Error.InvalidOperation("Differential has nested constraints for node {0}, but this is a leaf node in base", diff.Path);
            }
        }
    }
}
