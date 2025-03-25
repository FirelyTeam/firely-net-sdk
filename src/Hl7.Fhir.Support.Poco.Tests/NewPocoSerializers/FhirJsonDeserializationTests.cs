using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;
using FhirJsonConverterFactory = Hl7.Fhir.Serialization.FhirJsonConverterFactory;

#nullable enable

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
public class FhirJsonDeserializationTests
{
    [TestMethod]
    public void PrimitiveValueCannotBeComplex()
    {
        ParsePrimitiveValue(new { bla = 4 }, typeof(FhirBoolean), ERR.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE);
    }

    [DataTestMethod]
    [DataRow("OperationOutcome", null)]
    [DataRow("Meta", null)]
    [DataRow(4, ERR.RESOURCETYPE_SHOULD_BE_STRING_CODE)]
    [DataRow(null, ERR.NO_RESOURCETYPE_PROPERTY_CODE)]
    public void DeriveClassMapping(object? typename, string? errorcode)
    {
        var (result, error) = test(typename);
        if (errorcode is null)
            error.Should().BeNull();
        else
            error?.ErrorCode.Should().Be(errorcode);

        if (errorcode is null)
            result!.Name.Should().Be((string?)typename);

        static (ClassMapping?, FhirJsonException?) test(object? typename)
        {
            var inspector = ModelInspector.ForAssembly(typeof(Resource).Assembly);

            var jsonBytes = typename != null
                ? JsonSerializer.SerializeToUtf8Bytes(new { resourceType = typename })
                : JsonSerializer.SerializeToUtf8Bytes(new { resourceTypeX = "wrong" });
            var reader = new Utf8JsonReader(jsonBytes);
            reader.Read();

            var ps = new PathStack();
            ps.EnterElement("Patient", 0, false);
            return BaseFhirJsonDeserializer.DetermineClassMappingFromInstance(ref reader, inspector, ps);
        }
    }

    [DataTestMethod]
    [DataRow(null, typeof(FhirString), ERR.EXPECTED_PRIMITIVE_NOT_NULL_CODE)]
    [DataRow(new[] { 1, 2 }, typeof(FhirString), ERR.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE)]
    [DataRow("SGkh", typeof(FhirString), null, "SGkh")]
    [DataRow(4, typeof(FhirString), COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, 4)]
    [DataRow("SGkh", typeof(Base64Binary), null, "SGkh")]
    [DataRow("hi!", typeof(Base64Binary), COVE.INVALID_BASE64_VALUE_CODE, "hi!")]
    [DataRow(4, typeof(Base64Binary), COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, 4)]
    [DataRow("2007-04", typeof(FhirDateTime), null, "2007-04")]
    [DataRow("", typeof(FhirDateTime), ERR.PROPERTY_MAY_NOT_BE_EMPTY_CODE, null)]
    [DataRow("2007-", typeof(FhirDateTime), COVE.LITERAL_INVALID_CODE, "2007-")]
    [DataRow(true, typeof(FhirDateTime), COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, true)]
    [DataRow("female", typeof(Code), null, "female")]
    [DataRow("is-a", typeof(Code<FilterOperator>), null, "is-a")]
    [DataRow("wrong", typeof(Code<FilterOperator>), COVE.INVALID_CODED_VALUE_CODE,
        "wrong")] // just sets ObjectValue, POCO validation handles enum checks
    [DataRow(true, typeof(Code), COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, true)]
    [DataRow("hi!", typeof(Instant), COVE.LITERAL_INVALID_CODE)]
    [DataRow("2007-02-03T12:00:00Z", typeof(Instant), null, "2007-02-03T12:00:00Z")]
    [DataRow(3, typeof(FhirDecimal), null, 3)]
    [DataRow(3.14, typeof(FhirDecimal), null, 3.14)]
    [DataRow(3L, typeof(Integer64), COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE)]
    [DataRow("hoi", typeof(Integer64), COVE.LITERAL_INVALID_CODE)]
    [DataRow("3", typeof(Integer64), null, "3")]
    [DataRow(314, typeof(Integer), null, 314)]
    [DataRow(3.14, typeof(FhirBoolean), COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE)]
    [DataRow(true, typeof(FhirBoolean), null, true)]
    public void ParsePrimitiveValue(object value, Type targetType, string? errorcode,
        object? expectedObjectValue = null)
    {
        var state = new FhirJsonPocoDeserializerState();

        PrimitiveType test()
        {
            var inspector = ModelInspector.ForType(typeof(Patient));
            var deserializer = new BaseFhirJsonDeserializer(inspector);
            var mapping = inspector.ImportType(targetType)!;

            var reader = constructReader(value);
            reader.Read();

            return deserializer.DeserializeFhirPrimitive(null, "dummy", mapping, null!, ref reader, null, state);
        }

        var result = test();

        if (state.Errors.HasExceptions)
        {
            if (errorcode is not null)
                state.Errors.Should().OnlyContain(ce => ce.ErrorCode == errorcode);
            else
                errorcode.Should().BeNull(because: state.Errors.ToString());
        }
        else
        {
            errorcode.Should().BeNull(because: state.Errors.ToString());
        }

        if (expectedObjectValue is not null)
            result.ObjectValue.Should().BeEquivalentTo(expectedObjectValue);
    }

    private static (Base?, IReadOnlyCollection<CodedException>) deserializeComplex(Type objectType,
        object testObject, out Utf8JsonReader readerState,
        FhirJsonConverterOptions settings)
    {
        // For the tests, enable full XHML validation so we can test it when necessary.
        var deserializer = new BaseFhirJsonDeserializer(ModelInspector.ForType<Patient>(), settings);
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

    private static void assertErrors(IEnumerable<CodedException> actual, string[] expected)
    {
        if (expected.Length == 0 && !actual.Any())
            return;

        string why =
            $"Should be the same: actual [{string.Join(",", actual.Select(a => a.ErrorCode))}] and expected [{string.Join(";", expected)}]";
        Console.WriteLine("Messages: " + string.Join(", ", actual.Select(a => a.Message)));
        actual.Count().Should().Be(expected.Length, because: why);
        _ = actual.Zip(expected, (a, e) => a.ErrorCode.Should().Be(e, because: why)).ToList();
        Console.WriteLine($"Found {string.Join(", ", actual.Select(a => a.Message))}");
    }

    [TestMethod]
    [DynamicData(nameof(TestDeserializeResourceData))]
    [DynamicData(nameof(TestDeserializeNestedResource))]
    public void TestDeserializeResource(object testObject, JsonTokenType tokenAfterParsing, params string[] errors)
    {
        var reader = constructReader(testObject);
        reader.Read();

        var deserializer = new BaseFhirJsonDeserializer(ModelInspector.ForType<Patient>());
        var state = new FhirJsonPocoDeserializerState();
        _ = deserializer.DeserializeResourceInternal(ref reader, state, stayOnLastToken: false);
        assertErrors(state.Errors, errors);
        reader.TokenType.Should().Be(tokenAfterParsing);
    }

    public static IEnumerable<object[]> TestDeserializeResourceData
    {
        get
        {
            yield return [5, JsonTokenType.Number, ERR.EXPECTED_START_OF_OBJECT_CODE];
            yield return [new { }, JsonTokenType.EndObject, ERR.NO_RESOURCETYPE_PROPERTY_CODE];
            yield return
            [
                new { resourceType = 4, crap = 4 }, JsonTokenType.EndObject,
                ERR.RESOURCETYPE_SHOULD_BE_STRING_CODE
            ];
            yield return
            [
                new { resourceType = "Doesnotexist", crap = 5 }, JsonTokenType.EndObject,
                ERR.UNKNOWN_RESOURCE_TYPE_CODE
            ];
            yield return
            [
                new { resourceType = nameof(OperationOutcome), crap = 5 }, JsonTokenType.EndObject,
                ERR.UNKNOWN_PROPERTY_FOUND_CODE
            ];
            yield return
            [
                new { resourceType = nameof(Meta) }, JsonTokenType.EndObject, ERR.OBJECTS_CANNOT_BE_EMPTY_CODE,
                ERR.RESOURCE_TYPE_NOT_A_RESOURCE_CODE
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
    [DynamicData(nameof(TestPrimitiveArrayData), DynamicDataSourceType.Method)]
    [DynamicData(nameof(CatchesIncorrectlyStructuredComplexData), DynamicDataSourceType.Method)]
    [DynamicData(nameof(TestNormalArrayData), DynamicDataSourceType.Method)]
    [DynamicData(nameof(TestPrimitiveData), DynamicDataSourceType.Method)]
    [DynamicData(nameof(TestValidatePrimitiveData), DynamicDataSourceType.Method)]
    public void TestData(Type t, object testObject, JsonTokenType token, Action<object?>? verify,
        params string[] expectedErrors)
    {
        // Enable full narrative validation so we can test for it
        var (result, errors) = deserializeComplex(t, testObject, out var readerState,
            new FhirJsonConverterOptions
            {
                NarrativeValidation = NarrativeValidationKind.FhirXhtml
            });

        assertErrors(errors, expectedErrors);
        readerState.TokenType.Should().Be(token);
        result.Should().BeOfType(t);
        verify?.Invoke(result);
    }

    private static object?[] data<T>(object data, Action<object> verifier, params object[] args) =>
        new[] { typeof(T), data, JsonTokenType.EndObject, verifier }.Concat(args).ToArray();

    private static object?[] data<T>(object data, JsonTokenType token, params object[] args) =>
        new[] { typeof(T), data, token, null }.Concat(args).ToArray();

    private static object?[] data<T>(object data, params object[] args) =>
        new[] { typeof(T), data, JsonTokenType.EndObject, null }.Concat(args).ToArray();


    public static IEnumerable<object?[]> CatchesIncorrectlyStructuredComplexData()
    {
        yield return
        [
            typeof(Extension), 5, JsonTokenType.Number, null,
            ERR.EXPECTED_START_OF_OBJECT_CODE
        ];
        yield return data<Extension>(5, JsonTokenType.Number, ERR.EXPECTED_START_OF_OBJECT_CODE);
        yield return data<Extension>(new[] { 2, 3 }, JsonTokenType.EndArray, ERR.EXPECTED_START_OF_OBJECT_CODE);
        yield return data<Extension>(new { }, ERR.OBJECTS_CANNOT_BE_EMPTY_CODE);
        yield return data<Extension>(new { }, ERR.OBJECTS_CANNOT_BE_EMPTY_CODE);
        yield return data<Extension>(new { unknown = "test" }, ERR.UNKNOWN_PROPERTY_FOUND_CODE);
        yield return data<Extension>(new { url = "test" });
        yield return data<Extension>(new { _url = "test" }, ERR.EXPECTED_START_OF_OBJECT_CODE);
        yield return data<Extension>(new { unknown = "test", url = "test" },
            ERR.UNKNOWN_PROPERTY_FOUND_CODE);
        yield return data<Extension>(new { value = "no type suffix" }, ERR.CHOICE_ELEMENT_HAS_NO_TYPE_CODE);
        yield return data<Extension>(new { valueUnknown = "incorrect type suffix" },
            ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE);
        yield return data<Extension>(new { valueBoolean = true, url = "http://something.nl" },
            JsonTokenType.EndObject);
        yield return data<Extension>(new { valueUnknown = "incorrect type suffix", unknown = "unknown" },
            ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE, ERR.UNKNOWN_PROPERTY_FOUND_CODE);
    }

    public static IEnumerable<object?[]> TestNormalArrayData()
    {
        yield return data<ContactDetail>(new { name = "Ewout", telecom = 4 }, checkName,
            ERR.EXPECTED_START_OF_ARRAY_CODE, ERR.EXPECTED_START_OF_OBJECT_CODE);
        yield return data<ContactDetail>(new { name = "Ewout", telecom = Array.Empty<object>() }, checkName,
            ERR.ARRAYS_CANNOT_BE_EMPTY_CODE);
        yield return data<ContactDetail>(
            new { name = "Ewout", telecom = new object[] { new { system = "phone" }, new { systemX = "b" } } },
            checkData, ERR.UNKNOWN_PROPERTY_FOUND_CODE);
        yield return data<ContactDetail>(
            new { name = "Ewout", _telecom = new object[] { new { system = "phone" }, new { systemX = "b" } } },
            checkData, ERR.USE_OF_UNDERSCORE_ILLEGAL_CODE, ERR.UNKNOWN_PROPERTY_FOUND_CODE);
        yield return data<ContactDetail>(new { name = new[] { "Ewout" } }, ERR.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE);

        static void checkName(object parsed) =>
            parsed.Should().BeOfType<ContactDetail>().Which.Name.Should().Be("Ewout");

        static void checkData(object parsedObject)
        {
            checkName(parsedObject);

            var parsed = parsedObject.Should().BeOfType<ContactDetail>().Subject;
            parsed.Telecom.Count.Should().Be(2);
            parsed.Telecom[0].System.Should().Be(ContactPoint.ContactPointSystem.Phone);
            parsed.Telecom[1].EnumerateElements().Count().Should().Be(0);
        }
    }

    public static IEnumerable<object?[]> TestPrimitiveData()
    {
        yield return data<ContactDetail>(new { name = new[] { "Ewout" } }, ERR.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE);
        yield return data<ContactDetail>(new { name = new { dummy = "Ewout" } },
            ERR.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE);
        yield return data<ContactDetail>(new { _name = new[] { "Ewout" } }, ERR.EXPECTED_START_OF_OBJECT_CODE);
        yield return data<ContactDetail>(new { _name = "Ewout" }, ERR.EXPECTED_START_OF_OBJECT_CODE);
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
        yield return data<Narrative>(new { div = "this isn't xml" }, COVE.NARRATIVE_XML_IS_MALFORMED_CODE);
        yield return data<Narrative>(new { div = "<puinhoop />" }, COVE.NARRATIVE_XML_IS_INVALID_CODE);

        yield return data<Attachment>(new { url = "urn:oid:1.3.6.1.4.1.343" });
        //   yield return data<Attachment>(new { url = "urn:oid:1" }, COVE.URI_LITERAL_INVALID_CODE);
        // This is an URL in our datamodel now, since that's what it is in R4 and later.
    }

    public static IEnumerable<object?[]> TestPrimitiveArrayData()
    {
        yield return data<Address>(new { line = "hi!" }, ERR.EXPECTED_START_OF_ARRAY_CODE);
        yield return data<Address>(new { line = Array.Empty<string>() }, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE);
        yield return data<Address>(new { line = Array.Empty<string>(), _line = Array.Empty<string>() },
            ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE);
        yield return data<Address>(new { line = Array.Empty<string>(), _line = new string?[] { null } },
            ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null }, _line = new[] { new { id = "1" } } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { line = new[] { "Ewout" }, _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null }, _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null }, _line = new string?[] { null, null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
        yield return data<Address>(new { line = new string?[] { null, null }, _line = new string?[] { null } },
            ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE);
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

        static void checkName(object parsed) => parsed.Should().BeOfType<Address>().Which.Line.Should()
            .BeEquivalentTo("Ewout", "Wouter");

        static void checkIds(object parsed) =>
            parsed.Should().BeOfType<Address>().Which.LineElement.Select(le => le?.ElementId).Should()
                .BeEquivalentTo("1", "2");

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
    public void TestRecovery()
    {
        var filename = Path.Combine("TestData", "fp-test-patient-errors.json");
        var jsonInput = File.ReadAllText(filename);

        var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

        try
        {
            var actual = JsonSerializer.Deserialize<Patient>(jsonInput, options);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            Console.WriteLine(dfe.Message);
            var recoveredActual = JsonSerializer.Serialize(dfe.PartialResult, options);
            Console.WriteLine(recoveredActual);

            assertErrors(dfe.Exceptions, [
                COVE.LITERAL_INVALID_CODE,
                ERR.UNKNOWN_PROPERTY_FOUND_CODE, // resourceType at the non-root level
                ERR.UNKNOWN_RESOURCE_TYPE_CODE, ERR.RESOURCE_TYPE_NOT_A_RESOURCE_CODE,
                ERR.RESOURCETYPE_SHOULD_BE_STRING_CODE, ERR.NO_RESOURCETYPE_PROPERTY_CODE,
                COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, ERR.EXPECTED_START_OF_ARRAY_CODE,
                ERR.UNKNOWN_PROPERTY_FOUND_CODE, // mother is not a property of HumanName
                ERR.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE, // family is not an array,
                ERR.EXPECTED_PRIMITIVE_NOT_NULL_CODE, // telecom use cannot be null
                ERR.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE, // address.use is not an object
                COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, // address.line should not have a null at the same position in both arrays
                COVE.INVALID_CODED_VALUE_CODE, // status 'generatedY'
                ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, // Questionnaire._subjectType cannot be just null
                COVE.CHOICE_TYPE_NOT_ALLOWED_CODE, // incorrect use of valueBoolean in option.
                ERR.EXPECTED_START_OF_OBJECT_CODE, // item.code is a complex object, not a boolean
                COVE.LITERAL_INVALID_CODE, // incorrect oid
                COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, // given cannot be a single array with just a null
                COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, // telecom.rank should be a number, not a boolean
                ERR.EXPECTED_START_OF_OBJECT_CODE, // extension._url is an object (although not applicable)
                COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, // gender.extension.valueCode should be a string, not a number
                ERR.CHOICE_ELEMENT_HAS_NO_TYPE_CODE, // extension.value is incorrect
                ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE, // extension.valueSuperDecimal is incorrect
                COVE.INVALID_BASE64_VALUE_CODE, ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, ERR.PROPERTY_MAY_NOT_BE_EMPTY_CODE,
                ERR.OBJECTS_CANNOT_BE_EMPTY_CODE,
                COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, // deceasedBoolean should be a boolean not a string
                COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, // multipleBirthInteger should not be a float (3.14)
            ]);

            var recoveredFilename = Path.Combine("TestData", "fp-test-patient-errors-recovered.json");
            var recoveredExpected = File.ReadAllText(recoveredFilename);

            List<string> errors = new();
            JsonAssert.AreSame("fp-test-patient-json-errors/recovery", recoveredExpected, recoveredActual, errors);
            errors.Should().BeEmpty();
        }
    }

    [TestMethod]
    public void TestBase64Parsing()
    {
        var attachment = deserializeAttachment(new FhirJsonConverterOptions());

        // After parsing, the ObjectValue is supposed to be the base64 string
        attachment.DataElement!.ObjectValue.Should().BeOfType<string>().And.Subject.Should().Be("SGkh");

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
        obj["body"].Should().BeEquivalentTo(new List<FhirString> { new("Test") });
        obj["testBool"].Should().BeEquivalentTo(new DynamicPrimitive{ ObjectValue = true });
        obj["testDec"].Should().BeEquivalentTo(new DynamicPrimitive{ ObjectValue = new decimal(123.4) });
        obj["testInt"].Should().BeEquivalentTo(new DynamicPrimitive{ ObjectValue = 999});
        obj["value"].Should().BeEquivalentTo(new FhirDateTime(dt));
    }

    [TestMethod]
    public void JsonDeserializerSupportsUnknownPropertiesOnKnownTypes()
    {
        var parser = new BaseFhirJsonDeserializer(ModelInspector.ForType<Patient>());

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
        obj!.TypeName.Should().Be("Patient");
        obj.Id.Should().Be("TestIdentifier");
        // array where primitive
        obj["active"].Should().BeEquivalentTo(new[]{new FhirBoolean(true), new FhirBoolean(false)});
        // primitive where array
        obj["communication"].Should().BeEquivalentTo(new DynamicPrimitive{ ObjectValue = "en" });
        // primitive when complex
        obj["name"].Should().BeEquivalentTo(new DynamicPrimitive{ ObjectValue = "Test"});
    }
    
    [TestMethod]
    public void JsonDeserializerHandleUnexpectedObject()
    {
        var parser = new BaseFhirJsonDeserializer(ModelInspector.ForType<Patient>());

        var test = new
        {
            resourceType = "Observation",
            status = new { value = "final" }, // Expected a primitive, got an object
            code = new
            {
                text = "Heart Rate"
            },
            valueQuantity = new
            {
                value = new { amount = 72 }, // Expected a number, got an object
                unit = "bpm"
            }
        };

        Utf8JsonReader reader = constructReader(test);

        parser.TryDeserializeResource(ref reader, out var obj, out var errors);
        
        obj.Should().NotBeNull();
        obj!.TypeName.Should().Be("Observation");
    }
    
    [TestMethod]
    public void JsonDeserializerHandleContainedStuff()
    {
        var parser = new BaseFhirJsonDeserializer(ModelInspector.ForType<Patient>());

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


    internal class CustomComplexValidator : DataAnnotationDeserialzationValidator
    {
        //public object? DateTimeSeenByObjectValueValidator;
        public FhirDateTime? DateTimeSeenByInstanceValidator;
        public FhirDateTime? DateTimeSeenByPropertyValidator;

        // public override void ValidateObjectValue(ref object? value, in ObjectValueDeserializationContext context,
        //     out COVE[]? reportedErrors)
        // {
        //     DateTimeSeenByObjectValueValidator = value;
        //
        //     // Now change it, to whether the next step picks it up.
        //     value = "1972-30-11T12:00:00Z";
        //
        //     base.ValidateObjectValue(ref value, context, out reportedErrors);
        // }

        public override void ValidateInstance(Base instance, in InstanceDeserializationContext context,
            out COVE[]? reportedErrors)
        {
            if (instance is FhirDateTime fdt)
            {
                DateTimeSeenByInstanceValidator = fdt;
                fdt.ObjectValue = "1972-11-30T12:00:00Z";
            }

            base.ValidateInstance(instance, context, out reportedErrors);
        }

        public override void ValidateProperty(object? propertyValue, in PropertyDeserializationContext context,
            out COVE[]? reportedErrors)
        {
            base.ValidateProperty(propertyValue, context, out reportedErrors);

            if (context.Path == "Patient.deceased")
            {
                var fdt = propertyValue.Should().BeOfType<FhirDateTime>().Subject;

                // Take note of what we got.
                DateTimeSeenByPropertyValidator = fdt;

                var validationContext = new ValidationContext(context.ObjectInstance)
                    .SetValidateRecursively(
                        false) // Don't go deeper - we've already validated the children because we're parsing bottom-up.
                    .SetPositionInfo(new PositionInfo((int)context.LineNumber, (int)context.LinePosition))
                    .SetLocationProducer(context.PathStack.GetInstancePath);
                reportedErrors = [..reportedErrors ?? [], COVE.LITERAL_INVALID(validationContext, "Nothing wrong, really", "DateTime")];
            }
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

        var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

        var identifier = JsonSerializer.Deserialize<Identifier>(ref reader, options)!;
        identifier.Should().BeOfType<Identifier>();
        identifier.System.Should().Be("http://nu.nl");
    }

    [TestMethod]
    public void CanParseMixedClass()
    {
        var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

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
             """, [ERR.DUPLICATE_PROPERTY_CODE, ERR.DUPLICATE_PROPERTY_CODE]),
            (
                """
                {
                   "resourceType" : "Patient",
                   "_active" : { "id" : "1234" },
                   "_active" : { "id" : "5678" }
                }
                """, [ERR.DUPLICATE_PROPERTY_CODE, ERR.DUPLICATE_PROPERTY_CODE]),
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
             """, [ERR.DUPLICATE_ARRAY_CODE]),
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
             """, [ERR.DUPLICATE_ARRAY_CODE]),
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
             """, [ERR.DUPLICATE_ARRAY_CODE]),
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


    [DataTestMethod]
    [DynamicData(nameof(getDuplicatePropertyTests), DynamicDataSourceType.Method)]
    public void TestDuplicateProperties(string testJson, string[] expectedErrs)
    {
        var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(testJson, options);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            assertErrors(dfe.Exceptions, expectedErrs);
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

        var jsonSerializerOptions = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

        try
        {
            _ = JsonSerializer.Deserialize<Patient>(scenario, jsonSerializerOptions);
            Assert.Fail("Should have encountered errors.");
        }
        catch (DeserializationFailedException dfe)
        {
            assertErrors(dfe.Exceptions, [expected]);
        }
    }

    [TestMethod]
    public void TestBackboneElementEmptyStack()
    {
        var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

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
            new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly)
                .UsingMode(DeserializationMode.Ostrich),
            new Predicate<IEnumerable<CodedException>>(errs => !errs.Any())
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly)
                .UsingMode(DeserializationMode.Recoverable),
            new Predicate<IEnumerable<CodedException>>(errs => !errs.Any(e => CodedExceptionFilters.IsRecoverableIssue(e)))
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly)
                .UsingMode(DeserializationMode.BackwardsCompatible),
            new Predicate<IEnumerable<CodedException>>(errs => !errs.Any(e => CodedExceptionFilters.IsBackwardsCompatibilityIssue(e)))
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly)
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE]),
            new Predicate<IEnumerable<CodedException>>(errs => errs.All(e => e.ErrorCode != COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE))
        ];
        yield return
        [
            new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly).UsingMode(DeserializationMode.Ostrich)
                .Enforcing([ERR.ARRAYS_CANNOT_BE_EMPTY_CODE, COVE.LITERAL_INVALID_CODE]),
            new Predicate<IEnumerable<CodedException>>(errs =>
            {
                IEnumerable<CodedException> codedExceptions = errs as CodedException[] ?? errs.ToArray();
                return codedExceptions.Any() && codedExceptions.All(e =>
                    e.ErrorCode is ERR.ARRAYS_CANNOT_BE_EMPTY_CODE or COVE.LITERAL_INVALID_CODE);
            })
        ];
    }

    [DataTestMethod]
    [DynamicData(nameof(getExtensionOptionsAndExpectedErrors), DynamicDataSourceType.Method)]
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
                .ForFhir(typeof(Patient).Assembly)
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
                .ForFhir(typeof(Patient).Assembly)
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])),
            new Predicate<CodedException>(_ => false)
        ];
        yield return
        [
            getPredicateFromOptions(new JsonSerializerOptions()
                .ForFhir(typeof(Patient).Assembly)
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Enforcing([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])
                .Ignoring([COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE])),
            new Predicate<CodedException>(ce => ce.ErrorCode == COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE)
        ];
    }

    private static IEnumerable<CodedException> getErrorsList()
    {
        var testDeserializerOptions = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly)
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


    [DataTestMethod]
    [DynamicData(nameof(getIgnoreEnforceTests), DynamicDataSourceType.Method)]
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
}