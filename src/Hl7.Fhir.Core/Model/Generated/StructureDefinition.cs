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
// Generated for FHIR v1.6.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Structural Definition
    /// </summary>
    [FhirType("StructureDefinition", IsResource=true)]
    [DataContract]
    public partial class StructureDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.StructureDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "StructureDefinition"; } }
        
        /// <summary>
        /// Defines the type of structure that a definition is describing.
        /// (url: http://hl7.org/fhir/ValueSet/structure-definition-kind)
        /// </summary>
        [FhirEnumeration("StructureDefinitionKind")]
        public enum StructureDefinitionKind
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("primitive-type"), Description("Primitive Data Type")]
            PrimitiveType,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("complex-type"), Description("Complex Data Type")]
            ComplexType,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("resource"), Description("Resource")]
            Resource,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("logical"), Description("Logical Model")]
            Logical,
        }

        /// <summary>
        /// How an extension context is interpreted.
        /// (url: http://hl7.org/fhir/ValueSet/extension-context)
        /// </summary>
        [FhirEnumeration("ExtensionContext")]
        public enum ExtensionContext
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/extension-context)
            /// </summary>
            [EnumLiteral("resource"), Description("Resource")]
            Resource,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/extension-context)
            /// </summary>
            [EnumLiteral("datatype"), Description("Datatype")]
            Datatype,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/extension-context)
            /// </summary>
            [EnumLiteral("extension"), Description("Extension")]
            Extension,
        }

        /// <summary>
        /// How a type relates to it's baseDefinition.
        /// (url: http://hl7.org/fhir/ValueSet/type-derivation-rule)
        /// </summary>
        [FhirEnumeration("TypeDerivationRule")]
        public enum TypeDerivationRule
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/type-derivation-rule)
            /// </summary>
            [EnumLiteral("specialization"), Description("Specialization")]
            Specialization,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/type-derivation-rule)
            /// </summary>
            [EnumLiteral("constraint"), Description("Constraint")]
            Constraint,
        }

        [FhirType("ContactComponent")]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContactComponent"; } }
            
            /// <summary>
            /// Name of an individual to contact
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
            /// Name of an individual to contact
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
            /// Contact details for individual or publisher
            /// </summary>
            [FhirElement("telecom", InSummary=true, Order=50)]
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
                var dest = other as ContactComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("MappingComponent")]
        [DataContract]
        public partial class MappingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MappingComponent"; } }
            
            /// <summary>
            /// Internal id when this mapping is used
            /// </summary>
            [FhirElement("identity", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentityElement
            {
                get { return _IdentityElement; }
                set { _IdentityElement = value; OnPropertyChanged("IdentityElement"); }
            }
            
            private Hl7.Fhir.Model.Id _IdentityElement;
            
            /// <summary>
            /// Internal id when this mapping is used
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identity
            {
                get { return IdentityElement != null ? IdentityElement.Value : null; }
                set
                {
                if (value == null)
                      IdentityElement = null; 
                    else
                        IdentityElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identity");
                }
            }
            
            /// <summary>
            /// Identifies what this mapping refers to
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
            /// Identifies what this mapping refers to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                if (value == null)
                      UriElement = null; 
                    else
                        UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            [FhirElement("name", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Names what this mapping refers to
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
            /// Versions, Issues, Scope limitations etc.
            /// </summary>
            [FhirElement("comments", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement
            {
                get { return _CommentsElement; }
                set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentsElement;
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc.
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comments
            {
                get { return CommentsElement != null ? CommentsElement.Value : null; }
                set
                {
                if (value == null)
                      CommentsElement = null; 
                    else
                        CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comments");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MappingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SnapshotComponent")]
        [DataContract]
        public partial class SnapshotComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SnapshotComponent"; } }
            
            /// <summary>
            /// Definition of elements in the resource (if no StructureDefinition)
            /// </summary>
            [FhirElement("element", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ElementDefinition> Element
            {
                get { if(_Element==null) _Element = new List<Hl7.Fhir.Model.ElementDefinition>(); return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            
            private List<Hl7.Fhir.Model.ElementDefinition> _Element;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SnapshotComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Element != null) dest.Element = new List<Hl7.Fhir.Model.ElementDefinition>(Element.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SnapshotComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SnapshotComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SnapshotComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DifferentialComponent")]
        [DataContract]
        public partial class DifferentialComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DifferentialComponent"; } }
            
            /// <summary>
            /// Definition of elements in the resource (if no StructureDefinition)
            /// </summary>
            [FhirElement("element", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ElementDefinition> Element
            {
                get { if(_Element==null) _Element = new List<Hl7.Fhir.Model.ElementDefinition>(); return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            
            private List<Hl7.Fhir.Model.ElementDefinition> _Element;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DifferentialComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Element != null) dest.Element = new List<Hl7.Fhir.Model.ElementDefinition>(Element.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DifferentialComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DifferentialComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DifferentialComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Absolute URL used to reference this StructureDefinition
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Absolute URL used to reference this StructureDefinition
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
        /// Other identifiers for the StructureDefinition
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
        /// Logical id for this version of the StructureDefinition
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
        /// Logical id for this version of the StructureDefinition
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
        /// Informal name for this StructureDefinition
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name for this StructureDefinition
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
        /// Use this name when displaying the value
        /// </summary>
        [FhirElement("display", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DisplayElement
        {
            get { return _DisplayElement; }
            set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DisplayElement;
        
        /// <summary>
        /// Use this name when displaying the value
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Display
        {
            get { return DisplayElement != null ? DisplayElement.Value : null; }
            set
            {
                if (value == null)
                  DisplayElement = null; 
                else
                  DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Display");
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ConformanceResourceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ConformanceResourceStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ConformanceResourceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// If for testing purposes, not real usage
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
        /// Name of the publisher (Organization or individual)
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
        /// Name of the publisher (Organization or individual)
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
        /// Contact details of the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.StructureDefinition.ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.StructureDefinition.ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.StructureDefinition.ContactComponent> _Contact;
        
        /// <summary>
        /// Date for this version of the StructureDefinition
        /// </summary>
        [FhirElement("date", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for this version of the StructureDefinition
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
        /// Natural language description of the StructureDefinition
        /// </summary>
        [FhirElement("description", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// Content intends to support these contexts
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UseContext;
        
        /// <summary>
        /// Scope and Usage this structure definition is for
        /// </summary>
        [FhirElement("requirements", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Requirements
        {
            get { return _Requirements; }
            set { _Requirements = value; OnPropertyChanged("Requirements"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Requirements;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CopyrightElement;
        
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
                  CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Assist with indexing and finding
        /// </summary>
        [FhirElement("code", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Code;
        
        /// <summary>
        /// FHIR Version this StructureDefinition targets
        /// </summary>
        [FhirElement("fhirVersion", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private Hl7.Fhir.Model.Id _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version this StructureDefinition targets
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
            set
            {
                if (value == null)
                  FhirVersionElement = null; 
                else
                  FhirVersionElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// External specification that the content is mapped to
        /// </summary>
        [FhirElement("mapping", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.StructureDefinition.MappingComponent> Mapping
        {
            get { if(_Mapping==null) _Mapping = new List<Hl7.Fhir.Model.StructureDefinition.MappingComponent>(); return _Mapping; }
            set { _Mapping = value; OnPropertyChanged("Mapping"); }
        }
        
        private List<Hl7.Fhir.Model.StructureDefinition.MappingComponent> _Mapping;
        
        /// <summary>
        /// primitive-type | complex-type | resource | logical
        /// </summary>
        [FhirElement("kind", InSummary=true, Order=260)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.StructureDefinition.StructureDefinitionKind> KindElement
        {
            get { return _KindElement; }
            set { _KindElement = value; OnPropertyChanged("KindElement"); }
        }
        
        private Code<Hl7.Fhir.Model.StructureDefinition.StructureDefinitionKind> _KindElement;
        
        /// <summary>
        /// primitive-type | complex-type | resource | logical
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.StructureDefinition.StructureDefinitionKind? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  KindElement = null; 
                else
                  KindElement = new Code<Hl7.Fhir.Model.StructureDefinition.StructureDefinitionKind>(value);
                OnPropertyChanged("Kind");
            }
        }
        
        /// <summary>
        /// Whether the structure is abstract
        /// </summary>
        [FhirElement("abstract", InSummary=true, Order=270)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AbstractElement
        {
            get { return _AbstractElement; }
            set { _AbstractElement = value; OnPropertyChanged("AbstractElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _AbstractElement;
        
        /// <summary>
        /// Whether the structure is abstract
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Abstract
        {
            get { return AbstractElement != null ? AbstractElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  AbstractElement = null; 
                else
                  AbstractElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Abstract");
            }
        }
        
        /// <summary>
        /// resource | datatype | extension
        /// </summary>
        [FhirElement("contextType", InSummary=true, Order=280)]
        [DataMember]
        public Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContext> ContextTypeElement
        {
            get { return _ContextTypeElement; }
            set { _ContextTypeElement = value; OnPropertyChanged("ContextTypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContext> _ContextTypeElement;
        
        /// <summary>
        /// resource | datatype | extension
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.StructureDefinition.ExtensionContext? ContextType
        {
            get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ContextTypeElement = null; 
                else
                  ContextTypeElement = new Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContext>(value);
                OnPropertyChanged("ContextType");
            }
        }
        
        /// <summary>
        /// Where the extension can be used in instances
        /// </summary>
        [FhirElement("context", InSummary=true, Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ContextElement
        {
            get { if(_ContextElement==null) _ContextElement = new List<Hl7.Fhir.Model.FhirString>(); return _ContextElement; }
            set { _ContextElement = value; OnPropertyChanged("ContextElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ContextElement;
        
        /// <summary>
        /// Where the extension can be used in instances
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Context
        {
            get { return ContextElement != null ? ContextElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ContextElement = null; 
                else
                  ContextElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Context");
            }
        }
        
        /// <summary>
        /// Type defined or constrained by this structure
        /// </summary>
        [FhirElement("type", InSummary=true, Order=300)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _TypeElement;
        
        /// <summary>
        /// Type defined or constrained by this structure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Definition that this type is constrained/specialized from
        /// </summary>
        [FhirElement("baseDefinition", InSummary=true, Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri BaseDefinitionElement
        {
            get { return _BaseDefinitionElement; }
            set { _BaseDefinitionElement = value; OnPropertyChanged("BaseDefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _BaseDefinitionElement;
        
        /// <summary>
        /// Definition that this type is constrained/specialized from
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string BaseDefinition
        {
            get { return BaseDefinitionElement != null ? BaseDefinitionElement.Value : null; }
            set
            {
                if (value == null)
                  BaseDefinitionElement = null; 
                else
                  BaseDefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("BaseDefinition");
            }
        }
        
        /// <summary>
        /// specialization | constraint - How relates to base definition
        /// </summary>
        [FhirElement("derivation", InSummary=true, Order=320)]
        [DataMember]
        public Code<Hl7.Fhir.Model.StructureDefinition.TypeDerivationRule> DerivationElement
        {
            get { return _DerivationElement; }
            set { _DerivationElement = value; OnPropertyChanged("DerivationElement"); }
        }
        
        private Code<Hl7.Fhir.Model.StructureDefinition.TypeDerivationRule> _DerivationElement;
        
        /// <summary>
        /// specialization | constraint - How relates to base definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.StructureDefinition.TypeDerivationRule? Derivation
        {
            get { return DerivationElement != null ? DerivationElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  DerivationElement = null; 
                else
                  DerivationElement = new Code<Hl7.Fhir.Model.StructureDefinition.TypeDerivationRule>(value);
                OnPropertyChanged("Derivation");
            }
        }
        
        /// <summary>
        /// Snapshot view of the structure
        /// </summary>
        [FhirElement("snapshot", Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.StructureDefinition.SnapshotComponent Snapshot
        {
            get { return _Snapshot; }
            set { _Snapshot = value; OnPropertyChanged("Snapshot"); }
        }
        
        private Hl7.Fhir.Model.StructureDefinition.SnapshotComponent _Snapshot;
        
        /// <summary>
        /// Differential view of the structure
        /// </summary>
        [FhirElement("differential", Order=340)]
        [DataMember]
        public Hl7.Fhir.Model.StructureDefinition.DifferentialComponent Differential
        {
            get { return _Differential; }
            set { _Differential = value; OnPropertyChanged("Differential"); }
        }
        
        private Hl7.Fhir.Model.StructureDefinition.DifferentialComponent _Differential;
        

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_16 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.element.id.trace('ids').isDistinct()",
            Key = "sdf-16",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All element definitions must have unique ids (snapshot)",
            Xpath = "count(*/f:element)=count(*/f:element/@id)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_9 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "children().element.first().label.empty() and children().element.first().code.empty() and children().element.first().requirements.empty()",
            Key = "sdf-9",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "In any snapshot or differential, no label, code or requirements on the an element without a \".\" in the path (e.g. the first element)",
            Xpath = "not(exists(f:snapshot/f:element[not(contains(f:path/@value, '.')) and (f:label or f:code or f:requirements)])) and not(exists(f:differential/f:element[not(contains(f:path/@value, '.')) and (f:label or f:code or f:requirements)]))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_17 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "differential.element.id.trace('ids').isDistinct()",
            Key = "sdf-17",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All element definitions must have unique ids (diff)",
            Xpath = "count(*/f:element)=count(*/f:element/@id)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_12 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.exists() implies (snapshot.element.base.exists() = baseDefinition.exists())",
            Key = "sdf-12",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "element.base cannot appear if there is no base on the structure definition",
            Xpath = "f:baseDefinition or not(exists(f:snapshot/f:element/f:base) or exists(f:differential/f:element/f:base))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_11 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.empty() or snapshot.element.first().path = type",
            Key = "sdf-11",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If there's a type, its content must match the path name in the first element of a snapshot",
            Xpath = "not(exists(f:snapshot)) or (f:type/@value = f:snapshot/f:element[1]/f:path/@value)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_14 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.element.all(id.exists()) and differential.element.all(id.exists())",
            Key = "sdf-14",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All element definitions must have an id",
            Xpath = "count(*/f:element)=count(*/f:element/@id)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "derivation = 'constraint' or snapshot.element.select(path).distinct()",
            Key = "sdf-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Element paths must be unique unless the structure is a constraint",
            Xpath = "(f:derivation/@value = 'constraint') or (count(f:snapshot/f:element) = count(distinct-values(f:snapshot/f:element/f:path/@value)))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_7 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(derivation = 'constraint') or (url = 'http://hl7.org/fhir/StructureDefinition/'+id)",
            Key = "sdf-7",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If the structure describes a base Resource or Type, the URL has to start with \"http://hl7.org/fhir/StructureDefinition/\" and the tail must match the id",
            Xpath = "(f:derivation/@value = 'constraint') or f:url/@value=concat('http://hl7.org/fhir/StructureDefinition/', f:id/@value)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_6 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.exists() or differential.exists()",
            Key = "sdf-6",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "A structure must have either a differential, or a snapshot (or both)",
            Xpath = "exists(f:snapshot) or exists(f:differential)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_5 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "type != 'Extension' or derivation = 'specialization' or (context.exists() and contextType.exists())",
            Key = "sdf-5",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If the structure defines an extension then the structure must have context information",
            Xpath = "not(f:type/@value = 'extension') or (f:derivation/@value = 'specialization') or (exists(f:context) and exists(f:contextType))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "abstract = true or baseDefinition.exists()",
            Key = "sdf-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If the structure is not abstract, then there SHALL be a baseDefinition",
            Xpath = "(f:abstract/@value=true()) or exists(f:baseDefinition)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "mapping.all(name.exists() or uri.exists())",
            Key = "sdf-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Must have at a name or a uri (or both)",
            Xpath = "exists(f:uri) or exists(f:name)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_15 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.all(element.first().type.empty())",
            Key = "sdf-15",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "The first element in a snapshot has no type",
            Xpath = "not(f:element[1]/f:type)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_8 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.all(element.first().path = %resource.type and element.tail().all(path.startsWith(%resource.type&'.')))",
            Key = "sdf-8",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "In any snapshot, all the elements must be in the specified type",
            Xpath = "f:element[1]/f:path/@value=parent::f:StructureDefinition/f:type/@value and count(f:element[position()!=1])=count(f:element[position()!=1][starts-with(f:path/@value, concat(ancestor::f:StructureDefinition/f:type/@value, '.'))])"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "snapshot.all(element.all(definition and min and max))",
            Key = "sdf-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Each element definition in a snapshot must have a formal definition and cardinalities",
            Xpath = "count(f:element) = count(f:element[exists(f:definition) and exists(f:min) and exists(f:max)])"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_15A = new ElementDefinition.ConstraintComponent()
        {
            Expression = "differential.all(element.first().path.contains('.').not() implies element.first().type.empty())",
            Key = "sdf-15a",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If the first element in a differential has no \".\" in the path, it has no type",
            Xpath = "not(f:element[1][not(contains(f:path/@value, '.'))]/f:type)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_8A = new ElementDefinition.ConstraintComponent()
        {
            Expression = "differential.all(element.first().path.startsWith(%resource.type) and element.tail().all(path.startsWith(%resource.type&'.')))",
            Key = "sdf-8a",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "In any differential, all the elements must be in the specified type",
            Xpath = "count(f:element)=count(f:element[f:path/@value=ancestor::f:StructureDefinition/f:type/@value or starts-with(f:path/@value, concat(ancestor::f:StructureDefinition/f:type/@value, '.'))])"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(StructureDefinition_SDF_16);
            InvariantConstraints.Add(StructureDefinition_SDF_9);
            InvariantConstraints.Add(StructureDefinition_SDF_17);
            InvariantConstraints.Add(StructureDefinition_SDF_12);
            InvariantConstraints.Add(StructureDefinition_SDF_11);
            InvariantConstraints.Add(StructureDefinition_SDF_14);
            InvariantConstraints.Add(StructureDefinition_SDF_1);
            InvariantConstraints.Add(StructureDefinition_SDF_7);
            InvariantConstraints.Add(StructureDefinition_SDF_6);
            InvariantConstraints.Add(StructureDefinition_SDF_5);
            InvariantConstraints.Add(StructureDefinition_SDF_4);
            InvariantConstraints.Add(StructureDefinition_SDF_2);
            InvariantConstraints.Add(StructureDefinition_SDF_15);
            InvariantConstraints.Add(StructureDefinition_SDF_8);
            InvariantConstraints.Add(StructureDefinition_SDF_3);
            InvariantConstraints.Add(StructureDefinition_SDF_15A);
            InvariantConstraints.Add(StructureDefinition_SDF_8A);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as StructureDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.StructureDefinition.ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(Requirements != null) dest.Requirements = (Hl7.Fhir.Model.Markdown)Requirements.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.StructureDefinition.MappingComponent>(Mapping.DeepCopy());
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.StructureDefinition.StructureDefinitionKind>)KindElement.DeepCopy();
                if(AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
                if(ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContext>)ContextTypeElement.DeepCopy();
                if(ContextElement != null) dest.ContextElement = new List<Hl7.Fhir.Model.FhirString>(ContextElement.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(BaseDefinitionElement != null) dest.BaseDefinitionElement = (Hl7.Fhir.Model.FhirUri)BaseDefinitionElement.DeepCopy();
                if(DerivationElement != null) dest.DerivationElement = (Code<Hl7.Fhir.Model.StructureDefinition.TypeDerivationRule>)DerivationElement.DeepCopy();
                if(Snapshot != null) dest.Snapshot = (Hl7.Fhir.Model.StructureDefinition.SnapshotComponent)Snapshot.DeepCopy();
                if(Differential != null) dest.Differential = (Hl7.Fhir.Model.StructureDefinition.DifferentialComponent)Differential.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new StructureDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as StructureDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Requirements, otherT.Requirements)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
            if( !DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
            if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(BaseDefinitionElement, otherT.BaseDefinitionElement)) return false;
            if( !DeepComparable.Matches(DerivationElement, otherT.DerivationElement)) return false;
            if( !DeepComparable.Matches(Snapshot, otherT.Snapshot)) return false;
            if( !DeepComparable.Matches(Differential, otherT.Differential)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as StructureDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Requirements, otherT.Requirements)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
            if( !DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
            if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(BaseDefinitionElement, otherT.BaseDefinitionElement)) return false;
            if( !DeepComparable.IsExactly(DerivationElement, otherT.DerivationElement)) return false;
            if( !DeepComparable.IsExactly(Snapshot, otherT.Snapshot)) return false;
            if( !DeepComparable.IsExactly(Differential, otherT.Differential)) return false;
            
            return true;
        }
        
    }
    
}
