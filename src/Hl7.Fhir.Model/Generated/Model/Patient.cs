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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about a person or animal receiving health care services
    /// </summary>
    [FhirType("Patient", IsResource=true)]
    [DataContract]
    public partial class Patient : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The type of link between this patient resource and another patient resource.
        /// </summary>
        [FhirEnumeration("LinkType")]
        public enum LinkType
        {
            [EnumLiteral("replace")]
            Replace, // The patient resource containing this link must no longer be used. The link points forward to another patient resource that must be used in lieu of the patient resource that contains the link.
            [EnumLiteral("refer")]
            Refer, // The patient resource containing this link is in use and valid but not considered the main source of information about a patient. The link points forward to another patient resource that should be consulted to retrieve additional patient information.
            [EnumLiteral("seealso")]
            Seealso, // The patient resource containing this link is in use and valid, but points to another patient resource that is known to contain data about the same person. Data in this resource might overlap or contradict information found in the other patient resource. This link does not indicate any relative importance of the resources concerned, and both should be regarded as equally valid.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ContactComponent")]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The kind of relationship
            /// </summary>
            [FhirElement("relationship", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Relationship { get; set; }
            
            /// <summary>
            /// A name associated with the person
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.HumanName Name { get; set; }
            
            /// <summary>
            /// A contact detail for the person
            /// </summary>
            [FhirElement("telecom", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
            
            /// <summary>
            /// Address for the contact person
            /// </summary>
            [FhirElement("address", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Address Address { get; set; }
            
            /// <summary>
            /// Gender for administrative purposes
            /// </summary>
            [FhirElement("gender", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Gender { get; set; }
            
            /// <summary>
            /// Organization that is associated with the contact
            /// </summary>
            [FhirElement("organization", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("AnimalComponent")]
        [DataContract]
        public partial class AnimalComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// E.g. Dog, Cow
            /// </summary>
            [FhirElement("species", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Species { get; set; }
            
            /// <summary>
            /// E.g. Poodle, Angus
            /// </summary>
            [FhirElement("breed", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Breed { get; set; }
            
            /// <summary>
            /// E.g. Neutered, Intact
            /// </summary>
            [FhirElement("genderStatus", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept GenderStatus { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("PatientLinkComponent")]
        [DataContract]
        public partial class PatientLinkComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The other patient resource that the link refers to
            /// </summary>
            [FhirElement("other", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Other { get; set; }
            
            /// <summary>
            /// replace | refer | seealso - type of link
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Patient.LinkType> TypeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Patient.LinkType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Patient.LinkType>(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// An identifier for the person as this patient
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// A name associated with the patient
        /// </summary>
        [FhirElement("name", Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.HumanName> Name { get; set; }
        
        /// <summary>
        /// A contact detail for the individual
        /// </summary>
        [FhirElement("telecom", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Gender for administrative purposes
        /// </summary>
        [FhirElement("gender", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Gender { get; set; }
        
        /// <summary>
        /// The date and time of birth for the individual
        /// </summary>
        [FhirElement("birthDate", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime BirthDateElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string BirthDate
        {
            get { return BirthDateElement != null ? BirthDateElement.Value : null; }
            set
            {
                if(value == null)
                  BirthDateElement = null; 
                else
                  BirthDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
            }
        }
        
        /// <summary>
        /// Indicates if the individual is deceased or not
        /// </summary>
        [FhirElement("deceased", Order=120, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDateTime))]
        [DataMember]
        public Hl7.Fhir.Model.Element Deceased { get; set; }
        
        /// <summary>
        /// Addresses for the individual
        /// </summary>
        [FhirElement("address", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Address> Address { get; set; }
        
        /// <summary>
        /// Marital (civil) status of a person
        /// </summary>
        [FhirElement("maritalStatus", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept MaritalStatus { get; set; }
        
        /// <summary>
        /// Whether patient is part of a multiple birth
        /// </summary>
        [FhirElement("multipleBirth", Order=150, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer))]
        [DataMember]
        public Hl7.Fhir.Model.Element MultipleBirth { get; set; }
        
        /// <summary>
        /// Image of the person
        /// </summary>
        [FhirElement("photo", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Photo { get; set; }
        
        /// <summary>
        /// A contact party (e.g. guardian, partner, friend) for the patient
        /// </summary>
        [FhirElement("contact", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Patient.ContactComponent> Contact { get; set; }
        
        /// <summary>
        /// If this patient is an animal (non-human)
        /// </summary>
        [FhirElement("animal", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Patient.AnimalComponent Animal { get; set; }
        
        /// <summary>
        /// Languages which may be used to communicate with the patient about his or her health
        /// </summary>
        [FhirElement("communication", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Communication { get; set; }
        
        /// <summary>
        /// Patient's nominated care provider
        /// </summary>
        [FhirElement("careProvider", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CareProvider { get; set; }
        
        /// <summary>
        /// Organization that is the custodian of the patient record
        /// </summary>
        [FhirElement("managingOrganization", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }
        
        /// <summary>
        /// Link to another patient resource that concerns the same actual person
        /// </summary>
        [FhirElement("link", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Patient.PatientLinkComponent> Link { get; set; }
        
        /// <summary>
        /// Whether this patient's record is in active use
        /// </summary>
        [FhirElement("active", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Active
        {
            get { return ActiveElement != null ? ActiveElement.Value : null; }
            set
            {
                if(value == null)
                  ActiveElement = null; 
                else
                  ActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
    }
    
}
