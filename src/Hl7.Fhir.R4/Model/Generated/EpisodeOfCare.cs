// <auto-generated/>
// Contents of: hl7.fhir.r4.expansions@4.0.1, hl7.fhir.r4.core@4.0.1

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
  /// An association of a Patient with an Organization and  Healthcare Provider(s) for a period of time that the Organization assumes some level of responsibility
  /// </summary>
  /// <remarks>
  /// An association between a patient and an organization / healthcare provider(s) during which time encounters may occur. The managing organization assumes a level of responsibility for the patient during this time.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("EpisodeOfCare","http://hl7.org/fhir/StructureDefinition/EpisodeOfCare")]
  public partial class EpisodeOfCare : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>, ICoded<List<Hl7.Fhir.Model.CodeableConcept>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "EpisodeOfCare"; } }

    /// <summary>
    /// The status of the episode of care.
    /// (url: http://hl7.org/fhir/ValueSet/episode-of-care-status)
    /// (system: http://hl7.org/fhir/episode-of-care-status)
    /// </summary>
    [FhirEnumeration("EpisodeOfCareStatus", "http://hl7.org/fhir/ValueSet/episode-of-care-status", "http://hl7.org/fhir/episode-of-care-status")]
    public enum EpisodeOfCareStatus
    {
      /// <summary>
      /// This episode of care is planned to start at the date specified in the period.start. During this status, an organization may perform assessments to determine if the patient is eligible to receive services, or be organizing to make resources available to provide care services.
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("planned"), Description("Planned")]
      Planned,
      /// <summary>
      /// This episode has been placed on a waitlist, pending the episode being made active (or cancelled).
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("waitlist"), Description("Waitlist")]
      Waitlist,
      /// <summary>
      /// This episode of care is current.
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("active"), Description("Active")]
      Active,
      /// <summary>
      /// This episode of care is on hold; the organization has limited responsibility for the patient (such as while on respite).
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("onhold"), Description("On Hold")]
      Onhold,
      /// <summary>
      /// This episode of care is finished and the organization is not expecting to be providing further care to the patient. Can also be known as \"closed\", \"completed\" or other similar terms.
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("finished"), Description("Finished")]
      Finished,
      /// <summary>
      /// The episode of care was cancelled, or withdrawn from service, often selected during the planned stage as the patient may have gone elsewhere, or the circumstances have changed and the organization is unable to provide the care. It indicates that services terminated outside the planned/expected workflow.
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("cancelled"), Description("Cancelled")]
      Cancelled,
      /// <summary>
      /// This instance should not have been part of this patient's medical record.
      /// (system: http://hl7.org/fhir/episode-of-care-status)
      /// </summary>
      [EnumLiteral("entered-in-error"), Description("Entered in Error")]
      EnteredInError,
    }

    /// <summary>
    /// Past list of status codes (the current status may be included to cover the start date of the status)
    /// </summary>
    /// <remarks>
    /// The history of statuses that the EpisodeOfCare has been through (without requiring processing the history of the resource).
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("EpisodeOfCare.statusHistory", IsBackboneType=true)]
    public partial class StatusHistoryComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "EpisodeOfCare.statusHistory"; } }

      /// <summary>
      /// planned | waitlist | active | onhold | finished | cancelled | entered-in-error
      /// </summary>
      [FhirElement("status", Order=40)]
      [DeclaredType(Type = typeof(Code))]
      [Binding("EpisodeOfCareStatus")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus> StatusElement
      {
        get { return _StatusElement; }
        set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
      }

      private Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus> _StatusElement;

      /// <summary>
      /// planned | waitlist | active | onhold | finished | cancelled | entered-in-error
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus? Status
      {
        get { return StatusElement != null ? StatusElement.Value : null; }
        set
        {
          if (value == null)
            StatusElement = null;
          else
            StatusElement = new Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus>(value);
          OnPropertyChanged("Status");
        }
      }

      /// <summary>
      /// Duration the EpisodeOfCare was in the specified status
      /// </summary>
      [FhirElement("period", Order=50)]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.Period Period
      {
        get { return _Period; }
        set { _Period = value; OnPropertyChanged("Period"); }
      }

      private Hl7.Fhir.Model.Period _Period;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as StatusHistoryComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus>)StatusElement.DeepCopy();
        if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new StatusHistoryComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as StatusHistoryComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
        if( !DeepComparable.Matches(Period, otherT.Period)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as StatusHistoryComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
        if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (StatusElement != null) yield return StatusElement;
          if (Period != null) yield return Period;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (StatusElement != null) yield return new ElementValue("status", StatusElement);
          if (Period != null) yield return new ElementValue("period", Period);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "status":
            value = StatusElement;
            return StatusElement is not null;
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
          case "status":
            StatusElement = (Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus>)value;
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
        if (StatusElement is not null) yield return new KeyValuePair<string,object>("status",StatusElement);
        if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      }

    }

    /// <summary>
    /// The list of diagnosis relevant to this episode of care
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("EpisodeOfCare.diagnosis", IsBackboneType=true)]
    public partial class DiagnosisComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "EpisodeOfCare.diagnosis"; } }

      /// <summary>
      /// Conditions/problems/diagnoses this episode of care is for
      /// </summary>
      [FhirElement("condition", InSummary=true, Order=40, FiveWs="FiveWs.what[x]")]
      [CLSCompliant(false)]
      [References("Condition")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Condition
      {
        get { return _Condition; }
        set { _Condition = value; OnPropertyChanged("Condition"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Condition;

      /// <summary>
      /// Role that this diagnosis has within the episode of care (e.g. admission, billing, discharge …)
      /// </summary>
      [FhirElement("role", InSummary=true, Order=50)]
      [Binding("DiagnosisRole")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Role
      {
        get { return _Role; }
        set { _Role = value; OnPropertyChanged("Role"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Role;

      /// <summary>
      /// Ranking of the diagnosis (for each role type)
      /// </summary>
      [FhirElement("rank", InSummary=true, Order=60)]
      [DataMember]
      public Hl7.Fhir.Model.PositiveInt RankElement
      {
        get { return _RankElement; }
        set { _RankElement = value; OnPropertyChanged("RankElement"); }
      }

      private Hl7.Fhir.Model.PositiveInt _RankElement;

      /// <summary>
      /// Ranking of the diagnosis (for each role type)
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public int? Rank
      {
        get { return RankElement != null ? RankElement.Value : null; }
        set
        {
          if (value == null)
            RankElement = null;
          else
            RankElement = new Hl7.Fhir.Model.PositiveInt(value);
          OnPropertyChanged("Rank");
        }
      }

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as DiagnosisComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Condition != null) dest.Condition = (Hl7.Fhir.Model.ResourceReference)Condition.DeepCopy();
        if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
        if(RankElement != null) dest.RankElement = (Hl7.Fhir.Model.PositiveInt)RankElement.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new DiagnosisComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as DiagnosisComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
        if( !DeepComparable.Matches(Role, otherT.Role)) return false;
        if( !DeepComparable.Matches(RankElement, otherT.RankElement)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as DiagnosisComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
        if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
        if( !DeepComparable.IsExactly(RankElement, otherT.RankElement)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Condition != null) yield return Condition;
          if (Role != null) yield return Role;
          if (RankElement != null) yield return RankElement;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Condition != null) yield return new ElementValue("condition", Condition);
          if (Role != null) yield return new ElementValue("role", Role);
          if (RankElement != null) yield return new ElementValue("rank", RankElement);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "condition":
            value = Condition;
            return Condition is not null;
          case "role":
            value = Role;
            return Role is not null;
          case "rank":
            value = RankElement;
            return RankElement is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "condition":
            Condition = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          case "role":
            Role = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "rank":
            RankElement = (Hl7.Fhir.Model.PositiveInt)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Condition is not null) yield return new KeyValuePair<string,object>("condition",Condition);
        if (Role is not null) yield return new KeyValuePair<string,object>("role",Role);
        if (RankElement is not null) yield return new KeyValuePair<string,object>("rank",RankElement);
      }

    }

    /// <summary>
    /// Business Identifier(s) relevant for this EpisodeOfCare
    /// </summary>
    [FhirElement("identifier", Order=90, FiveWs="FiveWs.identifier")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Identifier> Identifier
    {
      get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
      set { _Identifier = value; OnPropertyChanged("Identifier"); }
    }

    private List<Hl7.Fhir.Model.Identifier> _Identifier;

    /// <summary>
    /// planned | waitlist | active | onhold | finished | cancelled | entered-in-error
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=100, FiveWs="FiveWs.status")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("EpisodeOfCareStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus> _StatusElement;

    /// <summary>
    /// planned | waitlist | active | onhold | finished | cancelled | entered-in-error
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// Past list of status codes (the current status may be included to cover the start date of the status)
    /// </summary>
    [FhirElement("statusHistory", Order=110)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.EpisodeOfCare.StatusHistoryComponent> StatusHistory
    {
      get { if(_StatusHistory==null) _StatusHistory = new List<Hl7.Fhir.Model.EpisodeOfCare.StatusHistoryComponent>(); return _StatusHistory; }
      set { _StatusHistory = value; OnPropertyChanged("StatusHistory"); }
    }

    private List<Hl7.Fhir.Model.EpisodeOfCare.StatusHistoryComponent> _StatusHistory;

    /// <summary>
    /// Type/class  - e.g. specialist referral, disease management
    /// </summary>
    [FhirElement("type", InSummary=true, Order=120, FiveWs="FiveWs.class")]
    [Binding("EpisodeOfCareType")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CodeableConcept> Type
    {
      get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
      set { _Type = value; OnPropertyChanged("Type"); }
    }

    private List<Hl7.Fhir.Model.CodeableConcept> _Type;

    /// <summary>
    /// The list of diagnosis relevant to this episode of care
    /// </summary>
    [FhirElement("diagnosis", InSummary=true, Order=130)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.EpisodeOfCare.DiagnosisComponent> Diagnosis
    {
      get { if(_Diagnosis==null) _Diagnosis = new List<Hl7.Fhir.Model.EpisodeOfCare.DiagnosisComponent>(); return _Diagnosis; }
      set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
    }

    private List<Hl7.Fhir.Model.EpisodeOfCare.DiagnosisComponent> _Diagnosis;

    /// <summary>
    /// The patient who is the focus of this episode of care
    /// </summary>
    [FhirElement("patient", InSummary=true, Order=140, FiveWs="FiveWs.subject")]
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
    /// Organization that assumes care
    /// </summary>
    [FhirElement("managingOrganization", InSummary=true, Order=150)]
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
    /// Interval during responsibility is assumed
    /// </summary>
    [FhirElement("period", InSummary=true, Order=160, FiveWs="FiveWs.init")]
    [DataMember]
    public Hl7.Fhir.Model.Period Period
    {
      get { return _Period; }
      set { _Period = value; OnPropertyChanged("Period"); }
    }

    private Hl7.Fhir.Model.Period _Period;

    /// <summary>
    /// Originating Referral Request(s)
    /// </summary>
    [FhirElement("referralRequest", Order=170)]
    [CLSCompliant(false)]
    [References("ServiceRequest")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> ReferralRequest
    {
      get { if(_ReferralRequest==null) _ReferralRequest = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReferralRequest; }
      set { _ReferralRequest = value; OnPropertyChanged("ReferralRequest"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _ReferralRequest;

    /// <summary>
    /// Care manager/care coordinator for the patient
    /// </summary>
    [FhirElement("careManager", Order=180)]
    [CLSCompliant(false)]
    [References("Practitioner","PractitionerRole")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference CareManager
    {
      get { return _CareManager; }
      set { _CareManager = value; OnPropertyChanged("CareManager"); }
    }

    private Hl7.Fhir.Model.ResourceReference _CareManager;

    /// <summary>
    /// Other practitioners facilitating this episode of care
    /// </summary>
    [FhirElement("team", Order=190)]
    [CLSCompliant(false)]
    [References("CareTeam")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Team
    {
      get { if(_Team==null) _Team = new List<Hl7.Fhir.Model.ResourceReference>(); return _Team; }
      set { _Team = value; OnPropertyChanged("Team"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Team;

    /// <summary>
    /// The set of accounts that may be used for billing for this EpisodeOfCare
    /// </summary>
    [FhirElement("account", Order=200)]
    [CLSCompliant(false)]
    [References("Account")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Account
    {
      get { if(_Account==null) _Account = new List<Hl7.Fhir.Model.ResourceReference>(); return _Account; }
      set { _Account = value; OnPropertyChanged("Account"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Account;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    List<Hl7.Fhir.Model.CodeableConcept> ICoded<List<Hl7.Fhir.Model.CodeableConcept>>.Code { get => Type; set => Type = value; }
    IEnumerable<Coding> ICoded.ToCodings() => Type.ToCodings();

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as EpisodeOfCare;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus>)StatusElement.DeepCopy();
      if(StatusHistory.Any()) dest.StatusHistory = new List<Hl7.Fhir.Model.EpisodeOfCare.StatusHistoryComponent>(StatusHistory.DeepCopy());
      if(Type.Any()) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
      if(Diagnosis.Any()) dest.Diagnosis = new List<Hl7.Fhir.Model.EpisodeOfCare.DiagnosisComponent>(Diagnosis.DeepCopy());
      if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
      if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
      if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
      if(ReferralRequest.Any()) dest.ReferralRequest = new List<Hl7.Fhir.Model.ResourceReference>(ReferralRequest.DeepCopy());
      if(CareManager != null) dest.CareManager = (Hl7.Fhir.Model.ResourceReference)CareManager.DeepCopy();
      if(Team.Any()) dest.Team = new List<Hl7.Fhir.Model.ResourceReference>(Team.DeepCopy());
      if(Account.Any()) dest.Account = new List<Hl7.Fhir.Model.ResourceReference>(Account.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new EpisodeOfCare());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as EpisodeOfCare;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(StatusHistory, otherT.StatusHistory)) return false;
      if( !DeepComparable.Matches(Type, otherT.Type)) return false;
      if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
      if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
      if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
      if( !DeepComparable.Matches(Period, otherT.Period)) return false;
      if( !DeepComparable.Matches(ReferralRequest, otherT.ReferralRequest)) return false;
      if( !DeepComparable.Matches(CareManager, otherT.CareManager)) return false;
      if( !DeepComparable.Matches(Team, otherT.Team)) return false;
      if( !DeepComparable.Matches(Account, otherT.Account)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as EpisodeOfCare;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(StatusHistory, otherT.StatusHistory)) return false;
      if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
      if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
      if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
      if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
      if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
      if( !DeepComparable.IsExactly(ReferralRequest, otherT.ReferralRequest)) return false;
      if( !DeepComparable.IsExactly(CareManager, otherT.CareManager)) return false;
      if( !DeepComparable.IsExactly(Team, otherT.Team)) return false;
      if( !DeepComparable.IsExactly(Account, otherT.Account)) return false;

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
        foreach (var elem in StatusHistory) { if (elem != null) yield return elem; }
        foreach (var elem in Type) { if (elem != null) yield return elem; }
        foreach (var elem in Diagnosis) { if (elem != null) yield return elem; }
        if (Patient != null) yield return Patient;
        if (ManagingOrganization != null) yield return ManagingOrganization;
        if (Period != null) yield return Period;
        foreach (var elem in ReferralRequest) { if (elem != null) yield return elem; }
        if (CareManager != null) yield return CareManager;
        foreach (var elem in Team) { if (elem != null) yield return elem; }
        foreach (var elem in Account) { if (elem != null) yield return elem; }
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
        foreach (var elem in StatusHistory) { if (elem != null) yield return new ElementValue("statusHistory", elem); }
        foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
        foreach (var elem in Diagnosis) { if (elem != null) yield return new ElementValue("diagnosis", elem); }
        if (Patient != null) yield return new ElementValue("patient", Patient);
        if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
        if (Period != null) yield return new ElementValue("period", Period);
        foreach (var elem in ReferralRequest) { if (elem != null) yield return new ElementValue("referralRequest", elem); }
        if (CareManager != null) yield return new ElementValue("careManager", CareManager);
        foreach (var elem in Team) { if (elem != null) yield return new ElementValue("team", elem); }
        foreach (var elem in Account) { if (elem != null) yield return new ElementValue("account", elem); }
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
        case "statusHistory":
          value = StatusHistory;
          return StatusHistory?.Any() == true;
        case "type":
          value = Type;
          return Type?.Any() == true;
        case "diagnosis":
          value = Diagnosis;
          return Diagnosis?.Any() == true;
        case "patient":
          value = Patient;
          return Patient is not null;
        case "managingOrganization":
          value = ManagingOrganization;
          return ManagingOrganization is not null;
        case "period":
          value = Period;
          return Period is not null;
        case "referralRequest":
          value = ReferralRequest;
          return ReferralRequest?.Any() == true;
        case "careManager":
          value = CareManager;
          return CareManager is not null;
        case "team":
          value = Team;
          return Team?.Any() == true;
        case "account":
          value = Account;
          return Account?.Any() == true;
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
        case "status":
          StatusElement = (Code<Hl7.Fhir.Model.EpisodeOfCare.EpisodeOfCareStatus>)value;
          return this;
        case "statusHistory":
          StatusHistory = (List<Hl7.Fhir.Model.EpisodeOfCare.StatusHistoryComponent>)value;
          return this;
        case "type":
          Type = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "diagnosis":
          Diagnosis = (List<Hl7.Fhir.Model.EpisodeOfCare.DiagnosisComponent>)value;
          return this;
        case "patient":
          Patient = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "managingOrganization":
          ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "period":
          Period = (Hl7.Fhir.Model.Period)value;
          return this;
        case "referralRequest":
          ReferralRequest = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "careManager":
          CareManager = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "team":
          Team = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "account":
          Account = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (Identifier?.Any() == true) yield return new KeyValuePair<string,object>("identifier",Identifier);
      if (StatusElement is not null) yield return new KeyValuePair<string,object>("status",StatusElement);
      if (StatusHistory?.Any() == true) yield return new KeyValuePair<string,object>("statusHistory",StatusHistory);
      if (Type?.Any() == true) yield return new KeyValuePair<string,object>("type",Type);
      if (Diagnosis?.Any() == true) yield return new KeyValuePair<string,object>("diagnosis",Diagnosis);
      if (Patient is not null) yield return new KeyValuePair<string,object>("patient",Patient);
      if (ManagingOrganization is not null) yield return new KeyValuePair<string,object>("managingOrganization",ManagingOrganization);
      if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      if (ReferralRequest?.Any() == true) yield return new KeyValuePair<string,object>("referralRequest",ReferralRequest);
      if (CareManager is not null) yield return new KeyValuePair<string,object>("careManager",CareManager);
      if (Team?.Any() == true) yield return new KeyValuePair<string,object>("team",Team);
      if (Account?.Any() == true) yield return new KeyValuePair<string,object>("account",Account);
    }

  }

}

// end of file
