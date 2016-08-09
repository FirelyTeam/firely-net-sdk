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
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

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
            TypeRef typeref = new TypeRef(Fhir.Model.FHIRDefinedType.Extension);
            UriHelper.SetTypeRefIdentification(structure, typeref);
            element.TypeRefs.Add(typeref);
            structure.Elements.Add(element);
        }

        public static Structure Primitive(Fhir.Model.FHIRDefinedType name, Func<string,bool> validator, string nsprefix = FhirNamespaceManager.Fhir)
        {
            Structure structure = new Structure();
            structure.ConstrainedType = name;
            UriHelper.SetStructureIdentification(structure, UriHelper.BASEPROFILE);

            Element element = new Element();
            element.Path = new Path(name.GetLiteral());
            element.Name = name.GetLiteral();
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
            Structure structure = Primitive(Fhir.Model.FHIRDefinedType.Xhtml, null, FhirNamespaceManager.XHtml);
            structure.NameSpacePrefix = FhirNamespaceManager.XHtml;
            return structure;
        }

        public static List<Structure> PrimitiveProfileFor(IEnumerable<FHIRDefinedType> list)
        {
            List<Structure> structures = new List<Structure>();
            foreach (var s in list)
            {
                structures.Add(Primitive(s, null));
            }

            return structures;
        }

        public static List<Structure> MetaTypes()
        {
            FHIRDefinedType[] list = { FHIRDefinedType.Extension, FHIRDefinedType.Resource }; //   "Structure" stond daar ook tussen
            // NB. Narrative bevat <status> en <div>, en <div> mag html bevatten. 
            // Dit blijkt niet uit profiles.
            return PrimitiveProfileFor(list);
        }

        public static List<Structure> PrimitiveTypes()
        {
//            Hl7.Fhir.Validation.DatePatternAttribute.IsValidValue()

            List<Structure> list = new List<Structure>
            { 
                Primitive(FHIRDefinedType.Instant, null),
                Primitive(FHIRDefinedType.Date, null),
                Primitive(FHIRDefinedType.DateTime, Hl7.Fhir.Model.FhirDateTime.IsValidValue),
                Primitive(FHIRDefinedType.Decimal, Hl7.Fhir.Model.FhirDecimal.IsValidValue),
                //Primitive("element", ".*"),
                Primitive(FHIRDefinedType.Boolean, Hl7.Fhir.Model.FhirBoolean.IsValidValue),
                Primitive(FHIRDefinedType.Integer, Hl7.Fhir.Model.Integer.IsValidValue),
                Primitive(FHIRDefinedType.String, Hl7.Fhir.Model.FhirString.IsValidValue),
                Primitive(FHIRDefinedType.Uri, Hl7.Fhir.Model.FhirUri.IsValidValue),
                Primitive(FHIRDefinedType.Base64Binary, Hl7.Fhir.Model.Base64Binary.IsValidValue),
                Primitive(FHIRDefinedType.Code, Hl7.Fhir.Model.Code.IsValidValue),
                Primitive(FHIRDefinedType.Id, Hl7.Fhir.Model.Id.IsValidValue),
                Primitive(FHIRDefinedType.Oid, Hl7.Fhir.Model.Oid.IsValidValue),
                Primitive(FHIRDefinedType.Uuid , Hl7.Fhir.Model.Uuid.IsValidValue)
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
