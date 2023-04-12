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
    /// Details of a Health Insurance product/plan provided by an organization
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "InsurancePlan", IsResource=true)]
    [DataContract]
    public partial class InsurancePlan : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.InsurancePlan; } }
        [NotMapped]
        public override string TypeName { get { return "InsurancePlan"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ContactComponent")]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ContactComponent"; } }
            
            /// <summary>
            /// The type of contact
            /// </summary>
            [FhirElement("purpose", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Purpose
            {
                get { return _Purpose; }
                set { _Purpose = value; OnPropertyChanged("Purpose"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Purpose;
            
            /// <summary>
            /// A name associated with the contact
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.R4.HumanName Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            
            private Hl7.Fhir.Model.R4.HumanName _Name;
            
            /// <summary>
            /// Contact details (telephone, email, etc.)  for a contact
            /// </summary>
            [FhirElement("telecom", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.R4.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.R4.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            
            private List<Hl7.Fhir.Model.R4.ContactPoint> _Telecom;
            
            /// <summary>
            /// Visiting or postal addresses for the contact
            /// </summary>
            [FhirElement("address", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Address Address
            {
                get { return _Address; }
                set { _Address = value; OnPropertyChanged("Address"); }
            }
            
            private Hl7.Fhir.Model.Address _Address;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContactComponent");
                base.Serialize(sink);
                sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Purpose?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Name?.Serialize(sink);
                sink.BeginList("telecom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Telecom)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Address?.Serialize(sink);
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
                    case "purpose":
                        Purpose = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "name":
                        Name = source.Get<Hl7.Fhir.Model.R4.HumanName>();
                        return true;
                    case "telecom":
                        Telecom = source.GetList<Hl7.Fhir.Model.R4.ContactPoint>();
                        return true;
                    case "address":
                        Address = source.Get<Hl7.Fhir.Model.Address>();
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
                    case "purpose":
                        Purpose = source.Populate(Purpose);
                        return true;
                    case "name":
                        Name = source.Populate(Name);
                        return true;
                    case "telecom":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "address":
                        Address = source.Populate(Address);
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
                    case "telecom":
                        source.PopulateListItem(Telecom, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.CodeableConcept)Purpose.DeepCopy();
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.R4.HumanName)Name.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.R4.ContactPoint>(Telecom.DeepCopy());
                    if(Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                if( !DeepComparable.Matches(Address, otherT.Address)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Purpose != null) yield return Purpose;
                    if (Name != null) yield return Name;
                    foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                    if (Address != null) yield return Address;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                    if (Name != null) yield return new ElementValue("name", Name);
                    foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                    if (Address != null) yield return new ElementValue("address", Address);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// Type of coverage
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
            /// What networks provide coverage
            /// </summary>
            [FhirElement("network", Order=50)]
            [CLSCompliant(false)]
            [References("Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Network
            {
                get { if(_Network==null) _Network = new List<Hl7.Fhir.Model.ResourceReference>(); return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Network;
            
            /// <summary>
            /// List of benefits
            /// </summary>
            [FhirElement("benefit", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<CoverageBenefitComponent> Benefit
            {
                get { if(_Benefit==null) _Benefit = new List<CoverageBenefitComponent>(); return _Benefit; }
                set { _Benefit = value; OnPropertyChanged("Benefit"); }
            }
            
            private List<CoverageBenefitComponent> _Benefit;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CoverageComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.BeginList("network", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Network)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("benefit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Benefit)
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "network":
                        Network = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "benefit":
                        Benefit = source.GetList<CoverageBenefitComponent>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "network":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "benefit":
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
                    case "network":
                        source.PopulateListItem(Network, index);
                        return true;
                    case "benefit":
                        source.PopulateListItem(Benefit, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Network != null) dest.Network = new List<Hl7.Fhir.Model.ResourceReference>(Network.DeepCopy());
                    if(Benefit != null) dest.Benefit = new List<CoverageBenefitComponent>(Benefit.DeepCopy());
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
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(Benefit, otherT.Benefit)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(Benefit, otherT.Benefit)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Network) { if (elem != null) yield return elem; }
                    foreach (var elem in Benefit) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Network) { if (elem != null) yield return new ElementValue("network", elem); }
                    foreach (var elem in Benefit) { if (elem != null) yield return new ElementValue("benefit", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CoverageBenefitComponent")]
        [DataContract]
        public partial class CoverageBenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageBenefitComponent"; } }
            
            /// <summary>
            /// Type of benefit
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
            /// Referral requirements
            /// </summary>
            [FhirElement("requirement", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementElement
            {
                get { return _RequirementElement; }
                set { _RequirementElement = value; OnPropertyChanged("RequirementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequirementElement;
            
            /// <summary>
            /// Referral requirements
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Requirement
            {
                get { return RequirementElement != null ? RequirementElement.Value : null; }
                set
                {
                    if (value == null)
                        RequirementElement = null;
                    else
                        RequirementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Requirement");
                }
            }
            
            /// <summary>
            /// Benefit limits
            /// </summary>
            [FhirElement("limit", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<LimitComponent> Limit
            {
                get { if(_Limit==null) _Limit = new List<LimitComponent>(); return _Limit; }
                set { _Limit = value; OnPropertyChanged("Limit"); }
            }
            
            private List<LimitComponent> _Limit;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CoverageBenefitComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("requirement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequirementElement?.Serialize(sink);
                sink.BeginList("limit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Limit)
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "requirement":
                        RequirementElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "limit":
                        Limit = source.GetList<LimitComponent>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "requirement":
                        RequirementElement = source.PopulateValue(RequirementElement);
                        return true;
                    case "_requirement":
                        RequirementElement = source.Populate(RequirementElement);
                        return true;
                    case "limit":
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
                    case "limit":
                        source.PopulateListItem(Limit, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageBenefitComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(RequirementElement != null) dest.RequirementElement = (Hl7.Fhir.Model.FhirString)RequirementElement.DeepCopy();
                    if(Limit != null) dest.Limit = new List<LimitComponent>(Limit.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CoverageBenefitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageBenefitComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.Matches(Limit, otherT.Limit)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageBenefitComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.IsExactly(Limit, otherT.Limit)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (RequirementElement != null) yield return RequirementElement;
                    foreach (var elem in Limit) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (RequirementElement != null) yield return new ElementValue("requirement", RequirementElement);
                    foreach (var elem in Limit) { if (elem != null) yield return new ElementValue("limit", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "LimitComponent")]
        [DataContract]
        public partial class LimitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "LimitComponent"; } }
            
            /// <summary>
            /// Maximum value allowed
            /// </summary>
            [FhirElement("value", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Quantity _Value;
            
            /// <summary>
            /// Benefit limit details
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("LimitComponent");
                base.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Value?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
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
                    case "value":
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "value":
                        Value = source.Populate(Value);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LimitComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Quantity)Value.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new LimitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LimitComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LimitComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                    if (Code != null) yield return Code;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (Code != null) yield return new ElementValue("code", Code);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PlanComponent")]
        [DataContract]
        public partial class PlanComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PlanComponent"; } }
            
            /// <summary>
            /// Business Identifier for Product
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
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
            /// Type of plan
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Where product applies
            /// </summary>
            [FhirElement("coverageArea", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [References("Location")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> CoverageArea
            {
                get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(); return _CoverageArea; }
                set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _CoverageArea;
            
            /// <summary>
            /// What networks provide coverage
            /// </summary>
            [FhirElement("network", Order=70)]
            [CLSCompliant(false)]
            [References("Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Network
            {
                get { if(_Network==null) _Network = new List<Hl7.Fhir.Model.ResourceReference>(); return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Network;
            
            /// <summary>
            /// Overall costs
            /// </summary>
            [FhirElement("generalCost", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<GeneralCostComponent> GeneralCost
            {
                get { if(_GeneralCost==null) _GeneralCost = new List<GeneralCostComponent>(); return _GeneralCost; }
                set { _GeneralCost = value; OnPropertyChanged("GeneralCost"); }
            }
            
            private List<GeneralCostComponent> _GeneralCost;
            
            /// <summary>
            /// Specific costs
            /// </summary>
            [FhirElement("specificCost", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SpecificCostComponent> SpecificCost
            {
                get { if(_SpecificCost==null) _SpecificCost = new List<SpecificCostComponent>(); return _SpecificCost; }
                set { _SpecificCost = value; OnPropertyChanged("SpecificCost"); }
            }
            
            private List<SpecificCostComponent> _SpecificCost;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PlanComponent");
                base.Serialize(sink);
                sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Identifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.BeginList("coverageArea", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in CoverageArea)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("network", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Network)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("generalCost", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in GeneralCost)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("specificCost", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SpecificCost)
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
                    case "identifier":
                        Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "coverageArea":
                        CoverageArea = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "network":
                        Network = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "generalCost":
                        GeneralCost = source.GetList<GeneralCostComponent>();
                        return true;
                    case "specificCost":
                        SpecificCost = source.GetList<SpecificCostComponent>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "coverageArea":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "network":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "generalCost":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "specificCost":
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
                    case "identifier":
                        source.PopulateListItem(Identifier, index);
                        return true;
                    case "coverageArea":
                        source.PopulateListItem(CoverageArea, index);
                        return true;
                    case "network":
                        source.PopulateListItem(Network, index);
                        return true;
                    case "generalCost":
                        source.PopulateListItem(GeneralCost, index);
                        return true;
                    case "specificCost":
                        source.PopulateListItem(SpecificCost, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PlanComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(CoverageArea.DeepCopy());
                    if(Network != null) dest.Network = new List<Hl7.Fhir.Model.ResourceReference>(Network.DeepCopy());
                    if(GeneralCost != null) dest.GeneralCost = new List<GeneralCostComponent>(GeneralCost.DeepCopy());
                    if(SpecificCost != null) dest.SpecificCost = new List<SpecificCostComponent>(SpecificCost.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PlanComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PlanComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(GeneralCost, otherT.GeneralCost)) return false;
                if( !DeepComparable.Matches(SpecificCost, otherT.SpecificCost)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PlanComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(GeneralCost, otherT.GeneralCost)) return false;
                if( !DeepComparable.IsExactly(SpecificCost, otherT.SpecificCost)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                    foreach (var elem in CoverageArea) { if (elem != null) yield return elem; }
                    foreach (var elem in Network) { if (elem != null) yield return elem; }
                    foreach (var elem in GeneralCost) { if (elem != null) yield return elem; }
                    foreach (var elem in SpecificCost) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in CoverageArea) { if (elem != null) yield return new ElementValue("coverageArea", elem); }
                    foreach (var elem in Network) { if (elem != null) yield return new ElementValue("network", elem); }
                    foreach (var elem in GeneralCost) { if (elem != null) yield return new ElementValue("generalCost", elem); }
                    foreach (var elem in SpecificCost) { if (elem != null) yield return new ElementValue("specificCost", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GeneralCostComponent")]
        [DataContract]
        public partial class GeneralCostComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GeneralCostComponent"; } }
            
            /// <summary>
            /// Type of cost
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
            /// Number of enrollees
            /// </summary>
            [FhirElement("groupSize", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt GroupSizeElement
            {
                get { return _GroupSizeElement; }
                set { _GroupSizeElement = value; OnPropertyChanged("GroupSizeElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _GroupSizeElement;
            
            /// <summary>
            /// Number of enrollees
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? GroupSize
            {
                get { return GroupSizeElement != null ? GroupSizeElement.Value : null; }
                set
                {
                    if (value == null)
                        GroupSizeElement = null;
                    else
                        GroupSizeElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("GroupSize");
                }
            }
            
            /// <summary>
            /// Cost value
            /// </summary>
            [FhirElement("cost", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Cost
            {
                get { return _Cost; }
                set { _Cost = value; OnPropertyChanged("Cost"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Cost;
            
            /// <summary>
            /// Additional cost information
            /// </summary>
            [FhirElement("comment", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Additional cost information
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GeneralCostComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("groupSize", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); GroupSizeElement?.Serialize(sink);
                sink.Element("cost", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Cost?.Serialize(sink);
                sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentElement?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "groupSize":
                        GroupSizeElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "cost":
                        Cost = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "comment":
                        CommentElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "groupSize":
                        GroupSizeElement = source.PopulateValue(GroupSizeElement);
                        return true;
                    case "_groupSize":
                        GroupSizeElement = source.Populate(GroupSizeElement);
                        return true;
                    case "cost":
                        Cost = source.Populate(Cost);
                        return true;
                    case "comment":
                        CommentElement = source.PopulateValue(CommentElement);
                        return true;
                    case "_comment":
                        CommentElement = source.Populate(CommentElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GeneralCostComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(GroupSizeElement != null) dest.GroupSizeElement = (Hl7.Fhir.Model.PositiveInt)GroupSizeElement.DeepCopy();
                    if(Cost != null) dest.Cost = (Hl7.Fhir.Model.R4.Money)Cost.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GeneralCostComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GeneralCostComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(GroupSizeElement, otherT.GroupSizeElement)) return false;
                if( !DeepComparable.Matches(Cost, otherT.Cost)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GeneralCostComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(GroupSizeElement, otherT.GroupSizeElement)) return false;
                if( !DeepComparable.IsExactly(Cost, otherT.Cost)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (GroupSizeElement != null) yield return GroupSizeElement;
                    if (Cost != null) yield return Cost;
                    if (CommentElement != null) yield return CommentElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (GroupSizeElement != null) yield return new ElementValue("groupSize", GroupSizeElement);
                    if (Cost != null) yield return new ElementValue("cost", Cost);
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SpecificCostComponent")]
        [DataContract]
        public partial class SpecificCostComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SpecificCostComponent"; } }
            
            /// <summary>
            /// General category of benefit
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Benefits list
            /// </summary>
            [FhirElement("benefit", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<PlanBenefitComponent> Benefit
            {
                get { if(_Benefit==null) _Benefit = new List<PlanBenefitComponent>(); return _Benefit; }
                set { _Benefit = value; OnPropertyChanged("Benefit"); }
            }
            
            private List<PlanBenefitComponent> _Benefit;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SpecificCostComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Category?.Serialize(sink);
                sink.BeginList("benefit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Benefit)
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "benefit":
                        Benefit = source.GetList<PlanBenefitComponent>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "benefit":
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
                    case "benefit":
                        source.PopulateListItem(Benefit, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecificCostComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Benefit != null) dest.Benefit = new List<PlanBenefitComponent>(Benefit.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SpecificCostComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecificCostComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Benefit, otherT.Benefit)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecificCostComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Benefit, otherT.Benefit)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    foreach (var elem in Benefit) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    foreach (var elem in Benefit) { if (elem != null) yield return new ElementValue("benefit", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PlanBenefitComponent")]
        [DataContract]
        public partial class PlanBenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PlanBenefitComponent"; } }
            
            /// <summary>
            /// Type of specific benefit
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
            /// List of the costs
            /// </summary>
            [FhirElement("cost", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<CostComponent> Cost
            {
                get { if(_Cost==null) _Cost = new List<CostComponent>(); return _Cost; }
                set { _Cost = value; OnPropertyChanged("Cost"); }
            }
            
            private List<CostComponent> _Cost;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PlanBenefitComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.BeginList("cost", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Cost)
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "cost":
                        Cost = source.GetList<CostComponent>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "cost":
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
                    case "cost":
                        source.PopulateListItem(Cost, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PlanBenefitComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Cost != null) dest.Cost = new List<CostComponent>(Cost.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PlanBenefitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PlanBenefitComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Cost, otherT.Cost)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PlanBenefitComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Cost, otherT.Cost)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Cost) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Cost) { if (elem != null) yield return new ElementValue("cost", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CostComponent")]
        [DataContract]
        public partial class CostComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CostComponent"; } }
            
            /// <summary>
            /// Type of cost
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
            /// in-network | out-of-network | other
            /// </summary>
            [FhirElement("applicability", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Applicability
            {
                get { return _Applicability; }
                set { _Applicability = value; OnPropertyChanged("Applicability"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Applicability;
            
            /// <summary>
            /// Additional information about the cost
            /// </summary>
            [FhirElement("qualifiers", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Qualifiers
            {
                get { if(_Qualifiers==null) _Qualifiers = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Qualifiers; }
                set { _Qualifiers = value; OnPropertyChanged("Qualifiers"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Qualifiers;
            
            /// <summary>
            /// The actual cost value
            /// </summary>
            [FhirElement("value", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Quantity _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CostComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("applicability", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Applicability?.Serialize(sink);
                sink.BeginList("qualifiers", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Qualifiers)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Value?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "applicability":
                        Applicability = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "qualifiers":
                        Qualifiers = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "value":
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "applicability":
                        Applicability = source.Populate(Applicability);
                        return true;
                    case "qualifiers":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "value":
                        Value = source.Populate(Value);
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
                    case "qualifiers":
                        source.PopulateListItem(Qualifiers, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CostComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Applicability != null) dest.Applicability = (Hl7.Fhir.Model.CodeableConcept)Applicability.DeepCopy();
                    if(Qualifiers != null) dest.Qualifiers = new List<Hl7.Fhir.Model.CodeableConcept>(Qualifiers.DeepCopy());
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Quantity)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CostComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CostComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Applicability, otherT.Applicability)) return false;
                if( !DeepComparable.Matches(Qualifiers, otherT.Qualifiers)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CostComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Applicability, otherT.Applicability)) return false;
                if( !DeepComparable.IsExactly(Qualifiers, otherT.Qualifiers)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Applicability != null) yield return Applicability;
                    foreach (var elem in Qualifiers) { if (elem != null) yield return elem; }
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Applicability != null) yield return new ElementValue("applicability", Applicability);
                    foreach (var elem in Qualifiers) { if (elem != null) yield return new ElementValue("qualifiers", elem); }
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Business Identifier for Product
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
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Kind of product
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// Official name
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Official name
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
        /// Alternate names
        /// </summary>
        [FhirElement("alias", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if(_AliasElement==null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _AliasElement;
        
        /// <summary>
        /// Alternate names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Alias
        {
            get { return AliasElement != null ? AliasElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    AliasElement = null;
                else
                    AliasElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Alias");
            }
        }
        
        /// <summary>
        /// When the product is available
        /// </summary>
        [FhirElement("period", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Plan issuer
        /// </summary>
        [FhirElement("ownedBy", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OwnedBy
        {
            get { return _OwnedBy; }
            set { _OwnedBy = value; OnPropertyChanged("OwnedBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OwnedBy;
        
        /// <summary>
        /// Product administrator
        /// </summary>
        [FhirElement("administeredBy", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference AdministeredBy
        {
            get { return _AdministeredBy; }
            set { _AdministeredBy = value; OnPropertyChanged("AdministeredBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _AdministeredBy;
        
        /// <summary>
        /// Where product applies
        /// </summary>
        [FhirElement("coverageArea", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CoverageArea
        {
            get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(); return _CoverageArea; }
            set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CoverageArea;
        
        /// <summary>
        /// Contact for the product
        /// </summary>
        [FhirElement("contact", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactComponent> _Contact;
        
        /// <summary>
        /// Technical endpoint
        /// </summary>
        [FhirElement("endpoint", Order=190)]
        [CLSCompliant(false)]
        [References("Endpoint")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Endpoint
        {
            get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
            set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
        
        /// <summary>
        /// What networks are Included
        /// </summary>
        [FhirElement("network", Order=200)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Network
        {
            get { if(_Network==null) _Network = new List<Hl7.Fhir.Model.ResourceReference>(); return _Network; }
            set { _Network = value; OnPropertyChanged("Network"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Network;
        
        /// <summary>
        /// Coverage details
        /// </summary>
        [FhirElement("coverage", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<CoverageComponent> _Coverage;
        
        /// <summary>
        /// Plan details
        /// </summary>
        [FhirElement("plan", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PlanComponent> Plan
        {
            get { if(_Plan==null) _Plan = new List<PlanComponent>(); return _Plan; }
            set { _Plan = value; OnPropertyChanged("Plan"); }
        }
        
        private List<PlanComponent> _Plan;
    
    
        public static ElementDefinitionConstraint[] InsurancePlan_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ipn-1",
                severity: ConstraintSeverity.Warning,
                expression: "(identifier.count() + name.count()) > 0",
                human: "The organization SHALL at least have a name or an idendtifier, and possibly more than one",
                xpath: "count(f:identifier | f:name) > 0"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(InsurancePlan_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as InsurancePlan;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(OwnedBy != null) dest.OwnedBy = (Hl7.Fhir.Model.ResourceReference)OwnedBy.DeepCopy();
                if(AdministeredBy != null) dest.AdministeredBy = (Hl7.Fhir.Model.ResourceReference)AdministeredBy.DeepCopy();
                if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(CoverageArea.DeepCopy());
                if(Contact != null) dest.Contact = new List<ContactComponent>(Contact.DeepCopy());
                if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
                if(Network != null) dest.Network = new List<Hl7.Fhir.Model.ResourceReference>(Network.DeepCopy());
                if(Coverage != null) dest.Coverage = new List<CoverageComponent>(Coverage.DeepCopy());
                if(Plan != null) dest.Plan = new List<PlanComponent>(Plan.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new InsurancePlan());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as InsurancePlan;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(OwnedBy, otherT.OwnedBy)) return false;
            if( !DeepComparable.Matches(AdministeredBy, otherT.AdministeredBy)) return false;
            if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.Matches(Network, otherT.Network)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Plan, otherT.Plan)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as InsurancePlan;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(OwnedBy, otherT.OwnedBy)) return false;
            if( !DeepComparable.IsExactly(AdministeredBy, otherT.AdministeredBy)) return false;
            if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Plan, otherT.Plan)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("InsurancePlan");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Type)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.BeginList("alias", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(AliasElement);
            sink.End();
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
            sink.Element("ownedBy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OwnedBy?.Serialize(sink);
            sink.Element("administeredBy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AdministeredBy?.Serialize(sink);
            sink.BeginList("coverageArea", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in CoverageArea)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Endpoint)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("network", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Network)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Coverage)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("plan", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Plan)
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
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.PublicationStatus>>();
                    return true;
                case "type":
                    Type = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "alias":
                    AliasElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "ownedBy":
                    OwnedBy = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "administeredBy":
                    AdministeredBy = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "coverageArea":
                    CoverageArea = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "contact":
                    Contact = source.GetList<ContactComponent>();
                    return true;
                case "endpoint":
                    Endpoint = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "network":
                    Network = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "coverage":
                    Coverage = source.GetList<CoverageComponent>();
                    return true;
                case "plan":
                    Plan = source.GetList<PlanComponent>();
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
                case "type":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "alias":
                case "_alias":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "ownedBy":
                    OwnedBy = source.Populate(OwnedBy);
                    return true;
                case "administeredBy":
                    AdministeredBy = source.Populate(AdministeredBy);
                    return true;
                case "coverageArea":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "endpoint":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "network":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "coverage":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "plan":
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "type":
                    source.PopulateListItem(Type, index);
                    return true;
                case "alias":
                    source.PopulatePrimitiveListItemValue(AliasElement, index);
                    return true;
                case "_alias":
                    source.PopulatePrimitiveListItem(AliasElement, index);
                    return true;
                case "coverageArea":
                    source.PopulateListItem(CoverageArea, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "endpoint":
                    source.PopulateListItem(Endpoint, index);
                    return true;
                case "network":
                    source.PopulateListItem(Network, index);
                    return true;
                case "coverage":
                    source.PopulateListItem(Coverage, index);
                    return true;
                case "plan":
                    source.PopulateListItem(Plan, index);
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
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (NameElement != null) yield return NameElement;
                foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
                if (Period != null) yield return Period;
                if (OwnedBy != null) yield return OwnedBy;
                if (AdministeredBy != null) yield return AdministeredBy;
                foreach (var elem in CoverageArea) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
                foreach (var elem in Network) { if (elem != null) yield return elem; }
                foreach (var elem in Coverage) { if (elem != null) yield return elem; }
                foreach (var elem in Plan) { if (elem != null) yield return elem; }
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
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
                if (OwnedBy != null) yield return new ElementValue("ownedBy", OwnedBy);
                if (AdministeredBy != null) yield return new ElementValue("administeredBy", AdministeredBy);
                foreach (var elem in CoverageArea) { if (elem != null) yield return new ElementValue("coverageArea", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                foreach (var elem in Network) { if (elem != null) yield return new ElementValue("network", elem); }
                foreach (var elem in Coverage) { if (elem != null) yield return new ElementValue("coverage", elem); }
                foreach (var elem in Plan) { if (elem != null) yield return new ElementValue("plan", elem); }
            }
        }
    
    }

}
