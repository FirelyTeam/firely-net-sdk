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
    /// A set of images produced in single study (one or more series of references images)
    /// </summary>
    [FhirType("ImagingStudy", IsResource=true)]
    [DataContract]
    public partial class ImagingStudy : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImagingStudy; } }
        [NotMapped]
        public override string TypeName { get { return "ImagingStudy"; } }
        
        /// <summary>
        /// The status of the ImagingStudy.
        /// (url: http://hl7.org/fhir/ValueSet/imagingstudy-status)
        /// </summary>
        [FhirEnumeration("ImagingStudyStatus")]
        public enum ImagingStudyStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/imagingstudy-status)
            /// </summary>
            [EnumLiteral("registered", "http://hl7.org/fhir/imagingstudy-status"), Description("Registered")]
            Registered,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/imagingstudy-status)
            /// </summary>
            [EnumLiteral("available", "http://hl7.org/fhir/imagingstudy-status"), Description("Available")]
            Available,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/imagingstudy-status)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/imagingstudy-status"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/imagingstudy-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/imagingstudy-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/imagingstudy-status)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/imagingstudy-status"), Description("Unknown")]
            Unknown,
        }

        [FhirType("SeriesComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SeriesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SeriesComponent"; } }
            
            /// <summary>
            /// DICOM Series Instance UID for the series
            /// </summary>
            [FhirElement("uid", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Id _UidElement;
            
            /// <summary>
            /// DICOM Series Instance UID for the series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uid
            {
                get { return UidElement != null ? UidElement.Value : null; }
                set
                {
                    if (value == null)
                        UidElement = null; 
                    else
                        UidElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Uid");
                }
            }
            
            /// <summary>
            /// Numeric identifier of this series
            /// </summary>
            [FhirElement("number", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberElement;
            
            /// <summary>
            /// Numeric identifier of this series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberElement = null; 
                    else
                        NumberElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// The modality of the instances in the series
            /// </summary>
            [FhirElement("modality", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Modality
            {
                get { return _Modality; }
                set { _Modality = value; OnPropertyChanged("Modality"); }
            }
            
            private Hl7.Fhir.Model.Coding _Modality;
            
            /// <summary>
            /// A short human readable summary of the series
            /// </summary>
            [FhirElement("description", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// A short human readable summary of the series
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
            /// Number of Series Related Instances
            /// </summary>
            [FhirElement("numberOfInstances", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement
            {
                get { return _NumberOfInstancesElement; }
                set { _NumberOfInstancesElement = value; OnPropertyChanged("NumberOfInstancesElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberOfInstancesElement;
            
            /// <summary>
            /// Number of Series Related Instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfInstances
            {
                get { return NumberOfInstancesElement != null ? NumberOfInstancesElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberOfInstancesElement = null; 
                    else
                        NumberOfInstancesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("NumberOfInstances");
                }
            }
            
            /// <summary>
            /// Series access endpoint
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=90)]
            [CLSCompliant(false)]
			[References("Endpoint")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Endpoint
            {
                get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
                set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
            
            /// <summary>
            /// Body part examined
            /// </summary>
            [FhirElement("bodySite", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Coding BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.Coding _BodySite;
            
            /// <summary>
            /// Body part laterality
            /// </summary>
            [FhirElement("laterality", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Laterality
            {
                get { return _Laterality; }
                set { _Laterality = value; OnPropertyChanged("Laterality"); }
            }
            
            private Hl7.Fhir.Model.Coding _Laterality;
            
            /// <summary>
            /// Specimen imaged
            /// </summary>
            [FhirElement("specimen", InSummary=true, Order=120)]
            [CLSCompliant(false)]
			[References("Specimen")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Specimen
            {
                get { if(_Specimen==null) _Specimen = new List<Hl7.Fhir.Model.ResourceReference>(); return _Specimen; }
                set { _Specimen = value; OnPropertyChanged("Specimen"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Specimen;
            
            /// <summary>
            /// When the series started
            /// </summary>
            [FhirElement("started", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime StartedElement
            {
                get { return _StartedElement; }
                set { _StartedElement = value; OnPropertyChanged("StartedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _StartedElement;
            
            /// <summary>
            /// When the series started
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Started
            {
                get { return StartedElement != null ? StartedElement.Value : null; }
                set
                {
                    if (value == null)
                        StartedElement = null; 
                    else
                        StartedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Started");
                }
            }
            
            /// <summary>
            /// Who performed the series
            /// </summary>
            [FhirElement("performer", InSummary=true, Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingStudy.PerformerComponent> Performer
            {
                get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ImagingStudy.PerformerComponent>(); return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingStudy.PerformerComponent> _Performer;
            
            /// <summary>
            /// A single SOP instance from the series
            /// </summary>
            [FhirElement("instance", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingStudy.InstanceComponent> Instance
            {
                get { if(_Instance==null) _Instance = new List<Hl7.Fhir.Model.ImagingStudy.InstanceComponent>(); return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingStudy.InstanceComponent> _Instance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SeriesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Id)UidElement.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.UnsignedInt)NumberElement.DeepCopy();
                    if(Modality != null) dest.Modality = (Hl7.Fhir.Model.Coding)Modality.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfInstancesElement.DeepCopy();
                    if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
                    if(Laterality != null) dest.Laterality = (Hl7.Fhir.Model.Coding)Laterality.DeepCopy();
                    if(Specimen != null) dest.Specimen = new List<Hl7.Fhir.Model.ResourceReference>(Specimen.DeepCopy());
                    if(StartedElement != null) dest.StartedElement = (Hl7.Fhir.Model.FhirDateTime)StartedElement.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ImagingStudy.PerformerComponent>(Performer.DeepCopy());
                    if(Instance != null) dest.Instance = new List<Hl7.Fhir.Model.ImagingStudy.InstanceComponent>(Instance.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SeriesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SeriesComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(Modality, otherT.Modality)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
                if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(Laterality, otherT.Laterality)) return false;
                if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
                if( !DeepComparable.Matches(StartedElement, otherT.StartedElement)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SeriesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(Modality, otherT.Modality)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
                if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(Laterality, otherT.Laterality)) return false;
                if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
                if( !DeepComparable.IsExactly(StartedElement, otherT.StartedElement)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UidElement != null) yield return UidElement;
                    if (NumberElement != null) yield return NumberElement;
                    if (Modality != null) yield return Modality;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (NumberOfInstancesElement != null) yield return NumberOfInstancesElement;
                    foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
                    if (BodySite != null) yield return BodySite;
                    if (Laterality != null) yield return Laterality;
                    foreach (var elem in Specimen) { if (elem != null) yield return elem; }
                    if (StartedElement != null) yield return StartedElement;
                    foreach (var elem in Performer) { if (elem != null) yield return elem; }
                    foreach (var elem in Instance) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UidElement != null) yield return new ElementValue("uid", UidElement);
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (Modality != null) yield return new ElementValue("modality", Modality);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (NumberOfInstancesElement != null) yield return new ElementValue("numberOfInstances", NumberOfInstancesElement);
                    foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    if (Laterality != null) yield return new ElementValue("laterality", Laterality);
                    foreach (var elem in Specimen) { if (elem != null) yield return new ElementValue("specimen", elem); }
                    if (StartedElement != null) yield return new ElementValue("started", StartedElement);
                    foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                    foreach (var elem in Instance) { if (elem != null) yield return new ElementValue("instance", elem); }
                }
            }

            
        }
        
        
        [FhirType("PerformerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// Type of performance
            /// </summary>
            [FhirElement("function", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Function
            {
                get { return _Function; }
                set { _Function = value; OnPropertyChanged("Function"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Function;
            
            /// <summary>
            /// Who performed the series
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization","CareTeam","Patient","Device","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Function != null) dest.Function = (Hl7.Fhir.Model.CodeableConcept)Function.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Function, otherT.Function)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Function, otherT.Function)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Function != null) yield return Function;
                    if (Actor != null) yield return Actor;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Function != null) yield return new ElementValue("function", Function);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                }
            }

            
        }
        
        
        [FhirType("InstanceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class InstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InstanceComponent"; } }
            
            /// <summary>
            /// DICOM SOP Instance UID
            /// </summary>
            [FhirElement("uid", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Id _UidElement;
            
            /// <summary>
            /// DICOM SOP Instance UID
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uid
            {
                get { return UidElement != null ? UidElement.Value : null; }
                set
                {
                    if (value == null)
                        UidElement = null; 
                    else
                        UidElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Uid");
                }
            }
            
            /// <summary>
            /// DICOM class type
            /// </summary>
            [FhirElement("sopClass", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding SopClass
            {
                get { return _SopClass; }
                set { _SopClass = value; OnPropertyChanged("SopClass"); }
            }
            
            private Hl7.Fhir.Model.Coding _SopClass;
            
            /// <summary>
            /// The number of this instance in the series
            /// </summary>
            [FhirElement("number", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberElement;
            
            /// <summary>
            /// The number of this instance in the series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberElement = null; 
                    else
                        NumberElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// Description of instance
            /// </summary>
            [FhirElement("title", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Description of instance
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Id)UidElement.DeepCopy();
                    if(SopClass != null) dest.SopClass = (Hl7.Fhir.Model.Coding)SopClass.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.UnsignedInt)NumberElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(SopClass, otherT.SopClass)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(SopClass, otherT.SopClass)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UidElement != null) yield return UidElement;
                    if (SopClass != null) yield return SopClass;
                    if (NumberElement != null) yield return NumberElement;
                    if (TitleElement != null) yield return TitleElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UidElement != null) yield return new ElementValue("uid", UidElement);
                    if (SopClass != null) yield return new ElementValue("sopClass", SopClass);
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Identifiers for the whole study
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// registered | available | cancelled | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ImagingStudy.ImagingStudyStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ImagingStudy.ImagingStudyStatus> _StatusElement;
        
        /// <summary>
        /// registered | available | cancelled | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ImagingStudy.ImagingStudyStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ImagingStudy.ImagingStudyStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// All series modality if actual acquisition modalities
        /// </summary>
        [FhirElement("modality", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Modality
        {
            get { if(_Modality==null) _Modality = new List<Hl7.Fhir.Model.Coding>(); return _Modality; }
            set { _Modality = value; OnPropertyChanged("Modality"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Modality;
        
        /// <summary>
        /// Who or what is the subject of the study
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Patient","Device","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter with which this imaging study is associated
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// When the study was started
        /// </summary>
        [FhirElement("started", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime StartedElement
        {
            get { return _StartedElement; }
            set { _StartedElement = value; OnPropertyChanged("StartedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _StartedElement;
        
        /// <summary>
        /// When the study was started
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Started
        {
            get { return StartedElement != null ? StartedElement.Value : null; }
            set
            {
                if (value == null)
                  StartedElement = null; 
                else
                  StartedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Started");
            }
        }
        
        /// <summary>
        /// Request fulfilled
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("CarePlan","ServiceRequest","Appointment","AppointmentResponse","Task")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Referring physician
        /// </summary>
        [FhirElement("referrer", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referrer
        {
            get { return _Referrer; }
            set { _Referrer = value; OnPropertyChanged("Referrer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Referrer;
        
        /// <summary>
        /// Who interpreted images
        /// </summary>
        [FhirElement("interpreter", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Interpreter
        {
            get { if(_Interpreter==null) _Interpreter = new List<Hl7.Fhir.Model.ResourceReference>(); return _Interpreter; }
            set { _Interpreter = value; OnPropertyChanged("Interpreter"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Interpreter;
        
        /// <summary>
        /// Study access endpoint
        /// </summary>
        [FhirElement("endpoint", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Endpoint")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Endpoint
        {
            get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
            set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
        
        /// <summary>
        /// Number of Study Related Series
        /// </summary>
        [FhirElement("numberOfSeries", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt NumberOfSeriesElement
        {
            get { return _NumberOfSeriesElement; }
            set { _NumberOfSeriesElement = value; OnPropertyChanged("NumberOfSeriesElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _NumberOfSeriesElement;
        
        /// <summary>
        /// Number of Study Related Series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfSeries
        {
            get { return NumberOfSeriesElement != null ? NumberOfSeriesElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NumberOfSeriesElement = null; 
                else
                  NumberOfSeriesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("NumberOfSeries");
            }
        }
        
        /// <summary>
        /// Number of Study Related Instances
        /// </summary>
        [FhirElement("numberOfInstances", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement
        {
            get { return _NumberOfInstancesElement; }
            set { _NumberOfInstancesElement = value; OnPropertyChanged("NumberOfInstancesElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _NumberOfInstancesElement;
        
        /// <summary>
        /// Number of Study Related Instances
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfInstances
        {
            get { return NumberOfInstancesElement != null ? NumberOfInstancesElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NumberOfInstancesElement = null; 
                else
                  NumberOfInstancesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("NumberOfInstances");
            }
        }
        
        /// <summary>
        /// The performed Procedure reference
        /// </summary>
        [FhirElement("procedureReference", InSummary=true, Order=210)]
        [CLSCompliant(false)]
		[References("Procedure")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ProcedureReference
        {
            get { return _ProcedureReference; }
            set { _ProcedureReference = value; OnPropertyChanged("ProcedureReference"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ProcedureReference;
        
        /// <summary>
        /// The performed procedure code
        /// </summary>
        [FhirElement("procedureCode", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ProcedureCode
        {
            get { if(_ProcedureCode==null) _ProcedureCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProcedureCode; }
            set { _ProcedureCode = value; OnPropertyChanged("ProcedureCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ProcedureCode;
        
        /// <summary>
        /// Where ImagingStudy occurred
        /// </summary>
        [FhirElement("location", InSummary=true, Order=230)]
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
        /// Why the study was requested
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Why was study performed
        /// </summary>
        [FhirElement("reasonReference", InSummary=true, Order=250)]
        [CLSCompliant(false)]
		[References("Condition","Observation","Media","DiagnosticReport","DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// User-defined comments
        /// </summary>
        [FhirElement("note", InSummary=true, Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Institution-generated description
        /// </summary>
        [FhirElement("description", InSummary=true, Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Institution-generated description
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
        /// Each study has one or more series of instances
        /// </summary>
        [FhirElement("series", InSummary=true, Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImagingStudy.SeriesComponent> Series
        {
            get { if(_Series==null) _Series = new List<Hl7.Fhir.Model.ImagingStudy.SeriesComponent>(); return _Series; }
            set { _Series = value; OnPropertyChanged("Series"); }
        }
        
        private List<Hl7.Fhir.Model.ImagingStudy.SeriesComponent> _Series;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImagingStudy;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ImagingStudy.ImagingStudyStatus>)StatusElement.DeepCopy();
                if(Modality != null) dest.Modality = new List<Hl7.Fhir.Model.Coding>(Modality.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(StartedElement != null) dest.StartedElement = (Hl7.Fhir.Model.FhirDateTime)StartedElement.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Referrer != null) dest.Referrer = (Hl7.Fhir.Model.ResourceReference)Referrer.DeepCopy();
                if(Interpreter != null) dest.Interpreter = new List<Hl7.Fhir.Model.ResourceReference>(Interpreter.DeepCopy());
                if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
                if(NumberOfSeriesElement != null) dest.NumberOfSeriesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfSeriesElement.DeepCopy();
                if(NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfInstancesElement.DeepCopy();
                if(ProcedureReference != null) dest.ProcedureReference = (Hl7.Fhir.Model.ResourceReference)ProcedureReference.DeepCopy();
                if(ProcedureCode != null) dest.ProcedureCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProcedureCode.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Series != null) dest.Series = new List<Hl7.Fhir.Model.ImagingStudy.SeriesComponent>(Series.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ImagingStudy());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImagingStudy;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Modality, otherT.Modality)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(StartedElement, otherT.StartedElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Referrer, otherT.Referrer)) return false;
            if( !DeepComparable.Matches(Interpreter, otherT.Interpreter)) return false;
            if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.Matches(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
            if( !DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
            if( !DeepComparable.Matches(ProcedureReference, otherT.ProcedureReference)) return false;
            if( !DeepComparable.Matches(ProcedureCode, otherT.ProcedureCode)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Series, otherT.Series)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImagingStudy;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Modality, otherT.Modality)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(StartedElement, otherT.StartedElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Referrer, otherT.Referrer)) return false;
            if( !DeepComparable.IsExactly(Interpreter, otherT.Interpreter)) return false;
            if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.IsExactly(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
            if( !DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
            if( !DeepComparable.IsExactly(ProcedureReference, otherT.ProcedureReference)) return false;
            if( !DeepComparable.IsExactly(ProcedureCode, otherT.ProcedureCode)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Series, otherT.Series)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in Modality) { if (elem != null) yield return elem; }
				if (Subject != null) yield return Subject;
				if (Encounter != null) yield return Encounter;
				if (StartedElement != null) yield return StartedElement;
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				if (Referrer != null) yield return Referrer;
				foreach (var elem in Interpreter) { if (elem != null) yield return elem; }
				foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
				if (NumberOfSeriesElement != null) yield return NumberOfSeriesElement;
				if (NumberOfInstancesElement != null) yield return NumberOfInstancesElement;
				if (ProcedureReference != null) yield return ProcedureReference;
				foreach (var elem in ProcedureCode) { if (elem != null) yield return elem; }
				if (Location != null) yield return Location;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				if (DescriptionElement != null) yield return DescriptionElement;
				foreach (var elem in Series) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Modality) { if (elem != null) yield return new ElementValue("modality", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (StartedElement != null) yield return new ElementValue("started", StartedElement);
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (Referrer != null) yield return new ElementValue("referrer", Referrer);
                foreach (var elem in Interpreter) { if (elem != null) yield return new ElementValue("interpreter", elem); }
                foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                if (NumberOfSeriesElement != null) yield return new ElementValue("numberOfSeries", NumberOfSeriesElement);
                if (NumberOfInstancesElement != null) yield return new ElementValue("numberOfInstances", NumberOfInstancesElement);
                if (ProcedureReference != null) yield return new ElementValue("procedureReference", ProcedureReference);
                foreach (var elem in ProcedureCode) { if (elem != null) yield return new ElementValue("procedureCode", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Series) { if (elem != null) yield return new ElementValue("series", elem); }
            }
        }

    }
    
}
