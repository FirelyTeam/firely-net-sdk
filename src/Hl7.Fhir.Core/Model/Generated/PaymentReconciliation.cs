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
        
        [FhirType("DetailsComponent")]
        [DataContract]
        public partial class DetailsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DetailsComponent"; } }
            
            /// <summary>
            /// Type code
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Claim
            /// </summary>
            [FhirElement("request", Order=50)]
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
            /// Claim Response
            /// </summary>
            [FhirElement("response", Order=60)]
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
            /// Organization which submitted the claim
            /// </summary>
            [FhirElement("submitter", Order=70)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Submitter
            {
                get { return _Submitter; }
                set { _Submitter = value; OnPropertyChanged("Submitter"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Submitter;
            
            /// <summary>
            /// Organization which is receiving the payment
            /// </summary>
            [FhirElement("payee", Order=80)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Payee
            {
                get { return _Payee; }
                set { _Payee = value; OnPropertyChanged("Payee"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Payee;
            
            /// <summary>
            /// Invoice date
            /// </summary>
            [FhirElement("date", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// Invoice date
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
            /// Amount being paid
            /// </summary>
            [FhirElement("amount", Order=100)]
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
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                    if(Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
                    if(Submitter != null) dest.Submitter = (Hl7.Fhir.Model.ResourceReference)Submitter.DeepCopy();
                    if(Payee != null) dest.Payee = (Hl7.Fhir.Model.ResourceReference)Payee.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
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
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Request, otherT.Request)) return false;
                if( !DeepComparable.Matches(Response, otherT.Response)) return false;
                if( !DeepComparable.Matches(Submitter, otherT.Submitter)) return false;
                if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
                if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
                if( !DeepComparable.IsExactly(Submitter, otherT.Submitter)) return false;
                if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Request != null) yield return Request;
                    if (Response != null) yield return Response;
                    if (Submitter != null) yield return Submitter;
                    if (Payee != null) yield return Payee;
                    if (DateElement != null) yield return DateElement;
                    if (Amount != null) yield return Amount;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Request != null) yield return new ElementValue("request", Request);
                    if (Response != null) yield return new ElementValue("response", Response);
                    if (Submitter != null) yield return new ElementValue("submitter", Submitter);
                    if (Payee != null) yield return new ElementValue("payee", Payee);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }

            
        }
        
        
        [FhirType("NotesComponent")]
        [DataContract]
        public partial class NotesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "NotesComponent"; } }
            
            /// <summary>
            /// display | print | printoper
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Comment on the processing
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
            /// Comment on the processing
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
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
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
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NotesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (TextElement != null) yield return TextElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier
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
        [FhirElement("period", Order=110)]
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
        [FhirElement("created", Order=120)]
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
        /// Insurer
        /// </summary>
        [FhirElement("organization", Order=130)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// Claim reference
        /// </summary>
        [FhirElement("request", Order=140)]
        [CLSCompliant(false)]
		[References("ProcessRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// complete | error | partial
        /// </summary>
        [FhirElement("outcome", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DispositionElement
        {
            get { return _DispositionElement; }
            set { _DispositionElement = value; OnPropertyChanged("DispositionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DispositionElement;
        
        /// <summary>
        /// Disposition Message
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
        /// Responsible practitioner
        /// </summary>
        [FhirElement("requestProvider", Order=170)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestProvider
        {
            get { return _RequestProvider; }
            set { _RequestProvider = value; OnPropertyChanged("RequestProvider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestProvider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("requestOrganization", Order=180)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestOrganization
        {
            get { return _RequestOrganization; }
            set { _RequestOrganization = value; OnPropertyChanged("RequestOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestOrganization;
        
        /// <summary>
        /// List of settlements
        /// </summary>
        [FhirElement("detail", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent> Detail
        {
            get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent>(); return _Detail; }
            set { _Detail = value; OnPropertyChanged("Detail"); }
        }
        
        private List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent> _Detail;
        
        /// <summary>
        /// Printed Form Identifier
        /// </summary>
        [FhirElement("form", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Form;
        
        /// <summary>
        /// Total amount of Payment
        /// </summary>
        [FhirElement("total", Order=210)]
        [DataMember]
        public Money Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged("Total"); }
        }
        
        private Money _Total;
        
        /// <summary>
        /// Processing comments
        /// </summary>
        [FhirElement("processNote", Order=220)]
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
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(RequestProvider != null) dest.RequestProvider = (Hl7.Fhir.Model.ResourceReference)RequestProvider.DeepCopy();
                if(RequestOrganization != null) dest.RequestOrganization = (Hl7.Fhir.Model.ResourceReference)RequestOrganization.DeepCopy();
                if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.PaymentReconciliation.DetailsComponent>(Detail.DeepCopy());
                if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                if(Total != null) dest.Total = (Money)Total.DeepCopy();
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
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.Matches(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(Total, otherT.Total)) return false;
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
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.IsExactly(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
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
				if (Organization != null) yield return Organization;
				if (Request != null) yield return Request;
				if (Outcome != null) yield return Outcome;
				if (DispositionElement != null) yield return DispositionElement;
				if (RequestProvider != null) yield return RequestProvider;
				if (RequestOrganization != null) yield return RequestOrganization;
				foreach (var elem in Detail) { if (elem != null) yield return elem; }
				if (Form != null) yield return Form;
				if (Total != null) yield return Total;
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
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (Request != null) yield return new ElementValue("request", Request);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (RequestProvider != null) yield return new ElementValue("requestProvider", RequestProvider);
                if (RequestOrganization != null) yield return new ElementValue("requestOrganization", RequestOrganization);
                foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                if (Form != null) yield return new ElementValue("form", Form);
                if (Total != null) yield return new ElementValue("total", Total);
                foreach (var elem in ProcessNote) { if (elem != null) yield return new ElementValue("processNote", elem); }
            }
        }

    }
    
}
