/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>Returns the primary element type, if it exists.</summary>
        /// <param name="defn">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns>A <see cref="ElementDefinition.TypeRefComponent"/> instance, or <c>null</c>.</returns>
        public static ElementDefinition.TypeRefComponent PrimaryType(this ElementDefinition defn)
        {
            return defn.Type != null && defn.Type.Count > 0 ? defn.Type[0] : null;
        }

        /// <summary>Enumerates the type profile references of the primary element type.</summary>
        public static IEnumerable<string> PrimaryTypeProfiles(this ElementDefinition defn)
        {
            var primaryType = defn.PrimaryType();
            if (primaryType != null)
            {
                return primaryType.Profile;
            }
            return Enumerable.Empty<string>();
        }


        /// <summary>Returns the first type profile reference of the primary element type, if it exists, or <c>null</c></summary>
        public static string PrimaryTypeProfile(this ElementDefinition defn)
        {
            return defn.PrimaryTypeProfiles().FirstOrDefault();
        }

              /// <summary>Returns the type code of the primary element type, or <c>null</c>.</summary>
        public static FHIRDefinedType? PrimaryTypeCode(this ElementDefinition defn)
        {
            var primaryType = defn.PrimaryType();
            if (primaryType != null)
            {
                return primaryType.Code;
            }
            return null;
        }

        ///// <summary>
        ///// If the element is constrained to a single common type (i.e. if all the existing
        ///// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        ///// then return that common type code, otherwise return <c>null</c>.
        ///// </summary>
        ///// <param name="types">A list of element types.</param>
        ///// <returns>A type code, or <c>null</c>.</returns>
        //public static FHIRDefinedType? CommonTypeCode(this List<ElementDefinition.TypeRefComponent> types)
        //{
        //    if (types != null)
        //    {
        //        var cnt = types.Count;
        //        if (cnt > 0)
        //        {
        //            var firstCode = types[0].Code;
        //            for (int i = 1; i < cnt; i++)
        //            {
        //                var code = types[i].Code;
        //                // Ignore empty codes (invalid, Type.code is required)
        //                if (code != null && code != firstCode)
        //                {
        //                    return null;
        //                }
        //            }
        //            return firstCode;
        //        }
        //    }
        //    return null;
        //}

        ///// <summary>
        ///// If the element is constrained to a single common type (i.e. if all the existing
        ///// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        ///// then return that common type code, otherwise return <c>null</c>.
        ///// </summary>
        ///// <param name="elem">An element definition.</param>
        ///// <returns>A type code, or <c>null</c>.</returns>
        //public static FHIRDefinedType? CommonTypeCode(this ElementDefinition elem) => elem?.Type.CommonTypeCode();

        ///// <summary>Returns <c>true</c> if the element represents an extension with a custom extension profile url, or <c>false</c> otherwise.</summary>
        //public static bool IsMappedExtension(this ElementDefinition defn)
        //{
        //    return defn.IsExtension() && defn.PrimaryTypeProfile() != null;
        //}

        public static bool IsExtension(this ElementDefinition defn) => IsExtensionPath(defn.Path);

        /// <summary>Determines if the specified element path represents a (modifier) extension element.</summary>
        /// <returns><c>true</c> if <paramref name="path"/> ends with <c>.extension</c> or <c>.modifierExtension</c>, or <c>false</c> otherwise.</returns>
        public static bool IsExtensionPath(string path) => !string.IsNullOrEmpty(path) && (path.EndsWith(".extension") || path.EndsWith(".modifierExtension"));

        /// <summary>Determines if the specified element definition represents a <see cref="ResourceReference"/>.</summary>
        /// <param name="defn">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a reference, or <c>false</c> otherwise.</returns>
        public static bool IsReference(this ElementDefinition defn)
        {
            var primaryType = defn.Type.FirstOrDefault();
            // return primaryType != null && primaryType.Code.HasValue && ModelInfo.IsReference(primaryType.Code.Value);
            return primaryType != null && primaryType.IsReference();
        }

        public static string GetParentNameFromPath(this ElementDefinition defn)
        {
            return ElementDefinitionNavigator.GetParentPath(defn.Path);
        }

        /// <summary>Returns the root element from the specified element list, if available, or <c>null</c>.</summary>
        public static ElementDefinition GetRootElement(this IElementList elements)
        {
            return elements?.Element?.FirstOrDefault(e => e.IsRootElement());
        }

        public static string UrlAndPath(this ElementDefinitionNavigator me) =>
            (me.StructureDefinition?.Url ?? "") + (me.Current?.Path ?? "(root)");
    }
}


