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
// Generated on Mon, Dec 22, 2014 15:52+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Base Resource
    /// </summary>
    [DataContract]
    public abstract partial class Resource : Base
    {
        [NotMapped]
        public virtual ResourceType ResourceType { get { return ResourceType.Resource; } }
        [NotMapped]
        public override string TypeName { get { return "Resource"; } }
        
        [FhirType("ResourceMetaComponent")]
        [DataContract]
        public partial class ResourceMetaComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceMetaComponent"; } }
            
            /// <summary>
            /// Version specific identifier
            /// </summary>
            [FhirElement("versionId", InSummary=true, Order=20)]
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
                    if(value == null)
                      VersionIdElement = null; 
                    else
                      VersionIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("VersionId");
                }
            }
            
            /// <summary>
            /// When the resource version last changed
            /// </summary>
            [FhirElement("lastUpdated", InSummary=true, Order=30)]
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
                    if(value == null)
                      LastUpdatedElement = null; 
                    else
                      LastUpdatedElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("LastUpdated");
                }
            }
            
            /// <summary>
            /// Profiles this resource claims to conform to
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=40)]
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
                    if(value == null)
                      ProfileElement = null; 
                    else
                      ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Profile");
                }
            }
            
            /// <summary>
            /// Security Labels applied to this resource
            /// </summary>
            [FhirElement("security", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Security
            {
                get { if(_Security==null) _Security = new List<Hl7.Fhir.Model.Coding>(); return _Security; }
                set { _Security = value; OnPropertyChanged("Security"); }
            }
            private List<Hl7.Fhir.Model.Coding> _Security;
            
            /// <summary>
            /// Tags applied
            /// </summary>
            [FhirElement("tag", InSummary=true, Order=60)]
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
                var dest = other as ResourceMetaComponent;
                
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
                return CopyTo(new ResourceMetaComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResourceMetaComponent;
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
                var otherT = other as ResourceMetaComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(VersionIdElement, otherT.VersionIdElement)) return false;
                if( !DeepComparable.IsExactly(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.IsExactly(Security, otherT.Security)) return false;
                if( !DeepComparable.IsExactly(Tag, otherT.Tag)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical id of this artefact
        /// </summary>
        [FhirElement("id", Order=10)]
        [DataMember]
        public Hl7.Fhir.Model.Id IdElement
        {
            get { return _IdElement; }
            set { _IdElement = value; OnPropertyChanged("IdElement"); }
        }
        private Hl7.Fhir.Model.Id _IdElement;
        
        /// <summary>
        /// Logical id of this artefact
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Id
        {
            get { return IdElement != null ? IdElement.Value : null; }
            set
            {
                if(value == null)
                  IdElement = null; 
                else
                  IdElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("Id");
            }
        }
        
        /// <summary>
        /// Metadata about the resource
        /// </summary>
        [FhirElement("meta", Order=20)]
        [DataMember]
        public Hl7.Fhir.Model.Resource.ResourceMetaComponent Meta
        {
            get { return _Meta; }
            set { _Meta = value; OnPropertyChanged("Meta"); }
        }
        private Hl7.Fhir.Model.Resource.ResourceMetaComponent _Meta;
        
        /// <summary>
        /// A set of rules under which this content was created
        /// </summary>
        [FhirElement("implicitRules", Order=30)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ImplicitRulesElement
        {
            get { return _ImplicitRulesElement; }
            set { _ImplicitRulesElement = value; OnPropertyChanged("ImplicitRulesElement"); }
        }
        private Hl7.Fhir.Model.FhirUri _ImplicitRulesElement;
        
        /// <summary>
        /// A set of rules under which this content was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ImplicitRules
        {
            get { return ImplicitRulesElement != null ? ImplicitRulesElement.Value : null; }
            set
            {
                if(value == null)
                  ImplicitRulesElement = null; 
                else
                  ImplicitRulesElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ImplicitRules");
            }
        }
        
        /// <summary>
        /// Language of the resource content
        /// </summary>
        [FhirElement("language", Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.Code LanguageElement
        {
            get { return _LanguageElement; }
            set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
        }
        private Hl7.Fhir.Model.Code _LanguageElement;
        
        /// <summary>
        /// Language of the resource content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Language
        {
            get { return LanguageElement != null ? LanguageElement.Value : null; }
            set
            {
                if(value == null)
                  LanguageElement = null; 
                else
                  LanguageElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Language");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Resource;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(IdElement != null) dest.IdElement = (Hl7.Fhir.Model.Id)IdElement.DeepCopy();
                if(Meta != null) dest.Meta = (Hl7.Fhir.Model.Resource.ResourceMetaComponent)Meta.DeepCopy();
                if(ImplicitRulesElement != null) dest.ImplicitRulesElement = (Hl7.Fhir.Model.FhirUri)ImplicitRulesElement.DeepCopy();
                if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Resource;
            if(otherT == null) return false;
            
            if( !DeepComparable.Matches(IdElement, otherT.IdElement)) return false;
            if( !DeepComparable.Matches(Meta, otherT.Meta)) return false;
            if( !DeepComparable.Matches(ImplicitRulesElement, otherT.ImplicitRulesElement)) return false;
            if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Resource;
            if(otherT == null) return false;
            
            if( !DeepComparable.IsExactly(IdElement, otherT.IdElement)) return false;
            if( !DeepComparable.IsExactly(Meta, otherT.Meta)) return false;
            if( !DeepComparable.IsExactly(ImplicitRulesElement, otherT.ImplicitRulesElement)) return false;
            if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
            
            return true;
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            	PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(property));
        }
    }
    
}
