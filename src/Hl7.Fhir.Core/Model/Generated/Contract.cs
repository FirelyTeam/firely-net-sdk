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
// Generated on Mon, Feb 16, 2015 14:50+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    [FhirType("Contract", IsResource=true)]
    [DataContract]
    public partial class Contract : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Contract; } }
        [NotMapped]
        public override string TypeName { get { return "Contract"; } }
        
        [FhirType("ContractSignerComponent")]
        [DataContract]
        public partial class ContractSignerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContractSignerComponent"; } }
            
            /// <summary>
            /// Signer Type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.Coding>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Type;
            
            /// <summary>
            /// Documentation Signature
            /// </summary>
            [FhirElement("signature", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SignatureElement
            {
                get { return _SignatureElement; }
                set { _SignatureElement = value; OnPropertyChanged("SignatureElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SignatureElement;
            
            /// <summary>
            /// Documentation Signature
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Signature
            {
                get { return SignatureElement != null ? SignatureElement.Value : null; }
                set
                {
                    if(value == null)
                      SignatureElement = null; 
                    else
                      SignatureElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Signature");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContractSignerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Coding>(Type.DeepCopy());
                    if(SignatureElement != null) dest.SignatureElement = (Hl7.Fhir.Model.FhirString)SignatureElement.DeepCopy();
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
                if( !DeepComparable.Matches(SignatureElement, otherT.SignatureElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContractSignerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(SignatureElement, otherT.SignatureElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ContractTermComponent")]
        [DataContract]
        public partial class ContractTermComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContractTermComponent"; } }
            
            /// <summary>
            /// Term identifier
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
            /// Term type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
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
            [FhirElement("subtype", InSummary=true, Order=60)]
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
            [FhirElement("subject", InSummary=true, Order=70)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject
            {
                get { return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Subject;
            
            /// <summary>
            /// Human readable Term text
            /// </summary>
            [FhirElement("text", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Human readable Term text
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
            
            /// <summary>
            /// When issued
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
            /// When issued
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
            /// When effective
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
            [FhirElement("quantity", InSummary=true, Order=110)]
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
            [FhirElement("unitPrice", InSummary=true, Order=120)]
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
            [FhirElement("factor", InSummary=true, Order=130)]
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
            [FhirElement("points", InSummary=true, Order=140)]
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
            [FhirElement("net", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.Money _Net;
            
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
                    if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                    if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.Money)Net.DeepCopy();
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
                if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
                if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                
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
                if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
                if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Contract identifier
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
        /// Subject
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=100)]
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
        /// Authority
        /// </summary>
        [FhirElement("authority", Order=110)]
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
        /// Domain
        /// </summary>
        [FhirElement("domain", Order=120)]
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
        /// Type of contract
        /// </summary>
        [FhirElement("type", InSummary=true, Order=130)]
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
        [FhirElement("subtype", InSummary=true, Order=140)]
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
        [FhirElement("issued", InSummary=true, Order=150)]
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
        [FhirElement("applies", InSummary=true, Order=160)]
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
        [FhirElement("quantity", Order=170)]
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
        [FhirElement("unitPrice", Order=180)]
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
        [FhirElement("factor", Order=190)]
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
        [FhirElement("points", Order=200)]
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
        [FhirElement("net", Order=210)]
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
        [FhirElement("author", InSummary=true, Order=220)]
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
        [FhirElement("grantor", InSummary=true, Order=230)]
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
        [FhirElement("grantee", InSummary=true, Order=240)]
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
        [FhirElement("witness", Order=250)]
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
        [FhirElement("executor", InSummary=true, Order=260)]
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
        [FhirElement("notary", InSummary=true, Order=270)]
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
        [FhirElement("signer", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.ContractSignerComponent> Signer
        {
            get { if(_Signer==null) _Signer = new List<Hl7.Fhir.Model.Contract.ContractSignerComponent>(); return _Signer; }
            set { _Signer = value; OnPropertyChanged("Signer"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.ContractSignerComponent> _Signer;
        
        /// <summary>
        /// The terms of the Contract
        /// </summary>
        [FhirElement("term", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.ContractTermComponent> Term
        {
            get { if(_Term==null) _Term = new List<Hl7.Fhir.Model.Contract.ContractTermComponent>(); return _Term; }
            set { _Term = value; OnPropertyChanged("Term"); }
        }
        
        private List<Hl7.Fhir.Model.Contract.ContractTermComponent> _Term;
        
        /// <summary>
        /// Binding Contract
        /// </summary>
        [FhirElement("binding", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Binding
        {
            get { return _Binding; }
            set { _Binding = value; OnPropertyChanged("Binding"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Binding;
        
        /// <summary>
        /// Binding Contract effective time
        /// </summary>
        [FhirElement("bindingDateTime", Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime BindingDateTimeElement
        {
            get { return _BindingDateTimeElement; }
            set { _BindingDateTimeElement = value; OnPropertyChanged("BindingDateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _BindingDateTimeElement;
        
        /// <summary>
        /// Binding Contract effective time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string BindingDateTime
        {
            get { return BindingDateTimeElement != null ? BindingDateTimeElement.Value : null; }
            set
            {
                if(value == null)
                  BindingDateTimeElement = null; 
                else
                  BindingDateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("BindingDateTime");
            }
        }
        
        /// <summary>
        /// Human readable contract text
        /// </summary>
        [FhirElement("friendly", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Friendly
        {
            get { if(_Friendly==null) _Friendly = new List<Hl7.Fhir.Model.Attachment>(); return _Friendly; }
            set { _Friendly = value; OnPropertyChanged("Friendly"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Friendly;
        
        /// <summary>
        /// Human readable contract text effective time
        /// </summary>
        [FhirElement("friendlyDateTime", Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime FriendlyDateTimeElement
        {
            get { return _FriendlyDateTimeElement; }
            set { _FriendlyDateTimeElement = value; OnPropertyChanged("FriendlyDateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _FriendlyDateTimeElement;
        
        /// <summary>
        /// Human readable contract text effective time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string FriendlyDateTime
        {
            get { return FriendlyDateTimeElement != null ? FriendlyDateTimeElement.Value : null; }
            set
            {
                if(value == null)
                  FriendlyDateTimeElement = null; 
                else
                  FriendlyDateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("FriendlyDateTime");
            }
        }
        
        /// <summary>
        /// Legal contract text
        /// </summary>
        [FhirElement("legal", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Legal
        {
            get { if(_Legal==null) _Legal = new List<Hl7.Fhir.Model.Attachment>(); return _Legal; }
            set { _Legal = value; OnPropertyChanged("Legal"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Legal;
        
        /// <summary>
        /// Legal contract text date time
        /// </summary>
        [FhirElement("legalDateTime", Order=350)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LegalDateTimeElement
        {
            get { return _LegalDateTimeElement; }
            set { _LegalDateTimeElement = value; OnPropertyChanged("LegalDateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LegalDateTimeElement;
        
        /// <summary>
        /// Legal contract text date time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LegalDateTime
        {
            get { return LegalDateTimeElement != null ? LegalDateTimeElement.Value : null; }
            set
            {
                if(value == null)
                  LegalDateTimeElement = null; 
                else
                  LegalDateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LegalDateTime");
            }
        }
        
        /// <summary>
        /// Computable contract text
        /// </summary>
        [FhirElement("rule", Order=360)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Rule
        {
            get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.Attachment>(); return _Rule; }
            set { _Rule = value; OnPropertyChanged("Rule"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Rule;
        
        /// <summary>
        /// Computable contract text effect time
        /// </summary>
        [FhirElement("ruleDateTime", Order=370)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RuleDateTimeElement
        {
            get { return _RuleDateTimeElement; }
            set { _RuleDateTimeElement = value; OnPropertyChanged("RuleDateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RuleDateTimeElement;
        
        /// <summary>
        /// Computable contract text effect time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RuleDateTime
        {
            get { return RuleDateTimeElement != null ? RuleDateTimeElement.Value : null; }
            set
            {
                if(value == null)
                  RuleDateTimeElement = null; 
                else
                  RuleDateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RuleDateTime");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Contract;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(Authority != null) dest.Authority = new List<Hl7.Fhir.Model.ResourceReference>(Authority.DeepCopy());
                if(Domain != null) dest.Domain = new List<Hl7.Fhir.Model.ResourceReference>(Domain.DeepCopy());
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
                if(Binding != null) dest.Binding = (Hl7.Fhir.Model.Attachment)Binding.DeepCopy();
                if(BindingDateTimeElement != null) dest.BindingDateTimeElement = (Hl7.Fhir.Model.FhirDateTime)BindingDateTimeElement.DeepCopy();
                if(Friendly != null) dest.Friendly = new List<Hl7.Fhir.Model.Attachment>(Friendly.DeepCopy());
                if(FriendlyDateTimeElement != null) dest.FriendlyDateTimeElement = (Hl7.Fhir.Model.FhirDateTime)FriendlyDateTimeElement.DeepCopy();
                if(Legal != null) dest.Legal = new List<Hl7.Fhir.Model.Attachment>(Legal.DeepCopy());
                if(LegalDateTimeElement != null) dest.LegalDateTimeElement = (Hl7.Fhir.Model.FhirDateTime)LegalDateTimeElement.DeepCopy();
                if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.Attachment>(Rule.DeepCopy());
                if(RuleDateTimeElement != null) dest.RuleDateTimeElement = (Hl7.Fhir.Model.FhirDateTime)RuleDateTimeElement.DeepCopy();
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
            if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
            if( !DeepComparable.Matches(Domain, otherT.Domain)) return false;
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
            if( !DeepComparable.Matches(Binding, otherT.Binding)) return false;
            if( !DeepComparable.Matches(BindingDateTimeElement, otherT.BindingDateTimeElement)) return false;
            if( !DeepComparable.Matches(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.Matches(FriendlyDateTimeElement, otherT.FriendlyDateTimeElement)) return false;
            if( !DeepComparable.Matches(Legal, otherT.Legal)) return false;
            if( !DeepComparable.Matches(LegalDateTimeElement, otherT.LegalDateTimeElement)) return false;
            if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            if( !DeepComparable.Matches(RuleDateTimeElement, otherT.RuleDateTimeElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Contract;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
            if( !DeepComparable.IsExactly(Domain, otherT.Domain)) return false;
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
            if( !DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
            if( !DeepComparable.IsExactly(BindingDateTimeElement, otherT.BindingDateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.IsExactly(FriendlyDateTimeElement, otherT.FriendlyDateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Legal, otherT.Legal)) return false;
            if( !DeepComparable.IsExactly(LegalDateTimeElement, otherT.LegalDateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            if( !DeepComparable.IsExactly(RuleDateTimeElement, otherT.RuleDateTimeElement)) return false;
            
            return true;
        }
        
    }
    
}
