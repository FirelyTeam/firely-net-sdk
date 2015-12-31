using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Defines common metadata used by quality artifacts
    /// </summary>
    [FhirType("ModuleMetadata", IsResource=true)]
    [DataContract]
    public partial class ModuleMetadata : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ModuleMetadata; } }
        [NotMapped]
        public override string TypeName { get { return "ModuleMetadata"; } }
        
        /// <summary>
        /// The status of the knowledge module
        /// (url: http://hl7.org/fhir/ValueSet/module-metadata-status)
        /// </summary>
        [FhirEnumeration("ModuleMetadataStatus")]
        public enum ModuleMetadataStatus
        {
            /// <summary>
            /// The module is in draft state
            /// (system: http://hl7.org/fhir/module-metadata-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
            /// <summary>
            /// The module is in test state
            /// (system: http://hl7.org/fhir/module-metadata-status)
            /// </summary>
            [EnumLiteral("test"), Description("Test")]
            Test,
            /// <summary>
            /// The module is active
            /// (system: http://hl7.org/fhir/module-metadata-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// The module is inactive, either rejected before publication, or retired after publication
            /// (system: http://hl7.org/fhir/module-metadata-status)
            /// </summary>
            [EnumLiteral("inactive"), Description("Inactive")]
            Inactive,
        }

        /// <summary>
        /// The type of contributor
        /// (url: http://hl7.org/fhir/ValueSet/module-metadata-contributor)
        /// </summary>
        [FhirEnumeration("ModuleMetadataContributorType")]
        public enum ModuleMetadataContributorType
        {
            /// <summary>
            /// An author of the content of the module
            /// (system: http://hl7.org/fhir/module-metadata-contributor)
            /// </summary>
            [EnumLiteral("author"), Description("Author")]
            Author,
            /// <summary>
            /// An editor of the content of the module
            /// (system: http://hl7.org/fhir/module-metadata-contributor)
            /// </summary>
            [EnumLiteral("editor"), Description("Editor")]
            Editor,
            /// <summary>
            /// A reviewer of the content of the module
            /// (system: http://hl7.org/fhir/module-metadata-contributor)
            /// </summary>
            [EnumLiteral("reviewer"), Description("Reviewer")]
            Reviewer,
            /// <summary>
            /// An endorser of the content of the module
            /// (system: http://hl7.org/fhir/module-metadata-contributor)
            /// </summary>
            [EnumLiteral("endorser"), Description("Endorser")]
            Endorser,
        }

        /// <summary>
        /// The type of related resource for the module
        /// (url: http://hl7.org/fhir/ValueSet/module-metadata-resource-type)
        /// </summary>
        [FhirEnumeration("ModuleMetadataResourceType")]
        public enum ModuleMetadataResourceType
        {
            /// <summary>
            /// Additional documentation for the module
            /// (system: http://hl7.org/fhir/module-metadata-resource-type)
            /// </summary>
            [EnumLiteral("documentation"), Description("Documentation")]
            Documentation,
            /// <summary>
            /// Supporting evidence for the module
            /// (system: http://hl7.org/fhir/module-metadata-resource-type)
            /// </summary>
            [EnumLiteral("evidence"), Description("Evidence")]
            Evidence,
            /// <summary>
            /// Bibliographic citation for the module
            /// (system: http://hl7.org/fhir/module-metadata-resource-type)
            /// </summary>
            [EnumLiteral("citation"), Description("Citation")]
            Citation,
            /// <summary>
            /// The previous version of the module
            /// (system: http://hl7.org/fhir/module-metadata-resource-type)
            /// </summary>
            [EnumLiteral("predecessor"), Description("Predecessor")]
            Predecessor,
            /// <summary>
            /// The next version of the module
            /// (system: http://hl7.org/fhir/module-metadata-resource-type)
            /// </summary>
            [EnumLiteral("successor"), Description("Successor")]
            Successor,
            /// <summary>
            /// The module is derived from the resource
            /// (system: http://hl7.org/fhir/module-metadata-resource-type)
            /// </summary>
            [EnumLiteral("derived-from"), Description("Derived From")]
            DerivedFrom,
        }

        [FhirType("CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// patient-gender | patient-age-group | clinical-focus | target-user | workflow-setting | workflow-task | clinical-venue
            /// </summary>
            [FhirElement("focus", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code FocusElement
            {
                get { return _FocusElement; }
                set { _FocusElement = value; OnPropertyChanged("FocusElement"); }
            }
            
            private Hl7.Fhir.Model.Code _FocusElement;
            
            /// <summary>
            /// patient-gender | patient-age-group | clinical-focus | target-user | workflow-setting | workflow-task | clinical-venue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Focus
            {
                get { return FocusElement != null ? FocusElement.Value : null; }
                set
                {
                    if(value == null)
                      FocusElement = null; 
                    else
                      FocusElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Focus");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            [FhirElement("value", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FocusElement != null) dest.FocusElement = (Hl7.Fhir.Model.Code)FocusElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CoverageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(FocusElement, otherT.FocusElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FocusElement, otherT.FocusElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ContributorComponent")]
        [DataContract]
        public partial class ContributorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContributorComponent"; } }
            
            /// <summary>
            /// author | editor | reviewer | endorser
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataContributorType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataContributorType> _TypeElement;
            
            /// <summary>
            /// author | editor | reviewer | endorser
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataContributorType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataContributorType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("party", Order=50)]
            [References("Person","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContributorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataContributorType>)TypeElement.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContributorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContributorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContributorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("RelatedResourceComponent")]
        [DataContract]
        public partial class RelatedResourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedResourceComponent"; } }
            
            /// <summary>
            /// documentation | evidence | citation | predecessor | successor | derived-from
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataResourceType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataResourceType> _TypeElement;
            
            /// <summary>
            /// documentation | evidence | citation | predecessor | successor | derived-from
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataResourceType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataResourceType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("uri", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if(value == null)
                      UriElement = null; 
                    else
                      UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            [FhirElement("document", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Attachment Document
            {
                get { return _Document; }
                set { _Document = value; OnPropertyChanged("Document"); }
            }
            
            private Hl7.Fhir.Model.Attachment _Document;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedResourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataResourceType>)TypeElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Document != null) dest.Document = (Hl7.Fhir.Model.Attachment)Document.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RelatedResourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedResourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Document, otherT.Document)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedResourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// The version of the module, if any
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
        /// The version of the module, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("title", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if(value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// module | library | decision-support-rule | documentation-template | order-set
        /// </summary>
        [FhirElement("type", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _TypeElement;
        
        /// <summary>
        /// module | library | decision-support-rule | documentation-template | order-set
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
        /// draft | test | active | inactive
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataStatus> _StatusElement;
        
        /// <summary>
        /// draft | test | active | inactive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("description", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        [FhirElement("purpose", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PurposeElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if(value == null)
                  PurposeElement = null; 
                else
                  PurposeElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("usage", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UsageElement
        {
            get { return _UsageElement; }
            set { _UsageElement = value; OnPropertyChanged("UsageElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _UsageElement;
        
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        [FhirElement("publicationDate", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Date PublicationDateElement
        {
            get { return _PublicationDateElement; }
            set { _PublicationDateElement = value; OnPropertyChanged("PublicationDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _PublicationDateElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PublicationDate
        {
            get { return PublicationDateElement != null ? PublicationDateElement.Value : null; }
            set
            {
                if(value == null)
                  PublicationDateElement = null; 
                else
                  PublicationDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("PublicationDate");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("lastReviewDate", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Date LastReviewDateElement
        {
            get { return _LastReviewDateElement; }
            set { _LastReviewDateElement = value; OnPropertyChanged("LastReviewDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LastReviewDateElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastReviewDate
        {
            get { return LastReviewDateElement != null ? LastReviewDateElement.Value : null; }
            set
            {
                if(value == null)
                  LastReviewDateElement = null; 
                else
                  LastReviewDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("LastReviewDate");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("effectivePeriod", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Period EffectivePeriod
        {
            get { return _EffectivePeriod; }
            set { _EffectivePeriod = value; OnPropertyChanged("EffectivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _EffectivePeriod;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("coverage", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleMetadata.CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<Hl7.Fhir.Model.ModuleMetadata.CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleMetadata.CoverageComponent> _Coverage;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("topic", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Topic
        {
            get { if(_Topic==null) _Topic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Topic;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("keyword", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> KeywordElement
        {
            get { if(_KeywordElement==null) _KeywordElement = new List<Hl7.Fhir.Model.FhirString>(); return _KeywordElement; }
            set { _KeywordElement = value; OnPropertyChanged("KeywordElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _KeywordElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Keyword
        {
            get { return KeywordElement != null ? KeywordElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  KeywordElement = null; 
                else
                  KeywordElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Keyword");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("contributor", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleMetadata.ContributorComponent> Contributor
        {
            get { if(_Contributor==null) _Contributor = new List<Hl7.Fhir.Model.ModuleMetadata.ContributorComponent>(); return _Contributor; }
            set { _Contributor = value; OnPropertyChanged("Contributor"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleMetadata.ContributorComponent> _Contributor;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("publisher", Order=240)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Publisher
        {
            get { return _Publisher; }
            set { _Publisher = value; OnPropertyChanged("Publisher"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Publisher;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("steward", Order=250)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Steward
        {
            get { return _Steward; }
            set { _Steward = value; OnPropertyChanged("Steward"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Steward;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("rightsDeclaration", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RightsDeclarationElement
        {
            get { return _RightsDeclarationElement; }
            set { _RightsDeclarationElement = value; OnPropertyChanged("RightsDeclarationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RightsDeclarationElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RightsDeclaration
        {
            get { return RightsDeclarationElement != null ? RightsDeclarationElement.Value : null; }
            set
            {
                if(value == null)
                  RightsDeclarationElement = null; 
                else
                  RightsDeclarationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RightsDeclaration");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("relatedResource", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleMetadata.RelatedResourceComponent> RelatedResource
        {
            get { if(_RelatedResource==null) _RelatedResource = new List<Hl7.Fhir.Model.ModuleMetadata.RelatedResourceComponent>(); return _RelatedResource; }
            set { _RelatedResource = value; OnPropertyChanged("RelatedResource"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleMetadata.RelatedResourceComponent> _RelatedResource;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ModuleMetadata;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataStatus>)StatusElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.FhirString)PurposeElement.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(PublicationDateElement != null) dest.PublicationDateElement = (Hl7.Fhir.Model.Date)PublicationDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.ModuleMetadata.CoverageComponent>(Coverage.DeepCopy());
                if(Topic != null) dest.Topic = new List<Hl7.Fhir.Model.CodeableConcept>(Topic.DeepCopy());
                if(KeywordElement != null) dest.KeywordElement = new List<Hl7.Fhir.Model.FhirString>(KeywordElement.DeepCopy());
                if(Contributor != null) dest.Contributor = new List<Hl7.Fhir.Model.ModuleMetadata.ContributorComponent>(Contributor.DeepCopy());
                if(Publisher != null) dest.Publisher = (Hl7.Fhir.Model.ResourceReference)Publisher.DeepCopy();
                if(Steward != null) dest.Steward = (Hl7.Fhir.Model.ResourceReference)Steward.DeepCopy();
                if(RightsDeclarationElement != null) dest.RightsDeclarationElement = (Hl7.Fhir.Model.FhirString)RightsDeclarationElement.DeepCopy();
                if(RelatedResource != null) dest.RelatedResource = new List<Hl7.Fhir.Model.ModuleMetadata.RelatedResourceComponent>(RelatedResource.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ModuleMetadata());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ModuleMetadata;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.Matches(PublicationDateElement, otherT.PublicationDateElement)) return false;
            if( !DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(KeywordElement, otherT.KeywordElement)) return false;
            if( !DeepComparable.Matches(Contributor, otherT.Contributor)) return false;
            if( !DeepComparable.Matches(Publisher, otherT.Publisher)) return false;
            if( !DeepComparable.Matches(Steward, otherT.Steward)) return false;
            if( !DeepComparable.Matches(RightsDeclarationElement, otherT.RightsDeclarationElement)) return false;
            if( !DeepComparable.Matches(RelatedResource, otherT.RelatedResource)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ModuleMetadata;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.IsExactly(PublicationDateElement, otherT.PublicationDateElement)) return false;
            if( !DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(KeywordElement, otherT.KeywordElement)) return false;
            if( !DeepComparable.IsExactly(Contributor, otherT.Contributor)) return false;
            if( !DeepComparable.IsExactly(Publisher, otherT.Publisher)) return false;
            if( !DeepComparable.IsExactly(Steward, otherT.Steward)) return false;
            if( !DeepComparable.IsExactly(RightsDeclarationElement, otherT.RightsDeclarationElement)) return false;
            if( !DeepComparable.IsExactly(RelatedResource, otherT.RelatedResource)) return false;
            
            return true;
        }
        
    }
    
}
