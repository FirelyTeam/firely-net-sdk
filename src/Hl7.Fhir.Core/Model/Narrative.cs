using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
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
    /// A human-readable formatted text, including images
    /// </summary>
    [FhirType("Narrative")]
    [DataContract]
    public partial class Narrative : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Narrative"; } }
        
        /// <summary>
        /// The status of a resource narrative
        /// (url: http://hl7.org/fhir/ValueSet/narrative-status)
        /// </summary>
        [FhirEnumeration("NarrativeStatus")]
        public enum NarrativeStatus
        {
            /// <summary>
            /// The contents of the narrative are entirely generated from the structured data in the content.
            /// (system: http://hl7.org/fhir/narrative-status)
            /// </summary>
            [EnumLiteral("generated"), Description("Generated")]
            Generated,
            /// <summary>
            /// The contents of the narrative are entirely generated from the structured data in the content and some of the content is generated from extensions
            /// (system: http://hl7.org/fhir/narrative-status)
            /// </summary>
            [EnumLiteral("extensions"), Description("Extensions")]
            Extensions,
            /// <summary>
            /// The contents of the narrative contain additional information not found in the structured data
            /// (system: http://hl7.org/fhir/narrative-status)
            /// </summary>
            [EnumLiteral("additional"), Description("Additional")]
            Additional,
            /// <summary>
            /// The contents of the narrative are some equivalent of "No human-readable text provided in this case"
            /// (system: http://hl7.org/fhir/narrative-status)
            /// </summary>
            [EnumLiteral("empty"), Description("Empty")]
            Empty,
        }

        /// <summary>
        /// generated | extensions | additional | empty
        /// </summary>
        [FhirElement("status", InSummary = true, Order = 30)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Narrative.NarrativeStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Narrative.NarrativeStatus> _StatusElement;
        
        /// <summary>
        /// generated | extensions | additional | empty
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Narrative.NarrativeStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Narrative.NarrativeStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Limited xhtml content
        /// </summary>
        [FhirElement("div", XmlSerialization=XmlRepresentation.XHtml, InSummary=true, Order=40, TypeRedirect = typeof(XHtml))]
        [Cardinality(Min=1,Max=1)]
        [NarrativeXhtmlPattern]
        [DataMember]
        public string Div
        {
            get { return _Div; }
            set { _Div = value; OnPropertyChanged("Div"); }
        }
        
        private string _Div;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Narrative;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Narrative.NarrativeStatus>)StatusElement.DeepCopy();
                if(Div != null) dest.Div = Div;
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Narrative());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Narrative;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( Div != otherT.Div ) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Narrative;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( Div != otherT.Div ) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                // Narrative elements
                if (StatusElement != null) yield return StatusElement;
                // Div property does not implement Base...
            }
        }


        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Div != null) yield return new ElementValue("div", Div);                
            }
        }
    }
    
}
