using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>Extension methods for the <see cref="SnapshotGenerator"/> class.</summary>
    public static class SnapshotGeneratorExtensions
    {
        /// <summary>The canonical url of the extension definition that marks snapshot elements with associated differential constraints.</summary>
        public static readonly string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";

        /// <summary>Mark the snapshot element as changed by the differential.</summary>
        /// <param name="element">An <see cref="IExtendable"/> instance.</param>
        /// <param name="value">An optional boolean value (default <c>true</c>).</param>
        /// <remarks>Sets the <see cref="CHANGED_BY_DIFF_EXT"/> extension to store the boolean flag.</remarks>
        public static void SetChangedByDiff(this IExtendable element, bool value = true)
        {
            if (element == null) { throw Error.ArgumentNull("element"); }
            element.SetBoolExtension(CHANGED_BY_DIFF_EXT, value);
        }

        /// <summary>Determines wether the snapshot element was marked as changed by the differential.</summary>
        /// <param name="element">An <see cref="IExtendable"/> instance.</param>
        /// <returns>A boolean value, or <c>null</c>.</returns>
        /// <remarks>Gets the boolean flag from the <see cref="CHANGED_BY_DIFF_EXT"/> extension, if it exists.</remarks>
        public static bool? GetChangedByDiff(this IExtendable element)
        {
            return element.GetBoolExtension(CHANGED_BY_DIFF_EXT);
        }

        /// <summary>Removes the <see cref="CHANGED_BY_DIFF_EXT"/> extension from the element.</summary>
        /// <param name="element">An <see cref="IExtendable"/> instance.</param>
        public static void ClearChangedByDiff(this IExtendable element)
        {
            if (element == null) { throw Error.ArgumentNull("element"); }
            element.RemoveExtension(CHANGED_BY_DIFF_EXT);
        }

        /// <summary>Removes the <see cref="CHANGED_BY_DIFF_EXT"/> extension from the snapshot element definition and it's child elements.</summary>
        /// <param name="element">An <see cref="ElementDefinition"/> instance.</param>
        public static void ClearAllChangedByDiff(this ElementDefinition element)
        {
            ClearAllExtensions(element, CHANGED_BY_DIFF_EXT);
        }

        /// <summary>Removes a specific extension from the snapshot element definition and it's child elements.</summary>
        /// <param name="element">An <see cref="ElementDefinition"/> instance.</param>
        /// <param name="uri">The canonical url of the extension.</param>
        internal static void ClearAllExtensions(this ElementDefinition element, string uri)
        {
            if (element == null) return;
            element.RemoveExtension(uri);
            ClearAllExtensions(element.AliasElement, uri);
            ClearExtension(element.Base, uri);
            ClearExtension(element.Binding, uri);
            ClearAllExtensions(element.Code, uri);
            ClearExtension(element.CommentsElement, uri);
            ClearAllExtensions(element.ConditionElement, uri);
            ClearAllExtensions(element.Constraint, uri);
            ClearExtension(element.DefaultValue, uri);
            ClearExtension(element.DefinitionElement, uri);
            ClearExtension(element.Example, uri);
            ClearAllExtensions(element.Extension, uri);
            ClearAllExtensions(element.FhirCommentsElement, uri);
            ClearExtension(element.Fixed, uri);
            ClearExtension(element.IsModifierElement, uri);
            ClearExtension(element.IsSummaryElement, uri);
            ClearExtension(element.LabelElement, uri);
            ClearAllExtensions(element.Mapping, uri);
            ClearExtension(element.MaxElement, uri);
            ClearExtension(element.MaxLengthElement, uri);
            ClearExtension(element.MaxValue, uri);
            ClearExtension(element.MeaningWhenMissingElement, uri);
            ClearExtension(element.MinElement, uri);
            ClearExtension(element.MinValue, uri);
            ClearExtension(element.MustSupportElement, uri);
            ClearExtension(element.NameElement, uri);
            ClearExtension(element.NameReferenceElement, uri);
            ClearExtension(element.PathElement, uri);
            ClearExtension(element.Pattern, uri);
            ClearAllExtensions(element.RepresentationElement, uri);
            ClearExtension(element.ShortElement, uri);
            ClearExtension(element.Slicing, uri);
            ClearExtension(element.RequirementsElement, uri);
            ClearAllExtensions(element.Type, uri);
        }

        internal static void ClearAllExtensions<T>(this IList<T> extendables, string uri) where T : IExtendable
        {
            if (extendables == null) return;
            foreach (var ext in extendables)
            {
                ClearExtension(ext, uri);
            }
        }

        static void ClearExtension(this IExtendable extendable, string uri)
        {
            if (extendable != null) { extendable.RemoveExtension(uri); }
        }

    }
}
