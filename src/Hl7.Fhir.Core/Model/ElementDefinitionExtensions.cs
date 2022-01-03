/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model
{
    public static class ElementDefinitionExtensions
    {
        public static ElementDefinition Unbounded(this ElementDefinition ed)
        {
            ed.Max = "*";
            return ed;
        }

        public static ElementDefinition Prohibited(this ElementDefinition ed)
        {
            ed.Min = 0;
            ed.Max = "0";
            return ed;
        }

        public static ElementDefinition Required(this ElementDefinition ed, int min = 1, string max = "1")
        {
            ed.Min = min;
            ed.Max = max;
            return ed;
        }

        public static ElementDefinition OfType(this ElementDefinition ed, FHIRAllTypes type, string profile = null)
        {
            ed.Type.Clear();
            ed.OrType(type, profile);

            return ed;
        }

        public static ElementDefinition OfReference(this ElementDefinition ed, string targetProfile, IEnumerable<ElementDefinition.AggregationMode> aggregation = null, string profile = null)
        {
            ed.Type.Clear();
            ed.OrReference(targetProfile, aggregation, profile);

            return ed;
        }

        public static ElementDefinition OrType(this ElementDefinition ed, FHIRAllTypes type, string profile = null)
        {
            if (type == FHIRAllTypes.Reference) throw Error.InvalidOperation("Use OfReference/OrReference instead of OfType/OrType for references");

            var newType = new ElementDefinition.TypeRefComponent { Code = type.GetLiteral() };
            if (profile != null) newType.Profile = profile;

            ed.Type.Add(newType);

            return ed;
        }

        public static ElementDefinition OrReference(this ElementDefinition ed, string targetProfile, IEnumerable<ElementDefinition.AggregationMode> aggregation = null, string profile = null)
        {
            var newType = new ElementDefinition.TypeRefComponent { Code = FHIRAllTypes.Reference.GetLiteral() };

            if (targetProfile != null) newType.TargetProfile = targetProfile;
            if (profile != null) newType.Profile = profile;
            if (aggregation != null) newType.Aggregation = aggregation.Cast<ElementDefinition.AggregationMode?>();

            ed.Type.Add(newType);

            return ed;
        }

        public static ElementDefinition Value(this ElementDefinition ed, DataType fix = null, DataType pattern = null)
        {
            ed.Fixed = fix;
            ed.Pattern = pattern;

            return ed;
        }

        public static ElementDefinition WithBinding(this ElementDefinition ed, string valueSetUri, BindingStrength strength)
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = new ResourceReference(valueSetUri),
                Strength = strength
            };

            ed.Binding = binding;

            return ed;
        }

        public static ElementDefinition WithSlicingIntro(this ElementDefinition ed, ElementDefinition.SlicingRules rules,
            params (ElementDefinition.DiscriminatorType type, string path)[] discriminators)

        {
            ed.Slicing = new ElementDefinition.SlicingComponent
            {
                Rules = rules,
                Discriminator = discriminators.Select(t =>
                    new ElementDefinition.DiscriminatorComponent { Path = t.path, Type = t.type })
                    .ToList()
            };

            return ed;
        }


        /*
         * From ProfileNavigationExtensions.cs
         */

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

        public static bool IsExtension(this ElementDefinition defn) => defn != null && IsExtensionPath(defn.Path);

        public static bool IsExtensionPath(string path) => !string.IsNullOrEmpty(path) && (path.EndsWith(".extension") || path.EndsWith(".modifierExtension"));

        // [WMR 20160805] New
        public static bool IsRootElement(this ElementDefinition defn) => defn != null && IsRootPath(defn.Path);

        /// <summary>Determines if the specified element path represents a root element.</summary>
        public static bool IsRootPath(string path) => !string.IsNullOrEmpty(path) && !path.Contains(".");


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
            return GetParentPath(defn.Path);
        }

        /// <summary>Returns the parent path of the specified element path, or an empty string.</summary>
        public static string GetParentPath(string child)
        {
            var dot = child.LastIndexOf(".");
            return dot != -1 ? child.Substring(0, dot) : String.Empty;
        }


        /// <summary>Returns the root element from the specified element list, if available, or <c>null</c>.</summary>
        public static ElementDefinition GetRootElement(this IElementList elements)
        {
            return elements?.Element.GetRootElement();
        }

        /// <summary>Returns the root element from the specified element list, if available, or <c>null</c>.</summary>
        public static ElementDefinition GetRootElement(this List<ElementDefinition> elements)
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


        /*
         * From ElementDefinitionNavigatorExtensions.cs
         */
        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed)
        {
            //TODO: There is something smarter for this in STU3
            var path = ed.Path;

            return path.EndsWith(".value") && ed.Type.All(t => t.Code == null);
        }

        public static bool IsResourcePlaceholder(this ElementDefinition ed)
            => ed.Type is not null && ed.Type.Any(t => t.Code == "Resource" || t.Code == "DomainResource");


        /*
         * From TypeRefExtension.cs
         */

        public static string ReadableName(this StructureDefinition sd) => sd.Derivation == StructureDefinition.TypeDerivationRule.Constraint ? sd.Url : sd.Id;

        public static string GetDeclaredProfiles(this ElementDefinition.TypeRefComponent typeRef)
        {
            if (!System.String.IsNullOrEmpty(typeRef.Profile))
                return typeRef.Profile;
            else if (!string.IsNullOrEmpty(typeRef.Code))
                return ModelInfo.CanonicalUriForFhirCoreType(typeRef.Code);
            else
                return null;
        }

        public static bool HasChoices(this ElementDefinition definition)
        {
            return definition.Type.Where(tr => tr.Code != null).Distinct().Count() > 1;
        }

        /// <summary>Determines if the specified element definition represents a type choice element by verifying that the element name ends with "[x]".</summary>
        /// <param name="defn">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a type choice element, or <c>false</c> otherwise.</returns>
        public static bool IsChoice(this ElementDefinition defn)
        {
            return defn.Path.EndsWith("[x]");
        }


        public static List<FHIRAllTypes> ChoiceTypes(this ElementDefinition definition)
        {
            return definition.Type.Where(tr => tr.Code != null).Select(tr => EnumUtility.ParseLiteral<FHIRAllTypes>(tr.Code).Value).Distinct().ToList();
        }

    }
}
