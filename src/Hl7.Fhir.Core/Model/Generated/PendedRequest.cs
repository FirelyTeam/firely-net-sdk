using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
// Generated on Thu, Mar 5, 2015 16:19+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// PendedRequest request
    /// </summary>
    [FhirType("PendedRequest", IsResource=true)]
    [DataContract]
    public partial class PendedRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.PendedRequest; } }
        [NotMapped]
        public override string TypeName { get { return "PendedRequest"; } }
        
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
        /// Resource version
        /// </summary>
        [FhirElement("ruleset", Order=100)]
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
        [FhirElement("originalRuleset", Order=110)]
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
        [FhirElement("created", Order=120)]
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
                if(value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Insurer
        /// </summary>
        [FhirElement("target", Order=130)]
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
        [FhirElement("provider", Order=140)]
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
        [FhirElement("organization", Order=150)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// Request reference
        /// </summary>
        [FhirElement("request", Order=160)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Resource type(s) to include
        /// </summary>
        [FhirElement("include", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> IncludeElement
        {
            get { if(_IncludeElement==null) _IncludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _IncludeElement; }
            set { _IncludeElement = value; OnPropertyChanged("IncludeElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _IncludeElement;
        
        /// <summary>
        /// Resource type(s) to include
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Include
        {
            get { return IncludeElement != null ? IncludeElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  IncludeElement = null; 
                else
                  IncludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Include");
            }
        }
        
        /// <summary>
        /// Resource type(s) to exclude
        /// </summary>
        [FhirElement("exclude", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ExcludeElement
        {
            get { if(_ExcludeElement==null) _ExcludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _ExcludeElement; }
            set { _ExcludeElement = value; OnPropertyChanged("ExcludeElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ExcludeElement;
        
        /// <summary>
        /// Resource type(s) to exclude
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Exclude
        {
            get { return ExcludeElement != null ? ExcludeElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  ExcludeElement = null; 
                else
                  ExcludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Exclude");
            }
        }
        
        /// <summary>
        /// Period
        /// </summary>
        [FhirElement("period", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as PendedRequest;
            
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
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(IncludeElement != null) dest.IncludeElement = new List<Hl7.Fhir.Model.FhirString>(IncludeElement.DeepCopy());
                if(ExcludeElement != null) dest.ExcludeElement = new List<Hl7.Fhir.Model.FhirString>(ExcludeElement.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new PendedRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as PendedRequest;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(IncludeElement, otherT.IncludeElement)) return false;
            if( !DeepComparable.Matches(ExcludeElement, otherT.ExcludeElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as PendedRequest;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(IncludeElement, otherT.IncludeElement)) return false;
            if( !DeepComparable.IsExactly(ExcludeElement, otherT.ExcludeElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            
            return true;
        }
        
    }
    
}
