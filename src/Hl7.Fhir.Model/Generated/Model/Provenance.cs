using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Who, What, When for a set of resources
    /// </summary>
    [FhirType("Provenance", IsResource=true)]
    [DataContract]
    public partial class Provenance : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// How an entity was used in an activity
        /// </summary>
        [FhirEnumeration("ProvenanceEntityRole")]
        public enum ProvenanceEntityRole
        {
            [EnumLiteral("derivation")]
            Derivation, // A transformation of an entity into another, an update of an entity resulting in a new one, or the construction of a new entity based on a preexisting entity.
            [EnumLiteral("revision")]
            Revision, // A derivation for which the resulting entity is a revised version of some original.
            [EnumLiteral("quotation")]
            Quotation, // The repeat of (some or all of) an entity, such as text or image, by someone who may or may not be its original author.
            [EnumLiteral("source")]
            Source, // A primary source for a topic refers to something produced by some agent with direct experience and knowledge about the topic, at the time of the topic's study, without benefit from hindsight.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProvenanceAgentComponent")]
        [DataContract]
        public partial class ProvenanceAgentComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// e.g. author | overseer | enterer | attester | source | cc: +
            /// </summary>
            [FhirElement("role", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            private Hl7.Fhir.Model.Coding _Role;
            
            /// <summary>
            /// e.g. Resource | Person | Application | Record | Document +
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Identity of agent (urn or url)
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ReferenceElement
            {
                get { return _ReferenceElement; }
                set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _ReferenceElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Reference
            {
                get { return ReferenceElement != null ? ReferenceElement.Value : null; }
                set
                {
                    if(value == null)
                      ReferenceElement = null; 
                    else
                      ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Reference");
                }
            }
            
            /// <summary>
            /// Human description of participant
            /// </summary>
            [FhirElement("display", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if(value == null)
                      DisplayElement = null; 
                    else
                      DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProvenanceEntityComponent")]
        [DataContract]
        public partial class ProvenanceEntityComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// derivation | revision | quotation | source
            /// </summary>
            [FhirElement("role", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole> RoleElement
            {
                get { return _RoleElement; }
                set { _RoleElement = value; OnPropertyChanged("RoleElement"); }
            }
            private Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole> _RoleElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Provenance.ProvenanceEntityRole? Role
            {
                get { return RoleElement != null ? RoleElement.Value : null; }
                set
                {
                    if(value == null)
                      RoleElement = null; 
                    else
                      RoleElement = new Code<Hl7.Fhir.Model.Provenance.ProvenanceEntityRole>(value);
                    OnPropertyChanged("Role");
                }
            }
            
            /// <summary>
            /// Resource Type, or something else
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Identity of participant (urn or url)
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ReferenceElement
            {
                get { return _ReferenceElement; }
                set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _ReferenceElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Reference
            {
                get { return ReferenceElement != null ? ReferenceElement.Value : null; }
                set
                {
                    if(value == null)
                      ReferenceElement = null; 
                    else
                      ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Reference");
                }
            }
            
            /// <summary>
            /// Human description of participant
            /// </summary>
            [FhirElement("display", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if(value == null)
                      DisplayElement = null; 
                    else
                      DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// Entity is attributed to this agent
            /// </summary>
            [FhirElement("agent", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent Agent
            {
                get { return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            private Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent _Agent;
            
        }
        
        
        /// <summary>
        /// Target resource(s) (usually version specific)
        /// </summary>
        [FhirElement("target", Order=70)]
        [References()]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Target;
        
        /// <summary>
        /// When the activity occurred
        /// </summary>
        [FhirElement("period", Order=80)]
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
        [FhirElement("recorded", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant RecordedElement
        {
            get { return _RecordedElement; }
            set { _RecordedElement = value; OnPropertyChanged("RecordedElement"); }
        }
        private Hl7.Fhir.Model.Instant _RecordedElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Recorded
        {
            get { return RecordedElement != null ? RecordedElement.Value : null; }
            set
            {
                if(value == null)
                  RecordedElement = null; 
                else
                  RecordedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Recorded");
            }
        }
        
        /// <summary>
        /// Reason the activity is occurring
        /// </summary>
        [FhirElement("reason", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// Where the activity occurred, if relevant
        /// </summary>
        [FhirElement("location", Order=110)]
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
        [FhirElement("policy", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> PolicyElement
        {
            get { return _PolicyElement; }
            set { _PolicyElement = value; OnPropertyChanged("PolicyElement"); }
        }
        private List<Hl7.Fhir.Model.FhirUri> _PolicyElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<System.Uri> Policy
        {
            get { return PolicyElement != null ? PolicyElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  PolicyElement = null; 
                else
                  PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Policy");
            }
        }
        
        /// <summary>
        /// Person, organization, records, etc. involved in creating resource
        /// </summary>
        [FhirElement("agent", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent> Agent
        {
            get { return _Agent; }
            set { _Agent = value; OnPropertyChanged("Agent"); }
        }
        private List<Hl7.Fhir.Model.Provenance.ProvenanceAgentComponent> _Agent;
        
        /// <summary>
        /// An entity used in this activity
        /// </summary>
        [FhirElement("entity", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Provenance.ProvenanceEntityComponent> Entity
        {
            get { return _Entity; }
            set { _Entity = value; OnPropertyChanged("Entity"); }
        }
        private List<Hl7.Fhir.Model.Provenance.ProvenanceEntityComponent> _Entity;
        
        /// <summary>
        /// Base64 signature (DigSig) - integrity check
        /// </summary>
        [FhirElement("integritySignature", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IntegritySignatureElement
        {
            get { return _IntegritySignatureElement; }
            set { _IntegritySignatureElement = value; OnPropertyChanged("IntegritySignatureElement"); }
        }
        private Hl7.Fhir.Model.FhirString _IntegritySignatureElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string IntegritySignature
        {
            get { return IntegritySignatureElement != null ? IntegritySignatureElement.Value : null; }
            set
            {
                if(value == null)
                  IntegritySignatureElement = null; 
                else
                  IntegritySignatureElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("IntegritySignature");
            }
        }
        
    }
    
}
