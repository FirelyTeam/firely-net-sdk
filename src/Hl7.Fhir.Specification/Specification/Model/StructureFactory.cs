/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.XPath;


namespace Hl7.Fhir.Specification.Model
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

        public static Structure Primitive(string name, Func<string,bool> validator, string nsprefix = FhirNamespaceManager.Fhir)
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

        public static List<Structure> PrimitiveTypes()
        {
//            Hl7.Fhir.Validation.DatePatternAttribute.IsValidValue()

            List<Structure> list = new List<Structure>
            { 
                Primitive("instant", null),
                Primitive("date", null),
                Primitive("dateTime", Hl7.Fhir.Model.FhirDateTime.IsValidValue),
                Primitive("decimal", Hl7.Fhir.Model.FhirDecimal.IsValidValue),
                //Primitive("element", ".*"),
                Primitive("boolean", Hl7.Fhir.Model.FhirBoolean.IsValidValue),
                Primitive("integer", Hl7.Fhir.Model.Integer.IsValidValue),
                Primitive("string", Hl7.Fhir.Model.FhirString.IsValidValue),
                Primitive("uri", Hl7.Fhir.Model.FhirUri.IsValidValue),
                Primitive("base64Binary", Hl7.Fhir.Model.Base64Binary.IsValidValue),
                Primitive("code", Hl7.Fhir.Model.Code.IsValidValue),
                Primitive("id", Hl7.Fhir.Model.Id.IsValidValue),
                Primitive("oid", Hl7.Fhir.Model.Oid.IsValidValue),
                Primitive("uuid" , Hl7.Fhir.Model.Uuid.IsValidValue)
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
