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
    /// A person with a  formal responsibility in the provisioning of healthcare or related services
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Practitioner", IsResource=true)]
    [DataContract]
    public partial class Practitioner : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IPractitioner, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Practitioner; } }
        [NotMapped]
        public override string TypeName { get { return "Practitioner"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "PractitionerRoleComponent")]
        [DataContract]
        public partial class PractitionerRoleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PractitionerRoleComponent"; } }
            
            /// <summary>
            /// Organization where the roles are performed
            /// </summary>
            [FhirElement("managingOrganization", Order=40)]
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
            /// Roles which this practitioner may perform
            /// </summary>
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Specific specialty of the practitioner
            /// </summary>
            [FhirElement("specialty", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Specialty
            {
                get { if(_Specialty==null) _Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Specialty; }
                set { _Specialty = value; OnPropertyChanged("Specialty"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
            
            /// <summary>
            /// The period during which the practitioner is authorized to perform in these role(s)
            /// </summary>
            [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// The location(s) at which this practitioner provides care
            /// </summary>
            [FhirElement("location", Order=80)]
            [CLSCompliant(false)]
            [References("Location")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Location
            {
                get { if(_Location==null) _Location = new List<Hl7.Fhir.Model.ResourceReference>(); return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Location;
            
            /// <summary>
            /// The list of healthcare services that this worker provides for this role's Organization/Location(s)
            /// </summary>
            [FhirElement("healthcareService", Order=90)]
            [CLSCompliant(false)]
            [References("HealthcareService")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> HealthcareService
            {
                get { if(_HealthcareService==null) _HealthcareService = new List<Hl7.Fhir.Model.ResourceReference>(); return _HealthcareService; }
                set { _HealthcareService = value; OnPropertyChanged("HealthcareService"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _HealthcareService;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PractitionerRoleComponent");
                base.Serialize(sink);
                sink.Element("managingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ManagingOrganization?.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Role?.Serialize(sink);
                sink.BeginList("specialty", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Specialty)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
                sink.BeginList("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Location)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("healthcareService", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in HealthcareService)
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
                    case "managingOrganization":
                        ManagingOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "role":
                        Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "specialty":
                        Specialty = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "location":
                        Location = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "healthcareService":
                        HealthcareService = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                    case "managingOrganization":
                        ManagingOrganization = source.Populate(ManagingOrganization);
                        return true;
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "specialty":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                    case "location":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "healthcareService":
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
                    case "specialty":
                        source.PopulateListItem(Specialty, index);
                        return true;
                    case "location":
                        source.PopulateListItem(Location, index);
                        return true;
                    case "healthcareService":
                        source.PopulateListItem(HealthcareService, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PractitionerRoleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Location != null) dest.Location = new List<Hl7.Fhir.Model.ResourceReference>(Location.DeepCopy());
                    if(HealthcareService != null) dest.HealthcareService = new List<Hl7.Fhir.Model.ResourceReference>(HealthcareService.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PractitionerRoleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PractitionerRoleComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(HealthcareService, otherT.HealthcareService)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PractitionerRoleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(HealthcareService, otherT.HealthcareService)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ManagingOrganization != null) yield return ManagingOrganization;
                    if (Role != null) yield return Role;
                    foreach (var elem in Specialty) { if (elem != null) yield return elem; }
                    if (Period != null) yield return Period;
                    foreach (var elem in Location) { if (elem != null) yield return elem; }
                    foreach (var elem in HealthcareService) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                    if (Role != null) yield return new ElementValue("role", Role);
                    foreach (var elem in Specialty) { if (elem != null) yield return new ElementValue("specialty", elem); }
                    if (Period != null) yield return new ElementValue("period", Period);
                    foreach (var elem in Location) { if (elem != null) yield return new ElementValue("location", elem); }
                    foreach (var elem in HealthcareService) { if (elem != null) yield return new ElementValue("healthcareService", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "QualificationComponent")]
        [DataContract]
        public partial class QualificationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IPractitionerQualificationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "QualificationComponent"; } }
            
            /// <summary>
            /// An identifier for this qualification for the practitioner
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Coded representation of the qualification
            /// </summary>
            [FhirElement("code", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Period during which the qualification is valid
            /// </summary>
            [FhirElement("period", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// Organization that regulates and issues the qualification
            /// </summary>
            [FhirElement("issuer", Order=70)]
            [CLSCompliant(false)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Issuer
            {
                get { return _Issuer; }
                set { _Issuer = value; OnPropertyChanged("Issuer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Issuer;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("QualificationComponent");
                base.Serialize(sink);
                sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Identifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Code?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
                sink.Element("issuer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Issuer?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "issuer":
                        Issuer = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                    case "issuer":
                        Issuer = source.Populate(Issuer);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualificationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Issuer != null) dest.Issuer = (Hl7.Fhir.Model.ResourceReference)Issuer.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new QualificationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QualificationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Issuer, otherT.Issuer)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QualificationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Issuer, otherT.Issuer)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (Code != null) yield return Code;
                    if (Period != null) yield return Period;
                    if (Issuer != null) yield return Issuer;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Period != null) yield return new ElementValue("period", Period);
                    if (Issuer != null) yield return new ElementValue("issuer", Issuer);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IPractitioner.Telecom { get { return Telecom; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IPractitionerQualificationComponent> Hl7.Fhir.Model.IPractitioner.Qualification { get { return Qualification; } }
    
        
        /// <summary>
        /// A identifier for the person as this agent
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
        /// Whether this practitioner's record is in active use
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
        /// Whether this practitioner's record is in active use
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
        /// A name associated with the person
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.HumanName Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.HumanName _Name;
        
        /// <summary>
        /// A contact detail for the practitioner
        /// </summary>
        [FhirElement("telecom", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DSTU2.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.DSTU2.ContactPoint> _Telecom;
        
        /// <summary>
        /// Where practitioner can be found/visited
        /// </summary>
        [FhirElement("address", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
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
        /// male | female | other | unknown
        /// </summary>
        [FhirElement("gender", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// The date  on which the practitioner was born
        /// </summary>
        [FhirElement("birthDate", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date BirthDateElement
        {
            get { return _BirthDateElement; }
            set { _BirthDateElement = value; OnPropertyChanged("BirthDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _BirthDateElement;
        
        /// <summary>
        /// The date  on which the practitioner was born
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
        /// Image of the person
        /// </summary>
        [FhirElement("photo", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Photo
        {
            get { if(_Photo==null) _Photo = new List<Hl7.Fhir.Model.Attachment>(); return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Photo;
        
        /// <summary>
        /// Roles/organizations the practitioner is associated with
        /// </summary>
        [FhirElement("practitionerRole", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PractitionerRoleComponent> PractitionerRole
        {
            get { if(_PractitionerRole==null) _PractitionerRole = new List<PractitionerRoleComponent>(); return _PractitionerRole; }
            set { _PractitionerRole = value; OnPropertyChanged("PractitionerRole"); }
        }
        
        private List<PractitionerRoleComponent> _PractitionerRole;
        
        /// <summary>
        /// Qualifications obtained by training and certification
        /// </summary>
        [FhirElement("qualification", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<QualificationComponent> Qualification
        {
            get { if(_Qualification==null) _Qualification = new List<QualificationComponent>(); return _Qualification; }
            set { _Qualification = value; OnPropertyChanged("Qualification"); }
        }
        
        private List<QualificationComponent> _Qualification;
        
        /// <summary>
        /// A language the practitioner is able to use in patient communication
        /// </summary>
        [FhirElement("communication", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Communication
        {
            get { if(_Communication==null) _Communication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Communication; }
            set { _Communication = value; OnPropertyChanged("Communication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Communication;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Practitioner;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
                if(Name != null) dest.Name = (Hl7.Fhir.Model.DSTU2.HumanName)Name.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(Telecom.DeepCopy());
                if(Address != null) dest.Address = new List<Hl7.Fhir.Model.Address>(Address.DeepCopy());
                if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
                if(BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.Date)BirthDateElement.DeepCopy();
                if(Photo != null) dest.Photo = new List<Hl7.Fhir.Model.Attachment>(Photo.DeepCopy());
                if(PractitionerRole != null) dest.PractitionerRole = new List<PractitionerRoleComponent>(PractitionerRole.DeepCopy());
                if(Qualification != null) dest.Qualification = new List<QualificationComponent>(Qualification.DeepCopy());
                if(Communication != null) dest.Communication = new List<Hl7.Fhir.Model.CodeableConcept>(Communication.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Practitioner());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Practitioner;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(Address, otherT.Address)) return false;
            if( !DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.Matches(BirthDateElement, otherT.BirthDateElement)) return false;
            if( !DeepComparable.Matches(Photo, otherT.Photo)) return false;
            if( !DeepComparable.Matches(PractitionerRole, otherT.PractitionerRole)) return false;
            if( !DeepComparable.Matches(Qualification, otherT.Qualification)) return false;
            if( !DeepComparable.Matches(Communication, otherT.Communication)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Practitioner;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if( !DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.IsExactly(BirthDateElement, otherT.BirthDateElement)) return false;
            if( !DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
            if( !DeepComparable.IsExactly(PractitionerRole, otherT.PractitionerRole)) return false;
            if( !DeepComparable.IsExactly(Qualification, otherT.Qualification)) return false;
            if( !DeepComparable.IsExactly(Communication, otherT.Communication)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Practitioner");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("active", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActiveElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Name?.Serialize(sink);
            sink.BeginList("telecom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Telecom)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Address)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("gender", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GenderElement?.Serialize(sink);
            sink.Element("birthDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); BirthDateElement?.Serialize(sink);
            sink.BeginList("photo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Photo)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("practitionerRole", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in PractitionerRole)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("qualification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Qualification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("communication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Communication)
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
                case "active":
                    ActiveElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "name":
                    Name = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "telecom":
                    Telecom = source.GetList<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "address":
                    Address = source.GetList<Hl7.Fhir.Model.Address>();
                    return true;
                case "gender":
                    GenderElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AdministrativeGender>>();
                    return true;
                case "birthDate":
                    BirthDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "photo":
                    Photo = source.GetList<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "practitionerRole":
                    PractitionerRole = source.GetList<PractitionerRoleComponent>();
                    return true;
                case "qualification":
                    Qualification = source.GetList<QualificationComponent>();
                    return true;
                case "communication":
                    Communication = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                case "name":
                    Name = source.Populate(Name);
                    return true;
                case "telecom":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "address":
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
                case "photo":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "practitionerRole":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "qualification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "communication":
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
                case "telecom":
                    source.PopulateListItem(Telecom, index);
                    return true;
                case "address":
                    source.PopulateListItem(Address, index);
                    return true;
                case "photo":
                    source.PopulateListItem(Photo, index);
                    return true;
                case "practitionerRole":
                    source.PopulateListItem(PractitionerRole, index);
                    return true;
                case "qualification":
                    source.PopulateListItem(Qualification, index);
                    return true;
                case "communication":
                    source.PopulateListItem(Communication, index);
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
                if (Name != null) yield return Name;
                foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                foreach (var elem in Address) { if (elem != null) yield return elem; }
                if (GenderElement != null) yield return GenderElement;
                if (BirthDateElement != null) yield return BirthDateElement;
                foreach (var elem in Photo) { if (elem != null) yield return elem; }
                foreach (var elem in PractitionerRole) { if (elem != null) yield return elem; }
                foreach (var elem in Qualification) { if (elem != null) yield return elem; }
                foreach (var elem in Communication) { if (elem != null) yield return elem; }
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
                if (Name != null) yield return new ElementValue("name", Name);
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                foreach (var elem in Address) { if (elem != null) yield return new ElementValue("address", elem); }
                if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                if (BirthDateElement != null) yield return new ElementValue("birthDate", BirthDateElement);
                foreach (var elem in Photo) { if (elem != null) yield return new ElementValue("photo", elem); }
                foreach (var elem in PractitionerRole) { if (elem != null) yield return new ElementValue("practitionerRole", elem); }
                foreach (var elem in Qualification) { if (elem != null) yield return new ElementValue("qualification", elem); }
                foreach (var elem in Communication) { if (elem != null) yield return new ElementValue("communication", elem); }
            }
        }
    
    }

}
