using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
// Generated on Tue, Sep 22, 2015 20:02+1000 for FHIR v1.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The gender of a person used for administrative purposes.
    /// </summary>
    [FhirEnumeration("AdministrativeGender")]
    public enum AdministrativeGender
    {
        /// <summary>
        /// Male
        /// </summary>
        [EnumLiteral("male")]
        Male,
        /// <summary>
        /// Female
        /// </summary>
        [EnumLiteral("female")]
        Female,
        /// <summary>
        /// Other
        /// </summary>
        [EnumLiteral("other")]
        Other,
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumLiteral("unknown")]
        Unknown,
    }
    
    /// <summary>
    /// A valueSet of UCUM codes for representing age value units.
    /// </summary>
    [FhirEnumeration("AgeUnits")]
    public enum AgeUnits
    {
        [EnumLiteral("min")]
        Min,
        [EnumLiteral("h")]
        H,
        [EnumLiteral("d")]
        D,
        [EnumLiteral("wk")]
        Wk,
        [EnumLiteral("mo")]
        Mo,
        [EnumLiteral("a")]
        A,
    }
    
    /// <summary>
    /// Indication of the degree of conformance expectations associated with a binding.
    /// </summary>
    [FhirEnumeration("BindingStrength")]
    public enum BindingStrength
    {
        /// <summary>
        /// To be conformant, instances of this element SHALL include a code from the specified value set.
        /// </summary>
        [EnumLiteral("required")]
        Required,
        /// <summary>
        /// To be conformant, instances of this element SHALL include a code from the specified value set if any of the codes within the value set can apply to the concept being communicated.  If the valueset does not cover the concept (based on human review), alternate codings (or, data type allowing, text) may be included instead.
        /// </summary>
        [EnumLiteral("extensible")]
        Extensible,
        /// <summary>
        /// Instances are encouraged to draw from the specified codes for interoperability purposes but are not required to do so to be considered conformant.
        /// </summary>
        [EnumLiteral("preferred")]
        Preferred,
        /// <summary>
        /// Instances are not expected or even encouraged to draw from the specified value set.  The value set merely provides examples of the types of concepts intended to be included.
        /// </summary>
        [EnumLiteral("example")]
        Example,
    }
    
    /// <summary>
    /// The degree of equivalence between concepts.
    /// </summary>
    [FhirEnumeration("ConceptMapEquivalence")]
    public enum ConceptMapEquivalence
    {
        /// <summary>
        /// The definitions of the concepts mean the same thing (including when structural implications of meaning are considered) (i.e. extensionally identical).
        /// </summary>
        [EnumLiteral("equivalent")]
        Equivalent,
        /// <summary>
        /// The definitions of the concepts are exactly the same (i.e. only grammatical differences) and structural implications of meaning are identical or irrelevant (i.e. intentionally identical).
        /// </summary>
        [EnumLiteral("equal")]
        Equal,
        /// <summary>
        /// The target mapping is wider in meaning than the source concept.
        /// </summary>
        [EnumLiteral("wider")]
        Wider,
        /// <summary>
        /// The target mapping subsumes the meaning of the source concept (e.g. the source is-a target).
        /// </summary>
        [EnumLiteral("subsumes")]
        Subsumes,
        /// <summary>
        /// The target mapping is narrower in meaning that the source concept. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when attempting to use these mappings operationally.
        /// </summary>
        [EnumLiteral("narrower")]
        Narrower,
        /// <summary>
        /// The target mapping specializes the meaning of the source concept (e.g. the target is-a source).
        /// </summary>
        [EnumLiteral("specializes")]
        Specializes,
        /// <summary>
        /// The target mapping overlaps with the source concept, but both source and target cover additional meaning, or the definitions are imprecise and it is uncertain whether they have the same boundaries to their meaning. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when attempting to use these mappings operationally.
        /// </summary>
        [EnumLiteral("inexact")]
        Inexact,
        /// <summary>
        /// There is no match for this concept in the destination concept system.
        /// </summary>
        [EnumLiteral("unmatched")]
        Unmatched,
        /// <summary>
        /// This is an explicit assertion that there is no mapping between the source and target concept.
        /// </summary>
        [EnumLiteral("disjoint")]
        Disjoint,
    }
    
    /// <summary>
    /// The lifecycle status of a Value Set or Concept Map.
    /// </summary>
    [FhirEnumeration("ConformanceResourceStatus")]
    public enum ConformanceResourceStatus
    {
        /// <summary>
        /// This resource is still under development.
        /// </summary>
        [EnumLiteral("draft")]
        Draft,
        /// <summary>
        /// This resource is ready for normal use.
        /// </summary>
        [EnumLiteral("active")]
        Active,
        /// <summary>
        /// This resource has been withdrawn or superseded and should no longer be used.
        /// </summary>
        [EnumLiteral("retired")]
        Retired,
    }
    
    /// <summary>
    /// Used to specify why the normally expected content of the data element is missing.
    /// </summary>
    [FhirEnumeration("DataAbsentReason")]
    public enum DataAbsentReason
    {
        /// <summary>
        /// The value is not known.
        /// </summary>
        [EnumLiteral("unknown")]
        Unknown,
        /// <summary>
        /// The source human does not know the value.
        /// </summary>
        [EnumLiteral("asked")]
        Asked,
        /// <summary>
        /// There is reason to expect (from the workflow) that the value may become known.
        /// </summary>
        [EnumLiteral("temp")]
        Temp,
        /// <summary>
        /// The workflow didn't lead to this value being known.
        /// </summary>
        [EnumLiteral("not-asked")]
        NotAsked,
        /// <summary>
        /// The information is not available due to security, privacy or related reasons.
        /// </summary>
        [EnumLiteral("masked")]
        Masked,
        /// <summary>
        /// The source system wasn't capable of supporting this element.
        /// </summary>
        [EnumLiteral("unsupported")]
        Unsupported,
        /// <summary>
        /// The content of the data is represented in the resource narrative.
        /// </summary>
        [EnumLiteral("astext")]
        Astext,
        /// <summary>
        /// Some system or workflow process error means that the information is not available.
        /// </summary>
        [EnumLiteral("error")]
        Error,
        /// <summary>
        /// NaN, standing for not a number, is a numeric data type value representing an undefined or unrepresentable value.
        /// </summary>
        [EnumLiteral("NaN")]
        NaN,
    }
    
    /// <summary>
    /// The status of the document reference.
    /// </summary>
    [FhirEnumeration("DocumentReferenceStatus")]
    public enum DocumentReferenceStatus
    {
        /// <summary>
        /// This is the current reference for this document.
        /// </summary>
        [EnumLiteral("current")]
        Current,
        /// <summary>
        /// This reference has been superseded by another reference.
        /// </summary>
        [EnumLiteral("superseded")]
        Superseded,
        /// <summary>
        /// This reference was created in error.
        /// </summary>
        [EnumLiteral("entered-in-error")]
        EnteredInError,
    }
    
    /// <summary>
    /// The presentation types of notes.
    /// </summary>
    [FhirEnumeration("NoteType")]
    public enum NoteType
    {
        /// <summary>
        /// Display the note.
        /// </summary>
        [EnumLiteral("display")]
        Display,
        /// <summary>
        /// Print the note on the form.
        /// </summary>
        [EnumLiteral("print")]
        Print,
        /// <summary>
        /// Print the note for the operator.
        /// </summary>
        [EnumLiteral("printoper")]
        Printoper,
    }
    
    /// <summary>
    /// The outcome of the processing.
    /// </summary>
    [FhirEnumeration("RemittanceOutcome")]
    public enum RemittanceOutcome
    {
        /// <summary>
        /// The processing completed without errors.
        /// </summary>
        [EnumLiteral("complete")]
        Complete,
        /// <summary>
        /// The processing identified errors.
        /// </summary>
        [EnumLiteral("error")]
        Error,
    }
    
    /// <summary>
    /// Data types allowed to be used for search parameters.
    /// </summary>
    [FhirEnumeration("SearchParamType")]
    public enum SearchParamType
    {
        /// <summary>
        /// Search parameter SHALL be a number (a whole number, or a decimal).
        /// </summary>
        [EnumLiteral("number")]
        Number,
        /// <summary>
        /// Search parameter is on a date/time. The date format is the standard XML format, though other formats may be supported.
        /// </summary>
        [EnumLiteral("date")]
        Date,
        /// <summary>
        /// Search parameter is a simple string, like a name part. Search is case-insensitive and accent-insensitive. May match just the start of a string. String parameters may contain spaces.
        /// </summary>
        [EnumLiteral("string")]
        String,
        /// <summary>
        /// Search parameter on a coded element or identifier. May be used to search through the text, displayname, code and code/codesystem (for codes) and label, system and key (for identifier). Its value is either a string or a pair of namespace and value, separated by a "|", depending on the modifier used.
        /// </summary>
        [EnumLiteral("token")]
        Token,
        /// <summary>
        /// A reference to another resource.
        /// </summary>
        [EnumLiteral("reference")]
        Reference,
        /// <summary>
        /// A composite search parameter that combines a search on two values together.
        /// </summary>
        [EnumLiteral("composite")]
        Composite,
        /// <summary>
        /// A search parameter that searches on a quantity.
        /// </summary>
        [EnumLiteral("quantity")]
        Quantity,
        /// <summary>
        /// A search parameter that searches on a URI (RFC 3986).
        /// </summary>
        [EnumLiteral("uri")]
        Uri,
    }
    
    /// <summary>
    /// A set of generally useful codes defined so they can be included in value sets.
    /// </summary>
    [FhirEnumeration("SpecialValues")]
    public enum SpecialValues
    {
        /// <summary>
        /// Boolean true.
        /// </summary>
        [EnumLiteral("true")]
        True,
        /// <summary>
        /// Boolean false.
        /// </summary>
        [EnumLiteral("false")]
        False,
        /// <summary>
        /// The content is greater than zero, but too small to be quantified.
        /// </summary>
        [EnumLiteral("trace")]
        Trace,
        /// <summary>
        /// The specific quantity is not known, but is known to be non-zero and is not specified because it makes up the bulk of the material.
        /// </summary>
        [EnumLiteral("sufficient")]
        Sufficient,
        /// <summary>
        /// The value is no longer available.
        /// </summary>
        [EnumLiteral("withdrawn")]
        Withdrawn,
        /// <summary>
        /// The are no known applicable values in this context.
        /// </summary>
        [EnumLiteral("nil-known")]
        NilKnown,
    }
    
    /// <summary>
    /// List of all supported FHIR Resources
    /// </summary>
    [FhirEnumeration("ResourceType")]
    public enum ResourceType
    {
        /// <summary>
        /// The DomainResource resource
        /// </summary>
        [EnumLiteral("DomainResource")]
        DomainResource,
        /// <summary>
        /// The Parameters resource
        /// </summary>
        [EnumLiteral("Parameters")]
        Parameters,
        /// <summary>
        /// The Resource resource
        /// </summary>
        [EnumLiteral("Resource")]
        Resource,
        /// <summary>
        /// The Account resource
        /// </summary>
        [EnumLiteral("Account")]
        Account,
        /// <summary>
        /// The AllergyIntolerance resource
        /// </summary>
        [EnumLiteral("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// The Appointment resource
        /// </summary>
        [EnumLiteral("Appointment")]
        Appointment,
        /// <summary>
        /// The AppointmentResponse resource
        /// </summary>
        [EnumLiteral("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// The AuditEvent resource
        /// </summary>
        [EnumLiteral("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// The Basic resource
        /// </summary>
        [EnumLiteral("Basic")]
        Basic,
        /// <summary>
        /// The Binary resource
        /// </summary>
        [EnumLiteral("Binary")]
        Binary,
        /// <summary>
        /// The BodySite resource
        /// </summary>
        [EnumLiteral("BodySite")]
        BodySite,
        /// <summary>
        /// The Bundle resource
        /// </summary>
        [EnumLiteral("Bundle")]
        Bundle,
        /// <summary>
        /// The CarePlan resource
        /// </summary>
        [EnumLiteral("CarePlan")]
        CarePlan,
        /// <summary>
        /// The Claim resource
        /// </summary>
        [EnumLiteral("Claim")]
        Claim,
        /// <summary>
        /// The ClaimResponse resource
        /// </summary>
        [EnumLiteral("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// The ClinicalImpression resource
        /// </summary>
        [EnumLiteral("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// The Communication resource
        /// </summary>
        [EnumLiteral("Communication")]
        Communication,
        /// <summary>
        /// The CommunicationRequest resource
        /// </summary>
        [EnumLiteral("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// The Composition resource
        /// </summary>
        [EnumLiteral("Composition")]
        Composition,
        /// <summary>
        /// The ConceptMap resource
        /// </summary>
        [EnumLiteral("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// The Condition resource
        /// </summary>
        [EnumLiteral("Condition")]
        Condition,
        /// <summary>
        /// The Conformance resource
        /// </summary>
        [EnumLiteral("Conformance")]
        Conformance,
        /// <summary>
        /// The Contract resource
        /// </summary>
        [EnumLiteral("Contract")]
        Contract,
        /// <summary>
        /// The Coverage resource
        /// </summary>
        [EnumLiteral("Coverage")]
        Coverage,
        /// <summary>
        /// The DataElement resource
        /// </summary>
        [EnumLiteral("DataElement")]
        DataElement,
        /// <summary>
        /// The DetectedIssue resource
        /// </summary>
        [EnumLiteral("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// The Device resource
        /// </summary>
        [EnumLiteral("Device")]
        Device,
        /// <summary>
        /// The DeviceComponent resource
        /// </summary>
        [EnumLiteral("DeviceComponent")]
        DeviceComponent,
        /// <summary>
        /// The DeviceMetric resource
        /// </summary>
        [EnumLiteral("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// The DeviceUseRequest resource
        /// </summary>
        [EnumLiteral("DeviceUseRequest")]
        DeviceUseRequest,
        /// <summary>
        /// The DeviceUseStatement resource
        /// </summary>
        [EnumLiteral("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// The DiagnosticOrder resource
        /// </summary>
        [EnumLiteral("DiagnosticOrder")]
        DiagnosticOrder,
        /// <summary>
        /// The DiagnosticReport resource
        /// </summary>
        [EnumLiteral("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// The DocumentManifest resource
        /// </summary>
        [EnumLiteral("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// The DocumentReference resource
        /// </summary>
        [EnumLiteral("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// The EligibilityRequest resource
        /// </summary>
        [EnumLiteral("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// The EligibilityResponse resource
        /// </summary>
        [EnumLiteral("EligibilityResponse")]
        EligibilityResponse,
        /// <summary>
        /// The Encounter resource
        /// </summary>
        [EnumLiteral("Encounter")]
        Encounter,
        /// <summary>
        /// The EnrollmentRequest resource
        /// </summary>
        [EnumLiteral("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// The EnrollmentResponse resource
        /// </summary>
        [EnumLiteral("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// The EpisodeOfCare resource
        /// </summary>
        [EnumLiteral("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// The ExplanationOfBenefit resource
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// The FamilyMemberHistory resource
        /// </summary>
        [EnumLiteral("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// The Flag resource
        /// </summary>
        [EnumLiteral("Flag")]
        Flag,
        /// <summary>
        /// The Goal resource
        /// </summary>
        [EnumLiteral("Goal")]
        Goal,
        /// <summary>
        /// The Group resource
        /// </summary>
        [EnumLiteral("Group")]
        Group,
        /// <summary>
        /// The HealthcareService resource
        /// </summary>
        [EnumLiteral("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// The ImagingObjectSelection resource
        /// </summary>
        [EnumLiteral("ImagingObjectSelection")]
        ImagingObjectSelection,
        /// <summary>
        /// The ImagingStudy resource
        /// </summary>
        [EnumLiteral("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// The Immunization resource
        /// </summary>
        [EnumLiteral("Immunization")]
        Immunization,
        /// <summary>
        /// The ImmunizationRecommendation resource
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// The ImplementationGuide resource
        /// </summary>
        [EnumLiteral("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// The List resource
        /// </summary>
        [EnumLiteral("List")]
        List,
        /// <summary>
        /// The Location resource
        /// </summary>
        [EnumLiteral("Location")]
        Location,
        /// <summary>
        /// The Media resource
        /// </summary>
        [EnumLiteral("Media")]
        Media,
        /// <summary>
        /// The Medication resource
        /// </summary>
        [EnumLiteral("Medication")]
        Medication,
        /// <summary>
        /// The MedicationAdministration resource
        /// </summary>
        [EnumLiteral("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// The MedicationDispense resource
        /// </summary>
        [EnumLiteral("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// The MedicationOrder resource
        /// </summary>
        [EnumLiteral("MedicationOrder")]
        MedicationOrder,
        /// <summary>
        /// The MedicationStatement resource
        /// </summary>
        [EnumLiteral("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// The MessageHeader resource
        /// </summary>
        [EnumLiteral("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// The NamingSystem resource
        /// </summary>
        [EnumLiteral("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// The NutritionOrder resource
        /// </summary>
        [EnumLiteral("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// The Observation resource
        /// </summary>
        [EnumLiteral("Observation")]
        Observation,
        /// <summary>
        /// The OperationDefinition resource
        /// </summary>
        [EnumLiteral("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// The OperationOutcome resource
        /// </summary>
        [EnumLiteral("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// The Order resource
        /// </summary>
        [EnumLiteral("Order")]
        Order,
        /// <summary>
        /// The OrderResponse resource
        /// </summary>
        [EnumLiteral("OrderResponse")]
        OrderResponse,
        /// <summary>
        /// The Organization resource
        /// </summary>
        [EnumLiteral("Organization")]
        Organization,
        /// <summary>
        /// The Patient resource
        /// </summary>
        [EnumLiteral("Patient")]
        Patient,
        /// <summary>
        /// The PaymentNotice resource
        /// </summary>
        [EnumLiteral("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// The PaymentReconciliation resource
        /// </summary>
        [EnumLiteral("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// The Person resource
        /// </summary>
        [EnumLiteral("Person")]
        Person,
        /// <summary>
        /// The Practitioner resource
        /// </summary>
        [EnumLiteral("Practitioner")]
        Practitioner,
        /// <summary>
        /// The Procedure resource
        /// </summary>
        [EnumLiteral("Procedure")]
        Procedure,
        /// <summary>
        /// The ProcedureRequest resource
        /// </summary>
        [EnumLiteral("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// The ProcessRequest resource
        /// </summary>
        [EnumLiteral("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// The ProcessResponse resource
        /// </summary>
        [EnumLiteral("ProcessResponse")]
        ProcessResponse,
        /// <summary>
        /// The Provenance resource
        /// </summary>
        [EnumLiteral("Provenance")]
        Provenance,
        /// <summary>
        /// The Questionnaire resource
        /// </summary>
        [EnumLiteral("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// The QuestionnaireResponse resource
        /// </summary>
        [EnumLiteral("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// The ReferralRequest resource
        /// </summary>
        [EnumLiteral("ReferralRequest")]
        ReferralRequest,
        /// <summary>
        /// The RelatedPerson resource
        /// </summary>
        [EnumLiteral("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// The RiskAssessment resource
        /// </summary>
        [EnumLiteral("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// The Schedule resource
        /// </summary>
        [EnumLiteral("Schedule")]
        Schedule,
        /// <summary>
        /// The SearchParameter resource
        /// </summary>
        [EnumLiteral("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// The Slot resource
        /// </summary>
        [EnumLiteral("Slot")]
        Slot,
        /// <summary>
        /// The Specimen resource
        /// </summary>
        [EnumLiteral("Specimen")]
        Specimen,
        /// <summary>
        /// The StructureDefinition resource
        /// </summary>
        [EnumLiteral("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// The Subscription resource
        /// </summary>
        [EnumLiteral("Subscription")]
        Subscription,
        /// <summary>
        /// The Substance resource
        /// </summary>
        [EnumLiteral("Substance")]
        Substance,
        /// <summary>
        /// The SupplyDelivery resource
        /// </summary>
        [EnumLiteral("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// The SupplyRequest resource
        /// </summary>
        [EnumLiteral("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// The TestScript resource
        /// </summary>
        [EnumLiteral("TestScript")]
        TestScript,
        /// <summary>
        /// The ValueSet resource
        /// </summary>
        [EnumLiteral("ValueSet")]
        ValueSet,
        /// <summary>
        /// The VisionPrescription resource
        /// </summary>
        [EnumLiteral("VisionPrescription")]
        VisionPrescription,
    }
    
}
