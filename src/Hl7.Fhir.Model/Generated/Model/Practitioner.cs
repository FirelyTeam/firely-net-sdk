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
// Generated on Thu, Apr 17, 2014 11:39+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A person with a  formal responsibility in the provisioning of healthcare or related services
    /// </summary>
    [FhirType("Practitioner", IsResource=true)]
    [DataContract]
    public partial class Practitioner : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("PractitionerQualificationComponent")]
        [DataContract]
        public partial class PractitionerQualificationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Coded representation of the qualification
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Period during which the qualification is valid
            /// </summary>
            [FhirElement("period", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// Organization that regulates and issues the qualification
            /// </summary>
            [FhirElement("issuer", InSummary=true, Order=60)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Issuer
            {
                get { return _Issuer; }
                set { _Issuer = value; OnPropertyChanged("Issuer"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Issuer;
            
        }
        
        
        /// <summary>
        /// A identifier for the person as this agent
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// A name associated with the person
        /// </summary>
        [FhirElement("name", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.HumanName Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        private Hl7.Fhir.Model.HumanName _Name;
        
        /// <summary>
        /// A contact detail for the practitioner
        /// </summary>
        [FhirElement("telecom", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom
        {
            get { return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        private List<Hl7.Fhir.Model.Contact> _Telecom;
        
        /// <summary>
        /// Where practitioner can be found/visited
        /// </summary>
        [FhirElement("address", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Address Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged("Address"); }
        }
        private Hl7.Fhir.Model.Address _Address;
        
        /// <summary>
        /// Gender for administrative purposes
        /// </summary>
        [FhirElement("gender", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Gender
        {
            get { return _Gender; }
            set { _Gender = value; OnPropertyChanged("Gender"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Gender;
        
        /// <summary>
        /// The date and time of birth for the practitioner
        /// </summary>
        [FhirElement("birthDate", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime BirthDateElement
        {
            get { return _BirthDateElement; }
            set { _BirthDateElement = value; OnPropertyChanged("BirthDateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _BirthDateElement;
        
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
                OnPropertyChanged("BirthDate");
            }
        }
        
        /// <summary>
        /// Image of the person
        /// </summary>
        [FhirElement("photo", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Photo
        {
            get { return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }
        private List<Hl7.Fhir.Model.Attachment> _Photo;
        
        /// <summary>
        /// The represented organization
        /// </summary>
        [FhirElement("organization", InSummary=true, Order=140)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// Roles which this practitioner may perform
        /// </summary>
        [FhirElement("role", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Role
        {
            get { return _Role; }
            set { _Role = value; OnPropertyChanged("Role"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Role;
        
        /// <summary>
        /// Specific specialty of the practitioner
        /// </summary>
        [FhirElement("specialty", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Specialty
        {
            get { return _Specialty; }
            set { _Specialty = value; OnPropertyChanged("Specialty"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
        
        /// <summary>
        /// The period during which the practitioner is authorized to perform in these role(s)
        /// </summary>
        [FhirElement("period", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// The location(s) at which this practitioner provides care
        /// </summary>
        [FhirElement("location", Order=180)]
        [References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Location;
        
        /// <summary>
        /// Qualifications obtained by training and certification
        /// </summary>
        [FhirElement("qualification", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Practitioner.PractitionerQualificationComponent> Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; OnPropertyChanged("Qualification"); }
        }
        private List<Hl7.Fhir.Model.Practitioner.PractitionerQualificationComponent> _Qualification;
        
        /// <summary>
        /// A language the practitioner is able to use in patient communication
        /// </summary>
        [FhirElement("communication", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Communication
        {
            get { return _Communication; }
            set { _Communication = value; OnPropertyChanged("Communication"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Communication;
        
    }
    
}
