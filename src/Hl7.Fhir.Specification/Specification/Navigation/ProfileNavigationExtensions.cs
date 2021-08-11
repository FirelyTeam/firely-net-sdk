/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
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
            if (defn.Type.IsNullOrEmpty())
                return null;
            else if (defn.Type.Count == 1)
                return defn.Type[0];
            else  //if there are multiple types (value[x]), try to get a common type, otherwise, use Element as the common datatype             
            {
                var distinctTypeCode = defn.CommonTypeCode() ?? FHIRAllTypes.Element.GetLiteral();
                return new ElementDefinition.TypeRefComponent() { Code = distinctTypeCode };
            }

        }

        /// <summary>Returns the type profile reference of the primary element type, if it exists, or <c>null</c></summary>
        public static string PrimaryTypeProfile(this ElementDefinition elem)
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

        /// <summary>Returns the explicit primary type profile, if specified, or otherwise the core profile url for the specified type code.</summary>
        public static string GetTypeProfile(this ElementDefinition.TypeRefComponent elemType)
        {
            string profile = null;
            if (elemType != null)
            {
                profile = elemType.Profile;
                if (profile == null && elemType.Code != null)
                {
                    profile = ModelInfo.CanonicalUriForFhirCoreType(elemType.Code);
                }
            }
            return profile;
        }

        /// <summary>Returns the type code of the primary element type, or <c>null</c>.</summary>
        public static FHIRAllTypes? PrimaryTypeCode(this ElementDefinition elem)
        {
            if (elem.Type != null)
            {
                var type = elem.Type.FirstOrDefault();
                if (type != null && !string.IsNullOrEmpty(type.Code))
                {
                    return Utility.EnumUtility.ParseLiteral<FHIRAllTypes>(type.Code);
                    // return (FHIRAllTypes)Enum.Parse(typeof(FHIRAllTypes), type.Code);
                }
            }
            return null;
        }

        /// <summary>
        /// If the element is constrained to a single common type (i.e. if all the existing
        /// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        /// then return that common type code, otherwise return <c>null</c>.
        /// </summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A type code.</returns>
        public static string CommonTypeCode(this List<ElementDefinition.TypeRefComponent> types)
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
        /// <returns>A type code.</returns>
        public static string CommonTypeCode(this ElementDefinition elem) => elem?.Type.CommonTypeCode();

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A list of type code strings.</returns>
        public static List<string> DistinctTypeCodes(this List<ElementDefinition.TypeRefComponent> types)
            => types.Where(t => t.Code != null).Select(t => t.Code).Distinct().ToList();

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="elem">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns>A list of type code strings.</returns>
        public static List<string> DistinctTypeCodes(this ElementDefinition elem) => elem?.Type.DistinctTypeCodes();

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
            (defn.Type[0].Code == "BackboneElement" || defn.Type[0].Code == "Element");


        /// <summary>Determines if the specified type reference represents a <see cref="ResourceReference"/>.</summary>
        /// <param name="typeRef">A <see cref="ElementDefinition.TypeRefComponent"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a reference, or <c>false</c> otherwise.</returns>
        public static bool IsReference(this ElementDefinition.TypeRefComponent typeRef)
        {
            return !string.IsNullOrEmpty(typeRef.Code) && ModelInfo.IsReference(typeRef.Code);
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
            return elements?.Element.GetRootElement();
        }

        /// <summary>Returns the root element from the specified element list, if available, or <c>null</c>.</summary>
        internal static ElementDefinition GetRootElement(this List<ElementDefinition> elements)
        {
            return elements?.FirstOrDefault(e => e.IsRootElement());
        }

        /// <summary>
        /// Builds a fully qualified path for the ElementDefinition.
        /// </summary>
        /// <param name="def"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        /// <remarks>A fully qualified path is the path of the ElementDefinition, prefixed by the canonical of 
        /// the StructureDefinition the ElementDefinition is part of.</remarks>
        public static string CanonicalPath(this ElementDefinition def, StructureDefinition parent = null)
        {
            var path = parent.Url ?? "";
            path += $"#{(def?.Path ?? "(root)")}";
            if (def?.ElementId != null)
                path += $" ({def.ElementId})";
            return path;
        }


        /// <summary>
        /// Builds a fully qualified path for the ElementDefinition.
        /// </summary>
        /// <remarks>A fully qualified path is the path of the ElementDefinition, prefixed by the canonical of 
        /// the StructureDefinition the ElementDefinition is part of.</remarks>
        public static string CanonicalPath(this ElementDefinitionNavigator nav) =>
            CanonicalPath(nav.Current, nav.StructureDefinition);

        /// <summary>
        /// Given an name, determines whether this ElementDefinition's path matches the name.
        /// </summary>
        /// <param name="def"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>This function will match any definition for which the path is a direct match, or matches the element name without suffix.</remarks>
        public static bool MatchesName(this ElementDefinition def, string name)
        {
            var namePart = GetNameFromPath(def.Path);

            // Direct match
            if (namePart == name) return true;

            // Match an unconstrained choice type name
            var suffixedName = name + "[x]";
            if (namePart == suffixedName) return true;

            // Match a constrained choice type name, by looking at the original name of the element
            if (def.Base != null)
            {
                var baseNamePart = GetNameFromPath(def.Base.Path);
                if (baseNamePart == suffixedName) return true;
            }

            return false;
        }
    }
}


