using Hl7.Fhir.Utility;
/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

//
// Generated on Tue, Sep 1, 2015 21:04+1000 for FHIR v1.0.0
//
namespace Hl7.Fhir.Model
{

    /// <summary>
    /// The lifecycle status of a Value Set or Concept Map.
    /// (url: http://hl7.org/fhir/ValueSet/publication-status)
    /// </summary>
    [FhirEnumeration("PublicationStatus")]
    public enum PublicationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("draft"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("active"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("retired"), Description("Retired")]
        Retired,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("unknown"), Description("Unknown")]
        Unknown,
    }

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
        [EnumLiteral("Account"), Description("Account")]
        Account,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ActivityDefinition"), Description("ActivityDefinition")]
        ActivityDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AdverseEvent"), Description("AdverseEvent")]
        AdverseEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic"), Description("Basic")]
        Basic,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary"), Description("Binary")]
        Binary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite"), Description("BodySite")]
        BodySite,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CapabilityStatement"), Description("CapabilityStatement")]
        CapabilityStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CareTeam"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItem"), Description("ChargeItem")]
        ChargeItem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CodeSystem"), Description("CodeSystem")]
        CodeSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication"), Description("Communication")]
        Communication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CompartmentDefinition"), Description("CompartmentDefinition")]
        CompartmentDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition"), Description("Composition")]
        Composition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Consent"), Description("Consent")]
        Consent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement"), Description("DataElement")]
        DataElement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device"), Description("Device")]
        Device,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent"), Description("DeviceComponent")]
        DeviceComponent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceRequest"), Description("DeviceRequest")]
        DeviceRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse"), Description("EligibilityResponse")]
        EligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Endpoint"), Description("Endpoint")]
        Endpoint,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExpansionProfile"), Description("ExpansionProfile")]
        ExpansionProfile,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag"), Description("Flag")]
        Flag,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal"), Description("Goal")]
        Goal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GraphDefinition"), Description("GraphDefinition")]
        GraphDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GuidanceResponse"), Description("GuidanceResponse")]
        GuidanceResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingManifest"), Description("ImagingManifest")]
        ImagingManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Library"), Description("Library")]
        Library,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Linkage"), Description("Linkage")]
        Linkage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List"), Description("List")]
        List,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location"), Description("Location")]
        Location,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Measure"), Description("Measure")]
        Measure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MeasureReport"), Description("MeasureReport")]
        MeasureReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media"), Description("Media")]
        Media,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationRequest"), Description("MedicationRequest")]
        MedicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageDefinition"), Description("MessageDefinition")]
        MessageDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation"), Description("Observation")]
        Observation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization"), Description("Organization")]
        Organization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PlanDefinition"), Description("PlanDefinition")]
        PlanDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PractitionerRole"), Description("PractitionerRole")]
        PractitionerRole,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse"), Description("ProcessResponse")]
        ProcessResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest"), Description("ReferralRequest")]
        ReferralRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RequestGroup"), Description("RequestGroup")]
        RequestGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchStudy"), Description("ResearchStudy")]
        ResearchStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchSubject"), Description("ResearchSubject")]
        ResearchSubject,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Sequence"), Description("Sequence")]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition"), Description("ServiceDefinition")]
        ServiceDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot"), Description("Slot")]
        Slot,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureMap"), Description("StructureMap")]
        StructureMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance"), Description("Substance")]
        Substance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Task"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestReport"), Description("TestReport")]
        TestReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription"), Description("VisionPrescription")]
        VisionPrescription,
    }

    /// <summary>
    /// The type of participant for the action
    /// (url: http://hl7.org/fhir/ValueSet/action-participant-type)
    /// </summary>
    [FhirEnumeration("ActionParticipantType")]
    public enum ActionParticipantType
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("patient"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("practitioner"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("related-person"), Description("Related Person")]
        RelatedPerson,
    }

    /// <summary>
    /// The Participation status of an appointment.
    /// (url: http://hl7.org/fhir/ValueSet/participationstatus)
    /// </summary>
    [FhirEnumeration("ParticipationStatus")]
    public enum ParticipationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("accepted"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("declined"), Description("Declined")]
        Declined,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("tentative"), Description("Tentative")]
        Tentative,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("needs-action"), Description("Needs Action")]
        NeedsAction,
    }

    /// <summary>
    /// Data types allowed to be used for search parameters.
    /// (url: http://hl7.org/fhir/ValueSet/search-param-type)
    /// </summary>
    [FhirEnumeration("SearchParamType")]
    public enum SearchParamType
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("number"), Description("Number")]
        Number,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("date"), Description("Date/DateTime")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("string"), Description("String")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("token"), Description("Token")]
        Token,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("reference"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("composite"), Description("Composite")]
        Composite,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("quantity"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("uri"), Description("URI")]
        Uri,
    }

    /// <summary>
    /// The impact of the content of a message.
    /// (url: http://hl7.org/fhir/ValueSet/message-significance-category)
    /// </summary>
    [FhirEnumeration("MessageSignificanceCategory")]
    public enum MessageSignificanceCategory
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Consequence"), Description("Consequence")]
        Consequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Currency"), Description("Currency")]
        Currency,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Notification"), Description("Notification")]
        Notification,
    }

    /// <summary>
    /// This value set includes Status codes.
    /// (url: http://hl7.org/fhir/ValueSet/fm-status)
    /// </summary>
    [FhirEnumeration("FinancialResourceStatusCodes")]
    public enum FinancialResourceStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("active"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("cancelled"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("draft"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("entered-in-error"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The kind of operation to perform as a part of a property based filter.
    /// (url: http://hl7.org/fhir/ValueSet/filter-operator)
    /// </summary>
    [FhirEnumeration("FilterOperator")]
    public enum FilterOperator
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("="), Description("Equals")]
        Equal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-a"), Description("Is A (by subsumption)")]
        IsA,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("descendent-of"), Description("Descendent Of (by subsumption)")]
        DescendentOf,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-not-a"), Description("Not (Is A) (by subsumption)")]
        IsNotA,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("regex"), Description("Regular Expression")]
        Regex,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("in"), Description("In Set")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("not-in"), Description("Not in Set")]
        NotIn,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("generalizes"), Description("Generalizes (by Subsumption)")]
        Generalizes,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("exists"), Description("Exists")]
        Exists,
    }

    /// <summary>
    /// Codes identifying the stage lifecycle stage of a event
    /// (url: http://hl7.org/fhir/ValueSet/event-status)
    /// </summary>
    [FhirEnumeration("EventStatus")]
    public enum EventStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("preparation"), Description("Preparation")]
        Preparation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("in-progress"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("suspended"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("aborted"), Description("Aborted")]
        Aborted,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("completed"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("entered-in-error"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("unknown"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// Codes identifying the stage lifecycle stage of a request
    /// (url: http://hl7.org/fhir/ValueSet/request-status)
    /// </summary>
    [FhirEnumeration("RequestStatus")]
    public enum RequestStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("draft"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("active"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("suspended"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("cancelled"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("completed"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("entered-in-error"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("unknown"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The clinical priority of a diagnostic order.
    /// (url: http://hl7.org/fhir/ValueSet/request-priority)
    /// </summary>
    [FhirEnumeration("RequestPriority")]
    public enum RequestPriority
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("routine"), Description("Routine")]
        Routine,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("urgent"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("asap"), Description("ASAP")]
        Asap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("stat"), Description("STAT")]
        Stat,
    }

    /// <summary>
    /// Which compartment a compartment definition describes
    /// (url: http://hl7.org/fhir/ValueSet/compartment-type)
    /// </summary>
    [FhirEnumeration("CompartmentType")]
    public enum CompartmentType
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Patient"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Encounter"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("RelatedPerson"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Practitioner"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Device"), Description("Device")]
        Device,
    }

    /// <summary>
    /// The workflow/clinical status of the composition.
    /// (url: http://hl7.org/fhir/ValueSet/composition-status)
    /// </summary>
    [FhirEnumeration("CompositionStatus")]
    public enum CompositionStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("preliminary"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("final"), Description("Final")]
        Final,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("amended"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("entered-in-error"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The type of relationship between documents.
    /// (url: http://hl7.org/fhir/ValueSet/document-relationship-type)
    /// </summary>
    [FhirEnumeration("DocumentRelationshipType")]
    public enum DocumentRelationshipType
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("replaces"), Description("Replaces")]
        Replaces,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("transforms"), Description("Transforms")]
        Transforms,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("signs"), Description("Signs")]
        Signs,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("appends"), Description("Appends")]
        Appends,
    }

    /// <summary>
    /// The processing mode that applies to this list
    /// (url: http://hl7.org/fhir/ValueSet/list-mode)
    /// </summary>
    [FhirEnumeration("ListMode")]
    public enum ListMode
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("working"), Description("Working List")]
        Working,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("snapshot"), Description("Snapshot List")]
        Snapshot,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("changes"), Description("Change List")]
        Changes,
    }

    /// <summary>
    /// Codes providing the status of an observation.
    /// (url: http://hl7.org/fhir/ValueSet/observation-status)
    /// </summary>
    [FhirEnumeration("ObservationStatus")]
    public enum ObservationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("registered"), Description("Registered")]
        Registered,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("preliminary"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("final"), Description("Final")]
        Final,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("amended"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("corrected"), Description("Corrected")]
        Corrected,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("cancelled"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("entered-in-error"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("unknown"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The status of the document reference.
    /// (url: http://hl7.org/fhir/ValueSet/document-reference-status)
    /// </summary>
    [FhirEnumeration("DocumentReferenceStatus")]
    public enum DocumentReferenceStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("current"), Description("Current")]
        Current,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("superseded"), Description("Superseded")]
        Superseded,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("entered-in-error"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The gender of a person used for administrative purposes.
    /// (url: http://hl7.org/fhir/ValueSet/administrative-gender)
    /// </summary>
    [FhirEnumeration("AdministrativeGender")]
    public enum AdministrativeGender
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("male"), Description("Male")]
        Male,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("female"), Description("Female")]
        Female,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("other"), Description("Other")]
        Other,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("unknown"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The days of the week.
    /// (url: http://hl7.org/fhir/ValueSet/days-of-week)
    /// </summary>
    [FhirEnumeration("DaysOfWeek")]
    public enum DaysOfWeek
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("mon"), Description("Monday")]
        Mon,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("tue"), Description("Tuesday")]
        Tue,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("wed"), Description("Wednesday")]
        Wed,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("thu"), Description("Thursday")]
        Thu,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("fri"), Description("Friday")]
        Fri,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("sat"), Description("Saturday")]
        Sat,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("sun"), Description("Sunday")]
        Sun,
    }

    /// <summary>
    /// Whether an operation parameter is an input or an output parameter.
    /// (url: http://hl7.org/fhir/ValueSet/operation-parameter-use)
    /// </summary>
    [FhirEnumeration("OperationParameterUse")]
    public enum OperationParameterUse
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/operation-parameter-use)
        /// </summary>
        [EnumLiteral("in"), Description("In")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/operation-parameter-use)
        /// </summary>
        [EnumLiteral("out"), Description("Out")]
        Out,
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
        [EnumLiteral("Address"), Description("Address")]
        Address,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Age"), Description("Age")]
        Age,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Annotation"), Description("Annotation")]
        Annotation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Attachment"), Description("Attachment")]
        Attachment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("BackboneElement"), Description("BackboneElement")]
        BackboneElement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("CodeableConcept"), Description("CodeableConcept")]
        CodeableConcept,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Coding"), Description("Coding")]
        Coding,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ContactDetail"), Description("ContactDetail")]
        ContactDetail,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ContactPoint"), Description("ContactPoint")]
        ContactPoint,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Contributor"), Description("Contributor")]
        Contributor,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Count"), Description("Count")]
        Count,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("DataRequirement"), Description("DataRequirement")]
        DataRequirement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Distance"), Description("Distance")]
        Distance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Dosage"), Description("Dosage")]
        Dosage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Duration"), Description("Duration")]
        Duration,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Element"), Description("Element")]
        Element,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ElementDefinition"), Description("ElementDefinition")]
        ElementDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Extension"), Description("Extension")]
        Extension,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("HumanName"), Description("HumanName")]
        HumanName,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Identifier"), Description("Identifier")]
        Identifier,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Meta"), Description("Meta")]
        Meta,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Money"), Description("Money")]
        Money,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Narrative"), Description("Narrative")]
        Narrative,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ParameterDefinition"), Description("ParameterDefinition")]
        ParameterDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Period"), Description("Period")]
        Period,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Quantity"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Range"), Description("Range")]
        Range,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Ratio"), Description("Ratio")]
        Ratio,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Reference"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("RelatedArtifact"), Description("RelatedArtifact")]
        RelatedArtifact,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SampledData"), Description("SampledData")]
        SampledData,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Signature"), Description("Signature")]
        Signature,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SimpleQuantity"), Description("SimpleQuantity")]
        SimpleQuantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Timing"), Description("Timing")]
        Timing,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("TriggerDefinition"), Description("TriggerDefinition")]
        TriggerDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("UsageContext"), Description("UsageContext")]
        UsageContext,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("base64Binary"), Description("base64Binary")]
        Base64Binary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("boolean"), Description("boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("code"), Description("code")]
        Code,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("date"), Description("date")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("dateTime"), Description("dateTime")]
        DateTime,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("decimal"), Description("decimal")]
        Decimal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("id"), Description("id")]
        Id,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("instant"), Description("instant")]
        Instant,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("integer"), Description("integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("markdown"), Description("markdown")]
        Markdown,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("oid"), Description("oid")]
        Oid,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("positiveInt"), Description("positiveInt")]
        PositiveInt,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("string"), Description("string")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("time"), Description("time")]
        Time,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("unsignedInt"), Description("unsignedInt")]
        UnsignedInt,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uri"), Description("uri")]
        Uri,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uuid"), Description("uuid")]
        Uuid,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("xhtml"), Description("XHTML")]
        Xhtml,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Account"), Description("Account")]
        Account,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ActivityDefinition"), Description("ActivityDefinition")]
        ActivityDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AdverseEvent"), Description("AdverseEvent")]
        AdverseEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic"), Description("Basic")]
        Basic,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary"), Description("Binary")]
        Binary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite"), Description("BodySite")]
        BodySite,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CapabilityStatement"), Description("CapabilityStatement")]
        CapabilityStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CareTeam"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItem"), Description("ChargeItem")]
        ChargeItem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CodeSystem"), Description("CodeSystem")]
        CodeSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication"), Description("Communication")]
        Communication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CompartmentDefinition"), Description("CompartmentDefinition")]
        CompartmentDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition"), Description("Composition")]
        Composition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Consent"), Description("Consent")]
        Consent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement"), Description("DataElement")]
        DataElement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device"), Description("Device")]
        Device,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent"), Description("DeviceComponent")]
        DeviceComponent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceRequest"), Description("DeviceRequest")]
        DeviceRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse"), Description("EligibilityResponse")]
        EligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Endpoint"), Description("Endpoint")]
        Endpoint,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExpansionProfile"), Description("ExpansionProfile")]
        ExpansionProfile,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag"), Description("Flag")]
        Flag,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal"), Description("Goal")]
        Goal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GraphDefinition"), Description("GraphDefinition")]
        GraphDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("GuidanceResponse"), Description("GuidanceResponse")]
        GuidanceResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingManifest"), Description("ImagingManifest")]
        ImagingManifest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Library"), Description("Library")]
        Library,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Linkage"), Description("Linkage")]
        Linkage,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List"), Description("List")]
        List,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location"), Description("Location")]
        Location,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Measure"), Description("Measure")]
        Measure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MeasureReport"), Description("MeasureReport")]
        MeasureReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media"), Description("Media")]
        Media,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationRequest"), Description("MedicationRequest")]
        MedicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageDefinition"), Description("MessageDefinition")]
        MessageDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation"), Description("Observation")]
        Observation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization"), Description("Organization")]
        Organization,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PlanDefinition"), Description("PlanDefinition")]
        PlanDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PractitionerRole"), Description("PractitionerRole")]
        PractitionerRole,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse"), Description("ProcessResponse")]
        ProcessResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest"), Description("ReferralRequest")]
        ReferralRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RequestGroup"), Description("RequestGroup")]
        RequestGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchStudy"), Description("ResearchStudy")]
        ResearchStudy,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchSubject"), Description("ResearchSubject")]
        ResearchSubject,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Sequence"), Description("Sequence")]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition"), Description("ServiceDefinition")]
        ServiceDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot"), Description("Slot")]
        Slot,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureMap"), Description("StructureMap")]
        StructureMap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance"), Description("Substance")]
        Substance,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Task"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestReport"), Description("TestReport")]
        TestReport,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription"), Description("VisionPrescription")]
        VisionPrescription,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/abstract-types)
        /// </summary>
        [EnumLiteral("Type"), Description("Type")]
        Type,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/abstract-types)
        /// </summary>
        [EnumLiteral("Any"), Description("Any")]
        Any,
    }

    /// <summary>
    /// Indication of the degree of conformance expectations associated with a binding.
    /// (url: http://hl7.org/fhir/ValueSet/binding-strength)
    /// </summary>
    [FhirEnumeration("BindingStrength")]
    public enum BindingStrength
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("required"), Description("Required")]
        Required,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("extensible"), Description("Extensible")]
        Extensible,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("preferred"), Description("Preferred")]
        Preferred,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("example"), Description("Example")]
        Example,
    }

    /// <summary>
    /// Defines the kinds of conditions that can appear on actions
    /// (url: http://hl7.org/fhir/ValueSet/action-condition-kind)
    /// </summary>
    [FhirEnumeration("ActionConditionKind")]
    public enum ActionConditionKind
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("applicability"), Description("Applicability")]
        Applicability,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("start"), Description("Start")]
        Start,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("stop"), Description("Stop")]
        Stop,
    }

    /// <summary>
    /// Defines the types of relationships between actions
    /// (url: http://hl7.org/fhir/ValueSet/action-relationship-type)
    /// </summary>
    [FhirEnumeration("ActionRelationshipType")]
    public enum ActionRelationshipType
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before-start"), Description("Before Start")]
        BeforeStart,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before"), Description("Before")]
        Before,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before-end"), Description("Before End")]
        BeforeEnd,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent-with-start"), Description("Concurrent With Start")]
        ConcurrentWithStart,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent"), Description("Concurrent")]
        Concurrent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent-with-end"), Description("Concurrent With End")]
        ConcurrentWithEnd,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after-start"), Description("After Start")]
        AfterStart,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after"), Description("After")]
        After,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after-end"), Description("After End")]
        AfterEnd,
    }

    /// <summary>
    /// Defines organization behavior of a group
    /// (url: http://hl7.org/fhir/ValueSet/action-grouping-behavior)
    /// </summary>
    [FhirEnumeration("ActionGroupingBehavior")]
    public enum ActionGroupingBehavior
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("visual-group"), Description("Visual Group")]
        VisualGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("logical-group"), Description("Logical Group")]
        LogicalGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("sentence-group"), Description("Sentence Group")]
        SentenceGroup,
    }

    /// <summary>
    /// Defines selection behavior of a group
    /// (url: http://hl7.org/fhir/ValueSet/action-selection-behavior)
    /// </summary>
    [FhirEnumeration("ActionSelectionBehavior")]
    public enum ActionSelectionBehavior
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("any"), Description("Any")]
        Any,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("all"), Description("All")]
        All,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("all-or-none"), Description("All Or None")]
        AllOrNone,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("exactly-one"), Description("Exactly One")]
        ExactlyOne,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("at-most-one"), Description("At Most One")]
        AtMostOne,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("one-or-more"), Description("One Or More")]
        OneOrMore,
    }

    /// <summary>
    /// Defines requiredness behavior for selecting an action or an action group
    /// (url: http://hl7.org/fhir/ValueSet/action-required-behavior)
    /// </summary>
    [FhirEnumeration("ActionRequiredBehavior")]
    public enum ActionRequiredBehavior
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("must"), Description("Must")]
        Must,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("could"), Description("Could")]
        Could,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("must-unless-documented"), Description("Must Unless Documented")]
        MustUnlessDocumented,
    }

    /// <summary>
    /// Defines selection frequency behavior for an action or group
    /// (url: http://hl7.org/fhir/ValueSet/action-precheck-behavior)
    /// </summary>
    [FhirEnumeration("ActionPrecheckBehavior")]
    public enum ActionPrecheckBehavior
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-precheck-behavior)
        /// </summary>
        [EnumLiteral("yes"), Description("Yes")]
        Yes,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-precheck-behavior)
        /// </summary>
        [EnumLiteral("no"), Description("No")]
        No,
    }

    /// <summary>
    /// Defines behavior for an action or a group for how many times that item may be repeated
    /// (url: http://hl7.org/fhir/ValueSet/action-cardinality-behavior)
    /// </summary>
    [FhirEnumeration("ActionCardinalityBehavior")]
    public enum ActionCardinalityBehavior
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-cardinality-behavior)
        /// </summary>
        [EnumLiteral("single"), Description("Single")]
        Single,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-cardinality-behavior)
        /// </summary>
        [EnumLiteral("multiple"), Description("Multiple")]
        Multiple,
    }

    /// <summary>
    /// Codes indicating the degree of authority/intentionality associated with a request
    /// (url: http://hl7.org/fhir/ValueSet/request-intent)
    /// </summary>
    [FhirEnumeration("RequestIntent")]
    public enum RequestIntent
    {
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("proposal"), Description("Proposal")]
        Proposal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("plan"), Description("Plan")]
        Plan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("order"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("original-order"), Description("Original Order")]
        OriginalOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("reflex-order"), Description("Reflex Order")]
        ReflexOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("filler-order"), Description("Filler Order")]
        FillerOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("instance-order"), Description("Instance Order")]
        InstanceOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("option"), Description("Option")]
        Option,
    }

}
