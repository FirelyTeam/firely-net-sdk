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
    /// A resource that defines a type of message that can be exchanged between systems
    /// </summary>
    [FhirType("MessageDefinition", IsResource=true)]
    [DataContract]
    public partial class MessageDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MessageDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "MessageDefinition"; } }
        
        /// <summary>
        /// The impact of the content of a message.
        /// (url: http://hl7.org/fhir/ValueSet/message-significance-category)
        /// </summary>
        [FhirEnumeration("MessageSignificanceCategory")]
        public enum MessageSignificanceCategory
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/message-significance-category)
            /// </summary>
            [EnumLiteral("consequence", "http://hl7.org/fhir/message-significance-category"), Description("Consequence")]
            Consequence,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/message-significance-category)
            /// </summary>
            [EnumLiteral("currency", "http://hl7.org/fhir/message-significance-category"), Description("Currency")]
            Currency,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/message-significance-category)
            /// </summary>
            [EnumLiteral("notification", "http://hl7.org/fhir/message-significance-category"), Description("Notification")]
            Notification,
        }

        /// <summary>
        /// HL7-defined table of codes which identify conditions under which acknowledgments are required to be returned in response to a message.
        /// (url: http://hl7.org/fhir/ValueSet/messageheader-response-request)
        /// </summary>
        [FhirEnumeration("messageheader_response_request")]
        public enum messageheader_response_request
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/messageheader-response-request)
            /// </summary>
            [EnumLiteral("always", "http://hl7.org/fhir/messageheader-response-request"), Description("Always")]
            Always,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/messageheader-response-request)
            /// </summary>
            [EnumLiteral("on-error", "http://hl7.org/fhir/messageheader-response-request"), Description("Error/reject conditions only")]
            OnError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/messageheader-response-request)
            /// </summary>
            [EnumLiteral("never", "http://hl7.org/fhir/messageheader-response-request"), Description("Never")]
            Never,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/messageheader-response-request)
            /// </summary>
            [EnumLiteral("on-success", "http://hl7.org/fhir/messageheader-response-request"), Description("Successful completion only")]
            OnSuccess,
        }

        [FhirType("FocusComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class FocusComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FocusComponent"; } }
            
            /// <summary>
            /// Type of resource
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
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
                    if (!value.HasValue)
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
            [DataMember]
            public Hl7.Fhir.Model.Canonical ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ProfileElement;
            
            /// <summary>
            /// Profile that must be adhered to by focus
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
            /// Minimum number of focuses of this type
            /// </summary>
            [FhirElement("min", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
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
                    if (!value.HasValue)
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FocusComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.ResourceType>)CodeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
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
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
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
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
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
                    if (ProfileElement != null) yield return ProfileElement;
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
                    if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                    if (MinElement != null) yield return new ElementValue("min", MinElement);
                    if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                }
            }

            
        }
        
        
        [FhirType("AllowedResponseComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AllowedResponseComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AllowedResponseComponent"; } }
            
            /// <summary>
            /// Reference to allowed message definition response
            /// </summary>
            [FhirElement("message", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical MessageElement
            {
                get { return _MessageElement; }
                set { _MessageElement = value; OnPropertyChanged("MessageElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _MessageElement;
            
            /// <summary>
            /// Reference to allowed message definition response
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Message
            {
                get { return MessageElement != null ? MessageElement.Value : null; }
                set
                {
                    if (value == null)
                        MessageElement = null; 
                    else
                        MessageElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Message");
                }
            }
            
            /// <summary>
            /// When should this response be used
            /// </summary>
            [FhirElement("situation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Situation
            {
                get { return _Situation; }
                set { _Situation = value; OnPropertyChanged("Situation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Situation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AllowedResponseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(MessageElement != null) dest.MessageElement = (Hl7.Fhir.Model.Canonical)MessageElement.DeepCopy();
                    if(Situation != null) dest.Situation = (Hl7.Fhir.Model.Markdown)Situation.DeepCopy();
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
                if( !DeepComparable.Matches(MessageElement, otherT.MessageElement)) return false;
                if( !DeepComparable.Matches(Situation, otherT.Situation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AllowedResponseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(MessageElement, otherT.MessageElement)) return false;
                if( !DeepComparable.IsExactly(Situation, otherT.Situation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (MessageElement != null) yield return MessageElement;
                    if (Situation != null) yield return Situation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (MessageElement != null) yield return new ElementValue("message", MessageElement);
                    if (Situation != null) yield return new ElementValue("situation", Situation);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for a given MessageDefinition
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
        /// Business Identifier for a given MessageDefinition
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
        /// Primary key for the message definition on a given server
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
        /// Business version of the message definition
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
        [FhirElement("name", InSummary=true, Order=120)]
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
        [FhirElement("title", InSummary=true, Order=130)]
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
        /// Takes the place of
        /// </summary>
        [FhirElement("replaces", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> ReplacesElement
        {
            get { if(_ReplacesElement==null) _ReplacesElement = new List<Hl7.Fhir.Model.Canonical>(); return _ReplacesElement; }
            set { _ReplacesElement = value; OnPropertyChanged("ReplacesElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _ReplacesElement;
        
        /// <summary>
        /// Takes the place of
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Replaces
        {
            get { return ReplacesElement != null ? ReplacesElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ReplacesElement = null; 
                else
                  ReplacesElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Replaces");
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
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=170)]
        [Cardinality(Min=1,Max=1)]
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
        [FhirElement("publisher", InSummary=true, Order=180)]
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
        [FhirElement("contact", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the message definition
        /// </summary>
        [FhirElement("description", InSummary=true, Order=200)]
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
        [FhirElement("useContext", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for message definition (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=220)]
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
        [FhirElement("purpose", InSummary=true, Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Purpose;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// Definition this one is based on
        /// </summary>
        [FhirElement("base", InSummary=true, Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical BaseElement
        {
            get { return _BaseElement; }
            set { _BaseElement = value; OnPropertyChanged("BaseElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _BaseElement;
        
        /// <summary>
        /// Definition this one is based on
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Base
        {
            get { return BaseElement != null ? BaseElement.Value : null; }
            set
            {
                if (value == null)
                  BaseElement = null; 
                else
                  BaseElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Base");
            }
        }
        
        /// <summary>
        /// Protocol/workflow this is part of
        /// </summary>
        [FhirElement("parent", InSummary=true, Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> ParentElement
        {
            get { if(_ParentElement==null) _ParentElement = new List<Hl7.Fhir.Model.Canonical>(); return _ParentElement; }
            set { _ParentElement = value; OnPropertyChanged("ParentElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _ParentElement;
        
        /// <summary>
        /// Protocol/workflow this is part of
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Parent
        {
            get { return ParentElement != null ? ParentElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ParentElement = null; 
                else
                  ParentElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Parent");
            }
        }
        
        /// <summary>
        /// Event code  or link to the EventDefinition
        /// </summary>
        [FhirElement("event", InSummary=true, Order=270, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.FhirUri))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private Hl7.Fhir.Model.Element _Event;
        
        /// <summary>
        /// consequence | currency | notification
        /// </summary>
        [FhirElement("category", InSummary=true, Order=280)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MessageDefinition.MessageSignificanceCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MessageDefinition.MessageSignificanceCategory> _CategoryElement;
        
        /// <summary>
        /// consequence | currency | notification
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MessageDefinition.MessageSignificanceCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  CategoryElement = null; 
                else
                  CategoryElement = new Code<Hl7.Fhir.Model.MessageDefinition.MessageSignificanceCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// Resource(s) that are the subject of the event
        /// </summary>
        [FhirElement("focus", InSummary=true, Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MessageDefinition.FocusComponent> Focus
        {
            get { if(_Focus==null) _Focus = new List<Hl7.Fhir.Model.MessageDefinition.FocusComponent>(); return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private List<Hl7.Fhir.Model.MessageDefinition.FocusComponent> _Focus;
        
        /// <summary>
        /// always | on-error | never | on-success
        /// </summary>
        [FhirElement("responseRequired", Order=300)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MessageDefinition.messageheader_response_request> ResponseRequiredElement
        {
            get { return _ResponseRequiredElement; }
            set { _ResponseRequiredElement = value; OnPropertyChanged("ResponseRequiredElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MessageDefinition.messageheader_response_request> _ResponseRequiredElement;
        
        /// <summary>
        /// always | on-error | never | on-success
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MessageDefinition.messageheader_response_request? ResponseRequired
        {
            get { return ResponseRequiredElement != null ? ResponseRequiredElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ResponseRequiredElement = null; 
                else
                  ResponseRequiredElement = new Code<Hl7.Fhir.Model.MessageDefinition.messageheader_response_request>(value);
                OnPropertyChanged("ResponseRequired");
            }
        }
        
        /// <summary>
        /// Responses to this message
        /// </summary>
        [FhirElement("allowedResponse", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MessageDefinition.AllowedResponseComponent> AllowedResponse
        {
            get { if(_AllowedResponse==null) _AllowedResponse = new List<Hl7.Fhir.Model.MessageDefinition.AllowedResponseComponent>(); return _AllowedResponse; }
            set { _AllowedResponse = value; OnPropertyChanged("AllowedResponse"); }
        }
        
        private List<Hl7.Fhir.Model.MessageDefinition.AllowedResponseComponent> _AllowedResponse;
        
        /// <summary>
        /// Canonical reference to a GraphDefinition
        /// </summary>
        [FhirElement("graph", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> GraphElement
        {
            get { if(_GraphElement==null) _GraphElement = new List<Hl7.Fhir.Model.Canonical>(); return _GraphElement; }
            set { _GraphElement = value; OnPropertyChanged("GraphElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _GraphElement;
        
        /// <summary>
        /// Canonical reference to a GraphDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Graph
        {
            get { return GraphElement != null ? GraphElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  GraphElement = null; 
                else
                  GraphElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Graph");
            }
        }
        

        public static ElementDefinition.ConstraintComponent MessageDefinition_MSD_0 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "msd-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public static ElementDefinition.ConstraintComponent MessageDefinition_MD_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "focus.all(max='*' or (max.toInteger() > 0))",
            Key = "md-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Max must be postive int or *",
            Xpath = "f:max/@value='*' or number(f:max/@value) > 0"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(MessageDefinition_MSD_0);
            InvariantConstraints.Add(MessageDefinition_MD_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MessageDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(ReplacesElement != null) dest.ReplacesElement = new List<Hl7.Fhir.Model.Canonical>(ReplacesElement.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(BaseElement != null) dest.BaseElement = (Hl7.Fhir.Model.Canonical)BaseElement.DeepCopy();
                if(ParentElement != null) dest.ParentElement = new List<Hl7.Fhir.Model.Canonical>(ParentElement.DeepCopy());
                if(Event != null) dest.Event = (Hl7.Fhir.Model.Element)Event.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.MessageDefinition.MessageSignificanceCategory>)CategoryElement.DeepCopy();
                if(Focus != null) dest.Focus = new List<Hl7.Fhir.Model.MessageDefinition.FocusComponent>(Focus.DeepCopy());
                if(ResponseRequiredElement != null) dest.ResponseRequiredElement = (Code<Hl7.Fhir.Model.MessageDefinition.messageheader_response_request>)ResponseRequiredElement.DeepCopy();
                if(AllowedResponse != null) dest.AllowedResponse = new List<Hl7.Fhir.Model.MessageDefinition.AllowedResponseComponent>(AllowedResponse.DeepCopy());
                if(GraphElement != null) dest.GraphElement = new List<Hl7.Fhir.Model.Canonical>(GraphElement.DeepCopy());
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
            if( !DeepComparable.Matches(ReplacesElement, otherT.ReplacesElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.Matches(ParentElement, otherT.ParentElement)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(ResponseRequiredElement, otherT.ResponseRequiredElement)) return false;
            if( !DeepComparable.Matches(AllowedResponse, otherT.AllowedResponse)) return false;
            if( !DeepComparable.Matches(GraphElement, otherT.GraphElement)) return false;
            
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
            if( !DeepComparable.IsExactly(ReplacesElement, otherT.ReplacesElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.IsExactly(ParentElement, otherT.ParentElement)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(ResponseRequiredElement, otherT.ResponseRequiredElement)) return false;
            if( !DeepComparable.IsExactly(AllowedResponse, otherT.AllowedResponse)) return false;
            if( !DeepComparable.IsExactly(GraphElement, otherT.GraphElement)) return false;
            
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
				foreach (var elem in ReplacesElement) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (Description != null) yield return Description;
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Purpose != null) yield return Purpose;
				if (Copyright != null) yield return Copyright;
				if (BaseElement != null) yield return BaseElement;
				foreach (var elem in ParentElement) { if (elem != null) yield return elem; }
				if (Event != null) yield return Event;
				if (CategoryElement != null) yield return CategoryElement;
				foreach (var elem in Focus) { if (elem != null) yield return elem; }
				if (ResponseRequiredElement != null) yield return ResponseRequiredElement;
				foreach (var elem in AllowedResponse) { if (elem != null) yield return elem; }
				foreach (var elem in GraphElement) { if (elem != null) yield return elem; }
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
                foreach (var elem in ReplacesElement) { if (elem != null) yield return new ElementValue("replaces", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (BaseElement != null) yield return new ElementValue("base", BaseElement);
                foreach (var elem in ParentElement) { if (elem != null) yield return new ElementValue("parent", elem); }
                if (Event != null) yield return new ElementValue("event", Event);
                if (CategoryElement != null) yield return new ElementValue("category", CategoryElement);
                foreach (var elem in Focus) { if (elem != null) yield return new ElementValue("focus", elem); }
                if (ResponseRequiredElement != null) yield return new ElementValue("responseRequired", ResponseRequiredElement);
                foreach (var elem in AllowedResponse) { if (elem != null) yield return new ElementValue("allowedResponse", elem); }
                foreach (var elem in GraphElement) { if (elem != null) yield return new ElementValue("graph", elem); }
            }
        }

    }
    
}
