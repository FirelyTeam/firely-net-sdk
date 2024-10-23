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
  /// Describes the intended objective(s) for a patient, group or organization
  /// </summary>
  /// <remarks>
  /// Describes the intended objective(s) for a patient, group or organization care, for example, weight loss, restoring an activity of daily living, obtaining herd immunity via immunization, meeting a process improvement objective, etc.
  /// Goal can be achieving a particular change or merely maintaining a current state or even slowing a decline.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("Goal","http://hl7.org/fhir/StructureDefinition/Goal")]
  public partial class Goal : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "Goal"; } }

    /// <summary>
    /// Indicates whether the goal has been met and is still being targeted
    /// (url: http://hl7.org/fhir/ValueSet/goal-status)
    /// (system: http://hl7.org/fhir/goal-status)
    /// </summary>
    [FhirEnumeration("GoalStatus", "http://hl7.org/fhir/ValueSet/goal-status", "http://hl7.org/fhir/goal-status")]
    public enum GoalStatus
    {
      /// <summary>
      /// A goal is proposed for this patient
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("proposed"), Description("Proposed")]
      Proposed,
      /// <summary>
      /// A proposed goal was accepted or acknowledged
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("accepted"), Description("Accepted")]
      Accepted,
      /// <summary>
      /// A goal is planned for this patient
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("planned"), Description("Planned")]
      Planned,
      /// <summary>
      /// The goal is being sought but has not yet been reached.  (Also applies if goal was reached in the past but there has been regression and goal is being sought again)
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("in-progress"), Description("In Progress")]
      InProgress,
      /// <summary>
      /// The goal is on schedule for the planned timelines
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("on-target"), Description("On Target")]
      OnTarget,
      /// <summary>
      /// The goal is ahead of the planned timelines
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("ahead-of-target"), Description("Ahead of Target")]
      AheadOfTarget,
      /// <summary>
      /// The goal is behind the planned timelines
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("behind-target"), Description("Behind Target")]
      BehindTarget,
      /// <summary>
      /// The goal has been met, but ongoing activity is needed to sustain the goal objective
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("sustaining"), Description("Sustaining")]
      Sustaining,
      /// <summary>
      /// The goal has been met and no further action is needed
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("achieved"), Description("Achieved")]
      Achieved,
      /// <summary>
      /// The goal remains a long term objective but is no longer being actively pursued for a temporary period of time.
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("on-hold"), Description("On Hold")]
      OnHold,
      /// <summary>
      /// The previously accepted goal is no longer being sought
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("cancelled"), Description("Cancelled")]
      Cancelled,
      /// <summary>
      /// The goal was entered in error and voided.
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("entered-in-error"), Description("Entered In Error")]
      EnteredInError,
      /// <summary>
      /// A proposed goal was rejected
      /// (system: http://hl7.org/fhir/goal-status)
      /// </summary>
      [EnumLiteral("rejected"), Description("Rejected")]
      Rejected,
    }

    /// <summary>
    /// Target outcome for the goal
    /// </summary>
    /// <remarks>
    /// Indicates what should be done by when.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("Goal.target", IsBackboneType=true)]
    public partial class TargetComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Goal.target"; } }

      /// <summary>
      /// The parameter whose value is being tracked
      /// </summary>
      [FhirElement("measure", InSummary=true, Order=40)]
      [Binding("GoalTargetMeasure")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Measure
      {
        get { return _Measure; }
        set { _Measure = value; OnPropertyChanged("Measure"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Measure;

      /// <summary>
      /// The target value to be achieved
      /// </summary>
      [FhirElement("detail", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
      [Binding("GoalTargetDetail")]
      [CLSCompliant(false)]
      [AllowedTypes(typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.CodeableConcept))]
      [DataMember]
      public Hl7.Fhir.Model.DataType Detail
      {
        get { return _Detail; }
        set { _Detail = value; OnPropertyChanged("Detail"); }
      }

      private Hl7.Fhir.Model.DataType _Detail;

      /// <summary>
      /// Reach goal on or before
      /// </summary>
      [FhirElement("due", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice, FiveWs="when.done")]
      [CLSCompliant(false)]
      [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Duration))]
      [DataMember]
      public Hl7.Fhir.Model.DataType Due
      {
        get { return _Due; }
        set { _Due = value; OnPropertyChanged("Due"); }
      }

      private Hl7.Fhir.Model.DataType _Due;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as TargetComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Measure != null) dest.Measure = (Hl7.Fhir.Model.CodeableConcept)Measure.DeepCopy();
        if(Detail != null) dest.Detail = (Hl7.Fhir.Model.DataType)Detail.DeepCopy();
        if(Due != null) dest.Due = (Hl7.Fhir.Model.DataType)Due.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new TargetComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as TargetComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Measure, otherT.Measure)) return false;
        if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
        if( !DeepComparable.Matches(Due, otherT.Due)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as TargetComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Measure, otherT.Measure)) return false;
        if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
        if( !DeepComparable.IsExactly(Due, otherT.Due)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Measure != null) yield return Measure;
          if (Detail != null) yield return Detail;
          if (Due != null) yield return Due;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Measure != null) yield return new ElementValue("measure", Measure);
          if (Detail != null) yield return new ElementValue("detail", Detail);
          if (Due != null) yield return new ElementValue("due", Due);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "measure":
            value = Measure;
            return Measure is not null;
          case "detail":
            value = Detail;
            return Detail is not null;
          case "due":
            value = Due;
            return Due is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "measure":
            Measure = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "detail":
            Detail = (Hl7.Fhir.Model.DataType)value;
            return this;
          case "due":
            Due = (Hl7.Fhir.Model.DataType)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Measure is not null) yield return new KeyValuePair<string,object>("measure",Measure);
        if (Detail is not null) yield return new KeyValuePair<string,object>("detail",Detail);
        if (Due is not null) yield return new KeyValuePair<string,object>("due",Due);
      }

    }

    /// <summary>
    /// External Ids for this goal
    /// </summary>
    [FhirElement("identifier", Order=90, FiveWs="id")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Identifier> Identifier
    {
      get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
      set { _Identifier = value; OnPropertyChanged("Identifier"); }
    }

    private List<Hl7.Fhir.Model.Identifier> _Identifier;

    /// <summary>
    /// proposed | accepted | planned | in-progress | on-target | ahead-of-target | behind-target | sustaining | achieved | on-hold | cancelled | entered-in-error | rejected
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=100, FiveWs="status")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("GoalStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.Goal.GoalStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.Goal.GoalStatus> _StatusElement;

    /// <summary>
    /// proposed | accepted | planned | in-progress | on-target | ahead-of-target | behind-target | sustaining | achieved | on-hold | cancelled | entered-in-error | rejected
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.Goal.GoalStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.Goal.GoalStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// E.g. Treatment, dietary, behavioral, etc.
    /// </summary>
    [FhirElement("category", InSummary=true, Order=110, FiveWs="class")]
    [Binding("GoalCategory")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CodeableConcept> Category
    {
      get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
      set { _Category = value; OnPropertyChanged("Category"); }
    }

    private List<Hl7.Fhir.Model.CodeableConcept> _Category;

    /// <summary>
    /// high-priority | medium-priority | low-priority
    /// </summary>
    [FhirElement("priority", InSummary=true, Order=120, FiveWs="grade")]
    [Binding("GoalPriority")]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Priority
    {
      get { return _Priority; }
      set { _Priority = value; OnPropertyChanged("Priority"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Priority;

    /// <summary>
    /// Code or text describing goal
    /// </summary>
    [FhirElement("description", InSummary=true, Order=130, FiveWs="what")]
    [Binding("GoalDescription")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Description
    {
      get { return _Description; }
      set { _Description = value; OnPropertyChanged("Description"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Description;

    /// <summary>
    /// Who this goal is intended for
    /// </summary>
    [FhirElement("subject", InSummary=true, Order=140, FiveWs="who.focus")]
    [CLSCompliant(false)]
    [References("Patient","Group","Organization")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Subject
    {
      get { return _Subject; }
      set { _Subject = value; OnPropertyChanged("Subject"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Subject;

    /// <summary>
    /// When goal pursuit begins
    /// </summary>
    [FhirElement("start", InSummary=true, Order=150, Choice=ChoiceType.DatatypeChoice, FiveWs="when.planned")]
    [Binding("GoalStartEvent")]
    [CLSCompliant(false)]
    [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.CodeableConcept))]
    [DataMember]
    public Hl7.Fhir.Model.DataType Start
    {
      get { return _Start; }
      set { _Start = value; OnPropertyChanged("Start"); }
    }

    private Hl7.Fhir.Model.DataType _Start;

    /// <summary>
    /// Target outcome for the goal
    /// </summary>
    [FhirElement("target", Order=160)]
    [DataMember]
    public Hl7.Fhir.Model.Goal.TargetComponent Target
    {
      get { return _Target; }
      set { _Target = value; OnPropertyChanged("Target"); }
    }

    private Hl7.Fhir.Model.Goal.TargetComponent _Target;

    /// <summary>
    /// When goal status took effect
    /// </summary>
    [FhirElement("statusDate", InSummary=true, Order=170, FiveWs="when.recorded")]
    [DataMember]
    public Hl7.Fhir.Model.Date StatusDateElement
    {
      get { return _StatusDateElement; }
      set { _StatusDateElement = value; OnPropertyChanged("StatusDateElement"); }
    }

    private Hl7.Fhir.Model.Date _StatusDateElement;

    /// <summary>
    /// When goal status took effect
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string StatusDate
    {
      get { return StatusDateElement != null ? StatusDateElement.Value : null; }
      set
      {
        if (value == null)
          StatusDateElement = null;
        else
          StatusDateElement = new Hl7.Fhir.Model.Date(value);
        OnPropertyChanged("StatusDate");
      }
    }

    /// <summary>
    /// Reason for current status
    /// </summary>
    [FhirElement("statusReason", Order=180)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString StatusReasonElement
    {
      get { return _StatusReasonElement; }
      set { _StatusReasonElement = value; OnPropertyChanged("StatusReasonElement"); }
    }

    private Hl7.Fhir.Model.FhirString _StatusReasonElement;

    /// <summary>
    /// Reason for current status
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string StatusReason
    {
      get { return StatusReasonElement != null ? StatusReasonElement.Value : null; }
      set
      {
        if (value == null)
          StatusReasonElement = null;
        else
          StatusReasonElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("StatusReason");
      }
    }

    /// <summary>
    /// Who's responsible for creating Goal?
    /// </summary>
    [FhirElement("expressedBy", InSummary=true, Order=190, FiveWs="who.source")]
    [CLSCompliant(false)]
    [References("Patient","Practitioner","RelatedPerson")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference ExpressedBy
    {
      get { return _ExpressedBy; }
      set { _ExpressedBy = value; OnPropertyChanged("ExpressedBy"); }
    }

    private Hl7.Fhir.Model.ResourceReference _ExpressedBy;

    /// <summary>
    /// Issues addressed by this goal
    /// </summary>
    [FhirElement("addresses", Order=200, FiveWs="why")]
    [CLSCompliant(false)]
    [References("Condition","Observation","MedicationStatement","NutritionOrder","ProcedureRequest","RiskAssessment")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Addresses
    {
      get { if(_Addresses==null) _Addresses = new List<Hl7.Fhir.Model.ResourceReference>(); return _Addresses; }
      set { _Addresses = value; OnPropertyChanged("Addresses"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Addresses;

    /// <summary>
    /// Comments about the goal
    /// </summary>
    [FhirElement("note", Order=210)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Annotation> Note
    {
      get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
      set { _Note = value; OnPropertyChanged("Note"); }
    }

    private List<Hl7.Fhir.Model.Annotation> _Note;

    /// <summary>
    /// What result was achieved regarding the goal?
    /// </summary>
    [FhirElement("outcomeCode", Order=220)]
    [Binding("GoalOutcome")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CodeableConcept> OutcomeCode
    {
      get { if(_OutcomeCode==null) _OutcomeCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _OutcomeCode; }
      set { _OutcomeCode = value; OnPropertyChanged("OutcomeCode"); }
    }

    private List<Hl7.Fhir.Model.CodeableConcept> _OutcomeCode;

    /// <summary>
    /// Observation that resulted from goal
    /// </summary>
    [FhirElement("outcomeReference", Order=230)]
    [CLSCompliant(false)]
    [References("Observation")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> OutcomeReference
    {
      get { if(_OutcomeReference==null) _OutcomeReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _OutcomeReference; }
      set { _OutcomeReference = value; OnPropertyChanged("OutcomeReference"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _OutcomeReference;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as Goal;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Goal.GoalStatus>)StatusElement.DeepCopy();
      if(Category.Any()) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
      if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
      if(Description != null) dest.Description = (Hl7.Fhir.Model.CodeableConcept)Description.DeepCopy();
      if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
      if(Start != null) dest.Start = (Hl7.Fhir.Model.DataType)Start.DeepCopy();
      if(Target != null) dest.Target = (Hl7.Fhir.Model.Goal.TargetComponent)Target.DeepCopy();
      if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.Date)StatusDateElement.DeepCopy();
      if(StatusReasonElement != null) dest.StatusReasonElement = (Hl7.Fhir.Model.FhirString)StatusReasonElement.DeepCopy();
      if(ExpressedBy != null) dest.ExpressedBy = (Hl7.Fhir.Model.ResourceReference)ExpressedBy.DeepCopy();
      if(Addresses.Any()) dest.Addresses = new List<Hl7.Fhir.Model.ResourceReference>(Addresses.DeepCopy());
      if(Note.Any()) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
      if(OutcomeCode.Any()) dest.OutcomeCode = new List<Hl7.Fhir.Model.CodeableConcept>(OutcomeCode.DeepCopy());
      if(OutcomeReference.Any()) dest.OutcomeReference = new List<Hl7.Fhir.Model.ResourceReference>(OutcomeReference.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new Goal());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as Goal;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(Category, otherT.Category)) return false;
      if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
      if( !DeepComparable.Matches(Description, otherT.Description)) return false;
      if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
      if( !DeepComparable.Matches(Start, otherT.Start)) return false;
      if( !DeepComparable.Matches(Target, otherT.Target)) return false;
      if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
      if( !DeepComparable.Matches(StatusReasonElement, otherT.StatusReasonElement)) return false;
      if( !DeepComparable.Matches(ExpressedBy, otherT.ExpressedBy)) return false;
      if( !DeepComparable.Matches(Addresses, otherT.Addresses)) return false;
      if( !DeepComparable.Matches(Note, otherT.Note)) return false;
      if( !DeepComparable.Matches(OutcomeCode, otherT.OutcomeCode)) return false;
      if( !DeepComparable.Matches(OutcomeReference, otherT.OutcomeReference)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as Goal;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
      if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
      if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
      if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
      if( !DeepComparable.IsExactly(Start, otherT.Start)) return false;
      if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
      if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
      if( !DeepComparable.IsExactly(StatusReasonElement, otherT.StatusReasonElement)) return false;
      if( !DeepComparable.IsExactly(ExpressedBy, otherT.ExpressedBy)) return false;
      if( !DeepComparable.IsExactly(Addresses, otherT.Addresses)) return false;
      if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
      if( !DeepComparable.IsExactly(OutcomeCode, otherT.OutcomeCode)) return false;
      if( !DeepComparable.IsExactly(OutcomeReference, otherT.OutcomeReference)) return false;

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
        foreach (var elem in Category) { if (elem != null) yield return elem; }
        if (Priority != null) yield return Priority;
        if (Description != null) yield return Description;
        if (Subject != null) yield return Subject;
        if (Start != null) yield return Start;
        if (Target != null) yield return Target;
        if (StatusDateElement != null) yield return StatusDateElement;
        if (StatusReasonElement != null) yield return StatusReasonElement;
        if (ExpressedBy != null) yield return ExpressedBy;
        foreach (var elem in Addresses) { if (elem != null) yield return elem; }
        foreach (var elem in Note) { if (elem != null) yield return elem; }
        foreach (var elem in OutcomeCode) { if (elem != null) yield return elem; }
        foreach (var elem in OutcomeReference) { if (elem != null) yield return elem; }
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
        foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
        if (Priority != null) yield return new ElementValue("priority", Priority);
        if (Description != null) yield return new ElementValue("description", Description);
        if (Subject != null) yield return new ElementValue("subject", Subject);
        if (Start != null) yield return new ElementValue("start", Start);
        if (Target != null) yield return new ElementValue("target", Target);
        if (StatusDateElement != null) yield return new ElementValue("statusDate", StatusDateElement);
        if (StatusReasonElement != null) yield return new ElementValue("statusReason", StatusReasonElement);
        if (ExpressedBy != null) yield return new ElementValue("expressedBy", ExpressedBy);
        foreach (var elem in Addresses) { if (elem != null) yield return new ElementValue("addresses", elem); }
        foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
        foreach (var elem in OutcomeCode) { if (elem != null) yield return new ElementValue("outcomeCode", elem); }
        foreach (var elem in OutcomeReference) { if (elem != null) yield return new ElementValue("outcomeReference", elem); }
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
        case "category":
          value = Category;
          return Category?.Any() == true;
        case "priority":
          value = Priority;
          return Priority is not null;
        case "description":
          value = Description;
          return Description is not null;
        case "subject":
          value = Subject;
          return Subject is not null;
        case "start":
          value = Start;
          return Start is not null;
        case "target":
          value = Target;
          return Target is not null;
        case "statusDate":
          value = StatusDateElement;
          return StatusDateElement is not null;
        case "statusReason":
          value = StatusReasonElement;
          return StatusReasonElement is not null;
        case "expressedBy":
          value = ExpressedBy;
          return ExpressedBy is not null;
        case "addresses":
          value = Addresses;
          return Addresses?.Any() == true;
        case "note":
          value = Note;
          return Note?.Any() == true;
        case "outcomeCode":
          value = OutcomeCode;
          return OutcomeCode?.Any() == true;
        case "outcomeReference":
          value = OutcomeReference;
          return OutcomeReference?.Any() == true;
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
          StatusElement = (Code<Hl7.Fhir.Model.Goal.GoalStatus>)value;
          return this;
        case "category":
          Category = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "priority":
          Priority = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "description":
          Description = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "subject":
          Subject = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "start":
          Start = (Hl7.Fhir.Model.DataType)value;
          return this;
        case "target":
          Target = (Hl7.Fhir.Model.Goal.TargetComponent)value;
          return this;
        case "statusDate":
          StatusDateElement = (Hl7.Fhir.Model.Date)value;
          return this;
        case "statusReason":
          StatusReasonElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "expressedBy":
          ExpressedBy = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "addresses":
          Addresses = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "note":
          Note = (List<Hl7.Fhir.Model.Annotation>)value;
          return this;
        case "outcomeCode":
          OutcomeCode = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "outcomeReference":
          OutcomeReference = (List<Hl7.Fhir.Model.ResourceReference>)value;
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
      if (Category?.Any() == true) yield return new KeyValuePair<string,object>("category",Category);
      if (Priority is not null) yield return new KeyValuePair<string,object>("priority",Priority);
      if (Description is not null) yield return new KeyValuePair<string,object>("description",Description);
      if (Subject is not null) yield return new KeyValuePair<string,object>("subject",Subject);
      if (Start is not null) yield return new KeyValuePair<string,object>("start",Start);
      if (Target is not null) yield return new KeyValuePair<string,object>("target",Target);
      if (StatusDateElement is not null) yield return new KeyValuePair<string,object>("statusDate",StatusDateElement);
      if (StatusReasonElement is not null) yield return new KeyValuePair<string,object>("statusReason",StatusReasonElement);
      if (ExpressedBy is not null) yield return new KeyValuePair<string,object>("expressedBy",ExpressedBy);
      if (Addresses?.Any() == true) yield return new KeyValuePair<string,object>("addresses",Addresses);
      if (Note?.Any() == true) yield return new KeyValuePair<string,object>("note",Note);
      if (OutcomeCode?.Any() == true) yield return new KeyValuePair<string,object>("outcomeCode",OutcomeCode);
      if (OutcomeReference?.Any() == true) yield return new KeyValuePair<string,object>("outcomeReference",OutcomeReference);
    }

  }

}

// end of file
