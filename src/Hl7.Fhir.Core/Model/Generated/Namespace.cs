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
// Generated on Thu, Oct 23, 2014 14:13+0200 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// System of unique identification
    /// </summary>
    [FhirType("Namespace", IsResource=true)]
    [DataContract]
    public partial class Namespace : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Indicates whether the namespace should be used
        /// </summary>
        [FhirEnumeration("NamespaceStatus")]
        public enum NamespaceStatus
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
        /// Identifies the purpose of the namespace
        /// </summary>
        [FhirEnumeration("NamespaceType")]
        public enum NamespaceType
        {
            /// <summary>
            /// The namespace is used to define concepts and symbols to represent those concepts.  E.g. UCUM, LOINC, NDC code, local lab codes, etc.
            /// </summary>
            [EnumLiteral("codesystem")]
            Codesystem,
            /// <summary>
            /// The namespace is used to manage identifiers (e.g. license numbers, order numbers, etc.).
            /// </summary>
            [EnumLiteral("identifier")]
            Identifier,
            /// <summary>
            /// The namespace is used as the root for other identifiers and namespaces.
            /// </summary>
            [EnumLiteral("root")]
            Root,
        }
        
        /// <summary>
        /// Identifies the style of unique identifier used to identify a namepace
        /// </summary>
        [FhirEnumeration("NamespaceIdentifierType")]
        public enum NamespaceIdentifierType
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
        
        [FhirType("NamespaceContactComponent")]
        [DataContract]
        public partial class NamespaceContactComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of person
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
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
            [FhirElement("telecom", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ContactPoint> Telecom
            {
                get { return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NamespaceContactComponent;
                
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
                return CopyTo(new NamespaceContactComponent());
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NamespaceContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
            public override bool Matches(IDeepComparable other)
            {
                throw new NotImplementedException();
            }
        }
        
        
        [FhirType("NamespaceUniqueIdComponent")]
        [DataContract]
        public partial class NamespaceUniqueIdComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// oid | uuid | uri | other
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Namespace.NamespaceIdentifierType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.Namespace.NamespaceIdentifierType> _TypeElement;
            
            /// <summary>
            /// oid | uuid | uri | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Namespace.NamespaceIdentifierType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Namespace.NamespaceIdentifierType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The unique identifier
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50)]
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
            [FhirElement("preferred", InSummary=true, Order=60)]
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
            [FhirElement("period", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            private Hl7.Fhir.Model.Period _Period;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NamespaceUniqueIdComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Namespace.NamespaceIdentifierType>)TypeElement.DeepCopy();
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
                return CopyTo(new NamespaceUniqueIdComponent());
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NamespaceUniqueIdComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(PreferredElement, otherT.PreferredElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool Matches(IDeepComparable other)
            {
                throw new NotImplementedException();
            }
        }
        
        
        /// <summary>
        /// codesystem | identifier | root
        /// </summary>
        [FhirElement("type", Order=60)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Namespace.NamespaceType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        private Code<Hl7.Fhir.Model.Namespace.NamespaceType> _TypeElement;
        
        /// <summary>
        /// codesystem | identifier | root
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Namespace.NamespaceType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Namespace.NamespaceType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        [FhirElement("name", Order=70)]
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
        [FhirElement("status", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Namespace.NamespaceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Namespace.NamespaceStatus> _StatusElement;
        
        /// <summary>
        /// proposed | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Namespace.NamespaceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Namespace.NamespaceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// ISO 3-char country code
        /// </summary>
        [FhirElement("country", Order=90)]
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
        [FhirElement("category", Order=100)]
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
        [FhirElement("responsible", Order=110)]
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
        /// What does namespace identify?
        /// </summary>
        [FhirElement("description", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// What does namespace identify?
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
        [FhirElement("usage", Order=130)]
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
        [FhirElement("uniqueId", Order=140)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Namespace.NamespaceUniqueIdComponent> UniqueId
        {
            get { return _UniqueId; }
            set { _UniqueId = value; OnPropertyChanged("UniqueId"); }
        }
        private List<Hl7.Fhir.Model.Namespace.NamespaceUniqueIdComponent> _UniqueId;
        
        /// <summary>
        /// Who should be contacted for questions about namespace
        /// </summary>
        [FhirElement("contact", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Namespace.NamespaceContactComponent Contact
        {
            get { return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        private Hl7.Fhir.Model.Namespace.NamespaceContactComponent _Contact;
        
        /// <summary>
        /// Use this instead
        /// </summary>
        [FhirElement("replacedBy", Order=160)]
        [References("Namespace")]
        [DataMember]
        public Hl7.Fhir.Model.Reference ReplacedBy
        {
            get { return _ReplacedBy; }
            set { _ReplacedBy = value; OnPropertyChanged("ReplacedBy"); }
        }
        private Hl7.Fhir.Model.Reference _ReplacedBy;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Namespace;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Namespace.NamespaceType>)TypeElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Namespace.NamespaceStatus>)StatusElement.DeepCopy();
                if(CountryElement != null) dest.CountryElement = (Hl7.Fhir.Model.Code)CountryElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(ResponsibleElement != null) dest.ResponsibleElement = (Hl7.Fhir.Model.FhirString)ResponsibleElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(UniqueId != null) dest.UniqueId = new List<Hl7.Fhir.Model.Namespace.NamespaceUniqueIdComponent>(UniqueId.DeepCopy());
                if(Contact != null) dest.Contact = (Hl7.Fhir.Model.Namespace.NamespaceContactComponent)Contact.DeepCopy();
                if(ReplacedBy != null) dest.ReplacedBy = (Hl7.Fhir.Model.Reference)ReplacedBy.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Namespace());
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Namespace;
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
        
        public override bool Matches(IDeepComparable other)
        {
            throw new NotImplementedException();
        }
    }
    
}
