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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Claim, Pre-determination or Pre-authorization
    /// </summary>
    [FhirType("Claim", IsResource=true)]
    [DataContract]
    public partial class Claim : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Claim; } }
        [NotMapped]
        public override string TypeName { get { return "Claim"; } }
        
        /// <summary>
        /// The type or discipline-style of the claim.
        /// (url: http://hl7.org/fhir/ValueSet/claim-type-link)
        /// </summary>
        [FhirEnumeration("ClaimType")]
        public enum ClaimType
        {
            /// <summary>
            /// A claim for Institution based, typically in-patient, goods and services.
            /// (system: http://hl7.org/fhir/claim-type-link)
            /// </summary>
            [EnumLiteral("institutional", "http://hl7.org/fhir/claim-type-link"), Description("Institutional")]
            Institutional,
            /// <summary>
            /// A claim for Oral Health (Dentist, Denturist, Hygienist) goods and services.
            /// (system: http://hl7.org/fhir/claim-type-link)
            /// </summary>
            [EnumLiteral("oral", "http://hl7.org/fhir/claim-type-link"), Description("Oral Health")]
            Oral,
            /// <summary>
            /// A claim for Pharmacy based goods and services.
            /// (system: http://hl7.org/fhir/claim-type-link)
            /// </summary>
            [EnumLiteral("pharmacy", "http://hl7.org/fhir/claim-type-link"), Description("Pharmacy")]
            Pharmacy,
            /// <summary>
            /// A claim for Professional, typically out-patient, goods and services.
            /// (system: http://hl7.org/fhir/claim-type-link)
            /// </summary>
            [EnumLiteral("professional", "http://hl7.org/fhir/claim-type-link"), Description("Professional")]
            Professional,
            /// <summary>
            /// A claim for Vision (Ophthamologist, Optometrist and Optician) goods and services.
            /// (system: http://hl7.org/fhir/claim-type-link)
            /// </summary>
            [EnumLiteral("vision", "http://hl7.org/fhir/claim-type-link"), Description("Vision")]
            Vision,
        }

        /// <summary>
        /// Complete, proposed, exploratory, other.
        /// (url: http://hl7.org/fhir/ValueSet/claim-use-link)
        /// </summary>
        [FhirEnumeration("Use")]
        public enum Use
        {
            /// <summary>
            /// The treatment is complete and this represents a Claim for the services.
            /// (system: http://hl7.org/fhir/claim-use-link)
            /// </summary>
            [EnumLiteral("complete", "http://hl7.org/fhir/claim-use-link"), Description("Complete")]
            Complete,
            /// <summary>
            /// The treatment is proposed and this represents a Pre-authorization for the services.
            /// (system: http://hl7.org/fhir/claim-use-link)
            /// </summary>
            [EnumLiteral("proposed", "http://hl7.org/fhir/claim-use-link"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// The treatment is proposed and this represents a Pre-determination for the services.
            /// (system: http://hl7.org/fhir/claim-use-link)
            /// </summary>
            [EnumLiteral("exploratory", "http://hl7.org/fhir/claim-use-link"), Description("Exploratory")]
            Exploratory,
            /// <summary>
            /// A locally defined or otherwise resolved status.
            /// (system: http://hl7.org/fhir/claim-use-link)
            /// </summary>
            [EnumLiteral("other", "http://hl7.org/fhir/claim-use-link"), Description("Other")]
            Other,
        }

        [FhirType("PayeeComponent")]
        [DataContract]
        public partial class PayeeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PayeeComponent"; } }
            
            /// <summary>
            /// Party to be paid any benefits payable
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Provider who is the payee
            /// </summary>
            [FhirElement("provider", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Provider;
            
            /// <summary>
            /// Organization who is the payee
            /// </summary>
            [FhirElement("organization", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Organization;
            
            /// <summary>
            /// Other person who is the payee
            /// </summary>
            [FhirElement("person", InSummary=true, Order=70)]
            [CLSCompliant(false)]
			[References("Patient")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Person
            {
                get { return _Person; }
                set { _Person = value; OnPropertyChanged("Person"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Person;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayeeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                    if(Person != null) dest.Person = (Hl7.Fhir.Model.ResourceReference)Person.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PayeeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(Person, otherT.Person)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(Person, otherT.Person)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Provider != null) yield return Provider;
                    if (Organization != null) yield return Organization;
                    if (Person != null) yield return Person;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Provider != null) yield return new ElementValue("provider", Provider);
                    if (Organization != null) yield return new ElementValue("organization", Organization);
                    if (Person != null) yield return new ElementValue("person", Person);
                }
            }

            
        }
        
        
        [FhirType("DiagnosisComponent")]
        [DataContract]
        public partial class DiagnosisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DiagnosisComponent"; } }
            
            /// <summary>
            /// Sequence of diagnosis
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Sequence of diagnosis
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceElement = null; 
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Patient's list of diagnosis
            /// </summary>
            [FhirElement("diagnosis", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Diagnosis
            {
                get { return _Diagnosis; }
                set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
            }
            
            private Hl7.Fhir.Model.Coding _Diagnosis;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Diagnosis != null) dest.Diagnosis = (Hl7.Fhir.Model.Coding)Diagnosis.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Diagnosis != null) yield return Diagnosis;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Diagnosis != null) yield return new ElementValue("diagnosis", Diagnosis);
                }
            }

            
        }
        
        
        [FhirType("CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceElement = null; 
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// The focal Coverage
            /// </summary>
            [FhirElement("focal", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// The focal Coverage
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Focal
            {
                get { return FocalElement != null ? FocalElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        FocalElement = null; 
                    else
                        FocalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Focal");
                }
            }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", InSummary=true, Order=60)]
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
            /// Business agreement
            /// </summary>
            [FhirElement("businessArrangement", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BusinessArrangementElement
            {
                get { return _BusinessArrangementElement; }
                set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string BusinessArrangement
            {
                get { return BusinessArrangementElement != null ? BusinessArrangementElement.Value : null; }
                set
                {
                    if (value == null)
                        BusinessArrangementElement = null; 
                    else
                        BusinessArrangementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("BusinessArrangement");
                }
            }
            
            /// <summary>
            /// Patient relationship to subscriber
            /// </summary>
            [FhirElement("relationship", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            
            private Hl7.Fhir.Model.Coding _Relationship;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preAuthRef", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PreAuthRefElement
            {
                get { if(_PreAuthRefElement==null) _PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(); return _PreAuthRefElement; }
                set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _PreAuthRefElement;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> PreAuthRef
            {
                get { return PreAuthRefElement != null ? PreAuthRefElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PreAuthRefElement = null; 
                    else
                        PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("PreAuthRef");
                }
            }
            
            /// <summary>
            /// Adjudication results
            /// </summary>
            [FhirElement("claimResponse", InSummary=true, Order=100)]
            [CLSCompliant(false)]
			[References("ClaimResponse")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ClaimResponse
            {
                get { return _ClaimResponse; }
                set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
            
            /// <summary>
            /// Original version
            /// </summary>
            [FhirElement("originalRuleset", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Coding OriginalRuleset
            {
                get { return _OriginalRuleset; }
                set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
            }
            
            private Hl7.Fhir.Model.Coding _OriginalRuleset;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.Coding)Relationship.DeepCopy();
                    if(PreAuthRefElement != null) dest.PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(PreAuthRefElement.DeepCopy());
                    if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
                    if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
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
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
                if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
                if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (FocalElement != null) yield return FocalElement;
                    if (Coverage != null) yield return Coverage;
                    if (BusinessArrangementElement != null) yield return BusinessArrangementElement;
                    if (Relationship != null) yield return Relationship;
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return elem; }
                    if (ClaimResponse != null) yield return ClaimResponse;
                    if (OriginalRuleset != null) yield return OriginalRuleset;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (FocalElement != null) yield return new ElementValue("focal", FocalElement);
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    if (BusinessArrangementElement != null) yield return new ElementValue("businessArrangement", BusinessArrangementElement);
                    if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return new ElementValue("preAuthRef", elem); }
                    if (ClaimResponse != null) yield return new ElementValue("claimResponse", ClaimResponse);
                    if (OriginalRuleset != null) yield return new ElementValue("originalRuleset", OriginalRuleset);
                }
            }

            
        }
        
        
        [FhirType("ItemsComponent")]
        [DataContract]
        public partial class ItemsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ItemsComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceElement = null; 
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Group or type of product or service
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
            
            /// <summary>
            /// Responsible practitioner
            /// </summary>
            [FhirElement("provider", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Provider;
            
            /// <summary>
            /// Diagnosis Link
            /// </summary>
            [FhirElement("diagnosisLinkId", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> DiagnosisLinkIdElement
            {
                get { if(_DiagnosisLinkIdElement==null) _DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _DiagnosisLinkIdElement; }
                set { _DiagnosisLinkIdElement = value; OnPropertyChanged("DiagnosisLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _DiagnosisLinkIdElement;
            
            /// <summary>
            /// Diagnosis Link
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> DiagnosisLinkId
            {
                get { return DiagnosisLinkIdElement != null ? DiagnosisLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        DiagnosisLinkIdElement = null; 
                    else
                        DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("DiagnosisLinkId");
                }
            }
            
            /// <summary>
            /// Item Code
            /// </summary>
            [FhirElement("service", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Date of Service
            /// </summary>
            [FhirElement("serviceDate", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Date ServiceDateElement
            {
                get { return _ServiceDateElement; }
                set { _ServiceDateElement = value; OnPropertyChanged("ServiceDateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _ServiceDateElement;
            
            /// <summary>
            /// Date of Service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ServiceDate
            {
                get { return ServiceDateElement != null ? ServiceDateElement.Value : null; }
                set
                {
                    if (value == null)
                        ServiceDateElement = null; 
                    else
                        ServiceDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("ServiceDate");
                }
            }
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        FactorElement = null; 
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            [FhirElement("points", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PointsElement = null; 
                    else
                        PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("net", InSummary=true, Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Udi
            {
                get { return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private Hl7.Fhir.Model.Coding _Udi;
            
            /// <summary>
            /// Service Location
            /// </summary>
            [FhirElement("bodySite", InSummary=true, Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.Coding BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.Coding _BodySite;
            
            /// <summary>
            /// Service Sub-location
            /// </summary>
            [FhirElement("subSite", InSummary=true, Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> SubSite
            {
                get { if(_SubSite==null) _SubSite = new List<Hl7.Fhir.Model.Coding>(); return _SubSite; }
                set { _SubSite = value; OnPropertyChanged("SubSite"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _SubSite;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", InSummary=true, Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.Coding>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Modifier;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=190)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Claim.DetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.Claim.DetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.Claim.DetailComponent> _Detail;
            
            /// <summary>
            /// Prosthetic details
            /// </summary>
            [FhirElement("prosthesis", InSummary=true, Order=200)]
            [DataMember]
            public Hl7.Fhir.Model.Claim.ProsthesisComponent Prosthesis
            {
                get { return _Prosthesis; }
                set { _Prosthesis = value; OnPropertyChanged("Prosthesis"); }
            }
            
            private Hl7.Fhir.Model.Claim.ProsthesisComponent _Prosthesis;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                    if(DiagnosisLinkIdElement != null) dest.DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(DiagnosisLinkIdElement.DeepCopy());
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(ServiceDateElement != null) dest.ServiceDateElement = (Hl7.Fhir.Model.Date)ServiceDateElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = (Hl7.Fhir.Model.Coding)Udi.DeepCopy();
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
                    if(SubSite != null) dest.SubSite = new List<Hl7.Fhir.Model.Coding>(SubSite.DeepCopy());
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.Coding>(Modifier.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.Claim.DetailComponent>(Detail.DeepCopy());
                    if(Prosthesis != null) dest.Prosthesis = (Hl7.Fhir.Model.Claim.ProsthesisComponent)Prosthesis.DeepCopy();
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
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(ServiceDateElement, otherT.ServiceDateElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(Prosthesis, otherT.Prosthesis)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(ServiceDateElement, otherT.ServiceDateElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(Prosthesis, otherT.Prosthesis)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Type != null) yield return Type;
                    if (Provider != null) yield return Provider;
                    foreach (var elem in DiagnosisLinkIdElement) { if (elem != null) yield return elem; }
                    if (Service != null) yield return Service;
                    if (ServiceDateElement != null) yield return ServiceDateElement;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (PointsElement != null) yield return PointsElement;
                    if (Net != null) yield return Net;
                    if (Udi != null) yield return Udi;
                    if (BodySite != null) yield return BodySite;
                    foreach (var elem in SubSite) { if (elem != null) yield return elem; }
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                    if (Prosthesis != null) yield return Prosthesis;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Provider != null) yield return new ElementValue("provider", Provider);
                    foreach (var elem in DiagnosisLinkIdElement) { if (elem != null) yield return new ElementValue("diagnosisLinkId", elem); }
                    if (Service != null) yield return new ElementValue("service", Service);
                    if (ServiceDateElement != null) yield return new ElementValue("serviceDate", ServiceDateElement);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (PointsElement != null) yield return new ElementValue("points", PointsElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    if (Udi != null) yield return new ElementValue("udi", Udi);
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    foreach (var elem in SubSite) { if (elem != null) yield return new ElementValue("subSite", elem); }
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                    if (Prosthesis != null) yield return new ElementValue("prosthesis", Prosthesis);
                }
            }

            
        }
        
        
        [FhirType("DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceElement = null; 
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Group or type of product or service
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
            
            /// <summary>
            /// Additional item codes
            /// </summary>
            [FhirElement("service", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        FactorElement = null; 
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            [FhirElement("points", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PointsElement = null; 
                    else
                        PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Total additional item cost
            /// </summary>
            [FhirElement("net", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Udi
            {
                get { return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private Hl7.Fhir.Model.Coding _Udi;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("subDetail", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Claim.SubDetailComponent> SubDetail
            {
                get { if(_SubDetail==null) _SubDetail = new List<Hl7.Fhir.Model.Claim.SubDetailComponent>(); return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            
            private List<Hl7.Fhir.Model.Claim.SubDetailComponent> _SubDetail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = (Hl7.Fhir.Model.Coding)Udi.DeepCopy();
                    if(SubDetail != null) dest.SubDetail = new List<Hl7.Fhir.Model.Claim.SubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Type != null) yield return Type;
                    if (Service != null) yield return Service;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (PointsElement != null) yield return PointsElement;
                    if (Net != null) yield return Net;
                    if (Udi != null) yield return Udi;
                    foreach (var elem in SubDetail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Service != null) yield return new ElementValue("service", Service);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (PointsElement != null) yield return new ElementValue("points", PointsElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    if (Udi != null) yield return new ElementValue("udi", Udi);
                    foreach (var elem in SubDetail) { if (elem != null) yield return new ElementValue("subDetail", elem); }
                }
            }

            
        }
        
        
        [FhirType("SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SubDetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceElement = null; 
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Type of product or service
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
            
            /// <summary>
            /// Additional item codes
            /// </summary>
            [FhirElement("service", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        FactorElement = null; 
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            [FhirElement("points", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PointsElement = null; 
                    else
                        PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Net additional item cost
            /// </summary>
            [FhirElement("net", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Udi
            {
                get { return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private Hl7.Fhir.Model.Coding _Udi;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = (Hl7.Fhir.Model.Coding)Udi.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Type != null) yield return Type;
                    if (Service != null) yield return Service;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (PointsElement != null) yield return PointsElement;
                    if (Net != null) yield return Net;
                    if (Udi != null) yield return Udi;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Service != null) yield return new ElementValue("service", Service);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (PointsElement != null) yield return new ElementValue("points", PointsElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    if (Udi != null) yield return new ElementValue("udi", Udi);
                }
            }

            
        }
        
        
        [FhirType("ProsthesisComponent")]
        [DataContract]
        public partial class ProsthesisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ProsthesisComponent"; } }
            
            /// <summary>
            /// Is this the initial service
            /// </summary>
            [FhirElement("initial", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean InitialElement
            {
                get { return _InitialElement; }
                set { _InitialElement = value; OnPropertyChanged("InitialElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _InitialElement;
            
            /// <summary>
            /// Is this the initial service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Initial
            {
                get { return InitialElement != null ? InitialElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        InitialElement = null; 
                    else
                        InitialElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Initial");
                }
            }
            
            /// <summary>
            /// Initial service Date
            /// </summary>
            [FhirElement("priorDate", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Date PriorDateElement
            {
                get { return _PriorDateElement; }
                set { _PriorDateElement = value; OnPropertyChanged("PriorDateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _PriorDateElement;
            
            /// <summary>
            /// Initial service Date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PriorDate
            {
                get { return PriorDateElement != null ? PriorDateElement.Value : null; }
                set
                {
                    if (value == null)
                        PriorDateElement = null; 
                    else
                        PriorDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("PriorDate");
                }
            }
            
            /// <summary>
            /// Prosthetic Material
            /// </summary>
            [FhirElement("priorMaterial", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding PriorMaterial
            {
                get { return _PriorMaterial; }
                set { _PriorMaterial = value; OnPropertyChanged("PriorMaterial"); }
            }
            
            private Hl7.Fhir.Model.Coding _PriorMaterial;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProsthesisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(InitialElement != null) dest.InitialElement = (Hl7.Fhir.Model.FhirBoolean)InitialElement.DeepCopy();
                    if(PriorDateElement != null) dest.PriorDateElement = (Hl7.Fhir.Model.Date)PriorDateElement.DeepCopy();
                    if(PriorMaterial != null) dest.PriorMaterial = (Hl7.Fhir.Model.Coding)PriorMaterial.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProsthesisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProsthesisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(InitialElement, otherT.InitialElement)) return false;
                if( !DeepComparable.Matches(PriorDateElement, otherT.PriorDateElement)) return false;
                if( !DeepComparable.Matches(PriorMaterial, otherT.PriorMaterial)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProsthesisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(InitialElement, otherT.InitialElement)) return false;
                if( !DeepComparable.IsExactly(PriorDateElement, otherT.PriorDateElement)) return false;
                if( !DeepComparable.IsExactly(PriorMaterial, otherT.PriorMaterial)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (InitialElement != null) yield return InitialElement;
                    if (PriorDateElement != null) yield return PriorDateElement;
                    if (PriorMaterial != null) yield return PriorMaterial;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (InitialElement != null) yield return new ElementValue("initial", InitialElement);
                    if (PriorDateElement != null) yield return new ElementValue("priorDate", PriorDateElement);
                    if (PriorMaterial != null) yield return new ElementValue("priorMaterial", PriorMaterial);
                }
            }

            
        }
        
        
        [FhirType("MissingTeethComponent")]
        [DataContract]
        public partial class MissingTeethComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "MissingTeethComponent"; } }
            
            /// <summary>
            /// Tooth Code
            /// </summary>
            [FhirElement("tooth", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Tooth
            {
                get { return _Tooth; }
                set { _Tooth = value; OnPropertyChanged("Tooth"); }
            }
            
            private Hl7.Fhir.Model.Coding _Tooth;
            
            /// <summary>
            /// Reason for missing
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.Coding _Reason;
            
            /// <summary>
            /// Date of Extraction
            /// </summary>
            [FhirElement("extractionDate", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Date ExtractionDateElement
            {
                get { return _ExtractionDateElement; }
                set { _ExtractionDateElement = value; OnPropertyChanged("ExtractionDateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _ExtractionDateElement;
            
            /// <summary>
            /// Date of Extraction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ExtractionDate
            {
                get { return ExtractionDateElement != null ? ExtractionDateElement.Value : null; }
                set
                {
                    if (value == null)
                        ExtractionDateElement = null; 
                    else
                        ExtractionDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("ExtractionDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MissingTeethComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Tooth != null) dest.Tooth = (Hl7.Fhir.Model.Coding)Tooth.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Coding)Reason.DeepCopy();
                    if(ExtractionDateElement != null) dest.ExtractionDateElement = (Hl7.Fhir.Model.Date)ExtractionDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MissingTeethComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MissingTeethComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Tooth, otherT.Tooth)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ExtractionDateElement, otherT.ExtractionDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MissingTeethComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Tooth, otherT.Tooth)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ExtractionDateElement, otherT.ExtractionDateElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Tooth != null) yield return Tooth;
                    if (Reason != null) yield return Reason;
                    if (ExtractionDateElement != null) yield return ExtractionDateElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Tooth != null) yield return new ElementValue("tooth", Tooth);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                    if (ExtractionDateElement != null) yield return new ElementValue("extractionDate", ExtractionDateElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// institutional | oral | pharmacy | professional | vision
        /// </summary>
        [FhirElement("type", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Claim.ClaimType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Claim.ClaimType> _TypeElement;
        
        /// <summary>
        /// institutional | oral | pharmacy | professional | vision
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Claim.ClaimType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Claim.ClaimType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Claim number
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Current specification followed
        /// </summary>
        [FhirElement("ruleset", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Ruleset
        {
            get { return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _Ruleset;
        
        /// <summary>
        /// Original specification followed
        /// </summary>
        [FhirElement("originalRuleset", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Coding OriginalRuleset
        {
            get { return _OriginalRuleset; }
            set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _OriginalRuleset;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", InSummary=true, Order=130)]
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
        /// Insurer
        /// </summary>
        [FhirElement("target", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Target;
        
        /// <summary>
        /// Responsible provider
        /// </summary>
        [FhirElement("provider", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Provider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("organization", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        [FhirElement("use", InSummary=true, Order=170)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Claim.Use> Use_Element
        {
            get { return _Use_Element; }
            set { _Use_Element = value; OnPropertyChanged("Use_Element"); }
        }
        
        private Code<Hl7.Fhir.Model.Claim.Use> _Use_Element;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Claim.Use? Use_
        {
            get { return Use_Element != null ? Use_Element.Value : null; }
            set
            {
                if (!value.HasValue)
                  Use_Element = null; 
                else
                  Use_Element = new Code<Hl7.Fhir.Model.Claim.Use>(value);
                OnPropertyChanged("Use_");
            }
        }
        
        /// <summary>
        /// Desired processing priority
        /// </summary>
        [FhirElement("priority", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.Coding _Priority;
        
        /// <summary>
        /// Funds requested to be reserved
        /// </summary>
        [FhirElement("fundsReserve", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Coding FundsReserve
        {
            get { return _FundsReserve; }
            set { _FundsReserve = value; OnPropertyChanged("FundsReserve"); }
        }
        
        private Hl7.Fhir.Model.Coding _FundsReserve;
        
        /// <summary>
        /// Author
        /// </summary>
        [FhirElement("enterer", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", InSummary=true, Order=210)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Facility
        {
            get { return _Facility; }
            set { _Facility = value; OnPropertyChanged("Facility"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Facility;
        
        /// <summary>
        /// Prescription
        /// </summary>
        [FhirElement("prescription", InSummary=true, Order=220)]
        [CLSCompliant(false)]
		[References("MedicationOrder","VisionPrescription")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescription
        {
            get { return _Prescription; }
            set { _Prescription = value; OnPropertyChanged("Prescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Prescription;
        
        /// <summary>
        /// Original Prescription
        /// </summary>
        [FhirElement("originalPrescription", InSummary=true, Order=230)]
        [CLSCompliant(false)]
		[References("MedicationOrder")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OriginalPrescription
        {
            get { return _OriginalPrescription; }
            set { _OriginalPrescription = value; OnPropertyChanged("OriginalPrescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OriginalPrescription;
        
        /// <summary>
        /// Payee
        /// </summary>
        [FhirElement("payee", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Claim.PayeeComponent Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged("Payee"); }
        }
        
        private Hl7.Fhir.Model.Claim.PayeeComponent _Payee;
        
        /// <summary>
        /// Treatment Referral
        /// </summary>
        [FhirElement("referral", InSummary=true, Order=250)]
        [CLSCompliant(false)]
		[References("ReferralRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referral
        {
            get { return _Referral; }
            set { _Referral = value; OnPropertyChanged("Referral"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Referral;
        
        /// <summary>
        /// Diagnosis
        /// </summary>
        [FhirElement("diagnosis", InSummary=true, Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.DiagnosisComponent> Diagnosis
        {
            get { if(_Diagnosis==null) _Diagnosis = new List<Hl7.Fhir.Model.Claim.DiagnosisComponent>(); return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.DiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// List of presenting Conditions
        /// </summary>
        [FhirElement("condition", InSummary=true, Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Condition
        {
            get { if(_Condition==null) _Condition = new List<Hl7.Fhir.Model.Coding>(); return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Condition;
        
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=280)]
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
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("coverage", InSummary=true, Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<Hl7.Fhir.Model.Claim.CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.CoverageComponent> _Coverage;
        
        /// <summary>
        /// Eligibility exceptions
        /// </summary>
        [FhirElement("exception", InSummary=true, Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Exception
        {
            get { if(_Exception==null) _Exception = new List<Hl7.Fhir.Model.Coding>(); return _Exception; }
            set { _Exception = value; OnPropertyChanged("Exception"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Exception;
        
        /// <summary>
        /// Name of School
        /// </summary>
        [FhirElement("school", InSummary=true, Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SchoolElement
        {
            get { return _SchoolElement; }
            set { _SchoolElement = value; OnPropertyChanged("SchoolElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SchoolElement;
        
        /// <summary>
        /// Name of School
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string School
        {
            get { return SchoolElement != null ? SchoolElement.Value : null; }
            set
            {
                if (value == null)
                  SchoolElement = null; 
                else
                  SchoolElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("School");
            }
        }
        
        /// <summary>
        /// Accident Date
        /// </summary>
        [FhirElement("accident", InSummary=true, Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.Date AccidentElement
        {
            get { return _AccidentElement; }
            set { _AccidentElement = value; OnPropertyChanged("AccidentElement"); }
        }
        
        private Hl7.Fhir.Model.Date _AccidentElement;
        
        /// <summary>
        /// Accident Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Accident
        {
            get { return AccidentElement != null ? AccidentElement.Value : null; }
            set
            {
                if (value == null)
                  AccidentElement = null; 
                else
                  AccidentElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("Accident");
            }
        }
        
        /// <summary>
        /// Accident Type
        /// </summary>
        [FhirElement("accidentType", InSummary=true, Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.Coding AccidentType
        {
            get { return _AccidentType; }
            set { _AccidentType = value; OnPropertyChanged("AccidentType"); }
        }
        
        private Hl7.Fhir.Model.Coding _AccidentType;
        
        /// <summary>
        /// Intervention and exception code (Pharma)
        /// </summary>
        [FhirElement("interventionException", InSummary=true, Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> InterventionException
        {
            get { if(_InterventionException==null) _InterventionException = new List<Hl7.Fhir.Model.Coding>(); return _InterventionException; }
            set { _InterventionException = value; OnPropertyChanged("InterventionException"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _InterventionException;
        
        /// <summary>
        /// Goods and Services
        /// </summary>
        [FhirElement("item", InSummary=true, Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.ItemsComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.Claim.ItemsComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.ItemsComponent> _Item;
        
        /// <summary>
        /// Additional materials, documents, etc.
        /// </summary>
        [FhirElement("additionalMaterials", InSummary=true, Order=360)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> AdditionalMaterials
        {
            get { if(_AdditionalMaterials==null) _AdditionalMaterials = new List<Hl7.Fhir.Model.Coding>(); return _AdditionalMaterials; }
            set { _AdditionalMaterials = value; OnPropertyChanged("AdditionalMaterials"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _AdditionalMaterials;
        
        /// <summary>
        /// Only if type = oral
        /// </summary>
        [FhirElement("missingTeeth", InSummary=true, Order=370)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.MissingTeethComponent> MissingTeeth
        {
            get { if(_MissingTeeth==null) _MissingTeeth = new List<Hl7.Fhir.Model.Claim.MissingTeethComponent>(); return _MissingTeeth; }
            set { _MissingTeeth = value; OnPropertyChanged("MissingTeeth"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.MissingTeethComponent> _MissingTeeth;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Claim;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Claim.ClaimType>)TypeElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.Coding)Ruleset.DeepCopy();
                if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(Use_Element != null) dest.Use_Element = (Code<Hl7.Fhir.Model.Claim.Use>)Use_Element.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.Coding)Priority.DeepCopy();
                if(FundsReserve != null) dest.FundsReserve = (Hl7.Fhir.Model.Coding)FundsReserve.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.ResourceReference)Facility.DeepCopy();
                if(Prescription != null) dest.Prescription = (Hl7.Fhir.Model.ResourceReference)Prescription.DeepCopy();
                if(OriginalPrescription != null) dest.OriginalPrescription = (Hl7.Fhir.Model.ResourceReference)OriginalPrescription.DeepCopy();
                if(Payee != null) dest.Payee = (Hl7.Fhir.Model.Claim.PayeeComponent)Payee.DeepCopy();
                if(Referral != null) dest.Referral = (Hl7.Fhir.Model.ResourceReference)Referral.DeepCopy();
                if(Diagnosis != null) dest.Diagnosis = new List<Hl7.Fhir.Model.Claim.DiagnosisComponent>(Diagnosis.DeepCopy());
                if(Condition != null) dest.Condition = new List<Hl7.Fhir.Model.Coding>(Condition.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.Claim.CoverageComponent>(Coverage.DeepCopy());
                if(Exception != null) dest.Exception = new List<Hl7.Fhir.Model.Coding>(Exception.DeepCopy());
                if(SchoolElement != null) dest.SchoolElement = (Hl7.Fhir.Model.FhirString)SchoolElement.DeepCopy();
                if(AccidentElement != null) dest.AccidentElement = (Hl7.Fhir.Model.Date)AccidentElement.DeepCopy();
                if(AccidentType != null) dest.AccidentType = (Hl7.Fhir.Model.Coding)AccidentType.DeepCopy();
                if(InterventionException != null) dest.InterventionException = new List<Hl7.Fhir.Model.Coding>(InterventionException.DeepCopy());
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.Claim.ItemsComponent>(Item.DeepCopy());
                if(AdditionalMaterials != null) dest.AdditionalMaterials = new List<Hl7.Fhir.Model.Coding>(AdditionalMaterials.DeepCopy());
                if(MissingTeeth != null) dest.MissingTeeth = new List<Hl7.Fhir.Model.Claim.MissingTeethComponent>(MissingTeeth.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Claim());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Claim;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.Matches(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
            if( !DeepComparable.Matches(Referral, otherT.Referral)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Exception, otherT.Exception)) return false;
            if( !DeepComparable.Matches(SchoolElement, otherT.SchoolElement)) return false;
            if( !DeepComparable.Matches(AccidentElement, otherT.AccidentElement)) return false;
            if( !DeepComparable.Matches(AccidentType, otherT.AccidentType)) return false;
            if( !DeepComparable.Matches(InterventionException, otherT.InterventionException)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(AdditionalMaterials, otherT.AdditionalMaterials)) return false;
            if( !DeepComparable.Matches(MissingTeeth, otherT.MissingTeeth)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Claim;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.IsExactly(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
            if( !DeepComparable.IsExactly(Referral, otherT.Referral)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Exception, otherT.Exception)) return false;
            if( !DeepComparable.IsExactly(SchoolElement, otherT.SchoolElement)) return false;
            if( !DeepComparable.IsExactly(AccidentElement, otherT.AccidentElement)) return false;
            if( !DeepComparable.IsExactly(AccidentType, otherT.AccidentType)) return false;
            if( !DeepComparable.IsExactly(InterventionException, otherT.InterventionException)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(AdditionalMaterials, otherT.AdditionalMaterials)) return false;
            if( !DeepComparable.IsExactly(MissingTeeth, otherT.MissingTeeth)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (TypeElement != null) yield return TypeElement;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (Ruleset != null) yield return Ruleset;
				if (OriginalRuleset != null) yield return OriginalRuleset;
				if (CreatedElement != null) yield return CreatedElement;
				if (Target != null) yield return Target;
				if (Provider != null) yield return Provider;
				if (Organization != null) yield return Organization;
				if (Use_Element != null) yield return Use_Element;
				if (Priority != null) yield return Priority;
				if (FundsReserve != null) yield return FundsReserve;
				if (Enterer != null) yield return Enterer;
				if (Facility != null) yield return Facility;
				if (Prescription != null) yield return Prescription;
				if (OriginalPrescription != null) yield return OriginalPrescription;
				if (Payee != null) yield return Payee;
				if (Referral != null) yield return Referral;
				foreach (var elem in Diagnosis) { if (elem != null) yield return elem; }
				foreach (var elem in Condition) { if (elem != null) yield return elem; }
				if (Patient != null) yield return Patient;
				foreach (var elem in Coverage) { if (elem != null) yield return elem; }
				foreach (var elem in Exception) { if (elem != null) yield return elem; }
				if (SchoolElement != null) yield return SchoolElement;
				if (AccidentElement != null) yield return AccidentElement;
				if (AccidentType != null) yield return AccidentType;
				foreach (var elem in InterventionException) { if (elem != null) yield return elem; }
				foreach (var elem in Item) { if (elem != null) yield return elem; }
				foreach (var elem in AdditionalMaterials) { if (elem != null) yield return elem; }
				foreach (var elem in MissingTeeth) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Ruleset != null) yield return new ElementValue("ruleset", Ruleset);
                if (OriginalRuleset != null) yield return new ElementValue("originalRuleset", OriginalRuleset);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Target != null) yield return new ElementValue("target", Target);
                if (Provider != null) yield return new ElementValue("provider", Provider);
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (Use_Element != null) yield return new ElementValue("use", Use_Element);
                if (Priority != null) yield return new ElementValue("priority", Priority);
                if (FundsReserve != null) yield return new ElementValue("fundsReserve", FundsReserve);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (Facility != null) yield return new ElementValue("facility", Facility);
                if (Prescription != null) yield return new ElementValue("prescription", Prescription);
                if (OriginalPrescription != null) yield return new ElementValue("originalPrescription", OriginalPrescription);
                if (Payee != null) yield return new ElementValue("payee", Payee);
                if (Referral != null) yield return new ElementValue("referral", Referral);
                foreach (var elem in Diagnosis) { if (elem != null) yield return new ElementValue("diagnosis", elem); }
                foreach (var elem in Condition) { if (elem != null) yield return new ElementValue("condition", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                foreach (var elem in Coverage) { if (elem != null) yield return new ElementValue("coverage", elem); }
                foreach (var elem in Exception) { if (elem != null) yield return new ElementValue("exception", elem); }
                if (SchoolElement != null) yield return new ElementValue("school", SchoolElement);
                if (AccidentElement != null) yield return new ElementValue("accident", AccidentElement);
                if (AccidentType != null) yield return new ElementValue("accidentType", AccidentType);
                foreach (var elem in InterventionException) { if (elem != null) yield return new ElementValue("interventionException", elem); }
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                foreach (var elem in AdditionalMaterials) { if (elem != null) yield return new ElementValue("additionalMaterials", elem); }
                foreach (var elem in MissingTeeth) { if (elem != null) yield return new ElementValue("missingTeeth", elem); }
            }
        }

    }
    
}
