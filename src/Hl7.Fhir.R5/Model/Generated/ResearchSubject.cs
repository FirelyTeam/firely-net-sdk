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
  /// Participant or object which is the recipient of investigative activities in a study
  /// </summary>
  /// <remarks>
  /// A ResearchSubject is a participant or object which is the recipient of investigative activities in a research study.
  /// Need to make sure we encompass public health studies.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("ResearchSubject","http://hl7.org/fhir/StructureDefinition/ResearchSubject")]
  public partial class ResearchSubject : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "ResearchSubject"; } }

    /// <summary>
    /// Indicates the progression of a study subject through a study.
    /// (url: http://hl7.org/fhir/ValueSet/research-subject-state)
    /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
    /// </summary>
    [FhirEnumeration("ResearchSubjectState", "http://hl7.org/fhir/ValueSet/research-subject-state", "http://terminology.hl7.org/CodeSystem/research-subject-state")]
    public enum ResearchSubjectState
    {
      /// <summary>
      /// Candidate
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("candidate"), Description("Candidate")]
      Candidate,
      /// <summary>
      /// Eligible
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("eligible"), Description("Eligible")]
      Eligible,
      /// <summary>
      /// Follow-up
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("follow-up"), Description("Follow-up")]
      FollowUp,
      /// <summary>
      /// Ineligible
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("ineligible"), Description("Ineligible")]
      Ineligible,
      /// <summary>
      /// Not Registered
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("not-registered"), Description("Not Registered")]
      NotRegistered,
      /// <summary>
      /// Off-study
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("off-study"), Description("Off-study")]
      OffStudy,
      /// <summary>
      /// On-study
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("on-study"), Description("On-study")]
      OnStudy,
      /// <summary>
      /// On-study-intervention
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("on-study-intervention"), Description("On-study-intervention")]
      OnStudyIntervention,
      /// <summary>
      /// On-study-observation
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("on-study-observation"), Description("On-study-observation")]
      OnStudyObservation,
      /// <summary>
      /// Pending on-study
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("pending-on-study"), Description("Pending on-study")]
      PendingOnStudy,
      /// <summary>
      /// Potential Candidate
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("potential-candidate"), Description("Potential Candidate")]
      PotentialCandidate,
      /// <summary>
      /// Screening
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("screening"), Description("Screening")]
      Screening,
      /// <summary>
      /// Withdrawn
      /// (system: http://terminology.hl7.org/CodeSystem/research-subject-state)
      /// </summary>
      [EnumLiteral("withdrawn"), Description("Withdrawn")]
      Withdrawn,
    }

    /// <summary>
    /// Subject status
    /// </summary>
    /// <remarks>
    /// The current state (status) of the subject and resons for status change where appropriate.
    /// This is intended to deal with the confusion routinely created by haing two conflated concepts of being in a particular state and having achieved a particular milestone.  In strict terms a milestone is a point of time event that results in a change from one state to another.  The state before the milestone is achieved is often given the same name as the milestone, and sometimes the state may have the same description.  For instance "Randomised" and "Visit 1" may be different milestones but the state remains at "on study" after each of them. 
    /// It is likely that more than one "state" pattern will be recorded for a subject and a type has been introduced to allow this simultaneous recording.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("ResearchSubject#Progress")]
    [BackboneType("ResearchSubject.progress")]
    public partial class ProgressComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "ResearchSubject#Progress"; } }

      /// <summary>
      /// state | milestone
      /// </summary>
      [FhirElement("type", Order=40)]
      [Binding("ResearchSubjectStateType")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Type
      {
        get { return _Type; }
        set { _Type = value; OnPropertyChanged("Type"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Type;

      /// <summary>
      /// candidate | eligible | follow-up | ineligible | not-registered | off-study | on-study | on-study-intervention | on-study-observation | pending-on-study | potential-candidate | screening | withdrawn
      /// </summary>
      [FhirElement("subjectState", Order=50, FiveWs="FiveWs.status")]
      [Binding("ResearchSubjectProgresss")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept SubjectState
      {
        get { return _SubjectState; }
        set { _SubjectState = value; OnPropertyChanged("SubjectState"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _SubjectState;

      /// <summary>
      /// SignedUp | Screened | Randomized
      /// </summary>
      [FhirElement("milestone", Order=60, FiveWs="FiveWs.status")]
      [Binding("ResearchSubjectMilestone")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Milestone
      {
        get { return _Milestone; }
        set { _Milestone = value; OnPropertyChanged("Milestone"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Milestone;

      /// <summary>
      /// State change reason
      /// </summary>
      [FhirElement("reason", Order=70)]
      [Binding("StateChangeReason")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Reason
      {
        get { return _Reason; }
        set { _Reason = value; OnPropertyChanged("Reason"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Reason;

      /// <summary>
      /// State change date
      /// </summary>
      [FhirElement("startDate", Order=80)]
      [DataMember]
      public Hl7.Fhir.Model.FhirDateTime StartDateElement
      {
        get { return _StartDateElement; }
        set { _StartDateElement = value; OnPropertyChanged("StartDateElement"); }
      }

      private Hl7.Fhir.Model.FhirDateTime _StartDateElement;

      /// <summary>
      /// State change date
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public string StartDate
      {
        get { return StartDateElement != null ? StartDateElement.Value : null; }
        set
        {
          if (value == null)
            StartDateElement = null;
          else
            StartDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
          OnPropertyChanged("StartDate");
        }
      }

      /// <summary>
      /// State change date
      /// </summary>
      [FhirElement("endDate", Order=90)]
      [DataMember]
      public Hl7.Fhir.Model.FhirDateTime EndDateElement
      {
        get { return _EndDateElement; }
        set { _EndDateElement = value; OnPropertyChanged("EndDateElement"); }
      }

      private Hl7.Fhir.Model.FhirDateTime _EndDateElement;

      /// <summary>
      /// State change date
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public string EndDate
      {
        get { return EndDateElement != null ? EndDateElement.Value : null; }
        set
        {
          if (value == null)
            EndDateElement = null;
          else
            EndDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
          OnPropertyChanged("EndDate");
        }
      }

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as ProgressComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
        if(SubjectState != null) dest.SubjectState = (Hl7.Fhir.Model.CodeableConcept)SubjectState.DeepCopy();
        if(Milestone != null) dest.Milestone = (Hl7.Fhir.Model.CodeableConcept)Milestone.DeepCopy();
        if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
        if(StartDateElement != null) dest.StartDateElement = (Hl7.Fhir.Model.FhirDateTime)StartDateElement.DeepCopy();
        if(EndDateElement != null) dest.EndDateElement = (Hl7.Fhir.Model.FhirDateTime)EndDateElement.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new ProgressComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as ProgressComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Type, otherT.Type)) return false;
        if( !DeepComparable.Matches(SubjectState, otherT.SubjectState)) return false;
        if( !DeepComparable.Matches(Milestone, otherT.Milestone)) return false;
        if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
        if( !DeepComparable.Matches(StartDateElement, otherT.StartDateElement)) return false;
        if( !DeepComparable.Matches(EndDateElement, otherT.EndDateElement)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as ProgressComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
        if( !DeepComparable.IsExactly(SubjectState, otherT.SubjectState)) return false;
        if( !DeepComparable.IsExactly(Milestone, otherT.Milestone)) return false;
        if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
        if( !DeepComparable.IsExactly(StartDateElement, otherT.StartDateElement)) return false;
        if( !DeepComparable.IsExactly(EndDateElement, otherT.EndDateElement)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Type != null) yield return Type;
          if (SubjectState != null) yield return SubjectState;
          if (Milestone != null) yield return Milestone;
          if (Reason != null) yield return Reason;
          if (StartDateElement != null) yield return StartDateElement;
          if (EndDateElement != null) yield return EndDateElement;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Type != null) yield return new ElementValue("type", Type);
          if (SubjectState != null) yield return new ElementValue("subjectState", SubjectState);
          if (Milestone != null) yield return new ElementValue("milestone", Milestone);
          if (Reason != null) yield return new ElementValue("reason", Reason);
          if (StartDateElement != null) yield return new ElementValue("startDate", StartDateElement);
          if (EndDateElement != null) yield return new ElementValue("endDate", EndDateElement);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "type":
            value = Type;
            return Type is not null;
          case "subjectState":
            value = SubjectState;
            return SubjectState is not null;
          case "milestone":
            value = Milestone;
            return Milestone is not null;
          case "reason":
            value = Reason;
            return Reason is not null;
          case "startDate":
            value = StartDateElement;
            return StartDateElement is not null;
          case "endDate":
            value = EndDateElement;
            return EndDateElement is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "type":
            Type = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "subjectState":
            SubjectState = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "milestone":
            Milestone = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "reason":
            Reason = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "startDate":
            StartDateElement = (Hl7.Fhir.Model.FhirDateTime)value;
            return this;
          case "endDate":
            EndDateElement = (Hl7.Fhir.Model.FhirDateTime)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Type is not null) yield return new KeyValuePair<string,object>("type",Type);
        if (SubjectState is not null) yield return new KeyValuePair<string,object>("subjectState",SubjectState);
        if (Milestone is not null) yield return new KeyValuePair<string,object>("milestone",Milestone);
        if (Reason is not null) yield return new KeyValuePair<string,object>("reason",Reason);
        if (StartDateElement is not null) yield return new KeyValuePair<string,object>("startDate",StartDateElement);
        if (EndDateElement is not null) yield return new KeyValuePair<string,object>("endDate",EndDateElement);
      }

    }

    /// <summary>
    /// Business Identifier for research subject in a study
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
    /// draft | active | retired | unknown
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=100, FiveWs="FiveWs.status")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("PublicationStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;

    /// <summary>
    /// draft | active | retired | unknown
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.PublicationStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// Subject status
    /// </summary>
    [FhirElement("progress", Order=110)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResearchSubject.ProgressComponent> Progress
    {
      get { if(_Progress==null) _Progress = new List<Hl7.Fhir.Model.ResearchSubject.ProgressComponent>(); return _Progress; }
      set { _Progress = value; OnPropertyChanged("Progress"); }
    }

    private List<Hl7.Fhir.Model.ResearchSubject.ProgressComponent> _Progress;

    /// <summary>
    /// Start and end of participation
    /// </summary>
    [FhirElement("period", InSummary=true, Order=120)]
    [DataMember]
    public Hl7.Fhir.Model.Period Period
    {
      get { return _Period; }
      set { _Period = value; OnPropertyChanged("Period"); }
    }

    private Hl7.Fhir.Model.Period _Period;

    /// <summary>
    /// Study subject is part of
    /// </summary>
    [FhirElement("study", InSummary=true, Order=130)]
    [CLSCompliant(false)]
    [References("ResearchStudy")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Study
    {
      get { return _Study; }
      set { _Study = value; OnPropertyChanged("Study"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Study;

    /// <summary>
    /// Who or what is part of study
    /// </summary>
    [FhirElement("subject", InSummary=true, Order=140)]
    [CLSCompliant(false)]
    [References("Patient","Group","Specimen","Device","Medication","Substance","BiologicallyDerivedProduct")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Subject
    {
      get { return _Subject; }
      set { _Subject = value; OnPropertyChanged("Subject"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Subject;

    /// <summary>
    /// What path should be followed
    /// </summary>
    [FhirElement("assignedComparisonGroup", Order=150)]
    [DataMember]
    public Hl7.Fhir.Model.Id AssignedComparisonGroupElement
    {
      get { return _AssignedComparisonGroupElement; }
      set { _AssignedComparisonGroupElement = value; OnPropertyChanged("AssignedComparisonGroupElement"); }
    }

    private Hl7.Fhir.Model.Id _AssignedComparisonGroupElement;

    /// <summary>
    /// What path should be followed
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string AssignedComparisonGroup
    {
      get { return AssignedComparisonGroupElement != null ? AssignedComparisonGroupElement.Value : null; }
      set
      {
        if (value == null)
          AssignedComparisonGroupElement = null;
        else
          AssignedComparisonGroupElement = new Hl7.Fhir.Model.Id(value);
        OnPropertyChanged("AssignedComparisonGroup");
      }
    }

    /// <summary>
    /// What path was followed
    /// </summary>
    [FhirElement("actualComparisonGroup", Order=160)]
    [DataMember]
    public Hl7.Fhir.Model.Id ActualComparisonGroupElement
    {
      get { return _ActualComparisonGroupElement; }
      set { _ActualComparisonGroupElement = value; OnPropertyChanged("ActualComparisonGroupElement"); }
    }

    private Hl7.Fhir.Model.Id _ActualComparisonGroupElement;

    /// <summary>
    /// What path was followed
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string ActualComparisonGroup
    {
      get { return ActualComparisonGroupElement != null ? ActualComparisonGroupElement.Value : null; }
      set
      {
        if (value == null)
          ActualComparisonGroupElement = null;
        else
          ActualComparisonGroupElement = new Hl7.Fhir.Model.Id(value);
        OnPropertyChanged("ActualComparisonGroup");
      }
    }

    /// <summary>
    /// Agreement to participate in study
    /// </summary>
    [FhirElement("consent", Order=170)]
    [CLSCompliant(false)]
    [References("Consent")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Consent
    {
      get { if(_Consent==null) _Consent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Consent; }
      set { _Consent = value; OnPropertyChanged("Consent"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Consent;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as ResearchSubject;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
      if(Progress.Any()) dest.Progress = new List<Hl7.Fhir.Model.ResearchSubject.ProgressComponent>(Progress.DeepCopy());
      if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
      if(Study != null) dest.Study = (Hl7.Fhir.Model.ResourceReference)Study.DeepCopy();
      if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
      if(AssignedComparisonGroupElement != null) dest.AssignedComparisonGroupElement = (Hl7.Fhir.Model.Id)AssignedComparisonGroupElement.DeepCopy();
      if(ActualComparisonGroupElement != null) dest.ActualComparisonGroupElement = (Hl7.Fhir.Model.Id)ActualComparisonGroupElement.DeepCopy();
      if(Consent.Any()) dest.Consent = new List<Hl7.Fhir.Model.ResourceReference>(Consent.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new ResearchSubject());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as ResearchSubject;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(Progress, otherT.Progress)) return false;
      if( !DeepComparable.Matches(Period, otherT.Period)) return false;
      if( !DeepComparable.Matches(Study, otherT.Study)) return false;
      if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
      if( !DeepComparable.Matches(AssignedComparisonGroupElement, otherT.AssignedComparisonGroupElement)) return false;
      if( !DeepComparable.Matches(ActualComparisonGroupElement, otherT.ActualComparisonGroupElement)) return false;
      if( !DeepComparable.Matches(Consent, otherT.Consent)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as ResearchSubject;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(Progress, otherT.Progress)) return false;
      if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
      if( !DeepComparable.IsExactly(Study, otherT.Study)) return false;
      if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
      if( !DeepComparable.IsExactly(AssignedComparisonGroupElement, otherT.AssignedComparisonGroupElement)) return false;
      if( !DeepComparable.IsExactly(ActualComparisonGroupElement, otherT.ActualComparisonGroupElement)) return false;
      if( !DeepComparable.IsExactly(Consent, otherT.Consent)) return false;

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
        foreach (var elem in Progress) { if (elem != null) yield return elem; }
        if (Period != null) yield return Period;
        if (Study != null) yield return Study;
        if (Subject != null) yield return Subject;
        if (AssignedComparisonGroupElement != null) yield return AssignedComparisonGroupElement;
        if (ActualComparisonGroupElement != null) yield return ActualComparisonGroupElement;
        foreach (var elem in Consent) { if (elem != null) yield return elem; }
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
        foreach (var elem in Progress) { if (elem != null) yield return new ElementValue("progress", elem); }
        if (Period != null) yield return new ElementValue("period", Period);
        if (Study != null) yield return new ElementValue("study", Study);
        if (Subject != null) yield return new ElementValue("subject", Subject);
        if (AssignedComparisonGroupElement != null) yield return new ElementValue("assignedComparisonGroup", AssignedComparisonGroupElement);
        if (ActualComparisonGroupElement != null) yield return new ElementValue("actualComparisonGroup", ActualComparisonGroupElement);
        foreach (var elem in Consent) { if (elem != null) yield return new ElementValue("consent", elem); }
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
        case "progress":
          value = Progress;
          return Progress?.Any() == true;
        case "period":
          value = Period;
          return Period is not null;
        case "study":
          value = Study;
          return Study is not null;
        case "subject":
          value = Subject;
          return Subject is not null;
        case "assignedComparisonGroup":
          value = AssignedComparisonGroupElement;
          return AssignedComparisonGroupElement is not null;
        case "actualComparisonGroup":
          value = ActualComparisonGroupElement;
          return ActualComparisonGroupElement is not null;
        case "consent":
          value = Consent;
          return Consent?.Any() == true;
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
          StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)value;
          return this;
        case "progress":
          Progress = (List<Hl7.Fhir.Model.ResearchSubject.ProgressComponent>)value;
          return this;
        case "period":
          Period = (Hl7.Fhir.Model.Period)value;
          return this;
        case "study":
          Study = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "subject":
          Subject = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "assignedComparisonGroup":
          AssignedComparisonGroupElement = (Hl7.Fhir.Model.Id)value;
          return this;
        case "actualComparisonGroup":
          ActualComparisonGroupElement = (Hl7.Fhir.Model.Id)value;
          return this;
        case "consent":
          Consent = (List<Hl7.Fhir.Model.ResourceReference>)value;
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
      if (Progress?.Any() == true) yield return new KeyValuePair<string,object>("progress",Progress);
      if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      if (Study is not null) yield return new KeyValuePair<string,object>("study",Study);
      if (Subject is not null) yield return new KeyValuePair<string,object>("subject",Subject);
      if (AssignedComparisonGroupElement is not null) yield return new KeyValuePair<string,object>("assignedComparisonGroup",AssignedComparisonGroupElement);
      if (ActualComparisonGroupElement is not null) yield return new KeyValuePair<string,object>("actualComparisonGroup",ActualComparisonGroupElement);
      if (Consent?.Any() == true) yield return new KeyValuePair<string,object>("consent",Consent);
    }

  }

}

// end of file
