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
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes the context of use for a module
    /// </summary>
    [FhirType("UsageContext")]
    [DataContract]
    public partial class UsageContext : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "UsageContext"; } }
        
        /// <summary>
        /// Patient gender
        /// </summary>
        [FhirElement("patientGender", InSummary=true, Order=30)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PatientGender
        {
            get { if(_PatientGender==null) _PatientGender = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PatientGender; }
            set { _PatientGender = value; OnPropertyChanged("PatientGender"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PatientGender;
        
        /// <summary>
        /// Demographic category
        /// </summary>
        [FhirElement("patientAgeGroup", InSummary=true, Order=40)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PatientAgeGroup
        {
            get { if(_PatientAgeGroup==null) _PatientAgeGroup = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PatientAgeGroup; }
            set { _PatientAgeGroup = value; OnPropertyChanged("PatientAgeGroup"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PatientAgeGroup;
        
        /// <summary>
        /// Clinical concepts addressed
        /// </summary>
        [FhirElement("clinicalFocus", InSummary=true, Order=50)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ClinicalFocus
        {
            get { if(_ClinicalFocus==null) _ClinicalFocus = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ClinicalFocus; }
            set { _ClinicalFocus = value; OnPropertyChanged("ClinicalFocus"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ClinicalFocus;
        
        /// <summary>
        /// Target user type
        /// </summary>
        [FhirElement("targetUser", InSummary=true, Order=60)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> TargetUser
        {
            get { if(_TargetUser==null) _TargetUser = new List<Hl7.Fhir.Model.CodeableConcept>(); return _TargetUser; }
            set { _TargetUser = value; OnPropertyChanged("TargetUser"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _TargetUser;
        
        /// <summary>
        /// Workflow setting
        /// </summary>
        [FhirElement("workflowSetting", InSummary=true, Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> WorkflowSetting
        {
            get { if(_WorkflowSetting==null) _WorkflowSetting = new List<Hl7.Fhir.Model.CodeableConcept>(); return _WorkflowSetting; }
            set { _WorkflowSetting = value; OnPropertyChanged("WorkflowSetting"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _WorkflowSetting;
        
        /// <summary>
        /// Clinical task context
        /// </summary>
        [FhirElement("workflowTask", InSummary=true, Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> WorkflowTask
        {
            get { if(_WorkflowTask==null) _WorkflowTask = new List<Hl7.Fhir.Model.CodeableConcept>(); return _WorkflowTask; }
            set { _WorkflowTask = value; OnPropertyChanged("WorkflowTask"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _WorkflowTask;
        
        /// <summary>
        /// Applicable venue
        /// </summary>
        [FhirElement("clinicalVenue", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ClinicalVenue
        {
            get { if(_ClinicalVenue==null) _ClinicalVenue = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ClinicalVenue; }
            set { _ClinicalVenue = value; OnPropertyChanged("ClinicalVenue"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ClinicalVenue;
        
        /// <summary>
        /// Intended jurisdiction
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as UsageContext;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(PatientGender != null) dest.PatientGender = new List<Hl7.Fhir.Model.CodeableConcept>(PatientGender.DeepCopy());
                if(PatientAgeGroup != null) dest.PatientAgeGroup = new List<Hl7.Fhir.Model.CodeableConcept>(PatientAgeGroup.DeepCopy());
                if(ClinicalFocus != null) dest.ClinicalFocus = new List<Hl7.Fhir.Model.CodeableConcept>(ClinicalFocus.DeepCopy());
                if(TargetUser != null) dest.TargetUser = new List<Hl7.Fhir.Model.CodeableConcept>(TargetUser.DeepCopy());
                if(WorkflowSetting != null) dest.WorkflowSetting = new List<Hl7.Fhir.Model.CodeableConcept>(WorkflowSetting.DeepCopy());
                if(WorkflowTask != null) dest.WorkflowTask = new List<Hl7.Fhir.Model.CodeableConcept>(WorkflowTask.DeepCopy());
                if(ClinicalVenue != null) dest.ClinicalVenue = new List<Hl7.Fhir.Model.CodeableConcept>(ClinicalVenue.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new UsageContext());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as UsageContext;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(PatientGender, otherT.PatientGender)) return false;
            if( !DeepComparable.Matches(PatientAgeGroup, otherT.PatientAgeGroup)) return false;
            if( !DeepComparable.Matches(ClinicalFocus, otherT.ClinicalFocus)) return false;
            if( !DeepComparable.Matches(TargetUser, otherT.TargetUser)) return false;
            if( !DeepComparable.Matches(WorkflowSetting, otherT.WorkflowSetting)) return false;
            if( !DeepComparable.Matches(WorkflowTask, otherT.WorkflowTask)) return false;
            if( !DeepComparable.Matches(ClinicalVenue, otherT.ClinicalVenue)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as UsageContext;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(PatientGender, otherT.PatientGender)) return false;
            if( !DeepComparable.IsExactly(PatientAgeGroup, otherT.PatientAgeGroup)) return false;
            if( !DeepComparable.IsExactly(ClinicalFocus, otherT.ClinicalFocus)) return false;
            if( !DeepComparable.IsExactly(TargetUser, otherT.TargetUser)) return false;
            if( !DeepComparable.IsExactly(WorkflowSetting, otherT.WorkflowSetting)) return false;
            if( !DeepComparable.IsExactly(WorkflowTask, otherT.WorkflowTask)) return false;
            if( !DeepComparable.IsExactly(ClinicalVenue, otherT.ClinicalVenue)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            
            return true;
        }
    
    
    }
    
}
