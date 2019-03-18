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
    /// The lifecycle status of a Value Set or Concept Map.
    /// (url: http://hl7.org/fhir/ValueSet/publication-status)
    /// </summary>
    [FhirEnumeration("PublicationStatus")]
    public enum PublicationStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/publication-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/publication-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("retired", "http://hl7.org/fhir/publication-status"), Description("Retired")]
        Retired,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/publication-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/publication-status"), Description("Unknown")]
        Unknown,
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
    /// How a capability statement is intended to be used.
    /// (url: http://hl7.org/fhir/ValueSet/capability-statement-kind)
    /// </summary>
    [FhirEnumeration("CapabilityStatementKind")]
    public enum CapabilityStatementKind
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/capability-statement-kind)
        /// </summary>
        [EnumLiteral("instance", "http://hl7.org/fhir/capability-statement-kind"), Description("Instance")]
        Instance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/capability-statement-kind)
        /// </summary>
        [EnumLiteral("capability", "http://hl7.org/fhir/capability-statement-kind"), Description("Capability")]
        Capability,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/capability-statement-kind)
        /// </summary>
        [EnumLiteral("requirements", "http://hl7.org/fhir/capability-statement-kind"), Description("Requirements")]
        Requirements,
    }

    /// <summary>
    /// The mode of a RESTful capability statement.
    /// (url: http://hl7.org/fhir/ValueSet/restful-capability-mode)
    /// </summary>
    [FhirEnumeration("RestfulCapabilityMode")]
    public enum RestfulCapabilityMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-capability-mode)
        /// </summary>
        [EnumLiteral("client", "http://hl7.org/fhir/restful-capability-mode"), Description("Client")]
        Client,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/restful-capability-mode)
        /// </summary>
        [EnumLiteral("server", "http://hl7.org/fhir/restful-capability-mode"), Description("Server")]
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
    /// A code that indicates how the server supports conditional read.
    /// (url: http://hl7.org/fhir/ValueSet/conditional-read-status)
    /// </summary>
    [FhirEnumeration("ConditionalReadStatus")]
    public enum ConditionalReadStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conditional-read-status)
        /// </summary>
        [EnumLiteral("not-supported", "http://hl7.org/fhir/conditional-read-status"), Description("Not Supported")]
        NotSupported,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conditional-read-status)
        /// </summary>
        [EnumLiteral("modified-since", "http://hl7.org/fhir/conditional-read-status"), Description("If-Modified-Since")]
        ModifiedSince,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conditional-read-status)
        /// </summary>
        [EnumLiteral("not-match", "http://hl7.org/fhir/conditional-read-status"), Description("If-None-Match")]
        NotMatch,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conditional-read-status)
        /// </summary>
        [EnumLiteral("full-support", "http://hl7.org/fhir/conditional-read-status"), Description("Full Support")]
        FullSupport,
    }

    /// <summary>
    /// A set of flags that defines how references are supported.
    /// (url: http://hl7.org/fhir/ValueSet/reference-handling-policy)
    /// </summary>
    [FhirEnumeration("ReferenceHandlingPolicy")]
    public enum ReferenceHandlingPolicy
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-handling-policy)
        /// </summary>
        [EnumLiteral("literal", "http://hl7.org/fhir/reference-handling-policy"), Description("Literal References")]
        Literal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-handling-policy)
        /// </summary>
        [EnumLiteral("logical", "http://hl7.org/fhir/reference-handling-policy"), Description("Logical References")]
        Logical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-handling-policy)
        /// </summary>
        [EnumLiteral("resolves", "http://hl7.org/fhir/reference-handling-policy"), Description("Resolves References")]
        Resolves,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-handling-policy)
        /// </summary>
        [EnumLiteral("enforced", "http://hl7.org/fhir/reference-handling-policy"), Description("Reference Integrity Enforced")]
        Enforced,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-handling-policy)
        /// </summary>
        [EnumLiteral("local", "http://hl7.org/fhir/reference-handling-policy"), Description("Local References Only")]
        Local,
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
    /// The mode of a message capability statement.
    /// (url: http://hl7.org/fhir/ValueSet/event-capability-mode)
    /// </summary>
    [FhirEnumeration("EventCapabilityMode")]
    public enum EventCapabilityMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-capability-mode)
        /// </summary>
        [EnumLiteral("sender", "http://hl7.org/fhir/event-capability-mode"), Description("Sender")]
        Sender,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-capability-mode)
        /// </summary>
        [EnumLiteral("receiver", "http://hl7.org/fhir/event-capability-mode"), Description("Receiver")]
        Receiver,
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
    /// Codes indicating the degree of authority/intentionality associated with a care plan
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-intent)
    /// </summary>
    [FhirEnumeration("CarePlanIntent")]
    public enum CarePlanIntent
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-intent)
        /// </summary>
        [EnumLiteral("proposal", "http://hl7.org/fhir/care-plan-intent"), Description("Proposal")]
        Proposal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-intent)
        /// </summary>
        [EnumLiteral("plan", "http://hl7.org/fhir/care-plan-intent"), Description("Plan")]
        Plan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-intent)
        /// </summary>
        [EnumLiteral("order", "http://hl7.org/fhir/care-plan-intent"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-intent)
        /// </summary>
        [EnumLiteral("option", "http://hl7.org/fhir/care-plan-intent"), Description("Option")]
        Option,
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
    /// Indicates the status of the care team.
    /// (url: http://hl7.org/fhir/ValueSet/care-team-status)
    /// </summary>
    [FhirEnumeration("CareTeamStatus")]
    public enum CareTeamStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-team-status)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/care-team-status"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-team-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/care-team-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-team-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/care-team-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-team-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/care-team-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-team-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/care-team-status"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// Codes identifying the stage lifecycle stage of a ChargeItem
    /// (url: http://hl7.org/fhir/ValueSet/chargeitem-status)
    /// </summary>
    [FhirEnumeration("ChargeItemStatus")]
    public enum ChargeItemStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/chargeitem-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("billable", "http://hl7.org/fhir/chargeitem-status"), Description("Billable")]
        Billable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("not-billable", "http://hl7.org/fhir/chargeitem-status"), Description("Not billable")]
        NotBillable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("aborted", "http://hl7.org/fhir/chargeitem-status"), Description("Aborted")]
        Aborted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("billed", "http://hl7.org/fhir/chargeitem-status"), Description("Billed")]
        Billed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/chargeitem-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/chargeitem-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/chargeitem-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// This value set includes Status codes.
    /// (url: http://hl7.org/fhir/ValueSet/fm-status)
    /// </summary>
    [FhirEnumeration("FinancialResourceStatusCodes")]
    public enum FinancialResourceStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/fm-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/fm-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/fm-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/fm-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/fm-status"), Description("Entered in Error")]
        EnteredInError,
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
    /// The meaning of the hierarchy of concepts in a code system
    /// (url: http://hl7.org/fhir/ValueSet/codesystem-hierarchy-meaning)
    /// </summary>
    [FhirEnumeration("CodeSystemHierarchyMeaning")]
    public enum CodeSystemHierarchyMeaning
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
        /// </summary>
        [EnumLiteral("grouped-by", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Grouped By")]
        GroupedBy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
        /// </summary>
        [EnumLiteral("is-a", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Is-A")]
        IsA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
        /// </summary>
        [EnumLiteral("part-of", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Part Of")]
        PartOf,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
        /// </summary>
        [EnumLiteral("classified-with", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Classified With")]
        ClassifiedWith,
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
    /// The clinical priority of a diagnostic order.
    /// (url: http://hl7.org/fhir/ValueSet/request-priority)
    /// </summary>
    [FhirEnumeration("RequestPriority")]
    public enum RequestPriority
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("routine", "http://hl7.org/fhir/request-priority"), Description("Routine")]
        Routine,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("urgent", "http://hl7.org/fhir/request-priority"), Description("Urgent")]
        Urgent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-priority)
        /// </summary>
        [EnumLiteral("asap", "http://hl7.org/fhir/request-priority"), Description("ASAP")]
        Asap,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Patient", "http://hl7.org/fhir/compartment-type"), Description("Patient")]
        Patient,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/compartment-type"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/compartment-type"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/compartment-type"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/compartment-type)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/compartment-type"), Description("Device")]
        Device,
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
    /// Defines which action to take if there is no match in the group.
    /// (url: http://hl7.org/fhir/ValueSet/conceptmap-unmapped-mode)
    /// </summary>
    [FhirEnumeration("ConceptMapGroupUnmappedMode")]
    public enum ConceptMapGroupUnmappedMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conceptmap-unmapped-mode)
        /// </summary>
        [EnumLiteral("provided", "http://hl7.org/fhir/conceptmap-unmapped-mode"), Description("Provided Code")]
        Provided,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conceptmap-unmapped-mode)
        /// </summary>
        [EnumLiteral("fixed", "http://hl7.org/fhir/conceptmap-unmapped-mode"), Description("Fixed Code")]
        Fixed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/conceptmap-unmapped-mode)
        /// </summary>
        [EnumLiteral("other-map", "http://hl7.org/fhir/conceptmap-unmapped-mode"), Description("Other Map")]
        OtherMap,
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
    /// Indicates the state of the consent
    /// (url: http://hl7.org/fhir/ValueSet/consent-state-codes)
    /// </summary>
    [FhirEnumeration("ConsentState")]
    public enum ConsentState
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-state-codes)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/consent-state-codes"), Description("Pending")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-state-codes)
        /// </summary>
        [EnumLiteral("proposed", "http://hl7.org/fhir/consent-state-codes"), Description("Proposed")]
        Proposed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-state-codes)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/consent-state-codes"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-state-codes)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/consent-state-codes"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-state-codes)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/consent-state-codes"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-state-codes)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/consent-state-codes"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// How a resource reference is interpreted when testing consent restrictions
    /// (url: http://hl7.org/fhir/ValueSet/consent-data-meaning)
    /// </summary>
    [FhirEnumeration("ConsentDataMeaning")]
    public enum ConsentDataMeaning
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-data-meaning)
        /// </summary>
        [EnumLiteral("instance", "http://hl7.org/fhir/consent-data-meaning"), Description("Instance")]
        Instance,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-data-meaning)
        /// </summary>
        [EnumLiteral("related", "http://hl7.org/fhir/consent-data-meaning"), Description("Related")]
        Related,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-data-meaning)
        /// </summary>
        [EnumLiteral("dependents", "http://hl7.org/fhir/consent-data-meaning"), Description("Dependents")]
        Dependents,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-data-meaning)
        /// </summary>
        [EnumLiteral("authoredby", "http://hl7.org/fhir/consent-data-meaning"), Description("AuthoredBy")]
        Authoredby,
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
    /// This value set contract specific codes for status.
    /// (url: http://hl7.org/fhir/ValueSet/contract-status)
    /// </summary>
    [FhirEnumeration("ContractResourceStatusCodes")]
    public enum ContractResourceStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/contract-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("appended", "http://hl7.org/fhir/contract-status"), Description("Appended")]
        Appended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/contract-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("disputed", "http://hl7.org/fhir/contract-status"), Description("Disputed")]
        Disputed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/contract-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("executable", "http://hl7.org/fhir/contract-status"), Description("Executable")]
        Executable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("executed", "http://hl7.org/fhir/contract-status"), Description("Executed")]
        Executed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("negotiable", "http://hl7.org/fhir/contract-status"), Description("Negotiable")]
        Negotiable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("offered", "http://hl7.org/fhir/contract-status"), Description("Offered")]
        Offered,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("policy", "http://hl7.org/fhir/contract-status"), Description("Policy")]
        Policy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/contract-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("renewed", "http://hl7.org/fhir/contract-status"), Description("Renewed")]
        Renewed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("revoked", "http://hl7.org/fhir/contract-status"), Description("Revoked")]
        Revoked,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("resolved", "http://hl7.org/fhir/contract-status"), Description("Resolved")]
        Resolved,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-status)
        /// </summary>
        [EnumLiteral("terminated", "http://hl7.org/fhir/contract-status"), Description("Terminated")]
        Terminated,
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
    /// Codes to identify how UDI data was entered
    /// (url: http://hl7.org/fhir/ValueSet/udi-entry-type)
    /// </summary>
    [FhirEnumeration("UDIEntryType")]
    public enum UDIEntryType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("barcode", "http://hl7.org/fhir/udi-entry-type"), Description("BarCode")]
        Barcode,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("rfid", "http://hl7.org/fhir/udi-entry-type"), Description("RFID")]
        Rfid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("manual", "http://hl7.org/fhir/udi-entry-type"), Description("Manual")]
        Manual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("card", "http://hl7.org/fhir/udi-entry-type"), Description("Card")]
        Card,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("self-reported", "http://hl7.org/fhir/udi-entry-type"), Description("Self Reported")]
        SelfReported,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/udi-entry-type"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The availability status of the device.
    /// (url: http://hl7.org/fhir/ValueSet/device-status)
    /// </summary>
    [FhirEnumeration("FHIRDeviceStatus")]
    public enum FHIRDeviceStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/device-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/device-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/device-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/device-status"), Description("Unknown")]
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
    /// A coded concept indicating the current status of a the Device Usage
    /// (url: http://hl7.org/fhir/ValueSet/device-statement-status)
    /// </summary>
    [FhirEnumeration("DeviceUseStatementStatus")]
    public enum DeviceUseStatementStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-statement-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/device-statement-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-statement-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/device-statement-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-statement-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/device-statement-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-statement-status)
        /// </summary>
        [EnumLiteral("intended", "http://hl7.org/fhir/device-statement-status"), Description("Intended")]
        Intended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-statement-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/device-statement-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-statement-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/device-statement-status"), Description("On Hold")]
        OnHold,
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
    /// Current state of the encounter
    /// (url: http://hl7.org/fhir/ValueSet/encounter-status)
    /// </summary>
    [FhirEnumeration("EncounterStatus")]
    public enum EncounterStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/encounter-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("arrived", "http://hl7.org/fhir/encounter-status"), Description("Arrived")]
        Arrived,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("triaged", "http://hl7.org/fhir/encounter-status"), Description("Triaged")]
        Triaged,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/encounter-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("onleave", "http://hl7.org/fhir/encounter-status"), Description("On Leave")]
        Onleave,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("finished", "http://hl7.org/fhir/encounter-status"), Description("Finished")]
        Finished,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/encounter-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/encounter-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/encounter-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/encounter-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// The status of the endpoint
    /// (url: http://hl7.org/fhir/ValueSet/endpoint-status)
    /// </summary>
    [FhirEnumeration("EndpointStatus")]
    public enum EndpointStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/endpoint-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/endpoint-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/endpoint-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/endpoint-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/endpoint-status)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/endpoint-status"), Description("Error")]
        Error,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/endpoint-status)
        /// </summary>
        [EnumLiteral("off", "http://hl7.org/fhir/endpoint-status"), Description("Off")]
        Off,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/endpoint-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/endpoint-status"), Description("Entered in error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/endpoint-status)
        /// </summary>
        [EnumLiteral("test", "http://hl7.org/fhir/endpoint-status"), Description("Test")]
        Test,
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
    /// A code specifying the state of the resource instance.
    /// (url: http://hl7.org/fhir/ValueSet/explanationofbenefit-status)
    /// </summary>
    [FhirEnumeration("ExplanationOfBenefitStatus")]
    public enum ExplanationOfBenefitStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/explanationofbenefit-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/explanationofbenefit-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/explanationofbenefit-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/explanationofbenefit-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/explanationofbenefit-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/explanationofbenefit-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/explanationofbenefit-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/explanationofbenefit-status"), Description("Entered In Error")]
        EnteredInError,
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
    /// How a compartment must be linked
    /// (url: http://hl7.org/fhir/ValueSet/graph-compartment-rule)
    /// </summary>
    [FhirEnumeration("GraphCompartmentRule")]
    public enum GraphCompartmentRule
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/graph-compartment-rule)
        /// </summary>
        [EnumLiteral("identical", "http://hl7.org/fhir/graph-compartment-rule"), Description("Identical")]
        Identical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/graph-compartment-rule)
        /// </summary>
        [EnumLiteral("matching", "http://hl7.org/fhir/graph-compartment-rule"), Description("Matching")]
        Matching,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/graph-compartment-rule)
        /// </summary>
        [EnumLiteral("different", "http://hl7.org/fhir/graph-compartment-rule"), Description("Different")]
        Different,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/graph-compartment-rule)
        /// </summary>
        [EnumLiteral("custom", "http://hl7.org/fhir/graph-compartment-rule"), Description("Custom")]
        Custom,
    }

    /// <summary>
    /// The status of a guidance response
    /// (url: http://hl7.org/fhir/ValueSet/guidance-response-status)
    /// </summary>
    [FhirEnumeration("GuidanceResponseStatus")]
    public enum GuidanceResponseStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guidance-response-status)
        /// </summary>
        [EnumLiteral("success", "http://hl7.org/fhir/guidance-response-status"), Description("Success")]
        Success,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guidance-response-status)
        /// </summary>
        [EnumLiteral("data-requested", "http://hl7.org/fhir/guidance-response-status"), Description("Data Requested")]
        DataRequested,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guidance-response-status)
        /// </summary>
        [EnumLiteral("data-required", "http://hl7.org/fhir/guidance-response-status"), Description("Data Required")]
        DataRequired,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guidance-response-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/guidance-response-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guidance-response-status)
        /// </summary>
        [EnumLiteral("failure", "http://hl7.org/fhir/guidance-response-status"), Description("Failure")]
        Failure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guidance-response-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/guidance-response-status"), Description("Entered In Error")]
        EnteredInError,
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
    /// Used to distinguish different roles a resource can play within a set of linked resources
    /// (url: http://hl7.org/fhir/ValueSet/linkage-type)
    /// </summary>
    [FhirEnumeration("LinkageType")]
    public enum LinkageType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/linkage-type)
        /// </summary>
        [EnumLiteral("source", "http://hl7.org/fhir/linkage-type"), Description("Source of truth")]
        Source,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/linkage-type)
        /// </summary>
        [EnumLiteral("alternate", "http://hl7.org/fhir/linkage-type"), Description("Alternate record")]
        Alternate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/linkage-type)
        /// </summary>
        [EnumLiteral("historical", "http://hl7.org/fhir/linkage-type"), Description("Historical/obsolete record")]
        Historical,
    }

    /// <summary>
    /// The status of the measure report
    /// (url: http://hl7.org/fhir/ValueSet/measure-report-status)
    /// </summary>
    [FhirEnumeration("MeasureReportStatus")]
    public enum MeasureReportStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-status)
        /// </summary>
        [EnumLiteral("complete", "http://hl7.org/fhir/measure-report-status"), Description("Complete")]
        Complete,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-status)
        /// </summary>
        [EnumLiteral("pending", "http://hl7.org/fhir/measure-report-status"), Description("Pending")]
        Pending,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-status)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/measure-report-status"), Description("Error")]
        Error,
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
    /// Defines the kinds of conditions that can appear on actions
    /// (url: http://hl7.org/fhir/ValueSet/action-condition-kind)
    /// </summary>
    [FhirEnumeration("ActionConditionKind")]
    public enum ActionConditionKind
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("applicability", "http://hl7.org/fhir/action-condition-kind"), Description("Applicability")]
        Applicability,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-condition-kind)
        /// </summary>
        [EnumLiteral("start", "http://hl7.org/fhir/action-condition-kind"), Description("Start")]
        Start,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before-start", "http://hl7.org/fhir/action-relationship-type"), Description("Before Start")]
        BeforeStart,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before", "http://hl7.org/fhir/action-relationship-type"), Description("Before")]
        Before,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("before-end", "http://hl7.org/fhir/action-relationship-type"), Description("Before End")]
        BeforeEnd,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent-with-start", "http://hl7.org/fhir/action-relationship-type"), Description("Concurrent With Start")]
        ConcurrentWithStart,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent", "http://hl7.org/fhir/action-relationship-type"), Description("Concurrent")]
        Concurrent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("concurrent-with-end", "http://hl7.org/fhir/action-relationship-type"), Description("Concurrent With End")]
        ConcurrentWithEnd,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after-start", "http://hl7.org/fhir/action-relationship-type"), Description("After Start")]
        AfterStart,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-relationship-type)
        /// </summary>
        [EnumLiteral("after", "http://hl7.org/fhir/action-relationship-type"), Description("After")]
        After,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("visual-group", "http://hl7.org/fhir/action-grouping-behavior"), Description("Visual Group")]
        VisualGroup,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-grouping-behavior)
        /// </summary>
        [EnumLiteral("logical-group", "http://hl7.org/fhir/action-grouping-behavior"), Description("Logical Group")]
        LogicalGroup,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("any", "http://hl7.org/fhir/action-selection-behavior"), Description("Any")]
        Any,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("all", "http://hl7.org/fhir/action-selection-behavior"), Description("All")]
        All,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("all-or-none", "http://hl7.org/fhir/action-selection-behavior"), Description("All Or None")]
        AllOrNone,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("exactly-one", "http://hl7.org/fhir/action-selection-behavior"), Description("Exactly One")]
        ExactlyOne,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-selection-behavior)
        /// </summary>
        [EnumLiteral("at-most-one", "http://hl7.org/fhir/action-selection-behavior"), Description("At Most One")]
        AtMostOne,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("must", "http://hl7.org/fhir/action-required-behavior"), Description("Must")]
        Must,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-required-behavior)
        /// </summary>
        [EnumLiteral("could", "http://hl7.org/fhir/action-required-behavior"), Description("Could")]
        Could,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-precheck-behavior)
        /// </summary>
        [EnumLiteral("yes", "http://hl7.org/fhir/action-precheck-behavior"), Description("Yes")]
        Yes,
        /// <summary>
        /// MISSING DESCRIPTION
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
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-cardinality-behavior)
        /// </summary>
        [EnumLiteral("single", "http://hl7.org/fhir/action-cardinality-behavior"), Description("Single")]
        Single,
        /// <summary>
        /// MISSING DESCRIPTION
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
    /// Distinguishes groups from questions and display text and indicates data type for questions
    /// (url: http://hl7.org/fhir/ValueSet/item-type)
    /// </summary>
    [FhirEnumeration("QuestionnaireItemType")]
    public enum QuestionnaireItemType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("group", "http://hl7.org/fhir/item-type"), Description("Group")]
        Group,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("display", "http://hl7.org/fhir/item-type"), Description("Display")]
        Display,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/item-type"), Description("Boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("decimal", "http://hl7.org/fhir/item-type"), Description("Decimal")]
        Decimal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/item-type"), Description("Integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/item-type"), Description("Date")]
        Date,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/item-type"), Description("Date Time")]
        DateTime,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("time", "http://hl7.org/fhir/item-type"), Description("Time")]
        Time,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/item-type"), Description("String")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("text", "http://hl7.org/fhir/item-type"), Description("Text")]
        Text,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("url", "http://hl7.org/fhir/item-type"), Description("Url")]
        Url,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("choice", "http://hl7.org/fhir/item-type"), Description("Choice")]
        Choice,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("open-choice", "http://hl7.org/fhir/item-type"), Description("Open Choice")]
        OpenChoice,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("attachment", "http://hl7.org/fhir/item-type"), Description("Attachment")]
        Attachment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/item-type"), Description("Reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [EnumLiteral("quantity", "http://hl7.org/fhir/item-type"), Description("Quantity")]
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
    /// What Search Comparator Codes are supported in search
    /// (url: http://hl7.org/fhir/ValueSet/search-comparator)
    /// </summary>
    [FhirEnumeration("SearchComparator")]
    public enum SearchComparator
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("eq", "http://hl7.org/fhir/search-comparator"), Description("Equals")]
        Eq,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("ne", "http://hl7.org/fhir/search-comparator"), Description("Not Equals")]
        Ne,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("gt", "http://hl7.org/fhir/search-comparator"), Description("Greater Than")]
        Gt,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("lt", "http://hl7.org/fhir/search-comparator"), Description("Less Then")]
        Lt,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("ge", "http://hl7.org/fhir/search-comparator"), Description("Greater or Equals")]
        Ge,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("le", "http://hl7.org/fhir/search-comparator"), Description("Less of Equal")]
        Le,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("sa", "http://hl7.org/fhir/search-comparator"), Description("Starts After")]
        Sa,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("eb", "http://hl7.org/fhir/search-comparator"), Description("Ends Before")]
        Eb,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-comparator)
        /// </summary>
        [EnumLiteral("ap", "http://hl7.org/fhir/search-comparator"), Description("Approximately")]
        Ap,
    }

    /// <summary>
    /// Type for quality report
    /// (url: http://hl7.org/fhir/ValueSet/quality-type)
    /// </summary>
    [FhirEnumeration("qualityType")]
    public enum qualityType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/quality-type)
        /// </summary>
        [EnumLiteral("indel", "http://hl7.org/fhir/quality-type"), Description("INDEL Comparison")]
        Indel,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/quality-type)
        /// </summary>
        [EnumLiteral("snp", "http://hl7.org/fhir/quality-type"), Description("SNP Comparison")]
        Snp,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/quality-type)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/quality-type"), Description("UNKNOWN Comparison")]
        Unknown,
    }

    /// <summary>
    /// Type for access of external URI
    /// (url: http://hl7.org/fhir/ValueSet/repository-type)
    /// </summary>
    [FhirEnumeration("repositoryType")]
    public enum repositoryType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/repository-type)
        /// </summary>
        [EnumLiteral("directlink", "http://hl7.org/fhir/repository-type"), Description("Click and see")]
        Directlink,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/repository-type)
        /// </summary>
        [EnumLiteral("openapi", "http://hl7.org/fhir/repository-type"), Description("The URL is the RESTful or other kind of API that can access to the result.")]
        Openapi,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/repository-type)
        /// </summary>
        [EnumLiteral("login", "http://hl7.org/fhir/repository-type"), Description("Result cannot be access unless an account is logged in")]
        Login,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/repository-type)
        /// </summary>
        [EnumLiteral("oauth", "http://hl7.org/fhir/repository-type"), Description("Result need to be fetched with API and need LOGIN( or cookies are required when visiting the link of resource)")]
        Oauth,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/repository-type)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/repository-type"), Description("Some other complicated or particular way to get resource from URL.")]
        Other,
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
    /// How a type relates to its baseDefinition.
    /// (url: http://hl7.org/fhir/ValueSet/type-derivation-rule)
    /// </summary>
    [FhirEnumeration("TypeDerivationRule")]
    public enum TypeDerivationRule
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/type-derivation-rule)
        /// </summary>
        [EnumLiteral("specialization", "http://hl7.org/fhir/type-derivation-rule"), Description("Specialization")]
        Specialization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/type-derivation-rule)
        /// </summary>
        [EnumLiteral("constraint", "http://hl7.org/fhir/type-derivation-rule"), Description("Constraint")]
        Constraint,
    }

    /// <summary>
    /// How the referenced structure is used in this mapping
    /// (url: http://hl7.org/fhir/ValueSet/map-model-mode)
    /// </summary>
    [FhirEnumeration("StructureMapModelMode")]
    public enum StructureMapModelMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-model-mode)
        /// </summary>
        [EnumLiteral("source", "http://hl7.org/fhir/map-model-mode"), Description("Source Structure Definition")]
        Source,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-model-mode)
        /// </summary>
        [EnumLiteral("queried", "http://hl7.org/fhir/map-model-mode"), Description("Queried Structure Definition")]
        Queried,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-model-mode)
        /// </summary>
        [EnumLiteral("target", "http://hl7.org/fhir/map-model-mode"), Description("Target Structure Definition")]
        Target,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-model-mode)
        /// </summary>
        [EnumLiteral("produced", "http://hl7.org/fhir/map-model-mode"), Description("Produced Structure Definition")]
        Produced,
    }

    /// <summary>
    /// If this is the default rule set to apply for the source type, or this combination of types
    /// (url: http://hl7.org/fhir/ValueSet/map-group-type-mode)
    /// </summary>
    [FhirEnumeration("StructureMapGroupTypeMode")]
    public enum StructureMapGroupTypeMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-group-type-mode)
        /// </summary>
        [EnumLiteral("none", "http://hl7.org/fhir/map-group-type-mode"), Description("Not a Default")]
        None,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-group-type-mode)
        /// </summary>
        [EnumLiteral("types", "http://hl7.org/fhir/map-group-type-mode"), Description("Default for Type Combination")]
        Types,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-group-type-mode)
        /// </summary>
        [EnumLiteral("type-and-types", "http://hl7.org/fhir/map-group-type-mode"), Description("Default for type + combination")]
        TypeAndTypes,
    }

    /// <summary>
    /// Mode for this instance of data
    /// (url: http://hl7.org/fhir/ValueSet/map-input-mode)
    /// </summary>
    [FhirEnumeration("StructureMapInputMode")]
    public enum StructureMapInputMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-input-mode)
        /// </summary>
        [EnumLiteral("source", "http://hl7.org/fhir/map-input-mode"), Description("Source Instance")]
        Source,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-input-mode)
        /// </summary>
        [EnumLiteral("target", "http://hl7.org/fhir/map-input-mode"), Description("Target Instance")]
        Target,
    }

    /// <summary>
    /// If field is a list, how to manage the source
    /// (url: http://hl7.org/fhir/ValueSet/map-source-list-mode)
    /// </summary>
    [FhirEnumeration("StructureMapSourceListMode")]
    public enum StructureMapSourceListMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-source-list-mode)
        /// </summary>
        [EnumLiteral("first", "http://hl7.org/fhir/map-source-list-mode"), Description("First")]
        First,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-source-list-mode)
        /// </summary>
        [EnumLiteral("not_first", "http://hl7.org/fhir/map-source-list-mode"), Description("All but the first")]
        Not_first,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-source-list-mode)
        /// </summary>
        [EnumLiteral("last", "http://hl7.org/fhir/map-source-list-mode"), Description("Last")]
        Last,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-source-list-mode)
        /// </summary>
        [EnumLiteral("not_last", "http://hl7.org/fhir/map-source-list-mode"), Description("All but the last")]
        Not_last,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-source-list-mode)
        /// </summary>
        [EnumLiteral("only_one", "http://hl7.org/fhir/map-source-list-mode"), Description("Enforce only one")]
        Only_one,
    }

    /// <summary>
    /// How to interpret the context
    /// (url: http://hl7.org/fhir/ValueSet/map-context-type)
    /// </summary>
    [FhirEnumeration("StructureMapContextType")]
    public enum StructureMapContextType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-context-type)
        /// </summary>
        [EnumLiteral("type", "http://hl7.org/fhir/map-context-type"), Description("Type")]
        Type,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-context-type)
        /// </summary>
        [EnumLiteral("variable", "http://hl7.org/fhir/map-context-type"), Description("Variable")]
        Variable,
    }

    /// <summary>
    /// If field is a list, how to manage the production
    /// (url: http://hl7.org/fhir/ValueSet/map-target-list-mode)
    /// </summary>
    [FhirEnumeration("StructureMapTargetListMode")]
    public enum StructureMapTargetListMode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-target-list-mode)
        /// </summary>
        [EnumLiteral("first", "http://hl7.org/fhir/map-target-list-mode"), Description("First")]
        First,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-target-list-mode)
        /// </summary>
        [EnumLiteral("share", "http://hl7.org/fhir/map-target-list-mode"), Description("Share")]
        Share,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-target-list-mode)
        /// </summary>
        [EnumLiteral("last", "http://hl7.org/fhir/map-target-list-mode"), Description("Last")]
        Last,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-target-list-mode)
        /// </summary>
        [EnumLiteral("collate", "http://hl7.org/fhir/map-target-list-mode"), Description("Collate")]
        Collate,
    }

    /// <summary>
    /// How data is copied/created
    /// (url: http://hl7.org/fhir/ValueSet/map-transform)
    /// </summary>
    [FhirEnumeration("StructureMapTransform")]
    public enum StructureMapTransform
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("create", "http://hl7.org/fhir/map-transform"), Description("create")]
        Create,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("copy", "http://hl7.org/fhir/map-transform"), Description("copy")]
        Copy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("truncate", "http://hl7.org/fhir/map-transform"), Description("truncate")]
        Truncate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("escape", "http://hl7.org/fhir/map-transform"), Description("escape")]
        Escape,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("cast", "http://hl7.org/fhir/map-transform"), Description("cast")]
        Cast,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("append", "http://hl7.org/fhir/map-transform"), Description("append")]
        Append,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("translate", "http://hl7.org/fhir/map-transform"), Description("translate")]
        Translate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/map-transform"), Description("reference")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("dateOp", "http://hl7.org/fhir/map-transform"), Description("dateOp")]
        DateOp,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("uuid", "http://hl7.org/fhir/map-transform"), Description("uuid")]
        Uuid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("pointer", "http://hl7.org/fhir/map-transform"), Description("pointer")]
        Pointer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("evaluate", "http://hl7.org/fhir/map-transform"), Description("evaluate")]
        Evaluate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("cc", "http://hl7.org/fhir/map-transform"), Description("cc")]
        Cc,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("c", "http://hl7.org/fhir/map-transform"), Description("c")]
        C,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("qty", "http://hl7.org/fhir/map-transform"), Description("qty")]
        Qty,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("id", "http://hl7.org/fhir/map-transform"), Description("id")]
        Id,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/map-transform)
        /// </summary>
        [EnumLiteral("cp", "http://hl7.org/fhir/map-transform"), Description("cp")]
        Cp,
    }

    /// <summary>
    /// A code to indicate if the substance is actively used
    /// (url: http://hl7.org/fhir/ValueSet/substance-status)
    /// </summary>
    [FhirEnumeration("FHIRSubstanceStatus")]
    public enum FHIRSubstanceStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/substance-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/substance-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/substance-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/substance-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/substance-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/substance-status"), Description("Entered in Error")]
        EnteredInError,
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
    /// The current status of the task.
    /// (url: http://hl7.org/fhir/ValueSet/task-status)
    /// </summary>
    [FhirEnumeration("TaskStatus")]
    public enum TaskStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/task-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/task-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("received", "http://hl7.org/fhir/task-status"), Description("Received")]
        Received,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/task-status"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/task-status"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("ready", "http://hl7.org/fhir/task-status"), Description("Ready")]
        Ready,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/task-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/task-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/task-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("failed", "http://hl7.org/fhir/task-status"), Description("Failed")]
        Failed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/task-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/task-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The current status of the test report.
    /// (url: http://hl7.org/fhir/ValueSet/report-status-codes)
    /// </summary>
    [FhirEnumeration("TestReportStatus")]
    public enum TestReportStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-status-codes)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/report-status-codes"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-status-codes)
        /// </summary>
        [EnumLiteral("in-progress", "http://hl7.org/fhir/report-status-codes"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-status-codes)
        /// </summary>
        [EnumLiteral("waiting", "http://hl7.org/fhir/report-status-codes"), Description("Waiting")]
        Waiting,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-status-codes)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/report-status-codes"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-status-codes)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/report-status-codes"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// The reported execution result.
    /// (url: http://hl7.org/fhir/ValueSet/report-result-codes)
    /// </summary>
    [FhirEnumeration("TestReportResult")]
    public enum TestReportResult
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-result-codes)
        /// </summary>
        [EnumLiteral("pass", "http://hl7.org/fhir/report-result-codes"), Description("Pass")]
        Pass,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-result-codes)
        /// </summary>
        [EnumLiteral("fail", "http://hl7.org/fhir/report-result-codes"), Description("Fail")]
        Fail,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-result-codes)
        /// </summary>
        [EnumLiteral("pending", "http://hl7.org/fhir/report-result-codes"), Description("Pending")]
        Pending,
    }

    /// <summary>
    /// The type of participant.
    /// (url: http://hl7.org/fhir/ValueSet/report-participant-type)
    /// </summary>
    [FhirEnumeration("TestReportParticipantType")]
    public enum TestReportParticipantType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-participant-type)
        /// </summary>
        [EnumLiteral("test-engine", "http://hl7.org/fhir/report-participant-type"), Description("Test Engine")]
        TestEngine,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-participant-type)
        /// </summary>
        [EnumLiteral("client", "http://hl7.org/fhir/report-participant-type"), Description("Client")]
        Client,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-participant-type)
        /// </summary>
        [EnumLiteral("server", "http://hl7.org/fhir/report-participant-type"), Description("Server")]
        Server,
    }

    /// <summary>
    /// The results of executing an action.
    /// (url: http://hl7.org/fhir/ValueSet/report-action-result-codes)
    /// </summary>
    [FhirEnumeration("TestReportActionResult")]
    public enum TestReportActionResult
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-action-result-codes)
        /// </summary>
        [EnumLiteral("pass", "http://hl7.org/fhir/report-action-result-codes"), Description("Pass")]
        Pass,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-action-result-codes)
        /// </summary>
        [EnumLiteral("skip", "http://hl7.org/fhir/report-action-result-codes"), Description("Skip")]
        Skip,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-action-result-codes)
        /// </summary>
        [EnumLiteral("fail", "http://hl7.org/fhir/report-action-result-codes"), Description("Fail")]
        Fail,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-action-result-codes)
        /// </summary>
        [EnumLiteral("warning", "http://hl7.org/fhir/report-action-result-codes"), Description("Warning")]
        Warning,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/report-action-result-codes)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/report-action-result-codes"), Description("Error")]
        Error,
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
    /// The type of contributor
    /// (url: http://hl7.org/fhir/ValueSet/contributor-type)
    /// </summary>
    [FhirEnumeration("ContributorType")]
    public enum ContributorType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contributor-type)
        /// </summary>
        [EnumLiteral("author", "http://hl7.org/fhir/contributor-type"), Description("Author")]
        Author,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contributor-type)
        /// </summary>
        [EnumLiteral("editor", "http://hl7.org/fhir/contributor-type"), Description("Editor")]
        Editor,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contributor-type)
        /// </summary>
        [EnumLiteral("reviewer", "http://hl7.org/fhir/contributor-type"), Description("Reviewer")]
        Reviewer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contributor-type)
        /// </summary>
        [EnumLiteral("endorser", "http://hl7.org/fhir/contributor-type"), Description("Endorser")]
        Endorser,
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
    /// How an element value is interpreted when discrimination is evaluated
    /// (url: http://hl7.org/fhir/ValueSet/discriminator-type)
    /// </summary>
    [FhirEnumeration("DiscriminatorType")]
    public enum DiscriminatorType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/discriminator-type)
        /// </summary>
        [EnumLiteral("value", "http://hl7.org/fhir/discriminator-type"), Description("Value")]
        Value,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/discriminator-type)
        /// </summary>
        [EnumLiteral("exists", "http://hl7.org/fhir/discriminator-type"), Description("Exists")]
        Exists,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/discriminator-type)
        /// </summary>
        [EnumLiteral("pattern", "http://hl7.org/fhir/discriminator-type"), Description("Pattern")]
        Pattern,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/discriminator-type)
        /// </summary>
        [EnumLiteral("type", "http://hl7.org/fhir/discriminator-type"), Description("Type")]
        Type,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/discriminator-type)
        /// </summary>
        [EnumLiteral("profile", "http://hl7.org/fhir/discriminator-type"), Description("Profile")]
        Profile,
    }

    /// <summary>
    /// Whether a reference needs to be version specific or version independent, or whether either can be used
    /// (url: http://hl7.org/fhir/ValueSet/reference-version-rules)
    /// </summary>
    [FhirEnumeration("ReferenceVersionRules")]
    public enum ReferenceVersionRules
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-version-rules)
        /// </summary>
        [EnumLiteral("either", "http://hl7.org/fhir/reference-version-rules"), Description("Either Specific or independent")]
        Either,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-version-rules)
        /// </summary>
        [EnumLiteral("independent", "http://hl7.org/fhir/reference-version-rules"), Description("Version independent")]
        Independent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/reference-version-rules)
        /// </summary>
        [EnumLiteral("specific", "http://hl7.org/fhir/reference-version-rules"), Description("Version Specific")]
        Specific,
    }

    /// <summary>
    /// The type of relationship to the related artifact
    /// (url: http://hl7.org/fhir/ValueSet/related-artifact-type)
    /// </summary>
    [FhirEnumeration("RelatedArtifactType")]
    public enum RelatedArtifactType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("documentation", "http://hl7.org/fhir/related-artifact-type"), Description("Documentation")]
        Documentation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("justification", "http://hl7.org/fhir/related-artifact-type"), Description("Justification")]
        Justification,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("citation", "http://hl7.org/fhir/related-artifact-type"), Description("Citation")]
        Citation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("predecessor", "http://hl7.org/fhir/related-artifact-type"), Description("Predecessor")]
        Predecessor,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("successor", "http://hl7.org/fhir/related-artifact-type"), Description("Successor")]
        Successor,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("derived-from", "http://hl7.org/fhir/related-artifact-type"), Description("Derived From")]
        DerivedFrom,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("depends-on", "http://hl7.org/fhir/related-artifact-type"), Description("Depends On")]
        DependsOn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/related-artifact-type)
        /// </summary>
        [EnumLiteral("composed-of", "http://hl7.org/fhir/related-artifact-type"), Description("Composed Of")]
        ComposedOf,
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
