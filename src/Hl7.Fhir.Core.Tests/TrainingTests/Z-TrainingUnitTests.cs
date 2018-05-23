using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;

using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using System.Diagnostics;
using System.Xml;

namespace FHIR.Server.Tests
{
    [TestClass]
    public class Training_UnitTests
    {
        [TestMethod, TestCategory("Training")]
        public void CreateAPatient()
        {
            var pat = new Patient();
            pat.Name = new System.Collections.Generic.List<HumanName>();
            pat.Name.Add(new HumanName().WithGiven("Brian").AndFamily("Postlethwaite"));
            pat.Name[0].Use = HumanName.NameUse.Usual;
            pat.Name[0].Prefix = new string[] { "Mr" };
            DateTime nativeBirthTime = new DateTime(2016, 6, 16, 15, 23, 0);
            pat.BirthDate = nativeBirthTime.ToFhirDate();

            // Note that there is a pat.BirthDateElement
            // This is so that extensions on primitive values.
            // e.g. Use the Birth Time Extension
            pat.BirthDateElement.AddExtension(
                "http://hl7.org/fhir/StructureDefinition/patient-birthTime",
                new FhirDateTime(nativeBirthTime));

            // Use a CodeableConcept
            pat.MaritalStatus = new CodeableConcept(
                "http://hl7.org/fhir/ValueSet/marital-status", "M", "Married", "Married");
            pat.MaritalStatus.Coding.Add(new Coding("https://example.org/govt-status-codes", "2", "Legally Married"));

            // Add an address to the patient
            pat.Address.Add(new Address()
            {
                Line = new string[] { "5 Smart Street" },
                City = "Melbourne",
                State = "Victoria",
                Country = "Australia",
                PostalCode = "3000"
            });
            pat.Address.Add(new Address()
            {
                Line = new string[] { "21 Main Street" },
                City = "Moonee Ponds",
                State = "Victoria",
                Country = "Australia",
                PostalCode = "3039"
            });

            // Telecom
            pat.Telecom.Add(new ContactPoint(ContactPoint.ContactPointSystem.Email,
                ContactPoint.ContactPointUse.Home, "555 123456"));
            pat.Telecom[0].Period = new Period() { Start = "2015", End = "2017" };
        }

        // See https://groups.google.com/forum/?utm_medium=email&utm_source=footer#!msg/fhir-dotnet/2L0_3ONfIbk/Er8Ho0yeCAAJ
        [TestMethod, TestCategory("Training")]
        public void AddingAnExtensionToPatientResource()
        {
            var pat = new Patient();
            var coding = new Coding("http://meteor.aihw.gov.au/content/index.phtml/itemId/602543#Codes", "1");
            coding.Display = "Aboriginal but not Torres Strait Islander origin";
            pat.AddExtension("http://hl7.org.au/fhir/StructureDefinition/indigenous-status", coding);

            // Read the value of the extention from the Patient resource
            Coding result = pat.GetExtension("http://hl7.org.au/fhir/StructureDefinition/indigenous-status").Value as Coding;
        }

        [TestMethod, TestCategory("Training")]
        public void AddingModifierExtensionToPatientResource()
        {
            var pat = new Patient();
            var isRobot = new FhirBoolean(true);
            pat.ModifierExtension.Add(new Extension("http://example.org/robot", isRobot));
        }

        [TestMethod, TestCategory("Training")]
        public void UsingResourceIdentity()
        {
            ResourceIdentity ri = ResourceIdentity.Build(
                    new Uri("http://sqlonfhir.azurewebsites.net/fhir"),
                    "Patient", "45", "1");
#if NET_CONSOLE
            Trace.WriteLine(ri.WithoutVersion().OriginalString);
#endif
        }

        [TestMethod, TestCategory("Training")]
        public void ContainedResources()
        {
            var pat = new Patient();
            pat.Name.Add(new HumanName().WithGiven("Brian").AndFamily("Postlethwaite"));

            var org = new Organization();
            org.Id = Guid.NewGuid().ToFhirId(); // .ToString("n");
            org.Name = "HL7 International";

            pat.Contained.Add(org);
            pat.ManagingOrganization = new ResourceReference()
            {
                Reference = "#" + org.Id,
                Display = org.Name
            };
        }

        public void ParseElementDefinitionConstraint()
        {
            string constraint = "";
            /*
                  <element>
                        <constraint>
                          <key value="vsd-7"/>
                          <severity value="error"/>
                          <human value="A defined code system (if present) SHALL have a different url than the value set url"/>
                          <expression value="codeSystem.empty() or (codeSystem.system != url)"/>
                          <xpath value="not(f:codeSystem/f:system/@value = f:url/@value)"/>
                        </constraint>
                  </element>
             */
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(constraint);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("fhir", "http://hl7.org/fhir");
            foreach (XmlElement node in doc.SelectNodes("fhir:constraint"))
            {
                string expression = node.SelectSingleNode("fhir:expression/@value").Value;
                string key = node.SelectSingleNode("fhir:key/@value").Value;
                string severity = node.SelectSingleNode("fhir:severity/@value").Value;
                string human = node.SelectSingleNode("fhir:human/@value").Value;
                string xpath = node.SelectSingleNode("fhir:xpath/@value").Value;
                var newConst = new ElementDefinition.ConstraintComponent()
                {
                    Expression = expression,
                    Key = key,
                    Severity = severity == "Error" ? ElementDefinition.ConstraintSeverity.Error : ElementDefinition.ConstraintSeverity.Warning,
                    Human = human,
                    Xpath = xpath
                };
            }
        }

        [TestMethod, TestCategory("Training")]
        public void TestParameters()
        {
            Parameters p = new Parameters();
            p.Add("match.concept", new Coding("http://example.org/coding", "1", "some test to display"));
            Coding result = p.GetSingleValue<Coding>("match.concept");
        }
    }
}
