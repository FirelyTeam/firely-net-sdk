using Hl7.Fhir.Serialization;
using Xunit;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    public class ParseDemoPatientXml
    {
        [Fact]
        public void CanReadThroughNavigator()
        {
            var tpXml = TestData.ReadTextFile("fp-test-patient.xml");
            var nav = XmlDomFhirNavigator.Create(tpXml);

            Assert.Equal("Patient", nav.Name);
            Assert.Equal("Patient", nav.Type);

            Assert.True(nav.MoveToFirstChild());
            Assert.Equal("myattr", nav.Name);        // none-xmlns attributes will come through
            var xmldetails = nav.GetSerializationDetails<XmlSerializationDetails>();
            Assert.Equal("http://somenamespace", xmldetails.Namespace);

            Assert.True(nav.MoveToNext());
            Assert.Equal("id", nav.Name);
            Assert.Null(nav.Type);
            var id = nav.Clone();

            Assert.True(nav.MoveToFirstChild());
            Assert.Equal("value", nav.Name);
            Assert.Equal("pat1", nav.Value);
            Assert.False(nav.MoveToFirstChild());
            Assert.False(nav.MoveToNext());

            Assert.True(id.MoveToNext());
            Assert.Equal("text", id.Name);
            var text = id.Clone();

            Assert.True(id.MoveToFirstChild()); // status
            Assert.True(id.MoveToNext());
            Assert.Equal("div", id.Name);
            Assert.StartsWith("<div xmlns=", (string)id.Value);       // special handling of xhtml
            Assert.False(id.MoveToFirstChild()); // cannot move into xhtml
            Assert.Equal("div", id.Name); // still on xhtml <div>
            Assert.False(id.MoveToNext());  // nothing more in <text>

            Assert.True(text.MoveToNext()); // contained
            Assert.Equal("contained", text.Name);
            Assert.Equal("Patient", text.Type);
            Assert.True(text.MoveToFirstChild()); // id
            Assert.True(text.MoveToNext()); // identifier
            var identifier = text.Clone();

            Assert.True(text.MoveToFirstChild()); // system
            Assert.True(text.MoveToNext()); // value
            Assert.False(text.MoveToNext()); // still value

            Assert.Equal("value", text.Name);
            Assert.True(text.MoveToFirstChild());
            Assert.Equal("value", text.Name); // value.value
            Assert.Equal("444222222", text.Value);
            Assert.False(text.MoveToNext());

            Assert.True(identifier.MoveToNext()); // active
            Assert.True(identifier.MoveToNext()); // name
            Assert.Equal("name", identifier.Name);
            Assert.True(identifier.MoveToFirstChild());  // id (attribute)
            Assert.Equal("id", identifier.Name);
            Assert.True(identifier.MoveToNext());  // use (element!)
            Assert.Equal("use", identifier.Name);
        }

        [Fact]
        public void ProducesCorrectLocations()
        {
            var tpXml = TestData.ReadTextFile("fp-test-patient.xml");
            var patient = XmlDomFhirNavigator.Create(tpXml);

            Assert.Equal("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.Equal("Patient.myattr[0]", patient.Location);

            patient.MoveToNext();
            Assert.Equal("Patient.id[0]", patient.Location);

            patient.MoveToNext();   // text
            patient.MoveToNext();   // contained[0]
            patient.MoveToNext();   // contained[1]
            Assert.Equal("Patient.contained[1]", patient.Location);

            patient.MoveToFirstChild();
            Assert.Equal("Patient.contained[1].id[0]", patient.Location);
        }
    }
}
