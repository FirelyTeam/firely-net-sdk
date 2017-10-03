using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.Diagnostics;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    public class ParseDemoPatientXml
    {
        private readonly ITestOutputHelper output;

        public ParseDemoPatientXml(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CanReadThroughNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = XmlDomFhirNavigator.Create(tpXml);

            Assert.Equal("Patient", nav.Name);
            Assert.Equal("Patient", nav.Type);

            Assert.True(nav.MoveToFirstChild());
            Assert.Equal("id", nav.Name);
            Assert.Equal("pat1", nav.Value);

            Assert.False(nav.MoveToFirstChild());

            Assert.True(nav.MoveToNext());
            Assert.Equal("text", nav.Name);
            var text = nav.Clone();

            Assert.True(text.MoveToFirstChild("status")); // status
            Assert.True(text.MoveToNext());
            Assert.Equal("div", text.Name);
            Assert.StartsWith("<div xmlns=", (string)text.Value);       // special handling of xhtml
            Assert.False(text.MoveToFirstChild()); // cannot move into xhtml
            Assert.Equal("div", text.Name); // still on xhtml <div>
            Assert.False(text.MoveToNext());  // nothing more in <text>

            Assert.True(nav.MoveToNext()); // contained
            Assert.Equal("contained", nav.Name);
            Assert.Equal("Patient", nav.Type);
            Assert.True(nav.MoveToFirstChild()); // id
            Assert.True(nav.MoveToNext()); // identifier
            var identifier = nav.Clone();

            Assert.True(identifier.MoveToFirstChild()); // system
            Assert.True(identifier.MoveToNext()); // value
            Assert.False(identifier.MoveToNext()); // still value

            Assert.Equal("value", identifier.Name);
            Assert.False(identifier.MoveToFirstChild());
            Assert.Equal("444222222", identifier.Value);

            Assert.True(nav.MoveToNext("name"));
            Assert.Equal("name", nav.Name);
            Assert.True(nav.MoveToFirstChild());  // id (attribute)
            Assert.Equal("id", nav.Name);
            Assert.Equal("firstname", nav.Value);
            Assert.True(nav.MoveToNext());  // use (element!)
            Assert.Equal("use", nav.Name);
        }

        [Fact]
        public void ProducesCorrectLocations()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patient = XmlDomFhirNavigator.Create(tpXml);

            Assert.Equal("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.Equal("Patient.id[0]", patient.Location);

            patient.MoveToNext();   // text
            patient.MoveToNext();   // contained[0]
            patient.MoveToNext();   // contained[1]
            Assert.Equal("Patient.contained[1]", patient.Location);

            patient.MoveToFirstChild();
            Assert.Equal("Patient.contained[1].id[0]", patient.Location);
        }

        [Fact]
        public void ReadsAttributesAsElements()
        {
            var nav = XmlDomFhirNavigator.Create("<Patient xmlns='http://hl7.org/fhir' xmlns:q='http://somenamespace' q:myattr='dummy' />");

            Assert.True(nav.MoveToFirstChild());
            Assert.Equal("myattr", nav.Name);        // none-xmlns attributes will come through
            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.Equal("http://somenamespace", xmldetails.Namespace);

            Assert.Equal("Patient.myattr[0]", nav.Location);
        }

        [Fact]
        public void CompareXmlJsonParseOutcomes()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var navXml = XmlDomFhirNavigator.Create(tpXml);
            var navJson = JsonDomFhirNavigator.Create(tpJson);

            var compare = navXml.IsEqualTo(navJson);

            if(compare.Success == false)
            {
                output.WriteLine($"Difference in {compare.Details} at {compare.FailureLocation}");
                Assert.True(compare.Success);
            }
            Assert.True(compare.Success);
        }
    }
}