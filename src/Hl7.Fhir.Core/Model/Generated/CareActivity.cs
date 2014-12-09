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
// Generated on Tue, Dec 9, 2014 15:49+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Healthcare plan for patient
    /// </summary>
    [FhirType("CareActivity", IsResource=true)]
    [DataContract]
    public partial class CareActivity : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CareActivity; } }
        [NotMapped]
        public override string TypeName { get { return "CareActivity"; } }
        
        /// <summary>
        /// High-level categorization of the type of activity in a care plan.
        /// </summary>
        [FhirEnumeration("CareActivityCategory")]
        public enum CareActivityCategory
        {
            /// <summary>
            /// Plan for the patient to consume food of a specified nature.
            /// </summary>
            [EnumLiteral("diet")]
            Diet,
            /// <summary>
            /// Plan for the patient to consume/receive a drug, vaccine or other product.
            /// </summary>
            [EnumLiteral("drug")]
            Drug,
            /// <summary>
            /// Plan to meet or communicate with the patient (in-patient, out-patient, phone call, etc.).
            /// </summary>
            [EnumLiteral("encounter")]
            Encounter,
            /// <summary>
            /// Plan to capture information about a patient (vitals, labs, diagnostic images, etc.).
            /// </summary>
            [EnumLiteral("observation")]
            Observation,
            /// <summary>
            /// Plan to modify the patient in some way (surgery, physiotherapy, education, counseling, etc.).
            /// </summary>
            [EnumLiteral("procedure")]
            Procedure,
            /// <summary>
            /// Plan to provide something to the patient (medication, medical supply, etc.).
            /// </summary>
            [EnumLiteral("supply")]
            Supply,
            /// <summary>
            /// Some other form of action.
            /// </summary>
            [EnumLiteral("other")]
            Other,
        }
        
        /// <summary>
        /// Indicates where the activity is at in its overall life cycle
        /// </summary>
        [FhirEnumeration("CareActivityStatus")]
        public enum CareActivityStatus
        {
            /// <summary>
            /// Activity is planned but no action has yet been taken.
            /// </summary>
            [EnumLiteral("not started")]
            NotStarted,
            /// <summary>
            /// Appointment or other booking has occurred but activity has not yet begun.
            /// </summary>
            [EnumLiteral("scheduled")]
            Scheduled,
            /// <summary>
            /// Activity has been started but is not yet complete.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// Activity was started but has temporarily ceased with an expectation of resumption at a future time.
            /// </summary>
            [EnumLiteral("on hold")]
            OnHold,
            /// <summary>
            /// The activities have been completed (more or less) as planned.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The activities have been ended prior to completion (perhaps even before they were started).
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
        }
        
        [FhirType("CareActivitySimpleComponent")]
        [DataContract]
        public partial class CareActivitySimpleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CareActivitySimpleComponent"; } }
            
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            [FhirElement("category", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CareActivity.CareActivityCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            private Code<Hl7.Fhir.Model.CareActivity.CareActivityCategory> _CategoryElement;
            
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CareActivity.CareActivityCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if(value == null)
                      CategoryElement = null; 
                    else
                      CategoryElement = new Code<Hl7.Fhir.Model.CareActivity.CareActivityCategory>(value);
                    OnPropertyChanged("Category");
                }
            }
            
            /// <summary>
            /// Detail type of activity
            /// </summary>
            [FhirElement("code", InSummary=true, Order=30)]
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
            [FhirElement("scheduled", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Scheduled
            {
                get { return _Scheduled; }
                set { _Scheduled = value; OnPropertyChanged("Scheduled"); }
            }
            private Hl7.Fhir.Model.Element _Scheduled;
            
            /// <summary>
            /// Where it should happen
            /// </summary>
            [FhirElement("location", InSummary=true, Order=50)]
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
            [FhirElement("performer", InSummary=true, Order=60)]
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
            [FhirElement("product", InSummary=true, Order=70)]
            [References("Medication","Substance")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Product
            {
                get { return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Product;
            
            /// <summary>
            /// How much consumed/day?
            /// </summary>
            [FhirElement("dailyAmount", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity DailyAmount
            {
                get { return _DailyAmount; }
                set { _DailyAmount = value; OnPropertyChanged("DailyAmount"); }
            }
            private Hl7.Fhir.Model.Quantity _DailyAmount;
            
            /// <summary>
            /// How much is administered/supplied/consumed
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Extra info on activity occurrence
            /// </summary>
            [FhirElement("details", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DetailsElement
            {
                get { return _DetailsElement; }
                set { _DetailsElement = value; OnPropertyChanged("DetailsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DetailsElement;
            
            /// <summary>
            /// Extra info on activity occurrence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Details
            {
                get { return DetailsElement != null ? DetailsElement.Value : null; }
                set
                {
                    if(value == null)
                      DetailsElement = null; 
                    else
                      DetailsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Details");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CareActivitySimpleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.CareActivity.CareActivityCategory>)CategoryElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Scheduled != null) dest.Scheduled = (Hl7.Fhir.Model.Element)Scheduled.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                    if(Product != null) dest.Product = (Hl7.Fhir.Model.ResourceReference)Product.DeepCopy();
                    if(DailyAmount != null) dest.DailyAmount = (Hl7.Fhir.Model.Quantity)DailyAmount.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(DetailsElement != null) dest.DetailsElement = (Hl7.Fhir.Model.FhirString)DetailsElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CareActivitySimpleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CareActivitySimpleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Scheduled, otherT.Scheduled)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
                if( !DeepComparable.Matches(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(DetailsElement, otherT.DetailsElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CareActivitySimpleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Scheduled, otherT.Scheduled)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
                if( !DeepComparable.IsExactly(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(DetailsElement, otherT.DetailsElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this plan
        /// </summary>
        [FhirElement("identifier", Order=50)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who care plan is for
        /// </summary>
        [FhirElement("patient", Order=60)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Goals this activity relates to
        /// </summary>
        [FhirElement("goal", Order=70)]
        [References("Goal")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Goal
        {
            get { if(_Goal==null) _Goal = new List<Hl7.Fhir.Model.ResourceReference>(); return _Goal; }
            set { _Goal = value; OnPropertyChanged("Goal"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Goal;
        
        /// <summary>
        /// not started | scheduled | in progress | on hold | completed | cancelled
        /// </summary>
        [FhirElement("status", Order=80)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CareActivity.CareActivityStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.CareActivity.CareActivityStatus> _StatusElement;
        
        /// <summary>
        /// not started | scheduled | in progress | on hold | completed | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CareActivity.CareActivityStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.CareActivity.CareActivityStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Do NOT do
        /// </summary>
        [FhirElement("prohibited", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ProhibitedElement
        {
            get { return _ProhibitedElement; }
            set { _ProhibitedElement = value; OnPropertyChanged("ProhibitedElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ProhibitedElement;
        
        /// <summary>
        /// Do NOT do
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Prohibited
        {
            get { return ProhibitedElement != null ? ProhibitedElement.Value : null; }
            set
            {
                if(value == null)
                  ProhibitedElement = null; 
                else
                  ProhibitedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Prohibited");
            }
        }
        
        /// <summary>
        /// Appointments, orders, etc.
        /// </summary>
        [FhirElement("actionResulting", Order=100)]
        [References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ActionResulting
        {
            get { if(_ActionResulting==null) _ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(); return _ActionResulting; }
            set { _ActionResulting = value; OnPropertyChanged("ActionResulting"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _ActionResulting;
        
        /// <summary>
        /// Comments about the activity
        /// </summary>
        [FhirElement("notes", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement
        {
            get { return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NotesElement;
        
        /// <summary>
        /// Comments about the activity
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Notes
        {
            get { return NotesElement != null ? NotesElement.Value : null; }
            set
            {
                if(value == null)
                  NotesElement = null; 
                else
                  NotesElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Notes");
            }
        }
        
        /// <summary>
        /// Activity details defined in specific resource
        /// </summary>
        [FhirElement("detail", Order=120)]
        [References("Procedure","MedicationPrescription","DiagnosticOrder","Encounter","Supply")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Detail
        {
            get { return _Detail; }
            set { _Detail = value; OnPropertyChanged("Detail"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Detail;
        
        /// <summary>
        /// Activity details summarised here
        /// </summary>
        [FhirElement("simple", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CareActivity.CareActivitySimpleComponent Simple
        {
            get { return _Simple; }
            set { _Simple = value; OnPropertyChanged("Simple"); }
        }
        private Hl7.Fhir.Model.CareActivity.CareActivitySimpleComponent _Simple;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CareActivity;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.ResourceReference>(Goal.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CareActivity.CareActivityStatus>)StatusElement.DeepCopy();
                if(ProhibitedElement != null) dest.ProhibitedElement = (Hl7.Fhir.Model.FhirBoolean)ProhibitedElement.DeepCopy();
                if(ActionResulting != null) dest.ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(ActionResulting.DeepCopy());
                if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                if(Detail != null) dest.Detail = (Hl7.Fhir.Model.ResourceReference)Detail.DeepCopy();
                if(Simple != null) dest.Simple = (Hl7.Fhir.Model.CareActivity.CareActivitySimpleComponent)Simple.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new CareActivity());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CareActivity;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ProhibitedElement, otherT.ProhibitedElement)) return false;
            if( !DeepComparable.Matches(ActionResulting, otherT.ActionResulting)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            if( !DeepComparable.Matches(Simple, otherT.Simple)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CareActivity;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ProhibitedElement, otherT.ProhibitedElement)) return false;
            if( !DeepComparable.IsExactly(ActionResulting, otherT.ActionResulting)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            if( !DeepComparable.IsExactly(Simple, otherT.Simple)) return false;
            
            return true;
        }
        
    }
    
}
