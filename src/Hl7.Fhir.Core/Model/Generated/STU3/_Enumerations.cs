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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{

    /// <summary>
    /// Indicates whether the account is available to be used.
    /// (url: http://hl7.org/fhir/ValueSet/account-status)
    /// </summary>
    [FhirEnumeration("AccountStatus")]
    public enum AccountStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/account-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/account-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/account-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/account-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/account-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/account-status"), Description("Entered in error")]
        EnteredInError,
    }

    /// <summary>
    /// The type of participant for the action
    /// (url: http://hl7.org/fhir/ValueSet/action-participant-type)
    /// </summary>
    [FhirEnumeration("ActionParticipantType")]
    public enum ActionParticipantType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("patient", "http://hl7.org/fhir/action-participant-type"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("practitioner", "http://hl7.org/fhir/action-participant-type"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("related-person", "http://hl7.org/fhir/action-participant-type"), Description("Related Person")]
        RelatedPerson,
    }

    /// <summary>
    /// Overall categorization of the event, e.g. real or potential
    /// (url: http://hl7.org/fhir/ValueSet/adverse-event-category)
    /// </summary>
    [FhirEnumeration("AdverseEventCategory")]
    public enum AdverseEventCategory
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/adverse-event-category)
        /// </summary>
        [EnumLiteral("AE", "http://hl7.org/fhir/adverse-event-category"), Description("Adverse Event")]
        AE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/adverse-event-category)
        /// </summary>
        [EnumLiteral("PAE", "http://hl7.org/fhir/adverse-event-category"), Description("Potential Adverse Event")]
        PAE,
    }

    /// <summary>
    /// TODO
    /// (url: http://hl7.org/fhir/ValueSet/adverse-event-causality)
    /// </summary>
    [FhirEnumeration("AdverseEventCausality")]
    public enum AdverseEventCausality
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/adverse-event-causality)
        /// </summary>
        [EnumLiteral("causality1", "http://hl7.org/fhir/adverse-event-causality"), Description("causality1 placeholder")]
        Causality1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/adverse-event-causality)
        /// </summary>
        [EnumLiteral("causality2", "http://hl7.org/fhir/adverse-event-causality"), Description("causality2 placeholder")]
        Causality2,
    }

    /// <summary>
    /// The clinical status of the allergy or intolerance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-clinical-status)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceClinicalStatus")]
    public enum AllergyIntoleranceClinicalStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-clinical-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/allergy-clinical-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-clinical-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/allergy-clinical-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-clinical-status)
        /// </summary>
        [EnumLiteral("resolved", "http://hl7.org/fhir/allergy-clinical-status"), Description("Resolved")]
        Resolved,
    }

    /// <summary>
    /// Assertion about certainty associated with a propensity, or potential risk, of a reaction to the identified substance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-verification-status)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceVerificationStatus")]
    public enum AllergyIntoleranceVerificationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-verification-status)
        /// </summary>
        [EnumLiteral("unconfirmed", "http://hl7.org/fhir/allergy-verification-status"), Description("Unconfirmed")]
        Unconfirmed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-verification-status)
        /// </summary>
        [EnumLiteral("confirmed", "http://hl7.org/fhir/allergy-verification-status"), Description("Confirmed")]
        Confirmed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-verification-status)
        /// </summary>
        [EnumLiteral("refuted", "http://hl7.org/fhir/allergy-verification-status"), Description("Refuted")]
        Refuted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-verification-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/allergy-verification-status"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// Category of an identified substance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-category)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceCategory")]
    public enum AllergyIntoleranceCategory
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("food", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Food")]
        Food,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("medication", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Medication")]
        Medication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("environment", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Environment")]
        Environment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-category)
        /// </summary>
        [EnumLiteral("biologic", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Biologic")]
        Biologic,
    }

    /// <summary>
    /// Estimate of the potential clinical harm, or seriousness, of a reaction to an identified substance.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-criticality)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceCriticality")]
    public enum AllergyIntoleranceCriticality
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
        /// </summary>
        [EnumLiteral("low", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("Low Risk")]
        Low,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
        /// </summary>
        [EnumLiteral("high", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("High Risk")]
        High,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
        /// </summary>
        [EnumLiteral("unable-to-assess", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("Unable to Assess Risk")]
        UnableToAssess,
    }

    /// <summary>
    /// The free/busy status of an appointment.
    /// (url: http://hl7.org/fhir/ValueSet/appointmentstatus)
    /// </summary>
    [FhirEnumeration("AppointmentStatus")]
    public enum AppointmentStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/appointmentstatus"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("pending", "http://hl7.org/fhir/appointmentstatus"), Description("Pending")]
        Pending,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("booked", "http://hl7.org/fhir/appointmentstatus"), Description("Booked")]
        Booked,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("arrived", "http://hl7.org/fhir/appointmentstatus"), Description("Arrived")]
        Arrived,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("fulfilled", "http://hl7.org/fhir/appointmentstatus"), Description("Fulfilled")]
        Fulfilled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/appointmentstatus"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("noshow", "http://hl7.org/fhir/appointmentstatus"), Description("No Show")]
        Noshow,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/appointmentstatus"), Description("Entered in error")]
        EnteredInError,
    }

    /// <summary>
    /// The type of network access point of this agent in the audit event
    /// (url: http://hl7.org/fhir/ValueSet/network-type)
    /// </summary>
    [FhirEnumeration("AuditEventAgentNetworkType")]
    public enum AuditEventAgentNetworkType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("1", "http://hl7.org/fhir/network-type"), Description("Machine Name")]
        N1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("2", "http://hl7.org/fhir/network-type"), Description("IP Address")]
        N2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("3", "http://hl7.org/fhir/network-type"), Description("Telephone Number")]
        N3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("4", "http://hl7.org/fhir/network-type"), Description("Email address")]
        N4,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/network-type)
        /// </summary>
        [EnumLiteral("5", "http://hl7.org/fhir/network-type"), Description("URI")]
        N5,
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
        [EnumLiteral("patch", "http://hl7.org/fhir/restful-interaction"), Description("patch")]
        Patch,
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
        [EnumLiteral("batch", "http://hl7.org/fhir/restful-interaction"), Description("batch")]
        Batch,
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
    /// The impact of the content of a message.
    /// (url: http://hl7.org/fhir/ValueSet/message-significance-category)
    /// </summary>
    [FhirEnumeration("MessageSignificanceCategory")]
    public enum MessageSignificanceCategory
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Consequence", "http://hl7.org/fhir/message-significance-category"), Description("Consequence")]
        Consequence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Currency", "http://hl7.org/fhir/message-significance-category"), Description("Currency")]
        Currency,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Notification", "http://hl7.org/fhir/message-significance-category"), Description("Notification")]
        Notification,
    }

    /// <summary>
    /// Indicates whether the plan is currently being acted upon, represents future intentions or is now a historical record.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-status)
    /// </summary>
    [FhirEnumeration("CarePlanStatus")]
    public enum CarePlanStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/care-plan-status"), Description("Pending")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/care-plan-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/care-plan-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/care-plan-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/care-plan-status"), Description("Entered In Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/care-plan-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/care-plan-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// Indicates where the activity is at in its overall life cycle.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-activity-status)
    /// </summary>
    [FhirEnumeration("CarePlanActivityStatus")]
    public enum CarePlanActivityStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("not-started", "http://hl7.org/fhir/care-plan-activity-status"), Description("Not Started")]
        NotStarted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("scheduled", "http://hl7.org/fhir/care-plan-activity-status"), Description("Scheduled")]
        Scheduled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/care-plan-activity-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/care-plan-activity-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/care-plan-activity-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/care-plan-activity-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/care-plan-activity-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// Complete, proposed, exploratory, other
    /// (url: http://hl7.org/fhir/ValueSet/claim-use)
    /// </summary>
    [FhirEnumeration("Use")]
    public enum Use
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("complete", "http://hl7.org/fhir/claim-use"), Description("Complete")]
        Complete,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/claim-use"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("exploratory", "http://hl7.org/fhir/claim-use"), Description("Exploratory")]
        Exploratory,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/claim-use"), Description("Other")]
        Other,
    }

    /// <summary>
    /// The workflow state of a clinical impression.
    /// (url: http://hl7.org/fhir/ValueSet/clinical-impression-status)
    /// </summary>
    [FhirEnumeration("ClinicalImpressionStatus")]
    public enum ClinicalImpressionStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/clinical-impression-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/clinical-impression-status"), Description("In progress")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/clinical-impression-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/clinical-impression-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/clinical-impression-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/clinical-impression-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// How much of the content of the code system - the concepts and codes it defines - are represented in a code system resource
    /// (url: http://hl7.org/fhir/ValueSet/codesystem-content-mode)
    /// </summary>
    [FhirEnumeration("CodeSystemContentMode")]
    public enum CodeSystemContentMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-content-mode)
        /// </summary>
        [EnumLiteral("not-present", "http://hl7.org/fhir/codesystem-content-mode"), Description("Not Present")]
        NotPresent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-content-mode)
        /// </summary>
        [EnumLiteral("example", "http://hl7.org/fhir/codesystem-content-mode"), Description("Example")]
        Example,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-content-mode)
        /// </summary>
        [EnumLiteral("fragment", "http://hl7.org/fhir/codesystem-content-mode"), Description("Fragment")]
        Fragment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-content-mode)
        /// </summary>
        [EnumLiteral("complete", "http://hl7.org/fhir/codesystem-content-mode"), Description("Complete")]
        Complete,
    }

    /// <summary>
    /// The kind of operation to perform as a part of a property based filter.
    /// (url: http://hl7.org/fhir/ValueSet/filter-operator)
    /// </summary>
    [FhirEnumeration("FilterOperator")]
    public enum FilterOperator
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("=", "http://hl7.org/fhir/filter-operator"), Description("Equals")]
        Equal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-a", "http://hl7.org/fhir/filter-operator"), Description("Is A (by subsumption)")]
        IsA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("descendent-of", "http://hl7.org/fhir/filter-operator"), Description("Descendent Of (by subsumption)")]
        DescendentOf,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("is-not-a", "http://hl7.org/fhir/filter-operator"), Description("Not (Is A) (by subsumption)")]
        IsNotA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("regex", "http://hl7.org/fhir/filter-operator"), Description("Regular Expression")]
        Regex,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/filter-operator"), Description("In Set")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("not-in", "http://hl7.org/fhir/filter-operator"), Description("Not in Set")]
        NotIn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("generalizes", "http://hl7.org/fhir/filter-operator"), Description("Generalizes (by Subsumption)")]
        Generalizes,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/filter-operator)
        /// </summary>
        [EnumLiteral("exists", "http://hl7.org/fhir/filter-operator"), Description("Exists")]
        Exists,
    }

    /// <summary>
    /// The type of a property value
    /// (url: http://hl7.org/fhir/ValueSet/concept-property-type)
    /// </summary>
    [FhirEnumeration("PropertyType")]
    public enum PropertyType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("code", "http://hl7.org/fhir/concept-property-type"), Description("code (internal reference)")]
        Code,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("Coding", "http://hl7.org/fhir/concept-property-type"), Description("Coding (external reference)")]
        Coding,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/concept-property-type"), Description("string")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/concept-property-type"), Description("integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/concept-property-type"), Description("boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/concept-property-type"), Description("dateTime")]
        DateTime,
    }

    /// <summary>
    /// Codes identifying the stage lifecycle stage of a event
    /// (url: http://hl7.org/fhir/ValueSet/event-status)
    /// </summary>
    [FhirEnumeration("EventStatus")]
    public enum EventStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("preparation", "http://hl7.org/fhir/event-status"), Description("Preparation")]
        Preparation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/event-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/event-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/event-status"), Description("Aborted")]
        Aborted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/event-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/event-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/request-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/request-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/request-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/request-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/request-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/request-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    ///  Set of codes used to value Act.Confidentiality and Role.Confidentiality attribute in accordance with the definition for concept domain "Confidentiality".
    /// (url: http://hl7.org/fhir/ValueSet/v3-ConfidentialityClassification)
    /// </summary>
    [FhirEnumeration("ConfidentialityClassification")]
    public enum ConfidentialityClassification
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("U", "http://hl7.org/fhir/v3/Confidentiality"), Description("unrestricted")]
        U,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("L", "http://hl7.org/fhir/v3/Confidentiality"), Description("low")]
        L,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("M", "http://hl7.org/fhir/v3/Confidentiality"), Description("moderate")]
        M,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("N", "http://hl7.org/fhir/v3/Confidentiality"), Description("normal")]
        N,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/v3/Confidentiality)
        /// </summary>
        [EnumLiteral("R", "http://hl7.org/fhir/v3/Confidentiality"), Description("restricted")]
        R,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("relatedto", "http://hl7.org/fhir/concept-map-equivalence"), Description("Related To")]
        Relatedto,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("equivalent", "http://hl7.org/fhir/concept-map-equivalence"), Description("Equivalent")]
        Equivalent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("equal", "http://hl7.org/fhir/concept-map-equivalence"), Description("Equal")]
        Equal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("wider", "http://hl7.org/fhir/concept-map-equivalence"), Description("Wider")]
        Wider,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("subsumes", "http://hl7.org/fhir/concept-map-equivalence"), Description("Subsumes")]
        Subsumes,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("narrower", "http://hl7.org/fhir/concept-map-equivalence"), Description("Narrower")]
        Narrower,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("specializes", "http://hl7.org/fhir/concept-map-equivalence"), Description("Specializes")]
        Specializes,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("inexact", "http://hl7.org/fhir/concept-map-equivalence"), Description("Inexact")]
        Inexact,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("unmatched", "http://hl7.org/fhir/concept-map-equivalence"), Description("Unmatched")]
        Unmatched,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-map-equivalence)
        /// </summary>
        [EnumLiteral("disjoint", "http://hl7.org/fhir/concept-map-equivalence"), Description("Disjoint")]
        Disjoint,
    }

    /// <summary>
    /// Preferred value set for Condition Clinical Status.
    /// (url: http://hl7.org/fhir/ValueSet/condition-clinical)
    /// </summary>
    [FhirEnumeration("ConditionClinicalStatusCodes")]
    public enum ConditionClinicalStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/condition-clinical)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/condition-clinical"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/condition-clinical)
        /// </summary>
        [EnumLiteral("recurrence", "http://hl7.org/fhir/condition-clinical"), Description("Recurrence")]
        Recurrence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/condition-clinical)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/condition-clinical"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/condition-clinical)
        /// </summary>
        [EnumLiteral("remission", "http://hl7.org/fhir/condition-clinical"), Description("Remission")]
        Remission,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/condition-clinical)
        /// </summary>
        [EnumLiteral("resolved", "http://hl7.org/fhir/condition-clinical"), Description("Resolved")]
        Resolved,
    }

    /// <summary>
    /// How an exception statement is applied, such as adding additional consent or removing consent
    /// (url: http://hl7.org/fhir/ValueSet/consent-except-type)
    /// </summary>
    [FhirEnumeration("ConsentExceptType")]
    public enum ConsentExceptType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-except-type)
        /// </summary>
        [EnumLiteral("deny", "http://hl7.org/fhir/consent-except-type"), Description("Opt Out")]
        Deny,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-except-type)
        /// </summary>
        [EnumLiteral("permit", "http://hl7.org/fhir/consent-except-type"), Description("Opt In")]
        Permit,
    }

    /// <summary>
    /// Codes providing the status of an observation.
    /// (url: http://hl7.org/fhir/ValueSet/observation-status)
    /// </summary>
    [FhirEnumeration("ObservationStatus")]
    public enum ObservationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("registered", "http://hl7.org/fhir/observation-status"), Description("Registered")]
        Registered,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("preliminary", "http://hl7.org/fhir/observation-status"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/observation-status"), Description("Final")]
        Final,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/observation-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("corrected", "http://hl7.org/fhir/observation-status"), Description("Corrected")]
        Corrected,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/observation-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/observation-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/observation-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// Different measurement principle supported by the device.
    /// (url: http://hl7.org/fhir/ValueSet/measurement-principle)
    /// </summary>
    [FhirEnumeration("MeasmntPrinciple")]
    public enum MeasmntPrinciple
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/measurement-principle"), Description("MSP Other")]
        Other,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("chemical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Chemical")]
        Chemical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("electrical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Electrical")]
        Electrical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("impedance", "http://hl7.org/fhir/measurement-principle"), Description("MSP Impedance")]
        Impedance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("nuclear", "http://hl7.org/fhir/measurement-principle"), Description("MSP Nuclear")]
        Nuclear,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("optical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Optical")]
        Optical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("thermal", "http://hl7.org/fhir/measurement-principle"), Description("MSP Thermal")]
        Thermal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("biological", "http://hl7.org/fhir/measurement-principle"), Description("MSP Biological")]
        Biological,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("mechanical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Mechanical")]
        Mechanical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measurement-principle)
        /// </summary>
        [EnumLiteral("acoustical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Acoustical")]
        Acoustical,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("on", "http://hl7.org/fhir/metric-operational-status"), Description("On")]
        On,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("off", "http://hl7.org/fhir/metric-operational-status"), Description("Off")]
        Off,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("standby", "http://hl7.org/fhir/metric-operational-status"), Description("Standby")]
        Standby,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/metric-operational-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/metric-operational-status"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// The status of the diagnostic report as a whole.
    /// (url: http://hl7.org/fhir/ValueSet/diagnostic-report-status)
    /// </summary>
    [FhirEnumeration("DiagnosticReportStatus")]
    public enum DiagnosticReportStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("registered", "http://hl7.org/fhir/diagnostic-report-status"), Description("Registered")]
        Registered,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("partial", "http://hl7.org/fhir/diagnostic-report-status"), Description("Partial")]
        Partial,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("preliminary", "http://hl7.org/fhir/diagnostic-report-status"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/diagnostic-report-status"), Description("Final")]
        Final,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/diagnostic-report-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("corrected", "http://hl7.org/fhir/diagnostic-report-status"), Description("Corrected")]
        Corrected,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("appended", "http://hl7.org/fhir/diagnostic-report-status"), Description("Appended")]
        Appended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/diagnostic-report-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/diagnostic-report-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/diagnostic-report-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/diagnostic-report-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The status of the episode of care.
    /// (url: http://hl7.org/fhir/ValueSet/episode-of-care-status)
    /// </summary>
    [FhirEnumeration("EpisodeOfCareStatus")]
    public enum EpisodeOfCareStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/episode-of-care-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("waitlist", "http://hl7.org/fhir/episode-of-care-status"), Description("Waitlist")]
        Waitlist,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/episode-of-care-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("onhold", "http://hl7.org/fhir/episode-of-care-status"), Description("On Hold")]
        Onhold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("finished", "http://hl7.org/fhir/episode-of-care-status"), Description("Finished")]
        Finished,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/episode-of-care-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/episode-of-care-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/episode-of-care-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// How to manage the intersection between a fixed version in a value set, and a fixed version of the system in the expansion profile
    /// (url: http://hl7.org/fhir/ValueSet/system-version-processing-mode)
    /// </summary>
    [FhirEnumeration("SystemVersionProcessingMode")]
    public enum SystemVersionProcessingMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/system-version-processing-mode)
        /// </summary>
        [EnumLiteral("default", "http://hl7.org/fhir/system-version-processing-mode"), Description("Default Version")]
        Default,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/system-version-processing-mode)
        /// </summary>
        [EnumLiteral("check", "http://hl7.org/fhir/system-version-processing-mode"), Description("Check ValueSet Version")]
        Check,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/system-version-processing-mode)
        /// </summary>
        [EnumLiteral("override", "http://hl7.org/fhir/system-version-processing-mode"), Description("Override ValueSet Version")]
        Override,
    }

    /// <summary>
    /// Indicates whether the goal has been met and is still being targeted
    /// (url: http://hl7.org/fhir/ValueSet/goal-status)
    /// </summary>
    [FhirEnumeration("GoalStatus")]
    public enum GoalStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/goal-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/goal-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/goal-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/goal-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("on-target", "http://hl7.org/fhir/goal-status"), Description("On Target")]
        OnTarget,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("ahead-of-target", "http://hl7.org/fhir/goal-status"), Description("Ahead of Target")]
        AheadOfTarget,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("behind-target", "http://hl7.org/fhir/goal-status"), Description("Behind Target")]
        BehindTarget,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("sustaining", "http://hl7.org/fhir/goal-status"), Description("Sustaining")]
        Sustaining,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("achieved", "http://hl7.org/fhir/goal-status"), Description("Achieved")]
        Achieved,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/goal-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/goal-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/goal-status"), Description("Entered In Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/goal-status"), Description("Rejected")]
        Rejected,
    }

    /// <summary>
    /// The value set to instantiate this attribute should be drawn from a terminologically robust code system that consists of or contains concepts to support describing the current status of the administered dose of vaccine.
    /// (url: http://hl7.org/fhir/ValueSet/immunization-status)
    /// </summary>
    [FhirEnumeration("ImmunizationStatusCodes")]
    public enum ImmunizationStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-admin-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-admin-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The type of the measure report
    /// (url: http://hl7.org/fhir/ValueSet/measure-report-type)
    /// </summary>
    [FhirEnumeration("MeasureReportType")]
    public enum MeasureReportType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-type)
        /// </summary>
        [EnumLiteral("individual", "http://hl7.org/fhir/measure-report-type"), Description("Individual")]
        Individual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-type)
        /// </summary>
        [EnumLiteral("patient-list", "http://hl7.org/fhir/measure-report-type"), Description("Patient List")]
        PatientList,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-type)
        /// </summary>
        [EnumLiteral("summary", "http://hl7.org/fhir/measure-report-type"), Description("Summary")]
        Summary,
    }

    /// <summary>
    /// A coded concept defining if the medication is in active use
    /// (url: http://hl7.org/fhir/ValueSet/medication-status)
    /// </summary>
    [FhirEnumeration("MedicationStatus")]
    public enum MedicationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/medication-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/medication-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// A set of codes indicating the current status of a MedicationAdministration.
    /// (url: http://hl7.org/fhir/ValueSet/medication-admin-status)
    /// </summary>
    [FhirEnumeration("MedicationAdministrationStatus")]
    public enum MedicationAdministrationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/medication-admin-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-admin-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-admin-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-admin-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-admin-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-admin-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/medication-admin-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// A coded concept specifying the state of the dispense event.
    /// (url: http://hl7.org/fhir/ValueSet/medication-dispense-status)
    /// </summary>
    [FhirEnumeration("MedicationDispenseStatus")]
    public enum MedicationDispenseStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("preparation", "http://hl7.org/fhir/medication-dispense-status"), Description("Preparation")]
        Preparation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/medication-dispense-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-dispense-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-dispense-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-dispense-status"), Description("Entered in-Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-dispense-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-dispense-status"), Description("Stopped")]
        Stopped,
    }

    /// <summary>
    /// A coded concept specifying the state of the prescribing event. Describes the lifecycle of the prescription
    /// (url: http://hl7.org/fhir/ValueSet/medication-request-status)
    /// </summary>
    [FhirEnumeration("MedicationRequestStatus")]
    public enum MedicationRequestStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/medication-request-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-request-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/medication-request-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-request-status"), Description("Entered In Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-request-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/medication-request-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/medication-request-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The kind of medication order
    /// (url: http://hl7.org/fhir/ValueSet/medication-request-intent)
    /// </summary>
    [FhirEnumeration("MedicationRequestIntent")]
    public enum MedicationRequestIntent
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-intent)
        /// </summary>
        [EnumLiteral("proposal", "http://hl7.org/fhir/medication-request-intent"), Description("Proposal")]
        Proposal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-intent)
        /// </summary>
        [EnumLiteral("plan", "http://hl7.org/fhir/medication-request-intent"), Description("Plan")]
        Plan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-intent)
        /// </summary>
        [EnumLiteral("order", "http://hl7.org/fhir/medication-request-intent"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-intent)
        /// </summary>
        [EnumLiteral("instance-order", "http://hl7.org/fhir/medication-request-intent"), Description("Instance Order")]
        InstanceOrder,
    }

    /// <summary>
    /// Identifies the level of importance to be assigned to actioning the request
    /// (url: http://hl7.org/fhir/ValueSet/medication-request-priority)
    /// </summary>
    [FhirEnumeration("MedicationRequestPriority")]
    public enum MedicationRequestPriority
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-priority)
        /// </summary>
        [EnumLiteral("routine", "http://hl7.org/fhir/medication-request-priority"), Description("Routine")]
        Routine,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-priority)
        /// </summary>
        [EnumLiteral("urgent", "http://hl7.org/fhir/medication-request-priority"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-priority)
        /// </summary>
        [EnumLiteral("stat", "http://hl7.org/fhir/medication-request-priority"), Description("Stat")]
        Stat,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-request-priority)
        /// </summary>
        [EnumLiteral("asap", "http://hl7.org/fhir/medication-request-priority"), Description("ASAP")]
        Asap,
    }

    /// <summary>
    /// A coded concept indicating the current status of a MedicationStatement.
    /// (url: http://hl7.org/fhir/ValueSet/medication-statement-status)
    /// </summary>
    [FhirEnumeration("MedicationStatementStatus")]
    public enum MedicationStatementStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/medication-statement-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/medication-statement-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-statement-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("intended", "http://hl7.org/fhir/medication-statement-status"), Description("Intended")]
        Intended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/medication-statement-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-statement-status"), Description("On Hold")]
        OnHold,
    }

    /// <summary>
    /// A coded concept identifying level of certainty if patient has taken or has not taken the medication
    /// (url: http://hl7.org/fhir/ValueSet/medication-statement-taken)
    /// </summary>
    [FhirEnumeration("MedicationStatementTaken")]
    public enum MedicationStatementTaken
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-taken)
        /// </summary>
        [EnumLiteral("y", "http://hl7.org/fhir/medication-statement-taken"), Description("Yes")]
        Y,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-taken)
        /// </summary>
        [EnumLiteral("n", "http://hl7.org/fhir/medication-statement-taken"), Description("No")]
        N,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-taken)
        /// </summary>
        [EnumLiteral("unk", "http://hl7.org/fhir/medication-statement-taken"), Description("Unknown")]
        Unk,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/medication-statement-taken)
        /// </summary>
        [EnumLiteral("na", "http://hl7.org/fhir/medication-statement-taken"), Description("Not Applicable")]
        Na,
    }

    /// <summary>
    /// Codes specifying the state of the request. Describes the lifecycle of the nutrition order.
    /// (url: http://hl7.org/fhir/ValueSet/nutrition-request-status)
    /// </summary>
    [FhirEnumeration("NutritionOrderStatus")]
    public enum NutritionOrderStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/nutrition-request-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/nutrition-request-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/nutrition-request-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/nutrition-request-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/nutrition-request-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/nutrition-request-status"), Description("On-Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/nutrition-request-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/nutrition-request-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/nutrition-request-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/nutrition-request-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// Either an abstract type, a resource or a data type.
    /// (url: http://hl7.org/fhir/ValueSet/all-types)
    /// </summary>
    [FhirEnumeration("FHIRAllTypes")]
    public enum FHIRAllTypes
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
        [EnumLiteral("ContactDetail", "http://hl7.org/fhir/data-types"), Description("ContactDetail")]
        ContactDetail,
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
        [EnumLiteral("Contributor", "http://hl7.org/fhir/data-types"), Description("Contributor")]
        Contributor,
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
        [EnumLiteral("DataRequirement", "http://hl7.org/fhir/data-types"), Description("DataRequirement")]
        DataRequirement,
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
        [EnumLiteral("Dosage", "http://hl7.org/fhir/data-types"), Description("Dosage")]
        Dosage,
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
        [EnumLiteral("ParameterDefinition", "http://hl7.org/fhir/data-types"), Description("ParameterDefinition")]
        ParameterDefinition,
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
        [EnumLiteral("RelatedArtifact", "http://hl7.org/fhir/data-types"), Description("RelatedArtifact")]
        RelatedArtifact,
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
        [EnumLiteral("TriggerDefinition", "http://hl7.org/fhir/data-types"), Description("TriggerDefinition")]
        TriggerDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("UsageContext", "http://hl7.org/fhir/data-types"), Description("UsageContext")]
        UsageContext,
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
        [EnumLiteral("ActivityDefinition", "http://hl7.org/fhir/resource-types"), Description("ActivityDefinition")]
        ActivityDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AdverseEvent", "http://hl7.org/fhir/resource-types"), Description("AdverseEvent")]
        AdverseEvent,
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
        [EnumLiteral("CapabilityStatement", "http://hl7.org/fhir/resource-types"), Description("CapabilityStatement")]
        CapabilityStatement,
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
        [EnumLiteral("CareTeam", "http://hl7.org/fhir/resource-types"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
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
        [EnumLiteral("CodeSystem", "http://hl7.org/fhir/resource-types"), Description("CodeSystem")]
        CodeSystem,
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
        [EnumLiteral("CompartmentDefinition", "http://hl7.org/fhir/resource-types"), Description("CompartmentDefinition")]
        CompartmentDefinition,
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
        [EnumLiteral("Consent", "http://hl7.org/fhir/resource-types"), Description("Consent")]
        Consent,
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
        [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceRequest")]
        DeviceRequest,
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
        [EnumLiteral("Endpoint", "http://hl7.org/fhir/resource-types"), Description("Endpoint")]
        Endpoint,
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
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile")]
        ExpansionProfile,
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
        [EnumLiteral("GraphDefinition", "http://hl7.org/fhir/resource-types"), Description("GraphDefinition")]
        GraphDefinition,
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
        [EnumLiteral("GuidanceResponse", "http://hl7.org/fhir/resource-types"), Description("GuidanceResponse")]
        GuidanceResponse,
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
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest")]
        ImagingManifest,
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
        [EnumLiteral("Library", "http://hl7.org/fhir/resource-types"), Description("Library")]
        Library,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Linkage", "http://hl7.org/fhir/resource-types"), Description("Linkage")]
        Linkage,
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
        [EnumLiteral("Measure", "http://hl7.org/fhir/resource-types"), Description("Measure")]
        Measure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MeasureReport", "http://hl7.org/fhir/resource-types"), Description("MeasureReport")]
        MeasureReport,
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
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/resource-types"), Description("MedicationRequest")]
        MedicationRequest,
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
        [EnumLiteral("MessageDefinition", "http://hl7.org/fhir/resource-types"), Description("MessageDefinition")]
        MessageDefinition,
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
        [EnumLiteral("PlanDefinition", "http://hl7.org/fhir/resource-types"), Description("PlanDefinition")]
        PlanDefinition,
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
        [EnumLiteral("PractitionerRole", "http://hl7.org/fhir/resource-types"), Description("PractitionerRole")]
        PractitionerRole,
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
        [EnumLiteral("RequestGroup", "http://hl7.org/fhir/resource-types"), Description("RequestGroup")]
        RequestGroup,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchStudy", "http://hl7.org/fhir/resource-types"), Description("ResearchStudy")]
        ResearchStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchSubject", "http://hl7.org/fhir/resource-types"), Description("ResearchSubject")]
        ResearchSubject,
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
        [EnumLiteral("Sequence", "http://hl7.org/fhir/resource-types"), Description("Sequence")]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition", "http://hl7.org/fhir/resource-types"), Description("ServiceDefinition")]
        ServiceDefinition,
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
        [EnumLiteral("StructureMap", "http://hl7.org/fhir/resource-types"), Description("StructureMap")]
        StructureMap,
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
        [EnumLiteral("Task", "http://hl7.org/fhir/resource-types"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestReport", "http://hl7.org/fhir/resource-types"), Description("TestReport")]
        TestReport,
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/abstract-types)
        /// </summary>
        [EnumLiteral("Type", "http://hl7.org/fhir/abstract-types"), Description("Type")]
        Type,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/abstract-types)
        /// </summary>
        [EnumLiteral("Any", "http://hl7.org/fhir/abstract-types"), Description("Any")]
        Any,
    }

    /// <summary>
    /// A code that describes the type of issue.
    /// (url: http://hl7.org/fhir/ValueSet/issue-type)
    /// </summary>
    [FhirEnumeration("IssueType")]
    public enum IssueType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("invalid", "http://hl7.org/fhir/issue-type"), Description("Invalid Content")]
        Invalid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("structure", "http://hl7.org/fhir/issue-type"), Description("Structural Issue")]
        Structure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("required", "http://hl7.org/fhir/issue-type"), Description("Required element missing")]
        Required,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("value", "http://hl7.org/fhir/issue-type"), Description("Element value invalid")]
        Value,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("invariant", "http://hl7.org/fhir/issue-type"), Description("Validation rule failed")]
        Invariant,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("security", "http://hl7.org/fhir/issue-type"), Description("Security Problem")]
        Security,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("login", "http://hl7.org/fhir/issue-type"), Description("Login Required")]
        Login,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/issue-type"), Description("Unknown User")]
        Unknown,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("expired", "http://hl7.org/fhir/issue-type"), Description("Session Expired")]
        Expired,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("forbidden", "http://hl7.org/fhir/issue-type"), Description("Forbidden")]
        Forbidden,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("suppressed", "http://hl7.org/fhir/issue-type"), Description("Information  Suppressed")]
        Suppressed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("processing", "http://hl7.org/fhir/issue-type"), Description("Processing Failure")]
        Processing,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("not-supported", "http://hl7.org/fhir/issue-type"), Description("Content not supported")]
        NotSupported,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("duplicate", "http://hl7.org/fhir/issue-type"), Description("Duplicate")]
        Duplicate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("not-found", "http://hl7.org/fhir/issue-type"), Description("Not Found")]
        NotFound,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("too-long", "http://hl7.org/fhir/issue-type"), Description("Content Too Long")]
        TooLong,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("code-invalid", "http://hl7.org/fhir/issue-type"), Description("Invalid Code")]
        CodeInvalid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("extension", "http://hl7.org/fhir/issue-type"), Description("Unacceptable Extension")]
        Extension,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("too-costly", "http://hl7.org/fhir/issue-type"), Description("Operation Too Costly")]
        TooCostly,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("business-rule", "http://hl7.org/fhir/issue-type"), Description("Business Rule Violation")]
        BusinessRule,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("conflict", "http://hl7.org/fhir/issue-type"), Description("Edit Version Conflict")]
        Conflict,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("incomplete", "http://hl7.org/fhir/issue-type"), Description("Incomplete Results")]
        Incomplete,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("transient", "http://hl7.org/fhir/issue-type"), Description("Transient Issue")]
        Transient,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("lock-error", "http://hl7.org/fhir/issue-type"), Description("Lock Error")]
        LockError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("no-store", "http://hl7.org/fhir/issue-type"), Description("No Store Available")]
        NoStore,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("exception", "http://hl7.org/fhir/issue-type"), Description("Exception")]
        Exception,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("timeout", "http://hl7.org/fhir/issue-type"), Description("Timeout")]
        Timeout,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("throttled", "http://hl7.org/fhir/issue-type"), Description("Throttled")]
        Throttled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("informational", "http://hl7.org/fhir/issue-type"), Description("Informational Note")]
        Informational,
    }

    /// <summary>
    /// The type of link between this patient resource and another patient resource.
    /// (url: http://hl7.org/fhir/ValueSet/link-type)
    /// </summary>
    [FhirEnumeration("LinkType")]
    public enum LinkType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("replaced-by", "http://hl7.org/fhir/link-type"), Description("Replaced-by")]
        ReplacedBy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("replaces", "http://hl7.org/fhir/link-type"), Description("Replaces")]
        Replaces,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("refer", "http://hl7.org/fhir/link-type"), Description("Refer")]
        Refer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [EnumLiteral("seealso", "http://hl7.org/fhir/link-type"), Description("See also")]
        Seealso,
    }

    /// <summary>
    /// Codes indicating the degree of authority/intentionality associated with a request
    /// (url: http://hl7.org/fhir/ValueSet/request-intent)
    /// </summary>
    [FhirEnumeration("RequestIntent")]
    public enum RequestIntent
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("proposal", "http://hl7.org/fhir/request-intent"), Description("Proposal")]
        Proposal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("plan", "http://hl7.org/fhir/request-intent"), Description("Plan")]
        Plan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("order", "http://hl7.org/fhir/request-intent"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("original-order", "http://hl7.org/fhir/request-intent"), Description("Original Order")]
        OriginalOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("reflex-order", "http://hl7.org/fhir/request-intent"), Description("Reflex Order")]
        ReflexOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("filler-order", "http://hl7.org/fhir/request-intent"), Description("Filler Order")]
        FillerOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("instance-order", "http://hl7.org/fhir/request-intent"), Description("Instance Order")]
        InstanceOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-intent)
        /// </summary>
        [EnumLiteral("option", "http://hl7.org/fhir/request-intent"), Description("Option")]
        Option,
    }

    /// <summary>
    /// How an entity was used in an activity.
    /// (url: http://hl7.org/fhir/ValueSet/provenance-entity-role)
    /// </summary>
    [FhirEnumeration("ProvenanceEntityRole")]
    public enum ProvenanceEntityRole
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("derivation", "http://hl7.org/fhir/provenance-entity-role"), Description("Derivation")]
        Derivation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("revision", "http://hl7.org/fhir/provenance-entity-role"), Description("Revision")]
        Revision,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("quotation", "http://hl7.org/fhir/provenance-entity-role"), Description("Quotation")]
        Quotation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("source", "http://hl7.org/fhir/provenance-entity-role"), Description("Source")]
        Source,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/provenance-entity-role)
        /// </summary>
        [EnumLiteral("removal", "http://hl7.org/fhir/provenance-entity-role"), Description("Removal")]
        Removal,
    }

    /// <summary>
    /// Lifecycle status of the questionnaire response.
    /// (url: http://hl7.org/fhir/ValueSet/questionnaire-answers-status)
    /// </summary>
    [FhirEnumeration("QuestionnaireResponseStatus")]
    public enum QuestionnaireResponseStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/questionnaire-answers-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-answers-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Stopped")]
        Stopped,
    }

    /// <summary>
    /// Codes that convey the current status of the research study
    /// (url: http://hl7.org/fhir/ValueSet/research-study-status)
    /// </summary>
    [FhirEnumeration("ResearchStudyStatus")]
    public enum ResearchStudyStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/research-study-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/research-study-status"), Description("In-progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/research-study-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/research-study-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/research-study-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/research-study-status"), Description("Entered in error")]
        EnteredInError,
    }

    /// <summary>
    /// Indicates the progression of a study subject through a study
    /// (url: http://hl7.org/fhir/ValueSet/research-subject-status)
    /// </summary>
    [FhirEnumeration("ResearchSubjectStatus")]
    public enum ResearchSubjectStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("candidate", "http://hl7.org/fhir/research-subject-status"), Description("Candidate")]
        Candidate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("enrolled", "http://hl7.org/fhir/research-subject-status"), Description("Enrolled")]
        Enrolled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/research-subject-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/research-subject-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("withdrawn", "http://hl7.org/fhir/research-subject-status"), Description("Withdrawn")]
        Withdrawn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/research-subject-status"), Description("Completed")]
        Completed,
    }

    /// <summary>
    /// A supported modifier for a search parameter.
    /// (url: http://hl7.org/fhir/ValueSet/search-modifier-code)
    /// </summary>
    [FhirEnumeration("SearchModifierCode")]
    public enum SearchModifierCode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("missing", "http://hl7.org/fhir/search-modifier-code"), Description("Missing")]
        Missing,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("exact", "http://hl7.org/fhir/search-modifier-code"), Description("Exact")]
        Exact,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("contains", "http://hl7.org/fhir/search-modifier-code"), Description("Contains")]
        Contains,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("not", "http://hl7.org/fhir/search-modifier-code"), Description("Not")]
        Not,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("text", "http://hl7.org/fhir/search-modifier-code"), Description("Text")]
        Text,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/search-modifier-code"), Description("In")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("not-in", "http://hl7.org/fhir/search-modifier-code"), Description("Not In")]
        NotIn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("below", "http://hl7.org/fhir/search-modifier-code"), Description("Below")]
        Below,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("above", "http://hl7.org/fhir/search-modifier-code"), Description("Above")]
        Above,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("type", "http://hl7.org/fhir/search-modifier-code"), Description("Type")]
        Type,
    }

    /// <summary>
    /// The free/busy status of the slot.
    /// (url: http://hl7.org/fhir/ValueSet/slotstatus)
    /// </summary>
    [FhirEnumeration("SlotStatus")]
    public enum SlotStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("busy", "http://hl7.org/fhir/slotstatus"), Description("Busy")]
        Busy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("free", "http://hl7.org/fhir/slotstatus"), Description("Free")]
        Free,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("busy-unavailable", "http://hl7.org/fhir/slotstatus"), Description("Busy (Unavailable)")]
        BusyUnavailable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("busy-tentative", "http://hl7.org/fhir/slotstatus"), Description("Busy (Tentative)")]
        BusyTentative,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/slotstatus)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/slotstatus"), Description("Entered in error")]
        EnteredInError,
    }

    /// <summary>
    /// Defines the type of structure that a definition is describing.
    /// (url: http://hl7.org/fhir/ValueSet/structure-definition-kind)
    /// </summary>
    [FhirEnumeration("StructureDefinitionKind")]
    public enum StructureDefinitionKind
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("primitive-type", "http://hl7.org/fhir/structure-definition-kind"), Description("Primitive Data Type")]
        PrimitiveType,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("complex-type", "http://hl7.org/fhir/structure-definition-kind"), Description("Complex Data Type")]
        ComplexType,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("resource", "http://hl7.org/fhir/structure-definition-kind"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/structure-definition-kind)
        /// </summary>
        [EnumLiteral("logical", "http://hl7.org/fhir/structure-definition-kind"), Description("Logical Model")]
        Logical,
    }

    /// <summary>
    /// How an extension context is interpreted.
    /// (url: http://hl7.org/fhir/ValueSet/extension-context)
    /// </summary>
    [FhirEnumeration("ExtensionContext")]
    public enum ExtensionContext
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/extension-context)
        /// </summary>
        [EnumLiteral("resource", "http://hl7.org/fhir/extension-context"), Description("Resource")]
        Resource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/extension-context)
        /// </summary>
        [EnumLiteral("datatype", "http://hl7.org/fhir/extension-context"), Description("Datatype")]
        Datatype,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/supplydelivery-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/supplydelivery-status"), Description("Delivered")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("abandoned", "http://hl7.org/fhir/supplydelivery-status"), Description("Abandoned")]
        Abandoned,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplydelivery-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/supplydelivery-status"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// Status of the supply request
    /// (url: http://hl7.org/fhir/ValueSet/supplyrequest-status)
    /// </summary>
    [FhirEnumeration("SupplyRequestStatus")]
    public enum SupplyRequestStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/supplyrequest-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/supplyrequest-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/supplyrequest-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/supplyrequest-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/supplyrequest-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/supplyrequest-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/supplyrequest-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/supplyrequest-status"), Description("Unknown")]
        Unknown,
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
        [EnumLiteral("ContactDetail", "http://hl7.org/fhir/data-types"), Description("ContactDetail")]
        ContactDetail,
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
        [EnumLiteral("Contributor", "http://hl7.org/fhir/data-types"), Description("Contributor")]
        Contributor,
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
        [EnumLiteral("DataRequirement", "http://hl7.org/fhir/data-types"), Description("DataRequirement")]
        DataRequirement,
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
        [EnumLiteral("Dosage", "http://hl7.org/fhir/data-types"), Description("Dosage")]
        Dosage,
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
        [EnumLiteral("ParameterDefinition", "http://hl7.org/fhir/data-types"), Description("ParameterDefinition")]
        ParameterDefinition,
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
        [EnumLiteral("RelatedArtifact", "http://hl7.org/fhir/data-types"), Description("RelatedArtifact")]
        RelatedArtifact,
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
        [EnumLiteral("TriggerDefinition", "http://hl7.org/fhir/data-types"), Description("TriggerDefinition")]
        TriggerDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("UsageContext", "http://hl7.org/fhir/data-types"), Description("UsageContext")]
        UsageContext,
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
        [EnumLiteral("ActivityDefinition", "http://hl7.org/fhir/resource-types"), Description("ActivityDefinition")]
        ActivityDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AdverseEvent", "http://hl7.org/fhir/resource-types"), Description("AdverseEvent")]
        AdverseEvent,
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
        [EnumLiteral("CapabilityStatement", "http://hl7.org/fhir/resource-types"), Description("CapabilityStatement")]
        CapabilityStatement,
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
        [EnumLiteral("CareTeam", "http://hl7.org/fhir/resource-types"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
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
        [EnumLiteral("CodeSystem", "http://hl7.org/fhir/resource-types"), Description("CodeSystem")]
        CodeSystem,
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
        [EnumLiteral("CompartmentDefinition", "http://hl7.org/fhir/resource-types"), Description("CompartmentDefinition")]
        CompartmentDefinition,
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
        [EnumLiteral("Consent", "http://hl7.org/fhir/resource-types"), Description("Consent")]
        Consent,
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
        [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceRequest")]
        DeviceRequest,
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
        [EnumLiteral("Endpoint", "http://hl7.org/fhir/resource-types"), Description("Endpoint")]
        Endpoint,
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
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile")]
        ExpansionProfile,
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
        [EnumLiteral("GraphDefinition", "http://hl7.org/fhir/resource-types"), Description("GraphDefinition")]
        GraphDefinition,
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
        [EnumLiteral("GuidanceResponse", "http://hl7.org/fhir/resource-types"), Description("GuidanceResponse")]
        GuidanceResponse,
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
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest")]
        ImagingManifest,
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
        [EnumLiteral("Library", "http://hl7.org/fhir/resource-types"), Description("Library")]
        Library,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Linkage", "http://hl7.org/fhir/resource-types"), Description("Linkage")]
        Linkage,
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
        [EnumLiteral("Measure", "http://hl7.org/fhir/resource-types"), Description("Measure")]
        Measure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MeasureReport", "http://hl7.org/fhir/resource-types"), Description("MeasureReport")]
        MeasureReport,
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
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/resource-types"), Description("MedicationRequest")]
        MedicationRequest,
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
        [EnumLiteral("MessageDefinition", "http://hl7.org/fhir/resource-types"), Description("MessageDefinition")]
        MessageDefinition,
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
        [EnumLiteral("PlanDefinition", "http://hl7.org/fhir/resource-types"), Description("PlanDefinition")]
        PlanDefinition,
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
        [EnumLiteral("PractitionerRole", "http://hl7.org/fhir/resource-types"), Description("PractitionerRole")]
        PractitionerRole,
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
        [EnumLiteral("RequestGroup", "http://hl7.org/fhir/resource-types"), Description("RequestGroup")]
        RequestGroup,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchStudy", "http://hl7.org/fhir/resource-types"), Description("ResearchStudy")]
        ResearchStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchSubject", "http://hl7.org/fhir/resource-types"), Description("ResearchSubject")]
        ResearchSubject,
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
        [EnumLiteral("Sequence", "http://hl7.org/fhir/resource-types"), Description("Sequence")]
        Sequence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceDefinition", "http://hl7.org/fhir/resource-types"), Description("ServiceDefinition")]
        ServiceDefinition,
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
        [EnumLiteral("StructureMap", "http://hl7.org/fhir/resource-types"), Description("StructureMap")]
        StructureMap,
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
        [EnumLiteral("Task", "http://hl7.org/fhir/resource-types"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestReport", "http://hl7.org/fhir/resource-types"), Description("TestReport")]
        TestReport,
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
    /// The content or mime type.
    /// (url: http://hl7.org/fhir/ValueSet/content-type)
    /// </summary>
    [FhirEnumeration("ContentType")]
    public enum ContentType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/content-type)
        /// </summary>
        [EnumLiteral("xml", "http://hl7.org/fhir/content-type"), Description("xml")]
        Xml,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/content-type)
        /// </summary>
        [EnumLiteral("json", "http://hl7.org/fhir/content-type"), Description("json")]
        Json,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/content-type)
        /// </summary>
        [EnumLiteral("ttl", "http://hl7.org/fhir/content-type"), Description("ttl")]
        Ttl,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/content-type)
        /// </summary>
        [EnumLiteral("none", "http://hl7.org/fhir/content-type"), Description("none")]
        None,
    }

    /// <summary>
    /// The type of operator to use for assertion.
    /// (url: http://hl7.org/fhir/ValueSet/assert-operator-codes)
    /// </summary>
    [FhirEnumeration("AssertionOperatorType")]
    public enum AssertionOperatorType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("equals", "http://hl7.org/fhir/assert-operator-codes"), Description("equals")]
        Equals,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notEquals", "http://hl7.org/fhir/assert-operator-codes"), Description("notEquals")]
        NotEquals,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/assert-operator-codes"), Description("in")]
        In,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notIn", "http://hl7.org/fhir/assert-operator-codes"), Description("notIn")]
        NotIn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("greaterThan", "http://hl7.org/fhir/assert-operator-codes"), Description("greaterThan")]
        GreaterThan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("lessThan", "http://hl7.org/fhir/assert-operator-codes"), Description("lessThan")]
        LessThan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("empty", "http://hl7.org/fhir/assert-operator-codes"), Description("empty")]
        Empty,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notEmpty", "http://hl7.org/fhir/assert-operator-codes"), Description("notEmpty")]
        NotEmpty,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("contains", "http://hl7.org/fhir/assert-operator-codes"), Description("contains")]
        Contains,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("notContains", "http://hl7.org/fhir/assert-operator-codes"), Description("notContains")]
        NotContains,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/assert-operator-codes)
        /// </summary>
        [EnumLiteral("eval", "http://hl7.org/fhir/assert-operator-codes"), Description("evaluate")]
        Eval,
    }

    /// <summary>
    /// The allowable request method or HTTP operation codes.
    /// (url: http://hl7.org/fhir/ValueSet/http-operations)
    /// </summary>
    [FhirEnumeration("TestScriptRequestMethodCode")]
    public enum TestScriptRequestMethodCode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("delete", "http://hl7.org/fhir/http-operations"), Description("DELETE")]
        Delete,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("get", "http://hl7.org/fhir/http-operations"), Description("GET")]
        Get,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("options", "http://hl7.org/fhir/http-operations"), Description("OPTIONS")]
        Options,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("patch", "http://hl7.org/fhir/http-operations"), Description("PATCH")]
        Patch,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("post", "http://hl7.org/fhir/http-operations"), Description("POST")]
        Post,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("put", "http://hl7.org/fhir/http-operations"), Description("PUT")]
        Put,
    }

    /// <summary>
    /// The use of an address
    /// (url: http://hl7.org/fhir/ValueSet/address-use)
    /// </summary>
    [FhirEnumeration("AddressUse")]
    public enum AddressUse
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("home", "http://hl7.org/fhir/address-use"), Description("Home")]
        Home,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("work", "http://hl7.org/fhir/address-use"), Description("Work")]
        Work,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("temp", "http://hl7.org/fhir/address-use"), Description("Temporary")]
        Temp,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("old", "http://hl7.org/fhir/address-use"), Description("Old / Incorrect")]
        Old,
    }

    /// <summary>
    /// Telecommunications form for contact point
    /// (url: http://hl7.org/fhir/ValueSet/contact-point-system)
    /// </summary>
    [FhirEnumeration("ContactPointSystem")]
    public enum ContactPointSystem
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("phone", "http://hl7.org/fhir/contact-point-system"), Description("Phone")]
        Phone,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("fax", "http://hl7.org/fhir/contact-point-system"), Description("Fax")]
        Fax,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("email", "http://hl7.org/fhir/contact-point-system"), Description("Email")]
        Email,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("pager", "http://hl7.org/fhir/contact-point-system"), Description("Pager")]
        Pager,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("url", "http://hl7.org/fhir/contact-point-system"), Description("URL")]
        Url,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("sms", "http://hl7.org/fhir/contact-point-system"), Description("SMS")]
        Sms,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contact-point-system)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/contact-point-system"), Description("Other")]
        Other,
    }

    /// <summary>
    /// How a property is represented when serialized.
    /// (url: http://hl7.org/fhir/ValueSet/property-representation)
    /// </summary>
    [FhirEnumeration("PropertyRepresentation")]
    public enum PropertyRepresentation
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/property-representation)
        /// </summary>
        [EnumLiteral("xmlAttr", "http://hl7.org/fhir/property-representation"), Description("XML Attribute")]
        XmlAttr,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/property-representation)
        /// </summary>
        [EnumLiteral("xmlText", "http://hl7.org/fhir/property-representation"), Description("XML Text")]
        XmlText,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/property-representation)
        /// </summary>
        [EnumLiteral("typeAttr", "http://hl7.org/fhir/property-representation"), Description("Type Attribute")]
        TypeAttr,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/property-representation)
        /// </summary>
        [EnumLiteral("cdaText", "http://hl7.org/fhir/property-representation"), Description("CDA Text Format")]
        CdaText,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/property-representation)
        /// </summary>
        [EnumLiteral("xhtml", "http://hl7.org/fhir/property-representation"), Description("XHTML")]
        Xhtml,
    }

    /// <summary>
    /// Identifies the purpose for this identifier, if known .
    /// (url: http://hl7.org/fhir/ValueSet/identifier-use)
    /// </summary>
    [FhirEnumeration("IdentifierUse")]
    public enum IdentifierUse
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("usual", "http://hl7.org/fhir/identifier-use"), Description("Usual")]
        Usual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("official", "http://hl7.org/fhir/identifier-use"), Description("Official")]
        Official,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("temp", "http://hl7.org/fhir/identifier-use"), Description("Temp")]
        Temp,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("secondary", "http://hl7.org/fhir/identifier-use"), Description("Secondary")]
        Secondary,
    }

    /// <summary>
    /// Real world event relating to the schedule.
    /// (url: http://hl7.org/fhir/ValueSet/event-timing)
    /// </summary>
    [FhirEnumeration("EventTiming")]
    public enum EventTiming
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("MORN", "http://hl7.org/fhir/event-timing"), Description("Morning")]
        MORN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("AFT", "http://hl7.org/fhir/event-timing"), Description("Afternoon")]
        AFT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("EVE", "http://hl7.org/fhir/event-timing"), Description("Evening")]
        EVE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("NIGHT", "http://hl7.org/fhir/event-timing"), Description("Night")]
        NIGHT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("PHS", "http://hl7.org/fhir/event-timing"), Description("After Sleep")]
        PHS,
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
    /// The type of trigger
    /// (url: http://hl7.org/fhir/ValueSet/trigger-type)
    /// </summary>
    [FhirEnumeration("TriggerType")]
    public enum TriggerType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("named-event", "http://hl7.org/fhir/trigger-type"), Description("Named Event")]
        NamedEvent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("periodic", "http://hl7.org/fhir/trigger-type"), Description("Periodic")]
        Periodic,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("data-added", "http://hl7.org/fhir/trigger-type"), Description("Data Added")]
        DataAdded,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("data-modified", "http://hl7.org/fhir/trigger-type"), Description("Data Modified")]
        DataModified,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("data-removed", "http://hl7.org/fhir/trigger-type"), Description("Data Removed")]
        DataRemoved,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("data-accessed", "http://hl7.org/fhir/trigger-type"), Description("Data Accessed")]
        DataAccessed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/trigger-type)
        /// </summary>
        [EnumLiteral("data-access-ended", "http://hl7.org/fhir/trigger-type"), Description("Data Access Ended")]
        DataAccessEnded,
    }

}
