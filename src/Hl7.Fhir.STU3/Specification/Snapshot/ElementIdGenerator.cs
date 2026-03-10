/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160917] STU3: (re-)generate ElementId values
    // http://hl7.org/fhir/STU3/elementdefinition.html#id

    // [WMR 20170614] TODO
    // - Generate normalized ID for type slices of the form [originalElementName]:[sliceName]
    //   Tooling convention: initialize sliceName with renamed element, e.g. "valueString"
    //   Example: Patient.deceased[x]:deceasedBoolean
    // - Maintain any existing element IDs in differential
    //   But generate canonical IDs for child elements without explicit user IDs
    //   Example: Patient.identifier:ssn : ID = "PatientSsnId" (overridden)
    //        =>  Patient.identifier:ssn.system : ID = "Patient.identifier:ssn.system" (NOT "PatientSsnId.system")
    // - Expose public utility functions to
    //   * Generate element id segment from element name and slice name
    //   * Verify if an element id has the canonical form

    // Syntax: [path[:name][.path[:name]...]]
    // - Element.ElementId has the exact same number of components as Element.Path
    // - Components are separated by the dot character "."
    // - First part of each component equals the local path component, i.e. the FHIR element name
    // - Second optional part of each component equals the slice name, if not empty, separated by a semi-colon ":"
    // Notes:
    // - slice name may not contain dot characters "." !
    // - for re-slicing, the slice name can be of the form "OrignalSlice/Reslice"
    // - DSTU2: id has regex  [A-Za-z0-9\-\.]{1,64} => cannot use proposed id scheme

    /// <summary>Represents a segment of an element Id, derived from the (original) element name and the (optional) slice name.</summary>
    public struct ElementIdSegment
    {
        /// <summary>Represents an empty segment.</summary>
        public static readonly ElementIdSegment Empty = new ElementIdSegment(null, null);

        /// <summary>Create a new <see cref="ElementIdSegment"/> from the specified (original) element name and (optional) slice name.</summary>
        public static ElementIdSegment Create(string elementName, string sliceName = null)
        {
            if (string.IsNullOrEmpty(elementName)) { throw Error.ArgumentNull(nameof(elementName)); }
            return new ElementIdSegment(elementName, sliceName);
        }

        /// <summary>Create a new <see cref="ElementIdSegment"/> from the specified element definition.</summary>
        public static ElementIdSegment Create(ElementDefinition elemDef) => new ElementIdSegment(elemDef);

        /// <summary>
        /// Create a new <see cref="ElementIdSegment"/> by parsing the specified id segment string of the form "elementName[:sliceName]".
        /// </summary>
        public static ElementIdSegment Parse(string idSegment)
        {
            if (string.IsNullOrEmpty(idSegment)) { throw Error.ArgumentNull(nameof(idSegment)); }
            if (idSegment.IndexOf(ElementIdGenerator.ElementIdSegmentDelimiter) > -1) { throw Error.Argument(nameof(idSegment), $"The specified element id segment is invalid. A segment cannot contain the segment delimiter character '{ElementIdGenerator.ElementIdSegmentDelimiter}'."); }

            var pos = idSegment.IndexOf(ElementIdGenerator.ElementIdSliceNameDelimiter);
            if (pos == -1) { return new ElementIdSegment(idSegment); }
            return new ElementIdSegment(idSegment.Substring(0, pos), idSegment.Substring(pos + 1));
        }

        /// <summary>Parse the specified element id into a sequence of <see cref="ElementIdSegment"/>s.</summary>
        public static IEnumerable<ElementIdSegment> ParseId(string elementId)
        {
            if (string.IsNullOrEmpty(elementId)) { throw Error.ArgumentNull(nameof(elementId)); }
            var segments = ElementIdGenerator.ParseId(elementId);
            return segments.Select(s => ElementIdSegment.Parse(s));
        }

        ElementIdSegment(string elementName, string sliceName = null)
        {
            ElementName = elementName;
            SliceName = sliceName;
        }

        ElementIdSegment(ElementDefinition elemDef)
        {
            elemDef.ThrowIfNullOrEmptyPath(nameof(elemDef));

            var basePath = elemDef.Base?.Path;
            var elemPath = basePath != null && ElementDefinitionNavigator.IsChoiceTypeElement(basePath) ? basePath : elemDef.Path;
            ElementName = ProfileNavigationExtensions.GetNameFromPath(elemPath);
            SliceName = elemDef.SliceName;
        }

        /// <summary>Returns the (original) element name.</summary>
        public readonly string ElementName;

        /// <summary>Returns the (optional) slice name, or <c>null</c>.</summary>
        public readonly string SliceName;

        /// <summary>Determines if this instance represents an empty segment, i.e. if the <see cref="ElementName"/> equals <c>null</c>.</summary>
        public bool IsEmpty => ElementName == null;

        /// <summary>Returns a formatted element id segment of the form "ElementName[:SliceName]".</summary>
        public override string ToString()
        {
            return string.IsNullOrEmpty(SliceName)
                ? ElementName
                : ElementName + ElementIdGenerator.ElementIdSliceNameDelimiter + SliceName;
        }
    }

    /// <summary>For generating unique element IDs according to the standardized default format.</summary>
    public static class ElementIdGenerator
    {
        #region Public interface

        /// <summary>Delimiter inbetween segments of an element id.</summary>
        public const char ElementIdSegmentDelimiter = '.';

        /// <summary>Delimiter inbetween path segment and (optional) element slice name.</summary>
        public const char ElementIdSliceNameDelimiter = ':';

        /// <summary>
        /// Generate an element ID segment for the specified <see cref="ElementDefinition"/>
        /// by concatenating the (original) element name and the slice name (if not empty).
        /// For choice type elements, the element ID segment is always generated from the original
        /// element name (ending with "[x]") as specified by <see cref="ElementDefinition.BaseComponent.Path"/>,
        /// concatenated with the sliceName, which by convention should be initialized to the renamed element name.
        /// </summary>
        /// <returns>A string that represents an element id segment of the form "elementName[:sliceName]".</returns>
        /// <remarks>To generate the full element id, traverse the element hierarchy and concatenate the associated element id segments.</remarks>
        /// <example>value[x]:valueString</example>
        public static string GenerateIdSegment(ElementDefinition elemDef) => ElementIdSegment.Create(elemDef).ToString();

        /// <summary>Generate a segment of a standardized element ID from the specified element path and slice name.</summary>
        /// <returns>A string that represents an standardized element id segment of the form "elementName[:sliceName]".</returns>
        public static string GenerateIdSegment(string elementPath, string sliceName)
        {
            var elementName = ProfileNavigationExtensions.GetNameFromPath(elementPath);
            return ElementIdSegment.Create(elementName, sliceName).ToString();
        }

        /// <summary>Parse a standardized element id into an array of segments of the form "elementName[:sliceName]".</summary>
        /// <param name="elementId">An element id.</param>
        /// <returns>An array of element id segments of the form "elementName[:sliceName]".</returns>
        public static string[] ParseId(string elementId)
        {
            if (elementId == null) { throw Error.ArgumentNull(nameof(elementId)); }
            return elementId.Split(ElementIdSegmentDelimiter);
        }

        /// <summary>Generate a standard element ID for the specified <see cref="ElementDefinition"/> instance.</summary>
        /// <param name="elemDef">An <see cref="ElementDefinition"/> instance.</param>
        /// <param name="parentElementId">The (generated) element id of the associated parent element, or <c>null</c>.</param>
        /// <returns>A string that represents the full element ID.</returns>
        public static string GenerateId(ElementDefinition elemDef, string parentElementId)
        {
            // GenerateIdSegment method will verify the elemDef argument
            var idSegment = GenerateIdSegment(elemDef);

            return !string.IsNullOrEmpty(parentElementId)
                ? parentElementId + ElementIdSegmentDelimiter + idSegment
                : idSegment;
        }

        /// <summary>
        /// Generate a standard element ID for the current element of the specified
        /// <see cref="ElementDefinitionNavigator"/> instance, by recursively traversing all the (grand)parent elements.
        /// </summary>
        /// <returns>A string that represents the full element ID.</returns>
        public static string GenerateId(ElementDefinitionNavigator nav)
        {
            nav.ThrowIfNullOrNotPositioned(nameof(nav));

            var bm = nav.Bookmark();
            string parentId = "";
            if (nav.MoveToParent())
            {
                if (!nav.AtRoot) { parentId = GenerateId(nav); }
                nav.ReturnToBookmark(bm);
            }
            return GenerateId(nav.Current, parentId);
        }

        /// <summary>
        /// Generate and assign standard element IDs to the current element of the specified
        /// <see cref="ElementDefinitionNavigator"/> instance and it's children, recursively.
        /// </summary>
        /// <param name="nav">An <see cref="ElementDefinitionNavigator"/> instance.</param>
        /// <param name="force">Determines wether to regenerate (<c>true</c>) or maintain (<c>false</c>) any existing element IDs.</param>
        /// <param name="onlyChildren">Determines wether to only update child element ids (<c>true</c>) or also update the id of the current element (<c>false</c>).</param>
        public static void Update(ElementDefinitionNavigator nav, bool force = false, bool onlyChildren = false)
        {
            var bm = nav.Bookmark();

            var parentId = "";
            if (onlyChildren)
            {
                // Note: cannot rely on nav.Current.ElementId as it may represent a custom id value
                // Instead, always recalculate full parent id
                if (!nav.AtRoot)
                {
                    parentId = GenerateId(nav);
                }
                if (nav.MoveToFirstChild())
                {
                    generate(nav, parentId, force);
                    nav.ReturnToBookmark(bm);
                }
            }
            else
            {
                if (nav.MoveToParent())
                {
                    // Parent may have a custom element Id value
                    // => must always (re-)generate full id by traversing parent hierarchy
                    // Possible optimization: caller could specify parent id
                    if (!nav.AtRoot)
                    {
                        parentId = GenerateId(nav);
                    }
                    nav.ReturnToBookmark(bm);
                }
                generate(nav, parentId, force);
                nav.ReturnToBookmark(bm);
            }
        }

        /// <summary>
        /// Generate and assign unique element IDs in standardized default format
        /// for all the <see cref="ElementDefinition"/> items in the snapshot
        /// component of the specified <see cref="StructureDefinition"/> instance.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        /// <param name="force">Determines wether to regenerate (<c>true</c>) or maintain (<c>false</c>) any existing element IDs.</param>
        public static void Update(StructureDefinition structure, bool force = false)
        {
            structure.ThrowIfNullOrNoSnapshot(nameof(structure));

            Update(structure.Snapshot.Element, force);
        }

        /// <summary>
        /// Generate and assign unique element IDs in standardized default format
        /// for a list of <see cref="ElementDefinition"/> instances.
        /// </summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/>s.</param>
        /// <param name="force">Determines wether to regenerate (<c>true</c>) or maintain (<c>false</c>) any existing element IDs.</param>
        public static void Update(IList<ElementDefinition> elements, bool force = false)
        {
            if (elements == null) { throw new ArgumentNullException(nameof(elements)); }

            var nav = new ElementDefinitionNavigator(elements);
            if (nav.MoveToFirstChild())
            {
                generate(nav, "", force);
            }
        }

        /// <summary>Clear all the existing element IDs in the snapshot component.</summary>
        public static void Clear(StructureDefinition structure)
        {
            structure.ThrowIfNullOrNoSnapshot(nameof(structure));

            Clear(structure.Snapshot.Element);
        }

        /// <summary>Clear all the existing element IDs.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        public static void Clear(IList<ElementDefinition> elements)
        {
            if (elements == null) { throw new ArgumentNullException(nameof(elements)); }

            foreach (var elem in elements)
            {
                elem.ElementId = null;
            }
        }

        /// <summary>Clear the element IDs of the current element and it's children, recursively.</summary>
        /// <param name="nav">An <see cref="ElementDefinitionNavigator"/> instance that is positioned on a specific element.</param>
        /// <param name="onlyChildren">Determines wether to only clear child element ids (<c>true</c>) or also clear the id of the current element (<c>false</c>).</param>
        public static void Clear(ElementDefinitionNavigator nav, bool onlyChildren)
        {
            nav.ThrowIfNullOrNotPositioned(nameof(nav));
            if (onlyChildren)
            {
                clearChildren(nav);
            }
            else
            {
                clear(nav);
            }
        }

        static void clear(ElementDefinitionNavigator nav)
        {
            nav.Current.ElementId = null;
            clearChildren(nav);
        }

        static void clearChildren(ElementDefinitionNavigator nav)
        {
            var bm = nav.Bookmark();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    clear(nav);
                } while (nav.MoveToNext());
                nav.ReturnToBookmark(bm);
            }
        }

        #endregion

        #region Private interface

        static void generate(ElementDefinitionNavigator nav, string parentElemId, bool force)
        {
            nav.ThrowIfNullOrNotPositioned(nameof(nav));

            do
            {
                var id = generate(nav.Current, parentElemId, force);
                var bm = nav.Bookmark();
                if (nav.MoveToFirstChild())
                {
                    // Recurse child elements
                    generate(nav, id, force);
                    nav.ReturnToBookmark(bm);
                }
            } while (nav.MoveToNext());
        }

        static string generate(ElementDefinition elemDef, string parentElemId, bool force)
        {
            elemDef.ThrowIfNullOrEmptyPath(nameof(elemDef));

            var id = GenerateId(elemDef, parentElemId);

            // Don't replace existing IDs, unless force = true
            if (force || elemDef.ElementId == null)
            {
                // Debug.WriteLine($"[{nameof(ElementIdGenerator)}.{nameof(generate)}] Path: {elemDef.Path}:{elemDef.SliceName} Id: {elemDef.ElementId} => {id}");
                elemDef.ElementId = id;
            }

            // Always return the generated ID value, as prefix for child element IDs
            return id;
        }

        #endregion

    }
}
