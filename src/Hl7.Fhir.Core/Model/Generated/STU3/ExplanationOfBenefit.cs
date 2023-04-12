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
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Explanation of Benefit resource
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "ExplanationOfBenefit", IsResource=true)]
    [DataContract]
    public partial class ExplanationOfBenefit : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IExplanationOfBenefit, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ExplanationOfBenefit; } }
        [NotMapped]
        public override string TypeName { get { return "ExplanationOfBenefit"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RelatedClaimComponent")]
        [DataContract]
        public partial class RelatedClaimComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedClaimComponent"; } }
            
            /// <summary>
            /// Reference to the related claim
            /// </summary>
            [FhirElement("claim", Order=40)]
            [CLSCompliant(false)]
            [References("Claim")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Claim
            {
                get { return _Claim; }
                set { _Claim = value; OnPropertyChanged("Claim"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Claim;
            
            /// <summary>
            /// How the reference claim is related
            /// </summary>
            [FhirElement("relationship", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Relationship;
            
            /// <summary>
            /// Related file or case reference
            /// </summary>
            [FhirElement("reference", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Reference;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RelatedClaimComponent");
                base.Serialize(sink);
                sink.Element("claim", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Claim?.Serialize(sink);
                sink.Element("relationship", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Relationship?.Serialize(sink);
                sink.Element("reference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reference?.Serialize(sink);
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
                    case "claim":
                        Claim = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "relationship":
                        Relationship = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "reference":
                        Reference = source.Get<Hl7.Fhir.Model.Identifier>();
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
                    case "claim":
                        Claim = source.Populate(Claim);
                        return true;
                    case "relationship":
                        Relationship = source.Populate(Relationship);
                        return true;
                    case "reference":
                        Reference = source.Populate(Reference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedClaimComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Claim != null) dest.Claim = (Hl7.Fhir.Model.ResourceReference)Claim.DeepCopy();
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.Identifier)Reference.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RelatedClaimComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedClaimComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Claim, otherT.Claim)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedClaimComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Claim, otherT.Claim)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Claim != null) yield return Claim;
                    if (Relationship != null) yield return Relationship;
                    if (Reference != null) yield return Reference;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Claim != null) yield return new ElementValue("claim", Claim);
                    if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "PayeeComponent")]
        [DataContract]
        public partial class PayeeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PayeeComponent"; } }
            
            /// <summary>
            /// Type of party: Subscriber, Provider, other
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// organization | patient | practitioner | relatedperson
            /// </summary>
            [FhirElement("resourceType", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ResourceType
            {
                get { return _ResourceType; }
                set { _ResourceType = value; OnPropertyChanged("ResourceType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ResourceType;
            
            /// <summary>
            /// Party to receive the payable
            /// </summary>
            [FhirElement("party", Order=60)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","Patient","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PayeeComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("resourceType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResourceType?.Serialize(sink);
                sink.Element("party", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Party?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "resourceType":
                        ResourceType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "party":
                        Party = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "resourceType":
                        ResourceType = source.Populate(ResourceType);
                        return true;
                    case "party":
                        Party = source.Populate(Party);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayeeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(ResourceType != null) dest.ResourceType = (Hl7.Fhir.Model.CodeableConcept)ResourceType.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PayeeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ResourceType, otherT.ResourceType)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ResourceType, otherT.ResourceType)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (ResourceType != null) yield return ResourceType;
                    if (Party != null) yield return Party;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (ResourceType != null) yield return new ElementValue("resourceType", ResourceType);
                    if (Party != null) yield return new ElementValue("party", Party);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SupportingInformationComponent")]
        [DataContract]
        public partial class SupportingInformationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SupportingInformationComponent"; } }
            
            /// <summary>
            /// Information instance identifier
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Information instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// General class of information
            /// </summary>
            [FhirElement("category", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Type of information
            /// </summary>
            [FhirElement("code", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// When it occurred
            /// </summary>
            [FhirElement("timing", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Additional Data or supporting information
            /// </summary>
            [FhirElement("value", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Reason associated with the information
            /// </summary>
            [FhirElement("reason", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.Coding _Reason;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SupportingInformationComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Category?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
                sink.Element("timing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Timing?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Value?.Serialize(sink);
                sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reason?.Serialize(sink);
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "timingDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "timingPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Attachment>();
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "reason":
                        Reason = source.Get<Hl7.Fhir.Model.Coding>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "timingDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Timing, "timing");
                        Timing = source.PopulateValue(Timing as Hl7.Fhir.Model.Date);
                        return true;
                    case "_timingDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.Date);
                        return true;
                    case "timingPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.Period);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "reason":
                        Reason = source.Populate(Reason);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SupportingInformationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Coding)Reason.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SupportingInformationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SupportingInformationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SupportingInformationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Category != null) yield return Category;
                    if (Code != null) yield return Code;
                    if (Timing != null) yield return Timing;
                    if (Value != null) yield return Value;
                    if (Reason != null) yield return Reason;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Timing != null) yield return new ElementValue("timing", Timing);
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "CareTeamComponent")]
        [DataContract]
        public partial class CareTeamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CareTeamComponent"; } }
            
            /// <summary>
            /// Number to covey order of careteam
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Number to covey order of careteam
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Member of the Care Team
            /// </summary>
            [FhirElement("provider", Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Provider;
            
            /// <summary>
            /// Billing practitioner
            /// </summary>
            [FhirElement("responsible", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ResponsibleElement
            {
                get { return _ResponsibleElement; }
                set { _ResponsibleElement = value; OnPropertyChanged("ResponsibleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ResponsibleElement;
            
            /// <summary>
            /// Billing practitioner
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Responsible
            {
                get { return ResponsibleElement != null ? ResponsibleElement.Value : null; }
                set
                {
                    if (value == null)
                        ResponsibleElement = null;
                    else
                        ResponsibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Responsible");
                }
            }
            
            /// <summary>
            /// Role on the team
            /// </summary>
            [FhirElement("role", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Type, classification or Specialization
            /// </summary>
            [FhirElement("qualification", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Qualification
            {
                get { return _Qualification; }
                set { _Qualification = value; OnPropertyChanged("Qualification"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Qualification;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CareTeamComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("provider", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Provider?.Serialize(sink);
                sink.Element("responsible", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponsibleElement?.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Role?.Serialize(sink);
                sink.Element("qualification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Qualification?.Serialize(sink);
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "provider":
                        Provider = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "responsible":
                        ResponsibleElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "role":
                        Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "qualification":
                        Qualification = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "provider":
                        Provider = source.Populate(Provider);
                        return true;
                    case "responsible":
                        ResponsibleElement = source.PopulateValue(ResponsibleElement);
                        return true;
                    case "_responsible":
                        ResponsibleElement = source.Populate(ResponsibleElement);
                        return true;
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "qualification":
                        Qualification = source.Populate(Qualification);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CareTeamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                    if(ResponsibleElement != null) dest.ResponsibleElement = (Hl7.Fhir.Model.FhirBoolean)ResponsibleElement.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Qualification != null) dest.Qualification = (Hl7.Fhir.Model.CodeableConcept)Qualification.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CareTeamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(ResponsibleElement, otherT.ResponsibleElement)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Qualification, otherT.Qualification)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(ResponsibleElement, otherT.ResponsibleElement)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Qualification, otherT.Qualification)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Provider != null) yield return Provider;
                    if (ResponsibleElement != null) yield return ResponsibleElement;
                    if (Role != null) yield return Role;
                    if (Qualification != null) yield return Qualification;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Provider != null) yield return new ElementValue("provider", Provider);
                    if (ResponsibleElement != null) yield return new ElementValue("responsible", ResponsibleElement);
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Qualification != null) yield return new ElementValue("qualification", Qualification);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DiagnosisComponent")]
        [DataContract]
        public partial class DiagnosisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DiagnosisComponent"; } }
            
            /// <summary>
            /// Number to covey order of diagnosis
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Number to covey order of diagnosis
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Patient's diagnosis
            /// </summary>
            [FhirElement("diagnosis", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Diagnosis
            {
                get { return _Diagnosis; }
                set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
            }
            
            private Hl7.Fhir.Model.Element _Diagnosis;
            
            /// <summary>
            /// Timing or nature of the diagnosis
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Package billing code
            /// </summary>
            [FhirElement("packageCode", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept PackageCode
            {
                get { return _PackageCode; }
                set { _PackageCode = value; OnPropertyChanged("PackageCode"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _PackageCode;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DiagnosisComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("diagnosis", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Diagnosis?.Serialize(sink);
                sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Type)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("packageCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PackageCode?.Serialize(sink);
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "diagnosisCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Diagnosis, "diagnosis");
                        Diagnosis = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "diagnosisReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Diagnosis, "diagnosis");
                        Diagnosis = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "type":
                        Type = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "packageCode":
                        PackageCode = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "diagnosisCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Diagnosis, "diagnosis");
                        Diagnosis = source.Populate(Diagnosis as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "diagnosisReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Diagnosis, "diagnosis");
                        Diagnosis = source.Populate(Diagnosis as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "type":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "packageCode":
                        PackageCode = source.Populate(PackageCode);
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
                    case "type":
                        source.PopulateListItem(Type, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosisComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Diagnosis != null) dest.Diagnosis = (Hl7.Fhir.Model.Element)Diagnosis.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(PackageCode != null) dest.PackageCode = (Hl7.Fhir.Model.CodeableConcept)PackageCode.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DiagnosisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(PackageCode, otherT.PackageCode)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(PackageCode, otherT.PackageCode)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Diagnosis != null) yield return Diagnosis;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    if (PackageCode != null) yield return PackageCode;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Diagnosis != null) yield return new ElementValue("diagnosis", Diagnosis);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    if (PackageCode != null) yield return new ElementValue("packageCode", PackageCode);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ProcedureComponent")]
        [DataContract]
        public partial class ProcedureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureComponent"; } }
            
            /// <summary>
            /// Procedure sequence for reference
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Procedure sequence for reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// When the procedure was performed
            /// </summary>
            [FhirElement("date", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When the procedure was performed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if (value == null)
                        DateElement = null;
                    else
                        DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Patient's list of procedures performed
            /// </summary>
            [FhirElement("procedure", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Procedure
            {
                get { return _Procedure; }
                set { _Procedure = value; OnPropertyChanged("Procedure"); }
            }
            
            private Hl7.Fhir.Model.Element _Procedure;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ProcedureComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DateElement?.Serialize(sink);
                sink.Element("procedure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Procedure?.Serialize(sink);
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "date":
                        DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "procedureCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Procedure, "procedure");
                        Procedure = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "procedureReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Procedure, "procedure");
                        Procedure = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "procedureCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Procedure, "procedure");
                        Procedure = source.Populate(Procedure as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "procedureReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Procedure, "procedure");
                        Procedure = source.Populate(Procedure as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.Element)Procedure.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ProcedureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (DateElement != null) yield return DateElement;
                    if (Procedure != null) yield return Procedure;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Procedure != null) yield return new ElementValue("procedure", Procedure);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "InsuranceComponent")]
        [DataContract]
        public partial class InsuranceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InsuranceComponent"; } }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", Order=40)]
            [CLSCompliant(false)]
            [References("Coverage")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Coverage;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preAuthRef", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PreAuthRefElement
            {
                get { if(_PreAuthRefElement==null) _PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(); return _PreAuthRefElement; }
                set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _PreAuthRefElement;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> PreAuthRef
            {
                get { return PreAuthRefElement != null ? PreAuthRefElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PreAuthRefElement = null;
                    else
                        PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("PreAuthRef");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InsuranceComponent");
                base.Serialize(sink);
                sink.Element("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Coverage?.Serialize(sink);
                sink.BeginList("preAuthRef", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(PreAuthRefElement);
                sink.End();
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
                    case "coverage":
                        Coverage = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "preAuthRef":
                        PreAuthRefElement = source.GetList<Hl7.Fhir.Model.FhirString>();
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
                    case "coverage":
                        Coverage = source.Populate(Coverage);
                        return true;
                    case "preAuthRef":
                    case "_preAuthRef":
                        source.SetList(this, jsonPropertyName);
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
                    case "preAuthRef":
                        source.PopulatePrimitiveListItemValue(PreAuthRefElement, index);
                        return true;
                    case "_preAuthRef":
                        source.PopulatePrimitiveListItem(PreAuthRefElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InsuranceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(PreAuthRefElement != null) dest.PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(PreAuthRefElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new InsuranceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Coverage != null) yield return Coverage;
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return new ElementValue("preAuthRef", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AccidentComponent")]
        [DataContract]
        public partial class AccidentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AccidentComponent"; } }
            
            /// <summary>
            /// When the accident occurred
            /// </summary>
            [FhirElement("date", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// When the accident occurred
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if (value == null)
                        DateElement = null;
                    else
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// The nature of the accident
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Accident Place
            /// </summary>
            [FhirElement("location", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AccidentComponent");
                base.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DateElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Location?.Serialize(sink);
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
                    case "date":
                        DateElement = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "locationAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.Address>();
                        return true;
                    case "locationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "locationAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.Address);
                        return true;
                    case "locationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AccidentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AccidentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AccidentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AccidentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DateElement != null) yield return DateElement;
                    if (Type != null) yield return Type;
                    if (Location != null) yield return Location;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Location != null) yield return new ElementValue("location", Location);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Applicable careteam members
            /// </summary>
            [FhirElement("careTeamLinkId", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> CareTeamLinkIdElement
            {
                get { if(_CareTeamLinkIdElement==null) _CareTeamLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _CareTeamLinkIdElement; }
                set { _CareTeamLinkIdElement = value; OnPropertyChanged("CareTeamLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _CareTeamLinkIdElement;
            
            /// <summary>
            /// Applicable careteam members
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> CareTeamLinkId
            {
                get { return CareTeamLinkIdElement != null ? CareTeamLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        CareTeamLinkIdElement = null;
                    else
                        CareTeamLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("CareTeamLinkId");
                }
            }
            
            /// <summary>
            /// Applicable diagnoses
            /// </summary>
            [FhirElement("diagnosisLinkId", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> DiagnosisLinkIdElement
            {
                get { if(_DiagnosisLinkIdElement==null) _DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _DiagnosisLinkIdElement; }
                set { _DiagnosisLinkIdElement = value; OnPropertyChanged("DiagnosisLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _DiagnosisLinkIdElement;
            
            /// <summary>
            /// Applicable diagnoses
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> DiagnosisLinkId
            {
                get { return DiagnosisLinkIdElement != null ? DiagnosisLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        DiagnosisLinkIdElement = null;
                    else
                        DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("DiagnosisLinkId");
                }
            }
            
            /// <summary>
            /// Applicable procedures
            /// </summary>
            [FhirElement("procedureLinkId", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> ProcedureLinkIdElement
            {
                get { if(_ProcedureLinkIdElement==null) _ProcedureLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _ProcedureLinkIdElement; }
                set { _ProcedureLinkIdElement = value; OnPropertyChanged("ProcedureLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _ProcedureLinkIdElement;
            
            /// <summary>
            /// Applicable procedures
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> ProcedureLinkId
            {
                get { return ProcedureLinkIdElement != null ? ProcedureLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ProcedureLinkIdElement = null;
                    else
                        ProcedureLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("ProcedureLinkId");
                }
            }
            
            /// <summary>
            /// Applicable exception and supporting information
            /// </summary>
            [FhirElement("informationLinkId", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> InformationLinkIdElement
            {
                get { if(_InformationLinkIdElement==null) _InformationLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _InformationLinkIdElement; }
                set { _InformationLinkIdElement = value; OnPropertyChanged("InformationLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _InformationLinkIdElement;
            
            /// <summary>
            /// Applicable exception and supporting information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> InformationLinkId
            {
                get { return InformationLinkIdElement != null ? InformationLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        InformationLinkIdElement = null;
                    else
                        InformationLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("InformationLinkId");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Date or dates of Service
            /// </summary>
            [FhirElement("serviced", Order=140, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Serviced
            {
                get { return _Serviced; }
                set { _Serviced = value; OnPropertyChanged("Serviced"); }
            }
            
            private Hl7.Fhir.Model.Element _Serviced;
            
            /// <summary>
            /// Place of service
            /// </summary>
            [FhirElement("location", Order=150, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("net", Order=190)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=200)]
            [CLSCompliant(false)]
            [References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// Service Location
            /// </summary>
            [FhirElement("bodySite", Order=210)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _BodySite;
            
            /// <summary>
            /// Service Sub-location
            /// </summary>
            [FhirElement("subSite", Order=220)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SubSite
            {
                get { if(_SubSite==null) _SubSite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubSite; }
                set { _SubSite = value; OnPropertyChanged("SubSite"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SubSite;
            
            /// <summary>
            /// Encounters related to this billed item
            /// </summary>
            [FhirElement("encounter", Order=230)]
            [CLSCompliant(false)]
            [References("Encounter")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Encounter
            {
                get { if(_Encounter==null) _Encounter = new List<Hl7.Fhir.Model.ResourceReference>(); return _Encounter; }
                set { _Encounter = value; OnPropertyChanged("Encounter"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Encounter;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=240)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null;
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=250)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("detail", Order=260)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<DetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<DetailComponent> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ItemComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.BeginList("careTeamLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(CareTeamLinkIdElement);
                sink.End();
                sink.BeginList("diagnosisLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(DiagnosisLinkIdElement);
                sink.End();
                sink.BeginList("procedureLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(ProcedureLinkIdElement);
                sink.End();
                sink.BeginList("informationLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(InformationLinkIdElement);
                sink.End();
                sink.Element("revenue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Revenue?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Service?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("programCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ProgramCode)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("serviced", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Serviced?.Serialize(sink);
                sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Location?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.BeginList("udi", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Udi)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BodySite?.Serialize(sink);
                sink.BeginList("subSite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SubSite)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Encounter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Detail)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "careTeamLinkId":
                        CareTeamLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "diagnosisLinkId":
                        DiagnosisLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "procedureLinkId":
                        ProcedureLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "informationLinkId":
                        InformationLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "revenue":
                        Revenue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "programCode":
                        ProgramCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "servicedDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                        Serviced = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "servicedPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Serviced, "serviced");
                        Serviced = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "locationCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "locationAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.Address>();
                        return true;
                    case "locationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "factor":
                        FactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "net":
                        Net = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "udi":
                        Udi = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "bodySite":
                        BodySite = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "subSite":
                        SubSite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "encounter":
                        Encounter = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
                        return true;
                    case "detail":
                        Detail = source.GetList<DetailComponent>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "careTeamLinkId":
                    case "_careTeamLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "diagnosisLinkId":
                    case "_diagnosisLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "procedureLinkId":
                    case "_procedureLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "informationLinkId":
                    case "_informationLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "revenue":
                        Revenue = source.Populate(Revenue);
                        return true;
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "programCode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "servicedDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                        Serviced = source.PopulateValue(Serviced as Hl7.Fhir.Model.Date);
                        return true;
                    case "_servicedDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                        Serviced = source.Populate(Serviced as Hl7.Fhir.Model.Date);
                        return true;
                    case "servicedPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Serviced, "serviced");
                        Serviced = source.Populate(Serviced as Hl7.Fhir.Model.Period);
                        return true;
                    case "locationCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "locationAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.Address);
                        return true;
                    case "locationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                    case "udi":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "bodySite":
                        BodySite = source.Populate(BodySite);
                        return true;
                    case "subSite":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "encounter":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "detail":
                        source.SetList(this, jsonPropertyName);
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
                    case "careTeamLinkId":
                        source.PopulatePrimitiveListItemValue(CareTeamLinkIdElement, index);
                        return true;
                    case "_careTeamLinkId":
                        source.PopulatePrimitiveListItem(CareTeamLinkIdElement, index);
                        return true;
                    case "diagnosisLinkId":
                        source.PopulatePrimitiveListItemValue(DiagnosisLinkIdElement, index);
                        return true;
                    case "_diagnosisLinkId":
                        source.PopulatePrimitiveListItem(DiagnosisLinkIdElement, index);
                        return true;
                    case "procedureLinkId":
                        source.PopulatePrimitiveListItemValue(ProcedureLinkIdElement, index);
                        return true;
                    case "_procedureLinkId":
                        source.PopulatePrimitiveListItem(ProcedureLinkIdElement, index);
                        return true;
                    case "informationLinkId":
                        source.PopulatePrimitiveListItemValue(InformationLinkIdElement, index);
                        return true;
                    case "_informationLinkId":
                        source.PopulatePrimitiveListItem(InformationLinkIdElement, index);
                        return true;
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "programCode":
                        source.PopulateListItem(ProgramCode, index);
                        return true;
                    case "udi":
                        source.PopulateListItem(Udi, index);
                        return true;
                    case "subSite":
                        source.PopulateListItem(SubSite, index);
                        return true;
                    case "encounter":
                        source.PopulateListItem(Encounter, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(CareTeamLinkIdElement != null) dest.CareTeamLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(CareTeamLinkIdElement.DeepCopy());
                    if(DiagnosisLinkIdElement != null) dest.DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(DiagnosisLinkIdElement.DeepCopy());
                    if(ProcedureLinkIdElement != null) dest.ProcedureLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(ProcedureLinkIdElement.DeepCopy());
                    if(InformationLinkIdElement != null) dest.InformationLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(InformationLinkIdElement.DeepCopy());
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.STU3.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.STU3.Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                    if(SubSite != null) dest.SubSite = new List<Hl7.Fhir.Model.CodeableConcept>(SubSite.DeepCopy());
                    if(Encounter != null) dest.Encounter = new List<Hl7.Fhir.Model.ResourceReference>(Encounter.DeepCopy());
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<DetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(CareTeamLinkIdElement, otherT.CareTeamLinkIdElement)) return false;
                if( !DeepComparable.Matches(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.Matches(ProcedureLinkIdElement, otherT.ProcedureLinkIdElement)) return false;
                if( !DeepComparable.Matches(InformationLinkIdElement, otherT.InformationLinkIdElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(CareTeamLinkIdElement, otherT.CareTeamLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(ProcedureLinkIdElement, otherT.ProcedureLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(InformationLinkIdElement, otherT.InformationLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    foreach (var elem in CareTeamLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in DiagnosisLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ProcedureLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in InformationLinkIdElement) { if (elem != null) yield return elem; }
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Serviced != null) yield return Serviced;
                    if (Location != null) yield return Location;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in Udi) { if (elem != null) yield return elem; }
                    if (BodySite != null) yield return BodySite;
                    foreach (var elem in SubSite) { if (elem != null) yield return elem; }
                    foreach (var elem in Encounter) { if (elem != null) yield return elem; }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    foreach (var elem in CareTeamLinkIdElement) { if (elem != null) yield return new ElementValue("careTeamLinkId", elem); }
                    foreach (var elem in DiagnosisLinkIdElement) { if (elem != null) yield return new ElementValue("diagnosisLinkId", elem); }
                    foreach (var elem in ProcedureLinkIdElement) { if (elem != null) yield return new ElementValue("procedureLinkId", elem); }
                    foreach (var elem in InformationLinkIdElement) { if (elem != null) yield return new ElementValue("informationLinkId", elem); }
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Serviced != null) yield return new ElementValue("serviced", Serviced);
                    if (Location != null) yield return new ElementValue("location", Location);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in Udi) { if (elem != null) yield return new ElementValue("udi", elem); }
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    foreach (var elem in SubSite) { if (elem != null) yield return new ElementValue("subSite", elem); }
                    foreach (var elem in Encounter) { if (elem != null) yield return new ElementValue("encounter", elem); }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AdjudicationComponent")]
        [DataContract]
        public partial class AdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Explanation of Adjudication outcome
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Amount;
            
            /// <summary>
            /// Non-monitory value
            /// </summary>
            [FhirElement("value", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monitory value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AdjudicationComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Category?.Serialize(sink);
                sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reason?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValueElement?.Serialize(sink);
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "reason":
                        Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "reason":
                        Reason = source.Populate(Reason);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.STU3.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (Reason != null) yield return Reason;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Group or type of product or service
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Total additional item cost
            /// </summary>
            [FhirElement("net", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=150)]
            [CLSCompliant(false)]
            [References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null;
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Detail level adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("subDetail", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SubDetailComponent> SubDetail
            {
                get { if(_SubDetail==null) _SubDetail = new List<SubDetailComponent>(); return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            
            private List<SubDetailComponent> _SubDetail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DetailComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("revenue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Revenue?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Service?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("programCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ProgramCode)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.BeginList("udi", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Udi)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("subDetail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SubDetail)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "revenue":
                        Revenue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "programCode":
                        ProgramCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "factor":
                        FactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "net":
                        Net = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "udi":
                        Udi = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
                        return true;
                    case "subDetail":
                        SubDetail = source.GetList<SubDetailComponent>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "revenue":
                        Revenue = source.Populate(Revenue);
                        return true;
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "programCode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                    case "udi":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "subDetail":
                        source.SetList(this, jsonPropertyName);
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
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "programCode":
                        source.PopulateListItem(ProgramCode, index);
                        return true;
                    case "udi":
                        source.PopulateListItem(Udi, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                    case "subDetail":
                        source.PopulateListItem(SubDetail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.STU3.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.STU3.Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    if(SubDetail != null) dest.SubDetail = new List<SubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(SubDetail, otherT.SubDetail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Type != null) yield return Type;
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in Udi) { if (elem != null) yield return elem; }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in SubDetail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in Udi) { if (elem != null) yield return new ElementValue("udi", elem); }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in SubDetail) { if (elem != null) yield return new ElementValue("subDetail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubDetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Type of product or service
            /// </summary>
            [FhirElement("type", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Net additional item cost
            /// </summary>
            [FhirElement("net", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=150)]
            [CLSCompliant(false)]
            [References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null;
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Language if different from the resource
            /// </summary>
            [FhirElement("adjudication", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubDetailComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("revenue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Revenue?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Service?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("programCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ProgramCode)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.BeginList("udi", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Udi)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "revenue":
                        Revenue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "programCode":
                        ProgramCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "factor":
                        FactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "net":
                        Net = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "udi":
                        Udi = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "revenue":
                        Revenue = source.Populate(Revenue);
                        return true;
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "programCode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                    case "udi":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
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
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "programCode":
                        source.PopulateListItem(ProgramCode, index);
                        return true;
                    case "udi":
                        source.PopulateListItem(Udi, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.STU3.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.STU3.Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (Type != null) yield return Type;
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in Udi) { if (elem != null) yield return elem; }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in Udi) { if (elem != null) yield return new ElementValue("udi", elem); }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AddedItemComponent")]
        [DataContract]
        public partial class AddedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemComponent"; } }
            
            /// <summary>
            /// Service instances
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> SequenceLinkIdElement
            {
                get { if(_SequenceLinkIdElement==null) _SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null;
                    else
                        SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Fee;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null;
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Added items adjudication
            /// </summary>
            [FhirElement("adjudication", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Added items details
            /// </summary>
            [FhirElement("detail", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AddedItemsDetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<AddedItemsDetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<AddedItemsDetailComponent> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemComponent");
                base.Serialize(sink);
                sink.BeginList("sequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(SequenceLinkIdElement);
                sink.End();
                sink.Element("revenue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Revenue?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Service?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("fee", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Fee?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Detail)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "revenue":
                        Revenue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "fee":
                        Fee = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
                        return true;
                    case "detail":
                        Detail = source.GetList<AddedItemsDetailComponent>();
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
                    case "sequenceLinkId":
                    case "_sequenceLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "revenue":
                        Revenue = source.Populate(Revenue);
                        return true;
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "fee":
                        Fee = source.Populate(Fee);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "detail":
                        source.SetList(this, jsonPropertyName);
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
                    case "sequenceLinkId":
                        source.PopulatePrimitiveListItemValue(SequenceLinkIdElement, index);
                        return true;
                    case "_sequenceLinkId":
                        source.PopulatePrimitiveListItem(SequenceLinkIdElement, index);
                        return true;
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(SequenceLinkIdElement.DeepCopy());
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.STU3.Money)Fee.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<AddedItemsDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in SequenceLinkIdElement) { if (elem != null) yield return elem; }
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Fee != null) yield return Fee;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in SequenceLinkIdElement) { if (elem != null) yield return new ElementValue("sequenceLinkId", elem); }
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Fee != null) yield return new ElementValue("fee", Fee);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AddedItemsDetailComponent")]
        [DataContract]
        public partial class AddedItemsDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemsDetailComponent"; } }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Fee;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null;
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Added items detail adjudication
            /// </summary>
            [FhirElement("adjudication", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemsDetailComponent");
                base.Serialize(sink);
                sink.Element("revenue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Revenue?.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Service?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("fee", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Fee?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "revenue":
                        Revenue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "fee":
                        Fee = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
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
                    case "revenue":
                        Revenue = source.Populate(Revenue);
                        return true;
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "fee":
                        Fee = source.Populate(Fee);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
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
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemsDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.STU3.Money)Fee.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemsDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemsDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemsDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Fee != null) yield return Fee;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Fee != null) yield return new ElementValue("fee", Fee);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "PaymentComponent")]
        [DataContract]
        public partial class PaymentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PaymentComponent"; } }
            
            /// <summary>
            /// Partial or Complete
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Payment adjustment for non-Claim issues
            /// </summary>
            [FhirElement("adjustment", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Adjustment
            {
                get { return _Adjustment; }
                set { _Adjustment = value; OnPropertyChanged("Adjustment"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Adjustment;
            
            /// <summary>
            /// Explanation for the non-claim adjustment
            /// </summary>
            [FhirElement("adjustmentReason", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdjustmentReason
            {
                get { return _AdjustmentReason; }
                set { _AdjustmentReason = value; OnPropertyChanged("AdjustmentReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AdjustmentReason;
            
            /// <summary>
            /// Expected date of Payment
            /// </summary>
            [FhirElement("date", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// Expected date of Payment
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if (value == null)
                        DateElement = null;
                    else
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Payable amount after adjustment
            /// </summary>
            [FhirElement("amount", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.STU3.Money _Amount;
            
            /// <summary>
            /// Identifier of the payment instrument
            /// </summary>
            [FhirElement("identifier", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PaymentComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("adjustment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Adjustment?.Serialize(sink);
                sink.Element("adjustmentReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AdjustmentReason?.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DateElement?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Amount?.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "adjustment":
                        Adjustment = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "adjustmentReason":
                        AdjustmentReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "date":
                        DateElement = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "identifier":
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "adjustment":
                        Adjustment = source.Populate(Adjustment);
                        return true;
                    case "adjustmentReason":
                        AdjustmentReason = source.Populate(AdjustmentReason);
                        return true;
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "identifier":
                        Identifier = source.Populate(Identifier);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PaymentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Adjustment != null) dest.Adjustment = (Hl7.Fhir.Model.STU3.Money)Adjustment.DeepCopy();
                    if(AdjustmentReason != null) dest.AdjustmentReason = (Hl7.Fhir.Model.CodeableConcept)AdjustmentReason.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.STU3.Money)Amount.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PaymentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PaymentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Adjustment, otherT.Adjustment)) return false;
                if( !DeepComparable.Matches(AdjustmentReason, otherT.AdjustmentReason)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PaymentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Adjustment, otherT.Adjustment)) return false;
                if( !DeepComparable.IsExactly(AdjustmentReason, otherT.AdjustmentReason)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Adjustment != null) yield return Adjustment;
                    if (AdjustmentReason != null) yield return AdjustmentReason;
                    if (DateElement != null) yield return DateElement;
                    if (Amount != null) yield return Amount;
                    if (Identifier != null) yield return Identifier;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Adjustment != null) yield return new ElementValue("adjustment", Adjustment);
                    if (AdjustmentReason != null) yield return new ElementValue("adjustmentReason", AdjustmentReason);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "NoteComponent")]
        [DataContract]
        public partial class NoteComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NoteComponent"; } }
            
            /// <summary>
            /// Sequence number for this note
            /// </summary>
            [FhirElement("number", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _NumberElement;
            
            /// <summary>
            /// Sequence number for this note
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberElement = null;
                    else
                        NumberElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// display | print | printoper
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Note explanitory text
            /// </summary>
            [FhirElement("text", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Note explanitory text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null;
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Language if different from the resource
            /// </summary>
            [FhirElement("language", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Language
            {
                get { return _Language; }
                set { _Language = value; OnPropertyChanged("Language"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Language;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NoteComponent");
                base.Serialize(sink);
                sink.Element("number", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NumberElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TextElement?.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Language?.Serialize(sink);
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
                    case "number":
                        NumberElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "text":
                        TextElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "language":
                        Language = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "number":
                        NumberElement = source.PopulateValue(NumberElement);
                        return true;
                    case "_number":
                        NumberElement = source.Populate(NumberElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                    case "language":
                        Language = source.Populate(Language);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NoteComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.PositiveInt)NumberElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Language != null) dest.Language = (Hl7.Fhir.Model.CodeableConcept)Language.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NoteComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NoteComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Language, otherT.Language)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NoteComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Language, otherT.Language)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NumberElement != null) yield return NumberElement;
                    if (Type != null) yield return Type;
                    if (TextElement != null) yield return TextElement;
                    if (Language != null) yield return Language;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (Language != null) yield return new ElementValue("language", Language);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "BenefitBalanceComponent")]
        [DataContract]
        public partial class BenefitBalanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BenefitBalanceComponent"; } }
            
            /// <summary>
            /// Type of services covered
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Detailed services covered within the type
            /// </summary>
            [FhirElement("subCategory", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SubCategory
            {
                get { return _SubCategory; }
                set { _SubCategory = value; OnPropertyChanged("SubCategory"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SubCategory;
            
            /// <summary>
            /// Excluded from the plan
            /// </summary>
            [FhirElement("excluded", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ExcludedElement
            {
                get { return _ExcludedElement; }
                set { _ExcludedElement = value; OnPropertyChanged("ExcludedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ExcludedElement;
            
            /// <summary>
            /// Excluded from the plan
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Excluded
            {
                get { return ExcludedElement != null ? ExcludedElement.Value : null; }
                set
                {
                    if (value == null)
                        ExcludedElement = null;
                    else
                        ExcludedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Excluded");
                }
            }
            
            /// <summary>
            /// Short name for the benefit
            /// </summary>
            [FhirElement("name", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Short name for the benefit
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
            /// Description of the benefit or services covered
            /// </summary>
            [FhirElement("description", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of the benefit or services covered
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
            /// In or out of network
            /// </summary>
            [FhirElement("network", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Network
            {
                get { return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Network;
            
            /// <summary>
            /// Individual or family
            /// </summary>
            [FhirElement("unit", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Unit
            {
                get { return _Unit; }
                set { _Unit = value; OnPropertyChanged("Unit"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Unit;
            
            /// <summary>
            /// Annual or lifetime
            /// </summary>
            [FhirElement("term", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Term
            {
                get { return _Term; }
                set { _Term = value; OnPropertyChanged("Term"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Term;
            
            /// <summary>
            /// Benefit Summary
            /// </summary>
            [FhirElement("financial", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<BenefitComponent> Financial
            {
                get { if(_Financial==null) _Financial = new List<BenefitComponent>(); return _Financial; }
                set { _Financial = value; OnPropertyChanged("Financial"); }
            }
            
            private List<BenefitComponent> _Financial;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BenefitBalanceComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Category?.Serialize(sink);
                sink.Element("subCategory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubCategory?.Serialize(sink);
                sink.Element("excluded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExcludedElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("network", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Network?.Serialize(sink);
                sink.Element("unit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Unit?.Serialize(sink);
                sink.Element("term", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Term?.Serialize(sink);
                sink.BeginList("financial", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Financial)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "subCategory":
                        SubCategory = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "excluded":
                        ExcludedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "network":
                        Network = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "unit":
                        Unit = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "term":
                        Term = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "financial":
                        Financial = source.GetList<BenefitComponent>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "subCategory":
                        SubCategory = source.Populate(SubCategory);
                        return true;
                    case "excluded":
                        ExcludedElement = source.PopulateValue(ExcludedElement);
                        return true;
                    case "_excluded":
                        ExcludedElement = source.Populate(ExcludedElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "network":
                        Network = source.Populate(Network);
                        return true;
                    case "unit":
                        Unit = source.Populate(Unit);
                        return true;
                    case "term":
                        Term = source.Populate(Term);
                        return true;
                    case "financial":
                        source.SetList(this, jsonPropertyName);
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
                    case "financial":
                        source.PopulateListItem(Financial, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BenefitBalanceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(SubCategory != null) dest.SubCategory = (Hl7.Fhir.Model.CodeableConcept)SubCategory.DeepCopy();
                    if(ExcludedElement != null) dest.ExcludedElement = (Hl7.Fhir.Model.FhirBoolean)ExcludedElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Network != null) dest.Network = (Hl7.Fhir.Model.CodeableConcept)Network.DeepCopy();
                    if(Unit != null) dest.Unit = (Hl7.Fhir.Model.CodeableConcept)Unit.DeepCopy();
                    if(Term != null) dest.Term = (Hl7.Fhir.Model.CodeableConcept)Term.DeepCopy();
                    if(Financial != null) dest.Financial = new List<BenefitComponent>(Financial.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new BenefitBalanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BenefitBalanceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(SubCategory, otherT.SubCategory)) return false;
                if( !DeepComparable.Matches(ExcludedElement, otherT.ExcludedElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(Unit, otherT.Unit)) return false;
                if( !DeepComparable.Matches(Term, otherT.Term)) return false;
                if( !DeepComparable.Matches(Financial, otherT.Financial)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BenefitBalanceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(SubCategory, otherT.SubCategory)) return false;
                if( !DeepComparable.IsExactly(ExcludedElement, otherT.ExcludedElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(Unit, otherT.Unit)) return false;
                if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
                if( !DeepComparable.IsExactly(Financial, otherT.Financial)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (SubCategory != null) yield return SubCategory;
                    if (ExcludedElement != null) yield return ExcludedElement;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Network != null) yield return Network;
                    if (Unit != null) yield return Unit;
                    if (Term != null) yield return Term;
                    foreach (var elem in Financial) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (SubCategory != null) yield return new ElementValue("subCategory", SubCategory);
                    if (ExcludedElement != null) yield return new ElementValue("excluded", ExcludedElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Network != null) yield return new ElementValue("network", Network);
                    if (Unit != null) yield return new ElementValue("unit", Unit);
                    if (Term != null) yield return new ElementValue("term", Term);
                    foreach (var elem in Financial) { if (elem != null) yield return new ElementValue("financial", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "BenefitComponent")]
        [DataContract]
        public partial class BenefitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BenefitComponent"; } }
            
            /// <summary>
            /// Deductable, visits, benefit amount
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Benefits allowed
            /// </summary>
            [FhirElement("allowed", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.STU3.Money))]
            [DataMember]
            public Hl7.Fhir.Model.Element Allowed
            {
                get { return _Allowed; }
                set { _Allowed = value; OnPropertyChanged("Allowed"); }
            }
            
            private Hl7.Fhir.Model.Element _Allowed;
            
            /// <summary>
            /// Benefits used
            /// </summary>
            [FhirElement("used", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.STU3.Money))]
            [DataMember]
            public Hl7.Fhir.Model.Element Used
            {
                get { return _Used; }
                set { _Used = value; OnPropertyChanged("Used"); }
            }
            
            private Hl7.Fhir.Model.Element _Used;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BenefitComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("allowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Allowed?.Serialize(sink);
                sink.Element("used", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Used?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "allowedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "allowedString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "allowedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "usedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Used, "used");
                        Used = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "usedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Used, "used");
                        Used = source.Get<Hl7.Fhir.Model.STU3.Money>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "allowedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Allowed, "allowed");
                        Allowed = source.PopulateValue(Allowed as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_allowedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "allowedString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Allowed, "allowed");
                        Allowed = source.PopulateValue(Allowed as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_allowedString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "allowedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.STU3.Money);
                        return true;
                    case "usedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Used, "used");
                        Used = source.PopulateValue(Used as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_usedUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Used, "used");
                        Used = source.Populate(Used as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "usedMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Used, "used");
                        Used = source.Populate(Used as Hl7.Fhir.Model.STU3.Money);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BenefitComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Allowed != null) dest.Allowed = (Hl7.Fhir.Model.Element)Allowed.DeepCopy();
                    if(Used != null) dest.Used = (Hl7.Fhir.Model.Element)Used.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new BenefitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BenefitComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Allowed, otherT.Allowed)) return false;
                if( !DeepComparable.Matches(Used, otherT.Used)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BenefitComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Allowed, otherT.Allowed)) return false;
                if( !DeepComparable.IsExactly(Used, otherT.Used)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Allowed != null) yield return Allowed;
                    if (Used != null) yield return Used;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Allowed != null) yield return new ElementValue("allowed", Allowed);
                    if (Used != null) yield return new ElementValue("used", Used);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Business Identifier
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ExplanationOfBenefitStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ExplanationOfBenefitStatus> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ExplanationOfBenefitStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.ExplanationOfBenefitStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Type or discipline
        /// </summary>
        [FhirElement("type", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Finer grained claim type information
        /// </summary>
        [FhirElement("subType", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> SubType
        {
            get { if(_SubType==null) _SubType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubType; }
            set { _SubType = value; OnPropertyChanged("SubType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _SubType;
        
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Order=130)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Period for charge submission
        /// </summary>
        [FhirElement("billablePeriod", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Period BillablePeriod
        {
            get { return _BillablePeriod; }
            set { _BillablePeriod = value; OnPropertyChanged("BillablePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _BillablePeriod;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                    CreatedElement = null;
                else
                    CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Author
        /// </summary>
        [FhirElement("enterer", Order=160)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// Insurer responsible for the EOB
        /// </summary>
        [FhirElement("insurer", Order=170)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Insurer;
        
        /// <summary>
        /// Responsible provider for the claim
        /// </summary>
        [FhirElement("provider", Order=180)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Provider;
        
        /// <summary>
        /// Responsible organization for the claim
        /// </summary>
        [FhirElement("organization", Order=190)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// Treatment Referral
        /// </summary>
        [FhirElement("referral", Order=200)]
        [CLSCompliant(false)]
        [References("ReferralRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referral
        {
            get { return _Referral; }
            set { _Referral = value; OnPropertyChanged("Referral"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Referral;
        
        /// <summary>
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", Order=210)]
        [CLSCompliant(false)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Facility
        {
            get { return _Facility; }
            set { _Facility = value; OnPropertyChanged("Facility"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Facility;
        
        /// <summary>
        /// Claim reference
        /// </summary>
        [FhirElement("claim", Order=220)]
        [CLSCompliant(false)]
        [References("Claim")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Claim
        {
            get { return _Claim; }
            set { _Claim = value; OnPropertyChanged("Claim"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Claim;
        
        /// <summary>
        /// Claim response reference
        /// </summary>
        [FhirElement("claimResponse", Order=230)]
        [CLSCompliant(false)]
        [References("ClaimResponse")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ClaimResponse
        {
            get { return _ClaimResponse; }
            set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
        
        /// <summary>
        /// complete | error | partial
        /// </summary>
        [FhirElement("outcome", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DispositionElement
        {
            get { return _DispositionElement; }
            set { _DispositionElement = value; OnPropertyChanged("DispositionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DispositionElement;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Disposition
        {
            get { return DispositionElement != null ? DispositionElement.Value : null; }
            set
            {
                if (value == null)
                    DispositionElement = null;
                else
                    DispositionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Disposition");
            }
        }
        
        /// <summary>
        /// Related Claims which may be revelant to processing this claim
        /// </summary>
        [FhirElement("related", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedClaimComponent> Related
        {
            get { if(_Related==null) _Related = new List<RelatedClaimComponent>(); return _Related; }
            set { _Related = value; OnPropertyChanged("Related"); }
        }
        
        private List<RelatedClaimComponent> _Related;
        
        /// <summary>
        /// Prescription authorizing services or products
        /// </summary>
        [FhirElement("prescription", Order=270)]
        [CLSCompliant(false)]
        [References("MedicationRequest","VisionPrescription")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescription
        {
            get { return _Prescription; }
            set { _Prescription = value; OnPropertyChanged("Prescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Prescription;
        
        /// <summary>
        /// Original prescription if superceded by fulfiller
        /// </summary>
        [FhirElement("originalPrescription", Order=280)]
        [CLSCompliant(false)]
        [References("MedicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OriginalPrescription
        {
            get { return _OriginalPrescription; }
            set { _OriginalPrescription = value; OnPropertyChanged("OriginalPrescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OriginalPrescription;
        
        /// <summary>
        /// Party to be paid any benefits payable
        /// </summary>
        [FhirElement("payee", Order=290)]
        [DataMember]
        public PayeeComponent Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged("Payee"); }
        }
        
        private PayeeComponent _Payee;
        
        /// <summary>
        /// Exceptions, special considerations, the condition, situation, prior or concurrent issues
        /// </summary>
        [FhirElement("information", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SupportingInformationComponent> Information
        {
            get { if(_Information==null) _Information = new List<SupportingInformationComponent>(); return _Information; }
            set { _Information = value; OnPropertyChanged("Information"); }
        }
        
        private List<SupportingInformationComponent> _Information;
        
        /// <summary>
        /// Care Team members
        /// </summary>
        [FhirElement("careTeam", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CareTeamComponent> CareTeam
        {
            get { if(_CareTeam==null) _CareTeam = new List<CareTeamComponent>(); return _CareTeam; }
            set { _CareTeam = value; OnPropertyChanged("CareTeam"); }
        }
        
        private List<CareTeamComponent> _CareTeam;
        
        /// <summary>
        /// List of Diagnosis
        /// </summary>
        [FhirElement("diagnosis", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DiagnosisComponent> Diagnosis
        {
            get { if(_Diagnosis==null) _Diagnosis = new List<DiagnosisComponent>(); return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        
        private List<DiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// Procedures performed
        /// </summary>
        [FhirElement("procedure", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ProcedureComponent> Procedure
        {
            get { if(_Procedure==null) _Procedure = new List<ProcedureComponent>(); return _Procedure; }
            set { _Procedure = value; OnPropertyChanged("Procedure"); }
        }
        
        private List<ProcedureComponent> _Procedure;
        
        /// <summary>
        /// Precedence (primary, secondary, etc.)
        /// </summary>
        [FhirElement("precedence", Order=340)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt PrecedenceElement
        {
            get { return _PrecedenceElement; }
            set { _PrecedenceElement = value; OnPropertyChanged("PrecedenceElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _PrecedenceElement;
        
        /// <summary>
        /// Precedence (primary, secondary, etc.)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Precedence
        {
            get { return PrecedenceElement != null ? PrecedenceElement.Value : null; }
            set
            {
                if (value == null)
                    PrecedenceElement = null;
                else
                    PrecedenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Precedence");
            }
        }
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("insurance", Order=350)]
        [DataMember]
        public InsuranceComponent Insurance
        {
            get { return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private InsuranceComponent _Insurance;
        
        /// <summary>
        /// Details of an accident
        /// </summary>
        [FhirElement("accident", Order=360)]
        [DataMember]
        public AccidentComponent Accident
        {
            get { return _Accident; }
            set { _Accident = value; OnPropertyChanged("Accident"); }
        }
        
        private AccidentComponent _Accident;
        
        /// <summary>
        /// Period unable to work
        /// </summary>
        [FhirElement("employmentImpacted", Order=370)]
        [DataMember]
        public Hl7.Fhir.Model.Period EmploymentImpacted
        {
            get { return _EmploymentImpacted; }
            set { _EmploymentImpacted = value; OnPropertyChanged("EmploymentImpacted"); }
        }
        
        private Hl7.Fhir.Model.Period _EmploymentImpacted;
        
        /// <summary>
        /// Period in hospital
        /// </summary>
        [FhirElement("hospitalization", Order=380)]
        [DataMember]
        public Hl7.Fhir.Model.Period Hospitalization
        {
            get { return _Hospitalization; }
            set { _Hospitalization = value; OnPropertyChanged("Hospitalization"); }
        }
        
        private Hl7.Fhir.Model.Period _Hospitalization;
        
        /// <summary>
        /// Goods and Services
        /// </summary>
        [FhirElement("item", Order=390)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<ItemComponent> _Item;
        
        /// <summary>
        /// Insurer added line items
        /// </summary>
        [FhirElement("addItem", Order=400)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AddedItemComponent> AddItem
        {
            get { if(_AddItem==null) _AddItem = new List<AddedItemComponent>(); return _AddItem; }
            set { _AddItem = value; OnPropertyChanged("AddItem"); }
        }
        
        private List<AddedItemComponent> _AddItem;
        
        /// <summary>
        /// Total Cost of service from the Claim
        /// </summary>
        [FhirElement("totalCost", Order=410)]
        [DataMember]
        public Hl7.Fhir.Model.STU3.Money TotalCost
        {
            get { return _TotalCost; }
            set { _TotalCost = value; OnPropertyChanged("TotalCost"); }
        }
        
        private Hl7.Fhir.Model.STU3.Money _TotalCost;
        
        /// <summary>
        /// Unallocated deductable
        /// </summary>
        [FhirElement("unallocDeductable", Order=420)]
        [DataMember]
        public Hl7.Fhir.Model.STU3.Money UnallocDeductable
        {
            get { return _UnallocDeductable; }
            set { _UnallocDeductable = value; OnPropertyChanged("UnallocDeductable"); }
        }
        
        private Hl7.Fhir.Model.STU3.Money _UnallocDeductable;
        
        /// <summary>
        /// Total benefit payable for the Claim
        /// </summary>
        [FhirElement("totalBenefit", Order=430)]
        [DataMember]
        public Hl7.Fhir.Model.STU3.Money TotalBenefit
        {
            get { return _TotalBenefit; }
            set { _TotalBenefit = value; OnPropertyChanged("TotalBenefit"); }
        }
        
        private Hl7.Fhir.Model.STU3.Money _TotalBenefit;
        
        /// <summary>
        /// Payment (if paid)
        /// </summary>
        [FhirElement("payment", Order=440)]
        [DataMember]
        public PaymentComponent Payment
        {
            get { return _Payment; }
            set { _Payment = value; OnPropertyChanged("Payment"); }
        }
        
        private PaymentComponent _Payment;
        
        /// <summary>
        /// Printed Form Identifier
        /// </summary>
        [FhirElement("form", Order=450)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Form;
        
        /// <summary>
        /// Processing notes
        /// </summary>
        [FhirElement("processNote", Order=460)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<NoteComponent> ProcessNote
        {
            get { if(_ProcessNote==null) _ProcessNote = new List<NoteComponent>(); return _ProcessNote; }
            set { _ProcessNote = value; OnPropertyChanged("ProcessNote"); }
        }
        
        private List<NoteComponent> _ProcessNote;
        
        /// <summary>
        /// Balance by Benefit Category
        /// </summary>
        [FhirElement("benefitBalance", Order=470)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<BenefitBalanceComponent> BenefitBalance
        {
            get { if(_BenefitBalance==null) _BenefitBalance = new List<BenefitBalanceComponent>(); return _BenefitBalance; }
            set { _BenefitBalance = value; OnPropertyChanged("BenefitBalance"); }
        }
        
        private List<BenefitBalanceComponent> _BenefitBalance;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ExplanationOfBenefit;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ExplanationOfBenefitStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SubType != null) dest.SubType = new List<Hl7.Fhir.Model.CodeableConcept>(SubType.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(BillablePeriod != null) dest.BillablePeriod = (Hl7.Fhir.Model.Period)BillablePeriod.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(Referral != null) dest.Referral = (Hl7.Fhir.Model.ResourceReference)Referral.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.ResourceReference)Facility.DeepCopy();
                if(Claim != null) dest.Claim = (Hl7.Fhir.Model.ResourceReference)Claim.DeepCopy();
                if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(Related != null) dest.Related = new List<RelatedClaimComponent>(Related.DeepCopy());
                if(Prescription != null) dest.Prescription = (Hl7.Fhir.Model.ResourceReference)Prescription.DeepCopy();
                if(OriginalPrescription != null) dest.OriginalPrescription = (Hl7.Fhir.Model.ResourceReference)OriginalPrescription.DeepCopy();
                if(Payee != null) dest.Payee = (PayeeComponent)Payee.DeepCopy();
                if(Information != null) dest.Information = new List<SupportingInformationComponent>(Information.DeepCopy());
                if(CareTeam != null) dest.CareTeam = new List<CareTeamComponent>(CareTeam.DeepCopy());
                if(Diagnosis != null) dest.Diagnosis = new List<DiagnosisComponent>(Diagnosis.DeepCopy());
                if(Procedure != null) dest.Procedure = new List<ProcedureComponent>(Procedure.DeepCopy());
                if(PrecedenceElement != null) dest.PrecedenceElement = (Hl7.Fhir.Model.PositiveInt)PrecedenceElement.DeepCopy();
                if(Insurance != null) dest.Insurance = (InsuranceComponent)Insurance.DeepCopy();
                if(Accident != null) dest.Accident = (AccidentComponent)Accident.DeepCopy();
                if(EmploymentImpacted != null) dest.EmploymentImpacted = (Hl7.Fhir.Model.Period)EmploymentImpacted.DeepCopy();
                if(Hospitalization != null) dest.Hospitalization = (Hl7.Fhir.Model.Period)Hospitalization.DeepCopy();
                if(Item != null) dest.Item = new List<ItemComponent>(Item.DeepCopy());
                if(AddItem != null) dest.AddItem = new List<AddedItemComponent>(AddItem.DeepCopy());
                if(TotalCost != null) dest.TotalCost = (Hl7.Fhir.Model.STU3.Money)TotalCost.DeepCopy();
                if(UnallocDeductable != null) dest.UnallocDeductable = (Hl7.Fhir.Model.STU3.Money)UnallocDeductable.DeepCopy();
                if(TotalBenefit != null) dest.TotalBenefit = (Hl7.Fhir.Model.STU3.Money)TotalBenefit.DeepCopy();
                if(Payment != null) dest.Payment = (PaymentComponent)Payment.DeepCopy();
                if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                if(ProcessNote != null) dest.ProcessNote = new List<NoteComponent>(ProcessNote.DeepCopy());
                if(BenefitBalance != null) dest.BenefitBalance = new List<BenefitBalanceComponent>(BenefitBalance.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ExplanationOfBenefit());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ExplanationOfBenefit;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(BillablePeriod, otherT.BillablePeriod)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Referral, otherT.Referral)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(Claim, otherT.Claim)) return false;
            if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(Related, otherT.Related)) return false;
            if( !DeepComparable.Matches(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.Matches(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
            if( !DeepComparable.Matches(Information, otherT.Information)) return false;
            if( !DeepComparable.Matches(CareTeam, otherT.CareTeam)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.Matches(PrecedenceElement, otherT.PrecedenceElement)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(Accident, otherT.Accident)) return false;
            if( !DeepComparable.Matches(EmploymentImpacted, otherT.EmploymentImpacted)) return false;
            if( !DeepComparable.Matches(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.Matches(TotalCost, otherT.TotalCost)) return false;
            if( !DeepComparable.Matches(UnallocDeductable, otherT.UnallocDeductable)) return false;
            if( !DeepComparable.Matches(TotalBenefit, otherT.TotalBenefit)) return false;
            if( !DeepComparable.Matches(Payment, otherT.Payment)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(ProcessNote, otherT.ProcessNote)) return false;
            if( !DeepComparable.Matches(BenefitBalance, otherT.BenefitBalance)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ExplanationOfBenefit;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(BillablePeriod, otherT.BillablePeriod)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Referral, otherT.Referral)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(Claim, otherT.Claim)) return false;
            if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;
            if( !DeepComparable.IsExactly(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.IsExactly(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
            if( !DeepComparable.IsExactly(Information, otherT.Information)) return false;
            if( !DeepComparable.IsExactly(CareTeam, otherT.CareTeam)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.IsExactly(PrecedenceElement, otherT.PrecedenceElement)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(Accident, otherT.Accident)) return false;
            if( !DeepComparable.IsExactly(EmploymentImpacted, otherT.EmploymentImpacted)) return false;
            if( !DeepComparable.IsExactly(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.IsExactly(TotalCost, otherT.TotalCost)) return false;
            if( !DeepComparable.IsExactly(UnallocDeductable, otherT.UnallocDeductable)) return false;
            if( !DeepComparable.IsExactly(TotalBenefit, otherT.TotalBenefit)) return false;
            if( !DeepComparable.IsExactly(Payment, otherT.Payment)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(ProcessNote, otherT.ProcessNote)) return false;
            if( !DeepComparable.IsExactly(BenefitBalance, otherT.BenefitBalance)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ExplanationOfBenefit");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
            sink.BeginList("subType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SubType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Patient?.Serialize(sink);
            sink.Element("billablePeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BillablePeriod?.Serialize(sink);
            sink.Element("created", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CreatedElement?.Serialize(sink);
            sink.Element("enterer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Enterer?.Serialize(sink);
            sink.Element("insurer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Insurer?.Serialize(sink);
            sink.Element("provider", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Provider?.Serialize(sink);
            sink.Element("organization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Organization?.Serialize(sink);
            sink.Element("referral", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Referral?.Serialize(sink);
            sink.Element("facility", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Facility?.Serialize(sink);
            sink.Element("claim", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Claim?.Serialize(sink);
            sink.Element("claimResponse", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ClaimResponse?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Outcome?.Serialize(sink);
            sink.Element("disposition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DispositionElement?.Serialize(sink);
            sink.BeginList("related", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Related)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("prescription", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Prescription?.Serialize(sink);
            sink.Element("originalPrescription", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OriginalPrescription?.Serialize(sink);
            sink.Element("payee", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Payee?.Serialize(sink);
            sink.BeginList("information", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Information)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("careTeam", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in CareTeam)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("diagnosis", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Diagnosis)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("procedure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Procedure)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("precedence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PrecedenceElement?.Serialize(sink);
            sink.Element("insurance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Insurance?.Serialize(sink);
            sink.Element("accident", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Accident?.Serialize(sink);
            sink.Element("employmentImpacted", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EmploymentImpacted?.Serialize(sink);
            sink.Element("hospitalization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Hospitalization?.Serialize(sink);
            sink.BeginList("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Item)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("addItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AddItem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("totalCost", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TotalCost?.Serialize(sink);
            sink.Element("unallocDeductable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnallocDeductable?.Serialize(sink);
            sink.Element("totalBenefit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TotalBenefit?.Serialize(sink);
            sink.Element("payment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Payment?.Serialize(sink);
            sink.Element("form", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Form?.Serialize(sink);
            sink.BeginList("processNote", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ProcessNote)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("benefitBalance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in BenefitBalance)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ExplanationOfBenefitStatus>>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subType":
                    SubType = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "billablePeriod":
                    BillablePeriod = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "created":
                    CreatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "enterer":
                    Enterer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "insurer":
                    Insurer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "provider":
                    Provider = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "organization":
                    Organization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "referral":
                    Referral = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "facility":
                    Facility = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "claim":
                    Claim = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "claimResponse":
                    ClaimResponse = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "outcome":
                    Outcome = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "disposition":
                    DispositionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "related":
                    Related = source.GetList<RelatedClaimComponent>();
                    return true;
                case "prescription":
                    Prescription = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "originalPrescription":
                    OriginalPrescription = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "payee":
                    Payee = source.Get<PayeeComponent>();
                    return true;
                case "information":
                    Information = source.GetList<SupportingInformationComponent>();
                    return true;
                case "careTeam":
                    CareTeam = source.GetList<CareTeamComponent>();
                    return true;
                case "diagnosis":
                    Diagnosis = source.GetList<DiagnosisComponent>();
                    return true;
                case "procedure":
                    Procedure = source.GetList<ProcedureComponent>();
                    return true;
                case "precedence":
                    PrecedenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "insurance":
                    Insurance = source.Get<InsuranceComponent>();
                    return true;
                case "accident":
                    Accident = source.Get<AccidentComponent>();
                    return true;
                case "employmentImpacted":
                    EmploymentImpacted = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "hospitalization":
                    Hospitalization = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "item":
                    Item = source.GetList<ItemComponent>();
                    return true;
                case "addItem":
                    AddItem = source.GetList<AddedItemComponent>();
                    return true;
                case "totalCost":
                    TotalCost = source.Get<Hl7.Fhir.Model.STU3.Money>();
                    return true;
                case "unallocDeductable":
                    UnallocDeductable = source.Get<Hl7.Fhir.Model.STU3.Money>();
                    return true;
                case "totalBenefit":
                    TotalBenefit = source.Get<Hl7.Fhir.Model.STU3.Money>();
                    return true;
                case "payment":
                    Payment = source.Get<PaymentComponent>();
                    return true;
                case "form":
                    Form = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "processNote":
                    ProcessNote = source.GetList<NoteComponent>();
                    return true;
                case "benefitBalance":
                    BenefitBalance = source.GetList<BenefitBalanceComponent>();
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "subType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "billablePeriod":
                    BillablePeriod = source.Populate(BillablePeriod);
                    return true;
                case "created":
                    CreatedElement = source.PopulateValue(CreatedElement);
                    return true;
                case "_created":
                    CreatedElement = source.Populate(CreatedElement);
                    return true;
                case "enterer":
                    Enterer = source.Populate(Enterer);
                    return true;
                case "insurer":
                    Insurer = source.Populate(Insurer);
                    return true;
                case "provider":
                    Provider = source.Populate(Provider);
                    return true;
                case "organization":
                    Organization = source.Populate(Organization);
                    return true;
                case "referral":
                    Referral = source.Populate(Referral);
                    return true;
                case "facility":
                    Facility = source.Populate(Facility);
                    return true;
                case "claim":
                    Claim = source.Populate(Claim);
                    return true;
                case "claimResponse":
                    ClaimResponse = source.Populate(ClaimResponse);
                    return true;
                case "outcome":
                    Outcome = source.Populate(Outcome);
                    return true;
                case "disposition":
                    DispositionElement = source.PopulateValue(DispositionElement);
                    return true;
                case "_disposition":
                    DispositionElement = source.Populate(DispositionElement);
                    return true;
                case "related":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "prescription":
                    Prescription = source.Populate(Prescription);
                    return true;
                case "originalPrescription":
                    OriginalPrescription = source.Populate(OriginalPrescription);
                    return true;
                case "payee":
                    Payee = source.Populate(Payee);
                    return true;
                case "information":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "careTeam":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "diagnosis":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "procedure":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "precedence":
                    PrecedenceElement = source.PopulateValue(PrecedenceElement);
                    return true;
                case "_precedence":
                    PrecedenceElement = source.Populate(PrecedenceElement);
                    return true;
                case "insurance":
                    Insurance = source.Populate(Insurance);
                    return true;
                case "accident":
                    Accident = source.Populate(Accident);
                    return true;
                case "employmentImpacted":
                    EmploymentImpacted = source.Populate(EmploymentImpacted);
                    return true;
                case "hospitalization":
                    Hospitalization = source.Populate(Hospitalization);
                    return true;
                case "item":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "addItem":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "totalCost":
                    TotalCost = source.Populate(TotalCost);
                    return true;
                case "unallocDeductable":
                    UnallocDeductable = source.Populate(UnallocDeductable);
                    return true;
                case "totalBenefit":
                    TotalBenefit = source.Populate(TotalBenefit);
                    return true;
                case "payment":
                    Payment = source.Populate(Payment);
                    return true;
                case "form":
                    Form = source.Populate(Form);
                    return true;
                case "processNote":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "benefitBalance":
                    source.SetList(this, jsonPropertyName);
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "subType":
                    source.PopulateListItem(SubType, index);
                    return true;
                case "related":
                    source.PopulateListItem(Related, index);
                    return true;
                case "information":
                    source.PopulateListItem(Information, index);
                    return true;
                case "careTeam":
                    source.PopulateListItem(CareTeam, index);
                    return true;
                case "diagnosis":
                    source.PopulateListItem(Diagnosis, index);
                    return true;
                case "procedure":
                    source.PopulateListItem(Procedure, index);
                    return true;
                case "item":
                    source.PopulateListItem(Item, index);
                    return true;
                case "addItem":
                    source.PopulateListItem(AddItem, index);
                    return true;
                case "processNote":
                    source.PopulateListItem(ProcessNote, index);
                    return true;
                case "benefitBalance":
                    source.PopulateListItem(BenefitBalance, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (Type != null) yield return Type;
                foreach (var elem in SubType) { if (elem != null) yield return elem; }
                if (Patient != null) yield return Patient;
                if (BillablePeriod != null) yield return BillablePeriod;
                if (CreatedElement != null) yield return CreatedElement;
                if (Enterer != null) yield return Enterer;
                if (Insurer != null) yield return Insurer;
                if (Provider != null) yield return Provider;
                if (Organization != null) yield return Organization;
                if (Referral != null) yield return Referral;
                if (Facility != null) yield return Facility;
                if (Claim != null) yield return Claim;
                if (ClaimResponse != null) yield return ClaimResponse;
                if (Outcome != null) yield return Outcome;
                if (DispositionElement != null) yield return DispositionElement;
                foreach (var elem in Related) { if (elem != null) yield return elem; }
                if (Prescription != null) yield return Prescription;
                if (OriginalPrescription != null) yield return OriginalPrescription;
                if (Payee != null) yield return Payee;
                foreach (var elem in Information) { if (elem != null) yield return elem; }
                foreach (var elem in CareTeam) { if (elem != null) yield return elem; }
                foreach (var elem in Diagnosis) { if (elem != null) yield return elem; }
                foreach (var elem in Procedure) { if (elem != null) yield return elem; }
                if (PrecedenceElement != null) yield return PrecedenceElement;
                if (Insurance != null) yield return Insurance;
                if (Accident != null) yield return Accident;
                if (EmploymentImpacted != null) yield return EmploymentImpacted;
                if (Hospitalization != null) yield return Hospitalization;
                foreach (var elem in Item) { if (elem != null) yield return elem; }
                foreach (var elem in AddItem) { if (elem != null) yield return elem; }
                if (TotalCost != null) yield return TotalCost;
                if (UnallocDeductable != null) yield return UnallocDeductable;
                if (TotalBenefit != null) yield return TotalBenefit;
                if (Payment != null) yield return Payment;
                if (Form != null) yield return Form;
                foreach (var elem in ProcessNote) { if (elem != null) yield return elem; }
                foreach (var elem in BenefitBalance) { if (elem != null) yield return elem; }
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
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in SubType) { if (elem != null) yield return new ElementValue("subType", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (BillablePeriod != null) yield return new ElementValue("billablePeriod", BillablePeriod);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                if (Provider != null) yield return new ElementValue("provider", Provider);
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (Referral != null) yield return new ElementValue("referral", Referral);
                if (Facility != null) yield return new ElementValue("facility", Facility);
                if (Claim != null) yield return new ElementValue("claim", Claim);
                if (ClaimResponse != null) yield return new ElementValue("claimResponse", ClaimResponse);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                foreach (var elem in Related) { if (elem != null) yield return new ElementValue("related", elem); }
                if (Prescription != null) yield return new ElementValue("prescription", Prescription);
                if (OriginalPrescription != null) yield return new ElementValue("originalPrescription", OriginalPrescription);
                if (Payee != null) yield return new ElementValue("payee", Payee);
                foreach (var elem in Information) { if (elem != null) yield return new ElementValue("information", elem); }
                foreach (var elem in CareTeam) { if (elem != null) yield return new ElementValue("careTeam", elem); }
                foreach (var elem in Diagnosis) { if (elem != null) yield return new ElementValue("diagnosis", elem); }
                foreach (var elem in Procedure) { if (elem != null) yield return new ElementValue("procedure", elem); }
                if (PrecedenceElement != null) yield return new ElementValue("precedence", PrecedenceElement);
                if (Insurance != null) yield return new ElementValue("insurance", Insurance);
                if (Accident != null) yield return new ElementValue("accident", Accident);
                if (EmploymentImpacted != null) yield return new ElementValue("employmentImpacted", EmploymentImpacted);
                if (Hospitalization != null) yield return new ElementValue("hospitalization", Hospitalization);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                foreach (var elem in AddItem) { if (elem != null) yield return new ElementValue("addItem", elem); }
                if (TotalCost != null) yield return new ElementValue("totalCost", TotalCost);
                if (UnallocDeductable != null) yield return new ElementValue("unallocDeductable", UnallocDeductable);
                if (TotalBenefit != null) yield return new ElementValue("totalBenefit", TotalBenefit);
                if (Payment != null) yield return new ElementValue("payment", Payment);
                if (Form != null) yield return new ElementValue("form", Form);
                foreach (var elem in ProcessNote) { if (elem != null) yield return new ElementValue("processNote", elem); }
                foreach (var elem in BenefitBalance) { if (elem != null) yield return new ElementValue("benefitBalance", elem); }
            }
        }
    
    }

}
