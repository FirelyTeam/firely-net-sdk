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

        public static bool IsRepeating(this ElementDefinition defn) => defn != null && defn.Max != "1" && defn.Max != "0";

        public static bool IsExtension(this ElementDefinition defn) => defn != null && ElementDefinitionNavigator.IsExtensionPath(defn.Path);

        // [WMR 20160805] New
        public static bool IsRootElement(this ElementDefinition defn) => defn != null && ElementDefinitionNavigator.IsRootPath(defn.Path);

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

        /// <summary>Returns the explicit primary type profile, if specified, or otherwise the core profile url for the specified type code.</summary>
        public static string TypeProfile(this ElementDefinition.TypeRefComponent elemType)
        {
            string profile = null;
            if (elemType != null)
            {
                profile = elemType.Profile.FirstOrDefault();
                if (profile == null && elemType.Code.HasValue)
                {
                    profile = ModelInfo.CanonicalUriForFhirCoreType(elemType.Code.Value);
                }
            }
            return profile;
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

        /// <summary>
        /// If the element is constrained to a single common type (i.e. if all the existing
        /// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        /// then return that common type code, otherwise return <c>null</c>.
        /// </summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A type code, or <c>null</c>.</returns>
        public static FHIRDefinedType? CommonTypeCode(this List<ElementDefinition.TypeRefComponent> types)
        {
            if (types != null)
            {
                var cnt = types.Count;
                if (cnt > 0)
                {
                    var firstCode = types[0].Code;
                    for (int i = 1; i < cnt; i++)
                    {
                        var code = types[i].Code;
                        // Ignore empty codes (invalid, Type.code is required)
                        if (code != null && code != firstCode)
                        {
                            return null;
                        }
                    }
                    return firstCode;
                }
            }
            return null;
        }

        /// <summary>
        /// If the element is constrained to a single common type (i.e. if all the existing
        /// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        /// then return that common type code, otherwise return <c>null</c>.
        /// </summary>
        /// <param name="elem">An element definition.</param>
        /// <returns>A type code, or <c>null</c>.</returns>
        public static FHIRDefinedType? CommonTypeCode(this ElementDefinition elem) => elem?.Type.CommonTypeCode();

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A list of type code strings.</returns>
        public static List<FHIRDefinedType> DistinctTypeCodes(this List<ElementDefinition.TypeRefComponent> types)
            => types.Where(t => t.Code != null).Select(t => t.Code.Value).Distinct().ToList();

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="elem">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns>A list of type code strings.</returns>
        public static List<FHIRDefinedType> DistinctTypeCodes(this ElementDefinition elem) => elem?.Type.DistinctTypeCodes();

        /// <summary>Returns <c>true</c> if the element represents an extension with a custom extension profile url, or <c>false</c> otherwise.</summary>
        public static bool IsMappedExtension(this ElementDefinition defn)
        {
            return defn.IsExtension() && defn.PrimaryTypeProfile() != null;
        }

        /// <summary>Determines if the specified element definition represents a <see cref="ResourceReference"/>.</summary>
        /// <param name="defn">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a reference, or <c>false</c> otherwise.</returns>
        public static bool IsReference(this ElementDefinition defn)
        {
            var primaryType = defn.Type.FirstOrDefault();
            // return primaryType != null && primaryType.Code.HasValue && ModelInfo.IsReference(primaryType.Code.Value);
            return primaryType != null && IsReference(primaryType);
        }

        /// <summary>
        /// Determines if the specified element is a backbone element
        /// </summary>
        /// <param name="defn"></param>
        /// <returns></returns>
        /// <remarks>Backbone elements are nested groups of elements, that appear within resources (of type BackboneElement) or as
        /// within datatypes (of type Element).
        ///</remarks>
        public static bool IsBackboneElement(this ElementDefinition defn) => defn.Path.Contains('.') && defn.Type.Count == 1 && 
            (defn.Type[0].Code == FHIRDefinedType.BackboneElement || defn.Type[0].Code == FHIRDefinedType.Element);


        /// <summary>Determines if the specified type reference represents a <see cref="ResourceReference"/>.</summary>
        /// <param name="typeRef">A <see cref="ElementDefinition.TypeRefComponent"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a reference, or <c>false</c> otherwise.</returns>
        public static bool IsReference(this ElementDefinition.TypeRefComponent typeRef)
        {
            return typeRef.Code.HasValue && ModelInfo.IsReference(typeRef.Code.Value);
        }

        /// <summary>Determines if the specified element definition represents a type choice element by verifying that the element name ends with "[x]".</summary>
        /// <param name="defn">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a type choice element, or <c>false</c> otherwise.</returns>
        public static bool IsChoice(this ElementDefinition defn)
        {
            return defn.Path.EndsWith("[x]");
        }

        public static string GetNameFromPath(string path)
        {
            var pos = path.LastIndexOf(".");

            return pos != -1 ? path.Substring(pos + 1) : path;
        }

        public static string GetNameFromPath(this ElementDefinition defn)
        {
            return GetNameFromPath(defn.Path);
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
    }
}


