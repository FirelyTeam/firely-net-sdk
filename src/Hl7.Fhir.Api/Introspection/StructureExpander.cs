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
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
    internal class StructureExpander
    {
        public StructureExpander(Profile.ProfileStructureComponent structure, StructureLoader loader)
        {
            Structure = structure;
            _loader = loader;
        }

        public Profile.ProfileStructureComponent Structure { get; private set; }

        private StructureLoader _loader;

        public Profile.ProfileStructureComponent Expand(Profile.ProfileStructureComponent differential)
        {
            var baseStructure = _loader.LocateBaseStructure(differential.TypeElement);
            var baseUri = StructureLoader.BuildBaseStructureUri(differential.TypeElement).ToString();

            var snapshot = (Profile.ProfileStructureComponent)baseStructure.DeepCopy();
            snapshot.SetStructureForm(StructureForm.Snapshot);
            snapshot.SetStructureBaseUri(baseUri.ToString());

            var fullDifferential = new DifferentialTreeConstructor(differential).MakeTree();

            // Do stuff to snapshot...

            return snapshot;
        }


        public ElementNavigator ExpandElement(ElementNavigator nav)
        {
            if (nav.HasChildren) return null;

            if (nav.Current.Definition != null)
            {
                var defn = nav.Current.Definition;
                if (!String.IsNullOrEmpty(defn.NameReference))
                {
                    var sourceNav = resolveNameReference(nav.Structure, defn.NameReference);
                    nav.CopyChildren(sourceNav);
                }
                else if (defn.Type != null && defn.Type.Count > 0)
                {
                    if (defn.Type.Count > 1)
                        throw new NotImplementedException("Don't know how to implement navigation into choice types yet at node " + nav.Path);
                    else
                    {
                        var sourceNav = resolveStructureReference(_loader, defn.Type[0].CodeElement);

                        if (sourceNav != null)
                        {
                            sourceNav.MoveToFirstChild();
                            nav.CopyChildren(sourceNav);
                        }
                        else
                            throw new FileNotFoundException("Cannot locate base-structure for datatype " + defn.Type[0].Code);
                    }
                }
            }

            return nav;
        }

        private static ElementNavigator resolveStructureReference(StructureLoader _loader, Code code)
        {
            var result = _loader.LocateBaseStructure(code);
            return result != null ? new ElementNavigator(result) : null;
        }


        private static ElementNavigator resolveNameReference(Profile.ProfileStructureComponent structure, string nameReference)
        {
            return structure.JumpToNameReference(nameReference);
        }
    }
}
