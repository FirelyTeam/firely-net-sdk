/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.Core.Tests.Serialization
{
    [TestClass]
    public class CustomSerializationTests
    {

        //[20180730] Support for this way of customization has been removed from the API

        //private Patient createData()
        //{
        //    var result = new Patient() { Active = true, Gender = AdministrativeGender.Male };
        //    result.Name.Add(HumanName.ForFamily("Kramer").WithGiven("Ewout"));
        //    return result;
        //}


        //[TestMethod]
        //public void NoOpSerializer()
        //{
        //    var xmlSerializer = new FhirXmlSerializer(new ParserSettings() { CustomSerializer = new DoNothingCustomSerializer() });
        //    var pat = createData();

        //    // Make sure a no-op serializer does not influence serialization
        //    var xml = xmlSerializer.SerializeToString(pat);
        //    Assert.AreEqual(new FhirXmlSerializer().SerializeToString(pat), xml);

        //    var jsonSerializer = new FhirJsonSerializer(new ParserSettings() { CustomSerializer = new DoNothingCustomSerializer() });

        //    // Make sure a no-op serializer does not influence serialization
        //    var json = jsonSerializer.SerializeToString(pat);
        //    Assert.AreEqual(new FhirJsonSerializer().SerializeToString(pat), json);
        //}

        //private class DoNothingCustomSerializer : ISerializerCustomization
        //{
        //    public void OnAfterSerializeComplexType(object instance, IFhirWriter writer) { }

        //    public void OnBeforeSerializeComplexType(object instance, IFhirWriter writer) { }

        //    public bool OnBeforeSerializeProperty(string name, object value, IFhirWriter writer) => false;
        //}

        //[TestMethod]
        //public void TestBeforeAndAfterSerializeComplex()
        //{
        //    var pat = createData();

        //    var xmlSerializer = new FhirXmlSerializer(new ParserSettings() { CustomSerializer = new InsertAdditionalMembersAroundCustomSerializer() });
        //    var xml = xmlSerializer.SerializeToString(pat);
        //    Assert.AreEqual("<Patient myProp=\"true\" myProp2=\"dude\" xmlns=\"http://hl7.org/fhir\">" +
        //            "<active myProp=\"true\" myProp2=\"dude\" value=\"true\" /><name myProp=\"true\" myProp2=\"dude\">" +
        //            "<family myProp=\"true\" myProp2=\"dude\" value=\"Kramer\" /><given myProp=\"true\" myProp2=\"dude\" value=\"Ewout\" />" +
        //            "</name><gender myProp=\"true\" myProp2=\"dude\" value=\"male\" /><active2 value=\"true\" /><gender2>male</gender2></Patient>", xml);

        //    var jsonSerializer = new FhirJsonSerializer(new ParserSettings() { CustomSerializer = new InsertAdditionalMembersAroundCustomSerializer() });
        //    var json = jsonSerializer.SerializeToString(pat);
        //    Assert.AreEqual("{\"resourceType\":\"Patient\",\"myProp\":true,\"myProp2\":\"dude\",\"active\":true,\"_active\":{\"myProp\":true,\"myProp2\":\"dude\"},\"name\":[{\"myProp\":true,\"myProp2\":\"dude\",\"family\":[\"Kramer\"],\"_family\":[{\"myProp\":true,\"myProp2\":\"dude\"}],\"given\":[\"Ewout\"],\"_given\":[{\"myProp\":true,\"myProp2\":\"dude\"}]}],\"gender\":\"male\",\"_gender\":{\"myProp\":true,\"myProp2\":\"dude\"},\"active2\":{\"value\":true},\"gender2\":{\"value\":\"male\"}}", json);

        //}

        //private class InsertAdditionalMembersAroundCustomSerializer : ISerializerCustomization
        //{
        //    public void OnBeforeSerializeComplexType(object instance, IFhirWriter writer)
        //    {
        //        writer.WriteStartProperty("myProp");
        //        writer.WritePrimitiveContents(true, XmlRepresentation.XmlAttr);
        //        writer.WriteEndProperty();

        //        writer.WriteStartProperty("myProp2");
        //        writer.WritePrimitiveContents("dude", XmlRepresentation.XmlAttr);
        //        writer.WriteEndProperty();
        //    }

        //    public void OnAfterSerializeComplexType(object instance, IFhirWriter writer)
        //    {
        //        if (instance is Patient p)
        //        {
        //            writer.WriteStartProperty("active2");
        //            writer.WriteStartComplexContent();
        //            writer.WriteStartProperty("value");
        //            writer.WritePrimitiveContents(p.Active, XmlRepresentation.None);
        //            writer.WriteEndProperty();
        //            writer.WriteEndComplexContent();
        //            writer.WriteEndProperty();

        //            writer.WriteStartProperty("gender2");
        //            writer.WriteStartComplexContent();
        //            writer.WriteStartProperty("value");
        //            writer.WritePrimitiveContents(p.Gender, XmlRepresentation.XmlText);
        //            writer.WriteEndProperty();
        //            writer.WriteEndComplexContent();
        //            writer.WriteEndProperty();
        //        }
        //    }


        //    public bool OnBeforeSerializeProperty(string name, object value, IFhirWriter writer) => false;
        //}


        //[TestMethod]
        //public void TestOnSerializeProperty()
        //{
        //    var pat = createData();

        //    var xmlSerializer = new FhirXmlSerializer(new ParserSettings() { CustomSerializer = new SkipActiveCustomSerializer() });
        //    var xml = xmlSerializer.SerializeToString(pat);
        //    Assert.IsFalse(xml.Contains("<active"));
        //    Assert.IsTrue(new FhirXmlSerializer().SerializeToString(pat).Contains("<active"));

        //    var jsonSerializer = new FhirJsonSerializer(new ParserSettings() { CustomSerializer = new SkipActiveCustomSerializer() });
        //    var json = jsonSerializer.SerializeToString(pat);
        //    Assert.IsFalse(json.Contains("\"active\":"));
        //    Assert.IsTrue(new FhirJsonSerializer().SerializeToString(pat).Contains("\"active\":"));
        //}

        //private class SkipActiveCustomSerializer : ISerializerCustomization
        //{
        //    public void OnAfterSerializeComplexType(object instance, IFhirWriter writer) { }

        //    public void OnBeforeSerializeComplexType(object instance, IFhirWriter writer) { }

        //    public bool OnBeforeSerializeProperty(string name, object value, IFhirWriter writer) => name == "active";
        //}


        //[TestMethod]
        //public void TestSerializeAnnotation()
        //{
        //    var pat = createData();
        //    pat.AddAnnotation(new YadaYadaAnnotation { Num = 3 });
        //    var xmlSerializer = new FhirXmlSerializer(new ParserSettings() { CustomSerializer = new DumpAnnotationCustomSerializer() });
        //    var xml = xmlSerializer.SerializeToString(pat);

        //    Assert.IsTrue(xml.Contains("<Patient yada=\"3\""));
        //}

        //private class YadaYadaAnnotation
        //{
        //    public int Num;
        //}

        //private class DumpAnnotationCustomSerializer : ISerializerCustomization
        //{
        //    public void OnBeforeSerializeComplexType(object instance, IFhirWriter writer)
        //    {
        //        if (instance is Base b)
        //        {
        //            var ann = b.Annotation<YadaYadaAnnotation>();
        //            if (ann != null)
        //            {
        //                writer.WriteStartProperty("yada");
        //                writer.WritePrimitiveContents(PrimitiveTypeConverter.ConvertTo<string>(ann.Num), 
        //                    XmlRepresentation.XmlAttr);
        //                writer.WriteEndProperty();
        //            }
        //        }
        //    }

        //    public void OnAfterSerializeComplexType(object instance, IFhirWriter writer) { }

        //    public bool OnBeforeSerializeProperty(string name, object value, IFhirWriter writer) => false;
        //}


        //[TestMethod]
        //public void AnnotationDeserializer()
        //{
        //    var pat = createData();
        //    pat.AddAnnotation(new YadaYadaAnnotation { Num = 4 });
        //    var xmlSerializer = new FhirXmlSerializer(new ParserSettings() { CustomSerializer = new DumpAnnotationCustomSerializer() });
        //    var patXml = xmlSerializer.SerializeToString(pat);

        //    var xmlDeserializer = new FhirXmlParser(new ParserSettings() { CustomDeserializer = new RetrieveAnnotationCustomDeserializer() });
        //    var yadaPat = xmlDeserializer.Parse<Patient>(patXml);

        //    var yada = yadaPat.Annotation<YadaYadaAnnotation>();
        //    Assert.AreEqual(4, yada?.Num);
        //}

        //private class RetrieveAnnotationCustomDeserializer : IDeserializerCustomization
        //{
        //    public bool OnBeforeDeserializeProperty(string name, Base parent, IElementNavigator current)
        //    {
        //        if(name == "yada")
        //        {
        //            var num = PrimitiveTypeConverter.ConvertTo<int>(current.Value);

        //            parent.AddAnnotation(new YadaYadaAnnotation { Num = num });
        //            return true;
        //        }

        //        return false;
        //    }
        //}
    }
}