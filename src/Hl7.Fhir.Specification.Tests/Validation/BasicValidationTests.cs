using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Validation;
using Hl7.FhirPath;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using Xunit;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [Trait("Category", "Validation")]
    public class BasicValidationTests : IClassFixture<ValidationFixture>
    {
        private readonly IResourceResolver _source;
        private readonly IAsyncResourceResolver _asyncSource;

        private Validator _validator;
        private readonly Xunit.Abstractions.ITestOutputHelper output;

        public BasicValidationTests(ValidationFixture fixture, Xunit.Abstractions.ITestOutputHelper output)
        {
            _source = fixture.Resolver;
            _asyncSource = fixture.AsyncResolver;
            _validator = fixture.Validator;
            this.output = output;
        }

        //[TestInitialize]
        //public async T.Task SetupSource()
        //{
        //    // Ensure the FHIR extensions are registered
        //    FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

        //    _source = new CachedResolver(
        //        new MultiResolver(
        //            new BundleExampleResolver(Path.Combine("TestData", "validation")),
        //            new DirectorySource(Path.Combine("TestData", "validation")),
        //            new TestProfileArtifactSource(),
        //            new ZipSource("specification.zip")));

        //    var ctx = new ValidationSettings()
        //    {
        //        ResourceResolver = _source,
        //        GenerateSnapshot = true,
        //        EnableXsdValidation = true,
        //        Trace = false,
        //        ResolveExternalReferences = true
        //    };

        //    _validator = new Validator(ctx);
        //}


        [Fact]
        public async T.Task TestEmptyElement()
        {
            var boolSd = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Boolean);
            var data = SourceNode.Node("active").ToTypedElement(new PocoStructureDefinitionSummaryProvider(), "boolean");

            var result = _validator.Validate(data, boolSd);
            Assert.False(result.Success);
            Assert.Contains("must not be empty", result.ToString());
        }


        [Fact]
        public void NameMatching()
        {
            var data = SourceNode.Valued("active", "true")
                .ToTypedElement(new PocoStructureDefinitionSummaryProvider(), "boolean");

            Assert.True(ChildNameMatcher.NameMatches("active", data));
            Assert.True(ChildNameMatcher.NameMatches("activeBoolean", data));
            Assert.False(ChildNameMatcher.NameMatches("activeDateTime", data));
            Assert.True(ChildNameMatcher.NameMatches("active[x]", data));
            Assert.False(ChildNameMatcher.NameMatches("activate", data));
        }

        [Fact]
        public async T.Task PrimitiveChildMatching()
        {
            var boolean = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Boolean);
            var boolDefNav = ElementDefinitionNavigator.ForSnapshot(boolean);
            boolDefNav.MoveToFirstChild();

            var data = SourceNode.Valued("active", "true",
                    SourceNode.Node("extension",
                        SourceNode.Valued("value", "4")),
                    SourceNode.Node("nonExistant")
                        ).ToTypedElement(new PocoStructureDefinitionSummaryProvider(), type: "boolean", settings: new TypedElementSettings { ErrorMode = TypedElementSettings.TypeErrorMode.Passthrough });

            var matches = ChildNameMatcher.Match(boolDefNav, new ScopedNode(data));
            Assert.Single(matches.UnmatchedInstanceElements);
            Assert.Equal(3, matches.Matches.Count());        // id, extension, value
            Assert.Empty(matches.Matches[0].InstanceElements); // id
            Assert.Single(matches.Matches[1].InstanceElements); // extension
            Assert.Single(matches.Matches[2].InstanceElements); // value

            Assert.Equal("extension", matches.Matches[1].InstanceElements.First().Name);
            Assert.Equal("extension", matches.Matches[1].Definition.PathName);
            Assert.Equal("active", matches.Matches[2].InstanceElements.First().Name);
            Assert.Equal("value", matches.Matches[2].Definition.PathName);
        }


        [Fact]
        public async T.Task ValidatePrimitiveValue()
        {
            var def = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Oid);

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

        /// <summary>
        /// This unit test proves issue 552: https://github.com/FirelyTeam/firely-net-sdk/issues/552
        /// </summary>
        [Fact]
        public async T.Task ValidateOidType()
        {
            var def = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Oid);

            var instance = new Oid("urn:oid:213.2.840.113674.514.212.200");
            var report = _validator.Validate(instance, def);
            Assert.False(report.Success);
            Assert.Equal(1, report.Errors);
            Assert.Equal(0, report.Warnings);

            instance = new Oid("urn:oid:2.2.840.113674.514.212.200");
            report = _validator.Validate(instance, def);
            Assert.True(report.Success);
            Assert.Equal(0, report.Errors);
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
        public async T.Task ValidateCardinality()
        {
            var boolSd = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Boolean);
            var data = SourceNode.Valued("active", "true",
                        SourceNode.Valued("id", "myId1"),
                        SourceNode.Valued("id", "myId2"),
                        SourceNode.Node("extension",
                            SourceNode.Valued("valueInteger", "4")))
                            .ToTypedElement(new PocoStructureDefinitionSummaryProvider(), "boolean");

            var report = _validator.Validate(data, boolSd);
            output.WriteLine(report.ToString());
            Assert.Equal(0, report.Fatals);
            Assert.Equal(2, report.Errors); // boolean.id [0..1], extension.url [1..1]
            Assert.Equal(0, report.Warnings);
        }


        //We've had issues where both boolean.extension and extension throw the same error of an invariant. This checks if all errors are reported just once
        [Fact]
        public async T.Task TestDuplicateValidationMessages()
        {
            var boolSd = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Boolean);
            var data = SourceNode.Valued("active", "true",
                          SourceNode.Node("extension", SourceNode.Valued("url", "http://hl7.org/fhir/StructureDefinition/iso21090-nullFlavor")))
                       .ToTypedElement(new PocoStructureDefinitionSummaryProvider(), "boolean");

            var report = _validator.Validate(data, boolSd);
            output.WriteLine(report.ToString());
            Assert.Equal(0, report.Fatals);
            Assert.Equal(1, report.Errors); // ext-1
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
        public void TestDuplicateOperationOutcomeIssues()
        {
            var duplicateErrors = new OperationOutcome
            {
                Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent
                    {
                        Location = new string[]{"active.extension"},
                        Severity = OperationOutcome.IssueSeverity.Error,
                        Details = new CodeableConcept
                        {
                            Text = "ext-1: value or extension"
                        }
                    },
                    new OperationOutcome.IssueComponent
                    {
                        Location = new string[]{"active.extension"},
                        Severity = OperationOutcome.IssueSeverity.Error,
                        Details = new CodeableConcept
                        {
                            Text = "ext-1: value or extension"
                        }
                    }
                }
            };

            var sameErrorOnDifferentElement = new OperationOutcome
            {
                Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent
                    {
                        Location = new string[]{"active.value"},
                        Severity = OperationOutcome.IssueSeverity.Error,
                        Details = new CodeableConcept
                        {
                            Text = "ele-1: value or child"
                        }
                    },
                    new OperationOutcome.IssueComponent
                    {
                        Location = new string[]{"active.extension"},
                        Severity = OperationOutcome.IssueSeverity.Error,
                        Details = new CodeableConcept
                        {
                            Text = "ele-1: value or child"
                        }
                    }
                }
            };

            duplicateErrors = duplicateErrors.RemoveDuplicateMessages();
            sameErrorOnDifferentElement = sameErrorOnDifferentElement.RemoveDuplicateMessages();

            Assert.Single(duplicateErrors.Issue);
            Assert.Equal(2, sameErrorOnDifferentElement.Issue.Count);
        }

        [Fact]
        public async T.Task ValidateChoiceElement()
        {
            var extensionSd = (StructureDefinition)(await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Extension)).DeepCopy();

            var extensionInstance = new Extension("http://some.org/testExtension", new Oid("urn:oid:1.2.3.4.5"));

            var report = _validator.Validate(extensionInstance, extensionSd);

            Assert.Equal(0, report.Errors);
            Assert.Equal(0, report.Warnings);

            // Now remove the choice available for OID
            var extValueDef = extensionSd.Snapshot.Element.Single(e => e.Path == "Extension.value[x]");
            extValueDef.Type.RemoveAll(t => ModelInfo.FhirTypeNameToFhirType(t.Code) == FHIRAllTypes.Oid);

            report = _validator.Validate(extensionInstance, extensionSd);

            Assert.Equal(1, report.Errors);
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
        public async T.Task AutoGeneratesDifferential()
        {
            var identifierBsn = (StructureDefinition)(await _asyncSource.FindStructureDefinitionAsync("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN")).DeepCopy();
            Assert.NotNull(identifierBsn);
            identifierBsn.Snapshot = null;

            var instance = new Identifier("http://clearly.incorrect.nl/definition", "1234");

            var settingsNoSnapshot = new ValidationSettings { ResourceResolver = _source, GenerateSnapshot = false };
            var validator = new Validator(settingsNoSnapshot);

            var report = validator.Validate(instance, identifierBsn);
            Assert.Contains("does not include a snapshot", report.ToString());

            var settingsSnapshot = new ValidationSettings(settingsNoSnapshot) { GenerateSnapshot = true };
            validator = new Validator(settingsSnapshot);
            report = validator.Validate(instance, identifierBsn);
            Assert.DoesNotContain("does not include a snapshot", report.ToString());

            bool snapshotNeedCalled = false;

            // I disabled cloning of SDs in the validator, so the last call to Validate() will have added a snapshot
            // to our local identifierBSN
            identifierBsn.Snapshot = null;

            validator.OnSnapshotNeeded += (object s, OnSnapshotNeededEventArgs a) => { snapshotNeedCalled = true;  /* change nothing, warning should return */ };

            report = validator.Validate(instance, identifierBsn);
            Assert.True(snapshotNeedCalled);
            Assert.Contains("does not include a snapshot", report.ToString());
        }


        [Fact]
        public async T.Task ValidatesFixedValue()
        {
            var patientSd = (StructureDefinition)(await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient)).DeepCopy();

            var instance1 = new CodeableConcept("http://hl7.org/fhir/marital-status", "U")
            {
                Text = "This is fixed too"
            };

            var maritalStatusElement = patientSd.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Fixed = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient
            {
                MaritalStatus = instance1
            };

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
        public async T.Task ValidatesPatternValue()
        {
            // [WMR 20170727] Fixed
            // Do NOT modify common core Patient definition, as this would affect all subsequent tests.
            // Instead, clone the core def and modify the clone
            var patientSd = (StructureDefinition)(await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient)).DeepCopy();

            var instance1 = new CodeableConcept("http://hl7.org/fhir/marital-status", "U");

            var maritalStatusElement = patientSd.Snapshot.Element.Single(e => e.Path == "Patient.maritalStatus");
            maritalStatusElement.Pattern = (CodeableConcept)instance1.DeepCopy();

            var patient = new Patient
            {
                MaritalStatus = instance1
            };

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

            Patient p = new Patient
            {
                Active = true
            };

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
            var validator = new Validator(new ValidationSettings { ResourceResolver = _source, ResolveExternalReferences = true });

            Patient p = new Patient
            {
                Active = true,
                ManagingOrganization = new ResourceReference("http://reference.cannot.be.found.nl/fhir/Patient/1")
            };

            var result = validator.Validate(p);
            Assert.True(result.Success);
            Assert.Equal(1, result.Warnings);
            Assert.Contains("Cannot resolve reference http://reference.cannot.be.found.nl/fhir/Patient/1", result.Issue[0].ToString());

            validator.Settings.ResolveExternalReferences = false;

            result = validator.Validate(p);
            Assert.True(result.Success);
            Assert.Equal(0, result.Warnings);
        }

        [Fact]
        public void ValidateOverNameRef()
        {
            var questionnaireXml = File.ReadAllText(Path.Combine("TestData", "validation", "questionnaire-with-incorrect-fixed-type.xml"));

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.NotNull(questionnaire);

            // the questionnaire instance references the profile to be validated:
            //      http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType
            var report = _validator.Validate(questionnaire, "http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType");
            Assert.False(report.Success);
            Assert.Equal(2, report.Errors);
            Assert.Equal(0, report.Warnings);           // 20 warnings about valueset too complex
        }

        [Fact]
        public void ValidateInstant()
        {
            var docRef = SourceNode.Resource("DocumentReference", "DocumentReference",
                SourceNode.Valued("id", "example"),
                SourceNode.Valued("status", "current"),
                SourceNode.Valued("type", null,
                    SourceNode.Valued("coding", null,
                        SourceNode.Valued("system", "http://loinc.org"),
                        SourceNode.Valued("code", "34108-1"),
                        SourceNode.Valued("display", "Outpatient Note"))),
                SourceNode.Valued("indexed", "2005-12-24T09:43:41"));

            var report = _validator.Validate(docRef.ToTypedElement(new PocoStructureDefinitionSummaryProvider()));
            Assert.False(report.Success);
            Assert.Equal(2, report.Errors);
            Assert.Equal(0, report.Warnings);
            Assert.Contains("does not match regex", report.Issue[0].Details.Text);
        }


        [Fact]
        public void ValidateFhirDateFormat()
        {
            Patient p = new Patient
            {
                BirthDate = "1974-12-25+03:00"
            };

            var report = _validator.Validate(p);
            Assert.Equal(1, report.Errors);
            Assert.Contains("Value '1974-12-25+03:00' does not match regex", report.Issue[0].Details.Text);
            Assert.Equal(0, report.Warnings);
        }

        [Fact]
        public void ValidateChoiceWithConstraints()
        {
            var obs = new Observation()
            {
                Status = ObservationStatus.Final,
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
        public async T.Task ValidateContained()
        {
            var careplanXml = File.ReadAllText(Path.Combine("TestData", "validation", "careplan-example-integrated.xml"));

            var careplan = (new FhirXmlParser()).Parse<CarePlan>(careplanXml);
            Assert.NotNull(careplan);
            var careplanSd = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.CarePlan);
            var report = _validator.Validate(careplan, careplanSd);
            //output.WriteLine(report.ToString());
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);            // 3x invariant

        }


        [Fact]
        public void MeasureDeepCopyPerformance()
        {
            var questionnaireXml = File.ReadAllText(Path.Combine("TestData", "validation", "questionnaire-sdc-profile-example-cap.xml"));

            var questionnaire = (new FhirXmlParser()).Parse<Questionnaire>(questionnaireXml);
            Assert.NotNull(questionnaire);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10000; i++)
            {
                _ = (Questionnaire)questionnaire.DeepCopy();
            }
            sw.Stop();

            Debug.WriteLine(sw.ElapsedMilliseconds / 10000.0);
        }

        [Fact]
        public void TriggerFpValidationError()
        {
            // pat-1: SHALL at least contain a contact's details or a reference to an organization (xpath: f:name or f:telecom or f:address or f:organization)
            var p = new Patient
            {
                Active = true
            };

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
        public void ValidateCarePlan()
        {
            var eoc = new EpisodeOfCare
            {
                Identifier = new List<Identifier>() { new Identifier { System = "EpisodeOfCare/example", Value = "example" } },
                Status = EpisodeOfCare.EpisodeOfCareStatus.Active,
                Patient = new ResourceReference
                {
                    Reference = "Patient/23"
                }
            };

            var patient = new Patient
            {
                Identifier = new List<Identifier>() { new Identifier { System = "Patient/23", Value = "23" } }
            };

            var cp = new CarePlan
            {
                Status = CarePlan.CarePlanStatus.Active,
                Intent = CarePlan.CarePlanIntent.Plan,
                Subject = new ResourceReference
                {
                    Reference = "Patient/23"
                },
                Context = new ResourceReference
                {
                    Reference = "EpisodeOfCare/example"
                }
            };

            var source =
                    new MultiResolver(
                        new DirectorySource(@"TestData\validation"),
                        new ZipSource("specification.zip"));

            var ctx = new ValidationSettings()
            {
                ResourceResolver = source,
                GenerateSnapshot = false,
                EnableXsdValidation = false,
                Trace = false,
                ResolveExternalReferences = true
            };

            var validator = new Validator(ctx);
            validator.OnExternalResolutionNeeded += onGetExampleResource;
            var report = validator.Validate(cp);

            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);
            Assert.Equal(0, report.Errors);

            void onGetExampleResource(object sender, OnResolveResourceReferenceEventArgs e)
            {
                if (e.Reference.Contains("EpisodeOfCare"))
                    e.Result = eoc.ToTypedElement();
                else
                    e.Result = patient.ToTypedElement();
            };
        }

        [Fact]
        public void ValidateBundle()
        {
            var bundleXml = File.ReadAllText(Path.Combine("TestData", "validation", "bundle-contained-references.xml"));

            var bundle = (new FhirXmlParser()).Parse<Bundle>(bundleXml);
            Assert.NotNull(bundle);

            var ctx = new ValidationSettings() { ResourceResolver = _source, GenerateSnapshot = true, ResolveExternalReferences = true, Trace = false };
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

        /// <summary>
        /// The sdf-8 constraint on element StructureDefinition.Snapshot in the differential is not correct encoded. We manually changed 
        /// this in Hl7.Fhir.Specification\data\profiles-resources.xml. 
        /// See also issue https://github.com/FirelyTeam/firely-net-sdk/issues/1302
        /// </summary>
        [Fact]
        public async T.Task CheckSdf8Expression()
        {
            var structDef = await _asyncSource.FindStructureDefinitionAsync("http://hl7.org/fhir/StructureDefinition/StructureDefinition");
            var sdf8 = structDef.Differential.Element.FirstOrDefault(e => e.ElementId is "StructureDefinition.snapshot")?.Constraint.FirstOrDefault(c => c.Key is "sdf-8");

            var sdf8Expression = @"(%resource.kind = 'logical' or element.first().path = %resource.type) and element.tail().all(path.startsWith(%resource.snapshot.element.first().path&'.'))";

            Assert.Equal(sdf8Expression, sdf8.Expression);
        }

        [Fact]
        public void RunXsdValidation()
        {
            var careplanXml = File.ReadAllText(Path.Combine("TestData", "validation", "careplan-example-integrated.xml"));
            var cpDoc = XDocument.Parse(careplanXml, LoadOptions.SetLineInfo);

            var report = _validator.Validate(cpDoc.CreateReader());
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);            // 3x missing invariant

            // Damage the document by removing the mandated 'status' element
            cpDoc.Element(XName.Get("CarePlan", "http://hl7.org/fhir")).Elements(XName.Get("status", "http://hl7.org/fhir")).Remove();

            report = _validator.Validate(cpDoc.CreateReader());
            Assert.False(report.Success);
            Assert.Contains(".NET Xsd validation", report.ToString());
        }

        [Fact]
        public void TestBindingValidation()
        {
            var p = new Patient
            {
                MaritalStatus = new CodeableConcept("http://hl7.org/fhir/v3/MaritalStatus", "S")
            };

            var report = _validator.Validate(p);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);

            p.MaritalStatus.Coding[0].Code = "XX";

            report = _validator.Validate(p);
            Assert.True(report.Success);
            Assert.Equal(0, report.Warnings);
            //Assert.True(report.ToString().Contains("not valid for non-required binding"));
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
            Assert.Contains("not-a-member", report.ToString());
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
            var levinXml = File.ReadAllText(Path.Combine("TestData", "validation", "Levin.patient.xml"));
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
            Assert.Contains("Instance count for 'Extension.extension:NCT' is 0", report.ToString());

            levin.Extension[1].Extension[0].Url = "NCT";
            levin.Extension[1].Extension[1].Value = new FhirString("wrong!");
            report = _validator.Validate(levin);
            DebugDumpOutputXml(report);
            Assert.False(report.Success);
            Assert.Contains("The declared type of the element (Period) is incompatible with that of the instance ('string')", report.ToString());
        }

        [Fact]
        public void ValidateBundleExample()
        {
            var bundle = _source.ResolveByUri("http://example.org/examples/Bundle/MainBundle");
            Assert.NotNull(bundle);

            var report = _validator.Validate(bundle);

            Assert.True(report.Success, report.ToString());
            Assert.Equal(0, report.Warnings);   // 2 warnings about valueset too complex
        }


        internal class BundleExampleResolver : IResourceResolver
        {
            private readonly string _path;

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
                var filename = $"{reference.Id}.{reference.ResourceType.ToLower()}.xml";
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
            Bundle b = new Bundle
            {
                Type = Bundle.BundleType.Message
            };
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


        [Fact]
        public async T.Task TriggerEscapingValidationError()
        {
            // This unit-test is here to trigger because of an escaping mistake in the FHIR spec 3.0.1.
            // The cause is the escaped \\_ character in eld-16. I have manually corrected profiles-types.xml
            // in the /data directory for this invariant, so this unit-test will normally pass.
            // If it does not, the profiles-types.xml will have been updated/overwritten with a version that
            // still contains this mistake.
            // [MV 20191217] Still with the FHIR spec 3.0.2 this problem arises. I have manually corrected profiles-types.xml
            // again.
            var sd = (StructureDefinition)(await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient)).DeepCopy();
            sd.Snapshot.Element[0].SliceName = "dummy";

            var result = _validator.Validate(sd);
            Assert.True(result.Success);
        }

        // [WMR 20161220] Example by Christiaan Knaap
        // Causes stack overflow exception in validator when processing the related Organization profile
        // TypeRefValidationExtensions.ValidateTypeReferences needs to detect and handle recursion
        // Example: Organization.partOf => Organization
        [Fact(Skip = "Don't handle recursion yet")]
        public async T.Task TestPatientWithOrganization()
        {
            // DirectorySource (and ResourceStreamScanner) does not support json...
            // var source = new DirectorySource(Path.Combine("TestData", "validation"));
            // var res = source.ResolveByUri("Patient/pat1"); // cf. "Patient/Levin"

            var jsonPatient = File.ReadAllText(Path.Combine("TestData", "validation", "patient-ck.json"));
            var parser = new FhirJsonParser();
            var patient = parser.Parse<Patient>(jsonPatient);
            Assert.NotNull(patient);

            var jsonOrganization = File.ReadAllText(Path.Combine("TestData", "validation", "organization-ck.json"));
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
                        // new BundleExampleResolver(Path.Combine("TestData", "validation")),
                        // new DirectorySource(Path.Combine("TestData", "validation")),
                        // new TestProfileArtifactSource(),
                        memResolver,
                        new ZipSource("specification.zip"))));

            var ctx = new ValidationSettings()
            {
                ResourceResolver = source,
                GenerateSnapshot = true,
                EnableXsdValidation = true,
                Trace = false,
                ResolveExternalReferences = true
            };

            var validator = new Validator(ctx);

            var report = validator.Validate(patient);
            Assert.True(report.Success);

            // Assert.Equal(4, report.Warnings);

            // To check for ele-1 constraints on expanded Patient snapshot:
            // source.FindStructureDefinitionForCoreType(FHIRDefinedType.Patient).Snapshot.Element.Select(e=>e.Path + " : " + e.Constraint.FirstOrDefault()?.Key ?? "").ToArray()
            var patientStructDef = await source.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Patient);
            Assert.NotNull(patientStructDef);
            Assert.True(patientStructDef.HasSnapshot);
            assertElementConstraints(patientStructDef.Snapshot.Element);
        }

        /// <summary>
        /// Test for issue 423  (https://github.com/FirelyTeam/firely-net-sdk/issues/423)
        /// </summary>
        [Fact]
        public void ValidateInternalReferenceWithinContainedResources()
        {
            var obsOverview = File.ReadAllText(Path.Combine("TestData", "validation", "observation-list.xml"));
            var parser = new FhirXmlParser();

            var obsList = parser.Parse<List>(obsOverview);
            Assert.NotNull(obsList);

            var result = _validator.Validate(obsList);
            Assert.True(result.Success);
            Assert.Equal(0, result.Errors);
        }

        private Validator buildValidator(CachedResolver cr)
        {
            var ctx = new ValidationSettings()
            {
                ResourceResolver = cr,
                GenerateSnapshot = true,
                EnableXsdValidation = true,
                Trace = false,
                ResolveExternalReferences = true
            };

            return new Validator(ctx);
        }

        /// <summary>
        /// Test for issue 556 (https://github.com/FirelyTeam/firely-net-sdk/issues/556) 
        /// </summary>
        [Fact, Trait("Category", "LongRunner")]
        public async System.Threading.Tasks.Task RunValueSetExpanderMultiThreaded()
        {
            var cr = new CachedResolver(
                    new MultiResolver(
                    new BasicValidationTests.BundleExampleResolver(@"TestData\validation"),
                    new DirectorySource(@"TestData\validation"),
                    new TestProfileArtifactSource(),
                    new ZipSource("specification.zip")));

            var nrOfParrallelTasks = 50;
            var results = new ConcurrentBag<OperationOutcome>();
            var buffer = new BufferBlock<XDocument>();
            var processor = new ActionBlock<XDocument>(d =>
                {
                    var v = buildValidator(cr);
                    var outcome = v.Validate(d.CreateReader());
                    results.Add(outcome);
                }
                ,
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 100
                });
            buffer.LinkTo(processor, new DataflowLinkOptions { PropagateCompletion = true });

            var careplanXml = File.ReadAllText(Path.Combine("TestData", "validation", "careplan-example-integrated.xml"));
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

     
        /// <summary>
        /// This test should show that the rng-2 constraint is totally ignored (it's
        /// incorrect in DSTU2 and STU3), but others are not.
        /// </summary>
        [Fact]
        public async T.Task IgnoreRng2FPConstraint()
        {
            var def = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Observation);

            var instance = new Observation
            {
                // this should not trigger rng-2
                Value = new Range()
                {
                    Low = new Quantity() { Value = 5, Code = "kg", System = "ucum.org" },
                    High = new Quantity() { Value = 4, Code = "kg", System = "ucum.org" },
                }
            };

            var report = _validator.Validate(instance, def);
            Assert.False(report.Success);
            Assert.Equal(2, report.Errors);  // Obs.status missing, Obs.code missing
            Assert.Equal(0, report.Warnings);
        }


        /// <summary>
        /// This test proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/1140
        /// </summary>
        [Fact]
        public async T.Task ValidatePrimitiveWithEmptyTypeElement()
        {
            var def = await _asyncSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Code);
            var elem = def.Snapshot.Element.Where(e => e.Path == "code.value").Single();
            var data = elem.ToTypedElement();

            Assert.True(data.IsBoolean("type.select(code&profile&targetProfile).isDistinct()", true));

            var result = _validator.Validate(def);
            Assert.True(result.Success, result.ToString());
        }


        /// <summary>
        /// This test proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/617
        /// </summary>
        [Fact]
        public void ValidateConditionalResourceInBundle()
        {
            TransactionBuilder tb = new TransactionBuilder("http://example.fhir.org");

            var obs = new Observation()
            {
                Status = ObservationStatus.Preliminary,
                Code = new CodeableConcept("system", "P"),
                Subject = new ResourceReference("Patient?identifier=system|12345")
            };

            var patient = new Patient();
            patient.Identifier.Add(new Identifier("system", "12345"));

            tb.Create(patient);
            tb.Create(obs);
            var bundle = tb.ToBundle();
            // fill in the FullUrl to make the DSTU2 validation happy
            bundle.Entry[0].FullUrl = "http://example.fhir.org/Observation";
            bundle.Entry[1].FullUrl = "http://example.fhir.org/Patient";

            var result = _validator.Validate(bundle);

            Assert.True(result.Success);
        }

        // Verify aggregated element constraints
        private static void assertElementConstraints(List<ElementDefinition> patientElems)
        {
            foreach (var elem in patientElems)
            {
                if (elem.IsRootElement())
                {
                    // DomainResource constraints dom-1 ... dom-4 are defined in reversed order (specification.zip/profile-resources.xml)
                    // Assert.Equal("dom-4", elem.Constraint.FirstOrDefault()?.Key);
                    var constraintKeys = elem.Constraint.Select(c => c.Key).ToList();
                    Assert.Contains("dom-1", constraintKeys);
                    Assert.Contains("dom-2", constraintKeys);
                    Assert.Contains("dom-3", constraintKeys);
                    Assert.Contains("dom-4", constraintKeys);
                }
                else if (!elem.Path.EndsWith(".contained"))
                {
                    // ele-1 should always be the first constraint
                    Assert.Equal("ele-1", elem.Constraint.FirstOrDefault()?.Key);
                }
            }
        }

      

        private class ClearSnapshotResolver : IResourceResolver
        {
            private readonly IResourceResolver _resolver;
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

    internal class InMemoryResourceResolver : IResourceResolver
    {
        private readonly ILookup<string, Resource> _resources;

        public InMemoryResourceResolver(IEnumerable<Resource> profiles)
        {
            _resources = profiles.ToLookup(r => getResourceUri(r), r => r as Resource);
        }

        public InMemoryResourceResolver(Resource profile) : this(new Resource[] { profile }) { }

        public Resource ResolveByCanonicalUri(string uri) => null;

        public Resource ResolveByUri(string uri) => _resources[uri].FirstOrDefault();

        // cf. ResourceStreamScanner.StreamResources
        private static string getResourceUri(Resource res) => res.TypeName + "/" + res.Id;
    }


}

