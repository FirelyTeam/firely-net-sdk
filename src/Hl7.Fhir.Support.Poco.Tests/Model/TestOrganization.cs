// Originally generated from hl7.fhir.r3.core version: 3.0.2

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A grouping of people or organizations with a common purpose
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Organization", "http://hl7.org/fhir/StructureDefinition/Organization", IsResource = true)]
    public partial class TestOrganization : Hl7.Fhir.Model.DomainResource
    {
        /// <summary>
        /// FHIR Type Name
        /// </summary>
        public override string TypeName { get { return "Organization"; } }

        /// <summary>
        /// Contact for the organization for a certain purpose
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Organization#Contact", IsNestedType = true)]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Organization#Contact"; } }

            /// <summary>
            /// The type of contact
            /// </summary>
            [FhirElement("purpose", Order = 40)]
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
            [FhirElement("name", Order = 50)]
            [DataMember]
            public Hl7.Fhir.Model.TestHumanName Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }

            private Hl7.Fhir.Model.TestHumanName _Name;

            /// <summary>
            /// Contact details (telephone, email, etc.)  for a contact
            /// </summary>
            [FhirElement("telecom", Order = 60)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ContactPoint> Telecom
            {
                get { if (_Telecom == null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }

            private List<Hl7.Fhir.Model.ContactPoint> _Telecom;

            /// <summary>
            /// Visiting or postal addresses for the contact
            /// </summary>
            [FhirElement("address", Order = 70)]
            [DataMember]
            public Hl7.Fhir.Model.TestAddress Address
            {
                get { return _Address; }
                set { _Address = value; OnPropertyChanged("Address"); }
            }

            private Hl7.Fhir.Model.TestAddress _Address;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (Purpose != null) dest.Purpose = (Hl7.Fhir.Model.CodeableConcept)Purpose.DeepCopy();
                if (Name != null) dest.Name = (Hl7.Fhir.Model.TestHumanName)Name.DeepCopy();
                if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if (Address != null) dest.Address = (Hl7.Fhir.Model.TestAddress)Address.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContactComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
                if (!DeepComparable.Matches(Name, otherT.Name)) return false;
                if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                if (!DeepComparable.Matches(Address, otherT.Address)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
                if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
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

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "purpose":
                        value = Purpose;
                        return Purpose is not null;
                    case "name":
                        value = Name;
                        return Name is not null;
                    case "telecom":
                        value = Telecom;
                        return Telecom?.Any() == true;
                    case "address":
                        value = Address;
                        return Address is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (Purpose is not null) yield return new KeyValuePair<string, object>("purpose", Purpose);
                if (Name is not null) yield return new KeyValuePair<string, object>("name", Name);
                if (Telecom?.Any() == true) yield return new KeyValuePair<string, object>("telecom", Telecom);
                if (Address is not null) yield return new KeyValuePair<string, object>("address", Address);
            }

        }

        /// <summary>
        /// Identifies this organization  across multiple systems
        /// </summary>
        [FhirElement("identifier", InSummary = true, Order = 90)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if (_Identifier == null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }

        private List<Hl7.Fhir.Model.Identifier> _Identifier;

        /// <summary>
        /// Whether the organization's record is still in active use
        /// </summary>
        [FhirElement("active", InSummary = true, Order = 100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveElement
        {
            get { return _ActiveElement; }
            set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
        }

        private Hl7.Fhir.Model.FhirBoolean _ActiveElement;

        /// <summary>
        /// Whether the organization's record is still in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        /// Kind of organization
        /// </summary>
        [FhirElement("type", InSummary = true, Order = 110)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if (_Type == null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }

        private List<Hl7.Fhir.Model.CodeableConcept> _Type;

        /// <summary>
        /// Name used for the organization
        /// </summary>
        [FhirElement("name", InSummary = true, Order = 120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }

        private Hl7.Fhir.Model.FhirString _NameElement;

        /// <summary>
        /// Name used for the organization
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        /// A list of alternate names that the organization is known as, or was known as in the past
        /// </summary>
        [FhirElement("alias", Order = 130)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if (_AliasElement == null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }

        private List<Hl7.Fhir.Model.FhirString> _AliasElement;

        /// <summary>
        /// A list of alternate names that the organization is known as, or was known as in the past
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public IEnumerable<string> Alias
        {
            get { return AliasElement != null ? AliasElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    AliasElement = null;
                else
                    AliasElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem => new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Alias");
            }
        }

        /// <summary>
        /// A contact detail for the organization
        /// </summary>
        [FhirElement("telecom", Order = 140)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Telecom
        {
            get { if (_Telecom == null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }

        private List<Hl7.Fhir.Model.ContactPoint> _Telecom;

        /// <summary>
        /// An address for the organization
        /// </summary>
        [FhirElement("address", Order = 150)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestAddress> Address
        {
            get { if (_Address == null) _Address = new List<Hl7.Fhir.Model.TestAddress>(); return _Address; }
            set { _Address = value; OnPropertyChanged("Address"); }
        }

        private List<Hl7.Fhir.Model.TestAddress> _Address;

        /// <summary>
        /// The organization of which this organization forms a part
        /// </summary>
        [FhirElement("partOf", InSummary = true, Order = 160)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf
        {
            get { return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }

        private Hl7.Fhir.Model.ResourceReference _PartOf;

        /// <summary>
        /// Contact for the organization for a certain purpose
        /// </summary>
        [FhirElement("contact", Order = 170)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestOrganization.ContactComponent> Contact
        {
            get { if (_Contact == null) _Contact = new List<Hl7.Fhir.Model.TestOrganization.ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }

        private List<Hl7.Fhir.Model.TestOrganization.ContactComponent> _Contact;

        /// <summary>
        /// Technical endpoints providing access to services operated for the organization
        /// </summary>
        [FhirElement("endpoint", Order = 180)]
        [References("Endpoint")]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Endpoint
        {
            get { if (_Endpoint == null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
            set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
        }

        private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestOrganization;

            if (dest == null)
            {
                throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            base.CopyTo(dest);
            if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
            if (ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
            if (Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
            if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
            if (AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
            if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
            if (Address != null) dest.Address = new List<Hl7.Fhir.Model.TestAddress>(Address.DeepCopy());
            if (PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
            if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.TestOrganization.ContactComponent>(Contact.DeepCopy());
            if (Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
            return dest;
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestOrganization());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestOrganization;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if (!DeepComparable.Matches(Type, otherT.Type)) return false;
            if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if (!DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if (!DeepComparable.Matches(Address, otherT.Address)) return false;
            if (!DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if (!DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestOrganization;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if (!DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if (!DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if (!DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;

            return true;
        }

        [IgnoreDataMember]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (ActiveElement != null) yield return ActiveElement;
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (NameElement != null) yield return NameElement;
                foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
                foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                foreach (var elem in Address) { if (elem != null) yield return elem; }
                if (PartOf != null) yield return PartOf;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
            }
        }

        [IgnoreDataMember]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ActiveElement != null) yield return new ElementValue("active", ActiveElement);
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                foreach (var elem in Address) { if (elem != null) yield return new ElementValue("address", elem); }
                if (PartOf != null) yield return new ElementValue("partOf", PartOf);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
            }
        }

        protected override bool TryGetValue(string key, out object value)
        {
            switch (key)
            {
                case "identifier":
                    value = Identifier;
                    return Identifier?.Any() == true;
                case "active":
                    value = ActiveElement;
                    return ActiveElement is not null;
                case "type":
                    value = Type;
                    return Type?.Any() == true;
                case "name":
                    value = NameElement;
                    return NameElement is not null;
                case "alias":
                    value = AliasElement;
                    return AliasElement?.Any() == true;
                case "telecom":
                    value = Telecom;
                    return Telecom?.Any() == true;
                case "address":
                    value = Address;
                    return Address?.Any() == true;
                case "partOf":
                    value = PartOf;
                    return PartOf is not null;
                case "contact":
                    value = Contact;
                    return Contact?.Any() == true;
                case "endpoint":
                    value = Endpoint;
                    return Endpoint?.Any() == true;
                default:
                    return base.TryGetValue(key, out value);
            };

        }

        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
            if (Identifier?.Any() == true) yield return new KeyValuePair<string, object>("identifier", Identifier);
            if (ActiveElement is not null) yield return new KeyValuePair<string, object>("active", ActiveElement);
            if (Type?.Any() == true) yield return new KeyValuePair<string, object>("type", Type);
            if (NameElement is not null) yield return new KeyValuePair<string, object>("name", NameElement);
            if (AliasElement?.Any() == true) yield return new KeyValuePair<string, object>("alias", AliasElement);
            if (Telecom?.Any() == true) yield return new KeyValuePair<string, object>("telecom", Telecom);
            if (Address?.Any() == true) yield return new KeyValuePair<string, object>("address", Address);
            if (PartOf is not null) yield return new KeyValuePair<string, object>("partOf", PartOf);
            if (Contact?.Any() == true) yield return new KeyValuePair<string, object>("contact", Contact);
            if (Endpoint?.Any() == true) yield return new KeyValuePair<string, object>("endpoint", Endpoint);
        }

    }

}

// end of file
