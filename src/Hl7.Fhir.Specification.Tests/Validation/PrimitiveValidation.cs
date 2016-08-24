using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
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
            source = new CachedArtifactSource(new FileDirectoryArtifactSource("TestData/validation", includeSubdirectories: true));

            var patient = source.GetStructureDefinitionForCoreType(Model.FHIRDefinedType.Patient);
            patientDefNav = ElementDefinitionNavigator.ForSnapshot(patient);
            patientDefNav.MoveToFirstChild();

            var extension = source.GetStructureDefinitionForCoreType(FHIRDefinedType.Extension);
            extensionDefNav = ElementDefinitionNavigator.ForSnapshot(extension);
            extensionDefNav.MoveToFirstChild();

            var boolean = source.GetStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            boolDefNav = ElementDefinitionNavigator.ForSnapshot(boolean);
            boolDefNav.MoveToFirstChild();

            ctx = new ValidationContext() { ArtifactSource = source };
        }

        IArtifactSource source;
        ValidationContext ctx;

        ElementDefinitionNavigator patientDefNav;
        ElementDefinitionNavigator boolDefNav;
        ElementDefinitionNavigator extensionDefNav;

        [TestMethod]
        public void TestEmptyElement()
        {
            var data = ElementNode.Node("active").ToNavigator();

            var validator = new Validator(null);
            var result = validator.ValidateElement(boolDefNav, data);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ToString().Contains("must not be empty"));
        }


        [TestMethod]
        public void NameMatching()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral()).ToNavigator();

            Assert.IsTrue(ChildNameMatcher.NameMatches("active", data));
            Assert.IsTrue(ChildNameMatcher.NameMatches("activeBoolean", data));
            Assert.IsFalse(ChildNameMatcher.NameMatches("activeDateTime", data));
            Assert.IsTrue(ChildNameMatcher.NameMatches("active[x]", data));
            Assert.IsFalse(ChildNameMatcher.NameMatches("activate", data));
        }

        [TestMethod]
        public void PrimitiveChildMatching()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                    ElementNode.Node("extension",
                        ElementNode.Valued("value", 4, "integer"))).ToNavigator();

            var matches = ChildNameMatcher.Match(boolDefNav, data);
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


        [TestMethod]
        public void ValidatePrimitiveValue()
        {
            var def = source.GetStructureDefinitionForCoreType(FHIRDefinedType.Oid);
            var validator = new Validator(ctx);

            var instance = new Oid("1.2.3.4.q");
            var report = validator.Validate(def, instance);

            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            instance = new Oid("1.2.3.4");
            report = validator.Validate(def, instance);

            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Errors);
            Assert.AreEqual(0, report.Warnings);
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

            var validator = new Validator(ctx);
            var report = validator.ValidateElement(boolDefNav,data);

            Assert.AreEqual(3, report.ListErrors().Count());
        }

        [TestMethod]
        public void ValidateChoiceElement()
        {
            var extensionSD = (StructureDefinition)source.GetStructureDefinitionForCoreType(FHIRDefinedType.Extension).DeepCopy();

            var extensionInstance = new Extension("http://some.org/testExtension", new Oid("1.2.3.4.5"));

            var validator = new Validator(ctx);
            var report = validator.Validate(extensionSD, extensionInstance);

            Assert.AreEqual(0, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            // Now remove the choice available for OID
            var extValueDef = extensionSD.Snapshot.Element.Single(e => e.Path == "Extension.value[x]");
            extValueDef.Type.RemoveAll(t => t.Code == FHIRDefinedType.Oid);

            report = validator.Validate(extensionSD, extensionInstance);

            Assert.AreEqual(1, report.Errors);
            Assert.AreEqual(0, report.Warnings);
        }

        [TestMethod]
        public void AutoGeneratesDifferential()
        {
            var identifierBSN = source.GetStructureDefinition("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN");
            Assert.IsNotNull(identifierBSN);

            var instance = new Identifier("http://clearly.incorrect.nl/definition", "1234");

            var validationContext = new ValidationContext { ArtifactSource = source, GenerateSnapshot = false };
            var validator = new Validator(validationContext);

            var report = validator.Validate(identifierBSN, instance);
            Assert.IsTrue(report.ToString().Contains("does not include a snapshot"));

            validationContext.GenerateSnapshot = true;
            report = validator.Validate(identifierBSN, instance);
            Assert.IsFalse(report.ToString().Contains("does not include a snapshot"));

            bool snapshotNeedCalled = false;

            validator.OnSnapshotNeeded += (object s, OnSnapshotNeededEventArgs a) => { snapshotNeedCalled = true;  /* change nothing, warning should return */ };

            report = validator.Validate(identifierBSN, instance);
            Assert.IsTrue(snapshotNeedCalled);
            Assert.IsTrue(report.ToString().Contains("does not include a snapshot"));
        }


        [TestMethod]
        public void ValidatesFixedValue()
        {
            var context = new ValidationContext { ArtifactSource = source, GenerateSnapshot = true };
            var validator = new Validator(context);

            var instance1 = new Identifier("http://clearly.incorrect.nl/definition", "1234");

            var report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN", instance1);
            Assert.AreEqual(1, report.Errors);

            instance1.System = "urn:oid:2.16.840.1.113883.2.4.6.3";

            report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN", instance1);
            Assert.AreEqual(0, report.Errors);

            var weirdSD = (StructureDefinition)source.GetStructureDefinitionForCoreType(FHIRDefinedType.Identifier).DeepCopy();

            // Looks a bit weird, but by setting a complex fixed value on the root
            // we actually limit all instances of the type to that single fixed value
            weirdSD.Snapshot.Element[0].Fixed = (Identifier)instance1.DeepCopy();

            // Should still have 0 errors, since the fixed == the instance
            report = validator.Validate(weirdSD, instance1);
            Assert.AreEqual(0, report.Errors);

            instance1.System = "http://clearly.another.mistake/definition";
            report = validator.Validate(weirdSD, instance1);
            Assert.AreEqual(1, report.Errors);
        }

        //TODO: Could check whether we handle "typeslices" correctly, where
        //typeslices are done without slicing, just having multiple typerefs, which nested
        //constraints. Is that even allowed?            
    }
}
