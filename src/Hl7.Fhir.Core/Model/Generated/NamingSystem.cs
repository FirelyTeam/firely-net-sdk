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
// Generated on Fri, Dec 5, 2014 10:08+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// System of unique identification
    /// </summary>
    [FhirType("NamingSystem", IsResource=true)]
    [DataContract]
    public partial class NamingSystem : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        public override ResourceType ResourceType { get { return ResourceType.NamingSystem; } }
        public override string TypeName { get { return "NamingSystem"; } }
        
        /// <summary>
        /// Indicates whether the namingsystem should be used
        /// </summary>
        [FhirEnumeration("NamingSystemStatus")]
        public enum NamingSystemStatus
        {
            /// <summary>
            /// System has been submitted but not yet approved.
            /// </summary>
            [EnumLiteral("proposed")]
            Proposed,
            /// <summary>
            /// System is valid for use.
            /// </summary>
            [EnumLiteral("active")]
            Active,
            /// <summary>
            /// System should no longer be used.
            /// </summary>
            [EnumLiteral("retired")]
            Retired,
        }
        
        /// <summary>
        /// Identifies the purpose of the namingsystem
        /// </summary>
        [FhirEnumeration("NamingSystemType")]
        public enum NamingSystemType
        {
            /// <summary>
            /// The namingsystem is used to define concepts and symbols to represent those concepts.  E.g. UCUM, LOINC, NDC code, local lab codes, etc.
            /// </summary>
            [EnumLiteral("codesystem")]
            Codesystem,
            /// <summary>
            /// The namingsystem is used to manage identifiers (e.g. license numbers, order numbers, etc.).
            /// </summary>
            [EnumLiteral("identifier")]
            Identifier,
            /// <summary>
            /// The namingsystem is used as the root for other identifiers and namingsystems.
            /// </summary>
            [EnumLiteral("root")]
            Root,
        }
        
        /// <summary>
        /// Identifies the style of unique identifier used to identify a namepace
        /// </summary>
        [FhirEnumeration("NamingSystemIdentifierType")]
        public enum NamingSystemIdentifierType
        {
            /// <summary>
            /// An ISO object identifier.  E.g. 1.2.3.4.5.
            /// </summary>
            [EnumLiteral("oid")]
            Oid,
            /// <summary>
            /// A universally unique identifier of the form a5afddf4-e880-459b-876e-e4591b0acc11.
            /// </summary>
            [EnumLiteral("uuid")]
            Uuid,
            /// <summary>
            /// A uniform resource identifier (ideally a URL - uniform resource locator).  E.g. http://unitsofmeasure.org.
            /// </summary>
            [EnumLiteral("uri")]
            Uri,
            /// <summary>
            /// Some other type of unique identifier.  E.g HL7-assigned reserved string such as LN for LOINC.
            /// </summary>
            [EnumLiteral("other")]
            Other,
        }
        
        [FhirType("NamingSystemContactComponent")]
        [DataContract]
        public partial class NamingSystemContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            public override string TypeName { get { return "NamingSystemContactComponent"; } }
            
            /// <summary>
            /// Name of person
            /// </summary>
            [FhirElement("name", InSummary=true, Order=20)]
            [DataMember]
            public Hl7.Fhir.Model.HumanName Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            private Hl7.Fhir.Model.HumanName _Name;
            
            /// <summary>
            /// Phone, email, etc.
            /// </summary>
            [FhirElement("telecom", InSummary=true, Order=30)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NamingSystemContactComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.HumanName)Name.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NamingSystemContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NamingSystemContactComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NamingSystemContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("NamingSystemUniqueIdComponent")]
        [DataContract]
        public partial class NamingSystemUniqueIdComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            public override string TypeName { get { return "NamingSystemUniqueIdComponent"; } }
            
            /// <summary>
            /// oid | uuid | uri | other
            /// </summary>
            [FhirElement("type", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.NamingSystem.NamingSystemIdentifierType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.NamingSystem.NamingSystemIdentifierType> _TypeElement;
            
            /// <summary>
            /// oid | uuid | uri | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.NamingSystem.NamingSystemIdentifierType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.NamingSystem.NamingSystemIdentifierType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The unique identifier
            /// </summary>
            [FhirElement("value", InSummary=true, Order=30)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The unique identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if(value == null)
                      ValueElement = null; 
                    else
                      ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// Is this the id that should be used for this type
            /// </summary>
            [FhirElement("preferred", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean PreferredElement
            {
                get { return _PreferredElement; }
                set { _PreferredElement = value; OnPropertyChanged("PreferredElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _PreferredElement;
            
            /// <summary>
            /// Is this the id that should be used for this type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Preferred
            {
                get { return PreferredElement != null ? PreferredElement.Value : null; }
                set
                {
                    if(value == null)
                      PreferredElement = null; 
                    else
                      PreferredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Preferred");
                }
            }
            
            /// <summary>
            /// When is identifier valid?
            /// </summary>
            [FhirElement("period", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            private Hl7.Fhir.Model.Period _Period;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NamingSystemUniqueIdComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.NamingSystem.NamingSystemIdentifierType>)TypeElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(PreferredElement != null) dest.PreferredElement = (Hl7.Fhir.Model.FhirBoolean)PreferredElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NamingSystemUniqueIdComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NamingSystemUniqueIdComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(PreferredElement, otherT.PreferredElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NamingSystemUniqueIdComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(PreferredElement, otherT.PreferredElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// codesystem | identifier | root
        /// </summary>
        [FhirElement("type", Order=50)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.NamingSystem.NamingSystemType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        private Code<Hl7.Fhir.Model.NamingSystem.NamingSystemType> _TypeElement;
        
        /// <summary>
        /// codesystem | identifier | root
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.NamingSystem.NamingSystemType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.NamingSystem.NamingSystemType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        [FhirElement("name", Order=60)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// proposed | active | retired
        /// </summary>
        [FhirElement("status", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.NamingSystem.NamingSystemStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.NamingSystem.NamingSystemStatus> _StatusElement;
        
        /// <summary>
        /// proposed | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.NamingSystem.NamingSystemStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.NamingSystem.NamingSystemStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// ISO 3-char country code
        /// </summary>
        [FhirElement("country", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Code CountryElement
        {
            get { return _CountryElement; }
            set { _CountryElement = value; OnPropertyChanged("CountryElement"); }
        }
        private Hl7.Fhir.Model.Code _CountryElement;
        
        /// <summary>
        /// ISO 3-char country code
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Country
        {
            get { return CountryElement != null ? CountryElement.Value : null; }
            set
            {
                if(value == null)
                  CountryElement = null; 
                else
                  CountryElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Country");
            }
        }
        
        /// <summary>
        /// driver | provider | patient | bank
        /// </summary>
        [FhirElement("category", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// Who maintains system namespace?
        /// </summary>
        [FhirElement("responsible", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ResponsibleElement
        {
            get { return _ResponsibleElement; }
            set { _ResponsibleElement = value; OnPropertyChanged("ResponsibleElement"); }
        }
        private Hl7.Fhir.Model.FhirString _ResponsibleElement;
        
        /// <summary>
        /// Who maintains system namespace?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Responsible
        {
            get { return ResponsibleElement != null ? ResponsibleElement.Value : null; }
            set
            {
                if(value == null)
                  ResponsibleElement = null; 
                else
                  ResponsibleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Responsible");
            }
        }
        
        /// <summary>
        /// What does namingsystem identify?
        /// </summary>
        [FhirElement("description", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// What does namingsystem identify?
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
        /// How/where is it used
        /// </summary>
        [FhirElement("usage", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UsageElement
        {
            get { return _UsageElement; }
            set { _UsageElement = value; OnPropertyChanged("UsageElement"); }
        }
        private Hl7.Fhir.Model.FhirString _UsageElement;
        
        /// <summary>
        /// How/where is it used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Usage
        {
            get { return UsageElement != null ? UsageElement.Value : null; }
            set
            {
                if(value == null)
                  UsageElement = null; 
                else
                  UsageElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Usage");
            }
        }
        
        /// <summary>
        /// Unique identifiers used for system
        /// </summary>
        [FhirElement("uniqueId", Order=130)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.NamingSystem.NamingSystemUniqueIdComponent> UniqueId
        {
            get { if(_UniqueId==null) _UniqueId = new List<Hl7.Fhir.Model.NamingSystem.NamingSystemUniqueIdComponent>(); return _UniqueId; }
            set { _UniqueId = value; OnPropertyChanged("UniqueId"); }
        }
        private List<Hl7.Fhir.Model.NamingSystem.NamingSystemUniqueIdComponent> _UniqueId;
        
        /// <summary>
        /// Who should be contacted for questions about namingsystem
        /// </summary>
        [FhirElement("contact", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.NamingSystem.NamingSystemContactComponent Contact
        {
            get { return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        private Hl7.Fhir.Model.NamingSystem.NamingSystemContactComponent _Contact;
        
        /// <summary>
        /// Use this instead
        /// </summary>
        [FhirElement("replacedBy", Order=150)]
        [References("NamingSystem")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ReplacedBy
        {
            get { return _ReplacedBy; }
            set { _ReplacedBy = value; OnPropertyChanged("ReplacedBy"); }
        }
        private Hl7.Fhir.Model.ResourceReference _ReplacedBy;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as NamingSystem;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.NamingSystem.NamingSystemType>)TypeElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.NamingSystem.NamingSystemStatus>)StatusElement.DeepCopy();
                if(CountryElement != null) dest.CountryElement = (Hl7.Fhir.Model.Code)CountryElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(ResponsibleElement != null) dest.ResponsibleElement = (Hl7.Fhir.Model.FhirString)ResponsibleElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(UniqueId != null) dest.UniqueId = new List<Hl7.Fhir.Model.NamingSystem.NamingSystemUniqueIdComponent>(UniqueId.DeepCopy());
                if(Contact != null) dest.Contact = (Hl7.Fhir.Model.NamingSystem.NamingSystemContactComponent)Contact.DeepCopy();
                if(ReplacedBy != null) dest.ReplacedBy = (Hl7.Fhir.Model.ResourceReference)ReplacedBy.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new NamingSystem());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as NamingSystem;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(CountryElement, otherT.CountryElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(ResponsibleElement, otherT.ResponsibleElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.Matches(UniqueId, otherT.UniqueId)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(ReplacedBy, otherT.ReplacedBy)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as NamingSystem;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(CountryElement, otherT.CountryElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(ResponsibleElement, otherT.ResponsibleElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.IsExactly(UniqueId, otherT.UniqueId)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(ReplacedBy, otherT.ReplacedBy)) return false;
            
            return true;
        }
        
    }
    
}
