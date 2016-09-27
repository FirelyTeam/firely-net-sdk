using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160917] NEW
    // Methods to generate ElementId values

    public partial class SnapshotGenerator
    {
        public void GenerateSnapshotElementsId(StructureDefinition structureDef, bool force = false)
        {
            if (structureDef == null) { throw Error.ArgumentNull("structureDef"); }
            if (!structureDef.HasSnapshot) { throw Error.Argument("structureDef", "StructureDefinition.Snapshot component is null or empty."); }
            clearIssues();
            generateSnapshotElementsId(structureDef, force);
        }

        void generateSnapshotElementsId(StructureDefinition structureDef, bool force = false)
        {
            generateElementsId(structureDef.Snapshot.Element, force);
        }

        void generateElementsId(IList<ElementDefinition> elements, bool force = false)
        {
            var nav = new ElementDefinitionNavigator(elements);
            generateChildElementsId(nav, force);
        }

        // (Re-)generate ElementId values for all children of the current element
        void generateChildElementsId(ElementDefinitionNavigator nav, bool force = false)
        {
            var parent = nav.Current;
            Debug.Print("generateChildElementsId: '{0}'", parent != null ? parent.Path : "[root]");
            var bm = nav.Bookmark();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    var elem = nav.Current;
                    if (force | string.IsNullOrEmpty(elem.ElementId))
                    {
                        elem.ElementId = generateElementId(elem, parent);
                    }
                    generateChildElementsId(nav, force);
                } while (nav.MoveToNext());
                nav.ReturnToBookmark(bm);
            }
        }

        // Generate an ElementId for the specified element and parent element
        string generateElementId(ElementDefinition element, ElementDefinition parent)
        {
            if (element == null) { throw new ArgumentNullException("element"); }
            // parent is null for the resource root element
            var id = parent != null ? parent.ElementId : null;
            // Add element name (last path component)
            var pathPart = ElementDefinitionNavigator.GetLastPathComponent(element.Path);
            id = addIdComponent(id, ".", pathPart);
            // Add element slice name (user defined), if present
            if (!string.IsNullOrEmpty(element.Name))
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

        string addIdComponent(string id, string separator, string component)
        {
            return string.IsNullOrEmpty(id) ? component : id + separator + component;
        }
    }
}
