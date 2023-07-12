using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializationExceptionHandlersJson
    {
        [TestMethod, Ignore] // ignored as this is intended behaviour that the code doesn't do (yet)
        public void JsonInvalidEnumerationValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                {
                  "resourceType": "Patient",
                  "id": "pat1",
                  "name": [
                    {
                      "family": "Doe"
                    }
                  ],
                  "chicken": "rubbish prop",
                  "gender": "cat",
                  "birthDate": "1 Jan 1970"
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Patient>(rawData);
            DebugDump.OutputJson(p);
        }

        [TestMethod]
        public void JsonInvalidEmptyObservation()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                {
                  "resourceType": "Observation"
                  // ,"id": "pat1"
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Observation>(rawData);
            DebugDump.OutputJson(p);
        }

        [TestMethod]
        public void JsonInvalidDateValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                {
                  "resourceType": "Patient",
                  "id": "pat1",
                  "name": [
                    {
                      "family": "Doe"
                    }
                  ],
                  "chicken": "rubbish prop",
                  "birthDate": "1 Jan 1970"
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Patient>(rawData);
            DebugDump.OutputJson(p);
        }

        [TestMethod]
        public void JsonInvalidDateValueWithTime()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                {
                  "resourceType": "Patient",
                  "id": "pat1",
                  "name": [
                    {
                      "family": "Doe"
                    }
                  ],
                  "birthDate": "1970-01-01T12:45:00Z"
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Patient>(rawData);
            DebugDump.OutputJson(p);
        }

        [TestMethod]
        public void JsonInvalidPropertyDetected()
        {
            string rawData = """
                {
                  "resourceType": "Patient",
                  "id": "pat1",
                  "name": [
                    {
                      "family": "Doe"
                    }
                  ],
                  "chicken": "rubbish prop",
                  "gender": "male",
                  "birthDate": "1970-01-01"
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Patient>(rawData);
            DebugDump.OutputJson(p);
        }

        [TestMethod, Ignore] // ignored as this is intended behaviour that the code doesn't do (yet)
        public void JsonInvalidDecimalValue()
        {
            string rawData = """
                {
                  "resourceType": "Observation",
                  "id": "decimal",
                  "status": "final",
                  "code": {
                    "text": "Decimal Testing Observation"
                  },
                  "component": [
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": 1.0,
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": 1.00,
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": 1.0e0,
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": 0.00000000000000001,
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": 10000000000000000,
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": "1.00000000000000000e-24",
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": "-1.00000000000000000e245",
                        "unit": "g"
                      }
                    }
                  ]
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Observation>(rawData);
            DebugDump.OutputJson(p);
        }

        [TestMethod, Ignore] // ignored as this is intended behaviour that the code doesn't do (yet)
        public void JsonMixedInvalidParseIssues()
        {
            string rawData = """
                {
                  "resourceType": "Observation",
                  "id": "decimal",
                  "status": "glarb",
                  "code": {
                    "text": "Decimal Testing Observation"
                  },
                  "component": [
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": "1.00000000000000000e-24",
                        "unit": "g"
                      }
                    },
                    {
                      "code": {
                        "text": "Component"
                      },
                      "valueQuantity": {
                        "value": "-1.00000000000000000e245",
                        "unit": "g"
                      }
                    }
                  ]
                }
                """;

            var parserSettings = new ParserSettings()
            {
                PermissiveParsing = false,
                ExceptionHandler = (sender, notification) =>
                {
                    System.Diagnostics.Trace.WriteLine($"{notification.Severity} {notification.Message}");
                    // System.Diagnostics.Trace.WriteLine($"{notification.Location}");
                    // intentionally not throwing here
                }
            };
            var parser = new FhirJsonParser(parserSettings);
            var p = parser.Parse<Observation>(rawData);
            DebugDump.OutputJson(p);
        }
    }
}
