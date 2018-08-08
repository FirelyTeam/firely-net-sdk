using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializePartialTree
    {
        public IElementNavigator getXmlNav(string xml, FhirXmlNavigatorSettings s = null) =>
            FhirXmlNavigator.ForResource(xml, new PocoStructureDefinitionSummaryProvider(), s);
        public IElementNavigator getJsonNav(string json, FhirJsonNavigatorSettings s = null) =>
            FhirJsonNavigator.ForResource(json, new PocoStructureDefinitionSummaryProvider(), settings: s);
        

        [TestMethod]
        public void CanSerializeSubtree()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var pat = (new FhirXmlParser()).Parse<Patient>(tpXml);

            // Should work on the parent resource
            var navXml = getXmlNav(tpXml);
            var navJson = getJsonNav(tpJson);
            var navPoco = pat.ToElementNavigator();
            testSubtree(navXml, navJson, navPoco);

            // An on a child that's a normal datatype
            var subnavXml = navXml.Children("photo").First();
            var subnavJson = navJson.Children("photo").First();
            var subnavPoco = navPoco.Children("photo").First();
            testSubtree(subnavXml, subnavJson, subnavPoco);

            // And on a child that's a primitive datatype
            //subnavXml = navXml.Children("id").First();
            //subnavJson = navJson.Children("id").First();
            //subnavPoco = navPoco.Children("id").First();
            //testSubtree(subnavXml, subnavJson, subnavPoco);

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

        private void testSubtree(IElementNavigator navXml, IElementNavigator navJson, IElementNavigator navPoco)
        {
            assertAreNavsEqual(navXml, navJson, navPoco);

            var navRtXml = FhirJsonNavigator.ForElement(navXml.ToJson(), navXml.Type,
                new PocoStructureDefinitionSummaryProvider(), navXml.Name);
            var navRtJson = navJson.ToPoco(ModelInfo.GetTypeForFhirType(navJson.Type))
                .ToElementNavigator(navJson.Name);
            var navRtPoco = FhirXmlNavigator.ForElement(navPoco.ToXml(), navPoco.Type,
                new PocoStructureDefinitionSummaryProvider());
            assertAreNavsEqual(navRtXml, navRtJson, navRtPoco);
        }

        private void assertAreNavsEqual(IElementNavigator subnavXml, IElementNavigator subnavJson, IElementNavigator subnavPoco)
        {
            var result = subnavXml.IsEqualTo(subnavJson);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
            Assert.IsTrue(subnavJson.IsEqualTo(subnavPoco).Success);
            Assert.IsTrue(subnavPoco.IsEqualTo(subnavXml).Success);
        }
    }
}