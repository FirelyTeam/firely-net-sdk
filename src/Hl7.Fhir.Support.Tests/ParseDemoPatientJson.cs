using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Xunit;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    public class ParseDemoPatientJson
    {
        [Fact]
        public void CanReadThroughNavigator()
        {
            var tpJson = TestData.ReadTextFile("json-edge-cases.json");

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
            Assert.True(period.MoveToFirstChild());
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

            Assert.True(patient.MoveToNext()); // extension (2x)
            Assert.True(patient.MoveToNext());
            Assert.True(patient.MoveToNext()); // modifierExtension (2x)
            Assert.True(patient.MoveToNext());
            Assert.True(patient.MoveToNext()); // gender
            Assert.True(patient.MoveToNext()); // birthdate
            Assert.True(patient.MoveToNext()); // deceasedBoolean
            Assert.Equal("deceasedBoolean", patient.Name);
            Assert.Equal("true", patient.Value);

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
    }
}
