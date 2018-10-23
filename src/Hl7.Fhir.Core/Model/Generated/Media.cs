using System;
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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A photo, video, or audio recording acquired or used in healthcare. The actual content may be inline or provided by direct reference
    /// </summary>
    [FhirType("Media", IsResource=true)]
    [DataContract]
    public partial class Media : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Media; } }
        [NotMapped]
        public override string TypeName { get { return "Media"; } }
        
        /// <summary>
        /// Whether the media is a photo, video, or audio
        /// (url: http://hl7.org/fhir/ValueSet/digital-media-type)
        /// </summary>
        [FhirEnumeration("DigitalMediaType")]
        public enum DigitalMediaType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/digital-media-type)
            /// </summary>
            [EnumLiteral("photo", "http://hl7.org/fhir/digital-media-type"), Description("Photo")]
            Photo,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/digital-media-type)
            /// </summary>
            [EnumLiteral("video", "http://hl7.org/fhir/digital-media-type"), Description("Video")]
            Video,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/digital-media-type)
            /// </summary>
            [EnumLiteral("audio", "http://hl7.org/fhir/digital-media-type"), Description("Audio")]
            Audio,
        }

        /// <summary>
        /// Identifier(s) for the image
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
        /// Procedure that caused this media to be created
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("ProcedureRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// photo | video | audio
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Media.DigitalMediaType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Media.DigitalMediaType> _TypeElement;
        
        /// <summary>
        /// photo | video | audio
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Media.DigitalMediaType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Media.DigitalMediaType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// The type of acquisition equipment/process
        /// </summary>
        [FhirElement("subtype", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Subtype
        {
            get { return _Subtype; }
            set { _Subtype = value; OnPropertyChanged("Subtype"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Subtype;
        
        /// <summary>
        /// Imaging view, e.g. Lateral or Antero-posterior
        /// </summary>
        [FhirElement("view", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept View
        {
            get { return _View; }
            set { _View = value; OnPropertyChanged("View"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _View;
        
        /// <summary>
        /// Who/What this Media is a record of
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","Group","Device","Specimen")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter / Episode associated with media
        /// </summary>
        [FhirElement("context", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// When Media was collected
        /// </summary>
        [FhirElement("occurrence", InSummary=true, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// The person who generated the image
        /// </summary>
        [FhirElement("operator", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Operator
        {
            get { return _Operator; }
            set { _Operator = value; OnPropertyChanged("Operator"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Operator;
        
        /// <summary>
        /// Why was event performed?
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Body part in media
        /// </summary>
        [FhirElement("bodySite", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BodySite
        {
            get { return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BodySite;
        
        /// <summary>
        /// Observing Device
        /// </summary>
        [FhirElement("device", InSummary=true, Order=200)]
        [CLSCompliant(false)]
		[References("Device","DeviceMetric")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Device;
        
        /// <summary>
        /// Height of the image in pixels (photo/video)
        /// </summary>
        [FhirElement("height", InSummary=true, Order=210)]
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
                if (!value.HasValue)
                  HeightElement = null; 
                else
                  HeightElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Height");
            }
        }
        
        /// <summary>
        /// Width of the image in pixels (photo/video)
        /// </summary>
        [FhirElement("width", InSummary=true, Order=220)]
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
                if (!value.HasValue)
                  WidthElement = null; 
                else
                  WidthElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Width");
            }
        }
        
        /// <summary>
        /// Number of frames if > 1 (photo)
        /// </summary>
        [FhirElement("frames", InSummary=true, Order=230)]
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
                if (!value.HasValue)
                  FramesElement = null; 
                else
                  FramesElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Frames");
            }
        }
        
        /// <summary>
        /// Length in seconds (audio / video)
        /// </summary>
        [FhirElement("duration", InSummary=true, Order=240)]
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
                if (!value.HasValue)
                  DurationElement = null; 
                else
                  DurationElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Duration");
            }
        }
        
        /// <summary>
        /// Actual Media - reference or data
        /// </summary>
        [FhirElement("content", Order=250)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged("Content"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Content;
        
        /// <summary>
        /// Comments made about the media
        /// </summary>
        [FhirElement("note", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        

        public static ElementDefinition.ConstraintComponent Media_MDA_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "height.empty() or type != 'audio'",
            Key = "mda-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Height can only be used for a photo or video",
            Xpath = "not(f:type/@value='audio') or not(f:height)"
        };

        public static ElementDefinition.ConstraintComponent Media_MDA_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "width.empty() or type != 'audio'",
            Key = "mda-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Width can only be used for a photo or video",
            Xpath = "not(f:type/@value='audio') or not(f:width)"
        };

        public static ElementDefinition.ConstraintComponent Media_MDA_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "frames.empty() or type = 'photo'",
            Key = "mda-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Frames can only be used for a photo",
            Xpath = "(f:type/@value='photo') or not(f:frames)"
        };

        public static ElementDefinition.ConstraintComponent Media_MDA_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "duration.empty() or type != 'photo'",
            Key = "mda-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Duration can only be used for an audio or a video",
            Xpath = "not(f:type/@value='photo') or not(f:duration)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Media_MDA_1);
            InvariantConstraints.Add(Media_MDA_2);
            InvariantConstraints.Add(Media_MDA_3);
            InvariantConstraints.Add(Media_MDA_4);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Media;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Media.DigitalMediaType>)TypeElement.DeepCopy();
                if(Subtype != null) dest.Subtype = (Hl7.Fhir.Model.CodeableConcept)Subtype.DeepCopy();
                if(View != null) dest.View = (Hl7.Fhir.Model.CodeableConcept)View.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(Operator != null) dest.Operator = (Hl7.Fhir.Model.ResourceReference)Operator.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(HeightElement != null) dest.HeightElement = (Hl7.Fhir.Model.PositiveInt)HeightElement.DeepCopy();
                if(WidthElement != null) dest.WidthElement = (Hl7.Fhir.Model.PositiveInt)WidthElement.DeepCopy();
                if(FramesElement != null) dest.FramesElement = (Hl7.Fhir.Model.PositiveInt)FramesElement.DeepCopy();
                if(DurationElement != null) dest.DurationElement = (Hl7.Fhir.Model.UnsignedInt)DurationElement.DeepCopy();
                if(Content != null) dest.Content = (Hl7.Fhir.Model.Attachment)Content.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Media());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Media;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.Matches(View, otherT.View)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(Operator, otherT.Operator)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(HeightElement, otherT.HeightElement)) return false;
            if( !DeepComparable.Matches(WidthElement, otherT.WidthElement)) return false;
            if( !DeepComparable.Matches(FramesElement, otherT.FramesElement)) return false;
            if( !DeepComparable.Matches(DurationElement, otherT.DurationElement)) return false;
            if( !DeepComparable.Matches(Content, otherT.Content)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Media;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
            if( !DeepComparable.IsExactly(View, otherT.View)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(Operator, otherT.Operator)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(HeightElement, otherT.HeightElement)) return false;
            if( !DeepComparable.IsExactly(WidthElement, otherT.WidthElement)) return false;
            if( !DeepComparable.IsExactly(FramesElement, otherT.FramesElement)) return false;
            if( !DeepComparable.IsExactly(DurationElement, otherT.DurationElement)) return false;
            if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				if (TypeElement != null) yield return TypeElement;
				if (Subtype != null) yield return Subtype;
				if (View != null) yield return View;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Occurrence != null) yield return Occurrence;
				if (Operator != null) yield return Operator;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				if (BodySite != null) yield return BodySite;
				if (Device != null) yield return Device;
				if (HeightElement != null) yield return HeightElement;
				if (WidthElement != null) yield return WidthElement;
				if (FramesElement != null) yield return FramesElement;
				if (DurationElement != null) yield return DurationElement;
				if (Content != null) yield return Content;
				foreach (var elem in Note) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (Subtype != null) yield return new ElementValue("subtype", Subtype);
                if (View != null) yield return new ElementValue("view", View);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                if (Operator != null) yield return new ElementValue("operator", Operator);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                if (Device != null) yield return new ElementValue("device", Device);
                if (HeightElement != null) yield return new ElementValue("height", HeightElement);
                if (WidthElement != null) yield return new ElementValue("width", WidthElement);
                if (FramesElement != null) yield return new ElementValue("frames", FramesElement);
                if (DurationElement != null) yield return new ElementValue("duration", DurationElement);
                if (Content != null) yield return new ElementValue("content", Content);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }

    }
    
}
