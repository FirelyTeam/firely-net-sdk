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
    [DataRow("<foo value =\"3.14e2\"/>", typeof(FhirDecimal), 3.14e2, null, DisplayName = "XmlDecimal1")]
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
}