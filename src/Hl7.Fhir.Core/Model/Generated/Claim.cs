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
        /// Complete, proposed, exploratory, other
        /// (url: http://hl7.org/fhir/ValueSet/claim-use)
        /// </summary>
        [FhirEnumeration("Use")]
        public enum Use
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("complete", "http://hl7.org/fhir/claim-use"), Description("Complete")]
            Complete,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("proposed", "http://hl7.org/fhir/claim-use"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("exploratory", "http://hl7.org/fhir/claim-use"), Description("Exploratory")]
            Exploratory,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("other", "http://hl7.org/fhir/claim-use"), Description("Other")]
            Other,
        }

        [FhirType("RelatedClaimComponent")]
        [DataContract]
        public partial class RelatedClaimComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedClaimComponent"; } }
            
            /// <summary>
            /// Reference to the related claim
            /// </summary>
            [FhirElement("claim", Order=40)]
            [CLSCompliant(false)]
			[References("Claim")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Claim
            {
                get { return _Claim; }
                set { _Claim = value; OnPropertyChanged("Claim"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Claim;
            
            /// <summary>
            /// How the reference claim is related
            /// </summary>
            [FhirElement("relationship", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Relationship;
            
            /// <summary>
            /// Related file or case reference
            /// </summary>
            [FhirElement("reference", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Reference;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedClaimComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Claim != null) dest.Claim = (Hl7.Fhir.Model.ResourceReference)Claim.DeepCopy();
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.Identifier)Reference.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RelatedClaimComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedClaimComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Claim, otherT.Claim)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedClaimComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Claim, otherT.Claim)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Claim != null) yield return Claim;
                    if (Relationship != null) yield return Relationship;
                    if (Reference != null) yield return Reference;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Claim != null) yield return new ElementValue("claim", Claim);
                    if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                }
            }

            
        }
        
        
        [FhirType("PayeeComponent")]
        [DataContract]
        public partial class PayeeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PayeeComponent"; } }
            
            /// <summary>
            /// Type of party: Subscriber, Provider, other
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
            /// organization | patient | practitioner | relatedperson
            /// </summary>
            [FhirElement("resourceType", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding ResourceType
            {
                get { return _ResourceType; }
                set { _ResourceType = value; OnPropertyChanged("ResourceType"); }
            }
            
            private Hl7.Fhir.Model.Coding _ResourceType;
            
            /// <summary>
            /// Party to receive the payable
            /// </summary>
            [FhirElement("party", Order=60)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization","Patient","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayeeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(ResourceType != null) dest.ResourceType = (Hl7.Fhir.Model.Coding)ResourceType.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
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
                if( !DeepComparable.Matches(ResourceType, otherT.ResourceType)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ResourceType, otherT.ResourceType)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (ResourceType != null) yield return ResourceType;
                    if (Party != null) yield return Party;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (ResourceType != null) yield return new ElementValue("resourceType", ResourceType);
                    if (Party != null) yield return new ElementValue("party", Party);
                }
            }

            
        }
        
        
        [FhirType("CareTeamComponent")]
        [DataContract]
        public partial class CareTeamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "CareTeamComponent"; } }
            
            /// <summary>
            /// Number to covey order of careTeam
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Number to covey order of careTeam
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
            /// Provider individual or organization
            /// </summary>
            [FhirElement("provider", Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Provider;
            
            /// <summary>
            /// Billing provider
            /// </summary>
            [FhirElement("responsible", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ResponsibleElement
            {
                get { return _ResponsibleElement; }
                set { _ResponsibleElement = value; OnPropertyChanged("ResponsibleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ResponsibleElement;
            
            /// <summary>
            /// Billing provider
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Responsible
            {
                get { return ResponsibleElement != null ? ResponsibleElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ResponsibleElement = null; 
                    else
                        ResponsibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Responsible");
                }
            }
            
            /// <summary>
            /// Role on the team
            /// </summary>
            [FhirElement("role", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Type, classification or Specialization
            /// </summary>
            [FhirElement("qualification", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Qualification
            {
                get { return _Qualification; }
                set { _Qualification = value; OnPropertyChanged("Qualification"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Qualification;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CareTeamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                    if(ResponsibleElement != null) dest.ResponsibleElement = (Hl7.Fhir.Model.FhirBoolean)ResponsibleElement.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Qualification != null) dest.Qualification = (Hl7.Fhir.Model.CodeableConcept)Qualification.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CareTeamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(ResponsibleElement, otherT.ResponsibleElement)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Qualification, otherT.Qualification)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(ResponsibleElement, otherT.ResponsibleElement)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Qualification, otherT.Qualification)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Provider != null) yield return Provider;
                    if (ResponsibleElement != null) yield return ResponsibleElement;
                    if (Role != null) yield return Role;
                    if (Qualification != null) yield return Qualification;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Provider != null) yield return new ElementValue("provider", Provider);
                    if (ResponsibleElement != null) yield return new ElementValue("responsible", ResponsibleElement);
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Qualification != null) yield return new ElementValue("qualification", Qualification);
                }
            }

            
        }
        
        
        [FhirType("SpecialConditionComponent")]
        [DataContract]
        public partial class SpecialConditionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SpecialConditionComponent"; } }
            
            /// <summary>
            /// Information instance identifier
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Information instance identifier
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
            /// General class of information
            /// </summary>
            [FhirElement("category", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Type of information
            /// </summary>
            [FhirElement("code", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// When it occurred
            /// </summary>
            [FhirElement("timing", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Additional Data or supporting information
            /// </summary>
            [FhirElement("value", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Quantity),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Reason associated with the information
            /// </summary>
            [FhirElement("reason", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecialConditionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecialConditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecialConditionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecialConditionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Category != null) yield return Category;
                    if (Code != null) yield return Code;
                    if (Timing != null) yield return Timing;
                    if (Value != null) yield return Value;
                    if (Reason != null) yield return Reason;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Timing != null) yield return new ElementValue("timing", Timing);
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
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
            /// Number to covey order of diagnosis
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Number to covey order of diagnosis
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
            /// Patient's diagnosis
            /// </summary>
            [FhirElement("diagnosis", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Diagnosis
            {
                get { return _Diagnosis; }
                set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
            }
            
            private Hl7.Fhir.Model.Element _Diagnosis;
            
            /// <summary>
            /// Timing or nature of the diagnosis
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Package billing code
            /// </summary>
            [FhirElement("packageCode", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept PackageCode
            {
                get { return _PackageCode; }
                set { _PackageCode = value; OnPropertyChanged("PackageCode"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _PackageCode;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Diagnosis != null) dest.Diagnosis = (Hl7.Fhir.Model.Element)Diagnosis.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(PackageCode != null) dest.PackageCode = (Hl7.Fhir.Model.CodeableConcept)PackageCode.DeepCopy();
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
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(PackageCode, otherT.PackageCode)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(PackageCode, otherT.PackageCode)) return false;
                
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
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    if (PackageCode != null) yield return PackageCode;
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
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    if (PackageCode != null) yield return new ElementValue("packageCode", PackageCode);
                }
            }

            
        }
        
        
        [FhirType("ProcedureComponent")]
        [DataContract]
        public partial class ProcedureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureComponent"; } }
            
            /// <summary>
            /// Procedure sequence for reference
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Procedure sequence for reference
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
            /// When the procedure was performed
            /// </summary>
            [FhirElement("date", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When the procedure was performed
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
            /// Patient's list of procedures performed
            /// </summary>
            [FhirElement("procedure", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Procedure
            {
                get { return _Procedure; }
                set { _Procedure = value; OnPropertyChanged("Procedure"); }
            }
            
            private Hl7.Fhir.Model.Element _Procedure;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.Element)Procedure.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (DateElement != null) yield return DateElement;
                    if (Procedure != null) yield return Procedure;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Procedure != null) yield return new ElementValue("procedure", Procedure);
                }
            }

            
        }
        
        
        [FhirType("InsuranceComponent")]
        [DataContract]
        public partial class InsuranceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "InsuranceComponent"; } }
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            [FhirElement("sequence", Order=40)]
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
            /// Is the focal Coverage
            /// </summary>
            [FhirElement("focal", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// Is the focal Coverage
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
            [FhirElement("coverage", Order=60)]
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
            [FhirElement("businessArrangement", Order=70)]
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
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preAuthRef", Order=80)]
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
            [FhirElement("claimResponse", Order=90)]
            [CLSCompliant(false)]
			[References("ClaimResponse")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ClaimResponse
            {
                get { return _ClaimResponse; }
                set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InsuranceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(PreAuthRefElement != null) dest.PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(PreAuthRefElement.DeepCopy());
                    if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
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
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
                
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
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return elem; }
                    if (ClaimResponse != null) yield return ClaimResponse;
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
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return new ElementValue("preAuthRef", elem); }
                    if (ClaimResponse != null) yield return new ElementValue("claimResponse", ClaimResponse);
                }
            }

            
        }
        
        
        [FhirType("AccidentComponent")]
        [DataContract]
        public partial class AccidentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AccidentComponent"; } }
            
            /// <summary>
            /// When the accident occurred
            /// see information codes
            /// see information codes
            /// </summary>
            [FhirElement("date", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// When the accident occurred
        /// see information codes
        /// see information codes
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
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// The nature of the accident
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
            /// Accident Place
            /// </summary>
            [FhirElement("location", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AccidentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AccidentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AccidentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AccidentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DateElement != null) yield return DateElement;
                    if (Type != null) yield return Type;
                    if (Location != null) yield return Location;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Location != null) yield return new ElementValue("location", Location);
                }
            }

            
        }
        
        
        [FhirType("ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
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
            /// Applicable careTeam members
            /// </summary>
            [FhirElement("careTeamLinkId", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> CareTeamLinkIdElement
            {
                get { if(_CareTeamLinkIdElement==null) _CareTeamLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _CareTeamLinkIdElement; }
                set { _CareTeamLinkIdElement = value; OnPropertyChanged("CareTeamLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _CareTeamLinkIdElement;
            
            /// <summary>
            /// Applicable careTeam members
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> CareTeamLinkId
            {
                get { return CareTeamLinkIdElement != null ? CareTeamLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        CareTeamLinkIdElement = null; 
                    else
                        CareTeamLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("CareTeamLinkId");
                }
            }
            
            /// <summary>
            /// Applicable diagnoses
            /// </summary>
            [FhirElement("diagnosisLinkId", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> DiagnosisLinkIdElement
            {
                get { if(_DiagnosisLinkIdElement==null) _DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _DiagnosisLinkIdElement; }
                set { _DiagnosisLinkIdElement = value; OnPropertyChanged("DiagnosisLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _DiagnosisLinkIdElement;
            
            /// <summary>
            /// Applicable diagnoses
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
            /// Applicable procedures
            /// </summary>
            [FhirElement("procedureLinkId", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> ProcedureLinkIdElement
            {
                get { if(_ProcedureLinkIdElement==null) _ProcedureLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _ProcedureLinkIdElement; }
                set { _ProcedureLinkIdElement = value; OnPropertyChanged("ProcedureLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _ProcedureLinkIdElement;
            
            /// <summary>
            /// Applicable procedures
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> ProcedureLinkId
            {
                get { return ProcedureLinkIdElement != null ? ProcedureLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ProcedureLinkIdElement = null; 
                    else
                        ProcedureLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("ProcedureLinkId");
                }
            }
            
            /// <summary>
            /// Applicable exception and supporting information
            /// </summary>
            [FhirElement("informationLinkId", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> InformationLinkIdElement
            {
                get { if(_InformationLinkIdElement==null) _InformationLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _InformationLinkIdElement; }
                set { _InformationLinkIdElement = value; OnPropertyChanged("InformationLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _InformationLinkIdElement;
            
            /// <summary>
            /// Applicable exception and supporting information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> InformationLinkId
            {
                get { return InformationLinkIdElement != null ? InformationLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        InformationLinkIdElement = null; 
                    else
                        InformationLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("InformationLinkId");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Date or dates of Service
            /// </summary>
            [FhirElement("serviced", Order=140, Choice=ChoiceType.DatatypeChoice)]
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
            /// Place of service
            /// </summary>
            [FhirElement("location", Order=150, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=160)]
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
            [FhirElement("unitPrice", Order=170)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=180)]
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
            /// Total item cost
            /// </summary>
            [FhirElement("net", Order=190)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=200)]
            [CLSCompliant(false)]
			[References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// Service Location
            /// </summary>
            [FhirElement("bodySite", Order=210)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _BodySite;
            
            /// <summary>
            /// Service Sub-location
            /// </summary>
            [FhirElement("subSite", Order=220)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SubSite
            {
                get { if(_SubSite==null) _SubSite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubSite; }
                set { _SubSite = value; OnPropertyChanged("SubSite"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SubSite;
            
            /// <summary>
            /// Encounters related to this billed item
            /// </summary>
            [FhirElement("encounter", Order=230)]
            [CLSCompliant(false)]
			[References("Encounter")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Encounter
            {
                get { if(_Encounter==null) _Encounter = new List<Hl7.Fhir.Model.ResourceReference>(); return _Encounter; }
                set { _Encounter = value; OnPropertyChanged("Encounter"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Encounter;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("detail", Order=240)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Claim.DetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.Claim.DetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.Claim.DetailComponent> _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(CareTeamLinkIdElement != null) dest.CareTeamLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(CareTeamLinkIdElement.DeepCopy());
                    if(DiagnosisLinkIdElement != null) dest.DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(DiagnosisLinkIdElement.DeepCopy());
                    if(ProcedureLinkIdElement != null) dest.ProcedureLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(ProcedureLinkIdElement.DeepCopy());
                    if(InformationLinkIdElement != null) dest.InformationLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(InformationLinkIdElement.DeepCopy());
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                    if(SubSite != null) dest.SubSite = new List<Hl7.Fhir.Model.CodeableConcept>(SubSite.DeepCopy());
                    if(Encounter != null) dest.Encounter = new List<Hl7.Fhir.Model.ResourceReference>(Encounter.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.Claim.DetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(CareTeamLinkIdElement, otherT.CareTeamLinkIdElement)) return false;
                if( !DeepComparable.Matches(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.Matches(ProcedureLinkIdElement, otherT.ProcedureLinkIdElement)) return false;
                if( !DeepComparable.Matches(InformationLinkIdElement, otherT.InformationLinkIdElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(CareTeamLinkIdElement, otherT.CareTeamLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(ProcedureLinkIdElement, otherT.ProcedureLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(InformationLinkIdElement, otherT.InformationLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    foreach (var elem in CareTeamLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in DiagnosisLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ProcedureLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in InformationLinkIdElement) { if (elem != null) yield return elem; }
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Serviced != null) yield return Serviced;
                    if (Location != null) yield return Location;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in Udi) { if (elem != null) yield return elem; }
                    if (BodySite != null) yield return BodySite;
                    foreach (var elem in SubSite) { if (elem != null) yield return elem; }
                    foreach (var elem in Encounter) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    foreach (var elem in CareTeamLinkIdElement) { if (elem != null) yield return new ElementValue("careTeamLinkId", elem); }
                    foreach (var elem in DiagnosisLinkIdElement) { if (elem != null) yield return new ElementValue("diagnosisLinkId", elem); }
                    foreach (var elem in ProcedureLinkIdElement) { if (elem != null) yield return new ElementValue("procedureLinkId", elem); }
                    foreach (var elem in InformationLinkIdElement) { if (elem != null) yield return new ElementValue("informationLinkId", elem); }
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Serviced != null) yield return new ElementValue("serviced", Serviced);
                    if (Location != null) yield return new ElementValue("location", Location);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in Udi) { if (elem != null) yield return new ElementValue("udi", elem); }
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    foreach (var elem in SubSite) { if (elem != null) yield return new ElementValue("subSite", elem); }
                    foreach (var elem in Encounter) { if (elem != null) yield return new ElementValue("encounter", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
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
            [FhirElement("sequence", Order=40)]
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
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=100)]
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
            [FhirElement("unitPrice", Order=110)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=120)]
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
            /// Total additional item cost
            /// </summary>
            [FhirElement("net", Order=130)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=140)]
            [CLSCompliant(false)]
			[References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("subDetail", Order=150)]
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
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
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
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
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
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
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
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in Udi) { if (elem != null) yield return elem; }
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
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in Udi) { if (elem != null) yield return new ElementValue("udi", elem); }
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
            [FhirElement("sequence", Order=40)]
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
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=100)]
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
            [FhirElement("unitPrice", Order=110)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=120)]
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
            /// Net additional item cost
            /// </summary>
            [FhirElement("net", Order=130)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=140)]
            [CLSCompliant(false)]
			[References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
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
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
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
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
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
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in Udi) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in Udi) { if (elem != null) yield return new ElementValue("udi", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Claim number
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
        /// Type or discipline
        /// </summary>
        [FhirElement("type", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Finer grained claim type information
        /// </summary>
        [FhirElement("subType", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> SubType
        {
            get { if(_SubType==null) _SubType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubType; }
            set { _SubType = value; OnPropertyChanged("SubType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _SubType;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        [FhirElement("use", Order=130)]
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
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Order=140)]
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
        /// Period for charge submission
        /// </summary>
        [FhirElement("billablePeriod", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Period BillablePeriod
        {
            get { return _BillablePeriod; }
            set { _BillablePeriod = value; OnPropertyChanged("BillablePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _BillablePeriod;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", Order=160)]
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
        /// Author
        /// </summary>
        [FhirElement("enterer", Order=170)]
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
        /// Target
        /// </summary>
        [FhirElement("insurer", Order=180)]
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
        /// Responsible provider
        /// </summary>
        [FhirElement("provider", Order=190)]
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
        [FhirElement("organization", Order=200)]
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
        /// Desired processing priority
        /// </summary>
        [FhirElement("priority", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// Funds requested to be reserved
        /// </summary>
        [FhirElement("fundsReserve", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FundsReserve
        {
            get { return _FundsReserve; }
            set { _FundsReserve = value; OnPropertyChanged("FundsReserve"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FundsReserve;
        
        /// <summary>
        /// Related Claims which may be revelant to processing this claimn
        /// </summary>
        [FhirElement("related", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.RelatedClaimComponent> Related
        {
            get { if(_Related==null) _Related = new List<Hl7.Fhir.Model.Claim.RelatedClaimComponent>(); return _Related; }
            set { _Related = value; OnPropertyChanged("Related"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.RelatedClaimComponent> _Related;
        
        /// <summary>
        /// Prescription authorizing services or products
        /// </summary>
        [FhirElement("prescription", Order=240)]
        [CLSCompliant(false)]
		[References("MedicationRequest","VisionPrescription")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescription
        {
            get { return _Prescription; }
            set { _Prescription = value; OnPropertyChanged("Prescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Prescription;
        
        /// <summary>
        /// Original prescription if superceded by fulfiller
        /// </summary>
        [FhirElement("originalPrescription", Order=250)]
        [CLSCompliant(false)]
		[References("MedicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OriginalPrescription
        {
            get { return _OriginalPrescription; }
            set { _OriginalPrescription = value; OnPropertyChanged("OriginalPrescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OriginalPrescription;
        
        /// <summary>
        /// Party to be paid any benefits payable
        /// </summary>
        [FhirElement("payee", Order=260)]
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
        [FhirElement("referral", Order=270)]
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
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", Order=280)]
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
        /// Members of the care team
        /// </summary>
        [FhirElement("careTeam", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.CareTeamComponent> CareTeam
        {
            get { if(_CareTeam==null) _CareTeam = new List<Hl7.Fhir.Model.Claim.CareTeamComponent>(); return _CareTeam; }
            set { _CareTeam = value; OnPropertyChanged("CareTeam"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.CareTeamComponent> _CareTeam;
        
        /// <summary>
        /// Exceptions, special considerations, the condition, situation, prior or concurrent issues
        /// </summary>
        [FhirElement("information", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.SpecialConditionComponent> Information
        {
            get { if(_Information==null) _Information = new List<Hl7.Fhir.Model.Claim.SpecialConditionComponent>(); return _Information; }
            set { _Information = value; OnPropertyChanged("Information"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.SpecialConditionComponent> _Information;
        
        /// <summary>
        /// List of Diagnosis
        /// </summary>
        [FhirElement("diagnosis", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.DiagnosisComponent> Diagnosis
        {
            get { if(_Diagnosis==null) _Diagnosis = new List<Hl7.Fhir.Model.Claim.DiagnosisComponent>(); return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.DiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// Procedures performed
        /// </summary>
        [FhirElement("procedure", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.ProcedureComponent> Procedure
        {
            get { if(_Procedure==null) _Procedure = new List<Hl7.Fhir.Model.Claim.ProcedureComponent>(); return _Procedure; }
            set { _Procedure = value; OnPropertyChanged("Procedure"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.ProcedureComponent> _Procedure;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("insurance", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.InsuranceComponent> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<Hl7.Fhir.Model.Claim.InsuranceComponent>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.InsuranceComponent> _Insurance;
        
        /// <summary>
        /// Details about an accident
        /// </summary>
        [FhirElement("accident", Order=340)]
        [DataMember]
        public Hl7.Fhir.Model.Claim.AccidentComponent Accident
        {
            get { return _Accident; }
            set { _Accident = value; OnPropertyChanged("Accident"); }
        }
        
        private Hl7.Fhir.Model.Claim.AccidentComponent _Accident;
        
        /// <summary>
        /// Period unable to work
        /// </summary>
        [FhirElement("employmentImpacted", Order=350)]
        [DataMember]
        public Hl7.Fhir.Model.Period EmploymentImpacted
        {
            get { return _EmploymentImpacted; }
            set { _EmploymentImpacted = value; OnPropertyChanged("EmploymentImpacted"); }
        }
        
        private Hl7.Fhir.Model.Period _EmploymentImpacted;
        
        /// <summary>
        /// Period in hospital
        /// </summary>
        [FhirElement("hospitalization", Order=360)]
        [DataMember]
        public Hl7.Fhir.Model.Period Hospitalization
        {
            get { return _Hospitalization; }
            set { _Hospitalization = value; OnPropertyChanged("Hospitalization"); }
        }
        
        private Hl7.Fhir.Model.Period _Hospitalization;
        
        /// <summary>
        /// Goods and Services
        /// </summary>
        [FhirElement("item", Order=370)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.Claim.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.ItemComponent> _Item;
        
        /// <summary>
        /// Total claim cost
        /// </summary>
        [FhirElement("total", Order=380)]
        [DataMember]
        public Money Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged("Total"); }
        }
        
        private Money _Total;
        

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
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SubType != null) dest.SubType = new List<Hl7.Fhir.Model.CodeableConcept>(SubType.DeepCopy());
                if(Use_Element != null) dest.Use_Element = (Code<Hl7.Fhir.Model.Claim.Use>)Use_Element.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(BillablePeriod != null) dest.BillablePeriod = (Hl7.Fhir.Model.Period)BillablePeriod.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(FundsReserve != null) dest.FundsReserve = (Hl7.Fhir.Model.CodeableConcept)FundsReserve.DeepCopy();
                if(Related != null) dest.Related = new List<Hl7.Fhir.Model.Claim.RelatedClaimComponent>(Related.DeepCopy());
                if(Prescription != null) dest.Prescription = (Hl7.Fhir.Model.ResourceReference)Prescription.DeepCopy();
                if(OriginalPrescription != null) dest.OriginalPrescription = (Hl7.Fhir.Model.ResourceReference)OriginalPrescription.DeepCopy();
                if(Payee != null) dest.Payee = (Hl7.Fhir.Model.Claim.PayeeComponent)Payee.DeepCopy();
                if(Referral != null) dest.Referral = (Hl7.Fhir.Model.ResourceReference)Referral.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.ResourceReference)Facility.DeepCopy();
                if(CareTeam != null) dest.CareTeam = new List<Hl7.Fhir.Model.Claim.CareTeamComponent>(CareTeam.DeepCopy());
                if(Information != null) dest.Information = new List<Hl7.Fhir.Model.Claim.SpecialConditionComponent>(Information.DeepCopy());
                if(Diagnosis != null) dest.Diagnosis = new List<Hl7.Fhir.Model.Claim.DiagnosisComponent>(Diagnosis.DeepCopy());
                if(Procedure != null) dest.Procedure = new List<Hl7.Fhir.Model.Claim.ProcedureComponent>(Procedure.DeepCopy());
                if(Insurance != null) dest.Insurance = new List<Hl7.Fhir.Model.Claim.InsuranceComponent>(Insurance.DeepCopy());
                if(Accident != null) dest.Accident = (Hl7.Fhir.Model.Claim.AccidentComponent)Accident.DeepCopy();
                if(EmploymentImpacted != null) dest.EmploymentImpacted = (Hl7.Fhir.Model.Period)EmploymentImpacted.DeepCopy();
                if(Hospitalization != null) dest.Hospitalization = (Hl7.Fhir.Model.Period)Hospitalization.DeepCopy();
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.Claim.ItemComponent>(Item.DeepCopy());
                if(Total != null) dest.Total = (Money)Total.DeepCopy();
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
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
            if( !DeepComparable.Matches(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(BillablePeriod, otherT.BillablePeriod)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.Matches(Related, otherT.Related)) return false;
            if( !DeepComparable.Matches(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.Matches(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
            if( !DeepComparable.Matches(Referral, otherT.Referral)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(CareTeam, otherT.CareTeam)) return false;
            if( !DeepComparable.Matches(Information, otherT.Information)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(Accident, otherT.Accident)) return false;
            if( !DeepComparable.Matches(EmploymentImpacted, otherT.EmploymentImpacted)) return false;
            if( !DeepComparable.Matches(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(Total, otherT.Total)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Claim;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
            if( !DeepComparable.IsExactly(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(BillablePeriod, otherT.BillablePeriod)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;
            if( !DeepComparable.IsExactly(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.IsExactly(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
            if( !DeepComparable.IsExactly(Referral, otherT.Referral)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(CareTeam, otherT.CareTeam)) return false;
            if( !DeepComparable.IsExactly(Information, otherT.Information)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(Accident, otherT.Accident)) return false;
            if( !DeepComparable.IsExactly(EmploymentImpacted, otherT.EmploymentImpacted)) return false;
            if( !DeepComparable.IsExactly(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
            
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
				if (Type != null) yield return Type;
				foreach (var elem in SubType) { if (elem != null) yield return elem; }
				if (Use_Element != null) yield return Use_Element;
				if (Patient != null) yield return Patient;
				if (BillablePeriod != null) yield return BillablePeriod;
				if (CreatedElement != null) yield return CreatedElement;
				if (Enterer != null) yield return Enterer;
				if (Insurer != null) yield return Insurer;
				if (Provider != null) yield return Provider;
				if (Organization != null) yield return Organization;
				if (Priority != null) yield return Priority;
				if (FundsReserve != null) yield return FundsReserve;
				foreach (var elem in Related) { if (elem != null) yield return elem; }
				if (Prescription != null) yield return Prescription;
				if (OriginalPrescription != null) yield return OriginalPrescription;
				if (Payee != null) yield return Payee;
				if (Referral != null) yield return Referral;
				if (Facility != null) yield return Facility;
				foreach (var elem in CareTeam) { if (elem != null) yield return elem; }
				foreach (var elem in Information) { if (elem != null) yield return elem; }
				foreach (var elem in Diagnosis) { if (elem != null) yield return elem; }
				foreach (var elem in Procedure) { if (elem != null) yield return elem; }
				foreach (var elem in Insurance) { if (elem != null) yield return elem; }
				if (Accident != null) yield return Accident;
				if (EmploymentImpacted != null) yield return EmploymentImpacted;
				if (Hospitalization != null) yield return Hospitalization;
				foreach (var elem in Item) { if (elem != null) yield return elem; }
				if (Total != null) yield return Total;
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
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in SubType) { if (elem != null) yield return new ElementValue("subType", elem); }
                if (Use_Element != null) yield return new ElementValue("use", Use_Element);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (BillablePeriod != null) yield return new ElementValue("billablePeriod", BillablePeriod);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                if (Provider != null) yield return new ElementValue("provider", Provider);
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (Priority != null) yield return new ElementValue("priority", Priority);
                if (FundsReserve != null) yield return new ElementValue("fundsReserve", FundsReserve);
                foreach (var elem in Related) { if (elem != null) yield return new ElementValue("related", elem); }
                if (Prescription != null) yield return new ElementValue("prescription", Prescription);
                if (OriginalPrescription != null) yield return new ElementValue("originalPrescription", OriginalPrescription);
                if (Payee != null) yield return new ElementValue("payee", Payee);
                if (Referral != null) yield return new ElementValue("referral", Referral);
                if (Facility != null) yield return new ElementValue("facility", Facility);
                foreach (var elem in CareTeam) { if (elem != null) yield return new ElementValue("careTeam", elem); }
                foreach (var elem in Information) { if (elem != null) yield return new ElementValue("information", elem); }
                foreach (var elem in Diagnosis) { if (elem != null) yield return new ElementValue("diagnosis", elem); }
                foreach (var elem in Procedure) { if (elem != null) yield return new ElementValue("procedure", elem); }
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
                if (Accident != null) yield return new ElementValue("accident", Accident);
                if (EmploymentImpacted != null) yield return new ElementValue("employmentImpacted", EmploymentImpacted);
                if (Hospitalization != null) yield return new ElementValue("hospitalization", Hospitalization);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                if (Total != null) yield return new ElementValue("total", Total);
            }
        }

    }
    
}
