using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Sample for analysis
    /// </summary>
    [FhirType("Specimen", IsResource=true)]
    [DataContract]
    public partial class Specimen : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Type indicating if this is a parent or child relationship
        /// </summary>
        [FhirEnumeration("HierarchicalRelationshipType")]
        public enum HierarchicalRelationshipType
        {
            /// <summary>
            /// The target resource is the parent of the focal specimen resource.
            /// </summary>
            [EnumLiteral("parent")]
            Parent,
            /// <summary>
            /// The target resource is the child of the focal specimen resource.
            /// </summary>
            [EnumLiteral("child")]
            Child,
        }
        
        [FhirType("SpecimenCollectionComponent")]
        [DataContract]
        public partial class SpecimenCollectionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Who collected the specimen
            /// </summary>
            [FhirElement("collector", InSummary=true, Order=40)]
            [References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Collector
            {
                get { return _Collector; }
                set { _Collector = value; OnPropertyChanged("Collector"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Collector;
            
            /// <summary>
            /// Collector comments
            /// </summary>
            [FhirElement("comment", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _CommentElement;
            
            /// <summary>
            /// Collector comments
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Comment
            {
                get { return CommentElement != null ? CommentElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      CommentElement = null; 
                    else
                      CommentElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Comment");
                }
            }
            
            /// <summary>
            /// Collection time
            /// </summary>
            [FhirElement("collected", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Collected
            {
                get { return _Collected; }
                set { _Collected = value; OnPropertyChanged("Collected"); }
            }
            private Hl7.Fhir.Model.Element _Collected;
            
            /// <summary>
            /// The quantity of specimen collected
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Technique used to perform collection
            /// </summary>
            [FhirElement("method", InSummary=true, Order=80)]
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
            [FhirElement("sourceSite", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SourceSite
            {
                get { return _SourceSite; }
                set { _SourceSite = value; OnPropertyChanged("SourceSite"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _SourceSite;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecimenCollectionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Collector != null) dest.Collector = (Hl7.Fhir.Model.ResourceReference)Collector.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = new List<Hl7.Fhir.Model.FhirString>(CommentElement.DeepCopy());
                    if(Collected != null) dest.Collected = (Hl7.Fhir.Model.Element)Collected.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(SourceSite != null) dest.SourceSite = (Hl7.Fhir.Model.CodeableConcept)SourceSite.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecimenCollectionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecimenCollectionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Collector, otherT.Collector)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.Matches(Collected, otherT.Collected)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(SourceSite, otherT.SourceSite)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecimenCollectionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Collector, otherT.Collector)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.IsExactly(Collected, otherT.Collected)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(SourceSite, otherT.SourceSite)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SpecimenSourceComponent")]
        [DataContract]
        public partial class SpecimenSourceComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// parent | child
            /// </summary>
            [FhirElement("relationship", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType> RelationshipElement
            {
                get { return _RelationshipElement; }
                set { _RelationshipElement = value; OnPropertyChanged("RelationshipElement"); }
            }
            private Code<Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType> _RelationshipElement;
            
            /// <summary>
            /// parent | child
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType? Relationship
            {
                get { return RelationshipElement != null ? RelationshipElement.Value : null; }
                set
                {
                    if(value == null)
                      RelationshipElement = null; 
                    else
                      RelationshipElement = new Code<Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType>(value);
                    OnPropertyChanged("Relationship");
                }
            }
            
            /// <summary>
            /// The subject of the relationship
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [References("Specimen")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Target;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecimenSourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RelationshipElement != null) dest.RelationshipElement = (Code<Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType>)RelationshipElement.DeepCopy();
                    if(Target != null) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecimenSourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecimenSourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RelationshipElement, otherT.RelationshipElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecimenSourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RelationshipElement, otherT.RelationshipElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SpecimenTreatmentComponent")]
        [DataContract]
        public partial class SpecimenTreatmentComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Textual description of procedure
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
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
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Indicates the treatment or processing step  applied to the specimen
            /// </summary>
            [FhirElement("procedure", InSummary=true, Order=50)]
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
            [FhirElement("additive", InSummary=true, Order=60)]
            [References("Substance")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Additive
            {
                get { return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Additive;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecimenTreatmentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.CodeableConcept)Procedure.DeepCopy();
                    if(Additive != null) dest.Additive = new List<Hl7.Fhir.Model.ResourceReference>(Additive.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecimenTreatmentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecimenTreatmentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecimenTreatmentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SpecimenContainerComponent")]
        [DataContract]
        public partial class SpecimenContainerComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Id for the container
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Textual description of the container
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
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
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Kind of container directly associated with specimen
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
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
            [FhirElement("capacity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Capacity
            {
                get { return _Capacity; }
                set { _Capacity = value; OnPropertyChanged("Capacity"); }
            }
            private Hl7.Fhir.Model.Quantity _Capacity;
            
            /// <summary>
            /// Quantity of specimen within container
            /// </summary>
            [FhirElement("specimenQuantity", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity SpecimenQuantity
            {
                get { return _SpecimenQuantity; }
                set { _SpecimenQuantity = value; OnPropertyChanged("SpecimenQuantity"); }
            }
            private Hl7.Fhir.Model.Quantity _SpecimenQuantity;
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("additive", InSummary=true, Order=90)]
            [References("Substance")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Additive
            {
                get { return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Additive;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecimenContainerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Capacity != null) dest.Capacity = (Hl7.Fhir.Model.Quantity)Capacity.DeepCopy();
                    if(SpecimenQuantity != null) dest.SpecimenQuantity = (Hl7.Fhir.Model.Quantity)SpecimenQuantity.DeepCopy();
                    if(Additive != null) dest.Additive = (Hl7.Fhir.Model.ResourceReference)Additive.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecimenContainerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecimenContainerComponent;
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
                var otherT = other as SpecimenContainerComponent;
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
            
        }
        
        
        /// <summary>
        /// External Identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Kind of material that forms the specimen
        /// </summary>
        [FhirElement("type", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Parent of specimen
        /// </summary>
        [FhirElement("source", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.SpecimenSourceComponent> Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        private List<Hl7.Fhir.Model.Specimen.SpecimenSourceComponent> _Source;
        
        /// <summary>
        /// Where the specimen came from. This may be the patient(s) or from the environment or  a device
        /// </summary>
        [FhirElement("subject", Order=100)]
        [References("Patient","Group","Device","Substance")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Identifier assigned by the lab
        /// </summary>
        [FhirElement("accessionIdentifier", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier AccessionIdentifier
        {
            get { return _AccessionIdentifier; }
            set { _AccessionIdentifier = value; OnPropertyChanged("AccessionIdentifier"); }
        }
        private Hl7.Fhir.Model.Identifier _AccessionIdentifier;
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        [FhirElement("receivedTime", Order=120)]
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
                if(value == null)
                  ReceivedTimeElement = null; 
                else
                  ReceivedTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ReceivedTime");
            }
        }
        
        /// <summary>
        /// Collection details
        /// </summary>
        [FhirElement("collection", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Specimen.SpecimenCollectionComponent Collection
        {
            get { return _Collection; }
            set { _Collection = value; OnPropertyChanged("Collection"); }
        }
        private Hl7.Fhir.Model.Specimen.SpecimenCollectionComponent _Collection;
        
        /// <summary>
        /// Treatment and processing step details
        /// </summary>
        [FhirElement("treatment", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.SpecimenTreatmentComponent> Treatment
        {
            get { return _Treatment; }
            set { _Treatment = value; OnPropertyChanged("Treatment"); }
        }
        private List<Hl7.Fhir.Model.Specimen.SpecimenTreatmentComponent> _Treatment;
        
        /// <summary>
        /// Direct container of specimen (tube/slide, etc)
        /// </summary>
        [FhirElement("container", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.SpecimenContainerComponent> Container
        {
            get { return _Container; }
            set { _Container = value; OnPropertyChanged("Container"); }
        }
        private List<Hl7.Fhir.Model.Specimen.SpecimenContainerComponent> _Container;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Specimen;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Source != null) dest.Source = new List<Hl7.Fhir.Model.Specimen.SpecimenSourceComponent>(Source.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(AccessionIdentifier != null) dest.AccessionIdentifier = (Hl7.Fhir.Model.Identifier)AccessionIdentifier.DeepCopy();
                if(ReceivedTimeElement != null) dest.ReceivedTimeElement = (Hl7.Fhir.Model.FhirDateTime)ReceivedTimeElement.DeepCopy();
                if(Collection != null) dest.Collection = (Hl7.Fhir.Model.Specimen.SpecimenCollectionComponent)Collection.DeepCopy();
                if(Treatment != null) dest.Treatment = new List<Hl7.Fhir.Model.Specimen.SpecimenTreatmentComponent>(Treatment.DeepCopy());
                if(Container != null) dest.Container = new List<Hl7.Fhir.Model.Specimen.SpecimenContainerComponent>(Container.DeepCopy());
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
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(AccessionIdentifier, otherT.AccessionIdentifier)) return false;
            if( !DeepComparable.Matches(ReceivedTimeElement, otherT.ReceivedTimeElement)) return false;
            if( !DeepComparable.Matches(Collection, otherT.Collection)) return false;
            if( !DeepComparable.Matches(Treatment, otherT.Treatment)) return false;
            if( !DeepComparable.Matches(Container, otherT.Container)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Specimen;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(AccessionIdentifier, otherT.AccessionIdentifier)) return false;
            if( !DeepComparable.IsExactly(ReceivedTimeElement, otherT.ReceivedTimeElement)) return false;
            if( !DeepComparable.IsExactly(Collection, otherT.Collection)) return false;
            if( !DeepComparable.IsExactly(Treatment, otherT.Treatment)) return false;
            if( !DeepComparable.IsExactly(Container, otherT.Container)) return false;
            
            return true;
        }
        
    }
    
}
