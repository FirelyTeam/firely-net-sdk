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
    /// Detailed definition of a medicinal product, typically for uses other than direct patient care (e.g. regulatory use)
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicinalProduct", IsResource=true)]
    [DataContract]
    public partial class MedicinalProduct : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProduct; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProduct"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "NameComponent")]
        [DataContract]
        public partial class NameComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NameComponent"; } }
            
            /// <summary>
            /// The full product name
            /// </summary>
            [FhirElement("productName", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ProductNameElement
            {
                get { return _ProductNameElement; }
                set { _ProductNameElement = value; OnPropertyChanged("ProductNameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ProductNameElement;
            
            /// <summary>
            /// The full product name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ProductName
            {
                get { return ProductNameElement != null ? ProductNameElement.Value : null; }
                set
                {
                    if (value == null)
                        ProductNameElement = null;
                    else
                        ProductNameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ProductName");
                }
            }
            
            /// <summary>
            /// Coding words or phrases of the name
            /// </summary>
            [FhirElement("namePart", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<NamePartComponent> NamePart
            {
                get { if(_NamePart==null) _NamePart = new List<NamePartComponent>(); return _NamePart; }
                set { _NamePart = value; OnPropertyChanged("NamePart"); }
            }
            
            private List<NamePartComponent> _NamePart;
            
            /// <summary>
            /// Country where the name applies
            /// </summary>
            [FhirElement("countryLanguage", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<CountryLanguageComponent> CountryLanguage
            {
                get { if(_CountryLanguage==null) _CountryLanguage = new List<CountryLanguageComponent>(); return _CountryLanguage; }
                set { _CountryLanguage = value; OnPropertyChanged("CountryLanguage"); }
            }
            
            private List<CountryLanguageComponent> _CountryLanguage;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NameComponent");
                base.Serialize(sink);
                sink.Element("productName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ProductNameElement?.Serialize(sink);
                sink.BeginList("namePart", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in NamePart)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("countryLanguage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in CountryLanguage)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "productName":
                        ProductNameElement = source.PopulateValue(ProductNameElement);
                        return true;
                    case "_productName":
                        ProductNameElement = source.Populate(ProductNameElement);
                        return true;
                    case "namePart":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "countryLanguage":
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
                    case "namePart":
                        source.PopulateListItem(NamePart, index);
                        return true;
                    case "countryLanguage":
                        source.PopulateListItem(CountryLanguage, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NameComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ProductNameElement != null) dest.ProductNameElement = (Hl7.Fhir.Model.FhirString)ProductNameElement.DeepCopy();
                    if(NamePart != null) dest.NamePart = new List<NamePartComponent>(NamePart.DeepCopy());
                    if(CountryLanguage != null) dest.CountryLanguage = new List<CountryLanguageComponent>(CountryLanguage.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NameComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NameComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ProductNameElement, otherT.ProductNameElement)) return false;
                if( !DeepComparable.Matches(NamePart, otherT.NamePart)) return false;
                if( !DeepComparable.Matches(CountryLanguage, otherT.CountryLanguage)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NameComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ProductNameElement, otherT.ProductNameElement)) return false;
                if( !DeepComparable.IsExactly(NamePart, otherT.NamePart)) return false;
                if( !DeepComparable.IsExactly(CountryLanguage, otherT.CountryLanguage)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ProductNameElement != null) yield return ProductNameElement;
                    foreach (var elem in NamePart) { if (elem != null) yield return elem; }
                    foreach (var elem in CountryLanguage) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ProductNameElement != null) yield return new ElementValue("productName", ProductNameElement);
                    foreach (var elem in NamePart) { if (elem != null) yield return new ElementValue("namePart", elem); }
                    foreach (var elem in CountryLanguage) { if (elem != null) yield return new ElementValue("countryLanguage", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "NamePartComponent")]
        [DataContract]
        public partial class NamePartComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NamePartComponent"; } }
            
            /// <summary>
            /// A fragment of a product name
            /// </summary>
            [FhirElement("part", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PartElement
            {
                get { return _PartElement; }
                set { _PartElement = value; OnPropertyChanged("PartElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PartElement;
            
            /// <summary>
            /// A fragment of a product name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Part
            {
                get { return PartElement != null ? PartElement.Value : null; }
                set
                {
                    if (value == null)
                        PartElement = null;
                    else
                        PartElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Part");
                }
            }
            
            /// <summary>
            /// Idenifying type for this part of the name (e.g. strength part)
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NamePartComponent");
                base.Serialize(sink);
                sink.Element("part", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PartElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "part":
                        PartElement = source.PopulateValue(PartElement);
                        return true;
                    case "_part":
                        PartElement = source.Populate(PartElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NamePartComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PartElement != null) dest.PartElement = (Hl7.Fhir.Model.FhirString)PartElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NamePartComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NamePartComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PartElement, otherT.PartElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NamePartComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PartElement, otherT.PartElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PartElement != null) yield return PartElement;
                    if (Type != null) yield return Type;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PartElement != null) yield return new ElementValue("part", PartElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CountryLanguageComponent")]
        [DataContract]
        public partial class CountryLanguageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CountryLanguageComponent"; } }
            
            /// <summary>
            /// Country code for where this name applies
            /// </summary>
            [FhirElement("country", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Country
            {
                get { return _Country; }
                set { _Country = value; OnPropertyChanged("Country"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Country;
            
            /// <summary>
            /// Jurisdiction code for where this name applies
            /// </summary>
            [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Jurisdiction
            {
                get { return _Jurisdiction; }
                set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Jurisdiction;
            
            /// <summary>
            /// Language code for this name
            /// </summary>
            [FhirElement("language", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Language
            {
                get { return _Language; }
                set { _Language = value; OnPropertyChanged("Language"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Language;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CountryLanguageComponent");
                base.Serialize(sink);
                sink.Element("country", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Country?.Serialize(sink);
                sink.Element("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Jurisdiction?.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Language?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "country":
                        Country = source.Populate(Country);
                        return true;
                    case "jurisdiction":
                        Jurisdiction = source.Populate(Jurisdiction);
                        return true;
                    case "language":
                        Language = source.Populate(Language);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CountryLanguageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Country != null) dest.Country = (Hl7.Fhir.Model.CodeableConcept)Country.DeepCopy();
                    if(Jurisdiction != null) dest.Jurisdiction = (Hl7.Fhir.Model.CodeableConcept)Jurisdiction.DeepCopy();
                    if(Language != null) dest.Language = (Hl7.Fhir.Model.CodeableConcept)Language.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CountryLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CountryLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Country, otherT.Country)) return false;
                if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
                if( !DeepComparable.Matches(Language, otherT.Language)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CountryLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Country, otherT.Country)) return false;
                if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
                if( !DeepComparable.IsExactly(Language, otherT.Language)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Country != null) yield return Country;
                    if (Jurisdiction != null) yield return Jurisdiction;
                    if (Language != null) yield return Language;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Country != null) yield return new ElementValue("country", Country);
                    if (Jurisdiction != null) yield return new ElementValue("jurisdiction", Jurisdiction);
                    if (Language != null) yield return new ElementValue("language", Language);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ManufacturingBusinessOperationComponent")]
        [DataContract]
        public partial class ManufacturingBusinessOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ManufacturingBusinessOperationComponent"; } }
            
            /// <summary>
            /// The type of manufacturing operation
            /// </summary>
            [FhirElement("operationType", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept OperationType
            {
                get { return _OperationType; }
                set { _OperationType = value; OnPropertyChanged("OperationType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _OperationType;
            
            /// <summary>
            /// Regulatory authorization reference number
            /// </summary>
            [FhirElement("authorisationReferenceNumber", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier AuthorisationReferenceNumber
            {
                get { return _AuthorisationReferenceNumber; }
                set { _AuthorisationReferenceNumber = value; OnPropertyChanged("AuthorisationReferenceNumber"); }
            }
            
            private Hl7.Fhir.Model.Identifier _AuthorisationReferenceNumber;
            
            /// <summary>
            /// Regulatory authorization date
            /// </summary>
            [FhirElement("effectiveDate", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime EffectiveDateElement
            {
                get { return _EffectiveDateElement; }
                set { _EffectiveDateElement = value; OnPropertyChanged("EffectiveDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _EffectiveDateElement;
            
            /// <summary>
            /// Regulatory authorization date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string EffectiveDate
            {
                get { return EffectiveDateElement != null ? EffectiveDateElement.Value : null; }
                set
                {
                    if (value == null)
                        EffectiveDateElement = null;
                    else
                        EffectiveDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("EffectiveDate");
                }
            }
            
            /// <summary>
            /// To indicate if this proces is commercially confidential
            /// </summary>
            [FhirElement("confidentialityIndicator", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ConfidentialityIndicator
            {
                get { return _ConfidentialityIndicator; }
                set { _ConfidentialityIndicator = value; OnPropertyChanged("ConfidentialityIndicator"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ConfidentialityIndicator;
            
            /// <summary>
            /// The manufacturer or establishment associated with the process
            /// </summary>
            [FhirElement("manufacturer", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
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
            /// A regulator which oversees the operation
            /// </summary>
            [FhirElement("regulator", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Regulator
            {
                get { return _Regulator; }
                set { _Regulator = value; OnPropertyChanged("Regulator"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Regulator;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ManufacturingBusinessOperationComponent");
                base.Serialize(sink);
                sink.Element("operationType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OperationType?.Serialize(sink);
                sink.Element("authorisationReferenceNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AuthorisationReferenceNumber?.Serialize(sink);
                sink.Element("effectiveDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EffectiveDateElement?.Serialize(sink);
                sink.Element("confidentialityIndicator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ConfidentialityIndicator?.Serialize(sink);
                sink.BeginList("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Manufacturer)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("regulator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Regulator?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "operationType":
                        OperationType = source.Populate(OperationType);
                        return true;
                    case "authorisationReferenceNumber":
                        AuthorisationReferenceNumber = source.Populate(AuthorisationReferenceNumber);
                        return true;
                    case "effectiveDate":
                        EffectiveDateElement = source.PopulateValue(EffectiveDateElement);
                        return true;
                    case "_effectiveDate":
                        EffectiveDateElement = source.Populate(EffectiveDateElement);
                        return true;
                    case "confidentialityIndicator":
                        ConfidentialityIndicator = source.Populate(ConfidentialityIndicator);
                        return true;
                    case "manufacturer":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "regulator":
                        Regulator = source.Populate(Regulator);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManufacturingBusinessOperationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(OperationType != null) dest.OperationType = (Hl7.Fhir.Model.CodeableConcept)OperationType.DeepCopy();
                    if(AuthorisationReferenceNumber != null) dest.AuthorisationReferenceNumber = (Hl7.Fhir.Model.Identifier)AuthorisationReferenceNumber.DeepCopy();
                    if(EffectiveDateElement != null) dest.EffectiveDateElement = (Hl7.Fhir.Model.FhirDateTime)EffectiveDateElement.DeepCopy();
                    if(ConfidentialityIndicator != null) dest.ConfidentialityIndicator = (Hl7.Fhir.Model.CodeableConcept)ConfidentialityIndicator.DeepCopy();
                    if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                    if(Regulator != null) dest.Regulator = (Hl7.Fhir.Model.ResourceReference)Regulator.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ManufacturingBusinessOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManufacturingBusinessOperationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(OperationType, otherT.OperationType)) return false;
                if( !DeepComparable.Matches(AuthorisationReferenceNumber, otherT.AuthorisationReferenceNumber)) return false;
                if( !DeepComparable.Matches(EffectiveDateElement, otherT.EffectiveDateElement)) return false;
                if( !DeepComparable.Matches(ConfidentialityIndicator, otherT.ConfidentialityIndicator)) return false;
                if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
                if( !DeepComparable.Matches(Regulator, otherT.Regulator)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManufacturingBusinessOperationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(OperationType, otherT.OperationType)) return false;
                if( !DeepComparable.IsExactly(AuthorisationReferenceNumber, otherT.AuthorisationReferenceNumber)) return false;
                if( !DeepComparable.IsExactly(EffectiveDateElement, otherT.EffectiveDateElement)) return false;
                if( !DeepComparable.IsExactly(ConfidentialityIndicator, otherT.ConfidentialityIndicator)) return false;
                if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
                if( !DeepComparable.IsExactly(Regulator, otherT.Regulator)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (OperationType != null) yield return OperationType;
                    if (AuthorisationReferenceNumber != null) yield return AuthorisationReferenceNumber;
                    if (EffectiveDateElement != null) yield return EffectiveDateElement;
                    if (ConfidentialityIndicator != null) yield return ConfidentialityIndicator;
                    foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                    if (Regulator != null) yield return Regulator;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (OperationType != null) yield return new ElementValue("operationType", OperationType);
                    if (AuthorisationReferenceNumber != null) yield return new ElementValue("authorisationReferenceNumber", AuthorisationReferenceNumber);
                    if (EffectiveDateElement != null) yield return new ElementValue("effectiveDate", EffectiveDateElement);
                    if (ConfidentialityIndicator != null) yield return new ElementValue("confidentialityIndicator", ConfidentialityIndicator);
                    foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                    if (Regulator != null) yield return new ElementValue("regulator", Regulator);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SpecialDesignationComponent")]
        [DataContract]
        public partial class SpecialDesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SpecialDesignationComponent"; } }
            
            /// <summary>
            /// Identifier for the designation, or procedure number
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
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
            /// The type of special designation, e.g. orphan drug, minor use
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The intended use of the product, e.g. prevention, treatment
            /// </summary>
            [FhirElement("intendedUse", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept IntendedUse
            {
                get { return _IntendedUse; }
                set { _IntendedUse = value; OnPropertyChanged("IntendedUse"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _IntendedUse;
            
            /// <summary>
            /// Condition for which the medicinal use applies
            /// </summary>
            [FhirElement("indication", InSummary=Hl7.Fhir.Model.Version.All, Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Indication
            {
                get { return _Indication; }
                set { _Indication = value; OnPropertyChanged("Indication"); }
            }
            
            private Hl7.Fhir.Model.Element _Indication;
            
            /// <summary>
            /// For example granted, pending, expired or withdrawn
            /// </summary>
            [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Status
            {
                get { return _Status; }
                set { _Status = value; OnPropertyChanged("Status"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Status;
            
            /// <summary>
            /// Date when the designation was granted
            /// </summary>
            [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// Date when the designation was granted
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
            /// Animal species for which this applies
            /// </summary>
            [FhirElement("species", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Species
            {
                get { return _Species; }
                set { _Species = value; OnPropertyChanged("Species"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Species;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SpecialDesignationComponent");
                base.Serialize(sink);
                sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Identifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
                sink.Element("intendedUse", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IntendedUse?.Serialize(sink);
                sink.Element("indication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Indication?.Serialize(sink);
                sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Status?.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
                sink.Element("species", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Species?.Serialize(sink);
                sink.End();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "intendedUse":
                        IntendedUse = source.Populate(IntendedUse);
                        return true;
                    case "indicationCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Indication, "indication");
                        Indication = source.Populate(Indication as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "indicationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Indication, "indication");
                        Indication = source.Populate(Indication as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "status":
                        Status = source.Populate(Status);
                        return true;
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "species":
                        Species = source.Populate(Species);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecialDesignationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(IntendedUse != null) dest.IntendedUse = (Hl7.Fhir.Model.CodeableConcept)IntendedUse.DeepCopy();
                    if(Indication != null) dest.Indication = (Hl7.Fhir.Model.Element)Indication.DeepCopy();
                    if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Species != null) dest.Species = (Hl7.Fhir.Model.CodeableConcept)Species.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SpecialDesignationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecialDesignationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(IntendedUse, otherT.IntendedUse)) return false;
                if( !DeepComparable.Matches(Indication, otherT.Indication)) return false;
                if( !DeepComparable.Matches(Status, otherT.Status)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Species, otherT.Species)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecialDesignationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(IntendedUse, otherT.IntendedUse)) return false;
                if( !DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
                if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Species, otherT.Species)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                    if (IntendedUse != null) yield return IntendedUse;
                    if (Indication != null) yield return Indication;
                    if (Status != null) yield return Status;
                    if (DateElement != null) yield return DateElement;
                    if (Species != null) yield return Species;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (IntendedUse != null) yield return new ElementValue("intendedUse", IntendedUse);
                    if (Indication != null) yield return new ElementValue("indication", Indication);
                    if (Status != null) yield return new ElementValue("status", Status);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Species != null) yield return new ElementValue("species", Species);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Business identifier for this product. Could be an MPID
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
        /// Regulatory type, e.g. Investigational or Authorized
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// If this medicine applies to human or veterinary uses
        /// </summary>
        [FhirElement("domain", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Domain
        {
            get { return _Domain; }
            set { _Domain = value; OnPropertyChanged("Domain"); }
        }
        
        private Hl7.Fhir.Model.Coding _Domain;
        
        /// <summary>
        /// The dose form for a single part product, or combined form of a multiple part product
        /// </summary>
        [FhirElement("combinedPharmaceuticalDoseForm", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept CombinedPharmaceuticalDoseForm
        {
            get { return _CombinedPharmaceuticalDoseForm; }
            set { _CombinedPharmaceuticalDoseForm = value; OnPropertyChanged("CombinedPharmaceuticalDoseForm"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _CombinedPharmaceuticalDoseForm;
        
        /// <summary>
        /// The legal status of supply of the medicinal product as classified by the regulator
        /// </summary>
        [FhirElement("legalStatusOfSupply", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept LegalStatusOfSupply
        {
            get { return _LegalStatusOfSupply; }
            set { _LegalStatusOfSupply = value; OnPropertyChanged("LegalStatusOfSupply"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _LegalStatusOfSupply;
        
        /// <summary>
        /// Whether the Medicinal Product is subject to additional monitoring for regulatory reasons
        /// </summary>
        [FhirElement("additionalMonitoringIndicator", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AdditionalMonitoringIndicator
        {
            get { return _AdditionalMonitoringIndicator; }
            set { _AdditionalMonitoringIndicator = value; OnPropertyChanged("AdditionalMonitoringIndicator"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AdditionalMonitoringIndicator;
        
        /// <summary>
        /// Whether the Medicinal Product is subject to special measures for regulatory reasons
        /// </summary>
        [FhirElement("specialMeasures", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> SpecialMeasuresElement
        {
            get { if(_SpecialMeasuresElement==null) _SpecialMeasuresElement = new List<Hl7.Fhir.Model.FhirString>(); return _SpecialMeasuresElement; }
            set { _SpecialMeasuresElement = value; OnPropertyChanged("SpecialMeasuresElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _SpecialMeasuresElement;
        
        /// <summary>
        /// Whether the Medicinal Product is subject to special measures for regulatory reasons
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> SpecialMeasures
        {
            get { return SpecialMeasuresElement != null ? SpecialMeasuresElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    SpecialMeasuresElement = null;
                else
                    SpecialMeasuresElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("SpecialMeasures");
            }
        }
        
        /// <summary>
        /// If authorised for use in children
        /// </summary>
        [FhirElement("paediatricUseIndicator", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PaediatricUseIndicator
        {
            get { return _PaediatricUseIndicator; }
            set { _PaediatricUseIndicator = value; OnPropertyChanged("PaediatricUseIndicator"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PaediatricUseIndicator;
        
        /// <summary>
        /// Allows the product to be classified by various systems
        /// </summary>
        [FhirElement("productClassification", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ProductClassification
        {
            get { if(_ProductClassification==null) _ProductClassification = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProductClassification; }
            set { _ProductClassification = value; OnPropertyChanged("ProductClassification"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ProductClassification;
        
        /// <summary>
        /// Marketing status of the medicinal product, in contrast to marketing authorizaton
        /// </summary>
        [FhirElement("marketingStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MarketingStatus> MarketingStatus
        {
            get { if(_MarketingStatus==null) _MarketingStatus = new List<Hl7.Fhir.Model.MarketingStatus>(); return _MarketingStatus; }
            set { _MarketingStatus = value; OnPropertyChanged("MarketingStatus"); }
        }
        
        private List<Hl7.Fhir.Model.MarketingStatus> _MarketingStatus;
        
        /// <summary>
        /// Pharmaceutical aspects of product
        /// </summary>
        [FhirElement("pharmaceuticalProduct", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("MedicinalProductPharmaceutical")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PharmaceuticalProduct
        {
            get { if(_PharmaceuticalProduct==null) _PharmaceuticalProduct = new List<Hl7.Fhir.Model.ResourceReference>(); return _PharmaceuticalProduct; }
            set { _PharmaceuticalProduct = value; OnPropertyChanged("PharmaceuticalProduct"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PharmaceuticalProduct;
        
        /// <summary>
        /// Package representation for the product
        /// </summary>
        [FhirElement("packagedMedicinalProduct", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [References("MedicinalProductPackaged")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PackagedMedicinalProduct
        {
            get { if(_PackagedMedicinalProduct==null) _PackagedMedicinalProduct = new List<Hl7.Fhir.Model.ResourceReference>(); return _PackagedMedicinalProduct; }
            set { _PackagedMedicinalProduct = value; OnPropertyChanged("PackagedMedicinalProduct"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PackagedMedicinalProduct;
        
        /// <summary>
        /// Supporting documentation, typically for regulatory submission
        /// </summary>
        [FhirElement("attachedDocument", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [References("DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> AttachedDocument
        {
            get { if(_AttachedDocument==null) _AttachedDocument = new List<Hl7.Fhir.Model.ResourceReference>(); return _AttachedDocument; }
            set { _AttachedDocument = value; OnPropertyChanged("AttachedDocument"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _AttachedDocument;
        
        /// <summary>
        /// A master file for to the medicinal product (e.g. Pharmacovigilance System Master File)
        /// </summary>
        [FhirElement("masterFile", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [References("DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> MasterFile
        {
            get { if(_MasterFile==null) _MasterFile = new List<Hl7.Fhir.Model.ResourceReference>(); return _MasterFile; }
            set { _MasterFile = value; OnPropertyChanged("MasterFile"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _MasterFile;
        
        /// <summary>
        /// A product specific contact, person (in a role), or an organization
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [References("Organization","PractitionerRole")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.ResourceReference>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Contact;
        
        /// <summary>
        /// Clinical trials or studies that this product is involved in
        /// </summary>
        [FhirElement("clinicalTrial", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("ResearchStudy")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ClinicalTrial
        {
            get { if(_ClinicalTrial==null) _ClinicalTrial = new List<Hl7.Fhir.Model.ResourceReference>(); return _ClinicalTrial; }
            set { _ClinicalTrial = value; OnPropertyChanged("ClinicalTrial"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ClinicalTrial;
        
        /// <summary>
        /// The product's name, including full name and possibly coded parts
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<NameComponent> Name
        {
            get { if(_Name==null) _Name = new List<NameComponent>(); return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        
        private List<NameComponent> _Name;
        
        /// <summary>
        /// Reference to another product, e.g. for linking authorised to investigational product
        /// </summary>
        [FhirElement("crossReference", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> CrossReference
        {
            get { if(_CrossReference==null) _CrossReference = new List<Hl7.Fhir.Model.Identifier>(); return _CrossReference; }
            set { _CrossReference = value; OnPropertyChanged("CrossReference"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _CrossReference;
        
        /// <summary>
        /// An operation applied to the product, for manufacturing or adminsitrative purpose
        /// </summary>
        [FhirElement("manufacturingBusinessOperation", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ManufacturingBusinessOperationComponent> ManufacturingBusinessOperation
        {
            get { if(_ManufacturingBusinessOperation==null) _ManufacturingBusinessOperation = new List<ManufacturingBusinessOperationComponent>(); return _ManufacturingBusinessOperation; }
            set { _ManufacturingBusinessOperation = value; OnPropertyChanged("ManufacturingBusinessOperation"); }
        }
        
        private List<ManufacturingBusinessOperationComponent> _ManufacturingBusinessOperation;
        
        /// <summary>
        /// Indicates if the medicinal product has an orphan designation for the treatment of a rare disease
        /// </summary>
        [FhirElement("specialDesignation", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SpecialDesignationComponent> SpecialDesignation
        {
            get { if(_SpecialDesignation==null) _SpecialDesignation = new List<SpecialDesignationComponent>(); return _SpecialDesignation; }
            set { _SpecialDesignation = value; OnPropertyChanged("SpecialDesignation"); }
        }
        
        private List<SpecialDesignationComponent> _SpecialDesignation;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProduct;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Domain != null) dest.Domain = (Hl7.Fhir.Model.Coding)Domain.DeepCopy();
                if(CombinedPharmaceuticalDoseForm != null) dest.CombinedPharmaceuticalDoseForm = (Hl7.Fhir.Model.CodeableConcept)CombinedPharmaceuticalDoseForm.DeepCopy();
                if(LegalStatusOfSupply != null) dest.LegalStatusOfSupply = (Hl7.Fhir.Model.CodeableConcept)LegalStatusOfSupply.DeepCopy();
                if(AdditionalMonitoringIndicator != null) dest.AdditionalMonitoringIndicator = (Hl7.Fhir.Model.CodeableConcept)AdditionalMonitoringIndicator.DeepCopy();
                if(SpecialMeasuresElement != null) dest.SpecialMeasuresElement = new List<Hl7.Fhir.Model.FhirString>(SpecialMeasuresElement.DeepCopy());
                if(PaediatricUseIndicator != null) dest.PaediatricUseIndicator = (Hl7.Fhir.Model.CodeableConcept)PaediatricUseIndicator.DeepCopy();
                if(ProductClassification != null) dest.ProductClassification = new List<Hl7.Fhir.Model.CodeableConcept>(ProductClassification.DeepCopy());
                if(MarketingStatus != null) dest.MarketingStatus = new List<Hl7.Fhir.Model.MarketingStatus>(MarketingStatus.DeepCopy());
                if(PharmaceuticalProduct != null) dest.PharmaceuticalProduct = new List<Hl7.Fhir.Model.ResourceReference>(PharmaceuticalProduct.DeepCopy());
                if(PackagedMedicinalProduct != null) dest.PackagedMedicinalProduct = new List<Hl7.Fhir.Model.ResourceReference>(PackagedMedicinalProduct.DeepCopy());
                if(AttachedDocument != null) dest.AttachedDocument = new List<Hl7.Fhir.Model.ResourceReference>(AttachedDocument.DeepCopy());
                if(MasterFile != null) dest.MasterFile = new List<Hl7.Fhir.Model.ResourceReference>(MasterFile.DeepCopy());
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.ResourceReference>(Contact.DeepCopy());
                if(ClinicalTrial != null) dest.ClinicalTrial = new List<Hl7.Fhir.Model.ResourceReference>(ClinicalTrial.DeepCopy());
                if(Name != null) dest.Name = new List<NameComponent>(Name.DeepCopy());
                if(CrossReference != null) dest.CrossReference = new List<Hl7.Fhir.Model.Identifier>(CrossReference.DeepCopy());
                if(ManufacturingBusinessOperation != null) dest.ManufacturingBusinessOperation = new List<ManufacturingBusinessOperationComponent>(ManufacturingBusinessOperation.DeepCopy());
                if(SpecialDesignation != null) dest.SpecialDesignation = new List<SpecialDesignationComponent>(SpecialDesignation.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicinalProduct());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProduct;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Domain, otherT.Domain)) return false;
            if( !DeepComparable.Matches(CombinedPharmaceuticalDoseForm, otherT.CombinedPharmaceuticalDoseForm)) return false;
            if( !DeepComparable.Matches(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
            if( !DeepComparable.Matches(AdditionalMonitoringIndicator, otherT.AdditionalMonitoringIndicator)) return false;
            if( !DeepComparable.Matches(SpecialMeasuresElement, otherT.SpecialMeasuresElement)) return false;
            if( !DeepComparable.Matches(PaediatricUseIndicator, otherT.PaediatricUseIndicator)) return false;
            if( !DeepComparable.Matches(ProductClassification, otherT.ProductClassification)) return false;
            if( !DeepComparable.Matches(MarketingStatus, otherT.MarketingStatus)) return false;
            if( !DeepComparable.Matches(PharmaceuticalProduct, otherT.PharmaceuticalProduct)) return false;
            if( !DeepComparable.Matches(PackagedMedicinalProduct, otherT.PackagedMedicinalProduct)) return false;
            if( !DeepComparable.Matches(AttachedDocument, otherT.AttachedDocument)) return false;
            if( !DeepComparable.Matches(MasterFile, otherT.MasterFile)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(ClinicalTrial, otherT.ClinicalTrial)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(CrossReference, otherT.CrossReference)) return false;
            if( !DeepComparable.Matches(ManufacturingBusinessOperation, otherT.ManufacturingBusinessOperation)) return false;
            if( !DeepComparable.Matches(SpecialDesignation, otherT.SpecialDesignation)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProduct;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Domain, otherT.Domain)) return false;
            if( !DeepComparable.IsExactly(CombinedPharmaceuticalDoseForm, otherT.CombinedPharmaceuticalDoseForm)) return false;
            if( !DeepComparable.IsExactly(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
            if( !DeepComparable.IsExactly(AdditionalMonitoringIndicator, otherT.AdditionalMonitoringIndicator)) return false;
            if( !DeepComparable.IsExactly(SpecialMeasuresElement, otherT.SpecialMeasuresElement)) return false;
            if( !DeepComparable.IsExactly(PaediatricUseIndicator, otherT.PaediatricUseIndicator)) return false;
            if( !DeepComparable.IsExactly(ProductClassification, otherT.ProductClassification)) return false;
            if( !DeepComparable.IsExactly(MarketingStatus, otherT.MarketingStatus)) return false;
            if( !DeepComparable.IsExactly(PharmaceuticalProduct, otherT.PharmaceuticalProduct)) return false;
            if( !DeepComparable.IsExactly(PackagedMedicinalProduct, otherT.PackagedMedicinalProduct)) return false;
            if( !DeepComparable.IsExactly(AttachedDocument, otherT.AttachedDocument)) return false;
            if( !DeepComparable.IsExactly(MasterFile, otherT.MasterFile)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(ClinicalTrial, otherT.ClinicalTrial)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(CrossReference, otherT.CrossReference)) return false;
            if( !DeepComparable.IsExactly(ManufacturingBusinessOperation, otherT.ManufacturingBusinessOperation)) return false;
            if( !DeepComparable.IsExactly(SpecialDesignation, otherT.SpecialDesignation)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicinalProduct");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("domain", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Domain?.Serialize(sink);
            sink.Element("combinedPharmaceuticalDoseForm", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CombinedPharmaceuticalDoseForm?.Serialize(sink);
            sink.Element("legalStatusOfSupply", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LegalStatusOfSupply?.Serialize(sink);
            sink.Element("additionalMonitoringIndicator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AdditionalMonitoringIndicator?.Serialize(sink);
            sink.BeginList("specialMeasures", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(SpecialMeasuresElement);
            sink.End();
            sink.Element("paediatricUseIndicator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PaediatricUseIndicator?.Serialize(sink);
            sink.BeginList("productClassification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ProductClassification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("marketingStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in MarketingStatus)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("pharmaceuticalProduct", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PharmaceuticalProduct)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("packagedMedicinalProduct", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PackagedMedicinalProduct)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("attachedDocument", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in AttachedDocument)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("masterFile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in MasterFile)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("clinicalTrial", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ClinicalTrial)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Name)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("crossReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in CrossReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("manufacturingBusinessOperation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ManufacturingBusinessOperation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("specialDesignation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in SpecialDesignation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
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
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "domain":
                    Domain = source.Populate(Domain);
                    return true;
                case "combinedPharmaceuticalDoseForm":
                    CombinedPharmaceuticalDoseForm = source.Populate(CombinedPharmaceuticalDoseForm);
                    return true;
                case "legalStatusOfSupply":
                    LegalStatusOfSupply = source.Populate(LegalStatusOfSupply);
                    return true;
                case "additionalMonitoringIndicator":
                    AdditionalMonitoringIndicator = source.Populate(AdditionalMonitoringIndicator);
                    return true;
                case "specialMeasures":
                case "_specialMeasures":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "paediatricUseIndicator":
                    PaediatricUseIndicator = source.Populate(PaediatricUseIndicator);
                    return true;
                case "productClassification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "marketingStatus":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "pharmaceuticalProduct":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "packagedMedicinalProduct":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "attachedDocument":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "masterFile":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "clinicalTrial":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "name":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "crossReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "manufacturingBusinessOperation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "specialDesignation":
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
                case "specialMeasures":
                    source.PopulatePrimitiveListItemValue(SpecialMeasuresElement, index);
                    return true;
                case "_specialMeasures":
                    source.PopulatePrimitiveListItem(SpecialMeasuresElement, index);
                    return true;
                case "productClassification":
                    source.PopulateListItem(ProductClassification, index);
                    return true;
                case "marketingStatus":
                    source.PopulateListItem(MarketingStatus, index);
                    return true;
                case "pharmaceuticalProduct":
                    source.PopulateListItem(PharmaceuticalProduct, index);
                    return true;
                case "packagedMedicinalProduct":
                    source.PopulateListItem(PackagedMedicinalProduct, index);
                    return true;
                case "attachedDocument":
                    source.PopulateListItem(AttachedDocument, index);
                    return true;
                case "masterFile":
                    source.PopulateListItem(MasterFile, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "clinicalTrial":
                    source.PopulateListItem(ClinicalTrial, index);
                    return true;
                case "name":
                    source.PopulateListItem(Name, index);
                    return true;
                case "crossReference":
                    source.PopulateListItem(CrossReference, index);
                    return true;
                case "manufacturingBusinessOperation":
                    source.PopulateListItem(ManufacturingBusinessOperation, index);
                    return true;
                case "specialDesignation":
                    source.PopulateListItem(SpecialDesignation, index);
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
                if (Type != null) yield return Type;
                if (Domain != null) yield return Domain;
                if (CombinedPharmaceuticalDoseForm != null) yield return CombinedPharmaceuticalDoseForm;
                if (LegalStatusOfSupply != null) yield return LegalStatusOfSupply;
                if (AdditionalMonitoringIndicator != null) yield return AdditionalMonitoringIndicator;
                foreach (var elem in SpecialMeasuresElement) { if (elem != null) yield return elem; }
                if (PaediatricUseIndicator != null) yield return PaediatricUseIndicator;
                foreach (var elem in ProductClassification) { if (elem != null) yield return elem; }
                foreach (var elem in MarketingStatus) { if (elem != null) yield return elem; }
                foreach (var elem in PharmaceuticalProduct) { if (elem != null) yield return elem; }
                foreach (var elem in PackagedMedicinalProduct) { if (elem != null) yield return elem; }
                foreach (var elem in AttachedDocument) { if (elem != null) yield return elem; }
                foreach (var elem in MasterFile) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                foreach (var elem in ClinicalTrial) { if (elem != null) yield return elem; }
                foreach (var elem in Name) { if (elem != null) yield return elem; }
                foreach (var elem in CrossReference) { if (elem != null) yield return elem; }
                foreach (var elem in ManufacturingBusinessOperation) { if (elem != null) yield return elem; }
                foreach (var elem in SpecialDesignation) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Type != null) yield return new ElementValue("type", Type);
                if (Domain != null) yield return new ElementValue("domain", Domain);
                if (CombinedPharmaceuticalDoseForm != null) yield return new ElementValue("combinedPharmaceuticalDoseForm", CombinedPharmaceuticalDoseForm);
                if (LegalStatusOfSupply != null) yield return new ElementValue("legalStatusOfSupply", LegalStatusOfSupply);
                if (AdditionalMonitoringIndicator != null) yield return new ElementValue("additionalMonitoringIndicator", AdditionalMonitoringIndicator);
                foreach (var elem in SpecialMeasuresElement) { if (elem != null) yield return new ElementValue("specialMeasures", elem); }
                if (PaediatricUseIndicator != null) yield return new ElementValue("paediatricUseIndicator", PaediatricUseIndicator);
                foreach (var elem in ProductClassification) { if (elem != null) yield return new ElementValue("productClassification", elem); }
                foreach (var elem in MarketingStatus) { if (elem != null) yield return new ElementValue("marketingStatus", elem); }
                foreach (var elem in PharmaceuticalProduct) { if (elem != null) yield return new ElementValue("pharmaceuticalProduct", elem); }
                foreach (var elem in PackagedMedicinalProduct) { if (elem != null) yield return new ElementValue("packagedMedicinalProduct", elem); }
                foreach (var elem in AttachedDocument) { if (elem != null) yield return new ElementValue("attachedDocument", elem); }
                foreach (var elem in MasterFile) { if (elem != null) yield return new ElementValue("masterFile", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in ClinicalTrial) { if (elem != null) yield return new ElementValue("clinicalTrial", elem); }
                foreach (var elem in Name) { if (elem != null) yield return new ElementValue("name", elem); }
                foreach (var elem in CrossReference) { if (elem != null) yield return new ElementValue("crossReference", elem); }
                foreach (var elem in ManufacturingBusinessOperation) { if (elem != null) yield return new ElementValue("manufacturingBusinessOperation", elem); }
                foreach (var elem in SpecialDesignation) { if (elem != null) yield return new ElementValue("specialDesignation", elem); }
            }
        }
    
    }

}
