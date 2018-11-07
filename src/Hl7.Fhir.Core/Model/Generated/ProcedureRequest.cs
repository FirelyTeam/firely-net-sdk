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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A request for a procedure or diagnostic to be performed
    /// </summary>
    [FhirType("ProcedureRequest", IsResource=true)]
    [DataContract]
    public partial class ProcedureRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ProcedureRequest; } }
        [NotMapped]
        public override string TypeName { get { return "ProcedureRequest"; } }
        
        [FhirType("RequesterComponent")]
        [DataContract]
        public partial class RequesterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RequesterComponent"; } }
            
            /// <summary>
            /// Individual making the request
            /// </summary>
            [FhirElement("agent", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("Device","Practitioner","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Agent
            {
                get { return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Agent;
            
            /// <summary>
            /// Organization agent is acting for
            /// </summary>
            [FhirElement("onBehalfOf", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequesterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Agent != null) dest.Agent = (Hl7.Fhir.Model.ResourceReference)Agent.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RequesterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Agent != null) yield return Agent;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Agent != null) yield return new ElementValue("agent", Agent);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }

            
        }
        
        
        /// <summary>
        /// Identifiers assigned to this order
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
        /// Protocol or definition
        /// </summary>
        [FhirElement("definition", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("ActivityDefinition","PlanDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Definition
        {
            get { if(_Definition==null) _Definition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Definition;
        
        /// <summary>
        /// What request fulfills
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=110)]
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
        /// What request replaces
        /// </summary>
        [FhirElement("replaces", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Replaces
        {
            get { if(_Replaces==null) _Replaces = new List<Hl7.Fhir.Model.ResourceReference>(); return _Replaces; }
            set { _Replaces = value; OnPropertyChanged("Replaces"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Replaces;
        
        /// <summary>
        /// Composite Request ID
        /// </summary>
        [FhirElement("requisition", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Requisition
        {
            get { return _Requisition; }
            set { _Requisition = value; OnPropertyChanged("Requisition"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Requisition;
        
        /// <summary>
        /// draft | active | suspended | completed | entered-in-error | cancelled
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | suspended | completed | entered-in-error | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.RequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// proposal | plan | order +
        /// </summary>
        [FhirElement("intent", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | order +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IntentElement = null; 
                else
                  IntentElement = new Code<Hl7.Fhir.Model.RequestIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", InSummary=true, Order=160)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | asap | stat
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
        /// True if procedure should not be performed
        /// </summary>
        [FhirElement("doNotPerform", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean DoNotPerformElement
        {
            get { return _DoNotPerformElement; }
            set { _DoNotPerformElement = value; OnPropertyChanged("DoNotPerformElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _DoNotPerformElement;
        
        /// <summary>
        /// True if procedure should not be performed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? DoNotPerform
        {
            get { return DoNotPerformElement != null ? DoNotPerformElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  DoNotPerformElement = null; 
                else
                  DoNotPerformElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("DoNotPerform");
            }
        }
        
        /// <summary>
        /// Classification of procedure
        /// </summary>
        [FhirElement("category", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// What is being requested/ordered
        /// </summary>
        [FhirElement("code", InSummary=true, Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Individual the service is ordered for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References("Patient","Group","Location","Device")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter or Episode during which request was created
        /// </summary>
        [FhirElement("context", InSummary=true, Order=210)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// When procedure should occur
        /// </summary>
        [FhirElement("occurrence", InSummary=true, Order=220, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// Preconditions for procedure or diagnostic
        /// </summary>
        [FhirElement("asNeeded", InSummary=true, Order=230, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element AsNeeded
        {
            get { return _AsNeeded; }
            set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
        }
        
        private Hl7.Fhir.Model.Element _AsNeeded;
        
        /// <summary>
        /// Date request signed
        /// </summary>
        [FhirElement("authoredOn", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredOnElement
        {
            get { return _AuthoredOnElement; }
            set { _AuthoredOnElement = value; OnPropertyChanged("AuthoredOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredOnElement;
        
        /// <summary>
        /// Date request signed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoredOn
        {
            get { return AuthoredOnElement != null ? AuthoredOnElement.Value : null; }
            set
            {
                if (value == null)
                  AuthoredOnElement = null; 
                else
                  AuthoredOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoredOn");
            }
        }
        
        /// <summary>
        /// Who/what is requesting procedure or diagnostic
        /// </summary>
        [FhirElement("requester", InSummary=true, Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.ProcedureRequest.RequesterComponent Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.ProcedureRequest.RequesterComponent _Requester;
        
        /// <summary>
        /// Performer role
        /// </summary>
        [FhirElement("performerType", InSummary=true, Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PerformerType
        {
            get { return _PerformerType; }
            set { _PerformerType = value; OnPropertyChanged("PerformerType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PerformerType;
        
        /// <summary>
        /// Requested perfomer
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=270)]
        [CLSCompliant(false)]
		[References("Practitioner","Organization","Patient","Device","RelatedPerson","HealthcareService")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// Explanation/Justification for test
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
        /// Explanation/Justification for test
        /// </summary>
        [FhirElement("reasonReference", InSummary=true, Order=290)]
        [CLSCompliant(false)]
		[References("Condition","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Additional clinical information
        /// </summary>
        [FhirElement("supportingInfo", Order=300)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInfo
        {
            get { if(_SupportingInfo==null) _SupportingInfo = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInfo; }
            set { _SupportingInfo = value; OnPropertyChanged("SupportingInfo"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInfo;
        
        /// <summary>
        /// Procedure Samples
        /// </summary>
        [FhirElement("specimen", InSummary=true, Order=310)]
        [CLSCompliant(false)]
		[References("Specimen")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Specimen
        {
            get { if(_Specimen==null) _Specimen = new List<Hl7.Fhir.Model.ResourceReference>(); return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Specimen;
        
        /// <summary>
        /// Location on Body
        /// </summary>
        [FhirElement("bodySite", InSummary=true, Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// Comments
        /// </summary>
        [FhirElement("note", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Request provenance
        /// </summary>
        [FhirElement("relevantHistory", Order=340)]
        [CLSCompliant(false)]
		[References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> RelevantHistory
        {
            get { if(_RelevantHistory==null) _RelevantHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _RelevantHistory; }
            set { _RelevantHistory = value; OnPropertyChanged("RelevantHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _RelevantHistory;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ProcedureRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Definition != null) dest.Definition = new List<Hl7.Fhir.Model.ResourceReference>(Definition.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Replaces != null) dest.Replaces = new List<Hl7.Fhir.Model.ResourceReference>(Replaces.DeepCopy());
                if(Requisition != null) dest.Requisition = (Hl7.Fhir.Model.Identifier)Requisition.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.RequestStatus>)StatusElement.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.RequestIntent>)IntentElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(DoNotPerformElement != null) dest.DoNotPerformElement = (Hl7.Fhir.Model.FhirBoolean)DoNotPerformElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.ProcedureRequest.RequesterComponent)Requester.DeepCopy();
                if(PerformerType != null) dest.PerformerType = (Hl7.Fhir.Model.CodeableConcept)PerformerType.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(SupportingInfo != null) dest.SupportingInfo = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInfo.DeepCopy());
                if(Specimen != null) dest.Specimen = new List<Hl7.Fhir.Model.ResourceReference>(Specimen.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(RelevantHistory != null) dest.RelevantHistory = new List<Hl7.Fhir.Model.ResourceReference>(RelevantHistory.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ProcedureRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ProcedureRequest;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Replaces, otherT.Replaces)) return false;
            if( !DeepComparable.Matches(Requisition, otherT.Requisition)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(SupportingInfo, otherT.SupportingInfo)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(RelevantHistory, otherT.RelevantHistory)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ProcedureRequest;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Replaces, otherT.Replaces)) return false;
            if( !DeepComparable.IsExactly(Requisition, otherT.Requisition)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(SupportingInfo, otherT.SupportingInfo)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(RelevantHistory, otherT.RelevantHistory)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in Definition) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				foreach (var elem in Replaces) { if (elem != null) yield return elem; }
				if (Requisition != null) yield return Requisition;
				if (StatusElement != null) yield return StatusElement;
				if (IntentElement != null) yield return IntentElement;
				if (PriorityElement != null) yield return PriorityElement;
				if (DoNotPerformElement != null) yield return DoNotPerformElement;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Occurrence != null) yield return Occurrence;
				if (AsNeeded != null) yield return AsNeeded;
				if (AuthoredOnElement != null) yield return AuthoredOnElement;
				if (Requester != null) yield return Requester;
				if (PerformerType != null) yield return PerformerType;
				if (Performer != null) yield return Performer;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in SupportingInfo) { if (elem != null) yield return elem; }
				foreach (var elem in Specimen) { if (elem != null) yield return elem; }
				foreach (var elem in BodySite) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in RelevantHistory) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Definition) { if (elem != null) yield return new ElementValue("definition", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in Replaces) { if (elem != null) yield return new ElementValue("replaces", elem); }
                if (Requisition != null) yield return new ElementValue("requisition", Requisition);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (IntentElement != null) yield return new ElementValue("intent", IntentElement);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (DoNotPerformElement != null) yield return new ElementValue("doNotPerform", DoNotPerformElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                if (AsNeeded != null) yield return new ElementValue("asNeeded", AsNeeded);
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (Requester != null) yield return new ElementValue("requester", Requester);
                if (PerformerType != null) yield return new ElementValue("performerType", PerformerType);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in SupportingInfo) { if (elem != null) yield return new ElementValue("supportingInfo", elem); }
                foreach (var elem in Specimen) { if (elem != null) yield return new ElementValue("specimen", elem); }
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in RelevantHistory) { if (elem != null) yield return new ElementValue("relevantHistory", elem); }
            }
        }

    }
    
}
