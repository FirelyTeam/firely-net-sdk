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
    /// Key Object Selection
    /// </summary>
    [FhirType("ImagingObjectSelection", IsResource=true)]
    [DataContract]
    public partial class ImagingObjectSelection : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImagingObjectSelection; } }
        [NotMapped]
        public override string TypeName { get { return "ImagingObjectSelection"; } }
        
        /// <summary>
        /// The document title code of key object selection
        /// </summary>
        [FhirEnumeration("KOStitle")]
        public enum KOStitle
        {
            /// <summary>
            /// Of Interest.
            /// </summary>
            [EnumLiteral("113000")]
            N113000,
            /// <summary>
            /// Rejected for Quality Reasons.
            /// </summary>
            [EnumLiteral("113001")]
            N113001,
            /// <summary>
            /// For Referring Provider.
            /// </summary>
            [EnumLiteral("113002")]
            N113002,
            /// <summary>
            /// For Surgery.
            /// </summary>
            [EnumLiteral("113003")]
            N113003,
            /// <summary>
            /// For Teaching.
            /// </summary>
            [EnumLiteral("113004")]
            N113004,
            /// <summary>
            /// For Conference.
            /// </summary>
            [EnumLiteral("113005")]
            N113005,
            /// <summary>
            /// For Therapy.
            /// </summary>
            [EnumLiteral("113006")]
            N113006,
            /// <summary>
            /// For Patient.
            /// </summary>
            [EnumLiteral("113007")]
            N113007,
            /// <summary>
            /// For Peer Review.
            /// </summary>
            [EnumLiteral("113008")]
            N113008,
            /// <summary>
            /// For Research.
            /// </summary>
            [EnumLiteral("113009")]
            N113009,
            /// <summary>
            /// Quality Issue.
            /// </summary>
            [EnumLiteral("113010")]
            N113010,
            /// <summary>
            /// Best In Set.
            /// </summary>
            [EnumLiteral("113013")]
            N113013,
            /// <summary>
            /// For Printing.
            /// </summary>
            [EnumLiteral("113018")]
            N113018,
            /// <summary>
            /// For Report Attachment.
            /// </summary>
            [EnumLiteral("113020")]
            N113020,
            /// <summary>
            /// For Litigation.
            /// </summary>
            [EnumLiteral("113021")]
            N113021,
            /// <summary>
            /// Manifest.
            /// </summary>
            [EnumLiteral("113030")]
            N113030,
            /// <summary>
            /// Signed Manifest.
            /// </summary>
            [EnumLiteral("113031")]
            N113031,
            /// <summary>
            /// Complete Study Content.
            /// </summary>
            [EnumLiteral("113032")]
            N113032,
            /// <summary>
            /// Signed Complete Study Content.
            /// </summary>
            [EnumLiteral("113033")]
            N113033,
            /// <summary>
            /// Complete Acquisition Content.
            /// </summary>
            [EnumLiteral("113034")]
            N113034,
            /// <summary>
            /// Signed Complete Acquisition Content.
            /// </summary>
            [EnumLiteral("113035")]
            N113035,
            /// <summary>
            /// Group of Frames for Display.
            /// </summary>
            [EnumLiteral("113036")]
            N113036,
            /// <summary>
            /// Rejected for Patient Safety Reasons.
            /// </summary>
            [EnumLiteral("113037")]
            N113037,
            /// <summary>
            /// Incorrect Modality Worklist Entry.
            /// </summary>
            [EnumLiteral("113038")]
            N113038,
            /// <summary>
            /// Data Retention Policy Expired.
            /// </summary>
            [EnumLiteral("113039")]
            N113039,
        }
        
        [FhirType("StudyComponent")]
        [DataContract]
        public partial class StudyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StudyComponent"; } }
            
            /// <summary>
            /// Study instance uid
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
            /// Study instance uid
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
            /// Retrieve URL
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve URL
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
            /// Series identity of the selected instances
            /// </summary>
            [FhirElement("series", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingObjectSelection.SeriesComponent> Series
            {
                get { if(_Series==null) _Series = new List<Hl7.Fhir.Model.ImagingObjectSelection.SeriesComponent>(); return _Series; }
                set { _Series = value; OnPropertyChanged("Series"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingObjectSelection.SeriesComponent> _Series;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StudyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(Series != null) dest.Series = new List<Hl7.Fhir.Model.ImagingObjectSelection.SeriesComponent>(Series.DeepCopy());
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
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(Series, otherT.Series)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StudyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(Series, otherT.Series)) return false;
                
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
            /// SOP class uid of instance
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
            /// SOP class uid of instance
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
            /// Uid of the selected instance
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
            /// Uid of the selected instance
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
            /// Retrieve URL
            /// </summary>
            [FhirElement("url", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve URL
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
            /// The frame set
            /// </summary>
            [FhirElement("frames", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingObjectSelection.FramesComponent> Frames
            {
                get { if(_Frames==null) _Frames = new List<Hl7.Fhir.Model.ImagingObjectSelection.FramesComponent>(); return _Frames; }
                set { _Frames = value; OnPropertyChanged("Frames"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingObjectSelection.FramesComponent> _Frames;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SopClassElement != null) dest.SopClassElement = (Hl7.Fhir.Model.Oid)SopClassElement.DeepCopy();
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(Frames != null) dest.Frames = new List<Hl7.Fhir.Model.ImagingObjectSelection.FramesComponent>(Frames.DeepCopy());
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
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(Frames, otherT.Frames)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SopClassElement, otherT.SopClassElement)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(Frames, otherT.Frames)) return false;
                
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
            /// Series instance uid
            /// </summary>
            [FhirElement("uid", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Series instance uid
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
            /// Retrieve URL
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve URL
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
            /// The selected instance
            /// </summary>
            [FhirElement("instance", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImagingObjectSelection.InstanceComponent> Instance
            {
                get { if(_Instance==null) _Instance = new List<Hl7.Fhir.Model.ImagingObjectSelection.InstanceComponent>(); return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private List<Hl7.Fhir.Model.ImagingObjectSelection.InstanceComponent> _Instance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SeriesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(Instance != null) dest.Instance = new List<Hl7.Fhir.Model.ImagingObjectSelection.InstanceComponent>(Instance.DeepCopy());
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
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SeriesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("FramesComponent")]
        [DataContract]
        public partial class FramesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FramesComponent"; } }
            
            /// <summary>
            /// Frame numbers
            /// </summary>
            [FhirElement("frameNumbers", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.UnsignedInt> FrameNumbersElement
            {
                get { if(_FrameNumbersElement==null) _FrameNumbersElement = new List<Hl7.Fhir.Model.UnsignedInt>(); return _FrameNumbersElement; }
                set { _FrameNumbersElement = value; OnPropertyChanged("FrameNumbersElement"); }
            }
            
            private List<Hl7.Fhir.Model.UnsignedInt> _FrameNumbersElement;
            
            /// <summary>
            /// Frame numbers
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
            
            /// <summary>
            /// Retrieve URL
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// Retrieve URL
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
                var dest = other as FramesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FrameNumbersElement != null) dest.FrameNumbersElement = new List<Hl7.Fhir.Model.UnsignedInt>(FrameNumbersElement.DeepCopy());
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FramesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FramesComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(FrameNumbersElement, otherT.FrameNumbersElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FramesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FrameNumbersElement, otherT.FrameNumbersElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Instance UID
        /// </summary>
        [FhirElement("uid", Order=90)]
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
        /// Reason for selection
        /// </summary>
        [FhirElement("title", Order=110)]
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
        [FhirElement("description", Order=120)]
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
        /// Author (human or machine)
        /// </summary>
        [FhirElement("author", Order=130)]
        [References("Practitioner","Device","Organization","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Authoring time of the selection
        /// </summary>
        [FhirElement("authoringTime", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoringTimeElement
        {
            get { return _AuthoringTimeElement; }
            set { _AuthoringTimeElement = value; OnPropertyChanged("AuthoringTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoringTimeElement;
        
        /// <summary>
        /// Authoring time of the selection
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
        /// Study identity of the selected instances
        /// </summary>
        [FhirElement("study", Order=150)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImagingObjectSelection.StudyComponent> Study
        {
            get { if(_Study==null) _Study = new List<Hl7.Fhir.Model.ImagingObjectSelection.StudyComponent>(); return _Study; }
            set { _Study = value; OnPropertyChanged("Study"); }
        }
        
        private List<Hl7.Fhir.Model.ImagingObjectSelection.StudyComponent> _Study;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImagingObjectSelection;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Title != null) dest.Title = (Hl7.Fhir.Model.CodeableConcept)Title.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(AuthoringTimeElement != null) dest.AuthoringTimeElement = (Hl7.Fhir.Model.FhirDateTime)AuthoringTimeElement.DeepCopy();
                if(Study != null) dest.Study = new List<Hl7.Fhir.Model.ImagingObjectSelection.StudyComponent>(Study.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ImagingObjectSelection());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImagingObjectSelection;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Title, otherT.Title)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(AuthoringTimeElement, otherT.AuthoringTimeElement)) return false;
            if( !DeepComparable.Matches(Study, otherT.Study)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImagingObjectSelection;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Title, otherT.Title)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(AuthoringTimeElement, otherT.AuthoringTimeElement)) return false;
            if( !DeepComparable.IsExactly(Study, otherT.Study)) return false;
            
            return true;
        }
        
    }
    
}
