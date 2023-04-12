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
// Generated for FHIR v4.0.1, v1.0.2, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The marketing status describes the date when a medicinal product is actually put on the market or the date as of which it is no longer available
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "ProdCharacteristic")]
    [DataContract]
    public partial class ProdCharacteristic : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ProdCharacteristic"; } }
    
        
        /// <summary>
        /// Where applicable, the height can be specified using a numerical value and its unit of measurement The unit of measurement shall be specified in accordance with ISO 11240 and the resulting terminology The symbol and the symbol identifier shall be used
        /// </summary>
        [FhirElement("height", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Height
        {
            get { return _Height; }
            set { _Height = value; OnPropertyChanged("Height"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Height;
        
        /// <summary>
        /// Where applicable, the width can be specified using a numerical value and its unit of measurement The unit of measurement shall be specified in accordance with ISO 11240 and the resulting terminology The symbol and the symbol identifier shall be used
        /// </summary>
        [FhirElement("width", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Width
        {
            get { return _Width; }
            set { _Width = value; OnPropertyChanged("Width"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Width;
        
        /// <summary>
        /// Where applicable, the depth can be specified using a numerical value and its unit of measurement The unit of measurement shall be specified in accordance with ISO 11240 and the resulting terminology The symbol and the symbol identifier shall be used
        /// </summary>
        [FhirElement("depth", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Depth
        {
            get { return _Depth; }
            set { _Depth = value; OnPropertyChanged("Depth"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Depth;
        
        /// <summary>
        /// Where applicable, the weight can be specified using a numerical value and its unit of measurement The unit of measurement shall be specified in accordance with ISO 11240 and the resulting terminology The symbol and the symbol identifier shall be used
        /// </summary>
        [FhirElement("weight", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Weight
        {
            get { return _Weight; }
            set { _Weight = value; OnPropertyChanged("Weight"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Weight;
        
        /// <summary>
        /// Where applicable, the nominal volume can be specified using a numerical value and its unit of measurement The unit of measurement shall be specified in accordance with ISO 11240 and the resulting terminology The symbol and the symbol identifier shall be used
        /// </summary>
        [FhirElement("nominalVolume", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity NominalVolume
        {
            get { return _NominalVolume; }
            set { _NominalVolume = value; OnPropertyChanged("NominalVolume"); }
        }
        
        private Hl7.Fhir.Model.Quantity _NominalVolume;
        
        /// <summary>
        /// Where applicable, the external diameter can be specified using a numerical value and its unit of measurement The unit of measurement shall be specified in accordance with ISO 11240 and the resulting terminology The symbol and the symbol identifier shall be used
        /// </summary>
        [FhirElement("externalDiameter", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity ExternalDiameter
        {
            get { return _ExternalDiameter; }
            set { _ExternalDiameter = value; OnPropertyChanged("ExternalDiameter"); }
        }
        
        private Hl7.Fhir.Model.Quantity _ExternalDiameter;
        
        /// <summary>
        /// Where applicable, the shape can be specified An appropriate controlled vocabulary shall be used The term and the term identifier shall be used
        /// </summary>
        [FhirElement("shape", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ShapeElement
        {
            get { return _ShapeElement; }
            set { _ShapeElement = value; OnPropertyChanged("ShapeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ShapeElement;
        
        /// <summary>
        /// Where applicable, the shape can be specified An appropriate controlled vocabulary shall be used The term and the term identifier shall be used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Shape
        {
            get { return ShapeElement != null ? ShapeElement.Value : null; }
            set
            {
                if (value == null)
                    ShapeElement = null;
                else
                    ShapeElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Shape");
            }
        }
        
        /// <summary>
        /// Where applicable, the color can be specified An appropriate controlled vocabulary shall be used The term and the term identifier shall be used
        /// </summary>
        [FhirElement("color", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ColorElement
        {
            get { if(_ColorElement==null) _ColorElement = new List<Hl7.Fhir.Model.FhirString>(); return _ColorElement; }
            set { _ColorElement = value; OnPropertyChanged("ColorElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ColorElement;
        
        /// <summary>
        /// Where applicable, the color can be specified An appropriate controlled vocabulary shall be used The term and the term identifier shall be used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Color
        {
            get { return ColorElement != null ? ColorElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ColorElement = null;
                else
                    ColorElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Color");
            }
        }
        
        /// <summary>
        /// Where applicable, the imprint can be specified as text
        /// </summary>
        [FhirElement("imprint", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ImprintElement
        {
            get { if(_ImprintElement==null) _ImprintElement = new List<Hl7.Fhir.Model.FhirString>(); return _ImprintElement; }
            set { _ImprintElement = value; OnPropertyChanged("ImprintElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ImprintElement;
        
        /// <summary>
        /// Where applicable, the imprint can be specified as text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Imprint
        {
            get { return ImprintElement != null ? ImprintElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ImprintElement = null;
                else
                    ImprintElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Imprint");
            }
        }
        
        /// <summary>
        /// Where applicable, the image can be provided The format of the image attachment shall be specified by regional implementations
        /// </summary>
        [FhirElement("image", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Image
        {
            get { if(_Image==null) _Image = new List<Hl7.Fhir.Model.Attachment>(); return _Image; }
            set { _Image = value; OnPropertyChanged("Image"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Image;
        
        /// <summary>
        /// Where applicable, the scoring can be specified An appropriate controlled vocabulary shall be used The term and the term identifier shall be used
        /// </summary>
        [FhirElement("scoring", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Scoring
        {
            get { return _Scoring; }
            set { _Scoring = value; OnPropertyChanged("Scoring"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Scoring;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ProdCharacteristic;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Height != null) dest.Height = (Hl7.Fhir.Model.Quantity)Height.DeepCopy();
                if(Width != null) dest.Width = (Hl7.Fhir.Model.Quantity)Width.DeepCopy();
                if(Depth != null) dest.Depth = (Hl7.Fhir.Model.Quantity)Depth.DeepCopy();
                if(Weight != null) dest.Weight = (Hl7.Fhir.Model.Quantity)Weight.DeepCopy();
                if(NominalVolume != null) dest.NominalVolume = (Hl7.Fhir.Model.Quantity)NominalVolume.DeepCopy();
                if(ExternalDiameter != null) dest.ExternalDiameter = (Hl7.Fhir.Model.Quantity)ExternalDiameter.DeepCopy();
                if(ShapeElement != null) dest.ShapeElement = (Hl7.Fhir.Model.FhirString)ShapeElement.DeepCopy();
                if(ColorElement != null) dest.ColorElement = new List<Hl7.Fhir.Model.FhirString>(ColorElement.DeepCopy());
                if(ImprintElement != null) dest.ImprintElement = new List<Hl7.Fhir.Model.FhirString>(ImprintElement.DeepCopy());
                if(Image != null) dest.Image = new List<Hl7.Fhir.Model.Attachment>(Image.DeepCopy());
                if(Scoring != null) dest.Scoring = (Hl7.Fhir.Model.CodeableConcept)Scoring.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ProdCharacteristic());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ProdCharacteristic;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Height, otherT.Height)) return false;
            if( !DeepComparable.Matches(Width, otherT.Width)) return false;
            if( !DeepComparable.Matches(Depth, otherT.Depth)) return false;
            if( !DeepComparable.Matches(Weight, otherT.Weight)) return false;
            if( !DeepComparable.Matches(NominalVolume, otherT.NominalVolume)) return false;
            if( !DeepComparable.Matches(ExternalDiameter, otherT.ExternalDiameter)) return false;
            if( !DeepComparable.Matches(ShapeElement, otherT.ShapeElement)) return false;
            if( !DeepComparable.Matches(ColorElement, otherT.ColorElement)) return false;
            if( !DeepComparable.Matches(ImprintElement, otherT.ImprintElement)) return false;
            if( !DeepComparable.Matches(Image, otherT.Image)) return false;
            if( !DeepComparable.Matches(Scoring, otherT.Scoring)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ProdCharacteristic;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Height, otherT.Height)) return false;
            if( !DeepComparable.IsExactly(Width, otherT.Width)) return false;
            if( !DeepComparable.IsExactly(Depth, otherT.Depth)) return false;
            if( !DeepComparable.IsExactly(Weight, otherT.Weight)) return false;
            if( !DeepComparable.IsExactly(NominalVolume, otherT.NominalVolume)) return false;
            if( !DeepComparable.IsExactly(ExternalDiameter, otherT.ExternalDiameter)) return false;
            if( !DeepComparable.IsExactly(ShapeElement, otherT.ShapeElement)) return false;
            if( !DeepComparable.IsExactly(ColorElement, otherT.ColorElement)) return false;
            if( !DeepComparable.IsExactly(ImprintElement, otherT.ImprintElement)) return false;
            if( !DeepComparable.IsExactly(Image, otherT.Image)) return false;
            if( !DeepComparable.IsExactly(Scoring, otherT.Scoring)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("ProdCharacteristic");
            base.Serialize(sink);
            sink.Element("height", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); Height?.Serialize(sink);
            sink.Element("width", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); Width?.Serialize(sink);
            sink.Element("depth", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); Depth?.Serialize(sink);
            sink.Element("weight", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); Weight?.Serialize(sink);
            sink.Element("nominalVolume", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); NominalVolume?.Serialize(sink);
            sink.Element("externalDiameter", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); ExternalDiameter?.Serialize(sink);
            sink.Element("shape", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); ShapeElement?.Serialize(sink);
            sink.BeginList("color", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false);
            sink.Serialize(ColorElement);
            sink.End();
            sink.BeginList("imprint", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false);
            sink.Serialize(ImprintElement);
            sink.End();
            sink.BeginList("image", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false);
            foreach(var item in Image)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("scoring", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); Scoring?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "height" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Height = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "width" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Width = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "depth" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Depth = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "weight" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Weight = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "nominalVolume" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    NominalVolume = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "externalDiameter" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ExternalDiameter = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "shape" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ShapeElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "color" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ColorElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "imprint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ImprintElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "image" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Image = source.GetList<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "scoring" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Scoring = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "height" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Height = source.Populate(Height);
                    return true;
                case "width" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Width = source.Populate(Width);
                    return true;
                case "depth" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Depth = source.Populate(Depth);
                    return true;
                case "weight" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Weight = source.Populate(Weight);
                    return true;
                case "nominalVolume" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    NominalVolume = source.Populate(NominalVolume);
                    return true;
                case "externalDiameter" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ExternalDiameter = source.Populate(ExternalDiameter);
                    return true;
                case "shape" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ShapeElement = source.PopulateValue(ShapeElement);
                    return true;
                case "_shape" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ShapeElement = source.Populate(ShapeElement);
                    return true;
                case "color" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                case "_color" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "imprint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                case "_imprint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "image" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "scoring" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Scoring = source.Populate(Scoring);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "color" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.PopulatePrimitiveListItemValue(ColorElement, index);
                    return true;
                case "_color" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.PopulatePrimitiveListItem(ColorElement, index);
                    return true;
                case "imprint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.PopulatePrimitiveListItemValue(ImprintElement, index);
                    return true;
                case "_imprint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.PopulatePrimitiveListItem(ImprintElement, index);
                    return true;
                case "image" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.PopulateListItem(Image, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Height != null) yield return Height;
                if (Width != null) yield return Width;
                if (Depth != null) yield return Depth;
                if (Weight != null) yield return Weight;
                if (NominalVolume != null) yield return NominalVolume;
                if (ExternalDiameter != null) yield return ExternalDiameter;
                if (ShapeElement != null) yield return ShapeElement;
                foreach (var elem in ColorElement) { if (elem != null) yield return elem; }
                foreach (var elem in ImprintElement) { if (elem != null) yield return elem; }
                foreach (var elem in Image) { if (elem != null) yield return elem; }
                if (Scoring != null) yield return Scoring;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Height != null) yield return new ElementValue("height", Height);
                if (Width != null) yield return new ElementValue("width", Width);
                if (Depth != null) yield return new ElementValue("depth", Depth);
                if (Weight != null) yield return new ElementValue("weight", Weight);
                if (NominalVolume != null) yield return new ElementValue("nominalVolume", NominalVolume);
                if (ExternalDiameter != null) yield return new ElementValue("externalDiameter", ExternalDiameter);
                if (ShapeElement != null) yield return new ElementValue("shape", ShapeElement);
                foreach (var elem in ColorElement) { if (elem != null) yield return new ElementValue("color", elem); }
                foreach (var elem in ImprintElement) { if (elem != null) yield return new ElementValue("imprint", elem); }
                foreach (var elem in Image) { if (elem != null) yield return new ElementValue("image", elem); }
                if (Scoring != null) yield return new ElementValue("scoring", Scoring);
            }
        }
    
    }

}
