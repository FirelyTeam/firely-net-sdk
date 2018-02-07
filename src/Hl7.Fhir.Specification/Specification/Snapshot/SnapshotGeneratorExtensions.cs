/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20170209] cf. ConstrainedByDifferentialAnnotation
    // This extension indicates snapshot elements with associated differential constraints in the profile.
    // Note: extensions are persisted to XML/JSON, whereas annotations are ephemeral (in-memory only)

    /// <summary>Helper methods for the <see cref="SnapshotGenerator"/> class to generate and inspect custom extensions.</summary>
    public static class SnapshotGeneratorExtensions
    {
        /// <summary>The canonical url of the extension definition that marks snapshot elements with associated differential constraints.</summary>
        // public static readonly string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";
        public static readonly string CONSTRAINED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/constrainedByDifferentialExtension";

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

        /// <summary>Recursively removes all instances of the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension from the specified element definition and all it's child objects.</summary>
        public static void RemoveAllConstrainedByDiffExtensions(this Element element)
        {
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }
            element.RemoveConstrainedByDiffExtension();
            foreach (var child in element.Children.OfType<Element>())
            {
                child.RemoveAllConstrainedByDiffExtensions();
            }
        }

        /// <summary>Recursively removes all instances of the <see cref="CONSTRAINED_BY_DIFF_EXT"/> extension from all the elements and their respective child objects.</summary>
        public static void RemoveAllConstrainedByDiffExtensions<T>(this IEnumerable<T> elements) where T : Element
        {
            if (elements == null) { throw Error.ArgumentNull(nameof(elements)); }
            foreach (var elem in elements)
            {
                elem.RemoveAllConstrainedByDiffExtensions();
            }
        }

        // ========== For internal use only ==========
        // [WMR 20170209] OBSOLETE
#if false

        /// <summary>Removes a specific extension from the snapshot element definition and it's descendant elements, recursively.</summary>
        /// <param name="elemDef">An <see cref="ElementDefinition"/> instance.</param>
        /// <param name="uri">The canonical url of the extension.</param>
        static void ClearAllExtensions(this ElementDefinition elemDef, string uri)
        {
            if (elemDef != null)
            {
                ClearExtensions(elemDef, uri);
            }
        }

        static void ClearExtensions<T>(this IEnumerable<T> elements, string uri) where T : Base
        {
            if (elements != null)
            {
                foreach (var child in elements)
                {
                    ClearExtensions(child, uri);
                }
            }
        }

        static void ClearExtensions<T>(this T element, string uri) where T : Base
        {
            if (element != null)
            {
                ClearExtension(element as IExtendable, uri);
                ClearExtensions(element.Children, uri);
            }
        }

        static void ClearExtension(this IExtendable extendable, string uri)
        {
            extendable?.RemoveExtension(uri);
        }
#endif

    }
}
