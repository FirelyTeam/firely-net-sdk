using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

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
#pragma warning disable 1591 // suppress XML summary warnings

//
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Details and position information for a physical place
    /// </summary>
    [FhirType("Location", IsResource=true)]
    [DataContract]
    public partial class Location : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Location; } }
        [NotMapped]
        public override string TypeName { get { return "Location"; } }
        
        /// <summary>
        /// Indicates whether the location is still in use.
        /// (url: http://hl7.org/fhir/ValueSet/location-status)
        /// </summary>
        [FhirEnumeration("LocationStatus")]
        public enum LocationStatus
        {
            /// <summary>
            /// The location is operational.
            /// (system: http://hl7.org/fhir/location-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/location-status"), Description("Active")]
            Active,
            /// <summary>
            /// The location is temporarily closed.
            /// (system: http://hl7.org/fhir/location-status)
            /// </summary>
            [EnumLiteral("suspended", "http://hl7.org/fhir/location-status"), Description("Suspended")]
            Suspended,
            /// <summary>
            /// The location is no longer used.
            /// (system: http://hl7.org/fhir/location-status)
            /// </summary>
            [EnumLiteral("inactive", "http://hl7.org/fhir/location-status"), Description("Inactive")]
            Inactive,
        }

        /// <summary>
        /// Indicates whether a resource instance represents a specific location or a class of locations.
        /// (url: http://hl7.org/fhir/ValueSet/location-mode)
        /// </summary>
        [FhirEnumeration("LocationMode")]
        public enum LocationMode
        {
            /// <summary>
            /// The Location resource represents a specific instance of a location (e.g. Operating Theatre 1A).
            /// (system: http://hl7.org/fhir/location-mode)
            /// </summary>
            [EnumLiteral("instance", "http://hl7.org/fhir/location-mode"), Description("Instance")]
            Instance,
            /// <summary>
            /// The Location represents a class of locations (e.g. Any Operating Theatre) although this class of locations could be constrained within a specific boundary (such as organization, or parent location, address etc.).
            /// (system: http://hl7.org/fhir/location-mode)
            /// </summary>
            [EnumLiteral("kind", "http://hl7.org/fhir/location-mode"), Description("Kind")]
            Kind,
        }

        [FhirType("PositionComponent")]
        [DataContract]
        public partial class PositionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PositionComponent"; } }
            
            /// <summary>
            /// Longitude with WGS84 datum
            /// </summary>
            [FhirElement("longitude", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LongitudeElement
            {
                get { return _LongitudeElement; }
                set { _LongitudeElement = value; OnPropertyChanged("LongitudeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _LongitudeElement;
            
            /// <summary>
            /// Longitude with WGS84 datum
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Longitude
            {
                get { return LongitudeElement != null ? LongitudeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        LongitudeElement = null; 
                    else
                        LongitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Longitude");
                }
            }
            
            /// <summary>
            /// Latitude with WGS84 datum
            /// </summary>
            [FhirElement("latitude", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LatitudeElement
            {
                get { return _LatitudeElement; }
                set { _LatitudeElement = value; OnPropertyChanged("LatitudeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _LatitudeElement;
            
            /// <summary>
            /// Latitude with WGS84 datum
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Latitude
            {
                get { return LatitudeElement != null ? LatitudeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        LatitudeElement = null; 
                    else
                        LatitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Latitude");
                }
            }
            
            /// <summary>
            /// Altitude with WGS84 datum
            /// </summary>
            [FhirElement("altitude", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal AltitudeElement
            {
                get { return _AltitudeElement; }
                set { _AltitudeElement = value; OnPropertyChanged("AltitudeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _AltitudeElement;
            
            /// <summary>
            /// Altitude with WGS84 datum
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Altitude
            {
                get { return AltitudeElement != null ? AltitudeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AltitudeElement = null; 
                    else
                        AltitudeElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Altitude");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PositionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LongitudeElement != null) dest.LongitudeElement = (Hl7.Fhir.Model.FhirDecimal)LongitudeElement.DeepCopy();
                    if(LatitudeElement != null) dest.LatitudeElement = (Hl7.Fhir.Model.FhirDecimal)LatitudeElement.DeepCopy();
                    if(AltitudeElement != null) dest.AltitudeElement = (Hl7.Fhir.Model.FhirDecimal)AltitudeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PositionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PositionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LongitudeElement, otherT.LongitudeElement)) return false;
                if( !DeepComparable.Matches(LatitudeElement, otherT.LatitudeElement)) return false;
                if( !DeepComparable.Matches(AltitudeElement, otherT.AltitudeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PositionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LongitudeElement, otherT.LongitudeElement)) return false;
                if( !DeepComparable.IsExactly(LatitudeElement, otherT.LatitudeElement)) return false;
                if( !DeepComparable.IsExactly(AltitudeElement, otherT.AltitudeElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LongitudeElement != null) yield return LongitudeElement;
                    if (LatitudeElement != null) yield return LatitudeElement;
                    if (AltitudeElement != null) yield return AltitudeElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LongitudeElement != null) yield return new ElementValue("longitude", LongitudeElement);
                    if (LatitudeElement != null) yield return new ElementValue("latitude", LatitudeElement);
                    if (AltitudeElement != null) yield return new ElementValue("altitude", AltitudeElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique code or number identifying the location to its users
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
        /// active | suspended | inactive
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Location.LocationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Location.LocationStatus> _StatusElement;
        
        /// <summary>
        /// active | suspended | inactive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Location.LocationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Location.LocationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Name of the location as used by humans
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name of the location as used by humans
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Description of the location
        /// </summary>
        [FhirElement("description", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Description of the location
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if (value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// instance | kind
        /// </summary>
        [FhirElement("mode", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Location.LocationMode> ModeElement
        {
            get { return _ModeElement; }
            set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Location.LocationMode> _ModeElement;
        
        /// <summary>
        /// instance | kind
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Location.LocationMode? Mode
        {
            get { return ModeElement != null ? ModeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ModeElement = null; 
                else
                  ModeElement = new Code<Hl7.Fhir.Model.Location.LocationMode>(value);
                OnPropertyChanged("Mode");
            }
        }
        
        /// <summary>
        /// Type of function performed
        /// </summary>
        [FhirElement("type", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Contact details of the location
        /// </summary>
        [FhirElement("telecom", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
        
        /// <summary>
        /// Physical location
        /// </summary>
        [FhirElement("address", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Address Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged("Address"); }
        }
        
        private Hl7.Fhir.Model.Address _Address;
        
        /// <summary>
        /// Physical form of the location
        /// </summary>
        [FhirElement("physicalType", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PhysicalType
        {
            get { return _PhysicalType; }
            set { _PhysicalType = value; OnPropertyChanged("PhysicalType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PhysicalType;
        
        /// <summary>
        /// The absolute geographic location
        /// </summary>
        [FhirElement("position", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Location.PositionComponent Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged("Position"); }
        }
        
        private Hl7.Fhir.Model.Location.PositionComponent _Position;
        
        /// <summary>
        /// Organization responsible for provisioning and upkeep
        /// </summary>
        [FhirElement("managingOrganization", InSummary=true, Order=190)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization
        {
            get { return _ManagingOrganization; }
            set { _ManagingOrganization = value; OnPropertyChanged("ManagingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ManagingOrganization;
        
        /// <summary>
        /// Another Location this one is physically part of
        /// </summary>
        [FhirElement("partOf", Order=200)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf
        {
            get { return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PartOf;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Location;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Location.LocationStatus>)StatusElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Location.LocationMode>)ModeElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if(Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
                if(PhysicalType != null) dest.PhysicalType = (Hl7.Fhir.Model.CodeableConcept)PhysicalType.DeepCopy();
                if(Position != null) dest.Position = (Hl7.Fhir.Model.Location.PositionComponent)Position.DeepCopy();
                if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
                if(PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Location());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Location;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(Address, otherT.Address)) return false;
            if( !DeepComparable.Matches(PhysicalType, otherT.PhysicalType)) return false;
            if( !DeepComparable.Matches(Position, otherT.Position)) return false;
            if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Location;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if( !DeepComparable.IsExactly(PhysicalType, otherT.PhysicalType)) return false;
            if( !DeepComparable.IsExactly(Position, otherT.Position)) return false;
            if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (NameElement != null) yield return NameElement;
				if (DescriptionElement != null) yield return DescriptionElement;
				if (ModeElement != null) yield return ModeElement;
				if (Type != null) yield return Type;
				foreach (var elem in Telecom) { if (elem != null) yield return elem; }
				if (Address != null) yield return Address;
				if (PhysicalType != null) yield return PhysicalType;
				if (Position != null) yield return Position;
				if (ManagingOrganization != null) yield return ManagingOrganization;
				if (PartOf != null) yield return PartOf;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                if (Address != null) yield return new ElementValue("address", Address);
                if (PhysicalType != null) yield return new ElementValue("physicalType", PhysicalType);
                if (Position != null) yield return new ElementValue("position", Position);
                if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                if (PartOf != null) yield return new ElementValue("partOf", PartOf);
            }
        }

    }
    
}
