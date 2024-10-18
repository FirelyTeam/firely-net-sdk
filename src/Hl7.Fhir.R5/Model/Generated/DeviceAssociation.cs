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
  /// A record of association or dissociation of a device with a patient
  /// </summary>
  [Serializable]
  [DataContract]
  [FhirType("DeviceAssociation","http://hl7.org/fhir/StructureDefinition/DeviceAssociation")]
  public partial class DeviceAssociation : Hl7.Fhir.Model.DomainResource, IIdentifiable<List<Identifier>>
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "DeviceAssociation"; } }

    /// <summary>
    /// DeviceAssociation Status Codes
    /// (url: http://hl7.org/fhir/ValueSet/deviceassociation-status)
    /// (system: http://hl7.org/fhir/deviceassociation-status)
    /// </summary>
    [FhirEnumeration("DeviceAssociationCodes", "http://hl7.org/fhir/ValueSet/deviceassociation-status", "http://hl7.org/fhir/deviceassociation-status")]
    public enum DeviceAssociationCodes
    {
      /// <summary>
      /// The device is implanted in the patient.
      /// (system: http://hl7.org/fhir/deviceassociation-status)
      /// </summary>
      [EnumLiteral("implanted"), Description("Implanted")]
      Implanted,
      /// <summary>
      /// The device is no longer implanted in the patient. Note that this is not the value to be used for devices that have never been implanted. In those cases, no value or a specific value can be used.
      /// (system: http://hl7.org/fhir/deviceassociation-status)
      /// </summary>
      [EnumLiteral("explanted"), Description("Explanted")]
      Explanted,
      /// <summary>
      /// The association was entered in error and therefore nullified.
      /// (system: http://hl7.org/fhir/deviceassociation-status)
      /// </summary>
      [EnumLiteral("entered-in-error"), Description("Entered in Error")]
      EnteredInError,
      /// <summary>
      /// The device is attached to the patient but not implanted in the patient.
      /// (system: http://hl7.org/fhir/deviceassociation-status)
      /// </summary>
      [EnumLiteral("attached"), Description("Attached")]
      Attached,
      /// <summary>
      /// The association status of the device has not been determined.
      /// (system: http://hl7.org/fhir/deviceassociation-status)
      /// </summary>
      [EnumLiteral("unknown"), Description("Unknown")]
      Unknown,
    }

    /// <summary>
    /// The details about the device when it is in use to describe its operation
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("DeviceAssociation#Operation")]
    [BackboneType("DeviceAssociation.operation")]
    public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "DeviceAssociation#Operation"; } }

      /// <summary>
      /// Device operational condition
      /// </summary>
      [FhirElement("status", InSummary=true, Order=40)]
      [Binding("DeviceAssociationOperationStatus")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Status
      {
        get { return _Status; }
        set { _Status = value; OnPropertyChanged("Status"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Status;

      /// <summary>
      /// The individual performing the action enabled by the device
      /// </summary>
      [FhirElement("operator", InSummary=true, Order=50)]
      [CLSCompliant(false)]
      [References("Patient","Practitioner","RelatedPerson")]
      [Cardinality(Min=0,Max=-1)]
      [DataMember]
      public List<Hl7.Fhir.Model.ResourceReference> Operator
      {
        get { if(_Operator==null) _Operator = new List<Hl7.Fhir.Model.ResourceReference>(); return _Operator; }
        set { _Operator = value; OnPropertyChanged("Operator"); }
      }

      private List<Hl7.Fhir.Model.ResourceReference> _Operator;

      /// <summary>
      /// Begin and end dates and times for the device's operation
      /// </summary>
      [FhirElement("period", InSummary=true, Order=60)]
      [DataMember]
      public Hl7.Fhir.Model.Period Period
      {
        get { return _Period; }
        set { _Period = value; OnPropertyChanged("Period"); }
      }

      private Hl7.Fhir.Model.Period _Period;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as OperationComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
        if(Operator.Any()) dest.Operator = new List<Hl7.Fhir.Model.ResourceReference>(Operator.DeepCopy());
        if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new OperationComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as OperationComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Status, otherT.Status)) return false;
        if( !DeepComparable.Matches(Operator, otherT.Operator)) return false;
        if( !DeepComparable.Matches(Period, otherT.Period)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as OperationComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
        if( !DeepComparable.IsExactly(Operator, otherT.Operator)) return false;
        if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Status != null) yield return Status;
          foreach (var elem in Operator) { if (elem != null) yield return elem; }
          if (Period != null) yield return Period;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Status != null) yield return new ElementValue("status", Status);
          foreach (var elem in Operator) { if (elem != null) yield return new ElementValue("operator", elem); }
          if (Period != null) yield return new ElementValue("period", Period);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "status":
            value = Status;
            return Status is not null;
          case "operator":
            value = Operator;
            return Operator?.Any() == true;
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
            Status = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "operator":
            Operator = (List<Hl7.Fhir.Model.ResourceReference>)value;
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
        if (Status is not null) yield return new KeyValuePair<string,object>("status",Status);
        if (Operator?.Any() == true) yield return new KeyValuePair<string,object>("operator",Operator);
        if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      }

    }

    /// <summary>
    /// Instance identifier
    /// </summary>
    [FhirElement("identifier", InSummary=true, Order=90)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Identifier> Identifier
    {
      get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
      set { _Identifier = value; OnPropertyChanged("Identifier"); }
    }

    private List<Hl7.Fhir.Model.Identifier> _Identifier;

    /// <summary>
    /// Reference to the devices associated with the patient or group
    /// </summary>
    [FhirElement("device", InSummary=true, Order=100)]
    [CLSCompliant(false)]
    [References("Device")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Device
    {
      get { return _Device; }
      set { _Device = value; OnPropertyChanged("Device"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Device;

    /// <summary>
    /// Describes the relationship between the device and subject
    /// </summary>
    [FhirElement("category", InSummary=true, Order=110)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CodeableConcept> Category
    {
      get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
      set { _Category = value; OnPropertyChanged("Category"); }
    }

    private List<Hl7.Fhir.Model.CodeableConcept> _Category;

    /// <summary>
    /// implanted | explanted | attached | entered-in-error | unknown
    /// </summary>
    [FhirElement("status", InSummary=true, Order=120)]
    [Binding("DeviceAssociationStatus")]
    [Cardinality(Min=1,Max=1)]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Status
    {
      get { return _Status; }
      set { _Status = value; OnPropertyChanged("Status"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Status;

    /// <summary>
    /// The reasons given for the current association status
    /// </summary>
    [FhirElement("statusReason", InSummary=true, Order=130)]
    [Binding("DeviceAssociationStatusReason")]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.CodeableConcept> StatusReason
    {
      get { if(_StatusReason==null) _StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _StatusReason; }
      set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
    }

    private List<Hl7.Fhir.Model.CodeableConcept> _StatusReason;

    /// <summary>
    /// The individual, group of individuals or device that the device is on or associated with
    /// </summary>
    [FhirElement("subject", InSummary=true, Order=140)]
    [CLSCompliant(false)]
    [References("Patient","Group","Practitioner","RelatedPerson","Device")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference Subject
    {
      get { return _Subject; }
      set { _Subject = value; OnPropertyChanged("Subject"); }
    }

    private Hl7.Fhir.Model.ResourceReference _Subject;

    /// <summary>
    /// Current anatomical location of the device in/on subject
    /// </summary>
    [FhirElement("bodyStructure", InSummary=true, Order=150)]
    [CLSCompliant(false)]
    [References("BodyStructure")]
    [DataMember]
    public Hl7.Fhir.Model.ResourceReference BodyStructure
    {
      get { return _BodyStructure; }
      set { _BodyStructure = value; OnPropertyChanged("BodyStructure"); }
    }

    private Hl7.Fhir.Model.ResourceReference _BodyStructure;

    /// <summary>
    /// Begin and end dates and times for the device association
    /// </summary>
    [FhirElement("period", InSummary=true, Order=160)]
    [DataMember]
    public Hl7.Fhir.Model.Period Period
    {
      get { return _Period; }
      set { _Period = value; OnPropertyChanged("Period"); }
    }

    private Hl7.Fhir.Model.Period _Period;

    /// <summary>
    /// The details about the device when it is in use to describe its operation
    /// </summary>
    [FhirElement("operation", InSummary=true, Order=170)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.DeviceAssociation.OperationComponent> Operation
    {
      get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.DeviceAssociation.OperationComponent>(); return _Operation; }
      set { _Operation = value; OnPropertyChanged("Operation"); }
    }

    private List<Hl7.Fhir.Model.DeviceAssociation.OperationComponent> _Operation;

    List<Identifier> IIdentifiable<List<Identifier>>.Identifier { get => Identifier; set => Identifier = value; }

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as DeviceAssociation;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Identifier.Any()) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
      if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
      if(Category.Any()) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
      if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
      if(StatusReason.Any()) dest.StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(StatusReason.DeepCopy());
      if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
      if(BodyStructure != null) dest.BodyStructure = (Hl7.Fhir.Model.ResourceReference)BodyStructure.DeepCopy();
      if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
      if(Operation.Any()) dest.Operation = new List<Hl7.Fhir.Model.DeviceAssociation.OperationComponent>(Operation.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new DeviceAssociation());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as DeviceAssociation;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.Matches(Device, otherT.Device)) return false;
      if( !DeepComparable.Matches(Category, otherT.Category)) return false;
      if( !DeepComparable.Matches(Status, otherT.Status)) return false;
      if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
      if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
      if( !DeepComparable.Matches(BodyStructure, otherT.BodyStructure)) return false;
      if( !DeepComparable.Matches(Period, otherT.Period)) return false;
      if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;

      return true;
    }

    public override bool IsExactly(IDeepComparable other)
    {
      var otherT = other as DeviceAssociation;
      if(otherT == null) return false;

      if(!base.IsExactly(otherT)) return false;
      if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
      if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
      if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
      if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
      if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
      if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
      if( !DeepComparable.IsExactly(BodyStructure, otherT.BodyStructure)) return false;
      if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
      if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;

      return true;
    }

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        foreach (var elem in Identifier) { if (elem != null) yield return elem; }
        if (Device != null) yield return Device;
        foreach (var elem in Category) { if (elem != null) yield return elem; }
        if (Status != null) yield return Status;
        foreach (var elem in StatusReason) { if (elem != null) yield return elem; }
        if (Subject != null) yield return Subject;
        if (BodyStructure != null) yield return BodyStructure;
        if (Period != null) yield return Period;
        foreach (var elem in Operation) { if (elem != null) yield return elem; }
      }
    }

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
        if (Device != null) yield return new ElementValue("device", Device);
        foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
        if (Status != null) yield return new ElementValue("status", Status);
        foreach (var elem in StatusReason) { if (elem != null) yield return new ElementValue("statusReason", elem); }
        if (Subject != null) yield return new ElementValue("subject", Subject);
        if (BodyStructure != null) yield return new ElementValue("bodyStructure", BodyStructure);
        if (Period != null) yield return new ElementValue("period", Period);
        foreach (var elem in Operation) { if (elem != null) yield return new ElementValue("operation", elem); }
      }
    }

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "identifier":
          value = Identifier;
          return Identifier?.Any() == true;
        case "device":
          value = Device;
          return Device is not null;
        case "category":
          value = Category;
          return Category?.Any() == true;
        case "status":
          value = Status;
          return Status is not null;
        case "statusReason":
          value = StatusReason;
          return StatusReason?.Any() == true;
        case "subject":
          value = Subject;
          return Subject is not null;
        case "bodyStructure":
          value = BodyStructure;
          return BodyStructure is not null;
        case "period":
          value = Period;
          return Period is not null;
        case "operation":
          value = Operation;
          return Operation?.Any() == true;
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
        case "device":
          Device = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "category":
          Category = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "status":
          Status = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "statusReason":
          StatusReason = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "subject":
          Subject = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "bodyStructure":
          BodyStructure = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "period":
          Period = (Hl7.Fhir.Model.Period)value;
          return this;
        case "operation":
          Operation = (List<Hl7.Fhir.Model.DeviceAssociation.OperationComponent>)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (Identifier?.Any() == true) yield return new KeyValuePair<string,object>("identifier",Identifier);
      if (Device is not null) yield return new KeyValuePair<string,object>("device",Device);
      if (Category?.Any() == true) yield return new KeyValuePair<string,object>("category",Category);
      if (Status is not null) yield return new KeyValuePair<string,object>("status",Status);
      if (StatusReason?.Any() == true) yield return new KeyValuePair<string,object>("statusReason",StatusReason);
      if (Subject is not null) yield return new KeyValuePair<string,object>("subject",Subject);
      if (BodyStructure is not null) yield return new KeyValuePair<string,object>("bodyStructure",BodyStructure);
      if (Period is not null) yield return new KeyValuePair<string,object>("period",Period);
      if (Operation?.Any() == true) yield return new KeyValuePair<string,object>("operation",Operation);
    }

  }

}

// end of file
