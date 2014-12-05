using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Fri, Dec 5, 2014 10:08+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    [FhirType("Contract", IsResource=true)]
    [DataContract]
    public partial class Contract : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        public override ResourceType ResourceType { get { return ResourceType.Contract; } }
        public override string TypeName { get { return "Contract"; } }
        
        [FhirType("ContractSignerComponent")]
        [DataContract]
        public partial class ContractSignerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            public override string TypeName { get { return "ContractSignerComponent"; } }
            
            /// <summary>
            /// Signer Type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Documentation Signature
            /// </summary>
            [FhirElement("singnature", InSummary=true, Order=30)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SingnatureElement
            {
                get { return _SingnatureElement; }
                set { _SingnatureElement = value; OnPropertyChanged("SingnatureElement"); }
            }
            private Hl7.Fhir.Model.FhirString _SingnatureElement;
            
            /// <summary>
            /// Documentation Signature
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Singnature
            {
                get { return SingnatureElement != null ? SingnatureElement.Value : null; }
                set
                {
                    if(value == null)
                      SingnatureElement = null; 
                    else
                      SingnatureElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Singnature");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContractSignerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(SingnatureElement != null) dest.SingnatureElement = (Hl7.Fhir.Model.FhirString)SingnatureElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContractSignerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContractSignerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(SingnatureElement, otherT.SingnatureElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContractSignerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(SingnatureElement, otherT.SingnatureElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ContractTermComponent")]
        [DataContract]
        public partial class ContractTermComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            public override string TypeName { get { return "ContractTermComponent"; } }
            
            /// <summary>
            /// Term identifier
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=20)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Term type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=30)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Term subtype
            /// </summary>
            [FhirElement("subtype", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Subtype
            {
                get { return _Subtype; }
                set { _Subtype = value; OnPropertyChanged("Subtype"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Subtype;
            
            /// <summary>
            /// Subject for the Term
            /// </summary>
            [FhirElement("subject", InSummary=true, Order=50)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject
            {
                get { return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Subject;
            
            /// <summary>
            /// Human readable term text
            /// </summary>
            [FhirElement("text", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Human readable term text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if(value == null)
                      TextElement = null; 
                    else
                      TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContractTermComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Subtype != null) dest.Subtype = (Hl7.Fhir.Model.CodeableConcept)Subtype.DeepCopy();
                    if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContractTermComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContractTermComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContractTermComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Contract identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=50)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Subject
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=60)]
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
        /// Type of contract
        /// </summary>
        [FhirElement("type", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Subtype of contract
        /// </summary>
        [FhirElement("subtype", InSummary=true, Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Subtype
        {
            get { if(_Subtype==null) _Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Subtype; }
            set { _Subtype = value; OnPropertyChanged("Subtype"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Subtype;
        
        /// <summary>
        /// When this was issued
        /// </summary>
        [FhirElement("issued", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
        
        /// <summary>
        /// When this was issued
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if(value == null)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// Effective time
        /// </summary>
        [FhirElement("applies", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Period Applies
        {
            get { return _Applies; }
            set { _Applies = value; OnPropertyChanged("Applies"); }
        }
        private Hl7.Fhir.Model.Period _Applies;
        
        /// <summary>
        /// Count of Products or Services
        /// </summary>
        [FhirElement("quantity", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        private Hl7.Fhir.Model.Quantity _Quantity;
        
        /// <summary>
        /// Fee, charge or cost per point
        /// </summary>
        [FhirElement("unitPrice", Order=120)]
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
        [FhirElement("factor", Order=130)]
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
                if(value == null)
                  FactorElement = null; 
                else
                  FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Factor");
            }
        }
        
        /// <summary>
        /// Difficulty scaling factor
        /// </summary>
        [FhirElement("points", Order=140)]
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
                if(value == null)
                  PointsElement = null; 
                else
                  PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Points");
            }
        }
        
        /// <summary>
        /// Total item cost
        /// </summary>
        [FhirElement("net", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Money Net
        {
            get { return _Net; }
            set { _Net = value; OnPropertyChanged("Net"); }
        }
        private Hl7.Fhir.Model.Money _Net;
        
        /// <summary>
        /// Contract author or responsible party
        /// </summary>
        [FhirElement("author", InSummary=true, Order=160)]
        [References("Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.ResourceReference>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Author;
        
        /// <summary>
        /// First Party or delegator
        /// </summary>
        [FhirElement("grantor", InSummary=true, Order=170)]
        [References("Practitioner","RelatedPerson","Organization","Patient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Grantor
        {
            get { if(_Grantor==null) _Grantor = new List<Hl7.Fhir.Model.ResourceReference>(); return _Grantor; }
            set { _Grantor = value; OnPropertyChanged("Grantor"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Grantor;
        
        /// <summary>
        /// Second Party or delegatee
        /// </summary>
        [FhirElement("grantee", InSummary=true, Order=180)]
        [References("Practitioner","RelatedPerson","Organization","Patient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Grantee
        {
            get { if(_Grantee==null) _Grantee = new List<Hl7.Fhir.Model.ResourceReference>(); return _Grantee; }
            set { _Grantee = value; OnPropertyChanged("Grantee"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Grantee;
        
        /// <summary>
        /// Witness to the contract
        /// </summary>
        [FhirElement("witness", Order=190)]
        [References("Practitioner","RelatedPerson","Patient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Witness
        {
            get { if(_Witness==null) _Witness = new List<Hl7.Fhir.Model.ResourceReference>(); return _Witness; }
            set { _Witness = value; OnPropertyChanged("Witness"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Witness;
        
        /// <summary>
        /// Trustee
        /// </summary>
        [FhirElement("executor", InSummary=true, Order=200)]
        [References("Practitioner","RelatedPerson","Organization","Patient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Executor
        {
            get { if(_Executor==null) _Executor = new List<Hl7.Fhir.Model.ResourceReference>(); return _Executor; }
            set { _Executor = value; OnPropertyChanged("Executor"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Executor;
        
        /// <summary>
        /// Notary Public
        /// </summary>
        [FhirElement("notary", InSummary=true, Order=210)]
        [References("Practitioner","RelatedPerson","Organization","Patient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Notary
        {
            get { if(_Notary==null) _Notary = new List<Hl7.Fhir.Model.ResourceReference>(); return _Notary; }
            set { _Notary = value; OnPropertyChanged("Notary"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Notary;
        
        /// <summary>
        /// Signer
        /// </summary>
        [FhirElement("signer", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.ContractSignerComponent> Signer
        {
            get { if(_Signer==null) _Signer = new List<Hl7.Fhir.Model.Contract.ContractSignerComponent>(); return _Signer; }
            set { _Signer = value; OnPropertyChanged("Signer"); }
        }
        private List<Hl7.Fhir.Model.Contract.ContractSignerComponent> _Signer;
        
        /// <summary>
        /// Contract provisions
        /// </summary>
        [FhirElement("term", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.ContractTermComponent> Term
        {
            get { if(_Term==null) _Term = new List<Hl7.Fhir.Model.Contract.ContractTermComponent>(); return _Term; }
            set { _Term = value; OnPropertyChanged("Term"); }
        }
        private List<Hl7.Fhir.Model.Contract.ContractTermComponent> _Term;
        
        /// <summary>
        /// Human readable contract text
        /// </summary>
        [FhirElement("friendly", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Friendly
        {
            get { return _Friendly; }
            set { _Friendly = value; OnPropertyChanged("Friendly"); }
        }
        private Hl7.Fhir.Model.Attachment _Friendly;
        
        /// <summary>
        /// Legal contract text
        /// </summary>
        [FhirElement("legal", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Legal
        {
            get { return _Legal; }
            set { _Legal = value; OnPropertyChanged("Legal"); }
        }
        private Hl7.Fhir.Model.Attachment _Legal;
        
        /// <summary>
        /// Computable contract text
        /// </summary>
        [FhirElement("rule", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Rule
        {
            get { return _Rule; }
            set { _Rule = value; OnPropertyChanged("Rule"); }
        }
        private Hl7.Fhir.Model.Attachment _Rule;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Contract;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Subtype != null) dest.Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(Subtype.DeepCopy());
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.Money)UnitPrice.DeepCopy();
                if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                if(Net != null) dest.Net = (Hl7.Fhir.Model.Money)Net.DeepCopy();
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
                if(Grantor != null) dest.Grantor = new List<Hl7.Fhir.Model.ResourceReference>(Grantor.DeepCopy());
                if(Grantee != null) dest.Grantee = new List<Hl7.Fhir.Model.ResourceReference>(Grantee.DeepCopy());
                if(Witness != null) dest.Witness = new List<Hl7.Fhir.Model.ResourceReference>(Witness.DeepCopy());
                if(Executor != null) dest.Executor = new List<Hl7.Fhir.Model.ResourceReference>(Executor.DeepCopy());
                if(Notary != null) dest.Notary = new List<Hl7.Fhir.Model.ResourceReference>(Notary.DeepCopy());
                if(Signer != null) dest.Signer = new List<Hl7.Fhir.Model.Contract.ContractSignerComponent>(Signer.DeepCopy());
                if(Term != null) dest.Term = new List<Hl7.Fhir.Model.Contract.ContractTermComponent>(Term.DeepCopy());
                if(Friendly != null) dest.Friendly = (Hl7.Fhir.Model.Attachment)Friendly.DeepCopy();
                if(Legal != null) dest.Legal = (Hl7.Fhir.Model.Attachment)Legal.DeepCopy();
                if(Rule != null) dest.Rule = (Hl7.Fhir.Model.Attachment)Rule.DeepCopy();
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
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
            if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
            if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
            if( !DeepComparable.Matches(Net, otherT.Net)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Grantor, otherT.Grantor)) return false;
            if( !DeepComparable.Matches(Grantee, otherT.Grantee)) return false;
            if( !DeepComparable.Matches(Witness, otherT.Witness)) return false;
            if( !DeepComparable.Matches(Executor, otherT.Executor)) return false;
            if( !DeepComparable.Matches(Notary, otherT.Notary)) return false;
            if( !DeepComparable.Matches(Signer, otherT.Signer)) return false;
            if( !DeepComparable.Matches(Term, otherT.Term)) return false;
            if( !DeepComparable.Matches(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.Matches(Legal, otherT.Legal)) return false;
            if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Contract;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
            if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
            if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
            if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Grantor, otherT.Grantor)) return false;
            if( !DeepComparable.IsExactly(Grantee, otherT.Grantee)) return false;
            if( !DeepComparable.IsExactly(Witness, otherT.Witness)) return false;
            if( !DeepComparable.IsExactly(Executor, otherT.Executor)) return false;
            if( !DeepComparable.IsExactly(Notary, otherT.Notary)) return false;
            if( !DeepComparable.IsExactly(Signer, otherT.Signer)) return false;
            if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
            if( !DeepComparable.IsExactly(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.IsExactly(Legal, otherT.Legal)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            
            return true;
        }
        
    }
    
}
