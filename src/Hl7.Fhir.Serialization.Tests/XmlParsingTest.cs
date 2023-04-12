using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class XmlParsingTest
    {
        [TestMethod]
        public void WrongResourceType()
        {
            Throws<Patient>(
                "<Bundle xmlns='http://hl7.org/fhir'><type value='transaction'/></Bundle>",
                "Expected a Patient but found a Bundle"
            );
        }

        [TestMethod]
        public void UnexpectedNodeType()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'>Some text<active value='true'>More text</active>and text</Patient>";

            Throws<Patient>(xml, "Unexpected Text node");

            var patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void MalformedXml()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><active value='true'></Patient>";

            Throws<Patient>(xml, "Invalid XML: The 'active' start tag");

            Throws<Patient>(xml, "Invalid XML: The 'active' start tag", permissiveParsing: true);
        }

        [TestMethod]
        public void MalformedDivXml()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><text><div xmlns='http://www.w3.org/1999/xhtml'><p>First paragraph</div></text></Patient>";

            Throws<Patient>(xml, "Invalid XML: The 'p' start tag");

            Throws<Patient>(xml, "Invalid XML: The 'p' start tag", permissiveParsing: true);
        }

        [TestMethod]
        public void EmptyContainedResource()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><contained><OperationOutcome /></contained><active value='true'/></Patient>";

            Throws<Patient>(xml, "Empty elements are not allowed");

            var patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient);
            Assert.AreEqual(0, patient.Contained.Count);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void EmptyContainedElement()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><contained /><active value='true'/></Patient>";

            Throws<Patient>(xml, "Empty elements are not allowed");

            var patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient);
            Assert.AreEqual(0, patient.Contained.Count);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);

            xml = "<Patient xmlns='http://hl7.org/fhir'><contained></contained><active value='true'/></Patient>";

            Throws<Patient>(xml, "Empty elements are not allowed");

            patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient);
            Assert.AreEqual(0, patient.Contained.Count);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void ContainedWithInvalidAttribute()
        {
            var xml = @"
<Patient xmlns='http://hl7.org/fhir'>
    <contained value='wrong'>
        <AllergyIntolerance>
            <status value='entered-in-error'/>
        </AllergyIntolerance>
    </contained>
    <active value='true'/>
</Patient>";

            Throws<Patient>(xml, "Unknown attribute 'value' (at contained[0] line 3, 16)");

            var patient = Parse<Patient>(xml, acceptUnknownMembers: true);
            Assert.IsNotNull(patient);
            Assert.AreEqual(1, patient.Contained.Count);
            Assert.IsInstanceOfType(patient.Contained[0], typeof(AllergyIntolerance));
            Assert.AreEqual(AllergyIntoleranceStatus.EnteredInError, ((AllergyIntolerance)patient.Contained[0]).Status);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void ContainedWithExtraResource()
        {
            var xml = @"
<Patient xmlns='http://hl7.org/fhir'>
    <contained>
        <AllergyIntolerance>
            <status value='entered-in-error'/>
        </AllergyIntolerance>
        <AllergyIntolerance>
            <status value='active'/>
        </AllergyIntolerance>
    </contained>
    <active value='true'/>
</Patient>";

            Throws<Patient>(xml, "Unexpected element 'AllergyIntolerance' (at contained[0] line 7, 10)");

            var patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient);
            Assert.AreEqual(1, patient.Contained.Count);
            Assert.IsInstanceOfType(patient.Contained[0], typeof(AllergyIntolerance));
            Assert.AreEqual(AllergyIntoleranceStatus.EnteredInError, ((AllergyIntolerance)patient.Contained[0]).Status);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void ContainedSkipDifferentNamespaces()
        {
            var xml = @"
<Patient xmlns='http://hl7.org/fhir'>
    <contained>
        <!--
            Some comments
        -->
        <f:Element xmlns:f='http://myorg.com/namespaces/f'/>
        <AllergyIntolerance>
            <status value='entered-in-error'/>
        </AllergyIntolerance>
        <Element xmlns:f='http://myorg.com/namespaces/f' value='zot'>
            Some text
            <SubElement>Othe text</SubElement>
        </Element> <!-- Another comment -->
    </contained>
    <active value='true'/>
</Patient>";

            var patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient);
            Assert.AreEqual(1, patient.Contained.Count);
            Assert.IsInstanceOfType(patient.Contained[0], typeof(AllergyIntolerance));
            Assert.AreEqual(AllergyIntoleranceStatus.EnteredInError, ((AllergyIntolerance)patient.Contained[0]).Status);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void VersionSpecificElement()
        {
            var xml = @"
<Patient xmlns='http://hl7.org/fhir'>
    <active value='true'/>
    <managingOrganization>
        <reference value='Organization/1'/>
        <identifier>
            <value value='XXX'/>
        </identifier>
    </managingOrganization>
</Patient>";

            Throws<Patient>(xml, "Encountered unknown element 'identifier' (at managingOrganization.identifier line 6, 10)");

            var patient = Parse<Patient>(xml, acceptUnknownMembers: true);
            Assert.IsNotNull(patient);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
            Assert.AreEqual("Organization/1", patient.ManagingOrganization?.Reference);

            var r4patient = Parse<Model.R4.Patient>(xml, Model.Version.R4);
            Assert.IsNotNull(r4patient);
            Assert.IsNotNull(r4patient.Active);
            Assert.IsTrue(r4patient.Active.Value);
            Assert.AreEqual("Organization/1", r4patient.ManagingOrganization?.Reference);
            Assert.AreEqual("XXX", r4patient.ManagingOrganization?.Identifier?.Value);
        }

        [TestMethod]
        public void VersionSpecificPolymorphicElement()
        {
            var xml = @"
<Patient xmlns='http://hl7.org/fhir'>
    <extension url='http://mydomain.com/extensions/first'>
        <valueUrl value='https://myserver.com/segment'/>
    </extension>
    <active value='true'/>
</Patient>";

            Throws<Patient>(xml, "Encountered unknown element 'valueUrl' (at extension[0].valueUrl line 4, 10)");

            var patient = Parse<Patient>(xml, acceptUnknownMembers: true);
            Assert.IsNotNull(patient);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
            Assert.AreEqual(1, patient.Extension.Count);
            Assert.IsNull(patient.Extension[0].Value);

            var r4patient = Parse<Model.R4.Patient>(xml, Model.Version.R4);
            Assert.IsNotNull(r4patient);
            Assert.IsNotNull(r4patient.Active);
            Assert.IsTrue(r4patient.Active.Value);
            Assert.AreEqual(1, r4patient.Extension.Count);
            Assert.IsInstanceOfType(r4patient.Extension[0].Value, typeof(Url));
            Assert.AreEqual("https://myserver.com/segment", ((Url)r4patient.Extension[0].Value).Value);
        }

        [TestMethod]
        public void ErrorPath()
        {
            var xml = @"
<Patient xmlns='http://hl7.org/fhir'>
    <name>
        <family value='Doe'/>
    </name>
    <name>
        <family value='X'/>
        <given value='Y'>
            <extension url='http://mydomain.com/extensions/first'>
                <valueUrl value='https://myserver.com/segment'/>
            </extension>
        </given>
    </name>
    <active value='true'/>
</Patient>";

            Throws<Patient>(xml, "Encountered unknown element 'valueUrl' (at name[1].given[0].extension[0].valueUrl line 10, 18)");
        }

        [TestMethod]
        public void BooleanValue()
        {
            PrimitiveValue<Model.R4.Patient, bool>(
                "<Patient xmlns='http://hl7.org/fhir'>{0}</Patient>",
                "active",
                patient => patient.ActiveElement,
                new[] { ("true", true), ("false", false) },
                "34.5",
                "boolean"
            );
        }

        [TestMethod]
        public void StringValue()
        {
            PrimitiveString<Model.R4.Immunization>(
                "<Immunization xmlns='http://hl7.org/fhir'>{0}</Immunization>",
                "lotNumber",
                immunization => immunization.LotNumberElement,
                "Lot-123",
                ""
            );
        }

        [TestMethod]
        public void DecimalValue()
        {
            PrimitiveValue<Model.R4.Observation, decimal>(
                "<Observation xmlns='http://hl7.org/fhir'><valueQuantity>{0}<unit value='m'/></valueQuantity></Observation>",
                "value",
                observation => ((Quantity)observation.Value).ValueElement,
                "-5678.23",
                -5678.23M,
                "ZZZ",
                "decimal"
            );
        }

        [TestMethod]
        public void IntegerValue()
        {
            PrimitiveValue<Model.R4.Observation, int>(
                "<Observation xmlns='http://hl7.org/fhir'>{0}</Observation>",
                "valueInteger",
                observation => ((Integer)observation.Value),
                "-5678",
                -5678,
                "ZZZ",
                "integer"
            );
        }

        [TestMethod]
        public void PositiveIntValue()
        {
            PrimitiveValue<Model.R4.ExplanationOfBenefit, int>(
                "<ExplanationOfBenefit xmlns='http://hl7.org/fhir'><item>{0}<productOrService><text value='Something'/></productOrService></item></ExplanationOfBenefit>",
                "sequence",
                explanationOfBenefit => explanationOfBenefit.Item[0].SequenceElement,
                "1",
                1,
                "0",
                "positive integer"
            );
        }

        [TestMethod]
        public void UnsignedIntValue()
        {
            PrimitiveValue<Model.R4.DiagnosticReport, int>(
                "<DiagnosticReport xmlns='http://hl7.org/fhir'><presentedForm>{0}<contentType value='text/plain'/></presentedForm></DiagnosticReport>",
                "size",
                diagnosticReport => diagnosticReport.PresentedForm[0].SizeElement,
                "0",
                0,
                "-1",
                "unsigned integer"
            );
        }

        [TestMethod]
        public void DateTimeOffsetValue()
        {
            PrimitiveValue<Model.R4.Observation, DateTimeOffset>(
                "<Observation xmlns='http://hl7.org/fhir'>{0}</Observation>",
                "effectiveInstant",
                observation => (Instant)observation.Effective,
                "1981-07-19T22:00:54.234-07:30",
                DateTimeOffset.Parse("1981-07-19T22:00:54.234-07:30"),
                "ZZZ",
                "instant"
            );
        }

        [TestMethod]
        public void BinaryValue()
        {
            PrimitiveBinary<Model.R4.DiagnosticReport>(
                "<DiagnosticReport xmlns='http://hl7.org/fhir'><presentedForm>{0}<contentType value='text/plain'/></presentedForm></DiagnosticReport>",
                "data",
                diagnosticReport => diagnosticReport.PresentedForm[0].DataElement,
                new byte[] { 0x23, 0x23, 0x35, 0x36, 0x37, 0x38, 0x39, 0x40, 0x4a, 0x4b, 0x4c },
                "ZZZ",
                "base64 binary"
            );
        }

        [TestMethod]
        public void CodeValue()
        {
            PrimitiveString<Model.R4.DiagnosticReport>(
                "<DiagnosticReport xmlns='http://hl7.org/fhir'><presentedForm><url value='https://someserver.com/data/0'/>{0}</presentedForm></DiagnosticReport>",
                "contentType",
                diagnosticReport => diagnosticReport.PresentedForm[0].ContentTypeElement,
                "text/plain",
                ""
            );
        }

        [TestMethod]
        public void CodeEnumValue()
        {
            PrimitiveEnum<Model.R4.DiagnosticReport, Model.R4.DiagnosticReportStatus>(
                "<DiagnosticReport xmlns='http://hl7.org/fhir'>{0}</DiagnosticReport>",
                "status",
                diagnosticReport => diagnosticReport.StatusElement,
                Model.R4.DiagnosticReportStatus.Final,
                "ZZZ"
            );
        }

        [TestMethod]
        public void DateValue()
        {
            PrimitiveString<Model.R4.Patient>(
                "<Patient xmlns='http://hl7.org/fhir'>{0}</Patient>",
                "birthDate",
                patient => patient.BirthDateElement,
                "1981-07-19",
                "34.5",
                "date"
            );
        }

        [TestMethod]
        public void DateTimeValue()
        {
            PrimitiveString<Model.R4.Observation>(
                "<Observation xmlns='http://hl7.org/fhir'>{0}</Observation>",
                "effectiveDateTime",
                observation => (FhirDateTime)observation.Effective,
                "1981-07-19T22:00:54.234-07:30",
                "ZZZ",
                "date-time"
            );
        }

        [TestMethod]
        public void TimeValue()
        {
            PrimitiveString<Model.R4.Observation>(
                "<Observation xmlns='http://hl7.org/fhir'>{0}</Observation>",
                "valueTime",
                observation => (Time)observation.Value,
                "13:27:42.56791",
                "ZZZ",
                "time"
            );
        }

        [TestMethod]
        public void MarkdownValue()
        {
            PrimitiveString<Model.R4.CapabilityStatement>(
                "<CapabilityStatement xmlns='http://hl7.org/fhir'><rest><mode value='server'/>{0}</rest></CapabilityStatement>",
                "documentation",
                capabilityStatement => capabilityStatement.Rest[0].DocumentationElement,
                "### Headline",
                ""
            );
        }

        [TestMethod]
        public void IdValue()
        {
            PrimitiveString<Model.R4.Observation>(
                "<Observation xmlns='http://hl7.org/fhir'>{0}</Observation>",
                "id",
                observation => observation.IdElement,
                "obs-123",
                ""
            );
        }

        [TestMethod]
        public void UriValue()
        {
            PrimitiveString<Model.R4.Bundle>(
                "<Bundle xmlns='http://hl7.org/fhir'><link>{0}<relation value='next'/></link></Bundle>",
                "url",
                bundle => bundle.Link[0].UrlElement,
                "http://something.com/other",
                ""
            );
        }

        [TestMethod]
        public void UrlValue()
        {
            PrimitiveString<Model.R4.DiagnosticReport>(
                "<DiagnosticReport xmlns='http://hl7.org/fhir'><presentedForm><contentType value='text/plain'/>{0}</presentedForm></DiagnosticReport>",
                "url",
                diagnosticReport => diagnosticReport.PresentedForm[0].UrlElement,
                "https://someserver.com/data/0",
                ""
            );
        }

        [TestMethod]
        public void UuidValue()
        {
            PrimitiveString<Parameters>(
                "<Parameters xmlns='http://hl7.org/fhir'><parameter>{0}<name value='P'/></parameter></Parameters>",
                "valueUuid",
                parameters => (Uuid)parameters.Parameter[0].Value,
                "urn:uuid:c757873d-ec9a-4326-a141-556f43239520",
                ""
            );
        }

        [TestMethod]
        public void OidValue()
        {
            PrimitiveString<Parameters>(
                "<Parameters xmlns='http://hl7.org/fhir'><parameter>{0}<name value='P'/></parameter></Parameters>",
                "valueOid",
                parameters => (Oid)parameters.Parameter[0].Value,
                "urn:oid:1.2.3.4",
                ""
            );
        }

        [TestMethod]
        public void CanonicalValue()
        {
            PrimitiveString<Parameters>(
                "<Parameters xmlns='http://hl7.org/fhir'><meta>{0}<source value='http://myserver.com/source'/></meta></Parameters>",
                "profile",
                parameters => parameters.Meta.ProfileElement.FirstOrDefault(),
                "https://myserver.com/profiles/first",
                ""
            );
        }

        [TestMethod]
        public void ResourceLanguage()
        {
            var parameters = Parse<Parameters>("<Parameters xmlns='http://hl7.org/fhir'><language value='en-US'/></Parameters>");
            Assert.AreEqual("en-US", parameters.Language);
        }

        [TestMethod]
        public void ResourceImplicitRules()
        {
            var parameters = Parse<Parameters>("<Parameters xmlns='http://hl7.org/fhir'><implicitRules value='https://mysite.com/rules'/></Parameters>");
            Assert.AreEqual("https://mysite.com/rules", parameters.ImplicitRules);
        }

        [TestMethod]
        public void EmptyElementId()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><active id='' value='true'/></Patient>";
            Throws<Patient>(
                xml,
                "Empty strings are not allowed"
            );
            var patient = Parse<Patient>(xml, permissiveParsing: true);
            Assert.IsNotNull(patient.Active);
            Assert.AreEqual(true, patient.Active.Value);
            Assert.IsNull(patient.ActiveElement.ElementId);
        }

        [TestMethod]
        public void ExtensionUrl()
        {
            AssertSuccess(
                "<Patient xmlns='http://hl7.org/fhir'><extension url='http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex'><valueCode value='F'/></extension></Patient>",
                "http://hl7.org/fhir/us/core/StructureDefinition/us-core-birthsex", "F"
            );

            AssertSuccessAndError(
                "<Patient xmlns='http://hl7.org/fhir'><extension url='\t\t'/></Patient>",
                null, null,
                "Empty strings are not allowed"
            );

            void AssertSuccessAndError(string patientXml, string expectedUrl, string expectedCode, string expectedErrorMessage)
            {
                AssertSuccess(patientXml, expectedUrl, expectedCode, permissiveParsing: true);
                Throws<Patient>(
                    patientXml,
                    expectedErrorMessage
                );
            }

            void AssertSuccess(string patientXml, string expectedUrl, string expectedCode, bool permissiveParsing = false)
            {
                var patient = Parse<Patient>(
                    patientXml,
                    permissiveParsing: permissiveParsing
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
        public void WhitespaceRoundtrip()
        {
            var markdown = @"# Headline

This is the **first** paragraph

This is a list

- first

- second

- third with a [link](http://something.com)";

            var capabilityStatement = new Model.R4.CapabilityStatement
            {
                Rest = new List<Model.R4.CapabilityStatement.RestComponent>
                {
                    new Model.R4.CapabilityStatement.RestComponent
                    {
                        Documentation = markdown
                    }
                }
            };
            var xml = new FhirXmlFastSerializer(Model.Version.R4).SerializeToString(capabilityStatement);
            var parsedCapabilityStatement = Parse<Model.R4.CapabilityStatement>(xml, Model.Version.R4);
            Assert.AreEqual(markdown, parsedCapabilityStatement.Rest[0].Documentation);
        }

        private static void PrimitiveString<TResource>(
            string resourceFormat,
            string elementName,
            Func<TResource, Primitive<string>> getElement,
            string validValue,
            string invalidValue = null,
            string typeName = null
        )
            where TResource : Resource
        {
            AssertSuccess(
                $"<{elementName} value='{validValue}'/>",
                validValue, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1' value='{validValue}'/>",
                validValue, "ACT-1"
            );

            AssertSuccess(
                string.Empty,
                null, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1'/>",
                null, "ACT-1"
            );

            if (invalidValue != null)
            {
                AssertSuccessAndError(
                    $"<{elementName} value='{invalidValue}'/>",
                    typeName == null ?
                        "Empty strings are not allowed" :
                        $"'{invalidValue}' is not a valid {typeName}"
                );
            }

            void AssertSuccessAndError(string elementXml, string expectedErrorMessage)
            {
                AssertSuccess(elementXml, null, null, permissiveParsing: true);
                Throws<TResource>(
                   string.Format(resourceFormat, elementXml),
                   expectedErrorMessage,
                   Model.Version.R4
                );
            }

            void AssertSuccess(string elementXml, string expectedValue, string expectedId, bool permissiveParsing = false)
            {
                var resourceXml = string.Format(resourceFormat, elementXml);
                var resource = Parse<TResource>(resourceXml, Model.Version.R4, permissiveParsing: permissiveParsing);
                var element = getElement(resource);
                if (expectedValue == null && expectedId == null)
                {
                    Assert.IsNull(element);
                }
                else
                {
                    Assert.AreEqual(expectedValue, (string)element.ObjectValue);
                    Assert.AreEqual(expectedId, element.ElementId);
                }
            }
        }

        private static void PrimitiveBinary<TResource>(
            string resourceFormat,
            string elementName,
            Func<TResource, Primitive<byte[]>> getElement,
            byte[] validValue,
            string invalidValue,
            string typeName
        )
            where TResource : Resource
        {
            var validValueString = Convert.ToBase64String(validValue);
            AssertSuccess(
                $"<{elementName} value='{validValueString}'/>",
                validValue, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1' value='{validValueString}'/>",
                validValue, "ACT-1"
            );

            AssertSuccess(
                string.Empty,
                null, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1'/>",
                null, "ACT-1"
            );

            AssertSuccessAndError(
                $"<{elementName} value='{invalidValue}'/>",
                $"'{invalidValue}' is not a valid {typeName}"
            );

            void AssertSuccessAndError(string elementXml, string expectedErrorMessage)
            {
                AssertSuccess(elementXml, null, null, permissiveParsing: true);
                Throws<TResource>(
                   string.Format(resourceFormat, elementXml),
                   expectedErrorMessage,
                   Model.Version.R4
                );
            }

            void AssertSuccess(string elementXml, byte[] expectedValue, string expectedId, bool permissiveParsing = false)
            {
                var resourceXml = string.Format(resourceFormat, elementXml);
                var resource = Parse<TResource>(resourceXml, Model.Version.R4, permissiveParsing: permissiveParsing);
                var element = getElement(resource);
                if (expectedValue == null && expectedId == null)
                {
                    Assert.IsNull(element);
                }
                else
                {
                    if (expectedValue == null)
                    {
                        Assert.IsNull(element.ObjectValue);
                    }
                    else
                    {
                        Assert.AreEqual(Convert.ToBase64String(expectedValue), Convert.ToBase64String((byte[])element.ObjectValue));
                    }
                    Assert.AreEqual(expectedId, element.ElementId);
                }
            }
        }

        private static void PrimitiveValue<TResource, TValue>(
            string resourceFormat,
            string elementName,
            Func<TResource, Primitive<TValue?>> getElement,
            string validValueString, TValue validValue,
            string invalidValue,
            string typeName
        )
            where TResource : Resource
            where TValue : struct
        {
            PrimitiveValue(
                resourceFormat,
                elementName,
                getElement,
                new[] { (validValueString, validValue) },
                invalidValue,
                typeName
            );
        }

        private static void PrimitiveValue<TResource, TValue>(
            string resourceFormat,
            string elementName,
            Func<TResource, Primitive<TValue?>> getElement,
            (string ValueString, TValue Value)[] validValues,
            string invalidValue,
            string typeName
        )
            where TResource : Resource
            where TValue : struct
        {
            foreach (var (str, val) in  validValues)
            {
                AssertSuccess(
                    $"<{elementName} value='{str}'/>",
                    val, null
                );
            }

            var (validValueString, validValue) = validValues[0];

            AssertSuccess(
                $"<{elementName} id='ACT-1' value='{validValueString}'/>",
                validValue, "ACT-1"
            );

            AssertSuccess(
                string.Empty,
                null, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1'/>",
                null, "ACT-1"
            );

            AssertSuccessAndError(
                $"<{elementName} value='{invalidValue}'/>",
                $"'{invalidValue}' is not a valid {typeName}"
            );

            void AssertSuccessAndError(string elementXml, string expectedErrorMessage)
            {
                AssertSuccess(elementXml, null, null, permissiveParsing: true);
                Throws<TResource>(
                   string.Format(resourceFormat, elementXml),
                   expectedErrorMessage,
                   Model.Version.R4
                );
            }

            void AssertSuccess(string elementXml, TValue? expectedValue, string expectedId, bool permissiveParsing = false)
            {
                var resourceXml = string.Format(resourceFormat, elementXml);
                var resource = Parse<TResource>(resourceXml, Model.Version.R4, permissiveParsing: permissiveParsing);
                var element = getElement(resource);
                if (expectedValue == null && expectedId == null)
                {
                    Assert.IsNull(element);
                }
                else
                {
                    Assert.AreEqual(expectedValue, (TValue?)element.ObjectValue);
                    Assert.AreEqual(expectedId, element.ElementId);
                }
            }
        }

        private static void PrimitiveEnum<TResource, TEnum>(
            string resourceFormat,
            string elementName,
            Func<TResource, Code<TEnum>> getElement,
            TEnum validValue,
            string invalidValue
        )
            where TResource : Resource
            where TEnum : struct
        {
            var validValueString = ((Enum)(object)validValue).GetLiteral();
            AssertSuccess(
                $"<{elementName} value='{validValueString}'/>",
                validValue, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1' value='{validValueString}'/>",
                validValue, "ACT-1"
            );

            AssertSuccess(
                string.Empty,
                null, null
            );

            AssertSuccess(
                $"<{elementName} id='ACT-1'/>",
                null, "ACT-1"
            );

            AssertSuccessAndError(
                $"<{elementName} value='{invalidValue}'/>",
                $"'{invalidValue}' is not a valid {typeof(TEnum).Name}"
            );

            void AssertSuccessAndError(string elementXml, string expectedErrorMessage)
            {
                AssertSuccess(elementXml, null, null, permissive: true);
                Throws<TResource>(
                   string.Format(resourceFormat, elementXml),
                   expectedErrorMessage,
                   Model.Version.R4
                );
            }

            void AssertSuccess(string elementXml, TEnum? expectedValue, string expectedId, bool permissive = false)
            {
                var resourceXml = string.Format(resourceFormat, elementXml);
                var resource = Parse<TResource>(resourceXml, Model.Version.R4, allowUnrecognizedEnums: permissive, permissiveParsing: permissive);
                var element = getElement(resource);
                if (expectedValue == null && expectedId == null)
                {
                    Assert.IsNull(element);
                }
                else
                {
                    Assert.AreEqual(expectedValue, element.Value);
                    Assert.AreEqual(expectedId, element.ElementId);
                }
            }
        }

        private static void Throws<TResource>(
            string xml,
            string expectedErrorMessage,
            Model.Version version = Model.Version.DSTU2,
            bool permissiveParsing = false,
            bool allowUnrecognizedEnums = false,
            bool acceptUnknownMembers = false,
            bool disallowXsiAttributesOnRoot = false
        )
            where TResource : Base
        {
            var exception = Assert.ThrowsException<FormatException>(
                () => Parse<TResource>(xml, version, permissiveParsing, allowUnrecognizedEnums, acceptUnknownMembers, disallowXsiAttributesOnRoot)
            );
            Assert.IsTrue(exception.Message.Contains(expectedErrorMessage), $"Unexpected <{exception.Message}>");
        }

        private static TResource Parse<TResource>(
            string xml,
            Model.Version version = Model.Version.DSTU2,
            bool permissiveParsing = false,
            bool allowUnrecognizedEnums = false,
            bool acceptUnknownMembers = false,
            bool disallowXsiAttributesOnRoot = false
        )
            where TResource : Base
        {
            var settings = new ParserSettings(version)
            {
                PermissiveParsing = permissiveParsing,
                AllowUnrecognizedEnums = allowUnrecognizedEnums,
                AcceptUnknownMembers = acceptUnknownMembers,
                DisallowXsiAttributesOnRoot = disallowXsiAttributesOnRoot
            };
            var parser = new FhirXmlParser(settings);
            return parser.Parse<TResource>(xml);
        }

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
    }
}
