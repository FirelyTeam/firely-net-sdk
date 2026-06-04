using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializePartialTree
    {
        public ITypedElement getXmlNode(string xml, FhirXmlParsingSettings s = null) =>
            XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(), s);
        public async Tasks.Task<ITypedElement> getJsonNode(string json, FhirJsonParsingSettings s = null) =>
            await JsonParsingHelpers.ParseToTypedElementAsync(json, new PocoStructureDefinitionSummaryProvider(), settings: s);

        [TestMethod]
        public void DeterminePocoInstanceTypeWithRedirect()
        {
            var pat = new Patient();
            pat.Text = new Narrative();
            pat.Text.Div = "whatever";

            var patNav = pat.ToTypedElement();
            Assert.AreEqual("xhtml", patNav.Children("text").Children("div").Single().InstanceType);
        }

        [TestMethod]
        public async Tasks.Task CanSerializeSubtree()
        {
            var tpXml = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.xml"));
            var tpJson = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));

            var pat = FhirXmlDeserializer.OSTRICH.Deserialize<Patient>(tpXml);

            // Should work on the parent resource
            var navXml = getXmlNode(tpXml);
            var navJson = await getJsonNode(tpJson);
            var navPoco = pat.ToTypedElement();
            testSubtree(navXml, navJson, navPoco);

            // An on a child that's a normal datatype
            var subnavXml = navXml.Children("photo").First();
            var subnavJson = navJson.Children("photo").First();
            var subnavPoco = navPoco.Children("photo").First();
            testSubtree(subnavXml, subnavJson, subnavPoco);

            // And on a contained resource
            subnavXml = navXml.Children("contained").First();
            subnavJson = navJson.Children("contained").First();
            subnavPoco = navPoco.Children("contained").First();
            testSubtree(subnavXml, subnavJson, subnavPoco);

            // And on a child of the contained resource
            subnavXml = navXml.Children("contained").First().Children("name").First();
            subnavJson = navJson.Children("contained").First().Children("name").First();
            subnavPoco = navPoco.Children("contained").First().Children("name").First();
            testSubtree(subnavXml, subnavJson, subnavPoco);
        }

        private void testSubtree(ITypedElement navXml, ITypedElement navJson, ITypedElement navPoco)
        {
            assertAreNavsEqual(navXml, navJson, navPoco);

            var navRtXml = JsonParsingHelpers.ParseToTypedElement(navXml.ToJson(), navXml.InstanceType,
                new PocoStructureDefinitionSummaryProvider(), navXml.Name);
            var navRtJson = navJson.ToPoco().ToTypedElement(navJson.Name);
            var navRtPoco = XmlParsingHelpers.ParseToTypedElement(navPoco.ToXml(), navPoco.InstanceType,
                new PocoStructureDefinitionSummaryProvider());
            assertAreNavsEqual(navRtXml, navRtJson, navRtPoco);
        }

        private void assertAreNavsEqual(ITypedElement subnavXml, ITypedElement subnavJson, ITypedElement subnavPoco)
        {
            var result = subnavXml.IsEqualTo(subnavJson);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
            result = subnavJson.IsEqualTo(subnavPoco);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
            result = subnavPoco.IsEqualTo(subnavXml);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
        }
    }

    internal static class JsonParsingHelpers
    {
        internal static ITypedElement ParseToTypedElement(string json, IStructureDefinitionSummaryProvider provider, string rootName = null,
            FhirJsonParsingSettings settings = null, TypedElementSettings tnSettings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return FhirJsonNode.Parse(json, rootName, settings).ToTypedElement(provider, null, tnSettings);
        }
        
        internal static async Tasks.Task<ITypedElement> ParseToTypedElementAsync(string json, IStructureDefinitionSummaryProvider provider, string rootName = null,
            FhirJsonParsingSettings settings = null, TypedElementSettings tnSettings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            json = json.Replace("\r","");
            return (await FhirJsonNode.ParseAsync(json, rootName, settings)).ToTypedElement(provider, null, tnSettings);
        }

        internal static ITypedElement ParseToTypedElement(string json, string type, IStructureDefinitionSummaryProvider provider, string rootName = null,
            FhirJsonParsingSettings settings = null, TypedElementSettings tnSettings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            json = json.Replace("\r","");
            return (FhirJsonNode.Parse(json, rootName, settings)).ToTypedElement(provider, type, tnSettings);
        }
    }

    internal static class XmlParsingHelpers
    {
        public static ITypedElement ParseToTypedElement(string xml, IStructureDefinitionSummaryProvider provider, FhirXmlParsingSettings settings = null, TypedElementSettings tnSettings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            xml = xml.Replace("\r","");

            return FhirXmlNode.Parse(xml, settings).ToTypedElement(provider, null, tnSettings);
        }

        public static ITypedElement ParseToTypedElement(string xml, string type, IStructureDefinitionSummaryProvider provider,
            FhirXmlParsingSettings settings = null, TypedElementSettings tnSettings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            xml = xml.Replace("\r","");
            return FhirXmlNode.Parse(xml, settings).ToTypedElement(provider, type, tnSettings);
        }

    }

}