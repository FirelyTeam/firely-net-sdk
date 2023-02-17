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
    /// Information about an individual or animal receiving health care services
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Patient", "http://hl7.org/fhir/StructureDefinition/Patient", IsResource = true)]
    public partial class TestPatient : Hl7.Fhir.Model.DomainResource
    {
        /// <summary>
        /// FHIR Type Name
        /// </summary>
        public override string TypeName { get { return "Patient"; } }

        /// <summary>
        /// The type of link between this patient resource and another patient resource.
        /// (url: http://hl7.org/fhir/ValueSet/link-type)
        /// (system: http://hl7.org/fhir/link-type)
        /// </summary>
        [FhirEnumeration("LinkType")]
        public enum LinkType
        {
            /// <summary>
            /// The patient resource containing this link must no longer be used. The link points forward to another patient resource that must be used in lieu of the patient resource that contains this link.
            /// (system: http://hl7.org/fhir/link-type)
            /// </summary>
            [EnumLiteral("replaced-by", "http://hl7.org/fhir/link-type"), Description("Replaced-by")]
            ReplacedBy,
            /// <summary>
            /// The patient resource containing this link is the current active patient record. The link points back to an inactive patient resource that has been merged into this resource, and should be consulted to retrieve additional referenced information.
            /// (system: http://hl7.org/fhir/link-type)
            /// </summary>
            [EnumLiteral("replaces", "http://hl7.org/fhir/link-type"), Description("Replaces")]
            Replaces,
            /// <summary>
            /// The patient resource containing this link is in use and valid but not considered the main source of information about a patient. The link points forward to another patient resource that should be consulted to retrieve additional patient information.
            /// (system: http://hl7.org/fhir/link-type)
            /// </summary>
            [EnumLiteral("refer", "http://hl7.org/fhir/link-type"), Description("Refer")]
            Refer,
            /// <summary>
            /// The patient resource containing this link is in use and valid, but points to another patient resource that is known to contain data about the same person. Data in this resource might overlap or contradict information found in the other patient resource. This link does not indicate any relative importance of the resources concerned, and both should be regarded as equally valid.
            /// (system: http://hl7.org/fhir/link-type)
            /// </summary>
            [EnumLiteral("seealso", "http://hl7.org/fhir/link-type"), Description("See also")]
            Seealso,
        }

        /// <summary>
        /// A contact party (e.g. guardian, partner, friend) for the patient
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Patient#Contact", IsNestedType = true)]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Patient#Contact"; } }

            /// <summary>
            /// The kind of relationship
            /// </summary>
            [FhirElement("relationship", Order = 40)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Relationship
            {
                get { if (_Relationship == null) _Relationship = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }

            private List<Hl7.Fhir.Model.CodeableConcept> _Relationship;

            /// <summary>
            /// A name associated with the contact person
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
            /// A contact detail for the person
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
            /// Address for the contact person
            /// </summary>
            [FhirElement("address", Order = 70)]
            [DataMember]
            public Hl7.Fhir.Model.TestAddress Address
            {
                get { return _Address; }
                set { _Address = value; OnPropertyChanged("Address"); }
            }

            private Hl7.Fhir.Model.TestAddress _Address;

            /// <summary>
            /// male | female | other | unknown
            /// </summary>
            [FhirElement("gender", Order = 80)]
            [DeclaredType(Type = typeof(Code))]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestAdministrativeGender> GenderElement
            {
                get { return _GenderElement; }
                set { _GenderElement = value; OnPropertyChanged("GenderElement"); }
            }

            private Code<Hl7.Fhir.Model.TestAdministrativeGender> _GenderElement;

            /// <summary>
            /// male | female | other | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [IgnoreDataMember]
            public Hl7.Fhir.Model.TestAdministrativeGender? Gender
            {
                get { return GenderElement != null ? GenderElement.Value : null; }
                set
                {
                    if (value == null)
                        GenderElement = null;
                    else
                        GenderElement = new Code<Hl7.Fhir.Model.TestAdministrativeGender>(value);
                    OnPropertyChanged("Gender");
                }
            }

            /// <summary>
            /// Organization that is associated with the contact
            /// </summary>
            [FhirElement("organization", Order = 90)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }

            private Hl7.Fhir.Model.ResourceReference _Organization;

            /// <summary>
            /// The period during which this contact person or organization is valid to be contacted relating to this patient
            /// </summary>
            [FhirElement("period", Order = 100)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }

            private Hl7.Fhir.Model.Period _Period;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (Relationship != null) dest.Relationship = new List<Hl7.Fhir.Model.CodeableConcept>(Relationship.DeepCopy());
                if (Name != null) dest.Name = (Hl7.Fhir.Model.TestHumanName)Name.DeepCopy();
                if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if (Address != null) dest.Address = (Hl7.Fhir.Model.TestAddress)Address.DeepCopy();
                if (GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.TestAdministrativeGender>)GenderElement.DeepCopy();
                if (Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if (Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
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
                if (!DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if (!DeepComparable.Matches(Name, otherT.Name)) return false;
                if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                if (!DeepComparable.Matches(Address, otherT.Address)) return false;
                if (!DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
                if (!DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if (!DeepComparable.Matches(Period, otherT.Period)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
                if (!DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
                if (!DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if (!DeepComparable.IsExactly(Period, otherT.Period)) return false;

                return true;
            }

            [IgnoreDataMember]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Relationship) { if (elem != null) yield return elem; }
                    if (Name != null) yield return Name;
                    foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                    if (Address != null) yield return Address;
                    if (GenderElement != null) yield return GenderElement;
                    if (Organization != null) yield return Organization;
                    if (Period != null) yield return Period;
                }
            }

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Relationship) { if (elem != null) yield return new ElementValue("relationship", elem); }
                    if (Name != null) yield return new ElementValue("name", Name);
                    foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                    if (Address != null) yield return new ElementValue("address", Address);
                    if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                    if (Organization != null) yield return new ElementValue("organization", Organization);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "relationship":
                        value = Relationship;
                        return Relationship?.Any() == true;
                    case "name":
                        value = Name;
                        return Name is not null;
                    case "telecom":
                        value = Telecom;
                        return Telecom?.Any() == true;
                    case "address":
                        value = Address;
                        return Address is not null;
                    case "gender":
                        value = GenderElement;
                        return GenderElement is not null;
                    case "organization":
                        value = Organization;
                        return Organization is not null;
                    case "period":
                        value = Period;
                        return Period is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (Relationship?.Any() == true) yield return new KeyValuePair<string, object>("relationship", Relationship);
                if (Name is not null) yield return new KeyValuePair<string, object>("name", Name);
                if (Telecom?.Any() == true) yield return new KeyValuePair<string, object>("telecom", Telecom);
                if (Address is not null) yield return new KeyValuePair<string, object>("address", Address);
                if (GenderElement is not null) yield return new KeyValuePair<string, object>("gender", GenderElement);
                if (Organization is not null) yield return new KeyValuePair<string, object>("organization", Organization);
                if (Period is not null) yield return new KeyValuePair<string, object>("period", Period);
            }

        }

        /// <summary>
        /// This patient is known to be an animal (non-human)
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Patient#Animal", IsNestedType = true)]
        public partial class AnimalComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Patient#Animal"; } }

            /// <summary>
            /// E.g. Dog, Cow
            /// </summary>
            [FhirElement("species", InSummary = true, Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Species
            {
                get { return _Species; }
                set { _Species = value; OnPropertyChanged("Species"); }
            }

            private Hl7.Fhir.Model.CodeableConcept _Species;

            /// <summary>
            /// E.g. Poodle, Angus
            /// </summary>
            [FhirElement("breed", InSummary = true, Order = 50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Breed
            {
                get { return _Breed; }
                set { _Breed = value; OnPropertyChanged("Breed"); }
            }

            private Hl7.Fhir.Model.CodeableConcept _Breed;

            /// <summary>
            /// E.g. Neutered, Intact
            /// </summary>
            [FhirElement("genderStatus", InSummary = true, Order = 60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept GenderStatus
            {
                get { return _GenderStatus; }
                set { _GenderStatus = value; OnPropertyChanged("GenderStatus"); }
            }

            private Hl7.Fhir.Model.CodeableConcept _GenderStatus;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AnimalComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (Species != null) dest.Species = (Hl7.Fhir.Model.CodeableConcept)Species.DeepCopy();
                if (Breed != null) dest.Breed = (Hl7.Fhir.Model.CodeableConcept)Breed.DeepCopy();
                if (GenderStatus != null) dest.GenderStatus = (Hl7.Fhir.Model.CodeableConcept)GenderStatus.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AnimalComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AnimalComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Species, otherT.Species)) return false;
                if (!DeepComparable.Matches(Breed, otherT.Breed)) return false;
                if (!DeepComparable.Matches(GenderStatus, otherT.GenderStatus)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AnimalComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Species, otherT.Species)) return false;
                if (!DeepComparable.IsExactly(Breed, otherT.Breed)) return false;
                if (!DeepComparable.IsExactly(GenderStatus, otherT.GenderStatus)) return false;

                return true;
            }

            [IgnoreDataMember]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Species != null) yield return Species;
                    if (Breed != null) yield return Breed;
                    if (GenderStatus != null) yield return GenderStatus;
                }
            }

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Species != null) yield return new ElementValue("species", Species);
                    if (Breed != null) yield return new ElementValue("breed", Breed);
                    if (GenderStatus != null) yield return new ElementValue("genderStatus", GenderStatus);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "species":
                        value = Species;
                        return Species is not null;
                    case "breed":
                        value = Breed;
                        return Breed is not null;
                    case "genderStatus":
                        value = GenderStatus;
                        return GenderStatus is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (Species is not null) yield return new KeyValuePair<string, object>("species", Species);
                if (Breed is not null) yield return new KeyValuePair<string, object>("breed", Breed);
                if (GenderStatus is not null) yield return new KeyValuePair<string, object>("genderStatus", GenderStatus);
            }

        }

        /// <summary>
        /// A list of Languages which may be used to communicate with the patient about his or her health
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Patient#Communication", IsNestedType = true)]
        public partial class CommunicationComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Patient#Communication"; } }

            /// <summary>
            /// The language which can be used to communicate with the patient about his or her health
            /// </summary>
            [FhirElement("language", Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Language
            {
                get { return _Language; }
                set { _Language = value; OnPropertyChanged("Language"); }
            }

            private Hl7.Fhir.Model.CodeableConcept _Language;

            /// <summary>
            /// Language preference indicator
            /// </summary>
            [FhirElement("preferred", Order = 50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean PreferredElement
            {
                get { return _PreferredElement; }
                set { _PreferredElement = value; OnPropertyChanged("PreferredElement"); }
            }

            private Hl7.Fhir.Model.FhirBoolean _PreferredElement;

            /// <summary>
            /// Language preference indicator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [IgnoreDataMember]
            public bool? Preferred
            {
                get { return PreferredElement != null ? PreferredElement.Value : null; }
                set
                {
                    if (value == null)
                        PreferredElement = null;
                    else
                        PreferredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Preferred");
                }
            }

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CommunicationComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (Language != null) dest.Language = (Hl7.Fhir.Model.CodeableConcept)Language.DeepCopy();
                if (PreferredElement != null) dest.PreferredElement = (Hl7.Fhir.Model.FhirBoolean)PreferredElement.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CommunicationComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CommunicationComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Language, otherT.Language)) return false;
                if (!DeepComparable.Matches(PreferredElement, otherT.PreferredElement)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CommunicationComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Language, otherT.Language)) return false;
                if (!DeepComparable.IsExactly(PreferredElement, otherT.PreferredElement)) return false;

                return true;
            }

            [IgnoreDataMember]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Language != null) yield return Language;
                    if (PreferredElement != null) yield return PreferredElement;
                }
            }

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Language != null) yield return new ElementValue("language", Language);
                    if (PreferredElement != null) yield return new ElementValue("preferred", PreferredElement);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "language":
                        value = Language;
                        return Language is not null;
                    case "preferred":
                        value = PreferredElement;
                        return PreferredElement is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (Language is not null) yield return new KeyValuePair<string, object>("language", Language);
                if (PreferredElement is not null) yield return new KeyValuePair<string, object>("preferred", PreferredElement);
            }

        }

        /// <summary>
        /// Link to another patient resource that concerns the same actual person
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Patient#Link", IsNestedType = true)]
        public partial class LinkComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Patient#Link"; } }

            /// <summary>
            /// The other patient or related person resource that the link refers to
            /// </summary>
            [FhirElement("other", InSummary = true, Order = 40)]
            [References("Patient", "RelatedPerson")]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Other
            {
                get { return _Other; }
                set { _Other = value; OnPropertyChanged("Other"); }
            }

            private Hl7.Fhir.Model.ResourceReference _Other;

            /// <summary>
            /// replaced-by | replaces | refer | seealso - type of link
            /// </summary>
            [FhirElement("type", InSummary = true, Order = 50)]
            [DeclaredType(Type = typeof(Code))]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Code<LinkType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }

            private Code<LinkType> _TypeElement;

            /// <summary>
            /// replaced-by | replaces | refer | seealso - type of link
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [IgnoreDataMember]
            public LinkType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<LinkType>(value);
                    OnPropertyChanged("Type");
                }
            }

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LinkComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (Other != null) dest.Other = (Hl7.Fhir.Model.ResourceReference)Other.DeepCopy();
                if (TypeElement != null) dest.TypeElement = (Code<LinkType>)TypeElement.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LinkComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Other, otherT.Other)) return false;
                if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Other, otherT.Other)) return false;
                if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;

                return true;
            }

            [IgnoreDataMember]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Other != null) yield return Other;
                    if (TypeElement != null) yield return TypeElement;
                }
            }

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Other != null) yield return new ElementValue("other", Other);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "other":
                        value = Other;
                        return Other is not null;
                    case "type":
                        value = TypeElement;
                        return TypeElement is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (Other is not null) yield return new KeyValuePair<string, object>("other", Other);
                if (TypeElement is not null) yield return new KeyValuePair<string, object>("type", TypeElement);
            }

        }

        /// <summary>
        /// An identifier for this patient
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
        /// Whether this patient's record is in active use
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
        /// Whether this patient's record is in active use
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
        /// A name associated with the patient
        /// </summary>
        [FhirElement("name", InSummary = true, Order = 110)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestHumanName> Name
        {
            get { if (_Name == null) _Name = new List<Hl7.Fhir.Model.TestHumanName>(); return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }

        private List<Hl7.Fhir.Model.TestHumanName> _Name;

        /// <summary>
        /// A contact detail for the individual
        /// </summary>
        [FhirElement("telecom", InSummary = true, Order = 120)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Telecom
        {
            get { if (_Telecom == null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }

        private List<Hl7.Fhir.Model.ContactPoint> _Telecom;

        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        [FhirElement("gender", InSummary = true, Order = 130)]
        [DeclaredType(Type = typeof(Code))]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestAdministrativeGender> GenderElement
        {
            get { return _GenderElement; }
            set { _GenderElement = value; OnPropertyChanged("GenderElement"); }
        }

        private Code<Hl7.Fhir.Model.TestAdministrativeGender> _GenderElement;

        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public Hl7.Fhir.Model.TestAdministrativeGender? Gender
        {
            get { return GenderElement != null ? GenderElement.Value : null; }
            set
            {
                if (value == null)
                    GenderElement = null;
                else
                    GenderElement = new Code<Hl7.Fhir.Model.TestAdministrativeGender>(value);
                OnPropertyChanged("Gender");
            }
        }

        /// <summary>
        /// The date of birth for the individual
        /// </summary>
        [FhirElement("birthDate", InSummary = true, Order = 140)]
        [DataMember]
        public Hl7.Fhir.Model.Date BirthDateElement
        {
            get { return _BirthDateElement; }
            set { _BirthDateElement = value; OnPropertyChanged("BirthDateElement"); }
        }

        private Hl7.Fhir.Model.Date _BirthDateElement;

        /// <summary>
        /// The date of birth for the individual
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        /// Indicates if the individual is deceased or not
        /// </summary>
        [FhirElement("deceased", InSummary = true, Order = 150, Choice = ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.FhirDateTime))]
        [DataMember]
        public Hl7.Fhir.Model.DataType Deceased
        {
            get { return _Deceased; }
            set { _Deceased = value; OnPropertyChanged("Deceased"); }
        }

        private Hl7.Fhir.Model.DataType _Deceased;

        /// <summary>
        /// Addresses for the individual
        /// </summary>
        [FhirElement("address", InSummary = true, Order = 160)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestAddress> Address
        {
            get { if (_Address == null) _Address = new List<Hl7.Fhir.Model.TestAddress>(); return _Address; }
            set { _Address = value; OnPropertyChanged("Address"); }
        }

        private List<Hl7.Fhir.Model.TestAddress> _Address;

        /// <summary>
        /// Marital (civil) status of a patient
        /// </summary>
        [FhirElement("maritalStatus", Order = 170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept MaritalStatus
        {
            get { return _MaritalStatus; }
            set { _MaritalStatus = value; OnPropertyChanged("MaritalStatus"); }
        }

        private Hl7.Fhir.Model.CodeableConcept _MaritalStatus;

        /// <summary>
        /// Whether patient is part of a multiple birth
        /// </summary>
        [FhirElement("multipleBirth", Order = 180, Choice = ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.Integer))]
        [DataMember]
        public Hl7.Fhir.Model.DataType MultipleBirth
        {
            get { return _MultipleBirth; }
            set { _MultipleBirth = value; OnPropertyChanged("MultipleBirth"); }
        }

        private Hl7.Fhir.Model.DataType _MultipleBirth;

        /// <summary>
        /// Image of the patient
        /// </summary>
        [FhirElement("photo", Order = 190)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestAttachment> Photo
        {
            get { if (_Photo == null) _Photo = new List<Hl7.Fhir.Model.TestAttachment>(); return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }

        private List<Hl7.Fhir.Model.TestAttachment> _Photo;

        /// <summary>
        /// A contact party (e.g. guardian, partner, friend) for the patient
        /// </summary>
        [FhirElement("contact", Order = 200)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestPatient.ContactComponent> Contact
        {
            get { if (_Contact == null) _Contact = new List<Hl7.Fhir.Model.TestPatient.ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }

        private List<Hl7.Fhir.Model.TestPatient.ContactComponent> _Contact;

        /// <summary>
        /// This patient is known to be an animal (non-human)
        /// </summary>
        [FhirElement("animal", InSummary = true, Order = 210)]
        [DataMember]
        public Hl7.Fhir.Model.TestPatient.AnimalComponent Animal
        {
            get { return _Animal; }
            set { _Animal = value; OnPropertyChanged("Animal"); }
        }

        private Hl7.Fhir.Model.TestPatient.AnimalComponent _Animal;

        /// <summary>
        /// A list of Languages which may be used to communicate with the patient about his or her health
        /// </summary>
        [FhirElement("communication", Order = 220)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestPatient.CommunicationComponent> Communication
        {
            get { if (_Communication == null) _Communication = new List<Hl7.Fhir.Model.TestPatient.CommunicationComponent>(); return _Communication; }
            set { _Communication = value; OnPropertyChanged("Communication"); }
        }

        private List<Hl7.Fhir.Model.TestPatient.CommunicationComponent> _Communication;

        /// <summary>
        /// Patient's nominated primary care provider
        /// </summary>
        [FhirElement("generalPractitioner", Order = 230)]
        [References("Organization", "Practitioner")]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> GeneralPractitioner
        {
            get { if (_GeneralPractitioner == null) _GeneralPractitioner = new List<Hl7.Fhir.Model.ResourceReference>(); return _GeneralPractitioner; }
            set { _GeneralPractitioner = value; OnPropertyChanged("GeneralPractitioner"); }
        }

        private List<Hl7.Fhir.Model.ResourceReference> _GeneralPractitioner;

        /// <summary>
        /// Organization that is the custodian of the patient record
        /// </summary>
        [FhirElement("managingOrganization", InSummary = true, Order = 240)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization
        {
            get { return _ManagingOrganization; }
            set { _ManagingOrganization = value; OnPropertyChanged("ManagingOrganization"); }
        }

        private Hl7.Fhir.Model.ResourceReference _ManagingOrganization;

        /// <summary>
        /// Link to another patient resource that concerns the same actual person
        /// </summary>
        [FhirElement("link", InSummary = true, Order = 250)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestPatient.LinkComponent> Link
        {
            get { if (_Link == null) _Link = new List<Hl7.Fhir.Model.TestPatient.LinkComponent>(); return _Link; }
            set { _Link = value; OnPropertyChanged("Link"); }
        }

        private List<Hl7.Fhir.Model.TestPatient.LinkComponent> _Link;

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestPatient;

            if (dest == null)
            {
                throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            base.CopyTo(dest);
            if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
            if (ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
            if (Name != null) dest.Name = new List<Hl7.Fhir.Model.TestHumanName>(Name.DeepCopy());
            if (Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
            if (GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.TestAdministrativeGender>)GenderElement.DeepCopy();
            if (BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.Date)BirthDateElement.DeepCopy();
            if (Deceased != null) dest.Deceased = (Hl7.Fhir.Model.DataType)Deceased.DeepCopy();
            if (Address != null) dest.Address = new List<Hl7.Fhir.Model.TestAddress>(Address.DeepCopy());
            if (MaritalStatus != null) dest.MaritalStatus = (Hl7.Fhir.Model.CodeableConcept)MaritalStatus.DeepCopy();
            if (MultipleBirth != null) dest.MultipleBirth = (Hl7.Fhir.Model.DataType)MultipleBirth.DeepCopy();
            if (Photo != null) dest.Photo = new List<Hl7.Fhir.Model.TestAttachment>(Photo.DeepCopy());
            if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.TestPatient.ContactComponent>(Contact.DeepCopy());
            if (Animal != null) dest.Animal = (Hl7.Fhir.Model.TestPatient.AnimalComponent)Animal.DeepCopy();
            if (Communication != null) dest.Communication = new List<Hl7.Fhir.Model.TestPatient.CommunicationComponent>(Communication.DeepCopy());
            if (GeneralPractitioner != null) dest.GeneralPractitioner = new List<Hl7.Fhir.Model.ResourceReference>(GeneralPractitioner.DeepCopy());
            if (ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
            if (Link != null) dest.Link = new List<Hl7.Fhir.Model.TestPatient.LinkComponent>(Link.DeepCopy());
            return dest;
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestPatient());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestPatient;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if (!DeepComparable.Matches(Name, otherT.Name)) return false;
            if (!DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if (!DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
            if (!DeepComparable.Matches(BirthDateElement, otherT.BirthDateElement)) return false;
            if (!DeepComparable.Matches(Deceased, otherT.Deceased)) return false;
            if (!DeepComparable.Matches(Address, otherT.Address)) return false;
            if (!DeepComparable.Matches(MaritalStatus, otherT.MaritalStatus)) return false;
            if (!DeepComparable.Matches(MultipleBirth, otherT.MultipleBirth)) return false;
            if (!DeepComparable.Matches(Photo, otherT.Photo)) return false;
            if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if (!DeepComparable.Matches(Animal, otherT.Animal)) return false;
            if (!DeepComparable.Matches(Communication, otherT.Communication)) return false;
            if (!DeepComparable.Matches(GeneralPractitioner, otherT.GeneralPractitioner)) return false;
            if (!DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if (!DeepComparable.Matches(Link, otherT.Link)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestPatient;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if (!DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if (!DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if (!DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
            if (!DeepComparable.IsExactly(BirthDateElement, otherT.BirthDateElement)) return false;
            if (!DeepComparable.IsExactly(Deceased, otherT.Deceased)) return false;
            if (!DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if (!DeepComparable.IsExactly(MaritalStatus, otherT.MaritalStatus)) return false;
            if (!DeepComparable.IsExactly(MultipleBirth, otherT.MultipleBirth)) return false;
            if (!DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
            if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if (!DeepComparable.IsExactly(Animal, otherT.Animal)) return false;
            if (!DeepComparable.IsExactly(Communication, otherT.Communication)) return false;
            if (!DeepComparable.IsExactly(GeneralPractitioner, otherT.GeneralPractitioner)) return false;
            if (!DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if (!DeepComparable.IsExactly(Link, otherT.Link)) return false;

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
                foreach (var elem in Name) { if (elem != null) yield return elem; }
                foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                if (GenderElement != null) yield return GenderElement;
                if (BirthDateElement != null) yield return BirthDateElement;
                if (Deceased != null) yield return Deceased;
                foreach (var elem in Address) { if (elem != null) yield return elem; }
                if (MaritalStatus != null) yield return MaritalStatus;
                if (MultipleBirth != null) yield return MultipleBirth;
                foreach (var elem in Photo) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (Animal != null) yield return Animal;
                foreach (var elem in Communication) { if (elem != null) yield return elem; }
                foreach (var elem in GeneralPractitioner) { if (elem != null) yield return elem; }
                if (ManagingOrganization != null) yield return ManagingOrganization;
                foreach (var elem in Link) { if (elem != null) yield return elem; }
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
                foreach (var elem in Name) { if (elem != null) yield return new ElementValue("name", elem); }
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                if (BirthDateElement != null) yield return new ElementValue("birthDate", BirthDateElement);
                if (Deceased != null) yield return new ElementValue("deceased", Deceased);
                foreach (var elem in Address) { if (elem != null) yield return new ElementValue("address", elem); }
                if (MaritalStatus != null) yield return new ElementValue("maritalStatus", MaritalStatus);
                if (MultipleBirth != null) yield return new ElementValue("multipleBirth", MultipleBirth);
                foreach (var elem in Photo) { if (elem != null) yield return new ElementValue("photo", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Animal != null) yield return new ElementValue("animal", Animal);
                foreach (var elem in Communication) { if (elem != null) yield return new ElementValue("communication", elem); }
                foreach (var elem in GeneralPractitioner) { if (elem != null) yield return new ElementValue("generalPractitioner", elem); }
                if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                foreach (var elem in Link) { if (elem != null) yield return new ElementValue("link", elem); }
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
                case "name":
                    value = Name;
                    return Name?.Any() == true;
                case "telecom":
                    value = Telecom;
                    return Telecom?.Any() == true;
                case "gender":
                    value = GenderElement;
                    return GenderElement is not null;
                case "birthDate":
                    value = BirthDateElement;
                    return BirthDateElement is not null;
                case "deceased":
                    value = Deceased;
                    return Deceased is not null;
                case "address":
                    value = Address;
                    return Address?.Any() == true;
                case "maritalStatus":
                    value = MaritalStatus;
                    return MaritalStatus is not null;
                case "multipleBirth":
                    value = MultipleBirth;
                    return MultipleBirth is not null;
                case "photo":
                    value = Photo;
                    return Photo?.Any() == true;
                case "contact":
                    value = Contact;
                    return Contact?.Any() == true;
                case "animal":
                    value = Animal;
                    return Animal is not null;
                case "communication":
                    value = Communication;
                    return Communication?.Any() == true;
                case "generalPractitioner":
                    value = GeneralPractitioner;
                    return GeneralPractitioner?.Any() == true;
                case "managingOrganization":
                    value = ManagingOrganization;
                    return ManagingOrganization is not null;
                case "link":
                    value = Link;
                    return Link?.Any() == true;
                default:
                    return base.TryGetValue(key, out value);
            };
        }

        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
            if (Identifier?.Any() == true) yield return new KeyValuePair<string, object>("identifier", Identifier);
            if (ActiveElement is not null) yield return new KeyValuePair<string, object>("active", ActiveElement);
            if (Name?.Any() == true) yield return new KeyValuePair<string, object>("name", Name);
            if (Telecom?.Any() == true) yield return new KeyValuePair<string, object>("telecom", Telecom);
            if (GenderElement is not null) yield return new KeyValuePair<string, object>("gender", GenderElement);
            if (BirthDateElement is not null) yield return new KeyValuePair<string, object>("birthDate", BirthDateElement);
            if (Deceased is not null) yield return new KeyValuePair<string, object>("deceased", Deceased);
            if (Address?.Any() == true) yield return new KeyValuePair<string, object>("address", Address);
            if (MaritalStatus is not null) yield return new KeyValuePair<string, object>("maritalStatus", MaritalStatus);
            if (MultipleBirth is not null) yield return new KeyValuePair<string, object>("multipleBirth", MultipleBirth);
            if (Photo?.Any() == true) yield return new KeyValuePair<string, object>("photo", Photo);
            if (Contact?.Any() == true) yield return new KeyValuePair<string, object>("contact", Contact);
            if (Animal is not null) yield return new KeyValuePair<string, object>("animal", Animal);
            if (Communication?.Any() == true) yield return new KeyValuePair<string, object>("communication", Communication);
            if (GeneralPractitioner?.Any() == true) yield return new KeyValuePair<string, object>("generalPractitioner", GeneralPractitioner);
            if (ManagingOrganization is not null) yield return new KeyValuePair<string, object>("managingOrganization", ManagingOrganization);
            if (Link?.Any() == true) yield return new KeyValuePair<string, object>("link", Link);
        }

    }

}

// end of file
