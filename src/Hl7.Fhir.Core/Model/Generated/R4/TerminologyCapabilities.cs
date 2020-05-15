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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// A statement of system capabilities
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "TerminologyCapabilities", IsResource=true)]
    [DataContract]
    public partial class TerminologyCapabilities : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TerminologyCapabilities; } }
        [NotMapped]
        public override string TypeName { get { return "TerminologyCapabilities"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SoftwareComponent")]
        [DataContract]
        public partial class SoftwareComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SoftwareComponent"; } }
            
            /// <summary>
            /// A name the software is known by
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SoftwareComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SoftwareComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
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
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SoftwareComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            
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
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ImplementationComponent")]
        [DataContract]
        public partial class ImplementationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ImplementationComponent"; } }
            
            /// <summary>
            /// Describes this specific instance
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            /// Base URL for the implementation
            /// </summary>
            [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Url UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.Url _UrlElement;
            
            /// <summary>
            /// Base URL for the implementation
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
                        UrlElement = new Hl7.Fhir.Model.Url(value);
                    OnPropertyChanged("Url");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ImplementationComponent");
                base.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DescriptionElement?.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImplementationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.Url)UrlElement.DeepCopy();
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
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImplementationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            
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
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CodeSystemComponent")]
        [DataContract]
        public partial class CodeSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemComponent"; } }
            
            /// <summary>
            /// URI for the Code System
            /// </summary>
            [FhirElement("uri", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _UriElement;
            
            /// <summary>
            /// URI for the Code System
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
                        UriElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// Version of Code System supported
            /// </summary>
            [FhirElement("version", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<VersionComponent> Version
            {
                get { if(_Version==null) _Version = new List<VersionComponent>(); return _Version; }
                set { _Version = value; OnPropertyChanged("Version"); }
            }
            
            private List<VersionComponent> _Version;
            
            /// <summary>
            /// Whether subsumption is supported
            /// </summary>
            [FhirElement("subsumption", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean SubsumptionElement
            {
                get { return _SubsumptionElement; }
                set { _SubsumptionElement = value; OnPropertyChanged("SubsumptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _SubsumptionElement;
            
            /// <summary>
            /// Whether subsumption is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Subsumption
            {
                get { return SubsumptionElement != null ? SubsumptionElement.Value : null; }
                set
                {
                    if (value == null)
                        SubsumptionElement = null;
                    else
                        SubsumptionElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Subsumption");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CodeSystemComponent");
                base.Serialize(sink);
                sink.Element("uri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UriElement?.Serialize(sink);
                sink.BeginList("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Version)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("subsumption", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubsumptionElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.Canonical)UriElement.DeepCopy();
                    if(Version != null) dest.Version = new List<VersionComponent>(Version.DeepCopy());
                    if(SubsumptionElement != null) dest.SubsumptionElement = (Hl7.Fhir.Model.FhirBoolean)SubsumptionElement.DeepCopy();
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
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(Version, otherT.Version)) return false;
                if( !DeepComparable.Matches(SubsumptionElement, otherT.SubsumptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(Version, otherT.Version)) return false;
                if( !DeepComparable.IsExactly(SubsumptionElement, otherT.SubsumptionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UriElement != null) yield return UriElement;
                    foreach (var elem in Version) { if (elem != null) yield return elem; }
                    if (SubsumptionElement != null) yield return SubsumptionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    foreach (var elem in Version) { if (elem != null) yield return new ElementValue("version", elem); }
                    if (SubsumptionElement != null) yield return new ElementValue("subsumption", SubsumptionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "VersionComponent")]
        [DataContract]
        public partial class VersionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "VersionComponent"; } }
            
            /// <summary>
            /// Version identifier for this version
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CodeElement;
            
            /// <summary>
            /// Version identifier for this version
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// If this is the default version for this code system
            /// </summary>
            [FhirElement("isDefault", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsDefaultElement
            {
                get { return _IsDefaultElement; }
                set { _IsDefaultElement = value; OnPropertyChanged("IsDefaultElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _IsDefaultElement;
            
            /// <summary>
            /// If this is the default version for this code system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsDefault
            {
                get { return IsDefaultElement != null ? IsDefaultElement.Value : null; }
                set
                {
                    if (value == null)
                        IsDefaultElement = null;
                    else
                        IsDefaultElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsDefault");
                }
            }
            
            /// <summary>
            /// If compositional grammar is supported
            /// </summary>
            [FhirElement("compositional", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CompositionalElement
            {
                get { return _CompositionalElement; }
                set { _CompositionalElement = value; OnPropertyChanged("CompositionalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _CompositionalElement;
            
            /// <summary>
            /// If compositional grammar is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Compositional
            {
                get { return CompositionalElement != null ? CompositionalElement.Value : null; }
                set
                {
                    if (value == null)
                        CompositionalElement = null;
                    else
                        CompositionalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Compositional");
                }
            }
            
            /// <summary>
            /// Language Displays supported
            /// </summary>
            [FhirElement("language", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> LanguageElement
            {
                get { if(_LanguageElement==null) _LanguageElement = new List<Hl7.Fhir.Model.Code>(); return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private List<Hl7.Fhir.Model.Code> _LanguageElement;
            
            /// <summary>
            /// Language Displays supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Language
            {
                get { return LanguageElement != null ? LanguageElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LanguageElement = null;
                    else
                        LanguageElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Filter Properties supported
            /// </summary>
            [FhirElement("filter", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<FilterComponent> Filter
            {
                get { if(_Filter==null) _Filter = new List<FilterComponent>(); return _Filter; }
                set { _Filter = value; OnPropertyChanged("Filter"); }
            }
            
            private List<FilterComponent> _Filter;
            
            /// <summary>
            /// Properties supported for $lookup
            /// </summary>
            [FhirElement("property", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> PropertyElement
            {
                get { if(_PropertyElement==null) _PropertyElement = new List<Hl7.Fhir.Model.Code>(); return _PropertyElement; }
                set { _PropertyElement = value; OnPropertyChanged("PropertyElement"); }
            }
            
            private List<Hl7.Fhir.Model.Code> _PropertyElement;
            
            /// <summary>
            /// Properties supported for $lookup
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Property
            {
                get { return PropertyElement != null ? PropertyElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PropertyElement = null;
                    else
                        PropertyElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("Property");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("VersionComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CodeElement?.Serialize(sink);
                sink.Element("isDefault", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IsDefaultElement?.Serialize(sink);
                sink.Element("compositional", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CompositionalElement?.Serialize(sink);
                sink.BeginList("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(LanguageElement);
                sink.End();
                sink.BeginList("filter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Filter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("property", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(PropertyElement);
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VersionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.FhirString)CodeElement.DeepCopy();
                    if(IsDefaultElement != null) dest.IsDefaultElement = (Hl7.Fhir.Model.FhirBoolean)IsDefaultElement.DeepCopy();
                    if(CompositionalElement != null) dest.CompositionalElement = (Hl7.Fhir.Model.FhirBoolean)CompositionalElement.DeepCopy();
                    if(LanguageElement != null) dest.LanguageElement = new List<Hl7.Fhir.Model.Code>(LanguageElement.DeepCopy());
                    if(Filter != null) dest.Filter = new List<FilterComponent>(Filter.DeepCopy());
                    if(PropertyElement != null) dest.PropertyElement = new List<Hl7.Fhir.Model.Code>(PropertyElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new VersionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VersionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(IsDefaultElement, otherT.IsDefaultElement)) return false;
                if( !DeepComparable.Matches(CompositionalElement, otherT.CompositionalElement)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(Filter, otherT.Filter)) return false;
                if( !DeepComparable.Matches(PropertyElement, otherT.PropertyElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VersionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(IsDefaultElement, otherT.IsDefaultElement)) return false;
                if( !DeepComparable.IsExactly(CompositionalElement, otherT.CompositionalElement)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(Filter, otherT.Filter)) return false;
                if( !DeepComparable.IsExactly(PropertyElement, otherT.PropertyElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (IsDefaultElement != null) yield return IsDefaultElement;
                    if (CompositionalElement != null) yield return CompositionalElement;
                    foreach (var elem in LanguageElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Filter) { if (elem != null) yield return elem; }
                    foreach (var elem in PropertyElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (IsDefaultElement != null) yield return new ElementValue("isDefault", IsDefaultElement);
                    if (CompositionalElement != null) yield return new ElementValue("compositional", CompositionalElement);
                    foreach (var elem in LanguageElement) { if (elem != null) yield return new ElementValue("language", elem); }
                    foreach (var elem in Filter) { if (elem != null) yield return new ElementValue("filter", elem); }
                    foreach (var elem in PropertyElement) { if (elem != null) yield return new ElementValue("property", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "FilterComponent")]
        [DataContract]
        public partial class FilterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FilterComponent"; } }
            
            /// <summary>
            /// Code of the property supported
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code of the property supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Operations supported for the property
            /// </summary>
            [FhirElement("op", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> OpElement
            {
                get { if(_OpElement==null) _OpElement = new List<Hl7.Fhir.Model.Code>(); return _OpElement; }
                set { _OpElement = value; OnPropertyChanged("OpElement"); }
            }
            
            private List<Hl7.Fhir.Model.Code> _OpElement;
            
            /// <summary>
            /// Operations supported for the property
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Op
            {
                get { return OpElement != null ? OpElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OpElement = null;
                    else
                        OpElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("Op");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FilterComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.BeginList("op", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                sink.Serialize(OpElement);
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(OpElement != null) dest.OpElement = new List<Hl7.Fhir.Model.Code>(OpElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(OpElement, otherT.OpElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(OpElement, otherT.OpElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    foreach (var elem in OpElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    foreach (var elem in OpElement) { if (elem != null) yield return new ElementValue("op", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ExpansionComponent")]
        [DataContract]
        public partial class ExpansionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ExpansionComponent"; } }
            
            /// <summary>
            /// Whether the server can return nested value sets
            /// </summary>
            [FhirElement("hierarchical", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean HierarchicalElement
            {
                get { return _HierarchicalElement; }
                set { _HierarchicalElement = value; OnPropertyChanged("HierarchicalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _HierarchicalElement;
            
            /// <summary>
            /// Whether the server can return nested value sets
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Hierarchical
            {
                get { return HierarchicalElement != null ? HierarchicalElement.Value : null; }
                set
                {
                    if (value == null)
                        HierarchicalElement = null;
                    else
                        HierarchicalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Hierarchical");
                }
            }
            
            /// <summary>
            /// Whether the server supports paging on expansion
            /// </summary>
            [FhirElement("paging", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean PagingElement
            {
                get { return _PagingElement; }
                set { _PagingElement = value; OnPropertyChanged("PagingElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _PagingElement;
            
            /// <summary>
            /// Whether the server supports paging on expansion
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Paging
            {
                get { return PagingElement != null ? PagingElement.Value : null; }
                set
                {
                    if (value == null)
                        PagingElement = null;
                    else
                        PagingElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Paging");
                }
            }
            
            /// <summary>
            /// Allow request for incomplete expansions?
            /// </summary>
            [FhirElement("incomplete", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IncompleteElement
            {
                get { return _IncompleteElement; }
                set { _IncompleteElement = value; OnPropertyChanged("IncompleteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _IncompleteElement;
            
            /// <summary>
            /// Allow request for incomplete expansions?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Incomplete
            {
                get { return IncompleteElement != null ? IncompleteElement.Value : null; }
                set
                {
                    if (value == null)
                        IncompleteElement = null;
                    else
                        IncompleteElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Incomplete");
                }
            }
            
            /// <summary>
            /// Supported expansion parameter
            /// </summary>
            [FhirElement("parameter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ParameterComponent> Parameter
            {
                get { if(_Parameter==null) _Parameter = new List<ParameterComponent>(); return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            
            private List<ParameterComponent> _Parameter;
            
            /// <summary>
            /// Documentation about text searching works
            /// </summary>
            [FhirElement("textFilter", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown TextFilterElement
            {
                get { return _TextFilterElement; }
                set { _TextFilterElement = value; OnPropertyChanged("TextFilterElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _TextFilterElement;
            
            /// <summary>
            /// Documentation about text searching works
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TextFilter
            {
                get { return TextFilterElement != null ? TextFilterElement.Value : null; }
                set
                {
                    if (value == null)
                        TextFilterElement = null;
                    else
                        TextFilterElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("TextFilter");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ExpansionComponent");
                base.Serialize(sink);
                sink.Element("hierarchical", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HierarchicalElement?.Serialize(sink);
                sink.Element("paging", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PagingElement?.Serialize(sink);
                sink.Element("incomplete", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); IncompleteElement?.Serialize(sink);
                sink.BeginList("parameter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Parameter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("textFilter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TextFilterElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExpansionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(HierarchicalElement != null) dest.HierarchicalElement = (Hl7.Fhir.Model.FhirBoolean)HierarchicalElement.DeepCopy();
                    if(PagingElement != null) dest.PagingElement = (Hl7.Fhir.Model.FhirBoolean)PagingElement.DeepCopy();
                    if(IncompleteElement != null) dest.IncompleteElement = (Hl7.Fhir.Model.FhirBoolean)IncompleteElement.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<ParameterComponent>(Parameter.DeepCopy());
                    if(TextFilterElement != null) dest.TextFilterElement = (Hl7.Fhir.Model.Markdown)TextFilterElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ExpansionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExpansionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(HierarchicalElement, otherT.HierarchicalElement)) return false;
                if( !DeepComparable.Matches(PagingElement, otherT.PagingElement)) return false;
                if( !DeepComparable.Matches(IncompleteElement, otherT.IncompleteElement)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.Matches(TextFilterElement, otherT.TextFilterElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExpansionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(HierarchicalElement, otherT.HierarchicalElement)) return false;
                if( !DeepComparable.IsExactly(PagingElement, otherT.PagingElement)) return false;
                if( !DeepComparable.IsExactly(IncompleteElement, otherT.IncompleteElement)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.IsExactly(TextFilterElement, otherT.TextFilterElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (HierarchicalElement != null) yield return HierarchicalElement;
                    if (PagingElement != null) yield return PagingElement;
                    if (IncompleteElement != null) yield return IncompleteElement;
                    foreach (var elem in Parameter) { if (elem != null) yield return elem; }
                    if (TextFilterElement != null) yield return TextFilterElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (HierarchicalElement != null) yield return new ElementValue("hierarchical", HierarchicalElement);
                    if (PagingElement != null) yield return new ElementValue("paging", PagingElement);
                    if (IncompleteElement != null) yield return new ElementValue("incomplete", IncompleteElement);
                    foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
                    if (TextFilterElement != null) yield return new ElementValue("textFilter", TextFilterElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Expansion Parameter name
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
            /// Expansion Parameter name
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
                        NameElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Description of support for parameter
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Description of support for parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if (value == null)
                        DocumentationElement = null;
                    else
                        DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Code)NameElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ValidateCodeComponent")]
        [DataContract]
        public partial class ValidateCodeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ValidateCodeComponent"; } }
            
            /// <summary>
            /// Whether translations are validated
            /// </summary>
            [FhirElement("translations", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean TranslationsElement
            {
                get { return _TranslationsElement; }
                set { _TranslationsElement = value; OnPropertyChanged("TranslationsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _TranslationsElement;
            
            /// <summary>
            /// Whether translations are validated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Translations
            {
                get { return TranslationsElement != null ? TranslationsElement.Value : null; }
                set
                {
                    if (value == null)
                        TranslationsElement = null;
                    else
                        TranslationsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Translations");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ValidateCodeComponent");
                base.Serialize(sink);
                sink.Element("translations", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TranslationsElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValidateCodeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TranslationsElement != null) dest.TranslationsElement = (Hl7.Fhir.Model.FhirBoolean)TranslationsElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ValidateCodeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValidateCodeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TranslationsElement, otherT.TranslationsElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValidateCodeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TranslationsElement, otherT.TranslationsElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TranslationsElement != null) yield return TranslationsElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TranslationsElement != null) yield return new ElementValue("translations", TranslationsElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TranslationComponent")]
        [DataContract]
        public partial class TranslationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TranslationComponent"; } }
            
            /// <summary>
            /// Whether the client must identify the map
            /// </summary>
            [FhirElement("needsMap", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean NeedsMapElement
            {
                get { return _NeedsMapElement; }
                set { _NeedsMapElement = value; OnPropertyChanged("NeedsMapElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _NeedsMapElement;
            
            /// <summary>
            /// Whether the client must identify the map
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? NeedsMap
            {
                get { return NeedsMapElement != null ? NeedsMapElement.Value : null; }
                set
                {
                    if (value == null)
                        NeedsMapElement = null;
                    else
                        NeedsMapElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("NeedsMap");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TranslationComponent");
                base.Serialize(sink);
                sink.Element("needsMap", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NeedsMapElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TranslationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NeedsMapElement != null) dest.NeedsMapElement = (Hl7.Fhir.Model.FhirBoolean)NeedsMapElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TranslationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TranslationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NeedsMapElement, otherT.NeedsMapElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TranslationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NeedsMapElement, otherT.NeedsMapElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NeedsMapElement != null) yield return NeedsMapElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NeedsMapElement != null) yield return new ElementValue("needsMap", NeedsMapElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ClosureComponent")]
        [DataContract]
        public partial class ClosureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ClosureComponent"; } }
            
            /// <summary>
            /// If cross-system closure is supported
            /// </summary>
            [FhirElement("translation", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean TranslationElement
            {
                get { return _TranslationElement; }
                set { _TranslationElement = value; OnPropertyChanged("TranslationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _TranslationElement;
            
            /// <summary>
            /// If cross-system closure is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Translation
            {
                get { return TranslationElement != null ? TranslationElement.Value : null; }
                set
                {
                    if (value == null)
                        TranslationElement = null;
                    else
                        TranslationElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Translation");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ClosureComponent");
                base.Serialize(sink);
                sink.Element("translation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TranslationElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ClosureComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TranslationElement != null) dest.TranslationElement = (Hl7.Fhir.Model.FhirBoolean)TranslationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ClosureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ClosureComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TranslationElement, otherT.TranslationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ClosureComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TranslationElement, otherT.TranslationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TranslationElement != null) yield return TranslationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TranslationElement != null) yield return new ElementValue("translation", TranslationElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Canonical identifier for this terminology capabilities, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this terminology capabilities, represented as a URI (globally unique)
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
        /// Business version of the terminology capabilities
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the terminology capabilities
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
        /// Name for this terminology capabilities (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this terminology capabilities (computer friendly)
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
        /// Name for this terminology capabilities (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this terminology capabilities (human friendly)
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
                if (value == null)
                    ExperimentalElement = null;
                else
                    ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
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
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the terminology capabilities
        /// </summary>
        [FhirElement("description", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the terminology capabilities
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for terminology capabilities (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this terminology capabilities is defined
        /// </summary>
        [FhirElement("purpose", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this terminology capabilities is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if (value == null)
                    PurposeElement = null;
                else
                    PurposeElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CopyrightElement;
        
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
                    CopyrightElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// instance | capability | requirements
        /// </summary>
        [FhirElement("kind", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
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
                if (value == null)
                    KindElement = null;
                else
                    KindElement = new Code<Hl7.Fhir.Model.CapabilityStatementKind>(value);
                OnPropertyChanged("Kind");
            }
        }
        
        /// <summary>
        /// Software that is covered by this terminology capability statement
        /// </summary>
        [FhirElement("software", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public SoftwareComponent Software
        {
            get { return _Software; }
            set { _Software = value; OnPropertyChanged("Software"); }
        }
        
        private SoftwareComponent _Software;
        
        /// <summary>
        /// If this describes a specific instance
        /// </summary>
        [FhirElement("implementation", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public ImplementationComponent Implementation
        {
            get { return _Implementation; }
            set { _Implementation = value; OnPropertyChanged("Implementation"); }
        }
        
        private ImplementationComponent _Implementation;
        
        /// <summary>
        /// Whether lockedDate is supported
        /// </summary>
        [FhirElement("lockedDate", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean LockedDateElement
        {
            get { return _LockedDateElement; }
            set { _LockedDateElement = value; OnPropertyChanged("LockedDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _LockedDateElement;
        
        /// <summary>
        /// Whether lockedDate is supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? LockedDate
        {
            get { return LockedDateElement != null ? LockedDateElement.Value : null; }
            set
            {
                if (value == null)
                    LockedDateElement = null;
                else
                    LockedDateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("LockedDate");
            }
        }
        
        /// <summary>
        /// A code system supported by the server
        /// </summary>
        [FhirElement("codeSystem", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CodeSystemComponent> CodeSystem
        {
            get { if(_CodeSystem==null) _CodeSystem = new List<CodeSystemComponent>(); return _CodeSystem; }
            set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
        }
        
        private List<CodeSystemComponent> _CodeSystem;
        
        /// <summary>
        /// Information about the [ValueSet/$expand](valueset-operation-expand.html) operation
        /// </summary>
        [FhirElement("expansion", Order=280)]
        [DataMember]
        public ExpansionComponent Expansion
        {
            get { return _Expansion; }
            set { _Expansion = value; OnPropertyChanged("Expansion"); }
        }
        
        private ExpansionComponent _Expansion;
        
        /// <summary>
        /// explicit | all
        /// </summary>
        [FhirElement("codeSearch", Order=290)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.CodeSearchSupport> CodeSearchElement
        {
            get { return _CodeSearchElement; }
            set { _CodeSearchElement = value; OnPropertyChanged("CodeSearchElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.CodeSearchSupport> _CodeSearchElement;
        
        /// <summary>
        /// explicit | all
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.CodeSearchSupport? CodeSearch
        {
            get { return CodeSearchElement != null ? CodeSearchElement.Value : null; }
            set
            {
                if (value == null)
                    CodeSearchElement = null;
                else
                    CodeSearchElement = new Code<Hl7.Fhir.Model.R4.CodeSearchSupport>(value);
                OnPropertyChanged("CodeSearch");
            }
        }
        
        /// <summary>
        /// Information about the [ValueSet/$validate-code](valueset-operation-validate-code.html) operation
        /// </summary>
        [FhirElement("validateCode", Order=300)]
        [DataMember]
        public ValidateCodeComponent ValidateCode
        {
            get { return _ValidateCode; }
            set { _ValidateCode = value; OnPropertyChanged("ValidateCode"); }
        }
        
        private ValidateCodeComponent _ValidateCode;
        
        /// <summary>
        /// Information about the [ConceptMap/$translate](conceptmap-operation-translate.html) operation
        /// </summary>
        [FhirElement("translation", Order=310)]
        [DataMember]
        public TranslationComponent Translation
        {
            get { return _Translation; }
            set { _Translation = value; OnPropertyChanged("Translation"); }
        }
        
        private TranslationComponent _Translation;
        
        /// <summary>
        /// Information about the [ConceptMap/$closure](conceptmap-operation-closure.html) operation
        /// </summary>
        [FhirElement("closure", Order=320)]
        [DataMember]
        public ClosureComponent Closure
        {
            get { return _Closure; }
            set { _Closure = value; OnPropertyChanged("Closure"); }
        }
        
        private ClosureComponent _Closure;
    
    
        public static ElementDefinitionConstraint[] TerminologyCapabilities_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "tcp-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "tcp-3",
                severity: ConstraintSeverity.Warning,
                expression: "(kind != 'instance') or implementation.exists()",
                human: "If kind = instance, implementation must be present and software may be present",
                xpath: "not(f:kind/@value='instance') or exists(f:implementation)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "tcp-2",
                severity: ConstraintSeverity.Warning,
                expression: "(description.count() + software.count() + implementation.count()) > 0",
                human: "A Capability Statement SHALL have at least one of description, software, or implementation element.",
                xpath: "count(f:software | f:implementation | f:description) > 0"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "tcp-5",
                severity: ConstraintSeverity.Warning,
                expression: "(kind!='requirements') or (implementation.exists().not() and software.exists().not())",
                human: "If kind = requirements, implementation and software must be absent",
                xpath: "not(f:kind/@value='instance') or (not(exists(f:implementation)) and not(exists(f:software)))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "tcp-4",
                severity: ConstraintSeverity.Warning,
                expression: "(kind != 'capability') or (implementation.exists().not() and software.exists())",
                human: "If kind = capability, implementation must be absent, software must be present",
                xpath: " not(f:kind/@value='instance') or (not(exists(f:implementation)) and exists(f:software))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "tcp-1",
                severity: ConstraintSeverity.Warning,
                expression: "codeSystem.all(version.count() > 1 implies version.all(code.exists()))",
                human: "If there is more than one version, a version code must be defined",
                xpath: "(count(f:version) <= 1) or not(exists(f:version[not(f:code)]))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(TerminologyCapabilities_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TerminologyCapabilities;
        
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
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.CapabilityStatementKind>)KindElement.DeepCopy();
                if(Software != null) dest.Software = (SoftwareComponent)Software.DeepCopy();
                if(Implementation != null) dest.Implementation = (ImplementationComponent)Implementation.DeepCopy();
                if(LockedDateElement != null) dest.LockedDateElement = (Hl7.Fhir.Model.FhirBoolean)LockedDateElement.DeepCopy();
                if(CodeSystem != null) dest.CodeSystem = new List<CodeSystemComponent>(CodeSystem.DeepCopy());
                if(Expansion != null) dest.Expansion = (ExpansionComponent)Expansion.DeepCopy();
                if(CodeSearchElement != null) dest.CodeSearchElement = (Code<Hl7.Fhir.Model.R4.CodeSearchSupport>)CodeSearchElement.DeepCopy();
                if(ValidateCode != null) dest.ValidateCode = (ValidateCodeComponent)ValidateCode.DeepCopy();
                if(Translation != null) dest.Translation = (TranslationComponent)Translation.DeepCopy();
                if(Closure != null) dest.Closure = (ClosureComponent)Closure.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new TerminologyCapabilities());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TerminologyCapabilities;
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
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(Software, otherT.Software)) return false;
            if( !DeepComparable.Matches(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.Matches(LockedDateElement, otherT.LockedDateElement)) return false;
            if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.Matches(Expansion, otherT.Expansion)) return false;
            if( !DeepComparable.Matches(CodeSearchElement, otherT.CodeSearchElement)) return false;
            if( !DeepComparable.Matches(ValidateCode, otherT.ValidateCode)) return false;
            if( !DeepComparable.Matches(Translation, otherT.Translation)) return false;
            if( !DeepComparable.Matches(Closure, otherT.Closure)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TerminologyCapabilities;
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
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(Software, otherT.Software)) return false;
            if( !DeepComparable.IsExactly(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.IsExactly(LockedDateElement, otherT.LockedDateElement)) return false;
            if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.IsExactly(Expansion, otherT.Expansion)) return false;
            if( !DeepComparable.IsExactly(CodeSearchElement, otherT.CodeSearchElement)) return false;
            if( !DeepComparable.IsExactly(ValidateCode, otherT.ValidateCode)) return false;
            if( !DeepComparable.IsExactly(Translation, otherT.Translation)) return false;
            if( !DeepComparable.IsExactly(Closure, otherT.Closure)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("TerminologyCapabilities");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DateElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("kind", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); KindElement?.Serialize(sink);
            sink.Element("software", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Software?.Serialize(sink);
            sink.Element("implementation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Implementation?.Serialize(sink);
            sink.Element("lockedDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LockedDateElement?.Serialize(sink);
            sink.BeginList("codeSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in CodeSystem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("expansion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Expansion?.Serialize(sink);
            sink.Element("codeSearch", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeSearchElement?.Serialize(sink);
            sink.Element("validateCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidateCode?.Serialize(sink);
            sink.Element("translation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Translation?.Serialize(sink);
            sink.Element("closure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Closure?.Serialize(sink);
            sink.End();
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
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (PurposeElement != null) yield return PurposeElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                if (KindElement != null) yield return KindElement;
                if (Software != null) yield return Software;
                if (Implementation != null) yield return Implementation;
                if (LockedDateElement != null) yield return LockedDateElement;
                foreach (var elem in CodeSystem) { if (elem != null) yield return elem; }
                if (Expansion != null) yield return Expansion;
                if (CodeSearchElement != null) yield return CodeSearchElement;
                if (ValidateCode != null) yield return ValidateCode;
                if (Translation != null) yield return Translation;
                if (Closure != null) yield return Closure;
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
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (KindElement != null) yield return new ElementValue("kind", KindElement);
                if (Software != null) yield return new ElementValue("software", Software);
                if (Implementation != null) yield return new ElementValue("implementation", Implementation);
                if (LockedDateElement != null) yield return new ElementValue("lockedDate", LockedDateElement);
                foreach (var elem in CodeSystem) { if (elem != null) yield return new ElementValue("codeSystem", elem); }
                if (Expansion != null) yield return new ElementValue("expansion", Expansion);
                if (CodeSearchElement != null) yield return new ElementValue("codeSearch", CodeSearchElement);
                if (ValidateCode != null) yield return new ElementValue("validateCode", ValidateCode);
                if (Translation != null) yield return new ElementValue("translation", Translation);
                if (Closure != null) yield return new ElementValue("closure", Closure);
            }
        }
    
    }

}
