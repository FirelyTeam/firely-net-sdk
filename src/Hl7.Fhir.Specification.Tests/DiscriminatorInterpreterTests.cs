using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]

    public class DiscriminatorInterpreterTests
    {
        public static IResourceResolver CreateTestResolver()
        {
            return new CachedResolver(
                new SnapshotSource(
                new MultiResolver(
                    new DirectorySource(@"TestData\validation"),
                    new ZipSource("specification.zip"))));
        }

        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            _source = CreateTestResolver();
        }

        private static IResourceResolver _source = null;

        [TestMethod]
        public void WalkIntoTypeMembers()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation), _source);

            // Walk to a primivite type
            var elem = walker.Walk("status").Single();
            Assert.AreEqual("Observation.status", elem.Current.Path);

            // Walk into a complex type
            elem = walker.Walk("method.coding.version").Single();
            Assert.AreEqual("Coding.version", elem.Current.Path);

            // Move into a component with name reference
            elem = walker.Walk("component.referenceRange.low").Single();
            Assert.AreEqual("Observation.referenceRange.low", elem.Current.Path);

            // should not walk into value[x] when unconstrained to a single type
            elem = walker.Child("value");
            Assert.ThrowsException<StructureDefinitionWalkerException>(() => elem.Walk("system").First());  // i.e. a Quantity

            // can't walk into an unknown child
            Assert.ThrowsException<StructureDefinitionWalkerException>(() => walker.Walk("ewout").First());
        }

        [TestMethod]
        public void WalkIntoChoice()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation), _source);

            // If you filter on the type of a non-choice member, you'll arrive at that type.
            var elem = walker.Walk("value.ofType('Quantity').system").Single();
            Assert.AreEqual("Quantity.system", elem.Current.Path);

            // Try walking into an 'any' choice
            elem = walker.Walk("method.extension.value.ofType('HumanName').family").Single();
            Assert.AreEqual("HumanName.family", elem.Current.Path);
        }

        [TestMethod]
        public void WalkAcrossReference()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation), _source);

            var elem = walker.Walk("performer.resolve().ofType('Practitioner').name").Single();
            Assert.AreEqual("Practitioner.name", elem.Current.Path);
        }

        [TestMethod]
        public void WalkToThis()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation), _source);

            var elem = walker.Walk("performer").Single();
            var elem2 = elem.Walk("$this").Single();
            Assert.AreEqual(elem, elem2);
        }

        [TestMethod]
        public void WalkToExtension()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation), _source);

            var elem = walker.Walk("status.extension('http://hl7.org/fhir/StructureDefinition/data-absent-reason').value.ofType('code')").Single();
            Assert.AreEqual("code", elem.Current.Path);   // 'code' in STU3+
        }

        [TestMethod]
        public void WalkToExtensionSingleChoice()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Observation), _source);

            // The data-absent-reason limits its value to a type slice of one choice - we should be able to handle that,
            // although we don't handle slicing along a discriminator path in general
            var elem = walker.Walk("status.extension('http://hl7.org/fhir/StructureDefinition/data-absent-reason').value").Single();
            Assert.AreEqual("code", elem.Current.Current.Type.Single().Code);   // 'code' in STU3+
        }

        [TestMethod]
        public void WalkToInlineExtensionConstraints()
        {
            var walker = new StructureDefinitionWalker(_source.FindStructureDefinition("http://example.org/fhir/StructureDefinition/observation-profile-for-discriminator-test"), _source);

            var elem = walker.Walk("identifier.extension('http://example.org/fhir/StructureDefinition/string-extension-for-discriminator-test').value").Single();
            var fixedString = elem.Current.Current.Fixed;

            Assert.IsTrue(fixedString is FhirString);
            Assert.AreEqual("hi!", ((FhirString)elem.Current.Current.Fixed).Value);
        }

        [TestMethod]
        public void ParseInvalidDiscriminatorExpressions()
        {
            var patientDef = _source.FindStructureDefinitionForCoreType(FHIRAllTypes.Patient);
            var schemas = new StructureDefinitionWalker(new ElementDefinitionNavigator(patientDef), _source);

            eval("45");
            eval("active.resolve('bla')");
            eval("active.extension()");
            eval("active.extension(33)");
            eval("active.extension('arg1', 'arg2')");
            eval("active.slice()");
            eval("active.ofType()");
            eval("active.OfType('something')");
            eval("active.where(true)");

            void eval(string expr)
            {
                Assert.ThrowsException<DiscriminatorFormatException>(() => schemas.Walk(expr));
            }
        }



    }
}


