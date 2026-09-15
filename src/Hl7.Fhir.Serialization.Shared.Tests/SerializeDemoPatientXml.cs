using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests;

[TestClass]
public class SerializeDemoPatientXml
{
    public ITypedElement getXmlElement(string xml, FhirXmlParsingSettings s = null) =>
        XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(), s);
    public async Tasks.Task<ITypedElement> getJsonElement(string json, FhirJsonParsingSettings s = null) =>
        await JsonParsingHelpers.ParseToTypedElementAsync(json, new PocoStructureDefinitionSummaryProvider(), settings: s);


    [TestMethod]
    public void CanSerializeThroughNavigatorAndCompare()
    {
        var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
        var nav = getXmlElement(tpXml);
        var output = nav.ToXml();
        XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
    }

    [TestMethod]
    public void TestPruneEmptyNodes()
    {
        var tpXml = File.ReadAllText(Path.Combine("TestData", "test-empty-nodes.xml"));

        // Make sure permissive parsing is on - otherwise the parser will complain about all those empty nodes
        var nav = getXmlElement(tpXml, new FhirXmlParsingSettings { PermissiveParsing = true });
        var doc = nav.ToXDocument().Root!;
        Assert.AreEqual(10, doc.DescendantNodesAndSelf().Count());  // only 8 nodes + 2 comments left after pruning
    }

    [TestMethod]
    public void TestElementReordering()
    {
        var tpXml = File.ReadAllText(Path.Combine("TestData", "patient-out-of-order.xml"));
        var nav = getXmlElement(tpXml, new FhirXmlParsingSettings { PermissiveParsing = true });  // since the order is incorrect
        var root = nav.ToXDocument().Root!;

        var orderedNames = root.Elements().Select(e => e.Name.LocalName).ToList();
        CollectionAssert.AreEqual(new[] { "id", "text", "identifier", "identifier", "active", "name", "telecom" }, orderedNames);

        var orderedNameNames = root.Element("{http://hl7.org/fhir}name")
            .Elements().Select(e => e.Name.LocalName).ToList();
        CollectionAssert.AreEqual(new[] { "use", "family", "given" }, orderedNameNames);
    }

    [TestMethod]
    public void CanSerializeFromPoco()
    {
        var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
        var pat = FhirXmlDeserializer.OSTRICH.Deserialize<Patient>(tpXml);

        var nav = pat.ToTypedElement();
        var output = nav.ToXml();
        XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
    }

    [TestMethod]
    public async Tasks.Task CompareSubtrees()
    {
        var tpXml = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.xml"));
        var tpJson = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
        // If on a Unix platform replace \\r\\n in json strings to \\n.
        if(Environment.NewLine == "\n")
            tpJson = tpJson.Replace(@"\r\n", @"\n");
        var pat = FhirXmlDeserializer.RECOVERABLE.Deserialize<Patient>(tpXml);

        var navXml = getXmlElement(tpXml);
        var navJson = await getJsonElement(tpJson);
        var navPoco = pat.ToTypedElement();
        assertAreAllEqual(navXml, navJson, navPoco);

        // A subtree that's a normal datatype
        var subnavXml = navXml.Children("photo").First();
        var subnavJson = navJson.Children("photo").First();
        var subnavPoco = navPoco.Children("photo").First();
        assertAreAllEqual(subnavXml, subnavJson, subnavPoco);
    }

    private void assertAreAllEqual(ITypedElement subnavXml, ITypedElement subnavJson, ITypedElement subnavPoco)
    {
        Assert.IsTrue(subnavXml.IsEqualTo(subnavJson).Success);
        Assert.IsTrue(subnavJson.IsEqualTo(subnavPoco).Success);
        Assert.IsTrue(subnavPoco.IsEqualTo(subnavXml).Success);
    }

    [TestMethod]
    public async Tasks.Task DoesPretty()
    {
        var xml = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.xml"));

        var nav = getXmlElement(xml);
        var output = nav.ToXml();
        Assert.DoesNotContain('\n', output[..50]);
        var pretty = nav.ToXml(pretty: true);
        Assert.Contains('\n', pretty[..50]);

        var p = FhirXmlDeserializer.OSTRICH.Deserialize<Patient>(xml);
        output = new FhirXmlSerializer().SerializeToString(p, pretty: false);
        Assert.DoesNotContain('\n', output[..50]);
        pretty = new FhirXmlSerializer().SerializeToString(p, pretty: true);
        Assert.Contains('\n', pretty[..50]);
    }
}