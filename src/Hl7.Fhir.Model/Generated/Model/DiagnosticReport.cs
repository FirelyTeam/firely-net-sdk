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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A Diagnostic report - a combination of request information, atomic results, images, interpretation, as well as formatted reports
    /// </summary>
    [FhirType("DiagnosticReport", IsResource=true)]
    [DataContract]
    public partial class DiagnosticReport : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The status of the diagnostic report as a whole
        /// </summary>
        [FhirEnumeration("DiagnosticReportStatus")]
        public enum DiagnosticReportStatus
        {
            [EnumLiteral("registered")]
            Registered, // The existence of the report is registered, but there is nothing yet available.
            [EnumLiteral("partial")]
            Partial, // This is a partial (e.g. initial, interim or preliminary) report: data in the report may be incomplete or unverified.
            [EnumLiteral("final")]
            Final, // The report is complete and verified by an authorized person.
            [EnumLiteral("corrected")]
            Corrected, // The report has been modified subsequent to being Final, and is complete and verified by an authorized person.
            [EnumLiteral("amended")]
            Amended, // The report has been modified subsequent to being Final, and is complete and verified by an authorized person, and data has been changed.
            [EnumLiteral("appended")]
            Appended, // The report has been modified subsequent to being Final, and is complete and verified by an authorized person. New content has been added, but existing content hasn't changed.
            [EnumLiteral("cancelled")]
            Cancelled, // The report is unavailable because the measurement was not started or not completed (also sometimes called "aborted").
            [EnumLiteral("entered in error")]
            EnteredInError, // The report has been withdrawn following previous Final release.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("DiagnosticReportImageComponent")]
        [DataContract]
        public partial class DiagnosticReportImageComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Comment about the image (e.g. explanation)
            /// </summary>
            [FhirElement("comment", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comment
            {
                get { return CommentElement != null ? CommentElement.Value : null; }
                set
                {
                    if(value == null)
                      CommentElement = null; 
                    else
                      CommentElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Reference to the image source
            /// </summary>
            [FhirElement("link", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Link { get; set; }
            
        }
        
        
        /// <summary>
        /// Name/Code for this diagnostic report
        /// </summary>
        [FhirElement("name", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Name { get; set; }
        
        /// <summary>
        /// registered | partial | final | corrected +
        /// </summary>
        [FhirElement("status", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus>(value);
            }
        }
        
        /// <summary>
        /// Date this version was released
        /// </summary>
        [FhirElement("issued", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime IssuedElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if(value == null)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
            }
        }
        
        /// <summary>
        /// The subject of the report, usually, but not always, the patient
        /// </summary>
        [FhirElement("subject", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Responsible Diagnostic Service
        /// </summary>
        [FhirElement("performer", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer { get; set; }
        
        /// <summary>
        /// Id for external references to this report
        /// </summary>
        [FhirElement("identifier", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier { get; set; }
        
        /// <summary>
        /// What was requested
        /// </summary>
        [FhirElement("requestDetail", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> RequestDetail { get; set; }
        
        /// <summary>
        /// Biochemistry, Hematology etc.
        /// </summary>
        [FhirElement("serviceCategory", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ServiceCategory { get; set; }
        
        /// <summary>
        /// Physiologically Relevant time/time-period for report
        /// </summary>
        [FhirElement("diagnostic", Order=150, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Diagnostic { get; set; }
        
        /// <summary>
        /// Specimens this report is based on
        /// </summary>
        [FhirElement("specimen", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Specimen { get; set; }
        
        /// <summary>
        /// Observations - simple, or complex nested groups
        /// </summary>
        [FhirElement("result", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Result { get; set; }
        
        /// <summary>
        /// Reference to full details of imaging associated with the diagnostic report
        /// </summary>
        [FhirElement("imagingStudy", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ImagingStudy { get; set; }
        
        /// <summary>
        /// Key images associated with this report
        /// </summary>
        [FhirElement("image", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportImageComponent> Image { get; set; }
        
        /// <summary>
        /// Clinical Interpretation of test results
        /// </summary>
        [FhirElement("conclusion", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ConclusionElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Conclusion
        {
            get { return ConclusionElement != null ? ConclusionElement.Value : null; }
            set
            {
                if(value == null)
                  ConclusionElement = null; 
                else
                  ConclusionElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Codes for the conclusion
        /// </summary>
        [FhirElement("codedDiagnosis", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> CodedDiagnosis { get; set; }
        
        /// <summary>
        /// Entire Report as issued
        /// </summary>
        [FhirElement("presentedForm", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> PresentedForm { get; set; }
        
    }
    
}
