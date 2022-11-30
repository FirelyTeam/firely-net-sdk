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
    /// Event record kept for security purposes
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "AuditEvent", IsResource=true)]
    [DataContract]
    public partial class AuditEvent : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IAuditEvent, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AuditEvent; } }
        [NotMapped]
        public override string TypeName { get { return "AuditEvent"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AgentComponent")]
        [DataContract]
        public partial class AgentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AgentComponent"; } }
            
            /// <summary>
            /// How agent participated
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
            /// Agent role in the event
            /// </summary>
            [FhirElement("role", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { if(_Role==null) _Role = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
            
            /// <summary>
            /// Identifier of who
            /// </summary>
            [FhirElement("who", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [References("PractitionerRole","Practitioner","Organization","Device","Patient","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Who
            {
                get { return _Who; }
                set { _Who = value; OnPropertyChanged("Who"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Who;
            
            /// <summary>
            /// Alternative User identity
            /// </summary>
            [FhirElement("altId", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AltIdElement
            {
                get { return _AltIdElement; }
                set { _AltIdElement = value; OnPropertyChanged("AltIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _AltIdElement;
            
            /// <summary>
            /// Alternative User identity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AltId
            {
                get { return AltIdElement != null ? AltIdElement.Value : null; }
                set
                {
                    if (value == null)
                        AltIdElement = null;
                    else
                        AltIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("AltId");
                }
            }
            
            /// <summary>
            /// Human friendly name for the agent
            /// </summary>
            [FhirElement("name", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Human friendly name for the agent
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Whether user is initiator
            /// </summary>
            [FhirElement("requestor", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequestorElement
            {
                get { return _RequestorElement; }
                set { _RequestorElement = value; OnPropertyChanged("RequestorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RequestorElement;
            
            /// <summary>
            /// Whether user is initiator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Requestor
            {
                get { return RequestorElement != null ? RequestorElement.Value : null; }
                set
                {
                    if (value == null)
                        RequestorElement = null;
                    else
                        RequestorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Requestor");
                }
            }
            
            /// <summary>
            /// Where
            /// </summary>
            [FhirElement("location", Order=100)]
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
            /// Policy that authorized event
            /// </summary>
            [FhirElement("policy", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> PolicyElement
            {
                get { if(_PolicyElement==null) _PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(); return _PolicyElement; }
                set { _PolicyElement = value; OnPropertyChanged("PolicyElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _PolicyElement;
            
            /// <summary>
            /// Policy that authorized event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Policy
            {
                get { return PolicyElement != null ? PolicyElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PolicyElement = null;
                    else
                        PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Policy");
                }
            }
            
            /// <summary>
            /// Type of media
            /// </summary>
            [FhirElement("media", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Media
            {
                get { return _Media; }
                set { _Media = value; OnPropertyChanged("Media"); }
            }
            
            private Hl7.Fhir.Model.Coding _Media;
            
            /// <summary>
            /// Logical network location for application activity
            /// </summary>
            [FhirElement("network", Order=130)]
            [DataMember]
            public NetworkComponent Network
            {
                get { return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private NetworkComponent _Network;
            
            /// <summary>
            /// Reason given for this user
            /// </summary>
            [FhirElement("purposeOfUse", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> PurposeOfUse
            {
                get { if(_PurposeOfUse==null) _PurposeOfUse = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PurposeOfUse; }
                set { _PurposeOfUse = value; OnPropertyChanged("PurposeOfUse"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _PurposeOfUse;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AgentComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.BeginList("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Role)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("who", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Who?.Serialize(sink);
                sink.Element("altId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AltIdElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("requestor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RequestorElement?.Serialize(sink);
                sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Location?.Serialize(sink);
                sink.BeginList("policy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(PolicyElement);
                sink.End();
                sink.Element("media", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Media?.Serialize(sink);
                sink.Element("network", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Network?.Serialize(sink);
                sink.BeginList("purposeOfUse", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in PurposeOfUse)
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
                        Type = source.Populate(Type);
                        return true;
                    case "role":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "who":
                        Who = source.Populate(Who);
                        return true;
                    case "altId":
                        AltIdElement = source.PopulateValue(AltIdElement);
                        return true;
                    case "_altId":
                        AltIdElement = source.Populate(AltIdElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "requestor":
                        RequestorElement = source.PopulateValue(RequestorElement);
                        return true;
                    case "_requestor":
                        RequestorElement = source.Populate(RequestorElement);
                        return true;
                    case "location":
                        Location = source.Populate(Location);
                        return true;
                    case "policy":
                    case "_policy":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "media":
                        Media = source.Populate(Media);
                        return true;
                    case "network":
                        Network = source.Populate(Network);
                        return true;
                    case "purposeOfUse":
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
                    case "role":
                        source.PopulateListItem(Role, index);
                        return true;
                    case "policy":
                        source.PopulatePrimitiveListItemValue(PolicyElement, index);
                        return true;
                    case "_policy":
                        source.PopulatePrimitiveListItem(PolicyElement, index);
                        return true;
                    case "purposeOfUse":
                        source.PopulateListItem(PurposeOfUse, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AgentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
                    if(AltIdElement != null) dest.AltIdElement = (Hl7.Fhir.Model.FhirString)AltIdElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(RequestorElement != null) dest.RequestorElement = (Hl7.Fhir.Model.FhirBoolean)RequestorElement.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(PolicyElement != null) dest.PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(PolicyElement.DeepCopy());
                    if(Media != null) dest.Media = (Hl7.Fhir.Model.Coding)Media.DeepCopy();
                    if(Network != null) dest.Network = (NetworkComponent)Network.DeepCopy();
                    if(PurposeOfUse != null) dest.PurposeOfUse = new List<Hl7.Fhir.Model.CodeableConcept>(PurposeOfUse.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AgentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AgentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Who, otherT.Who)) return false;
                if( !DeepComparable.Matches(AltIdElement, otherT.AltIdElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(RequestorElement, otherT.RequestorElement)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(PolicyElement, otherT.PolicyElement)) return false;
                if( !DeepComparable.Matches(Media, otherT.Media)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(PurposeOfUse, otherT.PurposeOfUse)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AgentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
                if( !DeepComparable.IsExactly(AltIdElement, otherT.AltIdElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(RequestorElement, otherT.RequestorElement)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(PolicyElement, otherT.PolicyElement)) return false;
                if( !DeepComparable.IsExactly(Media, otherT.Media)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(PurposeOfUse, otherT.PurposeOfUse)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Role) { if (elem != null) yield return elem; }
                    if (Who != null) yield return Who;
                    if (AltIdElement != null) yield return AltIdElement;
                    if (NameElement != null) yield return NameElement;
                    if (RequestorElement != null) yield return RequestorElement;
                    if (Location != null) yield return Location;
                    foreach (var elem in PolicyElement) { if (elem != null) yield return elem; }
                    if (Media != null) yield return Media;
                    if (Network != null) yield return Network;
                    foreach (var elem in PurposeOfUse) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
                    if (Who != null) yield return new ElementValue("who", Who);
                    if (AltIdElement != null) yield return new ElementValue("altId", AltIdElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (RequestorElement != null) yield return new ElementValue("requestor", RequestorElement);
                    if (Location != null) yield return new ElementValue("location", Location);
                    foreach (var elem in PolicyElement) { if (elem != null) yield return new ElementValue("policy", elem); }
                    if (Media != null) yield return new ElementValue("media", Media);
                    if (Network != null) yield return new ElementValue("network", Network);
                    foreach (var elem in PurposeOfUse) { if (elem != null) yield return new ElementValue("purposeOfUse", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "NetworkComponent")]
        [DataContract]
        public partial class NetworkComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IAuditEventNetworkComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NetworkComponent"; } }
            
            /// <summary>
            /// Identifier for the network access point of the user device
            /// </summary>
            [FhirElement("address", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AddressElement
            {
                get { return _AddressElement; }
                set { _AddressElement = value; OnPropertyChanged("AddressElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _AddressElement;
            
            /// <summary>
            /// Identifier for the network access point of the user device
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Address
            {
                get { return AddressElement != null ? AddressElement.Value : null; }
                set
                {
                    if (value == null)
                        AddressElement = null;
                    else
                        AddressElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Address");
                }
            }
            
            /// <summary>
            /// The type of network access point
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.AuditEventAgentNetworkType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.AuditEventAgentNetworkType> _TypeElement;
            
            /// <summary>
            /// The type of network access point
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.AuditEventAgentNetworkType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.AuditEventAgentNetworkType>(value);
                    OnPropertyChanged("Type");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NetworkComponent");
                base.Serialize(sink);
                sink.Element("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AddressElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TypeElement?.Serialize(sink);
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
                    case "address":
                        AddressElement = source.PopulateValue(AddressElement);
                        return true;
                    case "_address":
                        AddressElement = source.Populate(AddressElement);
                        return true;
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NetworkComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AddressElement != null) dest.AddressElement = (Hl7.Fhir.Model.FhirString)AddressElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.AuditEventAgentNetworkType>)TypeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NetworkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NetworkComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AddressElement, otherT.AddressElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NetworkComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AddressElement, otherT.AddressElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AddressElement != null) yield return AddressElement;
                    if (TypeElement != null) yield return TypeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AddressElement != null) yield return new ElementValue("address", AddressElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SourceComponent")]
        [DataContract]
        public partial class SourceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IAuditEventSourceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SourceComponent"; } }
            
            /// <summary>
            /// Logical source location within the enterprise
            /// </summary>
            [FhirElement("site", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SiteElement
            {
                get { return _SiteElement; }
                set { _SiteElement = value; OnPropertyChanged("SiteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SiteElement;
            
            /// <summary>
            /// Logical source location within the enterprise
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Site
            {
                get { return SiteElement != null ? SiteElement.Value : null; }
                set
                {
                    if (value == null)
                        SiteElement = null;
                    else
                        SiteElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Site");
                }
            }
            
            /// <summary>
            /// The identity of source detecting the event
            /// </summary>
            [FhirElement("observer", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("PractitionerRole","Practitioner","Organization","Device","Patient","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Observer
            {
                get { return _Observer; }
                set { _Observer = value; OnPropertyChanged("Observer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Observer;
            
            /// <summary>
            /// The type of source where event originated
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.Coding>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Type;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SourceComponent");
                base.Serialize(sink);
                sink.Element("site", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SiteElement?.Serialize(sink);
                sink.Element("observer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Observer?.Serialize(sink);
                sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Type)
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
                    case "site":
                        SiteElement = source.PopulateValue(SiteElement);
                        return true;
                    case "_site":
                        SiteElement = source.Populate(SiteElement);
                        return true;
                    case "observer":
                        Observer = source.Populate(Observer);
                        return true;
                    case "type":
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SourceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SiteElement != null) dest.SiteElement = (Hl7.Fhir.Model.FhirString)SiteElement.DeepCopy();
                    if(Observer != null) dest.Observer = (Hl7.Fhir.Model.ResourceReference)Observer.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Coding>(Type.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SiteElement, otherT.SiteElement)) return false;
                if( !DeepComparable.Matches(Observer, otherT.Observer)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SiteElement, otherT.SiteElement)) return false;
                if( !DeepComparable.IsExactly(Observer, otherT.Observer)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SiteElement != null) yield return SiteElement;
                    if (Observer != null) yield return Observer;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SiteElement != null) yield return new ElementValue("site", SiteElement);
                    if (Observer != null) yield return new ElementValue("observer", Observer);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "EntityComponent")]
        [DataContract]
        public partial class EntityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EntityComponent"; } }
            
            /// <summary>
            /// Specific instance of resource
            /// </summary>
            [FhirElement("what", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference What
            {
                get { return _What; }
                set { _What = value; OnPropertyChanged("What"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _What;
            
            /// <summary>
            /// Type of entity involved
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// What role the entity played
            /// </summary>
            [FhirElement("role", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.Coding _Role;
            
            /// <summary>
            /// Life-cycle stage for the entity
            /// </summary>
            [FhirElement("lifecycle", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Lifecycle
            {
                get { return _Lifecycle; }
                set { _Lifecycle = value; OnPropertyChanged("Lifecycle"); }
            }
            
            private Hl7.Fhir.Model.Coding _Lifecycle;
            
            /// <summary>
            /// Security labels on the entity
            /// </summary>
            [FhirElement("securityLabel", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> SecurityLabel
            {
                get { if(_SecurityLabel==null) _SecurityLabel = new List<Hl7.Fhir.Model.Coding>(); return _SecurityLabel; }
                set { _SecurityLabel = value; OnPropertyChanged("SecurityLabel"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _SecurityLabel;
            
            /// <summary>
            /// Descriptor for entity
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Descriptor for entity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Descriptive text
            /// </summary>
            [FhirElement("description", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Descriptive text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null;
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Query parameters
            /// </summary>
            [FhirElement("query", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary QueryElement
            {
                get { return _QueryElement; }
                set { _QueryElement = value; OnPropertyChanged("QueryElement"); }
            }
            
            private Hl7.Fhir.Model.Base64Binary _QueryElement;
            
            /// <summary>
            /// Query parameters
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public byte[] Query
            {
                get { return QueryElement != null ? QueryElement.Value : null; }
                set
                {
                    if (value == null)
                        QueryElement = null;
                    else
                        QueryElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Query");
                }
            }
            
            /// <summary>
            /// Additional Information about the entity
            /// </summary>
            [FhirElement("detail", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<DetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<DetailComponent> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EntityComponent");
                base.Serialize(sink);
                sink.Element("what", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); What?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Role?.Serialize(sink);
                sink.Element("lifecycle", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Lifecycle?.Serialize(sink);
                sink.BeginList("securityLabel", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SecurityLabel)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("query", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); QueryElement?.Serialize(sink);
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Detail)
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
                    case "what":
                        What = source.Populate(What);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "lifecycle":
                        Lifecycle = source.Populate(Lifecycle);
                        return true;
                    case "securityLabel":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "query":
                        QueryElement = source.PopulateValue(QueryElement);
                        return true;
                    case "_query":
                        QueryElement = source.Populate(QueryElement);
                        return true;
                    case "detail":
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
                    case "securityLabel":
                        source.PopulateListItem(SecurityLabel, index);
                        return true;
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EntityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(What != null) dest.What = (Hl7.Fhir.Model.ResourceReference)What.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.Coding)Role.DeepCopy();
                    if(Lifecycle != null) dest.Lifecycle = (Hl7.Fhir.Model.Coding)Lifecycle.DeepCopy();
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(QueryElement != null) dest.QueryElement = (Hl7.Fhir.Model.Base64Binary)QueryElement.DeepCopy();
                    if(Detail != null) dest.Detail = new List<DetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EntityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EntityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(What, otherT.What)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Lifecycle, otherT.Lifecycle)) return false;
                if( !DeepComparable.Matches(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(QueryElement, otherT.QueryElement)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EntityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(What, otherT.What)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Lifecycle, otherT.Lifecycle)) return false;
                if( !DeepComparable.IsExactly(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(QueryElement, otherT.QueryElement)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (What != null) yield return What;
                    if (Type != null) yield return Type;
                    if (Role != null) yield return Role;
                    if (Lifecycle != null) yield return Lifecycle;
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return elem; }
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (QueryElement != null) yield return QueryElement;
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (What != null) yield return new ElementValue("what", What);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Lifecycle != null) yield return new ElementValue("lifecycle", Lifecycle);
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return new ElementValue("securityLabel", elem); }
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (QueryElement != null) yield return new ElementValue("query", QueryElement);
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IAuditEventDetailComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// Name of the property
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// Name of the property
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Property value
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Base64Binary))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DetailComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Value?.Serialize(sink);
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
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "_valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
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
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.IAuditEventSourceComponent Hl7.Fhir.Model.IAuditEvent.Source { get { return Source; } }
    
        
        /// <summary>
        /// Type/identifier of event
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.Coding _Type;
        
        /// <summary>
        /// More specific type/id for the event
        /// </summary>
        [FhirElement("subtype", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Subtype
        {
            get { if(_Subtype==null) _Subtype = new List<Hl7.Fhir.Model.Coding>(); return _Subtype; }
            set { _Subtype = value; OnPropertyChanged("Subtype"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Subtype;
        
        /// <summary>
        /// Type of action performed during the event
        /// </summary>
        [FhirElement("action", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AuditEventAction> ActionElement
        {
            get { return _ActionElement; }
            set { _ActionElement = value; OnPropertyChanged("ActionElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AuditEventAction> _ActionElement;
        
        /// <summary>
        /// Type of action performed during the event
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AuditEventAction? Action
        {
            get { return ActionElement != null ? ActionElement.Value : null; }
            set
            {
                if (value == null)
                    ActionElement = null;
                else
                    ActionElement = new Code<Hl7.Fhir.Model.AuditEventAction>(value);
                OnPropertyChanged("Action");
            }
        }
        
        /// <summary>
        /// When the activity occurred
        /// </summary>
        [FhirElement("period", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Time when the event was recorded
        /// </summary>
        [FhirElement("recorded", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant RecordedElement
        {
            get { return _RecordedElement; }
            set { _RecordedElement = value; OnPropertyChanged("RecordedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _RecordedElement;
        
        /// <summary>
        /// Time when the event was recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Recorded
        {
            get { return RecordedElement != null ? RecordedElement.Value : null; }
            set
            {
                if (value == null)
                    RecordedElement = null;
                else
                    RecordedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Recorded");
            }
        }
        
        /// <summary>
        /// Whether the event succeeded or failed
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AuditEventOutcome> OutcomeElement
        {
            get { return _OutcomeElement; }
            set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AuditEventOutcome> _OutcomeElement;
        
        /// <summary>
        /// Whether the event succeeded or failed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AuditEventOutcome? Outcome
        {
            get { return OutcomeElement != null ? OutcomeElement.Value : null; }
            set
            {
                if (value == null)
                    OutcomeElement = null;
                else
                    OutcomeElement = new Code<Hl7.Fhir.Model.AuditEventOutcome>(value);
                OnPropertyChanged("Outcome");
            }
        }
        
        /// <summary>
        /// Description of the event outcome
        /// </summary>
        [FhirElement("outcomeDesc", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OutcomeDescElement
        {
            get { return _OutcomeDescElement; }
            set { _OutcomeDescElement = value; OnPropertyChanged("OutcomeDescElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _OutcomeDescElement;
        
        /// <summary>
        /// Description of the event outcome
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OutcomeDesc
        {
            get { return OutcomeDescElement != null ? OutcomeDescElement.Value : null; }
            set
            {
                if (value == null)
                    OutcomeDescElement = null;
                else
                    OutcomeDescElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("OutcomeDesc");
            }
        }
        
        /// <summary>
        /// The purposeOfUse of the event
        /// </summary>
        [FhirElement("purposeOfEvent", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PurposeOfEvent
        {
            get { if(_PurposeOfEvent==null) _PurposeOfEvent = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PurposeOfEvent; }
            set { _PurposeOfEvent = value; OnPropertyChanged("PurposeOfEvent"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PurposeOfEvent;
        
        /// <summary>
        /// Actor involved in the event
        /// </summary>
        [FhirElement("agent", Order=170)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<AgentComponent> Agent
        {
            get { if(_Agent==null) _Agent = new List<AgentComponent>(); return _Agent; }
            set { _Agent = value; OnPropertyChanged("Agent"); }
        }
        
        private List<AgentComponent> _Agent;
        
        /// <summary>
        /// Audit Event Reporter
        /// </summary>
        [FhirElement("source", Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public SourceComponent Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private SourceComponent _Source;
        
        /// <summary>
        /// Data or objects used
        /// </summary>
        [FhirElement("entity", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EntityComponent> Entity
        {
            get { if(_Entity==null) _Entity = new List<EntityComponent>(); return _Entity; }
            set { _Entity = value; OnPropertyChanged("Entity"); }
        }
        
        private List<EntityComponent> _Entity;
    
    
        public static ElementDefinitionConstraint[] AuditEvent_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "sev-1",
                severity: ConstraintSeverity.Warning,
                expression: "entity.all(name.empty() or query.empty())",
                human: "Either a name or a query (NOT both)",
                xpath: "not(exists(f:name)) or not(exists(f:query))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(AuditEvent_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AuditEvent;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                if(Subtype != null) dest.Subtype = new List<Hl7.Fhir.Model.Coding>(Subtype.DeepCopy());
                if(ActionElement != null) dest.ActionElement = (Code<Hl7.Fhir.Model.AuditEventAction>)ActionElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(RecordedElement != null) dest.RecordedElement = (Hl7.Fhir.Model.Instant)RecordedElement.DeepCopy();
                if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.AuditEventOutcome>)OutcomeElement.DeepCopy();
                if(OutcomeDescElement != null) dest.OutcomeDescElement = (Hl7.Fhir.Model.FhirString)OutcomeDescElement.DeepCopy();
                if(PurposeOfEvent != null) dest.PurposeOfEvent = new List<Hl7.Fhir.Model.CodeableConcept>(PurposeOfEvent.DeepCopy());
                if(Agent != null) dest.Agent = new List<AgentComponent>(Agent.DeepCopy());
                if(Source != null) dest.Source = (SourceComponent)Source.DeepCopy();
                if(Entity != null) dest.Entity = new List<EntityComponent>(Entity.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new AuditEvent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AuditEvent;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.Matches(ActionElement, otherT.ActionElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(RecordedElement, otherT.RecordedElement)) return false;
            if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.Matches(OutcomeDescElement, otherT.OutcomeDescElement)) return false;
            if( !DeepComparable.Matches(PurposeOfEvent, otherT.PurposeOfEvent)) return false;
            if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AuditEvent;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.IsExactly(ActionElement, otherT.ActionElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(RecordedElement, otherT.RecordedElement)) return false;
            if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.IsExactly(OutcomeDescElement, otherT.OutcomeDescElement)) return false;
            if( !DeepComparable.IsExactly(PurposeOfEvent, otherT.PurposeOfEvent)) return false;
            if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("AuditEvent");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
            sink.BeginList("subtype", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Subtype)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActionElement?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
            sink.Element("recorded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RecordedElement?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OutcomeElement?.Serialize(sink);
            sink.Element("outcomeDesc", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OutcomeDescElement?.Serialize(sink);
            sink.BeginList("purposeOfEvent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PurposeOfEvent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
            foreach(var item in Agent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Source?.Serialize(sink);
            sink.BeginList("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Entity)
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
                    Type = source.Populate(Type);
                    return true;
                case "subtype":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "action":
                    ActionElement = source.PopulateValue(ActionElement);
                    return true;
                case "_action":
                    ActionElement = source.Populate(ActionElement);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "recorded":
                    RecordedElement = source.PopulateValue(RecordedElement);
                    return true;
                case "_recorded":
                    RecordedElement = source.Populate(RecordedElement);
                    return true;
                case "outcome":
                    OutcomeElement = source.PopulateValue(OutcomeElement);
                    return true;
                case "_outcome":
                    OutcomeElement = source.Populate(OutcomeElement);
                    return true;
                case "outcomeDesc":
                    OutcomeDescElement = source.PopulateValue(OutcomeDescElement);
                    return true;
                case "_outcomeDesc":
                    OutcomeDescElement = source.Populate(OutcomeDescElement);
                    return true;
                case "purposeOfEvent":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "agent":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "source":
                    Source = source.Populate(Source);
                    return true;
                case "entity":
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
                case "subtype":
                    source.PopulateListItem(Subtype, index);
                    return true;
                case "purposeOfEvent":
                    source.PopulateListItem(PurposeOfEvent, index);
                    return true;
                case "agent":
                    source.PopulateListItem(Agent, index);
                    return true;
                case "entity":
                    source.PopulateListItem(Entity, index);
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
                if (Type != null) yield return Type;
                foreach (var elem in Subtype) { if (elem != null) yield return elem; }
                if (ActionElement != null) yield return ActionElement;
                if (Period != null) yield return Period;
                if (RecordedElement != null) yield return RecordedElement;
                if (OutcomeElement != null) yield return OutcomeElement;
                if (OutcomeDescElement != null) yield return OutcomeDescElement;
                foreach (var elem in PurposeOfEvent) { if (elem != null) yield return elem; }
                foreach (var elem in Agent) { if (elem != null) yield return elem; }
                if (Source != null) yield return Source;
                foreach (var elem in Entity) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in Subtype) { if (elem != null) yield return new ElementValue("subtype", elem); }
                if (ActionElement != null) yield return new ElementValue("action", ActionElement);
                if (Period != null) yield return new ElementValue("period", Period);
                if (RecordedElement != null) yield return new ElementValue("recorded", RecordedElement);
                if (OutcomeElement != null) yield return new ElementValue("outcome", OutcomeElement);
                if (OutcomeDescElement != null) yield return new ElementValue("outcomeDesc", OutcomeDescElement);
                foreach (var elem in PurposeOfEvent) { if (elem != null) yield return new ElementValue("purposeOfEvent", elem); }
                foreach (var elem in Agent) { if (elem != null) yield return new ElementValue("agent", elem); }
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Entity) { if (elem != null) yield return new ElementValue("entity", elem); }
            }
        }
    
    }

}
