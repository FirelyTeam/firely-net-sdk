using Hl7.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Validation
{
    [TestClass]
    public class BasicValidationTests
    {
        [TestInitialize]
        public void SetupSource()
        {
            _source = new CachedResolver(
                new MultiResolver(
                    new TestProfileArtifactSource(),
                    new ZipSource("specification.zip")));

            var ctx = new ValidationSettings() { ResourceResolver = _source, GenerateSnapshot = true, EnableXsdValidation = true, Trace = false };

            _validator = new Validator(ctx);
        }

        IResourceResolver _source;
        Validator _validator;

        [TestMethod]
        public void TestEmptyElement()
        {
            var boolSd = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            var data = ElementNode.Node("active").ToNavigator();

            var result = _validator.Validate(data, boolSd);
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
            var boolean = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
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
            var def = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Oid);

            var instance = new Oid("1.2.3.4.q");
            var report = _validator.Validate(instance, def);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            instance = new Oid("1.2.3.4");
            report = _validator.Validate(instance, def);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Errors);
            Assert.AreEqual(0, report.Warnings);
        }


        [TestMethod]
        public void ValidateCardinality()
        {
            var boolSd = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                        ElementNode.Valued("id", "myId1"),
                        ElementNode.Valued("id", "myId2"),
                        ElementNode.Node("extension",
                            ElementNode.Valued("value", 4, "integer")),
                        ElementNode.Node("extension",
                            ElementNode.Valued("value", "world!", "string"))).ToNavigator();

            var report = _validator.Validate(data, boolSd);
            Assert.AreEqual(3, report.ListErrors().Count());
        }

        [TestMethod]
        public void ValidateChoiceElement()
        {
            var extensionSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Extension).DeepCopy();

            var extensionInstance = new Extension("http://some.org/testExtension", new Oid("1.2.3.4.5"));

            var report = _validator.Validate(extensionInstance, extensionSd);

            Assert.AreEqual(0, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            // Now remove the choice available for OID
            var extValueDef = extensionSd.Snapshot.Element.Single(e => e.Path == "Extension.value[x]");
            extValueDef.Type.RemoveAll(t => t.Code == FHIRDefinedType.Oid);

            report = _validator.Validate(extensionInstance, extensionSd);

            Assert.AreEqual(1, report.Errors);
            Assert.AreEqual(0, report.Warnings);
        }

        [TestMethod]
        public void AutoGeneratesDifferential()
        {
            var identifierBsn = _source.FindStructureDefinition("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN");
            Assert.IsNotNull(identifierBsn);

            var instance = new Identifier("http://clearly.incorrect.nl/definition", "1234");

            var validationContext = new ValidationSettings { ResourceResolver = _source, GenerateSnapshot = false };
            var automatedValidator = new Validator(validationContext);

            var report = automatedValidator.Validate(instance, identifierBsn);
            Assert.IsTrue(report.ToString().Contains("does not include a snapshot"));

            validationContext.GenerateSnapshot = true;
            report = automatedValidator.Validate(instance, identifierBsn);
            Assert.IsFalse(report.ToString().Contains("does not include a snapshot"));

            bool snapshotNeedCalled = false;

            // I disabled cloning of SDs in the validator, so the last call to Validate() will have added a snapshot
            // to our local identifierBSN
            identifierBsn.Snapshot = null;

            automatedValidator.OnSnapshotNeeded += (object s, OnSnapshotNeededEventArgs a) => { snapshotNeedCalled = true;  /* change nothing, warning should return */ };

            report = automatedValidator.Validate(instance, identifierBsn);
            Assert.IsTrue(snapshotNeedCalled);
            Assert.IsTrue(report.ToString().Contains("does not include a snapshot"));
        }


        [TestMethod]
        public void ValidatesFixedValue()
        {
            var patientSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);

            var instance1 = new CodeableConcept("http://this.isa.test.nl/definition", "1234");
            instance1.Text = "This is fixed too";

            var maritalStatusElement = patientSd.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Fixed = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient();
            patient.MaritalStatus = instance1;

            var report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Text = "This is incorrect";
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Text = "This is fixed too";
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Coding.Add(new Coding("http://this.isa.test.nl/definition", "5678"));
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);
        }

        [TestMethod]
        public void ValidatesPatternValue()
        {
            var patientSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);

            var instance1 = new CodeableConcept("http://this.isa.test.nl/definition", "1234");

            var maritalStatusElement = patientSd.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Pattern = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient();
            patient.MaritalStatus = instance1;

            var report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Text = "This is irrelevant";
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);

            ((CodeableConcept)maritalStatusElement.Pattern).Text = "Not anymore";
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Text = "Not anymore";
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Coding.Insert(0, new Coding("http://this.isa.test.nl/definition", "5678"));
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(1, report.Errors);
        }

        [TestMethod]
        public void ValidatesMultiplePossibleTypeRefs()
        {
            // Try adding a period

            Patient p = new Patient();
            p.Active = true;

            var identifierBsn = new Identifier("urn:oid:2.16.840.1.113883.2.4.6.3", "1234");
            var identifierDl = new Identifier("urn:oid:2.16.840.1.113883.2.4.6.12", "5678");

            var dutchPatientUri = "http://validationtest.org/fhir/StructureDefinition/DutchPatient";

            // First, Patient without the required identifier
            var report = _validator.Validate(p, dutchPatientUri);
            Assert.AreEqual(1, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            // Now, with the required identifier
            p.Identifier.Add(identifierBsn);

            report = _validator.Validate(p, dutchPatientUri);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            // Make the identifier incorrect
            p.Identifier[0].System = "http://wrong.system";

            report = _validator.Validate(p, dutchPatientUri);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(0, report.Warnings);

            // Add the alternative
            p.Identifier.Clear();
            p.Identifier.Add(identifierDl);
            report = _validator.Validate(p, dutchPatientUri);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);
        }


        [TestMethod]
        public void ValidateOverNameRef()
        {
            var questionnaireXml = File.ReadAllText("TestData\\validation\\questionnaire-sdc-profile-example-cap.xml");

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.IsNotNull(questionnaire);

            // the questionnaire instance references the profile to be validated:
            //      http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType
            var report = _validator.Validate(questionnaire);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(19, report.Errors);
            Assert.AreEqual(26, report.Warnings);           // StructureDefinition/xhtml not found, 3x narrative constraint with no fluentpath + 22 bindings
        }


        [TestMethod]
        public void ValidateChoiceWithConstraints()
        {
            var obs = new Observation();
            obs.Status = Observation.ObservationStatus.Final;
            obs.Code = new CodeableConcept("http://somesystem.org/codes", "AABB");

            obs.Value = new FhirString("I should be ok");
            var report = _validator.Validate(obs, "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation");
            Assert.IsTrue(report.Success);
            Assert.AreEqual(1, report.Warnings);    // missing qty-3 fp invariant

            obs.Value = FhirDateTime.Now();
            report = _validator.Validate(obs, "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Warnings);   // missing qty-3 fp invariant

            obs.Value = new Quantity(78m, "kg");
            report = _validator.Validate(obs, "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation");
            Assert.IsTrue(report.Success);
            Assert.AreEqual(1, report.Warnings);   // 2x missing qty-3 fp invariant

            obs.Value = new Quantity(183m, "cm");
            report = _validator.Validate(obs, "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation");
            Assert.IsTrue(report.Success);
            Assert.AreEqual(1, report.Warnings); // 2x missing qty-3 fp invariant

            obs.Value = new Quantity(300m, "in");
            report = _validator.Validate(obs, "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Warnings); // 3x missing qty-3 fp invariant
        }


        [TestMethod]
        public void ValidateContained()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");

            var careplan = (new FhirXmlParser()).Parse<CarePlan>(careplanXml);
            Assert.IsNotNull(careplan);
            var careplanSd = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.CarePlan);

            var report = _validator.Validate(careplan, careplanSd);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(46, report.Warnings);            // 4x missing xhtml + 42 missing bindings
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
        public void TriggerFpValidationError()
        {
            // pat-1: SHALL at least contain a contact's details or a reference to an organization (xpath: f:name or f:telecom or f:address or f:organization)
            var p = new Patient();

            p.Active = true;

            var report = _validator.Validate(p);
            Assert.IsTrue(report.Success);

            p.Contact.Add(new Patient.ContactComponent { Gender = AdministrativeGender.Male });

            report = _validator.Validate(p);
            Assert.IsFalse(report.Success);

            _validator.Settings.SkipConstraintValidation = true;
            report = _validator.Validate(p);
            Assert.IsTrue(report.Success);

            _validator.Settings.SkipConstraintValidation = false;

            p.Contact.First().Address = new Address() { City = "Amsterdam" };

            report = _validator.Validate(p);
            Assert.IsTrue(report.Success);
        }


        [TestMethod]
        public void ValidateBundle()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.IsNotNull(bundle);

            var ctx = new ValidationSettings() { ResourceResolver = _source, GenerateSnapshot = true, ResolveExteralReferences=true, Trace = false };
            bool hitResolution = false;

            _validator = new Validator(ctx);
            _validator.OnExternalResolutionNeeded += (s, a) => hitResolution = true;

            var report = _validator.Validate(bundle);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(2, report.Warnings);            // 1 unresolvable reference + 1 binding
            Assert.IsTrue(hitResolution);

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithContainedEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(2, report.Warnings);            // 1 unresolvable reference + 1 binding
            Assert.AreEqual(4, report.Errors);            // 4 non-contained references

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithContainedBundledEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(2, report.Warnings);            // 1 unresolvable reference + 1 binding
            Assert.AreEqual(1, report.Errors);            // 1 external reference

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithBundledEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(2, report.Warnings);            // 1 unresolvable reference + 1 binding
            Assert.AreEqual(2, report.Errors);            // 1 external reference, 1 contained reference

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithReferencedEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(2, report.Warnings);            // 1 unresolvable reference + 1 binding
            Assert.AreEqual(4, report.Errors);            // 3 bundled reference, 1 contained reference
        }

        [TestMethod]
        public void RunXsdValidation()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");
            var cpDoc = XDocument.Parse(careplanXml, LoadOptions.SetLineInfo);

            var report = _validator.Validate(cpDoc.CreateReader());
            Assert.IsTrue(report.Success);
            Assert.AreEqual(46, report.Warnings);            // 4x missing xhtml + many "binding not supported"

            // Damage the document by removing the mandated 'status' element
            cpDoc.Element(XName.Get("CarePlan", "http://hl7.org/fhir")).Elements(XName.Get("status", "http://hl7.org/fhir")).Remove();

            report = _validator.Validate(cpDoc.CreateReader());
            Assert.IsFalse(report.Success);
            Assert.IsTrue(report.ToString().Contains(".NET Xsd validation"));
        }

    }
}

