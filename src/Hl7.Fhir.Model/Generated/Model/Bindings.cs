using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Used to specify why the normally expected content of the data element is missing
    /// </summary>
    [FhirEnumeration("DataAbsentReason")]
    public enum DataAbsentReason
    {
        [EnumLiteral("unknown")]
        Unknown, // The value is not known
        [EnumLiteral("asked")]
        Asked, // The source human does not know the value
        [EnumLiteral("temp")]
        Temp, // There is reason to expect (from the workflow) that the value may become known
        [EnumLiteral("notasked")]
        Notasked, // The workflow didn't lead to this value being known
        [EnumLiteral("masked")]
        Masked, // The information is not available due to security, privacy or related reasons
        [EnumLiteral("unsupported")]
        Unsupported, // The source system wasn't capable of supporting this element
        [EnumLiteral("astext")]
        Astext, // The content of the data is represented in the resource narrative
        [EnumLiteral("error")]
        Error, // Some system or workflow process error means that the information is not available
    }
    
    /// <summary>
    /// A set of generally useful codes defined so they can be included in value sets
    /// </summary>
    [FhirEnumeration("SpecialValues")]
    public enum SpecialValues
    {
        [EnumLiteral("true")]
        True, // Boolean true
        [EnumLiteral("false")]
        False, // Boolean false
        [EnumLiteral("trace")]
        Trace, // The content is greater than zero, but too small to be quantified
        [EnumLiteral("sufficient")]
        Sufficient, // The specific quantity is not known, but is known to be non-zero and is not specified because it makes up the bulk of the material
        [EnumLiteral("withdrawn")]
        Withdrawn, // The value is no longer available
        [EnumLiteral("nil known")]
        NilKnown, // The are no known applicable values in this context
    }
    
    /// <summary>
    /// List of all supported FHIR Resources
    /// </summary>
    [FhirEnumeration("ResourceType")]
    public enum ResourceType
    {
        [EnumLiteral("Resource")]
        Resource, // The Resource resource
        [EnumLiteral("AdverseReaction")]
        AdverseReaction, // The AdverseReaction resource
        [EnumLiteral("Alert")]
        Alert, // The Alert resource
        [EnumLiteral("AllergyIntolerance")]
        AllergyIntolerance, // The AllergyIntolerance resource
        [EnumLiteral("Appointment")]
        Appointment, // The Appointment resource
        [EnumLiteral("AppointmentResponse")]
        AppointmentResponse, // The AppointmentResponse resource
        [EnumLiteral("Availability")]
        Availability, // The Availability resource
        [EnumLiteral("CarePlan")]
        CarePlan, // The CarePlan resource
        [EnumLiteral("Composition")]
        Composition, // The Composition resource
        [EnumLiteral("ConceptMap")]
        ConceptMap, // The ConceptMap resource
        [EnumLiteral("Condition")]
        Condition, // The Condition resource
        [EnumLiteral("Conformance")]
        Conformance, // The Conformance resource
        [EnumLiteral("Device")]
        Device, // The Device resource
        [EnumLiteral("DeviceObservationReport")]
        DeviceObservationReport, // The DeviceObservationReport resource
        [EnumLiteral("DiagnosticOrder")]
        DiagnosticOrder, // The DiagnosticOrder resource
        [EnumLiteral("DiagnosticReport")]
        DiagnosticReport, // The DiagnosticReport resource
        [EnumLiteral("DocumentManifest")]
        DocumentManifest, // The DocumentManifest resource
        [EnumLiteral("DocumentReference")]
        DocumentReference, // The DocumentReference resource
        [EnumLiteral("Encounter")]
        Encounter, // The Encounter resource
        [EnumLiteral("FamilyHistory")]
        FamilyHistory, // The FamilyHistory resource
        [EnumLiteral("Group")]
        Group, // The Group resource
        [EnumLiteral("ImagingStudy")]
        ImagingStudy, // The ImagingStudy resource
        [EnumLiteral("Immunization")]
        Immunization, // The Immunization resource
        [EnumLiteral("ImmunizationRecommendation")]
        ImmunizationRecommendation, // The ImmunizationRecommendation resource
        [EnumLiteral("List")]
        List, // The List resource
        [EnumLiteral("Location")]
        Location, // The Location resource
        [EnumLiteral("Media")]
        Media, // The Media resource
        [EnumLiteral("Medication")]
        Medication, // The Medication resource
        [EnumLiteral("MedicationAdministration")]
        MedicationAdministration, // The MedicationAdministration resource
        [EnumLiteral("MedicationDispense")]
        MedicationDispense, // The MedicationDispense resource
        [EnumLiteral("MedicationPrescription")]
        MedicationPrescription, // The MedicationPrescription resource
        [EnumLiteral("MedicationStatement")]
        MedicationStatement, // The MedicationStatement resource
        [EnumLiteral("MessageHeader")]
        MessageHeader, // The MessageHeader resource
        [EnumLiteral("Observation")]
        Observation, // The Observation resource
        [EnumLiteral("OperationOutcome")]
        OperationOutcome, // The OperationOutcome resource
        [EnumLiteral("Order")]
        Order, // The Order resource
        [EnumLiteral("OrderResponse")]
        OrderResponse, // The OrderResponse resource
        [EnumLiteral("Organization")]
        Organization, // The Organization resource
        [EnumLiteral("Other")]
        Other, // The Other resource
        [EnumLiteral("Patient")]
        Patient, // The Patient resource
        [EnumLiteral("Practitioner")]
        Practitioner, // The Practitioner resource
        [EnumLiteral("Procedure")]
        Procedure, // The Procedure resource
        [EnumLiteral("Profile")]
        Profile, // The Profile resource
        [EnumLiteral("Provenance")]
        Provenance, // The Provenance resource
        [EnumLiteral("Query")]
        Query, // The Query resource
        [EnumLiteral("Questionnaire")]
        Questionnaire, // The Questionnaire resource
        [EnumLiteral("Referral")]
        Referral, // The Referral resource
        [EnumLiteral("RelatedPerson")]
        RelatedPerson, // The RelatedPerson resource
        [EnumLiteral("SecurityEvent")]
        SecurityEvent, // The SecurityEvent resource
        [EnumLiteral("Slot")]
        Slot, // The Slot resource
        [EnumLiteral("Specimen")]
        Specimen, // The Specimen resource
        [EnumLiteral("Substance")]
        Substance, // The Substance resource
        [EnumLiteral("Supply")]
        Supply, // The Supply resource
        [EnumLiteral("ValueSet")]
        ValueSet, // The ValueSet resource
        [EnumLiteral("Binary")]
        Binary, // The Binary resource
    }
    
}
