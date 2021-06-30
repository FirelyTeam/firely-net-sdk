using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]

    public class StructureDefinitionWalkerTests
    {
        public static IAsyncResourceResolver CreateTestResolver()
        {
            return new CachedResolver(
                new SnapshotSource(
                new MultiResolver(
                    new DirectorySource(@"TestData\validation"),
                    new ZipSource("specification.zip"))));
        }

        [ClassInitialize]
        public static void SetupSource(TestContext _)
        {
            _source = CreateTestResolver();
        }

        private static IAsyncResourceResolver _source = null;

        [TestMethod]
        public async T.Task WalkIntoTypeMembers()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var nav = ElementDefinitionNavigator.ForSnapshot(sd);
            var walker = new StructureDefinitionWalker(nav, _source);

            // A primivite type
            var elem = walker.Child("status");
            Assert.AreEqual("Observation.status", elem.Current.Path);

            // A member that is of a complex type.
            elem = walker.Child("method");
            Assert.AreEqual("Observation.method", elem.Current.Path);

            // Move into the complex type
            elem = elem.Child("coding");
            Assert.AreEqual("CodeableConcept.coding", elem.Current.Path);

            // now move deeper, across type boundary
            elem = elem.Child("system");
            Assert.AreEqual("Coding.system", elem.Current.Path);
            Assert.AreEqual("uri.extension", elem.Child("extension").Current.Path);

            // Try move to the special value member
            Assert.ThrowsException<StructureDefinitionWalkerException>(() => elem.Child("value"), "Primitives should not have a 'value' member");

            // Move into a component
            elem = walker.Child("component");
            Assert.AreEqual("Observation.component", elem.Current.Path);
            Assert.IsTrue(elem.Current.Current.IsBackboneElement());

            // ...and deeper into .referenceRange
            elem = elem.Child("referenceRange");
            Assert.AreEqual("Observation.component.referenceRange", elem.Current.Path);
            elem = elem.Child("low");
            Assert.AreEqual("Observation.referenceRange.low", elem.Current.Path);

            // normal navigation into a Reference
            elem = walker.Child("performer");
            Assert.AreEqual("Observation.performer", elem.Current.Path);
            elem = elem.Child("display");
            Assert.AreEqual("Reference.display", elem.Current.Path);

            // should not walk into value[x] when unconstrained to a single type
            elem = walker.Child("value");
            Assert.ThrowsException<StructureDefinitionWalkerException>(() => elem.Child("system"));  // i.e. a Quantity

            // can't walk into an unknown child
            Assert.ThrowsException<StructureDefinitionWalkerException>(() => walker.Child("ewout"));
        }

        [TestMethod]
        public async T.Task WalkIntoChoice()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var nav = ElementDefinitionNavigator.ForSnapshot(sd);
            var walker = new StructureDefinitionWalker(nav, _source);

            // If you filter on the type of a non-choice member, you'll arrive at that type.
            var elem = walker.Child("method");
            var typed = elem.OfType("CodeableConcept");
            Assert.AreEqual(1, typed.Count());
            Assert.AreEqual("CodeableConcept", typed.Single().Current.Path);

            // Now, walk across a choice type, disambiguating the choice.
            elem = walker.Child("value").OfType("Quantity").Child("system").Single();
            Assert.AreEqual("Quantity.system", elem.Current.Path);

            // Try walking into an 'any' choice
            elem = walker.Child("method").Child("extension").Child("value").OfType("HumanName").Child("family").Single();
            Assert.AreEqual("HumanName.family", elem.Current.Path);
        }

        [TestMethod]
        public async T.Task WalkAcrossReference()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var nav = ElementDefinitionNavigator.ForSnapshot(sd);
            var walker = new StructureDefinitionWalker(nav, _source);

            var elem = walker.Child("performer").Resolve().OfType("Practitioner").Child("name").Single();
            Assert.AreEqual("Practitioner.name", elem.Current.Path);
        }

        [TestMethod]
        public async T.Task WalkAcrossInlineExtension()
        {
            var sd = await _source.FindStructureDefinitionAsync("http://unittest.com/StructureDefinition/patient-sliced-complex-extension");
            var nav = ElementDefinitionNavigator.ForSnapshot(sd);
            nav.JumpToFirst("Patient.communication");
            nav.MoveToNextSlice();

            var fortest = await nav.StructureDefinition.ToXmlAsync();
            var walker = new StructureDefinitionWalker(nav, _source);

            var elem = walker.Extension("http://hl7.org/fhir/StructureDefinition/patient-proficiency");
            elem = elem.Extension("type");

            Assert.IsNotNull(elem);
            Assert.AreEqual("Patient.communication:languageControlListening.extension:languageControlListening-proficiency.extension:type", elem.Current.Current.ElementId);
        }
    }
}


