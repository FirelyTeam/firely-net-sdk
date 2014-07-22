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
        public StructureExpander(Profile.ProfileStructureComponent structure, StructureLocator locator)
        {
            Structure = structure;
            _locator = locator;
        }

        public Profile.ProfileStructureComponent Structure { get; private set; }

        private StructureLocator _locator;

        public ElementNavigator ExpandElement(string path)
        {
            var nav = new ElementNavigator(Structure);

            var points = nav.Find(path);

            //foreach (var point in points)
            //{
            //    nav.ReturnToBookmark(point);
            //    ExpandElement(nav);
            //}

            if (points.Any())
            {
                nav.ReturnToBookmark(points.First());
                return ExpandElement(nav);
            }
            else
                return null;
        }

        public ElementNavigator ExpandElement(ElementNavigator nav)
        {
            if (nav.HasChildren) return null;
                        
            if (nav.Current.Definition != null)
            {
                var defn = nav.Current.Definition;
                if (!String.IsNullOrEmpty(defn.NameReference))
                {
                    var sourceNav = resolveNameReference(nav.Structure,defn.NameReference);
                    nav.CopyChildren(sourceNav);
                }
                else if (defn.Type != null && defn.Type.Count > 0)
                {
                    if (defn.Type.Count > 1)
                        throw new NotImplementedException("Don't know how to implement navigation into choice types yet at node " + nav.Path);
                    else
                    {
                        var sourceNav = resolveStructureReference(_locator, defn.Type[0].CodeElement);

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


        private static ElementNavigator resolveNameReference(Profile.ProfileStructureComponent structure, string nameReference)
        {
            return structure.JumpToNameReference(nameReference);
        }

        private static ElementNavigator resolveStructureReference(StructureLocator locator, Code baseType)
        {
            // TODO: This works for profiles based on base-profiles, but if you could constrain a constrained
            // element with a TypeRef.profile, this logic should change
            string fullReference = CoreZipArtifactSource.CORE_SPEC_PROFILE_URI_PREFIX + baseType.Value.ToLower();
            var structure = locator.Locate(new Uri(fullReference), baseType);
            
            return structure != null ? new ElementNavigator(structure) : null;
        }
    }
}
