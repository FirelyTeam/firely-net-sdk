using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializationExceptionHandlersXml
    {
        [TestMethod, Ignore] // ignored as this is intended behaviour that the code doesn't do (yet)
        public void XMLInvalidEnumerationValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string xmlPatient = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <birthDate value="1 Jan 1970"/>
                    <gender value="cat"/>
                    <chicken value="rubbish prop"/>
                    <name><family value="Doe"/></name>
                </Patient>
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
            var xs = new FhirXmlParser(parserSettings);
            var p = xs.Parse<Patient>(xmlPatient);
            DebugDump.OutputXml(p);
        }

        [TestMethod]
        public void XMLInvalidDateValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string xmlPatient = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <birthDate value="1 Jan 1970"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                </Patient>
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
            var xs = new FhirXmlParser(parserSettings);
            var p = xs.Parse<Patient>(xmlPatient);
            DebugDump.OutputXml(p);
        }

        [TestMethod]
        public void XMLInvalidDateValueWithTime()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string xmlPatient = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <birthDate value="1970-01-01T12:45:00Z"/>
                    <chicken value="rubbish prop"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                </Patient>
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
            var xs = new FhirXmlParser(parserSettings);
            var p = xs.Parse<Patient>(xmlPatient);
            DebugDump.OutputXml(p);
        }

        [TestMethod]
        public void XMLInvalidPropertyOrdering()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string xmlPatient = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <birthDate value="1970-01-01"/>
                    <gender Value="male"/>
                    <chicken value="rubbish prop"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                    <active value="true"/>
                </Patient>
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
            var xs = new FhirXmlParser(parserSettings);
            var p = xs.Parse<Patient>(xmlPatient);

            DebugDump.OutputXml(p);
        }

        [TestMethod]
        public void XMLInvalidPropertyDetected()
        {
            string xmlPatient = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <birthDate value="1970-01-01"/>
                    <gender value="male"/>
                    <chicken value="rubbish prop"/>
                </Patient>
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
            var xs = new FhirXmlParser(parserSettings);
            var p = xs.Parse<Patient>(xmlPatient);
            DebugDump.OutputXml(p);
        }

        [TestMethod, Ignore] // ignored as this is intended behaviour that the code doesn't do (yet)
        public void XMLInvalidDecimalValue()
        {
            string xml = """
                <Observation xmlns="http://hl7.org/fhir">
                	<id value="decimal"/>
                	<status value="final"/>
                	<code>
                    <text value="Decimal Testing Observation"/>
                	</code>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="1.0"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="1.00"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="1.0e0"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="0.00000000000000001"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="10000000000000000"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="1.00000000000000000e-24"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                  <component>
                    <code>
                      <text value="Component"/>
                    </code>
                    <valueQuantity>
                      <value value="-1.00000000000000000e245"/>
                      <unit value="g"/>
                    </valueQuantity>
                  </component>
                </Observation>
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
            var xs = new FhirXmlParser(parserSettings);
            var p = xs.Parse<Observation>(xml);
            DebugDump.OutputXml(p);
        }
    }
}
