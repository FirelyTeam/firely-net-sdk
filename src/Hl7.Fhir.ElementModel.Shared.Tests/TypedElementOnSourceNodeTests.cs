using System;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class TypedElementOnSourceNodeTests
    {
        private readonly IStructureDefinitionSummaryProvider provider = new PocoStructureDefinitionSummaryProvider();

        [ExpectedException(typeof(StructuralTypeException), "Should have thrown on .Value as complex types can't have a value")]
        [TestMethod]
        public async Task TestExceptionComplexTypeValue()
        {
            var bundleJson = "{\"resourceType\":\"Bundle\", \"entry\":\"Invalid\"}";
            var bundle = await FhirJsonNode.ParseAsync(bundleJson);
            var typedBundle = bundle.ToTypedElement(provider, "Bundle");

            var _ = typedBundle.Children("entry").First().Value;
        }
    }
}
