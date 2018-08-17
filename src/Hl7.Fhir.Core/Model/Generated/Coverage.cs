using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

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

#pragma warning disable 1591 // suppress XML summary warnings 

//
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Insurance or medical plan or a payment agreement
    /// </summary>
    [FhirType("Coverage", IsResource=true)]
    [DataContract]
    public partial class Coverage : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Coverage; } }
        [NotMapped]
        public override string TypeName { get { return "Coverage"; } }
        
        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// An identifier for the group
            /// </summary>
            [FhirElement("group", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString GroupElement
            {
                get { return _GroupElement; }
                set { _GroupElement = value; OnPropertyChanged("GroupElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _GroupElement;
            
            /// <summary>
            /// An identifier for the group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Group
            {
                get { return GroupElement != null ? GroupElement.Value : null; }
                set
                {
                    if (value == null)
                        GroupElement = null; 
                    else
                        GroupElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Group");
                }
            }
            
            /// <summary>
            /// Display text for an identifier for the group
            /// </summary>
            [FhirElement("groupDisplay", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString GroupDisplayElement
            {
                get { return _GroupDisplayElement; }
                set { _GroupDisplayElement = value; OnPropertyChanged("GroupDisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _GroupDisplayElement;
            
            /// <summary>
            /// Display text for an identifier for the group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string GroupDisplay
            {
                get { return GroupDisplayElement != null ? GroupDisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        GroupDisplayElement = null; 
                    else
                        GroupDisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("GroupDisplay");
                }
            }
            
            /// <summary>
            /// An identifier for the subsection of the group
            /// </summary>
            [FhirElement("subGroup", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SubGroupElement
            {
                get { return _SubGroupElement; }
                set { _SubGroupElement = value; OnPropertyChanged("SubGroupElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SubGroupElement;
            
            /// <summary>
            /// An identifier for the subsection of the group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SubGroup
            {
                get { return SubGroupElement != null ? SubGroupElement.Value : null; }
                set
                {
                    if (value == null)
                        SubGroupElement = null; 
                    else
                        SubGroupElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SubGroup");
                }
            }
            
            /// <summary>
            /// Display text for the subsection of the group
            /// </summary>
            [FhirElement("subGroupDisplay", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SubGroupDisplayElement
            {
                get { return _SubGroupDisplayElement; }
                set { _SubGroupDisplayElement = value; OnPropertyChanged("SubGroupDisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SubGroupDisplayElement;
            
            /// <summary>
            /// Display text for the subsection of the group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SubGroupDisplay
            {
                get { return SubGroupDisplayElement != null ? SubGroupDisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        SubGroupDisplayElement = null; 
                    else
                        SubGroupDisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SubGroupDisplay");
                }
            }
            
            /// <summary>
            /// An identifier for the plan
            /// </summary>
            [FhirElement("plan", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PlanElement
            {
                get { return _PlanElement; }
                set { _PlanElement = value; OnPropertyChanged("PlanElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PlanElement;
            
            /// <summary>
            /// An identifier for the plan
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Plan
            {
                get { return PlanElement != null ? PlanElement.Value : null; }
                set
                {
                    if (value == null)
                        PlanElement = null; 
                    else
                        PlanElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Plan");
                }
            }
            
            /// <summary>
            /// Display text for the plan
            /// </summary>
            [FhirElement("planDisplay", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PlanDisplayElement
            {
                get { return _PlanDisplayElement; }
                set { _PlanDisplayElement = value; OnPropertyChanged("PlanDisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PlanDisplayElement;
            
            /// <summary>
            /// Display text for the plan
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PlanDisplay
            {
                get { return PlanDisplayElement != null ? PlanDisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        PlanDisplayElement = null; 
                    else
                        PlanDisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("PlanDisplay");
                }
            }
            
            /// <summary>
            /// An identifier for the subsection of the plan
            /// </summary>
            [FhirElement("subPlan", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SubPlanElement
            {
                get { return _SubPlanElement; }
                set { _SubPlanElement = value; OnPropertyChanged("SubPlanElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SubPlanElement;
            
            /// <summary>
            /// An identifier for the subsection of the plan
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SubPlan
            {
                get { return SubPlanElement != null ? SubPlanElement.Value : null; }
                set
                {
                    if (value == null)
                        SubPlanElement = null; 
                    else
                        SubPlanElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SubPlan");
                }
            }
            
            /// <summary>
            /// Display text for the subsection of the plan
            /// </summary>
            [FhirElement("subPlanDisplay", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SubPlanDisplayElement
            {
                get { return _SubPlanDisplayElement; }
                set { _SubPlanDisplayElement = value; OnPropertyChanged("SubPlanDisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SubPlanDisplayElement;
            
            /// <summary>
            /// Display text for the subsection of the plan
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SubPlanDisplay
            {
                get { return SubPlanDisplayElement != null ? SubPlanDisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        SubPlanDisplayElement = null; 
                    else
                        SubPlanDisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SubPlanDisplay");
                }
            }
            
            /// <summary>
            /// An identifier for the class
            /// </summary>
            [FhirElement("class", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ClassElement
            {
                get { return _ClassElement; }
                set { _ClassElement = value; OnPropertyChanged("ClassElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ClassElement;
            
            /// <summary>
            /// An identifier for the class
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Class
            {
                get { return ClassElement != null ? ClassElement.Value : null; }
                set
                {
                    if (value == null)
                        ClassElement = null; 
                    else
                        ClassElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Class");
                }
            }
            
            /// <summary>
            /// Display text for the class
            /// </summary>
            [FhirElement("classDisplay", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ClassDisplayElement
            {
                get { return _ClassDisplayElement; }
                set { _ClassDisplayElement = value; OnPropertyChanged("ClassDisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ClassDisplayElement;
            
            /// <summary>
            /// Display text for the class
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ClassDisplay
            {
                get { return ClassDisplayElement != null ? ClassDisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        ClassDisplayElement = null; 
                    else
                        ClassDisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ClassDisplay");
                }
            }
            
            /// <summary>
            /// An identifier for the subsection of the class
            /// </summary>
            [FhirElement("subClass", InSummary=true, Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SubClassElement
            {
                get { return _SubClassElement; }
                set { _SubClassElement = value; OnPropertyChanged("SubClassElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SubClassElement;
            
            /// <summary>
            /// An identifier for the subsection of the class
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SubClass
            {
                get { return SubClassElement != null ? SubClassElement.Value : null; }
                set
                {
                    if (value == null)
                        SubClassElement = null; 
                    else
                        SubClassElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SubClass");
                }
            }
            
            /// <summary>
            /// Display text for the subsection of the subclass
            /// </summary>
            [FhirElement("subClassDisplay", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SubClassDisplayElement
            {
                get { return _SubClassDisplayElement; }
                set { _SubClassDisplayElement = value; OnPropertyChanged("SubClassDisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SubClassDisplayElement;
            
            /// <summary>
            /// Display text for the subsection of the subclass
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SubClassDisplay
            {
                get { return SubClassDisplayElement != null ? SubClassDisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        SubClassDisplayElement = null; 
                    else
                        SubClassDisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SubClassDisplay");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(GroupElement != null) dest.GroupElement = (Hl7.Fhir.Model.FhirString)GroupElement.DeepCopy();
                    if(GroupDisplayElement != null) dest.GroupDisplayElement = (Hl7.Fhir.Model.FhirString)GroupDisplayElement.DeepCopy();
                    if(SubGroupElement != null) dest.SubGroupElement = (Hl7.Fhir.Model.FhirString)SubGroupElement.DeepCopy();
                    if(SubGroupDisplayElement != null) dest.SubGroupDisplayElement = (Hl7.Fhir.Model.FhirString)SubGroupDisplayElement.DeepCopy();
                    if(PlanElement != null) dest.PlanElement = (Hl7.Fhir.Model.FhirString)PlanElement.DeepCopy();
                    if(PlanDisplayElement != null) dest.PlanDisplayElement = (Hl7.Fhir.Model.FhirString)PlanDisplayElement.DeepCopy();
                    if(SubPlanElement != null) dest.SubPlanElement = (Hl7.Fhir.Model.FhirString)SubPlanElement.DeepCopy();
                    if(SubPlanDisplayElement != null) dest.SubPlanDisplayElement = (Hl7.Fhir.Model.FhirString)SubPlanDisplayElement.DeepCopy();
                    if(ClassElement != null) dest.ClassElement = (Hl7.Fhir.Model.FhirString)ClassElement.DeepCopy();
                    if(ClassDisplayElement != null) dest.ClassDisplayElement = (Hl7.Fhir.Model.FhirString)ClassDisplayElement.DeepCopy();
                    if(SubClassElement != null) dest.SubClassElement = (Hl7.Fhir.Model.FhirString)SubClassElement.DeepCopy();
                    if(SubClassDisplayElement != null) dest.SubClassDisplayElement = (Hl7.Fhir.Model.FhirString)SubClassDisplayElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(GroupElement, otherT.GroupElement)) return false;
                if( !DeepComparable.Matches(GroupDisplayElement, otherT.GroupDisplayElement)) return false;
                if( !DeepComparable.Matches(SubGroupElement, otherT.SubGroupElement)) return false;
                if( !DeepComparable.Matches(SubGroupDisplayElement, otherT.SubGroupDisplayElement)) return false;
                if( !DeepComparable.Matches(PlanElement, otherT.PlanElement)) return false;
                if( !DeepComparable.Matches(PlanDisplayElement, otherT.PlanDisplayElement)) return false;
                if( !DeepComparable.Matches(SubPlanElement, otherT.SubPlanElement)) return false;
                if( !DeepComparable.Matches(SubPlanDisplayElement, otherT.SubPlanDisplayElement)) return false;
                if( !DeepComparable.Matches(ClassElement, otherT.ClassElement)) return false;
                if( !DeepComparable.Matches(ClassDisplayElement, otherT.ClassDisplayElement)) return false;
                if( !DeepComparable.Matches(SubClassElement, otherT.SubClassElement)) return false;
                if( !DeepComparable.Matches(SubClassDisplayElement, otherT.SubClassDisplayElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(GroupElement, otherT.GroupElement)) return false;
                if( !DeepComparable.IsExactly(GroupDisplayElement, otherT.GroupDisplayElement)) return false;
                if( !DeepComparable.IsExactly(SubGroupElement, otherT.SubGroupElement)) return false;
                if( !DeepComparable.IsExactly(SubGroupDisplayElement, otherT.SubGroupDisplayElement)) return false;
                if( !DeepComparable.IsExactly(PlanElement, otherT.PlanElement)) return false;
                if( !DeepComparable.IsExactly(PlanDisplayElement, otherT.PlanDisplayElement)) return false;
                if( !DeepComparable.IsExactly(SubPlanElement, otherT.SubPlanElement)) return false;
                if( !DeepComparable.IsExactly(SubPlanDisplayElement, otherT.SubPlanDisplayElement)) return false;
                if( !DeepComparable.IsExactly(ClassElement, otherT.ClassElement)) return false;
                if( !DeepComparable.IsExactly(ClassDisplayElement, otherT.ClassDisplayElement)) return false;
                if( !DeepComparable.IsExactly(SubClassElement, otherT.SubClassElement)) return false;
                if( !DeepComparable.IsExactly(SubClassDisplayElement, otherT.SubClassDisplayElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (GroupElement != null) yield return GroupElement;
                    if (GroupDisplayElement != null) yield return GroupDisplayElement;
                    if (SubGroupElement != null) yield return SubGroupElement;
                    if (SubGroupDisplayElement != null) yield return SubGroupDisplayElement;
                    if (PlanElement != null) yield return PlanElement;
                    if (PlanDisplayElement != null) yield return PlanDisplayElement;
                    if (SubPlanElement != null) yield return SubPlanElement;
                    if (SubPlanDisplayElement != null) yield return SubPlanDisplayElement;
                    if (ClassElement != null) yield return ClassElement;
                    if (ClassDisplayElement != null) yield return ClassDisplayElement;
                    if (SubClassElement != null) yield return SubClassElement;
                    if (SubClassDisplayElement != null) yield return SubClassDisplayElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (GroupElement != null) yield return new ElementValue("group", GroupElement);
                    if (GroupDisplayElement != null) yield return new ElementValue("groupDisplay", GroupDisplayElement);
                    if (SubGroupElement != null) yield return new ElementValue("subGroup", SubGroupElement);
                    if (SubGroupDisplayElement != null) yield return new ElementValue("subGroupDisplay", SubGroupDisplayElement);
                    if (PlanElement != null) yield return new ElementValue("plan", PlanElement);
                    if (PlanDisplayElement != null) yield return new ElementValue("planDisplay", PlanDisplayElement);
                    if (SubPlanElement != null) yield return new ElementValue("subPlan", SubPlanElement);
                    if (SubPlanDisplayElement != null) yield return new ElementValue("subPlanDisplay", SubPlanDisplayElement);
                    if (ClassElement != null) yield return new ElementValue("class", ClassElement);
                    if (ClassDisplayElement != null) yield return new ElementValue("classDisplay", ClassDisplayElement);
                    if (SubClassElement != null) yield return new ElementValue("subClass", SubClassElement);
                    if (SubClassDisplayElement != null) yield return new ElementValue("subClassDisplay", SubClassDisplayElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// The primary coverage ID
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Type of coverage such as medical or accident
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Owner of the policy
        /// </summary>
        [FhirElement("policyHolder", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PolicyHolder
        {
            get { return _PolicyHolder; }
            set { _PolicyHolder = value; OnPropertyChanged("PolicyHolder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PolicyHolder;
        
        /// <summary>
        /// Subscriber to the policy
        /// </summary>
        [FhirElement("subscriber", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subscriber
        {
            get { return _Subscriber; }
            set { _Subscriber = value; OnPropertyChanged("Subscriber"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subscriber;
        
        /// <summary>
        /// ID assigned to the Subscriber
        /// </summary>
        [FhirElement("subscriberId", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SubscriberIdElement
        {
            get { return _SubscriberIdElement; }
            set { _SubscriberIdElement = value; OnPropertyChanged("SubscriberIdElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SubscriberIdElement;
        
        /// <summary>
        /// ID assigned to the Subscriber
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string SubscriberId
        {
            get { return SubscriberIdElement != null ? SubscriberIdElement.Value : null; }
            set
            {
                if (value == null)
                  SubscriberIdElement = null; 
                else
                  SubscriberIdElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("SubscriberId");
            }
        }
        
        /// <summary>
        /// Plan Beneficiary
        /// </summary>
        [FhirElement("beneficiary", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Beneficiary
        {
            get { return _Beneficiary; }
            set { _Beneficiary = value; OnPropertyChanged("Beneficiary"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Beneficiary;
        
        /// <summary>
        /// Beneficiary relationship to the Subscriber
        /// </summary>
        [FhirElement("relationship", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; OnPropertyChanged("Relationship"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Relationship;
        
        /// <summary>
        /// Coverage start and end dates
        /// </summary>
        [FhirElement("period", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Identifier for the plan or agreement issuer
        /// </summary>
        [FhirElement("payor", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Organization","Patient","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Payor
        {
            get { if(_Payor==null) _Payor = new List<Hl7.Fhir.Model.ResourceReference>(); return _Payor; }
            set { _Payor = value; OnPropertyChanged("Payor"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Payor;
        
        /// <summary>
        /// Additional coverage classifications
        /// </summary>
        [FhirElement("grouping", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Coverage.GroupComponent Grouping
        {
            get { return _Grouping; }
            set { _Grouping = value; OnPropertyChanged("Grouping"); }
        }
        
        private Hl7.Fhir.Model.Coverage.GroupComponent _Grouping;
        
        /// <summary>
        /// Dependent number
        /// </summary>
        [FhirElement("dependent", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DependentElement
        {
            get { return _DependentElement; }
            set { _DependentElement = value; OnPropertyChanged("DependentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DependentElement;
        
        /// <summary>
        /// Dependent number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Dependent
        {
            get { return DependentElement != null ? DependentElement.Value : null; }
            set
            {
                if (value == null)
                  DependentElement = null; 
                else
                  DependentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Dependent");
            }
        }
        
        /// <summary>
        /// The plan instance or sequence counter
        /// </summary>
        [FhirElement("sequence", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SequenceElement
        {
            get { return _SequenceElement; }
            set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SequenceElement;
        
        /// <summary>
        /// The plan instance or sequence counter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Sequence
        {
            get { return SequenceElement != null ? SequenceElement.Value : null; }
            set
            {
                if (value == null)
                  SequenceElement = null; 
                else
                  SequenceElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Sequence");
            }
        }
        
        /// <summary>
        /// Relative order of the coverage
        /// </summary>
        [FhirElement("order", InSummary=true, Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt OrderElement
        {
            get { return _OrderElement; }
            set { _OrderElement = value; OnPropertyChanged("OrderElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _OrderElement;
        
        /// <summary>
        /// Relative order of the coverage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Order
        {
            get { return OrderElement != null ? OrderElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  OrderElement = null; 
                else
                  OrderElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Order");
            }
        }
        
        /// <summary>
        /// Insurer network
        /// </summary>
        [FhirElement("network", InSummary=true, Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NetworkElement
        {
            get { return _NetworkElement; }
            set { _NetworkElement = value; OnPropertyChanged("NetworkElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NetworkElement;
        
        /// <summary>
        /// Insurer network
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Network
        {
            get { return NetworkElement != null ? NetworkElement.Value : null; }
            set
            {
                if (value == null)
                  NetworkElement = null; 
                else
                  NetworkElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Network");
            }
        }
        
        /// <summary>
        /// Contract details
        /// </summary>
        [FhirElement("contract", Order=240)]
        [CLSCompliant(false)]
		[References("Contract")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Contract
        {
            get { if(_Contract==null) _Contract = new List<Hl7.Fhir.Model.ResourceReference>(); return _Contract; }
            set { _Contract = value; OnPropertyChanged("Contract"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Contract;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Coverage;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(PolicyHolder != null) dest.PolicyHolder = (Hl7.Fhir.Model.ResourceReference)PolicyHolder.DeepCopy();
                if(Subscriber != null) dest.Subscriber = (Hl7.Fhir.Model.ResourceReference)Subscriber.DeepCopy();
                if(SubscriberIdElement != null) dest.SubscriberIdElement = (Hl7.Fhir.Model.FhirString)SubscriberIdElement.DeepCopy();
                if(Beneficiary != null) dest.Beneficiary = (Hl7.Fhir.Model.ResourceReference)Beneficiary.DeepCopy();
                if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Payor != null) dest.Payor = new List<Hl7.Fhir.Model.ResourceReference>(Payor.DeepCopy());
                if(Grouping != null) dest.Grouping = (Hl7.Fhir.Model.Coverage.GroupComponent)Grouping.DeepCopy();
                if(DependentElement != null) dest.DependentElement = (Hl7.Fhir.Model.FhirString)DependentElement.DeepCopy();
                if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.FhirString)SequenceElement.DeepCopy();
                if(OrderElement != null) dest.OrderElement = (Hl7.Fhir.Model.PositiveInt)OrderElement.DeepCopy();
                if(NetworkElement != null) dest.NetworkElement = (Hl7.Fhir.Model.FhirString)NetworkElement.DeepCopy();
                if(Contract != null) dest.Contract = new List<Hl7.Fhir.Model.ResourceReference>(Contract.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Coverage());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Coverage;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(PolicyHolder, otherT.PolicyHolder)) return false;
            if( !DeepComparable.Matches(Subscriber, otherT.Subscriber)) return false;
            if( !DeepComparable.Matches(SubscriberIdElement, otherT.SubscriberIdElement)) return false;
            if( !DeepComparable.Matches(Beneficiary, otherT.Beneficiary)) return false;
            if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Payor, otherT.Payor)) return false;
            if( !DeepComparable.Matches(Grouping, otherT.Grouping)) return false;
            if( !DeepComparable.Matches(DependentElement, otherT.DependentElement)) return false;
            if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
            if( !DeepComparable.Matches(OrderElement, otherT.OrderElement)) return false;
            if( !DeepComparable.Matches(NetworkElement, otherT.NetworkElement)) return false;
            if( !DeepComparable.Matches(Contract, otherT.Contract)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Coverage;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(PolicyHolder, otherT.PolicyHolder)) return false;
            if( !DeepComparable.IsExactly(Subscriber, otherT.Subscriber)) return false;
            if( !DeepComparable.IsExactly(SubscriberIdElement, otherT.SubscriberIdElement)) return false;
            if( !DeepComparable.IsExactly(Beneficiary, otherT.Beneficiary)) return false;
            if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Payor, otherT.Payor)) return false;
            if( !DeepComparable.IsExactly(Grouping, otherT.Grouping)) return false;
            if( !DeepComparable.IsExactly(DependentElement, otherT.DependentElement)) return false;
            if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
            if( !DeepComparable.IsExactly(OrderElement, otherT.OrderElement)) return false;
            if( !DeepComparable.IsExactly(NetworkElement, otherT.NetworkElement)) return false;
            if( !DeepComparable.IsExactly(Contract, otherT.Contract)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Type != null) yield return Type;
				if (PolicyHolder != null) yield return PolicyHolder;
				if (Subscriber != null) yield return Subscriber;
				if (SubscriberIdElement != null) yield return SubscriberIdElement;
				if (Beneficiary != null) yield return Beneficiary;
				if (Relationship != null) yield return Relationship;
				if (Period != null) yield return Period;
				foreach (var elem in Payor) { if (elem != null) yield return elem; }
				if (Grouping != null) yield return Grouping;
				if (DependentElement != null) yield return DependentElement;
				if (SequenceElement != null) yield return SequenceElement;
				if (OrderElement != null) yield return OrderElement;
				if (NetworkElement != null) yield return NetworkElement;
				foreach (var elem in Contract) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (PolicyHolder != null) yield return new ElementValue("policyHolder", PolicyHolder);
                if (Subscriber != null) yield return new ElementValue("subscriber", Subscriber);
                if (SubscriberIdElement != null) yield return new ElementValue("subscriberId", SubscriberIdElement);
                if (Beneficiary != null) yield return new ElementValue("beneficiary", Beneficiary);
                if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                if (Period != null) yield return new ElementValue("period", Period);
                foreach (var elem in Payor) { if (elem != null) yield return new ElementValue("payor", elem); }
                if (Grouping != null) yield return new ElementValue("grouping", Grouping);
                if (DependentElement != null) yield return new ElementValue("dependent", DependentElement);
                if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                if (OrderElement != null) yield return new ElementValue("order", OrderElement);
                if (NetworkElement != null) yield return new ElementValue("network", NetworkElement);
                foreach (var elem in Contract) { if (elem != null) yield return new ElementValue("contract", elem); }
            }
        }

    }
    
}
