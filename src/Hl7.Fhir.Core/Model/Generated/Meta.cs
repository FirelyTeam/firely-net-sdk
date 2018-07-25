using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Metadata about a resource
    /// </summary>
    [FhirType("Meta")]
    [DataContract]
    public partial class Meta : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Meta"; } }
        
        /// <summary>
        /// Version specific identifier
        /// </summary>
        [FhirElement("versionId", InSummary=true, Order=30)]
        [DataMember]
        public Hl7.Fhir.Model.Id VersionIdElement
        {
            get { return _VersionIdElement; }
            set { _VersionIdElement = value; OnPropertyChanged("VersionIdElement"); }
        }
        
        private Hl7.Fhir.Model.Id _VersionIdElement;
        
        /// <summary>
        /// Version specific identifier
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string VersionId
        {
            get { return VersionIdElement != null ? VersionIdElement.Value : null; }
            set
            {
                if (value == null)
                  VersionIdElement = null; 
                else
                  VersionIdElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("VersionId");
            }
        }
        
        /// <summary>
        /// When the resource version last changed
        /// </summary>
        [FhirElement("lastUpdated", InSummary=true, Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.Instant LastUpdatedElement
        {
            get { return _LastUpdatedElement; }
            set { _LastUpdatedElement = value; OnPropertyChanged("LastUpdatedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _LastUpdatedElement;
        
        /// <summary>
        /// When the resource version last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? LastUpdated
        {
            get { return LastUpdatedElement != null ? LastUpdatedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  LastUpdatedElement = null; 
                else
                  LastUpdatedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("LastUpdated");
            }
        }
        
        /// <summary>
        /// Profiles this resource claims to conform to
        /// </summary>
        [FhirElement("profile", InSummary=true, Order=50)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> ProfileElement
        {
            get { if(_ProfileElement==null) _ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ProfileElement; }
            set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _ProfileElement;
        
        /// <summary>
        /// Profiles this resource claims to conform to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Profile
        {
            get { return ProfileElement != null ? ProfileElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ProfileElement = null; 
                else
                  ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Profile");
            }
        }
        
        /// <summary>
        /// Security Labels applied to this resource
        /// </summary>
        [FhirElement("security", InSummary=true, Order=60)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Security
        {
            get { if(_Security==null) _Security = new List<Hl7.Fhir.Model.Coding>(); return _Security; }
            set { _Security = value; OnPropertyChanged("Security"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Security;
        
        /// <summary>
        /// Tags applied to this resource
        /// </summary>
        [FhirElement("tag", InSummary=true, Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Tag
        {
            get { if(_Tag==null) _Tag = new List<Hl7.Fhir.Model.Coding>(); return _Tag; }
            set { _Tag = value; OnPropertyChanged("Tag"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Tag;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Meta;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(VersionIdElement != null) dest.VersionIdElement = (Hl7.Fhir.Model.Id)VersionIdElement.DeepCopy();
                if(LastUpdatedElement != null) dest.LastUpdatedElement = (Hl7.Fhir.Model.Instant)LastUpdatedElement.DeepCopy();
                if(ProfileElement != null) dest.ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(ProfileElement.DeepCopy());
                if(Security != null) dest.Security = new List<Hl7.Fhir.Model.Coding>(Security.DeepCopy());
                if(Tag != null) dest.Tag = new List<Hl7.Fhir.Model.Coding>(Tag.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Meta());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Meta;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(VersionIdElement, otherT.VersionIdElement)) return false;
            if( !DeepComparable.Matches(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
            if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.Matches(Security, otherT.Security)) return false;
            if( !DeepComparable.Matches(Tag, otherT.Tag)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Meta;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(VersionIdElement, otherT.VersionIdElement)) return false;
            if( !DeepComparable.IsExactly(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
            if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.IsExactly(Security, otherT.Security)) return false;
            if( !DeepComparable.IsExactly(Tag, otherT.Tag)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (VersionIdElement != null) yield return VersionIdElement;
                if (LastUpdatedElement != null) yield return LastUpdatedElement;
                foreach (var elem in ProfileElement) { if (elem != null) yield return elem; }
                foreach (var elem in Security) { if (elem != null) yield return elem; }
                foreach (var elem in Tag) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (VersionIdElement != null) yield return new ElementValue("versionId", VersionIdElement);
                if (LastUpdatedElement != null) yield return new ElementValue("lastUpdated", LastUpdatedElement);
                foreach (var elem in ProfileElement) { if (elem != null) yield return new ElementValue("profile", elem); }
                foreach (var elem in Security) { if (elem != null) yield return new ElementValue("security", elem); }
                foreach (var elem in Tag) { if (elem != null) yield return new ElementValue("tag", elem); }
 
            } 
        } 
    
    
    }
    
}
