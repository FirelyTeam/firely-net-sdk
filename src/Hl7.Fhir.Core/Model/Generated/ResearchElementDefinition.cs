﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A population, intervention, or exposure definition
    /// </summary>
    [FhirType("ResearchElementDefinition", IsResource=true)]
    [DataContract]
    public partial class ResearchElementDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ResearchElementDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ResearchElementDefinition"; } }
        
        /// <summary>
        /// The possible types of research elements (E.g. Population, Exposure, Outcome).
        /// (url: http://hl7.org/fhir/ValueSet/research-element-type)
        /// </summary>
        [FhirEnumeration("ResearchElementType")]
        public enum ResearchElementType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-element-type)
            /// </summary>
            [EnumLiteral("population", "http://hl7.org/fhir/research-element-type"), Description("Population")]
            Population,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-element-type)
            /// </summary>
            [EnumLiteral("exposure", "http://hl7.org/fhir/research-element-type"), Description("Exposure")]
            Exposure,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-element-type)
            /// </summary>
            [EnumLiteral("outcome", "http://hl7.org/fhir/research-element-type"), Description("Outcome")]
            Outcome,
        }

        [FhirType("CharacteristicComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CharacteristicComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CharacteristicComponent"; } }
            
            /// <summary>
            /// What code or expression defines members?
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Canonical),typeof(Expression),typeof(DataRequirement))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            
            private Hl7.Fhir.Model.Element _Definition;
            
            /// <summary>
            /// What code/value pairs define members?
            /// </summary>
            [FhirElement("usageContext", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<UsageContext> UsageContext
            {
                get { if(_UsageContext==null) _UsageContext = new List<UsageContext>(); return _UsageContext; }
                set { _UsageContext = value; OnPropertyChanged("UsageContext"); }
            }
            
            private List<UsageContext> _UsageContext;
            
            /// <summary>
            /// Whether the characteristic includes or excludes members
            /// </summary>
            [FhirElement("exclude", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ExcludeElement
            {
                get { return _ExcludeElement; }
                set { _ExcludeElement = value; OnPropertyChanged("ExcludeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ExcludeElement;
            
            /// <summary>
            /// Whether the characteristic includes or excludes members
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Exclude
            {
                get { return ExcludeElement != null ? ExcludeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ExcludeElement = null; 
                    else
                        ExcludeElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Exclude");
                }
            }
            
            /// <summary>
            /// What unit is the outcome described in?
            /// </summary>
            [FhirElement("unitOfMeasure", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept UnitOfMeasure
            {
                get { return _UnitOfMeasure; }
                set { _UnitOfMeasure = value; OnPropertyChanged("UnitOfMeasure"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _UnitOfMeasure;
            
            /// <summary>
            /// What time period does the study cover
            /// </summary>
            [FhirElement("studyEffectiveDescription", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString StudyEffectiveDescriptionElement
            {
                get { return _StudyEffectiveDescriptionElement; }
                set { _StudyEffectiveDescriptionElement = value; OnPropertyChanged("StudyEffectiveDescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _StudyEffectiveDescriptionElement;
            
            /// <summary>
            /// What time period does the study cover
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string StudyEffectiveDescription
            {
                get { return StudyEffectiveDescriptionElement != null ? StudyEffectiveDescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        StudyEffectiveDescriptionElement = null; 
                    else
                        StudyEffectiveDescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("StudyEffectiveDescription");
                }
            }
            
            /// <summary>
            /// What time period does the study cover
            /// </summary>
            [FhirElement("studyEffective", Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Duration),typeof(Hl7.Fhir.Model.Timing))]
            [DataMember]
            public Hl7.Fhir.Model.Element StudyEffective
            {
                get { return _StudyEffective; }
                set { _StudyEffective = value; OnPropertyChanged("StudyEffective"); }
            }
            
            private Hl7.Fhir.Model.Element _StudyEffective;
            
            /// <summary>
            /// Observation time from study start
            /// </summary>
            [FhirElement("studyEffectiveTimeFromStart", Order=100)]
            [DataMember]
            public Duration StudyEffectiveTimeFromStart
            {
                get { return _StudyEffectiveTimeFromStart; }
                set { _StudyEffectiveTimeFromStart = value; OnPropertyChanged("StudyEffectiveTimeFromStart"); }
            }
            
            private Duration _StudyEffectiveTimeFromStart;
            
            /// <summary>
            /// mean | median | mean-of-mean | mean-of-median | median-of-mean | median-of-median
            /// </summary>
            [FhirElement("studyEffectiveGroupMeasure", Order=110)]
            [DataMember]
            public Code<Hl7.Fhir.Model.GroupMeasure> StudyEffectiveGroupMeasureElement
            {
                get { return _StudyEffectiveGroupMeasureElement; }
                set { _StudyEffectiveGroupMeasureElement = value; OnPropertyChanged("StudyEffectiveGroupMeasureElement"); }
            }
            
            private Code<Hl7.Fhir.Model.GroupMeasure> _StudyEffectiveGroupMeasureElement;
            
            /// <summary>
            /// mean | median | mean-of-mean | mean-of-median | median-of-mean | median-of-median
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.GroupMeasure? StudyEffectiveGroupMeasure
            {
                get { return StudyEffectiveGroupMeasureElement != null ? StudyEffectiveGroupMeasureElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StudyEffectiveGroupMeasureElement = null; 
                    else
                        StudyEffectiveGroupMeasureElement = new Code<Hl7.Fhir.Model.GroupMeasure>(value);
                    OnPropertyChanged("StudyEffectiveGroupMeasure");
                }
            }
            
            /// <summary>
            /// What time period do participants cover
            /// </summary>
            [FhirElement("participantEffectiveDescription", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ParticipantEffectiveDescriptionElement
            {
                get { return _ParticipantEffectiveDescriptionElement; }
                set { _ParticipantEffectiveDescriptionElement = value; OnPropertyChanged("ParticipantEffectiveDescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ParticipantEffectiveDescriptionElement;
            
            /// <summary>
            /// What time period do participants cover
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ParticipantEffectiveDescription
            {
                get { return ParticipantEffectiveDescriptionElement != null ? ParticipantEffectiveDescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        ParticipantEffectiveDescriptionElement = null; 
                    else
                        ParticipantEffectiveDescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ParticipantEffectiveDescription");
                }
            }
            
            /// <summary>
            /// What time period do participants cover
            /// </summary>
            [FhirElement("participantEffective", Order=130, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Duration),typeof(Hl7.Fhir.Model.Timing))]
            [DataMember]
            public Hl7.Fhir.Model.Element ParticipantEffective
            {
                get { return _ParticipantEffective; }
                set { _ParticipantEffective = value; OnPropertyChanged("ParticipantEffective"); }
            }
            
            private Hl7.Fhir.Model.Element _ParticipantEffective;
            
            /// <summary>
            /// Observation time from study start
            /// </summary>
            [FhirElement("participantEffectiveTimeFromStart", Order=140)]
            [DataMember]
            public Duration ParticipantEffectiveTimeFromStart
            {
                get { return _ParticipantEffectiveTimeFromStart; }
                set { _ParticipantEffectiveTimeFromStart = value; OnPropertyChanged("ParticipantEffectiveTimeFromStart"); }
            }
            
            private Duration _ParticipantEffectiveTimeFromStart;
            
            /// <summary>
            /// mean | median | mean-of-mean | mean-of-median | median-of-mean | median-of-median
            /// </summary>
            [FhirElement("participantEffectiveGroupMeasure", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.GroupMeasure> ParticipantEffectiveGroupMeasureElement
            {
                get { return _ParticipantEffectiveGroupMeasureElement; }
                set { _ParticipantEffectiveGroupMeasureElement = value; OnPropertyChanged("ParticipantEffectiveGroupMeasureElement"); }
            }
            
            private Code<Hl7.Fhir.Model.GroupMeasure> _ParticipantEffectiveGroupMeasureElement;
            
            /// <summary>
            /// mean | median | mean-of-mean | mean-of-median | median-of-mean | median-of-median
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.GroupMeasure? ParticipantEffectiveGroupMeasure
            {
                get { return ParticipantEffectiveGroupMeasureElement != null ? ParticipantEffectiveGroupMeasureElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ParticipantEffectiveGroupMeasureElement = null; 
                    else
                        ParticipantEffectiveGroupMeasureElement = new Code<Hl7.Fhir.Model.GroupMeasure>(value);
                    OnPropertyChanged("ParticipantEffectiveGroupMeasure");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CharacteristicComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.Element)Definition.DeepCopy();
                    if(UsageContext != null) dest.UsageContext = new List<UsageContext>(UsageContext.DeepCopy());
                    if(ExcludeElement != null) dest.ExcludeElement = (Hl7.Fhir.Model.FhirBoolean)ExcludeElement.DeepCopy();
                    if(UnitOfMeasure != null) dest.UnitOfMeasure = (Hl7.Fhir.Model.CodeableConcept)UnitOfMeasure.DeepCopy();
                    if(StudyEffectiveDescriptionElement != null) dest.StudyEffectiveDescriptionElement = (Hl7.Fhir.Model.FhirString)StudyEffectiveDescriptionElement.DeepCopy();
                    if(StudyEffective != null) dest.StudyEffective = (Hl7.Fhir.Model.Element)StudyEffective.DeepCopy();
                    if(StudyEffectiveTimeFromStart != null) dest.StudyEffectiveTimeFromStart = (Duration)StudyEffectiveTimeFromStart.DeepCopy();
                    if(StudyEffectiveGroupMeasureElement != null) dest.StudyEffectiveGroupMeasureElement = (Code<Hl7.Fhir.Model.GroupMeasure>)StudyEffectiveGroupMeasureElement.DeepCopy();
                    if(ParticipantEffectiveDescriptionElement != null) dest.ParticipantEffectiveDescriptionElement = (Hl7.Fhir.Model.FhirString)ParticipantEffectiveDescriptionElement.DeepCopy();
                    if(ParticipantEffective != null) dest.ParticipantEffective = (Hl7.Fhir.Model.Element)ParticipantEffective.DeepCopy();
                    if(ParticipantEffectiveTimeFromStart != null) dest.ParticipantEffectiveTimeFromStart = (Duration)ParticipantEffectiveTimeFromStart.DeepCopy();
                    if(ParticipantEffectiveGroupMeasureElement != null) dest.ParticipantEffectiveGroupMeasureElement = (Code<Hl7.Fhir.Model.GroupMeasure>)ParticipantEffectiveGroupMeasureElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CharacteristicComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CharacteristicComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
                if( !DeepComparable.Matches(UsageContext, otherT.UsageContext)) return false;
                if( !DeepComparable.Matches(ExcludeElement, otherT.ExcludeElement)) return false;
                if( !DeepComparable.Matches(UnitOfMeasure, otherT.UnitOfMeasure)) return false;
                if( !DeepComparable.Matches(StudyEffectiveDescriptionElement, otherT.StudyEffectiveDescriptionElement)) return false;
                if( !DeepComparable.Matches(StudyEffective, otherT.StudyEffective)) return false;
                if( !DeepComparable.Matches(StudyEffectiveTimeFromStart, otherT.StudyEffectiveTimeFromStart)) return false;
                if( !DeepComparable.Matches(StudyEffectiveGroupMeasureElement, otherT.StudyEffectiveGroupMeasureElement)) return false;
                if( !DeepComparable.Matches(ParticipantEffectiveDescriptionElement, otherT.ParticipantEffectiveDescriptionElement)) return false;
                if( !DeepComparable.Matches(ParticipantEffective, otherT.ParticipantEffective)) return false;
                if( !DeepComparable.Matches(ParticipantEffectiveTimeFromStart, otherT.ParticipantEffectiveTimeFromStart)) return false;
                if( !DeepComparable.Matches(ParticipantEffectiveGroupMeasureElement, otherT.ParticipantEffectiveGroupMeasureElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CharacteristicComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
                if( !DeepComparable.IsExactly(UsageContext, otherT.UsageContext)) return false;
                if( !DeepComparable.IsExactly(ExcludeElement, otherT.ExcludeElement)) return false;
                if( !DeepComparable.IsExactly(UnitOfMeasure, otherT.UnitOfMeasure)) return false;
                if( !DeepComparable.IsExactly(StudyEffectiveDescriptionElement, otherT.StudyEffectiveDescriptionElement)) return false;
                if( !DeepComparable.IsExactly(StudyEffective, otherT.StudyEffective)) return false;
                if( !DeepComparable.IsExactly(StudyEffectiveTimeFromStart, otherT.StudyEffectiveTimeFromStart)) return false;
                if( !DeepComparable.IsExactly(StudyEffectiveGroupMeasureElement, otherT.StudyEffectiveGroupMeasureElement)) return false;
                if( !DeepComparable.IsExactly(ParticipantEffectiveDescriptionElement, otherT.ParticipantEffectiveDescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ParticipantEffective, otherT.ParticipantEffective)) return false;
                if( !DeepComparable.IsExactly(ParticipantEffectiveTimeFromStart, otherT.ParticipantEffectiveTimeFromStart)) return false;
                if( !DeepComparable.IsExactly(ParticipantEffectiveGroupMeasureElement, otherT.ParticipantEffectiveGroupMeasureElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Definition != null) yield return Definition;
                    foreach (var elem in UsageContext) { if (elem != null) yield return elem; }
                    if (ExcludeElement != null) yield return ExcludeElement;
                    if (UnitOfMeasure != null) yield return UnitOfMeasure;
                    if (StudyEffectiveDescriptionElement != null) yield return StudyEffectiveDescriptionElement;
                    if (StudyEffective != null) yield return StudyEffective;
                    if (StudyEffectiveTimeFromStart != null) yield return StudyEffectiveTimeFromStart;
                    if (StudyEffectiveGroupMeasureElement != null) yield return StudyEffectiveGroupMeasureElement;
                    if (ParticipantEffectiveDescriptionElement != null) yield return ParticipantEffectiveDescriptionElement;
                    if (ParticipantEffective != null) yield return ParticipantEffective;
                    if (ParticipantEffectiveTimeFromStart != null) yield return ParticipantEffectiveTimeFromStart;
                    if (ParticipantEffectiveGroupMeasureElement != null) yield return ParticipantEffectiveGroupMeasureElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Definition != null) yield return new ElementValue("definition", Definition);
                    foreach (var elem in UsageContext) { if (elem != null) yield return new ElementValue("usageContext", elem); }
                    if (ExcludeElement != null) yield return new ElementValue("exclude", ExcludeElement);
                    if (UnitOfMeasure != null) yield return new ElementValue("unitOfMeasure", UnitOfMeasure);
                    if (StudyEffectiveDescriptionElement != null) yield return new ElementValue("studyEffectiveDescription", StudyEffectiveDescriptionElement);
                    if (StudyEffective != null) yield return new ElementValue("studyEffective", StudyEffective);
                    if (StudyEffectiveTimeFromStart != null) yield return new ElementValue("studyEffectiveTimeFromStart", StudyEffectiveTimeFromStart);
                    if (StudyEffectiveGroupMeasureElement != null) yield return new ElementValue("studyEffectiveGroupMeasure", StudyEffectiveGroupMeasureElement);
                    if (ParticipantEffectiveDescriptionElement != null) yield return new ElementValue("participantEffectiveDescription", ParticipantEffectiveDescriptionElement);
                    if (ParticipantEffective != null) yield return new ElementValue("participantEffective", ParticipantEffective);
                    if (ParticipantEffectiveTimeFromStart != null) yield return new ElementValue("participantEffectiveTimeFromStart", ParticipantEffectiveTimeFromStart);
                    if (ParticipantEffectiveGroupMeasureElement != null) yield return new ElementValue("participantEffectiveGroupMeasure", ParticipantEffectiveGroupMeasureElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this research element definition, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this research element definition, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the research element definition
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Business version of the research element definition
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the research element definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this research element definition (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this research element definition (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name for this research element definition (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this research element definition (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// Title for use in informal contexts
        /// </summary>
        [FhirElement("shortTitle", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ShortTitleElement
        {
            get { return _ShortTitleElement; }
            set { _ShortTitleElement = value; OnPropertyChanged("ShortTitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ShortTitleElement;
        
        /// <summary>
        /// Title for use in informal contexts
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ShortTitle
        {
            get { return ShortTitleElement != null ? ShortTitleElement.Value : null; }
            set
            {
                if (value == null)
                  ShortTitleElement = null; 
                else
                  ShortTitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ShortTitle");
            }
        }
        
        /// <summary>
        /// Subordinate title of the ResearchElementDefinition
        /// </summary>
        [FhirElement("subtitle", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SubtitleElement
        {
            get { return _SubtitleElement; }
            set { _SubtitleElement = value; OnPropertyChanged("SubtitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SubtitleElement;
        
        /// <summary>
        /// Subordinate title of the ResearchElementDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Subtitle
        {
            get { return SubtitleElement != null ? SubtitleElement.Value : null; }
            set
            {
                if (value == null)
                  SubtitleElement = null; 
                else
                  SubtitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Subtitle");
            }
        }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// E.g. Patient, Practitioner, RelatedPerson, Organization, Location, Device
        /// </summary>
        [FhirElement("subject", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.Element _Subject;
        
        /// <summary>
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the research element definition
        /// </summary>
        [FhirElement("description", InSummary=true, Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// Used for footnotes or explanatory notes
        /// </summary>
        [FhirElement("comment", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> CommentElement
        {
            get { if(_CommentElement==null) _CommentElement = new List<Hl7.Fhir.Model.FhirString>(); return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _CommentElement;
        
        /// <summary>
        /// Used for footnotes or explanatory notes
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Comment
        {
            get { return CommentElement != null ? CommentElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  CommentElement = null; 
                else
                  CommentElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for research element definition (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this research element definition is defined
        /// </summary>
        [FhirElement("purpose", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Purpose;
        
        /// <summary>
        /// Describes the clinical usage of the ResearchElementDefinition
        /// </summary>
        [FhirElement("usage", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UsageElement
        {
            get { return _UsageElement; }
            set { _UsageElement = value; OnPropertyChanged("UsageElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _UsageElement;
        
        /// <summary>
        /// Describes the clinical usage of the ResearchElementDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Usage
        {
            get { return UsageElement != null ? UsageElement.Value : null; }
            set
            {
                if (value == null)
                  UsageElement = null; 
                else
                  UsageElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Usage");
            }
        }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// When the research element definition was approved by publisher
        /// </summary>
        [FhirElement("approvalDate", Order=290)]
        [DataMember]
        public Hl7.Fhir.Model.Date ApprovalDateElement
        {
            get { return _ApprovalDateElement; }
            set { _ApprovalDateElement = value; OnPropertyChanged("ApprovalDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _ApprovalDateElement;
        
        /// <summary>
        /// When the research element definition was approved by publisher
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ApprovalDate
        {
            get { return ApprovalDateElement != null ? ApprovalDateElement.Value : null; }
            set
            {
                if (value == null)
                  ApprovalDateElement = null; 
                else
                  ApprovalDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("ApprovalDate");
            }
        }
        
        /// <summary>
        /// When the research element definition was last reviewed
        /// </summary>
        [FhirElement("lastReviewDate", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.Date LastReviewDateElement
        {
            get { return _LastReviewDateElement; }
            set { _LastReviewDateElement = value; OnPropertyChanged("LastReviewDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LastReviewDateElement;
        
        /// <summary>
        /// When the research element definition was last reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastReviewDate
        {
            get { return LastReviewDateElement != null ? LastReviewDateElement.Value : null; }
            set
            {
                if (value == null)
                  LastReviewDateElement = null; 
                else
                  LastReviewDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("LastReviewDate");
            }
        }
        
        /// <summary>
        /// When the research element definition is expected to be used
        /// </summary>
        [FhirElement("effectivePeriod", InSummary=true, Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.Period EffectivePeriod
        {
            get { return _EffectivePeriod; }
            set { _EffectivePeriod = value; OnPropertyChanged("EffectivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _EffectivePeriod;
        
        /// <summary>
        /// The category of the ResearchElementDefinition, such as Education, Treatment, Assessment, etc.
        /// </summary>
        [FhirElement("topic", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Topic
        {
            get { if(_Topic==null) _Topic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Topic;
        
        /// <summary>
        /// Who authored the content
        /// </summary>
        [FhirElement("author", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Author
        {
            get { if(_Author==null) _Author = new List<ContactDetail>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<ContactDetail> _Author;
        
        /// <summary>
        /// Who edited the content
        /// </summary>
        [FhirElement("editor", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Editor
        {
            get { if(_Editor==null) _Editor = new List<ContactDetail>(); return _Editor; }
            set { _Editor = value; OnPropertyChanged("Editor"); }
        }
        
        private List<ContactDetail> _Editor;
        
        /// <summary>
        /// Who reviewed the content
        /// </summary>
        [FhirElement("reviewer", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Reviewer
        {
            get { if(_Reviewer==null) _Reviewer = new List<ContactDetail>(); return _Reviewer; }
            set { _Reviewer = value; OnPropertyChanged("Reviewer"); }
        }
        
        private List<ContactDetail> _Reviewer;
        
        /// <summary>
        /// Who endorsed the content
        /// </summary>
        [FhirElement("endorser", Order=360)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Endorser
        {
            get { if(_Endorser==null) _Endorser = new List<ContactDetail>(); return _Endorser; }
            set { _Endorser = value; OnPropertyChanged("Endorser"); }
        }
        
        private List<ContactDetail> _Endorser;
        
        /// <summary>
        /// Additional documentation, citations, etc.
        /// </summary>
        [FhirElement("relatedArtifact", Order=370)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedArtifact> RelatedArtifact
        {
            get { if(_RelatedArtifact==null) _RelatedArtifact = new List<RelatedArtifact>(); return _RelatedArtifact; }
            set { _RelatedArtifact = value; OnPropertyChanged("RelatedArtifact"); }
        }
        
        private List<RelatedArtifact> _RelatedArtifact;
        
        /// <summary>
        /// Logic used by the ResearchElementDefinition
        /// </summary>
        [FhirElement("library", Order=380)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> LibraryElement
        {
            get { if(_LibraryElement==null) _LibraryElement = new List<Hl7.Fhir.Model.Canonical>(); return _LibraryElement; }
            set { _LibraryElement = value; OnPropertyChanged("LibraryElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _LibraryElement;
        
        /// <summary>
        /// Logic used by the ResearchElementDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Library
        {
            get { return LibraryElement != null ? LibraryElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  LibraryElement = null; 
                else
                  LibraryElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Library");
            }
        }
        
        /// <summary>
        /// population | exposure | outcome
        /// </summary>
        [FhirElement("type", InSummary=true, Order=390)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ResearchElementDefinition.ResearchElementType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ResearchElementDefinition.ResearchElementType> _TypeElement;
        
        /// <summary>
        /// population | exposure | outcome
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ResearchElementDefinition.ResearchElementType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.ResearchElementDefinition.ResearchElementType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// dichotomous | continuous | descriptive
        /// </summary>
        [FhirElement("variableType", Order=400)]
        [DataMember]
        public Code<Hl7.Fhir.Model.EvidenceVariableType> VariableTypeElement
        {
            get { return _VariableTypeElement; }
            set { _VariableTypeElement = value; OnPropertyChanged("VariableTypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.EvidenceVariableType> _VariableTypeElement;
        
        /// <summary>
        /// dichotomous | continuous | descriptive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.EvidenceVariableType? VariableType
        {
            get { return VariableTypeElement != null ? VariableTypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  VariableTypeElement = null; 
                else
                  VariableTypeElement = new Code<Hl7.Fhir.Model.EvidenceVariableType>(value);
                OnPropertyChanged("VariableType");
            }
        }
        
        /// <summary>
        /// What defines the members of the research element
        /// </summary>
        [FhirElement("characteristic", InSummary=true, Order=410)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResearchElementDefinition.CharacteristicComponent> Characteristic
        {
            get { if(_Characteristic==null) _Characteristic = new List<Hl7.Fhir.Model.ResearchElementDefinition.CharacteristicComponent>(); return _Characteristic; }
            set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
        }
        
        private List<Hl7.Fhir.Model.ResearchElementDefinition.CharacteristicComponent> _Characteristic;
        

        public static ElementDefinition.ConstraintComponent ResearchElementDefinition_RED_0 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "red-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(ResearchElementDefinition_RED_0);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ResearchElementDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(ShortTitleElement != null) dest.ShortTitleElement = (Hl7.Fhir.Model.FhirString)ShortTitleElement.DeepCopy();
                if(SubtitleElement != null) dest.SubtitleElement = (Hl7.Fhir.Model.FhirString)SubtitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.Element)Subject.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(CommentElement != null) dest.CommentElement = new List<Hl7.Fhir.Model.FhirString>(CommentElement.DeepCopy());
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(Topic != null) dest.Topic = new List<Hl7.Fhir.Model.CodeableConcept>(Topic.DeepCopy());
                if(Author != null) dest.Author = new List<ContactDetail>(Author.DeepCopy());
                if(Editor != null) dest.Editor = new List<ContactDetail>(Editor.DeepCopy());
                if(Reviewer != null) dest.Reviewer = new List<ContactDetail>(Reviewer.DeepCopy());
                if(Endorser != null) dest.Endorser = new List<ContactDetail>(Endorser.DeepCopy());
                if(RelatedArtifact != null) dest.RelatedArtifact = new List<RelatedArtifact>(RelatedArtifact.DeepCopy());
                if(LibraryElement != null) dest.LibraryElement = new List<Hl7.Fhir.Model.Canonical>(LibraryElement.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ResearchElementDefinition.ResearchElementType>)TypeElement.DeepCopy();
                if(VariableTypeElement != null) dest.VariableTypeElement = (Code<Hl7.Fhir.Model.EvidenceVariableType>)VariableTypeElement.DeepCopy();
                if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.ResearchElementDefinition.CharacteristicComponent>(Characteristic.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ResearchElementDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ResearchElementDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(ShortTitleElement, otherT.ShortTitleElement)) return false;
            if( !DeepComparable.Matches(SubtitleElement, otherT.SubtitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Editor, otherT.Editor)) return false;
            if( !DeepComparable.Matches(Reviewer, otherT.Reviewer)) return false;
            if( !DeepComparable.Matches(Endorser, otherT.Endorser)) return false;
            if( !DeepComparable.Matches(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.Matches(LibraryElement, otherT.LibraryElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(VariableTypeElement, otherT.VariableTypeElement)) return false;
            if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ResearchElementDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(ShortTitleElement, otherT.ShortTitleElement)) return false;
            if( !DeepComparable.IsExactly(SubtitleElement, otherT.SubtitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Editor, otherT.Editor)) return false;
            if( !DeepComparable.IsExactly(Reviewer, otherT.Reviewer)) return false;
            if( !DeepComparable.IsExactly(Endorser, otherT.Endorser)) return false;
            if( !DeepComparable.IsExactly(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.IsExactly(LibraryElement, otherT.LibraryElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(VariableTypeElement, otherT.VariableTypeElement)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
				if (ShortTitleElement != null) yield return ShortTitleElement;
				if (SubtitleElement != null) yield return SubtitleElement;
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (Subject != null) yield return Subject;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (Description != null) yield return Description;
				foreach (var elem in CommentElement) { if (elem != null) yield return elem; }
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Purpose != null) yield return Purpose;
				if (UsageElement != null) yield return UsageElement;
				if (Copyright != null) yield return Copyright;
				if (ApprovalDateElement != null) yield return ApprovalDateElement;
				if (LastReviewDateElement != null) yield return LastReviewDateElement;
				if (EffectivePeriod != null) yield return EffectivePeriod;
				foreach (var elem in Topic) { if (elem != null) yield return elem; }
				foreach (var elem in Author) { if (elem != null) yield return elem; }
				foreach (var elem in Editor) { if (elem != null) yield return elem; }
				foreach (var elem in Reviewer) { if (elem != null) yield return elem; }
				foreach (var elem in Endorser) { if (elem != null) yield return elem; }
				foreach (var elem in RelatedArtifact) { if (elem != null) yield return elem; }
				foreach (var elem in LibraryElement) { if (elem != null) yield return elem; }
				if (TypeElement != null) yield return TypeElement;
				if (VariableTypeElement != null) yield return VariableTypeElement;
				foreach (var elem in Characteristic) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (ShortTitleElement != null) yield return new ElementValue("shortTitle", ShortTitleElement);
                if (SubtitleElement != null) yield return new ElementValue("subtitle", SubtitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in CommentElement) { if (elem != null) yield return new ElementValue("comment", elem); }
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (UsageElement != null) yield return new ElementValue("usage", UsageElement);
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (ApprovalDateElement != null) yield return new ElementValue("approvalDate", ApprovalDateElement);
                if (LastReviewDateElement != null) yield return new ElementValue("lastReviewDate", LastReviewDateElement);
                if (EffectivePeriod != null) yield return new ElementValue("effectivePeriod", EffectivePeriod);
                foreach (var elem in Topic) { if (elem != null) yield return new ElementValue("topic", elem); }
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
                foreach (var elem in Editor) { if (elem != null) yield return new ElementValue("editor", elem); }
                foreach (var elem in Reviewer) { if (elem != null) yield return new ElementValue("reviewer", elem); }
                foreach (var elem in Endorser) { if (elem != null) yield return new ElementValue("endorser", elem); }
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return new ElementValue("relatedArtifact", elem); }
                foreach (var elem in LibraryElement) { if (elem != null) yield return new ElementValue("library", elem); }
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (VariableTypeElement != null) yield return new ElementValue("variableType", VariableTypeElement);
                foreach (var elem in Characteristic) { if (elem != null) yield return new ElementValue("characteristic", elem); }
            }
        }

    }
    
}
