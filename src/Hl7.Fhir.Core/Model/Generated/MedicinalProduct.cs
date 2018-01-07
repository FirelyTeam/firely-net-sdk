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
// Generated for FHIR v3.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Detailed definition of a medicinal product, typically for uses other than direct patient care (e.g. regulatory use)
    /// </summary>
    [FhirType("MedicinalProduct", IsResource=true)]
    [DataContract]
    public partial class MedicinalProduct : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProduct; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProduct"; } }
        
        [FhirType("NameComponent")]
        [DataContract]
        public partial class NameComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "NameComponent"; } }
            
            /// <summary>
            /// The full product name
            /// </summary>
            [FhirElement("fullName", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FullNameElement
            {
                get { return _FullNameElement; }
                set { _FullNameElement = value; OnPropertyChanged("FullNameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _FullNameElement;
            
            /// <summary>
            /// The full product name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string FullName
            {
                get { return FullNameElement != null ? FullNameElement.Value : null; }
                set
                {
                    if (value == null)
                        FullNameElement = null; 
                    else
                        FullNameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("FullName");
                }
            }
            
            /// <summary>
            /// Coding words or phrases of the name
            /// </summary>
            [FhirElement("namePart", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProduct.NamePartComponent> NamePart
            {
                get { if(_NamePart==null) _NamePart = new List<Hl7.Fhir.Model.MedicinalProduct.NamePartComponent>(); return _NamePart; }
                set { _NamePart = value; OnPropertyChanged("NamePart"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProduct.NamePartComponent> _NamePart;
            
            /// <summary>
            /// Country where the name applies
            /// </summary>
            [FhirElement("countryLanguage", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProduct.CountryLanguageComponent> CountryLanguage
            {
                get { if(_CountryLanguage==null) _CountryLanguage = new List<Hl7.Fhir.Model.MedicinalProduct.CountryLanguageComponent>(); return _CountryLanguage; }
                set { _CountryLanguage = value; OnPropertyChanged("CountryLanguage"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProduct.CountryLanguageComponent> _CountryLanguage;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NameComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FullNameElement != null) dest.FullNameElement = (Hl7.Fhir.Model.FhirString)FullNameElement.DeepCopy();
                    if(NamePart != null) dest.NamePart = new List<Hl7.Fhir.Model.MedicinalProduct.NamePartComponent>(NamePart.DeepCopy());
                    if(CountryLanguage != null) dest.CountryLanguage = new List<Hl7.Fhir.Model.MedicinalProduct.CountryLanguageComponent>(CountryLanguage.DeepCopy());
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
                if( !DeepComparable.Matches(FullNameElement, otherT.FullNameElement)) return false;
                if( !DeepComparable.Matches(NamePart, otherT.NamePart)) return false;
                if( !DeepComparable.Matches(CountryLanguage, otherT.CountryLanguage)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NameComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FullNameElement, otherT.FullNameElement)) return false;
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
                    if (FullNameElement != null) yield return FullNameElement;
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
                    if (FullNameElement != null) yield return new ElementValue("fullName", false, FullNameElement);
                    foreach (var elem in NamePart) { if (elem != null) yield return new ElementValue("namePart", true, elem); }
                    foreach (var elem in CountryLanguage) { if (elem != null) yield return new ElementValue("countryLanguage", true, elem); }
                }
            }

            
        }
        
        
        [FhirType("NamePartComponent")]
        [DataContract]
        public partial class NamePartComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "NamePartComponent"; } }
            
            /// <summary>
            /// A fragment of a product name
            /// </summary>
            [FhirElement("part", InSummary=true, Order=40)]
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
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
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
                    if (PartElement != null) yield return new ElementValue("part", false, PartElement);
                    if (Type != null) yield return new ElementValue("type", false, Type);
                }
            }

            
        }
        
        
        [FhirType("CountryLanguageComponent")]
        [DataContract]
        public partial class CountryLanguageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CountryLanguageComponent"; } }
            
            /// <summary>
            /// Country code for where this name applies
            /// </summary>
            [FhirElement("country", InSummary=true, Order=40)]
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
            [FhirElement("jurisdiction", InSummary=true, Order=50)]
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
            [FhirElement("language", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Language
            {
                get { return _Language; }
                set { _Language = value; OnPropertyChanged("Language"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Language;
            
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
                    if (Country != null) yield return new ElementValue("country", false, Country);
                    if (Jurisdiction != null) yield return new ElementValue("jurisdiction", false, Jurisdiction);
                    if (Language != null) yield return new ElementValue("language", false, Language);
                }
            }

            
        }
        
        
        [FhirType("ManufacturingBusinessOperationComponent")]
        [DataContract]
        public partial class ManufacturingBusinessOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ManufacturingBusinessOperationComponent"; } }
            
            /// <summary>
            /// The type of manufacturing operation
            /// </summary>
            [FhirElement("operationType", InSummary=true, Order=40)]
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
            [FhirElement("authorisationReferenceNumber", InSummary=true, Order=50)]
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
            [FhirElement("effectiveDate", InSummary=true, Order=60)]
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
            [FhirElement("confidentialityIndicator", InSummary=true, Order=70)]
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
            [FhirElement("manufacturer", InSummary=true, Order=80)]
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
            [FhirElement("regulator", InSummary=true, Order=90)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Regulator
            {
                get { return _Regulator; }
                set { _Regulator = value; OnPropertyChanged("Regulator"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Regulator;
            
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
                    if (OperationType != null) yield return new ElementValue("operationType", false, OperationType);
                    if (AuthorisationReferenceNumber != null) yield return new ElementValue("authorisationReferenceNumber", false, AuthorisationReferenceNumber);
                    if (EffectiveDateElement != null) yield return new ElementValue("effectiveDate", false, EffectiveDateElement);
                    if (ConfidentialityIndicator != null) yield return new ElementValue("confidentialityIndicator", false, ConfidentialityIndicator);
                    foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", true, elem); }
                    if (Regulator != null) yield return new ElementValue("regulator", false, Regulator);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business idenfifier for this product. Could be an MPID
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
        /// Regulatory type, e.g Investigational or Authorized
        /// </summary>
        [FhirElement("type", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// The dose form for a single part product, or combined form of a multiple part product
        /// </summary>
        [FhirElement("combinedPharmaceuticalDoseForm", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept CombinedPharmaceuticalDoseForm
        {
            get { return _CombinedPharmaceuticalDoseForm; }
            set { _CombinedPharmaceuticalDoseForm = value; OnPropertyChanged("CombinedPharmaceuticalDoseForm"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _CombinedPharmaceuticalDoseForm;
        
        /// <summary>
        /// Whether the Medicinal Product is subject to additional monitoring for regulatory reasons
        /// </summary>
        [FhirElement("additionalMonitoringIndicator", InSummary=true, Order=120)]
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
        [FhirElement("specialMeasures", InSummary=true, Order=130)]
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
        [FhirElement("paediatricUseIndicator", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PaediatricUseIndicator
        {
            get { return _PaediatricUseIndicator; }
            set { _PaediatricUseIndicator = value; OnPropertyChanged("PaediatricUseIndicator"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PaediatricUseIndicator;
        
        /// <summary>
        /// Indicates if the medicinal product has an orphan designation for the treatment of a rare disease
        /// </summary>
        [FhirElement("orphanDesignationStatus", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept OrphanDesignationStatus
        {
            get { return _OrphanDesignationStatus; }
            set { _OrphanDesignationStatus = value; OnPropertyChanged("OrphanDesignationStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _OrphanDesignationStatus;
        
        /// <summary>
        /// Allows the product to be classified by various systems
        /// </summary>
        [FhirElement("productClassification", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ProductClassification
        {
            get { if(_ProductClassification==null) _ProductClassification = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProductClassification; }
            set { _ProductClassification = value; OnPropertyChanged("ProductClassification"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ProductClassification;
        
        /// <summary>
        /// Product regulatory authorization
        /// </summary>
        [FhirElement("marketingAuthorization", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("MedicinalProductAuthorization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference MarketingAuthorization
        {
            get { return _MarketingAuthorization; }
            set { _MarketingAuthorization = value; OnPropertyChanged("MarketingAuthorization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _MarketingAuthorization;
        
        /// <summary>
        /// Package representation for the product
        /// </summary>
        [FhirElement("packagedMedicinalProduct", InSummary=true, Order=180)]
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
        /// Pharmaceutical aspects of product
        /// </summary>
        [FhirElement("pharmaceuticalProduct", InSummary=true, Order=190)]
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
        /// Clinical particulars, indications etc
        /// </summary>
        [FhirElement("clinicalParticulars", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References("MedicinalProductClinicals")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ClinicalParticulars
        {
            get { if(_ClinicalParticulars==null) _ClinicalParticulars = new List<Hl7.Fhir.Model.ResourceReference>(); return _ClinicalParticulars; }
            set { _ClinicalParticulars = value; OnPropertyChanged("ClinicalParticulars"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ClinicalParticulars;
        
        /// <summary>
        /// Supporting documentation, typically for regulatory submission
        /// </summary>
        [FhirElement("attachedDocument", InSummary=true, Order=210)]
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
        [FhirElement("masterFile", InSummary=true, Order=220)]
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
        /// The product's name, including full name and possibly coded parts
        /// </summary>
        [FhirElement("name", InSummary=true, Order=230)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProduct.NameComponent> Name
        {
            get { if(_Name==null) _Name = new List<Hl7.Fhir.Model.MedicinalProduct.NameComponent>(); return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProduct.NameComponent> _Name;
        
        /// <summary>
        /// Reference to another product, e.g. for linking authorised to investigational product
        /// </summary>
        [FhirElement("crossReference", InSummary=true, Order=240)]
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
        [FhirElement("manufacturingBusinessOperation", InSummary=true, Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProduct.ManufacturingBusinessOperationComponent> ManufacturingBusinessOperation
        {
            get { if(_ManufacturingBusinessOperation==null) _ManufacturingBusinessOperation = new List<Hl7.Fhir.Model.MedicinalProduct.ManufacturingBusinessOperationComponent>(); return _ManufacturingBusinessOperation; }
            set { _ManufacturingBusinessOperation = value; OnPropertyChanged("ManufacturingBusinessOperation"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProduct.ManufacturingBusinessOperationComponent> _ManufacturingBusinessOperation;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProduct;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(CombinedPharmaceuticalDoseForm != null) dest.CombinedPharmaceuticalDoseForm = (Hl7.Fhir.Model.CodeableConcept)CombinedPharmaceuticalDoseForm.DeepCopy();
                if(AdditionalMonitoringIndicator != null) dest.AdditionalMonitoringIndicator = (Hl7.Fhir.Model.CodeableConcept)AdditionalMonitoringIndicator.DeepCopy();
                if(SpecialMeasuresElement != null) dest.SpecialMeasuresElement = new List<Hl7.Fhir.Model.FhirString>(SpecialMeasuresElement.DeepCopy());
                if(PaediatricUseIndicator != null) dest.PaediatricUseIndicator = (Hl7.Fhir.Model.CodeableConcept)PaediatricUseIndicator.DeepCopy();
                if(OrphanDesignationStatus != null) dest.OrphanDesignationStatus = (Hl7.Fhir.Model.CodeableConcept)OrphanDesignationStatus.DeepCopy();
                if(ProductClassification != null) dest.ProductClassification = new List<Hl7.Fhir.Model.CodeableConcept>(ProductClassification.DeepCopy());
                if(MarketingAuthorization != null) dest.MarketingAuthorization = (Hl7.Fhir.Model.ResourceReference)MarketingAuthorization.DeepCopy();
                if(PackagedMedicinalProduct != null) dest.PackagedMedicinalProduct = new List<Hl7.Fhir.Model.ResourceReference>(PackagedMedicinalProduct.DeepCopy());
                if(PharmaceuticalProduct != null) dest.PharmaceuticalProduct = new List<Hl7.Fhir.Model.ResourceReference>(PharmaceuticalProduct.DeepCopy());
                if(ClinicalParticulars != null) dest.ClinicalParticulars = new List<Hl7.Fhir.Model.ResourceReference>(ClinicalParticulars.DeepCopy());
                if(AttachedDocument != null) dest.AttachedDocument = new List<Hl7.Fhir.Model.ResourceReference>(AttachedDocument.DeepCopy());
                if(MasterFile != null) dest.MasterFile = new List<Hl7.Fhir.Model.ResourceReference>(MasterFile.DeepCopy());
                if(Name != null) dest.Name = new List<Hl7.Fhir.Model.MedicinalProduct.NameComponent>(Name.DeepCopy());
                if(CrossReference != null) dest.CrossReference = new List<Hl7.Fhir.Model.Identifier>(CrossReference.DeepCopy());
                if(ManufacturingBusinessOperation != null) dest.ManufacturingBusinessOperation = new List<Hl7.Fhir.Model.MedicinalProduct.ManufacturingBusinessOperationComponent>(ManufacturingBusinessOperation.DeepCopy());
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
            if( !DeepComparable.Matches(CombinedPharmaceuticalDoseForm, otherT.CombinedPharmaceuticalDoseForm)) return false;
            if( !DeepComparable.Matches(AdditionalMonitoringIndicator, otherT.AdditionalMonitoringIndicator)) return false;
            if( !DeepComparable.Matches(SpecialMeasuresElement, otherT.SpecialMeasuresElement)) return false;
            if( !DeepComparable.Matches(PaediatricUseIndicator, otherT.PaediatricUseIndicator)) return false;
            if( !DeepComparable.Matches(OrphanDesignationStatus, otherT.OrphanDesignationStatus)) return false;
            if( !DeepComparable.Matches(ProductClassification, otherT.ProductClassification)) return false;
            if( !DeepComparable.Matches(MarketingAuthorization, otherT.MarketingAuthorization)) return false;
            if( !DeepComparable.Matches(PackagedMedicinalProduct, otherT.PackagedMedicinalProduct)) return false;
            if( !DeepComparable.Matches(PharmaceuticalProduct, otherT.PharmaceuticalProduct)) return false;
            if( !DeepComparable.Matches(ClinicalParticulars, otherT.ClinicalParticulars)) return false;
            if( !DeepComparable.Matches(AttachedDocument, otherT.AttachedDocument)) return false;
            if( !DeepComparable.Matches(MasterFile, otherT.MasterFile)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(CrossReference, otherT.CrossReference)) return false;
            if( !DeepComparable.Matches(ManufacturingBusinessOperation, otherT.ManufacturingBusinessOperation)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProduct;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(CombinedPharmaceuticalDoseForm, otherT.CombinedPharmaceuticalDoseForm)) return false;
            if( !DeepComparable.IsExactly(AdditionalMonitoringIndicator, otherT.AdditionalMonitoringIndicator)) return false;
            if( !DeepComparable.IsExactly(SpecialMeasuresElement, otherT.SpecialMeasuresElement)) return false;
            if( !DeepComparable.IsExactly(PaediatricUseIndicator, otherT.PaediatricUseIndicator)) return false;
            if( !DeepComparable.IsExactly(OrphanDesignationStatus, otherT.OrphanDesignationStatus)) return false;
            if( !DeepComparable.IsExactly(ProductClassification, otherT.ProductClassification)) return false;
            if( !DeepComparable.IsExactly(MarketingAuthorization, otherT.MarketingAuthorization)) return false;
            if( !DeepComparable.IsExactly(PackagedMedicinalProduct, otherT.PackagedMedicinalProduct)) return false;
            if( !DeepComparable.IsExactly(PharmaceuticalProduct, otherT.PharmaceuticalProduct)) return false;
            if( !DeepComparable.IsExactly(ClinicalParticulars, otherT.ClinicalParticulars)) return false;
            if( !DeepComparable.IsExactly(AttachedDocument, otherT.AttachedDocument)) return false;
            if( !DeepComparable.IsExactly(MasterFile, otherT.MasterFile)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(CrossReference, otherT.CrossReference)) return false;
            if( !DeepComparable.IsExactly(ManufacturingBusinessOperation, otherT.ManufacturingBusinessOperation)) return false;
            
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
				if (CombinedPharmaceuticalDoseForm != null) yield return CombinedPharmaceuticalDoseForm;
				if (AdditionalMonitoringIndicator != null) yield return AdditionalMonitoringIndicator;
				foreach (var elem in SpecialMeasuresElement) { if (elem != null) yield return elem; }
				if (PaediatricUseIndicator != null) yield return PaediatricUseIndicator;
				if (OrphanDesignationStatus != null) yield return OrphanDesignationStatus;
				foreach (var elem in ProductClassification) { if (elem != null) yield return elem; }
				if (MarketingAuthorization != null) yield return MarketingAuthorization;
				foreach (var elem in PackagedMedicinalProduct) { if (elem != null) yield return elem; }
				foreach (var elem in PharmaceuticalProduct) { if (elem != null) yield return elem; }
				foreach (var elem in ClinicalParticulars) { if (elem != null) yield return elem; }
				foreach (var elem in AttachedDocument) { if (elem != null) yield return elem; }
				foreach (var elem in MasterFile) { if (elem != null) yield return elem; }
				foreach (var elem in Name) { if (elem != null) yield return elem; }
				foreach (var elem in CrossReference) { if (elem != null) yield return elem; }
				foreach (var elem in ManufacturingBusinessOperation) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (Type != null) yield return new ElementValue("type", false, Type);
                if (CombinedPharmaceuticalDoseForm != null) yield return new ElementValue("combinedPharmaceuticalDoseForm", false, CombinedPharmaceuticalDoseForm);
                if (AdditionalMonitoringIndicator != null) yield return new ElementValue("additionalMonitoringIndicator", false, AdditionalMonitoringIndicator);
                foreach (var elem in SpecialMeasuresElement) { if (elem != null) yield return new ElementValue("specialMeasures", true, elem); }
                if (PaediatricUseIndicator != null) yield return new ElementValue("paediatricUseIndicator", false, PaediatricUseIndicator);
                if (OrphanDesignationStatus != null) yield return new ElementValue("orphanDesignationStatus", false, OrphanDesignationStatus);
                foreach (var elem in ProductClassification) { if (elem != null) yield return new ElementValue("productClassification", true, elem); }
                if (MarketingAuthorization != null) yield return new ElementValue("marketingAuthorization", false, MarketingAuthorization);
                foreach (var elem in PackagedMedicinalProduct) { if (elem != null) yield return new ElementValue("packagedMedicinalProduct", true, elem); }
                foreach (var elem in PharmaceuticalProduct) { if (elem != null) yield return new ElementValue("pharmaceuticalProduct", true, elem); }
                foreach (var elem in ClinicalParticulars) { if (elem != null) yield return new ElementValue("clinicalParticulars", true, elem); }
                foreach (var elem in AttachedDocument) { if (elem != null) yield return new ElementValue("attachedDocument", true, elem); }
                foreach (var elem in MasterFile) { if (elem != null) yield return new ElementValue("masterFile", true, elem); }
                foreach (var elem in Name) { if (elem != null) yield return new ElementValue("name", true, elem); }
                foreach (var elem in CrossReference) { if (elem != null) yield return new ElementValue("crossReference", true, elem); }
                foreach (var elem in ManufacturingBusinessOperation) { if (elem != null) yield return new ElementValue("manufacturingBusinessOperation", true, elem); }
            }
        }

    }
    
}
