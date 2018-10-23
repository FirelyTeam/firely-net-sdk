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
    /// An interaction during which services are provided to the patient
    /// </summary>
    [FhirType("Encounter", IsResource=true)]
    [DataContract]
    public partial class Encounter : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Encounter; } }
        [NotMapped]
        public override string TypeName { get { return "Encounter"; } }
        
        /// <summary>
        /// Current state of the encounter
        /// (url: http://hl7.org/fhir/ValueSet/encounter-status)
        /// </summary>
        [FhirEnumeration("EncounterStatus")]
        public enum EncounterStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("planned", "http://hl7.org/fhir/encounter-status"), Description("Planned")]
            Planned,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("arrived", "http://hl7.org/fhir/encounter-status"), Description("Arrived")]
            Arrived,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("triaged", "http://hl7.org/fhir/encounter-status"), Description("Triaged")]
            Triaged,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/encounter-status"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("onleave", "http://hl7.org/fhir/encounter-status"), Description("On Leave")]
            Onleave,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("finished", "http://hl7.org/fhir/encounter-status"), Description("Finished")]
            Finished,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/encounter-status"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/encounter-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-status)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/encounter-status"), Description("Unknown")]
            Unknown,
        }

        /// <summary>
        /// The status of the location.
        /// (url: http://hl7.org/fhir/ValueSet/encounter-location-status)
        /// </summary>
        [FhirEnumeration("EncounterLocationStatus")]
        public enum EncounterLocationStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-location-status)
            /// </summary>
            [EnumLiteral("planned", "http://hl7.org/fhir/encounter-location-status"), Description("Planned")]
            Planned,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-location-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/encounter-location-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-location-status)
            /// </summary>
            [EnumLiteral("reserved", "http://hl7.org/fhir/encounter-location-status"), Description("Reserved")]
            Reserved,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/encounter-location-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/encounter-location-status"), Description("Completed")]
            Completed,
        }

        [FhirType("StatusHistoryComponent")]
        [DataContract]
        public partial class StatusHistoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "StatusHistoryComponent"; } }
            
            /// <summary>
            /// planned | arrived | triaged | in-progress | onleave | finished | cancelled +
            /// </summary>
            [FhirElement("status", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Encounter.EncounterStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Encounter.EncounterStatus> _StatusElement;
            
            /// <summary>
            /// planned | arrived | triaged | in-progress | onleave | finished | cancelled +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Encounter.EncounterStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StatusElement = null; 
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.Encounter.EncounterStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// The time that the episode was in the specified status
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
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterStatus>)StatusElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StatusHistoryComponent());
            }
            
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


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (StatusElement != null) yield return StatusElement;
                    if (Period != null) yield return Period;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }

            
        }
        
        
        [FhirType("ClassHistoryComponent")]
        [DataContract]
        public partial class ClassHistoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ClassHistoryComponent"; } }
            
            /// <summary>
            /// inpatient | outpatient | ambulatory | emergency +
            /// </summary>
            [FhirElement("class", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Class
            {
                get { return _Class; }
                set { _Class = value; OnPropertyChanged("Class"); }
            }
            
            private Hl7.Fhir.Model.Coding _Class;
            
            /// <summary>
            /// The time that the episode was in the specified class
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
                var dest = other as ClassHistoryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Class != null) dest.Class = (Hl7.Fhir.Model.Coding)Class.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ClassHistoryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ClassHistoryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Class, otherT.Class)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ClassHistoryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Class != null) yield return Class;
                    if (Period != null) yield return Period;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Class != null) yield return new ElementValue("class", Class);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }

            
        }
        
        
        [FhirType("ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// Role of participant in encounter
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Period of time during the encounter that the participant participated
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
            /// Persons involved in the encounter other than the patient
            /// </summary>
            [FhirElement("individual", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("Practitioner","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Individual
            {
                get { return _Individual; }
                set { _Individual = value; OnPropertyChanged("Individual"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Individual;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Individual != null) dest.Individual = (Hl7.Fhir.Model.ResourceReference)Individual.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Individual, otherT.Individual)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Individual, otherT.Individual)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    if (Period != null) yield return Period;
                    if (Individual != null) yield return Individual;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    if (Period != null) yield return new ElementValue("period", Period);
                    if (Individual != null) yield return new ElementValue("individual", Individual);
                }
            }

            
        }
        
        
        [FhirType("DiagnosisComponent")]
        [DataContract]
        public partial class DiagnosisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DiagnosisComponent"; } }
            
            /// <summary>
            /// Reason the encounter takes place (resource)
            /// </summary>
            [FhirElement("condition", Order=40)]
            [CLSCompliant(false)]
			[References("Condition","Procedure")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Condition
            {
                get { return _Condition; }
                set { _Condition = value; OnPropertyChanged("Condition"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Condition;
            
            /// <summary>
            /// Role that this diagnosis has within the encounter (e.g. admission, billing, discharge …)
            /// </summary>
            [FhirElement("role", Order=50)]
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
            [FhirElement("rank", Order=60)]
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
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Rank
            {
                get { return RankElement != null ? RankElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        RankElement = null; 
                    else
                        RankElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Rank");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Condition != null) dest.Condition = (Hl7.Fhir.Model.ResourceReference)Condition.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(RankElement != null) dest.RankElement = (Hl7.Fhir.Model.PositiveInt)RankElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosisComponent());
            }
            
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


            [NotMapped]
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

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Condition != null) yield return new ElementValue("condition", Condition);
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (RankElement != null) yield return new ElementValue("rank", RankElement);
                }
            }

            
        }
        
        
        [FhirType("HospitalizationComponent")]
        [DataContract]
        public partial class HospitalizationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "HospitalizationComponent"; } }
            
            /// <summary>
            /// Pre-admission identifier
            /// </summary>
            [FhirElement("preAdmissionIdentifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier PreAdmissionIdentifier
            {
                get { return _PreAdmissionIdentifier; }
                set { _PreAdmissionIdentifier = value; OnPropertyChanged("PreAdmissionIdentifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _PreAdmissionIdentifier;
            
            /// <summary>
            /// The location from which the patient came before admission
            /// </summary>
            [FhirElement("origin", Order=50)]
            [CLSCompliant(false)]
			[References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Origin
            {
                get { return _Origin; }
                set { _Origin = value; OnPropertyChanged("Origin"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Origin;
            
            /// <summary>
            /// From where patient was admitted (physician referral, transfer)
            /// </summary>
            [FhirElement("admitSource", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdmitSource
            {
                get { return _AdmitSource; }
                set { _AdmitSource = value; OnPropertyChanged("AdmitSource"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AdmitSource;
            
            /// <summary>
            /// The type of hospital re-admission that has occurred (if any). If the value is absent, then this is not identified as a readmission
            /// </summary>
            [FhirElement("reAdmission", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ReAdmission
            {
                get { return _ReAdmission; }
                set { _ReAdmission = value; OnPropertyChanged("ReAdmission"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ReAdmission;
            
            /// <summary>
            /// Diet preferences reported by the patient
            /// </summary>
            [FhirElement("dietPreference", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> DietPreference
            {
                get { if(_DietPreference==null) _DietPreference = new List<Hl7.Fhir.Model.CodeableConcept>(); return _DietPreference; }
                set { _DietPreference = value; OnPropertyChanged("DietPreference"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _DietPreference;
            
            /// <summary>
            /// Special courtesies (VIP, board member)
            /// </summary>
            [FhirElement("specialCourtesy", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialCourtesy
            {
                get { if(_SpecialCourtesy==null) _SpecialCourtesy = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SpecialCourtesy; }
                set { _SpecialCourtesy = value; OnPropertyChanged("SpecialCourtesy"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SpecialCourtesy;
            
            /// <summary>
            /// Wheelchair, translator, stretcher, etc.
            /// </summary>
            [FhirElement("specialArrangement", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialArrangement
            {
                get { if(_SpecialArrangement==null) _SpecialArrangement = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SpecialArrangement; }
                set { _SpecialArrangement = value; OnPropertyChanged("SpecialArrangement"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SpecialArrangement;
            
            /// <summary>
            /// Location to which the patient is discharged
            /// </summary>
            [FhirElement("destination", Order=110)]
            [CLSCompliant(false)]
			[References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination
            {
                get { return _Destination; }
                set { _Destination = value; OnPropertyChanged("Destination"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Destination;
            
            /// <summary>
            /// Category or kind of location after discharge
            /// </summary>
            [FhirElement("dischargeDisposition", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DischargeDisposition
            {
                get { return _DischargeDisposition; }
                set { _DischargeDisposition = value; OnPropertyChanged("DischargeDisposition"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DischargeDisposition;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HospitalizationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PreAdmissionIdentifier != null) dest.PreAdmissionIdentifier = (Hl7.Fhir.Model.Identifier)PreAdmissionIdentifier.DeepCopy();
                    if(Origin != null) dest.Origin = (Hl7.Fhir.Model.ResourceReference)Origin.DeepCopy();
                    if(AdmitSource != null) dest.AdmitSource = (Hl7.Fhir.Model.CodeableConcept)AdmitSource.DeepCopy();
                    if(ReAdmission != null) dest.ReAdmission = (Hl7.Fhir.Model.CodeableConcept)ReAdmission.DeepCopy();
                    if(DietPreference != null) dest.DietPreference = new List<Hl7.Fhir.Model.CodeableConcept>(DietPreference.DeepCopy());
                    if(SpecialCourtesy != null) dest.SpecialCourtesy = new List<Hl7.Fhir.Model.CodeableConcept>(SpecialCourtesy.DeepCopy());
                    if(SpecialArrangement != null) dest.SpecialArrangement = new List<Hl7.Fhir.Model.CodeableConcept>(SpecialArrangement.DeepCopy());
                    if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                    if(DischargeDisposition != null) dest.DischargeDisposition = (Hl7.Fhir.Model.CodeableConcept)DischargeDisposition.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HospitalizationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HospitalizationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PreAdmissionIdentifier, otherT.PreAdmissionIdentifier)) return false;
                if( !DeepComparable.Matches(Origin, otherT.Origin)) return false;
                if( !DeepComparable.Matches(AdmitSource, otherT.AdmitSource)) return false;
                if( !DeepComparable.Matches(ReAdmission, otherT.ReAdmission)) return false;
                if( !DeepComparable.Matches(DietPreference, otherT.DietPreference)) return false;
                if( !DeepComparable.Matches(SpecialCourtesy, otherT.SpecialCourtesy)) return false;
                if( !DeepComparable.Matches(SpecialArrangement, otherT.SpecialArrangement)) return false;
                if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
                if( !DeepComparable.Matches(DischargeDisposition, otherT.DischargeDisposition)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HospitalizationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PreAdmissionIdentifier, otherT.PreAdmissionIdentifier)) return false;
                if( !DeepComparable.IsExactly(Origin, otherT.Origin)) return false;
                if( !DeepComparable.IsExactly(AdmitSource, otherT.AdmitSource)) return false;
                if( !DeepComparable.IsExactly(ReAdmission, otherT.ReAdmission)) return false;
                if( !DeepComparable.IsExactly(DietPreference, otherT.DietPreference)) return false;
                if( !DeepComparable.IsExactly(SpecialCourtesy, otherT.SpecialCourtesy)) return false;
                if( !DeepComparable.IsExactly(SpecialArrangement, otherT.SpecialArrangement)) return false;
                if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
                if( !DeepComparable.IsExactly(DischargeDisposition, otherT.DischargeDisposition)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PreAdmissionIdentifier != null) yield return PreAdmissionIdentifier;
                    if (Origin != null) yield return Origin;
                    if (AdmitSource != null) yield return AdmitSource;
                    if (ReAdmission != null) yield return ReAdmission;
                    foreach (var elem in DietPreference) { if (elem != null) yield return elem; }
                    foreach (var elem in SpecialCourtesy) { if (elem != null) yield return elem; }
                    foreach (var elem in SpecialArrangement) { if (elem != null) yield return elem; }
                    if (Destination != null) yield return Destination;
                    if (DischargeDisposition != null) yield return DischargeDisposition;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PreAdmissionIdentifier != null) yield return new ElementValue("preAdmissionIdentifier", PreAdmissionIdentifier);
                    if (Origin != null) yield return new ElementValue("origin", Origin);
                    if (AdmitSource != null) yield return new ElementValue("admitSource", AdmitSource);
                    if (ReAdmission != null) yield return new ElementValue("reAdmission", ReAdmission);
                    foreach (var elem in DietPreference) { if (elem != null) yield return new ElementValue("dietPreference", elem); }
                    foreach (var elem in SpecialCourtesy) { if (elem != null) yield return new ElementValue("specialCourtesy", elem); }
                    foreach (var elem in SpecialArrangement) { if (elem != null) yield return new ElementValue("specialArrangement", elem); }
                    if (Destination != null) yield return new ElementValue("destination", Destination);
                    if (DischargeDisposition != null) yield return new ElementValue("dischargeDisposition", DischargeDisposition);
                }
            }

            
        }
        
        
        [FhirType("LocationComponent")]
        [DataContract]
        public partial class LocationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "LocationComponent"; } }
            
            /// <summary>
            /// Location the encounter takes place
            /// </summary>
            [FhirElement("location", Order=40)]
            [CLSCompliant(false)]
			[References("Location")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Location;
            
            /// <summary>
            /// planned | active | reserved | completed
            /// </summary>
            [FhirElement("status", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus> _StatusElement;
            
            /// <summary>
            /// planned | active | reserved | completed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Encounter.EncounterLocationStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StatusElement = null; 
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Time period during which the patient was present at the location
            /// </summary>
            [FhirElement("period", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LocationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus>)StatusElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LocationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LocationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LocationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Location != null) yield return Location;
                    if (StatusElement != null) yield return StatusElement;
                    if (Period != null) yield return Period;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Location != null) yield return new ElementValue("location", Location);
                    if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }

            
        }
        
        
        /// <summary>
        /// Identifier(s) by which this encounter is known
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
        /// planned | arrived | triaged | in-progress | onleave | finished | cancelled +
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Encounter.EncounterStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Encounter.EncounterStatus> _StatusElement;
        
        /// <summary>
        /// planned | arrived | triaged | in-progress | onleave | finished | cancelled +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Encounter.EncounterStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Encounter.EncounterStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// List of past encounter statuses
        /// </summary>
        [FhirElement("statusHistory", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.StatusHistoryComponent> StatusHistory
        {
            get { if(_StatusHistory==null) _StatusHistory = new List<Hl7.Fhir.Model.Encounter.StatusHistoryComponent>(); return _StatusHistory; }
            set { _StatusHistory = value; OnPropertyChanged("StatusHistory"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.StatusHistoryComponent> _StatusHistory;
        
        /// <summary>
        /// inpatient | outpatient | ambulatory | emergency +
        /// </summary>
        [FhirElement("class", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Class
        {
            get { return _Class; }
            set { _Class = value; OnPropertyChanged("Class"); }
        }
        
        private Hl7.Fhir.Model.Coding _Class;
        
        /// <summary>
        /// List of past encounter classes
        /// </summary>
        [FhirElement("classHistory", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.ClassHistoryComponent> ClassHistory
        {
            get { if(_ClassHistory==null) _ClassHistory = new List<Hl7.Fhir.Model.Encounter.ClassHistoryComponent>(); return _ClassHistory; }
            set { _ClassHistory = value; OnPropertyChanged("ClassHistory"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.ClassHistoryComponent> _ClassHistory;
        
        /// <summary>
        /// Specific type of encounter
        /// </summary>
        [FhirElement("type", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// Indicates the urgency of the encounter
        /// </summary>
        [FhirElement("priority", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// The patient ro group present at the encounter
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Episode(s) of care that this encounter should be recorded against
        /// </summary>
        [FhirElement("episodeOfCare", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("EpisodeOfCare")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EpisodeOfCare
        {
            get { if(_EpisodeOfCare==null) _EpisodeOfCare = new List<Hl7.Fhir.Model.ResourceReference>(); return _EpisodeOfCare; }
            set { _EpisodeOfCare = value; OnPropertyChanged("EpisodeOfCare"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EpisodeOfCare;
        
        /// <summary>
        /// The ReferralRequest that initiated this encounter
        /// </summary>
        [FhirElement("incomingReferral", Order=180)]
        [CLSCompliant(false)]
		[References("ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> IncomingReferral
        {
            get { if(_IncomingReferral==null) _IncomingReferral = new List<Hl7.Fhir.Model.ResourceReference>(); return _IncomingReferral; }
            set { _IncomingReferral = value; OnPropertyChanged("IncomingReferral"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _IncomingReferral;
        
        /// <summary>
        /// List of participants involved in the encounter
        /// </summary>
        [FhirElement("participant", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.Encounter.ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.ParticipantComponent> _Participant;
        
        /// <summary>
        /// The appointment that scheduled this encounter
        /// </summary>
        [FhirElement("appointment", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References("Appointment")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Appointment
        {
            get { return _Appointment; }
            set { _Appointment = value; OnPropertyChanged("Appointment"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Appointment;
        
        /// <summary>
        /// The start and end time of the encounter
        /// </summary>
        [FhirElement("period", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Quantity of time the encounter lasted (less time absent)
        /// </summary>
        [FhirElement("length", Order=220)]
        [DataMember]
        public Duration Length
        {
            get { return _Length; }
            set { _Length = value; OnPropertyChanged("Length"); }
        }
        
        private Duration _Length;
        
        /// <summary>
        /// Reason the encounter takes place (code)
        /// </summary>
        [FhirElement("reason", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// The list of diagnosis relevant to this encounter
        /// </summary>
        [FhirElement("diagnosis", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.DiagnosisComponent> Diagnosis
        {
            get { if(_Diagnosis==null) _Diagnosis = new List<Hl7.Fhir.Model.Encounter.DiagnosisComponent>(); return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.DiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// The set of accounts that may be used for billing for this Encounter
        /// </summary>
        [FhirElement("account", Order=250)]
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
        
        /// <summary>
        /// Details about the admission to a healthcare service
        /// </summary>
        [FhirElement("hospitalization", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Encounter.HospitalizationComponent Hospitalization
        {
            get { return _Hospitalization; }
            set { _Hospitalization = value; OnPropertyChanged("Hospitalization"); }
        }
        
        private Hl7.Fhir.Model.Encounter.HospitalizationComponent _Hospitalization;
        
        /// <summary>
        /// List of locations where the patient has been
        /// </summary>
        [FhirElement("location", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.LocationComponent> Location
        {
            get { if(_Location==null) _Location = new List<Hl7.Fhir.Model.Encounter.LocationComponent>(); return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.LocationComponent> _Location;
        
        /// <summary>
        /// The custodian organization of this Encounter record
        /// </summary>
        [FhirElement("serviceProvider", Order=280)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ServiceProvider
        {
            get { return _ServiceProvider; }
            set { _ServiceProvider = value; OnPropertyChanged("ServiceProvider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ServiceProvider;
        
        /// <summary>
        /// Another Encounter this encounter is part of
        /// </summary>
        [FhirElement("partOf", Order=290)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf
        {
            get { return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PartOf;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Encounter;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterStatus>)StatusElement.DeepCopy();
                if(StatusHistory != null) dest.StatusHistory = new List<Hl7.Fhir.Model.Encounter.StatusHistoryComponent>(StatusHistory.DeepCopy());
                if(Class != null) dest.Class = (Hl7.Fhir.Model.Coding)Class.DeepCopy();
                if(ClassHistory != null) dest.ClassHistory = new List<Hl7.Fhir.Model.Encounter.ClassHistoryComponent>(ClassHistory.DeepCopy());
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(EpisodeOfCare != null) dest.EpisodeOfCare = new List<Hl7.Fhir.Model.ResourceReference>(EpisodeOfCare.DeepCopy());
                if(IncomingReferral != null) dest.IncomingReferral = new List<Hl7.Fhir.Model.ResourceReference>(IncomingReferral.DeepCopy());
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.Encounter.ParticipantComponent>(Participant.DeepCopy());
                if(Appointment != null) dest.Appointment = (Hl7.Fhir.Model.ResourceReference)Appointment.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Length != null) dest.Length = (Duration)Length.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(Diagnosis != null) dest.Diagnosis = new List<Hl7.Fhir.Model.Encounter.DiagnosisComponent>(Diagnosis.DeepCopy());
                if(Account != null) dest.Account = new List<Hl7.Fhir.Model.ResourceReference>(Account.DeepCopy());
                if(Hospitalization != null) dest.Hospitalization = (Hl7.Fhir.Model.Encounter.HospitalizationComponent)Hospitalization.DeepCopy();
                if(Location != null) dest.Location = new List<Hl7.Fhir.Model.Encounter.LocationComponent>(Location.DeepCopy());
                if(ServiceProvider != null) dest.ServiceProvider = (Hl7.Fhir.Model.ResourceReference)ServiceProvider.DeepCopy();
                if(PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Encounter());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Encounter;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusHistory, otherT.StatusHistory)) return false;
            if( !DeepComparable.Matches(Class, otherT.Class)) return false;
            if( !DeepComparable.Matches(ClassHistory, otherT.ClassHistory)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(EpisodeOfCare, otherT.EpisodeOfCare)) return false;
            if( !DeepComparable.Matches(IncomingReferral, otherT.IncomingReferral)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Appointment, otherT.Appointment)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Length, otherT.Length)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Account, otherT.Account)) return false;
            if( !DeepComparable.Matches(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ServiceProvider, otherT.ServiceProvider)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Encounter;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusHistory, otherT.StatusHistory)) return false;
            if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
            if( !DeepComparable.IsExactly(ClassHistory, otherT.ClassHistory)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(EpisodeOfCare, otherT.EpisodeOfCare)) return false;
            if( !DeepComparable.IsExactly(IncomingReferral, otherT.IncomingReferral)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Appointment, otherT.Appointment)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Length, otherT.Length)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Account, otherT.Account)) return false;
            if( !DeepComparable.IsExactly(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ServiceProvider, otherT.ServiceProvider)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            
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
				foreach (var elem in StatusHistory) { if (elem != null) yield return elem; }
				if (Class != null) yield return Class;
				foreach (var elem in ClassHistory) { if (elem != null) yield return elem; }
				foreach (var elem in Type) { if (elem != null) yield return elem; }
				if (Priority != null) yield return Priority;
				if (Subject != null) yield return Subject;
				foreach (var elem in EpisodeOfCare) { if (elem != null) yield return elem; }
				foreach (var elem in IncomingReferral) { if (elem != null) yield return elem; }
				foreach (var elem in Participant) { if (elem != null) yield return elem; }
				if (Appointment != null) yield return Appointment;
				if (Period != null) yield return Period;
				if (Length != null) yield return Length;
				foreach (var elem in Reason) { if (elem != null) yield return elem; }
				foreach (var elem in Diagnosis) { if (elem != null) yield return elem; }
				foreach (var elem in Account) { if (elem != null) yield return elem; }
				if (Hospitalization != null) yield return Hospitalization;
				foreach (var elem in Location) { if (elem != null) yield return elem; }
				if (ServiceProvider != null) yield return ServiceProvider;
				if (PartOf != null) yield return PartOf;
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
                foreach (var elem in StatusHistory) { if (elem != null) yield return new ElementValue("statusHistory", elem); }
                if (Class != null) yield return new ElementValue("class", Class);
                foreach (var elem in ClassHistory) { if (elem != null) yield return new ElementValue("classHistory", elem); }
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (Priority != null) yield return new ElementValue("priority", Priority);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                foreach (var elem in EpisodeOfCare) { if (elem != null) yield return new ElementValue("episodeOfCare", elem); }
                foreach (var elem in IncomingReferral) { if (elem != null) yield return new ElementValue("incomingReferral", elem); }
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                if (Appointment != null) yield return new ElementValue("appointment", Appointment);
                if (Period != null) yield return new ElementValue("period", Period);
                if (Length != null) yield return new ElementValue("length", Length);
                foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                foreach (var elem in Diagnosis) { if (elem != null) yield return new ElementValue("diagnosis", elem); }
                foreach (var elem in Account) { if (elem != null) yield return new ElementValue("account", elem); }
                if (Hospitalization != null) yield return new ElementValue("hospitalization", Hospitalization);
                foreach (var elem in Location) { if (elem != null) yield return new ElementValue("location", elem); }
                if (ServiceProvider != null) yield return new ElementValue("serviceProvider", ServiceProvider);
                if (PartOf != null) yield return new ElementValue("partOf", PartOf);
            }
        }

    }
    
}
