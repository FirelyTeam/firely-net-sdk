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
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
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
        /// Type of acquired image data in the instance
        /// </summary>
        [FhirEnumeration("ImagingModality")]
        public enum ImagingModality
        {
            [EnumLiteral("AR")]
            AR,
            [EnumLiteral("BMD")]
            BMD,
            [EnumLiteral("BDUS")]
            BDUS,
            [EnumLiteral("EPS")]
            EPS,
            [EnumLiteral("CR")]
            CR,
            [EnumLiteral("CT")]
            CT,
            [EnumLiteral("DX")]
            DX,
            [EnumLiteral("ECG")]
            ECG,
            [EnumLiteral("ES")]
            ES,
            [EnumLiteral("XC")]
            XC,
            [EnumLiteral("GM")]
            GM,
            [EnumLiteral("HD")]
            HD,
            [EnumLiteral("IO")]
            IO,
            [EnumLiteral("IVOCT")]
            IVOCT,
            [EnumLiteral("IVUS")]
            IVUS,
            [EnumLiteral("KER")]
            KER,
            [EnumLiteral("LEN")]
            LEN,
            [EnumLiteral("MR")]
            MR,
            [EnumLiteral("MG")]
            MG,
            [EnumLiteral("NM")]
            NM,
            [EnumLiteral("OAM")]
            OAM,
            [EnumLiteral("OCT")]
            OCT,
            [EnumLiteral("OPM")]
            OPM,
            [EnumLiteral("OP")]
            OP,
            [EnumLiteral("OPR")]
            OPR,
            [EnumLiteral("OPT")]
            OPT,
            [EnumLiteral("OPV")]
            OPV,
            [EnumLiteral("PX")]
            PX,
            [EnumLiteral("PT")]
            PT,
            [EnumLiteral("RF")]
            RF,
            [EnumLiteral("RG")]
            RG,
            [EnumLiteral("SM")]
            SM,
            [EnumLiteral("SRF")]
            SRF,
            [EnumLiteral("US")]
            US,
            [EnumLiteral("VA")]
            VA,
            [EnumLiteral("XA")]
            XA,
        }
        
        /// <summary>
        /// Availability of the resource
        /// </summary>
        [FhirEnumeration("InstanceAvailability")]
        public enum InstanceAvailability
        {
            /// <summary>
            /// Resources are immediately available,.
            /// </summary>
            [EnumLiteral("ONLINE")]
            ONLINE,
            /// <summary>
            /// Resources need to be retrieved by manual intervention.
            /// </summary>
            [EnumLiteral("OFFLINE")]
            OFFLINE,
            /// <summary>
            /// Resources need to be retrieved from relatively slow media.
            /// </summary>
            [EnumLiteral("NEARLINE")]
            NEARLINE,
            /// <summary>
            /// Resources cannot be retrieved.
            /// </summary>
            [EnumLiteral("UNAVAILABLE")]
            UNAVAILABLE,
        }
        
        /// <summary>
        /// Type of data in the instance
        /// </summary>
        [FhirEnumeration("Modality")]
        public enum Modality
        {
            [EnumLiteral("AR")]
            AR,
            [EnumLiteral("AU")]
            AU,
            [EnumLiteral("BDUS")]
            BDUS,
            [EnumLiteral("BI")]
            BI,
            [EnumLiteral("BMD")]
            BMD,
            [EnumLiteral("CR")]
            CR,
            [EnumLiteral("CT")]
            CT,
            [EnumLiteral("DG")]
            DG,
            [EnumLiteral("DX")]
            DX,
            [EnumLiteral("ECG")]
            ECG,
            [EnumLiteral("EPS")]
            EPS,
            [EnumLiteral("ES")]
            ES,
            [EnumLiteral("GM")]
            GM,
            [EnumLiteral("HC")]
            HC,
            [EnumLiteral("HD")]
            HD,
            [EnumLiteral("IO")]
            IO,
            [EnumLiteral("IVOCT")]
            IVOCT,
            [EnumLiteral("IVUS")]
            IVUS,
            [EnumLiteral("KER")]
            KER,
            [EnumLiteral("KO")]
            KO,
            [EnumLiteral("LEN")]
            LEN,
            [EnumLiteral("LS")]
            LS,
            [EnumLiteral("MG")]
            MG,
            [EnumLiteral("MR")]
            MR,
            [EnumLiteral("NM")]
            NM,
            [EnumLiteral("OAM")]
            OAM,
            [EnumLiteral("OCT")]
            OCT,
            [EnumLiteral("OP")]
            OP,
            [EnumLiteral("OPM")]
            OPM,
            [EnumLiteral("OPT")]
            OPT,
            [EnumLiteral("OPV")]
            OPV,
            [EnumLiteral("OT")]
            OT,
            [EnumLiteral("PR")]
            PR,
            [EnumLiteral("PT")]
            PT,
            [EnumLiteral("PX")]
            PX,
            [EnumLiteral("REG")]
            REG,
            [EnumLiteral("RF")]
            RF,
            [EnumLiteral("RG")]
            RG,
            [EnumLiteral("RTDOSE")]
            RTDOSE,
            [EnumLiteral("RTIMAGE")]
            RTIMAGE,
            [EnumLiteral("RTPLAN")]
            RTPLAN,
            [EnumLiteral("RTRECORD")]
            RTRECORD,
            [EnumLiteral("RTSTRUCT")]
            RTSTRUCT,
            [EnumLiteral("SEG")]
            SEG,
            [EnumLiteral("SM")]
            SM,
            [EnumLiteral("SMR")]
            SMR,
            [EnumLiteral("SR")]
            SR,
            [EnumLiteral("SRF")]
            SRF,
            [EnumLiteral("TG")]
            TG,
            [EnumLiteral("US")]
            US,
            [EnumLiteral("VA")]
            VA,
            [EnumLiteral("XA")]
            XA,
            [EnumLiteral("XC")]
            XC,
        }
        
        [FhirType("ImagingStudySeriesComponent")]
        [DataContract]
        public partial class ImagingStudySeriesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ImagingStudySeriesComponent"; } }
            
            /// <summary>
            /// Numeric identifier of this series (0020,0011)
            /// </summary>
            [FhirElement("number", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberElement;
            
            /// <summary>
            /// Numeric identifier of this series (0020,0011)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if(value == null)
                      NumberElement = null; 
                    else
                      NumberElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// The modality of the instances in the series (0008,0060)
            /// </summary>
            [FhirElement("modality", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingStudy.Modality> ModalityElement
            {
                get { return _ModalityElement; }
                set { _ModalityElement = value; OnPropertyChanged("ModalityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImagingStudy.Modality> _ModalityElement;
            
            /// <summary>
            /// The modality of the instances in the series (0008,0060)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImagingStudy.Modality? Modality
            {
                get { return ModalityElement != null ? ModalityElement.Value : null; }
                set
                {
                    if(value == null)
                      ModalityElement = null; 
                    else
                      ModalityElement = new Code<Hl7.Fhir.Model.ImagingStudy.Modality>(value);
                    OnPropertyChanged("Modality");
                }
            }
            
            /// <summary>
            /// Formal identifier for this series (0020,000E)
            /// </summary>
            [FhirElement("uid", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Formal identifier for this series (0020,000E)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uid
            {
                get { return UidElement != null ? UidElement.Value : null; }
                set
                {
                    if(value == null)
                      UidElement = null; 
                    else
                      UidElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("Uid");
                }
            }
            
            /// <summary>
            /// A description of the series (0008,103E)
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
            /// A description of the series (0008,103E)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Number of Series Related Instances (0020,1209)
            /// </summary>
            [FhirElement("numberOfInstances", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement
            {
                get { return _NumberOfInstancesElement; }
                set { _NumberOfInstancesElement = value; OnPropertyChanged("NumberOfInstancesElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberOfInstancesElement;
            
            /// <summary>
            /// Number of Series Related Instances (0020,1209)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfInstances
            {
                get { return NumberOfInstancesElement != null ? NumberOfInstancesElement.Value : null; }
                set
                {
                    if(value == null)
                      NumberOfInstancesElement = null; 
                    else
                      NumberOfInstancesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("NumberOfInstances");
                }
            }
            
            /// <summary>
            /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
            /// </summary>
            [FhirElement("availability", InSummary=true, Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> AvailabilityElement
            {
                get { return _AvailabilityElement; }
                set { _AvailabilityElement = value; OnPropertyChanged("AvailabilityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> _AvailabilityElement;
            
            /// <summary>
            /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImagingStudy.InstanceAvailability? Availability
            {
                get { return AvailabilityElement != null ? AvailabilityElement.Value : null; }
                set
                {
                    if(value == null)
                      AvailabilityElement = null; 
                    else
                      AvailabilityElement = new Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>(value);
                    OnPropertyChanged("Availability");
                }
            }
            
            /// <summary>
            /// Retrieve URI (0008,1115 &gt; 0008,1190)
            /// </summary>
            [FhirElement("url", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve URI (0008,1115 &gt; 0008,1190)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// Body part examined (Map from 0018,0015)
            /// </summary>
            [FhirElement("bodySite", InSummary=true, Order=110)]
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
            [FhirElement("laterality", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Laterality
            {
                get { return _Laterality; }
                set { _Laterality = value; OnPropertyChanged("Laterality"); }
            }
            
            private Hl7.Fhir.Model.Coding _Laterality;
            
            /// <summary>
            /// When the series started
            /// </summary>
            [FhirElement("dateTime", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateTimeElement
            {
                get { return _DateTimeElement; }
                set { _DateTimeElement = value; OnPropertyChanged("DateTimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateTimeElement;
            
            /// <summary>
            /// When the series started
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DateTime
            {
                get { return DateTimeElement != null ? DateTimeElement.Value : null; }
                set
                {
                    if(value == null)
                      DateTimeElement = null; 
                    else
                      DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("DateTime");
                }
            }
            
            /// <summary>
            /// A single instance taken from a patient (image or other)
            /// </summary>
            [FhirElement("instance", InSummary=true, Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent> Instance
            {
                get { if(_Instance==null) _Instance = new List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent>(); return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent> _Instance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImagingStudySeriesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.UnsignedInt)NumberElement.DeepCopy();
                    if(ModalityElement != null) dest.ModalityElement = (Code<Hl7.Fhir.Model.ImagingStudy.Modality>)ModalityElement.DeepCopy();
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfInstancesElement.DeepCopy();
                    if(AvailabilityElement != null) dest.AvailabilityElement = (Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>)AvailabilityElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
                    if(Laterality != null) dest.Laterality = (Hl7.Fhir.Model.Coding)Laterality.DeepCopy();
                    if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
                    if(Instance != null) dest.Instance = new List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent>(Instance.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImagingStudySeriesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImagingStudySeriesComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(ModalityElement, otherT.ModalityElement)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
                if( !DeepComparable.Matches(AvailabilityElement, otherT.AvailabilityElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(Laterality, otherT.Laterality)) return false;
                if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImagingStudySeriesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(ModalityElement, otherT.ModalityElement)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
                if( !DeepComparable.IsExactly(AvailabilityElement, otherT.AvailabilityElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(Laterality, otherT.Laterality)) return false;
                if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ImagingStudySeriesInstanceComponent")]
        [DataContract]
        public partial class ImagingStudySeriesInstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ImagingStudySeriesInstanceComponent"; } }
            
            /// <summary>
            /// The number of this instance in the series (0020,0013)
            /// </summary>
            [FhirElement("number", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberElement;
            
            /// <summary>
            /// The number of this instance in the series (0020,0013)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if(value == null)
                      NumberElement = null; 
                    else
                      NumberElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// Formal identifier for this instance (0008,0018)
            /// </summary>
            [FhirElement("uid", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Formal identifier for this instance (0008,0018)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uid
            {
                get { return UidElement != null ? UidElement.Value : null; }
                set
                {
                    if(value == null)
                      UidElement = null; 
                    else
                      UidElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("Uid");
                }
            }
            
            /// <summary>
            /// DICOM class type (0008,0016)
            /// </summary>
            [FhirElement("sopclass", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid SopclassElement
            {
                get { return _SopclassElement; }
                set { _SopclassElement = value; OnPropertyChanged("SopclassElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _SopclassElement;
            
            /// <summary>
            /// DICOM class type (0008,0016)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Sopclass
            {
                get { return SopclassElement != null ? SopclassElement.Value : null; }
                set
                {
                    if(value == null)
                      SopclassElement = null; 
                    else
                      SopclassElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("Sopclass");
                }
            }
            
            /// <summary>
            /// Type of instance (image etc) (0004,1430)
            /// </summary>
            [FhirElement("type", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// Type of instance (image etc) (0004,1430)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Description (0070,0080 | 0040,A043 &gt; 0008,0104 | 0042,0010 | 0008,0008)
            /// </summary>
            [FhirElement("title", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Description (0070,0080 | 0040,A043 &gt; 0008,0104 | 0042,0010 | 0008,0008)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Title
            {
                get { return TitleElement != null ? TitleElement.Value : null; }
                set
                {
                    if(value == null)
                      TitleElement = null; 
                    else
                      TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
            
            /// <summary>
            /// Content of the instance
            /// </summary>
            [FhirElement("content", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> Content
            {
                get { if(_Content==null) _Content = new List<Hl7.Fhir.Model.Attachment>(); return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImagingStudySeriesInstanceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.UnsignedInt)NumberElement.DeepCopy();
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(SopclassElement != null) dest.SopclassElement = (Hl7.Fhir.Model.Oid)SopclassElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(Content != null) dest.Content = new List<Hl7.Fhir.Model.Attachment>(Content.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImagingStudySeriesInstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImagingStudySeriesInstanceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(SopclassElement, otherT.SopclassElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImagingStudySeriesInstanceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(SopclassElement, otherT.SopclassElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// When the study was started (0008,0020)+(0008,0030)
        /// </summary>
        [FhirElement("started", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime StartedElement
        {
            get { return _StartedElement; }
            set { _StartedElement = value; OnPropertyChanged("StartedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _StartedElement;
        
        /// <summary>
        /// When the study was started (0008,0020)+(0008,0030)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Started
        {
            get { return StartedElement != null ? StartedElement.Value : null; }
            set
            {
                if(value == null)
                  StartedElement = null; 
                else
                  StartedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Started");
            }
        }
        
        /// <summary>
        /// Who the images are of
        /// </summary>
        [FhirElement("patient", Order=100)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Formal identifier for the study (0020,000D)
        /// </summary>
        [FhirElement("uid", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Oid UidElement
        {
            get { return _UidElement; }
            set { _UidElement = value; OnPropertyChanged("UidElement"); }
        }
        
        private Hl7.Fhir.Model.Oid _UidElement;
        
        /// <summary>
        /// Formal identifier for the study (0020,000D)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Uid
        {
            get { return UidElement != null ? UidElement.Value : null; }
            set
            {
                if(value == null)
                  UidElement = null; 
                else
                  UidElement = new Hl7.Fhir.Model.Oid(value);
                OnPropertyChanged("Uid");
            }
        }
        
        /// <summary>
        /// Accession Number (0008,0050)
        /// </summary>
        [FhirElement("accession", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Accession
        {
            get { return _Accession; }
            set { _Accession = value; OnPropertyChanged("Accession"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Accession;
        
        /// <summary>
        /// Other identifiers for the study (0020,0010)
        /// </summary>
        [FhirElement("identifier", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Order(s) that caused this study to be performed
        /// </summary>
        [FhirElement("order", Order=140)]
        [References("DiagnosticOrder")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Order
        {
            get { if(_Order==null) _Order = new List<Hl7.Fhir.Model.ResourceReference>(); return _Order; }
            set { _Order = value; OnPropertyChanged("Order"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Order;
        
        /// <summary>
        /// All series.modality if actual acquisition modalities
        /// </summary>
        [FhirElement("modalityList", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>> ModalityListElement
        {
            get { if(_ModalityListElement==null) _ModalityListElement = new List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>>(); return _ModalityListElement; }
            set { _ModalityListElement = value; OnPropertyChanged("ModalityListElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>> _ModalityListElement;
        
        /// <summary>
        /// All series.modality if actual acquisition modalities
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ImagingStudy.ImagingModality?> ModalityList
        {
            get { return ModalityListElement != null ? ModalityListElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  ModalityListElement = null; 
                else
                  ModalityListElement = new List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>>(value.Select(elem=>new Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>(elem)));
                OnPropertyChanged("ModalityList");
            }
        }
        
        /// <summary>
        /// Referring physician (0008,0090)
        /// </summary>
        [FhirElement("referrer", Order=160)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referrer
        {
            get { return _Referrer; }
            set { _Referrer = value; OnPropertyChanged("Referrer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Referrer;
        
        /// <summary>
        /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
        /// </summary>
        [FhirElement("availability", Order=170)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> AvailabilityElement
        {
            get { return _AvailabilityElement; }
            set { _AvailabilityElement = value; OnPropertyChanged("AvailabilityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> _AvailabilityElement;
        
        /// <summary>
        /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ImagingStudy.InstanceAvailability? Availability
        {
            get { return AvailabilityElement != null ? AvailabilityElement.Value : null; }
            set
            {
                if(value == null)
                  AvailabilityElement = null; 
                else
                  AvailabilityElement = new Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>(value);
                OnPropertyChanged("Availability");
            }
        }
        
        /// <summary>
        /// Retrieve URI (0008,1190)
        /// </summary>
        [FhirElement("url", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Retrieve URI (0008,1190)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if(value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Number of Study Related Series (0020,1206)
        /// </summary>
        [FhirElement("numberOfSeries", Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt NumberOfSeriesElement
        {
            get { return _NumberOfSeriesElement; }
            set { _NumberOfSeriesElement = value; OnPropertyChanged("NumberOfSeriesElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _NumberOfSeriesElement;
        
        /// <summary>
        /// Number of Study Related Series (0020,1206)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfSeries
        {
            get { return NumberOfSeriesElement != null ? NumberOfSeriesElement.Value : null; }
            set
            {
                if(value == null)
                  NumberOfSeriesElement = null; 
                else
                  NumberOfSeriesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("NumberOfSeries");
            }
        }
        
        /// <summary>
        /// Number of Study Related Instances (0020,1208)
        /// </summary>
        [FhirElement("numberOfInstances", Order=200)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement
        {
            get { return _NumberOfInstancesElement; }
            set { _NumberOfInstancesElement = value; OnPropertyChanged("NumberOfInstancesElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _NumberOfInstancesElement;
        
        /// <summary>
        /// Number of Study Related Instances (0020,1208)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfInstances
        {
            get { return NumberOfInstancesElement != null ? NumberOfInstancesElement.Value : null; }
            set
            {
                if(value == null)
                  NumberOfInstancesElement = null; 
                else
                  NumberOfInstancesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("NumberOfInstances");
            }
        }
        
        /// <summary>
        /// Diagnoses etc with request (0040,1002)
        /// </summary>
        [FhirElement("clinicalInformation", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ClinicalInformationElement
        {
            get { return _ClinicalInformationElement; }
            set { _ClinicalInformationElement = value; OnPropertyChanged("ClinicalInformationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ClinicalInformationElement;
        
        /// <summary>
        /// Diagnoses etc with request (0040,1002)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ClinicalInformation
        {
            get { return ClinicalInformationElement != null ? ClinicalInformationElement.Value : null; }
            set
            {
                if(value == null)
                  ClinicalInformationElement = null; 
                else
                  ClinicalInformationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ClinicalInformation");
            }
        }
        
        /// <summary>
        /// Type of procedure performed (0008,1032)
        /// </summary>
        [FhirElement("procedure", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Procedure
        {
            get { if(_Procedure==null) _Procedure = new List<Hl7.Fhir.Model.Coding>(); return _Procedure; }
            set { _Procedure = value; OnPropertyChanged("Procedure"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Procedure;
        
        /// <summary>
        /// Who interpreted images (0008,1060)
        /// </summary>
        [FhirElement("interpreter", Order=230)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Interpreter
        {
            get { return _Interpreter; }
            set { _Interpreter = value; OnPropertyChanged("Interpreter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Interpreter;
        
        /// <summary>
        /// Institution-generated description (0008,1030)
        /// </summary>
        [FhirElement("description", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Institution-generated description (0008,1030)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Each study has one or more series of instances
        /// </summary>
        [FhirElement("series", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent> Series
        {
            get { if(_Series==null) _Series = new List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent>(); return _Series; }
            set { _Series = value; OnPropertyChanged("Series"); }
        }
        
        private List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent> _Series;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImagingStudy;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(StartedElement != null) dest.StartedElement = (Hl7.Fhir.Model.FhirDateTime)StartedElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                if(Accession != null) dest.Accession = (Hl7.Fhir.Model.Identifier)Accession.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Order != null) dest.Order = new List<Hl7.Fhir.Model.ResourceReference>(Order.DeepCopy());
                if(ModalityListElement != null) dest.ModalityListElement = new List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>>(ModalityListElement.DeepCopy());
                if(Referrer != null) dest.Referrer = (Hl7.Fhir.Model.ResourceReference)Referrer.DeepCopy();
                if(AvailabilityElement != null) dest.AvailabilityElement = (Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability>)AvailabilityElement.DeepCopy();
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(NumberOfSeriesElement != null) dest.NumberOfSeriesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfSeriesElement.DeepCopy();
                if(NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfInstancesElement.DeepCopy();
                if(ClinicalInformationElement != null) dest.ClinicalInformationElement = (Hl7.Fhir.Model.FhirString)ClinicalInformationElement.DeepCopy();
                if(Procedure != null) dest.Procedure = new List<Hl7.Fhir.Model.Coding>(Procedure.DeepCopy());
                if(Interpreter != null) dest.Interpreter = (Hl7.Fhir.Model.ResourceReference)Interpreter.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Series != null) dest.Series = new List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent>(Series.DeepCopy());
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
            if( !DeepComparable.Matches(StartedElement, otherT.StartedElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.Matches(Accession, otherT.Accession)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Order, otherT.Order)) return false;
            if( !DeepComparable.Matches(ModalityListElement, otherT.ModalityListElement)) return false;
            if( !DeepComparable.Matches(Referrer, otherT.Referrer)) return false;
            if( !DeepComparable.Matches(AvailabilityElement, otherT.AvailabilityElement)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
            if( !DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
            if( !DeepComparable.Matches(ClinicalInformationElement, otherT.ClinicalInformationElement)) return false;
            if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.Matches(Interpreter, otherT.Interpreter)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Series, otherT.Series)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImagingStudy;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(StartedElement, otherT.StartedElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.IsExactly(Accession, otherT.Accession)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Order, otherT.Order)) return false;
            if( !DeepComparable.IsExactly(ModalityListElement, otherT.ModalityListElement)) return false;
            if( !DeepComparable.IsExactly(Referrer, otherT.Referrer)) return false;
            if( !DeepComparable.IsExactly(AvailabilityElement, otherT.AvailabilityElement)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
            if( !DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
            if( !DeepComparable.IsExactly(ClinicalInformationElement, otherT.ClinicalInformationElement)) return false;
            if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.IsExactly(Interpreter, otherT.Interpreter)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Series, otherT.Series)) return false;
            
            return true;
        }
        
    }
    
}
