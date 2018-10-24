/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model
{
    public static class ElementDefinitionUtilities
    {
        /// <summary>
        /// Changes the given ElementDefinition to have no upper limit on the cardinality.
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        public static ElementDefinition Unbounded(this ElementDefinition ed)
        {
            ed.Max = "*";
            return ed;
        }

        /// <summary>
        /// Changes the given ElementDefinition to prohibit the element.
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        /// <remarks>This is done by setting the upper limit of the cardinality to 0.</remarks>
        public static ElementDefinition Prohibited(this ElementDefinition ed)
        {
            ed.Min = 0;
            ed.Max = "0";
            return ed;
        }

        /// <summary>
        /// Ensures that the given ElementDefinition has a minimum and maximum cardinality of 1, unless specified otherwise.
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ElementDefinition Required(this ElementDefinition ed, int min = 1, string max = "1")
        {
            ed.Min = min;
            ed.Max = max;
            return ed;
        }

        /// <summary>
        /// Adds a TypeRef to the given ElementDefinition
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="type"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static ElementDefinition Type(this ElementDefinition ed, FHIRDefinedType type, string profile = null)
        {
            var newType = new ElementDefinition.TypeRefComponent { Code = type };
            if (profile != null) newType.Profile = new[] { profile };

            ed.Type.Add(newType);
            return ed;
        }

        /// <summary>
        /// Sets the TypeRef of the given ElementDefinition to the single type specified in the arguments.
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="targetProfile"></param>
        /// <param name="aggregation"></param>
        /// <returns></returns>
        public static ElementDefinition Reference(this ElementDefinition ed, string targetProfile = null, IEnumerable<ElementDefinition.AggregationMode> aggregation = null)
        {
            var newType = new ElementDefinition.TypeRefComponent { Code = FHIRDefinedType.Reference };
            if (targetProfile != null) newType.Profile = new[] { targetProfile };
            if (aggregation != null) newType.Aggregation = aggregation.Cast<ElementDefinition.AggregationMode?>();

            ed.Type.Add(newType);
            return ed;
        }


        /// <summary>
        /// Set the Fixed or Pattern values of the ElementDefinition
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="fix"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static ElementDefinition Value(this ElementDefinition ed, Element fix=null, Element pattern=null )
        {
            ed.Fixed = fix;
            ed.Pattern = pattern;

            return ed;
        }

        /// <summary>
        /// Set the Binding of the given ElementDefinition
        /// </summary>
        /// <param name="ed"></param>
        /// <param name="valueSetUri"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static ElementDefinition WithBinding(this ElementDefinition ed, string valueSetUri, BindingStrength strength)
        {
            var binding = new ElementDefinition.BindingComponent
            {
                ValueSet = new ResourceReference(valueSetUri),
                Strength = strength
            };

            ed.Binding = binding;

            return ed;
        }

        /// <summary>
        /// Determines whether the given instance count is in range for the cardinality of the element.
        /// </summary>
        /// <param name="defn"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool InRange(this ElementDefinition defn, int count)
        {
            int min = Convert.ToInt32(defn.Min);
            if (count < min)
                return false;

            if (defn.Max == "*")
                return true;

            int max = Convert.ToInt32(defn.Max);

            return count > max ? false : true;
        }

        /// <summary>
        /// Determines if the specified element path is a root, i.e. does not contain a '.'.
        /// </summary>
        public static bool IsRootPath(string path) => !string.IsNullOrEmpty(path) && !path.Contains(".");

        /// <summary>
        /// Determines if the element is the root element of a StructureDefinition.
        /// </summary>
        /// <param name="defn"></param>
        /// <returns></returns>
        public static bool IsRootElement(this ElementDefinition defn) => IsRootPath(defn.Path);

        /// <summary>
        /// Determines if the cardinality of the defined element allows repetitions.
        /// </summary>
        /// <param name="defn"></param>
        /// <returns><c>true</c> if Max is not given, or Max > 1 </returns>
        public static bool IsRepeating(this ElementDefinition defn) => defn.Max != "1" && defn.Max != "0";

        /// <summary>
        /// Determines if the specified element is a backbone element
        /// </summary>
        /// <param name="defn"></param>
        /// <returns></returns>
        /// <remarks>Backbone elements are nested groups of elements, that appear within resources (of type BackboneElement) or as
        /// within datatypes (of type Element).
        ///</remarks>
        public static bool IsBackboneElement(this ElementDefinition defn) => defn.Path.Contains(".") && defn.Type.Count == 1 &&
            (defn.Type[0].Code == FHIRDefinedType.BackboneElement || defn.Type[0].Code == FHIRDefinedType.Element);


        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A list of type code strings.</returns>
        public static FHIRDefinedType[] DistinctTypeCodes(this IEnumerable<ElementDefinition.TypeRefComponent> types)
            => types.Where(t => t.Code != null).Select(t => t.Code.Value).Distinct().ToArray();

        /// <summary>Returns a list of distinct type codes supported by the specified element definition.</summary>
        /// <param name="elem">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns>A list of type code strings.</returns>
        public static FHIRDefinedType[] DistinctTypeCodes(this ElementDefinition elem) => elem.Type.DistinctTypeCodes();

        /// <summary>Determines if the specified type reference represents a <see cref="ResourceReference"/>.</summary>
        /// <param name="typeRef">A <see cref="ElementDefinition.TypeRefComponent"/> instance.</param>
        /// <returns><c>true</c> if the instance defines a reference, or <c>false</c> otherwise.</returns>
        public static bool IsReference(this ElementDefinition.TypeRefComponent typeRef) 
            => typeRef.Code.HasValue && ModelInfo.IsReference(typeRef.Code.Value);

        public const string CHOICE_ELEMENT_SUFFIX = "[x]";

        /// <summary>Determines if the specified element definition has a path ending in "[x]".</summary>
        public static bool HasChoiceSuffix(this ElementDefinition defn) => HasChoiceSuffix(defn.Path);

        /// <summary>Determines if the specified path ends in "[x]".</summary>
        public static bool HasChoiceSuffix(string path) => path.EndsWith("[x]");

        /// <summary>
        /// Determines whether an element is a choice element
        /// </summary>
        /// <param name="definition"></param>
        /// <remarks>Note that this determines whether the element is choice, regardless of whether it has been constrained to allow only a single type.</remarks>
        public static bool IsChoiceElement(this ElementDefinition definition)
        {
            // If in a constrained element, the suffix [x] has been replaced by an actual type, it's still originally a choice element.
            if (definition.Base?.Path?.EndsWith(CHOICE_ELEMENT_SUFFIX) == true) return true;

            // Otherwise, check for the suffix on the path.
            return definition.Path.EndsWith(CHOICE_ELEMENT_SUFFIX);
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
            if(def.Base != null)
            {
                var baseNamePart = GetNameFromPath(def.Base.Path);
                if (baseNamePart == suffixedName) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the given definition defines the primitive value of an element.
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        /// <remarks>This is the <c>value</c> attribute in XML, of the literal value in JSON.</remarks>
        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed)
        {
            //TODO: There is something smarter for this in STU3
            var path = ed.Path;

            return path.EndsWith(".value") && ed.Type.All(t => t.Code == null);
        }

        /// <summary>
        /// Returns the last part of the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetNameFromPath(string path)
        {
            var pos = path.LastIndexOf(".");

            return pos != -1 ? path.Substring(pos + 1) : path;
        }

        /// <summary>
        /// Returns the last part of the path of the given ElementDefinition.
        /// </summary>
        public static string GetNameFromPath(this ElementDefinition defn) => GetNameFromPath(defn.Path);


    }
}
