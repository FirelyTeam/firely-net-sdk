﻿using System;
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
    /// A record of information transmitted from a sender to a receiver
    /// </summary>
    [FhirType("Communication", IsResource=true)]
    [DataContract]
    public partial class Communication : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Communication; } }
        [NotMapped]
        public override string TypeName { get { return "Communication"; } }
        
        [FhirType("PayloadComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PayloadComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PayloadComponent"; } }
            
            /// <summary>
            /// Message part content
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayloadComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PayloadComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayloadComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayloadComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Instantiates FHIR protocol or definition
        /// </summary>
        [FhirElement("instantiatesCanonical", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> InstantiatesCanonicalElement
        {
            get { if(_InstantiatesCanonicalElement==null) _InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(); return _InstantiatesCanonicalElement; }
            set { _InstantiatesCanonicalElement = value; OnPropertyChanged("InstantiatesCanonicalElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _InstantiatesCanonicalElement;
        
        /// <summary>
        /// Instantiates FHIR protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesCanonical
        {
            get { return InstantiatesCanonicalElement != null ? InstantiatesCanonicalElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  InstantiatesCanonicalElement = null; 
                else
                  InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("InstantiatesCanonical");
            }
        }
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        [FhirElement("instantiatesUri", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> InstantiatesUriElement
        {
            get { if(_InstantiatesUriElement==null) _InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(); return _InstantiatesUriElement; }
            set { _InstantiatesUriElement = value; OnPropertyChanged("InstantiatesUriElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _InstantiatesUriElement;
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesUri
        {
            get { return InstantiatesUriElement != null ? InstantiatesUriElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  InstantiatesUriElement = null; 
                else
                  InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("InstantiatesUri");
            }
        }
        
        /// <summary>
        /// Request fulfilled by this communication
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Part of this action
        /// </summary>
        [FhirElement("partOf", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// Reply to
        /// </summary>
        [FhirElement("inResponseTo", Order=140)]
        [CLSCompliant(false)]
		[References("Communication")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> InResponseTo
        {
            get { if(_InResponseTo==null) _InResponseTo = new List<Hl7.Fhir.Model.ResourceReference>(); return _InResponseTo; }
            set { _InResponseTo = value; OnPropertyChanged("InResponseTo"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _InResponseTo;
        
        /// <summary>
        /// preparation | in-progress | not-done | suspended | aborted | completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.EventStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.EventStatus> _StatusElement;
        
        /// <summary>
        /// preparation | in-progress | not-done | suspended | aborted | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.EventStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.EventStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// Message category
        /// </summary>
        [FhirElement("category", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Message urgency
        /// </summary>
        [FhirElement("priority", InSummary=true, Order=180)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// Message urgency
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  PriorityElement = null; 
                else
                  PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// A channel of communication
        /// </summary>
        [FhirElement("medium", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Medium
        {
            get { if(_Medium==null) _Medium = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Medium; }
            set { _Medium = value; OnPropertyChanged("Medium"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Medium;
        
        /// <summary>
        /// Focus of message
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Description of the purpose/content
        /// </summary>
        [FhirElement("topic", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Topic
        {
            get { return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Topic;
        
        /// <summary>
        /// Resources that pertain to this communication
        /// </summary>
        [FhirElement("about", Order=220)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> About
        {
            get { if(_About==null) _About = new List<Hl7.Fhir.Model.ResourceReference>(); return _About; }
            set { _About = value; OnPropertyChanged("About"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _About;
        
        /// <summary>
        /// Encounter created as part of
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=230)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// When sent
        /// </summary>
        [FhirElement("sent", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime SentElement
        {
            get { return _SentElement; }
            set { _SentElement = value; OnPropertyChanged("SentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _SentElement;
        
        /// <summary>
        /// When sent
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Sent
        {
            get { return SentElement != null ? SentElement.Value : null; }
            set
            {
                if (value == null)
                  SentElement = null; 
                else
                  SentElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Sent");
            }
        }
        
        /// <summary>
        /// When received
        /// </summary>
        [FhirElement("received", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ReceivedElement
        {
            get { return _ReceivedElement; }
            set { _ReceivedElement = value; OnPropertyChanged("ReceivedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ReceivedElement;
        
        /// <summary>
        /// When received
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Received
        {
            get { return ReceivedElement != null ? ReceivedElement.Value : null; }
            set
            {
                if (value == null)
                  ReceivedElement = null; 
                else
                  ReceivedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Received");
            }
        }
        
        /// <summary>
        /// Message recipient
        /// </summary>
        [FhirElement("recipient", Order=260)]
        [CLSCompliant(false)]
		[References("Device","Organization","Patient","Practitioner","PractitionerRole","RelatedPerson","Group","CareTeam","HealthcareService")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Recipient
        {
            get { if(_Recipient==null) _Recipient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recipient; }
            set { _Recipient = value; OnPropertyChanged("Recipient"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Recipient;
        
        /// <summary>
        /// Message sender
        /// </summary>
        [FhirElement("sender", Order=270)]
        [CLSCompliant(false)]
		[References("Device","Organization","Patient","Practitioner","PractitionerRole","RelatedPerson","HealthcareService")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Sender
        {
            get { return _Sender; }
            set { _Sender = value; OnPropertyChanged("Sender"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Sender;
        
        /// <summary>
        /// Indication for message
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Why was communication done?
        /// </summary>
        [FhirElement("reasonReference", InSummary=true, Order=290)]
        [CLSCompliant(false)]
		[References("Condition","Observation","DiagnosticReport","DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Message payload
        /// </summary>
        [FhirElement("payload", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Communication.PayloadComponent> Payload
        {
            get { if(_Payload==null) _Payload = new List<Hl7.Fhir.Model.Communication.PayloadComponent>(); return _Payload; }
            set { _Payload = value; OnPropertyChanged("Payload"); }
        }
        
        private List<Hl7.Fhir.Model.Communication.PayloadComponent> _Payload;
        
        /// <summary>
        /// Comments made about the communication
        /// </summary>
        [FhirElement("note", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Communication;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(InstantiatesCanonicalElement != null) dest.InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(InstantiatesCanonicalElement.DeepCopy());
                if(InstantiatesUriElement != null) dest.InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(InstantiatesUriElement.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(InResponseTo != null) dest.InResponseTo = new List<Hl7.Fhir.Model.ResourceReference>(InResponseTo.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.EventStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(Medium != null) dest.Medium = new List<Hl7.Fhir.Model.CodeableConcept>(Medium.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Topic != null) dest.Topic = (Hl7.Fhir.Model.CodeableConcept)Topic.DeepCopy();
                if(About != null) dest.About = new List<Hl7.Fhir.Model.ResourceReference>(About.DeepCopy());
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(SentElement != null) dest.SentElement = (Hl7.Fhir.Model.FhirDateTime)SentElement.DeepCopy();
                if(ReceivedElement != null) dest.ReceivedElement = (Hl7.Fhir.Model.FhirDateTime)ReceivedElement.DeepCopy();
                if(Recipient != null) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
                if(Sender != null) dest.Sender = (Hl7.Fhir.Model.ResourceReference)Sender.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Payload != null) dest.Payload = new List<Hl7.Fhir.Model.Communication.PayloadComponent>(Payload.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Communication());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Communication;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.Matches(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(InResponseTo, otherT.InResponseTo)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(Medium, otherT.Medium)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(About, otherT.About)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(SentElement, otherT.SentElement)) return false;
            if( !DeepComparable.Matches(ReceivedElement, otherT.ReceivedElement)) return false;
            if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.Matches(Sender, otherT.Sender)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Payload, otherT.Payload)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Communication;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(InResponseTo, otherT.InResponseTo)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(Medium, otherT.Medium)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(About, otherT.About)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(SentElement, otherT.SentElement)) return false;
            if( !DeepComparable.IsExactly(ReceivedElement, otherT.ReceivedElement)) return false;
            if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.IsExactly(Sender, otherT.Sender)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Payload, otherT.Payload)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return elem; }
				foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				foreach (var elem in PartOf) { if (elem != null) yield return elem; }
				foreach (var elem in InResponseTo) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (StatusReason != null) yield return StatusReason;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (PriorityElement != null) yield return PriorityElement;
				foreach (var elem in Medium) { if (elem != null) yield return elem; }
				if (Subject != null) yield return Subject;
				if (Topic != null) yield return Topic;
				foreach (var elem in About) { if (elem != null) yield return elem; }
				if (Encounter != null) yield return Encounter;
				if (SentElement != null) yield return SentElement;
				if (ReceivedElement != null) yield return ReceivedElement;
				foreach (var elem in Recipient) { if (elem != null) yield return elem; }
				if (Sender != null) yield return Sender;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in Payload) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return new ElementValue("instantiatesCanonical", elem); }
                foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return new ElementValue("instantiatesUri", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                foreach (var elem in InResponseTo) { if (elem != null) yield return new ElementValue("inResponseTo", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                foreach (var elem in Medium) { if (elem != null) yield return new ElementValue("medium", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Topic != null) yield return new ElementValue("topic", Topic);
                foreach (var elem in About) { if (elem != null) yield return new ElementValue("about", elem); }
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (SentElement != null) yield return new ElementValue("sent", SentElement);
                if (ReceivedElement != null) yield return new ElementValue("received", ReceivedElement);
                foreach (var elem in Recipient) { if (elem != null) yield return new ElementValue("recipient", elem); }
                if (Sender != null) yield return new ElementValue("sender", Sender);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in Payload) { if (elem != null) yield return new ElementValue("payload", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }

    }
    
}
