/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Fhir.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhir.IO;

namespace Fhir.Profiling
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
            element.TypeRefs.Add(new TypeRef("Extension"));
            structure.Elements.Add(element);
        }

        public static Structure Primitive(string name, string pattern, string nsprefix = FhirNamespaceManager.Fhir)
        {
            Structure structure = new Structure();
            structure.Type = name;

            Element element = new Element();
            element.Path = new Path(name);
            element.Name = name;
            element.IsPrimitive = true;
            element.PrimitivePattern = pattern;
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
                structures.Add(Primitive(s, ""));
            }

            return structures;
        }

        public static List<Structure> MetaTypes()
        {
            string[] list = { "Structure", "Extension", "Resource", "Narrative" };
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
                Primitive("instant", @".*"),
                Primitive("date", @".*"),
                Primitive("dateTime", Hl7.Fhir.Model.FhirDateTime.PATTERN),
                Primitive("decimal", @"\d+"),
                //Primitive("element", ".*"),
                Primitive("boolean", @"(true|false)"),
                Primitive("integer", @"\d+"),
                Primitive("string", @".*"),
                Primitive("uri", @"http"),
                Primitive("base64Binary", @".*"),
                Primitive("code", Hl7.Fhir.Model.Code.PATTERN),
                Primitive("id", @"\d+"),
                Primitive("oid", @".*"),
                Primitive("uuid" , @".*")
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
