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
    /// A generic person record
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Person", IsResource=true)]
    [DataContract]
    public partial class Person : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IPerson, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Person; } }
        [NotMapped]
        public override string TypeName { get { return "Person"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "LinkComponent")]
        [DataContract]
        public partial class LinkComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IPersonLinkComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "LinkComponent"; } }
            
            /// <summary>
            /// The resource to which this actual person is associated
            /// </summary>
            [FhirElement("target", Order=40)]
            [CLSCompliant(false)]
            [References("Patient","Practitioner","RelatedPerson","Person")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            /// <summary>
            /// level1 | level2 | level3 | level4
            /// </summary>
            [FhirElement("assurance", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.IdentityAssuranceLevel> AssuranceElement
            {
                get { return _AssuranceElement; }
                set { _AssuranceElement = value; OnPropertyChanged("AssuranceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.IdentityAssuranceLevel> _AssuranceElement;
            
            /// <summary>
            /// level1 | level2 | level3 | level4
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.IdentityAssuranceLevel? Assurance
            {
                get { return AssuranceElement != null ? AssuranceElement.Value : null; }
                set
                {
                    if (value == null)
                        AssuranceElement = null;
                    else
                        AssuranceElement = new Code<Hl7.Fhir.Model.IdentityAssuranceLevel>(value);
                    OnPropertyChanged("Assurance");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("LinkComponent");
                base.Serialize(sink);
                sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Target?.Serialize(sink);
                sink.Element("assurance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AssuranceElement?.Serialize(sink);
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
                        Target = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "assurance":
                        AssuranceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.IdentityAssuranceLevel>>();
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
                        Target = source.Populate(Target);
                        return true;
                    case "assurance":
                        AssuranceElement = source.PopulateValue(AssuranceElement);
                        return true;
                    case "_assurance":
                        AssuranceElement = source.Populate(AssuranceElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LinkComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    if(AssuranceElement != null) dest.AssuranceElement = (Code<Hl7.Fhir.Model.IdentityAssuranceLevel>)AssuranceElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new LinkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                if( !DeepComparable.Matches(AssuranceElement, otherT.AssuranceElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                if( !DeepComparable.IsExactly(AssuranceElement, otherT.AssuranceElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Target != null) yield return Target;
                    if (AssuranceElement != null) yield return AssuranceElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Target != null) yield return new ElementValue("target", Target);
                    if (AssuranceElement != null) yield return new ElementValue("assurance", AssuranceElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IHumanName> Hl7.Fhir.Model.IPerson.Name { get { return Name; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IPerson.Telecom { get { return Telecom; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IPersonLinkComponent> Hl7.Fhir.Model.IPerson.Link { get { return Link; } }
    
        
        /// <summary>
        /// A human identifier for this person
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
        /// A name associated with the person
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.HumanName> Name
        {
            get { if(_Name==null) _Name = new List<Hl7.Fhir.Model.R4.HumanName>(); return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        
        private List<Hl7.Fhir.Model.R4.HumanName> _Name;
        
        /// <summary>
        /// A contact detail for the person
        /// </summary>
        [FhirElement("telecom", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.R4.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactPoint> _Telecom;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        [FhirElement("gender", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
        /// The date on which the person was born
        /// </summary>
        [FhirElement("birthDate", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date BirthDateElement
        {
            get { return _BirthDateElement; }
            set { _BirthDateElement = value; OnPropertyChanged("BirthDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _BirthDateElement;
        
        /// <summary>
        /// The date on which the person was born
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
        /// One or more addresses for the person
        /// </summary>
        [FhirElement("address", Order=140)]
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
        [FhirElement("photo", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Photo
        {
            get { return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Photo;
        
        /// <summary>
        /// The organization that is the custodian of the person record
        /// </summary>
        [FhirElement("managingOrganization", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization
        {
            get { return _ManagingOrganization; }
            set { _ManagingOrganization = value; OnPropertyChanged("ManagingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ManagingOrganization;
        
        /// <summary>
        /// This person's record is in active use
        /// </summary>
        [FhirElement("active", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveElement
        {
            get { return _ActiveElement; }
            set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ActiveElement;
        
        /// <summary>
        /// This person's record is in active use
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
        /// Link to a resource that concerns the same actual person
        /// </summary>
        [FhirElement("link", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<LinkComponent> Link
        {
            get { if(_Link==null) _Link = new List<LinkComponent>(); return _Link; }
            set { _Link = value; OnPropertyChanged("Link"); }
        }
        
        private List<LinkComponent> _Link;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Person;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Name != null) dest.Name = new List<Hl7.Fhir.Model.R4.HumanName>(Name.DeepCopy());
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.R4.ContactPoint>(Telecom.DeepCopy());
                if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
                if(BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.Date)BirthDateElement.DeepCopy();
                if(Address != null) dest.Address = new List<Hl7.Fhir.Model.Address>(Address.DeepCopy());
                if(Photo != null) dest.Photo = (Hl7.Fhir.Model.Attachment)Photo.DeepCopy();
                if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
                if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
                if(Link != null) dest.Link = new List<LinkComponent>(Link.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Person());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Person;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.Matches(BirthDateElement, otherT.BirthDateElement)) return false;
            if( !DeepComparable.Matches(Address, otherT.Address)) return false;
            if( !DeepComparable.Matches(Photo, otherT.Photo)) return false;
            if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.Matches(Link, otherT.Link)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Person;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.IsExactly(BirthDateElement, otherT.BirthDateElement)) return false;
            if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if( !DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
            if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Person");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
            sink.BeginList("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Address)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("photo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Photo?.Serialize(sink);
            sink.Element("managingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ManagingOrganization?.Serialize(sink);
            sink.Element("active", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActiveElement?.Serialize(sink);
            sink.BeginList("link", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Link)
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
                case "name":
                    Name = source.GetList<Hl7.Fhir.Model.R4.HumanName>();
                    return true;
                case "telecom":
                    Telecom = source.GetList<Hl7.Fhir.Model.R4.ContactPoint>();
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
                    Photo = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "managingOrganization":
                    ManagingOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "active":
                    ActiveElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "link":
                    Link = source.GetList<LinkComponent>();
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
                    Photo = source.Populate(Photo);
                    return true;
                case "managingOrganization":
                    ManagingOrganization = source.Populate(ManagingOrganization);
                    return true;
                case "active":
                    ActiveElement = source.PopulateValue(ActiveElement);
                    return true;
                case "_active":
                    ActiveElement = source.Populate(ActiveElement);
                    return true;
                case "link":
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
                case "name":
                    source.PopulateListItem(Name, index);
                    return true;
                case "telecom":
                    source.PopulateListItem(Telecom, index);
                    return true;
                case "address":
                    source.PopulateListItem(Address, index);
                    return true;
                case "link":
                    source.PopulateListItem(Link, index);
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
                foreach (var elem in Name) { if (elem != null) yield return elem; }
                foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                if (GenderElement != null) yield return GenderElement;
                if (BirthDateElement != null) yield return BirthDateElement;
                foreach (var elem in Address) { if (elem != null) yield return elem; }
                if (Photo != null) yield return Photo;
                if (ManagingOrganization != null) yield return ManagingOrganization;
                if (ActiveElement != null) yield return ActiveElement;
                foreach (var elem in Link) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Name) { if (elem != null) yield return new ElementValue("name", elem); }
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                if (BirthDateElement != null) yield return new ElementValue("birthDate", BirthDateElement);
                foreach (var elem in Address) { if (elem != null) yield return new ElementValue("address", elem); }
                if (Photo != null) yield return new ElementValue("photo", Photo);
                if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                if (ActiveElement != null) yield return new ElementValue("active", ActiveElement);
                foreach (var elem in Link) { if (elem != null) yield return new ElementValue("link", elem); }
            }
        }
    
    }

}
