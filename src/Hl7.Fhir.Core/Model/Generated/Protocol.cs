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
// Generated for FHIR v1.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Contextual set of behaviors
    /// </summary>
    [FhirType("Protocol", IsResource=true)]
    [DataContract]
    public partial class Protocol : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Protocol; } }
        [NotMapped]
        public override string TypeName { get { return "Protocol"; } }
        
        /// <summary>
        /// The lifecycle status of a Protocol
        /// (url: http://hl7.org/fhir/ValueSet/protocol-status)
        /// </summary>
        [FhirEnumeration("ProtocolStatus")]
        public enum ProtocolStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-status)
            /// </summary>
            [EnumLiteral("testing"), Description("Testing")]
            Testing,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-status)
            /// </summary>
            [EnumLiteral("review"), Description("Review")]
            Review,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-status)
            /// </summary>
            [EnumLiteral("withdrawn"), Description("Withdrawn")]
            Withdrawn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-status)
            /// </summary>
            [EnumLiteral("superseded"), Description("Superseded")]
            Superseded,
        }

        /// <summary>
        /// High-level categorization of the protocol
        /// (url: http://hl7.org/fhir/ValueSet/protocol-type)
        /// </summary>
        [FhirEnumeration("ProtocolType")]
        public enum ProtocolType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-type)
            /// </summary>
            [EnumLiteral("condition"), Description("Condition")]
            Condition,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-type)
            /// </summary>
            [EnumLiteral("device"), Description("Device")]
            Device,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-type)
            /// </summary>
            [EnumLiteral("drug"), Description("Drug")]
            Drug,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/protocol-type)
            /// </summary>
            [EnumLiteral("study"), Description("Study")]
            Study,
        }

        /// <summary>
        /// High-level categorization of the type of activity in a protocol.
        /// (url: http://hl7.org/fhir/ValueSet/activity-definition-category)
        /// </summary>
        [FhirEnumeration("ActivityDefinitionCategory")]
        public enum ActivityDefinitionCategory
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("diet"), Description("Diet")]
            Diet,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("drug"), Description("Drug")]
            Drug,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("encounter"), Description("Encounter")]
            Encounter,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("observation"), Description("Observation")]
            Observation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("procedure"), Description("Procedure")]
            Procedure,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("supply"), Description("Supply")]
            Supply,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/activity-definition-category)
            /// </summary>
            [EnumLiteral("other"), Description("Other")]
            Other,
        }

        [FhirType("StepComponent")]
        [DataContract]
        public partial class StepComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StepComponent"; } }
            
            /// <summary>
            /// Label for step
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Label for step
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
            /// Human description of activity
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Human description of activity
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
            /// How long does step last?
            /// </summary>
            [FhirElement("duration", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Duration Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Hl7.Fhir.Model.Duration _Duration;
            
            /// <summary>
            /// Rules prior to execution
            /// </summary>
            [FhirElement("precondition", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Protocol.PreconditionComponent Precondition
            {
                get { return _Precondition; }
                set { _Precondition = value; OnPropertyChanged("Precondition"); }
            }
            
            private Hl7.Fhir.Model.Protocol.PreconditionComponent _Precondition;
            
            /// <summary>
            /// Rules prior to completion
            /// </summary>
            [FhirElement("exit", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Protocol.PreconditionComponent Exit
            {
                get { return _Exit; }
                set { _Exit = value; OnPropertyChanged("Exit"); }
            }
            
            private Hl7.Fhir.Model.Protocol.PreconditionComponent _Exit;
            
            /// <summary>
            /// First activity within timepoint
            /// </summary>
            [FhirElement("firstActivity", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri FirstActivityElement
            {
                get { return _FirstActivityElement; }
                set { _FirstActivityElement = value; OnPropertyChanged("FirstActivityElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _FirstActivityElement;
            
            /// <summary>
            /// First activity within timepoint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string FirstActivity
            {
                get { return FirstActivityElement != null ? FirstActivityElement.Value : null; }
                set
                {
                    if(value == null)
                      FirstActivityElement = null; 
                    else
                      FirstActivityElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("FirstActivity");
                }
            }
            
            /// <summary>
            /// Activities that occur within timepoint
            /// </summary>
            [FhirElement("activity", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Protocol.ActivityComponent> Activity
            {
                get { if(_Activity==null) _Activity = new List<Hl7.Fhir.Model.Protocol.ActivityComponent>(); return _Activity; }
                set { _Activity = value; OnPropertyChanged("Activity"); }
            }
            
            private List<Hl7.Fhir.Model.Protocol.ActivityComponent> _Activity;
            
            /// <summary>
            /// What happens next?
            /// </summary>
            [FhirElement("next", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Protocol.NextComponent> Next
            {
                get { if(_Next==null) _Next = new List<Hl7.Fhir.Model.Protocol.NextComponent>(); return _Next; }
                set { _Next = value; OnPropertyChanged("Next"); }
            }
            
            private List<Hl7.Fhir.Model.Protocol.NextComponent> _Next;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StepComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.Duration)Duration.DeepCopy();
                    if(Precondition != null) dest.Precondition = (Hl7.Fhir.Model.Protocol.PreconditionComponent)Precondition.DeepCopy();
                    if(Exit != null) dest.Exit = (Hl7.Fhir.Model.Protocol.PreconditionComponent)Exit.DeepCopy();
                    if(FirstActivityElement != null) dest.FirstActivityElement = (Hl7.Fhir.Model.FhirUri)FirstActivityElement.DeepCopy();
                    if(Activity != null) dest.Activity = new List<Hl7.Fhir.Model.Protocol.ActivityComponent>(Activity.DeepCopy());
                    if(Next != null) dest.Next = new List<Hl7.Fhir.Model.Protocol.NextComponent>(Next.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StepComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StepComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                if( !DeepComparable.Matches(Precondition, otherT.Precondition)) return false;
                if( !DeepComparable.Matches(Exit, otherT.Exit)) return false;
                if( !DeepComparable.Matches(FirstActivityElement, otherT.FirstActivityElement)) return false;
                if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
                if( !DeepComparable.Matches(Next, otherT.Next)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StepComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                if( !DeepComparable.IsExactly(Precondition, otherT.Precondition)) return false;
                if( !DeepComparable.IsExactly(Exit, otherT.Exit)) return false;
                if( !DeepComparable.IsExactly(FirstActivityElement, otherT.FirstActivityElement)) return false;
                if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
                if( !DeepComparable.IsExactly(Next, otherT.Next)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("PreconditionComponent")]
        [DataContract]
        public partial class PreconditionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PreconditionComponent"; } }
            
            /// <summary>
            /// Description of condition
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of condition
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
            /// Condition evaluated
            /// </summary>
            [FhirElement("condition", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Protocol.ConditionComponent Condition
            {
                get { return _Condition; }
                set { _Condition = value; OnPropertyChanged("Condition"); }
            }
            
            private Hl7.Fhir.Model.Protocol.ConditionComponent _Condition;
            
            /// <summary>
            /// And conditions
            /// </summary>
            [FhirElement("intersection", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Protocol.PreconditionComponent> Intersection
            {
                get { if(_Intersection==null) _Intersection = new List<Hl7.Fhir.Model.Protocol.PreconditionComponent>(); return _Intersection; }
                set { _Intersection = value; OnPropertyChanged("Intersection"); }
            }
            
            private List<Hl7.Fhir.Model.Protocol.PreconditionComponent> _Intersection;
            
            /// <summary>
            /// Or conditions
            /// </summary>
            [FhirElement("union", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Protocol.PreconditionComponent> Union
            {
                get { if(_Union==null) _Union = new List<Hl7.Fhir.Model.Protocol.PreconditionComponent>(); return _Union; }
                set { _Union = value; OnPropertyChanged("Union"); }
            }
            
            private List<Hl7.Fhir.Model.Protocol.PreconditionComponent> _Union;
            
            /// <summary>
            /// Not conditions
            /// </summary>
            [FhirElement("exclude", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Protocol.PreconditionComponent> Exclude
            {
                get { if(_Exclude==null) _Exclude = new List<Hl7.Fhir.Model.Protocol.PreconditionComponent>(); return _Exclude; }
                set { _Exclude = value; OnPropertyChanged("Exclude"); }
            }
            
            private List<Hl7.Fhir.Model.Protocol.PreconditionComponent> _Exclude;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PreconditionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Condition != null) dest.Condition = (Hl7.Fhir.Model.Protocol.ConditionComponent)Condition.DeepCopy();
                    if(Intersection != null) dest.Intersection = new List<Hl7.Fhir.Model.Protocol.PreconditionComponent>(Intersection.DeepCopy());
                    if(Union != null) dest.Union = new List<Hl7.Fhir.Model.Protocol.PreconditionComponent>(Union.DeepCopy());
                    if(Exclude != null) dest.Exclude = new List<Hl7.Fhir.Model.Protocol.PreconditionComponent>(Exclude.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PreconditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PreconditionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
                if( !DeepComparable.Matches(Intersection, otherT.Intersection)) return false;
                if( !DeepComparable.Matches(Union, otherT.Union)) return false;
                if( !DeepComparable.Matches(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PreconditionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
                if( !DeepComparable.IsExactly(Intersection, otherT.Intersection)) return false;
                if( !DeepComparable.IsExactly(Union, otherT.Union)) return false;
                if( !DeepComparable.IsExactly(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConditionComponent")]
        [DataContract]
        public partial class ConditionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionComponent"; } }
            
            /// <summary>
            /// Observation / test / assertion
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Value needed to satisfy condition
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.SimpleQuantity),typeof(Hl7.Fhir.Model.Range))]
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
                var dest = other as ConditionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ActivityComponent")]
        [DataContract]
        public partial class ActivityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActivityComponent"; } }
            
            /// <summary>
            /// What can be done instead?
            /// </summary>
            [FhirElement("alternative", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> AlternativeElement
            {
                get { if(_AlternativeElement==null) _AlternativeElement = new List<Hl7.Fhir.Model.FhirUri>(); return _AlternativeElement; }
                set { _AlternativeElement = value; OnPropertyChanged("AlternativeElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _AlternativeElement;
            
            /// <summary>
            /// What can be done instead?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Alternative
            {
                get { return AlternativeElement != null ? AlternativeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      AlternativeElement = null; 
                    else
                      AlternativeElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Alternative");
                }
            }
            
            /// <summary>
            /// Activities that are part of this activity
            /// </summary>
            [FhirElement("component", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Protocol.ComponentComponent> Component
            {
                get { if(_Component==null) _Component = new List<Hl7.Fhir.Model.Protocol.ComponentComponent>(); return _Component; }
                set { _Component = value; OnPropertyChanged("Component"); }
            }
            
            private List<Hl7.Fhir.Model.Protocol.ComponentComponent> _Component;
            
            /// <summary>
            /// What happens next
            /// </summary>
            [FhirElement("following", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> FollowingElement
            {
                get { if(_FollowingElement==null) _FollowingElement = new List<Hl7.Fhir.Model.FhirUri>(); return _FollowingElement; }
                set { _FollowingElement = value; OnPropertyChanged("FollowingElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _FollowingElement;
            
            /// <summary>
            /// What happens next
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Following
            {
                get { return FollowingElement != null ? FollowingElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      FollowingElement = null; 
                    else
                      FollowingElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Following");
                }
            }
            
            /// <summary>
            /// Pause before start
            /// </summary>
            [FhirElement("wait", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Duration Wait
            {
                get { return _Wait; }
                set { _Wait = value; OnPropertyChanged("Wait"); }
            }
            
            private Hl7.Fhir.Model.Duration _Wait;
            
            /// <summary>
            /// Details of activity
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Protocol.DetailComponent Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private Hl7.Fhir.Model.Protocol.DetailComponent _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActivityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AlternativeElement != null) dest.AlternativeElement = new List<Hl7.Fhir.Model.FhirUri>(AlternativeElement.DeepCopy());
                    if(Component != null) dest.Component = new List<Hl7.Fhir.Model.Protocol.ComponentComponent>(Component.DeepCopy());
                    if(FollowingElement != null) dest.FollowingElement = new List<Hl7.Fhir.Model.FhirUri>(FollowingElement.DeepCopy());
                    if(Wait != null) dest.Wait = (Hl7.Fhir.Model.Duration)Wait.DeepCopy();
                    if(Detail != null) dest.Detail = (Hl7.Fhir.Model.Protocol.DetailComponent)Detail.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActivityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActivityComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AlternativeElement, otherT.AlternativeElement)) return false;
                if( !DeepComparable.Matches(Component, otherT.Component)) return false;
                if( !DeepComparable.Matches(FollowingElement, otherT.FollowingElement)) return false;
                if( !DeepComparable.Matches(Wait, otherT.Wait)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActivityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AlternativeElement, otherT.AlternativeElement)) return false;
                if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
                if( !DeepComparable.IsExactly(FollowingElement, otherT.FollowingElement)) return false;
                if( !DeepComparable.IsExactly(Wait, otherT.Wait)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ComponentComponent")]
        [DataContract]
        public partial class ComponentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ComponentComponent"; } }
            
            /// <summary>
            /// Order of occurrence
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _SequenceElement;
            
            /// <summary>
            /// Order of occurrence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Component activity
            /// </summary>
            [FhirElement("activity", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ActivityElement
            {
                get { return _ActivityElement; }
                set { _ActivityElement = value; OnPropertyChanged("ActivityElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _ActivityElement;
            
            /// <summary>
            /// Component activity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Activity
            {
                get { return ActivityElement != null ? ActivityElement.Value : null; }
                set
                {
                    if(value == null)
                      ActivityElement = null; 
                    else
                      ActivityElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Activity");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComponentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                    if(ActivityElement != null) dest.ActivityElement = (Hl7.Fhir.Model.FhirUri)ActivityElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ComponentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(ActivityElement, otherT.ActivityElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(ActivityElement, otherT.ActivityElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// diet | drug | encounter | observation +
            /// </summary>
            [FhirElement("category", InSummary=true, Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Protocol.ActivityDefinitionCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Protocol.ActivityDefinitionCategory> _CategoryElement;
            
            /// <summary>
            /// diet | drug | encounter | observation +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Protocol.ActivityDefinitionCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if(value == null)
                      CategoryElement = null; 
                    else
                      CategoryElement = new Code<Hl7.Fhir.Model.Protocol.ActivityDefinitionCategory>(value);
                    OnPropertyChanged("Category");
                }
            }
            
            /// <summary>
            /// Detail type of activity
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// When activity is to occur
            /// </summary>
            [FhirElement("timing", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Timing))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Where it should happen
            /// </summary>
            [FhirElement("location", InSummary=true, Order=70)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Location;
            
            /// <summary>
            /// Who's responsible?
            /// </summary>
            [FhirElement("performer", InSummary=true, Order=80)]
            [References("Practitioner","Organization","RelatedPerson","Patient")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Performer
            {
                get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Performer;
            
            /// <summary>
            /// What's administered/supplied
            /// </summary>
            [FhirElement("product", InSummary=true, Order=90)]
            [References("Medication","Substance")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Product
            {
                get { return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Product;
            
            /// <summary>
            /// How much is administered/consumed/supplied
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Extra info on activity occurrence
            /// </summary>
            [FhirElement("description", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Extra info on activity occurrence
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.Protocol.ActivityDefinitionCategory>)CategoryElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                    if(Product != null) dest.Product = (Hl7.Fhir.Model.ResourceReference)Product.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("NextComponent")]
        [DataContract]
        public partial class NextComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "NextComponent"; } }
            
            /// <summary>
            /// Description of what happens next
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of what happens next
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
            /// Id of following step
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ReferenceElement
            {
                get { return _ReferenceElement; }
                set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _ReferenceElement;
            
            /// <summary>
            /// Id of following step
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Reference
            {
                get { return ReferenceElement != null ? ReferenceElement.Value : null; }
                set
                {
                    if(value == null)
                      ReferenceElement = null; 
                    else
                      ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Reference");
                }
            }
            
            /// <summary>
            /// Condition in which next step is executed
            /// </summary>
            [FhirElement("condition", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Protocol.PreconditionComponent Condition
            {
                get { return _Condition; }
                set { _Condition = value; OnPropertyChanged("Condition"); }
            }
            
            private Hl7.Fhir.Model.Protocol.PreconditionComponent _Condition;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NextComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
                    if(Condition != null) dest.Condition = (Hl7.Fhir.Model.Protocol.PreconditionComponent)Condition.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NextComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NextComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
                if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NextComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
                if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Unique Id for this particular protocol
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
        /// Name of protocol
        /// </summary>
        [FhirElement("title", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name of protocol
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if(value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// draft | testing | review | active | withdrawn | superseded
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Protocol.ProtocolStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Protocol.ProtocolStatus> _StatusElement;
        
        /// <summary>
        /// draft | testing | review | active | withdrawn | superseded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Protocol.ProtocolStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Protocol.ProtocolStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// condition | device | drug | study
        /// </summary>
        [FhirElement("type", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Protocol.ProtocolType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Protocol.ProtocolType> _TypeElement;
        
        /// <summary>
        /// condition | device | drug | study
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Protocol.ProtocolType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Protocol.ProtocolType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// What does protocol deal with?
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=130)]
        [References("Condition","Device","Medication")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// To whom does Protocol apply?
        /// </summary>
        [FhirElement("group", InSummary=true, Order=140)]
        [References("Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Group
        {
            get { return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Group;
        
        /// <summary>
        /// When is protocol to be used?
        /// </summary>
        [FhirElement("purpose", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PurposeElement;
        
        /// <summary>
        /// When is protocol to be used?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if(value == null)
                  PurposeElement = null; 
                else
                  PurposeElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// Who wrote protocol?
        /// </summary>
        [FhirElement("author", InSummary=true, Order=160)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// What's done as part of protocol
        /// </summary>
        [FhirElement("step", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Protocol.StepComponent> Step
        {
            get { if(_Step==null) _Step = new List<Hl7.Fhir.Model.Protocol.StepComponent>(); return _Step; }
            set { _Step = value; OnPropertyChanged("Step"); }
        }
        
        private List<Hl7.Fhir.Model.Protocol.StepComponent> _Step;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Protocol;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Protocol.ProtocolStatus>)StatusElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Protocol.ProtocolType>)TypeElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Group != null) dest.Group = (Hl7.Fhir.Model.ResourceReference)Group.DeepCopy();
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.FhirString)PurposeElement.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Step != null) dest.Step = new List<Hl7.Fhir.Model.Protocol.StepComponent>(Step.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Protocol());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Protocol;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Step, otherT.Step)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Protocol;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Step, otherT.Step)) return false;
            
            return true;
        }
        
    }
    
}
