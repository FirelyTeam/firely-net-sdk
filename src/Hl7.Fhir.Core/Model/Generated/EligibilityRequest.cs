using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.6.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Eligibility request
    /// </summary>
    [FhirType("EligibilityRequest", IsResource=true)]
    [DataContract]
    public partial class EligibilityRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.EligibilityRequest; } }
        [NotMapped]
        public override string TypeName { get { return "EligibilityRequest"; } }
        
        /// <summary>
        /// A code specifying the state of the resource instance.
        /// (url: http://hl7.org/fhir/ValueSet/eligibilityrequest-status)
        /// </summary>
        [FhirEnumeration("EligibilityRequestStatus")]
        public enum EligibilityRequestStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityrequest-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityrequest-status)
            /// </summary>
            [EnumLiteral("cancelled"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityrequest-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/eligibilityrequest-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered In Error")]
            EnteredInError,
        }

        /// <summary>
        /// Business Identifier
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.EligibilityRequest.EligibilityRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.EligibilityRequest.EligibilityRequestStatus> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.EligibilityRequest.EligibilityRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.EligibilityRequest.EligibilityRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Resource version
        /// </summary>
        [FhirElement("ruleset", Order=110)]
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
        [FhirElement("originalRuleset", Order=120)]
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
        [FhirElement("created", Order=130)]
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
        /// Target
        /// </summary>
        [FhirElement("insurer", Order=140, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.Element _Insurer;
        
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        [FhirElement("provider", Order=150, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.Element _Provider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("organization", Order=160, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.Element _Organization;
        
        /// <summary>
        /// Desired processing priority
        /// </summary>
        [FhirElement("priority", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.Coding _Priority;
        
        /// <summary>
        /// Author
        /// </summary>
        [FhirElement("enterer", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.Element _Enterer;
        
        /// <summary>
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", Order=190, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Facility
        {
            get { return _Facility; }
            set { _Facility = value; OnPropertyChanged("Facility"); }
        }
        
        private Hl7.Fhir.Model.Element _Facility;
        
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Order=200, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.Element _Patient;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("coverage", Order=210, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Coverage
        {
            get { return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private Hl7.Fhir.Model.Element _Coverage;
        
        /// <summary>
        /// Business agreement
        /// </summary>
        [FhirElement("businessArrangement", Order=220)]
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
        /// Estimated date or dates of Service
        /// </summary>
        [FhirElement("serviced", Order=230, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Serviced
        {
            get { return _Serviced; }
            set { _Serviced = value; OnPropertyChanged("Serviced"); }
        }
        
        private Hl7.Fhir.Model.Element _Serviced;
        
        /// <summary>
        /// Benefit Category
        /// </summary>
        [FhirElement("benefitCategory", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Coding BenefitCategory
        {
            get { return _BenefitCategory; }
            set { _BenefitCategory = value; OnPropertyChanged("BenefitCategory"); }
        }
        
        private Hl7.Fhir.Model.Coding _BenefitCategory;
        
        /// <summary>
        /// Benefit SubCategory
        /// </summary>
        [FhirElement("benefitSubCategory", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.Coding BenefitSubCategory
        {
            get { return _BenefitSubCategory; }
            set { _BenefitSubCategory = value; OnPropertyChanged("BenefitSubCategory"); }
        }
        
        private Hl7.Fhir.Model.Coding _BenefitSubCategory;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as EligibilityRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.EligibilityRequest.EligibilityRequestStatus>)StatusElement.DeepCopy();
                if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.Coding)Ruleset.DeepCopy();
                if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.Element)Insurer.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.Element)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.Element)Organization.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.Coding)Priority.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.Element)Enterer.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.Element)Facility.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.Element)Patient.DeepCopy();
                if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.Element)Coverage.DeepCopy();
                if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                if(BenefitCategory != null) dest.BenefitCategory = (Hl7.Fhir.Model.Coding)BenefitCategory.DeepCopy();
                if(BenefitSubCategory != null) dest.BenefitSubCategory = (Hl7.Fhir.Model.Coding)BenefitSubCategory.DeepCopy();
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
            if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
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
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
            if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
            if( !DeepComparable.IsExactly(BenefitCategory, otherT.BenefitCategory)) return false;
            if( !DeepComparable.IsExactly(BenefitSubCategory, otherT.BenefitSubCategory)) return false;
            
            return true;
        }
        
    }
    
}
