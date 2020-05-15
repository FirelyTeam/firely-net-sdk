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
    /// An instance of a medical-related component of a medical device
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "DeviceDefinition", IsResource=true)]
    [DataContract]
    public partial class DeviceDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "UdiDeviceIdentifierComponent")]
        [DataContract]
        public partial class UdiDeviceIdentifierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "UdiDeviceIdentifierComponent"; } }
            
            /// <summary>
            /// The identifier that is to be associated with every Device that references this DeviceDefintiion for the issuer and jurisdication porvided in the DeviceDefinition.udiDeviceIdentifier
            /// </summary>
            [FhirElement("deviceIdentifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DeviceIdentifierElement
            {
                get { return _DeviceIdentifierElement; }
                set { _DeviceIdentifierElement = value; OnPropertyChanged("DeviceIdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DeviceIdentifierElement;
            
            /// <summary>
            /// The identifier that is to be associated with every Device that references this DeviceDefintiion for the issuer and jurisdication porvided in the DeviceDefinition.udiDeviceIdentifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DeviceIdentifier
            {
                get { return DeviceIdentifierElement != null ? DeviceIdentifierElement.Value : null; }
                set
                {
                    if (value == null)
                        DeviceIdentifierElement = null;
                    else
                        DeviceIdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("DeviceIdentifier");
                }
            }
            
            /// <summary>
            /// The organization that assigns the identifier algorithm
            /// </summary>
            [FhirElement("issuer", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri IssuerElement
            {
                get { return _IssuerElement; }
                set { _IssuerElement = value; OnPropertyChanged("IssuerElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _IssuerElement;
            
            /// <summary>
            /// The organization that assigns the identifier algorithm
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Issuer
            {
                get { return IssuerElement != null ? IssuerElement.Value : null; }
                set
                {
                    if (value == null)
                        IssuerElement = null;
                    else
                        IssuerElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Issuer");
                }
            }
            
            /// <summary>
            /// The jurisdiction to which the deviceIdentifier applies
            /// </summary>
            [FhirElement("jurisdiction", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri JurisdictionElement
            {
                get { return _JurisdictionElement; }
                set { _JurisdictionElement = value; OnPropertyChanged("JurisdictionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _JurisdictionElement;
            
            /// <summary>
            /// The jurisdiction to which the deviceIdentifier applies
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Jurisdiction
            {
                get { return JurisdictionElement != null ? JurisdictionElement.Value : null; }
                set
                {
                    if (value == null)
                        JurisdictionElement = null;
                    else
                        JurisdictionElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Jurisdiction");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("UdiDeviceIdentifierComponent");
                base.Serialize(sink);
                sink.Element("deviceIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); DeviceIdentifierElement?.Serialize(sink);
                sink.Element("issuer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); IssuerElement?.Serialize(sink);
                sink.Element("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); JurisdictionElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as UdiDeviceIdentifierComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DeviceIdentifierElement != null) dest.DeviceIdentifierElement = (Hl7.Fhir.Model.FhirString)DeviceIdentifierElement.DeepCopy();
                    if(IssuerElement != null) dest.IssuerElement = (Hl7.Fhir.Model.FhirUri)IssuerElement.DeepCopy();
                    if(JurisdictionElement != null) dest.JurisdictionElement = (Hl7.Fhir.Model.FhirUri)JurisdictionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new UdiDeviceIdentifierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UdiDeviceIdentifierComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DeviceIdentifierElement, otherT.DeviceIdentifierElement)) return false;
                if( !DeepComparable.Matches(IssuerElement, otherT.IssuerElement)) return false;
                if( !DeepComparable.Matches(JurisdictionElement, otherT.JurisdictionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UdiDeviceIdentifierComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DeviceIdentifierElement, otherT.DeviceIdentifierElement)) return false;
                if( !DeepComparable.IsExactly(IssuerElement, otherT.IssuerElement)) return false;
                if( !DeepComparable.IsExactly(JurisdictionElement, otherT.JurisdictionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DeviceIdentifierElement != null) yield return DeviceIdentifierElement;
                    if (IssuerElement != null) yield return IssuerElement;
                    if (JurisdictionElement != null) yield return JurisdictionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DeviceIdentifierElement != null) yield return new ElementValue("deviceIdentifier", DeviceIdentifierElement);
                    if (IssuerElement != null) yield return new ElementValue("issuer", IssuerElement);
                    if (JurisdictionElement != null) yield return new ElementValue("jurisdiction", JurisdictionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DeviceNameComponent")]
        [DataContract]
        public partial class DeviceNameComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DeviceNameComponent"; } }
            
            /// <summary>
            /// The name of the device
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// The name of the device
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
            /// udi-label-name | user-friendly-name | patient-reported-name | manufacturer-name | model-name | other
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.DeviceNameType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.DeviceNameType> _TypeElement;
            
            /// <summary>
            /// udi-label-name | user-friendly-name | patient-reported-name | manufacturer-name | model-name | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.DeviceNameType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.DeviceNameType>(value);
                    OnPropertyChanged("Type");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DeviceNameComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DeviceNameComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.DeviceNameType>)TypeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DeviceNameComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DeviceNameComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DeviceNameComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (TypeElement != null) yield return TypeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SpecializationComponent")]
        [DataContract]
        public partial class SpecializationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SpecializationComponent"; } }
            
            /// <summary>
            /// The standard that is used to operate and communicate
            /// </summary>
            [FhirElement("systemType", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SystemTypeElement
            {
                get { return _SystemTypeElement; }
                set { _SystemTypeElement = value; OnPropertyChanged("SystemTypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SystemTypeElement;
            
            /// <summary>
            /// The standard that is used to operate and communicate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SystemType
            {
                get { return SystemTypeElement != null ? SystemTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        SystemTypeElement = null;
                    else
                        SystemTypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SystemType");
                }
            }
            
            /// <summary>
            /// The version of the standard that is used to operate and communicate
            /// </summary>
            [FhirElement("version", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// The version of the standard that is used to operate and communicate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                    if (value == null)
                        VersionElement = null;
                    else
                        VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SpecializationComponent");
                base.Serialize(sink);
                sink.Element("systemType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SystemTypeElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersionElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecializationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemTypeElement != null) dest.SystemTypeElement = (Hl7.Fhir.Model.FhirString)SystemTypeElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SpecializationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecializationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemTypeElement, otherT.SystemTypeElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecializationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemTypeElement, otherT.SystemTypeElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SystemTypeElement != null) yield return SystemTypeElement;
                    if (VersionElement != null) yield return VersionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SystemTypeElement != null) yield return new ElementValue("systemType", SystemTypeElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CapabilityComponent")]
        [DataContract]
        public partial class CapabilityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CapabilityComponent"; } }
            
            /// <summary>
            /// Type of capability
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
            /// Description of capability
            /// </summary>
            [FhirElement("description", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Description
            {
                get { if(_Description==null) _Description = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Description;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CapabilityComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.BeginList("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Description)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CapabilityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Description != null) dest.Description = new List<Hl7.Fhir.Model.CodeableConcept>(Description.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CapabilityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CapabilityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CapabilityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Description) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Description) { if (elem != null) yield return new ElementValue("description", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PropertyComponent")]
        [DataContract]
        public partial class PropertyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PropertyComponent"; } }
            
            /// <summary>
            /// Code that specifies the property DeviceDefinitionPropetyCode (Extensible)
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
            /// Property value as a quantity
            /// </summary>
            [FhirElement("valueQuantity", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Quantity> ValueQuantity
            {
                get { if(_ValueQuantity==null) _ValueQuantity = new List<Hl7.Fhir.Model.Quantity>(); return _ValueQuantity; }
                set { _ValueQuantity = value; OnPropertyChanged("ValueQuantity"); }
            }
            
            private List<Hl7.Fhir.Model.Quantity> _ValueQuantity;
            
            /// <summary>
            /// Property value as a code, e.g., NTP4 (synced to NTP)
            /// </summary>
            [FhirElement("valueCode", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ValueCode
            {
                get { if(_ValueCode==null) _ValueCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ValueCode; }
                set { _ValueCode = value; OnPropertyChanged("ValueCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ValueCode;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PropertyComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.BeginList("valueQuantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ValueQuantity)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("valueCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ValueCode)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PropertyComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(ValueQuantity != null) dest.ValueQuantity = new List<Hl7.Fhir.Model.Quantity>(ValueQuantity.DeepCopy());
                    if(ValueCode != null) dest.ValueCode = new List<Hl7.Fhir.Model.CodeableConcept>(ValueCode.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PropertyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PropertyComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ValueQuantity, otherT.ValueQuantity)) return false;
                if( !DeepComparable.Matches(ValueCode, otherT.ValueCode)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PropertyComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ValueQuantity, otherT.ValueQuantity)) return false;
                if( !DeepComparable.IsExactly(ValueCode, otherT.ValueCode)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in ValueQuantity) { if (elem != null) yield return elem; }
                    foreach (var elem in ValueCode) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in ValueQuantity) { if (elem != null) yield return new ElementValue("valueQuantity", elem); }
                    foreach (var elem in ValueCode) { if (elem != null) yield return new ElementValue("valueCode", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "MaterialComponent")]
        [DataContract]
        public partial class MaterialComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MaterialComponent"; } }
            
            /// <summary>
            /// The substance
            /// </summary>
            [FhirElement("substance", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Substance
            {
                get { return _Substance; }
                set { _Substance = value; OnPropertyChanged("Substance"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Substance;
            
            /// <summary>
            /// Indicates an alternative material of the device
            /// </summary>
            [FhirElement("alternate", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AlternateElement
            {
                get { return _AlternateElement; }
                set { _AlternateElement = value; OnPropertyChanged("AlternateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AlternateElement;
            
            /// <summary>
            /// Indicates an alternative material of the device
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Alternate
            {
                get { return AlternateElement != null ? AlternateElement.Value : null; }
                set
                {
                    if (value == null)
                        AlternateElement = null;
                    else
                        AlternateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Alternate");
                }
            }
            
            /// <summary>
            /// Whether the substance is a known or suspected allergen
            /// </summary>
            [FhirElement("allergenicIndicator", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllergenicIndicatorElement
            {
                get { return _AllergenicIndicatorElement; }
                set { _AllergenicIndicatorElement = value; OnPropertyChanged("AllergenicIndicatorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllergenicIndicatorElement;
            
            /// <summary>
            /// Whether the substance is a known or suspected allergen
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? AllergenicIndicator
            {
                get { return AllergenicIndicatorElement != null ? AllergenicIndicatorElement.Value : null; }
                set
                {
                    if (value == null)
                        AllergenicIndicatorElement = null;
                    else
                        AllergenicIndicatorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("AllergenicIndicator");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MaterialComponent");
                base.Serialize(sink);
                sink.Element("substance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Substance?.Serialize(sink);
                sink.Element("alternate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AlternateElement?.Serialize(sink);
                sink.Element("allergenicIndicator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AllergenicIndicatorElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MaterialComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Substance != null) dest.Substance = (Hl7.Fhir.Model.CodeableConcept)Substance.DeepCopy();
                    if(AlternateElement != null) dest.AlternateElement = (Hl7.Fhir.Model.FhirBoolean)AlternateElement.DeepCopy();
                    if(AllergenicIndicatorElement != null) dest.AllergenicIndicatorElement = (Hl7.Fhir.Model.FhirBoolean)AllergenicIndicatorElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MaterialComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MaterialComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
                if( !DeepComparable.Matches(AlternateElement, otherT.AlternateElement)) return false;
                if( !DeepComparable.Matches(AllergenicIndicatorElement, otherT.AllergenicIndicatorElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MaterialComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
                if( !DeepComparable.IsExactly(AlternateElement, otherT.AlternateElement)) return false;
                if( !DeepComparable.IsExactly(AllergenicIndicatorElement, otherT.AllergenicIndicatorElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Substance != null) yield return Substance;
                    if (AlternateElement != null) yield return AlternateElement;
                    if (AllergenicIndicatorElement != null) yield return AllergenicIndicatorElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Substance != null) yield return new ElementValue("substance", Substance);
                    if (AlternateElement != null) yield return new ElementValue("alternate", AlternateElement);
                    if (AllergenicIndicatorElement != null) yield return new ElementValue("allergenicIndicator", AllergenicIndicatorElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Instance identifier
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
        /// Unique Device Identifier (UDI) Barcode string
        /// </summary>
        [FhirElement("udiDeviceIdentifier", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UdiDeviceIdentifierComponent> UdiDeviceIdentifier
        {
            get { if(_UdiDeviceIdentifier==null) _UdiDeviceIdentifier = new List<UdiDeviceIdentifierComponent>(); return _UdiDeviceIdentifier; }
            set { _UdiDeviceIdentifier = value; OnPropertyChanged("UdiDeviceIdentifier"); }
        }
        
        private List<UdiDeviceIdentifierComponent> _UdiDeviceIdentifier;
        
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=110, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        
        private Hl7.Fhir.Model.Element _Manufacturer;
        
        /// <summary>
        /// A name given to the device to identify it
        /// </summary>
        [FhirElement("deviceName", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DeviceNameComponent> DeviceName
        {
            get { if(_DeviceName==null) _DeviceName = new List<DeviceNameComponent>(); return _DeviceName; }
            set { _DeviceName = value; OnPropertyChanged("DeviceName"); }
        }
        
        private List<DeviceNameComponent> _DeviceName;
        
        /// <summary>
        /// The model number for the device
        /// </summary>
        [FhirElement("modelNumber", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ModelNumberElement
        {
            get { return _ModelNumberElement; }
            set { _ModelNumberElement = value; OnPropertyChanged("ModelNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ModelNumberElement;
        
        /// <summary>
        /// The model number for the device
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ModelNumber
        {
            get { return ModelNumberElement != null ? ModelNumberElement.Value : null; }
            set
            {
                if (value == null)
                    ModelNumberElement = null;
                else
                    ModelNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ModelNumber");
            }
        }
        
        /// <summary>
        /// What kind of device or device system this is
        /// </summary>
        [FhirElement("type", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// The capabilities supported on a  device, the standards to which the device conforms for a particular purpose, and used for the communication
        /// </summary>
        [FhirElement("specialization", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SpecializationComponent> Specialization
        {
            get { if(_Specialization==null) _Specialization = new List<SpecializationComponent>(); return _Specialization; }
            set { _Specialization = value; OnPropertyChanged("Specialization"); }
        }
        
        private List<SpecializationComponent> _Specialization;
        
        /// <summary>
        /// Available versions
        /// </summary>
        [FhirElement("version", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> VersionElement
        {
            get { if(_VersionElement==null) _VersionElement = new List<Hl7.Fhir.Model.FhirString>(); return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _VersionElement;
        
        /// <summary>
        /// Available versions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Version
        {
            get { return VersionElement != null ? VersionElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    VersionElement = null;
                else
                    VersionElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Safety characteristics of the device
        /// </summary>
        [FhirElement("safety", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Safety
        {
            get { if(_Safety==null) _Safety = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Safety; }
            set { _Safety = value; OnPropertyChanged("Safety"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Safety;
        
        /// <summary>
        /// Shelf Life and storage information
        /// </summary>
        [FhirElement("shelfLifeStorage", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ProductShelfLife> ShelfLifeStorage
        {
            get { if(_ShelfLifeStorage==null) _ShelfLifeStorage = new List<Hl7.Fhir.Model.ProductShelfLife>(); return _ShelfLifeStorage; }
            set { _ShelfLifeStorage = value; OnPropertyChanged("ShelfLifeStorage"); }
        }
        
        private List<Hl7.Fhir.Model.ProductShelfLife> _ShelfLifeStorage;
        
        /// <summary>
        /// Dimensions, color etc.
        /// </summary>
        [FhirElement("physicalCharacteristics", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.ProdCharacteristic PhysicalCharacteristics
        {
            get { return _PhysicalCharacteristics; }
            set { _PhysicalCharacteristics = value; OnPropertyChanged("PhysicalCharacteristics"); }
        }
        
        private Hl7.Fhir.Model.ProdCharacteristic _PhysicalCharacteristics;
        
        /// <summary>
        /// Language code for the human-readable text strings produced by the device (all supported)
        /// </summary>
        [FhirElement("languageCode", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> LanguageCode
        {
            get { if(_LanguageCode==null) _LanguageCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _LanguageCode; }
            set { _LanguageCode = value; OnPropertyChanged("LanguageCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _LanguageCode;
        
        /// <summary>
        /// Device capabilities
        /// </summary>
        [FhirElement("capability", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CapabilityComponent> Capability
        {
            get { if(_Capability==null) _Capability = new List<CapabilityComponent>(); return _Capability; }
            set { _Capability = value; OnPropertyChanged("Capability"); }
        }
        
        private List<CapabilityComponent> _Capability;
        
        /// <summary>
        /// The actual configuration settings of a device as it actually operates, e.g., regulation status, time properties
        /// </summary>
        [FhirElement("property", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PropertyComponent> Property
        {
            get { if(_Property==null) _Property = new List<PropertyComponent>(); return _Property; }
            set { _Property = value; OnPropertyChanged("Property"); }
        }
        
        private List<PropertyComponent> _Property;
        
        /// <summary>
        /// Organization responsible for device
        /// </summary>
        [FhirElement("owner", Order=230)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Owner
        {
            get { return _Owner; }
            set { _Owner = value; OnPropertyChanged("Owner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Owner;
        
        /// <summary>
        /// Details for human/organization for support
        /// </summary>
        [FhirElement("contact", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactPoint> _Contact;
        
        /// <summary>
        /// Network address to contact device
        /// </summary>
        [FhirElement("url", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Network address to contact device
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                    UrlElement = null;
                else
                    UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Access to on-line information
        /// </summary>
        [FhirElement("onlineInformation", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri OnlineInformationElement
        {
            get { return _OnlineInformationElement; }
            set { _OnlineInformationElement = value; OnPropertyChanged("OnlineInformationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _OnlineInformationElement;
        
        /// <summary>
        /// Access to on-line information
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OnlineInformation
        {
            get { return OnlineInformationElement != null ? OnlineInformationElement.Value : null; }
            set
            {
                if (value == null)
                    OnlineInformationElement = null;
                else
                    OnlineInformationElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("OnlineInformation");
            }
        }
        
        /// <summary>
        /// Device notes and comments
        /// </summary>
        [FhirElement("note", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// The quantity of the device present in the packaging (e.g. the number of devices present in a pack, or the number of devices in the same package of the medicinal product)
        /// </summary>
        [FhirElement("quantity", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Quantity;
        
        /// <summary>
        /// The parent device it can be part of
        /// </summary>
        [FhirElement("parentDevice", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [References("DeviceDefinition")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ParentDevice
        {
            get { return _ParentDevice; }
            set { _ParentDevice = value; OnPropertyChanged("ParentDevice"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ParentDevice;
        
        /// <summary>
        /// A substance used to create the material(s) of which the device is made
        /// </summary>
        [FhirElement("material", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MaterialComponent> Material
        {
            get { if(_Material==null) _Material = new List<MaterialComponent>(); return _Material; }
            set { _Material = value; OnPropertyChanged("Material"); }
        }
        
        private List<MaterialComponent> _Material;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DeviceDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(UdiDeviceIdentifier != null) dest.UdiDeviceIdentifier = new List<UdiDeviceIdentifierComponent>(UdiDeviceIdentifier.DeepCopy());
                if(Manufacturer != null) dest.Manufacturer = (Hl7.Fhir.Model.Element)Manufacturer.DeepCopy();
                if(DeviceName != null) dest.DeviceName = new List<DeviceNameComponent>(DeviceName.DeepCopy());
                if(ModelNumberElement != null) dest.ModelNumberElement = (Hl7.Fhir.Model.FhirString)ModelNumberElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Specialization != null) dest.Specialization = new List<SpecializationComponent>(Specialization.DeepCopy());
                if(VersionElement != null) dest.VersionElement = new List<Hl7.Fhir.Model.FhirString>(VersionElement.DeepCopy());
                if(Safety != null) dest.Safety = new List<Hl7.Fhir.Model.CodeableConcept>(Safety.DeepCopy());
                if(ShelfLifeStorage != null) dest.ShelfLifeStorage = new List<Hl7.Fhir.Model.ProductShelfLife>(ShelfLifeStorage.DeepCopy());
                if(PhysicalCharacteristics != null) dest.PhysicalCharacteristics = (Hl7.Fhir.Model.ProdCharacteristic)PhysicalCharacteristics.DeepCopy();
                if(LanguageCode != null) dest.LanguageCode = new List<Hl7.Fhir.Model.CodeableConcept>(LanguageCode.DeepCopy());
                if(Capability != null) dest.Capability = new List<CapabilityComponent>(Capability.DeepCopy());
                if(Property != null) dest.Property = new List<PropertyComponent>(Property.DeepCopy());
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactPoint>(Contact.DeepCopy());
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(OnlineInformationElement != null) dest.OnlineInformationElement = (Hl7.Fhir.Model.FhirUri)OnlineInformationElement.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                if(ParentDevice != null) dest.ParentDevice = (Hl7.Fhir.Model.ResourceReference)ParentDevice.DeepCopy();
                if(Material != null) dest.Material = new List<MaterialComponent>(Material.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DeviceDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DeviceDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(UdiDeviceIdentifier, otherT.UdiDeviceIdentifier)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(DeviceName, otherT.DeviceName)) return false;
            if( !DeepComparable.Matches(ModelNumberElement, otherT.ModelNumberElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Specialization, otherT.Specialization)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(Safety, otherT.Safety)) return false;
            if( !DeepComparable.Matches(ShelfLifeStorage, otherT.ShelfLifeStorage)) return false;
            if( !DeepComparable.Matches(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
            if( !DeepComparable.Matches(LanguageCode, otherT.LanguageCode)) return false;
            if( !DeepComparable.Matches(Capability, otherT.Capability)) return false;
            if( !DeepComparable.Matches(Property, otherT.Property)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(OnlineInformationElement, otherT.OnlineInformationElement)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(ParentDevice, otherT.ParentDevice)) return false;
            if( !DeepComparable.Matches(Material, otherT.Material)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DeviceDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(UdiDeviceIdentifier, otherT.UdiDeviceIdentifier)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(DeviceName, otherT.DeviceName)) return false;
            if( !DeepComparable.IsExactly(ModelNumberElement, otherT.ModelNumberElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Specialization, otherT.Specialization)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(Safety, otherT.Safety)) return false;
            if( !DeepComparable.IsExactly(ShelfLifeStorage, otherT.ShelfLifeStorage)) return false;
            if( !DeepComparable.IsExactly(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
            if( !DeepComparable.IsExactly(LanguageCode, otherT.LanguageCode)) return false;
            if( !DeepComparable.IsExactly(Capability, otherT.Capability)) return false;
            if( !DeepComparable.IsExactly(Property, otherT.Property)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(OnlineInformationElement, otherT.OnlineInformationElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(ParentDevice, otherT.ParentDevice)) return false;
            if( !DeepComparable.IsExactly(Material, otherT.Material)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("DeviceDefinition");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("udiDeviceIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in UdiDeviceIdentifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Manufacturer?.Serialize(sink);
            sink.BeginList("deviceName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DeviceName)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("modelNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ModelNumberElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
            sink.BeginList("specialization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Specialization)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(VersionElement);
            sink.End();
            sink.BeginList("safety", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Safety)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("shelfLifeStorage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ShelfLifeStorage)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("physicalCharacteristics", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PhysicalCharacteristics?.Serialize(sink);
            sink.BeginList("languageCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in LanguageCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("capability", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Capability)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("property", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Property)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("owner", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Owner?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UrlElement?.Serialize(sink);
            sink.Element("onlineInformation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OnlineInformationElement?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
            sink.Element("parentDevice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ParentDevice?.Serialize(sink);
            sink.BeginList("material", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Material)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                foreach (var elem in UdiDeviceIdentifier) { if (elem != null) yield return elem; }
                if (Manufacturer != null) yield return Manufacturer;
                foreach (var elem in DeviceName) { if (elem != null) yield return elem; }
                if (ModelNumberElement != null) yield return ModelNumberElement;
                if (Type != null) yield return Type;
                foreach (var elem in Specialization) { if (elem != null) yield return elem; }
                foreach (var elem in VersionElement) { if (elem != null) yield return elem; }
                foreach (var elem in Safety) { if (elem != null) yield return elem; }
                foreach (var elem in ShelfLifeStorage) { if (elem != null) yield return elem; }
                if (PhysicalCharacteristics != null) yield return PhysicalCharacteristics;
                foreach (var elem in LanguageCode) { if (elem != null) yield return elem; }
                foreach (var elem in Capability) { if (elem != null) yield return elem; }
                foreach (var elem in Property) { if (elem != null) yield return elem; }
                if (Owner != null) yield return Owner;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (UrlElement != null) yield return UrlElement;
                if (OnlineInformationElement != null) yield return OnlineInformationElement;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                if (Quantity != null) yield return Quantity;
                if (ParentDevice != null) yield return ParentDevice;
                foreach (var elem in Material) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in UdiDeviceIdentifier) { if (elem != null) yield return new ElementValue("udiDeviceIdentifier", elem); }
                if (Manufacturer != null) yield return new ElementValue("manufacturer", Manufacturer);
                foreach (var elem in DeviceName) { if (elem != null) yield return new ElementValue("deviceName", elem); }
                if (ModelNumberElement != null) yield return new ElementValue("modelNumber", ModelNumberElement);
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in Specialization) { if (elem != null) yield return new ElementValue("specialization", elem); }
                foreach (var elem in VersionElement) { if (elem != null) yield return new ElementValue("version", elem); }
                foreach (var elem in Safety) { if (elem != null) yield return new ElementValue("safety", elem); }
                foreach (var elem in ShelfLifeStorage) { if (elem != null) yield return new ElementValue("shelfLifeStorage", elem); }
                if (PhysicalCharacteristics != null) yield return new ElementValue("physicalCharacteristics", PhysicalCharacteristics);
                foreach (var elem in LanguageCode) { if (elem != null) yield return new ElementValue("languageCode", elem); }
                foreach (var elem in Capability) { if (elem != null) yield return new ElementValue("capability", elem); }
                foreach (var elem in Property) { if (elem != null) yield return new ElementValue("property", elem); }
                if (Owner != null) yield return new ElementValue("owner", Owner);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (OnlineInformationElement != null) yield return new ElementValue("onlineInformation", OnlineInformationElement);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                if (ParentDevice != null) yield return new ElementValue("parentDevice", ParentDevice);
                foreach (var elem in Material) { if (elem != null) yield return new ElementValue("material", elem); }
            }
        }
    
    }

}
