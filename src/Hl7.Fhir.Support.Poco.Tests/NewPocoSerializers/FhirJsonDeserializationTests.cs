using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;

#nullable enable

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class FhirJsonDeserializationTests
    {
        //TODO: Now that we have constants for all of these errors, we could replace the string here.
        private const string NUMBER_CANNOT_BE_PARSED = "JSON108";

        [DataTestMethod]
        [DataRow(null, typeof(decimal), "JSON109")]
        [DataRow(new[] { 1, 2 }, typeof(decimal), "JSON105")]

        [DataRow("hi!", typeof(string), null)]
        [DataRow("SGkh", typeof(byte[]), null)]
        [DataRow("hi!", typeof(byte[]), "JSON106")]
        [DataRow("hi!", typeof(DateTimeOffset), "JSON107")]
        [DataRow("2007-02-03", typeof(DateTimeOffset), null)]
        [DataRow("enumvalue", typeof(UriFormat), COVE.INVALID_CODED_VALUE_CODE)]
        [DataRow(true, typeof(Enum), "JSON110")]
        [DataRow("hi!", typeof(int), "JSON110")]

        [DataRow(3, typeof(decimal), null)]
        [DataRow(3, typeof(uint), null)]
        [DataRow(3, typeof(long), null)]
        [DataRow(3, typeof(ulong), null)]
        [DataRow(3.14, typeof(decimal), null)]
        [DataRow(double.MaxValue, typeof(decimal), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(3.14, typeof(int), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(3.14, typeof(uint), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(3.14, typeof(long), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(-3, typeof(ulong), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(long.MaxValue, typeof(uint), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(long.MaxValue, typeof(int), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(long.MaxValue, typeof(decimal), null)]
        [DataRow(5, typeof(float), null)]
        [DataRow(double.MaxValue, typeof(float), NUMBER_CANNOT_BE_PARSED)]
        [DataRow(6.14, typeof(double), null)]
        [DataRow(314, typeof(int), null)]
        [DataRow(314, typeof(decimal), null)]
        [DataRow(3.14, typeof(bool), "JSON110")]

        [DataRow(true, typeof(bool), null)]
        [DataRow(true, typeof(string), "JSON110")]
        public void TryDeserializePrimitiveValue(object data, Type expected, string code)
        {
            var reader = constructReader(data);
            reader.Read();

            var deserializer = getTestDeserializer(new());
            var (result, error) = deserializer.DeserializePrimitiveValue(ref reader, expected);

            if (code is not null)
                error?.ErrorCode.Should().Be(code);
            else
                error.Should().BeNull();

            if (expected == typeof(byte[]))
            {
                if (error is null)
                    Convert.ToBase64String((byte[])result!).Should().Be((string)data);
                else
                    result.Should().Be(data);
            }
            else if (expected == typeof(DateTimeOffset))
            {
                if (error is null)
                    result.Should().BeOfType<DateTimeOffset>().Which.ToFhirDate().Should().Be((string)data);
                else
                    result.Should().Be(data);
            }
            else if (code == ERR.EXPECTED_PRIMITIVE_NOT_ARRAY.ErrorCode ||
                code == ERR.EXPECTED_PRIMITIVE_NOT_OBJECT.ErrorCode)
#pragma warning disable CS0642 // Possible mistaken empty statement
                ; // nothing to check
#pragma warning restore CS0642 // Possible mistaken empty statement
            else
            {
                if (error is null)
                    result.Should().Be(data);
                else
                    result.Should().Be(data is not null ? PrimitiveTypeConverter.ConvertTo<string>(data) : null);
            }
        }

        private static FhirJsonPocoDeserializer getTestDeserializer(FhirJsonPocoDeserializerSettings settings) =>
            new(typeof(TestPatient).Assembly, settings);

        [TestMethod]
        public void TestCustomRecovery()
        {
            var (result, error) = test(1);
            result.Should().Be(false);

            (result, error) = test(11);
            result.Should().Be(true);

            (result, error) = test(21);
            result.Should().Be("21");
            error?.ErrorCode.Should().Be(FhirJsonException.ARRAYS_CANNOT_BE_EMPTY_CODE);

            (result, error) = test(31);
            result.Should().BeNull();
            error?.ErrorCode.Should().Be(FhirJsonException.UNEXPECTED_JSON_TOKEN_CODE);

            try
            {
                _ = test(41);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
            }

            static (object?, FhirJsonException?) correctIntToBool(ref Utf8JsonReader reader,
                                                                  Type targetType,
                                                                  object? originalValue,
                                                                  FhirJsonException originalException)
            {
                return reader.GetInt32() switch
                {
                    < 10 => (false, null),
                    < 20 => (true, null),
                    < 30 => (originalValue, ERR.ARRAYS_CANNOT_BE_EMPTY),
                    < 40 => (null, originalException),
                    _ => throw new InvalidOperationException("Something")
                };
            }

            static (object?, FhirJsonException?) test(int number)
            {
                var reader = constructReader(number); reader.Read();
                var deserializer = getTestDeserializer(new() { OnPrimitiveParseFailed = correctIntToBool });
                return deserializer.DeserializePrimitiveValue(ref reader, typeof(bool));
            }
        }

        [TestMethod]
        public void PrimitiveValueCannotBeComplex()
        {
            TryDeserializePrimitiveValue(new { bla = 4 }, typeof(int), ERR.EXPECTED_PRIMITIVE_NOT_OBJECT.ErrorCode);
        }

        [DataTestMethod]
        [DataRow("OperationOutcome", null)]
        [DataRow("OperationOutcomeX", "JSON116")]
        [DataRow("Meta", null)]
        [DataRow(4, "JSON102")]
        [DataRow(null, "JSON103")]
        public void DeriveClassMapping(object typename, string errorcode)
        {
            var (result, error) = test(typename);
            if (errorcode is null)
                error.Should().BeNull();
            else
                error?.ErrorCode.Should().Be(errorcode);

            if (errorcode is null)
                result!.Name.Should().Be((string)typename);

            static (ClassMapping?, FhirJsonException?) test(object typename)
            {
                var inspector = ModelInspector.ForAssembly(typeof(Resource).Assembly);

                var jsonBytes = typename != null
                    ? JsonSerializer.SerializeToUtf8Bytes(new { resourceType = typename })
                    : JsonSerializer.SerializeToUtf8Bytes(new { resourceTypeX = "wrong" });
                var reader = new Utf8JsonReader(jsonBytes);
                reader.Read();

                return FhirJsonPocoDeserializer.DetermineClassMappingFromInstance(ref reader, inspector);
            }
        }

        [DataTestMethod]
        [DataRow(null, typeof(FhirString), "JSON109")]
        [DataRow(new[] { 1, 2 }, typeof(FhirString), "JSON105")]
        [DataRow("SGkh", typeof(FhirString), null, "SGkh")]

        [DataRow("SGkh", typeof(Base64Binary), null, new byte[] { 72, 105, 33 })]
        [DataRow("hi!", typeof(Base64Binary), "JSON106", "hi!")]
        [DataRow(4, typeof(Base64Binary), "JSON110", "4")]

        [DataRow("2007-04", typeof(FhirDateTime), null, "2007-04")]
        [DataRow("2007-", typeof(FhirDateTime), COVE.DATETIME_LITERAL_INVALID_CODE, "2007-")]
        [DataRow(4.45, typeof(FhirDateTime), "JSON110", "4.45")]

        [DataRow("female", typeof(Code), null, "female")]
        [DataRow("is-a", typeof(Code<FilterOperator>), null, "is-a")]
        [DataRow("wrong", typeof(Code<FilterOperator>), COVE.INVALID_CODED_VALUE_CODE, "wrong")]   // just sets ObjectValue, POCO validation handles enum checks
        [DataRow(true, typeof(Code), ERR.UNEXPECTED_JSON_TOKEN_CODE, "true")]

        [DataRow("hi!", typeof(Instant), "JSON107")]
        [DataRow("2007-02-03", typeof(Instant), null, 2007)]

        public void ParsePrimitiveValue(object value, Type targetType, string errorcode, object? expectedObjectValue = null)
        {
            var state = new FhirJsonPocoDeserializerState();

            PrimitiveType test()
            {
                var inspector = ModelInspector.ForAssembly(typeof(TestPatient).Assembly);
                var deserializer = new FhirJsonPocoDeserializer(typeof(TestPatient).Assembly);
                var mapping = inspector.ImportType(targetType)!;

                var reader = constructReader(value);
                reader.Read();

                return deserializer.DeserializeFhirPrimitive(null, "dummy", mapping, ref reader, null, state);
            }

            var result = test();

            state.Errors.HasExceptions.Should().Be(errorcode is not null);

            if (state.Errors.HasExceptions)
            {
                if (errorcode is not null)
                    state.Errors.Should().OnlyContain(ce => ce.ErrorCode == errorcode);
                else
                    throw state.Errors.Single();
            }

            if (expectedObjectValue is not null)
            {
                if (targetType != typeof(Instant))
                    result.ObjectValue.Should().BeEquivalentTo(expectedObjectValue);
                else
                    result.ObjectValue.Should().BeOfType<DateTimeOffset>().Which.Year.Should().Be((int)expectedObjectValue!);
            }
        }

        private static (Base?, IReadOnlyCollection<CodedException>) deserializeComplex(Type objectType, object testObject, out Utf8JsonReader readerState,
            FhirJsonPocoDeserializerSettings settings)
        {
            // For the tests, enable full XHML validation so we can test it when necessary.
            var deserializer = new FhirJsonPocoDeserializer(typeof(TestPatient).Assembly, settings);
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

            string why = $"Should be the same: actual [{string.Join(",", actual.Select(a => a.ErrorCode))}] and expected [{string.Join(";", expected)}]";
            Console.WriteLine("Messages: " + string.Join(", ", actual.Select(a => a.Message)));
            actual.Count().Should().Be(expected.Length, because: why);
            _ = actual.Zip(expected, (a, e) => a.ErrorCode.Should().Be(e, because: why)).ToList();
            Console.WriteLine($"Found {string.Join(", ", actual.Select(a => a.Message))}");
        }

        [TestMethod]
        [DynamicData(nameof(TestDeserializeResourceData))]
        [DynamicData(nameof(TestDeserializeNestedResource))]
        public void TestDeserializeResource(object testObject, JsonTokenType tokenAfterParsing, params CodedException[] errors)
        {
            var reader = constructReader(testObject);
            reader.Read();

            var deserializer = new FhirJsonPocoDeserializer(typeof(TestPatient).Assembly);
            var state = new FhirJsonPocoDeserializerState();
            _ = deserializer.DeserializeResourceInternal(ref reader, state, stayOnLastToken: false);
            assertErrors(state.Errors, errors.Select(e => e.ErrorCode).ToArray());
            reader.TokenType.Should().Be(tokenAfterParsing);
        }

        public static IEnumerable<object[]> TestDeserializeResourceData
        {
            get
            {
                yield return new object[] { 5, JsonTokenType.Number, ERR.EXPECTED_START_OF_OBJECT };
                yield return new object[] { new { }, JsonTokenType.EndObject, ERR.NO_RESOURCETYPE_PROPERTY };
                yield return new object[] { new { resourceType = 4, crap = 4 }, JsonTokenType.EndObject, ERR.RESOURCETYPE_SHOULD_BE_STRING };
                yield return new object[] { new { resourceType = "Doesnotexist", crap = 5 }, JsonTokenType.EndObject, ERR.UNKNOWN_RESOURCE_TYPE };
                yield return new object[] { new { resourceType = nameof(OperationOutcome), crap = 5 }, JsonTokenType.EndObject, ERR.UNKNOWN_PROPERTY_FOUND };
                yield return new object[] { new { resourceType = nameof(Meta) },
                    JsonTokenType.EndObject, ERR.OBJECTS_CANNOT_BE_EMPTY, ERR.RESOURCE_TYPE_NOT_A_RESOURCE };
                yield return new object[] { new { resourceType = "Patient", deceasedDateTime = "2022-05" }, JsonTokenType.EndObject };
                yield return new object[] { new { resourceType = "Patient", deceasedDateTime = "2022-05", _deceasedDateTime = new { extension = new object[] { new { url = "test", valueString = "Smile" } } } }, JsonTokenType.EndObject };
            }
        }

        public static IEnumerable<object[]> TestDeserializeNestedResource
        {
            get
            {
                yield return new object[]
                {
                    new
                    {
                        resourceType = "Parameters",
                        parameter = new[]
                        {
                            new
                            { name = "a", resource = new
                                {
                                    resourceType = "Patient",
                                    active = true
                                }
                            }
                        }
                    },
                    JsonTokenType.EndObject
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestPrimitiveArrayData), DynamicDataSourceType.Method)]
        [DynamicData(nameof(CatchesIncorrectlyStructuredComplexData), DynamicDataSourceType.Method)]
        [DynamicData(nameof(TestNormalArrayData), DynamicDataSourceType.Method)]
        [DynamicData(nameof(TestPrimitiveData), DynamicDataSourceType.Method)]
        [DynamicData(nameof(TestValidatePrimitiveData), DynamicDataSourceType.Method)]
        public void TestData(Type t, object testObject, JsonTokenType token, Action<object?>? verify, params CodedException[] expectedErrors)
        {
            // Enable full narrative validation so we can test for it
            var (result, errors) = deserializeComplex(t, testObject, out var readerState,
                new() { Validator = new DataAnnotationDeserialzationValidator(narrativeValidation: Validation.NarrativeValidationKind.FhirXhtml) });

            assertErrors(errors, expectedErrors.Select(e => e.ErrorCode).ToArray());
            readerState.TokenType.Should().Be(token);
            var cdResult = result.Should().BeOfType(t);
            verify?.Invoke(result);
        }

        private static object?[] data<T>(object data, Action<object> verifier, params object[] args) =>
            new[] { typeof(T), data, JsonTokenType.EndObject, verifier }.Concat(args).ToArray();

        private static object?[] data<T>(object data, JsonTokenType token, params object[] args) =>
            new[] { typeof(T), data, token, default(Action<object>) }.Concat(args).ToArray();

        private static object?[] data<T>(object data, params object[] args) =>
            new[] { typeof(T), data, JsonTokenType.EndObject, null }.Concat(args).ToArray();


        public static IEnumerable<object?[]> CatchesIncorrectlyStructuredComplexData()
        {
            yield return new object?[] { typeof(Extension), 5, JsonTokenType.Number, default(Action<object>), ERR.EXPECTED_START_OF_OBJECT };
            yield return data<Extension>(5, JsonTokenType.Number, ERR.EXPECTED_START_OF_OBJECT);
            yield return data<Extension>(new[] { 2, 3 }, JsonTokenType.EndArray, ERR.EXPECTED_START_OF_OBJECT);
            yield return data<Extension>(new { }, ERR.OBJECTS_CANNOT_BE_EMPTY);
            yield return data<Extension>(new { resourceType = "Whatever" },
                ERR.RESOURCETYPE_UNEXPECTED, ERR.OBJECTS_CANNOT_BE_EMPTY);
            yield return data<Extension>(new { }, ERR.OBJECTS_CANNOT_BE_EMPTY);
            yield return data<Extension>(new { unknown = "test" }, ERR.UNKNOWN_PROPERTY_FOUND);
            yield return data<Extension>(new { url = "test" });
            yield return data<Extension>(new { _url = "test" }, ERR.USE_OF_UNDERSCORE_ILLEGAL);
            yield return data<Extension>(new { resourceType = "whatever", unknown = "test", url = "test" },
                    ERR.RESOURCETYPE_UNEXPECTED, ERR.UNKNOWN_PROPERTY_FOUND);
            yield return data<Extension>(new { value = "no type suffix" }, ERR.CHOICE_ELEMENT_HAS_NO_TYPE);
            yield return data<Extension>(new { valueUnknown = "incorrect type suffix" }, ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE);
            yield return data<Extension>(new { valueBoolean = true }, JsonTokenType.EndObject);
            yield return data<Extension>(new { valueUnknown = "incorrect type suffix", unknown = "unknown" },
                    ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE, ERR.UNKNOWN_PROPERTY_FOUND);
        }

        public static IEnumerable<object?[]> TestNormalArrayData()
        {
            yield return data<ContactDetail>(new { name = "Ewout", telecom = 4 }, checkName, ERR.EXPECTED_START_OF_ARRAY, ERR.EXPECTED_START_OF_OBJECT);
            yield return data<ContactDetail>(new { name = "Ewout", telecom = Array.Empty<object>() }, checkName,
                ERR.ARRAYS_CANNOT_BE_EMPTY);
            yield return data<ContactDetail>(new { name = "Ewout", telecom = new object[] { new { system = "phone" }, new { systemX = "b" } } },
                    checkData, ERR.UNKNOWN_PROPERTY_FOUND);
            yield return data<ContactDetail>(new { name = "Ewout", _telecom = new object[] { new { system = "phone" }, new { systemX = "b" } } },
                 checkData, ERR.USE_OF_UNDERSCORE_ILLEGAL, ERR.UNKNOWN_PROPERTY_FOUND);
            yield return data<ContactDetail>(new { name = new[] { "Ewout" } }, ERR.EXPECTED_PRIMITIVE_NOT_ARRAY);

            static void checkName(object parsed) => parsed.Should().BeOfType<ContactDetail>().Which.Name.Should().Be("Ewout");

            static void checkData(object parsedObject)
            {
                checkName(parsedObject);

                var parsed = parsedObject.Should().BeOfType<ContactDetail>().Subject;
                parsed.Telecom.Count.Should().Be(2);
                parsed.Telecom[0].System.Should().Be(ContactPoint.ContactPointSystem.Phone);
                parsed.Telecom[1].Count().Should().Be(0);
            }
        }

        public static IEnumerable<object?[]> TestPrimitiveData()
        {
            yield return data<ContactDetail>(new { name = new[] { "Ewout" } }, ERR.EXPECTED_PRIMITIVE_NOT_ARRAY);
            yield return data<ContactDetail>(new { name = new { dummy = "Ewout" } }, ERR.EXPECTED_PRIMITIVE_NOT_OBJECT);
            yield return data<ContactDetail>(new { _name = new[] { "Ewout" } }, ERR.EXPECTED_START_OF_OBJECT);
            yield return data<ContactDetail>(new { _name = "Ewout" }, ERR.EXPECTED_START_OF_OBJECT);
            yield return data<ContactDetail>(new { name = "Ewout" }, checkName);
            yield return data<ContactDetail>(new { _name = new { id = "12345" } }, checkId);
            yield return data<ContactDetail>(new { _name = new { id = true } }, ERR.INCOMPATIBLE_SIMPLE_VALUE);
            yield return data<ContactDetail>(new { name = "Ewout", _name = new { id = "12345" } }, checkAll);

            static void checkName(object parsed) => parsed.Should().BeOfType<ContactDetail>().Which.NameElement.Value.Should().Be("Ewout");
            static void checkId(object parsed) => parsed.Should().BeOfType<ContactDetail>().Which.NameElement.ElementId.Should().Be("12345");
            static void checkAll(object parsed)
            {
                checkName(parsed);
                checkId(parsed);
            }
        }

        public static IEnumerable<object?[]> TestValidatePrimitiveData()
        {
            yield return data<Narrative>(new { div = "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p>correct</p></div>" });
            yield return data<Narrative>(new { div = "this isn't xml" }, COVE.NARRATIVE_XML_IS_MALFORMED);
            yield return data<Narrative>(new { div = "<puinhoop />" }, COVE.NARRATIVE_XML_IS_INVALID);

            yield return data<TestAttachment>(new { url = "urn:oid:1.3.6.1.4.1.343" });
            yield return data<TestAttachment>(new { url = "urn:oid:1" }, COVE.URI_LITERAL_INVALID);
        }

        public static IEnumerable<object?[]> TestPrimitiveArrayData()
        {
            yield return data<TestAddress>(new { line = "hi!" }, ERR.EXPECTED_START_OF_ARRAY);
            yield return data<TestAddress>(new { line = Array.Empty<string>() }, ERR.ARRAYS_CANNOT_BE_EMPTY);
            yield return data<TestAddress>(new { line = Array.Empty<string>(), _line = Array.Empty<string>() }, ERR.ARRAYS_CANNOT_BE_EMPTY, ERR.ARRAYS_CANNOT_BE_EMPTY);
            yield return data<TestAddress>(new { line = Array.Empty<string>(), _line = new string?[] { null } }, ERR.ARRAYS_CANNOT_BE_EMPTY, ERR.PRIMITIVE_ARRAYS_ONLY_NULL);
            yield return data<TestAddress>(new { line = new string?[] { null }, _line = new[] { new { id = "1" } } }, ERR.PRIMITIVE_ARRAYS_ONLY_NULL);
            yield return data<TestAddress>(new { line = new[] { "Ewout" }, _line = new string?[] { null } }, ERR.PRIMITIVE_ARRAYS_ONLY_NULL);
            yield return data<TestAddress>(new { line = new string?[] { null }, _line = new string?[] { null } }, ERR.PRIMITIVE_ARRAYS_ONLY_NULL, ERR.PRIMITIVE_ARRAYS_ONLY_NULL);
            yield return data<TestAddress>(new { line = new string?[] { null }, _line = new string?[] { null, null } }, ERR.PRIMITIVE_ARRAYS_ONLY_NULL, ERR.PRIMITIVE_ARRAYS_ONLY_NULL);
            yield return data<TestAddress>(new { line = new string?[] { null, null }, _line = new string?[] { null } }, ERR.PRIMITIVE_ARRAYS_ONLY_NULL, ERR.PRIMITIVE_ARRAYS_ONLY_NULL);
            yield return data<TestAddress>(new { line = new[] { "Ewout", "Wouter" } }, checkName);
            yield return data<TestAddress>(new { line = new[] { "Ewout", "Wouter" }, _line = new[] { new { id = "1" } } }, checkId1AndName);
            yield return data<TestAddress>(new { line = new[] { "Ewout", "Wouter" }, _line = new[] { new { id = "1" }, null } }, checkId1AndName);
            yield return data<TestAddress>(new { line = new[] { "Ewout", "Wouter" }, _line = new[] { new { id = "1" }, new { id = "2" } } }, checkAll);
            yield return data<TestAddress>(new { line = new[] { "Ewout", null }, _line = new[] { null, new { id = "2" } } });
            yield return data<TestAddress>(new { line = new[] { "Ewout", null }, _line = new[] { new { id = "1" }, null } }, checkId1, COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL);
            yield return data<TestAddress>(new { _line = new[] { new { id = "1" }, null } }, checkId1, COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL);
            yield return data<TestAddress>(new { _line = new[] { new { id = "1" }, new { id = "2" } } }, checkIds);

            static void checkName(object parsed) => parsed.Should().BeOfType<TestAddress>().Which.Line.Should().BeEquivalentTo("Ewout", "Wouter");
            static void checkIds(object parsed) =>
                parsed.Should().BeOfType<TestAddress>().Which.LineElement.Select(le => le?.ElementId).Should().BeEquivalentTo("1", "2");
            static void checkId1(object parsed) =>
                parsed.Should().BeOfType<TestAddress>().Which.LineElement.Select(le => le?.ElementId).Should().BeEquivalentTo("1", null);
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
            var deserializer = new FhirJsonPocoDeserializer(typeof(Resource).Assembly);
            var reader = constructReader(
                    new
                    {
                        resourceType = "Parameters",
                        parameter = new[]
                        {
                            new { name = "a" }
                        }
                    });

            deserializer.DeserializeResource(ref reader).Should().NotBeNull();

            reader = constructReader(
                    new
                    {
                        resourceType = "ParametersX",
                    });

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
            var deserializer = new FhirJsonPocoDeserializer(typeof(Resource).Assembly);
            var reader = constructReader(
                    new
                    {
                        name = "Ewout"
                    });

            deserializer.DeserializeObject<ContactDetail>(ref reader).Should().NotBeNull();

            reader = constructReader(
                    new
                    {
                        nameX = "Ewout",
                    });

            try
            {
                deserializer.DeserializeObject<ContactDetail>(ref reader);
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

            var options = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);

            try
            {
                var actual = JsonSerializer.Deserialize<TestPatient>(jsonInput, options);
                Assert.Fail("Should have encountered errors.");
            }
            catch (DeserializationFailedException dfe)
            {
                Console.WriteLine(dfe.Message);
                var recoveredActual = JsonSerializer.Serialize(dfe.PartialResult, options);
                Console.WriteLine(recoveredActual);

                assertErrors(dfe.Exceptions, new string[]
                {
                    ERR.STRING_ISNOTAN_INSTANT_CODE,
                    ERR.RESOURCETYPE_UNEXPECTED_CODE,
                    ERR.UNKNOWN_RESOURCE_TYPE_CODE,
                    ERR.RESOURCE_TYPE_NOT_A_RESOURCE_CODE,
                    ERR.RESOURCETYPE_SHOULD_BE_STRING_CODE,
                    ERR.NO_RESOURCETYPE_PROPERTY_CODE,
                    ERR.INCOMPATIBLE_SIMPLE_VALUE_CODE,
                    ERR.EXPECTED_START_OF_ARRAY_CODE,
                    ERR.UNKNOWN_PROPERTY_FOUND_CODE, // mother is not a property of HumanName
                    ERR.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE, // family is not an array,
                    //ERR.PRIMITIVE_ARRAYS_INCOMPAT_SIZE_CODE, // given and _given not the same length
                    ERR.EXPECTED_PRIMITIVE_NOT_NULL_CODE, // telecom use cannot be null
                    ERR.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE, // address.use is not an object
                    COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, // address.line should not have a null at the same position in both arrays
                    COVE.INVALID_CODED_VALUE_CODE,      // status 'generatedY'
                    ERR.PRIMITIVE_ARRAYS_ONLY_NULL_CODE, // Questionnaire._subjectType cannot be just null
                    COVE.CHOICE_TYPE_NOT_ALLOWED_CODE,   // incorrect use of valueBoolean in option.
                    ERR.EXPECTED_START_OF_OBJECT_CODE, // item.code is a complex object, not a boolean
                    COVE.URI_LITERAL_INVALID_CODE, // incorrect oid
                    COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL_CODE, // given cannot be a single array with just a null
                    ERR.UNEXPECTED_JSON_TOKEN_CODE, // telecom.rank should be a number, not a boolean
                    ERR.USE_OF_UNDERSCORE_ILLEGAL_CODE, // should be extension.url, not extension._url
                    ERR.UNEXPECTED_JSON_TOKEN_CODE, // gender.extension.valueCode should be a string, not a number
                    ERR.CHOICE_ELEMENT_HAS_NO_TYPE_CODE, // extension.value is incorrect
                    ERR.CHOICE_ELEMENT_HAS_UNKOWN_TYPE_CODE, // extension.valueSuperDecimal is incorrect
                    ERR.UNEXPECTED_JSON_TOKEN_CODE, // deceasedBoolean should be a boolean not a string
                    ERR.NUMBER_CANNOT_BE_PARSED_CODE, // multipleBirthInteger should not be a float (3.14)
                    ERR.INCORRECT_BASE64_DATA_CODE,
                    ERR.ARRAYS_CANNOT_BE_EMPTY_CODE,
                    ERR.OBJECTS_CANNOT_BE_EMPTY_CODE
                });

                var recoveredFilename = Path.Combine("TestData", "fp-test-patient-errors-recovered.json");
                var recoveredExpected = File.ReadAllText(recoveredFilename);

                List<string> errors = new();
                JsonAssert.AreSame("fp-test-patient-json-errors/recovery", recoveredExpected, recoveredActual, errors);
                errors.Should().BeEmpty();
            }

        }

        [TestMethod]
        public void TestDisableBase64Parsing()
        {
            var attachment = deserializeAttachment(new());
            Encoding.ASCII.GetString(attachment.Data).Should().Be("Hi!");

            attachment = deserializeAttachment(new() { DisableBase64Decoding = true });
            attachment.DataElement.ObjectValue.Should().BeOfType<string>().And.Subject.Should().Be("SGkh");

            static TestAttachment deserializeAttachment(FhirJsonPocoDeserializerSettings settings)
            {
                var (attachment, errors) = deserializeComplex(typeof(TestAttachment), new { data = "SGkh" }, out _, settings);
                errors.Any().Should().BeFalse();

                return (TestAttachment)attachment!;
            }
        }

        internal class CustomComplexValidator : IDeserializationValidator
        {
            public void ValidateInstance(object? instance, in InstanceDeserializationContext context, out COVE[]? reportedErrors)
            {
                reportedErrors = null;
            }

            public void ValidateProperty(ref object? instance, in PropertyDeserializationContext context, out CodedValidationException[]? reportedErrors)
            {
                reportedErrors = null;

                if (instance is not FhirDateTime f) return;
                if (context.Path != "Patient.deceased") return;

                context.ElementMapping.DeclaringClass.Name.Should().Be("Patient");
                context.PropertyName.Should().Be("deceasedDateTime");
                context.ElementMapping.Name.Should().Be("deceased");

                // Invalid value, but since this value has already been validated during
                // deserialization of the FhirDateTime, validation will not be triggered!
                if (f.Value.EndsWith("Z")) f.Value = f.Value.TrimEnd('Z') + "+00:00";

                reportedErrors = new[] { COVE.DATETIME_LITERAL_INVALID };
            }

        }

        internal class CustomPropertyValueValidator : IDeserializationValidator
        {
            public void ValidateInstance(object? instance, in InstanceDeserializationContext context, out COVE[]? reportedErrors)
            {
                reportedErrors = null;
            }

            public void ValidateProperty(ref object? instance, in PropertyDeserializationContext context, out CodedValidationException[]? reportedErrors)
            {
                reportedErrors = null;

                if (instance is not String f) return;
                if (context.Path != "Patient.deceased.value") return;

                context.ElementMapping.DeclaringClass.Name.Should().Be("dateTime");
                context.PropertyName.Should().Be("value");
                context.ElementMapping.Name.Should().Be("value");

                // Invalid value, but since this value has already been validated during
                // deserialization of the FhirDateTime, validation will not be triggered!
                if (f.EndsWith("Z"))
                {
                    instance = f.TrimEnd('Z') + "+00:00";
                }
                reportedErrors = new[] { COVE.DATETIME_LITERAL_INVALID };
            }

        }

        internal class CustomDataTypeValidator : IDeserializationValidator
        {
            public void ValidateInstance(object? instance, in InstanceDeserializationContext context, out COVE[]? reportedErrors)
            {
                if (context.InstanceMapping.Name == "dateTime")
                {
                    var dt = instance.Should().BeOfType<FhirDateTime>().Subject;

                    if (dt.Value.EndsWith("Z")) dt.Value = dt.Value.TrimEnd('Z') + "+00:00";
                    reportedErrors = new[] { COVE.DATETIME_LITERAL_INVALID };
                }
                else
                {
                    reportedErrors = null;
                }
            }

            public void ValidateProperty(ref object? instance, in PropertyDeserializationContext context, out CodedValidationException[]? reportedErrors)
            {
                reportedErrors = null;
            }
        }

        [TestMethod]
        public void TestUpdatePrimitiveValue()
        {
            //test(new CustomComplexValidator());
            //test(new CustomDataTypeValidator());
            test(new CustomPropertyValueValidator());

            static void test(IDeserializationValidator validator)
            {
                var (result, errors) = deserializeComplex(typeof(TestPatient),
                    new { resourceType = "Patient", deceasedDateTime = "2070-01-01T12:01:02Z" },
                        out _, new() { Validator = validator });

                errors.Any().Should().BeTrue();
                errors.Should().AllBeOfType<COVE>()
                    .And.ContainSingle(e => ((COVE)e).ErrorCode == COVE.DATETIME_LITERAL_INVALID_CODE);
                result.Should().BeOfType<TestPatient>()
                    .Which.Deceased.Should().BeOfType<FhirDateTime>()
                    .Which.Value.Should().EndWith("+00:00");
            }
        }

        private class MixedClass
        {
            public TestPatient? FhirPatient { get; set; }

            public string? HandledByTextJson { get; set; }

            // This only works well when we construct deserializers using the ConverterFactory method
            // from System.Text.Json
            public List<Identifier>? FhirIdentifier { get; set; }
        }


        [TestMethod]
        public void CanParseIsolatedDataType()
        {
            var reader = constructReader(new { system = "http://nu.nl", value = "bla" });

            var options = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);

            var identifier = JsonSerializer.Deserialize<Identifier>(ref reader, options)!;
            identifier.Should().BeOfType<Identifier>();
            identifier.System.Should().Be("http://nu.nl");
        }

        [TestMethod]
        public void CanParseMixedClass()
        {
            var options = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);

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
    }
}
#nullable restore