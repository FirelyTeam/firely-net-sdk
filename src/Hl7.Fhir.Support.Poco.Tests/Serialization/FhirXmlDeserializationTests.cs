using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using VerifyMSTest;
using T = System.Threading.Tasks;
using ERR = Hl7.Fhir.Serialization.FhirXmlException;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
[UsesVerify]
public partial class FhirXmlDeserializationTests
{
    private static readonly VerifierHelper _verifier;

    static FhirXmlDeserializationTests()
    {
        _verifier = new VerifierHelper();
    }
    
    [TestMethod]
    public async T.Task CheckVerifier() => await _verifier.Check();

    [TestMethod]
    public async T.Task SerializingErroneousResource_Should_ThrowExpectedErrors()
    {
        var patientFileName = Path.Combine("TestData", "fp-test-patient-errors.xml");
        var xmlInput = File.ReadAllText(patientFileName);

        var ser = new FhirXmlDeserializer();

        try
        {
            _ = ser.Deserialize<Patient>(xmlInput);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            var recoveredActual = new FhirXmlSerializer().SerializeToString(dfe.PartialResult!);
            var errorsActual = dfe.Exceptions
                .OrderBy(e => e is ExtendedCodedException ece ? ece.LineNumber : 0);

            await _verifier.Verify(new { Errors = errorsActual, Obj = recoveredActual });
        }
    }

    [TestMethod]
    [DataRow("<active value =\"true\"/>", typeof(FhirBoolean), true, null)]
    [DataRow("<multipleBirthInteger value =\"1\"/>", typeof(Integer), 1, null)]
    [DataRow("<Birthdate value =\"2000-01-01\"/>", typeof(FhirDateTime), "2000-01-01", null)]
    [DataRow("<given value =\" foo \"/>", typeof(FhirString), "foo", ERR.STRING_SHOULD_NOT_HAVE_LEADING_OR_TRAILING_WHITESPACE)]
    public void TryDeserializePrimitives(string xmlPrimitive, Type expectedFhirType, object expectedValue, string error)
    {
        var reader = constructReader(xmlPrimitive);
        reader.Read();

        var deserializer = getTestDeserializer(
            new DeserializerSettings().Ignoring([ERR.EMPTY_ELEMENT_NAMESPACE_CODE]));
        deserializer.TryDeserializeElement(expectedFhirType, reader, out var datatype, out var errors);

        datatype.Should().BeOfType(expectedFhirType);
        datatype.As<PrimitiveType>().JsonValue.Should().Be(expectedValue);
        if (error is not null)
            errors.Should().Contain(x => x.ErrorCode == error);
    }

    [TestMethod]
    [DataRow("<foo value =\"true\"/>", typeof(FhirBoolean), true, null, DisplayName = "XmlBool1")]
    [DataRow("<foo value =\"1\"/>", typeof(FhirBoolean), "1", COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE,
        DisplayName = "XmlBool2")]
    [DataRow("<foo value =\"treu\"/>", typeof(FhirBoolean), "treu", COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE,
        DisplayName = "XmlBool3")]
    [DataRow("<foo value =\"2000-01-01T12:00:00Z\"/>", typeof(Instant), "2000-01-01T12:00:00Z", null,
        DisplayName = "XmlInstant1")]
    [DataRow("<foo value =\"foo\"/>", typeof(Instant), "foo", COVE.LITERAL_INVALID_CODE,
        DisplayName = "XmlInstant2")]
    [DataRow("<foo value =\"foo\"/>", typeof(Base64Binary), "foo", COVE.INVALID_BASE64_VALUE_CODE,
        DisplayName = "XmlByteArray")]
    [DataRow("<foo value =\"1\"/>", typeof(Integer), 1, null, DisplayName = "XmlInteger1")]
    [DataRow("<foo value =\"1.1\"/>", typeof(Integer), "1.1", COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE,
        DisplayName = "XmlInteger2")]
    [DataRow("<foo value =\"1\"/>", typeof(Integer64), "1", null, DisplayName = "XmlLong1")]
    [DataRow("<foo value =\"1.1\"/>", typeof(Integer64), "1.1", COVE.LITERAL_INVALID_CODE,
        DisplayName = "XmlLong2")]
    [DataRow("<foo value =\"3.14\"/>", typeof(FhirDecimal), 3.14, null, DisplayName = "XmlDecimal1")]
    [DataRow("<foo value =\"3.14e2\"/>", typeof(FhirDecimal), 3.14e2, null, DisplayName = "XmlDecimalScientific")]
    [DataRow("<foo value =\"3.14e500\"/>", typeof(FhirDecimal), "3.14e500", COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE,
        DisplayName = "XmlDecimal2")]

    public void TryDeserializePrimitiveValue(string xmlPrimitive, Type fhirTargetType, object expectedValue,
        string expectedErrorCode)
    {
        var reader = constructReader(xmlPrimitive);
        reader.MoveToContent();
        //reader.MoveToFirstAttribute();

        var deserializer = getTestDeserializer(
            new DeserializerSettings());
        var classMapping = ModelInfo.ModelInspector.ImportType(fhirTargetType)!;
        var target = (PrimitiveType)classMapping.CreateInstance()!;
        var state = new PocoDeserializerState();
        deserializer.DeserializeElementInto(target, classMapping, reader, state);

        var cleaned = state.Errors.Remove(ce => ce.ErrorCode == ERR.EMPTY_ELEMENT_NAMESPACE_CODE).ToList();

        cleaned.Should().HaveCount(expectedErrorCode == null ? 0 : 1);

        if (cleaned.Count > 0)
        {
            cleaned.First().ErrorCode.Should().Be(expectedErrorCode);
        }
        else
        {
            target.JsonValue.Should().Be(expectedValue);
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
    public void ListErrorsAreReportedAtTheElement()
    {
        // These errors are about the list as a whole, so they should not point at its last element.
        var content = "<Patient xmlns=\"http://hl7.org/fhir\"><Name><family value=\"a\"/></Name><Name><family value=\"b\"/></Name></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var state = new PocoDeserializerState();
        _ = getTestDeserializer(new()).DeserializeResourceInternal(reader, state);

        state.Errors.Should().ContainSingle(ce => ce.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE)
            .Which.Should().BeAssignableTo<ExtendedCodedException>().Which.InstancePath.Should().Be("Patient.name");
    }

    [TestMethod]
    public void TryDeserializeResourceWithEmptyAttribute()
    {
        var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"\"/></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        state.Errors.Should().OnlyContain(ce => ce.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE);

        resource.Should().BeOfType<Patient>();
    }

    [TestMethod]
    public void TryDeserializeResourceWithAttributeWithoutAValue()
    {
        var content = "<Patient xmlns=\"http://hl7.org/fhir\"><active/></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        state.Errors.Should().OnlyContain(ce => ce.ErrorCode == COVE.ELEMENT_CANNOT_BE_EMPTY_CODE);

        resource.Should().BeOfType<Patient>();
    }

    [TestMethod]
    public void MakeSureVersionSpecificTypedPropertiesGetCreatedOk()
    {
        var pat = new Patient()
        {
            Meta = new Meta() { VersionId = "1", ProfileElement = [new FhirUri("http://nu.nl")] }
        };

        var content = pat.ToXml();
        var pat2 = FhirXmlDeserializer.DEFAULT.DeserializeResource(content);

        // If we deserialize the profile incorrectly due to AllowedTypes etc,
        // it will end up in the overflow and this will crash.
        pat2.Meta.Profile.Should().HaveCount(1);
    }

    [TestMethod]
    public void TryDeserializeResourceWithouthAValue()
    {
        var content = "<Patient xmlns=\"http://hl7.org/fhir\"></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        state.Errors.Should().OnlyContain(ce => ce.ErrorCode == COVE.ELEMENT_CANNOT_BE_EMPTY_CODE);

        resource.Should().BeOfType<Patient>();
    }



    [TestMethod]
    public void TryDeserializeResourceWithoutNamespace()
    {
        var content = "<Patient><active value=\"true\"/></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        state.Errors.Should().OnlyContain(ce => ce.ErrorCode == ERR.EMPTY_ELEMENT_NAMESPACE_CODE);

        resource.Should().BeOfType<Patient>();
        resource.As<Patient>().Active.Value.Should().Be(true);
    }

    [TestMethod]
    public void TryDeserializeResourceWithExplicitNamespaces()
    {
        var content =
            "<hl7:Patient xmlns:hl7='http://hl7.org/fhir'><hl7:active value=\"true\"></hl7:active></hl7:Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();
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

        var deserializer = getTestDeserializer(new DeserializerSettings { DisallowXsiAttributesOnRoot = true });
        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        state.Errors.Should().Contain(ce => ce.ErrorCode == ERR.SCHEMALOCATION_DISALLOWED_CODE);

        resource.Should().BeOfType<Patient>();
        resource.As<Patient>().Active.Value.Should().Be(true);
    }

    [TestMethod]
    public void TryDeserializeNarrative()
    {
        var content =
            "<Patient xmlns=\"http://hl7.org/fhir\"><text><status value=\"generated\"/><div xmlns=\"http://www.w3.org/1999/xhtml\">this is text</div></text><active value=\"true\"/></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var resource = deserializer.DeserializeResource(reader);

        resource.As<Patient>().Text.Status.Should().Be(Narrative.NarrativeStatus.Generated);
        resource.As<Patient>().Text.Div.Should()
            .Be("<div xmlns=\"http://www.w3.org/1999/xhtml\">this is text</div>");
    }


    [TestMethod]
    public void TryDeserializeExtensions()
    {
        var content =
            "<Patient xmlns=\"http://hl7.org/fhir\"><extension url=\"http://fire.ly/fhir/StructureDefinition/extension-test\"><valueString value =\"foo\"/></extension><active value=\"true\"/></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var resource = deserializer.DeserializeResource(reader);

        resource.Should().BeOfType<Patient>();
        resource.As<Patient>().Active!.Should().Be(true);
        resource.As<Patient>().Extension.Should().HaveCount(1);
        resource.As<Patient>().Extension[0].Url.Should()
            .Be("http://fire.ly/fhir/StructureDefinition/extension-test");
        resource.As<Patient>().Extension[0].Value.As<FhirString>().Value.Should().Be("foo");
    }

    [TestMethod]
    public void TryDeserializeResourceMultiplePrimitives()
    {
        var content =
            "<Patient xmlns=\"http://hl7.org/fhir\"><active value=\"true\"/><gender value=\"female\"/></Patient>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var resource = deserializer.DeserializeResource(reader);

        resource.Should().BeOfType<Patient>();
        resource.As<Patient>().Active.Value.Should().Be(true);
        resource.As<Patient>().Gender.Value.Should().Be(AdministrativeGender.Female);
    }

    [TestMethod]
    public void MandatoryElementsShouldBeDetected()
    {
        var content =
            "<Observation xmlns=\"http://hl7.org/fhir\"><issued value=\"2025-04-15T12:09:00Z\"/></Observation>";

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var success = deserializer.TryDeserializeResource(reader, out var resource, out var errors);
        success.Should().BeFalse();

        errors.Should().Contain(ce =>
            ce.ErrorCode == CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
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
        var state = new PocoDeserializerState();

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
        var content =
            "<name xmlns=\"http://hl7.org/fhir\"><family value =\"oof\"/><foo value = \"bar\"/><given value=\"foo\"/></name>";

        var reader = constructReader(content);
        reader.Read();

        var state = new PocoDeserializerState();
        var deserializer = getTestDeserializer(new());
        var datatype = deserializer.DeserializeElementInternal(typeof(HumanName), reader, state);

        datatype.Should().BeOfType<HumanName>();
        datatype.As<HumanName>().GivenElement[0].Value.Should().Be("foo");
        datatype.As<HumanName>().Family.Should().Be("oof");

        state.Errors.Select(x => x.ErrorCode).Should().BeEquivalentTo([COVE.UNKNOWN_ELEMENT_CODE]);
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

        var state = new PocoDeserializerState();
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
    public void TestValidatorIsCalledDuringDeserialization()
    {
        var validator = new FhirJsonDeserializationTests.CustomComplexValidator();

        const string xml =
            "<Patient xmlns=\"http://hl7.org/fhir\"><deceasedDateTime value=\"2070-01-01T12:01:02Z\"/></Patient>";
        var reader = constructReader(xml);
        reader.Read();

        var serializer = getTestDeserializer(new DeserializerSettings { Validator = validator });
        serializer.TryDeserializeResource(reader, out _, out var issues);

        var errors = issues.ToList();
        errors.Should().HaveCount(1);
        errors.Single().Should().BeOfType<CodedValidationException>().Which.ErrorCode.Should()
            .Be(CodedValidationException.LITERAL_INVALID_CODE);
        validator.DateTimeSeenByInstanceValidator?.Value.Should().Be("1972-11-30T12:00:00Z");
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

        var actual = FhirXmlSerializer.Default.SerializeToString(patient);

        // now parse this back out with the new parser
        BaseFhirXmlDeserializer ds = getTestDeserializer(new DeserializerSettings());

        var np = ds.DeserializeResource(actual).Should().BeOfType<Patient>().Subject;
        Assert.AreEqual(patient.Text.Div, np.Text.Div, "New narrative should be the same");
    }

    [TestMethod]
    public void TestComplicatedXml()
    {
        var xmlFileName = Path.Combine("TestData", "fp-test-patient.xml");
        var xml = File.ReadAllText(xmlFileName);
        var reader = constructReader(xml);
        reader.MoveToContent();

        var serializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();

        var result = serializer.DeserializeResourceInternal(reader, state);

        state.Errors.HasExceptions.Should().BeTrue();

        result.Should().BeOfType<Patient>();
        result.As<Patient>().Contained.Should().HaveCount(2);
        result.As<Patient>().Contained[0].As<Patient>().Name[0].ElementId.Should().Be("firstname");
        result.As<Patient>().Contained[1].As<Questionnaire>().Text.Div.Should().NotBeNull();
    }

    [TestMethod]
    public void DuplicateAttributesShouldBeReportedCorrectly()
    {
        // This test verifies that when an element has duplicate attributes,
        // the parser reports an error but continues parsing gracefully
        var content = """
            <Patient xmlns="http://hl7.org/fhir">
                <address>
                    <line id="test2" value="5. OG - Hinterhof" value="2test">
                        <extension url="http://hl7.org/fhir/StructureDefinition/iso21090-ADXP-additionalLocator">
                            <valueString value="5. OG - Hinterhof" />
                        </extension>
                    </line>
                </address>
            </Patient>
            """;

        var reader = constructReader(content);
        reader.Read();

        var deserializer = getTestDeserializer(new());
        var state = new PocoDeserializerState();
        var callable = () => deserializer.DeserializeResourceInternal(reader, state);
        Assert.Throws<XmlException>(callable, "XML parser throws an exception when duplicate attributes are present, and it's not being hidden by us");
    }

    private static XmlReader constructReader(string xml)
    {
        var stringReader = new StringReader(xml);
        var reader = XmlReader.Create(stringReader);
        return reader;
    }

    private static FhirXmlDeserializer getTestDeserializer(DeserializerSettings settings) => new(settings);

    [TestMethod]
    public void TestDateTimeStuff()
    {
        var xml = """
                  <Patient xmlns="http://hl7.org/fhir">
                  <deceasedDateTime value="1310-10-13T10:23:13.00000011Z" />
                  </Patient>
                  """;
        var reader = constructReader(xml);
        var parsed = getTestDeserializer(new DeserializerSettings())
            .TryDeserializeResource(reader, out var instance, out var issues);

        return;
    }

    [TestMethod]
    public void CanParseAndSerializeCustomProperty()
    {
        var inspector = new ModelInspector(FhirRelease.STU3);
        inspector.Import(typeof(Patient).Assembly);
        inspector.Import(typeof(Base).Assembly);

        var parser = new BaseFhirXmlDeserializer(inspector,
            new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));

        const string json = """
                            <Patient xmlns="http://hl7.org/fhir">
                                <active value="true"/>
                                <patientLocation value="http://nu.nl"/>
                                <remarksString value="Nice guy"/>
                                <newList value="singleitem" />
                            </Patient>
                            """;

        var parsed = parser.DeserializeResource(json).Should().BeOfType<Patient>().Subject;
        parsed.Active.Should().BeTrue();
        parsed["active"].Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
        parsed["patientLocation"].Should().BeOfType<DynamicPrimitive>().Which.Value.Should().Be("http://nu.nl");
        parsed["remarksString"].Should().BeOfType<DynamicPrimitive>().Which.Value.Should().Be("Nice guy");
        parsed["newList"].Should().BeOfType<DynamicPrimitive>().Which.Value.Should().Be("singleitem");

        var patientMapping = inspector.FindClassMapping(typeof(Patient))!;
        var customPropertyA = new PropertyMapping(patientMapping, "patientLocation", typeof(FhirUri));
        patientMapping.PropertyMappings.Add(customPropertyA);
        var customPropertyB = new PropertyMapping(patientMapping, "remarks", typeof(DataType),
            [typeof(FhirString), typeof(Markdown)]);
        patientMapping.PropertyMappings.Add(customPropertyB);
        var customPropertyC = new PropertyMapping(patientMapping, "newList", typeof(List<FhirString>));
        patientMapping.PropertyMappings.Add(customPropertyC);

        parsed = parser.DeserializeResource(json).Should().BeOfType<Patient>().Subject;
        parsed.Active.Should().BeTrue();
        parsed["patientLocation"].Should().BeOfType<FhirUri>().Which.Value.Should().Be("http://nu.nl");
        parsed["remarks"].Should().BeOfType<FhirString>().Which.Value.Should().Be("Nice guy");
        parsed["newList"].Should().BeOfType<List<FhirString>>().Which.Single().Value.Should().Be("singleitem");

        var serializer = new BaseFhirXmlSerializer(inspector);
        var serialized = serializer.SerializeToString(parsed);
        XmlAssert.AreSame("parsed", json, serialized);
    }

    [TestMethod]
    public void WrongCaseNames_DefaultMode_NoErrors()
    {
        // In the default (non-Strict) mode, case-sensitivity issues are filtered and should not surface.
        var settings = new DeserializerSettings();
        var deserializer = getTestDeserializer(settings);

        var content = "<Patient xmlns=\"http://hl7.org/fhir\"><Id value=\"test\"/></Patient>";
        var reader = constructReader(content);
        reader.Read();
        var result = deserializer.DeserializeResource(reader);
        result.Should().BeOfType<Patient>().Which.Id.Should().Be("test");
    }

    [TestMethod]
    public void WrongCaseNames_BackwardsCompatibleMode_NoErrors()
    {
        // BackwardsCompatible mode must also suppress case-sensitivity errors.
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.BackwardsCompatible);
        var deserializer = getTestDeserializer(settings);

        var content = "<patient xmlns=\"http://hl7.org/fhir\"><Id value=\"test\"/></patient>";
        var reader = constructReader(content);
        reader.Read();
        var result = deserializer.DeserializeResource(reader);
        result.Should().BeOfType<Patient>().Which.Id.Should().Be("test");
    }

    [TestMethod]
    public void WrongCaseChoicePrefix_LenientModes_BindsToChoiceElement()
    {
        // SDK 6.2 and earlier matched the element-name part of a choice name case-sensitively, so
        // "DeceasedBoolean" was treated as an unknown element. We consider that a bug: in lenient
        // modes, all wrong-cased names bind to the element they nearly match.
        var settings = new DeserializerSettings();
        var deserializer = getTestDeserializer(settings);

        var content = "<Patient xmlns=\"http://hl7.org/fhir\"><DeceasedBoolean value=\"true\"/></Patient>";
        var reader = constructReader(content);
        reader.Read();
        var result = deserializer.DeserializeResource(reader);
        result.Should().BeOfType<Patient>().Which.Deceased.Should().BeOfType<FhirBoolean>()
            .Which.Value.Should().Be(true);
    }

    [TestMethod]
    [DataRow("<Patient xmlns=\"http://hl7.org/fhir\"><Id value=\"test\"/></Patient>", COVE.WRONG_CASED_ELEMENT_CODE, DisplayName = "WrongCase_PropertyName")]
    [DataRow("<patient xmlns=\"http://hl7.org/fhir\"><id value=\"test\"/></patient>", COVE.WRONG_CASED_RESOURCE_TYPE_CODE, DisplayName = "WrongCase_ResourceType")]
    [DataRow("<Patient xmlns=\"http://hl7.org/fhir\"><deceasedDatetime value=\"2022-05\"/></Patient>", COVE.WRONG_CASED_ELEMENT_CODE, DisplayName = "WrongCase_ChoiceTypeSuffix")]
    [DataRow("<Observation xmlns=\"http://hl7.org/fhir\"><status value=\"final\"/><code><text value=\"t\"/></code><ValueString value=\"hello\"/></Observation>", COVE.WRONG_CASED_ELEMENT_CODE, DisplayName = "WrongCase_ChoicePrefix")]
    public void WrongCaseNames_StrictMode_ReportsError(string xmlContent, string expectedErrorCode)
    {
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.Strict);
        var deserializer = getTestDeserializer(settings);
        var reader = constructReader(xmlContent);
        reader.Read();

        var state = new PocoDeserializerState();
        _ = deserializer.DeserializeResourceInternal(reader, state);
        state.Errors.Should().Contain(e => e.ErrorCode == expectedErrorCode);
    }

    [TestMethod]
    public void WrongCaseNames_StrictMode_PreservesDataInOverflow()
    {
        // In strict mode, a wrong-cased element is not silently corrected: it is treated as an
        // unknown element, so the original data is preserved (and roundtrippable) in the overflow.
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.Strict);
        var deserializer = getTestDeserializer(settings);
        var content = "<Patient xmlns=\"http://hl7.org/fhir\"><Active value=\"true\"/></Patient>";
        var reader = constructReader(content);
        reader.Read();

        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        // The casing violation is reported - and only that: a near miss is never additionally
        // reported as an unknown element.
        state.Errors.Should().ContainSingle(e => e.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE);
        state.Errors.Should().NotContain(e => e.ErrorCode == COVE.UNKNOWN_ELEMENT_CODE);

        // The data is in the overflow under its original name, not in the typed property.
        var patient = resource.Should().BeOfType<Patient>().Subject;
        patient.ActiveElement.Should().BeNull();
        patient.TryGetValue("Active", out _).Should().BeTrue();
    }

    [TestMethod]
    public void WrongCaseResourceType_StrictMode_StillBindsToPocoType()
    {
        // Unlike elements, a wrong-cased resource type is still deserialized into the matching
        // POCO type: falling back to a DynamicResource would degrade the entire resource to
        // overflow data, which is disproportionate. The violation is reported with its own code.
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.Strict);
        var deserializer = getTestDeserializer(settings);
        var reader = constructReader("<patient xmlns=\"http://hl7.org/fhir\"><id value=\"test\"/></patient>");
        reader.Read();

        var state = new PocoDeserializerState();
        var resource = deserializer.DeserializeResourceInternal(reader, state);

        state.Errors.Should().ContainSingle(e => e.ErrorCode == COVE.WRONG_CASED_RESOURCE_TYPE_CODE);
        resource.Should().BeOfType<Patient>().Which.Id.Should().Be("test");
    }

    [TestMethod]
    public void WrongCase_ChoicePrefixAndSuffix_SuggestsFullyCorrectedName()
    {
        // When both the choice prefix ("Value") and the type suffix ("Datetime") have wrong casing,
        // the suggested name in the error message should be fully corrected ("valueDateTime"), not
        // just have its prefix fixed ("valueDatetime").
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.Strict);
        var deserializer = getTestDeserializer(settings);
        var content = "<Observation xmlns=\"http://hl7.org/fhir\"><status value=\"final\"/><code><text value=\"t\"/></code><ValueDatetime value=\"2022-05\"/></Observation>";
        var reader = constructReader(content);
        reader.Read();

        var state = new PocoDeserializerState();
        _ = deserializer.DeserializeResourceInternal(reader, state);
        state.Errors.Should().Contain(e => e.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE
                                            && e.Message.Contains("valueDateTime"));
    }

    [TestMethod]
    // Also verifies that a multi-word type suffix like "CodeableConcept" does not produce a false positive.
    [DataRow("<Patient xmlns=\"http://hl7.org/fhir\"><id value=\"test\"/><deceasedDateTime value=\"2022-05\"/></Patient>", DisplayName = "Correct_DateTimeChoice")]
    [DataRow("<Observation xmlns=\"http://hl7.org/fhir\"><id value=\"test\"/><status value=\"final\"/><code><text value=\"t\"/></code><valueCodeableConcept><text value=\"x\"/></valueCodeableConcept></Observation>", DisplayName = "Correct_CodeableConceptChoice")]
    public void CorrectCaseNames_StrictMode_NoError(string content)
    {
        var settings = new DeserializerSettings().UsingMode(DeserializationMode.Strict);
        var deserializer = getTestDeserializer(settings);
        var reader = constructReader(content);
        reader.Read();
        var state = new PocoDeserializerState();
        _ = deserializer.DeserializeResourceInternal(reader, state);
        state.Errors.Should().NotContain(e => e.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE
                                              || e.ErrorCode == COVE.WRONG_CASED_RESOURCE_TYPE_CODE);
    }

    // In Xml every value is text, so "wrongly typed" means the text is not a valid literal for the
    // primitive and stays a string after parsing.
    private const string WRONGLY_TYPED_PATIENT_XML = """
        <Patient xmlns="http://hl7.org/fhir">
          <extension url="http://example.org/int"><valueInteger value="42.0"/></extension>
          <extension url="http://example.org/pos"><valuePositiveInt value="7.0"/></extension>
          <extension url="http://example.org/unsigned"><valueUnsignedInt value="0.0"/></extension>
          <extension url="http://example.org/dec"><valueDecimal value=".5"/></extension>
          <extension url="http://example.org/bool"><valueBoolean value="False"/></extension>
          <active id="a1" value="True"/>
          <name><family value="Doe"/></name>
          <birthDate value="2000-01-01"/>
          <multipleBirthInteger value="2.0"/>
        </Patient>
        """;

    private static (Patient, IEnumerable<CodedException>) parseWithConverter(DeserializerSettings settings, string xml = WRONGLY_TYPED_PATIENT_XML)
    {
        getTestDeserializer(settings).TryDeserializeResource(xml, out var resource, out var issues);
        return (resource.Should().BeOfType<Patient>().Subject, issues);
    }

    [TestMethod]
    [DataRow(DeserializationMode.Recoverable)]
    [DataRow(DeserializationMode.Strict)]
    [DataRow(DeserializationMode.Ostrich)]
    public void PrimitiveValueConverter_ConvertsWronglyTypedPrimitives(DeserializationMode mode)
    {
        var settings = new DeserializerSettings { PrimitiveValueConverter = FhirJsonDeserializationTests.ConvertNumbersAndBooleans }
            .UsingMode(mode).Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]);

        var (patient, issues) = parseWithConverter(settings);

        FhirJsonDeserializationTests.AssertConvertedPatient(patient, decValue: 0.5m);
        issues.Should().NotContain(i => i.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE);
    }

    [TestMethod]
    [DataRow(DeserializationMode.Recoverable)]
    [DataRow(DeserializationMode.Ostrich)]
    public void PrimitiveValueConverter_NotSet_KeepsReportingWrongTypes(DeserializationMode mode)
    {
        var settings = new DeserializerSettings().UsingMode(mode).Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]);

        var (patient, issues) = parseWithConverter(settings);

        patient.ActiveElement.JsonValue.Should().Be("True");
        patient.Extension[0].Value.Should().BeOfType<Integer>().Which.JsonValue.Should().Be("42.0");
        issues.Count(i => i.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE).Should().Be(mode == DeserializationMode.Ostrich ? 0 : 7, "Ostrich runs no validator");
    }

    [TestMethod]
    public void PrimitiveValueConverter_Declining_KeepsReportingWrongTypes()
    {
        var settings = new DeserializerSettings { PrimitiveValueConverter = (v, _, _) => v }
            .UsingMode(DeserializationMode.Recoverable).Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]);

        var (patient, issues) = parseWithConverter(settings);

        patient.ActiveElement.JsonValue.Should().Be("True");
        issues.Count(i => i.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE).Should().Be(7);
    }

    [TestMethod]
    public void PrimitiveValueConverter_FailedConversion_KeepsReportingWrongType()
    {
        var settings = new DeserializerSettings { PrimitiveValueConverter = FhirJsonDeserializationTests.ConvertNumbersAndBooleans }
            .UsingMode(DeserializationMode.Recoverable).Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]);

        // "forty-two" throws a FormatException, "4.5" an OverflowException.
        var (patient, issues) = parseWithConverter(settings,
            """<Patient xmlns="http://hl7.org/fhir"><extension url="http://example.org/int"><valueInteger value="forty-two"/></extension><active value="yes"/><multipleBirthInteger value="4.5"/></Patient>""");

        patient.ActiveElement.JsonValue.Should().Be("yes");
        patient.MultipleBirth.Should().BeOfType<Integer>().Which.JsonValue.Should().Be("4.5");
        patient.Extension[0].Value.Should().BeOfType<Integer>().Which.JsonValue.Should().Be("forty-two");
        issues.Count(i => i.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE).Should().Be(3);
    }

    [TestMethod]
    public void PrimitiveValueConverter_OtherExceptionsPropagate()
    {
        var settings = new DeserializerSettings { PrimitiveValueConverter = (_, _, _) => throw new InvalidOperationException("boom") };

        var act = () => parseWithConverter(settings);

        act.Should().Throw<InvalidOperationException>().WithMessage("boom");
    }

    [TestMethod]
    public void PrimitiveValueConverter_IsOnlyCalledForWronglyTypedValues()
    {
        var calls = new List<(object, Type, string)>();
        var settings = new DeserializerSettings
        {
            PrimitiveValueConverter = (v, t, m) =>
            {
                calls.Add((v, t, m.Name));
                return FhirJsonDeserializationTests.ConvertNumbersAndBooleans(v, t, m);
            }
        };

        var (patient, _) = parseWithConverter(settings,
            """<Patient xmlns="http://hl7.org/fhir"><meta><lastUpdated value="2020-01-01T00:00:00Z"/></meta><extension url="http://example.org/dec"><valueDecimal value=".5"/></extension><extension url="http://example.org/time"><valueTime value="10:00:00"/></extension><active value="true"/><name><family value="Doe"/></name><gender value="male"/><birthDate value="2000"/><deceasedDateTime value="2020-01-01"/><multipleBirthInteger value="2"/></Patient>""");

        calls.Should().BeEquivalentTo([
            ("2020-01-01T00:00:00Z", typeof(string), "instant"), ("2020-01-01", typeof(string), "dateTime"), ("10:00:00", typeof(string), "time"),
            ("2000", typeof(string), "date"), (".5", typeof(decimal), "decimal")]);
        patient.BirthDateElement.JsonValue.Should().Be("2000");
        patient.Name.Single().Family.Should().Be("Doe");
        patient.Extension[0].Value.Should().BeOfType<FhirDecimal>().Which.JsonValue.Should().Be(0.5m);
    }

    [TestMethod]
    public void PrimitiveValueConverter_CanFixDateTimeFormat()
    {
        object fixDateTime(object v, Type _, ClassMapping m) =>
            m.NativeType == typeof(FhirDateTime) && v is string s ? s.Replace(' ', 'T') + "Z" : v;
        var settings = new DeserializerSettings { PrimitiveValueConverter = fixDateTime };
        const string data = """<Patient xmlns="http://hl7.org/fhir"><deceasedDateTime value="2020-01-01 10:00:00"/></Patient>""";

        var (patient, issues) = parseWithConverter(settings, data);
        patient.Deceased.Should().BeOfType<FhirDateTime>().Which.Value.Should().Be("2020-01-01T10:00:00Z");
        issues.Should().BeEmpty();

        (_, issues) = parseWithConverter(new DeserializerSettings(), data);
        issues.Should().ContainSingle(i => i.ErrorCode == COVE.LITERAL_INVALID_CODE);
    }

    [TestMethod]
    [DataRow(DeserializationMode.Recoverable)]
    [DataRow(DeserializationMode.Ostrich)]
    public void PrimitiveValueConverter_ConvertsRepeatingPrimitives(DeserializationMode mode)
    {
        var settings = new DeserializerSettings { PrimitiveValueConverter = FhirJsonDeserializationTests.ConvertNumbersAndBooleans }
            .UsingMode(mode).Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]);
        const string xml = """
            <ClaimResponse xmlns="http://hl7.org/fhir">
              <item>
                <sequenceLinkId value="1.0"/>
                <noteNumber id="n1" value="1.0"/>
                <noteNumber value="2"/>
                <noteNumber value="3.0"><extension url="http://example.org/b"><valueBoolean value="True"/></extension></noteNumber>
              </item>
            </ClaimResponse>
            """;

        getTestDeserializer(settings).TryDeserializeResource(xml, out var resource, out var issues);

        var item = resource.Should().BeOfType<ClaimResponse>().Subject.Item.Single();
        item.SequenceLinkIdElement.JsonValue.Should().Be(1);
        item.NoteNumberElement.Select(n => n.JsonValue).Should().Equal(1, 2, 3);
        item.NoteNumberElement[0].ElementId.Should().Be("n1");
        item.NoteNumberElement[2].Extension.Single().Value.Should().BeOfType<FhirBoolean>().Which.JsonValue.Should().Be(true);
        issues.Should().NotContain(i => i.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE);
    }
}