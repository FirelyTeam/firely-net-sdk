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
    /// A healthcare consumer's  choices to permit or deny recipients or roles to perform actions for specific purposes and periods of time
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Consent", IsResource=true)]
    [DataContract]
    public partial class Consent : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IConsent, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Consent; } }
        [NotMapped]
        public override string TypeName { get { return "Consent"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PolicyComponent")]
        [DataContract]
        public partial class PolicyComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConsentPolicyComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PolicyComponent"; } }
            
            /// <summary>
            /// Enforcement source for policy
            /// </summary>
            [FhirElement("authority", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri AuthorityElement
            {
                get { return _AuthorityElement; }
                set { _AuthorityElement = value; OnPropertyChanged("AuthorityElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _AuthorityElement;
            
            /// <summary>
            /// Enforcement source for policy
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Authority
            {
                get { return AuthorityElement != null ? AuthorityElement.Value : null; }
                set
                {
                    if (value == null)
                        AuthorityElement = null;
                    else
                        AuthorityElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Authority");
                }
            }
            
            /// <summary>
            /// Specific policy covered by this consent
            /// </summary>
            [FhirElement("uri", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// Specific policy covered by this consent
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if (value == null)
                        UriElement = null;
                    else
                        UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PolicyComponent");
                base.Serialize(sink);
                sink.Element("authority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AuthorityElement?.Serialize(sink);
                sink.Element("uri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UriElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "authority":
                        AuthorityElement = source.PopulateValue(AuthorityElement);
                        return true;
                    case "_authority":
                        AuthorityElement = source.Populate(AuthorityElement);
                        return true;
                    case "uri":
                        UriElement = source.PopulateValue(UriElement);
                        return true;
                    case "_uri":
                        UriElement = source.Populate(UriElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PolicyComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AuthorityElement != null) dest.AuthorityElement = (Hl7.Fhir.Model.FhirUri)AuthorityElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PolicyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PolicyComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AuthorityElement, otherT.AuthorityElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PolicyComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AuthorityElement, otherT.AuthorityElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AuthorityElement != null) yield return AuthorityElement;
                    if (UriElement != null) yield return UriElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AuthorityElement != null) yield return new ElementValue("authority", AuthorityElement);
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "VerificationComponent")]
        [DataContract]
        public partial class VerificationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "VerificationComponent"; } }
            
            /// <summary>
            /// Has been verified
            /// </summary>
            [FhirElement("verified", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean VerifiedElement
            {
                get { return _VerifiedElement; }
                set { _VerifiedElement = value; OnPropertyChanged("VerifiedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _VerifiedElement;
            
            /// <summary>
            /// Has been verified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Verified
            {
                get { return VerifiedElement != null ? VerifiedElement.Value : null; }
                set
                {
                    if (value == null)
                        VerifiedElement = null;
                    else
                        VerifiedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Verified");
                }
            }
            
            /// <summary>
            /// Person who verified
            /// </summary>
            [FhirElement("verifiedWith", Order=50)]
            [CLSCompliant(false)]
            [References("Patient","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference VerifiedWith
            {
                get { return _VerifiedWith; }
                set { _VerifiedWith = value; OnPropertyChanged("VerifiedWith"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _VerifiedWith;
            
            /// <summary>
            /// When consent verified
            /// </summary>
            [FhirElement("verificationDate", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime VerificationDateElement
            {
                get { return _VerificationDateElement; }
                set { _VerificationDateElement = value; OnPropertyChanged("VerificationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _VerificationDateElement;
            
            /// <summary>
            /// When consent verified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VerificationDate
            {
                get { return VerificationDateElement != null ? VerificationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        VerificationDateElement = null;
                    else
                        VerificationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("VerificationDate");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("VerificationComponent");
                base.Serialize(sink);
                sink.Element("verified", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); VerifiedElement?.Serialize(sink);
                sink.Element("verifiedWith", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VerifiedWith?.Serialize(sink);
                sink.Element("verificationDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VerificationDateElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "verified":
                        VerifiedElement = source.PopulateValue(VerifiedElement);
                        return true;
                    case "_verified":
                        VerifiedElement = source.Populate(VerifiedElement);
                        return true;
                    case "verifiedWith":
                        VerifiedWith = source.Populate(VerifiedWith);
                        return true;
                    case "verificationDate":
                        VerificationDateElement = source.PopulateValue(VerificationDateElement);
                        return true;
                    case "_verificationDate":
                        VerificationDateElement = source.Populate(VerificationDateElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VerificationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(VerifiedElement != null) dest.VerifiedElement = (Hl7.Fhir.Model.FhirBoolean)VerifiedElement.DeepCopy();
                    if(VerifiedWith != null) dest.VerifiedWith = (Hl7.Fhir.Model.ResourceReference)VerifiedWith.DeepCopy();
                    if(VerificationDateElement != null) dest.VerificationDateElement = (Hl7.Fhir.Model.FhirDateTime)VerificationDateElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new VerificationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VerificationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(VerifiedElement, otherT.VerifiedElement)) return false;
                if( !DeepComparable.Matches(VerifiedWith, otherT.VerifiedWith)) return false;
                if( !DeepComparable.Matches(VerificationDateElement, otherT.VerificationDateElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VerificationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(VerifiedElement, otherT.VerifiedElement)) return false;
                if( !DeepComparable.IsExactly(VerifiedWith, otherT.VerifiedWith)) return false;
                if( !DeepComparable.IsExactly(VerificationDateElement, otherT.VerificationDateElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (VerifiedElement != null) yield return VerifiedElement;
                    if (VerifiedWith != null) yield return VerifiedWith;
                    if (VerificationDateElement != null) yield return VerificationDateElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (VerifiedElement != null) yield return new ElementValue("verified", VerifiedElement);
                    if (VerifiedWith != null) yield return new ElementValue("verifiedWith", VerifiedWith);
                    if (VerificationDateElement != null) yield return new ElementValue("verificationDate", VerificationDateElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "provisionComponent")]
        [DataContract]
        public partial class provisionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "provisionComponent"; } }
            
            /// <summary>
            /// deny | permit
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.ConsentProvisionType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.ConsentProvisionType> _TypeElement;
            
            /// <summary>
            /// deny | permit
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.ConsentProvisionType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.ConsentProvisionType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Timeframe for this rule
            /// </summary>
            [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// Who|what controlled by this rule (or group, by role)
            /// </summary>
            [FhirElement("actor", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<provisionActorComponent> Actor
            {
                get { if(_Actor==null) _Actor = new List<provisionActorComponent>(); return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private List<provisionActorComponent> _Actor;
            
            /// <summary>
            /// Actions controlled by this rule
            /// </summary>
            [FhirElement("action", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Action;
            
            /// <summary>
            /// Security Labels that define affected resources
            /// </summary>
            [FhirElement("securityLabel", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> SecurityLabel
            {
                get { if(_SecurityLabel==null) _SecurityLabel = new List<Hl7.Fhir.Model.Coding>(); return _SecurityLabel; }
                set { _SecurityLabel = value; OnPropertyChanged("SecurityLabel"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _SecurityLabel;
            
            /// <summary>
            /// Context of activities covered by this rule
            /// </summary>
            [FhirElement("purpose", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Purpose
            {
                get { if(_Purpose==null) _Purpose = new List<Hl7.Fhir.Model.Coding>(); return _Purpose; }
                set { _Purpose = value; OnPropertyChanged("Purpose"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Purpose;
            
            /// <summary>
            /// e.g. Resource Type, Profile, CDA, etc.
            /// </summary>
            [FhirElement("class", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Class
            {
                get { if(_Class==null) _Class = new List<Hl7.Fhir.Model.Coding>(); return _Class; }
                set { _Class = value; OnPropertyChanged("Class"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Class;
            
            /// <summary>
            /// e.g. LOINC or SNOMED CT code, etc. in the content
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Timeframe for data controlled by this rule
            /// </summary>
            [FhirElement("dataPeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Period DataPeriod
            {
                get { return _DataPeriod; }
                set { _DataPeriod = value; OnPropertyChanged("DataPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _DataPeriod;
            
            /// <summary>
            /// Data controlled by this rule
            /// </summary>
            [FhirElement("data", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<provisionDataComponent> Data
            {
                get { if(_Data==null) _Data = new List<provisionDataComponent>(); return _Data; }
                set { _Data = value; OnPropertyChanged("Data"); }
            }
            
            private List<provisionDataComponent> _Data;
            
            /// <summary>
            /// Nested Exception Rules
            /// </summary>
            [FhirElement("provision", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<provisionComponent> Provision
            {
                get { if(_Provision==null) _Provision = new List<provisionComponent>(); return _Provision; }
                set { _Provision = value; OnPropertyChanged("Provision"); }
            }
            
            private List<provisionComponent> _Provision;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("provisionComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeElement?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
                sink.BeginList("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Actor)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Action)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("securityLabel", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in SecurityLabel)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Purpose)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("class", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Class)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Code)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("dataPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DataPeriod?.Serialize(sink);
                sink.BeginList("data", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Data)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("provision", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Provision)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                    case "actor":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "action":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "securityLabel":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "purpose":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "class":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "code":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "dataPeriod":
                        DataPeriod = source.Populate(DataPeriod);
                        return true;
                    case "data":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "provision":
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
                    case "actor":
                        source.PopulateListItem(Actor, index);
                        return true;
                    case "action":
                        source.PopulateListItem(Action, index);
                        return true;
                    case "securityLabel":
                        source.PopulateListItem(SecurityLabel, index);
                        return true;
                    case "purpose":
                        source.PopulateListItem(Purpose, index);
                        return true;
                    case "class":
                        source.PopulateListItem(Class, index);
                        return true;
                    case "code":
                        source.PopulateListItem(Code, index);
                        return true;
                    case "data":
                        source.PopulateListItem(Data, index);
                        return true;
                    case "provision":
                        source.PopulateListItem(Provision, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as provisionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.ConsentProvisionType>)TypeElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Actor != null) dest.Actor = new List<provisionActorComponent>(Actor.DeepCopy());
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                    if(Purpose != null) dest.Purpose = new List<Hl7.Fhir.Model.Coding>(Purpose.DeepCopy());
                    if(Class != null) dest.Class = new List<Hl7.Fhir.Model.Coding>(Class.DeepCopy());
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(DataPeriod != null) dest.DataPeriod = (Hl7.Fhir.Model.Period)DataPeriod.DeepCopy();
                    if(Data != null) dest.Data = new List<provisionDataComponent>(Data.DeepCopy());
                    if(Provision != null) dest.Provision = new List<provisionComponent>(Provision.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new provisionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as provisionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.Matches(Class, otherT.Class)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(DataPeriod, otherT.DataPeriod)) return false;
                if( !DeepComparable.Matches(Data, otherT.Data)) return false;
                if( !DeepComparable.Matches(Provision, otherT.Provision)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as provisionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(DataPeriod, otherT.DataPeriod)) return false;
                if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
                if( !DeepComparable.IsExactly(Provision, otherT.Provision)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Period != null) yield return Period;
                    foreach (var elem in Actor) { if (elem != null) yield return elem; }
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return elem; }
                    foreach (var elem in Purpose) { if (elem != null) yield return elem; }
                    foreach (var elem in Class) { if (elem != null) yield return elem; }
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    if (DataPeriod != null) yield return DataPeriod;
                    foreach (var elem in Data) { if (elem != null) yield return elem; }
                    foreach (var elem in Provision) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                    foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return new ElementValue("securityLabel", elem); }
                    foreach (var elem in Purpose) { if (elem != null) yield return new ElementValue("purpose", elem); }
                    foreach (var elem in Class) { if (elem != null) yield return new ElementValue("class", elem); }
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    if (DataPeriod != null) yield return new ElementValue("dataPeriod", DataPeriod);
                    foreach (var elem in Data) { if (elem != null) yield return new ElementValue("data", elem); }
                    foreach (var elem in Provision) { if (elem != null) yield return new ElementValue("provision", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "provisionActorComponent")]
        [DataContract]
        public partial class provisionActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "provisionActorComponent"; } }
            
            /// <summary>
            /// How the actor is involved
            /// </summary>
            [FhirElement("role", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Resource for the actor (or group, by role)
            /// </summary>
            [FhirElement("reference", Order=50)]
            [CLSCompliant(false)]
            [References("Device","Group","CareTeam","Organization","Patient","Practitioner","RelatedPerson","PractitionerRole")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("provisionActorComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Role?.Serialize(sink);
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Reference?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "reference":
                        Reference = source.Populate(Reference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as provisionActorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new provisionActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as provisionActorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as provisionActorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Role != null) yield return Role;
                    if (Reference != null) yield return Reference;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "provisionDataComponent")]
        [DataContract]
        public partial class provisionDataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "provisionDataComponent"; } }
            
            /// <summary>
            /// instance | related | dependents | authoredby
            /// </summary>
            [FhirElement("meaning", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ConsentDataMeaning> MeaningElement
            {
                get { return _MeaningElement; }
                set { _MeaningElement = value; OnPropertyChanged("MeaningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ConsentDataMeaning> _MeaningElement;
            
            /// <summary>
            /// instance | related | dependents | authoredby
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ConsentDataMeaning? Meaning
            {
                get { return MeaningElement != null ? MeaningElement.Value : null; }
                set
                {
                    if (value == null)
                        MeaningElement = null;
                    else
                        MeaningElement = new Code<Hl7.Fhir.Model.ConsentDataMeaning>(value);
                    OnPropertyChanged("Meaning");
                }
            }
            
            /// <summary>
            /// The actual data reference
            /// </summary>
            [FhirElement("reference", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("provisionDataComponent");
                base.Serialize(sink);
                sink.Element("meaning", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); MeaningElement?.Serialize(sink);
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Reference?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "meaning":
                        MeaningElement = source.PopulateValue(MeaningElement);
                        return true;
                    case "_meaning":
                        MeaningElement = source.Populate(MeaningElement);
                        return true;
                    case "reference":
                        Reference = source.Populate(Reference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as provisionDataComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(MeaningElement != null) dest.MeaningElement = (Code<Hl7.Fhir.Model.ConsentDataMeaning>)MeaningElement.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new provisionDataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as provisionDataComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MeaningElement, otherT.MeaningElement)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as provisionDataComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(MeaningElement, otherT.MeaningElement)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (MeaningElement != null) yield return MeaningElement;
                    if (Reference != null) yield return Reference;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (MeaningElement != null) yield return new ElementValue("meaning", MeaningElement);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IConsentPolicyComponent> Hl7.Fhir.Model.IConsent.Policy { get { return Policy; } }
    
        
        /// <summary>
        /// Identifier for this record (external references)
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ConsentState> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ConsentState> _StatusElement;
        
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ConsentState? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.ConsentState>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Which of the four areas this resource covers (extensible)
        /// </summary>
        [FhirElement("scope", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Scope
        {
            get { return _Scope; }
            set { _Scope = value; OnPropertyChanged("Scope"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Scope;
        
        /// <summary>
        /// Classification of the consent statement - for indexing/retrieval
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Who the consent applies to
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// When this Consent was created or indexed
        /// </summary>
        [FhirElement("dateTime", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateTimeElement
        {
            get { return _DateTimeElement; }
            set { _DateTimeElement = value; OnPropertyChanged("DateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateTimeElement;
        
        /// <summary>
        /// When this Consent was created or indexed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateTime
        {
            get { return DateTimeElement != null ? DateTimeElement.Value : null; }
            set
            {
                if (value == null)
                    DateTimeElement = null;
                else
                    DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateTime");
            }
        }
        
        /// <summary>
        /// Who is agreeing to the policy and rules
        /// </summary>
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Organization","Patient","Practitioner","RelatedPerson","PractitionerRole")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Performer;
        
        /// <summary>
        /// Custodian of the consent
        /// </summary>
        [FhirElement("organization", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Organization
        {
            get { if(_Organization==null) _Organization = new List<Hl7.Fhir.Model.ResourceReference>(); return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Organization;
        
        /// <summary>
        /// Source from which this consent is taken
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.Element _Source;
        
        /// <summary>
        /// Policies covered by this consent
        /// </summary>
        [FhirElement("policy", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PolicyComponent> Policy
        {
            get { if(_Policy==null) _Policy = new List<PolicyComponent>(); return _Policy; }
            set { _Policy = value; OnPropertyChanged("Policy"); }
        }
        
        private List<PolicyComponent> _Policy;
        
        /// <summary>
        /// Regulation that this consents to
        /// </summary>
        [FhirElement("policyRule", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PolicyRule
        {
            get { return _PolicyRule; }
            set { _PolicyRule = value; OnPropertyChanged("PolicyRule"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PolicyRule;
        
        /// <summary>
        /// Consent Verified by patient or family
        /// </summary>
        [FhirElement("verification", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<VerificationComponent> Verification
        {
            get { if(_Verification==null) _Verification = new List<VerificationComponent>(); return _Verification; }
            set { _Verification = value; OnPropertyChanged("Verification"); }
        }
        
        private List<VerificationComponent> _Verification;
        
        /// <summary>
        /// Constraints to the base Consent.policyRule
        /// </summary>
        [FhirElement("provision", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public provisionComponent Provision
        {
            get { return _Provision; }
            set { _Provision = value; OnPropertyChanged("Provision"); }
        }
        
        private provisionComponent _Provision;
    
    
        public static ElementDefinitionConstraint[] Consent_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ppc-4",
                severity: ConstraintSeverity.Warning,
                expression: "patient.exists() or scope.coding.where(system='something' and code='adr').exists().not()",
                human: "IF Scope=adr, there must be a patient",
                xpath: "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='adr'])))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ppc-5",
                severity: ConstraintSeverity.Warning,
                expression: "patient.exists() or scope.coding.where(system='something' and code='treatment').exists().not()",
                human: "IF Scope=treatment, there must be a patient",
                xpath: "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='treatment'])))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ppc-2",
                severity: ConstraintSeverity.Warning,
                expression: "patient.exists() or scope.coding.where(system='something' and code='patient-privacy').exists().not()",
                human: "IF Scope=privacy, there must be a patient",
                xpath: "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='patient-privacy'])))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ppc-3",
                severity: ConstraintSeverity.Warning,
                expression: "patient.exists() or scope.coding.where(system='something' and code='research').exists().not()",
                human: "IF Scope=research, there must be a patient",
                xpath: "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='research'])))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ppc-1",
                severity: ConstraintSeverity.Warning,
                expression: "policy.exists() or policyRule.exists()",
                human: "Either a Policy or PolicyRule",
                xpath: "exists(f:policy) or exists(f:policyRule)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Consent_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Consent;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ConsentState>)StatusElement.DeepCopy();
                if(Scope != null) dest.Scope = (Hl7.Fhir.Model.CodeableConcept)Scope.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                if(Organization != null) dest.Organization = new List<Hl7.Fhir.Model.ResourceReference>(Organization.DeepCopy());
                if(Source != null) dest.Source = (Hl7.Fhir.Model.Element)Source.DeepCopy();
                if(Policy != null) dest.Policy = new List<PolicyComponent>(Policy.DeepCopy());
                if(PolicyRule != null) dest.PolicyRule = (Hl7.Fhir.Model.CodeableConcept)PolicyRule.DeepCopy();
                if(Verification != null) dest.Verification = new List<VerificationComponent>(Verification.DeepCopy());
                if(Provision != null) dest.Provision = (provisionComponent)Provision.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Consent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Consent;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Scope, otherT.Scope)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Policy, otherT.Policy)) return false;
            if( !DeepComparable.Matches(PolicyRule, otherT.PolicyRule)) return false;
            if( !DeepComparable.Matches(Verification, otherT.Verification)) return false;
            if( !DeepComparable.Matches(Provision, otherT.Provision)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Consent;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Scope, otherT.Scope)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Policy, otherT.Policy)) return false;
            if( !DeepComparable.IsExactly(PolicyRule, otherT.PolicyRule)) return false;
            if( !DeepComparable.IsExactly(Verification, otherT.Verification)) return false;
            if( !DeepComparable.IsExactly(Provision, otherT.Provision)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Consent");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("scope", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Scope?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Patient?.Serialize(sink);
            sink.Element("dateTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateTimeElement?.Serialize(sink);
            sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Performer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("organization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Organization)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Source?.Serialize(sink);
            sink.BeginList("policy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Policy)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("policyRule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PolicyRule?.Serialize(sink);
            sink.BeginList("verification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Verification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("provision", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Provision?.Serialize(sink);
            sink.End();
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
                case "scope":
                    Scope = source.Populate(Scope);
                    return true;
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "dateTime":
                    DateTimeElement = source.PopulateValue(DateTimeElement);
                    return true;
                case "_dateTime":
                    DateTimeElement = source.Populate(DateTimeElement);
                    return true;
                case "performer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "organization":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "sourceAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Source, "source");
                    Source = source.Populate(Source as Hl7.Fhir.Model.Attachment);
                    return true;
                case "sourceReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Source, "source");
                    Source = source.Populate(Source as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "policy":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "policyRule":
                    PolicyRule = source.Populate(PolicyRule);
                    return true;
                case "verification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "provision":
                    Provision = source.Populate(Provision);
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
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "performer":
                    source.PopulateListItem(Performer, index);
                    return true;
                case "organization":
                    source.PopulateListItem(Organization, index);
                    return true;
                case "policy":
                    source.PopulateListItem(Policy, index);
                    return true;
                case "verification":
                    source.PopulateListItem(Verification, index);
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
                if (Scope != null) yield return Scope;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (Patient != null) yield return Patient;
                if (DateTimeElement != null) yield return DateTimeElement;
                foreach (var elem in Performer) { if (elem != null) yield return elem; }
                foreach (var elem in Organization) { if (elem != null) yield return elem; }
                if (Source != null) yield return Source;
                foreach (var elem in Policy) { if (elem != null) yield return elem; }
                if (PolicyRule != null) yield return PolicyRule;
                foreach (var elem in Verification) { if (elem != null) yield return elem; }
                if (Provision != null) yield return Provision;
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
                if (Scope != null) yield return new ElementValue("scope", Scope);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateTimeElement != null) yield return new ElementValue("dateTime", DateTimeElement);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                foreach (var elem in Organization) { if (elem != null) yield return new ElementValue("organization", elem); }
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Policy) { if (elem != null) yield return new ElementValue("policy", elem); }
                if (PolicyRule != null) yield return new ElementValue("policyRule", PolicyRule);
                foreach (var elem in Verification) { if (elem != null) yield return new ElementValue("verification", elem); }
                if (Provision != null) yield return new ElementValue("provision", Provision);
            }
        }
    
    }

}
