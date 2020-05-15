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
    /// Operation Request or Response
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Parameters", IsResource=true)]
    [DataContract]
    public partial class Parameters : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Parameters; } }
        [NotMapped]
        public override string TypeName { get { return "Parameters"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Name from the definition
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name from the definition
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
            /// If parameter is a data type
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(Version=Version.DSTU2, Types=new[]{typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta)})]
            [AllowedTypes(Version=Version.R4, Types=new[]{typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta)})]
            [AllowedTypes(Version=Version.STU3, Types=new[]{typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.STU3.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.STU3.ContactPoint),typeof(Hl7.Fhir.Model.STU3.Count),typeof(Hl7.Fhir.Model.STU3.Distance),typeof(Hl7.Fhir.Model.STU3.Duration),typeof(Hl7.Fhir.Model.STU3.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.STU3.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.STU3.SampledData),typeof(Hl7.Fhir.Model.STU3.Signature),typeof(Hl7.Fhir.Model.STU3.Timing),typeof(Hl7.Fhir.Model.Meta)})]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// If parameter is a whole resource
            /// </summary>
            [FhirElement("resource", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60, Choice=ChoiceType.ResourceChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.Resource _Resource;
            
            /// <summary>
            /// Named part of a parameter (e.g. Tuple)
            /// </summary>
            [FhirElement("part", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ParameterComponent> Part
            {
                get { if(_Part==null) _Part = new List<ParameterComponent>(); return _Part; }
                set { _Part = value; OnPropertyChanged("Part"); }
            }
            
            private List<ParameterComponent> _Part;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, true); Value?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); Resource?.Serialize(sink);
                sink.BeginList("part", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false);
                foreach(var item in Part)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.Resource)Resource.DeepCopy();
                    if(Part != null) dest.Part = new List<ParameterComponent>(Part.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Part, otherT.Part)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Part, otherT.Part)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Value != null) yield return Value;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Part) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Part) { if (elem != null) yield return new ElementValue("part", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Operation Parameter
        /// </summary>
        [FhirElement("parameter", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParameterComponent> Parameter
        {
            get { if(_Parameter==null) _Parameter = new List<ParameterComponent>(); return _Parameter; }
            set { _Parameter = value; OnPropertyChanged("Parameter"); }
        }
        
        private List<ParameterComponent> _Parameter;
    
    
        public static ElementDefinitionConstraint[] Parameters_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                human: "A parameter must have only one of (value, resource, part)",
                xpath: "exists(f:value) or exists(f:resource) and not(exists(f:value) and exists(f:resource))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                human: "A parameter must have one and only one of (value, resource, part)",
                xpath: "(exists(f:resource) or exists(f:part) or exists(f:*[starts-with(local-name(.), 'value')])) and not(exists(f:*[starts-with(local-name(.), 'value')])) and exists(f:resource))) and not(exists(f:*[starts-with(local-name(.), 'value')])) and exists(f:part))) and not(exists(f:part) and exists(f:resource))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                human: "A parameter must have only one of (value, resource, part)",
                xpath: "exists(f:value) or exists(f:resource) and not(exists(f:value) and exists(f:resource))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Parameters_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Parameters;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Parameter != null) dest.Parameter = new List<ParameterComponent>(Parameter.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Parameters());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Parameters;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Parameters;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Parameters");
            base.Serialize(sink);
            sink.BeginList("parameter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false);
            foreach(var item in Parameter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Parameter) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
            }
        }
    
    }

}
