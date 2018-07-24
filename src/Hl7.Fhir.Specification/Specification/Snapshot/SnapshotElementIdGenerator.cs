/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160917] STU3: (re-)generate ElementId values
    // Syntax: [path[:name][.path[:name]...]]
    // - Element.ElementId has the exact same number of components as Element.Path
    // - Components are separated by the dot character "."
    // - First part of each component equals the local path component, i.e. the FHIR element name
    // - Second optional part of each component equals the slice name, if not empty, separated by a semi-colon ":"
    // Notes:
    // - slice name may not contain dot characters "." !
    // - for re-slicing, the slice name can be of the form "OrignalSlice/Reslice"
    // - DSTU2: id has regex  [A-Za-z0-9\-\.]{1,64} => cannot use proposed id scheme

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
            if (parent != null && !string.IsNullOrEmpty(element.Name))
            {
                // Chris Grenz generates compound names, e.g. [slice-name].[child-element-name]
                // Only use the last part, and only if it differs from the xml element name
                var localName = ElementDefinitionNavigator.GetLastPathComponent(element.Name);
                if (localName != pathPart)
                {
                    id = addIdComponent(id, ":", localName);
                }
            }
            return id;
        }

        static string addIdComponent(string id, string separator, string component) => string.IsNullOrEmpty(id) ? component : id + separator + component;

    }
}
