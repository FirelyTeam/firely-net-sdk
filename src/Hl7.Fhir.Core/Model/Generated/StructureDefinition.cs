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
// Generated for FHIR v4.0.0
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
            [EnumLiteral("primitive-type", "http://hl7.org/fhir/structure-definition-kind"), Description("Primitive Data Type")]
            PrimitiveType,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("complex-type", "http://hl7.org/fhir/structure-definition-kind"), Description("Complex Data Type")]
            ComplexType,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("resource", "http://hl7.org/fhir/structure-definition-kind"), Description("Resource")]
            Resource,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/structure-definition-kind)
            /// </summary>
            [EnumLiteral("logical", "http://hl7.org/fhir/structure-definition-kind"), Description("Logical")]
            Logical,
        }

        /// <summary>
        /// How an extension context is interpreted.
        /// (url: http://hl7.org/fhir/ValueSet/extension-context-type)
        /// </summary>
        [FhirEnumeration("ExtensionContextType")]
        public enum ExtensionContextType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/extension-context-type)
            /// </summary>
            [EnumLiteral("fhirpath", "http://hl7.org/fhir/extension-context-type"), Description("FHIRPath")]
            Fhirpath,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/extension-context-type)
            /// </summary>
            [EnumLiteral("element", "http://hl7.org/fhir/extension-context-type"), Description("Element ID")]
            Element,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/extension-context-type)
            /// </summary>
            [EnumLiteral("extension", "http://hl7.org/fhir/extension-context-type"), Description("Extension URL")]
            Extension,
        }

        /// <summary>
        /// How a type relates to its baseDefinition.
        /// (url: http://hl7.org/fhir/ValueSet/type-derivation-rule)
        /// </summary>
        [FhirEnumeration("TypeDerivationRule")]
        public enum TypeDerivationRule
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/type-derivation-rule)
            /// </summary>
            [EnumLiteral("specialization", "http://hl7.org/fhir/type-derivation-rule"), Description("Specialization")]
            Specialization,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/type-derivation-rule)
            /// </summary>
            [EnumLiteral("constraint", "http://hl7.org/fhir/type-derivation-rule"), Description("Constraint")]
            Constraint,
        }

        [FhirType("MappingComponent", NamedBackboneElement=true)]
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
            [FhirElement("comment", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc.
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comment
            {
                get { return CommentElement != null ? CommentElement.Value : null; }
                set
                {
                    if (value == null)
                        CommentElement = null; 
                    else
                        CommentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comment");
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
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
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
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                
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
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentityElement != null) yield return IdentityElement;
                    if (UriElement != null) yield return UriElement;
                    if (NameElement != null) yield return NameElement;
                    if (CommentElement != null) yield return CommentElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IdentityElement != null) yield return new ElementValue("identity", IdentityElement);
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                }
            }

            
        }
        
        
        [FhirType("ContextComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContextComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContextComponent"; } }
            
            /// <summary>
            /// fhirpath | element | extension
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContextType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContextType> _TypeElement;
            
            /// <summary>
            /// fhirpath | element | extension
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureDefinition.ExtensionContextType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContextType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Where the extension can be used in instances
            /// </summary>
            [FhirElement("expression", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// Where the extension can be used in instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                        ExpressionElement = null; 
                    else
                        ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContextComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.StructureDefinition.ExtensionContextType>)TypeElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContextComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContextComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContextComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                }
            }

            
        }
        
        
        [FhirType("SnapshotComponent", NamedBackboneElement=true)]
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


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return new ElementValue("element", elem); }
                }
            }

            
        }
        
        
        [FhirType("DifferentialComponent", NamedBackboneElement=true)]
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


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return new ElementValue("element", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this structure definition, represented as a URI (globally unique)
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
        /// Canonical identifier for this structure definition, represented as a URI (globally unique)
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
        /// Additional identifier for the structure definition
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
        /// Business version of the structure definition
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
        /// Business version of the structure definition
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
        /// Name for this structure definition (computer friendly)
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
        /// Name for this structure definition (computer friendly)
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
        /// Name for this structure definition (human friendly)
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
        /// Name for this structure definition (human friendly)
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
        [FhirElement("status", InSummary=true, Order=140)]
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
        [FhirElement("experimental", InSummary=true, Order=150)]
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
        [FhirElement("date", InSummary=true, Order=160)]
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
        [FhirElement("publisher", InSummary=true, Order=170)]
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
        [FhirElement("contact", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the structure definition
        /// </summary>
        [FhirElement("description", Order=190)]
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
        [FhirElement("useContext", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for structure definition (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this structure definition is defined
        /// </summary>
        [FhirElement("purpose", Order=220)]
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
        [FhirElement("copyright", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// Assist with indexing and finding
        /// </summary>
        [FhirElement("keyword", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Keyword
        {
            get { if(_Keyword==null) _Keyword = new List<Hl7.Fhir.Model.Coding>(); return _Keyword; }
            set { _Keyword = value; OnPropertyChanged("Keyword"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Keyword;
        
        /// <summary>
        /// FHIR Version this StructureDefinition targets
        /// </summary>
        [FhirElement("fhirVersion", InSummary=true, Order=250)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FHIRVersion> FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FHIRVersion> _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version this StructureDefinition targets
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
        /// External specification that the content is mapped to
        /// </summary>
        [FhirElement("mapping", Order=260)]
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
        [FhirElement("kind", InSummary=true, Order=270)]
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
        [FhirElement("abstract", InSummary=true, Order=280)]
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
        /// If an extension, where it can be used in instances
        /// </summary>
        [FhirElement("context", InSummary=true, Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.StructureDefinition.ContextComponent> Context
        {
            get { if(_Context==null) _Context = new List<Hl7.Fhir.Model.StructureDefinition.ContextComponent>(); return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private List<Hl7.Fhir.Model.StructureDefinition.ContextComponent> _Context;
        
        /// <summary>
        /// FHIRPath invariants - when the extension can be used
        /// </summary>
        [FhirElement("contextInvariant", InSummary=true, Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ContextInvariantElement
        {
            get { if(_ContextInvariantElement==null) _ContextInvariantElement = new List<Hl7.Fhir.Model.FhirString>(); return _ContextInvariantElement; }
            set { _ContextInvariantElement = value; OnPropertyChanged("ContextInvariantElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ContextInvariantElement;
        
        /// <summary>
        /// FHIRPath invariants - when the extension can be used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> ContextInvariant
        {
            get { return ContextInvariantElement != null ? ContextInvariantElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ContextInvariantElement = null; 
                else
                  ContextInvariantElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("ContextInvariant");
            }
        }
        
        /// <summary>
        /// Type defined or constrained by this structure
        /// </summary>
        [FhirElement("type", InSummary=true, Order=310)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _TypeElement;
        
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
                  TypeElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Definition that this type is constrained/specialized from
        /// </summary>
        [FhirElement("baseDefinition", InSummary=true, Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical BaseDefinitionElement
        {
            get { return _BaseDefinitionElement; }
            set { _BaseDefinitionElement = value; OnPropertyChanged("BaseDefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _BaseDefinitionElement;
        
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
                  BaseDefinitionElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("BaseDefinition");
            }
        }
        
        /// <summary>
        /// specialization | constraint - How relates to base definition
        /// </summary>
        [FhirElement("derivation", InSummary=true, Order=330)]
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
        [FhirElement("snapshot", Order=340)]
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
        [FhirElement("differential", Order=350)]
        [DataMember]
        public Hl7.Fhir.Model.StructureDefinition.DifferentialComponent Differential
        {
            get { return _Differential; }
            set { _Differential = value; OnPropertyChanged("Differential"); }
        }
        
        private Hl7.Fhir.Model.StructureDefinition.DifferentialComponent _Differential;
        

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_9 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "children().element.where(path.contains('.').not()).label.empty() and children().element.where(path.contains('.').not()).code.empty() and children().element.where(path.contains('.').not()).requirements.empty()",
            Key = "sdf-9",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "In any snapshot or differential, no label, code or requirements on an element without a \".\" in the path (e.g. the first element)",
            Xpath = "not(exists(f:snapshot/f:element[not(contains(f:path/@value, '.')) and (f:label or f:code or f:requirements)])) and not(exists(f:differential/f:element[not(contains(f:path/@value, '.')) and (f:label or f:code or f:requirements)]))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_15A = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "(kind!='logical'  and differential.element.first().path.contains('.').not()) implies differential.element.first().type.empty()",
            Key = "sdf-15a",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If the first element in a differential has no \".\" in the path and it's not a logical model, it has no type",
            Xpath = "f:kind/@value='logical' or not(f:differential/f:element[1][not(contains(f:path/@value, '.'))]/f:type)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_19 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "url.startsWith('http://hl7.org/fhir/StructureDefinition') implies (differential.element.type.code.all(hasValue() implies matches('^[a-zA-Z0-9]+$')) and snapshot.element.type.code.all(hasValue() implies matches('^[a-zA-Z0-9]+$')))",
            Key = "sdf-19",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "FHIR Specification models only use FHIR defined types",
            Xpath = "not(starts-with(f:url/@value, 'http://hl7.org/fhir/StructureDefinition')) or count(f:differential/f:element/f:type/f:code[@value and not(matches(string(@value), '^[a-zA-Z0-9]+$'))]|f:snapshot/f:element/f:type/f:code[@value and not(matches(string(@value), '^[a-zA-Z0-9]+$'))]) =0"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_16 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "snapshot.element.all(id) and snapshot.element.id.trace('ids').isDistinct()",
            Key = "sdf-16",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All element definitions must have unique ids (snapshot)",
            Xpath = "count(f:snapshot/f:element)=count(f:snapshot/f:element/@id) and (count(f:snapshot/f:element)=count(distinct-values(f:snapshot/f:element/@id)))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_15 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "kind!='logical'  implies snapshot.element.first().type.empty()",
            Key = "sdf-15",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "The first element in a snapshot has no type unless model is a logical model.",
            Xpath = "f:kind/@value='logical' or not(f:snapshot/f:element[1]/f:type)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_18 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "contextInvariant.exists() implies type = 'Extension'",
            Key = "sdf-18",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Context Invariants can only be used for extensions",
            Xpath = "not(exists(f:contextInvariant)) or (f:type/@value = 'Extension')"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_17 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "differential.element.all(id) and differential.element.id.trace('ids').isDistinct()",
            Key = "sdf-17",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All element definitions must have unique ids (diff)",
            Xpath = "count(f:differential/f:element)=count(f:differential/f:element/@id) and (count(f:differential/f:element)=count(distinct-values(f:differential/f:element/@id)))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_23 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "(snapshot | differential).element.all(path.contains('.').not() implies sliceName.empty())",
            Key = "sdf-23",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "No slice name on root",
            Xpath = "count(*[self::snapshot or self::differential]/f:element[not(contains(f:path/@value, '.')) and f:sliceName])=0"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_11 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "kind != 'logical' implies snapshot.empty() or snapshot.element.first().path = type",
            Key = "sdf-11",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If there's a type, its content must match the path name in the first element of a snapshot",
            Xpath = "(f:kind/@value = 'logical') or not(exists(f:snapshot)) or (f:type/@value = f:snapshot/f:element[1]/f:path/@value)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_22 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "url.startsWith('http://hl7.org/fhir/StructureDefinition') implies (snapshot.element.defaultValue.empty() and differential.element.defaultValue.empty())",
            Key = "sdf-22",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "FHIR Specification models never have default values",
            Xpath = "not(starts-with(f:url/@value, 'http://hl7.org/fhir/StructureDefinition')) or (not(exists(f:snapshot/f:element/*[starts-with(local-name(), 'defaultValue')])) and not(exists(f:differential/f:element/*[starts-with(local-name(), 'defaultValue')])))"
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
            Expression = "derivation = 'constraint' or snapshot.element.select(path).isDistinct()",
            Key = "sdf-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Element paths must be unique unless the structure is a constraint",
            Xpath = "(f:derivation/@value = 'constraint') or (count(f:snapshot/f:element) = count(distinct-values(f:snapshot/f:element/f:path/@value)))"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_21 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "differential.element.defaultValue.exists() implies (derivation = 'specialization')",
            Key = "sdf-21",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Default values can only be specified on specializations",
            Xpath = "not(exists(f:differential/f:element/*[starts-with(local-name(), 'defaultValue')])) or (f:derivation/@value = 'specialization')"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_0 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "sdf-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
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
            Expression = "type != 'Extension' or derivation = 'specialization' or (context.exists())",
            Key = "sdf-5",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If the structure defines an extension then the structure must have context information",
            Xpath = "not(f:type/@value = 'extension') or (f:derivation/@value = 'specialization') or (exists(f:context))"
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
            Human = "Must have at least a name or a uri (or both)",
            Xpath = "exists(f:uri) or exists(f:name)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_8 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "snapshot.all((%resource.kind = 'logical' or element.first().path = %resource.type) and element.tail().all(path.startsWith(%resource.snapshot.element.first().path&'.')))",
            Key = "sdf-8",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All snapshot elements must start with the StructureDefinition's specified type for non-logical models, or with the same type name for logical models",
            Xpath = "f:element[1]/f:path/@value=parent::f:StructureDefinition/f:type/@value and count(f:element[position()!=1])=count(f:element[position()!=1][starts-with(f:path/@value, concat(ancestor::f:StructureDefinition/f:type/@value, '.'))])"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_3 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "snapshot.all(element.all(definition.exists() and min.exists() and max.exists()))",
            Key = "sdf-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Each element definition in a snapshot must have a formal definition and cardinalities",
            Xpath = "count(f:element) = count(f:element[exists(f:definition) and exists(f:min) and exists(f:max)])"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_8B = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "snapshot.all(element.all(base.exists()))",
            Key = "sdf-8b",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "All snapshot elements must have a base definition",
            Xpath = "count(f:element) = count(f:element/f:base)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_10 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "snapshot.element.all(binding.empty() or binding.valueSet.exists() or binding.description.exists())",
            Key = "sdf-10",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "provide either a binding reference or a description (or both)",
            Xpath = "not(exists(f:binding)) or exists(f:binding/f:valueSet) or exists(f:binding/f:description)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_20 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "differential.all(element.where(path.contains('.').not()).slicing.empty())",
            Key = "sdf-20",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "No slicing on the root element",
            Xpath = "not(f:element[1]/f:slicing)"
        };

        public static ElementDefinition.ConstraintComponent StructureDefinition_SDF_8A = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "differential.all((%resource.kind = 'logical' or element.first().path.startsWith(%resource.type)) and (element.tail().not() or  element.tail().all(path.startsWith(%resource.differential.element.first().path.replaceMatches('\\\\..*','')&'.'))))",
            Key = "sdf-8a",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "In any differential, all the elements must start with the StructureDefinition's specified type for non-logical models, or with the same type name for logical models",
            Xpath = "count(f:element)=count(f:element[f:path/@value=ancestor::f:StructureDefinition/f:type/@value or starts-with(f:path/@value, concat(ancestor::f:StructureDefinition/f:type/@value, '.'))])"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(StructureDefinition_SDF_9);
            InvariantConstraints.Add(StructureDefinition_SDF_15A);
            InvariantConstraints.Add(StructureDefinition_SDF_19);
            InvariantConstraints.Add(StructureDefinition_SDF_16);
            InvariantConstraints.Add(StructureDefinition_SDF_15);
            InvariantConstraints.Add(StructureDefinition_SDF_18);
            InvariantConstraints.Add(StructureDefinition_SDF_17);
            InvariantConstraints.Add(StructureDefinition_SDF_23);
            InvariantConstraints.Add(StructureDefinition_SDF_11);
            InvariantConstraints.Add(StructureDefinition_SDF_22);
            InvariantConstraints.Add(StructureDefinition_SDF_14);
            InvariantConstraints.Add(StructureDefinition_SDF_1);
            InvariantConstraints.Add(StructureDefinition_SDF_21);
            InvariantConstraints.Add(StructureDefinition_SDF_0);
            InvariantConstraints.Add(StructureDefinition_SDF_6);
            InvariantConstraints.Add(StructureDefinition_SDF_5);
            InvariantConstraints.Add(StructureDefinition_SDF_4);
            InvariantConstraints.Add(StructureDefinition_SDF_2);
            InvariantConstraints.Add(StructureDefinition_SDF_8);
            InvariantConstraints.Add(StructureDefinition_SDF_3);
            InvariantConstraints.Add(StructureDefinition_SDF_8B);
            InvariantConstraints.Add(StructureDefinition_SDF_10);
            InvariantConstraints.Add(StructureDefinition_SDF_20);
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
                if(Keyword != null) dest.Keyword = new List<Hl7.Fhir.Model.Coding>(Keyword.DeepCopy());
                if(FhirVersionElement != null) dest.FhirVersionElement = (Code<Hl7.Fhir.Model.FHIRVersion>)FhirVersionElement.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.StructureDefinition.MappingComponent>(Mapping.DeepCopy());
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.StructureDefinition.StructureDefinitionKind>)KindElement.DeepCopy();
                if(AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
                if(Context != null) dest.Context = new List<Hl7.Fhir.Model.StructureDefinition.ContextComponent>(Context.DeepCopy());
                if(ContextInvariantElement != null) dest.ContextInvariantElement = new List<Hl7.Fhir.Model.FhirString>(ContextInvariantElement.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirUri)TypeElement.DeepCopy();
                if(BaseDefinitionElement != null) dest.BaseDefinitionElement = (Hl7.Fhir.Model.Canonical)BaseDefinitionElement.DeepCopy();
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
            if( !DeepComparable.Matches(Keyword, otherT.Keyword)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(ContextInvariantElement, otherT.ContextInvariantElement)) return false;
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
            if( !DeepComparable.IsExactly(Keyword, otherT.Keyword)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(ContextInvariantElement, otherT.ContextInvariantElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(BaseDefinitionElement, otherT.BaseDefinitionElement)) return false;
            if( !DeepComparable.IsExactly(DerivationElement, otherT.DerivationElement)) return false;
            if( !DeepComparable.IsExactly(Snapshot, otherT.Snapshot)) return false;
            if( !DeepComparable.IsExactly(Differential, otherT.Differential)) return false;
            
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
				foreach (var elem in Keyword) { if (elem != null) yield return elem; }
				if (FhirVersionElement != null) yield return FhirVersionElement;
				foreach (var elem in Mapping) { if (elem != null) yield return elem; }
				if (KindElement != null) yield return KindElement;
				if (AbstractElement != null) yield return AbstractElement;
				foreach (var elem in Context) { if (elem != null) yield return elem; }
				foreach (var elem in ContextInvariantElement) { if (elem != null) yield return elem; }
				if (TypeElement != null) yield return TypeElement;
				if (BaseDefinitionElement != null) yield return BaseDefinitionElement;
				if (DerivationElement != null) yield return DerivationElement;
				if (Snapshot != null) yield return Snapshot;
				if (Differential != null) yield return Differential;
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
                foreach (var elem in Keyword) { if (elem != null) yield return new ElementValue("keyword", elem); }
                if (FhirVersionElement != null) yield return new ElementValue("fhirVersion", FhirVersionElement);
                foreach (var elem in Mapping) { if (elem != null) yield return new ElementValue("mapping", elem); }
                if (KindElement != null) yield return new ElementValue("kind", KindElement);
                if (AbstractElement != null) yield return new ElementValue("abstract", AbstractElement);
                foreach (var elem in Context) { if (elem != null) yield return new ElementValue("context", elem); }
                foreach (var elem in ContextInvariantElement) { if (elem != null) yield return new ElementValue("contextInvariant", elem); }
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (BaseDefinitionElement != null) yield return new ElementValue("baseDefinition", BaseDefinitionElement);
                if (DerivationElement != null) yield return new ElementValue("derivation", DerivationElement);
                if (Snapshot != null) yield return new ElementValue("snapshot", Snapshot);
                if (Differential != null) yield return new ElementValue("differential", Differential);
            }
        }

    }
    
}
