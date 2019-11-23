using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class FastSerializerTests
    {
        [TestMethod]
        public void CompletePatientJson()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "patient.json"), Encoding.UTF8);

            var jsonParser = new FhirJsonParser(Model.Version.DSTU2);
            var patient = jsonParser.Parse<Model.DSTU2.Patient>(json);

            var serializedJson = FastSerializeToJsonString(patient);
            WriteFilesIfDifferent(json, serializedJson, "patient");
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void CompletePatientXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "patient.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var patient = xmlParser.Parse<Model.DSTU2.Patient>(xml);

            var serializedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + FastSerializeToXmlString(patient);
            WriteFilesIfDifferent(xml, serializedXml, "patient");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void CompleteBundleXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "bundle.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var bundle = xmlParser.Parse<Model.DSTU2.Bundle>(xml);

            var serializedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + FastSerializeToXmlString(bundle);
            WriteFilesIfDifferent(xml, serializedXml, "bundle");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void CompleteBundleJson()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "bundle.xml"), Encoding.UTF8);

            var xmlParser = new FhirXmlParser(Model.Version.DSTU2);
            var bundle = xmlParser.Parse<Model.DSTU2.Bundle>(xml);

            var json = SerializeToJsonString(bundle);
            var serializedJson = FastSerializeToJsonString(bundle);
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
            var serializedJson = FastSerializeToJsonString(bundle, summary: Rest.SummaryType.True);
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
            var serializedJson = FastSerializeToJsonString(patient);
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

            var serializedXml = FastSerializeToXmlString(patient);
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

            var serializedXml = FastSerializeToXmlString(patient);
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

            var serializedXml = FastSerializeToXmlString(patient);
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

            var serializedXml = FastSerializeToXmlString(patient);
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

            var serializedXml = FastSerializeToXmlString(patient);
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
      <td><![CDATA[text with tags:<>]]></td>
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
          <td><![CDATA[text with tags:<>]]></td>
        </tr>
      </table>
    </div>
  </text>
</Patient>";

            var serializedXml = FastSerializeToXmlString(patient);
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
            var exception = Assert.ThrowsException<XmlException>(() => FastSerializeToXmlString(patient));
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

            var serializedXml = FastSerializeToXmlString(patient);
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

            var serializedXml = FastSerializeToXmlString(patient);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void SummaryXml()
        {
            var observation = new Model.R4.Observation
            {
                Id = "obs-1001",
                ImplicitRules = "https://who-knows.org",
                Language = "en-US",
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Additional,
                    Div = "<div>Some text</div>"
                },
                Status = Model.R4.ObservationStatus.Final,
                Category = new List<Model.CodeableConcept>
                {
                    new Model.CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category","laboratory")
                },
                Code = new Model.CodeableConcept("http://loinc.org", "11502-9", "lab value"),
                Subject = new Model.ResourceReference("Patient/001")
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <id value=""obs-1001"" />
  <meta>
    <tag>
      <system value=""http://hl7.org/fhir/v3/ObservationValue"" />
      <code value=""SUBSETTED"" />
    </tag>
  </meta>
  <implicitRules value=""https://who-knows.org"" />
  <status value=""final"" />
  <code>
    <coding>
      <system value=""http://loinc.org"" />
      <code value=""11502-9"" />
    </coding>
    <text value=""lab value"" />
  </code>
  <subject>
    <reference value=""Patient/001"" />
  </subject>
</Observation>";

            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4, Rest.SummaryType.True);
            WriteFilesIfDifferent(xml, serializedXml, "summaryxml");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void OnlyDataXml()
        {
            var observation = new Model.R4.Observation
            {
                Id = "obs-1001",
                ImplicitRules = "https://who-knows.org",
                Language = "en-US",
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Additional,
                    Div = "<div>Some text</div>"
                },
                Status = Model.R4.ObservationStatus.Final,
                Category = new List<Model.CodeableConcept>
                {
                    new Model.CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category","laboratory")
                },
                Code = new Model.CodeableConcept("http://loinc.org", "11502-9", "lab value"),
                Subject = new Model.ResourceReference("Patient/001")
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <id value=""obs-1001"" />
  <meta>
    <tag>
      <system value=""http://hl7.org/fhir/v3/ObservationValue"" />
      <code value=""SUBSETTED"" />
    </tag>
  </meta>
  <implicitRules value=""https://who-knows.org"" />
  <language value=""en-US"" />
  <status value=""final"" />
  <category>
    <coding>
      <system value=""http://terminology.hl7.org/CodeSystem/observation-category"" />
      <code value=""laboratory"" />
    </coding>
  </category>
  <code>
    <coding>
      <system value=""http://loinc.org"" />
      <code value=""11502-9"" />
    </coding>
    <text value=""lab value"" />
  </code>
  <subject>
    <reference value=""Patient/001"" />
  </subject>
</Observation>";

            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4, Rest.SummaryType.Data);
            WriteFilesIfDifferent(xml, serializedXml, "onlydataxml");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void OnlyTextXml()
        {
            var observation = new Model.R4.Observation
            {
                Id = "obs-1001",
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Additional,
                    Div = "<div>Some text</div>"
                },
                Status = Model.R4.ObservationStatus.Final,
                Category = new List<Model.CodeableConcept>
                {
                    new Model.CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category","laboratory")
                },
                Code = new Model.CodeableConcept("http://loinc.org", "11502-9", "lab value"),
                Subject = new Model.ResourceReference("Patient/001")
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <id value=""obs-1001"" />
  <meta>
    <tag>
      <system value=""http://hl7.org/fhir/v3/ObservationValue"" />
      <code value=""SUBSETTED"" />
    </tag>
  </meta>
  <text>
    <status value=""additional"" />
    <div xmlns=""http://www.w3.org/1999/xhtml"">Some text</div>
  </text>
  <status value=""final"" />
  <code>
    <coding>
      <system value=""http://loinc.org"" />
      <code value=""11502-9"" />
    </coding>
    <text value=""lab value"" />
  </code>
</Observation>";

            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4, Rest.SummaryType.Text);
            WriteFilesIfDifferent(xml, serializedXml, "onlytextxml");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void OnlyElementsXml()
        {
            var observation = new Model.R4.Observation
            {
                Id = "obs-1001",
                Text = new Model.Narrative
                {
                    Status = Model.Narrative.NarrativeStatus.Additional,
                    Div = "<div>Some text</div>"
                },
                Status = Model.R4.ObservationStatus.Final,
                Category = new List<Model.CodeableConcept>
                {
                    new Model.CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category","laboratory")
                },
                Code = new Model.CodeableConcept("http://loinc.org", "11502-9", "lab value"),
                Subject = new Model.ResourceReference("Patient/001")
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <subject>
    <reference value=""Patient/001"" />
  </subject>
</Observation>";

            var serializedXml = SerializeToXmlString(observation, version: Model.Version.R4, elements: new[] { "subject" });
            WriteFilesIfDifferent(xml, serializedXml, "onlyelementsxml");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void SummaryAddSubsettedExistingMeta()
        {
            var patient = new Model.DSTU2.Patient
            {
                Meta = new Model.Meta
                {
                    Tag = new List<Model.Coding>
                    {
                        new Model.Coding("http://mysite.com/tags", "MyTag")
                    }

                }
            };

            var json = @"{
  ""resourceType"": ""Patient"",
  ""meta"": {
    ""tag"": [
      {
        ""system"": ""http://mysite.com/tags"",
        ""code"": ""MyTag""
      },
      {
        ""system"": ""http://hl7.org/fhir/v3/ObservationValue"",
        ""code"": ""SUBSETTED""
      }
    ]
  }
}";

            var serializedJson = FastSerializeToJsonString(patient, summary: Rest.SummaryType.Text);
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void SummaryDoNotAddSubsettedTwice()
        {
            var patient = new Model.DSTU2.Patient
            {
                Meta = new Model.Meta
                {
                    Tag = new List<Model.Coding>
                    {
                        new Model.Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED")
                    }

                }
            };

            var json = @"{
  ""resourceType"": ""Patient"",
  ""meta"": {
    ""tag"": [
      {
        ""system"": ""http://hl7.org/fhir/v3/ObservationValue"",
        ""code"": ""SUBSETTED""
      }
    ]
  }
}";

            var serializedJson = FastSerializeToJsonString(patient, summary: Rest.SummaryType.Text);
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void SummaryDoNotAddSubsettedRootBundle()
        {
            var bundle = new Model.DSTU2.Bundle
            {
                Entry = new List<Model.DSTU2.Bundle.EntryComponent>
                {
                    new Model.DSTU2.Bundle.EntryComponent
                    {
                        Resource = new Model.DSTU2.Patient()
                    },
                    new Model.DSTU2.Bundle.EntryComponent
                    {
                        Resource = new Model.DSTU2.Bundle()
                    }
                }
            };

            var json = @"{
  ""resourceType"": ""Bundle"",
  ""entry"": [
    {
      ""resource"": {
        ""resourceType"": ""Patient"",
        ""meta"": {
          ""tag"": [
            {
              ""system"": ""http://hl7.org/fhir/v3/ObservationValue"",
              ""code"": ""SUBSETTED""
            }
          ]
        }
      }
    },
    {
      ""resource"": {
        ""resourceType"": ""Bundle"",
        ""meta"": {
          ""tag"": [
            {
              ""system"": ""http://hl7.org/fhir/v3/ObservationValue"",
              ""code"": ""SUBSETTED""
            }
          ]
        }
      }
    }
  ]
}";

            var serializedJson = FastSerializeToJsonString(bundle, summary: Rest.SummaryType.True);
            WriteFilesIfDifferent(json, serializedJson, "summarybundles");
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void XmlDataType()
        {
            var codeableConcept = new Model.CodeableConcept("http://loinc.org", "11050-2", "Lab result");

            var xml = @"<CodeableConcept xmlns=""http://hl7.org/fhir"">
  <coding>
    <system value=""http://loinc.org"" />
    <code value=""11050-2"" />
  </coding>
  <text value=""Lab result"" />
</CodeableConcept>";

            var serializedXml = FastSerializeToXmlString(codeableConcept);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void JsonDataType()
        {
            var codeableConcept = new Model.CodeableConcept("http://loinc.org", "11050-2", "Lab result");

            var xml = @"{
  ""coding"": [
    {
      ""system"": ""http://loinc.org"",
      ""code"": ""11050-2""
    }
  ],
  ""text"": ""Lab result""
}";

            var serializedXml = FastSerializeToJsonString(codeableConcept);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XmlEmptyResource()
        {
            var patient = new Model.DSTU2.Patient();

            var xml = @"<Patient xmlns=""http://hl7.org/fhir"" />";

            var serializedXml = FastSerializeToXmlString(patient);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void JsonEmptyResource()
        {
            var patient = new Model.DSTU2.Patient();

            var json = @"{
  ""resourceType"": ""Patient""
}";

            var serializedJson = FastSerializeToJsonString(patient);
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void XmlDifferentRoot()
        {
            var patient = new Model.DSTU2.Patient();

            var xml = @"<Resource xmlns=""http://hl7.org/fhir"" />";

            var serializedXml = FastSerializeToXmlString(patient, Model.Version.DSTU2, Rest.SummaryType.False, "Resource");
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XmlBytes()
        {
            var codeableConcept = new Model.CodeableConcept("http://loinc.org", "11050-2", "Lab result");
            var expectedBytes = new FhirXmlSerializer(Model.Version.STU3).SerializeToBytes(codeableConcept);
            var actualBytes = new FhirXmlFastSerializer(Model.Version.STU3).SerializeToBytes(codeableConcept);
            Assert.AreEqual(Encoding.UTF8.GetString(expectedBytes), Encoding.UTF8.GetString(actualBytes));
        }

        [TestMethod]
        public void JsonBytes()
        {
            var codeableConcept = new Model.CodeableConcept("http://loinc.org", "11050-2", "Lab result");
            var expectedBytes = new FhirJsonSerializer(Model.Version.STU3).SerializeToBytes(codeableConcept);
            var actualBytes = new FhirJsonFastSerializer(Model.Version.STU3).SerializeToBytes(codeableConcept);
            Assert.AreEqual(Encoding.UTF8.GetString(expectedBytes), Encoding.UTF8.GetString(actualBytes));
        }

        [TestMethod]
        public void VersionSpecific()
        {
            var reference = new Model.ResourceReference
            {
                Reference = "Patient/1",
                Display = "John Smith",
                Type = "Patient",
                Identifier = new Model.Identifier("http://myserver.com/identifier/MRN", "P-1001")
            };

            var dstu2Json = @"{
  ""reference"": ""Patient/1"",
  ""display"": ""John Smith""
}";
            var stu3Json = @"{
  ""reference"": ""Patient/1"",
  ""display"": ""John Smith"",
  ""identifier"": {
    ""system"": ""http://myserver.com/identifier/MRN"",
    ""value"": ""P-1001""
  }
}";
            var r4Json = @"{
  ""reference"": ""Patient/1"",
  ""display"": ""John Smith"",
  ""type"": ""Patient"",
  ""identifier"": {
    ""system"": ""http://myserver.com/identifier/MRN"",
    ""value"": ""P-1001""
  }
}";
            var json = FastSerializeToJsonString(reference, Model.Version.DSTU2);
            Assert.AreEqual(dstu2Json, json);
            json = FastSerializeToJsonString(reference, Model.Version.STU3);
            Assert.AreEqual(stu3Json, json);
            json = FastSerializeToJsonString(reference, Model.Version.R4);
            Assert.AreEqual(r4Json, json);
        }

        [TestMethod]
        public void JsonInstant()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.Instant(new DateTimeOffset(2019, 11, 21, 13, 45, 6, 567, TimeSpan.Zero))
            };

            var json = @"{
  ""resourceType"": ""Observation"",
  ""valueInstant"": ""2019-11-21T13:45:06.567+00:00""
}";
            var serializedJson = FastSerializeToJsonString(observation, Model.Version.R4);
            Assert.AreEqual(json, serializedJson);

            observation = new Model.R4.Observation
            {
                Value = new Model.Instant(new DateTimeOffset(2019, 11, 21, 13, 45, 6, new TimeSpan(-4, 0, 0)))
            };

            json = @"{
  ""resourceType"": ""Observation"",
  ""valueInstant"": ""2019-11-21T13:45:06-04:00""
}";
            serializedJson = FastSerializeToJsonString(observation, Model.Version.R4);
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void JsonDecimal()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.Quantity { Value = 3.1315926M }
            };

            var json = @"{
  ""resourceType"": ""Observation"",
  ""valueQuantity"": {
    ""value"": 3.1315926
  }
}";
            var serializedJson = FastSerializeToJsonString(observation, Model.Version.R4);
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void JsonBoolean()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.FhirBoolean(true)
            };

            var json = @"{
  ""resourceType"": ""Observation"",
  ""valueBoolean"": true
}";
            var serializedJson = FastSerializeToJsonString(observation, Model.Version.R4);
            Assert.AreEqual(json, serializedJson);
        }

        [TestMethod]
        public void XmlInstant()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.Instant(new DateTimeOffset(2019, 11, 21, 13, 45, 6, 567, TimeSpan.Zero))
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <valueInstant value=""2019-11-21T13:45:06.567Z"" />
</Observation>";
            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4);
            Assert.AreEqual(xml, serializedXml);

            observation = new Model.R4.Observation
            {
                Value = new Model.Instant(new DateTimeOffset(2019, 11, 21, 13, 45, 6, new TimeSpan(-4, 0, 0)))
            };

            xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <valueInstant value=""2019-11-21T13:45:06-04:00"" />
</Observation>";
            serializedXml = FastSerializeToXmlString(observation, Model.Version.R4);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XmlDecimal()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.Quantity { Value = 3.1315926M }
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <valueQuantity>
    <value value=""3.1315926"" />
  </valueQuantity>
</Observation>";
            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XmlBoolean()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.FhirBoolean(true)
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <valueBoolean value=""true"" />
</Observation>";
            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void XmlIdAndExtension()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.FhirBoolean(true)
                {
                    ElementId = "value1",
                    Extension = new List<Model.Extension>
                    {
                        new Model.Extension("http://myserver.com/myext", new Model.FhirString("theValue")),
                        new Model.Extension
                        {
                            Url = "http://myserver.com/otherext",
                            Extension = new List<Model.Extension>
                            {
                                new Model.Extension("child", new Model.FhirString("otherValue"))
                            }
                        }

                    }
                }
            };

            var xml = @"<Observation xmlns=""http://hl7.org/fhir"">
  <valueBoolean value=""true"" id=""value1"">
    <extension url=""http://myserver.com/myext"">
      <valueString value=""theValue"" />
    </extension>
    <extension url=""http://myserver.com/otherext"">
      <extension url=""child"">
        <valueString value=""otherValue"" />
      </extension>
    </extension>
  </valueBoolean>
</Observation>";
            var serializedXml = FastSerializeToXmlString(observation, Model.Version.R4);
            Assert.AreEqual(xml, serializedXml);
        }

        [TestMethod]
        public void JsonIdAndExtension()
        {
            var observation = new Model.R4.Observation
            {
                Value = new Model.FhirBoolean(true)
                {
                    ElementId = "value1",
                    Extension = new List<Model.Extension>
                    {
                        new Model.Extension("http://myserver.com/myext", new Model.FhirString("theValue")),
                        new Model.Extension
                        {
                            Url = "http://myserver.com/otherext",
                            Extension = new List<Model.Extension>
                            {
                                new Model.Extension("child", new Model.FhirString("otherValue"))
                            }
                        }

                    }
                }
            };

            var json = @"{
  ""resourceType"": ""Observation"",
  ""valueBoolean"": true,
  ""_valueBoolean"": {
    ""id"": ""value1"",
    ""extension"": [
      {
        ""url"": ""http://myserver.com/myext"",
        ""valueString"": ""theValue""
      },
      {
        ""url"": ""http://myserver.com/otherext"",
        ""extension"": [
          {
            ""url"": ""child"",
            ""valueString"": ""otherValue""
          }
        ]
      }
    ]
  }
}";
            var serializedJson = FastSerializeToJsonString(observation, Model.Version.R4);
            WriteFilesIfDifferent(json, serializedJson, "idandextension");
            Assert.AreEqual(json, serializedJson);
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

        private static string FastSerializeToJsonString(Model.Base @base, Model.Version version = Model.Version.DSTU2, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            var serializer = new FhirJsonFastSerializer(new SerializerSettings(version) { Pretty = true });
            return serializer.SerializeToString(@base, summary, elements);
        }

        private static string FastSerializeToXmlString(Model.Base @base, Model.Version version = Model.Version.DSTU2, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null)
        {
            var serializer = new FhirXmlFastSerializer(new SerializerSettings(version) { Pretty = true });
            return serializer.SerializeToString(@base, summary, root, elements);
        }

        private static string SerializeToJsonString(Model.Base @base, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            var serializer = new FhirJsonSerializer(new SerializerSettings(Model.Version.DSTU2) { Pretty = true });
            return serializer.SerializeToString(@base, summary, elements);
        }

        private static string SerializeToXmlString(Model.Base @base, Model.Version version = Model.Version.DSTU2, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null)
        {
            var serializer = new FhirXmlSerializer(new SerializerSettings(version) { Pretty = true });
            return serializer.SerializeToString(@base, summary, root, elements);
        }
    }
}
