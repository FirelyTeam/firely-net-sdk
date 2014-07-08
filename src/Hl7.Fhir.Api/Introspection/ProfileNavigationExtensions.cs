/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Introspection
{
    public static class ProfileNavigationExtensions
    {
        public static Profile.ElementComponent FindChild(this Profile.ProfileStructureComponent root, string path)
        {
            foreach (var element in root.Element)
            {
                var scanPath = element.Path;

                if (element.Definition != null && !String.IsNullOrEmpty(element.Definition.NameReference) && path.StartsWith(scanPath))
                {
                    // The path we are navigating to is on or below this element, but the element defers its definition to another named element in the structure
                    if (path.Length > scanPath.Length)
                    {
                        // The path navigates further into the referenced element, so go ahead along the path over there
                        var targetElement = element.Definition.NameReference + "." + path.Substring(scanPath.Length + 1);
                        return FindChild(root, targetElement);
                    }
                    else
                    {
                        // The path we are looking for is actually this element, but since it defers it definition, go get the referenced element
                        return FindChild(root, element.Definition.NameReference);
                    }
                }

                // Note the order of the if/else if matters here
                else if (scanPath == path)
                    return element;

            }

            return null;
        }


        public static Profile.ElementComponent FindChild(this Profile.ProfileStructureComponent root, Profile.ElementComponent element)
        {
            return FindChild(root, element.Path);
        }


        public static string GetNameFromPath(this Profile.ElementComponent element)
        {
 	        var pos = element.Path.LastIndexOf(".");

            return pos != -1 ? element.Path.Substring(pos+1) : element.Path;
        }


        public static IEnumerable<Profile.ElementComponent> GetChildren(this Profile.ProfileStructureComponent root, string path, bool includeGrandchildren = false)
        {
            var parent = FindChild(root, path);

            if (parent != null)
            {
                // We know that the path of the found parent is where we'll find the children (FindChild resolves NameReferences), so
                // we can just do a quick lookup for its child elements
                var resolvedPath = parent.Path;
                var children = root.Element.Where(elem => elem.Path.StartsWith(resolvedPath + "."));

                foreach (var child in children)
                {
                    // Skip children of this child
                    var tail = child.Path.Substring(resolvedPath.Length + 1);
                    if (!tail.Contains('.'))
                    {
                        yield return child;

                        if (includeGrandchildren)
                        {
                            foreach (var grandChild in GetChildren(root, child.Path, includeGrandchildren:true))
                                yield return grandChild;
                        }
                    }
                }
            }
               
            yield break;
        }
    }
}
    
    
