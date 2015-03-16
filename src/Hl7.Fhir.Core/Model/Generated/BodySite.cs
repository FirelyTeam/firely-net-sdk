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
// Generated on Mon, Mar 16, 2015 22:38+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Specific and identified anatomical location
    /// </summary>
    [FhirType("BodySite", IsResource=true)]
    [DataContract]
    public partial class BodySite : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.BodySite; } }
        [NotMapped]
        public override string TypeName { get { return "BodySite"; } }
        
        [FhirType("BodySiteSpecificLocationComponent")]
        [DataContract]
        public partial class BodySiteSpecificLocationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BodySiteSpecificLocationComponent"; } }
            
            /// <summary>
            /// Named anatomical location
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Name;
            
            /// <summary>
            /// Laterality
            /// </summary>
            [FhirElement("side", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Side
            {
                get { return _Side; }
                set { _Side = value; OnPropertyChanged("Side"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Side;
            
            /// <summary>
            /// Which instance of many
            /// </summary>
            [FhirElement("number", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumberElement;
            
            /// <summary>
            /// Which instance of many
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
                      NumberElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// Description of anatomical plane
            /// </summary>
            [FhirElement("anatomicalPlane", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AnatomicalPlane
            {
                get { return _AnatomicalPlane; }
                set { _AnatomicalPlane = value; OnPropertyChanged("AnatomicalPlane"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AnatomicalPlane;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BodySiteSpecificLocationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.CodeableConcept)Name.DeepCopy();
                    if(Side != null) dest.Side = (Hl7.Fhir.Model.CodeableConcept)Side.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.Integer)NumberElement.DeepCopy();
                    if(AnatomicalPlane != null) dest.AnatomicalPlane = (Hl7.Fhir.Model.CodeableConcept)AnatomicalPlane.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BodySiteSpecificLocationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BodySiteSpecificLocationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(Side, otherT.Side)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(AnatomicalPlane, otherT.AnatomicalPlane)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BodySiteSpecificLocationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(Side, otherT.Side)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(AnatomicalPlane, otherT.AnatomicalPlane)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("BodySiteRelativeLocationComponent")]
        [DataContract]
        public partial class BodySiteRelativeLocationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BodySiteRelativeLocationComponent"; } }
            
            /// <summary>
            /// Identified landmark
            /// </summary>
            [FhirElement("landmark", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Landmark
            {
                get { return _Landmark; }
                set { _Landmark = value; OnPropertyChanged("Landmark"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Landmark;
            
            /// <summary>
            /// Relative position to landmark
            /// </summary>
            [FhirElement("aspect", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Aspect
            {
                get { return _Aspect; }
                set { _Aspect = value; OnPropertyChanged("Aspect"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Aspect;
            
            /// <summary>
            /// Distance from Landmark
            /// </summary>
            [FhirElement("distance", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Distance
            {
                get { return _Distance; }
                set { _Distance = value; OnPropertyChanged("Distance"); }
            }
            
            private Hl7.Fhir.Model.Quantity _Distance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BodySiteRelativeLocationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Landmark != null) dest.Landmark = (Hl7.Fhir.Model.CodeableConcept)Landmark.DeepCopy();
                    if(Aspect != null) dest.Aspect = (Hl7.Fhir.Model.CodeableConcept)Aspect.DeepCopy();
                    if(Distance != null) dest.Distance = (Hl7.Fhir.Model.Quantity)Distance.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BodySiteRelativeLocationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BodySiteRelativeLocationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Landmark, otherT.Landmark)) return false;
                if( !DeepComparable.Matches(Aspect, otherT.Aspect)) return false;
                if( !DeepComparable.Matches(Distance, otherT.Distance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BodySiteRelativeLocationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Landmark, otherT.Landmark)) return false;
                if( !DeepComparable.IsExactly(Aspect, otherT.Aspect)) return false;
                if( !DeepComparable.IsExactly(Distance, otherT.Distance)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Bodysite identifier
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
        /// Specific anatomical location
        /// </summary>
        [FhirElement("specificLocation", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.BodySite.BodySiteSpecificLocationComponent SpecificLocation
        {
            get { return _SpecificLocation; }
            set { _SpecificLocation = value; OnPropertyChanged("SpecificLocation"); }
        }
        
        private Hl7.Fhir.Model.BodySite.BodySiteSpecificLocationComponent _SpecificLocation;
        
        /// <summary>
        /// Relative anatomical location(s)
        /// </summary>
        [FhirElement("relativeLocation", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.BodySite.BodySiteRelativeLocationComponent> RelativeLocation
        {
            get { if(_RelativeLocation==null) _RelativeLocation = new List<Hl7.Fhir.Model.BodySite.BodySiteRelativeLocationComponent>(); return _RelativeLocation; }
            set { _RelativeLocation = value; OnPropertyChanged("RelativeLocation"); }
        }
        
        private List<Hl7.Fhir.Model.BodySite.BodySiteRelativeLocationComponent> _RelativeLocation;
        
        /// <summary>
        /// The Description of anatomical location
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
        /// The Description of anatomical location
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
        /// Attached images
        /// </summary>
        [FhirElement("image", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Image
        {
            get { if(_Image==null) _Image = new List<Hl7.Fhir.Model.Attachment>(); return _Image; }
            set { _Image = value; OnPropertyChanged("Image"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Image;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as BodySite;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(SpecificLocation != null) dest.SpecificLocation = (Hl7.Fhir.Model.BodySite.BodySiteSpecificLocationComponent)SpecificLocation.DeepCopy();
                if(RelativeLocation != null) dest.RelativeLocation = new List<Hl7.Fhir.Model.BodySite.BodySiteRelativeLocationComponent>(RelativeLocation.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Image != null) dest.Image = new List<Hl7.Fhir.Model.Attachment>(Image.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new BodySite());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as BodySite;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(SpecificLocation, otherT.SpecificLocation)) return false;
            if( !DeepComparable.Matches(RelativeLocation, otherT.RelativeLocation)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Image, otherT.Image)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as BodySite;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(SpecificLocation, otherT.SpecificLocation)) return false;
            if( !DeepComparable.IsExactly(RelativeLocation, otherT.RelativeLocation)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Image, otherT.Image)) return false;
            
            return true;
        }
        
    }
    
}
