using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A Diagnostic report - a combination of request information, atomic results, images, interpretation, as well as formatted reports
    /// </summary>
    [FhirType("DiagnosticReport", IsResource=true)]
    [DataContract]
    public partial class DiagnosticReport : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DiagnosticReport; } }
        [NotMapped]
        public override string TypeName { get { return "DiagnosticReport"; } }
        
        /// <summary>
        /// The status of the diagnostic report as a whole
        /// </summary>
        [FhirEnumeration("DiagnosticReportStatus")]
        public enum DiagnosticReportStatus
        {
            /// <summary>
            /// The existence of the report is registered, but there is nothing yet available.
            /// </summary>
            [EnumLiteral("registered")]
            Registered,
            /// <summary>
            /// This is a partial (e.g. initial, interim or preliminary) report: data in the report may be incomplete or unverified.
            /// </summary>
            [EnumLiteral("partial")]
            Partial,
            /// <summary>
            /// The report is complete and verified by an authorized person.
            /// </summary>
            [EnumLiteral("final")]
            Final,
            /// <summary>
            /// The report has been modified subsequent to being Final, and is complete and verified by an authorized person.
            /// </summary>
            [EnumLiteral("corrected")]
            Corrected,
            /// <summary>
            /// The report has been modified subsequent to being Final, and is complete and verified by an authorized person. New content has been added, but existing content hasn't changed.
            /// </summary>
            [EnumLiteral("appended")]
            Appended,
            /// <summary>
            /// The report is unavailable because the measurement was not started or not completed (also sometimes called "aborted").
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
            /// <summary>
            /// The report has been withdrawn following previous Final release.
            /// </summary>
            [EnumLiteral("entered-in-error")]
            EnteredInError,
        }
        
        [FhirType("DiagnosticReportImageComponent")]
        [DataContract]
        public partial class DiagnosticReportImageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DiagnosticReportImageComponent"; } }
            
            /// <summary>
            /// Comment about the image (e.g. explanation)
            /// </summary>
            [FhirElement("comment", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Comment about the image (e.g. explanation)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                    OnPropertyChanged("Comment");
                }
            }
            
            /// <summary>
            /// Reference to the image source
            /// </summary>
            [FhirElement("link", InSummary=true, Order=50)]
            [References("Media")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Link
            {
                get { return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Link;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosticReportImageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    if(Link != null) dest.Link = (Hl7.Fhir.Model.ResourceReference)Link.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosticReportImageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosticReportImageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosticReportImageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Name/Code for this diagnostic report
        /// </summary>
        [FhirElement("name", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Name;
        
        /// <summary>
        /// registered | partial | final | corrected | appended | cancelled | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus> _StatusElement;
        
        /// <summary>
        /// registered | partial | final | corrected | appended | cancelled | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Date this version was released
        /// </summary>
        [FhirElement("issued", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
        
        /// <summary>
        /// Date this version was released
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// The subject of the report, usually, but not always, the patient
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=120)]
        [References("Patient","Group","Device","Location")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Responsible Diagnostic Service
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=130)]
        [References("Practitioner","Organization")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// Health care event when test ordered
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=140)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Id for external references to this report
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// What was requested
        /// </summary>
        [FhirElement("requestDetail", Order=160)]
        [References("DiagnosticOrder")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> RequestDetail
        {
            get { if(_RequestDetail==null) _RequestDetail = new List<Hl7.Fhir.Model.ResourceReference>(); return _RequestDetail; }
            set { _RequestDetail = value; OnPropertyChanged("RequestDetail"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _RequestDetail;
        
        /// <summary>
        /// Biochemistry, Hematology etc.
        /// </summary>
        [FhirElement("serviceCategory", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ServiceCategory
        {
            get { return _ServiceCategory; }
            set { _ServiceCategory = value; OnPropertyChanged("ServiceCategory"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ServiceCategory;
        
        /// <summary>
        /// Physiologically Relevant time/time-period for report
        /// </summary>
        [FhirElement("diagnostic", InSummary=true, Order=180, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Diagnostic
        {
            get { return _Diagnostic; }
            set { _Diagnostic = value; OnPropertyChanged("Diagnostic"); }
        }
        
        private Hl7.Fhir.Model.Element _Diagnostic;
        
        /// <summary>
        /// Specimens this report is based on
        /// </summary>
        [FhirElement("specimen", Order=190)]
        [References("Specimen")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Specimen
        {
            get { if(_Specimen==null) _Specimen = new List<Hl7.Fhir.Model.ResourceReference>(); return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Specimen;
        
        /// <summary>
        /// Observations - simple, or complex nested groups
        /// </summary>
        [FhirElement("result", Order=200)]
        [References("Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Result
        {
            get { if(_Result==null) _Result = new List<Hl7.Fhir.Model.ResourceReference>(); return _Result; }
            set { _Result = value; OnPropertyChanged("Result"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Result;
        
        /// <summary>
        /// Reference to full details of imaging associated with the diagnostic report
        /// </summary>
        [FhirElement("imagingStudy", Order=210)]
        [References("ImagingStudy")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ImagingStudy
        {
            get { if(_ImagingStudy==null) _ImagingStudy = new List<Hl7.Fhir.Model.ResourceReference>(); return _ImagingStudy; }
            set { _ImagingStudy = value; OnPropertyChanged("ImagingStudy"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ImagingStudy;
        
        /// <summary>
        /// Key images associated with this report
        /// </summary>
        [FhirElement("image", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportImageComponent> Image
        {
            get { if(_Image==null) _Image = new List<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportImageComponent>(); return _Image; }
            set { _Image = value; OnPropertyChanged("Image"); }
        }
        
        private List<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportImageComponent> _Image;
        
        /// <summary>
        /// Clinical Interpretation of test results
        /// </summary>
        [FhirElement("conclusion", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ConclusionElement
        {
            get { return _ConclusionElement; }
            set { _ConclusionElement = value; OnPropertyChanged("ConclusionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ConclusionElement;
        
        /// <summary>
        /// Clinical Interpretation of test results
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                OnPropertyChanged("Conclusion");
            }
        }
        
        /// <summary>
        /// Codes for the conclusion
        /// </summary>
        [FhirElement("codedDiagnosis", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> CodedDiagnosis
        {
            get { if(_CodedDiagnosis==null) _CodedDiagnosis = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CodedDiagnosis; }
            set { _CodedDiagnosis = value; OnPropertyChanged("CodedDiagnosis"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _CodedDiagnosis;
        
        /// <summary>
        /// Entire Report as issued
        /// </summary>
        [FhirElement("presentedForm", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> PresentedForm
        {
            get { if(_PresentedForm==null) _PresentedForm = new List<Hl7.Fhir.Model.Attachment>(); return _PresentedForm; }
            set { _PresentedForm = value; OnPropertyChanged("PresentedForm"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _PresentedForm;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DiagnosticReport;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Name != null) dest.Name = (Hl7.Fhir.Model.CodeableConcept)Name.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus>)StatusElement.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(RequestDetail != null) dest.RequestDetail = new List<Hl7.Fhir.Model.ResourceReference>(RequestDetail.DeepCopy());
                if(ServiceCategory != null) dest.ServiceCategory = (Hl7.Fhir.Model.CodeableConcept)ServiceCategory.DeepCopy();
                if(Diagnostic != null) dest.Diagnostic = (Hl7.Fhir.Model.Element)Diagnostic.DeepCopy();
                if(Specimen != null) dest.Specimen = new List<Hl7.Fhir.Model.ResourceReference>(Specimen.DeepCopy());
                if(Result != null) dest.Result = new List<Hl7.Fhir.Model.ResourceReference>(Result.DeepCopy());
                if(ImagingStudy != null) dest.ImagingStudy = new List<Hl7.Fhir.Model.ResourceReference>(ImagingStudy.DeepCopy());
                if(Image != null) dest.Image = new List<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportImageComponent>(Image.DeepCopy());
                if(ConclusionElement != null) dest.ConclusionElement = (Hl7.Fhir.Model.FhirString)ConclusionElement.DeepCopy();
                if(CodedDiagnosis != null) dest.CodedDiagnosis = new List<Hl7.Fhir.Model.CodeableConcept>(CodedDiagnosis.DeepCopy());
                if(PresentedForm != null) dest.PresentedForm = new List<Hl7.Fhir.Model.Attachment>(PresentedForm.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DiagnosticReport());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DiagnosticReport;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(RequestDetail, otherT.RequestDetail)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(Diagnostic, otherT.Diagnostic)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(Result, otherT.Result)) return false;
            if( !DeepComparable.Matches(ImagingStudy, otherT.ImagingStudy)) return false;
            if( !DeepComparable.Matches(Image, otherT.Image)) return false;
            if( !DeepComparable.Matches(ConclusionElement, otherT.ConclusionElement)) return false;
            if( !DeepComparable.Matches(CodedDiagnosis, otherT.CodedDiagnosis)) return false;
            if( !DeepComparable.Matches(PresentedForm, otherT.PresentedForm)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DiagnosticReport;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(RequestDetail, otherT.RequestDetail)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(Diagnostic, otherT.Diagnostic)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Result, otherT.Result)) return false;
            if( !DeepComparable.IsExactly(ImagingStudy, otherT.ImagingStudy)) return false;
            if( !DeepComparable.IsExactly(Image, otherT.Image)) return false;
            if( !DeepComparable.IsExactly(ConclusionElement, otherT.ConclusionElement)) return false;
            if( !DeepComparable.IsExactly(CodedDiagnosis, otherT.CodedDiagnosis)) return false;
            if( !DeepComparable.IsExactly(PresentedForm, otherT.PresentedForm)) return false;
            
            return true;
        }
        
    }
    
}
