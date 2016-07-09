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
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Defines the data requirements information for a quality artifact
    /// </summary>
    [FhirType("ModuleDefinition", IsResource=true)]
    [DataContract]
    public partial class ModuleDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ModuleDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ModuleDefinition"; } }
        
        [FhirType("ModelComponent")]
        [DataContract]
        public partial class ModelComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ModelComponent"; } }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("version", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// 
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ModelComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ModelComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ModelComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ModelComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("LibraryComponent")]
        [DataContract]
        public partial class LibraryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "LibraryComponent"; } }
            
            /// <summary>
            /// The local name for the library
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// The local name for the library
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("version", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// 
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
            [FhirElement("document", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Document
            {
                get { return _Document; }
                set { _Document = value; OnPropertyChanged("Document"); }
            }
            
            private Hl7.Fhir.Model.Element _Document;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LibraryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(Document != null) dest.Document = (Hl7.Fhir.Model.Element)Document.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LibraryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LibraryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(Document, otherT.Document)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LibraryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeSystemComponent")]
        [DataContract]
        public partial class CodeSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemComponent"; } }
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("version", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// 
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeSystemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ValueSetComponent")]
        [DataContract]
        public partial class ValueSetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ValueSetComponent"; } }
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("version", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// 
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
            [FhirElement("codeSystem", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> CodeSystemElement
            {
                get { if(_CodeSystemElement==null) _CodeSystemElement = new List<Hl7.Fhir.Model.FhirString>(); return _CodeSystemElement; }
                set { _CodeSystemElement = value; OnPropertyChanged("CodeSystemElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _CodeSystemElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> CodeSystem
            {
                get { return CodeSystemElement != null ? CodeSystemElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      CodeSystemElement = null; 
                    else
                      CodeSystemElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("CodeSystem");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValueSetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(CodeSystemElement != null) dest.CodeSystemElement = new List<Hl7.Fhir.Model.FhirString>(CodeSystemElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValueSetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValueSetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(CodeSystemElement, otherT.CodeSystemElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValueSetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(CodeSystemElement, otherT.CodeSystemElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Code _NameElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("use", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OperationParameterUse> UseElement
            {
                get { return _UseElement; }
                set { _UseElement = value; OnPropertyChanged("UseElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OperationParameterUse> _UseElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OperationParameterUse? Use
            {
                get { return UseElement != null ? UseElement.Value : null; }
                set
                {
                    if(value == null)
                      UseElement = null; 
                    else
                      UseElement = new Code<Hl7.Fhir.Model.OperationParameterUse>(value);
                    OnPropertyChanged("Use");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("documentation", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("type", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.FHIRDefinedType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.FHIRDefinedType> _TypeElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.FHIRDefinedType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.FHIRDefinedType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("profile", Order=80)]
            [References("StructureDefinition")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Code)NameElement.DeepCopy();
                    if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.OperationParameterUse>)UseElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.FHIRDefinedType>)TypeElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DataComponent")]
        [DataContract]
        public partial class DataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DataComponent"; } }
            
            /// <summary>
            /// The type of the required data
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.FHIRDefinedType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.FHIRDefinedType> _TypeElement;
            
            /// <summary>
            /// The type of the required data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.FHIRDefinedType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.FHIRDefinedType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The profile of the required data
            /// </summary>
            [FhirElement("profile", Order=50)]
            [References("StructureDefinition")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
            /// <summary>
            /// Indicates that specific structure elements are referenced by the knowledge module
            /// </summary>
            [FhirElement("mustSupport", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> MustSupportElement
            {
                get { if(_MustSupportElement==null) _MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(); return _MustSupportElement; }
                set { _MustSupportElement = value; OnPropertyChanged("MustSupportElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _MustSupportElement;
            
            /// <summary>
            /// Indicates that specific structure elements are referenced by the knowledge module
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> MustSupport
            {
                get { return MustSupportElement != null ? MustSupportElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      MustSupportElement = null; 
                    else
                      MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("MustSupport");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("codeFilter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ModuleDefinition.CodeFilterComponent> CodeFilter
            {
                get { if(_CodeFilter==null) _CodeFilter = new List<Hl7.Fhir.Model.ModuleDefinition.CodeFilterComponent>(); return _CodeFilter; }
                set { _CodeFilter = value; OnPropertyChanged("CodeFilter"); }
            }
            
            private List<Hl7.Fhir.Model.ModuleDefinition.CodeFilterComponent> _CodeFilter;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("dateFilter", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ModuleDefinition.DateFilterComponent> DateFilter
            {
                get { if(_DateFilter==null) _DateFilter = new List<Hl7.Fhir.Model.ModuleDefinition.DateFilterComponent>(); return _DateFilter; }
                set { _DateFilter = value; OnPropertyChanged("DateFilter"); }
            }
            
            private List<Hl7.Fhir.Model.ModuleDefinition.DateFilterComponent> _DateFilter;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DataComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.FHIRDefinedType>)TypeElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
                    if(MustSupportElement != null) dest.MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(MustSupportElement.DeepCopy());
                    if(CodeFilter != null) dest.CodeFilter = new List<Hl7.Fhir.Model.ModuleDefinition.CodeFilterComponent>(CodeFilter.DeepCopy());
                    if(DateFilter != null) dest.DateFilter = new List<Hl7.Fhir.Model.ModuleDefinition.DateFilterComponent>(DateFilter.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                if( !DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
                if( !DeepComparable.Matches(CodeFilter, otherT.CodeFilter)) return false;
                if( !DeepComparable.Matches(DateFilter, otherT.DateFilter)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DataComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                if( !DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
                if( !DeepComparable.IsExactly(CodeFilter, otherT.CodeFilter)) return false;
                if( !DeepComparable.IsExactly(DateFilter, otherT.DateFilter)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeFilterComponent")]
        [DataContract]
        public partial class CodeFilterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeFilterComponent"; } }
            
            /// <summary>
            /// The code-valued attribute of the filter
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
            /// The code-valued attribute of the filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// The valueset for the code filter
            /// </summary>
            [FhirElement("valueSet", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element ValueSet
            {
                get { return _ValueSet; }
                set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
            }
            
            private Hl7.Fhir.Model.Element _ValueSet;
            
            /// <summary>
            /// The codeableConcepts for the filter
            /// </summary>
            [FhirElement("codeableConcept", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> CodeableConcept
            {
                get { if(_CodeableConcept==null) _CodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CodeableConcept; }
                set { _CodeableConcept = value; OnPropertyChanged("CodeableConcept"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _CodeableConcept;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeFilterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(ValueSet != null) dest.ValueSet = (Hl7.Fhir.Model.Element)ValueSet.DeepCopy();
                    if(CodeableConcept != null) dest.CodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(CodeableConcept.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeFilterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
                if( !DeepComparable.Matches(CodeableConcept, otherT.CodeableConcept)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeFilterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
                if( !DeepComparable.IsExactly(CodeableConcept, otherT.CodeableConcept)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DateFilterComponent")]
        [DataContract]
        public partial class DateFilterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DateFilterComponent"; } }
            
            /// <summary>
            /// The date-valued attribute of the filter
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
            /// The date-valued attribute of the filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// The value of the filter, as a Period or dateTime value
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DateFilterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DateFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DateFilterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DateFilterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
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
        [FhirElement("model", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleDefinition.ModelComponent> Model
        {
            get { if(_Model==null) _Model = new List<Hl7.Fhir.Model.ModuleDefinition.ModelComponent>(); return _Model; }
            set { _Model = value; OnPropertyChanged("Model"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleDefinition.ModelComponent> _Model;
        
        /// <summary>
        /// A library referenced by the module
        /// </summary>
        [FhirElement("library", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleDefinition.LibraryComponent> Library
        {
            get { if(_Library==null) _Library = new List<Hl7.Fhir.Model.ModuleDefinition.LibraryComponent>(); return _Library; }
            set { _Library = value; OnPropertyChanged("Library"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleDefinition.LibraryComponent> _Library;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("codeSystem", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleDefinition.CodeSystemComponent> CodeSystem
        {
            get { if(_CodeSystem==null) _CodeSystem = new List<Hl7.Fhir.Model.ModuleDefinition.CodeSystemComponent>(); return _CodeSystem; }
            set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleDefinition.CodeSystemComponent> _CodeSystem;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("valueSet", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleDefinition.ValueSetComponent> ValueSet
        {
            get { if(_ValueSet==null) _ValueSet = new List<Hl7.Fhir.Model.ModuleDefinition.ValueSetComponent>(); return _ValueSet; }
            set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleDefinition.ValueSetComponent> _ValueSet;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("parameter", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleDefinition.ParameterComponent> Parameter
        {
            get { if(_Parameter==null) _Parameter = new List<Hl7.Fhir.Model.ModuleDefinition.ParameterComponent>(); return _Parameter; }
            set { _Parameter = value; OnPropertyChanged("Parameter"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleDefinition.ParameterComponent> _Parameter;
        
        /// <summary>
        /// Describes a required data item
        /// </summary>
        [FhirElement("data", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ModuleDefinition.DataComponent> Data
        {
            get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.ModuleDefinition.DataComponent>(); return _Data; }
            set { _Data = value; OnPropertyChanged("Data"); }
        }
        
        private List<Hl7.Fhir.Model.ModuleDefinition.DataComponent> _Data;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ModuleDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(Model != null) dest.Model = new List<Hl7.Fhir.Model.ModuleDefinition.ModelComponent>(Model.DeepCopy());
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ModuleDefinition.LibraryComponent>(Library.DeepCopy());
                if(CodeSystem != null) dest.CodeSystem = new List<Hl7.Fhir.Model.ModuleDefinition.CodeSystemComponent>(CodeSystem.DeepCopy());
                if(ValueSet != null) dest.ValueSet = new List<Hl7.Fhir.Model.ModuleDefinition.ValueSetComponent>(ValueSet.DeepCopy());
                if(Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.ModuleDefinition.ParameterComponent>(Parameter.DeepCopy());
                if(Data != null) dest.Data = new List<Hl7.Fhir.Model.ModuleDefinition.DataComponent>(Data.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ModuleDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ModuleDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(Model, otherT.Model)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
            if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.Matches(Data, otherT.Data)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ModuleDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(Model, otherT.Model)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
            if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
            
            return true;
        }
        
    }
    
}
