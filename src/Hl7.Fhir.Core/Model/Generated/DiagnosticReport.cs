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

        [FhirType("PerformerComponent")]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// Type of performer
            /// </summary>
            [FhirElement("role", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Practitioner or Organization  participant
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Role != null) yield return Role;
                    if (Actor != null) yield return Actor;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                }
            }

            
        }
        
        
        [FhirType("ImageComponent")]
        [DataContract]
        public partial class ImageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ImageComponent"; } }
            
            /// <summary>
            /// Comment about the image (e.g. explanation)
            /// </summary>
            [FhirElement("comment", Order=40)]
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
                    if (value == null)
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
            [CLSCompliant(false)]
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
                var dest = other as ImageComponent;
                
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
                return CopyTo(new ImageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CommentElement != null) yield return CommentElement;
                    if (Link != null) yield return Link;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                    if (Link != null) yield return new ElementValue("link", Link);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier for report
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
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
        [FhirElement("basedOn", Order=100)]
        [CLSCompliant(false)]
		[References("CarePlan","ImmunizationRecommendation","MedicationRequest","NutritionOrder","ProcedureRequest","ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// registered | partial | preliminary | final +
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus> _StatusElement;
        
        /// <summary>
        /// registered | partial | preliminary | final +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Service category
        /// </summary>
        [FhirElement("category", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// Name/Code for this diagnostic report
        /// </summary>
        [FhirElement("code", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// The subject of the report - usually, but not always, the patient
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient","Group","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Health care event when test ordered
        /// </summary>
        [FhirElement("context", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Clinically relevant time/time-period for report
        /// </summary>
        [FhirElement("effective", InSummary=true, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// DateTime this version was released
        /// </summary>
        [FhirElement("issued", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Instant IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _IssuedElement;
        
        /// <summary>
        /// DateTime this version was released
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// Participants in producing the report
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DiagnosticReport.PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.DiagnosticReport.PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.DiagnosticReport.PerformerComponent> _Performer;
        
        /// <summary>
        /// Specimens this report is based on
        /// </summary>
        [FhirElement("specimen", Order=190)]
        [CLSCompliant(false)]
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
        [CLSCompliant(false)]
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
        [CLSCompliant(false)]
		[References("ImagingStudy","ImagingManifest")]
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
        public List<Hl7.Fhir.Model.DiagnosticReport.ImageComponent> Image
        {
            get { if(_Image==null) _Image = new List<Hl7.Fhir.Model.DiagnosticReport.ImageComponent>(); return _Image; }
            set { _Image = value; OnPropertyChanged("Image"); }
        }
        
        private List<Hl7.Fhir.Model.DiagnosticReport.ImageComponent> _Image;
        
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
                if (value == null)
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
        /// Entire report as issued
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
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DiagnosticReport;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.Instant)IssuedElement.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.DiagnosticReport.PerformerComponent>(Performer.DeepCopy());
                if(Specimen != null) dest.Specimen = new List<Hl7.Fhir.Model.ResourceReference>(Specimen.DeepCopy());
                if(Result != null) dest.Result = new List<Hl7.Fhir.Model.ResourceReference>(Result.DeepCopy());
                if(ImagingStudy != null) dest.ImagingStudy = new List<Hl7.Fhir.Model.ResourceReference>(ImagingStudy.DeepCopy());
                if(Image != null) dest.Image = new List<Hl7.Fhir.Model.DiagnosticReport.ImageComponent>(Image.DeepCopy());
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
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
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
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Result, otherT.Result)) return false;
            if( !DeepComparable.IsExactly(ImagingStudy, otherT.ImagingStudy)) return false;
            if( !DeepComparable.IsExactly(Image, otherT.Image)) return false;
            if( !DeepComparable.IsExactly(ConclusionElement, otherT.ConclusionElement)) return false;
            if( !DeepComparable.IsExactly(CodedDiagnosis, otherT.CodedDiagnosis)) return false;
            if( !DeepComparable.IsExactly(PresentedForm, otherT.PresentedForm)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Category != null) yield return Category;
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Effective != null) yield return Effective;
				if (IssuedElement != null) yield return IssuedElement;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				foreach (var elem in Specimen) { if (elem != null) yield return elem; }
				foreach (var elem in Result) { if (elem != null) yield return elem; }
				foreach (var elem in ImagingStudy) { if (elem != null) yield return elem; }
				foreach (var elem in Image) { if (elem != null) yield return elem; }
				if (ConclusionElement != null) yield return ConclusionElement;
				foreach (var elem in CodedDiagnosis) { if (elem != null) yield return elem; }
				foreach (var elem in PresentedForm) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Category != null) yield return new ElementValue("category", Category);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Effective != null) yield return new ElementValue("effective", Effective);
                if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                foreach (var elem in Specimen) { if (elem != null) yield return new ElementValue("specimen", elem); }
                foreach (var elem in Result) { if (elem != null) yield return new ElementValue("result", elem); }
                foreach (var elem in ImagingStudy) { if (elem != null) yield return new ElementValue("imagingStudy", elem); }
                foreach (var elem in Image) { if (elem != null) yield return new ElementValue("image", elem); }
                if (ConclusionElement != null) yield return new ElementValue("conclusion", ConclusionElement);
                foreach (var elem in CodedDiagnosis) { if (elem != null) yield return new ElementValue("codedDiagnosis", elem); }
                foreach (var elem in PresentedForm) { if (elem != null) yield return new ElementValue("presentedForm", elem); }
            }
        }

    }
    
}
