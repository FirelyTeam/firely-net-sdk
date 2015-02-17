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
// Generated on Tue, Feb 17, 2015 17:24+0100 for FHIR v0.4.0
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
            /// Indicates which Days of the week are available between the Start and End Times
            /// </summary>
            [FhirElement("daysOfWeek", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> DaysOfWeek
            {
                get { if(_DaysOfWeek==null) _DaysOfWeek = new List<Hl7.Fhir.Model.CodeableConcept>(); return _DaysOfWeek; }
                set { _DaysOfWeek = value; OnPropertyChanged("DaysOfWeek"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _DaysOfWeek;
            
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
            /// The opening time of day (the date is not included). Note: If the AllDay flag is set, then this time is ignored
            /// </summary>
            [FhirElement("availableStartTime", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime AvailableStartTimeElement
            {
                get { return _AvailableStartTimeElement; }
                set { _AvailableStartTimeElement = value; OnPropertyChanged("AvailableStartTimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _AvailableStartTimeElement;
            
            /// <summary>
            /// The opening time of day (the date is not included). Note: If the AllDay flag is set, then this time is ignored
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
                      AvailableStartTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("AvailableStartTime");
                }
            }
            
            /// <summary>
            /// The closing time of day (the date is not included). Note: If the AllDay flag is set, then this time is ignored
            /// </summary>
            [FhirElement("availableEndTime", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime AvailableEndTimeElement
            {
                get { return _AvailableEndTimeElement; }
                set { _AvailableEndTimeElement = value; OnPropertyChanged("AvailableEndTimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _AvailableEndTimeElement;
            
            /// <summary>
            /// The closing time of day (the date is not included). Note: If the AllDay flag is set, then this time is ignored
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
                      AvailableEndTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("AvailableEndTime");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HealthcareServiceAvailableTimeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DaysOfWeek != null) dest.DaysOfWeek = new List<Hl7.Fhir.Model.CodeableConcept>(DaysOfWeek.DeepCopy());
                    if(AllDayElement != null) dest.AllDayElement = (Hl7.Fhir.Model.FhirBoolean)AllDayElement.DeepCopy();
                    if(AvailableStartTimeElement != null) dest.AvailableStartTimeElement = (Hl7.Fhir.Model.FhirDateTime)AvailableStartTimeElement.DeepCopy();
                    if(AvailableEndTimeElement != null) dest.AvailableEndTimeElement = (Hl7.Fhir.Model.FhirDateTime)AvailableEndTimeElement.DeepCopy();
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
                if( !DeepComparable.Matches(DaysOfWeek, otherT.DaysOfWeek)) return false;
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
                if( !DeepComparable.IsExactly(DaysOfWeek, otherT.DaysOfWeek)) return false;
                if( !DeepComparable.IsExactly(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.IsExactly(AvailableStartTimeElement, otherT.AvailableStartTimeElement)) return false;
                if( !DeepComparable.IsExactly(AvailableEndTimeElement, otherT.AvailableEndTimeElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("HealthcareServiceNotAvailableTimeComponent")]
        [DataContract]
        public partial class HealthcareServiceNotAvailableTimeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HealthcareServiceNotAvailableTimeComponent"; } }
            
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
            [FhirElement("startDate", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime StartDateElement
            {
                get { return _StartDateElement; }
                set { _StartDateElement = value; OnPropertyChanged("StartDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _StartDateElement;
            
            /// <summary>
            /// Service is not available (seasonally or for a public holiday) from this date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string StartDate
            {
                get { return StartDateElement != null ? StartDateElement.Value : null; }
                set
                {
                    if(value == null)
                      StartDateElement = null; 
                    else
                      StartDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("StartDate");
                }
            }
            
            /// <summary>
            /// Service is not available (seasonally or for a public holiday) until this date
            /// </summary>
            [FhirElement("endDate", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime EndDateElement
            {
                get { return _EndDateElement; }
                set { _EndDateElement = value; OnPropertyChanged("EndDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _EndDateElement;
            
            /// <summary>
            /// Service is not available (seasonally or for a public holiday) until this date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string EndDate
            {
                get { return EndDateElement != null ? EndDateElement.Value : null; }
                set
                {
                    if(value == null)
                      EndDateElement = null; 
                    else
                      EndDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("EndDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HealthcareServiceNotAvailableTimeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(StartDateElement != null) dest.StartDateElement = (Hl7.Fhir.Model.FhirDateTime)StartDateElement.DeepCopy();
                    if(EndDateElement != null) dest.EndDateElement = (Hl7.Fhir.Model.FhirDateTime)EndDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HealthcareServiceNotAvailableTimeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HealthcareServiceNotAvailableTimeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(StartDateElement, otherT.StartDateElement)) return false;
                if( !DeepComparable.Matches(EndDateElement, otherT.EndDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HealthcareServiceNotAvailableTimeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(StartDateElement, otherT.StartDateElement)) return false;
                if( !DeepComparable.IsExactly(EndDateElement, otherT.EndDateElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// The location where this healthcare service may be provided
        /// </summary>
        [FhirElement("location", Order=100)]
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
        [FhirElement("serviceCategory", Order=110)]
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
        [FhirElement("serviceType", Order=120)]
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
        [FhirElement("serviceName", Order=130)]
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
        /// Additional description of the  or any specific issues not covered by the other attributes, which can be displayed as further detail under the serviceName
        /// </summary>
        [FhirElement("comment", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Additional description of the  or any specific issues not covered by the other attributes, which can be displayed as further detail under the serviceName
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
        [FhirElement("extraDetails", Order=150)]
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
        /// The free provision code provides a link to the Free Provision reference entity to enable the selection of one free provision type
        /// </summary>
        [FhirElement("freeProvisionCode", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FreeProvisionCode
        {
            get { return _FreeProvisionCode; }
            set { _FreeProvisionCode = value; OnPropertyChanged("FreeProvisionCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FreeProvisionCode;
        
        /// <summary>
        /// Does this service have specific eligibility requirements that need to be met in order to use the service
        /// </summary>
        [FhirElement("eligibility", Order=170)]
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
        [FhirElement("eligibilityNote", Order=180)]
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
        /// Indicates whether or not a prospective consumer will require an appointment for a particular service at a Site to be provided by the Organization. Indicates if an appointment is required for access to this service. If this flag is 'NotDefined', then this flag is overridden by the Site's availability flag. (ConditionalIndicator Enum)
        /// </summary>
        [FhirElement("appointmentRequired", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AppointmentRequired
        {
            get { return _AppointmentRequired; }
            set { _AppointmentRequired = value; OnPropertyChanged("AppointmentRequired"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AppointmentRequired;
        
        /// <summary>
        /// If there is an image associated with this Service Site, its URI can be included here
        /// </summary>
        [FhirElement("imageURI", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ImageURIElement
        {
            get { return _ImageURIElement; }
            set { _ImageURIElement = value; OnPropertyChanged("ImageURIElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ImageURIElement;
        
        /// <summary>
        /// If there is an image associated with this Service Site, its URI can be included here
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ImageURI
        {
            get { return ImageURIElement != null ? ImageURIElement.Value : null; }
            set
            {
                if(value == null)
                  ImageURIElement = null; 
                else
                  ImageURIElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ImageURI");
            }
        }
        
        /// <summary>
        /// A Collection of times that the Service Site is available
        /// </summary>
        [FhirElement("availableTime", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent> AvailableTime
        {
            get { if(_AvailableTime==null) _AvailableTime = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent>(); return _AvailableTime; }
            set { _AvailableTime = value; OnPropertyChanged("AvailableTime"); }
        }
        
        private List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent> _AvailableTime;
        
        /// <summary>
        /// Not avail times - need better description
        /// </summary>
        [FhirElement("notAvailableTime", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableTimeComponent> NotAvailableTime
        {
            get { if(_NotAvailableTime==null) _NotAvailableTime = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableTimeComponent>(); return _NotAvailableTime; }
            set { _NotAvailableTime = value; OnPropertyChanged("NotAvailableTime"); }
        }
        
        private List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableTimeComponent> _NotAvailableTime;
        
        /// <summary>
        /// A description of Site availability exceptions, e.g., public holiday availability. Succinctly describing all possible exceptions to normal Site availability as details in the Available Times and Not Available Times
        /// </summary>
        [FhirElement("availabilityExceptions", Order=230)]
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
        
        /// <summary>
        /// The public part of the 'keys' allocated to an Organization by an accredited body to support secure exchange of data over the internet. To be provided by the Organization, where available
        /// </summary>
        [FhirElement("publicKey", Order=240)]
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
        /// Program Names that can be used to categorize the service
        /// </summary>
        [FhirElement("programName", Order=250)]
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
        /// List of contacts related to this specific healthcare service. If this is empty, then refer to the location's contacts
        /// </summary>
        [FhirElement("contactPoint", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> ContactPoint
        {
            get { if(_ContactPoint==null) _ContactPoint = new List<Hl7.Fhir.Model.ContactPoint>(); return _ContactPoint; }
            set { _ContactPoint = value; OnPropertyChanged("ContactPoint"); }
        }
        
        private List<Hl7.Fhir.Model.ContactPoint> _ContactPoint;
        
        /// <summary>
        /// Collection of Characteristics (attributes)
        /// </summary>
        [FhirElement("characteristic", Order=270)]
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
        [FhirElement("referralMethod", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReferralMethod
        {
            get { if(_ReferralMethod==null) _ReferralMethod = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReferralMethod; }
            set { _ReferralMethod = value; OnPropertyChanged("ReferralMethod"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReferralMethod;
        
        /// <summary>
        /// The setting where this service can be provided, such is in home, or at location in organisation
        /// </summary>
        [FhirElement("setting", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Setting
        {
            get { if(_Setting==null) _Setting = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Setting; }
            set { _Setting = value; OnPropertyChanged("Setting"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Setting;
        
        /// <summary>
        /// Collection of Target Groups for the Service Site (The target audience that this service is for)
        /// </summary>
        [FhirElement("targetGroup", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> TargetGroup
        {
            get { if(_TargetGroup==null) _TargetGroup = new List<Hl7.Fhir.Model.CodeableConcept>(); return _TargetGroup; }
            set { _TargetGroup = value; OnPropertyChanged("TargetGroup"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _TargetGroup;
        
        /// <summary>
        /// Need better description
        /// </summary>
        [FhirElement("coverageArea", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> CoverageArea
        {
            get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CoverageArea; }
            set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _CoverageArea;
        
        /// <summary>
        /// Need better description
        /// </summary>
        [FhirElement("catchmentArea", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> CatchmentArea
        {
            get { if(_CatchmentArea==null) _CatchmentArea = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CatchmentArea; }
            set { _CatchmentArea = value; OnPropertyChanged("CatchmentArea"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _CatchmentArea;
        
        /// <summary>
        /// List of the specific
        /// </summary>
        [FhirElement("serviceCode", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceCode
        {
            get { if(_ServiceCode==null) _ServiceCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceCode; }
            set { _ServiceCode = value; OnPropertyChanged("ServiceCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceCode;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as HealthcareService;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ServiceCategory != null) dest.ServiceCategory = (Hl7.Fhir.Model.CodeableConcept)ServiceCategory.DeepCopy();
                if(ServiceType != null) dest.ServiceType = new List<Hl7.Fhir.Model.HealthcareService.ServiceTypeComponent>(ServiceType.DeepCopy());
                if(ServiceNameElement != null) dest.ServiceNameElement = (Hl7.Fhir.Model.FhirString)ServiceNameElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(ExtraDetailsElement != null) dest.ExtraDetailsElement = (Hl7.Fhir.Model.FhirString)ExtraDetailsElement.DeepCopy();
                if(FreeProvisionCode != null) dest.FreeProvisionCode = (Hl7.Fhir.Model.CodeableConcept)FreeProvisionCode.DeepCopy();
                if(Eligibility != null) dest.Eligibility = (Hl7.Fhir.Model.CodeableConcept)Eligibility.DeepCopy();
                if(EligibilityNoteElement != null) dest.EligibilityNoteElement = (Hl7.Fhir.Model.FhirString)EligibilityNoteElement.DeepCopy();
                if(AppointmentRequired != null) dest.AppointmentRequired = (Hl7.Fhir.Model.CodeableConcept)AppointmentRequired.DeepCopy();
                if(ImageURIElement != null) dest.ImageURIElement = (Hl7.Fhir.Model.FhirUri)ImageURIElement.DeepCopy();
                if(AvailableTime != null) dest.AvailableTime = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceAvailableTimeComponent>(AvailableTime.DeepCopy());
                if(NotAvailableTime != null) dest.NotAvailableTime = new List<Hl7.Fhir.Model.HealthcareService.HealthcareServiceNotAvailableTimeComponent>(NotAvailableTime.DeepCopy());
                if(AvailabilityExceptionsElement != null) dest.AvailabilityExceptionsElement = (Hl7.Fhir.Model.FhirString)AvailabilityExceptionsElement.DeepCopy();
                if(PublicKeyElement != null) dest.PublicKeyElement = (Hl7.Fhir.Model.FhirString)PublicKeyElement.DeepCopy();
                if(ProgramNameElement != null) dest.ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(ProgramNameElement.DeepCopy());
                if(ContactPoint != null) dest.ContactPoint = new List<Hl7.Fhir.Model.ContactPoint>(ContactPoint.DeepCopy());
                if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.CodeableConcept>(Characteristic.DeepCopy());
                if(ReferralMethod != null) dest.ReferralMethod = new List<Hl7.Fhir.Model.CodeableConcept>(ReferralMethod.DeepCopy());
                if(Setting != null) dest.Setting = new List<Hl7.Fhir.Model.CodeableConcept>(Setting.DeepCopy());
                if(TargetGroup != null) dest.TargetGroup = new List<Hl7.Fhir.Model.CodeableConcept>(TargetGroup.DeepCopy());
                if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.CodeableConcept>(CoverageArea.DeepCopy());
                if(CatchmentArea != null) dest.CatchmentArea = new List<Hl7.Fhir.Model.CodeableConcept>(CatchmentArea.DeepCopy());
                if(ServiceCode != null) dest.ServiceCode = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceCode.DeepCopy());
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
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.Matches(ServiceNameElement, otherT.ServiceNameElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(ExtraDetailsElement, otherT.ExtraDetailsElement)) return false;
            if( !DeepComparable.Matches(FreeProvisionCode, otherT.FreeProvisionCode)) return false;
            if( !DeepComparable.Matches(Eligibility, otherT.Eligibility)) return false;
            if( !DeepComparable.Matches(EligibilityNoteElement, otherT.EligibilityNoteElement)) return false;
            if( !DeepComparable.Matches(AppointmentRequired, otherT.AppointmentRequired)) return false;
            if( !DeepComparable.Matches(ImageURIElement, otherT.ImageURIElement)) return false;
            if( !DeepComparable.Matches(AvailableTime, otherT.AvailableTime)) return false;
            if( !DeepComparable.Matches(NotAvailableTime, otherT.NotAvailableTime)) return false;
            if( !DeepComparable.Matches(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
            if( !DeepComparable.Matches(PublicKeyElement, otherT.PublicKeyElement)) return false;
            if( !DeepComparable.Matches(ProgramNameElement, otherT.ProgramNameElement)) return false;
            if( !DeepComparable.Matches(ContactPoint, otherT.ContactPoint)) return false;
            if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.Matches(ReferralMethod, otherT.ReferralMethod)) return false;
            if( !DeepComparable.Matches(Setting, otherT.Setting)) return false;
            if( !DeepComparable.Matches(TargetGroup, otherT.TargetGroup)) return false;
            if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.Matches(CatchmentArea, otherT.CatchmentArea)) return false;
            if( !DeepComparable.Matches(ServiceCode, otherT.ServiceCode)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as HealthcareService;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.IsExactly(ServiceNameElement, otherT.ServiceNameElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(ExtraDetailsElement, otherT.ExtraDetailsElement)) return false;
            if( !DeepComparable.IsExactly(FreeProvisionCode, otherT.FreeProvisionCode)) return false;
            if( !DeepComparable.IsExactly(Eligibility, otherT.Eligibility)) return false;
            if( !DeepComparable.IsExactly(EligibilityNoteElement, otherT.EligibilityNoteElement)) return false;
            if( !DeepComparable.IsExactly(AppointmentRequired, otherT.AppointmentRequired)) return false;
            if( !DeepComparable.IsExactly(ImageURIElement, otherT.ImageURIElement)) return false;
            if( !DeepComparable.IsExactly(AvailableTime, otherT.AvailableTime)) return false;
            if( !DeepComparable.IsExactly(NotAvailableTime, otherT.NotAvailableTime)) return false;
            if( !DeepComparable.IsExactly(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
            if( !DeepComparable.IsExactly(PublicKeyElement, otherT.PublicKeyElement)) return false;
            if( !DeepComparable.IsExactly(ProgramNameElement, otherT.ProgramNameElement)) return false;
            if( !DeepComparable.IsExactly(ContactPoint, otherT.ContactPoint)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.IsExactly(ReferralMethod, otherT.ReferralMethod)) return false;
            if( !DeepComparable.IsExactly(Setting, otherT.Setting)) return false;
            if( !DeepComparable.IsExactly(TargetGroup, otherT.TargetGroup)) return false;
            if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.IsExactly(CatchmentArea, otherT.CatchmentArea)) return false;
            if( !DeepComparable.IsExactly(ServiceCode, otherT.ServiceCode)) return false;
            
            return true;
        }
        
    }
    
}
