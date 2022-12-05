﻿using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.STU3.Specification.Tests.Snapshot
{
    internal static class LExt
    {
        public static List<T> AddM<T>(this List<T> me, IEnumerable<T> them)
        {
            me.AddRange(them);
            return me;
        }
    }

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
            buildPatientWithIdentifierSlicing(),
            buildMiPatient(),
            slicingWithCodeableConcept(),
            slicingWithQuantity(),
            buildPatientWithExistsSlicing(),
            buildTranslatableCodeableConcept(),
            buildObservationWithTranslatableCode(),
            buildObservationWithTargetProfilesAndChildDefs(),
            buildPatientWithDeceasedConstraints("RequiredBoolean"),
            buildPatientWithDeceasedConstraints(),
            buildBoolean(),
            buildMyExtension(),
            buildSliceOnChoice(),
            buildConstrainBindableType()
        }.AddM(buildPatientWithProfiledReferences());

        private static StructureDefinition buildObservationWithTargetProfilesAndChildDefs()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/Observation-issue-1654", "Observation-issue-1654",
                "Observation with targetprofile on subject and children definition under subject as well", FHIRAllTypes.Observation);

            var cons = result.Differential.Element;
            cons.Add(new ElementDefinition("Observation.subject")
            {
                ElementId = "Observation.subject",
            }.OfReference(targetProfile: ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Patient)));

            cons.Add(new ElementDefinition("Observation.subject.display")
            {
                ElementId = "Observation.subject.display",
                MaxLength = 10
            });

            return result;
        }

        private static StructureDefinition buildTranslatableCodeableConcept()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/CodeableConceptTranslatable", "CodeableConceptTranslatable",
                                  "Test CodeableConcept with an extension on CodeableConcept.text", FHIRAllTypes.CodeableConcept);

            var cons = result.Differential.Element;
            var ed = new ElementDefinition("CodeableConcept.text")
            {
                ElementId = "CodeableConcept.text",
            };
            ed.AddExtension("http://hl7.org/fhir/StructureDefinition/elementdefinition-translatable", new FhirBoolean(true));
            cons.Add(ed);

            return result;
        }

        private static StructureDefinition buildObservationWithTranslatableCode()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ObservationWithTranslatableCode", "ObservationWithTranslatableCode",
                       "Test Observation with a profiled CodeableConcept for Observation.code", FHIRAllTypes.Observation);

            var cons = result.Differential.Element;
            cons.Add(new ElementDefinition("Observation.code")
            {
                ElementId = "Observation.code"
            }.OfType(FHIRAllTypes.CodeableConcept, "http://validationtest.org/fhir/StructureDefinition/CodeableConceptTranslatable"));

            return result;
        }

        private static StructureDefinition slicingWithCodeableConcept()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ObservationSlicingCodeableConcept", "ObservationSlicingCodeableConcept",
                       "Test Observation with slicing on value[x], first slice CodeableConcept", FHIRAllTypes.Observation);

            var cons = result.Differential.Element;

            var slicingIntro = new ElementDefinition("Observation.value[x]")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed)
               .OfType(FHIRAllTypes.CodeableConcept);

            cons.Add(slicingIntro);

            cons.Add(new ElementDefinition("Observation.value[x]")
            {
                ElementId = "Observation.value[x]:valueCodeableConcept",
                SliceName = "valueCodeableConcept",
            }.OfType(FHIRAllTypes.CodeableConcept)
             .WithBinding("http://somewhere/something", BindingStrength.Required));

            return result;
        }

        private static StructureDefinition slicingWithQuantity()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ObservationValueSlicingQuantity", "ObservationSlicingQuantity",
                       "Test Observation with slicing on value[x], first slice Quantity", FHIRAllTypes.Observation,
                       "http://validationtest.org/fhir/StructureDefinition/ObservationSlicingCodeableConcept");

            var cons = result.Differential.Element;

            var slicingIntro = new ElementDefinition("Observation.value[x]")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed)
               .OfType(FHIRAllTypes.Quantity);

            cons.Add(slicingIntro);

            cons.Add(new ElementDefinition("Observation.value[x]")
            {
                ElementId = "Observation.value[x]:valueQuantity",
                SliceName = "valueQuantity",
            }.OfType(FHIRAllTypes.Quantity)
             .WithBinding("http://somewhere/something-else", BindingStrength.Required));

            return result;
        }


        private static StructureDefinition buildPatientWithIdentifierSlicing()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/PatientIdentifierSlicing", "PatientIdentifierSlicing",
                       "Test Patient with slicing on Identifier, first slice BSN", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;
            var slicingIntro = new ElementDefinition("Patient.identifier")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed,
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

        private static StructureDefinition buildPatientWithExistsSlicing()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/PatientExistsSlicing", "PatientExistsSlicing",
                       "Test Patient with exists slicing on Identifier", FHIRAllTypes.Patient);

            var cons = result.Differential.Element;
            var slicingIntro = new ElementDefinition("Patient.identifier")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed,
               (ElementDefinition.DiscriminatorType.Exists, "use"));

            cons.Add(slicingIntro);

            // Slice 1: if there is no use, then there should not be a system
            cons.Add(new ElementDefinition("Patient.identifier")
            {
                ElementId = "Patient.identifier:WithoutUse",
                SliceName = "WithoutUse"
            });

            cons.Add(new ElementDefinition("Patient.identifier.use")
            {
                ElementId = "Patient.identifier:WithoutUse.system",
            }.Prohibited());

            cons.Add(new ElementDefinition("Patient.identifier.system")
            {
                ElementId = "Patient.identifier:WithoutUse.system",
            }.Prohibited());

            // Slice 2: if there is a use, then there should be a system
            cons.Add(new ElementDefinition("Patient.identifier")
            {
                ElementId = "Patient.identifier:WithUse",
                SliceName = "WithUse"
            });

            cons.Add(new ElementDefinition("Patient.identifier.use")
            {
                ElementId = "Patient.identifier:WithUse.system",
            }.Required());

            cons.Add(new ElementDefinition("Patient.identifier.system")
            {
                ElementId = "Patient.identifier:WithUse.system",
            }.Required());

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
                Definition = "BSN naming system",
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
                Definition = "Test_1295"
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

            var nameDef = new ElementDefinition("Organization.name.value").OfType(FHIRAllTypes.String);
            nameDef.Type.Single().SetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-regex", "[A-Z].*");
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
                        .OfType(FHIRAllTypes.Identifier, "http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN")
                        .OrType(FHIRAllTypes.Identifier, "http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL"));

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
                .OfType(FHIRAllTypes.Quantity, "http://validationtest.org/fhir/StructureDefinition/WeightQuantity")
                .OrType(FHIRAllTypes.Quantity, "http://validationtest.org/fhir/StructureDefinition/HeightQuantity")
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
                .OrType(FHIRAllTypes.Patient, $"http://validationtest.org/fhir/StructureDefinition/PatientWith{prefix}Organization"));

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
                .OfReference(ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Organization), aggregation));

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
        public const string PROFILED_ORG_URL = "http://validationtest.org/fhir/StructureDefinition/ProfiledOrganization";

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
                .OfType(FHIRAllTypes.Quantity, profile: QUANTITY_WITH_UNLIMITED_ROOT_CARDINALITY_CANONICAL)
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
                .WithSlicingIntro(ElementDefinition.SlicingRules.Closed,
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
            result.FhirVersion = ModelInfo.Version;
            result.Derivation = StructureDefinition.TypeDerivationRule.Constraint;

            if (ModelInfo.IsKnownResource(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.Resource;
            else if (ModelInfo.IsPrimitive(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.PrimitiveType;
            else if (ModelInfo.IsDataType(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.ComplexType;
            else
                result.Kind = StructureDefinition.StructureDefinitionKind.Logical;

            result.Type = constrainedType.GetLiteral();
            result.Abstract = false;

            if (baseUri == null)
                baseUri = ResourceIdentity.Core(constrainedType.GetLiteral()).ToString();

            result.BaseDefinition = baseUri;

            result.Differential = new StructureDefinition.DifferentialComponent();

            return result;
        }

        private static StructureDefinition buildMyExtension()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MyRangeExtension", "My Rage Extension",
                "An extension with a value of type Range", FHIRAllTypes.Extension);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Extension").OfType(FHIRAllTypes.Extension));
            cons.Add(new ElementDefinition("Extension.url").Value(new FhirUri("http://validationtest.org/fhir/StructureDefinition/MyRangeExtension")));
            cons.Add(new ElementDefinition("Extension.value[x]").OfType(FHIRAllTypes.Range));

            return result;
        }

        private static StructureDefinition buildBoolean()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/RequiredBoolean", "Required Boolean",
                "A boolean type where the value is required", FHIRAllTypes.Boolean);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Boolean").OfType(FHIRAllTypes.Boolean));
            cons.Add(new ElementDefinition("Boolean.value").Required());

            return result;
        }

        private static StructureDefinition buildPatientWithDeceasedConstraints(string profile = null)
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/DeceasedPatient" + profile, "DeceasedPatient",
                    "Test Patient with extra deceased constraints", FHIRAllTypes.Patient);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRAllTypes.Patient));

            if (profile is not null)
            {
                cons.Add(new ElementDefinition("Patient.deceased[x]")
                    .OfType(FHIRAllTypes.Boolean, "http://validationtest.org/fhir/StructureDefinition/RequiredBoolean")
                    .OrType(FHIRAllTypes.DateTime));
            }

            var slicingIntro = new ElementDefinition("Patient.deceased[x].extension")
                .WithSlicingIntro(ElementDefinition.SlicingRules.Open, (ElementDefinition.DiscriminatorType.Value, "url"));

            cons.Add(slicingIntro);

            cons.Add(new ElementDefinition("Patient.deceased[x].extension")
            {
                ElementId = "Patient.deceased[x].extension:range",
                SliceName = "range",
            }.OfType(FHIRAllTypes.Extension, "http://validationtest.org/fhir/StructureDefinition/MyRangeExtension"));
            return result;
        }

        private static IEnumerable<StructureDefinition> buildPatientWithProfiledReferences()
        {
            yield return createTestSD(PROFILED_ORG_URL, "A profiled organization",
                    "A profiled Organization with no additional constraints", FHIRAllTypes.Organization);

            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/PatientWithReferences", "Patient with References",
                    "Test Patient which has a profiled managing organization", FHIRAllTypes.Patient);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRAllTypes.Patient));
            cons.Add(new ElementDefinition("Patient.managingOrganization").OfReference(PROFILED_ORG_URL));
            yield return result;
        }

        private static StructureDefinition buildSliceOnChoice()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MedicationStatement-issue-2132", "MedicationStatement-issue-2132",
                "MedicationStatement sliced on asNeeded[x]", FHIRAllTypes.MedicationStatement);

            var cons = result.Differential.Element;

            var slicingIntro = new ElementDefinition("MedicationStatement.dosage.asNeeded[x]")
               .WithSlicingIntro(ElementDefinition.SlicingRules.Closed, (ElementDefinition.DiscriminatorType.Type, "$this"));

            cons.Add(slicingIntro);

            cons.Add(new ElementDefinition("MedicationStatement.dosage.asNeeded[x]")
            {
                ElementId = "MedicationStatement.dosage.asNeeded[x]:asNeededBoolean",
                SliceName = "asNeededBoolean",
            }.OfType(FHIRAllTypes.Boolean));

            cons.Add(new ElementDefinition("MedicationStatement.dosage.asNeeded[x]")
            {
                ElementId = "MedicationStatement.dosage.asNeeded[x]:asNeededCodeableConcept",
                SliceName = "asNeededCodeableConcept",
            }.OfType(FHIRAllTypes.CodeableConcept));

            return result;
        }

        private static StructureDefinition buildConstrainBindableType()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MedicationStatement-issue-2132-2", "MedicationStatement-issue-2132",
                "MedicationStatement sliced on asNeeded[x]", FHIRAllTypes.MedicationStatement);

            var cons = result.Differential.Element;

            var typeConstraint = new ElementDefinition("MedicationStatement.dosage.asNeededBoolean").OfType(FHIRAllTypes.Boolean);

            cons.Add(typeConstraint);

            return result;
        }

    }
}
