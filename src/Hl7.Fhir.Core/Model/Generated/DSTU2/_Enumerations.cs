using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
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
#pragma warning disable 1591 // suppress XML summary warnings

//
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{

    /// <summary>
    /// The lifecycle status of a Value Set or Concept Map.
    /// (url: http://hl7.org/fhir/ValueSet/conformance-resource-status)
    /// </summary>
    [FhirEnumeration("ConformanceResourceStatus")]
    public enum ConformanceResourceStatus
    {
        /// <summary>
        /// This resource is still under development.
        /// (system: http://hl7.org/fhir/conformance-resource-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/conformance-resource-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// This resource is ready for normal use.
        /// (system: http://hl7.org/fhir/conformance-resource-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/conformance-resource-status"), Description("Active")]
        Active,
        /// <summary>
        /// This resource has been withdrawn or superseded and should no longer be used.
        /// (system: http://hl7.org/fhir/conformance-resource-status)
        /// </summary>
        [EnumLiteral("retired", "http://hl7.org/fhir/conformance-resource-status"), Description("Retired")]
        Retired,
    }

    /// <summary>
    /// The kind of operation to perform as a part of a property based filter.
    /// (url: http://hl7.org/fhir/ValueSet/filter-operator)
    /// </summary>
    [FhirEnumeration("FilterOperator")]
    public enum FilterOperator
    {
        /// <summary>
        /// The specified property of the code equals the provided value.
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("=", "http://hl7.org/fhir/filter-operator"), Description("Equals")]
        Equal,
        /// <summary>
        /// Includes all concept ids that have a transitive is-a relationship with the concept Id provided as the value, including the provided concept itself.
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-a", "http://hl7.org/fhir/filter-operator"), Description("Is A (by subsumption)")]
        IsA,
        /// <summary>
        /// The specified property of the code does not have an is-a relationship with the provided value.
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-not-a", "http://hl7.org/fhir/filter-operator"), Description("Not (Is A) (by subsumption)")]
        IsNotA,
        /// <summary>
        /// The specified property of the code  matches the regex specified in the provided value.
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("regex", "http://hl7.org/fhir/filter-operator"), Description("Regular Expression")]
        Regex,
        /// <summary>
        /// The specified property of the code is in the set of codes or concepts specified in the provided value (comma separated list).
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/filter-operator"), Description("In Set")]
        In,
        /// <summary>
        /// The specified property of the code is not in the set of codes or concepts specified in the provided value (comma separated list).
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("not-in", "http://hl7.org/fhir/filter-operator"), Description("Not in Set")]
        NotIn,
    }

    /// <summary>
    /// Assertion about certainty associated with a propensity, or potential risk, of a reaction to the identified Substance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-status)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceStatus")]
    public enum AllergyIntoleranceStatus
    {
        /// <summary>
        /// An active record of a reaction to the identified Substance.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Active")]
        Active,
        /// <summary>
        /// A low level of certainty about the propensity for a reaction to the identified Substance.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("unconfirmed", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Unconfirmed")]
        Unconfirmed,
        /// <summary>
        /// A high level of certainty about the propensity for a reaction to the identified Substance, which may include clinical evidence by testing or rechallenge.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("confirmed", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Confirmed")]
        Confirmed,
        /// <summary>
        /// An inactive record of a reaction to the identified Substance.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// A reaction to the identified Substance has been clinically reassessed by testing or rechallenge and considered to be resolved.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("resolved", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Resolved")]
        Resolved,
        /// <summary>
        /// A propensity for a reaction to the identified Substance has been disproven with a high level of clinical certainty, which may include testing or rechallenge, and is refuted.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("refuted", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Refuted")]
        Refuted,
        /// <summary>
        /// The statement was entered in error and is not valid.
        /// (system: http://hl7.org/fhir/allergy-intolerance-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/allergy-intolerance-status"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// Estimate of the potential clinical harm, or seriousness, of a reaction to an identified Substance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-criticality)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceCriticality")]
    public enum AllergyIntoleranceCriticality
    {
        /// <summary>
        /// The potential clinical impact of a future reaction is estimated as low risk: exposure to substance is unlikely to result in a life threatening or organ system threatening outcome. Future exposure to the Substance is considered a relative contra-indication.
        /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
        /// </summary>
        [EnumLiteral("CRITL", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("Low Risk")]
        CRITL,
        /// <summary>
        /// The potential clinical impact of a future reaction is estimated as high risk: exposure to substance may result in a life threatening or organ system threatening outcome. Future exposure to the Substance may be considered an absolute contra-indication.
        /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
        /// </summary>
        [EnumLiteral("CRITH", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("High Risk")]
        CRITH,
        /// <summary>
        /// Unable to assess the potential clinical impact with the information available.
        /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
        /// </summary>
        [EnumLiteral("CRITU", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("Unable to determine")]
        CRITU,
    }

    /// <summary>
    /// Category of an identified Substance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-category)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceCategory")]
    public enum AllergyIntoleranceCategory
    {
        /// <summary>
        /// Any substance consumed to provide nutritional support for the body.
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("food", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Food")]
        Food,
        /// <summary>
        /// Substances administered to achieve a physiological effect.
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("medication", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Medication")]
        Medication,
        /// <summary>
        /// Substances that are encountered in the environment.
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("environment", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Environment")]
        Environment,
        /// <summary>
        /// Other substances that are not covered by any other category.
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Other")]
        Other,
    }

    /// <summary>
    /// Statement about the degree of clinical certainty that a Specific Substance was the cause of the Manifestation in an reaction event.
    /// (url: http://hl7.org/fhir/ValueSet/reaction-event-certainty)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceCertainty")]
    public enum AllergyIntoleranceCertainty
    {
        /// <summary>
        /// There is a low level of clinical certainty that the reaction was caused by the identified Substance.
        /// (system: http://hl7.org/fhir/reaction-event-certainty)
        /// </summary>
        [EnumLiteral("unlikely", "http://hl7.org/fhir/reaction-event-certainty"), Description("Unlikely")]
        Unlikely,
        /// <summary>
        /// There is a high level of clinical certainty that the reaction was caused by the identified Substance.
        /// (system: http://hl7.org/fhir/reaction-event-certainty)
        /// </summary>
        [EnumLiteral("likely", "http://hl7.org/fhir/reaction-event-certainty"), Description("Likely")]
        Likely,
        /// <summary>
        /// There is a very high level of clinical certainty that the reaction was due to the identified Substance, which may include clinical evidence by testing or rechallenge.
        /// (system: http://hl7.org/fhir/reaction-event-certainty)
        /// </summary>
        [EnumLiteral("confirmed", "http://hl7.org/fhir/reaction-event-certainty"), Description("Confirmed")]
        Confirmed,
    }

    /// <summary>
    /// The free/busy status of an appointment.
    /// (url: http://hl7.org/fhir/ValueSet/appointmentstatus)
    /// </summary>
    [FhirEnumeration("AppointmentStatus")]
    public enum AppointmentStatus
    {
        /// <summary>
        /// None of the participant(s) have finalized their acceptance of the appointment request, and the start/end time may not be set yet.
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/appointmentstatus"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// Some or all of the participant(s) have not finalized their acceptance of the appointment request.
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("pending", "http://hl7.org/fhir/appointmentstatus"), Description("Pending")]
        Pending,
        /// <summary>
        /// All participant(s) have been considered and the appointment is confirmed to go ahead at the date/times specified.
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("booked", "http://hl7.org/fhir/appointmentstatus"), Description("Booked")]
        Booked,
        /// <summary>
        /// Some of the patients have arrived.
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("arrived", "http://hl7.org/fhir/appointmentstatus"), Description("Arrived")]
        Arrived,
        /// <summary>
        /// This appointment has completed and may have resulted in an encounter.
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("fulfilled", "http://hl7.org/fhir/appointmentstatus"), Description("Fulfilled")]
        Fulfilled,
        /// <summary>
        /// The appointment has been cancelled.
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/appointmentstatus"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// Some or all of the participant(s) have not/did not appear for the appointment (usually the patient).
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("noshow", "http://hl7.org/fhir/appointmentstatus"), Description("No Show")]
        Noshow,
    }

    /// <summary>
    /// The Participation status of an appointment.
    /// (url: http://hl7.org/fhir/ValueSet/participantstatus)
    /// </summary>
    [FhirEnumeration("ParticipantStatus")]
    public enum ParticipantStatus
    {
        /// <summary>
        /// The appointment participant has accepted that they can attend the appointment at the time specified in the AppointmentResponse.
        /// (system: http://hl7.org/fhir/participantstatus)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/participantstatus"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The appointment participant has declined the appointment.
        /// (system: http://hl7.org/fhir/participantstatus)
        /// </summary>
        [EnumLiteral("declined", "http://hl7.org/fhir/participantstatus"), Description("Declined")]
        Declined,
        /// <summary>
        /// The appointment participant has tentatively accepted the appointment.
        /// (system: http://hl7.org/fhir/participantstatus)
        /// </summary>
        [EnumLiteral("tentative", "http://hl7.org/fhir/participantstatus"), Description("Tentative")]
        Tentative,
        /// <summary>
        /// The participant has in-process the appointment.
        /// (system: http://hl7.org/fhir/participantstatus)
        /// </summary>
        [EnumLiteral("in-process", "http://hl7.org/fhir/participantstatus"), Description("In Process")]
        InProcess,
        /// <summary>
        /// The participant has completed the appointment.
        /// (system: http://hl7.org/fhir/participantstatus)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/participantstatus"), Description("Completed")]
        Completed,
        /// <summary>
        /// This is the intitial status of an appointment participant until a participant has replied. It implies that there is no commitment for the appointment.
        /// (system: http://hl7.org/fhir/participantstatus)
        /// </summary>
        [EnumLiteral("needs-action", "http://hl7.org/fhir/participantstatus"), Description("Needs Action")]
        NeedsAction,
    }

    /// <summary>
    /// The type of network access point of this participant in the audit event
    /// (url: http://hl7.org/fhir/ValueSet/network-type)
    /// </summary>
    [FhirEnumeration("AuditEventParticipantNetworkType")]
    public enum AuditEventParticipantNetworkType
    {
        /// <summary>
        /// The machine name, including DNS name.
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("1", "http://hl7.org/fhir/network-type"), Description("Machine Name")]
        N1,
        /// <summary>
        /// The assigned Internet Protocol (IP) address.
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("2", "http://hl7.org/fhir/network-type"), Description("IP Address")]
        N2,
        /// <summary>
        /// The assigned telephone number.
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("3", "http://hl7.org/fhir/network-type"), Description("Telephone Number")]
        N3,
        /// <summary>
        /// The assigned email address.
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("4", "http://hl7.org/fhir/network-type"), Description("Email address")]
        N4,
        /// <summary>
        /// URI (User directory, HTTP-PUT, ftp, etc.).
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("5", "http://hl7.org/fhir/network-type"), Description("URI")]
        N5,
    }

    /// <summary>
    /// Indicates whether the plan is currently being acted upon, represents future intentions or is now a historical record.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-status)
    /// </summary>
    [FhirEnumeration("CarePlanStatus")]
    public enum CarePlanStatus
    {
        /// <summary>
        /// The plan has been suggested but no commitment to it has yet been made.
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/care-plan-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The plan is in development or awaiting use but is not yet intended to be acted upon.
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/care-plan-status"), Description("Pending")]
        Draft,
        /// <summary>
        /// The plan is intended to be followed and used as part of patient care.
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/care-plan-status"), Description("Active")]
        Active,
        /// <summary>
        /// The plan is no longer in use and is not expected to be followed or used in patient care.
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/care-plan-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The plan has been terminated prior to reaching completion (though it may have been replaced by a new plan).
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/care-plan-status"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// Codes identifying the types of relationships between two plans.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-relationship)
    /// </summary>
    [FhirEnumeration("CarePlanRelationship")]
    public enum CarePlanRelationship
    {
        /// <summary>
        /// The referenced plan is considered to be part of this plan.
        /// (system: http://hl7.org/fhir/care-plan-relationship)
        /// </summary>
        [EnumLiteral("includes", "http://hl7.org/fhir/care-plan-relationship"), Description("Includes")]
        Includes,
        /// <summary>
        /// This plan takes the places of the referenced plan.
        /// (system: http://hl7.org/fhir/care-plan-relationship)
        /// </summary>
        [EnumLiteral("replaces", "http://hl7.org/fhir/care-plan-relationship"), Description("Replaces")]
        Replaces,
        /// <summary>
        /// This plan provides details about how to perform activities defined at a higher level by the referenced plan.
        /// (system: http://hl7.org/fhir/care-plan-relationship)
        /// </summary>
        [EnumLiteral("fulfills", "http://hl7.org/fhir/care-plan-relationship"), Description("Fulfills")]
        Fulfills,
    }

    /// <summary>
    /// Indicates where the activity is at in its overall life cycle.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-activity-status)
    /// </summary>
    [FhirEnumeration("CarePlanActivityStatus")]
    public enum CarePlanActivityStatus
    {
        /// <summary>
        /// Activity is planned but no action has yet been taken.
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("not-started", "http://hl7.org/fhir/care-plan-activity-status"), Description("Not Started")]
        NotStarted,
        /// <summary>
        /// Appointment or other booking has occurred but activity has not yet begun.
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("scheduled", "http://hl7.org/fhir/care-plan-activity-status"), Description("Scheduled")]
        Scheduled,
        /// <summary>
        /// Activity has been started but is not yet complete.
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/care-plan-activity-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// Activity was started but has temporarily ceased with an expectation of resumption at a future time.
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/care-plan-activity-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// The activities have been completed (more or less) as planned.
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/care-plan-activity-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The activities have been ended prior to completion (perhaps even before they were started).
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/care-plan-activity-status"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// The type or discipline-style of the claim.
    /// (url: http://hl7.org/fhir/ValueSet/claim-type-link)
    /// </summary>
    [FhirEnumeration("ClaimType")]
    public enum ClaimType
    {
        /// <summary>
        /// A claim for Institution based, typically in-patient, goods and services.
        /// (system: http://hl7.org/fhir/claim-type-link)
        /// </summary>
        [EnumLiteral("institutional", "http://hl7.org/fhir/claim-type-link"), Description("Institutional")]
        Institutional,
        /// <summary>
        /// A claim for Oral Health (Dentist, Denturist, Hygienist) goods and services.
        /// (system: http://hl7.org/fhir/claim-type-link)
        /// </summary>
        [EnumLiteral("oral", "http://hl7.org/fhir/claim-type-link"), Description("Oral Health")]
        Oral,
        /// <summary>
        /// A claim for Pharmacy based goods and services.
        /// (system: http://hl7.org/fhir/claim-type-link)
        /// </summary>
        [EnumLiteral("pharmacy", "http://hl7.org/fhir/claim-type-link"), Description("Pharmacy")]
        Pharmacy,
        /// <summary>
        /// A claim for Professional, typically out-patient, goods and services.
        /// (system: http://hl7.org/fhir/claim-type-link)
        /// </summary>
        [EnumLiteral("professional", "http://hl7.org/fhir/claim-type-link"), Description("Professional")]
        Professional,
        /// <summary>
        /// A claim for Vision (Ophthamologist, Optometrist and Optician) goods and services.
        /// (system: http://hl7.org/fhir/claim-type-link)
        /// </summary>
        [EnumLiteral("vision", "http://hl7.org/fhir/claim-type-link"), Description("Vision")]
        Vision,
    }

    /// <summary>
    /// Complete, proposed, exploratory, other.
    /// (url: http://hl7.org/fhir/ValueSet/claim-use-link)
    /// </summary>
    [FhirEnumeration("Use")]
    public enum Use
    {
        /// <summary>
        /// The treatment is complete and this represents a Claim for the services.
        /// (system: http://hl7.org/fhir/claim-use-link)
        /// </summary>
        [EnumLiteral("complete", "http://hl7.org/fhir/claim-use-link"), Description("Complete")]
        Complete,
        /// <summary>
        /// The treatment is proposed and this represents a Pre-authorization for the services.
        /// (system: http://hl7.org/fhir/claim-use-link)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/claim-use-link"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The treatment is proposed and this represents a Pre-determination for the services.
        /// (system: http://hl7.org/fhir/claim-use-link)
        /// </summary>
        [EnumLiteral("exploratory", "http://hl7.org/fhir/claim-use-link"), Description("Exploratory")]
        Exploratory,
        /// <summary>
        /// A locally defined or otherwise resolved status.
        /// (system: http://hl7.org/fhir/claim-use-link)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/claim-use-link"), Description("Other")]
        Other,
    }

    /// <summary>
    /// The outcome of the processing.
    /// (url: http://hl7.org/fhir/ValueSet/remittance-outcome)
    /// </summary>
    [FhirEnumeration("RemittanceOutcome")]
    public enum RemittanceOutcome
    {
        /// <summary>
        /// The processing completed without errors.
        /// (system: http://hl7.org/fhir/remittance-outcome)
        /// </summary>
        [EnumLiteral("complete", "http://hl7.org/fhir/remittance-outcome"), Description("Complete")]
        Complete,
        /// <summary>
        /// The processing identified errors.
        /// (system: http://hl7.org/fhir/remittance-outcome)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/remittance-outcome"), Description("Error")]
        Error,
    }

    /// <summary>
    /// The workflow state of a clinical impression.
    /// (url: http://hl7.org/fhir/ValueSet/clinical-impression-status)
    /// </summary>
    [FhirEnumeration("ClinicalImpressionStatus")]
    public enum ClinicalImpressionStatus
    {
        /// <summary>
        /// The assessment is still on-going and results are not yet final.
        /// (system: http://hl7.org/fhir/clinical-impression-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/clinical-impression-status"), Description("In progress")]
        InProgress,
        /// <summary>
        /// The assessment is done and the results are final.
        /// (system: http://hl7.org/fhir/clinical-impression-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/clinical-impression-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// This assessment was never actually done and the record is erroneous (e.g. Wrong patient).
        /// (system: http://hl7.org/fhir/clinical-impression-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/clinical-impression-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The status of the communication.
    /// (url: http://hl7.org/fhir/ValueSet/communication-status)
    /// </summary>
    [FhirEnumeration("CommunicationStatus")]
    public enum CommunicationStatus
    {
        /// <summary>
        /// The communication transmission is ongoing.
        /// (system: http://hl7.org/fhir/communication-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/communication-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// The message transmission is complete, i.e., delivered to the recipient's destination.
        /// (system: http://hl7.org/fhir/communication-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/communication-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The communication transmission has been held by originating system/user request.
        /// (system: http://hl7.org/fhir/communication-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/communication-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// The receiving system has declined to accept the message.
        /// (system: http://hl7.org/fhir/communication-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/communication-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// There was a failure in transmitting the message out.
        /// (system: http://hl7.org/fhir/communication-status)
        /// </summary>
        [EnumLiteral("failed", "http://hl7.org/fhir/communication-status"), Description("Failed")]
        Failed,
    }

    /// <summary>
    /// The status of the communication.
    /// (url: http://hl7.org/fhir/ValueSet/communication-request-status)
    /// </summary>
    [FhirEnumeration("CommunicationRequestStatus")]
    public enum CommunicationRequestStatus
    {
        /// <summary>
        /// The request has been proposed.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/communication-request-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The request has been planned.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/communication-request-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// The request has been placed.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/communication-request-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// The receiving system has received the request but not yet decided whether it will be performed.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("received", "http://hl7.org/fhir/communication-request-status"), Description("Received")]
        Received,
        /// <summary>
        /// The receiving system has accepted the order, but work has not yet commenced.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/communication-request-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The work to fulfill the order is happening.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/communication-request-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// The work has been complete, the report(s) released, and no further work is planned.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/communication-request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The request has been held by originating system/user request.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/communication-request-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// The receiving system has declined to fulfill the request
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/communication-request-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The communication was attempted, but due to some procedural error, it could not be completed.
        /// (system: http://hl7.org/fhir/communication-request-status)
        /// </summary>
        [EnumLiteral("failed", "http://hl7.org/fhir/communication-request-status"), Description("Failed")]
        Failed,
    }

    /// <summary>
    ///  A set of codes specifying the security classification of acts and roles in accordance with the definition for concept domain "Confidentiality".
    /// (url: http://hl7.org/fhir/ValueSet/v3-Confidentiality)
    /// </summary>
    [FhirEnumeration("v3CodeSystemConfidentiality")]
    public enum v3CodeSystemConfidentiality
    {
        /// <summary>
        /// A specializable code and its leaf codes used in Confidentiality value sets to value the Act.Confidentiality and Role.Confidentiality attribute in accordance with the definition for concept domain "Confidentiality".
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("_Confidentiality", "http://hl7.org/fhir/v3/Confidentiality"), Description("Confidentiality")]
        Confidentiality,
        /// <summary>
        /// Definition: Privacy metadata indicating that the information has been de-identified, and there are mitigating circumstances that prevent re-identification, which minimize risk of harm from unauthorized disclosure.  The information requires protection to maintain low sensitivity.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Examples: Includes anonymized, pseudonymized, or non-personally identifiable information such as HIPAA limited data sets.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Map: No clear map to ISO 13606-4 Sensitivity Level (1) Care Management:   RECORD_COMPONENTs that might need to be accessed by a wide range of administrative staff to manage the subject of care's access to health services.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Usage Note: This metadata indicates the receiver may have an obligation to comply with a data use agreement.
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("L", "http://hl7.org/fhir/v3/Confidentiality"), Description("low")]
        L,
        /// <summary>
        /// Definition: Privacy metadata indicating moderately sensitive information, which presents moderate risk of harm if disclosed without authorization.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Examples: Includes allergies of non-sensitive nature used inform food service; health information a patient authorizes to be used for marketing, released to a bank for a health credit card or savings account; or information in personal health record systems that are not governed under health privacy laws.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Map: Partial Map to ISO 13606-4 Sensitivity Level (2) Clinical Management:  Less sensitive RECORD_COMPONENTs that might need to be accessed by a wider range of personnel not all of whom are actively caring for the patient (e.g. radiology staff).<br/>
        /// <br/>
        ///                         <br/>
        ///                            Usage Note: This metadata indicates that the receiver may be obligated to comply with the receiver's terms of use or privacy policies.
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("M", "http://hl7.org/fhir/v3/Confidentiality"), Description("moderate")]
        M,
        /// <summary>
        /// Definition: Privacy metadata indicating that the information is typical, non-stigmatizing health information, which presents typical risk of harm if disclosed without authorization.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Examples: In the US, this includes what HIPAA identifies as the minimum necessary protected health information (PHI) given a covered purpose of use (treatment, payment, or operations).  Includes typical, non-stigmatizing health information disclosed in an application for health, workers compensation, disability, or life insurance.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Map: Partial Map to ISO 13606-4 Sensitivity Level (3) Clinical Care:   Default for normal clinical care access (i.e. most clinical staff directly caring for the patient should be able to access nearly all of the EHR).   Maps to normal confidentiality for treatment information but not to ancillary care, payment and operations.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Usage Note: This metadata indicates that the receiver may be obligated to comply with applicable jurisdictional privacy law or disclosure authorization.
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("N", "http://hl7.org/fhir/v3/Confidentiality"), Description("normal")]
        N,
        /// <summary>
        /// Privacy metadata indicating highly sensitive, potentially stigmatizing information, which presents a high risk to the information subject if disclosed without authorization. May be pre-empted by jurisdictional law, e.g. for public health reporting or emergency treatment.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Examples: Includes information that is additionally protected such as sensitive conditions mental health, HIV, substance abuse, domestic violence, child abuse, genetic disease, and reproductive health; or sensitive demographic information such as a patient's standing as an employee or a celebrity. May be used to indicate proprietary or classified information that is not related to an individual, e.g. secret ingredients in a therapeutic substance; or the name of a manufacturer.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Map: Partial Map to ISO 13606-4 Sensitivity Level (3) Clinical Care: Default for normal clinical care access (i.e. most clinical staff directly caring for the patient should be able to access nearly all of the EHR). Maps to normal confidentiality for treatment information but not to ancillary care, payment and operations..<br/>
        /// <br/>
        ///                         <br/>
        ///                            Usage Note: This metadata indicates that the receiver may be obligated to comply with applicable, prevailing (default) jurisdictional privacy law or disclosure authorization..
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("R", "http://hl7.org/fhir/v3/Confidentiality"), Description("restricted")]
        R,
        /// <summary>
        /// Definition: Privacy metadata indicating that the information is not classified as sensitive.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Examples: Includes publicly available information, e.g. business name, phone, email or physical address.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Usage Note: This metadata indicates that the receiver has no obligation to consider additional policies when making access control decisions.   Note that in some jurisdictions, personally identifiable information must be protected as confidential, so it would not be appropriate to assign a confidentiality code of "unrestricted"  to that information even if it is publicly available.
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("U", "http://hl7.org/fhir/v3/Confidentiality"), Description("unrestricted")]
        U,
        /// <summary>
        /// . Privacy metadata indicating that the information is extremely sensitive and likely stigmatizing health information that presents a very high risk if disclosed without authorization.  This information must be kept in the highest confidence.  <br/>
        /// <br/>
        ///                         <br/>
        ///                            Examples:  Includes information about a victim of abuse, patient requested information sensitivity, and taboo subjects relating to health status that must be discussed with the patient by an attending provider before sharing with the patient.  May also include information held under â€œlegal lockâ€? or attorney-client privilege<br/>
        /// <br/>
        ///                         <br/>
        ///                            Map:  This metadata indicates that the receiver may not disclose this information except as directed by the information custodian, who may be the information subject.<br/>
        /// <br/>
        ///                         <br/>
        ///                            Usage Note:  This metadata indicates that the receiver may not disclose this information except as directed by the information custodian, who may be the information subject.
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("V", "http://hl7.org/fhir/v3/Confidentiality"), Description("very restricted")]
        V,
    }

    /// <summary>
    /// The degree of equivalence between concepts.
    /// (url: http://hl7.org/fhir/ValueSet/concept-map-equivalence)
    /// </summary>
    [FhirEnumeration("ConceptMapEquivalence")]
    public enum ConceptMapEquivalence
    {
        /// <summary>
        /// The definitions of the concepts mean the same thing (including when structural implications of meaning are considered) (i.e. extensionally identical).
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("equivalent", "http://hl7.org/fhir/concept-map-equivalence"), Description("Equivalent")]
        Equivalent,
        /// <summary>
        /// The definitions of the concepts are exactly the same (i.e. only grammatical differences) and structural implications of meaning are identical or irrelevant (i.e. intentionally identical).
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("equal", "http://hl7.org/fhir/concept-map-equivalence"), Description("Equal")]
        Equal,
        /// <summary>
        /// The target mapping is wider in meaning than the source concept.
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("wider", "http://hl7.org/fhir/concept-map-equivalence"), Description("Wider")]
        Wider,
        /// <summary>
        /// The target mapping subsumes the meaning of the source concept (e.g. the source is-a target).
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("subsumes", "http://hl7.org/fhir/concept-map-equivalence"), Description("Subsumes")]
        Subsumes,
        /// <summary>
        /// The target mapping is narrower in meaning that the source concept. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when attempting to use these mappings operationally.
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("narrower", "http://hl7.org/fhir/concept-map-equivalence"), Description("Narrower")]
        Narrower,
        /// <summary>
        /// The target mapping specializes the meaning of the source concept (e.g. the target is-a source).
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("specializes", "http://hl7.org/fhir/concept-map-equivalence"), Description("Specializes")]
        Specializes,
        /// <summary>
        /// The target mapping overlaps with the source concept, but both source and target cover additional meaning, or the definitions are imprecise and it is uncertain whether they have the same boundaries to their meaning. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when attempting to use these mappings operationally.
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("inexact", "http://hl7.org/fhir/concept-map-equivalence"), Description("Inexact")]
        Inexact,
        /// <summary>
        /// There is no match for this concept in the destination concept system.
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("unmatched", "http://hl7.org/fhir/concept-map-equivalence"), Description("Unmatched")]
        Unmatched,
        /// <summary>
        /// This is an explicit assertion that there is no mapping between the source and target concept.
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("disjoint", "http://hl7.org/fhir/concept-map-equivalence"), Description("Disjoint")]
        Disjoint,
    }

    /// <summary>
    /// How a conformance statement is intended to be used.
    /// (url: http://hl7.org/fhir/ValueSet/conformance-statement-kind)
    /// </summary>
    [FhirEnumeration("ConformanceStatementKind")]
    public enum ConformanceStatementKind
    {
        /// <summary>
        /// The Conformance instance represents the present capabilities of a specific system instance.  This is the kind returned by OPTIONS for a FHIR server end-point.
        /// (system: http://hl7.org/fhir/conformance-statement-kind)
        /// </summary>
        [EnumLiteral("instance", "http://hl7.org/fhir/conformance-statement-kind"), Description("Instance")]
        Instance,
        /// <summary>
        /// The Conformance instance represents the capabilities of a system or piece of software, independent of a particular installation.
        /// (system: http://hl7.org/fhir/conformance-statement-kind)
        /// </summary>
        [EnumLiteral("capability", "http://hl7.org/fhir/conformance-statement-kind"), Description("Capability")]
        Capability,
        /// <summary>
        /// The Conformance instance represents a set of requirements for other systems to meet; e.g. as part of an implementation guide or 'request for proposal'.
        /// (system: http://hl7.org/fhir/conformance-statement-kind)
        /// </summary>
        [EnumLiteral("requirements", "http://hl7.org/fhir/conformance-statement-kind"), Description("Requirements")]
        Requirements,
    }

    /// <summary>
    /// The mode of a RESTful conformance statement.
    /// (url: http://hl7.org/fhir/ValueSet/restful-conformance-mode)
    /// </summary>
    [FhirEnumeration("RestfulConformanceMode")]
    public enum RestfulConformanceMode
    {
        /// <summary>
        /// The application acts as a client for this resource.
        /// (system: http://hl7.org/fhir/restful-conformance-mode)
        /// </summary>
        [EnumLiteral("client", "http://hl7.org/fhir/restful-conformance-mode"), Description("Client")]
        Client,
        /// <summary>
        /// The application acts as a server for this resource.
        /// (system: http://hl7.org/fhir/restful-conformance-mode)
        /// </summary>
        [EnumLiteral("server", "http://hl7.org/fhir/restful-conformance-mode"), Description("Server")]
        Server,
    }

    /// <summary>
    /// Operations supported by REST at the type or instance level.
    /// (url: http://hl7.org/fhir/ValueSet/type-restful-interaction)
    /// </summary>
    [FhirEnumeration("TypeRestfulInteraction")]
    public enum TypeRestfulInteraction
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("read", "http://hl7.org/fhir/restful-interaction"), Description("read")]
        Read,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("vread", "http://hl7.org/fhir/restful-interaction"), Description("vread")]
        Vread,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("update", "http://hl7.org/fhir/restful-interaction"), Description("update")]
        Update,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("delete", "http://hl7.org/fhir/restful-interaction"), Description("delete")]
        Delete,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("history-instance", "http://hl7.org/fhir/restful-interaction"), Description("history-instance")]
        HistoryInstance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("validate", "http://hl7.org/fhir/restful-interaction"), Description("validate")]
        Validate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("history-type", "http://hl7.org/fhir/restful-interaction"), Description("history-type")]
        HistoryType,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("create", "http://hl7.org/fhir/restful-interaction"), Description("create")]
        Create,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("search-type", "http://hl7.org/fhir/restful-interaction"), Description("search-type")]
        SearchType,
    }

    /// <summary>
    /// Operations supported by REST at the system level.
    /// (url: http://hl7.org/fhir/ValueSet/system-restful-interaction)
    /// </summary>
    [FhirEnumeration("SystemRestfulInteraction")]
    public enum SystemRestfulInteraction
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("transaction", "http://hl7.org/fhir/restful-interaction"), Description("transaction")]
        Transaction,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("search-system", "http://hl7.org/fhir/restful-interaction"), Description("search-system")]
        SearchSystem,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-interaction)
        /// </summary>
        [EnumLiteral("history-system", "http://hl7.org/fhir/restful-interaction"), Description("history-system")]
        HistorySystem,
    }

    /// <summary>
    /// A code that indicates how transactions are supported.
    /// (url: http://hl7.org/fhir/ValueSet/transaction-mode)
    /// </summary>
    [FhirEnumeration("TransactionMode")]
    public enum TransactionMode
    {
        /// <summary>
        /// Neither batch or transaction is supported.
        /// (system: http://hl7.org/fhir/transaction-mode)
        /// </summary>
        [EnumLiteral("not-supported", "http://hl7.org/fhir/transaction-mode"), Description("None")]
        NotSupported,
        /// <summary>
        /// Batches are  supported.
        /// (system: http://hl7.org/fhir/transaction-mode)
        /// </summary>
        [EnumLiteral("batch", "http://hl7.org/fhir/transaction-mode"), Description("Batches supported")]
        Batch,
        /// <summary>
        /// Transactions are supported.
        /// (system: http://hl7.org/fhir/transaction-mode)
        /// </summary>
        [EnumLiteral("transaction", "http://hl7.org/fhir/transaction-mode"), Description("Transactions Supported")]
        Transaction,
        /// <summary>
        /// Both batches and transactions are supported.
        /// (system: http://hl7.org/fhir/transaction-mode)
        /// </summary>
        [EnumLiteral("both", "http://hl7.org/fhir/transaction-mode"), Description("Batches & Transactions")]
        Both,
    }

    /// <summary>
    /// The mode of a message conformance statement.
    /// (url: http://hl7.org/fhir/ValueSet/message-conformance-event-mode)
    /// </summary>
    [FhirEnumeration("ConformanceEventMode")]
    public enum ConformanceEventMode
    {
        /// <summary>
        /// The application sends requests and receives responses.
        /// (system: http://hl7.org/fhir/message-conformance-event-mode)
        /// </summary>
        [EnumLiteral("sender", "http://hl7.org/fhir/message-conformance-event-mode"), Description("Sender")]
        Sender,
        /// <summary>
        /// The application receives requests and sends responses.
        /// (system: http://hl7.org/fhir/message-conformance-event-mode)
        /// </summary>
        [EnumLiteral("receiver", "http://hl7.org/fhir/message-conformance-event-mode"), Description("Receiver")]
        Receiver,
    }

    /// <summary>
    /// The availability status of the device.
    /// (url: http://hl7.org/fhir/ValueSet/devicestatus)
    /// </summary>
    [FhirEnumeration("DeviceStatus")]
    public enum DeviceStatus
    {
        /// <summary>
        /// The Device is available for use.
        /// (system: http://hl7.org/fhir/devicestatus)
        /// </summary>
        [EnumLiteral("available", "http://hl7.org/fhir/devicestatus"), Description("Available")]
        Available,
        /// <summary>
        /// The Device is no longer available for use (e.g. lost, expired, damaged).
        /// (system: http://hl7.org/fhir/devicestatus)
        /// </summary>
        [EnumLiteral("not-available", "http://hl7.org/fhir/devicestatus"), Description("Not Available")]
        NotAvailable,
        /// <summary>
        /// The Device was entered in error and voided.
        /// (system: http://hl7.org/fhir/devicestatus)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/devicestatus"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// Different measurement principle supported by the device.
    /// (url: http://hl7.org/fhir/ValueSet/measurement-principle)
    /// </summary>
    [FhirEnumeration("Measmnt_Principle")]
    public enum Measmnt_Principle
    {
        /// <summary>
        /// Measurement principle isn't in the list.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/measurement-principle"), Description("MSP Other")]
        Other,
        /// <summary>
        /// Measurement is done using the chemical principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("chemical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Chemical")]
        Chemical,
        /// <summary>
        /// Measurement is done using the electrical principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("electrical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Electrical")]
        Electrical,
        /// <summary>
        /// Measurement is done using the impedance principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("impedance", "http://hl7.org/fhir/measurement-principle"), Description("MSP Impedance")]
        Impedance,
        /// <summary>
        /// Measurement is done using the nuclear principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("nuclear", "http://hl7.org/fhir/measurement-principle"), Description("MSP Nuclear")]
        Nuclear,
        /// <summary>
        /// Measurement is done using the optical principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("optical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Optical")]
        Optical,
        /// <summary>
        /// Measurement is done using the thermal principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("thermal", "http://hl7.org/fhir/measurement-principle"), Description("MSP Thermal")]
        Thermal,
        /// <summary>
        /// Measurement is done using the biological principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("biological", "http://hl7.org/fhir/measurement-principle"), Description("MSP Biological")]
        Biological,
        /// <summary>
        /// Measurement is done using the mechanical principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("mechanical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Mechanical")]
        Mechanical,
        /// <summary>
        /// Measurement is done using the acoustical principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("acoustical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Acoustical")]
        Acoustical,
        /// <summary>
        /// Measurement is done using the manual principle.
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("manual", "http://hl7.org/fhir/measurement-principle"), Description("MSP Manual")]
        Manual,
    }

    /// <summary>
    /// Describes the operational status of the DeviceMetric.
    /// (url: http://hl7.org/fhir/ValueSet/metric-operational-status)
    /// </summary>
    [FhirEnumeration("DeviceMetricOperationalStatus")]
    public enum DeviceMetricOperationalStatus
    {
        /// <summary>
        /// The DeviceMetric is operating and will generate DeviceObservations.
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("on", "http://hl7.org/fhir/metric-operational-status"), Description("On")]
        On,
        /// <summary>
        /// The DeviceMetric is not operating.
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("off", "http://hl7.org/fhir/metric-operational-status"), Description("Off")]
        Off,
        /// <summary>
        /// The DeviceMetric is operating, but will not generate any DeviceObservations.
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("standby", "http://hl7.org/fhir/metric-operational-status"), Description("Standby")]
        Standby,
    }

    /// <summary>
    /// Codes representing the status of the request.
    /// (url: http://hl7.org/fhir/ValueSet/device-use-request-status)
    /// </summary>
    [FhirEnumeration("DeviceUseRequestStatus")]
    public enum DeviceUseRequestStatus
    {
        /// <summary>
        /// The request has been proposed.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/device-use-request-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The request has been planned.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/device-use-request-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// The request has been placed.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/device-use-request-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// The receiving system has received the request but not yet decided whether it will be performed.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("received", "http://hl7.org/fhir/device-use-request-status"), Description("Received")]
        Received,
        /// <summary>
        /// The receiving system has accepted the request but work has not yet commenced.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/device-use-request-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The work to fulfill the order is happening.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/device-use-request-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// The work has been complete, the report(s) released, and no further work is planned.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/device-use-request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The request has been held by originating system/user request.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/device-use-request-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// The receiving system has declined to fulfill the request.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/device-use-request-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The request was attempted, but due to some procedural error, it could not be completed.
        /// (system: http://hl7.org/fhir/device-use-request-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/device-use-request-status"), Description("Aborted")]
        Aborted,
    }

    /// <summary>
    /// Codes representing the priority of the request.
    /// (url: http://hl7.org/fhir/ValueSet/device-use-request-priority)
    /// </summary>
    [FhirEnumeration("DeviceUseRequestPriority")]
    public enum DeviceUseRequestPriority
    {
        /// <summary>
        /// The request has a normal priority.
        /// (system: http://hl7.org/fhir/device-use-request-priority)
        /// </summary>
        [EnumLiteral("routine", "http://hl7.org/fhir/device-use-request-priority"), Description("Routine")]
        Routine,
        /// <summary>
        /// The request should be done urgently.
        /// (system: http://hl7.org/fhir/device-use-request-priority)
        /// </summary>
        [EnumLiteral("urgent", "http://hl7.org/fhir/device-use-request-priority"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// The request is time-critical.
        /// (system: http://hl7.org/fhir/device-use-request-priority)
        /// </summary>
        [EnumLiteral("stat", "http://hl7.org/fhir/device-use-request-priority"), Description("Stat")]
        Stat,
        /// <summary>
        /// The request should be acted on as soon as possible.
        /// (system: http://hl7.org/fhir/device-use-request-priority)
        /// </summary>
        [EnumLiteral("asap", "http://hl7.org/fhir/device-use-request-priority"), Description("ASAP")]
        Asap,
    }

    /// <summary>
    /// The status of a diagnostic order.
    /// (url: http://hl7.org/fhir/ValueSet/diagnostic-order-status)
    /// </summary>
    [FhirEnumeration("DiagnosticOrderStatus")]
    public enum DiagnosticOrderStatus
    {
        /// <summary>
        /// The request has been proposed.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/diagnostic-order-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The request is in preliminary form prior to being sent.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/diagnostic-order-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// The request has been planned.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/diagnostic-order-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// The request has been placed.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/diagnostic-order-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// The receiving system has received the order, but not yet decided whether it will be performed.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("received", "http://hl7.org/fhir/diagnostic-order-status"), Description("Received")]
        Received,
        /// <summary>
        /// The receiving system has accepted the order, but work has not yet commenced.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/diagnostic-order-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The work to fulfill the order is happening.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/diagnostic-order-status"), Description("In-Progress")]
        InProgress,
        /// <summary>
        /// The work is complete, and the outcomes are being reviewed for approval.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("review", "http://hl7.org/fhir/diagnostic-order-status"), Description("Review")]
        Review,
        /// <summary>
        /// The work has been completed, the report(s) released, and no further work is planned.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/diagnostic-order-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The request has been withdrawn.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/diagnostic-order-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// The request has been held by originating system/user request.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/diagnostic-order-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// The receiving system has declined to fulfill the request.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/diagnostic-order-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The diagnostic investigation was attempted, but due to some procedural error, it could not be completed.
        /// (system: http://hl7.org/fhir/diagnostic-order-status)
        /// </summary>
        [EnumLiteral("failed", "http://hl7.org/fhir/diagnostic-order-status"), Description("Failed")]
        Failed,
    }

    /// <summary>
    /// The clinical priority of a diagnostic order.
    /// (url: http://hl7.org/fhir/ValueSet/diagnostic-order-priority)
    /// </summary>
    [FhirEnumeration("DiagnosticOrderPriority")]
    public enum DiagnosticOrderPriority
    {
        /// <summary>
        /// The order has a normal priority .
        /// (system: http://hl7.org/fhir/diagnostic-order-priority)
        /// </summary>
        [EnumLiteral("routine", "http://hl7.org/fhir/diagnostic-order-priority"), Description("Routine")]
        Routine,
        /// <summary>
        /// The order should be urgently.
        /// (system: http://hl7.org/fhir/diagnostic-order-priority)
        /// </summary>
        [EnumLiteral("urgent", "http://hl7.org/fhir/diagnostic-order-priority"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// The order is time-critical.
        /// (system: http://hl7.org/fhir/diagnostic-order-priority)
        /// </summary>
        [EnumLiteral("stat", "http://hl7.org/fhir/diagnostic-order-priority"), Description("Stat")]
        Stat,
        /// <summary>
        /// The order should be acted on as soon as possible.
        /// (system: http://hl7.org/fhir/diagnostic-order-priority)
        /// </summary>
        [EnumLiteral("asap", "http://hl7.org/fhir/diagnostic-order-priority"), Description("ASAP")]
        Asap,
    }

    /// <summary>
    /// The status of the diagnostic report as a whole.
    /// (url: http://hl7.org/fhir/ValueSet/diagnostic-report-status)
    /// </summary>
    [FhirEnumeration("DiagnosticReportStatus")]
    public enum DiagnosticReportStatus
    {
        /// <summary>
        /// The existence of the report is registered, but there is nothing yet available.
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("registered", "http://hl7.org/fhir/diagnostic-report-status"), Description("Registered")]
        Registered,
        /// <summary>
        /// This is a partial (e.g. initial, interim or preliminary) report: data in the report may be incomplete or unverified.
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("partial", "http://hl7.org/fhir/diagnostic-report-status"), Description("Partial")]
        Partial,
        /// <summary>
        /// The report is complete and verified by an authorized person.
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/diagnostic-report-status"), Description("Final")]
        Final,
        /// <summary>
        /// The report has been modified subsequent to being Final, and is complete and verified by an authorized person. New content has been added, but existing content hasn't changed
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("corrected", "http://hl7.org/fhir/diagnostic-report-status"), Description("Corrected")]
        Corrected,
        /// <summary>
        /// The report has been modified subsequent to being Final, and is complete and verified by an authorized person. New content has been added, but existing content hasn't changed.
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("appended", "http://hl7.org/fhir/diagnostic-report-status"), Description("Appended")]
        Appended,
        /// <summary>
        /// The report is unavailable because the measurement was not started or not completed (also sometimes called "aborted").
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/diagnostic-report-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// The report has been withdrawn following a previous final release.
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/diagnostic-report-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// Current state of the encounter
    /// (url: http://hl7.org/fhir/ValueSet/encounter-state)
    /// </summary>
    [FhirEnumeration("EncounterState")]
    public enum EncounterState
    {
        /// <summary>
        /// The Encounter has not yet started.
        /// (system: http://hl7.org/fhir/encounter-state)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/encounter-state"), Description("Planned")]
        Planned,
        /// <summary>
        /// The Patient is present for the encounter, however is not currently meeting with a practitioner.
        /// (system: http://hl7.org/fhir/encounter-state)
        /// </summary>
        [EnumLiteral("arrived", "http://hl7.org/fhir/encounter-state"), Description("Arrived")]
        Arrived,
        /// <summary>
        /// The Encounter has begun and the patient is present / the practitioner and the patient are meeting.
        /// (system: http://hl7.org/fhir/encounter-state)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/encounter-state"), Description("in Progress")]
        InProgress,
        /// <summary>
        /// The Encounter has begun, but the patient is temporarily on leave.
        /// (system: http://hl7.org/fhir/encounter-state)
        /// </summary>
        [EnumLiteral("onleave", "http://hl7.org/fhir/encounter-state"), Description("On Leave")]
        Onleave,
        /// <summary>
        /// The Encounter has ended.
        /// (system: http://hl7.org/fhir/encounter-state)
        /// </summary>
        [EnumLiteral("finished", "http://hl7.org/fhir/encounter-state"), Description("Finished")]
        Finished,
        /// <summary>
        /// The Encounter has ended before it has begun.
        /// (system: http://hl7.org/fhir/encounter-state)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/encounter-state"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// Classification of the encounter
    /// (url: http://hl7.org/fhir/ValueSet/encounter-class)
    /// </summary>
    [FhirEnumeration("EncounterClass")]
    public enum EncounterClass
    {
        /// <summary>
        /// An encounter during which the patient is hospitalized and stays overnight.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("inpatient", "http://hl7.org/fhir/encounter-class"), Description("Inpatient")]
        Inpatient,
        /// <summary>
        /// An encounter during which the patient is not hospitalized overnight.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("outpatient", "http://hl7.org/fhir/encounter-class"), Description("Outpatient")]
        Outpatient,
        /// <summary>
        /// An encounter where the patient visits the practitioner in his/her office, e.g. a G.P. visit.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("ambulatory", "http://hl7.org/fhir/encounter-class"), Description("Ambulatory")]
        Ambulatory,
        /// <summary>
        /// An encounter in the Emergency Care Department.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("emergency", "http://hl7.org/fhir/encounter-class"), Description("Emergency")]
        Emergency,
        /// <summary>
        /// An encounter where the practitioner visits the patient at his/her home.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("home", "http://hl7.org/fhir/encounter-class"), Description("Home")]
        Home,
        /// <summary>
        /// An encounter taking place outside the regular environment for giving care.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("field", "http://hl7.org/fhir/encounter-class"), Description("Field")]
        Field,
        /// <summary>
        /// An encounter where the patient needs more prolonged treatment or investigations than outpatients, but who do not need to stay in the hospital overnight.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("daytime", "http://hl7.org/fhir/encounter-class"), Description("Daytime")]
        Daytime,
        /// <summary>
        /// An encounter that takes place where the patient and practitioner do not physically meet but use electronic means for contact.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("virtual", "http://hl7.org/fhir/encounter-class"), Description("Virtual")]
        Virtual,
        /// <summary>
        /// Any other encounter type that is not described by one of the other values. Where this is used it is expected that an implementer will include an extension value to define what the actual other type is.
        /// (system: http://hl7.org/fhir/encounter-class)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/encounter-class"), Description("Other")]
        Other,
    }

    /// <summary>
    /// The status of the encounter.
    /// (url: http://hl7.org/fhir/ValueSet/episode-of-care-status)
    /// </summary>
    [FhirEnumeration("EpisodeOfCareStatus")]
    public enum EpisodeOfCareStatus
    {
        /// <summary>
        /// This episode of care is planned to start at the date specified in the period.start. During this status an organization may perform assessments to determine if they are eligible to receive services, or be organizing to make resources available to provide care services.
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/episode-of-care-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// This episode has been placed on a waitlist, pending the episode being made active (or cancelled).
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("waitlist", "http://hl7.org/fhir/episode-of-care-status"), Description("Waitlist")]
        Waitlist,
        /// <summary>
        /// This episode of care is current.
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/episode-of-care-status"), Description("Active")]
        Active,
        /// <summary>
        /// This episode of care is on hold, the organization has limited responsibility for the patient (such as while on respite).
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("onhold", "http://hl7.org/fhir/episode-of-care-status"), Description("On Hold")]
        Onhold,
        /// <summary>
        /// This episode of care is finished at the organization is not expecting to be providing care to the patient. Can also be known as "closed", "completed" or other similar terms.
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("finished", "http://hl7.org/fhir/episode-of-care-status"), Description("Finished")]
        Finished,
        /// <summary>
        /// The episode of care was cancelled, or withdrawn from service, often selected during the planned stage as the patient may have gone elsewhere, or the circumstances have changed and the organization is unable to provide the care. It indicates that services terminated outside the planned/expected workflow.
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/episode-of-care-status"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// Indicates whether the goal has been met and is still being targeted
    /// (url: http://hl7.org/fhir/ValueSet/goal-status)
    /// </summary>
    [FhirEnumeration("GoalStatus")]
    public enum GoalStatus
    {
        /// <summary>
        /// A goal is proposed for this patient
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/goal-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// A goal is planned for this patient
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/goal-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// A proposed goal was accepted
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/goal-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// A proposed goal was rejected
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/goal-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The goal is being sought but has not yet been reached.  (Also applies if goal was reached in the past but there has been regression and goal is being sought again)
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/goal-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// The goal has been met and no further action is needed
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("achieved", "http://hl7.org/fhir/goal-status"), Description("Achieved")]
        Achieved,
        /// <summary>
        /// The goal has been met, but ongoing activity is needed to sustain the goal objective
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("sustaining", "http://hl7.org/fhir/goal-status"), Description("Sustaining")]
        Sustaining,
        /// <summary>
        /// The goal remains a long term objective but is no longer being actively pursued for a temporary period of time.
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/goal-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// The goal is no longer being sought
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/goal-status"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// A set of codes indicating the current status of a MedicationAdministration.
    /// (url: http://hl7.org/fhir/ValueSet/medication-admin-status)
    /// </summary>
    [FhirEnumeration("MedicationAdministrationStatus")]
    public enum MedicationAdministrationStatus
    {
        /// <summary>
        /// The administration has started but has not yet completed.
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/medication-admin-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// Actions implied by the administration have been temporarily halted, but are expected to continue later. May also be called "suspended".
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-admin-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// All actions that are implied by the administration have occurred.
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-admin-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The administration was entered in error and therefore nullified.
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-admin-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// Actions implied by the administration have been permanently halted, before all of them occurred.
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-admin-status"), Description("Stopped")]
        Stopped,
    }

    /// <summary>
    /// Why a resource is included in the guide.
    /// (url: http://hl7.org/fhir/ValueSet/guide-resource-purpose)
    /// </summary>
    [FhirEnumeration("GuideResourcePurpose")]
    public enum GuideResourcePurpose
    {
        /// <summary>
        /// The resource is intended as an example.
        /// (system: http://hl7.org/fhir/guide-resource-purpose)
        /// </summary>
        [EnumLiteral("example", "http://hl7.org/fhir/guide-resource-purpose"), Description("Example")]
        Example,
        /// <summary>
        /// The resource defines a value set or concept map used in the implementation guide.
        /// (system: http://hl7.org/fhir/guide-resource-purpose)
        /// </summary>
        [EnumLiteral("terminology", "http://hl7.org/fhir/guide-resource-purpose"), Description("Terminology")]
        Terminology,
        /// <summary>
        /// The resource defines a profile (StructureDefinition) that is used in the implementation guide.
        /// (system: http://hl7.org/fhir/guide-resource-purpose)
        /// </summary>
        [EnumLiteral("profile", "http://hl7.org/fhir/guide-resource-purpose"), Description("Profile")]
        Profile,
        /// <summary>
        /// The resource defines an extension (StructureDefinition) that is used in the implementation guide.
        /// (system: http://hl7.org/fhir/guide-resource-purpose)
        /// </summary>
        [EnumLiteral("extension", "http://hl7.org/fhir/guide-resource-purpose"), Description("Extension")]
        Extension,
        /// <summary>
        /// The resource contains a dictionary that is part of the implementation guide.
        /// (system: http://hl7.org/fhir/guide-resource-purpose)
        /// </summary>
        [EnumLiteral("dictionary", "http://hl7.org/fhir/guide-resource-purpose"), Description("Dictionary")]
        Dictionary,
        /// <summary>
        /// The resource defines a logical model (in a StructureDefinition) that is used in the implementation guide.
        /// (system: http://hl7.org/fhir/guide-resource-purpose)
        /// </summary>
        [EnumLiteral("logical", "http://hl7.org/fhir/guide-resource-purpose"), Description("Logical Model")]
        Logical,
    }

    /// <summary>
    /// A code specifying the state of the dispense event.<br/>
    /// <br/>
    /// Describes the lifecycle of the dispense.
    /// (url: http://hl7.org/fhir/ValueSet/medication-dispense-status)
    /// </summary>
    [FhirEnumeration("MedicationDispenseStatus")]
    public enum MedicationDispenseStatus
    {
        /// <summary>
        /// The dispense has started but has not yet completed.
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/medication-dispense-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// Actions implied by the administration have been temporarily halted, but are expected to continue later. May also be called "suspended"
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-dispense-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// All actions that are implied by the dispense have occurred.
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-dispense-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The dispense was entered in error and therefore nullified.
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-dispense-status"), Description("Entered in-Error")]
        EnteredInError,
        /// <summary>
        /// Actions implied by the dispense have been permanently halted, before all of them occurred.
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-dispense-status"), Description("Stopped")]
        Stopped,
    }

    /// <summary>
    /// A code specifying the state of the prescribing event. Describes the lifecycle of the prescription.
    /// (url: http://hl7.org/fhir/ValueSet/medication-order-status)
    /// </summary>
    [FhirEnumeration("MedicationOrderStatus")]
    public enum MedicationOrderStatus
    {
        /// <summary>
        /// The prescription is 'actionable', but not all actions that are implied by it have occurred yet.
        /// (system: http://hl7.org/fhir/medication-order-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/medication-order-status"), Description("Active")]
        Active,
        /// <summary>
        /// Actions implied by the prescription are to be temporarily halted, but are expected to continue later.  May also be called "suspended".
        /// (system: http://hl7.org/fhir/medication-order-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-order-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// All actions that are implied by the prescription have occurred.
        /// (system: http://hl7.org/fhir/medication-order-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-order-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The prescription was entered in error.
        /// (system: http://hl7.org/fhir/medication-order-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-order-status"), Description("Entered In Error")]
        EnteredInError,
        /// <summary>
        /// Actions implied by the prescription are to be permanently halted, before all of them occurred.
        /// (system: http://hl7.org/fhir/medication-order-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-order-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// The prescription is not yet 'actionable', i.e. it is a work in progress, requires sign-off or verification, and needs to be run through decision support process.
        /// (system: http://hl7.org/fhir/medication-order-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/medication-order-status"), Description("Draft")]
        Draft,
    }

    /// <summary>
    /// A set of codes indicating the current status of a MedicationStatement.
    /// (url: http://hl7.org/fhir/ValueSet/medication-statement-status)
    /// </summary>
    [FhirEnumeration("MedicationStatementStatus")]
    public enum MedicationStatementStatus
    {
        /// <summary>
        /// The medication is still being taken.
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/medication-statement-status"), Description("Active")]
        Active,
        /// <summary>
        /// The medication is no longer being taken.
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-statement-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The statement was entered in error.
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-statement-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// The medication may be taken at some time in the future.
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("intended", "http://hl7.org/fhir/medication-statement-status"), Description("Intended")]
        Intended,
    }

    /// <summary>
    /// Codes specifying the state of the request. Describes the lifecycle of the nutrition order.
    /// (url: http://hl7.org/fhir/ValueSet/nutrition-order-status)
    /// </summary>
    [FhirEnumeration("NutritionOrderStatus")]
    public enum NutritionOrderStatus
    {
        /// <summary>
        /// The request has been proposed.
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/nutrition-order-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The request is in preliminary form prior to being sent.
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/nutrition-order-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// The request has been planned.
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/nutrition-order-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// The request has been placed.
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/nutrition-order-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// The request is 'actionable', but not all actions that are implied by it have occurred yet.
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/nutrition-order-status"), Description("Active")]
        Active,
        /// <summary>
        /// Actions implied by the request have been temporarily halted, but are expected to continue later. May also be called "suspended".
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/nutrition-order-status"), Description("On-Hold")]
        OnHold,
        /// <summary>
        /// All actions that are implied by the order have occurred and no continuation is planned (this will rarely be made explicit).
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/nutrition-order-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The request has been withdrawn and is no longer actionable.
        /// (system: http://hl7.org/fhir/nutrition-order-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/nutrition-order-status"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// Codes providing the status of an observation.
    /// (url: http://hl7.org/fhir/ValueSet/observation-status)
    /// </summary>
    [FhirEnumeration("ObservationStatus")]
    public enum ObservationStatus
    {
        /// <summary>
        /// The existence of the observation is registered, but there is no result yet available.
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("registered", "http://hl7.org/fhir/observation-status"), Description("Registered")]
        Registered,
        /// <summary>
        /// This is an initial or interim observation: data may be incomplete or unverified.
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("preliminary", "http://hl7.org/fhir/observation-status"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// The observation is complete and verified by an authorized person.
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/observation-status"), Description("Final")]
        Final,
        /// <summary>
        /// The observation has been modified subsequent to being Final, and is complete and verified by an authorized person.
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/observation-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// The observation is unavailable because the measurement was not started or not completed (also sometimes called "aborted").
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/observation-status"), Description("cancelled")]
        Cancelled,
        /// <summary>
        /// The observation has been withdrawn following previous final release.
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/observation-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// The observation status is unknown.  Note that "unknown" is a value of last resort and every attempt should be made to provide a meaningful value other than "unknown".
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/observation-status"), Description("Unknown Status")]
        Unknown,
    }

    /// <summary>
    /// Parameter Types used in Operation Definitions
    /// (url: http://hl7.org/fhir/ValueSet/operation-parameter-type)
    /// </summary>
    [FhirEnumeration("ParameterTypesusedinOperationDefinitions")]
    public enum ParameterTypesusedinOperationDefinitions
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("number", "http://hl7.org/fhir/search-param-type"), Description("Number")]
        Number,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/search-param-type"), Description("Date/DateTime")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/search-param-type"), Description("String")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("token", "http://hl7.org/fhir/search-param-type"), Description("Token")]
        Token,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/search-param-type"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("composite", "http://hl7.org/fhir/search-param-type"), Description("Composite")]
        Composite,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("quantity", "http://hl7.org/fhir/search-param-type"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("uri", "http://hl7.org/fhir/search-param-type"), Description("URI")]
        Uri,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Address", "http://hl7.org/fhir/data-types"), Description("Address")]
        Address,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Age", "http://hl7.org/fhir/data-types"), Description("Age")]
        Age,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Annotation", "http://hl7.org/fhir/data-types"), Description("Annotation")]
        Annotation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Attachment", "http://hl7.org/fhir/data-types"), Description("Attachment")]
        Attachment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("BackboneElement", "http://hl7.org/fhir/data-types"), Description("BackboneElement")]
        BackboneElement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("CodeableConcept", "http://hl7.org/fhir/data-types"), Description("CodeableConcept")]
        CodeableConcept,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Coding", "http://hl7.org/fhir/data-types"), Description("Coding")]
        Coding,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ContactPoint", "http://hl7.org/fhir/data-types"), Description("ContactPoint")]
        ContactPoint,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Count", "http://hl7.org/fhir/data-types"), Description("Count")]
        Count,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Distance", "http://hl7.org/fhir/data-types"), Description("Distance")]
        Distance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Duration", "http://hl7.org/fhir/data-types"), Description("Duration")]
        Duration,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Element", "http://hl7.org/fhir/data-types"), Description("Element")]
        Element,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ElementDefinition", "http://hl7.org/fhir/data-types"), Description("ElementDefinition")]
        ElementDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Extension", "http://hl7.org/fhir/data-types"), Description("Extension")]
        Extension,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("HumanName", "http://hl7.org/fhir/data-types"), Description("HumanName")]
        HumanName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Identifier", "http://hl7.org/fhir/data-types"), Description("Identifier")]
        Identifier,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Meta", "http://hl7.org/fhir/data-types"), Description("Meta")]
        Meta,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Money", "http://hl7.org/fhir/data-types"), Description("Money")]
        Money,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Narrative", "http://hl7.org/fhir/data-types"), Description("Narrative")]
        Narrative,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Period", "http://hl7.org/fhir/data-types"), Description("Period")]
        Period,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Range", "http://hl7.org/fhir/data-types"), Description("Range")]
        Range,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Ratio", "http://hl7.org/fhir/data-types"), Description("Ratio")]
        Ratio,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SampledData", "http://hl7.org/fhir/data-types"), Description("SampledData")]
        SampledData,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Signature", "http://hl7.org/fhir/data-types"), Description("Signature")]
        Signature,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SimpleQuantity", "http://hl7.org/fhir/data-types"), Description("SimpleQuantity")]
        SimpleQuantity,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Timing", "http://hl7.org/fhir/data-types"), Description("Timing")]
        Timing,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("base64Binary", "http://hl7.org/fhir/data-types"), Description("base64Binary")]
        Base64Binary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/data-types"), Description("boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("code", "http://hl7.org/fhir/data-types"), Description("code")]
        Code,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/data-types"), Description("dateTime")]
        DateTime,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("decimal", "http://hl7.org/fhir/data-types"), Description("decimal")]
        Decimal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("id", "http://hl7.org/fhir/data-types"), Description("id")]
        Id,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("instant", "http://hl7.org/fhir/data-types"), Description("instant")]
        Instant,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/data-types"), Description("integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("markdown", "http://hl7.org/fhir/data-types"), Description("markdown")]
        Markdown,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("oid", "http://hl7.org/fhir/data-types"), Description("oid")]
        Oid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("positiveInt", "http://hl7.org/fhir/data-types"), Description("positiveInt")]
        PositiveInt,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("time", "http://hl7.org/fhir/data-types"), Description("time")]
        Time,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("unsignedInt", "http://hl7.org/fhir/data-types"), Description("unsignedInt")]
        UnsignedInt,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uuid", "http://hl7.org/fhir/data-types"), Description("uuid")]
        Uuid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("xhtml", "http://hl7.org/fhir/data-types"), Description("XHTML")]
        Xhtml,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Account", "http://hl7.org/fhir/resource-types"), Description("Account")]
        Account,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance", "http://hl7.org/fhir/resource-types"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment", "http://hl7.org/fhir/resource-types"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/resource-types"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent", "http://hl7.org/fhir/resource-types"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic", "http://hl7.org/fhir/resource-types"), Description("Basic")]
        Basic,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary", "http://hl7.org/fhir/resource-types"), Description("Binary")]
        Binary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite")]
        BodySite,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle", "http://hl7.org/fhir/resource-types"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan", "http://hl7.org/fhir/resource-types"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim", "http://hl7.org/fhir/resource-types"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse", "http://hl7.org/fhir/resource-types"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression", "http://hl7.org/fhir/resource-types"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication", "http://hl7.org/fhir/resource-types"), Description("Communication")]
        Communication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition", "http://hl7.org/fhir/resource-types"), Description("Composition")]
        Composition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap", "http://hl7.org/fhir/resource-types"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition", "http://hl7.org/fhir/resource-types"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Conformance", "http://hl7.org/fhir/resource-types"), Description("Conformance")]
        Conformance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract", "http://hl7.org/fhir/resource-types"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage", "http://hl7.org/fhir/resource-types"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement")]
        DataElement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue", "http://hl7.org/fhir/resource-types"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/resource-types"), Description("Device")]
        Device,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent")]
        DeviceComponent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric", "http://hl7.org/fhir/resource-types"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceUseRequest")]
        DeviceUseRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement", "http://hl7.org/fhir/resource-types"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticOrder", "http://hl7.org/fhir/resource-types"), Description("DiagnosticOrder")]
        DiagnosticOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport", "http://hl7.org/fhir/resource-types"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest", "http://hl7.org/fhir/resource-types"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference", "http://hl7.org/fhir/resource-types"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource", "http://hl7.org/fhir/resource-types"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse")]
        EligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/resource-types"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/resource-types"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse", "http://hl7.org/fhir/resource-types"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare", "http://hl7.org/fhir/resource-types"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit", "http://hl7.org/fhir/resource-types"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory", "http://hl7.org/fhir/resource-types"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag", "http://hl7.org/fhir/resource-types"), Description("Flag")]
        Flag,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal", "http://hl7.org/fhir/resource-types"), Description("Goal")]
        Goal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group", "http://hl7.org/fhir/resource-types"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService", "http://hl7.org/fhir/resource-types"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingObjectSelection", "http://hl7.org/fhir/resource-types"), Description("ImagingObjectSelection")]
        ImagingObjectSelection,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy", "http://hl7.org/fhir/resource-types"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization", "http://hl7.org/fhir/resource-types"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide", "http://hl7.org/fhir/resource-types"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List", "http://hl7.org/fhir/resource-types"), Description("List")]
        List,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location", "http://hl7.org/fhir/resource-types"), Description("Location")]
        Location,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media", "http://hl7.org/fhir/resource-types"), Description("Media")]
        Media,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication", "http://hl7.org/fhir/resource-types"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration", "http://hl7.org/fhir/resource-types"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense", "http://hl7.org/fhir/resource-types"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationOrder", "http://hl7.org/fhir/resource-types"), Description("MedicationOrder")]
        MedicationOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem", "http://hl7.org/fhir/resource-types"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/resource-types"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation", "http://hl7.org/fhir/resource-types"), Description("Observation")]
        Observation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition", "http://hl7.org/fhir/resource-types"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome", "http://hl7.org/fhir/resource-types"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Order", "http://hl7.org/fhir/resource-types"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OrderResponse", "http://hl7.org/fhir/resource-types"), Description("OrderResponse")]
        OrderResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization", "http://hl7.org/fhir/resource-types"), Description("Organization")]
        Organization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters", "http://hl7.org/fhir/resource-types"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient", "http://hl7.org/fhir/resource-types"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice", "http://hl7.org/fhir/resource-types"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation", "http://hl7.org/fhir/resource-types"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person", "http://hl7.org/fhir/resource-types"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/resource-types"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure", "http://hl7.org/fhir/resource-types"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse")]
        ProcessResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance", "http://hl7.org/fhir/resource-types"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire", "http://hl7.org/fhir/resource-types"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse", "http://hl7.org/fhir/resource-types"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest")]
        ReferralRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/resource-types"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource", "http://hl7.org/fhir/resource-types"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment", "http://hl7.org/fhir/resource-types"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule", "http://hl7.org/fhir/resource-types"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter", "http://hl7.org/fhir/resource-types"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot", "http://hl7.org/fhir/resource-types"), Description("Slot")]
        Slot,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen", "http://hl7.org/fhir/resource-types"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition", "http://hl7.org/fhir/resource-types"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription", "http://hl7.org/fhir/resource-types"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance", "http://hl7.org/fhir/resource-types"), Description("Substance")]
        Substance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery", "http://hl7.org/fhir/resource-types"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/resource-types"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript", "http://hl7.org/fhir/resource-types"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet", "http://hl7.org/fhir/resource-types"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
    }

    /// <summary>
    /// The status of the response to an order.
    /// (url: http://hl7.org/fhir/ValueSet/order-status)
    /// </summary>
    [FhirEnumeration("OrderStatus")]
    public enum OrderStatus
    {
        /// <summary>
        /// The order is known, but no processing has occurred at this time
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("pending", "http://hl7.org/fhir/order-status"), Description("Pending")]
        Pending,
        /// <summary>
        /// The order is undergoing initial processing to determine whether it will be accepted (usually this involves human review)
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("review", "http://hl7.org/fhir/order-status"), Description("Review")]
        Review,
        /// <summary>
        /// The order was rejected because of a workflow/business logic reason
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/order-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The order was unable to be processed because of a technical error (i.e. unexpected error)
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/order-status"), Description("Error")]
        Error,
        /// <summary>
        /// The order has been accepted, and work is in progress.
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/order-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// Processing the order was halted at the initiators request.
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/order-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// The order has been cancelled and replaced by another.
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("replaced", "http://hl7.org/fhir/order-status"), Description("Replaced")]
        Replaced,
        /// <summary>
        /// Processing the order was stopped because of some workflow/business logic reason.
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/order-status"), Description("Aborted")]
        Aborted,
        /// <summary>
        /// The order has been completed.
        /// (system: http://hl7.org/fhir/order-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/order-status"), Description("Completed")]
        Completed,
    }

    /// <summary>
    /// The type of link between this patient resource and another patient resource.
    /// (url: http://hl7.org/fhir/ValueSet/link-type)
    /// </summary>
    [FhirEnumeration("LinkType")]
    public enum LinkType
    {
        /// <summary>
        /// The patient resource containing this link must no longer be used. The link points forward to another patient resource that must be used in lieu of the patient resource that contains this link.
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("replace", "http://hl7.org/fhir/link-type"), Description("Replace")]
        Replace,
        /// <summary>
        /// The patient resource containing this link is in use and valid but not considered the main source of information about a patient. The link points forward to another patient resource that should be consulted to retrieve additional patient information.
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("refer", "http://hl7.org/fhir/link-type"), Description("Refer")]
        Refer,
        /// <summary>
        /// The patient resource containing this link is in use and valid, but points to another patient resource that is known to contain data about the same person. Data in this resource might overlap or contradict information found in the other patient resource. This link does not indicate any relative importance of the resources concerned, and both should be regarded as equally valid.
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("seealso", "http://hl7.org/fhir/link-type"), Description("See also")]
        Seealso,
    }

    /// <summary>
    /// A code specifying the state of the procedure.
    /// (url: http://hl7.org/fhir/ValueSet/procedure-status)
    /// </summary>
    [FhirEnumeration("ProcedureStatus")]
    public enum ProcedureStatus
    {
        /// <summary>
        /// The procedure is still occurring.
        /// (system: http://hl7.org/fhir/procedure-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/procedure-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// The procedure was terminated without completing successfully.
        /// (system: http://hl7.org/fhir/procedure-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/procedure-status"), Description("Aboted")]
        Aborted,
        /// <summary>
        /// All actions involved in the procedure have taken place.
        /// (system: http://hl7.org/fhir/procedure-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/procedure-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The statement was entered in error and Is not valid.
        /// (system: http://hl7.org/fhir/procedure-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/procedure-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The status of the request.
    /// (url: http://hl7.org/fhir/ValueSet/procedure-request-status)
    /// </summary>
    [FhirEnumeration("ProcedureRequestStatus")]
    public enum ProcedureRequestStatus
    {
        /// <summary>
        /// The request has been proposed.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/procedure-request-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// The request is in preliminary form, prior to being requested.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/procedure-request-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// The request has been placed.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/procedure-request-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// The receiving system has received the request but not yet decided whether it will be performed.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("received", "http://hl7.org/fhir/procedure-request-status"), Description("Received")]
        Received,
        /// <summary>
        /// The receiving system has accepted the request, but work has not yet commenced.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/procedure-request-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The work to fulfill the request is happening.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/procedure-request-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// The work has been completed, the report(s) released, and no further work is planned.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/procedure-request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// The request has been held by originating system/user request.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/procedure-request-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// The receiving system has declined to fulfill the request.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/procedure-request-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The request was attempted, but due to some procedural error, it could not be completed.
        /// (system: http://hl7.org/fhir/procedure-request-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/procedure-request-status"), Description("Aborted")]
        Aborted,
    }

    /// <summary>
    /// The priority of the request.
    /// (url: http://hl7.org/fhir/ValueSet/procedure-request-priority)
    /// </summary>
    [FhirEnumeration("ProcedureRequestPriority")]
    public enum ProcedureRequestPriority
    {
        /// <summary>
        /// The request has a normal priority.
        /// (system: http://hl7.org/fhir/procedure-request-priority)
        /// </summary>
        [EnumLiteral("routine", "http://hl7.org/fhir/procedure-request-priority"), Description("Routine")]
        Routine,
        /// <summary>
        /// The request should be done urgently.
        /// (system: http://hl7.org/fhir/procedure-request-priority)
        /// </summary>
        [EnumLiteral("urgent", "http://hl7.org/fhir/procedure-request-priority"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// The request is time-critical.
        /// (system: http://hl7.org/fhir/procedure-request-priority)
        /// </summary>
        [EnumLiteral("stat", "http://hl7.org/fhir/procedure-request-priority"), Description("Stat")]
        Stat,
        /// <summary>
        /// The request should be acted on as soon as possible.
        /// (system: http://hl7.org/fhir/procedure-request-priority)
        /// </summary>
        [EnumLiteral("asap", "http://hl7.org/fhir/procedure-request-priority"), Description("ASAP")]
        Asap,
    }

    /// <summary>
    /// How an entity was used in an activity.
    /// (url: http://hl7.org/fhir/ValueSet/provenance-entity-role)
    /// </summary>
    [FhirEnumeration("ProvenanceEntityRole")]
    public enum ProvenanceEntityRole
    {
        /// <summary>
        /// A transformation of an entity into another, an update of an entity resulting in a new one, or the construction of a new entity based on a preexisting entity.
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("derivation", "http://hl7.org/fhir/provenance-entity-role"), Description("Derivation")]
        Derivation,
        /// <summary>
        /// A derivation for which the resulting entity is a revised version of some original.
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("revision", "http://hl7.org/fhir/provenance-entity-role"), Description("Revision")]
        Revision,
        /// <summary>
        /// The repeat of (some or all of) an entity, such as text or image, by someone who may or may not be its original author.
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("quotation", "http://hl7.org/fhir/provenance-entity-role"), Description("Quotation")]
        Quotation,
        /// <summary>
        /// A primary source for a topic refers to something produced by some agent with direct experience and knowledge about the topic, at the time of the topic's study, without benefit from hindsight.
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("source", "http://hl7.org/fhir/provenance-entity-role"), Description("Source")]
        Source,
    }

    /// <summary>
    /// Lifecycle status of the questionnaire.
    /// (url: http://hl7.org/fhir/ValueSet/questionnaire-status)
    /// </summary>
    [FhirEnumeration("QuestionnaireStatus")]
    public enum QuestionnaireStatus
    {
        /// <summary>
        /// This Questionnaire is not ready for official use.
        /// (system: http://hl7.org/fhir/questionnaire-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/questionnaire-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// This Questionnaire is ready for use.
        /// (system: http://hl7.org/fhir/questionnaire-status)
        /// </summary>
        [EnumLiteral("published", "http://hl7.org/fhir/questionnaire-status"), Description("Published")]
        Published,
        /// <summary>
        /// This Questionnaire should no longer be used to gather data.
        /// (system: http://hl7.org/fhir/questionnaire-status)
        /// </summary>
        [EnumLiteral("retired", "http://hl7.org/fhir/questionnaire-status"), Description("Retired")]
        Retired,
    }

    /// <summary>
    /// The expected format of an answer.
    /// (url: http://hl7.org/fhir/ValueSet/answer-format)
    /// </summary>
    [FhirEnumeration("AnswerFormat")]
    public enum AnswerFormat
    {
        /// <summary>
        /// Answer is a yes/no answer.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/answer-format"), Description("Boolean")]
        Boolean,
        /// <summary>
        /// Answer is a floating point number.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("decimal", "http://hl7.org/fhir/answer-format"), Description("Decimal")]
        Decimal,
        /// <summary>
        /// Answer is an integer.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/answer-format"), Description("Integer")]
        Integer,
        /// <summary>
        /// Answer is a date.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/answer-format"), Description("Date")]
        Date,
        /// <summary>
        /// Answer is a date and time.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/answer-format"), Description("Date Time")]
        DateTime,
        /// <summary>
        /// Answer is a system timestamp.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("instant", "http://hl7.org/fhir/answer-format"), Description("Instant")]
        Instant,
        /// <summary>
        /// Answer is a time (hour/minute/second) independent of date.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("time", "http://hl7.org/fhir/answer-format"), Description("Time")]
        Time,
        /// <summary>
        /// Answer is a short (few words to short sentence) free-text entry.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/answer-format"), Description("String")]
        String,
        /// <summary>
        /// Answer is a long (potentially multi-paragraph) free-text entry (still captured as a string).
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("text", "http://hl7.org/fhir/answer-format"), Description("Text")]
        Text,
        /// <summary>
        /// Answer is a url (website, FTP site, etc.).
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("url", "http://hl7.org/fhir/answer-format"), Description("Url")]
        Url,
        /// <summary>
        /// Answer is a Coding drawn from a list of options.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("choice", "http://hl7.org/fhir/answer-format"), Description("Choice")]
        Choice,
        /// <summary>
        /// Answer is a Coding drawn from a list of options or a free-text entry.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("open-choice", "http://hl7.org/fhir/answer-format"), Description("Open Choice")]
        OpenChoice,
        /// <summary>
        /// Answer is binary content such as a image, PDF, etc.
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("attachment", "http://hl7.org/fhir/answer-format"), Description("Attachment")]
        Attachment,
        /// <summary>
        /// Answer is a reference to another resource (practitioner, organization, etc.).
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/answer-format"), Description("Reference")]
        Reference,
        /// <summary>
        /// Answer is a combination of a numeric value and unit, potentially with a comparator (&lt;, &gt;, etc.).
        /// (system: http://hl7.org/fhir/answer-format)
        /// </summary>
        [EnumLiteral("quantity", "http://hl7.org/fhir/answer-format"), Description("Quantity")]
        Quantity,
    }

    /// <summary>
    /// Lifecycle status of the questionnaire response.
    /// (url: http://hl7.org/fhir/ValueSet/questionnaire-answers-status)
    /// </summary>
    [FhirEnumeration("QuestionnaireResponseStatus")]
    public enum QuestionnaireResponseStatus
    {
        /// <summary>
        /// This QuestionnaireResponse has been partially filled out with answers, but changes or additions are still expected to be made to it.
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/questionnaire-answers-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// This QuestionnaireResponse has been filled out with answers, and the current content is regarded as definitive.
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// This QuestionnaireResponse has been filled out with answers, then marked as complete, yet changes or additions have been made to it afterwards.
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Amended")]
        Amended,
    }

    /// <summary>
    /// The status of the referral.
    /// (url: http://hl7.org/fhir/ValueSet/referralstatus)
    /// </summary>
    [FhirEnumeration("ReferralStatus")]
    public enum ReferralStatus
    {
        /// <summary>
        /// A draft referral that has yet to be send.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/referralstatus"), Description("Draft")]
        Draft,
        /// <summary>
        /// The referral has been transmitted, but not yet acknowledged by the recipient.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/referralstatus"), Description("Requested")]
        Requested,
        /// <summary>
        /// The referral has been acknowledged by the recipient, and is in the process of being actioned.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/referralstatus"), Description("Active")]
        Active,
        /// <summary>
        /// The referral has been cancelled without being completed. For example it is no longer needed.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/referralstatus"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// The recipient has agreed to deliver the care requested by the referral.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/referralstatus"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The recipient has declined to accept the referral.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/referralstatus"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// The referral has been completely actioned.
        /// (system: http://hl7.org/fhir/referralstatus)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/referralstatus"), Description("Completed")]
        Completed,
    }

    /// <summary>
    /// The free/busy status of a slot.
    /// (url: http://hl7.org/fhir/ValueSet/slotstatus)
    /// </summary>
    [FhirEnumeration("SlotStatus")]
    public enum SlotStatus
    {
        /// <summary>
        /// Indicates that the time interval is busy because one  or more events have been scheduled for that interval.
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("busy", "http://hl7.org/fhir/slotstatus"), Description("Busy")]
        Busy,
        /// <summary>
        /// Indicates that the time interval is free for scheduling.
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("free", "http://hl7.org/fhir/slotstatus"), Description("Free")]
        Free,
        /// <summary>
        /// Indicates that the time interval is busy and that the interval can not be scheduled.
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("busy-unavailable", "http://hl7.org/fhir/slotstatus"), Description("Busy (Unavailable)")]
        BusyUnavailable,
        /// <summary>
        /// Indicates that the time interval is busy because one or more events have been tentatively scheduled for that interval.
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("busy-tentative", "http://hl7.org/fhir/slotstatus"), Description("Busy (Tentative)")]
        BusyTentative,
    }

    /// <summary>
    /// Defines the type of structure that a definition is describing.
    /// (url: http://hl7.org/fhir/ValueSet/structure-definition-kind)
    /// </summary>
    [FhirEnumeration("StructureDefinitionKind")]
    public enum StructureDefinitionKind
    {
        /// <summary>
        /// A data type - either a primitive or complex structure that defines a set of data elements. These can be used throughout Resource and extension definitions.
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("datatype", "http://hl7.org/fhir/structure-definition-kind"), Description("Data Type")]
        Datatype,
        /// <summary>
        /// A resource defined by the FHIR specification.
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("resource", "http://hl7.org/fhir/structure-definition-kind"), Description("Resource")]
        Resource,
        /// <summary>
        /// A logical model - a conceptual package of data that will be mapped to resources for implementation.
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("logical", "http://hl7.org/fhir/structure-definition-kind"), Description("Logical Model")]
        Logical,
    }

    /// <summary>
    /// Either a resource or a data type.
    /// (url: http://hl7.org/fhir/ValueSet/defined-types)
    /// </summary>
    [FhirEnumeration("FHIRDefinedType")]
    public enum FHIRDefinedType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Address", "http://hl7.org/fhir/data-types"), Description("Address")]
        Address,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Age", "http://hl7.org/fhir/data-types"), Description("Age")]
        Age,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Annotation", "http://hl7.org/fhir/data-types"), Description("Annotation")]
        Annotation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Attachment", "http://hl7.org/fhir/data-types"), Description("Attachment")]
        Attachment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("BackboneElement", "http://hl7.org/fhir/data-types"), Description("BackboneElement")]
        BackboneElement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("CodeableConcept", "http://hl7.org/fhir/data-types"), Description("CodeableConcept")]
        CodeableConcept,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Coding", "http://hl7.org/fhir/data-types"), Description("Coding")]
        Coding,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ContactPoint", "http://hl7.org/fhir/data-types"), Description("ContactPoint")]
        ContactPoint,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Count", "http://hl7.org/fhir/data-types"), Description("Count")]
        Count,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Distance", "http://hl7.org/fhir/data-types"), Description("Distance")]
        Distance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Duration", "http://hl7.org/fhir/data-types"), Description("Duration")]
        Duration,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Element", "http://hl7.org/fhir/data-types"), Description("Element")]
        Element,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ElementDefinition", "http://hl7.org/fhir/data-types"), Description("ElementDefinition")]
        ElementDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Extension", "http://hl7.org/fhir/data-types"), Description("Extension")]
        Extension,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("HumanName", "http://hl7.org/fhir/data-types"), Description("HumanName")]
        HumanName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Identifier", "http://hl7.org/fhir/data-types"), Description("Identifier")]
        Identifier,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Meta", "http://hl7.org/fhir/data-types"), Description("Meta")]
        Meta,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Money", "http://hl7.org/fhir/data-types"), Description("Money")]
        Money,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Narrative", "http://hl7.org/fhir/data-types"), Description("Narrative")]
        Narrative,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Period", "http://hl7.org/fhir/data-types"), Description("Period")]
        Period,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Quantity", "http://hl7.org/fhir/data-types"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Range", "http://hl7.org/fhir/data-types"), Description("Range")]
        Range,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Ratio", "http://hl7.org/fhir/data-types"), Description("Ratio")]
        Ratio,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Reference", "http://hl7.org/fhir/data-types"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SampledData", "http://hl7.org/fhir/data-types"), Description("SampledData")]
        SampledData,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Signature", "http://hl7.org/fhir/data-types"), Description("Signature")]
        Signature,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("SimpleQuantity", "http://hl7.org/fhir/data-types"), Description("SimpleQuantity")]
        SimpleQuantity,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("Timing", "http://hl7.org/fhir/data-types"), Description("Timing")]
        Timing,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("base64Binary", "http://hl7.org/fhir/data-types"), Description("base64Binary")]
        Base64Binary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/data-types"), Description("boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("code", "http://hl7.org/fhir/data-types"), Description("code")]
        Code,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/data-types"), Description("date")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/data-types"), Description("dateTime")]
        DateTime,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("decimal", "http://hl7.org/fhir/data-types"), Description("decimal")]
        Decimal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("id", "http://hl7.org/fhir/data-types"), Description("id")]
        Id,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("instant", "http://hl7.org/fhir/data-types"), Description("instant")]
        Instant,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/data-types"), Description("integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("markdown", "http://hl7.org/fhir/data-types"), Description("markdown")]
        Markdown,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("oid", "http://hl7.org/fhir/data-types"), Description("oid")]
        Oid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("positiveInt", "http://hl7.org/fhir/data-types"), Description("positiveInt")]
        PositiveInt,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/data-types"), Description("string")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("time", "http://hl7.org/fhir/data-types"), Description("time")]
        Time,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("unsignedInt", "http://hl7.org/fhir/data-types"), Description("unsignedInt")]
        UnsignedInt,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uri", "http://hl7.org/fhir/data-types"), Description("uri")]
        Uri,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("uuid", "http://hl7.org/fhir/data-types"), Description("uuid")]
        Uuid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("xhtml", "http://hl7.org/fhir/data-types"), Description("XHTML")]
        Xhtml,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Account", "http://hl7.org/fhir/resource-types"), Description("Account")]
        Account,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance", "http://hl7.org/fhir/resource-types"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment", "http://hl7.org/fhir/resource-types"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/resource-types"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent", "http://hl7.org/fhir/resource-types"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic", "http://hl7.org/fhir/resource-types"), Description("Basic")]
        Basic,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary", "http://hl7.org/fhir/resource-types"), Description("Binary")]
        Binary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite")]
        BodySite,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle", "http://hl7.org/fhir/resource-types"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan", "http://hl7.org/fhir/resource-types"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim", "http://hl7.org/fhir/resource-types"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse", "http://hl7.org/fhir/resource-types"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression", "http://hl7.org/fhir/resource-types"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication", "http://hl7.org/fhir/resource-types"), Description("Communication")]
        Communication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition", "http://hl7.org/fhir/resource-types"), Description("Composition")]
        Composition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap", "http://hl7.org/fhir/resource-types"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition", "http://hl7.org/fhir/resource-types"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Conformance", "http://hl7.org/fhir/resource-types"), Description("Conformance")]
        Conformance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract", "http://hl7.org/fhir/resource-types"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage", "http://hl7.org/fhir/resource-types"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement")]
        DataElement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue", "http://hl7.org/fhir/resource-types"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/resource-types"), Description("Device")]
        Device,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent")]
        DeviceComponent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric", "http://hl7.org/fhir/resource-types"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceUseRequest")]
        DeviceUseRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement", "http://hl7.org/fhir/resource-types"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticOrder", "http://hl7.org/fhir/resource-types"), Description("DiagnosticOrder")]
        DiagnosticOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport", "http://hl7.org/fhir/resource-types"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest", "http://hl7.org/fhir/resource-types"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference", "http://hl7.org/fhir/resource-types"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource", "http://hl7.org/fhir/resource-types"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse")]
        EligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/resource-types"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/resource-types"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse", "http://hl7.org/fhir/resource-types"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare", "http://hl7.org/fhir/resource-types"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit", "http://hl7.org/fhir/resource-types"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory", "http://hl7.org/fhir/resource-types"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag", "http://hl7.org/fhir/resource-types"), Description("Flag")]
        Flag,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal", "http://hl7.org/fhir/resource-types"), Description("Goal")]
        Goal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group", "http://hl7.org/fhir/resource-types"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService", "http://hl7.org/fhir/resource-types"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingObjectSelection", "http://hl7.org/fhir/resource-types"), Description("ImagingObjectSelection")]
        ImagingObjectSelection,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy", "http://hl7.org/fhir/resource-types"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization", "http://hl7.org/fhir/resource-types"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide", "http://hl7.org/fhir/resource-types"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List", "http://hl7.org/fhir/resource-types"), Description("List")]
        List,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location", "http://hl7.org/fhir/resource-types"), Description("Location")]
        Location,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media", "http://hl7.org/fhir/resource-types"), Description("Media")]
        Media,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication", "http://hl7.org/fhir/resource-types"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration", "http://hl7.org/fhir/resource-types"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense", "http://hl7.org/fhir/resource-types"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationOrder", "http://hl7.org/fhir/resource-types"), Description("MedicationOrder")]
        MedicationOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem", "http://hl7.org/fhir/resource-types"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/resource-types"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation", "http://hl7.org/fhir/resource-types"), Description("Observation")]
        Observation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition", "http://hl7.org/fhir/resource-types"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome", "http://hl7.org/fhir/resource-types"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Order", "http://hl7.org/fhir/resource-types"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OrderResponse", "http://hl7.org/fhir/resource-types"), Description("OrderResponse")]
        OrderResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization", "http://hl7.org/fhir/resource-types"), Description("Organization")]
        Organization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters", "http://hl7.org/fhir/resource-types"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient", "http://hl7.org/fhir/resource-types"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice", "http://hl7.org/fhir/resource-types"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation", "http://hl7.org/fhir/resource-types"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person", "http://hl7.org/fhir/resource-types"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/resource-types"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure", "http://hl7.org/fhir/resource-types"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse")]
        ProcessResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance", "http://hl7.org/fhir/resource-types"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire", "http://hl7.org/fhir/resource-types"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse", "http://hl7.org/fhir/resource-types"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest")]
        ReferralRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/resource-types"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource", "http://hl7.org/fhir/resource-types"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment", "http://hl7.org/fhir/resource-types"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule", "http://hl7.org/fhir/resource-types"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter", "http://hl7.org/fhir/resource-types"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot", "http://hl7.org/fhir/resource-types"), Description("Slot")]
        Slot,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen", "http://hl7.org/fhir/resource-types"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition", "http://hl7.org/fhir/resource-types"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription", "http://hl7.org/fhir/resource-types"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance", "http://hl7.org/fhir/resource-types"), Description("Substance")]
        Substance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery", "http://hl7.org/fhir/resource-types"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/resource-types"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript", "http://hl7.org/fhir/resource-types"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet", "http://hl7.org/fhir/resource-types"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
    }

    /// <summary>
    /// How an extension context is interpreted.
    /// (url: http://hl7.org/fhir/ValueSet/extension-context)
    /// </summary>
    [FhirEnumeration("ExtensionContext")]
    public enum ExtensionContext
    {
        /// <summary>
        /// The context is all elements matching a particular resource element path.
        /// (system: http://hl7.org/fhir/extension-context)
        /// </summary>
        [EnumLiteral("resource", "http://hl7.org/fhir/extension-context"), Description("Resource")]
        Resource,
        /// <summary>
        /// The context is all nodes matching a particular data type element path (root or repeating element) or all elements referencing a particular primitive data type (expressed as the datatype name).
        /// (system: http://hl7.org/fhir/extension-context)
        /// </summary>
        [EnumLiteral("datatype", "http://hl7.org/fhir/extension-context"), Description("Datatype")]
        Datatype,
        /// <summary>
        /// The context is all nodes whose mapping to a specified reference model corresponds to a particular mapping structure.  The context identifies the mapping target. The mapping should clearly identify where such an extension could be used.
        /// (system: http://hl7.org/fhir/extension-context)
        /// </summary>
        [EnumLiteral("mapping", "http://hl7.org/fhir/extension-context"), Description("Mapping")]
        Mapping,
        /// <summary>
        /// The context is a particular extension from a particular profile, a uri that identifies the extension definition.
        /// (system: http://hl7.org/fhir/extension-context)
        /// </summary>
        [EnumLiteral("extension", "http://hl7.org/fhir/extension-context"), Description("Extension")]
        Extension,
    }

    /// <summary>
    /// Status of the supply delivery.
    /// (url: http://hl7.org/fhir/ValueSet/supplydelivery-status)
    /// </summary>
    [FhirEnumeration("SupplyDeliveryStatus")]
    public enum SupplyDeliveryStatus
    {
        /// <summary>
        /// Supply has been requested, but not delivered.
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/supplydelivery-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// Supply has been delivered ("completed").
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/supplydelivery-status"), Description("Delivered")]
        Completed,
        /// <summary>
        /// Dispensing was not completed.
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("abandoned", "http://hl7.org/fhir/supplydelivery-status"), Description("Abandoned")]
        Abandoned,
    }

    /// <summary>
    /// Status of the supply request
    /// (url: http://hl7.org/fhir/ValueSet/supplyrequest-status)
    /// </summary>
    [FhirEnumeration("SupplyRequestStatus")]
    public enum SupplyRequestStatus
    {
        /// <summary>
        /// Supply has been requested, but not dispensed.
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/supplyrequest-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// Supply has been received by the requestor.
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/supplyrequest-status"), Description("Received")]
        Completed,
        /// <summary>
        /// The supply will not be completed because the supplier was unable or unwilling to supply the item.
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("failed", "http://hl7.org/fhir/supplyrequest-status"), Description("Failed")]
        Failed,
        /// <summary>
        /// The orderer of the supply cancelled the request.
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/supplyrequest-status"), Description("Cancelled")]
        Cancelled,
    }

    /// <summary>
    /// The content or mime type.<br/>
    /// <br/>
    /// The content type or mime type to be specified in Accept or Content-Type header.
    /// (url: http://hl7.org/fhir/ValueSet/content-type)
    /// </summary>
    [FhirEnumeration("ContentType")]
    public enum ContentType
    {
        /// <summary>
        /// XML content-type corresponding to the application/xml+fhir mime-type.
        /// (system: http://hl7.org/fhir/content-type)
        /// </summary>
        [EnumLiteral("xml", "http://hl7.org/fhir/content-type"), Description("xml")]
        Xml,
        /// <summary>
        /// JSON content-type corresponding to the application/json+fhir mime-type.
        /// (system: http://hl7.org/fhir/content-type)
        /// </summary>
        [EnumLiteral("json", "http://hl7.org/fhir/content-type"), Description("json")]
        Json,
    }

    /// <summary>
    /// The type of operator to use for assertion.<br/>
    /// <br/>
    /// The type of operator to use for assertions.
    /// (url: http://hl7.org/fhir/ValueSet/assert-operator-codes)
    /// </summary>
    [FhirEnumeration("AssertionOperatorType")]
    public enum AssertionOperatorType
    {
        /// <summary>
        /// Default value. Equals comparison.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("equals", "http://hl7.org/fhir/assert-operator-codes"), Description("equals")]
        Equals,
        /// <summary>
        /// Not equals comparison.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notEquals", "http://hl7.org/fhir/assert-operator-codes"), Description("notEquals")]
        NotEquals,
        /// <summary>
        /// Compare value within a known set of values.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/assert-operator-codes"), Description("in")]
        In,
        /// <summary>
        /// Compare value not within a known set of values.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notIn", "http://hl7.org/fhir/assert-operator-codes"), Description("notIn")]
        NotIn,
        /// <summary>
        /// Compare value to be greater than a known value.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("greaterThan", "http://hl7.org/fhir/assert-operator-codes"), Description("greaterThan")]
        GreaterThan,
        /// <summary>
        /// Compare value to be less than a known value.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("lessThan", "http://hl7.org/fhir/assert-operator-codes"), Description("lessThan")]
        LessThan,
        /// <summary>
        /// Compare value is empty.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("empty", "http://hl7.org/fhir/assert-operator-codes"), Description("empty")]
        Empty,
        /// <summary>
        /// Compare value is not empty.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notEmpty", "http://hl7.org/fhir/assert-operator-codes"), Description("notEmpty")]
        NotEmpty,
        /// <summary>
        /// Compare value string contains a known value.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("contains", "http://hl7.org/fhir/assert-operator-codes"), Description("contains")]
        Contains,
        /// <summary>
        /// Compare value string does not contain a known value.
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notContains", "http://hl7.org/fhir/assert-operator-codes"), Description("notContains")]
        NotContains,
    }

    /// <summary>
    /// Telecommunications form for contact point
    /// (url: http://hl7.org/fhir/ValueSet/contact-point-system)
    /// </summary>
    [FhirEnumeration("ContactPointSystem")]
    public enum ContactPointSystem
    {
        /// <summary>
        /// The value is a telephone number used for voice calls. Use of full international numbers starting with + is recommended to enable automatic dialing support but not required.
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("phone", "http://hl7.org/fhir/contact-point-system"), Description("Phone")]
        Phone,
        /// <summary>
        /// The value is a fax machine. Use of full international numbers starting with + is recommended to enable automatic dialing support but not required.
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("fax", "http://hl7.org/fhir/contact-point-system"), Description("Fax")]
        Fax,
        /// <summary>
        /// The value is an email address.
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("email", "http://hl7.org/fhir/contact-point-system"), Description("Email")]
        Email,
        /// <summary>
        /// The value is a pager number. These may be local pager numbers that are only usable on a particular pager system.
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("pager", "http://hl7.org/fhir/contact-point-system"), Description("Pager")]
        Pager,
        /// <summary>
        /// A contact that is not a phone, fax, or email address. The format of the value SHOULD be a URL. This is intended for various personal contacts including blogs, Twitter, Facebook, etc. Do not use for email addresses. If this is not a URL, then it will require human interpretation.
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/contact-point-system"), Description("URL")]
        Other,
    }

    /// <summary>
    /// Real world event that the relating to the schedule.
    /// (url: http://hl7.org/fhir/ValueSet/event-timing)
    /// </summary>
    [FhirEnumeration("EventTiming")]
    public enum EventTiming
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("HS", "http://hl7.org/fhir/v3/TimingEvent"), Description("HS")]
        HS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("WAKE", "http://hl7.org/fhir/v3/TimingEvent"), Description("WAKE")]
        WAKE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("C", "http://hl7.org/fhir/v3/TimingEvent"), Description("C")]
        C,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("CM", "http://hl7.org/fhir/v3/TimingEvent"), Description("CM")]
        CM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("CD", "http://hl7.org/fhir/v3/TimingEvent"), Description("CD")]
        CD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("CV", "http://hl7.org/fhir/v3/TimingEvent"), Description("CV")]
        CV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("AC", "http://hl7.org/fhir/v3/TimingEvent"), Description("AC")]
        AC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("ACM", "http://hl7.org/fhir/v3/TimingEvent"), Description("ACM")]
        ACM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("ACD", "http://hl7.org/fhir/v3/TimingEvent"), Description("ACD")]
        ACD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("ACV", "http://hl7.org/fhir/v3/TimingEvent"), Description("ACV")]
        ACV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("PC", "http://hl7.org/fhir/v3/TimingEvent"), Description("PC")]
        PC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("PCM", "http://hl7.org/fhir/v3/TimingEvent"), Description("PCM")]
        PCM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("PCD", "http://hl7.org/fhir/v3/TimingEvent"), Description("PCD")]
        PCD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/TimingEvent)
        /// </summary>
        [EnumLiteral("PCV", "http://hl7.org/fhir/v3/TimingEvent"), Description("PCV")]
        PCV,
    }

    /// <summary>
    /// How a property is represented on the wire.
    /// (url: http://hl7.org/fhir/ValueSet/property-representation)
    /// </summary>
    [FhirEnumeration("PropertyRepresentation")]
    public enum PropertyRepresentation
    {
        /// <summary>
        /// In XML, this property is represented as an attribute not an element.
        /// (system: http://hl7.org/fhir/property-representation)
        /// </summary>
        [EnumLiteral("xmlAttr", "http://hl7.org/fhir/property-representation"), Description("XML Attribute")]
        XmlAttr,
    }

}
