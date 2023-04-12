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
    /// Physical entity which is the primary unit of interest in the study
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ResearchSubject", IsResource=true)]
    [DataContract]
    public partial class ResearchSubject : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IResearchSubject, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ResearchSubject; } }
        [NotMapped]
        public override string TypeName { get { return "ResearchSubject"; } }
    
        
        /// <summary>
        /// Business Identifier for research subject in a study
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
        /// candidate | eligible | follow-up | ineligible | not-registered | off-study | on-study | on-study-intervention | on-study-observation | pending-on-study | potential-candidate | screening | withdrawn
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.ResearchSubjectStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.ResearchSubjectStatus> _StatusElement;
        
        /// <summary>
        /// candidate | eligible | follow-up | ineligible | not-registered | off-study | on-study | on-study-intervention | on-study-observation | pending-on-study | potential-candidate | screening | withdrawn
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.ResearchSubjectStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.ResearchSubjectStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Start and end of participation
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Study subject is part of
        /// </summary>
        [FhirElement("study", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("ResearchStudy")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Study
        {
            get { return _Study; }
            set { _Study = value; OnPropertyChanged("Study"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Study;
        
        /// <summary>
        /// Who is part of study
        /// </summary>
        [FhirElement("individual", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Individual
        {
            get { return _Individual; }
            set { _Individual = value; OnPropertyChanged("Individual"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Individual;
        
        /// <summary>
        /// What path should be followed
        /// </summary>
        [FhirElement("assignedArm", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AssignedArmElement
        {
            get { return _AssignedArmElement; }
            set { _AssignedArmElement = value; OnPropertyChanged("AssignedArmElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AssignedArmElement;
        
        /// <summary>
        /// What path should be followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AssignedArm
        {
            get { return AssignedArmElement != null ? AssignedArmElement.Value : null; }
            set
            {
                if (value == null)
                    AssignedArmElement = null;
                else
                    AssignedArmElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AssignedArm");
            }
        }
        
        /// <summary>
        /// What path was followed
        /// </summary>
        [FhirElement("actualArm", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ActualArmElement
        {
            get { return _ActualArmElement; }
            set { _ActualArmElement = value; OnPropertyChanged("ActualArmElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ActualArmElement;
        
        /// <summary>
        /// What path was followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ActualArm
        {
            get { return ActualArmElement != null ? ActualArmElement.Value : null; }
            set
            {
                if (value == null)
                    ActualArmElement = null;
                else
                    ActualArmElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ActualArm");
            }
        }
        
        /// <summary>
        /// Agreement to participate in study
        /// </summary>
        [FhirElement("consent", Order=160)]
        [CLSCompliant(false)]
        [References("Consent")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Consent
        {
            get { return _Consent; }
            set { _Consent = value; OnPropertyChanged("Consent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Consent;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ResearchSubject;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.ResearchSubjectStatus>)StatusElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Study != null) dest.Study = (Hl7.Fhir.Model.ResourceReference)Study.DeepCopy();
                if(Individual != null) dest.Individual = (Hl7.Fhir.Model.ResourceReference)Individual.DeepCopy();
                if(AssignedArmElement != null) dest.AssignedArmElement = (Hl7.Fhir.Model.FhirString)AssignedArmElement.DeepCopy();
                if(ActualArmElement != null) dest.ActualArmElement = (Hl7.Fhir.Model.FhirString)ActualArmElement.DeepCopy();
                if(Consent != null) dest.Consent = (Hl7.Fhir.Model.ResourceReference)Consent.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ResearchSubject());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ResearchSubject;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Study, otherT.Study)) return false;
            if( !DeepComparable.Matches(Individual, otherT.Individual)) return false;
            if( !DeepComparable.Matches(AssignedArmElement, otherT.AssignedArmElement)) return false;
            if( !DeepComparable.Matches(ActualArmElement, otherT.ActualArmElement)) return false;
            if( !DeepComparable.Matches(Consent, otherT.Consent)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ResearchSubject;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Study, otherT.Study)) return false;
            if( !DeepComparable.IsExactly(Individual, otherT.Individual)) return false;
            if( !DeepComparable.IsExactly(AssignedArmElement, otherT.AssignedArmElement)) return false;
            if( !DeepComparable.IsExactly(ActualArmElement, otherT.ActualArmElement)) return false;
            if( !DeepComparable.IsExactly(Consent, otherT.Consent)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ResearchSubject");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.Element("study", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Study?.Serialize(sink);
            sink.Element("individual", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Individual?.Serialize(sink);
            sink.Element("assignedArm", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AssignedArmElement?.Serialize(sink);
            sink.Element("actualArm", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ActualArmElement?.Serialize(sink);
            sink.Element("consent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Consent?.Serialize(sink);
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
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ResearchSubjectStatus>>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "study":
                    Study = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "individual":
                    Individual = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "assignedArm":
                    AssignedArmElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "actualArm":
                    ActualArmElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "consent":
                    Consent = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "study":
                    Study = source.Populate(Study);
                    return true;
                case "individual":
                    Individual = source.Populate(Individual);
                    return true;
                case "assignedArm":
                    AssignedArmElement = source.PopulateValue(AssignedArmElement);
                    return true;
                case "_assignedArm":
                    AssignedArmElement = source.Populate(AssignedArmElement);
                    return true;
                case "actualArm":
                    ActualArmElement = source.PopulateValue(ActualArmElement);
                    return true;
                case "_actualArm":
                    ActualArmElement = source.Populate(ActualArmElement);
                    return true;
                case "consent":
                    Consent = source.Populate(Consent);
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
                if (StatusElement != null) yield return StatusElement;
                if (Period != null) yield return Period;
                if (Study != null) yield return Study;
                if (Individual != null) yield return Individual;
                if (AssignedArmElement != null) yield return AssignedArmElement;
                if (ActualArmElement != null) yield return ActualArmElement;
                if (Consent != null) yield return Consent;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Period != null) yield return new ElementValue("period", Period);
                if (Study != null) yield return new ElementValue("study", Study);
                if (Individual != null) yield return new ElementValue("individual", Individual);
                if (AssignedArmElement != null) yield return new ElementValue("assignedArm", AssignedArmElement);
                if (ActualArmElement != null) yield return new ElementValue("actualArm", ActualArmElement);
                if (Consent != null) yield return new ElementValue("consent", Consent);
            }
        }
    
    }

}
