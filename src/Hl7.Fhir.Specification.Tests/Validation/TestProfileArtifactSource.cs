using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class TestProfileArtifactSource : IResourceResolver
    {
        public List<StructureDefinition> TestProfiles = new List<StructureDefinition>
        {
            buildIdentifierWithBSN(),
            buildIdentifierWithDriversLicense(),
            buildDutchPatient(),
            buildQuestionnaireWithFixedType(),
            buildHeightQuantity(),
            buildWeightQuantity(),
            buildWeightHeightObservation(),
            bundleWithSpecificEntries("Contained"),
            patientWithSpecificOrganization(new[] { ElementDefinition.AggregationMode.Contained }, "Contained"),
            bundleWithSpecificEntries("ContainedBundled"),
            patientWithSpecificOrganization(new[] { ElementDefinition.AggregationMode.Contained, ElementDefinition.AggregationMode.Bundled }, "ContainedBundled"),
            bundleWithSpecificEntries("Bundled"),
            patientWithSpecificOrganization(new[] { ElementDefinition.AggregationMode.Bundled }, "Bundled"),
            bundleWithSpecificEntries("Referenced"),
            patientWithSpecificOrganization(new[] { ElementDefinition.AggregationMode.Referenced }, "Referenced"),
            buildParametersWithBoundParams(),
            bundleWithConstrainedContained(),
            buildOrganizationWithRegexConstraintOnName(),
            buildOrganizationWithRegexConstraintOnType(),
            buildValueDescriminatorWithPattern(),
            buildQuantityWithUnlimitedRootCardinality(),
            buildRangeWithLowAsAQuantityWithUnlimitedRootCardinality(),
            buildBloodPressureSlicing(),
            buildTelecomSlicing(),
            buildNestedSlicing(),
            buildPatientWithFixedMaritalStatus(),
            buildPatientWithPatternMaritalStatus(),
            buildExtensionWithLessTypes(),
            buildPatientWithIdentifierSlicing(),
            buildMiPatient()
        };

        private static StructureDefinition buildExtensionWithLessTypes()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ExtensionWithLessTypes", "Extension with less possible types",
                      "Test of Extension without Uid or Oid types", FHIRAllTypes.Extension);

            var cons = result.Differential.Element;
            var valueElement = new ElementDefinition("Extension.value[x]");

            foreach (var item in ModelInfo.OpenTypes)
            {
                var type = ModelInfo.FhirTypeNameToFhirType(ModelInfo.GetFhirTypeNameForType(item));
                if (!type.HasValue || type == FHIRAllTypes.Oid || type == FHIRAllTypes.Uri)
                    continue;
                if (type == FHIRAllTypes.Reference)
                    valueElement.OrReference(new string[] { });
                else
                    valueElement.OrType(type.Value);
            }
            cons.Add(valueElement);
            return result;
        }

        private static StructureDefinition buildPatientWithFixedMaritalStatus()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/patient_fixed_maritalStatus_changed", "Patient Fixed MaritalStatus",
                      "Test of patient with a fixed marital status", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient.maritalStatus").Value(fix: new CodeableConcept("http://hl7.org/fhir/marital-status", "U") { Text = "This is fixed too" }));

            return result;
        }

        private static StructureDefinition buildPatientWithPatternMaritalStatus()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/patient_pattern_maritalStatus_changed", "Patient Fixed MaritalStatus",
                      "Test of patient with a pattern marital status", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient.maritalStatus").Value(pattern: new CodeableConcept("http://hl7.org/fhir/marital-status", "U")));

            return result;
        }

        private static StructureDefinition buildNestedSlicing()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/CompositionNestedSlicing", "Composition sections",
                      "Test of nested slices in Composition sections", FHIRAllTypes.Composition);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Composition").OfType(FHIRAllTypes.Composition));

            var slicingIntro = new ElementDefinition("Composition.section")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed, ordered: true,
                   (ElementDefinition.DiscriminatorType.Value, "code")
               ).Required(3, "3");
            cons.Add(slicingIntro);

            // first slice: reason for visit
            cons.Add(new ElementDefinition("Composition.section")
            {
                ElementId = "Composition.section:reason-for-visit",
                SliceName = "reason-for-visit"
            }.Required());
            cons.Add(new ElementDefinition("Composition.section.code")
            {
                ElementId = "Composition.section.code:reason-for-visit",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "29299-5", "Reason for visit Narrative", null)));

            // second slice: medications
            cons.Add(new ElementDefinition("Composition.section")
            {
                ElementId = "Composition.section:medications",
                SliceName = "medications"
            }.Required());
            cons.Add(new ElementDefinition("Composition.section.code")
            {
                ElementId = "Composition.section.code:medications",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "46057-6", "Medications section", null)));

            // setting up the inner slicing on medication Composition.section.section
            var nestedSlicingIntro = new ElementDefinition("Composition.section.section")
            {
                ElementId = "Composition.section.section:medication"
            }.WithSlicingIntro(ElementDefinition.SlicingRules.Closed, ordered: true,
                   (ElementDefinition.DiscriminatorType.Value, "code")
               ).Required(1, "2");
            cons.Add(nestedSlicingIntro);

            // first inner slice: prescribed medications
            cons.Add(new ElementDefinition("Composition.section.section")
            {
                ElementId = "Composition.section.section:prescibed",
                SliceName = "prescibed"
            }.Required());
            cons.Add(new ElementDefinition("Composition.section.section.code")
            {
                ElementId = "Composition.section.section.code:prescibed",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "66149-6", "Prescribed medications", null)));

            // second inner slice: over the counter medications
            cons.Add(new ElementDefinition("Composition.section.section")
            {
                ElementId = "Composition.section.section:otc",
                SliceName = "otc"
            }.Required(min: 0, max: "1"));
            cons.Add(new ElementDefinition("Composition.section.section.code")
            {
                ElementId = "Composition.section.section.code:otc",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "66150-4", "Over the counter medications", null)));

            // third slice: Vital Signs
            cons.Add(new ElementDefinition("Composition.section")
            {
                ElementId = "Composition.section:vital-signs",
                SliceName = "vital-signs"
            }.Required());
            cons.Add(new ElementDefinition("Composition.section.code")
            {
                ElementId = "Composition.section.code:vital-signs",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "8716-3", "Vital signs", null)));

            return result;
        }

        private static StructureDefinition buildTelecomSlicing()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/PatientTelecomSlicing", "Patient Telecom",
                     "Test of slicing of telecom", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRAllTypes.Patient));

            var slicingIntro = new ElementDefinition("Patient.telecom")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed, ordered: false,
                   (ElementDefinition.DiscriminatorType.Value, "system"),
                   (ElementDefinition.DiscriminatorType.Value, "use")
               ).Required(1, "3");
            cons.Add(slicingIntro);

            // first slice: home phone 
            cons.Add(new ElementDefinition("Patient.telecom")
            {
                ElementId = "Patient.telecom:homePhone",
                SliceName = "homePhone"
            }.Required());
            cons.Add(new ElementDefinition("Patient.telecom.system")
            {
                ElementId = "Patient.telecom.system:homePhone",
                Min = 1
            }.Value(fix: new Code("phone")));
            cons.Add(new ElementDefinition("Patient.telecom.value")
            {
                ElementId = "Patient.telecom.value:homePhone",
                Min = 1
            });
            cons.Add(new ElementDefinition("Patient.telecom.use")
            {
                ElementId = "Patient.telecom.use:homePhone",
                Min = 1
            }.Value(fix: new Code("home")));

            // second slice: work phone
            cons.Add(new ElementDefinition("Patient.telecom")
            {
                ElementId = "Patient.telecom:workPhone",
                SliceName = "workPhone",
                Min = 0,
                Max = "*"
            });
            cons.Add(new ElementDefinition("Patient.telecom.system")
            {
                ElementId = "Patient.telecom.system:workPhone",
                Min = 1
            }.Value(fix: new Code("phone")));
            cons.Add(new ElementDefinition("Patient.telecom.value")
            {
                ElementId = "Patient.telecom.value:workPhone",
                Min = 1
            });
            cons.Add(new ElementDefinition("Patient.telecom.use")
            {
                ElementId = "Patient.telecom.use:workPhone",
                Min = 1
            }.Value(fix: new Code("work")));

            // second slice: email
            cons.Add(new ElementDefinition("Patient.telecom")
            {
                ElementId = "Patient.telecom:email",
                SliceName = "email",
                Min = 0,
                Max = "1"
            });
            cons.Add(new ElementDefinition("Patient.telecom.system")
            {
                ElementId = "Patient.telecom.system:email",
                Min = 1
            }.Value(fix: new Code("email")));
            cons.Add(new ElementDefinition("Patient.telecom.value")
            {
                ElementId = "Patient.telecom.value:email",
                Min = 1
            });
            cons.Add(new ElementDefinition("Patient.telecom.use")
            {
                ElementId = "Patient.telecom.use:email",
            }.Prohibited());

            return result;
        }

        private static StructureDefinition buildBloodPressureSlicing()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/BloodPressureMeasurement", "Blood pressure measurement",
                    "Test an observation with slicing on LOINC code", FHIRAllTypes.Observation);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Observation").OfType(FHIRAllTypes.Observation));

            var slicingIntro = new ElementDefinition("Observation.component")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Open, ordered: false,
               (ElementDefinition.DiscriminatorType.Value, "code")).Required(2, "*");
            cons.Add(slicingIntro);

            // Slice 1 - systolic
            cons.Add(new ElementDefinition("Observation.component")
            {
                ElementId = "Observation.component:systolic",
                SliceName = "systolic"
            }.Required());
            cons.Add(new ElementDefinition("Observation.component.code")
            {
                ElementId = "Observation.component.code:systolic",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "8480-6", display: "Systolic blood pressure", null)));
            cons.Add(new ElementDefinition("Observation.component.value[x]")
            {
                ElementId = "Observation.component.valueQuantity:systolic",
                Min = 1
            }.OrType(FHIRAllTypes.Quantity));

            // Slice 2 - diastolic 
            cons.Add(new ElementDefinition("Observation.component")
            {
                ElementId = "Observation.component:diastolic ",
                SliceName = "diastolic "
            }.Required());
            cons.Add(new ElementDefinition("Observation.component.code")
            {
                ElementId = "Observation.component.code:diastolic ",
                Min = 1
            }.Value(fix: new CodeableConcept("http://loinc.org", "8462-4", display: "Diastolic blood pressure", null)));
            cons.Add(new ElementDefinition("Observation.component.value[x]")
            {
                ElementId = "Observation.component.valueQuantity:diastolic",
                Min = 1
            }.OrType(FHIRAllTypes.Quantity));

            return result;
        }

        private static StructureDefinition buildPatientWithIdentifierSlicing()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/PatientIdentifierSlicing", "PatientIdentifierSlicing",
                       "Test Patient with slicing on Identifier, first slice BSN", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;
            var slicingIntro = new ElementDefinition("Patient.identifier")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed, false,
               (ElementDefinition.DiscriminatorType.Value, "system"));

            cons.Add(slicingIntro);

            cons.Add(new ElementDefinition("Patient.identifier")
            {
                ElementId = "Patient.identifier:BSN",
                SliceName = "BSN"
            });

            cons.Add(new ElementDefinition("Patient.identifier.system")
            {
                ElementId = "Patient.identifier:BSN.system",
            }.Value(fix: new FhirUri("http://example.com/someuri")));

            return result;
        }

        private static StructureDefinition buildMiPatient()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/mi-patient", "mi-Patient",
                      "Test a derived Patient introducing a new slice to the base introduction Slicing",
                      FHIRAllTypes.Patient, "http://validationtest.org/fhir/StructureDefinition/PatientIdentifierSlicing");

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient.identifier")
            {
                ElementId = "Patient.identifier:BSN",
                SliceName = "BSN"
            });

            // adding extra constraint on existing slice in base
            cons.Add(new ElementDefinition("Patient.identifier.system")
            {
                ElementId = "Patient.identifier:BSN.system",
                Definition = new Markdown("BSN naming system"),
                MustSupport = true
            });

            // adding a new slice
            cons.Add(new ElementDefinition("Patient.identifier")
            {
                ElementId = "Patient.identifier:newSlice",
                SliceName = "newSlice"
            });
            cons.Add(new ElementDefinition("Patient.identifier.system")
            {
                ElementId = "Patient.identifier:newSlice.system",
                Definition = new Markdown("Test_1295")
            });

            return result;
        }

        private static StructureDefinition buildOrganizationWithRegexConstraintOnName()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MyOrganization", "My Organization",
                    "Test an organization with Name containing regex", FHIRAllTypes.Organization);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Organization").OfType(FHIRAllTypes.Organization));

            var nameDef = new ElementDefinition("Organization.name");
            nameDef.SetStringExtension("http://hl7.org/fhir/StructureDefinition/regex", "[A-Z].*");
            cons.Add(nameDef);

            return result;
        }


        private static StructureDefinition buildOrganizationWithRegexConstraintOnType()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MyOrganization2", "My Organization",
                    "Test an organization with Name containing regex", FHIRAllTypes.Organization);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Organization").OfType(FHIRAllTypes.Organization));

            var nameDef = new ElementDefinition("Organization.name.value")
                .OfType(FHIRAllTypes.String);
            nameDef.Type.Single().SetStringExtension("http://hl7.org/fhir/StructureDefinition/regex", "[A-Z].*");
            // R4: [Primitive].value elements have no type code
            nameDef.Type.Single().Code = null;

            cons.Add(nameDef);

            return result;
        }
        public Resource ResolveByCanonicalUri(string uri)
        {
            return TestProfiles.SingleOrDefault(p => p.Url == uri);
        }

        public Resource ResolveByUri(string uri)
        {
            return ResolveByCanonicalUri(uri);
        }


        private static StructureDefinition buildDutchPatient()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/DutchPatient", "Dutch Patient",
                    "Test Patient which requires an Identifier with either BSN or drivers license", FHIRAllTypes.Patient);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRAllTypes.Patient));
            cons.Add(new ElementDefinition("Patient.identifier").Required(max: "*")
                        .OfType(FHIRAllTypes.Identifier, new[] { "http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN", "http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL" }));

            return result;
        }

        private static StructureDefinition buildIdentifierWithBSN()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN", "BSN Identifier",
                    "Test Identifier which requires a BSN oid", FHIRAllTypes.Identifier);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Identifier").OfType(FHIRAllTypes.Identifier));
            cons.Add(new ElementDefinition("Identifier.system").Required().Value(fix: new FhirUri("urn:oid:2.16.840.1.113883.2.4.6.3")));
            cons.Add(new ElementDefinition("Identifier.period").Prohibited());
            cons.Add(new ElementDefinition("Identifier.assigner").Prohibited());

            return result;
        }

        private static StructureDefinition buildIdentifierWithDriversLicense()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL", "Drivers license Identifier",
                    "Test Identifier which requires a drivers license oid", FHIRAllTypes.Identifier);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Identifier").OfType(FHIRAllTypes.Identifier));
            cons.Add(new ElementDefinition("Identifier.system").Required().Value(fix: new FhirUri("urn:oid:2.16.840.1.113883.2.4.6.12")));

            return result;
        }


        private static StructureDefinition buildQuestionnaireWithFixedType()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType", "Fixed Questionnaire",
                    "Questionnaire with a fixed question type of 'decimal'", FHIRAllTypes.Questionnaire);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Questionnaire").OfType(FHIRAllTypes.Questionnaire));
            cons.Add(new ElementDefinition("Questionnaire.item.item.type").Value(fix: new Code("decimal")));

            return result;
        }

        private static StructureDefinition buildWeightQuantity()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/WeightQuantity", "Weight Quantity",
                    "Quantity which allows just kilograms", FHIRAllTypes.Quantity);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Quantity").OfType(FHIRAllTypes.Quantity));
            cons.Add(new ElementDefinition("Quantity.unit").Required().Value(fix: new FhirString("kg")));
            cons.Add(new ElementDefinition("Quantity.system").Required().Value(fix: new FhirUri("http://unitsofmeasure.org")));
            cons.Add(new ElementDefinition("Quantity.code").Required().Value(fix: new Code("kg")));

            return result;
        }

        private static StructureDefinition buildHeightQuantity()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/HeightQuantity", "Height Quantity",
                    "Quantity which allows just centimeters", FHIRAllTypes.Quantity);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Quantity").OfType(FHIRAllTypes.Quantity));
            cons.Add(new ElementDefinition("Quantity.unit").Required().Value(fix: new FhirString("cm")));
            cons.Add(new ElementDefinition("Quantity.system").Required().Value(fix: new FhirUri("http://unitsofmeasure.org")));
            cons.Add(new ElementDefinition("Quantity.code").Required().Value(fix: new Code("cm")));

            return result;
        }

        private static StructureDefinition buildWeightHeightObservation()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", "Weight/Height Observation",
                    "Observation with a choice of weight/height or another type of value", FHIRAllTypes.Observation);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Observation").OfType(FHIRAllTypes.Observation));
            cons.Add(new ElementDefinition("Observation.value[x]")
                .OfType(FHIRAllTypes.Quantity, new[] { "http://validationtest.org/fhir/StructureDefinition/WeightQuantity", "http://validationtest.org/fhir/StructureDefinition/HeightQuantity" })
                .OrType(FHIRAllTypes.String));

            return result;
        }


        private static StructureDefinition bundleWithSpecificEntries(string prefix)
        {
            var result = createTestSD($"http://validationtest.org/fhir/StructureDefinition/BundleWith{prefix}Entries", $"Bundle with specific {prefix} test entries",
                    $"Bundle with just Organization or {prefix} Patient entries", FHIRAllTypes.Bundle);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Bundle").OfType(FHIRAllTypes.Bundle));
            cons.Add(new ElementDefinition("Bundle.entry.resource")
                .OfType(FHIRAllTypes.Organization)
                .OrType(FHIRAllTypes.Patient, new[] { $"http://validationtest.org/fhir/StructureDefinition/PatientWith{prefix}Organization" }));

            return result;
        }

        private static StructureDefinition bundleWithConstrainedContained()
        {
            var result = createTestSD($"http://validationtest.org/fhir/StructureDefinition/BundleWithConstrainedContained",
                            $"Bundle with a constraint on the Bundle.entry.resource",
                    $"Bundle with a constraint on the Bundle.entry.resource", FHIRAllTypes.Bundle);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Bundle").OfType(FHIRAllTypes.Bundle));
            cons.Add(new ElementDefinition("Bundle.entry.resource.meta").Required());

            return result;
        }


        private static StructureDefinition patientWithSpecificOrganization(IEnumerable<ElementDefinition.AggregationMode> aggregation, string prefix)
        {
            var result = createTestSD($"http://validationtest.org/fhir/StructureDefinition/PatientWith{prefix}Organization", $"Patient with {prefix} managing organization",
                    $"Patient for which the managingOrganization reference is limited to {prefix} references", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRAllTypes.Patient));
            cons.Add(new ElementDefinition("Patient.managingOrganization")
                .OfReference(new[] { (string)ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Organization) }, aggregation));

            return result;
        }


        private static StructureDefinition buildParametersWithBoundParams()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ParametersWithBoundParams", "Parameters with term binding on Params",
                    "Parameters resource where the parameter.value[x] is bound to a valueset", FHIRAllTypes.Parameters);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Parameters").OfType(FHIRAllTypes.Parameters));
            cons.Add(new ElementDefinition("Parameters.parameter.value[x]")
                    .WithBinding("http://hl7.org/fhir/ValueSet/data-absent-reason", BindingStrength.Required));

            return result;
        }

        private const string QUANTITY_WITH_UNLIMITED_ROOT_CARDINALITY_CANONICAL = "http://validationtest.org/fhir/StructureDefinition/QuantityWithUnlimitedRootCardinality";

        private static StructureDefinition buildQuantityWithUnlimitedRootCardinality()
        {
            var result = createTestSD(QUANTITY_WITH_UNLIMITED_ROOT_CARDINALITY_CANONICAL, "A Quantity with a root cardinality of 0..*",
                    "Parameters resource where the parameter.value[x] is bound to a valueset", FHIRAllTypes.Quantity);
            var cons = result.Differential.Element;

            var quantityRoot = new ElementDefinition("Quantity").OfType(FHIRAllTypes.Quantity);
            quantityRoot.Min = 0;
            quantityRoot.Max = "*";  // this is actually the case in all core classes as well.
            cons.Add(quantityRoot);

            return result;
        }

        private static StructureDefinition buildRangeWithLowAsAQuantityWithUnlimitedRootCardinality()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/RangeWithLowAsAQuantityWithUnlimitedRootCardinality",
                "Range referring to a profiled quantity",
                "Range that refers to a profiled quantity on its Range.low - this profiled Quantity has a 0..* root.",
                   FHIRAllTypes.Range);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Range").OfType(FHIRAllTypes.Range));
            cons.Add(new ElementDefinition("Range.low")
                .OfType(FHIRAllTypes.Quantity, profile: new[] { QUANTITY_WITH_UNLIMITED_ROOT_CARDINALITY_CANONICAL })
                .Required(min: 1, max: null));   // just set min to 1 and leave max out.

            return result;
        }

        private static StructureDefinition buildValueDescriminatorWithPattern()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ValueDiscriminatorWithPattern", "A value discriminator including pattern[x]",
                    "Expresses a discriminator of type value, which expects pattern[x] to be considered part of it too.", FHIRAllTypes.Practitioner);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Practitioner").OfType(FHIRAllTypes.Practitioner));

            var slicingIntro = new ElementDefinition("Practitioner.identifier")
                .WithSlicingIntro(ElementDefinition.SlicingRules.Closed, ordered: false,
                (ElementDefinition.DiscriminatorType.Pattern, "type"));

            cons.Add(slicingIntro);

            // Slice 1 - binds to FHIR's identifier-type (req) PLUS requires a local code translation
            cons.Add(new ElementDefinition("Practitioner.identifier")
            {
                ElementId = "Practitioner.identifier:slice1",
                SliceName = "slice1",
                Max = "1"
            });
            cons.Add(new ElementDefinition("Practitioner.identifier.type")
            {
                ElementId = "Practitioner.identifier:slice1.type",
                Pattern = new CodeableConcept("http://local-codes.nl/identifier-types", "ID-TYPE-1")
            }.WithBinding("http://hl7.org/fhir/ValueSet/identifier-type", BindingStrength.Required));

            // Slice 2 - binds to FHIR's identifier-type (req) PLUS requires another local code translation
            cons.Add(new ElementDefinition("Practitioner.identifier")
            {
                ElementId = "Practitioner.identifier:slice2",
                SliceName = "slice2",
                Max = "*"
            });
            cons.Add(new ElementDefinition("Practitioner.identifier.type")
            {
                ElementId = "Practitioner.identifier:slice2.type",
                Pattern = new CodeableConcept("http://local-codes.nl/identifier-types", "ID-TYPE-2")
            }.WithBinding("http://hl7.org/fhir/ValueSet/identifier-type", BindingStrength.Required));

            // Slice 3 - binds to FHIR's identifier-type (req), no other requirements
            cons.Add(new ElementDefinition("Practitioner.identifier")
            {
                ElementId = "Practitioner.identifier:slice3",
                SliceName = "slice3",
                Max = "*"
            });
            cons.Add(new ElementDefinition("Practitioner.identifier.type")
            {
                ElementId = "Practitioner.identifier:slice3.type",
            }.WithBinding("http://hl7.org/fhir/ValueSet/identifier-type", BindingStrength.Required));

            return result;
        }


        private static StructureDefinition createTestSD(string url, string name, string description, FHIRAllTypes constrainedType, string baseUri = null)
        {
            var result = new StructureDefinition();

            result.Url = url;
            result.Name = name;
            result.Status = PublicationStatus.Draft;
            result.Description = new Markdown(description);
            result.FhirVersion = EnumUtility.ParseLiteral<FHIRVersion>(ModelInfo.Version);
            result.Derivation = StructureDefinition.TypeDerivationRule.Constraint;

            if (ModelInfo.IsKnownResource(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.Resource;
            else if (ModelInfo.IsPrimitive(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.PrimitiveType;
            else if (ModelInfo.IsDataType(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.ComplexType;
            else
                result.Kind = StructureDefinition.StructureDefinitionKind.Logical;

            result.Type = constrainedType.ToString();
            result.Abstract = false;

            if (baseUri == null)
                baseUri = ResourceIdentity.Core(constrainedType).ToString();

            result.BaseDefinition = baseUri;

            result.Differential = new StructureDefinition.DifferentialComponent();

            return result;
        }


    }
}
