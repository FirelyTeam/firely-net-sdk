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
// Generated for FHIR v1.0.2, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Eligibility request
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "EligibilityRequest", IsResource=true)]
    [DataContract]
    public partial class EligibilityRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.EligibilityRequest; } }
        [NotMapped]
        public override string TypeName { get { return "EligibilityRequest"; } }
    
        
        /// <summary>
        /// Business Identifier
        /// </summary>
        [FhirElement("identifier", Versions=Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=90)]
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
        /// Resource version
        /// </summary>
        [FhirElement("ruleset", Versions=Hl7.Fhir.Model.Version.DSTU2, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Ruleset
        {
            get { return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _Ruleset;
        
        /// <summary>
        /// Original version
        /// </summary>
        [FhirElement("originalRuleset", Versions=Hl7.Fhir.Model.Version.DSTU2, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding OriginalRuleset
        {
            get { return _OriginalRuleset; }
            set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _OriginalRuleset;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", Versions=Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                    CreatedElement = null;
                else
                    CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Insurer
        /// </summary>
        [FhirElement("target", Versions=Hl7.Fhir.Model.Version.DSTU2, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=130)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Target;
        
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        [FhirElement("provider", Versions=Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=140)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Provider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("organization", Versions=Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=150)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", Versions=Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.STU3, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Desired processing priority
        /// </summary>
        [FhirElement("priority", Versions=Hl7.Fhir.Model.Version.STU3, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Versions=Hl7.Fhir.Model.Version.STU3, Order=180)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Estimated date or dates of Service
        /// </summary>
        [FhirElement("serviced", Versions=Hl7.Fhir.Model.Version.STU3, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(Version=Version.STU3, Types=new[]{typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period)})]
        [DataMember]
        public Hl7.Fhir.Model.Element Serviced
        {
            get { return _Serviced; }
            set { _Serviced = value; OnPropertyChanged("Serviced"); }
        }
        
        private Hl7.Fhir.Model.Element _Serviced;
        
        /// <summary>
        /// Author
        /// </summary>
        [FhirElement("enterer", Versions=Hl7.Fhir.Model.Version.STU3, Order=200)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// Target
        /// </summary>
        [FhirElement("insurer", Versions=Hl7.Fhir.Model.Version.STU3, Order=210)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Insurer;
        
        /// <summary>
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", Versions=Hl7.Fhir.Model.Version.STU3, Order=220)]
        [CLSCompliant(false)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Facility
        {
            get { return _Facility; }
            set { _Facility = value; OnPropertyChanged("Facility"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Facility;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("coverage", Versions=Hl7.Fhir.Model.Version.STU3, Order=230)]
        [CLSCompliant(false)]
        [References("Coverage")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Coverage
        {
            get { return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Coverage;
        
        /// <summary>
        /// Business agreement
        /// </summary>
        [FhirElement("businessArrangement", Versions=Hl7.Fhir.Model.Version.STU3, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString BusinessArrangementElement
        {
            get { return _BusinessArrangementElement; }
            set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
        
        /// <summary>
        /// Business agreement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string BusinessArrangement
        {
            get { return BusinessArrangementElement != null ? BusinessArrangementElement.Value : null; }
            set
            {
                if (value == null)
                    BusinessArrangementElement = null;
                else
                    BusinessArrangementElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("BusinessArrangement");
            }
        }
        
        /// <summary>
        /// Type of services covered
        /// </summary>
        [FhirElement("benefitCategory", Versions=Hl7.Fhir.Model.Version.STU3, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BenefitCategory
        {
            get { return _BenefitCategory; }
            set { _BenefitCategory = value; OnPropertyChanged("BenefitCategory"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BenefitCategory;
        
        /// <summary>
        /// Detailed services covered within the type
        /// </summary>
        [FhirElement("benefitSubCategory", Versions=Hl7.Fhir.Model.Version.STU3, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BenefitSubCategory
        {
            get { return _BenefitSubCategory; }
            set { _BenefitSubCategory = value; OnPropertyChanged("BenefitSubCategory"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BenefitSubCategory;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as EligibilityRequest;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.Coding)Ruleset.DeepCopy();
                if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.ResourceReference)Facility.DeepCopy();
                if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                if(BenefitCategory != null) dest.BenefitCategory = (Hl7.Fhir.Model.CodeableConcept)BenefitCategory.DeepCopy();
                if(BenefitSubCategory != null) dest.BenefitSubCategory = (Hl7.Fhir.Model.CodeableConcept)BenefitSubCategory.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new EligibilityRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as EligibilityRequest;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
            if( !DeepComparable.Matches(BenefitCategory, otherT.BenefitCategory)) return false;
            if( !DeepComparable.Matches(BenefitSubCategory, otherT.BenefitSubCategory)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as EligibilityRequest;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
            if( !DeepComparable.IsExactly(BenefitCategory, otherT.BenefitCategory)) return false;
            if( !DeepComparable.IsExactly(BenefitSubCategory, otherT.BenefitSubCategory)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("EligibilityRequest");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.DSTU2, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("ruleset", Hl7.Fhir.Model.Version.DSTU2, Hl7.Fhir.Model.Version.DSTU2, false, false); Ruleset?.Serialize(sink);
            sink.Element("originalRuleset", Hl7.Fhir.Model.Version.DSTU2, Hl7.Fhir.Model.Version.DSTU2, false, false); OriginalRuleset?.Serialize(sink);
            sink.Element("created", Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.DSTU2, false, false); CreatedElement?.Serialize(sink);
            sink.Element("target", Hl7.Fhir.Model.Version.DSTU2, Hl7.Fhir.Model.Version.DSTU2, false, false); Target?.Serialize(sink);
            sink.Element("provider", Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.DSTU2, false, false); Provider?.Serialize(sink);
            sink.Element("organization", Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.DSTU2, false, false); Organization?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.STU3, false, false); StatusElement?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Priority?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Patient?.Serialize(sink);
            sink.Element("serviced", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, true); Serviced?.Serialize(sink);
            sink.Element("enterer", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Enterer?.Serialize(sink);
            sink.Element("insurer", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Insurer?.Serialize(sink);
            sink.Element("facility", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Facility?.Serialize(sink);
            sink.Element("coverage", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Coverage?.Serialize(sink);
            sink.Element("businessArrangement", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); BusinessArrangementElement?.Serialize(sink);
            sink.Element("benefitCategory", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); BenefitCategory?.Serialize(sink);
            sink.Element("benefitSubCategory", Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); BenefitSubCategory?.Serialize(sink);
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
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "ruleset" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                    Ruleset = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "originalRuleset" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                    OriginalRuleset = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "created" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    CreatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "target" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                    Target = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "provider" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    Provider = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "organization" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    Organization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>>();
                    return true;
                case "priority" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Priority = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "patient" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "servicedDate" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                    Serviced = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "servicedPeriod" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Serviced, "serviced");
                    Serviced = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "enterer" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Enterer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "insurer" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Insurer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "facility" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Facility = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "coverage" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Coverage = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "businessArrangement" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BusinessArrangementElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "benefitCategory" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BenefitCategory = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "benefitSubCategory" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BenefitSubCategory = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "ruleset" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                    Ruleset = source.Populate(Ruleset);
                    return true;
                case "originalRuleset" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                    OriginalRuleset = source.Populate(OriginalRuleset);
                    return true;
                case "created" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    CreatedElement = source.PopulateValue(CreatedElement);
                    return true;
                case "_created" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    CreatedElement = source.Populate(CreatedElement);
                    return true;
                case "target" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                    Target = source.Populate(Target);
                    return true;
                case "provider" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    Provider = source.Populate(Provider);
                    return true;
                case "organization" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
                    Organization = source.Populate(Organization);
                    return true;
                case "status" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "priority" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Priority = source.Populate(Priority);
                    return true;
                case "patient" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Patient = source.Populate(Patient);
                    return true;
                case "servicedDate" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                    Serviced = source.PopulateValue(Serviced as Hl7.Fhir.Model.Date);
                    return true;
                case "_servicedDate" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                    Serviced = source.Populate(Serviced as Hl7.Fhir.Model.Date);
                    return true;
                case "servicedPeriod" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Serviced, "serviced");
                    Serviced = source.Populate(Serviced as Hl7.Fhir.Model.Period);
                    return true;
                case "enterer" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Enterer = source.Populate(Enterer);
                    return true;
                case "insurer" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Insurer = source.Populate(Insurer);
                    return true;
                case "facility" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Facility = source.Populate(Facility);
                    return true;
                case "coverage" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    Coverage = source.Populate(Coverage);
                    return true;
                case "businessArrangement" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BusinessArrangementElement = source.PopulateValue(BusinessArrangementElement);
                    return true;
                case "_businessArrangement" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BusinessArrangementElement = source.Populate(BusinessArrangementElement);
                    return true;
                case "benefitCategory" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BenefitCategory = source.Populate(BenefitCategory);
                    return true;
                case "benefitSubCategory" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                    BenefitSubCategory = source.Populate(BenefitSubCategory);
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
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2|Hl7.Fhir.Model.Version.STU3):
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
                if (Ruleset != null) yield return Ruleset;
                if (OriginalRuleset != null) yield return OriginalRuleset;
                if (CreatedElement != null) yield return CreatedElement;
                if (Target != null) yield return Target;
                if (Provider != null) yield return Provider;
                if (Organization != null) yield return Organization;
                if (StatusElement != null) yield return StatusElement;
                if (Priority != null) yield return Priority;
                if (Patient != null) yield return Patient;
                if (Serviced != null) yield return Serviced;
                if (Enterer != null) yield return Enterer;
                if (Insurer != null) yield return Insurer;
                if (Facility != null) yield return Facility;
                if (Coverage != null) yield return Coverage;
                if (BusinessArrangementElement != null) yield return BusinessArrangementElement;
                if (BenefitCategory != null) yield return BenefitCategory;
                if (BenefitSubCategory != null) yield return BenefitSubCategory;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Ruleset != null) yield return new ElementValue("ruleset", Ruleset);
                if (OriginalRuleset != null) yield return new ElementValue("originalRuleset", OriginalRuleset);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Target != null) yield return new ElementValue("target", Target);
                if (Provider != null) yield return new ElementValue("provider", Provider);
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Priority != null) yield return new ElementValue("priority", Priority);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Serviced != null) yield return new ElementValue("serviced", Serviced);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                if (Facility != null) yield return new ElementValue("facility", Facility);
                if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                if (BusinessArrangementElement != null) yield return new ElementValue("businessArrangement", BusinessArrangementElement);
                if (BenefitCategory != null) yield return new ElementValue("benefitCategory", BenefitCategory);
                if (BenefitSubCategory != null) yield return new ElementValue("benefitSubCategory", BenefitSubCategory);
            }
        }
    
    }

}
