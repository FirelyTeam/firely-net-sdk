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
// Generated for FHIR v1.0.2, v3.0.1
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
    /// HTTP verbs (in the HTTP command line).
    /// (url: http://hl7.org/fhir/ValueSet/http-verb)
    /// </summary>
    [FhirEnumeration("HTTPVerb")]
    public enum HTTPVerb
    {
        /// <summary>
        /// HTTP GET
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("GET", "http://hl7.org/fhir/http-verb"), Description("GET")]
        GET,
        /// <summary>
        /// HTTP POST
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("POST", "http://hl7.org/fhir/http-verb"), Description("POST")]
        POST,
        /// <summary>
        /// HTTP PUT
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("PUT", "http://hl7.org/fhir/http-verb"), Description("PUT")]
        PUT,
        /// <summary>
        /// HTTP DELETE
        /// (system: http://hl7.org/fhir/http-verb)
        /// </summary>
        [EnumLiteral("DELETE", "http://hl7.org/fhir/http-verb"), Description("DELETE")]
        DELETE,
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
        [EnumLiteral("ChargeItem", "http://hl7.org/fhir/resource-types"), Description("ChargeItem")]
        ChargeItem,
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
        [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/resource-types"), Description("DeviceRequest")]
        DeviceRequest,
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
        [EnumLiteral("ExpansionProfile", "http://hl7.org/fhir/resource-types"), Description("ExpansionProfile")]
        ExpansionProfile,
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
        [EnumLiteral("ImagingManifest", "http://hl7.org/fhir/resource-types"), Description("ImagingManifest")]
        ImagingManifest,
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
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/resource-types"), Description("MedicationRequest")]
        MedicationRequest,
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
        [EnumLiteral("StructureMap", "http://hl7.org/fhir/resource-types"), Description("StructureMap")]
        StructureMap,
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
        /// Search parameter SHALL be a number (a whole number, or a decimal).
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("number", "http://hl7.org/fhir/search-param-type"), Description("Number")]
        Number,
        /// <summary>
        /// Search parameter is on a date/time. The date format is the standard XML format, though other formats may be supported.
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("date", "http://hl7.org/fhir/search-param-type"), Description("Date/DateTime")]
        Date,
        /// <summary>
        /// Search parameter is a simple string, like a name part. Search is case-insensitive and accent-insensitive. May match just the start of a string. String parameters may contain spaces.
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/search-param-type"), Description("String")]
        String,
        /// <summary>
        /// Search parameter on a coded element or identifier. May be used to search through the text, displayname, code and code/codesystem (for codes) and label, system and key (for identifier). Its value is either a string or a pair of namespace and value, separated by a "|", depending on the modifier used.
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("token", "http://hl7.org/fhir/search-param-type"), Description("Token")]
        Token,
        /// <summary>
        /// A reference to another resource.
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/search-param-type"), Description("Reference")]
        Reference,
        /// <summary>
        /// A composite search parameter that combines a search on two values together.
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("composite", "http://hl7.org/fhir/search-param-type"), Description("Composite")]
        Composite,
        /// <summary>
        /// A search parameter that searches on a quantity.
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("quantity", "http://hl7.org/fhir/search-param-type"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// A search parameter that searches on a URI (RFC 3986).
        /// (system: http://hl7.org/fhir/search-param-type)
        /// </summary>
        [EnumLiteral("uri", "http://hl7.org/fhir/search-param-type"), Description("URI")]
        Uri,
    }

    /// <summary>
    /// A supported modifier for a search parameter.
    /// (url: http://hl7.org/fhir/ValueSet/search-modifier-code)
    /// </summary>
    [FhirEnumeration("SearchModifierCode")]
    public enum SearchModifierCode
    {
        /// <summary>
        /// The search parameter returns resources that have a value or not.
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("missing", "http://hl7.org/fhir/search-modifier-code"), Description("Missing")]
        Missing,
        /// <summary>
        /// The search parameter returns resources that have a value that exactly matches the supplied parameter (the whole string, including casing and accents).
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("exact", "http://hl7.org/fhir/search-modifier-code"), Description("Exact")]
        Exact,
        /// <summary>
        /// The search parameter returns resources that include the supplied parameter value anywhere within the field being searched.
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("contains", "http://hl7.org/fhir/search-modifier-code"), Description("Contains")]
        Contains,
        /// <summary>
        /// The search parameter returns resources that do not contain a match .
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("not", "http://hl7.org/fhir/search-modifier-code"), Description("Not")]
        Not,
        /// <summary>
        /// The search parameter is processed as a string that searches text associated with the code/value - either CodeableConcept.text, Coding.display, or Identifier.type.text.
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("text", "http://hl7.org/fhir/search-modifier-code"), Description("Text")]
        Text,
        /// <summary>
        /// The search parameter is a URI (relative or absolute) that identifies a value set, and the search parameter tests whether the coding is in the specified value set.
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("in", "http://hl7.org/fhir/search-modifier-code"), Description("In")]
        In,
        /// <summary>
        /// The search parameter is a URI (relative or absolute) that identifies a value set, and the search parameter tests whether the coding is not in the specified value set.
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("not-in", "http://hl7.org/fhir/search-modifier-code"), Description("Not In")]
        NotIn,
        /// <summary>
        /// The search parameter tests whether the value in a resource is subsumed by the specified value (is-a, or hierarchical relationships).
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("below", "http://hl7.org/fhir/search-modifier-code"), Description("Below")]
        Below,
        /// <summary>
        /// The search parameter tests whether the value in a resource subsumes the specified value (is-a, or hierarchical relationships).
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("above", "http://hl7.org/fhir/search-modifier-code"), Description("Above")]
        Above,
        /// <summary>
        /// The search parameter only applies to the Resource Type specified as a modifier (e.g. the modifier is not actually :type, but :Patient etc.).
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("type", "http://hl7.org/fhir/search-modifier-code"), Description("Type")]
        Type,
    }

    /// <summary>
    /// The impact of the content of a message.
    /// (url: http://hl7.org/fhir/ValueSet/message-significance-category)
    /// </summary>
    [FhirEnumeration("MessageSignificanceCategory")]
    public enum MessageSignificanceCategory
    {
        /// <summary>
        /// The message represents/requests a change that should not be processed more than once; e.g. Making a booking for an appointment.
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Consequence", "http://hl7.org/fhir/message-significance-category"), Description("Consequence")]
        Consequence,
        /// <summary>
        /// The message represents a response to query for current information. Retrospective processing is wrong and/or wasteful.
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Currency", "http://hl7.org/fhir/message-significance-category"), Description("Currency")]
        Currency,
        /// <summary>
        /// The content is not necessarily intended to be current, and it can be reprocessed, though there may be version issues created by processing old notifications.
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("Notification", "http://hl7.org/fhir/message-significance-category"), Description("Notification")]
        Notification,
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
    /// A code that describes the type of issue.
    /// (url: http://hl7.org/fhir/ValueSet/issue-type)
    /// </summary>
    [FhirEnumeration("IssueType")]
    public enum IssueType
    {
        /// <summary>
        /// Content invalid against the specification or a profile.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("invalid", "http://hl7.org/fhir/issue-type"), Description("Invalid Content")]
        Invalid,
        /// <summary>
        /// A structural issue in the content such as wrong namespace, or unable to parse the content completely, or invalid json syntax.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("structure", "http://hl7.org/fhir/issue-type"), Description("Structural Issue")]
        Structure,
        /// <summary>
        /// A required element is missing.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("required", "http://hl7.org/fhir/issue-type"), Description("Required element missing")]
        Required,
        /// <summary>
        /// An element value is invalid.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("value", "http://hl7.org/fhir/issue-type"), Description("Element value invalid")]
        Value,
        /// <summary>
        /// A content validation rule failed - e.g. a schematron rule.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("invariant", "http://hl7.org/fhir/issue-type"), Description("Validation rule failed")]
        Invariant,
        /// <summary>
        /// An authentication/authorization/permissions issue of some kind.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("security", "http://hl7.org/fhir/issue-type"), Description("Security Problem")]
        Security,
        /// <summary>
        /// The client needs to initiate an authentication process.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("login", "http://hl7.org/fhir/issue-type"), Description("Login Required")]
        Login,
        /// <summary>
        /// The user or system was not able to be authenticated (either there is no process, or the proferred token is unacceptable).
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/issue-type"), Description("Unknown User")]
        Unknown,
        /// <summary>
        /// User session expired; a login may be required.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("expired", "http://hl7.org/fhir/issue-type"), Description("Session Expired")]
        Expired,
        /// <summary>
        /// The user does not have the rights to perform this action.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("forbidden", "http://hl7.org/fhir/issue-type"), Description("Forbidden")]
        Forbidden,
        /// <summary>
        /// Some information was not or may not have been returned due to business rules, consent or privacy rules, or access permission constraints.  This information may be accessible through alternate processes.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("suppressed", "http://hl7.org/fhir/issue-type"), Description("Information  Suppressed")]
        Suppressed,
        /// <summary>
        /// Processing issues. These are expected to be final e.g. there is no point resubmitting the same content unchanged.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("processing", "http://hl7.org/fhir/issue-type"), Description("Processing Failure")]
        Processing,
        /// <summary>
        /// The resource or profile is not supported.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("not-supported", "http://hl7.org/fhir/issue-type"), Description("Content not supported")]
        NotSupported,
        /// <summary>
        /// An attempt was made to create a duplicate record.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("duplicate", "http://hl7.org/fhir/issue-type"), Description("Duplicate")]
        Duplicate,
        /// <summary>
        /// The reference provided was not found. In a pure RESTful environment, this would be an HTTP 404 error, but this code may be used where the content is not found further into the application architecture.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("not-found", "http://hl7.org/fhir/issue-type"), Description("Not Found")]
        NotFound,
        /// <summary>
        /// Provided content is too long (typically, this is a denial of service protection type of error).
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("too-long", "http://hl7.org/fhir/issue-type"), Description("Content Too Long")]
        TooLong,
        /// <summary>
        /// The code or system could not be understood, or it was not valid in the context of a particular ValueSet.code.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("code-invalid", "http://hl7.org/fhir/issue-type"), Description("Invalid Code")]
        CodeInvalid,
        /// <summary>
        /// An extension was found that was not acceptable, could not be resolved, or a modifierExtension was not recognized.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("extension", "http://hl7.org/fhir/issue-type"), Description("Unacceptable Extension")]
        Extension,
        /// <summary>
        /// The operation was stopped to protect server resources; e.g. a request for a value set expansion on all of SNOMED CT.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("too-costly", "http://hl7.org/fhir/issue-type"), Description("Operation Too Costly")]
        TooCostly,
        /// <summary>
        /// The content/operation failed to pass some business rule, and so could not proceed.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("business-rule", "http://hl7.org/fhir/issue-type"), Description("Business Rule Violation")]
        BusinessRule,
        /// <summary>
        /// Content could not be accepted because of an edit conflict (i.e. version aware updates) (In a pure RESTful environment, this would be an HTTP 404 error, but this code may be used where the conflict is discovered further into the application architecture.)
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("conflict", "http://hl7.org/fhir/issue-type"), Description("Edit Version Conflict")]
        Conflict,
        /// <summary>
        /// Not all data sources typically accessed could be reached, or responded in time, so the returned information may not be complete.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("incomplete", "http://hl7.org/fhir/issue-type"), Description("Incomplete Results")]
        Incomplete,
        /// <summary>
        /// Transient processing issues. The system receiving the error may be able to resubmit the same content once an underlying issue is resolved.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("transient", "http://hl7.org/fhir/issue-type"), Description("Transient Issue")]
        Transient,
        /// <summary>
        /// A resource/record locking failure (usually in an underlying database).
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("lock-error", "http://hl7.org/fhir/issue-type"), Description("Lock Error")]
        LockError,
        /// <summary>
        /// The persistent store is unavailable; e.g. the database is down for maintenance or similar action.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("no-store", "http://hl7.org/fhir/issue-type"), Description("No Store Available")]
        NoStore,
        /// <summary>
        /// An unexpected internal error has occurred.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("exception", "http://hl7.org/fhir/issue-type"), Description("Exception")]
        Exception,
        /// <summary>
        /// An internal timeout has occurred.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("timeout", "http://hl7.org/fhir/issue-type"), Description("Timeout")]
        Timeout,
        /// <summary>
        /// The system is not prepared to handle this request due to load management.
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("throttled", "http://hl7.org/fhir/issue-type"), Description("Throttled")]
        Throttled,
        /// <summary>
        /// A message unrelated to the processing success of the completed operation (examples of the latter include things like reminders of password expiry, system maintenance times, etc.).
        /// (system: http://hl7.org/fhir/issue-type)
        /// </summary>
        [EnumLiteral("informational", "http://hl7.org/fhir/issue-type"), Description("Informational Note")]
        Informational,
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
    /// Identifies the purpose for this identifier, if known .
    /// (url: http://hl7.org/fhir/ValueSet/identifier-use)
    /// </summary>
    [FhirEnumeration("IdentifierUse")]
    public enum IdentifierUse
    {
        /// <summary>
        /// The identifier recommended for display and use in real-world interactions.
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("usual", "http://hl7.org/fhir/identifier-use"), Description("Usual")]
        Usual,
        /// <summary>
        /// The identifier considered to be most trusted for the identification of this item.
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("official", "http://hl7.org/fhir/identifier-use"), Description("Official")]
        Official,
        /// <summary>
        /// A temporary identifier.
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("temp", "http://hl7.org/fhir/identifier-use"), Description("Temp")]
        Temp,
        /// <summary>
        /// An identifier that was assigned in secondary use - it serves to identify the object in a relative context, but cannot be consistently assigned to the same object again in a different context.
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("secondary", "http://hl7.org/fhir/identifier-use"), Description("Secondary")]
        Secondary,
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
    /// The use of an address<br/>
    /// <br/>
    /// The use of an address (home / work / etc.).
    /// (url: http://hl7.org/fhir/ValueSet/address-use)
    /// </summary>
    [FhirEnumeration("AddressUse")]
    public enum AddressUse
    {
        /// <summary>
        /// A communication address at a home.
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("home", "http://hl7.org/fhir/address-use"), Description("Home")]
        Home,
        /// <summary>
        /// An office address. First choice for business related contacts during business hours.
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("work", "http://hl7.org/fhir/address-use"), Description("Work")]
        Work,
        /// <summary>
        /// A temporary address. The period can provide more detailed information.
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("temp", "http://hl7.org/fhir/address-use"), Description("Temporary")]
        Temp,
        /// <summary>
        /// This address is no longer in use (or was never correct, but retained for records).
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("old", "http://hl7.org/fhir/address-use"), Description("Old / Incorrect")]
        Old,
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
    /// Supported FHIR versions
    /// </summary>
    [FhirEnumeration("Version")]
    public enum Version
    {
        DSTU2,
        STU3,
        All,
    }

}
