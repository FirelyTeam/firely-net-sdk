using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Resource for capturing binary data
    /// </summary>
    [FhirType("Binary", IsResource=true)]
    [DataContract]
    public partial class Binary : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Binary contents
        /// </summary>
        [FhirElement("content", XmlSerialization=XmlSerializationHint.TextNode, Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public byte[] Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged("Content"); }
        }
        private byte[] _Content;
        
        /// <summary>
        /// Media type of contents
        /// </summary>
        [FhirElement("contentType", XmlSerialization=XmlSerializationHint.Attribute, Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; OnPropertyChanged("ContentType"); }
        }
        private string _ContentType;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Binary;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Content != null) dest.Content = Content;
                if(ContentType != null) dest.ContentType = ContentType;
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Binary());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Binary;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( Content != otherT.Content ) return false;
            if( ContentType != otherT.ContentType ) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Binary;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( Content != otherT.Content ) return false;
            if( ContentType != otherT.ContentType ) return false;
            
            return true;
        }
        
    }
    
}
