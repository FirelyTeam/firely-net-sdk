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
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using VerifyMSTest;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using DataType = Hl7.Fhir.Model.DataType;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;
using FhirJsonConverterFactory = Hl7.Fhir.Serialization.FhirJsonConverterFactory;
using T=System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
[UsesVerify]
public partial class FhirJsonDeserializationTests
{
    private static readonly VerifierHelper _verifier;

    static FhirJsonDeserializationTests()
    {
        _verifier = new VerifierHelper();
    }

    [TestMethod]
    public async T.Task CheckVerifier() => await _verifier.Check();

    private static (Base?, IReadOnlyCollection<CodedException>) deserializeComplex(Type objectType,
        object testObject, out Utf8JsonReader readerState,
        FhirJsonConverterOptions settings)
    {
        // For the tests, enable full XHML validation so we can test it when necessary.
        var deserializer = new FhirJsonDeserializer(settings);
        Utf8JsonReader reader = constructReader(testObject);
        reader.Read();

        try
        {
            var result = objectType.IsAssignableTo(typeof(Resource))
                ? deserializer.DeserializeResource(ref reader)
                : deserializer.DeserializeObject(objectType, ref reader);

            readerState = reader; // copy
            return (result, Array.Empty<CodedException>());
        }
        catch (DeserializationFailedException dfe)
        {
            readerState = reader;
            return (dfe.PartialResult, dfe.Exceptions);
        }
    }

    private static Utf8JsonReader constructReader(object testObject)
    {
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(testObject);
        var reader = new Utf8JsonReader(jsonBytes);
        return reader;
    }

    [TestMethod]
    [DynamicData(nameof(TestDeserializeResourceData))]
    [DynamicData(nameof(TestDeserializeNestedResource))]
    public void TestDeserializeResource(object testObject, JsonTokenType tokenAfterParsing, params string[] errors)
    {
        var reader = constructReader(testObject);
        reader.Read();

        var deserializer = new FhirJsonDeserializer(new DeserializerSettings { ExceptionFilter = null } );
        _ = deserializer.TryDeserializeResource(ref reader, out Resource? _, out var issues);
        issues.Select(err => err.ErrorCode).Should().BeEquivalentTo(errors);
        reader.TokenType.Should().Be(tokenAfterParsing);
    }

    public static IEnumerable<object[]> TestDeserializeResourceData
    {
        get
        {
            yield return [new { }, JsonTokenType.EndObject, ERR.NO_RESOURCETYPE_PROPERTY_CODE, ERR.OBJECTS_CANNOT_BE_EMPTY_CODE, COVE.ELEMENT_CANNOT_BE_EMPTY_CODE];
            yield return
            [
                new { resourceType = 4, crap = 4 }, JsonTokenType.EndObject,
                ERR.RESOURCETYPE_SHOULD_BE_STRING_CODE, COVE.UNKNOWN_RESOURCE_TYPE_CODE, COVE.UNKNOWN_ELEMENT_CODE
            ];
            yield return
            [
                new { resourceType = "Doesnotexist", crap = 5 }, JsonTokenType.EndObject, COVE.UNKNOWN_RESOURCE_TYPE_CODE, COVE.UNKNOWN_ELEMENT_CODE
            ];
            yield return
            [
                new { resourceType = nameof(OperationOutcome), crap = 5 }, JsonTokenType.EndObject,
                COVE.UNKNOWN_ELEMENT_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE
            ];
            yield return
            [
                new { resourceType = nameof(Meta) }, JsonTokenType.EndObject, 
                COVE.UNKNOWN_RESOURCE_TYPE_CODE, ERR.OBJECTS_CANNOT_BE_EMPTY_CODE, COVE.ELEMENT_CANNOT_BE_EMPTY_CODE
            ];
            yield return
            [
                new { resourceType = "Patient", deceasedDateTime = "2022-05" }, JsonTokenType.EndObject
            ];
            yield return
            [
                new
                {
                    resourceType = "Patient",
                    deceasedDateTime = "2022-05",
                    _deceasedDateTime =
                        new { extension = new object[] { new { url = "test", valueString = "Smile" } } }
                },
                JsonTokenType.EndObject
            ];
        }
    }

    public static IEnumerable<object[]> TestDeserializeNestedResource
    {
        get
        {
            yield return
            [
                new
                {
                    resourceType = "Parameters",
                    parameter = new[]
                    {
                        new { name = "a", resource = new { resourceType = "Patient", active = true } }
                    }
                },
                JsonTokenType.EndObject
            ];
        }
    }

    [TestMethod]
    [DynamicData(nameof(TestPrimitiveArrayData))]
    [DynamicData(nameof(CatchesIncorrectlyStructuredComplexData))]
    [DynamicData(nameof(TestNormalArrayData))]
    [DynamicData(nameof(TestPrimitiveData))]
    [DynamicData(nameof(TestValidatePrimitiveData))]
    public void TestData(Type t, object testObject, JsonTokenType token, Action<object?>? verify,
        params string[] expectedErrors)
    {
        // Enable full narrative validation so we can test for it
        var (result, errors) = deserializeComplex(t, testObject, out var readerState,
            new FhirJsonConverterOptions
            {
                NarrativeValidation = NarrativeValidationKind.FhirXhtml
            });

        errors.Select(err => err.ErrorCode).Should().BeEquivalentTo(expectedErrors);
        readerState.TokenType.Should().Be(token);
        result.Should().BeOfType(t);
        verify?.Invoke(result);
    }

    [TestMethod]
    public void TestSingleData()
    {
        var testData = new { url = "http://nu.nl", value = "blabla" };
        string[] expectedErrors = [ERR.CHOICE_ELEMENT_MUST_HAVE_SUFFIX_CODE, COVE.CHOICE_TYPE_NOT_ALLOWED_CODE];
        var (result, errors) = deserializeComplex(typeof(Extension), testData, out var readerState,
            new FhirJsonConverterOptions());
        
        errors.Select(err => err.ErrorCode).Should().BeEquivalentTo(expectedErrors);
    }

    private static object?[] data<T>(object data, Action<object> verifier, params object[] args) =>
        new[] { typeof(T), data, JsonTokenType.EndObject, verifier }.Concat(args).ToArray();

    private static object?[] data<T>(object data, JsonTokenType token, params object[] args) =>
        new[] { typeof(T), data, token, null }.Concat(args).ToArray();

    private static object?[] data<T>(object data, params object[] args) =>
        new[] { typeof(T), data, JsonTokenType.EndObject, null }.Concat(args).ToArray();


    public static IEnumerable<object?[]> CatchesIncorrectlyStructuredComplexData()
    {
        yield return data<Extension>(new { }, ERR.OBJECTS_CANNOT_BE_EMPTY_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE, COVE.ELEMENT_CANNOT_BE_EMPTY_CODE);
        yield return data<Extension>(new { unknown = "test" }, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE, COVE.UNKNOWN_ELEMENT_CODE);
        yield return data<Extension>(new { url = "test" });
        yield return data<Extension>(new { _url = "test" }, ERR.UNDERSCORE_SHOULD_BE_OBJECT_CODE); // No other errors, since we're setting url to value anyway.
        yield return data<Extension>(new { unknown = "test", url = "test" }, COVE.UNKNOWN_ELEMENT_CODE);
        yield return data<Extension>(new { value = "no type suffix" }, ERR.CHOICE_ELEMENT_MUST_HAVE_SUFFIX_CODE, COVE.CHOICE_TYPE_NOT_ALLOWED_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
        yield return data<Extension>(new { valueUnknown = "incorrect type suffix" },
            COVE.CHOICE_TYPE_NOT_ALLOWED_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
        yield return data<Extension>(new { valueBoolean = true, url = "http://something.nl" }, JsonTokenType.EndObject);
        yield return data<Extension>(new { valueUnknown = "incorrect type suffix", unknown = "unknown" },
            COVE.CHOICE_TYPE_NOT_ALLOWED_CODE, COVE.UNKNOWN_ELEMENT_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
    }

    public static IEnumerable<object?[]> TestNormalArrayData()
    {
        yield return data<ContactDetail>(new { name = "Ewout", telecom = 4 }, checkName, ERR.UNEXPECTED_PRIMITIVE_VALUE_FOR_NON_PRIMITIVE_CODE, COVE.PROPERTY_TYPE_MISMATCH_CODE);
        yield return data<ContactDetail>(new { name = "Ewout", telecom = Array.Empty<object>() }, checkName, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE);
        yield return data<ContactDetail>(
            new { name = "Ewout", telecom = new object[] { new { system = "phone" }, new { systemX = "b" } } },
            checkData,  COVE.UNKNOWN_ELEMENT_CODE);
        yield return data<ContactDetail>(
            new { name = "Ewout", _telecom = new object[] { new { system = "phone" }, new { systemX = "b" } } },
            checkData, ERR.USE_OF_UNDERSCORE_WITH_NON_PRIMITIVE_CODE, COVE.UNKNOWN_ELEMENT_CODE);
        yield return data<ContactDetail>(new { name = new[] { "Ewout" }, }, COVE.PROPERTY_TYPE_MISMATCH_CODE);
        yield return data<ContactDetail>(new { telecom = new { system = "phone" } }, COVE.PROPERTY_TYPE_MISMATCH_CODE);

        static void checkName(object parsed) =>
            parsed.Should().BeOfType<ContactDetail>().Which.Name.Should().Be("Ewout");

        static void checkData(object parsedObject)
        {
            checkName(parsedObject);

            var parsed = parsedObject.Should().BeOfType<ContactDetail>().Subject;
            parsed.Telecom.Count.Should().Be(2);
            parsed.Telecom[0].System.Should().Be(ContactPoint.ContactPointSystem.Phone);
            parsed.Telecom[1].EnumerateElements().Count().Should().Be(1);
        }
    }

    public static IEnumerable<object?[]> TestPrimitiveData()
    {
        yield return data<ContactDetail>(new { name = new[] { "Ewout" } }, COVE.PROPERTY_TYPE_MISMATCH_CODE);
        yield return data<ContactDetail>(new { name = new { dummy = "Ewout" } }, ERR.UNEXPECTED_OBJECT_VALUE_FOR_PRIMITIVE_CODE, COVE.UNKNOWN_ELEMENT_CODE);
        yield return data<ContactDetail>(new { _name = new[] { "Ewout" } }, ERR.UNDERSCORE_SHOULD_BE_OBJECT_CODE,COVE.PROPERTY_TYPE_MISMATCH_CODE);
        yield return data<ContactDetail>(new { _name = "Ewout" }, ERR.UNDERSCORE_SHOULD_BE_OBJECT_CODE);
        yield return data<ContactDetail>(new { name = "Ewout" }, checkName);
        yield return data<ContactDetail>(new { _name = new { id = "12345" } }, checkId);
        yield return data<ContactDetail>(new { _name = new { id = true } }, COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE);
        yield return data<ContactDetail>(new { name = "Ewout", _name = new { id = "12345" } }, checkAll);

        static void checkName(object parsed) => parsed.Should().BeOfType<ContactDetail>().Which.NameElement!.Value
            .Should().Be("Ewout");
        
        static void checkId(object parsed) => parsed.Should().BeOfType<ContactDetail>().Which.NameElement!.ElementId
            .Should().Be("12345");
        
        static void checkAll(object parsed)
        {
            checkName(parsed);
            checkId(parsed);
        }
    }

    public static IEnumerable<object?[]> TestValidatePrimitiveData()
    {
        yield return data<Narrative>(new
        {
            div = "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p>correct</p></div>", status = "additional"
        });
        yield return data<Narrative>(new { div = "this isn't xml" }, COVE.NARRATIVE_XML_IS_MALFORMED_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
        yield return data<Narrative>(new { div = "<puinhoop />" }, COVE.NARRATIVE_XML_IS_INVALID_CODE, COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
        yield return data<Narrative>(new { div = "<puinhoop />", status = "generated" }, COVE.NARRATIVE_XML_IS_INVALID_CODE);

        yield return data<Attachment>(new { url = "urn:oid:1.3.6.1.4.1.343" });
        //   yield return data<Attachment>(new { url = "urn:oid:1" }, COVE.URI_LITERAL_INVALID_CODE);
        // This is an URL in our datamodel now, since that's what it is in R4 and later.
    }

    public static IEnumerable<object?[]> TestPrimitiveArrayData()
    {
        yield return data<Address>(new { line = "hi!" }, COVE.PROPERTY_TYPE_MISMATCH_CODE); // expected collection of string, found string
        yield return data<Address>(new { line = Array.Empty<string>() }, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, COVE.ELEMENT_CANNOT_BE_EMPTY_CODE);
        yield return data<Address>(new { line = Array.Empty<string>(), _line = Array.Empty<string>() },
            ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, COVE.ELEMENT_CANNOT_BE_EMPTY_CODE);
        yield return data<Address>(new { line = Array.Empty<string>(), _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null }, _line = new[] { new { id = "1" } } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { _line = new[] { new { id = "1" } } });
        yield return data<Address>(new { line = new[] { "Ewout" }, _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null }, _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null }, _line = new string?[] { null, null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null, null }, _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE);
        yield return data<Address>(new { line = new[] { "Ewout", "Wouter" } }, checkName);
        yield return data<Address>(
            new { line = new[] { "Ewout", "Wouter" }, _line = new[] { new { id = "1" } } }, checkId1AndName);
        yield return data<Address>(
            new { line = new[] { "Ewout", "Wouter" }, _line = new[] { new { id = "1" }, null } }, checkId1AndName);
        yield return data<Address>(
            new { line = new[] { "Ewout", "Wouter" }, _line = new[] { new { id = "1" }, new { id = "2" } } },
            checkAll);
        yield return data<Address>(new
        {
            line = new[] { "Ewout", null }, _line = new[] { null, new { id = "2" } }
        });
        yield return data<Address>(
            new { line = new[] { "Ewout", null }, _line = new[] { new { id = "1" }, null } }, checkId1,
            COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE);
        yield return data<Address>(new { _line = new[] { new { id = "1" }, null } }, checkId1,
            COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE);
        yield return data<Address>(new { _line = new[] { new { id = "1" }, new { id = "2" } } }, checkIds);
        yield return data<HumanName>(new
        {
            _given = new[]
            {
                null,
                new { id = "a3", extension = new[] { new { url = "http://nu.nl", valueString = "test" } } },
                null
            },
            given = new[] { "Benedicte", "Denise", "Marie" },
        }, checkIdsName);

        static void checkName(object parsed) => parsed.Should().BeOfType<Address>().Which.Line.Should()
            .BeEquivalentTo("Ewout", "Wouter");

        static void checkIds(object parsed) =>
            parsed.Should().BeOfType<Address>().Which.LineElement.Select(le => le?.ElementId).Should()
                .BeEquivalentTo("1", "2");

        static void checkIdsName(object parsed) =>
            parsed.Should().BeOfType<HumanName>().Which.GivenElement.Select(ge => ge.ElementId).Should()
                .BeEquivalentTo(default(string), "a3", null);

        static void checkId1(object parsed) =>
            parsed.Should().BeOfType<Address>().Which.LineElement.Select(le => le?.ElementId).Should()
                .BeEquivalentTo("1", null);

        static void checkId1AndName(object parsed)
        {
            checkName(parsed);
            checkId1(parsed);
        }

        static void checkAll(object parsed)
        {
            checkName(parsed);
            checkIds(parsed);
        }
    }

    [TestMethod]
    public void TestParseResourcePublicMethod()
    {
        var deserializer = new BaseFhirJsonDeserializer(ModelInspector.Base);
        var reader = constructReader(
            new { resourceType = "Parameters", parameter = new[] { new { name = "a" } } });

        deserializer.DeserializeResource(ref reader).Should().NotBeNull();

        reader = constructReader(
            new { resourceType = "ParametersX", });

        try
        {
            deserializer.DeserializeResource(ref reader);
            Assert.Fail();
        }
        catch (DeserializationFailedException)
        {
            // ok!
        }
    }

    [TestMethod]
    public void TestParseObjectPublicMethod()
    {
        var deserializer = new BaseFhirJsonDeserializer(ModelInspector.Base);
        var reader = constructReader(
            new { name = "Ewout" });

        deserializer.Deserialize<ContactDetail>(ref reader).Should().NotBeNull();

        reader = constructReader(
            new { nameX = "Ewout", });

        try
        {
            deserializer.Deserialize<ContactDetail>(ref reader);
            Assert.Fail();
        }
        catch (DeserializationFailedException)
        {
            // ok!
        }

        try
        {
            deserializer.DeserializeObject(typeof(FhirJsonDeserializationTests), ref reader);
            Assert.Fail();
        }
        catch (ArgumentException)
        {
            // ok!
        }
    }

    [TestMethod]
    public async T.Task SerializingErroneousResource_Should_ThrowExpectedErrors()
    {
        var patientFileName = Path.Combine("TestData", "fp-test-patient-errors.json");
        var jsonInput = File.ReadAllText(patientFileName);

        var options = new JsonSerializerOptions().ForFhir().Pretty();

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(jsonInput, options);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            var recoveredActual = JsonSerializer.Serialize(dfe.PartialResult, options);
            var errorsActual = dfe.Exceptions
                .OrderBy(e => e is ExtendedCodedException ece ? ece.LineNumber : 0);

            await _verifier.Verify(new { Errors = errorsActual, Obj = recoveredActual });
        }
    }

    [TestMethod]
    public void MakeSureVersionSpecificTypedPropertiesGetCreatedOk()
    {
        var pat = new Patient()
        {
            Meta = new Meta()
            {
                VersionId = "1",
                ProfileElement = [new FhirUri("http://nu.nl")]
            }
        };

        var content = pat.ToJson();
        var pat2 = FhirJsonDeserializer.DEFAULT.DeserializeResource(content);

        // If we deserialize the profile incorrectly due to AllowedTypes etc,
        // it will end up in the overflow and this will crash.
        pat2.Meta!.Profile.Should().HaveCount(1);
    }

    [TestMethod]
    public void TestBase64Parsing()
    {
        var attachment = deserializeAttachment(new FhirJsonConverterOptions());

        // After parsing, the ObjectValue is supposed to be the base64 string
        attachment.DataElement!.JsonValue.Should().BeOfType<string>().And.Subject.Should().Be("SGkh");

        // Getting the Value should decode and return a byte[]
        Encoding.UTF8.GetString(attachment.Data!).Should().Be("Hi!");

        static Attachment deserializeAttachment(FhirJsonConverterOptions settings)
        {
            var (attachment, errors) =
                deserializeComplex(typeof(Attachment), new { data = "SGkh" }, out _, settings);
            errors.Any().Should().BeFalse();

            return (Attachment)attachment!;
        }
    }

    [TestMethod]
    public void JsonDeserializerSupportsParsingUnknownTypesAndProperties()
    {
        var parser = new BaseFhirJsonDeserializer(ModelInspector.Base);

        var dt = DateTimeOffset.UtcNow;
        
        Utf8JsonReader reader = constructReader(new { resourceType = "Unknown", id = "TestIdentifier", body = new[] { "Test" }, testBool = true, valueDateTime = dt, testDec = 123.4, testInt = 999});

         parser.TryDeserializeResource(ref reader, out var obj, out var errors);

        obj.Should().NotBeNull();
        obj!.Id.Should().Be("TestIdentifier");
        obj["body"].Should().BeEquivalentTo(new List<DynamicPrimitive> { new(){DynamicTypeName = "DynamicPrimitive", JsonValue = "Test"} });
        obj["testBool"].Should().BeEquivalentTo(new DynamicPrimitive(){DynamicTypeName = "DynamicPrimitive", JsonValue = true});
        obj["testDec"].Should().BeEquivalentTo(new DynamicPrimitive(){DynamicTypeName = "DynamicPrimitive", JsonValue = 123.4});
        obj["testInt"].Should().BeEquivalentTo(new DynamicPrimitive(){DynamicTypeName = "DynamicPrimitive", JsonValue = 999});
        obj["valueDateTime"].Should().BeEquivalentTo(new DynamicPrimitive(){DynamicTypeName = "DynamicPrimitive", JsonValue = dt.ToFhirDateTime()});
    }

    [TestMethod]
    public async T.Task JsonDeserializerSupportsUnknownPropertiesOnKnownTypes()
    {
        var parser = new FhirJsonDeserializer();

        var dt = DateTimeOffset.UtcNow;
        
        Utf8JsonReader reader = constructReader(new 
        {
            resourceType = "Patient", 
            id = "TestIdentifier",
            active = new[] { true, false },
            telecom = new{ system = "phone", value = "magicnumber"},
            communication = "en",
            name = "Test",
        });

        parser.TryDeserializeResource(ref reader, out var obj, out var errors);
        obj.Should().NotBeNull();

        await _verifier.Verify(new { Errors = errors, Obj = obj.ToJson(pretty:true) });
    }
    
    [TestMethod]
    public async T.Task JsonDeserializerHandleUnexpectedObject()
    {
        var parser = new FhirJsonDeserializer();

        var test = new
        {
            resourceType = "Patient",
            active = new { value = true }, // Expected a primitive, got an object
            valueQuantity = new
            {
                value = new { amount = 73 }, // Expected a number, got an object
                unit = "bpm"
            }
        };

        Utf8JsonReader reader = constructReader(test);

        parser.TryDeserializeResource(ref reader, out var obj, out var errors);

        await _verifier.Verify(new { Errors = errors, Obj = obj?.ToJson(pretty: true) });
    }

    [TestMethod]
    public void JsonDeserializerHandleContainedStuff()
    {
        var parser = new FhirJsonDeserializer();

        var test = new
        {
            resourceType = "Patient",
            id = "patient",
            name = new []{ new { Family = "Doe", Given = new[] { "John" } } },
            contained = new[]
            {
                new { resourceType = "Medication", id = "medication", code = "1234" }
            }
        };

        Utf8JsonReader reader = constructReader(test);

        parser.TryDeserializeResource(ref reader, out var obj, out var errors);
        
        obj.Should().NotBeNull();
        obj!.TypeName.Should().Be("Patient");
        (obj as Patient)!.Contained.Should().HaveCount(1).And.Subject.Should().Satisfy(x => x.TypeName == "Medication");
    }

    
    internal class CustomComplexValidator : FhirAttributeValidator
    {
        public FhirDateTime? DateTimeSeenByInstanceValidator;
        public FhirDateTime? DateTimeSeenByPropertyValidator;

        public override IReadOnlyCollection<COVE> ValidateObject(Base instance, ClassMapping classMapping, PocoValidationContext context)
        {
            if (instance is FhirDateTime fdt)
            {
                DateTimeSeenByInstanceValidator = fdt;
                fdt.JsonValue = "1972-11-30T12:00:00Z";
            }

            return base.ValidateObject(instance, classMapping, context);
        }

        public override IReadOnlyCollection<COVE> ValidateProperty(string name, object? propertyValue, PropertyMapping? propertyMapping, PocoValidationContext context)
        {
            var reportedErrors = base.ValidateProperty(name, propertyValue, propertyMapping, context);

            if (context.PathProducer.Invoke() == "Patient.deceased")
            {
                var fdt = propertyValue.Should().BeOfType<FhirDateTime>().Subject;

                // Take note of what we got.
                DateTimeSeenByPropertyValidator = fdt;

                return [..reportedErrors, COVE.LITERAL_INVALID(context, "Nothing wrong, really", "DateTime")];
            }

            return reportedErrors;
        }
    }

    [TestMethod]
    public void TestValidatorIsCalledDuringDeserialization()
    {
        var validator = new CustomComplexValidator();

        var (_, errors) = deserializeComplex(typeof(Patient),
            new { resourceType = "Patient", deceasedDateTime = "2070-01-01T12:01:02Z" },
            out _, new FhirJsonConverterOptions { Validator = validator });

        errors.Should().HaveCount(1);
        errors.Single().Should().BeOfType<COVE>().Which.ErrorCode.Should().Be(COVE.LITERAL_INVALID_CODE);
        validator.DateTimeSeenByInstanceValidator?.Value.Should().Be("1972-11-30T12:00:00Z");
    }

    private class MixedClass
    {
        public Patient? FhirPatient { get; init; }

        public string? HandledByTextJson { get; init; }

        // This only works well when we construct deserializers using the ConverterFactory method
        // from System.Text.Json
        public List<Identifier>? FhirIdentifier { get; init; }
    }


    [TestMethod]
    public void CanParseIsolatedDataType()
    {
        var reader = constructReader(new { system = "http://nu.nl", value = "bla" });

        var options = new JsonSerializerOptions().ForFhir();

        var identifier = JsonSerializer.Deserialize<Identifier>(ref reader, options)!;
        identifier.Should().BeOfType<Identifier>();
        identifier.System.Should().Be("http://nu.nl");
    }

    [TestMethod]
    public void CanParseMixedClass()
    {
        var options = new JsonSerializerOptions().ForFhir();

        var mc = new MixedClass
        {
            FhirIdentifier = new() { new Identifier("http://nu.nl", "bla") },
            HandledByTextJson = "Hi!",
            FhirPatient = new() { Active = true }
        };

        var json = JsonSerializer.Serialize(mc, options);

        var mc2 = JsonSerializer.Deserialize<MixedClass>(json, options)!;

        mc2.Should().BeOfType<MixedClass>();
        mc2.FhirIdentifier!.Single().System.Should().Be("http://nu.nl");
        mc2.HandledByTextJson.Should().Be("Hi!");
        mc2.FhirPatient?.Active.Should().Be(true);
    }

    [TestMethod]
    public void ReportsMissingMandatoryElements()
    {
        var (codesystem, errors) = deserializeComplex(typeof(CodeSystem),
            new { resourceType = "CodeSystem", content = "example" }, out _, new());

        // should contain error that mandatory item "status" is missing.
        errors.Should().ContainSingle(ce => ce.ErrorCode == "PVAL105");
    }


    private static IEnumerable<object[]> getDuplicatePropertyTests()
    {
        (string, string[])[] duplicatePropertiesJson =
        [
            ("""
             {
              "resourceType" : "Patient",
              "extension" : [{
              "url" : "http://nu.nl",
              "url" : "http://nu.nl"
              }]
              }
             """, [ERR.DUPLICATE_PROPERTY_CODE]),
            ("""
             {
              "resourceType" : "Patient",
              "active" : true,
              "active" : false
             }
             """, [ERR.DUPLICATE_PROPERTY_CODE]),
            ("""
             {
                 "resourceType" : "Patient",
                 "active" : true,
                 "_active" : { "id" : "1234" },
                 "_active" : { "id" : "5678" }
             }
             """, [ERR.DUPLICATE_PROPERTY_CODE]),
            (
                """
                {
                   "resourceType" : "Patient",
                   "_active" : { "id" : "1234" },
                   "_active" : { "id" : "5678" }
                }
                """, [ERR.DUPLICATE_PROPERTY_CODE]),
            (
                """
                {
                   "resourceType" : "Patient",
                   "_active" : { "id" : "1234" },
                   "_active" : { "extension" : [{ "url" : "http://nu.nl" }] }
                }
                """, [ERR.DUPLICATE_PROPERTY_CODE]),
            ("""
             {
               "resourceType" : "OperationOutcome",
               "issue" : [{
                 "severity" : "error",
                 "code" : "code-invalid",
                 "expression" : ["Patient.gender"],
                 "_expression" : [{ "id" : "1234" }],
                 "_expression" : [{ "id" : "3456" }]
               }]
             }
             """, [ERR.DUPLICATE_PROPERTY_CODE]),
            ("""
             {
               "resourceType" : "OperationOutcome",
               "issue" : [{
                 "severity" : "error",
                 "code" : "code-invalid",
                 "expression" : ["Patient.gender"],
                 "expression" : ["Patient.administrativeGender"],
                 "_expression" : [{ "id" : "3456" }]
               }]
             }
             """, [ERR.DUPLICATE_PROPERTY_CODE]),
            ("""
             {
                 "resourceType" : "Patient",
                 "identifier" :
                 [{
                     "use" : "usual",
                     "system" : "urn:oid:2.16.840.1.113883.2.4.6.3",
                     "value" : "738472983"
                 }],
                 "identifier" :
                 [{
                     "use" : "usual",
                     "system" : "urn:oid:2.16.840.1.113883.2.4.6.3",
                     "value" : "738472983"
                 }]
             }
             """, [ERR.DUPLICATE_PROPERTY_CODE]),
            ("""
             {
                 "resourceType" : "Patient",
                 "managingOrganization" :
                 {
                     "reference" : "Organization/f001",
                     "display" : "Burgers University Medical Centre"
                 },
                 "managingOrganization" :
                 {
                     "reference" : "Organization/f002",
                     "display" : "Burgers Zoo"
                 }
             }
             """, [ERR.DUPLICATE_PROPERTY_CODE])
        ];

        return duplicatePropertiesJson.Select(testCase => (object[])( [testCase.Item1, testCase.Item2]));
    }


    [TestMethod]
    [DynamicData(nameof(getDuplicatePropertyTests))]
    public void TestDuplicateProperties(string testJson, string[] expectedErrs)
    {
        var options = new JsonSerializerOptions().ForFhir();

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(testJson, options);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            dfe.Exceptions.Select(ex => ex.ErrorCode).Should().BeEquivalentTo(expectedErrs);
        }
    }

    [TestMethod]
    public void TestDuplicateChoiceTypeEntries()
    {
        var scenario = """
                       {
                         "resourceType": "Patient",
                         "deceasedBoolean": true,
                         "deceasedDateTime": "2022-01-01T12:00:00Z"
                       }
                       """;

        string expected = ERR.DUPLICATE_PROPERTY_CODE;

        var jsonSerializerOptions = new JsonSerializerOptions().ForFhir();

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(scenario, jsonSerializerOptions);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            dfe.Exceptions.Select(ex => ex.ErrorCode).Should().BeEquivalentTo(expected);
        }
    }

    [TestMethod]
    public void TestBackboneElementEmptyStack()
    {
        var options = new JsonSerializerOptions().ForFhir().UsingMode(DeserializationMode.Ostrich);

        var bundleEntryComponent = new Parameters.ParameterComponent()
        {
            Name = "name",
            Resource = new Patient{Gender = AdministrativeGender.Female}
        };

        var jsonString = JsonSerializer.Serialize(bundleEntryComponent, options);

        var seq = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes(jsonString));

        var newJsonReader = new Utf8JsonReader(seq, true, default);

        // System.InvalidOperationException: 'Stack empty.' thrown when attempting to deserialize
        var result = JsonSerializer.Deserialize<Parameters.ParameterComponent>(ref newJsonReader, options);
    }

    private static IEnumerable<object[]> getExtensionOptionsAndExpectedErrors()
    {
        yield return
        [
            new JsonSerializerOptions().ForFhir()
                .UsingMode(DeserializationMode.Ostrich),
            new Predicate<IEnumerable<CodedException>>(errs => !errs.Any())
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir()
                .UsingMode(DeserializationMode.Recoverable),
            new Predicate<IEnumerable<CodedException>>(errs => !errs.Any(e => CodedExceptionFilters.FilterRecoverableIssues(e)))
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir()
                .UsingMode(DeserializationMode.BackwardsCompatible),
            new Predicate<IEnumerable<CodedException>>(errs => !errs.Any(e => CodedExceptionFilters.FilterBackwardsCompatibilityIssues(e)))
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir()
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]),
            new Predicate<IEnumerable<CodedException>>(errs => errs.All(e => e.ErrorCode != COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE))
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir().UsingMode(DeserializationMode.Ostrich)
                .Enforcing([ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, COVE.LITERAL_INVALID_CODE]),
            new Predicate<IEnumerable<CodedException>>(errs =>
            {
                IEnumerable<CodedException> codedExceptions = errs as CodedException[] ?? errs.ToArray();
                return codedExceptions.Any() && codedExceptions.All(e =>
                    e.ErrorCode is ERR.ARRAYS_CANNOT_BE_EMPTY_CODE or COVE.LITERAL_INVALID_CODE);
            })
        ];
    }

    [TestMethod]
    [DynamicData(nameof(getExtensionOptionsAndExpectedErrors))]
    public void TestExtensionMethods(JsonSerializerOptions options, Predicate<IEnumerable<CodedException>> shouldHold)
    {
        string testJson = File.ReadAllText(Path.Combine("TestData", "fp-test-patient-errors.json"));

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(testJson, options);
            throw new DeserializationFailedException(null, []);
        }
        catch (DeserializationFailedException dfe)
        {
            shouldHold(dfe.Exceptions).Should().BeTrue();
        }
    }

    private static Predicate<CodedException> getPredicateFromOptions(JsonSerializerOptions options)
    {
        var factory = options.Converters.FindCustomConverter() as FhirJsonConverterFactory ?? throw new InvalidOperationException();
        return factory.CurrentOptions.ExceptionFilter!;
    }

    private static IEnumerable<object[]> getIgnoreEnforceTests()
    {
        yield return
        [
            getPredicateFromOptions(new JsonSerializerOptions()
                .ForFhir()
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Ignoring([ERR.ARRAYS_CANNOT_BE_EMPTY_CODE])
                .Ignoring([COVE.INVALID_BASE64_VALUE_CODE])),
            new Predicate<CodedException>(ce =>
                ce.ErrorCode is COVE.INVALID_BASE64_VALUE_CODE or ERR.ARRAYS_CANNOT_BE_EMPTY_CODE
                    or COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE)
        ];
        yield return
        [
            getPredicateFromOptions(new JsonSerializerOptions()
                .ForFhir()
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])),
            new Predicate<CodedException>(_ => false)
        ];
        yield return
        [
            getPredicateFromOptions(new JsonSerializerOptions()
                .ForFhir()
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])),
            new Predicate<CodedException>(ce => ce.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE)
        ];
    }

    private static IEnumerable<CodedException> getErrorsList()
    {
        var testDeserializerOptions = new JsonSerializerOptions().ForFhir()
            .UsingMode(DeserializationMode.Strict);
        string testJson = File.ReadAllText(Path.Combine("TestData", "fp-test-patient-errors.json"));

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(testJson, testDeserializerOptions);
        }
        catch (DeserializationFailedException dfe)
        {
            return dfe.Exceptions;
        }

        throw new InvalidOperationException("Should have encountered errors");
    }


    [TestMethod]
    [DynamicData(nameof(getIgnoreEnforceTests))]
    public void TestIgnoreEnforcePrevalence(Predicate<CodedException> actual, Predicate<CodedException> expected)
    {
        var errors = getErrorsList();

        foreach (var err in errors) (actual(err) == expected(err)).Should().BeTrue(); // test if predicates are equivalent
    }


    [TestMethod]
    public void TestInvalidCustomization()
    {
        var shouldThrow = () => (_ = new JsonSerializerOptions().UsingMode(DeserializationMode.Ostrich));
        shouldThrow.Should().Throw<NotSupportedException>("Expected error trying to set the mode of a non-existent converter");
    }

    [TestMethod]
    public void CanParseAndSerializeCustomProperty()
    {
        var inspector = new ModelInspector(FhirRelease.STU3);
        inspector.Import(typeof(Patient).Assembly);
        inspector.Import(typeof(Base).Assembly);

        var parser = new BaseFhirJsonDeserializer(inspector, new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));

        const string json = """
                            { 
                                "resourceType": "Patient", 
                                "active": true, 
                                "patientLocation": "http://nu.nl",
                                "newList": ["singleitem"],
                                "remarksString": "Nice guy"
                            }
                            """;

        var parsed = parser.DeserializeResource(json).Should().BeOfType<Patient>().Subject;
        parsed.Active.Should().BeTrue();
        parsed["active"].Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
        parsed["patientLocation"].Should().BeOfType<DynamicPrimitive>().Which.Value.Should().Be("http://nu.nl");
        parsed["newList"].Should().BeOfType<List<DynamicPrimitive>>().Which.Single().Value.Should().Be("singleitem");
        parsed["remarksString"].Should().BeOfType<DynamicPrimitive>().Which.Value.Should().Be("Nice guy");

        var patientMapping = inspector.FindClassMapping(typeof(Patient))!;
        var customPropertyA = new PropertyMapping(patientMapping, "patientLocation", typeof(FhirUri));
        patientMapping.PropertyMappings.Add(customPropertyA);
        var customPropertyB = new PropertyMapping(patientMapping, "remarks", typeof(DataType), [typeof(FhirString), typeof(Markdown)]);
        patientMapping.PropertyMappings.Add(customPropertyB);
        var customPropertyC = new PropertyMapping(patientMapping, "newList", typeof(List<FhirString>));
        patientMapping.PropertyMappings.Add(customPropertyC);

        parsed = parser.DeserializeResource(json).Should().BeOfType<Patient>().Subject;
        parsed.Active.Should().BeTrue();
        parsed["patientLocation"].Should().BeOfType<FhirUri>().Which.Value.Should().Be("http://nu.nl");
        parsed["remarks"].Should().BeOfType<FhirString>().Which.Value.Should().Be("Nice guy");
        parsed["newList"].Should().BeOfType<List<FhirString>>().Which.Single().Value.Should().Be("singleitem");

        var serializer = new BaseFhirJsonSerializer(inspector);
        var serialized = serializer.SerializeToString(parsed);
        JsonAssert.AreSame("parsed", json, serialized);
    }

// This test is only relevant when we have getter/setter codegen enabled. - See PropertyMapping's
// Getter and Setter, which are also excluded by this preprocessor directive.
#if USE_GETTER_SETTER_AND_CODEGEN

    [TestMethod]
    public void CanAccessPropertiesViaPropertyMapping()
    {
        var inspector = new ModelInspector(FhirRelease.STU3);
        inspector.Import(typeof(Patient).Assembly);
        inspector.Import(typeof(Base).Assembly);

        var patientMapping = inspector.FindClassMapping(typeof(Patient))!;
        var patientLocPm = new PropertyMapping(patientMapping, "patientLocation", typeof(FhirUri));
        patientMapping.PropertyMappings.Add(patientLocPm);

        var parser = new BaseFhirJsonDeserializer(inspector,
            new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));

        const string json = """
                            { 
                                "resourceType": "Patient", 
                                "active": true, 
                                "patientLocation": "http://nu.nl",
                            }
                            """;

        var parsed = parser.DeserializeResource(json).Should().BeOfType<Patient>().Subject;

        var activePm = patientMapping.FindMappedElementByName("active")!;
        activePm.Getter(parsed).Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
        activePm.Setter(parsed, new FhirBoolean(false));
        parsed.Active.Should().BeFalse();

        patientLocPm.Getter(parsed).Should().BeOfType<FhirUri>().Which.Value.Should().Be("http://nu.nl");
        patientLocPm.Setter(parsed, new FhirUri("there"));
        parsed["patientLocation"].Should().BeOfType<FhirUri>().Which.Value.Should().Be("there");
    }

#endif
}