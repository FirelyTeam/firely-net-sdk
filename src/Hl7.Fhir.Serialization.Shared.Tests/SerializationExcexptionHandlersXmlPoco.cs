using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Serialization.Tests;

[TestClass]
public class SerializationExceptionHandlersXmlPoco
{
    private static T deserializeResource<T>(string xml)
        where T : Resource
    {
        using var reader = SerializationUtil.XmlReaderFromXmlText(xml);
        var ds = new FhirXmlDeserializer();
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
            var p = deserializeResource<Patient>(rawData);
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
            var p = deserializeResource<Patient>(rawData);
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
}