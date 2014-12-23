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
// Generated on Tue, Dec 23, 2014 10:25+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Contains a group of resources
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
        /// The status of a resource in the bundle. Used for search (to differentiate between resources included as a match, and resources included as an _include), and for transactions (create/update/delete)
        /// </summary>
        [FhirEnumeration("BundleEntryStatus")]
        public enum BundleEntryStatus
        {
            /// <summary>
            /// Transaction: perform a create operation on this resource.
            /// </summary>
            [EnumLiteral("create")]
            Create,
            /// <summary>
            /// Transaction: perform an update operation on this resource.
            /// </summary>
            [EnumLiteral("update")]
            Update,
            /// <summary>
            /// Transaction: look for this resource using the search url provided. If there's no match, create it. Search: this resource is returned because it matches the search criteria.
            /// </summary>
            [EnumLiteral("match")]
            Match,
            /// <summary>
            /// Search: this resource is returned because it meets an _include criteria.
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
        
        [FhirType("BundleEntryDeletedComponent")]
        [DataContract]
        public partial class BundleEntryDeletedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BundleEntryDeletedComponent"; } }
            
            /// <summary>
            /// Type of resource that was deleted
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Hl7.Fhir.Model.Code _TypeElement;
            
            /// <summary>
            /// Type of resource that was deleted
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Id of resource that was deleted
            /// </summary>
            [FhirElement("resourceId", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResourceIdElement
            {
                get { return _ResourceIdElement; }
                set { _ResourceIdElement = value; OnPropertyChanged("ResourceIdElement"); }
            }
            private Hl7.Fhir.Model.Id _ResourceIdElement;
            
            /// <summary>
            /// Id of resource that was deleted
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResourceId
            {
                get { return ResourceIdElement != null ? ResourceIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ResourceIdElement = null; 
                    else
                      ResourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ResourceId");
                }
            }
            
            /// <summary>
            /// Version id for releted resource
            /// </summary>
            [FhirElement("versionId", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id VersionIdElement
            {
                get { return _VersionIdElement; }
                set { _VersionIdElement = value; OnPropertyChanged("VersionIdElement"); }
            }
            private Hl7.Fhir.Model.Id _VersionIdElement;
            
            /// <summary>
            /// Version id for releted resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VersionId
            {
                get { return VersionIdElement != null ? VersionIdElement.Value : null; }
                set
                {
                    if(value == null)
                      VersionIdElement = null; 
                    else
                      VersionIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("VersionId");
                }
            }
            
            /// <summary>
            /// When the resource was deleted
            /// </summary>
            [FhirElement("instant", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Instant InstantElement
            {
                get { return _InstantElement; }
                set { _InstantElement = value; OnPropertyChanged("InstantElement"); }
            }
            private Hl7.Fhir.Model.Instant _InstantElement;
            
            /// <summary>
            /// When the resource was deleted
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? Instant
            {
                get { return InstantElement != null ? InstantElement.Value : null; }
                set
                {
                    if(value == null)
                      InstantElement = null; 
                    else
                      InstantElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("Instant");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleEntryDeletedComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(ResourceIdElement != null) dest.ResourceIdElement = (Hl7.Fhir.Model.Id)ResourceIdElement.DeepCopy();
                    if(VersionIdElement != null) dest.VersionIdElement = (Hl7.Fhir.Model.Id)VersionIdElement.DeepCopy();
                    if(InstantElement != null) dest.InstantElement = (Hl7.Fhir.Model.Instant)InstantElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BundleEntryDeletedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BundleEntryDeletedComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ResourceIdElement, otherT.ResourceIdElement)) return false;
                if( !DeepComparable.Matches(VersionIdElement, otherT.VersionIdElement)) return false;
                if( !DeepComparable.Matches(InstantElement, otherT.InstantElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleEntryDeletedComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ResourceIdElement, otherT.ResourceIdElement)) return false;
                if( !DeepComparable.IsExactly(VersionIdElement, otherT.VersionIdElement)) return false;
                if( !DeepComparable.IsExactly(InstantElement, otherT.InstantElement)) return false;
                
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
            /// create | update | match | include - for search &amp; transaction
            /// </summary>
            [FhirElement("status", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Bundle.BundleEntryStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.Bundle.BundleEntryStatus> _StatusElement;
            
            /// <summary>
            /// create | update | match | include - for search &amp; transaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Bundle.BundleEntryStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.Bundle.BundleEntryStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Search URL (see transaction)
            /// </summary>
            [FhirElement("search", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SearchElement
            {
                get { return _SearchElement; }
                set { _SearchElement = value; OnPropertyChanged("SearchElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _SearchElement;
            
            /// <summary>
            /// Search URL (see transaction)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Search
            {
                get { return SearchElement != null ? SearchElement.Value : null; }
                set
                {
                    if(value == null)
                      SearchElement = null; 
                    else
                      SearchElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Search");
                }
            }
            
            /// <summary>
            /// Search ranking (between 0 and 1)
            /// </summary>
            [FhirElement("score", InSummary=true, Order=70)]
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
            
            /// <summary>
            /// If this is a deleted resource (transaction/history)
            /// </summary>
            [FhirElement("deleted", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Bundle.BundleEntryDeletedComponent Deleted
            {
                get { return _Deleted; }
                set { _Deleted = value; OnPropertyChanged("Deleted"); }
            }
            private Hl7.Fhir.Model.Bundle.BundleEntryDeletedComponent _Deleted;
            
            /// <summary>
            /// Resources in this bundle
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=90, Choice=ChoiceType.ResourceChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            private Hl7.Fhir.Model.Resource _Resource;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BundleEntryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(BaseElement != null) dest.BaseElement = (Hl7.Fhir.Model.FhirUri)BaseElement.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Bundle.BundleEntryStatus>)StatusElement.DeepCopy();
                    if(SearchElement != null) dest.SearchElement = (Hl7.Fhir.Model.FhirUri)SearchElement.DeepCopy();
                    if(ScoreElement != null) dest.ScoreElement = (Hl7.Fhir.Model.FhirDecimal)ScoreElement.DeepCopy();
                    if(Deleted != null) dest.Deleted = (Hl7.Fhir.Model.Bundle.BundleEntryDeletedComponent)Deleted.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.Resource)Resource.DeepCopy();
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
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(SearchElement, otherT.SearchElement)) return false;
                if( !DeepComparable.Matches(ScoreElement, otherT.ScoreElement)) return false;
                if( !DeepComparable.Matches(Deleted, otherT.Deleted)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BundleEntryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(SearchElement, otherT.SearchElement)) return false;
                if( !DeepComparable.IsExactly(ScoreElement, otherT.ScoreElement)) return false;
                if( !DeepComparable.IsExactly(Deleted, otherT.Deleted)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                
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
        public Hl7.Fhir.Model.Integer TotalElement
        {
            get { return _TotalElement; }
            set { _TotalElement = value; OnPropertyChanged("TotalElement"); }
        }
        private Hl7.Fhir.Model.Integer _TotalElement;
        
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
                  TotalElement = new Hl7.Fhir.Model.Integer(value);
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
        /// Entry in the bundle - will have deleted or resource
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
                if(TotalElement != null) dest.TotalElement = (Hl7.Fhir.Model.Integer)TotalElement.DeepCopy();
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
