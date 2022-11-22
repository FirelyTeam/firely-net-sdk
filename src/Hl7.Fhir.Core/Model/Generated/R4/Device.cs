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
    /// Item used in healthcare
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Device", IsResource=true)]
    [DataContract]
    public partial class Device : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IDevice, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Device; } }
        [NotMapped]
        public override string TypeName { get { return "Device"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "UdiCarrierComponent")]
        [DataContract]
        public partial class UdiCarrierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "UdiCarrierComponent"; } }
            
            /// <summary>
            /// Mandatory fixed portion of UDI
            /// </summary>
            [FhirElement("deviceIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DeviceIdentifierElement
            {
                get { return _DeviceIdentifierElement; }
                set { _DeviceIdentifierElement = value; OnPropertyChanged("DeviceIdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DeviceIdentifierElement;
            
            /// <summary>
            /// Mandatory fixed portion of UDI
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
            /// UDI Issuing Organization
            /// </summary>
            [FhirElement("issuer", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri IssuerElement
            {
                get { return _IssuerElement; }
                set { _IssuerElement = value; OnPropertyChanged("IssuerElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _IssuerElement;
            
            /// <summary>
            /// UDI Issuing Organization
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
            /// Regional UDI authority
            /// </summary>
            [FhirElement("jurisdiction", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri JurisdictionElement
            {
                get { return _JurisdictionElement; }
                set { _JurisdictionElement = value; OnPropertyChanged("JurisdictionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _JurisdictionElement;
            
            /// <summary>
            /// Regional UDI authority
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
            
            /// <summary>
            /// UDI Machine Readable Barcode String
            /// </summary>
            [FhirElement("carrierAIDC", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary CarrierAIDCElement
            {
                get { return _CarrierAIDCElement; }
                set { _CarrierAIDCElement = value; OnPropertyChanged("CarrierAIDCElement"); }
            }
            
            private Hl7.Fhir.Model.Base64Binary _CarrierAIDCElement;
            
            /// <summary>
            /// UDI Machine Readable Barcode String
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public byte[] CarrierAIDC
            {
                get { return CarrierAIDCElement != null ? CarrierAIDCElement.Value : null; }
                set
                {
                    if (value == null)
                        CarrierAIDCElement = null;
                    else
                        CarrierAIDCElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("CarrierAIDC");
                }
            }
            
            /// <summary>
            /// UDI Human Readable Barcode String
            /// </summary>
            [FhirElement("carrierHRF", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CarrierHRFElement
            {
                get { return _CarrierHRFElement; }
                set { _CarrierHRFElement = value; OnPropertyChanged("CarrierHRFElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CarrierHRFElement;
            
            /// <summary>
            /// UDI Human Readable Barcode String
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CarrierHRF
            {
                get { return CarrierHRFElement != null ? CarrierHRFElement.Value : null; }
                set
                {
                    if (value == null)
                        CarrierHRFElement = null;
                    else
                        CarrierHRFElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CarrierHRF");
                }
            }
            
            /// <summary>
            /// barcode | rfid | manual +
            /// </summary>
            [FhirElement("entryType", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.UDIEntryType> EntryTypeElement
            {
                get { return _EntryTypeElement; }
                set { _EntryTypeElement = value; OnPropertyChanged("EntryTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.UDIEntryType> _EntryTypeElement;
            
            /// <summary>
            /// barcode | rfid | manual +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.UDIEntryType? EntryType
            {
                get { return EntryTypeElement != null ? EntryTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        EntryTypeElement = null;
                    else
                        EntryTypeElement = new Code<Hl7.Fhir.Model.UDIEntryType>(value);
                    OnPropertyChanged("EntryType");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("UdiCarrierComponent");
                base.Serialize(sink);
                sink.Element("deviceIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DeviceIdentifierElement?.Serialize(sink);
                sink.Element("issuer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); IssuerElement?.Serialize(sink);
                sink.Element("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); JurisdictionElement?.Serialize(sink);
                sink.Element("carrierAIDC", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CarrierAIDCElement?.Serialize(sink);
                sink.Element("carrierHRF", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CarrierHRFElement?.Serialize(sink);
                sink.Element("entryType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EntryTypeElement?.Serialize(sink);
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
                    case "deviceIdentifier":
                        DeviceIdentifierElement = source.PopulateValue(DeviceIdentifierElement);
                        return true;
                    case "_deviceIdentifier":
                        DeviceIdentifierElement = source.Populate(DeviceIdentifierElement);
                        return true;
                    case "issuer":
                        IssuerElement = source.PopulateValue(IssuerElement);
                        return true;
                    case "_issuer":
                        IssuerElement = source.Populate(IssuerElement);
                        return true;
                    case "jurisdiction":
                        JurisdictionElement = source.PopulateValue(JurisdictionElement);
                        return true;
                    case "_jurisdiction":
                        JurisdictionElement = source.Populate(JurisdictionElement);
                        return true;
                    case "carrierAIDC":
                        CarrierAIDCElement = source.PopulateValue(CarrierAIDCElement);
                        return true;
                    case "_carrierAIDC":
                        CarrierAIDCElement = source.Populate(CarrierAIDCElement);
                        return true;
                    case "carrierHRF":
                        CarrierHRFElement = source.PopulateValue(CarrierHRFElement);
                        return true;
                    case "_carrierHRF":
                        CarrierHRFElement = source.Populate(CarrierHRFElement);
                        return true;
                    case "entryType":
                        EntryTypeElement = source.PopulateValue(EntryTypeElement);
                        return true;
                    case "_entryType":
                        EntryTypeElement = source.Populate(EntryTypeElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as UdiCarrierComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DeviceIdentifierElement != null) dest.DeviceIdentifierElement = (Hl7.Fhir.Model.FhirString)DeviceIdentifierElement.DeepCopy();
                    if(IssuerElement != null) dest.IssuerElement = (Hl7.Fhir.Model.FhirUri)IssuerElement.DeepCopy();
                    if(JurisdictionElement != null) dest.JurisdictionElement = (Hl7.Fhir.Model.FhirUri)JurisdictionElement.DeepCopy();
                    if(CarrierAIDCElement != null) dest.CarrierAIDCElement = (Hl7.Fhir.Model.Base64Binary)CarrierAIDCElement.DeepCopy();
                    if(CarrierHRFElement != null) dest.CarrierHRFElement = (Hl7.Fhir.Model.FhirString)CarrierHRFElement.DeepCopy();
                    if(EntryTypeElement != null) dest.EntryTypeElement = (Code<Hl7.Fhir.Model.UDIEntryType>)EntryTypeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new UdiCarrierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UdiCarrierComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DeviceIdentifierElement, otherT.DeviceIdentifierElement)) return false;
                if( !DeepComparable.Matches(IssuerElement, otherT.IssuerElement)) return false;
                if( !DeepComparable.Matches(JurisdictionElement, otherT.JurisdictionElement)) return false;
                if( !DeepComparable.Matches(CarrierAIDCElement, otherT.CarrierAIDCElement)) return false;
                if( !DeepComparable.Matches(CarrierHRFElement, otherT.CarrierHRFElement)) return false;
                if( !DeepComparable.Matches(EntryTypeElement, otherT.EntryTypeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UdiCarrierComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DeviceIdentifierElement, otherT.DeviceIdentifierElement)) return false;
                if( !DeepComparable.IsExactly(IssuerElement, otherT.IssuerElement)) return false;
                if( !DeepComparable.IsExactly(JurisdictionElement, otherT.JurisdictionElement)) return false;
                if( !DeepComparable.IsExactly(CarrierAIDCElement, otherT.CarrierAIDCElement)) return false;
                if( !DeepComparable.IsExactly(CarrierHRFElement, otherT.CarrierHRFElement)) return false;
                if( !DeepComparable.IsExactly(EntryTypeElement, otherT.EntryTypeElement)) return false;
            
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
                    if (CarrierAIDCElement != null) yield return CarrierAIDCElement;
                    if (CarrierHRFElement != null) yield return CarrierHRFElement;
                    if (EntryTypeElement != null) yield return EntryTypeElement;
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
                    if (CarrierAIDCElement != null) yield return new ElementValue("carrierAIDC", CarrierAIDCElement);
                    if (CarrierHRFElement != null) yield return new ElementValue("carrierHRF", CarrierHRFElement);
                    if (EntryTypeElement != null) yield return new ElementValue("entryType", EntryTypeElement);
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
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
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
            public Hl7.Fhir.Model.CodeableConcept SystemType
            {
                get { return _SystemType; }
                set { _SystemType = value; OnPropertyChanged("SystemType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SystemType;
            
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
                sink.Element("systemType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SystemType?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersionElement?.Serialize(sink);
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
                    case "systemType":
                        SystemType = source.Populate(SystemType);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecializationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemType != null) dest.SystemType = (Hl7.Fhir.Model.CodeableConcept)SystemType.DeepCopy();
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
                if( !DeepComparable.Matches(SystemType, otherT.SystemType)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecializationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemType, otherT.SystemType)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SystemType != null) yield return SystemType;
                    if (VersionElement != null) yield return VersionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SystemType != null) yield return new ElementValue("systemType", SystemType);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "VersionComponent")]
        [DataContract]
        public partial class VersionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "VersionComponent"; } }
            
            /// <summary>
            /// The type of the device version
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
            /// A single component of the device version
            /// </summary>
            [FhirElement("component", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Component
            {
                get { return _Component; }
                set { _Component = value; OnPropertyChanged("Component"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Component;
            
            /// <summary>
            /// The version text
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The version text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("VersionComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("component", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Component?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
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
                    case "component":
                        Component = source.Populate(Component);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VersionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Component != null) dest.Component = (Hl7.Fhir.Model.Identifier)Component.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new VersionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VersionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Component, otherT.Component)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VersionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Component != null) yield return Component;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Component != null) yield return new ElementValue("component", Component);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
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
                    case "valueQuantity":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "valueCode":
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
                    case "valueQuantity":
                        source.PopulateListItem(ValueQuantity, index);
                        return true;
                    case "valueCode":
                        source.PopulateListItem(ValueCode, index);
                        return true;
                }
                return false;
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
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IDevice.Contact { get { return Contact; } }
    
        
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
        /// The reference to the definition for the device
        /// </summary>
        [FhirElement("definition", Order=100)]
        [CLSCompliant(false)]
        [References("DeviceDefinition")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Definition
        {
            get { return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Definition;
        
        /// <summary>
        /// Unique Device Identifier (UDI) Barcode string
        /// </summary>
        [FhirElement("udiCarrier", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UdiCarrierComponent> UdiCarrier
        {
            get { if(_UdiCarrier==null) _UdiCarrier = new List<UdiCarrierComponent>(); return _UdiCarrier; }
            set { _UdiCarrier = value; OnPropertyChanged("UdiCarrier"); }
        }
        
        private List<UdiCarrierComponent> _UdiCarrier;
        
        /// <summary>
        /// active | inactive | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FHIRDeviceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FHIRDeviceStatus> _StatusElement;
        
        /// <summary>
        /// active | inactive | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FHIRDeviceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.FHIRDeviceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// online | paused | standby | offline | not-ready | transduc-discon | hw-discon | off
        /// </summary>
        [FhirElement("statusReason", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> StatusReason
        {
            get { if(_StatusReason==null) _StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _StatusReason;
        
        /// <summary>
        /// The distinct identification string
        /// </summary>
        [FhirElement("distinctIdentifier", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DistinctIdentifierElement
        {
            get { return _DistinctIdentifierElement; }
            set { _DistinctIdentifierElement = value; OnPropertyChanged("DistinctIdentifierElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DistinctIdentifierElement;
        
        /// <summary>
        /// The distinct identification string
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DistinctIdentifier
        {
            get { return DistinctIdentifierElement != null ? DistinctIdentifierElement.Value : null; }
            set
            {
                if (value == null)
                    DistinctIdentifierElement = null;
                else
                    DistinctIdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("DistinctIdentifier");
            }
        }
        
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ManufacturerElement
        {
            get { return _ManufacturerElement; }
            set { _ManufacturerElement = value; OnPropertyChanged("ManufacturerElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ManufacturerElement;
        
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Manufacturer
        {
            get { return ManufacturerElement != null ? ManufacturerElement.Value : null; }
            set
            {
                if (value == null)
                    ManufacturerElement = null;
                else
                    ManufacturerElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Manufacturer");
            }
        }
        
        /// <summary>
        /// Date when the device was made
        /// </summary>
        [FhirElement("manufactureDate", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ManufactureDateElement
        {
            get { return _ManufactureDateElement; }
            set { _ManufactureDateElement = value; OnPropertyChanged("ManufactureDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ManufactureDateElement;
        
        /// <summary>
        /// Date when the device was made
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ManufactureDate
        {
            get { return ManufactureDateElement != null ? ManufactureDateElement.Value : null; }
            set
            {
                if (value == null)
                    ManufactureDateElement = null;
                else
                    ManufactureDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ManufactureDate");
            }
        }
        
        /// <summary>
        /// Date and time of expiry of this device (if applicable)
        /// </summary>
        [FhirElement("expirationDate", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ExpirationDateElement
        {
            get { return _ExpirationDateElement; }
            set { _ExpirationDateElement = value; OnPropertyChanged("ExpirationDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ExpirationDateElement;
        
        /// <summary>
        /// Date and time of expiry of this device (if applicable)
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
                    ExpirationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ExpirationDate");
            }
        }
        
        /// <summary>
        /// Lot number of manufacture
        /// </summary>
        [FhirElement("lotNumber", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LotNumberElement
        {
            get { return _LotNumberElement; }
            set { _LotNumberElement = value; OnPropertyChanged("LotNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _LotNumberElement;
        
        /// <summary>
        /// Lot number of manufacture
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
        /// Serial number assigned by the manufacturer
        /// </summary>
        [FhirElement("serialNumber", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SerialNumberElement
        {
            get { return _SerialNumberElement; }
            set { _SerialNumberElement = value; OnPropertyChanged("SerialNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SerialNumberElement;
        
        /// <summary>
        /// Serial number assigned by the manufacturer
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string SerialNumber
        {
            get { return SerialNumberElement != null ? SerialNumberElement.Value : null; }
            set
            {
                if (value == null)
                    SerialNumberElement = null;
                else
                    SerialNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("SerialNumber");
            }
        }
        
        /// <summary>
        /// The name of the device as given by the manufacturer
        /// </summary>
        [FhirElement("deviceName", Order=200)]
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
        [FhirElement("modelNumber", Order=210)]
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
        /// The part number of the device
        /// </summary>
        [FhirElement("partNumber", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PartNumberElement
        {
            get { return _PartNumberElement; }
            set { _PartNumberElement = value; OnPropertyChanged("PartNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PartNumberElement;
        
        /// <summary>
        /// The part number of the device
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PartNumber
        {
            get { return PartNumberElement != null ? PartNumberElement.Value : null; }
            set
            {
                if (value == null)
                    PartNumberElement = null;
                else
                    PartNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PartNumber");
            }
        }
        
        /// <summary>
        /// The kind or type of device
        /// </summary>
        [FhirElement("type", Order=230)]
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
        [FhirElement("specialization", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SpecializationComponent> Specialization
        {
            get { if(_Specialization==null) _Specialization = new List<SpecializationComponent>(); return _Specialization; }
            set { _Specialization = value; OnPropertyChanged("Specialization"); }
        }
        
        private List<SpecializationComponent> _Specialization;
        
        /// <summary>
        /// The actual design of the device or software version running on the device
        /// </summary>
        [FhirElement("version", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<VersionComponent> Version
        {
            get { if(_Version==null) _Version = new List<VersionComponent>(); return _Version; }
            set { _Version = value; OnPropertyChanged("Version"); }
        }
        
        private List<VersionComponent> _Version;
        
        /// <summary>
        /// The actual configuration settings of a device as it actually operates, e.g., regulation status, time properties
        /// </summary>
        [FhirElement("property", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PropertyComponent> Property
        {
            get { if(_Property==null) _Property = new List<PropertyComponent>(); return _Property; }
            set { _Property = value; OnPropertyChanged("Property"); }
        }
        
        private List<PropertyComponent> _Property;
        
        /// <summary>
        /// Patient to whom Device is affixed
        /// </summary>
        [FhirElement("patient", Order=270)]
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
        /// Organization responsible for device
        /// </summary>
        [FhirElement("owner", Order=280)]
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
        [FhirElement("contact", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactPoint> _Contact;
        
        /// <summary>
        /// Where the device is found
        /// </summary>
        [FhirElement("location", Order=300)]
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
        /// Network address to contact device
        /// </summary>
        [FhirElement("url", Order=310)]
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
        /// Device notes and comments
        /// </summary>
        [FhirElement("note", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Safety Characteristics of Device
        /// </summary>
        [FhirElement("safety", InSummary=Hl7.Fhir.Model.Version.All, Order=330)]
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
        /// The parent device
        /// </summary>
        [FhirElement("parent", Order=340)]
        [CLSCompliant(false)]
        [References("Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Parent
        {
            get { return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Parent;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Device;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
                if(UdiCarrier != null) dest.UdiCarrier = new List<UdiCarrierComponent>(UdiCarrier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FHIRDeviceStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(StatusReason.DeepCopy());
                if(DistinctIdentifierElement != null) dest.DistinctIdentifierElement = (Hl7.Fhir.Model.FhirString)DistinctIdentifierElement.DeepCopy();
                if(ManufacturerElement != null) dest.ManufacturerElement = (Hl7.Fhir.Model.FhirString)ManufacturerElement.DeepCopy();
                if(ManufactureDateElement != null) dest.ManufactureDateElement = (Hl7.Fhir.Model.FhirDateTime)ManufactureDateElement.DeepCopy();
                if(ExpirationDateElement != null) dest.ExpirationDateElement = (Hl7.Fhir.Model.FhirDateTime)ExpirationDateElement.DeepCopy();
                if(LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                if(SerialNumberElement != null) dest.SerialNumberElement = (Hl7.Fhir.Model.FhirString)SerialNumberElement.DeepCopy();
                if(DeviceName != null) dest.DeviceName = new List<DeviceNameComponent>(DeviceName.DeepCopy());
                if(ModelNumberElement != null) dest.ModelNumberElement = (Hl7.Fhir.Model.FhirString)ModelNumberElement.DeepCopy();
                if(PartNumberElement != null) dest.PartNumberElement = (Hl7.Fhir.Model.FhirString)PartNumberElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Specialization != null) dest.Specialization = new List<SpecializationComponent>(Specialization.DeepCopy());
                if(Version != null) dest.Version = new List<VersionComponent>(Version.DeepCopy());
                if(Property != null) dest.Property = new List<PropertyComponent>(Property.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactPoint>(Contact.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Safety != null) dest.Safety = new List<Hl7.Fhir.Model.CodeableConcept>(Safety.DeepCopy());
                if(Parent != null) dest.Parent = (Hl7.Fhir.Model.ResourceReference)Parent.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Device());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Device;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(UdiCarrier, otherT.UdiCarrier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(DistinctIdentifierElement, otherT.DistinctIdentifierElement)) return false;
            if( !DeepComparable.Matches(ManufacturerElement, otherT.ManufacturerElement)) return false;
            if( !DeepComparable.Matches(ManufactureDateElement, otherT.ManufactureDateElement)) return false;
            if( !DeepComparable.Matches(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.Matches(SerialNumberElement, otherT.SerialNumberElement)) return false;
            if( !DeepComparable.Matches(DeviceName, otherT.DeviceName)) return false;
            if( !DeepComparable.Matches(ModelNumberElement, otherT.ModelNumberElement)) return false;
            if( !DeepComparable.Matches(PartNumberElement, otherT.PartNumberElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Specialization, otherT.Specialization)) return false;
            if( !DeepComparable.Matches(Version, otherT.Version)) return false;
            if( !DeepComparable.Matches(Property, otherT.Property)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Safety, otherT.Safety)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Device;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(UdiCarrier, otherT.UdiCarrier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(DistinctIdentifierElement, otherT.DistinctIdentifierElement)) return false;
            if( !DeepComparable.IsExactly(ManufacturerElement, otherT.ManufacturerElement)) return false;
            if( !DeepComparable.IsExactly(ManufactureDateElement, otherT.ManufactureDateElement)) return false;
            if( !DeepComparable.IsExactly(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.IsExactly(SerialNumberElement, otherT.SerialNumberElement)) return false;
            if( !DeepComparable.IsExactly(DeviceName, otherT.DeviceName)) return false;
            if( !DeepComparable.IsExactly(ModelNumberElement, otherT.ModelNumberElement)) return false;
            if( !DeepComparable.IsExactly(PartNumberElement, otherT.PartNumberElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Specialization, otherT.Specialization)) return false;
            if( !DeepComparable.IsExactly(Version, otherT.Version)) return false;
            if( !DeepComparable.IsExactly(Property, otherT.Property)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Safety, otherT.Safety)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Device");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Definition?.Serialize(sink);
            sink.BeginList("udiCarrier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UdiCarrier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.BeginList("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in StatusReason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("distinctIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DistinctIdentifierElement?.Serialize(sink);
            sink.Element("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManufacturerElement?.Serialize(sink);
            sink.Element("manufactureDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManufactureDateElement?.Serialize(sink);
            sink.Element("expirationDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpirationDateElement?.Serialize(sink);
            sink.Element("lotNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LotNumberElement?.Serialize(sink);
            sink.Element("serialNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SerialNumberElement?.Serialize(sink);
            sink.BeginList("deviceName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DeviceName)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("modelNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ModelNumberElement?.Serialize(sink);
            sink.Element("partNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PartNumberElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
            sink.BeginList("specialization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Specialization)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Version)
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
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Patient?.Serialize(sink);
            sink.Element("owner", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Owner?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Location?.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UrlElement?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("safety", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Safety)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("parent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Parent?.Serialize(sink);
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
                case "definition":
                    Definition = source.Populate(Definition);
                    return true;
                case "udiCarrier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusReason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "distinctIdentifier":
                    DistinctIdentifierElement = source.PopulateValue(DistinctIdentifierElement);
                    return true;
                case "_distinctIdentifier":
                    DistinctIdentifierElement = source.Populate(DistinctIdentifierElement);
                    return true;
                case "manufacturer":
                    ManufacturerElement = source.PopulateValue(ManufacturerElement);
                    return true;
                case "_manufacturer":
                    ManufacturerElement = source.Populate(ManufacturerElement);
                    return true;
                case "manufactureDate":
                    ManufactureDateElement = source.PopulateValue(ManufactureDateElement);
                    return true;
                case "_manufactureDate":
                    ManufactureDateElement = source.Populate(ManufactureDateElement);
                    return true;
                case "expirationDate":
                    ExpirationDateElement = source.PopulateValue(ExpirationDateElement);
                    return true;
                case "_expirationDate":
                    ExpirationDateElement = source.Populate(ExpirationDateElement);
                    return true;
                case "lotNumber":
                    LotNumberElement = source.PopulateValue(LotNumberElement);
                    return true;
                case "_lotNumber":
                    LotNumberElement = source.Populate(LotNumberElement);
                    return true;
                case "serialNumber":
                    SerialNumberElement = source.PopulateValue(SerialNumberElement);
                    return true;
                case "_serialNumber":
                    SerialNumberElement = source.Populate(SerialNumberElement);
                    return true;
                case "deviceName":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "modelNumber":
                    ModelNumberElement = source.PopulateValue(ModelNumberElement);
                    return true;
                case "_modelNumber":
                    ModelNumberElement = source.Populate(ModelNumberElement);
                    return true;
                case "partNumber":
                    PartNumberElement = source.PopulateValue(PartNumberElement);
                    return true;
                case "_partNumber":
                    PartNumberElement = source.Populate(PartNumberElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "specialization":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "version":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "property":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "owner":
                    Owner = source.Populate(Owner);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "safety":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "parent":
                    Parent = source.Populate(Parent);
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
                case "udiCarrier":
                    source.PopulateListItem(UdiCarrier, index);
                    return true;
                case "statusReason":
                    source.PopulateListItem(StatusReason, index);
                    return true;
                case "deviceName":
                    source.PopulateListItem(DeviceName, index);
                    return true;
                case "specialization":
                    source.PopulateListItem(Specialization, index);
                    return true;
                case "version":
                    source.PopulateListItem(Version, index);
                    return true;
                case "property":
                    source.PopulateListItem(Property, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "safety":
                    source.PopulateListItem(Safety, index);
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
                if (Definition != null) yield return Definition;
                foreach (var elem in UdiCarrier) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in StatusReason) { if (elem != null) yield return elem; }
                if (DistinctIdentifierElement != null) yield return DistinctIdentifierElement;
                if (ManufacturerElement != null) yield return ManufacturerElement;
                if (ManufactureDateElement != null) yield return ManufactureDateElement;
                if (ExpirationDateElement != null) yield return ExpirationDateElement;
                if (LotNumberElement != null) yield return LotNumberElement;
                if (SerialNumberElement != null) yield return SerialNumberElement;
                foreach (var elem in DeviceName) { if (elem != null) yield return elem; }
                if (ModelNumberElement != null) yield return ModelNumberElement;
                if (PartNumberElement != null) yield return PartNumberElement;
                if (Type != null) yield return Type;
                foreach (var elem in Specialization) { if (elem != null) yield return elem; }
                foreach (var elem in Version) { if (elem != null) yield return elem; }
                foreach (var elem in Property) { if (elem != null) yield return elem; }
                if (Patient != null) yield return Patient;
                if (Owner != null) yield return Owner;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (Location != null) yield return Location;
                if (UrlElement != null) yield return UrlElement;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Safety) { if (elem != null) yield return elem; }
                if (Parent != null) yield return Parent;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Definition != null) yield return new ElementValue("definition", Definition);
                foreach (var elem in UdiCarrier) { if (elem != null) yield return new ElementValue("udiCarrier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in StatusReason) { if (elem != null) yield return new ElementValue("statusReason", elem); }
                if (DistinctIdentifierElement != null) yield return new ElementValue("distinctIdentifier", DistinctIdentifierElement);
                if (ManufacturerElement != null) yield return new ElementValue("manufacturer", ManufacturerElement);
                if (ManufactureDateElement != null) yield return new ElementValue("manufactureDate", ManufactureDateElement);
                if (ExpirationDateElement != null) yield return new ElementValue("expirationDate", ExpirationDateElement);
                if (LotNumberElement != null) yield return new ElementValue("lotNumber", LotNumberElement);
                if (SerialNumberElement != null) yield return new ElementValue("serialNumber", SerialNumberElement);
                foreach (var elem in DeviceName) { if (elem != null) yield return new ElementValue("deviceName", elem); }
                if (ModelNumberElement != null) yield return new ElementValue("modelNumber", ModelNumberElement);
                if (PartNumberElement != null) yield return new ElementValue("partNumber", PartNumberElement);
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in Specialization) { if (elem != null) yield return new ElementValue("specialization", elem); }
                foreach (var elem in Version) { if (elem != null) yield return new ElementValue("version", elem); }
                foreach (var elem in Property) { if (elem != null) yield return new ElementValue("property", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Owner != null) yield return new ElementValue("owner", Owner);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Safety) { if (elem != null) yield return new ElementValue("safety", elem); }
                if (Parent != null) yield return new ElementValue("parent", Parent);
            }
        }
    
    }

}
