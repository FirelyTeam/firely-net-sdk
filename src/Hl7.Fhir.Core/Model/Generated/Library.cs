using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
    /// Represents a library of quality improvement components
    /// </summary>
    [FhirType("Library", IsResource=true)]
    [DataContract]
    public partial class Library : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Library; } }
        [NotMapped]
        public override string TypeName { get { return "Library"; } }
        
        /// <summary>
        /// Logical identifier
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
        /// The version of the module, if any
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// The version of the module, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// The metadata information for the library
        /// </summary>
        [FhirElement("moduleMetadata", Order=110)]
        [References("ModuleMetadata")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ModuleMetadata;
        
        /// <summary>
        /// The header information for the library
        /// </summary>
        [FhirElement("moduleDefinition", Order=120)]
        [References("ModuleDefinition")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ModuleDefinition
        {
            get { return _ModuleDefinition; }
            set { _ModuleDefinition = value; OnPropertyChanged("ModuleDefinition"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ModuleDefinition;
        
        /// <summary>
        /// The content of the library
        /// </summary>
        [FhirElement("document", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Document
        {
            get { return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Document;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Library;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(ModuleMetadata != null) dest.ModuleMetadata = (Hl7.Fhir.Model.ResourceReference)ModuleMetadata.DeepCopy();
                if(ModuleDefinition != null) dest.ModuleDefinition = (Hl7.Fhir.Model.ResourceReference)ModuleDefinition.DeepCopy();
                if(Document != null) dest.Document = (Hl7.Fhir.Model.Attachment)Document.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Library());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Library;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(ModuleDefinition, otherT.ModuleDefinition)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Library;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(ModuleDefinition, otherT.ModuleDefinition)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            
            return true;
        }
        
    }
    
}
