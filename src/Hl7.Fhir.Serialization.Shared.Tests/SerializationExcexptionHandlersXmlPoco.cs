using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializationExceptionHandlersXmlPoco
    {
        private T SerializeResource<T>(string xml)
            where T : Resource
        {
            using var reader = SerializationUtil.XmlReaderFromXmlText(xml);
            var settings = new DeserializerSettings()
            {
                ValidateOnFailedParse = true,
                // Validator = null
            };
            var ds = new FhirXmlDeserializer(settings);
            return (T)ds.DeserializeResource(reader);
        }
        
        [TestMethod]
        public void XMLInvalidRepeatingOnNonRepeating()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                             <Patient xmlns="http://hl7.org/fhir">
                                 <id value="pat1"/>
                                 <active value="true"/>
                                 <active value="false"/>
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML121", oc.Issue[0].Details.Coding[0].Code);
                
                Assert.AreEqual("Patient.birthDate", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE, oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);
            }
        }
        
        [TestMethod]
        public void XMLInvalidObjectWhenPrimitiveExpected()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                             <Patient xmlns="http://hl7.org/fhir">
                                 <id value="pat1"/>
                                 <active value="true">
                                     <name value="false"/>
                                     <data value="testData"/>
                                 </active>
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.active", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);

                var result = ex.PartialResult!;
                result["active"].Should().NotBeNull();
                var active = (Base)result["active"];
                active["name"].Should().NotBeNull();
                active["data"].Should().NotBeNull();
            }
        }
        
        [TestMethod]
        public void XMLInvalidPrimitiveWhenObjectExpected()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                             <Patient xmlns="http://hl7.org/fhir">
                                 <id value="pat1"/>
                                 <name value="Doe"/>
                             </Patient>
                             """;
            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                // Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML121", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(3, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidPropertySupportedWithOverflow()
        {
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                    </name>
                    <name>
                        <family value="Doe2"/>
                        <family xmlns="http://example.org/external-content" value="Doe3"/>
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                var patient = (ex.PartialResult as Patient)!;
                Assert.AreEqual("Doe2", patient.Name[1].Family);

                Assert.AreEqual("Patient.name[1]", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML112", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[1]", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("XML120", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[1]", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual("Patient", oc.Issue[3].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[3].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[3].Details.Coding[0].Code);

                Assert.AreEqual(4, oc.Issue.Count);

                patient["chicken"].Should().NotBeNull();
                patient.Name[1]["turkey"].Should().NotBeNull();
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
                OperationOutcome oc = ex.ToOperationOutcome();
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
        public void XMLInvalidMultipleSinglePropValues()
        {
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                        <family value="Doe2"/>
                    </name>
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.name[0].family", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML121", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.contact[0].gender", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL116", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);
                
                var partialResult = ex.PartialResult as Patient;
                partialResult!.Name.Should().HaveCount(1);
                partialResult!.Name[0]["family"].Should().BeEquivalentTo(new List<FhirString>{new("Doe"), new("Doe2")});
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
                      <name>
                        <text value="demo" />
                      </name>
                      <gender value="cat" />
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Observation.contained[0].active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Observation.contained[0].gender", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual(COVE.INVALID_CODED_VALUE_CODE, oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Observation.status", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual(COVE.MANDATORY_ELEMENT_CANNOT_BE_NULL_CODE, oc.Issue[2].Details.Coding[0].Code);
                Assert.IsTrue(oc.Issue[2].Details.Text.Contains("status"));

                Assert.AreEqual("Observation.code", oc.Issue[3].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[3].Severity);
                Assert.AreEqual("PVAL105", oc.Issue[3].Details.Coding[0].Code);
                Assert.IsTrue(oc.Issue[3].Details.Text.Contains("code"));

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
                        <given value="Br"/>
                        <given value="Ri"/>
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.birthDate", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE, oc.Issue[0].Details.Coding[0].Code);

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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.birthDate", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE, oc.Issue[1].Details.Coding[0].Code);

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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.birthDate", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE, oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(1, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLUnknownPropertyRoundtrip()
        {
            string rawData = """
                             <Patient xmlns="http://hl7.org/fhir">
                                 <id value="pat1"/>
                                 <test value="true" data="test"/>
                             </Patient>
                             """;

            try
            {
                var p = SerializeResource<Patient>(rawData);
                DebugDump.OutputXml(p);
                // no longer complain about unknown items
                Assert.Fail("Expected to throw parsing");
            }
            catch (DeserializationFailedException ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex.Message}");
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(1, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidPropertyOrdering()
        {
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML109", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual(2, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XmlInvalidEmptyObservation()
        {
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Observation", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual("XML120", oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Observation.status", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual("PVAL105", oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Observation.code", oc.Issue[2].Expression.First());
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Observation.component[2].value.value", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, oc.Issue[0].Details.Coding[0].Code);

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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.active", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE, oc.Issue[0].Details.Coding[0].Code);

                Assert.AreEqual("Patient.birthDate", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE, oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Patient.gender", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual(FhirXmlException.ELEMENT_OUT_OF_ORDER_CODE, oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual("Patient.gender", oc.Issue[3].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[3].Severity);
                Assert.AreEqual(COVE.INVALID_CODED_VALUE_CODE, oc.Issue[3].Details.Coding[0].Code);

                Assert.AreEqual("Patient", oc.Issue[4].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[4].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[4].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[0]", oc.Issue[5].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[5].Severity);
                Assert.AreEqual(FhirXmlException.ELEMENT_OUT_OF_ORDER_CODE, oc.Issue[5].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[0]", oc.Issue[6].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[6].Severity);
                Assert.AreEqual(FhirXmlException.ELEMENT_HAS_NO_VALUE_OR_CHILDREN_CODE, oc.Issue[6].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[0]", oc.Issue[7].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[7].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[7].Details.Coding[0].Code);
                
                Assert.AreEqual(8, oc.Issue.Count);
            }
        }

        [TestMethod]
        public void XMLInvalidDuplicateArray()
        {
            // string containing a FHIR Patient with name John Doe, 17 Jan 1970, an invalid gender and an invalid date of birth
            string rawData = """
                <Patient xmlns="http://hl7.org/fhir">
                    <id value="pat1"/>
                    <name>
                        <family value="Doe"/>
                        <chicken value="rubbish prop"/>
                    </name>
                    <active value="true"/>
                    <name>
                        <family value="Doe2"/>
                        <turkey value="rubbish prop"/>
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
                OperationOutcome oc = ex.ToOperationOutcome();
                DebugDump.OutputXml(oc);
                DebugDump.OutputXml(ex.PartialResult);

                Assert.AreEqual("Patient.name[0]", oc.Issue[0].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[0].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[0].Details.Coding[0].Code);
                
                Assert.AreEqual("Patient.active", oc.Issue[1].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[1].Severity);
                Assert.AreEqual(FhirXmlException.ELEMENT_OUT_OF_ORDER_CODE, oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[1]", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual(FhirXmlException.ELEMENT_NOT_IN_SEQUENCE_CODE, oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual("Patient.name[1]", oc.Issue[3].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[3].Severity);
                Assert.AreEqual(COVE.UNKNOWN_ELEMENT_CODE, oc.Issue[3].Details.Coding[0].Code);

                Assert.AreEqual(4, oc.Issue.Count);
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
                      <Parameters>
                        <id value="pars-int" />
                        <parameter>
                          <name value="int-one" />
                          <valueInteger value="1" />
                        </parameter>
                        <parameter>
                          <name value="int-two" />
                          <valueInteger value="2" />
                        </parameter>
                      </Parameters>
                    </resource>
                  </entry>
                  <entry>
                    <fullUrl value="https://example.org/Questionnaire/obs-comp" />
                    <resource>
                      <Parameters>
                        <id value="pars-bool" />
                        <parameter>
                          <name value="int-one" />
                          <valueBoolean value="true" />
                        </parameter>
                        <parameter>
                          <name value="int-bool" />
                          <valueBoolean value="false" />
                        </parameter>
                      </Parameters>
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
                OperationOutcome oc = ex.ToOperationOutcome();
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
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE , oc.Issue[1].Details.Coding[0].Code);

                Assert.AreEqual("Bundle.entry[2].resource.birthDate", oc.Issue[2].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[2].Severity);
                Assert.AreEqual(COVE.LITERAL_INVALID_CODE, oc.Issue[2].Details.Coding[0].Code);

                Assert.AreEqual("Bundle.entry[7].resource.status", oc.Issue[3].Expression.First());
                Assert.AreEqual(OperationOutcome.IssueSeverity.Error, oc.Issue[3].Severity);
                Assert.AreEqual(COVE.INVALID_CODED_VALUE_CODE, oc.Issue[3].Details.Coding[0].Code);

                Assert.AreEqual(4, oc.Issue.Count);
            }
        }
    }
}