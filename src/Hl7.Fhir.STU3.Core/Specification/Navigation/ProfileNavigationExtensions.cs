/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Diagnostics;

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
            Rebase(root.Element, path);
        }


        /// <summary>
        /// Rewrites the Path's of the elements in a structure so they are based on the given path: the root
        /// of the given structure will become the given path, it's children will be relocated below that path
        /// </summary>
        /// <param name="elements">A list of element definitions that will be rebased on the path.</param>
        /// <param name="path">The path to rebase the structure on.</param>
        public static void Rebase(this IList<ElementDefinition> elements, string path)
        {
            var nav = new ElementDefinitionNavigator(elements);

            if (nav.MoveToFirstChild())
            {
                var newPaths = new List<string>() { path };

                rebaseChildren(nav, path, newPaths);
                Debug.Assert(elements.Count == newPaths.Count);

                // Can only change the paths after navigating the tree, otherwise the
                // navigation functions (which are based on the paths) won't function correctly
                for (var i = 0; i < elements.Count; i++)
                {
                    elements[i].Path = newPaths[i];
                }
            }
        }

        private static void rebaseChildren(ElementDefinitionNavigator nav, string path, List<string> newPaths)
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


        /// <summary>
        /// Builds a fully qualified path for the ElementDefinition.
        /// </summary>
        /// <remarks>A fully qualified path is the path of the ElementDefinition, prefixed by the canonical of 
        /// the StructureDefinition the ElementDefinition is part of.</remarks>
        public static string CanonicalPath(this ElementDefinitionNavigator nav) =>
            nav.Current.CanonicalPath(nav.StructureDefinition);

        public static string GetNameFromPath(string path)
        {
            var pos = path.LastIndexOf(".");

            return pos != -1 ? path.Substring(pos + 1) : path;
        }
    }
}


