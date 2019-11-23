﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Todo
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "SubstanceReferenceInformation", IsResource=true)]
    [DataContract]
    public partial class SubstanceReferenceInformation : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SubstanceReferenceInformation; } }
        [NotMapped]
        public override string TypeName { get { return "SubstanceReferenceInformation"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GeneComponent")]
        [DataContract]
        public partial class GeneComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GeneComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("geneSequenceOrigin", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept GeneSequenceOrigin
            {
                get { return _GeneSequenceOrigin; }
                set { _GeneSequenceOrigin = value; OnPropertyChanged("GeneSequenceOrigin"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _GeneSequenceOrigin;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("gene", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Gene
            {
                get { return _Gene; }
                set { _Gene = value; OnPropertyChanged("Gene"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Gene;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("source", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=60)]
            [CLSCompliant(false)]
            [References("DocumentReference")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Source
            {
                get { if(_Source==null) _Source = new List<Hl7.Fhir.Model.ResourceReference>(); return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Source;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GeneComponent");
                base.Serialize(sink);
                sink.Element("geneSequenceOrigin", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GeneSequenceOrigin?.Serialize(sink);
                sink.Element("gene", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Gene?.Serialize(sink);
                sink.BeginList("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Source)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GeneComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(GeneSequenceOrigin != null) dest.GeneSequenceOrigin = (Hl7.Fhir.Model.CodeableConcept)GeneSequenceOrigin.DeepCopy();
                    if(Gene != null) dest.Gene = (Hl7.Fhir.Model.CodeableConcept)Gene.DeepCopy();
                    if(Source != null) dest.Source = new List<Hl7.Fhir.Model.ResourceReference>(Source.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GeneComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GeneComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(GeneSequenceOrigin, otherT.GeneSequenceOrigin)) return false;
                if( !DeepComparable.Matches(Gene, otherT.Gene)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GeneComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(GeneSequenceOrigin, otherT.GeneSequenceOrigin)) return false;
                if( !DeepComparable.IsExactly(Gene, otherT.Gene)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (GeneSequenceOrigin != null) yield return GeneSequenceOrigin;
                    if (Gene != null) yield return Gene;
                    foreach (var elem in Source) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (GeneSequenceOrigin != null) yield return new ElementValue("geneSequenceOrigin", GeneSequenceOrigin);
                    if (Gene != null) yield return new ElementValue("gene", Gene);
                    foreach (var elem in Source) { if (elem != null) yield return new ElementValue("source", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GeneElementComponent")]
        [DataContract]
        public partial class GeneElementComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GeneElementComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("type", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("element", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Element
            {
                get { return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Element;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("source", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=60)]
            [CLSCompliant(false)]
            [References("DocumentReference")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Source
            {
                get { if(_Source==null) _Source = new List<Hl7.Fhir.Model.ResourceReference>(); return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Source;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GeneElementComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
                sink.Element("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Element?.Serialize(sink);
                sink.BeginList("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Source)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GeneElementComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Element != null) dest.Element = (Hl7.Fhir.Model.Identifier)Element.DeepCopy();
                    if(Source != null) dest.Source = new List<Hl7.Fhir.Model.ResourceReference>(Source.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GeneElementComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GeneElementComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GeneElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Element != null) yield return Element;
                    foreach (var elem in Source) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Element != null) yield return new ElementValue("element", Element);
                    foreach (var elem in Source) { if (elem != null) yield return new ElementValue("source", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ClassificationComponent")]
        [DataContract]
        public partial class ClassificationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ClassificationComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("domain", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Domain
            {
                get { return _Domain; }
                set { _Domain = value; OnPropertyChanged("Domain"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Domain;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("classification", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Classification
            {
                get { return _Classification; }
                set { _Classification = value; OnPropertyChanged("Classification"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Classification;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("subtype", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Subtype
            {
                get { if(_Subtype==null) _Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Subtype; }
                set { _Subtype = value; OnPropertyChanged("Subtype"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Subtype;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("source", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=70)]
            [CLSCompliant(false)]
            [References("DocumentReference")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Source
            {
                get { if(_Source==null) _Source = new List<Hl7.Fhir.Model.ResourceReference>(); return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Source;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ClassificationComponent");
                base.Serialize(sink);
                sink.Element("domain", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Domain?.Serialize(sink);
                sink.Element("classification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Classification?.Serialize(sink);
                sink.BeginList("subtype", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Subtype)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Source)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ClassificationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Domain != null) dest.Domain = (Hl7.Fhir.Model.CodeableConcept)Domain.DeepCopy();
                    if(Classification != null) dest.Classification = (Hl7.Fhir.Model.CodeableConcept)Classification.DeepCopy();
                    if(Subtype != null) dest.Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(Subtype.DeepCopy());
                    if(Source != null) dest.Source = new List<Hl7.Fhir.Model.ResourceReference>(Source.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ClassificationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ClassificationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Domain, otherT.Domain)) return false;
                if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
                if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ClassificationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Domain, otherT.Domain)) return false;
                if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
                if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Domain != null) yield return Domain;
                    if (Classification != null) yield return Classification;
                    foreach (var elem in Subtype) { if (elem != null) yield return elem; }
                    foreach (var elem in Source) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Domain != null) yield return new ElementValue("domain", Domain);
                    if (Classification != null) yield return new ElementValue("classification", Classification);
                    foreach (var elem in Subtype) { if (elem != null) yield return new ElementValue("subtype", elem); }
                    foreach (var elem in Source) { if (elem != null) yield return new ElementValue("source", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TargetComponent")]
        [DataContract]
        public partial class TargetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TargetComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("target", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Target;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("type", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("interaction", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Interaction
            {
                get { return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Interaction;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("organism", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Organism
            {
                get { return _Organism; }
                set { _Organism = value; OnPropertyChanged("Organism"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Organism;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("organismType", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept OrganismType
            {
                get { return _OrganismType; }
                set { _OrganismType = value; OnPropertyChanged("OrganismType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _OrganismType;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("amount", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.Element _Amount;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("amountType", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AmountType
            {
                get { return _AmountType; }
                set { _AmountType = value; OnPropertyChanged("AmountType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AmountType;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("source", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=110)]
            [CLSCompliant(false)]
            [References("DocumentReference")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Source
            {
                get { if(_Source==null) _Source = new List<Hl7.Fhir.Model.ResourceReference>(); return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Source;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TargetComponent");
                base.Serialize(sink);
                sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Target?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
                sink.Element("interaction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Interaction?.Serialize(sink);
                sink.Element("organism", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Organism?.Serialize(sink);
                sink.Element("organismType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrganismType?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Amount?.Serialize(sink);
                sink.Element("amountType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AmountType?.Serialize(sink);
                sink.BeginList("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Source)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TargetComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.Identifier)Target.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Interaction != null) dest.Interaction = (Hl7.Fhir.Model.CodeableConcept)Interaction.DeepCopy();
                    if(Organism != null) dest.Organism = (Hl7.Fhir.Model.CodeableConcept)Organism.DeepCopy();
                    if(OrganismType != null) dest.OrganismType = (Hl7.Fhir.Model.CodeableConcept)OrganismType.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.Element)Amount.DeepCopy();
                    if(AmountType != null) dest.AmountType = (Hl7.Fhir.Model.CodeableConcept)AmountType.DeepCopy();
                    if(Source != null) dest.Source = new List<Hl7.Fhir.Model.ResourceReference>(Source.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TargetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.Matches(Organism, otherT.Organism)) return false;
                if( !DeepComparable.Matches(OrganismType, otherT.OrganismType)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(AmountType, otherT.AmountType)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.IsExactly(Organism, otherT.Organism)) return false;
                if( !DeepComparable.IsExactly(OrganismType, otherT.OrganismType)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(AmountType, otherT.AmountType)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Target != null) yield return Target;
                    if (Type != null) yield return Type;
                    if (Interaction != null) yield return Interaction;
                    if (Organism != null) yield return Organism;
                    if (OrganismType != null) yield return OrganismType;
                    if (Amount != null) yield return Amount;
                    if (AmountType != null) yield return AmountType;
                    foreach (var elem in Source) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Target != null) yield return new ElementValue("target", Target);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Interaction != null) yield return new ElementValue("interaction", Interaction);
                    if (Organism != null) yield return new ElementValue("organism", Organism);
                    if (OrganismType != null) yield return new ElementValue("organismType", OrganismType);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (AmountType != null) yield return new ElementValue("amountType", AmountType);
                    foreach (var elem in Source) { if (elem != null) yield return new ElementValue("source", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("comment", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Todo
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                    CommentElement = null;
                else
                    CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("gene", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GeneComponent> Gene
        {
            get { if(_Gene==null) _Gene = new List<GeneComponent>(); return _Gene; }
            set { _Gene = value; OnPropertyChanged("Gene"); }
        }
        
        private List<GeneComponent> _Gene;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("geneElement", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GeneElementComponent> GeneElement
        {
            get { if(_GeneElement==null) _GeneElement = new List<GeneElementComponent>(); return _GeneElement; }
            set { _GeneElement = value; OnPropertyChanged("GeneElement"); }
        }
        
        private List<GeneElementComponent> _GeneElement;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("classification", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ClassificationComponent> Classification
        {
            get { if(_Classification==null) _Classification = new List<ClassificationComponent>(); return _Classification; }
            set { _Classification = value; OnPropertyChanged("Classification"); }
        }
        
        private List<ClassificationComponent> _Classification;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("target", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TargetComponent> Target
        {
            get { if(_Target==null) _Target = new List<TargetComponent>(); return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private List<TargetComponent> _Target;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstanceReferenceInformation;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(Gene != null) dest.Gene = new List<GeneComponent>(Gene.DeepCopy());
                if(GeneElement != null) dest.GeneElement = new List<GeneElementComponent>(GeneElement.DeepCopy());
                if(Classification != null) dest.Classification = new List<ClassificationComponent>(Classification.DeepCopy());
                if(Target != null) dest.Target = new List<TargetComponent>(Target.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new SubstanceReferenceInformation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstanceReferenceInformation;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(Gene, otherT.Gene)) return false;
            if( !DeepComparable.Matches(GeneElement, otherT.GeneElement)) return false;
            if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstanceReferenceInformation;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(Gene, otherT.Gene)) return false;
            if( !DeepComparable.IsExactly(GeneElement, otherT.GeneElement)) return false;
            if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SubstanceReferenceInformation");
            base.Serialize(sink);
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CommentElement?.Serialize(sink);
            sink.BeginList("gene", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Gene)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("geneElement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in GeneElement)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("classification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Classification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Target)
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
                if (CommentElement != null) yield return CommentElement;
                foreach (var elem in Gene) { if (elem != null) yield return elem; }
                foreach (var elem in GeneElement) { if (elem != null) yield return elem; }
                foreach (var elem in Classification) { if (elem != null) yield return elem; }
                foreach (var elem in Target) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                foreach (var elem in Gene) { if (elem != null) yield return new ElementValue("gene", elem); }
                foreach (var elem in GeneElement) { if (elem != null) yield return new ElementValue("geneElement", elem); }
                foreach (var elem in Classification) { if (elem != null) yield return new ElementValue("classification", elem); }
                foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
            }
        }
    
    }

}
