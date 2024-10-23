// <auto-generated/>
// Contents of: hl7.fhir.r4b.expansions@4.3.0, hl7.fhir.r4b.core@4.3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using SystemPrimitive = Hl7.Fhir.ElementModel.Types;

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

namespace Hl7.Fhir.Model
{
  /// <summary>
  /// Who, What, When for a set of resources
  /// </summary>
  /// <remarks>
  /// Provenance of a resource is a record that describes entities and processes involved in producing and delivering or otherwise influencing that resource. Provenance provides a critical foundation for assessing authenticity, enabling trust, and allowing reproducibility. Provenance assertions are a form of contextual metadata and can themselves become important records with their own provenance. Provenance statement indicates clinical significance in terms of confidence in authenticity, reliability, and trustworthiness, integrity, and stage in lifecycle (e.g. Document Completion - has the artifact been legally authenticated), all of which may impact security, privacy, and trust policies.
  /// Some parties may be duplicated between the target resource and its provenance.  For instance, the prescriber is usually (but not always) the author of the prescription resource. This resource is defined with close consideration for W3C Provenance.
  /// </remarks>
  [Serializable]
  [DataContract]
  [FhirType("Provenance","http://hl7.org/fhir/StructureDefinition/Provenance")]
  public partial class Provenance : Hl7.Fhir.Model.DomainResource
  {
    /// <summary>
    /// FHIR Type Name
    /// </summary>
    public override string TypeName { get { return "Provenance"; } }

    /// <summary>
    /// How an entity was used in an activity.
    /// (url: http://hl7.org/fhir/ValueSet/provenance-entity-role)
    /// (system: http://hl7.org/fhir/provenance-entity-role)
    /// </summary>
    [FhirEnumeration("ProvenanceEntityRole", "http://hl7.org/fhir/ValueSet/provenance-entity-role", "http://hl7.org/fhir/provenance-entity-role")]
    public enum ProvenanceEntityRole
    {
      /// <summary>
      /// A transformation of an entity into another, an update of an entity resulting in a new one, or the construction of a new entity based on a pre-existing entity.
      /// (system: http://hl7.org/fhir/provenance-entity-role)
      /// </summary>
      [EnumLiteral("derivation"), Description("Derivation")]
      Derivation,
      /// <summary>
      /// A derivation for which the resulting entity is a revised version of some original.
      /// (system: http://hl7.org/fhir/provenance-entity-role)
      /// </summary>
      [EnumLiteral("revision"), Description("Revision")]
      Revision,
      /// <summary>
      /// The repeat of (some or all of) an entity, such as text or image, by someone who might or might not be its original author.
      /// (system: http://hl7.org/fhir/provenance-entity-role)
      /// </summary>
      [EnumLiteral("quotation"), Description("Quotation")]
      Quotation,
      /// <summary>
      /// A primary source for a topic refers to something produced by some agent with direct experience and knowledge about the topic, at the time of the topic's study, without benefit from hindsight.
      /// (system: http://hl7.org/fhir/provenance-entity-role)
      /// </summary>
      [EnumLiteral("source"), Description("Source")]
      Source,
      /// <summary>
      /// A derivation for which the entity is removed from accessibility usually through the use of the Delete operation.
      /// (system: http://hl7.org/fhir/provenance-entity-role)
      /// </summary>
      [EnumLiteral("removal"), Description("Removal")]
      Removal,
    }

    /// <summary>
    /// Actor involved
    /// </summary>
    /// <remarks>
    /// An actor taking a role in an activity  for which it can be assigned some degree of responsibility for the activity taking place.
    /// Several agents may be associated (i.e. has some responsibility for an activity) with an activity and vice-versa.
    /// </remarks>
    [Serializable]
    [DataContract]
    [FhirType("Provenance.agent", IsBackboneType=true)]
    public partial class AgentComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Provenance.agent"; } }

      /// <summary>
      /// How the agent participated
      /// </summary>
      [FhirElement("type", InSummary=true, Order=40)]
      [Binding("ProvenanceAgentType")]
      [DataMember]
      public Hl7.Fhir.Model.CodeableConcept Type
      {
        get { return _Type; }
        set { _Type = value; OnPropertyChanged("Type"); }
      }

      private Hl7.Fhir.Model.CodeableConcept _Type;

      /// <summary>
      /// What the agents role was
      /// </summary>
      [FhirElement("role", Order=50)]
      [Binding("ProvenanceAgentRole")]
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
      [FhirElement("who", InSummary=true, Order=60, FiveWs="FiveWs.actor")]
      [CLSCompliant(false)]
      [References("Practitioner","PractitionerRole","RelatedPerson","Patient","Device","Organization")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference Who
      {
        get { return _Who; }
        set { _Who = value; OnPropertyChanged("Who"); }
      }

      private Hl7.Fhir.Model.ResourceReference _Who;

      /// <summary>
      /// Who the agent is representing
      /// </summary>
      [FhirElement("onBehalfOf", Order=70)]
      [CLSCompliant(false)]
      [References("Practitioner","PractitionerRole","RelatedPerson","Patient","Device","Organization")]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference OnBehalfOf
      {
        get { return _OnBehalfOf; }
        set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
      }

      private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as AgentComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
        if(Role.Any()) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
        if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
        if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new AgentComponent());
      }

      ///<inheritdoc />
      public override bool Matches(IDeepComparable other)
      {
        var otherT = other as AgentComponent;
        if(otherT == null) return false;

        if(!base.Matches(otherT)) return false;
        if( !DeepComparable.Matches(Type, otherT.Type)) return false;
        if( !DeepComparable.Matches(Role, otherT.Role)) return false;
        if( !DeepComparable.Matches(Who, otherT.Who)) return false;
        if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;

        return true;
      }

      public override bool IsExactly(IDeepComparable other)
      {
        var otherT = other as AgentComponent;
        if(otherT == null) return false;

        if(!base.IsExactly(otherT)) return false;
        if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
        if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
        if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
        if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;

        return true;
      }

      [IgnoreDataMember]
      public override IEnumerable<Base> Children
      {
        get
        {
          foreach (var item in base.Children) yield return item;
          if (Type != null) yield return Type;
          foreach (var elem in Role) { if (elem != null) yield return elem; }
          if (Who != null) yield return Who;
          if (OnBehalfOf != null) yield return OnBehalfOf;
        }
      }

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (Type != null) yield return new ElementValue("type", Type);
          foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
          if (Who != null) yield return new ElementValue("who", Who);
          if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "type":
            value = Type;
            return Type is not null;
          case "role":
            value = Role;
            return Role?.Any() == true;
          case "who":
            value = Who;
            return Who is not null;
          case "onBehalfOf":
            value = OnBehalfOf;
            return OnBehalfOf is not null;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "type":
            Type = (Hl7.Fhir.Model.CodeableConcept)value;
            return this;
          case "role":
            Role = (List<Hl7.Fhir.Model.CodeableConcept>)value;
            return this;
          case "who":
            Who = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          case "onBehalfOf":
            OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (Type is not null) yield return new KeyValuePair<string,object>("type",Type);
        if (Role?.Any() == true) yield return new KeyValuePair<string,object>("role",Role);
        if (Who is not null) yield return new KeyValuePair<string,object>("who",Who);
        if (OnBehalfOf is not null) yield return new KeyValuePair<string,object>("onBehalfOf",OnBehalfOf);
      }

    }

    /// <summary>
    /// An entity used in this activity
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Provenance.entity", IsBackboneType=true)]
    public partial class EntityComponent : Hl7.Fhir.Model.BackboneElement
    {
      /// <summary>
      /// FHIR Type Name
      /// </summary>
      public override string TypeName { get { return "Provenance.entity"; } }

      /// <summary>
      /// derivation | revision | quotation | source | removal
      /// </summary>
      [FhirElement("role", InSummary=true, Order=40)]
      [DeclaredType(Type = typeof(Code))]
      [Binding("ProvenanceEntityRole")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole> RoleElement
      {
        get { return _RoleElement; }
        set { _RoleElement = value; OnPropertyChanged("RoleElement"); }
      }

      private Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole> _RoleElement;

      /// <summary>
      /// derivation | revision | quotation | source | removal
      /// </summary>
      /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
      [IgnoreDataMember]
      public Hl7.Fhir.Model.Provenance.ProvenanceEntityRole? Role
      {
        get { return RoleElement != null ? RoleElement.Value : null; }
        set
        {
          if (value == null)
            RoleElement = null;
          else
            RoleElement = new Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole>(value);
          OnPropertyChanged("Role");
        }
      }

      /// <summary>
      /// Identity of entity
      /// </summary>
      [FhirElement("what", InSummary=true, Order=50)]
      [CLSCompliant(false)]
      [References("Resource")]
      [Cardinality(Min=1,Max=1)]
      [DataMember]
      public Hl7.Fhir.Model.ResourceReference What
      {
        get { return _What; }
        set { _What = value; OnPropertyChanged("What"); }
      }

      private Hl7.Fhir.Model.ResourceReference _What;

      /// <summary>
      /// Entity is attributed to this agent
      /// </summary>
      [FhirElement("agent", Order=60)]
      [Cardinality(Min=0,Max=-1)]
      [DataMember]
      public List<Hl7.Fhir.Model.Provenance.AgentComponent> Agent
      {
        get { if(_Agent==null) _Agent = new List<Hl7.Fhir.Model.Provenance.AgentComponent>(); return _Agent; }
        set { _Agent = value; OnPropertyChanged("Agent"); }
      }

      private List<Hl7.Fhir.Model.Provenance.AgentComponent> _Agent;

      public override IDeepCopyable CopyTo(IDeepCopyable other)
      {
        var dest = other as EntityComponent;

        if (dest == null)
        {
          throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        base.CopyTo(dest);
        if(RoleElement != null) dest.RoleElement = (Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole>)RoleElement.DeepCopy();
        if(What != null) dest.What = (Hl7.Fhir.Model.ResourceReference)What.DeepCopy();
        if(Agent.Any()) dest.Agent = new List<Hl7.Fhir.Model.Provenance.AgentComponent>(Agent.DeepCopy());
        return dest;
      }

      public override IDeepCopyable DeepCopy()
      {
        return CopyTo(new EntityComponent());
      }

      ///<inheritdoc />
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

      [IgnoreDataMember]
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

      [IgnoreDataMember]
      public override IEnumerable<ElementValue> NamedChildren
      {
        get
        {
          foreach (var item in base.NamedChildren) yield return item;
          if (RoleElement != null) yield return new ElementValue("role", RoleElement);
          if (What != null) yield return new ElementValue("what", What);
          foreach (var elem in Agent) { if (elem != null) yield return new ElementValue("agent", elem); }
        }
      }

      protected override bool TryGetValue(string key, out object value)
      {
        switch (key)
        {
          case "role":
            value = RoleElement;
            return RoleElement is not null;
          case "what":
            value = What;
            return What is not null;
          case "agent":
            value = Agent;
            return Agent?.Any() == true;
          default:
            return base.TryGetValue(key, out value);
        }

      }

      protected override Base SetValue(string key, object value)
      {
        switch (key)
        {
          case "role":
            RoleElement = (Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole>)value;
            return this;
          case "what":
            What = (Hl7.Fhir.Model.ResourceReference)value;
            return this;
          case "agent":
            Agent = (List<Hl7.Fhir.Model.Provenance.AgentComponent>)value;
            return this;
          default:
            return base.SetValue(key, value);
        }

      }

      protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
      {
        foreach (var kvp in base.GetElementPairs()) yield return kvp;
        if (RoleElement is not null) yield return new KeyValuePair<string,object>("role",RoleElement);
        if (What is not null) yield return new KeyValuePair<string,object>("what",What);
        if (Agent?.Any() == true) yield return new KeyValuePair<string,object>("agent",Agent);
      }

    }

    /// <summary>
    /// Target Reference(s) (usually version specific)
    /// </summary>
    [FhirElement("target", InSummary=true, Order=90, FiveWs="FiveWs.what[x]")]
    [CLSCompliant(false)]
    [References("Resource")]
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
    [FhirElement("occurred", Order=100, Choice=ChoiceType.DatatypeChoice, FiveWs="FiveWs.done[x]")]
    [CLSCompliant(false)]
    [AllowedTypes(typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirDateTime))]
    [DataMember]
    public Hl7.Fhir.Model.DataType Occurred
    {
      get { return _Occurred; }
      set { _Occurred = value; OnPropertyChanged("Occurred"); }
    }

    private Hl7.Fhir.Model.DataType _Occurred;

    /// <summary>
    /// When the activity was recorded / updated
    /// </summary>
    [FhirElement("recorded", InSummary=true, Order=110, FiveWs="FiveWs.recorded")]
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
    [IgnoreDataMember]
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
    [IgnoreDataMember]
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
    [FhirElement("location", Order=130, FiveWs="FiveWs.where[x]")]
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
    [FhirElement("reason", Order=140, FiveWs="FiveWs.why[x]")]
    [Binding("ProvenanceReason")]
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
    [FhirElement("activity", Order=150, FiveWs="FiveWs.why[x]")]
    [Binding("ProvenanceActivity")]
    [DataMember]
    public Hl7.Fhir.Model.CodeableConcept Activity
    {
      get { return _Activity; }
      set { _Activity = value; OnPropertyChanged("Activity"); }
    }

    private Hl7.Fhir.Model.CodeableConcept _Activity;

    /// <summary>
    /// Actor involved
    /// </summary>
    [FhirElement("agent", Order=160, FiveWs="FiveWs.who")]
    [Cardinality(Min=1,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Provenance.AgentComponent> Agent
    {
      get { if(_Agent==null) _Agent = new List<Hl7.Fhir.Model.Provenance.AgentComponent>(); return _Agent; }
      set { _Agent = value; OnPropertyChanged("Agent"); }
    }

    private List<Hl7.Fhir.Model.Provenance.AgentComponent> _Agent;

    /// <summary>
    /// An entity used in this activity
    /// </summary>
    [FhirElement("entity", Order=170)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Provenance.EntityComponent> Entity
    {
      get { if(_Entity==null) _Entity = new List<Hl7.Fhir.Model.Provenance.EntityComponent>(); return _Entity; }
      set { _Entity = value; OnPropertyChanged("Entity"); }
    }

    private List<Hl7.Fhir.Model.Provenance.EntityComponent> _Entity;

    /// <summary>
    /// Signature on target
    /// </summary>
    [FhirElement("signature", Order=180)]
    [Cardinality(Min=0,Max=-1)]
    [DataMember]
    public List<Hl7.Fhir.Model.Signature> Signature
    {
      get { if(_Signature==null) _Signature = new List<Hl7.Fhir.Model.Signature>(); return _Signature; }
      set { _Signature = value; OnPropertyChanged("Signature"); }
    }

    private List<Hl7.Fhir.Model.Signature> _Signature;

    public override IDeepCopyable CopyTo(IDeepCopyable other)
    {
      var dest = other as Provenance;

      if (dest == null)
      {
        throw new ArgumentException("Can only copy to an object of the same type", "other");
      }

      base.CopyTo(dest);
      if(Target.Any()) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
      if(Occurred != null) dest.Occurred = (Hl7.Fhir.Model.DataType)Occurred.DeepCopy();
      if(RecordedElement != null) dest.RecordedElement = (Hl7.Fhir.Model.Instant)RecordedElement.DeepCopy();
      if(PolicyElement.Any()) dest.PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(PolicyElement.DeepCopy());
      if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
      if(Reason.Any()) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
      if(Activity != null) dest.Activity = (Hl7.Fhir.Model.CodeableConcept)Activity.DeepCopy();
      if(Agent.Any()) dest.Agent = new List<Hl7.Fhir.Model.Provenance.AgentComponent>(Agent.DeepCopy());
      if(Entity.Any()) dest.Entity = new List<Hl7.Fhir.Model.Provenance.EntityComponent>(Entity.DeepCopy());
      if(Signature.Any()) dest.Signature = new List<Hl7.Fhir.Model.Signature>(Signature.DeepCopy());
      return dest;
    }

    public override IDeepCopyable DeepCopy()
    {
      return CopyTo(new Provenance());
    }

    ///<inheritdoc />
    public override bool Matches(IDeepComparable other)
    {
      var otherT = other as Provenance;
      if(otherT == null) return false;

      if(!base.Matches(otherT)) return false;
      if( !DeepComparable.Matches(Target, otherT.Target)) return false;
      if( !DeepComparable.Matches(Occurred, otherT.Occurred)) return false;
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
      if( !DeepComparable.IsExactly(Occurred, otherT.Occurred)) return false;
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

    [IgnoreDataMember]
    public override IEnumerable<Base> Children
    {
      get
      {
        foreach (var item in base.Children) yield return item;
        foreach (var elem in Target) { if (elem != null) yield return elem; }
        if (Occurred != null) yield return Occurred;
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

    [IgnoreDataMember]
    public override IEnumerable<ElementValue> NamedChildren
    {
      get
      {
        foreach (var item in base.NamedChildren) yield return item;
        foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
        if (Occurred != null) yield return new ElementValue("occurred", Occurred);
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

    protected override bool TryGetValue(string key, out object value)
    {
      switch (key)
      {
        case "target":
          value = Target;
          return Target?.Any() == true;
        case "occurred":
          value = Occurred;
          return Occurred is not null;
        case "recorded":
          value = RecordedElement;
          return RecordedElement is not null;
        case "policy":
          value = PolicyElement;
          return PolicyElement?.Any() == true;
        case "location":
          value = Location;
          return Location is not null;
        case "reason":
          value = Reason;
          return Reason?.Any() == true;
        case "activity":
          value = Activity;
          return Activity is not null;
        case "agent":
          value = Agent;
          return Agent?.Any() == true;
        case "entity":
          value = Entity;
          return Entity?.Any() == true;
        case "signature":
          value = Signature;
          return Signature?.Any() == true;
        default:
          return base.TryGetValue(key, out value);
      }

    }

    protected override Base SetValue(string key, object value)
    {
      switch (key)
      {
        case "target":
          Target = (List<Hl7.Fhir.Model.ResourceReference>)value;
          return this;
        case "occurred":
          Occurred = (Hl7.Fhir.Model.DataType)value;
          return this;
        case "recorded":
          RecordedElement = (Hl7.Fhir.Model.Instant)value;
          return this;
        case "policy":
          PolicyElement = (List<Hl7.Fhir.Model.FhirUri>)value;
          return this;
        case "location":
          Location = (Hl7.Fhir.Model.ResourceReference)value;
          return this;
        case "reason":
          Reason = (List<Hl7.Fhir.Model.CodeableConcept>)value;
          return this;
        case "activity":
          Activity = (Hl7.Fhir.Model.CodeableConcept)value;
          return this;
        case "agent":
          Agent = (List<Hl7.Fhir.Model.Provenance.AgentComponent>)value;
          return this;
        case "entity":
          Entity = (List<Hl7.Fhir.Model.Provenance.EntityComponent>)value;
          return this;
        case "signature":
          Signature = (List<Hl7.Fhir.Model.Signature>)value;
          return this;
        default:
          return base.SetValue(key, value);
      }

    }

    protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
    {
      foreach (var kvp in base.GetElementPairs()) yield return kvp;
      if (Target?.Any() == true) yield return new KeyValuePair<string,object>("target",Target);
      if (Occurred is not null) yield return new KeyValuePair<string,object>("occurred",Occurred);
      if (RecordedElement is not null) yield return new KeyValuePair<string,object>("recorded",RecordedElement);
      if (PolicyElement?.Any() == true) yield return new KeyValuePair<string,object>("policy",PolicyElement);
      if (Location is not null) yield return new KeyValuePair<string,object>("location",Location);
      if (Reason?.Any() == true) yield return new KeyValuePair<string,object>("reason",Reason);
      if (Activity is not null) yield return new KeyValuePair<string,object>("activity",Activity);
      if (Agent?.Any() == true) yield return new KeyValuePair<string,object>("agent",Agent);
      if (Entity?.Any() == true) yield return new KeyValuePair<string,object>("entity",Entity);
      if (Signature?.Any() == true) yield return new KeyValuePair<string,object>("signature",Signature);
    }

  }

}

// end of file
