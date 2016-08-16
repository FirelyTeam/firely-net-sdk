/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    public static class ProfileNavigationExtensions
    {
        /// <summary>
        /// Rewrites the Path's of the elements in a structure so they are based on the given path: the root
        /// of the given structure will become the given path, it's children will be relocated below that path
        /// </summary>
        /// <param name="root">The structure that will be rebased on the path</param>
        /// <param name="path">The path to rebase the structure on</param>
        public static void Rebase(this IElementList root, string path)
        {
            var nav = new ElementNavigator(root.Element);

            if (nav.MoveToFirstChild())
            {
                var newPaths = new List<string>() { path };

                rebaseChildren(nav, path, newPaths);

                var snapshot = root.Element;

                // Can only change the paths after navigating the tree, otherwise the
                // navigation functions (which are based on the paths) won't function correctly
                for (var i = 0; i < root.Element.Count; i++)
                    root.Element[i].Path = newPaths[i];
            }
        }


        private static void rebaseChildren(BaseElementNavigator nav, string path, List<string> newPaths)
        {
            var bm = nav.Bookmark();

            if (nav.MoveToFirstChild())
            {
                do
                {
                    var newPath = path + "." + nav.Current.GetNameFromPath();

                    newPaths.Add(newPath);

                    if (nav.HasChildren)
                        rebaseChildren(nav, newPath, newPaths);
                }
                while (nav.MoveToNext());

                nav.ReturnToBookmark(bm);
            }
        }

        public static bool InRange(this ElementDefinition defn, int count)
        {
            int min = Convert.ToInt32(defn.Min);
            if (count < min)
                return false;

            if (defn.Max == "*")
                return true;

            int max = Convert.ToInt32(defn.Max);
            if (count > max)
                return false;

            return true;
        }

        public static bool IsRepeating(this ElementDefinition defn)
        {
            return defn.Max != "1" && defn.Max != "0";
        }

        public static bool IsExtension(this ElementDefinition elem)
        {
            return elem.Path.EndsWith(".extension") || elem.Path.EndsWith(".modifierExtension");
        }

        // [WMR 20160805] New
        public static bool IsRootElement(this ElementDefinition elem)
        {
            return !string.IsNullOrEmpty(elem.Path) && !elem.Path.Contains('.');
        }

        // [WMR 20160801] NEW

        /// <summary>Enumerates the type profile references of the primary element type.</summary>
        public static string PrimaryTypeProfiles(this ElementDefinition elem)
        {
            if (elem.Type != null)
            {
                var primaryType = elem.Type.FirstOrDefault();
                if (primaryType != null)
                {
                    return primaryType.Profile;
                }
            }
            return null;
        }


        /// <summary>Returns the first type profile reference of the primary element type, if it exists, or <c>null</c></summary>
        public static string PrimaryTypeProfile(this ElementDefinition elem)
        {
            return elem.PrimaryTypeProfiles();
        }

        /// <summary>Returns the type code of the primary element type, or <c>null</c>.</summary>
        public static FHIRAllTypes? PrimaryTypeCode(this ElementDefinition elem)
        {
            if (elem.Type != null)
            {
                var type = elem.Type.FirstOrDefault();
                if (type != null)
                {
                    return (FHIRAllTypes)Enum.Parse(typeof(FHIRAllTypes), type.Code);
                }
            }
            return null;
        }


        /// <summary>Returns <c>true</c> if the element represents an extension with a custom extension profile url, or <c>false</c> otherwise.</summary>
        public static bool IsMappedExtension(this ElementDefinition elem)
        {
            return elem.IsExtension() && elem.PrimaryTypeProfile() != null;
        }

        // [WMR 20160720] NEW
        public static bool IsReference(this ElementDefinition elem)
        {
            var primaryType = elem.Type.FirstOrDefault();
            return primaryType != null && !string.IsNullOrEmpty(primaryType.Code) && ModelInfo.IsReference(primaryType.Code);
        }

        public static bool IsChoice(this ElementDefinition defn)
        {
            return defn.Path.EndsWith("[x]");
        }

        public static string GetNameFromPath(this ElementDefinition element)
        {
            var pos = element.Path.LastIndexOf(".");

            return pos != -1 ? element.Path.Substring(pos + 1) : element.Path;
        }

        public static string GetParentNameFromPath(this ElementDefinition element)
        {
            return ElementNavigator.GetParentPath(element.Path);
        }

    }
}


