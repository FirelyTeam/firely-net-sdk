using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An identifier intended for computation
    /// </summary>
    [FhirType("Identifier")]
    [DataContract]
    public partial class Identifier : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Identifier"; } }
        
        /// <summary>
        /// Identifies the purpose for this identifier, if known .
        /// (url: http://hl7.org/fhir/ValueSet/identifier-use)
        /// </summary>
        [FhirEnumeration("IdentifierUse")]
        public enum IdentifierUse
        {
            /// <summary>
            /// The identifier recommended for display and use in real-world interactions.
            /// (system: http://hl7.org/fhir/identifier-use)
            /// </summary>
            [EnumLiteral("usual", "http://hl7.org/fhir/identifier-use"), Description("Usual")]
            Usual,
            /// <summary>
            /// The identifier considered to be most trusted for the identification of this item.
            /// (system: http://hl7.org/fhir/identifier-use)
            /// </summary>
            [EnumLiteral("official", "http://hl7.org/fhir/identifier-use"), Description("Official")]
            Official,
            /// <summary>
            /// A temporary identifier.
            /// (system: http://hl7.org/fhir/identifier-use)
            /// </summary>
            [EnumLiteral("temp", "http://hl7.org/fhir/identifier-use"), Description("Temp")]
            Temp,
            /// <summary>
            /// An identifier that was assigned in secondary use - it serves to identify the object in a relative context, but cannot be consistently assigned to the same object again in a different context.
            /// (system: http://hl7.org/fhir/identifier-use)
            /// </summary>
            [EnumLiteral("secondary", "http://hl7.org/fhir/identifier-use"), Description("Secondary")]
            Secondary,
        }

        /// <summary>
        /// usual | official | temp | secondary (If known)
        /// </summary>
        [FhirElement("use", InSummary=true, Order=30)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Identifier.IdentifierUse> UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Identifier.IdentifierUse> _UseElement;
        
        /// <summary>
        /// usual | official | temp | secondary (If known)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Identifier.IdentifierUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  UseElement = null; 
                else
                  UseElement = new Code<Hl7.Fhir.Model.Identifier.IdentifierUse>(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// Description of identifier
        /// </summary>
        [FhirElement("type", InSummary=true, Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// The namespace for the identifier
        /// </summary>
        [FhirElement("system", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri SystemElement
        {
            get { return _SystemElement; }
            set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _SystemElement;
        
        /// <summary>
        /// The namespace for the identifier
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string System
        {
            get { return SystemElement != null ? SystemElement.Value : null; }
            set
            {
                if (value == null)
                  SystemElement = null; 
                else
                  SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("System");
            }
        }
        
        /// <summary>
        /// The value that is unique
        /// </summary>
        [FhirElement("value", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ValueElement
        {
            get { return _ValueElement; }
            set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ValueElement;
        
        /// <summary>
        /// The value that is unique
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Value
        {
            get { return ValueElement != null ? ValueElement.Value : null; }
            set
            {
                if (value == null)
                  ValueElement = null; 
                else
                  ValueElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Value");
            }
        }
        
        /// <summary>
        /// Time period when id is/was valid for use
        /// </summary>
        [FhirElement("period", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Organization that issued id (may be just text)
        /// </summary>
        [FhirElement("assigner", InSummary=true, Order=80)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Assigner
        {
            get { return _Assigner; }
            set { _Assigner = value; OnPropertyChanged("Assigner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Assigner;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Identifier;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.Identifier.IdentifierUse>)UseElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Assigner != null) dest.Assigner = (Hl7.Fhir.Model.ResourceReference)Assigner.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Identifier());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Identifier;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Assigner, otherT.Assigner)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Identifier;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Assigner, otherT.Assigner)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (UseElement != null) yield return UseElement;
                if (Type != null) yield return Type;
                if (SystemElement != null) yield return SystemElement;
                if (ValueElement != null) yield return ValueElement;
                if (Period != null) yield return Period;
                if (Assigner != null) yield return Assigner;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (UseElement != null) yield return new ElementValue("use", UseElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                if (Period != null) yield return new ElementValue("period", Period);
                if (Assigner != null) yield return new ElementValue("assigner", Assigner);
 
            } 
        } 
    
    
    }
    
}
