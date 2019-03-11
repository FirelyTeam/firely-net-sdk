﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// CoverageEligibilityResponse resource
    /// </summary>
    [FhirType("CoverageEligibilityResponse", IsResource=true)]
    [DataContract]
    public partial class CoverageEligibilityResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CoverageEligibilityResponse; } }
        [NotMapped]
        public override string TypeName { get { return "CoverageEligibilityResponse"; } }
        
        /// <summary>
        /// A code specifying the types of information being requested.
        /// (url: http://hl7.org/fhir/ValueSet/eligibilityresponse-purpose)
        /// </summary>
        [FhirEnumeration("EligibilityResponsePurpose")]
        public enum EligibilityResponsePurpose
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
            /// </summary>
            [EnumLiteral("auth-requirements", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage auth-requirements")]
            AuthRequirements,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
            /// </summary>
            [EnumLiteral("benefits", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage benefits")]
            Benefits,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
            /// </summary>
            [EnumLiteral("discovery", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage Discovery")]
            Discovery,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityresponse-purpose)
            /// </summary>
            [EnumLiteral("validation", "http://hl7.org/fhir/eligibilityresponse-purpose"), Description("Coverage Validation")]
            Validation,
        }

        [FhirType("InsuranceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class InsuranceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InsuranceComponent"; } }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("Coverage")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Coverage;
            
            /// <summary>
            /// Coverage inforce indicator
            /// </summary>
            [FhirElement("inforce", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean InforceElement
            {
                get { return _InforceElement; }
                set { _InforceElement = value; OnPropertyChanged("InforceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _InforceElement;
            
            /// <summary>
            /// Coverage inforce indicator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Inforce
            {
                get { return InforceElement != null ? InforceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        InforceElement = null; 
                    else
                        InforceElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Inforce");
                }
            }
            
            /// <summary>
            /// When the benefits are applicable
            /// </summary>
            [FhirElement("benefitPeriod", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period BenefitPeriod
            {
                get { return _BenefitPeriod; }
                set { _BenefitPeriod = value; OnPropertyChanged("BenefitPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _BenefitPeriod;
            
            /// <summary>
            /// Benefits and authorization details
            /// </summary>
            [FhirElement("item", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CoverageEligibilityResponse.ItemsComponent> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.ItemsComponent>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<Hl7.Fhir.Model.CoverageEligibilityResponse.ItemsComponent> _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InsuranceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(InforceElement != null) dest.InforceElement = (Hl7.Fhir.Model.FhirBoolean)InforceElement.DeepCopy();
                    if(BenefitPeriod != null) dest.BenefitPeriod = (Hl7.Fhir.Model.Period)BenefitPeriod.DeepCopy();
                    if(Item != null) dest.Item = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.ItemsComponent>(Item.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InsuranceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(InforceElement, otherT.InforceElement)) return false;
                if( !DeepComparable.Matches(BenefitPeriod, otherT.BenefitPeriod)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(InforceElement, otherT.InforceElement)) return false;
                if( !DeepComparable.IsExactly(BenefitPeriod, otherT.BenefitPeriod)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Coverage != null) yield return Coverage;
                    if (InforceElement != null) yield return InforceElement;
                    if (BenefitPeriod != null) yield return BenefitPeriod;
                    foreach (var elem in Item) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    if (InforceElement != null) yield return new ElementValue("inforce", InforceElement);
                    if (BenefitPeriod != null) yield return new ElementValue("benefitPeriod", BenefitPeriod);
                    foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                }
            }

            
        }
        
        
        [FhirType("ItemsComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ItemsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ItemsComponent"; } }
            
            /// <summary>
            /// Benefit classification
            /// </summary>
            [FhirElement("category", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing, service, product, or drug code
            /// </summary>
            [FhirElement("productOrService", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ProductOrService
            {
                get { return _ProductOrService; }
                set { _ProductOrService = value; OnPropertyChanged("ProductOrService"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ProductOrService;
            
            /// <summary>
            /// Product or service billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Performing practitioner
            /// </summary>
            [FhirElement("provider", Order=70)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Provider;
            
            /// <summary>
            /// Excluded from the plan
            /// </summary>
            [FhirElement("excluded", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ExcludedElement
            {
                get { return _ExcludedElement; }
                set { _ExcludedElement = value; OnPropertyChanged("ExcludedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ExcludedElement;
            
            /// <summary>
            /// Excluded from the plan
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Excluded
            {
                get { return ExcludedElement != null ? ExcludedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ExcludedElement = null; 
                    else
                        ExcludedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Excluded");
                }
            }
            
            /// <summary>
            /// Short name for the benefit
            /// </summary>
            [FhirElement("name", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Short name for the benefit
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Description of the benefit or services covered
            /// </summary>
            [FhirElement("description", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of the benefit or services covered
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// In or out of network
            /// </summary>
            [FhirElement("network", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Network
            {
                get { return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Network;
            
            /// <summary>
            /// Individual or family
            /// </summary>
            [FhirElement("unit", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Unit
            {
                get { return _Unit; }
                set { _Unit = value; OnPropertyChanged("Unit"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Unit;
            
            /// <summary>
            /// Annual or lifetime
            /// </summary>
            [FhirElement("term", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Term
            {
                get { return _Term; }
                set { _Term = value; OnPropertyChanged("Term"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Term;
            
            /// <summary>
            /// Benefit Summary
            /// </summary>
            [FhirElement("benefit", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CoverageEligibilityResponse.BenefitComponent> Benefit
            {
                get { if(_Benefit==null) _Benefit = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.BenefitComponent>(); return _Benefit; }
                set { _Benefit = value; OnPropertyChanged("Benefit"); }
            }
            
            private List<Hl7.Fhir.Model.CoverageEligibilityResponse.BenefitComponent> _Benefit;
            
            /// <summary>
            /// Authorization required flag
            /// </summary>
            [FhirElement("authorizationRequired", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AuthorizationRequiredElement
            {
                get { return _AuthorizationRequiredElement; }
                set { _AuthorizationRequiredElement = value; OnPropertyChanged("AuthorizationRequiredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AuthorizationRequiredElement;
            
            /// <summary>
            /// Authorization required flag
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? AuthorizationRequired
            {
                get { return AuthorizationRequiredElement != null ? AuthorizationRequiredElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AuthorizationRequiredElement = null; 
                    else
                        AuthorizationRequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("AuthorizationRequired");
                }
            }
            
            /// <summary>
            /// Type of required supporting materials
            /// </summary>
            [FhirElement("authorizationSupporting", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AuthorizationSupporting
            {
                get { if(_AuthorizationSupporting==null) _AuthorizationSupporting = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AuthorizationSupporting; }
                set { _AuthorizationSupporting = value; OnPropertyChanged("AuthorizationSupporting"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AuthorizationSupporting;
            
            /// <summary>
            /// Preauthorization requirements endpoint
            /// </summary>
            [FhirElement("authorizationUrl", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri AuthorizationUrlElement
            {
                get { return _AuthorizationUrlElement; }
                set { _AuthorizationUrlElement = value; OnPropertyChanged("AuthorizationUrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _AuthorizationUrlElement;
            
            /// <summary>
            /// Preauthorization requirements endpoint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AuthorizationUrl
            {
                get { return AuthorizationUrlElement != null ? AuthorizationUrlElement.Value : null; }
                set
                {
                    if (value == null)
                        AuthorizationUrlElement = null; 
                    else
                        AuthorizationUrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("AuthorizationUrl");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(ProductOrService != null) dest.ProductOrService = (Hl7.Fhir.Model.CodeableConcept)ProductOrService.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                    if(ExcludedElement != null) dest.ExcludedElement = (Hl7.Fhir.Model.FhirBoolean)ExcludedElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Network != null) dest.Network = (Hl7.Fhir.Model.CodeableConcept)Network.DeepCopy();
                    if(Unit != null) dest.Unit = (Hl7.Fhir.Model.CodeableConcept)Unit.DeepCopy();
                    if(Term != null) dest.Term = (Hl7.Fhir.Model.CodeableConcept)Term.DeepCopy();
                    if(Benefit != null) dest.Benefit = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.BenefitComponent>(Benefit.DeepCopy());
                    if(AuthorizationRequiredElement != null) dest.AuthorizationRequiredElement = (Hl7.Fhir.Model.FhirBoolean)AuthorizationRequiredElement.DeepCopy();
                    if(AuthorizationSupporting != null) dest.AuthorizationSupporting = new List<Hl7.Fhir.Model.CodeableConcept>(AuthorizationSupporting.DeepCopy());
                    if(AuthorizationUrlElement != null) dest.AuthorizationUrlElement = (Hl7.Fhir.Model.FhirUri)AuthorizationUrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(ExcludedElement, otherT.ExcludedElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(Unit, otherT.Unit)) return false;
                if( !DeepComparable.Matches(Term, otherT.Term)) return false;
                if( !DeepComparable.Matches(Benefit, otherT.Benefit)) return false;
                if( !DeepComparable.Matches(AuthorizationRequiredElement, otherT.AuthorizationRequiredElement)) return false;
                if( !DeepComparable.Matches(AuthorizationSupporting, otherT.AuthorizationSupporting)) return false;
                if( !DeepComparable.Matches(AuthorizationUrlElement, otherT.AuthorizationUrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(ExcludedElement, otherT.ExcludedElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(Unit, otherT.Unit)) return false;
                if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
                if( !DeepComparable.IsExactly(Benefit, otherT.Benefit)) return false;
                if( !DeepComparable.IsExactly(AuthorizationRequiredElement, otherT.AuthorizationRequiredElement)) return false;
                if( !DeepComparable.IsExactly(AuthorizationSupporting, otherT.AuthorizationSupporting)) return false;
                if( !DeepComparable.IsExactly(AuthorizationUrlElement, otherT.AuthorizationUrlElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (ProductOrService != null) yield return ProductOrService;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Provider != null) yield return Provider;
                    if (ExcludedElement != null) yield return ExcludedElement;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Network != null) yield return Network;
                    if (Unit != null) yield return Unit;
                    if (Term != null) yield return Term;
                    foreach (var elem in Benefit) { if (elem != null) yield return elem; }
                    if (AuthorizationRequiredElement != null) yield return AuthorizationRequiredElement;
                    foreach (var elem in AuthorizationSupporting) { if (elem != null) yield return elem; }
                    if (AuthorizationUrlElement != null) yield return AuthorizationUrlElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (ProductOrService != null) yield return new ElementValue("productOrService", ProductOrService);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Provider != null) yield return new ElementValue("provider", Provider);
                    if (ExcludedElement != null) yield return new ElementValue("excluded", ExcludedElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Network != null) yield return new ElementValue("network", Network);
                    if (Unit != null) yield return new ElementValue("unit", Unit);
                    if (Term != null) yield return new ElementValue("term", Term);
                    foreach (var elem in Benefit) { if (elem != null) yield return new ElementValue("benefit", elem); }
                    if (AuthorizationRequiredElement != null) yield return new ElementValue("authorizationRequired", AuthorizationRequiredElement);
                    foreach (var elem in AuthorizationSupporting) { if (elem != null) yield return new ElementValue("authorizationSupporting", elem); }
                    if (AuthorizationUrlElement != null) yield return new ElementValue("authorizationUrl", AuthorizationUrlElement);
                }
            }

            
        }
        
        
        [FhirType("BenefitComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class BenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BenefitComponent"; } }
            
            /// <summary>
            /// Benefit classification
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Benefits allowed
            /// </summary>
            [FhirElement("allowed", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Money))]
            [DataMember]
            public Hl7.Fhir.Model.Element Allowed
            {
                get { return _Allowed; }
                set { _Allowed = value; OnPropertyChanged("Allowed"); }
            }
            
            private Hl7.Fhir.Model.Element _Allowed;
            
            /// <summary>
            /// Benefits used
            /// </summary>
            [FhirElement("used", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Money))]
            [DataMember]
            public Hl7.Fhir.Model.Element Used
            {
                get { return _Used; }
                set { _Used = value; OnPropertyChanged("Used"); }
            }
            
            private Hl7.Fhir.Model.Element _Used;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BenefitComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Allowed != null) dest.Allowed = (Hl7.Fhir.Model.Element)Allowed.DeepCopy();
                    if(Used != null) dest.Used = (Hl7.Fhir.Model.Element)Used.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BenefitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BenefitComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Allowed, otherT.Allowed)) return false;
                if( !DeepComparable.Matches(Used, otherT.Used)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BenefitComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Allowed, otherT.Allowed)) return false;
                if( !DeepComparable.IsExactly(Used, otherT.Used)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Allowed != null) yield return Allowed;
                    if (Used != null) yield return Used;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Allowed != null) yield return new ElementValue("allowed", Allowed);
                    if (Used != null) yield return new ElementValue("used", Used);
                }
            }

            
        }
        
        
        [FhirType("ErrorsComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ErrorsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ErrorsComponent"; } }
            
            /// <summary>
            /// Error code detailing processing issues
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ErrorsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ErrorsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ErrorsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ErrorsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for coverage eligiblity request
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// auth-requirements | benefits | discovery | validation
        /// </summary>
        [FhirElement("purpose", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose>> PurposeElement
        {
            get { if(_PurposeElement==null) _PurposeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose>>(); return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose>> _PurposeElement;
        
        /// <summary>
        /// auth-requirements | benefits | discovery | validation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose?> Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  PurposeElement = null; 
                else
                  PurposeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose>(elem)));
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// Intended recipient of products and services
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=120)]
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
        /// Estimated date or dates of service
        /// </summary>
        [FhirElement("serviced", Order=130, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Serviced
        {
            get { return _Serviced; }
            set { _Serviced = value; OnPropertyChanged("Serviced"); }
        }
        
        private Hl7.Fhir.Model.Element _Serviced;
        
        /// <summary>
        /// Response creation date
        /// </summary>
        [FhirElement("created", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Response creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Party responsible for the request
        /// </summary>
        [FhirElement("requestor", Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requestor
        {
            get { return _Requestor; }
            set { _Requestor = value; OnPropertyChanged("Requestor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requestor;
        
        /// <summary>
        /// Eligibility request reference
        /// </summary>
        [FhirElement("request", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("CoverageEligibilityRequest")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// queued | complete | error | partial
        /// </summary>
        [FhirElement("outcome", InSummary=true, Order=170)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ClaimProcessingCodes> OutcomeElement
        {
            get { return _OutcomeElement; }
            set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ClaimProcessingCodes> _OutcomeElement;
        
        /// <summary>
        /// queued | complete | error | partial
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ClaimProcessingCodes? Outcome
        {
            get { return OutcomeElement != null ? OutcomeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  OutcomeElement = null; 
                else
                  OutcomeElement = new Code<Hl7.Fhir.Model.ClaimProcessingCodes>(value);
                OnPropertyChanged("Outcome");
            }
        }
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DispositionElement
        {
            get { return _DispositionElement; }
            set { _DispositionElement = value; OnPropertyChanged("DispositionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DispositionElement;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Disposition
        {
            get { return DispositionElement != null ? DispositionElement.Value : null; }
            set
            {
                if (value == null)
                  DispositionElement = null; 
                else
                  DispositionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Disposition");
            }
        }
        
        /// <summary>
        /// Coverage issuer
        /// </summary>
        [FhirElement("insurer", InSummary=true, Order=190)]
        [CLSCompliant(false)]
		[References("Organization")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Insurer;
        
        /// <summary>
        /// Patient insurance information
        /// </summary>
        [FhirElement("insurance", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CoverageEligibilityResponse.InsuranceComponent> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.InsuranceComponent>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<Hl7.Fhir.Model.CoverageEligibilityResponse.InsuranceComponent> _Insurance;
        
        /// <summary>
        /// Preauthorization reference
        /// </summary>
        [FhirElement("preAuthRef", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PreAuthRefElement
        {
            get { return _PreAuthRefElement; }
            set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PreAuthRefElement;
        
        /// <summary>
        /// Preauthorization reference
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PreAuthRef
        {
            get { return PreAuthRefElement != null ? PreAuthRefElement.Value : null; }
            set
            {
                if (value == null)
                  PreAuthRefElement = null; 
                else
                  PreAuthRefElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PreAuthRef");
            }
        }
        
        /// <summary>
        /// Printed form identifier
        /// </summary>
        [FhirElement("form", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Form;
        
        /// <summary>
        /// Processing errors
        /// </summary>
        [FhirElement("error", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CoverageEligibilityResponse.ErrorsComponent> Error
        {
            get { if(_Error==null) _Error = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.ErrorsComponent>(); return _Error; }
            set { _Error = value; OnPropertyChanged("Error"); }
        }
        
        private List<Hl7.Fhir.Model.CoverageEligibilityResponse.ErrorsComponent> _Error;
        

        public static ElementDefinition.ConstraintComponent CoverageEligibilityResponse_CES_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "insurance.item.all(category.exists() xor productOrService.exists())",
            Key = "ces-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "SHALL contain a category or a billcode but not both.",
            Xpath = "exists(f:category) or exists(f:productOrService)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(CoverageEligibilityResponse_CES_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CoverageEligibilityResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(PurposeElement != null) dest.PurposeElement = new List<Code<Hl7.Fhir.Model.CoverageEligibilityResponse.EligibilityResponsePurpose>>(PurposeElement.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Requestor != null) dest.Requestor = (Hl7.Fhir.Model.ResourceReference)Requestor.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.ClaimProcessingCodes>)OutcomeElement.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(Insurance != null) dest.Insurance = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.InsuranceComponent>(Insurance.DeepCopy());
                if(PreAuthRefElement != null) dest.PreAuthRefElement = (Hl7.Fhir.Model.FhirString)PreAuthRefElement.DeepCopy();
                if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                if(Error != null) dest.Error = new List<Hl7.Fhir.Model.CoverageEligibilityResponse.ErrorsComponent>(Error.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new CoverageEligibilityResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CoverageEligibilityResponse;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Requestor, otherT.Requestor)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(Error, otherT.Error)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CoverageEligibilityResponse;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Requestor, otherT.Requestor)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(Error, otherT.Error)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in PurposeElement) { if (elem != null) yield return elem; }
				if (Patient != null) yield return Patient;
				if (Serviced != null) yield return Serviced;
				if (CreatedElement != null) yield return CreatedElement;
				if (Requestor != null) yield return Requestor;
				if (Request != null) yield return Request;
				if (OutcomeElement != null) yield return OutcomeElement;
				if (DispositionElement != null) yield return DispositionElement;
				if (Insurer != null) yield return Insurer;
				foreach (var elem in Insurance) { if (elem != null) yield return elem; }
				if (PreAuthRefElement != null) yield return PreAuthRefElement;
				if (Form != null) yield return Form;
				foreach (var elem in Error) { if (elem != null) yield return elem; }
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
                foreach (var elem in PurposeElement) { if (elem != null) yield return new ElementValue("purpose", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Serviced != null) yield return new ElementValue("serviced", Serviced);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Requestor != null) yield return new ElementValue("requestor", Requestor);
                if (Request != null) yield return new ElementValue("request", Request);
                if (OutcomeElement != null) yield return new ElementValue("outcome", OutcomeElement);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
                if (PreAuthRefElement != null) yield return new ElementValue("preAuthRef", PreAuthRefElement);
                if (Form != null) yield return new ElementValue("form", Form);
                foreach (var elem in Error) { if (elem != null) yield return new ElementValue("error", elem); }
            }
        }

    }
    
}
