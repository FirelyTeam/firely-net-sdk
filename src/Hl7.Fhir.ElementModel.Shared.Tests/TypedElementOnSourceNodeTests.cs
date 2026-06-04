using Hl7.Fhir.Model;
using System;
using System.Linq;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class TypedElementOnSourceNodeTests
    {
        private readonly IStructureDefinitionSummaryProvider provider = new PocoStructureDefinitionSummaryProvider();

        [TestMethod]
        public async Task TestExceptionComplexTypeValue()
        {
            await Assert.ThrowsAsync<StructuralTypeException>(async () =>
            {
                var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":\"Invalid\"}";
                var bundle = await FhirJsonNode.ParseAsync(bundleJson);
                var typedBundle = bundle.ToTypedElement(provider, "Bundle");

                var _ = typedBundle.Children("entry").First().Value;
            });
        }
        
        private SourceNode testPatient => SourceNode.Node("Patient",
            SourceNode.Resource("contained", "Observation", SourceNode.Valued("valueBoolean", "true")),
            SourceNode.Valued("active", "true",
                SourceNode.Valued("id", "myId2"),
                SourceNode.Node("extension",
                    SourceNode.Valued("url", "http://example.org/ext"),
                    SourceNode.Valued("valueString", "world!"))));
        
        private TypedElementOnSourceNode getTestPatient => (TypedElementOnSourceNode)testPatient.ToTypedElement(ModelInfo.ModelInspector, "Patient");
        
        [TestMethod]
        public void KnowsPath()
        {
            var tp = getTestPatient;
            Assert.AreEqual("Patient", getTestPatient.Location);
            Assert.AreEqual("Patient.contained[0].value[0]", getTestPatient.Children("contained").First().Children("value").First().Location);
            Assert.AreEqual("Patient.active[0]", getTestPatient.Children("active").First().Location);
            Assert.AreEqual("Patient.active[0].id[0]", getTestPatient.Children("active").First().Children("id").First().Location);
            Assert.AreEqual("Patient.active[0].extension[0].url[0]", getTestPatient.Children("active").First().Children("extension").First().Children("url").First().Location);
            Assert.AreEqual("Patient.active[0].extension[0].value[0]", getTestPatient.Children("active").First().Children("extension").First().Children("value").First().Location);
        }
    }
}
