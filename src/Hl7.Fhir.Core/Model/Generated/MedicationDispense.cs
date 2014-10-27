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
    /// Dispensing a medication to a named patient
    /// </summary>
    [FhirType("MedicationDispense", IsResource=true)]
    [DataContract]
    public partial class MedicationDispense : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// A code specifying the state of the dispense event.
        /// </summary>
        [FhirEnumeration("MedicationDispenseStatus")]
        public enum MedicationDispenseStatus
        {
            /// <summary>
            /// The dispense has started but has not yet completed.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// Actions implied by the administration have been temporarily halted, but are expected to continue later. May also be called "suspended".
            /// </summary>
            [EnumLiteral("on hold")]
            OnHold,
            /// <summary>
            /// All actions that are implied by the dispense have occurred.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The dispense was entered in error and therefore nullified.
            /// </summary>
            [EnumLiteral("entered in error")]
            EnteredInError,
            /// <summary>
            /// Actions implied by the dispense have been permanently halted, before all of them occurred.
            /// </summary>
            [EnumLiteral("stopped")]
            Stopped,
        }
        
        [FhirType("MedicationDispenseDispenseDosageComponent")]
        [DataContract]
        public partial class MedicationDispenseDispenseDosageComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// E.g. "Take with food"
            /// </summary>
            [FhirElement("additionalInstructions", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdditionalInstructions
            {
                get { return _AdditionalInstructions; }
                set { _AdditionalInstructions = value; OnPropertyChanged("AdditionalInstructions"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _AdditionalInstructions;
            
            /// <summary>
            /// When medication should be administered
            /// </summary>
            [FhirElement("timing", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Schedule))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded
            {
                get { return _AsNeeded; }
                set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
            }
            private Hl7.Fhir.Model.Element _AsNeeded;
            
            /// <summary>
            /// Body site to administer to
            /// </summary>
            [FhirElement("site", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Site;
            
            /// <summary>
            /// How drug should enter body
            /// </summary>
            [FhirElement("route", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route
            {
                get { return _Route; }
                set { _Route = value; OnPropertyChanged("Route"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Route;
            
            /// <summary>
            /// Technique for administering medication
            /// </summary>
            [FhirElement("method", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Amount of medication per dose
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Amount of medication per unit of time
            /// </summary>
            [FhirElement("rate", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            private Hl7.Fhir.Model.Ratio _Rate;
            
            /// <summary>
            /// Upper limit on medication per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod
            {
                get { return _MaxDosePerPeriod; }
                set { _MaxDosePerPeriod = value; OnPropertyChanged("MaxDosePerPeriod"); }
            }
            private Hl7.Fhir.Model.Ratio _MaxDosePerPeriod;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationDispenseDispenseDosageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AdditionalInstructions != null) dest.AdditionalInstructions = (Hl7.Fhir.Model.CodeableConcept)AdditionalInstructions.DeepCopy();
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                    if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.Ratio)Rate.DeepCopy();
                    if(MaxDosePerPeriod != null) dest.MaxDosePerPeriod = (Hl7.Fhir.Model.Ratio)MaxDosePerPeriod.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationDispenseDispenseDosageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseDispenseDosageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                if( !DeepComparable.Matches(Route, otherT.Route)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
                if( !DeepComparable.Matches(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseDispenseDosageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
                if( !DeepComparable.IsExactly(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("MedicationDispenseSubstitutionComponent")]
        [DataContract]
        public partial class MedicationDispenseSubstitutionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Type of substitiution
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
            /// Why was substitution made
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
            
            /// <summary>
            /// Who is responsible for the substitution
            /// </summary>
            [FhirElement("responsibleParty", InSummary=true, Order=60)]
            [References("Practitioner")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ResponsibleParty
            {
                get { return _ResponsibleParty; }
                set { _ResponsibleParty = value; OnPropertyChanged("ResponsibleParty"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _ResponsibleParty;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationDispenseSubstitutionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                    if(ResponsibleParty != null) dest.ResponsibleParty = new List<Hl7.Fhir.Model.ResourceReference>(ResponsibleParty.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationDispenseSubstitutionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseSubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ResponsibleParty, otherT.ResponsibleParty)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseSubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ResponsibleParty, otherT.ResponsibleParty)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("MedicationDispenseDispenseComponent")]
        [DataContract]
        public partial class MedicationDispenseDispenseComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// External identifier for individual item
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// in progress | on hold | completed | entered in error | stopped
            /// </summary>
            [FhirElement("status", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> _StatusElement;
            
            /// <summary>
            /// in progress | on hold | completed | entered in error | stopped
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Trial fill, partial fill, emergency fill, etc.
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Amount dispensed
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// What medication was supplied
            /// </summary>
            [FhirElement("medication", InSummary=true, Order=80)]
            [References("Medication")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Medication
            {
                get { return _Medication; }
                set { _Medication = value; OnPropertyChanged("Medication"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Medication;
            
            /// <summary>
            /// Dispense processing time
            /// </summary>
            [FhirElement("whenPrepared", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime WhenPreparedElement
            {
                get { return _WhenPreparedElement; }
                set { _WhenPreparedElement = value; OnPropertyChanged("WhenPreparedElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _WhenPreparedElement;
            
            /// <summary>
            /// Dispense processing time
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string WhenPrepared
            {
                get { return WhenPreparedElement != null ? WhenPreparedElement.Value : null; }
                set
                {
                    if(value == null)
                      WhenPreparedElement = null; 
                    else
                      WhenPreparedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("WhenPrepared");
                }
            }
            
            /// <summary>
            /// Handover time
            /// </summary>
            [FhirElement("whenHandedOver", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime WhenHandedOverElement
            {
                get { return _WhenHandedOverElement; }
                set { _WhenHandedOverElement = value; OnPropertyChanged("WhenHandedOverElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _WhenHandedOverElement;
            
            /// <summary>
            /// Handover time
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string WhenHandedOver
            {
                get { return WhenHandedOverElement != null ? WhenHandedOverElement.Value : null; }
                set
                {
                    if(value == null)
                      WhenHandedOverElement = null; 
                    else
                      WhenHandedOverElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("WhenHandedOver");
                }
            }
            
            /// <summary>
            /// Where the medication was sent
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=110)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination
            {
                get { return _Destination; }
                set { _Destination = value; OnPropertyChanged("Destination"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Destination;
            
            /// <summary>
            /// Who collected the medication
            /// </summary>
            [FhirElement("receiver", InSummary=true, Order=120)]
            [References("Patient","Practitioner")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Receiver
            {
                get { return _Receiver; }
                set { _Receiver = value; OnPropertyChanged("Receiver"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Receiver;
            
            /// <summary>
            /// Medicine administration instructions to the patient/carer
            /// </summary>
            [FhirElement("dosage", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseDosageComponent> Dosage
            {
                get { return _Dosage; }
                set { _Dosage = value; OnPropertyChanged("Dosage"); }
            }
            private List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseDosageComponent> _Dosage;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationDispenseDispenseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>)StatusElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Medication != null) dest.Medication = (Hl7.Fhir.Model.ResourceReference)Medication.DeepCopy();
                    if(WhenPreparedElement != null) dest.WhenPreparedElement = (Hl7.Fhir.Model.FhirDateTime)WhenPreparedElement.DeepCopy();
                    if(WhenHandedOverElement != null) dest.WhenHandedOverElement = (Hl7.Fhir.Model.FhirDateTime)WhenHandedOverElement.DeepCopy();
                    if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                    if(Receiver != null) dest.Receiver = new List<Hl7.Fhir.Model.ResourceReference>(Receiver.DeepCopy());
                    if(Dosage != null) dest.Dosage = new List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseDosageComponent>(Dosage.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationDispenseDispenseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseDispenseComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
                if( !DeepComparable.Matches(WhenPreparedElement, otherT.WhenPreparedElement)) return false;
                if( !DeepComparable.Matches(WhenHandedOverElement, otherT.WhenHandedOverElement)) return false;
                if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
                if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
                if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseDispenseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
                if( !DeepComparable.IsExactly(WhenPreparedElement, otherT.WhenPreparedElement)) return false;
                if( !DeepComparable.IsExactly(WhenHandedOverElement, otherT.WhenHandedOverElement)) return false;
                if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
                if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
                if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// in progress | on hold | completed | entered in error | stopped
        /// </summary>
        [FhirElement("status", Order=80)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> _StatusElement;
        
        /// <summary>
        /// in progress | on hold | completed | entered in error | stopped
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who the dispense is for
        /// </summary>
        [FhirElement("patient", Order=90)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Practitioner responsible for dispensing medication
        /// </summary>
        [FhirElement("dispenser", Order=100)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Dispenser
        {
            get { return _Dispenser; }
            set { _Dispenser = value; OnPropertyChanged("Dispenser"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Dispenser;
        
        /// <summary>
        /// Medication order that authorizes the dispense
        /// </summary>
        [FhirElement("authorizingPrescription", Order=110)]
        [References("MedicationPrescription")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> AuthorizingPrescription
        {
            get { return _AuthorizingPrescription; }
            set { _AuthorizingPrescription = value; OnPropertyChanged("AuthorizingPrescription"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _AuthorizingPrescription;
        
        /// <summary>
        /// Details for individual dispensed medicationdetails
        /// </summary>
        [FhirElement("dispense", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseComponent> Dispense
        {
            get { return _Dispense; }
            set { _Dispense = value; OnPropertyChanged("Dispense"); }
        }
        private List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseComponent> _Dispense;
        
        /// <summary>
        /// Deals with substitution of one medicine for another
        /// </summary>
        [FhirElement("substitution", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        private Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent _Substitution;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationDispense;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Dispenser != null) dest.Dispenser = (Hl7.Fhir.Model.ResourceReference)Dispenser.DeepCopy();
                if(AuthorizingPrescription != null) dest.AuthorizingPrescription = new List<Hl7.Fhir.Model.ResourceReference>(AuthorizingPrescription.DeepCopy());
                if(Dispense != null) dest.Dispense = new List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseComponent>(Dispense.DeepCopy());
                if(Substitution != null) dest.Substitution = (Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent)Substitution.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicationDispense());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationDispense;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Dispenser, otherT.Dispenser)) return false;
            if( !DeepComparable.Matches(AuthorizingPrescription, otherT.AuthorizingPrescription)) return false;
            if( !DeepComparable.Matches(Dispense, otherT.Dispense)) return false;
            if( !DeepComparable.Matches(Substitution, otherT.Substitution)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationDispense;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Dispenser, otherT.Dispenser)) return false;
            if( !DeepComparable.IsExactly(AuthorizingPrescription, otherT.AuthorizingPrescription)) return false;
            if( !DeepComparable.IsExactly(Dispense, otherT.Dispense)) return false;
            if( !DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;
            
            return true;
        }
        
    }
    
}
