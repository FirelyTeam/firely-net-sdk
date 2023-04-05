using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializationExceptionHandlersXmlPoco
    {
        /// <summary>
        /// Convert to an OperationOutcome
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static OperationOutcome ToOperationOutcome(DeserializationFailedException ex)
        {
            // Need to convert the list of general exceptions into an OperationOutcome.
            OperationOutcome oc = new OperationOutcome();
            foreach (var e in ex.Exceptions)
            {
                var issue =
                new OperationOutcome.IssueComponent()
                {
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Code = OperationOutcome.IssueType.Invalid
                };
                if (e is CodedWithLocationException ecl)
                {
                    issue = ecl.ToIssue();
                }
                oc.Issue.Add(issue);
            }

            return oc;
        }

        private T SerializeResource<T>(string xml)
            where T : Resource
        {
            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml))
            {
                FhirXmlPocoDeserializerSettings settings = new FhirXmlPocoDeserializerSettings()
                {
                    // Validator = null
                };
                FhirXmlPocoDeserializer ds = new FhirXmlPocoDeserializer(settings);
                return (T)ds.DeserializeResource(reader);
            }
        }

        [TestMethod]
        public void XMLInvalidEnumerationValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                    <gender value="cat"/>
                    <birthDate value="1970"/>
                    <contact>
                        <name>
                            <text value="brian"/>
                        </name>
                        <gender value="cat"/>
                    </contact>
                </Patient>
                """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.gender", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("PVAL116", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.contact[0].gender", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL116", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidDateValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                    <birthDate value="1 Jan 1970"/>
                </Patient>
                """;
            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.birthDate", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(1, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidDateValueWithTime()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                    <birthDate value="1970-01-01T12:45:00Z"/>
                </Patient>
                """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.birthDate", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(1, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidPropertyOrdering()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <gender value="male"/>
                    <birthDate value="1970-01-01"/>
                    <active value="true"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                </Patient>
                """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML109", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[0]", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("XML109", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidPropertyDetected()
        {
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                    <name>
                        <family xmlns="http://example.org/external-content" value="Doe2"/>
                        <family value="Doe2"/>
                        <turkey value2="rubbish prop"/>
                    </name>
                    <chicken value="rubbish prop"/>
                    <gender value="male"/>
                    <birthDate value="1970-01-01"/>
                </Patient>
                """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.name[1]", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML112", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[1]", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("XML104", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Patient", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual("XML104", oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual(3, oc.Issue.Count);
            }
        }

        [TestMethod]
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

            try
            {
                var p = SerializeResource<Observation>(xml);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Observation.component[2].value.value", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML203", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(1, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLMixedInvalidParseIssues()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <birthDate value="1 Jan 1970"/>
                    <gender value="cat"/>
                    <chicken value="rubbish prop"/>
                    <name>
                        <family value="Doe"/>
                        <turkey value2="rubbish prop"/>
                    </name>
                </Patient>
                """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);
            }
        }
    }
}
