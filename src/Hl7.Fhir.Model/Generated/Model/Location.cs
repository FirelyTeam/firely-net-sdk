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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Details and position information for a physical place
    /// </summary>
    [FhirType("Location", IsResource=true)]
    [DataContract]
    public partial class Location : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Indicates whether the location is still in use
        /// </summary>
        [FhirEnumeration("LocationStatus")]
        public enum LocationStatus
        {
            [EnumLiteral("active")]
            Active, // The location is operational.
            [EnumLiteral("suspended")]
            Suspended, // The location is temporarily closed.
            [EnumLiteral("inactive")]
            Inactive, // The location is no longer used.
        }
        
        /// <summary>
        /// Indicates whether a resource instance represents a specific location or a class of locations
        /// </summary>
        [FhirEnumeration("LocationMode")]
        public enum LocationMode
        {
            [EnumLiteral("instance")]
            Instance, // The Location resource represents a specific instance of a Location.
            [EnumLiteral("kind")]
            Kind, // The Location represents a class of Locations.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("LocationPositionComponent")]
        [DataContract]
        public partial class LocationPositionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Longitude as expressed in KML
            /// </summary>
            [FhirElement("longitude", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LongitudeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Longitude
            {
                get { return LongitudeElement != null ? LongitudeElement.Value : null; }
                set
                {
                    if(value == null)
                      LongitudeElement = null; 
                    else
                      LongitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
                }
            }
            
            /// <summary>
            /// Latitude as expressed in KML
            /// </summary>
            [FhirElement("latitude", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LatitudeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Latitude
            {
                get { return LatitudeElement != null ? LatitudeElement.Value : null; }
                set
                {
                    if(value == null)
                      LatitudeElement = null; 
                    else
                      LatitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
                }
            }
            
            /// <summary>
            /// Altitude as expressed in KML
            /// </summary>
            [FhirElement("altitude", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal AltitudeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Altitude
            {
                get { return AltitudeElement != null ? AltitudeElement.Value : null; }
                set
                {
                    if(value == null)
                      AltitudeElement = null; 
                    else
                      AltitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// Unique code or number identifying the location to its users
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier { get; set; }
        
        /// <summary>
        /// Name of the location as used by humans
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
        /// Description of the Location, which helps in finding or referencing the place
        /// </summary>
        [FhirElement("description", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Indicates the type of function performed at the location
        /// </summary>
        [FhirElement("type", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Contact details of the location
        /// </summary>
        [FhirElement("telecom", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Physical location
        /// </summary>
        [FhirElement("address", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Address Address { get; set; }
        
        /// <summary>
        /// Physical form of the location
        /// </summary>
        [FhirElement("physicalType", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PhysicalType { get; set; }
        
        /// <summary>
        /// The absolute geographic location
        /// </summary>
        [FhirElement("position", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Location.LocationPositionComponent Position { get; set; }
        
        /// <summary>
        /// The organization that is responsible for the provisioning and upkeep of the location
        /// </summary>
        [FhirElement("managingOrganization", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }
        
        /// <summary>
        /// active | suspended | inactive
        /// </summary>
        [FhirElement("status", Order=160)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Location.LocationStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Location.LocationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Location.LocationStatus>(value);
            }
        }
        
        /// <summary>
        /// Another Location which this Location is physically part of
        /// </summary>
        [FhirElement("partOf", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf { get; set; }
        
        /// <summary>
        /// instance | kind
        /// </summary>
        [FhirElement("mode", Order=180)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Location.LocationMode> ModeElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Location.LocationMode? Mode
        {
            get { return ModeElement != null ? ModeElement.Value : null; }
            set
            {
                if(value == null)
                  ModeElement = null; 
                else
                  ModeElement = new Code<Hl7.Fhir.Model.Location.LocationMode>(value);
            }
        }
        
    }
    
}
