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
    /// Describes validation requirements, source(s), status and dates for one or more elements
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "VerificationResult", IsResource=true)]
    [DataContract]
    public partial class VerificationResult : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.VerificationResult; } }
        [NotMapped]
        public override string TypeName { get { return "VerificationResult"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PrimarySourceComponent")]
        [DataContract]
        public partial class PrimarySourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PrimarySourceComponent"; } }
            
            /// <summary>
            /// Reference to the primary source
            /// </summary>
            [FhirElement("who", Order=40)]
            [CLSCompliant(false)]
            [References("Organization","Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Who
            {
                get { return _Who; }
                set { _Who = value; OnPropertyChanged("Who"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Who;
            
            /// <summary>
            /// Type of primary source (License Board; Primary Education; Continuing Education; Postal Service; Relationship owner; Registration Authority; legal source; issuing source; authoritative source)
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Method for exchanging information with the primary source
            /// </summary>
            [FhirElement("communicationMethod", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> CommunicationMethod
            {
                get { if(_CommunicationMethod==null) _CommunicationMethod = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CommunicationMethod; }
                set { _CommunicationMethod = value; OnPropertyChanged("CommunicationMethod"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _CommunicationMethod;
            
            /// <summary>
            /// successful | failed | unknown
            /// </summary>
            [FhirElement("validationStatus", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ValidationStatus
            {
                get { return _ValidationStatus; }
                set { _ValidationStatus = value; OnPropertyChanged("ValidationStatus"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ValidationStatus;
            
            /// <summary>
            /// When the target was validated against the primary source
            /// </summary>
            [FhirElement("validationDate", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValidationDateElement
            {
                get { return _ValidationDateElement; }
                set { _ValidationDateElement = value; OnPropertyChanged("ValidationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ValidationDateElement;
            
            /// <summary>
            /// When the target was validated against the primary source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidationDate
            {
                get { return ValidationDateElement != null ? ValidationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidationDateElement = null;
                    else
                        ValidationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ValidationDate");
                }
            }
            
            /// <summary>
            /// yes | no | undetermined
            /// </summary>
            [FhirElement("canPushUpdates", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CanPushUpdates
            {
                get { return _CanPushUpdates; }
                set { _CanPushUpdates = value; OnPropertyChanged("CanPushUpdates"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CanPushUpdates;
            
            /// <summary>
            /// specific | any | source
            /// </summary>
            [FhirElement("pushTypeAvailable", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> PushTypeAvailable
            {
                get { if(_PushTypeAvailable==null) _PushTypeAvailable = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PushTypeAvailable; }
                set { _PushTypeAvailable = value; OnPropertyChanged("PushTypeAvailable"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _PushTypeAvailable;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PrimarySourceComponent");
                base.Serialize(sink);
                sink.Element("who", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Who?.Serialize(sink);
                sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Type)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("communicationMethod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in CommunicationMethod)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("validationStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidationStatus?.Serialize(sink);
                sink.Element("validationDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidationDateElement?.Serialize(sink);
                sink.Element("canPushUpdates", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CanPushUpdates?.Serialize(sink);
                sink.BeginList("pushTypeAvailable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in PushTypeAvailable)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "who":
                        Who = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "type":
                        Type = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "communicationMethod":
                        CommunicationMethod = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "validationStatus":
                        ValidationStatus = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "validationDate":
                        ValidationDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "canPushUpdates":
                        CanPushUpdates = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "pushTypeAvailable":
                        PushTypeAvailable = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "who":
                        Who = source.Populate(Who);
                        return true;
                    case "type":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "communicationMethod":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "validationStatus":
                        ValidationStatus = source.Populate(ValidationStatus);
                        return true;
                    case "validationDate":
                        ValidationDateElement = source.PopulateValue(ValidationDateElement);
                        return true;
                    case "_validationDate":
                        ValidationDateElement = source.Populate(ValidationDateElement);
                        return true;
                    case "canPushUpdates":
                        CanPushUpdates = source.Populate(CanPushUpdates);
                        return true;
                    case "pushTypeAvailable":
                        source.SetList(this, jsonPropertyName);
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
                    case "type":
                        source.PopulateListItem(Type, index);
                        return true;
                    case "communicationMethod":
                        source.PopulateListItem(CommunicationMethod, index);
                        return true;
                    case "pushTypeAvailable":
                        source.PopulateListItem(PushTypeAvailable, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PrimarySourceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(CommunicationMethod != null) dest.CommunicationMethod = new List<Hl7.Fhir.Model.CodeableConcept>(CommunicationMethod.DeepCopy());
                    if(ValidationStatus != null) dest.ValidationStatus = (Hl7.Fhir.Model.CodeableConcept)ValidationStatus.DeepCopy();
                    if(ValidationDateElement != null) dest.ValidationDateElement = (Hl7.Fhir.Model.FhirDateTime)ValidationDateElement.DeepCopy();
                    if(CanPushUpdates != null) dest.CanPushUpdates = (Hl7.Fhir.Model.CodeableConcept)CanPushUpdates.DeepCopy();
                    if(PushTypeAvailable != null) dest.PushTypeAvailable = new List<Hl7.Fhir.Model.CodeableConcept>(PushTypeAvailable.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PrimarySourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PrimarySourceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Who, otherT.Who)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.Matches(ValidationStatus, otherT.ValidationStatus)) return false;
                if( !DeepComparable.Matches(ValidationDateElement, otherT.ValidationDateElement)) return false;
                if( !DeepComparable.Matches(CanPushUpdates, otherT.CanPushUpdates)) return false;
                if( !DeepComparable.Matches(PushTypeAvailable, otherT.PushTypeAvailable)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PrimarySourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.IsExactly(ValidationStatus, otherT.ValidationStatus)) return false;
                if( !DeepComparable.IsExactly(ValidationDateElement, otherT.ValidationDateElement)) return false;
                if( !DeepComparable.IsExactly(CanPushUpdates, otherT.CanPushUpdates)) return false;
                if( !DeepComparable.IsExactly(PushTypeAvailable, otherT.PushTypeAvailable)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Who != null) yield return Who;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    foreach (var elem in CommunicationMethod) { if (elem != null) yield return elem; }
                    if (ValidationStatus != null) yield return ValidationStatus;
                    if (ValidationDateElement != null) yield return ValidationDateElement;
                    if (CanPushUpdates != null) yield return CanPushUpdates;
                    foreach (var elem in PushTypeAvailable) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Who != null) yield return new ElementValue("who", Who);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    foreach (var elem in CommunicationMethod) { if (elem != null) yield return new ElementValue("communicationMethod", elem); }
                    if (ValidationStatus != null) yield return new ElementValue("validationStatus", ValidationStatus);
                    if (ValidationDateElement != null) yield return new ElementValue("validationDate", ValidationDateElement);
                    if (CanPushUpdates != null) yield return new ElementValue("canPushUpdates", CanPushUpdates);
                    foreach (var elem in PushTypeAvailable) { if (elem != null) yield return new ElementValue("pushTypeAvailable", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AttestationComponent")]
        [DataContract]
        public partial class AttestationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AttestationComponent"; } }
            
            /// <summary>
            /// The individual or organization attesting to information
            /// </summary>
            [FhirElement("who", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Who
            {
                get { return _Who; }
                set { _Who = value; OnPropertyChanged("Who"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Who;
            
            /// <summary>
            /// When the who is asserting on behalf of another (organization or individual)
            /// </summary>
            [FhirElement("onBehalfOf", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("Organization","Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
            
            /// <summary>
            /// The method by which attested information was submitted/retrieved
            /// </summary>
            [FhirElement("communicationMethod", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CommunicationMethod
            {
                get { return _CommunicationMethod; }
                set { _CommunicationMethod = value; OnPropertyChanged("CommunicationMethod"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CommunicationMethod;
            
            /// <summary>
            /// The date the information was attested to
            /// </summary>
            [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// The date the information was attested to
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
            /// A digital identity certificate associated with the attestation source
            /// </summary>
            [FhirElement("sourceIdentityCertificate", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SourceIdentityCertificateElement
            {
                get { return _SourceIdentityCertificateElement; }
                set { _SourceIdentityCertificateElement = value; OnPropertyChanged("SourceIdentityCertificateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SourceIdentityCertificateElement;
            
            /// <summary>
            /// A digital identity certificate associated with the attestation source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceIdentityCertificate
            {
                get { return SourceIdentityCertificateElement != null ? SourceIdentityCertificateElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdentityCertificateElement = null;
                    else
                        SourceIdentityCertificateElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SourceIdentityCertificate");
                }
            }
            
            /// <summary>
            /// A digital identity certificate associated with the proxy entity submitting attested information on behalf of the attestation source
            /// </summary>
            [FhirElement("proxyIdentityCertificate", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ProxyIdentityCertificateElement
            {
                get { return _ProxyIdentityCertificateElement; }
                set { _ProxyIdentityCertificateElement = value; OnPropertyChanged("ProxyIdentityCertificateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ProxyIdentityCertificateElement;
            
            /// <summary>
            /// A digital identity certificate associated with the proxy entity submitting attested information on behalf of the attestation source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ProxyIdentityCertificate
            {
                get { return ProxyIdentityCertificateElement != null ? ProxyIdentityCertificateElement.Value : null; }
                set
                {
                    if (value == null)
                        ProxyIdentityCertificateElement = null;
                    else
                        ProxyIdentityCertificateElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ProxyIdentityCertificate");
                }
            }
            
            /// <summary>
            /// Proxy signature
            /// </summary>
            [FhirElement("proxySignature", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Signature ProxySignature
            {
                get { return _ProxySignature; }
                set { _ProxySignature = value; OnPropertyChanged("ProxySignature"); }
            }
            
            private Hl7.Fhir.Model.R4.Signature _ProxySignature;
            
            /// <summary>
            /// Attester signature
            /// </summary>
            [FhirElement("sourceSignature", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Signature SourceSignature
            {
                get { return _SourceSignature; }
                set { _SourceSignature = value; OnPropertyChanged("SourceSignature"); }
            }
            
            private Hl7.Fhir.Model.R4.Signature _SourceSignature;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AttestationComponent");
                base.Serialize(sink);
                sink.Element("who", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Who?.Serialize(sink);
                sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OnBehalfOf?.Serialize(sink);
                sink.Element("communicationMethod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CommunicationMethod?.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
                sink.Element("sourceIdentityCertificate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceIdentityCertificateElement?.Serialize(sink);
                sink.Element("proxyIdentityCertificate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ProxyIdentityCertificateElement?.Serialize(sink);
                sink.Element("proxySignature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ProxySignature?.Serialize(sink);
                sink.Element("sourceSignature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceSignature?.Serialize(sink);
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
                    case "who":
                        Who = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "communicationMethod":
                        CommunicationMethod = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "date":
                        DateElement = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "sourceIdentityCertificate":
                        SourceIdentityCertificateElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "proxyIdentityCertificate":
                        ProxyIdentityCertificateElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "proxySignature":
                        ProxySignature = source.Get<Hl7.Fhir.Model.R4.Signature>();
                        return true;
                    case "sourceSignature":
                        SourceSignature = source.Get<Hl7.Fhir.Model.R4.Signature>();
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
                    case "who":
                        Who = source.Populate(Who);
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Populate(OnBehalfOf);
                        return true;
                    case "communicationMethod":
                        CommunicationMethod = source.Populate(CommunicationMethod);
                        return true;
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "sourceIdentityCertificate":
                        SourceIdentityCertificateElement = source.PopulateValue(SourceIdentityCertificateElement);
                        return true;
                    case "_sourceIdentityCertificate":
                        SourceIdentityCertificateElement = source.Populate(SourceIdentityCertificateElement);
                        return true;
                    case "proxyIdentityCertificate":
                        ProxyIdentityCertificateElement = source.PopulateValue(ProxyIdentityCertificateElement);
                        return true;
                    case "_proxyIdentityCertificate":
                        ProxyIdentityCertificateElement = source.Populate(ProxyIdentityCertificateElement);
                        return true;
                    case "proxySignature":
                        ProxySignature = source.Populate(ProxySignature);
                        return true;
                    case "sourceSignature":
                        SourceSignature = source.Populate(SourceSignature);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AttestationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    if(CommunicationMethod != null) dest.CommunicationMethod = (Hl7.Fhir.Model.CodeableConcept)CommunicationMethod.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(SourceIdentityCertificateElement != null) dest.SourceIdentityCertificateElement = (Hl7.Fhir.Model.FhirString)SourceIdentityCertificateElement.DeepCopy();
                    if(ProxyIdentityCertificateElement != null) dest.ProxyIdentityCertificateElement = (Hl7.Fhir.Model.FhirString)ProxyIdentityCertificateElement.DeepCopy();
                    if(ProxySignature != null) dest.ProxySignature = (Hl7.Fhir.Model.R4.Signature)ProxySignature.DeepCopy();
                    if(SourceSignature != null) dest.SourceSignature = (Hl7.Fhir.Model.R4.Signature)SourceSignature.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AttestationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AttestationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Who, otherT.Who)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
                if( !DeepComparable.Matches(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(SourceIdentityCertificateElement, otherT.SourceIdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(ProxyIdentityCertificateElement, otherT.ProxyIdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(ProxySignature, otherT.ProxySignature)) return false;
                if( !DeepComparable.Matches(SourceSignature, otherT.SourceSignature)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AttestationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
                if( !DeepComparable.IsExactly(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdentityCertificateElement, otherT.SourceIdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(ProxyIdentityCertificateElement, otherT.ProxyIdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(ProxySignature, otherT.ProxySignature)) return false;
                if( !DeepComparable.IsExactly(SourceSignature, otherT.SourceSignature)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Who != null) yield return Who;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                    if (CommunicationMethod != null) yield return CommunicationMethod;
                    if (DateElement != null) yield return DateElement;
                    if (SourceIdentityCertificateElement != null) yield return SourceIdentityCertificateElement;
                    if (ProxyIdentityCertificateElement != null) yield return ProxyIdentityCertificateElement;
                    if (ProxySignature != null) yield return ProxySignature;
                    if (SourceSignature != null) yield return SourceSignature;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Who != null) yield return new ElementValue("who", Who);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                    if (CommunicationMethod != null) yield return new ElementValue("communicationMethod", CommunicationMethod);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (SourceIdentityCertificateElement != null) yield return new ElementValue("sourceIdentityCertificate", SourceIdentityCertificateElement);
                    if (ProxyIdentityCertificateElement != null) yield return new ElementValue("proxyIdentityCertificate", ProxyIdentityCertificateElement);
                    if (ProxySignature != null) yield return new ElementValue("proxySignature", ProxySignature);
                    if (SourceSignature != null) yield return new ElementValue("sourceSignature", SourceSignature);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ValidatorComponent")]
        [DataContract]
        public partial class ValidatorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ValidatorComponent"; } }
            
            /// <summary>
            /// Reference to the organization validating information
            /// </summary>
            [FhirElement("organization", Order=40)]
            [CLSCompliant(false)]
            [References("Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Organization;
            
            /// <summary>
            /// A digital identity certificate associated with the validator
            /// </summary>
            [FhirElement("identityCertificate", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentityCertificateElement
            {
                get { return _IdentityCertificateElement; }
                set { _IdentityCertificateElement = value; OnPropertyChanged("IdentityCertificateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentityCertificateElement;
            
            /// <summary>
            /// A digital identity certificate associated with the validator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IdentityCertificate
            {
                get { return IdentityCertificateElement != null ? IdentityCertificateElement.Value : null; }
                set
                {
                    if (value == null)
                        IdentityCertificateElement = null;
                    else
                        IdentityCertificateElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IdentityCertificate");
                }
            }
            
            /// <summary>
            /// Validator signature
            /// </summary>
            [FhirElement("attestationSignature", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Signature AttestationSignature
            {
                get { return _AttestationSignature; }
                set { _AttestationSignature = value; OnPropertyChanged("AttestationSignature"); }
            }
            
            private Hl7.Fhir.Model.R4.Signature _AttestationSignature;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ValidatorComponent");
                base.Serialize(sink);
                sink.Element("organization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Organization?.Serialize(sink);
                sink.Element("identityCertificate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); IdentityCertificateElement?.Serialize(sink);
                sink.Element("attestationSignature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AttestationSignature?.Serialize(sink);
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
                    case "organization":
                        Organization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "identityCertificate":
                        IdentityCertificateElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "attestationSignature":
                        AttestationSignature = source.Get<Hl7.Fhir.Model.R4.Signature>();
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
                    case "organization":
                        Organization = source.Populate(Organization);
                        return true;
                    case "identityCertificate":
                        IdentityCertificateElement = source.PopulateValue(IdentityCertificateElement);
                        return true;
                    case "_identityCertificate":
                        IdentityCertificateElement = source.Populate(IdentityCertificateElement);
                        return true;
                    case "attestationSignature":
                        AttestationSignature = source.Populate(AttestationSignature);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValidatorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                    if(IdentityCertificateElement != null) dest.IdentityCertificateElement = (Hl7.Fhir.Model.FhirString)IdentityCertificateElement.DeepCopy();
                    if(AttestationSignature != null) dest.AttestationSignature = (Hl7.Fhir.Model.R4.Signature)AttestationSignature.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ValidatorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValidatorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(IdentityCertificateElement, otherT.IdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(AttestationSignature, otherT.AttestationSignature)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValidatorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(IdentityCertificateElement, otherT.IdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(AttestationSignature, otherT.AttestationSignature)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Organization != null) yield return Organization;
                    if (IdentityCertificateElement != null) yield return IdentityCertificateElement;
                    if (AttestationSignature != null) yield return AttestationSignature;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Organization != null) yield return new ElementValue("organization", Organization);
                    if (IdentityCertificateElement != null) yield return new ElementValue("identityCertificate", IdentityCertificateElement);
                    if (AttestationSignature != null) yield return new ElementValue("attestationSignature", AttestationSignature);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// A resource that was validated
        /// </summary>
        [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Target
        {
            get { if(_Target==null) _Target = new List<Hl7.Fhir.Model.ResourceReference>(); return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Target;
        
        /// <summary>
        /// The fhirpath location(s) within the resource that was validated
        /// </summary>
        [FhirElement("targetLocation", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> TargetLocationElement
        {
            get { if(_TargetLocationElement==null) _TargetLocationElement = new List<Hl7.Fhir.Model.FhirString>(); return _TargetLocationElement; }
            set { _TargetLocationElement = value; OnPropertyChanged("TargetLocationElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _TargetLocationElement;
        
        /// <summary>
        /// The fhirpath location(s) within the resource that was validated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> TargetLocation
        {
            get { return TargetLocationElement != null ? TargetLocationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    TargetLocationElement = null;
                else
                    TargetLocationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("TargetLocation");
            }
        }
        
        /// <summary>
        /// none | initial | periodic
        /// </summary>
        [FhirElement("need", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Need
        {
            get { return _Need; }
            set { _Need = value; OnPropertyChanged("Need"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Need;
        
        /// <summary>
        /// attested | validated | in-process | req-revalid | val-fail | reval-fail
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.status> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.status> _StatusElement;
        
        /// <summary>
        /// attested | validated | in-process | req-revalid | val-fail | reval-fail
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.status? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.status>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// When the validation status was updated
        /// </summary>
        [FhirElement("statusDate", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime StatusDateElement
        {
            get { return _StatusDateElement; }
            set { _StatusDateElement = value; OnPropertyChanged("StatusDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _StatusDateElement;
        
        /// <summary>
        /// When the validation status was updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string StatusDate
        {
            get { return StatusDateElement != null ? StatusDateElement.Value : null; }
            set
            {
                if (value == null)
                    StatusDateElement = null;
                else
                    StatusDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("StatusDate");
            }
        }
        
        /// <summary>
        /// nothing | primary | multiple
        /// </summary>
        [FhirElement("validationType", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ValidationType
        {
            get { return _ValidationType; }
            set { _ValidationType = value; OnPropertyChanged("ValidationType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ValidationType;
        
        /// <summary>
        /// The primary process by which the target is validated (edit check; value set; primary source; multiple sources; standalone; in context)
        /// </summary>
        [FhirElement("validationProcess", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ValidationProcess
        {
            get { if(_ValidationProcess==null) _ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ValidationProcess; }
            set { _ValidationProcess = value; OnPropertyChanged("ValidationProcess"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ValidationProcess;
        
        /// <summary>
        /// Frequency of revalidation
        /// </summary>
        [FhirElement("frequency", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.R4.Timing Frequency
        {
            get { return _Frequency; }
            set { _Frequency = value; OnPropertyChanged("Frequency"); }
        }
        
        private Hl7.Fhir.Model.R4.Timing _Frequency;
        
        /// <summary>
        /// The date/time validation was last completed (including failed validations)
        /// </summary>
        [FhirElement("lastPerformed", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastPerformedElement
        {
            get { return _LastPerformedElement; }
            set { _LastPerformedElement = value; OnPropertyChanged("LastPerformedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LastPerformedElement;
        
        /// <summary>
        /// The date/time validation was last completed (including failed validations)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastPerformed
        {
            get { return LastPerformedElement != null ? LastPerformedElement.Value : null; }
            set
            {
                if (value == null)
                    LastPerformedElement = null;
                else
                    LastPerformedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastPerformed");
            }
        }
        
        /// <summary>
        /// The date when target is next validated, if appropriate
        /// </summary>
        [FhirElement("nextScheduled", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Date NextScheduledElement
        {
            get { return _NextScheduledElement; }
            set { _NextScheduledElement = value; OnPropertyChanged("NextScheduledElement"); }
        }
        
        private Hl7.Fhir.Model.Date _NextScheduledElement;
        
        /// <summary>
        /// The date when target is next validated, if appropriate
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string NextScheduled
        {
            get { return NextScheduledElement != null ? NextScheduledElement.Value : null; }
            set
            {
                if (value == null)
                    NextScheduledElement = null;
                else
                    NextScheduledElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("NextScheduled");
            }
        }
        
        /// <summary>
        /// fatal | warn | rec-only | none
        /// </summary>
        [FhirElement("failureAction", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FailureAction
        {
            get { return _FailureAction; }
            set { _FailureAction = value; OnPropertyChanged("FailureAction"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FailureAction;
        
        /// <summary>
        /// Information about the primary source(s) involved in validation
        /// </summary>
        [FhirElement("primarySource", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PrimarySourceComponent> PrimarySource
        {
            get { if(_PrimarySource==null) _PrimarySource = new List<PrimarySourceComponent>(); return _PrimarySource; }
            set { _PrimarySource = value; OnPropertyChanged("PrimarySource"); }
        }
        
        private List<PrimarySourceComponent> _PrimarySource;
        
        /// <summary>
        /// Information about the entity attesting to information
        /// </summary>
        [FhirElement("attestation", Order=210)]
        [DataMember]
        public AttestationComponent Attestation
        {
            get { return _Attestation; }
            set { _Attestation = value; OnPropertyChanged("Attestation"); }
        }
        
        private AttestationComponent _Attestation;
        
        /// <summary>
        /// Information about the entity validating information
        /// </summary>
        [FhirElement("validator", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ValidatorComponent> Validator
        {
            get { if(_Validator==null) _Validator = new List<ValidatorComponent>(); return _Validator; }
            set { _Validator = value; OnPropertyChanged("Validator"); }
        }
        
        private List<ValidatorComponent> _Validator;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as VerificationResult;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Target != null) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
                if(TargetLocationElement != null) dest.TargetLocationElement = new List<Hl7.Fhir.Model.FhirString>(TargetLocationElement.DeepCopy());
                if(Need != null) dest.Need = (Hl7.Fhir.Model.CodeableConcept)Need.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.status>)StatusElement.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.FhirDateTime)StatusDateElement.DeepCopy();
                if(ValidationType != null) dest.ValidationType = (Hl7.Fhir.Model.CodeableConcept)ValidationType.DeepCopy();
                if(ValidationProcess != null) dest.ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(ValidationProcess.DeepCopy());
                if(Frequency != null) dest.Frequency = (Hl7.Fhir.Model.R4.Timing)Frequency.DeepCopy();
                if(LastPerformedElement != null) dest.LastPerformedElement = (Hl7.Fhir.Model.FhirDateTime)LastPerformedElement.DeepCopy();
                if(NextScheduledElement != null) dest.NextScheduledElement = (Hl7.Fhir.Model.Date)NextScheduledElement.DeepCopy();
                if(FailureAction != null) dest.FailureAction = (Hl7.Fhir.Model.CodeableConcept)FailureAction.DeepCopy();
                if(PrimarySource != null) dest.PrimarySource = new List<PrimarySourceComponent>(PrimarySource.DeepCopy());
                if(Attestation != null) dest.Attestation = (AttestationComponent)Attestation.DeepCopy();
                if(Validator != null) dest.Validator = new List<ValidatorComponent>(Validator.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new VerificationResult());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as VerificationResult;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(TargetLocationElement, otherT.TargetLocationElement)) return false;
            if( !DeepComparable.Matches(Need, otherT.Need)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(ValidationType, otherT.ValidationType)) return false;
            if( !DeepComparable.Matches(ValidationProcess, otherT.ValidationProcess)) return false;
            if( !DeepComparable.Matches(Frequency, otherT.Frequency)) return false;
            if( !DeepComparable.Matches(LastPerformedElement, otherT.LastPerformedElement)) return false;
            if( !DeepComparable.Matches(NextScheduledElement, otherT.NextScheduledElement)) return false;
            if( !DeepComparable.Matches(FailureAction, otherT.FailureAction)) return false;
            if( !DeepComparable.Matches(PrimarySource, otherT.PrimarySource)) return false;
            if( !DeepComparable.Matches(Attestation, otherT.Attestation)) return false;
            if( !DeepComparable.Matches(Validator, otherT.Validator)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as VerificationResult;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(TargetLocationElement, otherT.TargetLocationElement)) return false;
            if( !DeepComparable.IsExactly(Need, otherT.Need)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(ValidationType, otherT.ValidationType)) return false;
            if( !DeepComparable.IsExactly(ValidationProcess, otherT.ValidationProcess)) return false;
            if( !DeepComparable.IsExactly(Frequency, otherT.Frequency)) return false;
            if( !DeepComparable.IsExactly(LastPerformedElement, otherT.LastPerformedElement)) return false;
            if( !DeepComparable.IsExactly(NextScheduledElement, otherT.NextScheduledElement)) return false;
            if( !DeepComparable.IsExactly(FailureAction, otherT.FailureAction)) return false;
            if( !DeepComparable.IsExactly(PrimarySource, otherT.PrimarySource)) return false;
            if( !DeepComparable.IsExactly(Attestation, otherT.Attestation)) return false;
            if( !DeepComparable.IsExactly(Validator, otherT.Validator)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("VerificationResult");
            base.Serialize(sink);
            sink.BeginList("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Target)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("targetLocation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(TargetLocationElement);
            sink.End();
            sink.Element("need", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Need?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("statusDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusDateElement?.Serialize(sink);
            sink.Element("validationType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValidationType?.Serialize(sink);
            sink.BeginList("validationProcess", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ValidationProcess)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("frequency", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Frequency?.Serialize(sink);
            sink.Element("lastPerformed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LastPerformedElement?.Serialize(sink);
            sink.Element("nextScheduled", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NextScheduledElement?.Serialize(sink);
            sink.Element("failureAction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); FailureAction?.Serialize(sink);
            sink.BeginList("primarySource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in PrimarySource)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("attestation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Attestation?.Serialize(sink);
            sink.BeginList("validator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Validator)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                case "target":
                    Target = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "targetLocation":
                    TargetLocationElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "need":
                    Need = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.status>>();
                    return true;
                case "statusDate":
                    StatusDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "validationType":
                    ValidationType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "validationProcess":
                    ValidationProcess = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "frequency":
                    Frequency = source.Get<Hl7.Fhir.Model.R4.Timing>();
                    return true;
                case "lastPerformed":
                    LastPerformedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "nextScheduled":
                    NextScheduledElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "failureAction":
                    FailureAction = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "primarySource":
                    PrimarySource = source.GetList<PrimarySourceComponent>();
                    return true;
                case "attestation":
                    Attestation = source.Get<AttestationComponent>();
                    return true;
                case "validator":
                    Validator = source.GetList<ValidatorComponent>();
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
                case "target":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "targetLocation":
                case "_targetLocation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "need":
                    Need = source.Populate(Need);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusDate":
                    StatusDateElement = source.PopulateValue(StatusDateElement);
                    return true;
                case "_statusDate":
                    StatusDateElement = source.Populate(StatusDateElement);
                    return true;
                case "validationType":
                    ValidationType = source.Populate(ValidationType);
                    return true;
                case "validationProcess":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "frequency":
                    Frequency = source.Populate(Frequency);
                    return true;
                case "lastPerformed":
                    LastPerformedElement = source.PopulateValue(LastPerformedElement);
                    return true;
                case "_lastPerformed":
                    LastPerformedElement = source.Populate(LastPerformedElement);
                    return true;
                case "nextScheduled":
                    NextScheduledElement = source.PopulateValue(NextScheduledElement);
                    return true;
                case "_nextScheduled":
                    NextScheduledElement = source.Populate(NextScheduledElement);
                    return true;
                case "failureAction":
                    FailureAction = source.Populate(FailureAction);
                    return true;
                case "primarySource":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "attestation":
                    Attestation = source.Populate(Attestation);
                    return true;
                case "validator":
                    source.SetList(this, jsonPropertyName);
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
                case "target":
                    source.PopulateListItem(Target, index);
                    return true;
                case "targetLocation":
                    source.PopulatePrimitiveListItemValue(TargetLocationElement, index);
                    return true;
                case "_targetLocation":
                    source.PopulatePrimitiveListItem(TargetLocationElement, index);
                    return true;
                case "validationProcess":
                    source.PopulateListItem(ValidationProcess, index);
                    return true;
                case "primarySource":
                    source.PopulateListItem(PrimarySource, index);
                    return true;
                case "validator":
                    source.PopulateListItem(Validator, index);
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
                foreach (var elem in Target) { if (elem != null) yield return elem; }
                foreach (var elem in TargetLocationElement) { if (elem != null) yield return elem; }
                if (Need != null) yield return Need;
                if (StatusElement != null) yield return StatusElement;
                if (StatusDateElement != null) yield return StatusDateElement;
                if (ValidationType != null) yield return ValidationType;
                foreach (var elem in ValidationProcess) { if (elem != null) yield return elem; }
                if (Frequency != null) yield return Frequency;
                if (LastPerformedElement != null) yield return LastPerformedElement;
                if (NextScheduledElement != null) yield return NextScheduledElement;
                if (FailureAction != null) yield return FailureAction;
                foreach (var elem in PrimarySource) { if (elem != null) yield return elem; }
                if (Attestation != null) yield return Attestation;
                foreach (var elem in Validator) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
                foreach (var elem in TargetLocationElement) { if (elem != null) yield return new ElementValue("targetLocation", elem); }
                if (Need != null) yield return new ElementValue("need", Need);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusDateElement != null) yield return new ElementValue("statusDate", StatusDateElement);
                if (ValidationType != null) yield return new ElementValue("validationType", ValidationType);
                foreach (var elem in ValidationProcess) { if (elem != null) yield return new ElementValue("validationProcess", elem); }
                if (Frequency != null) yield return new ElementValue("frequency", Frequency);
                if (LastPerformedElement != null) yield return new ElementValue("lastPerformed", LastPerformedElement);
                if (NextScheduledElement != null) yield return new ElementValue("nextScheduled", NextScheduledElement);
                if (FailureAction != null) yield return new ElementValue("failureAction", FailureAction);
                foreach (var elem in PrimarySource) { if (elem != null) yield return new ElementValue("primarySource", elem); }
                if (Attestation != null) yield return new ElementValue("attestation", Attestation);
                foreach (var elem in Validator) { if (elem != null) yield return new ElementValue("validator", elem); }
            }
        }
    
    }

}
