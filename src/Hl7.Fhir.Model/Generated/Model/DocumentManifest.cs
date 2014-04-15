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
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A manifest that defines a set of documents
    /// </summary>
    [FhirType("DocumentManifest", IsResource=true)]
    [DataContract]
    public partial class DocumentManifest : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Unique Identifier for the set of documents
        /// </summary>
        [FhirElement("masterIdentifier", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier MasterIdentifier
        {
            get { return _MasterIdentifier; }
            set { _MasterIdentifier = value; OnPropertyChanged("MasterIdentifier"); }
        }
        private Hl7.Fhir.Model.Identifier _MasterIdentifier;
        
        /// <summary>
        /// Other identifiers for the manifest
        /// </summary>
        [FhirElement("identifier", Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// The subject of the set of documents
        /// </summary>
        [FhirElement("subject", Order=90)]
        [References("Patient","Practitioner","Group","Device")]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Subject;
        
        /// <summary>
        /// Intended to get notified about this set of documents
        /// </summary>
        [FhirElement("recipient", Order=100)]
        [References("Patient","Practitioner","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Recipient
        {
            get { return _Recipient; }
            set { _Recipient = value; OnPropertyChanged("Recipient"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Recipient;
        
        /// <summary>
        /// What kind of document set this is
        /// </summary>
        [FhirElement("type", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Who and/or what authored the document
        /// </summary>
        [FhirElement("author", Order=120)]
        [References("Practitioner","Device","Patient","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Author;
        
        /// <summary>
        /// When this document manifest created
        /// </summary>
        [FhirElement("created", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if(value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// The source system/application/software
        /// </summary>
        [FhirElement("source", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri SourceElement
        {
            get { return _SourceElement; }
            set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
        }
        private Hl7.Fhir.Model.FhirUri _SourceElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public System.Uri Source
        {
            get { return SourceElement != null ? SourceElement.Value : null; }
            set
            {
                if(value == null)
                  SourceElement = null; 
                else
                  SourceElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Source");
            }
        }
        
        /// <summary>
        /// current | superceded | entered in error
        /// </summary>
        [FhirElement("status", Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Hl7.Fhir.Model.Code _StatusElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If this document manifest replaces another
        /// </summary>
        [FhirElement("supercedes", Order=160)]
        [References("DocumentManifest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Supercedes
        {
            get { return _Supercedes; }
            set { _Supercedes = value; OnPropertyChanged("Supercedes"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Supercedes;
        
        /// <summary>
        /// Human-readable description (title)
        /// </summary>
        [FhirElement("description", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Sensitivity of set of documents
        /// </summary>
        [FhirElement("confidentiality", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Confidentiality
        {
            get { return _Confidentiality; }
            set { _Confidentiality = value; OnPropertyChanged("Confidentiality"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Confidentiality;
        
        /// <summary>
        /// Contents of this set of documents
        /// </summary>
        [FhirElement("content", Order=190)]
        [References("DocumentReference","Binary","Media")]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged("Content"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Content;
        
    }
    
}
