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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Sample for analysis
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Specimen", IsResource=true)]
    [DataContract]
    public partial class Specimen : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISpecimen, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Specimen; } }
        [NotMapped]
        public override string TypeName { get { return "Specimen"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CollectionComponent")]
        [DataContract]
        public partial class CollectionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ISpecimenCollectionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CollectionComponent"; } }
            
            /// <summary>
            /// Who collected the specimen
            /// </summary>
            [FhirElement("collector", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Collector
            {
                get { return _Collector; }
                set { _Collector = value; OnPropertyChanged("Collector"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Collector;
            
            /// <summary>
            /// Collection time
            /// </summary>
            [FhirElement("collected", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Collected
            {
                get { return _Collected; }
                set { _Collected = value; OnPropertyChanged("Collected"); }
            }
            
            private Hl7.Fhir.Model.Element _Collected;
            
            /// <summary>
            /// How long it took to collect specimen
            /// </summary>
            [FhirElement("duration", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Duration Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Hl7.Fhir.Model.R4.Duration _Duration;
            
            /// <summary>
            /// The quantity of specimen collected
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Technique used to perform collection
            /// </summary>
            [FhirElement("method", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Anatomical collection site
            /// </summary>
            [FhirElement("bodySite", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _BodySite;
            
            /// <summary>
            /// Whether or how long patient abstained from food and/or drink
            /// </summary>
            [FhirElement("fastingStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=100, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.R4.Duration))]
            [DataMember]
            public Hl7.Fhir.Model.Element FastingStatus
            {
                get { return _FastingStatus; }
                set { _FastingStatus = value; OnPropertyChanged("FastingStatus"); }
            }
            
            private Hl7.Fhir.Model.Element _FastingStatus;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CollectionComponent");
                base.Serialize(sink);
                sink.Element("collector", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Collector?.Serialize(sink);
                sink.Element("collected", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Collected?.Serialize(sink);
                sink.Element("duration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Duration?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Method?.Serialize(sink);
                sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BodySite?.Serialize(sink);
                sink.Element("fastingStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); FastingStatus?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CollectionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Collector != null) dest.Collector = (Hl7.Fhir.Model.ResourceReference)Collector.DeepCopy();
                    if(Collected != null) dest.Collected = (Hl7.Fhir.Model.Element)Collected.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.R4.Duration)Duration.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                    if(FastingStatus != null) dest.FastingStatus = (Hl7.Fhir.Model.Element)FastingStatus.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CollectionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CollectionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Collector, otherT.Collector)) return false;
                if( !DeepComparable.Matches(Collected, otherT.Collected)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(FastingStatus, otherT.FastingStatus)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CollectionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Collector, otherT.Collector)) return false;
                if( !DeepComparable.IsExactly(Collected, otherT.Collected)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(FastingStatus, otherT.FastingStatus)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Collector != null) yield return Collector;
                    if (Collected != null) yield return Collected;
                    if (Duration != null) yield return Duration;
                    if (Quantity != null) yield return Quantity;
                    if (Method != null) yield return Method;
                    if (BodySite != null) yield return BodySite;
                    if (FastingStatus != null) yield return FastingStatus;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Collector != null) yield return new ElementValue("collector", Collector);
                    if (Collected != null) yield return new ElementValue("collected", Collected);
                    if (Duration != null) yield return new ElementValue("duration", Duration);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (Method != null) yield return new ElementValue("method", Method);
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    if (FastingStatus != null) yield return new ElementValue("fastingStatus", FastingStatus);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ProcessingComponent")]
        [DataContract]
        public partial class ProcessingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ProcessingComponent"; } }
            
            /// <summary>
            /// Textual description of procedure
            /// </summary>
            [FhirElement("description", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Textual description of procedure
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
            /// Indicates the treatment step  applied to the specimen
            /// </summary>
            [FhirElement("procedure", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Procedure
            {
                get { return _Procedure; }
                set { _Procedure = value; OnPropertyChanged("Procedure"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Procedure;
            
            /// <summary>
            /// Material used in the processing step
            /// </summary>
            [FhirElement("additive", Order=60)]
            [CLSCompliant(false)]
            [References("Substance")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Additive
            {
                get { if(_Additive==null) _Additive = new List<Hl7.Fhir.Model.ResourceReference>(); return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Additive;
            
            /// <summary>
            /// Date and time of specimen processing
            /// </summary>
            [FhirElement("time", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Time
            {
                get { return _Time; }
                set { _Time = value; OnPropertyChanged("Time"); }
            }
            
            private Hl7.Fhir.Model.Element _Time;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ProcessingComponent");
                base.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("procedure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Procedure?.Serialize(sink);
                sink.BeginList("additive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Additive)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("time", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Time?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcessingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.CodeableConcept)Procedure.DeepCopy();
                    if(Additive != null) dest.Additive = new List<Hl7.Fhir.Model.ResourceReference>(Additive.DeepCopy());
                    if(Time != null) dest.Time = (Hl7.Fhir.Model.Element)Time.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ProcessingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcessingComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                if( !DeepComparable.Matches(Time, otherT.Time)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcessingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                if( !DeepComparable.IsExactly(Time, otherT.Time)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Procedure != null) yield return Procedure;
                    foreach (var elem in Additive) { if (elem != null) yield return elem; }
                    if (Time != null) yield return Time;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Procedure != null) yield return new ElementValue("procedure", Procedure);
                    foreach (var elem in Additive) { if (elem != null) yield return new ElementValue("additive", elem); }
                    if (Time != null) yield return new ElementValue("time", Time);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ContainerComponent")]
        [DataContract]
        public partial class ContainerComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ISpecimenContainerComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ContainerComponent"; } }
            
            /// <summary>
            /// Id for the container
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Textual description of the container
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Textual description of the container
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
            /// Kind of container directly associated with specimen
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Container volume or size
            /// </summary>
            [FhirElement("capacity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Capacity
            {
                get { return _Capacity; }
                set { _Capacity = value; OnPropertyChanged("Capacity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Capacity;
            
            /// <summary>
            /// Quantity of specimen within container
            /// </summary>
            [FhirElement("specimenQuantity", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity SpecimenQuantity
            {
                get { return _SpecimenQuantity; }
                set { _SpecimenQuantity = value; OnPropertyChanged("SpecimenQuantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _SpecimenQuantity;
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("additive", Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Additive
            {
                get { return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private Hl7.Fhir.Model.Element _Additive;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContainerComponent");
                base.Serialize(sink);
                sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Identifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("capacity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Capacity?.Serialize(sink);
                sink.Element("specimenQuantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SpecimenQuantity?.Serialize(sink);
                sink.Element("additive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Additive?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainerComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Capacity != null) dest.Capacity = (Hl7.Fhir.Model.SimpleQuantity)Capacity.DeepCopy();
                    if(SpecimenQuantity != null) dest.SpecimenQuantity = (Hl7.Fhir.Model.SimpleQuantity)SpecimenQuantity.DeepCopy();
                    if(Additive != null) dest.Additive = (Hl7.Fhir.Model.Element)Additive.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ContainerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContainerComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Capacity, otherT.Capacity)) return false;
                if( !DeepComparable.Matches(SpecimenQuantity, otherT.SpecimenQuantity)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainerComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Capacity, otherT.Capacity)) return false;
                if( !DeepComparable.IsExactly(SpecimenQuantity, otherT.SpecimenQuantity)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Type != null) yield return Type;
                    if (Capacity != null) yield return Capacity;
                    if (SpecimenQuantity != null) yield return SpecimenQuantity;
                    if (Additive != null) yield return Additive;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Capacity != null) yield return new ElementValue("capacity", Capacity);
                    if (SpecimenQuantity != null) yield return new ElementValue("specimenQuantity", SpecimenQuantity);
                    if (Additive != null) yield return new ElementValue("additive", Additive);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.ISpecimenCollectionComponent Hl7.Fhir.Model.ISpecimen.Collection { get { return Collection; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ISpecimenContainerComponent> Hl7.Fhir.Model.ISpecimen.Container { get { return Container; } }
    
        
        /// <summary>
        /// External Identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Identifier assigned by the lab
        /// </summary>
        [FhirElement("accessionIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier AccessionIdentifier
        {
            get { return _AccessionIdentifier; }
            set { _AccessionIdentifier = value; OnPropertyChanged("AccessionIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _AccessionIdentifier;
        
        /// <summary>
        /// available | unavailable | unsatisfactory | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SpecimenStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SpecimenStatus> _StatusElement;
        
        /// <summary>
        /// available | unavailable | unsatisfactory | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SpecimenStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.SpecimenStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Kind of material that forms the specimen
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Where the specimen came from. This may be from patient(s), from a location (e.g., the source of an environmental sample), or a sampling of a substance or a device
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient","Group","Device","Substance","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        [FhirElement("receivedTime", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ReceivedTimeElement
        {
            get { return _ReceivedTimeElement; }
            set { _ReceivedTimeElement = value; OnPropertyChanged("ReceivedTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ReceivedTimeElement;
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ReceivedTime
        {
            get { return ReceivedTimeElement != null ? ReceivedTimeElement.Value : null; }
            set
            {
                if (value == null)
                    ReceivedTimeElement = null;
                else
                    ReceivedTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ReceivedTime");
            }
        }
        
        /// <summary>
        /// Specimen from which this specimen originated
        /// </summary>
        [FhirElement("parent", Order=150)]
        [CLSCompliant(false)]
        [References("Specimen")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Parent
        {
            get { if(_Parent==null) _Parent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Parent;
        
        /// <summary>
        /// Why the specimen was collected
        /// </summary>
        [FhirElement("request", Order=160)]
        [CLSCompliant(false)]
        [References("ServiceRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Request
        {
            get { if(_Request==null) _Request = new List<Hl7.Fhir.Model.ResourceReference>(); return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Request;
        
        /// <summary>
        /// Collection details
        /// </summary>
        [FhirElement("collection", Order=170)]
        [DataMember]
        public CollectionComponent Collection
        {
            get { return _Collection; }
            set { _Collection = value; OnPropertyChanged("Collection"); }
        }
        
        private CollectionComponent _Collection;
        
        /// <summary>
        /// Processing and processing step details
        /// </summary>
        [FhirElement("processing", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ProcessingComponent> Processing
        {
            get { if(_Processing==null) _Processing = new List<ProcessingComponent>(); return _Processing; }
            set { _Processing = value; OnPropertyChanged("Processing"); }
        }
        
        private List<ProcessingComponent> _Processing;
        
        /// <summary>
        /// Direct container of specimen (tube/slide, etc.)
        /// </summary>
        [FhirElement("container", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContainerComponent> Container
        {
            get { if(_Container==null) _Container = new List<ContainerComponent>(); return _Container; }
            set { _Container = value; OnPropertyChanged("Container"); }
        }
        
        private List<ContainerComponent> _Container;
        
        /// <summary>
        /// State of the specimen
        /// </summary>
        [FhirElement("condition", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Condition
        {
            get { if(_Condition==null) _Condition = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Condition;
        
        /// <summary>
        /// Comments
        /// </summary>
        [FhirElement("note", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Specimen;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(AccessionIdentifier != null) dest.AccessionIdentifier = (Hl7.Fhir.Model.Identifier)AccessionIdentifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.SpecimenStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(ReceivedTimeElement != null) dest.ReceivedTimeElement = (Hl7.Fhir.Model.FhirDateTime)ReceivedTimeElement.DeepCopy();
                if(Parent != null) dest.Parent = new List<Hl7.Fhir.Model.ResourceReference>(Parent.DeepCopy());
                if(Request != null) dest.Request = new List<Hl7.Fhir.Model.ResourceReference>(Request.DeepCopy());
                if(Collection != null) dest.Collection = (CollectionComponent)Collection.DeepCopy();
                if(Processing != null) dest.Processing = new List<ProcessingComponent>(Processing.DeepCopy());
                if(Container != null) dest.Container = new List<ContainerComponent>(Container.DeepCopy());
                if(Condition != null) dest.Condition = new List<Hl7.Fhir.Model.CodeableConcept>(Condition.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Specimen());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Specimen;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(AccessionIdentifier, otherT.AccessionIdentifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(ReceivedTimeElement, otherT.ReceivedTimeElement)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Collection, otherT.Collection)) return false;
            if( !DeepComparable.Matches(Processing, otherT.Processing)) return false;
            if( !DeepComparable.Matches(Container, otherT.Container)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Specimen;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(AccessionIdentifier, otherT.AccessionIdentifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(ReceivedTimeElement, otherT.ReceivedTimeElement)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Collection, otherT.Collection)) return false;
            if( !DeepComparable.IsExactly(Processing, otherT.Processing)) return false;
            if( !DeepComparable.IsExactly(Container, otherT.Container)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Specimen");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("accessionIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AccessionIdentifier?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("receivedTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReceivedTimeElement?.Serialize(sink);
            sink.BeginList("parent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Parent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Request)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("collection", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Collection?.Serialize(sink);
            sink.BeginList("processing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Processing)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("container", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Container)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Condition)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (AccessionIdentifier != null) yield return AccessionIdentifier;
                if (StatusElement != null) yield return StatusElement;
                if (Type != null) yield return Type;
                if (Subject != null) yield return Subject;
                if (ReceivedTimeElement != null) yield return ReceivedTimeElement;
                foreach (var elem in Parent) { if (elem != null) yield return elem; }
                foreach (var elem in Request) { if (elem != null) yield return elem; }
                if (Collection != null) yield return Collection;
                foreach (var elem in Processing) { if (elem != null) yield return elem; }
                foreach (var elem in Container) { if (elem != null) yield return elem; }
                foreach (var elem in Condition) { if (elem != null) yield return elem; }
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
                if (AccessionIdentifier != null) yield return new ElementValue("accessionIdentifier", AccessionIdentifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (ReceivedTimeElement != null) yield return new ElementValue("receivedTime", ReceivedTimeElement);
                foreach (var elem in Parent) { if (elem != null) yield return new ElementValue("parent", elem); }
                foreach (var elem in Request) { if (elem != null) yield return new ElementValue("request", elem); }
                if (Collection != null) yield return new ElementValue("collection", Collection);
                foreach (var elem in Processing) { if (elem != null) yield return new ElementValue("processing", elem); }
                foreach (var elem in Container) { if (elem != null) yield return new ElementValue("container", elem); }
                foreach (var elem in Condition) { if (elem != null) yield return new ElementValue("condition", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }
    
    }

}
