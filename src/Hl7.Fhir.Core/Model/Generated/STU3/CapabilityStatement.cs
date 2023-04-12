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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A statement of system capabilities
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "CapabilityStatement", IsResource=true)]
    [DataContract]
    public partial class CapabilityStatement : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ICapabilityStatement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CapabilityStatement; } }
        [NotMapped]
        public override string TypeName { get { return "CapabilityStatement"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SoftwareComponent")]
        [DataContract]
        public partial class SoftwareComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementSoftwareComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            
            /// <summary>
            /// Date this version released
            /// </summary>
            [FhirElement("releaseDate", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ReleaseDateElement
            {
                get { return _ReleaseDateElement; }
                set { _ReleaseDateElement = value; OnPropertyChanged("ReleaseDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ReleaseDateElement;
            
            /// <summary>
            /// Date this version released
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SoftwareComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
                sink.Element("releaseDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReleaseDateElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "version":
                        VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "releaseDate":
                        ReleaseDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                    case "releaseDate":
                        ReleaseDateElement = source.PopulateValue(ReleaseDateElement);
                        return true;
                    case "_releaseDate":
                        ReleaseDateElement = source.Populate(ReleaseDateElement);
                        return true;
                }
                return false;
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ImplementationComponent")]
        [DataContract]
        public partial class ImplementationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementImplementationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            /// Base URL for the installation
            /// </summary>
            [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
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
                        UrlElement = new Hl7.Fhir.Model.FhirUri(value);
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
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "url":
                        UrlElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "url":
                        UrlElement = source.PopulateValue(UrlElement);
                        return true;
                    case "_url":
                        UrlElement = source.Populate(UrlElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImplementationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RestComponent")]
        [DataContract]
        public partial class RestComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementRestComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RestComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.ICapabilityStatementSecurityComponent Hl7.Fhir.Model.ICapabilityStatementRestComponent.Security { get { return Security; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementResourceComponent> Hl7.Fhir.Model.ICapabilityStatementRestComponent.Resource { get { return Resource; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSystemInteractionComponent> Hl7.Fhir.Model.ICapabilityStatementRestComponent.Interaction { get { return Interaction; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSearchParamComponent> Hl7.Fhir.Model.ICapabilityStatementRestComponent.SearchParam { get { return SearchParam; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementOperationComponent> Hl7.Fhir.Model.ICapabilityStatementRestComponent.Operation { get { return Operation; } }
            
            /// <summary>
            /// client | server
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.RestfulCapabilityMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.RestfulCapabilityMode> _ModeElement;
            
            /// <summary>
            /// client | server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.RestfulCapabilityMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.RestfulCapabilityMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// General description of implementation
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
            /// General description of implementation
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
            
            /// <summary>
            /// Information about security of implementation
            /// </summary>
            [FhirElement("security", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public SecurityComponent Security
            {
                get { return _Security; }
                set { _Security = value; OnPropertyChanged("Security"); }
            }
            
            private SecurityComponent _Security;
            
            /// <summary>
            /// Resource served on the REST interface
            /// </summary>
            [FhirElement("resource", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ResourceComponent> Resource
            {
                get { if(_Resource==null) _Resource = new List<ResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<ResourceComponent> _Resource;
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("interaction", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SystemInteractionComponent> Interaction
            {
                get { if(_Interaction==null) _Interaction = new List<SystemInteractionComponent>(); return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private List<SystemInteractionComponent> _Interaction;
            
            /// <summary>
            /// Search parameters for searching all resources
            /// </summary>
            [FhirElement("searchParam", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SearchParamComponent> SearchParam
            {
                get { if(_SearchParam==null) _SearchParam = new List<SearchParamComponent>(); return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            
            private List<SearchParamComponent> _SearchParam;
            
            /// <summary>
            /// Definition of an operation or a custom query
            /// </summary>
            [FhirElement("operation", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<OperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<OperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<OperationComponent> _Operation;
            
            /// <summary>
            /// Compartments served/used by system
            /// </summary>
            [FhirElement("compartment", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> CompartmentElement
            {
                get { if(_CompartmentElement==null) _CompartmentElement = new List<Hl7.Fhir.Model.FhirUri>(); return _CompartmentElement; }
                set { _CompartmentElement = value; OnPropertyChanged("CompartmentElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _CompartmentElement;
            
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
                        CompartmentElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Compartment");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RestComponent");
                base.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ModeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.Element("security", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Security?.Serialize(sink);
                sink.BeginList("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Resource)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("interaction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Interaction)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("searchParam", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SearchParam)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("operation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Operation)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("compartment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(CompartmentElement);
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "mode":
                        ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RestfulCapabilityMode>>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "security":
                        Security = source.Get<SecurityComponent>();
                        return true;
                    case "resource":
                        Resource = source.GetList<ResourceComponent>();
                        return true;
                    case "interaction":
                        Interaction = source.GetList<SystemInteractionComponent>();
                        return true;
                    case "searchParam":
                        SearchParam = source.GetList<SearchParamComponent>();
                        return true;
                    case "operation":
                        Operation = source.GetList<OperationComponent>();
                        return true;
                    case "compartment":
                        CompartmentElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                    case "security":
                        Security = source.Populate(Security);
                        return true;
                    case "resource":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "interaction":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "searchParam":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "operation":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "compartment":
                    case "_compartment":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "resource":
                        source.PopulateListItem(Resource, index);
                        return true;
                    case "interaction":
                        source.PopulateListItem(Interaction, index);
                        return true;
                    case "searchParam":
                        source.PopulateListItem(SearchParam, index);
                        return true;
                    case "operation":
                        source.PopulateListItem(Operation, index);
                        return true;
                    case "compartment":
                        source.PopulatePrimitiveListItemValue(CompartmentElement, index);
                        return true;
                    case "_compartment":
                        source.PopulatePrimitiveListItem(CompartmentElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.RestfulCapabilityMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Security != null) dest.Security = (SecurityComponent)Security.DeepCopy();
                    if(Resource != null) dest.Resource = new List<ResourceComponent>(Resource.DeepCopy());
                    if(Interaction != null) dest.Interaction = new List<SystemInteractionComponent>(Interaction.DeepCopy());
                    if(SearchParam != null) dest.SearchParam = new List<SearchParamComponent>(SearchParam.DeepCopy());
                    if(Operation != null) dest.Operation = new List<OperationComponent>(Operation.DeepCopy());
                    if(CompartmentElement != null) dest.CompartmentElement = new List<Hl7.Fhir.Model.FhirUri>(CompartmentElement.DeepCopy());
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
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
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
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
                    if (DocumentationElement != null) yield return DocumentationElement;
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
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                    if (Security != null) yield return new ElementValue("security", Security);
                    foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
                    foreach (var elem in Interaction) { if (elem != null) yield return new ElementValue("interaction", elem); }
                    foreach (var elem in SearchParam) { if (elem != null) yield return new ElementValue("searchParam", elem); }
                    foreach (var elem in Operation) { if (elem != null) yield return new ElementValue("operation", elem); }
                    foreach (var elem in CompartmentElement) { if (elem != null) yield return new ElementValue("compartment", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SecurityComponent")]
        [DataContract]
        public partial class SecurityComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementSecurityComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SecurityComponent"; } }
            
            /// <summary>
            /// Adds CORS Headers (http://enable-cors.org/)
            /// </summary>
            [FhirElement("cors", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        CorsElement = null;
                    else
                        CorsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Cors");
                }
            }
            
            /// <summary>
            /// OAuth | SMART-on-FHIR | NTLM | Basic | Kerberos | Certificates
            /// </summary>
            [FhirElement("service", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
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
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// General description of how security works
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
            /// Certificates associated with security profiles
            /// </summary>
            [FhirElement("certificate", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<CertificateComponent> Certificate
            {
                get { if(_Certificate==null) _Certificate = new List<CertificateComponent>(); return _Certificate; }
                set { _Certificate = value; OnPropertyChanged("Certificate"); }
            }
            
            private List<CertificateComponent> _Certificate;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SecurityComponent");
                base.Serialize(sink);
                sink.Element("cors", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CorsElement?.Serialize(sink);
                sink.BeginList("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Service)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("certificate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Certificate)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "cors":
                        CorsElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "service":
                        Service = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "certificate":
                        Certificate = source.GetList<CertificateComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "cors":
                        CorsElement = source.PopulateValue(CorsElement);
                        return true;
                    case "_cors":
                        CorsElement = source.Populate(CorsElement);
                        return true;
                    case "service":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "certificate":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "service":
                        source.PopulateListItem(Service, index);
                        return true;
                    case "certificate":
                        source.PopulateListItem(Certificate, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CorsElement != null) dest.CorsElement = (Hl7.Fhir.Model.FhirBoolean)CorsElement.DeepCopy();
                    if(Service != null) dest.Service = new List<Hl7.Fhir.Model.CodeableConcept>(Service.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Certificate != null) dest.Certificate = new List<CertificateComponent>(Certificate.DeepCopy());
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
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Certificate, otherT.Certificate)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CorsElement, otherT.CorsElement)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Certificate, otherT.Certificate)) return false;
            
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
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Certificate) { if (elem != null) yield return elem; }
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
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Certificate) { if (elem != null) yield return new ElementValue("certificate", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "CertificateComponent")]
        [DataContract]
        public partial class CertificateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CertificateComponent"; } }
            
            /// <summary>
            /// Mime type for certificates
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _TypeElement;
            
            /// <summary>
            /// Mime type for certificates
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
            /// Actual certificate
            /// </summary>
            [FhirElement("blob", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary BlobElement
            {
                get { return _BlobElement; }
                set { _BlobElement = value; OnPropertyChanged("BlobElement"); }
            }
            
            private Hl7.Fhir.Model.Base64Binary _BlobElement;
            
            /// <summary>
            /// Actual certificate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public byte[] Blob
            {
                get { return BlobElement != null ? BlobElement.Value : null; }
                set
                {
                    if (value == null)
                        BlobElement = null;
                    else
                        BlobElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Blob");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CertificateComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TypeElement?.Serialize(sink);
                sink.Element("blob", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BlobElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type":
                        TypeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "blob":
                        BlobElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "blob":
                        BlobElement = source.PopulateValue(BlobElement);
                        return true;
                    case "_blob":
                        BlobElement = source.Populate(BlobElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CertificateComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(BlobElement != null) dest.BlobElement = (Hl7.Fhir.Model.Base64Binary)BlobElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CertificateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CertificateComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(BlobElement, otherT.BlobElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CertificateComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(BlobElement, otherT.BlobElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (BlobElement != null) yield return BlobElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (BlobElement != null) yield return new ElementValue("blob", BlobElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ResourceComponent")]
        [DataContract]
        public partial class ResourceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementResourceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementResourceInteractionComponent> Hl7.Fhir.Model.ICapabilityStatementResourceComponent.Interaction { get { return Interaction; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSearchParamComponent> Hl7.Fhir.Model.ICapabilityStatementResourceComponent.SearchParam { get { return SearchParam; } }
            
            /// <summary>
            /// A resource type that is supported
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Base System profile for all uses of resource
            /// </summary>
            [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("StructureDefinition")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
            /// <summary>
            /// Additional information about the use of the resource type
            /// </summary>
            [FhirElement("documentation", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DocumentationElement;
            
            /// <summary>
            /// Additional information about the use of the resource type
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
                        DocumentationElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("interaction", Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<ResourceInteractionComponent> Interaction
            {
                get { if(_Interaction==null) _Interaction = new List<ResourceInteractionComponent>(); return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private List<ResourceInteractionComponent> _Interaction;
            
            /// <summary>
            /// no-version | versioned | versioned-update
            /// </summary>
            [FhirElement("versioning", Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResourceVersionPolicy> VersioningElement
            {
                get { return _VersioningElement; }
                set { _VersioningElement = value; OnPropertyChanged("VersioningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResourceVersionPolicy> _VersioningElement;
            
            /// <summary>
            /// no-version | versioned | versioned-update
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResourceVersionPolicy? Versioning
            {
                get { return VersioningElement != null ? VersioningElement.Value : null; }
                set
                {
                    if (value == null)
                        VersioningElement = null;
                    else
                        VersioningElement = new Code<Hl7.Fhir.Model.ResourceVersionPolicy>(value);
                    OnPropertyChanged("Versioning");
                }
            }
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            [FhirElement("readHistory", Order=90)]
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
                    if (value == null)
                        ReadHistoryElement = null;
                    else
                        ReadHistoryElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ReadHistory");
                }
            }
            
            /// <summary>
            /// If update can commit to a new identity
            /// </summary>
            [FhirElement("updateCreate", Order=100)]
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
                    if (value == null)
                        UpdateCreateElement = null;
                    else
                        UpdateCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("UpdateCreate");
                }
            }
            
            /// <summary>
            /// If allows/uses conditional create
            /// </summary>
            [FhirElement("conditionalCreate", Order=110)]
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
                    if (value == null)
                        ConditionalCreateElement = null;
                    else
                        ConditionalCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalCreate");
                }
            }
            
            /// <summary>
            /// not-supported | modified-since | not-match | full-support
            /// </summary>
            [FhirElement("conditionalRead", Order=120)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ConditionalReadStatus> ConditionalReadElement
            {
                get { return _ConditionalReadElement; }
                set { _ConditionalReadElement = value; OnPropertyChanged("ConditionalReadElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ConditionalReadStatus> _ConditionalReadElement;
            
            /// <summary>
            /// not-supported | modified-since | not-match | full-support
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ConditionalReadStatus? ConditionalRead
            {
                get { return ConditionalReadElement != null ? ConditionalReadElement.Value : null; }
                set
                {
                    if (value == null)
                        ConditionalReadElement = null;
                    else
                        ConditionalReadElement = new Code<Hl7.Fhir.Model.ConditionalReadStatus>(value);
                    OnPropertyChanged("ConditionalRead");
                }
            }
            
            /// <summary>
            /// If allows/uses conditional update
            /// </summary>
            [FhirElement("conditionalUpdate", Order=130)]
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
                    if (value == null)
                        ConditionalUpdateElement = null;
                    else
                        ConditionalUpdateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalUpdate");
                }
            }
            
            /// <summary>
            /// not-supported | single | multiple - how conditional delete is supported
            /// </summary>
            [FhirElement("conditionalDelete", Order=140)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ConditionalDeleteStatus> ConditionalDeleteElement
            {
                get { return _ConditionalDeleteElement; }
                set { _ConditionalDeleteElement = value; OnPropertyChanged("ConditionalDeleteElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ConditionalDeleteStatus> _ConditionalDeleteElement;
            
            /// <summary>
            /// not-supported | single | multiple - how conditional delete is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ConditionalDeleteStatus? ConditionalDelete
            {
                get { return ConditionalDeleteElement != null ? ConditionalDeleteElement.Value : null; }
                set
                {
                    if (value == null)
                        ConditionalDeleteElement = null;
                    else
                        ConditionalDeleteElement = new Code<Hl7.Fhir.Model.ConditionalDeleteStatus>(value);
                    OnPropertyChanged("ConditionalDelete");
                }
            }
            
            /// <summary>
            /// literal | logical | resolves | enforced | local
            /// </summary>
            [FhirElement("referencePolicy", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>> ReferencePolicyElement
            {
                get { if(_ReferencePolicyElement==null) _ReferencePolicyElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>>(); return _ReferencePolicyElement; }
                set { _ReferencePolicyElement = value; OnPropertyChanged("ReferencePolicyElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>> _ReferencePolicyElement;
            
            /// <summary>
            /// literal | logical | resolves | enforced | local
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.ReferenceHandlingPolicy?> ReferencePolicy
            {
                get { return ReferencePolicyElement != null ? ReferencePolicyElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ReferencePolicyElement = null;
                    else
                        ReferencePolicyElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>(elem)));
                    OnPropertyChanged("ReferencePolicy");
                }
            }
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            [FhirElement("searchInclude", Order=160)]
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
            [FhirElement("searchRevInclude", Order=170)]
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
            [FhirElement("searchParam", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SearchParamComponent> SearchParam
            {
                get { if(_SearchParam==null) _SearchParam = new List<SearchParamComponent>(); return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            
            private List<SearchParamComponent> _SearchParam;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ResourceComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Profile?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.BeginList("interaction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Interaction)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("versioning", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersioningElement?.Serialize(sink);
                sink.Element("readHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReadHistoryElement?.Serialize(sink);
                sink.Element("updateCreate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UpdateCreateElement?.Serialize(sink);
                sink.Element("conditionalCreate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ConditionalCreateElement?.Serialize(sink);
                sink.Element("conditionalRead", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ConditionalReadElement?.Serialize(sink);
                sink.Element("conditionalUpdate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ConditionalUpdateElement?.Serialize(sink);
                sink.Element("conditionalDelete", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ConditionalDeleteElement?.Serialize(sink);
                sink.BeginList("referencePolicy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(ReferencePolicyElement);
                sink.End();
                sink.BeginList("searchInclude", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(SearchIncludeElement);
                sink.End();
                sink.BeginList("searchRevInclude", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(SearchRevIncludeElement);
                sink.End();
                sink.BeginList("searchParam", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SearchParam)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type":
                        TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>();
                        return true;
                    case "profile":
                        Profile = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.Markdown>();
                        return true;
                    case "interaction":
                        Interaction = source.GetList<ResourceInteractionComponent>();
                        return true;
                    case "versioning":
                        VersioningElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceVersionPolicy>>();
                        return true;
                    case "readHistory":
                        ReadHistoryElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "updateCreate":
                        UpdateCreateElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "conditionalCreate":
                        ConditionalCreateElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "conditionalRead":
                        ConditionalReadElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ConditionalReadStatus>>();
                        return true;
                    case "conditionalUpdate":
                        ConditionalUpdateElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "conditionalDelete":
                        ConditionalDeleteElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ConditionalDeleteStatus>>();
                        return true;
                    case "referencePolicy":
                        ReferencePolicyElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>>();
                        return true;
                    case "searchInclude":
                        SearchIncludeElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "searchRevInclude":
                        SearchRevIncludeElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "searchParam":
                        SearchParam = source.GetList<SearchParamComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "profile":
                        Profile = source.Populate(Profile);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                    case "interaction":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "versioning":
                        VersioningElement = source.PopulateValue(VersioningElement);
                        return true;
                    case "_versioning":
                        VersioningElement = source.Populate(VersioningElement);
                        return true;
                    case "readHistory":
                        ReadHistoryElement = source.PopulateValue(ReadHistoryElement);
                        return true;
                    case "_readHistory":
                        ReadHistoryElement = source.Populate(ReadHistoryElement);
                        return true;
                    case "updateCreate":
                        UpdateCreateElement = source.PopulateValue(UpdateCreateElement);
                        return true;
                    case "_updateCreate":
                        UpdateCreateElement = source.Populate(UpdateCreateElement);
                        return true;
                    case "conditionalCreate":
                        ConditionalCreateElement = source.PopulateValue(ConditionalCreateElement);
                        return true;
                    case "_conditionalCreate":
                        ConditionalCreateElement = source.Populate(ConditionalCreateElement);
                        return true;
                    case "conditionalRead":
                        ConditionalReadElement = source.PopulateValue(ConditionalReadElement);
                        return true;
                    case "_conditionalRead":
                        ConditionalReadElement = source.Populate(ConditionalReadElement);
                        return true;
                    case "conditionalUpdate":
                        ConditionalUpdateElement = source.PopulateValue(ConditionalUpdateElement);
                        return true;
                    case "_conditionalUpdate":
                        ConditionalUpdateElement = source.Populate(ConditionalUpdateElement);
                        return true;
                    case "conditionalDelete":
                        ConditionalDeleteElement = source.PopulateValue(ConditionalDeleteElement);
                        return true;
                    case "_conditionalDelete":
                        ConditionalDeleteElement = source.Populate(ConditionalDeleteElement);
                        return true;
                    case "referencePolicy":
                    case "_referencePolicy":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "searchInclude":
                    case "_searchInclude":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "searchRevInclude":
                    case "_searchRevInclude":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "searchParam":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "interaction":
                        source.PopulateListItem(Interaction, index);
                        return true;
                    case "referencePolicy":
                        source.PopulatePrimitiveListItemValue(ReferencePolicyElement, index);
                        return true;
                    case "_referencePolicy":
                        source.PopulatePrimitiveListItem(ReferencePolicyElement, index);
                        return true;
                    case "searchInclude":
                        source.PopulatePrimitiveListItemValue(SearchIncludeElement, index);
                        return true;
                    case "_searchInclude":
                        source.PopulatePrimitiveListItem(SearchIncludeElement, index);
                        return true;
                    case "searchRevInclude":
                        source.PopulatePrimitiveListItemValue(SearchRevIncludeElement, index);
                        return true;
                    case "_searchRevInclude":
                        source.PopulatePrimitiveListItem(SearchRevIncludeElement, index);
                        return true;
                    case "searchParam":
                        source.PopulateListItem(SearchParam, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResourceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ResourceType>)TypeElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.Markdown)DocumentationElement.DeepCopy();
                    if(Interaction != null) dest.Interaction = new List<ResourceInteractionComponent>(Interaction.DeepCopy());
                    if(VersioningElement != null) dest.VersioningElement = (Code<Hl7.Fhir.Model.ResourceVersionPolicy>)VersioningElement.DeepCopy();
                    if(ReadHistoryElement != null) dest.ReadHistoryElement = (Hl7.Fhir.Model.FhirBoolean)ReadHistoryElement.DeepCopy();
                    if(UpdateCreateElement != null) dest.UpdateCreateElement = (Hl7.Fhir.Model.FhirBoolean)UpdateCreateElement.DeepCopy();
                    if(ConditionalCreateElement != null) dest.ConditionalCreateElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalCreateElement.DeepCopy();
                    if(ConditionalReadElement != null) dest.ConditionalReadElement = (Code<Hl7.Fhir.Model.ConditionalReadStatus>)ConditionalReadElement.DeepCopy();
                    if(ConditionalUpdateElement != null) dest.ConditionalUpdateElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalUpdateElement.DeepCopy();
                    if(ConditionalDeleteElement != null) dest.ConditionalDeleteElement = (Code<Hl7.Fhir.Model.ConditionalDeleteStatus>)ConditionalDeleteElement.DeepCopy();
                    if(ReferencePolicyElement != null) dest.ReferencePolicyElement = new List<Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>>(ReferencePolicyElement.DeepCopy());
                    if(SearchIncludeElement != null) dest.SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(SearchIncludeElement.DeepCopy());
                    if(SearchRevIncludeElement != null) dest.SearchRevIncludeElement = new List<Hl7.Fhir.Model.FhirString>(SearchRevIncludeElement.DeepCopy());
                    if(SearchParam != null) dest.SearchParam = new List<SearchParamComponent>(SearchParam.DeepCopy());
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
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
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
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
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
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Profile != null) yield return Profile;
                    if (DocumentationElement != null) yield return DocumentationElement;
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
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
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
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ResourceInteractionComponent")]
        [DataContract]
        public partial class ResourceInteractionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementResourceInteractionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceInteractionComponent"; } }
            
            /// <summary>
            /// read | vread | update | patch | delete | history-instance | history-type | create | search-type
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.TypeRestfulInteraction> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.TypeRestfulInteraction> _CodeElement;
            
            /// <summary>
            /// read | vread | update | patch | delete | history-instance | history-type | create | search-type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.TypeRestfulInteraction? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.STU3.TypeRestfulInteraction>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
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
            /// Anything special about operation behavior
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
                sink.BeginDataType("ResourceInteractionComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.TypeRestfulInteraction>>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResourceInteractionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.STU3.TypeRestfulInteraction>)CodeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceInteractionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SearchParamComponent")]
        [DataContract]
        public partial class SearchParamComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementSearchParamComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            public Hl7.Fhir.Model.FhirUri DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _DefinitionElement;
            
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
                        DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity | uri
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
            /// number | date | string | token | reference | composite | quantity | uri
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SearchParamType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
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
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Server-specific usage
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
                sink.BeginDataType("SearchParamComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DefinitionElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "definition":
                        DefinitionElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "type":
                        TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParamType>>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "definition":
                        DefinitionElement = source.PopulateValue(DefinitionElement);
                        return true;
                    case "_definition":
                        DefinitionElement = source.Populate(DefinitionElement);
                        return true;
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SearchParamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.SearchParamType>)TypeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
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
                    if (DefinitionElement != null) yield return DefinitionElement;
                    if (TypeElement != null) yield return TypeElement;
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
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SystemInteractionComponent")]
        [DataContract]
        public partial class SystemInteractionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementSystemInteractionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SystemInteractionComponent"; } }
            
            /// <summary>
            /// transaction | batch | search-system | history-system
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.SystemRestfulInteraction> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.SystemRestfulInteraction> _CodeElement;
            
            /// <summary>
            /// transaction | batch | search-system | history-system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.SystemRestfulInteraction? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.STU3.SystemRestfulInteraction>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
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
            /// Anything special about operation behavior
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
                sink.BeginDataType("SystemInteractionComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.SystemRestfulInteraction>>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SystemInteractionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.STU3.SystemRestfulInteraction>)CodeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SystemInteractionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementOperationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            /// <summary>
            /// Name by which the operation/query is invoked
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
            [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("OperationDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Definition;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OperationComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Definition?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "definition":
                        Definition = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "definition":
                        Definition = source.Populate(Definition);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
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
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Definition != null) yield return Definition;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Definition != null) yield return new ElementValue("definition", Definition);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "MessagingComponent")]
        [DataContract]
        public partial class MessagingComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementMessagingComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MessagingComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementEndpointComponent> Hl7.Fhir.Model.ICapabilityStatementMessagingComponent.Endpoint { get { return Endpoint; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSupportedMessageComponent> Hl7.Fhir.Model.ICapabilityStatementMessagingComponent.SupportedMessage { get { return SupportedMessage; } }
            
            /// <summary>
            /// Where messages should be sent
            /// </summary>
            [FhirElement("endpoint", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<EndpointComponent> Endpoint
            {
                get { if(_Endpoint==null) _Endpoint = new List<EndpointComponent>(); return _Endpoint; }
                set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
            }
            
            private List<EndpointComponent> _Endpoint;
            
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
                    if (value == null)
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
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Messaging interface behavior details
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
            
            /// <summary>
            /// Messages supported by this system
            /// </summary>
            [FhirElement("supportedMessage", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SupportedMessageComponent> SupportedMessage
            {
                get { if(_SupportedMessage==null) _SupportedMessage = new List<SupportedMessageComponent>(); return _SupportedMessage; }
                set { _SupportedMessage = value; OnPropertyChanged("SupportedMessage"); }
            }
            
            private List<SupportedMessageComponent> _SupportedMessage;
            
            /// <summary>
            /// Declare support for this event
            /// </summary>
            [FhirElement("event", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<EventComponent> Event
            {
                get { if(_Event==null) _Event = new List<EventComponent>(); return _Event; }
                set { _Event = value; OnPropertyChanged("Event"); }
            }
            
            private List<EventComponent> _Event;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MessagingComponent");
                base.Serialize(sink);
                sink.BeginList("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Endpoint)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("reliableCache", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReliableCacheElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.BeginList("supportedMessage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in SupportedMessage)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("event", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Event)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "endpoint":
                        Endpoint = source.GetList<EndpointComponent>();
                        return true;
                    case "reliableCache":
                        ReliableCacheElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "supportedMessage":
                        SupportedMessage = source.GetList<SupportedMessageComponent>();
                        return true;
                    case "event":
                        Event = source.GetList<EventComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "endpoint":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "reliableCache":
                        ReliableCacheElement = source.PopulateValue(ReliableCacheElement);
                        return true;
                    case "_reliableCache":
                        ReliableCacheElement = source.Populate(ReliableCacheElement);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                    case "supportedMessage":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "event":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "endpoint":
                        source.PopulateListItem(Endpoint, index);
                        return true;
                    case "supportedMessage":
                        source.PopulateListItem(SupportedMessage, index);
                        return true;
                    case "event":
                        source.PopulateListItem(Event, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MessagingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Endpoint != null) dest.Endpoint = new List<EndpointComponent>(Endpoint.DeepCopy());
                    if(ReliableCacheElement != null) dest.ReliableCacheElement = (Hl7.Fhir.Model.UnsignedInt)ReliableCacheElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(SupportedMessage != null) dest.SupportedMessage = new List<SupportedMessageComponent>(SupportedMessage.DeepCopy());
                    if(Event != null) dest.Event = new List<EventComponent>(Event.DeepCopy());
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(SupportedMessage, otherT.SupportedMessage)) return false;
                if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MessagingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.IsExactly(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(SupportedMessage, otherT.SupportedMessage)) return false;
                if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            
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
                    if (DocumentationElement != null) yield return DocumentationElement;
                    foreach (var elem in SupportedMessage) { if (elem != null) yield return elem; }
                    foreach (var elem in Event) { if (elem != null) yield return elem; }
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
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                    foreach (var elem in SupportedMessage) { if (elem != null) yield return new ElementValue("supportedMessage", elem); }
                    foreach (var elem in Event) { if (elem != null) yield return new ElementValue("event", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "EndpointComponent")]
        [DataContract]
        public partial class EndpointComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementEndpointComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            public Hl7.Fhir.Model.FhirUri AddressElement
            {
                get { return _AddressElement; }
                set { _AddressElement = value; OnPropertyChanged("AddressElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _AddressElement;
            
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
                        AddressElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Address");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EndpointComponent");
                base.Serialize(sink);
                sink.Element("protocol", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Protocol?.Serialize(sink);
                sink.Element("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); AddressElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "protocol":
                        Protocol = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "address":
                        AddressElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "protocol":
                        Protocol = source.Populate(Protocol);
                        return true;
                    case "address":
                        AddressElement = source.PopulateValue(AddressElement);
                        return true;
                    case "_address":
                        AddressElement = source.Populate(AddressElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EndpointComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Protocol != null) dest.Protocol = (Hl7.Fhir.Model.Coding)Protocol.DeepCopy();
                    if(AddressElement != null) dest.AddressElement = (Hl7.Fhir.Model.FhirUri)AddressElement.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SupportedMessageComponent")]
        [DataContract]
        public partial class SupportedMessageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementSupportedMessageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SupportedMessageComponent"; } }
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.EventCapabilityMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.EventCapabilityMode> _ModeElement;
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.EventCapabilityMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.EventCapabilityMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Message supported by this system
            /// </summary>
            [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("MessageDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Definition;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SupportedMessageComponent");
                base.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ModeElement?.Serialize(sink);
                sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Definition?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "mode":
                        ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.EventCapabilityMode>>();
                        return true;
                    case "definition":
                        Definition = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                    case "definition":
                        Definition = source.Populate(Definition);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SupportedMessageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.EventCapabilityMode>)ModeElement.DeepCopy();
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
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
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SupportedMessageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (Definition != null) yield return Definition;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (Definition != null) yield return new ElementValue("definition", Definition);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "EventComponent")]
        [DataContract]
        public partial class EventComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EventComponent"; } }
            
            /// <summary>
            /// Event type
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Consequence | Currency | Notification
            /// </summary>
            [FhirElement("category", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory> _CategoryElement;
            
            /// <summary>
            /// Consequence | Currency | Notification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.MessageSignificanceCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if (value == null)
                        CategoryElement = null;
                    else
                        CategoryElement = new Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory>(value);
                    OnPropertyChanged("Category");
                }
            }
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            [FhirElement("mode", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.EventCapabilityMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.EventCapabilityMode> _ModeElement;
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.EventCapabilityMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.EventCapabilityMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Resource that's focus of message
            /// </summary>
            [FhirElement("focus", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResourceType> FocusElement
            {
                get { return _FocusElement; }
                set { _FocusElement = value; OnPropertyChanged("FocusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResourceType> _FocusElement;
            
            /// <summary>
            /// Resource that's focus of message
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResourceType? Focus
            {
                get { return FocusElement != null ? FocusElement.Value : null; }
                set
                {
                    if (value == null)
                        FocusElement = null;
                    else
                        FocusElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
                    OnPropertyChanged("Focus");
                }
            }
            
            /// <summary>
            /// Profile that describes the request
            /// </summary>
            [FhirElement("request", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [References("StructureDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Request
            {
                get { return _Request; }
                set { _Request = value; OnPropertyChanged("Request"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Request;
            
            /// <summary>
            /// Profile that describes the response
            /// </summary>
            [FhirElement("response", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [References("StructureDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Response
            {
                get { return _Response; }
                set { _Response = value; OnPropertyChanged("Response"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Response;
            
            /// <summary>
            /// Endpoint-specific event documentation
            /// </summary>
            [FhirElement("documentation", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Endpoint-specific event documentation
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
                sink.BeginDataType("EventComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CategoryElement?.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ModeElement?.Serialize(sink);
                sink.Element("focus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); FocusElement?.Serialize(sink);
                sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Request?.Serialize(sink);
                sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Response?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "category":
                        CategoryElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory>>();
                        return true;
                    case "mode":
                        ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.EventCapabilityMode>>();
                        return true;
                    case "focus":
                        FocusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>();
                        return true;
                    case "request":
                        Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "response":
                        Response = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "category":
                        CategoryElement = source.PopulateValue(CategoryElement);
                        return true;
                    case "_category":
                        CategoryElement = source.Populate(CategoryElement);
                        return true;
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                    case "focus":
                        FocusElement = source.PopulateValue(FocusElement);
                        return true;
                    case "_focus":
                        FocusElement = source.Populate(FocusElement);
                        return true;
                    case "request":
                        Request = source.Populate(Request);
                        return true;
                    case "response":
                        Response = source.Populate(Response);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EventComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.STU3.MessageSignificanceCategory>)CategoryElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.EventCapabilityMode>)ModeElement.DeepCopy();
                    if(FocusElement != null) dest.FocusElement = (Code<Hl7.Fhir.Model.ResourceType>)FocusElement.DeepCopy();
                    if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                    if(Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EventComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(FocusElement, otherT.FocusElement)) return false;
                if( !DeepComparable.Matches(Request, otherT.Request)) return false;
                if( !DeepComparable.Matches(Response, otherT.Response)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EventComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(FocusElement, otherT.FocusElement)) return false;
                if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
                if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (CategoryElement != null) yield return CategoryElement;
                    if (ModeElement != null) yield return ModeElement;
                    if (FocusElement != null) yield return FocusElement;
                    if (Request != null) yield return Request;
                    if (Response != null) yield return Response;
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CategoryElement != null) yield return new ElementValue("category", CategoryElement);
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (FocusElement != null) yield return new ElementValue("focus", FocusElement);
                    if (Request != null) yield return new ElementValue("request", Request);
                    if (Response != null) yield return new ElementValue("response", Response);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DocumentComponent")]
        [DataContract]
        public partial class DocumentComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICapabilityStatementDocumentComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DocumentComponent"; } }
            
            /// <summary>
            /// producer | consumer
            /// </summary>
            [FhirElement("mode", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DocumentMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DocumentMode> _ModeElement;
            
            /// <summary>
            /// producer | consumer
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DocumentMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.DocumentMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Description of document support
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
            /// Description of document support
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
            
            /// <summary>
            /// Constraint on a resource used in the document
            /// </summary>
            [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [References("StructureDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Profile;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DocumentComponent");
                base.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ModeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Profile?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "mode":
                        ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DocumentMode>>();
                        return true;
                    case "documentation":
                        DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "profile":
                        Profile = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                    case "documentation":
                        DocumentationElement = source.PopulateValue(DocumentationElement);
                        return true;
                    case "_documentation":
                        DocumentationElement = source.Populate(DocumentationElement);
                        return true;
                    case "profile":
                        Profile = source.Populate(Profile);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DocumentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.DocumentMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
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
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DocumentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                    if (Profile != null) yield return Profile;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.ICapabilityStatement.Contact { get { return Contact; } }
        
        [NotMapped]
        Hl7.Fhir.Model.ICapabilityStatementSoftwareComponent Hl7.Fhir.Model.ICapabilityStatement.Software { get { return Software; } }
        
        [NotMapped]
        Hl7.Fhir.Model.ICapabilityStatementImplementationComponent Hl7.Fhir.Model.ICapabilityStatement.Implementation { get { return Implementation; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementRestComponent> Hl7.Fhir.Model.ICapabilityStatement.Rest { get { return Rest; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementMessagingComponent> Hl7.Fhir.Model.ICapabilityStatement.Messaging { get { return Messaging; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementDocumentComponent> Hl7.Fhir.Model.ICapabilityStatement.Document { get { return Document; } }
    
        
        /// <summary>
        /// Logical URI to reference this capability statement (globally unique)
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
        /// Logical URI to reference this capability statement (globally unique)
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
        /// Date this was last changed
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
        /// Date this was last changed
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
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the capability statement
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
        /// Natural language description of the capability statement
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
        /// Context the content is intended to support
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
        /// Intended jurisdiction for capability statement (if applicable)
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
        /// Why this capability statement is defined
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
        /// Why this capability statement is defined
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
        [FhirElement("copyright", Order=220)]
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
        /// Canonical URL of another capability statement this implements
        /// </summary>
        [FhirElement("instantiates", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> InstantiatesElement
        {
            get { if(_InstantiatesElement==null) _InstantiatesElement = new List<Hl7.Fhir.Model.FhirUri>(); return _InstantiatesElement; }
            set { _InstantiatesElement = value; OnPropertyChanged("InstantiatesElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _InstantiatesElement;
        
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
                    InstantiatesElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Instantiates");
            }
        }
        
        /// <summary>
        /// Software that is covered by this capability statement
        /// </summary>
        [FhirElement("software", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
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
        [FhirElement("implementation", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public ImplementationComponent Implementation
        {
            get { return _Implementation; }
            set { _Implementation = value; OnPropertyChanged("Implementation"); }
        }
        
        private ImplementationComponent _Implementation;
        
        /// <summary>
        /// FHIR Version the system uses
        /// </summary>
        [FhirElement("fhirVersion", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private Hl7.Fhir.Model.Id _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version the system uses
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
        /// no | extensions | elements | both
        /// </summary>
        [FhirElement("acceptUnknown", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.UnknownContentCode> AcceptUnknownElement
        {
            get { return _AcceptUnknownElement; }
            set { _AcceptUnknownElement = value; OnPropertyChanged("AcceptUnknownElement"); }
        }
        
        private Code<Hl7.Fhir.Model.UnknownContentCode> _AcceptUnknownElement;
        
        /// <summary>
        /// no | extensions | elements | both
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.UnknownContentCode? AcceptUnknown
        {
            get { return AcceptUnknownElement != null ? AcceptUnknownElement.Value : null; }
            set
            {
                if (value == null)
                    AcceptUnknownElement = null;
                else
                    AcceptUnknownElement = new Code<Hl7.Fhir.Model.UnknownContentCode>(value);
                OnPropertyChanged("AcceptUnknown");
            }
        }
        
        /// <summary>
        /// formats supported (xml | json | ttl | mime type)
        /// </summary>
        [FhirElement("format", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
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
        [FhirElement("patchFormat", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
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
        [FhirElement("implementationGuide", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> ImplementationGuideElement
        {
            get { if(_ImplementationGuideElement==null) _ImplementationGuideElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ImplementationGuideElement; }
            set { _ImplementationGuideElement = value; OnPropertyChanged("ImplementationGuideElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _ImplementationGuideElement;
        
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
                    ImplementationGuideElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("ImplementationGuide");
            }
        }
        
        /// <summary>
        /// Profiles for use cases supported
        /// </summary>
        [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=320)]
        [CLSCompliant(false)]
        [References("StructureDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile
        {
            get { if(_Profile==null) _Profile = new List<Hl7.Fhir.Model.ResourceReference>(); return _Profile; }
            set { _Profile = value; OnPropertyChanged("Profile"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Profile;
        
        /// <summary>
        /// If the endpoint is a RESTful one
        /// </summary>
        [FhirElement("rest", InSummary=Hl7.Fhir.Model.Version.All, Order=330)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RestComponent> Rest
        {
            get { if(_Rest==null) _Rest = new List<RestComponent>(); return _Rest; }
            set { _Rest = value; OnPropertyChanged("Rest"); }
        }
        
        private List<RestComponent> _Rest;
        
        /// <summary>
        /// If messaging is supported
        /// </summary>
        [FhirElement("messaging", InSummary=Hl7.Fhir.Model.Version.All, Order=340)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MessagingComponent> Messaging
        {
            get { if(_Messaging==null) _Messaging = new List<MessagingComponent>(); return _Messaging; }
            set { _Messaging = value; OnPropertyChanged("Messaging"); }
        }
        
        private List<MessagingComponent> _Messaging;
        
        /// <summary>
        /// Document definition
        /// </summary>
        [FhirElement("document", InSummary=Hl7.Fhir.Model.Version.All, Order=350)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DocumentComponent> Document
        {
            get { if(_Document==null) _Document = new List<DocumentComponent>(); return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private List<DocumentComponent> _Document;
    
    
        public static ElementDefinitionConstraint[] CapabilityStatement_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-8",
                severity: ConstraintSeverity.Warning,
                expression: "rest.select(mode).isDistinct()",
                human: "There can only be one REST declaration per mode.",
                xpath: "count(f:rest)=count(distinct-values(f:rest/f:mode/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-7",
                severity: ConstraintSeverity.Warning,
                expression: "document.select(profile.reference&mode).isDistinct()",
                human: "The set of documents must be unique by the combination of profile and mode.",
                xpath: "count(f:document[f:mode/@value='producer'])=count(distinct-values(f:document[f:mode/@value='producer']/f:profile/f:reference/@value)) and count(f:document[f:mode/@value='consumer'])=count(distinct-values(f:document[f:mode/@value='consumer']/f:profile/f:reference/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-15",
                severity: ConstraintSeverity.Warning,
                expression: "implementation.empty() or kind != 'capability'",
                human: "Capability Statements of kind 'instance' do not have implementation elements.",
                xpath: "not(exists(f:implementation)) or (f:kind/@value != 'capability')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-3",
                severity: ConstraintSeverity.Warning,
                expression: "messaging.endpoint.empty() or kind = 'instance'",
                human: "Messaging end-point is required (and is only permitted) when a statement is for an implementation.",
                xpath: "not(exists(f:messaging/f:endpoint)) or f:kind/@value = 'instance'"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-14",
                severity: ConstraintSeverity.Warning,
                expression: "(software.empty() and implementation.empty()) or kind != 'requirements'",
                human: "Capability Statements of kind 'requirements' do not have software or implementation elements.",
                xpath: "not(exists(f:software) or exists(f:implementation)) or (f:kind/@value != 'requirements')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-2",
                severity: ConstraintSeverity.Warning,
                expression: "(description.count() + software.count() + implementation.count()) > 0",
                human: "A Capability Statement SHALL have at least one of description, software, or implementation element.",
                xpath: "count(f:software | f:implementation | f:description) > 0"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-1",
                severity: ConstraintSeverity.Warning,
                expression: "rest.exists() or messaging.exists() or document.exists()",
                human: "A Capability Statement SHALL have at least one of REST, messaging or document element.",
                xpath: "exists(f:rest) or exists(f:messaging) or exists(f:document)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-9",
                severity: ConstraintSeverity.Warning,
                expression: "rest.all(resource.select(type).isDistinct())",
                human: "A given resource can only be described once per RESTful mode.",
                xpath: "count(f:resource)=count(distinct-values(f:resource/f:type/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-12",
                severity: ConstraintSeverity.Warning,
                expression: "rest.resource.all(searchParam.select(name).isDistinct())",
                human: "Search parameter names must be unique in the context of a resource.",
                xpath: "count(f:searchParam)=count(distinct-values(f:searchParam/f:name/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "cpb-16",
                severity: ConstraintSeverity.Warning,
                expression: "messaging.all(supportedMessage.empty() != event.empty())",
                human: "A Capability Statement messaging element SHALL have either supportedMessage or event element, but not both.",
                xpath: "exists(f:supportedMessage) != exists(f:event)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(CapabilityStatement_Constraints);
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
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.CapabilityStatementKind>)KindElement.DeepCopy();
                if(InstantiatesElement != null) dest.InstantiatesElement = new List<Hl7.Fhir.Model.FhirUri>(InstantiatesElement.DeepCopy());
                if(Software != null) dest.Software = (SoftwareComponent)Software.DeepCopy();
                if(Implementation != null) dest.Implementation = (ImplementationComponent)Implementation.DeepCopy();
                if(FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
                if(AcceptUnknownElement != null) dest.AcceptUnknownElement = (Code<Hl7.Fhir.Model.UnknownContentCode>)AcceptUnknownElement.DeepCopy();
                if(FormatElement != null) dest.FormatElement = new List<Hl7.Fhir.Model.Code>(FormatElement.DeepCopy());
                if(PatchFormatElement != null) dest.PatchFormatElement = new List<Hl7.Fhir.Model.Code>(PatchFormatElement.DeepCopy());
                if(ImplementationGuideElement != null) dest.ImplementationGuideElement = new List<Hl7.Fhir.Model.FhirUri>(ImplementationGuideElement.DeepCopy());
                if(Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
                if(Rest != null) dest.Rest = new List<RestComponent>(Rest.DeepCopy());
                if(Messaging != null) dest.Messaging = new List<MessagingComponent>(Messaging.DeepCopy());
                if(Document != null) dest.Document = new List<DocumentComponent>(Document.DeepCopy());
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
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(InstantiatesElement, otherT.InstantiatesElement)) return false;
            if( !DeepComparable.Matches(Software, otherT.Software)) return false;
            if( !DeepComparable.Matches(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(AcceptUnknownElement, otherT.AcceptUnknownElement)) return false;
            if( !DeepComparable.Matches(FormatElement, otherT.FormatElement)) return false;
            if( !DeepComparable.Matches(PatchFormatElement, otherT.PatchFormatElement)) return false;
            if( !DeepComparable.Matches(ImplementationGuideElement, otherT.ImplementationGuideElement)) return false;
            if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
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
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesElement, otherT.InstantiatesElement)) return false;
            if( !DeepComparable.IsExactly(Software, otherT.Software)) return false;
            if( !DeepComparable.IsExactly(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(AcceptUnknownElement, otherT.AcceptUnknownElement)) return false;
            if( !DeepComparable.IsExactly(FormatElement, otherT.FormatElement)) return false;
            if( !DeepComparable.IsExactly(PatchFormatElement, otherT.PatchFormatElement)) return false;
            if( !DeepComparable.IsExactly(ImplementationGuideElement, otherT.ImplementationGuideElement)) return false;
            if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            if( !DeepComparable.IsExactly(Rest, otherT.Rest)) return false;
            if( !DeepComparable.IsExactly(Messaging, otherT.Messaging)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("CapabilityStatement");
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
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("kind", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); KindElement?.Serialize(sink);
            sink.BeginList("instantiates", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(InstantiatesElement);
            sink.End();
            sink.Element("software", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Software?.Serialize(sink);
            sink.Element("implementation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Implementation?.Serialize(sink);
            sink.Element("fhirVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); FhirVersionElement?.Serialize(sink);
            sink.Element("acceptUnknown", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); AcceptUnknownElement?.Serialize(sink);
            sink.BeginList("format", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            sink.Serialize(FormatElement);
            sink.End();
            sink.BeginList("patchFormat", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(PatchFormatElement);
            sink.End();
            sink.BeginList("implementationGuide", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ImplementationGuideElement);
            sink.End();
            sink.BeginList("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Profile)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("rest", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Rest)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("messaging", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Messaging)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("document", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Document)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "url":
                    UrlElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.PublicationStatus>>();
                    return true;
                case "experimental":
                    ExperimentalElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "publisher":
                    PublisherElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.STU3.ContactDetail>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "useContext":
                    UseContext = source.GetList<Hl7.Fhir.Model.UsageContext>();
                    return true;
                case "jurisdiction":
                    Jurisdiction = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "purpose":
                    PurposeElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "copyright":
                    CopyrightElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "kind":
                    KindElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CapabilityStatementKind>>();
                    return true;
                case "instantiates":
                    InstantiatesElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "software":
                    Software = source.Get<SoftwareComponent>();
                    return true;
                case "implementation":
                    Implementation = source.Get<ImplementationComponent>();
                    return true;
                case "fhirVersion":
                    FhirVersionElement = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "acceptUnknown":
                    AcceptUnknownElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.UnknownContentCode>>();
                    return true;
                case "format":
                    FormatElement = source.GetList<Hl7.Fhir.Model.Code>();
                    return true;
                case "patchFormat":
                    PatchFormatElement = source.GetList<Hl7.Fhir.Model.Code>();
                    return true;
                case "implementationGuide":
                    ImplementationGuideElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "profile":
                    Profile = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "rest":
                    Rest = source.GetList<RestComponent>();
                    return true;
                case "messaging":
                    Messaging = source.GetList<MessagingComponent>();
                    return true;
                case "document":
                    Document = source.GetList<DocumentComponent>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "experimental":
                    ExperimentalElement = source.PopulateValue(ExperimentalElement);
                    return true;
                case "_experimental":
                    ExperimentalElement = source.Populate(ExperimentalElement);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "purpose":
                    PurposeElement = source.PopulateValue(PurposeElement);
                    return true;
                case "_purpose":
                    PurposeElement = source.Populate(PurposeElement);
                    return true;
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "kind":
                    KindElement = source.PopulateValue(KindElement);
                    return true;
                case "_kind":
                    KindElement = source.Populate(KindElement);
                    return true;
                case "instantiates":
                case "_instantiates":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "software":
                    Software = source.Populate(Software);
                    return true;
                case "implementation":
                    Implementation = source.Populate(Implementation);
                    return true;
                case "fhirVersion":
                    FhirVersionElement = source.PopulateValue(FhirVersionElement);
                    return true;
                case "_fhirVersion":
                    FhirVersionElement = source.Populate(FhirVersionElement);
                    return true;
                case "acceptUnknown":
                    AcceptUnknownElement = source.PopulateValue(AcceptUnknownElement);
                    return true;
                case "_acceptUnknown":
                    AcceptUnknownElement = source.Populate(AcceptUnknownElement);
                    return true;
                case "format":
                case "_format":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patchFormat":
                case "_patchFormat":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "implementationGuide":
                case "_implementationGuide":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "profile":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "rest":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "messaging":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "document":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "instantiates":
                    source.PopulatePrimitiveListItemValue(InstantiatesElement, index);
                    return true;
                case "_instantiates":
                    source.PopulatePrimitiveListItem(InstantiatesElement, index);
                    return true;
                case "format":
                    source.PopulatePrimitiveListItemValue(FormatElement, index);
                    return true;
                case "_format":
                    source.PopulatePrimitiveListItem(FormatElement, index);
                    return true;
                case "patchFormat":
                    source.PopulatePrimitiveListItemValue(PatchFormatElement, index);
                    return true;
                case "_patchFormat":
                    source.PopulatePrimitiveListItem(PatchFormatElement, index);
                    return true;
                case "implementationGuide":
                    source.PopulatePrimitiveListItemValue(ImplementationGuideElement, index);
                    return true;
                case "_implementationGuide":
                    source.PopulatePrimitiveListItem(ImplementationGuideElement, index);
                    return true;
                case "profile":
                    source.PopulateListItem(Profile, index);
                    return true;
                case "rest":
                    source.PopulateListItem(Rest, index);
                    return true;
                case "messaging":
                    source.PopulateListItem(Messaging, index);
                    return true;
                case "document":
                    source.PopulateListItem(Document, index);
                    return true;
            }
            return false;
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
                foreach (var elem in InstantiatesElement) { if (elem != null) yield return elem; }
                if (Software != null) yield return Software;
                if (Implementation != null) yield return Implementation;
                if (FhirVersionElement != null) yield return FhirVersionElement;
                if (AcceptUnknownElement != null) yield return AcceptUnknownElement;
                foreach (var elem in FormatElement) { if (elem != null) yield return elem; }
                foreach (var elem in PatchFormatElement) { if (elem != null) yield return elem; }
                foreach (var elem in ImplementationGuideElement) { if (elem != null) yield return elem; }
                foreach (var elem in Profile) { if (elem != null) yield return elem; }
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
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (KindElement != null) yield return new ElementValue("kind", KindElement);
                foreach (var elem in InstantiatesElement) { if (elem != null) yield return new ElementValue("instantiates", elem); }
                if (Software != null) yield return new ElementValue("software", Software);
                if (Implementation != null) yield return new ElementValue("implementation", Implementation);
                if (FhirVersionElement != null) yield return new ElementValue("fhirVersion", FhirVersionElement);
                if (AcceptUnknownElement != null) yield return new ElementValue("acceptUnknown", AcceptUnknownElement);
                foreach (var elem in FormatElement) { if (elem != null) yield return new ElementValue("format", elem); }
                foreach (var elem in PatchFormatElement) { if (elem != null) yield return new ElementValue("patchFormat", elem); }
                foreach (var elem in ImplementationGuideElement) { if (elem != null) yield return new ElementValue("implementationGuide", elem); }
                foreach (var elem in Profile) { if (elem != null) yield return new ElementValue("profile", elem); }
                foreach (var elem in Rest) { if (elem != null) yield return new ElementValue("rest", elem); }
                foreach (var elem in Messaging) { if (elem != null) yield return new ElementValue("messaging", elem); }
                foreach (var elem in Document) { if (elem != null) yield return new ElementValue("document", elem); }
            }
        }
    
    }

}
