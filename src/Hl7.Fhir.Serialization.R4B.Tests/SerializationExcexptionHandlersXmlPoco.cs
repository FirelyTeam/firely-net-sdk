using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
                    ValidateOnFailedParse = true,
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
        public void XmlInvalidPatientContainedInObservation()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Observation xmlns="http://hl7.org/fhir">
                  <id value="obs" />
                  <contained>
                    <Patient>
                      <id value="pat1" />
                      <active value="new" />
                      <gender value="cat" />
                      <name>
                        <text value="demo" />
                      </name>
                    </Patient>
                  </contained>
                  <subject>
                    <reference value="#pat1" />
                  </subject>
                </Observation>
                """;

            try
            {
                var p = SerializeResource<Observation>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Observation.contained[0].Patient.birthDate", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Observation.contained[0].Patient.birthDate", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Observation", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual("Observation", oc.Issue[3].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[3].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[3].Details.Coding[0].Code);

                Assert.AreEqual(4, oc.Issue.Count);
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
        public void XMLInvalidBooleanValue()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <active value="new"/>
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

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML203", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.birthDate", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);
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
        public void XmlInvalidEmptyObservation()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Observation xmlns="http://hl7.org/fhir">
                </Observation>
                """;

            try
            {
                var p = SerializeResource<Observation>(rawData);
                DebugDump.OutputXml(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Observation", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML120", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Observation", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL105", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Observation", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual("PVAL105", oc.Issue[2].Details.Coding[0].Code);

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
                    <active value="blue"/>
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


        [TestMethod]
        public void XmlInvalidBundledResources()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Bundle xmlns="http://hl7.org/fhir">
                  <type value="searchset" />
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Patient>
                        <id value="pat1" />
                        <name>
                          <family value="Doe" />
                        </name>
                        <gender value="cat" />
                        <birthDate value="1970" />
                      </Patient>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Patient>
                        <id value="pat1" />
                        <name>
                          <family value="Doe" />
                        </name>
                        <birthDate value="1 Jan 1970" />
                      </Patient>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Patient>
                        <id value="pat1" />
                        <name>
                          <family value="Doe" />
                        </name>
                        <birthDate value="1970-01-01T12:45:00Z" />
                      </Patient>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Patient>
                        <id value="inv-prop" />
                        <name>
                          <family value="Doe" />
                        </name>
                        <name>
                          <family value="Doe2" />
                        </name>
                        <gender value="male" />
                        <birthDate value="1970-01-01" />
                      </Patient>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Observation>
                        <id value="decimal" />
                        <status value="final" />
                        <code>
                          <text value="Decimal Testing Observation" />
                        </code>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="1.0" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="1.00" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="1.0" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="0.00000000000000001" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="10000000000000000" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="0.0000000000000000000000010000" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                      </Observation>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Observation>
                        <id value="obs-int" />
                        <status value="final" />
                        <code>
                          <text value="Integer Testing Observation" />
                        </code>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueInteger value="1" />
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueInteger value="2" />
                        </component>
                      </Observation>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Observation>
                        <id value="obs-bool" />
                        <status value="final" />
                        <code>
                          <text value="Boolean Testing Observation" />
                        </code>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueBoolean value="true" />
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueBoolean value="false" />
                        </component>
                      </Observation>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Observation>
                        <id value="decimal" />
                        <status value="glarb" />
                        <code>
                          <text value="Decimal Testing Observation" />
                        </code>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <value value="0.0000000000000000000000010000" />
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                        <component>
                          <code>
                            <text value="Component" />
                          </code>
                          <valueQuantity>
                            <unit value="g" />
                          </valueQuantity>
                        </component>
                      </Observation>
                    </resource>
                  </entry>
                </Bundle>
                """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputJson(p);
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ToOperationOutcome(ex);
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                // Now check over the content to see if the error annotations were included for contained resources
                if (ex.PartialResult is Bundle b)
                {
                    foreach (var resource in b.Entry.Select(e => e.Resource))
                    {
                        var errs = resource.Annotation<List<CodedException>>();
                        Console.WriteLine($"{resource.TypeName}/{resource.Id}");
                        if (errs != null)
                            Console.WriteLine($"    {String.Join("\r\n  ", errs.Select(ce => ce.Message))}");
                    }
                }

                Assert.AreEqual("Bundle.entry[0].resource.gender", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("PVAL116", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Bundle.entry[1].resource.birthDate", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Bundle.entry[2].resource.Patient.birthDate", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual("PVAL107", oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual(12, oc.Issue.Count);
            }
        }
    }
}
