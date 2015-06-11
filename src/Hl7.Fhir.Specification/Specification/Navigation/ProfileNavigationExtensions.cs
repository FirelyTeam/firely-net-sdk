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

                    if(nav.HasChildren) 
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

        public static bool IsChoice(this ElementDefinition defn)
        {
            return defn.Path.EndsWith("[x]");
        }

        public static string GetNameFromPath(this ElementDefinition element)
        {
 	        var pos = element.Path.LastIndexOf(".");

            return pos != -1 ? element.Path.Substring(pos+1) : element.Path;
        }

        public static string GetParentNameFromPath(this ElementDefinition element)
        {
            return ElementNavigator.GetParentPath(element.Path);
        }      
    }
}
    
    
