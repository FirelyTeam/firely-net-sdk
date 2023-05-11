// <auto-generated/>
// Contents of: hl7.fhir.r4.core version: 4.0.1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;

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
  /// Tracks balance, charges, for patient or cost center
  /// </summary>
  [Serializable]
  [DataContract]
  [FhirType("Account","http://hl7.org/fhir/StructureDefinition/Account", IsResource=true)]
  public partial class Account : Hl7.Fhir.Model.DomainResource
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "Account"; } }

    /// <summary>
    /// Indicates whether the account is available to be used.
    /// (url: http://hl7.org/fhir/ValueSet/account-status)
    /// (system: http://hl7.org/fhir/account-status)
    /// </summary>
    [FhirEnumeration("AccountStatus")]
    public enum AccountStatus
    {
      /// <summary>
      /// This account is active and may be used.
      /// (system: http://hl7.org/fhir/account-status)
      /// </summary>
      [EnumLiteral("active", "http://hl7.org/fhir/account-status"), Description("Active")]
      Active,
      /// <summary>
      /// This account is inactive and should not be used to track financial information.
      /// (system: http://hl7.org/fhir/account-status)
      /// </summary>
      [EnumLiteral("inactive", "http://hl7.org/fhir/account-status"), Description("Inactive")]
      Inactive,
      /// <summary>
      /// This instance should not have been part of this patient's medical record.
      /// (system: http://hl7.org/fhir/account-status)
      /// </summary>
      [EnumLiteral("entered-in-error", "http://hl7.org/fhir/account-status"), Description("Entered in error")]
      EnteredInError,
      /// <summary>
      /// This account is on hold.
      /// (system: http://hl7.org/fhir/account-status)
      /// </summary>
      [EnumLiteral("on-hold", "http://hl7.org/fhir/account-status"), Description("On Hold")]
      OnHold,
      /// <summary>
      /// The account status is unknown.
      /// (system: http://hl7.org/fhir/account-status)
      /// </summary>
      [EnumLiteral("unknown", "http://hl7.org/fhir/account-status"), Description("Unknown")]
      Unknown,
    }

    /// <summary>
    /// The party(s) that are responsible for covering the payment of this account, and what order should they be applied to the account
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Account#Coverage", IsNestedType=true)]
    [CqlType("{http://hl7.org/fhir}Account.Coverage")]
    public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Account#Coverage"; } }

      /// <summary>
      /// The party(s), such as insurances, that may contribute to the payment of this account
      /// </summary>
      [FhirElement("coverage", InSummary=true, Order=40)]
      [CLSCompliant(false)]
      [References("Coverage")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Coverage
      {
        get { return _Coverage; }
        set { _Coverage = value; OnPropertyChanged("Coverage"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Coverage;

      /// <summary>
      /// The priority of the coverage in the context of this account
      /// </summary>
      [FhirElement("priority", InSummary=true, Order=50)]
      [DataMember]
      public Hl7.Fhir.Model.PositiveInt PriorityElement
      {
        get { return _PriorityElement; }
        set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
      }

      private Hl7.Fhir.Model.PositiveInt _PriorityElement;

      /// <summary>
      /// The priority of the coverage in the context of this account
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public int? Priority
      {
        get { return PriorityElement != null ? PriorityElement.Value : null; }
        set
        {
          if (value == null)
            PriorityElement = null;
          else
            PriorityElement = new Hl7.Fhir.Model.PositiveInt(value);
          OnPropertyChanged("Priority");
        }
      }

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as CoverageComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
        if(PriorityElement != null) dest.PriorityElement = (Hl7.Fhir.Model.PositiveInt)PriorityElement.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new CoverageComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as CoverageComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
        if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as CoverageComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
        if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Coverage != null) yield return Coverage;
          if (PriorityElement != null) yield return PriorityElement;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Coverage != null) yield return new ElementValue("coverage", Coverage);
          if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "coverage":
            value = Coverage;
            return Coverage is not null;
          case "priority":
            value = PriorityElement;
            return PriorityElement is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Coverage is not null) yield return new KeyValuePair<string,object>("coverage",Coverage);
        if (PriorityElement is not null) yield return new KeyValuePair<string,object>("priority",PriorityElement);
      }

    }

    /// <summary>
    /// The parties ultimately responsible for balancing the Account
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Account#Guarantor", IsNestedType=true)]
    [CqlType("{http://hl7.org/fhir}Account.Guarantor")]
    public partial class GuarantorComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Account#Guarantor"; } }

      /// <summary>
      /// Responsible entity
      /// </summary>
      [FhirElement("party", Order=40)]
      [CLSCompliant(false)]
      [References("Patient","RelatedPerson","Organization")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Party
      {
        get { return _Party; }
        set { _Party = value; OnPropertyChanged("Party"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Party;

      /// <summary>
      /// Credit or other hold applied
      /// </summary>
      [FhirElement("onHold", Order=50)]
      [DataMember]
      public Hl7.Fhir.Model.FhirBoolean OnHoldElement
      {
        get { return _OnHoldElement; }
        set { _OnHoldElement = value; OnPropertyChanged("OnHoldElement"); }
      }

      private Hl7.Fhir.Model.FhirBoolean _OnHoldElement;

      /// <summary>
      /// Credit or other hold applied
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public bool? OnHold
      {
        get { return OnHoldElement != null ? OnHoldElement.Value : null; }
        set
        {
          if (value == null)
            OnHoldElement = null;
          else
            OnHoldElement = new Hl7.Fhir.Model.FhirBoolean(value);
          OnPropertyChanged("OnHold");
        }
      }

      /// <summary>
      /// Guarantee account during
      /// </summary>
      [FhirElement("period", Order=60)]
      [DataMember]
      public Hl7.Fhir.Model.Period Period
      {
        get { return _Period; }
        set { _Period = value; OnPropertyChanged("Period"); }
      }

      private Hl7.Fhir.Model.Period _Period;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as GuarantorComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
        if(OnHoldElement != null) dest.OnHoldElement = (Hl7.Fhir.Model.FhirBoolean)OnHoldElement.DeepCopy();
        if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new GuarantorComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as GuarantorComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Party, otherT.Party)) return false;
        if( !DeepComparable.Matches(OnHoldElement, otherT.OnHoldElement)) return false;
        if( !DeepComparable.Matches(Period, otherT.Period)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as GuarantorComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
        if( !DeepComparable.IsExactly(OnHoldElement, otherT.OnHoldElement)) return false;
        if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Party != null) yield return Party;
          if (OnHoldElement != null) yield return OnHoldElement;
          if (Period != null) yield return Period;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Party != null) yield return new ElementValue("party", Party);
          if (OnHoldElement != null) yield return new ElementValue("onHold", OnHoldElement);
          if (Period != null) yield return new ElementValue("period", Period);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "party":
            value = Party;
            return Party is not null;
          case "onHold":
            value = OnHoldElement;
            return OnHoldElement is not null;
          case "period":
            value = Period;
            return Period is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Party is not null) yield return new KeyValuePair<string,object>("party",Party);
        if (OnHoldElement is not null) yield return new KeyValuePair<string,object>("onHold",OnHoldElement);
        if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      }

    }

    /// <summary>
    /// Account number
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
    /// active | inactive | entered-in-error | on-hold | unknown
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=100, FiveWs="FiveWs.status")]
    [DeclaredType(Type = typeof(Code))]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.Account.AccountStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.Account.AccountStatus> _StatusElement;

    /// <summary>
    /// active | inactive | entered-in-error | on-hold | unknown
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.Account.AccountStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.Account.AccountStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// E.g. patient, expense, depreciation
    /// </summary>
    [FhirElement("type", InSummary=true, Order=110, FiveWs="FiveWs.class")]
    [CqlElement("type", IsPrimaryCodePath = true)]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Type
    {
      get { return _Type; }
      set { _Type = value; OnPropertyChanged("Type"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Type;

    /// <summary>
    /// Human-readable label
    /// </summary>
    [FhirElement("name", InSummary=true, Order=120, FiveWs="FiveWs.what[x]")]
    [DataMember]
    public Hl7.Fhir.Model.FhirString NameElement
    {
      get { return _NameElement; }
      set { _NameElement = value; OnPropertyChanged("NameElement"); }
    }

    private Hl7.Fhir.Model.FhirString _NameElement;

    /// <summary>
    /// Human-readable label
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
    /// The entity that caused the expenses
    /// </summary>
    [FhirElement("subject", InSummary=true, Order=130, FiveWs="FiveWs.subject")]
    [CLSCompliant(false)]
    [References("Patient","Device","Practitioner","PractitionerRole","Location","HealthcareService","Organization")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Subject
    {
      get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.ResourceReference>(); return _Subject; }
      set { _Subject = value; OnPropertyChanged("Subject"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Subject;

    /// <summary>
    /// Transaction window
    /// </summary>
    [FhirElement("servicePeriod", InSummary=true, Order=140, FiveWs="FiveWs.done[x]")]
    [DataMember]
    public Hl7.Fhir.Model.Period ServicePeriod
    {
      get { return _ServicePeriod; }
      set { _ServicePeriod = value; OnPropertyChanged("ServicePeriod"); }
    }

    private Hl7.Fhir.Model.Period _ServicePeriod;

    /// <summary>
    /// The party(s) that are responsible for covering the payment of this account, and what order should they be applied to the account
    /// </summary>
    [FhirElement("coverage", InSummary=true, Order=150)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Account.CoverageComponent> Coverage
    {
      get { if(_Coverage==null) _Coverage = new List<Hl7.Fhir.Model.Account.CoverageComponent>(); return _Coverage; }
      set { _Coverage = value; OnPropertyChanged("Coverage"); }
    }

    private List<Hl7.Fhir.Model.Account.CoverageComponent> _Coverage;

    /// <summary>
    /// Entity managing the Account
    /// </summary>
    [FhirElement("owner", InSummary=true, Order=160)]
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
    /// Explanation of purpose/use
    /// </summary>
    [FhirElement("description", InSummary=true, Order=170)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString DescriptionElement
    {
      get { return _DescriptionElement; }
      set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
    }

    private Hl7.Fhir.Model.FhirString _DescriptionElement;

    /// <summary>
    /// Explanation of purpose/use
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
          DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Description");
      }
    }

    /// <summary>
    /// The parties ultimately responsible for balancing the Account
    /// </summary>
    [FhirElement("guarantor", Order=180)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Account.GuarantorComponent> Guarantor
    {
      get { if(_Guarantor==null) _Guarantor = new List<Hl7.Fhir.Model.Account.GuarantorComponent>(); return _Guarantor; }
      set { _Guarantor = value; OnPropertyChanged("Guarantor"); }
    }

    private List<Hl7.Fhir.Model.Account.GuarantorComponent> _Guarantor;

    /// <summary>
    /// Reference to a parent Account
    /// </summary>
    [FhirElement("partOf", Order=190)]
    [CLSCompliant(false)]
    [References("Account")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference PartOf
    {
      get { return _PartOf; }
      set { _PartOf = value; OnPropertyChanged("PartOf"); }
    }

    private Hl7.Fhir.Model.ResourceReference _PartOf;

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as Account;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Account.AccountStatus>)StatusElement.DeepCopy();
      if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
      if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
      if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
      if(ServicePeriod != null) dest.ServicePeriod = (Hl7.Fhir.Model.Period)ServicePeriod.DeepCopy();
      if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.Account.CoverageComponent>(Coverage.DeepCopy());
      if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
      if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
      if(Guarantor != null) dest.Guarantor = new List<Hl7.Fhir.Model.Account.GuarantorComponent>(Guarantor.DeepCopy());
      if(PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new Account());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as Account;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(Type, otherT.Type)) return false;
      if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
      if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
      if( !DeepComparable.Matches(ServicePeriod, otherT.ServicePeriod)) return false;
      if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
      if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
      if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.Matches(Guarantor, otherT.Guarantor)) return false;
      if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as Account;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
      if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
      if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
      if( !DeepComparable.IsExactly(ServicePeriod, otherT.ServicePeriod)) return false;
      if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
      if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
      if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
      if( !DeepComparable.IsExactly(Guarantor, otherT.Guarantor)) return false;
      if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;

      return true;
    }

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        foreach (var elem in Identifier) { if (elem != null) yield return elem; }
        if (StatusElement != null) yield return StatusElement;
        if (Type != null) yield return Type;
        if (NameElement != null) yield return NameElement;
        foreach (var elem in Subject) { if (elem != null) yield return elem; }
        if (ServicePeriod != null) yield return ServicePeriod;
        foreach (var elem in Coverage) { if (elem != null) yield return elem; }
        if (Owner != null) yield return Owner;
        if (DescriptionElement != null) yield return DescriptionElement;
        foreach (var elem in Guarantor) { if (elem != null) yield return elem; }
        if (PartOf != null) yield return PartOf;
      }
    }

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
        if (StatusElement != null) yield return new ElementValue("status", StatusElement);
        if (Type != null) yield return new ElementValue("type", Type);
        if (NameElement != null) yield return new ElementValue("name", NameElement);
        foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
        if (ServicePeriod != null) yield return new ElementValue("servicePeriod", ServicePeriod);
        foreach (var elem in Coverage) { if (elem != null) yield return new ElementValue("coverage", elem); }
        if (Owner != null) yield return new ElementValue("owner", Owner);
        if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
        foreach (var elem in Guarantor) { if (elem != null) yield return new ElementValue("guarantor", elem); }
        if (PartOf != null) yield return new ElementValue("partOf", PartOf);
      }
    }

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "identifier":
          value = Identifier;
          return Identifier?.Any() == true;
        case "status":
          value = StatusElement;
          return StatusElement is not null;
        case "type":
          value = Type;
          return Type is not null;
        case "name":
          value = NameElement;
          return NameElement is not null;
        case "subject":
          value = Subject;
          return Subject?.Any() == true;
        case "servicePeriod":
          value = ServicePeriod;
          return ServicePeriod is not null;
        case "coverage":
          value = Coverage;
          return Coverage?.Any() == true;
        case "owner":
          value = Owner;
          return Owner is not null;
        case "description":
          value = DescriptionElement;
          return DescriptionElement is not null;
        case "guarantor":
          value = Guarantor;
          return Guarantor?.Any() == true;
        case "partOf":
          value = PartOf;
          return PartOf is not null;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (Identifier?.Any() == true) yield return new KeyValuePair<string,object>("identifier",Identifier);
      if (StatusElement is not null) yield return new KeyValuePair<string,object>("status",StatusElement);
      if (Type is not null) yield return new KeyValuePair<string,object>("type",Type);
      if (NameElement is not null) yield return new KeyValuePair<string,object>("name",NameElement);
      if (Subject?.Any() == true) yield return new KeyValuePair<string,object>("subject",Subject);
      if (ServicePeriod is not null) yield return new KeyValuePair<string,object>("servicePeriod",ServicePeriod);
      if (Coverage?.Any() == true) yield return new KeyValuePair<string,object>("coverage",Coverage);
      if (Owner is not null) yield return new KeyValuePair<string,object>("owner",Owner);
      if (DescriptionElement is not null) yield return new KeyValuePair<string,object>("description",DescriptionElement);
      if (Guarantor?.Any() == true) yield return new KeyValuePair<string,object>("guarantor",Guarantor);
      if (PartOf is not null) yield return new KeyValuePair<string,object>("partOf",PartOf);
    }

  }

}

// end of file
