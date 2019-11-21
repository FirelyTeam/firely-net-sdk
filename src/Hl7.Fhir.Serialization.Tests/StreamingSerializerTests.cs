using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class StreamingSerializerTests
    {
        [TestMethod]
        public void CompletePatientJson()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "patient.json"), Encoding.UTF8);

            var jsonParser = new FhirJsonParser(Model.Version.DSTU2);
            var patient = jsonParser.Parse<Model.DSTU2.Patient>(json);

            var serializedJson = StreamingSerializeToJsonString(patient);
            WriteFilesIfDifferent(json, serializedJson, "patient");
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void CompletePatientXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "patient.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var patient = xmlParser.Parse<Model.DSTU2.Patient>(xml);

            var serializedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "patient");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void CompleteBundleXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "bundle.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var bundle = xmlParser.Parse<Model.DSTU2.Bundle>(xml);

            var serializedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + StreamingSerializeToXmlString(bundle);
            WriteFilesIfDifferent(xml, serializedXml, "bundle");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void SummaryBundleXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "bundle.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var bundle = xmlParser.Parse<Model.DSTU2.Bundle>(xml);

            var serializedXml = StreamingSerializeToXmlString(bundle, Rest.SummaryType.True);
            xml = SerializeToXmlString(bundle, Rest.SummaryType.True);
            WriteFilesIfDifferent(xml, serializedXml, "bundlesummary");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void CompleteBundleJson()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "bundle.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var bundle = xmlParser.Parse<Model.DSTU2.Bundle>(xml);

            var json = SerializeToJsonString(bundle);
            var serializedJson = StreamingSerializeToJsonString(bundle);
            WriteFilesIfDifferent(json, serializedJson, "bundle");
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void SummaryBundleJson()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "bundle.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var bundle = xmlParser.Parse<Model.DSTU2.Bundle>(xml);

            var json = SerializeToJsonString(bundle, Rest.SummaryType.True);
            var serializedJson = StreamingSerializeToJsonString(bundle, Rest.SummaryType.True);
            WriteFilesIfDifferent(json, serializedJson, "bundlesummary");
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void TrimJson()
        {
            var patient = new Model.DSTU2.Patient
            {
                Name = new List<Model.DSTU2.HumanName>
                {
                    new Model.DSTU2.HumanName { Family = new[] { string.Empty, " Smith\r\n\t", "\t\r\n    " } }
                }
            };

            var json = @"{
  ""resourceType"": ""Patient"",
  ""name"": [
    {
      ""family"": [
        ""Smith""
      ]
    }
  ]
}";
            var serializedJson = StreamingSerializeToJsonString(patient);
            Assert.AreEqual(SerializeToJsonString(patient), serializedJson);
            WriteFilesIfDifferent(json, serializedJson, "trim");
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void TrimXml()
        {
            var patient = new Model.DSTU2.Patient
            {
                Name = new List<Model.DSTU2.HumanName>
                {
                    new Model.DSTU2.HumanName { Family = new[] { string.Empty, " Smith\r\n\t", "    \r\n\t" } }
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <name>
    <family value=""Smith"" />
  </name>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "trim");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlEntities()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">The&nbsp;&ndash;&nbsp;&lt;text&gt;</div>"
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
    <div xmlns=""http://www.w3.org/1999/xhtml"">The – &lt;text&gt;</div>
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "entities");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlNamespace()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = "<div>The <b>text</b></div>"
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
    <div xmlns=""http://www.w3.org/1999/xhtml"">The <b>text</b></div>
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "namespace");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlNoElement()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = "The <b>text</b>"
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
    <div xmlns=""http://www.w3.org/1999/xhtml"">The <b>text</b></div>
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "noelement");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlDifferentElement()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = "<p>The <b>text</b></p>"
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
    <div xmlns=""http://www.w3.org/1999/xhtml"">
      <p>The <b>text</b></p>
    </div>
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "element");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlComplex()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
  <h2>v3 map for AddressType (http://hl7.org/fhir/ConceptMap/v3-address-type)</h2>
  <p>Mapping from <a>http://hl7.org/fhir/ValueSet/address-type</a> to <a>http://hl7.org/fhir/ValueSet/v3-AddressUse</a></p>
  <br />
  <table>
    <tr valign=""top"">
      <td>
        <b>Source Code</b>
      </td>
      <td>
        <b>Equivalence</b>
      </td>
      <td>
        <b>Destination Code</b>
      </td>
    </tr>
    <tr valign=""top"">
      <td bgcolor=""red"" align=""center"">postal (Postal)</td>
      <td>equal</td>
      <td>PHYS (physical visit address)</td>
    </tr>
  </table>
</div>"
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
    <div xmlns=""http://www.w3.org/1999/xhtml"">
      <h2>v3 map for AddressType (http://hl7.org/fhir/ConceptMap/v3-address-type)</h2>
      <p>Mapping from <a>http://hl7.org/fhir/ValueSet/address-type</a> to <a>http://hl7.org/fhir/ValueSet/v3-AddressUse</a></p>
      <br />
      <table>
        <tr valign=""top"">
          <td>
            <b>Source Code</b>
          </td>
          <td>
            <b>Equivalence</b>
          </td>
          <td>
            <b>Destination Code</b>
          </td>
        </tr>
        <tr valign=""top"">
          <td bgcolor=""red"" align=""center"">postal (Postal)</td>
          <td>equal</td>
          <td>PHYS (physical visit address)</td>
        </tr>
      </table>
    </div>
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "entities");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlInvalid()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = "<div>The <b>text</div>"
                }
            };
            var exception = Assert.ThrowsException<XmlException>(() => StreamingSerializeToXmlString(patient));
            const string messageStart = "The 'b' start tag";
            Assert.IsTrue(exception.Message.StartsWith(messageStart), $"<{exception.Message}> does not start with <{messageStart}>");
        }

        [TestMethod]
        public void XhtmlEmpty()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = string.Empty
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "element");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XhtmlWhitespace()
        {
            var patient = new Model.DSTU2.Patient
            {
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Generated,
                    Div = "\t\r\n    "
                }
            };

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"">
  <text>
    <status value=""generated"" />
  </text>
</Patient>";

            var serializedXml = StreamingSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "element");
            Assert.AreEqual(xml, serializedXml);
        }

        private static void WriteFilesIfDifferent(string expected, string actual, string fileName)
        {
            if (expected != actual)
            {
                var extension = expected.StartsWith("<") ?
                    "xml" :
                    "json";
                var expectedFilePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(fileName + "-expected", extension));
                File.WriteAllText(expectedFilePath, expected, Encoding.UTF8);
                Console.WriteLine(expectedFilePath);

                var actualFilePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(fileName + "-actual", extension));
                File.WriteAllText(actualFilePath, actual, Encoding.UTF8);
                Console.WriteLine(actualFilePath);
            }
        }

        private static string StreamingSerializeToJsonString(Model.Base @base, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            var writer = new StringWriter();
            using (var jsonWriter = new JsonTextWriter(writer) { Formatting = Newtonsoft.Json.Formatting.Indented })
            {
                var serializer = new JsonStreamingSerializer(jsonWriter, Model.Version.DSTU2, summary, elements);
                @base.Serialize(serializer);
            }
            writer.Flush();
            return writer.ToString();
        }

        private static string StreamingSerializeToXmlString(Model.Base @base, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                NewLineHandling = NewLineHandling.Entitize,
                Indent = true,
                Encoding = Encoding.UTF8
            };
            using (var xmlWriter = XmlWriter.Create(sb, settings))
            {
                var serializer = new XmlStreamingSerializer(xmlWriter, Model.Version.DSTU2, summary, null, elements);
                
                @base.Serialize(serializer);
            }
            return sb.ToString();
        }

        private static string SerializeToJsonString(Model.Base @base, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            var settings = new SerializerSettings(Model.Version.DSTU2)
            {
                Pretty = true
            };
            var serializer = new FhirJsonSerializer(settings);
            return serializer.SerializeToString(@base, summary, elements);
        }

        private static string SerializeToXmlString(Model.Base @base, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            var settings = new SerializerSettings(Model.Version.DSTU2)
            {
                Pretty = true
            };
            var serializer = new FhirXmlSerializer(settings);
            return serializer.SerializeToString(@base, summary, null, elements);
        }
    }
}
