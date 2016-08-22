using Hl7.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    [TestClass]
    public class PrimitiveValidation
    {
        [TestInitialize]
        public void SetupSource()
        {
            source = ArtifactResolver.CreateOffline();

            var patient = source.GetStructureDefinitionForCoreType(Model.FHIRDefinedType.Patient);
            patientDefNav = ElementDefinitionNavigator.ForSnapshot(patient);
            patientDefNav.MoveToFirstChild();

            var boolean = source.GetStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            boolDefNav = ElementDefinitionNavigator.ForSnapshot(boolean);
            boolDefNav.MoveToFirstChild();

            ctx = new MyValidationContext() { ArtifactSource = source };
        }

        IArtifactSource source;
        IValidationContext ctx;

        ElementDefinitionNavigator patientDefNav;
        ElementDefinitionNavigator boolDefNav;

        [TestMethod]
        public void TestEmptyElement()
        {
            var data = ElementNode.Node("active").ToNavigator();

            var validator = new Validator(boolDefNav, data, null);
            var result = validator.Validate();
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ToString().Contains("must not be empty"));
        }


        [TestMethod]
        public void NameMatching()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral()).ToNavigator();

            Assert.IsTrue(InstanceToProfileMatcher.NameMatches("active", data));
            Assert.IsTrue(InstanceToProfileMatcher.NameMatches("activeBoolean", data));
            Assert.IsFalse(InstanceToProfileMatcher.NameMatches("activeDateTime", data));
            Assert.IsTrue(InstanceToProfileMatcher.NameMatches("active[x]", data));
            Assert.IsFalse(InstanceToProfileMatcher.NameMatches("activate", data));
        }

        [TestMethod]
        public void PrimitiveChildMatching()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                    ElementNode.Node("extension",
                        ElementNode.Valued("value", 4, "integer"))).ToNavigator();

            var matches = InstanceToProfileMatcher.Match(boolDefNav, data);
            Assert.IsFalse(matches.UnmatchedInstanceElements.Any());
            Assert.AreEqual(3, matches.Matches.Count());        // id, extension, value
            Assert.AreEqual(0, matches.Matches[0].InstanceElements.Count()); // id
            Assert.AreEqual(1, matches.Matches[1].InstanceElements.Count()); // extension
            Assert.AreEqual(1, matches.Matches[2].InstanceElements.Count()); // value

            Assert.AreEqual("extension", matches.Matches[1].InstanceElements.First().Name);
            Assert.AreEqual("extension", matches.Matches[1].Definition.PathName);
            Assert.AreEqual("active", matches.Matches[2].InstanceElements.First().Name);
            Assert.AreEqual("value", matches.Matches[2].Definition.PathName);
        }


        private class MyValidationContext : IValidationContext
        {
            public IArtifactSource ArtifactSource { get; set; }
        }


        [TestMethod]
        public void ValidatePrimitiveValue()
        {
            var data = ElementNode.Valued("active", "invalid", FHIRDefinedType.Boolean.GetLiteral()).ToNavigator();

            var validator = new Validator(boolDefNav, data, ctx);
            var report = validator.Validate();
            var x = report;
        }


        [TestMethod]
        public void ValidateCardinalityInPrimtive()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral()).ToNavigator();

            var validator = new Validator(boolDefNav, data, ctx);
            var report = validator.Validate();
        }


        [TestMethod]
        public void ValidateCardinality()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                    ElementNode.Valued("id", "myId1"),
                    ElementNode.Valued("id", "myId2"),
                    ElementNode.Node("extension",
                        ElementNode.Valued("value", 4, "integer")),
                    ElementNode.Node("extension",
                        ElementNode.Valued("value", "world!", "string"))).ToNavigator();

            var validator = new Validator(boolDefNav, data, ctx);
            var report = validator.Validate();
        }
    }

}
