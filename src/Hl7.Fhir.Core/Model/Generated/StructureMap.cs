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
    /// A Map of relationships between 2 structures that can be used to transform data
    /// </summary>
    [FhirType("StructureMap", IsResource=true)]
    [DataContract]
    public partial class StructureMap : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.StructureMap; } }
        [NotMapped]
        public override string TypeName { get { return "StructureMap"; } }
        
        /// <summary>
        /// How the referenced structure is used in this mapping
        /// (url: http://hl7.org/fhir/ValueSet/map-model-mode)
        /// </summary>
        [FhirEnumeration("StructureMapModelMode")]
        public enum StructureMapModelMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-model-mode)
            /// </summary>
            [EnumLiteral("source"), Description("Source Structure Definition")]
            Source,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-model-mode)
            /// </summary>
            [EnumLiteral("queried"), Description("Queried Structure Definition")]
            Queried,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-model-mode)
            /// </summary>
            [EnumLiteral("target"), Description("Target Structure Definition")]
            Target,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-model-mode)
            /// </summary>
            [EnumLiteral("produced"), Description("Produced Structure Definition")]
            Produced,
        }

        /// <summary>
        /// Mode for this instance of data
        /// (url: http://hl7.org/fhir/ValueSet/map-input-mode)
        /// </summary>
        [FhirEnumeration("StructureMapInputMode")]
        public enum StructureMapInputMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-input-mode)
            /// </summary>
            [EnumLiteral("source"), Description("Source Instance")]
            Source,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-input-mode)
            /// </summary>
            [EnumLiteral("target"), Description("Target Instance")]
            Target,
        }

        /// <summary>
        /// How to interpret the context
        /// (url: http://hl7.org/fhir/ValueSet/map-context-type)
        /// </summary>
        [FhirEnumeration("StructureMapContextType")]
        public enum StructureMapContextType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-context-type)
            /// </summary>
            [EnumLiteral("type"), Description("Type")]
            Type,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-context-type)
            /// </summary>
            [EnumLiteral("variable"), Description("Variable")]
            Variable,
        }

        /// <summary>
        /// If field is a list, how to manage the list
        /// (url: http://hl7.org/fhir/ValueSet/map-list-mode)
        /// </summary>
        [FhirEnumeration("StructureMapListMode")]
        public enum StructureMapListMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-list-mode)
            /// </summary>
            [EnumLiteral("first"), Description("First")]
            First,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-list-mode)
            /// </summary>
            [EnumLiteral("share"), Description("Share")]
            Share,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-list-mode)
            /// </summary>
            [EnumLiteral("last"), Description("Last")]
            Last,
        }

        /// <summary>
        /// How data is copied / created
        /// (url: http://hl7.org/fhir/ValueSet/map-transform)
        /// </summary>
        [FhirEnumeration("StructureMapTransform")]
        public enum StructureMapTransform
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("create"), Description("create")]
            Create,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("copy"), Description("copy")]
            Copy,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("truncate"), Description("truncate")]
            Truncate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("escape"), Description("escape")]
            Escape,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("cast"), Description("cast")]
            Cast,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("append"), Description("append")]
            Append,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("translate"), Description("translate")]
            Translate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("reference"), Description("reference")]
            Reference,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("dateOp"), Description("dateOp")]
            DateOp,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("uuid"), Description("uuid")]
            Uuid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("pointer"), Description("pointer")]
            Pointer,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/map-transform)
            /// </summary>
            [EnumLiteral("evaluate"), Description("evaluate")]
            Evaluate,
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
                    if(value == null)
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
        
        
        [FhirType("StructureComponent")]
        [DataContract]
        public partial class StructureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StructureComponent"; } }
            
            /// <summary>
            /// Canonical URL for structure definition
            /// </summary>
            [FhirElement("url", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Canonical URL for structure definition
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
            /// source | queried | target | produced
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMap.StructureMapModelMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMap.StructureMapModelMode> _ModeElement;
            
            /// <summary>
            /// source | queried | target | produced
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMap.StructureMapModelMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new Code<Hl7.Fhir.Model.StructureMap.StructureMapModelMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Documentation on use of structure
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
            /// Documentation on use of structure
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StructureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.StructureMap.StructureMapModelMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StructureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StructureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StructureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// Descriptive name for a user
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Descriptive name for a user
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
                      NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Another group that this group adds rules to
            /// </summary>
            [FhirElement("extends", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Id ExtendsElement
            {
                get { return _ExtendsElement; }
                set { _ExtendsElement = value; OnPropertyChanged("ExtendsElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ExtendsElement;
            
            /// <summary>
            /// Another group that this group adds rules to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Extends
            {
                get { return ExtendsElement != null ? ExtendsElement.Value : null; }
                set
                {
                    if(value == null)
                      ExtendsElement = null; 
                    else
                      ExtendsElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Extends");
                }
            }
            
            /// <summary>
            /// Documentation for this group
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Documentation for this group
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
            /// Named instance provided when invoking the map
            /// </summary>
            [FhirElement("input", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.InputComponent> Input
            {
                get { if(_Input==null) _Input = new List<Hl7.Fhir.Model.StructureMap.InputComponent>(); return _Input; }
                set { _Input = value; OnPropertyChanged("Input"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.InputComponent> _Input;
            
            /// <summary>
            /// Transform Rule from source to target
            /// </summary>
            [FhirElement("rule", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.RuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.StructureMap.RuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.RuleComponent> _Rule;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(ExtendsElement != null) dest.ExtendsElement = (Hl7.Fhir.Model.Id)ExtendsElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Input != null) dest.Input = new List<Hl7.Fhir.Model.StructureMap.InputComponent>(Input.DeepCopy());
                    if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.StructureMap.RuleComponent>(Rule.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ExtendsElement, otherT.ExtendsElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Input, otherT.Input)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ExtendsElement, otherT.ExtendsElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Input, otherT.Input)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("InputComponent")]
        [DataContract]
        public partial class InputComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InputComponent"; } }
            
            /// <summary>
            /// Name for this instance of data
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Name for this instance of data
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
                      NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Type for this instance of data
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// Type for this instance of data
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
                      TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// source | target
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMap.StructureMapInputMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMap.StructureMapInputMode> _ModeElement;
            
            /// <summary>
            /// source | target
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMap.StructureMapInputMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new Code<Hl7.Fhir.Model.StructureMap.StructureMapInputMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Documentation for this instance of data
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Documentation for this instance of data
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InputComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.StructureMap.StructureMapInputMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InputComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InputComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InputComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("RuleComponent")]
        [DataContract]
        public partial class RuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RuleComponent"; } }
            
            /// <summary>
            /// Name of the rule for internal references
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Name of the rule for internal references
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
                      NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Source inputs to the mapping
            /// </summary>
            [FhirElement("source", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.SourceComponent> Source
            {
                get { if(_Source==null) _Source = new List<Hl7.Fhir.Model.StructureMap.SourceComponent>(); return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.SourceComponent> _Source;
            
            /// <summary>
            /// Content to create because of this mapping rule
            /// </summary>
            [FhirElement("target", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.TargetComponent> Target
            {
                get { if(_Target==null) _Target = new List<Hl7.Fhir.Model.StructureMap.TargetComponent>(); return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.TargetComponent> _Target;
            
            /// <summary>
            /// Rules contained in this rule
            /// </summary>
            [FhirElement("rule", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.RuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<Hl7.Fhir.Model.StructureMap.RuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.RuleComponent> _Rule;
            
            /// <summary>
            /// Which other rules to apply in the context of this rule
            /// </summary>
            [FhirElement("dependent", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.DependentComponent> Dependent
            {
                get { if(_Dependent==null) _Dependent = new List<Hl7.Fhir.Model.StructureMap.DependentComponent>(); return _Dependent; }
                set { _Dependent = value; OnPropertyChanged("Dependent"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.DependentComponent> _Dependent;
            
            /// <summary>
            /// Documentation for this instance of data
            /// </summary>
            [FhirElement("documentation", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Documentation for this instance of data
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(Source != null) dest.Source = new List<Hl7.Fhir.Model.StructureMap.SourceComponent>(Source.DeepCopy());
                    if(Target != null) dest.Target = new List<Hl7.Fhir.Model.StructureMap.TargetComponent>(Target.DeepCopy());
                    if(Rule != null) dest.Rule = new List<Hl7.Fhir.Model.StructureMap.RuleComponent>(Rule.DeepCopy());
                    if(Dependent != null) dest.Dependent = new List<Hl7.Fhir.Model.StructureMap.DependentComponent>(Dependent.DeepCopy());
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                if( !DeepComparable.Matches(Dependent, otherT.Dependent)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                if( !DeepComparable.IsExactly(Dependent, otherT.Dependent)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SourceComponent")]
        [DataContract]
        public partial class SourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SourceComponent"; } }
            
            /// <summary>
            /// Whether this rule applies if the source isn't found
            /// </summary>
            [FhirElement("required", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RequiredElement;
            
            /// <summary>
            /// Whether this rule applies if the source isn't found
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if(value == null)
                      RequiredElement = null; 
                    else
                      RequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            [FhirElement("context", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id ContextElement
            {
                get { return _ContextElement; }
                set { _ContextElement = value; OnPropertyChanged("ContextElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ContextElement;
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Context
            {
                get { return ContextElement != null ? ContextElement.Value : null; }
                set
                {
                    if(value == null)
                      ContextElement = null; 
                    else
                      ContextElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Context");
                }
            }
            
            /// <summary>
            /// type | variable
            /// </summary>
            [FhirElement("contextType", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType> ContextTypeElement
            {
                get { return _ContextTypeElement; }
                set { _ContextTypeElement = value; OnPropertyChanged("ContextTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType> _ContextTypeElement;
            
            /// <summary>
            /// type | variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMap.StructureMapContextType? ContextType
            {
                get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
                set
                {
                    if(value == null)
                      ContextTypeElement = null; 
                    else
                      ContextTypeElement = new Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType>(value);
                    OnPropertyChanged("ContextType");
                }
            }
            
            /// <summary>
            /// Optional field for this source
            /// </summary>
            [FhirElement("element", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ElementElement
            {
                get { return _ElementElement; }
                set { _ElementElement = value; OnPropertyChanged("ElementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ElementElement;
            
            /// <summary>
            /// Optional field for this source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Element
            {
                get { return ElementElement != null ? ElementElement.Value : null; }
                set
                {
                    if(value == null)
                      ElementElement = null; 
                    else
                      ElementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Element");
                }
            }
            
            /// <summary>
            /// first | share | last
            /// </summary>
            [FhirElement("listMode", InSummary=true, Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode> ListModeElement
            {
                get { return _ListModeElement; }
                set { _ListModeElement = value; OnPropertyChanged("ListModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode> _ListModeElement;
            
            /// <summary>
            /// first | share | last
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMap.StructureMapListMode? ListMode
            {
                get { return ListModeElement != null ? ListModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ListModeElement = null; 
                    else
                      ListModeElement = new Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>(value);
                    OnPropertyChanged("ListMode");
                }
            }
            
            /// <summary>
            /// Named context for field, if a field is specified
            /// </summary>
            [FhirElement("variable", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id VariableElement
            {
                get { return _VariableElement; }
                set { _VariableElement = value; OnPropertyChanged("VariableElement"); }
            }
            
            private Hl7.Fhir.Model.Id _VariableElement;
            
            /// <summary>
            /// Named context for field, if a field is specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Variable
            {
                get { return VariableElement != null ? VariableElement.Value : null; }
                set
                {
                    if(value == null)
                      VariableElement = null; 
                    else
                      VariableElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Variable");
                }
            }
            
            /// <summary>
            /// FluentPath expression  - must be true or the rule does not apply
            /// </summary>
            [FhirElement("condition", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ConditionElement
            {
                get { return _ConditionElement; }
                set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ConditionElement;
            
            /// <summary>
            /// FluentPath expression  - must be true or the rule does not apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Condition
            {
                get { return ConditionElement != null ? ConditionElement.Value : null; }
                set
                {
                    if(value == null)
                      ConditionElement = null; 
                    else
                      ConditionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Condition");
                }
            }
            
            /// <summary>
            /// FluentPath expression  - must be true or the mapping engine throws an error instead of completing
            /// </summary>
            [FhirElement("check", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CheckElement
            {
                get { return _CheckElement; }
                set { _CheckElement = value; OnPropertyChanged("CheckElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CheckElement;
            
            /// <summary>
            /// FluentPath expression  - must be true or the mapping engine throws an error instead of completing
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Check
            {
                get { return CheckElement != null ? CheckElement.Value : null; }
                set
                {
                    if(value == null)
                      CheckElement = null; 
                    else
                      CheckElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Check");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                    if(ContextElement != null) dest.ContextElement = (Hl7.Fhir.Model.Id)ContextElement.DeepCopy();
                    if(ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType>)ContextTypeElement.DeepCopy();
                    if(ElementElement != null) dest.ElementElement = (Hl7.Fhir.Model.FhirString)ElementElement.DeepCopy();
                    if(ListModeElement != null) dest.ListModeElement = (Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>)ListModeElement.DeepCopy();
                    if(VariableElement != null) dest.VariableElement = (Hl7.Fhir.Model.Id)VariableElement.DeepCopy();
                    if(ConditionElement != null) dest.ConditionElement = (Hl7.Fhir.Model.FhirString)ConditionElement.DeepCopy();
                    if(CheckElement != null) dest.CheckElement = (Hl7.Fhir.Model.FhirString)CheckElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.Matches(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.Matches(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.Matches(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.Matches(CheckElement, otherT.CheckElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.IsExactly(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.IsExactly(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.IsExactly(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.IsExactly(CheckElement, otherT.CheckElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TargetComponent")]
        [DataContract]
        public partial class TargetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TargetComponent"; } }
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            [FhirElement("context", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id ContextElement
            {
                get { return _ContextElement; }
                set { _ContextElement = value; OnPropertyChanged("ContextElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ContextElement;
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Context
            {
                get { return ContextElement != null ? ContextElement.Value : null; }
                set
                {
                    if(value == null)
                      ContextElement = null; 
                    else
                      ContextElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Context");
                }
            }
            
            /// <summary>
            /// type | variable
            /// </summary>
            [FhirElement("contextType", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType> ContextTypeElement
            {
                get { return _ContextTypeElement; }
                set { _ContextTypeElement = value; OnPropertyChanged("ContextTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType> _ContextTypeElement;
            
            /// <summary>
            /// type | variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMap.StructureMapContextType? ContextType
            {
                get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
                set
                {
                    if(value == null)
                      ContextTypeElement = null; 
                    else
                      ContextTypeElement = new Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType>(value);
                    OnPropertyChanged("ContextType");
                }
            }
            
            /// <summary>
            /// Field to create in the context
            /// </summary>
            [FhirElement("element", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ElementElement
            {
                get { return _ElementElement; }
                set { _ElementElement = value; OnPropertyChanged("ElementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ElementElement;
            
            /// <summary>
            /// Field to create in the context
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Element
            {
                get { return ElementElement != null ? ElementElement.Value : null; }
                set
                {
                    if(value == null)
                      ElementElement = null; 
                    else
                      ElementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Element");
                }
            }
            
            /// <summary>
            /// Named context for field, if desired, and a field is specified
            /// </summary>
            [FhirElement("variable", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Id VariableElement
            {
                get { return _VariableElement; }
                set { _VariableElement = value; OnPropertyChanged("VariableElement"); }
            }
            
            private Hl7.Fhir.Model.Id _VariableElement;
            
            /// <summary>
            /// Named context for field, if desired, and a field is specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Variable
            {
                get { return VariableElement != null ? VariableElement.Value : null; }
                set
                {
                    if(value == null)
                      VariableElement = null; 
                    else
                      VariableElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Variable");
                }
            }
            
            /// <summary>
            /// first | share | last
            /// </summary>
            [FhirElement("listMode", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>> ListModeElement
            {
                get { if(_ListModeElement==null) _ListModeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>>(); return _ListModeElement; }
                set { _ListModeElement = value; OnPropertyChanged("ListModeElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>> _ListModeElement;
            
            /// <summary>
            /// first | share | last
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.StructureMap.StructureMapListMode?> ListMode
            {
                get { return ListModeElement != null ? ListModeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ListModeElement = null; 
                    else
                      ListModeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>(elem)));
                    OnPropertyChanged("ListMode");
                }
            }
            
            /// <summary>
            /// Internal rule reference for shared list items
            /// </summary>
            [FhirElement("listRuleId", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id ListRuleIdElement
            {
                get { return _ListRuleIdElement; }
                set { _ListRuleIdElement = value; OnPropertyChanged("ListRuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ListRuleIdElement;
            
            /// <summary>
            /// Internal rule reference for shared list items
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ListRuleId
            {
                get { return ListRuleIdElement != null ? ListRuleIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ListRuleIdElement = null; 
                    else
                      ListRuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ListRuleId");
                }
            }
            
            /// <summary>
            /// create | copy +
            /// </summary>
            [FhirElement("transform", InSummary=true, Order=100)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMap.StructureMapTransform> TransformElement
            {
                get { return _TransformElement; }
                set { _TransformElement = value; OnPropertyChanged("TransformElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMap.StructureMapTransform> _TransformElement;
            
            /// <summary>
            /// create | copy +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMap.StructureMapTransform? Transform
            {
                get { return TransformElement != null ? TransformElement.Value : null; }
                set
                {
                    if(value == null)
                      TransformElement = null; 
                    else
                      TransformElement = new Code<Hl7.Fhir.Model.StructureMap.StructureMapTransform>(value);
                    OnPropertyChanged("Transform");
                }
            }
            
            /// <summary>
            /// Parameters to the transform
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.StructureMap.ParameterComponent> Parameter
            {
                get { if(_Parameter==null) _Parameter = new List<Hl7.Fhir.Model.StructureMap.ParameterComponent>(); return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            
            private List<Hl7.Fhir.Model.StructureMap.ParameterComponent> _Parameter;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TargetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ContextElement != null) dest.ContextElement = (Hl7.Fhir.Model.Id)ContextElement.DeepCopy();
                    if(ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.StructureMap.StructureMapContextType>)ContextTypeElement.DeepCopy();
                    if(ElementElement != null) dest.ElementElement = (Hl7.Fhir.Model.FhirString)ElementElement.DeepCopy();
                    if(VariableElement != null) dest.VariableElement = (Hl7.Fhir.Model.Id)VariableElement.DeepCopy();
                    if(ListModeElement != null) dest.ListModeElement = new List<Code<Hl7.Fhir.Model.StructureMap.StructureMapListMode>>(ListModeElement.DeepCopy());
                    if(ListRuleIdElement != null) dest.ListRuleIdElement = (Hl7.Fhir.Model.Id)ListRuleIdElement.DeepCopy();
                    if(TransformElement != null) dest.TransformElement = (Code<Hl7.Fhir.Model.StructureMap.StructureMapTransform>)TransformElement.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.StructureMap.ParameterComponent>(Parameter.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TargetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.Matches(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.Matches(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.Matches(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.Matches(ListRuleIdElement, otherT.ListRuleIdElement)) return false;
                if( !DeepComparable.Matches(TransformElement, otherT.TransformElement)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.IsExactly(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.IsExactly(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.IsExactly(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.IsExactly(ListRuleIdElement, otherT.ListRuleIdElement)) return false;
                if( !DeepComparable.IsExactly(TransformElement, otherT.TransformElement)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                
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
            /// Parameter value - variable or literal
            /// </summary>
            [FhirElement("value", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
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
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DependentComponent")]
        [DataContract]
        public partial class DependentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DependentComponent"; } }
            
            /// <summary>
            /// Name of a rule or group to apply
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Name of a rule or group to apply
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
                      NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Names of variables to pass to the rule or group
            /// </summary>
            [FhirElement("variable", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> VariableElement
            {
                get { if(_VariableElement==null) _VariableElement = new List<Hl7.Fhir.Model.FhirString>(); return _VariableElement; }
                set { _VariableElement = value; OnPropertyChanged("VariableElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _VariableElement;
            
            /// <summary>
            /// Names of variables to pass to the rule or group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Variable
            {
                get { return VariableElement != null ? VariableElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      VariableElement = null; 
                    else
                      VariableElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Variable");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DependentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(VariableElement != null) dest.VariableElement = new List<Hl7.Fhir.Model.FhirString>(VariableElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DependentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DependentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(VariableElement, otherT.VariableElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DependentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(VariableElement, otherT.VariableElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Absolute URL used to reference this StructureMap
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
        /// Absolute URL used to reference this StructureMap
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
        /// Other identifiers for the StructureMap
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
        /// Logical id for this version of the StructureMap
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
        /// Logical id for this version of the StructureMap
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
        /// Informal name for this StructureMap
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
        /// Informal name for this StructureMap
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
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
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
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
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
                if(value == null)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=150)]
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
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details of the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.StructureMap.ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.StructureMap.ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.StructureMap.ContactComponent> _Contact;
        
        /// <summary>
        /// Date for this version of the StructureMap
        /// </summary>
        [FhirElement("date", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for this version of the StructureMap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Natural language description of the StructureMap
        /// </summary>
        [FhirElement("description", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the StructureMap
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
        /// Content intends to support these contexts
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UseContext;
        
        /// <summary>
        /// Scope and Usage this structure map is for
        /// </summary>
        [FhirElement("requirements", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Scope and Usage this structure map is for
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if(value == null)
                  RequirementsElement = null; 
                else
                  RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=210)]
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
                if(value == null)
                  CopyrightElement = null; 
                else
                  CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Structure Definition used by this map
        /// </summary>
        [FhirElement("structure", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.StructureMap.StructureComponent> Structure
        {
            get { if(_Structure==null) _Structure = new List<Hl7.Fhir.Model.StructureMap.StructureComponent>(); return _Structure; }
            set { _Structure = value; OnPropertyChanged("Structure"); }
        }
        
        private List<Hl7.Fhir.Model.StructureMap.StructureComponent> _Structure;
        
        /// <summary>
        /// Other maps used by this map (canonical URLs)
        /// </summary>
        [FhirElement("import", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> ImportElement
        {
            get { if(_ImportElement==null) _ImportElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ImportElement; }
            set { _ImportElement = value; OnPropertyChanged("ImportElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _ImportElement;
        
        /// <summary>
        /// Other maps used by this map (canonical URLs)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Import
        {
            get { return ImportElement != null ? ImportElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  ImportElement = null; 
                else
                  ImportElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Import");
            }
        }
        
        /// <summary>
        /// Named sections for reader convenience
        /// </summary>
        [FhirElement("group", InSummary=true, Order=240)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.StructureMap.GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.StructureMap.GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<Hl7.Fhir.Model.StructureMap.GroupComponent> _Group;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as StructureMap;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.StructureMap.ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(Structure != null) dest.Structure = new List<Hl7.Fhir.Model.StructureMap.StructureComponent>(Structure.DeepCopy());
                if(ImportElement != null) dest.ImportElement = new List<Hl7.Fhir.Model.FhirUri>(ImportElement.DeepCopy());
                if(Group != null) dest.Group = new List<Hl7.Fhir.Model.StructureMap.GroupComponent>(Group.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new StructureMap());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as StructureMap;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Structure, otherT.Structure)) return false;
            if( !DeepComparable.Matches(ImportElement, otherT.ImportElement)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as StructureMap;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Structure, otherT.Structure)) return false;
            if( !DeepComparable.IsExactly(ImportElement, otherT.ImportElement)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            
            return true;
        }
        
    }
    
}
