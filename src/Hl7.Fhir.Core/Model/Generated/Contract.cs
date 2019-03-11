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
    /// Legal Agreement
    /// </summary>
    [FhirType("Contract", IsResource=true)]
    [DataContract]
    public partial class Contract : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Contract; } }
        [NotMapped]
        public override string TypeName { get { return "Contract"; } }
        
        /// <summary>
        /// This value set contract specific codes for status.
        /// (url: http://hl7.org/fhir/ValueSet/contract-status)
        /// </summary>
        [FhirEnumeration("ContractResourceStatusCodes")]
        public enum ContractResourceStatusCodes
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("amended", "http://hl7.org/fhir/contract-status"), Description("Amended")]
            Amended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("appended", "http://hl7.org/fhir/contract-status"), Description("Appended")]
            Appended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/contract-status"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("disputed", "http://hl7.org/fhir/contract-status"), Description("Disputed")]
            Disputed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/contract-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("executable", "http://hl7.org/fhir/contract-status"), Description("Executable")]
            Executable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("executed", "http://hl7.org/fhir/contract-status"), Description("Executed")]
            Executed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("negotiable", "http://hl7.org/fhir/contract-status"), Description("Negotiable")]
            Negotiable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("offered", "http://hl7.org/fhir/contract-status"), Description("Offered")]
            Offered,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("policy", "http://hl7.org/fhir/contract-status"), Description("Policy")]
            Policy,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("rejected", "http://hl7.org/fhir/contract-status"), Description("Rejected")]
            Rejected,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("renewed", "http://hl7.org/fhir/contract-status"), Description("Renewed")]
            Renewed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("revoked", "http://hl7.org/fhir/contract-status"), Description("Revoked")]
            Revoked,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("resolved", "http://hl7.org/fhir/contract-status"), Description("Resolved")]
            Resolved,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-status)
            /// </summary>
            [EnumLiteral("terminated", "http://hl7.org/fhir/contract-status"), Description("Terminated")]
            Terminated,
        }

        /// <summary>
        /// This value set contract specific codes for status.
        /// (url: http://hl7.org/fhir/ValueSet/contract-publicationstatus)
        /// </summary>
        [FhirEnumeration("ContractResourcePublicationStatusCodes")]
        public enum ContractResourcePublicationStatusCodes
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("amended", "http://hl7.org/fhir/contract-publicationstatus"), Description("Amended")]
            Amended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("appended", "http://hl7.org/fhir/contract-publicationstatus"), Description("Appended")]
            Appended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/contract-publicationstatus"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("disputed", "http://hl7.org/fhir/contract-publicationstatus"), Description("Disputed")]
            Disputed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/contract-publicationstatus"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("executable", "http://hl7.org/fhir/contract-publicationstatus"), Description("Executable")]
            Executable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("executed", "http://hl7.org/fhir/contract-publicationstatus"), Description("Executed")]
            Executed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("negotiable", "http://hl7.org/fhir/contract-publicationstatus"), Description("Negotiable")]
            Negotiable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("offered", "http://hl7.org/fhir/contract-publicationstatus"), Description("Offered")]
            Offered,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("policy", "http://hl7.org/fhir/contract-publicationstatus"), Description("Policy")]
            Policy,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("rejected", "http://hl7.org/fhir/contract-publicationstatus"), Description("Rejected")]
            Rejected,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("renewed", "http://hl7.org/fhir/contract-publicationstatus"), Description("Renewed")]
            Renewed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("revoked", "http://hl7.org/fhir/contract-publicationstatus"), Description("Revoked")]
            Revoked,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("resolved", "http://hl7.org/fhir/contract-publicationstatus"), Description("Resolved")]
            Resolved,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contract-publicationstatus)
            /// </summary>
            [EnumLiteral("terminated", "http://hl7.org/fhir/contract-publicationstatus"), Description("Terminated")]
            Terminated,
        }

        [FhirType("ContentDefinitionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContentDefinitionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContentDefinitionComponent"; } }
            
            /// <summary>
            /// Content structure and use
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
            /// Detailed Content Type Definition
            /// </summary>
            [FhirElement("subType", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SubType
            {
                get { return _SubType; }
                set { _SubType = value; OnPropertyChanged("SubType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SubType;
            
            /// <summary>
            /// Publisher Entity
            /// </summary>
            [FhirElement("publisher", Order=60)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Publisher
            {
                get { return _Publisher; }
                set { _Publisher = value; OnPropertyChanged("Publisher"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Publisher;
            
            /// <summary>
            /// When published
            /// </summary>
            [FhirElement("publicationDate", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime PublicationDateElement
            {
                get { return _PublicationDateElement; }
                set { _PublicationDateElement = value; OnPropertyChanged("PublicationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _PublicationDateElement;
            
            /// <summary>
            /// When published
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PublicationDate
            {
                get { return PublicationDateElement != null ? PublicationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        PublicationDateElement = null; 
                    else
                        PublicationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("PublicationDate");
                }
            }
            
            /// <summary>
            /// draft | active | retired | unknown
            /// </summary>
            [FhirElement("publicationStatus", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Contract.ContractResourcePublicationStatusCodes> PublicationStatusElement
            {
                get { return _PublicationStatusElement; }
                set { _PublicationStatusElement = value; OnPropertyChanged("PublicationStatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Contract.ContractResourcePublicationStatusCodes> _PublicationStatusElement;
            
            /// <summary>
            /// draft | active | retired | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Contract.ContractResourcePublicationStatusCodes? PublicationStatus
            {
                get { return PublicationStatusElement != null ? PublicationStatusElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PublicationStatusElement = null; 
                    else
                        PublicationStatusElement = new Code<Hl7.Fhir.Model.Contract.ContractResourcePublicationStatusCodes>(value);
                    OnPropertyChanged("PublicationStatus");
                }
            }
            
            /// <summary>
            /// Publication Ownership
            /// </summary>
            [FhirElement("copyright", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Copyright
            {
                get { return _Copyright; }
                set { _Copyright = value; OnPropertyChanged("Copyright"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Copyright;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContentDefinitionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(SubType != null) dest.SubType = (Hl7.Fhir.Model.CodeableConcept)SubType.DeepCopy();
                    if(Publisher != null) dest.Publisher = (Hl7.Fhir.Model.ResourceReference)Publisher.DeepCopy();
                    if(PublicationDateElement != null) dest.PublicationDateElement = (Hl7.Fhir.Model.FhirDateTime)PublicationDateElement.DeepCopy();
                    if(PublicationStatusElement != null) dest.PublicationStatusElement = (Code<Hl7.Fhir.Model.Contract.ContractResourcePublicationStatusCodes>)PublicationStatusElement.DeepCopy();
                    if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContentDefinitionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContentDefinitionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
                if( !DeepComparable.Matches(Publisher, otherT.Publisher)) return false;
                if( !DeepComparable.Matches(PublicationDateElement, otherT.PublicationDateElement)) return false;
                if( !DeepComparable.Matches(PublicationStatusElement, otherT.PublicationStatusElement)) return false;
                if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContentDefinitionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
                if( !DeepComparable.IsExactly(Publisher, otherT.Publisher)) return false;
                if( !DeepComparable.IsExactly(PublicationDateElement, otherT.PublicationDateElement)) return false;
                if( !DeepComparable.IsExactly(PublicationStatusElement, otherT.PublicationStatusElement)) return false;
                if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (SubType != null) yield return SubType;
                    if (Publisher != null) yield return Publisher;
                    if (PublicationDateElement != null) yield return PublicationDateElement;
                    if (PublicationStatusElement != null) yield return PublicationStatusElement;
                    if (Copyright != null) yield return Copyright;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (SubType != null) yield return new ElementValue("subType", SubType);
                    if (Publisher != null) yield return new ElementValue("publisher", Publisher);
                    if (PublicationDateElement != null) yield return new ElementValue("publicationDate", PublicationDateElement);
                    if (PublicationStatusElement != null) yield return new ElementValue("publicationStatus", PublicationStatusElement);
                    if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                }
            }

            
        }
        
        
        [FhirType("TermComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class TermComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TermComponent"; } }
            
            /// <summary>
            /// Contract Term Number
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Contract Term Issue Date Time
            /// </summary>
            [FhirElement("issued", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime IssuedElement
            {
                get { return _IssuedElement; }
                set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
            
            /// <summary>
            /// Contract Term Issue Date Time
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Issued
            {
                get { return IssuedElement != null ? IssuedElement.Value : null; }
                set
                {
                    if (value == null)
                        IssuedElement = null; 
                    else
                        IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Issued");
                }
            }
            
            /// <summary>
            /// Contract Term Effective Time
            /// </summary>
            [FhirElement("applies", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period Applies
            {
                get { return _Applies; }
                set { _Applies = value; OnPropertyChanged("Applies"); }
            }
            
            private Hl7.Fhir.Model.Period _Applies;
            
            /// <summary>
            /// Term Concern
            /// </summary>
            [FhirElement("topic", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Topic
            {
                get { return _Topic; }
                set { _Topic = value; OnPropertyChanged("Topic"); }
            }
            
            private Hl7.Fhir.Model.Element _Topic;
            
            /// <summary>
            /// Contract Term Type or Form
            /// </summary>
            [FhirElement("type", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Contract Term Type specific classification
            /// </summary>
            [FhirElement("subType", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SubType
            {
                get { return _SubType; }
                set { _SubType = value; OnPropertyChanged("SubType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SubType;
            
            /// <summary>
            /// Term Statement
            /// </summary>
            [FhirElement("text", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Term Statement
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Protection for the Term
            /// </summary>
            [FhirElement("securityLabel", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.SecurityLabelComponent> SecurityLabel
            {
                get { if(_SecurityLabel==null) _SecurityLabel = new List<Hl7.Fhir.Model.Contract.SecurityLabelComponent>(); return _SecurityLabel; }
                set { _SecurityLabel = value; OnPropertyChanged("SecurityLabel"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.SecurityLabelComponent> _SecurityLabel;
            
            /// <summary>
            /// Context of the Contract term
            /// </summary>
            [FhirElement("offer", Order=120)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Contract.ContractOfferComponent Offer
            {
                get { return _Offer; }
                set { _Offer = value; OnPropertyChanged("Offer"); }
            }
            
            private Hl7.Fhir.Model.Contract.ContractOfferComponent _Offer;
            
            /// <summary>
            /// Contract Term Asset List
            /// </summary>
            [FhirElement("asset", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.ContractAssetComponent> Asset
            {
                get { if(_Asset==null) _Asset = new List<Hl7.Fhir.Model.Contract.ContractAssetComponent>(); return _Asset; }
                set { _Asset = value; OnPropertyChanged("Asset"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.ContractAssetComponent> _Asset;
            
            /// <summary>
            /// Entity being ascribed responsibility
            /// </summary>
            [FhirElement("action", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.ActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.Contract.ActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.ActionComponent> _Action;
            
            /// <summary>
            /// Nested Contract Term Group
            /// </summary>
            [FhirElement("group", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.TermComponent> Group
            {
                get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.Contract.TermComponent>(); return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.TermComponent> _Group;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TermComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                    if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                    if(Topic != null) dest.Topic = (Hl7.Fhir.Model.Element)Topic.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(SubType != null) dest.SubType = (Hl7.Fhir.Model.CodeableConcept)SubType.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Contract.SecurityLabelComponent>(SecurityLabel.DeepCopy());
                    if(Offer != null) dest.Offer = (Hl7.Fhir.Model.Contract.ContractOfferComponent)Offer.DeepCopy();
                    if(Asset != null) dest.Asset = new List<Hl7.Fhir.Model.Contract.ContractAssetComponent>(Asset.DeepCopy());
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.Contract.ActionComponent>(Action.DeepCopy());
                    if(Group != null) dest.Group = new List<Hl7.Fhir.Model.Contract.TermComponent>(Group.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TermComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TermComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
                if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
                if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.Matches(Offer, otherT.Offer)) return false;
                if( !DeepComparable.Matches(Asset, otherT.Asset)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(Group, otherT.Group)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TermComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
                if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
                if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.IsExactly(Offer, otherT.Offer)) return false;
                if( !DeepComparable.IsExactly(Asset, otherT.Asset)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (IssuedElement != null) yield return IssuedElement;
                    if (Applies != null) yield return Applies;
                    if (Topic != null) yield return Topic;
                    if (Type != null) yield return Type;
                    if (SubType != null) yield return SubType;
                    if (TextElement != null) yield return TextElement;
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return elem; }
                    if (Offer != null) yield return Offer;
                    foreach (var elem in Asset) { if (elem != null) yield return elem; }
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                    foreach (var elem in Group) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                    if (Applies != null) yield return new ElementValue("applies", Applies);
                    if (Topic != null) yield return new ElementValue("topic", Topic);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (SubType != null) yield return new ElementValue("subType", SubType);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return new ElementValue("securityLabel", elem); }
                    if (Offer != null) yield return new ElementValue("offer", Offer);
                    foreach (var elem in Asset) { if (elem != null) yield return new ElementValue("asset", elem); }
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                    foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
                }
            }

            
        }
        
        
        [FhirType("SecurityLabelComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SecurityLabelComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SecurityLabelComponent"; } }
            
            /// <summary>
            /// Link to Security Labels
            /// </summary>
            [FhirElement("number", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> NumberElement
            {
                get { if(_NumberElement==null) _NumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _NumberElement;
            
            /// <summary>
            /// Link to Security Labels
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> Number
            {
                get { return NumberElement != null ? NumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NumberElement = null; 
                    else
                        NumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(value.Select(elem=>new Hl7.Fhir.Model.UnsignedInt(elem)));
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// Confidentiality Protection
            /// </summary>
            [FhirElement("classification", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Classification
            {
                get { return _Classification; }
                set { _Classification = value; OnPropertyChanged("Classification"); }
            }
            
            private Hl7.Fhir.Model.Coding _Classification;
            
            /// <summary>
            /// Applicable Policy
            /// </summary>
            [FhirElement("category", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Category
            {
                get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.Coding>(); return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Category;
            
            /// <summary>
            /// Handling Instructions
            /// </summary>
            [FhirElement("control", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Control
            {
                get { if(_Control==null) _Control = new List<Hl7.Fhir.Model.Coding>(); return _Control; }
                set { _Control = value; OnPropertyChanged("Control"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Control;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityLabelComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(NumberElement.DeepCopy());
                    if(Classification != null) dest.Classification = (Hl7.Fhir.Model.Coding)Classification.DeepCopy();
                    if(Category != null) dest.Category = new List<Hl7.Fhir.Model.Coding>(Category.DeepCopy());
                    if(Control != null) dest.Control = new List<Hl7.Fhir.Model.Coding>(Control.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityLabelComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityLabelComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Control, otherT.Control)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityLabelComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Control, otherT.Control)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in NumberElement) { if (elem != null) yield return elem; }
                    if (Classification != null) yield return Classification;
                    foreach (var elem in Category) { if (elem != null) yield return elem; }
                    foreach (var elem in Control) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in NumberElement) { if (elem != null) yield return new ElementValue("number", elem); }
                    if (Classification != null) yield return new ElementValue("classification", Classification);
                    foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                    foreach (var elem in Control) { if (elem != null) yield return new ElementValue("control", elem); }
                }
            }

            
        }
        
        
        [FhirType("ContractOfferComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContractOfferComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContractOfferComponent"; } }
            
            /// <summary>
            /// Offer business ID
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Offer Recipient
            /// </summary>
            [FhirElement("party", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.ContractPartyComponent> Party
            {
                get { if(_Party==null) _Party = new List<Hl7.Fhir.Model.Contract.ContractPartyComponent>(); return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.ContractPartyComponent> _Party;
            
            /// <summary>
            /// Negotiable offer asset
            /// </summary>
            [FhirElement("topic", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Topic
            {
                get { return _Topic; }
                set { _Topic = value; OnPropertyChanged("Topic"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Topic;
            
            /// <summary>
            /// Contract Offer Type or Form
            /// </summary>
            [FhirElement("type", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Accepting party choice
            /// </summary>
            [FhirElement("decision", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Decision
            {
                get { return _Decision; }
                set { _Decision = value; OnPropertyChanged("Decision"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Decision;
            
            /// <summary>
            /// How decision is conveyed
            /// </summary>
            [FhirElement("decisionMode", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> DecisionMode
            {
                get { if(_DecisionMode==null) _DecisionMode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _DecisionMode; }
                set { _DecisionMode = value; OnPropertyChanged("DecisionMode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _DecisionMode;
            
            /// <summary>
            /// Response to offer text
            /// </summary>
            [FhirElement("answer", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.AnswerComponent> Answer
            {
                get { if(_Answer==null) _Answer = new List<Hl7.Fhir.Model.Contract.AnswerComponent>(); return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.AnswerComponent> _Answer;
            
            /// <summary>
            /// Human readable offer text
            /// </summary>
            [FhirElement("text", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Human readable offer text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Pointer to text
            /// </summary>
            [FhirElement("linkId", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LinkIdElement
            {
                get { if(_LinkIdElement==null) _LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _LinkIdElement;
            
            /// <summary>
            /// Pointer to text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> LinkId
            {
                get { return LinkIdElement != null ? LinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LinkIdElement = null; 
                    else
                        LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("LinkId");
                }
            }
            
            /// <summary>
            /// Offer restriction numbers
            /// </summary>
            [FhirElement("securityLabelNumber", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> SecurityLabelNumberElement
            {
                get { if(_SecurityLabelNumberElement==null) _SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _SecurityLabelNumberElement; }
                set { _SecurityLabelNumberElement = value; OnPropertyChanged("SecurityLabelNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _SecurityLabelNumberElement;
            
            /// <summary>
            /// Offer restriction numbers
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SecurityLabelNumber
            {
                get { return SecurityLabelNumberElement != null ? SecurityLabelNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SecurityLabelNumberElement = null; 
                    else
                        SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(value.Select(elem=>new Hl7.Fhir.Model.UnsignedInt(elem)));
                    OnPropertyChanged("SecurityLabelNumber");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContractOfferComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Party != null) dest.Party = new List<Hl7.Fhir.Model.Contract.ContractPartyComponent>(Party.DeepCopy());
                    if(Topic != null) dest.Topic = (Hl7.Fhir.Model.ResourceReference)Topic.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Decision != null) dest.Decision = (Hl7.Fhir.Model.CodeableConcept)Decision.DeepCopy();
                    if(DecisionMode != null) dest.DecisionMode = new List<Hl7.Fhir.Model.CodeableConcept>(DecisionMode.DeepCopy());
                    if(Answer != null) dest.Answer = new List<Hl7.Fhir.Model.Contract.AnswerComponent>(Answer.DeepCopy());
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(LinkIdElement != null) dest.LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(LinkIdElement.DeepCopy());
                    if(SecurityLabelNumberElement != null) dest.SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(SecurityLabelNumberElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContractOfferComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContractOfferComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Decision, otherT.Decision)) return false;
                if( !DeepComparable.Matches(DecisionMode, otherT.DecisionMode)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContractOfferComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Decision, otherT.Decision)) return false;
                if( !DeepComparable.IsExactly(DecisionMode, otherT.DecisionMode)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    foreach (var elem in Party) { if (elem != null) yield return elem; }
                    if (Topic != null) yield return Topic;
                    if (Type != null) yield return Type;
                    if (Decision != null) yield return Decision;
                    foreach (var elem in DecisionMode) { if (elem != null) yield return elem; }
                    foreach (var elem in Answer) { if (elem != null) yield return elem; }
                    if (TextElement != null) yield return TextElement;
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    foreach (var elem in Party) { if (elem != null) yield return new ElementValue("party", elem); }
                    if (Topic != null) yield return new ElementValue("topic", Topic);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Decision != null) yield return new ElementValue("decision", Decision);
                    foreach (var elem in DecisionMode) { if (elem != null) yield return new ElementValue("decisionMode", elem); }
                    foreach (var elem in Answer) { if (elem != null) yield return new ElementValue("answer", elem); }
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return new ElementValue("linkId", elem); }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return new ElementValue("securityLabelNumber", elem); }
                }
            }

            
        }
        
        
        [FhirType("ContractPartyComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContractPartyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContractPartyComponent"; } }
            
            /// <summary>
            /// Referenced entity
            /// </summary>
            [FhirElement("reference", Order=40)]
            [CLSCompliant(false)]
			[References("Patient","RelatedPerson","Practitioner","PractitionerRole","Device","Group","Organization")]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Reference
            {
                get { if(_Reference==null) _Reference = new List<Hl7.Fhir.Model.ResourceReference>(); return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Reference;
            
            /// <summary>
            /// Participant engagement type
            /// </summary>
            [FhirElement("role", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContractPartyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reference != null) dest.Reference = new List<Hl7.Fhir.Model.ResourceReference>(Reference.DeepCopy());
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContractPartyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContractPartyComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContractPartyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Reference) { if (elem != null) yield return elem; }
                    if (Role != null) yield return Role;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Reference) { if (elem != null) yield return new ElementValue("reference", elem); }
                    if (Role != null) yield return new ElementValue("role", Role);
                }
            }

            
        }
        
        
        [FhirType("AnswerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AnswerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AnswerComponent"; } }
            
            /// <summary>
            /// The actual answer response
            /// </summary>
            [FhirElement("value", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AnswerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AnswerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AnswerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AnswerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }

            
        }
        
        
        [FhirType("ContractAssetComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContractAssetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContractAssetComponent"; } }
            
            /// <summary>
            /// Range of asset
            /// </summary>
            [FhirElement("scope", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Scope
            {
                get { return _Scope; }
                set { _Scope = value; OnPropertyChanged("Scope"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Scope;
            
            /// <summary>
            /// Asset category
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Associated entities
            /// </summary>
            [FhirElement("typeReference", Order=60)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> TypeReference
            {
                get { if(_TypeReference==null) _TypeReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _TypeReference; }
                set { _TypeReference = value; OnPropertyChanged("TypeReference"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _TypeReference;
            
            /// <summary>
            /// Asset sub-category
            /// </summary>
            [FhirElement("subtype", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Subtype
            {
                get { if(_Subtype==null) _Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Subtype; }
                set { _Subtype = value; OnPropertyChanged("Subtype"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Subtype;
            
            /// <summary>
            /// Kinship of the asset
            /// </summary>
            [FhirElement("relationship", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            
            private Hl7.Fhir.Model.Coding _Relationship;
            
            /// <summary>
            /// Circumstance of the asset
            /// </summary>
            [FhirElement("context", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.AssetContextComponent> Context
            {
                get { if(_Context==null) _Context = new List<Hl7.Fhir.Model.Contract.AssetContextComponent>(); return _Context; }
                set { _Context = value; OnPropertyChanged("Context"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.AssetContextComponent> _Context;
            
            /// <summary>
            /// Quality desctiption of asset
            /// </summary>
            [FhirElement("condition", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ConditionElement
            {
                get { return _ConditionElement; }
                set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ConditionElement;
            
            /// <summary>
            /// Quality desctiption of asset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Condition
            {
                get { return ConditionElement != null ? ConditionElement.Value : null; }
                set
                {
                    if (value == null)
                        ConditionElement = null; 
                    else
                        ConditionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Condition");
                }
            }
            
            /// <summary>
            /// Asset availability types
            /// </summary>
            [FhirElement("periodType", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> PeriodType
            {
                get { if(_PeriodType==null) _PeriodType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PeriodType; }
                set { _PeriodType = value; OnPropertyChanged("PeriodType"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _PeriodType;
            
            /// <summary>
            /// Time period of the asset
            /// </summary>
            [FhirElement("period", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Period> Period
            {
                get { if(_Period==null) _Period = new List<Hl7.Fhir.Model.Period>(); return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private List<Hl7.Fhir.Model.Period> _Period;
            
            /// <summary>
            /// Time period
            /// </summary>
            [FhirElement("usePeriod", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Period> UsePeriod
            {
                get { if(_UsePeriod==null) _UsePeriod = new List<Hl7.Fhir.Model.Period>(); return _UsePeriod; }
                set { _UsePeriod = value; OnPropertyChanged("UsePeriod"); }
            }
            
            private List<Hl7.Fhir.Model.Period> _UsePeriod;
            
            /// <summary>
            /// Asset clause or question text
            /// </summary>
            [FhirElement("text", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Asset clause or question text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Pointer to asset text
            /// </summary>
            [FhirElement("linkId", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LinkIdElement
            {
                get { if(_LinkIdElement==null) _LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _LinkIdElement;
            
            /// <summary>
            /// Pointer to asset text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> LinkId
            {
                get { return LinkIdElement != null ? LinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LinkIdElement = null; 
                    else
                        LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("LinkId");
                }
            }
            
            /// <summary>
            /// Response to assets
            /// </summary>
            [FhirElement("answer", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.AnswerComponent> Answer
            {
                get { if(_Answer==null) _Answer = new List<Hl7.Fhir.Model.Contract.AnswerComponent>(); return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.AnswerComponent> _Answer;
            
            /// <summary>
            /// Asset restriction numbers
            /// </summary>
            [FhirElement("securityLabelNumber", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> SecurityLabelNumberElement
            {
                get { if(_SecurityLabelNumberElement==null) _SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _SecurityLabelNumberElement; }
                set { _SecurityLabelNumberElement = value; OnPropertyChanged("SecurityLabelNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _SecurityLabelNumberElement;
            
            /// <summary>
            /// Asset restriction numbers
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SecurityLabelNumber
            {
                get { return SecurityLabelNumberElement != null ? SecurityLabelNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SecurityLabelNumberElement = null; 
                    else
                        SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(value.Select(elem=>new Hl7.Fhir.Model.UnsignedInt(elem)));
                    OnPropertyChanged("SecurityLabelNumber");
                }
            }
            
            /// <summary>
            /// Contract Valued Item List
            /// </summary>
            [FhirElement("valuedItem", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.ValuedItemComponent> ValuedItem
            {
                get { if(_ValuedItem==null) _ValuedItem = new List<Hl7.Fhir.Model.Contract.ValuedItemComponent>(); return _ValuedItem; }
                set { _ValuedItem = value; OnPropertyChanged("ValuedItem"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.ValuedItemComponent> _ValuedItem;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContractAssetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Scope != null) dest.Scope = (Hl7.Fhir.Model.CodeableConcept)Scope.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(TypeReference != null) dest.TypeReference = new List<Hl7.Fhir.Model.ResourceReference>(TypeReference.DeepCopy());
                    if(Subtype != null) dest.Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(Subtype.DeepCopy());
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.Coding)Relationship.DeepCopy();
                    if(Context != null) dest.Context = new List<Hl7.Fhir.Model.Contract.AssetContextComponent>(Context.DeepCopy());
                    if(ConditionElement != null) dest.ConditionElement = (Hl7.Fhir.Model.FhirString)ConditionElement.DeepCopy();
                    if(PeriodType != null) dest.PeriodType = new List<Hl7.Fhir.Model.CodeableConcept>(PeriodType.DeepCopy());
                    if(Period != null) dest.Period = new List<Hl7.Fhir.Model.Period>(Period.DeepCopy());
                    if(UsePeriod != null) dest.UsePeriod = new List<Hl7.Fhir.Model.Period>(UsePeriod.DeepCopy());
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(LinkIdElement != null) dest.LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(LinkIdElement.DeepCopy());
                    if(Answer != null) dest.Answer = new List<Hl7.Fhir.Model.Contract.AnswerComponent>(Answer.DeepCopy());
                    if(SecurityLabelNumberElement != null) dest.SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(SecurityLabelNumberElement.DeepCopy());
                    if(ValuedItem != null) dest.ValuedItem = new List<Hl7.Fhir.Model.Contract.ValuedItemComponent>(ValuedItem.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContractAssetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContractAssetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Scope, otherT.Scope)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(TypeReference, otherT.TypeReference)) return false;
                if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(Context, otherT.Context)) return false;
                if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.Matches(PeriodType, otherT.PeriodType)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(UsePeriod, otherT.UsePeriod)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
                if( !DeepComparable.Matches(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                if( !DeepComparable.Matches(ValuedItem, otherT.ValuedItem)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContractAssetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Scope, otherT.Scope)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(TypeReference, otherT.TypeReference)) return false;
                if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
                if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.IsExactly(PeriodType, otherT.PeriodType)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(UsePeriod, otherT.UsePeriod)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
                if( !DeepComparable.IsExactly(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                if( !DeepComparable.IsExactly(ValuedItem, otherT.ValuedItem)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Scope != null) yield return Scope;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    foreach (var elem in TypeReference) { if (elem != null) yield return elem; }
                    foreach (var elem in Subtype) { if (elem != null) yield return elem; }
                    if (Relationship != null) yield return Relationship;
                    foreach (var elem in Context) { if (elem != null) yield return elem; }
                    if (ConditionElement != null) yield return ConditionElement;
                    foreach (var elem in PeriodType) { if (elem != null) yield return elem; }
                    foreach (var elem in Period) { if (elem != null) yield return elem; }
                    foreach (var elem in UsePeriod) { if (elem != null) yield return elem; }
                    if (TextElement != null) yield return TextElement;
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Answer) { if (elem != null) yield return elem; }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ValuedItem) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Scope != null) yield return new ElementValue("scope", Scope);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    foreach (var elem in TypeReference) { if (elem != null) yield return new ElementValue("typeReference", elem); }
                    foreach (var elem in Subtype) { if (elem != null) yield return new ElementValue("subtype", elem); }
                    if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                    foreach (var elem in Context) { if (elem != null) yield return new ElementValue("context", elem); }
                    if (ConditionElement != null) yield return new ElementValue("condition", ConditionElement);
                    foreach (var elem in PeriodType) { if (elem != null) yield return new ElementValue("periodType", elem); }
                    foreach (var elem in Period) { if (elem != null) yield return new ElementValue("period", elem); }
                    foreach (var elem in UsePeriod) { if (elem != null) yield return new ElementValue("usePeriod", elem); }
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return new ElementValue("linkId", elem); }
                    foreach (var elem in Answer) { if (elem != null) yield return new ElementValue("answer", elem); }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return new ElementValue("securityLabelNumber", elem); }
                    foreach (var elem in ValuedItem) { if (elem != null) yield return new ElementValue("valuedItem", elem); }
                }
            }

            
        }
        
        
        [FhirType("AssetContextComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AssetContextComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AssetContextComponent"; } }
            
            /// <summary>
            /// Creator,custodian or owner
            /// </summary>
            [FhirElement("reference", Order=40)]
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Codeable asset context
            /// </summary>
            [FhirElement("code", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Context description
            /// </summary>
            [FhirElement("text", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Context description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AssetContextComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AssetContextComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AssetContextComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AssetContextComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Reference != null) yield return Reference;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    if (TextElement != null) yield return TextElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                }
            }

            
        }
        
        
        [FhirType("ValuedItemComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ValuedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ValuedItemComponent"; } }
            
            /// <summary>
            /// Contract Valued Item Type
            /// </summary>
            [FhirElement("entity", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Entity
            {
                get { return _Entity; }
                set { _Entity = value; OnPropertyChanged("Entity"); }
            }
            
            private Hl7.Fhir.Model.Element _Entity;
            
            /// <summary>
            /// Contract Valued Item Number
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Contract Valued Item Effective Tiem
            /// </summary>
            [FhirElement("effectiveTime", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime EffectiveTimeElement
            {
                get { return _EffectiveTimeElement; }
                set { _EffectiveTimeElement = value; OnPropertyChanged("EffectiveTimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _EffectiveTimeElement;
            
            /// <summary>
            /// Contract Valued Item Effective Tiem
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string EffectiveTime
            {
                get { return EffectiveTimeElement != null ? EffectiveTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        EffectiveTimeElement = null; 
                    else
                        EffectiveTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("EffectiveTime");
                }
            }
            
            /// <summary>
            /// Count of Contract Valued Items
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Contract Valued Item fee, charge, or cost
            /// </summary>
            [FhirElement("unitPrice", Order=80)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Contract Valued Item Price Scaling Factor
            /// </summary>
            [FhirElement("factor", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Contract Valued Item Price Scaling Factor
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
            /// Contract Valued Item Difficulty Scaling Factor
            /// </summary>
            [FhirElement("points", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Contract Valued Item Difficulty Scaling Factor
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
            /// Total Contract Valued Item Value
            /// </summary>
            [FhirElement("net", Order=110)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Terms of valuation
            /// </summary>
            [FhirElement("payment", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PaymentElement
            {
                get { return _PaymentElement; }
                set { _PaymentElement = value; OnPropertyChanged("PaymentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PaymentElement;
            
            /// <summary>
            /// Terms of valuation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Payment
            {
                get { return PaymentElement != null ? PaymentElement.Value : null; }
                set
                {
                    if (value == null)
                        PaymentElement = null; 
                    else
                        PaymentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Payment");
                }
            }
            
            /// <summary>
            /// When payment is due
            /// </summary>
            [FhirElement("paymentDate", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime PaymentDateElement
            {
                get { return _PaymentDateElement; }
                set { _PaymentDateElement = value; OnPropertyChanged("PaymentDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _PaymentDateElement;
            
            /// <summary>
            /// When payment is due
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PaymentDate
            {
                get { return PaymentDateElement != null ? PaymentDateElement.Value : null; }
                set
                {
                    if (value == null)
                        PaymentDateElement = null; 
                    else
                        PaymentDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("PaymentDate");
                }
            }
            
            /// <summary>
            /// Who will make payment
            /// </summary>
            [FhirElement("responsible", Order=140)]
            [CLSCompliant(false)]
			[References("Organization","Patient","Practitioner","PractitionerRole","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Responsible
            {
                get { return _Responsible; }
                set { _Responsible = value; OnPropertyChanged("Responsible"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Responsible;
            
            /// <summary>
            /// Who will receive payment
            /// </summary>
            [FhirElement("recipient", Order=150)]
            [CLSCompliant(false)]
			[References("Organization","Patient","Practitioner","PractitionerRole","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Recipient
            {
                get { return _Recipient; }
                set { _Recipient = value; OnPropertyChanged("Recipient"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Recipient;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            [FhirElement("linkId", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LinkIdElement
            {
                get { if(_LinkIdElement==null) _LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _LinkIdElement;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> LinkId
            {
                get { return LinkIdElement != null ? LinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LinkIdElement = null; 
                    else
                        LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("LinkId");
                }
            }
            
            /// <summary>
            /// Security Labels that define affected terms
            /// </summary>
            [FhirElement("securityLabelNumber", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> SecurityLabelNumberElement
            {
                get { if(_SecurityLabelNumberElement==null) _SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _SecurityLabelNumberElement; }
                set { _SecurityLabelNumberElement = value; OnPropertyChanged("SecurityLabelNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _SecurityLabelNumberElement;
            
            /// <summary>
            /// Security Labels that define affected terms
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SecurityLabelNumber
            {
                get { return SecurityLabelNumberElement != null ? SecurityLabelNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SecurityLabelNumberElement = null; 
                    else
                        SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(value.Select(elem=>new Hl7.Fhir.Model.UnsignedInt(elem)));
                    OnPropertyChanged("SecurityLabelNumber");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValuedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Entity != null) dest.Entity = (Hl7.Fhir.Model.Element)Entity.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(EffectiveTimeElement != null) dest.EffectiveTimeElement = (Hl7.Fhir.Model.FhirDateTime)EffectiveTimeElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(PaymentElement != null) dest.PaymentElement = (Hl7.Fhir.Model.FhirString)PaymentElement.DeepCopy();
                    if(PaymentDateElement != null) dest.PaymentDateElement = (Hl7.Fhir.Model.FhirDateTime)PaymentDateElement.DeepCopy();
                    if(Responsible != null) dest.Responsible = (Hl7.Fhir.Model.ResourceReference)Responsible.DeepCopy();
                    if(Recipient != null) dest.Recipient = (Hl7.Fhir.Model.ResourceReference)Recipient.DeepCopy();
                    if(LinkIdElement != null) dest.LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(LinkIdElement.DeepCopy());
                    if(SecurityLabelNumberElement != null) dest.SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(SecurityLabelNumberElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValuedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValuedItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(EffectiveTimeElement, otherT.EffectiveTimeElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(PaymentElement, otherT.PaymentElement)) return false;
                if( !DeepComparable.Matches(PaymentDateElement, otherT.PaymentDateElement)) return false;
                if( !DeepComparable.Matches(Responsible, otherT.Responsible)) return false;
                if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValuedItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(EffectiveTimeElement, otherT.EffectiveTimeElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(PaymentElement, otherT.PaymentElement)) return false;
                if( !DeepComparable.IsExactly(PaymentDateElement, otherT.PaymentDateElement)) return false;
                if( !DeepComparable.IsExactly(Responsible, otherT.Responsible)) return false;
                if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Entity != null) yield return Entity;
                    if (Identifier != null) yield return Identifier;
                    if (EffectiveTimeElement != null) yield return EffectiveTimeElement;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (PointsElement != null) yield return PointsElement;
                    if (Net != null) yield return Net;
                    if (PaymentElement != null) yield return PaymentElement;
                    if (PaymentDateElement != null) yield return PaymentDateElement;
                    if (Responsible != null) yield return Responsible;
                    if (Recipient != null) yield return Recipient;
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Entity != null) yield return new ElementValue("entity", Entity);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (EffectiveTimeElement != null) yield return new ElementValue("effectiveTime", EffectiveTimeElement);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (PointsElement != null) yield return new ElementValue("points", PointsElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    if (PaymentElement != null) yield return new ElementValue("payment", PaymentElement);
                    if (PaymentDateElement != null) yield return new ElementValue("paymentDate", PaymentDateElement);
                    if (Responsible != null) yield return new ElementValue("responsible", Responsible);
                    if (Recipient != null) yield return new ElementValue("recipient", Recipient);
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return new ElementValue("linkId", elem); }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return new ElementValue("securityLabelNumber", elem); }
                }
            }

            
        }
        
        
        [FhirType("ActionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActionComponent"; } }
            
            /// <summary>
            /// True if the term prohibits the  action
            /// </summary>
            [FhirElement("doNotPerform", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean DoNotPerformElement
            {
                get { return _DoNotPerformElement; }
                set { _DoNotPerformElement = value; OnPropertyChanged("DoNotPerformElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _DoNotPerformElement;
            
            /// <summary>
            /// True if the term prohibits the  action
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? DoNotPerform
            {
                get { return DoNotPerformElement != null ? DoNotPerformElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DoNotPerformElement = null; 
                    else
                        DoNotPerformElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("DoNotPerform");
                }
            }
            
            /// <summary>
            /// Type or form of the action
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Entity of the action
            /// </summary>
            [FhirElement("subject", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contract.ActionSubjectComponent> Subject
            {
                get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.Contract.ActionSubjectComponent>(); return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            
            private List<Hl7.Fhir.Model.Contract.ActionSubjectComponent> _Subject;
            
            /// <summary>
            /// Purpose for the Contract Term Action
            /// </summary>
            [FhirElement("intent", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Intent
            {
                get { return _Intent; }
                set { _Intent = value; OnPropertyChanged("Intent"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Intent;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            [FhirElement("linkId", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LinkIdElement
            {
                get { if(_LinkIdElement==null) _LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _LinkIdElement;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> LinkId
            {
                get { return LinkIdElement != null ? LinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LinkIdElement = null; 
                    else
                        LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("LinkId");
                }
            }
            
            /// <summary>
            /// State of the action
            /// </summary>
            [FhirElement("status", Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Status
            {
                get { return _Status; }
                set { _Status = value; OnPropertyChanged("Status"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Status;
            
            /// <summary>
            /// Episode associated with action
            /// </summary>
            [FhirElement("context", Order=100)]
            [CLSCompliant(false)]
			[References("Encounter","EpisodeOfCare")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Context
            {
                get { return _Context; }
                set { _Context = value; OnPropertyChanged("Context"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Context;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            [FhirElement("contextLinkId", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ContextLinkIdElement
            {
                get { if(_ContextLinkIdElement==null) _ContextLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _ContextLinkIdElement; }
                set { _ContextLinkIdElement = value; OnPropertyChanged("ContextLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ContextLinkIdElement;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> ContextLinkId
            {
                get { return ContextLinkIdElement != null ? ContextLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ContextLinkIdElement = null; 
                    else
                        ContextLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("ContextLinkId");
                }
            }
            
            /// <summary>
            /// When action happens
            /// </summary>
            [FhirElement("occurrence", Order=120, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Timing))]
            [DataMember]
            public Hl7.Fhir.Model.Element Occurrence
            {
                get { return _Occurrence; }
                set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
            }
            
            private Hl7.Fhir.Model.Element _Occurrence;
            
            /// <summary>
            /// Who asked for action
            /// </summary>
            [FhirElement("requester", Order=130)]
            [CLSCompliant(false)]
			[References("Patient","RelatedPerson","Practitioner","PractitionerRole","Device","Group","Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Requester
            {
                get { if(_Requester==null) _Requester = new List<Hl7.Fhir.Model.ResourceReference>(); return _Requester; }
                set { _Requester = value; OnPropertyChanged("Requester"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Requester;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            [FhirElement("requesterLinkId", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> RequesterLinkIdElement
            {
                get { if(_RequesterLinkIdElement==null) _RequesterLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _RequesterLinkIdElement; }
                set { _RequesterLinkIdElement = value; OnPropertyChanged("RequesterLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _RequesterLinkIdElement;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> RequesterLinkId
            {
                get { return RequesterLinkIdElement != null ? RequesterLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        RequesterLinkIdElement = null; 
                    else
                        RequesterLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("RequesterLinkId");
                }
            }
            
            /// <summary>
            /// Kind of service performer
            /// </summary>
            [FhirElement("performerType", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> PerformerType
            {
                get { if(_PerformerType==null) _PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PerformerType; }
                set { _PerformerType = value; OnPropertyChanged("PerformerType"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _PerformerType;
            
            /// <summary>
            /// Competency of the performer
            /// </summary>
            [FhirElement("performerRole", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept PerformerRole
            {
                get { return _PerformerRole; }
                set { _PerformerRole = value; OnPropertyChanged("PerformerRole"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _PerformerRole;
            
            /// <summary>
            /// Actor that wil execute (or not) the action
            /// </summary>
            [FhirElement("performer", Order=170)]
            [CLSCompliant(false)]
			[References("RelatedPerson","Patient","Practitioner","PractitionerRole","CareTeam","Device","Substance","Organization","Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Performer
            {
                get { return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Performer;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            [FhirElement("performerLinkId", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PerformerLinkIdElement
            {
                get { if(_PerformerLinkIdElement==null) _PerformerLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _PerformerLinkIdElement; }
                set { _PerformerLinkIdElement = value; OnPropertyChanged("PerformerLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _PerformerLinkIdElement;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> PerformerLinkId
            {
                get { return PerformerLinkIdElement != null ? PerformerLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PerformerLinkIdElement = null; 
                    else
                        PerformerLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("PerformerLinkId");
                }
            }
            
            /// <summary>
            /// Why is action (not) needed?
            /// </summary>
            [FhirElement("reasonCode", Order=190)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
            {
                get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
                set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
            
            /// <summary>
            /// Why is action (not) needed?
            /// </summary>
            [FhirElement("reasonReference", Order=200)]
            [CLSCompliant(false)]
			[References("Condition","Observation","DiagnosticReport","DocumentReference","Questionnaire","QuestionnaireResponse")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
            {
                get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
                set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
            
            /// <summary>
            /// Why action is to be performed
            /// </summary>
            [FhirElement("reason", Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ReasonElement
            {
                get { if(_ReasonElement==null) _ReasonElement = new List<Hl7.Fhir.Model.FhirString>(); return _ReasonElement; }
                set { _ReasonElement = value; OnPropertyChanged("ReasonElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ReasonElement;
            
            /// <summary>
            /// Why action is to be performed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Reason
            {
                get { return ReasonElement != null ? ReasonElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ReasonElement = null; 
                    else
                        ReasonElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Reason");
                }
            }
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            [FhirElement("reasonLinkId", Order=220)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ReasonLinkIdElement
            {
                get { if(_ReasonLinkIdElement==null) _ReasonLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(); return _ReasonLinkIdElement; }
                set { _ReasonLinkIdElement = value; OnPropertyChanged("ReasonLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ReasonLinkIdElement;
            
            /// <summary>
            /// Pointer to specific item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> ReasonLinkId
            {
                get { return ReasonLinkIdElement != null ? ReasonLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ReasonLinkIdElement = null; 
                    else
                        ReasonLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("ReasonLinkId");
                }
            }
            
            /// <summary>
            /// Comments about the action
            /// </summary>
            [FhirElement("note", Order=230)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            /// <summary>
            /// Action restriction numbers
            /// </summary>
            [FhirElement("securityLabelNumber", Order=240)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> SecurityLabelNumberElement
            {
                get { if(_SecurityLabelNumberElement==null) _SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _SecurityLabelNumberElement; }
                set { _SecurityLabelNumberElement = value; OnPropertyChanged("SecurityLabelNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _SecurityLabelNumberElement;
            
            /// <summary>
            /// Action restriction numbers
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SecurityLabelNumber
            {
                get { return SecurityLabelNumberElement != null ? SecurityLabelNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SecurityLabelNumberElement = null; 
                    else
                        SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(value.Select(elem=>new Hl7.Fhir.Model.UnsignedInt(elem)));
                    OnPropertyChanged("SecurityLabelNumber");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DoNotPerformElement != null) dest.DoNotPerformElement = (Hl7.Fhir.Model.FhirBoolean)DoNotPerformElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.Contract.ActionSubjectComponent>(Subject.DeepCopy());
                    if(Intent != null) dest.Intent = (Hl7.Fhir.Model.CodeableConcept)Intent.DeepCopy();
                    if(LinkIdElement != null) dest.LinkIdElement = new List<Hl7.Fhir.Model.FhirString>(LinkIdElement.DeepCopy());
                    if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
                    if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                    if(ContextLinkIdElement != null) dest.ContextLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(ContextLinkIdElement.DeepCopy());
                    if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                    if(Requester != null) dest.Requester = new List<Hl7.Fhir.Model.ResourceReference>(Requester.DeepCopy());
                    if(RequesterLinkIdElement != null) dest.RequesterLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(RequesterLinkIdElement.DeepCopy());
                    if(PerformerType != null) dest.PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(PerformerType.DeepCopy());
                    if(PerformerRole != null) dest.PerformerRole = (Hl7.Fhir.Model.CodeableConcept)PerformerRole.DeepCopy();
                    if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                    if(PerformerLinkIdElement != null) dest.PerformerLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(PerformerLinkIdElement.DeepCopy());
                    if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                    if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                    if(ReasonElement != null) dest.ReasonElement = new List<Hl7.Fhir.Model.FhirString>(ReasonElement.DeepCopy());
                    if(ReasonLinkIdElement != null) dest.ReasonLinkIdElement = new List<Hl7.Fhir.Model.FhirString>(ReasonLinkIdElement.DeepCopy());
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    if(SecurityLabelNumberElement != null) dest.SecurityLabelNumberElement = new List<Hl7.Fhir.Model.UnsignedInt>(SecurityLabelNumberElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
                if( !DeepComparable.Matches(Intent, otherT.Intent)) return false;
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(Status, otherT.Status)) return false;
                if( !DeepComparable.Matches(Context, otherT.Context)) return false;
                if( !DeepComparable.Matches(ContextLinkIdElement, otherT.ContextLinkIdElement)) return false;
                if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
                if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
                if( !DeepComparable.Matches(RequesterLinkIdElement, otherT.RequesterLinkIdElement)) return false;
                if( !DeepComparable.Matches(PerformerType, otherT.PerformerType)) return false;
                if( !DeepComparable.Matches(PerformerRole, otherT.PerformerRole)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(PerformerLinkIdElement, otherT.PerformerLinkIdElement)) return false;
                if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
                if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
                if( !DeepComparable.Matches(ReasonElement, otherT.ReasonElement)) return false;
                if( !DeepComparable.Matches(ReasonLinkIdElement, otherT.ReasonLinkIdElement)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                if( !DeepComparable.Matches(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
                if( !DeepComparable.IsExactly(Intent, otherT.Intent)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
                if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
                if( !DeepComparable.IsExactly(ContextLinkIdElement, otherT.ContextLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
                if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
                if( !DeepComparable.IsExactly(RequesterLinkIdElement, otherT.RequesterLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(PerformerType, otherT.PerformerType)) return false;
                if( !DeepComparable.IsExactly(PerformerRole, otherT.PerformerRole)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(PerformerLinkIdElement, otherT.PerformerLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
                if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
                if( !DeepComparable.IsExactly(ReasonElement, otherT.ReasonElement)) return false;
                if( !DeepComparable.IsExactly(ReasonLinkIdElement, otherT.ReasonLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                if( !DeepComparable.IsExactly(SecurityLabelNumberElement, otherT.SecurityLabelNumberElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DoNotPerformElement != null) yield return DoNotPerformElement;
                    if (Type != null) yield return Type;
                    foreach (var elem in Subject) { if (elem != null) yield return elem; }
                    if (Intent != null) yield return Intent;
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return elem; }
                    if (Status != null) yield return Status;
                    if (Context != null) yield return Context;
                    foreach (var elem in ContextLinkIdElement) { if (elem != null) yield return elem; }
                    if (Occurrence != null) yield return Occurrence;
                    foreach (var elem in Requester) { if (elem != null) yield return elem; }
                    foreach (var elem in RequesterLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in PerformerType) { if (elem != null) yield return elem; }
                    if (PerformerRole != null) yield return PerformerRole;
                    if (Performer != null) yield return Performer;
                    foreach (var elem in PerformerLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                    foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                    foreach (var elem in ReasonElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ReasonLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DoNotPerformElement != null) yield return new ElementValue("doNotPerform", DoNotPerformElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                    if (Intent != null) yield return new ElementValue("intent", Intent);
                    foreach (var elem in LinkIdElement) { if (elem != null) yield return new ElementValue("linkId", elem); }
                    if (Status != null) yield return new ElementValue("status", Status);
                    if (Context != null) yield return new ElementValue("context", Context);
                    foreach (var elem in ContextLinkIdElement) { if (elem != null) yield return new ElementValue("contextLinkId", elem); }
                    if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                    foreach (var elem in Requester) { if (elem != null) yield return new ElementValue("requester", elem); }
                    foreach (var elem in RequesterLinkIdElement) { if (elem != null) yield return new ElementValue("requesterLinkId", elem); }
                    foreach (var elem in PerformerType) { if (elem != null) yield return new ElementValue("performerType", elem); }
                    if (PerformerRole != null) yield return new ElementValue("performerRole", PerformerRole);
                    if (Performer != null) yield return new ElementValue("performer", Performer);
                    foreach (var elem in PerformerLinkIdElement) { if (elem != null) yield return new ElementValue("performerLinkId", elem); }
                    foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                    foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                    foreach (var elem in ReasonElement) { if (elem != null) yield return new ElementValue("reason", elem); }
                    foreach (var elem in ReasonLinkIdElement) { if (elem != null) yield return new ElementValue("reasonLinkId", elem); }
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                    foreach (var elem in SecurityLabelNumberElement) { if (elem != null) yield return new ElementValue("securityLabelNumber", elem); }
                }
            }

            
        }
        
        
        [FhirType("ActionSubjectComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ActionSubjectComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActionSubjectComponent"; } }
            
            /// <summary>
            /// Entity of the action
            /// </summary>
            [FhirElement("reference", Order=40)]
            [CLSCompliant(false)]
			[References("Patient","RelatedPerson","Practitioner","PractitionerRole","Device","Group","Organization")]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Reference
            {
                get { if(_Reference==null) _Reference = new List<Hl7.Fhir.Model.ResourceReference>(); return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Reference;
            
            /// <summary>
            /// Role type of the agent
            /// </summary>
            [FhirElement("role", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionSubjectComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reference != null) dest.Reference = new List<Hl7.Fhir.Model.ResourceReference>(Reference.DeepCopy());
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionSubjectComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionSubjectComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionSubjectComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Reference) { if (elem != null) yield return elem; }
                    if (Role != null) yield return Role;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Reference) { if (elem != null) yield return new ElementValue("reference", elem); }
                    if (Role != null) yield return new ElementValue("role", Role);
                }
            }

            
        }
        
        
        [FhirType("SignatoryComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SignatoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SignatoryComponent"; } }
            
            /// <summary>
            /// Contract Signatory Role
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Contract Signatory Party
            /// </summary>
            [FhirElement("party", Order=50)]
            [CLSCompliant(false)]
			[References("Organization","Patient","Practitioner","PractitionerRole","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
            
            /// <summary>
            /// Contract Documentation Signature
            /// </summary>
            [FhirElement("signature", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Signature> Signature
            {
                get { if(_Signature==null) _Signature = new List<Hl7.Fhir.Model.Signature>(); return _Signature; }
                set { _Signature = value; OnPropertyChanged("Signature"); }
            }
            
            private List<Hl7.Fhir.Model.Signature> _Signature;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SignatoryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
                    if(Signature != null) dest.Signature = new List<Hl7.Fhir.Model.Signature>(Signature.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SignatoryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SignatoryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                if( !DeepComparable.Matches(Signature, otherT.Signature)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SignatoryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                if( !DeepComparable.IsExactly(Signature, otherT.Signature)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Party != null) yield return Party;
                    foreach (var elem in Signature) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Party != null) yield return new ElementValue("party", Party);
                    foreach (var elem in Signature) { if (elem != null) yield return new ElementValue("signature", elem); }
                }
            }

            
        }
        
        
        [FhirType("FriendlyLanguageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class FriendlyLanguageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FriendlyLanguageComponent"; } }
            
            /// <summary>
            /// Easily comprehended representation of this Contract
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FriendlyLanguageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FriendlyLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FriendlyLanguageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FriendlyLanguageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }

            
        }
        
        
        [FhirType("LegalLanguageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class LegalLanguageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "LegalLanguageComponent"; } }
            
            /// <summary>
            /// Contract Legal Text
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LegalLanguageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LegalLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LegalLanguageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LegalLanguageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }

            
        }
        
        
        [FhirType("ComputableLanguageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ComputableLanguageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ComputableLanguageComponent"; } }
            
            /// <summary>
            /// Computable Contract Rules
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComputableLanguageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ComputableLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComputableLanguageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComputableLanguageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }

            
        }
        
        
        /// <summary>
        /// Contract number
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
        /// Basal definition
        /// </summary>
        [FhirElement("url", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Basal definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Business edition
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business edition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// draft | active | suspended | cancelled | completed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Contract.ContractResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Contract.ContractResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// draft | active | suspended | cancelled | completed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Contract.ContractResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Contract.ContractResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Negotiation status
        /// </summary>
        [FhirElement("legalState", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept LegalState
        {
            get { return _LegalState; }
            set { _LegalState = value; OnPropertyChanged("LegalState"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _LegalState;
        
        /// <summary>
        /// Source Contract Definition
        /// </summary>
        [FhirElement("instantiatesCanonical", Order=140)]
        [CLSCompliant(false)]
		[References("Contract")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference InstantiatesCanonical
        {
            get { return _InstantiatesCanonical; }
            set { _InstantiatesCanonical = value; OnPropertyChanged("InstantiatesCanonical"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _InstantiatesCanonical;
        
        /// <summary>
        /// External Contract Definition
        /// </summary>
        [FhirElement("instantiatesUri", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri InstantiatesUriElement
        {
            get { return _InstantiatesUriElement; }
            set { _InstantiatesUriElement = value; OnPropertyChanged("InstantiatesUriElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _InstantiatesUriElement;
        
        /// <summary>
        /// External Contract Definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string InstantiatesUri
        {
            get { return InstantiatesUriElement != null ? InstantiatesUriElement.Value : null; }
            set
            {
                if (value == null)
                  InstantiatesUriElement = null; 
                else
                  InstantiatesUriElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("InstantiatesUri");
            }
        }
        
        /// <summary>
        /// Content derived from the basal information
        /// </summary>
        [FhirElement("contentDerivative", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ContentDerivative
        {
            get { return _ContentDerivative; }
            set { _ContentDerivative = value; OnPropertyChanged("ContentDerivative"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ContentDerivative;
        
        /// <summary>
        /// When this Contract was issued
        /// </summary>
        [FhirElement("issued", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
        
        /// <summary>
        /// When this Contract was issued
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if (value == null)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// Effective time
        /// </summary>
        [FhirElement("applies", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Period Applies
        {
            get { return _Applies; }
            set { _Applies = value; OnPropertyChanged("Applies"); }
        }
        
        private Hl7.Fhir.Model.Period _Applies;
        
        /// <summary>
        /// Contract cessation cause
        /// </summary>
        [FhirElement("expirationType", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ExpirationType
        {
            get { return _ExpirationType; }
            set { _ExpirationType = value; OnPropertyChanged("ExpirationType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ExpirationType;
        
        /// <summary>
        /// Contract Target Entity
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject
        {
            get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.ResourceReference>(); return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Subject;
        
        /// <summary>
        /// Authority under which this Contract has standing
        /// </summary>
        [FhirElement("authority", Order=210)]
        [CLSCompliant(false)]
		[References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Authority
        {
            get { if(_Authority==null) _Authority = new List<Hl7.Fhir.Model.ResourceReference>(); return _Authority; }
            set { _Authority = value; OnPropertyChanged("Authority"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Authority;
        
        /// <summary>
        /// A sphere of control governed by an authoritative jurisdiction, organization, or person
        /// </summary>
        [FhirElement("domain", Order=220)]
        [CLSCompliant(false)]
		[References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Domain
        {
            get { if(_Domain==null) _Domain = new List<Hl7.Fhir.Model.ResourceReference>(); return _Domain; }
            set { _Domain = value; OnPropertyChanged("Domain"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Domain;
        
        /// <summary>
        /// Specific Location
        /// </summary>
        [FhirElement("site", Order=230)]
        [CLSCompliant(false)]
		[References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Site
        {
            get { if(_Site==null) _Site = new List<Hl7.Fhir.Model.ResourceReference>(); return _Site; }
            set { _Site = value; OnPropertyChanged("Site"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Site;
        
        /// <summary>
        /// Computer friendly designation
        /// </summary>
        [FhirElement("name", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Computer friendly designation
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
        /// Human Friendly name
        /// </summary>
        [FhirElement("title", InSummary=true, Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Human Friendly name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// Subordinate Friendly name
        /// </summary>
        [FhirElement("subtitle", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SubtitleElement
        {
            get { return _SubtitleElement; }
            set { _SubtitleElement = value; OnPropertyChanged("SubtitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SubtitleElement;
        
        /// <summary>
        /// Subordinate Friendly name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Subtitle
        {
            get { return SubtitleElement != null ? SubtitleElement.Value : null; }
            set
            {
                if (value == null)
                  SubtitleElement = null; 
                else
                  SubtitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Subtitle");
            }
        }
        
        /// <summary>
        /// Acronym or short name
        /// </summary>
        [FhirElement("alias", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if(_AliasElement==null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _AliasElement;
        
        /// <summary>
        /// Acronym or short name
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
        /// Source of Contract
        /// </summary>
        [FhirElement("author", Order=280)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Range of Legal Concerns
        /// </summary>
        [FhirElement("scope", Order=290)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Scope
        {
            get { return _Scope; }
            set { _Scope = value; OnPropertyChanged("Scope"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Scope;
        
        /// <summary>
        /// Focus of contract interest
        /// </summary>
        [FhirElement("topic", Order=300, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Topic
        {
            get { return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private Hl7.Fhir.Model.Element _Topic;
        
        /// <summary>
        /// Legal instrument category
        /// </summary>
        [FhirElement("type", InSummary=true, Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Subtype within the context of type
        /// </summary>
        [FhirElement("subType", InSummary=true, Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> SubType
        {
            get { if(_SubType==null) _SubType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubType; }
            set { _SubType = value; OnPropertyChanged("SubType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _SubType;
        
        /// <summary>
        /// Contract precursor content
        /// </summary>
        [FhirElement("contentDefinition", Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.Contract.ContentDefinitionComponent ContentDefinition
        {
            get { return _ContentDefinition; }
            set { _ContentDefinition = value; OnPropertyChanged("ContentDefinition"); }
        }
        
        private Hl7.Fhir.Model.Contract.ContentDefinitionComponent _ContentDefinition;
        
        /// <summary>
        /// Contract Term List
        /// </summary>
        [FhirElement("term", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.TermComponent> Term
        {
            get { if(_Term==null) _Term = new List<Hl7.Fhir.Model.Contract.TermComponent>(); return _Term; }
            set { _Term = value; OnPropertyChanged("Term"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.TermComponent> _Term;
        
        /// <summary>
        /// Extra Information
        /// </summary>
        [FhirElement("supportingInfo", Order=350)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInfo
        {
            get { if(_SupportingInfo==null) _SupportingInfo = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInfo; }
            set { _SupportingInfo = value; OnPropertyChanged("SupportingInfo"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInfo;
        
        /// <summary>
        /// Key event in Contract History
        /// </summary>
        [FhirElement("relevantHistory", Order=360)]
        [CLSCompliant(false)]
		[References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> RelevantHistory
        {
            get { if(_RelevantHistory==null) _RelevantHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _RelevantHistory; }
            set { _RelevantHistory = value; OnPropertyChanged("RelevantHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _RelevantHistory;
        
        /// <summary>
        /// Contract Signatory
        /// </summary>
        [FhirElement("signer", Order=370)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.SignatoryComponent> Signer
        {
            get { if(_Signer==null) _Signer = new List<Hl7.Fhir.Model.Contract.SignatoryComponent>(); return _Signer; }
            set { _Signer = value; OnPropertyChanged("Signer"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.SignatoryComponent> _Signer;
        
        /// <summary>
        /// Contract Friendly Language
        /// </summary>
        [FhirElement("friendly", Order=380)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.FriendlyLanguageComponent> Friendly
        {
            get { if(_Friendly==null) _Friendly = new List<Hl7.Fhir.Model.Contract.FriendlyLanguageComponent>(); return _Friendly; }
            set { _Friendly = value; OnPropertyChanged("Friendly"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.FriendlyLanguageComponent> _Friendly;
        
        /// <summary>
        /// Contract Legal Language
        /// </summary>
        [FhirElement("legal", Order=390)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.LegalLanguageComponent> Legal
        {
            get { if(_Legal==null) _Legal = new List<Hl7.Fhir.Model.Contract.LegalLanguageComponent>(); return _Legal; }
            set { _Legal = value; OnPropertyChanged("Legal"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.LegalLanguageComponent> _Legal;
        
        /// <summary>
        /// Computable Contract Language
        /// </summary>
        [FhirElement("rule", Order=400)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.ComputableLanguageComponent> Rule
        {
            get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.Contract.ComputableLanguageComponent>(); return _Rule; }
            set { _Rule = value; OnPropertyChanged("Rule"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.ComputableLanguageComponent> _Rule;
        
        /// <summary>
        /// Binding Contract
        /// </summary>
        [FhirElement("legallyBinding", Order=410, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element LegallyBinding
        {
            get { return _LegallyBinding; }
            set { _LegallyBinding = value; OnPropertyChanged("LegallyBinding"); }
        }
        
        private Hl7.Fhir.Model.Element _LegallyBinding;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Contract;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Contract.ContractResourceStatusCodes>)StatusElement.DeepCopy();
                if(LegalState != null) dest.LegalState = (Hl7.Fhir.Model.CodeableConcept)LegalState.DeepCopy();
                if(InstantiatesCanonical != null) dest.InstantiatesCanonical = (Hl7.Fhir.Model.ResourceReference)InstantiatesCanonical.DeepCopy();
                if(InstantiatesUriElement != null) dest.InstantiatesUriElement = (Hl7.Fhir.Model.FhirUri)InstantiatesUriElement.DeepCopy();
                if(ContentDerivative != null) dest.ContentDerivative = (Hl7.Fhir.Model.CodeableConcept)ContentDerivative.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                if(ExpirationType != null) dest.ExpirationType = (Hl7.Fhir.Model.CodeableConcept)ExpirationType.DeepCopy();
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(Authority != null) dest.Authority = new List<Hl7.Fhir.Model.ResourceReference>(Authority.DeepCopy());
                if(Domain != null) dest.Domain = new List<Hl7.Fhir.Model.ResourceReference>(Domain.DeepCopy());
                if(Site != null) dest.Site = new List<Hl7.Fhir.Model.ResourceReference>(Site.DeepCopy());
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(SubtitleElement != null) dest.SubtitleElement = (Hl7.Fhir.Model.FhirString)SubtitleElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Scope != null) dest.Scope = (Hl7.Fhir.Model.CodeableConcept)Scope.DeepCopy();
                if(Topic != null) dest.Topic = (Hl7.Fhir.Model.Element)Topic.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SubType != null) dest.SubType = new List<Hl7.Fhir.Model.CodeableConcept>(SubType.DeepCopy());
                if(ContentDefinition != null) dest.ContentDefinition = (Hl7.Fhir.Model.Contract.ContentDefinitionComponent)ContentDefinition.DeepCopy();
                if(Term != null) dest.Term = new List<Hl7.Fhir.Model.Contract.TermComponent>(Term.DeepCopy());
                if(SupportingInfo != null) dest.SupportingInfo = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInfo.DeepCopy());
                if(RelevantHistory != null) dest.RelevantHistory = new List<Hl7.Fhir.Model.ResourceReference>(RelevantHistory.DeepCopy());
                if(Signer != null) dest.Signer = new List<Hl7.Fhir.Model.Contract.SignatoryComponent>(Signer.DeepCopy());
                if(Friendly != null) dest.Friendly = new List<Hl7.Fhir.Model.Contract.FriendlyLanguageComponent>(Friendly.DeepCopy());
                if(Legal != null) dest.Legal = new List<Hl7.Fhir.Model.Contract.LegalLanguageComponent>(Legal.DeepCopy());
                if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.Contract.ComputableLanguageComponent>(Rule.DeepCopy());
                if(LegallyBinding != null) dest.LegallyBinding = (Hl7.Fhir.Model.Element)LegallyBinding.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Contract());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Contract;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(LegalState, otherT.LegalState)) return false;
            if( !DeepComparable.Matches(InstantiatesCanonical, otherT.InstantiatesCanonical)) return false;
            if( !DeepComparable.Matches(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.Matches(ContentDerivative, otherT.ContentDerivative)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
            if( !DeepComparable.Matches(ExpirationType, otherT.ExpirationType)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
            if( !DeepComparable.Matches(Domain, otherT.Domain)) return false;
            if( !DeepComparable.Matches(Site, otherT.Site)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(SubtitleElement, otherT.SubtitleElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Scope, otherT.Scope)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
            if( !DeepComparable.Matches(ContentDefinition, otherT.ContentDefinition)) return false;
            if( !DeepComparable.Matches(Term, otherT.Term)) return false;
            if( !DeepComparable.Matches(SupportingInfo, otherT.SupportingInfo)) return false;
            if( !DeepComparable.Matches(RelevantHistory, otherT.RelevantHistory)) return false;
            if( !DeepComparable.Matches(Signer, otherT.Signer)) return false;
            if( !DeepComparable.Matches(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.Matches(Legal, otherT.Legal)) return false;
            if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            if( !DeepComparable.Matches(LegallyBinding, otherT.LegallyBinding)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Contract;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(LegalState, otherT.LegalState)) return false;
            if( !DeepComparable.IsExactly(InstantiatesCanonical, otherT.InstantiatesCanonical)) return false;
            if( !DeepComparable.IsExactly(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.IsExactly(ContentDerivative, otherT.ContentDerivative)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
            if( !DeepComparable.IsExactly(ExpirationType, otherT.ExpirationType)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
            if( !DeepComparable.IsExactly(Domain, otherT.Domain)) return false;
            if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(SubtitleElement, otherT.SubtitleElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Scope, otherT.Scope)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
            if( !DeepComparable.IsExactly(ContentDefinition, otherT.ContentDefinition)) return false;
            if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
            if( !DeepComparable.IsExactly(SupportingInfo, otherT.SupportingInfo)) return false;
            if( !DeepComparable.IsExactly(RelevantHistory, otherT.RelevantHistory)) return false;
            if( !DeepComparable.IsExactly(Signer, otherT.Signer)) return false;
            if( !DeepComparable.IsExactly(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.IsExactly(Legal, otherT.Legal)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            if( !DeepComparable.IsExactly(LegallyBinding, otherT.LegallyBinding)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (UrlElement != null) yield return UrlElement;
				if (VersionElement != null) yield return VersionElement;
				if (StatusElement != null) yield return StatusElement;
				if (LegalState != null) yield return LegalState;
				if (InstantiatesCanonical != null) yield return InstantiatesCanonical;
				if (InstantiatesUriElement != null) yield return InstantiatesUriElement;
				if (ContentDerivative != null) yield return ContentDerivative;
				if (IssuedElement != null) yield return IssuedElement;
				if (Applies != null) yield return Applies;
				if (ExpirationType != null) yield return ExpirationType;
				foreach (var elem in Subject) { if (elem != null) yield return elem; }
				foreach (var elem in Authority) { if (elem != null) yield return elem; }
				foreach (var elem in Domain) { if (elem != null) yield return elem; }
				foreach (var elem in Site) { if (elem != null) yield return elem; }
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
				if (SubtitleElement != null) yield return SubtitleElement;
				foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
				if (Author != null) yield return Author;
				if (Scope != null) yield return Scope;
				if (Topic != null) yield return Topic;
				if (Type != null) yield return Type;
				foreach (var elem in SubType) { if (elem != null) yield return elem; }
				if (ContentDefinition != null) yield return ContentDefinition;
				foreach (var elem in Term) { if (elem != null) yield return elem; }
				foreach (var elem in SupportingInfo) { if (elem != null) yield return elem; }
				foreach (var elem in RelevantHistory) { if (elem != null) yield return elem; }
				foreach (var elem in Signer) { if (elem != null) yield return elem; }
				foreach (var elem in Friendly) { if (elem != null) yield return elem; }
				foreach (var elem in Legal) { if (elem != null) yield return elem; }
				foreach (var elem in Rule) { if (elem != null) yield return elem; }
				if (LegallyBinding != null) yield return LegallyBinding;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (LegalState != null) yield return new ElementValue("legalState", LegalState);
                if (InstantiatesCanonical != null) yield return new ElementValue("instantiatesCanonical", InstantiatesCanonical);
                if (InstantiatesUriElement != null) yield return new ElementValue("instantiatesUri", InstantiatesUriElement);
                if (ContentDerivative != null) yield return new ElementValue("contentDerivative", ContentDerivative);
                if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                if (Applies != null) yield return new ElementValue("applies", Applies);
                if (ExpirationType != null) yield return new ElementValue("expirationType", ExpirationType);
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                foreach (var elem in Authority) { if (elem != null) yield return new ElementValue("authority", elem); }
                foreach (var elem in Domain) { if (elem != null) yield return new ElementValue("domain", elem); }
                foreach (var elem in Site) { if (elem != null) yield return new ElementValue("site", elem); }
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (SubtitleElement != null) yield return new ElementValue("subtitle", SubtitleElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (Author != null) yield return new ElementValue("author", Author);
                if (Scope != null) yield return new ElementValue("scope", Scope);
                if (Topic != null) yield return new ElementValue("topic", Topic);
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in SubType) { if (elem != null) yield return new ElementValue("subType", elem); }
                if (ContentDefinition != null) yield return new ElementValue("contentDefinition", ContentDefinition);
                foreach (var elem in Term) { if (elem != null) yield return new ElementValue("term", elem); }
                foreach (var elem in SupportingInfo) { if (elem != null) yield return new ElementValue("supportingInfo", elem); }
                foreach (var elem in RelevantHistory) { if (elem != null) yield return new ElementValue("relevantHistory", elem); }
                foreach (var elem in Signer) { if (elem != null) yield return new ElementValue("signer", elem); }
                foreach (var elem in Friendly) { if (elem != null) yield return new ElementValue("friendly", elem); }
                foreach (var elem in Legal) { if (elem != null) yield return new ElementValue("legal", elem); }
                foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                if (LegallyBinding != null) yield return new ElementValue("legallyBinding", LegallyBinding);
            }
        }

    }
    
}
