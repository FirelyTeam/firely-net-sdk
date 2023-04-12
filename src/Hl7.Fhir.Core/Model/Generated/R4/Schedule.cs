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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// A container for slots of time that may be available for booking appointments
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Schedule", IsResource=true)]
    [DataContract]
    public partial class Schedule : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISchedule, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Schedule; } }
        [NotMapped]
        public override string TypeName { get { return "Schedule"; } }
    
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
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
        /// Whether this schedule is in active use
        /// </summary>
        [FhirElement("active", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveElement
        {
            get { return _ActiveElement; }
            set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ActiveElement;
        
        /// <summary>
        /// Whether this schedule is in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Active
        {
            get { return ActiveElement != null ? ActiveElement.Value : null; }
            set
            {
                if (value == null)
                    ActiveElement = null;
                else
                    ActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Active");
            }
        }
        
        /// <summary>
        /// High-level category
        /// </summary>
        [FhirElement("serviceCategory", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceCategory
        {
            get { if(_ServiceCategory==null) _ServiceCategory = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceCategory; }
            set { _ServiceCategory = value; OnPropertyChanged("ServiceCategory"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceCategory;
        
        /// <summary>
        /// Specific service
        /// </summary>
        [FhirElement("serviceType", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceType
        {
            get { if(_ServiceType==null) _ServiceType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceType; }
            set { _ServiceType = value; OnPropertyChanged("ServiceType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceType;
        
        /// <summary>
        /// Type of specialty needed
        /// </summary>
        [FhirElement("specialty", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Specialty
        {
            get { if(_Specialty==null) _Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Specialty; }
            set { _Specialty = value; OnPropertyChanged("Specialty"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
        
        /// <summary>
        /// Resource(s) that availability information is being provided for
        /// </summary>
        [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","PractitionerRole","RelatedPerson","Device","HealthcareService","Location")]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Actor
        {
            get { if(_Actor==null) _Actor = new List<Hl7.Fhir.Model.ResourceReference>(); return _Actor; }
            set { _Actor = value; OnPropertyChanged("Actor"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Actor;
        
        /// <summary>
        /// Period of time covered by schedule
        /// </summary>
        [FhirElement("planningHorizon", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period PlanningHorizon
        {
            get { return _PlanningHorizon; }
            set { _PlanningHorizon = value; OnPropertyChanged("PlanningHorizon"); }
        }
        
        private Hl7.Fhir.Model.Period _PlanningHorizon;
        
        /// <summary>
        /// Comments on availability
        /// </summary>
        [FhirElement("comment", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Comments on availability
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                    CommentElement = null;
                else
                    CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Schedule;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
                if(ServiceCategory != null) dest.ServiceCategory = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceCategory.DeepCopy());
                if(ServiceType != null) dest.ServiceType = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceType.DeepCopy());
                if(Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
                if(Actor != null) dest.Actor = new List<Hl7.Fhir.Model.ResourceReference>(Actor.DeepCopy());
                if(PlanningHorizon != null) dest.PlanningHorizon = (Hl7.Fhir.Model.Period)PlanningHorizon.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Schedule());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Schedule;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
            if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            if( !DeepComparable.Matches(PlanningHorizon, otherT.PlanningHorizon)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Schedule;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
            if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            if( !DeepComparable.IsExactly(PlanningHorizon, otherT.PlanningHorizon)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Schedule");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("active", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActiveElement?.Serialize(sink);
            sink.BeginList("serviceCategory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ServiceCategory)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("serviceType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ServiceType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("specialty", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Specialty)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Actor)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("planningHorizon", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PlanningHorizon?.Serialize(sink);
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentElement?.Serialize(sink);
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
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "active":
                    ActiveElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "serviceCategory":
                    ServiceCategory = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "serviceType":
                    ServiceType = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "specialty":
                    Specialty = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "actor":
                    Actor = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "planningHorizon":
                    PlanningHorizon = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "comment":
                    CommentElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "active":
                    ActiveElement = source.PopulateValue(ActiveElement);
                    return true;
                case "_active":
                    ActiveElement = source.Populate(ActiveElement);
                    return true;
                case "serviceCategory":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "serviceType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "specialty":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "actor":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "planningHorizon":
                    PlanningHorizon = source.Populate(PlanningHorizon);
                    return true;
                case "comment":
                    CommentElement = source.PopulateValue(CommentElement);
                    return true;
                case "_comment":
                    CommentElement = source.Populate(CommentElement);
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
                case "serviceCategory":
                    source.PopulateListItem(ServiceCategory, index);
                    return true;
                case "serviceType":
                    source.PopulateListItem(ServiceType, index);
                    return true;
                case "specialty":
                    source.PopulateListItem(Specialty, index);
                    return true;
                case "actor":
                    source.PopulateListItem(Actor, index);
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
                if (ActiveElement != null) yield return ActiveElement;
                foreach (var elem in ServiceCategory) { if (elem != null) yield return elem; }
                foreach (var elem in ServiceType) { if (elem != null) yield return elem; }
                foreach (var elem in Specialty) { if (elem != null) yield return elem; }
                foreach (var elem in Actor) { if (elem != null) yield return elem; }
                if (PlanningHorizon != null) yield return PlanningHorizon;
                if (CommentElement != null) yield return CommentElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ActiveElement != null) yield return new ElementValue("active", ActiveElement);
                foreach (var elem in ServiceCategory) { if (elem != null) yield return new ElementValue("serviceCategory", elem); }
                foreach (var elem in ServiceType) { if (elem != null) yield return new ElementValue("serviceType", elem); }
                foreach (var elem in Specialty) { if (elem != null) yield return new ElementValue("specialty", elem); }
                foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                if (PlanningHorizon != null) yield return new ElementValue("planningHorizon", PlanningHorizon);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
            }
        }
    
    }

}
