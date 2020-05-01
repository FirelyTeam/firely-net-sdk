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
            buildIssue1227(),
            buildMetadataExtension()
        };

        private const string METADATA_EXTENSION_CANONICAL = "http://ehelse.no/fhir/StructureDefinition/gd-fregmetadata";

        private static StructureDefinition buildMetadataExtension()
        {
            var result = createTestSD(METADATA_EXTENSION_CANONICAL, "FregMetadata",
                   "fregmetadata (registermetadata)", FHIRAllTypes.Extension);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Extension") { ElementId = "Extension" });

            var slicingIntro = new ElementDefinition("Extension.extension") { ElementId = "Extension.extension" }
                .WithSlicingIntro(ElementDefinition.SlicingRules.Open,
                (ElementDefinition.DiscriminatorType.Value, "url"));
            cons.Add(slicingIntro);

            //slice 1:
            cons.Add(new ElementDefinition("Extension.extension")
            {
                ElementId = "Extension.extension:registeredTimestamp",
                SliceName = "registeredTimestamp",
                Max = "1"
            });

            cons.Add(new ElementDefinition("Extension.extension.url")
            {
                ElementId = "Extension.extension:registeredTimestamp.url"
            }.Value(fix: new FhirUri("registeredTimestamp")));

            cons.Add(new ElementDefinition("Extension.extension.value[x]")
            {
                ElementId = "Extension.extension:registeredTimestamp.value[x]"
            }.OfType(FHIRAllTypes.DateTime));


            cons.Add(new ElementDefinition("Extension.url")
            {
                ElementId = "Extension.url"
            }.Value(fix: new FhirUri(METADATA_EXTENSION_CANONICAL)));

            cons.Add(new ElementDefinition("Extension.value[x]") { ElementId = "Extension.value[x]" }.Prohibited());

            return result;
        }

        private static StructureDefinition buildIssue1227()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/Issue1227", "Issue1227",
                    "Nested slicing", FHIRAllTypes.Person);
            var cons = result.Differential.Element;

            // cons.Add(new ElementDefinition("Person.Meta") { ElementId = "Person.meta", MustSupport = true });

            // slicing on Person.Meta.Security
            var slicingIntro = new ElementDefinition("Person.meta.security") { ElementId = "Person.meta.security" }
                .WithSlicingIntro(ElementDefinition.SlicingRules.Open,
                (ElementDefinition.DiscriminatorType.Value, "system"));

            cons.Add(slicingIntro);

            // Slice 1 with Binding
            cons.Add(new ElementDefinition("Person.meta.security")
            {
                ElementId = "Person.meta.security:addressConfidentiality",
                SliceName = "addressConfidentiality",
                Max = "1",

            });

            cons.Add(new ElementDefinition("Person.meta.security.system")
            {
                ElementId = "Person.meta.security:addressConfidentiality.system",
                Min = 1
            }.Value(fix: new FhirUri("http://ehelse.no/fhir/ValueSet/gd-address-confidentiality-v05")));

            // Nested slicing on Person.meta.security.extension
            var securityExtSliceIntro = new ElementDefinition("Person.meta.security.extension") { ElementId = "Person.meta.security:addressConfidentiality.extension" }
                .WithSlicingIntro(ElementDefinition.SlicingRules.OpenAtEnd,
                (ElementDefinition.DiscriminatorType.Value, "url"));

            cons.Add(securityExtSliceIntro);

            cons.Add(new ElementDefinition("Person.meta.security.extension")
            {
                ElementId = "Person.meta.security:addressConfidentiality.extension:fregMetadata",
                SliceName = "fregMetadata",
                Max = "1"
            }.OfType(FHIRAllTypes.Extension, METADATA_EXTENSION_CANONICAL));


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
