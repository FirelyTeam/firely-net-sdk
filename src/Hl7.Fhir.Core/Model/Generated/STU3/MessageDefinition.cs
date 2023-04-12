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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A resource that defines a type of message that can be exchanged between systems
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "MessageDefinition", IsResource=true)]
    [DataContract]
    public partial class MessageDefinition : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMessageDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MessageDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "MessageDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "FocusComponent")]
        [DataContract]
        public partial class FocusComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMessageDefinitionFocusComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FocusComponent"; } }
            
            /// <summary>
            /// Type of resource
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResourceType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResourceType> _CodeElement;
            
            /// <summary>
            /// Type of resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResourceType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Profile that must be adhered to by focus
            /// </summary>
            [FhirElement("profile", Order=50)]
            [CLSCompliant(false)]
            [References("StructureDefinition")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
            /// <summary>
            /// Minimum number of focuses of this type
            /// </summary>
            [FhirElement("min", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt MinElement
            {
                get { return _MinElement; }
                set { _MinElement = value; OnPropertyChanged("MinElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _MinElement;
            
            /// <summary>
            /// Minimum number of focuses of this type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Min
            {
                get { return MinElement != null ? MinElement.Value : null; }
                set
                {
                    if (value == null)
                        MinElement = null;
                    else
                        MinElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Min");
                }
            }
            
            /// <summary>
            /// Maximum number of focuses of this type
            /// </summary>
            [FhirElement("max", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaxElement
            {
                get { return _MaxElement; }
                set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MaxElement;
            
            /// <summary>
            /// Maximum number of focuses of this type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Max
            {
                get { return MaxElement != null ? MaxElement.Value : null; }
                set
                {
                    if (value == null)
                        MaxElement = null;
                    else
                        MaxElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Max");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FocusComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Profile?.Serialize(sink);
                sink.Element("min", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MinElement?.Serialize(sink);
                sink.Element("max", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MaxElement?.Serialize(sink);
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
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>();
                        return true;
                    case "profile":
                        Profile = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "min":
                        MinElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "max":
                        MaxElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "profile":
                        Profile = source.Populate(Profile);
                        return true;
                    case "min":
                        MinElement = source.PopulateValue(MinElement);
                        return true;
                    case "_min":
                        MinElement = source.Populate(MinElement);
                        return true;
                    case "max":
                        MaxElement = source.PopulateValue(MaxElement);
                        return true;
                    case "_max":
                        MaxElement = source.Populate(MaxElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FocusComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.ResourceType>)CodeElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
                    if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.UnsignedInt)MinElement.DeepCopy();
                    if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FocusComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FocusComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FocusComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Profile != null) yield return Profile;
                    if (MinElement != null) yield return MinElement;
                    if (MaxElement != null) yield return MaxElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                    if (MinElement != null) yield return new ElementValue("min", MinElement);
                    if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AllowedResponseComponent")]
        [DataContract]
        public partial class AllowedResponseComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMessageDefinitionAllowedResponseComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AllowedResponseComponent"; } }
            
            /// <summary>
            /// Reference to allowed message definition response
            /// </summary>
            [FhirElement("message", Order=40)]
            [CLSCompliant(false)]
            [References("MessageDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Message
            {
                get { return _Message; }
                set { _Message = value; OnPropertyChanged("Message"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Message;
            
            /// <summary>
            /// When should this response be used
            /// </summary>
            [FhirElement("situation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown SituationElement
            {
                get { return _SituationElement; }
                set { _SituationElement = value; OnPropertyChanged("SituationElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _SituationElement;
            
            /// <summary>
            /// When should this response be used
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Situation
            {
                get { return SituationElement != null ? SituationElement.Value : null; }
                set
                {
                    if (value == null)
                        SituationElement = null;
                    else
                        SituationElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("Situation");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AllowedResponseComponent");
                base.Serialize(sink);
                sink.Element("message", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Message?.Serialize(sink);
                sink.Element("situation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SituationElement?.Serialize(sink);
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
                    case "message":
                        Message = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "situation":
                        SituationElement = source.Get<Hl7.Fhir.Model.Markdown>();
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
                    case "message":
                        Message = source.Populate(Message);
                        return true;
                    case "situation":
                        SituationElement = source.PopulateValue(SituationElement);
                        return true;
                    case "_situation":
                        SituationElement = source.Populate(SituationElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AllowedResponseComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Message != null) dest.Message = (Hl7.Fhir.Model.ResourceReference)Message.DeepCopy();
                    if(SituationElement != null) dest.SituationElement = (Hl7.Fhir.Model.Markdown)SituationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AllowedResponseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AllowedResponseComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Message, otherT.Message)) return false;
                if( !DeepComparable.Matches(SituationElement, otherT.SituationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AllowedResponseComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Message, otherT.Message)) return false;
                if( !DeepComparable.IsExactly(SituationElement, otherT.SituationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Message != null) yield return Message;
                    if (SituationElement != null) yield return SituationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Message != null) yield return new ElementValue("message", Message);
                    if (SituationElement != null) yield return new ElementValue("situation", SituationElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.IMessageDefinition.Contact { get { return Contact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IMessageDefinitionFocusComponent> Hl7.Fhir.Model.IMessageDefinition.Focus { get { return Focus; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IMessageDefinitionAllowedResponseComponent> Hl7.Fhir.Model.IMessageDefinition.AllowedResponse { get { return AllowedResponse; } }
    
        
        /// <summary>
        /// Logical URI to reference this message definition (globally unique)
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
        /// Logical URI to reference this message definition (globally unique)
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
        /// Additional identifier for the message definition
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Business version of the message definition
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
        /// Business version of the message definition
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
        /// Name for this message definition (computer friendly)
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
        /// Name for this message definition (computer friendly)
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
        /// Name for this message definition (human friendly)
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
        /// Name for this message definition (human friendly)
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
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// Date this was last changed
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date this was last changed
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
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
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
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the message definition
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the message definition
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
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        /// Intended jurisdiction for message definition (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
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
        /// Why this message definition is defined
        /// </summary>
        [FhirElement("purpose", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this message definition is defined
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
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=230)]
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
        /// Definition this one is based on
        /// </summary>
        [FhirElement("base", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("MessageDefinition")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Base
        {
            get { return _Base; }
            set { _Base = value; OnPropertyChanged("Base"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Base;
        
        /// <summary>
        /// Protocol/workflow this is part of
        /// </summary>
        [FhirElement("parent", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [References("ActivityDefinition","PlanDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Parent
        {
            get { if(_Parent==null) _Parent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Parent;
        
        /// <summary>
        /// Takes the place of
        /// </summary>
        [FhirElement("replaces", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [References("MessageDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Replaces
        {
            get { if(_Replaces==null) _Replaces = new List<Hl7.Fhir.Model.ResourceReference>(); return _Replaces; }
            set { _Replaces = value; OnPropertyChanged("Replaces"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Replaces;
        
        /// <summary>
        /// Event type
        /// </summary>
        [FhirElement("event", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private Hl7.Fhir.Model.Coding _Event;
        
        /// <summary>
        /// Consequence | Currency | Notification
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory> _CategoryElement;
        
        /// <summary>
        /// Consequence | Currency | Notification
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.MessageSignificanceCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if (value == null)
                    CategoryElement = null;
                else
                    CategoryElement = new Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// Resource(s) that are the subject of the event
        /// </summary>
        [FhirElement("focus", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FocusComponent> Focus
        {
            get { if(_Focus==null) _Focus = new List<FocusComponent>(); return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private List<FocusComponent> _Focus;
        
        /// <summary>
        /// Is a response required?
        /// </summary>
        [FhirElement("responseRequired", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ResponseRequiredElement
        {
            get { return _ResponseRequiredElement; }
            set { _ResponseRequiredElement = value; OnPropertyChanged("ResponseRequiredElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ResponseRequiredElement;
        
        /// <summary>
        /// Is a response required?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? ResponseRequired
        {
            get { return ResponseRequiredElement != null ? ResponseRequiredElement.Value : null; }
            set
            {
                if (value == null)
                    ResponseRequiredElement = null;
                else
                    ResponseRequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ResponseRequired");
            }
        }
        
        /// <summary>
        /// Responses to this message
        /// </summary>
        [FhirElement("allowedResponse", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AllowedResponseComponent> AllowedResponse
        {
            get { if(_AllowedResponse==null) _AllowedResponse = new List<AllowedResponseComponent>(); return _AllowedResponse; }
            set { _AllowedResponse = value; OnPropertyChanged("AllowedResponse"); }
        }
        
        private List<AllowedResponseComponent> _AllowedResponse;
    
    
        public static ElementDefinitionConstraint[] MessageDefinition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "md-1",
                severity: ConstraintSeverity.Warning,
                expression: "focus.all(max='*' or (max.toInteger() > 0))",
                human: "Max must be postive int or *",
                xpath: "f:max/@value='*' or number(f:max/@value) > 0"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(MessageDefinition_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MessageDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(Base != null) dest.Base = (Hl7.Fhir.Model.ResourceReference)Base.DeepCopy();
                if(Parent != null) dest.Parent = new List<Hl7.Fhir.Model.ResourceReference>(Parent.DeepCopy());
                if(Replaces != null) dest.Replaces = new List<Hl7.Fhir.Model.ResourceReference>(Replaces.DeepCopy());
                if(Event != null) dest.Event = (Hl7.Fhir.Model.Coding)Event.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory>)CategoryElement.DeepCopy();
                if(Focus != null) dest.Focus = new List<FocusComponent>(Focus.DeepCopy());
                if(ResponseRequiredElement != null) dest.ResponseRequiredElement = (Hl7.Fhir.Model.FhirBoolean)ResponseRequiredElement.DeepCopy();
                if(AllowedResponse != null) dest.AllowedResponse = new List<AllowedResponseComponent>(AllowedResponse.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MessageDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MessageDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Base, otherT.Base)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(Replaces, otherT.Replaces)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(ResponseRequiredElement, otherT.ResponseRequiredElement)) return false;
            if( !DeepComparable.Matches(AllowedResponse, otherT.AllowedResponse)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MessageDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Base, otherT.Base)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(Replaces, otherT.Replaces)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(ResponseRequiredElement, otherT.ResponseRequiredElement)) return false;
            if( !DeepComparable.IsExactly(AllowedResponse, otherT.AllowedResponse)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MessageDefinition");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DateElement?.Serialize(sink);
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
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PurposeElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("base", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Base?.Serialize(sink);
            sink.BeginList("parent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Parent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("replaces", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Replaces)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("event", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Event?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CategoryElement?.Serialize(sink);
            sink.BeginList("focus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Focus)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("responseRequired", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponseRequiredElement?.Serialize(sink);
            sink.BeginList("allowedResponse", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AllowedResponse)
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
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
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
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.PublicationStatus>>();
                    return true;
                case "experimental":
                    ExperimentalElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "publisher":
                    PublisherElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.STU3.ContactDetail>();
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
                case "copyright":
                    CopyrightElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "base":
                    Base = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "parent":
                    Parent = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "replaces":
                    Replaces = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "event":
                    Event = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "category":
                    CategoryElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory>>();
                    return true;
                case "focus":
                    Focus = source.GetList<FocusComponent>();
                    return true;
                case "responseRequired":
                    ResponseRequiredElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "allowedResponse":
                    AllowedResponse = source.GetList<AllowedResponseComponent>();
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
                    Identifier = source.Populate(Identifier);
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
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "base":
                    Base = source.Populate(Base);
                    return true;
                case "parent":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "replaces":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "event":
                    Event = source.Populate(Event);
                    return true;
                case "category":
                    CategoryElement = source.PopulateValue(CategoryElement);
                    return true;
                case "_category":
                    CategoryElement = source.Populate(CategoryElement);
                    return true;
                case "focus":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "responseRequired":
                    ResponseRequiredElement = source.PopulateValue(ResponseRequiredElement);
                    return true;
                case "_responseRequired":
                    ResponseRequiredElement = source.Populate(ResponseRequiredElement);
                    return true;
                case "allowedResponse":
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
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "parent":
                    source.PopulateListItem(Parent, index);
                    return true;
                case "replaces":
                    source.PopulateListItem(Replaces, index);
                    return true;
                case "focus":
                    source.PopulateListItem(Focus, index);
                    return true;
                case "allowedResponse":
                    source.PopulateListItem(AllowedResponse, index);
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
                if (Identifier != null) yield return Identifier;
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (TitleElement != null) yield return TitleElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (PurposeElement != null) yield return PurposeElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                if (Base != null) yield return Base;
                foreach (var elem in Parent) { if (elem != null) yield return elem; }
                foreach (var elem in Replaces) { if (elem != null) yield return elem; }
                if (Event != null) yield return Event;
                if (CategoryElement != null) yield return CategoryElement;
                foreach (var elem in Focus) { if (elem != null) yield return elem; }
                if (ResponseRequiredElement != null) yield return ResponseRequiredElement;
                foreach (var elem in AllowedResponse) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (Base != null) yield return new ElementValue("base", Base);
                foreach (var elem in Parent) { if (elem != null) yield return new ElementValue("parent", elem); }
                foreach (var elem in Replaces) { if (elem != null) yield return new ElementValue("replaces", elem); }
                if (Event != null) yield return new ElementValue("event", Event);
                if (CategoryElement != null) yield return new ElementValue("category", CategoryElement);
                foreach (var elem in Focus) { if (elem != null) yield return new ElementValue("focus", elem); }
                if (ResponseRequiredElement != null) yield return new ElementValue("responseRequired", ResponseRequiredElement);
                foreach (var elem in AllowedResponse) { if (elem != null) yield return new ElementValue("allowedResponse", elem); }
            }
        }
    
    }

}
