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
    /// Describes the intended objective(s) for a patient, group or organization
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Goal", IsResource=true)]
    [DataContract]
    public partial class Goal : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IGoal, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Goal; } }
        [NotMapped]
        public override string TypeName { get { return "Goal"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "OutcomeComponent")]
        [DataContract]
        public partial class OutcomeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OutcomeComponent"; } }
            
            /// <summary>
            /// Code or observation that resulted from goal
            /// </summary>
            [FhirElement("result", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Result
            {
                get { return _Result; }
                set { _Result = value; OnPropertyChanged("Result"); }
            }
            
            private Hl7.Fhir.Model.Element _Result;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OutcomeComponent");
                base.Serialize(sink);
                sink.Element("result", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Result?.Serialize(sink);
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
                    case "resultCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Result, "result");
                        Result = source.Populate(Result as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "resultReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Result, "result");
                        Result = source.Populate(Result as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OutcomeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Result != null) dest.Result = (Hl7.Fhir.Model.Element)Result.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OutcomeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OutcomeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Result, otherT.Result)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OutcomeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Result, otherT.Result)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Result != null) yield return Result;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Result != null) yield return new ElementValue("result", Result);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// External Ids for this goal
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who this goal is intended for
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("Patient","Group","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// When goal pursuit begins
        /// </summary>
        [FhirElement("start", InSummary=Hl7.Fhir.Model.Version.All, Order=110, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element Start
        {
            get { return _Start; }
            set { _Start = value; OnPropertyChanged("Start"); }
        }
        
        private Hl7.Fhir.Model.Element _Start;
        
        /// <summary>
        /// Reach goal on or before
        /// </summary>
        [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=120, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.DSTU2.Duration))]
        [DataMember]
        public Hl7.Fhir.Model.Element Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.Element _Target;
        
        /// <summary>
        /// E.g. Treatment, dietary, behavioral, etc.
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// What's the desired outcome?
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// What's the desired outcome?
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
        /// proposed | planned | accepted | rejected | in-progress | achieved | sustaining | on-hold | cancelled
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.GoalStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.GoalStatus> _StatusElement;
        
        /// <summary>
        /// proposed | planned | accepted | rejected | in-progress | achieved | sustaining | on-hold | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.GoalStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.GoalStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// When goal status took effect
        /// </summary>
        [FhirElement("statusDate", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date StatusDateElement
        {
            get { return _StatusDateElement; }
            set { _StatusDateElement = value; OnPropertyChanged("StatusDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _StatusDateElement;
        
        /// <summary>
        /// When goal status took effect
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string StatusDate
        {
            get { return StatusDateElement != null ? StatusDateElement.Value : null; }
            set
            {
                if (value == null)
                    StatusDateElement = null;
                else
                    StatusDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("StatusDate");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// Who's responsible for creating Goal?
        /// </summary>
        [FhirElement("author", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// high | medium |low
        /// </summary>
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// Issues addressed by this goal
        /// </summary>
        [FhirElement("addresses", Order=200)]
        [CLSCompliant(false)]
        [References("Condition","Observation","MedicationStatement","NutritionOrder","ProcedureRequest","RiskAssessment")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Addresses
        {
            get { if(_Addresses==null) _Addresses = new List<Hl7.Fhir.Model.ResourceReference>(); return _Addresses; }
            set { _Addresses = value; OnPropertyChanged("Addresses"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Addresses;
        
        /// <summary>
        /// Comments about the goal
        /// </summary>
        [FhirElement("note", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// What was end result of goal?
        /// </summary>
        [FhirElement("outcome", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<OutcomeComponent> Outcome
        {
            get { if(_Outcome==null) _Outcome = new List<OutcomeComponent>(); return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private List<OutcomeComponent> _Outcome;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Goal;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Start != null) dest.Start = (Hl7.Fhir.Model.Element)Start.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.Element)Target.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.GoalStatus>)StatusElement.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.Date)StatusDateElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Addresses != null) dest.Addresses = new List<Hl7.Fhir.Model.ResourceReference>(Addresses.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Outcome != null) dest.Outcome = new List<OutcomeComponent>(Outcome.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Goal());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Goal;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Start, otherT.Start)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Goal;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Start, otherT.Start)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Goal");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Start?.Serialize(sink);
            sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Target?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DescriptionElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("statusDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusDateElement?.Serialize(sink);
            sink.Element("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusReason?.Serialize(sink);
            sink.Element("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Author?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Priority?.Serialize(sink);
            sink.BeginList("addresses", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Addresses)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Outcome)
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "startDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Start, "start");
                    Start = source.PopulateValue(Start as Hl7.Fhir.Model.Date);
                    return true;
                case "_startDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Start, "start");
                    Start = source.Populate(Start as Hl7.Fhir.Model.Date);
                    return true;
                case "startCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Start, "start");
                    Start = source.Populate(Start as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "targetDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Target, "target");
                    Target = source.PopulateValue(Target as Hl7.Fhir.Model.Date);
                    return true;
                case "_targetDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Target, "target");
                    Target = source.Populate(Target as Hl7.Fhir.Model.Date);
                    return true;
                case "targetQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Duration>(Target, "target");
                    Target = source.Populate(Target as Hl7.Fhir.Model.DSTU2.Duration);
                    return true;
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusDate":
                    StatusDateElement = source.PopulateValue(StatusDateElement);
                    return true;
                case "_statusDate":
                    StatusDateElement = source.Populate(StatusDateElement);
                    return true;
                case "statusReason":
                    StatusReason = source.Populate(StatusReason);
                    return true;
                case "author":
                    Author = source.Populate(Author);
                    return true;
                case "priority":
                    Priority = source.Populate(Priority);
                    return true;
                case "addresses":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "outcome":
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
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "addresses":
                    source.PopulateListItem(Addresses, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "outcome":
                    source.PopulateListItem(Outcome, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (Subject != null) yield return Subject;
                if (Start != null) yield return Start;
                if (Target != null) yield return Target;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                if (StatusElement != null) yield return StatusElement;
                if (StatusDateElement != null) yield return StatusDateElement;
                if (StatusReason != null) yield return StatusReason;
                if (Author != null) yield return Author;
                if (Priority != null) yield return Priority;
                foreach (var elem in Addresses) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Outcome) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Start != null) yield return new ElementValue("start", Start);
                if (Target != null) yield return new ElementValue("target", Target);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusDateElement != null) yield return new ElementValue("statusDate", StatusDateElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                if (Author != null) yield return new ElementValue("author", Author);
                if (Priority != null) yield return new ElementValue("priority", Priority);
                foreach (var elem in Addresses) { if (elem != null) yield return new ElementValue("addresses", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Outcome) { if (elem != null) yield return new ElementValue("outcome", elem); }
            }
        }
    
    }

}
