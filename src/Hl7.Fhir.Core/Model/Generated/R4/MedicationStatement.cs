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
    /// Record of medication being taken by a patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicationStatement", IsResource=true)]
    [DataContract]
    public partial class MedicationStatement : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMedicationStatement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationStatement; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationStatement"; } }
    
        
        /// <summary>
        /// External identifier
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
        /// Fulfils plan, proposal or order
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("MedicationRequest","CarePlan","ServiceRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Part of referenced event
        /// </summary>
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("MedicationAdministration","MedicationDispense","MedicationStatement","Procedure","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// active | completed | entered-in-error | intended | stopped | on-hold | unknown | not-taken
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.MedicationStatementStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.MedicationStatementStatus> _StatusElement;
        
        /// <summary>
        /// active | completed | entered-in-error | intended | stopped | on-hold | unknown | not-taken
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.MedicationStatementStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.MedicationStatementStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> StatusReason
        {
            get { if(_StatusReason==null) _StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _StatusReason;
        
        /// <summary>
        /// Type of medication usage
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// What medication was taken
        /// </summary>
        [FhirElement("medication", InSummary=Hl7.Fhir.Model.Version.All, Order=150, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.Element _Medication;
        
        /// <summary>
        /// Who is/was taking  the medication
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter / Episode associated with MedicationStatement
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
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
        /// The date/time or interval when the medication is/was/will be taken
        /// </summary>
        [FhirElement("effective", InSummary=Hl7.Fhir.Model.Version.All, Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// When the statement was asserted?
        /// </summary>
        [FhirElement("dateAsserted", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateAssertedElement
        {
            get { return _DateAssertedElement; }
            set { _DateAssertedElement = value; OnPropertyChanged("DateAssertedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateAssertedElement;
        
        /// <summary>
        /// When the statement was asserted?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateAsserted
        {
            get { return DateAssertedElement != null ? DateAssertedElement.Value : null; }
            set
            {
                if (value == null)
                    DateAssertedElement = null;
                else
                    DateAssertedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateAsserted");
            }
        }
        
        /// <summary>
        /// Person or organization that provided the information about the taking of this medication
        /// </summary>
        [FhirElement("informationSource", Order=200)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","PractitionerRole","RelatedPerson","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference InformationSource
        {
            get { return _InformationSource; }
            set { _InformationSource = value; OnPropertyChanged("InformationSource"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _InformationSource;
        
        /// <summary>
        /// Additional supporting information
        /// </summary>
        [FhirElement("derivedFrom", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> DerivedFrom
        {
            get { if(_DerivedFrom==null) _DerivedFrom = new List<Hl7.Fhir.Model.ResourceReference>(); return _DerivedFrom; }
            set { _DerivedFrom = value; OnPropertyChanged("DerivedFrom"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _DerivedFrom;
        
        /// <summary>
        /// Reason for why the medication is being/was taken
        /// </summary>
        [FhirElement("reasonCode", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Condition or observation that supports why the medication is being/was taken
        /// </summary>
        [FhirElement("reasonReference", Order=230)]
        [CLSCompliant(false)]
        [References("Condition","Observation","DiagnosticReport")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Further information about the statement
        /// </summary>
        [FhirElement("note", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Details of how medication is/was taken or should be taken
        /// </summary>
        [FhirElement("dosage", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.Dosage> Dosage
        {
            get { if(_Dosage==null) _Dosage = new List<Hl7.Fhir.Model.R4.Dosage>(); return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private List<Hl7.Fhir.Model.R4.Dosage> _Dosage;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationStatement;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.MedicationStatementStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(StatusReason.DeepCopy());
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.FhirDateTime)DateAssertedElement.DeepCopy();
                if(InformationSource != null) dest.InformationSource = (Hl7.Fhir.Model.ResourceReference)InformationSource.DeepCopy();
                if(DerivedFrom != null) dest.DerivedFrom = new List<Hl7.Fhir.Model.ResourceReference>(DerivedFrom.DeepCopy());
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Dosage != null) dest.Dosage = new List<Hl7.Fhir.Model.R4.Dosage>(Dosage.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicationStatement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationStatement;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.Matches(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.Matches(DerivedFrom, otherT.DerivedFrom)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationStatement;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.IsExactly(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.IsExactly(DerivedFrom, otherT.DerivedFrom)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicationStatement");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.BeginList("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in StatusReason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Category?.Serialize(sink);
            sink.Element("medication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Medication?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
            sink.Element("effective", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Effective?.Serialize(sink);
            sink.Element("dateAsserted", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateAssertedElement?.Serialize(sink);
            sink.Element("informationSource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InformationSource?.Serialize(sink);
            sink.BeginList("derivedFrom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DerivedFrom)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("dosage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Dosage)
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
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.MedicationStatementStatus>>();
                    return true;
                case "statusReason":
                    StatusReason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "effectivePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Effective, "effective");
                    Effective = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "dateAsserted":
                    DateAssertedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "informationSource":
                    InformationSource = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "derivedFrom":
                    DerivedFrom = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonCode":
                    ReasonCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    ReasonReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "dosage":
                    Dosage = source.GetList<Hl7.Fhir.Model.R4.Dosage>();
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
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusReason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "context":
                    Context = source.Populate(Context);
                    return true;
                case "effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.PopulateValue(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "effectivePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.Period);
                    return true;
                case "dateAsserted":
                    DateAssertedElement = source.PopulateValue(DateAssertedElement);
                    return true;
                case "_dateAsserted":
                    DateAssertedElement = source.Populate(DateAssertedElement);
                    return true;
                case "informationSource":
                    InformationSource = source.Populate(InformationSource);
                    return true;
                case "derivedFrom":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dosage":
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
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
                case "statusReason":
                    source.PopulateListItem(StatusReason, index);
                    return true;
                case "derivedFrom":
                    source.PopulateListItem(DerivedFrom, index);
                    return true;
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "dosage":
                    source.PopulateListItem(Dosage, index);
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
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in StatusReason) { if (elem != null) yield return elem; }
                if (Category != null) yield return Category;
                if (Medication != null) yield return Medication;
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                if (Effective != null) yield return Effective;
                if (DateAssertedElement != null) yield return DateAssertedElement;
                if (InformationSource != null) yield return InformationSource;
                foreach (var elem in DerivedFrom) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Dosage) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in StatusReason) { if (elem != null) yield return new ElementValue("statusReason", elem); }
                if (Category != null) yield return new ElementValue("category", Category);
                if (Medication != null) yield return new ElementValue("medication", Medication);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Effective != null) yield return new ElementValue("effective", Effective);
                if (DateAssertedElement != null) yield return new ElementValue("dateAsserted", DateAssertedElement);
                if (InformationSource != null) yield return new ElementValue("informationSource", InformationSource);
                foreach (var elem in DerivedFrom) { if (elem != null) yield return new ElementValue("derivedFrom", elem); }
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Dosage) { if (elem != null) yield return new ElementValue("dosage", elem); }
            }
        }
    
    }

}
