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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A reference from one resource to another
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Reference")]
    [DataContract]
    public partial class ResourceReference : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Reference"; } }
    
        
        /// <summary>
        /// Relative, internal or absolute URL reference
        /// </summary>
        [FhirElement("reference", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ReferenceElement
        {
            get { return _ReferenceElement; }
            set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ReferenceElement;
        
        /// <summary>
        /// Relative, internal or absolute URL reference
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Reference
        {
            get { return ReferenceElement != null ? ReferenceElement.Value : null; }
            set
            {
                if (value == null)
                    ReferenceElement = null;
                else
                    ReferenceElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Reference");
            }
        }
        
        /// <summary>
        /// Text alternative for the resource
        /// </summary>
        [FhirElement("display", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DisplayElement
        {
            get { return _DisplayElement; }
            set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DisplayElement;
        
        /// <summary>
        /// Text alternative for the resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Display
        {
            get { return DisplayElement != null ? DisplayElement.Value : null; }
            set
            {
                if (value == null)
                    DisplayElement = null;
                else
                    DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Display");
            }
        }
        
        /// <summary>
        /// Type the reference refers to (e.g. "Patient")
        /// </summary>
        [FhirElement("type", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _TypeElement;
        
        /// <summary>
        /// Type the reference refers to (e.g. "Patient")
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Logical reference, when literal reference is not known
        /// </summary>
        [FhirElement("identifier", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
    
    
        public static ElementDefinitionConstraint[] ResourceReference_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "ref-1",
                severity: ConstraintSeverity.Warning,
                expression: "reference.startsWith('#').not() or (reference.substring(1).trace('url') in %resource.contained.id.trace('ids'))",
                human: "SHALL have a local reference if the resource is provided inline",
                xpath: "not(starts-with(f:reference/@value, '#')) or exists(ancestor::*[self::f:entry or self::f:parameter]/f:resource/f:*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, '#')]|/*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, '#')])"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "ref-1",
                severity: ConstraintSeverity.Warning,
                expression: "reference.startsWith('#').not() or (reference.substring(1).trace('url') in %rootResource.contained.id.trace('ids'))",
                human: "SHALL have a contained resource if a local reference is provided",
                xpath: "not(starts-with(f:reference/@value, '#')) or exists(ancestor::*[self::f:entry or self::f:parameter]/f:resource/f:*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, '#')]|/*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, '#')])"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "ref-1",
                severity: ConstraintSeverity.Warning,
                expression: "reference.startsWith('#').not() or (reference.substring(1).trace('url') in %resource.contained.id.trace('ids'))",
                human: "SHALL have a contained resource if a local reference is provided",
                xpath: "not(starts-with(f:reference/@value, '#')) or exists(ancestor::*[self::f:entry or self::f:parameter]/f:resource/f:*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, '#')]|/*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, '#')])"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ResourceReference;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirString)ReferenceElement.DeepCopy();
                if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirUri)TypeElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ResourceReference());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ResourceReference;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
            if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ResourceReference;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
            if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Reference");
            base.Serialize(sink);
            sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReferenceElement?.Serialize(sink);
            sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisplayElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); TypeElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); Identifier?.Serialize(sink);
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
                case "reference":
                    ReferenceElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "display":
                    DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "type" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    TypeElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
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
                case "reference":
                    ReferenceElement = source.PopulateValue(ReferenceElement);
                    return true;
                case "_reference":
                    ReferenceElement = source.Populate(ReferenceElement);
                    return true;
                case "display":
                    DisplayElement = source.PopulateValue(DisplayElement);
                    return true;
                case "_display":
                    DisplayElement = source.Populate(DisplayElement);
                    return true;
                case "type" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Identifier = source.Populate(Identifier);
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
                if (ReferenceElement != null) yield return ReferenceElement;
                if (DisplayElement != null) yield return DisplayElement;
                if (TypeElement != null) yield return TypeElement;
                if (Identifier != null) yield return Identifier;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
                if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
            }
        }
    
    }

}
