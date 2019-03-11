﻿using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// How the medication is/was taken or should be taken
    /// </summary>
    [FhirType("Dosage")]
    [DataContract]
    public partial class Dosage : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Dosage"; } }
        
        [FhirType("DoseAndRateComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class DoseAndRateComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DoseAndRateComponent"; } }
            
            /// <summary>
            /// The kind of dose or rate specified
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Amount of medication per dose
            /// </summary>
            [FhirElement("dose", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
			[CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.SimpleQuantity))]
            [DataMember]
            public Hl7.Fhir.Model.Element Dose
            {
                get { return _Dose; }
                set { _Dose = value; OnPropertyChanged("Dose"); }
            }
            
            private Hl7.Fhir.Model.Element _Dose;
            
            /// <summary>
            /// Amount of medication per unit of time
            /// </summary>
            [FhirElement("rate", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
			[CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.SimpleQuantity))]
            [DataMember]
            public Hl7.Fhir.Model.Element Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            
            private Hl7.Fhir.Model.Element _Rate;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DoseAndRateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Dose != null) dest.Dose = (Hl7.Fhir.Model.Element)Dose.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.Element)Rate.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DoseAndRateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DoseAndRateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Dose, otherT.Dose)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DoseAndRateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Dose, otherT.Dose)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Dose != null) yield return Dose;
                    if (Rate != null) yield return Rate;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Dose != null) yield return new ElementValue("dose", Dose);
                    if (Rate != null) yield return new ElementValue("rate", Rate);
 
                } 
            } 
            
        }                
        /// <summary>
        /// The order of the dosage instructions
        /// </summary>
        [FhirElement("sequence", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Integer SequenceElement
        {
            get { return _SequenceElement; }
            set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _SequenceElement;
        
        /// <summary>
        /// The order of the dosage instructions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Sequence
        {
            get { return SequenceElement != null ? SequenceElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  SequenceElement = null; 
                else
                  SequenceElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("Sequence");
            }
        }
        
        /// <summary>
        /// Free text dosage instructions e.g. SIG
        /// </summary>
        [FhirElement("text", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TextElement
        {
            get { return _TextElement; }
            set { _TextElement = value; OnPropertyChanged("TextElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TextElement;
        
        /// <summary>
        /// Free text dosage instructions e.g. SIG
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Text
        {
            get { return TextElement != null ? TextElement.Value : null; }
            set
            {
                if (value == null)
                  TextElement = null; 
                else
                  TextElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Text");
            }
        }
        
        /// <summary>
        /// Supplemental instruction or warnings to the patient - e.g. "with meals", "may cause drowsiness"
        /// </summary>
        [FhirElement("additionalInstruction", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> AdditionalInstruction
        {
            get { if(_AdditionalInstruction==null) _AdditionalInstruction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AdditionalInstruction; }
            set { _AdditionalInstruction = value; OnPropertyChanged("AdditionalInstruction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _AdditionalInstruction;
        
        /// <summary>
        /// Patient or consumer oriented instructions
        /// </summary>
        [FhirElement("patientInstruction", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PatientInstructionElement
        {
            get { return _PatientInstructionElement; }
            set { _PatientInstructionElement = value; OnPropertyChanged("PatientInstructionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PatientInstructionElement;
        
        /// <summary>
        /// Patient or consumer oriented instructions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PatientInstruction
        {
            get { return PatientInstructionElement != null ? PatientInstructionElement.Value : null; }
            set
            {
                if (value == null)
                  PatientInstructionElement = null; 
                else
                  PatientInstructionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PatientInstruction");
            }
        }
        
        /// <summary>
        /// When medication should be administered
        /// </summary>
        [FhirElement("timing", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Timing Timing
        {
            get { return _Timing; }
            set { _Timing = value; OnPropertyChanged("Timing"); }
        }
        
        private Hl7.Fhir.Model.Timing _Timing;
        
        /// <summary>
        /// Take "as needed" (for x)
        /// </summary>
        [FhirElement("asNeeded", InSummary=true, Order=140, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
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
        [FhirElement("site", InSummary=true, Order=150)]
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
        [FhirElement("route", InSummary=true, Order=160)]
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
        [FhirElement("method", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Method
        {
            get { return _Method; }
            set { _Method = value; OnPropertyChanged("Method"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Method;
        
        /// <summary>
        /// Amount of medication administered
        /// </summary>
        [FhirElement("doseAndRate", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Dosage.DoseAndRateComponent> DoseAndRate
        {
            get { if(_DoseAndRate==null) _DoseAndRate = new List<Hl7.Fhir.Model.Dosage.DoseAndRateComponent>(); return _DoseAndRate; }
            set { _DoseAndRate = value; OnPropertyChanged("DoseAndRate"); }
        }
        
        private List<Hl7.Fhir.Model.Dosage.DoseAndRateComponent> _DoseAndRate;
        
        /// <summary>
        /// Upper limit on medication per unit of time
        /// </summary>
        [FhirElement("maxDosePerPeriod", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Ratio MaxDosePerPeriod
        {
            get { return _MaxDosePerPeriod; }
            set { _MaxDosePerPeriod = value; OnPropertyChanged("MaxDosePerPeriod"); }
        }
        
        private Hl7.Fhir.Model.Ratio _MaxDosePerPeriod;
        
        /// <summary>
        /// Upper limit on medication per administration
        /// </summary>
        [FhirElement("maxDosePerAdministration", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity MaxDosePerAdministration
        {
            get { return _MaxDosePerAdministration; }
            set { _MaxDosePerAdministration = value; OnPropertyChanged("MaxDosePerAdministration"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _MaxDosePerAdministration;
        
        /// <summary>
        /// Upper limit on medication per lifetime of the patient
        /// </summary>
        [FhirElement("maxDosePerLifetime", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity MaxDosePerLifetime
        {
            get { return _MaxDosePerLifetime; }
            set { _MaxDosePerLifetime = value; OnPropertyChanged("MaxDosePerLifetime"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _MaxDosePerLifetime;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Dosage;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.Integer)SequenceElement.DeepCopy();
                if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                if(AdditionalInstruction != null) dest.AdditionalInstruction = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalInstruction.DeepCopy());
                if(PatientInstructionElement != null) dest.PatientInstructionElement = (Hl7.Fhir.Model.FhirString)PatientInstructionElement.DeepCopy();
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Timing)Timing.DeepCopy();
                if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(DoseAndRate != null) dest.DoseAndRate = new List<Hl7.Fhir.Model.Dosage.DoseAndRateComponent>(DoseAndRate.DeepCopy());
                if(MaxDosePerPeriod != null) dest.MaxDosePerPeriod = (Hl7.Fhir.Model.Ratio)MaxDosePerPeriod.DeepCopy();
                if(MaxDosePerAdministration != null) dest.MaxDosePerAdministration = (Hl7.Fhir.Model.SimpleQuantity)MaxDosePerAdministration.DeepCopy();
                if(MaxDosePerLifetime != null) dest.MaxDosePerLifetime = (Hl7.Fhir.Model.SimpleQuantity)MaxDosePerLifetime.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Dosage());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Dosage;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
            if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.Matches(AdditionalInstruction, otherT.AdditionalInstruction)) return false;
            if( !DeepComparable.Matches(PatientInstructionElement, otherT.PatientInstructionElement)) return false;
            if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
            if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.Matches(Site, otherT.Site)) return false;
            if( !DeepComparable.Matches(Route, otherT.Route)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(DoseAndRate, otherT.DoseAndRate)) return false;
            if( !DeepComparable.Matches(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
            if( !DeepComparable.Matches(MaxDosePerAdministration, otherT.MaxDosePerAdministration)) return false;
            if( !DeepComparable.Matches(MaxDosePerLifetime, otherT.MaxDosePerLifetime)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Dosage;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
            if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.IsExactly(AdditionalInstruction, otherT.AdditionalInstruction)) return false;
            if( !DeepComparable.IsExactly(PatientInstructionElement, otherT.PatientInstructionElement)) return false;
            if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
            if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
            if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(DoseAndRate, otherT.DoseAndRate)) return false;
            if( !DeepComparable.IsExactly(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
            if( !DeepComparable.IsExactly(MaxDosePerAdministration, otherT.MaxDosePerAdministration)) return false;
            if( !DeepComparable.IsExactly(MaxDosePerLifetime, otherT.MaxDosePerLifetime)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (SequenceElement != null) yield return SequenceElement;
                if (TextElement != null) yield return TextElement;
                foreach (var elem in AdditionalInstruction) { if (elem != null) yield return elem; }
                if (PatientInstructionElement != null) yield return PatientInstructionElement;
                if (Timing != null) yield return Timing;
                if (AsNeeded != null) yield return AsNeeded;
                if (Site != null) yield return Site;
                if (Route != null) yield return Route;
                if (Method != null) yield return Method;
                foreach (var elem in DoseAndRate) { if (elem != null) yield return elem; }
                if (MaxDosePerPeriod != null) yield return MaxDosePerPeriod;
                if (MaxDosePerAdministration != null) yield return MaxDosePerAdministration;
                if (MaxDosePerLifetime != null) yield return MaxDosePerLifetime;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                if (TextElement != null) yield return new ElementValue("text", TextElement);
                foreach (var elem in AdditionalInstruction) { if (elem != null) yield return new ElementValue("additionalInstruction", elem); }
                if (PatientInstructionElement != null) yield return new ElementValue("patientInstruction", PatientInstructionElement);
                if (Timing != null) yield return new ElementValue("timing", Timing);
                if (AsNeeded != null) yield return new ElementValue("asNeeded", AsNeeded);
                if (Site != null) yield return new ElementValue("site", Site);
                if (Route != null) yield return new ElementValue("route", Route);
                if (Method != null) yield return new ElementValue("method", Method);
                foreach (var elem in DoseAndRate) { if (elem != null) yield return new ElementValue("doseAndRate", elem); }
                if (MaxDosePerPeriod != null) yield return new ElementValue("maxDosePerPeriod", MaxDosePerPeriod);
                if (MaxDosePerAdministration != null) yield return new ElementValue("maxDosePerAdministration", MaxDosePerAdministration);
                if (MaxDosePerLifetime != null) yield return new ElementValue("maxDosePerLifetime", MaxDosePerLifetime);
 
            } 
        } 
    
    
    }
    
}
