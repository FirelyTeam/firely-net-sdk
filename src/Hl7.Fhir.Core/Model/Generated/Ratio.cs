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
    /// A ratio of two Quantity values - a numerator and a denominator
    /// </summary>
    [FhirType("Ratio")]
    [DataContract]
    public partial class Ratio : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Ratio"; } }
        
        /// <summary>
        /// Numerator value
        /// </summary>
        [FhirElement("numerator", InSummary=true, Order=30)]
        [DataMember]
        public Quantity Numerator
        {
            get { return _Numerator; }
            set { _Numerator = value; OnPropertyChanged("Numerator"); }
        }
        
        private Quantity _Numerator;
        
        /// <summary>
        /// Denominator value
        /// </summary>
        [FhirElement("denominator", InSummary=true, Order=40)]
        [DataMember]
        public Quantity Denominator
        {
            get { return _Denominator; }
            set { _Denominator = value; OnPropertyChanged("Denominator"); }
        }
        
        private Quantity _Denominator;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Ratio;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Numerator != null) dest.Numerator = (Quantity)Numerator.DeepCopy();
                if(Denominator != null) dest.Denominator = (Quantity)Denominator.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Ratio());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Ratio;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Numerator, otherT.Numerator)) return false;
            if( !DeepComparable.Matches(Denominator, otherT.Denominator)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Ratio;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Numerator, otherT.Numerator)) return false;
            if( !DeepComparable.IsExactly(Denominator, otherT.Denominator)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Numerator != null) yield return Numerator;
                if (Denominator != null) yield return Denominator;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (Numerator != null) yield return new ElementValue("numerator", Numerator);
                if (Denominator != null) yield return new ElementValue("denominator", Denominator);
 
            } 
        } 
    
    
    }
    
}
