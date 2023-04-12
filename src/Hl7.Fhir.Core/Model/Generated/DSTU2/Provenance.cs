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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// Who, What, When for a set of resources
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Provenance", IsResource=true)]
    [DataContract]
    public partial class Provenance : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IProvenance, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Provenance; } }
        [NotMapped]
        public override string TypeName { get { return "Provenance"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AgentComponent")]
        [DataContract]
        public partial class AgentComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProvenanceAgentComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AgentComponent"; } }
            
            /// <summary>
            /// What the agents involvement was
            /// </summary>
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.Coding _Role;
            
            /// <summary>
            /// Individual, device or organization playing role
            /// </summary>
            [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","RelatedPerson","Patient","Device","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            /// <summary>
            /// Authorization-system identifier for the agent
            /// </summary>
            [FhirElement("userId", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier UserId
            {
                get { return _UserId; }
                set { _UserId = value; OnPropertyChanged("UserId"); }
            }
            
            private Hl7.Fhir.Model.Identifier _UserId;
            
            /// <summary>
            /// Track delegation between agents
            /// </summary>
            [FhirElement("relatedAgent", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RelatedAgentComponent> RelatedAgent
            {
                get { if(_RelatedAgent==null) _RelatedAgent = new List<RelatedAgentComponent>(); return _RelatedAgent; }
                set { _RelatedAgent = value; OnPropertyChanged("RelatedAgent"); }
            }
            
            private List<RelatedAgentComponent> _RelatedAgent;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AgentComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Role?.Serialize(sink);
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Actor?.Serialize(sink);
                sink.Element("userId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UserId?.Serialize(sink);
                sink.BeginList("relatedAgent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in RelatedAgent)
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
                    case "role":
                        Role = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "actor":
                        Actor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "userId":
                        UserId = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "relatedAgent":
                        RelatedAgent = source.GetList<RelatedAgentComponent>();
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
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "actor":
                        Actor = source.Populate(Actor);
                        return true;
                    case "userId":
                        UserId = source.Populate(UserId);
                        return true;
                    case "relatedAgent":
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
                    case "relatedAgent":
                        source.PopulateListItem(RelatedAgent, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AgentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.Coding)Role.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(UserId != null) dest.UserId = (Hl7.Fhir.Model.Identifier)UserId.DeepCopy();
                    if(RelatedAgent != null) dest.RelatedAgent = new List<RelatedAgentComponent>(RelatedAgent.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AgentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AgentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(UserId, otherT.UserId)) return false;
                if( !DeepComparable.Matches(RelatedAgent, otherT.RelatedAgent)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AgentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(UserId, otherT.UserId)) return false;
                if( !DeepComparable.IsExactly(RelatedAgent, otherT.RelatedAgent)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Role != null) yield return Role;
                    if (Actor != null) yield return Actor;
                    if (UserId != null) yield return UserId;
                    foreach (var elem in RelatedAgent) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                    if (UserId != null) yield return new ElementValue("userId", UserId);
                    foreach (var elem in RelatedAgent) { if (elem != null) yield return new ElementValue("relatedAgent", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "RelatedAgentComponent")]
        [DataContract]
        public partial class RelatedAgentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedAgentComponent"; } }
            
            /// <summary>
            /// Type of relationship between agents
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Reference to other agent in this resource by identifier
            /// </summary>
            [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _TargetElement;
            
            /// <summary>
            /// Reference to other agent in this resource by identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Target
            {
                get { return TargetElement != null ? TargetElement.Value : null; }
                set
                {
                    if (value == null)
                        TargetElement = null;
                    else
                        TargetElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Target");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RelatedAgentComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
                sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TargetElement?.Serialize(sink);
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
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "target":
                        TargetElement = source.Get<Hl7.Fhir.Model.FhirUri>();
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
                        Type = source.Populate(Type);
                        return true;
                    case "target":
                        TargetElement = source.PopulateValue(TargetElement);
                        return true;
                    case "_target":
                        TargetElement = source.Populate(TargetElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedAgentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = (Hl7.Fhir.Model.FhirUri)TargetElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RelatedAgentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedAgentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedAgentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (TargetElement != null) yield return TargetElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (TargetElement != null) yield return new ElementValue("target", TargetElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "EntityComponent")]
        [DataContract]
        public partial class EntityComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProvenanceEntityComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EntityComponent"; } }
            
            /// <summary>
            /// derivation | revision | quotation | source
            /// </summary>
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.ProvenanceEntityRole> RoleElement
            {
                get { return _RoleElement; }
                set { _RoleElement = value; OnPropertyChanged("RoleElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.ProvenanceEntityRole> _RoleElement;
            
            /// <summary>
            /// derivation | revision | quotation | source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.ProvenanceEntityRole? Role
            {
                get { return RoleElement != null ? RoleElement.Value : null; }
                set
                {
                    if (value == null)
                        RoleElement = null;
                    else
                        RoleElement = new Code<Hl7.Fhir.Model.DSTU2.ProvenanceEntityRole>(value);
                    OnPropertyChanged("Role");
                }
            }
            
            /// <summary>
            /// The type of resource in this entity
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Identity of entity
            /// </summary>
            [FhirElement("reference", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ReferenceElement
            {
                get { return _ReferenceElement; }
                set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _ReferenceElement;
            
            /// <summary>
            /// Identity of entity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Reference
            {
                get { return ReferenceElement != null ? ReferenceElement.Value : null; }
                set
                {
                    if (value == null)
                        ReferenceElement = null;
                    else
                        ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Reference");
                }
            }
            
            /// <summary>
            /// Human description of entity
            /// </summary>
            [FhirElement("display", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Human description of entity
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
            /// Entity is attributed to this agent
            /// </summary>
            [FhirElement("agent", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public AgentComponent Agent
            {
                get { return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            
            private AgentComponent _Agent;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EntityComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RoleElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ReferenceElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisplayElement?.Serialize(sink);
                sink.Element("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Agent?.Serialize(sink);
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
                    case "role":
                        RoleElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ProvenanceEntityRole>>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "reference":
                        ReferenceElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "display":
                        DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "agent":
                        Agent = source.Get<AgentComponent>();
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
                    case "role":
                        RoleElement = source.PopulateValue(RoleElement);
                        return true;
                    case "_role":
                        RoleElement = source.Populate(RoleElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "reference":
                        ReferenceElement = source.PopulateValue(ReferenceElement);
                        return true;
                    case "_reference":
                        ReferenceElement = source.Populate(ReferenceElement);
                        return true;
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "agent":
                        Agent = source.Populate(Agent);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EntityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RoleElement != null) dest.RoleElement = (Code<Hl7.Fhir.Model.DSTU2.ProvenanceEntityRole>)RoleElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(Agent != null) dest.Agent = (AgentComponent)Agent.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EntityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EntityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RoleElement, otherT.RoleElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EntityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RoleElement, otherT.RoleElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RoleElement != null) yield return RoleElement;
                    if (Type != null) yield return Type;
                    if (ReferenceElement != null) yield return ReferenceElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    if (Agent != null) yield return Agent;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RoleElement != null) yield return new ElementValue("role", RoleElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    if (Agent != null) yield return new ElementValue("agent", Agent);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IProvenanceAgentComponent> Hl7.Fhir.Model.IProvenance.Agent { get { return Agent; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IProvenanceEntityComponent> Hl7.Fhir.Model.IProvenance.Entity { get { return Entity; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ISignature> Hl7.Fhir.Model.IProvenance.Signature { get { return Signature; } }
    
        
        /// <summary>
        /// Target Reference(s) (usually version specific)
        /// </summary>
        [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Target
        {
            get { if(_Target==null) _Target = new List<Hl7.Fhir.Model.ResourceReference>(); return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Target;
        
        /// <summary>
        /// When the activity occurred
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// When the activity was recorded / updated
        /// </summary>
        [FhirElement("recorded", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant RecordedElement
        {
            get { return _RecordedElement; }
            set { _RecordedElement = value; OnPropertyChanged("RecordedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _RecordedElement;
        
        /// <summary>
        /// When the activity was recorded / updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Recorded
        {
            get { return RecordedElement != null ? RecordedElement.Value : null; }
            set
            {
                if (value == null)
                    RecordedElement = null;
                else
                    RecordedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Recorded");
            }
        }
        
        /// <summary>
        /// Reason the activity is occurring
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// Activity that occurred
        /// </summary>
        [FhirElement("activity", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Activity
        {
            get { return _Activity; }
            set { _Activity = value; OnPropertyChanged("Activity"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Activity;
        
        /// <summary>
        /// Where the activity occurred, if relevant
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Policy or plan the activity was defined by
        /// </summary>
        [FhirElement("policy", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> PolicyElement
        {
            get { if(_PolicyElement==null) _PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(); return _PolicyElement; }
            set { _PolicyElement = value; OnPropertyChanged("PolicyElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _PolicyElement;
        
        /// <summary>
        /// Policy or plan the activity was defined by
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Policy
        {
            get { return PolicyElement != null ? PolicyElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    PolicyElement = null;
                else
                    PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Policy");
            }
        }
        
        /// <summary>
        /// Agents involved in creating resource
        /// </summary>
        [FhirElement("agent", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AgentComponent> Agent
        {
            get { if(_Agent==null) _Agent = new List<AgentComponent>(); return _Agent; }
            set { _Agent = value; OnPropertyChanged("Agent"); }
        }
        
        private List<AgentComponent> _Agent;
        
        /// <summary>
        /// An entity used in this activity
        /// </summary>
        [FhirElement("entity", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EntityComponent> Entity
        {
            get { if(_Entity==null) _Entity = new List<EntityComponent>(); return _Entity; }
            set { _Entity = value; OnPropertyChanged("Entity"); }
        }
        
        private List<EntityComponent> _Entity;
        
        /// <summary>
        /// Signature on target
        /// </summary>
        [FhirElement("signature", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DSTU2.Signature> Signature
        {
            get { if(_Signature==null) _Signature = new List<Hl7.Fhir.Model.DSTU2.Signature>(); return _Signature; }
            set { _Signature = value; OnPropertyChanged("Signature"); }
        }
        
        private List<Hl7.Fhir.Model.DSTU2.Signature> _Signature;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Provenance;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Target != null) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(RecordedElement != null) dest.RecordedElement = (Hl7.Fhir.Model.Instant)RecordedElement.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(Activity != null) dest.Activity = (Hl7.Fhir.Model.CodeableConcept)Activity.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(PolicyElement != null) dest.PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(PolicyElement.DeepCopy());
                if(Agent != null) dest.Agent = new List<AgentComponent>(Agent.DeepCopy());
                if(Entity != null) dest.Entity = new List<EntityComponent>(Entity.DeepCopy());
                if(Signature != null) dest.Signature = new List<Hl7.Fhir.Model.DSTU2.Signature>(Signature.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Provenance());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Provenance;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(RecordedElement, otherT.RecordedElement)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(PolicyElement, otherT.PolicyElement)) return false;
            if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
            if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
            if( !DeepComparable.Matches(Signature, otherT.Signature)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Provenance;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(RecordedElement, otherT.RecordedElement)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(PolicyElement, otherT.PolicyElement)) return false;
            if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
            if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
            if( !DeepComparable.IsExactly(Signature, otherT.Signature)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Provenance");
            base.Serialize(sink);
            sink.BeginList("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Target)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.Element("recorded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RecordedElement?.Serialize(sink);
            sink.BeginList("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Reason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("activity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Activity?.Serialize(sink);
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Location?.Serialize(sink);
            sink.BeginList("policy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(PolicyElement);
            sink.End();
            sink.BeginList("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Agent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Entity)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("signature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Signature)
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
                case "target":
                    Target = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "recorded":
                    RecordedElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "reason":
                    Reason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "activity":
                    Activity = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "policy":
                    PolicyElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "agent":
                    Agent = source.GetList<AgentComponent>();
                    return true;
                case "entity":
                    Entity = source.GetList<EntityComponent>();
                    return true;
                case "signature":
                    Signature = source.GetList<Hl7.Fhir.Model.DSTU2.Signature>();
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
                case "target":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "recorded":
                    RecordedElement = source.PopulateValue(RecordedElement);
                    return true;
                case "_recorded":
                    RecordedElement = source.Populate(RecordedElement);
                    return true;
                case "reason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "activity":
                    Activity = source.Populate(Activity);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "policy":
                case "_policy":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "agent":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "entity":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "signature":
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
                case "target":
                    source.PopulateListItem(Target, index);
                    return true;
                case "reason":
                    source.PopulateListItem(Reason, index);
                    return true;
                case "policy":
                    source.PopulatePrimitiveListItemValue(PolicyElement, index);
                    return true;
                case "_policy":
                    source.PopulatePrimitiveListItem(PolicyElement, index);
                    return true;
                case "agent":
                    source.PopulateListItem(Agent, index);
                    return true;
                case "entity":
                    source.PopulateListItem(Entity, index);
                    return true;
                case "signature":
                    source.PopulateListItem(Signature, index);
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
                foreach (var elem in Target) { if (elem != null) yield return elem; }
                if (Period != null) yield return Period;
                if (RecordedElement != null) yield return RecordedElement;
                foreach (var elem in Reason) { if (elem != null) yield return elem; }
                if (Activity != null) yield return Activity;
                if (Location != null) yield return Location;
                foreach (var elem in PolicyElement) { if (elem != null) yield return elem; }
                foreach (var elem in Agent) { if (elem != null) yield return elem; }
                foreach (var elem in Entity) { if (elem != null) yield return elem; }
                foreach (var elem in Signature) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
                if (RecordedElement != null) yield return new ElementValue("recorded", RecordedElement);
                foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                if (Activity != null) yield return new ElementValue("activity", Activity);
                if (Location != null) yield return new ElementValue("location", Location);
                foreach (var elem in PolicyElement) { if (elem != null) yield return new ElementValue("policy", elem); }
                foreach (var elem in Agent) { if (elem != null) yield return new ElementValue("agent", elem); }
                foreach (var elem in Entity) { if (elem != null) yield return new ElementValue("entity", elem); }
                foreach (var elem in Signature) { if (elem != null) yield return new ElementValue("signature", elem); }
            }
        }
    
    }

}
