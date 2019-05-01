/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Validation
{

    internal static class ElementDefinitionNavigatorExtensions
    {
        public static string GetFhirPathConstraint(this ElementDefinition.ConstraintComponent cc)
        {
            // This was required for 3.0.0, but was rectified in the 3.0.1 technical update
            //if (cc.Key == "ele-1")
            //    return "(children().count() > id.count()) | hasValue()";
            return cc.Expression;
        }

        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed) =>
                   ed.Path.EndsWith(".value") && IsPrimitiveConstraint(ed);

        // EK 20190109 BUG: Our snapshot generator, when constraining .value elements, does not bring in the
        // original extensions/empty code element - so in profiles that constraint .value, this check does not
        // recognize primitive constraints anymore. The commented out code is what I would like to have when
        // this gets fixed.
#if true

        public static bool IsPrimitiveConstraint(this ElementDefinition ed)
        {
            // [WMR 20190124] #827
            // R4: ElementDefinition.type.code.value is empty for primitive core element definitions:
            // - [Primitive].value
            // - Extension.url
            var isPrimitive = ed.Type.Count == 1 && ed.Type[0].Code is null;

#if DEBUG
            // DEBUGGING

            // Assuming that ed is fully specified (originates from a snapshot component), then
            // we only expect to find empty type code in combination with special "compiler magic" extensions
            if (isPrimitive)
            {
                Debug.Assert(ed.Type[0].CodeElement.Extension.Count >= 3);
                Debug.Assert(ed.Type[0].CodeElement.GetExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-xml-type") != null);
                Debug.Assert(ed.Type[0].CodeElement.GetExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-json-type") != null);
                Debug.Assert(ed.Type[0].CodeElement.GetExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-rdf-type") != null);

                // Elements represented as XmlAttr (and Xhtml) are always primitives
                Debug.Assert(
                    ed.Representation.Contains(ElementDefinition.PropertyRepresentation.XmlAttr) ||
                    // For xhtml.value
                    ed.Representation.Contains(ElementDefinition.PropertyRepresentation.Xhtml)
                );

            }

#endif

            return isPrimitive;

            //var result = ed.Type.Any() 
            //    && ed.Type.First().CodeElement != null 
            //    && ed.Type.First().CodeElement.GetExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-xml-type") != null;

            //var result2 = ed.Representation.Any() ?
            //    (ed.Representation.Contains(ElementDefinition.PropertyRepresentation.XmlAttr) ||
            //     ed.Representation.Contains(ElementDefinition.PropertyRepresentation.Xhtml))
            //: false;

            ////System.Diagnostics.Debug.Assert(result == result2);
            //return result;
        }
#else
        public static bool IsPrimitiveConstraint(this ElementDefinition ed)
            => ed.Representation.Any() ?
                (ed.Representation.Contains(ElementDefinition.PropertyRepresentation.XmlAttr) ||
                 ed.Representation.Contains(ElementDefinition.PropertyRepresentation.Xhtml))
            : false;
            // ed.Type.Any() 
            //&& ed.Type.First().CodeElement != null 
            //&& ed.Type.First().CodeElement.GetExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-xml-type") != null;
#endif


        internal static bool IsResourcePlaceholder(this ElementDefinition ed)
        {
            if (ed.Type == null) return false;
            return ed.Type.Any(t => t.Code == "Resource" || t.Code == "DomainResource");
        }

        public static string ConstraintDescription(this ElementDefinition.ConstraintComponent cc)
        {
            var desc = cc.Key;

            if (cc.Human != null)
                desc += " \"" + cc.Human + "\"";

            return desc;
        }


        public static string QualifiedDefinitionPath(this ElementDefinitionNavigator nav)
        {
            string path = "";

            if (nav.StructureDefinition != null && nav.StructureDefinition.Url != null)
                path = "{" + nav.StructureDefinition.Url + "}";

            path += nav.Path;

            return path;
        }
    }

}
