using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A task to be performed
    /// </summary>
    [FhirType("Task", IsResource=true)]
    [DataContract]
    public partial class Task : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Task; } }
        [NotMapped]
        public override string TypeName { get { return "Task"; } }
        
        /// <summary>
        /// The current status of the task.
        /// (url: http://hl7.org/fhir/ValueSet/task-status)
        /// </summary>
        [FhirEnumeration("TaskStatus")]
        public enum TaskStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("requested"), Description("Requested")]
            Requested,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("received"), Description("Received")]
            Received,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("accepted"), Description("Accepted")]
            Accepted,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("rejected"), Description("Rejected")]
            Rejected,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("ready"), Description("Ready")]
            Ready,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("in-progress"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("on-hold"), Description("On Hold")]
            OnHold,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("failed"), Description("Failed")]
            Failed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-status)
            /// </summary>
            [EnumLiteral("completed"), Description("Completed")]
            Completed,
        }

        /// <summary>
        /// The task's priority
        /// (url: http://hl7.org/fhir/ValueSet/task-priority)
        /// </summary>
        [FhirEnumeration("TaskPriority")]
        public enum TaskPriority
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-priority)
            /// </summary>
            [EnumLiteral("low"), Description("Low")]
            Low,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-priority)
            /// </summary>
            [EnumLiteral("normal"), Description("Normal")]
            Normal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/task-priority)
            /// </summary>
            [EnumLiteral("high"), Description("High")]
            High,
        }

        [FhirType("FulfillmentComponent")]
        [DataContract]
        public partial class FulfillmentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FulfillmentComponent"; } }
            
            /// <summary>
            /// How many times to repeat
            /// </summary>
            [FhirElement("repetitions", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt RepetitionsElement
            {
                get { return _RepetitionsElement; }
                set { _RepetitionsElement = value; OnPropertyChanged("RepetitionsElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _RepetitionsElement;
            
            /// <summary>
            /// How many times to repeat
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Repetitions
            {
                get { return RepetitionsElement != null ? RepetitionsElement.Value : null; }
                set
                {
                    if(value == null)
                      RepetitionsElement = null; 
                    else
                      RepetitionsElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Repetitions");
                }
            }
            
            /// <summary>
            /// Over what time-period is fulfillment sought
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
            /// For whom is fulfillment sought?
            /// </summary>
            [FhirElement("recipients", Order=60)]
            [References("Patient","Practitioner","RelatedPerson","Group","Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Recipients
            {
                get { if(_Recipients==null) _Recipients = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recipients; }
                set { _Recipients = value; OnPropertyChanged("Recipients"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Recipients;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FulfillmentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RepetitionsElement != null) dest.RepetitionsElement = (Hl7.Fhir.Model.PositiveInt)RepetitionsElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Recipients != null) dest.Recipients = new List<Hl7.Fhir.Model.ResourceReference>(Recipients.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FulfillmentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FulfillmentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RepetitionsElement, otherT.RepetitionsElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Recipients, otherT.Recipients)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FulfillmentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RepetitionsElement, otherT.RepetitionsElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Recipients, otherT.Recipients)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Input Name
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Input Name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Input Value
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Count),typeof(Age),typeof(Duration),typeof(Distance),typeof(Money),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Signature),typeof(Hl7.Fhir.Model.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ContactPoint),typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("OutputComponent")]
        [DataContract]
        public partial class OutputComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OutputComponent"; } }
            
            /// <summary>
            /// Output Name
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Output Name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Output Value
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Count),typeof(Age),typeof(Duration),typeof(Distance),typeof(Money),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Signature),typeof(Hl7.Fhir.Model.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ContactPoint),typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OutputComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OutputComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OutputComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OutputComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Task Instance Identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Request fulfilled by this request
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=100)]
        [References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Composite request this is part of
        /// </summary>
        [FhirElement("requisition", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Requisition
        {
            get { return _Requisition; }
            set { _Requisition = value; OnPropertyChanged("Requisition"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Requisition;
        
        /// <summary>
        /// Composite task
        /// </summary>
        [FhirElement("parent", InSummary=true, Order=120)]
        [References("Task")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Parent
        {
            get { if(_Parent==null) _Parent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Parent;
        
        /// <summary>
        /// draft | requested | received | accepted | +
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Task.TaskStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Task.TaskStatus> _StatusElement;
        
        /// <summary>
        /// draft | requested | received | accepted | +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Task.TaskStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Task.TaskStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// E.g. "Specimen collected", "IV prepped"
        /// </summary>
        [FhirElement("businessStatus", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BusinessStatus
        {
            get { return _BusinessStatus; }
            set { _BusinessStatus = value; OnPropertyChanged("BusinessStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BusinessStatus;
        
        /// <summary>
        /// proposed | planned | actionable +
        /// </summary>
        [FhirElement("stage", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Stage
        {
            get { return _Stage; }
            set { _Stage = value; OnPropertyChanged("Stage"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Stage;
        
        /// <summary>
        /// Task Type
        /// </summary>
        [FhirElement("code", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// low | normal | high
        /// </summary>
        [FhirElement("priority", Order=180)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Task.TaskPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Task.TaskPriority> _PriorityElement;
        
        /// <summary>
        /// low | normal | high
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Task.TaskPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if(value == null)
                  PriorityElement = null; 
                else
                  PriorityElement = new Code<Hl7.Fhir.Model.Task.TaskPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// Task Description
        /// </summary>
        [FhirElement("description", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Task Description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// What task is acting on
        /// </summary>
        [FhirElement("focus", InSummary=true, Order=200)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Focus
        {
            get { return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Focus;
        
        /// <summary>
        /// Beneficiary of the Task
        /// </summary>
        [FhirElement("for", InSummary=true, Order=210)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference For
        {
            get { return _For; }
            set { _For = value; OnPropertyChanged("For"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _For;
        
        /// <summary>
        /// Healthcare event during which this task originated
        /// </summary>
        [FhirElement("context", InSummary=true, Order=220)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Task Creation Date
        /// </summary>
        [FhirElement("created", Order=230)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Task Creation Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if(value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Task Last Modified Date
        /// </summary>
        [FhirElement("lastModified", InSummary=true, Order=240)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastModifiedElement
        {
            get { return _LastModifiedElement; }
            set { _LastModifiedElement = value; OnPropertyChanged("LastModifiedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LastModifiedElement;
        
        /// <summary>
        /// Task Last Modified Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastModified
        {
            get { return LastModifiedElement != null ? LastModifiedElement.Value : null; }
            set
            {
                if(value == null)
                  LastModifiedElement = null; 
                else
                  LastModifiedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastModified");
            }
        }
        
        /// <summary>
        /// Task Creator
        /// </summary>
        [FhirElement("requester", InSummary=true, Order=250)]
        [References("Device","Organization","Patient","Practitioner","RelatedPerson")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requester;
        
        /// <summary>
        /// Task Owner
        /// </summary>
        [FhirElement("owner", InSummary=true, Order=260)]
        [References("Device","Organization","Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Owner
        {
            get { return _Owner; }
            set { _Owner = value; OnPropertyChanged("Owner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Owner;
        
        /// <summary>
        /// requester | dispatcher | scheduler | performer | monitor | manager | acquirer | reviewer
        /// </summary>
        [FhirElement("performerType", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PerformerType
        {
            get { if(_PerformerType==null) _PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PerformerType; }
            set { _PerformerType = value; OnPropertyChanged("PerformerType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PerformerType;
        
        /// <summary>
        /// Why task is needed
        /// </summary>
        [FhirElement("reason", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// Comments made about the task
        /// </summary>
        [FhirElement("note", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Constraints on fulfillment tasks
        /// </summary>
        [FhirElement("fulfillment", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.Task.FulfillmentComponent Fulfillment
        {
            get { return _Fulfillment; }
            set { _Fulfillment = value; OnPropertyChanged("Fulfillment"); }
        }
        
        private Hl7.Fhir.Model.Task.FulfillmentComponent _Fulfillment;
        
        /// <summary>
        /// Task Definition
        /// </summary>
        [FhirElement("definition", InSummary=true, Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri DefinitionElement
        {
            get { return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _DefinitionElement;
        
        /// <summary>
        /// Task Definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Definition
        {
            get { return DefinitionElement != null ? DefinitionElement.Value : null; }
            set
            {
                if(value == null)
                  DefinitionElement = null; 
                else
                  DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Definition");
            }
        }
        
        /// <summary>
        /// Task Input
        /// </summary>
        [FhirElement("input", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Task.ParameterComponent> Input
        {
            get { if(_Input==null) _Input = new List<Hl7.Fhir.Model.Task.ParameterComponent>(); return _Input; }
            set { _Input = value; OnPropertyChanged("Input"); }
        }
        
        private List<Hl7.Fhir.Model.Task.ParameterComponent> _Input;
        
        /// <summary>
        /// Task Output
        /// </summary>
        [FhirElement("output", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Task.OutputComponent> Output
        {
            get { if(_Output==null) _Output = new List<Hl7.Fhir.Model.Task.OutputComponent>(); return _Output; }
            set { _Output = value; OnPropertyChanged("Output"); }
        }
        
        private List<Hl7.Fhir.Model.Task.OutputComponent> _Output;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Task;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Requisition != null) dest.Requisition = (Hl7.Fhir.Model.Identifier)Requisition.DeepCopy();
                if(Parent != null) dest.Parent = new List<Hl7.Fhir.Model.ResourceReference>(Parent.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Task.TaskStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(BusinessStatus != null) dest.BusinessStatus = (Hl7.Fhir.Model.CodeableConcept)BusinessStatus.DeepCopy();
                if(Stage != null) dest.Stage = (Hl7.Fhir.Model.CodeableConcept)Stage.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.Task.TaskPriority>)PriorityElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Focus != null) dest.Focus = (Hl7.Fhir.Model.ResourceReference)Focus.DeepCopy();
                if(For != null) dest.For = (Hl7.Fhir.Model.ResourceReference)For.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(LastModifiedElement != null) dest.LastModifiedElement = (Hl7.Fhir.Model.FhirDateTime)LastModifiedElement.DeepCopy();
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.ResourceReference)Requester.DeepCopy();
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(PerformerType != null) dest.PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(PerformerType.DeepCopy());
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Fulfillment != null) dest.Fulfillment = (Hl7.Fhir.Model.Task.FulfillmentComponent)Fulfillment.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
                if(Input != null) dest.Input = new List<Hl7.Fhir.Model.Task.ParameterComponent>(Input.DeepCopy());
                if(Output != null) dest.Output = new List<Hl7.Fhir.Model.Task.OutputComponent>(Output.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Task());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Task;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Requisition, otherT.Requisition)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(BusinessStatus, otherT.BusinessStatus)) return false;
            if( !DeepComparable.Matches(Stage, otherT.Stage)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(For, otherT.For)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(LastModifiedElement, otherT.LastModifiedElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Fulfillment, otherT.Fulfillment)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(Input, otherT.Input)) return false;
            if( !DeepComparable.Matches(Output, otherT.Output)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Task;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Requisition, otherT.Requisition)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(BusinessStatus, otherT.BusinessStatus)) return false;
            if( !DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(For, otherT.For)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(LastModifiedElement, otherT.LastModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Fulfillment, otherT.Fulfillment)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(Input, otherT.Input)) return false;
            if( !DeepComparable.IsExactly(Output, otherT.Output)) return false;
            
            return true;
        }
        
    }
    
}
