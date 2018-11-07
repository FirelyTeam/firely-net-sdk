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
    /// A resource that describes a message that is exchanged between systems
    /// </summary>
    [FhirType("MessageHeader", IsResource=true)]
    [DataContract]
    public partial class MessageHeader : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MessageHeader; } }
        [NotMapped]
        public override string TypeName { get { return "MessageHeader"; } }
        
        /// <summary>
        /// The kind of response to a message
        /// (url: http://hl7.org/fhir/ValueSet/response-code)
        /// </summary>
        [FhirEnumeration("ResponseType")]
        public enum ResponseType
        {
            /// <summary>
            /// The message was accepted and processed without error.
            /// (system: http://hl7.org/fhir/response-code)
            /// </summary>
            [EnumLiteral("ok", "http://hl7.org/fhir/response-code"), Description("OK")]
            Ok,
            /// <summary>
            /// Some internal unexpected error occurred - wait and try again. Note - this is usually used for things like database unavailable, which may be expected to resolve, though human intervention may be required.
            /// (system: http://hl7.org/fhir/response-code)
            /// </summary>
            [EnumLiteral("transient-error", "http://hl7.org/fhir/response-code"), Description("Transient Error")]
            TransientError,
            /// <summary>
            /// The message was rejected because of some content in it. There is no point in re-sending without change. The response narrative SHALL describe the issue.
            /// (system: http://hl7.org/fhir/response-code)
            /// </summary>
            [EnumLiteral("fatal-error", "http://hl7.org/fhir/response-code"), Description("Fatal Error")]
            FatalError,
        }

        [FhirType("ResponseComponent")]
        [DataContract]
        public partial class ResponseComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ResponseComponent"; } }
            
            /// <summary>
            /// Id of original message
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.Id _IdentifierElement;
            
            /// <summary>
            /// Id of original message
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if (value == null)
                        IdentifierElement = null; 
                    else
                        IdentifierElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MessageHeader.ResponseType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.MessageHeader.ResponseType> _CodeElement;
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MessageHeader.ResponseType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.MessageHeader.ResponseType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Specific list of hints/warnings/errors
            /// </summary>
            [FhirElement("details", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("OperationOutcome")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Details
            {
                get { return _Details; }
                set { _Details = value; OnPropertyChanged("Details"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Details;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResponseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.Id)IdentifierElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.MessageHeader.ResponseType>)CodeElement.DeepCopy();
                    if(Details != null) dest.Details = (Hl7.Fhir.Model.ResourceReference)Details.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ResponseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResponseComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Details, otherT.Details)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResponseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Details, otherT.Details)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentifierElement != null) yield return IdentifierElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (Details != null) yield return Details;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IdentifierElement != null) yield return new ElementValue("identifier", IdentifierElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Details != null) yield return new ElementValue("details", Details);
                }
            }

            
        }
        
        
        [FhirType("MessageSourceComponent")]
        [DataContract]
        public partial class MessageSourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "MessageSourceComponent"; } }
            
            /// <summary>
            /// Name of system
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of system
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
            /// Name of software running the system
            /// </summary>
            [FhirElement("software", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SoftwareElement
            {
                get { return _SoftwareElement; }
                set { _SoftwareElement = value; OnPropertyChanged("SoftwareElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SoftwareElement;
            
            /// <summary>
            /// Name of software running the system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Software
            {
                get { return SoftwareElement != null ? SoftwareElement.Value : null; }
                set
                {
                    if (value == null)
                        SoftwareElement = null; 
                    else
                        SoftwareElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Software");
                }
            }
            
            /// <summary>
            /// Version of software running
            /// </summary>
            [FhirElement("version", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version of software running
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
            /// Human contact for problems
            /// </summary>
            [FhirElement("contact", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.ContactPoint Contact
            {
                get { return _Contact; }
                set { _Contact = value; OnPropertyChanged("Contact"); }
            }
            
            private Hl7.Fhir.Model.ContactPoint _Contact;
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _EndpointElement;
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if (value == null)
                        EndpointElement = null; 
                    else
                        EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MessageSourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(SoftwareElement != null) dest.SoftwareElement = (Hl7.Fhir.Model.FhirString)SoftwareElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(Contact != null) dest.Contact = (Hl7.Fhir.Model.ContactPoint)Contact.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MessageSourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MessageSourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(SoftwareElement, otherT.SoftwareElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MessageSourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(SoftwareElement, otherT.SoftwareElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (SoftwareElement != null) yield return SoftwareElement;
                    if (VersionElement != null) yield return VersionElement;
                    if (Contact != null) yield return Contact;
                    if (EndpointElement != null) yield return EndpointElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (SoftwareElement != null) yield return new ElementValue("software", SoftwareElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                    if (Contact != null) yield return new ElementValue("contact", Contact);
                    if (EndpointElement != null) yield return new ElementValue("endpoint", EndpointElement);
                }
            }

            
        }
        
        
        [FhirType("MessageDestinationComponent")]
        [DataContract]
        public partial class MessageDestinationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "MessageDestinationComponent"; } }
            
            /// <summary>
            /// Name of system
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of system
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
            /// Particular delivery destination within the destination
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Device")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _EndpointElement;
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if (value == null)
                        EndpointElement = null; 
                    else
                        EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MessageDestinationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MessageDestinationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MessageDestinationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MessageDestinationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Target != null) yield return Target;
                    if (EndpointElement != null) yield return EndpointElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Target != null) yield return new ElementValue("target", Target);
                    if (EndpointElement != null) yield return new ElementValue("endpoint", EndpointElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Time that the message was sent
        /// </summary>
        [FhirElement("timestamp", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant TimestampElement
        {
            get { return _TimestampElement; }
            set { _TimestampElement = value; OnPropertyChanged("TimestampElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _TimestampElement;
        
        /// <summary>
        /// Time that the message was sent
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Timestamp
        {
            get { return TimestampElement != null ? TimestampElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TimestampElement = null; 
                else
                  TimestampElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Timestamp");
            }
        }
        
        /// <summary>
        /// Code for the event this message represents
        /// </summary>
        [FhirElement("event", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private Hl7.Fhir.Model.Coding _Event;
        
        /// <summary>
        /// If this is a reply to prior message
        /// </summary>
        [FhirElement("response", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.MessageHeader.ResponseComponent Response
        {
            get { return _Response; }
            set { _Response = value; OnPropertyChanged("Response"); }
        }
        
        private Hl7.Fhir.Model.MessageHeader.ResponseComponent _Response;
        
        /// <summary>
        /// Message Source Application
        /// </summary>
        [FhirElement("source", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.MessageHeader.MessageSourceComponent Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.MessageHeader.MessageSourceComponent _Source;
        
        /// <summary>
        /// Message Destination Application(s)
        /// </summary>
        [FhirElement("destination", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent> Destination
        {
            get { if(_Destination==null) _Destination = new List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent>(); return _Destination; }
            set { _Destination = value; OnPropertyChanged("Destination"); }
        }
        
        private List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent> _Destination;
        
        /// <summary>
        /// The source of the data entry
        /// </summary>
        [FhirElement("enterer", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// The source of the decision
        /// </summary>
        [FhirElement("author", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Intended "real-world" recipient for the data
        /// </summary>
        [FhirElement("receiver", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Practitioner","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Receiver
        {
            get { return _Receiver; }
            set { _Receiver = value; OnPropertyChanged("Receiver"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Receiver;
        
        /// <summary>
        /// Final responsibility for event
        /// </summary>
        [FhirElement("responsible", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Practitioner","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Responsible
        {
            get { return _Responsible; }
            set { _Responsible = value; OnPropertyChanged("Responsible"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Responsible;
        
        /// <summary>
        /// Cause of event
        /// </summary>
        [FhirElement("reason", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// The actual content of the message
        /// </summary>
        [FhirElement("data", InSummary=true, Order=190)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Data
        {
            get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.ResourceReference>(); return _Data; }
            set { _Data = value; OnPropertyChanged("Data"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Data;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MessageHeader;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TimestampElement != null) dest.TimestampElement = (Hl7.Fhir.Model.Instant)TimestampElement.DeepCopy();
                if(Event != null) dest.Event = (Hl7.Fhir.Model.Coding)Event.DeepCopy();
                if(Response != null) dest.Response = (Hl7.Fhir.Model.MessageHeader.ResponseComponent)Response.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.MessageHeader.MessageSourceComponent)Source.DeepCopy();
                if(Destination != null) dest.Destination = new List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent>(Destination.DeepCopy());
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Receiver != null) dest.Receiver = (Hl7.Fhir.Model.ResourceReference)Receiver.DeepCopy();
                if(Responsible != null) dest.Responsible = (Hl7.Fhir.Model.ResourceReference)Responsible.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                if(Data != null) dest.Data = new List<Hl7.Fhir.Model.ResourceReference>(Data.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MessageHeader());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MessageHeader;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TimestampElement, otherT.TimestampElement)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
            if( !DeepComparable.Matches(Responsible, otherT.Responsible)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Data, otherT.Data)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MessageHeader;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TimestampElement, otherT.TimestampElement)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
            if( !DeepComparable.IsExactly(Responsible, otherT.Responsible)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (TimestampElement != null) yield return TimestampElement;
				if (Event != null) yield return Event;
				if (Response != null) yield return Response;
				if (Source != null) yield return Source;
				foreach (var elem in Destination) { if (elem != null) yield return elem; }
				if (Enterer != null) yield return Enterer;
				if (Author != null) yield return Author;
				if (Receiver != null) yield return Receiver;
				if (Responsible != null) yield return Responsible;
				if (Reason != null) yield return Reason;
				foreach (var elem in Data) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TimestampElement != null) yield return new ElementValue("timestamp", TimestampElement);
                if (Event != null) yield return new ElementValue("event", Event);
                if (Response != null) yield return new ElementValue("response", Response);
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Destination) { if (elem != null) yield return new ElementValue("destination", elem); }
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (Author != null) yield return new ElementValue("author", Author);
                if (Receiver != null) yield return new ElementValue("receiver", Receiver);
                if (Responsible != null) yield return new ElementValue("responsible", Responsible);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                foreach (var elem in Data) { if (elem != null) yield return new ElementValue("data", elem); }
            }
        }

    }
    
}
