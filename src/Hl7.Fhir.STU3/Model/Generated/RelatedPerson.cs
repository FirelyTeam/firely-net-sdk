// <auto-generated/>
// Contents of: hl7.fhir.r3.expansions@3.0.2, hl7.fhir.r3.core@3.0.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using SystemPrimitive = Hl7.Fhir.ElementModel.Types;

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
  /// An person that is related to a patient, but who is not a direct target of care
  /// </summary>
  /// <remarks>
  /// Information about a person that is involved in the care for a patient, but who is not the target of healthcare, nor has a formal responsibility in the care process.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("RelatedPerson","http://hl7.org/fhir/StructureDefinition/RelatedPerson")]
  public partial class RelatedPerson : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "RelatedPerson"; } }

    /// <summary>
    /// A human identifier for this person
    /// </summary>
    [FhirElement("identifier", InSummary=true, Order=90, FiveWs="id")]
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
    [FhirElement("active", InSummary=true, IsModifier=true, Order=100, FiveWs="status")]
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
    /// The patient this person is related to
    /// </summary>
    [FhirElement("patient", InSummary=true, Order=110)]
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
    [FhirElement("relationship", InSummary=true, Order=120, FiveWs="class")]
    [Binding("PatientRelationshipType")]
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
    [FhirElement("name", InSummary=true, Order=130)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.HumanName> Name
    {
      get { if(_Name==null) _Name = new List<Hl7.Fhir.Model.HumanName>(); return _Name; }
      set { _Name = value; OnPropertyChanged("Name"); }
    }

    private List<Hl7.Fhir.Model.HumanName> _Name;

    /// <summary>
    /// A contact detail for the person
    /// </summary>
    [FhirElement("telecom", InSummary=true, Order=140)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ContactPoint> Telecom
    {
      get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
      set { _Telecom = value; OnPropertyChanged("Telecom"); }
    }

    private List<Hl7.Fhir.Model.ContactPoint> _Telecom;

    /// <summary>
    /// male | female | other | unknown
    /// </summary>
    [FhirElement("gender", InSummary=true, Order=150)]
    [DeclaredType(Type = typeof(Code))]
    [Binding("AdministrativeGender")]
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
    [IgnoreDataMember]
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
    [FhirElement("birthDate", InSummary=true, Order=160)]
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
    /// Address where the related person can be contacted or visited
    /// </summary>
    [FhirElement("address", InSummary=true, Order=170)]
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
    [FhirElement("period", Order=190, FiveWs="when.done")]
    [DataMember]
    public Hl7.Fhir.Model.Period Period
    {
      get { return _Period; }
      set { _Period = value; OnPropertyChanged("Period"); }
    }

    private Hl7.Fhir.Model.Period _Period;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as RelatedPerson;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
      if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
      if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
      if(Name.Any()) dest.Name = new List<Hl7.Fhir.Model.HumanName>(Name.DeepCopy());
      if(Telecom.Any()) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
      if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
      if(BirthDateElement != null) dest.BirthDateElement = (Hl7.Fhir.Model.Date)BirthDateElement.DeepCopy();
      if(Address.Any()) dest.Address = new List<Hl7.Fhir.Model.Address>(Address.DeepCopy());
      if(Photo.Any()) dest.Photo = new List<Hl7.Fhir.Model.Attachment>(Photo.DeepCopy());
      if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new RelatedPerson());
    }

    ///<inheritdoc />
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

    [IgnoreDataMember]
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

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
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
        case "patient":
          value = Patient;
          return Patient is not null;
        case "relationship":
          value = Relationship;
          return Relationship is not null;
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
        case "address":
          value = Address;
          return Address?.Any() == true;
        case "photo":
          value = Photo;
          return Photo?.Any() == true;
        case "period":
          value = Period;
          return Period is not null;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override Base SetValue(string key, object value)
    {
      switch (key)
      {
        case "identifier":
          Identifier = (List<Hl7.Fhir.Model.Identifier>)value;
          return this;
        case "active":
          ActiveElement = (Hl7.Fhir.Model.FhirBoolean)value;
          return this;
        case "patient":
          Patient = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "relationship":
          Relationship = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "name":
          Name = (List<Hl7.Fhir.Model.HumanName>)value;
          return this;
        case "telecom":
          Telecom = (List<Hl7.Fhir.Model.ContactPoint>)value;
          return this;
        case "gender":
          GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)value;
          return this;
        case "birthDate":
          BirthDateElement = (Hl7.Fhir.Model.Date)value;
          return this;
        case "address":
          Address = (List<Hl7.Fhir.Model.Address>)value;
          return this;
        case "photo":
          Photo = (List<Hl7.Fhir.Model.Attachment>)value;
          return this;
        case "period":
          Period = (Hl7.Fhir.Model.Period)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (Identifier?.Any() == true) yield return new KeyValuePair<string,object>("identifier",Identifier);
      if (ActiveElement is not null) yield return new KeyValuePair<string,object>("active",ActiveElement);
      if (Patient is not null) yield return new KeyValuePair<string,object>("patient",Patient);
      if (Relationship is not null) yield return new KeyValuePair<string,object>("relationship",Relationship);
      if (Name?.Any() == true) yield return new KeyValuePair<string,object>("name",Name);
      if (Telecom?.Any() == true) yield return new KeyValuePair<string,object>("telecom",Telecom);
      if (GenderElement is not null) yield return new KeyValuePair<string,object>("gender",GenderElement);
      if (BirthDateElement is not null) yield return new KeyValuePair<string,object>("birthDate",BirthDateElement);
      if (Address?.Any() == true) yield return new KeyValuePair<string,object>("address",Address);
      if (Photo?.Any() == true) yield return new KeyValuePair<string,object>("photo",Photo);
      if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
    }

  }

}

// end of file
