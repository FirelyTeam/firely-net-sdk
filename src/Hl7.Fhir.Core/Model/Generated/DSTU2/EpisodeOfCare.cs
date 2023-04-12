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
    /// An association of a Patient with an Organization and  Healthcare Provider(s) for a period of time that the Organization assumes some level of responsibility
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "EpisodeOfCare", IsResource=true)]
    [DataContract]
    public partial class EpisodeOfCare : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IEpisodeOfCare, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.EpisodeOfCare; } }
        [NotMapped]
        public override string TypeName { get { return "EpisodeOfCare"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "StatusHistoryComponent")]
        [DataContract]
        public partial class StatusHistoryComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IEpisodeOfCareStatusHistoryComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StatusHistoryComponent"; } }
            
            /// <summary>
            /// planned | waitlist | active | onhold | finished | cancelled
            /// </summary>
            [FhirElement("status", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus> _StatusElement;
            
            /// <summary>
            /// planned | waitlist | active | onhold | finished | cancelled
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (value == null)
                        StatusElement = null;
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Period for the status
            /// </summary>
            [FhirElement("period", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StatusHistoryComponent");
                base.Serialize(sink);
                sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); StatusElement?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Period?.Serialize(sink);
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
                    case "status":
                        StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus>>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
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
                    case "status":
                        StatusElement = source.PopulateValue(StatusElement);
                        return true;
                    case "_status":
                        StatusElement = source.Populate(StatusElement);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StatusHistoryComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus>)StatusElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StatusHistoryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StatusHistoryComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StatusHistoryComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (StatusElement != null) yield return StatusElement;
                    if (Period != null) yield return Period;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "CareTeamComponent")]
        [DataContract]
        public partial class CareTeamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CareTeamComponent"; } }
            
            /// <summary>
            /// Role taken by this team member
            /// </summary>
            [FhirElement("role", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { if(_Role==null) _Role = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
            
            /// <summary>
            /// Period of time for this role
            /// </summary>
            [FhirElement("period", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// The practitioner (or Organization) within the team
            /// </summary>
            [FhirElement("member", Order=60)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Member
            {
                get { return _Member; }
                set { _Member = value; OnPropertyChanged("Member"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Member;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CareTeamComponent");
                base.Serialize(sink);
                sink.BeginList("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Role)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
                sink.Element("member", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Member?.Serialize(sink);
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
                        Role = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "member":
                        Member = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                    case "member":
                        Member = source.Populate(Member);
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
                var dest = other as CareTeamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Member != null) dest.Member = (Hl7.Fhir.Model.ResourceReference)Member.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CareTeamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Member, otherT.Member)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Role) { if (elem != null) yield return elem; }
                    if (Period != null) yield return Period;
                    if (Member != null) yield return Member;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
                    if (Period != null) yield return new ElementValue("period", Period);
                    if (Member != null) yield return new ElementValue("member", Member);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IEpisodeOfCareStatusHistoryComponent> Hl7.Fhir.Model.IEpisodeOfCare.StatusHistory { get { return StatusHistory; } }
    
        
        /// <summary>
        /// Identifier(s) for the EpisodeOfCare
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
        /// planned | waitlist | active | onhold | finished | cancelled
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus> _StatusElement;
        
        /// <summary>
        /// planned | waitlist | active | onhold | finished | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Past list of status codes
        /// </summary>
        [FhirElement("statusHistory", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<StatusHistoryComponent> StatusHistory
        {
            get { if(_StatusHistory==null) _StatusHistory = new List<StatusHistoryComponent>(); return _StatusHistory; }
            set { _StatusHistory = value; OnPropertyChanged("StatusHistory"); }
        }
        
        private List<StatusHistoryComponent> _StatusHistory;
        
        /// <summary>
        /// Type/class  - e.g. specialist referral, disease management
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// Conditions/problems/diagnoses this episode of care is for
        /// </summary>
        [FhirElement("condition", Order=130)]
        [CLSCompliant(false)]
        [References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Condition
        {
            get { if(_Condition==null) _Condition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Condition;
        
        /// <summary>
        /// Patient for this episode of care
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Organization that assumes care
        /// </summary>
        [FhirElement("managingOrganization", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization
        {
            get { return _ManagingOrganization; }
            set { _ManagingOrganization = value; OnPropertyChanged("ManagingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ManagingOrganization;
        
        /// <summary>
        /// Interval during responsibility is assumed
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Originating Referral Request(s)
        /// </summary>
        [FhirElement("referralRequest", Order=170)]
        [CLSCompliant(false)]
        [References("ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReferralRequest
        {
            get { if(_ReferralRequest==null) _ReferralRequest = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReferralRequest; }
            set { _ReferralRequest = value; OnPropertyChanged("ReferralRequest"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReferralRequest;
        
        /// <summary>
        /// Care manager/care co-ordinator for the patient
        /// </summary>
        [FhirElement("careManager", Order=180)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference CareManager
        {
            get { return _CareManager; }
            set { _CareManager = value; OnPropertyChanged("CareManager"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _CareManager;
        
        /// <summary>
        /// Other practitioners facilitating this episode of care
        /// </summary>
        [FhirElement("careTeam", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CareTeamComponent> CareTeam
        {
            get { if(_CareTeam==null) _CareTeam = new List<CareTeamComponent>(); return _CareTeam; }
            set { _CareTeam = value; OnPropertyChanged("CareTeam"); }
        }
        
        private List<CareTeamComponent> _CareTeam;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as EpisodeOfCare;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus>)StatusElement.DeepCopy();
                if(StatusHistory != null) dest.StatusHistory = new List<StatusHistoryComponent>(StatusHistory.DeepCopy());
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(Condition != null) dest.Condition = new List<Hl7.Fhir.Model.ResourceReference>(Condition.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(ReferralRequest != null) dest.ReferralRequest = new List<Hl7.Fhir.Model.ResourceReference>(ReferralRequest.DeepCopy());
                if(CareManager != null) dest.CareManager = (Hl7.Fhir.Model.ResourceReference)CareManager.DeepCopy();
                if(CareTeam != null) dest.CareTeam = new List<CareTeamComponent>(CareTeam.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new EpisodeOfCare());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as EpisodeOfCare;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusHistory, otherT.StatusHistory)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(ReferralRequest, otherT.ReferralRequest)) return false;
            if( !DeepComparable.Matches(CareManager, otherT.CareManager)) return false;
            if( !DeepComparable.Matches(CareTeam, otherT.CareTeam)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as EpisodeOfCare;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusHistory, otherT.StatusHistory)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(ReferralRequest, otherT.ReferralRequest)) return false;
            if( !DeepComparable.IsExactly(CareManager, otherT.CareManager)) return false;
            if( !DeepComparable.IsExactly(CareTeam, otherT.CareTeam)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("EpisodeOfCare");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.BeginList("statusHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in StatusHistory)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Type)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Condition)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("managingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ManagingOrganization?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.BeginList("referralRequest", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReferralRequest)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("careManager", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CareManager?.Serialize(sink);
            sink.BeginList("careTeam", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in CareTeam)
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
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.EpisodeOfCareStatus>>();
                    return true;
                case "statusHistory":
                    StatusHistory = source.GetList<StatusHistoryComponent>();
                    return true;
                case "type":
                    Type = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "condition":
                    Condition = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "managingOrganization":
                    ManagingOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "referralRequest":
                    ReferralRequest = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "careManager":
                    CareManager = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "careTeam":
                    CareTeam = source.GetList<CareTeamComponent>();
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
                case "statusHistory":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "type":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "condition":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "managingOrganization":
                    ManagingOrganization = source.Populate(ManagingOrganization);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "referralRequest":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "careManager":
                    CareManager = source.Populate(CareManager);
                    return true;
                case "careTeam":
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
                case "statusHistory":
                    source.PopulateListItem(StatusHistory, index);
                    return true;
                case "type":
                    source.PopulateListItem(Type, index);
                    return true;
                case "condition":
                    source.PopulateListItem(Condition, index);
                    return true;
                case "referralRequest":
                    source.PopulateListItem(ReferralRequest, index);
                    return true;
                case "careTeam":
                    source.PopulateListItem(CareTeam, index);
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
                foreach (var elem in StatusHistory) { if (elem != null) yield return elem; }
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                foreach (var elem in Condition) { if (elem != null) yield return elem; }
                if (Patient != null) yield return Patient;
                if (ManagingOrganization != null) yield return ManagingOrganization;
                if (Period != null) yield return Period;
                foreach (var elem in ReferralRequest) { if (elem != null) yield return elem; }
                if (CareManager != null) yield return CareManager;
                foreach (var elem in CareTeam) { if (elem != null) yield return elem; }
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
                foreach (var elem in StatusHistory) { if (elem != null) yield return new ElementValue("statusHistory", elem); }
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                foreach (var elem in Condition) { if (elem != null) yield return new ElementValue("condition", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                if (Period != null) yield return new ElementValue("period", Period);
                foreach (var elem in ReferralRequest) { if (elem != null) yield return new ElementValue("referralRequest", elem); }
                if (CareManager != null) yield return new ElementValue("careManager", CareManager);
                foreach (var elem in CareTeam) { if (elem != null) yield return new ElementValue("careTeam", elem); }
            }
        }
    
    }

}
