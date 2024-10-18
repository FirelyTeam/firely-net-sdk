// <auto-generated/>
// Contents of: hl7.fhir.r4b.expansions@4.3.0, hl7.fhir.r4b.core@4.3.0

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
  /// Clinical issue with action
  /// </summary>
  /// <remarks>
  /// Indicates an actual or potential clinical issue with or between one or more active or proposed clinical actions for a patient; e.g. Drug-drug interaction, Ineffective treatment frequency, Procedure-condition conflict, etc.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("DetectedIssue","http://hl7.org/fhir/StructureDefinition/DetectedIssue")]
  public partial class DetectedIssue : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "DetectedIssue"; } }

    /// <summary>
    /// Indicates the potential degree of impact of the identified issue on the patient.
    /// (url: http://hl7.org/fhir/ValueSet/detectedissue-severity)
    /// (system: http://hl7.org/fhir/detectedissue-severity)
    /// </summary>
    [FhirEnumeration("DetectedIssueSeverity", "http://hl7.org/fhir/ValueSet/detectedissue-severity", "http://hl7.org/fhir/detectedissue-severity")]
    public enum DetectedIssueSeverity
    {
      /// <summary>
      /// Indicates the issue may be life-threatening or has the potential to cause permanent injury.
      /// (system: http://hl7.org/fhir/detectedissue-severity)
      /// </summary>
      [EnumLiteral("high"), Description("High")]
      High,
      /// <summary>
      /// Indicates the issue may result in noticeable adverse consequences but is unlikely to be life-threatening or cause permanent injury.
      /// (system: http://hl7.org/fhir/detectedissue-severity)
      /// </summary>
      [EnumLiteral("moderate"), Description("Moderate")]
      Moderate,
      /// <summary>
      /// Indicates the issue may result in some adverse consequences but is unlikely to substantially affect the situation of the subject.
      /// (system: http://hl7.org/fhir/detectedissue-severity)
      /// </summary>
      [EnumLiteral("low"), Description("Low")]
      Low,
    }

    /// <summary>
    /// Supporting evidence
    /// </summary>
    /// <remarks>
    /// Supporting evidence or manifestations that provide the basis for identifying the detected issue such as a GuidanceResponse or MeasureReport.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("DetectedIssue#Evidence")]
    [BackboneType("DetectedIssue.evidence")]
    public partial class EvidenceComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "DetectedIssue#Evidence"; } }

      /// <summary>
      /// Manifestation
      /// </summary>
      [FhirElement("code", Order=40, FiveWs="FiveWs.why[x]")]
      [Binding("DetectedIssueEvidenceCode")]
      [Cardinality(Min=0,Max=-1)]
      [DataMember]
      public List<Hl7.Fhir.Model.CodeableConcept> Code
      {
        get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
        set { _Code = value; OnPropertyChanged("Code"); }
      }

      private List<Hl7.Fhir.Model.CodeableConcept> _Code;

      /// <summary>
      /// Supporting information
      /// </summary>
      [FhirElement("detail", Order=50, FiveWs="FiveWs.why[x]")]
      [CLSCompliant(false)]
      [References("Resource")]
      [Cardinality(Min=0,Max=-1)]
      [DataMember]
      public List<Hl7.Fhir.Model.ResourceReference> Detail
      {
        get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ResourceReference>(); return _Detail; }
        set { _Detail = value; OnPropertyChanged("Detail"); }
      }

      private List<Hl7.Fhir.Model.ResourceReference> _Detail;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as EvidenceComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Code.Any()) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
        if(Detail.Any()) dest.Detail = new List<Hl7.Fhir.Model.ResourceReference>(Detail.DeepCopy());
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new EvidenceComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as EvidenceComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Code, otherT.Code)) return false;
        if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as EvidenceComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
        if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          foreach (var elem in Code) { if (elem != null) yield return elem; }
          foreach (var elem in Detail) { if (elem != null) yield return elem; }
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
          foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "code":
            value = Code;
            return Code?.Any() == true;
          case "detail":
            value = Detail;
            return Detail?.Any() == true;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "code":
            Code = (List<Hl7.Fhir.Model.CodeableConcept>)value;
            return this;
          case "detail":
            Detail = (List<Hl7.Fhir.Model.ResourceReference>)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Code?.Any() == true) yield return new KeyValuePair<string,object>("code",Code);
        if (Detail?.Any() == true) yield return new KeyValuePair<string,object>("detail",Detail);
      }

    }

    /// <summary>
    /// Step taken to address
    /// </summary>
    /// <remarks>
    /// Indicates an action that has been taken or is committed to reduce or eliminate the likelihood of the risk identified by the detected issue from manifesting.  Can also reflect an observation of known mitigating factors that may reduce/eliminate the need for any action.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("DetectedIssue#Mitigation")]
    [BackboneType("DetectedIssue.mitigation")]
    public partial class MitigationComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "DetectedIssue#Mitigation"; } }

      /// <summary>
      /// What mitigation?
      /// </summary>
      [FhirElement("action", Order=40)]
      [Binding("DetectedIssueMitigationAction")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Action
      {
        get { return _Action; }
        set { _Action = value; OnPropertyChanged("Action"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Action;

      /// <summary>
      /// Date committed
      /// </summary>
      [FhirElement("date", Order=50)]
      [DataMember]
      public Hl7.Fhir.Model.FhirDateTime DateElement
      {
        get { return _DateElement; }
        set { _DateElement = value; OnPropertyChanged("DateElement"); }
      }

      private Hl7.Fhir.Model.FhirDateTime _DateElement;

      /// <summary>
      /// Date committed
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public string Date
      {
        get { return DateElement != null ? DateElement.Value : null; }
        set
        {
          if (value == null)
            DateElement = null;
          else
            DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
          OnPropertyChanged("Date");
        }
      }

      /// <summary>
      /// Who is committing?
      /// </summary>
      [FhirElement("author", Order=60)]
      [CLSCompliant(false)]
      [References("Practitioner","PractitionerRole")]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Author
      {
        get { return _Author; }
        set { _Author = value; OnPropertyChanged("Author"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Author;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as MitigationComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Action != null) dest.Action = (Hl7.Fhir.Model.CodeableConcept)Action.DeepCopy();
        if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
        if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new MitigationComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as MitigationComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Action, otherT.Action)) return false;
        if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
        if( !DeepComparable.Matches(Author, otherT.Author)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as MitigationComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
        if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
        if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Action != null) yield return Action;
          if (DateElement != null) yield return DateElement;
          if (Author != null) yield return Author;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Action != null) yield return new ElementValue("action", Action);
          if (DateElement != null) yield return new ElementValue("date", DateElement);
          if (Author != null) yield return new ElementValue("author", Author);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "action":
            value = Action;
            return Action is not null;
          case "date":
            value = DateElement;
            return DateElement is not null;
          case "author":
            value = Author;
            return Author is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "action":
            Action = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "date":
            DateElement = (Hl7.Fhir.Model.FhirDateTime)value;
            return this;
          case "author":
            Author = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Action is not null) yield return new KeyValuePair<string,object>("action",Action);
        if (DateElement is not null) yield return new KeyValuePair<string,object>("date",DateElement);
        if (Author is not null) yield return new KeyValuePair<string,object>("author",Author);
      }

    }

    /// <summary>
    /// Unique id for the detected issue
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
    /// registered | preliminary | final | amended +
    /// </summary>
    [FhirElement("status", InSummary=true, IsModifier=true, Order=100, FiveWs="FiveWs.status")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("DetectedIssueStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Code<Hl7.Fhir.Model.ObservationStatus> StatusElement
    {
      get { return _StatusElement; }
      set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
    }

    private Code<Hl7.Fhir.Model.ObservationStatus> _StatusElement;

    /// <summary>
    /// registered | preliminary | final | amended +
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.ObservationStatus? Status
    {
      get { return StatusElement != null ? StatusElement.Value : null; }
      set
      {
        if (value == null)
          StatusElement = null;
        else
          StatusElement = new Code<Hl7.Fhir.Model.ObservationStatus>(value);
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// Issue Category, e.g. drug-drug, duplicate therapy, etc.
    /// </summary>
    [FhirElement("code", InSummary=true, Order=110, FiveWs="FiveWs.class")]
    [Binding("DetectedIssueCategory")]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Code
    {
      get { return _Code; }
      set { _Code = value; OnPropertyChanged("Code"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Code;

    /// <summary>
    /// high | moderate | low
    /// </summary>
    [FhirElement("severity", InSummary=true, Order=120, FiveWs="FiveWs.grade")]
    [DeclaredType(Type = typeof(Code))]
    [Binding("DetectedIssueSeverity")]
    [DataMember]
    public Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity> SeverityElement
    {
      get { return _SeverityElement; }
      set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
    }

    private Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity> _SeverityElement;

    /// <summary>
    /// high | moderate | low
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity? Severity
    {
      get { return SeverityElement != null ? SeverityElement.Value : null; }
      set
      {
        if (value == null)
          SeverityElement = null;
        else
          SeverityElement = new Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity>(value);
        OnPropertyChanged("Severity");
      }
    }

    /// <summary>
    /// Associated patient
    /// </summary>
    [FhirElement("patient", InSummary=true, Order=130, FiveWs="FiveWs.subject")]
    [CLSCompliant(false)]
    [References("Patient")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Patient
    {
      get { return _Patient; }
      set { _Patient = value; OnPropertyChanged("Patient"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Patient;

    /// <summary>
    /// When identified
    /// </summary>
    [FhirElement("identified", InSummary=true, Order=140, Choice=ChoiceType.DatatypeChoice, FiveWs="FiveWs.recorded")]
    [CLSCompliant(false)]
    [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
    [DataMember]
    public Hl7.Fhir.Model.DataType Identified
    {
      get { return _Identified; }
      set { _Identified = value; OnPropertyChanged("Identified"); }
    }

    private Hl7.Fhir.Model.DataType _Identified;

    /// <summary>
    /// The provider or device that identified the issue
    /// </summary>
    [FhirElement("author", InSummary=true, Order=150, FiveWs="FiveWs.author")]
    [CLSCompliant(false)]
    [References("Practitioner","PractitionerRole","Device")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Author
    {
      get { return _Author; }
      set { _Author = value; OnPropertyChanged("Author"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Author;

    /// <summary>
    /// Problem resource
    /// </summary>
    [FhirElement("implicated", InSummary=true, Order=160)]
    [CLSCompliant(false)]
    [References("Resource")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.ResourceReference> Implicated
    {
      get { if(_Implicated==null) _Implicated = new List<Hl7.Fhir.Model.ResourceReference>(); return _Implicated; }
      set { _Implicated = value; OnPropertyChanged("Implicated"); }
    }

    private List<Hl7.Fhir.Model.ResourceReference> _Implicated;

    /// <summary>
    /// Supporting evidence
    /// </summary>
    [FhirElement("evidence", Order=170, FiveWs="FiveWs.why[x]")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent> Evidence
    {
      get { if(_Evidence==null) _Evidence = new List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent>(); return _Evidence; }
      set { _Evidence = value; OnPropertyChanged("Evidence"); }
    }

    private List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent> _Evidence;

    /// <summary>
    /// Description and context
    /// </summary>
    [FhirElement("detail", Order=180)]
    [DataMember]
    public Hl7.Fhir.Model.FhirString DetailElement
    {
      get { return _DetailElement; }
      set { _DetailElement = value; OnPropertyChanged("DetailElement"); }
    }

    private Hl7.Fhir.Model.FhirString _DetailElement;

    /// <summary>
    /// Description and context
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Detail
    {
      get { return DetailElement != null ? DetailElement.Value : null; }
      set
      {
        if (value == null)
          DetailElement = null;
        else
          DetailElement = new Hl7.Fhir.Model.FhirString(value);
        OnPropertyChanged("Detail");
      }
    }

    /// <summary>
    /// Authority for issue
    /// </summary>
    [FhirElement("reference", Order=190)]
    [DataMember]
    public Hl7.Fhir.Model.FhirUri ReferenceElement
    {
      get { return _ReferenceElement; }
      set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
    }

    private Hl7.Fhir.Model.FhirUri _ReferenceElement;

    /// <summary>
    /// Authority for issue
    /// </summary>
    /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
    [IgnoreDataMember]
    public string Reference
    {
      get { return ReferenceElement != null ? ReferenceElement.Value : null; }
      set
      {
        if (value == null)
          ReferenceElement = null;
        else
          ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
        OnPropertyChanged("Reference");
      }
    }

    /// <summary>
    /// Step taken to address
    /// </summary>
    [FhirElement("mitigation", Order=200)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent> Mitigation
    {
      get { if(_Mitigation==null) _Mitigation = new List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent>(); return _Mitigation; }
      set { _Mitigation = value; OnPropertyChanged("Mitigation"); }
    }

    private List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent> _Mitigation;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as DetectedIssue;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ObservationStatus>)StatusElement.DeepCopy();
      if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
      if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity>)SeverityElement.DeepCopy();
      if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
      if(Identified != null) dest.Identified = (Hl7.Fhir.Model.DataType)Identified.DeepCopy();
      if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
      if(Implicated.Any()) dest.Implicated = new List<Hl7.Fhir.Model.ResourceReference>(Implicated.DeepCopy());
      if(Evidence.Any()) dest.Evidence = new List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent>(Evidence.DeepCopy());
      if(DetailElement != null) dest.DetailElement = (Hl7.Fhir.Model.FhirString)DetailElement.DeepCopy();
      if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
      if(Mitigation.Any()) dest.Mitigation = new List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent>(Mitigation.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new DetectedIssue());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as DetectedIssue;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.Matches(Code, otherT.Code)) return false;
      if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
      if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
      if( !DeepComparable.Matches(Identified, otherT.Identified)) return false;
      if( !DeepComparable.Matches(Author, otherT.Author)) return false;
      if( !DeepComparable.Matches(Implicated, otherT.Implicated)) return false;
      if( !DeepComparable.Matches(Evidence, otherT.Evidence)) return false;
      if( !DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;
      if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
      if( !DeepComparable.Matches(Mitigation, otherT.Mitigation)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as DetectedIssue;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
      if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
      if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
      if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
      if( !DeepComparable.IsExactly(Identified, otherT.Identified)) return false;
      if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
      if( !DeepComparable.IsExactly(Implicated, otherT.Implicated)) return false;
      if( !DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
      if( !DeepComparable.IsExactly(DetailElement, otherT.DetailElement)) return false;
      if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
      if( !DeepComparable.IsExactly(Mitigation, otherT.Mitigation)) return false;

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
        if (Code != null) yield return Code;
        if (SeverityElement != null) yield return SeverityElement;
        if (Patient != null) yield return Patient;
        if (Identified != null) yield return Identified;
        if (Author != null) yield return Author;
        foreach (var elem in Implicated) { if (elem != null) yield return elem; }
        foreach (var elem in Evidence) { if (elem != null) yield return elem; }
        if (DetailElement != null) yield return DetailElement;
        if (ReferenceElement != null) yield return ReferenceElement;
        foreach (var elem in Mitigation) { if (elem != null) yield return elem; }
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
        if (Code != null) yield return new ElementValue("code", Code);
        if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
        if (Patient != null) yield return new ElementValue("patient", Patient);
        if (Identified != null) yield return new ElementValue("identified", Identified);
        if (Author != null) yield return new ElementValue("author", Author);
        foreach (var elem in Implicated) { if (elem != null) yield return new ElementValue("implicated", elem); }
        foreach (var elem in Evidence) { if (elem != null) yield return new ElementValue("evidence", elem); }
        if (DetailElement != null) yield return new ElementValue("detail", DetailElement);
        if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
        foreach (var elem in Mitigation) { if (elem != null) yield return new ElementValue("mitigation", elem); }
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
        case "code":
          value = Code;
          return Code is not null;
        case "severity":
          value = SeverityElement;
          return SeverityElement is not null;
        case "patient":
          value = Patient;
          return Patient is not null;
        case "identified":
          value = Identified;
          return Identified is not null;
        case "author":
          value = Author;
          return Author is not null;
        case "implicated":
          value = Implicated;
          return Implicated?.Any() == true;
        case "evidence":
          value = Evidence;
          return Evidence?.Any() == true;
        case "detail":
          value = DetailElement;
          return DetailElement is not null;
        case "reference":
          value = ReferenceElement;
          return ReferenceElement is not null;
        case "mitigation":
          value = Mitigation;
          return Mitigation?.Any() == true;
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
          StatusElement = (Code<Hl7.Fhir.Model.ObservationStatus>)value;
          return this;
        case "code":
          Code = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "severity":
          SeverityElement = (Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity>)value;
          return this;
        case "patient":
          Patient = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "identified":
          Identified = (Hl7.Fhir.Model.DataType)value;
          return this;
        case "author":
          Author = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "implicated":
          Implicated = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "evidence":
          Evidence = (List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent>)value;
          return this;
        case "detail":
          DetailElement = (Hl7.Fhir.Model.FhirString)value;
          return this;
        case "reference":
          ReferenceElement = (Hl7.Fhir.Model.FhirUri)value;
          return this;
        case "mitigation":
          Mitigation = (List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent>)value;
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
      if (Code is not null) yield return new KeyValuePair<string,object>("code",Code);
      if (SeverityElement is not null) yield return new KeyValuePair<string,object>("severity",SeverityElement);
      if (Patient is not null) yield return new KeyValuePair<string,object>("patient",Patient);
      if (Identified is not null) yield return new KeyValuePair<string,object>("identified",Identified);
      if (Author is not null) yield return new KeyValuePair<string,object>("author",Author);
      if (Implicated?.Any() == true) yield return new KeyValuePair<string,object>("implicated",Implicated);
      if (Evidence?.Any() == true) yield return new KeyValuePair<string,object>("evidence",Evidence);
      if (DetailElement is not null) yield return new KeyValuePair<string,object>("detail",DetailElement);
      if (ReferenceElement is not null) yield return new KeyValuePair<string,object>("reference",ReferenceElement);
      if (Mitigation?.Any() == true) yield return new KeyValuePair<string,object>("mitigation",Mitigation);
    }

  }

}

// end of file
