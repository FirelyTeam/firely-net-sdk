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
    /// The definition of a specific activity to be taken, independent of any particular patient or context
    /// </summary>
    [FhirType("ActivityDefinition", IsResource=true)]
    [DataContract]
    public partial class ActivityDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ActivityDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ActivityDefinition"; } }
        
        /// <summary>
        /// A list of all the request resource types defined in this version of the FHIR specification.
        /// (url: http://hl7.org/fhir/ValueSet/request-resource-types)
        /// </summary>
        [FhirEnumeration("RequestResourceType")]
        public enum RequestResourceType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("Appointment", "http://hl7.org/fhir/request-resource-types"), Description("Appointment")]
            Appointment,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("AppointmentResponse", "http://hl7.org/fhir/request-resource-types"), Description("AppointmentResponse")]
            AppointmentResponse,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("CarePlan", "http://hl7.org/fhir/request-resource-types"), Description("CarePlan")]
            CarePlan,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("Claim", "http://hl7.org/fhir/request-resource-types"), Description("Claim")]
            Claim,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("CommunicationRequest", "http://hl7.org/fhir/request-resource-types"), Description("CommunicationRequest")]
            CommunicationRequest,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("Contract", "http://hl7.org/fhir/request-resource-types"), Description("Contract")]
            Contract,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("DeviceRequest", "http://hl7.org/fhir/request-resource-types"), Description("DeviceRequest")]
            DeviceRequest,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("EnrollmentRequest", "http://hl7.org/fhir/request-resource-types"), Description("EnrollmentRequest")]
            EnrollmentRequest,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("ImmunizationRecommendation", "http://hl7.org/fhir/request-resource-types"), Description("ImmunizationRecommendation")]
            ImmunizationRecommendation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("MedicationRequest", "http://hl7.org/fhir/request-resource-types"), Description("MedicationRequest")]
            MedicationRequest,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("NutritionOrder", "http://hl7.org/fhir/request-resource-types"), Description("NutritionOrder")]
            NutritionOrder,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("ServiceRequest", "http://hl7.org/fhir/request-resource-types"), Description("ServiceRequest")]
            ServiceRequest,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("SupplyRequest", "http://hl7.org/fhir/request-resource-types"), Description("SupplyRequest")]
            SupplyRequest,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("Task", "http://hl7.org/fhir/request-resource-types"), Description("Task")]
            Task,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/request-resource-types)
            /// </summary>
            [EnumLiteral("VisionPrescription", "http://hl7.org/fhir/request-resource-types"), Description("VisionPrescription")]
            VisionPrescription,
        }

        [FhirType("ParticipantComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// patient | practitioner | related-person | device
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionParticipantType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionParticipantType> _TypeElement;
            
            /// <summary>
            /// patient | practitioner | related-person | device
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionParticipantType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.ActionParticipantType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// E.g. Nurse, Surgeon, Parent, etc.
            /// </summary>
            [FhirElement("role", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ActionParticipantType>)TypeElement.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Role != null) yield return Role;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Role != null) yield return new ElementValue("role", Role);
                }
            }

            
        }
        
        
        [FhirType("DynamicValueComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class DynamicValueComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DynamicValueComponent"; } }
            
            /// <summary>
            /// The path to the element to be set dynamically
            /// </summary>
            [FhirElement("path", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// The path to the element to be set dynamically
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null; 
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// An expression that provides the dynamic value for the customization
            /// </summary>
            [FhirElement("expression", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Expression Expression
            {
                get { return _Expression; }
                set { _Expression = value; OnPropertyChanged("Expression"); }
            }
            
            private Expression _Expression;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DynamicValueComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(Expression != null) dest.Expression = (Expression)Expression.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DynamicValueComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DynamicValueComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(Expression, otherT.Expression)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DynamicValueComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(Expression, otherT.Expression)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (Expression != null) yield return Expression;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (Expression != null) yield return new ElementValue("expression", Expression);
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this activity definition, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this activity definition, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the activity definition
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Business version of the activity definition
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the activity definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this activity definition (computer friendly)
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
        /// Name for this activity definition (computer friendly)
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
        /// Name for this activity definition (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this activity definition (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// Subordinate title of the activity definition
        /// </summary>
        [FhirElement("subtitle", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SubtitleElement
        {
            get { return _SubtitleElement; }
            set { _SubtitleElement = value; OnPropertyChanged("SubtitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SubtitleElement;
        
        /// <summary>
        /// Subordinate title of the activity definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Subtitle
        {
            get { return SubtitleElement != null ? SubtitleElement.Value : null; }
            set
            {
                if (value == null)
                  SubtitleElement = null; 
                else
                  SubtitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Subtitle");
            }
        }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Type of individual the activity definition is intended for
        /// </summary>
        [FhirElement("subject", Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.Element _Subject;
        
        /// <summary>
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the activity definition
        /// </summary>
        [FhirElement("description", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for activity definition (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this activity definition is defined
        /// </summary>
        [FhirElement("purpose", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Purpose;
        
        /// <summary>
        /// Describes the clinical usage of the activity definition
        /// </summary>
        [FhirElement("usage", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UsageElement
        {
            get { return _UsageElement; }
            set { _UsageElement = value; OnPropertyChanged("UsageElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _UsageElement;
        
        /// <summary>
        /// Describes the clinical usage of the activity definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Usage
        {
            get { return UsageElement != null ? UsageElement.Value : null; }
            set
            {
                if (value == null)
                  UsageElement = null; 
                else
                  UsageElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Usage");
            }
        }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// When the activity definition was approved by publisher
        /// </summary>
        [FhirElement("approvalDate", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.Date ApprovalDateElement
        {
            get { return _ApprovalDateElement; }
            set { _ApprovalDateElement = value; OnPropertyChanged("ApprovalDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _ApprovalDateElement;
        
        /// <summary>
        /// When the activity definition was approved by publisher
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ApprovalDate
        {
            get { return ApprovalDateElement != null ? ApprovalDateElement.Value : null; }
            set
            {
                if (value == null)
                  ApprovalDateElement = null; 
                else
                  ApprovalDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("ApprovalDate");
            }
        }
        
        /// <summary>
        /// When the activity definition was last reviewed
        /// </summary>
        [FhirElement("lastReviewDate", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.Date LastReviewDateElement
        {
            get { return _LastReviewDateElement; }
            set { _LastReviewDateElement = value; OnPropertyChanged("LastReviewDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LastReviewDateElement;
        
        /// <summary>
        /// When the activity definition was last reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastReviewDate
        {
            get { return LastReviewDateElement != null ? LastReviewDateElement.Value : null; }
            set
            {
                if (value == null)
                  LastReviewDateElement = null; 
                else
                  LastReviewDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("LastReviewDate");
            }
        }
        
        /// <summary>
        /// When the activity definition is expected to be used
        /// </summary>
        [FhirElement("effectivePeriod", InSummary=true, Order=290)]
        [DataMember]
        public Hl7.Fhir.Model.Period EffectivePeriod
        {
            get { return _EffectivePeriod; }
            set { _EffectivePeriod = value; OnPropertyChanged("EffectivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _EffectivePeriod;
        
        /// <summary>
        /// E.g. Education, Treatment, Assessment, etc.
        /// </summary>
        [FhirElement("topic", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Topic
        {
            get { if(_Topic==null) _Topic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Topic;
        
        /// <summary>
        /// Who authored the content
        /// </summary>
        [FhirElement("author", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Author
        {
            get { if(_Author==null) _Author = new List<ContactDetail>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<ContactDetail> _Author;
        
        /// <summary>
        /// Who edited the content
        /// </summary>
        [FhirElement("editor", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Editor
        {
            get { if(_Editor==null) _Editor = new List<ContactDetail>(); return _Editor; }
            set { _Editor = value; OnPropertyChanged("Editor"); }
        }
        
        private List<ContactDetail> _Editor;
        
        /// <summary>
        /// Who reviewed the content
        /// </summary>
        [FhirElement("reviewer", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Reviewer
        {
            get { if(_Reviewer==null) _Reviewer = new List<ContactDetail>(); return _Reviewer; }
            set { _Reviewer = value; OnPropertyChanged("Reviewer"); }
        }
        
        private List<ContactDetail> _Reviewer;
        
        /// <summary>
        /// Who endorsed the content
        /// </summary>
        [FhirElement("endorser", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Endorser
        {
            get { if(_Endorser==null) _Endorser = new List<ContactDetail>(); return _Endorser; }
            set { _Endorser = value; OnPropertyChanged("Endorser"); }
        }
        
        private List<ContactDetail> _Endorser;
        
        /// <summary>
        /// Additional documentation, citations, etc.
        /// </summary>
        [FhirElement("relatedArtifact", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedArtifact> RelatedArtifact
        {
            get { if(_RelatedArtifact==null) _RelatedArtifact = new List<RelatedArtifact>(); return _RelatedArtifact; }
            set { _RelatedArtifact = value; OnPropertyChanged("RelatedArtifact"); }
        }
        
        private List<RelatedArtifact> _RelatedArtifact;
        
        /// <summary>
        /// Logic used by the activity definition
        /// </summary>
        [FhirElement("library", Order=360)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> LibraryElement
        {
            get { if(_LibraryElement==null) _LibraryElement = new List<Hl7.Fhir.Model.Canonical>(); return _LibraryElement; }
            set { _LibraryElement = value; OnPropertyChanged("LibraryElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _LibraryElement;
        
        /// <summary>
        /// Logic used by the activity definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Library
        {
            get { return LibraryElement != null ? LibraryElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  LibraryElement = null; 
                else
                  LibraryElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Library");
            }
        }
        
        /// <summary>
        /// Kind of resource
        /// </summary>
        [FhirElement("kind", InSummary=true, Order=370)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ActivityDefinition.RequestResourceType> KindElement
        {
            get { return _KindElement; }
            set { _KindElement = value; OnPropertyChanged("KindElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ActivityDefinition.RequestResourceType> _KindElement;
        
        /// <summary>
        /// Kind of resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ActivityDefinition.RequestResourceType? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  KindElement = null; 
                else
                  KindElement = new Code<Hl7.Fhir.Model.ActivityDefinition.RequestResourceType>(value);
                OnPropertyChanged("Kind");
            }
        }
        
        /// <summary>
        /// What profile the resource needs to conform to
        /// </summary>
        [FhirElement("profile", Order=380)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical ProfileElement
        {
            get { return _ProfileElement; }
            set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _ProfileElement;
        
        /// <summary>
        /// What profile the resource needs to conform to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Profile
        {
            get { return ProfileElement != null ? ProfileElement.Value : null; }
            set
            {
                if (value == null)
                  ProfileElement = null; 
                else
                  ProfileElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Profile");
            }
        }
        
        /// <summary>
        /// Detail type of activity
        /// </summary>
        [FhirElement("code", InSummary=true, Order=390)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// proposal | plan | order
        /// </summary>
        [FhirElement("intent", Order=400)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | order
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IntentElement = null; 
                else
                  IntentElement = new Code<Hl7.Fhir.Model.RequestIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", Order=410)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  PriorityElement = null; 
                else
                  PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// True if the activity should not be performed
        /// </summary>
        [FhirElement("doNotPerform", InSummary=true, Order=420)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean DoNotPerformElement
        {
            get { return _DoNotPerformElement; }
            set { _DoNotPerformElement = value; OnPropertyChanged("DoNotPerformElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _DoNotPerformElement;
        
        /// <summary>
        /// True if the activity should not be performed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? DoNotPerform
        {
            get { return DoNotPerformElement != null ? DoNotPerformElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  DoNotPerformElement = null; 
                else
                  DoNotPerformElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("DoNotPerform");
            }
        }
        
        /// <summary>
        /// When activity is to occur
        /// </summary>
        [FhirElement("timing", Order=430, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Duration))]
        [DataMember]
        public Hl7.Fhir.Model.Element Timing
        {
            get { return _Timing; }
            set { _Timing = value; OnPropertyChanged("Timing"); }
        }
        
        private Hl7.Fhir.Model.Element _Timing;
        
        /// <summary>
        /// Where it should happen
        /// </summary>
        [FhirElement("location", Order=440)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Who should participate in the action
        /// </summary>
        [FhirElement("participant", Order=450)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ActivityDefinition.ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.ActivityDefinition.ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.ActivityDefinition.ParticipantComponent> _Participant;
        
        /// <summary>
        /// What's administered/supplied
        /// </summary>
        [FhirElement("product", Order=460, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged("Product"); }
        }
        
        private Hl7.Fhir.Model.Element _Product;
        
        /// <summary>
        /// How much is administered/consumed/supplied
        /// </summary>
        [FhirElement("quantity", Order=470)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _Quantity;
        
        /// <summary>
        /// Detailed dosage instructions
        /// </summary>
        [FhirElement("dosage", Order=480)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Dosage> Dosage
        {
            get { if(_Dosage==null) _Dosage = new List<Dosage>(); return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private List<Dosage> _Dosage;
        
        /// <summary>
        /// What part of body to perform on
        /// </summary>
        [FhirElement("bodySite", Order=490)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// What specimens are required to perform this action
        /// </summary>
        [FhirElement("specimenRequirement", Order=500)]
        [CLSCompliant(false)]
		[References("SpecimenDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SpecimenRequirement
        {
            get { if(_SpecimenRequirement==null) _SpecimenRequirement = new List<Hl7.Fhir.Model.ResourceReference>(); return _SpecimenRequirement; }
            set { _SpecimenRequirement = value; OnPropertyChanged("SpecimenRequirement"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SpecimenRequirement;
        
        /// <summary>
        /// What observations are required to perform this action
        /// </summary>
        [FhirElement("observationRequirement", Order=510)]
        [CLSCompliant(false)]
		[References("ObservationDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ObservationRequirement
        {
            get { if(_ObservationRequirement==null) _ObservationRequirement = new List<Hl7.Fhir.Model.ResourceReference>(); return _ObservationRequirement; }
            set { _ObservationRequirement = value; OnPropertyChanged("ObservationRequirement"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ObservationRequirement;
        
        /// <summary>
        /// What observations must be produced by this action
        /// </summary>
        [FhirElement("observationResultRequirement", Order=520)]
        [CLSCompliant(false)]
		[References("ObservationDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ObservationResultRequirement
        {
            get { if(_ObservationResultRequirement==null) _ObservationResultRequirement = new List<Hl7.Fhir.Model.ResourceReference>(); return _ObservationResultRequirement; }
            set { _ObservationResultRequirement = value; OnPropertyChanged("ObservationResultRequirement"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ObservationResultRequirement;
        
        /// <summary>
        /// Transform to apply the template
        /// </summary>
        [FhirElement("transform", Order=530)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical TransformElement
        {
            get { return _TransformElement; }
            set { _TransformElement = value; OnPropertyChanged("TransformElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _TransformElement;
        
        /// <summary>
        /// Transform to apply the template
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Transform
        {
            get { return TransformElement != null ? TransformElement.Value : null; }
            set
            {
                if (value == null)
                  TransformElement = null; 
                else
                  TransformElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Transform");
            }
        }
        
        /// <summary>
        /// Dynamic aspects of the definition
        /// </summary>
        [FhirElement("dynamicValue", Order=540)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ActivityDefinition.DynamicValueComponent> DynamicValue
        {
            get { if(_DynamicValue==null) _DynamicValue = new List<Hl7.Fhir.Model.ActivityDefinition.DynamicValueComponent>(); return _DynamicValue; }
            set { _DynamicValue = value; OnPropertyChanged("DynamicValue"); }
        }
        
        private List<Hl7.Fhir.Model.ActivityDefinition.DynamicValueComponent> _DynamicValue;
        

        public static ElementDefinition.ConstraintComponent ActivityDefinition_ADF_0 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "adf-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(ActivityDefinition_ADF_0);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ActivityDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(SubtitleElement != null) dest.SubtitleElement = (Hl7.Fhir.Model.FhirString)SubtitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.Element)Subject.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(Topic != null) dest.Topic = new List<Hl7.Fhir.Model.CodeableConcept>(Topic.DeepCopy());
                if(Author != null) dest.Author = new List<ContactDetail>(Author.DeepCopy());
                if(Editor != null) dest.Editor = new List<ContactDetail>(Editor.DeepCopy());
                if(Reviewer != null) dest.Reviewer = new List<ContactDetail>(Reviewer.DeepCopy());
                if(Endorser != null) dest.Endorser = new List<ContactDetail>(Endorser.DeepCopy());
                if(RelatedArtifact != null) dest.RelatedArtifact = new List<RelatedArtifact>(RelatedArtifact.DeepCopy());
                if(LibraryElement != null) dest.LibraryElement = new List<Hl7.Fhir.Model.Canonical>(LibraryElement.DeepCopy());
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.ActivityDefinition.RequestResourceType>)KindElement.DeepCopy();
                if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.RequestIntent>)IntentElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(DoNotPerformElement != null) dest.DoNotPerformElement = (Hl7.Fhir.Model.FhirBoolean)DoNotPerformElement.DeepCopy();
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.ActivityDefinition.ParticipantComponent>(Participant.DeepCopy());
                if(Product != null) dest.Product = (Hl7.Fhir.Model.Element)Product.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                if(Dosage != null) dest.Dosage = new List<Dosage>(Dosage.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(SpecimenRequirement != null) dest.SpecimenRequirement = new List<Hl7.Fhir.Model.ResourceReference>(SpecimenRequirement.DeepCopy());
                if(ObservationRequirement != null) dest.ObservationRequirement = new List<Hl7.Fhir.Model.ResourceReference>(ObservationRequirement.DeepCopy());
                if(ObservationResultRequirement != null) dest.ObservationResultRequirement = new List<Hl7.Fhir.Model.ResourceReference>(ObservationResultRequirement.DeepCopy());
                if(TransformElement != null) dest.TransformElement = (Hl7.Fhir.Model.Canonical)TransformElement.DeepCopy();
                if(DynamicValue != null) dest.DynamicValue = new List<Hl7.Fhir.Model.ActivityDefinition.DynamicValueComponent>(DynamicValue.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ActivityDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ActivityDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(SubtitleElement, otherT.SubtitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Editor, otherT.Editor)) return false;
            if( !DeepComparable.Matches(Reviewer, otherT.Reviewer)) return false;
            if( !DeepComparable.Matches(Endorser, otherT.Endorser)) return false;
            if( !DeepComparable.Matches(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.Matches(LibraryElement, otherT.LibraryElement)) return false;
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
            if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Product, otherT.Product)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(SpecimenRequirement, otherT.SpecimenRequirement)) return false;
            if( !DeepComparable.Matches(ObservationRequirement, otherT.ObservationRequirement)) return false;
            if( !DeepComparable.Matches(ObservationResultRequirement, otherT.ObservationResultRequirement)) return false;
            if( !DeepComparable.Matches(TransformElement, otherT.TransformElement)) return false;
            if( !DeepComparable.Matches(DynamicValue, otherT.DynamicValue)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ActivityDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(SubtitleElement, otherT.SubtitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Editor, otherT.Editor)) return false;
            if( !DeepComparable.IsExactly(Reviewer, otherT.Reviewer)) return false;
            if( !DeepComparable.IsExactly(Endorser, otherT.Endorser)) return false;
            if( !DeepComparable.IsExactly(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.IsExactly(LibraryElement, otherT.LibraryElement)) return false;
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
            if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(SpecimenRequirement, otherT.SpecimenRequirement)) return false;
            if( !DeepComparable.IsExactly(ObservationRequirement, otherT.ObservationRequirement)) return false;
            if( !DeepComparable.IsExactly(ObservationResultRequirement, otherT.ObservationResultRequirement)) return false;
            if( !DeepComparable.IsExactly(TransformElement, otherT.TransformElement)) return false;
            if( !DeepComparable.IsExactly(DynamicValue, otherT.DynamicValue)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
				if (SubtitleElement != null) yield return SubtitleElement;
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (Subject != null) yield return Subject;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (Description != null) yield return Description;
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Purpose != null) yield return Purpose;
				if (UsageElement != null) yield return UsageElement;
				if (Copyright != null) yield return Copyright;
				if (ApprovalDateElement != null) yield return ApprovalDateElement;
				if (LastReviewDateElement != null) yield return LastReviewDateElement;
				if (EffectivePeriod != null) yield return EffectivePeriod;
				foreach (var elem in Topic) { if (elem != null) yield return elem; }
				foreach (var elem in Author) { if (elem != null) yield return elem; }
				foreach (var elem in Editor) { if (elem != null) yield return elem; }
				foreach (var elem in Reviewer) { if (elem != null) yield return elem; }
				foreach (var elem in Endorser) { if (elem != null) yield return elem; }
				foreach (var elem in RelatedArtifact) { if (elem != null) yield return elem; }
				foreach (var elem in LibraryElement) { if (elem != null) yield return elem; }
				if (KindElement != null) yield return KindElement;
				if (ProfileElement != null) yield return ProfileElement;
				if (Code != null) yield return Code;
				if (IntentElement != null) yield return IntentElement;
				if (PriorityElement != null) yield return PriorityElement;
				if (DoNotPerformElement != null) yield return DoNotPerformElement;
				if (Timing != null) yield return Timing;
				if (Location != null) yield return Location;
				foreach (var elem in Participant) { if (elem != null) yield return elem; }
				if (Product != null) yield return Product;
				if (Quantity != null) yield return Quantity;
				foreach (var elem in Dosage) { if (elem != null) yield return elem; }
				foreach (var elem in BodySite) { if (elem != null) yield return elem; }
				foreach (var elem in SpecimenRequirement) { if (elem != null) yield return elem; }
				foreach (var elem in ObservationRequirement) { if (elem != null) yield return elem; }
				foreach (var elem in ObservationResultRequirement) { if (elem != null) yield return elem; }
				if (TransformElement != null) yield return TransformElement;
				foreach (var elem in DynamicValue) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (SubtitleElement != null) yield return new ElementValue("subtitle", SubtitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (UsageElement != null) yield return new ElementValue("usage", UsageElement);
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (ApprovalDateElement != null) yield return new ElementValue("approvalDate", ApprovalDateElement);
                if (LastReviewDateElement != null) yield return new ElementValue("lastReviewDate", LastReviewDateElement);
                if (EffectivePeriod != null) yield return new ElementValue("effectivePeriod", EffectivePeriod);
                foreach (var elem in Topic) { if (elem != null) yield return new ElementValue("topic", elem); }
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
                foreach (var elem in Editor) { if (elem != null) yield return new ElementValue("editor", elem); }
                foreach (var elem in Reviewer) { if (elem != null) yield return new ElementValue("reviewer", elem); }
                foreach (var elem in Endorser) { if (elem != null) yield return new ElementValue("endorser", elem); }
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return new ElementValue("relatedArtifact", elem); }
                foreach (var elem in LibraryElement) { if (elem != null) yield return new ElementValue("library", elem); }
                if (KindElement != null) yield return new ElementValue("kind", KindElement);
                if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                if (Code != null) yield return new ElementValue("code", Code);
                if (IntentElement != null) yield return new ElementValue("intent", IntentElement);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (DoNotPerformElement != null) yield return new ElementValue("doNotPerform", DoNotPerformElement);
                if (Timing != null) yield return new ElementValue("timing", Timing);
                if (Location != null) yield return new ElementValue("location", Location);
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                if (Product != null) yield return new ElementValue("product", Product);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                foreach (var elem in Dosage) { if (elem != null) yield return new ElementValue("dosage", elem); }
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                foreach (var elem in SpecimenRequirement) { if (elem != null) yield return new ElementValue("specimenRequirement", elem); }
                foreach (var elem in ObservationRequirement) { if (elem != null) yield return new ElementValue("observationRequirement", elem); }
                foreach (var elem in ObservationResultRequirement) { if (elem != null) yield return new ElementValue("observationResultRequirement", elem); }
                if (TransformElement != null) yield return new ElementValue("transform", TransformElement);
                foreach (var elem in DynamicValue) { if (elem != null) yield return new ElementValue("dynamicValue", elem); }
            }
        }

    }
    
}
