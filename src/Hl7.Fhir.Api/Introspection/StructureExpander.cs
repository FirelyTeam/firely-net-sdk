/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
    public class StructureExpander
    {
        public StructureExpander(Profile.ProfileStructureComponent structure, IArtifactSource artifactSource)
        {
            Structure = structure;
            ArtifactSource = artifactSource;
        }

        public IArtifactSource ArtifactSource { get; private set; }

        public Profile.ProfileStructureComponent Structure { get; private set; }


        public void ExpandElement(string path)
        {
            var nav = new ElementNavigator(Structure);

            var points = nav.Find(path);

            foreach (var point in points)
            {
                nav.ReturnToBookmark(point);
                ExpandElement(nav);
            }
        }

        public void ExpandElement(ElementNavigator nav)
        {
            if (nav.HasChildren) return;
                        
            if (nav.Current.Definition != null)
            {
                var defn = nav.Current.Definition;
                if (!String.IsNullOrEmpty(defn.NameReference))
                {
                    var sourceNav = resolveNameReference(defn.NameReference);
                    nav.CopyChildren(sourceNav);
                }
                else if (defn.Type != null && defn.Type.Count > 0)
                {
                    if (defn.Type.Count > 1)
                        throw new NotImplementedException("Don't know how to implement navigation into choice types yet at node " + nav.Path);
                    else
                    {
                        var sourceNav = resolveStructureReference(defn.Type[0].Code);
                        nav.CopyChildren(sourceNav);
                    }
                }
            }

            return;
        }


        private ElementNavigator resolveNameReference(string nameReference)
        {
            Console.WriteLine("I would have gone out to fetch NameReference " + nameReference);
            return null;
        }

        private ElementNavigator resolveStructureReference(string structureReference)
        {
            Console.WriteLine("I would have gone out to fetch Structure " + structureReference);
            return null;
        }
    }
}
