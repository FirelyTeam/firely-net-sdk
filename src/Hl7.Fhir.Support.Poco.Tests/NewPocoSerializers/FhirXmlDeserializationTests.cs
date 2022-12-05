using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;
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
            var (value, error) = deserializer.ParsePrimitiveValue(reader, implementingType);

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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
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

            resource.Should().BeOfType<TestPatient>();
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

            resource.Should().BeOfType<TestPatient>();
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

            resource.Should().BeOfType<TestPatient>();
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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
        }

        [TestMethod]
        public void TryDeserializeNarrative()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><text><status value=\"generated\"/><div xmlns=\"http://www.w3.org/1999/xhtml\">this is text</div></text><active value=\"true\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.As<TestPatient>().Text.Status.Should().Be(Narrative.NarrativeStatus.Generated);
            resource.As<TestPatient>().Text.Div.Should().Be("<div xmlns=\"http://www.w3.org/1999/xhtml\">this is text</div>");
        }


        [TestMethod]
        public void TryDeserializeExtensions()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><extension url=\"http://fire.ly/fhir/StructureDefinition/extension-test\"><valueString value =\"foo\"/></extension><active value=\"true\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
            resource.As<TestPatient>().Extension.Should().HaveCount(1);
            resource.As<TestPatient>().Extension[0].Url.Should().Be("http://fire.ly/fhir/StructureDefinition/extension-test");
            resource.As<TestPatient>().Extension[0].Value.As<FhirString>().Value.Should().Be("foo");
        }

        [TestMethod]
        public void TryDeserializeResourceMultiplePrimitives()
        {
            var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\"/><gender value=\"female\"/></Patient>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var resource = deserializer.DeserializeResource(reader);

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
            resource.As<TestPatient>().Gender.Value.Should().Be(TestAdministrativeGender.Female);
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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
            resource.As<TestPatient>().Gender.Value.Should().Be(TestAdministrativeGender.Female);
            resource.As<TestPatient>().Contained.Should().HaveCount(2);
            resource.As<TestPatient>().Contained[0].As<TestPatient>().MultipleBirth.As<FhirBoolean>().Value.Should().Be(true);
            resource.As<TestPatient>().Contained[1].As<TestPatient>().Active.Value.Should().Be(true);
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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);
            resource.As<TestPatient>().Gender.Value.Should().Be(TestAdministrativeGender.Female);
            resource.As<TestPatient>().Contained.Should().HaveCount(2);
            resource.As<TestPatient>().Contained[0].As<TestPatient>().MultipleBirth.As<FhirBoolean>().Value.Should().Be(true);
            resource.As<TestPatient>().Contained[1].As<TestPatient>().Active.Value.Should().Be(true);
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

            resource.Should().BeOfType<TestPatient>();
            resource.As<TestPatient>().Active.Value.Should().Be(true);

            resource.As<TestPatient>().Name.Should().HaveCount(2);
            resource.As<TestPatient>().Name[0].ElementId.Should().Be("1337");
            resource.As<TestPatient>().Name[0].Given.Should().Equal("foo", "bar");
            resource.As<TestPatient>().Name[1].Given.Should().Equal("foo2", "bar2");
        }


        [TestMethod]
        public void TryDeserializeListValue()
        {
            var content = "<name xmlns=\"http://hl7.org/fhir\"><given value=\"foo\"/><given value=\"bar\"/></name>";

            var reader = constructReader(content);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var datatype = deserializer.DeserializeElement(typeof(TestHumanName), reader);

            datatype.Should().BeOfType<TestHumanName>();
            datatype.As<TestHumanName>().Given.Should().HaveCount(2);
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
            var datatype = deserializer.DeserializeElementInternal(typeof(TestHumanName), reader, state);

            datatype.Should().BeOfType<TestHumanName>();
            datatype.As<TestHumanName>().Given.Should().HaveCount(3);
            datatype.As<TestHumanName>().Family.Should().Be("oof");

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
            var datatype = deserializer.DeserializeElementInternal(typeof(TestHumanName), reader, state);

            datatype.Should().BeOfType<TestHumanName>();
            datatype.As<TestHumanName>().GivenElement[0].Value.Should().Be("foo");
            datatype.As<TestHumanName>().Family.Should().Be("oof");

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

            resource.As<TestCodeSystem>().Concept[0].Code.Should().Be("foo");
            resource.As<TestCodeSystem>().Concept[0].Concept[0].Code.Should().Be("bar");
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
                result.Should().BeOfType<TestPatient>()
                    .Which.Deceased.Should().BeOfType<FhirDateTime>()
                    .Which.Value.Should().EndWith("+00:00");
            }
        }

        [TestMethod]
        public void TestNewXmlParserNarrativeParsing()
        {
            var patient = new TestPatient { Id = "example" };
            patient.Text = new Narrative() { Status = Narrative.NarrativeStatus.Generated, Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">some test data</div>" };
            var serializer = new FhirXmlPocoSerializer(Specification.FhirRelease.STU3);
            var actual = SerializationUtil.WriteXmlToString(patient, (o, w) => serializer.Serialize(o, w));

            //// now parse this back out with the old parser
            //var op = new Hl7.Fhir.Serialization.FhirXmlParser().Parse<Patient>(patientXML);
            //Assert.AreEqual(patient.Text.Div, op.Text.Div, "Old narrative should be the same");

            // now parse this back out with the new parser
            FhirXmlPocoDeserializer ds = getTestDeserializer(new());
            using (var reader = SerializationUtil.XmlReaderFromXmlText(actual))
            {
                var np = ds.DeserializeResource(reader) as TestPatient;
                Assert.AreEqual(patient.Text.Div, np.Text.Div, "New narrative should be the same");
            }
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

            result.Should().BeOfType<TestPatient>();
            result.As<TestPatient>().Contained.Should().HaveCount(2);
            result.As<TestPatient>().Contained[0].As<TestPatient>().Name[0].ElementId.Should().Be("firstname");
            result.As<TestPatient>().Contained[1].As<TestQuestionnaire>().Text.Div.Should().NotBeNull();
        }

        private static XmlReader constructReader(string xml)
        {
            var stringReader = new StringReader(xml);
            var reader = XmlReader.Create(stringReader);
            return reader;
        }

        private static FhirXmlPocoDeserializer getTestDeserializer(FhirXmlPocoDeserializerSettings settings) =>
                new(typeof(TestPatient).Assembly, settings);

    }
}
