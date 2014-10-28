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
// Generated on Tue, Oct 28, 2014 16:11+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    [FhirType("Contract", IsResource=true)]
    [DataContract]
    public partial class Contract : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [FhirType("ContractTermComponent")]
        [DataContract]
        public partial class ContractTermComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
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
            public Hl7.Fhir.Model.Reference Subject
            {
                get { return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            private Hl7.Fhir.Model.Reference _Subject;
            
            /// <summary>
            /// Human readable term text
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
                    if(Subject != null) dest.Subject = (Hl7.Fhir.Model.Reference)Subject.DeepCopy();
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
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=60)]
        [References("Patient","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.Reference _Subject;
        
        /// <summary>
        /// Type of contract (Privacy-Security, Agreement, Insurance)
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
        /// More specific type of contract (Privacy, Disclosure-Authorization, etc)
        /// </summary>
        [FhirElement("subtype", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Subtype
        {
            get { return _Subtype; }
            set { _Subtype = value; OnPropertyChanged("Subtype"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Subtype;
        
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
        /// Relevant time/time-period when applicable
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
        /// Contract author or responsible party
        /// </summary>
        [FhirElement("author", InSummary=true, Order=110)]
        [References("Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Reference> Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        private List<Hl7.Fhir.Model.Reference> _Author;
        
        /// <summary>
        /// First Party or delegator
        /// </summary>
        [FhirElement("grantor", InSummary=true, Order=120)]
        [References("Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Reference> Grantor
        {
            get { return _Grantor; }
            set { _Grantor = value; OnPropertyChanged("Grantor"); }
        }
        private List<Hl7.Fhir.Model.Reference> _Grantor;
        
        /// <summary>
        /// Second Party or delegatee
        /// </summary>
        [FhirElement("grantee", InSummary=true, Order=130)]
        [References("Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Reference> Grantee
        {
            get { return _Grantee; }
            set { _Grantee = value; OnPropertyChanged("Grantee"); }
        }
        private List<Hl7.Fhir.Model.Reference> _Grantee;
        
        /// <summary>
        /// Witness to the contract
        /// </summary>
        [FhirElement("witness", Order=140)]
        [References("Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Reference> Witness
        {
            get { return _Witness; }
            set { _Witness = value; OnPropertyChanged("Witness"); }
        }
        private List<Hl7.Fhir.Model.Reference> _Witness;
        
        /// <summary>
        /// Contract identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Contract provisions
        /// </summary>
        [FhirElement("term", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contract.ContractTermComponent> Term
        {
            get { return _Term; }
            set { _Term = value; OnPropertyChanged("Term"); }
        }
        private List<Hl7.Fhir.Model.Contract.ContractTermComponent> _Term;
        
        /// <summary>
        /// Human readable contract text
        /// </summary>
        [FhirElement("friendly", Order=170)]
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
        [FhirElement("legal", Order=180)]
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
        [FhirElement("rule", Order=190)]
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
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.Reference)Subject.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Subtype != null) dest.Subtype = (Hl7.Fhir.Model.CodeableConcept)Subtype.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.Reference>(Author.DeepCopy());
                if(Grantor != null) dest.Grantor = new List<Hl7.Fhir.Model.Reference>(Grantor.DeepCopy());
                if(Grantee != null) dest.Grantee = new List<Hl7.Fhir.Model.Reference>(Grantee.DeepCopy());
                if(Witness != null) dest.Witness = new List<Hl7.Fhir.Model.Reference>(Witness.DeepCopy());
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
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
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Grantor, otherT.Grantor)) return false;
            if( !DeepComparable.Matches(Grantee, otherT.Grantee)) return false;
            if( !DeepComparable.Matches(Witness, otherT.Witness)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
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
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Grantor, otherT.Grantor)) return false;
            if( !DeepComparable.IsExactly(Grantee, otherT.Grantee)) return false;
            if( !DeepComparable.IsExactly(Witness, otherT.Witness)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
            if( !DeepComparable.IsExactly(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.IsExactly(Legal, otherT.Legal)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            
            return true;
        }
        
    }
    
}
