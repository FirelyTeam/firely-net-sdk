using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;
using Date = Hl7.Fhir.ElementModel.Types.Date;
using DateTime = Hl7.Fhir.ElementModel.Types.DateTime;
using ERR = Hl7.Fhir.Serialization.FhirXmlException;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class FhirXmlDeserializationTests
    {
        [DataTestMethod]
        [DataRow("<active value =\"true\"/>", typeof(FhirBoolean), true, null)]
        [DataRow("<multipleBirthInteger value =\"1\"/>", typeof(Integer), 1, null)]
        [DataRow("<Birthdate value =\"2000-01-01\"/>", typeof(FhirDateTime), "2000-01-01", null)]
        [DataRow("<given value =\" foo \"/>", typeof(FhirString), "foo", null)]
        [TestMethod]
        public void TryDeserializePrimitives(string xmlPrimitive, Type expectedFhirType, object expectedValue, string error)
        {
            var reader = constructReader(xmlPrimitive);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var datatype = deserializer.DeserializeElement(expectedFhirType, reader);

            datatype.Should().BeOfType(expectedFhirType);
            datatype.As<PrimitiveType>().ObjectValue.Should().Be(expectedValue);
        }

        [DataTestMethod]
        [DataRow("<foo value =\"true\"/>", typeof(bool), true, null, DisplayName = "XmlBool1")]
        [DataRow("<foo value =\"1\"/>", typeof(bool), "1", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlBool2")]
        [DataRow("<foo value =\"treu\"/>", typeof(bool), "treu", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlBool3")]
        [DataRow("<foo value =\"2000-01-01\"/>", typeof(DateTimeOffset), "2000-01-01", null, DisplayName = "XmlInstant1")]
        [DataRow("<foo value =\"foo\"/>", typeof(DateTimeOffset), "foo", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlInstant2")]
        [DataRow("<foo value =\"foo\"/>", typeof(byte[]), "foo", ERR.INCORRECT_BASE64_DATA_CODE, DisplayName = "XmlByteArray")]
        [DataRow("<foo value =\"1\"/>", typeof(int), 1, null, DisplayName = "XmlInteger1")]
        [DataRow("<foo value =\"1.1\"/>", typeof(int), "1.1", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlInteger2")]
        [DataRow("<foo value =\"1\"/>", typeof(long), 1, null, DisplayName = "XmlLong1")]
        [DataRow("<foo value =\"1.1\"/>", typeof(long), "1.1", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlLong2")]
        [DataRow("<foo value =\"1\"/>", typeof(uint), 1, ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlUint1")]
        [DataRow("<foo value =\"-1\"/>", typeof(uint), "-1", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlUint2")]
        [DataRow("<foo value =\"3.14\"/>", typeof(decimal), 3.14, null, DisplayName = "XmlDecimal1")]
        [DataRow("<foo value =\"3.14e2\"/>", typeof(decimal), 3.14e2, null, DisplayName = "XmlDecimal1")]
        [DataRow("<foo value =\"3.14e500\"/>", typeof(decimal), "3.14e500", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlDecimal2")]
        [DataRow("<foo value =\"3.14\"/>", typeof(double), 3.14, null, DisplayName = "XmlDouble1")]
        [DataRow("<foo value =\"1\"/>", typeof(ulong), 1, ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlUlong1")]
        [DataRow("<foo value =\"-1\"/>", typeof(ulong), "-1", ERR.VALUE_IS_NOT_OF_EXPECTED_TYPE_CODE, DisplayName = "XmlUlong2")]
        [DataRow("<foo value =\"1\"/>", typeof(float), 1, null, DisplayName = "XmlFloat1")]
        public void TryDeserializePrimitiveValue(string xmlPrimitive, Type implementingType, object expectedValue, string expectedErrorCode)
        {
            var reader = constructReader(xmlPrimitive);
            reader.MoveToContent();
            reader.MoveToFirstAttribute();

            var deserializer = getTestDeserializer(new());
            var ps = new PathStack();
            ps.EnterElement("Patient", 0, false);
            var (value, error) = deserializer.ParsePrimitiveValue(reader, implementingType, ps);

            error?.ErrorCode.Should().Be(expectedErrorCode);

            if (implementingType == typeof(DateTimeOffset) && expectedErrorCode is null)
            {
                value.Should().BeOfType<DateTimeOffset>().Which.ToFhirDate().Should().Be((string)expectedValue);
            }
            else
            {
                value.Should().Be(expectedValue);
            }
        }


        [TestMethod]
        public void TryDeserializeResourceSinglePrimitive()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
        }

        [TestMethod]
        public void TryDeserializeResourceWithEmptyAttribute()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.ATTRIBUTE_HAS_EMPTY_VALUE_CODE);

            resource.Should().BeOfType<Patient>();
        }

        [TestMethod]
        public void TryDeserializeResourceWithAttributeWithoutAValue()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE);

            resource.Should().BeOfType<Patient>();
        }

        [TestMethod]
        public void TryDeserializeResourceWithouthAValue()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE);

            resource.Should().BeOfType<Patient>();
        }




        [TestMethod]
        public void TryDeserializeResourceWithoutNamespace()
        {
            var content = "<Patient><active value=\"true\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.EMPTY_ELEMENT_NAMESPACE_CODE);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
        }

        [TestMethod]
        public void TryDeserializeResourceWithExplicitNamespaces()
        {
            var content = "<hl7:Patient xmlns:hl7='http://hl7.org/fhir'><hl7:active value=\"true\"></hl7:active></hl7:Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().BeEmpty();

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
        }


        [TestMethod]
        public void TryDeserializeResourceWithSchemaAttribute()
        {
            var content = "<Patient xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xsi:schemaLocation = \"http://hl7.org/fhir ../patient.xsd\" " +
                "xmlns = \"http://hl7.org/fhir\" >" +
                    "<active value=\"true\"/>" +
                "</Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().Contain(ce => ce.ErrorCode == ERR.SCHEMALOCATION_DISALLOWED_CODE);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
        }

        [TestMethod]
        public void TryDeserializeNarrative()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><text><status value=\"generated\"/><div xmlns=\"http://www.w3.org/1999/xhtml\">this is text</div></text><active value=\"true\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.As<Patient>().Text.Status.Should().Be(Narrative.NarrativeStatus.Generated);
            resource.As<Patient>().Text.Div.Should().Be("<div xmlns=\"http://www.w3.org/1999/xhtml\">this is text</div>");
        }


        [TestMethod]
        public void TryDeserializeExtensions()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><extension url=\"http://fire.ly/fhir/StructureDefinition/extension-test\"><valueString value =\"foo\"/></extension><active value=\"true\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
            resource.As<Patient>().Extension.Should().HaveCount(1);
            resource.As<Patient>().Extension[0].Url.Should().Be("http://fire.ly/fhir/StructureDefinition/extension-test");
            resource.As<Patient>().Extension[0].Value.As<FhirString>().Value.Should().Be("foo");
        }

        [TestMethod]
        public void TryDeserializeResourceMultiplePrimitives()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\"/><gender value=\"female\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
            resource.As<Patient>().Gender.Value.Should().Be(AdministrativeGender.Female);
        }


        [TestMethod]
        public void TryDeserializeContainedResource()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\">" +
                            "<contained>" +
                                "<Patient>" +
                                   "<multipleBirthBoolean value = \"true\"/>" +
                                "</Patient>" +
                            "</contained>" +
                            "<contained>" +
                                "<Patient>" +
                                    "<active value = \"true\"/>" +
                                "</Patient>" +
                            "</contained>" +
                            "<active value=\"true\"/>" +
                            "<gender value=\"female\"/>" +
                          "</Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
            resource.As<Patient>().Gender.Value.Should().Be(AdministrativeGender.Female);
            resource.As<Patient>().Contained.Should().HaveCount(2);
            resource.As<Patient>().Contained[0].As<Patient>().MultipleBirth.As<FhirBoolean>().Value.Should().Be(true);
            resource.As<Patient>().Contained[1].As<Patient>().Active.Value.Should().Be(true);
        }

        [TestMethod]
        public void TryDeserializeIncorrectContainedResource()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\">" +
                             "<contained>" +
                                "<Patient>" +
                                    "<multipleBirthBoolean value = \"true\"/>" +
                                "</Patient>" +
                                "<Patient>" +
                                    "<birthdate value = \"2020-01-01\"/>" +
                                "</Patient>" +
                              "</contained>" +
                              "<contained>" +
                                "<Patient>" +
                                    "<active value = \"true\"/>" +
                                "</Patient>" +
                              "</contained>" +
                              "<active value=\"true\"/>" +
                              "<gender value=\"female\"/>" +
                          "</Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();
            var resource = deserializer.DeserializeResourceInternal(reader, state);

            state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.UNALLOWED_ELEMENT_IN_RESOURCE_CONTAINER_CODE);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);
            resource.As<Patient>().Gender.Value.Should().Be(AdministrativeGender.Female);
            resource.As<Patient>().Contained.Should().HaveCount(2);
            resource.As<Patient>().Contained[0].As<Patient>().MultipleBirth.As<FhirBoolean>().Value.Should().Be(true);
            resource.As<Patient>().Contained[1].As<Patient>().Active.Value.Should().Be(true);
        }


        [TestMethod]
        public void TryDeserializeComplexResource()
        {
            var content =
            "<Patient xmlns=\"http://hl7.org/fhir\">" +
                "<active value=\"true\"/>" +
                "<name id=\"1337\">" +
                    "<given value=\"foo\"/>" +
                    "<given value=\"bar\"/>" +
                "</name>" +
                "<name>" +
                    "<given value=\"foo2\"/>" +
                    "<given value=\"bar2\"/>" +
                "</name>" +
             "</Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<Patient>();
            resource.As<Patient>().Active.Value.Should().Be(true);

            resource.As<Patient>().Name.Should().HaveCount(2);
            resource.As<Patient>().Name[0].ElementId.Should().Be("1337");
            resource.As<Patient>().Name[0].Given.Should().Equal("foo", "bar");
            resource.As<Patient>().Name[1].Given.Should().Equal("foo2", "bar2");
        }


        [TestMethod]
        public void TryDeserializeListValue()
        {
            var content = "<name xmlns=\"http://hl7.org/fhir\"><given value=\"foo\"/><given value=\"bar\"/></name>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var datatype = deserializer.DeserializeElement(typeof(HumanName), reader);

            datatype.Should().BeOfType<HumanName>();
            datatype.As<HumanName>().Given.Should().HaveCount(2);
        }

        [TestMethod]
        public void TryDeserializeWrongListValue()
        {
            var content = "<name xmlns=\"http://hl7.org/fhir\" >" +
                                "<family value=\"oof\"/>" +
                                "<given value=\"foo\"/>" +
                                "<given value=\"rab\"/>" +
                                "<prefix value=\"mr.\"/>" +
                                "<given value=\"bar\"/>" +
                          "</name>";

            var reader = constructReader(content);
            reader.Read();
            var state = new FhirXmlPocoDeserializerState();

            var deserializer = getTestDeserializer(new());
            var datatype = deserializer.DeserializeElementInternal(typeof(HumanName), reader, state);

            datatype.Should().BeOfType<HumanName>();
            datatype.As<HumanName>().Given.Should().HaveCount(3);
            datatype.As<HumanName>().Family.Should().Be("oof");

            state.Errors.Should().HaveCount(2);
            state.Errors.Should().Contain(ce => ce.ErrorCode == ERR.ELEMENT_OUT_OF_ORDER_CODE);
            state.Errors.Should().Contain(ce => ce.ErrorCode == ERR.ELEMENT_NOT_IN_SEQUENCE_CODE);
        }


        [TestMethod]
        public void TryDeserializeUnknownElement()
        {
            var content = "<name xmlns=\"http://hl7.org/fhir\"><family value =\"oof\"/><foo value = \"bar\"/><given value=\"foo\"/></name>";

            var reader = constructReader(content);
            reader.Read();

            var state = new FhirXmlPocoDeserializerState();
            var deserializer = getTestDeserializer(new());
            var datatype = deserializer.DeserializeElementInternal(typeof(HumanName), reader, state);

            datatype.Should().BeOfType<HumanName>();
            datatype.As<HumanName>().GivenElement[0].Value.Should().Be("foo");
            datatype.As<HumanName>().Family.Should().Be("oof");

            state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.UNKNOWN_ELEMENT_CODE);
        }

        [TestMethod]
        public void TryDeserializeRecursiveElements()
        {
            var content =
            "<CodeSystem xmlns=\"http://hl7.org/fhir\">" +
                "<concept>" +
                    "<code value = \"foo\" />" +
                    "<concept>" +
                        "<code value = \"bar\" />" +
                    "</concept>" +
                "</concept>" +
            "</CodeSystem >";

            var reader = constructReader(content);
            reader.Read();

            var state = new FhirXmlPocoDeserializerState();
            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResourceInternal(reader, state);
            resource.Should().NotBeNull();

            resource.As<CodeSystem>().Concept[0].Code.Should().Be("foo");
            resource.As<CodeSystem>().Concept[0].Concept[0].Code.Should().Be("bar");
        }

        [TestMethod]
        public void TryDeserializeDatatypeWithId()
        {
            var content =
                """
                    <Patient xmlns="http://hl7.org/fhir">
                      <name id="f2">
                          <use value="official" />
                          <family id="a2" value="Van" />
                          <given value="Karen" />
                      </name>
                      <birthDate id="314159" value="1932-09-24"/>
                    </Patient>
                """;

            var reader = constructReader(content);
            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);
            resource.Should().NotBeNull();

            resource.As<Patient>().BirthDateElement.ElementId.Should().Be("314159");
            resource.As<Patient>().BirthDate.Should().Be("1932-09-24");

            resource.As<Patient>().Name.Should().ContainSingle().Which.ElementId.Should().Be("f2");
            resource.As<Patient>().Name[0].FamilyElement.ElementId.Should().Be("a2");
            resource.As<Patient>().Name[0].Family.Should().Be("Van");

        }

        [TestMethod]
        public void TestCustomValidators()
        {
            test(new FhirJsonDeserializationTests.CustomComplexValidator());
            test(new FhirJsonDeserializationTests.CustomDataTypeValidator());
            test(new FhirJsonDeserializationTests.CustomPropertyValueValidator());

            static void test(IDeserializationValidator validator)
            {
                var xml = "<Patient xmlns=\"http://hl7.org/fhir\"><deceasedDateTime value=\"2070-01-01T12:01:02Z\"/></Patient>";
                var reader = constructReader(xml);
                reader.Read();

                var serializer = getTestDeserializer(new FhirXmlPocoDeserializerSettings { Validator = validator });
                var state = new FhirXmlPocoDeserializerState();

                var result = serializer.DeserializeResourceInternal(reader, state);

                state.Errors.HasExceptions.Should().BeTrue();
                state.Errors.Should().AllBeOfType<CodedValidationException>()
                    .And.ContainSingle(e => ((CodedValidationException)e).ErrorCode == CodedValidationException.DATETIME_LITERAL_INVALID_CODE);
                result.Should().BeOfType<Patient>()
                    .Which.Deceased.Should().BeOfType<FhirDateTime>()
                    .Which.Value.Should().EndWith("+00:00");
            }
        }

        [TestMethod]
        public void TestNewXmlParserNarrativeParsing()
        {
            var patient = new Patient
            {
                Id = "example",
                Text = new Narrative()
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">some test data</div>"
                }
            };

            var serializer = new BaseFhirXmlPocoSerializer(Specification.FhirRelease.STU3);
            var actual = serializer.SerializeToString(patient);

            // now parse this back out with the new parser
            BaseFhirXmlPocoDeserializer ds = getTestDeserializer(new FhirXmlPocoDeserializerSettings());

            var np = ds.DeserializeResource(actual).Should().BeOfType<Patient>().Subject;
            Assert.AreEqual(patient.Text.Div, np.Text.Div, "New narrative should be the same");
        }

        [TestMethod]
        public void TestComplicatedXml()
        {
            var xmlFileName = Path.Combine("TestData", "fp-test-patient.xml");
            var xml = File.ReadAllText(xmlFileName);
            var reader = constructReader(xml);
            reader.Read();

            var serializer = getTestDeserializer(new());
            var state = new FhirXmlPocoDeserializerState();

            var result = serializer.DeserializeResourceInternal(reader, state);

            state.Errors.HasExceptions.Should().BeTrue();

            result.Should().BeOfType<Patient>();
            result.As<Patient>().Contained.Should().HaveCount(2);
            result.As<Patient>().Contained[0].As<Patient>().Name[0].ElementId.Should().Be("firstname");
            result.As<Patient>().Contained[1].As<Questionnaire>().Text.Div.Should().NotBeNull();
        }

        private static XmlReader constructReader(string xml)
        {
            var stringReader = new StringReader(xml);
            var reader = XmlReader.Create(stringReader);
            return reader;
        }

        private static BaseFhirXmlPocoDeserializer getTestDeserializer(FhirXmlPocoDeserializerSettings settings) =>
                new(typeof(Patient).Assembly, settings);
        
        [TestMethod]
        public void TestDateTimeStuff()
        {
            var xml = """
                      <Patient xmlns="http://hl7.org/fhir">
                      <deceasedDateTime value="1310-10-13T10:23:13.00000011Z" />
                      </Patient>
                      """;
            var reader = constructReader(xml);
            var parsed = getTestDeserializer(new()).TryDeserializeResource(reader, out var instance, out var issues);

            return;
        }
    }
}