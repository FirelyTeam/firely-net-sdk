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
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Results of a measure evaluation
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "MeasureReport", IsResource=true)]
    [DataContract]
    public partial class MeasureReport : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMeasureReport, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MeasureReport; } }
        [NotMapped]
        public override string TypeName { get { return "MeasureReport"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureReportGroupComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IMeasureReportPopulationComponent> Hl7.Fhir.Model.IMeasureReportGroupComponent.Population { get { return Population; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IMeasureReportStratifierComponent> Hl7.Fhir.Model.IMeasureReportGroupComponent.Stratifier { get { return Stratifier; } }
            
            /// <summary>
            /// What group of the measure
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// The populations in the group
            /// </summary>
            [FhirElement("population", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<PopulationComponent> _Population;
            
            /// <summary>
            /// What score this group achieved
            /// </summary>
            [FhirElement("measureScore", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal MeasureScoreElement
            {
                get { return _MeasureScoreElement; }
                set { _MeasureScoreElement = value; OnPropertyChanged("MeasureScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _MeasureScoreElement;
            
            /// <summary>
            /// What score this group achieved
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? MeasureScore
            {
                get { return MeasureScoreElement != null ? MeasureScoreElement.Value : null; }
                set
                {
                    if (value == null)
                        MeasureScoreElement = null;
                    else
                        MeasureScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("MeasureScore");
                }
            }
            
            /// <summary>
            /// Stratification results
            /// </summary>
            [FhirElement("stratifier", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StratifierComponent> Stratifier
            {
                get { if(_Stratifier==null) _Stratifier = new List<StratifierComponent>(); return _Stratifier; }
                set { _Stratifier = value; OnPropertyChanged("Stratifier"); }
            }
            
            private List<StratifierComponent> _Stratifier;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GroupComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Identifier?.Serialize(sink);
                sink.BeginList("population", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Population)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("measureScore", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeasureScoreElement?.Serialize(sink);
                sink.BeginList("stratifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Stratifier)
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
                        Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "population":
                        Population = source.GetList<PopulationComponent>();
                        return true;
                    case "measureScore":
                        MeasureScoreElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "stratifier":
                        Stratifier = source.GetList<StratifierComponent>();
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
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "population":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "measureScore":
                        MeasureScoreElement = source.PopulateValue(MeasureScoreElement);
                        return true;
                    case "_measureScore":
                        MeasureScoreElement = source.Populate(MeasureScoreElement);
                        return true;
                    case "stratifier":
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
                    case "population":
                        source.PopulateListItem(Population, index);
                        return true;
                    case "stratifier":
                        source.PopulateListItem(Stratifier, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Population != null) dest.Population = new List<PopulationComponent>(Population.DeepCopy());
                    if(MeasureScoreElement != null) dest.MeasureScoreElement = (Hl7.Fhir.Model.FhirDecimal)MeasureScoreElement.DeepCopy();
                    if(Stratifier != null) dest.Stratifier = new List<StratifierComponent>(Stratifier.DeepCopy());
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
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
                if( !DeepComparable.Matches(Stratifier, otherT.Stratifier)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
                if( !DeepComparable.IsExactly(Stratifier, otherT.Stratifier)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    if (MeasureScoreElement != null) yield return MeasureScoreElement;
                    foreach (var elem in Stratifier) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    if (MeasureScoreElement != null) yield return new ElementValue("measureScore", MeasureScoreElement);
                    foreach (var elem in Stratifier) { if (elem != null) yield return new ElementValue("stratifier", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "PopulationComponent")]
        [DataContract]
        public partial class PopulationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureReportPopulationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PopulationComponent"; } }
            
            /// <summary>
            /// Population identifier as defined in the measure
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-score
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            [FhirElement("count", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer CountElement
            {
                get { return _CountElement; }
                set { _CountElement = value; OnPropertyChanged("CountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _CountElement;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Count
            {
                get { return CountElement != null ? CountElement.Value : null; }
                set
                {
                    if (value == null)
                        CountElement = null;
                    else
                        CountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Count");
                }
            }
            
            /// <summary>
            /// For patient-list reports, the patients in this population
            /// </summary>
            [FhirElement("patients", Order=70)]
            [CLSCompliant(false)]
            [References("List")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Patients
            {
                get { return _Patients; }
                set { _Patients = value; OnPropertyChanged("Patients"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Patients;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PopulationComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
                sink.Element("count", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CountElement?.Serialize(sink);
                sink.Element("patients", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Patients?.Serialize(sink);
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
                        Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "count":
                        CountElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "patients":
                        Patients = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "count":
                        CountElement = source.PopulateValue(CountElement);
                        return true;
                    case "_count":
                        CountElement = source.Populate(CountElement);
                        return true;
                    case "patients":
                        Patients = source.Populate(Patients);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PopulationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(Patients != null) dest.Patients = (Hl7.Fhir.Model.ResourceReference)Patients.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PopulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(Patients, otherT.Patients)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(Patients, otherT.Patients)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Code != null) yield return Code;
                    if (CountElement != null) yield return CountElement;
                    if (Patients != null) yield return Patients;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (Patients != null) yield return new ElementValue("patients", Patients);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "StratifierComponent")]
        [DataContract]
        public partial class StratifierComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureReportStratifierComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IMeasureReportStratifierGroupComponent> Hl7.Fhir.Model.IMeasureReportStratifierComponent.Stratum { get { return Stratum; } }
            
            /// <summary>
            /// What stratifier of the group
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Stratum results, one for each unique value in the stratifier
            /// </summary>
            [FhirElement("stratum", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StratifierGroupComponent> Stratum
            {
                get { if(_Stratum==null) _Stratum = new List<StratifierGroupComponent>(); return _Stratum; }
                set { _Stratum = value; OnPropertyChanged("Stratum"); }
            }
            
            private List<StratifierGroupComponent> _Stratum;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StratifierComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
                sink.BeginList("stratum", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Stratum)
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
                        Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "stratum":
                        Stratum = source.GetList<StratifierGroupComponent>();
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
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "stratum":
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
                    case "stratum":
                        source.PopulateListItem(Stratum, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Stratum != null) dest.Stratum = new List<StratifierGroupComponent>(Stratum.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StratifierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Stratum, otherT.Stratum)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Stratum, otherT.Stratum)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in Stratum) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    foreach (var elem in Stratum) { if (elem != null) yield return new ElementValue("stratum", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "StratifierGroupComponent")]
        [DataContract]
        public partial class StratifierGroupComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureReportStratifierGroupComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierGroupComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IMeasureReportStratifierGroupPopulationComponent> Hl7.Fhir.Model.IMeasureReportStratifierGroupComponent.Population { get { return Population; } }
            
            /// <summary>
            /// The stratum value, e.g. male
            /// </summary>
            [FhirElement("value", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The stratum value, e.g. male
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// Population results in this stratum
            /// </summary>
            [FhirElement("population", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StratifierGroupPopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<StratifierGroupPopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<StratifierGroupPopulationComponent> _Population;
            
            /// <summary>
            /// What score this stratum achieved
            /// </summary>
            [FhirElement("measureScore", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal MeasureScoreElement
            {
                get { return _MeasureScoreElement; }
                set { _MeasureScoreElement = value; OnPropertyChanged("MeasureScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _MeasureScoreElement;
            
            /// <summary>
            /// What score this stratum achieved
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? MeasureScore
            {
                get { return MeasureScoreElement != null ? MeasureScoreElement.Value : null; }
                set
                {
                    if (value == null)
                        MeasureScoreElement = null;
                    else
                        MeasureScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("MeasureScore");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StratifierGroupComponent");
                base.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
                sink.BeginList("population", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Population)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("measureScore", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeasureScoreElement?.Serialize(sink);
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
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "population":
                        Population = source.GetList<StratifierGroupPopulationComponent>();
                        return true;
                    case "measureScore":
                        MeasureScoreElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                    case "population":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "measureScore":
                        MeasureScoreElement = source.PopulateValue(MeasureScoreElement);
                        return true;
                    case "_measureScore":
                        MeasureScoreElement = source.Populate(MeasureScoreElement);
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
                    case "population":
                        source.PopulateListItem(Population, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierGroupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(Population != null) dest.Population = new List<StratifierGroupPopulationComponent>(Population.DeepCopy());
                    if(MeasureScoreElement != null) dest.MeasureScoreElement = (Hl7.Fhir.Model.FhirDecimal)MeasureScoreElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StratifierGroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierGroupComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierGroupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ValueElement != null) yield return ValueElement;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    if (MeasureScoreElement != null) yield return MeasureScoreElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    if (MeasureScoreElement != null) yield return new ElementValue("measureScore", MeasureScoreElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "StratifierGroupPopulationComponent")]
        [DataContract]
        public partial class StratifierGroupPopulationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureReportStratifierGroupPopulationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierGroupPopulationComponent"; } }
            
            /// <summary>
            /// Population identifier as defined in the measure
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-score
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            [FhirElement("count", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer CountElement
            {
                get { return _CountElement; }
                set { _CountElement = value; OnPropertyChanged("CountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _CountElement;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Count
            {
                get { return CountElement != null ? CountElement.Value : null; }
                set
                {
                    if (value == null)
                        CountElement = null;
                    else
                        CountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Count");
                }
            }
            
            /// <summary>
            /// For patient-list reports, the patients in this population
            /// </summary>
            [FhirElement("patients", Order=70)]
            [CLSCompliant(false)]
            [References("List")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Patients
            {
                get { return _Patients; }
                set { _Patients = value; OnPropertyChanged("Patients"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Patients;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StratifierGroupPopulationComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
                sink.Element("count", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CountElement?.Serialize(sink);
                sink.Element("patients", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Patients?.Serialize(sink);
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
                        Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "count":
                        CountElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "patients":
                        Patients = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "count":
                        CountElement = source.PopulateValue(CountElement);
                        return true;
                    case "_count":
                        CountElement = source.Populate(CountElement);
                        return true;
                    case "patients":
                        Patients = source.Populate(Patients);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierGroupPopulationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(Patients != null) dest.Patients = (Hl7.Fhir.Model.ResourceReference)Patients.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StratifierGroupPopulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierGroupPopulationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(Patients, otherT.Patients)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierGroupPopulationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(Patients, otherT.Patients)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Code != null) yield return Code;
                    if (CountElement != null) yield return CountElement;
                    if (Patients != null) yield return Patients;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (Patients != null) yield return new ElementValue("patients", Patients);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IMeasureReportGroupComponent> Hl7.Fhir.Model.IMeasureReport.Group { get { return Group; } }
    
        
        /// <summary>
        /// Additional identifier for the Report
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// complete | pending | error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MeasureReportStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MeasureReportStatus> _StatusElement;
        
        /// <summary>
        /// complete | pending | error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MeasureReportStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.MeasureReportStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// individual | patient-list | summary
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.MeasureReportType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.MeasureReportType> _TypeElement;
        
        /// <summary>
        /// individual | patient-list | summary
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.MeasureReportType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.STU3.MeasureReportType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// What measure was evaluated
        /// </summary>
        [FhirElement("measure", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Measure")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Measure
        {
            get { return _Measure; }
            set { _Measure = value; OnPropertyChanged("Measure"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Measure;
        
        /// <summary>
        /// What patient the report is for
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// When the report was generated
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the report was generated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                    DateElement = null;
                else
                    DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Who is reporting the data
        /// </summary>
        [FhirElement("reportingOrganization", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ReportingOrganization
        {
            get { return _ReportingOrganization; }
            set { _ReportingOrganization = value; OnPropertyChanged("ReportingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ReportingOrganization;
        
        /// <summary>
        /// What period the report covers
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Measure results for each group
        /// </summary>
        [FhirElement("group", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<GroupComponent> _Group;
        
        /// <summary>
        /// What data was evaluated to produce the measure score
        /// </summary>
        [FhirElement("evaluatedResources", Order=180)]
        [CLSCompliant(false)]
        [References("Bundle")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference EvaluatedResources
        {
            get { return _EvaluatedResources; }
            set { _EvaluatedResources = value; OnPropertyChanged("EvaluatedResources"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _EvaluatedResources;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MeasureReport;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MeasureReportStatus>)StatusElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.STU3.MeasureReportType>)TypeElement.DeepCopy();
                if(Measure != null) dest.Measure = (Hl7.Fhir.Model.ResourceReference)Measure.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(ReportingOrganization != null) dest.ReportingOrganization = (Hl7.Fhir.Model.ResourceReference)ReportingOrganization.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Group != null) dest.Group = new List<GroupComponent>(Group.DeepCopy());
                if(EvaluatedResources != null) dest.EvaluatedResources = (Hl7.Fhir.Model.ResourceReference)EvaluatedResources.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MeasureReport());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MeasureReport;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Measure, otherT.Measure)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(ReportingOrganization, otherT.ReportingOrganization)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            if( !DeepComparable.Matches(EvaluatedResources, otherT.EvaluatedResources)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MeasureReport;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Measure, otherT.Measure)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(ReportingOrganization, otherT.ReportingOrganization)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            if( !DeepComparable.IsExactly(EvaluatedResources, otherT.EvaluatedResources)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MeasureReport");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("measure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Measure?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Patient?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("reportingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReportingOrganization?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Period?.Serialize(sink);
            sink.BeginList("group", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Group)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("evaluatedResources", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EvaluatedResources?.Serialize(sink);
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
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.MeasureReportStatus>>();
                    return true;
                case "type":
                    TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MeasureReportType>>();
                    return true;
                case "measure":
                    Measure = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "reportingOrganization":
                    ReportingOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "group":
                    Group = source.GetList<GroupComponent>();
                    return true;
                case "evaluatedResources":
                    EvaluatedResources = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    Identifier = source.Populate(Identifier);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "measure":
                    Measure = source.Populate(Measure);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "reportingOrganization":
                    ReportingOrganization = source.Populate(ReportingOrganization);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "group":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "evaluatedResources":
                    EvaluatedResources = source.Populate(EvaluatedResources);
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
                case "group":
                    source.PopulateListItem(Group, index);
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
                if (Identifier != null) yield return Identifier;
                if (StatusElement != null) yield return StatusElement;
                if (TypeElement != null) yield return TypeElement;
                if (Measure != null) yield return Measure;
                if (Patient != null) yield return Patient;
                if (DateElement != null) yield return DateElement;
                if (ReportingOrganization != null) yield return ReportingOrganization;
                if (Period != null) yield return Period;
                foreach (var elem in Group) { if (elem != null) yield return elem; }
                if (EvaluatedResources != null) yield return EvaluatedResources;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (Measure != null) yield return new ElementValue("measure", Measure);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (ReportingOrganization != null) yield return new ElementValue("reportingOrganization", ReportingOrganization);
                if (Period != null) yield return new ElementValue("period", Period);
                foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
                if (EvaluatedResources != null) yield return new ElementValue("evaluatedResources", EvaluatedResources);
            }
        }
    
    }

}
