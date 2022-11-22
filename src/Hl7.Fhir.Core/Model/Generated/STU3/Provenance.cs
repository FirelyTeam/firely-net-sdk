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
    /// Who, What, When for a set of resources
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Provenance", IsResource=true)]
    [DataContract]
    public partial class Provenance : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IProvenance, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Provenance; } }
        [NotMapped]
        public override string TypeName { get { return "Provenance"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AgentComponent")]
        [DataContract]
        public partial class AgentComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProvenanceAgentComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AgentComponent"; } }
            
            /// <summary>
            /// What the agents role was
            /// </summary>
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { if(_Role==null) _Role = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
            
            /// <summary>
            /// Who participated
            /// </summary>
            [FhirElement("who", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Who
            {
                get { return _Who; }
                set { _Who = value; OnPropertyChanged("Who"); }
            }
            
            private Hl7.Fhir.Model.Element _Who;
            
            /// <summary>
            /// Who the agent is representing
            /// </summary>
            [FhirElement("onBehalfOf", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.Element _OnBehalfOf;
            
            /// <summary>
            /// Type of relationship between agents
            /// </summary>
            [FhirElement("relatedAgentType", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept RelatedAgentType
            {
                get { return _RelatedAgentType; }
                set { _RelatedAgentType = value; OnPropertyChanged("RelatedAgentType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _RelatedAgentType;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AgentComponent");
                base.Serialize(sink);
                sink.BeginList("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Role)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("who", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Who?.Serialize(sink);
                sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); OnBehalfOf?.Serialize(sink);
                sink.Element("relatedAgentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RelatedAgentType?.Serialize(sink);
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
                    case "role":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "whoUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Who, "who");
                        Who = source.PopulateValue(Who as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_whoUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Who, "who");
                        Who = source.Populate(Who as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "whoReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Who, "who");
                        Who = source.Populate(Who as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "onBehalfOfUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(OnBehalfOf, "onBehalfOf");
                        OnBehalfOf = source.PopulateValue(OnBehalfOf as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_onBehalfOfUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(OnBehalfOf, "onBehalfOf");
                        OnBehalfOf = source.Populate(OnBehalfOf as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "onBehalfOfReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(OnBehalfOf, "onBehalfOf");
                        OnBehalfOf = source.Populate(OnBehalfOf as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "relatedAgentType":
                        RelatedAgentType = source.Populate(RelatedAgentType);
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
                    case "role":
                        source.PopulateListItem(Role, index);
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
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    if(Who != null) dest.Who = (Hl7.Fhir.Model.Element)Who.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.Element)OnBehalfOf.DeepCopy();
                    if(RelatedAgentType != null) dest.RelatedAgentType = (Hl7.Fhir.Model.CodeableConcept)RelatedAgentType.DeepCopy();
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
                if( !DeepComparable.Matches(Who, otherT.Who)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
                if( !DeepComparable.Matches(RelatedAgentType, otherT.RelatedAgentType)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AgentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
                if( !DeepComparable.IsExactly(RelatedAgentType, otherT.RelatedAgentType)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Role) { if (elem != null) yield return elem; }
                    if (Who != null) yield return Who;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                    if (RelatedAgentType != null) yield return RelatedAgentType;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
                    if (Who != null) yield return new ElementValue("who", Who);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                    if (RelatedAgentType != null) yield return new ElementValue("relatedAgentType", RelatedAgentType);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "EntityComponent")]
        [DataContract]
        public partial class EntityComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProvenanceEntityComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EntityComponent"; } }
            
            /// <summary>
            /// derivation | revision | quotation | source | removal
            /// </summary>
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.ProvenanceEntityRole> RoleElement
            {
                get { return _RoleElement; }
                set { _RoleElement = value; OnPropertyChanged("RoleElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.ProvenanceEntityRole> _RoleElement;
            
            /// <summary>
            /// derivation | revision | quotation | source | removal
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.ProvenanceEntityRole? Role
            {
                get { return RoleElement != null ? RoleElement.Value : null; }
                set
                {
                    if (value == null)
                        RoleElement = null;
                    else
                        RoleElement = new Code<Hl7.Fhir.Model.STU3.ProvenanceEntityRole>(value);
                    OnPropertyChanged("Role");
                }
            }
            
            /// <summary>
            /// Identity of entity
            /// </summary>
            [FhirElement("what", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Identifier))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element What
            {
                get { return _What; }
                set { _What = value; OnPropertyChanged("What"); }
            }
            
            private Hl7.Fhir.Model.Element _What;
            
            /// <summary>
            /// Entity is attributed to this agent
            /// </summary>
            [FhirElement("agent", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AgentComponent> Agent
            {
                get { if(_Agent==null) _Agent = new List<AgentComponent>(); return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            
            private List<AgentComponent> _Agent;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EntityComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RoleElement?.Serialize(sink);
                sink.Element("what", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); What?.Serialize(sink);
                sink.BeginList("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Agent)
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
                    case "role":
                        RoleElement = source.PopulateValue(RoleElement);
                        return true;
                    case "_role":
                        RoleElement = source.Populate(RoleElement);
                        return true;
                    case "whatUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(What, "what");
                        What = source.PopulateValue(What as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_whatUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(What, "what");
                        What = source.Populate(What as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "whatReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(What, "what");
                        What = source.Populate(What as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "whatIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(What, "what");
                        What = source.Populate(What as Hl7.Fhir.Model.Identifier);
                        return true;
                    case "agent":
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
                    case "agent":
                        source.PopulateListItem(Agent, index);
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
                    if(RoleElement != null) dest.RoleElement = (Code<Hl7.Fhir.Model.STU3.ProvenanceEntityRole>)RoleElement.DeepCopy();
                    if(What != null) dest.What = (Hl7.Fhir.Model.Element)What.DeepCopy();
                    if(Agent != null) dest.Agent = new List<AgentComponent>(Agent.DeepCopy());
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
                if( !DeepComparable.Matches(What, otherT.What)) return false;
                if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EntityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RoleElement, otherT.RoleElement)) return false;
                if( !DeepComparable.IsExactly(What, otherT.What)) return false;
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
                    if (What != null) yield return What;
                    foreach (var elem in Agent) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RoleElement != null) yield return new ElementValue("role", RoleElement);
                    if (What != null) yield return new ElementValue("what", What);
                    foreach (var elem in Agent) { if (elem != null) yield return new ElementValue("agent", elem); }
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
        [FhirElement("period", Order=100)]
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
        /// Policy or plan the activity was defined by
        /// </summary>
        [FhirElement("policy", Order=120)]
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
        /// Where the activity occurred, if relevant
        /// </summary>
        [FhirElement("location", Order=130)]
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
        /// Reason the activity is occurring
        /// </summary>
        [FhirElement("reason", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.Coding>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Reason;
        
        /// <summary>
        /// Activity that occurred
        /// </summary>
        [FhirElement("activity", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Activity
        {
            get { return _Activity; }
            set { _Activity = value; OnPropertyChanged("Activity"); }
        }
        
        private Hl7.Fhir.Model.Coding _Activity;
        
        /// <summary>
        /// Actor involved
        /// </summary>
        [FhirElement("agent", Order=160)]
        [Cardinality(Min=1,Max=-1)]
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
        [FhirElement("entity", Order=170)]
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
        [FhirElement("signature", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.Signature> Signature
        {
            get { if(_Signature==null) _Signature = new List<Hl7.Fhir.Model.STU3.Signature>(); return _Signature; }
            set { _Signature = value; OnPropertyChanged("Signature"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.Signature> _Signature;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Provenance;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Target != null) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(RecordedElement != null) dest.RecordedElement = (Hl7.Fhir.Model.Instant)RecordedElement.DeepCopy();
                if(PolicyElement != null) dest.PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(PolicyElement.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.Coding>(Reason.DeepCopy());
                if(Activity != null) dest.Activity = (Hl7.Fhir.Model.Coding)Activity.DeepCopy();
                if(Agent != null) dest.Agent = new List<AgentComponent>(Agent.DeepCopy());
                if(Entity != null) dest.Entity = new List<EntityComponent>(Entity.DeepCopy());
                if(Signature != null) dest.Signature = new List<Hl7.Fhir.Model.STU3.Signature>(Signature.DeepCopy());
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
            if( !DeepComparable.Matches(PolicyElement, otherT.PolicyElement)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
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
            if( !DeepComparable.IsExactly(PolicyElement, otherT.PolicyElement)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
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
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
            sink.Element("recorded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RecordedElement?.Serialize(sink);
            sink.BeginList("policy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(PolicyElement);
            sink.End();
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Location?.Serialize(sink);
            sink.BeginList("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Reason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("activity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Activity?.Serialize(sink);
            sink.BeginList("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
            foreach(var item in Agent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Entity)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("signature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Signature)
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
                case "policy":
                case "_policy":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "reason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "activity":
                    Activity = source.Populate(Activity);
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
                case "policy":
                    source.PopulatePrimitiveListItemValue(PolicyElement, index);
                    return true;
                case "_policy":
                    source.PopulatePrimitiveListItem(PolicyElement, index);
                    return true;
                case "reason":
                    source.PopulateListItem(Reason, index);
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
                foreach (var elem in PolicyElement) { if (elem != null) yield return elem; }
                if (Location != null) yield return Location;
                foreach (var elem in Reason) { if (elem != null) yield return elem; }
                if (Activity != null) yield return Activity;
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
                foreach (var elem in PolicyElement) { if (elem != null) yield return new ElementValue("policy", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                if (Activity != null) yield return new ElementValue("activity", Activity);
                foreach (var elem in Agent) { if (elem != null) yield return new ElementValue("agent", elem); }
                foreach (var elem in Entity) { if (elem != null) yield return new ElementValue("entity", elem); }
                foreach (var elem in Signature) { if (elem != null) yield return new ElementValue("signature", elem); }
            }
        }
    
    }

}
