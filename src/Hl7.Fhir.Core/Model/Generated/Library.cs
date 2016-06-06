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
// Generated for FHIR v1.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Represents a library of quality improvement components
    /// </summary>
    [FhirType("Library", IsResource=true)]
    [DataContract]
    public partial class Library : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Library; } }
        [NotMapped]
        public override string TypeName { get { return "Library"; } }
        
        [FhirType("ModelComponent")]
        [DataContract]
        public partial class ModelComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ModelComponent"; } }
            
            /// <summary>
            /// Name of the model
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
            /// Name of the model
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
            /// The identifier of the model
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// The identifier of the model
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
            /// The version of the model, if any
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
            /// The version of the model, if any
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
            /// Name of the library
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
            /// Name of the library
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
            /// The identifier of the library
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// The identifier of the library
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
            /// The version of the library, if any
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
            /// The version of the library, if any
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
            /// The content of the library
            /// </summary>
            [FhirElement("document", InSummary=true, Order=70, Choice=ChoiceType.DatatypeChoice)]
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
            /// Name of the code system
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
            /// Name of the code system
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
            /// The identifier of the code system
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// The identifier of the code system
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
            /// The version of the code system, if any
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
            /// The version of the code system, if any
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
            /// Name of the value set
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
            /// Name of the value set
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
            /// The identifier of the value set
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentifierElement;
            
            /// <summary>
            /// The identifier of the value set
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
            /// The version of the value set
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
            /// The version of the value set
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
            /// The code system binding for this value set definition
            /// </summary>
            [FhirElement("codeSystem", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> CodeSystemElement
            {
                get { if(_CodeSystemElement==null) _CodeSystemElement = new List<Hl7.Fhir.Model.FhirString>(); return _CodeSystemElement; }
                set { _CodeSystemElement = value; OnPropertyChanged("CodeSystemElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _CodeSystemElement;
            
            /// <summary>
            /// The code system binding for this value set definition
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
        
        
        /// <summary>
        /// The metadata information for the library
        /// </summary>
        [FhirElement("moduleMetadata", InSummary=true, Order=90)]
        [DataMember]
        public ModuleMetadata ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private ModuleMetadata _ModuleMetadata;
        
        /// <summary>
        /// A model used by the library
        /// </summary>
        [FhirElement("model", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Library.ModelComponent> Model
        {
            get { if(_Model==null) _Model = new List<Hl7.Fhir.Model.Library.ModelComponent>(); return _Model; }
            set { _Model = value; OnPropertyChanged("Model"); }
        }
        
        private List<Hl7.Fhir.Model.Library.ModelComponent> _Model;
        
        /// <summary>
        /// A library referenced by this library
        /// </summary>
        [FhirElement("library", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Library.LibraryComponent> Library_
        {
            get { if(_Library_==null) _Library_ = new List<Hl7.Fhir.Model.Library.LibraryComponent>(); return _Library_; }
            set { _Library_ = value; OnPropertyChanged("Library_"); }
        }
        
        private List<Hl7.Fhir.Model.Library.LibraryComponent> _Library_;
        
        /// <summary>
        /// A code system used by the library
        /// </summary>
        [FhirElement("codeSystem", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Library.CodeSystemComponent> CodeSystem
        {
            get { if(_CodeSystem==null) _CodeSystem = new List<Hl7.Fhir.Model.Library.CodeSystemComponent>(); return _CodeSystem; }
            set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
        }
        
        private List<Hl7.Fhir.Model.Library.CodeSystemComponent> _CodeSystem;
        
        /// <summary>
        /// A value set used by the library
        /// </summary>
        [FhirElement("valueSet", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Library.ValueSetComponent> ValueSet
        {
            get { if(_ValueSet==null) _ValueSet = new List<Hl7.Fhir.Model.Library.ValueSetComponent>(); return _ValueSet; }
            set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
        }
        
        private List<Hl7.Fhir.Model.Library.ValueSetComponent> _ValueSet;
        
        /// <summary>
        /// Parameters defined by the library
        /// </summary>
        [FhirElement("parameter", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParameterDefinition> Parameter
        {
            get { if(_Parameter==null) _Parameter = new List<ParameterDefinition>(); return _Parameter; }
            set { _Parameter = value; OnPropertyChanged("Parameter"); }
        }
        
        private List<ParameterDefinition> _Parameter;
        
        /// <summary>
        /// Data requirements of the library
        /// </summary>
        [FhirElement("dataRequirement", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DataRequirement> DataRequirement
        {
            get { if(_DataRequirement==null) _DataRequirement = new List<DataRequirement>(); return _DataRequirement; }
            set { _DataRequirement = value; OnPropertyChanged("DataRequirement"); }
        }
        
        private List<DataRequirement> _DataRequirement;
        
        /// <summary>
        /// The content of the library
        /// </summary>
        [FhirElement("document", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Document
        {
            get { return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Document;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Library;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ModuleMetadata != null) dest.ModuleMetadata = (ModuleMetadata)ModuleMetadata.DeepCopy();
                if(Model != null) dest.Model = new List<Hl7.Fhir.Model.Library.ModelComponent>(Model.DeepCopy());
                if(Library_ != null) dest.Library_ = new List<Hl7.Fhir.Model.Library.LibraryComponent>(Library_.DeepCopy());
                if(CodeSystem != null) dest.CodeSystem = new List<Hl7.Fhir.Model.Library.CodeSystemComponent>(CodeSystem.DeepCopy());
                if(ValueSet != null) dest.ValueSet = new List<Hl7.Fhir.Model.Library.ValueSetComponent>(ValueSet.DeepCopy());
                if(Parameter != null) dest.Parameter = new List<ParameterDefinition>(Parameter.DeepCopy());
                if(DataRequirement != null) dest.DataRequirement = new List<DataRequirement>(DataRequirement.DeepCopy());
                if(Document != null) dest.Document = (Hl7.Fhir.Model.Attachment)Document.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Library());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Library;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Model, otherT.Model)) return false;
            if( !DeepComparable.Matches(Library_, otherT.Library_)) return false;
            if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
            if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.Matches(DataRequirement, otherT.DataRequirement)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Library;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Model, otherT.Model)) return false;
            if( !DeepComparable.IsExactly(Library_, otherT.Library_)) return false;
            if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
            if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.IsExactly(DataRequirement, otherT.DataRequirement)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            
            return true;
        }
        
    }
    
}
