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
    /// Healthcare plan for patient or group
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "CarePlan", IsResource=true)]
    [DataContract]
    public partial class CarePlan : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ICarePlan, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CarePlan; } }
        [NotMapped]
        public override string TypeName { get { return "CarePlan"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "RelatedPlanComponent")]
        [DataContract]
        public partial class RelatedPlanComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedPlanComponent"; } }
            
            /// <summary>
            /// includes | replaces | fulfills
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.CarePlanRelationship> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.CarePlanRelationship> _CodeElement;
            
            /// <summary>
            /// includes | replaces | fulfills
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.CarePlanRelationship? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.DSTU2.CarePlanRelationship>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Plan relationship exists with
            /// </summary>
            [FhirElement("plan", Order=50)]
            [CLSCompliant(false)]
            [References("CarePlan")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Plan
            {
                get { return _Plan; }
                set { _Plan = value; OnPropertyChanged("Plan"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Plan;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RelatedPlanComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
                sink.Element("plan", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Plan?.Serialize(sink);
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
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.CarePlanRelationship>>();
                        return true;
                    case "plan":
                        Plan = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "plan":
                        Plan = source.Populate(Plan);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedPlanComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.DSTU2.CarePlanRelationship>)CodeElement.DeepCopy();
                    if(Plan != null) dest.Plan = (Hl7.Fhir.Model.ResourceReference)Plan.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RelatedPlanComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedPlanComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Plan, otherT.Plan)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedPlanComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Plan, otherT.Plan)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Plan != null) yield return Plan;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Plan != null) yield return new ElementValue("plan", Plan);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// Type of involvement
            /// </summary>
            [FhirElement("role", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Who is involved
            /// </summary>
            [FhirElement("member", Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","RelatedPerson","Patient","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Member
            {
                get { return _Member; }
                set { _Member = value; OnPropertyChanged("Member"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Member;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParticipantComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Role?.Serialize(sink);
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
                        Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                        Role = source.Populate(Role);
                        return true;
                    case "member":
                        Member = source.Populate(Member);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Member != null) dest.Member = (Hl7.Fhir.Model.ResourceReference)Member.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Member, otherT.Member)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Role != null) yield return Role;
                    if (Member != null) yield return Member;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Member != null) yield return new ElementValue("member", Member);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ActivityComponent")]
        [DataContract]
        public partial class ActivityComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICarePlanActivityComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActivityComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.ICarePlanDetailComponent Hl7.Fhir.Model.ICarePlanActivityComponent.Detail { get { return Detail; } }
            
            /// <summary>
            /// Appointments, orders, etc.
            /// </summary>
            [FhirElement("actionResulting", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ActionResulting
            {
                get { if(_ActionResulting==null) _ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(); return _ActionResulting; }
                set { _ActionResulting = value; OnPropertyChanged("ActionResulting"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ActionResulting;
            
            /// <summary>
            /// Comments about the activity status/progress
            /// </summary>
            [FhirElement("progress", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Progress
            {
                get { if(_Progress==null) _Progress = new List<Hl7.Fhir.Model.Annotation>(); return _Progress; }
                set { _Progress = value; OnPropertyChanged("Progress"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Progress;
            
            /// <summary>
            /// Activity details defined in specific resource
            /// </summary>
            [FhirElement("reference", Order=60)]
            [CLSCompliant(false)]
            [References("Appointment","CommunicationRequest","DeviceUseRequest","DiagnosticOrder","MedicationOrder","NutritionOrder","Order","ProcedureRequest","ProcessRequest","ReferralRequest","SupplyRequest","VisionPrescription")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// In-line definition of activity
            /// </summary>
            [FhirElement("detail", Order=70)]
            [DataMember]
            public DetailComponent Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private DetailComponent _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActivityComponent");
                base.Serialize(sink);
                sink.BeginList("actionResulting", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ActionResulting)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("progress", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Progress)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reference?.Serialize(sink);
                sink.Element("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Detail?.Serialize(sink);
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
                    case "actionResulting":
                        ActionResulting = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "progress":
                        Progress = source.GetList<Hl7.Fhir.Model.Annotation>();
                        return true;
                    case "reference":
                        Reference = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "detail":
                        Detail = source.Get<DetailComponent>();
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
                    case "actionResulting":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "progress":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "reference":
                        Reference = source.Populate(Reference);
                        return true;
                    case "detail":
                        Detail = source.Populate(Detail);
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
                    case "actionResulting":
                        source.PopulateListItem(ActionResulting, index);
                        return true;
                    case "progress":
                        source.PopulateListItem(Progress, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActivityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActionResulting != null) dest.ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(ActionResulting.DeepCopy());
                    if(Progress != null) dest.Progress = new List<Hl7.Fhir.Model.Annotation>(Progress.DeepCopy());
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(Detail != null) dest.Detail = (DetailComponent)Detail.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActivityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActivityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ActionResulting, otherT.ActionResulting)) return false;
                if( !DeepComparable.Matches(Progress, otherT.Progress)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActivityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActionResulting, otherT.ActionResulting)) return false;
                if( !DeepComparable.IsExactly(Progress, otherT.Progress)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in ActionResulting) { if (elem != null) yield return elem; }
                    foreach (var elem in Progress) { if (elem != null) yield return elem; }
                    if (Reference != null) yield return Reference;
                    if (Detail != null) yield return Detail;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in ActionResulting) { if (elem != null) yield return new ElementValue("actionResulting", elem); }
                    foreach (var elem in Progress) { if (elem != null) yield return new ElementValue("progress", elem); }
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    if (Detail != null) yield return new ElementValue("detail", Detail);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICarePlanDetailComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            [FhirElement("category", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Detail type of activity
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Why activity should be done
            /// </summary>
            [FhirElement("reasonCode", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
            {
                get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
                set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
            
            /// <summary>
            /// Condition triggering need for activity
            /// </summary>
            [FhirElement("reasonReference", Order=70)]
            [CLSCompliant(false)]
            [References("Condition")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
            {
                get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
                set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
            
            /// <summary>
            /// Goals this activity relates to
            /// </summary>
            [FhirElement("goal", Order=80)]
            [CLSCompliant(false)]
            [References("Goal")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Goal
            {
                get { if(_Goal==null) _Goal = new List<Hl7.Fhir.Model.ResourceReference>(); return _Goal; }
                set { _Goal = value; OnPropertyChanged("Goal"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Goal;
            
            /// <summary>
            /// not-started | scheduled | in-progress | on-hold | completed | cancelled
            /// </summary>
            [FhirElement("status", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.CarePlanActivityStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.CarePlanActivityStatus> _StatusElement;
            
            /// <summary>
            /// not-started | scheduled | in-progress | on-hold | completed | cancelled
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.CarePlanActivityStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (value == null)
                        StatusElement = null;
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.DSTU2.CarePlanActivityStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Reason for current status
            /// </summary>
            [FhirElement("statusReason", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept StatusReason
            {
                get { return _StatusReason; }
                set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _StatusReason;
            
            /// <summary>
            /// Do NOT do
            /// </summary>
            [FhirElement("prohibited", Order=110)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ProhibitedElement
            {
                get { return _ProhibitedElement; }
                set { _ProhibitedElement = value; OnPropertyChanged("ProhibitedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ProhibitedElement;
            
            /// <summary>
            /// Do NOT do
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Prohibited
            {
                get { return ProhibitedElement != null ? ProhibitedElement.Value : null; }
                set
                {
                    if (value == null)
                        ProhibitedElement = null;
                    else
                        ProhibitedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Prohibited");
                }
            }
            
            /// <summary>
            /// When activity is to occur
            /// </summary>
            [FhirElement("scheduled", Order=120, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Scheduled
            {
                get { return _Scheduled; }
                set { _Scheduled = value; OnPropertyChanged("Scheduled"); }
            }
            
            private Hl7.Fhir.Model.Element _Scheduled;
            
            /// <summary>
            /// Where it should happen
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
            /// Who will be responsible?
            /// </summary>
            [FhirElement("performer", Order=140)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","RelatedPerson","Patient")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Performer
            {
                get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Performer;
            
            /// <summary>
            /// What is to be administered/supplied
            /// </summary>
            [FhirElement("product", Order=150, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Product
            {
                get { return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            
            private Hl7.Fhir.Model.Element _Product;
            
            /// <summary>
            /// How to consume/day?
            /// </summary>
            [FhirElement("dailyAmount", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity DailyAmount
            {
                get { return _DailyAmount; }
                set { _DailyAmount = value; OnPropertyChanged("DailyAmount"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _DailyAmount;
            
            /// <summary>
            /// How much to administer/supply/consume
            /// </summary>
            [FhirElement("quantity", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Extra info describing activity to perform
            /// </summary>
            [FhirElement("description", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Extra info describing activity to perform
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DetailComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
                sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ReasonCode)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ReasonReference)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("goal", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Goal)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusElement?.Serialize(sink);
                sink.Element("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusReason?.Serialize(sink);
                sink.Element("prohibited", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ProhibitedElement?.Serialize(sink);
                sink.Element("scheduled", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Scheduled?.Serialize(sink);
                sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Location?.Serialize(sink);
                sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Performer)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("product", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Product?.Serialize(sink);
                sink.Element("dailyAmount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DailyAmount?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "reasonCode":
                        ReasonCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "reasonReference":
                        ReasonReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "goal":
                        Goal = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "status":
                        StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.CarePlanActivityStatus>>();
                        return true;
                    case "statusReason":
                        StatusReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "prohibited":
                        ProhibitedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "scheduledTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Scheduled, "scheduled");
                        Scheduled = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                        return true;
                    case "scheduledPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Scheduled, "scheduled");
                        Scheduled = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "scheduledString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Scheduled, "scheduled");
                        Scheduled = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "location":
                        Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "performer":
                        Performer = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "productCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Product, "product");
                        Product = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "productReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Product, "product");
                        Product = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "dailyAmount":
                        DailyAmount = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "reasonCode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "reasonReference":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "goal":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "status":
                        StatusElement = source.PopulateValue(StatusElement);
                        return true;
                    case "_status":
                        StatusElement = source.Populate(StatusElement);
                        return true;
                    case "statusReason":
                        StatusReason = source.Populate(StatusReason);
                        return true;
                    case "prohibited":
                        ProhibitedElement = source.PopulateValue(ProhibitedElement);
                        return true;
                    case "_prohibited":
                        ProhibitedElement = source.Populate(ProhibitedElement);
                        return true;
                    case "scheduledTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Scheduled, "scheduled");
                        Scheduled = source.Populate(Scheduled as Hl7.Fhir.Model.DSTU2.Timing);
                        return true;
                    case "scheduledPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Scheduled, "scheduled");
                        Scheduled = source.Populate(Scheduled as Hl7.Fhir.Model.Period);
                        return true;
                    case "scheduledString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Scheduled, "scheduled");
                        Scheduled = source.PopulateValue(Scheduled as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_scheduledString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Scheduled, "scheduled");
                        Scheduled = source.Populate(Scheduled as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "location":
                        Location = source.Populate(Location);
                        return true;
                    case "performer":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "productCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Product, "product");
                        Product = source.Populate(Product as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "productReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Product, "product");
                        Product = source.Populate(Product as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "dailyAmount":
                        DailyAmount = source.Populate(DailyAmount);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
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
                    case "reasonCode":
                        source.PopulateListItem(ReasonCode, index);
                        return true;
                    case "reasonReference":
                        source.PopulateListItem(ReasonReference, index);
                        return true;
                    case "goal":
                        source.PopulateListItem(Goal, index);
                        return true;
                    case "performer":
                        source.PopulateListItem(Performer, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                    if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                    if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.ResourceReference>(Goal.DeepCopy());
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.CarePlanActivityStatus>)StatusElement.DeepCopy();
                    if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                    if(ProhibitedElement != null) dest.ProhibitedElement = (Hl7.Fhir.Model.FhirBoolean)ProhibitedElement.DeepCopy();
                    if(Scheduled != null) dest.Scheduled = (Hl7.Fhir.Model.Element)Scheduled.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                    if(Product != null) dest.Product = (Hl7.Fhir.Model.Element)Product.DeepCopy();
                    if(DailyAmount != null) dest.DailyAmount = (Hl7.Fhir.Model.SimpleQuantity)DailyAmount.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
                if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
                if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
                if( !DeepComparable.Matches(ProhibitedElement, otherT.ProhibitedElement)) return false;
                if( !DeepComparable.Matches(Scheduled, otherT.Scheduled)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
                if( !DeepComparable.Matches(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
                if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
                if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
                if( !DeepComparable.IsExactly(ProhibitedElement, otherT.ProhibitedElement)) return false;
                if( !DeepComparable.IsExactly(Scheduled, otherT.Scheduled)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
                if( !DeepComparable.IsExactly(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (Code != null) yield return Code;
                    foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                    foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                    foreach (var elem in Goal) { if (elem != null) yield return elem; }
                    if (StatusElement != null) yield return StatusElement;
                    if (StatusReason != null) yield return StatusReason;
                    if (ProhibitedElement != null) yield return ProhibitedElement;
                    if (Scheduled != null) yield return Scheduled;
                    if (Location != null) yield return Location;
                    foreach (var elem in Performer) { if (elem != null) yield return elem; }
                    if (Product != null) yield return Product;
                    if (DailyAmount != null) yield return DailyAmount;
                    if (Quantity != null) yield return Quantity;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Code != null) yield return new ElementValue("code", Code);
                    foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                    foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                    foreach (var elem in Goal) { if (elem != null) yield return new ElementValue("goal", elem); }
                    if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                    if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                    if (ProhibitedElement != null) yield return new ElementValue("prohibited", ProhibitedElement);
                    if (Scheduled != null) yield return new ElementValue("scheduled", Scheduled);
                    if (Location != null) yield return new ElementValue("location", Location);
                    foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                    if (Product != null) yield return new ElementValue("product", Product);
                    if (DailyAmount != null) yield return new ElementValue("dailyAmount", DailyAmount);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICarePlanActivityComponent> Hl7.Fhir.Model.ICarePlan.Activity { get { return Activity; } }
    
        
        /// <summary>
        /// External Ids for this plan
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
        /// Who care plan is for
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// proposed | draft | active | completed | cancelled
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.CarePlanStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.CarePlanStatus> _StatusElement;
        
        /// <summary>
        /// proposed | draft | active | completed | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.CarePlanStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.CarePlanStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Created in context of
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
        /// Time period plan covers
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Who is responsible for contents of the plan
        /// </summary>
        [FhirElement("author", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.ResourceReference>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Author;
        
        /// <summary>
        /// When last updated
        /// </summary>
        [FhirElement("modified", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ModifiedElement
        {
            get { return _ModifiedElement; }
            set { _ModifiedElement = value; OnPropertyChanged("ModifiedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ModifiedElement;
        
        /// <summary>
        /// When last updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Modified
        {
            get { return ModifiedElement != null ? ModifiedElement.Value : null; }
            set
            {
                if (value == null)
                    ModifiedElement = null;
                else
                    ModifiedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Modified");
            }
        }
        
        /// <summary>
        /// Type of plan
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
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
        /// Summary of nature of plan
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Summary of nature of plan
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
        /// Health issues this plan addresses
        /// </summary>
        [FhirElement("addresses", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Addresses
        {
            get { if(_Addresses==null) _Addresses = new List<Hl7.Fhir.Model.ResourceReference>(); return _Addresses; }
            set { _Addresses = value; OnPropertyChanged("Addresses"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Addresses;
        
        /// <summary>
        /// Information considered as part of plan
        /// </summary>
        [FhirElement("support", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Support
        {
            get { if(_Support==null) _Support = new List<Hl7.Fhir.Model.ResourceReference>(); return _Support; }
            set { _Support = value; OnPropertyChanged("Support"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Support;
        
        /// <summary>
        /// Plans related to this one
        /// </summary>
        [FhirElement("relatedPlan", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedPlanComponent> RelatedPlan
        {
            get { if(_RelatedPlan==null) _RelatedPlan = new List<RelatedPlanComponent>(); return _RelatedPlan; }
            set { _RelatedPlan = value; OnPropertyChanged("RelatedPlan"); }
        }
        
        private List<RelatedPlanComponent> _RelatedPlan;
        
        /// <summary>
        /// Who's involved in plan?
        /// </summary>
        [FhirElement("participant", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<ParticipantComponent> _Participant;
        
        /// <summary>
        /// Desired outcome of plan
        /// </summary>
        [FhirElement("goal", Order=220)]
        [CLSCompliant(false)]
        [References("Goal")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Goal
        {
            get { if(_Goal==null) _Goal = new List<Hl7.Fhir.Model.ResourceReference>(); return _Goal; }
            set { _Goal = value; OnPropertyChanged("Goal"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Goal;
        
        /// <summary>
        /// Action to occur as part of plan
        /// </summary>
        [FhirElement("activity", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ActivityComponent> Activity
        {
            get { if(_Activity==null) _Activity = new List<ActivityComponent>(); return _Activity; }
            set { _Activity = value; OnPropertyChanged("Activity"); }
        }
        
        private List<ActivityComponent> _Activity;
        
        /// <summary>
        /// Comments about the plan
        /// </summary>
        [FhirElement("note", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Annotation Note
        {
            get { return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private Hl7.Fhir.Model.Annotation _Note;
    
    
        public static ElementDefinitionConstraint[] CarePlan_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "cpl-3",
                severity: ConstraintSeverity.Warning,
                expression: "activity.all(detail.empty() or reference.empty())",
                human: "Provide a reference or detail, not both",
                xpath: "not(exists(f:detail)) or not(exists(f:reference))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(CarePlan_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CarePlan;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.CarePlanStatus>)StatusElement.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
                if(ModifiedElement != null) dest.ModifiedElement = (Hl7.Fhir.Model.FhirDateTime)ModifiedElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Addresses != null) dest.Addresses = new List<Hl7.Fhir.Model.ResourceReference>(Addresses.DeepCopy());
                if(Support != null) dest.Support = new List<Hl7.Fhir.Model.ResourceReference>(Support.DeepCopy());
                if(RelatedPlan != null) dest.RelatedPlan = new List<RelatedPlanComponent>(RelatedPlan.DeepCopy());
                if(Participant != null) dest.Participant = new List<ParticipantComponent>(Participant.DeepCopy());
                if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.ResourceReference>(Goal.DeepCopy());
                if(Activity != null) dest.Activity = new List<ActivityComponent>(Activity.DeepCopy());
                if(Note != null) dest.Note = (Hl7.Fhir.Model.Annotation)Note.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new CarePlan());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CarePlan;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.Matches(Support, otherT.Support)) return false;
            if( !DeepComparable.Matches(RelatedPlan, otherT.RelatedPlan)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
            if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CarePlan;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.IsExactly(Support, otherT.Support)) return false;
            if( !DeepComparable.IsExactly(RelatedPlan, otherT.RelatedPlan)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
            if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("CarePlan");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.BeginList("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Author)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("modified", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ModifiedElement?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("addresses", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Addresses)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("support", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Support)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("relatedPlan", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in RelatedPlan)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("participant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Participant)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("goal", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Goal)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("activity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Activity)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Note?.Serialize(sink);
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
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.CarePlanStatus>>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "author":
                    Author = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "modified":
                    ModifiedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "category":
                    Category = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "addresses":
                    Addresses = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "support":
                    Support = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "relatedPlan":
                    RelatedPlan = source.GetList<RelatedPlanComponent>();
                    return true;
                case "participant":
                    Participant = source.GetList<ParticipantComponent>();
                    return true;
                case "goal":
                    Goal = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "activity":
                    Activity = source.GetList<ActivityComponent>();
                    return true;
                case "note":
                    Note = source.Get<Hl7.Fhir.Model.Annotation>();
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
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "context":
                    Context = source.Populate(Context);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "author":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "modified":
                    ModifiedElement = source.PopulateValue(ModifiedElement);
                    return true;
                case "_modified":
                    ModifiedElement = source.Populate(ModifiedElement);
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
                case "addresses":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "support":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "relatedPlan":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "participant":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "goal":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "activity":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    Note = source.Populate(Note);
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
                case "author":
                    source.PopulateListItem(Author, index);
                    return true;
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "addresses":
                    source.PopulateListItem(Addresses, index);
                    return true;
                case "support":
                    source.PopulateListItem(Support, index);
                    return true;
                case "relatedPlan":
                    source.PopulateListItem(RelatedPlan, index);
                    return true;
                case "participant":
                    source.PopulateListItem(Participant, index);
                    return true;
                case "goal":
                    source.PopulateListItem(Goal, index);
                    return true;
                case "activity":
                    source.PopulateListItem(Activity, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (Context != null) yield return Context;
                if (Period != null) yield return Period;
                foreach (var elem in Author) { if (elem != null) yield return elem; }
                if (ModifiedElement != null) yield return ModifiedElement;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in Addresses) { if (elem != null) yield return elem; }
                foreach (var elem in Support) { if (elem != null) yield return elem; }
                foreach (var elem in RelatedPlan) { if (elem != null) yield return elem; }
                foreach (var elem in Participant) { if (elem != null) yield return elem; }
                foreach (var elem in Goal) { if (elem != null) yield return elem; }
                foreach (var elem in Activity) { if (elem != null) yield return elem; }
                if (Note != null) yield return Note;
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
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Period != null) yield return new ElementValue("period", Period);
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
                if (ModifiedElement != null) yield return new ElementValue("modified", ModifiedElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Addresses) { if (elem != null) yield return new ElementValue("addresses", elem); }
                foreach (var elem in Support) { if (elem != null) yield return new ElementValue("support", elem); }
                foreach (var elem in RelatedPlan) { if (elem != null) yield return new ElementValue("relatedPlan", elem); }
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                foreach (var elem in Goal) { if (elem != null) yield return new ElementValue("goal", elem); }
                foreach (var elem in Activity) { if (elem != null) yield return new ElementValue("activity", elem); }
                if (Note != null) yield return new ElementValue("note", Note);
            }
        }
    
    }

}
