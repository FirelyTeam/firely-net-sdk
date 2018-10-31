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
    /// A measured or measurable amount
    /// </summary>
    [FhirType("Quantity")]
    [DataContract]
    public partial class Quantity : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Quantity"; } }
        
        /// <summary>
        /// How the Quantity should be understood and represented.
        /// (url: http://hl7.org/fhir/ValueSet/quantity-comparator)
        /// </summary>
        [FhirEnumeration("QuantityComparator")]
        public enum QuantityComparator
        {
            /// <summary>
            /// The actual value is less than the given value.
            /// (system: http://hl7.org/fhir/quantity-comparator)
            /// </summary>
            [EnumLiteral("<", "http://hl7.org/fhir/quantity-comparator"), Description("Less than")]
            LessThan,
            /// <summary>
            /// The actual value is less than or equal to the given value.
            /// (system: http://hl7.org/fhir/quantity-comparator)
            /// </summary>
            [EnumLiteral("<=", "http://hl7.org/fhir/quantity-comparator"), Description("Less or Equal to")]
            LessOrEqual,
            /// <summary>
            /// The actual value is greater than or equal to the given value.
            /// (system: http://hl7.org/fhir/quantity-comparator)
            /// </summary>
            [EnumLiteral(">=", "http://hl7.org/fhir/quantity-comparator"), Description("Greater or Equal to")]
            GreaterOrEqual,
            /// <summary>
            /// The actual value is greater than the given value.
            /// (system: http://hl7.org/fhir/quantity-comparator)
            /// </summary>
            [EnumLiteral(">", "http://hl7.org/fhir/quantity-comparator"), Description("Greater than")]
            GreaterThan,
        }

        /// <summary>
        /// Numerical value (with implicit precision)
        /// </summary>
        [FhirElement("value", InSummary=true, Order=30)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal ValueElement
        {
            get { return _ValueElement; }
            set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _ValueElement;
        
        /// <summary>
        /// Numerical value (with implicit precision)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? Value
        {
            get { return ValueElement != null ? ValueElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ValueElement = null; 
                else
                  ValueElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Value");
            }
        }
        
        /// <summary>
        /// &lt; | &lt;= | &gt;= | &gt; - how to understand the value
        /// </summary>
        [FhirElement("comparator", InSummary=true, Order=40)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Quantity.QuantityComparator> ComparatorElement
        {
            get { return _ComparatorElement; }
            set { _ComparatorElement = value; OnPropertyChanged("ComparatorElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Quantity.QuantityComparator> _ComparatorElement;
        
        /// <summary>
        /// &lt; | &lt;= | &gt;= | &gt; - how to understand the value
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Quantity.QuantityComparator? Comparator
        {
            get { return ComparatorElement != null ? ComparatorElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ComparatorElement = null; 
                else
                  ComparatorElement = new Code<Hl7.Fhir.Model.Quantity.QuantityComparator>(value);
                OnPropertyChanged("Comparator");
            }
        }
        
        /// <summary>
        /// Unit representation
        /// </summary>
        [FhirElement("unit", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UnitElement
        {
            get { return _UnitElement; }
            set { _UnitElement = value; OnPropertyChanged("UnitElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _UnitElement;
        
        /// <summary>
        /// Unit representation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Unit
        {
            get { return UnitElement != null ? UnitElement.Value : null; }
            set
            {
                if (value == null)
                  UnitElement = null; 
                else
                  UnitElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Unit");
            }
        }
        
        /// <summary>
        /// System that defines coded unit form
        /// </summary>
        [FhirElement("system", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri SystemElement
        {
            get { return _SystemElement; }
            set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _SystemElement;
        
        /// <summary>
        /// System that defines coded unit form
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
        /// Coded form of the unit
        /// </summary>
        [FhirElement("code", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Code CodeElement
        {
            get { return _CodeElement; }
            set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _CodeElement;
        
        /// <summary>
        /// Coded form of the unit
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Code
        {
            get { return CodeElement != null ? CodeElement.Value : null; }
            set
            {
                if (value == null)
                  CodeElement = null; 
                else
                  CodeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Code");
            }
        }
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Quantity;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                if(ComparatorElement != null) dest.ComparatorElement = (Code<Hl7.Fhir.Model.Quantity.QuantityComparator>)ComparatorElement.DeepCopy();
                if(UnitElement != null) dest.UnitElement = (Hl7.Fhir.Model.FhirString)UnitElement.DeepCopy();
                if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Quantity());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Quantity;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.Matches(ComparatorElement, otherT.ComparatorElement)) return false;
            if( !DeepComparable.Matches(UnitElement, otherT.UnitElement)) return false;
            if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Quantity;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.IsExactly(ComparatorElement, otherT.ComparatorElement)) return false;
            if( !DeepComparable.IsExactly(UnitElement, otherT.UnitElement)) return false;
            if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (ValueElement != null) yield return ValueElement;
                if (ComparatorElement != null) yield return ComparatorElement;
                if (UnitElement != null) yield return UnitElement;
                if (SystemElement != null) yield return SystemElement;
                if (CodeElement != null) yield return CodeElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                if (ComparatorElement != null) yield return new ElementValue("comparator", ComparatorElement);
                if (UnitElement != null) yield return new ElementValue("unit", UnitElement);
                if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                if (CodeElement != null) yield return new ElementValue("code", CodeElement);
 
            } 
        } 
    
    
    }
    
}
