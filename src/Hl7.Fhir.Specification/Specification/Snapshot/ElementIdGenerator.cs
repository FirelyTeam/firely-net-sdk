/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160917] STU3: (re-)generate ElementId values
    // http://hl7.org/fhir/STU3/elementdefinition.html#id

    // Syntax: [path[:name][.path[:name]...]]
    // - Element.ElementId has the exact same number of components as Element.Path
    // - Components are separated by the dot character "."
    // - First part of each component equals the local path component, i.e. the FHIR element name
    // - Second optional part of each component equals the slice name, if not empty, separated by a semi-colon ":"
    // Notes:
    // - slice name may not contain dot characters "." !
    // - for re-slicing, the slice name can be of the form "OrignalSlice/Reslice"
    // - DSTU2: id has regex  [A-Za-z0-9\-\.]{1,64} => cannot use proposed id scheme

#if true
    /// <summary>For generating unique Element IDs.</summary>
    public static class ElementIdGenerator
    {
        /// <summary>Delimiter inbetween segments of an element id.</summary>
        public const string ElementIdSegmentDelimiter = ".";
        
        /// <summary>Delimiter inbetween path segment and (optional) element slice name.</summary>
        public const string ElementIdSliceNameDelimiter = ":";

        /// <summary>
        /// Generate unique element IDs for the <see cref="ElementDefinition"/>s
        /// in the <see cref="StructureDefinition.Snapshot"/> component.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        /// <param name="force">Determines wether to regenerate (<c>true</c>) or maintain (<c>false</c>) any existing element IDs.</param>
        public static void Generate(StructureDefinition structure, bool force = false)
        {
            if (structure == null) { throw new ArgumentNullException(nameof(structure)); }
            if (!structure.HasSnapshot) { throw new ArgumentException("Error! The specified structure has no snapshot.", nameof(structure)); }
            Generate(structure.Snapshot.Element, force);
        }

        /// <summary>Generate unique element IDs for all the <see cref="ElementDefinition"/>s in the specified list.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/>s.</param>
        /// <param name="force">Determines wether to regenerate (<c>true</c>) or maintain (<c>false</c>) any existing element IDs.</param>
        public static void Generate(IList<ElementDefinition> elements, bool force = false)
        {
            if (elements == null) { throw new ArgumentNullException(nameof(elements)); }
            var nav = new ElementDefinitionNavigator(elements);
            if (nav.MoveToFirstChild())
            {
                generate(nav, force, "");
            }
        }

        /// <summary>Clear all the existing element IDs in the snapshot component.</summary>
        public static void Clear(StructureDefinition structure)
        {
            if (structure == null) { throw new ArgumentNullException(nameof(structure)); }
            if (!structure.HasSnapshot) { throw new ArgumentException("Error! The specified structure has no snapshot.", nameof(structure)); }
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
        /// <param name="nav"></param>
        public static void Clear(ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw new ArgumentNullException(nameof(nav)); }
            if (nav.Current == null) { throw new ArgumentException("Error! The navigator is not positioned on an element.", nameof(nav)); }

            nav.Current.ElementId = null;
            var bm = nav.Bookmark();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    Clear(nav);
                } while (nav.MoveToNext());
                nav.ReturnToBookmark(bm);
            }
        }

        private static void generate(ElementDefinitionNavigator nav, bool force, string parentElemId)
        {
            if (nav == null) { throw new ArgumentNullException(nameof(nav)); }
            if (nav.Current == null) { throw new ArgumentException("Error! The specified navigator is not positioned on an element.", nameof(nav)); }

            do
            {
                var id = generate(nav.Current, force, parentElemId);
                var bm = nav.Bookmark();
                if (nav.MoveToFirstChild())
                {
                    // Recurse child elements
                    generate(nav, force, id);
                    nav.ReturnToBookmark(bm);
                }
            } while (nav.MoveToNext());
        }

        private static string generate(ElementDefinition elemDef, bool force, string parentElemId)
        {
            if (elemDef == null) { throw new ArgumentNullException(nameof(elemDef)); }
            if (elemDef.Path == null) { throw new ArgumentException("Error! The specified element has no path.", nameof(elemDef)); }
            var id =
                (!string.IsNullOrEmpty(parentElemId) ? parentElemId + ElementIdSegmentDelimiter : null)
                + ElementDefinitionNavigator.GetLastPathComponent(elemDef.Path)
                + (elemDef.SliceName != null ? ElementIdSliceNameDelimiter + elemDef.SliceName : null);
            // Don't replace existing IDs, unless force = true

            // Debug.WriteLineIf(force || elemDef.ElementId == null, $"[{nameof(ElementIdGenerator)}.{nameof(generate)}] {elemDef.Path} '{elemDef.SliceName}' - ID: '{elemDef.ElementId}' => '{id}'");
            // Debug.WriteLineIf(!force && elemDef.ElementId != null, $"[{nameof(ElementIdGenerator)}.{nameof(generate)}] {elemDef.Path} '{elemDef.SliceName}' - ID: '{elemDef.ElementId}' ('{id}')");

            if (force || elemDef.ElementId == null)
            {
                elemDef.ElementId = id;
            }
            // Always return the generated ID value, as prefix for child element IDs
            return id;
        }
    }
#else
    public partial class SnapshotGenerator
    {
        public void GenerateSnapshotElementsId(StructureDefinition structureDef, bool force = false)
        {
            if (structureDef == null) { throw Error.ArgumentNull(nameof(structureDef)); }
            if (!structureDef.HasSnapshot) { throw Error.Argument(nameof(structureDef), "The StructureDefinition.Snapshot component is null or empty."); }
            clearIssues();
            generateSnapshotElementsId(structureDef, force);
        }

        static void generateSnapshotElementsId(StructureDefinition structureDef, bool force = false)
        {
            generateElementsId(structureDef.Snapshot.Element, force);
        }

        static void generateElementsId(IList<ElementDefinition> elements, bool force = false)
        {
            var nav = new ElementDefinitionNavigator(elements);
            generateChildElementsId(nav, force);
        }

        // (Re-)generate ElementId values for all children of the current element
        static void generateChildElementsId(ElementDefinitionNavigator nav, bool force = false)
        {
            var parent = nav.Current;
            // Debug.Print($"[{nameof(generateChildElementsId)}] '{(parent != null ? parent.Path : "[root]")}'");
            var bm = nav.Bookmark();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    var elem = nav.Current;
                    if (force || string.IsNullOrEmpty(elem.ElementId))
                    {
                        elem.ElementId = generateElementId(elem, parent);
                    }
                    generateChildElementsId(nav, force);
                } while (nav.MoveToNext());
                nav.ReturnToBookmark(bm);
            }
        }

        // Generate an ElementId for the specified element and parent element
        static string generateElementId(ElementDefinition element, ElementDefinition parent)
        {
            if (element == null) { throw new ArgumentNullException(nameof(element)); }
            // Add element name (last path component)
            // parent is null for the resource root element
            var pathPart = ElementDefinitionNavigator.GetLastPathComponent(element.Path);
            var id = addIdComponent(parent?.ElementId, ".", pathPart);
            // Add element slice name (user defined), if present
            // [WMR 20160922] Except for root element, e.g. SimpleQuantity
            if (parent != null && !string.IsNullOrEmpty(element.SliceName))
            {
                // Chris Grenz generates compound names, e.g. [slice-name].[child-element-name]
                // Only use the last part, and only if it differs from the xml element name
                var localName = ElementDefinitionNavigator.GetLastPathComponent(element.SliceName);
                if (localName != pathPart)
                {
                    id = addIdComponent(id, ":", localName);
                }
            }
            return id;
        }

        static string addIdComponent(string id, string separator, string component) => string.IsNullOrEmpty(id) ? component : id + separator + component;

    }
#endif
}
