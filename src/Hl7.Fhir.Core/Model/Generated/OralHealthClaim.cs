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
// Generated on Tue, Oct 28, 2014 16:11+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    [FhirType("OralHealthClaim", IsResource=true)]
    [DataContract]
    public partial class OralHealthClaim : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Complete, proposed, exploratory, other
        /// </summary>
        [FhirEnumeration("Use")]
        public enum Use
        {
            /// <summary>
            /// The treatment is complete and this represents a Claim for the services.
            /// </summary>
            [EnumLiteral("complete")]
            Complete,
            /// <summary>
            /// The treatment is proposed and this represents a Pre-authorization for the services.
            /// </summary>
            [EnumLiteral("proposed")]
            Proposed,
            /// <summary>
            /// The treatment is proposed and this represents a Pre-determination for the services.
            /// </summary>
            [EnumLiteral("exploratory")]
            Exploratory,
            /// <summary>
            /// A locally defined or otherwise resolved status.
            /// </summary>
            [EnumLiteral("other")]
            Other,
        }
        
        [FhirType("ItemsComponent")]
        [DataContract]
        public partial class ItemsComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Group or type of product or service
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Item Code
            /// </summary>
            [FhirElement("service", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Date of Service
            /// </summary>
            [FhirElement("serviceDate", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Date ServiceDateElement
            {
                get { return _ServiceDateElement; }
                set { _ServiceDateElement = value; OnPropertyChanged("ServiceDateElement"); }
            }
            private Hl7.Fhir.Model.Date _ServiceDateElement;
            
            /// <summary>
            /// Date of Service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ServiceDate
            {
                get { return ServiceDateElement != null ? ServiceDateElement.Value : null; }
                set
                {
                    if(value == null)
                      ServiceDateElement = null; 
                    else
                      ServiceDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("ServiceDate");
                }
            }
            
            /// <summary>
            /// Responsible practitioner
            /// </summary>
            [FhirElement("provider", InSummary=true, Order=90)]
            [References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.Reference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            private Hl7.Fhir.Model.Reference _Provider;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            private Hl7.Fhir.Model.Money _Fee;
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("total", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Money Total
            {
                get { return _Total; }
                set { _Total = value; OnPropertyChanged("Total"); }
            }
            private Hl7.Fhir.Model.Money _Total;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Udi
            {
                get { return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            private Hl7.Fhir.Model.Coding _Udi;
            
            /// <summary>
            /// Service Location
            /// </summary>
            [FhirElement("bodySite", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.Coding BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            private Hl7.Fhir.Model.Coding _BodySite;
            
            /// <summary>
            /// Service Sub-location
            /// </summary>
            [FhirElement("subsite", InSummary=true, Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Subsite
            {
                get { return _Subsite; }
                set { _Subsite = value; OnPropertyChanged("Subsite"); }
            }
            private List<Hl7.Fhir.Model.Coding> _Subsite;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", InSummary=true, Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Modifier
            {
                get { return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            private List<Hl7.Fhir.Model.Coding> _Modifier;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.OralHealthClaim.DetailComponent> Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            private List<Hl7.Fhir.Model.OralHealthClaim.DetailComponent> _Detail;
            
            /// <summary>
            /// Prosthetic details
            /// </summary>
            [FhirElement("prosthesis", InSummary=true, Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.OralHealthClaim.ProsthesisComponent Prosthesis
            {
                get { return _Prosthesis; }
                set { _Prosthesis = value; OnPropertyChanged("Prosthesis"); }
            }
            private Hl7.Fhir.Model.OralHealthClaim.ProsthesisComponent _Prosthesis;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(ServiceDateElement != null) dest.ServiceDateElement = (Hl7.Fhir.Model.Date)ServiceDateElement.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.Reference)Provider.DeepCopy();
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.Money)Fee.DeepCopy();
                    if(Total != null) dest.Total = (Hl7.Fhir.Model.Money)Total.DeepCopy();
                    if(Udi != null) dest.Udi = (Hl7.Fhir.Model.Coding)Udi.DeepCopy();
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
                    if(Subsite != null) dest.Subsite = new List<Hl7.Fhir.Model.Coding>(Subsite.DeepCopy());
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.Coding>(Modifier.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.OralHealthClaim.DetailComponent>(Detail.DeepCopy());
                    if(Prosthesis != null) dest.Prosthesis = (Hl7.Fhir.Model.OralHealthClaim.ProsthesisComponent)Prosthesis.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(ServiceDateElement, otherT.ServiceDateElement)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(Total, otherT.Total)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(Subsite, otherT.Subsite)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(Prosthesis, otherT.Prosthesis)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(ServiceDateElement, otherT.ServiceDateElement)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(Subsite, otherT.Subsite)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(Prosthesis, otherT.Prosthesis)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("OrthodonticPlanComponent")]
        [DataContract]
        public partial class OrthodonticPlanComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Start date
            /// </summary>
            [FhirElement("start", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Date StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            private Hl7.Fhir.Model.Date _StartElement;
            
            /// <summary>
            /// Start date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if(value == null)
                      StartElement = null; 
                    else
                      StartElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// First exam fee
            /// </summary>
            [FhirElement("examFee", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Money ExamFee
            {
                get { return _ExamFee; }
                set { _ExamFee = value; OnPropertyChanged("ExamFee"); }
            }
            private Hl7.Fhir.Model.Money _ExamFee;
            
            /// <summary>
            /// Diagnostic phase fee
            /// </summary>
            [FhirElement("diagnosticFee", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Money DiagnosticFee
            {
                get { return _DiagnosticFee; }
                set { _DiagnosticFee = value; OnPropertyChanged("DiagnosticFee"); }
            }
            private Hl7.Fhir.Model.Money _DiagnosticFee;
            
            /// <summary>
            /// Initial payment
            /// </summary>
            [FhirElement("initialPayment", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Money InitialPayment
            {
                get { return _InitialPayment; }
                set { _InitialPayment = value; OnPropertyChanged("InitialPayment"); }
            }
            private Hl7.Fhir.Model.Money _InitialPayment;
            
            /// <summary>
            /// Duration in months
            /// </summary>
            [FhirElement("durationMonths", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DurationMonthsElement
            {
                get { return _DurationMonthsElement; }
                set { _DurationMonthsElement = value; OnPropertyChanged("DurationMonthsElement"); }
            }
            private Hl7.Fhir.Model.Integer _DurationMonthsElement;
            
            /// <summary>
            /// Duration in months
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DurationMonths
            {
                get { return DurationMonthsElement != null ? DurationMonthsElement.Value : null; }
                set
                {
                    if(value == null)
                      DurationMonthsElement = null; 
                    else
                      DurationMonthsElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DurationMonths");
                }
            }
            
            /// <summary>
            /// Anticipated number of payments
            /// </summary>
            [FhirElement("paymentCount", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Integer PaymentCountElement
            {
                get { return _PaymentCountElement; }
                set { _PaymentCountElement = value; OnPropertyChanged("PaymentCountElement"); }
            }
            private Hl7.Fhir.Model.Integer _PaymentCountElement;
            
            /// <summary>
            /// Anticipated number of payments
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? PaymentCount
            {
                get { return PaymentCountElement != null ? PaymentCountElement.Value : null; }
                set
                {
                    if(value == null)
                      PaymentCountElement = null; 
                    else
                      PaymentCountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("PaymentCount");
                }
            }
            
            /// <summary>
            /// Anticipated payment
            /// </summary>
            [FhirElement("periodicPayment", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Money PeriodicPayment
            {
                get { return _PeriodicPayment; }
                set { _PeriodicPayment = value; OnPropertyChanged("PeriodicPayment"); }
            }
            private Hl7.Fhir.Model.Money _PeriodicPayment;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OrthodonticPlanComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Date)StartElement.DeepCopy();
                    if(ExamFee != null) dest.ExamFee = (Hl7.Fhir.Model.Money)ExamFee.DeepCopy();
                    if(DiagnosticFee != null) dest.DiagnosticFee = (Hl7.Fhir.Model.Money)DiagnosticFee.DeepCopy();
                    if(InitialPayment != null) dest.InitialPayment = (Hl7.Fhir.Model.Money)InitialPayment.DeepCopy();
                    if(DurationMonthsElement != null) dest.DurationMonthsElement = (Hl7.Fhir.Model.Integer)DurationMonthsElement.DeepCopy();
                    if(PaymentCountElement != null) dest.PaymentCountElement = (Hl7.Fhir.Model.Integer)PaymentCountElement.DeepCopy();
                    if(PeriodicPayment != null) dest.PeriodicPayment = (Hl7.Fhir.Model.Money)PeriodicPayment.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OrthodonticPlanComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OrthodonticPlanComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(ExamFee, otherT.ExamFee)) return false;
                if( !DeepComparable.Matches(DiagnosticFee, otherT.DiagnosticFee)) return false;
                if( !DeepComparable.Matches(InitialPayment, otherT.InitialPayment)) return false;
                if( !DeepComparable.Matches(DurationMonthsElement, otherT.DurationMonthsElement)) return false;
                if( !DeepComparable.Matches(PaymentCountElement, otherT.PaymentCountElement)) return false;
                if( !DeepComparable.Matches(PeriodicPayment, otherT.PeriodicPayment)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OrthodonticPlanComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(ExamFee, otherT.ExamFee)) return false;
                if( !DeepComparable.IsExactly(DiagnosticFee, otherT.DiagnosticFee)) return false;
                if( !DeepComparable.IsExactly(InitialPayment, otherT.InitialPayment)) return false;
                if( !DeepComparable.IsExactly(DurationMonthsElement, otherT.DurationMonthsElement)) return false;
                if( !DeepComparable.IsExactly(PaymentCountElement, otherT.PaymentCountElement)) return false;
                if( !DeepComparable.IsExactly(PeriodicPayment, otherT.PeriodicPayment)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Group or type of product or service
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Additional item codes
            /// </summary>
            [FhirElement("service", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Additional item fee
            /// </summary>
            [FhirElement("fee", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            private Hl7.Fhir.Model.Money _Fee;
            
            /// <summary>
            /// Total additional item cost
            /// </summary>
            [FhirElement("total", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Money Total
            {
                get { return _Total; }
                set { _Total = value; OnPropertyChanged("Total"); }
            }
            private Hl7.Fhir.Model.Money _Total;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Udi
            {
                get { return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            private Hl7.Fhir.Model.Coding _Udi;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("subDetail", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.OralHealthClaim.SubDetailComponent> SubDetail
            {
                get { return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            private List<Hl7.Fhir.Model.OralHealthClaim.SubDetailComponent> _SubDetail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.Money)Fee.DeepCopy();
                    if(Total != null) dest.Total = (Hl7.Fhir.Model.Money)Total.DeepCopy();
                    if(Udi != null) dest.Udi = (Hl7.Fhir.Model.Coding)Udi.DeepCopy();
                    if(SubDetail != null) dest.SubDetail = new List<Hl7.Fhir.Model.OralHealthClaim.SubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(Total, otherT.Total)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Service instance identifier
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _SequenceElement;
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            [FhirElement("focal", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Focal
            {
                get { return FocalElement != null ? FocalElement.Value : null; }
                set
                {
                    if(value == null)
                      FocalElement = null; 
                    else
                      FocalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Focal");
                }
            }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", InSummary=true, Order=60)]
            [References("Coverage")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Reference Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            private Hl7.Fhir.Model.Reference _Coverage;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            [FhirElement("businessArrangement", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BusinessArrangementElement
            {
                get { return _BusinessArrangementElement; }
                set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
            }
            private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string BusinessArrangement
            {
                get { return BusinessArrangementElement != null ? BusinessArrangementElement.Value : null; }
                set
                {
                    if(value == null)
                      BusinessArrangementElement = null; 
                    else
                      BusinessArrangementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("BusinessArrangement");
                }
            }
            
            /// <summary>
            /// Patient relationship to subscriber
            /// </summary>
            [FhirElement("relationship", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            private Hl7.Fhir.Model.Coding _Relationship;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preauthref", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PreauthrefElement
            {
                get { return _PreauthrefElement; }
                set { _PreauthrefElement = value; OnPropertyChanged("PreauthrefElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _PreauthrefElement;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Preauthref
            {
                get { return PreauthrefElement != null ? PreauthrefElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      PreauthrefElement = null; 
                    else
                      PreauthrefElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Preauthref");
                }
            }
            
            /// <summary>
            /// Original version
            /// </summary>
            [FhirElement("originalRuleset", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Coding OriginalRuleset
            {
                get { return _OriginalRuleset; }
                set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
            }
            private Hl7.Fhir.Model.Coding _OriginalRuleset;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.Reference)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.Coding)Relationship.DeepCopy();
                    if(PreauthrefElement != null) dest.PreauthrefElement = new List<Hl7.Fhir.Model.FhirString>(PreauthrefElement.DeepCopy());
                    if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CoverageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(PreauthrefElement, otherT.PreauthrefElement)) return false;
                if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(PreauthrefElement, otherT.PreauthrefElement)) return false;
                if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("PayeeComponent")]
        [DataContract]
        public partial class PayeeComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Party to be paid any benefits payable
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Provider who is the payee
            /// </summary>
            [FhirElement("provider", InSummary=true, Order=50)]
            [References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.Reference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            private Hl7.Fhir.Model.Reference _Provider;
            
            /// <summary>
            /// Organization who is the payee
            /// </summary>
            [FhirElement("organization", InSummary=true, Order=60)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.Reference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }
            private Hl7.Fhir.Model.Reference _Organization;
            
            /// <summary>
            /// Other person who is the payee
            /// </summary>
            [FhirElement("person", InSummary=true, Order=70)]
            [References("Patient")]
            [DataMember]
            public Hl7.Fhir.Model.Reference Person
            {
                get { return _Person; }
                set { _Person = value; OnPropertyChanged("Person"); }
            }
            private Hl7.Fhir.Model.Reference _Person;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayeeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.Reference)Provider.DeepCopy();
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.Reference)Organization.DeepCopy();
                    if(Person != null) dest.Person = (Hl7.Fhir.Model.Reference)Person.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PayeeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(Person, otherT.Person)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(Person, otherT.Person)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DiagnosisComponent")]
        [DataContract]
        public partial class DiagnosisComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Sequence of diagnosis
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _SequenceElement;
            
            /// <summary>
            /// Sequence of diagnosis
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Patient's list of diagnosis
            /// </summary>
            [FhirElement("diagnosis", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Diagnosis
            {
                get { return _Diagnosis; }
                set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
            }
            private Hl7.Fhir.Model.Coding _Diagnosis;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                    if(Diagnosis != null) dest.Diagnosis = (Hl7.Fhir.Model.Coding)Diagnosis.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ProsthesisComponent")]
        [DataContract]
        public partial class ProsthesisComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Is this the initial service
            /// </summary>
            [FhirElement("initial", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean InitialElement
            {
                get { return _InitialElement; }
                set { _InitialElement = value; OnPropertyChanged("InitialElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _InitialElement;
            
            /// <summary>
            /// Is this the initial service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Initial
            {
                get { return InitialElement != null ? InitialElement.Value : null; }
                set
                {
                    if(value == null)
                      InitialElement = null; 
                    else
                      InitialElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Initial");
                }
            }
            
            /// <summary>
            /// Initial service Date
            /// </summary>
            [FhirElement("priorDate", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Date PriorDateElement
            {
                get { return _PriorDateElement; }
                set { _PriorDateElement = value; OnPropertyChanged("PriorDateElement"); }
            }
            private Hl7.Fhir.Model.Date _PriorDateElement;
            
            /// <summary>
            /// Initial service Date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PriorDate
            {
                get { return PriorDateElement != null ? PriorDateElement.Value : null; }
                set
                {
                    if(value == null)
                      PriorDateElement = null; 
                    else
                      PriorDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("PriorDate");
                }
            }
            
            /// <summary>
            /// Prosthetic Material
            /// </summary>
            [FhirElement("priorMaterial", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding PriorMaterial
            {
                get { return _PriorMaterial; }
                set { _PriorMaterial = value; OnPropertyChanged("PriorMaterial"); }
            }
            private Hl7.Fhir.Model.Coding _PriorMaterial;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProsthesisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(InitialElement != null) dest.InitialElement = (Hl7.Fhir.Model.FhirBoolean)InitialElement.DeepCopy();
                    if(PriorDateElement != null) dest.PriorDateElement = (Hl7.Fhir.Model.Date)PriorDateElement.DeepCopy();
                    if(PriorMaterial != null) dest.PriorMaterial = (Hl7.Fhir.Model.Coding)PriorMaterial.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProsthesisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProsthesisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(InitialElement, otherT.InitialElement)) return false;
                if( !DeepComparable.Matches(PriorDateElement, otherT.PriorDateElement)) return false;
                if( !DeepComparable.Matches(PriorMaterial, otherT.PriorMaterial)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProsthesisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(InitialElement, otherT.InitialElement)) return false;
                if( !DeepComparable.IsExactly(PriorDateElement, otherT.PriorDateElement)) return false;
                if( !DeepComparable.IsExactly(PriorMaterial, otherT.PriorMaterial)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Type of product or service
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Additional item codes
            /// </summary>
            [FhirElement("service", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Additional item fee
            /// </summary>
            [FhirElement("fee", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            private Hl7.Fhir.Model.Money _Fee;
            
            /// <summary>
            /// Total additional item cost
            /// </summary>
            [FhirElement("total", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Money Total
            {
                get { return _Total; }
                set { _Total = value; OnPropertyChanged("Total"); }
            }
            private Hl7.Fhir.Model.Money _Total;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Udi
            {
                get { return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            private Hl7.Fhir.Model.Coding _Udi;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.Money)Fee.DeepCopy();
                    if(Total != null) dest.Total = (Hl7.Fhir.Model.Money)Total.DeepCopy();
                    if(Udi != null) dest.Udi = (Hl7.Fhir.Model.Coding)Udi.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(Total, otherT.Total)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("MissingTeethComponent")]
        [DataContract]
        public partial class MissingTeethComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Tooth Code
            /// </summary>
            [FhirElement("tooth", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Tooth
            {
                get { return _Tooth; }
                set { _Tooth = value; OnPropertyChanged("Tooth"); }
            }
            private Hl7.Fhir.Model.Coding _Tooth;
            
            /// <summary>
            /// Reason for missing
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            private Hl7.Fhir.Model.Coding _Reason;
            
            /// <summary>
            /// Date of Extraction
            /// </summary>
            [FhirElement("extractiondate", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Date ExtractiondateElement
            {
                get { return _ExtractiondateElement; }
                set { _ExtractiondateElement = value; OnPropertyChanged("ExtractiondateElement"); }
            }
            private Hl7.Fhir.Model.Date _ExtractiondateElement;
            
            /// <summary>
            /// Date of Extraction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Extractiondate
            {
                get { return ExtractiondateElement != null ? ExtractiondateElement.Value : null; }
                set
                {
                    if(value == null)
                      ExtractiondateElement = null; 
                    else
                      ExtractiondateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Extractiondate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MissingTeethComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Tooth != null) dest.Tooth = (Hl7.Fhir.Model.Coding)Tooth.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Coding)Reason.DeepCopy();
                    if(ExtractiondateElement != null) dest.ExtractiondateElement = (Hl7.Fhir.Model.Date)ExtractiondateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MissingTeethComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MissingTeethComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Tooth, otherT.Tooth)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ExtractiondateElement, otherT.ExtractiondateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MissingTeethComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Tooth, otherT.Tooth)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ExtractiondateElement, otherT.ExtractiondateElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Claim number
        /// </summary>
        [FhirElement("identifier", Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        [FhirElement("use", Order=70)]
        [DataMember]
        public Code<Hl7.Fhir.Model.OralHealthClaim.Use> Use_Element
        {
            get { return _Use_Element; }
            set { _Use_Element = value; OnPropertyChanged("Use_Element"); }
        }
        private Code<Hl7.Fhir.Model.OralHealthClaim.Use> _Use_Element;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.OralHealthClaim.Use? Use_
        {
            get { return Use_Element != null ? Use_Element.Value : null; }
            set
            {
                if(value == null)
                  Use_Element = null; 
                else
                  Use_Element = new Code<Hl7.Fhir.Model.OralHealthClaim.Use>(value);
                OnPropertyChanged("Use_");
            }
        }
        
        /// <summary>
        /// Funds requested to be reserved
        /// </summary>
        [FhirElement("fundsReserve", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Coding FundsReserve
        {
            get { return _FundsReserve; }
            set { _FundsReserve = value; OnPropertyChanged("FundsReserve"); }
        }
        private Hl7.Fhir.Model.Coding _FundsReserve;
        
        /// <summary>
        /// Author
        /// </summary>
        [FhirElement("enterer", Order=90)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        private Hl7.Fhir.Model.Reference _Enterer;
        
        /// <summary>
        /// Insurer Identifier
        /// </summary>
        [FhirElement("insurer", Order=100)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        private Hl7.Fhir.Model.Reference _Insurer;
        
        /// <summary>
        /// Current specification followed
        /// </summary>
        [FhirElement("ruleset", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Ruleset
        {
            get { return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        private Hl7.Fhir.Model.Coding _Ruleset;
        
        /// <summary>
        /// Original specification followed
        /// </summary>
        [FhirElement("originalRuleset", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Coding OriginalRuleset
        {
            get { return _OriginalRuleset; }
            set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
        }
        private Hl7.Fhir.Model.Coding _OriginalRuleset;
        
        /// <summary>
        /// Desired processing priority
        /// </summary>
        [FhirElement("priority", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        private Hl7.Fhir.Model.Coding _Priority;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("date", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Date DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.Date _DateElement;
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("organization", Order=150)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        private Hl7.Fhir.Model.Reference _Organization;
        
        /// <summary>
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", Order=160)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Facility
        {
            get { return _Facility; }
            set { _Facility = value; OnPropertyChanged("Facility"); }
        }
        private Hl7.Fhir.Model.Reference _Facility;
        
        /// <summary>
        /// Payee
        /// </summary>
        [FhirElement("payee", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.OralHealthClaim.PayeeComponent Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged("Payee"); }
        }
        private Hl7.Fhir.Model.OralHealthClaim.PayeeComponent _Payee;
        
        /// <summary>
        /// Treatment Referral
        /// </summary>
        [FhirElement("referral", Order=180)]
        [References("ReferralRequest")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Referral
        {
            get { return _Referral; }
            set { _Referral = value; OnPropertyChanged("Referral"); }
        }
        private Hl7.Fhir.Model.Reference _Referral;
        
        /// <summary>
        /// Diagnosis
        /// </summary>
        [FhirElement("diagnosis", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OralHealthClaim.DiagnosisComponent> Diagnosis
        {
            get { return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        private List<Hl7.Fhir.Model.OralHealthClaim.DiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// List of presenting Conditions
        /// </summary>
        [FhirElement("condition", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Condition
        {
            get { return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        private List<Hl7.Fhir.Model.Coding> _Condition;
        
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Order=210)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Reference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.Reference _Patient;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("coverage", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OralHealthClaim.CoverageComponent> Coverage
        {
            get { return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        private List<Hl7.Fhir.Model.OralHealthClaim.CoverageComponent> _Coverage;
        
        /// <summary>
        /// Eligibility exceptions
        /// </summary>
        [FhirElement("exception", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Exception
        {
            get { return _Exception; }
            set { _Exception = value; OnPropertyChanged("Exception"); }
        }
        private List<Hl7.Fhir.Model.Coding> _Exception;
        
        /// <summary>
        /// Name of School
        /// </summary>
        [FhirElement("school", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SchoolElement
        {
            get { return _SchoolElement; }
            set { _SchoolElement = value; OnPropertyChanged("SchoolElement"); }
        }
        private Hl7.Fhir.Model.FhirString _SchoolElement;
        
        /// <summary>
        /// Name of School
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string School
        {
            get { return SchoolElement != null ? SchoolElement.Value : null; }
            set
            {
                if(value == null)
                  SchoolElement = null; 
                else
                  SchoolElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("School");
            }
        }
        
        /// <summary>
        /// Accident Date
        /// </summary>
        [FhirElement("accident", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.Date AccidentElement
        {
            get { return _AccidentElement; }
            set { _AccidentElement = value; OnPropertyChanged("AccidentElement"); }
        }
        private Hl7.Fhir.Model.Date _AccidentElement;
        
        /// <summary>
        /// Accident Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Accident
        {
            get { return AccidentElement != null ? AccidentElement.Value : null; }
            set
            {
                if(value == null)
                  AccidentElement = null; 
                else
                  AccidentElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("Accident");
            }
        }
        
        /// <summary>
        /// Accident Type
        /// </summary>
        [FhirElement("accidentType", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Coding AccidentType
        {
            get { return _AccidentType; }
            set { _AccidentType = value; OnPropertyChanged("AccidentType"); }
        }
        private Hl7.Fhir.Model.Coding _AccidentType;
        
        /// <summary>
        /// Intervention and exception code (Pharma)
        /// </summary>
        [FhirElement("interventionException", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> InterventionException
        {
            get { return _InterventionException; }
            set { _InterventionException = value; OnPropertyChanged("InterventionException"); }
        }
        private List<Hl7.Fhir.Model.Coding> _InterventionException;
        
        /// <summary>
        /// Missing Teeth
        /// </summary>
        [FhirElement("missingteeth", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OralHealthClaim.MissingTeethComponent> Missingteeth
        {
            get { return _Missingteeth; }
            set { _Missingteeth = value; OnPropertyChanged("Missingteeth"); }
        }
        private List<Hl7.Fhir.Model.OralHealthClaim.MissingTeethComponent> _Missingteeth;
        
        /// <summary>
        /// Orthodontic Treatment Plan
        /// </summary>
        [FhirElement("orthoPlan", Order=290)]
        [DataMember]
        public Hl7.Fhir.Model.OralHealthClaim.OrthodonticPlanComponent OrthoPlan
        {
            get { return _OrthoPlan; }
            set { _OrthoPlan = value; OnPropertyChanged("OrthoPlan"); }
        }
        private Hl7.Fhir.Model.OralHealthClaim.OrthodonticPlanComponent _OrthoPlan;
        
        /// <summary>
        /// Goods and Servcies
        /// </summary>
        [FhirElement("item", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OralHealthClaim.ItemsComponent> Item
        {
            get { return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        private List<Hl7.Fhir.Model.OralHealthClaim.ItemsComponent> _Item;
        
        /// <summary>
        /// Additional materials, documents, etc.
        /// </summary>
        [FhirElement("additionalMaterials", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> AdditionalMaterials
        {
            get { return _AdditionalMaterials; }
            set { _AdditionalMaterials = value; OnPropertyChanged("AdditionalMaterials"); }
        }
        private List<Hl7.Fhir.Model.Coding> _AdditionalMaterials;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as OralHealthClaim;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Use_Element != null) dest.Use_Element = (Code<Hl7.Fhir.Model.OralHealthClaim.Use>)Use_Element.DeepCopy();
                if(FundsReserve != null) dest.FundsReserve = (Hl7.Fhir.Model.Coding)FundsReserve.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.Reference)Enterer.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.Reference)Insurer.DeepCopy();
                if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.Coding)Ruleset.DeepCopy();
                if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.Coding)Priority.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.Reference)Organization.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.Reference)Facility.DeepCopy();
                if(Payee != null) dest.Payee = (Hl7.Fhir.Model.OralHealthClaim.PayeeComponent)Payee.DeepCopy();
                if(Referral != null) dest.Referral = (Hl7.Fhir.Model.Reference)Referral.DeepCopy();
                if(Diagnosis != null) dest.Diagnosis = new List<Hl7.Fhir.Model.OralHealthClaim.DiagnosisComponent>(Diagnosis.DeepCopy());
                if(Condition != null) dest.Condition = new List<Hl7.Fhir.Model.Coding>(Condition.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.Reference)Patient.DeepCopy();
                if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.OralHealthClaim.CoverageComponent>(Coverage.DeepCopy());
                if(Exception != null) dest.Exception = new List<Hl7.Fhir.Model.Coding>(Exception.DeepCopy());
                if(SchoolElement != null) dest.SchoolElement = (Hl7.Fhir.Model.FhirString)SchoolElement.DeepCopy();
                if(AccidentElement != null) dest.AccidentElement = (Hl7.Fhir.Model.Date)AccidentElement.DeepCopy();
                if(AccidentType != null) dest.AccidentType = (Hl7.Fhir.Model.Coding)AccidentType.DeepCopy();
                if(InterventionException != null) dest.InterventionException = new List<Hl7.Fhir.Model.Coding>(InterventionException.DeepCopy());
                if(Missingteeth != null) dest.Missingteeth = new List<Hl7.Fhir.Model.OralHealthClaim.MissingTeethComponent>(Missingteeth.DeepCopy());
                if(OrthoPlan != null) dest.OrthoPlan = (Hl7.Fhir.Model.OralHealthClaim.OrthodonticPlanComponent)OrthoPlan.DeepCopy();
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.OralHealthClaim.ItemsComponent>(Item.DeepCopy());
                if(AdditionalMaterials != null) dest.AdditionalMaterials = new List<Hl7.Fhir.Model.Coding>(AdditionalMaterials.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new OralHealthClaim());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as OralHealthClaim;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.Matches(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
            if( !DeepComparable.Matches(Referral, otherT.Referral)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Exception, otherT.Exception)) return false;
            if( !DeepComparable.Matches(SchoolElement, otherT.SchoolElement)) return false;
            if( !DeepComparable.Matches(AccidentElement, otherT.AccidentElement)) return false;
            if( !DeepComparable.Matches(AccidentType, otherT.AccidentType)) return false;
            if( !DeepComparable.Matches(InterventionException, otherT.InterventionException)) return false;
            if( !DeepComparable.Matches(Missingteeth, otherT.Missingteeth)) return false;
            if( !DeepComparable.Matches(OrthoPlan, otherT.OrthoPlan)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(AdditionalMaterials, otherT.AdditionalMaterials)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as OralHealthClaim;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.IsExactly(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
            if( !DeepComparable.IsExactly(Referral, otherT.Referral)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Exception, otherT.Exception)) return false;
            if( !DeepComparable.IsExactly(SchoolElement, otherT.SchoolElement)) return false;
            if( !DeepComparable.IsExactly(AccidentElement, otherT.AccidentElement)) return false;
            if( !DeepComparable.IsExactly(AccidentType, otherT.AccidentType)) return false;
            if( !DeepComparable.IsExactly(InterventionException, otherT.InterventionException)) return false;
            if( !DeepComparable.IsExactly(Missingteeth, otherT.Missingteeth)) return false;
            if( !DeepComparable.IsExactly(OrthoPlan, otherT.OrthoPlan)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(AdditionalMaterials, otherT.AdditionalMaterials)) return false;
            
            return true;
        }
        
    }
    
}
