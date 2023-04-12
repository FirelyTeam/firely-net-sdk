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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// An person that is related to a patient, but who is not a direct target of care
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "RelatedPerson", IsResource=true)]
    [DataContract]
    public partial class RelatedPerson : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IRelatedPerson, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.RelatedPerson; } }
        [NotMapped]
        public override string TypeName { get { return "RelatedPerson"; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IRelatedPerson.Telecom { get { return Telecom; } }
    
        
        /// <summary>
        /// A human identifier for this person
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
        /// Whether this related person's record is in active use
        /// </summary>
        [FhirElement("active", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveElement
        {
            get { return _ActiveElement; }
            set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ActiveElement;
        
        /// <summary>
        /// Whether this related person's record is in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Active
        {
            get { return ActiveElement != null ? ActiveElement.Value : null; }
            set
            {
                if (value == null)
                    ActiveElement = null;
                else
                    ActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Active");
            }
        }
        
        /// <summary>
        /// The patient this person is related to
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// The nature of the relationship
        /// </summary>
        [FhirElement("relationship", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; OnPropertyChanged("Relationship"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Relationship;
        
        /// <summary>
        /// A name associated with the person
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.HumanName> Name
        {
            get { if(_Name==null) _Name = new List<Hl7.Fhir.Model.STU3.HumanName>(); return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.HumanName> _Name;
        
        /// <summary>
        /// A contact detail for the person
        /// </summary>
        [FhirElement("telecom", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.STU3.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactPoint> _Telecom;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        [FhirElement("gender", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdministrativeGender> GenderElement
        {
            get { return _GenderElement; }
            set { _GenderElement = value; OnPropertyChanged("GenderElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AdministrativeGender> _GenderElement;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdministrativeGender? Gender
        {
            get { return GenderElement != null ? GenderElement.Value : null; }
            set
            {
                if (value == null)
                    GenderElement = null;
                else
                    GenderElement = new Code<Hl7.Fhir.Model.AdministrativeGender>(value);
                OnPropertyChanged("Gender");
            }
        }
        
        /// <summary>
        /// The date on which the related person was born
        /// </summary>
        [FhirElement("birthDate", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date BirthDateElement
        {
            get { return _BirthDateElement; }
            set { _BirthDateElement = value; OnPropertyChanged("BirthDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _BirthDateElement;
        
        /// <summary>
        /// The date on which the related person was born
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string BirthDate
        {
            get { return BirthDateElement != null ? BirthDateElement.Value : null; }
            set
            {
                if (value == null)
                    BirthDateElement = null;
                else
                    BirthDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("BirthDate");
            }
        }
        
        /// <summary>
        /// Address where the related person can be contacted or visited
        /// </summary>
        [FhirElement("address", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Address> Address
        {
            get { if(_Address==null) _Address = new List<Hl7.Fhir.Model.Address>(); return _Address; }
            set { _Address = value; OnPropertyChanged("Address"); }
        }
        
        private List<Hl7.Fhir.Model.Address> _Address;
        
        /// <summary>
        /// Image of the person
        /// </summary>
        [FhirElement("photo", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Photo
        {
            get { if(_Photo==null) _Photo = new List<Hl7.Fhir.Model.Attachment>(); return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Photo;
        
        /// <summary>
        /// Period of time that this relationship is considered valid
        /// </summary>
        [FhirElement("period", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as RelatedPerson;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                if(Name != null) dest.Name = new List<Hl7.Fhir.Model.STU3.HumanName>(Name.DeepCopy());
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.STU3.ContactPoint>(Telecom.DeepCopy());
                if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
                if(BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.Date)BirthDateElement.DeepCopy();
                if(Address != null) dest.Address = new List<Hl7.Fhir.Model.Address>(Address.DeepCopy());
                if(Photo != null) dest.Photo = new List<Hl7.Fhir.Model.Attachment>(Photo.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new RelatedPerson());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RelatedPerson;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.Matches(BirthDateElement, otherT.BirthDateElement)) return false;
            if( !DeepComparable.Matches(Address, otherT.Address)) return false;
            if( !DeepComparable.Matches(Photo, otherT.Photo)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as RelatedPerson;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.IsExactly(BirthDateElement, otherT.BirthDateElement)) return false;
            if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if( !DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("RelatedPerson");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("active", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActiveElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("relationship", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Relationship?.Serialize(sink);
            sink.BeginList("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Name)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("telecom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Telecom)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("gender", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GenderElement?.Serialize(sink);
            sink.Element("birthDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); BirthDateElement?.Serialize(sink);
            sink.BeginList("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Address)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("photo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Photo)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
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
                case "active":
                    ActiveElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "relationship":
                    Relationship = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "name":
                    Name = source.GetList<Hl7.Fhir.Model.STU3.HumanName>();
                    return true;
                case "telecom":
                    Telecom = source.GetList<Hl7.Fhir.Model.STU3.ContactPoint>();
                    return true;
                case "gender":
                    GenderElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AdministrativeGender>>();
                    return true;
                case "birthDate":
                    BirthDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "address":
                    Address = source.GetList<Hl7.Fhir.Model.Address>();
                    return true;
                case "photo":
                    Photo = source.GetList<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
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
                case "active":
                    ActiveElement = source.PopulateValue(ActiveElement);
                    return true;
                case "_active":
                    ActiveElement = source.Populate(ActiveElement);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "relationship":
                    Relationship = source.Populate(Relationship);
                    return true;
                case "name":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "telecom":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "gender":
                    GenderElement = source.PopulateValue(GenderElement);
                    return true;
                case "_gender":
                    GenderElement = source.Populate(GenderElement);
                    return true;
                case "birthDate":
                    BirthDateElement = source.PopulateValue(BirthDateElement);
                    return true;
                case "_birthDate":
                    BirthDateElement = source.Populate(BirthDateElement);
                    return true;
                case "address":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "photo":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "period":
                    Period = source.Populate(Period);
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
                case "name":
                    source.PopulateListItem(Name, index);
                    return true;
                case "telecom":
                    source.PopulateListItem(Telecom, index);
                    return true;
                case "address":
                    source.PopulateListItem(Address, index);
                    return true;
                case "photo":
                    source.PopulateListItem(Photo, index);
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
                if (ActiveElement != null) yield return ActiveElement;
                if (Patient != null) yield return Patient;
                if (Relationship != null) yield return Relationship;
                foreach (var elem in Name) { if (elem != null) yield return elem; }
                foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                if (GenderElement != null) yield return GenderElement;
                if (BirthDateElement != null) yield return BirthDateElement;
                foreach (var elem in Address) { if (elem != null) yield return elem; }
                foreach (var elem in Photo) { if (elem != null) yield return elem; }
                if (Period != null) yield return Period;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ActiveElement != null) yield return new ElementValue("active", ActiveElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                foreach (var elem in Name) { if (elem != null) yield return new ElementValue("name", elem); }
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                if (BirthDateElement != null) yield return new ElementValue("birthDate", BirthDateElement);
                foreach (var elem in Address) { if (elem != null) yield return new ElementValue("address", elem); }
                foreach (var elem in Photo) { if (elem != null) yield return new ElementValue("photo", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
            }
        }
    
    }

}
