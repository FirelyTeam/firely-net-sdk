using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Group of multiple entities
    /// </summary>
    [FhirType("Group", IsResource=true)]
    [DataContract]
    public partial class Group : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Types of resources that are part of group
        /// </summary>
        [FhirEnumeration("GroupType")]
        public enum GroupType
        {
            /// <summary>
            /// Group contains "person" Patient resources.
            /// </summary>
            [EnumLiteral("person")]
            Person,
            /// <summary>
            /// Group contains "animal" Patient resources.
            /// </summary>
            [EnumLiteral("animal")]
            Animal,
            /// <summary>
            /// Group contains healthcare practitioner resources.
            /// </summary>
            [EnumLiteral("practitioner")]
            Practitioner,
            /// <summary>
            /// Group contains Device resources.
            /// </summary>
            [EnumLiteral("device")]
            Device,
            /// <summary>
            /// Group contains Medication resources.
            /// </summary>
            [EnumLiteral("medication")]
            Medication,
            /// <summary>
            /// Group contains Substance resources.
            /// </summary>
            [EnumLiteral("substance")]
            Substance,
        }
        
        [FhirType("GroupCharacteristicComponent")]
        [DataContract]
        public partial class GroupCharacteristicComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Kind of characteristic
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
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
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            private Hl7.Fhir.Model.Element _Value;
            
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
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Exclude
            {
                get { return ExcludeElement != null ? ExcludeElement.Value : null; }
                set
                {
                    if(value == null)
                      ExcludeElement = null; 
                    else
                      ExcludeElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Exclude");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupCharacteristicComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(ExcludeElement != null) dest.ExcludeElement = (Hl7.Fhir.Model.FhirBoolean)ExcludeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GroupCharacteristicComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupCharacteristicComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(ExcludeElement, otherT.ExcludeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupCharacteristicComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(ExcludeElement, otherT.ExcludeElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Unique id
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// person | animal | practitioner | device | medication | substance
        /// </summary>
        [FhirElement("type", InSummary=true, Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Group.GroupType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        private Code<Hl7.Fhir.Model.Group.GroupType> _TypeElement;
        
        /// <summary>
        /// person | animal | practitioner | device | medication | substance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Group.GroupType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Group.GroupType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Descriptive or actual
        /// </summary>
        [FhirElement("actual", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActualElement
        {
            get { return _ActualElement; }
            set { _ActualElement = value; OnPropertyChanged("ActualElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ActualElement;
        
        /// <summary>
        /// Descriptive or actual
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Actual
        {
            get { return ActualElement != null ? ActualElement.Value : null; }
            set
            {
                if(value == null)
                  ActualElement = null; 
                else
                  ActualElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Actual");
            }
        }
        
        /// <summary>
        /// Kind of Group members
        /// </summary>
        [FhirElement("code", InSummary=true, Order=100)]
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
        [FhirElement("name", InSummary=true, Order=110)]
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
        /// Number of members
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Integer QuantityElement
        {
            get { return _QuantityElement; }
            set { _QuantityElement = value; OnPropertyChanged("QuantityElement"); }
        }
        private Hl7.Fhir.Model.Integer _QuantityElement;
        
        /// <summary>
        /// Number of members
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Quantity
        {
            get { return QuantityElement != null ? QuantityElement.Value : null; }
            set
            {
                if(value == null)
                  QuantityElement = null; 
                else
                  QuantityElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("Quantity");
            }
        }
        
        /// <summary>
        /// Trait of group members
        /// </summary>
        [FhirElement("characteristic", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Group.GroupCharacteristicComponent> Characteristic
        {
            get { return _Characteristic; }
            set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
        }
        private List<Hl7.Fhir.Model.Group.GroupCharacteristicComponent> _Characteristic;
        
        /// <summary>
        /// Who is in group
        /// </summary>
        [FhirElement("member", Order=140)]
        [References("Patient","Practitioner","Device","Medication","Substance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Member
        {
            get { return _Member; }
            set { _Member = value; OnPropertyChanged("Member"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Member;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Group;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Group.GroupType>)TypeElement.DeepCopy();
                if(ActualElement != null) dest.ActualElement = (Hl7.Fhir.Model.FhirBoolean)ActualElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(QuantityElement != null) dest.QuantityElement = (Hl7.Fhir.Model.Integer)QuantityElement.DeepCopy();
                if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.Group.GroupCharacteristicComponent>(Characteristic.DeepCopy());
                if(Member != null) dest.Member = new List<Hl7.Fhir.Model.ResourceReference>(Member.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Group());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Group;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(ActualElement, otherT.ActualElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(QuantityElement, otherT.QuantityElement)) return false;
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
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(ActualElement, otherT.ActualElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(QuantityElement, otherT.QuantityElement)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;
            
            return true;
        }
        
    }
    
}
