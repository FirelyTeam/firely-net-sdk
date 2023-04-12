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
    /// A set of rules about how FHIR is used
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ImplementationGuide", IsResource=true)]
    [DataContract]
    public partial class ImplementationGuide : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IImplementationGuide, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImplementationGuide; } }
        [NotMapped]
        public override string TypeName { get { return "ImplementationGuide"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DependsOnComponent")]
        [DataContract]
        public partial class DependsOnComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DependsOnComponent"; } }
            
            /// <summary>
            /// Identity of the IG that this depends on
            /// </summary>
            [FhirElement("uri", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _UriElement;
            
            /// <summary>
            /// Identity of the IG that this depends on
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
            /// NPM Package name for IG this depends on
            /// </summary>
            [FhirElement("packageId", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Id PackageIdElement
            {
                get { return _PackageIdElement; }
                set { _PackageIdElement = value; OnPropertyChanged("PackageIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _PackageIdElement;
            
            /// <summary>
            /// NPM Package name for IG this depends on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PackageId
            {
                get { return PackageIdElement != null ? PackageIdElement.Value : null; }
                set
                {
                    if (value == null)
                        PackageIdElement = null;
                    else
                        PackageIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("PackageId");
                }
            }
            
            /// <summary>
            /// Version of the IG
            /// </summary>
            [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version of the IG
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
                sink.BeginDataType("DependsOnComponent");
                base.Serialize(sink);
                sink.Element("uri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UriElement?.Serialize(sink);
                sink.Element("packageId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PackageIdElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
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
                    case "uri":
                        UriElement = source.Get<Hl7.Fhir.Model.Canonical>();
                        return true;
                    case "packageId":
                        PackageIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "version":
                        VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "uri":
                        UriElement = source.PopulateValue(UriElement);
                        return true;
                    case "_uri":
                        UriElement = source.Populate(UriElement);
                        return true;
                    case "packageId":
                        PackageIdElement = source.PopulateValue(PackageIdElement);
                        return true;
                    case "_packageId":
                        PackageIdElement = source.Populate(PackageIdElement);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DependsOnComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.Canonical)UriElement.DeepCopy();
                    if(PackageIdElement != null) dest.PackageIdElement = (Hl7.Fhir.Model.Id)PackageIdElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DependsOnComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DependsOnComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(PackageIdElement, otherT.PackageIdElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DependsOnComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(PackageIdElement, otherT.PackageIdElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UriElement != null) yield return UriElement;
                    if (PackageIdElement != null) yield return PackageIdElement;
                    if (VersionElement != null) yield return VersionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    if (PackageIdElement != null) yield return new ElementValue("packageId", PackageIdElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GlobalComponent")]
        [DataContract]
        public partial class GlobalComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IImplementationGuideGlobalComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GlobalComponent"; } }
            
            /// <summary>
            /// Type this profile applies to
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
            /// Type this profile applies to
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
            /// Profile that all resources must conform to
            /// </summary>
            [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ProfileElement;
            
            /// <summary>
            /// Profile that all resources must conform to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Profile
            {
                get { return ProfileElement != null ? ProfileElement.Value : null; }
                set
                {
                    if (value == null)
                        ProfileElement = null;
                    else
                        ProfileElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Profile");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GlobalComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ProfileElement?.Serialize(sink);
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
                        ProfileElement = source.Get<Hl7.Fhir.Model.Canonical>();
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
                        ProfileElement = source.PopulateValue(ProfileElement);
                        return true;
                    case "_profile":
                        ProfileElement = source.Populate(ProfileElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GlobalComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ResourceType>)TypeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GlobalComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GlobalComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GlobalComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (ProfileElement != null) yield return ProfileElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DefinitionComponent")]
        [DataContract]
        public partial class DefinitionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DefinitionComponent"; } }
            
            /// <summary>
            /// Grouping used to present related resources in the IG
            /// </summary>
            [FhirElement("grouping", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<GroupingComponent> Grouping
            {
                get { if(_Grouping==null) _Grouping = new List<GroupingComponent>(); return _Grouping; }
                set { _Grouping = value; OnPropertyChanged("Grouping"); }
            }
            
            private List<GroupingComponent> _Grouping;
            
            /// <summary>
            /// Resource in the implementation guide
            /// </summary>
            [FhirElement("resource", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<ResourceComponent> Resource
            {
                get { if(_Resource==null) _Resource = new List<ResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<ResourceComponent> _Resource;
            
            /// <summary>
            /// Page/Section in the Guide
            /// </summary>
            [FhirElement("page", Order=60)]
            [DataMember]
            public PageComponent Page
            {
                get { return _Page; }
                set { _Page = value; OnPropertyChanged("Page"); }
            }
            
            private PageComponent _Page;
            
            /// <summary>
            /// Defines how IG is built by tools
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
            /// A template for building resources
            /// </summary>
            [FhirElement("template", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<TemplateComponent> Template
            {
                get { if(_Template==null) _Template = new List<TemplateComponent>(); return _Template; }
                set { _Template = value; OnPropertyChanged("Template"); }
            }
            
            private List<TemplateComponent> _Template;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DefinitionComponent");
                base.Serialize(sink);
                sink.BeginList("grouping", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Grouping)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Resource)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("page", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Page?.Serialize(sink);
                sink.BeginList("parameter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Parameter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("template", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Template)
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
                    case "grouping":
                        Grouping = source.GetList<GroupingComponent>();
                        return true;
                    case "resource":
                        Resource = source.GetList<ResourceComponent>();
                        return true;
                    case "page":
                        Page = source.Get<PageComponent>();
                        return true;
                    case "parameter":
                        Parameter = source.GetList<ParameterComponent>();
                        return true;
                    case "template":
                        Template = source.GetList<TemplateComponent>();
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
                    case "grouping":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "resource":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "page":
                        Page = source.Populate(Page);
                        return true;
                    case "parameter":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "template":
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
                    case "grouping":
                        source.PopulateListItem(Grouping, index);
                        return true;
                    case "resource":
                        source.PopulateListItem(Resource, index);
                        return true;
                    case "parameter":
                        source.PopulateListItem(Parameter, index);
                        return true;
                    case "template":
                        source.PopulateListItem(Template, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DefinitionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Grouping != null) dest.Grouping = new List<GroupingComponent>(Grouping.DeepCopy());
                    if(Resource != null) dest.Resource = new List<ResourceComponent>(Resource.DeepCopy());
                    if(Page != null) dest.Page = (PageComponent)Page.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<ParameterComponent>(Parameter.DeepCopy());
                    if(Template != null) dest.Template = new List<TemplateComponent>(Template.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DefinitionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DefinitionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Grouping, otherT.Grouping)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Page, otherT.Page)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.Matches(Template, otherT.Template)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DefinitionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Grouping, otherT.Grouping)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Page, otherT.Page)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.IsExactly(Template, otherT.Template)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Grouping) { if (elem != null) yield return elem; }
                    foreach (var elem in Resource) { if (elem != null) yield return elem; }
                    if (Page != null) yield return Page;
                    foreach (var elem in Parameter) { if (elem != null) yield return elem; }
                    foreach (var elem in Template) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Grouping) { if (elem != null) yield return new ElementValue("grouping", elem); }
                    foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
                    if (Page != null) yield return new ElementValue("page", Page);
                    foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
                    foreach (var elem in Template) { if (elem != null) yield return new ElementValue("template", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GroupingComponent")]
        [DataContract]
        public partial class GroupingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GroupingComponent"; } }
            
            /// <summary>
            /// Descriptive name for the package
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
            /// Descriptive name for the package
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
            /// Human readable text describing the package
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
            /// Human readable text describing the package
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GroupingComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
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
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GroupingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupingComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ResourceComponent")]
        [DataContract]
        public partial class ResourceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IImplementationGuideResourceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceComponent"; } }
            
            /// <summary>
            /// Location of the resource
            /// </summary>
            [FhirElement("reference", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Versions this applies to (if different to IG)
            /// </summary>
            [FhirElement("fhirVersion", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.R4.FHIRVersion>> FhirVersionElement
            {
                get { if(_FhirVersionElement==null) _FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>>(); return _FhirVersionElement; }
                set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.R4.FHIRVersion>> _FhirVersionElement;
            
            /// <summary>
            /// Versions this applies to (if different to IG)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.R4.FHIRVersion?> FhirVersion
            {
                get { return FhirVersionElement != null ? FhirVersionElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        FhirVersionElement = null;
                    else
                        FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>(elem)));
                    OnPropertyChanged("FhirVersion");
                }
            }
            
            /// <summary>
            /// Human Name for the resource
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
            /// Human Name for the resource
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
            /// Reason why included in guide
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Reason why included in guide
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
            /// Is an example/What is this an example of?
            /// </summary>
            [FhirElement("example", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical))]
            [DataMember]
            public Hl7.Fhir.Model.Element Example
            {
                get { return _Example; }
                set { _Example = value; OnPropertyChanged("Example"); }
            }
            
            private Hl7.Fhir.Model.Element _Example;
            
            /// <summary>
            /// Grouping this is part of
            /// </summary>
            [FhirElement("groupingId", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id GroupingIdElement
            {
                get { return _GroupingIdElement; }
                set { _GroupingIdElement = value; OnPropertyChanged("GroupingIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _GroupingIdElement;
            
            /// <summary>
            /// Grouping this is part of
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string GroupingId
            {
                get { return GroupingIdElement != null ? GroupingIdElement.Value : null; }
                set
                {
                    if (value == null)
                        GroupingIdElement = null;
                    else
                        GroupingIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("GroupingId");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ResourceComponent");
                base.Serialize(sink);
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Reference?.Serialize(sink);
                sink.BeginList("fhirVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(FhirVersionElement);
                sink.End();
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("example", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Example?.Serialize(sink);
                sink.Element("groupingId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); GroupingIdElement?.Serialize(sink);
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
                    case "reference":
                        Reference = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "fhirVersion":
                        FhirVersionElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>>();
                        return true;
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "exampleBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                        Example = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "exampleCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Example, "example");
                        Example = source.Get<Hl7.Fhir.Model.Canonical>();
                        return true;
                    case "groupingId":
                        GroupingIdElement = source.Get<Hl7.Fhir.Model.Id>();
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
                    case "reference":
                        Reference = source.Populate(Reference);
                        return true;
                    case "fhirVersion":
                    case "_fhirVersion":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "exampleBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                        Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_exampleBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                        Example = source.Populate(Example as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "exampleCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Example, "example");
                        Example = source.PopulateValue(Example as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "_exampleCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Example, "example");
                        Example = source.Populate(Example as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "groupingId":
                        GroupingIdElement = source.PopulateValue(GroupingIdElement);
                        return true;
                    case "_groupingId":
                        GroupingIdElement = source.Populate(GroupingIdElement);
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
                    case "fhirVersion":
                        source.PopulatePrimitiveListItemValue(FhirVersionElement, index);
                        return true;
                    case "_fhirVersion":
                        source.PopulatePrimitiveListItem(FhirVersionElement, index);
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
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(FhirVersionElement != null) dest.FhirVersionElement = new List<Code<Hl7.Fhir.Model.R4.FHIRVersion>>(FhirVersionElement.DeepCopy());
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                    if(GroupingIdElement != null) dest.GroupingIdElement = (Hl7.Fhir.Model.Id)GroupingIdElement.DeepCopy();
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
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Example, otherT.Example)) return false;
                if( !DeepComparable.Matches(GroupingIdElement, otherT.GroupingIdElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
                if( !DeepComparable.IsExactly(GroupingIdElement, otherT.GroupingIdElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Reference != null) yield return Reference;
                    foreach (var elem in FhirVersionElement) { if (elem != null) yield return elem; }
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Example != null) yield return Example;
                    if (GroupingIdElement != null) yield return GroupingIdElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    foreach (var elem in FhirVersionElement) { if (elem != null) yield return new ElementValue("fhirVersion", elem); }
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Example != null) yield return new ElementValue("example", Example);
                    if (GroupingIdElement != null) yield return new ElementValue("groupingId", GroupingIdElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PageComponent")]
        [DataContract]
        public partial class PageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IImplementationGuidePageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PageComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IImplementationGuidePageComponent> Hl7.Fhir.Model.IImplementationGuidePageComponent.Page { get { return Page; } }
            
            /// <summary>
            /// Where to find that page
            /// </summary>
            [FhirElement("name", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            
            private Hl7.Fhir.Model.Element _Name;
            
            /// <summary>
            /// Short title shown for navigational assistance
            /// </summary>
            [FhirElement("title", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Short title shown for navigational assistance
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
            /// html | markdown | xml | generated
            /// </summary>
            [FhirElement("generation", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.GuidePageGeneration> GenerationElement
            {
                get { return _GenerationElement; }
                set { _GenerationElement = value; OnPropertyChanged("GenerationElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.GuidePageGeneration> _GenerationElement;
            
            /// <summary>
            /// html | markdown | xml | generated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.GuidePageGeneration? Generation
            {
                get { return GenerationElement != null ? GenerationElement.Value : null; }
                set
                {
                    if (value == null)
                        GenerationElement = null;
                    else
                        GenerationElement = new Code<Hl7.Fhir.Model.R4.GuidePageGeneration>(value);
                    OnPropertyChanged("Generation");
                }
            }
            
            /// <summary>
            /// Nested Pages / Sections
            /// </summary>
            [FhirElement("page", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<PageComponent> Page
            {
                get { if(_Page==null) _Page = new List<PageComponent>(); return _Page; }
                set { _Page = value; OnPropertyChanged("Page"); }
            }
            
            private List<PageComponent> _Page;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PageComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Name?.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TitleElement?.Serialize(sink);
                sink.Element("generation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); GenerationElement?.Serialize(sink);
                sink.BeginList("page", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Page)
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
                    case "nameUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Name, "name");
                        Name = source.Get<Hl7.Fhir.Model.Url>();
                        return true;
                    case "nameReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Name, "name");
                        Name = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "title":
                        TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "generation":
                        GenerationElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.GuidePageGeneration>>();
                        return true;
                    case "page":
                        Page = source.GetList<PageComponent>();
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
                    case "nameUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Name, "name");
                        Name = source.PopulateValue(Name as Hl7.Fhir.Model.Url);
                        return true;
                    case "_nameUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Name, "name");
                        Name = source.Populate(Name as Hl7.Fhir.Model.Url);
                        return true;
                    case "nameReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Name, "name");
                        Name = source.Populate(Name as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                    case "generation":
                        GenerationElement = source.PopulateValue(GenerationElement);
                        return true;
                    case "_generation":
                        GenerationElement = source.Populate(GenerationElement);
                        return true;
                    case "page":
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
                    case "page":
                        source.PopulateListItem(Page, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.Element)Name.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(GenerationElement != null) dest.GenerationElement = (Code<Hl7.Fhir.Model.R4.GuidePageGeneration>)GenerationElement.DeepCopy();
                    if(Page != null) dest.Page = new List<PageComponent>(Page.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(GenerationElement, otherT.GenerationElement)) return false;
                if( !DeepComparable.Matches(Page, otherT.Page)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(GenerationElement, otherT.GenerationElement)) return false;
                if( !DeepComparable.IsExactly(Page, otherT.Page)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Name != null) yield return Name;
                    if (TitleElement != null) yield return TitleElement;
                    if (GenerationElement != null) yield return GenerationElement;
                    foreach (var elem in Page) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Name != null) yield return new ElementValue("name", Name);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    if (GenerationElement != null) yield return new ElementValue("generation", GenerationElement);
                    foreach (var elem in Page) { if (elem != null) yield return new ElementValue("page", elem); }
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
            /// apply | path-resource | path-pages | path-tx-cache | expansion-parameter | rule-broken-links | generate-xml | generate-json | generate-turtle | html-template
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.GuideParameterCode> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.GuideParameterCode> _CodeElement;
            
            /// <summary>
            /// apply | path-resource | path-pages | path-tx-cache | expansion-parameter | rule-broken-links | generate-xml | generate-json | generate-turtle | html-template
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.GuideParameterCode? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.R4.GuideParameterCode>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Value for named type
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Value for named type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
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
                        CodeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.GuideParameterCode>>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.R4.GuideParameterCode>)CodeElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
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
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TemplateComponent")]
        [DataContract]
        public partial class TemplateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TemplateComponent"; } }
            
            /// <summary>
            /// Type of template specified
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
            /// Type of template specified
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
            /// The source location for the template
            /// </summary>
            [FhirElement("source", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SourceElement;
            
            /// <summary>
            /// The source location for the template
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceElement = null;
                    else
                        SourceElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Source");
                }
            }
            
            /// <summary>
            /// The scope in which the template applies
            /// </summary>
            [FhirElement("scope", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ScopeElement
            {
                get { return _ScopeElement; }
                set { _ScopeElement = value; OnPropertyChanged("ScopeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ScopeElement;
            
            /// <summary>
            /// The scope in which the template applies
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Scope
            {
                get { return ScopeElement != null ? ScopeElement.Value : null; }
                set
                {
                    if (value == null)
                        ScopeElement = null;
                    else
                        ScopeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Scope");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TemplateComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SourceElement?.Serialize(sink);
                sink.Element("scope", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ScopeElement?.Serialize(sink);
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
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "source":
                        SourceElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "scope":
                        ScopeElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "source":
                        SourceElement = source.PopulateValue(SourceElement);
                        return true;
                    case "_source":
                        SourceElement = source.Populate(SourceElement);
                        return true;
                    case "scope":
                        ScopeElement = source.PopulateValue(ScopeElement);
                        return true;
                    case "_scope":
                        ScopeElement = source.Populate(ScopeElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TemplateComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.FhirString)SourceElement.DeepCopy();
                    if(ScopeElement != null) dest.ScopeElement = (Hl7.Fhir.Model.FhirString)ScopeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TemplateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TemplateComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.Matches(ScopeElement, otherT.ScopeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TemplateComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.IsExactly(ScopeElement, otherT.ScopeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (SourceElement != null) yield return SourceElement;
                    if (ScopeElement != null) yield return ScopeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (SourceElement != null) yield return new ElementValue("source", SourceElement);
                    if (ScopeElement != null) yield return new ElementValue("scope", ScopeElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ManifestComponent")]
        [DataContract]
        public partial class ManifestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ManifestComponent"; } }
            
            /// <summary>
            /// Location of rendered implementation guide
            /// </summary>
            [FhirElement("rendering", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Url RenderingElement
            {
                get { return _RenderingElement; }
                set { _RenderingElement = value; OnPropertyChanged("RenderingElement"); }
            }
            
            private Hl7.Fhir.Model.Url _RenderingElement;
            
            /// <summary>
            /// Location of rendered implementation guide
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Rendering
            {
                get { return RenderingElement != null ? RenderingElement.Value : null; }
                set
                {
                    if (value == null)
                        RenderingElement = null;
                    else
                        RenderingElement = new Hl7.Fhir.Model.Url(value);
                    OnPropertyChanged("Rendering");
                }
            }
            
            /// <summary>
            /// Resource in the implementation guide
            /// </summary>
            [FhirElement("resource", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<ManifestResourceComponent> Resource
            {
                get { if(_Resource==null) _Resource = new List<ManifestResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<ManifestResourceComponent> _Resource;
            
            /// <summary>
            /// HTML page within the parent IG
            /// </summary>
            [FhirElement("page", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ManifestPageComponent> Page
            {
                get { if(_Page==null) _Page = new List<ManifestPageComponent>(); return _Page; }
                set { _Page = value; OnPropertyChanged("Page"); }
            }
            
            private List<ManifestPageComponent> _Page;
            
            /// <summary>
            /// Image within the IG
            /// </summary>
            [FhirElement("image", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ImageElement
            {
                get { if(_ImageElement==null) _ImageElement = new List<Hl7.Fhir.Model.FhirString>(); return _ImageElement; }
                set { _ImageElement = value; OnPropertyChanged("ImageElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ImageElement;
            
            /// <summary>
            /// Image within the IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Image
            {
                get { return ImageElement != null ? ImageElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ImageElement = null;
                    else
                        ImageElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Image");
                }
            }
            
            /// <summary>
            /// Additional linkable file in IG
            /// </summary>
            [FhirElement("other", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> OtherElement
            {
                get { if(_OtherElement==null) _OtherElement = new List<Hl7.Fhir.Model.FhirString>(); return _OtherElement; }
                set { _OtherElement = value; OnPropertyChanged("OtherElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _OtherElement;
            
            /// <summary>
            /// Additional linkable file in IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Other
            {
                get { return OtherElement != null ? OtherElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OtherElement = null;
                    else
                        OtherElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Other");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ManifestComponent");
                base.Serialize(sink);
                sink.Element("rendering", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RenderingElement?.Serialize(sink);
                sink.BeginList("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                foreach(var item in Resource)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("page", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Page)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("image", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(ImageElement);
                sink.End();
                sink.BeginList("other", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(OtherElement);
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
                    case "rendering":
                        RenderingElement = source.Get<Hl7.Fhir.Model.Url>();
                        return true;
                    case "resource":
                        Resource = source.GetList<ManifestResourceComponent>();
                        return true;
                    case "page":
                        Page = source.GetList<ManifestPageComponent>();
                        return true;
                    case "image":
                        ImageElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "other":
                        OtherElement = source.GetList<Hl7.Fhir.Model.FhirString>();
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
                    case "rendering":
                        RenderingElement = source.PopulateValue(RenderingElement);
                        return true;
                    case "_rendering":
                        RenderingElement = source.Populate(RenderingElement);
                        return true;
                    case "resource":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "page":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "image":
                    case "_image":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "other":
                    case "_other":
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
                    case "page":
                        source.PopulateListItem(Page, index);
                        return true;
                    case "image":
                        source.PopulatePrimitiveListItemValue(ImageElement, index);
                        return true;
                    case "_image":
                        source.PopulatePrimitiveListItem(ImageElement, index);
                        return true;
                    case "other":
                        source.PopulatePrimitiveListItemValue(OtherElement, index);
                        return true;
                    case "_other":
                        source.PopulatePrimitiveListItem(OtherElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManifestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RenderingElement != null) dest.RenderingElement = (Hl7.Fhir.Model.Url)RenderingElement.DeepCopy();
                    if(Resource != null) dest.Resource = new List<ManifestResourceComponent>(Resource.DeepCopy());
                    if(Page != null) dest.Page = new List<ManifestPageComponent>(Page.DeepCopy());
                    if(ImageElement != null) dest.ImageElement = new List<Hl7.Fhir.Model.FhirString>(ImageElement.DeepCopy());
                    if(OtherElement != null) dest.OtherElement = new List<Hl7.Fhir.Model.FhirString>(OtherElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ManifestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManifestComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RenderingElement, otherT.RenderingElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Page, otherT.Page)) return false;
                if( !DeepComparable.Matches(ImageElement, otherT.ImageElement)) return false;
                if( !DeepComparable.Matches(OtherElement, otherT.OtherElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManifestComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RenderingElement, otherT.RenderingElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Page, otherT.Page)) return false;
                if( !DeepComparable.IsExactly(ImageElement, otherT.ImageElement)) return false;
                if( !DeepComparable.IsExactly(OtherElement, otherT.OtherElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RenderingElement != null) yield return RenderingElement;
                    foreach (var elem in Resource) { if (elem != null) yield return elem; }
                    foreach (var elem in Page) { if (elem != null) yield return elem; }
                    foreach (var elem in ImageElement) { if (elem != null) yield return elem; }
                    foreach (var elem in OtherElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RenderingElement != null) yield return new ElementValue("rendering", RenderingElement);
                    foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
                    foreach (var elem in Page) { if (elem != null) yield return new ElementValue("page", elem); }
                    foreach (var elem in ImageElement) { if (elem != null) yield return new ElementValue("image", elem); }
                    foreach (var elem in OtherElement) { if (elem != null) yield return new ElementValue("other", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ManifestResourceComponent")]
        [DataContract]
        public partial class ManifestResourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ManifestResourceComponent"; } }
            
            /// <summary>
            /// Location of the resource
            /// </summary>
            [FhirElement("reference", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Is an example/What is this an example of?
            /// </summary>
            [FhirElement("example", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical))]
            [DataMember]
            public Hl7.Fhir.Model.Element Example
            {
                get { return _Example; }
                set { _Example = value; OnPropertyChanged("Example"); }
            }
            
            private Hl7.Fhir.Model.Element _Example;
            
            /// <summary>
            /// Relative path for page in IG
            /// </summary>
            [FhirElement("relativePath", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Url RelativePathElement
            {
                get { return _RelativePathElement; }
                set { _RelativePathElement = value; OnPropertyChanged("RelativePathElement"); }
            }
            
            private Hl7.Fhir.Model.Url _RelativePathElement;
            
            /// <summary>
            /// Relative path for page in IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RelativePath
            {
                get { return RelativePathElement != null ? RelativePathElement.Value : null; }
                set
                {
                    if (value == null)
                        RelativePathElement = null;
                    else
                        RelativePathElement = new Hl7.Fhir.Model.Url(value);
                    OnPropertyChanged("RelativePath");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ManifestResourceComponent");
                base.Serialize(sink);
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Reference?.Serialize(sink);
                sink.Element("example", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Example?.Serialize(sink);
                sink.Element("relativePath", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RelativePathElement?.Serialize(sink);
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
                    case "reference":
                        Reference = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "exampleBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                        Example = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "exampleCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Example, "example");
                        Example = source.Get<Hl7.Fhir.Model.Canonical>();
                        return true;
                    case "relativePath":
                        RelativePathElement = source.Get<Hl7.Fhir.Model.Url>();
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
                    case "reference":
                        Reference = source.Populate(Reference);
                        return true;
                    case "exampleBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                        Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_exampleBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                        Example = source.Populate(Example as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "exampleCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Example, "example");
                        Example = source.PopulateValue(Example as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "_exampleCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Example, "example");
                        Example = source.Populate(Example as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "relativePath":
                        RelativePathElement = source.PopulateValue(RelativePathElement);
                        return true;
                    case "_relativePath":
                        RelativePathElement = source.Populate(RelativePathElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManifestResourceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                    if(RelativePathElement != null) dest.RelativePathElement = (Hl7.Fhir.Model.Url)RelativePathElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ManifestResourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManifestResourceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Example, otherT.Example)) return false;
                if( !DeepComparable.Matches(RelativePathElement, otherT.RelativePathElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManifestResourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
                if( !DeepComparable.IsExactly(RelativePathElement, otherT.RelativePathElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Reference != null) yield return Reference;
                    if (Example != null) yield return Example;
                    if (RelativePathElement != null) yield return RelativePathElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    if (Example != null) yield return new ElementValue("example", Example);
                    if (RelativePathElement != null) yield return new ElementValue("relativePath", RelativePathElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ManifestPageComponent")]
        [DataContract]
        public partial class ManifestPageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ManifestPageComponent"; } }
            
            /// <summary>
            /// HTML page name
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
            /// HTML page name
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
            /// Title of the page, for references
            /// </summary>
            [FhirElement("title", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Title of the page, for references
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
            /// Anchor available on the page
            /// </summary>
            [FhirElement("anchor", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> AnchorElement
            {
                get { if(_AnchorElement==null) _AnchorElement = new List<Hl7.Fhir.Model.FhirString>(); return _AnchorElement; }
                set { _AnchorElement = value; OnPropertyChanged("AnchorElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _AnchorElement;
            
            /// <summary>
            /// Anchor available on the page
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Anchor
            {
                get { return AnchorElement != null ? AnchorElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        AnchorElement = null;
                    else
                        AnchorElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Anchor");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ManifestPageComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TitleElement?.Serialize(sink);
                sink.BeginList("anchor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(AnchorElement);
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
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "title":
                        TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "anchor":
                        AnchorElement = source.GetList<Hl7.Fhir.Model.FhirString>();
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
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                    case "anchor":
                    case "_anchor":
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
                    case "anchor":
                        source.PopulatePrimitiveListItemValue(AnchorElement, index);
                        return true;
                    case "_anchor":
                        source.PopulatePrimitiveListItem(AnchorElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManifestPageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(AnchorElement != null) dest.AnchorElement = new List<Hl7.Fhir.Model.FhirString>(AnchorElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ManifestPageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManifestPageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(AnchorElement, otherT.AnchorElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManifestPageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(AnchorElement, otherT.AnchorElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (TitleElement != null) yield return TitleElement;
                    foreach (var elem in AnchorElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    foreach (var elem in AnchorElement) { if (elem != null) yield return new ElementValue("anchor", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IImplementationGuideGlobalComponent> Hl7.Fhir.Model.IImplementationGuide.Global { get { return Global; } }
    
        
        /// <summary>
        /// Canonical identifier for this implementation guide, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this implementation guide, represented as a URI (globally unique)
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
        /// Business version of the implementation guide
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
        /// Business version of the implementation guide
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
        /// Name for this implementation guide (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// Name for this implementation guide (computer friendly)
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
        /// Name for this implementation guide (human friendly)
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
        /// Name for this implementation guide (human friendly)
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
        /// Natural language description of the implementation guide
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
        /// Natural language description of the implementation guide
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
        /// Intended jurisdiction for implementation guide (if applicable)
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
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=210)]
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
        /// NPM Package name for IG
        /// </summary>
        [FhirElement("packageId", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id PackageIdElement
        {
            get { return _PackageIdElement; }
            set { _PackageIdElement = value; OnPropertyChanged("PackageIdElement"); }
        }
        
        private Hl7.Fhir.Model.Id _PackageIdElement;
        
        /// <summary>
        /// NPM Package name for IG
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PackageId
        {
            get { return PackageIdElement != null ? PackageIdElement.Value : null; }
            set
            {
                if (value == null)
                    PackageIdElement = null;
                else
                    PackageIdElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("PackageId");
            }
        }
        
        /// <summary>
        /// SPDX license code for this IG (or not-open-source)
        /// </summary>
        [FhirElement("license", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.SPDXLicense> LicenseElement
        {
            get { return _LicenseElement; }
            set { _LicenseElement = value; OnPropertyChanged("LicenseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.SPDXLicense> _LicenseElement;
        
        /// <summary>
        /// SPDX license code for this IG (or not-open-source)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.SPDXLicense? License
        {
            get { return LicenseElement != null ? LicenseElement.Value : null; }
            set
            {
                if (value == null)
                    LicenseElement = null;
                else
                    LicenseElement = new Code<Hl7.Fhir.Model.R4.SPDXLicense>(value);
                OnPropertyChanged("License");
            }
        }
        
        /// <summary>
        /// FHIR Version(s) this Implementation Guide targets
        /// </summary>
        [FhirElement("fhirVersion", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.R4.FHIRVersion>> FhirVersionElement
        {
            get { if(_FhirVersionElement==null) _FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>>(); return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.R4.FHIRVersion>> _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version(s) this Implementation Guide targets
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.R4.FHIRVersion?> FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    FhirVersionElement = null;
                else
                    FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>(elem)));
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// Another Implementation guide this depends on
        /// </summary>
        [FhirElement("dependsOn", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DependsOnComponent> DependsOn
        {
            get { if(_DependsOn==null) _DependsOn = new List<DependsOnComponent>(); return _DependsOn; }
            set { _DependsOn = value; OnPropertyChanged("DependsOn"); }
        }
        
        private List<DependsOnComponent> _DependsOn;
        
        /// <summary>
        /// Profiles that apply globally
        /// </summary>
        [FhirElement("global", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GlobalComponent> Global
        {
            get { if(_Global==null) _Global = new List<GlobalComponent>(); return _Global; }
            set { _Global = value; OnPropertyChanged("Global"); }
        }
        
        private List<GlobalComponent> _Global;
        
        /// <summary>
        /// Information needed to build the IG
        /// </summary>
        [FhirElement("definition", Order=270)]
        [DataMember]
        public DefinitionComponent Definition
        {
            get { return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private DefinitionComponent _Definition;
        
        /// <summary>
        /// Information about an assembled IG
        /// </summary>
        [FhirElement("manifest", Order=280)]
        [DataMember]
        public ManifestComponent Manifest
        {
            get { return _Manifest; }
            set { _Manifest = value; OnPropertyChanged("Manifest"); }
        }
        
        private ManifestComponent _Manifest;
    
    
        public static ElementDefinitionConstraint[] ImplementationGuide_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ig-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ig-2",
                severity: ConstraintSeverity.Warning,
                expression: "definition.resource.fhirVersion.all(%context.fhirVersion contains $this)",
                human: "If a resource has a fhirVersion, it must be oe of the versions defined for the Implementation Guide",
                xpath: "count(for $id in (f:resource/f:fhirVersion) return $id[not(ancestor::f:fhirVersion/@value=$id/@value)])=0"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ig-1",
                severity: ConstraintSeverity.Warning,
                expression: "definition.all(resource.groupingId.all(%context.grouping.id contains $this))",
                human: "If a resource has a groupingId, it must refer to a grouping defined in the Implementation Guide",
                xpath: "count(for $id in (f:resource/f:groupingId) return $id[not(ancestor::f:grouping/@id=$id/@value)])=0"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(ImplementationGuide_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImplementationGuide;
        
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
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(PackageIdElement != null) dest.PackageIdElement = (Hl7.Fhir.Model.Id)PackageIdElement.DeepCopy();
                if(LicenseElement != null) dest.LicenseElement = (Code<Hl7.Fhir.Model.R4.SPDXLicense>)LicenseElement.DeepCopy();
                if(FhirVersionElement != null) dest.FhirVersionElement = new List<Code<Hl7.Fhir.Model.R4.FHIRVersion>>(FhirVersionElement.DeepCopy());
                if(DependsOn != null) dest.DependsOn = new List<DependsOnComponent>(DependsOn.DeepCopy());
                if(Global != null) dest.Global = new List<GlobalComponent>(Global.DeepCopy());
                if(Definition != null) dest.Definition = (DefinitionComponent)Definition.DeepCopy();
                if(Manifest != null) dest.Manifest = (ManifestComponent)Manifest.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ImplementationGuide());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImplementationGuide;
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
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(PackageIdElement, otherT.PackageIdElement)) return false;
            if( !DeepComparable.Matches(LicenseElement, otherT.LicenseElement)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(DependsOn, otherT.DependsOn)) return false;
            if( !DeepComparable.Matches(Global, otherT.Global)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(Manifest, otherT.Manifest)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImplementationGuide;
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
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(PackageIdElement, otherT.PackageIdElement)) return false;
            if( !DeepComparable.IsExactly(LicenseElement, otherT.LicenseElement)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(DependsOn, otherT.DependsOn)) return false;
            if( !DeepComparable.IsExactly(Global, otherT.Global)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(Manifest, otherT.Manifest)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ImplementationGuide");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UrlElement?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
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
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("packageId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PackageIdElement?.Serialize(sink);
            sink.Element("license", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LicenseElement?.Serialize(sink);
            sink.BeginList("fhirVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            sink.Serialize(FhirVersionElement);
            sink.End();
            sink.BeginList("dependsOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in DependsOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("global", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Global)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Definition?.Serialize(sink);
            sink.Element("manifest", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Manifest?.Serialize(sink);
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
                    Contact = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
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
                case "copyright":
                    CopyrightElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "packageId":
                    PackageIdElement = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "license":
                    LicenseElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.SPDXLicense>>();
                    return true;
                case "fhirVersion":
                    FhirVersionElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRVersion>>();
                    return true;
                case "dependsOn":
                    DependsOn = source.GetList<DependsOnComponent>();
                    return true;
                case "global":
                    Global = source.GetList<GlobalComponent>();
                    return true;
                case "definition":
                    Definition = source.Get<DefinitionComponent>();
                    return true;
                case "manifest":
                    Manifest = source.Get<ManifestComponent>();
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
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "packageId":
                    PackageIdElement = source.PopulateValue(PackageIdElement);
                    return true;
                case "_packageId":
                    PackageIdElement = source.Populate(PackageIdElement);
                    return true;
                case "license":
                    LicenseElement = source.PopulateValue(LicenseElement);
                    return true;
                case "_license":
                    LicenseElement = source.Populate(LicenseElement);
                    return true;
                case "fhirVersion":
                case "_fhirVersion":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dependsOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "global":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "definition":
                    Definition = source.Populate(Definition);
                    return true;
                case "manifest":
                    Manifest = source.Populate(Manifest);
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
                case "fhirVersion":
                    source.PopulatePrimitiveListItemValue(FhirVersionElement, index);
                    return true;
                case "_fhirVersion":
                    source.PopulatePrimitiveListItem(FhirVersionElement, index);
                    return true;
                case "dependsOn":
                    source.PopulateListItem(DependsOn, index);
                    return true;
                case "global":
                    source.PopulateListItem(Global, index);
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
                if (CopyrightElement != null) yield return CopyrightElement;
                if (PackageIdElement != null) yield return PackageIdElement;
                if (LicenseElement != null) yield return LicenseElement;
                foreach (var elem in FhirVersionElement) { if (elem != null) yield return elem; }
                foreach (var elem in DependsOn) { if (elem != null) yield return elem; }
                foreach (var elem in Global) { if (elem != null) yield return elem; }
                if (Definition != null) yield return Definition;
                if (Manifest != null) yield return Manifest;
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
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (PackageIdElement != null) yield return new ElementValue("packageId", PackageIdElement);
                if (LicenseElement != null) yield return new ElementValue("license", LicenseElement);
                foreach (var elem in FhirVersionElement) { if (elem != null) yield return new ElementValue("fhirVersion", elem); }
                foreach (var elem in DependsOn) { if (elem != null) yield return new ElementValue("dependsOn", elem); }
                foreach (var elem in Global) { if (elem != null) yield return new ElementValue("global", elem); }
                if (Definition != null) yield return new ElementValue("definition", Definition);
                if (Manifest != null) yield return new ElementValue("manifest", Manifest);
            }
        }
    
    }

}
