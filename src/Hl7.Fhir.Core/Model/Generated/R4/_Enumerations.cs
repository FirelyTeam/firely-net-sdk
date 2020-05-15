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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/account-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/account-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/account-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/account-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// A list of all the request resource types defined in this version of the FHIR specification.
    /// (url: http://hl7.org/fhir/ValueSet/request-resource-types)
    /// </summary>
    [FhirEnumeration("RequestResourceType")]
    public enum RequestResourceType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("Appointment", "http://hl7.org/fhir/request-resource-types"), Description("Appointment")]
        Appointment,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/request-resource-types"), Description("AppointmentResponse")]
        AppointmentResponse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("CarePlan", "http://hl7.org/fhir/request-resource-types"), Description("CarePlan")]
        CarePlan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("Claim", "http://hl7.org/fhir/request-resource-types"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/request-resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("Contract", "http://hl7.org/fhir/request-resource-types"), Description("Contract")]
        Contract,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/request-resource-types"), Description("DeviceRequest")]
        DeviceRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/request-resource-types"), Description("EnrollmentRequest")]
        EnrollmentRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/request-resource-types"), Description("ImmunizationRecommendation")]
        ImmunizationRecommendation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/request-resource-types"), Description("MedicationRequest")]
        MedicationRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/request-resource-types"), Description("NutritionOrder")]
        NutritionOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/request-resource-types"), Description("ServiceRequest")]
        ServiceRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/request-resource-types"), Description("SupplyRequest")]
        SupplyRequest,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("Task", "http://hl7.org/fhir/request-resource-types"), Description("Task")]
        Task,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/request-resource-types"), Description("VisionPrescription")]
        VisionPrescription,
    }

    /// <summary>
    /// Codes indicating the degree of authority/intentionality associated with a request.
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
        [EnumLiteral("directive", "http://hl7.org/fhir/request-intent"), Description("Directive")]
        Directive,
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
    /// The type of participant for the action.
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/action-participant-type)
        /// </summary>
        [EnumLiteral("device", "http://hl7.org/fhir/action-participant-type"), Description("Device")]
        Device,
    }

    /// <summary>
    /// Overall nature of the adverse event, e.g. real or potential.
    /// (url: http://hl7.org/fhir/ValueSet/adverse-event-actuality)
    /// </summary>
    [FhirEnumeration("AdverseEventActuality")]
    public enum AdverseEventActuality
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/adverse-event-actuality)
        /// </summary>
        [EnumLiteral("actual", "http://hl7.org/fhir/adverse-event-actuality"), Description("Adverse Event")]
        Actual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/adverse-event-actuality)
        /// </summary>
        [EnumLiteral("potential", "http://hl7.org/fhir/adverse-event-actuality"), Description("Potential Adverse Event")]
        Potential,
    }

    /// <summary>
    /// Category of an identified substance associated with allergies or intolerances.
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("checked-in", "http://hl7.org/fhir/appointmentstatus"), Description("Checked In")]
        CheckedIn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/appointmentstatus)
        /// </summary>
        [EnumLiteral("waitlist", "http://hl7.org/fhir/appointmentstatus"), Description("Waitlisted")]
        Waitlist,
    }

    /// <summary>
    /// The type of network access point of this agent in the audit event.
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
    /// Biologically Derived Product Category.
    /// (url: http://hl7.org/fhir/ValueSet/product-category)
    /// </summary>
    [FhirEnumeration("BiologicallyDerivedProductCategory")]
    public enum BiologicallyDerivedProductCategory
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-category)
        /// </summary>
        [EnumLiteral("organ", "http://hl7.org/fhir/product-category"), Description("Organ")]
        Organ,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-category)
        /// </summary>
        [EnumLiteral("tissue", "http://hl7.org/fhir/product-category"), Description("Tissue")]
        Tissue,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-category)
        /// </summary>
        [EnumLiteral("fluid", "http://hl7.org/fhir/product-category"), Description("Fluid")]
        Fluid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-category)
        /// </summary>
        [EnumLiteral("cells", "http://hl7.org/fhir/product-category"), Description("Cells")]
        Cells,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-category)
        /// </summary>
        [EnumLiteral("biologicalAgent", "http://hl7.org/fhir/product-category"), Description("BiologicalAgent")]
        BiologicalAgent,
    }

    /// <summary>
    /// Biologically Derived Product Status.
    /// (url: http://hl7.org/fhir/ValueSet/product-status)
    /// </summary>
    [FhirEnumeration("BiologicallyDerivedProductStatus")]
    public enum BiologicallyDerivedProductStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-status)
        /// </summary>
        [EnumLiteral("available", "http://hl7.org/fhir/product-status"), Description("Available")]
        Available,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-status)
        /// </summary>
        [EnumLiteral("unavailable", "http://hl7.org/fhir/product-status"), Description("Unavailable")]
        Unavailable,
    }

    /// <summary>
    /// BiologicallyDerived Product Storage Scale.
    /// (url: http://hl7.org/fhir/ValueSet/product-storage-scale)
    /// </summary>
    [FhirEnumeration("BiologicallyDerivedProductStorageScale")]
    public enum BiologicallyDerivedProductStorageScale
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-storage-scale)
        /// </summary>
        [EnumLiteral("farenheit", "http://hl7.org/fhir/product-storage-scale"), Description("Fahrenheit")]
        Farenheit,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-storage-scale)
        /// </summary>
        [EnumLiteral("celsius", "http://hl7.org/fhir/product-storage-scale"), Description("Celsius")]
        Celsius,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/product-storage-scale)
        /// </summary>
        [EnumLiteral("kelvin", "http://hl7.org/fhir/product-storage-scale"), Description("Kelvin")]
        Kelvin,
    }

    /// <summary>
    /// All published FHIR Versions.
    /// (url: http://hl7.org/fhir/ValueSet/FHIR-version)
    /// </summary>
    [FhirEnumeration("FHIRVersion")]
    public enum FHIRVersion
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.01", "http://hl7.org/fhir/FHIR-version"), Description("0.01")]
        N0_01,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.05", "http://hl7.org/fhir/FHIR-version"), Description("0.05")]
        N0_05,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.06", "http://hl7.org/fhir/FHIR-version"), Description("0.06")]
        N0_06,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.11", "http://hl7.org/fhir/FHIR-version"), Description("0.11")]
        N0_11,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.0.80", "http://hl7.org/fhir/FHIR-version"), Description("0.0.80")]
        N0_0_80,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.0.81", "http://hl7.org/fhir/FHIR-version"), Description("0.0.81")]
        N0_0_81,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.0.82", "http://hl7.org/fhir/FHIR-version"), Description("0.0.82")]
        N0_0_82,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.4.0", "http://hl7.org/fhir/FHIR-version"), Description("0.4.0")]
        N0_4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("0.5.0", "http://hl7.org/fhir/FHIR-version"), Description("0.5.0")]
        N0_5_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.0.0", "http://hl7.org/fhir/FHIR-version"), Description("1.0.0")]
        N1_0_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.0.1", "http://hl7.org/fhir/FHIR-version"), Description("1.0.1")]
        N1_0_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.0.2", "http://hl7.org/fhir/FHIR-version"), Description("1.0.2")]
        N1_0_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.1.0", "http://hl7.org/fhir/FHIR-version"), Description("1.1.0")]
        N1_1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.4.0", "http://hl7.org/fhir/FHIR-version"), Description("1.4.0")]
        N1_4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.6.0", "http://hl7.org/fhir/FHIR-version"), Description("1.6.0")]
        N1_6_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("1.8.0", "http://hl7.org/fhir/FHIR-version"), Description("1.8.0")]
        N1_8_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("3.0.0", "http://hl7.org/fhir/FHIR-version"), Description("3.0.0")]
        N3_0_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("3.0.1", "http://hl7.org/fhir/FHIR-version"), Description("3.0.1")]
        N3_0_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("3.3.0", "http://hl7.org/fhir/FHIR-version"), Description("3.3.0")]
        N3_3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("3.5.0", "http://hl7.org/fhir/FHIR-version"), Description("3.5.0")]
        N3_5_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("4.0.0", "http://hl7.org/fhir/FHIR-version"), Description("4.0.0")]
        N4_0_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/FHIR-version)
        /// </summary>
        [EnumLiteral("4.0.1", "http://hl7.org/fhir/FHIR-version"), Description("4.0.1")]
        N4_0_1,
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
    /// Codes identifying the lifecycle stage of a request.
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
        [EnumLiteral("on-hold", "http://hl7.org/fhir/request-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/request-status)
        /// </summary>
        [EnumLiteral("revoked", "http://hl7.org/fhir/request-status"), Description("Revoked")]
        Revoked,
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
    /// Resource types defined as part of FHIR that can be represented as in-line definitions of a care plan activity.
    /// (url: http://hl7.org/fhir/ValueSet/care-plan-activity-kind)
    /// </summary>
    [FhirEnumeration("CarePlanActivityKind")]
    public enum CarePlanActivityKind
    {
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
        [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/resource-types"), Description("CommunicationRequest")]
        CommunicationRequest,
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
        [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/resource-types"), Description("MedicationRequest")]
        MedicationRequest,
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
        [EnumLiteral("Task", "http://hl7.org/fhir/resource-types"), Description("Task")]
        Task,
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
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
    }

    /// <summary>
    /// Codes that reflect the current state of a care plan activity within its overall life cycle.
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
        [EnumLiteral("stopped", "http://hl7.org/fhir/care-plan-activity-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/care-plan-activity-status"), Description("Unknown")]
        Unknown,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/care-plan-activity-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/care-plan-activity-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The type of relations between entries.
    /// (url: http://hl7.org/fhir/ValueSet/relation-type)
    /// </summary>
    [FhirEnumeration("CatalogEntryRelationType")]
    public enum CatalogEntryRelationType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/relation-type)
        /// </summary>
        [EnumLiteral("triggers", "http://hl7.org/fhir/relation-type"), Description("Triggers")]
        Triggers,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/relation-type)
        /// </summary>
        [EnumLiteral("is-replaced-by", "http://hl7.org/fhir/relation-type"), Description("Replaced By")]
        IsReplacedBy,
    }

    /// <summary>
    /// Codes indicating the kind of the price component.
    /// (url: http://hl7.org/fhir/ValueSet/invoice-priceComponentType)
    /// </summary>
    [FhirEnumeration("InvoicePriceComponentType")]
    public enum InvoicePriceComponentType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-priceComponentType)
        /// </summary>
        [EnumLiteral("base", "http://hl7.org/fhir/invoice-priceComponentType"), Description("base price")]
        Base,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-priceComponentType)
        /// </summary>
        [EnumLiteral("surcharge", "http://hl7.org/fhir/invoice-priceComponentType"), Description("surcharge")]
        Surcharge,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-priceComponentType)
        /// </summary>
        [EnumLiteral("deduction", "http://hl7.org/fhir/invoice-priceComponentType"), Description("deduction")]
        Deduction,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-priceComponentType)
        /// </summary>
        [EnumLiteral("discount", "http://hl7.org/fhir/invoice-priceComponentType"), Description("discount")]
        Discount,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-priceComponentType)
        /// </summary>
        [EnumLiteral("tax", "http://hl7.org/fhir/invoice-priceComponentType"), Description("tax")]
        Tax,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-priceComponentType)
        /// </summary>
        [EnumLiteral("informational", "http://hl7.org/fhir/invoice-priceComponentType"), Description("informational")]
        Informational,
    }

    /// <summary>
    /// The purpose of the Claim: predetermination, preauthorization, claim.
    /// (url: http://hl7.org/fhir/ValueSet/claim-use)
    /// </summary>
    [FhirEnumeration("Use")]
    public enum Use
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("claim", "http://hl7.org/fhir/claim-use"), Description("Claim")]
        Claim,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("preauthorization", "http://hl7.org/fhir/claim-use"), Description("Preauthorization")]
        Preauthorization,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/claim-use)
        /// </summary>
        [EnumLiteral("predetermination", "http://hl7.org/fhir/claim-use"), Description("Predetermination")]
        Predetermination,
    }

    /// <summary>
    /// This value set includes Claim Processing Outcome codes.
    /// (url: http://hl7.org/fhir/ValueSet/remittance-outcome)
    /// </summary>
    [FhirEnumeration("ClaimProcessingCodes")]
    public enum ClaimProcessingCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/remittance-outcome)
        /// </summary>
        [EnumLiteral("queued", "http://hl7.org/fhir/remittance-outcome"), Description("Queued")]
        Queued,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/remittance-outcome)
        /// </summary>
        [EnumLiteral("complete", "http://hl7.org/fhir/remittance-outcome"), Description("Processing Complete")]
        Complete,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/remittance-outcome)
        /// </summary>
        [EnumLiteral("error", "http://hl7.org/fhir/remittance-outcome"), Description("Error")]
        Error,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/remittance-outcome)
        /// </summary>
        [EnumLiteral("partial", "http://hl7.org/fhir/remittance-outcome"), Description("Partial Processing")]
        Partial,
    }

    /// <summary>
    /// The presentation types of notes.
    /// (url: http://hl7.org/fhir/ValueSet/note-type)
    /// </summary>
    [FhirEnumeration("NoteType")]
    public enum NoteType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/note-type)
        /// </summary>
        [EnumLiteral("display", "http://hl7.org/fhir/note-type"), Description("Display")]
        Display,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/note-type)
        /// </summary>
        [EnumLiteral("print", "http://hl7.org/fhir/note-type"), Description("Print (Form)")]
        Print,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/note-type)
        /// </summary>
        [EnumLiteral("printoper", "http://hl7.org/fhir/note-type"), Description("Print (Operator)")]
        Printoper,
    }

    /// <summary>
    /// Codes that reflect the current state of a clinical impression within its overall lifecycle.
    /// (url: http://hl7.org/fhir/ValueSet/clinicalimpression-status)
    /// </summary>
    [FhirEnumeration("ClinicalImpressionStatus")]
    public enum ClinicalImpressionStatus
    {
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
        [EnumLiteral("completed", "http://hl7.org/fhir/event-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/event-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The extent of the content of the code system (the concepts and codes it defines) are represented in a code system resource.
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/codesystem-content-mode)
        /// </summary>
        [EnumLiteral("supplement", "http://hl7.org/fhir/codesystem-content-mode"), Description("Supplement")]
        Supplement,
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
    /// The type of a property value.
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [EnumLiteral("decimal", "http://hl7.org/fhir/concept-property-type"), Description("decimal")]
        Decimal,
    }

    /// <summary>
    /// Codes identifying the lifecycle stage of an event.
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
        [EnumLiteral("not-done", "http://hl7.org/fhir/event-status"), Description("Not Done")]
        NotDone,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/event-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/event-status"), Description("Stopped")]
        Stopped,
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
    /// How a rule statement is applied, such as adding additional consent or removing consent.
    /// (url: http://hl7.org/fhir/ValueSet/consent-provision-type)
    /// </summary>
    [FhirEnumeration("ConsentProvisionType")]
    public enum ConsentProvisionType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-provision-type)
        /// </summary>
        [EnumLiteral("deny", "http://hl7.org/fhir/consent-provision-type"), Description("Opt Out")]
        Deny,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/consent-provision-type)
        /// </summary>
        [EnumLiteral("permit", "http://hl7.org/fhir/consent-provision-type"), Description("Opt In")]
        Permit,
    }

    /// <summary>
    /// This value set contract specific codes for status.
    /// (url: http://hl7.org/fhir/ValueSet/contract-publicationstatus)
    /// </summary>
    [FhirEnumeration("ContractResourcePublicationStatusCodes")]
    public enum ContractResourcePublicationStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("amended", "http://hl7.org/fhir/contract-publicationstatus"), Description("Amended")]
        Amended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("appended", "http://hl7.org/fhir/contract-publicationstatus"), Description("Appended")]
        Appended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/contract-publicationstatus"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("disputed", "http://hl7.org/fhir/contract-publicationstatus"), Description("Disputed")]
        Disputed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/contract-publicationstatus"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("executable", "http://hl7.org/fhir/contract-publicationstatus"), Description("Executable")]
        Executable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("executed", "http://hl7.org/fhir/contract-publicationstatus"), Description("Executed")]
        Executed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("negotiable", "http://hl7.org/fhir/contract-publicationstatus"), Description("Negotiable")]
        Negotiable,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("offered", "http://hl7.org/fhir/contract-publicationstatus"), Description("Offered")]
        Offered,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("policy", "http://hl7.org/fhir/contract-publicationstatus"), Description("Policy")]
        Policy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/contract-publicationstatus"), Description("Rejected")]
        Rejected,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("renewed", "http://hl7.org/fhir/contract-publicationstatus"), Description("Renewed")]
        Renewed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("revoked", "http://hl7.org/fhir/contract-publicationstatus"), Description("Revoked")]
        Revoked,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("resolved", "http://hl7.org/fhir/contract-publicationstatus"), Description("Resolved")]
        Resolved,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/contract-publicationstatus)
        /// </summary>
        [EnumLiteral("terminated", "http://hl7.org/fhir/contract-publicationstatus"), Description("Terminated")]
        Terminated,
    }

    /// <summary>
    /// A code specifying the types of information being requested.
    /// (url: http://hl7.org/fhir/ValueSet/eligibilityrequest-purpose)
    /// </summary>
    [FhirEnumeration("EligibilityRequestPurpose")]
    public enum EligibilityRequestPurpose
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityrequest-purpose)
        /// </summary>
        [EnumLiteral("auth-requirements", "http://hl7.org/fhir/eligibilityrequest-purpose"), Description("Coverage auth-requirements")]
        AuthRequirements,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityrequest-purpose)
        /// </summary>
        [EnumLiteral("benefits", "http://hl7.org/fhir/eligibilityrequest-purpose"), Description("Coverage benefits")]
        Benefits,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityrequest-purpose)
        /// </summary>
        [EnumLiteral("discovery", "http://hl7.org/fhir/eligibilityrequest-purpose"), Description("Coverage Discovery")]
        Discovery,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityrequest-purpose)
        /// </summary>
        [EnumLiteral("validation", "http://hl7.org/fhir/eligibilityrequest-purpose"), Description("Coverage Validation")]
        Validation,
    }

    /// <summary>
    /// A code specifying the types of information being requested.
    /// (url: http://hl7.org/fhir/ValueSet/eligibilityresponse-purpose)
    /// </summary>
    [FhirEnumeration("EligibilityResponsePurpose")]
    public enum EligibilityResponsePurpose
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
        /// </summary>
        [EnumLiteral("auth-requirements", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage auth-requirements")]
        AuthRequirements,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
        /// </summary>
        [EnumLiteral("benefits", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage benefits")]
        Benefits,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
        /// </summary>
        [EnumLiteral("discovery", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage Discovery")]
        Discovery,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
        /// </summary>
        [EnumLiteral("validation", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage Validation")]
        Validation,
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
    /// The type of name the device is referred by.
    /// (url: http://hl7.org/fhir/ValueSet/device-nametype)
    /// </summary>
    [FhirEnumeration("DeviceNameType")]
    public enum DeviceNameType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-nametype)
        /// </summary>
        [EnumLiteral("udi-label-name", "http://hl7.org/fhir/device-nametype"), Description("UDI Label name")]
        UdiLabelName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-nametype)
        /// </summary>
        [EnumLiteral("user-friendly-name", "http://hl7.org/fhir/device-nametype"), Description("User Friendly name")]
        UserFriendlyName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-nametype)
        /// </summary>
        [EnumLiteral("patient-reported-name", "http://hl7.org/fhir/device-nametype"), Description("Patient Reported name")]
        PatientReportedName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-nametype)
        /// </summary>
        [EnumLiteral("manufacturer-name", "http://hl7.org/fhir/device-nametype"), Description("Manufacturer name")]
        ManufacturerName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-nametype)
        /// </summary>
        [EnumLiteral("model-name", "http://hl7.org/fhir/device-nametype"), Description("Model name")]
        ModelName,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/device-nametype)
        /// </summary>
        [EnumLiteral("other", "http://hl7.org/fhir/device-nametype"), Description("other")]
        Other,
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
    /// The status of the diagnostic report.
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
    /// Whether the results by exposure is describing the results for the primary exposure of interest (exposure) or the alternative state (exposureAlternative).
    /// (url: http://hl7.org/fhir/ValueSet/exposure-state)
    /// </summary>
    [FhirEnumeration("ExposureState")]
    public enum ExposureState
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/exposure-state)
        /// </summary>
        [EnumLiteral("exposure", "http://hl7.org/fhir/exposure-state"), Description("Exposure")]
        Exposure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/exposure-state)
        /// </summary>
        [EnumLiteral("exposure-alternative", "http://hl7.org/fhir/exposure-state"), Description("Exposure Alternative")]
        ExposureAlternative,
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
    /// The possible types of variables for exposures or outcomes (E.g. Dichotomous, Continuous, Descriptive).
    /// (url: http://hl7.org/fhir/ValueSet/variable-type)
    /// </summary>
    [FhirEnumeration("EvidenceVariableType")]
    public enum EvidenceVariableType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/variable-type)
        /// </summary>
        [EnumLiteral("dichotomous", "http://hl7.org/fhir/variable-type"), Description("Dichotomous")]
        Dichotomous,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/variable-type)
        /// </summary>
        [EnumLiteral("continuous", "http://hl7.org/fhir/variable-type"), Description("Continuous")]
        Continuous,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/variable-type)
        /// </summary>
        [EnumLiteral("descriptive", "http://hl7.org/fhir/variable-type"), Description("Descriptive")]
        Descriptive,
    }

    /// <summary>
    /// Possible group measure aggregates (E.g. Mean, Median).
    /// (url: http://hl7.org/fhir/ValueSet/group-measure)
    /// </summary>
    [FhirEnumeration("GroupMeasure")]
    public enum GroupMeasure
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/group-measure)
        /// </summary>
        [EnumLiteral("mean", "http://hl7.org/fhir/group-measure"), Description("Mean")]
        Mean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/group-measure)
        /// </summary>
        [EnumLiteral("median", "http://hl7.org/fhir/group-measure"), Description("Median")]
        Median,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/group-measure)
        /// </summary>
        [EnumLiteral("mean-of-mean", "http://hl7.org/fhir/group-measure"), Description("Mean of Study Means")]
        MeanOfMean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/group-measure)
        /// </summary>
        [EnumLiteral("mean-of-median", "http://hl7.org/fhir/group-measure"), Description("Mean of Study Medins")]
        MeanOfMedian,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/group-measure)
        /// </summary>
        [EnumLiteral("median-of-mean", "http://hl7.org/fhir/group-measure"), Description("Median of Study Means")]
        MedianOfMean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/group-measure)
        /// </summary>
        [EnumLiteral("median-of-median", "http://hl7.org/fhir/group-measure"), Description("Median of Study Medians")]
        MedianOfMedian,
    }

    /// <summary>
    /// The type of actor - system or human.
    /// (url: http://hl7.org/fhir/ValueSet/examplescenario-actor-type)
    /// </summary>
    [FhirEnumeration("ExampleScenarioActorType")]
    public enum ExampleScenarioActorType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/examplescenario-actor-type)
        /// </summary>
        [EnumLiteral("person", "http://hl7.org/fhir/examplescenario-actor-type"), Description("Person")]
        Person,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/examplescenario-actor-type)
        /// </summary>
        [EnumLiteral("entity", "http://hl7.org/fhir/examplescenario-actor-type"), Description("System")]
        Entity,
    }

    /// <summary>
    /// Codes that reflect the current state of a goal and whether the goal is still being targeted.
    /// (url: http://hl7.org/fhir/ValueSet/goal-status)
    /// </summary>
    [FhirEnumeration("GoalLifecycleStatus")]
    public enum GoalLifecycleStatus
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
        [EnumLiteral("planned", "http://hl7.org/fhir/goal-status"), Description("Planned")]
        Planned,
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
        [EnumLiteral("active", "http://hl7.org/fhir/goal-status"), Description("Active")]
        Active,
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
        [EnumLiteral("completed", "http://hl7.org/fhir/goal-status"), Description("Completed")]
        Completed,
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
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/goal-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/goal-status)
        /// </summary>
        [EnumLiteral("rejected", "http://hl7.org/fhir/goal-status"), Description("Rejected")]
        Rejected,
    }

    /// <summary>
    /// Defines how a compartment rule is used.
    /// (url: http://hl7.org/fhir/ValueSet/graph-compartment-use)
    /// </summary>
    [FhirEnumeration("GraphCompartmentUse")]
    public enum GraphCompartmentUse
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/graph-compartment-use)
        /// </summary>
        [EnumLiteral("condition", "http://hl7.org/fhir/graph-compartment-use"), Description("Condition")]
        Condition,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/graph-compartment-use)
        /// </summary>
        [EnumLiteral("requirement", "http://hl7.org/fhir/graph-compartment-use"), Description("Requirement")]
        Requirement,
    }

    /// <summary>
    /// The status of the ImagingStudy.
    /// (url: http://hl7.org/fhir/ValueSet/imagingstudy-status)
    /// </summary>
    [FhirEnumeration("ImagingStudyStatus")]
    public enum ImagingStudyStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/imagingstudy-status)
        /// </summary>
        [EnumLiteral("registered", "http://hl7.org/fhir/imagingstudy-status"), Description("Registered")]
        Registered,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/imagingstudy-status)
        /// </summary>
        [EnumLiteral("available", "http://hl7.org/fhir/imagingstudy-status"), Description("Available")]
        Available,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/imagingstudy-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/imagingstudy-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/imagingstudy-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/imagingstudy-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/imagingstudy-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/imagingstudy-status"), Description("Unknown")]
        Unknown,
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
        [EnumLiteral("not-done", "http://hl7.org/fhir/event-status"), Description("Not Done")]
        NotDone,
    }

    /// <summary>
    /// The value set to instantiate this attribute should be drawn from a terminologically robust code system that consists of or contains concepts to support describing the current status of the evaluation for vaccine administration event.
    /// (url: http://hl7.org/fhir/ValueSet/immunization-evaluation-status)
    /// </summary>
    [FhirEnumeration("ImmunizationEvaluationStatusCodes")]
    public enum ImmunizationEvaluationStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("completed", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// The license that applies to an Implementation Guide (using an SPDX license Identifiers, or 'not-open-source'). The binding is required but new SPDX license Identifiers are allowed to be used (https://spdx.org/licenses/).
    /// (url: http://hl7.org/fhir/ValueSet/spdx-license)
    /// </summary>
    [FhirEnumeration("SPDXLicense")]
    public enum SPDXLicense
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("not-open-source", "http://hl7.org/fhir/spdx-license"), Description("Not open source")]
        NotOpenSource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("0BSD", "http://hl7.org/fhir/spdx-license"), Description("BSD Zero Clause License")]
        N0BSD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AAL", "http://hl7.org/fhir/spdx-license"), Description("Attribution Assurance License")]
        AAL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Abstyles", "http://hl7.org/fhir/spdx-license"), Description("Abstyles License")]
        Abstyles,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Adobe-2006", "http://hl7.org/fhir/spdx-license"), Description("Adobe Systems Incorporated Source Code License Agreement")]
        Adobe2006,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Adobe-Glyph", "http://hl7.org/fhir/spdx-license"), Description("Adobe Glyph List License")]
        AdobeGlyph,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ADSL", "http://hl7.org/fhir/spdx-license"), Description("Amazon Digital Services License")]
        ADSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AFL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v1.1")]
        AFL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AFL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v1.2")]
        AFL1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AFL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v2.0")]
        AFL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AFL-2.1", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v2.1")]
        AFL2_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AFL-3.0", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v3.0")]
        AFL3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Afmparse", "http://hl7.org/fhir/spdx-license"), Description("Afmparse License")]
        Afmparse,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AGPL-1.0-only", "http://hl7.org/fhir/spdx-license"), Description("Affero General Public License v1.0 only")]
        AGPL1_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AGPL-1.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("Affero General Public License v1.0 or later")]
        AGPL1_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AGPL-3.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Affero General Public License v3.0 only")]
        AGPL3_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AGPL-3.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Affero General Public License v3.0 or later")]
        AGPL3_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Aladdin", "http://hl7.org/fhir/spdx-license"), Description("Aladdin Free Public License")]
        Aladdin,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AMDPLPA", "http://hl7.org/fhir/spdx-license"), Description("AMD's plpa_map.c License")]
        AMDPLPA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AML", "http://hl7.org/fhir/spdx-license"), Description("Apple MIT License")]
        AML,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("AMPAS", "http://hl7.org/fhir/spdx-license"), Description("Academy of Motion Picture Arts and Sciences BSD")]
        AMPAS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ANTLR-PD", "http://hl7.org/fhir/spdx-license"), Description("ANTLR Software Rights Notice")]
        ANTLRPD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Apache-1.0", "http://hl7.org/fhir/spdx-license"), Description("Apache License 1.0")]
        Apache1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Apache-1.1", "http://hl7.org/fhir/spdx-license"), Description("Apache License 1.1")]
        Apache1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Apache-2.0", "http://hl7.org/fhir/spdx-license"), Description("Apache License 2.0")]
        Apache2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("APAFML", "http://hl7.org/fhir/spdx-license"), Description("Adobe Postscript AFM License")]
        APAFML,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("APL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Adaptive Public License 1.0")]
        APL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("APSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 1.0")]
        APSL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("APSL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 1.1")]
        APSL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("APSL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 1.2")]
        APSL1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("APSL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 2.0")]
        APSL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Artistic-1.0-cl8", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 1.0 w/clause 8")]
        Artistic1_0Cl8,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Artistic-1.0-Perl", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 1.0 (Perl)")]
        Artistic1_0Perl,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Artistic-1.0", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 1.0")]
        Artistic1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Artistic-2.0", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 2.0")]
        Artistic2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Bahyph", "http://hl7.org/fhir/spdx-license"), Description("Bahyph License")]
        Bahyph,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Barr", "http://hl7.org/fhir/spdx-license"), Description("Barr License")]
        Barr,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Beerware", "http://hl7.org/fhir/spdx-license"), Description("Beerware License")]
        Beerware,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BitTorrent-1.0", "http://hl7.org/fhir/spdx-license"), Description("BitTorrent Open Source License v1.0")]
        BitTorrent1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BitTorrent-1.1", "http://hl7.org/fhir/spdx-license"), Description("BitTorrent Open Source License v1.1")]
        BitTorrent1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Borceux", "http://hl7.org/fhir/spdx-license"), Description("Borceux license")]
        Borceux,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-1-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 1-Clause License")]
        BSD1Clause,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-2-Clause-FreeBSD", "http://hl7.org/fhir/spdx-license"), Description("BSD 2-Clause FreeBSD License")]
        BSD2ClauseFreeBSD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-2-Clause-NetBSD", "http://hl7.org/fhir/spdx-license"), Description("BSD 2-Clause NetBSD License")]
        BSD2ClauseNetBSD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-2-Clause-Patent", "http://hl7.org/fhir/spdx-license"), Description("BSD-2-Clause Plus Patent License")]
        BSD2ClausePatent,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-2-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 2-Clause \"Simplified\" License")]
        BSD2Clause,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause-Attribution", "http://hl7.org/fhir/spdx-license"), Description("BSD with attribution")]
        BSD3ClauseAttribution,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause-Clear", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause Clear License")]
        BSD3ClauseClear,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause-LBNL", "http://hl7.org/fhir/spdx-license"), Description("Lawrence Berkeley National Labs BSD variant license")]
        BSD3ClauseLBNL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause-No-Nuclear-License-2014", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause No Nuclear License 2014")]
        BSD3ClauseNoNuclearLicense2014,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause-No-Nuclear-License", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause No Nuclear License")]
        BSD3ClauseNoNuclearLicense,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause-No-Nuclear-Warranty", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause No Nuclear Warranty")]
        BSD3ClauseNoNuclearWarranty,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-3-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause \"New\" or \"Revised\" License")]
        BSD3Clause,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-4-Clause-UC", "http://hl7.org/fhir/spdx-license"), Description("BSD-4-Clause (University of California-Specific)")]
        BSD4ClauseUC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-4-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 4-Clause \"Original\" or \"Old\" License")]
        BSD4Clause,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-Protection", "http://hl7.org/fhir/spdx-license"), Description("BSD Protection License")]
        BSDProtection,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSD-Source-Code", "http://hl7.org/fhir/spdx-license"), Description("BSD Source Code Attribution")]
        BSDSourceCode,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("BSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Boost Software License 1.0")]
        BSL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("bzip2-1.0.5", "http://hl7.org/fhir/spdx-license"), Description("bzip2 and libbzip2 License v1.0.5")]
        Bzip21_0_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("bzip2-1.0.6", "http://hl7.org/fhir/spdx-license"), Description("bzip2 and libbzip2 License v1.0.6")]
        Bzip21_0_6,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Caldera", "http://hl7.org/fhir/spdx-license"), Description("Caldera License")]
        Caldera,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CATOSL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Computer Associates Trusted Open Source License 1.1")]
        CATOSL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 1.0 Generic")]
        CCBY1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 2.0 Generic")]
        CCBY2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 2.5 Generic")]
        CCBY2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 3.0 Unported")]
        CCBY3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 4.0 International")]
        CCBY4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 1.0 Generic")]
        CCBYNC1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 2.0 Generic")]
        CCBYNC2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 2.5 Generic")]
        CCBYNC2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 3.0 Unported")]
        CCBYNC3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 4.0 International")]
        CCBYNC4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-ND-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 1.0 Generic")]
        CCBYNCND1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-ND-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 2.0 Generic")]
        CCBYNCND2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-ND-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 2.5 Generic")]
        CCBYNCND2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-ND-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 3.0 Unported")]
        CCBYNCND3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-ND-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 4.0 International")]
        CCBYNCND4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-SA-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 1.0 Generic")]
        CCBYNCSA1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-SA-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 2.0 Generic")]
        CCBYNCSA2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-SA-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 2.5 Generic")]
        CCBYNCSA2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-SA-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 3.0 Unported")]
        CCBYNCSA3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-NC-SA-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 4.0 International")]
        CCBYNCSA4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-ND-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 1.0 Generic")]
        CCBYND1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-ND-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 2.0 Generic")]
        CCBYND2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-ND-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 2.5 Generic")]
        CCBYND2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-ND-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 3.0 Unported")]
        CCBYND3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-ND-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 4.0 International")]
        CCBYND4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-SA-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 1.0 Generic")]
        CCBYSA1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-SA-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 2.0 Generic")]
        CCBYSA2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-SA-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 2.5 Generic")]
        CCBYSA2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-SA-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 3.0 Unported")]
        CCBYSA3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC-BY-SA-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 4.0 International")]
        CCBYSA4_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CC0-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Zero v1.0 Universal")]
        CC01_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CDDL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Common Development and Distribution License 1.0")]
        CDDL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CDDL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Common Development and Distribution License 1.1")]
        CDDL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CDLA-Permissive-1.0", "http://hl7.org/fhir/spdx-license"), Description("Community Data License Agreement Permissive 1.0")]
        CDLAPermissive1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CDLA-Sharing-1.0", "http://hl7.org/fhir/spdx-license"), Description("Community Data License Agreement Sharing 1.0")]
        CDLASharing1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CECILL-1.0", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v1.0")]
        CECILL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CECILL-1.1", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v1.1")]
        CECILL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CECILL-2.0", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v2.0")]
        CECILL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CECILL-2.1", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v2.1")]
        CECILL2_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CECILL-B", "http://hl7.org/fhir/spdx-license"), Description("CeCILL-B Free Software License Agreement")]
        CECILLB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CECILL-C", "http://hl7.org/fhir/spdx-license"), Description("CeCILL-C Free Software License Agreement")]
        CECILLC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ClArtistic", "http://hl7.org/fhir/spdx-license"), Description("Clarified Artistic License")]
        ClArtistic,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CNRI-Jython", "http://hl7.org/fhir/spdx-license"), Description("CNRI Jython License")]
        CNRIJython,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CNRI-Python-GPL-Compatible", "http://hl7.org/fhir/spdx-license"), Description("CNRI Python Open Source GPL Compatible License Agreement")]
        CNRIPythonGPLCompatible,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CNRI-Python", "http://hl7.org/fhir/spdx-license"), Description("CNRI Python License")]
        CNRIPython,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Condor-1.1", "http://hl7.org/fhir/spdx-license"), Description("Condor Public License v1.1")]
        Condor1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CPAL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Common Public Attribution License 1.0")]
        CPAL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Common Public License 1.0")]
        CPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CPOL-1.02", "http://hl7.org/fhir/spdx-license"), Description("Code Project Open License 1.02")]
        CPOL1_02,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Crossword", "http://hl7.org/fhir/spdx-license"), Description("Crossword License")]
        Crossword,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CrystalStacker", "http://hl7.org/fhir/spdx-license"), Description("CrystalStacker License")]
        CrystalStacker,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("CUA-OPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("CUA Office Public License v1.0")]
        CUAOPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Cube", "http://hl7.org/fhir/spdx-license"), Description("Cube License")]
        Cube,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("curl", "http://hl7.org/fhir/spdx-license"), Description("curl License")]
        Curl,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("D-FSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Deutsche Freie Software Lizenz")]
        DFSL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("diffmark", "http://hl7.org/fhir/spdx-license"), Description("diffmark license")]
        Diffmark,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("DOC", "http://hl7.org/fhir/spdx-license"), Description("DOC License")]
        DOC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Dotseqn", "http://hl7.org/fhir/spdx-license"), Description("Dotseqn License")]
        Dotseqn,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("DSDP", "http://hl7.org/fhir/spdx-license"), Description("DSDP License")]
        DSDP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("dvipdfm", "http://hl7.org/fhir/spdx-license"), Description("dvipdfm License")]
        Dvipdfm,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ECL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Educational Community License v1.0")]
        ECL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ECL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Educational Community License v2.0")]
        ECL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EFL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Eiffel Forum License v1.0")]
        EFL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EFL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Eiffel Forum License v2.0")]
        EFL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("eGenix", "http://hl7.org/fhir/spdx-license"), Description("eGenix.com Public License 1.1.0")]
        EGenix,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Entessa", "http://hl7.org/fhir/spdx-license"), Description("Entessa Public License v1.0")]
        Entessa,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Eclipse Public License 1.0")]
        EPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Eclipse Public License 2.0")]
        EPL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ErlPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Erlang Public License v1.1")]
        ErlPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EUDatagrid", "http://hl7.org/fhir/spdx-license"), Description("EU DataGrid Software License")]
        EUDatagrid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EUPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("European Union Public License 1.0")]
        EUPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EUPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("European Union Public License 1.1")]
        EUPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("EUPL-1.2", "http://hl7.org/fhir/spdx-license"), Description("European Union Public License 1.2")]
        EUPL1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Eurosym", "http://hl7.org/fhir/spdx-license"), Description("Eurosym License")]
        Eurosym,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Fair", "http://hl7.org/fhir/spdx-license"), Description("Fair License")]
        Fair,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Frameworx-1.0", "http://hl7.org/fhir/spdx-license"), Description("Frameworx Open License 1.0")]
        Frameworx1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("FreeImage", "http://hl7.org/fhir/spdx-license"), Description("FreeImage Public License v1.0")]
        FreeImage,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("FSFAP", "http://hl7.org/fhir/spdx-license"), Description("FSF All Permissive License")]
        FSFAP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("FSFUL", "http://hl7.org/fhir/spdx-license"), Description("FSF Unlimited License")]
        FSFUL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("FSFULLR", "http://hl7.org/fhir/spdx-license"), Description("FSF Unlimited License (with License Retention)")]
        FSFULLR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("FTL", "http://hl7.org/fhir/spdx-license"), Description("Freetype Project License")]
        FTL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GFDL-1.1-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.1 only")]
        GFDL1_1Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GFDL-1.1-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.1 or later")]
        GFDL1_1OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GFDL-1.2-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.2 only")]
        GFDL1_2Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GFDL-1.2-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.2 or later")]
        GFDL1_2OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GFDL-1.3-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.3 only")]
        GFDL1_3Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GFDL-1.3-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.3 or later")]
        GFDL1_3OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Giftware", "http://hl7.org/fhir/spdx-license"), Description("Giftware License")]
        Giftware,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GL2PS", "http://hl7.org/fhir/spdx-license"), Description("GL2PS License")]
        GL2PS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Glide", "http://hl7.org/fhir/spdx-license"), Description("3dfx Glide License")]
        Glide,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Glulxe", "http://hl7.org/fhir/spdx-license"), Description("Glulxe License")]
        Glulxe,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("gnuplot", "http://hl7.org/fhir/spdx-license"), Description("gnuplot License")]
        Gnuplot,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GPL-1.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v1.0 only")]
        GPL1_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GPL-1.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v1.0 or later")]
        GPL1_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GPL-2.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v2.0 only")]
        GPL2_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GPL-2.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v2.0 or later")]
        GPL2_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GPL-3.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v3.0 only")]
        GPL3_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("GPL-3.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v3.0 or later")]
        GPL3_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("gSOAP-1.3b", "http://hl7.org/fhir/spdx-license"), Description("gSOAP Public License v1.3b")]
        GSOAP1_3b,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("HaskellReport", "http://hl7.org/fhir/spdx-license"), Description("Haskell Language Report License")]
        HaskellReport,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("HPND", "http://hl7.org/fhir/spdx-license"), Description("Historical Permission Notice and Disclaimer")]
        HPND,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("IBM-pibs", "http://hl7.org/fhir/spdx-license"), Description("IBM PowerPC Initialization and Boot Software")]
        IBMPibs,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ICU", "http://hl7.org/fhir/spdx-license"), Description("ICU License")]
        ICU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("IJG", "http://hl7.org/fhir/spdx-license"), Description("Independent JPEG Group License")]
        IJG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ImageMagick", "http://hl7.org/fhir/spdx-license"), Description("ImageMagick License")]
        ImageMagick,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("iMatix", "http://hl7.org/fhir/spdx-license"), Description("iMatix Standard Function Library Agreement")]
        IMatix,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Imlib2", "http://hl7.org/fhir/spdx-license"), Description("Imlib2 License")]
        Imlib2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Info-ZIP", "http://hl7.org/fhir/spdx-license"), Description("Info-ZIP License")]
        InfoZIP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Intel-ACPI", "http://hl7.org/fhir/spdx-license"), Description("Intel ACPI Software License Agreement")]
        IntelACPI,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Intel", "http://hl7.org/fhir/spdx-license"), Description("Intel Open Source License")]
        Intel,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Interbase-1.0", "http://hl7.org/fhir/spdx-license"), Description("Interbase Public License v1.0")]
        Interbase1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("IPA", "http://hl7.org/fhir/spdx-license"), Description("IPA Font License")]
        IPA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("IPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("IBM Public License v1.0")]
        IPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ISC", "http://hl7.org/fhir/spdx-license"), Description("ISC License")]
        ISC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("JasPer-2.0", "http://hl7.org/fhir/spdx-license"), Description("JasPer License")]
        JasPer2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("JSON", "http://hl7.org/fhir/spdx-license"), Description("JSON License")]
        JSON,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LAL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Licence Art Libre 1.2")]
        LAL1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LAL-1.3", "http://hl7.org/fhir/spdx-license"), Description("Licence Art Libre 1.3")]
        LAL1_3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Latex2e", "http://hl7.org/fhir/spdx-license"), Description("Latex2e License")]
        Latex2e,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Leptonica", "http://hl7.org/fhir/spdx-license"), Description("Leptonica License")]
        Leptonica,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPL-2.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Library General Public License v2 only")]
        LGPL2_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPL-2.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Library General Public License v2 or later")]
        LGPL2_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPL-2.1-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v2.1 only")]
        LGPL2_1Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPL-2.1-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v2.1 or later")]
        LGPL2_1OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPL-3.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v3.0 only")]
        LGPL3_0Only,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPL-3.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v3.0 or later")]
        LGPL3_0OrLater,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LGPLLR", "http://hl7.org/fhir/spdx-license"), Description("Lesser General Public License For Linguistic Resources")]
        LGPLLR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Libpng", "http://hl7.org/fhir/spdx-license"), Description("libpng License")]
        Libpng,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("libtiff", "http://hl7.org/fhir/spdx-license"), Description("libtiff License")]
        Libtiff,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LiLiQ-P-1.1", "http://hl7.org/fhir/spdx-license"), Description("Licence Libre du Québec – Permissive version 1.1")]
        LiLiQP1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LiLiQ-R-1.1", "http://hl7.org/fhir/spdx-license"), Description("Licence Libre du Québec – Réciprocité version 1.1")]
        LiLiQR1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LiLiQ-Rplus-1.1", "http://hl7.org/fhir/spdx-license"), Description("Licence Libre du Québec – Réciprocité forte version 1.1")]
        LiLiQRplus1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Linux-OpenIB", "http://hl7.org/fhir/spdx-license"), Description("Linux Kernel Variant of OpenIB.org license")]
        LinuxOpenIB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Lucent Public License Version 1.0")]
        LPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPL-1.02", "http://hl7.org/fhir/spdx-license"), Description("Lucent Public License v1.02")]
        LPL1_02,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.0")]
        LPPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.1")]
        LPPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPPL-1.2", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.2")]
        LPPL1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPPL-1.3a", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.3a")]
        LPPL1_3a,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("LPPL-1.3c", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.3c")]
        LPPL1_3c,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MakeIndex", "http://hl7.org/fhir/spdx-license"), Description("MakeIndex License")]
        MakeIndex,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MirOS", "http://hl7.org/fhir/spdx-license"), Description("MirOS License")]
        MirOS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MIT-0", "http://hl7.org/fhir/spdx-license"), Description("MIT No Attribution")]
        MIT0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MIT-advertising", "http://hl7.org/fhir/spdx-license"), Description("Enlightenment License (e16)")]
        MITAdvertising,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MIT-CMU", "http://hl7.org/fhir/spdx-license"), Description("CMU License")]
        MITCMU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MIT-enna", "http://hl7.org/fhir/spdx-license"), Description("enna License")]
        MITEnna,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MIT-feh", "http://hl7.org/fhir/spdx-license"), Description("feh License")]
        MITFeh,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MIT", "http://hl7.org/fhir/spdx-license"), Description("MIT License")]
        MIT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MITNFA", "http://hl7.org/fhir/spdx-license"), Description("MIT +no-false-attribs license")]
        MITNFA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Motosoto", "http://hl7.org/fhir/spdx-license"), Description("Motosoto License")]
        Motosoto,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("mpich2", "http://hl7.org/fhir/spdx-license"), Description("mpich2 License")]
        Mpich2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 1.0")]
        MPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 1.1")]
        MPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MPL-2.0-no-copyleft-exception", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 2.0 (no copyleft exception)")]
        MPL2_0NoCopyleftException,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 2.0")]
        MPL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MS-PL", "http://hl7.org/fhir/spdx-license"), Description("Microsoft Public License")]
        MSPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MS-RL", "http://hl7.org/fhir/spdx-license"), Description("Microsoft Reciprocal License")]
        MSRL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("MTLL", "http://hl7.org/fhir/spdx-license"), Description("Matrix Template Library License")]
        MTLL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Multics", "http://hl7.org/fhir/spdx-license"), Description("Multics License")]
        Multics,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Mup", "http://hl7.org/fhir/spdx-license"), Description("Mup License")]
        Mup,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NASA-1.3", "http://hl7.org/fhir/spdx-license"), Description("NASA Open Source Agreement 1.3")]
        NASA1_3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Naumen", "http://hl7.org/fhir/spdx-license"), Description("Naumen Public License")]
        Naumen,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NBPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Net Boolean Public License v1")]
        NBPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NCSA", "http://hl7.org/fhir/spdx-license"), Description("University of Illinois/NCSA Open Source License")]
        NCSA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Net-SNMP", "http://hl7.org/fhir/spdx-license"), Description("Net-SNMP License")]
        NetSNMP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NetCDF", "http://hl7.org/fhir/spdx-license"), Description("NetCDF license")]
        NetCDF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Newsletr", "http://hl7.org/fhir/spdx-license"), Description("Newsletr License")]
        Newsletr,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NGPL", "http://hl7.org/fhir/spdx-license"), Description("Nethack General Public License")]
        NGPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NLOD-1.0", "http://hl7.org/fhir/spdx-license"), Description("Norwegian Licence for Open Government Data")]
        NLOD1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NLPL", "http://hl7.org/fhir/spdx-license"), Description("No Limit Public License")]
        NLPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Nokia", "http://hl7.org/fhir/spdx-license"), Description("Nokia Open Source License")]
        Nokia,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NOSL", "http://hl7.org/fhir/spdx-license"), Description("Netizen Open Source License")]
        NOSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Noweb", "http://hl7.org/fhir/spdx-license"), Description("Noweb License")]
        Noweb,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Netscape Public License v1.0")]
        NPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Netscape Public License v1.1")]
        NPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NPOSL-3.0", "http://hl7.org/fhir/spdx-license"), Description("Non-Profit Open Software License 3.0")]
        NPOSL3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NRL", "http://hl7.org/fhir/spdx-license"), Description("NRL License")]
        NRL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("NTP", "http://hl7.org/fhir/spdx-license"), Description("NTP License")]
        NTP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OCCT-PL", "http://hl7.org/fhir/spdx-license"), Description("Open CASCADE Technology Public License")]
        OCCTPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OCLC-2.0", "http://hl7.org/fhir/spdx-license"), Description("OCLC Research Public License 2.0")]
        OCLC2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ODbL-1.0", "http://hl7.org/fhir/spdx-license"), Description("ODC Open Database License v1.0")]
        ODbL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OFL-1.0", "http://hl7.org/fhir/spdx-license"), Description("SIL Open Font License 1.0")]
        OFL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OFL-1.1", "http://hl7.org/fhir/spdx-license"), Description("SIL Open Font License 1.1")]
        OFL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OGTSL", "http://hl7.org/fhir/spdx-license"), Description("Open Group Test Suite License")]
        OGTSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-1.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.1")]
        OLDAP1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-1.2", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.2")]
        OLDAP1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-1.3", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.3")]
        OLDAP1_3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-1.4", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.4")]
        OLDAP1_4,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.0.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.0.1")]
        OLDAP2_0_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.0", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.0 (or possibly 2.0A and 2.0B)")]
        OLDAP2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.1")]
        OLDAP2_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.2.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.2.1")]
        OLDAP2_2_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.2.2", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License 2.2.2")]
        OLDAP2_2_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.2", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.2")]
        OLDAP2_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.3", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.3")]
        OLDAP2_3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.4", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.4")]
        OLDAP2_4,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.5", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.5")]
        OLDAP2_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.6", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.6")]
        OLDAP2_6,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.7", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.7")]
        OLDAP2_7,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OLDAP-2.8", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.8")]
        OLDAP2_8,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OML", "http://hl7.org/fhir/spdx-license"), Description("Open Market License")]
        OML,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OpenSSL", "http://hl7.org/fhir/spdx-license"), Description("OpenSSL License")]
        OpenSSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Open Public License v1.0")]
        OPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OSET-PL-2.1", "http://hl7.org/fhir/spdx-license"), Description("OSET Public License version 2.1")]
        OSETPL2_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 1.0")]
        OSL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OSL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 1.1")]
        OSL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OSL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 2.0")]
        OSL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OSL-2.1", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 2.1")]
        OSL2_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("OSL-3.0", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 3.0")]
        OSL3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("PDDL-1.0", "http://hl7.org/fhir/spdx-license"), Description("ODC Public Domain Dedication & License 1.0")]
        PDDL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("PHP-3.0", "http://hl7.org/fhir/spdx-license"), Description("PHP License v3.0")]
        PHP3_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("PHP-3.01", "http://hl7.org/fhir/spdx-license"), Description("PHP License v3.01")]
        PHP3_01,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Plexus", "http://hl7.org/fhir/spdx-license"), Description("Plexus Classworlds License")]
        Plexus,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("PostgreSQL", "http://hl7.org/fhir/spdx-license"), Description("PostgreSQL License")]
        PostgreSQL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("psfrag", "http://hl7.org/fhir/spdx-license"), Description("psfrag License")]
        Psfrag,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("psutils", "http://hl7.org/fhir/spdx-license"), Description("psutils License")]
        Psutils,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Python-2.0", "http://hl7.org/fhir/spdx-license"), Description("Python License 2.0")]
        Python2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Qhull", "http://hl7.org/fhir/spdx-license"), Description("Qhull License")]
        Qhull,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("QPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Q Public License 1.0")]
        QPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Rdisc", "http://hl7.org/fhir/spdx-license"), Description("Rdisc License")]
        Rdisc,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("RHeCos-1.1", "http://hl7.org/fhir/spdx-license"), Description("Red Hat eCos Public License v1.1")]
        RHeCos1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("RPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Reciprocal Public License 1.1")]
        RPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("RPL-1.5", "http://hl7.org/fhir/spdx-license"), Description("Reciprocal Public License 1.5")]
        RPL1_5,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("RPSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("RealNetworks Public Source License v1.0")]
        RPSL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("RSA-MD", "http://hl7.org/fhir/spdx-license"), Description("RSA Message-Digest License")]
        RSAMD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("RSCPL", "http://hl7.org/fhir/spdx-license"), Description("Ricoh Source Code Public License")]
        RSCPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Ruby", "http://hl7.org/fhir/spdx-license"), Description("Ruby License")]
        Ruby,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SAX-PD", "http://hl7.org/fhir/spdx-license"), Description("Sax Public Domain Notice")]
        SAXPD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Saxpath", "http://hl7.org/fhir/spdx-license"), Description("Saxpath License")]
        Saxpath,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SCEA", "http://hl7.org/fhir/spdx-license"), Description("SCEA Shared Source License")]
        SCEA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Sendmail", "http://hl7.org/fhir/spdx-license"), Description("Sendmail License")]
        Sendmail,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SGI-B-1.0", "http://hl7.org/fhir/spdx-license"), Description("SGI Free Software License B v1.0")]
        SGIB1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SGI-B-1.1", "http://hl7.org/fhir/spdx-license"), Description("SGI Free Software License B v1.1")]
        SGIB1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SGI-B-2.0", "http://hl7.org/fhir/spdx-license"), Description("SGI Free Software License B v2.0")]
        SGIB2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SimPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Simple Public License 2.0")]
        SimPL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SISSL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Sun Industry Standards Source License v1.2")]
        SISSL1_2,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SISSL", "http://hl7.org/fhir/spdx-license"), Description("Sun Industry Standards Source License v1.1")]
        SISSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Sleepycat", "http://hl7.org/fhir/spdx-license"), Description("Sleepycat License")]
        Sleepycat,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SMLNJ", "http://hl7.org/fhir/spdx-license"), Description("Standard ML of New Jersey License")]
        SMLNJ,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SMPPL", "http://hl7.org/fhir/spdx-license"), Description("Secure Messaging Protocol Public License")]
        SMPPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SNIA", "http://hl7.org/fhir/spdx-license"), Description("SNIA Public License 1.1")]
        SNIA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Spencer-86", "http://hl7.org/fhir/spdx-license"), Description("Spencer License 86")]
        Spencer86,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Spencer-94", "http://hl7.org/fhir/spdx-license"), Description("Spencer License 94")]
        Spencer94,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Spencer-99", "http://hl7.org/fhir/spdx-license"), Description("Spencer License 99")]
        Spencer99,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Sun Public License v1.0")]
        SPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SugarCRM-1.1.3", "http://hl7.org/fhir/spdx-license"), Description("SugarCRM Public License v1.1.3")]
        SugarCRM1_1_3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("SWL", "http://hl7.org/fhir/spdx-license"), Description("Scheme Widget Library (SWL) Software License Agreement")]
        SWL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("TCL", "http://hl7.org/fhir/spdx-license"), Description("TCL/TK License")]
        TCL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("TCP-wrappers", "http://hl7.org/fhir/spdx-license"), Description("TCP Wrappers License")]
        TCPWrappers,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("TMate", "http://hl7.org/fhir/spdx-license"), Description("TMate Open Source License")]
        TMate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("TORQUE-1.1", "http://hl7.org/fhir/spdx-license"), Description("TORQUE v2.5+ Software License v1.1")]
        TORQUE1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("TOSL", "http://hl7.org/fhir/spdx-license"), Description("Trusster Open Source License")]
        TOSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Unicode-DFS-2015", "http://hl7.org/fhir/spdx-license"), Description("Unicode License Agreement - Data Files and Software (2015)")]
        UnicodeDFS2015,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Unicode-DFS-2016", "http://hl7.org/fhir/spdx-license"), Description("Unicode License Agreement - Data Files and Software (2016)")]
        UnicodeDFS2016,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Unicode-TOU", "http://hl7.org/fhir/spdx-license"), Description("Unicode Terms of Use")]
        UnicodeTOU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Unlicense", "http://hl7.org/fhir/spdx-license"), Description("The Unlicense")]
        Unlicense,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("UPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Universal Permissive License v1.0")]
        UPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Vim", "http://hl7.org/fhir/spdx-license"), Description("Vim License")]
        Vim,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("VOSTROM", "http://hl7.org/fhir/spdx-license"), Description("VOSTROM Public License for Open Source")]
        VOSTROM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("VSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Vovida Software License v1.0")]
        VSL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("W3C-19980720", "http://hl7.org/fhir/spdx-license"), Description("W3C Software Notice and License (1998-07-20)")]
        W3C19980720,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("W3C-20150513", "http://hl7.org/fhir/spdx-license"), Description("W3C Software Notice and Document License (2015-05-13)")]
        W3C20150513,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("W3C", "http://hl7.org/fhir/spdx-license"), Description("W3C Software Notice and License (2002-12-31)")]
        W3C,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Watcom-1.0", "http://hl7.org/fhir/spdx-license"), Description("Sybase Open Watcom Public License 1.0")]
        Watcom1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Wsuipa", "http://hl7.org/fhir/spdx-license"), Description("Wsuipa License")]
        Wsuipa,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("WTFPL", "http://hl7.org/fhir/spdx-license"), Description("Do What The F*ck You Want To Public License")]
        WTFPL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("X11", "http://hl7.org/fhir/spdx-license"), Description("X11 License")]
        X11,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Xerox", "http://hl7.org/fhir/spdx-license"), Description("Xerox License")]
        Xerox,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("XFree86-1.1", "http://hl7.org/fhir/spdx-license"), Description("XFree86 License 1.1")]
        XFree861_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("xinetd", "http://hl7.org/fhir/spdx-license"), Description("xinetd License")]
        Xinetd,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Xnet", "http://hl7.org/fhir/spdx-license"), Description("X.Net License")]
        Xnet,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("xpp", "http://hl7.org/fhir/spdx-license"), Description("XPP License")]
        Xpp,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("XSkat", "http://hl7.org/fhir/spdx-license"), Description("XSkat License")]
        XSkat,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("YPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Yahoo! Public License v1.0")]
        YPL1_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("YPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Yahoo! Public License v1.1")]
        YPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Zed", "http://hl7.org/fhir/spdx-license"), Description("Zed License")]
        Zed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Zend-2.0", "http://hl7.org/fhir/spdx-license"), Description("Zend License v2.0")]
        Zend2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Zimbra-1.3", "http://hl7.org/fhir/spdx-license"), Description("Zimbra Public License v1.3")]
        Zimbra1_3,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Zimbra-1.4", "http://hl7.org/fhir/spdx-license"), Description("Zimbra Public License v1.4")]
        Zimbra1_4,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("zlib-acknowledgement", "http://hl7.org/fhir/spdx-license"), Description("zlib/libpng License with Acknowledgement")]
        ZlibAcknowledgement,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("Zlib", "http://hl7.org/fhir/spdx-license"), Description("zlib License")]
        Zlib,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ZPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Zope Public License 1.1")]
        ZPL1_1,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ZPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Zope Public License 2.0")]
        ZPL2_0,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/spdx-license)
        /// </summary>
        [EnumLiteral("ZPL-2.1", "http://hl7.org/fhir/spdx-license"), Description("Zope Public License 2.1")]
        ZPL2_1,
    }

    /// <summary>
    /// A code that indicates how the page is generated.
    /// (url: http://hl7.org/fhir/ValueSet/guide-page-generation)
    /// </summary>
    [FhirEnumeration("GuidePageGeneration")]
    public enum GuidePageGeneration
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-page-generation)
        /// </summary>
        [EnumLiteral("html", "http://hl7.org/fhir/guide-page-generation"), Description("HTML")]
        Html,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-page-generation)
        /// </summary>
        [EnumLiteral("markdown", "http://hl7.org/fhir/guide-page-generation"), Description("Markdown")]
        Markdown,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-page-generation)
        /// </summary>
        [EnumLiteral("xml", "http://hl7.org/fhir/guide-page-generation"), Description("XML")]
        Xml,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-page-generation)
        /// </summary>
        [EnumLiteral("generated", "http://hl7.org/fhir/guide-page-generation"), Description("Generated")]
        Generated,
    }

    /// <summary>
    /// Code of parameter that is input to the guide.
    /// (url: http://hl7.org/fhir/ValueSet/guide-parameter-code)
    /// </summary>
    [FhirEnumeration("GuideParameterCode")]
    public enum GuideParameterCode
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("apply", "http://hl7.org/fhir/guide-parameter-code"), Description("Apply Metadata Value")]
        Apply,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("path-resource", "http://hl7.org/fhir/guide-parameter-code"), Description("Resource Path")]
        PathResource,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("path-pages", "http://hl7.org/fhir/guide-parameter-code"), Description("Pages Path")]
        PathPages,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("path-tx-cache", "http://hl7.org/fhir/guide-parameter-code"), Description("Terminology Cache Path")]
        PathTxCache,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("expansion-parameter", "http://hl7.org/fhir/guide-parameter-code"), Description("Expansion Profile")]
        ExpansionParameter,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("rule-broken-links", "http://hl7.org/fhir/guide-parameter-code"), Description("Broken Links Rule")]
        RuleBrokenLinks,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("generate-xml", "http://hl7.org/fhir/guide-parameter-code"), Description("Generate XML")]
        GenerateXml,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("generate-json", "http://hl7.org/fhir/guide-parameter-code"), Description("Generate JSON")]
        GenerateJson,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("generate-turtle", "http://hl7.org/fhir/guide-parameter-code"), Description("Generate Turtle")]
        GenerateTurtle,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/guide-parameter-code)
        /// </summary>
        [EnumLiteral("html-template", "http://hl7.org/fhir/guide-parameter-code"), Description("HTML Template")]
        HtmlTemplate,
    }

    /// <summary>
    /// Codes identifying the lifecycle stage of an Invoice.
    /// (url: http://hl7.org/fhir/ValueSet/invoice-status)
    /// </summary>
    [FhirEnumeration("InvoiceStatus")]
    public enum InvoiceStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/invoice-status"), Description("draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-status)
        /// </summary>
        [EnumLiteral("issued", "http://hl7.org/fhir/invoice-status"), Description("issued")]
        Issued,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-status)
        /// </summary>
        [EnumLiteral("balanced", "http://hl7.org/fhir/invoice-status"), Description("balanced")]
        Balanced,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/invoice-status"), Description("cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/invoice-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/invoice-status"), Description("entered in error")]
        EnteredInError,
    }

    /// <summary>
    /// The type of the measure report.
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
        [EnumLiteral("subject-list", "http://hl7.org/fhir/measure-report-type"), Description("Subject List")]
        SubjectList,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-type)
        /// </summary>
        [EnumLiteral("summary", "http://hl7.org/fhir/measure-report-type"), Description("Summary")]
        Summary,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/measure-report-type)
        /// </summary>
        [EnumLiteral("data-collection", "http://hl7.org/fhir/measure-report-type"), Description("Data Collection")]
        DataCollection,
    }

    /// <summary>
    /// Medication Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/medication-status)
    /// </summary>
    [FhirEnumeration("MedicationStatusCodes")]
    public enum MedicationStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/CodeSystem/medication-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-status)
        /// </summary>
        [EnumLiteral("inactive", "http://hl7.org/fhir/CodeSystem/medication-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/CodeSystem/medication-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// MedicationAdministration Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/medication-admin-status)
    /// </summary>
    [FhirEnumeration("MedicationAdministrationStatusCodes")]
    public enum MedicationAdministrationStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("not-done", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Not Done")]
        NotDone,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("completed", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("stopped", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medication-admin-status)
        /// </summary>
        [EnumLiteral("unknown", "http://terminology.hl7.org/CodeSystem/medication-admin-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// MedicationDispense Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/medicationdispense-status)
    /// </summary>
    [FhirEnumeration("MedicationDispenseStatusCodes")]
    public enum MedicationDispenseStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("preparation", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Preparation")]
        Preparation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("in-progress", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("In Progress")]
        InProgress,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("completed", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("stopped", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("declined", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Declined")]
        Declined,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationdispense-status)
        /// </summary>
        [EnumLiteral("unknown", "http://terminology.hl7.org/CodeSystem/medicationdispense-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// MedicationKnowledge Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/medicationknowledge-status)
    /// </summary>
    [FhirEnumeration("MedicationKnowledgeStatusCodes")]
    public enum MedicationKnowledgeStatusCodes
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationknowledge-status)
        /// </summary>
        [EnumLiteral("active", "http://terminology.hl7.org/CodeSystem/medicationknowledge-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationknowledge-status)
        /// </summary>
        [EnumLiteral("inactive", "http://terminology.hl7.org/CodeSystem/medicationknowledge-status"), Description("Inactive")]
        Inactive,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/medicationknowledge-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://terminology.hl7.org/CodeSystem/medicationknowledge-status"), Description("Entered in Error")]
        EnteredInError,
    }

    /// <summary>
    /// MedicationRequest Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/medicationrequest-status)
    /// </summary>
    [FhirEnumeration("medicationrequestStatus")]
    public enum medicationrequestStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("cancelled", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Cancelled")]
        Cancelled,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("draft", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Draft")]
        Draft,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/CodeSystem/medicationrequest-status"), Description("Unknown")]
        Unknown,
    }

    /// <summary>
    /// MedicationRequest Intent Codes
    /// (url: http://hl7.org/fhir/ValueSet/medicationrequest-intent)
    /// </summary>
    [FhirEnumeration("medicationRequestIntent")]
    public enum medicationRequestIntent
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("proposal", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Proposal")]
        Proposal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("plan", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Plan")]
        Plan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("order", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Order")]
        Order,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("original-order", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Original Order")]
        OriginalOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("reflex-order", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Reflex Order")]
        ReflexOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("filler-order", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Filler Order")]
        FillerOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("instance-order", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Instance Order")]
        InstanceOrder,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medicationrequest-intent)
        /// </summary>
        [EnumLiteral("option", "http://hl7.org/fhir/CodeSystem/medicationrequest-intent"), Description("Option")]
        Option,
    }

    /// <summary>
    /// Medication Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/medication-statement-status)
    /// </summary>
    [FhirEnumeration("MedicationStatementStatus")]
    public enum MedicationStatementStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("completed", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Completed")]
        Completed,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("entered-in-error", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Entered in Error")]
        EnteredInError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("intended", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Intended")]
        Intended,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("stopped", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Stopped")]
        Stopped,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("on-hold", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("On Hold")]
        OnHold,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Unknown")]
        Unknown,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/medication-statement-status)
        /// </summary>
        [EnumLiteral("not-taken", "http://hl7.org/fhir/CodeSystem/medication-statement-status"), Description("Not Taken")]
        NotTaken,
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
        [EnumLiteral("consequence", "http://hl7.org/fhir/message-significance-category"), Description("Consequence")]
        Consequence,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("currency", "http://hl7.org/fhir/message-significance-category"), Description("Currency")]
        Currency,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/message-significance-category)
        /// </summary>
        [EnumLiteral("notification", "http://hl7.org/fhir/message-significance-category"), Description("Notification")]
        Notification,
    }

    /// <summary>
    /// HL7-defined table of codes which identify conditions under which acknowledgments are required to be returned in response to a message.
    /// (url: http://hl7.org/fhir/ValueSet/messageheader-response-request)
    /// </summary>
    [FhirEnumeration("messageheader_response_request")]
    public enum messageheader_response_request
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/messageheader-response-request)
        /// </summary>
        [EnumLiteral("always", "http://hl7.org/fhir/messageheader-response-request"), Description("Always")]
        Always,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/messageheader-response-request)
        /// </summary>
        [EnumLiteral("on-error", "http://hl7.org/fhir/messageheader-response-request"), Description("Error/reject conditions only")]
        OnError,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/messageheader-response-request)
        /// </summary>
        [EnumLiteral("never", "http://hl7.org/fhir/messageheader-response-request"), Description("Never")]
        Never,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/messageheader-response-request)
        /// </summary>
        [EnumLiteral("on-success", "http://hl7.org/fhir/messageheader-response-request"), Description("Successful completion only")]
        OnSuccess,
    }

    /// <summary>
    /// Type if a sequence -- DNA, RNA, or amino acid sequence.
    /// (url: http://hl7.org/fhir/ValueSet/sequence-type)
    /// </summary>
    [FhirEnumeration("sequenceType")]
    public enum sequenceType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/sequence-type)
        /// </summary>
        [EnumLiteral("aa", "http://hl7.org/fhir/sequence-type"), Description("AA Sequence")]
        Aa,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/sequence-type)
        /// </summary>
        [EnumLiteral("dna", "http://hl7.org/fhir/sequence-type"), Description("DNA Sequence")]
        Dna,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/sequence-type)
        /// </summary>
        [EnumLiteral("rna", "http://hl7.org/fhir/sequence-type"), Description("RNA Sequence")]
        Rna,
    }

    /// <summary>
    /// Type for orientation.
    /// (url: http://hl7.org/fhir/ValueSet/orientation-type)
    /// </summary>
    [FhirEnumeration("orientationType")]
    public enum orientationType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/orientation-type)
        /// </summary>
        [EnumLiteral("sense", "http://hl7.org/fhir/orientation-type"), Description("Sense orientation of referenceSeq")]
        Sense,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/orientation-type)
        /// </summary>
        [EnumLiteral("antisense", "http://hl7.org/fhir/orientation-type"), Description("Antisense orientation of referenceSeq")]
        Antisense,
    }

    /// <summary>
    /// Type for strand.
    /// (url: http://hl7.org/fhir/ValueSet/strand-type)
    /// </summary>
    [FhirEnumeration("strandType")]
    public enum strandType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/strand-type)
        /// </summary>
        [EnumLiteral("watson", "http://hl7.org/fhir/strand-type"), Description("Watson strand of referenceSeq")]
        Watson,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/strand-type)
        /// </summary>
        [EnumLiteral("crick", "http://hl7.org/fhir/strand-type"), Description("Crick strand of referenceSeq")]
        Crick,
    }

    /// <summary>
    /// Permitted data type for observation value.
    /// (url: http://hl7.org/fhir/ValueSet/permitted-data-type)
    /// </summary>
    [FhirEnumeration("ObservationDataType")]
    public enum ObservationDataType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("Quantity", "http://hl7.org/fhir/permitted-data-type"), Description("Quantity")]
        Quantity,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("CodeableConcept", "http://hl7.org/fhir/permitted-data-type"), Description("CodeableConcept")]
        CodeableConcept,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("string", "http://hl7.org/fhir/permitted-data-type"), Description("string")]
        String,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("boolean", "http://hl7.org/fhir/permitted-data-type"), Description("boolean")]
        Boolean,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("integer", "http://hl7.org/fhir/permitted-data-type"), Description("integer")]
        Integer,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("Range", "http://hl7.org/fhir/permitted-data-type"), Description("Range")]
        Range,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("Ratio", "http://hl7.org/fhir/permitted-data-type"), Description("Ratio")]
        Ratio,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("SampledData", "http://hl7.org/fhir/permitted-data-type"), Description("SampledData")]
        SampledData,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("time", "http://hl7.org/fhir/permitted-data-type"), Description("time")]
        Time,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("dateTime", "http://hl7.org/fhir/permitted-data-type"), Description("dateTime")]
        DateTime,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/permitted-data-type)
        /// </summary>
        [EnumLiteral("Period", "http://hl7.org/fhir/permitted-data-type"), Description("Period")]
        Period,
    }

    /// <summary>
    /// Codes identifying the category of observation range.
    /// (url: http://hl7.org/fhir/ValueSet/observation-range-category)
    /// </summary>
    [FhirEnumeration("ObservationRangeCategory")]
    public enum ObservationRangeCategory
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-range-category)
        /// </summary>
        [EnumLiteral("reference", "http://hl7.org/fhir/observation-range-category"), Description("reference range")]
        Reference,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-range-category)
        /// </summary>
        [EnumLiteral("critical", "http://hl7.org/fhir/observation-range-category"), Description("critical range")]
        Critical,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/observation-range-category)
        /// </summary>
        [EnumLiteral("absolute", "http://hl7.org/fhir/observation-range-category"), Description("absolute range")]
        Absolute,
    }

    /// <summary>
    /// A list of all the concrete types defined in this version of the FHIR specification - Abstract Types, Data Types and Resource Types.
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
        [EnumLiteral("Expression", "http://hl7.org/fhir/data-types"), Description("Expression")]
        Expression,
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
        [EnumLiteral("MarketingStatus", "http://hl7.org/fhir/data-types"), Description("MarketingStatus")]
        MarketingStatus,
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
        [EnumLiteral("MoneyQuantity", "http://hl7.org/fhir/data-types"), Description("MoneyQuantity")]
        MoneyQuantity,
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
        [EnumLiteral("Population", "http://hl7.org/fhir/data-types"), Description("Population")]
        Population,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ProdCharacteristic", "http://hl7.org/fhir/data-types"), Description("ProdCharacteristic")]
        ProdCharacteristic,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ProductShelfLife", "http://hl7.org/fhir/data-types"), Description("ProductShelfLife")]
        ProductShelfLife,
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
        [EnumLiteral("SubstanceAmount", "http://hl7.org/fhir/data-types"), Description("SubstanceAmount")]
        SubstanceAmount,
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
        [EnumLiteral("canonical", "http://hl7.org/fhir/data-types"), Description("canonical")]
        Canonical,
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
        [EnumLiteral("url", "http://hl7.org/fhir/data-types"), Description("url")]
        Url,
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
        [EnumLiteral("DeviceDefinition", "http://hl7.org/fhir/resource-types"), Description("DeviceDefinition")]
        DeviceDefinition,
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
        [EnumLiteral("EffectEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("EffectEvidenceSynthesis")]
        EffectEvidenceSynthesis,
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
        [EnumLiteral("ImmunizationEvaluation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationEvaluation")]
        ImmunizationEvaluation,
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
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
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
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
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
        [EnumLiteral("ObservationDefinition", "http://hl7.org/fhir/resource-types"), Description("ObservationDefinition")]
        ObservationDefinition,
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
        [EnumLiteral("OrganizationAffiliation", "http://hl7.org/fhir/resource-types"), Description("OrganizationAffiliation")]
        OrganizationAffiliation,
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
        [EnumLiteral("RiskEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("RiskEvidenceSynthesis")]
        RiskEvidenceSynthesis,
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
        [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/resource-types"), Description("ServiceRequest")]
        ServiceRequest,
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
        [EnumLiteral("SpecimenDefinition", "http://hl7.org/fhir/resource-types"), Description("SpecimenDefinition")]
        SpecimenDefinition,
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
        [EnumLiteral("VerificationResult", "http://hl7.org/fhir/resource-types"), Description("VerificationResult")]
        VerificationResult,
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
        [EnumLiteral("multiple-matches", "http://hl7.org/fhir/issue-type"), Description("Multiple Matches")]
        MultipleMatches,
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
        [EnumLiteral("deleted", "http://hl7.org/fhir/issue-type"), Description("Deleted")]
        Deleted,
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
        [EnumLiteral("incomplete", "http://hl7.org/fhir/issue-type"), Description("Incomplete Results")]
        Incomplete,
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
    /// The criteria by which a question is enabled.
    /// (url: http://hl7.org/fhir/ValueSet/questionnaire-enable-operator)
    /// </summary>
    [FhirEnumeration("QuestionnaireItemOperator")]
    public enum QuestionnaireItemOperator
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral("exists", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Exists")]
        Exists,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral("=", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Equals")]
        Equal,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral("!=", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Not Equals")]
        NotEqual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral(">", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Greater Than")]
        GreaterThan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral("<", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Less Than")]
        LessThan,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral(">=", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Greater or Equals")]
        GreaterOrEqual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-operator)
        /// </summary>
        [EnumLiteral("<=", "http://hl7.org/fhir/questionnaire-enable-operator"), Description("Less or Equals")]
        LessOrEqual,
    }

    /// <summary>
    /// Controls how multiple enableWhen values are interpreted -  whether all or any must be true.
    /// (url: http://hl7.org/fhir/ValueSet/questionnaire-enable-behavior)
    /// </summary>
    [FhirEnumeration("EnableWhenBehavior")]
    public enum EnableWhenBehavior
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-behavior)
        /// </summary>
        [EnumLiteral("all", "http://hl7.org/fhir/questionnaire-enable-behavior"), Description("All")]
        All,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/questionnaire-enable-behavior)
        /// </summary>
        [EnumLiteral("any", "http://hl7.org/fhir/questionnaire-enable-behavior"), Description("Any")]
        Any,
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
    /// The possible types of research elements (E.g. Population, Exposure, Outcome).
    /// (url: http://hl7.org/fhir/ValueSet/research-element-type)
    /// </summary>
    [FhirEnumeration("ResearchElementType")]
    public enum ResearchElementType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-element-type)
        /// </summary>
        [EnumLiteral("population", "http://hl7.org/fhir/research-element-type"), Description("Population")]
        Population,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-element-type)
        /// </summary>
        [EnumLiteral("exposure", "http://hl7.org/fhir/research-element-type"), Description("Exposure")]
        Exposure,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-element-type)
        /// </summary>
        [EnumLiteral("outcome", "http://hl7.org/fhir/research-element-type"), Description("Outcome")]
        Outcome,
    }

    /// <summary>
    /// Codes that convey the current status of the research study.
    /// (url: http://hl7.org/fhir/ValueSet/research-study-status)
    /// </summary>
    [FhirEnumeration("ResearchStudyStatus")]
    public enum ResearchStudyStatus
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("active", "http://hl7.org/fhir/research-study-status"), Description("Active")]
        Active,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("administratively-completed", "http://hl7.org/fhir/research-study-status"), Description("Administratively Completed")]
        AdministrativelyCompleted,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("approved", "http://hl7.org/fhir/research-study-status"), Description("Approved")]
        Approved,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("closed-to-accrual", "http://hl7.org/fhir/research-study-status"), Description("Closed to Accrual")]
        ClosedToAccrual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("closed-to-accrual-and-intervention", "http://hl7.org/fhir/research-study-status"), Description("Closed to Accrual and Intervention")]
        ClosedToAccrualAndIntervention,
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
        [EnumLiteral("disapproved", "http://hl7.org/fhir/research-study-status"), Description("Disapproved")]
        Disapproved,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("in-review", "http://hl7.org/fhir/research-study-status"), Description("In Review")]
        InReview,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("temporarily-closed-to-accrual", "http://hl7.org/fhir/research-study-status"), Description("Temporarily Closed to Accrual")]
        TemporarilyClosedToAccrual,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("temporarily-closed-to-accrual-and-intervention", "http://hl7.org/fhir/research-study-status"), Description("Temporarily Closed to Accrual and Intervention")]
        TemporarilyClosedToAccrualAndIntervention,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-study-status)
        /// </summary>
        [EnumLiteral("withdrawn", "http://hl7.org/fhir/research-study-status"), Description("Withdrawn")]
        Withdrawn,
    }

    /// <summary>
    /// Indicates the progression of a study subject through a study.
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
        [EnumLiteral("eligible", "http://hl7.org/fhir/research-subject-status"), Description("Eligible")]
        Eligible,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("follow-up", "http://hl7.org/fhir/research-subject-status"), Description("Follow-up")]
        FollowUp,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("ineligible", "http://hl7.org/fhir/research-subject-status"), Description("Ineligible")]
        Ineligible,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("not-registered", "http://hl7.org/fhir/research-subject-status"), Description("Not Registered")]
        NotRegistered,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("off-study", "http://hl7.org/fhir/research-subject-status"), Description("Off-study")]
        OffStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("on-study", "http://hl7.org/fhir/research-subject-status"), Description("On-study")]
        OnStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("on-study-intervention", "http://hl7.org/fhir/research-subject-status"), Description("On-study-intervention")]
        OnStudyIntervention,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("on-study-observation", "http://hl7.org/fhir/research-subject-status"), Description("On-study-observation")]
        OnStudyObservation,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("pending-on-study", "http://hl7.org/fhir/research-subject-status"), Description("Pending on-study")]
        PendingOnStudy,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("potential-candidate", "http://hl7.org/fhir/research-subject-status"), Description("Potential Candidate")]
        PotentialCandidate,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("screening", "http://hl7.org/fhir/research-subject-status"), Description("Screening")]
        Screening,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/research-subject-status)
        /// </summary>
        [EnumLiteral("withdrawn", "http://hl7.org/fhir/research-subject-status"), Description("Withdrawn")]
        Withdrawn,
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("identifier", "http://hl7.org/fhir/search-modifier-code"), Description("Identifier")]
        Identifier,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/search-modifier-code)
        /// </summary>
        [EnumLiteral("ofType", "http://hl7.org/fhir/search-modifier-code"), Description("Of Type")]
        OfType,
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
    /// Degree of preference of a type of conditioned specimen.
    /// (url: http://hl7.org/fhir/ValueSet/specimen-contained-preference)
    /// </summary>
    [FhirEnumeration("SpecimenContainedPreference")]
    public enum SpecimenContainedPreference
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/specimen-contained-preference)
        /// </summary>
        [EnumLiteral("preferred", "http://hl7.org/fhir/specimen-contained-preference"), Description("Preferred")]
        Preferred,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/specimen-contained-preference)
        /// </summary>
        [EnumLiteral("alternate", "http://hl7.org/fhir/specimen-contained-preference"), Description("Alternate")]
        Alternate,
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
        [EnumLiteral("logical", "http://hl7.org/fhir/structure-definition-kind"), Description("Logical")]
        Logical,
    }

    /// <summary>
    /// How an extension context is interpreted.
    /// (url: http://hl7.org/fhir/ValueSet/extension-context-type)
    /// </summary>
    [FhirEnumeration("ExtensionContextType")]
    public enum ExtensionContextType
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/extension-context-type)
        /// </summary>
        [EnumLiteral("fhirpath", "http://hl7.org/fhir/extension-context-type"), Description("FHIRPath")]
        Fhirpath,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/extension-context-type)
        /// </summary>
        [EnumLiteral("element", "http://hl7.org/fhir/extension-context-type"), Description("Element ID")]
        Element,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/extension-context-type)
        /// </summary>
        [EnumLiteral("extension", "http://hl7.org/fhir/extension-context-type"), Description("Extension URL")]
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
    /// Status of the supply request.
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
    /// Distinguishes whether the task is a proposal, plan or full order.
    /// (url: http://hl7.org/fhir/ValueSet/task-intent)
    /// </summary>
    [FhirEnumeration("TaskIntent")]
    public enum TaskIntent
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/task-intent)
        /// </summary>
        [EnumLiteral("unknown", "http://hl7.org/fhir/task-intent"), Description("Unknown")]
        Unknown,
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
    /// The degree to which the server supports the code search parameter on ValueSet, if it is supported.
    /// (url: http://hl7.org/fhir/ValueSet/code-search-support)
    /// </summary>
    [FhirEnumeration("CodeSearchSupport")]
    public enum CodeSearchSupport
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/code-search-support)
        /// </summary>
        [EnumLiteral("explicit", "http://hl7.org/fhir/code-search-support"), Description("Explicit Codes")]
        Explicit,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/code-search-support)
        /// </summary>
        [EnumLiteral("all", "http://hl7.org/fhir/code-search-support"), Description("Implicit Codes")]
        All,
    }

    /// <summary>
    /// A list of all the concrete types defined in this version of the FHIR specification - Data Types and Resource Types.
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
        [EnumLiteral("Expression", "http://hl7.org/fhir/data-types"), Description("Expression")]
        Expression,
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
        [EnumLiteral("MarketingStatus", "http://hl7.org/fhir/data-types"), Description("MarketingStatus")]
        MarketingStatus,
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
        [EnumLiteral("MoneyQuantity", "http://hl7.org/fhir/data-types"), Description("MoneyQuantity")]
        MoneyQuantity,
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
        [EnumLiteral("Population", "http://hl7.org/fhir/data-types"), Description("Population")]
        Population,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ProdCharacteristic", "http://hl7.org/fhir/data-types"), Description("ProdCharacteristic")]
        ProdCharacteristic,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/data-types)
        /// </summary>
        [EnumLiteral("ProductShelfLife", "http://hl7.org/fhir/data-types"), Description("ProductShelfLife")]
        ProductShelfLife,
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
        [EnumLiteral("SubstanceAmount", "http://hl7.org/fhir/data-types"), Description("SubstanceAmount")]
        SubstanceAmount,
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
        [EnumLiteral("canonical", "http://hl7.org/fhir/data-types"), Description("canonical")]
        Canonical,
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
        [EnumLiteral("url", "http://hl7.org/fhir/data-types"), Description("url")]
        Url,
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
        [EnumLiteral("DeviceDefinition", "http://hl7.org/fhir/resource-types"), Description("DeviceDefinition")]
        DeviceDefinition,
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
        [EnumLiteral("EffectEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("EffectEvidenceSynthesis")]
        EffectEvidenceSynthesis,
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
        [EnumLiteral("ImmunizationEvaluation", "http://hl7.org/fhir/resource-types"), Description("ImmunizationEvaluation")]
        ImmunizationEvaluation,
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
        [EnumLiteral("MedicationStatement", "http://hl7.org/fhir/resource-types"), Description("MedicationStatement")]
        MedicationStatement,
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
        [EnumLiteral("MessageHeader", "http://hl7.org/fhir/resource-types"), Description("MessageHeader")]
        MessageHeader,
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
        [EnumLiteral("ObservationDefinition", "http://hl7.org/fhir/resource-types"), Description("ObservationDefinition")]
        ObservationDefinition,
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
        [EnumLiteral("OrganizationAffiliation", "http://hl7.org/fhir/resource-types"), Description("OrganizationAffiliation")]
        OrganizationAffiliation,
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
        [EnumLiteral("RiskEvidenceSynthesis", "http://hl7.org/fhir/resource-types"), Description("RiskEvidenceSynthesis")]
        RiskEvidenceSynthesis,
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
        [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/resource-types"), Description("ServiceRequest")]
        ServiceRequest,
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
        [EnumLiteral("SpecimenDefinition", "http://hl7.org/fhir/resource-types"), Description("SpecimenDefinition")]
        SpecimenDefinition,
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
        [EnumLiteral("VerificationResult", "http://hl7.org/fhir/resource-types"), Description("VerificationResult")]
        VerificationResult,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/resource-types)
        /// </summary>
        [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/resource-types"), Description("VisionPrescription")]
        VisionPrescription,
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/http-operations)
        /// </summary>
        [EnumLiteral("head", "http://hl7.org/fhir/http-operations"), Description("HEAD")]
        Head,
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
    /// The validation status of the target
    /// (url: http://hl7.org/fhir/ValueSet/verificationresult-status)
    /// </summary>
    [FhirEnumeration("status")]
    public enum status
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/status)
        /// </summary>
        [EnumLiteral("attested", "http://hl7.org/fhir/CodeSystem/status"), Description("Attested")]
        Attested,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/status)
        /// </summary>
        [EnumLiteral("validated", "http://hl7.org/fhir/CodeSystem/status"), Description("Validated")]
        Validated,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/status)
        /// </summary>
        [EnumLiteral("in-process", "http://hl7.org/fhir/CodeSystem/status"), Description("In process")]
        InProcess,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/status)
        /// </summary>
        [EnumLiteral("req-revalid", "http://hl7.org/fhir/CodeSystem/status"), Description("Requires revalidation")]
        ReqRevalid,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/status)
        /// </summary>
        [EnumLiteral("val-fail", "http://hl7.org/fhir/CodeSystem/status"), Description("Validation failed")]
        ValFail,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/CodeSystem/status)
        /// </summary>
        [EnumLiteral("reval-fail", "http://hl7.org/fhir/CodeSystem/status"), Description("Re-Validation failed")]
        RevalFail,
    }

    /// <summary>
    /// The use of an address.
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/address-use)
        /// </summary>
        [EnumLiteral("billing", "http://hl7.org/fhir/address-use"), Description("Billing")]
        Billing,
    }

    /// <summary>
    /// Telecommunications form for contact point.
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
    /// The possible sort directions, ascending or descending.
    /// (url: http://hl7.org/fhir/ValueSet/sort-direction)
    /// </summary>
    [FhirEnumeration("SortDirection")]
    public enum SortDirection
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/sort-direction)
        /// </summary>
        [EnumLiteral("ascending", "http://hl7.org/fhir/sort-direction"), Description("Ascending")]
        Ascending,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/sort-direction)
        /// </summary>
        [EnumLiteral("descending", "http://hl7.org/fhir/sort-direction"), Description("Descending")]
        Descending,
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
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/identifier-use)
        /// </summary>
        [EnumLiteral("old", "http://hl7.org/fhir/identifier-use"), Description("Old")]
        Old,
    }

    /// <summary>
    /// Currency codes from ISO 4217 (see https://www.iso.org/iso-4217-currency-codes.html)
    /// (url: http://hl7.org/fhir/ValueSet/currencies)
    /// </summary>
    [FhirEnumeration("Currencies")]
    public enum Currencies
    {
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AED", "urn:iso:std:iso:4217"), Description("United Arab Emirates dirham")]
        AED,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AFN", "urn:iso:std:iso:4217"), Description("Afghan afghani")]
        AFN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ALL", "urn:iso:std:iso:4217"), Description("Albanian lek")]
        ALL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AMD", "urn:iso:std:iso:4217"), Description("Armenian dram")]
        AMD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ANG", "urn:iso:std:iso:4217"), Description("Netherlands Antillean guilder")]
        ANG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AOA", "urn:iso:std:iso:4217"), Description("Angolan kwanza")]
        AOA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ARS", "urn:iso:std:iso:4217"), Description("Argentine peso")]
        ARS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AUD", "urn:iso:std:iso:4217"), Description("Australian dollar")]
        AUD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AWG", "urn:iso:std:iso:4217"), Description("Aruban florin")]
        AWG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("AZN", "urn:iso:std:iso:4217"), Description("Azerbaijani manat")]
        AZN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BAM", "urn:iso:std:iso:4217"), Description("Bosnia and Herzegovina convertible mark")]
        BAM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BBD", "urn:iso:std:iso:4217"), Description("Barbados dollar")]
        BBD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BDT", "urn:iso:std:iso:4217"), Description("Bangladeshi taka")]
        BDT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BGN", "urn:iso:std:iso:4217"), Description("Bulgarian lev")]
        BGN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BHD", "urn:iso:std:iso:4217"), Description("Bahraini dinar")]
        BHD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BIF", "urn:iso:std:iso:4217"), Description("Burundian franc")]
        BIF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BMD", "urn:iso:std:iso:4217"), Description("Bermudian dollar")]
        BMD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BND", "urn:iso:std:iso:4217"), Description("Brunei dollar")]
        BND,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BOB", "urn:iso:std:iso:4217"), Description("Boliviano")]
        BOB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BOV", "urn:iso:std:iso:4217"), Description("Bolivian Mvdol (funds code)")]
        BOV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BRL", "urn:iso:std:iso:4217"), Description("Brazilian real")]
        BRL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BSD", "urn:iso:std:iso:4217"), Description("Bahamian dollar")]
        BSD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BTN", "urn:iso:std:iso:4217"), Description("Bhutanese ngultrum")]
        BTN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BWP", "urn:iso:std:iso:4217"), Description("Botswana pula")]
        BWP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BYN", "urn:iso:std:iso:4217"), Description("Belarusian ruble")]
        BYN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("BZD", "urn:iso:std:iso:4217"), Description("Belize dollar")]
        BZD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CAD", "urn:iso:std:iso:4217"), Description("Canadian dollar")]
        CAD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CDF", "urn:iso:std:iso:4217"), Description("Congolese franc")]
        CDF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CHE", "urn:iso:std:iso:4217"), Description("WIR Euro (complementary currency)")]
        CHE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CHF", "urn:iso:std:iso:4217"), Description("Swiss franc")]
        CHF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CHW", "urn:iso:std:iso:4217"), Description("WIR Franc (complementary currency)")]
        CHW,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CLF", "urn:iso:std:iso:4217"), Description("Unidad de Fomento (funds code)")]
        CLF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CLP", "urn:iso:std:iso:4217"), Description("Chilean peso")]
        CLP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CNY", "urn:iso:std:iso:4217"), Description("Renminbi (Chinese) yuan[8]")]
        CNY,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("COP", "urn:iso:std:iso:4217"), Description("Colombian peso")]
        COP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("COU", "urn:iso:std:iso:4217"), Description("Unidad de Valor Real (UVR) (funds code)[9]")]
        COU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CRC", "urn:iso:std:iso:4217"), Description("Costa Rican colon")]
        CRC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CUC", "urn:iso:std:iso:4217"), Description("Cuban convertible peso")]
        CUC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CUP", "urn:iso:std:iso:4217"), Description("Cuban peso")]
        CUP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CVE", "urn:iso:std:iso:4217"), Description("Cape Verde escudo")]
        CVE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("CZK", "urn:iso:std:iso:4217"), Description("Czech koruna")]
        CZK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("DJF", "urn:iso:std:iso:4217"), Description("Djiboutian franc")]
        DJF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("DKK", "urn:iso:std:iso:4217"), Description("Danish krone")]
        DKK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("DOP", "urn:iso:std:iso:4217"), Description("Dominican peso")]
        DOP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("DZD", "urn:iso:std:iso:4217"), Description("Algerian dinar")]
        DZD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("EGP", "urn:iso:std:iso:4217"), Description("Egyptian pound")]
        EGP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ERN", "urn:iso:std:iso:4217"), Description("Eritrean nakfa")]
        ERN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ETB", "urn:iso:std:iso:4217"), Description("Ethiopian birr")]
        ETB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("EUR", "urn:iso:std:iso:4217"), Description("Euro")]
        EUR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("FJD", "urn:iso:std:iso:4217"), Description("Fiji dollar")]
        FJD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("FKP", "urn:iso:std:iso:4217"), Description("Falkland Islands pound")]
        FKP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GBP", "urn:iso:std:iso:4217"), Description("Pound sterling")]
        GBP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GEL", "urn:iso:std:iso:4217"), Description("Georgian lari")]
        GEL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GGP", "urn:iso:std:iso:4217"), Description("Guernsey Pound")]
        GGP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GHS", "urn:iso:std:iso:4217"), Description("Ghanaian cedi")]
        GHS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GIP", "urn:iso:std:iso:4217"), Description("Gibraltar pound")]
        GIP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GMD", "urn:iso:std:iso:4217"), Description("Gambian dalasi")]
        GMD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GNF", "urn:iso:std:iso:4217"), Description("Guinean franc")]
        GNF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GTQ", "urn:iso:std:iso:4217"), Description("Guatemalan quetzal")]
        GTQ,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("GYD", "urn:iso:std:iso:4217"), Description("Guyanese dollar")]
        GYD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("HKD", "urn:iso:std:iso:4217"), Description("Hong Kong dollar")]
        HKD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("HNL", "urn:iso:std:iso:4217"), Description("Honduran lempira")]
        HNL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("HRK", "urn:iso:std:iso:4217"), Description("Croatian kuna")]
        HRK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("HTG", "urn:iso:std:iso:4217"), Description("Haitian gourde")]
        HTG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("HUF", "urn:iso:std:iso:4217"), Description("Hungarian forint")]
        HUF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("IDR", "urn:iso:std:iso:4217"), Description("Indonesian rupiah")]
        IDR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ILS", "urn:iso:std:iso:4217"), Description("Israeli new shekel")]
        ILS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("IMP", "urn:iso:std:iso:4217"), Description("Isle of Man Pound")]
        IMP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("INR", "urn:iso:std:iso:4217"), Description("Indian rupee")]
        INR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("IQD", "urn:iso:std:iso:4217"), Description("Iraqi dinar")]
        IQD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("IRR", "urn:iso:std:iso:4217"), Description("Iranian rial")]
        IRR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ISK", "urn:iso:std:iso:4217"), Description("Icelandic króna")]
        ISK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("JEP", "urn:iso:std:iso:4217"), Description("Jersey Pound")]
        JEP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("JMD", "urn:iso:std:iso:4217"), Description("Jamaican dollar")]
        JMD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("JOD", "urn:iso:std:iso:4217"), Description("Jordanian dinar")]
        JOD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("JPY", "urn:iso:std:iso:4217"), Description("Japanese yen")]
        JPY,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KES", "urn:iso:std:iso:4217"), Description("Kenyan shilling")]
        KES,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KGS", "urn:iso:std:iso:4217"), Description("Kyrgyzstani som")]
        KGS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KHR", "urn:iso:std:iso:4217"), Description("Cambodian riel")]
        KHR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KMF", "urn:iso:std:iso:4217"), Description("Comoro franc")]
        KMF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KPW", "urn:iso:std:iso:4217"), Description("North Korean won")]
        KPW,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KRW", "urn:iso:std:iso:4217"), Description("South Korean won")]
        KRW,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KWD", "urn:iso:std:iso:4217"), Description("Kuwaiti dinar")]
        KWD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KYD", "urn:iso:std:iso:4217"), Description("Cayman Islands dollar")]
        KYD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("KZT", "urn:iso:std:iso:4217"), Description("Kazakhstani tenge")]
        KZT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("LAK", "urn:iso:std:iso:4217"), Description("Lao kip")]
        LAK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("LBP", "urn:iso:std:iso:4217"), Description("Lebanese pound")]
        LBP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("LKR", "urn:iso:std:iso:4217"), Description("Sri Lankan rupee")]
        LKR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("LRD", "urn:iso:std:iso:4217"), Description("Liberian dollar")]
        LRD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("LSL", "urn:iso:std:iso:4217"), Description("Lesotho loti")]
        LSL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("LYD", "urn:iso:std:iso:4217"), Description("Libyan dinar")]
        LYD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MAD", "urn:iso:std:iso:4217"), Description("Moroccan dirham")]
        MAD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MDL", "urn:iso:std:iso:4217"), Description("Moldovan leu")]
        MDL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MGA", "urn:iso:std:iso:4217"), Description("Malagasy ariary")]
        MGA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MKD", "urn:iso:std:iso:4217"), Description("Macedonian denar")]
        MKD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MMK", "urn:iso:std:iso:4217"), Description("Myanmar kyat")]
        MMK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MNT", "urn:iso:std:iso:4217"), Description("Mongolian tögrög")]
        MNT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MOP", "urn:iso:std:iso:4217"), Description("Macanese pataca")]
        MOP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MRU", "urn:iso:std:iso:4217"), Description("Mauritanian ouguiya")]
        MRU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MUR", "urn:iso:std:iso:4217"), Description("Mauritian rupee")]
        MUR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MVR", "urn:iso:std:iso:4217"), Description("Maldivian rufiyaa")]
        MVR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MWK", "urn:iso:std:iso:4217"), Description("Malawian kwacha")]
        MWK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MXN", "urn:iso:std:iso:4217"), Description("Mexican peso")]
        MXN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MXV", "urn:iso:std:iso:4217"), Description("Mexican Unidad de Inversion (UDI) (funds code)")]
        MXV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MYR", "urn:iso:std:iso:4217"), Description("Malaysian ringgit")]
        MYR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("MZN", "urn:iso:std:iso:4217"), Description("Mozambican metical")]
        MZN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("NAD", "urn:iso:std:iso:4217"), Description("Namibian dollar")]
        NAD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("NGN", "urn:iso:std:iso:4217"), Description("Nigerian naira")]
        NGN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("NIO", "urn:iso:std:iso:4217"), Description("Nicaraguan córdoba")]
        NIO,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("NOK", "urn:iso:std:iso:4217"), Description("Norwegian krone")]
        NOK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("NPR", "urn:iso:std:iso:4217"), Description("Nepalese rupee")]
        NPR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("NZD", "urn:iso:std:iso:4217"), Description("New Zealand dollar")]
        NZD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("OMR", "urn:iso:std:iso:4217"), Description("Omani rial")]
        OMR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PAB", "urn:iso:std:iso:4217"), Description("Panamanian balboa")]
        PAB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PEN", "urn:iso:std:iso:4217"), Description("Peruvian Sol")]
        PEN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PGK", "urn:iso:std:iso:4217"), Description("Papua New Guinean kina")]
        PGK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PHP", "urn:iso:std:iso:4217"), Description("Philippine piso[13]")]
        PHP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PKR", "urn:iso:std:iso:4217"), Description("Pakistani rupee")]
        PKR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PLN", "urn:iso:std:iso:4217"), Description("Polish złoty")]
        PLN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("PYG", "urn:iso:std:iso:4217"), Description("Paraguayan guaraní")]
        PYG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("QAR", "urn:iso:std:iso:4217"), Description("Qatari riyal")]
        QAR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("RON", "urn:iso:std:iso:4217"), Description("Romanian leu")]
        RON,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("RSD", "urn:iso:std:iso:4217"), Description("Serbian dinar")]
        RSD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("RUB", "urn:iso:std:iso:4217"), Description("Russian ruble")]
        RUB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("RWF", "urn:iso:std:iso:4217"), Description("Rwandan franc")]
        RWF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SAR", "urn:iso:std:iso:4217"), Description("Saudi riyal")]
        SAR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SBD", "urn:iso:std:iso:4217"), Description("Solomon Islands dollar")]
        SBD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SCR", "urn:iso:std:iso:4217"), Description("Seychelles rupee")]
        SCR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SDG", "urn:iso:std:iso:4217"), Description("Sudanese pound")]
        SDG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SEK", "urn:iso:std:iso:4217"), Description("Swedish krona/kronor")]
        SEK,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SGD", "urn:iso:std:iso:4217"), Description("Singapore dollar")]
        SGD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SHP", "urn:iso:std:iso:4217"), Description("Saint Helena pound")]
        SHP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SLL", "urn:iso:std:iso:4217"), Description("Sierra Leonean leone")]
        SLL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SOS", "urn:iso:std:iso:4217"), Description("Somali shilling")]
        SOS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SRD", "urn:iso:std:iso:4217"), Description("Surinamese dollar")]
        SRD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SSP", "urn:iso:std:iso:4217"), Description("South Sudanese pound")]
        SSP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("STN", "urn:iso:std:iso:4217"), Description("São Tomé and Príncipe dobra")]
        STN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SVC", "urn:iso:std:iso:4217"), Description("Salvadoran colón")]
        SVC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SYP", "urn:iso:std:iso:4217"), Description("Syrian pound")]
        SYP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("SZL", "urn:iso:std:iso:4217"), Description("Swazi lilangeni")]
        SZL,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("THB", "urn:iso:std:iso:4217"), Description("Thai baht")]
        THB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TJS", "urn:iso:std:iso:4217"), Description("Tajikistani somoni")]
        TJS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TMT", "urn:iso:std:iso:4217"), Description("Turkmenistan manat")]
        TMT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TND", "urn:iso:std:iso:4217"), Description("Tunisian dinar")]
        TND,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TOP", "urn:iso:std:iso:4217"), Description("Tongan paʻanga")]
        TOP,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TRY", "urn:iso:std:iso:4217"), Description("Turkish lira")]
        TRY,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TTD", "urn:iso:std:iso:4217"), Description("Trinidad and Tobago dollar")]
        TTD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TVD", "urn:iso:std:iso:4217"), Description("Tuvalu Dollar")]
        TVD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TWD", "urn:iso:std:iso:4217"), Description("New Taiwan dollar")]
        TWD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("TZS", "urn:iso:std:iso:4217"), Description("Tanzanian shilling")]
        TZS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("UAH", "urn:iso:std:iso:4217"), Description("Ukrainian hryvnia")]
        UAH,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("UGX", "urn:iso:std:iso:4217"), Description("Ugandan shilling")]
        UGX,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("USD", "urn:iso:std:iso:4217"), Description("United States dollar")]
        USD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("USN", "urn:iso:std:iso:4217"), Description("United States dollar (next day) (funds code)")]
        USN,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("UYI", "urn:iso:std:iso:4217"), Description("Uruguay Peso en Unidades Indexadas (URUIURUI) (funds code)")]
        UYI,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("UYU", "urn:iso:std:iso:4217"), Description("Uruguayan peso")]
        UYU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("UZS", "urn:iso:std:iso:4217"), Description("Uzbekistan som")]
        UZS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("VEF", "urn:iso:std:iso:4217"), Description("Venezuelan bolívar")]
        VEF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("VND", "urn:iso:std:iso:4217"), Description("Vietnamese đồng")]
        VND,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("VUV", "urn:iso:std:iso:4217"), Description("Vanuatu vatu")]
        VUV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("WST", "urn:iso:std:iso:4217"), Description("Samoan tala")]
        WST,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XAF", "urn:iso:std:iso:4217"), Description("CFA franc BEAC")]
        XAF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XAG", "urn:iso:std:iso:4217"), Description("Silver (one troy ounce)")]
        XAG,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XAU", "urn:iso:std:iso:4217"), Description("Gold (one troy ounce)")]
        XAU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XBA", "urn:iso:std:iso:4217"), Description("European Composite Unit (EURCO) (bond market unit)")]
        XBA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XBB", "urn:iso:std:iso:4217"), Description("European Monetary Unit (E.M.U.-6) (bond market unit)")]
        XBB,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XBC", "urn:iso:std:iso:4217"), Description("European Unit of Account 9 (E.U.A.-9) (bond market unit)")]
        XBC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XBD", "urn:iso:std:iso:4217"), Description("European Unit of Account 17 (E.U.A.-17) (bond market unit)")]
        XBD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XCD", "urn:iso:std:iso:4217"), Description("East Caribbean dollar")]
        XCD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XDR", "urn:iso:std:iso:4217"), Description("Special drawing rights")]
        XDR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XOF", "urn:iso:std:iso:4217"), Description("CFA franc BCEAO")]
        XOF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XPD", "urn:iso:std:iso:4217"), Description("Palladium (one troy ounce)")]
        XPD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XPF", "urn:iso:std:iso:4217"), Description("CFP franc (franc Pacifique)")]
        XPF,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XPT", "urn:iso:std:iso:4217"), Description("Platinum (one troy ounce)")]
        XPT,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XSU", "urn:iso:std:iso:4217"), Description("SUCRE")]
        XSU,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XTS", "urn:iso:std:iso:4217"), Description("Code reserved for testing purposes")]
        XTS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XUA", "urn:iso:std:iso:4217"), Description("ADB Unit of Account")]
        XUA,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("XXX", "urn:iso:std:iso:4217"), Description("No currency")]
        XXX,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("YER", "urn:iso:std:iso:4217"), Description("Yemeni rial")]
        YER,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ZAR", "urn:iso:std:iso:4217"), Description("South African rand")]
        ZAR,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ZMW", "urn:iso:std:iso:4217"), Description("Zambian kwacha")]
        ZMW,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: urn:iso:std:iso:4217)
        /// </summary>
        [EnumLiteral("ZWL", "urn:iso:std:iso:4217"), Description("Zimbabwean dollar A/10")]
        ZWL,
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
        [EnumLiteral("MORN.early", "http://hl7.org/fhir/event-timing"), Description("Early Morning")]
        MORN_early,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("MORN.late", "http://hl7.org/fhir/event-timing"), Description("Late Morning")]
        MORN_late,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("NOON", "http://hl7.org/fhir/event-timing"), Description("Noon")]
        NOON,
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
        [EnumLiteral("AFT.early", "http://hl7.org/fhir/event-timing"), Description("Early Afternoon")]
        AFT_early,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("AFT.late", "http://hl7.org/fhir/event-timing"), Description("Late Afternoon")]
        AFT_late,
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
        [EnumLiteral("EVE.early", "http://hl7.org/fhir/event-timing"), Description("Early Evening")]
        EVE_early,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://hl7.org/fhir/event-timing)
        /// </summary>
        [EnumLiteral("EVE.late", "http://hl7.org/fhir/event-timing"), Description("Late Evening")]
        EVE_late,
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
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("HS", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("HS")]
        HS,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("WAKE", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("WAKE")]
        WAKE,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("C", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("C")]
        C,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("CM", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("CM")]
        CM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("CD", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("CD")]
        CD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("CV", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("CV")]
        CV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("AC", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("AC")]
        AC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("ACM", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("ACM")]
        ACM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("ACD", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("ACD")]
        ACD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("ACV", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("ACV")]
        ACV,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("PC", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("PC")]
        PC,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("PCM", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("PCM")]
        PCM,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("PCD", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("PCD")]
        PCD,
        /// <summary>
        /// MISSING DESCRIPTION
        /// (system: http://terminology.hl7.org/CodeSystem/v3-TimingEvent)
        /// </summary>
        [EnumLiteral("PCV", "http://terminology.hl7.org/CodeSystem/v3-TimingEvent"), Description("PCV")]
        PCV,
    }

    /// <summary>
    /// The type of trigger.
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
        [EnumLiteral("data-changed", "http://hl7.org/fhir/trigger-type"), Description("Data Changed")]
        DataChanged,
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
        [EnumLiteral("data-modified", "http://hl7.org/fhir/trigger-type"), Description("Data Updated")]
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
