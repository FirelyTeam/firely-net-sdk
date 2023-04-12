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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// The definition of a specific activity to be taken, independent of any particular patient or context
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ActivityDefinition", IsResource=true)]
    [DataContract]
    public partial class ActivityDefinition : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IActivityDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ActivityDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ActivityDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IActivityDefinitionParticipantComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// patient | practitioner | related-person | device
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.ActionParticipantType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.ActionParticipantType> _TypeElement;
            
            /// <summary>
            /// patient | practitioner | related-person | device
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.ActionParticipantType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.ActionParticipantType>(value);
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParticipantComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Role?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type":
                        TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ActionParticipantType>>();
                        return true;
                    case "role":
                        Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.ActionParticipantType>)TypeElement.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DynamicValueComponent")]
        [DataContract]
        public partial class DynamicValueComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IActivityDefinitionDynamicValueComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            public Hl7.Fhir.Model.Expression Expression
            {
                get { return _Expression; }
                set { _Expression = value; OnPropertyChanged("Expression"); }
            }
            
            private Hl7.Fhir.Model.Expression _Expression;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DynamicValueComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); PathElement?.Serialize(sink);
                sink.Element("expression", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Expression?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "path":
                        PathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "expression":
                        Expression = source.Get<Hl7.Fhir.Model.Expression>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "expression":
                        Expression = source.Populate(Expression);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DynamicValueComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(Expression != null) dest.Expression = (Hl7.Fhir.Model.Expression)Expression.DeepCopy();
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
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.IActivityDefinition.Contact { get { return Contact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Hl7.Fhir.Model.IActivityDefinition.RelatedArtifact { get { return RelatedArtifact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IActivityDefinitionParticipantComponent> Hl7.Fhir.Model.IActivityDefinition.Participant { get { return Participant; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDosage> Hl7.Fhir.Model.IActivityDefinition.Dosage { get { return Dosage; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IActivityDefinitionDynamicValueComponent> Hl7.Fhir.Model.IActivityDefinition.DynamicValue { get { return DynamicValue; } }
    
        
        /// <summary>
        /// Canonical identifier for this activity definition, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
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
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
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
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
                if (value == null)
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
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
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
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
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
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the activity definition
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the activity definition
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for activity definition (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
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
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this activity definition is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if (value == null)
                    PurposeElement = null;
                else
                    PurposeElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Purpose");
            }
        }
        
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
        public Hl7.Fhir.Model.Markdown CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CopyrightElement;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if (value == null)
                    CopyrightElement = null;
                else
                    CopyrightElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Copyright");
            }
        }
        
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
        [FhirElement("effectivePeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
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
        public List<Hl7.Fhir.Model.R4.ContactDetail> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Author;
        
        /// <summary>
        /// Who edited the content
        /// </summary>
        [FhirElement("editor", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Editor
        {
            get { if(_Editor==null) _Editor = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Editor; }
            set { _Editor = value; OnPropertyChanged("Editor"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Editor;
        
        /// <summary>
        /// Who reviewed the content
        /// </summary>
        [FhirElement("reviewer", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Reviewer
        {
            get { if(_Reviewer==null) _Reviewer = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Reviewer; }
            set { _Reviewer = value; OnPropertyChanged("Reviewer"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Reviewer;
        
        /// <summary>
        /// Who endorsed the content
        /// </summary>
        [FhirElement("endorser", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Endorser
        {
            get { if(_Endorser==null) _Endorser = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Endorser; }
            set { _Endorser = value; OnPropertyChanged("Endorser"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Endorser;
        
        /// <summary>
        /// Additional documentation, citations, etc.
        /// </summary>
        [FhirElement("relatedArtifact", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.RelatedArtifact> RelatedArtifact
        {
            get { if(_RelatedArtifact==null) _RelatedArtifact = new List<Hl7.Fhir.Model.R4.RelatedArtifact>(); return _RelatedArtifact; }
            set { _RelatedArtifact = value; OnPropertyChanged("RelatedArtifact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.RelatedArtifact> _RelatedArtifact;
        
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
        [FhirElement("kind", InSummary=Hl7.Fhir.Model.Version.All, Order=370)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.RequestResourceType> KindElement
        {
            get { return _KindElement; }
            set { _KindElement = value; OnPropertyChanged("KindElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.RequestResourceType> _KindElement;
        
        /// <summary>
        /// Kind of resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.RequestResourceType? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if (value == null)
                    KindElement = null;
                else
                    KindElement = new Code<Hl7.Fhir.Model.R4.RequestResourceType>(value);
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
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=390)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// proposal | plan | directive | order | original-order | reflex-order | filler-order | instance-order | option
        /// </summary>
        [FhirElement("intent", Order=400)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.RequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.RequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | directive | order | original-order | reflex-order | filler-order | instance-order | option
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.RequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (value == null)
                    IntentElement = null;
                else
                    IntentElement = new Code<Hl7.Fhir.Model.R4.RequestIntent>(value);
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
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// True if the activity should not be performed
        /// </summary>
        [FhirElement("doNotPerform", InSummary=Hl7.Fhir.Model.Version.All, Order=420)]
        [CLSCompliant(false)]
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
                if (value == null)
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
        [AllowedTypes(typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.R4.Duration))]
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
        public List<ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<ParticipantComponent> _Participant;
        
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
        public List<Hl7.Fhir.Model.R4.Dosage> Dosage
        {
            get { if(_Dosage==null) _Dosage = new List<Hl7.Fhir.Model.R4.Dosage>(); return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private List<Hl7.Fhir.Model.R4.Dosage> _Dosage;
        
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
        public List<DynamicValueComponent> DynamicValue
        {
            get { if(_DynamicValue==null) _DynamicValue = new List<DynamicValueComponent>(); return _DynamicValue; }
            set { _DynamicValue = value; OnPropertyChanged("DynamicValue"); }
        }
        
        private List<DynamicValueComponent> _DynamicValue;
    
    
        public static ElementDefinitionConstraint[] ActivityDefinition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "adf-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(ActivityDefinition_Constraints);
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
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(Topic != null) dest.Topic = new List<Hl7.Fhir.Model.CodeableConcept>(Topic.DeepCopy());
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.R4.ContactDetail>(Author.DeepCopy());
                if(Editor != null) dest.Editor = new List<Hl7.Fhir.Model.R4.ContactDetail>(Editor.DeepCopy());
                if(Reviewer != null) dest.Reviewer = new List<Hl7.Fhir.Model.R4.ContactDetail>(Reviewer.DeepCopy());
                if(Endorser != null) dest.Endorser = new List<Hl7.Fhir.Model.R4.ContactDetail>(Endorser.DeepCopy());
                if(RelatedArtifact != null) dest.RelatedArtifact = new List<Hl7.Fhir.Model.R4.RelatedArtifact>(RelatedArtifact.DeepCopy());
                if(LibraryElement != null) dest.LibraryElement = new List<Hl7.Fhir.Model.Canonical>(LibraryElement.DeepCopy());
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.R4.RequestResourceType>)KindElement.DeepCopy();
                if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.R4.RequestIntent>)IntentElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(DoNotPerformElement != null) dest.DoNotPerformElement = (Hl7.Fhir.Model.FhirBoolean)DoNotPerformElement.DeepCopy();
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Participant != null) dest.Participant = new List<ParticipantComponent>(Participant.DeepCopy());
                if(Product != null) dest.Product = (Hl7.Fhir.Model.Element)Product.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                if(Dosage != null) dest.Dosage = new List<Hl7.Fhir.Model.R4.Dosage>(Dosage.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(SpecimenRequirement != null) dest.SpecimenRequirement = new List<Hl7.Fhir.Model.ResourceReference>(SpecimenRequirement.DeepCopy());
                if(ObservationRequirement != null) dest.ObservationRequirement = new List<Hl7.Fhir.Model.ResourceReference>(ObservationRequirement.DeepCopy());
                if(ObservationResultRequirement != null) dest.ObservationResultRequirement = new List<Hl7.Fhir.Model.ResourceReference>(ObservationResultRequirement.DeepCopy());
                if(TransformElement != null) dest.TransformElement = (Hl7.Fhir.Model.Canonical)TransformElement.DeepCopy();
                if(DynamicValue != null) dest.DynamicValue = new List<DynamicValueComponent>(DynamicValue.DeepCopy());
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
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
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
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
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
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ActivityDefinition");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("subtitle", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubtitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Subject?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.Element("usage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UsageElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("approvalDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ApprovalDateElement?.Serialize(sink);
            sink.Element("lastReviewDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LastReviewDateElement?.Serialize(sink);
            sink.Element("effectivePeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EffectivePeriod?.Serialize(sink);
            sink.BeginList("topic", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Topic)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Author)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("editor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Editor)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reviewer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Reviewer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("endorser", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Endorser)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("relatedArtifact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in RelatedArtifact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("library", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(LibraryElement);
            sink.End();
            sink.Element("kind", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); KindElement?.Serialize(sink);
            sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ProfileElement?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.Element("intent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); IntentElement?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PriorityElement?.Serialize(sink);
            sink.Element("doNotPerform", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DoNotPerformElement?.Serialize(sink);
            sink.Element("timing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Timing?.Serialize(sink);
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Location?.Serialize(sink);
            sink.BeginList("participant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Participant)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("product", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Product?.Serialize(sink);
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
            sink.BeginList("dosage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Dosage)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in BodySite)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("specimenRequirement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SpecimenRequirement)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("observationRequirement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ObservationRequirement)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("observationResultRequirement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ObservationResultRequirement)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("transform", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TransformElement?.Serialize(sink);
            sink.BeginList("dynamicValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DynamicValue)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "url":
                    UrlElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "subtitle":
                    SubtitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.PublicationStatus>>();
                    return true;
                case "experimental":
                    ExperimentalElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "subjectCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Subject, "subject");
                    Subject = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subjectReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Subject, "subject");
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "publisher":
                    PublisherElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "useContext":
                    UseContext = source.GetList<Hl7.Fhir.Model.UsageContext>();
                    return true;
                case "jurisdiction":
                    Jurisdiction = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "purpose":
                    PurposeElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "usage":
                    UsageElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "copyright":
                    CopyrightElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "approvalDate":
                    ApprovalDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "lastReviewDate":
                    LastReviewDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "effectivePeriod":
                    EffectivePeriod = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "topic":
                    Topic = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "author":
                    Author = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
                    return true;
                case "editor":
                    Editor = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
                    return true;
                case "reviewer":
                    Reviewer = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
                    return true;
                case "endorser":
                    Endorser = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
                    return true;
                case "relatedArtifact":
                    RelatedArtifact = source.GetList<Hl7.Fhir.Model.R4.RelatedArtifact>();
                    return true;
                case "library":
                    LibraryElement = source.GetList<Hl7.Fhir.Model.Canonical>();
                    return true;
                case "kind":
                    KindElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.RequestResourceType>>();
                    return true;
                case "profile":
                    ProfileElement = source.Get<Hl7.Fhir.Model.Canonical>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "intent":
                    IntentElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.RequestIntent>>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RequestPriority>>();
                    return true;
                case "doNotPerform":
                    DoNotPerformElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "timingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.R4.Timing>();
                    return true;
                case "timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "timingAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.R4.Age>();
                    return true;
                case "timingPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "timingRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "timingDuration":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.R4.Duration>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "participant":
                    Participant = source.GetList<ParticipantComponent>();
                    return true;
                case "productReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Product, "product");
                    Product = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "productCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Product, "product");
                    Product = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "quantity":
                    Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                    return true;
                case "dosage":
                    Dosage = source.GetList<Hl7.Fhir.Model.R4.Dosage>();
                    return true;
                case "bodySite":
                    BodySite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "specimenRequirement":
                    SpecimenRequirement = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "observationRequirement":
                    ObservationRequirement = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "observationResultRequirement":
                    ObservationResultRequirement = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "transform":
                    TransformElement = source.Get<Hl7.Fhir.Model.Canonical>();
                    return true;
                case "dynamicValue":
                    DynamicValue = source.GetList<DynamicValueComponent>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "subtitle":
                    SubtitleElement = source.PopulateValue(SubtitleElement);
                    return true;
                case "_subtitle":
                    SubtitleElement = source.Populate(SubtitleElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "experimental":
                    ExperimentalElement = source.PopulateValue(ExperimentalElement);
                    return true;
                case "_experimental":
                    ExperimentalElement = source.Populate(ExperimentalElement);
                    return true;
                case "subjectCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Subject, "subject");
                    Subject = source.Populate(Subject as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "subjectReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Subject, "subject");
                    Subject = source.Populate(Subject as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "purpose":
                    PurposeElement = source.PopulateValue(PurposeElement);
                    return true;
                case "_purpose":
                    PurposeElement = source.Populate(PurposeElement);
                    return true;
                case "usage":
                    UsageElement = source.PopulateValue(UsageElement);
                    return true;
                case "_usage":
                    UsageElement = source.Populate(UsageElement);
                    return true;
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "approvalDate":
                    ApprovalDateElement = source.PopulateValue(ApprovalDateElement);
                    return true;
                case "_approvalDate":
                    ApprovalDateElement = source.Populate(ApprovalDateElement);
                    return true;
                case "lastReviewDate":
                    LastReviewDateElement = source.PopulateValue(LastReviewDateElement);
                    return true;
                case "_lastReviewDate":
                    LastReviewDateElement = source.Populate(LastReviewDateElement);
                    return true;
                case "effectivePeriod":
                    EffectivePeriod = source.Populate(EffectivePeriod);
                    return true;
                case "topic":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "author":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "editor":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reviewer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "endorser":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "relatedArtifact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "library":
                case "_library":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "kind":
                    KindElement = source.PopulateValue(KindElement);
                    return true;
                case "_kind":
                    KindElement = source.Populate(KindElement);
                    return true;
                case "profile":
                    ProfileElement = source.PopulateValue(ProfileElement);
                    return true;
                case "_profile":
                    ProfileElement = source.Populate(ProfileElement);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "intent":
                    IntentElement = source.PopulateValue(IntentElement);
                    return true;
                case "_intent":
                    IntentElement = source.Populate(IntentElement);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "doNotPerform":
                    DoNotPerformElement = source.PopulateValue(DoNotPerformElement);
                    return true;
                case "_doNotPerform":
                    DoNotPerformElement = source.Populate(DoNotPerformElement);
                    return true;
                case "timingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.R4.Timing);
                    return true;
                case "timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.PopulateValue(Timing as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "timingAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.R4.Age);
                    return true;
                case "timingPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.Period);
                    return true;
                case "timingRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.Range);
                    return true;
                case "timingDuration":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.R4.Duration);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "participant":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "productReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Product, "product");
                    Product = source.Populate(Product as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "productCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Product, "product");
                    Product = source.Populate(Product as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "quantity":
                    Quantity = source.Populate(Quantity);
                    return true;
                case "dosage":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "bodySite":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "specimenRequirement":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "observationRequirement":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "observationResultRequirement":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "transform":
                    TransformElement = source.PopulateValue(TransformElement);
                    return true;
                case "_transform":
                    TransformElement = source.Populate(TransformElement);
                    return true;
                case "dynamicValue":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "topic":
                    source.PopulateListItem(Topic, index);
                    return true;
                case "author":
                    source.PopulateListItem(Author, index);
                    return true;
                case "editor":
                    source.PopulateListItem(Editor, index);
                    return true;
                case "reviewer":
                    source.PopulateListItem(Reviewer, index);
                    return true;
                case "endorser":
                    source.PopulateListItem(Endorser, index);
                    return true;
                case "relatedArtifact":
                    source.PopulateListItem(RelatedArtifact, index);
                    return true;
                case "library":
                    source.PopulatePrimitiveListItemValue(LibraryElement, index);
                    return true;
                case "_library":
                    source.PopulatePrimitiveListItem(LibraryElement, index);
                    return true;
                case "participant":
                    source.PopulateListItem(Participant, index);
                    return true;
                case "dosage":
                    source.PopulateListItem(Dosage, index);
                    return true;
                case "bodySite":
                    source.PopulateListItem(BodySite, index);
                    return true;
                case "specimenRequirement":
                    source.PopulateListItem(SpecimenRequirement, index);
                    return true;
                case "observationRequirement":
                    source.PopulateListItem(ObservationRequirement, index);
                    return true;
                case "observationResultRequirement":
                    source.PopulateListItem(ObservationResultRequirement, index);
                    return true;
                case "dynamicValue":
                    source.PopulateListItem(DynamicValue, index);
                    return true;
            }
            return false;
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
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (PurposeElement != null) yield return PurposeElement;
                if (UsageElement != null) yield return UsageElement;
                if (CopyrightElement != null) yield return CopyrightElement;
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
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (UsageElement != null) yield return new ElementValue("usage", UsageElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
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
