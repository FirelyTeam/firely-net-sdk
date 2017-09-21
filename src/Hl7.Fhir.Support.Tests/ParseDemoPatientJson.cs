using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Xunit;
using Hl7.Fhir.Utility;
using Xunit.Abstractions;
using System.IO;

namespace Hl7.FhirPath.Tests.JsonNavTests
{
    public class ParseDemoPatientJson
    {
        private readonly ITestOutputHelper output;

        public ParseDemoPatientJson(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CanReadThroughNavigator()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");

            var patient = JsonDomFhirNavigator.Create(tpJson);

            Assert.Equal("Patient", patient.Name);
            Assert.Equal("Patient", patient.Type);
            Assert.False(patient.MoveToNext());

            // Move into child Patient.identifier
            Assert.True(patient.MoveToFirstChild());
            var identifier = patient.Clone();
            Assert.Null(identifier.Type);
            Assert.Null(identifier.Value);

            // Move to child Patient.identifier.period
            Assert.True(identifier.MoveToFirstChild());
            var period = identifier.Clone();

            // Move to child Patient.identifier.period.start
            Assert.True(period.MoveToFirstChild("start"));
            Assert.Equal("start", period.Name);
            Assert.Equal("2001-05-06", period.Value);

            Assert.True(identifier.MoveToNext());    // assigner
            Assert.True(identifier.MoveToNext());    // use
            Assert.Equal("usual", identifier.Value);
            Assert.True(identifier.MoveToNext());    // system
            Assert.True(identifier.MoveToNext());    // value

            // Check the value + extensions on Patient.identifier.value
            Assert.Equal("12345", identifier.Value);
            var value = identifier.Clone();
            Assert.True(value.MoveToFirstChild());
            Assert.Equal("fhir_comments", value.Name);
            Assert.Equal("     seems like a likely choice     ", value.Value);
            Assert.False(value.MoveToNext());

            Assert.False(identifier.MoveToNext());

            // Move to sibling Patient.managingOrganization
            Assert.True(patient.MoveToNext());
            Assert.Equal("managingOrganization", patient.Name);

            // Move to sibling Patient.active
            Assert.True(patient.MoveToNext());
            Assert.Equal("active", patient.Name);
            Assert.Null(patient.Value);

            // Move to sibling Patient.name (2x)
            Assert.True(patient.MoveToNext());
            Assert.Equal("name", patient.Name);
            Assert.True(patient.MoveToNext());
            Assert.Equal("name", patient.Name);

            Assert.True(patient.MoveToNext("deceasedBoolean"));
            Assert.Equal("deceasedBoolean", patient.Name);
            Assert.Equal("true", patient.Value);

            var details = (patient as IAnnotated).Annotation<JsonSerializationDetails>();
            Assert.Equal(true, details.RawValue);

            Assert.True(patient.MoveToNext()); // address
            Assert.True(patient.MoveToNext()); // maritalStatus
            Assert.True(patient.MoveToNext()); // multipleBirthInteger
            Assert.Equal("3", patient.Value);

            Assert.True(patient.MoveToNext()); // text
            Assert.True(patient.MoveToNext()); // contained

            // Check Patient.contained[0], a Binary
            Assert.Equal("contained", patient.Name);
            Assert.Equal("Binary", patient.Type);

            Assert.True(patient.MoveToNext()); // contained

            // Check Patient.contained[1], an Organization
            Assert.Equal("contained", patient.Name);
            Assert.Equal("Organization", patient.Type);

            Assert.True(patient.MoveToNext()); // contact
            validateContact(patient);

            Assert.True(patient.MoveToNext()); // careProvider
            Assert.True(patient.MoveToNext()); // telecom (2x)
            Assert.True(patient.MoveToNext());
            Assert.False(patient.MoveToNext());
        }

        private void validateContact(IElementNavigator patient)
        {
            var contact = patient.Clone();

            Assert.True(contact.MoveToFirstChild()); // contact.name

            Assert.True(contact.MoveToFirstChild()); // contact.name.family[0]            
            Assert.Null(contact.Value);

            Assert.True(contact.MoveToNext()); // family[1]
            Assert.Equal("du", contact.Value);

            Assert.True(contact.MoveToNext()); // family[2]
            Assert.True(contact.MoveToNext()); // family[3]

            Assert.Equal("Marché", contact.Value);
            Assert.False(contact.MoveToFirstChild());

            Assert.True(contact.MoveToNext()); // family[4]
            Assert.Null(contact.Value);
            Assert.True(contact.MoveToFirstChild()); // family[4].extension
            Assert.Equal("extension", contact.Name);
        }

        [Fact]
        public void CatchesArrayMisMatch()
        {
            var nav = JsonDomFhirNavigator.Create("{ 'a': [2,3,4], '_a' : [{},null] }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': 2, '_a' : [{},null] }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': [2,3,4], '_a' : {} }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': [2,3,4] }", "test");
            Assert.True(nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ '_a': [{},{},{}] }", "test");
            Assert.True(nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': [null,2], '_a' : [{},null] }", "test");
            Assert.True(nav.MoveToFirstChild());
        }

        [Fact]
        public void CatchesUnsupportedFeatures()
        {
            var nav = JsonDomFhirNavigator.Create("{ 'a': {}, '_a' : {} }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': 3, '_a' : 4 }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': 3, '_a' : new DateTime() }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());

            nav = JsonDomFhirNavigator.Create("{ 'a': new DateTime() }", "test");
            Assert.Throws<FormatException>(() => nav.MoveToFirstChild());
        }


        [Fact]
        public void ProducesCorrectLocations()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");

            var patient = JsonDomFhirNavigator.Create(tpJson);

            Assert.Equal("Patient", patient.Location);

            patient.MoveToFirstChild();
            var cont = patient.Clone();

            Assert.Equal("Patient.identifier[0]", patient.Location);
            patient.MoveToFirstChild();
            Assert.Equal("Patient.identifier[0].period[0]", patient.Location);

            cont.MoveToNext(); // managingOrganization
            cont.MoveToNext();
            Assert.Equal("Patient.active[0]", cont.Location);

            cont.MoveToNext();
            Assert.Equal("Patient.name[0]", cont.Location);

            cont.MoveToNext();
            Assert.Equal("Patient.name[1]", cont.Location);

            cont.MoveToFirstChild();
            Assert.Equal("Patient.name[1].given[0]", cont.Location);
        }

        [Fact]
        public void CompareJsonXmlParseOutcomes()
        {
            var tpJson = File.ReadAllText(@"TestData\json-edge-cases.json");
            var tpXml = File.ReadAllText(@"TestData\json-edge-cases.xml");
            
            var navJson = JsonDomFhirNavigator.Create(tpJson);
            var navXml = XmlDomFhirNavigator.Create(tpXml);

            var compare = navJson.IsEqualTo(navXml);

            if (compare.Success == false)
            {
                output.WriteLine($"Difference in {compare.Details} at {compare.FailureLocation}");
                Assert.True(compare.Success);
            }
            Assert.True(compare.Success);
        }

        [Fact]
        public void FindFirstChild()
        {
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");

            var patient = JsonDomFhirNavigator.Create(tpJson);

            patient.MoveToFirstChild("gender");
            Assert.Equal("male", patient.Value.ToString());
            Assert.Equal("Patient.gender[0]", patient.Location);
        }
    }
}