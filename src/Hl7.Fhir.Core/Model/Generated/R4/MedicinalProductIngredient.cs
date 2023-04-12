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
    /// An ingredient of a manufactured item or pharmaceutical product
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicinalProductIngredient", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductIngredient : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductIngredient; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductIngredient"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SpecifiedSubstanceComponent")]
        [DataContract]
        public partial class SpecifiedSubstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SpecifiedSubstanceComponent"; } }
            
            /// <summary>
            /// The specified substance
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// The group of specified substance, e.g. group 1 to 4
            /// </summary>
            [FhirElement("group", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Group
            {
                get { return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Group;
            
            /// <summary>
            /// Confidentiality level of the specified substance as the ingredient
            /// </summary>
            [FhirElement("confidentiality", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Confidentiality
            {
                get { return _Confidentiality; }
                set { _Confidentiality = value; OnPropertyChanged("Confidentiality"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Confidentiality;
            
            /// <summary>
            /// Quantity of the substance or specified substance present in the manufactured item or pharmaceutical product
            /// </summary>
            [FhirElement("strength", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StrengthComponent> Strength
            {
                get { if(_Strength==null) _Strength = new List<StrengthComponent>(); return _Strength; }
                set { _Strength = value; OnPropertyChanged("Strength"); }
            }
            
            private List<StrengthComponent> _Strength;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SpecifiedSubstanceComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("group", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Group?.Serialize(sink);
                sink.Element("confidentiality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Confidentiality?.Serialize(sink);
                sink.BeginList("strength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Strength)
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "group":
                        Group = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "confidentiality":
                        Confidentiality = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "strength":
                        Strength = source.GetList<StrengthComponent>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "group":
                        Group = source.Populate(Group);
                        return true;
                    case "confidentiality":
                        Confidentiality = source.Populate(Confidentiality);
                        return true;
                    case "strength":
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
                    case "strength":
                        source.PopulateListItem(Strength, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecifiedSubstanceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Group != null) dest.Group = (Hl7.Fhir.Model.CodeableConcept)Group.DeepCopy();
                    if(Confidentiality != null) dest.Confidentiality = (Hl7.Fhir.Model.CodeableConcept)Confidentiality.DeepCopy();
                    if(Strength != null) dest.Strength = new List<StrengthComponent>(Strength.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SpecifiedSubstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecifiedSubstanceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Group, otherT.Group)) return false;
                if( !DeepComparable.Matches(Confidentiality, otherT.Confidentiality)) return false;
                if( !DeepComparable.Matches(Strength, otherT.Strength)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecifiedSubstanceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
                if( !DeepComparable.IsExactly(Confidentiality, otherT.Confidentiality)) return false;
                if( !DeepComparable.IsExactly(Strength, otherT.Strength)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Group != null) yield return Group;
                    if (Confidentiality != null) yield return Confidentiality;
                    foreach (var elem in Strength) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Group != null) yield return new ElementValue("group", Group);
                    if (Confidentiality != null) yield return new ElementValue("confidentiality", Confidentiality);
                    foreach (var elem in Strength) { if (elem != null) yield return new ElementValue("strength", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "StrengthComponent")]
        [DataContract]
        public partial class StrengthComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StrengthComponent"; } }
            
            /// <summary>
            /// The quantity of substance in the unit of presentation, or in the volume (or mass) of the single pharmaceutical product or manufactured item
            /// </summary>
            [FhirElement("presentation", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Presentation
            {
                get { return _Presentation; }
                set { _Presentation = value; OnPropertyChanged("Presentation"); }
            }
            
            private Hl7.Fhir.Model.Ratio _Presentation;
            
            /// <summary>
            /// A lower limit for the quantity of substance in the unit of presentation. For use when there is a range of strengths, this is the lower limit, with the presentation attribute becoming the upper limit
            /// </summary>
            [FhirElement("presentationLowLimit", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio PresentationLowLimit
            {
                get { return _PresentationLowLimit; }
                set { _PresentationLowLimit = value; OnPropertyChanged("PresentationLowLimit"); }
            }
            
            private Hl7.Fhir.Model.Ratio _PresentationLowLimit;
            
            /// <summary>
            /// The strength per unitary volume (or mass)
            /// </summary>
            [FhirElement("concentration", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Concentration
            {
                get { return _Concentration; }
                set { _Concentration = value; OnPropertyChanged("Concentration"); }
            }
            
            private Hl7.Fhir.Model.Ratio _Concentration;
            
            /// <summary>
            /// A lower limit for the strength per unitary volume (or mass), for when there is a range. The concentration attribute then becomes the upper limit
            /// </summary>
            [FhirElement("concentrationLowLimit", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio ConcentrationLowLimit
            {
                get { return _ConcentrationLowLimit; }
                set { _ConcentrationLowLimit = value; OnPropertyChanged("ConcentrationLowLimit"); }
            }
            
            private Hl7.Fhir.Model.Ratio _ConcentrationLowLimit;
            
            /// <summary>
            /// For when strength is measured at a particular point or distance
            /// </summary>
            [FhirElement("measurementPoint", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MeasurementPointElement
            {
                get { return _MeasurementPointElement; }
                set { _MeasurementPointElement = value; OnPropertyChanged("MeasurementPointElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MeasurementPointElement;
            
            /// <summary>
            /// For when strength is measured at a particular point or distance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MeasurementPoint
            {
                get { return MeasurementPointElement != null ? MeasurementPointElement.Value : null; }
                set
                {
                    if (value == null)
                        MeasurementPointElement = null;
                    else
                        MeasurementPointElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MeasurementPoint");
                }
            }
            
            /// <summary>
            /// The country or countries for which the strength range applies
            /// </summary>
            [FhirElement("country", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Country
            {
                get { if(_Country==null) _Country = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Country; }
                set { _Country = value; OnPropertyChanged("Country"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Country;
            
            /// <summary>
            /// Strength expressed in terms of a reference substance
            /// </summary>
            [FhirElement("referenceStrength", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ReferenceStrengthComponent> ReferenceStrength
            {
                get { if(_ReferenceStrength==null) _ReferenceStrength = new List<ReferenceStrengthComponent>(); return _ReferenceStrength; }
                set { _ReferenceStrength = value; OnPropertyChanged("ReferenceStrength"); }
            }
            
            private List<ReferenceStrengthComponent> _ReferenceStrength;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StrengthComponent");
                base.Serialize(sink);
                sink.Element("presentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Presentation?.Serialize(sink);
                sink.Element("presentationLowLimit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PresentationLowLimit?.Serialize(sink);
                sink.Element("concentration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Concentration?.Serialize(sink);
                sink.Element("concentrationLowLimit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ConcentrationLowLimit?.Serialize(sink);
                sink.Element("measurementPoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeasurementPointElement?.Serialize(sink);
                sink.BeginList("country", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Country)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("referenceStrength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in ReferenceStrength)
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
                    case "presentation":
                        Presentation = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "presentationLowLimit":
                        PresentationLowLimit = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "concentration":
                        Concentration = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "concentrationLowLimit":
                        ConcentrationLowLimit = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "measurementPoint":
                        MeasurementPointElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "country":
                        Country = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "referenceStrength":
                        ReferenceStrength = source.GetList<ReferenceStrengthComponent>();
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
                    case "presentation":
                        Presentation = source.Populate(Presentation);
                        return true;
                    case "presentationLowLimit":
                        PresentationLowLimit = source.Populate(PresentationLowLimit);
                        return true;
                    case "concentration":
                        Concentration = source.Populate(Concentration);
                        return true;
                    case "concentrationLowLimit":
                        ConcentrationLowLimit = source.Populate(ConcentrationLowLimit);
                        return true;
                    case "measurementPoint":
                        MeasurementPointElement = source.PopulateValue(MeasurementPointElement);
                        return true;
                    case "_measurementPoint":
                        MeasurementPointElement = source.Populate(MeasurementPointElement);
                        return true;
                    case "country":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "referenceStrength":
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
                    case "country":
                        source.PopulateListItem(Country, index);
                        return true;
                    case "referenceStrength":
                        source.PopulateListItem(ReferenceStrength, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StrengthComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Presentation != null) dest.Presentation = (Hl7.Fhir.Model.Ratio)Presentation.DeepCopy();
                    if(PresentationLowLimit != null) dest.PresentationLowLimit = (Hl7.Fhir.Model.Ratio)PresentationLowLimit.DeepCopy();
                    if(Concentration != null) dest.Concentration = (Hl7.Fhir.Model.Ratio)Concentration.DeepCopy();
                    if(ConcentrationLowLimit != null) dest.ConcentrationLowLimit = (Hl7.Fhir.Model.Ratio)ConcentrationLowLimit.DeepCopy();
                    if(MeasurementPointElement != null) dest.MeasurementPointElement = (Hl7.Fhir.Model.FhirString)MeasurementPointElement.DeepCopy();
                    if(Country != null) dest.Country = new List<Hl7.Fhir.Model.CodeableConcept>(Country.DeepCopy());
                    if(ReferenceStrength != null) dest.ReferenceStrength = new List<ReferenceStrengthComponent>(ReferenceStrength.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StrengthComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StrengthComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Presentation, otherT.Presentation)) return false;
                if( !DeepComparable.Matches(PresentationLowLimit, otherT.PresentationLowLimit)) return false;
                if( !DeepComparable.Matches(Concentration, otherT.Concentration)) return false;
                if( !DeepComparable.Matches(ConcentrationLowLimit, otherT.ConcentrationLowLimit)) return false;
                if( !DeepComparable.Matches(MeasurementPointElement, otherT.MeasurementPointElement)) return false;
                if( !DeepComparable.Matches(Country, otherT.Country)) return false;
                if( !DeepComparable.Matches(ReferenceStrength, otherT.ReferenceStrength)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StrengthComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Presentation, otherT.Presentation)) return false;
                if( !DeepComparable.IsExactly(PresentationLowLimit, otherT.PresentationLowLimit)) return false;
                if( !DeepComparable.IsExactly(Concentration, otherT.Concentration)) return false;
                if( !DeepComparable.IsExactly(ConcentrationLowLimit, otherT.ConcentrationLowLimit)) return false;
                if( !DeepComparable.IsExactly(MeasurementPointElement, otherT.MeasurementPointElement)) return false;
                if( !DeepComparable.IsExactly(Country, otherT.Country)) return false;
                if( !DeepComparable.IsExactly(ReferenceStrength, otherT.ReferenceStrength)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Presentation != null) yield return Presentation;
                    if (PresentationLowLimit != null) yield return PresentationLowLimit;
                    if (Concentration != null) yield return Concentration;
                    if (ConcentrationLowLimit != null) yield return ConcentrationLowLimit;
                    if (MeasurementPointElement != null) yield return MeasurementPointElement;
                    foreach (var elem in Country) { if (elem != null) yield return elem; }
                    foreach (var elem in ReferenceStrength) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Presentation != null) yield return new ElementValue("presentation", Presentation);
                    if (PresentationLowLimit != null) yield return new ElementValue("presentationLowLimit", PresentationLowLimit);
                    if (Concentration != null) yield return new ElementValue("concentration", Concentration);
                    if (ConcentrationLowLimit != null) yield return new ElementValue("concentrationLowLimit", ConcentrationLowLimit);
                    if (MeasurementPointElement != null) yield return new ElementValue("measurementPoint", MeasurementPointElement);
                    foreach (var elem in Country) { if (elem != null) yield return new ElementValue("country", elem); }
                    foreach (var elem in ReferenceStrength) { if (elem != null) yield return new ElementValue("referenceStrength", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ReferenceStrengthComponent")]
        [DataContract]
        public partial class ReferenceStrengthComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ReferenceStrengthComponent"; } }
            
            /// <summary>
            /// Relevant reference substance
            /// </summary>
            [FhirElement("substance", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Substance
            {
                get { return _Substance; }
                set { _Substance = value; OnPropertyChanged("Substance"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Substance;
            
            /// <summary>
            /// Strength expressed in terms of a reference substance
            /// </summary>
            [FhirElement("strength", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Strength
            {
                get { return _Strength; }
                set { _Strength = value; OnPropertyChanged("Strength"); }
            }
            
            private Hl7.Fhir.Model.Ratio _Strength;
            
            /// <summary>
            /// Strength expressed in terms of a reference substance
            /// </summary>
            [FhirElement("strengthLowLimit", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio StrengthLowLimit
            {
                get { return _StrengthLowLimit; }
                set { _StrengthLowLimit = value; OnPropertyChanged("StrengthLowLimit"); }
            }
            
            private Hl7.Fhir.Model.Ratio _StrengthLowLimit;
            
            /// <summary>
            /// For when strength is measured at a particular point or distance
            /// </summary>
            [FhirElement("measurementPoint", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MeasurementPointElement
            {
                get { return _MeasurementPointElement; }
                set { _MeasurementPointElement = value; OnPropertyChanged("MeasurementPointElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MeasurementPointElement;
            
            /// <summary>
            /// For when strength is measured at a particular point or distance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MeasurementPoint
            {
                get { return MeasurementPointElement != null ? MeasurementPointElement.Value : null; }
                set
                {
                    if (value == null)
                        MeasurementPointElement = null;
                    else
                        MeasurementPointElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MeasurementPoint");
                }
            }
            
            /// <summary>
            /// The country or countries for which the strength range applies
            /// </summary>
            [FhirElement("country", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Country
            {
                get { if(_Country==null) _Country = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Country; }
                set { _Country = value; OnPropertyChanged("Country"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Country;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ReferenceStrengthComponent");
                base.Serialize(sink);
                sink.Element("substance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Substance?.Serialize(sink);
                sink.Element("strength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Strength?.Serialize(sink);
                sink.Element("strengthLowLimit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StrengthLowLimit?.Serialize(sink);
                sink.Element("measurementPoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeasurementPointElement?.Serialize(sink);
                sink.BeginList("country", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Country)
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
                    case "substance":
                        Substance = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "strength":
                        Strength = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "strengthLowLimit":
                        StrengthLowLimit = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "measurementPoint":
                        MeasurementPointElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "country":
                        Country = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "substance":
                        Substance = source.Populate(Substance);
                        return true;
                    case "strength":
                        Strength = source.Populate(Strength);
                        return true;
                    case "strengthLowLimit":
                        StrengthLowLimit = source.Populate(StrengthLowLimit);
                        return true;
                    case "measurementPoint":
                        MeasurementPointElement = source.PopulateValue(MeasurementPointElement);
                        return true;
                    case "_measurementPoint":
                        MeasurementPointElement = source.Populate(MeasurementPointElement);
                        return true;
                    case "country":
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
                    case "country":
                        source.PopulateListItem(Country, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReferenceStrengthComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Substance != null) dest.Substance = (Hl7.Fhir.Model.CodeableConcept)Substance.DeepCopy();
                    if(Strength != null) dest.Strength = (Hl7.Fhir.Model.Ratio)Strength.DeepCopy();
                    if(StrengthLowLimit != null) dest.StrengthLowLimit = (Hl7.Fhir.Model.Ratio)StrengthLowLimit.DeepCopy();
                    if(MeasurementPointElement != null) dest.MeasurementPointElement = (Hl7.Fhir.Model.FhirString)MeasurementPointElement.DeepCopy();
                    if(Country != null) dest.Country = new List<Hl7.Fhir.Model.CodeableConcept>(Country.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ReferenceStrengthComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReferenceStrengthComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
                if( !DeepComparable.Matches(Strength, otherT.Strength)) return false;
                if( !DeepComparable.Matches(StrengthLowLimit, otherT.StrengthLowLimit)) return false;
                if( !DeepComparable.Matches(MeasurementPointElement, otherT.MeasurementPointElement)) return false;
                if( !DeepComparable.Matches(Country, otherT.Country)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReferenceStrengthComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
                if( !DeepComparable.IsExactly(Strength, otherT.Strength)) return false;
                if( !DeepComparable.IsExactly(StrengthLowLimit, otherT.StrengthLowLimit)) return false;
                if( !DeepComparable.IsExactly(MeasurementPointElement, otherT.MeasurementPointElement)) return false;
                if( !DeepComparable.IsExactly(Country, otherT.Country)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Substance != null) yield return Substance;
                    if (Strength != null) yield return Strength;
                    if (StrengthLowLimit != null) yield return StrengthLowLimit;
                    if (MeasurementPointElement != null) yield return MeasurementPointElement;
                    foreach (var elem in Country) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Substance != null) yield return new ElementValue("substance", Substance);
                    if (Strength != null) yield return new ElementValue("strength", Strength);
                    if (StrengthLowLimit != null) yield return new ElementValue("strengthLowLimit", StrengthLowLimit);
                    if (MeasurementPointElement != null) yield return new ElementValue("measurementPoint", MeasurementPointElement);
                    foreach (var elem in Country) { if (elem != null) yield return new ElementValue("country", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SubstanceComponent")]
        [DataContract]
        public partial class SubstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubstanceComponent"; } }
            
            /// <summary>
            /// The ingredient substance
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Quantity of the substance or specified substance present in the manufactured item or pharmaceutical product
            /// </summary>
            [FhirElement("strength", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StrengthComponent> Strength
            {
                get { if(_Strength==null) _Strength = new List<StrengthComponent>(); return _Strength; }
                set { _Strength = value; OnPropertyChanged("Strength"); }
            }
            
            private List<StrengthComponent> _Strength;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubstanceComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.BeginList("strength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Strength)
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "strength":
                        Strength = source.GetList<StrengthComponent>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "strength":
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
                    case "strength":
                        source.PopulateListItem(Strength, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubstanceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Strength != null) dest.Strength = new List<StrengthComponent>(Strength.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubstanceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Strength, otherT.Strength)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubstanceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Strength, otherT.Strength)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    foreach (var elem in Strength) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    foreach (var elem in Strength) { if (elem != null) yield return new ElementValue("strength", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Identifier for the ingredient
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
        /// Ingredient role e.g. Active ingredient, excipient
        /// </summary>
        [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Role
        {
            get { return _Role; }
            set { _Role = value; OnPropertyChanged("Role"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Role;
        
        /// <summary>
        /// If the ingredient is a known or suspected allergen
        /// </summary>
        [FhirElement("allergenicIndicator", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AllergenicIndicatorElement
        {
            get { return _AllergenicIndicatorElement; }
            set { _AllergenicIndicatorElement = value; OnPropertyChanged("AllergenicIndicatorElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _AllergenicIndicatorElement;
        
        /// <summary>
        /// If the ingredient is a known or suspected allergen
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? AllergenicIndicator
        {
            get { return AllergenicIndicatorElement != null ? AllergenicIndicatorElement.Value : null; }
            set
            {
                if (value == null)
                    AllergenicIndicatorElement = null;
                else
                    AllergenicIndicatorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("AllergenicIndicator");
            }
        }
        
        /// <summary>
        /// Manufacturer of this Ingredient
        /// </summary>
        [FhirElement("manufacturer", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Manufacturer
        {
            get { if(_Manufacturer==null) _Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Manufacturer;
        
        /// <summary>
        /// A specified substance that comprises this ingredient
        /// </summary>
        [FhirElement("specifiedSubstance", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SpecifiedSubstanceComponent> SpecifiedSubstance
        {
            get { if(_SpecifiedSubstance==null) _SpecifiedSubstance = new List<SpecifiedSubstanceComponent>(); return _SpecifiedSubstance; }
            set { _SpecifiedSubstance = value; OnPropertyChanged("SpecifiedSubstance"); }
        }
        
        private List<SpecifiedSubstanceComponent> _SpecifiedSubstance;
        
        /// <summary>
        /// The ingredient substance
        /// </summary>
        [FhirElement("substance", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public SubstanceComponent Substance
        {
            get { return _Substance; }
            set { _Substance = value; OnPropertyChanged("Substance"); }
        }
        
        private SubstanceComponent _Substance;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductIngredient;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                if(AllergenicIndicatorElement != null) dest.AllergenicIndicatorElement = (Hl7.Fhir.Model.FhirBoolean)AllergenicIndicatorElement.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if(SpecifiedSubstance != null) dest.SpecifiedSubstance = new List<SpecifiedSubstanceComponent>(SpecifiedSubstance.DeepCopy());
                if(Substance != null) dest.Substance = (SubstanceComponent)Substance.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicinalProductIngredient());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductIngredient;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Role, otherT.Role)) return false;
            if( !DeepComparable.Matches(AllergenicIndicatorElement, otherT.AllergenicIndicatorElement)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(SpecifiedSubstance, otherT.SpecifiedSubstance)) return false;
            if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductIngredient;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
            if( !DeepComparable.IsExactly(AllergenicIndicatorElement, otherT.AllergenicIndicatorElement)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(SpecifiedSubstance, otherT.SpecifiedSubstance)) return false;
            if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicinalProductIngredient");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Role?.Serialize(sink);
            sink.Element("allergenicIndicator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AllergenicIndicatorElement?.Serialize(sink);
            sink.BeginList("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Manufacturer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("specifiedSubstance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in SpecifiedSubstance)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("substance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Substance?.Serialize(sink);
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
                case "role":
                    Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "allergenicIndicator":
                    AllergenicIndicatorElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "manufacturer":
                    Manufacturer = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "specifiedSubstance":
                    SpecifiedSubstance = source.GetList<SpecifiedSubstanceComponent>();
                    return true;
                case "substance":
                    Substance = source.Get<SubstanceComponent>();
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
                case "role":
                    Role = source.Populate(Role);
                    return true;
                case "allergenicIndicator":
                    AllergenicIndicatorElement = source.PopulateValue(AllergenicIndicatorElement);
                    return true;
                case "_allergenicIndicator":
                    AllergenicIndicatorElement = source.Populate(AllergenicIndicatorElement);
                    return true;
                case "manufacturer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "specifiedSubstance":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "substance":
                    Substance = source.Populate(Substance);
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
                case "manufacturer":
                    source.PopulateListItem(Manufacturer, index);
                    return true;
                case "specifiedSubstance":
                    source.PopulateListItem(SpecifiedSubstance, index);
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
                if (Role != null) yield return Role;
                if (AllergenicIndicatorElement != null) yield return AllergenicIndicatorElement;
                foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                foreach (var elem in SpecifiedSubstance) { if (elem != null) yield return elem; }
                if (Substance != null) yield return Substance;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (Role != null) yield return new ElementValue("role", Role);
                if (AllergenicIndicatorElement != null) yield return new ElementValue("allergenicIndicator", AllergenicIndicatorElement);
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                foreach (var elem in SpecifiedSubstance) { if (elem != null) yield return new ElementValue("specifiedSubstance", elem); }
                if (Substance != null) yield return new ElementValue("substance", Substance);
            }
        }
    
    }

}
