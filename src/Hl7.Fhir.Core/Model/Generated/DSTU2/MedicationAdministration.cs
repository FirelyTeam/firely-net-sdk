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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// Administration of medication to a patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "MedicationAdministration", IsResource=true)]
    [DataContract]
    public partial class MedicationAdministration : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMedicationAdministration, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationAdministration; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationAdministration"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DosageComponent")]
        [DataContract]
        public partial class DosageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMedicationAdministrationDosageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DosageComponent"; } }
            
            /// <summary>
            /// Dosage Instructions
            /// </summary>
            [FhirElement("text", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Dosage Instructions
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
            /// Body site administered to
            /// </summary>
            [FhirElement("site", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.Element _Site;
            
            /// <summary>
            /// Path of substance into body
            /// </summary>
            [FhirElement("route", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route
            {
                get { return _Route; }
                set { _Route = value; OnPropertyChanged("Route"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Route;
            
            /// <summary>
            /// How drug was administered
            /// </summary>
            [FhirElement("method", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Amount administered in one dose
            /// </summary>
            [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Dose quantity per unit of time
            /// </summary>
            [FhirElement("rate", InSummary=Hl7.Fhir.Model.Version.All, Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.Range))]
            [DataMember]
            public Hl7.Fhir.Model.Element Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            
            private Hl7.Fhir.Model.Element _Rate;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DosageComponent");
                base.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TextElement?.Serialize(sink);
                sink.Element("site", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Site?.Serialize(sink);
                sink.Element("route", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Route?.Serialize(sink);
                sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Method?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Quantity?.Serialize(sink);
                sink.Element("rate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Rate?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "text":
                        TextElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "siteCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Site, "site");
                        Site = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "siteReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Site, "site");
                        Site = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "route":
                        Route = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "method":
                        Method = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "rateRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Rate, "rate");
                        Rate = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "rateRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Rate, "rate");
                        Rate = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                    case "siteCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Site, "site");
                        Site = source.Populate(Site as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "siteReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Site, "site");
                        Site = source.Populate(Site as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "route":
                        Route = source.Populate(Route);
                        return true;
                    case "method":
                        Method = source.Populate(Method);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "rateRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Rate, "rate");
                        Rate = source.Populate(Rate as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "rateRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Rate, "rate");
                        Rate = source.Populate(Rate as Hl7.Fhir.Model.Range);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DosageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.Element)Site.DeepCopy();
                    if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.Element)Rate.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DosageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DosageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                if( !DeepComparable.Matches(Route, otherT.Route)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DosageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TextElement != null) yield return TextElement;
                    if (Site != null) yield return Site;
                    if (Route != null) yield return Route;
                    if (Method != null) yield return Method;
                    if (Quantity != null) yield return Quantity;
                    if (Rate != null) yield return Rate;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (Site != null) yield return new ElementValue("site", Site);
                    if (Route != null) yield return new ElementValue("route", Route);
                    if (Method != null) yield return new ElementValue("method", Method);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (Rate != null) yield return new ElementValue("rate", Rate);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.IMedicationAdministrationDosageComponent Hl7.Fhir.Model.IMedicationAdministration.Dosage { get { return Dosage; } }
    
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// in-progress | on-hold | completed | entered-in-error | stopped
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.MedicationAdministrationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.MedicationAdministrationStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | on-hold | completed | entered-in-error | stopped
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.MedicationAdministrationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.MedicationAdministrationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who received medication
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        /// Who administered substance
        /// </summary>
        [FhirElement("practitioner", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Practitioner","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Practitioner
        {
            get { return _Practitioner; }
            set { _Practitioner = value; OnPropertyChanged("Practitioner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Practitioner;
        
        /// <summary>
        /// Encounter administered as part of
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Order administration performed against
        /// </summary>
        [FhirElement("prescription", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("MedicationOrder")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescription
        {
            get { return _Prescription; }
            set { _Prescription = value; OnPropertyChanged("Prescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Prescription;
        
        /// <summary>
        /// True if medication not administered
        /// </summary>
        [FhirElement("wasNotGiven", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean WasNotGivenElement
        {
            get { return _WasNotGivenElement; }
            set { _WasNotGivenElement = value; OnPropertyChanged("WasNotGivenElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _WasNotGivenElement;
        
        /// <summary>
        /// True if medication not administered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? WasNotGiven
        {
            get { return WasNotGivenElement != null ? WasNotGivenElement.Value : null; }
            set
            {
                if (value == null)
                    WasNotGivenElement = null;
                else
                    WasNotGivenElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("WasNotGiven");
            }
        }
        
        /// <summary>
        /// Reason administration not performed
        /// </summary>
        [FhirElement("reasonNotGiven", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotGiven
        {
            get { if(_ReasonNotGiven==null) _ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonNotGiven; }
            set { _ReasonNotGiven = value; OnPropertyChanged("ReasonNotGiven"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonNotGiven;
        
        /// <summary>
        /// Reason administration performed
        /// </summary>
        [FhirElement("reasonGiven", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonGiven
        {
            get { if(_ReasonGiven==null) _ReasonGiven = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonGiven; }
            set { _ReasonGiven = value; OnPropertyChanged("ReasonGiven"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonGiven;
        
        /// <summary>
        /// Start and end time of administration
        /// </summary>
        [FhirElement("effectiveTime", InSummary=Hl7.Fhir.Model.Version.All, Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element EffectiveTime
        {
            get { return _EffectiveTime; }
            set { _EffectiveTime = value; OnPropertyChanged("EffectiveTime"); }
        }
        
        private Hl7.Fhir.Model.Element _EffectiveTime;
        
        /// <summary>
        /// What was administered
        /// </summary>
        [FhirElement("medication", InSummary=Hl7.Fhir.Model.Version.All, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.Element _Medication;
        
        /// <summary>
        /// Device used to administer
        /// </summary>
        [FhirElement("device", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [References("Device")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Device
        {
            get { if(_Device==null) _Device = new List<Hl7.Fhir.Model.ResourceReference>(); return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Device;
        
        /// <summary>
        /// Information about the administration
        /// </summary>
        [FhirElement("note", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NoteElement
        {
            get { return _NoteElement; }
            set { _NoteElement = value; OnPropertyChanged("NoteElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NoteElement;
        
        /// <summary>
        /// Information about the administration
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Note
        {
            get { return NoteElement != null ? NoteElement.Value : null; }
            set
            {
                if (value == null)
                    NoteElement = null;
                else
                    NoteElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Note");
            }
        }
        
        /// <summary>
        /// Details of how medication was taken
        /// </summary>
        [FhirElement("dosage", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public DosageComponent Dosage
        {
            get { return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private DosageComponent _Dosage;
    
    
        public static ElementDefinitionConstraint[] MedicationAdministration_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "mad-3",
                severity: ConstraintSeverity.Warning,
                expression: "reasonGiven.empty() or wasNotGiven = 'false'",
                human: "Reason given is only permitted if wasNotGiven is false",
                xpath: "not(exists(f:reasonGiven) and f:wasNotGiven/@value=true())"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "mad-2",
                severity: ConstraintSeverity.Warning,
                expression: "reasonNotGiven.empty() or wasNotGiven = 'true'",
                human: "Reason not given is only permitted if wasNotGiven is true",
                xpath: "not(exists(f:reasonNotGiven) and f:wasNotGiven/@value=false())"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "mad-1",
                severity: ConstraintSeverity.Warning,
                expression: "dosage.all(quantity or rate)",
                human: "SHALL have at least one of dosage.quantity and dosage.rate[x]",
                xpath: "exists(f:quantity) or exists(f:rateRatio) or exists(f:rateRange)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(MedicationAdministration_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationAdministration;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.MedicationAdministrationStatus>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Practitioner != null) dest.Practitioner = (Hl7.Fhir.Model.ResourceReference)Practitioner.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Prescription != null) dest.Prescription = (Hl7.Fhir.Model.ResourceReference)Prescription.DeepCopy();
                if(WasNotGivenElement != null) dest.WasNotGivenElement = (Hl7.Fhir.Model.FhirBoolean)WasNotGivenElement.DeepCopy();
                if(ReasonNotGiven != null) dest.ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotGiven.DeepCopy());
                if(ReasonGiven != null) dest.ReasonGiven = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonGiven.DeepCopy());
                if(EffectiveTime != null) dest.EffectiveTime = (Hl7.Fhir.Model.Element)EffectiveTime.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Device != null) dest.Device = new List<Hl7.Fhir.Model.ResourceReference>(Device.DeepCopy());
                if(NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
                if(Dosage != null) dest.Dosage = (DosageComponent)Dosage.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicationAdministration());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationAdministration;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Practitioner, otherT.Practitioner)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.Matches(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.Matches(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
            if( !DeepComparable.Matches(ReasonGiven, otherT.ReasonGiven)) return false;
            if( !DeepComparable.Matches(EffectiveTime, otherT.EffectiveTime)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationAdministration;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Practitioner, otherT.Practitioner)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.IsExactly(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
            if( !DeepComparable.IsExactly(ReasonGiven, otherT.ReasonGiven)) return false;
            if( !DeepComparable.IsExactly(EffectiveTime, otherT.EffectiveTime)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicationAdministration");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("practitioner", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Practitioner?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("prescription", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Prescription?.Serialize(sink);
            sink.Element("wasNotGiven", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); WasNotGivenElement?.Serialize(sink);
            sink.BeginList("reasonNotGiven", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReasonNotGiven)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonGiven", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReasonGiven)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("effectiveTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); EffectiveTime?.Serialize(sink);
            sink.Element("medication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Medication?.Serialize(sink);
            sink.BeginList("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Device)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NoteElement?.Serialize(sink);
            sink.Element("dosage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Dosage?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.MedicationAdministrationStatus>>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "practitioner":
                    Practitioner = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "prescription":
                    Prescription = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "wasNotGiven":
                    WasNotGivenElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "reasonNotGiven":
                    ReasonNotGiven = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonGiven":
                    ReasonGiven = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "effectiveTimeDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(EffectiveTime, "effectiveTime");
                    EffectiveTime = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "effectiveTimePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(EffectiveTime, "effectiveTime");
                    EffectiveTime = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "device":
                    Device = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    NoteElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "dosage":
                    Dosage = source.Get<DosageComponent>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "practitioner":
                    Practitioner = source.Populate(Practitioner);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "prescription":
                    Prescription = source.Populate(Prescription);
                    return true;
                case "wasNotGiven":
                    WasNotGivenElement = source.PopulateValue(WasNotGivenElement);
                    return true;
                case "_wasNotGiven":
                    WasNotGivenElement = source.Populate(WasNotGivenElement);
                    return true;
                case "reasonNotGiven":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonGiven":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "effectiveTimeDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(EffectiveTime, "effectiveTime");
                    EffectiveTime = source.PopulateValue(EffectiveTime as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_effectiveTimeDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(EffectiveTime, "effectiveTime");
                    EffectiveTime = source.Populate(EffectiveTime as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "effectiveTimePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(EffectiveTime, "effectiveTime");
                    EffectiveTime = source.Populate(EffectiveTime as Hl7.Fhir.Model.Period);
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "device":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    NoteElement = source.PopulateValue(NoteElement);
                    return true;
                case "_note":
                    NoteElement = source.Populate(NoteElement);
                    return true;
                case "dosage":
                    Dosage = source.Populate(Dosage);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "reasonNotGiven":
                    source.PopulateListItem(ReasonNotGiven, index);
                    return true;
                case "reasonGiven":
                    source.PopulateListItem(ReasonGiven, index);
                    return true;
                case "device":
                    source.PopulateListItem(Device, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (Patient != null) yield return Patient;
                if (Practitioner != null) yield return Practitioner;
                if (Encounter != null) yield return Encounter;
                if (Prescription != null) yield return Prescription;
                if (WasNotGivenElement != null) yield return WasNotGivenElement;
                foreach (var elem in ReasonNotGiven) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonGiven) { if (elem != null) yield return elem; }
                if (EffectiveTime != null) yield return EffectiveTime;
                if (Medication != null) yield return Medication;
                foreach (var elem in Device) { if (elem != null) yield return elem; }
                if (NoteElement != null) yield return NoteElement;
                if (Dosage != null) yield return Dosage;
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
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Practitioner != null) yield return new ElementValue("practitioner", Practitioner);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Prescription != null) yield return new ElementValue("prescription", Prescription);
                if (WasNotGivenElement != null) yield return new ElementValue("wasNotGiven", WasNotGivenElement);
                foreach (var elem in ReasonNotGiven) { if (elem != null) yield return new ElementValue("reasonNotGiven", elem); }
                foreach (var elem in ReasonGiven) { if (elem != null) yield return new ElementValue("reasonGiven", elem); }
                if (EffectiveTime != null) yield return new ElementValue("effectiveTime", EffectiveTime);
                if (Medication != null) yield return new ElementValue("medication", Medication);
                foreach (var elem in Device) { if (elem != null) yield return new ElementValue("device", elem); }
                if (NoteElement != null) yield return new ElementValue("note", NoteElement);
                if (Dosage != null) yield return new ElementValue("dosage", Dosage);
            }
        }
    
    }

}
