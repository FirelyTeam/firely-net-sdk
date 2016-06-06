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
// Generated for FHIR v1.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A description of decision support service functionality
    /// </summary>
    [FhirType("DecisionSupportServiceModule", IsResource=true)]
    [DataContract]
    public partial class DecisionSupportServiceModule : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DecisionSupportServiceModule; } }
        [NotMapped]
        public override string TypeName { get { return "DecisionSupportServiceModule"; } }
        
        /// <summary>
        /// Metadata for the service module
        /// </summary>
        [FhirElement("moduleMetadata", InSummary=true, Order=90)]
        [DataMember]
        public ModuleMetadata ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private ModuleMetadata _ModuleMetadata;
        
        /// <summary>
        /// "when" the module should be invoked
        /// </summary>
        [FhirElement("trigger", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TriggerDefinition> Trigger
        {
            get { if(_Trigger==null) _Trigger = new List<TriggerDefinition>(); return _Trigger; }
            set { _Trigger = value; OnPropertyChanged("Trigger"); }
        }
        
        private List<TriggerDefinition> _Trigger;
        
        /// <summary>
        /// Parameters to the module
        /// </summary>
        [FhirElement("parameter", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParameterDefinition> Parameter
        {
            get { if(_Parameter==null) _Parameter = new List<ParameterDefinition>(); return _Parameter; }
            set { _Parameter = value; OnPropertyChanged("Parameter"); }
        }
        
        private List<ParameterDefinition> _Parameter;
        
        /// <summary>
        /// Data requirements for the module
        /// </summary>
        [FhirElement("dataRequirement", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DataRequirement> DataRequirement
        {
            get { if(_DataRequirement==null) _DataRequirement = new List<DataRequirement>(); return _DataRequirement; }
            set { _DataRequirement = value; OnPropertyChanged("DataRequirement"); }
        }
        
        private List<DataRequirement> _DataRequirement;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DecisionSupportServiceModule;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ModuleMetadata != null) dest.ModuleMetadata = (ModuleMetadata)ModuleMetadata.DeepCopy();
                if(Trigger != null) dest.Trigger = new List<TriggerDefinition>(Trigger.DeepCopy());
                if(Parameter != null) dest.Parameter = new List<ParameterDefinition>(Parameter.DeepCopy());
                if(DataRequirement != null) dest.DataRequirement = new List<DataRequirement>(DataRequirement.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DecisionSupportServiceModule());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DecisionSupportServiceModule;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.Matches(DataRequirement, otherT.DataRequirement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DecisionSupportServiceModule;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
            if( !DeepComparable.IsExactly(DataRequirement, otherT.DataRequirement)) return false;
            
            return true;
        }
        
    }
    
}
