﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Immunization event information
    /// </summary>
    [FhirType("Immunization", IsResource=true)]
    [DataContract]
    public partial class Immunization : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Immunization; } }
        [NotMapped]
        public override string TypeName { get { return "Immunization"; } }
        
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

        [FhirType("PerformerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// What type of performance was done
            /// </summary>
            [FhirElement("function", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Function
            {
                get { return _Function; }
                set { _Function = value; OnPropertyChanged("Function"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Function;
            
            /// <summary>
            /// Individual or organization who was performing
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization")]
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
                    if(Function != null) dest.Function = (Hl7.Fhir.Model.CodeableConcept)Function.DeepCopy();
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
                if( !DeepComparable.Matches(Function, otherT.Function)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Function, otherT.Function)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Function != null) yield return Function;
                    if (Actor != null) yield return Actor;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Function != null) yield return new ElementValue("function", Function);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                }
            }

            
        }
        
        
        [FhirType("EducationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class EducationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EducationComponent"; } }
            
            /// <summary>
            /// Educational material document identifier
            /// </summary>
            [FhirElement("documentType", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentTypeElement
            {
                get { return _DocumentTypeElement; }
                set { _DocumentTypeElement = value; OnPropertyChanged("DocumentTypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentTypeElement;
            
            /// <summary>
            /// Educational material document identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DocumentType
            {
                get { return DocumentTypeElement != null ? DocumentTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        DocumentTypeElement = null; 
                    else
                        DocumentTypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("DocumentType");
                }
            }
            
            /// <summary>
            /// Educational material reference pointer
            /// </summary>
            [FhirElement("reference", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ReferenceElement
            {
                get { return _ReferenceElement; }
                set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _ReferenceElement;
            
            /// <summary>
            /// Educational material reference pointer
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Reference
            {
                get { return ReferenceElement != null ? ReferenceElement.Value : null; }
                set
                {
                    if (value == null)
                        ReferenceElement = null; 
                    else
                        ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Reference");
                }
            }
            
            /// <summary>
            /// Educational material publication date
            /// </summary>
            [FhirElement("publicationDate", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime PublicationDateElement
            {
                get { return _PublicationDateElement; }
                set { _PublicationDateElement = value; OnPropertyChanged("PublicationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _PublicationDateElement;
            
            /// <summary>
            /// Educational material publication date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PublicationDate
            {
                get { return PublicationDateElement != null ? PublicationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        PublicationDateElement = null; 
                    else
                        PublicationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("PublicationDate");
                }
            }
            
            /// <summary>
            /// Educational material presentation date
            /// </summary>
            [FhirElement("presentationDate", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime PresentationDateElement
            {
                get { return _PresentationDateElement; }
                set { _PresentationDateElement = value; OnPropertyChanged("PresentationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _PresentationDateElement;
            
            /// <summary>
            /// Educational material presentation date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PresentationDate
            {
                get { return PresentationDateElement != null ? PresentationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        PresentationDateElement = null; 
                    else
                        PresentationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("PresentationDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EducationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DocumentTypeElement != null) dest.DocumentTypeElement = (Hl7.Fhir.Model.FhirString)DocumentTypeElement.DeepCopy();
                    if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
                    if(PublicationDateElement != null) dest.PublicationDateElement = (Hl7.Fhir.Model.FhirDateTime)PublicationDateElement.DeepCopy();
                    if(PresentationDateElement != null) dest.PresentationDateElement = (Hl7.Fhir.Model.FhirDateTime)PresentationDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EducationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EducationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DocumentTypeElement, otherT.DocumentTypeElement)) return false;
                if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
                if( !DeepComparable.Matches(PublicationDateElement, otherT.PublicationDateElement)) return false;
                if( !DeepComparable.Matches(PresentationDateElement, otherT.PresentationDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EducationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DocumentTypeElement, otherT.DocumentTypeElement)) return false;
                if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
                if( !DeepComparable.IsExactly(PublicationDateElement, otherT.PublicationDateElement)) return false;
                if( !DeepComparable.IsExactly(PresentationDateElement, otherT.PresentationDateElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DocumentTypeElement != null) yield return DocumentTypeElement;
                    if (ReferenceElement != null) yield return ReferenceElement;
                    if (PublicationDateElement != null) yield return PublicationDateElement;
                    if (PresentationDateElement != null) yield return PresentationDateElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DocumentTypeElement != null) yield return new ElementValue("documentType", DocumentTypeElement);
                    if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
                    if (PublicationDateElement != null) yield return new ElementValue("publicationDate", PublicationDateElement);
                    if (PresentationDateElement != null) yield return new ElementValue("presentationDate", PresentationDateElement);
                }
            }

            
        }
        
        
        [FhirType("ReactionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ReactionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ReactionComponent"; } }
            
            /// <summary>
            /// When reaction started
            /// </summary>
            [FhirElement("date", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When reaction started
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
            /// Additional information on reaction
            /// </summary>
            [FhirElement("detail", Order=50)]
            [CLSCompliant(false)]
			[References("Observation")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Detail;
            
            /// <summary>
            /// Indicates self-reported reaction
            /// </summary>
            [FhirElement("reported", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReportedElement
            {
                get { return _ReportedElement; }
                set { _ReportedElement = value; OnPropertyChanged("ReportedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ReportedElement;
            
            /// <summary>
            /// Indicates self-reported reaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Reported
            {
                get { return ReportedElement != null ? ReportedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ReportedElement = null; 
                    else
                        ReportedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Reported");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReactionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Detail != null) dest.Detail = (Hl7.Fhir.Model.ResourceReference)Detail.DeepCopy();
                    if(ReportedElement != null) dest.ReportedElement = (Hl7.Fhir.Model.FhirBoolean)ReportedElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ReactionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReactionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(ReportedElement, otherT.ReportedElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReactionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(ReportedElement, otherT.ReportedElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DateElement != null) yield return DateElement;
                    if (Detail != null) yield return Detail;
                    if (ReportedElement != null) yield return ReportedElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Detail != null) yield return new ElementValue("detail", Detail);
                    if (ReportedElement != null) yield return new ElementValue("reported", ReportedElement);
                }
            }

            
        }
        
        
        [FhirType("ProtocolAppliedComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ProtocolAppliedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProtocolAppliedComponent"; } }
            
            /// <summary>
            /// Name of vaccine series
            /// </summary>
            [FhirElement("series", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SeriesElement
            {
                get { return _SeriesElement; }
                set { _SeriesElement = value; OnPropertyChanged("SeriesElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SeriesElement;
            
            /// <summary>
            /// Name of vaccine series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Series
            {
                get { return SeriesElement != null ? SeriesElement.Value : null; }
                set
                {
                    if (value == null)
                        SeriesElement = null; 
                    else
                        SeriesElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Series");
                }
            }
            
            /// <summary>
            /// Who is responsible for publishing the recommendations
            /// </summary>
            [FhirElement("authority", Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Authority
            {
                get { return _Authority; }
                set { _Authority = value; OnPropertyChanged("Authority"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Authority;
            
            /// <summary>
            /// Vaccine preventatable disease being targetted
            /// </summary>
            [FhirElement("targetDisease", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> TargetDisease
            {
                get { if(_TargetDisease==null) _TargetDisease = new List<Hl7.Fhir.Model.CodeableConcept>(); return _TargetDisease; }
                set { _TargetDisease = value; OnPropertyChanged("TargetDisease"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _TargetDisease;
            
            /// <summary>
            /// Dose number within series
            /// </summary>
            [FhirElement("doseNumber", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element DoseNumber
            {
                get { return _DoseNumber; }
                set { _DoseNumber = value; OnPropertyChanged("DoseNumber"); }
            }
            
            private Hl7.Fhir.Model.Element _DoseNumber;
            
            /// <summary>
            /// Recommended number of doses for immunity
            /// </summary>
            [FhirElement("seriesDoses", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element SeriesDoses
            {
                get { return _SeriesDoses; }
                set { _SeriesDoses = value; OnPropertyChanged("SeriesDoses"); }
            }
            
            private Hl7.Fhir.Model.Element _SeriesDoses;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProtocolAppliedComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SeriesElement != null) dest.SeriesElement = (Hl7.Fhir.Model.FhirString)SeriesElement.DeepCopy();
                    if(Authority != null) dest.Authority = (Hl7.Fhir.Model.ResourceReference)Authority.DeepCopy();
                    if(TargetDisease != null) dest.TargetDisease = new List<Hl7.Fhir.Model.CodeableConcept>(TargetDisease.DeepCopy());
                    if(DoseNumber != null) dest.DoseNumber = (Hl7.Fhir.Model.Element)DoseNumber.DeepCopy();
                    if(SeriesDoses != null) dest.SeriesDoses = (Hl7.Fhir.Model.Element)SeriesDoses.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProtocolAppliedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProtocolAppliedComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SeriesElement, otherT.SeriesElement)) return false;
                if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
                if( !DeepComparable.Matches(TargetDisease, otherT.TargetDisease)) return false;
                if( !DeepComparable.Matches(DoseNumber, otherT.DoseNumber)) return false;
                if( !DeepComparable.Matches(SeriesDoses, otherT.SeriesDoses)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProtocolAppliedComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SeriesElement, otherT.SeriesElement)) return false;
                if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
                if( !DeepComparable.IsExactly(TargetDisease, otherT.TargetDisease)) return false;
                if( !DeepComparable.IsExactly(DoseNumber, otherT.DoseNumber)) return false;
                if( !DeepComparable.IsExactly(SeriesDoses, otherT.SeriesDoses)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SeriesElement != null) yield return SeriesElement;
                    if (Authority != null) yield return Authority;
                    foreach (var elem in TargetDisease) { if (elem != null) yield return elem; }
                    if (DoseNumber != null) yield return DoseNumber;
                    if (SeriesDoses != null) yield return SeriesDoses;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SeriesElement != null) yield return new ElementValue("series", SeriesElement);
                    if (Authority != null) yield return new ElementValue("authority", Authority);
                    foreach (var elem in TargetDisease) { if (elem != null) yield return new ElementValue("targetDisease", elem); }
                    if (DoseNumber != null) yield return new ElementValue("doseNumber", DoseNumber);
                    if (SeriesDoses != null) yield return new ElementValue("seriesDoses", SeriesDoses);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// completed | entered-in-error | not-done
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Immunization.ImmunizationStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Immunization.ImmunizationStatusCodes> _StatusElement;
        
        /// <summary>
        /// completed | entered-in-error | not-done
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Immunization.ImmunizationStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Immunization.ImmunizationStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason not done
        /// </summary>
        [FhirElement("statusReason", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// Vaccine product administered
        /// </summary>
        [FhirElement("vaccineCode", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept VaccineCode
        {
            get { return _VaccineCode; }
            set { _VaccineCode = value; OnPropertyChanged("VaccineCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _VaccineCode;
        
        /// <summary>
        /// Who was immunized
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Encounter immunization was part of
        /// </summary>
        [FhirElement("encounter", Order=140)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Vaccine administration date
        /// </summary>
        [FhirElement("occurrence", InSummary=true, Order=150, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirString))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// When the immunization was first captured in the subject's record
        /// </summary>
        [FhirElement("recorded", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedElement
        {
            get { return _RecordedElement; }
            set { _RecordedElement = value; OnPropertyChanged("RecordedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RecordedElement;
        
        /// <summary>
        /// When the immunization was first captured in the subject's record
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Recorded
        {
            get { return RecordedElement != null ? RecordedElement.Value : null; }
            set
            {
                if (value == null)
                  RecordedElement = null; 
                else
                  RecordedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Recorded");
            }
        }
        
        /// <summary>
        /// Indicates context the data was recorded in
        /// </summary>
        [FhirElement("primarySource", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean PrimarySourceElement
        {
            get { return _PrimarySourceElement; }
            set { _PrimarySourceElement = value; OnPropertyChanged("PrimarySourceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _PrimarySourceElement;
        
        /// <summary>
        /// Indicates context the data was recorded in
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? PrimarySource
        {
            get { return PrimarySourceElement != null ? PrimarySourceElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  PrimarySourceElement = null; 
                else
                  PrimarySourceElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("PrimarySource");
            }
        }
        
        /// <summary>
        /// Indicates the source of a secondarily reported record
        /// </summary>
        [FhirElement("reportOrigin", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ReportOrigin
        {
            get { return _ReportOrigin; }
            set { _ReportOrigin = value; OnPropertyChanged("ReportOrigin"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ReportOrigin;
        
        /// <summary>
        /// Where immunization occurred
        /// </summary>
        [FhirElement("location", Order=190)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Vaccine manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=200)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Manufacturer;
        
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        [FhirElement("lotNumber", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LotNumberElement
        {
            get { return _LotNumberElement; }
            set { _LotNumberElement = value; OnPropertyChanged("LotNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _LotNumberElement;
        
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LotNumber
        {
            get { return LotNumberElement != null ? LotNumberElement.Value : null; }
            set
            {
                if (value == null)
                  LotNumberElement = null; 
                else
                  LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("LotNumber");
            }
        }
        
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        [FhirElement("expirationDate", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Date ExpirationDateElement
        {
            get { return _ExpirationDateElement; }
            set { _ExpirationDateElement = value; OnPropertyChanged("ExpirationDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _ExpirationDateElement;
        
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ExpirationDate
        {
            get { return ExpirationDateElement != null ? ExpirationDateElement.Value : null; }
            set
            {
                if (value == null)
                  ExpirationDateElement = null; 
                else
                  ExpirationDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("ExpirationDate");
            }
        }
        
        /// <summary>
        /// Body site vaccine  was administered
        /// </summary>
        [FhirElement("site", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Site
        {
            get { return _Site; }
            set { _Site = value; OnPropertyChanged("Site"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Site;
        
        /// <summary>
        /// How vaccine entered body
        /// </summary>
        [FhirElement("route", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Route
        {
            get { return _Route; }
            set { _Route = value; OnPropertyChanged("Route"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Route;
        
        /// <summary>
        /// Amount of vaccine administered
        /// </summary>
        [FhirElement("doseQuantity", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity DoseQuantity
        {
            get { return _DoseQuantity; }
            set { _DoseQuantity = value; OnPropertyChanged("DoseQuantity"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _DoseQuantity;
        
        /// <summary>
        /// Who performed event
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.Immunization.PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.Immunization.PerformerComponent> _Performer;
        
        /// <summary>
        /// Additional immunization notes
        /// </summary>
        [FhirElement("note", InSummary=true, Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Why immunization occurred
        /// </summary>
        [FhirElement("reasonCode", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Why immunization occurred
        /// </summary>
        [FhirElement("reasonReference", Order=290)]
        [CLSCompliant(false)]
		[References("Condition","Observation","DiagnosticReport")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Dose potency
        /// </summary>
        [FhirElement("isSubpotent", InSummary=true, Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IsSubpotentElement
        {
            get { return _IsSubpotentElement; }
            set { _IsSubpotentElement = value; OnPropertyChanged("IsSubpotentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IsSubpotentElement;
        
        /// <summary>
        /// Dose potency
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IsSubpotent
        {
            get { return IsSubpotentElement != null ? IsSubpotentElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IsSubpotentElement = null; 
                else
                  IsSubpotentElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IsSubpotent");
            }
        }
        
        /// <summary>
        /// Reason for being subpotent
        /// </summary>
        [FhirElement("subpotentReason", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> SubpotentReason
        {
            get { if(_SubpotentReason==null) _SubpotentReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubpotentReason; }
            set { _SubpotentReason = value; OnPropertyChanged("SubpotentReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _SubpotentReason;
        
        /// <summary>
        /// Educational material presented to patient
        /// </summary>
        [FhirElement("education", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.EducationComponent> Education
        {
            get { if(_Education==null) _Education = new List<Hl7.Fhir.Model.Immunization.EducationComponent>(); return _Education; }
            set { _Education = value; OnPropertyChanged("Education"); }
        }
        
        private List<Hl7.Fhir.Model.Immunization.EducationComponent> _Education;
        
        /// <summary>
        /// Patient eligibility for a vaccination program
        /// </summary>
        [FhirElement("programEligibility", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ProgramEligibility
        {
            get { if(_ProgramEligibility==null) _ProgramEligibility = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramEligibility; }
            set { _ProgramEligibility = value; OnPropertyChanged("ProgramEligibility"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ProgramEligibility;
        
        /// <summary>
        /// Funding source for the vaccine
        /// </summary>
        [FhirElement("fundingSource", Order=340)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FundingSource
        {
            get { return _FundingSource; }
            set { _FundingSource = value; OnPropertyChanged("FundingSource"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FundingSource;
        
        /// <summary>
        /// Details of a reaction that follows immunization
        /// </summary>
        [FhirElement("reaction", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ReactionComponent> Reaction
        {
            get { if(_Reaction==null) _Reaction = new List<Hl7.Fhir.Model.Immunization.ReactionComponent>(); return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        
        private List<Hl7.Fhir.Model.Immunization.ReactionComponent> _Reaction;
        
        /// <summary>
        /// Protocol followed by the provider
        /// </summary>
        [FhirElement("protocolApplied", Order=360)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ProtocolAppliedComponent> ProtocolApplied
        {
            get { if(_ProtocolApplied==null) _ProtocolApplied = new List<Hl7.Fhir.Model.Immunization.ProtocolAppliedComponent>(); return _ProtocolApplied; }
            set { _ProtocolApplied = value; OnPropertyChanged("ProtocolApplied"); }
        }
        
        private List<Hl7.Fhir.Model.Immunization.ProtocolAppliedComponent> _ProtocolApplied;
        

        public static ElementDefinition.ConstraintComponent Immunization_IMM_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "education.all(documentType.exists() or reference.exists())",
            Key = "imm-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "One of documentType or reference SHALL be present",
            Xpath = "exists(f:documentType) or exists(f:reference)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Immunization_IMM_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Immunization;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Immunization.ImmunizationStatusCodes>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(VaccineCode != null) dest.VaccineCode = (Hl7.Fhir.Model.CodeableConcept)VaccineCode.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(RecordedElement != null) dest.RecordedElement = (Hl7.Fhir.Model.FhirDateTime)RecordedElement.DeepCopy();
                if(PrimarySourceElement != null) dest.PrimarySourceElement = (Hl7.Fhir.Model.FhirBoolean)PrimarySourceElement.DeepCopy();
                if(ReportOrigin != null) dest.ReportOrigin = (Hl7.Fhir.Model.CodeableConcept)ReportOrigin.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = (Hl7.Fhir.Model.ResourceReference)Manufacturer.DeepCopy();
                if(LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                if(ExpirationDateElement != null) dest.ExpirationDateElement = (Hl7.Fhir.Model.Date)ExpirationDateElement.DeepCopy();
                if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                if(DoseQuantity != null) dest.DoseQuantity = (Hl7.Fhir.Model.SimpleQuantity)DoseQuantity.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.Immunization.PerformerComponent>(Performer.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(IsSubpotentElement != null) dest.IsSubpotentElement = (Hl7.Fhir.Model.FhirBoolean)IsSubpotentElement.DeepCopy();
                if(SubpotentReason != null) dest.SubpotentReason = new List<Hl7.Fhir.Model.CodeableConcept>(SubpotentReason.DeepCopy());
                if(Education != null) dest.Education = new List<Hl7.Fhir.Model.Immunization.EducationComponent>(Education.DeepCopy());
                if(ProgramEligibility != null) dest.ProgramEligibility = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramEligibility.DeepCopy());
                if(FundingSource != null) dest.FundingSource = (Hl7.Fhir.Model.CodeableConcept)FundingSource.DeepCopy();
                if(Reaction != null) dest.Reaction = new List<Hl7.Fhir.Model.Immunization.ReactionComponent>(Reaction.DeepCopy());
                if(ProtocolApplied != null) dest.ProtocolApplied = new List<Hl7.Fhir.Model.Immunization.ProtocolAppliedComponent>(ProtocolApplied.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Immunization());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Immunization;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(VaccineCode, otherT.VaccineCode)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(RecordedElement, otherT.RecordedElement)) return false;
            if( !DeepComparable.Matches(PrimarySourceElement, otherT.PrimarySourceElement)) return false;
            if( !DeepComparable.Matches(ReportOrigin, otherT.ReportOrigin)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.Matches(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.Matches(Site, otherT.Site)) return false;
            if( !DeepComparable.Matches(Route, otherT.Route)) return false;
            if( !DeepComparable.Matches(DoseQuantity, otherT.DoseQuantity)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(IsSubpotentElement, otherT.IsSubpotentElement)) return false;
            if( !DeepComparable.Matches(SubpotentReason, otherT.SubpotentReason)) return false;
            if( !DeepComparable.Matches(Education, otherT.Education)) return false;
            if( !DeepComparable.Matches(ProgramEligibility, otherT.ProgramEligibility)) return false;
            if( !DeepComparable.Matches(FundingSource, otherT.FundingSource)) return false;
            if( !DeepComparable.Matches(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.Matches(ProtocolApplied, otherT.ProtocolApplied)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Immunization;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(VaccineCode, otherT.VaccineCode)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(RecordedElement, otherT.RecordedElement)) return false;
            if( !DeepComparable.IsExactly(PrimarySourceElement, otherT.PrimarySourceElement)) return false;
            if( !DeepComparable.IsExactly(ReportOrigin, otherT.ReportOrigin)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.IsExactly(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
            if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
            if( !DeepComparable.IsExactly(DoseQuantity, otherT.DoseQuantity)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(IsSubpotentElement, otherT.IsSubpotentElement)) return false;
            if( !DeepComparable.IsExactly(SubpotentReason, otherT.SubpotentReason)) return false;
            if( !DeepComparable.IsExactly(Education, otherT.Education)) return false;
            if( !DeepComparable.IsExactly(ProgramEligibility, otherT.ProgramEligibility)) return false;
            if( !DeepComparable.IsExactly(FundingSource, otherT.FundingSource)) return false;
            if( !DeepComparable.IsExactly(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.IsExactly(ProtocolApplied, otherT.ProtocolApplied)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (StatusReason != null) yield return StatusReason;
				if (VaccineCode != null) yield return VaccineCode;
				if (Patient != null) yield return Patient;
				if (Encounter != null) yield return Encounter;
				if (Occurrence != null) yield return Occurrence;
				if (RecordedElement != null) yield return RecordedElement;
				if (PrimarySourceElement != null) yield return PrimarySourceElement;
				if (ReportOrigin != null) yield return ReportOrigin;
				if (Location != null) yield return Location;
				if (Manufacturer != null) yield return Manufacturer;
				if (LotNumberElement != null) yield return LotNumberElement;
				if (ExpirationDateElement != null) yield return ExpirationDateElement;
				if (Site != null) yield return Site;
				if (Route != null) yield return Route;
				if (DoseQuantity != null) yield return DoseQuantity;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				if (IsSubpotentElement != null) yield return IsSubpotentElement;
				foreach (var elem in SubpotentReason) { if (elem != null) yield return elem; }
				foreach (var elem in Education) { if (elem != null) yield return elem; }
				foreach (var elem in ProgramEligibility) { if (elem != null) yield return elem; }
				if (FundingSource != null) yield return FundingSource;
				foreach (var elem in Reaction) { if (elem != null) yield return elem; }
				foreach (var elem in ProtocolApplied) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                if (VaccineCode != null) yield return new ElementValue("vaccineCode", VaccineCode);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                if (RecordedElement != null) yield return new ElementValue("recorded", RecordedElement);
                if (PrimarySourceElement != null) yield return new ElementValue("primarySource", PrimarySourceElement);
                if (ReportOrigin != null) yield return new ElementValue("reportOrigin", ReportOrigin);
                if (Location != null) yield return new ElementValue("location", Location);
                if (Manufacturer != null) yield return new ElementValue("manufacturer", Manufacturer);
                if (LotNumberElement != null) yield return new ElementValue("lotNumber", LotNumberElement);
                if (ExpirationDateElement != null) yield return new ElementValue("expirationDate", ExpirationDateElement);
                if (Site != null) yield return new ElementValue("site", Site);
                if (Route != null) yield return new ElementValue("route", Route);
                if (DoseQuantity != null) yield return new ElementValue("doseQuantity", DoseQuantity);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                if (IsSubpotentElement != null) yield return new ElementValue("isSubpotent", IsSubpotentElement);
                foreach (var elem in SubpotentReason) { if (elem != null) yield return new ElementValue("subpotentReason", elem); }
                foreach (var elem in Education) { if (elem != null) yield return new ElementValue("education", elem); }
                foreach (var elem in ProgramEligibility) { if (elem != null) yield return new ElementValue("programEligibility", elem); }
                if (FundingSource != null) yield return new ElementValue("fundingSource", FundingSource);
                foreach (var elem in Reaction) { if (elem != null) yield return new ElementValue("reaction", elem); }
                foreach (var elem in ProtocolApplied) { if (elem != null) yield return new ElementValue("protocolApplied", elem); }
            }
        }

    }
    
}
