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
    /// EligibilityResponse resource
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "EligibilityResponse", IsResource=true)]
    [DataContract]
    public partial class EligibilityResponse : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IEligibilityResponse, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.EligibilityResponse; } }
        [NotMapped]
        public override string TypeName { get { return "EligibilityResponse"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "InsuranceComponent")]
        [DataContract]
        public partial class InsuranceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InsuranceComponent"; } }
            
            /// <summary>
            /// Updated Coverage details
            /// </summary>
            [FhirElement("coverage", Order=40)]
            [CLSCompliant(false)]
            [References("Coverage")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Coverage;
            
            /// <summary>
            /// Contract details
            /// </summary>
            [FhirElement("contract", Order=50)]
            [CLSCompliant(false)]
            [References("Contract")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Contract
            {
                get { return _Contract; }
                set { _Contract = value; OnPropertyChanged("Contract"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Contract;
            
            /// <summary>
            /// Benefits by Category
            /// </summary>
            [FhirElement("benefitBalance", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<BenefitsComponent> BenefitBalance
            {
                get { if(_BenefitBalance==null) _BenefitBalance = new List<BenefitsComponent>(); return _BenefitBalance; }
                set { _BenefitBalance = value; OnPropertyChanged("BenefitBalance"); }
            }
            
            private List<BenefitsComponent> _BenefitBalance;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InsuranceComponent");
                base.Serialize(sink);
                sink.Element("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Coverage?.Serialize(sink);
                sink.Element("contract", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Contract?.Serialize(sink);
                sink.BeginList("benefitBalance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in BenefitBalance)
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
                    case "coverage":
                        Coverage = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "contract":
                        Contract = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "benefitBalance":
                        BenefitBalance = source.GetList<BenefitsComponent>();
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
                    case "coverage":
                        Coverage = source.Populate(Coverage);
                        return true;
                    case "contract":
                        Contract = source.Populate(Contract);
                        return true;
                    case "benefitBalance":
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
                    case "benefitBalance":
                        source.PopulateListItem(BenefitBalance, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InsuranceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(Contract != null) dest.Contract = (Hl7.Fhir.Model.ResourceReference)Contract.DeepCopy();
                    if(BenefitBalance != null) dest.BenefitBalance = new List<BenefitsComponent>(BenefitBalance.DeepCopy());
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
                if( !DeepComparable.Matches(Contract, otherT.Contract)) return false;
                if( !DeepComparable.Matches(BenefitBalance, otherT.BenefitBalance)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(Contract, otherT.Contract)) return false;
                if( !DeepComparable.IsExactly(BenefitBalance, otherT.BenefitBalance)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Coverage != null) yield return Coverage;
                    if (Contract != null) yield return Contract;
                    foreach (var elem in BenefitBalance) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    if (Contract != null) yield return new ElementValue("contract", Contract);
                    foreach (var elem in BenefitBalance) { if (elem != null) yield return new ElementValue("benefitBalance", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "BenefitsComponent")]
        [DataContract]
        public partial class BenefitsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BenefitsComponent"; } }
            
            /// <summary>
            /// Type of services covered
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Detailed services covered within the type
            /// </summary>
            [FhirElement("subCategory", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SubCategory
            {
                get { return _SubCategory; }
                set { _SubCategory = value; OnPropertyChanged("SubCategory"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SubCategory;
            
            /// <summary>
            /// Excluded from the plan
            /// </summary>
            [FhirElement("excluded", Order=60)]
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
                    if (value == null)
                        ExcludedElement = null;
                    else
                        ExcludedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Excluded");
                }
            }
            
            /// <summary>
            /// Short name for the benefit
            /// </summary>
            [FhirElement("name", Order=70)]
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
            [FhirElement("description", Order=80)]
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
            [FhirElement("network", Order=90)]
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
            [FhirElement("unit", Order=100)]
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
            [FhirElement("term", Order=110)]
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
            [FhirElement("financial", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<BenefitComponent> Financial
            {
                get { if(_Financial==null) _Financial = new List<BenefitComponent>(); return _Financial; }
                set { _Financial = value; OnPropertyChanged("Financial"); }
            }
            
            private List<BenefitComponent> _Financial;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BenefitsComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Category?.Serialize(sink);
                sink.Element("subCategory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubCategory?.Serialize(sink);
                sink.Element("excluded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExcludedElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("network", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Network?.Serialize(sink);
                sink.Element("unit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Unit?.Serialize(sink);
                sink.Element("term", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Term?.Serialize(sink);
                sink.BeginList("financial", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Financial)
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "subCategory":
                        SubCategory = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "excluded":
                        ExcludedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "network":
                        Network = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "unit":
                        Unit = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "term":
                        Term = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "financial":
                        Financial = source.GetList<BenefitComponent>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "subCategory":
                        SubCategory = source.Populate(SubCategory);
                        return true;
                    case "excluded":
                        ExcludedElement = source.PopulateValue(ExcludedElement);
                        return true;
                    case "_excluded":
                        ExcludedElement = source.Populate(ExcludedElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "network":
                        Network = source.Populate(Network);
                        return true;
                    case "unit":
                        Unit = source.Populate(Unit);
                        return true;
                    case "term":
                        Term = source.Populate(Term);
                        return true;
                    case "financial":
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
                    case "financial":
                        source.PopulateListItem(Financial, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BenefitsComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(SubCategory != null) dest.SubCategory = (Hl7.Fhir.Model.CodeableConcept)SubCategory.DeepCopy();
                    if(ExcludedElement != null) dest.ExcludedElement = (Hl7.Fhir.Model.FhirBoolean)ExcludedElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Network != null) dest.Network = (Hl7.Fhir.Model.CodeableConcept)Network.DeepCopy();
                    if(Unit != null) dest.Unit = (Hl7.Fhir.Model.CodeableConcept)Unit.DeepCopy();
                    if(Term != null) dest.Term = (Hl7.Fhir.Model.CodeableConcept)Term.DeepCopy();
                    if(Financial != null) dest.Financial = new List<BenefitComponent>(Financial.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new BenefitsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BenefitsComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(SubCategory, otherT.SubCategory)) return false;
                if( !DeepComparable.Matches(ExcludedElement, otherT.ExcludedElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(Unit, otherT.Unit)) return false;
                if( !DeepComparable.Matches(Term, otherT.Term)) return false;
                if( !DeepComparable.Matches(Financial, otherT.Financial)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BenefitsComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(SubCategory, otherT.SubCategory)) return false;
                if( !DeepComparable.IsExactly(ExcludedElement, otherT.ExcludedElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(Unit, otherT.Unit)) return false;
                if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
                if( !DeepComparable.IsExactly(Financial, otherT.Financial)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (SubCategory != null) yield return SubCategory;
                    if (ExcludedElement != null) yield return ExcludedElement;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Network != null) yield return Network;
                    if (Unit != null) yield return Unit;
                    if (Term != null) yield return Term;
                    foreach (var elem in Financial) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (SubCategory != null) yield return new ElementValue("subCategory", SubCategory);
                    if (ExcludedElement != null) yield return new ElementValue("excluded", ExcludedElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Network != null) yield return new ElementValue("network", Network);
                    if (Unit != null) yield return new ElementValue("unit", Unit);
                    if (Term != null) yield return new ElementValue("term", Term);
                    foreach (var elem in Financial) { if (elem != null) yield return new ElementValue("financial", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "BenefitComponent")]
        [DataContract]
        public partial class BenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BenefitComponent"; } }
            
            /// <summary>
            /// Deductable, visits, benefit amount
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
            [AllowedTypes(typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.STU3.Money))]
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
            [AllowedTypes(typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.STU3.Money))]
            [DataMember]
            public Hl7.Fhir.Model.Element Used
            {
                get { return _Used; }
                set { _Used = value; OnPropertyChanged("Used"); }
            }
            
            private Hl7.Fhir.Model.Element _Used;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BenefitComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("allowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Allowed?.Serialize(sink);
                sink.Element("used", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Used?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "allowedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "allowedString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "allowedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "usedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Used, "used");
                        Used = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "usedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Used, "used");
                        Used = source.Get<Hl7.Fhir.Model.STU3.Money>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "allowedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Allowed, "allowed");
                        Allowed = source.PopulateValue(Allowed as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_allowedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "allowedString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Allowed, "allowed");
                        Allowed = source.PopulateValue(Allowed as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_allowedString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "allowedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.STU3.Money);
                        return true;
                    case "usedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Used, "used");
                        Used = source.PopulateValue(Used as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_usedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Used, "used");
                        Used = source.Populate(Used as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "usedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Used, "used");
                        Used = source.Populate(Used as Hl7.Fhir.Model.STU3.Money);
                        return true;
                }
                return false;
            }
        
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ErrorsComponent")]
        [DataContract]
        public partial class ErrorsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ErrorsComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Code?.Serialize(sink);
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
                }
                return false;
            }
        
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
        /// Business Identifier
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
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
        /// Responsible practitioner
        /// </summary>
        [FhirElement("requestProvider", Order=120)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestProvider
        {
            get { return _RequestProvider; }
            set { _RequestProvider = value; OnPropertyChanged("RequestProvider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestProvider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("requestOrganization", Order=130)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestOrganization
        {
            get { return _RequestOrganization; }
            set { _RequestOrganization = value; OnPropertyChanged("RequestOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestOrganization;
        
        /// <summary>
        /// Eligibility reference
        /// </summary>
        [FhirElement("request", Order=140)]
        [CLSCompliant(false)]
        [References("EligibilityRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// complete | error | partial
        /// </summary>
        [FhirElement("outcome", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", Order=160)]
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
        /// Insurer issuing the coverage
        /// </summary>
        [FhirElement("insurer", Order=170)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Insurer;
        
        /// <summary>
        /// Coverage inforce indicator
        /// </summary>
        [FhirElement("inforce", Order=180)]
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
                if (value == null)
                    InforceElement = null;
                else
                    InforceElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Inforce");
            }
        }
        
        /// <summary>
        /// Details by insurance coverage
        /// </summary>
        [FhirElement("insurance", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<InsuranceComponent> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<InsuranceComponent>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<InsuranceComponent> _Insurance;
        
        /// <summary>
        /// Printed Form Identifier
        /// </summary>
        [FhirElement("form", Order=200)]
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
        [FhirElement("error", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ErrorsComponent> Error
        {
            get { if(_Error==null) _Error = new List<ErrorsComponent>(); return _Error; }
            set { _Error = value; OnPropertyChanged("Error"); }
        }
        
        private List<ErrorsComponent> _Error;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as EligibilityResponse;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(RequestProvider != null) dest.RequestProvider = (Hl7.Fhir.Model.ResourceReference)RequestProvider.DeepCopy();
                if(RequestOrganization != null) dest.RequestOrganization = (Hl7.Fhir.Model.ResourceReference)RequestOrganization.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(InforceElement != null) dest.InforceElement = (Hl7.Fhir.Model.FhirBoolean)InforceElement.DeepCopy();
                if(Insurance != null) dest.Insurance = new List<InsuranceComponent>(Insurance.DeepCopy());
                if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                if(Error != null) dest.Error = new List<ErrorsComponent>(Error.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new EligibilityResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as EligibilityResponse;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.Matches(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(InforceElement, otherT.InforceElement)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(Error, otherT.Error)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as EligibilityResponse;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.IsExactly(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(InforceElement, otherT.InforceElement)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(Error, otherT.Error)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("EligibilityResponse");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("created", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CreatedElement?.Serialize(sink);
            sink.Element("requestProvider", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequestProvider?.Serialize(sink);
            sink.Element("requestOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequestOrganization?.Serialize(sink);
            sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Request?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Outcome?.Serialize(sink);
            sink.Element("disposition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DispositionElement?.Serialize(sink);
            sink.Element("insurer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Insurer?.Serialize(sink);
            sink.Element("inforce", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InforceElement?.Serialize(sink);
            sink.BeginList("insurance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Insurance)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("form", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Form?.Serialize(sink);
            sink.BeginList("error", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Error)
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
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>>();
                    return true;
                case "created":
                    CreatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "requestProvider":
                    RequestProvider = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "requestOrganization":
                    RequestOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "request":
                    Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "outcome":
                    Outcome = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "disposition":
                    DispositionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "insurer":
                    Insurer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "inforce":
                    InforceElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "insurance":
                    Insurance = source.GetList<InsuranceComponent>();
                    return true;
                case "form":
                    Form = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "error":
                    Error = source.GetList<ErrorsComponent>();
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
                case "created":
                    CreatedElement = source.PopulateValue(CreatedElement);
                    return true;
                case "_created":
                    CreatedElement = source.Populate(CreatedElement);
                    return true;
                case "requestProvider":
                    RequestProvider = source.Populate(RequestProvider);
                    return true;
                case "requestOrganization":
                    RequestOrganization = source.Populate(RequestOrganization);
                    return true;
                case "request":
                    Request = source.Populate(Request);
                    return true;
                case "outcome":
                    Outcome = source.Populate(Outcome);
                    return true;
                case "disposition":
                    DispositionElement = source.PopulateValue(DispositionElement);
                    return true;
                case "_disposition":
                    DispositionElement = source.Populate(DispositionElement);
                    return true;
                case "insurer":
                    Insurer = source.Populate(Insurer);
                    return true;
                case "inforce":
                    InforceElement = source.PopulateValue(InforceElement);
                    return true;
                case "_inforce":
                    InforceElement = source.Populate(InforceElement);
                    return true;
                case "insurance":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "form":
                    Form = source.Populate(Form);
                    return true;
                case "error":
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
                case "insurance":
                    source.PopulateListItem(Insurance, index);
                    return true;
                case "error":
                    source.PopulateListItem(Error, index);
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
                if (CreatedElement != null) yield return CreatedElement;
                if (RequestProvider != null) yield return RequestProvider;
                if (RequestOrganization != null) yield return RequestOrganization;
                if (Request != null) yield return Request;
                if (Outcome != null) yield return Outcome;
                if (DispositionElement != null) yield return DispositionElement;
                if (Insurer != null) yield return Insurer;
                if (InforceElement != null) yield return InforceElement;
                foreach (var elem in Insurance) { if (elem != null) yield return elem; }
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
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (RequestProvider != null) yield return new ElementValue("requestProvider", RequestProvider);
                if (RequestOrganization != null) yield return new ElementValue("requestOrganization", RequestOrganization);
                if (Request != null) yield return new ElementValue("request", Request);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                if (InforceElement != null) yield return new ElementValue("inforce", InforceElement);
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
                if (Form != null) yield return new ElementValue("form", Form);
                foreach (var elem in Error) { if (elem != null) yield return new ElementValue("error", elem); }
            }
        }
    
    }

}
