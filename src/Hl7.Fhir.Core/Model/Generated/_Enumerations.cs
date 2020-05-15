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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{

    /// <summary>
    /// Identification of the underlying physiological mechanism for a Reaction Risk.
    /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-type)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceType")]
    public enum AllergyIntoleranceType
    {
        /// <summary>
        /// A propensity for hypersensitivity reaction(s) to a substance.  These reactions are most typically type I hypersensitivity, plus other "allergy-like" reactions, including pseudoallergy.
        /// (system: http://hl7.org/fhir/allergy-intolerance-type)
        /// </summary>
        [EnumLiteral("allergy", "http://hl7.org/fhir/allergy-intolerance-type"), Description("Allergy")]
        Allergy,
        /// <summary>
        /// A propensity for adverse reactions to a substance that is not judged to be allergic or "allergy-like".  These reactions are typically (but not necessarily) non-immune.  They are to some degree idiosyncratic and/or individually specific (i.e. are not a reaction that is expected to occur with most or all patients given similar circumstances).
        /// (system: http://hl7.org/fhir/allergy-intolerance-type)
        /// </summary>
        [EnumLiteral("intolerance", "http://hl7.org/fhir/allergy-intolerance-type"), Description("Intolerance")]
        Intolerance,
    }

    /// <summary>
    /// Clinical assessment of the severity of a reaction event as a whole, potentially considering multiple different manifestations.
    /// (url: http://hl7.org/fhir/ValueSet/reaction-event-severity)
    /// </summary>
    [FhirEnumeration("AllergyIntoleranceSeverity")]
    public enum AllergyIntoleranceSeverity
    {
        /// <summary>
        /// Causes mild physiological effects.
        /// (system: http://hl7.org/fhir/reaction-event-severity)
        /// </summary>
        [EnumLiteral("mild", "http://hl7.org/fhir/reaction-event-severity"), Description("Mild")]
        Mild,
        /// <summary>
        /// Causes moderate physiological effects.
        /// (system: http://hl7.org/fhir/reaction-event-severity)
        /// </summary>
        [EnumLiteral("moderate", "http://hl7.org/fhir/reaction-event-severity"), Description("Moderate")]
        Moderate,
        /// <summary>
        /// Causes severe physiological effects.
        /// (system: http://hl7.org/fhir/reaction-event-severity)
        /// </summary>
        [EnumLiteral("severe", "http://hl7.org/fhir/reaction-event-severity"), Description("Severe")]
        Severe,
    }

    /// <summary>
    /// Is the Participant required to attend the appointment.
    /// (url: http://hl7.org/fhir/ValueSet/participantrequired)
    /// </summary>
    [FhirEnumeration("ParticipantRequired")]
    public enum ParticipantRequired
    {
        /// <summary>
        /// The participant is required to attend the appointment.
        /// (system: http://hl7.org/fhir/participantrequired)
        /// </summary>
        [EnumLiteral("required", "http://hl7.org/fhir/participantrequired"), Description("Required")]
        Required,
        /// <summary>
        /// The participant may optionally attend the appointment.
        /// (system: http://hl7.org/fhir/participantrequired)
        /// </summary>
        [EnumLiteral("optional", "http://hl7.org/fhir/participantrequired"), Description("Optional")]
        Optional,
        /// <summary>
        /// The participant is excluded from the appointment, and may not be informed of the appointment taking place. (Appointment is about them, not for them - such as 2 doctors discussing results about a patient's test).
        /// (system: http://hl7.org/fhir/participantrequired)
        /// </summary>
        [EnumLiteral("information-only", "http://hl7.org/fhir/participantrequired"), Description("Information Only")]
        InformationOnly,
    }

    /// <summary>
    /// The Participation status of an appointment.
    /// (url: http://hl7.org/fhir/ValueSet/participationstatus)
    /// </summary>
    [FhirEnumeration("ParticipationStatus")]
    public enum ParticipationStatus
    {
        /// <summary>
        /// The participant has accepted the appointment.
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("accepted", "http://hl7.org/fhir/participationstatus"), Description("Accepted")]
        Accepted,
        /// <summary>
        /// The participant has declined the appointment and will not participate in the appointment.
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("declined", "http://hl7.org/fhir/participationstatus"), Description("Declined")]
        Declined,
        /// <summary>
        /// The participant has  tentatively accepted the appointment. This could be automatically created by a system and requires further processing before it can be accepted. There is no commitment that attendance will occur.
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("tentative", "http://hl7.org/fhir/participationstatus"), Description("Tentative")]
        Tentative,
        /// <summary>
        /// The participant needs to indicate if they accept the appointment by changing this status to one of the other statuses.
        /// (system: http://hl7.org/fhir/participationstatus)
        /// </summary>
        [EnumLiteral("needs-action", "http://hl7.org/fhir/participationstatus"), Description("Needs Action")]
        NeedsAction,
    }

    /// <summary>
    /// Indicator for type of action performed during the event that generated the audit.
    /// (url: http://hl7.org/fhir/ValueSet/audit-event-action)
    /// </summary>
    [FhirEnumeration("AuditEventAction")]
    public enum AuditEventAction
    {
        /// <summary>
        /// Create a new database object, such as placing an order.
        /// (system: http://hl7.org/fhir/audit-event-action)
        /// </summary>
        [EnumLiteral("C", "http://hl7.org/fhir/audit-event-action"), Description("Create")]
        C,
        /// <summary>
        /// Display or print data, such as a doctor census.
        /// (system: http://hl7.org/fhir/audit-event-action)
        /// </summary>
        [EnumLiteral("R", "http://hl7.org/fhir/audit-event-action"), Description("Read/View/Print")]
        R,
        /// <summary>
        /// Update data, such as revise patient information.
        /// (system: http://hl7.org/fhir/audit-event-action)
        /// </summary>
        [EnumLiteral("U", "http://hl7.org/fhir/audit-event-action"), Description("Update")]
        U,
        /// <summary>
        /// Delete items, such as a doctor master file record.
        /// (system: http://hl7.org/fhir/audit-event-action)
        /// </summary>
        [EnumLiteral("D", "http://hl7.org/fhir/audit-event-action"), Description("Delete")]
        D,
        /// <summary>
        /// Perform a system or application function such as log-on, program execution or use of an object's method, or perform a query/search operation.
        /// (system: http://hl7.org/fhir/audit-event-action)
        /// </summary>
        [EnumLiteral("E", "http://hl7.org/fhir/audit-event-action"), Description("Execute")]
        E,
    }

    /// <summary>
    /// Indicates whether the event succeeded or failed
    /// (url: http://hl7.org/fhir/ValueSet/audit-event-outcome)
    /// </summary>
    [FhirEnumeration("AuditEventOutcome")]
    public enum AuditEventOutcome
    {
        /// <summary>
        /// The operation completed successfully (whether with warnings or not).
        /// (system: http://hl7.org/fhir/audit-event-outcome)
        /// </summary>
        [EnumLiteral("0", "http://hl7.org/fhir/audit-event-outcome"), Description("Success")]
        N0,
        /// <summary>
        /// The action was not successful due to some kind of catered for error (often equivalent to an HTTP 400 response).
        /// (system: http://hl7.org/fhir/audit-event-outcome)
        /// </summary>
        [EnumLiteral("4", "http://hl7.org/fhir/audit-event-outcome"), Description("Minor failure")]
        N4,
        /// <summary>
        /// The action was not successful due to some kind of unexpected error (often equivalent to an HTTP 500 response).
        /// (system: http://hl7.org/fhir/audit-event-outcome)
        /// </summary>
        [EnumLiteral("8", "http://hl7.org/fhir/audit-event-outcome"), Description("Serious failure")]
        N8,
        /// <summary>
        /// An error of such magnitude occurred that the system is no longer available for use (i.e. the system died).
        /// (system: http://hl7.org/fhir/audit-event-outcome)
        /// </summary>
        [EnumLiteral("12", "http://hl7.org/fhir/audit-event-outcome"), Description("Major failure")]
        N12,
    }

    /// <summary>
    /// Indicates the purpose of a bundle - how it was intended to be used.
    /// (url: http://hl7.org/fhir/ValueSet/bundle-type)
    /// </summary>
    [FhirEnumeration("BundleType")]
    public enum BundleType
    {
        /// <summary>
        /// The bundle is a document. The first resource is a Composition.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("document", "http://hl7.org/fhir/bundle-type"), Description("Document")]
        Document,
        /// <summary>
        /// The bundle is a message. The first resource is a MessageHeader.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("message", "http://hl7.org/fhir/bundle-type"), Description("Message")]
        Message,
        /// <summary>
        /// The bundle is a transaction - intended to be processed by a server as an atomic commit.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("transaction", "http://hl7.org/fhir/bundle-type"), Description("Transaction")]
        Transaction,
        /// <summary>
        /// The bundle is a transaction response. Because the response is a transaction response, the transactionhas succeeded, and all responses are error free.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("transaction-response", "http://hl7.org/fhir/bundle-type"), Description("Transaction Response")]
        TransactionResponse,
        /// <summary>
        /// The bundle is a transaction - intended to be processed by a server as a group of actions.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("batch", "http://hl7.org/fhir/bundle-type"), Description("Batch")]
        Batch,
        /// <summary>
        /// The bundle is a batch response. Note that as a batch, some responses may indicate failure and others success.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("batch-response", "http://hl7.org/fhir/bundle-type"), Description("Batch Response")]
        BatchResponse,
        /// <summary>
        /// The bundle is a list of resources from a history interaction on a server.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("history", "http://hl7.org/fhir/bundle-type"), Description("History List")]
        History,
        /// <summary>
        /// The bundle is a list of resources returned as a result of a search/query interaction, operation, or message.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("searchset", "http://hl7.org/fhir/bundle-type"), Description("Search Results")]
        Searchset,
        /// <summary>
        /// The bundle is a set of resources collected into a single document for ease of distribution.
        /// (system: http://hl7.org/fhir/bundle-type)
        /// </summary>
        [EnumLiteral("collection", "http://hl7.org/fhir/bundle-type"), Description("Collection")]
        Collection,
    }

    /// <summary>
    /// Why an entry is in the result set - whether it's included as a match or because of an _include requirement.
    /// (url: http://hl7.org/fhir/ValueSet/search-entry-mode)
    /// </summary>
    [FhirEnumeration("SearchEntryMode")]
    public enum SearchEntryMode
    {
        /// <summary>
        /// This resource matched the search specification.
        /// (system: http://hl7.org/fhir/search-entry-mode)
        /// </summary>
        [EnumLiteral("match", "http://hl7.org/fhir/search-entry-mode"), Description("Match")]
        Match,
        /// <summary>
        /// This resource is returned because it is referred to from another resource in the search set.
        /// (system: http://hl7.org/fhir/search-entry-mode)
        /// </summary>
        [EnumLiteral("include", "http://hl7.org/fhir/search-entry-mode"), Description("Include")]
        Include,
        /// <summary>
        /// An OperationOutcome that provides additional information about the processing of a search.
        /// (system: http://hl7.org/fhir/search-entry-mode)
        /// </summary>
        [EnumLiteral("outcome", "http://hl7.org/fhir/search-entry-mode"), Description("Outcome")]
        Outcome,
    }

    /// <summary>
    /// HTTP verbs (in the HTTP command line). See [HTTP rfc](https://tools.ietf.org/html/rfc7231) for details.
    /// (url: http://hl7.org/fhir/ValueSet/http-verb)
    /// </summary>
    [FhirEnumeration("HTTPVerb")]
    public enum HTTPVerb
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("GET", "http://hl7.org/fhir/http-verb"), Description("GET")]
        GET,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("HEAD", "http://hl7.org/fhir/http-verb"), Description("HEAD")]
        HEAD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("POST", "http://hl7.org/fhir/http-verb"), Description("POST")]
        POST,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("PUT", "http://hl7.org/fhir/http-verb"), Description("PUT")]
        PUT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("DELETE", "http://hl7.org/fhir/http-verb"), Description("DELETE")]
        DELETE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("PATCH", "http://hl7.org/fhir/http-verb"), Description("PATCH")]
        PATCH,
    }

    /// <summary>
    /// The workflow/clinical status of the composition.
    /// (url: http://hl7.org/fhir/ValueSet/composition-status)
    /// </summary>
    [FhirEnumeration("CompositionStatus")]
    public enum CompositionStatus
    {
        /// <summary>
        /// This is a preliminary composition or document (also known as initial or interim). The content may be incomplete or unverified.
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("preliminary", "http://hl7.org/fhir/composition-status"), Description("Preliminary")]
        Preliminary,
        /// <summary>
        /// This version of the composition is complete and verified by an appropriate person and no further work is planned. Any subsequent updates would be on a new version of the composition.
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("final", "http://hl7.org/fhir/composition-status"), Description("Final")]
        Final,
        /// <summary>
        /// The composition content or the referenced resources have been modified (edited or added to) subsequent to being released as "final" and the composition is complete and verified by an authorized person.
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/composition-status"), Description("Amended")]
        Amended,
        /// <summary>
        /// The composition or document was originally created/issued in error, and this is an amendment that marks that the entire series should not be considered as valid.
        /// (system: http://hl7.org/fhir/composition-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/composition-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The way in which a person authenticated a composition.
    /// (url: http://hl7.org/fhir/ValueSet/composition-attestation-mode)
    /// </summary>
    [FhirEnumeration("CompositionAttestationMode")]
    public enum CompositionAttestationMode
    {
        /// <summary>
        /// The person authenticated the content in their personal capacity.
        /// (system: http://hl7.org/fhir/composition-attestation-mode)
        /// </summary>
        [EnumLiteral("personal", "http://hl7.org/fhir/composition-attestation-mode"), Description("Personal")]
        Personal,
        /// <summary>
        /// The person authenticated the content in their professional capacity.
        /// (system: http://hl7.org/fhir/composition-attestation-mode)
        /// </summary>
        [EnumLiteral("professional", "http://hl7.org/fhir/composition-attestation-mode"), Description("Professional")]
        Professional,
        /// <summary>
        /// The person authenticated the content and accepted legal responsibility for its content.
        /// (system: http://hl7.org/fhir/composition-attestation-mode)
        /// </summary>
        [EnumLiteral("legal", "http://hl7.org/fhir/composition-attestation-mode"), Description("Legal")]
        Legal,
        /// <summary>
        /// The organization authenticated the content as consistent with their policies and procedures.
        /// (system: http://hl7.org/fhir/composition-attestation-mode)
        /// </summary>
        [EnumLiteral("official", "http://hl7.org/fhir/composition-attestation-mode"), Description("Official")]
        Official,
    }

    /// <summary>
    /// The processing mode that applies to this list
    /// (url: http://hl7.org/fhir/ValueSet/list-mode)
    /// </summary>
    [FhirEnumeration("ListMode")]
    public enum ListMode
    {
        /// <summary>
        /// This list is the master list, maintained in an ongoing fashion with regular updates as the real world list it is tracking changes
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("working", "http://hl7.org/fhir/list-mode"), Description("Working List")]
        Working,
        /// <summary>
        /// This list was prepared as a snapshot. It should not be assumed to be current
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("snapshot", "http://hl7.org/fhir/list-mode"), Description("Snapshot List")]
        Snapshot,
        /// <summary>
        /// A list that indicates where changes have been made or recommended
        /// (system: http://hl7.org/fhir/list-mode)
        /// </summary>
        [EnumLiteral("changes", "http://hl7.org/fhir/list-mode"), Description("Change List")]
        Changes,
    }

    /// <summary>
    /// The verification status to support or decline the clinical status of the condition or diagnosis.
    /// (url: http://hl7.org/fhir/ValueSet/condition-ver-status)
    /// </summary>
    [FhirEnumeration("ConditionVerificationStatus")]
    public enum ConditionVerificationStatus
    {
        /// <summary>
        /// This is a tentative diagnosis - still a candidate that is under consideration.
        /// (system: http://hl7.org/fhir/condition-ver-status)
        /// </summary>
        [EnumLiteral("provisional", "http://hl7.org/fhir/condition-ver-status"), Description("Provisional")]
        Provisional,
        /// <summary>
        /// One of a set of potential (and typically mutually exclusive) diagnosis asserted to further guide the diagnostic process and preliminary treatment.
        /// (system: http://hl7.org/fhir/condition-ver-status)
        /// </summary>
        [EnumLiteral("differential", "http://hl7.org/fhir/condition-ver-status"), Description("Differential")]
        Differential,
        /// <summary>
        /// There is sufficient diagnostic and/or clinical evidence to treat this as a confirmed condition.
        /// (system: http://hl7.org/fhir/condition-ver-status)
        /// </summary>
        [EnumLiteral("confirmed", "http://hl7.org/fhir/condition-ver-status"), Description("Confirmed")]
        Confirmed,
        /// <summary>
        /// This condition has been ruled out by diagnostic and clinical evidence.
        /// (system: http://hl7.org/fhir/condition-ver-status)
        /// </summary>
        [EnumLiteral("refuted", "http://hl7.org/fhir/condition-ver-status"), Description("Refuted")]
        Refuted,
        /// <summary>
        /// The statement was entered in error and is not valid.
        /// (system: http://hl7.org/fhir/condition-ver-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/condition-ver-status"), Description("Entered In Error")]
        EnteredInError,
        /// <summary>
        /// The condition status is unknown.  Note that "unknown" is a value of last resort and every attempt should be made to provide a meaningful value other than "unknown".
        /// (system: http://hl7.org/fhir/condition-ver-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/condition-ver-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// A code that indicates whether an application accepts unknown elements or extensions when reading resources.
    /// (url: http://hl7.org/fhir/ValueSet/unknown-content-code)
    /// </summary>
    [FhirEnumeration("UnknownContentCode")]
    public enum UnknownContentCode
    {
        /// <summary>
        /// The application does not accept either unknown elements or extensions.
        /// (system: http://hl7.org/fhir/unknown-content-code)
        /// </summary>
        [EnumLiteral("no", "http://hl7.org/fhir/unknown-content-code"), Description("Neither Elements or Extensions")]
        No,
        /// <summary>
        /// The application accepts unknown extensions, but not unknown elements.
        /// (system: http://hl7.org/fhir/unknown-content-code)
        /// </summary>
        [EnumLiteral("extensions", "http://hl7.org/fhir/unknown-content-code"), Description("Unknown Extensions")]
        Extensions,
        /// <summary>
        /// The application accepts unknown elements, but not unknown extensions.
        /// (system: http://hl7.org/fhir/unknown-content-code)
        /// </summary>
        [EnumLiteral("elements", "http://hl7.org/fhir/unknown-content-code"), Description("Unknown Elements")]
        Elements,
        /// <summary>
        /// The application accepts unknown elements and extensions.
        /// (system: http://hl7.org/fhir/unknown-content-code)
        /// </summary>
        [EnumLiteral("both", "http://hl7.org/fhir/unknown-content-code"), Description("Unknown Elements and Extensions")]
        Both,
    }

    /// <summary>
    /// One of the resource types defined as part of FHIR.
    /// (url: http://hl7.org/fhir/ValueSet/resource-types)
    /// </summary>
    [FhirEnumeration("ResourceType")]
    public enum ResourceType
    {
        /// <summary>
        /// A financial tool for tracking value accrued for a particular purpose.  In the healthcare field, used to track charges for a patient, cost centres, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Account", "http://hl7.org/fhir/resource-types"), Description("Account")]
        Account,
        /// <summary>
        /// Risk of harmful or undesirable, physiological response which is unique to an individual and associated with exposure to a substance.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AllergyIntolerance", "http://hl7.org/fhir/resource-types"), Description("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// A booking of a healthcare event among patient(s), practitioner(s), related person(s) and/or device(s) for a specific date/time. This may result in one or more Encounter(s).
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Appointment", "http://hl7.org/fhir/resource-types"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// A reply to an appointment request for a patient and/or practitioner(s), such as a confirmation or rejection.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/resource-types"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// A record of an event made for purposes of maintaining a security log. Typical uses include detection of intrusion attempts and monitoring for inappropriate usage.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("AuditEvent", "http://hl7.org/fhir/resource-types"), Description("AuditEvent")]
        AuditEvent,
        /// <summary>
        /// Basic is used for handling concepts not yet defined in FHIR, narrative-only resources that don't map to an existing resource, and custom resources not appropriate for inclusion in the FHIR specification.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Basic", "http://hl7.org/fhir/resource-types"), Description("Basic")]
        Basic,
        /// <summary>
        /// A binary resource can contain any content, whether text, image, pdf, zip archive, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Binary", "http://hl7.org/fhir/resource-types"), Description("Binary")]
        Binary,
        /// <summary>
        /// Record details about the anatomical location of a specimen or body part.  This resource may be used when a coded concept does not provide the necessary detail needed for the use case.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodySite", "http://hl7.org/fhir/resource-types"), Description("BodySite")]
        BodySite,
        /// <summary>
        /// A container for a collection of resources.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Bundle", "http://hl7.org/fhir/resource-types"), Description("Bundle")]
        Bundle,
        /// <summary>
        /// Describes the intention of how one or more practitioners intend to deliver care for a particular patient, group or community for a period of time, possibly limited to care for a specific condition or set of conditions.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CarePlan", "http://hl7.org/fhir/resource-types"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// A provider issued list of services and products provided, or to be provided, to a patient which is provided to an insurer for payment recovery.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Claim", "http://hl7.org/fhir/resource-types"), Description("Claim")]
        Claim,
        /// <summary>
        /// This resource provides the adjudication details from the processing of a Claim resource.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClaimResponse", "http://hl7.org/fhir/resource-types"), Description("ClaimResponse")]
        ClaimResponse,
        /// <summary>
        /// A record of a clinical assessment performed to determine what problem(s) may affect the patient and before planning the treatments or management strategies that are best to manage a patient's condition. Assessments are often 1:1 with a clinical consultation / encounter,  but this varies greatly depending on the clinical workflow. This resource is called "ClinicalImpression" rather than "ClinicalAssessment" to avoid confusion with the recording of assessment tools such as Apgar score.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ClinicalImpression", "http://hl7.org/fhir/resource-types"), Description("ClinicalImpression")]
        ClinicalImpression,
        /// <summary>
        /// An occurrence of information being transmitted; e.g. an alert that was sent to a responsible provider, a public health agency was notified about a reportable condition.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Communication", "http://hl7.org/fhir/resource-types"), Description("Communication")]
        Communication,
        /// <summary>
        /// A request to convey information; e.g. the CDS system proposes that an alert be sent to a responsible provider, the CDS system proposes that the public health agency be notified about a reportable condition.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// A set of healthcare-related information that is assembled together into a single logical document that provides a single coherent statement of meaning, establishes its own context and that has clinical attestation with regard to who is making the statement. While a Composition defines the structure, it does not actually contain the content: rather the full content of a document is contained in a Bundle, of which the Composition is the first resource contained.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Composition", "http://hl7.org/fhir/resource-types"), Description("Composition")]
        Composition,
        /// <summary>
        /// A statement of relationships from one set of concepts to one or more other concepts - either code systems or data elements, or classes in class models.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ConceptMap", "http://hl7.org/fhir/resource-types"), Description("ConceptMap")]
        ConceptMap,
        /// <summary>
        /// Use to record detailed information about conditions, problems or diagnoses recognized by a clinician. There are many uses including: recording a diagnosis during an encounter; populating a problem list or a summary statement, such as a discharge summary.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Condition", "http://hl7.org/fhir/resource-types"), Description("Condition")]
        Condition,
        /// <summary>
        /// A conformance statement is a set of capabilities of a FHIR Server that may be used as a statement of actual server functionality or a statement of required or desired server implementation.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Conformance", "http://hl7.org/fhir/resource-types"), Description("Conformance")]
        Conformance,
        /// <summary>
        /// A formal agreement between parties regarding the conduct of business, exchange of information or other matters.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Contract", "http://hl7.org/fhir/resource-types"), Description("Contract")]
        Contract,
        /// <summary>
        /// Financial instrument which may be used to pay for or reimburse health care products and services.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Coverage", "http://hl7.org/fhir/resource-types"), Description("Coverage")]
        Coverage,
        /// <summary>
        /// The formal description of a single piece of information that can be gathered and reported.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DataElement", "http://hl7.org/fhir/resource-types"), Description("DataElement")]
        DataElement,
        /// <summary>
        /// Indicates an actual or potential clinical issue with or between one or more active or proposed clinical actions for a patient; e.g. Drug-drug interaction, Ineffective treatment frequency, Procedure-condition conflict, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DetectedIssue", "http://hl7.org/fhir/resource-types"), Description("DetectedIssue")]
        DetectedIssue,
        /// <summary>
        /// This resource identifies an instance of a manufactured item that is used in the provision of healthcare without being substantially changed through that activity. The device may be a medical or non-medical device.  Medical devices includes durable (reusable) medical equipment, implantable devices, as well as disposable equipment used for diagnostic, treatment, and research for healthcare and public health.  Non-medical devices may include items such as a machine, cellphone, computer, application, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Device", "http://hl7.org/fhir/resource-types"), Description("Device")]
        Device,
        /// <summary>
        /// Describes the characteristics, operational status and capabilities of a medical-related component of a medical device.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceComponent", "http://hl7.org/fhir/resource-types"), Description("DeviceComponent")]
        DeviceComponent,
        /// <summary>
        /// Describes a measurement, calculation or setting capability of a medical device.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceMetric", "http://hl7.org/fhir/resource-types"), Description("DeviceMetric")]
        DeviceMetric,
        /// <summary>
        /// Represents a request for a patient to employ a medical device. The device may be an implantable device, or an external assistive device, such as a walker.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceUseRequest")]
        DeviceUseRequest,
        /// <summary>
        /// A record of a device being used by a patient where the record is the result of a report from the patient or another clinician.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceUseStatement", "http://hl7.org/fhir/resource-types"), Description("DeviceUseStatement")]
        DeviceUseStatement,
        /// <summary>
        /// A record of a request for a diagnostic investigation service to be performed.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticOrder", "http://hl7.org/fhir/resource-types"), Description("DiagnosticOrder")]
        DiagnosticOrder,
        /// <summary>
        /// The findings and interpretation of diagnostic  tests performed on patients, groups of patients, devices, and locations, and/or specimens derived from these. The report includes clinical context such as requesting and provider information, and some mix of atomic results, images, textual and coded interpretations, and formatted representation of diagnostic reports.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DiagnosticReport", "http://hl7.org/fhir/resource-types"), Description("DiagnosticReport")]
        DiagnosticReport,
        /// <summary>
        /// A manifest that defines a set of documents.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentManifest", "http://hl7.org/fhir/resource-types"), Description("DocumentManifest")]
        DocumentManifest,
        /// <summary>
        /// A reference to a document .
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DocumentReference", "http://hl7.org/fhir/resource-types"), Description("DocumentReference")]
        DocumentReference,
        /// <summary>
        /// --- Abstract Type! ---A resource that includes narrative, extensions, and contained resources.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DomainResource", "http://hl7.org/fhir/resource-types"), Description("DomainResource")]
        DomainResource,
        /// <summary>
        /// This resource provides the insurance eligibility details from the insurer regarding a specified coverage and optionally some class of service.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("EligibilityRequest")]
        EligibilityRequest,
        /// <summary>
        /// This resource provides eligibility and plan details from the processing of an Eligibility resource.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("EligibilityResponse")]
        EligibilityResponse,
        /// <summary>
        /// An interaction between a patient and healthcare provider(s) for the purpose of providing healthcare service(s) or assessing the health status of a patient.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Encounter", "http://hl7.org/fhir/resource-types"), Description("Encounter")]
        Encounter,
        /// <summary>
        /// This resource provides the insurance enrollment details to the insurer regarding a specified coverage.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/resource-types"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// This resource provides enrollment and plan details from the processing of an Enrollment resource.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentResponse", "http://hl7.org/fhir/resource-types"), Description("EnrollmentResponse")]
        EnrollmentResponse,
        /// <summary>
        /// An association between a patient and an organization / healthcare provider(s) during which time encounters may occur. The managing organization assumes a level of responsibility for the patient during this time.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EpisodeOfCare", "http://hl7.org/fhir/resource-types"), Description("EpisodeOfCare")]
        EpisodeOfCare,
        /// <summary>
        /// This resource provides: the claim details; adjudication details from the processing of a Claim; and optionally account balance information, for informing the subscriber of the benefits provided.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExplanationOfBenefit", "http://hl7.org/fhir/resource-types"), Description("ExplanationOfBenefit")]
        ExplanationOfBenefit,
        /// <summary>
        /// Significant health events and conditions for a person related to the patient relevant in the context of care for the patient.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("FamilyMemberHistory", "http://hl7.org/fhir/resource-types"), Description("FamilyMemberHistory")]
        FamilyMemberHistory,
        /// <summary>
        /// Prospective warnings of potential issues when providing care to the patient.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Flag", "http://hl7.org/fhir/resource-types"), Description("Flag")]
        Flag,
        /// <summary>
        /// Describes the intended objective(s) for a patient, group or organization care, for example, weight loss, restoring an activity of daily living, obtaining herd immunity via immunization, meeting a process improvement objective, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Goal", "http://hl7.org/fhir/resource-types"), Description("Goal")]
        Goal,
        /// <summary>
        /// Represents a defined collection of entities that may be discussed or acted upon collectively but which are not expected to act collectively and are not formally or legally recognized; i.e. a collection of entities that isn't an Organization.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Group", "http://hl7.org/fhir/resource-types"), Description("Group")]
        Group,
        /// <summary>
        /// The details of a healthcare service available at a location.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("HealthcareService", "http://hl7.org/fhir/resource-types"), Description("HealthcareService")]
        HealthcareService,
        /// <summary>
        /// A manifest of a set of DICOM Service-Object Pair Instances (SOP Instances).  The referenced SOP Instances (images or other content) are for a single patient, and may be from one or more studies. The referenced SOP Instances have been selected for a purpose, such as quality assurance, conference, or consult. Reflecting that range of purposes, typical ImagingObjectSelection resources may include all SOP Instances in a study (perhaps for sharing through a Health Information Exchange); key images from multiple studies (for reference by a referring or treating physician); a multi-frame ultrasound instance ("cine" video clip) and a set of measurements taken from that instance (for inclusion in a teaching file); and so on.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingObjectSelection", "http://hl7.org/fhir/resource-types"), Description("ImagingObjectSelection")]
        ImagingObjectSelection,
        /// <summary>
        /// Representation of the content produced in a DICOM imaging study. A study comprises a set of series, each of which includes a set of Service-Object Pair Instances (SOP Instances - images or other data) acquired or produced in a common context.  A series is of only one modality (e.g. X-ray, CT, MR, ultrasound), but a study may have multiple series of different modalities.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImagingStudy", "http://hl7.org/fhir/resource-types"), Description("ImagingStudy")]
        ImagingStudy,
        /// <summary>
        /// Describes the event of a patient being administered a vaccination or a record of a vaccination as reported by a patient, a clinician or another party and may include vaccine reaction information and what vaccination protocol was followed.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Immunization", "http://hl7.org/fhir/resource-types"), Description("Immunization")]
        Immunization,
        /// <summary>
        /// A patient's point-in-time immunization and recommendation (i.e. forecasting a patient's immunization eligibility according to a published schedule) with optional supporting justification.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// A set of rules or how FHIR is used to solve a particular problem. This resource is used to gather all the parts of an implementation guide into a logical whole, and to publish a computable definition of all the parts.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImplementationGuide", "http://hl7.org/fhir/resource-types"), Description("ImplementationGuide")]
        ImplementationGuide,
        /// <summary>
        /// A set of information summarized from a list of other resources.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("List", "http://hl7.org/fhir/resource-types"), Description("List")]
        List,
        /// <summary>
        /// Details and position information for a physical place where services are provided  and resources and participants may be stored, found, contained or accommodated.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Location", "http://hl7.org/fhir/resource-types"), Description("Location")]
        Location,
        /// <summary>
        /// A photo, video, or audio recording acquired or used in healthcare. The actual content may be inline or provided by direct reference.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Media", "http://hl7.org/fhir/resource-types"), Description("Media")]
        Media,
        /// <summary>
        /// This resource is primarily used for the identification and definition of a medication. It covers the ingredients and the packaging for a medication.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Medication", "http://hl7.org/fhir/resource-types"), Description("Medication")]
        Medication,
        /// <summary>
        /// Describes the event of a patient consuming or otherwise being administered a medication.  This may be as simple as swallowing a tablet or it may be a long running infusion.  Related resources tie this event to the authorizing prescription, and the specific encounter between patient and health care practitioner.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationAdministration", "http://hl7.org/fhir/resource-types"), Description("MedicationAdministration")]
        MedicationAdministration,
        /// <summary>
        /// Indicates that a medication product is to be or has been dispensed for a named person/patient.  This includes a description of the medication product (supply) provided and the instructions for administering the medication.  The medication dispense is the result of a pharmacy system responding to a medication order.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationDispense", "http://hl7.org/fhir/resource-types"), Description("MedicationDispense")]
        MedicationDispense,
        /// <summary>
        /// An order for both supply of the medication and the instructions for administration of the medication to a patient. The resource is called "MedicationOrder" rather than "MedicationPrescription" to generalize the use across inpatient and outpatient settings as well as for care plans, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationOrder", "http://hl7.org/fhir/resource-types"), Description("MedicationOrder")]
        MedicationOrder,
        /// <summary>
        /// A record of a medication that is being consumed by a patient.   A MedicationStatement may indicate that the patient may be taking the medication now, or has taken the medication in the past or will be taking the medication in the future.  The source of this information can be the patient, significant other (such as a family member or spouse), or a clinician.  A common scenario where this information is captured is during the history taking process during a patient visit or stay.   The medication information may come from e.g. the patient's memory, from a prescription bottle,  or from a list of medications the patient, clinician or other party maintains <br/>
        /// The primary difference between a medication statement and a medication administration is that the medication administration has complete administration information and is based on actual administration information from the person who administered the medication.  A medication statement is often, if not always, less specific.  There is no required date/time when the medication was administered, in fact we only know that a source has reported the patient is taking this medication, where details such as time, quantity, or rate or even medication product may be incomplete or missing or less precise.  As stated earlier, the medication statement information may come from the patient's memory, from a prescription bottle or from a list of medications the patient, clinician or other party maintains.  Medication administration is more formal and is not missing detailed information.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
        /// <summary>
        /// The header for a message exchange that is either requesting or responding to an action.  The reference(s) that are the subject of the action as well as other information related to the action are typically transmitted in a bundle in which the MessageHeader resource instance is the first resource in the bundle.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
        /// <summary>
        /// A curated namespace that issues unique symbols within that namespace for the identification of concepts, people, devices, etc.  Represents a "System" used within the Identifier and Coding data types.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NamingSystem", "http://hl7.org/fhir/resource-types"), Description("NamingSystem")]
        NamingSystem,
        /// <summary>
        /// A request to supply a diet, formula feeding (enteral) or oral nutritional supplement to a patient/resident.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/resource-types"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// Measurements and simple assertions made about a patient, device or other subject.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Observation", "http://hl7.org/fhir/resource-types"), Description("Observation")]
        Observation,
        /// <summary>
        /// A formal computable definition of an operation (on the RESTful interface) or a named query (using the search interaction).
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationDefinition", "http://hl7.org/fhir/resource-types"), Description("OperationDefinition")]
        OperationDefinition,
        /// <summary>
        /// A collection of error, warning or information messages that result from a system action.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OperationOutcome", "http://hl7.org/fhir/resource-types"), Description("OperationOutcome")]
        OperationOutcome,
        /// <summary>
        /// A request to perform an action.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Order", "http://hl7.org/fhir/resource-types"), Description("Order")]
        Order,
        /// <summary>
        /// A response to an order.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OrderResponse", "http://hl7.org/fhir/resource-types"), Description("OrderResponse")]
        OrderResponse,
        /// <summary>
        /// A formally or informally recognized grouping of people or organizations formed for the purpose of achieving some form of collective action.  Includes companies, institutions, corporations, departments, community groups, healthcare practice groups, etc.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Organization", "http://hl7.org/fhir/resource-types"), Description("Organization")]
        Organization,
        /// <summary>
        /// This special resource type is used to represent an operation request and response (operations.html). It has no other use, and there is no RESTful endpoint associated with it.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Parameters", "http://hl7.org/fhir/resource-types"), Description("Parameters")]
        Parameters,
        /// <summary>
        /// Demographics and other administrative information about an individual or animal receiving care or other health-related services.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Patient", "http://hl7.org/fhir/resource-types"), Description("Patient")]
        Patient,
        /// <summary>
        /// This resource provides the status of the payment for goods and services rendered, and the request and response resource references.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentNotice", "http://hl7.org/fhir/resource-types"), Description("PaymentNotice")]
        PaymentNotice,
        /// <summary>
        /// This resource provides payment details and claim references supporting a bulk payment.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("PaymentReconciliation", "http://hl7.org/fhir/resource-types"), Description("PaymentReconciliation")]
        PaymentReconciliation,
        /// <summary>
        /// Demographics and administrative information about a person independent of a specific health-related context.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Person", "http://hl7.org/fhir/resource-types"), Description("Person")]
        Person,
        /// <summary>
        /// A person who is directly or indirectly involved in the provisioning of healthcare.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Practitioner", "http://hl7.org/fhir/resource-types"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// An action that is or was performed on a patient. This can be a physical intervention like an operation, or less invasive like counseling or hypnotherapy.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Procedure", "http://hl7.org/fhir/resource-types"), Description("Procedure")]
        Procedure,
        /// <summary>
        /// A request for a procedure to be performed. May be a proposal or an order.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcedureRequest", "http://hl7.org/fhir/resource-types"), Description("ProcedureRequest")]
        ProcedureRequest,
        /// <summary>
        /// This resource provides the target, request and response, and action details for an action to be performed by the target on or about existing resources.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessRequest", "http://hl7.org/fhir/resource-types"), Description("ProcessRequest")]
        ProcessRequest,
        /// <summary>
        /// This resource provides processing status, errors and notes from the processing of a resource.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ProcessResponse", "http://hl7.org/fhir/resource-types"), Description("ProcessResponse")]
        ProcessResponse,
        /// <summary>
        /// Provenance of a resource is a record that describes entities and processes involved in producing and delivering or otherwise influencing that resource. Provenance provides a critical foundation for assessing authenticity, enabling trust, and allowing reproducibility. Provenance assertions are a form of contextual metadata and can themselves become important records with their own provenance. Provenance statement indicates clinical significance in terms of confidence in authenticity, reliability, and trustworthiness, integrity, and stage in lifecycle (e.g. Document Completion - has the artifact been legally authenticated), all of which may impact security, privacy, and trust policies.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Provenance", "http://hl7.org/fhir/resource-types"), Description("Provenance")]
        Provenance,
        /// <summary>
        /// A structured set of questions intended to guide the collection of answers. The questions are ordered and grouped into coherent subsets, corresponding to the structure of the grouping of the underlying questions.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Questionnaire", "http://hl7.org/fhir/resource-types"), Description("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// A structured set of questions and their answers. The questions are ordered and grouped into coherent subsets, corresponding to the structure of the grouping of the underlying questions.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("QuestionnaireResponse", "http://hl7.org/fhir/resource-types"), Description("QuestionnaireResponse")]
        QuestionnaireResponse,
        /// <summary>
        /// Used to record and send details about a request for referral service or transfer of a patient to the care of another provider or provider organization.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ReferralRequest", "http://hl7.org/fhir/resource-types"), Description("ReferralRequest")]
        ReferralRequest,
        /// <summary>
        /// Information about a person that is involved in the care for a patient, but who is not the target of healthcare, nor has a formal responsibility in the care process.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RelatedPerson", "http://hl7.org/fhir/resource-types"), Description("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// --- Abstract Type! ---This is the base resource type for everything.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Resource", "http://hl7.org/fhir/resource-types"), Description("Resource")]
        Resource,
        /// <summary>
        /// An assessment of the likely outcome(s) for a patient or other subject as well as the likelihood of each outcome.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("RiskAssessment", "http://hl7.org/fhir/resource-types"), Description("RiskAssessment")]
        RiskAssessment,
        /// <summary>
        /// A container for slot(s) of time that may be available for booking appointments.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Schedule", "http://hl7.org/fhir/resource-types"), Description("Schedule")]
        Schedule,
        /// <summary>
        /// A search parameter that defines a named search item that can be used to search/filter on a resource.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SearchParameter", "http://hl7.org/fhir/resource-types"), Description("SearchParameter")]
        SearchParameter,
        /// <summary>
        /// A slot of time on a schedule that may be available for booking appointments.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Slot", "http://hl7.org/fhir/resource-types"), Description("Slot")]
        Slot,
        /// <summary>
        /// A sample to be used for analysis.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Specimen", "http://hl7.org/fhir/resource-types"), Description("Specimen")]
        Specimen,
        /// <summary>
        /// A definition of a FHIR structure. This resource is used to describe the underlying resources, data types defined in FHIR, and also for describing extensions, and constraints on resources and data types.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("StructureDefinition", "http://hl7.org/fhir/resource-types"), Description("StructureDefinition")]
        StructureDefinition,
        /// <summary>
        /// The subscription resource is used to define a push based subscription from a server to another system. Once a subscription is registered with the server, the server checks every resource that is created or updated, and if the resource matches the given criteria, it sends a message on the defined "channel" so that another system is able to take an appropriate action.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Subscription", "http://hl7.org/fhir/resource-types"), Description("Subscription")]
        Subscription,
        /// <summary>
        /// A homogeneous material with a definite composition.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Substance", "http://hl7.org/fhir/resource-types"), Description("Substance")]
        Substance,
        /// <summary>
        /// Record of delivery of what is supplied.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyDelivery", "http://hl7.org/fhir/resource-types"), Description("SupplyDelivery")]
        SupplyDelivery,
        /// <summary>
        /// A record of a request for a medication, substance or device used in the healthcare setting.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/resource-types"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// TestScript is a resource that specifies a suite of tests against a FHIR server implementation to determine compliance against the FHIR specification.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("TestScript", "http://hl7.org/fhir/resource-types"), Description("TestScript")]
        TestScript,
        /// <summary>
        /// A value set specifies a set of codes drawn from one or more code systems.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ValueSet", "http://hl7.org/fhir/resource-types"), Description("ValueSet")]
        ValueSet,
        /// <summary>
        /// An authorization for the supply of glasses and/or contact lenses to a patient.
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
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
        [EnumLiteral("BiologicallyDerivedProduct", "http://hl7.org/fhir/resource-types"), Description("BiologicallyDerivedProduct")]
        BiologicallyDerivedProduct,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("BodyStructure", "http://hl7.org/fhir/resource-types"), Description("BodyStructure")]
        BodyStructure,
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
        [EnumLiteral("CareTeam", "http://hl7.org/fhir/resource-types"), Description("CareTeam")]
        CareTeam,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CatalogEntry", "http://hl7.org/fhir/resource-types"), Description("CatalogEntry")]
        CatalogEntry,
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
        [EnumLiteral("ChargeItemDefinition", "http://hl7.org/fhir/resource-types"), Description("ChargeItemDefinition")]
        ChargeItemDefinition,
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
        [EnumLiteral("CompartmentDefinition", "http://hl7.org/fhir/resource-types"), Description("CompartmentDefinition")]
        CompartmentDefinition,
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
        [EnumLiteral("CoverageEligibilityRequest", "http://hl7.org/fhir/resource-types"), Description("CoverageEligibilityRequest")]
        CoverageEligibilityRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("CoverageEligibilityResponse", "http://hl7.org/fhir/resource-types"), Description("CoverageEligibilityResponse")]
        CoverageEligibilityResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("DeviceDefinition", "http://hl7.org/fhir/resource-types"), Description("DeviceDefinition")]
        DeviceDefinition,
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
        [EnumLiteral("EffectEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("EffectEvidenceSynthesis")]
        EffectEvidenceSynthesis,
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
        [EnumLiteral("EventDefinition", "http://hl7.org/fhir/resource-types"), Description("EventDefinition")]
        EventDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Evidence", "http://hl7.org/fhir/resource-types"), Description("Evidence")]
        Evidence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("EvidenceVariable", "http://hl7.org/fhir/resource-types"), Description("EvidenceVariable")]
        EvidenceVariable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ExampleScenario", "http://hl7.org/fhir/resource-types"), Description("ExampleScenario")]
        ExampleScenario,
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
        [EnumLiteral("GuidanceResponse", "http://hl7.org/fhir/resource-types"), Description("GuidanceResponse")]
        GuidanceResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationEvaluation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationEvaluation")]
        ImmunizationEvaluation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("InsurancePlan", "http://hl7.org/fhir/resource-types"), Description("InsurancePlan")]
        InsurancePlan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("Invoice", "http://hl7.org/fhir/resource-types"), Description("Invoice")]
        Invoice,
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
        [EnumLiteral("MedicationKnowledge", "http://hl7.org/fhir/resource-types"), Description("MedicationKnowledge")]
        MedicationKnowledge,
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
        [EnumLiteral("MedicinalProduct", "http://hl7.org/fhir/resource-types"), Description("MedicinalProduct")]
        MedicinalProduct,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductAuthorization", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductAuthorization")]
        MedicinalProductAuthorization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductContraindication", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductContraindication")]
        MedicinalProductContraindication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductIndication", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductIndication")]
        MedicinalProductIndication,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductIngredient", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductIngredient")]
        MedicinalProductIngredient,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductInteraction", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductInteraction")]
        MedicinalProductInteraction,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductManufactured", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductManufactured")]
        MedicinalProductManufactured,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductPackaged", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductPackaged")]
        MedicinalProductPackaged,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductPharmaceutical", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductPharmaceutical")]
        MedicinalProductPharmaceutical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("MedicinalProductUndesirableEffect", "http://hl7.org/fhir/resource-types"), Description("MedicinalProductUndesirableEffect")]
        MedicinalProductUndesirableEffect,
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
        [EnumLiteral("MolecularSequence", "http://hl7.org/fhir/resource-types"), Description("MolecularSequence")]
        MolecularSequence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ObservationDefinition", "http://hl7.org/fhir/resource-types"), Description("ObservationDefinition")]
        ObservationDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("OrganizationAffiliation", "http://hl7.org/fhir/resource-types"), Description("OrganizationAffiliation")]
        OrganizationAffiliation,
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
        [EnumLiteral("PractitionerRole", "http://hl7.org/fhir/resource-types"), Description("PractitionerRole")]
        PractitionerRole,
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
        [EnumLiteral("ResearchDefinition", "http://hl7.org/fhir/resource-types"), Description("ResearchDefinition")]
        ResearchDefinition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ResearchElementDefinition", "http://hl7.org/fhir/resource-types"), Description("ResearchElementDefinition")]
        ResearchElementDefinition,
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
        [EnumLiteral("RiskEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("RiskEvidenceSynthesis")]
        RiskEvidenceSynthesis,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/resource-types"), Description("ServiceRequest")]
        ServiceRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SpecimenDefinition", "http://hl7.org/fhir/resource-types"), Description("SpecimenDefinition")]
        SpecimenDefinition,
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
        [EnumLiteral("SubstanceNucleicAcid", "http://hl7.org/fhir/resource-types"), Description("SubstanceNucleicAcid")]
        SubstanceNucleicAcid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstancePolymer", "http://hl7.org/fhir/resource-types"), Description("SubstancePolymer")]
        SubstancePolymer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceProtein", "http://hl7.org/fhir/resource-types"), Description("SubstanceProtein")]
        SubstanceProtein,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceReferenceInformation", "http://hl7.org/fhir/resource-types"), Description("SubstanceReferenceInformation")]
        SubstanceReferenceInformation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceSourceMaterial", "http://hl7.org/fhir/resource-types"), Description("SubstanceSourceMaterial")]
        SubstanceSourceMaterial,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("SubstanceSpecification", "http://hl7.org/fhir/resource-types"), Description("SubstanceSpecification")]
        SubstanceSpecification,
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
        [EnumLiteral("TerminologyCapabilities", "http://hl7.org/fhir/resource-types"), Description("TerminologyCapabilities")]
        TerminologyCapabilities,
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
        [EnumLiteral("VerificationResult", "http://hl7.org/fhir/resource-types"), Description("VerificationResult")]
        VerificationResult,
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
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest")]
        ImagingManifest,
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
    }

    /// <summary>
    /// How the system supports versioning for a resource.
    /// (url: http://hl7.org/fhir/ValueSet/versioning-policy)
    /// </summary>
    [FhirEnumeration("ResourceVersionPolicy")]
    public enum ResourceVersionPolicy
    {
        /// <summary>
        /// VersionId meta-property is not supported (server) or used (client).
        /// (system: http://hl7.org/fhir/versioning-policy)
        /// </summary>
        [EnumLiteral("no-version", "http://hl7.org/fhir/versioning-policy"), Description("No VersionId Support")]
        NoVersion,
        /// <summary>
        /// VersionId meta-property is supported (server) or used (client).
        /// (system: http://hl7.org/fhir/versioning-policy)
        /// </summary>
        [EnumLiteral("versioned", "http://hl7.org/fhir/versioning-policy"), Description("Versioned")]
        Versioned,
        /// <summary>
        /// VersionId is must be correct for updates (server) or will be specified (If-match header) for updates (client).
        /// (system: http://hl7.org/fhir/versioning-policy)
        /// </summary>
        [EnumLiteral("versioned-update", "http://hl7.org/fhir/versioning-policy"), Description("VersionId tracked fully")]
        VersionedUpdate,
    }

    /// <summary>
    /// A code that indicates how the server supports conditional delete.
    /// (url: http://hl7.org/fhir/ValueSet/conditional-delete-status)
    /// </summary>
    [FhirEnumeration("ConditionalDeleteStatus")]
    public enum ConditionalDeleteStatus
    {
        /// <summary>
        /// No support for conditional deletes.
        /// (system: http://hl7.org/fhir/conditional-delete-status)
        /// </summary>
        [EnumLiteral("not-supported", "http://hl7.org/fhir/conditional-delete-status"), Description("Not Supported")]
        NotSupported,
        /// <summary>
        /// Conditional deletes are supported, but only single resources at a time.
        /// (system: http://hl7.org/fhir/conditional-delete-status)
        /// </summary>
        [EnumLiteral("single", "http://hl7.org/fhir/conditional-delete-status"), Description("Single Deletes Supported")]
        Single,
        /// <summary>
        /// Conditional deletes are supported, and multiple resources can be deleted in a single interaction.
        /// (system: http://hl7.org/fhir/conditional-delete-status)
        /// </summary>
        [EnumLiteral("multiple", "http://hl7.org/fhir/conditional-delete-status"), Description("Multiple Deletes Supported")]
        Multiple,
    }

    /// <summary>
    /// Data types allowed to be used for search parameters.
    /// (url: http://hl7.org/fhir/ValueSet/search-param-type)
    /// </summary>
    [FhirEnumeration("SearchParamType")]
    public enum SearchParamType
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
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("special", "http://hl7.org/fhir/search-param-type"), Description("Special")]
        Special,
    }

    /// <summary>
    /// Whether the application produces or consumes documents.
    /// (url: http://hl7.org/fhir/ValueSet/document-mode)
    /// </summary>
    [FhirEnumeration("DocumentMode")]
    public enum DocumentMode
    {
        /// <summary>
        /// The application produces documents of the specified type.
        /// (system: http://hl7.org/fhir/document-mode)
        /// </summary>
        [EnumLiteral("producer", "http://hl7.org/fhir/document-mode"), Description("Producer")]
        Producer,
        /// <summary>
        /// The application consumes documents of the specified type.
        /// (system: http://hl7.org/fhir/document-mode)
        /// </summary>
        [EnumLiteral("consumer", "http://hl7.org/fhir/document-mode"), Description("Consumer")]
        Consumer,
    }

    /// <summary>
    /// Indicates the degree of precision of the data element definition.
    /// (url: http://hl7.org/fhir/ValueSet/dataelement-stringency)
    /// </summary>
    [FhirEnumeration("DataElementStringency")]
    public enum DataElementStringency
    {
        /// <summary>
        /// The data element is sufficiently well-constrained that multiple pieces of data captured according to the constraints of the data element will be comparable (though in some cases, a degree of automated conversion/normalization may be required).
        /// (system: http://hl7.org/fhir/dataelement-stringency)
        /// </summary>
        [EnumLiteral("comparable", "http://hl7.org/fhir/dataelement-stringency"), Description("Comparable")]
        Comparable,
        /// <summary>
        /// The data element is fully specified down to a single value set, single unit of measure, single data type, etc.  Multiple pieces of data associated with this data element are fully comparable.
        /// (system: http://hl7.org/fhir/dataelement-stringency)
        /// </summary>
        [EnumLiteral("fully-specified", "http://hl7.org/fhir/dataelement-stringency"), Description("Fully Specified")]
        FullySpecified,
        /// <summary>
        /// The data element allows multiple units of measure having equivalent meaning; e.g. "cc" (cubic centimeter) and "mL" (milliliter).
        /// (system: http://hl7.org/fhir/dataelement-stringency)
        /// </summary>
        [EnumLiteral("equivalent", "http://hl7.org/fhir/dataelement-stringency"), Description("Equivalent")]
        Equivalent,
        /// <summary>
        /// The data element allows multiple units of measure that are convertable between each other (e.g. inches and centimeters) and/or allows data to be captured in multiple value sets for which a known mapping exists allowing conversion of meaning.
        /// (system: http://hl7.org/fhir/dataelement-stringency)
        /// </summary>
        [EnumLiteral("convertable", "http://hl7.org/fhir/dataelement-stringency"), Description("Convertable")]
        Convertable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/dataelement-stringency)
        /// </summary>
        [EnumLiteral("scaleable", "http://hl7.org/fhir/dataelement-stringency"), Description("Scaleable")]
        Scaleable,
        /// <summary>
        /// The data element is unconstrained in units, choice of data types and/or choice of vocabulary such that automated comparison of data captured using the data element is not possible.
        /// (system: http://hl7.org/fhir/dataelement-stringency)
        /// </summary>
        [EnumLiteral("flexible", "http://hl7.org/fhir/dataelement-stringency"), Description("Flexible")]
        Flexible,
    }

    /// <summary>
    /// Indicates the potential degree of impact of the identified issue on the patient.
    /// (url: http://hl7.org/fhir/ValueSet/detectedissue-severity)
    /// </summary>
    [FhirEnumeration("DetectedIssueSeverity")]
    public enum DetectedIssueSeverity
    {
        /// <summary>
        /// Indicates the issue may be life-threatening or has the potential to cause permanent injury.
        /// (system: http://hl7.org/fhir/detectedissue-severity)
        /// </summary>
        [EnumLiteral("high", "http://hl7.org/fhir/detectedissue-severity"), Description("High")]
        High,
        /// <summary>
        /// Indicates the issue may result in noticeable adverse consequences but is unlikely to be life-threatening or cause permanent injury.
        /// (system: http://hl7.org/fhir/detectedissue-severity)
        /// </summary>
        [EnumLiteral("moderate", "http://hl7.org/fhir/detectedissue-severity"), Description("Moderate")]
        Moderate,
        /// <summary>
        /// Indicates the issue may result in some adverse consequences but is unlikely to substantially affect the situation of the subject.
        /// (system: http://hl7.org/fhir/detectedissue-severity)
        /// </summary>
        [EnumLiteral("low", "http://hl7.org/fhir/detectedissue-severity"), Description("Low")]
        Low,
    }

    /// <summary>
    /// Describes the typical color of representation.
    /// (url: http://hl7.org/fhir/ValueSet/metric-color)
    /// </summary>
    [FhirEnumeration("DeviceMetricColor")]
    public enum DeviceMetricColor
    {
        /// <summary>
        /// Color for representation - black.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("black", "http://hl7.org/fhir/metric-color"), Description("Color Black")]
        Black,
        /// <summary>
        /// Color for representation - red.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("red", "http://hl7.org/fhir/metric-color"), Description("Color Red")]
        Red,
        /// <summary>
        /// Color for representation - green.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("green", "http://hl7.org/fhir/metric-color"), Description("Color Green")]
        Green,
        /// <summary>
        /// Color for representation - yellow.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("yellow", "http://hl7.org/fhir/metric-color"), Description("Color Yellow")]
        Yellow,
        /// <summary>
        /// Color for representation - blue.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("blue", "http://hl7.org/fhir/metric-color"), Description("Color Blue")]
        Blue,
        /// <summary>
        /// Color for representation - magenta.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("magenta", "http://hl7.org/fhir/metric-color"), Description("Color Magenta")]
        Magenta,
        /// <summary>
        /// Color for representation - cyan.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("cyan", "http://hl7.org/fhir/metric-color"), Description("Color Cyan")]
        Cyan,
        /// <summary>
        /// Color for representation - white.
        /// (system: http://hl7.org/fhir/metric-color)
        /// </summary>
        [EnumLiteral("white", "http://hl7.org/fhir/metric-color"), Description("Color White")]
        White,
    }

    /// <summary>
    /// Describes the category of the metric.
    /// (url: http://hl7.org/fhir/ValueSet/metric-category)
    /// </summary>
    [FhirEnumeration("DeviceMetricCategory")]
    public enum DeviceMetricCategory
    {
        /// <summary>
        /// DeviceObservations generated for this DeviceMetric are measured.
        /// (system: http://hl7.org/fhir/metric-category)
        /// </summary>
        [EnumLiteral("measurement", "http://hl7.org/fhir/metric-category"), Description("Measurement")]
        Measurement,
        /// <summary>
        /// DeviceObservations generated for this DeviceMetric is a setting that will influence the behavior of the Device.
        /// (system: http://hl7.org/fhir/metric-category)
        /// </summary>
        [EnumLiteral("setting", "http://hl7.org/fhir/metric-category"), Description("Setting")]
        Setting,
        /// <summary>
        /// DeviceObservations generated for this DeviceMetric are calculated.
        /// (system: http://hl7.org/fhir/metric-category)
        /// </summary>
        [EnumLiteral("calculation", "http://hl7.org/fhir/metric-category"), Description("Calculation")]
        Calculation,
        /// <summary>
        /// The category of this DeviceMetric is unspecified.
        /// (system: http://hl7.org/fhir/metric-category)
        /// </summary>
        [EnumLiteral("unspecified", "http://hl7.org/fhir/metric-category"), Description("Unspecified")]
        Unspecified,
    }

    /// <summary>
    /// Describes the type of a metric calibration.
    /// (url: http://hl7.org/fhir/ValueSet/metric-calibration-type)
    /// </summary>
    [FhirEnumeration("DeviceMetricCalibrationType")]
    public enum DeviceMetricCalibrationType
    {
        /// <summary>
        /// TODO
        /// (system: http://hl7.org/fhir/metric-calibration-type)
        /// </summary>
        [EnumLiteral("unspecified", "http://hl7.org/fhir/metric-calibration-type"), Description("Unspecified")]
        Unspecified,
        /// <summary>
        /// TODO
        /// (system: http://hl7.org/fhir/metric-calibration-type)
        /// </summary>
        [EnumLiteral("offset", "http://hl7.org/fhir/metric-calibration-type"), Description("Offset")]
        Offset,
        /// <summary>
        /// TODO
        /// (system: http://hl7.org/fhir/metric-calibration-type)
        /// </summary>
        [EnumLiteral("gain", "http://hl7.org/fhir/metric-calibration-type"), Description("Gain")]
        Gain,
        /// <summary>
        /// TODO
        /// (system: http://hl7.org/fhir/metric-calibration-type)
        /// </summary>
        [EnumLiteral("two-point", "http://hl7.org/fhir/metric-calibration-type"), Description("Two Point")]
        TwoPoint,
    }

    /// <summary>
    /// Describes the state of a metric calibration.
    /// (url: http://hl7.org/fhir/ValueSet/metric-calibration-state)
    /// </summary>
    [FhirEnumeration("DeviceMetricCalibrationState")]
    public enum DeviceMetricCalibrationState
    {
        /// <summary>
        /// The metric has not been calibrated.
        /// (system: http://hl7.org/fhir/metric-calibration-state)
        /// </summary>
        [EnumLiteral("not-calibrated", "http://hl7.org/fhir/metric-calibration-state"), Description("Not Calibrated")]
        NotCalibrated,
        /// <summary>
        /// The metric needs to be calibrated.
        /// (system: http://hl7.org/fhir/metric-calibration-state)
        /// </summary>
        [EnumLiteral("calibration-required", "http://hl7.org/fhir/metric-calibration-state"), Description("Calibration Required")]
        CalibrationRequired,
        /// <summary>
        /// The metric has been calibrated.
        /// (system: http://hl7.org/fhir/metric-calibration-state)
        /// </summary>
        [EnumLiteral("calibrated", "http://hl7.org/fhir/metric-calibration-state"), Description("Calibrated")]
        Calibrated,
        /// <summary>
        /// The state of calibration of this metric is unspecified.
        /// (system: http://hl7.org/fhir/metric-calibration-state)
        /// </summary>
        [EnumLiteral("unspecified", "http://hl7.org/fhir/metric-calibration-state"), Description("Unspecified")]
        Unspecified,
    }

    /// <summary>
    /// The status of the document reference.
    /// (url: http://hl7.org/fhir/ValueSet/document-reference-status)
    /// </summary>
    [FhirEnumeration("DocumentReferenceStatus")]
    public enum DocumentReferenceStatus
    {
        /// <summary>
        /// This is the current reference for this document.
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("current", "http://hl7.org/fhir/document-reference-status"), Description("Current")]
        Current,
        /// <summary>
        /// This reference has been superseded by another reference.
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("superseded", "http://hl7.org/fhir/document-reference-status"), Description("Superseded")]
        Superseded,
        /// <summary>
        /// This reference was created in error.
        /// (system: http://hl7.org/fhir/document-reference-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/document-reference-status"), Description("Entered in Error")]
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
        /// This document logically replaces or supersedes the target document.
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("replaces", "http://hl7.org/fhir/document-relationship-type"), Description("Replaces")]
        Replaces,
        /// <summary>
        /// This document was generated by transforming the target document (e.g. format or language conversion).
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("transforms", "http://hl7.org/fhir/document-relationship-type"), Description("Transforms")]
        Transforms,
        /// <summary>
        /// This document is a signature of the target document.
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("signs", "http://hl7.org/fhir/document-relationship-type"), Description("Signs")]
        Signs,
        /// <summary>
        /// This document adds additional information to the target document.
        /// (system: http://hl7.org/fhir/document-relationship-type)
        /// </summary>
        [EnumLiteral("appends", "http://hl7.org/fhir/document-relationship-type"), Description("Appends")]
        Appends,
    }

    /// <summary>
    /// The status of the location.
    /// (url: http://hl7.org/fhir/ValueSet/encounter-location-status)
    /// </summary>
    [FhirEnumeration("EncounterLocationStatus")]
    public enum EncounterLocationStatus
    {
        /// <summary>
        /// The patient is planned to be moved to this location at some point in the future.
        /// (system: http://hl7.org/fhir/encounter-location-status)
        /// </summary>
        [EnumLiteral("planned", "http://hl7.org/fhir/encounter-location-status"), Description("Planned")]
        Planned,
        /// <summary>
        /// The patient is currently at this location, or was between the period specified.<br/>
        /// <br/>
        /// A system may update these records when the patient leaves the location to either reserved, or completed
        /// (system: http://hl7.org/fhir/encounter-location-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/encounter-location-status"), Description("Active")]
        Active,
        /// <summary>
        /// This location is held empty for this patient.
        /// (system: http://hl7.org/fhir/encounter-location-status)
        /// </summary>
        [EnumLiteral("reserved", "http://hl7.org/fhir/encounter-location-status"), Description("Reserved")]
        Reserved,
        /// <summary>
        /// The patient was at this location during the period specified.<br/>
        /// <br/>
        /// Not to be used when the patient is currently at the location
        /// (system: http://hl7.org/fhir/encounter-location-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/encounter-location-status"), Description("Completed")]
        Completed,
    }

    /// <summary>
    /// A code that identifies the status of the family history record.
    /// (url: http://hl7.org/fhir/ValueSet/history-status)
    /// </summary>
    [FhirEnumeration("FamilyHistoryStatus")]
    public enum FamilyHistoryStatus
    {
        /// <summary>
        /// Some health information is known and captured, but not complete - see notes for details.
        /// (system: http://hl7.org/fhir/history-status)
        /// </summary>
        [EnumLiteral("partial", "http://hl7.org/fhir/history-status"), Description("Partial")]
        Partial,
        /// <summary>
        /// All relevant health information is known and captured.
        /// (system: http://hl7.org/fhir/history-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/history-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// This instance should not have been part of this patient's medical record.
        /// (system: http://hl7.org/fhir/history-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/history-status"), Description("Entered in error")]
        EnteredInError,
        /// <summary>
        /// Health information for this individual is unavailable/unknown.
        /// (system: http://hl7.org/fhir/history-status)
        /// </summary>
        [EnumLiteral("health-unknown", "http://hl7.org/fhir/history-status"), Description("Health unknown")]
        HealthUnknown,
    }

    /// <summary>
    /// The gender of a person used for administrative purposes.
    /// (url: http://hl7.org/fhir/ValueSet/administrative-gender)
    /// </summary>
    [FhirEnumeration("AdministrativeGender")]
    public enum AdministrativeGender
    {
        /// <summary>
        /// Male
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("male", "http://hl7.org/fhir/administrative-gender"), Description("Male")]
        Male,
        /// <summary>
        /// Female
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("female", "http://hl7.org/fhir/administrative-gender"), Description("Female")]
        Female,
        /// <summary>
        /// Other
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/administrative-gender"), Description("Other")]
        Other,
        /// <summary>
        /// Unknown
        /// (system: http://hl7.org/fhir/administrative-gender)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/administrative-gender"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// Indicates whether this flag is active and needs to be displayed to a user, or whether it is no longer needed or entered in error.
    /// (url: http://hl7.org/fhir/ValueSet/flag-status)
    /// </summary>
    [FhirEnumeration("FlagStatus")]
    public enum FlagStatus
    {
        /// <summary>
        /// A current flag that should be displayed to a user. A system may use the category to determine which roles should view the flag.
        /// (system: http://hl7.org/fhir/flag-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/flag-status"), Description("Active")]
        Active,
        /// <summary>
        /// The flag does not need to be displayed any more.
        /// (system: http://hl7.org/fhir/flag-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/flag-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// The flag was added in error, and should no longer be displayed.
        /// (system: http://hl7.org/fhir/flag-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/flag-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// Types of resources that are part of group
    /// (url: http://hl7.org/fhir/ValueSet/group-type)
    /// </summary>
    [FhirEnumeration("GroupType")]
    public enum GroupType
    {
        /// <summary>
        /// Group contains "person" Patient resources
        /// (system: http://hl7.org/fhir/group-type)
        /// </summary>
        [EnumLiteral("person", "http://hl7.org/fhir/group-type"), Description("Person")]
        Person,
        /// <summary>
        /// Group contains "animal" Patient resources
        /// (system: http://hl7.org/fhir/group-type)
        /// </summary>
        [EnumLiteral("animal", "http://hl7.org/fhir/group-type"), Description("Animal")]
        Animal,
        /// <summary>
        /// Group contains healthcare practitioner resources
        /// (system: http://hl7.org/fhir/group-type)
        /// </summary>
        [EnumLiteral("practitioner", "http://hl7.org/fhir/group-type"), Description("Practitioner")]
        Practitioner,
        /// <summary>
        /// Group contains Device resources
        /// (system: http://hl7.org/fhir/group-type)
        /// </summary>
        [EnumLiteral("device", "http://hl7.org/fhir/group-type"), Description("Device")]
        Device,
        /// <summary>
        /// Group contains Medication resources
        /// (system: http://hl7.org/fhir/group-type)
        /// </summary>
        [EnumLiteral("medication", "http://hl7.org/fhir/group-type"), Description("Medication")]
        Medication,
        /// <summary>
        /// Group contains Substance resources
        /// (system: http://hl7.org/fhir/group-type)
        /// </summary>
        [EnumLiteral("substance", "http://hl7.org/fhir/group-type"), Description("Substance")]
        Substance,
    }

    /// <summary>
    /// The days of the week.
    /// (url: http://hl7.org/fhir/ValueSet/days-of-week)
    /// </summary>
    [FhirEnumeration("DaysOfWeek")]
    public enum DaysOfWeek
    {
        /// <summary>
        /// Monday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("mon", "http://hl7.org/fhir/days-of-week"), Description("Monday")]
        Mon,
        /// <summary>
        /// Tuesday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("tue", "http://hl7.org/fhir/days-of-week"), Description("Tuesday")]
        Tue,
        /// <summary>
        /// Wednesday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("wed", "http://hl7.org/fhir/days-of-week"), Description("Wednesday")]
        Wed,
        /// <summary>
        /// Thursday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("thu", "http://hl7.org/fhir/days-of-week"), Description("Thursday")]
        Thu,
        /// <summary>
        /// Friday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("fri", "http://hl7.org/fhir/days-of-week"), Description("Friday")]
        Fri,
        /// <summary>
        /// Saturday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("sat", "http://hl7.org/fhir/days-of-week"), Description("Saturday")]
        Sat,
        /// <summary>
        /// Sunday
        /// (system: http://hl7.org/fhir/days-of-week)
        /// </summary>
        [EnumLiteral("sun", "http://hl7.org/fhir/days-of-week"), Description("Sunday")]
        Sun,
    }

    /// <summary>
    /// Availability of the resource
    /// (url: http://hl7.org/fhir/ValueSet/instance-availability)
    /// </summary>
    [FhirEnumeration("InstanceAvailability")]
    public enum InstanceAvailability
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://nema.org/dicom/dicm)
        /// </summary>
        [EnumLiteral("ONLINE", "http://nema.org/dicom/dicm"), Description("Online")]
        ONLINE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://nema.org/dicom/dicm)
        /// </summary>
        [EnumLiteral("OFFLINE", "http://nema.org/dicom/dicm"), Description("Offline")]
        OFFLINE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://nema.org/dicom/dicm)
        /// </summary>
        [EnumLiteral("NEARLINE", "http://nema.org/dicom/dicm"), Description("Nearline")]
        NEARLINE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://nema.org/dicom/dicm)
        /// </summary>
        [EnumLiteral("UNAVAILABLE", "http://nema.org/dicom/dicm"), Description("Unavailable")]
        UNAVAILABLE,
    }

    /// <summary>
    /// How a dependency is represented when the guide is published.
    /// (url: http://hl7.org/fhir/ValueSet/guide-dependency-type)
    /// </summary>
    [FhirEnumeration("GuideDependencyType")]
    public enum GuideDependencyType
    {
        /// <summary>
        /// The guide is referred to by URL.
        /// (system: http://hl7.org/fhir/guide-dependency-type)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/guide-dependency-type"), Description("Reference")]
        Reference,
        /// <summary>
        /// The guide is embedded in this guide when published.
        /// (system: http://hl7.org/fhir/guide-dependency-type)
        /// </summary>
        [EnumLiteral("inclusion", "http://hl7.org/fhir/guide-dependency-type"), Description("Inclusion")]
        Inclusion,
    }

    /// <summary>
    /// The kind of an included page.
    /// (url: http://hl7.org/fhir/ValueSet/guide-page-kind)
    /// </summary>
    [FhirEnumeration("GuidePageKind")]
    public enum GuidePageKind
    {
        /// <summary>
        /// This is a page of content that is included in the implementation guide. It has no particular function.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("page", "http://hl7.org/fhir/guide-page-kind"), Description("Page")]
        Page,
        /// <summary>
        /// This is a page that represents a human readable rendering of an example.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("example", "http://hl7.org/fhir/guide-page-kind"), Description("Example")]
        Example,
        /// <summary>
        /// This is a page that represents a list of resources of one or more types.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("list", "http://hl7.org/fhir/guide-page-kind"), Description("List")]
        List,
        /// <summary>
        /// This is a page showing where an included guide is injected.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("include", "http://hl7.org/fhir/guide-page-kind"), Description("Include")]
        Include,
        /// <summary>
        /// This is a page that lists the resources of a given type, and also creates pages for all the listed types as other pages in the section.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("directory", "http://hl7.org/fhir/guide-page-kind"), Description("Directory")]
        Directory,
        /// <summary>
        /// This is a page that creates the listed resources as a dictionary.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("dictionary", "http://hl7.org/fhir/guide-page-kind"), Description("Dictionary")]
        Dictionary,
        /// <summary>
        /// This is a generated page that contains the table of contents.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("toc", "http://hl7.org/fhir/guide-page-kind"), Description("Table Of Contents")]
        Toc,
        /// <summary>
        /// This is a page that represents a presented resource. This is typically used for generated conformance resource presentations.
        /// (system: http://hl7.org/fhir/guide-page-kind)
        /// </summary>
        [EnumLiteral("resource", "http://hl7.org/fhir/guide-page-kind"), Description("Resource")]
        Resource,
    }

    /// <summary>
    /// The current state of the list
    /// (url: http://hl7.org/fhir/ValueSet/list-status)
    /// </summary>
    [FhirEnumeration("ListStatus")]
    public enum ListStatus
    {
        /// <summary>
        /// The list is considered to be an active part of the patient's record.
        /// (system: http://hl7.org/fhir/list-status)
        /// </summary>
        [EnumLiteral("current", "http://hl7.org/fhir/list-status"), Description("Current")]
        Current,
        /// <summary>
        /// The list is "old" and should no longer be considered accurate or relevant.
        /// (system: http://hl7.org/fhir/list-status)
        /// </summary>
        [EnumLiteral("retired", "http://hl7.org/fhir/list-status"), Description("Retired")]
        Retired,
        /// <summary>
        /// The list was never accurate.  It is retained for medico-legal purposes only.
        /// (system: http://hl7.org/fhir/list-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/list-status"), Description("Entered In Error")]
        EnteredInError,
    }

    /// <summary>
    /// Indicates whether the location is still in use.
    /// (url: http://hl7.org/fhir/ValueSet/location-status)
    /// </summary>
    [FhirEnumeration("LocationStatus")]
    public enum LocationStatus
    {
        /// <summary>
        /// The location is operational.
        /// (system: http://hl7.org/fhir/location-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/location-status"), Description("Active")]
        Active,
        /// <summary>
        /// The location is temporarily closed.
        /// (system: http://hl7.org/fhir/location-status)
        /// </summary>
        [EnumLiteral("suspended", "http://hl7.org/fhir/location-status"), Description("Suspended")]
        Suspended,
        /// <summary>
        /// The location is no longer used.
        /// (system: http://hl7.org/fhir/location-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/location-status"), Description("Inactive")]
        Inactive,
    }

    /// <summary>
    /// Indicates whether a resource instance represents a specific location or a class of locations.
    /// (url: http://hl7.org/fhir/ValueSet/location-mode)
    /// </summary>
    [FhirEnumeration("LocationMode")]
    public enum LocationMode
    {
        /// <summary>
        /// The Location resource represents a specific instance of a location (e.g. Operating Theatre 1A).
        /// (system: http://hl7.org/fhir/location-mode)
        /// </summary>
        [EnumLiteral("instance", "http://hl7.org/fhir/location-mode"), Description("Instance")]
        Instance,
        /// <summary>
        /// The Location represents a class of locations (e.g. Any Operating Theatre) although this class of locations could be constrained within a specific boundary (such as organization, or parent location, address etc.).
        /// (system: http://hl7.org/fhir/location-mode)
        /// </summary>
        [EnumLiteral("kind", "http://hl7.org/fhir/location-mode"), Description("Kind")]
        Kind,
    }

    /// <summary>
    /// Whether the Media is a photo, video, or audio
    /// (url: http://hl7.org/fhir/ValueSet/digital-media-type)
    /// </summary>
    [FhirEnumeration("DigitalMediaType")]
    public enum DigitalMediaType
    {
        /// <summary>
        /// The media consists of one or more unmoving images, including photographs, computer-generated graphs and charts, and scanned documents
        /// (system: http://hl7.org/fhir/digital-media-type)
        /// </summary>
        [EnumLiteral("photo", "http://hl7.org/fhir/digital-media-type"), Description("Photo")]
        Photo,
        /// <summary>
        /// The media consists of a series of frames that capture a moving image
        /// (system: http://hl7.org/fhir/digital-media-type)
        /// </summary>
        [EnumLiteral("video", "http://hl7.org/fhir/digital-media-type"), Description("Video")]
        Video,
        /// <summary>
        /// The media consists of a sound recording
        /// (system: http://hl7.org/fhir/digital-media-type)
        /// </summary>
        [EnumLiteral("audio", "http://hl7.org/fhir/digital-media-type"), Description("Audio")]
        Audio,
    }

    /// <summary>
    /// The kind of response to a message
    /// (url: http://hl7.org/fhir/ValueSet/response-code)
    /// </summary>
    [FhirEnumeration("ResponseType")]
    public enum ResponseType
    {
        /// <summary>
        /// The message was accepted and processed without error.
        /// (system: http://hl7.org/fhir/response-code)
        /// </summary>
        [EnumLiteral("ok", "http://hl7.org/fhir/response-code"), Description("OK")]
        Ok,
        /// <summary>
        /// Some internal unexpected error occurred - wait and try again. Note - this is usually used for things like database unavailable, which may be expected to resolve, though human intervention may be required.
        /// (system: http://hl7.org/fhir/response-code)
        /// </summary>
        [EnumLiteral("transient-error", "http://hl7.org/fhir/response-code"), Description("Transient Error")]
        TransientError,
        /// <summary>
        /// The message was rejected because of some content in it. There is no point in re-sending without change. The response narrative SHALL describe the issue.
        /// (system: http://hl7.org/fhir/response-code)
        /// </summary>
        [EnumLiteral("fatal-error", "http://hl7.org/fhir/response-code"), Description("Fatal Error")]
        FatalError,
    }

    /// <summary>
    /// Identifies the purpose of the naming system.
    /// (url: http://hl7.org/fhir/ValueSet/namingsystem-type)
    /// </summary>
    [FhirEnumeration("NamingSystemType")]
    public enum NamingSystemType
    {
        /// <summary>
        /// The naming system is used to define concepts and symbols to represent those concepts; e.g. UCUM, LOINC, NDC code, local lab codes, etc.
        /// (system: http://hl7.org/fhir/namingsystem-type)
        /// </summary>
        [EnumLiteral("codesystem", "http://hl7.org/fhir/namingsystem-type"), Description("Code System")]
        Codesystem,
        /// <summary>
        /// The naming system is used to manage identifiers (e.g. license numbers, order numbers, etc.).
        /// (system: http://hl7.org/fhir/namingsystem-type)
        /// </summary>
        [EnumLiteral("identifier", "http://hl7.org/fhir/namingsystem-type"), Description("Identifier")]
        Identifier,
        /// <summary>
        /// The naming system is used as the root for other identifiers and naming systems.
        /// (system: http://hl7.org/fhir/namingsystem-type)
        /// </summary>
        [EnumLiteral("root", "http://hl7.org/fhir/namingsystem-type"), Description("Root")]
        Root,
    }

    /// <summary>
    /// Identifies the style of unique identifier used to identify a namespace.
    /// (url: http://hl7.org/fhir/ValueSet/namingsystem-identifier-type)
    /// </summary>
    [FhirEnumeration("NamingSystemIdentifierType")]
    public enum NamingSystemIdentifierType
    {
        /// <summary>
        /// An ISO object identifier; e.g. 1.2.3.4.5.
        /// (system: http://hl7.org/fhir/namingsystem-identifier-type)
        /// </summary>
        [EnumLiteral("oid", "http://hl7.org/fhir/namingsystem-identifier-type"), Description("OID")]
        Oid,
        /// <summary>
        /// A universally unique identifier of the form a5afddf4-e880-459b-876e-e4591b0acc11.
        /// (system: http://hl7.org/fhir/namingsystem-identifier-type)
        /// </summary>
        [EnumLiteral("uuid", "http://hl7.org/fhir/namingsystem-identifier-type"), Description("UUID")]
        Uuid,
        /// <summary>
        /// A uniform resource identifier (ideally a URL - uniform resource locator); e.g. http://unitsofmeasure.org.
        /// (system: http://hl7.org/fhir/namingsystem-identifier-type)
        /// </summary>
        [EnumLiteral("uri", "http://hl7.org/fhir/namingsystem-identifier-type"), Description("URI")]
        Uri,
        /// <summary>
        /// Some other type of unique identifier; e.g. HL7-assigned reserved string such as LN for LOINC.
        /// (system: http://hl7.org/fhir/namingsystem-identifier-type)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/namingsystem-identifier-type"), Description("Other")]
        Other,
    }

    /// <summary>
    /// Codes specifying how two observations are related.
    /// (url: http://hl7.org/fhir/ValueSet/observation-relationshiptypes)
    /// </summary>
    [FhirEnumeration("ObservationRelationshipType")]
    public enum ObservationRelationshipType
    {
        /// <summary>
        /// This observation is a group observation (e.g. a battery, a panel of tests, a set of vital sign measurements) that includes the target as a member of the group.
        /// (system: http://hl7.org/fhir/observation-relationshiptypes)
        /// </summary>
        [EnumLiteral("has-member", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Has Member")]
        HasMember,
        /// <summary>
        /// The target resource (Observation or QuestionnaireResponse) is part of the information from which this observation value is derived. (e.g. calculated anion gap, Apgar score)  NOTE:  "derived-from" is only logical choice when referencing QuestionnaireResponse.
        /// (system: http://hl7.org/fhir/observation-relationshiptypes)
        /// </summary>
        [EnumLiteral("derived-from", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Derived From")]
        DerivedFrom,
        /// <summary>
        /// This observation follows the target observation (e.g. timed tests such as Glucose Tolerance Test).
        /// (system: http://hl7.org/fhir/observation-relationshiptypes)
        /// </summary>
        [EnumLiteral("sequel-to", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Sequel To")]
        SequelTo,
        /// <summary>
        /// This observation replaces a previous observation (i.e. a revised value). The target observation is now obsolete.
        /// (system: http://hl7.org/fhir/observation-relationshiptypes)
        /// </summary>
        [EnumLiteral("replaces", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Replaces")]
        Replaces,
        /// <summary>
        /// The value of the target observation qualifies (refines) the semantics of the source observation (e.g. a lipemia measure target from a plasma measure).
        /// (system: http://hl7.org/fhir/observation-relationshiptypes)
        /// </summary>
        [EnumLiteral("qualified-by", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Qualified By")]
        QualifiedBy,
        /// <summary>
        /// The value of the target observation interferes (degrades quality, or prevents valid observation) with the semantics of the source observation (e.g. a hemolysis measure target from a plasma potassium measure which has no value).
        /// (system: http://hl7.org/fhir/observation-relationshiptypes)
        /// </summary>
        [EnumLiteral("interfered-by", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Interfered By")]
        InterferedBy,
    }

    /// <summary>
    /// Whether an operation is a normal operation or a query.
    /// (url: http://hl7.org/fhir/ValueSet/operation-kind)
    /// </summary>
    [FhirEnumeration("OperationKind")]
    public enum OperationKind
    {
        /// <summary>
        /// This operation is invoked as an operation.
        /// (system: http://hl7.org/fhir/operation-kind)
        /// </summary>
        [EnumLiteral("operation", "http://hl7.org/fhir/operation-kind"), Description("Operation")]
        Operation,
        /// <summary>
        /// This operation is a named query, invoked using the search mechanism.
        /// (system: http://hl7.org/fhir/operation-kind)
        /// </summary>
        [EnumLiteral("query", "http://hl7.org/fhir/operation-kind"), Description("Query")]
        Query,
    }

    /// <summary>
    /// Whether an operation parameter is an input or an output parameter.
    /// (url: http://hl7.org/fhir/ValueSet/operation-parameter-use)
    /// </summary>
    [FhirEnumeration("OperationParameterUse")]
    public enum OperationParameterUse
    {
        /// <summary>
        /// This is an input parameter.
        /// (system: http://hl7.org/fhir/operation-parameter-use)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/operation-parameter-use"), Description("In")]
        In,
        /// <summary>
        /// This is an output parameter.
        /// (system: http://hl7.org/fhir/operation-parameter-use)
        /// </summary>
        [EnumLiteral("out", "http://hl7.org/fhir/operation-parameter-use"), Description("Out")]
        Out,
    }

    /// <summary>
    /// Indication of the degree of conformance expectations associated with a binding.
    /// (url: http://hl7.org/fhir/ValueSet/binding-strength)
    /// </summary>
    [FhirEnumeration("BindingStrength")]
    public enum BindingStrength
    {
        /// <summary>
        /// To be conformant, instances of this element SHALL include a code from the specified value set.
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("required", "http://hl7.org/fhir/binding-strength"), Description("Required")]
        Required,
        /// <summary>
        /// To be conformant, instances of this element SHALL include a code from the specified value set if any of the codes within the value set can apply to the concept being communicated.  If the valueset does not cover the concept (based on human review), alternate codings (or, data type allowing, text) may be included instead.
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("extensible", "http://hl7.org/fhir/binding-strength"), Description("Extensible")]
        Extensible,
        /// <summary>
        /// Instances are encouraged to draw from the specified codes for interoperability purposes but are not required to do so to be considered conformant.
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("preferred", "http://hl7.org/fhir/binding-strength"), Description("Preferred")]
        Preferred,
        /// <summary>
        /// Instances are not expected or even encouraged to draw from the specified value set.  The value set merely provides examples of the types of concepts intended to be included.
        /// (system: http://hl7.org/fhir/binding-strength)
        /// </summary>
        [EnumLiteral("example", "http://hl7.org/fhir/binding-strength"), Description("Example")]
        Example,
    }

    /// <summary>
    /// How the issue affects the success of the action.
    /// (url: http://hl7.org/fhir/ValueSet/issue-severity)
    /// </summary>
    [FhirEnumeration("IssueSeverity")]
    public enum IssueSeverity
    {
        /// <summary>
        /// The issue caused the action to fail, and no further checking could be performed.
        /// (system: http://hl7.org/fhir/issue-severity)
        /// </summary>
        [EnumLiteral("fatal", "http://hl7.org/fhir/issue-severity"), Description("Fatal")]
        Fatal,
        /// <summary>
        /// The issue is sufficiently important to cause the action to fail.
        /// (system: http://hl7.org/fhir/issue-severity)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/issue-severity"), Description("Error")]
        Error,
        /// <summary>
        /// The issue is not important enough to cause the action to fail, but may cause it to be performed suboptimally or in a way that is not as desired.
        /// (system: http://hl7.org/fhir/issue-severity)
        /// </summary>
        [EnumLiteral("warning", "http://hl7.org/fhir/issue-severity"), Description("Warning")]
        Warning,
        /// <summary>
        /// The issue has no relation to the degree of success of the action.
        /// (system: http://hl7.org/fhir/issue-severity)
        /// </summary>
        [EnumLiteral("information", "http://hl7.org/fhir/issue-severity"), Description("Information")]
        Information,
    }

    /// <summary>
    /// The level of confidence that this link represents the same actual person, based on NIST Authentication Levels.
    /// (url: http://hl7.org/fhir/ValueSet/identity-assuranceLevel)
    /// </summary>
    [FhirEnumeration("IdentityAssuranceLevel")]
    public enum IdentityAssuranceLevel
    {
        /// <summary>
        /// Little or no confidence in the asserted identity's accuracy.
        /// (system: http://hl7.org/fhir/identity-assuranceLevel)
        /// </summary>
        [EnumLiteral("level1", "http://hl7.org/fhir/identity-assuranceLevel"), Description("Level 1")]
        Level1,
        /// <summary>
        /// Some confidence in the asserted identity's accuracy.
        /// (system: http://hl7.org/fhir/identity-assuranceLevel)
        /// </summary>
        [EnumLiteral("level2", "http://hl7.org/fhir/identity-assuranceLevel"), Description("Level 2")]
        Level2,
        /// <summary>
        /// High confidence in the asserted identity's accuracy.
        /// (system: http://hl7.org/fhir/identity-assuranceLevel)
        /// </summary>
        [EnumLiteral("level3", "http://hl7.org/fhir/identity-assuranceLevel"), Description("Level 3")]
        Level3,
        /// <summary>
        /// Very high confidence in the asserted identity's accuracy.
        /// (system: http://hl7.org/fhir/identity-assuranceLevel)
        /// </summary>
        [EnumLiteral("level4", "http://hl7.org/fhir/identity-assuranceLevel"), Description("Level 4")]
        Level4,
    }

    /// <summary>
    /// List of allowable action which this resource can request.
    /// (url: http://hl7.org/fhir/ValueSet/actionlist)
    /// </summary>
    [FhirEnumeration("ActionList")]
    public enum ActionList
    {
        /// <summary>
        /// Cancel, reverse or nullify the target resource.
        /// (system: http://hl7.org/fhir/actionlist)
        /// </summary>
        [EnumLiteral("cancel", "http://hl7.org/fhir/actionlist"), Description("Cancel, Reverse or Nullify")]
        Cancel,
        /// <summary>
        /// Check for previously un-read/ not-retrieved resources.
        /// (system: http://hl7.org/fhir/actionlist)
        /// </summary>
        [EnumLiteral("poll", "http://hl7.org/fhir/actionlist"), Description("Poll")]
        Poll,
        /// <summary>
        /// Re-process the target resource.
        /// (system: http://hl7.org/fhir/actionlist)
        /// </summary>
        [EnumLiteral("reprocess", "http://hl7.org/fhir/actionlist"), Description("Re-Process")]
        Reprocess,
        /// <summary>
        /// Retrieve the processing status of the target resource.
        /// (system: http://hl7.org/fhir/actionlist)
        /// </summary>
        [EnumLiteral("status", "http://hl7.org/fhir/actionlist"), Description("Status Check")]
        Status,
    }

    /// <summary>
    /// How a search parameter relates to the set of elements returned by evaluating its xpath query.
    /// (url: http://hl7.org/fhir/ValueSet/search-xpath-usage)
    /// </summary>
    [FhirEnumeration("XPathUsageType")]
    public enum XPathUsageType
    {
        /// <summary>
        /// The search parameter is derived directly from the selected nodes based on the type definitions.
        /// (system: http://hl7.org/fhir/search-xpath-usage)
        /// </summary>
        [EnumLiteral("normal", "http://hl7.org/fhir/search-xpath-usage"), Description("Normal")]
        Normal,
        /// <summary>
        /// The search parameter is derived by a phonetic transform from the selected nodes.
        /// (system: http://hl7.org/fhir/search-xpath-usage)
        /// </summary>
        [EnumLiteral("phonetic", "http://hl7.org/fhir/search-xpath-usage"), Description("Phonetic")]
        Phonetic,
        /// <summary>
        /// The search parameter is based on a spatial transform of the selected nodes.
        /// (system: http://hl7.org/fhir/search-xpath-usage)
        /// </summary>
        [EnumLiteral("nearby", "http://hl7.org/fhir/search-xpath-usage"), Description("Nearby")]
        Nearby,
        /// <summary>
        /// The search parameter is based on a spatial transform of the selected nodes, using physical distance from the middle.
        /// (system: http://hl7.org/fhir/search-xpath-usage)
        /// </summary>
        [EnumLiteral("distance", "http://hl7.org/fhir/search-xpath-usage"), Description("Distance")]
        Distance,
        /// <summary>
        /// The interpretation of the xpath statement is unknown (and can't be automated).
        /// (system: http://hl7.org/fhir/search-xpath-usage)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/search-xpath-usage"), Description("Other")]
        Other,
    }

    /// <summary>
    /// Codes providing the status/availability of a specimen.
    /// (url: http://hl7.org/fhir/ValueSet/specimen-status)
    /// </summary>
    [FhirEnumeration("SpecimenStatus")]
    public enum SpecimenStatus
    {
        /// <summary>
        /// The physical specimen is present and in good condition.
        /// (system: http://hl7.org/fhir/specimen-status)
        /// </summary>
        [EnumLiteral("available", "http://hl7.org/fhir/specimen-status"), Description("Available")]
        Available,
        /// <summary>
        /// There is no physical specimen because it is either lost, destroyed or consumed.
        /// (system: http://hl7.org/fhir/specimen-status)
        /// </summary>
        [EnumLiteral("unavailable", "http://hl7.org/fhir/specimen-status"), Description("Unavailable")]
        Unavailable,
        /// <summary>
        /// The specimen cannot be used because of a quality issue such as a broken container, contamination, or too old.
        /// (system: http://hl7.org/fhir/specimen-status)
        /// </summary>
        [EnumLiteral("unsatisfactory", "http://hl7.org/fhir/specimen-status"), Description("Unsatisfactory")]
        Unsatisfactory,
        /// <summary>
        /// The specimen was entered in error and therefore nullified.
        /// (system: http://hl7.org/fhir/specimen-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/specimen-status"), Description("Entered-in-error")]
        EnteredInError,
    }

    /// <summary>
    /// The status of a subscription.
    /// (url: http://hl7.org/fhir/ValueSet/subscription-status)
    /// </summary>
    [FhirEnumeration("SubscriptionStatus")]
    public enum SubscriptionStatus
    {
        /// <summary>
        /// The client has requested the subscription, and the server has not yet set it up.
        /// (system: http://hl7.org/fhir/subscription-status)
        /// </summary>
        [EnumLiteral("requested", "http://hl7.org/fhir/subscription-status"), Description("Requested")]
        Requested,
        /// <summary>
        /// The subscription is active.
        /// (system: http://hl7.org/fhir/subscription-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/subscription-status"), Description("Active")]
        Active,
        /// <summary>
        /// The server has an error executing the notification.
        /// (system: http://hl7.org/fhir/subscription-status)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/subscription-status"), Description("Error")]
        Error,
        /// <summary>
        /// Too many errors have occurred or the subscription has expired.
        /// (system: http://hl7.org/fhir/subscription-status)
        /// </summary>
        [EnumLiteral("off", "http://hl7.org/fhir/subscription-status"), Description("Off")]
        Off,
    }

    /// <summary>
    /// The type of method used to execute a subscription.
    /// (url: http://hl7.org/fhir/ValueSet/subscription-channel-type)
    /// </summary>
    [FhirEnumeration("SubscriptionChannelType")]
    public enum SubscriptionChannelType
    {
        /// <summary>
        /// The channel is executed by making a post to the URI. If a payload is included, the URL is interpreted as the service base, and an update (PUT) is made.
        /// (system: http://hl7.org/fhir/subscription-channel-type)
        /// </summary>
        [EnumLiteral("rest-hook", "http://hl7.org/fhir/subscription-channel-type"), Description("Rest Hook")]
        RestHook,
        /// <summary>
        /// The channel is executed by sending a packet across a web socket connection maintained by the client. The URL identifies the websocket, and the client binds to this URL.
        /// (system: http://hl7.org/fhir/subscription-channel-type)
        /// </summary>
        [EnumLiteral("websocket", "http://hl7.org/fhir/subscription-channel-type"), Description("Websocket")]
        Websocket,
        /// <summary>
        /// The channel is executed by sending an email to the email addressed in the URI (which must be a mailto:).
        /// (system: http://hl7.org/fhir/subscription-channel-type)
        /// </summary>
        [EnumLiteral("email", "http://hl7.org/fhir/subscription-channel-type"), Description("Email")]
        Email,
        /// <summary>
        /// The channel is executed by sending an SMS message to the phone number identified in the URL (tel:).
        /// (system: http://hl7.org/fhir/subscription-channel-type)
        /// </summary>
        [EnumLiteral("sms", "http://hl7.org/fhir/subscription-channel-type"), Description("SMS")]
        Sms,
        /// <summary>
        /// The channel is executed by sending a message (e.g. a Bundle with a MessageHeader resource etc.) to the application identified in the URI.
        /// (system: http://hl7.org/fhir/subscription-channel-type)
        /// </summary>
        [EnumLiteral("message", "http://hl7.org/fhir/subscription-channel-type"), Description("Message")]
        Message,
    }

    /// <summary>
    /// The type of direction to use for assertion.<br/>
    /// <br/>
    /// The direction to use for assertions.
    /// (url: http://hl7.org/fhir/ValueSet/assert-direction-codes)
    /// </summary>
    [FhirEnumeration("AssertionDirectionType")]
    public enum AssertionDirectionType
    {
        /// <summary>
        /// The assertion is evaluated on the response. This is the default value.
        /// (system: http://hl7.org/fhir/assert-direction-codes)
        /// </summary>
        [EnumLiteral("response", "http://hl7.org/fhir/assert-direction-codes"), Description("response")]
        Response,
        /// <summary>
        /// The assertion is evaluated on the request.
        /// (system: http://hl7.org/fhir/assert-direction-codes)
        /// </summary>
        [EnumLiteral("request", "http://hl7.org/fhir/assert-direction-codes"), Description("request")]
        Request,
    }

    /// <summary>
    /// The type of response code to use for assertion.<br/>
    /// <br/>
    /// The response code to expect in the response.
    /// (url: http://hl7.org/fhir/ValueSet/assert-response-code-types)
    /// </summary>
    [FhirEnumeration("AssertionResponseTypes")]
    public enum AssertionResponseTypes
    {
        /// <summary>
        /// Response code is 200.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("okay", "http://hl7.org/fhir/assert-response-code-types"), Description("okay")]
        Okay,
        /// <summary>
        /// Response code is 201.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("created", "http://hl7.org/fhir/assert-response-code-types"), Description("created")]
        Created,
        /// <summary>
        /// Response code is 204.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("noContent", "http://hl7.org/fhir/assert-response-code-types"), Description("noContent")]
        NoContent,
        /// <summary>
        /// Response code is 304.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("notModified", "http://hl7.org/fhir/assert-response-code-types"), Description("notModified")]
        NotModified,
        /// <summary>
        /// Response code is 400.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("bad", "http://hl7.org/fhir/assert-response-code-types"), Description("bad")]
        Bad,
        /// <summary>
        /// Response code is 403.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("forbidden", "http://hl7.org/fhir/assert-response-code-types"), Description("forbidden")]
        Forbidden,
        /// <summary>
        /// Response code is 404.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("notFound", "http://hl7.org/fhir/assert-response-code-types"), Description("notFound")]
        NotFound,
        /// <summary>
        /// Response code is 405.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("methodNotAllowed", "http://hl7.org/fhir/assert-response-code-types"), Description("methodNotAllowed")]
        MethodNotAllowed,
        /// <summary>
        /// Response code is 409.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("conflict", "http://hl7.org/fhir/assert-response-code-types"), Description("conflict")]
        Conflict,
        /// <summary>
        /// Response code is 410.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("gone", "http://hl7.org/fhir/assert-response-code-types"), Description("gone")]
        Gone,
        /// <summary>
        /// Response code is 412.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("preconditionFailed", "http://hl7.org/fhir/assert-response-code-types"), Description("preconditionFailed")]
        PreconditionFailed,
        /// <summary>
        /// Response code is 422.
        /// (system: http://hl7.org/fhir/assert-response-code-types)
        /// </summary>
        [EnumLiteral("unprocessable", "http://hl7.org/fhir/assert-response-code-types"), Description("unprocessable")]
        Unprocessable,
    }

    /// <summary>
    /// A coded concept listing the eye codes.
    /// (url: http://hl7.org/fhir/ValueSet/vision-eye-codes)
    /// </summary>
    [FhirEnumeration("VisionEyes")]
    public enum VisionEyes
    {
        /// <summary>
        /// Right Eye
        /// (system: http://hl7.org/fhir/vision-eye-codes)
        /// </summary>
        [EnumLiteral("right", "http://hl7.org/fhir/vision-eye-codes"), Description("Right Eye")]
        Right,
        /// <summary>
        /// Left Eye
        /// (system: http://hl7.org/fhir/vision-eye-codes)
        /// </summary>
        [EnumLiteral("left", "http://hl7.org/fhir/vision-eye-codes"), Description("Left Eye")]
        Left,
    }

    /// <summary>
    /// A coded concept listing the base codes.
    /// (url: http://hl7.org/fhir/ValueSet/vision-base-codes)
    /// </summary>
    [FhirEnumeration("VisionBase")]
    public enum VisionBase
    {
        /// <summary>
        /// top
        /// (system: http://hl7.org/fhir/vision-base-codes)
        /// </summary>
        [EnumLiteral("up", "http://hl7.org/fhir/vision-base-codes"), Description("Up")]
        Up,
        /// <summary>
        /// bottom
        /// (system: http://hl7.org/fhir/vision-base-codes)
        /// </summary>
        [EnumLiteral("down", "http://hl7.org/fhir/vision-base-codes"), Description("Down")]
        Down,
        /// <summary>
        /// inner edge
        /// (system: http://hl7.org/fhir/vision-base-codes)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/vision-base-codes"), Description("In")]
        In,
        /// <summary>
        /// outer edge
        /// (system: http://hl7.org/fhir/vision-base-codes)
        /// </summary>
        [EnumLiteral("out", "http://hl7.org/fhir/vision-base-codes"), Description("Out")]
        Out,
    }

    /// <summary>
    /// How the Quantity should be understood and represented.
    /// (url: http://hl7.org/fhir/ValueSet/quantity-comparator)
    /// </summary>
    [FhirEnumeration("QuantityComparator")]
    public enum QuantityComparator
    {
        /// <summary>
        /// The actual value is less than the given value.
        /// (system: http://hl7.org/fhir/quantity-comparator)
        /// </summary>
        [EnumLiteral("<", "http://hl7.org/fhir/quantity-comparator"), Description("Less than")]
        LessThan,
        /// <summary>
        /// The actual value is less than or equal to the given value.
        /// (system: http://hl7.org/fhir/quantity-comparator)
        /// </summary>
        [EnumLiteral("<=", "http://hl7.org/fhir/quantity-comparator"), Description("Less or Equal to")]
        LessOrEqual,
        /// <summary>
        /// The actual value is greater than or equal to the given value.
        /// (system: http://hl7.org/fhir/quantity-comparator)
        /// </summary>
        [EnumLiteral(">=", "http://hl7.org/fhir/quantity-comparator"), Description("Greater or Equal to")]
        GreaterOrEqual,
        /// <summary>
        /// The actual value is greater than the given value.
        /// (system: http://hl7.org/fhir/quantity-comparator)
        /// </summary>
        [EnumLiteral(">", "http://hl7.org/fhir/quantity-comparator"), Description("Greater than")]
        GreaterThan,
    }

    /// <summary>
    /// The status of a resource narrative
    /// (url: http://hl7.org/fhir/ValueSet/narrative-status)
    /// </summary>
    [FhirEnumeration("NarrativeStatus")]
    public enum NarrativeStatus
    {
        /// <summary>
        /// The contents of the narrative are entirely generated from the structured data in the content.
        /// (system: http://hl7.org/fhir/narrative-status)
        /// </summary>
        [EnumLiteral("generated", "http://hl7.org/fhir/narrative-status"), Description("Generated")]
        Generated,
        /// <summary>
        /// The contents of the narrative are entirely generated from the structured data in the content and some of the content is generated from extensions
        /// (system: http://hl7.org/fhir/narrative-status)
        /// </summary>
        [EnumLiteral("extensions", "http://hl7.org/fhir/narrative-status"), Description("Extensions")]
        Extensions,
        /// <summary>
        /// The contents of the narrative contain additional information not found in the structured data
        /// (system: http://hl7.org/fhir/narrative-status)
        /// </summary>
        [EnumLiteral("additional", "http://hl7.org/fhir/narrative-status"), Description("Additional")]
        Additional,
        /// <summary>
        /// The contents of the narrative are some equivalent of "No human-readable text provided in this case"
        /// (system: http://hl7.org/fhir/narrative-status)
        /// </summary>
        [EnumLiteral("empty", "http://hl7.org/fhir/narrative-status"), Description("Empty")]
        Empty,
    }

    /// <summary>
    /// The use of a human name
    /// (url: http://hl7.org/fhir/ValueSet/name-use)
    /// </summary>
    [FhirEnumeration("NameUse")]
    public enum NameUse
    {
        /// <summary>
        /// Known as/conventional/the one you normally use
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("usual", "http://hl7.org/fhir/name-use"), Description("Usual")]
        Usual,
        /// <summary>
        /// The formal name as registered in an official (government) registry, but which name might not be commonly used. May be called "legal name".
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("official", "http://hl7.org/fhir/name-use"), Description("Official")]
        Official,
        /// <summary>
        /// A temporary name. Name.period can provide more detailed information. This may also be used for temporary names assigned at birth or in emergency situations.
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("temp", "http://hl7.org/fhir/name-use"), Description("Temp")]
        Temp,
        /// <summary>
        /// A name that is used to address the person in an informal manner, but is not part of their formal or usual name
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("nickname", "http://hl7.org/fhir/name-use"), Description("Nickname")]
        Nickname,
        /// <summary>
        /// Anonymous assigned name, alias, or pseudonym (used to protect a person's identity for privacy reasons)
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("anonymous", "http://hl7.org/fhir/name-use"), Description("Anonymous")]
        Anonymous,
        /// <summary>
        /// This name is no longer in use (or was never correct, but retained for records)
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("old", "http://hl7.org/fhir/name-use"), Description("Old")]
        Old,
        /// <summary>
        /// A name used prior to marriage. Marriage naming customs vary greatly around the world. This name use is for use by applications that collect and store "maiden" names. Though the concept of maiden name is often gender specific, the use of this term is not gender specific. The use of this term does not imply any particular history for a person's name, nor should the maiden name be determined algorithmically.
        /// (system: http://hl7.org/fhir/name-use)
        /// </summary>
        [EnumLiteral("maiden", "http://hl7.org/fhir/name-use"), Description("Maiden")]
        Maiden,
    }

    /// <summary>
    /// Use of contact point
    /// (url: http://hl7.org/fhir/ValueSet/contact-point-use)
    /// </summary>
    [FhirEnumeration("ContactPointUse")]
    public enum ContactPointUse
    {
        /// <summary>
        /// A communication contact point at a home; attempted contacts for business purposes might intrude privacy and chances are one will contact family or other household members instead of the person one wishes to call. Typically used with urgent cases, or if no other contacts are available.
        /// (system: http://hl7.org/fhir/contact-point-use)
        /// </summary>
        [EnumLiteral("home", "http://hl7.org/fhir/contact-point-use"), Description("Home")]
        Home,
        /// <summary>
        /// An office contact point. First choice for business related contacts during business hours.
        /// (system: http://hl7.org/fhir/contact-point-use)
        /// </summary>
        [EnumLiteral("work", "http://hl7.org/fhir/contact-point-use"), Description("Work")]
        Work,
        /// <summary>
        /// A temporary contact point. The period can provide more detailed information.
        /// (system: http://hl7.org/fhir/contact-point-use)
        /// </summary>
        [EnumLiteral("temp", "http://hl7.org/fhir/contact-point-use"), Description("Temp")]
        Temp,
        /// <summary>
        /// This contact point is no longer in use (or was never correct, but retained for records).
        /// (system: http://hl7.org/fhir/contact-point-use)
        /// </summary>
        [EnumLiteral("old", "http://hl7.org/fhir/contact-point-use"), Description("Old")]
        Old,
        /// <summary>
        /// A telecommunication device that moves and stays with its owner. May have characteristics of all other use codes, suitable for urgent matters, not the first choice for routine business.
        /// (system: http://hl7.org/fhir/contact-point-use)
        /// </summary>
        [EnumLiteral("mobile", "http://hl7.org/fhir/contact-point-use"), Description("Mobile")]
        Mobile,
    }

    /// <summary>
    /// The type of an address (physical / postal)<br/>
    /// <br/>
    /// The type of an address (physical / postal).
    /// (url: http://hl7.org/fhir/ValueSet/address-type)
    /// </summary>
    [FhirEnumeration("AddressType")]
    public enum AddressType
    {
        /// <summary>
        /// Mailing addresses - PO Boxes and care-of addresses.
        /// (system: http://hl7.org/fhir/address-type)
        /// </summary>
        [EnumLiteral("postal", "http://hl7.org/fhir/address-type"), Description("Postal")]
        Postal,
        /// <summary>
        /// A physical address that can be visited.
        /// (system: http://hl7.org/fhir/address-type)
        /// </summary>
        [EnumLiteral("physical", "http://hl7.org/fhir/address-type"), Description("Physical")]
        Physical,
        /// <summary>
        /// An address that is both physical and postal.
        /// (system: http://hl7.org/fhir/address-type)
        /// </summary>
        [EnumLiteral("both", "http://hl7.org/fhir/address-type"), Description("Postal & Physical")]
        Both,
    }

    /// <summary>
    /// A unit of time (units from UCUM).
    /// (url: http://hl7.org/fhir/ValueSet/units-of-time)
    /// </summary>
    [FhirEnumeration("UnitsOfTime")]
    public enum UnitsOfTime
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("s", "http://unitsofmeasure.org"), Description("second")]
        S,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("min", "http://unitsofmeasure.org"), Description("minute")]
        Min,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("h", "http://unitsofmeasure.org"), Description("hour")]
        H,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("d", "http://unitsofmeasure.org"), Description("day")]
        D,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("wk", "http://unitsofmeasure.org"), Description("week")]
        Wk,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("mo", "http://unitsofmeasure.org"), Description("month")]
        Mo,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://unitsofmeasure.org)
        /// </summary>
        [EnumLiteral("a", "http://unitsofmeasure.org"), Description("year")]
        A,
    }

    /// <summary>
    /// How slices are interpreted when evaluating an instance.
    /// (url: http://hl7.org/fhir/ValueSet/resource-slicing-rules)
    /// </summary>
    [FhirEnumeration("SlicingRules")]
    public enum SlicingRules
    {
        /// <summary>
        /// No additional content is allowed other than that described by the slices in this profile.
        /// (system: http://hl7.org/fhir/resource-slicing-rules)
        /// </summary>
        [EnumLiteral("closed", "http://hl7.org/fhir/resource-slicing-rules"), Description("Closed")]
        Closed,
        /// <summary>
        /// Additional content is allowed anywhere in the list.
        /// (system: http://hl7.org/fhir/resource-slicing-rules)
        /// </summary>
        [EnumLiteral("open", "http://hl7.org/fhir/resource-slicing-rules"), Description("Open")]
        Open,
        /// <summary>
        /// Additional content is allowed, but only at the end of the list. Note that using this requires that the slices be ordered, which makes it hard to share uses. This should only be done where absolutely required.
        /// (system: http://hl7.org/fhir/resource-slicing-rules)
        /// </summary>
        [EnumLiteral("openAtEnd", "http://hl7.org/fhir/resource-slicing-rules"), Description("Open at End")]
        OpenAtEnd,
    }

    /// <summary>
    /// How resource references can be aggregated.
    /// (url: http://hl7.org/fhir/ValueSet/resource-aggregation-mode)
    /// </summary>
    [FhirEnumeration("AggregationMode")]
    public enum AggregationMode
    {
        /// <summary>
        /// The reference is a local reference to a contained resource.
        /// (system: http://hl7.org/fhir/resource-aggregation-mode)
        /// </summary>
        [EnumLiteral("contained", "http://hl7.org/fhir/resource-aggregation-mode"), Description("Contained")]
        Contained,
        /// <summary>
        /// The reference to a resource that has to be resolved externally to the resource that includes the reference.
        /// (system: http://hl7.org/fhir/resource-aggregation-mode)
        /// </summary>
        [EnumLiteral("referenced", "http://hl7.org/fhir/resource-aggregation-mode"), Description("Referenced")]
        Referenced,
        /// <summary>
        /// The resource the reference points to will be found in the same bundle as the resource that includes the reference.
        /// (system: http://hl7.org/fhir/resource-aggregation-mode)
        /// </summary>
        [EnumLiteral("bundled", "http://hl7.org/fhir/resource-aggregation-mode"), Description("Bundled")]
        Bundled,
    }

    /// <summary>
    /// SHALL applications comply with this constraint?
    /// (url: http://hl7.org/fhir/ValueSet/constraint-severity)
    /// </summary>
    [FhirEnumeration("ConstraintSeverity")]
    public enum ConstraintSeverity
    {
        /// <summary>
        /// If the constraint is violated, the resource is not conformant.
        /// (system: http://hl7.org/fhir/constraint-severity)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/constraint-severity"), Description("Error")]
        Error,
        /// <summary>
        /// If the constraint is violated, the resource is conformant, but it is not necessarily following best practice.
        /// (system: http://hl7.org/fhir/constraint-severity)
        /// </summary>
        [EnumLiteral("warning", "http://hl7.org/fhir/constraint-severity"), Description("Warning")]
        Warning,
    }

    /// <summary>
    /// The lifecycle status of an artifact.
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
    /// Codes indicating the degree of authority/intentionality associated with a care plan.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-intent)
    /// </summary>
    [FhirEnumeration("CarePlanIntent")]
    public enum CarePlanIntent
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
        [EnumLiteral("option", "http://hl7.org/fhir/request-intent"), Description("Option")]
        Option,
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
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/care-team-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// Codes identifying the lifecycle stage of a ChargeItem.
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
    /// The meaning of the hierarchy of concepts in a code system.
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
    /// Which type a compartment definition describes.
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
    /// Indicates the state of the consent.
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
    /// How a resource reference is interpreted when testing consent restrictions.
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
    /// Codes to identify how UDI data was entered.
    /// (url: http://hl7.org/fhir/ValueSet/udi-entry-type)
    /// </summary>
    [FhirEnumeration("UDIEntryType")]
    public enum UDIEntryType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/udi-entry-type)
        /// </summary>
        [EnumLiteral("barcode", "http://hl7.org/fhir/udi-entry-type"), Description("Barcode")]
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
    /// A coded concept indicating the current status of the Device Usage.
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
    /// Current state of the encounter.
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
    /// The status of the endpoint.
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
    /// How a compartment must be linked.
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
    /// The status of a guidance response.
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
    /// Used to distinguish different roles a resource can play within a set of linked resources.
    /// (url: http://hl7.org/fhir/ValueSet/linkage-type)
    /// </summary>
    [FhirEnumeration("LinkageType")]
    public enum LinkageType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/linkage-type)
        /// </summary>
        [EnumLiteral("source", "http://hl7.org/fhir/linkage-type"), Description("Source of Truth")]
        Source,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/linkage-type)
        /// </summary>
        [EnumLiteral("alternate", "http://hl7.org/fhir/linkage-type"), Description("Alternate Record")]
        Alternate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/linkage-type)
        /// </summary>
        [EnumLiteral("historical", "http://hl7.org/fhir/linkage-type"), Description("Historical/Obsolete Record")]
        Historical,
    }

    /// <summary>
    /// The status of the measure report.
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
    /// Type for quality report.
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
    /// Type for access of external URI.
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
    /// Defines the kinds of conditions that can appear on actions.
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
    /// Defines the types of relationships between actions.
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
    /// Defines organization behavior of a group.
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
    /// Defines selection behavior of a group.
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
    /// Defines expectations around whether an action or action group is required.
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
    /// Defines selection frequency behavior for an action or group.
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
    /// Defines behavior for an action or a group for how many times that item may be repeated.
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
    /// Distinguishes groups from questions and display text and indicates data type for questions.
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
    /// What Search Comparator Codes are supported in search.
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
        [EnumLiteral("lt", "http://hl7.org/fhir/search-comparator"), Description("Less Than")]
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
    /// How the referenced structure is used in this mapping.
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
    /// If this is the default rule set to apply for the source type, or this combination of types.
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
    /// Mode for this instance of data.
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
    /// If field is a list, how to manage the source.
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
    /// How to interpret the context.
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
    /// If field is a list, how to manage the production.
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
    /// How data is copied/created.
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
    /// A code to indicate if the substance is actively used.
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
    /// The type of contributor.
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
    /// How an element value is interpreted when discrimination is evaluated.
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
    /// Whether a reference needs to be version specific or version independent, or whether either can be used.
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
    /// The type of relationship to the related artifact.
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
    /// Either a resource or a data type that is defined in all the supported FHIR versions
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
        [EnumLiteral("DeviceMetric", "http://hl7.org/fhir/resource-types"), Description("DeviceMetric")]
        DeviceMetric,
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
    /// Supported FHIR versions
    /// (url: http://hl7.org/fhir/ValueSet/versions)
    /// </summary>
    [FhirEnumeration("Version")]
    [Flags]
    public enum Version
    {
        DSTU2 = 1,
        R4 = 2,
        STU3 = 4,
        All = 7,
        None = 0,
    }

}
