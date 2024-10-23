// <auto-generated/>
// Contents of: hl7.fhir.r5.expansions@5.0.0, hl7.fhir.r5.core@5.0.0

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
  /// Group of multiple entities
  /// </summary>
  /// <remarks>
  /// Represents a defined collection of entities that may be discussed or acted upon collectively but which are not expected to act collectively, and are not formally or legally recognized; i.e. a collection of entities that isn't an Organization.
  /// If both Group.characteristic and Group.member are present, then the members are the individuals who were found who met the characteristic.  It's possible that there might be other candidate members who meet the characteristic and aren't (yet) in the list.  All members SHALL have the listed characteristics.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("Group","http://hl7.org/fhir/StructureDefinition/Group")]
  public partial class Group : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "Group"; } }

    /// <summary>
    /// Types of resources that are part of group.
    /// (url: http://hl7.org/fhir/ValueSet/group-type)
    /// (system: http://hl7.org/fhir/group-type)
    /// </summary>
    [FhirEnumeration("GroupType", "http://hl7.org/fhir/ValueSet/group-type", "http://hl7.org/fhir/group-type")]
    public enum GroupType
    {
      /// <summary>
      /// Group contains \"person\" Patient resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("person"), Description("Person")]
      Person,
      /// <summary>
      /// Group contains \"animal\" Patient resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("animal"), Description("Animal")]
      Animal,
      /// <summary>
      /// Group contains healthcare practitioner resources (Practitioner or PractitionerRole).
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("practitioner"), Description("Practitioner")]
      Practitioner,
      /// <summary>
      /// Group contains Device resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("device"), Description("Device")]
      Device,
      /// <summary>
      /// Group contains CareTeam resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("careteam"), Description("CareTeam")]
      Careteam,
      /// <summary>
      /// Group contains HealthcareService resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("healthcareservice"), Description("HealthcareService")]
      Healthcareservice,
      /// <summary>
      /// Group contains Location resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("location"), Description("Location")]
      Location,
      /// <summary>
      /// Group contains Organization resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("organization"), Description("Organization")]
      Organization,
      /// <summary>
      /// Group contains RelatedPerson resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("relatedperson"), Description("RelatedPerson")]
      Relatedperson,
      /// <summary>
      /// Group contains Specimen resources.
      /// (system: http://hl7.org/fhir/group-type)
      /// </summary>
      [EnumLiteral("specimen"), Description("Specimen")]
      Specimen,
    }

    /// <summary>
    /// Basis for membership in a group
    /// (url: http://hl7.org/fhir/ValueSet/group-membership-basis)
    /// (system: http://hl7.org/fhir/group-membership-basis)
    /// </summary>
    [FhirEnumeration("GroupMembershipBasis", "http://hl7.org/fhir/ValueSet/group-membership-basis", "http://hl7.org/fhir/group-membership-basis")]
    public enum GroupMembershipBasis
    {
      /// <summary>
      /// The Group.characteristics specified are both necessary and sufficient to determine membership. All entities that meet the criteria are considered to be members of the group, whether referenced by the group or not. If members are present, they are individuals that happen to be known as meeting the Group.characteristics. The list cannot be presumed to be complete.
      /// (system: http://hl7.org/fhir/group-membership-basis)
      /// </summary>
      [EnumLiteral("definitional"), Description("Definitional")]
      Definitional,
      /// <summary>
      /// The Group.characteristics are necessary but not sufficient to determine membership. Membership is determined by being listed as one of the Group.member.
      /// (system: http://hl7.org/fhir/group-membership-basis)
      /// </summary>
      [EnumLiteral("enumerated"), Description("Enumerated")]
      Enumerated,
    }

    /// <summary>
    /// Include / Exclude group members by Trait
    /// </summary>
    /// <remarks>
    /// Identifies traits whose presence r absence is shared by members of the group.
    /// All the identified characteristics must be true for an entity to a member of the group.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("Group.characteristic", IsBackboneType=true)]
    public partial class CharacteristicComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Group.characteristic"; } }

      /// <summary>
      /// Kind of characteristic
      /// </summary>
      [FhirElement("code", InSummary=true, Order=40)]
      [Binding("GroupCharacteristicKind")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Code
      {
        get { return _Code; }
        set { _Code = value; OnPropertyChanged("Code"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Code;

      /// <summary>
      /// Value held by characteristic
      /// </summary>
      [FhirElement("value", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
      [Binding("GroupCharacteristicValue")]
      [CLSCompliant(false)]
      [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.ResourceReference))]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.DataType Value
      {
        get { return _Value; }
        set { _Value = value; OnPropertyChanged("Value"); }
      }

      private Hl7.Fhir.Model.DataType _Value;

      /// <summary>
      /// Group includes or excludes
      /// </summary>
      [FhirElement("exclude", InSummary=true, Order=60)]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.FhirBoolean ExcludeElement
      {
        get { return _ExcludeElement; }
        set { _ExcludeElement = value; OnPropertyChanged("ExcludeElement"); }
      }

      private Hl7.Fhir.Model.FhirBoolean _ExcludeElement;

      /// <summary>
      /// Group includes or excludes
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public bool? Exclude
      {
        get { return ExcludeElement != null ? ExcludeElement.Value : null; }
        set
        {
          if (value == null)
            ExcludeElement = null;
          else
            ExcludeElement = new Hl7.Fhir.Model.FhirBoolean(value);
          OnPropertyChanged("Exclude");
        }
      }

      /// <summary>
      /// Period over which characteristic is tested
      /// </summary>
      [FhirElement("period", Order=70)]
      [DataMember]
      public Hl7.Fhir.Model.Period Period
      {
        get { return _Period; }
        set { _Period = value; OnPropertyChanged("Period"); }
      }

      private Hl7.Fhir.Model.Period _Period;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as CharacteristicComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
        if(Value != null) dest.Value = (Hl7.Fhir.Model.DataType)Value.DeepCopy();
        if(ExcludeElement != null) dest.ExcludeElement = (Hl7.Fhir.Model.FhirBoolean)ExcludeElement.DeepCopy();
        if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new CharacteristicComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as CharacteristicComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Code, otherT.Code)) return false;
        if( !DeepComparable.Matches(Value, otherT.Value)) return false;
        if( !DeepComparable.Matches(ExcludeElement, otherT.ExcludeElement)) return false;
        if( !DeepComparable.Matches(Period, otherT.Period)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as CharacteristicComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
        if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
        if( !DeepComparable.IsExactly(ExcludeElement, otherT.ExcludeElement)) return false;
        if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Code != null) yield return Code;
          if (Value != null) yield return Value;
          if (ExcludeElement != null) yield return ExcludeElement;
          if (Period != null) yield return Period;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Code != null) yield return new ElementValue("code", Code);
          if (Value != null) yield return new ElementValue("value", Value);
          if (ExcludeElement != null) yield return new ElementValue("exclude", ExcludeElement);
          if (Period != null) yield return new ElementValue("period", Period);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "code":
            value = Code;
            return Code is not null;
          case "value":
            value = Value;
            return Value is not null;
          case "exclude":
            value = ExcludeElement;
            return ExcludeElement is not null;
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
          case "code":
            Code = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "value":
            Value = (Hl7.Fhir.Model.DataType)value;
            return this;
          case "exclude":
            ExcludeElement = (Hl7.Fhir.Model.FhirBoolean)value;
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
        if (Code is not null) yield return new KeyValuePair<string,object>("code",Code);
        if (Value is not null) yield return new KeyValuePair<string,object>("value",Value);
        if (ExcludeElement is not null) yield return new KeyValuePair<string,object>("exclude",ExcludeElement);
        if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      }

    }

    /// <summary>
    /// Who or what is in group
    /// </summary>
    /// <remarks>
    /// Identifies the resource instances that are members of the group.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("Group.member", IsBackboneType=true)]
    public partial class MemberComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Group.member"; } }

      /// <summary>
      /// Reference to the group member
      /// </summary>
      [FhirElement("entity", Order=40)]
      [CLSCompliant(false)]
      [References("CareTeam","Device","Group","HealthcareService","Location","Organization","Patient","Practitioner","PractitionerRole","RelatedPerson","Specimen")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Entity
      {
        get { return _Entity; }
        set { _Entity = value; OnPropertyChanged("Entity"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Entity;

      /// <summary>
      /// Period member belonged to the group
      /// </summary>
      [FhirElement("period", Order=50)]
      [DataMember]
      public Hl7.Fhir.Model.Period Period
      {
        get { return _Period; }
        set { _Period = value; OnPropertyChanged("Period"); }
      }

      private Hl7.Fhir.Model.Period _Period;

      /// <summary>
      /// If member is no longer in group
      /// </summary>
      [FhirElement("inactive", Order=60)]
      [DataMember]
      public Hl7.Fhir.Model.FhirBoolean InactiveElement
      {
        get { return _InactiveElement; }
        set { _InactiveElement = value; OnPropertyChanged("InactiveElement"); }
      }

      private Hl7.Fhir.Model.FhirBoolean _InactiveElement;

      /// <summary>
      /// If member is no longer in group
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public bool? Inactive
      {
        get { return InactiveElement != null ? InactiveElement.Value : null; }
        set
        {
          if (value == null)
            InactiveElement = null;
          else
            InactiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
          OnPropertyChanged("Inactive");
        }
      }

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as MemberComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Entity != null) dest.Entity = (Hl7.Fhir.Model.ResourceReference)Entity.DeepCopy();
        if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
        if(InactiveElement != null) dest.InactiveElement = (Hl7.Fhir.Model.FhirBoolean)InactiveElement.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new MemberComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as MemberComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
        if( !DeepComparable.Matches(Period, otherT.Period)) return false;
        if( !DeepComparable.Matches(InactiveElement, otherT.InactiveElement)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as MemberComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
        if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
        if( !DeepComparable.IsExactly(InactiveElement, otherT.InactiveElement)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Entity != null) yield return Entity;
          if (Period != null) yield return Period;
          if (InactiveElement != null) yield return InactiveElement;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Entity != null) yield return new ElementValue("entity", Entity);
          if (Period != null) yield return new ElementValue("period", Period);
          if (InactiveElement != null) yield return new ElementValue("inactive", InactiveElement);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "entity":
            value = Entity;
            return Entity is not null;
          case "period":
            value = Period;
            return Period is not null;
          case "inactive":
            value = InactiveElement;
            return InactiveElement is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "entity":
            Entity = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          case "period":
            Period = (Hl7.Fhir.Model.Period)value;
            return this;
          case "inactive":
            InactiveElement = (Hl7.Fhir.Model.FhirBoolean)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Entity is not null) yield return new KeyValuePair<string,object>("entity",Entity);
        if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
        if (InactiveElement is not null) yield return new KeyValuePair<string,object>("inactive",InactiveElement);
      }

    }

    /// <summary>
    /// Business Identifier for this Group
    /// </summary>
    [FhirElement("identifier", InSummary=true, Order=90, FiveWs="FiveWs.identifier")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Identifier> Identifier
    {
      get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
      set { _Identifier = value; OnPropertyChanged("Identifier"); }
    }

    private List<Hl7.Fhir.Model.Identifier> _Identifier;

    /// <summary>
    /// Whether this group's record is in active use
    /// </summary>
    [FhirElement("active", InSummary=true, IsModifier=true, Order=100, FiveWs="FiveWs.status")]
    [DataMember]
    public Hl7.Fhir.Model.FhirBoolean ActiveElement
    {
      get { return _ActiveElement; }
      set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
    }

    private Hl7.Fhir.Model.FhirBoolean _ActiveElement;

    /// <summary>
    /// Whether this group's record is in active use
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
    /// person | animal | practitioner | device | careteam | healthcareservice | location | organization | relatedperson | specimen
    /// </summary>
    [FhirElement("type", InSummary=true, Order=110, FiveWs="FiveWs.class")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("GroupType")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.Group.GroupType> TypeElement
    {
      get { return _TypeElement; }
      set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
    }

    private Code<Hl7.Fhir.Model.Group.GroupType> _TypeElement;

    /// <summary>
    /// person | animal | practitioner | device | careteam | healthcareservice | location | organization | relatedperson | specimen
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.Group.GroupType? Type
    {
      get { return TypeElement != null ? TypeElement.Value : null; }
      set
      {
        if (value == null)
          TypeElement = null;
        else
          TypeElement = new Code<Hl7.Fhir.Model.Group.GroupType>(value);
        OnPropertyChanged("Type");
      }
    }

    /// <summary>
    /// definitional | enumerated
    /// </summary>
    [FhirElement("membership", InSummary=true, Order=120)]
    [DeclaredType(Type = typeof(Code))]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.Group.GroupMembershipBasis> MembershipElement
    {
      get { return _MembershipElement; }
      set { _MembershipElement = value; OnPropertyChanged("MembershipElement"); }
    }

    private Code<Hl7.Fhir.Model.Group.GroupMembershipBasis> _MembershipElement;

    /// <summary>
    /// definitional | enumerated
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.Group.GroupMembershipBasis? Membership
    {
      get { return MembershipElement != null ? MembershipElement.Value : null; }
      set
      {
        if (value == null)
          MembershipElement = null;
        else
          MembershipElement = new Code<Hl7.Fhir.Model.Group.GroupMembershipBasis>(value);
        OnPropertyChanged("Membership");
      }
    }

    /// <summary>
    /// Kind of Group members
    /// </summary>
    [FhirElement("code", InSummary=true, Order=130, FiveWs="FiveWs.what[x]")]
    [Binding("GroupKind")]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Code
    {
      get { return _Code; }
      set { _Code = value; OnPropertyChanged("Code"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Code;

    /// <summary>
    /// Label for Group
    /// </summary>
    [FhirElement("name", InSummary=true, Order=140)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString NameElement
    {
      get { return _NameElement; }
      set { _NameElement = value; OnPropertyChanged("NameElement"); }
    }

    private Hl7.Fhir.Model.FhirString _NameElement;

    /// <summary>
    /// Label for Group
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
    /// Natural language description of the group
    /// </summary>
    [FhirElement("description", Order=150)]
    [DataMember]
    public Hl7.Fhir.Model.Markdown DescriptionElement
    {
      get { return _DescriptionElement; }
      set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
    }

    private Hl7.Fhir.Model.Markdown _DescriptionElement;

    /// <summary>
    /// Natural language description of the group
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Description
    {
      get { return DescriptionElement != null ? DescriptionElement.Value : null; }
      set
      {
        if (value == null)
          DescriptionElement = null;
        else
          DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
        OnPropertyChanged("Description");
      }
    }

    /// <summary>
    /// Number of members
    /// </summary>
    [FhirElement("quantity", InSummary=true, Order=160)]
    [DataMember]
    public Hl7.Fhir.Model.UnsignedInt QuantityElement
    {
      get { return _QuantityElement; }
      set { _QuantityElement = value; OnPropertyChanged("QuantityElement"); }
    }

    private Hl7.Fhir.Model.UnsignedInt _QuantityElement;

    /// <summary>
    /// Number of members
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public int? Quantity
    {
      get { return QuantityElement != null ? QuantityElement.Value : null; }
      set
      {
        if (value == null)
          QuantityElement = null;
        else
          QuantityElement = new Hl7.Fhir.Model.UnsignedInt(value);
        OnPropertyChanged("Quantity");
      }
    }

    /// <summary>
    /// Entity that is the custodian of the Group's definition
    /// </summary>
    [FhirElement("managingEntity", InSummary=true, Order=170, FiveWs="FiveWs.witness")]
    [CLSCompliant(false)]
    [References("Organization","RelatedPerson","Practitioner","PractitionerRole")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference ManagingEntity
    {
      get { return _ManagingEntity; }
      set { _ManagingEntity = value; OnPropertyChanged("ManagingEntity"); }
    }

    private Hl7.Fhir.Model.ResourceReference _ManagingEntity;

    /// <summary>
    /// Include / Exclude group members by Trait
    /// </summary>
    [FhirElement("characteristic", InSummary=true, Order=180)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Group.CharacteristicComponent> Characteristic
    {
      get { if(_Characteristic==null) _Characteristic = new List<Hl7.Fhir.Model.Group.CharacteristicComponent>(); return _Characteristic; }
      set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
    }

    private List<Hl7.Fhir.Model.Group.CharacteristicComponent> _Characteristic;

    /// <summary>
    /// Who or what is in group
    /// </summary>
    [FhirElement("member", Order=190, FiveWs="FiveWs.subject")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Group.MemberComponent> Member
    {
      get { if(_Member==null) _Member = new List<Hl7.Fhir.Model.Group.MemberComponent>(); return _Member; }
      set { _Member = value; OnPropertyChanged("Member"); }
    }

    private List<Hl7.Fhir.Model.Group.MemberComponent> _Member;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as Group;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
      if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Group.GroupType>)TypeElement.DeepCopy();
      if(MembershipElement != null) dest.MembershipElement = (Code<Hl7.Fhir.Model.Group.GroupMembershipBasis>)MembershipElement.DeepCopy();
      if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
      if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
      if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
      if(QuantityElement != null) dest.QuantityElement = (Hl7.Fhir.Model.UnsignedInt)QuantityElement.DeepCopy();
      if(ManagingEntity != null) dest.ManagingEntity = (Hl7.Fhir.Model.ResourceReference)ManagingEntity.DeepCopy();
      if(Characteristic.Any()) dest.Characteristic = new List<Hl7.Fhir.Model.Group.CharacteristicComponent>(Characteristic.DeepCopy());
      if(Member.Any()) dest.Member = new List<Hl7.Fhir.Model.Group.MemberComponent>(Member.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new Group());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as Group;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
      if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
      if( !DeepComparable.Matches(MembershipElement, otherT.MembershipElement)) return false;
      if( !DeepComparable.Matches(Code, otherT.Code)) return false;
      if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
      if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.Matches(QuantityElement, otherT.QuantityElement)) return false;
      if( !DeepComparable.Matches(ManagingEntity, otherT.ManagingEntity)) return false;
      if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
      if( !DeepComparable.Matches(Member, otherT.Member)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as Group;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
      if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
      if( !DeepComparable.IsExactly(MembershipElement, otherT.MembershipElement)) return false;
      if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
      if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
      if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.IsExactly(QuantityElement, otherT.QuantityElement)) return false;
      if( !DeepComparable.IsExactly(ManagingEntity, otherT.ManagingEntity)) return false;
      if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
      if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;

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
        if (TypeElement != null) yield return TypeElement;
        if (MembershipElement != null) yield return MembershipElement;
        if (Code != null) yield return Code;
        if (NameElement != null) yield return NameElement;
        if (DescriptionElement != null) yield return DescriptionElement;
        if (QuantityElement != null) yield return QuantityElement;
        if (ManagingEntity != null) yield return ManagingEntity;
        foreach (var elem in Characteristic) { if (elem != null) yield return elem; }
        foreach (var elem in Member) { if (elem != null) yield return elem; }
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
        if (TypeElement != null) yield return new ElementValue("type", TypeElement);
        if (MembershipElement != null) yield return new ElementValue("membership", MembershipElement);
        if (Code != null) yield return new ElementValue("code", Code);
        if (NameElement != null) yield return new ElementValue("name", NameElement);
        if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
        if (QuantityElement != null) yield return new ElementValue("quantity", QuantityElement);
        if (ManagingEntity != null) yield return new ElementValue("managingEntity", ManagingEntity);
        foreach (var elem in Characteristic) { if (elem != null) yield return new ElementValue("characteristic", elem); }
        foreach (var elem in Member) { if (elem != null) yield return new ElementValue("member", elem); }
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
          value = TypeElement;
          return TypeElement is not null;
        case "membership":
          value = MembershipElement;
          return MembershipElement is not null;
        case "code":
          value = Code;
          return Code is not null;
        case "name":
          value = NameElement;
          return NameElement is not null;
        case "description":
          value = DescriptionElement;
          return DescriptionElement is not null;
        case "quantity":
          value = QuantityElement;
          return QuantityElement is not null;
        case "managingEntity":
          value = ManagingEntity;
          return ManagingEntity is not null;
        case "characteristic":
          value = Characteristic;
          return Characteristic?.Any() == true;
        case "member":
          value = Member;
          return Member?.Any() == true;
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
        case "type":
          TypeElement = (Code<Hl7.Fhir.Model.Group.GroupType>)value;
          return this;
        case "membership":
          MembershipElement = (Code<Hl7.Fhir.Model.Group.GroupMembershipBasis>)value;
          return this;
        case "code":
          Code = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "name":
          NameElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "description":
          DescriptionElement = (Hl7.Fhir.Model.Markdown)value;
          return this;
        case "quantity":
          QuantityElement = (Hl7.Fhir.Model.UnsignedInt)value;
          return this;
        case "managingEntity":
          ManagingEntity = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "characteristic":
          Characteristic = (List<Hl7.Fhir.Model.Group.CharacteristicComponent>)value;
          return this;
        case "member":
          Member = (List<Hl7.Fhir.Model.Group.MemberComponent>)value;
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
      if (TypeElement is not null) yield return new KeyValuePair<string,object>("type",TypeElement);
      if (MembershipElement is not null) yield return new KeyValuePair<string,object>("membership",MembershipElement);
      if (Code is not null) yield return new KeyValuePair<string,object>("code",Code);
      if (NameElement is not null) yield return new KeyValuePair<string,object>("name",NameElement);
      if (DescriptionElement is not null) yield return new KeyValuePair<string,object>("description",DescriptionElement);
      if (QuantityElement is not null) yield return new KeyValuePair<string,object>("quantity",QuantityElement);
      if (ManagingEntity is not null) yield return new KeyValuePair<string,object>("managingEntity",ManagingEntity);
      if (Characteristic?.Any() == true) yield return new KeyValuePair<string,object>("characteristic",Characteristic);
      if (Member?.Any() == true) yield return new KeyValuePair<string,object>("member",Member);
    }

  }

}

// end of file
