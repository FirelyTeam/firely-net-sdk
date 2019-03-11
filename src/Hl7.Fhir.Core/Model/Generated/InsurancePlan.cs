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
    /// Details of a Health Insurance product/plan provided by an organization
    /// </summary>
    [FhirType("InsurancePlan", IsResource=true)]
    [DataContract]
    public partial class InsurancePlan : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.InsurancePlan; } }
        [NotMapped]
        public override string TypeName { get { return "InsurancePlan"; } }
        
        [FhirType("ContactComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContactComponent"; } }
            
            /// <summary>
            /// The type of contact
            /// </summary>
            [FhirElement("purpose", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Purpose
            {
                get { return _Purpose; }
                set { _Purpose = value; OnPropertyChanged("Purpose"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Purpose;
            
            /// <summary>
            /// A name associated with the contact
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.HumanName Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            
            private Hl7.Fhir.Model.HumanName _Name;
            
            /// <summary>
            /// Contact details (telephone, email, etc.)  for a contact
            /// </summary>
            [FhirElement("telecom", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            
            private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
            
            /// <summary>
            /// Visiting or postal addresses for the contact
            /// </summary>
            [FhirElement("address", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Address Address
            {
                get { return _Address; }
                set { _Address = value; OnPropertyChanged("Address"); }
            }
            
            private Hl7.Fhir.Model.Address _Address;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.CodeableConcept)Purpose.DeepCopy();
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.HumanName)Name.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                    if(Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                if( !DeepComparable.Matches(Address, otherT.Address)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Purpose != null) yield return Purpose;
                    if (Name != null) yield return Name;
                    foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                    if (Address != null) yield return Address;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                    if (Name != null) yield return new ElementValue("name", Name);
                    foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                    if (Address != null) yield return new ElementValue("address", Address);
                }
            }

            
        }
        
        
        [FhirType("CoverageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// Type of coverage
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
            /// What networks provide coverage
            /// </summary>
            [FhirElement("network", Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Network
            {
                get { if(_Network==null) _Network = new List<Hl7.Fhir.Model.ResourceReference>(); return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Network;
            
            /// <summary>
            /// List of benefits
            /// </summary>
            [FhirElement("benefit", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.InsurancePlan.CoverageBenefitComponent> Benefit
            {
                get { if(_Benefit==null) _Benefit = new List<Hl7.Fhir.Model.InsurancePlan.CoverageBenefitComponent>(); return _Benefit; }
                set { _Benefit = value; OnPropertyChanged("Benefit"); }
            }
            
            private List<Hl7.Fhir.Model.InsurancePlan.CoverageBenefitComponent> _Benefit;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Network != null) dest.Network = new List<Hl7.Fhir.Model.ResourceReference>(Network.DeepCopy());
                    if(Benefit != null) dest.Benefit = new List<Hl7.Fhir.Model.InsurancePlan.CoverageBenefitComponent>(Benefit.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CoverageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(Benefit, otherT.Benefit)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(Benefit, otherT.Benefit)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Network) { if (elem != null) yield return elem; }
                    foreach (var elem in Benefit) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Network) { if (elem != null) yield return new ElementValue("network", elem); }
                    foreach (var elem in Benefit) { if (elem != null) yield return new ElementValue("benefit", elem); }
                }
            }

            
        }
        
        
        [FhirType("CoverageBenefitComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CoverageBenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageBenefitComponent"; } }
            
            /// <summary>
            /// Type of benefit
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
            /// Referral requirements
            /// </summary>
            [FhirElement("requirement", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementElement
            {
                get { return _RequirementElement; }
                set { _RequirementElement = value; OnPropertyChanged("RequirementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequirementElement;
            
            /// <summary>
            /// Referral requirements
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Requirement
            {
                get { return RequirementElement != null ? RequirementElement.Value : null; }
                set
                {
                    if (value == null)
                        RequirementElement = null; 
                    else
                        RequirementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Requirement");
                }
            }
            
            /// <summary>
            /// Benefit limits
            /// </summary>
            [FhirElement("limit", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.InsurancePlan.LimitComponent> Limit
            {
                get { if(_Limit==null) _Limit = new List<Hl7.Fhir.Model.InsurancePlan.LimitComponent>(); return _Limit; }
                set { _Limit = value; OnPropertyChanged("Limit"); }
            }
            
            private List<Hl7.Fhir.Model.InsurancePlan.LimitComponent> _Limit;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageBenefitComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(RequirementElement != null) dest.RequirementElement = (Hl7.Fhir.Model.FhirString)RequirementElement.DeepCopy();
                    if(Limit != null) dest.Limit = new List<Hl7.Fhir.Model.InsurancePlan.LimitComponent>(Limit.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CoverageBenefitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageBenefitComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.Matches(Limit, otherT.Limit)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageBenefitComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.IsExactly(Limit, otherT.Limit)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (RequirementElement != null) yield return RequirementElement;
                    foreach (var elem in Limit) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (RequirementElement != null) yield return new ElementValue("requirement", RequirementElement);
                    foreach (var elem in Limit) { if (elem != null) yield return new ElementValue("limit", elem); }
                }
            }

            
        }
        
        
        [FhirType("LimitComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class LimitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "LimitComponent"; } }
            
            /// <summary>
            /// Maximum value allowed
            /// </summary>
            [FhirElement("value", Order=40)]
            [DataMember]
            public Quantity Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Quantity _Value;
            
            /// <summary>
            /// Benefit limit details
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LimitComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Quantity)Value.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LimitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LimitComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LimitComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                    if (Code != null) yield return Code;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (Code != null) yield return new ElementValue("code", Code);
                }
            }

            
        }
        
        
        [FhirType("PlanComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PlanComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PlanComponent"; } }
            
            /// <summary>
            /// Business Identifier for Product
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Type of plan
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Where product applies
            /// </summary>
            [FhirElement("coverageArea", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("Location")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> CoverageArea
            {
                get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(); return _CoverageArea; }
                set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _CoverageArea;
            
            /// <summary>
            /// What networks provide coverage
            /// </summary>
            [FhirElement("network", Order=70)]
            [CLSCompliant(false)]
			[References("Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Network
            {
                get { if(_Network==null) _Network = new List<Hl7.Fhir.Model.ResourceReference>(); return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Network;
            
            /// <summary>
            /// Overall costs
            /// </summary>
            [FhirElement("generalCost", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.InsurancePlan.GeneralCostComponent> GeneralCost
            {
                get { if(_GeneralCost==null) _GeneralCost = new List<Hl7.Fhir.Model.InsurancePlan.GeneralCostComponent>(); return _GeneralCost; }
                set { _GeneralCost = value; OnPropertyChanged("GeneralCost"); }
            }
            
            private List<Hl7.Fhir.Model.InsurancePlan.GeneralCostComponent> _GeneralCost;
            
            /// <summary>
            /// Specific costs
            /// </summary>
            [FhirElement("specificCost", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.InsurancePlan.SpecificCostComponent> SpecificCost
            {
                get { if(_SpecificCost==null) _SpecificCost = new List<Hl7.Fhir.Model.InsurancePlan.SpecificCostComponent>(); return _SpecificCost; }
                set { _SpecificCost = value; OnPropertyChanged("SpecificCost"); }
            }
            
            private List<Hl7.Fhir.Model.InsurancePlan.SpecificCostComponent> _SpecificCost;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PlanComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(CoverageArea.DeepCopy());
                    if(Network != null) dest.Network = new List<Hl7.Fhir.Model.ResourceReference>(Network.DeepCopy());
                    if(GeneralCost != null) dest.GeneralCost = new List<Hl7.Fhir.Model.InsurancePlan.GeneralCostComponent>(GeneralCost.DeepCopy());
                    if(SpecificCost != null) dest.SpecificCost = new List<Hl7.Fhir.Model.InsurancePlan.SpecificCostComponent>(SpecificCost.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PlanComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PlanComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(GeneralCost, otherT.GeneralCost)) return false;
                if( !DeepComparable.Matches(SpecificCost, otherT.SpecificCost)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PlanComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(GeneralCost, otherT.GeneralCost)) return false;
                if( !DeepComparable.IsExactly(SpecificCost, otherT.SpecificCost)) return false;
                
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
                    foreach (var elem in CoverageArea) { if (elem != null) yield return elem; }
                    foreach (var elem in Network) { if (elem != null) yield return elem; }
                    foreach (var elem in GeneralCost) { if (elem != null) yield return elem; }
                    foreach (var elem in SpecificCost) { if (elem != null) yield return elem; }
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
                    foreach (var elem in CoverageArea) { if (elem != null) yield return new ElementValue("coverageArea", elem); }
                    foreach (var elem in Network) { if (elem != null) yield return new ElementValue("network", elem); }
                    foreach (var elem in GeneralCost) { if (elem != null) yield return new ElementValue("generalCost", elem); }
                    foreach (var elem in SpecificCost) { if (elem != null) yield return new ElementValue("specificCost", elem); }
                }
            }

            
        }
        
        
        [FhirType("GeneralCostComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class GeneralCostComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GeneralCostComponent"; } }
            
            /// <summary>
            /// Type of cost
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Number of enrollees
            /// </summary>
            [FhirElement("groupSize", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt GroupSizeElement
            {
                get { return _GroupSizeElement; }
                set { _GroupSizeElement = value; OnPropertyChanged("GroupSizeElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _GroupSizeElement;
            
            /// <summary>
            /// Number of enrollees
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? GroupSize
            {
                get { return GroupSizeElement != null ? GroupSizeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        GroupSizeElement = null; 
                    else
                        GroupSizeElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("GroupSize");
                }
            }
            
            /// <summary>
            /// Cost value
            /// </summary>
            [FhirElement("cost", Order=60)]
            [DataMember]
            public Money Cost
            {
                get { return _Cost; }
                set { _Cost = value; OnPropertyChanged("Cost"); }
            }
            
            private Money _Cost;
            
            /// <summary>
            /// Additional cost information
            /// </summary>
            [FhirElement("comment", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Additional cost information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comment
            {
                get { return CommentElement != null ? CommentElement.Value : null; }
                set
                {
                    if (value == null)
                        CommentElement = null; 
                    else
                        CommentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comment");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GeneralCostComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(GroupSizeElement != null) dest.GroupSizeElement = (Hl7.Fhir.Model.PositiveInt)GroupSizeElement.DeepCopy();
                    if(Cost != null) dest.Cost = (Money)Cost.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GeneralCostComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GeneralCostComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(GroupSizeElement, otherT.GroupSizeElement)) return false;
                if( !DeepComparable.Matches(Cost, otherT.Cost)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GeneralCostComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(GroupSizeElement, otherT.GroupSizeElement)) return false;
                if( !DeepComparable.IsExactly(Cost, otherT.Cost)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (GroupSizeElement != null) yield return GroupSizeElement;
                    if (Cost != null) yield return Cost;
                    if (CommentElement != null) yield return CommentElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (GroupSizeElement != null) yield return new ElementValue("groupSize", GroupSizeElement);
                    if (Cost != null) yield return new ElementValue("cost", Cost);
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                }
            }

            
        }
        
        
        [FhirType("SpecificCostComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SpecificCostComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SpecificCostComponent"; } }
            
            /// <summary>
            /// General category of benefit
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
            /// Benefits list
            /// </summary>
            [FhirElement("benefit", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.InsurancePlan.PlanBenefitComponent> Benefit
            {
                get { if(_Benefit==null) _Benefit = new List<Hl7.Fhir.Model.InsurancePlan.PlanBenefitComponent>(); return _Benefit; }
                set { _Benefit = value; OnPropertyChanged("Benefit"); }
            }
            
            private List<Hl7.Fhir.Model.InsurancePlan.PlanBenefitComponent> _Benefit;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecificCostComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Benefit != null) dest.Benefit = new List<Hl7.Fhir.Model.InsurancePlan.PlanBenefitComponent>(Benefit.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecificCostComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecificCostComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Benefit, otherT.Benefit)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecificCostComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Benefit, otherT.Benefit)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    foreach (var elem in Benefit) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    foreach (var elem in Benefit) { if (elem != null) yield return new ElementValue("benefit", elem); }
                }
            }

            
        }
        
        
        [FhirType("PlanBenefitComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PlanBenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PlanBenefitComponent"; } }
            
            /// <summary>
            /// Type of specific benefit
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
            /// List of the costs
            /// </summary>
            [FhirElement("cost", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.InsurancePlan.CostComponent> Cost
            {
                get { if(_Cost==null) _Cost = new List<Hl7.Fhir.Model.InsurancePlan.CostComponent>(); return _Cost; }
                set { _Cost = value; OnPropertyChanged("Cost"); }
            }
            
            private List<Hl7.Fhir.Model.InsurancePlan.CostComponent> _Cost;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PlanBenefitComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Cost != null) dest.Cost = new List<Hl7.Fhir.Model.InsurancePlan.CostComponent>(Cost.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PlanBenefitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PlanBenefitComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Cost, otherT.Cost)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PlanBenefitComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Cost, otherT.Cost)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Cost) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Cost) { if (elem != null) yield return new ElementValue("cost", elem); }
                }
            }

            
        }
        
        
        [FhirType("CostComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CostComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CostComponent"; } }
            
            /// <summary>
            /// Type of cost
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
            /// in-network | out-of-network | other
            /// </summary>
            [FhirElement("applicability", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Applicability
            {
                get { return _Applicability; }
                set { _Applicability = value; OnPropertyChanged("Applicability"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Applicability;
            
            /// <summary>
            /// Additional information about the cost
            /// </summary>
            [FhirElement("qualifiers", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Qualifiers
            {
                get { if(_Qualifiers==null) _Qualifiers = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Qualifiers; }
                set { _Qualifiers = value; OnPropertyChanged("Qualifiers"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Qualifiers;
            
            /// <summary>
            /// The actual cost value
            /// </summary>
            [FhirElement("value", Order=70)]
            [DataMember]
            public Quantity Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Quantity _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CostComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Applicability != null) dest.Applicability = (Hl7.Fhir.Model.CodeableConcept)Applicability.DeepCopy();
                    if(Qualifiers != null) dest.Qualifiers = new List<Hl7.Fhir.Model.CodeableConcept>(Qualifiers.DeepCopy());
                    if(Value != null) dest.Value = (Quantity)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CostComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CostComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Applicability, otherT.Applicability)) return false;
                if( !DeepComparable.Matches(Qualifiers, otherT.Qualifiers)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CostComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Applicability, otherT.Applicability)) return false;
                if( !DeepComparable.IsExactly(Qualifiers, otherT.Qualifiers)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Applicability != null) yield return Applicability;
                    foreach (var elem in Qualifiers) { if (elem != null) yield return elem; }
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Applicability != null) yield return new ElementValue("applicability", Applicability);
                    foreach (var elem in Qualifiers) { if (elem != null) yield return new ElementValue("qualifiers", elem); }
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for Product
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Kind of product
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// Official name
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Official name
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
        /// Alternate names
        /// </summary>
        [FhirElement("alias", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if(_AliasElement==null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _AliasElement;
        
        /// <summary>
        /// Alternate names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Alias
        {
            get { return AliasElement != null ? AliasElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  AliasElement = null; 
                else
                  AliasElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Alias");
            }
        }
        
        /// <summary>
        /// When the product is available
        /// </summary>
        [FhirElement("period", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Plan issuer
        /// </summary>
        [FhirElement("ownedBy", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OwnedBy
        {
            get { return _OwnedBy; }
            set { _OwnedBy = value; OnPropertyChanged("OwnedBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OwnedBy;
        
        /// <summary>
        /// Product administrator
        /// </summary>
        [FhirElement("administeredBy", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference AdministeredBy
        {
            get { return _AdministeredBy; }
            set { _AdministeredBy = value; OnPropertyChanged("AdministeredBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _AdministeredBy;
        
        /// <summary>
        /// Where product applies
        /// </summary>
        [FhirElement("coverageArea", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CoverageArea
        {
            get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(); return _CoverageArea; }
            set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CoverageArea;
        
        /// <summary>
        /// Contact for the product
        /// </summary>
        [FhirElement("contact", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.InsurancePlan.ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.InsurancePlan.ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.InsurancePlan.ContactComponent> _Contact;
        
        /// <summary>
        /// Technical endpoint
        /// </summary>
        [FhirElement("endpoint", Order=190)]
        [CLSCompliant(false)]
		[References("Endpoint")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Endpoint
        {
            get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
            set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
        
        /// <summary>
        /// What networks are Included
        /// </summary>
        [FhirElement("network", Order=200)]
        [CLSCompliant(false)]
		[References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Network
        {
            get { if(_Network==null) _Network = new List<Hl7.Fhir.Model.ResourceReference>(); return _Network; }
            set { _Network = value; OnPropertyChanged("Network"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Network;
        
        /// <summary>
        /// Coverage details
        /// </summary>
        [FhirElement("coverage", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.InsurancePlan.CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<Hl7.Fhir.Model.InsurancePlan.CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<Hl7.Fhir.Model.InsurancePlan.CoverageComponent> _Coverage;
        
        /// <summary>
        /// Plan details
        /// </summary>
        [FhirElement("plan", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.InsurancePlan.PlanComponent> Plan
        {
            get { if(_Plan==null) _Plan = new List<Hl7.Fhir.Model.InsurancePlan.PlanComponent>(); return _Plan; }
            set { _Plan = value; OnPropertyChanged("Plan"); }
        }
        
        private List<Hl7.Fhir.Model.InsurancePlan.PlanComponent> _Plan;
        

        public static ElementDefinition.ConstraintComponent InsurancePlan_IPN_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(identifier.count() + name.count()) > 0",
            Key = "ipn-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "The organization SHALL at least have a name or an idendtifier, and possibly more than one",
            Xpath = "count(f:identifier | f:name) > 0"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(InsurancePlan_IPN_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as InsurancePlan;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(OwnedBy != null) dest.OwnedBy = (Hl7.Fhir.Model.ResourceReference)OwnedBy.DeepCopy();
                if(AdministeredBy != null) dest.AdministeredBy = (Hl7.Fhir.Model.ResourceReference)AdministeredBy.DeepCopy();
                if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(CoverageArea.DeepCopy());
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.InsurancePlan.ContactComponent>(Contact.DeepCopy());
                if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
                if(Network != null) dest.Network = new List<Hl7.Fhir.Model.ResourceReference>(Network.DeepCopy());
                if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.InsurancePlan.CoverageComponent>(Coverage.DeepCopy());
                if(Plan != null) dest.Plan = new List<Hl7.Fhir.Model.InsurancePlan.PlanComponent>(Plan.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new InsurancePlan());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as InsurancePlan;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(OwnedBy, otherT.OwnedBy)) return false;
            if( !DeepComparable.Matches(AdministeredBy, otherT.AdministeredBy)) return false;
            if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.Matches(Network, otherT.Network)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Plan, otherT.Plan)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as InsurancePlan;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(OwnedBy, otherT.OwnedBy)) return false;
            if( !DeepComparable.IsExactly(AdministeredBy, otherT.AdministeredBy)) return false;
            if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Plan, otherT.Plan)) return false;
            
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
				foreach (var elem in Type) { if (elem != null) yield return elem; }
				if (NameElement != null) yield return NameElement;
				foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
				if (Period != null) yield return Period;
				if (OwnedBy != null) yield return OwnedBy;
				if (AdministeredBy != null) yield return AdministeredBy;
				foreach (var elem in CoverageArea) { if (elem != null) yield return elem; }
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
				foreach (var elem in Network) { if (elem != null) yield return elem; }
				foreach (var elem in Coverage) { if (elem != null) yield return elem; }
				foreach (var elem in Plan) { if (elem != null) yield return elem; }
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
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
                if (OwnedBy != null) yield return new ElementValue("ownedBy", OwnedBy);
                if (AdministeredBy != null) yield return new ElementValue("administeredBy", AdministeredBy);
                foreach (var elem in CoverageArea) { if (elem != null) yield return new ElementValue("coverageArea", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                foreach (var elem in Network) { if (elem != null) yield return new ElementValue("network", elem); }
                foreach (var elem in Coverage) { if (elem != null) yield return new ElementValue("coverage", elem); }
                foreach (var elem in Plan) { if (elem != null) yield return new ElementValue("plan", elem); }
            }
        }

    }
    
}
