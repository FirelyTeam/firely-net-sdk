﻿using System;
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
// Generated for FHIR v4.0.0
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
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/location-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/location-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/location-status)
            /// </summary>
            [EnumLiteral("suspended", "http://hl7.org/fhir/location-status"), Description("Suspended")]
            Suspended,
            /// <summary>
            /// MISSING DESCRIPTION
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
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/location-mode)
            /// </summary>
            [EnumLiteral("instance", "http://hl7.org/fhir/location-mode"), Description("Instance")]
            Instance,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/location-mode)
            /// </summary>
            [EnumLiteral("kind", "http://hl7.org/fhir/location-mode"), Description("Kind")]
            Kind,
        }

        [FhirType("PositionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PositionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
        
        
        [FhirType("HoursOfOperationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class HoursOfOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HoursOfOperationComponent"; } }
            
            /// <summary>
            /// mon | tue | wed | thu | fri | sat | sun
            /// </summary>
            [FhirElement("daysOfWeek", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.DaysOfWeek>> DaysOfWeekElement
            {
                get { if(_DaysOfWeekElement==null) _DaysOfWeekElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>>(); return _DaysOfWeekElement; }
                set { _DaysOfWeekElement = value; OnPropertyChanged("DaysOfWeekElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.DaysOfWeek>> _DaysOfWeekElement;
            
            /// <summary>
            /// mon | tue | wed | thu | fri | sat | sun
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.DaysOfWeek?> DaysOfWeek
            {
                get { return DaysOfWeekElement != null ? DaysOfWeekElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        DaysOfWeekElement = null; 
                    else
                        DaysOfWeekElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>(elem)));
                    OnPropertyChanged("DaysOfWeek");
                }
            }
            
            /// <summary>
            /// The Location is open all day
            /// </summary>
            [FhirElement("allDay", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllDayElement
            {
                get { return _AllDayElement; }
                set { _AllDayElement = value; OnPropertyChanged("AllDayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllDayElement;
            
            /// <summary>
            /// The Location is open all day
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? AllDay
            {
                get { return AllDayElement != null ? AllDayElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AllDayElement = null; 
                    else
                        AllDayElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("AllDay");
                }
            }
            
            /// <summary>
            /// Time that the Location opens
            /// </summary>
            [FhirElement("openingTime", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Time OpeningTimeElement
            {
                get { return _OpeningTimeElement; }
                set { _OpeningTimeElement = value; OnPropertyChanged("OpeningTimeElement"); }
            }
            
            private Hl7.Fhir.Model.Time _OpeningTimeElement;
            
            /// <summary>
            /// Time that the Location opens
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string OpeningTime
            {
                get { return OpeningTimeElement != null ? OpeningTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        OpeningTimeElement = null; 
                    else
                        OpeningTimeElement = new Hl7.Fhir.Model.Time(value);
                    OnPropertyChanged("OpeningTime");
                }
            }
            
            /// <summary>
            /// Time that the Location closes
            /// </summary>
            [FhirElement("closingTime", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Time ClosingTimeElement
            {
                get { return _ClosingTimeElement; }
                set { _ClosingTimeElement = value; OnPropertyChanged("ClosingTimeElement"); }
            }
            
            private Hl7.Fhir.Model.Time _ClosingTimeElement;
            
            /// <summary>
            /// Time that the Location closes
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ClosingTime
            {
                get { return ClosingTimeElement != null ? ClosingTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        ClosingTimeElement = null; 
                    else
                        ClosingTimeElement = new Hl7.Fhir.Model.Time(value);
                    OnPropertyChanged("ClosingTime");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HoursOfOperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DaysOfWeekElement != null) dest.DaysOfWeekElement = new List<Code<Hl7.Fhir.Model.DaysOfWeek>>(DaysOfWeekElement.DeepCopy());
                    if(AllDayElement != null) dest.AllDayElement = (Hl7.Fhir.Model.FhirBoolean)AllDayElement.DeepCopy();
                    if(OpeningTimeElement != null) dest.OpeningTimeElement = (Hl7.Fhir.Model.Time)OpeningTimeElement.DeepCopy();
                    if(ClosingTimeElement != null) dest.ClosingTimeElement = (Hl7.Fhir.Model.Time)ClosingTimeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HoursOfOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HoursOfOperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DaysOfWeekElement, otherT.DaysOfWeekElement)) return false;
                if( !DeepComparable.Matches(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.Matches(OpeningTimeElement, otherT.OpeningTimeElement)) return false;
                if( !DeepComparable.Matches(ClosingTimeElement, otherT.ClosingTimeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HoursOfOperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DaysOfWeekElement, otherT.DaysOfWeekElement)) return false;
                if( !DeepComparable.IsExactly(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.IsExactly(OpeningTimeElement, otherT.OpeningTimeElement)) return false;
                if( !DeepComparable.IsExactly(ClosingTimeElement, otherT.ClosingTimeElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in DaysOfWeekElement) { if (elem != null) yield return elem; }
                    if (AllDayElement != null) yield return AllDayElement;
                    if (OpeningTimeElement != null) yield return OpeningTimeElement;
                    if (ClosingTimeElement != null) yield return ClosingTimeElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in DaysOfWeekElement) { if (elem != null) yield return new ElementValue("daysOfWeek", elem); }
                    if (AllDayElement != null) yield return new ElementValue("allDay", AllDayElement);
                    if (OpeningTimeElement != null) yield return new ElementValue("openingTime", OpeningTimeElement);
                    if (ClosingTimeElement != null) yield return new ElementValue("closingTime", ClosingTimeElement);
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
        /// The operational status of the location (typically only for a bed/room)
        /// </summary>
        [FhirElement("operationalStatus", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Coding OperationalStatus
        {
            get { return _OperationalStatus; }
            set { _OperationalStatus = value; OnPropertyChanged("OperationalStatus"); }
        }
        
        private Hl7.Fhir.Model.Coding _OperationalStatus;
        
        /// <summary>
        /// Name of the location as used by humans
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
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
        /// A list of alternate names that the location is known as, or was known as, in the past
        /// </summary>
        [FhirElement("alias", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if(_AliasElement==null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _AliasElement;
        
        /// <summary>
        /// A list of alternate names that the location is known as, or was known as, in the past
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Alias
        {
            get { return AliasElement != null ? AliasElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  AliasElement = null; 
                else
                  AliasElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Alias");
            }
        }
        
        /// <summary>
        /// Additional details about the location that could be displayed as further information to identify the location beyond its name
        /// </summary>
        [FhirElement("description", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Additional details about the location that could be displayed as further information to identify the location beyond its name
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
        [FhirElement("mode", InSummary=true, Order=150)]
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
        [FhirElement("type", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// Contact details of the location
        /// </summary>
        [FhirElement("telecom", Order=170)]
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
        [FhirElement("address", Order=180)]
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
        [FhirElement("physicalType", InSummary=true, Order=190)]
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
        [FhirElement("position", Order=200)]
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
        [FhirElement("managingOrganization", InSummary=true, Order=210)]
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
        /// Another Location this one is physically a part of
        /// </summary>
        [FhirElement("partOf", Order=220)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf
        {
            get { return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PartOf;
        
        /// <summary>
        /// What days/times during a week is this location usually open
        /// </summary>
        [FhirElement("hoursOfOperation", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Location.HoursOfOperationComponent> HoursOfOperation
        {
            get { if(_HoursOfOperation==null) _HoursOfOperation = new List<Hl7.Fhir.Model.Location.HoursOfOperationComponent>(); return _HoursOfOperation; }
            set { _HoursOfOperation = value; OnPropertyChanged("HoursOfOperation"); }
        }
        
        private List<Hl7.Fhir.Model.Location.HoursOfOperationComponent> _HoursOfOperation;
        
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        [FhirElement("availabilityExceptions", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AvailabilityExceptionsElement
        {
            get { return _AvailabilityExceptionsElement; }
            set { _AvailabilityExceptionsElement = value; OnPropertyChanged("AvailabilityExceptionsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AvailabilityExceptionsElement;
        
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AvailabilityExceptions
        {
            get { return AvailabilityExceptionsElement != null ? AvailabilityExceptionsElement.Value : null; }
            set
            {
                if (value == null)
                  AvailabilityExceptionsElement = null; 
                else
                  AvailabilityExceptionsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AvailabilityExceptions");
            }
        }
        
        /// <summary>
        /// Technical endpoints providing access to services operated for the location
        /// </summary>
        [FhirElement("endpoint", Order=250)]
        [CLSCompliant(false)]
		[References("Endpoint")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Endpoint
        {
            get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
            set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
        

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
                if(OperationalStatus != null) dest.OperationalStatus = (Hl7.Fhir.Model.Coding)OperationalStatus.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Location.LocationMode>)ModeElement.DeepCopy();
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if(Address != null) dest.Address = (Hl7.Fhir.Model.Address)Address.DeepCopy();
                if(PhysicalType != null) dest.PhysicalType = (Hl7.Fhir.Model.CodeableConcept)PhysicalType.DeepCopy();
                if(Position != null) dest.Position = (Hl7.Fhir.Model.Location.PositionComponent)Position.DeepCopy();
                if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
                if(PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
                if(HoursOfOperation != null) dest.HoursOfOperation = new List<Hl7.Fhir.Model.Location.HoursOfOperationComponent>(HoursOfOperation.DeepCopy());
                if(AvailabilityExceptionsElement != null) dest.AvailabilityExceptionsElement = (Hl7.Fhir.Model.FhirString)AvailabilityExceptionsElement.DeepCopy();
                if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
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
            if( !DeepComparable.Matches(OperationalStatus, otherT.OperationalStatus)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(Address, otherT.Address)) return false;
            if( !DeepComparable.Matches(PhysicalType, otherT.PhysicalType)) return false;
            if( !DeepComparable.Matches(Position, otherT.Position)) return false;
            if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(HoursOfOperation, otherT.HoursOfOperation)) return false;
            if( !DeepComparable.Matches(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
            if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Location;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(OperationalStatus, otherT.OperationalStatus)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(Address, otherT.Address)) return false;
            if( !DeepComparable.IsExactly(PhysicalType, otherT.PhysicalType)) return false;
            if( !DeepComparable.IsExactly(Position, otherT.Position)) return false;
            if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(HoursOfOperation, otherT.HoursOfOperation)) return false;
            if( !DeepComparable.IsExactly(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
            if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
            
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
				if (OperationalStatus != null) yield return OperationalStatus;
				if (NameElement != null) yield return NameElement;
				foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
				if (DescriptionElement != null) yield return DescriptionElement;
				if (ModeElement != null) yield return ModeElement;
				foreach (var elem in Type) { if (elem != null) yield return elem; }
				foreach (var elem in Telecom) { if (elem != null) yield return elem; }
				if (Address != null) yield return Address;
				if (PhysicalType != null) yield return PhysicalType;
				if (Position != null) yield return Position;
				if (ManagingOrganization != null) yield return ManagingOrganization;
				if (PartOf != null) yield return PartOf;
				foreach (var elem in HoursOfOperation) { if (elem != null) yield return elem; }
				if (AvailabilityExceptionsElement != null) yield return AvailabilityExceptionsElement;
				foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
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
                if (OperationalStatus != null) yield return new ElementValue("operationalStatus", OperationalStatus);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                if (Address != null) yield return new ElementValue("address", Address);
                if (PhysicalType != null) yield return new ElementValue("physicalType", PhysicalType);
                if (Position != null) yield return new ElementValue("position", Position);
                if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                if (PartOf != null) yield return new ElementValue("partOf", PartOf);
                foreach (var elem in HoursOfOperation) { if (elem != null) yield return new ElementValue("hoursOfOperation", elem); }
                if (AvailabilityExceptionsElement != null) yield return new ElementValue("availabilityExceptions", AvailabilityExceptionsElement);
                foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
            }
        }

    }
    
}
