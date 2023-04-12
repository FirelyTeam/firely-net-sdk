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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Item used in healthcare
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Device", IsResource=true)]
    [DataContract]
    public partial class Device : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IDevice, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Device; } }
        [NotMapped]
        public override string TypeName { get { return "Device"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "UdiComponent")]
        [DataContract]
        public partial class UdiComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "UdiComponent"; } }
            
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
            /// Device Name as appears on UDI label
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Device Name as appears on UDI label
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
            /// UDI Human Readable Barcode String
            /// </summary>
            [FhirElement("carrierHRF", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
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
            /// UDI Machine Readable Barcode String
            /// </summary>
            [FhirElement("carrierAIDC", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
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
            /// UDI Issuing Organization
            /// </summary>
            [FhirElement("issuer", Order=90)]
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
            /// barcode | rfid | manual +
            /// </summary>
            [FhirElement("entryType", Order=100)]
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
                sink.BeginDataType("UdiComponent");
                base.Serialize(sink);
                sink.Element("deviceIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DeviceIdentifierElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.Element("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); JurisdictionElement?.Serialize(sink);
                sink.Element("carrierHRF", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CarrierHRFElement?.Serialize(sink);
                sink.Element("carrierAIDC", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CarrierAIDCElement?.Serialize(sink);
                sink.Element("issuer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); IssuerElement?.Serialize(sink);
                sink.Element("entryType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EntryTypeElement?.Serialize(sink);
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
                    case "deviceIdentifier":
                        DeviceIdentifierElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "jurisdiction":
                        JurisdictionElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "carrierHRF":
                        CarrierHRFElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "carrierAIDC":
                        CarrierAIDCElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
                        return true;
                    case "issuer":
                        IssuerElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "entryType":
                        EntryTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.UDIEntryType>>();
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
                    case "deviceIdentifier":
                        DeviceIdentifierElement = source.PopulateValue(DeviceIdentifierElement);
                        return true;
                    case "_deviceIdentifier":
                        DeviceIdentifierElement = source.Populate(DeviceIdentifierElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "jurisdiction":
                        JurisdictionElement = source.PopulateValue(JurisdictionElement);
                        return true;
                    case "_jurisdiction":
                        JurisdictionElement = source.Populate(JurisdictionElement);
                        return true;
                    case "carrierHRF":
                        CarrierHRFElement = source.PopulateValue(CarrierHRFElement);
                        return true;
                    case "_carrierHRF":
                        CarrierHRFElement = source.Populate(CarrierHRFElement);
                        return true;
                    case "carrierAIDC":
                        CarrierAIDCElement = source.PopulateValue(CarrierAIDCElement);
                        return true;
                    case "_carrierAIDC":
                        CarrierAIDCElement = source.Populate(CarrierAIDCElement);
                        return true;
                    case "issuer":
                        IssuerElement = source.PopulateValue(IssuerElement);
                        return true;
                    case "_issuer":
                        IssuerElement = source.Populate(IssuerElement);
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
                var dest = other as UdiComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DeviceIdentifierElement != null) dest.DeviceIdentifierElement = (Hl7.Fhir.Model.FhirString)DeviceIdentifierElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(JurisdictionElement != null) dest.JurisdictionElement = (Hl7.Fhir.Model.FhirUri)JurisdictionElement.DeepCopy();
                    if(CarrierHRFElement != null) dest.CarrierHRFElement = (Hl7.Fhir.Model.FhirString)CarrierHRFElement.DeepCopy();
                    if(CarrierAIDCElement != null) dest.CarrierAIDCElement = (Hl7.Fhir.Model.Base64Binary)CarrierAIDCElement.DeepCopy();
                    if(IssuerElement != null) dest.IssuerElement = (Hl7.Fhir.Model.FhirUri)IssuerElement.DeepCopy();
                    if(EntryTypeElement != null) dest.EntryTypeElement = (Code<Hl7.Fhir.Model.UDIEntryType>)EntryTypeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new UdiComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UdiComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DeviceIdentifierElement, otherT.DeviceIdentifierElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(JurisdictionElement, otherT.JurisdictionElement)) return false;
                if( !DeepComparable.Matches(CarrierHRFElement, otherT.CarrierHRFElement)) return false;
                if( !DeepComparable.Matches(CarrierAIDCElement, otherT.CarrierAIDCElement)) return false;
                if( !DeepComparable.Matches(IssuerElement, otherT.IssuerElement)) return false;
                if( !DeepComparable.Matches(EntryTypeElement, otherT.EntryTypeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UdiComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DeviceIdentifierElement, otherT.DeviceIdentifierElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(JurisdictionElement, otherT.JurisdictionElement)) return false;
                if( !DeepComparable.IsExactly(CarrierHRFElement, otherT.CarrierHRFElement)) return false;
                if( !DeepComparable.IsExactly(CarrierAIDCElement, otherT.CarrierAIDCElement)) return false;
                if( !DeepComparable.IsExactly(IssuerElement, otherT.IssuerElement)) return false;
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
                    if (NameElement != null) yield return NameElement;
                    if (JurisdictionElement != null) yield return JurisdictionElement;
                    if (CarrierHRFElement != null) yield return CarrierHRFElement;
                    if (CarrierAIDCElement != null) yield return CarrierAIDCElement;
                    if (IssuerElement != null) yield return IssuerElement;
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
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (JurisdictionElement != null) yield return new ElementValue("jurisdiction", JurisdictionElement);
                    if (CarrierHRFElement != null) yield return new ElementValue("carrierHRF", CarrierHRFElement);
                    if (CarrierAIDCElement != null) yield return new ElementValue("carrierAIDC", CarrierAIDCElement);
                    if (IssuerElement != null) yield return new ElementValue("issuer", IssuerElement);
                    if (EntryTypeElement != null) yield return new ElementValue("entryType", EntryTypeElement);
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
        /// Unique Device Identifier (UDI) Barcode string
        /// </summary>
        [FhirElement("udi", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public UdiComponent Udi
        {
            get { return _Udi; }
            set { _Udi = value; OnPropertyChanged("Udi"); }
        }
        
        private UdiComponent _Udi;
        
        /// <summary>
        /// active | inactive | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// What kind of device this is
        /// </summary>
        [FhirElement("type", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Lot number of manufacture
        /// </summary>
        [FhirElement("lotNumber", Order=130)]
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
        /// Name of device manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=140)]
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
        [FhirElement("manufactureDate", Order=150)]
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
        [FhirElement("expirationDate", Order=160)]
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
        /// Model id assigned by the manufacturer
        /// </summary>
        [FhirElement("model", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ModelElement
        {
            get { return _ModelElement; }
            set { _ModelElement = value; OnPropertyChanged("ModelElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ModelElement;
        
        /// <summary>
        /// Model id assigned by the manufacturer
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Model
        {
            get { return ModelElement != null ? ModelElement.Value : null; }
            set
            {
                if (value == null)
                    ModelElement = null;
                else
                    ModelElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Model");
            }
        }
        
        /// <summary>
        /// Version number (i.e. software)
        /// </summary>
        [FhirElement("version", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Version number (i.e. software)
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
        
        /// <summary>
        /// Patient to whom Device is affixed
        /// </summary>
        [FhirElement("patient", Order=190)]
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
        [FhirElement("owner", Order=200)]
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
        [FhirElement("contact", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactPoint> _Contact;
        
        /// <summary>
        /// Where the resource is found
        /// </summary>
        [FhirElement("location", Order=220)]
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
        [FhirElement("url", Order=230)]
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
        [FhirElement("note", Order=240)]
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
        [FhirElement("safety", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Safety
        {
            get { if(_Safety==null) _Safety = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Safety; }
            set { _Safety = value; OnPropertyChanged("Safety"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Safety;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Device;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Udi != null) dest.Udi = (UdiComponent)Udi.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FHIRDeviceStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                if(ManufacturerElement != null) dest.ManufacturerElement = (Hl7.Fhir.Model.FhirString)ManufacturerElement.DeepCopy();
                if(ManufactureDateElement != null) dest.ManufactureDateElement = (Hl7.Fhir.Model.FhirDateTime)ManufactureDateElement.DeepCopy();
                if(ExpirationDateElement != null) dest.ExpirationDateElement = (Hl7.Fhir.Model.FhirDateTime)ExpirationDateElement.DeepCopy();
                if(ModelElement != null) dest.ModelElement = (Hl7.Fhir.Model.FhirString)ModelElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactPoint>(Contact.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Safety != null) dest.Safety = new List<Hl7.Fhir.Model.CodeableConcept>(Safety.DeepCopy());
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
            if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.Matches(ManufacturerElement, otherT.ManufacturerElement)) return false;
            if( !DeepComparable.Matches(ManufactureDateElement, otherT.ManufactureDateElement)) return false;
            if( !DeepComparable.Matches(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.Matches(ModelElement, otherT.ModelElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Safety, otherT.Safety)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Device;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.IsExactly(ManufacturerElement, otherT.ManufacturerElement)) return false;
            if( !DeepComparable.IsExactly(ManufactureDateElement, otherT.ManufactureDateElement)) return false;
            if( !DeepComparable.IsExactly(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.IsExactly(ModelElement, otherT.ModelElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Safety, otherT.Safety)) return false;
        
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
            sink.Element("udi", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Udi?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
            sink.Element("lotNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LotNumberElement?.Serialize(sink);
            sink.Element("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManufacturerElement?.Serialize(sink);
            sink.Element("manufactureDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManufactureDateElement?.Serialize(sink);
            sink.Element("expirationDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpirationDateElement?.Serialize(sink);
            sink.Element("model", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ModelElement?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersionElement?.Serialize(sink);
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
                case "udi":
                    Udi = source.Get<UdiComponent>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRDeviceStatus>>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "lotNumber":
                    LotNumberElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "manufacturer":
                    ManufacturerElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "manufactureDate":
                    ManufactureDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "expirationDate":
                    ExpirationDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "model":
                    ModelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "owner":
                    Owner = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.STU3.ContactPoint>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "url":
                    UrlElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "safety":
                    Safety = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                case "udi":
                    Udi = source.Populate(Udi);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "lotNumber":
                    LotNumberElement = source.PopulateValue(LotNumberElement);
                    return true;
                case "_lotNumber":
                    LotNumberElement = source.Populate(LotNumberElement);
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
                case "model":
                    ModelElement = source.PopulateValue(ModelElement);
                    return true;
                case "_model":
                    ModelElement = source.Populate(ModelElement);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
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
                if (Udi != null) yield return Udi;
                if (StatusElement != null) yield return StatusElement;
                if (Type != null) yield return Type;
                if (LotNumberElement != null) yield return LotNumberElement;
                if (ManufacturerElement != null) yield return ManufacturerElement;
                if (ManufactureDateElement != null) yield return ManufactureDateElement;
                if (ExpirationDateElement != null) yield return ExpirationDateElement;
                if (ModelElement != null) yield return ModelElement;
                if (VersionElement != null) yield return VersionElement;
                if (Patient != null) yield return Patient;
                if (Owner != null) yield return Owner;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (Location != null) yield return Location;
                if (UrlElement != null) yield return UrlElement;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Safety) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Udi != null) yield return new ElementValue("udi", Udi);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (LotNumberElement != null) yield return new ElementValue("lotNumber", LotNumberElement);
                if (ManufacturerElement != null) yield return new ElementValue("manufacturer", ManufacturerElement);
                if (ManufactureDateElement != null) yield return new ElementValue("manufactureDate", ManufactureDateElement);
                if (ExpirationDateElement != null) yield return new ElementValue("expirationDate", ExpirationDateElement);
                if (ModelElement != null) yield return new ElementValue("model", ModelElement);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Owner != null) yield return new ElementValue("owner", Owner);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Safety) { if (elem != null) yield return new ElementValue("safety", elem); }
            }
        }
    
    }

}
