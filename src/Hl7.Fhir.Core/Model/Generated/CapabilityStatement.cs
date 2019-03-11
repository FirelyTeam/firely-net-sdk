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
    /// A statement of system capabilities
    /// </summary>
    [FhirType("CapabilityStatement", IsResource=true)]
    [DataContract]
    public partial class CapabilityStatement : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CapabilityStatement; } }
        [NotMapped]
        public override string TypeName { get { return "CapabilityStatement"; } }
        
        /// <summary>
        /// The mode of a RESTful capability statement.
        /// (url: http://hl7.org/fhir/ValueSet/restful-capability-mode)
        /// </summary>
        [FhirEnumeration("RestfulCapabilityMode")]
        public enum RestfulCapabilityMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-capability-mode)
            /// </summary>
            [EnumLiteral("client", "http://hl7.org/fhir/restful-capability-mode"), Description("Client")]
            Client,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-capability-mode)
            /// </summary>
            [EnumLiteral("server", "http://hl7.org/fhir/restful-capability-mode"), Description("Server")]
            Server,
        }

        /// <summary>
        /// Operations supported by REST at the type or instance level.
        /// (url: http://hl7.org/fhir/ValueSet/type-restful-interaction)
        /// </summary>
        [FhirEnumeration("TypeRestfulInteraction")]
        public enum TypeRestfulInteraction
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("read", "http://hl7.org/fhir/restful-interaction"), Description("read")]
            Read,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("vread", "http://hl7.org/fhir/restful-interaction"), Description("vread")]
            Vread,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("update", "http://hl7.org/fhir/restful-interaction"), Description("update")]
            Update,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("patch", "http://hl7.org/fhir/restful-interaction"), Description("patch")]
            Patch,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("delete", "http://hl7.org/fhir/restful-interaction"), Description("delete")]
            Delete,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("history-instance", "http://hl7.org/fhir/restful-interaction"), Description("history-instance")]
            HistoryInstance,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("history-type", "http://hl7.org/fhir/restful-interaction"), Description("history-type")]
            HistoryType,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("create", "http://hl7.org/fhir/restful-interaction"), Description("create")]
            Create,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("search-type", "http://hl7.org/fhir/restful-interaction"), Description("search-type")]
            SearchType,
        }

        /// <summary>
        /// How the system supports versioning for a resource.
        /// (url: http://hl7.org/fhir/ValueSet/versioning-policy)
        /// </summary>
        [FhirEnumeration("ResourceVersionPolicy")]
        public enum ResourceVersionPolicy
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/versioning-policy)
            /// </summary>
            [EnumLiteral("no-version", "http://hl7.org/fhir/versioning-policy"), Description("No VersionId Support")]
            NoVersion,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/versioning-policy)
            /// </summary>
            [EnumLiteral("versioned", "http://hl7.org/fhir/versioning-policy"), Description("Versioned")]
            Versioned,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/versioning-policy)
            /// </summary>
            [EnumLiteral("versioned-update", "http://hl7.org/fhir/versioning-policy"), Description("VersionId tracked fully")]
            VersionedUpdate,
        }

        /// <summary>
        /// A code that indicates how the server supports conditional read.
        /// (url: http://hl7.org/fhir/ValueSet/conditional-read-status)
        /// </summary>
        [FhirEnumeration("ConditionalReadStatus")]
        public enum ConditionalReadStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-read-status)
            /// </summary>
            [EnumLiteral("not-supported", "http://hl7.org/fhir/conditional-read-status"), Description("Not Supported")]
            NotSupported,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-read-status)
            /// </summary>
            [EnumLiteral("modified-since", "http://hl7.org/fhir/conditional-read-status"), Description("If-Modified-Since")]
            ModifiedSince,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-read-status)
            /// </summary>
            [EnumLiteral("not-match", "http://hl7.org/fhir/conditional-read-status"), Description("If-None-Match")]
            NotMatch,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-read-status)
            /// </summary>
            [EnumLiteral("full-support", "http://hl7.org/fhir/conditional-read-status"), Description("Full Support")]
            FullSupport,
        }

        /// <summary>
        /// A code that indicates how the server supports conditional delete.
        /// (url: http://hl7.org/fhir/ValueSet/conditional-delete-status)
        /// </summary>
        [FhirEnumeration("ConditionalDeleteStatus")]
        public enum ConditionalDeleteStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-delete-status)
            /// </summary>
            [EnumLiteral("not-supported", "http://hl7.org/fhir/conditional-delete-status"), Description("Not Supported")]
            NotSupported,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-delete-status)
            /// </summary>
            [EnumLiteral("single", "http://hl7.org/fhir/conditional-delete-status"), Description("Single Deletes Supported")]
            Single,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/conditional-delete-status)
            /// </summary>
            [EnumLiteral("multiple", "http://hl7.org/fhir/conditional-delete-status"), Description("Multiple Deletes Supported")]
            Multiple,
        }

        /// <summary>
        /// A set of flags that defines how references are supported.
        /// (url: http://hl7.org/fhir/ValueSet/reference-handling-policy)
        /// </summary>
        [FhirEnumeration("ReferenceHandlingPolicy")]
        public enum ReferenceHandlingPolicy
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-handling-policy)
            /// </summary>
            [EnumLiteral("literal", "http://hl7.org/fhir/reference-handling-policy"), Description("Literal References")]
            Literal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-handling-policy)
            /// </summary>
            [EnumLiteral("logical", "http://hl7.org/fhir/reference-handling-policy"), Description("Logical References")]
            Logical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-handling-policy)
            /// </summary>
            [EnumLiteral("resolves", "http://hl7.org/fhir/reference-handling-policy"), Description("Resolves References")]
            Resolves,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-handling-policy)
            /// </summary>
            [EnumLiteral("enforced", "http://hl7.org/fhir/reference-handling-policy"), Description("Reference Integrity Enforced")]
            Enforced,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-handling-policy)
            /// </summary>
            [EnumLiteral("local", "http://hl7.org/fhir/reference-handling-policy"), Description("Local References Only")]
            Local,
        }

        /// <summary>
        /// Operations supported by REST at the system level.
        /// (url: http://hl7.org/fhir/ValueSet/system-restful-interaction)
        /// </summary>
        [FhirEnumeration("SystemRestfulInteraction")]
        public enum SystemRestfulInteraction
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("transaction", "http://hl7.org/fhir/restful-interaction"), Description("transaction")]
            Transaction,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("batch", "http://hl7.org/fhir/restful-interaction"), Description("batch")]
            Batch,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("search-system", "http://hl7.org/fhir/restful-interaction"), Description("search-system")]
            SearchSystem,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/restful-interaction)
            /// </summary>
            [EnumLiteral("history-system", "http://hl7.org/fhir/restful-interaction"), Description("history-system")]
            HistorySystem,
        }

        /// <summary>
        /// The mode of a message capability statement.
        /// (url: http://hl7.org/fhir/ValueSet/event-capability-mode)
        /// </summary>
        [FhirEnumeration("EventCapabilityMode")]
        public enum EventCapabilityMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/event-capability-mode)
            /// </summary>
            [EnumLiteral("sender", "http://hl7.org/fhir/event-capability-mode"), Description("Sender")]
            Sender,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/event-capability-mode)
            /// </summary>
            [EnumLiteral("receiver", "http://hl7.org/fhir/event-capability-mode"), Description("Receiver")]
            Receiver,
        }

        /// <summary>
        /// Whether the application produces or consumes documents.
        /// (url: http://hl7.org/fhir/ValueSet/document-mode)
        /// </summary>
        [FhirEnumeration("DocumentMode")]
        public enum DocumentMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/document-mode)
            /// </summary>
            [EnumLiteral("producer", "http://hl7.org/fhir/document-mode"), Description("Producer")]
            Producer,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/document-mode)
            /// </summary>
            [EnumLiteral("consumer", "http://hl7.org/fhir/document-mode"), Description("Consumer")]
            Consumer,
        }

        [FhirType("SoftwareComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SoftwareComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SoftwareComponent"; } }
            
            /// <summary>
            /// A name the software is known by
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// A name the software is known by
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
            /// Version covered by this statement
            /// </summary>
            [FhirElement("version", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version covered by this statement
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
            /// Date this version was released
            /// </summary>
            [FhirElement("releaseDate", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ReleaseDateElement
            {
                get { return _ReleaseDateElement; }
                set { _ReleaseDateElement = value; OnPropertyChanged("ReleaseDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ReleaseDateElement;
            
            /// <summary>
            /// Date this version was released
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReleaseDate
            {
                get { return ReleaseDateElement != null ? ReleaseDateElement.Value : null; }
                set
                {
                    if (value == null)
                        ReleaseDateElement = null; 
                    else
                        ReleaseDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ReleaseDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SoftwareComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(ReleaseDateElement != null) dest.ReleaseDateElement = (Hl7.Fhir.Model.FhirDateTime)ReleaseDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SoftwareComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SoftwareComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(ReleaseDateElement, otherT.ReleaseDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SoftwareComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(ReleaseDateElement, otherT.ReleaseDateElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (VersionElement != null) yield return VersionElement;
                    if (ReleaseDateElement != null) yield return ReleaseDateElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                    if (ReleaseDateElement != null) yield return new ElementValue("releaseDate", ReleaseDateElement);
                }
            }

            
        }
        
        
        [FhirType("ImplementationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ImplementationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ImplementationComponent"; } }
            
            /// <summary>
            /// Describes this specific instance
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
            /// Describes this specific instance
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
            /// Base URL for the installation
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUrl UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUrl _UrlElement;
            
            /// <summary>
            /// Base URL for the installation
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
                        UrlElement = new Hl7.Fhir.Model.FhirUrl(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// Organization that manages the data
            /// </summary>
            [FhirElement("custodian", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Custodian
            {
                get { return _Custodian; }
                set { _Custodian = value; OnPropertyChanged("Custodian"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Custodian;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImplementationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUrl)UrlElement.DeepCopy();
                    if(Custodian != null) dest.Custodian = (Hl7.Fhir.Model.ResourceReference)Custodian.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImplementationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImplementationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(Custodian, otherT.Custodian)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImplementationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(Custodian, otherT.Custodian)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (UrlElement != null) yield return UrlElement;
                    if (Custodian != null) yield return Custodian;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                    if (Custodian != null) yield return new ElementValue("custodian", Custodian);
                }
            }

            
        }
        
        
        [FhirType("RestComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class RestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RestComponent"; } }
            
            /// <summary>
            /// client | server
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.RestfulCapabilityMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.RestfulCapabilityMode> _ModeElement;
            
            /// <summary>
            /// client | server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.RestfulCapabilityMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ModeElement = null; 
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.CapabilityStatement.RestfulCapabilityMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// General description of implementation
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            /// <summary>
            /// Information about security of implementation
            /// </summary>
            [FhirElement("security", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CapabilityStatement.SecurityComponent Security
            {
                get { return _Security; }
                set { _Security = value; OnPropertyChanged("Security"); }
            }
            
            private Hl7.Fhir.Model.CapabilityStatement.SecurityComponent _Security;
            
            /// <summary>
            /// Resource served on the REST interface
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.ResourceComponent> Resource
            {
                get { if(_Resource==null) _Resource = new List<Hl7.Fhir.Model.CapabilityStatement.ResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.ResourceComponent> _Resource;
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("interaction", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.SystemInteractionComponent> Interaction
            {
                get { if(_Interaction==null) _Interaction = new List<Hl7.Fhir.Model.CapabilityStatement.SystemInteractionComponent>(); return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.SystemInteractionComponent> _Interaction;
            
            /// <summary>
            /// Search parameters for searching all resources
            /// </summary>
            [FhirElement("searchParam", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent> SearchParam
            {
                get { if(_SearchParam==null) _SearchParam = new List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent>(); return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent> _SearchParam;
            
            /// <summary>
            /// Definition of a system level operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent> _Operation;
            
            /// <summary>
            /// Compartments served/used by system
            /// </summary>
            [FhirElement("compartment", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Canonical> CompartmentElement
            {
                get { if(_CompartmentElement==null) _CompartmentElement = new List<Hl7.Fhir.Model.Canonical>(); return _CompartmentElement; }
                set { _CompartmentElement = value; OnPropertyChanged("CompartmentElement"); }
            }
            
            private List<Hl7.Fhir.Model.Canonical> _CompartmentElement;
            
            /// <summary>
            /// Compartments served/used by system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Compartment
            {
                get { return CompartmentElement != null ? CompartmentElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        CompartmentElement = null; 
                    else
                        CompartmentElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                    OnPropertyChanged("Compartment");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.CapabilityStatement.RestfulCapabilityMode>)ModeElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    if(Security != null) dest.Security = (Hl7.Fhir.Model.CapabilityStatement.SecurityComponent)Security.DeepCopy();
                    if(Resource != null) dest.Resource = new List<Hl7.Fhir.Model.CapabilityStatement.ResourceComponent>(Resource.DeepCopy());
                    if(Interaction != null) dest.Interaction = new List<Hl7.Fhir.Model.CapabilityStatement.SystemInteractionComponent>(Interaction.DeepCopy());
                    if(SearchParam != null) dest.SearchParam = new List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent>(SearchParam.DeepCopy());
                    if(Operation != null) dest.Operation = new List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent>(Operation.DeepCopy());
                    if(CompartmentElement != null) dest.CompartmentElement = new List<Hl7.Fhir.Model.Canonical>(CompartmentElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(Security, otherT.Security)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.Matches(SearchParam, otherT.SearchParam)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(CompartmentElement, otherT.CompartmentElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(Security, otherT.Security)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.IsExactly(SearchParam, otherT.SearchParam)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(CompartmentElement, otherT.CompartmentElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (Documentation != null) yield return Documentation;
                    if (Security != null) yield return Security;
                    foreach (var elem in Resource) { if (elem != null) yield return elem; }
                    foreach (var elem in Interaction) { if (elem != null) yield return elem; }
                    foreach (var elem in SearchParam) { if (elem != null) yield return elem; }
                    foreach (var elem in Operation) { if (elem != null) yield return elem; }
                    foreach (var elem in CompartmentElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                    if (Security != null) yield return new ElementValue("security", Security);
                    foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
                    foreach (var elem in Interaction) { if (elem != null) yield return new ElementValue("interaction", elem); }
                    foreach (var elem in SearchParam) { if (elem != null) yield return new ElementValue("searchParam", elem); }
                    foreach (var elem in Operation) { if (elem != null) yield return new ElementValue("operation", elem); }
                    foreach (var elem in CompartmentElement) { if (elem != null) yield return new ElementValue("compartment", elem); }
                }
            }

            
        }
        
        
        [FhirType("SecurityComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SecurityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SecurityComponent"; } }
            
            /// <summary>
            /// Adds CORS Headers (http://enable-cors.org/)
            /// </summary>
            [FhirElement("cors", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CorsElement
            {
                get { return _CorsElement; }
                set { _CorsElement = value; OnPropertyChanged("CorsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _CorsElement;
            
            /// <summary>
            /// Adds CORS Headers (http://enable-cors.org/)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Cors
            {
                get { return CorsElement != null ? CorsElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CorsElement = null; 
                    else
                        CorsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Cors");
                }
            }
            
            /// <summary>
            /// OAuth | SMART-on-FHIR | NTLM | Basic | Kerberos | Certificates
            /// </summary>
            [FhirElement("service", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Service
            {
                get { if(_Service==null) _Service = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Service;
            
            /// <summary>
            /// General description of how security works
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CorsElement != null) dest.CorsElement = (Hl7.Fhir.Model.FhirBoolean)CorsElement.DeepCopy();
                    if(Service != null) dest.Service = new List<Hl7.Fhir.Model.CodeableConcept>(Service.DeepCopy());
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CorsElement, otherT.CorsElement)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CorsElement, otherT.CorsElement)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CorsElement != null) yield return CorsElement;
                    foreach (var elem in Service) { if (elem != null) yield return elem; }
                    if (Description != null) yield return Description;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CorsElement != null) yield return new ElementValue("cors", CorsElement);
                    foreach (var elem in Service) { if (elem != null) yield return new ElementValue("service", elem); }
                    if (Description != null) yield return new ElementValue("description", Description);
                }
            }

            
        }
        
        
        [FhirType("ResourceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ResourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceComponent"; } }
            
            /// <summary>
            /// A resource type that is supported
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResourceType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResourceType> _TypeElement;
            
            /// <summary>
            /// A resource type that is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResourceType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Base System profile for all uses of resource
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ProfileElement;
            
            /// <summary>
            /// Base System profile for all uses of resource
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
            /// Profiles for use cases supported
            /// </summary>
            [FhirElement("supportedProfile", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Canonical> SupportedProfileElement
            {
                get { if(_SupportedProfileElement==null) _SupportedProfileElement = new List<Hl7.Fhir.Model.Canonical>(); return _SupportedProfileElement; }
                set { _SupportedProfileElement = value; OnPropertyChanged("SupportedProfileElement"); }
            }
            
            private List<Hl7.Fhir.Model.Canonical> _SupportedProfileElement;
            
            /// <summary>
            /// Profiles for use cases supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> SupportedProfile
            {
                get { return SupportedProfileElement != null ? SupportedProfileElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SupportedProfileElement = null; 
                    else
                        SupportedProfileElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                    OnPropertyChanged("SupportedProfile");
                }
            }
            
            /// <summary>
            /// Additional information about the use of the resource type
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("interaction", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.ResourceInteractionComponent> Interaction
            {
                get { if(_Interaction==null) _Interaction = new List<Hl7.Fhir.Model.CapabilityStatement.ResourceInteractionComponent>(); return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.ResourceInteractionComponent> _Interaction;
            
            /// <summary>
            /// no-version | versioned | versioned-update
            /// </summary>
            [FhirElement("versioning", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.ResourceVersionPolicy> VersioningElement
            {
                get { return _VersioningElement; }
                set { _VersioningElement = value; OnPropertyChanged("VersioningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.ResourceVersionPolicy> _VersioningElement;
            
            /// <summary>
            /// no-version | versioned | versioned-update
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.ResourceVersionPolicy? Versioning
            {
                get { return VersioningElement != null ? VersioningElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        VersioningElement = null; 
                    else
                        VersioningElement = new Code<Hl7.Fhir.Model.CapabilityStatement.ResourceVersionPolicy>(value);
                    OnPropertyChanged("Versioning");
                }
            }
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            [FhirElement("readHistory", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReadHistoryElement
            {
                get { return _ReadHistoryElement; }
                set { _ReadHistoryElement = value; OnPropertyChanged("ReadHistoryElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ReadHistoryElement;
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ReadHistory
            {
                get { return ReadHistoryElement != null ? ReadHistoryElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ReadHistoryElement = null; 
                    else
                        ReadHistoryElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ReadHistory");
                }
            }
            
            /// <summary>
            /// If update can commit to a new identity
            /// </summary>
            [FhirElement("updateCreate", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean UpdateCreateElement
            {
                get { return _UpdateCreateElement; }
                set { _UpdateCreateElement = value; OnPropertyChanged("UpdateCreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _UpdateCreateElement;
            
            /// <summary>
            /// If update can commit to a new identity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? UpdateCreate
            {
                get { return UpdateCreateElement != null ? UpdateCreateElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        UpdateCreateElement = null; 
                    else
                        UpdateCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("UpdateCreate");
                }
            }
            
            /// <summary>
            /// If allows/uses conditional create
            /// </summary>
            [FhirElement("conditionalCreate", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ConditionalCreateElement
            {
                get { return _ConditionalCreateElement; }
                set { _ConditionalCreateElement = value; OnPropertyChanged("ConditionalCreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ConditionalCreateElement;
            
            /// <summary>
            /// If allows/uses conditional create
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ConditionalCreate
            {
                get { return ConditionalCreateElement != null ? ConditionalCreateElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ConditionalCreateElement = null; 
                    else
                        ConditionalCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalCreate");
                }
            }
            
            /// <summary>
            /// not-supported | modified-since | not-match | full-support
            /// </summary>
            [FhirElement("conditionalRead", Order=130)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalReadStatus> ConditionalReadElement
            {
                get { return _ConditionalReadElement; }
                set { _ConditionalReadElement = value; OnPropertyChanged("ConditionalReadElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalReadStatus> _ConditionalReadElement;
            
            /// <summary>
            /// not-supported | modified-since | not-match | full-support
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.ConditionalReadStatus? ConditionalRead
            {
                get { return ConditionalReadElement != null ? ConditionalReadElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ConditionalReadElement = null; 
                    else
                        ConditionalReadElement = new Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalReadStatus>(value);
                    OnPropertyChanged("ConditionalRead");
                }
            }
            
            /// <summary>
            /// If allows/uses conditional update
            /// </summary>
            [FhirElement("conditionalUpdate", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ConditionalUpdateElement
            {
                get { return _ConditionalUpdateElement; }
                set { _ConditionalUpdateElement = value; OnPropertyChanged("ConditionalUpdateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ConditionalUpdateElement;
            
            /// <summary>
            /// If allows/uses conditional update
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ConditionalUpdate
            {
                get { return ConditionalUpdateElement != null ? ConditionalUpdateElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ConditionalUpdateElement = null; 
                    else
                        ConditionalUpdateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalUpdate");
                }
            }
            
            /// <summary>
            /// not-supported | single | multiple - how conditional delete is supported
            /// </summary>
            [FhirElement("conditionalDelete", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalDeleteStatus> ConditionalDeleteElement
            {
                get { return _ConditionalDeleteElement; }
                set { _ConditionalDeleteElement = value; OnPropertyChanged("ConditionalDeleteElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalDeleteStatus> _ConditionalDeleteElement;
            
            /// <summary>
            /// not-supported | single | multiple - how conditional delete is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.ConditionalDeleteStatus? ConditionalDelete
            {
                get { return ConditionalDeleteElement != null ? ConditionalDeleteElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ConditionalDeleteElement = null; 
                    else
                        ConditionalDeleteElement = new Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalDeleteStatus>(value);
                    OnPropertyChanged("ConditionalDelete");
                }
            }
            
            /// <summary>
            /// literal | logical | resolves | enforced | local
            /// </summary>
            [FhirElement("referencePolicy", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy>> ReferencePolicyElement
            {
                get { if(_ReferencePolicyElement==null) _ReferencePolicyElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy>>(); return _ReferencePolicyElement; }
                set { _ReferencePolicyElement = value; OnPropertyChanged("ReferencePolicyElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy>> _ReferencePolicyElement;
            
            /// <summary>
            /// literal | logical | resolves | enforced | local
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy?> ReferencePolicy
            {
                get { return ReferencePolicyElement != null ? ReferencePolicyElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ReferencePolicyElement = null; 
                    else
                        ReferencePolicyElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy>(elem)));
                    OnPropertyChanged("ReferencePolicy");
                }
            }
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            [FhirElement("searchInclude", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SearchIncludeElement
            {
                get { if(_SearchIncludeElement==null) _SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _SearchIncludeElement; }
                set { _SearchIncludeElement = value; OnPropertyChanged("SearchIncludeElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _SearchIncludeElement;
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> SearchInclude
            {
                get { return SearchIncludeElement != null ? SearchIncludeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SearchIncludeElement = null; 
                    else
                        SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("SearchInclude");
                }
            }
            
            /// <summary>
            /// _revinclude values supported by the server
            /// </summary>
            [FhirElement("searchRevInclude", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SearchRevIncludeElement
            {
                get { if(_SearchRevIncludeElement==null) _SearchRevIncludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _SearchRevIncludeElement; }
                set { _SearchRevIncludeElement = value; OnPropertyChanged("SearchRevIncludeElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _SearchRevIncludeElement;
            
            /// <summary>
            /// _revinclude values supported by the server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> SearchRevInclude
            {
                get { return SearchRevIncludeElement != null ? SearchRevIncludeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SearchRevIncludeElement = null; 
                    else
                        SearchRevIncludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("SearchRevInclude");
                }
            }
            
            /// <summary>
            /// Search parameters supported by implementation
            /// </summary>
            [FhirElement("searchParam", Order=190)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent> SearchParam
            {
                get { if(_SearchParam==null) _SearchParam = new List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent>(); return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent> _SearchParam;
            
            /// <summary>
            /// Definition of a resource operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=200)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent> _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ResourceType>)TypeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                    if(SupportedProfileElement != null) dest.SupportedProfileElement = new List<Hl7.Fhir.Model.Canonical>(SupportedProfileElement.DeepCopy());
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    if(Interaction != null) dest.Interaction = new List<Hl7.Fhir.Model.CapabilityStatement.ResourceInteractionComponent>(Interaction.DeepCopy());
                    if(VersioningElement != null) dest.VersioningElement = (Code<Hl7.Fhir.Model.CapabilityStatement.ResourceVersionPolicy>)VersioningElement.DeepCopy();
                    if(ReadHistoryElement != null) dest.ReadHistoryElement = (Hl7.Fhir.Model.FhirBoolean)ReadHistoryElement.DeepCopy();
                    if(UpdateCreateElement != null) dest.UpdateCreateElement = (Hl7.Fhir.Model.FhirBoolean)UpdateCreateElement.DeepCopy();
                    if(ConditionalCreateElement != null) dest.ConditionalCreateElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalCreateElement.DeepCopy();
                    if(ConditionalReadElement != null) dest.ConditionalReadElement = (Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalReadStatus>)ConditionalReadElement.DeepCopy();
                    if(ConditionalUpdateElement != null) dest.ConditionalUpdateElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalUpdateElement.DeepCopy();
                    if(ConditionalDeleteElement != null) dest.ConditionalDeleteElement = (Code<Hl7.Fhir.Model.CapabilityStatement.ConditionalDeleteStatus>)ConditionalDeleteElement.DeepCopy();
                    if(ReferencePolicyElement != null) dest.ReferencePolicyElement = new List<Code<Hl7.Fhir.Model.CapabilityStatement.ReferenceHandlingPolicy>>(ReferencePolicyElement.DeepCopy());
                    if(SearchIncludeElement != null) dest.SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(SearchIncludeElement.DeepCopy());
                    if(SearchRevIncludeElement != null) dest.SearchRevIncludeElement = new List<Hl7.Fhir.Model.FhirString>(SearchRevIncludeElement.DeepCopy());
                    if(SearchParam != null) dest.SearchParam = new List<Hl7.Fhir.Model.CapabilityStatement.SearchParamComponent>(SearchParam.DeepCopy());
                    if(Operation != null) dest.Operation = new List<Hl7.Fhir.Model.CapabilityStatement.OperationComponent>(Operation.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ResourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.Matches(SupportedProfileElement, otherT.SupportedProfileElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.Matches(VersioningElement, otherT.VersioningElement)) return false;
                if( !DeepComparable.Matches(ReadHistoryElement, otherT.ReadHistoryElement)) return false;
                if( !DeepComparable.Matches(UpdateCreateElement, otherT.UpdateCreateElement)) return false;
                if( !DeepComparable.Matches(ConditionalCreateElement, otherT.ConditionalCreateElement)) return false;
                if( !DeepComparable.Matches(ConditionalReadElement, otherT.ConditionalReadElement)) return false;
                if( !DeepComparable.Matches(ConditionalUpdateElement, otherT.ConditionalUpdateElement)) return false;
                if( !DeepComparable.Matches(ConditionalDeleteElement, otherT.ConditionalDeleteElement)) return false;
                if( !DeepComparable.Matches(ReferencePolicyElement, otherT.ReferencePolicyElement)) return false;
                if( !DeepComparable.Matches(SearchIncludeElement, otherT.SearchIncludeElement)) return false;
                if( !DeepComparable.Matches(SearchRevIncludeElement, otherT.SearchRevIncludeElement)) return false;
                if( !DeepComparable.Matches(SearchParam, otherT.SearchParam)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.IsExactly(SupportedProfileElement, otherT.SupportedProfileElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.IsExactly(VersioningElement, otherT.VersioningElement)) return false;
                if( !DeepComparable.IsExactly(ReadHistoryElement, otherT.ReadHistoryElement)) return false;
                if( !DeepComparable.IsExactly(UpdateCreateElement, otherT.UpdateCreateElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalCreateElement, otherT.ConditionalCreateElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalReadElement, otherT.ConditionalReadElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalUpdateElement, otherT.ConditionalUpdateElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalDeleteElement, otherT.ConditionalDeleteElement)) return false;
                if( !DeepComparable.IsExactly(ReferencePolicyElement, otherT.ReferencePolicyElement)) return false;
                if( !DeepComparable.IsExactly(SearchIncludeElement, otherT.SearchIncludeElement)) return false;
                if( !DeepComparable.IsExactly(SearchRevIncludeElement, otherT.SearchRevIncludeElement)) return false;
                if( !DeepComparable.IsExactly(SearchParam, otherT.SearchParam)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (ProfileElement != null) yield return ProfileElement;
                    foreach (var elem in SupportedProfileElement) { if (elem != null) yield return elem; }
                    if (Documentation != null) yield return Documentation;
                    foreach (var elem in Interaction) { if (elem != null) yield return elem; }
                    if (VersioningElement != null) yield return VersioningElement;
                    if (ReadHistoryElement != null) yield return ReadHistoryElement;
                    if (UpdateCreateElement != null) yield return UpdateCreateElement;
                    if (ConditionalCreateElement != null) yield return ConditionalCreateElement;
                    if (ConditionalReadElement != null) yield return ConditionalReadElement;
                    if (ConditionalUpdateElement != null) yield return ConditionalUpdateElement;
                    if (ConditionalDeleteElement != null) yield return ConditionalDeleteElement;
                    foreach (var elem in ReferencePolicyElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SearchIncludeElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SearchRevIncludeElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SearchParam) { if (elem != null) yield return elem; }
                    foreach (var elem in Operation) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                    foreach (var elem in SupportedProfileElement) { if (elem != null) yield return new ElementValue("supportedProfile", elem); }
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                    foreach (var elem in Interaction) { if (elem != null) yield return new ElementValue("interaction", elem); }
                    if (VersioningElement != null) yield return new ElementValue("versioning", VersioningElement);
                    if (ReadHistoryElement != null) yield return new ElementValue("readHistory", ReadHistoryElement);
                    if (UpdateCreateElement != null) yield return new ElementValue("updateCreate", UpdateCreateElement);
                    if (ConditionalCreateElement != null) yield return new ElementValue("conditionalCreate", ConditionalCreateElement);
                    if (ConditionalReadElement != null) yield return new ElementValue("conditionalRead", ConditionalReadElement);
                    if (ConditionalUpdateElement != null) yield return new ElementValue("conditionalUpdate", ConditionalUpdateElement);
                    if (ConditionalDeleteElement != null) yield return new ElementValue("conditionalDelete", ConditionalDeleteElement);
                    foreach (var elem in ReferencePolicyElement) { if (elem != null) yield return new ElementValue("referencePolicy", elem); }
                    foreach (var elem in SearchIncludeElement) { if (elem != null) yield return new ElementValue("searchInclude", elem); }
                    foreach (var elem in SearchRevIncludeElement) { if (elem != null) yield return new ElementValue("searchRevInclude", elem); }
                    foreach (var elem in SearchParam) { if (elem != null) yield return new ElementValue("searchParam", elem); }
                    foreach (var elem in Operation) { if (elem != null) yield return new ElementValue("operation", elem); }
                }
            }

            
        }
        
        
        [FhirType("ResourceInteractionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ResourceInteractionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceInteractionComponent"; } }
            
            /// <summary>
            /// read | vread | update | patch | delete | history-instance | history-type | create | search-type
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction> _CodeElement;
            
            /// <summary>
            /// read | vread | update | patch | delete | history-instance | history-type | create | search-type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResourceInteractionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction>)CodeElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ResourceInteractionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResourceInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Documentation != null) yield return Documentation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                }
            }

            
        }
        
        
        [FhirType("SearchParamComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SearchParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SearchParamComponent"; } }
            
            /// <summary>
            /// Name of search parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of search parameter
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
            /// Source of definition for parameter
            /// </summary>
            [FhirElement("definition", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _DefinitionElement;
            
            /// <summary>
            /// Source of definition for parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if (value == null)
                        DefinitionElement = null; 
                    else
                        DefinitionElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity | uri | special
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SearchParamType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.SearchParamType> _TypeElement;
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity | uri | special
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SearchParamType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.SearchParamType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Server-specific usage
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SearchParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Canonical)DefinitionElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.SearchParamType>)TypeElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SearchParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SearchParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SearchParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DefinitionElement != null) yield return DefinitionElement;
                    if (TypeElement != null) yield return TypeElement;
                    if (Documentation != null) yield return Documentation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                }
            }

            
        }
        
        
        [FhirType("OperationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            /// <summary>
            /// Name by which the operation/query is invoked
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name by which the operation/query is invoked
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
            /// The defined operation/query
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _DefinitionElement;
            
            /// <summary>
            /// The defined operation/query
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if (value == null)
                        DefinitionElement = null; 
                    else
                        DefinitionElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// Specific details about operation behavior
            /// </summary>
            [FhirElement("documentation", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Canonical)DefinitionElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DefinitionElement != null) yield return DefinitionElement;
                    if (Documentation != null) yield return Documentation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                }
            }

            
        }
        
        
        [FhirType("SystemInteractionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SystemInteractionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SystemInteractionComponent"; } }
            
            /// <summary>
            /// transaction | batch | search-system | history-system
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.SystemRestfulInteraction> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.SystemRestfulInteraction> _CodeElement;
            
            /// <summary>
            /// transaction | batch | search-system | history-system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.SystemRestfulInteraction? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.CapabilityStatement.SystemRestfulInteraction>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SystemInteractionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.CapabilityStatement.SystemRestfulInteraction>)CodeElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SystemInteractionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SystemInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SystemInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Documentation != null) yield return Documentation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                }
            }

            
        }
        
        
        [FhirType("MessagingComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class MessagingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MessagingComponent"; } }
            
            /// <summary>
            /// Where messages should be sent
            /// </summary>
            [FhirElement("endpoint", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.EndpointComponent> Endpoint
            {
                get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.CapabilityStatement.EndpointComponent>(); return _Endpoint; }
                set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.EndpointComponent> _Endpoint;
            
            /// <summary>
            /// Reliable Message Cache Length (min)
            /// </summary>
            [FhirElement("reliableCache", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt ReliableCacheElement
            {
                get { return _ReliableCacheElement; }
                set { _ReliableCacheElement = value; OnPropertyChanged("ReliableCacheElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _ReliableCacheElement;
            
            /// <summary>
            /// Reliable Message Cache Length (min)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? ReliableCache
            {
                get { return ReliableCacheElement != null ? ReliableCacheElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ReliableCacheElement = null; 
                    else
                        ReliableCacheElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("ReliableCache");
                }
            }
            
            /// <summary>
            /// Messaging interface behavior details
            /// </summary>
            [FhirElement("documentation", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            /// <summary>
            /// Messages supported by this system
            /// </summary>
            [FhirElement("supportedMessage", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CapabilityStatement.SupportedMessageComponent> SupportedMessage
            {
                get { if(_SupportedMessage==null) _SupportedMessage = new List<Hl7.Fhir.Model.CapabilityStatement.SupportedMessageComponent>(); return _SupportedMessage; }
                set { _SupportedMessage = value; OnPropertyChanged("SupportedMessage"); }
            }
            
            private List<Hl7.Fhir.Model.CapabilityStatement.SupportedMessageComponent> _SupportedMessage;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MessagingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.CapabilityStatement.EndpointComponent>(Endpoint.DeepCopy());
                    if(ReliableCacheElement != null) dest.ReliableCacheElement = (Hl7.Fhir.Model.UnsignedInt)ReliableCacheElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    if(SupportedMessage != null) dest.SupportedMessage = new List<Hl7.Fhir.Model.CapabilityStatement.SupportedMessageComponent>(SupportedMessage.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MessagingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MessagingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.Matches(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(SupportedMessage, otherT.SupportedMessage)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MessagingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.IsExactly(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(SupportedMessage, otherT.SupportedMessage)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
                    if (ReliableCacheElement != null) yield return ReliableCacheElement;
                    if (Documentation != null) yield return Documentation;
                    foreach (var elem in SupportedMessage) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                    if (ReliableCacheElement != null) yield return new ElementValue("reliableCache", ReliableCacheElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                    foreach (var elem in SupportedMessage) { if (elem != null) yield return new ElementValue("supportedMessage", elem); }
                }
            }

            
        }
        
        
        [FhirType("EndpointComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class EndpointComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EndpointComponent"; } }
            
            /// <summary>
            /// http | ftp | mllp +
            /// </summary>
            [FhirElement("protocol", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Protocol
            {
                get { return _Protocol; }
                set { _Protocol = value; OnPropertyChanged("Protocol"); }
            }
            
            private Hl7.Fhir.Model.Coding _Protocol;
            
            /// <summary>
            /// Network address or identifier of the end-point
            /// </summary>
            [FhirElement("address", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUrl AddressElement
            {
                get { return _AddressElement; }
                set { _AddressElement = value; OnPropertyChanged("AddressElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUrl _AddressElement;
            
            /// <summary>
            /// Network address or identifier of the end-point
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Address
            {
                get { return AddressElement != null ? AddressElement.Value : null; }
                set
                {
                    if (value == null)
                        AddressElement = null; 
                    else
                        AddressElement = new Hl7.Fhir.Model.FhirUrl(value);
                    OnPropertyChanged("Address");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EndpointComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Protocol != null) dest.Protocol = (Hl7.Fhir.Model.Coding)Protocol.DeepCopy();
                    if(AddressElement != null) dest.AddressElement = (Hl7.Fhir.Model.FhirUrl)AddressElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EndpointComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EndpointComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Protocol, otherT.Protocol)) return false;
                if( !DeepComparable.Matches(AddressElement, otherT.AddressElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EndpointComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Protocol, otherT.Protocol)) return false;
                if( !DeepComparable.IsExactly(AddressElement, otherT.AddressElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Protocol != null) yield return Protocol;
                    if (AddressElement != null) yield return AddressElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Protocol != null) yield return new ElementValue("protocol", Protocol);
                    if (AddressElement != null) yield return new ElementValue("address", AddressElement);
                }
            }

            
        }
        
        
        [FhirType("SupportedMessageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SupportedMessageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SupportedMessageComponent"; } }
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.EventCapabilityMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.EventCapabilityMode> _ModeElement;
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.EventCapabilityMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ModeElement = null; 
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.CapabilityStatement.EventCapabilityMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Message supported by this system
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _DefinitionElement;
            
            /// <summary>
            /// Message supported by this system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if (value == null)
                        DefinitionElement = null; 
                    else
                        DefinitionElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SupportedMessageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.CapabilityStatement.EventCapabilityMode>)ModeElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Canonical)DefinitionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SupportedMessageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SupportedMessageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SupportedMessageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (DefinitionElement != null) yield return DefinitionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                }
            }

            
        }
        
        
        [FhirType("DocumentComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class DocumentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DocumentComponent"; } }
            
            /// <summary>
            /// producer | consumer
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CapabilityStatement.DocumentMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CapabilityStatement.DocumentMode> _ModeElement;
            
            /// <summary>
            /// producer | consumer
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CapabilityStatement.DocumentMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ModeElement = null; 
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.CapabilityStatement.DocumentMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Description of document support
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Documentation
            {
                get { return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Documentation;
            
            /// <summary>
            /// Constraint on the resources used in the document
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ProfileElement;
            
            /// <summary>
            /// Constraint on the resources used in the document
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DocumentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.CapabilityStatement.DocumentMode>)ModeElement.DeepCopy();
                    if(Documentation != null) dest.Documentation = (Hl7.Fhir.Model.Markdown)Documentation.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DocumentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DocumentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DocumentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (Documentation != null) yield return Documentation;
                    if (ProfileElement != null) yield return ProfileElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (Documentation != null) yield return new ElementValue("documentation", Documentation);
                    if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this capability statement, represented as a URI (globally unique)
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
        /// Canonical identifier for this capability statement, represented as a URI (globally unique)
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
        /// Business version of the capability statement
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the capability statement
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
        /// Name for this capability statement (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this capability statement (computer friendly)
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
        /// Name for this capability statement (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this capability statement (human friendly)
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
        [FhirElement("status", InSummary=true, Order=130)]
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
        [FhirElement("experimental", InSummary=true, Order=140)]
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
        [FhirElement("date", InSummary=true, Order=150)]
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
        [FhirElement("publisher", InSummary=true, Order=160)]
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
        [FhirElement("contact", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the capability statement
        /// </summary>
        [FhirElement("description", Order=180)]
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
        [FhirElement("useContext", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for capability statement (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this capability statement is defined
        /// </summary>
        [FhirElement("purpose", Order=210)]
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
        [FhirElement("copyright", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// instance | capability | requirements
        /// </summary>
        [FhirElement("kind", InSummary=true, Order=230)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CapabilityStatementKind> KindElement
        {
            get { return _KindElement; }
            set { _KindElement = value; OnPropertyChanged("KindElement"); }
        }
        
        private Code<Hl7.Fhir.Model.CapabilityStatementKind> _KindElement;
        
        /// <summary>
        /// instance | capability | requirements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CapabilityStatementKind? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  KindElement = null; 
                else
                  KindElement = new Code<Hl7.Fhir.Model.CapabilityStatementKind>(value);
                OnPropertyChanged("Kind");
            }
        }
        
        /// <summary>
        /// Canonical URL of another capability statement this implements
        /// </summary>
        [FhirElement("instantiates", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> InstantiatesElement
        {
            get { if(_InstantiatesElement==null) _InstantiatesElement = new List<Hl7.Fhir.Model.Canonical>(); return _InstantiatesElement; }
            set { _InstantiatesElement = value; OnPropertyChanged("InstantiatesElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _InstantiatesElement;
        
        /// <summary>
        /// Canonical URL of another capability statement this implements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Instantiates
        {
            get { return InstantiatesElement != null ? InstantiatesElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  InstantiatesElement = null; 
                else
                  InstantiatesElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Instantiates");
            }
        }
        
        /// <summary>
        /// Canonical URL of another capability statement this adds to
        /// </summary>
        [FhirElement("imports", InSummary=true, Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> ImportsElement
        {
            get { if(_ImportsElement==null) _ImportsElement = new List<Hl7.Fhir.Model.Canonical>(); return _ImportsElement; }
            set { _ImportsElement = value; OnPropertyChanged("ImportsElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _ImportsElement;
        
        /// <summary>
        /// Canonical URL of another capability statement this adds to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Imports
        {
            get { return ImportsElement != null ? ImportsElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ImportsElement = null; 
                else
                  ImportsElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Imports");
            }
        }
        
        /// <summary>
        /// Software that is covered by this capability statement
        /// </summary>
        [FhirElement("software", InSummary=true, Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.CapabilityStatement.SoftwareComponent Software
        {
            get { return _Software; }
            set { _Software = value; OnPropertyChanged("Software"); }
        }
        
        private Hl7.Fhir.Model.CapabilityStatement.SoftwareComponent _Software;
        
        /// <summary>
        /// If this describes a specific instance
        /// </summary>
        [FhirElement("implementation", InSummary=true, Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.CapabilityStatement.ImplementationComponent Implementation
        {
            get { return _Implementation; }
            set { _Implementation = value; OnPropertyChanged("Implementation"); }
        }
        
        private Hl7.Fhir.Model.CapabilityStatement.ImplementationComponent _Implementation;
        
        /// <summary>
        /// FHIR Version the system supports
        /// </summary>
        [FhirElement("fhirVersion", InSummary=true, Order=280)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FHIRVersion> FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FHIRVersion> _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version the system supports
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FHIRVersion? FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  FhirVersionElement = null; 
                else
                  FhirVersionElement = new Code<Hl7.Fhir.Model.FHIRVersion>(value);
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// formats supported (xml | json | ttl | mime type)
        /// </summary>
        [FhirElement("format", InSummary=true, Order=290)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Code> FormatElement
        {
            get { if(_FormatElement==null) _FormatElement = new List<Hl7.Fhir.Model.Code>(); return _FormatElement; }
            set { _FormatElement = value; OnPropertyChanged("FormatElement"); }
        }
        
        private List<Hl7.Fhir.Model.Code> _FormatElement;
        
        /// <summary>
        /// formats supported (xml | json | ttl | mime type)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Format
        {
            get { return FormatElement != null ? FormatElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  FormatElement = null; 
                else
                  FormatElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                OnPropertyChanged("Format");
            }
        }
        
        /// <summary>
        /// Patch formats supported
        /// </summary>
        [FhirElement("patchFormat", InSummary=true, Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Code> PatchFormatElement
        {
            get { if(_PatchFormatElement==null) _PatchFormatElement = new List<Hl7.Fhir.Model.Code>(); return _PatchFormatElement; }
            set { _PatchFormatElement = value; OnPropertyChanged("PatchFormatElement"); }
        }
        
        private List<Hl7.Fhir.Model.Code> _PatchFormatElement;
        
        /// <summary>
        /// Patch formats supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> PatchFormat
        {
            get { return PatchFormatElement != null ? PatchFormatElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  PatchFormatElement = null; 
                else
                  PatchFormatElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                OnPropertyChanged("PatchFormat");
            }
        }
        
        /// <summary>
        /// Implementation guides supported
        /// </summary>
        [FhirElement("implementationGuide", InSummary=true, Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> ImplementationGuideElement
        {
            get { if(_ImplementationGuideElement==null) _ImplementationGuideElement = new List<Hl7.Fhir.Model.Canonical>(); return _ImplementationGuideElement; }
            set { _ImplementationGuideElement = value; OnPropertyChanged("ImplementationGuideElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _ImplementationGuideElement;
        
        /// <summary>
        /// Implementation guides supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> ImplementationGuide
        {
            get { return ImplementationGuideElement != null ? ImplementationGuideElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ImplementationGuideElement = null; 
                else
                  ImplementationGuideElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("ImplementationGuide");
            }
        }
        
        /// <summary>
        /// If the endpoint is a RESTful one
        /// </summary>
        [FhirElement("rest", InSummary=true, Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CapabilityStatement.RestComponent> Rest
        {
            get { if(_Rest==null) _Rest = new List<Hl7.Fhir.Model.CapabilityStatement.RestComponent>(); return _Rest; }
            set { _Rest = value; OnPropertyChanged("Rest"); }
        }
        
        private List<Hl7.Fhir.Model.CapabilityStatement.RestComponent> _Rest;
        
        /// <summary>
        /// If messaging is supported
        /// </summary>
        [FhirElement("messaging", InSummary=true, Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CapabilityStatement.MessagingComponent> Messaging
        {
            get { if(_Messaging==null) _Messaging = new List<Hl7.Fhir.Model.CapabilityStatement.MessagingComponent>(); return _Messaging; }
            set { _Messaging = value; OnPropertyChanged("Messaging"); }
        }
        
        private List<Hl7.Fhir.Model.CapabilityStatement.MessagingComponent> _Messaging;
        
        /// <summary>
        /// Document definition
        /// </summary>
        [FhirElement("document", InSummary=true, Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CapabilityStatement.DocumentComponent> Document
        {
            get { if(_Document==null) _Document = new List<Hl7.Fhir.Model.CapabilityStatement.DocumentComponent>(); return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private List<Hl7.Fhir.Model.CapabilityStatement.DocumentComponent> _Document;
        

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_7 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "document.select(profile&mode).isDistinct()",
            Key = "cpb-7",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "The set of documents must be unique by the combination of profile and mode.",
            Xpath = "count(f:document[f:mode/@value='producer'])=count(distinct-values(f:document[f:mode/@value='producer']/f:profile/f:reference/@value)) and count(f:document[f:mode/@value='consumer'])=count(distinct-values(f:document[f:mode/@value='consumer']/f:profile/f:reference/@value))"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_16 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(kind!='requirements') or (implementation.exists().not() and software.exists().not())",
            Key = "cpb-16",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If kind = requirements, implementation and software must be absent",
            Xpath = "not(f:kind/@value='instance') or (not(exists(f:implementation)) and not(exists(f:software)))"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_15 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(kind != 'capability') or (implementation.exists().not() and software.exists())",
            Key = "cpb-15",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If kind = capability, implementation must be absent, software must be present",
            Xpath = " not(f:kind/@value='instance') or (not(exists(f:implementation)) and exists(f:software))"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "messaging.endpoint.empty() or kind = 'instance'",
            Key = "cpb-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Messaging end-point is required (and is only permitted) when a statement is for an implementation.",
            Xpath = "not(exists(f:messaging/f:endpoint)) or f:kind/@value = 'instance'"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_14 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(kind != 'instance') or implementation.exists()",
            Key = "cpb-14",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If kind = instance, implementation must be present and software may be present",
            Xpath = "not(f:kind/@value='instance') or exists(f:implementation)"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(description.count() + software.count() + implementation.count()) > 0",
            Key = "cpb-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "A Capability Statement SHALL have at least one of description, software, or implementation element.",
            Xpath = "count(f:software | f:implementation | f:description) > 0"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "rest.exists() or messaging.exists() or document.exists()",
            Key = "cpb-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "A Capability Statement SHALL have at least one of REST, messaging or document element.",
            Xpath = "exists(f:rest) or exists(f:messaging) or exists(f:document)"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_0 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "cpb-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_9 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "rest.all(resource.select(type).isDistinct())",
            Key = "cpb-9",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "A given resource can only be described once per RESTful mode.",
            Xpath = "count(f:resource)=count(distinct-values(f:resource/f:type/@value))"
        };

        public static ElementDefinition.ConstraintComponent CapabilityStatement_CPB_12 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "rest.resource.all(searchParam.select(name).isDistinct())",
            Key = "cpb-12",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Search parameter names must be unique in the context of a resource.",
            Xpath = "count(f:searchParam)=count(distinct-values(f:searchParam/f:name/@value))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(CapabilityStatement_CPB_7);
            InvariantConstraints.Add(CapabilityStatement_CPB_16);
            InvariantConstraints.Add(CapabilityStatement_CPB_15);
            InvariantConstraints.Add(CapabilityStatement_CPB_3);
            InvariantConstraints.Add(CapabilityStatement_CPB_14);
            InvariantConstraints.Add(CapabilityStatement_CPB_2);
            InvariantConstraints.Add(CapabilityStatement_CPB_1);
            InvariantConstraints.Add(CapabilityStatement_CPB_0);
            InvariantConstraints.Add(CapabilityStatement_CPB_9);
            InvariantConstraints.Add(CapabilityStatement_CPB_12);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CapabilityStatement;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
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
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.CapabilityStatementKind>)KindElement.DeepCopy();
                if(InstantiatesElement != null) dest.InstantiatesElement = new List<Hl7.Fhir.Model.Canonical>(InstantiatesElement.DeepCopy());
                if(ImportsElement != null) dest.ImportsElement = new List<Hl7.Fhir.Model.Canonical>(ImportsElement.DeepCopy());
                if(Software != null) dest.Software = (Hl7.Fhir.Model.CapabilityStatement.SoftwareComponent)Software.DeepCopy();
                if(Implementation != null) dest.Implementation = (Hl7.Fhir.Model.CapabilityStatement.ImplementationComponent)Implementation.DeepCopy();
                if(FhirVersionElement != null) dest.FhirVersionElement = (Code<Hl7.Fhir.Model.FHIRVersion>)FhirVersionElement.DeepCopy();
                if(FormatElement != null) dest.FormatElement = new List<Hl7.Fhir.Model.Code>(FormatElement.DeepCopy());
                if(PatchFormatElement != null) dest.PatchFormatElement = new List<Hl7.Fhir.Model.Code>(PatchFormatElement.DeepCopy());
                if(ImplementationGuideElement != null) dest.ImplementationGuideElement = new List<Hl7.Fhir.Model.Canonical>(ImplementationGuideElement.DeepCopy());
                if(Rest != null) dest.Rest = new List<Hl7.Fhir.Model.CapabilityStatement.RestComponent>(Rest.DeepCopy());
                if(Messaging != null) dest.Messaging = new List<Hl7.Fhir.Model.CapabilityStatement.MessagingComponent>(Messaging.DeepCopy());
                if(Document != null) dest.Document = new List<Hl7.Fhir.Model.CapabilityStatement.DocumentComponent>(Document.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new CapabilityStatement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CapabilityStatement;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
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
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(InstantiatesElement, otherT.InstantiatesElement)) return false;
            if( !DeepComparable.Matches(ImportsElement, otherT.ImportsElement)) return false;
            if( !DeepComparable.Matches(Software, otherT.Software)) return false;
            if( !DeepComparable.Matches(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(FormatElement, otherT.FormatElement)) return false;
            if( !DeepComparable.Matches(PatchFormatElement, otherT.PatchFormatElement)) return false;
            if( !DeepComparable.Matches(ImplementationGuideElement, otherT.ImplementationGuideElement)) return false;
            if( !DeepComparable.Matches(Rest, otherT.Rest)) return false;
            if( !DeepComparable.Matches(Messaging, otherT.Messaging)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CapabilityStatement;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
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
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesElement, otherT.InstantiatesElement)) return false;
            if( !DeepComparable.IsExactly(ImportsElement, otherT.ImportsElement)) return false;
            if( !DeepComparable.IsExactly(Software, otherT.Software)) return false;
            if( !DeepComparable.IsExactly(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(FormatElement, otherT.FormatElement)) return false;
            if( !DeepComparable.IsExactly(PatchFormatElement, otherT.PatchFormatElement)) return false;
            if( !DeepComparable.IsExactly(ImplementationGuideElement, otherT.ImplementationGuideElement)) return false;
            if( !DeepComparable.IsExactly(Rest, otherT.Rest)) return false;
            if( !DeepComparable.IsExactly(Messaging, otherT.Messaging)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
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
				if (KindElement != null) yield return KindElement;
				foreach (var elem in InstantiatesElement) { if (elem != null) yield return elem; }
				foreach (var elem in ImportsElement) { if (elem != null) yield return elem; }
				if (Software != null) yield return Software;
				if (Implementation != null) yield return Implementation;
				if (FhirVersionElement != null) yield return FhirVersionElement;
				foreach (var elem in FormatElement) { if (elem != null) yield return elem; }
				foreach (var elem in PatchFormatElement) { if (elem != null) yield return elem; }
				foreach (var elem in ImplementationGuideElement) { if (elem != null) yield return elem; }
				foreach (var elem in Rest) { if (elem != null) yield return elem; }
				foreach (var elem in Messaging) { if (elem != null) yield return elem; }
				foreach (var elem in Document) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
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
                if (KindElement != null) yield return new ElementValue("kind", KindElement);
                foreach (var elem in InstantiatesElement) { if (elem != null) yield return new ElementValue("instantiates", elem); }
                foreach (var elem in ImportsElement) { if (elem != null) yield return new ElementValue("imports", elem); }
                if (Software != null) yield return new ElementValue("software", Software);
                if (Implementation != null) yield return new ElementValue("implementation", Implementation);
                if (FhirVersionElement != null) yield return new ElementValue("fhirVersion", FhirVersionElement);
                foreach (var elem in FormatElement) { if (elem != null) yield return new ElementValue("format", elem); }
                foreach (var elem in PatchFormatElement) { if (elem != null) yield return new ElementValue("patchFormat", elem); }
                foreach (var elem in ImplementationGuideElement) { if (elem != null) yield return new ElementValue("implementationGuide", elem); }
                foreach (var elem in Rest) { if (elem != null) yield return new ElementValue("rest", elem); }
                foreach (var elem in Messaging) { if (elem != null) yield return new ElementValue("messaging", elem); }
                foreach (var elem in Document) { if (elem != null) yield return new ElementValue("document", elem); }
            }
        }

    }
    
}
