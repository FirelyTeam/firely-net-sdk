using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Hl7.Fhir.Rest;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification.Terminology;

namespace Hl7.Fhir.Validation
{
    [TestClass]
    public class BasicValidationTests
    {
        [TestInitialize]
        public void SetupSource()
        {
            // Ensure the FHIR extensions are registered
            Hl7.Fhir.FhirPath.PocoNavigatorExtensions.PrepareFhirSymbolTableFunctions();

            _source = new CachedResolver(
                new MultiResolver(
                    new BundleExampleResolver(@"TestData\validation"),
                    new DirectorySource(@"TestData\validation"),
                    new TestProfileArtifactSource(),
                    new ZipSource("specification.zip")));

            var ctx = new ValidationSettings()
            {
                ResourceResolver = _source,
                GenerateSnapshot = true,
                EnableXsdValidation = true,
                Trace = false,
                ResolveExteralReferences = true
            };

            // until we have a local terminology service ready, here is the remote implementation
            //ctx.TerminologyService = new ExternalTerminologyService(new FhirClient("http://fhir3.healthintersections.com.au/open"));
            ctx.TerminologyService = new LocalTerminologyServer(_source);

            _validator = new Validator(ctx);
        }

        IResourceResolver _source;
        Validator _validator;

        [TestMethod]
        public void TestEmptyElement()
        {
            var boolSd = _source.FindStructureDefinitionForCoreType(FHIRAllTypes.Boolean);
            var data = ElementNode.Node("active").ToNavigator();

            var result = _validator.Validate(data, boolSd);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ToString().Contains("must not be empty"));
        }


        [TestMethod]
        public void NameMatching()
        {
            var data = ElementNode.Valued("active", true, FHIRAllTypes.Boolean.GetLiteral()).ToNavigator();

            Assert.IsTrue(ChildNameMatcher.NameMatches("active", data));
            Assert.IsTrue(ChildNameMatcher.NameMatches("activeBoolean", data));
            Assert.IsFalse(ChildNameMatcher.NameMatches("activeDateTime", data)); 
            Assert.IsTrue(ChildNameMatcher.NameMatches("active[x]", data));
            Assert.IsFalse(ChildNameMatcher.NameMatches("activate", data));
        }

        [TestMethod]
        public void PrimitiveChildMatching()
        {
            var boolean = _source.FindStructureDefinitionForCoreType(FHIRAllTypes.Boolean);
            var boolDefNav = ElementDefinitionNavigator.ForSnapshot(boolean);
            boolDefNav.MoveToFirstChild();

            var data = ElementNode.Valued("active", true, FHIRAllTypes.Boolean.GetLiteral(),
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
            var def = _source.FindStructureDefinitionForCoreType(FHIRAllTypes.Oid);

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
            var boolSd = _source.FindStructureDefinitionForCoreType(FHIRAllTypes.Boolean);
            var data = ElementNode.Valued("active", true, FHIRAllTypes.Boolean.GetLiteral(),
                        ElementNode.Valued("id", "myId1"),
                        ElementNode.Valued("id", "myId2"),
                        ElementNode.Node("extension",
                            ElementNode.Valued("value", 4L, "integer")),
                        ElementNode.Node("extension",
                            ElementNode.Valued("value", "world!", "string"))).ToNavigator();

            var report = _validator.Validate(data, boolSd);
            Assert.AreEqual(3, report.Errors);
            Assert.AreEqual(0, report.Warnings);
        }

        [TestMethod]
        public void ValidateChoiceElement()
        {
            var extensionSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Extension).DeepCopy();

            var extensionInstance = new Extension("http://some.org/testExtension", new Oid("1.2.3.4.5"));

            var report = _validator.Validate(extensionInstance, extensionSd);

            Assert.AreEqual(0, report.Errors);
            Assert.AreEqual(0, report.Warnings);

            // Now remove the choice available for OID
            var extValueDef = extensionSd.Snapshot.Element.Single(e => e.Path == "Extension.value[x]");
            extValueDef.Type.RemoveAll(t => ModelInfo.FhirTypeNameToFhirType(t.Code) == FHIRAllTypes.Oid);

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
            var patientSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Patient);

            var instance1 = new CodeableConcept("http://hl7.org/fhir/marital-status", "U");
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

            patient.MaritalStatus.Coding.Add(new Coding("http://hl7.org/fhir/v3/MaritalStatus", "L"));
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(1, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = _validator.Validate(patient, patientSd);
            Assert.AreEqual(0, report.Errors);
        }

        [TestMethod]
        public void ValidatesPatternValue()
        {
            var patientSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRAllTypes.Patient);

            var instance1 = new CodeableConcept("http://hl7.org/fhir/marital-status", "U");

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

            patient.MaritalStatus.Coding.Insert(0, new Coding("http://hl7.org/fhir/v3/MaritalStatus", "L"));
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
            var questionnaireXml = File.ReadAllText("TestData\\validation\\questionnaire-with-incorrect-fixed-type.xml");

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.IsNotNull(questionnaire);

            // the questionnaire instance references the profile to be validated:
            //      http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType
            var report = _validator.Validate(questionnaire, "http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(2, report.Errors);
            Assert.AreEqual(0, report.Warnings);           // 3x narrative constraint with no fhirpath
        }


        [TestMethod]
        public void ValidateChoiceWithConstraints()
        {
            var obs = new Observation();
            obs.Status = ObservationStatus.Final;
            obs.Code = new CodeableConcept("http://somesystem.org/codes", "AABB");
            obs.Meta = new Meta { Profile = new[] { "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation" } };

            _validator.Settings.Trace = true;

            obs.Value = new FhirString("I should be ok");
            var report = _validator.Validate(obs);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            obs.Value = FhirDateTime.Now();
            report = _validator.Validate(obs);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(0, report.Warnings);

            obs.Value = new Quantity(78m, "kg");
            report = _validator.Validate(obs);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            obs.Value = new Quantity(183m, "cm");
            report = _validator.Validate(obs);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            obs.Value = new Quantity(300m, "in");
            report = _validator.Validate(obs);
            Assert.IsFalse(report.Success);
            Assert.AreEqual(0, report.Warnings);
        }


        [TestMethod]
        public void ValidateContained()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");

            var careplan = (new FhirXmlParser()).Parse<CarePlan>(careplanXml);
            Assert.IsNotNull(careplan);
            var careplanSd = _source.FindStructureDefinitionForCoreType(FHIRAllTypes.CarePlan);
            //var old = _validator.Settings.ResolveExteralReferences;
            //_validator.Settings.ResolveExteralReferences = false;
            var report = _validator.Validate(careplan, careplanSd);
            //_validator.Settings.ResolveExteralReferences = old;
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);            // 3x invariant
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

            var ctx = new ValidationSettings() { ResourceResolver = _source, GenerateSnapshot = true, ResolveExteralReferences = true, Trace = false };
            bool hitResolution = false;

            _validator = new Validator(ctx);
            _validator.OnExternalResolutionNeeded += (s, a) => hitResolution = true;

            var report = _validator.Validate(bundle);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(1, report.Warnings);            // 1 unresolvable reference
            Assert.IsTrue(hitResolution);

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithContainedEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Warnings);            // 1 unresolvable reference
            Assert.AreEqual(4, report.Errors);            // 4 non-contained references

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithContainedBundledEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Warnings);            // 1 unresolvable reference
            Assert.AreEqual(1, report.Errors);            // 1 external reference

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithBundledEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Warnings);            // 1 unresolvable reference
            Assert.AreEqual(2, report.Errors);            // 1 external reference, 1 contained reference

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithReferencedEntries");
            Assert.IsFalse(report.Success);
            Assert.AreEqual(1, report.Warnings);            // 1 unresolvable reference
            Assert.AreEqual(4, report.Errors);            // 3 bundled reference, 1 contained reference
        }

        [TestMethod]
        public void RunXsdValidation()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");
            var cpDoc = XDocument.Parse(careplanXml, LoadOptions.SetLineInfo);

            var report = _validator.Validate(cpDoc.CreateReader());
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);            // 3x missing invariant

            // Damage the document by removing the mandated 'status' element
            cpDoc.Element(XName.Get("CarePlan", "http://hl7.org/fhir")).Elements(XName.Get("status", "http://hl7.org/fhir")).Remove();

            report = _validator.Validate(cpDoc.CreateReader());
            Assert.IsFalse(report.Success);
            Assert.IsTrue(report.ToString().Contains(".NET Xsd validation"));
        }

        [TestMethod]
        public void TestBindingValidation()
        {
            var p = new Patient();

            p.MaritalStatus = new CodeableConcept("http://hl7.org/fhir/v3/MaritalStatus", "S");

            var report = _validator.Validate(p);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            p.MaritalStatus.Coding[0].Code = "XX";

            report = _validator.Validate(p);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(1, report.Warnings);
            Assert.IsTrue(report.ToString().Contains("not valid for non-required binding"));

            p.MaritalStatus = new CodeableConcept("http://hl7.org/fhir/v3/MaritalStatus", "S");
        }

        [TestMethod]
        public void TestChoiceBindingValidation()
        {
            var profile = "http://validationtest.org/fhir/StructureDefinition/ParametersWithBoundParams";
            var cc = new CodeableConcept();
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "NaN"));
            cc.Coding.Add(new Coding("http://hl7.org/fhir/data-absent-reason", "not-asked"));

            var p = new Parameters();
            p.Add("cc", cc);
            p.Add("c", new Coding("http://hl7.org/fhir/data-absent-reason", "NaN"));
            p.Add("s", new FhirString("not-asked"));

            var report = _validator.Validate(p, profile);
            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            p.Remove("s");
            p.Add("s", new FhirString("not-a-member"));
            report = _validator.Validate(p, profile);
            Assert.IsFalse(report.Success);
            Assert.IsTrue(report.ToString().Contains("not-a-member"));
            Assert.AreEqual(0, report.Warnings);
        }


        [TestMethod]
        public void ValidateExtensionExamples()
        {
            var levinXml = File.ReadAllText(@"TestData\validation\Levin.patient.xml");
            var levin = (new FhirXmlParser()).Parse<Patient>(levinXml);
            Assert.IsNotNull(levin);

            var report = _validator.Validate(levin);

            Assert.IsTrue(report.Success);
            Assert.AreEqual(0, report.Warnings);

            // Now, rename the mandatory NCT sub-extension
            levin.Extension[1].Extension[0].Url = "NCTX";
            report = _validator.Validate(levin);
            Assert.IsFalse(report.Success);
            Assert.IsTrue(report.ToString().Contains("Instance count for 'Extension.extension:NCT' is 0"));

            levin.Extension[1].Extension[0].Url = "NCT";
            levin.Extension[1].Extension[1].Value = new FhirString("wrong!");
            report = _validator.Validate(levin);
            Assert.IsFalse(report.Success);
            Assert.IsTrue(report.ToString().Contains("The declared type of the element (Period) is incompatible with that of the instance ('string')"));
        }

        [TestMethod]
        public void ValidateBundleExample()
        {
            var bundle = _source.ResolveByUri("http://example.org/examples/Bundle/MainBundle");
            Assert.IsNotNull(bundle);

            var report = _validator.Validate(bundle);

            Assert.IsTrue(report.Success);
            Assert.AreEqual(1, report.Warnings);
        }
    

        internal class BundleExampleResolver : IResourceResolver
        {
            private string _path;

            public BundleExampleResolver(string path)
            {
                _path = path;
            }
            public Resource ResolveByCanonicalUri(string uri)
            {
                // throw new NotImplementedException(); // Slow, pollutes debug output window
                return null;
            }

            public Resource ResolveByUri(string uri)
            {
                ResourceIdentity reference = new ResourceIdentity(uri);
                var filename = $"{reference.Id}.{reference.ResourceType}.xml";
                var path = Path.Combine(_path, filename);

                if (File.Exists(path))
                {
                    var xml = File.ReadAllText(path);
                    return (new FhirXmlParser()).Parse<Resource>(xml);
                }
                else
                    return null;
            }

        }

        class InMemoryResourceResolver : IResourceResolver
        {
            ILookup<string, Resource> _resources;

            public InMemoryResourceResolver(IEnumerable<Resource> profiles)
            {
                _resources = profiles.ToLookup(r => getResourceUri(r), r => r as Resource);
            }

            public InMemoryResourceResolver(Resource profile) : this(new Resource[] { profile }) { }

            public Resource ResolveByCanonicalUri(string uri) => null;

            public Resource ResolveByUri(string uri) => _resources[uri].FirstOrDefault();

            // cf. ResourceStreamScanner.StreamResources
            static string getResourceUri(Resource res) => res.TypeName + "/" + res.Id;
        }

        // [WMR 20161220] Example by Christiaan Knaap
        // Causes stack overflow exception in validator when processing the related Organization profile
        // TypeRefValidationExtensions.ValidateTypeReferences needs to detect and handle recursion
        // Example: Organization.partOf => Organization
        [TestMethod,Ignore] // Causes stack overflow exception
        public void TestPatientWithOrganization()
        {
            // DirectorySource (and ResourceStreamScanner) does not support json...
            // var source = new DirectorySource(@"TestData\validation");
            // var res = source.ResolveByUri("Patient/pat1"); // cf. "Patient/Levin"

            var jsonPatient = File.ReadAllText(@"TestData\validation\patient-ck.json");
            var parser = new FhirJsonParser();
            var patient = parser.Parse<Patient>(jsonPatient);
            Assert.IsNotNull(patient);

            var jsonOrganization = File.ReadAllText(@"TestData\validation\organization-ck.json");
            var organization = parser.Parse<Organization>(jsonOrganization);
            Assert.IsNotNull(organization);

            var resources = new Resource[] { patient, organization };
            var memResolver = new InMemoryResourceResolver(resources);

            // [WMR 20161220] Validator always uses existing snapshots if present
            // ProfilePreprocessor.GenerateSnapshots:
            // if (!sd.HasSnapshot) { ... snapshotGenerator(sd) ... }

            // Create custom source to properly force snapshot expansion
            // Run validator on instance
            // Afterwards, verify that instance profile has been expanded

            var source = new CachedResolver(
                // Clear snapshots after initial load
                // This will force the validator to regenerate all snapshots
                new ClearSnapshotResolver(
                    new MultiResolver(
                        // new BundleExampleResolver(@"TestData\validation"),
                        // new DirectorySource(@"TestData\validation"),
                        // new TestProfileArtifactSource(),
                        memResolver,
                        new ZipSource("specification.zip"))));

            var ctx = new ValidationSettings()
            {
                ResourceResolver = source,
                GenerateSnapshot = true,
                EnableXsdValidation = true,
                Trace = false,
                ResolveExteralReferences = true
            };

            var validator = new Validator(ctx);

            var report = validator.Validate(patient);
            Assert.IsTrue(report.Success);

            // Assert.AreEqual(4, report.Warnings);

            // To check for ele-1 constraints on expanded Patient snapshot:
            // source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient).Snapshot.Element.Select(e=>e.Path + " : " + e.Constraint.FirstOrDefault()?.Key ?? "").ToArray()
            var patientStructDef = source.FindStructureDefinitionForCoreType(FHIRAllTypes.Patient);
            Assert.IsNotNull(patientStructDef);
            Assert.IsTrue(patientStructDef.HasSnapshot);
            assertElementConstraints(patientStructDef.Snapshot.Element);
        }

        // Verify aggregated element constraints
        static void assertElementConstraints(List<ElementDefinition> patientElems)
        {
            foreach (var elem in patientElems)
            {
                if (elem.IsRootElement())
                {
                    // DomainResource constraints dom-1 ... dom-4 are defined in reversed order (specification.zip/profile-resources.xml)
                    // Assert.AreEqual("dom-4", elem.Constraint.FirstOrDefault()?.Key);
                    var constraintKeys = elem.Constraint.Select(c => c.Key).ToList();
                    Assert.IsTrue(constraintKeys.Contains("dom-1"));
                    Assert.IsTrue(constraintKeys.Contains("dom-2"));
                    Assert.IsTrue(constraintKeys.Contains("dom-3"));
                    Assert.IsTrue(constraintKeys.Contains("dom-4"));
                }
                else if (!elem.Path.EndsWith(".contained"))
                {
                    // ele-1 should always be the first constraint
                    Assert.AreEqual("ele-1", elem.Constraint.FirstOrDefault()?.Key);
                }
            }
        }

        class ClearSnapshotResolver : IResourceResolver
        {
            IResourceResolver _resolver;
            public ClearSnapshotResolver(IResourceResolver resolver)
            {
                _resolver = resolver;
            }

            public Resource ResolveByCanonicalUri(string uri)
            {
                var result = _resolver.ResolveByCanonicalUri(uri);
                return clearSnapshot(result);
            }

            public Resource ResolveByUri(string uri)
            {
                var result = _resolver.ResolveByUri(uri);
                return clearSnapshot(result);
            }

            private static Resource clearSnapshot(Resource result)
            {
                var sd = result as StructureDefinition;
                if (sd != null && sd.HasSnapshot)
                {
                    sd.Snapshot = null;
                }
                return result;
            }
        }

    }
}

