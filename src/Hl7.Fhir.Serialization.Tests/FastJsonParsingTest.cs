using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class FastJsonParsingTest
    {
        [TestMethod]
        public void BooleanTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Patient\", \"active\": true, \"birthDate\": \"1986-08-27\"}",
                true, null
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""active"": false,
                    ""_active"": {
                        ""id"": ""ACT-1""
                    },
                    ""birthDate"": ""1986-08-27""
                }",
                false, "ACT-1"
            );

            AssertSuccess(
                "{\"resourceType\":\"Patient\", \"birthDate\": \"1986-08-27\"}",
                null, null
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""_active"": {
                        ""id"": ""ACT-1""
                    },
                    ""birthDate"": ""1986-08-27""
                }",
                null, "ACT-1"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""active"": 34.5,
                    ""birthDate"": ""1986-08-27""
                }",
                "Expected a boolean but found a number ('34.5')"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""active"": [1,2,3],
                    ""birthDate"": ""1986-08-27""
                }",
                "Expected a boolean but found '['"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""active"": {
                        ""id"": ""ACT-1""
                    },
                    ""birthDate"": ""1986-08-27""
                }",
                "Expected a boolean but found '{'"
            );

            void AssertSuccessAndError(string patientJson, string expectedErrorMessage)
            {
                AssertSuccess(patientJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Patient>(
                        patientJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string patientJson, bool? expectedActive, string expectedActiveId, bool permissiveParsing = false)
            {
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) {  PermissiveParsing = permissiveParsing })
                );
                if (expectedActive == null && expectedActiveId == null)
                {
                    Assert.IsNull(patient.ActiveElement);
                }
                else
                {
                    Assert.AreEqual(expectedActive, patient.Active);
                    Assert.AreEqual(expectedActiveId, patient.ActiveElement.ElementId);
                }
                Assert.AreEqual("1986-08-27", patient.BirthDate);
            }
        }

        [TestMethod]
        public void StringTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Immunization\",\"lotNumber\": \"123\",\"status\":\"completed\"}",
                "123", null
            );

            AssertSuccess(
                "{\"resourceType\":\"Immunization\",\"_lotNumber\": {\"id\": \"ACT-1\"},\"status\":\"completed\"}",
                null, "ACT-1"
            );

            AssertSuccess(
                "{\"resourceType\":\"Immunization\",\"status\":\"completed\"}",
                null, null
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Immunization\",\"lotNumber\": false,\"status\":\"completed\"}",
                "Expected a string but found a boolean ('false')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Immunization\",\"lotNumber\": {\"id\":\"ACT-1\"},\"status\":\"completed\"}",
                "Expected a string but found '{'"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Immunization\",\"lotNumber\": [1,2,3],\"status\":\"completed\"}",
                "Expected a string but found '['"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Immunization\",\"lotNumber\": \"   \",\"status\":\"completed\"}",
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string immunizationJson, string expectedErrorMessage)
            {
                AssertSuccess(immunizationJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Immunization>(
                        immunizationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string immunizationJson, string expectedLotNumber, string expectedLotNumberId, bool permissiveParsing = false)
            {
                var immunization = JsonSerializer.Deserialize<Model.R4.Immunization>(
                    immunizationJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedLotNumber == null && expectedLotNumberId == null)
                {
                    Assert.IsNull(immunization.LotNumberElement);
                }
                else
                {
                    Assert.AreEqual(expectedLotNumber, immunization.LotNumber);
                    Assert.AreEqual(expectedLotNumberId, immunization.LotNumberElement.ElementId);
                    Assert.AreEqual(Model.R4.ImmunizationStatusCodes.Completed, immunization.Status);
                }
            }
        }

        [TestMethod]
        public void IntegerTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Observation\",\"valueInteger\": -5678, \"status\":\"final\"}",
                -5678, null
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Observation"",
                    ""valueInteger"": 2147483647,
                    ""_valueInteger"": {
                        ""id"": ""INT-1""
                    },
                    ""status"": ""final""
                }",
                2_147_483_647, "INT-1"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Observation\",\"valueInteger\": \"0123456789012345678901234567890123456789abc\", \"status\":\"final\"}",
                "Expected a number but found a string ('\"0123456789012345678901234567890123456...\"')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Observation\",\"valueInteger\":[{\"p\":\"1\"},{\"p\":\"2\"}], \"status\":\"final\"}",
                "Expected a number but found '['"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Observation\",\"valueInteger\":{\"p\":[1,2,3]}, \"status\":\"final\"}",
                "Expected a number but found '{'"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Observation\",\"valueInteger\": 123.78, \"status\":\"final\"}",
                "'123.78' is not a valid integer"
            );

            void AssertSuccessAndError(string observationJson, string expectedErrorMessage)
            {
                AssertSuccess(observationJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Observation>(
                        observationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string observationJson, int? expectedValue, string expectedValueId, bool permissiveParsing = false)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    observationJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedValue == null && expectedValueId == null)
                {
                    Assert.IsNull(observation.Value);
                }
                else
                {
                    Assert.AreEqual(expectedValue, IsType<Integer>(observation.Value).Value);
                    Assert.AreEqual(expectedValueId, observation.Value.ElementId);
                }
                Assert.AreEqual(Model.R4.ObservationStatus.Final, observation.Status);
            }
        }

        [TestMethod]
        public void PositiveIntTest()
        {
            AssertSuccess(
                @"{
                    ""resourceType"":""ExplanationOfBenefit"",
                    ""item"": [{
                        ""sequence"": 45
                    }]
                }",
                45, null
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""ExplanationOfBenefit"",
                    ""item"": [{
                        ""sequence"": 2147483647,
                        ""_sequence"": {
                            ""id"": ""INT-1""
                        }
                    }]
                }",
                2_147_483_647, "INT-1"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""ExplanationOfBenefit"",
                    ""item"": [{
                        ""sequence"": true
                    }]
                }",
                "Expected a number but found a boolean ('true')"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""ExplanationOfBenefit"",
                    ""item"": [{
                        ""sequence"": 0
                    }]
                }",
                "'0' is not a valid positive integer"
            );

            void AssertSuccessAndError(string explanationOfBenefitJson, string expectedErrorMessage)
            {
                AssertSuccess(explanationOfBenefitJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.ExplanationOfBenefit>(
                        explanationOfBenefitJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string explanationOfBenefitJson, int? expectedSequence, string expectedSequenceId, bool permissiveParsing = false)
            {
                var explanationOfBenefit = JsonSerializer.Deserialize<Model.R4.ExplanationOfBenefit>(
                    explanationOfBenefitJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedSequence == null && expectedSequenceId == null)
                {
                    Assert.AreEqual(0, explanationOfBenefit.Item.Count);
                }
                else
                {
                    var item = Single(explanationOfBenefit.Item);
                    Assert.AreEqual(expectedSequence, item.Sequence);
                    Assert.AreEqual(expectedSequenceId, item.SequenceElement.ElementId);
                }

            }
        }

        [TestMethod]
        public void UnsignedIntTest()
        {
            AssertSuccess(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""size"": 0
                    }]
                }",
                0, null
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""size"": 8234560,
                        ""_size"": {
                            ""id"": ""INT-1""
                        }
                    }]
                }",
                8234560, "INT-1"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""size"": false
                    }]
                }",
                "Expected a number but found a boolean ('false')"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""size"": -8192
                    }]
                }",
                "'-8192' is not a valid unsigned integer"
            );

            void AssertSuccessAndError(string diagnosticReportJson, string expectedErrorMessage)
            {
                AssertSuccess(diagnosticReportJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                        diagnosticReportJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string diagnosticReportJson, int? expectedSize, string expectedSizeId, bool permissiveParsing = false)
            {
                var diagnosticReport = JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                    diagnosticReportJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) {  PermissiveParsing = permissiveParsing})
                );
                if (expectedSize == null && expectedSizeId == null)
                {
                    Assert.AreEqual(0, diagnosticReport.PresentedForm.Count);
                }
                else
                {
                    var presentedForm = Single(diagnosticReport.PresentedForm);
                    Assert.AreEqual(expectedSize, presentedForm.Size);
                    Assert.AreEqual(expectedSizeId, presentedForm.SizeElement.ElementId);
                }
            }
        }

        [TestMethod]
        public void DecimalTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Observation\",\"valueQuantity\":{\"value\":-5678.23,\"_value\":{\"id\":\"QV1\"}}}",
                -5678.23M, "QV1"
            );

            AssertSucessAndError(
                "{\"resourceType\":\"Observation\",\"valueQuantity\":{\"value\":\"-5678.23\"}}",
                "Expected a number but found a string ('\"-5678.23\"')"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Observation>(
                    "{\"resourceType\":\"Observation\",\"valueQuantity\":{\"value\":ZZZ}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "'Z' is an invalid start of a value"
            );

            void AssertSucessAndError(string observationJson, string expectedErrorMessage)
            {
                AssertSuccess(observationJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Observation>(
                        observationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string observationJson, decimal? expectedValue, string expectedValueId, bool permissiveParsing = false)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    observationJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedValue == null && expectedValueId == null)
                {
                    Assert.IsNull(observation.Value);
                }
                else
                {
                    var quantity = IsType<Quantity>(observation.Value);
                    Assert.AreEqual(expectedValue, quantity.Value);
                    Assert.AreEqual(expectedValueId, quantity.ValueElement.ElementId);
                }
            }
        }

        [TestMethod]
        public void Base64BinaryTest()
        {
            var data = new byte[] { 0x23, 0x23, 0x35, 0x36, 0x37, 0x38, 0x39, 0x40, 0x4a, 0x4b, 0x4c };
            var base64Data = Convert.ToBase64String(data);

            AssertSuccess(
                $@"{{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{{
                        ""data"": ""{base64Data}"",
                        ""_data"": {{
                            ""id"": ""DATA-1""
                        }}
                    }}]
                }}",
                data, "DATA-1"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""data"": false
                    }]
                }",
                "Expected a string but found a boolean ('false')"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""data"": ""()""
                    }]
                }",
                "'\"()\"' is not a valid base64 binary"
            );
            
            void AssertSuccessAndError(string diagnosticReportJson, string expectedErrorMessage)
            {
                AssertSuccess(diagnosticReportJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                        diagnosticReportJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );

            }

            void AssertSuccess(string diagnosticReportJson, byte[] expectedData, string expectedDataId, bool permissiveParsing = false)
            {
                var diagnosticReport = JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                    diagnosticReportJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedData == null && expectedDataId == null)
                {
                    Assert.AreEqual(0, diagnosticReport.PresentedForm.Count);
                }
                else
                {
                    var presentedForm = Single(diagnosticReport.PresentedForm);
                    CollectionAssert.AreEqual(expectedData, presentedForm.Data);
                    Assert.AreEqual(expectedDataId, presentedForm.DataElement.ElementId);
                }
            }
        }

        [TestMethod]
        public void CodeTest()
        {
            AssertSuccess(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""contentType"": ""application/json""
                    }]
                }",
                "application/json", null
             );

            AssertSuccess(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""presentedForm"": [{
                        ""_contentType"": {
                            ""id"": ""CODE-1""
                        }
                    }]
                }",
                null, "CODE-1"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"DiagnosticReport\",\"presentedForm\": [{\"contentType\": 1234567890.34567890}]}",
                "Expected a string but found a number ('1234567890.34567890')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"DiagnosticReport\",\"presentedForm\": [{\"contentType\": \"\"}]}",
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string diagnosticReportJson, string expectedErrorMessage)
            {
                AssertSuccess(diagnosticReportJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                        diagnosticReportJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string diagnosticReportJson, string expectedContentType, string expectedContentTypeId, bool permissiveParsing = false)
            {
                var diagnosticReport = JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                    diagnosticReportJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedContentType == null && expectedContentTypeId == null)
                {
                    Assert.AreEqual(0, diagnosticReport.PresentedForm.Count);
                }
                else
                {
                    var presentedForm = Single(diagnosticReport.PresentedForm);
                    Assert.AreEqual(expectedContentType, presentedForm.ContentType);
                    Assert.AreEqual(expectedContentTypeId, presentedForm.ContentTypeElement.ElementId);
                }
            }
        }

        [TestMethod]
        public void CodeEnumTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"DiagnosticReport\",\"status\": \"final\"}",
                Model.R4.DiagnosticReportStatus.Final, null
            );

            AssertSuccessAndErrorPermissiveParsing(
                "{\"resourceType\":\"DiagnosticReport\",\"status\": 1}",
                null, null,
                "Expected a string but found a number ('1')"
            );

            AssertSuccessAndErrorPermissiveParsing(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""_status"": {
                        ""id"": ""STAT-1""
                    },
                    ""status"": """"
                }",
                null, "STAT-1",
                "Empty strings are not allowed"
            );

            AssertSuccessAndErrorAllowUnrecognizedEnums(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""_status"": {
                        ""id"": ""STAT-1""
                    },
                    ""status"": ""ZZZ""
                }",
                null, "STAT-1",
                "'ZZZ' is not a valid DiagnosticReportStatus"
            );

            AssertSuccessAndErrorAllowUnrecognizedEnums(
                @"{
                    ""resourceType"":""DiagnosticReport"",
                    ""status"": ""ZZZ"",
                    ""_status"": {
                        ""id"": ""STAT-1""
                    }
                }",
                null, "STAT-1",
                "'ZZZ' is not a valid DiagnosticReportStatus"
            );

            void AssertSuccessAndErrorPermissiveParsing(string diagnosticReportJson, Model.R4.DiagnosticReportStatus? expectedStatus, string expectedStatusId, string expectedErrorMessage)
            {
                AssertSuccess(diagnosticReportJson, expectedStatus, expectedStatusId, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                        diagnosticReportJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccessAndErrorAllowUnrecognizedEnums(string diagnosticReportJson, Model.R4.DiagnosticReportStatus? expectedStatus, string expectedStatusId, string expectedErrorMessage)
            {
                AssertSuccess(diagnosticReportJson, expectedStatus, expectedStatusId, allowUnrecognizedEnums: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                        diagnosticReportJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string diagnosticReportJson, Model.R4.DiagnosticReportStatus? expectedStatus, string expectedStatusId, bool permissiveParsing = false, bool allowUnrecognizedEnums = false)
            {
                var diagnosticReport = JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                    diagnosticReportJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing, AllowUnrecognizedEnums = allowUnrecognizedEnums })
                );
                if (expectedStatus == null && expectedStatusId == null)
                {
                    Assert.IsNull(diagnosticReport.StatusElement);
                }
                else
                {
                    Assert.AreEqual(expectedStatus, diagnosticReport.Status);
                    Assert.AreEqual(expectedStatusId, diagnosticReport.StatusElement.ElementId);
                }
            }
        }

        [TestMethod]
        public void DateTest()
        {
            TestValidDate("1981-07-19");
            TestValidDate("1981-07");
            TestValidDate("1981");

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Patient\",\"birthDate\": 1}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('1')"
            );
            TestPermissiveParsing("{\"resourceType\":\"Patient\",\"birthDate\": 1}");

            TestInvalidDate("zot");
            TestInvalidDate("");
            TestInvalidDate("1981-15-27");
            TestInvalidDate("1981-06-31");
            TestInvalidDate("1981-07-19T22:00:00Z");

            void TestValidDate(string dateString)
            {
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    $"{{\"resourceType\":\"Patient\",\"birthDate\": \"{dateString}\"}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                );
                Assert.AreEqual(dateString, patient.BirthDate);
                Assert.IsNull(patient.BirthDateElement.ElementId);
            }

            void TestInvalidDate(string invalidDateString)
            {
                var patientJson = $"{{\"resourceType\":\"Patient\",\"birthDate\": \"{invalidDateString}\"}}";
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Patient>(
                        patientJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    $"'{invalidDateString}' is not a valid date"
                );
                TestPermissiveParsing(patientJson);
            }

            void TestPermissiveParsing(string patientJson)
            {
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.IsNull(patient.BirthDate);
            }
        }

        [TestMethod]
        public void DateTimeTest()
        {
            TestValidDateTime("1981");
            TestValidDateTime("1981-07");
            TestValidDateTime("1981-07-19");
            TestValidDateTime("1981-07-19T22:00Z");
            TestValidDateTime("1981-07-19T22:12:34Z");
            TestValidDateTime("1981-07-19T22:12:34.519Z");
            TestValidDateTime("1981-07-19T22:00+05:00");
            TestValidDateTime("1981-07-19T22:00:00-07:30");
            TestValidDateTime("1981-07-19T22:00:54.234-07:30");

            TestInvalidDateTime("ZZZ");
            TestInvalidDateTime("   ");
            TestInvalidDateTime("1981-15");
            TestInvalidDateTime("1981-07-61");
            TestInvalidDateTime("1981-07-19T22:00");
            TestInvalidDateTime("1981-07-19T25:00Z");
            TestInvalidDateTime("1981-07-19T22:00:73Z");

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Observation>(
                    "{\"resourceType\":\"Observation\",\"effectiveDateTime\": null}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found 'null'"
            );
            TestPermissiveParsing("{\"resourceType\":\"Observation\",\"effectiveDateTime\": null}");

            void TestValidDateTime(string dateTimeString)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    $"{{\"resourceType\":\"Observation\", \"effectiveDateTime\": \"{dateTimeString}\"}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                );
                Assert.AreEqual(dateTimeString, IsType<FhirDateTime>(observation.Effective).Value);
                Assert.IsNull(observation.Effective.ElementId);
            }

            void TestInvalidDateTime(string invalidDateTimeString)
            {
                var observationJson = $"{{\"resourceType\":\"Observation\",\"effectiveDateTime\": \"{invalidDateTimeString}\"}}";
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Observation>(
                        observationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    $"'{invalidDateTimeString}' is not a valid date-time"
                );
                TestPermissiveParsing(observationJson);
            }

            void TestPermissiveParsing(string observationJson)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    observationJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.IsNull(observation.Effective);
            }
        }

        [TestMethod]
        public void InstantTest()
        {
            TestValidInstant("1981-07-19T22:00Z");
            TestValidInstant("1981-07-19T22:12:34Z");
            TestValidInstant("1981-07-19T22:12:34.519Z");
            TestValidInstant("1981-07-19T22:00+05:00");
            TestValidInstant("1981-07-19T22:00:00-07:30");
            TestValidInstant("1981-07-19T22:00:54.234-07:30");

            TestInvalidInstant("ZZZ");
            TestInvalidInstant("");
            TestInvalidInstant("1981");
            TestInvalidInstant("1981-07");
            TestInvalidInstant("1981-07-19");
            TestInvalidInstant("1981-15");
            TestInvalidInstant("1981-07-61");
            TestInvalidInstant("1981-07-19T22:00");
            TestInvalidInstant("1981-07-19T25:00Z");
            TestInvalidInstant("1981-07-19T22:00:73Z");

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Observation>(
                    "{\"resourceType\":\"Observation\",\"effectiveInstant\": 1}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('1')"
            );
            TestPermissiveParsing("{\"resourceType\":\"Observation\",\"effectiveInstant\": 1}");

            void TestValidInstant(string instantString)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    $"{{\"resourceType\":\"Observation\",\"effectiveInstant\": \"{instantString}\"}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                );
                Assert.AreEqual(DateTimeOffset.Parse(instantString), IsType<Instant>(observation.Effective).Value);
                Assert.IsNull(observation.Effective.ElementId);
            }

            void TestInvalidInstant(string invalidInstantString)
            {
                var observationJson = $"{{\"resourceType\":\"Observation\",\"effectiveInstant\": \"{invalidInstantString}\"}}";
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Observation>(
                        observationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    $"'{invalidInstantString}' is not a valid instant"
                );
                TestPermissiveParsing(observationJson);
            }

            void TestPermissiveParsing(string observationJson)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    observationJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.IsNull(observation.Effective);
            }
        }

        [TestMethod]
        public void TimeTest()
        {
            TestValidTime("13:27");
            TestValidTime("13:27:42");
            TestValidTime("13:27:42.56791");

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Observation>(
                    "{\"resourceType\":\"Observation\",\"valueTime\": 22}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('22')"
            );
            TestPermissiveParsing("{\"resourceType\":\"Observation\",\"valueTime\": 22}");

            TestInvalidTime("ZZZZ");
            TestInvalidTime("25:27");
            TestInvalidTime("24:00");
            TestInvalidTime("1:27");
            TestInvalidTime("13:27:99");
            TestInvalidTime("13:27:42.xxx");

            void TestValidTime(string timeString)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    $"{{\"resourceType\":\"Observation\",\"valueTime\": \"{timeString}\"}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                );
                Assert.AreEqual(timeString, IsType<Time>(observation.Value).Value);
                Assert.IsNull(observation.Value.ElementId);
            }

            void TestInvalidTime(string invalidTimeString)
            {
                var observationJson = $"{{\"resourceType\":\"Observation\",\"valueTime\": \"{invalidTimeString}\"}}";
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Observation>(
                        observationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    $"'{invalidTimeString}' is not a valid time"
                );
                TestPermissiveParsing(observationJson);
            }

            void TestPermissiveParsing(string observationJson)
            {
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    observationJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.IsNull(observation.Value);
            }
        }

        [TestMethod]
        public void ExtensionTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Patient\",\"extension\": [{\"url\": \"http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex\",\"valueCode\":\"F\"}]}",
                "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex", "F"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Patient\",\"extension\": [{\"url\": 12,\"valueCode\":\"F\"}]}",
                null, "F",
                "Expected a string but found a number ('12')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Patient\",\"extension\": [{\"url\": \"\\t\\t\"}]}",
                null, null,
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string patientJson, string expectedUrl, string expectedCode, string expectedErrorMessage)
            {
                AssertSuccess(patientJson, expectedUrl, expectedCode, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Patient>(
                        patientJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string patientJson, string expectedUrl, string expectedCode, bool permissiveParsing = false)
            {
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                if (expectedUrl == null && expectedCode == null)
                {
                    Assert.AreEqual(0, patient.Extension.Count);
                }
                else
                {
                    var extension = Single(patient.Extension);
                    Assert.AreEqual(expectedUrl, extension.Url);
                    Assert.AreEqual(expectedCode, IsType<Code>(extension.Value).Value);
                }
            }
        }

        [TestMethod]
        public void XHtmlTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Patient\",\"text\":{\"div\": \"<div xmlns=\\\"http://www.w3.org/1999/xhtml\\\">A patient</div>\"}}",
                "<div xmlns=\"http://www.w3.org/1999/xhtml\">A patient</div>"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Patient\",\"text\":{\"div\": \"removed\"}}",
                "removed",
                "Invalid Xml encountered. Details: Data at the root level is invalid. Line 1, position 1."
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Patient\",\"text\":{\"div\": 12}}",
                null,
                "Expected a string but found a number ('12')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Patient\",\"text\":{\"div\": \"\"}}",
                null,
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string patientJson, string expectedDiv, string expectedErrorMessage)
            {
                AssertSuccess(patientJson, expectedDiv, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Patient>(
                        patientJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string patientJson, string expectedDiv, bool permissiveParsing = false)
            {
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) {  PermissiveParsing = permissiveParsing })
                );
                if (expectedDiv == null)
                {
                    Assert.IsNull(patient.Text);
                }
                else
                {
                    Assert.AreEqual(expectedDiv, patient.Text.Div);
                }
            }
        }

        [TestMethod]
        public void MarkdownTest()
        {
            var capabilityStatement = JsonSerializer.Deserialize<Model.R4.CapabilityStatement>(
                "{\"resourceType\":\"CapabilityStatement\",\"rest\":[{\"documentation\": \"### Headline\\nThe body text\"}]}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            var rest = Single(capabilityStatement.Rest);
            Assert.AreEqual("### Headline\nThe body text", rest.Documentation);

            AssertErrorAndSuccess(
                "{\"resourceType\":\"CapabilityStatement\",\"rest\":[{\"documentation\": 12}]}",
                "Expected a string but found a number ('12')"
            );

            AssertErrorAndSuccess(
                "{\"resourceType\":\"CapabilityStatement\",\"rest\":[{\"documentation\": \"\"}]}",
                "Empty strings are not allowed"
            );

            void AssertErrorAndSuccess(string capabilityStatementJson, string expectedErrorMessage)
            {
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.CapabilityStatement>(
                        capabilityStatementJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
                var emptyCapabilityStatement = JsonSerializer.Deserialize<Model.R4.CapabilityStatement>(
                    capabilityStatementJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) {  PermissiveParsing = true })
                );
                Assert.AreEqual(0, emptyCapabilityStatement.Rest.Count);
            }
        }

        [TestMethod]
        public void IdTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Bundle\",\"id\":\"BBB-19022\"}",
                "BBB-19022", null
            );

            // There is no id validation . . .

            AssertSuccess(
                "{\"resourceType\":\"Bundle\",\"id\":\"f(g(123))\"}",
                "f(g(123))", null
            );

            AssertSuccess(
                "{\"resourceType\":\"Bundle\",\"_id\":{\"id\":\"ID-1\"}}",
                null, "ID-1"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Bundle\",\"id\":12}",
                null, null,
                "Expected a string but found a number ('12')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Bundle\",\"id\":\"\",\"_id\":{\"id\":\"ID-1\"}}",
                null, "ID-1", 
                "Empty strings are not allowed"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Bundle\",\"_id\":{\"id\":\"ID-1\"},\"id\":\"\"}",
                null, "ID-1",
                "Empty strings are not allowed"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Bundle\",\"id\":\"BBB-19022\",\"_id\":{\"id\":\"\"}}",
                "BBB-19022", null,
                "Empty strings are not allowed"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Bundle\",\"_id\":{\"id\":\"\"},\"id\":\"BBB-19022\"}",
                "BBB-19022", null,
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string bundleJson, string expectedId, string expectedIdId, string expectedErrorMessage)
            {
                AssertSuccess(bundleJson, expectedId, expectedIdId, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Bundle>(
                        bundleJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string bundleJson, string expectedId, string expectedIdId, bool permissiveParsing = false)
            {
                var bundle = JsonSerializer.Deserialize<Model.R4.Bundle>(
                    bundleJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) {  PermissiveParsing = permissiveParsing })
                );
                if (expectedId == null && expectedIdId == null)
                {
                    Assert.IsNull(bundle.IdElement);
                }
                else
                {
                    Assert.AreEqual(expectedId, bundle.Id);
                    Assert.AreEqual(expectedIdId, bundle.IdElement.ElementId);
                }
            }
        }

        [TestMethod]
        public void UriTest()
        {
            var bundle = JsonSerializer.Deserialize<Model.R4.Bundle>(
                "{\"resourceType\":\"Bundle\",\"link\":[{\"url\": \"http://something.com/root/search\"}]}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            var link = Single(bundle.Link);
            Assert.AreEqual("http://something.com/root/search", link.Url);

            AssertErrorAndSuccess(
                "{\"resourceType\":\"Bundle\",\"link\":[{\"url\": 12}]}",
                "Expected a string but found a number ('12')"
            );

            AssertErrorAndSuccess(
                "{\"resourceType\":\"Bundle\",\"link\":[{\"url\": \"\"}]}",
                "Empty strings are not allowed"
            );

            void AssertErrorAndSuccess(string bundleJson, string expectedErrorMessage)
            {
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Bundle>(
                        bundleJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
                var emptyBundle = JsonSerializer.Deserialize<Model.R4.Bundle>(
                    bundleJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.AreEqual(0, emptyBundle.Link.Count);
            }
        }

        [TestMethod]
        public void UrlTest()
        {
            var diagnosticReport = JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                "{\"resourceType\":\"DiagnosticReport\",\"presentedForm\":[{\"url\": \"http://something.com/root/Binary/12\"}]}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            var presentedForm = Single(diagnosticReport.PresentedForm);
            Assert.AreEqual("http://something.com/root/Binary/12", presentedForm.Url);

            AssertErrorAndSuccess(
                "{\"resourceType\":\"DiagnosticReport\",\"presentedForm\":[{\"url\": 12}]}",
                "Expected a string but found a number ('12')"
            );

            AssertErrorAndSuccess(
                "{\"resourceType\":\"DiagnosticReport\",\"presentedForm\":[{\"url\": \"\"}]}",
                "Empty strings are not allowed"
            );

            void AssertErrorAndSuccess(string diagnosticReportJson, string expectedErrorMessage)
            {
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                        diagnosticReportJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
                var emptyDiagnosticReport = JsonSerializer.Deserialize<Model.R4.DiagnosticReport>(
                    diagnosticReportJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.AreEqual(0, emptyDiagnosticReport.PresentedForm.Count);
            }
        }

        [TestMethod]
        public void UuidTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueUuid\":\"urn:uuid:c757873d-ec9a-4326-a141-556f43239520\"}]}",
                "urn:uuid:c757873d-ec9a-4326-a141-556f43239520", null
            );

            // Currently there is no UUID validation. . . 

            AssertSuccess(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueUuid\":\"ZZZ\", \"_valueUuid\":{\"id\":\"007\"}}]}",
                "ZZZ", "007"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueUuid\":12}]}",
                "Expected a string but found a number ('12')"
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueUuid\":\"\"}]}",
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string parametersJson, string expectedErrorMessage)
            {
                AssertSuccess(parametersJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Parameters>(
                        parametersJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string parametersJson, string expectedUuid, string expectedUuidId, bool permissiveParsing = false)
            {
                var parameters = JsonSerializer.Deserialize<Parameters>(
                    parametersJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedUuid == null && expectedUuidId == null)
                {
                    Assert.AreEqual(0, parameters.Parameter.Count);
                }
                else
                {
                    var parameter = Single(parameters.Parameter);
                    Assert.AreEqual(expectedUuid, IsType<Uuid>(parameter.Value).Value);
                    Assert.AreEqual(expectedUuidId, parameter.Value.ElementId);
                }
            }
        }

        [TestMethod]
        public void OidTest()
        {
            AssertSuccess(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueOid\":\"urn:oid:1.2.3.4.5\"}]}",
                "urn:oid:1.2.3.4.5", null
            );

            // Currently there is no OID validation. . . 

            AssertSuccess(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueOid\":\"ZZZ\"}]}",
                "ZZZ", null
            );

            AssertSuccessAndError(
                "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueOid\":12}]}",
                "Expected a string but found a number ('12')"
            );

            AssertSuccessAndError(
                 "{\"resourceType\":\"Parameters\",\"parameter\":[{\"valueOid\":\"\"}]}",
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string parametersJson, string expectedErrorMessage)
            {
                AssertSuccess(parametersJson, null, null, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Parameters>(
                        parametersJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string parametersJson, string expectedOid, string expectedOidId, bool permissiveParsing = false)
            {
                var parameters = JsonSerializer.Deserialize<Parameters>(
                    parametersJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                if (expectedOid == null && expectedOidId == null)
                {
                    Assert.AreEqual(0, parameters.Parameter.Count);
                }
                else
                {
                    var parameter = Single(parameters.Parameter);
                    Assert.AreEqual(expectedOid, IsType<Oid>(parameter.Value).Value);
                    Assert.AreEqual(expectedOidId, parameter.Value.ElementId);
                }
            }
        }

        [TestMethod]
        public void CanonicalTest()
        {
            var parameters = JsonSerializer.Deserialize<Parameters>(
                "{\"resourceType\":\"Parameters\",\"meta\":{\"profile\":[\"https://myserver.com/profiles/first\"]}}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            var profile = Single(parameters.Meta.ProfileElement);
            Assert.AreEqual("https://myserver.com/profiles/first", profile.Value);

            AssertErrorAndSuccess(
                "{\"resourceType\":\"Parameters\",\"meta\":{\"profile\":[12]}}",
                "Expected a string but found a number ('12')"
            );

            AssertErrorAndSuccess(
                "{\"resourceType\":\"Parameters\",\"meta\":{\"profile\":[\"\"]}}",
                "Empty strings are not allowed"
            );

            void AssertErrorAndSuccess(string parametersJson, string expectedErrorMessage)
            {
                Throws(
                    () => JsonSerializer.Deserialize<Parameters>(
                        parametersJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
                var emptyParameters = JsonSerializer.Deserialize<Parameters>(
                    parametersJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) {  PermissiveParsing = true })
                );
                Assert.IsNull(emptyParameters.Meta);
            }
        }

        [TestMethod]
        public void ResourceLanguageTest()
        {
            var parameters = JsonSerializer.Deserialize<Parameters>(
                "{\"resourceType\":\"Parameters\",\"language\":\"en-US\",\"_language\":{\"id\":\"L-101\"}}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            Assert.AreEqual("en-US", parameters.Language);
            Assert.AreEqual("L-101", parameters.LanguageElement.ElementId);
        }

        [TestMethod]
        public void ResourceImplicitRulesTest()
        {
            var parameters = JsonSerializer.Deserialize<Parameters>(
                "{\"resourceType\":\"Parameters\",\"implicitRules\":\"https://mysite.com/rules\",\"_implicitRules\":{\"id\":\"RULE-101\"}}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            Assert.AreEqual("https://mysite.com/rules", parameters.ImplicitRules);
            Assert.AreEqual("RULE-101", parameters.ImplicitRulesElement.ElementId);
        }

        [TestMethod]
        public void ElementExtensionTest()
        {
            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                "{\"resourceType\":\"Patient\",\"gender\":\"male\",\"_gender\":{\"extension\":[{\"url\":\"http://myserver.com/extensions/gendernumber\", \"valueInteger\":34}]}}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            var genderExtension = Single(patient.GenderElement.Extension);
            Assert.AreEqual("http://myserver.com/extensions/gendernumber", genderExtension.Url);
            Assert.AreEqual(34, IsType<Integer>(genderExtension.Value).Value);
        }

        [TestMethod]
        public void NarrativeStatusTest()
        {
            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                "{\"resourceType\":\"Patient\",\"text\":{\"status\":\"generated\",\"_status\":{\"id\":\"TEXT-101\"}}}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            Assert.AreEqual(Narrative.NarrativeStatus.Generated, patient.Text.Status);
            Assert.AreEqual("TEXT-101", patient.Text.StatusElement.ElementId);
        }

        [TestMethod]
        public void SimpleQuantityTest()
        {
            var json = "{\"resourceType\":\"MedicationDispense\", \"dosageInstruction\":[{\"doseQuantity\":{\"value\": 10.7}}]}";
            var medicationDispense = JsonSerializer.Deserialize<Model.STU3.MedicationDispense>(
                json,
                new JsonSerializerOptions().ForFhir(Model.Version.STU3)
            );
            var dosageInstruction = Single(medicationDispense.DosageInstruction);
            var dose = IsType<SimpleQuantity>(dosageInstruction.Dose);
            Assert.AreEqual(10.7M, dose.Value);
        }

        [TestMethod]
        public void ListTest()
        {
            // Empty lists are OK

            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                "{\"resourceType\":\"Patient\",\"name\":[]}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            Assert.AreEqual(0, patient.Name.Count);

            var patientJsonWithObjectInsteadOfArray = "{\"resourceType\":\"Patient\",\"name\": {\"family\":\"Smith\"}}";
            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJsonWithObjectInsteadOfArray,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected '[' but found '{'"
            );

            // When PermissiveParsing is true single values are OK for lists element

            patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                patientJsonWithObjectInsteadOfArray,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            Assert.AreEqual("Smith", Single(patient.Name).Family);

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Patient\",\"name\": [{\"family\":\"Smith\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected depth to be zero at the end of the JSON payload. There is an open JSON object or array that should be closed"
            );
        }

        [TestMethod]
        public void PrimitiveListTest()
        {
            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""given"": [""John"", ""R.""]
                    }]
                }",
                new[] { "John", "R." },
                new string[] { null, null }
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""given"": [""John"", ""R."", null]
                    }]
                }",
                new[] { "John", "R." },
                new string[] { null, null },
                "The 'given' property has one or more 'null'(s) and no matching '_given' property"
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[{""id"": ""GIVEN-0"" }, { ""id"": ""GIVEN-1"" }]
                    }]
                }",
                new string[] {null, null},
                new[] { "GIVEN-0", "GIVEN-1" }
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[null, {""id"": ""GIVEN-1"" }, null, { ""id"": ""GIVEN-3"" }]
                    }]
                }",
                new string[] { null, null },
                new[] { "GIVEN-1", "GIVEN-3" },
                "The '_given' property has one or more 'null'(s) and no matching 'given' property"
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""given"": [""John"", ""R."",                 null],
                        ""_given"":[null,     {""id"": ""GIVEN-1"" }, { ""id"": ""GIVEN-2"" }]
                    }]
                }",
                new[] { "John", "R.", null },
                new[] { null, "GIVEN-1", "GIVEN-2" }
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[null,     {""id"": ""GIVEN-1""}, { ""id"": ""GIVEN-2"" }],
                        ""given"": [""John"", ""R."",                null]
                    }]
                }",
                new[] { "John", "R.", null },
                new[] { null, "GIVEN-1", "GIVEN-2" }
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[{""id"": ""GIVEN-0""}],
                        ""given"": [""John"",            ""R.""]
                    }]
                }",
                new[] { "John", "R." },
                new[] { "GIVEN-0", null}
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""given"": [""John"",           ""R.""],
                        ""_given"":[{""id"":""GIVEN-0""}]
                    }]
                }",
                new[] { "John", "R." },
                new[] { "GIVEN-0", null }
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""given"": [""John""],
                        ""_given"":[{""id"": ""GIVEN-0""}, {""id"": ""GIVEN-1""}]
                    }]
                }",
                new[] { "John", null },
                new[] { "GIVEN-0", "GIVEN-1" }
            );

            AssertSuccess(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[{""id"":""GIVEN-0"" }, {""id"":""GIVEN-1""}],
                        ""given"": [""John""]
                    }]
                }",
                new[] { "John", null },
                new[] { "GIVEN-0", "GIVEN-1" }
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[null, {""id"":""GIVEN-1""}],
                        ""given"": [null, ""R.""]
                    }]
                }",
                new[] { "R." },
                new[] { "GIVEN-1" },
                "Expected a value but found 'null'"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""_given"":[{""id"":""GIVEN-0""}, null],
                        ""given"": [""John""]
                    }]
                }",
                new[] { "John" },
                new[] { "GIVEN-0" },
                "The '_given' property has one or more 'null'(s) not matched by values in the 'given' property"
            );

            AssertSuccessAndError(
                @"{
                    ""resourceType"":""Patient"",
                    ""name"": [{
                        ""given"": [""John"", """"]
                    }]
                }",
                new[] { "John" },
                new string[] { null },
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string patientJson, string[] expectedNames, string[] expectedIds, string expectedError)
            {
                AssertSuccess(patientJson, expectedNames, expectedIds, permissiveParsing: true);
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Patient>(
                        patientJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedError
                );
            }

            void AssertSuccess(string patientJson, string[] expectedNames, string[] expectedIds, bool permissiveParsing = false)
            {
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = permissiveParsing })
                );
                var name = Single(patient.Name);
                CollectionAssert.AreEqual(
                    expectedNames,
                    name.Given.ToList()
                );
                for (var i = 0; i < expectedIds.Length; i++)
                {
                    Assert.AreEqual(expectedIds[i], name.GivenElement[i].ElementId);
                }

            }
        }

        [TestMethod]
        public void DetermineResourceTypeTest()
        {
            var resource = JsonSerializer.Deserialize<Resource>(
                "{\"gender\": \"male\",\"resourceType\":\"Patient\"}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            var patient = IsType<Model.R4.Patient>(resource);
            Assert.AreEqual(AdministrativeGender.Male, patient.Gender);

            // Empty resources are OK (same behavior of old parser)

            resource = JsonSerializer.Deserialize<Resource>(
                "{\"resourceType\":\"Patient\"}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            IsType<Model.R4.Patient>(resource);

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    "{\"resourceType\":\"ZZZ\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Unknown resource type 'ZZZ'"
            );

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    "{\"resourceType\": 25}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('25')"
            );

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    "{\"resourceType\": \"\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Empty strings are not allowed"
            );

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    "{\"gender\": \"male\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Missing 'resourceType' property"
            );
        }

        [TestMethod]
        public void AssumeResourceTypeTest()
        {
            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"ZZZ\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Unknown resource type 'ZZZ'"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\": 25}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('25')"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\": \"\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Empty strings are not allowed"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Bundle\"}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a Patient but found a Bundle"
            );

            // If we have an explicit type we accept resource without 'resourceType' - same bahavior of the old parser

            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                "{\"gender\": \"male\"}",
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            Assert.AreEqual(AdministrativeGender.Male, patient.Gender);

            // . . . but not empty resources 

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Empty objects are not allowed"
            );
        }

        [TestMethod]
        public void UnrecognizedElementTest()
        {
            AssertErrorAndSuccessWithAcceptUnknownMembers(
                "{\"resourceType\":\"Observation\",\"pinco\": 10,\"valueInteger\": 123}"
            );

            AssertErrorAndSuccessWithAcceptUnknownMembers(
                "{\"resourceType\":\"Observation\",\"pinco\": \"A string\",\"valueInteger\": 123}"
            );

            AssertErrorAndSuccessWithAcceptUnknownMembers(
                "{\"resourceType\":\"Observation\",\"pinco\":{\"subPinco\": \"A string\"},\"valueInteger\": 123}"
            );

            AssertErrorAndSuccessWithAcceptUnknownMembers(
                "{\"resourceType\":\"Observation\",\"pinco\":[{\"subPinco\": \"A string\"},{\"subPinco\": \"Another string\"}],\"valueInteger\": 123}"
            );

            // PositiveInt is not a valid Observation.value[x] type

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Observation>(
                    "{\"resourceType\":\"Observation\",\"valuePositiveInt\": 123.78}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Unrecognized element 'valuePositiveInt'"
            );

            // Reference.identifier does not exist in DSTU2

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    @"{
                        ""resourceType"":""Patient"",
                        ""managingOrganization"": {
                            ""reference"": ""Organization/1"",
                            ""identifier"": {
                                ""value"": ""ORG-101""
                            }
                        }
                    }",
                    new JsonSerializerOptions().ForFhir(Model.Version.DSTU2)
                ),
                "Unrecognized element 'identifier'"
            );

            void AssertErrorAndSuccessWithAcceptUnknownMembers(string observationJson)
            {
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Observation>(
                        observationJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    "Unrecognized element 'pinco'"
                );
                var observation = JsonSerializer.Deserialize<Model.R4.Observation>(
                    "{\"resourceType\":\"Observation\",\"pinco\": 10,\"valueInteger\": 123}",
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { AcceptUnknownMembers = true })
                );
                Assert.AreEqual(123, IsType<Integer>(observation.Value).Value);
            }
        }

        [TestMethod]
        public void ContainedTest()
        {
            var patientJson = @"
            {
                ""contained"": [
                    {
                        ""resourceType"": ""Observation"",
                        ""valueString"": ""ObservationValue""
                    },
                    {
                        ""resourceType"": ""DiagnosticReport"",
                        ""status"": ""final""
                    }
                ],
                ""resourceType"": ""Patient""
            }";
            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                patientJson,
                new JsonSerializerOptions().ForFhir(Model.Version.R4)
            );
            Assert.AreEqual(2, patient.Contained.Count);
            var observation = IsType<Model.R4.Observation>(patient.Contained[0]);
            Assert.AreEqual("ObservationValue", IsType<FhirString>(observation.Value).Value);
            var diagnosticReport = IsType<Model.R4.DiagnosticReport>(patient.Contained[1]);
            Assert.AreEqual(Model.R4.DiagnosticReportStatus.Final, diagnosticReport.Status);
        }

        [TestMethod]
        public void JsonFhirCommentsTest()
        {
            var patientWithCommentJson = "{\"resourceType\":\"Patient\",\"name\":[{\"fhir_comments\":[\"Peter James, but called Jim\"],\"given\":[\"Peter\", \"James\"]}]}";

            var dstu2Patient = JsonSerializer.Deserialize<Model.DSTU2.Patient>(
                patientWithCommentJson,
                new JsonSerializerOptions().ForFhir(Model.Version.DSTU2)
            );
            CollectionAssert.AreEqual(
                new[] { "Peter", "James" },
                Single(dstu2Patient.Name).Given?.ToList()
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.STU3.Patient>(
                    patientWithCommentJson,
                    new JsonSerializerOptions().ForFhir(Model.Version.STU3)
                ),
                "The 'fhir_comments' feature is disabled"
            );
            var stu3Patient = JsonSerializer.Deserialize<Model.STU3.Patient>(
                patientWithCommentJson,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.STU3) { PermissiveParsing = true })
            );
            CollectionAssert.AreEqual(
                new[] { "Peter", "James" },
                Single(stu3Patient.Name).Given?.ToList()
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientWithCommentJson,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "The 'fhir_comments' feature is disabled"
            );
            var r4Patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                patientWithCommentJson,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            CollectionAssert.AreEqual(
                new[] { "Peter", "James" },
                Single(r4Patient.Name).Given?.ToList()
            );
        }

        [TestMethod]
        public void ObjectTest()
        {
            AssertErrorAndSuccess(
                "{\"resourceType\":\"Patient\",\"_gender\":[1, 2, 3],\"birthDate\":\"1976-08-12\"}",
                "Expected '{' but found '['"
            );

            AssertErrorAndSuccess(
                "{\"resourceType\":\"Patient\",\"_gender\":{},\"birthDate\":\"1976-08-12\"}",
                "Empty objects are not allowed"
            );

            AssertErrorAndSuccess(
                "{\"resourceType\":\"Patient\",\"_gender\":123,\"birthDate\":\"1976-08-12\"}",
                "Expected '{' but found a number ('123')"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "[{\"resourceType\":\"Patient\",\"birthDate\":\"1976-08-12\"}]",
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                ),
                "Expected '{' but found '['"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{}",
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                ),
                "Empty objects are not allowed"
            );

            void AssertErrorAndSuccess(string patientJson, string expectedErrorMessage)
            {
                Throws(
                    () => JsonSerializer.Deserialize<Model.R4.Patient>(
                        patientJson,
                        new JsonSerializerOptions().ForFhir(Model.Version.R4)
                    ),
                    expectedErrorMessage
                );
                var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
                );
                Assert.IsNull(patient.GenderElement);
                Assert.AreEqual("1976-08-12", patient.BirthDate);
            }
        }

        [TestMethod]
        public void RepeatTest()
        {
            var patientJson = "{\"resourceType\":\"Patient\",\"gender\": \"male\",\"gender\":\"female\"}";
            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Element 'gender' must not repeat"
            );

            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                patientJson,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            Assert.AreEqual(AdministrativeGender.Female, patient.Gender);

            patientJson = "{\"resourceType\":\"Patient\",\"_gender\":{\"id\":\"1\"},\"_gender\":{\"id\":\"2\"}}";
            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Element 'gender' must not repeat"
            );
            patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                patientJson,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            Assert.AreEqual("2", patient.GenderElement.ElementId);

            patientJson = @"{
                ""resourceType"":""Patient"",
                ""deceasedBoolean"": true,
                ""deceasedDateTime"": ""2001-07-26""
            }";
            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    patientJson,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Element 'deceased[x]' must not repeat"
            );
            patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                patientJson,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            Assert.AreEqual("2001-07-26", IsType<FhirDateTime>(patient.Deceased).Value);
        }

        [TestMethod]
        public void ErrorsTest()
        {
            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Patient\",\"_gender\":{",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected depth to be zero at the end of the JSON payload. There is an open JSON object or array that should be closed."
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Patient\",\"_gender\": {\"id\": 12}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('12')"
            );

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Patient\",\"_gender\": {\"id\": \"\"}}",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Empty strings are not allowed"
            );

            var jsonException = Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    "{\"resourceType\":\"Patient\",\"deceasedBoolean\":",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected depth to be zero at the end of the JSON payload. There is an open JSON object or array that should be closed."
            );
            Assert.AreEqual(0, jsonException.LineNumber);
            Assert.AreEqual(44, jsonException.BytePositionInLine);
            var formatException = jsonException.ToFormatException();
            Assert.AreEqual(
                "Expected depth to be zero at the end of the JSON payload. There is an open JSON object or array that should be closed. (at line 1, 45)",
                formatException.Message
            );

            var bundleWithInnerErrorJson =
@"{
    ""resourceType"":""Bundle"",
    ""entry"":[
        {
            ""fullUrl"": ""Patient/1"",
            ""resource"": {
                ""resourceType"":""Patient"",
                ""gender"": ""male"",
                ""name"": [
                    {
                        ""family"": ""Smith"",
                        ""given"": [
                            ""John"",
                            ""R.""
                        ]
                    }
                ]
            }
        },
        {
            ""fullUrl"": ""Patient/2"",
            ""resource"": {
                ""resourceType"":""Patient"",
                ""gender"": ""female"",
                ""name"": [
                    {
                        ""family"": ""Smith"",
                        ""given"": [
                            ""Jane""
                        ]
                    }
                ],
                ""address"": [
                    {
                        ""city"": ""Ann Arbor""
                    },
                    {
                        ""city"": 13
                    }
                ]
            }
        }
    ]
}";
            jsonException = Throws(
                () => JsonSerializer.Deserialize<Model.R4.Bundle>(
                    bundleWithInnerErrorJson,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Expected a string but found a number ('13')"
            );
            Assert.AreEqual("entry[1].resource.address[1].city", jsonException.Path);
            Assert.AreEqual(37, jsonException.LineNumber);
            Assert.AreEqual(34, jsonException.BytePositionInLine);
            formatException = jsonException.ToFormatException();
            Assert.AreEqual(
                "Expected a string but found a number ('13') (at entry[1].resource.address[1].city line 38, 35)",
                formatException.Message
            );
        }

        [TestMethod]
        public void TrailingCommasTest()
        {
            var json = "{\"resourceType\":\"Patient\",\"gender\":\"male\",}";

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    json,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "The JSON object contains a trailing comma at the end which is not supported in this mode"
            );

            var patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                json,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            Assert.AreEqual(AdministrativeGender.Male, patient.Gender);

            json = "{\"resourceType\":\"Patient\",\"name\":[{\"family\":\"Smith\"},]}";

            Throws(
                () => JsonSerializer.Deserialize<Model.R4.Patient>(
                    json,
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "The JSON array contains a trailing comma at the end which is not supported in this mode"
            );

            patient = JsonSerializer.Deserialize<Model.R4.Patient>(
                json,
                new JsonSerializerOptions().ForFhir(new ParserSettings(Model.Version.R4) { PermissiveParsing = true })
            );
            Assert.AreEqual("Smith", Single(patient.Name).Family);
        }

        [TestMethod]
        public void BundleR4Test()
        {
            var json =
@"{
    ""resourceType"":""Bundle"",
    ""entry"":[
        {
            ""resource"": {
                ""id"":""P-101"",
                ""resourceType"":""Patient"",
                ""gender"": ""male"",
                ""deceasedBoolean"": true,
                ""multipleBirthInteger"": 3,
                ""_multipleBirthInteger"": {
                    ""id"": ""MBI-1""
                },
                ""name"": [
                    {
                        ""family"": ""Smith"",
                        ""given"": [
                            ""John"",
                            ""R.""
                        ],
                        ""_given"": [
                            null,
                            { ""id"": ""GIVEN-1"" }
                        ]
                    }
                ]
            }
        }
    ]
}";
            var resource = JsonSerializer.Deserialize<Resource>(json, new JsonSerializerOptions().ForFhir(Model.Version.R4));
            var bundle = IsType<Model.R4.Bundle>(resource);
            Assert.AreEqual(1, bundle.Entry.Count);
            var firstResource = bundle.Entry[0].Resource;
            var patient = IsType<Model.R4.Patient>(firstResource);
            Assert.AreEqual("P-101", patient.Id);
            Assert.AreEqual(AdministrativeGender.Male, patient.Gender);
            var deceasedBoolean = IsType<FhirBoolean>(patient.Deceased);
            Assert.AreEqual(true, deceasedBoolean.Value);
            var multipleBirthInteger  = IsType<Integer>(patient.MultipleBirth);
            Assert.AreEqual(3, multipleBirthInteger.Value);
            Assert.AreEqual("MBI-1", multipleBirthInteger.ElementId);
            Assert.AreEqual(1, patient.Name.Count);
            var name = patient.Name[0];
            Assert.AreEqual("Smith", name.Family);
            Assert.AreEqual(2, name.GivenElement.Count);
            Assert.AreEqual("John", name.GivenElement[0].Value);
            Assert.IsNull(name.GivenElement[0].ElementId);
            Assert.AreEqual("R.", name.GivenElement[1].Value);
            Assert.AreEqual("GIVEN-1", name.GivenElement[1].ElementId);
        }

        [TestMethod]
        public void PatientR4Test()
        {
            var json =
@"{
    ""id"":""P-101"",
    ""resourceType"":""Patient"",
    ""gender"": ""female"",
    ""managingOrganization"": {
        ""reference"": ""Organization/1"",
        ""identifier"": {
            ""value"": ""ORG-101""
        }
    }
}";
            var resource = JsonSerializer.Deserialize<Resource>(json, new JsonSerializerOptions().ForFhir(Model.Version.R4));
            var patient = IsType<Model.R4.Patient>(resource);
            Assert.AreEqual("P-101", patient.Id);
            Assert.AreEqual(AdministrativeGender.Female, patient.Gender);
            Assert.AreEqual(0, patient.Name.Count);
            Assert.AreEqual("Organization/1",patient.ManagingOrganization?.Reference);
            Assert.AreEqual("ORG-101", patient.ManagingOrganization?.Identifier?.Value);
        }

        [TestMethod]
        public void PatientDuplicateBirthOrderTest()
        {
            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    @"{
                        ""resourceType"":""Patient"",
                        ""multipleBirthInteger"": 3,
                        ""multipleBirthBoolean"": true
                    }",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Element 'multipleBirth[x]' must not repeat"
            );

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    @"{
                        ""resourceType"":""Patient"",
                        ""multipleBirthInteger"": 3,
                        ""_multipleBirthBoolean"": {""id"": ""MBB-1""}
                    }",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Element 'multipleBirth[x]' must not repeat"
            );

            Throws(
                () => JsonSerializer.Deserialize<Resource>(
                    @"{
                        ""resourceType"":""Patient"",
                        ""_multipleBirthInteger"": {""id"": ""MBI-1""},
                        ""multipleBirthBoolean"": true
                    }",
                    new JsonSerializerOptions().ForFhir(Model.Version.R4)
                ),
                "Element 'multipleBirth[x]' must not repeat"
            );
        }

        [TestMethod]
        public void RoundTripDstu2()
        {
            RoundTripOneExample(Model.Version.DSTU2, "testscript-example(example).xml");
            RoundTripOneExample(Model.Version.DSTU2, "TestPatient.xml");
        }

        [TestMethod]
        public void RoundTripStu3()
        {
            RoundTripOneExample(Model.Version.STU3, "testscript-example(example)-STU3-R4.xml");
            RoundTripOneExample(Model.Version.STU3, "TestPatient.xml");
        }

        [TestMethod]
        public void RoundTripR4()
        {
            RoundTripOneExample(Model.Version.R4, "testscript-example(example)-STU3-R4.xml");
            RoundTripOneExample(Model.Version.R4, "TestPatient.xml");
        }

        [TestMethod]
        public void RoundTripR4BundleJson()
        {
            var bundleJson = File.ReadAllText(GetFullPathForExample("bundle.json"));
            var bundleOldParser = new FhirJsonParser(Model.Version.R4).Parse<Resource>(bundleJson);
            var bundleNewParsr = JsonSerializer.Deserialize<Resource>(bundleJson, new JsonSerializerOptions().ForFhir(Model.Version.R4));
            Assert.IsTrue(bundleOldParser.IsExactly(bundleNewParsr));
        }

        [TestMethod]
        public void SerializeAndParseSpecialCharacters()
        {
            var patient = new Model.R4.Patient
            {
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Additional,
                    Div = @"<div xmlns=""http://www.w3.org/1999/xhtml"">
	<p>Patient Ďonald ĎUCK @ Acme Healthcare, Inc. MR = 654—321</p>
</div>"
                },
                Identifier = new List<Identifier>
                {
                    new Identifier { Value = "654—321" }
                },
                Name = new List<Model.R4.HumanName>
                {
                    new Model.R4.HumanName
                    {
                        Family = "Ďuck",
                        Given = new[] { "Ďonald" }
                    }
                }
            };
            var patientJson = new FhirJsonFastSerializer(Model.Version.R4).SerializeToString(patient);
            var parsedPatient = JsonSerializer.Deserialize<Model.R4.Patient>(patientJson, new JsonSerializerOptions().ForFhir(Model.Version.R4));
            Assert.IsTrue(patient.IsExactly(parsedPatient));
        }

        private void RoundTripOneExample(Model.Version version, string filename)
        {
            var original = File.ReadAllText(GetFullPathForExample(filename));

            var t = new FhirXmlParser(version).Parse<Resource>(original);

            var outputJson = new FhirJsonSerializer(version).SerializeToString(t);
            var t2 = JsonSerializer.Deserialize<Resource>(outputJson, new JsonSerializerOptions().ForFhir(version));
            Assert.IsTrue(t.IsExactly(t2));
        }

        private static string GetFullPathForExample(string filename) => Path.Combine("TestData", filename);

        private static T IsType<T>(object obj)
        {
            Assert.IsInstanceOfType(obj, typeof(T));
            return (T)obj;
        }

        private static T Single<T>(List<T> objs)
        {
            Assert.AreEqual(1, objs.Count);
            return objs[0];
        }

        private static JsonException Throws(Action action, string expectedMessage)
        {
            var jsonException = Assert.ThrowsException<JsonException>( action );
            Assert.IsTrue(
                jsonException.Message.StartsWith(expectedMessage),
                $"Actual message: {jsonException.Message}"
            );
            return jsonException;
        }
    }
}
