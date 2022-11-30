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
    /// Example of workflow instance
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ExampleScenario", IsResource=true)]
    [DataContract]
    public partial class ExampleScenario : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ExampleScenario; } }
        [NotMapped]
        public override string TypeName { get { return "ExampleScenario"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ActorComponent")]
        [DataContract]
        public partial class ActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActorComponent"; } }
            
            /// <summary>
            /// ID or acronym of the actor
            /// </summary>
            [FhirElement("actorId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ActorIdElement
            {
                get { return _ActorIdElement; }
                set { _ActorIdElement = value; OnPropertyChanged("ActorIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ActorIdElement;
            
            /// <summary>
            /// ID or acronym of the actor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ActorId
            {
                get { return ActorIdElement != null ? ActorIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ActorIdElement = null;
                    else
                        ActorIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ActorId");
                }
            }
            
            /// <summary>
            /// person | entity
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.ExampleScenarioActorType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.ExampleScenarioActorType> _TypeElement;
            
            /// <summary>
            /// person | entity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.ExampleScenarioActorType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.ExampleScenarioActorType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The name of the actor as shown in the page
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
            /// The name of the actor as shown in the page
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
            /// The description of the actor
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DescriptionElement;
            
            /// <summary>
            /// The description of the actor
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActorComponent");
                base.Serialize(sink);
                sink.Element("actorId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ActorIdElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "actorId":
                        ActorIdElement = source.PopulateValue(ActorIdElement);
                        return true;
                    case "_actorId":
                        ActorIdElement = source.Populate(ActorIdElement);
                        return true;
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActorIdElement != null) dest.ActorIdElement = (Hl7.Fhir.Model.FhirString)ActorIdElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.ExampleScenarioActorType>)TypeElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ActorIdElement, otherT.ActorIdElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActorIdElement, otherT.ActorIdElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
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
                    if (ActorIdElement != null) yield return ActorIdElement;
                    if (TypeElement != null) yield return TypeElement;
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
                    if (ActorIdElement != null) yield return new ElementValue("actorId", ActorIdElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "InstanceComponent")]
        [DataContract]
        public partial class InstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InstanceComponent"; } }
            
            /// <summary>
            /// The id of the resource for referencing
            /// </summary>
            [FhirElement("resourceId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResourceIdElement
            {
                get { return _ResourceIdElement; }
                set { _ResourceIdElement = value; OnPropertyChanged("ResourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResourceIdElement;
            
            /// <summary>
            /// The id of the resource for referencing
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResourceId
            {
                get { return ResourceIdElement != null ? ResourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceIdElement = null;
                    else
                        ResourceIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResourceId");
                }
            }
            
            /// <summary>
            /// The type of the resource
            /// </summary>
            [FhirElement("resourceType", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResourceType> ResourceTypeElement
            {
                get { return _ResourceTypeElement; }
                set { _ResourceTypeElement = value; OnPropertyChanged("ResourceTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResourceType> _ResourceTypeElement;
            
            /// <summary>
            /// The type of the resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResourceType? ResourceType
            {
                get { return ResourceTypeElement != null ? ResourceTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceTypeElement = null;
                    else
                        ResourceTypeElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
                    OnPropertyChanged("ResourceType");
                }
            }
            
            /// <summary>
            /// A short name for the resource instance
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
            /// A short name for the resource instance
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
            /// Human-friendly description of the resource instance
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DescriptionElement;
            
            /// <summary>
            /// Human-friendly description of the resource instance
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
            /// A specific version of the resource
            /// </summary>
            [FhirElement("version", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<VersionComponent> Version
            {
                get { if(_Version==null) _Version = new List<VersionComponent>(); return _Version; }
                set { _Version = value; OnPropertyChanged("Version"); }
            }
            
            private List<VersionComponent> _Version;
            
            /// <summary>
            /// Resources contained in the instance
            /// </summary>
            [FhirElement("containedInstance", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ContainedInstanceComponent> ContainedInstance
            {
                get { if(_ContainedInstance==null) _ContainedInstance = new List<ContainedInstanceComponent>(); return _ContainedInstance; }
                set { _ContainedInstance = value; OnPropertyChanged("ContainedInstance"); }
            }
            
            private List<ContainedInstanceComponent> _ContainedInstance;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InstanceComponent");
                base.Serialize(sink);
                sink.Element("resourceId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ResourceIdElement?.Serialize(sink);
                sink.Element("resourceType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ResourceTypeElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Version)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("containedInstance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ContainedInstance)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "resourceId":
                        ResourceIdElement = source.PopulateValue(ResourceIdElement);
                        return true;
                    case "_resourceId":
                        ResourceIdElement = source.Populate(ResourceIdElement);
                        return true;
                    case "resourceType":
                        ResourceTypeElement = source.PopulateValue(ResourceTypeElement);
                        return true;
                    case "_resourceType":
                        ResourceTypeElement = source.Populate(ResourceTypeElement);
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
                    case "version":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "containedInstance":
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
                    case "version":
                        source.PopulateListItem(Version, index);
                        return true;
                    case "containedInstance":
                        source.PopulateListItem(ContainedInstance, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResourceIdElement != null) dest.ResourceIdElement = (Hl7.Fhir.Model.FhirString)ResourceIdElement.DeepCopy();
                    if(ResourceTypeElement != null) dest.ResourceTypeElement = (Code<Hl7.Fhir.Model.ResourceType>)ResourceTypeElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                    if(Version != null) dest.Version = new List<VersionComponent>(Version.DeepCopy());
                    if(ContainedInstance != null) dest.ContainedInstance = new List<ContainedInstanceComponent>(ContainedInstance.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new InstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ResourceIdElement, otherT.ResourceIdElement)) return false;
                if( !DeepComparable.Matches(ResourceTypeElement, otherT.ResourceTypeElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Version, otherT.Version)) return false;
                if( !DeepComparable.Matches(ContainedInstance, otherT.ContainedInstance)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ResourceIdElement, otherT.ResourceIdElement)) return false;
                if( !DeepComparable.IsExactly(ResourceTypeElement, otherT.ResourceTypeElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Version, otherT.Version)) return false;
                if( !DeepComparable.IsExactly(ContainedInstance, otherT.ContainedInstance)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ResourceIdElement != null) yield return ResourceIdElement;
                    if (ResourceTypeElement != null) yield return ResourceTypeElement;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Version) { if (elem != null) yield return elem; }
                    foreach (var elem in ContainedInstance) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ResourceIdElement != null) yield return new ElementValue("resourceId", ResourceIdElement);
                    if (ResourceTypeElement != null) yield return new ElementValue("resourceType", ResourceTypeElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Version) { if (elem != null) yield return new ElementValue("version", elem); }
                    foreach (var elem in ContainedInstance) { if (elem != null) yield return new ElementValue("containedInstance", elem); }
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
            /// The identifier of a specific version of a resource
            /// </summary>
            [FhirElement("versionId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionIdElement
            {
                get { return _VersionIdElement; }
                set { _VersionIdElement = value; OnPropertyChanged("VersionIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionIdElement;
            
            /// <summary>
            /// The identifier of a specific version of a resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VersionId
            {
                get { return VersionIdElement != null ? VersionIdElement.Value : null; }
                set
                {
                    if (value == null)
                        VersionIdElement = null;
                    else
                        VersionIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("VersionId");
                }
            }
            
            /// <summary>
            /// The description of the resource version
            /// </summary>
            [FhirElement("description", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DescriptionElement;
            
            /// <summary>
            /// The description of the resource version
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("VersionComponent");
                base.Serialize(sink);
                sink.Element("versionId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); VersionIdElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); DescriptionElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "versionId":
                        VersionIdElement = source.PopulateValue(VersionIdElement);
                        return true;
                    case "_versionId":
                        VersionIdElement = source.Populate(VersionIdElement);
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
                var dest = other as VersionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(VersionIdElement != null) dest.VersionIdElement = (Hl7.Fhir.Model.FhirString)VersionIdElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
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
                if( !DeepComparable.Matches(VersionIdElement, otherT.VersionIdElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VersionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(VersionIdElement, otherT.VersionIdElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (VersionIdElement != null) yield return VersionIdElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (VersionIdElement != null) yield return new ElementValue("versionId", VersionIdElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ContainedInstanceComponent")]
        [DataContract]
        public partial class ContainedInstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ContainedInstanceComponent"; } }
            
            /// <summary>
            /// Each resource contained in the instance
            /// </summary>
            [FhirElement("resourceId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResourceIdElement
            {
                get { return _ResourceIdElement; }
                set { _ResourceIdElement = value; OnPropertyChanged("ResourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResourceIdElement;
            
            /// <summary>
            /// Each resource contained in the instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResourceId
            {
                get { return ResourceIdElement != null ? ResourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceIdElement = null;
                    else
                        ResourceIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResourceId");
                }
            }
            
            /// <summary>
            /// A specific version of a resource contained in the instance
            /// </summary>
            [FhirElement("versionId", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionIdElement
            {
                get { return _VersionIdElement; }
                set { _VersionIdElement = value; OnPropertyChanged("VersionIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionIdElement;
            
            /// <summary>
            /// A specific version of a resource contained in the instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VersionId
            {
                get { return VersionIdElement != null ? VersionIdElement.Value : null; }
                set
                {
                    if (value == null)
                        VersionIdElement = null;
                    else
                        VersionIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("VersionId");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContainedInstanceComponent");
                base.Serialize(sink);
                sink.Element("resourceId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ResourceIdElement?.Serialize(sink);
                sink.Element("versionId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersionIdElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "resourceId":
                        ResourceIdElement = source.PopulateValue(ResourceIdElement);
                        return true;
                    case "_resourceId":
                        ResourceIdElement = source.Populate(ResourceIdElement);
                        return true;
                    case "versionId":
                        VersionIdElement = source.PopulateValue(VersionIdElement);
                        return true;
                    case "_versionId":
                        VersionIdElement = source.Populate(VersionIdElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainedInstanceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResourceIdElement != null) dest.ResourceIdElement = (Hl7.Fhir.Model.FhirString)ResourceIdElement.DeepCopy();
                    if(VersionIdElement != null) dest.VersionIdElement = (Hl7.Fhir.Model.FhirString)VersionIdElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ContainedInstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContainedInstanceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ResourceIdElement, otherT.ResourceIdElement)) return false;
                if( !DeepComparable.Matches(VersionIdElement, otherT.VersionIdElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainedInstanceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ResourceIdElement, otherT.ResourceIdElement)) return false;
                if( !DeepComparable.IsExactly(VersionIdElement, otherT.VersionIdElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ResourceIdElement != null) yield return ResourceIdElement;
                    if (VersionIdElement != null) yield return VersionIdElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ResourceIdElement != null) yield return new ElementValue("resourceId", ResourceIdElement);
                    if (VersionIdElement != null) yield return new ElementValue("versionId", VersionIdElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ProcessComponent")]
        [DataContract]
        public partial class ProcessComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ProcessComponent"; } }
            
            /// <summary>
            /// The diagram title of the group of operations
            /// </summary>
            [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// The diagram title of the group of operations
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
            /// A longer description of the group of operations
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DescriptionElement;
            
            /// <summary>
            /// A longer description of the group of operations
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
            /// Description of initial status before the process starts
            /// </summary>
            [FhirElement("preConditions", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown PreConditionsElement
            {
                get { return _PreConditionsElement; }
                set { _PreConditionsElement = value; OnPropertyChanged("PreConditionsElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _PreConditionsElement;
            
            /// <summary>
            /// Description of initial status before the process starts
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PreConditions
            {
                get { return PreConditionsElement != null ? PreConditionsElement.Value : null; }
                set
                {
                    if (value == null)
                        PreConditionsElement = null;
                    else
                        PreConditionsElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("PreConditions");
                }
            }
            
            /// <summary>
            /// Description of final status after the process ends
            /// </summary>
            [FhirElement("postConditions", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown PostConditionsElement
            {
                get { return _PostConditionsElement; }
                set { _PostConditionsElement = value; OnPropertyChanged("PostConditionsElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _PostConditionsElement;
            
            /// <summary>
            /// Description of final status after the process ends
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PostConditions
            {
                get { return PostConditionsElement != null ? PostConditionsElement.Value : null; }
                set
                {
                    if (value == null)
                        PostConditionsElement = null;
                    else
                        PostConditionsElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("PostConditions");
                }
            }
            
            /// <summary>
            /// Each step of the process
            /// </summary>
            [FhirElement("step", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StepComponent> Step
            {
                get { if(_Step==null) _Step = new List<StepComponent>(); return _Step; }
                set { _Step = value; OnPropertyChanged("Step"); }
            }
            
            private List<StepComponent> _Step;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ProcessComponent");
                base.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TitleElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("preConditions", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PreConditionsElement?.Serialize(sink);
                sink.Element("postConditions", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PostConditionsElement?.Serialize(sink);
                sink.BeginList("step", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Step)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "preConditions":
                        PreConditionsElement = source.PopulateValue(PreConditionsElement);
                        return true;
                    case "_preConditions":
                        PreConditionsElement = source.Populate(PreConditionsElement);
                        return true;
                    case "postConditions":
                        PostConditionsElement = source.PopulateValue(PostConditionsElement);
                        return true;
                    case "_postConditions":
                        PostConditionsElement = source.Populate(PostConditionsElement);
                        return true;
                    case "step":
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
                    case "step":
                        source.PopulateListItem(Step, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcessComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                    if(PreConditionsElement != null) dest.PreConditionsElement = (Hl7.Fhir.Model.Markdown)PreConditionsElement.DeepCopy();
                    if(PostConditionsElement != null) dest.PostConditionsElement = (Hl7.Fhir.Model.Markdown)PostConditionsElement.DeepCopy();
                    if(Step != null) dest.Step = new List<StepComponent>(Step.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ProcessComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcessComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(PreConditionsElement, otherT.PreConditionsElement)) return false;
                if( !DeepComparable.Matches(PostConditionsElement, otherT.PostConditionsElement)) return false;
                if( !DeepComparable.Matches(Step, otherT.Step)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcessComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(PreConditionsElement, otherT.PreConditionsElement)) return false;
                if( !DeepComparable.IsExactly(PostConditionsElement, otherT.PostConditionsElement)) return false;
                if( !DeepComparable.IsExactly(Step, otherT.Step)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TitleElement != null) yield return TitleElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (PreConditionsElement != null) yield return PreConditionsElement;
                    if (PostConditionsElement != null) yield return PostConditionsElement;
                    foreach (var elem in Step) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (PreConditionsElement != null) yield return new ElementValue("preConditions", PreConditionsElement);
                    if (PostConditionsElement != null) yield return new ElementValue("postConditions", PostConditionsElement);
                    foreach (var elem in Step) { if (elem != null) yield return new ElementValue("step", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "StepComponent")]
        [DataContract]
        public partial class StepComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StepComponent"; } }
            
            /// <summary>
            /// Nested process
            /// </summary>
            [FhirElement("process", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ProcessComponent> Process
            {
                get { if(_Process==null) _Process = new List<ProcessComponent>(); return _Process; }
                set { _Process = value; OnPropertyChanged("Process"); }
            }
            
            private List<ProcessComponent> _Process;
            
            /// <summary>
            /// If there is a pause in the flow
            /// </summary>
            [FhirElement("pause", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean PauseElement
            {
                get { return _PauseElement; }
                set { _PauseElement = value; OnPropertyChanged("PauseElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _PauseElement;
            
            /// <summary>
            /// If there is a pause in the flow
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Pause
            {
                get { return PauseElement != null ? PauseElement.Value : null; }
                set
                {
                    if (value == null)
                        PauseElement = null;
                    else
                        PauseElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Pause");
                }
            }
            
            /// <summary>
            /// Each interaction or action
            /// </summary>
            [FhirElement("operation", Order=60)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
            
            /// <summary>
            /// Alternate non-typical step action
            /// </summary>
            [FhirElement("alternative", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AlternativeComponent> Alternative
            {
                get { if(_Alternative==null) _Alternative = new List<AlternativeComponent>(); return _Alternative; }
                set { _Alternative = value; OnPropertyChanged("Alternative"); }
            }
            
            private List<AlternativeComponent> _Alternative;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StepComponent");
                base.Serialize(sink);
                sink.BeginList("process", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Process)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("pause", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PauseElement?.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Operation?.Serialize(sink);
                sink.BeginList("alternative", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Alternative)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "process":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "pause":
                        PauseElement = source.PopulateValue(PauseElement);
                        return true;
                    case "_pause":
                        PauseElement = source.Populate(PauseElement);
                        return true;
                    case "operation":
                        Operation = source.Populate(Operation);
                        return true;
                    case "alternative":
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
                    case "process":
                        source.PopulateListItem(Process, index);
                        return true;
                    case "alternative":
                        source.PopulateListItem(Alternative, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StepComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Process != null) dest.Process = new List<ProcessComponent>(Process.DeepCopy());
                    if(PauseElement != null) dest.PauseElement = (Hl7.Fhir.Model.FhirBoolean)PauseElement.DeepCopy();
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
                    if(Alternative != null) dest.Alternative = new List<AlternativeComponent>(Alternative.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StepComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StepComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Process, otherT.Process)) return false;
                if( !DeepComparable.Matches(PauseElement, otherT.PauseElement)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Alternative, otherT.Alternative)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StepComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Process, otherT.Process)) return false;
                if( !DeepComparable.IsExactly(PauseElement, otherT.PauseElement)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Alternative, otherT.Alternative)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Process) { if (elem != null) yield return elem; }
                    if (PauseElement != null) yield return PauseElement;
                    if (Operation != null) yield return Operation;
                    foreach (var elem in Alternative) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Process) { if (elem != null) yield return new ElementValue("process", elem); }
                    if (PauseElement != null) yield return new ElementValue("pause", PauseElement);
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    foreach (var elem in Alternative) { if (elem != null) yield return new ElementValue("alternative", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            /// <summary>
            /// The sequential number of the interaction
            /// </summary>
            [FhirElement("number", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NumberElement;
            
            /// <summary>
            /// The sequential number of the interaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberElement = null;
                    else
                        NumberElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// The type of operation - CRUD
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// The type of operation - CRUD
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
                        TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The human-friendly name of the interaction
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
            /// The human-friendly name of the interaction
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
            /// Who starts the transaction
            /// </summary>
            [FhirElement("initiator", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString InitiatorElement
            {
                get { return _InitiatorElement; }
                set { _InitiatorElement = value; OnPropertyChanged("InitiatorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _InitiatorElement;
            
            /// <summary>
            /// Who starts the transaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Initiator
            {
                get { return InitiatorElement != null ? InitiatorElement.Value : null; }
                set
                {
                    if (value == null)
                        InitiatorElement = null;
                    else
                        InitiatorElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Initiator");
                }
            }
            
            /// <summary>
            /// Who receives the transaction
            /// </summary>
            [FhirElement("receiver", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReceiverElement
            {
                get { return _ReceiverElement; }
                set { _ReceiverElement = value; OnPropertyChanged("ReceiverElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReceiverElement;
            
            /// <summary>
            /// Who receives the transaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Receiver
            {
                get { return ReceiverElement != null ? ReceiverElement.Value : null; }
                set
                {
                    if (value == null)
                        ReceiverElement = null;
                    else
                        ReceiverElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Receiver");
                }
            }
            
            /// <summary>
            /// A comment to be inserted in the diagram
            /// </summary>
            [FhirElement("description", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DescriptionElement;
            
            /// <summary>
            /// A comment to be inserted in the diagram
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
            /// Whether the initiator is deactivated right after the transaction
            /// </summary>
            [FhirElement("initiatorActive", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean InitiatorActiveElement
            {
                get { return _InitiatorActiveElement; }
                set { _InitiatorActiveElement = value; OnPropertyChanged("InitiatorActiveElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _InitiatorActiveElement;
            
            /// <summary>
            /// Whether the initiator is deactivated right after the transaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? InitiatorActive
            {
                get { return InitiatorActiveElement != null ? InitiatorActiveElement.Value : null; }
                set
                {
                    if (value == null)
                        InitiatorActiveElement = null;
                    else
                        InitiatorActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("InitiatorActive");
                }
            }
            
            /// <summary>
            /// Whether the receiver is deactivated right after the transaction
            /// </summary>
            [FhirElement("receiverActive", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReceiverActiveElement
            {
                get { return _ReceiverActiveElement; }
                set { _ReceiverActiveElement = value; OnPropertyChanged("ReceiverActiveElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ReceiverActiveElement;
            
            /// <summary>
            /// Whether the receiver is deactivated right after the transaction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ReceiverActive
            {
                get { return ReceiverActiveElement != null ? ReceiverActiveElement.Value : null; }
                set
                {
                    if (value == null)
                        ReceiverActiveElement = null;
                    else
                        ReceiverActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ReceiverActive");
                }
            }
            
            /// <summary>
            /// Each resource instance used by the initiator
            /// </summary>
            [FhirElement("request", Order=120)]
            [DataMember]
            public ContainedInstanceComponent Request
            {
                get { return _Request; }
                set { _Request = value; OnPropertyChanged("Request"); }
            }
            
            private ContainedInstanceComponent _Request;
            
            /// <summary>
            /// Each resource instance used by the responder
            /// </summary>
            [FhirElement("response", Order=130)]
            [DataMember]
            public ContainedInstanceComponent Response
            {
                get { return _Response; }
                set { _Response = value; OnPropertyChanged("Response"); }
            }
            
            private ContainedInstanceComponent _Response;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OperationComponent");
                base.Serialize(sink);
                sink.Element("number", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NumberElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TypeElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("initiator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InitiatorElement?.Serialize(sink);
                sink.Element("receiver", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReceiverElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("initiatorActive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InitiatorActiveElement?.Serialize(sink);
                sink.Element("receiverActive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReceiverActiveElement?.Serialize(sink);
                sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Request?.Serialize(sink);
                sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Response?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "number":
                        NumberElement = source.PopulateValue(NumberElement);
                        return true;
                    case "_number":
                        NumberElement = source.Populate(NumberElement);
                        return true;
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "initiator":
                        InitiatorElement = source.PopulateValue(InitiatorElement);
                        return true;
                    case "_initiator":
                        InitiatorElement = source.Populate(InitiatorElement);
                        return true;
                    case "receiver":
                        ReceiverElement = source.PopulateValue(ReceiverElement);
                        return true;
                    case "_receiver":
                        ReceiverElement = source.Populate(ReceiverElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "initiatorActive":
                        InitiatorActiveElement = source.PopulateValue(InitiatorActiveElement);
                        return true;
                    case "_initiatorActive":
                        InitiatorActiveElement = source.Populate(InitiatorActiveElement);
                        return true;
                    case "receiverActive":
                        ReceiverActiveElement = source.PopulateValue(ReceiverActiveElement);
                        return true;
                    case "_receiverActive":
                        ReceiverActiveElement = source.Populate(ReceiverActiveElement);
                        return true;
                    case "request":
                        Request = source.Populate(Request);
                        return true;
                    case "response":
                        Response = source.Populate(Response);
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
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.FhirString)NumberElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(InitiatorElement != null) dest.InitiatorElement = (Hl7.Fhir.Model.FhirString)InitiatorElement.DeepCopy();
                    if(ReceiverElement != null) dest.ReceiverElement = (Hl7.Fhir.Model.FhirString)ReceiverElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                    if(InitiatorActiveElement != null) dest.InitiatorActiveElement = (Hl7.Fhir.Model.FhirBoolean)InitiatorActiveElement.DeepCopy();
                    if(ReceiverActiveElement != null) dest.ReceiverActiveElement = (Hl7.Fhir.Model.FhirBoolean)ReceiverActiveElement.DeepCopy();
                    if(Request != null) dest.Request = (ContainedInstanceComponent)Request.DeepCopy();
                    if(Response != null) dest.Response = (ContainedInstanceComponent)Response.DeepCopy();
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
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(InitiatorElement, otherT.InitiatorElement)) return false;
                if( !DeepComparable.Matches(ReceiverElement, otherT.ReceiverElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(InitiatorActiveElement, otherT.InitiatorActiveElement)) return false;
                if( !DeepComparable.Matches(ReceiverActiveElement, otherT.ReceiverActiveElement)) return false;
                if( !DeepComparable.Matches(Request, otherT.Request)) return false;
                if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(InitiatorElement, otherT.InitiatorElement)) return false;
                if( !DeepComparable.IsExactly(ReceiverElement, otherT.ReceiverElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(InitiatorActiveElement, otherT.InitiatorActiveElement)) return false;
                if( !DeepComparable.IsExactly(ReceiverActiveElement, otherT.ReceiverActiveElement)) return false;
                if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
                if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NumberElement != null) yield return NumberElement;
                    if (TypeElement != null) yield return TypeElement;
                    if (NameElement != null) yield return NameElement;
                    if (InitiatorElement != null) yield return InitiatorElement;
                    if (ReceiverElement != null) yield return ReceiverElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (InitiatorActiveElement != null) yield return InitiatorActiveElement;
                    if (ReceiverActiveElement != null) yield return ReceiverActiveElement;
                    if (Request != null) yield return Request;
                    if (Response != null) yield return Response;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (InitiatorElement != null) yield return new ElementValue("initiator", InitiatorElement);
                    if (ReceiverElement != null) yield return new ElementValue("receiver", ReceiverElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (InitiatorActiveElement != null) yield return new ElementValue("initiatorActive", InitiatorActiveElement);
                    if (ReceiverActiveElement != null) yield return new ElementValue("receiverActive", ReceiverActiveElement);
                    if (Request != null) yield return new ElementValue("request", Request);
                    if (Response != null) yield return new ElementValue("response", Response);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AlternativeComponent")]
        [DataContract]
        public partial class AlternativeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AlternativeComponent"; } }
            
            /// <summary>
            /// Label for alternative
            /// </summary>
            [FhirElement("title", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Label for alternative
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
            /// A human-readable description of each option
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _DescriptionElement;
            
            /// <summary>
            /// A human-readable description of each option
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
            /// What happens in each alternative option
            /// </summary>
            [FhirElement("step", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StepComponent> Step
            {
                get { if(_Step==null) _Step = new List<StepComponent>(); return _Step; }
                set { _Step = value; OnPropertyChanged("Step"); }
            }
            
            private List<StepComponent> _Step;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AlternativeComponent");
                base.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TitleElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("step", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Step)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "step":
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
                    case "step":
                        source.PopulateListItem(Step, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AlternativeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                    if(Step != null) dest.Step = new List<StepComponent>(Step.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AlternativeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AlternativeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Step, otherT.Step)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AlternativeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Step, otherT.Step)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TitleElement != null) yield return TitleElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Step) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Step) { if (elem != null) yield return new ElementValue("step", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Canonical identifier for this example scenario, represented as a URI (globally unique)
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
        /// Canonical identifier for this example scenario, represented as a URI (globally unique)
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
        /// Additional identifier for the example scenario
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Business version of the example scenario
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the example scenario
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
        /// Name for this example scenario (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this example scenario (computer friendly)
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
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
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
        /// Intended jurisdiction for example scenario (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
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
        [FhirElement("copyright", Order=200)]
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
        /// The purpose of the example, e.g. to illustrate a scenario
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
        /// The purpose of the example, e.g. to illustrate a scenario
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
        /// Actor participating in the resource
        /// </summary>
        [FhirElement("actor", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ActorComponent> Actor
        {
            get { if(_Actor==null) _Actor = new List<ActorComponent>(); return _Actor; }
            set { _Actor = value; OnPropertyChanged("Actor"); }
        }
        
        private List<ActorComponent> _Actor;
        
        /// <summary>
        /// Each resource and each version that is present in the workflow
        /// </summary>
        [FhirElement("instance", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<InstanceComponent> Instance
        {
            get { if(_Instance==null) _Instance = new List<InstanceComponent>(); return _Instance; }
            set { _Instance = value; OnPropertyChanged("Instance"); }
        }
        
        private List<InstanceComponent> _Instance;
        
        /// <summary>
        /// Each major process - a group of operations
        /// </summary>
        [FhirElement("process", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ProcessComponent> Process
        {
            get { if(_Process==null) _Process = new List<ProcessComponent>(); return _Process; }
            set { _Process = value; OnPropertyChanged("Process"); }
        }
        
        private List<ProcessComponent> _Process;
        
        /// <summary>
        /// Another nested workflow
        /// </summary>
        [FhirElement("workflow", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> WorkflowElement
        {
            get { if(_WorkflowElement==null) _WorkflowElement = new List<Hl7.Fhir.Model.Canonical>(); return _WorkflowElement; }
            set { _WorkflowElement = value; OnPropertyChanged("WorkflowElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _WorkflowElement;
        
        /// <summary>
        /// Another nested workflow
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Workflow
        {
            get { return WorkflowElement != null ? WorkflowElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    WorkflowElement = null;
                else
                    WorkflowElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Workflow");
            }
        }
    
    
        public static ElementDefinitionConstraint[] ExampleScenario_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "esc-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(ExampleScenario_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ExampleScenario;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(Contact.DeepCopy());
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(Actor != null) dest.Actor = new List<ActorComponent>(Actor.DeepCopy());
                if(Instance != null) dest.Instance = new List<InstanceComponent>(Instance.DeepCopy());
                if(Process != null) dest.Process = new List<ProcessComponent>(Process.DeepCopy());
                if(WorkflowElement != null) dest.WorkflowElement = new List<Hl7.Fhir.Model.Canonical>(WorkflowElement.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ExampleScenario());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ExampleScenario;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
            if( !DeepComparable.Matches(Process, otherT.Process)) return false;
            if( !DeepComparable.Matches(WorkflowElement, otherT.WorkflowElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ExampleScenario;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
            if( !DeepComparable.IsExactly(Process, otherT.Process)) return false;
            if( !DeepComparable.IsExactly(WorkflowElement, otherT.WorkflowElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ExampleScenario");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
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
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.BeginList("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Actor)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("instance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Instance)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("process", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Process)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("workflow", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(WorkflowElement);
            sink.End();
            sink.End();
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
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
                case "purpose":
                    PurposeElement = source.PopulateValue(PurposeElement);
                    return true;
                case "_purpose":
                    PurposeElement = source.Populate(PurposeElement);
                    return true;
                case "actor":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "instance":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "process":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "workflow":
                case "_workflow":
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "actor":
                    source.PopulateListItem(Actor, index);
                    return true;
                case "instance":
                    source.PopulateListItem(Instance, index);
                    return true;
                case "process":
                    source.PopulateListItem(Process, index);
                    return true;
                case "workflow":
                    source.PopulatePrimitiveListItemValue(WorkflowElement, index);
                    return true;
                case "_workflow":
                    source.PopulatePrimitiveListItem(WorkflowElement, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (CopyrightElement != null) yield return CopyrightElement;
                if (PurposeElement != null) yield return PurposeElement;
                foreach (var elem in Actor) { if (elem != null) yield return elem; }
                foreach (var elem in Instance) { if (elem != null) yield return elem; }
                foreach (var elem in Process) { if (elem != null) yield return elem; }
                foreach (var elem in WorkflowElement) { if (elem != null) yield return elem; }
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
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                foreach (var elem in Instance) { if (elem != null) yield return new ElementValue("instance", elem); }
                foreach (var elem in Process) { if (elem != null) yield return new ElementValue("process", elem); }
                foreach (var elem in WorkflowElement) { if (elem != null) yield return new ElementValue("workflow", elem); }
            }
        }
    
    }

}
