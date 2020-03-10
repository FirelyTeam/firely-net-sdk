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
        [EnumLiteral("draft", "http://hl7.org/fhir/publication-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/publication-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("retired", "http://hl7.org/fhir/publication-status"), Description("Retired")]
        Retired,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/publication-status"), Description("Unknown")]
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
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite")]
        BodySite,
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
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
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
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement")]
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
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent")]
        DeviceComponent,
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
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse")]
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
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile")]
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
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest")]
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
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse")]
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
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest")]
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
        [EnumLiteral("Sequence", "http://hl7.org/fhir/resource-types"), Description("Sequence")]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition", "http://hl7.org/fhir/resource-types"), Description("ServiceDefinition")]
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
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
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
        [EnumLiteral("patient", "http://hl7.org/fhir/action-participant-type"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("practitioner", "http://hl7.org/fhir/action-participant-type"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("related-person", "http://hl7.org/fhir/action-participant-type"), Description("Related Person")]
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
        [EnumLiteral("accepted", "http://hl7.org/fhir/participationstatus"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("declined", "http://hl7.org/fhir/participationstatus"), Description("Declined")]
        Declined,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("tentative", "http://hl7.org/fhir/participationstatus"), Description("Tentative")]
        Tentative,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("needs-action", "http://hl7.org/fhir/participationstatus"), Description("Needs Action")]
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
        [EnumLiteral("number", "http://hl7.org/fhir/search-param-type"), Description("Number")]
        Number,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/search-param-type"), Description("Date/DateTime")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/search-param-type"), Description("String")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("token", "http://hl7.org/fhir/search-param-type"), Description("Token")]
        Token,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/search-param-type"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("composite", "http://hl7.org/fhir/search-param-type"), Description("Composite")]
        Composite,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("quantity", "http://hl7.org/fhir/search-param-type"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("uri", "http://hl7.org/fhir/search-param-type"), Description("URI")]
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
        [EnumLiteral("Consequence", "http://hl7.org/fhir/message-significance-category"), Description("Consequence")]
        Consequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Currency", "http://hl7.org/fhir/message-significance-category"), Description("Currency")]
        Currency,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Notification", "http://hl7.org/fhir/message-significance-category"), Description("Notification")]
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
        [EnumLiteral("active", "http://hl7.org/fhir/fm-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/fm-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/fm-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/fm-status"), Description("Entered in Error")]
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
        [EnumLiteral("=", "http://hl7.org/fhir/filter-operator"), Description("Equals")]
        Equal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-a", "http://hl7.org/fhir/filter-operator"), Description("Is A (by subsumption)")]
        IsA,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("descendent-of", "http://hl7.org/fhir/filter-operator"), Description("Descendent Of (by subsumption)")]
        DescendentOf,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-not-a", "http://hl7.org/fhir/filter-operator"), Description("Not (Is A) (by subsumption)")]
        IsNotA,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("regex", "http://hl7.org/fhir/filter-operator"), Description("Regular Expression")]
        Regex,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/filter-operator"), Description("In Set")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("not-in", "http://hl7.org/fhir/filter-operator"), Description("Not in Set")]
        NotIn,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("generalizes", "http://hl7.org/fhir/filter-operator"), Description("Generalizes (by Subsumption)")]
        Generalizes,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("exists", "http://hl7.org/fhir/filter-operator"), Description("Exists")]
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
        [EnumLiteral("preparation", "http://hl7.org/fhir/event-status"), Description("Preparation")]
        Preparation,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/event-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/event-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/event-status"), Description("Aborted")]
        Aborted,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/event-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/event-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/event-status"), Description("Unknown")]
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
        [EnumLiteral("draft", "http://hl7.org/fhir/request-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/request-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/request-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/request-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/request-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/request-status"), Description("Unknown")]
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
        [EnumLiteral("routine", "http://hl7.org/fhir/request-priority"), Description("Routine")]
        Routine,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("urgent", "http://hl7.org/fhir/request-priority"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("asap", "http://hl7.org/fhir/request-priority"), Description("ASAP")]
        Asap,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("stat", "http://hl7.org/fhir/request-priority"), Description("STAT")]
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
        [EnumLiteral("Patient", "http://hl7.org/fhir/compartment-type"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/compartment-type"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/compartment-type"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/compartment-type"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/compartment-type"), Description("Device")]
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
        [EnumLiteral("preliminary", "http://hl7.org/fhir/composition-status"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/composition-status"), Description("Final")]
        Final,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/composition-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/composition-status"), Description("Entered in Error")]
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
        [EnumLiteral("replaces", "http://hl7.org/fhir/document-relationship-type"), Description("Replaces")]
        Replaces,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("transforms", "http://hl7.org/fhir/document-relationship-type"), Description("Transforms")]
        Transforms,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("signs", "http://hl7.org/fhir/document-relationship-type"), Description("Signs")]
        Signs,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("appends", "http://hl7.org/fhir/document-relationship-type"), Description("Appends")]
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
        [EnumLiteral("working", "http://hl7.org/fhir/list-mode"), Description("Working List")]
        Working,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("snapshot", "http://hl7.org/fhir/list-mode"), Description("Snapshot List")]
        Snapshot,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("changes", "http://hl7.org/fhir/list-mode"), Description("Change List")]
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
        [EnumLiteral("registered", "http://hl7.org/fhir/observation-status"), Description("Registered")]
        Registered,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("preliminary", "http://hl7.org/fhir/observation-status"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/observation-status"), Description("Final")]
        Final,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/observation-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("corrected", "http://hl7.org/fhir/observation-status"), Description("Corrected")]
        Corrected,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/observation-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/observation-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/observation-status"), Description("Unknown")]
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
        [EnumLiteral("current", "http://hl7.org/fhir/document-reference-status"), Description("Current")]
        Current,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("superseded", "http://hl7.org/fhir/document-reference-status"), Description("Superseded")]
        Superseded,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/document-reference-status"), Description("Entered in Error")]
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
        [EnumLiteral("male", "http://hl7.org/fhir/administrative-gender"), Description("Male")]
        Male,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("female", "http://hl7.org/fhir/administrative-gender"), Description("Female")]
        Female,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/administrative-gender"), Description("Other")]
        Other,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/administrative-gender"), Description("Unknown")]
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
        [EnumLiteral("mon", "http://hl7.org/fhir/days-of-week"), Description("Monday")]
        Mon,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("tue", "http://hl7.org/fhir/days-of-week"), Description("Tuesday")]
        Tue,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("wed", "http://hl7.org/fhir/days-of-week"), Description("Wednesday")]
        Wed,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("thu", "http://hl7.org/fhir/days-of-week"), Description("Thursday")]
        Thu,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("fri", "http://hl7.org/fhir/days-of-week"), Description("Friday")]
        Fri,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("sat", "http://hl7.org/fhir/days-of-week"), Description("Saturday")]
        Sat,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("sun", "http://hl7.org/fhir/days-of-week"), Description("Sunday")]
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
        [EnumLiteral("in", "http://hl7.org/fhir/operation-parameter-use"), Description("In")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/operation-parameter-use)
        /// </summary>
        [EnumLiteral("out", "http://hl7.org/fhir/operation-parameter-use"), Description("Out")]
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
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite")]
        BodySite,
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
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
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
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement")]
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
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent")]
        DeviceComponent,
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
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse")]
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
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile")]
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
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest")]
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
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse")]
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
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest")]
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
        [EnumLiteral("Sequence", "http://hl7.org/fhir/resource-types"), Description("Sequence")]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition", "http://hl7.org/fhir/resource-types"), Description("ServiceDefinition")]
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
        [EnumLiteral("required", "http://hl7.org/fhir/binding-strength"), Description("Required")]
        Required,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("extensible", "http://hl7.org/fhir/binding-strength"), Description("Extensible")]
        Extensible,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("preferred", "http://hl7.org/fhir/binding-strength"), Description("Preferred")]
        Preferred,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("example", "http://hl7.org/fhir/binding-strength"), Description("Example")]
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
        [EnumLiteral("applicability", "http://hl7.org/fhir/action-condition-kind"), Description("Applicability")]
        Applicability,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("start", "http://hl7.org/fhir/action-condition-kind"), Description("Start")]
        Start,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("stop", "http://hl7.org/fhir/action-condition-kind"), Description("Stop")]
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
        [EnumLiteral("before-start", "http://hl7.org/fhir/action-relationship-type"), Description("Before Start")]
        BeforeStart,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before", "http://hl7.org/fhir/action-relationship-type"), Description("Before")]
        Before,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before-end", "http://hl7.org/fhir/action-relationship-type"), Description("Before End")]
        BeforeEnd,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent-with-start", "http://hl7.org/fhir/action-relationship-type"), Description("Concurrent With Start")]
        ConcurrentWithStart,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent", "http://hl7.org/fhir/action-relationship-type"), Description("Concurrent")]
        Concurrent,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent-with-end", "http://hl7.org/fhir/action-relationship-type"), Description("Concurrent With End")]
        ConcurrentWithEnd,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after-start", "http://hl7.org/fhir/action-relationship-type"), Description("After Start")]
        AfterStart,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after", "http://hl7.org/fhir/action-relationship-type"), Description("After")]
        After,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after-end", "http://hl7.org/fhir/action-relationship-type"), Description("After End")]
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
        [EnumLiteral("visual-group", "http://hl7.org/fhir/action-grouping-behavior"), Description("Visual Group")]
        VisualGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("logical-group", "http://hl7.org/fhir/action-grouping-behavior"), Description("Logical Group")]
        LogicalGroup,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("sentence-group", "http://hl7.org/fhir/action-grouping-behavior"), Description("Sentence Group")]
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
        [EnumLiteral("any", "http://hl7.org/fhir/action-selection-behavior"), Description("Any")]
        Any,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("all", "http://hl7.org/fhir/action-selection-behavior"), Description("All")]
        All,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("all-or-none", "http://hl7.org/fhir/action-selection-behavior"), Description("All Or None")]
        AllOrNone,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("exactly-one", "http://hl7.org/fhir/action-selection-behavior"), Description("Exactly One")]
        ExactlyOne,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("at-most-one", "http://hl7.org/fhir/action-selection-behavior"), Description("At Most One")]
        AtMostOne,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("one-or-more", "http://hl7.org/fhir/action-selection-behavior"), Description("One Or More")]
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
        [EnumLiteral("must", "http://hl7.org/fhir/action-required-behavior"), Description("Must")]
        Must,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("could", "http://hl7.org/fhir/action-required-behavior"), Description("Could")]
        Could,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("must-unless-documented", "http://hl7.org/fhir/action-required-behavior"), Description("Must Unless Documented")]
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
        [EnumLiteral("yes", "http://hl7.org/fhir/action-precheck-behavior"), Description("Yes")]
        Yes,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-precheck-behavior)
        /// </summary>
        [EnumLiteral("no", "http://hl7.org/fhir/action-precheck-behavior"), Description("No")]
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
        [EnumLiteral("single", "http://hl7.org/fhir/action-cardinality-behavior"), Description("Single")]
        Single,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/action-cardinality-behavior)
        /// </summary>
        [EnumLiteral("multiple", "http://hl7.org/fhir/action-cardinality-behavior"), Description("Multiple")]
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
        [EnumLiteral("proposal", "http://hl7.org/fhir/request-intent"), Description("Proposal")]
        Proposal,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("plan", "http://hl7.org/fhir/request-intent"), Description("Plan")]
        Plan,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("order", "http://hl7.org/fhir/request-intent"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("original-order", "http://hl7.org/fhir/request-intent"), Description("Original Order")]
        OriginalOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("reflex-order", "http://hl7.org/fhir/request-intent"), Description("Reflex Order")]
        ReflexOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("filler-order", "http://hl7.org/fhir/request-intent"), Description("Filler Order")]
        FillerOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("instance-order", "http://hl7.org/fhir/request-intent"), Description("Instance Order")]
        InstanceOrder,
        /// <summary>
        /// MISSING DESCRIPTION<br/>
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("option", "http://hl7.org/fhir/request-intent"), Description("Option")]
        Option,
    }

}
