using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using System.IO;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Serialization;
using System.Diagnostics;

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
            buildOrganizationWithRegexConstraintOnType()
        };

        private static StructureDefinition buildOrganizationWithRegexConstraintOnName()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MyOrganization", "My Organization",
                    "Test an organization with Name containing regex", FHIRDefinedType.Organization);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Organization").OfType(FHIRDefinedType.Organization));

            var nameDef = new ElementDefinition("Organization.name");
            nameDef.SetStringExtension("http://hl7.org/fhir/StructureDefinition/regex", "[A-Z].*");
            cons.Add(nameDef);

            return result;
        }


        private static StructureDefinition buildOrganizationWithRegexConstraintOnType()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/MyOrganization2", "My Organization",
                    "Test an organization with Name containing regex", FHIRDefinedType.Organization);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Organization").OfType(FHIRDefinedType.Organization));

            var nameDef = new ElementDefinition("Organization.name.value").OfType(FHIRDefinedType.String);
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
                    "Test Patient which requires an Identifier with either BSN or drivers license", FHIRDefinedType.Patient);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRDefinedType.Patient));
            cons.Add(new ElementDefinition("Patient.identifier").Required(max: "*")
                        .OfType(FHIRDefinedType.Identifier, "http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN")
                        .OrType(FHIRDefinedType.Identifier, "http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL"));

            return result;
        }

        private static StructureDefinition buildIdentifierWithBSN()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/IdentifierWithBSN", "BSN Identifier",
                    "Test Identifier which requires a BSN oid", FHIRDefinedType.Identifier);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Identifier").OfType(FHIRDefinedType.Identifier));
            cons.Add(new ElementDefinition("Identifier.system").Required().Value(fix: new FhirUri("urn:oid:2.16.840.1.113883.2.4.6.3")));
            cons.Add(new ElementDefinition("Identifier.period").Prohibited());
            cons.Add(new ElementDefinition("Identifier.assigner").Prohibited());

            return result;
        }

        private static StructureDefinition buildIdentifierWithDriversLicense()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/IdentifierWithDL", "Drivers license Identifier",
                    "Test Identifier which requires a drivers license oid", FHIRDefinedType.Identifier);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Identifier").OfType(FHIRDefinedType.Identifier));
            cons.Add(new ElementDefinition("Identifier.system").Required().Value(fix: new FhirUri("urn:oid:2.16.840.1.113883.2.4.6.12")));

            return result;
        }


        private static StructureDefinition buildQuestionnaireWithFixedType()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/QuestionnaireWithFixedType", "Fixed Questionnaire",
                    "Questionnaire with a fixed question type of 'decimal'", FHIRDefinedType.Questionnaire);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Questionnaire").OfType(FHIRDefinedType.Questionnaire));
            cons.Add(new ElementDefinition("Questionnaire.group.question.type").Value(fix: new Code("decimal")));

            return result;
        }

        private static StructureDefinition buildWeightQuantity()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/WeightQuantity", "Weight Quantity",
                    "Quantity which allows just kilograms", FHIRDefinedType.Quantity);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Quantity").OfType(FHIRDefinedType.Quantity));
            cons.Add(new ElementDefinition("Quantity.unit").Required().Value(fix: new FhirString("kg")));
            cons.Add(new ElementDefinition("Quantity.system").Required().Value(fix: new FhirUri("http://unitsofmeasure.org")));
            cons.Add(new ElementDefinition("Quantity.code").Required().Value(fix: new Code("kg")));

            return result;
        }

        private static StructureDefinition buildHeightQuantity()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/HeightQuantity", "Height Quantity",
                    "Quantity which allows just centimeters", FHIRDefinedType.Quantity);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Quantity").OfType(FHIRDefinedType.Quantity));
            cons.Add(new ElementDefinition("Quantity.unit").Required().Value(fix: new FhirString("cm")));
            cons.Add(new ElementDefinition("Quantity.system").Required().Value(fix: new FhirUri("http://unitsofmeasure.org")));
            cons.Add(new ElementDefinition("Quantity.code").Required().Value(fix: new Code("cm")));

            return result;
        }

        private static StructureDefinition buildWeightHeightObservation()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/WeightHeightObservation", "Weight/Height Observation",
                    "Observation with a choice of weight/height or another type of value", FHIRDefinedType.Observation);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Observation").OfType(FHIRDefinedType.Observation));
            cons.Add(new ElementDefinition("Observation.value[x]")
                .OfType(FHIRDefinedType.Quantity, "http://validationtest.org/fhir/StructureDefinition/WeightQuantity")
                .OrType(FHIRDefinedType.Quantity, "http://validationtest.org/fhir/StructureDefinition/HeightQuantity")
                .OrType(FHIRDefinedType.String));

            Debug.WriteLine(new FhirXmlSerializer().SerializeToString(result));
            return result;
        }


        private static StructureDefinition bundleWithSpecificEntries(string prefix)
        {
            var result = createTestSD($"http://validationtest.org/fhir/StructureDefinition/BundleWith{prefix}Entries", $"Bundle with specific {prefix} test entries",
                    $"Bundle with just Organization or {prefix} Patient entries", FHIRDefinedType.Bundle);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Bundle").OfType(FHIRDefinedType.Bundle));
            cons.Add(new ElementDefinition("Bundle.entry.resource")
                .OfType(FHIRDefinedType.Organization)
                .OrType(FHIRDefinedType.Patient, $"http://validationtest.org/fhir/StructureDefinition/PatientWith{prefix}Organization"));

            return result;
        }

        private static StructureDefinition bundleWithConstrainedContained()
        {
            var result = createTestSD($"http://validationtest.org/fhir/StructureDefinition/BundleWithConstrainedContained", 
                            $"Bundle with a constraint on the Bundle.entry.resource",
                    $"Bundle with a constraint on the Bundle.entry.resource", FHIRDefinedType.Bundle);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Bundle").OfType(FHIRDefinedType.Bundle));
            cons.Add(new ElementDefinition("Bundle.entry.resource.meta").Required());

            return result;
        }


        private static StructureDefinition patientWithSpecificOrganization(IEnumerable<ElementDefinition.AggregationMode> aggregation, string prefix)
        {
            var result = createTestSD($"http://validationtest.org/fhir/StructureDefinition/PatientWith{prefix}Organization", $"Patient with {prefix} managing organization",
                    $"Patient for which the managingOrganization reference is limited to {prefix} references", FHIRDefinedType.Patient);

            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Patient").OfType(FHIRDefinedType.Patient));
            cons.Add(new ElementDefinition("Patient.managingOrganization")
                .OfType(FHIRDefinedType.Reference, ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Organization), aggregation));

            return result;
        }


        private static StructureDefinition buildParametersWithBoundParams()
        {
            var result = createTestSD("http://validationtest.org/fhir/StructureDefinition/ParametersWithBoundParams", "Parameters with term binding on Params",
                    "Parameters resource where the parameter.value[x] is bound to a valueset", FHIRDefinedType.Parameters);
            var cons = result.Differential.Element;

            cons.Add(new ElementDefinition("Parameters").OfType(FHIRDefinedType.Parameters));
            cons.Add(new ElementDefinition("Parameters.parameter.value[x]")
                    .WithBinding("http://hl7.org/fhir/ValueSet/data-absent-reason", BindingStrength.Required));

            return result;
        }


        private static StructureDefinition createTestSD(string url, string name, string description, FHIRDefinedType constrainedType, string baseUri=null)
        {
            var result = new StructureDefinition();

            result.Url = url;
            result.Name = name;
            result.Status = ConformanceResourceStatus.Draft;
            result.Description = description;
            result.FhirVersion = ModelInfo.Version;

            if (ModelInfo.IsKnownResource(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.Resource;
            else if (ModelInfo.IsDataType(constrainedType) || ModelInfo.IsPrimitive(constrainedType))
                result.Kind = StructureDefinition.StructureDefinitionKind.Datatype;
            else
                result.Kind = StructureDefinition.StructureDefinitionKind.Logical;

            result.ConstrainedType = constrainedType;
            result.Abstract = false;

            if(baseUri == null)
                baseUri = ResourceIdentity.Core(constrainedType).ToString();

            result.Base = baseUri;

            result.Differential = new StructureDefinition.DifferentialComponent();

            return result;
        }


    }
}
