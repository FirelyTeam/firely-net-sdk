using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Xunit;
using System;
using Hl7.Fhir.Validation;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Hl7.Fhir.Specification.Tests
{
    [Trait("Category", "Validation")]
    public class BasicValidationTests : IClassFixture<ValidationFixture>
    {
        IResourceResolver _source;
        Validator _validator;
        private readonly Xunit.Abstractions.ITestOutputHelper output;

        public BasicValidationTests(ValidationFixture fixture, Xunit.Abstractions.ITestOutputHelper output)
        {
            _source = fixture.Resolver;
            _validator = fixture.Validator;
            this.output = output;
        }

        //[TestInitialize]
        //public void SetupSource()
        //{
        //    // Ensure the FHIR extensions are registered
        //    FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

        //    _source = new CachedResolver(
        //        new MultiResolver(
        //            new BundleExampleResolver(@"TestData\validation"),
        //            new DirectorySource(@"TestData\validation"),
        //            new TestProfileArtifactSource(),
        //            new ZipSource("specification.zip")));

        //    var ctx = new ValidationSettings()
        //    {
        //        ResourceResolver = _source,
        //        GenerateSnapshot = true,
        //        EnableXsdValidation = true,
        //        Trace = false,
        //        ResolveExteralReferences = true
        //    };

        //    _validator = new Validator(ctx);
        //}


        [Fact]
        public void TestEmptyElement()
        {
            var boolSd = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Boolean);
            var data = ElementNode.Node("active").ToNavigator();

            var result = _validator.Validate(data, boolSd);
            Assert.False(result.Success);
            Assert.True(result.ToString().Contains("must not be empty"));
        }


        [Fact]
        public void NameMatching()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral()).ToNavigator();

            Assert.True(ChildNameMatcher.NameMatches("active", data));
            Assert.True(ChildNameMatcher.NameMatches("activeBoolean", data));
            Assert.False(ChildNameMatcher.NameMatches("activeDateTime", data));
            Assert.True(ChildNameMatcher.NameMatches("active[x]", data));
            Assert.False(ChildNameMatcher.NameMatches("activate", data));
        }

        [Fact]
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

            var matches = ChildNameMatcher.Match(boolDefNav, new ScopedNavigator(data));
            Assert.Equal(1, matches.UnmatchedInstanceElements.Count);
            Assert.Equal(3, matches.Matches.Count());        // id, extension, value
            Assert.Equal(0, matches.Matches[0].InstanceElements.Count()); // id
            Assert.Equal(1, matches.Matches[1].InstanceElements.Count()); // extension
            Assert.Equal(1, matches.Matches[2].InstanceElements.Count()); // value

            Assert.Equal("extension", matches.Matches[1].InstanceElements.First().Name);
            Assert.Equal("extension", matches.Matches[1].Definition.PathName);
            Assert.Equal("active", matches.Matches[2].InstanceElements.First().Name);
            Assert.Equal("value", matches.Matches[2].Definition.PathName);
        }


        [Fact]
        public void ValidatePrimitiveValue()
        {
            var def = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.Oid);

            var instance = new Oid("urn:oid:1.2.3.4.q");
            var report = _validator.Validate(instance, def);
            Assert.False(report.Success);
            Assert.Equal(1, report.Errors);
            Assert.Equal(0, report.Warnings);

            instance = new Oid("urn:oid:1.2.3.4");
            report = _validator.Validate(instance, def);
            Assert.True(report.Success);
            Assert.Equal(0, report.Errors);
            Assert.Equal(0, report.Warnings);
        }


        [Fact]
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
            Assert.Equal(3, report.ListErrors().Count());
        }

        [Fact]
        public void ValidateChoiceElement()
        {
            var extensionSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Extension).DeepCopy();

            var extensionInstance = new Extension("http://some.org/testExtension", new Oid("urn:oid:1.2.3.4.5"));

            var report = _validator.Validate(extensionInstance, extensionSd);

            Assert.Equal(0, report.Errors);
            Assert.Equal(0, report.Warnings);

            // Now remove the choice available for OID
            var extValueDef = extensionSd.Snapshot.Element.Single(e => e.Path == "Extension.value[x]");
            extValueDef.Type.RemoveAll(t => t.Code == FHIRDefinedType.Oid);

            report = _validator.Validate(extensionInstance, extensionSd);

            Assert.Equal(1, report.Errors);
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
        public void AutoGeneratesDifferential()
        {
            var identifierBsn = _source.FindStructureDefinition("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN");
            Assert.NotNull(identifierBsn);
            identifierBsn.Snapshot = null;

            var instance = new Identifier("http://clearly.incorrect.nl/definition", "1234");

            var validationContext = new ValidationSettings { ResourceResolver = _source, GenerateSnapshot = false };
            var automatedValidator = new Validator(validationContext);

            var report = automatedValidator.Validate(instance, identifierBsn);
            Assert.True(report.ToString().Contains("does not include a snapshot"));

            validationContext.GenerateSnapshot = true;
            report = automatedValidator.Validate(instance, identifierBsn);
            Assert.False(report.ToString().Contains("does not include a snapshot"));

            bool snapshotNeedCalled = false;

            // I disabled cloning of SDs in the validator, so the last call to Validate() will have added a snapshot
            // to our local identifierBSN
            identifierBsn.Snapshot = null;

            automatedValidator.OnSnapshotNeeded += (object s, OnSnapshotNeededEventArgs a) => { snapshotNeedCalled = true;  /* change nothing, warning should return */ };

            report = automatedValidator.Validate(instance, identifierBsn);
            Assert.True(snapshotNeedCalled);
            Assert.True(report.ToString().Contains("does not include a snapshot"));
        }


        [Fact]
        public void ValidatesFixedValue()
        {
            var patientSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient).DeepCopy();

            var instance1 = new CodeableConcept("http://hl7.org/fhir/marital-status", "U");
            instance1.Text = "This is fixed too";

            var maritalStatusElement = patientSd.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Fixed = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient();
            patient.MaritalStatus = instance1;

            var report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);

            patient.MaritalStatus.Text = "This is incorrect";
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(1, report.Errors);

            patient.MaritalStatus.Text = "This is fixed too";
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);

            patient.MaritalStatus.Coding.Add(new Coding("http://hl7.org/fhir/v3/MaritalStatus", "L"));
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(1, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);
        }

        [Fact]
        public void ValidatesPatternValue()
        {
            // [WMR 20170727] Fixed
            // Do NOT modify common core Patient definition, as this would affect all subsequent tests.
            // Instead, clone the core def and modify the clone
            var patientSd = (StructureDefinition)_source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient).DeepCopy();

            var instance1 = new CodeableConcept("http://hl7.org/fhir/marital-status", "U");

            var maritalStatusElement = patientSd.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Pattern = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient();
            patient.MaritalStatus = instance1;

            var report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);

            patient.MaritalStatus.Text = "This is irrelevant";
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);

            ((CodeableConcept)maritalStatusElement.Pattern).Text = "Not anymore";
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(1, report.Errors);

            patient.MaritalStatus.Text = "Not anymore";
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);

            patient.MaritalStatus.Coding.Insert(0, new Coding("http://hl7.org/fhir/v3/MaritalStatus", "L"));
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(0, report.Errors);

            patient.MaritalStatus.Coding.RemoveAt(1);
            report = _validator.Validate(patient, patientSd);
            Assert.Equal(1, report.Errors);
        }

        [Fact]
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
            Assert.Equal(1, report.Errors);
            Assert.Equal(0, report.Warnings);

            // Now, with the required identifier
            p.Identifier.Add(identifierBsn);

            report = _validator.Validate(p, dutchPatientUri);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            // Make the identifier incorrect
            p.Identifier[0].System = "http://wrong.system";

            report = _validator.Validate(p, dutchPatientUri);
            Assert.False(report.Success);
            Assert.Equal(0, report.Warnings);

            // Add the alternative
            p.Identifier.Clear();
            p.Identifier.Add(identifierDl);
            report = _validator.Validate(p, dutchPatientUri);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
        public void ValidateOrganizationWithRegEx()
        {
            var o = new Organization() { Name = "firely" };
            var report = _validator.Validate(o, "http://validationtest.org/fhir/StructureDefinition/MyOrganization");

            Assert.False(report.Success);
            Assert.Equal(0, report.Warnings);

            o = new Organization() { Name = "Firely" }; // the first char is now uppercase
            report = _validator.Validate(o, "http://validationtest.org/fhir/StructureDefinition/MyOrganization");

            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

        }

        [Fact]
        public void ValidateOrganizationWithRegExOnType()
        {
            var o = new Organization() { Name = "firely" };
            var report = _validator.Validate(o, "http://validationtest.org/fhir/StructureDefinition/MyOrganization2");

            Assert.False(report.Success);
            Assert.Equal(0, report.Warnings);

            o = new Organization() { Name = "Firely" }; // the first char is now uppercase
            report = _validator.Validate(o, "http://validationtest.org/fhir/StructureDefinition/MyOrganization2");

            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

        }

        [Fact]
        public void DoNotFollowRefsSuppressesWarning()
        {
            var validator = new Validator(new ValidationSettings { ResourceResolver = _source, ResolveExteralReferences = true });

            Patient p = new Patient();
            p.Active = true;
            p.ManagingOrganization = new ResourceReference("http://reference.cannot.be.found.nl/fhir/Patient/1");

            var result = validator.Validate(p);
            Assert.True(result.Success);
            Assert.Equal(1, result.Warnings);
            Assert.Contains("Cannot resolve reference http://reference.cannot.be.found.nl/fhir/Patient/1", result.Issue[0].ToString());

            validator.Settings.ResolveExteralReferences = false;

            result = validator.Validate(p);
            Assert.True(result.Success);
            Assert.Equal(0, result.Warnings);
        }

        [Fact]
        public void ValidateOverNameRef()
        {
            var questionnaireXml = File.ReadAllText("TestData\\validation\\questionnaire-sdc-profile-example-cap.xml");

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.NotNull(questionnaire);

            // the questionnaire instance references the profile to be validated:
            //      http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType
            var report = _validator.Validate(questionnaire);
            Assert.False(report.Success);
            Assert.Equal(35, report.Errors);
            Assert.Equal(0, report.Warnings);           // 20 warnings about valueset too complex
        }


        [Fact]
        public void ValidateChoiceWithConstraints()
        {
            var obs = new Observation()
            {
                Status = Observation.ObservationStatus.Final,
                Code = new CodeableConcept("http://somesystem.org/codes", "AABB"),
                Meta = new Meta { Profile = new[] { "http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation" } }
            };

            _validator.Settings.Trace = true;

            obs.Value = new FhirString("I should be ok");
            var report = _validator.Validate(obs);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);   // 1 warning about valueset too complex

            obs.Value = FhirDateTime.Now();
            report = _validator.Validate(obs);
            Assert.False(report.Success);
            Assert.Equal(0, report.Warnings);

            obs.Value = new Quantity(78m, "kg");
            report = _validator.Validate(obs);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            obs.Value = new Quantity(183m, "cm");
            report = _validator.Validate(obs);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            obs.Value = new Quantity(300m, "in");
            report = _validator.Validate(obs);
            Assert.False(report.Success);
            Assert.Equal(0, report.Warnings);
        }


        [Fact]
        public void ValidateContained()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");

            var careplan = (new FhirXmlParser()).Parse<CarePlan>(careplanXml);
            Assert.NotNull(careplan);
            var careplanSd = _source.FindStructureDefinitionForCoreType(FHIRDefinedType.CarePlan);

            var report = _validator.Validate(careplan, careplanSd);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);            // 3x invariant
        }


        [Fact]
        public void MeasureDeepCopyPerformance()
        {
            var questionnaireXml = File.ReadAllText("TestData\\validation\\questionnaire-sdc-profile-example-cap.xml");

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.NotNull(questionnaire);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10000; i++)
            {
                var x = (Questionnaire)questionnaire.DeepCopy();
            }
            sw.Stop();

            Debug.WriteLine(sw.ElapsedMilliseconds / 10000.0);
        }

        [Fact]
        public void TriggerFpValidationError()
        {
            // pat-1: SHALL at least contain a contact's details or a reference to an organization (xpath: f:name or f:telecom or f:address or f:organization)
            var p = new Patient();

            p.Active = true;

            var report = _validator.Validate(p);
            Assert.True(report.Success);

            p.Contact.Add(new Patient.ContactComponent { Gender = AdministrativeGender.Male });

            report = _validator.Validate(p);
            Assert.False(report.Success);

            _validator.Settings.SkipConstraintValidation = true;
            report = _validator.Validate(p);
            Assert.True(report.Success);

            _validator.Settings.SkipConstraintValidation = false;

            p.Contact.First().Address = new Address() { City = "Amsterdam" };

            report = _validator.Validate(p);
            Assert.True(report.Success);
        }


        [Fact]
        public void ValidateBundle()
        {
            var bundleXml = File.ReadAllText("TestData\\validation\\bundle-contained-references.xml");

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.NotNull(bundle);

            var ctx = new ValidationSettings() { ResourceResolver = _source, GenerateSnapshot = true, ResolveExteralReferences = true, Trace = false };
            bool hitResolution = false;

            _validator = new Validator(ctx);
            _validator.OnExternalResolutionNeeded += (s, a) => hitResolution = true;

            var report = _validator.Validate(bundle);
            Assert.True(report.Success);
            Assert.Equal(1, report.Warnings);            // 1 unresolvable reference
            Assert.True(hitResolution);

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithContainedEntries");
            Assert.False(report.Success);
            Assert.Equal(1, report.Warnings);            // 1 unresolvable reference
            Assert.Equal(4, report.Errors);            // 4 non-contained references

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithContainedBundledEntries");
            Assert.False(report.Success);
            Assert.Equal(1, report.Warnings);            // 1 unresolvable reference
            Assert.Equal(1, report.Errors);            // 1 external reference

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithBundledEntries");
            Assert.False(report.Success);
            Assert.Equal(1, report.Warnings);            // 1 unresolvable reference
            Assert.Equal(2, report.Errors);            // 1 external reference, 1 contained reference

            report = _validator.Validate(bundle, "http://validationtest.org/fhir/StructureDefinition/BundleWithReferencedEntries");
            Assert.False(report.Success);
            Assert.Equal(1, report.Warnings);            // 1 unresolvable reference
            Assert.Equal(4, report.Errors);            // 3 bundled reference, 1 contained reference
        }

        [Fact]
        public void RunXsdValidation()
        {
            var careplanXml = File.ReadAllText("TestData\\validation\\careplan-example-integrated.xml");
            var cpDoc = XDocument.Parse(careplanXml, LoadOptions.SetLineInfo);

            var report = _validator.Validate(cpDoc.CreateReader());
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);            // 3x missing invariant

            // Damage the document by removing the mandated 'status' element
            cpDoc.Element(XName.Get("CarePlan", "http://hl7.org/fhir")).Elements(XName.Get("status", "http://hl7.org/fhir")).Remove();

            report = _validator.Validate(cpDoc.CreateReader());
            Assert.False(report.Success);
            Assert.True(report.ToString().Contains(".NET Xsd validation"));
        }

        [Fact]
        public void TestBindingValidation()
        {
            var p = new Patient();

            p.MaritalStatus = new CodeableConcept("http://hl7.org/fhir/v3/MaritalStatus", "S");

            var report = _validator.Validate(p);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            p.MaritalStatus.Coding[0].Code = "XX";

            report = _validator.Validate(p);
            Assert.False(report.Success);
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
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
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            p.Remove("s");
            p.Add("s", new FhirString("not-a-member"));
            report = _validator.Validate(p, profile);
            Assert.False(report.Success);
            Assert.True(report.ToString().Contains("not-a-member"));
            Assert.Equal(0, report.Warnings);
        }

        private void DebugDumpOutputXml(Base fragment)
        {
#if DUMP_OUTPUT
            // Commented out to not fill up the CI builds output log 
            var doc = System.Xml.Linq.XDocument.Parse(new Serialization.FhirXmlSerializer().SerializeToString(fragment));
            output.WriteLine(doc.ToString(System.Xml.Linq.SaveOptions.None));
#endif
        }

        [Fact]
        public void ValidateExtensionExamples()
        {
            var levinXml = File.ReadAllText(@"TestData\validation\Levin.patient.xml");
            var levin = (new FhirXmlParser()).Parse<Patient>(levinXml);
            DebugDumpOutputXml(levin);
            Assert.NotNull(levin);

            var report = _validator.Validate(levin);

            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            // Now, rename the mandatory NCT sub-extension
            levin.Extension[1].Extension[0].Url = "NCTX";
            report = _validator.Validate(levin);
            Assert.False(report.Success);
            Assert.True(report.ToString().Contains("Instance count for 'Extension.extension:NCT' is 0"));

            levin.Extension[1].Extension[0].Url = "NCT";
            levin.Extension[1].Extension[1].Value = new FhirString("wrong!");
            report = _validator.Validate(levin);
            DebugDumpOutputXml(report);
            Assert.False(report.Success);
            Assert.True(report.ToString().Contains("The declared type of the element (Period) is incompatible with that of the instance ('string')"));
        }

        [Fact]
        public void ValidateBundleExample()
        {
            var bundle = _source.ResolveByUri("http://example.org/examples/Bundle/MainBundle");
            Assert.NotNull(bundle);

            var report = _validator.Validate(bundle);

            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);   // 2 warnings about valueset too complex
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

        [Fact]
        public void HandlesParentElementOfCoreAbstractType()
        {
            var sd = "http://validationtest.org/fhir/StructureDefinition/BundleWithConstrainedContained";
            Bundle b = new Bundle();

            b.Type = Bundle.BundleType.Message;
            b.Entry.Add(new Bundle.EntryComponent
            {
                FullUrl = "http://somewhere.org/",
                Resource = new MessageHeader
                {
                    Timestamp = DateTimeOffset.Now,
                    Meta = new Meta { LastUpdated = DateTimeOffset.Now }
                }
            });

            var report = _validator.Validate(b, sd);
            Assert.Equal(2, report.Errors);
            Assert.Equal(0, report.Warnings);
            Assert.DoesNotContain("Encountered unknown child elements 'timestamp'", report.ToString());
        }

        // [WMR 20161220] Example by Christiaan Knaap
        // Causes stack overflow exception in validator when processing the related Organization profile
        // TypeRefValidationExtensions.ValidateTypeReferences needs to detect and handle recursion
        // Example: Organization.partOf => Organization
        [Fact(Skip = "Don't handle recursion yet")]
        public void TestPatientWithOrganization()
        {
            // DirectorySource (and ResourceStreamScanner) does not support json...
            // var source = new DirectorySource(@"TestData\validation");
            // var res = source.ResolveByUri("Patient/pat1"); // cf. "Patient/Levin"

            var jsonPatient = File.ReadAllText(@"TestData\validation\patient-ck.json");
            var parser = new FhirJsonParser();
            var patient = parser.Parse<Patient>(jsonPatient);
            Assert.NotNull(patient);

            var jsonOrganization = File.ReadAllText(@"TestData\validation\organization-ck.json");
            var organization = parser.Parse<Organization>(jsonOrganization);
            Assert.NotNull(organization);

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
            Assert.True(report.Success);

            // Assert.Equal(4, report.Warnings);

            // To check for ele-1 constraints on expanded Patient snapshot:
            // source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient).Snapshot.Element.Select(e=>e.Path + " : " + e.Constraint.FirstOrDefault()?.Key ?? "").ToArray()
            var patientStructDef = source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient);
            Assert.NotNull(patientStructDef);
            Assert.True(patientStructDef.HasSnapshot);
            assertElementConstraints(patientStructDef.Snapshot.Element);
        }

        /// <summary>
        /// Test for issue 423  (https://github.com/ewoutkramer/fhir-net-api/issues/423)
        /// </summary>
        [Fact]
        public void ValidateInternalReferenceWithinContainedResources()
        {
            var obsOverview = File.ReadAllText(@"TestData\validation\observation-list.xml");
            var parser = new FhirXmlParser();

            var obsList = parser.Parse<List>(obsOverview);
            Assert.NotNull(obsList);

            var result = _validator.Validate(obsList);
            Assert.True(result.Success);
            Assert.Equal(0, result.Errors);
        }

        /// <summary>
        /// Test for issue 556 (https://github.com/ewoutkramer/fhir-net-api/issues/556) 
        /// </summary>
        [Fact]
        public async Task RunValueSetExpanderMultiThreaded()
        {
            var nrOfParrallelTasks = 50;
            var results = new ConcurrentBag<OperationOutcome>();
            var buffer = new BufferBlock<XDocument>();
            var processor = new ActionBlock<XDocument>(d =>
                {
                    var outcome = _validator.Validate(d.CreateReader());
                    results.Add(outcome);
                }
                ,
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 100
                });
            buffer.LinkTo(processor, new DataflowLinkOptions { PropagateCompletion = true });

            var careplanXml = File.ReadAllText(@"TestData\validation\careplan-example-integrated.xml");
            var cpDoc = XDocument.Parse(careplanXml, LoadOptions.SetLineInfo);

            for (int i = 0; i < nrOfParrallelTasks; i++)
            {
                buffer.Post(cpDoc);
            }
            buffer.Complete();
            await processor.Completion;

            int successes = results.Count(r => r.Success);

            Assert.Equal(nrOfParrallelTasks, successes);
        }

        // Verify aggregated element constraints
        static void assertElementConstraints(List<ElementDefinition> patientElems)
        {
            foreach (var elem in patientElems)
            {
                if (elem.IsRootElement())
                {
                    // DomainResource constraints dom-1 ... dom-4 are defined in reversed order (specification.zip/profile-resources.xml)
                    // Assert.Equal("dom-4", elem.Constraint.FirstOrDefault()?.Key);
                    var constraintKeys = elem.Constraint.Select(c => c.Key).ToList();
                    Assert.True(constraintKeys.Contains("dom-1"));
                    Assert.True(constraintKeys.Contains("dom-2"));
                    Assert.True(constraintKeys.Contains("dom-3"));
                    Assert.True(constraintKeys.Contains("dom-4"));
                }
                else if (!elem.Path.EndsWith(".contained"))
                {
                    // ele-1 should always be the first constraint
                    Assert.Equal("ele-1", elem.Constraint.FirstOrDefault()?.Key);
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
                if (result is StructureDefinition sd && sd.HasSnapshot)
                {
                    sd.Snapshot = null;
                }
                return result;
            }
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


}

