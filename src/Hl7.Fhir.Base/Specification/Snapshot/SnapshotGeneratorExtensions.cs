/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20170209] cf. ConstrainedByDifferentialAnnotation
    // This extension indicates snapshot elements with associated differential constraints in the profile.
    // Note: extensions are persisted to XML/JSON, whereas annotations are ephemeral (in-memory only)

    /// <summary>Helper methods for the SnapshotGenerator class to generate and inspect custom extensions.</summary>
    public static class SnapshotGeneratorExtensions
    {
        /// <summary>The canonical url of the extension definition that marks snapshot elements with associated differential constraints.</summary>
        // public static readonly string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";
        public static readonly string CONSTRAINED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/constrainedByDifferentialExtension";
        public static readonly string STRUCTURE_DEFINITION_INTERFACE_EXT = "http://hl7.org/fhir/StructureDefinition/structuredefinition-interface";
        public static readonly string ELEMENTDEFINITION_SUPPRESS_EXT = "http://hl7.org/fhir/StructureDefinition/elementdefinition-suppress";

        /// <summary>
        /// Decorate the specified snapshot element definition with a special extension
        /// to indicate that the element is constrained by the differential.
        /// </summary>
        /// <param name="element">An <see cref="IExtendable"/> instance.</param>
        /// <param name="value">An optional boolean value (default <c>true</c>).</param>
        /// <remarks>Sets the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension to store the boolean flag.</remarks>
        internal static void SetConstrainedByDiffExtension(this IExtendable element, bool value = true)
        {
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }
            element.SetBoolExtension(CONSTRAINED_BY_DIFF_EXT, value);
        }

        /// <summary>Determines if the snapshot element was decorated with an extension indicating the element is constrained by the differential.</summary>
        /// <param name="element">An <see cref="IExtendable"/> instance.</param>
        /// <returns>A boolean value, or <c>null</c>.</returns>
        /// <remarks>Gets the boolean flag from the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension, if it exists.</remarks>
        public static bool? GetConstrainedByDiffExtension(this IExtendable element) => element.GetBoolExtension(CONSTRAINED_BY_DIFF_EXT);

        /// <summary>Removes the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension from the specified element definition.</summary>
        public static void RemoveConstrainedByDiffExtension(this IExtendable element)
        {
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }
            element.RemoveExtension(CONSTRAINED_BY_DIFF_EXT);
        }

        /// <summary>Determines if the element has the suppress extension indicating it should not be inherited.</summary>
        /// <param name="element">An <see cref="IExtendable"/> instance.</param>
        /// <returns>True if the element has the suppress extension, false otherwise.</returns>
        /// <remarks>Gets the boolean flag from the <see cref="ELEMENTDEFINITION_SUPPRESS_EXT"/> extension, if it exists.</remarks>
        public static bool HasSuppressExtension(this IExtendable element) 
        {
            if (element == null) return false;
            var suppressValue = element.GetBoolExtension(ELEMENTDEFINITION_SUPPRESS_EXT);
            return suppressValue == true;
        }

        /// <summary>Recursively removes all instances of the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension from the specified element definition and all it's child objects.</summary>
        [Obsolete("Use RemoveAllNonInheritableExtensions(this Element element) instead.")]
        public static void RemoveAllConstrainedByDiffExtensions(this Element element)
        {
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }
            element.RemoveConstrainedByDiffExtension();
            foreach (var child in element.Children().OfType<Element>())
            {
                child.RemoveAllConstrainedByDiffExtensions();
            }
        }

        /// <summary>Recursively removes all instances of the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension from all the elements and their respective child objects.</summary>
        [Obsolete("Use RemoveAllNonInheritableExtensions(this IEnumerable<T> elements) instead.")]
        public static void RemoveAllConstrainedByDiffExtensions<T>(this IEnumerable<T> elements) where T : Element
        {
            if (elements == null) { throw Error.ArgumentNull(nameof(elements)); }
            foreach (var elem in elements)
            {
                elem.RemoveAllConstrainedByDiffExtensions();
            }
        }

        internal static bool HasAnyNonInheritableExtensions(this Element element)
        {
            if (element == null) return false;
            if (element.HasNonInheritableExtensions()) return true;
#pragma warning disable CS0618 // Type or member is obsolete
            return element.Children().OfType<Element>().Any(child => child.HasAnyNonInheritableExtensions());
#pragma warning restore CS0618 // Type or member is obsolete
        }

        internal static bool HasNonInheritableExtensions(this IExtendable element)
        {
            if (element == null) 
                return false;

            if (element.Extension.Any(ext => _nonInheritableExtensions.Contains(ext.Url)))
                return true;

            if (element is IModifierExtendable modifierElement)
                return modifierElement.ModifierExtension.Any(ext => _nonInheritableExtensions.Contains(ext.Url));

            return false;
        }

        /// <summary>
        /// This extension removes all non-inheritable extensions from the specified element definition and all it's child objects.
        /// Non-inheritable extensions are extensions that should not be inherited by derived profiles.
        /// </summary>
        /// <param name="element"></param>
        internal static void RemoveAllNonInheritableExtensions(this Element element)
        {
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }
            element.RemoveNonInheritableExtensions();
#pragma warning disable CS0618 // Type or member is obsolete
            foreach (var child in element.Children().OfType<Element>())
#pragma warning restore CS0618 // Type or member is obsolete
            {
                child.RemoveAllNonInheritableExtensions();
            }
        }

        internal static void RemoveNonInheritableExtensions(this IExtendable element)
        {
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }
            element.RemoveExtensions(_nonInheritableExtensions);
        }

        private static readonly HashSet<string> _nonInheritableExtensions = [
         ResourceIdentity.CORE_BASE_URL + "elementdefinition-isCommonBinding",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-fmm",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-fmm-no-warnings",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-hierarchy",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-interface",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-normative-version",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-applicable-version",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-category",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-codegen-super",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-security-category",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-standards-status",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-summary",
         ResourceIdentity.CORE_BASE_URL + "structuredefinition-wg",
         ResourceIdentity.CORE_BASE_URL + "replaces",
         ResourceIdentity.CORE_BASE_URL + "resource-approvalDate",
         ResourceIdentity.CORE_BASE_URL + "resource-effectivePeriod",
         ResourceIdentity.CORE_BASE_URL + "resource-lastReviewDate",
         CONSTRAINED_BY_DIFF_EXT //this is our own extension to define differences compared to the base, this can't be inherited from the base profile
        ];
    }
}