using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about patient's relatives, relevant for patient
    /// </summary>
    [FhirType("FamilyMemberHistory", IsResource=true)]
    [DataContract]
    public partial class FamilyMemberHistory : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.FamilyMemberHistory; } }
        [NotMapped]
        public override string TypeName { get { return "FamilyMemberHistory"; } }
        
        [FhirType("FamilyMemberHistoryConditionComponent")]
        [DataContract]
        public partial class FamilyMemberHistoryConditionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FamilyMemberHistoryConditionComponent"; } }
            
            /// <summary>
            /// Condition suffered by relation
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
            /// deceased | permanent disability | etc.
            /// </summary>
            [FhirElement("outcome", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Outcome
            {
                get { return _Outcome; }
                set { _Outcome = value; OnPropertyChanged("Outcome"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Outcome;
            
            /// <summary>
            /// When condition first manifested
            /// </summary>
            [FhirElement("onset", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Onset
            {
                get { return _Onset; }
                set { _Onset = value; OnPropertyChanged("Onset"); }
            }
            
            private Hl7.Fhir.Model.Element _Onset;
            
            /// <summary>
            /// Extra information about condition
            /// </summary>
            [FhirElement("note", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NoteElement
            {
                get { return _NoteElement; }
                set { _NoteElement = value; OnPropertyChanged("NoteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NoteElement;
            
            /// <summary>
            /// Extra information about condition
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Note
            {
                get { return NoteElement != null ? NoteElement.Value : null; }
                set
                {
                    if(value == null)
                      NoteElement = null; 
                    else
                      NoteElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Note");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FamilyMemberHistoryConditionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                    if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                    if(NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FamilyMemberHistoryConditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FamilyMemberHistoryConditionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
                if( !DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FamilyMemberHistoryConditionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
                if( !DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Id(s) for this record
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Patient history is about
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=100)]
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
        /// When history was captured/updated
        /// </summary>
        [FhirElement("date", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When history was captured/updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// The family member described
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// The family member described
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
        /// Relationship to the subject
        /// </summary>
        [FhirElement("relationship", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; OnPropertyChanged("Relationship"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Relationship;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        [FhirElement("gender", InSummary=true, Order=140)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdministrativeGender> GenderElement
        {
            get { return _GenderElement; }
            set { _GenderElement = value; OnPropertyChanged("GenderElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AdministrativeGender> _GenderElement;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdministrativeGender? Gender
        {
            get { return GenderElement != null ? GenderElement.Value : null; }
            set
            {
                if(value == null)
                  GenderElement = null; 
                else
                  GenderElement = new Code<Hl7.Fhir.Model.AdministrativeGender>(value);
                OnPropertyChanged("Gender");
            }
        }
        
        /// <summary>
        /// (approximate) date of birth
        /// </summary>
        [FhirElement("born", Order=150, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Born
        {
            get { return _Born; }
            set { _Born = value; OnPropertyChanged("Born"); }
        }
        
        private Hl7.Fhir.Model.Element _Born;
        
        /// <summary>
        /// (approximate) age
        /// </summary>
        [FhirElement("age", Order=160, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Age
        {
            get { return _Age; }
            set { _Age = value; OnPropertyChanged("Age"); }
        }
        
        private Hl7.Fhir.Model.Element _Age;
        
        /// <summary>
        /// Dead? How old/when?
        /// </summary>
        [FhirElement("deceased", Order=170, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Deceased
        {
            get { return _Deceased; }
            set { _Deceased = value; OnPropertyChanged("Deceased"); }
        }
        
        private Hl7.Fhir.Model.Element _Deceased;
        
        /// <summary>
        /// General note about related person
        /// </summary>
        [FhirElement("note", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NoteElement
        {
            get { return _NoteElement; }
            set { _NoteElement = value; OnPropertyChanged("NoteElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NoteElement;
        
        /// <summary>
        /// General note about related person
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Note
        {
            get { return NoteElement != null ? NoteElement.Value : null; }
            set
            {
                if(value == null)
                  NoteElement = null; 
                else
                  NoteElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Note");
            }
        }
        
        /// <summary>
        /// Condition that the related person had
        /// </summary>
        [FhirElement("condition", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FamilyMemberHistory.FamilyMemberHistoryConditionComponent> Condition
        {
            get { if(_Condition==null) _Condition = new List<Hl7.Fhir.Model.FamilyMemberHistory.FamilyMemberHistoryConditionComponent>(); return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private List<Hl7.Fhir.Model.FamilyMemberHistory.FamilyMemberHistoryConditionComponent> _Condition;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as FamilyMemberHistory;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
                if(Born != null) dest.Born = (Hl7.Fhir.Model.Element)Born.DeepCopy();
                if(Age != null) dest.Age = (Hl7.Fhir.Model.Element)Age.DeepCopy();
                if(Deceased != null) dest.Deceased = (Hl7.Fhir.Model.Element)Deceased.DeepCopy();
                if(NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
                if(Condition != null) dest.Condition = new List<Hl7.Fhir.Model.FamilyMemberHistory.FamilyMemberHistoryConditionComponent>(Condition.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new FamilyMemberHistory());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as FamilyMemberHistory;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.Matches(Born, otherT.Born)) return false;
            if( !DeepComparable.Matches(Age, otherT.Age)) return false;
            if( !DeepComparable.Matches(Deceased, otherT.Deceased)) return false;
            if( !DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as FamilyMemberHistory;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.IsExactly(Born, otherT.Born)) return false;
            if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
            if( !DeepComparable.IsExactly(Deceased, otherT.Deceased)) return false;
            if( !DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            
            return true;
        }
        
    }
    
}
