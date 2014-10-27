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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Used to specify why the normally expected content of the data element is missing
    /// </summary>
    [FhirEnumeration("DataAbsentReason")]
    public enum DataAbsentReason
    {
        /// <summary>
        /// The value is not known
        /// </summary>
        [EnumLiteral("unknown")]
        Unknown,
        /// <summary>
        /// The source human does not know the value
        /// </summary>
        [EnumLiteral("asked")]
        Asked,
        /// <summary>
        /// There is reason to expect (from the workflow) that the value may become known
        /// </summary>
        [EnumLiteral("temp")]
        Temp,
        /// <summary>
        /// The workflow didn't lead to this value being known
        /// </summary>
        [EnumLiteral("notasked")]
        Notasked,
        /// <summary>
        /// The information is not available due to security, privacy or related reasons
        /// </summary>
        [EnumLiteral("masked")]
        Masked,
        /// <summary>
        /// The source system wasn't capable of supporting this element
        /// </summary>
        [EnumLiteral("unsupported")]
        Unsupported,
        /// <summary>
        /// The content of the data is represented in the resource narrative
        /// </summary>
        [EnumLiteral("astext")]
        Astext,
        /// <summary>
        /// Some system or workflow process error means that the information is not available
        /// </summary>
        [EnumLiteral("error")]
        Error,
    }
    
    /// <summary>
    /// A set of generally useful codes defined so they can be included in value sets
    /// </summary>
    [FhirEnumeration("SpecialValues")]
    public enum SpecialValues
    {
        /// <summary>
        /// Boolean true
        /// </summary>
        [EnumLiteral("true")]
        True,
        /// <summary>
        /// Boolean false
        /// </summary>
        [EnumLiteral("false")]
        False,
        /// <summary>
        /// The content is greater than zero, but too small to be quantified
        /// </summary>
        [EnumLiteral("trace")]
        Trace,
        /// <summary>
        /// The specific quantity is not known, but is known to be non-zero and is not specified because it makes up the bulk of the material
        /// </summary>
        [EnumLiteral("sufficient")]
        Sufficient,
        /// <summary>
        /// The value is no longer available
        /// </summary>
        [EnumLiteral("withdrawn")]
        Withdrawn,
        /// <summary>
        /// The are no known applicable values in this context
        /// </summary>
        [EnumLiteral("nil known")]
        NilKnown,
    }
    
    /// <summary>
    /// List of all supported FHIR Resources
    /// </summary>
    [FhirEnumeration("ResourceType")]
    public enum ResourceType
    {
        /// <summary>
        /// The Resource resource
        /// </summary>
        [EnumLiteral("Resource")]
        Resource,
        /// <summary>
        /// The AdverseReaction resource
        /// </summary>
        [EnumLiteral("AdverseReaction")]
        AdverseReaction,
        /// <summary>
        /// The Alert resource
        /// </summary>
        [EnumLiteral("Alert")]
        Alert,
        /// <summary>
        /// The AllergyIntolerance resource
        /// </summary>
        [EnumLiteral("AllergyIntolerance")]
        AllergyIntolerance,
        /// <summary>
        /// The CarePlan resource
        /// </summary>
        [EnumLiteral("CarePlan")]
        CarePlan,
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
        /// The Device resource
        /// </summary>
        [EnumLiteral("Device")]
        Device,
        /// <summary>
        /// The DeviceObservationReport resource
        /// </summary>
        [EnumLiteral("DeviceObservationReport")]
        DeviceObservationReport,
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
        /// The Encounter resource
        /// </summary>
        [EnumLiteral("Encounter")]
        Encounter,
        /// <summary>
        /// The FamilyHistory resource
        /// </summary>
        [EnumLiteral("FamilyHistory")]
        FamilyHistory,
        /// <summary>
        /// The Group resource
        /// </summary>
        [EnumLiteral("Group")]
        Group,
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
        /// The MedicationPrescription resource
        /// </summary>
        [EnumLiteral("MedicationPrescription")]
        MedicationPrescription,
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
        /// The Observation resource
        /// </summary>
        [EnumLiteral("Observation")]
        Observation,
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
        /// The Other resource
        /// </summary>
        [EnumLiteral("Other")]
        Other,
        /// <summary>
        /// The Patient resource
        /// </summary>
        [EnumLiteral("Patient")]
        Patient,
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
        /// The Profile resource
        /// </summary>
        [EnumLiteral("Profile")]
        Profile,
        /// <summary>
        /// The Provenance resource
        /// </summary>
        [EnumLiteral("Provenance")]
        Provenance,
        /// <summary>
        /// The Query resource
        /// </summary>
        [EnumLiteral("Query")]
        Query,
        /// <summary>
        /// The Questionnaire resource
        /// </summary>
        [EnumLiteral("Questionnaire")]
        Questionnaire,
        /// <summary>
        /// The RelatedPerson resource
        /// </summary>
        [EnumLiteral("RelatedPerson")]
        RelatedPerson,
        /// <summary>
        /// The SecurityEvent resource
        /// </summary>
        [EnumLiteral("SecurityEvent")]
        SecurityEvent,
        /// <summary>
        /// The Specimen resource
        /// </summary>
        [EnumLiteral("Specimen")]
        Specimen,
        /// <summary>
        /// The Substance resource
        /// </summary>
        [EnumLiteral("Substance")]
        Substance,
        /// <summary>
        /// The Supply resource
        /// </summary>
        [EnumLiteral("Supply")]
        Supply,
        /// <summary>
        /// The ValueSet resource
        /// </summary>
        [EnumLiteral("ValueSet")]
        ValueSet,
        /// <summary>
        /// The Binary resource
        /// </summary>
        [EnumLiteral("Binary")]
        Binary,
    }
    
}
