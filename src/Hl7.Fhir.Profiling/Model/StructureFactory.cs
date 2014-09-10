/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Hl7.Fhir.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.XPath;


namespace Hl7.Fhir.Profiling
{
    public static class StructureFactory
    {
       
        static StructureFactory()
        {
        }

        public static void AddExtensionElement(Structure structure, Element parent = null)
        {
            parent = parent  ?? structure.Root;
            string path = string.Format("{0}.extension", parent.Path); 
            Element element = new Element();
            element.Path = new Path(path);
            element.Name = "extension";
            element.Cardinality = new Cardinality { Min = "0", Max = "*" };
            TypeRef typeref = new TypeRef("Extension");
            UriHelper.SetTypeRefIdentification(structure, typeref);
            element.TypeRefs.Add(typeref);
            structure.Elements.Add(element);
        }

        public static Structure Primitive(string name, IPrimitiveValidator validator, string nsprefix = FhirNamespaceManager.Fhir)
        {
            Structure structure = new Structure();
            structure.Type = name;
            UriHelper.SetStructureIdentification(structure, UriHelper.BASEPROFILE);

            Element element = new Element();
            element.Path = new Path(name);
            element.Name = name;
            element.IsPrimitive = true;
            element.PrimitiveValidator = validator;
            element.Cardinality = new Cardinality { Min = "1", Max = "1" };
            element.NameSpacePrefix = nsprefix;
            structure.Elements.Add(element);

            AddExtensionElement(structure, element);
            
            return structure;
        }

        public static Structure XhtmlStructure()
        {
            Structure structure = Primitive("xhtml", null, FhirNamespaceManager.XHtml);
            structure.NameSpacePrefix = FhirNamespaceManager.XHtml;
            return structure;
        }

        public static List<Structure> PrimitiveProfileFor(IEnumerable<string> list)
        {
            List<Structure> structures = new List<Structure>();
            foreach (string s in list)
            {
                structures.Add(Primitive(s, null));
            }

            return structures;
        }

        public static List<Structure> MetaTypes()
        {
            string[] list = { "Structure", "Extension", "Resource" };
            // NB. Narrative bevat <status> en <div>, en <div> mag html bevatten. 
            // Dit blijkt niet uit profiles.
            return PrimitiveProfileFor(list);
        }

        public static List<Structure> MockDataTypes()
        {
            string[] list = {
                "ratio",
                "Period",
                "range",
                "Attachment",
                "Identifier",
                "schedule",
                "HumanName",
                "Coding",
                "Address", 
                "CodeableConcept",
                "Quantity",
                "SampledData",
                "Contact",
                "Age",
                "Distance",
                "Duration",
                "Count",
                "Money"};
            return PrimitiveProfileFor(list);
        }

        public static List<Structure> PrimitiveTypes()
        {
//            Hl7.Fhir.Validation.DatePatternAttribute.IsValidValue()

            List<Structure> list = new List<Structure>
            { 
                Primitive("instant", null),
                Primitive("date", null),
                Primitive("dateTime", RegExPrimitiveValidator.For(Hl7.Fhir.Model.FhirDateTime.PATTERN)),
                Primitive("decimal", RegExPrimitiveValidator.For(@"\d+")),
                //Primitive("element", ".*"),
                Primitive("boolean", RegExPrimitiveValidator.For("(true|false)")),
                Primitive("integer", RegExPrimitiveValidator.For(@"\d+")),
                Primitive("string", null),
                Primitive("uri", new UriPrimitiveValidator()),
                Primitive("base64Binary", null),
                Primitive("code", RegExPrimitiveValidator.For(Hl7.Fhir.Model.Code.PATTERN)),
                Primitive("id", RegExPrimitiveValidator.For(@"[a-z0-9\-\.]{1,36}")),
                Primitive("oid", null),
                Primitive("uuid" , null)
            };

            return list;
        }

        public static List<Structure> NonFhirNamespaces()
        {
            List<Structure> list = new List<Structure>();
            list.Add(XhtmlStructure());
            return list;
        }
    }
}
