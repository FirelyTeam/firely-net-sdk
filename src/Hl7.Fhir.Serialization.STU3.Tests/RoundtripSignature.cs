using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    // Signature is special, since it's the first case where
    // an element changed from a choice element in STU3 to a non-choice element in R4.
    // These unit-tests will exercise those parts that are sensitive to such a change.
    [TestClass]
    public class RoundtripSignature
    {
        [TestMethod]
        public void ModelInfoHasCorrectInfo()
        {
            var cls = ModelInfo.ModelInspector.FindClassMapping("Signature");
            cls.FindMappedElementByName("who").Should().NotBeNull();
            var prop = cls.FindMappedElementByChoiceName("whoReference");
            prop.Should().NotBeNull();

            prop.Choice.Should().Be(Introspection.ChoiceType.DatatypeChoice);
            prop.ImplementingType.Should().Be(typeof(DataType));
            prop.PropertyTypeMapping.Name.Should().Be("DataType");
            prop.FhirType.Should().BeEquivalentTo(new Type[] { typeof(FhirUri), typeof(ResourceReference) });
        }

        [TestMethod]
        public void SDSHasCorrectInfo()
        {
            var cls = new PocoStructureDefinitionSummaryProvider().Provide("http://hl7.org/fhir/StructureDefinition/Signature");
            var elem = cls.GetElements().Single(e => e.ElementName == "who");
            cls.GetElements().Where(e => e.ElementName == "whoReference").Should().BeEmpty();
            validateEDS(elem);
        }

        private void validateEDS(IElementDefinitionSummary eds)
        {
            eds.IsChoiceElement.Should().BeTrue();
            eds.Type.Select(t => t.GetTypeName()).Should().BeEquivalentTo("uri", "Reference");
        }

        [TestMethod]
        public void TypedElementHasCorrectInfo()
        {
            var cls = new Signature() { Who = new ResourceReference("http://nu.nl") }.ToTypedElement();

            var who = cls.Children("who").Single();
            who.Name.Should().Be("who");
            who.InstanceType.Should().Be("Reference");
            validateEDS(who.Definition);
        }

        [TestMethod]
        public void WorksWithTypedElementSerializers()
        {
            var sig = new Bundle() { Signature = new Signature() { Who = new ResourceReference("http://nu.nl") } };
            var json = sig.ToTypedElement().ToJson();
            json.Should().Contain("\"whoReference\"");
            var sig2 = FhirJsonNode.Parse(json).ToPoco();
            sig.IsExactly(sig2).Should().BeTrue();
        }

        [TestMethod]
        public void WorksWithPocoSerializers()
        {
            var sig = new Bundle() { Type = Bundle.BundleType.Document, Signature = new Signature() { Who = new ResourceReference("http://nu.nl") } };
            var json = new FhirJsonPocoSerializer().SerializeToString(sig);
            json.Should().Contain("\"whoReference\"");
            var sig2 = new FhirJsonPocoDeserializer().DeserializeResource(json);
            sig.IsExactly(sig2).Should().BeTrue();

            var xml = new FhirXmlPocoSerializer().SerializeToString(sig);
            xml.Should().Contain("<whoReference>");
            var sig3 = new FhirXmlPocoDeserializer().DeserializeResource(xml);
            sig.IsExactly(sig3).Should().BeTrue();
        }
    }
}
