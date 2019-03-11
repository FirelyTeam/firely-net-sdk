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
    /// PaymentReconciliation resource
    /// </summary>
    [FhirType("PaymentReconciliation", IsResource=true)]
    [DataContract]
    public partial class PaymentReconciliation : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.PaymentReconciliation; } }
        [NotMapped]
        public override string TypeName { get { return "PaymentReconciliation"; } }
        
        [FhirType("DetailsComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class DetailsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DetailsComponent"; } }
            
            /// <summary>
            /// Business identifier of the payment detail
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Business identifier of the prior payment detail
            /// </summary>
            [FhirElement("predecessor", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Predecessor
            {
                get { return _Predecessor; }
                set { _Predecessor = value; OnPropertyChanged("Predecessor"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Predecessor;
            
            /// <summary>
            /// Category of payment
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Request giving rise to the payment
            /// </summary>
            [FhirElement("request", Order=70)]
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Request
            {
                get { return _Request; }
                set { _Request = value; OnPropertyChanged("Request"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Request;
            
            /// <summary>
            /// Submitter of the request
            /// </summary>
            [FhirElement("submitter", Order=80)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Submitter
            {
                get { return _Submitter; }
                set { _Submitter = value; OnPropertyChanged("Submitter"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Submitter;
            
            /// <summary>
            /// Response committing to a payment
            /// </summary>
            [FhirElement("response", Order=90)]
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Response
            {
                get { return _Response; }
                set { _Response = value; OnPropertyChanged("Response"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Response;
            
            /// <summary>
            /// Date of commitment to pay
            /// </summary>
            [FhirElement("date", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// Date of commitment to pay
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
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Contact for the response
            /// </summary>
            [FhirElement("responsible", Order=110)]
            [CLSCompliant(false)]
			[References("PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Responsible
            {
                get { return _Responsible; }
                set { _Responsible = value; OnPropertyChanged("Responsible"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Responsible;
            
            /// <summary>
            /// Recipient of the payment
            /// </summary>
            [FhirElement("payee", Order=120)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Payee
            {
                get { return _Payee; }
                set { _Payee = value; OnPropertyChanged("Payee"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Payee;
            
            /// <summary>
            /// Amount allocated to this payable
            /// </summary>
            [FhirElement("amount", Order=130)]
            [DataMember]
            public Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Money _Amount;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Predecessor != null) dest.Predecessor = (Hl7.Fhir.Model.Identifier)Predecessor.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                    if(Submitter != null) dest.Submitter = (Hl7.Fhir.Model.ResourceReference)Submitter.DeepCopy();
                    if(Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Responsible != null) dest.Responsible = (Hl7.Fhir.Model.ResourceReference)Responsible.DeepCopy();
                    if(Payee != null) dest.Payee = (Hl7.Fhir.Model.ResourceReference)Payee.DeepCopy();
                    if(Amount != null) dest.Amount = (Money)Amount.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DetailsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Predecessor, otherT.Predecessor)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Request, otherT.Request)) return false;
                if( !DeepComparable.Matches(Submitter, otherT.Submitter)) return false;
                if( !DeepComparable.Matches(Response, otherT.Response)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Responsible, otherT.Responsible)) return false;
                if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Predecessor, otherT.Predecessor)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
                if( !DeepComparable.IsExactly(Submitter, otherT.Submitter)) return false;
                if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Responsible, otherT.Responsible)) return false;
                if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Predecessor != null) yield return Predecessor;
                    if (Type != null) yield return Type;
                    if (Request != null) yield return Request;
                    if (Submitter != null) yield return Submitter;
                    if (Response != null) yield return Response;
                    if (DateElement != null) yield return DateElement;
                    if (Responsible != null) yield return Responsible;
                    if (Payee != null) yield return Payee;
                    if (Amount != null) yield return Amount;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Predecessor != null) yield return new ElementValue("predecessor", Predecessor);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Request != null) yield return new ElementValue("request", Request);
                    if (Submitter != null) yield return new ElementValue("submitter", Submitter);
                    if (Response != null) yield return new ElementValue("response", Response);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Responsible != null) yield return new ElementValue("responsible", Responsible);
                    if (Payee != null) yield return new ElementValue("payee", Payee);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }

            
        }
        
        
        [FhirType("NotesComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class NotesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "NotesComponent"; } }
            
            /// <summary>
            /// display | print | printoper
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.NoteType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.NoteType> _TypeElement;
            
            /// <summary>
            /// display | print | printoper
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.NoteType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.NoteType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Note explanatory text
            /// </summary>
            [FhirElement("text", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Note explanatory text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NotesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.NoteType>)TypeElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NotesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NotesComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NotesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (TextElement != null) yield return TextElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for a payment reconciliation
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Period covered
        /// </summary>
        [FhirElement("period", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Party generating payment
        /// </summary>
        [FhirElement("paymentIssuer", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PaymentIssuer
        {
            get { return _PaymentIssuer; }
            set { _PaymentIssuer = value; OnPropertyChanged("PaymentIssuer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PaymentIssuer;
        
        /// <summary>
        /// Reference to requesting resource
        /// </summary>
        [FhirElement("request", Order=140)]
        [CLSCompliant(false)]
		[References("Task")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        [FhirElement("requestor", Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requestor
        {
            get { return _Requestor; }
            set { _Requestor = value; OnPropertyChanged("Requestor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requestor;
        
        /// <summary>
        /// queued | complete | error | partial
        /// </summary>
        [FhirElement("outcome", Order=160)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ClaimProcessingCodes> OutcomeElement
        {
            get { return _OutcomeElement; }
            set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ClaimProcessingCodes> _OutcomeElement;
        
        /// <summary>
        /// queued | complete | error | partial
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ClaimProcessingCodes? Outcome
        {
            get { return OutcomeElement != null ? OutcomeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  OutcomeElement = null; 
                else
                  OutcomeElement = new Code<Hl7.Fhir.Model.ClaimProcessingCodes>(value);
                OnPropertyChanged("Outcome");
            }
        }
        
        /// <summary>
        /// Disposition message
        /// </summary>
        [FhirElement("disposition", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DispositionElement
        {
            get { return _DispositionElement; }
            set { _DispositionElement = value; OnPropertyChanged("DispositionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DispositionElement;
        
        /// <summary>
        /// Disposition message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Disposition
        {
            get { return DispositionElement != null ? DispositionElement.Value : null; }
            set
            {
                if (value == null)
                  DispositionElement = null; 
                else
                  DispositionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Disposition");
            }
        }
        
        /// <summary>
        /// When payment issued
        /// </summary>
        [FhirElement("paymentDate", InSummary=true, Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Date PaymentDateElement
        {
            get { return _PaymentDateElement; }
            set { _PaymentDateElement = value; OnPropertyChanged("PaymentDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _PaymentDateElement;
        
        /// <summary>
        /// When payment issued
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PaymentDate
        {
            get { return PaymentDateElement != null ? PaymentDateElement.Value : null; }
            set
            {
                if (value == null)
                  PaymentDateElement = null; 
                else
                  PaymentDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("PaymentDate");
            }
        }
        
        /// <summary>
        /// Total amount of Payment
        /// </summary>
        [FhirElement("paymentAmount", InSummary=true, Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Money PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; OnPropertyChanged("PaymentAmount"); }
        }
        
        private Money _PaymentAmount;
        
        /// <summary>
        /// Business identifier for the payment
        /// </summary>
        [FhirElement("paymentIdentifier", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier PaymentIdentifier
        {
            get { return _PaymentIdentifier; }
            set { _PaymentIdentifier = value; OnPropertyChanged("PaymentIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _PaymentIdentifier;
        
        /// <summary>
        /// Settlement particulars
        /// </summary>
        [FhirElement("detail", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent> Detail
        {
            get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent>(); return _Detail; }
            set { _Detail = value; OnPropertyChanged("Detail"); }
        }
        
        private List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent> _Detail;
        
        /// <summary>
        /// Printed form identifier
        /// </summary>
        [FhirElement("formCode", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FormCode
        {
            get { return _FormCode; }
            set { _FormCode = value; OnPropertyChanged("FormCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FormCode;
        
        /// <summary>
        /// Note concerning processing
        /// </summary>
        [FhirElement("processNote", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.PaymentReconciliation.NotesComponent> ProcessNote
        {
            get { if(_ProcessNote==null) _ProcessNote = new List<Hl7.Fhir.Model.PaymentReconciliation.NotesComponent>(); return _ProcessNote; }
            set { _ProcessNote = value; OnPropertyChanged("ProcessNote"); }
        }
        
        private List<Hl7.Fhir.Model.PaymentReconciliation.NotesComponent> _ProcessNote;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as PaymentReconciliation;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(PaymentIssuer != null) dest.PaymentIssuer = (Hl7.Fhir.Model.ResourceReference)PaymentIssuer.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Requestor != null) dest.Requestor = (Hl7.Fhir.Model.ResourceReference)Requestor.DeepCopy();
                if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.ClaimProcessingCodes>)OutcomeElement.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(PaymentDateElement != null) dest.PaymentDateElement = (Hl7.Fhir.Model.Date)PaymentDateElement.DeepCopy();
                if(PaymentAmount != null) dest.PaymentAmount = (Money)PaymentAmount.DeepCopy();
                if(PaymentIdentifier != null) dest.PaymentIdentifier = (Hl7.Fhir.Model.Identifier)PaymentIdentifier.DeepCopy();
                if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent>(Detail.DeepCopy());
                if(FormCode != null) dest.FormCode = (Hl7.Fhir.Model.CodeableConcept)FormCode.DeepCopy();
                if(ProcessNote != null) dest.ProcessNote = new List<Hl7.Fhir.Model.PaymentReconciliation.NotesComponent>(ProcessNote.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new PaymentReconciliation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as PaymentReconciliation;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(PaymentIssuer, otherT.PaymentIssuer)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Requestor, otherT.Requestor)) return false;
            if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(PaymentDateElement, otherT.PaymentDateElement)) return false;
            if( !DeepComparable.Matches(PaymentAmount, otherT.PaymentAmount)) return false;
            if( !DeepComparable.Matches(PaymentIdentifier, otherT.PaymentIdentifier)) return false;
            if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            if( !DeepComparable.Matches(FormCode, otherT.FormCode)) return false;
            if( !DeepComparable.Matches(ProcessNote, otherT.ProcessNote)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as PaymentReconciliation;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(PaymentIssuer, otherT.PaymentIssuer)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Requestor, otherT.Requestor)) return false;
            if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(PaymentDateElement, otherT.PaymentDateElement)) return false;
            if( !DeepComparable.IsExactly(PaymentAmount, otherT.PaymentAmount)) return false;
            if( !DeepComparable.IsExactly(PaymentIdentifier, otherT.PaymentIdentifier)) return false;
            if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            if( !DeepComparable.IsExactly(FormCode, otherT.FormCode)) return false;
            if( !DeepComparable.IsExactly(ProcessNote, otherT.ProcessNote)) return false;
            
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
				if (Period != null) yield return Period;
				if (CreatedElement != null) yield return CreatedElement;
				if (PaymentIssuer != null) yield return PaymentIssuer;
				if (Request != null) yield return Request;
				if (Requestor != null) yield return Requestor;
				if (OutcomeElement != null) yield return OutcomeElement;
				if (DispositionElement != null) yield return DispositionElement;
				if (PaymentDateElement != null) yield return PaymentDateElement;
				if (PaymentAmount != null) yield return PaymentAmount;
				if (PaymentIdentifier != null) yield return PaymentIdentifier;
				foreach (var elem in Detail) { if (elem != null) yield return elem; }
				if (FormCode != null) yield return FormCode;
				foreach (var elem in ProcessNote) { if (elem != null) yield return elem; }
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
                if (Period != null) yield return new ElementValue("period", Period);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (PaymentIssuer != null) yield return new ElementValue("paymentIssuer", PaymentIssuer);
                if (Request != null) yield return new ElementValue("request", Request);
                if (Requestor != null) yield return new ElementValue("requestor", Requestor);
                if (OutcomeElement != null) yield return new ElementValue("outcome", OutcomeElement);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (PaymentDateElement != null) yield return new ElementValue("paymentDate", PaymentDateElement);
                if (PaymentAmount != null) yield return new ElementValue("paymentAmount", PaymentAmount);
                if (PaymentIdentifier != null) yield return new ElementValue("paymentIdentifier", PaymentIdentifier);
                foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                if (FormCode != null) yield return new ElementValue("formCode", FormCode);
                foreach (var elem in ProcessNote) { if (elem != null) yield return new ElementValue("processNote", elem); }
            }
        }

    }
    
}
