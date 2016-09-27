using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            source = new CachedResolver(
                new MultiResolver(
                    new TestProfileArtifactSource(),
                    new ZipSource("specification.zip")));

            var ctx = new ValidationSettings() { ResourceResolver = source, GenerateSnapshot = true, Trace = true };
            ctx.GenerateSnapshotSettings = Specification.Snapshot.SnapshotGeneratorSettings.Default;
            ctx.GenerateSnapshotSettings.ExpandExternalProfiles = true;
            validator = new Validator(ctx);
        }

        IResourceResolver source;
        Validator validator;

        [TestMethod]
        public void TestEmptyElement()
        {
            var boolSD = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            var data = ElementNode.Node("active").ToNavigator();

            var result = validator.Validate(boolSD, data);
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
            var boolean = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            var boolDefNav = ElementDefinitionNavigator.ForSnapshot(boolean);
            boolDefNav.MoveToFirstChild();

            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                    ElementNode.Node("extension",
                        ElementNode.Valued("value", 4, "integer")),
                    ElementNode.Node("nonExistant")                                             
                        ).ToNavigator();

            var matches = ChildNameMatcher.Match(boolDefNav, data);
            Assert.AreEqual(1, matches.UnmatchedInstanceElements.Count);
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
            var def = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Oid);

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
            var boolSD = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                        ElementNode.Valued("id", "myId1"),
                        ElementNode.Valued("id", "myId2"),
                        ElementNode.Node("extension",
                            ElementNode.Valued("value", 4, "integer")),
                        ElementNode.Node("extension",
                            ElementNode.Valued("value", "world!", "string"))).ToNavigator();

            var report = validator.Validate(boolSD, data);
            Assert.AreEqual(3, report.ListErrors().Count());
        }

        [TestMethod]
        public void ValidateChoiceElement()
        {
            var extensionSD = (StructureDefinition)source.FindStructureDefinitionForCoreType(FHIRDefinedType.Extension).DeepCopy();

            var extensionInstance = new Extension("http://some.org/testExtension", new Oid("1.2.3.4.5"));

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
            var identifierBSN = source.FindStructureDefinition("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN");
            Assert.IsNotNull(identifierBSN);

            var instance = new Identifier("http://clearly.incorrect.nl/definition", "1234");

            var validationContext = new ValidationSettings { ResourceResolver = source, GenerateSnapshot = false };
            var automatedValidator = new Validator(validationContext);

            var report = automatedValidator.Validate(identifierBSN, instance);
            Assert.IsTrue(report.ToString().Contains("does not include a snapshot"));

            validationContext.GenerateSnapshot = true;
            report = automatedValidator.Validate(identifierBSN, instance);
            Assert.IsFalse(report.ToString().Contains("does not include a snapshot"));

            bool snapshotNeedCalled = false;

            // I disabled cloning of SDs in the validator, so the last call to Validate() will have added a snapshot
            // to our local identifierBSN
            identifierBSN.Snapshot = null;

            automatedValidator.OnSnapshotNeeded += (object s, OnSnapshotNeededEventArgs a) => { snapshotNeedCalled = true;  /* change nothing, warning should return */ };

            report = automatedValidator.Validate(identifierBSN, instance);
            Assert.IsTrue(snapshotNeedCalled);
            Assert.IsTrue(report.ToString().Contains("does not include a snapshot"));
        }


        [TestMethod]
        public void ValidatesFixedValue()
        {
            var patientSD = (StructureDefinition)source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);

            var instance1 = new CodeableConcept("http://this.isa.test.nl/definition", "1234");
            instance1.Text = "This is fixed too";

            var maritalStatusElement = patientSD.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Fixed = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient();
            patient.MaritalStatus = instance1;

            var report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Text = "This is incorrect";
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Text = "This is fixed too";
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Coding.Add(new Coding("http://this.isa.test.nl/definition", "5678"));
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);
        }

        [TestMethod]
        public void ValidatesPatternValue()
        {
            var patientSD = (StructureDefinition)source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);

            var instance1 = new CodeableConcept("http://this.isa.test.nl/definition", "1234");

            var maritalStatusElement = patientSD.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Pattern = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient();
            patient.MaritalStatus = instance1;

            var report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Text = "This is irrelevant";
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);

            ((CodeableConcept)maritalStatusElement.Pattern).Text = "Not anymore";
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Text = "Not anymore";
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Coding.Insert(0,new Coding("http://this.isa.test.nl/definition", "5678"));
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = validator.Validate(patientSD, patient);
            Assert.AreEqual(1, report.Errors);
        }

        [TestMethod]
        public void ValidatesMultiplePossibleTypeRefs()
        {
            // Try adding a period

            Patient p = new Patient();
            p.Active = true;

            var identifierBSN = new Identifier("urn:oid:2.16.840.1.113883.2.4.6.3", "1234");
            var identifierDL = new Identifier("urn:oid:2.16.840.1.113883.2.4.6.12", "5678");

            var dutchPatientUri = "http://validationtest.org/fhir/StructureDefinition/DutchPatient";

            // First, Patient without the required identifier
            var report = validator.Validate(dutchPatientUri, p);
            Assert.AreEqual(1, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            // Now, with the required identifier
            p.Identifier.Add(identifierBSN);

            report = validator.Validate(dutchPatientUri, p);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            // Make the identifier incorrect
            p.Identifier[0].System = "http://wrong.system";

            report = validator.Validate(dutchPatientUri, p);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(0, report.Warnings);

            // Add the alternative
            p.Identifier.Clear();
            p.Identifier.Add(identifierDL);
            report = validator.Validate(dutchPatientUri, p);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);
        }


        [TestMethod]
        public void ValidateOverNameRef()
        {
            var questionnaireXml = File.ReadAllText("TestData\\validation\\questionnaire-sdc-profile-example-cap.xml");

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.IsNotNull(questionnaire);
            var questionnaireSD = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Questionnaire);

            var report = validator.Validate(questionnaireSD, questionnaire);
            Assert.IsTrue(report.Success);

            report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType", questionnaire);

            Assert.IsFalse(report.Success);
            Assert.AreEqual(19, report.Errors);
            Assert.AreEqual(4, report.Warnings);           // StructureDefinition/xhtml not found, 3x narrative constraint with no fluentpath
        }


        [TestMethod]
        public void ValidateChoiceWithConstraints()
        {
            var obs = new Observation();
            obs.Status = Observation.ObservationStatus.Final;
            obs.Code = new CodeableConcept("http://somesystem.org/codes", "AABB");

            obs.Value = new FhirString("I should be ok");
            var report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", obs);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);    // missing qty-3 fp invariant

            obs.Value = FhirDateTime.Now();
            report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", obs);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(0, report.Warnings);   // missing qty-3 fp invariant

            obs.Value = new Quantity(78m, "kg");
            report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", obs);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);   // 2x missing qty-3 fp invariant

            obs.Value = new Quantity(183m, "cm");
            report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", obs);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings); // 2x missing qty-3 fp invariant

            obs.Value = new Quantity(300m, "in");
            report = validator.Validate("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", obs);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(0, report.Warnings); // 3x missing qty-3 fp invariant
        }


        [TestMethod]
        public void ValidateContained()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");

            var careplan = (new FhirXmlParser()).Parse<CarePlan>(careplanXml);
            Assert.IsNotNull(careplan);
            var careplanSD = source.FindStructureDefinitionForCoreType(FHIRDefinedType.CarePlan);

            var report = validator.Validate(careplanSD, careplan);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(4, report.Warnings);            // 4x missing xhtml
        }


        [TestMethod]
        public void MeasureDeepCopyPerformance()
        {
            var questionnaireXml = File.ReadAllText("TestData\\validation\\questionnaire-sdc-profile-example-cap.xml");

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.IsNotNull(questionnaire);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10000; i++)
            {
                var x = (Questionnaire)questionnaire.DeepCopy();
            }
            sw.Stop();

            Debug.WriteLine(sw.ElapsedMilliseconds / 10000.0);
        }

        [TestMethod]
        public void TriggerFPValidationError()
        {
            // pat-1: SHALL at least contain a contact's details or a reference to an organization (xpath: f:name or f:telecom or f:address or f:organization)
            var p = new Patient();

            p.Active = true;

            var report = validator.Validate(p);
            Assert.IsTrue(report.Success);

            p.Contact.Add(new Patient.ContactComponent { Gender = AdministrativeGender.Male });

            report = validator.Validate(p);
            Assert.IsFalse(report.Success);

            validator.Settings.SkipConstraintValidation = true;
            report = validator.Validate(p);
            Assert.IsTrue(report.Success);

            validator.Settings.SkipConstraintValidation = false;

            p.Contact.First().Address = new Address() { City = "Amsterdam" };

            report = validator.Validate(p);
            Assert.IsTrue(report.Success);
        }

        [TestMethod]
        public void HarvestScopedIdentifiers()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            var cpNav = new PocoNavigator(bundle);

            var references = ReferenceHarvester.Harvest(cpNav);
            Assert.AreEqual(7, references.Count());
            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else

            // Get one of the entries with no contained resources, an Organization
            var orgNav = references.Single(r => r.Uri == "urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d");
            Assert.AreEqual("Organization", orgNav.Container.TypeName);
            var orgReferences = ReferenceHarvester.Harvest(orgNav.Container);
            Assert.AreEqual(0, orgReferences.Count());
            Assert.AreEqual("Organization", orgNav.Container.TypeName);      // should not have navigated somewhere else

            // Get one of the entries with contained resources, a Patient
            var patNav = references.First(r => r.Uri == "http://example.org/fhir/Patient/e");
            Assert.AreEqual("Patient", patNav.Container.TypeName);
            var patReferences = ReferenceHarvester.Harvest(patNav.Container);
            Assert.AreEqual(2, patReferences.Count());
            Assert.AreEqual("#orgX", patReferences.First().Uri);

            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else
        }

        [TestMethod]
        public void ScopeAndChildCreation()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            var cpNav = new PocoNavigator(bundle);

            var bundleScope = new Scope(cpNav, "http://somebase.org/fhir/Bundle/test");

            Assert.AreEqual(7, bundleScope.Children.Count());
            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else

            // Get one of the entries with no contained resources, an Organization
            var orgScope = bundleScope.ResolveChild("urn:uuid:04121321-4af5-424c-a0e1-ed3aab1c349d");
            Assert.AreEqual("Organization", orgScope.Container.TypeName);
            Assert.AreEqual(0, orgScope.Children.Count());
            Assert.AreEqual("Organization", orgScope.Container.TypeName);
            Assert.AreEqual("Bundle", bundleScope.Container.TypeName);          

            // Get one of the entries with contained resources, a Patient
            var patScope = bundleScope.ResolveChild("http://example.org/fhir/Patient/e");
            Assert.AreEqual("Patient", patScope.Container.TypeName);
            Assert.AreEqual(2, patScope.Children.Count());
            Assert.AreEqual("#orgX", patScope.Children.First().Uri);

            Assert.AreEqual("Bundle", cpNav.TypeName);      // should not have navigated somewhere else
        }

        [TestMethod]
        public void BuildScopeList()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);
            IElementNavigator cpNav = new PocoNavigator(bundle);

            var tracker = new ScopeTracker();
            tracker.Enter(cpNav);

            var entries = cpNav.GetChildrenByName("entry").GetChildrenByName("resource");

            int index = 0;
            foreach (var entry in entries)
            {
                tracker.Enter(entry);

                if (index == 6)
                {
                    var orgX =  entry.GetChildrenByName("contained").First();

                    tracker.Enter(orgX);
                    Assert.AreEqual("http://example.org/fhir/Patient/e", tracker.ContextFullUrl(orgX));
                    Assert.AreEqual("Bundle.entry[6].resource[0].contained[0]", tracker.ResourceContext(orgX).Path);
                    tracker.Leave(orgX);

                    var careProvRef = entry.GetChildrenByName("careProvider").GetChildrenByName("reference").Single();
                    Assert.AreEqual("Bundle.entry[6].resource[0]", tracker.ResourceContext(careProvRef).Path);

                    tracker.Enter(careProvRef);
                    Assert.AreEqual("Bundle.entry[6].resource[0].contained[1]", tracker.Resolve(careProvRef, "#orgY").Path);
                    Assert.AreEqual("Bundle.entry[2].resource[0]", tracker.Resolve(careProvRef, "http://example.org/fhir/Patient/a").Path);
                    tracker.Leave(careProvRef);
                }

                tracker.Leave(entry);              

                index++;
            }

            tracker.Leave(cpNav);
        }
    }
}

