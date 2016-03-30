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
// Generated for FHIR v1.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Key Object Selection
    /// </summary>
    [FhirType("ImagingExcerpt", IsResource=true)]
    [DataContract]
    public partial class ImagingExcerpt : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImagingExcerpt; } }
        [NotMapped]
        public override string TypeName { get { return "ImagingExcerpt"; } }
        
        /// <summary>
        /// The type of DICOM web technology available
        /// (url: http://hl7.org/fhir/ValueSet/dWebType)
        /// </summary>
        [FhirEnumeration("dWebType")]
        public enum dWebType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/dWebType)
            /// </summary>
            [EnumLiteral("WADO-RS"), Description("WADO-RS")]
            WADORS,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/dWebType)
            /// </summary>
            [EnumLiteral("WADO-URI"), Description("WADO-URI")]
            WADOURI,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/dWebType)
            /// </summary>
            [EnumLiteral("IID"), Description("IID")]
            IID,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/dWebType)
            /// </summary>
            [EnumLiteral("WADO-WS"), Description("WADO-WS")]
            WADOWS,
        }

        [FhirType("StudyComponent")]
        [DataContract]
        public partial class StudyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StudyComponent"; } }
            
            /// <summary>
            /// Study instance UID
            /// </summary>
            [FhirElement("uid", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Study instance UID
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
            /// Reference to ImagingStudy
            /// </summary>
            [FhirElement("imagingStudy", InSummary=true, Order=50)]
            [References("ImagingStudy")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ImagingStudy
            {
                get { return _ImagingStudy; }
                set { _ImagingStudy = value; OnPropertyChanged("ImagingStudy"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ImagingStudy;
            
            /// <summary>
            /// Dicom web access
            /// </summary>
            [FhirElement("dicom", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingExcerpt.DicomComponent> Dicom
            {
                get { if(_Dicom==null) _Dicom = new List<Hl7.Fhir.Model.ImagingExcerpt.DicomComponent>(); return _Dicom; }
                set { _Dicom = value; OnPropertyChanged("Dicom"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingExcerpt.DicomComponent> _Dicom;
            
            /// <summary>
            /// Viewable format
            /// </summary>
            [FhirElement("viewable", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingExcerpt.ViewableComponent> Viewable
            {
                get { if(_Viewable==null) _Viewable = new List<Hl7.Fhir.Model.ImagingExcerpt.ViewableComponent>(); return _Viewable; }
                set { _Viewable = value; OnPropertyChanged("Viewable"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingExcerpt.ViewableComponent> _Viewable;
            
            /// <summary>
            /// Series identity of the selected instances
            /// </summary>
            [FhirElement("series", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingExcerpt.SeriesComponent> Series
            {
                get { if(_Series==null) _Series = new List<Hl7.Fhir.Model.ImagingExcerpt.SeriesComponent>(); return _Series; }
                set { _Series = value; OnPropertyChanged("Series"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingExcerpt.SeriesComponent> _Series;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StudyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(ImagingStudy != null) dest.ImagingStudy = (Hl7.Fhir.Model.ResourceReference)ImagingStudy.DeepCopy();
                    if(Dicom != null) dest.Dicom = new List<Hl7.Fhir.Model.ImagingExcerpt.DicomComponent>(Dicom.DeepCopy());
                    if(Viewable != null) dest.Viewable = new List<Hl7.Fhir.Model.ImagingExcerpt.ViewableComponent>(Viewable.DeepCopy());
                    if(Series != null) dest.Series = new List<Hl7.Fhir.Model.ImagingExcerpt.SeriesComponent>(Series.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StudyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StudyComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(ImagingStudy, otherT.ImagingStudy)) return false;
                if( !DeepComparable.Matches(Dicom, otherT.Dicom)) return false;
                if( !DeepComparable.Matches(Viewable, otherT.Viewable)) return false;
                if( !DeepComparable.Matches(Series, otherT.Series)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StudyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(ImagingStudy, otherT.ImagingStudy)) return false;
                if( !DeepComparable.IsExactly(Dicom, otherT.Dicom)) return false;
                if( !DeepComparable.IsExactly(Viewable, otherT.Viewable)) return false;
                if( !DeepComparable.IsExactly(Series, otherT.Series)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DicomComponent")]
        [DataContract]
        public partial class DicomComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DicomComponent"; } }
            
            /// <summary>
            /// WADO-RS | WADO-URI | IID | WADO-WS
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType> _TypeElement;
            
            /// <summary>
            /// WADO-RS | WADO-URI | IID | WADO-WS
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImagingExcerpt.dWebType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Retrieve study URL
            /// </summary>
            [FhirElement("url", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve study URL
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DicomComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType>)TypeElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DicomComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DicomComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DicomComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ViewableComponent")]
        [DataContract]
        public partial class ViewableComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ViewableComponent"; } }
            
            /// <summary>
            /// Mime type of the content, with charset etc.
            /// </summary>
            [FhirElement("contentType", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _ContentTypeElement;
            
            /// <summary>
            /// Mime type of the content, with charset etc.
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if(value == null)
                      ContentTypeElement = null; 
                    else
                      ContentTypeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// Height of the image in pixels (photo/video)
            /// </summary>
            [FhirElement("height", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt HeightElement
            {
                get { return _HeightElement; }
                set { _HeightElement = value; OnPropertyChanged("HeightElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _HeightElement;
            
            /// <summary>
            /// Height of the image in pixels (photo/video)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Height
            {
                get { return HeightElement != null ? HeightElement.Value : null; }
                set
                {
                    if(value == null)
                      HeightElement = null; 
                    else
                      HeightElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Height");
                }
            }
            
            /// <summary>
            /// Width of the image in pixels (photo/video)
            /// </summary>
            [FhirElement("width", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt WidthElement
            {
                get { return _WidthElement; }
                set { _WidthElement = value; OnPropertyChanged("WidthElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _WidthElement;
            
            /// <summary>
            /// Width of the image in pixels (photo/video)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Width
            {
                get { return WidthElement != null ? WidthElement.Value : null; }
                set
                {
                    if(value == null)
                      WidthElement = null; 
                    else
                      WidthElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Width");
                }
            }
            
            /// <summary>
            /// Number of frames if > 1 (photo)
            /// </summary>
            [FhirElement("frames", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt FramesElement
            {
                get { return _FramesElement; }
                set { _FramesElement = value; OnPropertyChanged("FramesElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _FramesElement;
            
            /// <summary>
            /// Number of frames if > 1 (photo)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Frames
            {
                get { return FramesElement != null ? FramesElement.Value : null; }
                set
                {
                    if(value == null)
                      FramesElement = null; 
                    else
                      FramesElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Frames");
                }
            }
            
            /// <summary>
            /// Length in seconds (audio / video)
            /// </summary>
            [FhirElement("duration", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt DurationElement
            {
                get { return _DurationElement; }
                set { _DurationElement = value; OnPropertyChanged("DurationElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _DurationElement;
            
            /// <summary>
            /// Length in seconds (audio / video)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Duration
            {
                get { return DurationElement != null ? DurationElement.Value : null; }
                set
                {
                    if(value == null)
                      DurationElement = null; 
                    else
                      DurationElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Duration");
                }
            }
            
            /// <summary>
            /// Number of bytes of content (if url provided)
            /// </summary>
            [FhirElement("size", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt SizeElement
            {
                get { return _SizeElement; }
                set { _SizeElement = value; OnPropertyChanged("SizeElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _SizeElement;
            
            /// <summary>
            /// Number of bytes of content (if url provided)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Size
            {
                get { return SizeElement != null ? SizeElement.Value : null; }
                set
                {
                    if(value == null)
                      SizeElement = null; 
                    else
                      SizeElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Size");
                }
            }
            
            /// <summary>
            /// Label to display in place of the data
            /// </summary>
            [FhirElement("title", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Label to display in place of the data
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
            /// Uri where the data can be found
            /// </summary>
            [FhirElement("url", Order=110)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Uri where the data can be found
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ViewableComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
                    if(HeightElement != null) dest.HeightElement = (Hl7.Fhir.Model.PositiveInt)HeightElement.DeepCopy();
                    if(WidthElement != null) dest.WidthElement = (Hl7.Fhir.Model.PositiveInt)WidthElement.DeepCopy();
                    if(FramesElement != null) dest.FramesElement = (Hl7.Fhir.Model.PositiveInt)FramesElement.DeepCopy();
                    if(DurationElement != null) dest.DurationElement = (Hl7.Fhir.Model.UnsignedInt)DurationElement.DeepCopy();
                    if(SizeElement != null) dest.SizeElement = (Hl7.Fhir.Model.UnsignedInt)SizeElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ViewableComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ViewableComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(HeightElement, otherT.HeightElement)) return false;
                if( !DeepComparable.Matches(WidthElement, otherT.WidthElement)) return false;
                if( !DeepComparable.Matches(FramesElement, otherT.FramesElement)) return false;
                if( !DeepComparable.Matches(DurationElement, otherT.DurationElement)) return false;
                if( !DeepComparable.Matches(SizeElement, otherT.SizeElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ViewableComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(HeightElement, otherT.HeightElement)) return false;
                if( !DeepComparable.IsExactly(WidthElement, otherT.WidthElement)) return false;
                if( !DeepComparable.IsExactly(FramesElement, otherT.FramesElement)) return false;
                if( !DeepComparable.IsExactly(DurationElement, otherT.DurationElement)) return false;
                if( !DeepComparable.IsExactly(SizeElement, otherT.SizeElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SeriesComponent")]
        [DataContract]
        public partial class SeriesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SeriesComponent"; } }
            
            /// <summary>
            /// Series instance UID
            /// </summary>
            [FhirElement("uid", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Series instance UID
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
            /// Dicom web access
            /// </summary>
            [FhirElement("dicom", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingExcerpt.SeriesDicomComponent> Dicom
            {
                get { if(_Dicom==null) _Dicom = new List<Hl7.Fhir.Model.ImagingExcerpt.SeriesDicomComponent>(); return _Dicom; }
                set { _Dicom = value; OnPropertyChanged("Dicom"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingExcerpt.SeriesDicomComponent> _Dicom;
            
            /// <summary>
            /// The selected instance
            /// </summary>
            [FhirElement("instance", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingExcerpt.InstanceComponent> Instance
            {
                get { if(_Instance==null) _Instance = new List<Hl7.Fhir.Model.ImagingExcerpt.InstanceComponent>(); return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingExcerpt.InstanceComponent> _Instance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SeriesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(Dicom != null) dest.Dicom = new List<Hl7.Fhir.Model.ImagingExcerpt.SeriesDicomComponent>(Dicom.DeepCopy());
                    if(Instance != null) dest.Instance = new List<Hl7.Fhir.Model.ImagingExcerpt.InstanceComponent>(Instance.DeepCopy());
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
                if( !DeepComparable.Matches(Dicom, otherT.Dicom)) return false;
                if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SeriesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(Dicom, otherT.Dicom)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SeriesDicomComponent")]
        [DataContract]
        public partial class SeriesDicomComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SeriesDicomComponent"; } }
            
            /// <summary>
            /// WADO-RS | WADO-URI | IID | WADO-WS
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType> _TypeElement;
            
            /// <summary>
            /// WADO-RS | WADO-URI | IID | WADO-WS
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImagingExcerpt.dWebType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Retrieve study URL
            /// </summary>
            [FhirElement("url", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve study URL
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SeriesDicomComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType>)TypeElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SeriesDicomComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SeriesDicomComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SeriesDicomComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("InstanceComponent")]
        [DataContract]
        public partial class InstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InstanceComponent"; } }
            
            /// <summary>
            /// SOP class UID of instance
            /// </summary>
            [FhirElement("sopClass", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid SopClassElement
            {
                get { return _SopClassElement; }
                set { _SopClassElement = value; OnPropertyChanged("SopClassElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _SopClassElement;
            
            /// <summary>
            /// SOP class UID of instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SopClass
            {
                get { return SopClassElement != null ? SopClassElement.Value : null; }
                set
                {
                    if(value == null)
                      SopClassElement = null; 
                    else
                      SopClassElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("SopClass");
                }
            }
            
            /// <summary>
            /// Selected instance UID
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
            /// Selected instance UID
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
            /// Dicom web access
            /// </summary>
            [FhirElement("dicom", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingExcerpt.InstanceDicomComponent> Dicom
            {
                get { if(_Dicom==null) _Dicom = new List<Hl7.Fhir.Model.ImagingExcerpt.InstanceDicomComponent>(); return _Dicom; }
                set { _Dicom = value; OnPropertyChanged("Dicom"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingExcerpt.InstanceDicomComponent> _Dicom;
            
            /// <summary>
            /// Frame reference number
            /// </summary>
            [FhirElement("frameNumbers", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> FrameNumbersElement
            {
                get { if(_FrameNumbersElement==null) _FrameNumbersElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _FrameNumbersElement; }
                set { _FrameNumbersElement = value; OnPropertyChanged("FrameNumbersElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _FrameNumbersElement;
            
            /// <summary>
            /// Frame reference number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> FrameNumbers
            {
                get { return FrameNumbersElement != null ? FrameNumbersElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      FrameNumbersElement = null; 
                    else
                      FrameNumbersElement = new List<Hl7.Fhir.Model.UnsignedInt>(value.Select(elem=>new Hl7.Fhir.Model.UnsignedInt(elem)));
                    OnPropertyChanged("FrameNumbers");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SopClassElement != null) dest.SopClassElement = (Hl7.Fhir.Model.Oid)SopClassElement.DeepCopy();
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(Dicom != null) dest.Dicom = new List<Hl7.Fhir.Model.ImagingExcerpt.InstanceDicomComponent>(Dicom.DeepCopy());
                    if(FrameNumbersElement != null) dest.FrameNumbersElement = new List<Hl7.Fhir.Model.UnsignedInt>(FrameNumbersElement.DeepCopy());
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
                if( !DeepComparable.Matches(SopClassElement, otherT.SopClassElement)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(Dicom, otherT.Dicom)) return false;
                if( !DeepComparable.Matches(FrameNumbersElement, otherT.FrameNumbersElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SopClassElement, otherT.SopClassElement)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(Dicom, otherT.Dicom)) return false;
                if( !DeepComparable.IsExactly(FrameNumbersElement, otherT.FrameNumbersElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("InstanceDicomComponent")]
        [DataContract]
        public partial class InstanceDicomComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InstanceDicomComponent"; } }
            
            /// <summary>
            /// WADO-RS | WADO-URI | IID | WADO-WS
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType> _TypeElement;
            
            /// <summary>
            /// WADO-RS | WADO-URI | IID | WADO-WS
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImagingExcerpt.dWebType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Retrieve study URL
            /// </summary>
            [FhirElement("url", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve study URL
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceDicomComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ImagingExcerpt.dWebType>)TypeElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InstanceDicomComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InstanceDicomComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceDicomComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Instance UID
        /// </summary>
        [FhirElement("uid", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Oid UidElement
        {
            get { return _UidElement; }
            set { _UidElement = value; OnPropertyChanged("UidElement"); }
        }
        
        private Hl7.Fhir.Model.Oid _UidElement;
        
        /// <summary>
        /// Instance UID
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
        /// Patient of the selected objects
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=100)]
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
        /// Time when the imaging object selection was created
        /// </summary>
        [FhirElement("authoringTime", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoringTimeElement
        {
            get { return _AuthoringTimeElement; }
            set { _AuthoringTimeElement = value; OnPropertyChanged("AuthoringTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoringTimeElement;
        
        /// <summary>
        /// Time when the imaging object selection was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoringTime
        {
            get { return AuthoringTimeElement != null ? AuthoringTimeElement.Value : null; }
            set
            {
                if(value == null)
                  AuthoringTimeElement = null; 
                else
                  AuthoringTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoringTime");
            }
        }
        
        /// <summary>
        /// Author (human or machine)
        /// </summary>
        [FhirElement("author", InSummary=true, Order=120)]
        [References("Practitioner","Device","Organization","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Reason for selection
        /// </summary>
        [FhirElement("title", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged("Title"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Title;
        
        /// <summary>
        /// Description text
        /// </summary>
        [FhirElement("description", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Description text
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
        /// Study identity of the selected instances
        /// </summary>
        [FhirElement("study", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImagingExcerpt.StudyComponent> Study
        {
            get { if(_Study==null) _Study = new List<Hl7.Fhir.Model.ImagingExcerpt.StudyComponent>(); return _Study; }
            set { _Study = value; OnPropertyChanged("Study"); }
        }
        
        private List<Hl7.Fhir.Model.ImagingExcerpt.StudyComponent> _Study;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImagingExcerpt;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(AuthoringTimeElement != null) dest.AuthoringTimeElement = (Hl7.Fhir.Model.FhirDateTime)AuthoringTimeElement.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Title != null) dest.Title = (Hl7.Fhir.Model.CodeableConcept)Title.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Study != null) dest.Study = new List<Hl7.Fhir.Model.ImagingExcerpt.StudyComponent>(Study.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ImagingExcerpt());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImagingExcerpt;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(AuthoringTimeElement, otherT.AuthoringTimeElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Title, otherT.Title)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Study, otherT.Study)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImagingExcerpt;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(AuthoringTimeElement, otherT.AuthoringTimeElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Title, otherT.Title)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Study, otherT.Study)) return false;
            
            return true;
        }
        
    }
    
}
