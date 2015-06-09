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

            //TODO: Merge search params?
            
            structure.Snapshot = new StructureDefinition.StructureDefinitionSnapshotComponent() { Element = snapNav.ToListOfElements() };
        }

        private void merge(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            throw new NotImplementedException();
        }
    }
}
