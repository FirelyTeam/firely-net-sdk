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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// PaymentNotice request
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "PaymentNotice", IsResource=true)]
    [DataContract]
    public partial class PaymentNotice : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IPaymentNotice, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.PaymentNotice; } }
        [NotMapped]
        public override string TypeName { get { return "PaymentNotice"; } }
    
        
        /// <summary>
        /// Business Identifier for the payment noctice
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Request reference
        /// </summary>
        [FhirElement("request", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Response reference
        /// </summary>
        [FhirElement("response", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Response
        {
            get { return _Response; }
            set { _Response = value; OnPropertyChanged("Response"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Response;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        /// Responsible practitioner
        /// </summary>
        [FhirElement("provider", Order=140)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Provider;
        
        /// <summary>
        /// Payment reference
        /// </summary>
        [FhirElement("payment", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("PaymentReconciliation")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Payment
        {
            get { return _Payment; }
            set { _Payment = value; OnPropertyChanged("Payment"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Payment;
        
        /// <summary>
        /// Payment or clearing date
        /// </summary>
        [FhirElement("paymentDate", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Date PaymentDateElement
        {
            get { return _PaymentDateElement; }
            set { _PaymentDateElement = value; OnPropertyChanged("PaymentDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _PaymentDateElement;
        
        /// <summary>
        /// Payment or clearing date
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
        /// Party being paid
        /// </summary>
        [FhirElement("payee", Order=170)]
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
        /// Party being notified
        /// </summary>
        [FhirElement("recipient", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recipient
        {
            get { return _Recipient; }
            set { _Recipient = value; OnPropertyChanged("Recipient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recipient;
        
        /// <summary>
        /// Monetary amount of the payment
        /// </summary>
        [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.R4.Money Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged("Amount"); }
        }
        
        private Hl7.Fhir.Model.R4.Money _Amount;
        
        /// <summary>
        /// Issued or cleared Status of the payment
        /// </summary>
        [FhirElement("paymentStatus", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PaymentStatus
        {
            get { return _PaymentStatus; }
            set { _PaymentStatus = value; OnPropertyChanged("PaymentStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PaymentStatus;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as PaymentNotice;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                if(Payment != null) dest.Payment = (Hl7.Fhir.Model.ResourceReference)Payment.DeepCopy();
                if(PaymentDateElement != null) dest.PaymentDateElement = (Hl7.Fhir.Model.Date)PaymentDateElement.DeepCopy();
                if(Payee != null) dest.Payee = (Hl7.Fhir.Model.ResourceReference)Payee.DeepCopy();
                if(Recipient != null) dest.Recipient = (Hl7.Fhir.Model.ResourceReference)Recipient.DeepCopy();
                if(Amount != null) dest.Amount = (Hl7.Fhir.Model.R4.Money)Amount.DeepCopy();
                if(PaymentStatus != null) dest.PaymentStatus = (Hl7.Fhir.Model.CodeableConcept)PaymentStatus.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new PaymentNotice());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as PaymentNotice;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Payment, otherT.Payment)) return false;
            if( !DeepComparable.Matches(PaymentDateElement, otherT.PaymentDateElement)) return false;
            if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
            if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            if( !DeepComparable.Matches(PaymentStatus, otherT.PaymentStatus)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as PaymentNotice;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Payment, otherT.Payment)) return false;
            if( !DeepComparable.IsExactly(PaymentDateElement, otherT.PaymentDateElement)) return false;
            if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
            if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            if( !DeepComparable.IsExactly(PaymentStatus, otherT.PaymentStatus)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("PaymentNotice");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Request?.Serialize(sink);
            sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Response?.Serialize(sink);
            sink.Element("created", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CreatedElement?.Serialize(sink);
            sink.Element("provider", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Provider?.Serialize(sink);
            sink.Element("payment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Payment?.Serialize(sink);
            sink.Element("paymentDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PaymentDateElement?.Serialize(sink);
            sink.Element("payee", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Payee?.Serialize(sink);
            sink.Element("recipient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Recipient?.Serialize(sink);
            sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Amount?.Serialize(sink);
            sink.Element("paymentStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PaymentStatus?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>>();
                    return true;
                case "request":
                    Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "response":
                    Response = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "created":
                    CreatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "provider":
                    Provider = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "payment":
                    Payment = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "paymentDate":
                    PaymentDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "payee":
                    Payee = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "recipient":
                    Recipient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "amount":
                    Amount = source.Get<Hl7.Fhir.Model.R4.Money>();
                    return true;
                case "paymentStatus":
                    PaymentStatus = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "request":
                    Request = source.Populate(Request);
                    return true;
                case "response":
                    Response = source.Populate(Response);
                    return true;
                case "created":
                    CreatedElement = source.PopulateValue(CreatedElement);
                    return true;
                case "_created":
                    CreatedElement = source.Populate(CreatedElement);
                    return true;
                case "provider":
                    Provider = source.Populate(Provider);
                    return true;
                case "payment":
                    Payment = source.Populate(Payment);
                    return true;
                case "paymentDate":
                    PaymentDateElement = source.PopulateValue(PaymentDateElement);
                    return true;
                case "_paymentDate":
                    PaymentDateElement = source.Populate(PaymentDateElement);
                    return true;
                case "payee":
                    Payee = source.Populate(Payee);
                    return true;
                case "recipient":
                    Recipient = source.Populate(Recipient);
                    return true;
                case "amount":
                    Amount = source.Populate(Amount);
                    return true;
                case "paymentStatus":
                    PaymentStatus = source.Populate(PaymentStatus);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (Request != null) yield return Request;
                if (Response != null) yield return Response;
                if (CreatedElement != null) yield return CreatedElement;
                if (Provider != null) yield return Provider;
                if (Payment != null) yield return Payment;
                if (PaymentDateElement != null) yield return PaymentDateElement;
                if (Payee != null) yield return Payee;
                if (Recipient != null) yield return Recipient;
                if (Amount != null) yield return Amount;
                if (PaymentStatus != null) yield return PaymentStatus;
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
                if (Request != null) yield return new ElementValue("request", Request);
                if (Response != null) yield return new ElementValue("response", Response);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Provider != null) yield return new ElementValue("provider", Provider);
                if (Payment != null) yield return new ElementValue("payment", Payment);
                if (PaymentDateElement != null) yield return new ElementValue("paymentDate", PaymentDateElement);
                if (Payee != null) yield return new ElementValue("payee", Payee);
                if (Recipient != null) yield return new ElementValue("recipient", Recipient);
                if (Amount != null) yield return new ElementValue("amount", Amount);
                if (PaymentStatus != null) yield return new ElementValue("paymentStatus", PaymentStatus);
            }
        }
    
    }

}
