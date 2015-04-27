using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The details of a Healthcare Service available at a location
    /// </summary>
    [FhirType("HealthcareService", IsResource=true)]
    [DataContract]
    public partial class HealthcareService : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.HealthcareService; } }
        [NotMapped]
        public override string TypeName { get { return "HealthcareService"; } }
        
        /// <summary>
        /// The code(s) that detail the conditions under which the healthcare service is available/offered
        /// </summary>
        [FhirEnumeration("ServiceProvisionConditions")]
        public enum ServiceProvisionConditions
        {
            /// <summary>
            /// This service is available for no patient cost.
            /// </summary>
            [EnumLiteral("free")]
            Free,
            /// <summary>
            /// There are discounts available on this service for qualifying patients.
            /// </summary>
            [EnumLiteral("disc")]
            Disc,
            /// <summary>
            /// Fees apply for this service.
            /// </summary>
            [EnumLiteral("cost")]
            Cost,
        }
        
        /// <summary>
        /// The methods of referral can be used when referring to a specific HealthCareService resource
        /// </summary>
        [FhirEnumeration("ReferralMethod")]
        public enum ReferralMethod
        {
            /// <summary>
            /// Referrals may be accepted by fax.
            /// </summary>
            [EnumLiteral("fax")]
            Fax,
            /// <summary>
            /// Referrals may be accepted over the phone from a Practitioner.
            /// </summary>
            [EnumLiteral("phone")]
            Phone,
            /// <summary>
            /// Referrals may be accepted via a secure messaging system. To determine the types of secure messaging systems supported, refer to the identifiers collection. Callers will need to understand the specific identifier system used to know that they are able to transmit messages.
            /// </summary>
            [EnumLiteral("elec")]
            Elec,
            /// <summary>
            /// Referrals may be accepted via a secure email. To send please enrypt with the services public key.
            /// </summary>
            [EnumLiteral("semail")]
            Semail,
            /// <summary>
            /// Referrals may be accepted via regular postage (or hand delivered).
            /// </summary>
            [EnumLiteral("mail")]
            Mail,
        }
        
        /// <summary>
        /// The days of the week
        /// </summary>
        [FhirEnumeration("DaysOfWeek")]
        public enum DaysOfWeek
        {
            /// <summary>
            /// Monday.
            /// </summary>
            [EnumLiteral("mon")]
            Mon,
            /// <summary>
            /// Tuesday.
            /// </summary>
            [EnumLiteral("tue")]
            Tue,
            /// <summary>
            /// Wednesday.
            /// </summary>
            [EnumLiteral("wed")]
            Wed,
            /// <summary>
            /// Thursday.
            /// </summary>
            [EnumLiteral("thu")]
            Thu,
            /// <summary>
            /// Friday.
            /// </summary>
            [EnumLiteral("fri")]
            Fri,
            /// <summary>
            /// Saturday.
            /// </summary>
            [EnumLiteral("sat")]
            Sat,
            /// <summary>
            /// Sunday.
            /// </summary>
            [EnumLiteral("sun")]
            Sun,
        }
        
        [FhirType("HealthcareServiceNotAvailableComponent")]
        [DataContract]
        public partial class HealthcareServiceNotAvailableComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HealthcareServiceNotAvailableComponent"; } }
            
            /// <summary>
            /// The reason that can be presented to the user as to why this time is not available
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// The reason that can be presented to the user as to why this time is not available
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            /// Service is not available (seasonally or for a public holiday) from this date
            /// </summary>
            [FhirElement("during", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period During
            {
                get { return _During; }
                set { _During = value; OnPropertyChanged("During"); }
            }
            
            private Hl7.Fhir.Model.Period _During;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HealthcareServiceNotAvailableComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(During != null) dest.During = (Hl7.Fhir.Model.Period)During.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HealthcareServiceNotAvailableComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HealthcareServiceNotAvailableComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(During, otherT.During)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HealthcareServiceNotAvailableComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(During, otherT.During)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ServiceTypeComponent")]
        [DataContract]
        public partial class ServiceTypeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ServiceTypeComponent"; } }
            
            /// <summary>
            /// The specific type of service being delivered or performed
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Collection of Specialties handled by the Service Site. This is more of a Medical Term
            /// </summary>
            [FhirElement("specialty", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Specialty
            {
                get { if(_Specialty==null) _Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Specialty; }
                set { _Specialty = value; OnPropertyChanged("Specialty"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ServiceTypeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ServiceTypeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ServiceTypeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ServiceTypeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("HealthcareServiceAvailableTimeComponent")]
        [DataContract]
        public partial class HealthcareServiceAvailableTimeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HealthcareServiceAvailableTimeComponent"; } }
            
            /// <summary>
            /// mon | tue | wed | thu | fri | sat | sun
            /// </summary>
            [FhirElement("daysOfWeek", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.HealthcareService.DaysOfWeek>> DaysOfWeekElement
            {
                get { if(_DaysOfWeekElement==null) _DaysOfWeekElement = new List<Code<Hl7.Fhir.Model.HealthcareService.DaysOfWeek>>(); return _DaysOfWeekElement; }
                set { _DaysOfWeekElement = value; OnPropertyChanged("DaysOfWeekElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.HealthcareService.DaysOfWeek>> _DaysOfWeekElement;
            
            /// <summary>
            /// mon | tue | wed | thu | fri | sat | sun
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.HealthcareService.DaysOfWeek?> DaysOfWeek
            {
                get { return DaysOfWeekElement != null ? DaysOfWeekElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      DaysOfWeekElement = null; 
                    else
                      DaysOfWeekElement = new List<Code<Hl7.Fhir.Model.HealthcareService.DaysOfWeek>>(value.Select(elem=>new Code<Hl7.Fhir.Model.HealthcareService.DaysOfWeek>(elem)));
                    OnPropertyChanged("DaysOfWeek");
                }
            }
            
            /// <summary>
            /// Is this always available? (hence times are irrelevant) e.g. 24 hour service
            /// </summary>
            [FhirElement("allDay", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllDayElement
            {
                get { return _AllDayElement; }
                set { _AllDayElement = value; OnPropertyChanged("AllDayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllDayElement;
            
            /// <summary>
            /// Is this always available? (hence times are irrelevant) e.g. 24 hour service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? AllDay
            {
                get { return AllDayElement != null ? AllDayElement.Value : null; }
                set
                {
                    if(value == null)
                      AllDayElement = null; 
                    else
                      AllDayElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("AllDay");
                }
            }
            
            /// <summary>
            /// The opening time of day. Note: If the AllDay flag is set, then this time is ignored
            /// </summary>
            [FhirElement("availableStartTime", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Time AvailableStartTimeElement
            {
                get { return _AvailableStartTimeElement; }
                set { _AvailableStartTimeElement = value; OnPropertyChanged("AvailableStartTimeElement"); }
            }
            
            private Hl7.Fhir.Model.Time _AvailableStartTimeElement;
            
            /// <summary>
            /// The opening time of day. Note: If the AllDay flag is set, then this time is ignored
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AvailableStartTime
            {
                get { return AvailableStartTimeElement != null ? AvailableStartTimeElement.Value : null; }
                set
                {
                    if(value == null)
                      AvailableStartTimeElement = null; 
                    else
                      AvailableStartTimeElement = new Hl7.Fhir.Model.Time(value);
                    OnPropertyChanged("AvailableStartTime");
                }
            }
            
            /// <summary>
            /// The closing time of day. Note: If the AllDay flag is set, then this time is ignored
            /// </summary>
            [FhirElement("availableEndTime", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Time AvailableEndTimeElement
            {
                get { return _AvailableEndTimeElement; }
                set { _AvailableEndTimeElement = value; OnPropertyChanged("AvailableEndTimeElement"); }
            }
            
            private Hl7.Fhir.Model.Time _AvailableEndTimeElement;
            
            /// <summary>
            /// The closing time of day. Note: If the AllDay flag is set, then this time is ignored
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AvailableEndTime
            {
                get { return AvailableEndTimeElement != null ? AvailableEndTimeElement.Value : null; }
                set
                {
                    if(value == null)
                      AvailableEndTimeElement = null; 
                    else
                      AvailableEndTimeElement = new Hl7.Fhir.Model.Time(value);
                    OnPropertyChanged("AvailableEndTime");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HealthcareServiceAvailableTimeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DaysOfWeekElement != null) dest.DaysOfWeekElement = new List<Code<Hl7.Fhir.Model.HealthcareService.DaysOfWeek>>(DaysOfWeekElement.DeepCopy());
                    if(AllDayElement != null) dest.AllDayElement = (Hl7.Fhir.Model.FhirBoolean)AllDayElement.DeepCopy();
                    if(AvailableStartTimeElement != null) dest.AvailableStartTimeElement = (Hl7.Fhir.Model.Time)AvailableStartTimeElement.DeepCopy();
                    if(AvailableEndTimeElement != null) dest.AvailableEndTimeElement = (Hl7.Fhir.Model.Time)AvailableEndTimeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HealthcareServiceAvailableTimeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HealthcareServiceAvailableTimeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DaysOfWeekElement, otherT.DaysOfWeekElement)) return false;
                if( !DeepComparable.Matches(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.Matches(AvailableStartTimeElement, otherT.AvailableStartTimeElement)) return false;
                if( !DeepComparable.Matches(AvailableEndTimeElement, otherT.AvailableEndTimeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HealthcareServiceAvailableTimeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DaysOfWeekElement, otherT.DaysOfWeekElement)) return false;
                if( !DeepComparable.IsExactly(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.IsExactly(AvailableStartTimeElement, otherT.AvailableStartTimeElement)) return false;
                if( !DeepComparable.IsExactly(AvailableEndTimeElement, otherT.AvailableEndTimeElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Identifiers for this item
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
        /// The organization that provides this Healthcare Service
        /// </summary>
        [FhirElement("providedBy", InSummary=true, Order=100)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ProvidedBy
        {
            get { return _ProvidedBy; }
            set { _ProvidedBy = value; OnPropertyChanged("ProvidedBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ProvidedBy;
        
        /// <summary>
        /// The location where this healthcare service may be provided
        /// </summary>
        [FhirElement("location", InSummary=true, Order=110)]
        [References("Location")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Identifies the broad category of service being performed or delivered. Selecting a Service Category then determines the list of relevant service types that can be selected in the Primary Service Type
        /// </summary>
        [FhirElement("serviceCategory", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ServiceCategory
        {
            get { return _ServiceCategory; }
            set { _ServiceCategory = value; OnPropertyChanged("ServiceCategory"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ServiceCategory;
        
        /// <summary>
        /// A specific type of service that may be delivered or performed
        /// </summary>
        [FhirElement("serviceType", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.HealthcareService.ServiceTypeComponent> ServiceType
        {
            get { if(_ServiceType==null) _ServiceType = new List<Hl7.Fhir.Model.HealthcareService.ServiceTypeComponent>(); return _ServiceType; }
            set { _ServiceType = value; OnPropertyChanged("ServiceType"); }
        }
        
        private List<Hl7.Fhir.Model.HealthcareService.ServiceTypeComponent> _ServiceType;
        
        /// <summary>
        /// Further description of the service as it would be presented to a consumer while searching
        /// </summary>
        [FhirElement("serviceName", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ServiceNameElement
        {
            get { return _ServiceNameElement; }
            set { _ServiceNameElement = value; OnPropertyChanged("ServiceNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ServiceNameElement;
        
        /// <summary>
        /// Further description of the service as it would be presented to a consumer while searching
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ServiceName
        {
            get { return ServiceNameElement != null ? ServiceNameElement.Value : null; }
            set
            {
                if(value == null)
                  ServiceNameElement = null; 
                else
                  ServiceNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ServiceName");
            }
        }
        
        /// <summary>
        /// Any additional description of the service and/or any specific issues not covered by the other attributes, which can be displayed as further detail under the serviceName
        /// </summary>
        [FhirElement("comment", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Any additional description of the service and/or any specific issues not covered by the other attributes, which can be displayed as further detail under the serviceName
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if(value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Extra details about the service that can't be placed in the other fields
        /// </summary>
        [FhirElement("extraDetails", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ExtraDetailsElement
        {
            get { return _ExtraDetailsElement; }
            set { _ExtraDetailsElement = value; OnPropertyChanged("ExtraDetailsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ExtraDetailsElement;
        
        /// <summary>
        /// Extra details about the service that can't be placed in the other fields
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ExtraDetails
        {
            get { return ExtraDetailsElement != null ? ExtraDetailsElement.Value : null; }
            set
            {
                if(value == null)
                  ExtraDetailsElement = null; 
                else
                  ExtraDetailsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ExtraDetails");
            }
        }
        
        /// <summary>
        /// If there is a photo/symbol associated with this HealthcareService, it may be included here to facilitate quick identification of the service in a list
        /// </summary>
        [FhirElement("photo", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Photo
        {
            get { return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Photo;
        
        /// <summary>
        /// List of contacts related to this specific healthcare service. If this is empty, then refer to the location's contacts
        /// </summary>
        [FhirElement("telecom", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
        
        /// <summary>
        /// The location(s) that this service is available to (not where the service is provided)
        /// </summary>
        [FhirElement("coverageArea", Order=190)]
        [References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CoverageArea
        {
            get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(); return _CoverageArea; }
            set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CoverageArea;
        
        /// <summary>
        /// The code(s) that detail the conditions under which the healthcare service is available/offered
        /// </summary>
        [FhirElement("serviceProvisionCode", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceProvisionCode
        {
            get { if(_ServiceProvisionCode==null) _ServiceProvisionCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceProvisionCode; }
            set { _ServiceProvisionCode = value; OnPropertyChanged("ServiceProvisionCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceProvisionCode;
        
        /// <summary>
        /// Does this service have specific eligibility requirements that need to be met in order to use the service
        /// </summary>
        [FhirElement("eligibility", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Eligibility
        {
            get { return _Eligibility; }
            set { _Eligibility = value; OnPropertyChanged("Eligibility"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Eligibility;
        
        /// <summary>
        /// Describes the eligibility conditions for the service
        /// </summary>
        [FhirElement("eligibilityNote", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString EligibilityNoteElement
        {
            get { return _EligibilityNoteElement; }
            set { _EligibilityNoteElement = value; OnPropertyChanged("EligibilityNoteElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _EligibilityNoteElement;
        
        /// <summary>
        /// Describes the eligibility conditions for the service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string EligibilityNote
        {
            get { return EligibilityNoteElement != null ? EligibilityNoteElement.Value : null; }
            set
            {
                if(value == null)
                  EligibilityNoteElement = null; 
                else
                  EligibilityNoteElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("EligibilityNote");
            }
        }
        
        /// <summary>
        /// Program Names that can be used to categorize the service
        /// </summary>
        [FhirElement("programName", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ProgramNameElement
        {
            get { if(_ProgramNameElement==null) _ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(); return _ProgramNameElement; }
            set { _ProgramNameElement = value; OnPropertyChanged("ProgramNameElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ProgramNameElement;
        
        /// <summary>
        /// Program Names that can be used to categorize the service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> ProgramName
        {
            get { return ProgramNameElement != null ? ProgramNameElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  ProgramNameElement = null; 
                else
                  ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("ProgramName");
            }
        }
        
        /// <summary>
        /// Collection of Characteristics (attributes)
        /// </summary>
        [FhirElement("characteristic", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Characteristic
        {
            get { if(_Characteristic==null) _Characteristic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Characteristic; }
            set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Characteristic;
        
        /// <summary>
        /// Ways that the service accepts referrals
        /// </summary>
        [FhirElement("referralMethod", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReferralMethod_
        {
            get { if(_ReferralMethod_==null) _ReferralMethod_ = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReferralMethod_; }
            set { _ReferralMethod_ = value; OnPropertyChanged("ReferralMethod_"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReferralMethod_;
        
        /// <summary>
        /// The public part of the 'keys' allocated to an Organization by an accredited body to support secure exchange of data over the internet. To be provided by the Organization, where available
        /// </summary>
        [FhirElement("publicKey", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublicKeyElement
        {
            get { return _PublicKeyElement; }
            set { _PublicKeyElement = value; OnPropertyChanged("PublicKeyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublicKeyElement;
        
        /// <summary>
        /// The public part of the 'keys' allocated to an Organization by an accredited body to support secure exchange of data over the internet. To be provided by the Organization, where available
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PublicKey
        {
            get { return PublicKeyElement != null ? PublicKeyElement.Value : null; }
            set
            {
                if(value == null)
                  PublicKeyElement = null; 
                else
                  PublicKeyElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PublicKey");
            }
        }
        
        /// <summary>
        /// Indicates if an appointment is required for access to this service
        /// </summary>
        [FhirElement("appointmentRequired", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AppointmentRequiredElement
        {
            get { return _AppointmentRequiredElement; }
            set { _AppointmentRequiredElement = value; OnPropertyChanged("AppointmentRequiredElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _AppointmentRequiredElement;
        
        /// <summary>
        /// Indicates if an appointment is required for access to this service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? AppointmentRequired
        {
            get { return AppointmentRequiredElement != null ? AppointmentRequiredElement.Value : null; }
            set
            {
                if(value == null)
                  AppointmentRequiredElement = null; 
                else
                  AppointmentRequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("AppointmentRequired");
            }
        }
        
        /// <summary>
        /// A Collection of times that the Service Site is available
        /// </summary>
        [FhirElement("availableTime", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent> AvailableTime
        {
            get { if(_AvailableTime==null) _AvailableTime = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent>(); return _AvailableTime; }
            set { _AvailableTime = value; OnPropertyChanged("AvailableTime"); }
        }
        
        private List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent> _AvailableTime;
        
        /// <summary>
        /// The HealthcareService is not available during this period of time due to the provided reason
        /// </summary>
        [FhirElement("notAvailable", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableComponent> NotAvailable
        {
            get { if(_NotAvailable==null) _NotAvailable = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableComponent>(); return _NotAvailable; }
            set { _NotAvailable = value; OnPropertyChanged("NotAvailable"); }
        }
        
        private List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableComponent> _NotAvailable;
        
        /// <summary>
        /// A description of Site availability exceptions, e.g., public holiday availability. Succinctly describing all possible exceptions to normal Site availability as details in the Available Times and Not Available Times
        /// </summary>
        [FhirElement("availabilityExceptions", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AvailabilityExceptionsElement
        {
            get { return _AvailabilityExceptionsElement; }
            set { _AvailabilityExceptionsElement = value; OnPropertyChanged("AvailabilityExceptionsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AvailabilityExceptionsElement;
        
        /// <summary>
        /// A description of Site availability exceptions, e.g., public holiday availability. Succinctly describing all possible exceptions to normal Site availability as details in the Available Times and Not Available Times
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AvailabilityExceptions
        {
            get { return AvailabilityExceptionsElement != null ? AvailabilityExceptionsElement.Value : null; }
            set
            {
                if(value == null)
                  AvailabilityExceptionsElement = null; 
                else
                  AvailabilityExceptionsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AvailabilityExceptions");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as HealthcareService;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ProvidedBy != null) dest.ProvidedBy = (Hl7.Fhir.Model.ResourceReference)ProvidedBy.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ServiceCategory != null) dest.ServiceCategory = (Hl7.Fhir.Model.CodeableConcept)ServiceCategory.DeepCopy();
                if(ServiceType != null) dest.ServiceType = new List<Hl7.Fhir.Model.HealthcareService.ServiceTypeComponent>(ServiceType.DeepCopy());
                if(ServiceNameElement != null) dest.ServiceNameElement = (Hl7.Fhir.Model.FhirString)ServiceNameElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(ExtraDetailsElement != null) dest.ExtraDetailsElement = (Hl7.Fhir.Model.FhirString)ExtraDetailsElement.DeepCopy();
                if(Photo != null) dest.Photo = (Hl7.Fhir.Model.Attachment)Photo.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(CoverageArea.DeepCopy());
                if(ServiceProvisionCode != null) dest.ServiceProvisionCode = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceProvisionCode.DeepCopy());
                if(Eligibility != null) dest.Eligibility = (Hl7.Fhir.Model.CodeableConcept)Eligibility.DeepCopy();
                if(EligibilityNoteElement != null) dest.EligibilityNoteElement = (Hl7.Fhir.Model.FhirString)EligibilityNoteElement.DeepCopy();
                if(ProgramNameElement != null) dest.ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(ProgramNameElement.DeepCopy());
                if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.CodeableConcept>(Characteristic.DeepCopy());
                if(ReferralMethod_ != null) dest.ReferralMethod_ = new List<Hl7.Fhir.Model.CodeableConcept>(ReferralMethod_.DeepCopy());
                if(PublicKeyElement != null) dest.PublicKeyElement = (Hl7.Fhir.Model.FhirString)PublicKeyElement.DeepCopy();
                if(AppointmentRequiredElement != null) dest.AppointmentRequiredElement = (Hl7.Fhir.Model.FhirBoolean)AppointmentRequiredElement.DeepCopy();
                if(AvailableTime != null) dest.AvailableTime = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent>(AvailableTime.DeepCopy());
                if(NotAvailable != null) dest.NotAvailable = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableComponent>(NotAvailable.DeepCopy());
                if(AvailabilityExceptionsElement != null) dest.AvailabilityExceptionsElement = (Hl7.Fhir.Model.FhirString)AvailabilityExceptionsElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new HealthcareService());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as HealthcareService;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ProvidedBy, otherT.ProvidedBy)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.Matches(ServiceNameElement, otherT.ServiceNameElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(ExtraDetailsElement, otherT.ExtraDetailsElement)) return false;
            if( !DeepComparable.Matches(Photo, otherT.Photo)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.Matches(ServiceProvisionCode, otherT.ServiceProvisionCode)) return false;
            if( !DeepComparable.Matches(Eligibility, otherT.Eligibility)) return false;
            if( !DeepComparable.Matches(EligibilityNoteElement, otherT.EligibilityNoteElement)) return false;
            if( !DeepComparable.Matches(ProgramNameElement, otherT.ProgramNameElement)) return false;
            if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.Matches(ReferralMethod_, otherT.ReferralMethod_)) return false;
            if( !DeepComparable.Matches(PublicKeyElement, otherT.PublicKeyElement)) return false;
            if( !DeepComparable.Matches(AppointmentRequiredElement, otherT.AppointmentRequiredElement)) return false;
            if( !DeepComparable.Matches(AvailableTime, otherT.AvailableTime)) return false;
            if( !DeepComparable.Matches(NotAvailable, otherT.NotAvailable)) return false;
            if( !DeepComparable.Matches(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as HealthcareService;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ProvidedBy, otherT.ProvidedBy)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.IsExactly(ServiceNameElement, otherT.ServiceNameElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(ExtraDetailsElement, otherT.ExtraDetailsElement)) return false;
            if( !DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.IsExactly(ServiceProvisionCode, otherT.ServiceProvisionCode)) return false;
            if( !DeepComparable.IsExactly(Eligibility, otherT.Eligibility)) return false;
            if( !DeepComparable.IsExactly(EligibilityNoteElement, otherT.EligibilityNoteElement)) return false;
            if( !DeepComparable.IsExactly(ProgramNameElement, otherT.ProgramNameElement)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.IsExactly(ReferralMethod_, otherT.ReferralMethod_)) return false;
            if( !DeepComparable.IsExactly(PublicKeyElement, otherT.PublicKeyElement)) return false;
            if( !DeepComparable.IsExactly(AppointmentRequiredElement, otherT.AppointmentRequiredElement)) return false;
            if( !DeepComparable.IsExactly(AvailableTime, otherT.AvailableTime)) return false;
            if( !DeepComparable.IsExactly(NotAvailable, otherT.NotAvailable)) return false;
            if( !DeepComparable.IsExactly(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
            
            return true;
        }
        
    }
    
}
