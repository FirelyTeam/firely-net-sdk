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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Administration of medication to a patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicationAdministration", IsResource=true)]
    [DataContract]
    public partial class MedicationAdministration : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMedicationAdministration, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationAdministration; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationAdministration"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PerformerComponent")]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// Type of performance
            /// </summary>
            [FhirElement("function", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Function
            {
                get { return _Function; }
                set { _Function = value; OnPropertyChanged("Function"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Function;
            
            /// <summary>
            /// Who performed the medication administration
            /// </summary>
            [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole","Patient","RelatedPerson","Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PerformerComponent");
                base.Serialize(sink);
                sink.Element("function", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Function?.Serialize(sink);
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Actor?.Serialize(sink);
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
                    case "function":
                        Function = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "actor":
                        Actor = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "function":
                        Function = source.Populate(Function);
                        return true;
                    case "actor":
                        Actor = source.Populate(Actor);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PerformerComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Function != null) dest.Function = (Hl7.Fhir.Model.CodeableConcept)Function.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Function, otherT.Function)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Function, otherT.Function)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Function != null) yield return Function;
                    if (Actor != null) yield return Actor;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Function != null) yield return new ElementValue("function", Function);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DosageComponent")]
        [DataContract]
        public partial class DosageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMedicationAdministrationDosageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DosageComponent"; } }
            
            /// <summary>
            /// Free text dosage instructions e.g. SIG
            /// </summary>
            [FhirElement("text", Order=40)]
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
            /// Body site administered to
            /// </summary>
            [FhirElement("site", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Site;
            
            /// <summary>
            /// Path of substance into body
            /// </summary>
            [FhirElement("route", Order=60)]
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
            [FhirElement("method", Order=70)]
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
            [FhirElement("dose", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Dose
            {
                get { return _Dose; }
                set { _Dose = value; OnPropertyChanged("Dose"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Dose;
            
            /// <summary>
            /// Dose quantity per unit of time
            /// </summary>
            [FhirElement("rate", Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.SimpleQuantity))]
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
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TextElement?.Serialize(sink);
                sink.Element("site", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Site?.Serialize(sink);
                sink.Element("route", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Route?.Serialize(sink);
                sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Method?.Serialize(sink);
                sink.Element("dose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Dose?.Serialize(sink);
                sink.Element("rate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Rate?.Serialize(sink);
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
                    case "site":
                        Site = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "route":
                        Route = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "method":
                        Method = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "dose":
                        Dose = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "rateRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Rate, "rate");
                        Rate = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "rateQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.SimpleQuantity>(Rate, "rate");
                        Rate = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
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
                    case "site":
                        Site = source.Populate(Site);
                        return true;
                    case "route":
                        Route = source.Populate(Route);
                        return true;
                    case "method":
                        Method = source.Populate(Method);
                        return true;
                    case "dose":
                        Dose = source.Populate(Dose);
                        return true;
                    case "rateRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Rate, "rate");
                        Rate = source.Populate(Rate as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "rateQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.SimpleQuantity>(Rate, "rate");
                        Rate = source.Populate(Rate as Hl7.Fhir.Model.SimpleQuantity);
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
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                    if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(Dose != null) dest.Dose = (Hl7.Fhir.Model.SimpleQuantity)Dose.DeepCopy();
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
                if( !DeepComparable.Matches(Dose, otherT.Dose)) return false;
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
                    if (TextElement != null) yield return TextElement;
                    if (Site != null) yield return Site;
                    if (Route != null) yield return Route;
                    if (Method != null) yield return Method;
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
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (Site != null) yield return new ElementValue("site", Site);
                    if (Route != null) yield return new ElementValue("route", Route);
                    if (Method != null) yield return new ElementValue("method", Method);
                    if (Dose != null) yield return new ElementValue("dose", Dose);
                    if (Rate != null) yield return new ElementValue("rate", Rate);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.IMedicationAdministrationDosageComponent Hl7.Fhir.Model.IMedicationAdministration.Dosage { get { return Dosage; } }
    
        
        /// <summary>
        /// External identifier
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
        /// Instantiates protocol or definition
        /// </summary>
        [FhirElement("instantiates", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> InstantiatesElement
        {
            get { if(_InstantiatesElement==null) _InstantiatesElement = new List<Hl7.Fhir.Model.FhirUri>(); return _InstantiatesElement; }
            set { _InstantiatesElement = value; OnPropertyChanged("InstantiatesElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _InstantiatesElement;
        
        /// <summary>
        /// Instantiates protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Instantiates
        {
            get { return InstantiatesElement != null ? InstantiatesElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    InstantiatesElement = null;
                else
                    InstantiatesElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Instantiates");
            }
        }
        
        /// <summary>
        /// Part of referenced event
        /// </summary>
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("MedicationAdministration","Procedure")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// in-progress | not-done | on-hold | completed | entered-in-error | stopped | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.MedicationAdministrationStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.MedicationAdministrationStatusCodes> _StatusElement;
        
        /// <summary>
        /// in-progress | not-done | on-hold | completed | entered-in-error | stopped | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.MedicationAdministrationStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.MedicationAdministrationStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason administration not performed
        /// </summary>
        [FhirElement("statusReason", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> StatusReason
        {
            get { if(_StatusReason==null) _StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _StatusReason;
        
        /// <summary>
        /// Type of medication usage
        /// </summary>
        [FhirElement("category", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// What was administered
        /// </summary>
        [FhirElement("medication", InSummary=Hl7.Fhir.Model.Version.All, Order=150, Choice=ChoiceType.DatatypeChoice)]
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
        /// Who received medication
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter or Episode of Care administered as part of
        /// </summary>
        [FhirElement("context", Order=170)]
        [CLSCompliant(false)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Additional information to support administration
        /// </summary>
        [FhirElement("supportingInformation", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        
        /// <summary>
        /// Start and end time of administration
        /// </summary>
        [FhirElement("effective", InSummary=Hl7.Fhir.Model.Version.All, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// Who performed the medication administration and what they did
        /// </summary>
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<PerformerComponent> _Performer;
        
        /// <summary>
        /// Reason administration performed
        /// </summary>
        [FhirElement("reasonCode", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Condition or observation that supports why the medication was administered
        /// </summary>
        [FhirElement("reasonReference", Order=220)]
        [CLSCompliant(false)]
        [References("Condition","Observation","DiagnosticReport")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Request administration performed against
        /// </summary>
        [FhirElement("request", Order=230)]
        [CLSCompliant(false)]
        [References("MedicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Device used to administer
        /// </summary>
        [FhirElement("device", Order=240)]
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
        [FhirElement("note", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Details of how medication was taken
        /// </summary>
        [FhirElement("dosage", Order=260)]
        [DataMember]
        public DosageComponent Dosage
        {
            get { return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private DosageComponent _Dosage;
        
        /// <summary>
        /// A list of events of interest in the lifecycle
        /// </summary>
        [FhirElement("eventHistory", Order=270)]
        [CLSCompliant(false)]
        [References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EventHistory
        {
            get { if(_EventHistory==null) _EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _EventHistory; }
            set { _EventHistory = value; OnPropertyChanged("EventHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EventHistory;
    
    
        public static ElementDefinitionConstraint[] MedicationAdministration_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "mad-1",
                severity: ConstraintSeverity.Warning,
                expression: "dosage.all(dose.exists() or rate.exists())",
                human: "SHALL have at least one of dosage.dose or dosage.rate[x]",
                xpath: "exists(f:dose) or exists(f:*[starts-with(local-name(.), 'rate')])"
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
                if(InstantiatesElement != null) dest.InstantiatesElement = new List<Hl7.Fhir.Model.FhirUri>(InstantiatesElement.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.MedicationAdministrationStatusCodes>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(StatusReason.DeepCopy());
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(Performer != null) dest.Performer = new List<PerformerComponent>(Performer.DeepCopy());
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Device != null) dest.Device = new List<Hl7.Fhir.Model.ResourceReference>(Device.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Dosage != null) dest.Dosage = (DosageComponent)Dosage.DeepCopy();
                if(EventHistory != null) dest.EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(EventHistory.DeepCopy());
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
            if( !DeepComparable.Matches(InstantiatesElement, otherT.InstantiatesElement)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
            if( !DeepComparable.Matches(EventHistory, otherT.EventHistory)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationAdministration;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(InstantiatesElement, otherT.InstantiatesElement)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
            if( !DeepComparable.IsExactly(EventHistory, otherT.EventHistory)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicationAdministration");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("instantiates", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(InstantiatesElement);
            sink.End();
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.BeginList("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in StatusReason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
            sink.Element("medication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Medication?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Context?.Serialize(sink);
            sink.BeginList("supportingInformation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SupportingInformation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("effective", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Effective?.Serialize(sink);
            sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Performer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Request?.Serialize(sink);
            sink.BeginList("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Device)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("dosage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Dosage?.Serialize(sink);
            sink.BeginList("eventHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in EventHistory)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                case "instantiates":
                    InstantiatesElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.MedicationAdministrationStatusCodes>>();
                    return true;
                case "statusReason":
                    StatusReason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "supportingInformation":
                    SupportingInformation = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "effectivePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Effective, "effective");
                    Effective = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "performer":
                    Performer = source.GetList<PerformerComponent>();
                    return true;
                case "reasonCode":
                    ReasonCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    ReasonReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "request":
                    Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "device":
                    Device = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "dosage":
                    Dosage = source.Get<DosageComponent>();
                    return true;
                case "eventHistory":
                    EventHistory = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                case "instantiates":
                case "_instantiates":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusReason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "context":
                    Context = source.Populate(Context);
                    return true;
                case "supportingInformation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.PopulateValue(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "effectivePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.Period);
                    return true;
                case "performer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "request":
                    Request = source.Populate(Request);
                    return true;
                case "device":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dosage":
                    Dosage = source.Populate(Dosage);
                    return true;
                case "eventHistory":
                    source.SetList(this, jsonPropertyName);
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
                case "instantiates":
                    source.PopulatePrimitiveListItemValue(InstantiatesElement, index);
                    return true;
                case "_instantiates":
                    source.PopulatePrimitiveListItem(InstantiatesElement, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
                case "statusReason":
                    source.PopulateListItem(StatusReason, index);
                    return true;
                case "supportingInformation":
                    source.PopulateListItem(SupportingInformation, index);
                    return true;
                case "performer":
                    source.PopulateListItem(Performer, index);
                    return true;
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "device":
                    source.PopulateListItem(Device, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "eventHistory":
                    source.PopulateListItem(EventHistory, index);
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
                foreach (var elem in InstantiatesElement) { if (elem != null) yield return elem; }
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in StatusReason) { if (elem != null) yield return elem; }
                if (Category != null) yield return Category;
                if (Medication != null) yield return Medication;
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
                if (Effective != null) yield return Effective;
                foreach (var elem in Performer) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                if (Request != null) yield return Request;
                foreach (var elem in Device) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                if (Dosage != null) yield return Dosage;
                foreach (var elem in EventHistory) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in InstantiatesElement) { if (elem != null) yield return new ElementValue("instantiates", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in StatusReason) { if (elem != null) yield return new ElementValue("statusReason", elem); }
                if (Category != null) yield return new ElementValue("category", Category);
                if (Medication != null) yield return new ElementValue("medication", Medication);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
                if (Effective != null) yield return new ElementValue("effective", Effective);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                if (Request != null) yield return new ElementValue("request", Request);
                foreach (var elem in Device) { if (elem != null) yield return new ElementValue("device", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                if (Dosage != null) yield return new ElementValue("dosage", Dosage);
                foreach (var elem in EventHistory) { if (elem != null) yield return new ElementValue("eventHistory", elem); }
            }
        }
    
    }

}
