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
    /// An instance of a medical-related component of a medical device
    /// </summary>
    [FhirType("DeviceComponent", IsResource=true)]
    [DataContract]
    public partial class DeviceComponent : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceComponent; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceComponent"; } }
        
        /// <summary>
        /// Different measurement principle supported by the device.
        /// (url: http://hl7.org/fhir/ValueSet/measurement-principle)
        /// </summary>
        [FhirEnumeration("MeasmntPrinciple")]
        public enum MeasmntPrinciple
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("other", "http://hl7.org/fhir/measurement-principle"), Description("MSP Other")]
            Other,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("chemical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Chemical")]
            Chemical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("electrical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Electrical")]
            Electrical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("impedance", "http://hl7.org/fhir/measurement-principle"), Description("MSP Impedance")]
            Impedance,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("nuclear", "http://hl7.org/fhir/measurement-principle"), Description("MSP Nuclear")]
            Nuclear,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("optical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Optical")]
            Optical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("thermal", "http://hl7.org/fhir/measurement-principle"), Description("MSP Thermal")]
            Thermal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("biological", "http://hl7.org/fhir/measurement-principle"), Description("MSP Biological")]
            Biological,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("mechanical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Mechanical")]
            Mechanical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("acoustical", "http://hl7.org/fhir/measurement-principle"), Description("MSP Acoustical")]
            Acoustical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measurement-principle)
            /// </summary>
            [EnumLiteral("manual", "http://hl7.org/fhir/measurement-principle"), Description("MSP Manual")]
            Manual,
        }

        [FhirType("ProductionSpecificationComponent")]
        [DataContract]
        public partial class ProductionSpecificationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ProductionSpecificationComponent"; } }
            
            /// <summary>
            /// Type or kind of production specification, for example serial number or software revision
            /// </summary>
            [FhirElement("specType", InSummary=true, Order=40)]
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
            [FhirElement("componentId", InSummary=true, Order=50)]
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
            [FhirElement("productionSpec", InSummary=true, Order=60)]
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
        
        
        /// <summary>
        /// Instance id assigned by the software stack
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
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
        [FhirElement("type", InSummary=true, Order=100)]
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
        [FhirElement("lastSystemChange", InSummary=true, Order=110)]
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
                if (!value.HasValue)
                  LastSystemChangeElement = null; 
                else
                  LastSystemChangeElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("LastSystemChange");
            }
        }
        
        /// <summary>
        /// Top-level device resource link
        /// </summary>
        [FhirElement("source", InSummary=true, Order=120)]
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
        [FhirElement("parent", InSummary=true, Order=130)]
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
        [FhirElement("operationalStatus", InSummary=true, Order=140)]
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
        [FhirElement("parameterGroup", InSummary=true, Order=150)]
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
        [FhirElement("measurementPrinciple", InSummary=true, Order=160)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DeviceComponent.MeasmntPrinciple> MeasurementPrincipleElement
        {
            get { return _MeasurementPrincipleElement; }
            set { _MeasurementPrincipleElement = value; OnPropertyChanged("MeasurementPrincipleElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DeviceComponent.MeasmntPrinciple> _MeasurementPrincipleElement;
        
        /// <summary>
        /// other | chemical | electrical | impedance | nuclear | optical | thermal | biological | mechanical | acoustical | manual+
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DeviceComponent.MeasmntPrinciple? MeasurementPrinciple
        {
            get { return MeasurementPrincipleElement != null ? MeasurementPrincipleElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  MeasurementPrincipleElement = null; 
                else
                  MeasurementPrincipleElement = new Code<Hl7.Fhir.Model.DeviceComponent.MeasmntPrinciple>(value);
                OnPropertyChanged("MeasurementPrinciple");
            }
        }
        
        /// <summary>
        /// Specification details such as Component Revisions, or Serial Numbers
        /// </summary>
        [FhirElement("productionSpecification", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DeviceComponent.ProductionSpecificationComponent> ProductionSpecification
        {
            get { if(_ProductionSpecification==null) _ProductionSpecification = new List<Hl7.Fhir.Model.DeviceComponent.ProductionSpecificationComponent>(); return _ProductionSpecification; }
            set { _ProductionSpecification = value; OnPropertyChanged("ProductionSpecification"); }
        }
        
        private List<Hl7.Fhir.Model.DeviceComponent.ProductionSpecificationComponent> _ProductionSpecification;
        
        /// <summary>
        /// Language code for the human-readable text strings produced by the device
        /// </summary>
        [FhirElement("languageCode", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; OnPropertyChanged("LanguageCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _LanguageCode;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

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
                if(MeasurementPrincipleElement != null) dest.MeasurementPrincipleElement = (Code<Hl7.Fhir.Model.DeviceComponent.MeasmntPrinciple>)MeasurementPrincipleElement.DeepCopy();
                if(ProductionSpecification != null) dest.ProductionSpecification = new List<Hl7.Fhir.Model.DeviceComponent.ProductionSpecificationComponent>(ProductionSpecification.DeepCopy());
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
