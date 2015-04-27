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
    /// Contains a collection of resources
    /// </summary>
    [FhirType("Bundle", IsResource=true)]
    [DataContract]
    public partial class Bundle : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Bundle; } }
        [NotMapped]
        public override string TypeName { get { return "Bundle"; } }
        
        /// <summary>
        /// HTTP verbs (in the HTTP command line)
        /// </summary>
        [FhirEnumeration("HTTPVerb")]
        public enum HTTPVerb
        {
            /// <summary>
            /// HTTP GET.
            /// </summary>
            [EnumLiteral("GET")]
            GET,
            /// <summary>
            /// HTTP POST.
            /// </summary>
            [EnumLiteral("POST")]
            POST,
            /// <summary>
            /// HTTP PUT.
            /// </summary>
            [EnumLiteral("PUT")]
            PUT,
            /// <summary>
            /// HTTP DELETE.
            /// </summary>
            [EnumLiteral("DELETE")]
            DELETE,
        }
        
        /// <summary>
        /// Defines how a match URL should be used when processing a transaction
        /// </summary>
        [FhirEnumeration("TransactionProcessingMode")]
        public enum TransactionProcessingMode
        {
            /// <summary>
            /// If a matching resource is found, then ignore the resource and do not process it as part of the transaction (and return the matching resource information in the transaction response).
            /// </summary>
            [EnumLiteral("ignore")]
            Ignore,
            /// <summary>
            /// If a matching resource is found, then update it with this resource, otherwise create a new one.
            /// </summary>
            [EnumLiteral("update")]
            Update,
            /// <summary>
            /// If a resource matching the information provided is found, delete it. If no matching resource is found, the transaction fails.
            /// </summary>
            [EnumLiteral("delete")]
            Delete,
        }
        
        /// <summary>
        /// Why an entry is in the result set - whether it's included as a match or because of an _include requirement
        /// </summary>
        [FhirEnumeration("SearchEntryMode")]
        public enum SearchEntryMode
        {
            /// <summary>
            /// This resource matched the search specification.
            /// </summary>
            [EnumLiteral("match")]
            Match,
            /// <summary>
            /// This resource is returned because it is referred to from another resource in the search set.
            /// </summary>
            [EnumLiteral("include")]
            Include,
        }
        
        /// <summary>
        /// Indicates the purpose of a bundle- how it was intended to be used
        /// </summary>
        [FhirEnumeration("BundleType")]
        public enum BundleType
        {
            /// <summary>
            /// The bundle is a document. The first resource is a Composition.
            /// </summary>
            [EnumLiteral("document")]
            Document,
            /// <summary>
            /// The bundle is a message. The first resource is a MessageHeader.
            /// </summary>
            [EnumLiteral("message")]
            Message,
            /// <summary>
            /// The bundle is a transaction - intended to be processed by a server as an atomic commit.
            /// </summary>
            [EnumLiteral("transaction")]
            Transaction,
            /// <summary>
            /// The bundle is a transaction response.
            /// </summary>
            [EnumLiteral("transaction-response")]
            TransactionResponse,
            /// <summary>
            /// The bundle is a list of resources from a _history interaction on a server.
            /// </summary>
            [EnumLiteral("history")]
            History,
            /// <summary>
            /// The bundle is a list of resources returned as a result of a search/query interaction, operation, or message.
            /// </summary>
            [EnumLiteral("searchset")]
            Searchset,
            /// <summary>
            /// The bundle is a set of resources collected into a single document for ease of distribution.
            /// </summary>
            [EnumLiteral("collection")]
            Collection,
        }
        
        /// <summary>
        /// The operation associated with an entry in either a update history, or a transaction/ transaction response
        /// </summary>
        [FhirEnumeration("TransactionOperation")]
        public enum TransactionOperation
        {
            [EnumLiteral("create")]
            Create,
            [EnumLiteral("update")]
            Update,
            [EnumLiteral("delete")]
            Delete,
            /// <summary>
            /// No Operation occurred - only valid in a transaction response.
            /// </summary>
            [EnumLiteral("noop")]
            Noop,
        }
        
        [FhirType("BundleEntryTransactionComponent")]
        [DataContract]
        public partial class BundleEntryTransactionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BundleEntryTransactionComponent"; } }
            
            /// <summary>
            /// GET | POST | PUT | DELETE
            /// </summary>
            [FhirElement("method", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Bundle.HTTPVerb> MethodElement
            {
                get { return _MethodElement; }
                set { _MethodElement = value; OnPropertyChanged("MethodElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Bundle.HTTPVerb> _MethodElement;
            
            /// <summary>
            /// GET | POST | PUT | DELETE
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Bundle.HTTPVerb? Method
            {
                get { return MethodElement != null ? MethodElement.Value : null; }
                set
                {
                    if(value == null)
                      MethodElement = null; 
                    else
                      MethodElement = new Code<Hl7.Fhir.Model.Bundle.HTTPVerb>(value);
                    OnPropertyChanged("Method");
                }
            }
            
            /// <summary>
            /// The URL for the transaction
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// The URL for the transaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// For managing cache currency
            /// </summary>
            [FhirElement("ifNoneMatch", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IfNoneMatchElement
            {
                get { return _IfNoneMatchElement; }
                set { _IfNoneMatchElement = value; OnPropertyChanged("IfNoneMatchElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IfNoneMatchElement;
            
            /// <summary>
            /// For managing cache currency
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IfNoneMatch
            {
                get { return IfNoneMatchElement != null ? IfNoneMatchElement.Value : null; }
                set
                {
                    if(value == null)
                      IfNoneMatchElement = null; 
                    else
                      IfNoneMatchElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IfNoneMatch");
                }
            }
            
            /// <summary>
            /// For managing update contention
            /// </summary>
            [FhirElement("ifMatch", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IfMatchElement
            {
                get { return _IfMatchElement; }
                set { _IfMatchElement = value; OnPropertyChanged("IfMatchElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IfMatchElement;
            
            /// <summary>
            /// For managing update contention
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IfMatch
            {
                get { return IfMatchElement != null ? IfMatchElement.Value : null; }
                set
                {
                    if(value == null)
                      IfMatchElement = null; 
                    else
                      IfMatchElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IfMatch");
                }
            }
            
            /// <summary>
            /// For managing update contention
            /// </summary>
            [FhirElement("ifModifiedSince", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Instant IfModifiedSinceElement
            {
                get { return _IfModifiedSinceElement; }
                set { _IfModifiedSinceElement = value; OnPropertyChanged("IfModifiedSinceElement"); }
            }
            
            private Hl7.Fhir.Model.Instant _IfModifiedSinceElement;
            
            /// <summary>
            /// For managing update contention
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? IfModifiedSince
            {
                get { return IfModifiedSinceElement != null ? IfModifiedSinceElement.Value : null; }
                set
                {
                    if(value == null)
                      IfModifiedSinceElement = null; 
                    else
                      IfModifiedSinceElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("IfModifiedSince");
                }
            }
            
            /// <summary>
            /// For conditional creates
            /// </summary>
            [FhirElement("ifNoneExist", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IfNoneExistElement
            {
                get { return _IfNoneExistElement; }
                set { _IfNoneExistElement = value; OnPropertyChanged("IfNoneExistElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IfNoneExistElement;
            
            /// <summary>
            /// For conditional creates
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IfNoneExist
            {
                get { return IfNoneExistElement != null ? IfNoneExistElement.Value : null; }
                set
                {
                    if(value == null)
                      IfNoneExistElement = null; 
                    else
                      IfNoneExistElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IfNoneExist");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleEntryTransactionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(MethodElement != null) dest.MethodElement = (Code<Hl7.Fhir.Model.Bundle.HTTPVerb>)MethodElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(IfNoneMatchElement != null) dest.IfNoneMatchElement = (Hl7.Fhir.Model.FhirString)IfNoneMatchElement.DeepCopy();
                    if(IfMatchElement != null) dest.IfMatchElement = (Hl7.Fhir.Model.FhirString)IfMatchElement.DeepCopy();
                    if(IfModifiedSinceElement != null) dest.IfModifiedSinceElement = (Hl7.Fhir.Model.Instant)IfModifiedSinceElement.DeepCopy();
                    if(IfNoneExistElement != null) dest.IfNoneExistElement = (Hl7.Fhir.Model.FhirString)IfNoneExistElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BundleEntryTransactionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BundleEntryTransactionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MethodElement, otherT.MethodElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(IfNoneMatchElement, otherT.IfNoneMatchElement)) return false;
                if( !DeepComparable.Matches(IfMatchElement, otherT.IfMatchElement)) return false;
                if( !DeepComparable.Matches(IfModifiedSinceElement, otherT.IfModifiedSinceElement)) return false;
                if( !DeepComparable.Matches(IfNoneExistElement, otherT.IfNoneExistElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleEntryTransactionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(MethodElement, otherT.MethodElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(IfNoneMatchElement, otherT.IfNoneMatchElement)) return false;
                if( !DeepComparable.IsExactly(IfMatchElement, otherT.IfMatchElement)) return false;
                if( !DeepComparable.IsExactly(IfModifiedSinceElement, otherT.IfModifiedSinceElement)) return false;
                if( !DeepComparable.IsExactly(IfNoneExistElement, otherT.IfNoneExistElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("BundleEntryTransactionResponseComponent")]
        [DataContract]
        public partial class BundleEntryTransactionResponseComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BundleEntryTransactionResponseComponent"; } }
            
            /// <summary>
            /// Status return code for entry
            /// </summary>
            [FhirElement("status", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _StatusElement;
            
            /// <summary>
            /// Status return code for entry
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// The location, if the operation returns a location
            /// </summary>
            [FhirElement("location", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri LocationElement
            {
                get { return _LocationElement; }
                set { _LocationElement = value; OnPropertyChanged("LocationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _LocationElement;
            
            /// <summary>
            /// The location, if the operation returns a location
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Location
            {
                get { return LocationElement != null ? LocationElement.Value : null; }
                set
                {
                    if(value == null)
                      LocationElement = null; 
                    else
                      LocationElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Location");
                }
            }
            
            /// <summary>
            /// The etag for the resource (if relevant)
            /// </summary>
            [FhirElement("etag", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString EtagElement
            {
                get { return _EtagElement; }
                set { _EtagElement = value; OnPropertyChanged("EtagElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _EtagElement;
            
            /// <summary>
            /// The etag for the resource (if relevant)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Etag
            {
                get { return EtagElement != null ? EtagElement.Value : null; }
                set
                {
                    if(value == null)
                      EtagElement = null; 
                    else
                      EtagElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Etag");
                }
            }
            
            /// <summary>
            /// Server's date time modified
            /// </summary>
            [FhirElement("lastModified", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Instant LastModifiedElement
            {
                get { return _LastModifiedElement; }
                set { _LastModifiedElement = value; OnPropertyChanged("LastModifiedElement"); }
            }
            
            private Hl7.Fhir.Model.Instant _LastModifiedElement;
            
            /// <summary>
            /// Server's date time modified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? LastModified
            {
                get { return LastModifiedElement != null ? LastModifiedElement.Value : null; }
                set
                {
                    if(value == null)
                      LastModifiedElement = null; 
                    else
                      LastModifiedElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("LastModified");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleEntryTransactionResponseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StatusElement != null) dest.StatusElement = (Hl7.Fhir.Model.FhirString)StatusElement.DeepCopy();
                    if(LocationElement != null) dest.LocationElement = (Hl7.Fhir.Model.FhirUri)LocationElement.DeepCopy();
                    if(EtagElement != null) dest.EtagElement = (Hl7.Fhir.Model.FhirString)EtagElement.DeepCopy();
                    if(LastModifiedElement != null) dest.LastModifiedElement = (Hl7.Fhir.Model.Instant)LastModifiedElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BundleEntryTransactionResponseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BundleEntryTransactionResponseComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(LocationElement, otherT.LocationElement)) return false;
                if( !DeepComparable.Matches(EtagElement, otherT.EtagElement)) return false;
                if( !DeepComparable.Matches(LastModifiedElement, otherT.LastModifiedElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleEntryTransactionResponseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(LocationElement, otherT.LocationElement)) return false;
                if( !DeepComparable.IsExactly(EtagElement, otherT.EtagElement)) return false;
                if( !DeepComparable.IsExactly(LastModifiedElement, otherT.LastModifiedElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("BundleEntrySearchComponent")]
        [DataContract]
        public partial class BundleEntrySearchComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BundleEntrySearchComponent"; } }
            
            /// <summary>
            /// match | include - why this is in the result set
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Bundle.SearchEntryMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Bundle.SearchEntryMode> _ModeElement;
            
            /// <summary>
            /// match | include - why this is in the result set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Bundle.SearchEntryMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new Code<Hl7.Fhir.Model.Bundle.SearchEntryMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Search ranking (between 0 and 1)
            /// </summary>
            [FhirElement("score", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ScoreElement
            {
                get { return _ScoreElement; }
                set { _ScoreElement = value; OnPropertyChanged("ScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ScoreElement;
            
            /// <summary>
            /// Search ranking (between 0 and 1)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Score
            {
                get { return ScoreElement != null ? ScoreElement.Value : null; }
                set
                {
                    if(value == null)
                      ScoreElement = null; 
                    else
                      ScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Score");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleEntrySearchComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Bundle.SearchEntryMode>)ModeElement.DeepCopy();
                    if(ScoreElement != null) dest.ScoreElement = (Hl7.Fhir.Model.FhirDecimal)ScoreElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BundleEntrySearchComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BundleEntrySearchComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(ScoreElement, otherT.ScoreElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleEntrySearchComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(ScoreElement, otherT.ScoreElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("BundleEntryComponent")]
        [DataContract]
        public partial class BundleEntryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BundleEntryComponent"; } }
            
            /// <summary>
            /// Base URL, if different to bundle base
            /// </summary>
            [FhirElement("base", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri BaseElement
            {
                get { return _BaseElement; }
                set { _BaseElement = value; OnPropertyChanged("BaseElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _BaseElement;
            
            /// <summary>
            /// Base URL, if different to bundle base
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Base
            {
                get { return BaseElement != null ? BaseElement.Value : null; }
                set
                {
                    if(value == null)
                      BaseElement = null; 
                    else
                      BaseElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Base");
                }
            }
            
            /// <summary>
            /// Links related to this entry
            /// </summary>
            [FhirElement("link", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Bundle.BundleLinkComponent> Link
            {
                get { if(_Link==null) _Link = new List<Hl7.Fhir.Model.Bundle.BundleLinkComponent>(); return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private List<Hl7.Fhir.Model.Bundle.BundleLinkComponent> _Link;
            
            /// <summary>
            /// Resources in this bundle
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=60, Choice=ChoiceType.ResourceChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.Resource _Resource;
            
            /// <summary>
            /// Search related information
            /// </summary>
            [FhirElement("search", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Bundle.BundleEntrySearchComponent Search
            {
                get { return _Search; }
                set { _Search = value; OnPropertyChanged("Search"); }
            }
            
            private Hl7.Fhir.Model.Bundle.BundleEntrySearchComponent _Search;
            
            /// <summary>
            /// Transaction Related Information
            /// </summary>
            [FhirElement("transaction", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Bundle.BundleEntryTransactionComponent Transaction
            {
                get { return _Transaction; }
                set { _Transaction = value; OnPropertyChanged("Transaction"); }
            }
            
            private Hl7.Fhir.Model.Bundle.BundleEntryTransactionComponent _Transaction;
            
            /// <summary>
            /// Transaction Related Information
            /// </summary>
            [FhirElement("transactionResponse", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Bundle.BundleEntryTransactionResponseComponent TransactionResponse
            {
                get { return _TransactionResponse; }
                set { _TransactionResponse = value; OnPropertyChanged("TransactionResponse"); }
            }
            
            private Hl7.Fhir.Model.Bundle.BundleEntryTransactionResponseComponent _TransactionResponse;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleEntryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(BaseElement != null) dest.BaseElement = (Hl7.Fhir.Model.FhirUri)BaseElement.DeepCopy();
                    if(Link != null) dest.Link = new List<Hl7.Fhir.Model.Bundle.BundleLinkComponent>(Link.DeepCopy());
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.Resource)Resource.DeepCopy();
                    if(Search != null) dest.Search = (Hl7.Fhir.Model.Bundle.BundleEntrySearchComponent)Search.DeepCopy();
                    if(Transaction != null) dest.Transaction = (Hl7.Fhir.Model.Bundle.BundleEntryTransactionComponent)Transaction.DeepCopy();
                    if(TransactionResponse != null) dest.TransactionResponse = (Hl7.Fhir.Model.Bundle.BundleEntryTransactionResponseComponent)TransactionResponse.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BundleEntryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BundleEntryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(BaseElement, otherT.BaseElement)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Search, otherT.Search)) return false;
                if( !DeepComparable.Matches(Transaction, otherT.Transaction)) return false;
                if( !DeepComparable.Matches(TransactionResponse, otherT.TransactionResponse)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleEntryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Search, otherT.Search)) return false;
                if( !DeepComparable.IsExactly(Transaction, otherT.Transaction)) return false;
                if( !DeepComparable.IsExactly(TransactionResponse, otherT.TransactionResponse)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("BundleLinkComponent")]
        [DataContract]
        public partial class BundleLinkComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BundleLinkComponent"; } }
            
            /// <summary>
            /// http://www.iana.org/assignments/link-relations/link-relations.xhtml
            /// </summary>
            [FhirElement("relation", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RelationElement
            {
                get { return _RelationElement; }
                set { _RelationElement = value; OnPropertyChanged("RelationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RelationElement;
            
            /// <summary>
            /// http://www.iana.org/assignments/link-relations/link-relations.xhtml
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Relation
            {
                get { return RelationElement != null ? RelationElement.Value : null; }
                set
                {
                    if(value == null)
                      RelationElement = null; 
                    else
                      RelationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Relation");
                }
            }
            
            /// <summary>
            /// Reference details for the link
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Reference details for the link
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleLinkComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RelationElement != null) dest.RelationElement = (Hl7.Fhir.Model.FhirString)RelationElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BundleLinkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BundleLinkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RelationElement, otherT.RelationElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleLinkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RelationElement, otherT.RelationElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// document | message | transaction | transaction-response | history | searchset | collection
        /// </summary>
        [FhirElement("type", Order=50)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Bundle.BundleType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Bundle.BundleType> _TypeElement;
        
        /// <summary>
        /// document | message | transaction | transaction-response | history | searchset | collection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Bundle.BundleType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Bundle.BundleType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Stated Base URL
        /// </summary>
        [FhirElement("base", Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri BaseElement
        {
            get { return _BaseElement; }
            set { _BaseElement = value; OnPropertyChanged("BaseElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _BaseElement;
        
        /// <summary>
        /// Stated Base URL
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Base
        {
            get { return BaseElement != null ? BaseElement.Value : null; }
            set
            {
                if(value == null)
                  BaseElement = null; 
                else
                  BaseElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Base");
            }
        }
        
        /// <summary>
        /// If search, the total number of matches
        /// </summary>
        [FhirElement("total", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt TotalElement
        {
            get { return _TotalElement; }
            set { _TotalElement = value; OnPropertyChanged("TotalElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _TotalElement;
        
        /// <summary>
        /// If search, the total number of matches
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Total
        {
            get { return TotalElement != null ? TotalElement.Value : null; }
            set
            {
                if(value == null)
                  TotalElement = null; 
                else
                  TotalElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Total");
            }
        }
        
        /// <summary>
        /// Links related to this Bundle
        /// </summary>
        [FhirElement("link", Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Bundle.BundleLinkComponent> Link
        {
            get { if(_Link==null) _Link = new List<Hl7.Fhir.Model.Bundle.BundleLinkComponent>(); return _Link; }
            set { _Link = value; OnPropertyChanged("Link"); }
        }
        
        private List<Hl7.Fhir.Model.Bundle.BundleLinkComponent> _Link;
        
        /// <summary>
        /// Entry in the bundle - will have a resource, or information
        /// </summary>
        [FhirElement("entry", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Bundle.BundleEntryComponent> Entry
        {
            get { if(_Entry==null) _Entry = new List<Hl7.Fhir.Model.Bundle.BundleEntryComponent>(); return _Entry; }
            set { _Entry = value; OnPropertyChanged("Entry"); }
        }
        
        private List<Hl7.Fhir.Model.Bundle.BundleEntryComponent> _Entry;
        
        /// <summary>
        /// XML Digital Signature (base64 encoded)
        /// </summary>
        [FhirElement("signature", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary SignatureElement
        {
            get { return _SignatureElement; }
            set { _SignatureElement = value; OnPropertyChanged("SignatureElement"); }
        }
        
        private Hl7.Fhir.Model.Base64Binary _SignatureElement;
        
        /// <summary>
        /// XML Digital Signature (base64 encoded)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public byte[] Signature
        {
            get { return SignatureElement != null ? SignatureElement.Value : null; }
            set
            {
                if(value == null)
                  SignatureElement = null; 
                else
                  SignatureElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Signature");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Bundle;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Bundle.BundleType>)TypeElement.DeepCopy();
                if(BaseElement != null) dest.BaseElement = (Hl7.Fhir.Model.FhirUri)BaseElement.DeepCopy();
                if(TotalElement != null) dest.TotalElement = (Hl7.Fhir.Model.UnsignedInt)TotalElement.DeepCopy();
                if(Link != null) dest.Link = new List<Hl7.Fhir.Model.Bundle.BundleLinkComponent>(Link.DeepCopy());
                if(Entry != null) dest.Entry = new List<Hl7.Fhir.Model.Bundle.BundleEntryComponent>(Entry.DeepCopy());
                if(SignatureElement != null) dest.SignatureElement = (Hl7.Fhir.Model.Base64Binary)SignatureElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Bundle());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Bundle;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.Matches(TotalElement, otherT.TotalElement)) return false;
            if( !DeepComparable.Matches(Link, otherT.Link)) return false;
            if( !DeepComparable.Matches(Entry, otherT.Entry)) return false;
            if( !DeepComparable.Matches(SignatureElement, otherT.SignatureElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Bundle;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.IsExactly(TotalElement, otherT.TotalElement)) return false;
            if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
            if( !DeepComparable.IsExactly(Entry, otherT.Entry)) return false;
            if( !DeepComparable.IsExactly(SignatureElement, otherT.SignatureElement)) return false;
            
            return true;
        }
        
    }
    
}
