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
    /// Example of workflow instance
    /// </summary>
    [FhirType("ExampleScenario", IsResource=true)]
    [DataContract]
    public partial class ExampleScenario : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ExampleScenario; } }
        [NotMapped]
        public override string TypeName { get { return "ExampleScenario"; } }
        
        /// <summary>
        /// The type of actor - system or human.
        /// (url: http://hl7.org/fhir/ValueSet/examplescenario-actor-type)
        /// </summary>
        [FhirEnumeration("ExampleScenarioActorType")]
        public enum ExampleScenarioActorType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/examplescenario-actor-type)
            /// </summary>
            [EnumLiteral("person", "http://hl7.org/fhir/examplescenario-actor-type"), Description("Person")]
            Person,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/examplescenario-actor-type)
            /// </summary>
            [EnumLiteral("entity", "http://hl7.org/fhir/examplescenario-actor-type"), Description("System")]
            Entity,
        }

        [FhirType("ActorComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
            public Code<Hl7.Fhir.Model.ExampleScenario.ExampleScenarioActorType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ExampleScenario.ExampleScenarioActorType> _TypeElement;
            
            /// <summary>
            /// person | entity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ExampleScenario.ExampleScenarioActorType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.ExampleScenario.ExampleScenarioActorType>(value);
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
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActorIdElement != null) dest.ActorIdElement = (Hl7.Fhir.Model.FhirString)ActorIdElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ExampleScenario.ExampleScenarioActorType>)TypeElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
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
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
                
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
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
                
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
                    if (Description != null) yield return Description;
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
                    if (Description != null) yield return new ElementValue("description", Description);
                }
            }

            
        }
        
        
        [FhirType("InstanceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class InstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
                    if (!value.HasValue)
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
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
            /// <summary>
            /// A specific version of the resource
            /// </summary>
            [FhirElement("version", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExampleScenario.VersionComponent> Version
            {
                get { if(_Version==null) _Version = new List<Hl7.Fhir.Model.ExampleScenario.VersionComponent>(); return _Version; }
                set { _Version = value; OnPropertyChanged("Version"); }
            }
            
            private List<Hl7.Fhir.Model.ExampleScenario.VersionComponent> _Version;
            
            /// <summary>
            /// Resources contained in the instance
            /// </summary>
            [FhirElement("containedInstance", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent> ContainedInstance
            {
                get { if(_ContainedInstance==null) _ContainedInstance = new List<Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent>(); return _ContainedInstance; }
                set { _ContainedInstance = value; OnPropertyChanged("ContainedInstance"); }
            }
            
            private List<Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent> _ContainedInstance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResourceIdElement != null) dest.ResourceIdElement = (Hl7.Fhir.Model.FhirString)ResourceIdElement.DeepCopy();
                    if(ResourceTypeElement != null) dest.ResourceTypeElement = (Code<Hl7.Fhir.Model.ResourceType>)ResourceTypeElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                    if(Version != null) dest.Version = new List<Hl7.Fhir.Model.ExampleScenario.VersionComponent>(Version.DeepCopy());
                    if(ContainedInstance != null) dest.ContainedInstance = new List<Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent>(ContainedInstance.DeepCopy());
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
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
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
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
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
                    if (Description != null) yield return Description;
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
                    if (Description != null) yield return new ElementValue("description", Description);
                    foreach (var elem in Version) { if (elem != null) yield return new ElementValue("version", elem); }
                    foreach (var elem in ContainedInstance) { if (elem != null) yield return new ElementValue("containedInstance", elem); }
                }
            }

            
        }
        
        
        [FhirType("VersionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class VersionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VersionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(VersionIdElement != null) dest.VersionIdElement = (Hl7.Fhir.Model.FhirString)VersionIdElement.DeepCopy();
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
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
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VersionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(VersionIdElement, otherT.VersionIdElement)) return false;
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (VersionIdElement != null) yield return VersionIdElement;
                    if (Description != null) yield return Description;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (VersionIdElement != null) yield return new ElementValue("versionId", VersionIdElement);
                    if (Description != null) yield return new ElementValue("description", Description);
                }
            }

            
        }
        
        
        [FhirType("ContainedInstanceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContainedInstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
        
        
        [FhirType("ProcessComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ProcessComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcessComponent"; } }
            
            /// <summary>
            /// The diagram title of the group of operations
            /// </summary>
            [FhirElement("title", InSummary=true, Order=40)]
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
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
            /// <summary>
            /// Description of initial status before the process starts
            /// </summary>
            [FhirElement("preConditions", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown PreConditions
            {
                get { return _PreConditions; }
                set { _PreConditions = value; OnPropertyChanged("PreConditions"); }
            }
            
            private Hl7.Fhir.Model.Markdown _PreConditions;
            
            /// <summary>
            /// Description of final status after the process ends
            /// </summary>
            [FhirElement("postConditions", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown PostConditions
            {
                get { return _PostConditions; }
                set { _PostConditions = value; OnPropertyChanged("PostConditions"); }
            }
            
            private Hl7.Fhir.Model.Markdown _PostConditions;
            
            /// <summary>
            /// Each step of the process
            /// </summary>
            [FhirElement("step", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExampleScenario.StepComponent> Step
            {
                get { if(_Step==null) _Step = new List<Hl7.Fhir.Model.ExampleScenario.StepComponent>(); return _Step; }
                set { _Step = value; OnPropertyChanged("Step"); }
            }
            
            private List<Hl7.Fhir.Model.ExampleScenario.StepComponent> _Step;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcessComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                    if(PreConditions != null) dest.PreConditions = (Hl7.Fhir.Model.Markdown)PreConditions.DeepCopy();
                    if(PostConditions != null) dest.PostConditions = (Hl7.Fhir.Model.Markdown)PostConditions.DeepCopy();
                    if(Step != null) dest.Step = new List<Hl7.Fhir.Model.ExampleScenario.StepComponent>(Step.DeepCopy());
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
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
                if( !DeepComparable.Matches(PreConditions, otherT.PreConditions)) return false;
                if( !DeepComparable.Matches(PostConditions, otherT.PostConditions)) return false;
                if( !DeepComparable.Matches(Step, otherT.Step)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcessComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
                if( !DeepComparable.IsExactly(PreConditions, otherT.PreConditions)) return false;
                if( !DeepComparable.IsExactly(PostConditions, otherT.PostConditions)) return false;
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
                    if (Description != null) yield return Description;
                    if (PreConditions != null) yield return PreConditions;
                    if (PostConditions != null) yield return PostConditions;
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
                    if (Description != null) yield return new ElementValue("description", Description);
                    if (PreConditions != null) yield return new ElementValue("preConditions", PreConditions);
                    if (PostConditions != null) yield return new ElementValue("postConditions", PostConditions);
                    foreach (var elem in Step) { if (elem != null) yield return new ElementValue("step", elem); }
                }
            }

            
        }
        
        
        [FhirType("StepComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StepComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StepComponent"; } }
            
            /// <summary>
            /// Nested process
            /// </summary>
            [FhirElement("process", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent> Process
            {
                get { if(_Process==null) _Process = new List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent>(); return _Process; }
                set { _Process = value; OnPropertyChanged("Process"); }
            }
            
            private List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent> _Process;
            
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
                    if (!value.HasValue)
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
            public Hl7.Fhir.Model.ExampleScenario.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.ExampleScenario.OperationComponent _Operation;
            
            /// <summary>
            /// Alternate non-typical step action
            /// </summary>
            [FhirElement("alternative", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExampleScenario.AlternativeComponent> Alternative
            {
                get { if(_Alternative==null) _Alternative = new List<Hl7.Fhir.Model.ExampleScenario.AlternativeComponent>(); return _Alternative; }
                set { _Alternative = value; OnPropertyChanged("Alternative"); }
            }
            
            private List<Hl7.Fhir.Model.ExampleScenario.AlternativeComponent> _Alternative;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StepComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Process != null) dest.Process = new List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent>(Process.DeepCopy());
                    if(PauseElement != null) dest.PauseElement = (Hl7.Fhir.Model.FhirBoolean)PauseElement.DeepCopy();
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.ExampleScenario.OperationComponent)Operation.DeepCopy();
                    if(Alternative != null) dest.Alternative = new List<Hl7.Fhir.Model.ExampleScenario.AlternativeComponent>(Alternative.DeepCopy());
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
        
        
        [FhirType("OperationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
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
                    if (!value.HasValue)
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
                    if (!value.HasValue)
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
            public Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent Request
            {
                get { return _Request; }
                set { _Request = value; OnPropertyChanged("Request"); }
            }
            
            private Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent _Request;
            
            /// <summary>
            /// Each resource instance used by the responder
            /// </summary>
            [FhirElement("response", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent Response
            {
                get { return _Response; }
                set { _Response = value; OnPropertyChanged("Response"); }
            }
            
            private Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent _Response;
            
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
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                    if(InitiatorActiveElement != null) dest.InitiatorActiveElement = (Hl7.Fhir.Model.FhirBoolean)InitiatorActiveElement.DeepCopy();
                    if(ReceiverActiveElement != null) dest.ReceiverActiveElement = (Hl7.Fhir.Model.FhirBoolean)ReceiverActiveElement.DeepCopy();
                    if(Request != null) dest.Request = (Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent)Request.DeepCopy();
                    if(Response != null) dest.Response = (Hl7.Fhir.Model.ExampleScenario.ContainedInstanceComponent)Response.DeepCopy();
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
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
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
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
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
                    if (Description != null) yield return Description;
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
                    if (Description != null) yield return new ElementValue("description", Description);
                    if (InitiatorActiveElement != null) yield return new ElementValue("initiatorActive", InitiatorActiveElement);
                    if (ReceiverActiveElement != null) yield return new ElementValue("receiverActive", ReceiverActiveElement);
                    if (Request != null) yield return new ElementValue("request", Request);
                    if (Response != null) yield return new ElementValue("response", Response);
                }
            }

            
        }
        
        
        [FhirType("AlternativeComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AlternativeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
            public Hl7.Fhir.Model.Markdown Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Description;
            
            /// <summary>
            /// What happens in each alternative option
            /// </summary>
            [FhirElement("step", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExampleScenario.StepComponent> Step
            {
                get { if(_Step==null) _Step = new List<Hl7.Fhir.Model.ExampleScenario.StepComponent>(); return _Step; }
                set { _Step = value; OnPropertyChanged("Step"); }
            }
            
            private List<Hl7.Fhir.Model.ExampleScenario.StepComponent> _Step;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AlternativeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                    if(Step != null) dest.Step = new List<Hl7.Fhir.Model.ExampleScenario.StepComponent>(Step.DeepCopy());
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
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
                if( !DeepComparable.Matches(Step, otherT.Step)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AlternativeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
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
                    if (Description != null) yield return Description;
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
                    if (Description != null) yield return new ElementValue("description", Description);
                    foreach (var elem in Step) { if (elem != null) yield return new ElementValue("step", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this example scenario, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
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
        /// Business version of the example scenario
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
        [FhirElement("name", InSummary=true, Order=120)]
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
        [FhirElement("status", InSummary=true, Order=130)]
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
        [FhirElement("experimental", InSummary=true, Order=140)]
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
        [FhirElement("date", InSummary=true, Order=150)]
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
        [FhirElement("publisher", InSummary=true, Order=160)]
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
        [FhirElement("contact", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for example scenario (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=190)]
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
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// The purpose of the example, e.g. to illustrate a scenario
        /// </summary>
        [FhirElement("purpose", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Purpose;
        
        /// <summary>
        /// Actor participating in the resource
        /// </summary>
        [FhirElement("actor", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ExampleScenario.ActorComponent> Actor
        {
            get { if(_Actor==null) _Actor = new List<Hl7.Fhir.Model.ExampleScenario.ActorComponent>(); return _Actor; }
            set { _Actor = value; OnPropertyChanged("Actor"); }
        }
        
        private List<Hl7.Fhir.Model.ExampleScenario.ActorComponent> _Actor;
        
        /// <summary>
        /// Each resource and each version that is present in the workflow
        /// </summary>
        [FhirElement("instance", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ExampleScenario.InstanceComponent> Instance
        {
            get { if(_Instance==null) _Instance = new List<Hl7.Fhir.Model.ExampleScenario.InstanceComponent>(); return _Instance; }
            set { _Instance = value; OnPropertyChanged("Instance"); }
        }
        
        private List<Hl7.Fhir.Model.ExampleScenario.InstanceComponent> _Instance;
        
        /// <summary>
        /// Each major process - a group of operations
        /// </summary>
        [FhirElement("process", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent> Process
        {
            get { if(_Process==null) _Process = new List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent>(); return _Process; }
            set { _Process = value; OnPropertyChanged("Process"); }
        }
        
        private List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent> _Process;
        
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
        

        public static ElementDefinition.ConstraintComponent ExampleScenario_ESC_0 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "esc-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(ExampleScenario_ESC_0);
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
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
                if(Actor != null) dest.Actor = new List<Hl7.Fhir.Model.ExampleScenario.ActorComponent>(Actor.DeepCopy());
                if(Instance != null) dest.Instance = new List<Hl7.Fhir.Model.ExampleScenario.InstanceComponent>(Instance.DeepCopy());
                if(Process != null) dest.Process = new List<Hl7.Fhir.Model.ExampleScenario.ProcessComponent>(Process.DeepCopy());
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
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
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
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
            if( !DeepComparable.IsExactly(Process, otherT.Process)) return false;
            if( !DeepComparable.IsExactly(WorkflowElement, otherT.WorkflowElement)) return false;
            
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
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Copyright != null) yield return Copyright;
				if (Purpose != null) yield return Purpose;
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
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                foreach (var elem in Instance) { if (elem != null) yield return new ElementValue("instance", elem); }
                foreach (var elem in Process) { if (elem != null) yield return new ElementValue("process", elem); }
                foreach (var elem in WorkflowElement) { if (elem != null) yield return new ElementValue("workflow", elem); }
            }
        }

    }
    
}
