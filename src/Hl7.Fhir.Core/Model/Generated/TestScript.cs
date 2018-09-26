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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes a set of tests
    /// </summary>
    [FhirType("TestScript", IsResource=true)]
    [DataContract]
    public partial class TestScript : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TestScript; } }
        [NotMapped]
        public override string TypeName { get { return "TestScript"; } }
        
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
        /// The type of direction to use for assertion.
        /// (url: http://hl7.org/fhir/ValueSet/assert-direction-codes)
        /// </summary>
        [FhirEnumeration("AssertionDirectionType")]
        public enum AssertionDirectionType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-direction-codes)
            /// </summary>
            [EnumLiteral("response", "http://hl7.org/fhir/assert-direction-codes"), Description("response")]
            Response,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-direction-codes)
            /// </summary>
            [EnumLiteral("request", "http://hl7.org/fhir/assert-direction-codes"), Description("request")]
            Request,
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
        /// The type of response code to use for assertion.
        /// (url: http://hl7.org/fhir/ValueSet/assert-response-code-types)
        /// </summary>
        [FhirEnumeration("AssertionResponseTypes")]
        public enum AssertionResponseTypes
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("okay", "http://hl7.org/fhir/assert-response-code-types"), Description("okay")]
            Okay,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("created", "http://hl7.org/fhir/assert-response-code-types"), Description("created")]
            Created,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("noContent", "http://hl7.org/fhir/assert-response-code-types"), Description("noContent")]
            NoContent,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("notModified", "http://hl7.org/fhir/assert-response-code-types"), Description("notModified")]
            NotModified,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("bad", "http://hl7.org/fhir/assert-response-code-types"), Description("bad")]
            Bad,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("forbidden", "http://hl7.org/fhir/assert-response-code-types"), Description("forbidden")]
            Forbidden,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("notFound", "http://hl7.org/fhir/assert-response-code-types"), Description("notFound")]
            NotFound,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("methodNotAllowed", "http://hl7.org/fhir/assert-response-code-types"), Description("methodNotAllowed")]
            MethodNotAllowed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("conflict", "http://hl7.org/fhir/assert-response-code-types"), Description("conflict")]
            Conflict,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("gone", "http://hl7.org/fhir/assert-response-code-types"), Description("gone")]
            Gone,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("preconditionFailed", "http://hl7.org/fhir/assert-response-code-types"), Description("preconditionFailed")]
            PreconditionFailed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/assert-response-code-types)
            /// </summary>
            [EnumLiteral("unprocessable", "http://hl7.org/fhir/assert-response-code-types"), Description("unprocessable")]
            Unprocessable,
        }

        [FhirType("OriginComponent")]
        [DataContract]
        public partial class OriginComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "OriginComponent"; } }
            
            /// <summary>
            /// The index of the abstract origin server starting at 1
            /// </summary>
            [FhirElement("index", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer IndexElement
            {
                get { return _IndexElement; }
                set { _IndexElement = value; OnPropertyChanged("IndexElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _IndexElement;
            
            /// <summary>
            /// The index of the abstract origin server starting at 1
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Index
            {
                get { return IndexElement != null ? IndexElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        IndexElement = null; 
                    else
                        IndexElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Index");
                }
            }
            
            /// <summary>
            /// FHIR-Client | FHIR-SDC-FormFiller
            /// </summary>
            [FhirElement("profile", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.Coding _Profile;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OriginComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IndexElement != null) dest.IndexElement = (Hl7.Fhir.Model.Integer)IndexElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.Coding)Profile.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OriginComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OriginComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OriginComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IndexElement != null) yield return IndexElement;
                    if (Profile != null) yield return Profile;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IndexElement != null) yield return new ElementValue("index", IndexElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                }
            }

            
        }
        
        
        [FhirType("DestinationComponent")]
        [DataContract]
        public partial class DestinationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DestinationComponent"; } }
            
            /// <summary>
            /// The index of the abstract destination server starting at 1
            /// </summary>
            [FhirElement("index", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer IndexElement
            {
                get { return _IndexElement; }
                set { _IndexElement = value; OnPropertyChanged("IndexElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _IndexElement;
            
            /// <summary>
            /// The index of the abstract destination server starting at 1
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Index
            {
                get { return IndexElement != null ? IndexElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        IndexElement = null; 
                    else
                        IndexElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Index");
                }
            }
            
            /// <summary>
            /// FHIR-Server | FHIR-SDC-FormManager | FHIR-SDC-FormReceiver | FHIR-SDC-FormProcessor
            /// </summary>
            [FhirElement("profile", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.Coding _Profile;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DestinationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IndexElement != null) dest.IndexElement = (Hl7.Fhir.Model.Integer)IndexElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.Coding)Profile.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DestinationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DestinationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DestinationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IndexElement != null) yield return IndexElement;
                    if (Profile != null) yield return Profile;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IndexElement != null) yield return new ElementValue("index", IndexElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                }
            }

            
        }
        
        
        [FhirType("MetadataComponent")]
        [DataContract]
        public partial class MetadataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "MetadataComponent"; } }
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            [FhirElement("link", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.LinkComponent> Link
            {
                get { if(_Link==null) _Link = new List<Hl7.Fhir.Model.TestScript.LinkComponent>(); return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.LinkComponent> _Link;
            
            /// <summary>
            /// Capabilities  that are assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("capability", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.CapabilityComponent> Capability
            {
                get { if(_Capability==null) _Capability = new List<Hl7.Fhir.Model.TestScript.CapabilityComponent>(); return _Capability; }
                set { _Capability = value; OnPropertyChanged("Capability"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.CapabilityComponent> _Capability;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MetadataComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Link != null) dest.Link = new List<Hl7.Fhir.Model.TestScript.LinkComponent>(Link.DeepCopy());
                    if(Capability != null) dest.Capability = new List<Hl7.Fhir.Model.TestScript.CapabilityComponent>(Capability.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MetadataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MetadataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                if( !DeepComparable.Matches(Capability, otherT.Capability)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MetadataComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                if( !DeepComparable.IsExactly(Capability, otherT.Capability)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Link) { if (elem != null) yield return elem; }
                    foreach (var elem in Capability) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Link) { if (elem != null) yield return new ElementValue("link", elem); }
                    foreach (var elem in Capability) { if (elem != null) yield return new ElementValue("capability", elem); }
                }
            }

            
        }
        
        
        [FhirType("LinkComponent")]
        [DataContract]
        public partial class LinkComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "LinkComponent"; } }
            
            /// <summary>
            /// URL to the specification
            /// </summary>
            [FhirElement("url", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// URL to the specification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if (value == null)
                        UrlElement = null; 
                    else
                        UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// Short description
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Short description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LinkComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LinkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UrlElement != null) yield return UrlElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }

            
        }
        
        
        [FhirType("CapabilityComponent")]
        [DataContract]
        public partial class CapabilityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "CapabilityComponent"; } }
            
            /// <summary>
            /// Are the capabilities required?
            /// </summary>
            [FhirElement("required", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RequiredElement;
            
            /// <summary>
            /// Are the capabilities required?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        RequiredElement = null; 
                    else
                        RequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// Are the capabilities validated?
            /// </summary>
            [FhirElement("validated", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ValidatedElement
            {
                get { return _ValidatedElement; }
                set { _ValidatedElement = value; OnPropertyChanged("ValidatedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ValidatedElement;
            
            /// <summary>
            /// Are the capabilities validated?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Validated
            {
                get { return ValidatedElement != null ? ValidatedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ValidatedElement = null; 
                    else
                        ValidatedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Validated");
                }
            }
            
            /// <summary>
            /// The expected capabilities of the server
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// The expected capabilities of the server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Which origin server these requirements apply to
            /// </summary>
            [FhirElement("origin", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Integer> OriginElement
            {
                get { if(_OriginElement==null) _OriginElement = new List<Hl7.Fhir.Model.Integer>(); return _OriginElement; }
                set { _OriginElement = value; OnPropertyChanged("OriginElement"); }
            }
            
            private List<Hl7.Fhir.Model.Integer> _OriginElement;
            
            /// <summary>
            /// Which origin server these requirements apply to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> Origin
            {
                get { return OriginElement != null ? OriginElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OriginElement = null; 
                    else
                        OriginElement = new List<Hl7.Fhir.Model.Integer>(value.Select(elem=>new Hl7.Fhir.Model.Integer(elem)));
                    OnPropertyChanged("Origin");
                }
            }
            
            /// <summary>
            /// Which server these requirements apply to
            /// </summary>
            [FhirElement("destination", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Which server these requirements apply to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Destination
            {
                get { return DestinationElement != null ? DestinationElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DestinationElement = null; 
                    else
                        DestinationElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Destination");
                }
            }
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            [FhirElement("link", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> LinkElement
            {
                get { if(_LinkElement==null) _LinkElement = new List<Hl7.Fhir.Model.FhirUri>(); return _LinkElement; }
                set { _LinkElement = value; OnPropertyChanged("LinkElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _LinkElement;
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Link
            {
                get { return LinkElement != null ? LinkElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LinkElement = null; 
                    else
                        LinkElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Link");
                }
            }
            
            /// <summary>
            /// Required Capability Statement
            /// </summary>
            [FhirElement("capabilities", Order=100)]
            [CLSCompliant(false)]
			[References("CapabilityStatement")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Capabilities
            {
                get { return _Capabilities; }
                set { _Capabilities = value; OnPropertyChanged("Capabilities"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Capabilities;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CapabilityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                    if(ValidatedElement != null) dest.ValidatedElement = (Hl7.Fhir.Model.FhirBoolean)ValidatedElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OriginElement != null) dest.OriginElement = new List<Hl7.Fhir.Model.Integer>(OriginElement.DeepCopy());
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(LinkElement != null) dest.LinkElement = new List<Hl7.Fhir.Model.FhirUri>(LinkElement.DeepCopy());
                    if(Capabilities != null) dest.Capabilities = (Hl7.Fhir.Model.ResourceReference)Capabilities.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CapabilityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CapabilityComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(ValidatedElement, otherT.ValidatedElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.Matches(Capabilities, otherT.Capabilities)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CapabilityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(ValidatedElement, otherT.ValidatedElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.IsExactly(Capabilities, otherT.Capabilities)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RequiredElement != null) yield return RequiredElement;
                    if (ValidatedElement != null) yield return ValidatedElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in OriginElement) { if (elem != null) yield return elem; }
                    if (DestinationElement != null) yield return DestinationElement;
                    foreach (var elem in LinkElement) { if (elem != null) yield return elem; }
                    if (Capabilities != null) yield return Capabilities;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RequiredElement != null) yield return new ElementValue("required", RequiredElement);
                    if (ValidatedElement != null) yield return new ElementValue("validated", ValidatedElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in OriginElement) { if (elem != null) yield return new ElementValue("origin", elem); }
                    if (DestinationElement != null) yield return new ElementValue("destination", DestinationElement);
                    foreach (var elem in LinkElement) { if (elem != null) yield return new ElementValue("link", elem); }
                    if (Capabilities != null) yield return new ElementValue("capabilities", Capabilities);
                }
            }

            
        }
        
        
        [FhirType("FixtureComponent")]
        [DataContract]
        public partial class FixtureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "FixtureComponent"; } }
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            [FhirElement("autocreate", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AutocreateElement
            {
                get { return _AutocreateElement; }
                set { _AutocreateElement = value; OnPropertyChanged("AutocreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AutocreateElement;
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Autocreate
            {
                get { return AutocreateElement != null ? AutocreateElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AutocreateElement = null; 
                    else
                        AutocreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Autocreate");
                }
            }
            
            /// <summary>
            /// Whether or not to implicitly delete the fixture during teardown
            /// </summary>
            [FhirElement("autodelete", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AutodeleteElement
            {
                get { return _AutodeleteElement; }
                set { _AutodeleteElement = value; OnPropertyChanged("AutodeleteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AutodeleteElement;
            
            /// <summary>
            /// Whether or not to implicitly delete the fixture during teardown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Autodelete
            {
                get { return AutodeleteElement != null ? AutodeleteElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AutodeleteElement = null; 
                    else
                        AutodeleteElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Autodelete");
                }
            }
            
            /// <summary>
            /// Reference of the resource
            /// </summary>
            [FhirElement("resource", Order=60)]
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FixtureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AutocreateElement != null) dest.AutocreateElement = (Hl7.Fhir.Model.FhirBoolean)AutocreateElement.DeepCopy();
                    if(AutodeleteElement != null) dest.AutodeleteElement = (Hl7.Fhir.Model.FhirBoolean)AutodeleteElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FixtureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FixtureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.Matches(AutodeleteElement, otherT.AutodeleteElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FixtureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.IsExactly(AutodeleteElement, otherT.AutodeleteElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AutocreateElement != null) yield return AutocreateElement;
                    if (AutodeleteElement != null) yield return AutodeleteElement;
                    if (Resource != null) yield return Resource;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AutocreateElement != null) yield return new ElementValue("autocreate", AutocreateElement);
                    if (AutodeleteElement != null) yield return new ElementValue("autodelete", AutodeleteElement);
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                }
            }

            
        }
        
        
        [FhirType("VariableComponent")]
        [DataContract]
        public partial class VariableComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "VariableComponent"; } }
            
            /// <summary>
            /// Descriptive name for this variable
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Descriptive name for this variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Default, hard-coded, or user-defined value for this variable
            /// </summary>
            [FhirElement("defaultValue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DefaultValueElement
            {
                get { return _DefaultValueElement; }
                set { _DefaultValueElement = value; OnPropertyChanged("DefaultValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DefaultValueElement;
            
            /// <summary>
            /// Default, hard-coded, or user-defined value for this variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DefaultValue
            {
                get { return DefaultValueElement != null ? DefaultValueElement.Value : null; }
                set
                {
                    if (value == null)
                        DefaultValueElement = null; 
                    else
                        DefaultValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("DefaultValue");
                }
            }
            
            /// <summary>
            /// Natural language description of the variable
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Natural language description of the variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// The fluentpath expression against the fixture body
            /// </summary>
            [FhirElement("expression", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// The fluentpath expression against the fixture body
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                        ExpressionElement = null; 
                    else
                        ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
            
            /// <summary>
            /// HTTP header field name for source
            /// </summary>
            [FhirElement("headerField", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderFieldElement
            {
                get { return _HeaderFieldElement; }
                set { _HeaderFieldElement = value; OnPropertyChanged("HeaderFieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderFieldElement;
            
            /// <summary>
            /// HTTP header field name for source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string HeaderField
            {
                get { return HeaderFieldElement != null ? HeaderFieldElement.Value : null; }
                set
                {
                    if (value == null)
                        HeaderFieldElement = null; 
                    else
                        HeaderFieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("HeaderField");
                }
            }
            
            /// <summary>
            /// Hint help text for default value to enter
            /// </summary>
            [FhirElement("hint", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HintElement
            {
                get { return _HintElement; }
                set { _HintElement = value; OnPropertyChanged("HintElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HintElement;
            
            /// <summary>
            /// Hint help text for default value to enter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Hint
            {
                get { return HintElement != null ? HintElement.Value : null; }
                set
                {
                    if (value == null)
                        HintElement = null; 
                    else
                        HintElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Hint");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath against the fixture body
            /// </summary>
            [FhirElement("path", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// XPath or JSONPath against the fixture body
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null; 
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// Fixture Id of source expression or headerField within this variable
            /// </summary>
            [FhirElement("sourceId", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of source expression or headerField within this variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdElement = null; 
                    else
                        SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VariableComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DefaultValueElement != null) dest.DefaultValueElement = (Hl7.Fhir.Model.FhirString)DefaultValueElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(HintElement != null) dest.HintElement = (Hl7.Fhir.Model.FhirString)HintElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new VariableComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VariableComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DefaultValueElement, otherT.DefaultValueElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(HintElement, otherT.HintElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VariableComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DefaultValueElement, otherT.DefaultValueElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(HintElement, otherT.HintElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DefaultValueElement != null) yield return DefaultValueElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                    if (HeaderFieldElement != null) yield return HeaderFieldElement;
                    if (HintElement != null) yield return HintElement;
                    if (PathElement != null) yield return PathElement;
                    if (SourceIdElement != null) yield return SourceIdElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DefaultValueElement != null) yield return new ElementValue("defaultValue", DefaultValueElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                    if (HeaderFieldElement != null) yield return new ElementValue("headerField", HeaderFieldElement);
                    if (HintElement != null) yield return new ElementValue("hint", HintElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                }
            }

            
        }
        
        
        [FhirType("RuleComponent")]
        [DataContract]
        public partial class RuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RuleComponent"; } }
            
            /// <summary>
            /// Assert rule resource reference
            /// </summary>
            [FhirElement("resource", Order=40)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// Rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.RuleParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<Hl7.Fhir.Model.TestScript.RuleParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.RuleParamComponent> _Param;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Param != null) dest.Param = new List<Hl7.Fhir.Model.TestScript.RuleParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }

            
        }
        
        
        [FhirType("RuleParamComponent")]
        [DataContract]
        public partial class RuleParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RuleParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuleParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RuleParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RuleParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuleParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("RulesetComponent")]
        [DataContract]
        public partial class RulesetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RulesetComponent"; } }
            
            /// <summary>
            /// Assert ruleset resource reference
            /// </summary>
            [FhirElement("resource", Order=40)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// The referenced rule within the ruleset
            /// </summary>
            [FhirElement("rule", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.RulesetRuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.TestScript.RulesetRuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.RulesetRuleComponent> _Rule;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RulesetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.TestScript.RulesetRuleComponent>(Rule.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RulesetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RulesetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RulesetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Rule) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                }
            }

            
        }
        
        
        [FhirType("RulesetRuleComponent")]
        [DataContract]
        public partial class RulesetRuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RulesetRuleComponent"; } }
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            [FhirElement("ruleId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RuleIdElement
            {
                get { return _RuleIdElement; }
                set { _RuleIdElement = value; OnPropertyChanged("RuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RuleIdElement;
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RuleId
            {
                get { return RuleIdElement != null ? RuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RuleIdElement = null; 
                    else
                        RuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RuleId");
                }
            }
            
            /// <summary>
            /// Ruleset rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.RulesetRuleParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<Hl7.Fhir.Model.TestScript.RulesetRuleParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.RulesetRuleParamComponent> _Param;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RulesetRuleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RuleIdElement != null) dest.RuleIdElement = (Hl7.Fhir.Model.Id)RuleIdElement.DeepCopy();
                    if(Param != null) dest.Param = new List<Hl7.Fhir.Model.TestScript.RulesetRuleParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RulesetRuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RulesetRuleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RulesetRuleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RuleIdElement != null) yield return RuleIdElement;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RuleIdElement != null) yield return new ElementValue("ruleId", RuleIdElement);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }

            
        }
        
        
        [FhirType("RulesetRuleParamComponent")]
        [DataContract]
        public partial class RulesetRuleParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RulesetRuleParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RulesetRuleParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RulesetRuleParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RulesetRuleParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RulesetRuleParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("SetupComponent")]
        [DataContract]
        public partial class SetupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SetupComponent"; } }
            
            /// <summary>
            /// A setup operation or assert to perform
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.SetupActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestScript.SetupActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.SetupActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestScript.SetupActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SetupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }

            
        }
        
        
        [FhirType("SetupActionComponent")]
        [DataContract]
        public partial class SetupActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SetupActionComponent"; } }
            
            /// <summary>
            /// The setup operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestScript.OperationComponent _Operation;
            
            /// <summary>
            /// The assertion to perform
            /// </summary>
            [FhirElement("assert", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private Hl7.Fhir.Model.TestScript.AssertComponent _Assert;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestScript.OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (Hl7.Fhir.Model.TestScript.AssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SetupActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SetupActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                    if (Assert != null) yield return Assert;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    if (Assert != null) yield return new ElementValue("assert", Assert);
                }
            }

            
        }
        
        
        [FhirType("OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            /// <summary>
            /// The operation code type that will be executed
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Resource type
            /// </summary>
            [FhirElement("resource", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType> ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType> _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.FHIRDefinedType? Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ResourceElement = null; 
                    else
                        ResourceElement = new Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType>(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// Tracking/logging operation label
            /// </summary>
            [FhirElement("label", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Tracking/logging operation label
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Label
            {
                get { return LabelElement != null ? LabelElement.Value : null; }
                set
                {
                    if (value == null)
                        LabelElement = null; 
                    else
                        LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// Tracking/reporting operation description
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Tracking/reporting operation description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            [FhirElement("accept", Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.ContentType> AcceptElement
            {
                get { return _AcceptElement; }
                set { _AcceptElement = value; OnPropertyChanged("AcceptElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.ContentType> _AcceptElement;
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.ContentType? Accept
            {
                get { return AcceptElement != null ? AcceptElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AcceptElement = null; 
                    else
                        AcceptElement = new Code<Hl7.Fhir.Model.TestScript.ContentType>(value);
                    OnPropertyChanged("Accept");
                }
            }
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            [FhirElement("contentType", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.ContentType> ContentType_Element
            {
                get { return _ContentType_Element; }
                set { _ContentType_Element = value; OnPropertyChanged("ContentType_Element"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.ContentType> _ContentType_Element;
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.ContentType? ContentType_
            {
                get { return ContentType_Element != null ? ContentType_Element.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ContentType_Element = null; 
                    else
                        ContentType_Element = new Code<Hl7.Fhir.Model.TestScript.ContentType>(value);
                    OnPropertyChanged("ContentType_");
                }
            }
            
            /// <summary>
            /// Server responding to the request
            /// </summary>
            [FhirElement("destination", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Server responding to the request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Destination
            {
                get { return DestinationElement != null ? DestinationElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DestinationElement = null; 
                    else
                        DestinationElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Destination");
                }
            }
            
            /// <summary>
            /// Whether or not to send the request url in encoded format
            /// </summary>
            [FhirElement("encodeRequestUrl", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean EncodeRequestUrlElement
            {
                get { return _EncodeRequestUrlElement; }
                set { _EncodeRequestUrlElement = value; OnPropertyChanged("EncodeRequestUrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _EncodeRequestUrlElement;
            
            /// <summary>
            /// Whether or not to send the request url in encoded format
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? EncodeRequestUrl
            {
                get { return EncodeRequestUrlElement != null ? EncodeRequestUrlElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        EncodeRequestUrlElement = null; 
                    else
                        EncodeRequestUrlElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("EncodeRequestUrl");
                }
            }
            
            /// <summary>
            /// Server initiating the request
            /// </summary>
            [FhirElement("origin", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Integer OriginElement
            {
                get { return _OriginElement; }
                set { _OriginElement = value; OnPropertyChanged("OriginElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _OriginElement;
            
            /// <summary>
            /// Server initiating the request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Origin
            {
                get { return OriginElement != null ? OriginElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        OriginElement = null; 
                    else
                        OriginElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Origin");
                }
            }
            
            /// <summary>
            /// Explicitly defined path parameters
            /// </summary>
            [FhirElement("params", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ParamsElement
            {
                get { return _ParamsElement; }
                set { _ParamsElement = value; OnPropertyChanged("ParamsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ParamsElement;
            
            /// <summary>
            /// Explicitly defined path parameters
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Params
            {
                get { return ParamsElement != null ? ParamsElement.Value : null; }
                set
                {
                    if (value == null)
                        ParamsElement = null; 
                    else
                        ParamsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Params");
                }
            }
            
            /// <summary>
            /// Each operation can have one or more header elements
            /// </summary>
            [FhirElement("requestHeader", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.RequestHeaderComponent> RequestHeader
            {
                get { if(_RequestHeader==null) _RequestHeader = new List<Hl7.Fhir.Model.TestScript.RequestHeaderComponent>(); return _RequestHeader; }
                set { _RequestHeader = value; OnPropertyChanged("RequestHeader"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.RequestHeaderComponent> _RequestHeader;
            
            /// <summary>
            /// Fixture Id of mapped request
            /// </summary>
            [FhirElement("requestId", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Id RequestIdElement
            {
                get { return _RequestIdElement; }
                set { _RequestIdElement = value; OnPropertyChanged("RequestIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RequestIdElement;
            
            /// <summary>
            /// Fixture Id of mapped request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RequestId
            {
                get { return RequestIdElement != null ? RequestIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RequestIdElement = null; 
                    else
                        RequestIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RequestId");
                }
            }
            
            /// <summary>
            /// Fixture Id of mapped response
            /// </summary>
            [FhirElement("responseId", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResponseIdElement
            {
                get { return _ResponseIdElement; }
                set { _ResponseIdElement = value; OnPropertyChanged("ResponseIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ResponseIdElement;
            
            /// <summary>
            /// Fixture Id of mapped response
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResponseId
            {
                get { return ResponseIdElement != null ? ResponseIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ResponseIdElement = null; 
                    else
                        ResponseIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ResponseId");
                }
            }
            
            /// <summary>
            /// Fixture Id of body for PUT and POST requests
            /// </summary>
            [FhirElement("sourceId", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of body for PUT and POST requests
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdElement = null; 
                    else
                        SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            /// <summary>
            /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
            /// </summary>
            [FhirElement("targetId", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.Id TargetIdElement
            {
                get { return _TargetIdElement; }
                set { _TargetIdElement = value; OnPropertyChanged("TargetIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _TargetIdElement;
            
            /// <summary>
            /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TargetId
            {
                get { return TargetIdElement != null ? TargetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        TargetIdElement = null; 
                    else
                        TargetIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("TargetId");
                }
            }
            
            /// <summary>
            /// Request URL
            /// </summary>
            [FhirElement("url", Order=190)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _UrlElement;
            
            /// <summary>
            /// Request URL
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if (value == null)
                        UrlElement = null; 
                    else
                        UrlElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Url");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType>)ResourceElement.DeepCopy();
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(AcceptElement != null) dest.AcceptElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)AcceptElement.DeepCopy();
                    if(ContentType_Element != null) dest.ContentType_Element = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentType_Element.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(EncodeRequestUrlElement != null) dest.EncodeRequestUrlElement = (Hl7.Fhir.Model.FhirBoolean)EncodeRequestUrlElement.DeepCopy();
                    if(OriginElement != null) dest.OriginElement = (Hl7.Fhir.Model.Integer)OriginElement.DeepCopy();
                    if(ParamsElement != null) dest.ParamsElement = (Hl7.Fhir.Model.FhirString)ParamsElement.DeepCopy();
                    if(RequestHeader != null) dest.RequestHeader = new List<Hl7.Fhir.Model.TestScript.RequestHeaderComponent>(RequestHeader.DeepCopy());
                    if(RequestIdElement != null) dest.RequestIdElement = (Hl7.Fhir.Model.Id)RequestIdElement.DeepCopy();
                    if(ResponseIdElement != null) dest.ResponseIdElement = (Hl7.Fhir.Model.Id)ResponseIdElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    if(TargetIdElement != null) dest.TargetIdElement = (Hl7.Fhir.Model.Id)TargetIdElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirString)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(AcceptElement, otherT.AcceptElement)) return false;
                if( !DeepComparable.Matches(ContentType_Element, otherT.ContentType_Element)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(EncodeRequestUrlElement, otherT.EncodeRequestUrlElement)) return false;
                if( !DeepComparable.Matches(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.Matches(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.Matches(RequestHeader, otherT.RequestHeader)) return false;
                if( !DeepComparable.Matches(RequestIdElement, otherT.RequestIdElement)) return false;
                if( !DeepComparable.Matches(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.Matches(TargetIdElement, otherT.TargetIdElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(AcceptElement, otherT.AcceptElement)) return false;
                if( !DeepComparable.IsExactly(ContentType_Element, otherT.ContentType_Element)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(EncodeRequestUrlElement, otherT.EncodeRequestUrlElement)) return false;
                if( !DeepComparable.IsExactly(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.IsExactly(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.IsExactly(RequestHeader, otherT.RequestHeader)) return false;
                if( !DeepComparable.IsExactly(RequestIdElement, otherT.RequestIdElement)) return false;
                if( !DeepComparable.IsExactly(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.IsExactly(TargetIdElement, otherT.TargetIdElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (ResourceElement != null) yield return ResourceElement;
                    if (LabelElement != null) yield return LabelElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (AcceptElement != null) yield return AcceptElement;
                    if (ContentType_Element != null) yield return ContentType_Element;
                    if (DestinationElement != null) yield return DestinationElement;
                    if (EncodeRequestUrlElement != null) yield return EncodeRequestUrlElement;
                    if (OriginElement != null) yield return OriginElement;
                    if (ParamsElement != null) yield return ParamsElement;
                    foreach (var elem in RequestHeader) { if (elem != null) yield return elem; }
                    if (RequestIdElement != null) yield return RequestIdElement;
                    if (ResponseIdElement != null) yield return ResponseIdElement;
                    if (SourceIdElement != null) yield return SourceIdElement;
                    if (TargetIdElement != null) yield return TargetIdElement;
                    if (UrlElement != null) yield return UrlElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (ResourceElement != null) yield return new ElementValue("resource", ResourceElement);
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (AcceptElement != null) yield return new ElementValue("accept", AcceptElement);
                    if (ContentType_Element != null) yield return new ElementValue("contentType", ContentType_Element);
                    if (DestinationElement != null) yield return new ElementValue("destination", DestinationElement);
                    if (EncodeRequestUrlElement != null) yield return new ElementValue("encodeRequestUrl", EncodeRequestUrlElement);
                    if (OriginElement != null) yield return new ElementValue("origin", OriginElement);
                    if (ParamsElement != null) yield return new ElementValue("params", ParamsElement);
                    foreach (var elem in RequestHeader) { if (elem != null) yield return new ElementValue("requestHeader", elem); }
                    if (RequestIdElement != null) yield return new ElementValue("requestId", RequestIdElement);
                    if (ResponseIdElement != null) yield return new ElementValue("responseId", ResponseIdElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                    if (TargetIdElement != null) yield return new ElementValue("targetId", TargetIdElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                }
            }

            
        }
        
        
        [FhirType("RequestHeaderComponent")]
        [DataContract]
        public partial class RequestHeaderComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RequestHeaderComponent"; } }
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            [FhirElement("field", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FieldElement
            {
                get { return _FieldElement; }
                set { _FieldElement = value; OnPropertyChanged("FieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _FieldElement;
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Field
            {
                get { return FieldElement != null ? FieldElement.Value : null; }
                set
                {
                    if (value == null)
                        FieldElement = null; 
                    else
                        FieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Field");
                }
            }
            
            /// <summary>
            /// HTTP headerfield value
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// HTTP headerfield value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequestHeaderComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FieldElement != null) dest.FieldElement = (Hl7.Fhir.Model.FhirString)FieldElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RequestHeaderComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequestHeaderComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(FieldElement, otherT.FieldElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequestHeaderComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FieldElement, otherT.FieldElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (FieldElement != null) yield return FieldElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (FieldElement != null) yield return new ElementValue("field", FieldElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("AssertComponent")]
        [DataContract]
        public partial class AssertComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AssertComponent"; } }
            
            /// <summary>
            /// Tracking/logging assertion label
            /// </summary>
            [FhirElement("label", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Tracking/logging assertion label
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Label
            {
                get { return LabelElement != null ? LabelElement.Value : null; }
                set
                {
                    if (value == null)
                        LabelElement = null; 
                    else
                        LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// Tracking/reporting assertion description
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Tracking/reporting assertion description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// response | request
            /// </summary>
            [FhirElement("direction", Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType> DirectionElement
            {
                get { return _DirectionElement; }
                set { _DirectionElement = value; OnPropertyChanged("DirectionElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType> _DirectionElement;
            
            /// <summary>
            /// response | request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.AssertionDirectionType? Direction
            {
                get { return DirectionElement != null ? DirectionElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DirectionElement = null; 
                    else
                        DirectionElement = new Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType>(value);
                    OnPropertyChanged("Direction");
                }
            }
            
            /// <summary>
            /// Id of the source fixture to be evaluated
            /// </summary>
            [FhirElement("compareToSourceId", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourceIdElement
            {
                get { return _CompareToSourceIdElement; }
                set { _CompareToSourceIdElement = value; OnPropertyChanged("CompareToSourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourceIdElement;
            
            /// <summary>
            /// Id of the source fixture to be evaluated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourceId
            {
                get { return CompareToSourceIdElement != null ? CompareToSourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        CompareToSourceIdElement = null; 
                    else
                        CompareToSourceIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourceId");
                }
            }
            
            /// <summary>
            /// The fluentpath expression to evaluate against the source fixture
            /// </summary>
            [FhirElement("compareToSourceExpression", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourceExpressionElement
            {
                get { return _CompareToSourceExpressionElement; }
                set { _CompareToSourceExpressionElement = value; OnPropertyChanged("CompareToSourceExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourceExpressionElement;
            
            /// <summary>
            /// The fluentpath expression to evaluate against the source fixture
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourceExpression
            {
                get { return CompareToSourceExpressionElement != null ? CompareToSourceExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                        CompareToSourceExpressionElement = null; 
                    else
                        CompareToSourceExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourceExpression");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression to evaluate against the source fixture
            /// </summary>
            [FhirElement("compareToSourcePath", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourcePathElement
            {
                get { return _CompareToSourcePathElement; }
                set { _CompareToSourcePathElement = value; OnPropertyChanged("CompareToSourcePathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourcePathElement;
            
            /// <summary>
            /// XPath or JSONPath expression to evaluate against the source fixture
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourcePath
            {
                get { return CompareToSourcePathElement != null ? CompareToSourcePathElement.Value : null; }
                set
                {
                    if (value == null)
                        CompareToSourcePathElement = null; 
                    else
                        CompareToSourcePathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourcePath");
                }
            }
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            [FhirElement("contentType", Order=100)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.ContentType> ContentType_Element
            {
                get { return _ContentType_Element; }
                set { _ContentType_Element = value; OnPropertyChanged("ContentType_Element"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.ContentType> _ContentType_Element;
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.ContentType? ContentType_
            {
                get { return ContentType_Element != null ? ContentType_Element.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ContentType_Element = null; 
                    else
                        ContentType_Element = new Code<Hl7.Fhir.Model.TestScript.ContentType>(value);
                    OnPropertyChanged("ContentType_");
                }
            }
            
            /// <summary>
            /// The fluentpath expression to be evaluated
            /// </summary>
            [FhirElement("expression", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// The fluentpath expression to be evaluated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                        ExpressionElement = null; 
                    else
                        ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            [FhirElement("headerField", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderFieldElement
            {
                get { return _HeaderFieldElement; }
                set { _HeaderFieldElement = value; OnPropertyChanged("HeaderFieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderFieldElement;
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string HeaderField
            {
                get { return HeaderFieldElement != null ? HeaderFieldElement.Value : null; }
                set
                {
                    if (value == null)
                        HeaderFieldElement = null; 
                    else
                        HeaderFieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("HeaderField");
                }
            }
            
            /// <summary>
            /// Fixture Id of minimum content resource
            /// </summary>
            [FhirElement("minimumId", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MinimumIdElement
            {
                get { return _MinimumIdElement; }
                set { _MinimumIdElement = value; OnPropertyChanged("MinimumIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MinimumIdElement;
            
            /// <summary>
            /// Fixture Id of minimum content resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MinimumId
            {
                get { return MinimumIdElement != null ? MinimumIdElement.Value : null; }
                set
                {
                    if (value == null)
                        MinimumIdElement = null; 
                    else
                        MinimumIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MinimumId");
                }
            }
            
            /// <summary>
            /// Perform validation on navigation links?
            /// </summary>
            [FhirElement("navigationLinks", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean NavigationLinksElement
            {
                get { return _NavigationLinksElement; }
                set { _NavigationLinksElement = value; OnPropertyChanged("NavigationLinksElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _NavigationLinksElement;
            
            /// <summary>
            /// Perform validation on navigation links?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? NavigationLinks
            {
                get { return NavigationLinksElement != null ? NavigationLinksElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NavigationLinksElement = null; 
                    else
                        NavigationLinksElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("NavigationLinks");
                }
            }
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains | eval
            /// </summary>
            [FhirElement("operator", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType> OperatorElement
            {
                get { return _OperatorElement; }
                set { _OperatorElement = value; OnPropertyChanged("OperatorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType> _OperatorElement;
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains | eval
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.AssertionOperatorType? Operator
            {
                get { return OperatorElement != null ? OperatorElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        OperatorElement = null; 
                    else
                        OperatorElement = new Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType>(value);
                    OnPropertyChanged("Operator");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression
            /// </summary>
            [FhirElement("path", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// XPath or JSONPath expression
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null; 
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// delete | get | options | patch | post | put
            /// </summary>
            [FhirElement("requestMethod", Order=170)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.TestScriptRequestMethodCode> RequestMethodElement
            {
                get { return _RequestMethodElement; }
                set { _RequestMethodElement = value; OnPropertyChanged("RequestMethodElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.TestScriptRequestMethodCode> _RequestMethodElement;
            
            /// <summary>
            /// delete | get | options | patch | post | put
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.TestScriptRequestMethodCode? RequestMethod
            {
                get { return RequestMethodElement != null ? RequestMethodElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        RequestMethodElement = null; 
                    else
                        RequestMethodElement = new Code<Hl7.Fhir.Model.TestScript.TestScriptRequestMethodCode>(value);
                    OnPropertyChanged("RequestMethod");
                }
            }
            
            /// <summary>
            /// Request URL comparison value
            /// </summary>
            [FhirElement("requestURL", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequestURLElement
            {
                get { return _RequestURLElement; }
                set { _RequestURLElement = value; OnPropertyChanged("RequestURLElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequestURLElement;
            
            /// <summary>
            /// Request URL comparison value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RequestURL
            {
                get { return RequestURLElement != null ? RequestURLElement.Value : null; }
                set
                {
                    if (value == null)
                        RequestURLElement = null; 
                    else
                        RequestURLElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("RequestURL");
                }
            }
            
            /// <summary>
            /// Resource type
            /// </summary>
            [FhirElement("resource", Order=190)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType> ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType> _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.FHIRDefinedType? Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ResourceElement = null; 
                    else
                        ResourceElement = new Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType>(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            [FhirElement("response", Order=200)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes> ResponseElement
            {
                get { return _ResponseElement; }
                set { _ResponseElement = value; OnPropertyChanged("ResponseElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes> _ResponseElement;
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.AssertionResponseTypes? Response
            {
                get { return ResponseElement != null ? ResponseElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ResponseElement = null; 
                    else
                        ResponseElement = new Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes>(value);
                    OnPropertyChanged("Response");
                }
            }
            
            /// <summary>
            /// HTTP response code to test
            /// </summary>
            [FhirElement("responseCode", Order=210)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResponseCodeElement
            {
                get { return _ResponseCodeElement; }
                set { _ResponseCodeElement = value; OnPropertyChanged("ResponseCodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResponseCodeElement;
            
            /// <summary>
            /// HTTP response code to test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResponseCode
            {
                get { return ResponseCodeElement != null ? ResponseCodeElement.Value : null; }
                set
                {
                    if (value == null)
                        ResponseCodeElement = null; 
                    else
                        ResponseCodeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResponseCode");
                }
            }
            
            /// <summary>
            /// The reference to a TestScript.rule
            /// </summary>
            [FhirElement("rule", Order=220)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.ActionAssertRuleComponent Rule
            {
                get { return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private Hl7.Fhir.Model.TestScript.ActionAssertRuleComponent _Rule;
            
            /// <summary>
            /// The reference to a TestScript.ruleset
            /// </summary>
            [FhirElement("ruleset", Order=230)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.ActionAssertRulesetComponent Ruleset
            {
                get { return _Ruleset; }
                set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
            }
            
            private Hl7.Fhir.Model.TestScript.ActionAssertRulesetComponent _Ruleset;
            
            /// <summary>
            /// Fixture Id of source expression or headerField
            /// </summary>
            [FhirElement("sourceId", Order=240)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of source expression or headerField
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdElement = null; 
                    else
                        SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            /// <summary>
            /// Profile Id of validation profile reference
            /// </summary>
            [FhirElement("validateProfileId", Order=250)]
            [DataMember]
            public Hl7.Fhir.Model.Id ValidateProfileIdElement
            {
                get { return _ValidateProfileIdElement; }
                set { _ValidateProfileIdElement = value; OnPropertyChanged("ValidateProfileIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ValidateProfileIdElement;
            
            /// <summary>
            /// Profile Id of validation profile reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidateProfileId
            {
                get { return ValidateProfileIdElement != null ? ValidateProfileIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidateProfileIdElement = null; 
                    else
                        ValidateProfileIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ValidateProfileId");
                }
            }
            
            /// <summary>
            /// The value to compare to
            /// </summary>
            [FhirElement("value", Order=260)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The value to compare to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// Will this assert produce a warning only on error?
            /// </summary>
            [FhirElement("warningOnly", Order=270)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean WarningOnlyElement
            {
                get { return _WarningOnlyElement; }
                set { _WarningOnlyElement = value; OnPropertyChanged("WarningOnlyElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _WarningOnlyElement;
            
            /// <summary>
            /// Will this assert produce a warning only on error?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? WarningOnly
            {
                get { return WarningOnlyElement != null ? WarningOnlyElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        WarningOnlyElement = null; 
                    else
                        WarningOnlyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("WarningOnly");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AssertComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(DirectionElement != null) dest.DirectionElement = (Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType>)DirectionElement.DeepCopy();
                    if(CompareToSourceIdElement != null) dest.CompareToSourceIdElement = (Hl7.Fhir.Model.FhirString)CompareToSourceIdElement.DeepCopy();
                    if(CompareToSourceExpressionElement != null) dest.CompareToSourceExpressionElement = (Hl7.Fhir.Model.FhirString)CompareToSourceExpressionElement.DeepCopy();
                    if(CompareToSourcePathElement != null) dest.CompareToSourcePathElement = (Hl7.Fhir.Model.FhirString)CompareToSourcePathElement.DeepCopy();
                    if(ContentType_Element != null) dest.ContentType_Element = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentType_Element.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(MinimumIdElement != null) dest.MinimumIdElement = (Hl7.Fhir.Model.FhirString)MinimumIdElement.DeepCopy();
                    if(NavigationLinksElement != null) dest.NavigationLinksElement = (Hl7.Fhir.Model.FhirBoolean)NavigationLinksElement.DeepCopy();
                    if(OperatorElement != null) dest.OperatorElement = (Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType>)OperatorElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(RequestMethodElement != null) dest.RequestMethodElement = (Code<Hl7.Fhir.Model.TestScript.TestScriptRequestMethodCode>)RequestMethodElement.DeepCopy();
                    if(RequestURLElement != null) dest.RequestURLElement = (Hl7.Fhir.Model.FhirString)RequestURLElement.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Code<Hl7.Fhir.Model.TestScript.FHIRDefinedType>)ResourceElement.DeepCopy();
                    if(ResponseElement != null) dest.ResponseElement = (Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes>)ResponseElement.DeepCopy();
                    if(ResponseCodeElement != null) dest.ResponseCodeElement = (Hl7.Fhir.Model.FhirString)ResponseCodeElement.DeepCopy();
                    if(Rule != null) dest.Rule = (Hl7.Fhir.Model.TestScript.ActionAssertRuleComponent)Rule.DeepCopy();
                    if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.TestScript.ActionAssertRulesetComponent)Ruleset.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    if(ValidateProfileIdElement != null) dest.ValidateProfileIdElement = (Hl7.Fhir.Model.Id)ValidateProfileIdElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(WarningOnlyElement != null) dest.WarningOnlyElement = (Hl7.Fhir.Model.FhirBoolean)WarningOnlyElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AssertComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(DirectionElement, otherT.DirectionElement)) return false;
                if( !DeepComparable.Matches(CompareToSourceIdElement, otherT.CompareToSourceIdElement)) return false;
                if( !DeepComparable.Matches(CompareToSourceExpressionElement, otherT.CompareToSourceExpressionElement)) return false;
                if( !DeepComparable.Matches(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.Matches(ContentType_Element, otherT.ContentType_Element)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.Matches(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.Matches(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(RequestMethodElement, otherT.RequestMethodElement)) return false;
                if( !DeepComparable.Matches(RequestURLElement, otherT.RequestURLElement)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.Matches(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.Matches(ValidateProfileIdElement, otherT.ValidateProfileIdElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(WarningOnlyElement, otherT.WarningOnlyElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(DirectionElement, otherT.DirectionElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourceIdElement, otherT.CompareToSourceIdElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourceExpressionElement, otherT.CompareToSourceExpressionElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.IsExactly(ContentType_Element, otherT.ContentType_Element)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.IsExactly(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.IsExactly(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(RequestMethodElement, otherT.RequestMethodElement)) return false;
                if( !DeepComparable.IsExactly(RequestURLElement, otherT.RequestURLElement)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.IsExactly(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.IsExactly(ValidateProfileIdElement, otherT.ValidateProfileIdElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(WarningOnlyElement, otherT.WarningOnlyElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LabelElement != null) yield return LabelElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (DirectionElement != null) yield return DirectionElement;
                    if (CompareToSourceIdElement != null) yield return CompareToSourceIdElement;
                    if (CompareToSourceExpressionElement != null) yield return CompareToSourceExpressionElement;
                    if (CompareToSourcePathElement != null) yield return CompareToSourcePathElement;
                    if (ContentType_Element != null) yield return ContentType_Element;
                    if (ExpressionElement != null) yield return ExpressionElement;
                    if (HeaderFieldElement != null) yield return HeaderFieldElement;
                    if (MinimumIdElement != null) yield return MinimumIdElement;
                    if (NavigationLinksElement != null) yield return NavigationLinksElement;
                    if (OperatorElement != null) yield return OperatorElement;
                    if (PathElement != null) yield return PathElement;
                    if (RequestMethodElement != null) yield return RequestMethodElement;
                    if (RequestURLElement != null) yield return RequestURLElement;
                    if (ResourceElement != null) yield return ResourceElement;
                    if (ResponseElement != null) yield return ResponseElement;
                    if (ResponseCodeElement != null) yield return ResponseCodeElement;
                    if (Rule != null) yield return Rule;
                    if (Ruleset != null) yield return Ruleset;
                    if (SourceIdElement != null) yield return SourceIdElement;
                    if (ValidateProfileIdElement != null) yield return ValidateProfileIdElement;
                    if (ValueElement != null) yield return ValueElement;
                    if (WarningOnlyElement != null) yield return WarningOnlyElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (DirectionElement != null) yield return new ElementValue("direction", DirectionElement);
                    if (CompareToSourceIdElement != null) yield return new ElementValue("compareToSourceId", CompareToSourceIdElement);
                    if (CompareToSourceExpressionElement != null) yield return new ElementValue("compareToSourceExpression", CompareToSourceExpressionElement);
                    if (CompareToSourcePathElement != null) yield return new ElementValue("compareToSourcePath", CompareToSourcePathElement);
                    if (ContentType_Element != null) yield return new ElementValue("contentType", ContentType_Element);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                    if (HeaderFieldElement != null) yield return new ElementValue("headerField", HeaderFieldElement);
                    if (MinimumIdElement != null) yield return new ElementValue("minimumId", MinimumIdElement);
                    if (NavigationLinksElement != null) yield return new ElementValue("navigationLinks", NavigationLinksElement);
                    if (OperatorElement != null) yield return new ElementValue("operator", OperatorElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (RequestMethodElement != null) yield return new ElementValue("requestMethod", RequestMethodElement);
                    if (RequestURLElement != null) yield return new ElementValue("requestURL", RequestURLElement);
                    if (ResourceElement != null) yield return new ElementValue("resource", ResourceElement);
                    if (ResponseElement != null) yield return new ElementValue("response", ResponseElement);
                    if (ResponseCodeElement != null) yield return new ElementValue("responseCode", ResponseCodeElement);
                    if (Rule != null) yield return new ElementValue("rule", Rule);
                    if (Ruleset != null) yield return new ElementValue("ruleset", Ruleset);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                    if (ValidateProfileIdElement != null) yield return new ElementValue("validateProfileId", ValidateProfileIdElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    if (WarningOnlyElement != null) yield return new ElementValue("warningOnly", WarningOnlyElement);
                }
            }

            
        }
        
        
        [FhirType("ActionAssertRuleComponent")]
        [DataContract]
        public partial class ActionAssertRuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRuleComponent"; } }
            
            /// <summary>
            /// Id of the TestScript.rule
            /// </summary>
            [FhirElement("ruleId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RuleIdElement
            {
                get { return _RuleIdElement; }
                set { _RuleIdElement = value; OnPropertyChanged("RuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RuleIdElement;
            
            /// <summary>
            /// Id of the TestScript.rule
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RuleId
            {
                get { return RuleIdElement != null ? RuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RuleIdElement = null; 
                    else
                        RuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RuleId");
                }
            }
            
            /// <summary>
            /// Rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.ActionAssertRuleParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<Hl7.Fhir.Model.TestScript.ActionAssertRuleParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.ActionAssertRuleParamComponent> _Param;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRuleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RuleIdElement != null) dest.RuleIdElement = (Hl7.Fhir.Model.Id)RuleIdElement.DeepCopy();
                    if(Param != null) dest.Param = new List<Hl7.Fhir.Model.TestScript.ActionAssertRuleParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionAssertRuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RuleIdElement != null) yield return RuleIdElement;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RuleIdElement != null) yield return new ElementValue("ruleId", RuleIdElement);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }

            
        }
        
        
        [FhirType("ActionAssertRuleParamComponent")]
        [DataContract]
        public partial class ActionAssertRuleParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRuleParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRuleParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionAssertRuleParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("ActionAssertRulesetComponent")]
        [DataContract]
        public partial class ActionAssertRulesetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRulesetComponent"; } }
            
            /// <summary>
            /// Id of the TestScript.ruleset
            /// </summary>
            [FhirElement("rulesetId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RulesetIdElement
            {
                get { return _RulesetIdElement; }
                set { _RulesetIdElement = value; OnPropertyChanged("RulesetIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RulesetIdElement;
            
            /// <summary>
            /// Id of the TestScript.ruleset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RulesetId
            {
                get { return RulesetIdElement != null ? RulesetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RulesetIdElement = null; 
                    else
                        RulesetIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RulesetId");
                }
            }
            
            /// <summary>
            /// The referenced rule within the ruleset
            /// </summary>
            [FhirElement("rule", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.ActionAssertRulesetRuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.TestScript.ActionAssertRulesetRuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.ActionAssertRulesetRuleComponent> _Rule;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRulesetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RulesetIdElement != null) dest.RulesetIdElement = (Hl7.Fhir.Model.Id)RulesetIdElement.DeepCopy();
                    if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.TestScript.ActionAssertRulesetRuleComponent>(Rule.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionAssertRulesetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RulesetIdElement, otherT.RulesetIdElement)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RulesetIdElement, otherT.RulesetIdElement)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RulesetIdElement != null) yield return RulesetIdElement;
                    foreach (var elem in Rule) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RulesetIdElement != null) yield return new ElementValue("rulesetId", RulesetIdElement);
                    foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                }
            }

            
        }
        
        
        [FhirType("ActionAssertRulesetRuleComponent")]
        [DataContract]
        public partial class ActionAssertRulesetRuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRulesetRuleComponent"; } }
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            [FhirElement("ruleId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RuleIdElement
            {
                get { return _RuleIdElement; }
                set { _RuleIdElement = value; OnPropertyChanged("RuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RuleIdElement;
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RuleId
            {
                get { return RuleIdElement != null ? RuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RuleIdElement = null; 
                    else
                        RuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RuleId");
                }
            }
            
            /// <summary>
            /// Rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.ParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<Hl7.Fhir.Model.TestScript.ParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.ParamComponent> _Param;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRulesetRuleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RuleIdElement != null) dest.RuleIdElement = (Hl7.Fhir.Model.Id)RuleIdElement.DeepCopy();
                    if(Param != null) dest.Param = new List<Hl7.Fhir.Model.TestScript.ParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionAssertRulesetRuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetRuleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetRuleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RuleIdElement != null) yield return RuleIdElement;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RuleIdElement != null) yield return new ElementValue("ruleId", RuleIdElement);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }

            
        }
        
        
        [FhirType("ParamComponent")]
        [DataContract]
        public partial class ParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("TestComponent")]
        [DataContract]
        public partial class TestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TestComponent"; } }
            
            /// <summary>
            /// Tracking/logging name of this test
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Tracking/logging name of this test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Tracking/reporting short description of the test
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Tracking/reporting short description of the test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// A test operation or assert to perform
            /// </summary>
            [FhirElement("action", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestScript.TestActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestScript.TestActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }

            
        }
        
        
        [FhirType("TestActionComponent")]
        [DataContract]
        public partial class TestActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TestActionComponent"; } }
            
            /// <summary>
            /// The setup operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestScript.OperationComponent _Operation;
            
            /// <summary>
            /// The setup assertion to perform
            /// </summary>
            [FhirElement("assert", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private Hl7.Fhir.Model.TestScript.AssertComponent _Assert;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestScript.OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (Hl7.Fhir.Model.TestScript.AssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                    if (Assert != null) yield return Assert;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    if (Assert != null) yield return new ElementValue("assert", Assert);
                }
            }

            
        }
        
        
        [FhirType("TeardownComponent")]
        [DataContract]
        public partial class TeardownComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownComponent"; } }
            
            /// <summary>
            /// One or more teardown operations to perform
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TeardownActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestScript.TeardownActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TeardownActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestScript.TeardownActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TeardownComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TeardownComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TeardownComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }

            
        }
        
        
        [FhirType("TeardownActionComponent")]
        [DataContract]
        public partial class TeardownActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownActionComponent"; } }
            
            /// <summary>
            /// The teardown operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestScript.OperationComponent _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestScript.OperationComponent)Operation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TeardownActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TeardownActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TeardownActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                }
            }

            
        }
        
        
        /// <summary>
        /// Logical URI to reference this test script (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Logical URI to reference this test script (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the test script
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Business version of the test script
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the test script
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this test script (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this test script (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name for this test script (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this test script (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date this was last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date this was last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the test script
        /// </summary>
        [FhirElement("description", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for test script (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this test script is defined
        /// </summary>
        [FhirElement("purpose", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Purpose;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// An abstract server representing a client or sender in a message exchange
        /// </summary>
        [FhirElement("origin", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.OriginComponent> Origin
        {
            get { if(_Origin==null) _Origin = new List<Hl7.Fhir.Model.TestScript.OriginComponent>(); return _Origin; }
            set { _Origin = value; OnPropertyChanged("Origin"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.OriginComponent> _Origin;
        
        /// <summary>
        /// An abstract server representing a destination or receiver in a message exchange
        /// </summary>
        [FhirElement("destination", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.DestinationComponent> Destination
        {
            get { if(_Destination==null) _Destination = new List<Hl7.Fhir.Model.TestScript.DestinationComponent>(); return _Destination; }
            set { _Destination = value; OnPropertyChanged("Destination"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.DestinationComponent> _Destination;
        
        /// <summary>
        /// Required capability that is assumed to function correctly on the FHIR server being tested
        /// </summary>
        [FhirElement("metadata", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.TestScript.MetadataComponent Metadata
        {
            get { return _Metadata; }
            set { _Metadata = value; OnPropertyChanged("Metadata"); }
        }
        
        private Hl7.Fhir.Model.TestScript.MetadataComponent _Metadata;
        
        /// <summary>
        /// Fixture in the test script - by reference (uri)
        /// </summary>
        [FhirElement("fixture", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.FixtureComponent> Fixture
        {
            get { if(_Fixture==null) _Fixture = new List<Hl7.Fhir.Model.TestScript.FixtureComponent>(); return _Fixture; }
            set { _Fixture = value; OnPropertyChanged("Fixture"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.FixtureComponent> _Fixture;
        
        /// <summary>
        /// Reference of the validation profile
        /// </summary>
        [FhirElement("profile", Order=280)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile
        {
            get { if(_Profile==null) _Profile = new List<Hl7.Fhir.Model.ResourceReference>(); return _Profile; }
            set { _Profile = value; OnPropertyChanged("Profile"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Profile;
        
        /// <summary>
        /// Placeholder for evaluated elements
        /// </summary>
        [FhirElement("variable", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.VariableComponent> Variable
        {
            get { if(_Variable==null) _Variable = new List<Hl7.Fhir.Model.TestScript.VariableComponent>(); return _Variable; }
            set { _Variable = value; OnPropertyChanged("Variable"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.VariableComponent> _Variable;
        
        /// <summary>
        /// Assert rule used within the test script
        /// </summary>
        [FhirElement("rule", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.RuleComponent> Rule
        {
            get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.TestScript.RuleComponent>(); return _Rule; }
            set { _Rule = value; OnPropertyChanged("Rule"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.RuleComponent> _Rule;
        
        /// <summary>
        /// Assert ruleset used within the test script
        /// </summary>
        [FhirElement("ruleset", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.RulesetComponent> Ruleset
        {
            get { if(_Ruleset==null) _Ruleset = new List<Hl7.Fhir.Model.TestScript.RulesetComponent>(); return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.RulesetComponent> _Ruleset;
        
        /// <summary>
        /// A series of required setup operations before tests are executed
        /// </summary>
        [FhirElement("setup", Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.TestScript.SetupComponent Setup
        {
            get { return _Setup; }
            set { _Setup = value; OnPropertyChanged("Setup"); }
        }
        
        private Hl7.Fhir.Model.TestScript.SetupComponent _Setup;
        
        /// <summary>
        /// A test in this script
        /// </summary>
        [FhirElement("test", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.TestComponent> Test
        {
            get { if(_Test==null) _Test = new List<Hl7.Fhir.Model.TestScript.TestComponent>(); return _Test; }
            set { _Test = value; OnPropertyChanged("Test"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.TestComponent> _Test;
        
        /// <summary>
        /// A series of required clean up steps
        /// </summary>
        [FhirElement("teardown", Order=340)]
        [DataMember]
        public Hl7.Fhir.Model.TestScript.TeardownComponent Teardown
        {
            get { return _Teardown; }
            set { _Teardown = value; OnPropertyChanged("Teardown"); }
        }
        
        private Hl7.Fhir.Model.TestScript.TeardownComponent _Teardown;
        

        public static ElementDefinition.ConstraintComponent TestScript_INV_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "metadata.all(capability.required.exists() or capability.validated.exists())",
            Key = "inv-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "TestScript metadata capability SHALL contain required or validated or both.",
            Xpath = "f:capability/f:required or f:capability/f:validated or (f:capability/f:required and f:capability/f:validated)"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "variable.all(expression.empty() or headerField.empty() or path.empty())",
            Key = "inv-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Variable can only contain one of expression, headerField or path.",
            Xpath = "not(f:expression and f:headerField and f:path)"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "setup.action.all(operation.exists() xor assert.exists())",
            Key = "inv-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Setup action SHALL contain either an operation or assert but not both.",
            Xpath = "(f:operation or f:assert) and not(f:operation and f:assert)"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_7 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "setup.action.operation.all(sourceId.exists() or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('capabilities' |'search' | 'transaction' | 'history')))",
            Key = "inv-7",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Setup operation SHALL contain either sourceId or targetId or params or url.",
            Xpath = "f:sourceId or ((f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1)) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_5 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "setup.action.assert.all(contentType.count() + expression.count() + headerField.count() + minimumId.count() + navigationLinks.count() + path.count() + requestMethod.count() + resource.count() + responseCode.count() + response.count() + rule.count() + ruleset.count() + validateProfileId.count() <=1)",
            Key = "inv-5",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Only a single assertion SHALL be present within setup action assert element.",
            Xpath = "count(f:contentType) + count(f:expression) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:requestMethod) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:rule) + count(f:ruleset) + count(f:validateProfileId)  <=1"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_10 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "setup.action.assert.all(compareToSourceId.empty() xor (compareToSourceExpression.exists() or compareToSourcePath.exists()))",
            Key = "inv-10",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Setup action assert SHALL contain either compareToSourceId and compareToSourceExpression, compareToSourceId and compareToSourcePath or neither.",
            Xpath = "(f:compareToSourceId and f:compareToSourceExpression) or (f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourceExpression or f:compareToSourcePath)"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_12 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "setup.action.assert.all((response.empty() and responseCode.empty() and direction = 'request') or direction.empty() or direction = 'response')",
            Key = "inv-12",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Setup action assert response and responseCode SHALL be empty when direction equals request",
            Xpath = "((count(f:response) + count(f:responseCode)) = 0 and (f:direction/@value='request')) or (count(f:direction) = 0) or (f:direction/@value='response')"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "test.action.all(operation.exists() xor assert.exists())",
            Key = "inv-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Test action SHALL contain either an operation or assert but not both.",
            Xpath = "(f:operation or f:assert) and not(f:operation and f:assert)"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_8 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "test.action.operation.all(sourceId.exists() or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('capabilities' | 'search' | 'transaction' | 'history')))",
            Key = "inv-8",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Test operation SHALL contain either sourceId or targetId or params or url.",
            Xpath = "f:sourceId or (f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_6 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "test.action.assert.all(contentType.count() + expression.count() + headerField.count() + minimumId.count() + navigationLinks.count() + path.count() + requestMethod.count() + resource.count() + responseCode.count() + response.count() + rule.count() + ruleset.count() + validateProfileId.count() <=1)",
            Key = "inv-6",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Only a single assertion SHALL be present within test action assert element.",
            Xpath = "count(f:contentType) + count(f:expression) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:requestMethod) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:rule) + count(f:ruleset) + count(f:validateProfileId)  <=1"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_11 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "test.action.assert.all(compareToSourceId.empty() xor (compareToSourceExpression.exists() or compareToSourcePath.exists()))",
            Key = "inv-11",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Test action assert SHALL contain either compareToSourceId and compareToSourceExpression, compareToSourceId and compareToSourcePath or neither.",
            Xpath = "(f:compareToSourceId and f:compareToSourceExpression) or (f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourceExpression or f:compareToSourcePath)"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_13 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "test.action.assert.all((response.empty() and responseCode.empty() and direction = 'request') or direction.empty() or direction = 'response')",
            Key = "inv-13",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Test action assert response and response and responseCode SHALL be empty when direction equals request",
            Xpath = "((count(f:response) + count(f:responseCode)) = 0 and (f:direction/@value='request')) or (count(f:direction) = 0) or (f:direction/@value='response')"
        };

        public static ElementDefinition.ConstraintComponent TestScript_INV_9 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "teardown.action.operation.all(sourceId.exists() or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('capabilities' | 'search' | 'transaction' | 'history')))",
            Key = "inv-9",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Teardown operation SHALL contain either sourceId or targetId or params or url.",
            Xpath = "f:sourceId or (f:targetId or f:url or (f:params and f:resource)) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(TestScript_INV_4);
            InvariantConstraints.Add(TestScript_INV_3);
            InvariantConstraints.Add(TestScript_INV_1);
            InvariantConstraints.Add(TestScript_INV_7);
            InvariantConstraints.Add(TestScript_INV_5);
            InvariantConstraints.Add(TestScript_INV_10);
            InvariantConstraints.Add(TestScript_INV_12);
            InvariantConstraints.Add(TestScript_INV_2);
            InvariantConstraints.Add(TestScript_INV_8);
            InvariantConstraints.Add(TestScript_INV_6);
            InvariantConstraints.Add(TestScript_INV_11);
            InvariantConstraints.Add(TestScript_INV_13);
            InvariantConstraints.Add(TestScript_INV_9);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestScript;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(Origin != null) dest.Origin = new List<Hl7.Fhir.Model.TestScript.OriginComponent>(Origin.DeepCopy());
                if(Destination != null) dest.Destination = new List<Hl7.Fhir.Model.TestScript.DestinationComponent>(Destination.DeepCopy());
                if(Metadata != null) dest.Metadata = (Hl7.Fhir.Model.TestScript.MetadataComponent)Metadata.DeepCopy();
                if(Fixture != null) dest.Fixture = new List<Hl7.Fhir.Model.TestScript.FixtureComponent>(Fixture.DeepCopy());
                if(Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
                if(Variable != null) dest.Variable = new List<Hl7.Fhir.Model.TestScript.VariableComponent>(Variable.DeepCopy());
                if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.TestScript.RuleComponent>(Rule.DeepCopy());
                if(Ruleset != null) dest.Ruleset = new List<Hl7.Fhir.Model.TestScript.RulesetComponent>(Ruleset.DeepCopy());
                if(Setup != null) dest.Setup = (Hl7.Fhir.Model.TestScript.SetupComponent)Setup.DeepCopy();
                if(Test != null) dest.Test = new List<Hl7.Fhir.Model.TestScript.TestComponent>(Test.DeepCopy());
                if(Teardown != null) dest.Teardown = (Hl7.Fhir.Model.TestScript.TeardownComponent)Teardown.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestScript());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestScript;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(Origin, otherT.Origin)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.Matches(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            if( !DeepComparable.Matches(Variable, otherT.Variable)) return false;
            if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(Setup, otherT.Setup)) return false;
            if( !DeepComparable.Matches(Test, otherT.Test)) return false;
            if( !DeepComparable.Matches(Teardown, otherT.Teardown)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestScript;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(Origin, otherT.Origin)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.IsExactly(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            if( !DeepComparable.IsExactly(Variable, otherT.Variable)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(Setup, otherT.Setup)) return false;
            if( !DeepComparable.IsExactly(Test, otherT.Test)) return false;
            if( !DeepComparable.IsExactly(Teardown, otherT.Teardown)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				if (Identifier != null) yield return Identifier;
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (Description != null) yield return Description;
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Purpose != null) yield return Purpose;
				if (Copyright != null) yield return Copyright;
				foreach (var elem in Origin) { if (elem != null) yield return elem; }
				foreach (var elem in Destination) { if (elem != null) yield return elem; }
				if (Metadata != null) yield return Metadata;
				foreach (var elem in Fixture) { if (elem != null) yield return elem; }
				foreach (var elem in Profile) { if (elem != null) yield return elem; }
				foreach (var elem in Variable) { if (elem != null) yield return elem; }
				foreach (var elem in Rule) { if (elem != null) yield return elem; }
				foreach (var elem in Ruleset) { if (elem != null) yield return elem; }
				if (Setup != null) yield return Setup;
				foreach (var elem in Test) { if (elem != null) yield return elem; }
				if (Teardown != null) yield return Teardown;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                foreach (var elem in Origin) { if (elem != null) yield return new ElementValue("origin", elem); }
                foreach (var elem in Destination) { if (elem != null) yield return new ElementValue("destination", elem); }
                if (Metadata != null) yield return new ElementValue("metadata", Metadata);
                foreach (var elem in Fixture) { if (elem != null) yield return new ElementValue("fixture", elem); }
                foreach (var elem in Profile) { if (elem != null) yield return new ElementValue("profile", elem); }
                foreach (var elem in Variable) { if (elem != null) yield return new ElementValue("variable", elem); }
                foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                foreach (var elem in Ruleset) { if (elem != null) yield return new ElementValue("ruleset", elem); }
                if (Setup != null) yield return new ElementValue("setup", Setup);
                foreach (var elem in Test) { if (elem != null) yield return new ElementValue("test", elem); }
                if (Teardown != null) yield return new ElementValue("teardown", Teardown);
            }
        }

    }
    
}
