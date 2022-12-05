using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]

    public class DiscriminatorInterpreterTests
    {
        public static CachedResolver CreateTestResolver()
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

        private static CachedResolver _source = null;

        [TestMethod]
        public async T.Task WalkIntoTypeMembers()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var walker = new StructureDefinitionWalker(sd, _source);

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
        public async T.Task WalkIntoChoice()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var walker = new StructureDefinitionWalker(sd, _source);

            // If you filter on the type of a non-choice member, you'll arrive at that type.
            var elem = walker.Walk("value.ofType(Quantity).system").Single();
            Assert.AreEqual("Quantity.system", elem.Current.Path);

            // Try walking into an 'any' choice
            elem = walker.Walk("method.extension.value.ofType(HumanName).family").Single();
            Assert.AreEqual("HumanName.family", elem.Current.Path);

            // use the backwards compatible function 'as()'
            elem = walker.Walk("value.as(Quantity).system").Single();
            Assert.AreEqual("Quantity.system", elem.Current.Path);
        }

        [TestMethod]
        public async T.Task WalkAcrossReference()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var walker = new StructureDefinitionWalker(sd, _source);

            var elem = walker.Walk("performer.resolve().ofType(Practitioner).name").Single();
            Assert.AreEqual("Practitioner.name", elem.Current.Path);
        }

        [TestMethod]
        public async T.Task WalkToThis()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var walker = new StructureDefinitionWalker(sd, _source);

            var elem = walker.Walk("performer").Single();
            var elem2 = elem.Walk("$this").Single();
            Assert.AreEqual(elem, elem2);
        }

        [TestMethod]
        public async T.Task WalkToExtension()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var walker = new StructureDefinitionWalker(sd, _source);

            var elem = walker.Walk("status.extension('http://hl7.org/fhir/StructureDefinition/data-absent-reason').value.ofType(code)").Single();
            Assert.AreEqual("code", elem.Current.Path);   // 'code' in STU3+
        }

        [TestMethod]
        public async T.Task WalkToExtensionSingleChoice()
        {
            var sd = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);
            var walker = new StructureDefinitionWalker(sd, _source);

            // The data-absent-reason limits its value to a type slice of one choice - we should be able to handle that,
            // although we don't handle slicing along a discriminator path in general
            var elem = walker.Walk("status.extension('http://hl7.org/fhir/StructureDefinition/data-absent-reason').value").Single();
            Assert.AreEqual("code", elem.Current.Current.Type.Single().Code);   // 'code' in STU3+
        }

        [TestMethod]
        public async T.Task WalkToInlineExtensionConstraints()
        {
            var sd = await _source.FindStructureDefinitionAsync("http://example.org/fhir/StructureDefinition/observation-profile-for-discriminator-test");
            var walker = new StructureDefinitionWalker(sd, _source);

            var elem = walker.Walk("identifier.extension('http://example.org/fhir/StructureDefinition/string-extension-for-discriminator-test').value").Single();
            var fixedString = elem.Current.Current.Fixed;

            Assert.IsTrue(fixedString is FhirString);
            Assert.AreEqual("hi!", ((FhirString)elem.Current.Current.Fixed).Value);
        }

        [TestMethod]
        public async T.Task WalkToInlineComplexExtensionConstraints()
        {
            var sd = await _source.FindStructureDefinitionAsync("http://unittest.com/StructureDefinition/patient-sliced-complex-extension");
            var nav = ElementDefinitionNavigator.ForSnapshot(sd);
            nav.JumpToFirst("Patient.communication");
            nav.MoveToNextSlice();
            var walker = new StructureDefinitionWalker(nav, _source);

            var elem = walker.Walk("extension('http://hl7.org/fhir/StructureDefinition/patient-proficiency').extension('type').value").Single();
            var pattern = elem.Current.Current.Pattern;

            Assert.IsTrue(pattern is Coding c && c.Code == "RSP");
        }


        [TestMethod]
        public async T.Task ParseInvalidDiscriminatorExpressions()
        {
            var patientDef = await _source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient);
            var schemas = new StructureDefinitionWalker(new ElementDefinitionNavigator(patientDef), _source);

            eval("45");
            eval("active.resolve('bla')");
            eval("active.extension()");
            eval("active.extension(33)");
            eval("active.extension('arg1', 'arg2')");
            eval("active.slice()");
            eval("active.ofType()");
            eval("active.OfType('something')");
            eval("active.as()");
            eval("active.As('something')");
            eval("active.where(true)");

            void eval(string expr)
            {
                Assert.ThrowsException<DiscriminatorFormatException>(() => schemas.Walk(expr));
            }
        }



    }
}


