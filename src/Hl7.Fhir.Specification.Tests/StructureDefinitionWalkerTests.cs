using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Specification.Navigation.FhirPath;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]

    public class StructureDefinitionWalkerTests
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
            var nav = ElementDefinitionNavigator.ForSnapshot(_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Observation));
            var walker = new StructureDefinitionSchemaWalker(nav, _source);

            // reference: subject
            // choice: value[x]

            // A member that is of a complex type.
            var elem = walker.Child("method");
            Assert.AreEqual("Observation.method", elem.Current.Path);

            // Move into the complex type
            elem = elem.Child("coding");
            Assert.AreEqual("CodeableConcept.coding", elem.Current.Path);

            // A primivite type
            elem = walker.Child("status");
            Assert.AreEqual("Observation.status", elem.Current.Path);

            // Try move to the special value member
            Assert.ThrowsException<StructureDefinitionSchemaWalkerException>(() => elem.Child("value"), "Primitives should not have a 'value' member");
            // STU3: Assert.AreEqual("code.extension", elem.Child("extension").Current.Path);
            Assert.AreEqual("string.extension", elem.Child("extension").Current.Path);

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

       
        }

        [TestMethod]
        public void WalkIntoConstrainedMembers()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void FilterByCanonical()
        {
            var nav = ElementDefinitionNavigator.ForSnapshot(_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Observation));
            var walker = new StructureDefinitionSchemaWalker(nav, _source);

            // reference: subject
            // choice: value[x]

            // A member that is of a complex type.
            var elem = walker.Child("method");
            Assert.AreEqual(1, elem.OfType(ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.CodeableConcept)).Count());

            //// A primivite type
            //elem = walker.Child("status");
            //Assert.AreEqual("Observation.status", elem.Current.Path);

            //// Try move to the special value member
            //Assert.ThrowsException<StructureDefinitionSchemaWalkerException>(() => elem.Child("value"), "Primitives should not have a 'value' member");
            //// STU3: Assert.AreEqual("code.extension", elem.Child("extension").Current.Path);
            //Assert.AreEqual("string.extension", elem.Child("extension").Current.Path);

            //// Move into a component
            //elem = walker.Child("component");
            //Assert.AreEqual("Observation.component", elem.Current.Path);
            //Assert.IsTrue(elem.Current.Current.IsBackboneElement());

            //// ...and deeper into .referenceRange
            //elem = elem.Child("referenceRange");
            //Assert.AreEqual("Observation.component.referenceRange", elem.Current.Path);
            //elem = elem.Child("low");
            //Assert.AreEqual("Observation.referenceRange.low", elem.Current.Path);

            //// normal navigation into a Reference
            //elem = walker.Child("performer");
            //Assert.AreEqual("Observation.performer", elem.Current.Path);
            //elem = elem.Child("display");
            //Assert.AreEqual("Reference.display", elem.Current.Path);
        }

        [TestMethod]
        public void WalkAcrossReference()
        {
            throw new NotImplementedException();

            // also test .resolve().ofType()
        }

        [TestMethod]
        public void WalkToExtensionDefinition()
        {
            throw new NotImplementedException();
        }
    }
}


   