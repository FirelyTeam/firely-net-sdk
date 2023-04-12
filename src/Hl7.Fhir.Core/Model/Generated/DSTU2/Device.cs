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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// An instance of a manufactured te that is used in the provision of healthcare
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Device", IsResource=true)]
    [DataContract]
    public partial class Device : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IDevice, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Device; } }
        [NotMapped]
        public override string TypeName { get { return "Device"; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IDevice.Contact { get { return Contact; } }
    
        
        /// <summary>
        /// Instance id from manufacturer, owner, and others
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
        /// What kind of device this is
        /// </summary>
        [FhirElement("type", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Device notes and comments
        /// </summary>
        [FhirElement("note", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// available | not-available | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.DeviceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.DeviceStatus> _StatusElement;
        
        /// <summary>
        /// available | not-available | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.DeviceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.DeviceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=130)]
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
        /// Model id assigned by the manufacturer
        /// </summary>
        [FhirElement("model", Order=140)]
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
        [FhirElement("version", Order=150)]
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
        /// Manufacture date
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
        /// Manufacture date
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
        [FhirElement("expiry", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ExpiryElement
        {
            get { return _ExpiryElement; }
            set { _ExpiryElement = value; OnPropertyChanged("ExpiryElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ExpiryElement;
        
        /// <summary>
        /// Date and time of expiry of this device (if applicable)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Expiry
        {
            get { return ExpiryElement != null ? ExpiryElement.Value : null; }
            set
            {
                if (value == null)
                    ExpiryElement = null;
                else
                    ExpiryElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Expiry");
            }
        }
        
        /// <summary>
        /// FDA mandated Unique Device Identifier
        /// </summary>
        [FhirElement("udi", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UdiElement
        {
            get { return _UdiElement; }
            set { _UdiElement = value; OnPropertyChanged("UdiElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _UdiElement;
        
        /// <summary>
        /// FDA mandated Unique Device Identifier
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Udi
        {
            get { return UdiElement != null ? UdiElement.Value : null; }
            set
            {
                if (value == null)
                    UdiElement = null;
                else
                    UdiElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Udi");
            }
        }
        
        /// <summary>
        /// Lot number of manufacture
        /// </summary>
        [FhirElement("lotNumber", Order=190)]
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
        /// Where the resource is found
        /// </summary>
        [FhirElement("location", Order=210)]
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
        /// If the resource is affixed to a person
        /// </summary>
        [FhirElement("patient", Order=220)]
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
        /// Details for human/organization for support
        /// </summary>
        [FhirElement("contact", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DSTU2.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.DSTU2.ContactPoint> _Contact;
        
        /// <summary>
        /// Network address to contact device
        /// </summary>
        [FhirElement("url", Order=240)]
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
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Device;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.DeviceStatus>)StatusElement.DeepCopy();
                if(ManufacturerElement != null) dest.ManufacturerElement = (Hl7.Fhir.Model.FhirString)ManufacturerElement.DeepCopy();
                if(ModelElement != null) dest.ModelElement = (Hl7.Fhir.Model.FhirString)ModelElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(ManufactureDateElement != null) dest.ManufactureDateElement = (Hl7.Fhir.Model.FhirDateTime)ManufactureDateElement.DeepCopy();
                if(ExpiryElement != null) dest.ExpiryElement = (Hl7.Fhir.Model.FhirDateTime)ExpiryElement.DeepCopy();
                if(UdiElement != null) dest.UdiElement = (Hl7.Fhir.Model.FhirString)UdiElement.DeepCopy();
                if(LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(Contact.DeepCopy());
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
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
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ManufacturerElement, otherT.ManufacturerElement)) return false;
            if( !DeepComparable.Matches(ModelElement, otherT.ModelElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(ManufactureDateElement, otherT.ManufactureDateElement)) return false;
            if( !DeepComparable.Matches(ExpiryElement, otherT.ExpiryElement)) return false;
            if( !DeepComparable.Matches(UdiElement, otherT.UdiElement)) return false;
            if( !DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Device;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ManufacturerElement, otherT.ManufacturerElement)) return false;
            if( !DeepComparable.IsExactly(ModelElement, otherT.ModelElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(ManufactureDateElement, otherT.ManufactureDateElement)) return false;
            if( !DeepComparable.IsExactly(ExpiryElement, otherT.ExpiryElement)) return false;
            if( !DeepComparable.IsExactly(UdiElement, otherT.UdiElement)) return false;
            if( !DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
        
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
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManufacturerElement?.Serialize(sink);
            sink.Element("model", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ModelElement?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersionElement?.Serialize(sink);
            sink.Element("manufactureDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManufactureDateElement?.Serialize(sink);
            sink.Element("expiry", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpiryElement?.Serialize(sink);
            sink.Element("udi", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UdiElement?.Serialize(sink);
            sink.Element("lotNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LotNumberElement?.Serialize(sink);
            sink.Element("owner", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Owner?.Serialize(sink);
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Location?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Patient?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UrlElement?.Serialize(sink);
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
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.DeviceStatus>>();
                    return true;
                case "manufacturer":
                    ManufacturerElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "model":
                    ModelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "manufactureDate":
                    ManufactureDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "expiry":
                    ExpiryElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "udi":
                    UdiElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "lotNumber":
                    LotNumberElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "owner":
                    Owner = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "url":
                    UrlElement = source.Get<Hl7.Fhir.Model.FhirUri>();
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
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "manufacturer":
                    ManufacturerElement = source.PopulateValue(ManufacturerElement);
                    return true;
                case "_manufacturer":
                    ManufacturerElement = source.Populate(ManufacturerElement);
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
                case "manufactureDate":
                    ManufactureDateElement = source.PopulateValue(ManufactureDateElement);
                    return true;
                case "_manufactureDate":
                    ManufactureDateElement = source.Populate(ManufactureDateElement);
                    return true;
                case "expiry":
                    ExpiryElement = source.PopulateValue(ExpiryElement);
                    return true;
                case "_expiry":
                    ExpiryElement = source.Populate(ExpiryElement);
                    return true;
                case "udi":
                    UdiElement = source.PopulateValue(UdiElement);
                    return true;
                case "_udi":
                    UdiElement = source.Populate(UdiElement);
                    return true;
                case "lotNumber":
                    LotNumberElement = source.PopulateValue(LotNumberElement);
                    return true;
                case "_lotNumber":
                    LotNumberElement = source.Populate(LotNumberElement);
                    return true;
                case "owner":
                    Owner = source.Populate(Owner);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
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
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
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
                if (Type != null) yield return Type;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (ManufacturerElement != null) yield return ManufacturerElement;
                if (ModelElement != null) yield return ModelElement;
                if (VersionElement != null) yield return VersionElement;
                if (ManufactureDateElement != null) yield return ManufactureDateElement;
                if (ExpiryElement != null) yield return ExpiryElement;
                if (UdiElement != null) yield return UdiElement;
                if (LotNumberElement != null) yield return LotNumberElement;
                if (Owner != null) yield return Owner;
                if (Location != null) yield return Location;
                if (Patient != null) yield return Patient;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (UrlElement != null) yield return UrlElement;
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
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ManufacturerElement != null) yield return new ElementValue("manufacturer", ManufacturerElement);
                if (ModelElement != null) yield return new ElementValue("model", ModelElement);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (ManufactureDateElement != null) yield return new ElementValue("manufactureDate", ManufactureDateElement);
                if (ExpiryElement != null) yield return new ElementValue("expiry", ExpiryElement);
                if (UdiElement != null) yield return new ElementValue("udi", UdiElement);
                if (LotNumberElement != null) yield return new ElementValue("lotNumber", LotNumberElement);
                if (Owner != null) yield return new ElementValue("owner", Owner);
                if (Location != null) yield return new ElementValue("location", Location);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
            }
        }
    
    }

}
