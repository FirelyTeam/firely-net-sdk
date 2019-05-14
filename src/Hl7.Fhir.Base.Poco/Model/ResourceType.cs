using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// One of the resource types defined as part of FHIR.
    /// (url: http://hl7.org/fhir/ValueSet/resource-types)
    /// </summary>
    [FhirEnumeration("ResourceType")]
    public enum ResourceType
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Account", "http://hl7.org/fhir/resource-types"), Description("Account")]
        Account,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ActivityDefinition", "http://hl7.org/fhir/resource-types"), Description("ActivityDefinition")]
        ActivityDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AdverseEvent", "http://hl7.org/fhir/resource-types"), Description("AdverseEvent")]
        AdverseEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance", "http://hl7.org/fhir/resource-types"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment", "http://hl7.org/fhir/resource-types"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/resource-types"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent", "http://hl7.org/fhir/resource-types"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic", "http://hl7.org/fhir/resource-types"), Description("Basic")]
        Basic,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary", "http://hl7.org/fhir/resource-types"), Description("Binary")]
        Binary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BiologicallyDerivedProduct", "http://hl7.org/fhir/resource-types"), Description("BiologicallyDerivedProduct"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        BiologicallyDerivedProduct,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        BodySite,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodyStructure", "http://hl7.org/fhir/resource-types"), Description("BodyStructure"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        BodyStructure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle", "http://hl7.org/fhir/resource-types"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CapabilityStatement", "http://hl7.org/fhir/resource-types"), Description("CapabilityStatement")]
        CapabilityStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan", "http://hl7.org/fhir/resource-types"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CareTeam", "http://hl7.org/fhir/resource-types"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CatalogEntry", "http://hl7.org/fhir/resource-types"), Description("CatalogEntry"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        CatalogEntry,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItemDefinition", "http://hl7.org/fhir/resource-types"), Description("ChargeItemDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ChargeItemDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim", "http://hl7.org/fhir/resource-types"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse", "http://hl7.org/fhir/resource-types"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression", "http://hl7.org/fhir/resource-types"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CodeSystem", "http://hl7.org/fhir/resource-types"), Description("CodeSystem")]
        CodeSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication", "http://hl7.org/fhir/resource-types"), Description("Communication")]
        Communication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CompartmentDefinition", "http://hl7.org/fhir/resource-types"), Description("CompartmentDefinition")]
        CompartmentDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition", "http://hl7.org/fhir/resource-types"), Description("Composition")]
        Composition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap", "http://hl7.org/fhir/resource-types"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition", "http://hl7.org/fhir/resource-types"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Consent", "http://hl7.org/fhir/resource-types"), Description("Consent")]
        Consent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract", "http://hl7.org/fhir/resource-types"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage", "http://hl7.org/fhir/resource-types"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        DataElement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CoverageEligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("CoverageEligibilityRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        CoverageEligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CoverageEligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("CoverageEligibilityResponse"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        CoverageEligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue", "http://hl7.org/fhir/resource-types"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/resource-types"), Description("Device")]
        Device,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        DeviceComponent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceDefinition", "http://hl7.org/fhir/resource-types"), Description("DeviceDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        DeviceDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric", "http://hl7.org/fhir/resource-types"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceRequest")]
        DeviceRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement", "http://hl7.org/fhir/resource-types"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport", "http://hl7.org/fhir/resource-types"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest", "http://hl7.org/fhir/resource-types"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference", "http://hl7.org/fhir/resource-types"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource", "http://hl7.org/fhir/resource-types"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EffectEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("EffectEvidenceSynthesis"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        EffectEvidenceSynthesis,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        EligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/resource-types"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Endpoint", "http://hl7.org/fhir/resource-types"), Description("Endpoint")]
        Endpoint,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/resource-types"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse", "http://hl7.org/fhir/resource-types"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare", "http://hl7.org/fhir/resource-types"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EventDefinition", "http://hl7.org/fhir/resource-types"), Description("EventDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        EventDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Evidence", "http://hl7.org/fhir/resource-types"), Description("Evidence"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Evidence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EvidenceVariable", "http://hl7.org/fhir/resource-types"), Description("EvidenceVariable"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        EvidenceVariable,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExampleScenario", "http://hl7.org/fhir/resource-types"), Description("ExampleScenario"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ExampleScenario,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ExpansionProfile,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit", "http://hl7.org/fhir/resource-types"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory", "http://hl7.org/fhir/resource-types"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag", "http://hl7.org/fhir/resource-types"), Description("Flag")]
        Flag,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal", "http://hl7.org/fhir/resource-types"), Description("Goal")]
        Goal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GraphDefinition", "http://hl7.org/fhir/resource-types"), Description("GraphDefinition")]
        GraphDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group", "http://hl7.org/fhir/resource-types"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GuidanceResponse", "http://hl7.org/fhir/resource-types"), Description("GuidanceResponse")]
        GuidanceResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService", "http://hl7.org/fhir/resource-types"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ImagingManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy", "http://hl7.org/fhir/resource-types"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization", "http://hl7.org/fhir/resource-types"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationEvaluation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationEvaluation"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ImmunizationEvaluation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide", "http://hl7.org/fhir/resource-types"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("InsurancePlan", "http://hl7.org/fhir/resource-types"), Description("InsurancePlan"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        InsurancePlan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Invoice", "http://hl7.org/fhir/resource-types"), Description("Invoice"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Invoice,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Library", "http://hl7.org/fhir/resource-types"), Description("Library")]
        Library,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Linkage", "http://hl7.org/fhir/resource-types"), Description("Linkage")]
        Linkage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List", "http://hl7.org/fhir/resource-types"), Description("List")]
        List,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location", "http://hl7.org/fhir/resource-types"), Description("Location")]
        Location,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Measure", "http://hl7.org/fhir/resource-types"), Description("Measure")]
        Measure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MeasureReport", "http://hl7.org/fhir/resource-types"), Description("MeasureReport")]
        MeasureReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media", "http://hl7.org/fhir/resource-types"), Description("Media")]
        Media,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication", "http://hl7.org/fhir/resource-types"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration", "http://hl7.org/fhir/resource-types"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense", "http://hl7.org/fhir/resource-types"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationKnowledge", "http://hl7.org/fhir/resource-types"), Description("MedicationKnowledge"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicationKnowledge,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/resource-types"), Description("MedicationRequest")]
        MedicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProduct", "http://hl7.org/fhir/resource-types"), Description("MedicinalProduct"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProduct,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductAuthorization", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductAuthorization"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductAuthorization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductContraindication", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductContraindication"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductContraindication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductIndication", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductIndication"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductIndication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductIngredient", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductIngredient"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductIngredient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductInteraction", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductInteraction"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductInteraction,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductManufactured", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductManufactured"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductManufactured,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductPackaged", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductPackaged"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductPackaged,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductPharmaceutical", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductPharmaceutical"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductPharmaceutical,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductUndesirableEffect", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductUndesirableEffect"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductUndesirableEffect,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageDefinition", "http://hl7.org/fhir/resource-types"), Description("MessageDefinition")]
        MessageDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MolecularSequence", "http://hl7.org/fhir/resource-types"), Description("MolecularSequence"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MolecularSequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem", "http://hl7.org/fhir/resource-types"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/resource-types"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation", "http://hl7.org/fhir/resource-types"), Description("Observation")]
        Observation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ObservationDefinition", "http://hl7.org/fhir/resource-types"), Description("ObservationDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ObservationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition", "http://hl7.org/fhir/resource-types"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome", "http://hl7.org/fhir/resource-types"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization", "http://hl7.org/fhir/resource-types"), Description("Organization")]
        Organization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OrganizationAffiliation", "http://hl7.org/fhir/resource-types"), Description("OrganizationAffiliation"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        OrganizationAffiliation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters", "http://hl7.org/fhir/resource-types"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient", "http://hl7.org/fhir/resource-types"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice", "http://hl7.org/fhir/resource-types"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation", "http://hl7.org/fhir/resource-types"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person", "http://hl7.org/fhir/resource-types"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PlanDefinition", "http://hl7.org/fhir/resource-types"), Description("PlanDefinition")]
        PlanDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/resource-types"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PractitionerRole", "http://hl7.org/fhir/resource-types"), Description("PractitionerRole")]
        PractitionerRole,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure", "http://hl7.org/fhir/resource-types"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ProcessResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance", "http://hl7.org/fhir/resource-types"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire", "http://hl7.org/fhir/resource-types"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse", "http://hl7.org/fhir/resource-types"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ReferralRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/resource-types"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RequestGroup", "http://hl7.org/fhir/resource-types"), Description("RequestGroup")]
        RequestGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchDefinition", "http://hl7.org/fhir/resource-types"), Description("ResearchDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ResearchDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchElementDefinition", "http://hl7.org/fhir/resource-types"), Description("ResearchElementDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ResearchElementDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchStudy", "http://hl7.org/fhir/resource-types"), Description("ResearchStudy")]
        ResearchStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchSubject", "http://hl7.org/fhir/resource-types"), Description("ResearchSubject")]
        ResearchSubject,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource", "http://hl7.org/fhir/resource-types"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment", "http://hl7.org/fhir/resource-types"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("RiskEvidenceSynthesis"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        RiskEvidenceSynthesis,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule", "http://hl7.org/fhir/resource-types"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter", "http://hl7.org/fhir/resource-types"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Sequence", "http://hl7.org/fhir/resource-types"), Description("Sequence"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition", "http://hl7.org/fhir/resource-types"), Description("ServiceDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ServiceDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/resource-types"), Description("ServiceRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ServiceRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot", "http://hl7.org/fhir/resource-types"), Description("Slot")]
        Slot,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen", "http://hl7.org/fhir/resource-types"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SpecimenDefinition", "http://hl7.org/fhir/resource-types"), Description("SpecimenDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SpecimenDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition", "http://hl7.org/fhir/resource-types"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureMap", "http://hl7.org/fhir/resource-types"), Description("StructureMap")]
        StructureMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription", "http://hl7.org/fhir/resource-types"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance", "http://hl7.org/fhir/resource-types"), Description("Substance")]
        Substance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceNucleicAcid", "http://hl7.org/fhir/resource-types"), Description("SubstanceNucleicAcid"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceNucleicAcid,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstancePolymer", "http://hl7.org/fhir/resource-types"), Description("SubstancePolymer"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstancePolymer,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceProtein", "http://hl7.org/fhir/resource-types"), Description("SubstanceProtein"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceProtein,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceReferenceInformation", "http://hl7.org/fhir/resource-types"), Description("SubstanceReferenceInformation"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceReferenceInformation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceSourceMaterial", "http://hl7.org/fhir/resource-types"), Description("SubstanceSourceMaterial"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceSourceMaterial,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceSpecification", "http://hl7.org/fhir/resource-types"), Description("SubstanceSpecification"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceSpecification,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery", "http://hl7.org/fhir/resource-types"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/resource-types"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Task", "http://hl7.org/fhir/resource-types"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TerminologyCapabilities", "http://hl7.org/fhir/resource-types"), Description("TerminologyCapabilities"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        TerminologyCapabilities,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestReport", "http://hl7.org/fhir/resource-types"), Description("TestReport")]
        TestReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript", "http://hl7.org/fhir/resource-types"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet", "http://hl7.org/fhir/resource-types"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VerificationResult", "http://hl7.org/fhir/resource-types"), Description("VerificationResult"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        VerificationResult,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
    }


    /// <summary>
    /// Either an abstract type, a resource or a data type.
    /// (url: http://hl7.org/fhir/ValueSet/all-types)
    /// </summary>
    [FhirEnumeration("FHIRAllTypes")]
    public enum FHIRAllTypes
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Address", "http://hl7.org/fhir/data-types"), Description("Address")]
        Address,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Age", "http://hl7.org/fhir/data-types"), Description("Age")]
        Age,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Annotation", "http://hl7.org/fhir/data-types"), Description("Annotation")]
        Annotation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Attachment", "http://hl7.org/fhir/data-types"), Description("Attachment")]
        Attachment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("BackboneElement", "http://hl7.org/fhir/data-types"), Description("BackboneElement")]
        BackboneElement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("CodeableConcept", "http://hl7.org/fhir/data-types"), Description("CodeableConcept")]
        CodeableConcept,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Coding", "http://hl7.org/fhir/data-types"), Description("Coding")]
        Coding,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ContactDetail", "http://hl7.org/fhir/data-types"), Description("ContactDetail")]
        ContactDetail,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ContactPoint", "http://hl7.org/fhir/data-types"), Description("ContactPoint")]
        ContactPoint,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Contributor", "http://hl7.org/fhir/data-types"), Description("Contributor")]
        Contributor,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Count", "http://hl7.org/fhir/data-types"), Description("Count")]
        Count,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("DataRequirement", "http://hl7.org/fhir/data-types"), Description("DataRequirement")]
        DataRequirement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Distance", "http://hl7.org/fhir/data-types"), Description("Distance")]
        Distance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Dosage", "http://hl7.org/fhir/data-types"), Description("Dosage")]
        Dosage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Duration", "http://hl7.org/fhir/data-types"), Description("Duration")]
        Duration,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Element", "http://hl7.org/fhir/data-types"), Description("Element")]
        Element,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ElementDefinition", "http://hl7.org/fhir/data-types"), Description("ElementDefinition")]
        ElementDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Expression", "http://hl7.org/fhir/data-types"), Description("Expression"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Expression,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Extension", "http://hl7.org/fhir/data-types"), Description("Extension")]
        Extension,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("HumanName", "http://hl7.org/fhir/data-types"), Description("HumanName")]
        HumanName,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Identifier", "http://hl7.org/fhir/data-types"), Description("Identifier")]
        Identifier,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("MarketingStatus", "http://hl7.org/fhir/data-types"), Description("MarketingStatus"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MarketingStatus,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Meta", "http://hl7.org/fhir/data-types"), Description("Meta")]
        Meta,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Money", "http://hl7.org/fhir/data-types"), Description("Money")]
        Money,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("MoneyQuantity", "http://hl7.org/fhir/data-types"), Description("MoneyQuantity"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MoneyQuantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Narrative", "http://hl7.org/fhir/data-types"), Description("Narrative")]
        Narrative,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ParameterDefinition", "http://hl7.org/fhir/data-types"), Description("ParameterDefinition")]
        ParameterDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Period", "http://hl7.org/fhir/data-types"), Description("Period")]
        Period,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Population", "http://hl7.org/fhir/data-types"), Description("Population"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Population,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ProdCharacteristic", "http://hl7.org/fhir/data-types"), Description("ProdCharacteristic"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ProdCharacteristic,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ProductShelfLife", "http://hl7.org/fhir/data-types"), Description("ProductShelfLife"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ProductShelfLife,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Quantity", "http://hl7.org/fhir/data-types"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Range", "http://hl7.org/fhir/data-types"), Description("Range")]
        Range,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Ratio", "http://hl7.org/fhir/data-types"), Description("Ratio")]
        Ratio,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Reference", "http://hl7.org/fhir/data-types"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("RelatedArtifact", "http://hl7.org/fhir/data-types"), Description("RelatedArtifact")]
        RelatedArtifact,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SampledData", "http://hl7.org/fhir/data-types"), Description("SampledData")]
        SampledData,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Signature", "http://hl7.org/fhir/data-types"), Description("Signature")]
        Signature,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SimpleQuantity", "http://hl7.org/fhir/data-types"), Description("SimpleQuantity")]
        SimpleQuantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SubstanceAmount", "http://hl7.org/fhir/data-types"), Description("SubstanceAmount"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceAmount,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Timing", "http://hl7.org/fhir/data-types"), Description("Timing")]
        Timing,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("TriggerDefinition", "http://hl7.org/fhir/data-types"), Description("TriggerDefinition")]
        TriggerDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("UsageContext", "http://hl7.org/fhir/data-types"), Description("UsageContext")]
        UsageContext,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("base64Binary", "http://hl7.org/fhir/data-types"), Description("base64Binary")]
        Base64Binary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/data-types"), Description("boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("canonical", "http://hl7.org/fhir/data-types"), Description("canonical"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Canonical,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("code", "http://hl7.org/fhir/data-types"), Description("code")]
        Code,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/data-types"), Description("date")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/data-types"), Description("dateTime")]
        DateTime,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("decimal", "http://hl7.org/fhir/data-types"), Description("decimal")]
        Decimal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("id", "http://hl7.org/fhir/data-types"), Description("id")]
        Id,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("instant", "http://hl7.org/fhir/data-types"), Description("instant")]
        Instant,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/data-types"), Description("integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("markdown", "http://hl7.org/fhir/data-types"), Description("markdown")]
        Markdown,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("oid", "http://hl7.org/fhir/data-types"), Description("oid")]
        Oid,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("positiveInt", "http://hl7.org/fhir/data-types"), Description("positiveInt")]
        PositiveInt,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/data-types"), Description("string")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("time", "http://hl7.org/fhir/data-types"), Description("time")]
        Time,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("unsignedInt", "http://hl7.org/fhir/data-types"), Description("unsignedInt")]
        UnsignedInt,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uri", "http://hl7.org/fhir/data-types"), Description("uri")]
        Uri,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("url", "http://hl7.org/fhir/data-types"), Description("url"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Url,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uuid", "http://hl7.org/fhir/data-types"), Description("uuid")]
        Uuid,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("xhtml", "http://hl7.org/fhir/data-types"), Description("XHTML")]
        Xhtml,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Account", "http://hl7.org/fhir/resource-types"), Description("Account")]
        Account,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ActivityDefinition", "http://hl7.org/fhir/resource-types"), Description("ActivityDefinition")]
        ActivityDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AdverseEvent", "http://hl7.org/fhir/resource-types"), Description("AdverseEvent")]
        AdverseEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance", "http://hl7.org/fhir/resource-types"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment", "http://hl7.org/fhir/resource-types"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/resource-types"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent", "http://hl7.org/fhir/resource-types"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic", "http://hl7.org/fhir/resource-types"), Description("Basic")]
        Basic,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary", "http://hl7.org/fhir/resource-types"), Description("Binary")]
        Binary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        BodySite,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BiologicallyDerivedProduct", "http://hl7.org/fhir/resource-types"), Description("BiologicallyDerivedProduct"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        BiologicallyDerivedProduct,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodyStructure", "http://hl7.org/fhir/resource-types"), Description("BodyStructure"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        BodyStructure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle", "http://hl7.org/fhir/resource-types"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CapabilityStatement", "http://hl7.org/fhir/resource-types"), Description("CapabilityStatement")]
        CapabilityStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan", "http://hl7.org/fhir/resource-types"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CareTeam", "http://hl7.org/fhir/resource-types"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CatalogEntry", "http://hl7.org/fhir/resource-types"), Description("CatalogEntry"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        CatalogEntry,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItemDefinition", "http://hl7.org/fhir/resource-types"), Description("ChargeItemDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ChargeItemDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim", "http://hl7.org/fhir/resource-types"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse", "http://hl7.org/fhir/resource-types"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression", "http://hl7.org/fhir/resource-types"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CodeSystem", "http://hl7.org/fhir/resource-types"), Description("CodeSystem")]
        CodeSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication", "http://hl7.org/fhir/resource-types"), Description("Communication")]
        Communication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CompartmentDefinition", "http://hl7.org/fhir/resource-types"), Description("CompartmentDefinition")]
        CompartmentDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition", "http://hl7.org/fhir/resource-types"), Description("Composition")]
        Composition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap", "http://hl7.org/fhir/resource-types"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition", "http://hl7.org/fhir/resource-types"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Consent", "http://hl7.org/fhir/resource-types"), Description("Consent")]
        Consent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract", "http://hl7.org/fhir/resource-types"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage", "http://hl7.org/fhir/resource-types"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CoverageEligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("CoverageEligibilityRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        CoverageEligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CoverageEligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("CoverageEligibilityResponse"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        CoverageEligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        DataElement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue", "http://hl7.org/fhir/resource-types"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/resource-types"), Description("Device")]
        Device,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        DeviceComponent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceDefinition", "http://hl7.org/fhir/resource-types"), Description("DeviceDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        DeviceDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric", "http://hl7.org/fhir/resource-types"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceRequest")]
        DeviceRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement", "http://hl7.org/fhir/resource-types"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport", "http://hl7.org/fhir/resource-types"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest", "http://hl7.org/fhir/resource-types"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference", "http://hl7.org/fhir/resource-types"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource", "http://hl7.org/fhir/resource-types"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        EligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EffectEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("EffectEvidenceSynthesis"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        EffectEvidenceSynthesis,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/resource-types"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Endpoint", "http://hl7.org/fhir/resource-types"), Description("Endpoint")]
        Endpoint,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/resource-types"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse", "http://hl7.org/fhir/resource-types"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare", "http://hl7.org/fhir/resource-types"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EventDefinition", "http://hl7.org/fhir/resource-types"), Description("EventDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        EventDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Evidence", "http://hl7.org/fhir/resource-types"), Description("Evidence"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Evidence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EvidenceVariable", "http://hl7.org/fhir/resource-types"), Description("EvidenceVariable"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        EvidenceVariable,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExampleScenario", "http://hl7.org/fhir/resource-types"), Description("ExampleScenario"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ExampleScenario,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ExpansionProfile,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit", "http://hl7.org/fhir/resource-types"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory", "http://hl7.org/fhir/resource-types"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag", "http://hl7.org/fhir/resource-types"), Description("Flag")]
        Flag,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal", "http://hl7.org/fhir/resource-types"), Description("Goal")]
        Goal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GraphDefinition", "http://hl7.org/fhir/resource-types"), Description("GraphDefinition")]
        GraphDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group", "http://hl7.org/fhir/resource-types"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GuidanceResponse", "http://hl7.org/fhir/resource-types"), Description("GuidanceResponse")]
        GuidanceResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService", "http://hl7.org/fhir/resource-types"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ImagingManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy", "http://hl7.org/fhir/resource-types"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization", "http://hl7.org/fhir/resource-types"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationEvaluation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationEvaluation"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ImmunizationEvaluation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide", "http://hl7.org/fhir/resource-types"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("InsurancePlan", "http://hl7.org/fhir/resource-types"), Description("InsurancePlan"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        InsurancePlan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Invoice", "http://hl7.org/fhir/resource-types"), Description("Invoice"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        Invoice,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Library", "http://hl7.org/fhir/resource-types"), Description("Library")]
        Library,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Linkage", "http://hl7.org/fhir/resource-types"), Description("Linkage")]
        Linkage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List", "http://hl7.org/fhir/resource-types"), Description("List")]
        List,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location", "http://hl7.org/fhir/resource-types"), Description("Location")]
        Location,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Measure", "http://hl7.org/fhir/resource-types"), Description("Measure")]
        Measure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MeasureReport", "http://hl7.org/fhir/resource-types"), Description("MeasureReport")]
        MeasureReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media", "http://hl7.org/fhir/resource-types"), Description("Media")]
        Media,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication", "http://hl7.org/fhir/resource-types"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration", "http://hl7.org/fhir/resource-types"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense", "http://hl7.org/fhir/resource-types"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationKnowledge", "http://hl7.org/fhir/resource-types"), Description("MedicationKnowledge"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicationKnowledge,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/resource-types"), Description("MedicationRequest")]
        MedicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProduct", "http://hl7.org/fhir/resource-types"), Description("MedicinalProduct"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProduct,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductAuthorization", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductAuthorization"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductAuthorization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductContraindication", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductContraindication"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductContraindication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductIndication", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductIndication"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductIndication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductIngredient", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductIngredient"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductIngredient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductInteraction", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductInteraction"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductInteraction,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductManufactured", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductManufactured"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductManufactured,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductPackaged", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductPackaged"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductPackaged,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductPharmaceutical", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductPharmaceutical"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductPharmaceutical,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductUndesirableEffect", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductUndesirableEffect"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MedicinalProductUndesirableEffect,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageDefinition", "http://hl7.org/fhir/resource-types"), Description("MessageDefinition")]
        MessageDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MolecularSequence", "http://hl7.org/fhir/resource-types"), Description("MolecularSequence"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        MolecularSequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem", "http://hl7.org/fhir/resource-types"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/resource-types"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation", "http://hl7.org/fhir/resource-types"), Description("Observation")]
        Observation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ObservationDefinition", "http://hl7.org/fhir/resource-types"), Description("ObservationDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ObservationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition", "http://hl7.org/fhir/resource-types"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome", "http://hl7.org/fhir/resource-types"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization", "http://hl7.org/fhir/resource-types"), Description("Organization")]
        Organization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OrganizationAffiliation", "http://hl7.org/fhir/resource-types"), Description("OrganizationAffiliation"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        OrganizationAffiliation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters", "http://hl7.org/fhir/resource-types"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient", "http://hl7.org/fhir/resource-types"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice", "http://hl7.org/fhir/resource-types"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation", "http://hl7.org/fhir/resource-types"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person", "http://hl7.org/fhir/resource-types"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PlanDefinition", "http://hl7.org/fhir/resource-types"), Description("PlanDefinition")]
        PlanDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/resource-types"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PractitionerRole", "http://hl7.org/fhir/resource-types"), Description("PractitionerRole")]
        PractitionerRole,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure", "http://hl7.org/fhir/resource-types"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ProcessResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance", "http://hl7.org/fhir/resource-types"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire", "http://hl7.org/fhir/resource-types"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse", "http://hl7.org/fhir/resource-types"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ReferralRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/resource-types"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RequestGroup", "http://hl7.org/fhir/resource-types"), Description("RequestGroup")]
        RequestGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchDefinition", "http://hl7.org/fhir/resource-types"), Description("ResearchDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ResearchDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchElementDefinition", "http://hl7.org/fhir/resource-types"), Description("ResearchElementDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ResearchElementDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchStudy", "http://hl7.org/fhir/resource-types"), Description("ResearchStudy")]
        ResearchStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchSubject", "http://hl7.org/fhir/resource-types"), Description("ResearchSubject")]
        ResearchSubject,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource", "http://hl7.org/fhir/resource-types"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment", "http://hl7.org/fhir/resource-types"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("RiskEvidenceSynthesis"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        RiskEvidenceSynthesis,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule", "http://hl7.org/fhir/resource-types"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter", "http://hl7.org/fhir/resource-types"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/resource-types"), Description("ServiceRequest"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        ServiceRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Sequence", "http://hl7.org/fhir/resource-types"), Description("Sequence"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition", "http://hl7.org/fhir/resource-types"), Description("ServiceDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.STU3)]
        ServiceDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot", "http://hl7.org/fhir/resource-types"), Description("Slot")]
        Slot,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen", "http://hl7.org/fhir/resource-types"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SpecimenDefinition", "http://hl7.org/fhir/resource-types"), Description("SpecimenDefinition"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SpecimenDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition", "http://hl7.org/fhir/resource-types"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureMap", "http://hl7.org/fhir/resource-types"), Description("StructureMap")]
        StructureMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription", "http://hl7.org/fhir/resource-types"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance", "http://hl7.org/fhir/resource-types"), Description("Substance")]
        Substance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceNucleicAcid", "http://hl7.org/fhir/resource-types"), Description("SubstanceNucleicAcid"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceNucleicAcid,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstancePolymer", "http://hl7.org/fhir/resource-types"), Description("SubstancePolymer"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstancePolymer,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceProtein", "http://hl7.org/fhir/resource-types"), Description("SubstanceProtein"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceProtein,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceReferenceInformation", "http://hl7.org/fhir/resource-types"), Description("SubstanceReferenceInformation"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceReferenceInformation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceSourceMaterial", "http://hl7.org/fhir/resource-types"), Description("SubstanceSourceMaterial"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceSourceMaterial,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceSpecification", "http://hl7.org/fhir/resource-types"), Description("SubstanceSpecification"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        SubstanceSpecification,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery", "http://hl7.org/fhir/resource-types"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/resource-types"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Task", "http://hl7.org/fhir/resource-types"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TerminologyCapabilities", "http://hl7.org/fhir/resource-types"), Description("TerminologyCapabilities"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        TerminologyCapabilities,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestReport", "http://hl7.org/fhir/resource-types"), Description("TestReport")]
        TestReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript", "http://hl7.org/fhir/resource-types"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet", "http://hl7.org/fhir/resource-types"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VerificationResult", "http://hl7.org/fhir/resource-types"), Description("VerificationResult"), EnumFhirVersion(FhirVersion = EnumFhirVersion.R4)]
        VerificationResult,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/abstract-types)
        /// </summary>
        [EnumLiteral("Type", "http://hl7.org/fhir/abstract-types"), Description("Type")]
        Type,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/abstract-types)
        /// </summary>
        [EnumLiteral("Any", "http://hl7.org/fhir/abstract-types"), Description("Any")]
        Any,
    }
}
