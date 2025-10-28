using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

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
            cls.FindMappedElementByChoiceName("whoReference").Should().BeNull();
            var prop = cls.FindMappedElementByName("who");
            prop.Should().NotBeNull();

            prop.Choice.Should().Be(Introspection.ChoiceType.None);
            prop.ImplementingType.Should().Be(typeof(DataType));
            prop.PropertyTypeMapping.Name.Should().Be("Reference");
            prop.FhirType.Should().BeEquivalentTo(new[] { typeof(ResourceReference) });
        }

        [TestMethod]
        public void SDSHasCorrectInfo()
        {
            var cls = new PocoStructureDefinitionSummaryProvider().Provide(ModelInfo.CanonicalUriForFhirCoreType("Signature"));
            var elem = cls.GetElements().Single(e => e.ElementName == "who");
            cls.GetElements().Where(e => e.ElementName == "whoReference").Should().BeEmpty();
            validateEDS(elem);
        }

        private void validateEDS(IElementDefinitionSummary eds)
        {
            eds.IsChoiceElement.Should().BeFalse();
            eds.Type.Select(t => t.GetTypeName()).Should().BeEquivalentTo("Reference");
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
            var sig = new Bundle { Signature = new Signature() { Who = new ResourceReference("http://nu.nl") } };
            var json =  sig.ToTypedElement().ToJson();
            //json.Should().Contain("\"who\"");
            var sig2 = FhirJsonNode.Parse(json).ToPoco();
            sig.IsExactly(sig2).Should().BeTrue();
        }

        [TestMethod]
        public void WorksWithPocoSerializers()
        {
            var sig = new Bundle() { Type = Bundle.BundleType.Document, Signature = new Signature() { Who = new ResourceReference("http://nu.nl") } };
            var json = FhirJsonSerializer.Default.SerializeToString(sig);
            json.Should().Contain("\"who\"");
            var sig2 = new FhirJsonDeserializer().DeserializeResource(json);
            sig.IsExactly(sig2).Should().BeTrue();

            var xml = FhirXmlSerializer.Default.SerializeToString(sig);
            xml.Should().Contain("<who>");
            var sig3 = new FhirXmlDeserializer().DeserializeResource(xml);
            sig.IsExactly(sig3).Should().BeTrue();
        }
    }
}