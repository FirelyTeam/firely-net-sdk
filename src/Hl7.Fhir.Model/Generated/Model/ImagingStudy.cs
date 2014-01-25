using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A set of images produced in single study (one or more series of references images)
    /// </summary>
    [FhirType("ImagingStudy", IsResource=true)]
    [DataContract]
    public partial class ImagingStudy : Hl7.Fhir.Model.Resource
    {
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
            [EnumLiteral("ONLINE")]
            ONLINE, // Resources are immediately available,.
            [EnumLiteral("OFFLINE")]
            OFFLINE, // Resources need to be retrieved by manual intervention.
            [EnumLiteral("NEARLINE")]
            NEARLINE, // Resources need to be retrieved from relatively slow media.
            [EnumLiteral("UNAVAILABLE")]
            UNAVAILABLE, // Resources cannot be retrieved.
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
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImagingStudySeriesComponent")]
        [DataContract]
        public partial class ImagingStudySeriesComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Number of this series in overall sequence (0020,0011)
            /// </summary>
            [FhirElement("number", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberElement { get; set; }
            
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
                      NumberElement = new Hl7.Fhir.Model.Integer(value);
                }
            }
            
            /// <summary>
            /// The modality of the instances in the series (0008,0060)
            /// </summary>
            [FhirElement("modality", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingStudy.Modality> ModalityElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Formal identifier for this series (0020,000E)
            /// </summary>
            [FhirElement("uid", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// A description of the series (0008,103E)
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Number of Series Related Instances (0020,1209)
            /// </summary>
            [FhirElement("numberOfInstances", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfInstancesElement { get; set; }
            
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
                      NumberOfInstancesElement = new Hl7.Fhir.Model.Integer(value);
                }
            }
            
            /// <summary>
            /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
            /// </summary>
            [FhirElement("availability", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> AvailabilityElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Retrieve URI (0008,1115 > 0008,1190)
            /// </summary>
            [FhirElement("url", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Body part examined (Map from 0018,0015)
            /// </summary>
            [FhirElement("bodySite", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Coding BodySite { get; set; }
            
            /// <summary>
            /// When the series started
            /// </summary>
            [FhirElement("dateTime", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// A single instance taken from a patient (image or other)
            /// </summary>
            [FhirElement("instance", Order=130)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesInstanceComponent> Instance { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImagingStudySeriesInstanceComponent")]
        [DataContract]
        public partial class ImagingStudySeriesInstanceComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The number of this instance in the series (0020,0013)
            /// </summary>
            [FhirElement("number", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberElement { get; set; }
            
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
                      NumberElement = new Hl7.Fhir.Model.Integer(value);
                }
            }
            
            /// <summary>
            /// Formal identifier for this instance (0008,0018)
            /// </summary>
            [FhirElement("uid", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// DICOM class type (0008,0016)
            /// </summary>
            [FhirElement("sopclass", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid SopclassElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Type of instance (image etc) (0004,1430)
            /// </summary>
            [FhirElement("type", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Description (0070,0080 | 0040,A043 > 0008,0104 | 0042,0010 | 0008,0008)
            /// </summary>
            [FhirElement("title", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// WADO-RS service where instance is available  (0008,1199 > 0008,1190)
            /// </summary>
            [FhirElement("url", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// A FHIR resource with content for this instance
            /// </summary>
            [FhirElement("attachment", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Attachment { get; set; }
            
        }
        
        
        /// <summary>
        /// When the study was performed
        /// </summary>
        [FhirElement("dateTime", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Who the images are of
        /// </summary>
        [FhirElement("subject", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Formal identifier for the study (0020,000D)
        /// </summary>
        [FhirElement("uid", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Oid UidElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Accession Number (0008,0050)
        /// </summary>
        [FhirElement("accessionNo", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier AccessionNo { get; set; }
        
        /// <summary>
        /// Other identifiers for the study (0020,0010)
        /// </summary>
        [FhirElement("identifier", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Order(s) that caused this study to be performed
        /// </summary>
        [FhirElement("order", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Order { get; set; }
        
        /// <summary>
        /// All series.modality if actual acquisition modalities
        /// </summary>
        [FhirElement("modality", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>> Modality_Element { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ImagingStudy.ImagingModality?> Modality_
        {
            get { return Modality_Element != null ? Modality_Element.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  Modality_Element = null; 
                else
                  Modality_Element = new List<Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>>(value.Select(elem=>new Code<Hl7.Fhir.Model.ImagingStudy.ImagingModality>(elem)));
            }
        }
        
        /// <summary>
        /// Referring physician (0008,0090)
        /// </summary>
        [FhirElement("referrer", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referrer { get; set; }
        
        /// <summary>
        /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE (0008,0056)
        /// </summary>
        [FhirElement("availability", Order=150)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ImagingStudy.InstanceAvailability> AvailabilityElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Retrieve URI (0008,1190)
        /// </summary>
        [FhirElement("url", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public System.Uri Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if(value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
            }
        }
        
        /// <summary>
        /// Number of Study Related Series (0020,1206)
        /// </summary>
        [FhirElement("numberOfSeries", Order=170)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Integer NumberOfSeriesElement { get; set; }
        
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
                  NumberOfSeriesElement = new Hl7.Fhir.Model.Integer(value);
            }
        }
        
        /// <summary>
        /// Number of Study Related Instances (0020,1208)
        /// </summary>
        [FhirElement("numberOfInstances", Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Integer NumberOfInstancesElement { get; set; }
        
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
                  NumberOfInstancesElement = new Hl7.Fhir.Model.Integer(value);
            }
        }
        
        /// <summary>
        /// Diagnoses etc with request (0040,1002)
        /// </summary>
        [FhirElement("clinicalInformation", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ClinicalInformationElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Type of procedure performed (0008,1032)
        /// </summary>
        [FhirElement("procedure", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Procedure { get; set; }
        
        /// <summary>
        /// Who interpreted images (0008,1060)
        /// </summary>
        [FhirElement("interpreter", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Interpreter { get; set; }
        
        /// <summary>
        /// Institution-generated description (0008,1030)
        /// </summary>
        [FhirElement("description", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Each study has one or more series of instances
        /// </summary>
        [FhirElement("series", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImagingStudy.ImagingStudySeriesComponent> Series { get; set; }
        
    }
    
}
