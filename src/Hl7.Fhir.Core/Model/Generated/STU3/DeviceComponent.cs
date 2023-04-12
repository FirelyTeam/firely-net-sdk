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
    /// An instance of a medical-related component of a medical device
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "DeviceComponent", IsResource=true)]
    [DataContract]
    public partial class DeviceComponent : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IDeviceComponent, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceComponent; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceComponent"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ProductionSpecificationComponent")]
        [DataContract]
        public partial class ProductionSpecificationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IDeviceComponentProductionSpecificationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ProductionSpecificationComponent"; } }
            
            /// <summary>
            /// Type or kind of production specification, for example serial number or software revision
            /// </summary>
            [FhirElement("specType", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SpecType
            {
                get { return _SpecType; }
                set { _SpecType = value; OnPropertyChanged("SpecType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SpecType;
            
            /// <summary>
            /// Internal component unique identification
            /// </summary>
            [FhirElement("componentId", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ComponentId
            {
                get { return _ComponentId; }
                set { _ComponentId = value; OnPropertyChanged("ComponentId"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ComponentId;
            
            /// <summary>
            /// A printable string defining the component
            /// </summary>
            [FhirElement("productionSpec", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ProductionSpecElement
            {
                get { return _ProductionSpecElement; }
                set { _ProductionSpecElement = value; OnPropertyChanged("ProductionSpecElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ProductionSpecElement;
            
            /// <summary>
            /// A printable string defining the component
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ProductionSpec
            {
                get { return ProductionSpecElement != null ? ProductionSpecElement.Value : null; }
                set
                {
                    if (value == null)
                        ProductionSpecElement = null;
                    else
                        ProductionSpecElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ProductionSpec");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ProductionSpecificationComponent");
                base.Serialize(sink);
                sink.Element("specType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SpecType?.Serialize(sink);
                sink.Element("componentId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ComponentId?.Serialize(sink);
                sink.Element("productionSpec", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ProductionSpecElement?.Serialize(sink);
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
                    case "specType":
                        SpecType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "componentId":
                        ComponentId = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "productionSpec":
                        ProductionSpecElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "specType":
                        SpecType = source.Populate(SpecType);
                        return true;
                    case "componentId":
                        ComponentId = source.Populate(ComponentId);
                        return true;
                    case "productionSpec":
                        ProductionSpecElement = source.PopulateValue(ProductionSpecElement);
                        return true;
                    case "_productionSpec":
                        ProductionSpecElement = source.Populate(ProductionSpecElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProductionSpecificationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SpecType != null) dest.SpecType = (Hl7.Fhir.Model.CodeableConcept)SpecType.DeepCopy();
                    if(ComponentId != null) dest.ComponentId = (Hl7.Fhir.Model.Identifier)ComponentId.DeepCopy();
                    if(ProductionSpecElement != null) dest.ProductionSpecElement = (Hl7.Fhir.Model.FhirString)ProductionSpecElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ProductionSpecificationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProductionSpecificationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SpecType, otherT.SpecType)) return false;
                if( !DeepComparable.Matches(ComponentId, otherT.ComponentId)) return false;
                if( !DeepComparable.Matches(ProductionSpecElement, otherT.ProductionSpecElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProductionSpecificationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SpecType, otherT.SpecType)) return false;
                if( !DeepComparable.IsExactly(ComponentId, otherT.ComponentId)) return false;
                if( !DeepComparable.IsExactly(ProductionSpecElement, otherT.ProductionSpecElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SpecType != null) yield return SpecType;
                    if (ComponentId != null) yield return ComponentId;
                    if (ProductionSpecElement != null) yield return ProductionSpecElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SpecType != null) yield return new ElementValue("specType", SpecType);
                    if (ComponentId != null) yield return new ElementValue("componentId", ComponentId);
                    if (ProductionSpecElement != null) yield return new ElementValue("productionSpec", ProductionSpecElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDeviceComponentProductionSpecificationComponent> Hl7.Fhir.Model.IDeviceComponent.ProductionSpecification { get { return ProductionSpecification; } }
    
        
        /// <summary>
        /// Instance id assigned by the software stack
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// What kind of component it is
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Recent system change timestamp
        /// </summary>
        [FhirElement("lastSystemChange", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Instant LastSystemChangeElement
        {
            get { return _LastSystemChangeElement; }
            set { _LastSystemChangeElement = value; OnPropertyChanged("LastSystemChangeElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _LastSystemChangeElement;
        
        /// <summary>
        /// Recent system change timestamp
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? LastSystemChange
        {
            get { return LastSystemChangeElement != null ? LastSystemChangeElement.Value : null; }
            set
            {
                if (value == null)
                    LastSystemChangeElement = null;
                else
                    LastSystemChangeElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("LastSystemChange");
            }
        }
        
        /// <summary>
        /// Top-level device resource link
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// Parent resource link
        /// </summary>
        [FhirElement("parent", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("DeviceComponent")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Parent
        {
            get { return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Parent;
        
        /// <summary>
        /// Current operational status of the component, for example On, Off or Standby
        /// </summary>
        [FhirElement("operationalStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> OperationalStatus
        {
            get { if(_OperationalStatus==null) _OperationalStatus = new List<Hl7.Fhir.Model.CodeableConcept>(); return _OperationalStatus; }
            set { _OperationalStatus = value; OnPropertyChanged("OperationalStatus"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _OperationalStatus;
        
        /// <summary>
        /// Current supported parameter group
        /// </summary>
        [FhirElement("parameterGroup", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ParameterGroup
        {
            get { return _ParameterGroup; }
            set { _ParameterGroup = value; OnPropertyChanged("ParameterGroup"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ParameterGroup;
        
        /// <summary>
        /// other | chemical | electrical | impedance | nuclear | optical | thermal | biological | mechanical | acoustical | manual+
        /// </summary>
        [FhirElement("measurementPrinciple", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.MeasmntPrinciple> MeasurementPrincipleElement
        {
            get { return _MeasurementPrincipleElement; }
            set { _MeasurementPrincipleElement = value; OnPropertyChanged("MeasurementPrincipleElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.MeasmntPrinciple> _MeasurementPrincipleElement;
        
        /// <summary>
        /// other | chemical | electrical | impedance | nuclear | optical | thermal | biological | mechanical | acoustical | manual+
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.MeasmntPrinciple? MeasurementPrinciple
        {
            get { return MeasurementPrincipleElement != null ? MeasurementPrincipleElement.Value : null; }
            set
            {
                if (value == null)
                    MeasurementPrincipleElement = null;
                else
                    MeasurementPrincipleElement = new Code<Hl7.Fhir.Model.STU3.MeasmntPrinciple>(value);
                OnPropertyChanged("MeasurementPrinciple");
            }
        }
        
        /// <summary>
        /// Specification details such as Component Revisions, or Serial Numbers
        /// </summary>
        [FhirElement("productionSpecification", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ProductionSpecificationComponent> ProductionSpecification
        {
            get { if(_ProductionSpecification==null) _ProductionSpecification = new List<ProductionSpecificationComponent>(); return _ProductionSpecification; }
            set { _ProductionSpecification = value; OnPropertyChanged("ProductionSpecification"); }
        }
        
        private List<ProductionSpecificationComponent> _ProductionSpecification;
        
        /// <summary>
        /// Language code for the human-readable text strings produced by the device
        /// </summary>
        [FhirElement("languageCode", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; OnPropertyChanged("LanguageCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _LanguageCode;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DeviceComponent;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(LastSystemChangeElement != null) dest.LastSystemChangeElement = (Hl7.Fhir.Model.Instant)LastSystemChangeElement.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(Parent != null) dest.Parent = (Hl7.Fhir.Model.ResourceReference)Parent.DeepCopy();
                if(OperationalStatus != null) dest.OperationalStatus = new List<Hl7.Fhir.Model.CodeableConcept>(OperationalStatus.DeepCopy());
                if(ParameterGroup != null) dest.ParameterGroup = (Hl7.Fhir.Model.CodeableConcept)ParameterGroup.DeepCopy();
                if(MeasurementPrincipleElement != null) dest.MeasurementPrincipleElement = (Code<Hl7.Fhir.Model.STU3.MeasmntPrinciple>)MeasurementPrincipleElement.DeepCopy();
                if(ProductionSpecification != null) dest.ProductionSpecification = new List<ProductionSpecificationComponent>(ProductionSpecification.DeepCopy());
                if(LanguageCode != null) dest.LanguageCode = (Hl7.Fhir.Model.CodeableConcept)LanguageCode.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DeviceComponent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DeviceComponent;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(LastSystemChangeElement, otherT.LastSystemChangeElement)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(OperationalStatus, otherT.OperationalStatus)) return false;
            if( !DeepComparable.Matches(ParameterGroup, otherT.ParameterGroup)) return false;
            if( !DeepComparable.Matches(MeasurementPrincipleElement, otherT.MeasurementPrincipleElement)) return false;
            if( !DeepComparable.Matches(ProductionSpecification, otherT.ProductionSpecification)) return false;
            if( !DeepComparable.Matches(LanguageCode, otherT.LanguageCode)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DeviceComponent;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(LastSystemChangeElement, otherT.LastSystemChangeElement)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(OperationalStatus, otherT.OperationalStatus)) return false;
            if( !DeepComparable.IsExactly(ParameterGroup, otherT.ParameterGroup)) return false;
            if( !DeepComparable.IsExactly(MeasurementPrincipleElement, otherT.MeasurementPrincipleElement)) return false;
            if( !DeepComparable.IsExactly(ProductionSpecification, otherT.ProductionSpecification)) return false;
            if( !DeepComparable.IsExactly(LanguageCode, otherT.LanguageCode)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("DeviceComponent");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Identifier?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
            sink.Element("lastSystemChange", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LastSystemChangeElement?.Serialize(sink);
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Source?.Serialize(sink);
            sink.Element("parent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Parent?.Serialize(sink);
            sink.BeginList("operationalStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in OperationalStatus)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("parameterGroup", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ParameterGroup?.Serialize(sink);
            sink.Element("measurementPrinciple", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeasurementPrincipleElement?.Serialize(sink);
            sink.BeginList("productionSpecification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ProductionSpecification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("languageCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LanguageCode?.Serialize(sink);
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
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "lastSystemChange":
                    LastSystemChangeElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "source":
                    Source = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "parent":
                    Parent = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "operationalStatus":
                    OperationalStatus = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "parameterGroup":
                    ParameterGroup = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "measurementPrinciple":
                    MeasurementPrincipleElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MeasmntPrinciple>>();
                    return true;
                case "productionSpecification":
                    ProductionSpecification = source.GetList<ProductionSpecificationComponent>();
                    return true;
                case "languageCode":
                    LanguageCode = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "lastSystemChange":
                    LastSystemChangeElement = source.PopulateValue(LastSystemChangeElement);
                    return true;
                case "_lastSystemChange":
                    LastSystemChangeElement = source.Populate(LastSystemChangeElement);
                    return true;
                case "source":
                    Source = source.Populate(Source);
                    return true;
                case "parent":
                    Parent = source.Populate(Parent);
                    return true;
                case "operationalStatus":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "parameterGroup":
                    ParameterGroup = source.Populate(ParameterGroup);
                    return true;
                case "measurementPrinciple":
                    MeasurementPrincipleElement = source.PopulateValue(MeasurementPrincipleElement);
                    return true;
                case "_measurementPrinciple":
                    MeasurementPrincipleElement = source.Populate(MeasurementPrincipleElement);
                    return true;
                case "productionSpecification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "languageCode":
                    LanguageCode = source.Populate(LanguageCode);
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
                case "operationalStatus":
                    source.PopulateListItem(OperationalStatus, index);
                    return true;
                case "productionSpecification":
                    source.PopulateListItem(ProductionSpecification, index);
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
                if (Type != null) yield return Type;
                if (LastSystemChangeElement != null) yield return LastSystemChangeElement;
                if (Source != null) yield return Source;
                if (Parent != null) yield return Parent;
                foreach (var elem in OperationalStatus) { if (elem != null) yield return elem; }
                if (ParameterGroup != null) yield return ParameterGroup;
                if (MeasurementPrincipleElement != null) yield return MeasurementPrincipleElement;
                foreach (var elem in ProductionSpecification) { if (elem != null) yield return elem; }
                if (LanguageCode != null) yield return LanguageCode;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (Type != null) yield return new ElementValue("type", Type);
                if (LastSystemChangeElement != null) yield return new ElementValue("lastSystemChange", LastSystemChangeElement);
                if (Source != null) yield return new ElementValue("source", Source);
                if (Parent != null) yield return new ElementValue("parent", Parent);
                foreach (var elem in OperationalStatus) { if (elem != null) yield return new ElementValue("operationalStatus", elem); }
                if (ParameterGroup != null) yield return new ElementValue("parameterGroup", ParameterGroup);
                if (MeasurementPrincipleElement != null) yield return new ElementValue("measurementPrinciple", MeasurementPrincipleElement);
                foreach (var elem in ProductionSpecification) { if (elem != null) yield return new ElementValue("productionSpecification", elem); }
                if (LanguageCode != null) yield return new ElementValue("languageCode", LanguageCode);
            }
        }
    
    }

}
