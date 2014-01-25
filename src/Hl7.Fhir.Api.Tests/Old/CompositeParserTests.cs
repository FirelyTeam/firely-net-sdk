using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Parsers;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Hl7.Fhir.Serializers;


namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class CompositeParserTests
    {
        [TestMethod]
        public void NarrativeParsing()
        {
            string xmlString = @"<testNarrative xmlns='http://hl7.org/fhir'>
                                    <status value='generated' />
                                    <div xmlns='http://www.w3.org/1999/xhtml'>Whatever</div>
                                 </testNarrative>";

            ErrorList errors = new ErrorList();
            Narrative result = (Narrative)FhirParser.ParseElementFromXml(xmlString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(Narrative.NarrativeStatus.Generated, result.Status.Value);
            Assert.IsTrue(result.Div != null);

            xmlString = @"<testNarrative xmlns='http://hl7.org/fhir'>
                             <status value='generated' />
                             <xhtml:div xmlns:xhtml='http://www.w3.org/1999/xhtml'>Whatever</xhtml:div>
                          </testNarrative>";
            errors.Clear();

            result = (Narrative)FhirParser.ParseElementFromXml(xmlString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(Narrative.NarrativeStatus.Generated, result.Status.Value);
            Assert.IsTrue(result.Div != null);

            xmlString = @"<testNarrative xmlns='http://hl7.org/fhir' xmlns:xhtml='http://www.w3.org/1999/xhtml'>
                              <status value='generated' />
                              <xhtml:div>Whatever</xhtml:div>
                          </testNarrative>";
            errors.Clear();

            result = (Narrative)FhirParser.ParseElementFromXml(xmlString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(Narrative.NarrativeStatus.Generated, result.Status.Value);
            Assert.IsTrue(result.Div != null);

            string jsonString = "{ \"testNarrative\" : {" +
                "\"status\" : { \"value\" : \"generated\" }, " +
                "\"div\" : " +
                "\"<div xmlns='http://www.w3.org/1999/xhtml'>Whatever</div>\" } }";

            errors.Clear();
            result = (Narrative)FhirParser.ParseElementFromJson(jsonString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(Narrative.NarrativeStatus.Generated, result.Status.Value);
            Assert.IsTrue(result.Div != null);
        }


        [TestMethod]
        public void JsonDivZonderNamespace()
        {
            string jsonString = "{ \"testNarrative\" : {" +
                "\"status\" : { \"value\" : \"generated\" }, " +
                "\"div\" : " +
                "\"<div>Whatever</div>\" } }";

            var errors = new ErrorList();
            var result = (Narrative)FhirParser.ParseElementFromJson(jsonString, errors);
            var xml = FhirSerializer.SerializeElementAsXml(result, "testNarrative");
            Assert.IsTrue(xml.Contains("w3.org/1999/xhtml"));

            jsonString = "{ \"testNarrative\" : {" +
             "\"status\" : { \"value\" : \"generated\" }, " +
             "\"div\" : " +
             "\"<div xmlns='http://www.w3.org/1999/xhtml'>Whatever</div>\" } }";

            errors.Clear();
            result = (Narrative)FhirParser.ParseElementFromJson(jsonString, errors);
            xml = FhirSerializer.SerializeElementAsXml(result, "testNarrative");
            Assert.IsTrue(xml.Contains("w3.org/1999/xhtml"));
        }

      

        [TestMethod]
        public void BinaryParsing()
        {
            string xmlString = @"<Binary id='pic1' contentType='image/gif' xmlns='http://hl7.org/fhir'>R0lGODlhEwARAPcAAAAAAAAA/+9aAO+1AP/WAP/eAP/eCP/eEP/eGP/nAP/nCP/nEP/nIf/nKf/nUv/nWv/vAP/vCP/vEP/vGP/vIf/vKf/vMf/vOf/vWv/vY//va//vjP/3c//3lP/3nP//tf//vf///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////yH5BAEAAAEALAAAAAATABEAAAi+AAMIDDCgYMGBCBMSvMCQ4QCFCQcwDBGCA4cLDyEGECDxAoAQHjxwyKhQAMeGIUOSJJjRpIAGDS5wCDly4AALFlYOgHlBwwOSNydM0AmzwYGjBi8IHWoTgQYORg8QIGDAwAKhESI8HIDgwQaRDI1WXXAhK9MBBzZ8/XDxQoUFZC9IiCBh6wEHGz6IbNuwQoSpWxEgyLCXL8O/gAnylNlW6AUEBRIL7Og3KwQIiCXb9HsZQoIEUzUjNEiaNMKAAAA7</Binary>";

            ErrorList errors = new ErrorList();
            Binary result = (Binary)FhirParser.ParseResourceFromXml(xmlString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());

            Assert.AreEqual("image/gif", result.ContentType);
            Assert.AreEqual(993, result.Content.Length);
            Assert.IsTrue(Encoding.ASCII.GetString(result.Content).StartsWith("GIF89a"));

            byte[] data = result.Content;
            File.WriteAllBytes(@"c:\temp\test.gif", data);

            string json = "{ Binary: { contentType : \"image/gif\", " +
                        "content: \"R0lGODlhEwARAPcAAAAAAAAA/+9aAO+1AP/WAP/eAP/eCP/eEP/eGP/nAP/nCP/nEP/nIf/nKf/nUv/nWv/vAP/vCP/vEP/vGP/vIf/vKf/vMf/vOf/vWv/vY//va//vjP/3c//3lP/3nP//tf//vf///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////yH5BAEAAAEALAAAAAATABEAAAi+AAMIDDCgYMGBCBMSvMCQ4QCFCQcwDBGCA4cLDyEGECDxAoAQHjxwyKhQAMeGIUOSJJjRpIAGDS5wCDly4AALFlYOgHlBwwOSNydM0AmzwYGjBi8IHWoTgQYORg8QIGDAwAKhESI8HIDgwQaRDI1WXXAhK9MBBzZ8/XDxQoUFZC9IiCBh6wEHGz6IbNuwQoSpWxEgyLCXL8O/gAnylNlW6AUEBRIL7Og3KwQIiCXb9HsZQoIEUzUjNEiaNMKAAAA7\" } }";
            errors.Clear();
            result = (Binary)FhirParser.ParseResourceFromJson(json, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());

            Assert.AreEqual("image/gif", result.ContentType);
            Assert.AreEqual(993, result.Content.Length);
            Assert.IsTrue(Encoding.ASCII.GetString(result.Content).StartsWith("GIF89a"));
        }

        [TestMethod]
        public void ParseJsonNativeTypes()
        {
            string json = "{ testExtension: { url: { value : \"http://bla.com\" }," +  
                        "valueInteger: { value: 14 } } }";
            string json2 = "{ testExtension: { url: { value : \"http://bla.com\" }," +
                        "valueBoolean: { value: true } } }";

            var errors = new ErrorList();
            var result = (Extension)FhirParser.ParseElementFromJson(json, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(14, ((Integer)result.Value).Value.Value);

            errors.Clear();
            result = (Extension)FhirParser.ParseElementFromJson(json2, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(true, ((FhirBoolean)result.Value).Value.Value);

            string jsonWrong = "{ testExtension: { url: { value : \"http://bla.com\" }," +
                        "valueInteger: { value: \"14\" } } }";
            errors.Clear();
            result = (Extension)FhirParser.ParseElementFromJson(jsonWrong, errors);
            Assert.IsTrue(errors.Count() > 0);
            Assert.IsTrue(errors.ToString().Contains("Expected") &&
                        errors.ToString().Contains("Boolean"),errors.ToString());
        }

        [TestMethod]
        public void ParseSimpleComposite()
        {
            string xmlString = @"<testCoding id='x4' xmlns='http://hl7.org/fhir'>
                                    <system value='http://hl7.org/fhir/sid/icd-10' />
                                    <code value='G44.1' />
                                 </testCoding>";

            ErrorList errors = new ErrorList();
            Coding result = (Coding)FhirParser.ParseElementFromXml(xmlString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual("x4", result.Id.ToString());
            Assert.AreEqual("G44.1", result.Code);
            Assert.AreEqual("http://hl7.org/fhir/sid/icd-10", result.System.ToString());
            Assert.IsNull(result.Display);

            string jsonString = "{ \"testCoding\" : { \"_id\" : \"x4\", " +
                    "\"system\": { \"value\" : \"http://hl7.org/fhir/sid/icd-10\" }, " +
                    "\"code\": { \"value\" : \"G44.1\" } } }";

            errors.Clear();
            result = (Coding)FhirParser.ParseElementFromJson(jsonString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual("x4", result.Id);
            Assert.AreEqual("G44.1", result.Code);
            Assert.AreEqual("http://hl7.org/fhir/sid/icd-10", result.System.ToString());
            Assert.IsNull(result.Display);
        }


        [TestMethod]
        public void CompositeWithRepeatingElement()
        {
            string xmlString = @"
                <testCodeableConcept xmlns='http://hl7.org/fhir'>
                    <coding>
                        <system value=""http://hl7.org/fhir/sid/icd-10"" />
                        <code value=""R51"" />
                    </coding>
                    <coding id='1'>
                        <system value=""http://snomed.info/id"" />
                        <code value=""25064002"" />
                    </coding>
                </testCodeableConcept>";

            ErrorList errors = new ErrorList();
            CodeableConcept result = (CodeableConcept)FhirParser.ParseElementFromXml(xmlString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(2, result.Coding.Count);
            Assert.AreEqual("R51", result.Coding[0].Code);
            Assert.AreEqual("25064002", result.Coding[1].Code);
            Assert.AreEqual("http://snomed.info/id", result.Coding[1].System.ToString());
            Assert.AreEqual("1", result.Coding[1].Id.ToString());


            string jsonString = @"{ ""testCodeableConcept"" : 
                    { ""coding"" : [ 
                        { ""system"" : { ""value"" : ""http://hl7.org/fhir/sid/icd-10"" },
                          ""code"" : { ""value"" : ""R51"" } },
                        { ""_id"" : ""1"", 
                          ""system"": { ""value"" : ""http://snomed.info/id"" },
                          ""code"" : { ""value"" : ""25064002"" } } ]
                    } }";

            errors.Clear();
            result = (CodeableConcept)FhirParser.ParseElementFromJson(jsonString, errors);
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.AreEqual(2, result.Coding.Count);
            Assert.AreEqual("R51", result.Coding[0].Code);
            Assert.AreEqual("25064002", result.Coding[1].Code);
            Assert.AreEqual("http://snomed.info/id", result.Coding[1].System.ToString());
            Assert.AreEqual("1", result.Coding[1].Id.ToString());
        }
        

        [TestMethod]
        public void ParseUnknownMembersAndRecover()
        {
            string xmlString = @"<testCodeableConcept xmlns='http://hl7.org/fhir'>
                    <coding>
                        <system value='http://hl7.org/fhir/sid/icd-10' />
                        <ewout>bla</ewout>
                        <code value='R51' />
                    </coding>
                    <coding id='1'>
                        <system value='http://snomed.info/id' />
                        <code value='25064002' />
                    </coding>
                    <grahame></grahame>
                    </testCodeableConcept>";

            ErrorList errors = new ErrorList();
            CodeableConcept result = (CodeableConcept)FhirParser.ParseElementFromXml(xmlString, errors);
            Assert.AreEqual(2,errors.Count);
            Assert.IsTrue(errors[0].ToString().Contains("ewout"));
            Assert.IsTrue(errors[1].ToString().Contains("grahame"));

            string jsonString = @"{ ""testCodeableConcept"" : 
                    { ""coding"" : [
                        { ""system"": { ""value"" : ""http://hl7.org/fhir/sid/icd-10"" }, 
                          ""ewout"" : ""bla"", 
                          ""code"" : { ""value"" : ""R51"" } 
                        },
                        { ""_id"" : ""1"", 
                          ""system"": { ""value"" : ""http://snomed.info/id"" }, 
                          ""code"" : { ""value"" : ""25064002""  }
                        } ],
                       ""grahame"" : { ""value"" : ""x"" } } }";

            errors.Clear();
            result = (CodeableConcept)FhirParser.ParseElementFromJson(jsonString, errors);
            Assert.AreEqual(2, errors.Count);
            Assert.IsTrue(errors[0].ToString().Contains("ewout"));
            Assert.IsTrue(errors[1].ToString().Contains("grahame"));
        }


        [TestMethod]
        public void ParseNameWithExtensions()
        {
            string xmlString =
                @"<Patient xmlns='http://hl7.org/fhir'>
                    <name>
                        <use value='official' />  
                        <given value='Regina' />
                        <prefix value='Dr.'>
                        <extension>
                            <url value='http://hl7.org/fhir/profile/@iso-20190' />
                            <valueCoding>
                                <system value='urn:oid:2.16.840.1.113883.5.1122' />       
                                <code value='AC' />
                            </valueCoding>
                        </extension>
                        </prefix>
                    </name>
                    <text>
                        <status value='generated' />
                        <div xmlns='http://www.w3.org/1999/xhtml'>Whatever</div>
                    </text>
                </Patient>";

            ErrorList errors = new ErrorList();
            Patient p = (Patient)FhirParser.ParseResourceFromXml(xmlString, errors);

            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.IsNotNull(p);
            Assert.AreEqual(1, p.Name[0].PrefixElement[0].Extension.Count());
        }


        [TestMethod]
        public void ParseLargeComposite()
        {
            XmlReader xr = XmlReader.Create(new StreamReader(@"..\..\..\..\..\publish\diagnosticreport-example.xml"));
            ErrorList errors = new ErrorList();
            DiagnosticReport rep = (DiagnosticReport)FhirParser.ParseResource(xr, errors);

            validateDiagReportAttributes(errors, rep);

            JsonTextReader jr = new JsonTextReader(new StreamReader(@"..\..\..\..\..\publish\diagnosticreport-example.json"));
            errors.Clear();
            rep = (DiagnosticReport)FhirParser.ParseResource(jr, errors);

            validateDiagReportAttributes(errors, rep);
        }

        private static void validateDiagReportAttributes(ErrorList errors, DiagnosticReport rep)
        {
            Assert.IsTrue(errors.Count() == 0, errors.ToString());
            Assert.IsNotNull(rep);

            Assert.AreEqual("2011-03-04T08:30:00+11:00", rep.Issued.ToString());
            Assert.AreEqual(17, rep.Contained.Count);
            Assert.AreEqual(17, rep.Results.Result.Count);

            Assert.IsNotNull(rep.Contained[1] as Observation);
            Observation obs1 = (Observation)rep.Contained[1];
            Assert.AreEqual(typeof(Quantity), obs1.Value.GetType());
            Assert.AreEqual((decimal)5.9, (obs1.Value as Quantity).Value.Value);

            Assert.IsNotNull(rep.Contained[8] as Observation);
            Observation obs8 = (Observation)rep.Contained[8];
            Assert.AreEqual("Neutrophils", obs8.Name.Coding[0].Display);
        }


        [TestMethod]
        public void TestDavidMessage()
        {
            var xml = File.ReadAllText(@"c:\temp\forEwout.xml");

            var errors = new ErrorList();
            
            var result = FhirParser.ParseBundleFromXml(xml, errors);
        }


        [TestMethod]
        public void ParsePerformance()
        {
            //string file = @"..\..\..\loinc.json";
            string file = @"..\..\..\..\..\publish\diagnosticreport-example.xml";
           
            int repeats = 20;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            sw.Start();

            ErrorList errors = new ErrorList();

            for (int i = 0; i < repeats; i++)
            {
                errors.Clear();
                var xmlr = XmlReader.Create(file);
                //var jsonr = new JsonTextReader(new System.IO.StreamReader(file));
                //var rep = FhirParser.ParseResource(jsonr, errors);
                var rep = FhirParser.ParseResource(xmlr, errors);
            }

            Assert.IsTrue(errors.Count == 0, errors.ToString());

            sw.Stop();

            FileInfo f = new FileInfo(file);
            long bytesPerMs = f.Length * repeats / sw.ElapsedMilliseconds;

            File.WriteAllText(@"c:\temp\speedtest.txt", bytesPerMs.ToString() + " bytes per ms");
          //  Assert.IsTrue(bytesPerMs > 10*1024);       // > 10k per ms (Speed is of course very dependent on debug/release and machine)
        }

        [TestMethod]
        public void HandleCrapInFhirParser()
        {
            var errors = new ErrorList();
            FhirParser.ParseResourceFromJson("Crap!", errors);
            Assert.IsTrue(errors.Count > 0);

            errors.Clear();
            FhirParser.ParseResourceFromJson("{ \" Crap!", errors);
            Assert.IsTrue(errors.Count > 0);

            errors.Clear();
            FhirParser.ParseResourceFromXml("Crap", errors);
            Assert.IsTrue(errors.Count > 0);

            errors.Clear();
            FhirParser.ParseResourceFromXml("<Crap><cra", errors);
            Assert.IsTrue(errors.Count > 0);

        }


        [TestMethod]
        public void TestParseCrash()
        {
            var crashesXml = File.ReadAllText(@"c:\temp\crashtext.txt");

//            var crashesXml = @"<SecurityEvent xmlns=""http://hl7.org/fhir"">
//  <text>
//    <status value=""generated""/>
//    <div xmlns=""http://www.w3.org/1999/xhtml"">Disclosure by Who to Where of Whoms data What</div>
//  </text>  <event>
//    <type>
//      <coding>
//        <system value=""http://nema.org/dicom/dcid""/>
//        <code value=""110106""/>
//        <display value=""Export""/>
//      </coding>
//    </type>
//    <subtype>
//        <coding>
//            <system value=""HIPAA""/>
//            <code value=""Disclosure""/>
//            <display value=""HIPAA disclosure""/>
//        </coding>
//    </subtype>
//    <action value=""R""/>
//    <dateTime value=""2013-09-21T14:07:42Z""/>
//    <outcome value=""0""/>
//  </event>
//  <participant>
//    <!-- Source Participant, the Releasing Agent -->
//    <role>
//      <coding>
//        <system value=""http://nema.org/dicom/dcid""/>
//        <code value=""110153""/>
//        <display value=""The sender of the data""/>
//      </coding>
//    </role>
//    <userId value=""Who""/>
//    <requestor value=""true""/>
//    <network>
//      <identifier value=""127.0.0.1""/>
//      <type value=""ip""/>
//    </network>
//    </participant>
//  <participant>
//    <!-- Destination Participant, the Receiving Agent -->
//    <role>
//      <coding>
//        <system value=""http://nema.org/dicom/dcid""/>
//        <code value=""110152""/>
//        <display value=""The destination of the data""/>
//      </coding>
//    </role>
//    <userId value=""Where""/>
//    <requestor value=""false""/>
//    <network>
//      <identifier value=""127.0.0.1""/>
//      <type value=""ip""/>
//    </network>
//    </participant>
//  <source>
//    <!-- Source of Audit event -->
//    <identifier value=""Johns Accounting of Disclosures Application""/>
//  </source>
//  <object>
//    <!-- ParticipantObject - Patient -->
//    <identifier value=""Whoms""/>
//    <name value=""Whoms""/>
//    <type value=""2""/>
//    <lifecycle value=""6""/>
//  </object> 
//
//  <object>
//    <!-- ParticipantObject - what was disclosed -->
//    <lifecycle value=""11""/>
//    <type value=""4""/>
//    <identifier value=""What""/>
//    <name value=""What""/>
//  </object>
//</SecurityEvent>";

            var errors = new ErrorList();
            var result = FhirParser.ParseResourceFromXml(crashesXml,errors);


        }

        [TestMethod]
        public void TestParseBinary()
        {
            var errors = new ErrorList();
            Binary result = (Binary)FhirParser.ParseResourceFromXml(binaryXml, errors);
            Assert.AreEqual(0, errors.Count, errors.ToString());
            Assert.IsNotNull(result);

            Assert.AreEqual("image/gif", result.ContentType);
            Assert.AreEqual(Narrative.NarrativeStatus.Generated, result.Text.Status);
            Assert.AreEqual(59, result.Content[0]);
            Assert.AreEqual(993, result.Content.Length);
        }

        private const string binaryXml = @"<Binary contentType=""image/gif"" xmlns=""http://hl7.org/fhir""><text><status                       value=""generated""/><div xmlns=""http://www.w3.org/1999/xhtml"">Binary content of type image/gif</div></text>OwAAgMI0mkg0IzVTBIJCGXv02yWICAQrN+jsCxIFBAXoVtmU8gmAv8Mvl7DIIBFbqYRCsNtsiD4bBwHrYSCISC9kBYVC8XD9fDYHAdMrIXBdVo0MkQbB4IAcPCIRoQLAwGAgEA9GDgaBE2odCC8Go4HBswnQTCc3kgPDQXmADlYWCwDgcjkIcC4NBoCk0ZgkkkMhhscAUKjIcDweEIAC8SAQBiEPC4cDghEMMAcJhQDhkMC8EhMIgcFgoDAMCAMAvggAABEAEwAAAAAsAAEAAAEE+SH///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////+9//+1//+c9/+U9/9z9/+M7/9r7/9j7/9a7/857/8x7/8p7/8h7/8Y7/8Q7/8I7/8A7/9a5/9S5/8p5/8h5/8Q5/8I5/8A5/8Y3v8Q3v8I3v8A3v8A1v8Ate8AWu//AAAAAAAAAPcAEQATYTk4RklH</Binary>";

    }
}