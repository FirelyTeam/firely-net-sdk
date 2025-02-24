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

#nullable enable

namespace Hl7.Fhir.Model
{
    public static class ElementDefinitionExtensions
    {
        /// <summary>
        /// Changes the <see cref="ElementDefinition"/> to have a maximum cardinality of "*".
        /// </summary>
        public static ElementDefinition Unbounded(this ElementDefinition ed)
        {
            ed.Max = "*";
            return ed;
        }

        /// <summary>
        /// Changes the <see cref="ElementDefinition"/> to have a cardinality of 0..0.
        /// </summary>
        public static ElementDefinition Prohibited(this ElementDefinition ed)
        {
            ed.Min = 0;
            ed.Max = "0";
            return ed;
        }

        /// <summary>
        /// Changes the <see cref="ElementDefinition"/> to have a cardinality of 1..1.
        /// </summary>
        /// <param name="ed">The <see cref="ElementDefinition"/> to change</param>
        /// <param name="min">Optional. Sets the minimum cardinality to something else than 1.</param>
        /// <param name="max">Optional. Sets the maximum cardinality to something else than 1.</param>
        /// <returns></returns>
        public static ElementDefinition Required(this ElementDefinition ed, int min = 1, string max = "1")
        {
            ed.Min = min;
            ed.Max = max;
            return ed;
        }

        /// <summary>
        /// Sets <see cref="ElementDefinition.Type"/> to a single <see cref="ElementDefinition.TypeRefComponent"/>.
        /// </summary>
        public static ElementDefinition OfType(this ElementDefinition ed, string type, string? profile = null)
        {
            ed.Type.Clear();
            ed.OrType(type, profile);

            return ed;
        }

        /// <inheritdoc cref="OfType(ElementDefinition, string, string?)"/>
        public static ElementDefinition OfType(this ElementDefinition ed, FHIRAllTypes type, string? profile = null) => ed.OfType(type.GetLiteral(), profile);

        private static readonly string REFERENCE_LITERAL = FHIRAllTypes.Reference.GetLiteral();

        /// <summary>
        /// Adds a <see cref="ElementDefinition.TypeRefComponent"/> to the given <see cref="ElementDefinition"/>.
        /// </summary>
        public static ElementDefinition OrType(this ElementDefinition ed, string type, string? profile = null)
        {
            if (type == REFERENCE_LITERAL) throw Error.InvalidOperation("Use OfReference/OrReference instead of OfType/OrType for references");

            var newType = new ElementDefinition.TypeRefComponent
            {
                Code = type,
                Profile = profile
            };

            ed.Type.Add(newType);

            return ed;
        }

        /// <inheritdoc cref="OrType(ElementDefinition, string, string?)"/>
        public static ElementDefinition OrType(this ElementDefinition ed, FHIRAllTypes type, string? profile = null) => ed.OrType(type.GetLiteral(), profile);

        /// <summary>
        /// Sets <see cref="ElementDefinition.Type"/> to a single "reference" with the given attributes.
        /// </summary>        
        public static ElementDefinition OfReference(this ElementDefinition ed, string targetProfile, IEnumerable<ElementDefinition.AggregationMode>? aggregation = null, string? profile = null)
        {
            ed.Type.Clear();
            ed.OrReference(targetProfile, aggregation, profile);

            return ed;
        }

        /// <summary>
        /// Adds a <see cref="ElementDefinition.TypeRefComponent"/> to the given <see cref="ElementDefinition"/> 
        /// for a "reference" with the given attributes.
        /// </summary>
        public static ElementDefinition OrReference(this ElementDefinition ed, string targetProfile, IEnumerable<ElementDefinition.AggregationMode>? aggregation = null, string? profile = null)
        {
            var newType = new ElementDefinition.TypeRefComponent
            {
                Code = REFERENCE_LITERAL,
                TargetProfile = targetProfile,
                Profile = profile,
                Aggregation = aggregation?.Cast<ElementDefinition.AggregationMode?>()!
            };

            ed.Type.Add(newType);
            return ed;
        }

        /// <summary>
        /// Sets the "fixed" or "pattern" elements of a <see cref="ElementDefinition"/>.
        /// </summary>
        public static ElementDefinition Value(this ElementDefinition ed, DataType? fix = null, DataType? pattern = null)
        {
            ed.Fixed = fix;
            ed.Pattern = pattern;

            return ed;
        }

        /// <summary>
        /// Sets the binding to the given valueset and strength.
        /// </summary>
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

        /// <summary>
        /// Sets the "slicing" element of an <see cref="ElementDefinition "/> that serves as the slicing intro.
        /// </summary>
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

        /// <summary>
        /// Determines whether the given count of an element is within the range of its cardinality.
        /// </summary>
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

        /// <summary>
        /// Determines whether the cardinality allows repetition of the element.
        /// </summary>
        public static bool IsRepeating(this ElementDefinition defn) => defn != null && defn.Max != "1" && defn.Max != "0";

        /// <summary>
        /// Determines whether the given path navigates to an extension or modifierExtension.
        /// </summary>
        public static bool IsExtensionPath(string path) => !string.IsNullOrEmpty(path) && (path.EndsWith(".extension") || path.EndsWith(".modifierExtension"));

        /// <summary>
        /// Determines whether the given element is the root element of an extension or modifierExtension.
        /// </summary>
        public static bool IsExtension(this ElementDefinition? defn) => defn?.Path is {} p && IsExtensionPath(p);

        /// <summary>Determines if the specified element path represents a root element.</summary>
        public static bool IsRootPath(string path) => !string.IsNullOrEmpty(path) && !path.Contains('.');

        /// <summary>
        /// Determines if the specified element is the root element.
        /// </summary>
        public static bool IsRootElement(this ElementDefinition? defn) => defn?.Path is {} p && IsRootPath(p);

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A list of type code strings.</returns>
        public static IReadOnlyList<string> DistinctTypeCodes(this List<ElementDefinition.TypeRefComponent> types)
            => types.Where(t => t.Code != null).Select(t => t.Code!).Distinct().ToList();

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="elem">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns>A list of type code strings.</returns>
        public static IReadOnlyList<string> DistinctTypeCodes(this ElementDefinition elem) => elem.Type.DistinctTypeCodes();

        /// <summary>
        /// Returns the profile on the given <see cref="ElementDefinition.TypeRefComponent"/> if specified, 
        /// or otherwise the core profile url for the specified type code.
        /// </summary>
        public static string? GetTypeProfile(this ElementDefinition.TypeRefComponent elemType) =>
            elemType?.Profile ?? (elemType?.Code is not null ? ModelInfo.CanonicalUriForFhirCoreType(elemType.Code).Value : null);

        /// <summary>
        /// Determines if the specified element definition represents a <see cref="ResourceReference"/>.
        /// </summary>
        public static bool IsReference(this ElementDefinition defn)
        {
            var primaryType = defn.Type.FirstOrDefault();
            return primaryType != null && IsReference(primaryType);
        }

        /// <summary>
        /// Determines if the specified element is a backbone element.
        /// </summary>
        public static bool IsBackboneElement(this ElementDefinition defn) =>
            defn.Path?.Contains('.') == true && defn.Type.Count == 1 &&
            defn.Type[0].Code is "BackboneElement" or "Element";

        /// <summary>
        /// Determines if the specified type reference represents a <see cref="ResourceReference"/>.
        /// </summary>
        public static bool IsReference(this ElementDefinition.TypeRefComponent typeRef) => typeRef.Code == REFERENCE_LITERAL;

        /// <summary>
        /// Returns the last part of the element's path.
        /// </summary>
        public static string GetNameFromPath(string path)
        {
            var pos = path.LastIndexOf(".", StringComparison.Ordinal);

            return pos != -1 ? path[(pos + 1)..] : path;
        }

        /// <inheritdoc cref="GetNameFromPath(string)"/>
        public static string GetNameFromPath(this ElementDefinition defn) =>
            GetNameFromPath(defn.Path ?? throw new ArgumentException("ElementDefinition must have a Path.", nameof(defn)));

        /// <summary>
        /// Returns the parent path of the specified element path, or an empty string if there is no parent.
        /// </summary>
        public static string GetParentPath(string child)
        {
            var dot = child.LastIndexOf(".", StringComparison.Ordinal);
            return dot != -1 ? child[..dot] : string.Empty;
        }

        /// <inheritdoc cref="GetParentPath(string)"/>
        public static string GetParentNameFromPath(this ElementDefinition defn) =>
            GetParentPath(defn.Path ?? throw new ArgumentException("ElementDefinition must have a Path.", nameof(defn)) );

        /// <summary>
        /// Returns the root element from the specified element list if available, or <c>null</c> otherwise.
        /// </summary>
        public static ElementDefinition? GetRootElement(this IList<ElementDefinition> elements) =>
            elements?.FirstOrDefault(e => e.IsRootElement());

        /// <inheritdoc cref="GetRootElement(IList{ElementDefinition})"/>   
        public static ElementDefinition? GetRootElement(this IElementList elements) => elements?.Element.GetRootElement();

        /// <summary>
        /// Builds a fully qualified path for the ElementDefinition.
        /// </summary>
        /// <returns>The path of the <see cref="ElementDefinition"/>, prefixed by the canonical of 
        /// the parent <see cref="StructureDefinition"/>.</returns>
        public static string CanonicalPath(this ElementDefinition def, StructureDefinition? parent = null)
        {
            var path = parent?.Url ?? "";
            path += $"#{(def?.Path ?? "(root)")}";
            if (def?.ElementId != null)
                path += $" ({def.ElementId})";
            return path;
        }

        /// <summary>
        /// Given an name, determines whether this ElementDefinition's path matches the name.
        /// </summary>
        /// <remarks>This function will match any definition for which the path is a direct match, or matches the element name without suffix.</remarks>
        public static bool MatchesName(this ElementDefinition def, string name)
        {
            var namePart = GetNameFromPath(def.Path ?? throw new ArgumentException("ElementDefinition must have a Path.", nameof(def)));

            // Direct match
            if (namePart == name) return true;

            // Match an unconstrained choice type name
            var suffixedName = name + "[x]";
            if (namePart == suffixedName) return true;

            // Match a constrained choice type name, by looking at the original name of the element
            if (def.Base?.Path is not null)
            {
                var baseNamePart = GetNameFromPath(def.Base.Path);
                if (baseNamePart == suffixedName) return true;
            }

            return false;
        }

        /*
         * From ElementDefinitionNavigatorExtensions.cs
         */

        /// <summary>
        /// Determines whether this element is the <c>Value</c> element of a FHIR primitive.
        /// </summary>
        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed)
        {
            return ed.Path?.EndsWith(".value") == true && ed.Type.All(t => t.Code == null);
        }

        /// <summary>
        /// Determines whether this element contains a nested resource.
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        public static bool IsResourcePlaceholder(this ElementDefinition ed) =>
            ed.Type.Any(t => t.Code is "Resource" or "DomainResource");

        /*
         * From TypeRefExtension.cs
         */

        /// <summary>
        /// Returns the human-readable name for this <see cref="StructureDefinition"/>.
        /// </summary>
        public static string? ReadableName(this StructureDefinition sd) =>
            sd.Derivation == StructureDefinition.TypeDerivationRule.Constraint ? sd.Url : sd.Id;

        /// <summary>
        /// Determines whether the element allows values of more than one type.
        /// </summary>
        public static bool HasChoices(this ElementDefinition definition) => definition.Type.Where(tr => tr.Code != null).Distinct().Count() > 1;

        /// <summary>
        /// Determines if the specified element definition represents a type choice element by verifying that the element name ends with "[x]".
        /// </summary>
        public static bool IsChoice(this ElementDefinition defn) =>
            defn.Path?.EndsWith("[x]") ?? throw new ArgumentException("ElementDefinition must have a Path.", nameof(defn));
    }
}

#nullable restore