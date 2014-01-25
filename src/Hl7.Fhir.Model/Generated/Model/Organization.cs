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
    /// A grouping of people or organizations with a common purpose
    /// </summary>
    [FhirType("Organization", IsResource=true)]
    [DataContract]
    public partial class Organization : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("OrganizationContactComponent")]
        [DataContract]
        public partial class OrganizationContactComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The type of contact
            /// </summary>
            [FhirElement("purpose", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Purpose { get; set; }
            
            /// <summary>
            /// A name associated with the contact
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.HumanName Name { get; set; }
            
            /// <summary>
            /// Contact details (telephone, email, etc)  for a contact
            /// </summary>
            [FhirElement("telecom", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
            
            /// <summary>
            /// Visiting or postal addresses for the contact
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
            
        }
        
        
        /// <summary>
        /// Identifies this organization  across multiple systems
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Name used for the organization
        /// </summary>
        [FhirElement("name", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Kind of organization
        /// </summary>
        [FhirElement("type", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// A contact detail for the organization
        /// </summary>
        [FhirElement("telecom", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// An address for the organization
        /// </summary>
        [FhirElement("address", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Address> Address { get; set; }
        
        /// <summary>
        /// The organization of which this organization forms a part
        /// </summary>
        [FhirElement("partOf", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf { get; set; }
        
        /// <summary>
        /// Contact for the organization for a certain purpose
        /// </summary>
        [FhirElement("contact", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Organization.OrganizationContactComponent> Contact { get; set; }
        
        /// <summary>
        /// Location(s) the organization uses to provide services
        /// </summary>
        [FhirElement("location", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Location { get; set; }
        
        /// <summary>
        /// Whether the organization's record is still in active use
        /// </summary>
        [FhirElement("active", Order=150)]
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
