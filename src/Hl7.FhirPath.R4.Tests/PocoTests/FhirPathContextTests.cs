using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.FhirPath.R4.Tests.PocoTests;

[TestClass]
public class FhirPathContextTests
{
    PocoNode _bundle;

    [TestInitialize]
    public void SetupSource()
    {
        var bundleXml = File.ReadAllText(Path.Combine("TestData", "bundle-contained-references.xml"));

        _bundle = (new FhirXmlDeserializer()).Deserialize<Bundle>(bundleXml).ToPocoNode();
    }
    
    [TestMethod]
    public void TestFhirEvaluationContext()
    {
        _bundle.IsTrue("entry[2].resource.contained[0].select(%resource) = %resource"); // should stay the same
        _bundle.IsTrue("%rootResource = Bundle.entry[2].resource.contained[0].select(%rootResource)"); // should stay the same

        var elemInContainedResource = _bundle.Child("entry").Skip(2).First().Child("resource").First().Child("contained").First().Child("id").First();
        elemInContainedResource.IsTrue("%rootResource != %resource"); // should be true
        elemInContainedResource.Select("%rootResource").Should().BeEquivalentTo(_bundle.Select("entry[2].resource"));

        var elemInDomainResource = _bundle.Child("entry").Skip(2).First().Child("resource").First().Child("id").First().First();
        elemInDomainResource.IsTrue("%rootResource = %resource");
        elemInDomainResource.Select("%rootResource").Should().BeEquivalentTo(_bundle.Select("entry[2].resource"));
    }
}