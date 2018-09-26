using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Definition of an element in a resource or extension
    /// </summary>
    [FhirType("ElementDefinition")]
    [DataContract]
    public partial class ElementDefinition : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ElementDefinition"; } }
        
        /// <summary>
        /// How a property is represented when serialized.
        /// (url: http://hl7.org/fhir/ValueSet/property-representation)
        /// </summary>
        [FhirEnumeration("PropertyRepresentation")]
        public enum PropertyRepresentation
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/property-representation)
            /// </summary>
            [EnumLiteral("xmlAttr", "http://hl7.org/fhir/property-representation"), Description("XML Attribute")]
            XmlAttr,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/property-representation)
            /// </summary>
            [EnumLiteral("xmlText", "http://hl7.org/fhir/property-representation"), Description("XML Text")]
            XmlText,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/property-representation)
            /// </summary>
            [EnumLiteral("typeAttr", "http://hl7.org/fhir/property-representation"), Description("Type Attribute")]
            TypeAttr,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/property-representation)
            /// </summary>
            [EnumLiteral("cdaText", "http://hl7.org/fhir/property-representation"), Description("CDA Text Format")]
            CdaText,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/property-representation)
            /// </summary>
            [EnumLiteral("xhtml", "http://hl7.org/fhir/property-representation"), Description("XHTML")]
            Xhtml,
        }

        /// <summary>
        /// How an element value is interpreted when discrimination is evaluated
        /// (url: http://hl7.org/fhir/ValueSet/discriminator-type)
        /// </summary>
        [FhirEnumeration("DiscriminatorType")]
        public enum DiscriminatorType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/discriminator-type)
            /// </summary>
            [EnumLiteral("value", "http://hl7.org/fhir/discriminator-type"), Description("Value")]
            Value,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/discriminator-type)
            /// </summary>
            [EnumLiteral("exists", "http://hl7.org/fhir/discriminator-type"), Description("Exists")]
            Exists,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/discriminator-type)
            /// </summary>
            [EnumLiteral("pattern", "http://hl7.org/fhir/discriminator-type"), Description("Pattern")]
            Pattern,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/discriminator-type)
            /// </summary>
            [EnumLiteral("type", "http://hl7.org/fhir/discriminator-type"), Description("Type")]
            Type,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/discriminator-type)
            /// </summary>
            [EnumLiteral("profile", "http://hl7.org/fhir/discriminator-type"), Description("Profile")]
            Profile,
        }

        /// <summary>
        /// How slices are interpreted when evaluating an instance.
        /// (url: http://hl7.org/fhir/ValueSet/resource-slicing-rules)
        /// </summary>
        [FhirEnumeration("SlicingRules")]
        public enum SlicingRules
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/resource-slicing-rules)
            /// </summary>
            [EnumLiteral("closed", "http://hl7.org/fhir/resource-slicing-rules"), Description("Closed")]
            Closed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/resource-slicing-rules)
            /// </summary>
            [EnumLiteral("open", "http://hl7.org/fhir/resource-slicing-rules"), Description("Open")]
            Open,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/resource-slicing-rules)
            /// </summary>
            [EnumLiteral("openAtEnd", "http://hl7.org/fhir/resource-slicing-rules"), Description("Open at End")]
            OpenAtEnd,
        }

        /// <summary>
        /// How resource references can be aggregated.
        /// (url: http://hl7.org/fhir/ValueSet/resource-aggregation-mode)
        /// </summary>
        [FhirEnumeration("AggregationMode")]
        public enum AggregationMode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/resource-aggregation-mode)
            /// </summary>
            [EnumLiteral("contained", "http://hl7.org/fhir/resource-aggregation-mode"), Description("Contained")]
            Contained,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/resource-aggregation-mode)
            /// </summary>
            [EnumLiteral("referenced", "http://hl7.org/fhir/resource-aggregation-mode"), Description("Referenced")]
            Referenced,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/resource-aggregation-mode)
            /// </summary>
            [EnumLiteral("bundled", "http://hl7.org/fhir/resource-aggregation-mode"), Description("Bundled")]
            Bundled,
        }

        /// <summary>
        /// Whether a reference needs to be version specific or version independent, or whether either can be used
        /// (url: http://hl7.org/fhir/ValueSet/reference-version-rules)
        /// </summary>
        [FhirEnumeration("ReferenceVersionRules")]
        public enum ReferenceVersionRules
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-version-rules)
            /// </summary>
            [EnumLiteral("either", "http://hl7.org/fhir/reference-version-rules"), Description("Either Specific or independent")]
            Either,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-version-rules)
            /// </summary>
            [EnumLiteral("independent", "http://hl7.org/fhir/reference-version-rules"), Description("Version independent")]
            Independent,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reference-version-rules)
            /// </summary>
            [EnumLiteral("specific", "http://hl7.org/fhir/reference-version-rules"), Description("Version Specific")]
            Specific,
        }

        /// <summary>
        /// SHALL applications comply with this constraint?
        /// (url: http://hl7.org/fhir/ValueSet/constraint-severity)
        /// </summary>
        [FhirEnumeration("ConstraintSeverity")]
        public enum ConstraintSeverity
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/constraint-severity)
            /// </summary>
            [EnumLiteral("error", "http://hl7.org/fhir/constraint-severity"), Description("Error")]
            Error,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/constraint-severity)
            /// </summary>
            [EnumLiteral("warning", "http://hl7.org/fhir/constraint-severity"), Description("Warning")]
            Warning,
        }

        [FhirType("SlicingComponent")]
        [DataContract]
        public partial class SlicingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SlicingComponent"; } }
            
            /// <summary>
            /// Element values that are used to distinguish the slices
            /// </summary>
            [FhirElement("discriminator", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ElementDefinition.DiscriminatorComponent> Discriminator
            {
                get { if(_Discriminator==null) _Discriminator = new List<Hl7.Fhir.Model.ElementDefinition.DiscriminatorComponent>(); return _Discriminator; }
                set { _Discriminator = value; OnPropertyChanged("Discriminator"); }
            }
            
            private List<Hl7.Fhir.Model.ElementDefinition.DiscriminatorComponent> _Discriminator;
            
            /// <summary>
            /// Text description of how slicing works (or not)
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
            /// Text description of how slicing works (or not)
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
            /// If elements must be in same order as slices
            /// </summary>
            [FhirElement("ordered", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean OrderedElement
            {
                get { return _OrderedElement; }
                set { _OrderedElement = value; OnPropertyChanged("OrderedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _OrderedElement;
            
            /// <summary>
            /// If elements must be in same order as slices
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Ordered
            {
                get { return OrderedElement != null ? OrderedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      OrderedElement = null; 
                    else
                      OrderedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Ordered");
                }
            }
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            [FhirElement("rules", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ElementDefinition.SlicingRules> RulesElement
            {
                get { return _RulesElement; }
                set { _RulesElement = value; OnPropertyChanged("RulesElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ElementDefinition.SlicingRules> _RulesElement;
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ElementDefinition.SlicingRules? Rules
            {
                get { return RulesElement != null ? RulesElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      RulesElement = null; 
                    else
                      RulesElement = new Code<Hl7.Fhir.Model.ElementDefinition.SlicingRules>(value);
                    OnPropertyChanged("Rules");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SlicingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Discriminator != null) dest.Discriminator = new List<Hl7.Fhir.Model.ElementDefinition.DiscriminatorComponent>(Discriminator.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OrderedElement != null) dest.OrderedElement = (Hl7.Fhir.Model.FhirBoolean)OrderedElement.DeepCopy();
                    if(RulesElement != null) dest.RulesElement = (Code<Hl7.Fhir.Model.ElementDefinition.SlicingRules>)RulesElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SlicingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SlicingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Discriminator, otherT.Discriminator)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OrderedElement, otherT.OrderedElement)) return false;
                if( !DeepComparable.Matches(RulesElement, otherT.RulesElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SlicingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Discriminator, otherT.Discriminator)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OrderedElement, otherT.OrderedElement)) return false;
                if( !DeepComparable.IsExactly(RulesElement, otherT.RulesElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Discriminator) { if (elem != null) yield return elem; }
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (OrderedElement != null) yield return OrderedElement;
                    if (RulesElement != null) yield return RulesElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    foreach (var elem in Discriminator) { if (elem != null) yield return new ElementValue("discriminator", elem); }
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (OrderedElement != null) yield return new ElementValue("ordered", OrderedElement);
                    if (RulesElement != null) yield return new ElementValue("rules", RulesElement);
 
                } 
            } 
            
        }                
        [FhirType("DiscriminatorComponent")]
        [DataContract]
        public partial class DiscriminatorComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DiscriminatorComponent"; } }
            
            /// <summary>
            /// value | exists | pattern | type | profile
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ElementDefinition.DiscriminatorType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ElementDefinition.DiscriminatorType> _TypeElement;
            
            /// <summary>
            /// value | exists | pattern | type | profile
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ElementDefinition.DiscriminatorType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.ElementDefinition.DiscriminatorType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Path to element value
            /// </summary>
            [FhirElement("path", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path to element value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiscriminatorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ElementDefinition.DiscriminatorType>)TypeElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiscriminatorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiscriminatorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiscriminatorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (PathElement != null) yield return PathElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
 
                } 
            } 
            
        }                
        [FhirType("BaseComponent")]
        [DataContract]
        public partial class BaseComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "BaseComponent"; } }
            
            /// <summary>
            /// Path that identifies the base element
            /// </summary>
            [FhirElement("path", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path that identifies the base element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// Min cardinality of the base element
            /// </summary>
            [FhirElement("min", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt MinElement
            {
                get { return _MinElement; }
                set { _MinElement = value; OnPropertyChanged("MinElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _MinElement;
            
            /// <summary>
            /// Min cardinality of the base element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Min
            {
                get { return MinElement != null ? MinElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      MinElement = null; 
                    else
                      MinElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Min");
                }
            }
            
            /// <summary>
            /// Max cardinality of the base element
            /// </summary>
            [FhirElement("max", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaxElement
            {
                get { return _MaxElement; }
                set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MaxElement;
            
            /// <summary>
            /// Max cardinality of the base element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Max
            {
                get { return MaxElement != null ? MaxElement.Value : null; }
                set
                {
                    if (value == null)
                      MaxElement = null; 
                    else
                      MaxElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Max");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BaseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.UnsignedInt)MinElement.DeepCopy();
                    if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BaseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BaseComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BaseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (MinElement != null) yield return MinElement;
                    if (MaxElement != null) yield return MaxElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (MinElement != null) yield return new ElementValue("min", MinElement);
                    if (MaxElement != null) yield return new ElementValue("max", MaxElement);
 
                } 
            } 
            
        }                
        [FhirType("TypeRefComponent")]
        [DataContract]
        public partial class TypeRefComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TypeRefComponent"; } }
            
            /// <summary>
            /// Data type or Resource (reference to definition)
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _CodeElement;
            
            /// <summary>
            /// Data type or Resource (reference to definition)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Profile (StructureDefinition) to apply (or IG)
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _ProfileElement;
            
            /// <summary>
            /// Profile (StructureDefinition) to apply (or IG)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Profile
            {
                get { return ProfileElement != null ? ProfileElement.Value : null; }
                set
                {
                    if (value == null)
                      ProfileElement = null; 
                    else
                      ProfileElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Profile");
                }
            }
            
            /// <summary>
            /// Profile (StructureDefinition) to apply to reference target (or IG)
            /// </summary>
            [FhirElement("targetProfile", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri TargetProfileElement
            {
                get { return _TargetProfileElement; }
                set { _TargetProfileElement = value; OnPropertyChanged("TargetProfileElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _TargetProfileElement;
            
            /// <summary>
            /// Profile (StructureDefinition) to apply to reference target (or IG)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TargetProfile
            {
                get { return TargetProfileElement != null ? TargetProfileElement.Value : null; }
                set
                {
                    if (value == null)
                      TargetProfileElement = null; 
                    else
                      TargetProfileElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("TargetProfile");
                }
            }
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            [FhirElement("aggregation", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.ElementDefinition.AggregationMode>> AggregationElement
            {
                get { if(_AggregationElement==null) _AggregationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ElementDefinition.AggregationMode>>(); return _AggregationElement; }
                set { _AggregationElement = value; OnPropertyChanged("AggregationElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.ElementDefinition.AggregationMode>> _AggregationElement;
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.ElementDefinition.AggregationMode?> Aggregation
            {
                get { return AggregationElement != null ? AggregationElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                      AggregationElement = null; 
                    else
                      AggregationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ElementDefinition.AggregationMode>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ElementDefinition.AggregationMode>(elem)));
                    OnPropertyChanged("Aggregation");
                }
            }
            
            /// <summary>
            /// either | independent | specific
            /// </summary>
            [FhirElement("versioning", InSummary=true, Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ElementDefinition.ReferenceVersionRules> VersioningElement
            {
                get { return _VersioningElement; }
                set { _VersioningElement = value; OnPropertyChanged("VersioningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ElementDefinition.ReferenceVersionRules> _VersioningElement;
            
            /// <summary>
            /// either | independent | specific
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ElementDefinition.ReferenceVersionRules? Versioning
            {
                get { return VersioningElement != null ? VersioningElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      VersioningElement = null; 
                    else
                      VersioningElement = new Code<Hl7.Fhir.Model.ElementDefinition.ReferenceVersionRules>(value);
                    OnPropertyChanged("Versioning");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TypeRefComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.FhirUri)CodeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.FhirUri)ProfileElement.DeepCopy();
                    if(TargetProfileElement != null) dest.TargetProfileElement = (Hl7.Fhir.Model.FhirUri)TargetProfileElement.DeepCopy();
                    if(AggregationElement != null) dest.AggregationElement = new List<Code<Hl7.Fhir.Model.ElementDefinition.AggregationMode>>(AggregationElement.DeepCopy());
                    if(VersioningElement != null) dest.VersioningElement = (Code<Hl7.Fhir.Model.ElementDefinition.ReferenceVersionRules>)VersioningElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TypeRefComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TypeRefComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.Matches(TargetProfileElement, otherT.TargetProfileElement)) return false;
                if( !DeepComparable.Matches(AggregationElement, otherT.AggregationElement)) return false;
                if( !DeepComparable.Matches(VersioningElement, otherT.VersioningElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TypeRefComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.IsExactly(TargetProfileElement, otherT.TargetProfileElement)) return false;
                if( !DeepComparable.IsExactly(AggregationElement, otherT.AggregationElement)) return false;
                if( !DeepComparable.IsExactly(VersioningElement, otherT.VersioningElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (ProfileElement != null) yield return ProfileElement;
                    if (TargetProfileElement != null) yield return TargetProfileElement;
                    foreach (var elem in AggregationElement) { if (elem != null) yield return elem; }
                    if (VersioningElement != null) yield return VersioningElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                    if (TargetProfileElement != null) yield return new ElementValue("targetProfile", TargetProfileElement);
                    foreach (var elem in AggregationElement) { if (elem != null) yield return new ElementValue("aggregation", elem); }
                    if (VersioningElement != null) yield return new ElementValue("versioning", VersioningElement);
 
                } 
            } 
            
        }                
        [FhirType("ExampleComponent")]
        [DataContract]
        public partial class ExampleComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ExampleComponent"; } }
            
            /// <summary>
            /// Describes the purpose of this example
            /// </summary>
            [FhirElement("label", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Describes the purpose of this example
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Label
            {
                get { return LabelElement != null ? LabelElement.Value : null; }
                set
                {
                    if (value == null)
                      LabelElement = null; 
                    else
                      LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// Value of Example (one of allowed types)
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
			[CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.ContactPoint),typeof(Count),typeof(Distance),typeof(Duration),typeof(Hl7.Fhir.Model.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Money),typeof(Hl7.Fhir.Model.Period),typeof(Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Signature),typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.Meta))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExampleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ExampleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExampleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExampleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LabelElement != null) yield return LabelElement;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (Value != null) yield return new ElementValue("value", Value);
 
                } 
            } 
            
        }                
        [FhirType("ConstraintComponent")]
        [DataContract]
        public partial class ConstraintComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ConstraintComponent"; } }
            
            /// <summary>
            /// Target of 'condition' reference above
            /// </summary>
            [FhirElement("key", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id KeyElement
            {
                get { return _KeyElement; }
                set { _KeyElement = value; OnPropertyChanged("KeyElement"); }
            }
            
            private Hl7.Fhir.Model.Id _KeyElement;
            
            /// <summary>
            /// Target of 'condition' reference above
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Key
            {
                get { return KeyElement != null ? KeyElement.Value : null; }
                set
                {
                    if (value == null)
                      KeyElement = null; 
                    else
                      KeyElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Key");
                }
            }
            
            /// <summary>
            /// Why this constraint is necessary or appropriate
            /// </summary>
            [FhirElement("requirements", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementsElement
            {
                get { return _RequirementsElement; }
                set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequirementsElement;
            
            /// <summary>
            /// Why this constraint is necessary or appropriate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Requirements
            {
                get { return RequirementsElement != null ? RequirementsElement.Value : null; }
                set
                {
                    if (value == null)
                      RequirementsElement = null; 
                    else
                      RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Requirements");
                }
            }
            
            /// <summary>
            /// error | warning
            /// </summary>
            [FhirElement("severity", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ElementDefinition.ConstraintSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ElementDefinition.ConstraintSeverity> _SeverityElement;
            
            /// <summary>
            /// error | warning
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ElementDefinition.ConstraintSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      SeverityElement = null; 
                    else
                      SeverityElement = new Code<Hl7.Fhir.Model.ElementDefinition.ConstraintSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Human description of constraint
            /// </summary>
            [FhirElement("human", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HumanElement
            {
                get { return _HumanElement; }
                set { _HumanElement = value; OnPropertyChanged("HumanElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HumanElement;
            
            /// <summary>
            /// Human description of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Human
            {
                get { return HumanElement != null ? HumanElement.Value : null; }
                set
                {
                    if (value == null)
                      HumanElement = null; 
                    else
                      HumanElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Human");
                }
            }
            
            /// <summary>
            /// FHIRPath expression of constraint
            /// </summary>
            [FhirElement("expression", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// FHIRPath expression of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                      ExpressionElement = null; 
                    else
                      ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
            
            /// <summary>
            /// XPath expression of constraint
            /// </summary>
            [FhirElement("xpath", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString XpathElement
            {
                get { return _XpathElement; }
                set { _XpathElement = value; OnPropertyChanged("XpathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _XpathElement;
            
            /// <summary>
            /// XPath expression of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Xpath
            {
                get { return XpathElement != null ? XpathElement.Value : null; }
                set
                {
                    if (value == null)
                      XpathElement = null; 
                    else
                      XpathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Xpath");
                }
            }
            
            /// <summary>
            /// Reference to original source of constraint
            /// </summary>
            [FhirElement("source", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SourceElement;
            
            /// <summary>
            /// Reference to original source of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if (value == null)
                      SourceElement = null; 
                    else
                      SourceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Source");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConstraintComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(KeyElement != null) dest.KeyElement = (Hl7.Fhir.Model.Id)KeyElement.DeepCopy();
                    if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.ElementDefinition.ConstraintSeverity>)SeverityElement.DeepCopy();
                    if(HumanElement != null) dest.HumanElement = (Hl7.Fhir.Model.FhirString)HumanElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    if(XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.FhirUri)SourceElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConstraintComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConstraintComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(KeyElement, otherT.KeyElement)) return false;
                if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(HumanElement, otherT.HumanElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConstraintComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(KeyElement, otherT.KeyElement)) return false;
                if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(HumanElement, otherT.HumanElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (KeyElement != null) yield return KeyElement;
                    if (RequirementsElement != null) yield return RequirementsElement;
                    if (SeverityElement != null) yield return SeverityElement;
                    if (HumanElement != null) yield return HumanElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                    if (XpathElement != null) yield return XpathElement;
                    if (SourceElement != null) yield return SourceElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (KeyElement != null) yield return new ElementValue("key", KeyElement);
                    if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                    if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
                    if (HumanElement != null) yield return new ElementValue("human", HumanElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                    if (XpathElement != null) yield return new ElementValue("xpath", XpathElement);
                    if (SourceElement != null) yield return new ElementValue("source", SourceElement);
 
                } 
            } 
            
        }                
        [FhirType("ElementDefinitionBindingComponent")]
        [DataContract]
        public partial class ElementDefinitionBindingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ElementDefinitionBindingComponent"; } }
            
            /// <summary>
            /// required | extensible | preferred | example
            /// </summary>
            [FhirElement("strength", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.BindingStrength> StrengthElement
            {
                get { return _StrengthElement; }
                set { _StrengthElement = value; OnPropertyChanged("StrengthElement"); }
            }
            
            private Code<Hl7.Fhir.Model.BindingStrength> _StrengthElement;
            
            /// <summary>
            /// required | extensible | preferred | example
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.BindingStrength? Strength
            {
                get { return StrengthElement != null ? StrengthElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      StrengthElement = null; 
                    else
                      StrengthElement = new Code<Hl7.Fhir.Model.BindingStrength>(value);
                    OnPropertyChanged("Strength");
                }
            }
            
            /// <summary>
            /// Human explanation of the value set
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
            /// Human explanation of the value set
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
            /// Source of value set
            /// </summary>
            [FhirElement("valueSet", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
			[CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element ValueSet
            {
                get { return _ValueSet; }
                set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
            }
            
            private Hl7.Fhir.Model.Element _ValueSet;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementDefinitionBindingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StrengthElement != null) dest.StrengthElement = (Code<Hl7.Fhir.Model.BindingStrength>)StrengthElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ValueSet != null) dest.ValueSet = (Hl7.Fhir.Model.Element)ValueSet.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementDefinitionBindingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionBindingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StrengthElement, otherT.StrengthElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionBindingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StrengthElement, otherT.StrengthElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (StrengthElement != null) yield return StrengthElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (ValueSet != null) yield return ValueSet;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (StrengthElement != null) yield return new ElementValue("strength", StrengthElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (ValueSet != null) yield return new ElementValue("valueSet", ValueSet);
 
                } 
            } 
            
        }                
        [FhirType("MappingComponent")]
        [DataContract]
        public partial class MappingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "MappingComponent"; } }
            
            /// <summary>
            /// Reference to mapping declaration
            /// </summary>
            [FhirElement("identity", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentityElement
            {
                get { return _IdentityElement; }
                set { _IdentityElement = value; OnPropertyChanged("IdentityElement"); }
            }
            
            private Hl7.Fhir.Model.Id _IdentityElement;
            
            /// <summary>
            /// Reference to mapping declaration
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identity
            {
                get { return IdentityElement != null ? IdentityElement.Value : null; }
                set
                {
                    if (value == null)
                      IdentityElement = null; 
                    else
                      IdentityElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identity");
                }
            }
            
            /// <summary>
            /// Computable language of mapping
            /// </summary>
            [FhirElement("language", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.Code _LanguageElement;
            
            /// <summary>
            /// Computable language of mapping
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Language
            {
                get { return LanguageElement != null ? LanguageElement.Value : null; }
                set
                {
                    if (value == null)
                      LanguageElement = null; 
                    else
                      LanguageElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            [FhirElement("map", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MapElement
            {
                get { return _MapElement; }
                set { _MapElement = value; OnPropertyChanged("MapElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MapElement;
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Map
            {
                get { return MapElement != null ? MapElement.Value : null; }
                set
                {
                    if (value == null)
                      MapElement = null; 
                    else
                      MapElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Map");
                }
            }
            
            /// <summary>
            /// Comments about the mapping or its use
            /// </summary>
            [FhirElement("comment", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Comments about the mapping or its use
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MappingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                    if(MapElement != null) dest.MapElement = (Hl7.Fhir.Model.FhirString)MapElement.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(MapElement, otherT.MapElement)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(MapElement, otherT.MapElement)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentityElement != null) yield return IdentityElement;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (MapElement != null) yield return MapElement;
                    if (CommentElement != null) yield return CommentElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (IdentityElement != null) yield return new ElementValue("identity", IdentityElement);
                    if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                    if (MapElement != null) yield return new ElementValue("map", MapElement);
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
 
                } 
            } 
            
        }                
        /// <summary>
        /// Path of the element in the hierarchy of elements
        /// </summary>
        [FhirElement("path", InSummary=true, Order=30)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PathElement
        {
            get { return _PathElement; }
            set { _PathElement = value; OnPropertyChanged("PathElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PathElement;
        
        /// <summary>
        /// Path of the element in the hierarchy of elements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Path
        {
            get { return PathElement != null ? PathElement.Value : null; }
            set
            {
                if (value == null)
                  PathElement = null; 
                else
                  PathElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Path");
            }
        }
        
        /// <summary>
        /// xmlAttr | xmlText | typeAttr | cdaText | xhtml
        /// </summary>
        [FhirElement("representation", InSummary=true, Order=40)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation>> RepresentationElement
        {
            get { if(_RepresentationElement==null) _RepresentationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation>>(); return _RepresentationElement; }
            set { _RepresentationElement = value; OnPropertyChanged("RepresentationElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation>> _RepresentationElement;
        
        /// <summary>
        /// xmlAttr | xmlText | typeAttr | cdaText | xhtml
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation?> Representation
        {
            get { return RepresentationElement != null ? RepresentationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  RepresentationElement = null; 
                else
                  RepresentationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation>(elem)));
                OnPropertyChanged("Representation");
            }
        }
        
        /// <summary>
        /// Name for this particular element (in a set of slices)
        /// </summary>
        [FhirElement("sliceName", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SliceNameElement
        {
            get { return _SliceNameElement; }
            set { _SliceNameElement = value; OnPropertyChanged("SliceNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SliceNameElement;
        
        /// <summary>
        /// Name for this particular element (in a set of slices)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string SliceName
        {
            get { return SliceNameElement != null ? SliceNameElement.Value : null; }
            set
            {
                if (value == null)
                  SliceNameElement = null; 
                else
                  SliceNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("SliceName");
            }
        }
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        [FhirElement("label", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LabelElement
        {
            get { return _LabelElement; }
            set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _LabelElement;
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Label
        {
            get { return LabelElement != null ? LabelElement.Value : null; }
            set
            {
                if (value == null)
                  LabelElement = null; 
                else
                  LabelElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Label");
            }
        }
        
        /// <summary>
        /// Corresponding codes in terminologies
        /// </summary>
        [FhirElement("code", InSummary=true, Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Code;
        
        /// <summary>
        /// This element is sliced - slices follow
        /// </summary>
        [FhirElement("slicing", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.ElementDefinition.SlicingComponent Slicing
        {
            get { return _Slicing; }
            set { _Slicing = value; OnPropertyChanged("Slicing"); }
        }
        
        private Hl7.Fhir.Model.ElementDefinition.SlicingComponent _Slicing;
        
        /// <summary>
        /// Concise definition for space-constrained presentation
        /// </summary>
        [FhirElement("short", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ShortElement
        {
            get { return _ShortElement; }
            set { _ShortElement = value; OnPropertyChanged("ShortElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ShortElement;
        
        /// <summary>
        /// Concise definition for space-constrained presentation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Short
        {
            get { return ShortElement != null ? ShortElement.Value : null; }
            set
            {
                if (value == null)
                  ShortElement = null; 
                else
                  ShortElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Short");
            }
        }
        
        /// <summary>
        /// Full formal definition as narrative text
        /// </summary>
        [FhirElement("definition", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DefinitionElement
        {
            get { return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DefinitionElement;
        
        /// <summary>
        /// Full formal definition as narrative text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Definition
        {
            get { return DefinitionElement != null ? DefinitionElement.Value : null; }
            set
            {
                if (value == null)
                  DefinitionElement = null; 
                else
                  DefinitionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Definition");
            }
        }
        
        /// <summary>
        /// Comments about the use of this element
        /// </summary>
        [FhirElement("comment", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CommentElement;
        
        /// <summary>
        /// Comments about the use of this element
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
                  CommentElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Why this resource has been created
        /// </summary>
        [FhirElement("requirements", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _RequirementsElement;
        
        /// <summary>
        /// Why this resource has been created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if (value == null)
                  RequirementsElement = null; 
                else
                  RequirementsElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Other names
        /// </summary>
        [FhirElement("alias", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if(_AliasElement==null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _AliasElement;
        
        /// <summary>
        /// Other names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Alias
        {
            get { return AliasElement != null ? AliasElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  AliasElement = null; 
                else
                  AliasElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Alias");
            }
        }
        
        /// <summary>
        /// Minimum Cardinality
        /// </summary>
        [FhirElement("min", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt MinElement
        {
            get { return _MinElement; }
            set { _MinElement = value; OnPropertyChanged("MinElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _MinElement;
        
        /// <summary>
        /// Minimum Cardinality
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Min
        {
            get { return MinElement != null ? MinElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  MinElement = null; 
                else
                  MinElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Min");
            }
        }
        
        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        [FhirElement("max", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString MaxElement
        {
            get { return _MaxElement; }
            set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _MaxElement;
        
        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Max
        {
            get { return MaxElement != null ? MaxElement.Value : null; }
            set
            {
                if (value == null)
                  MaxElement = null; 
                else
                  MaxElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Max");
            }
        }
        
        /// <summary>
        /// Base definition information for tools
        /// </summary>
        [FhirElement("base", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ElementDefinition.BaseComponent Base
        {
            get { return _Base; }
            set { _Base = value; OnPropertyChanged("Base"); }
        }
        
        private Hl7.Fhir.Model.ElementDefinition.BaseComponent _Base;
        
        /// <summary>
        /// Reference to definition of content for the element
        /// </summary>
        [FhirElement("contentReference", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ContentReferenceElement
        {
            get { return _ContentReferenceElement; }
            set { _ContentReferenceElement = value; OnPropertyChanged("ContentReferenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ContentReferenceElement;
        
        /// <summary>
        /// Reference to definition of content for the element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ContentReference
        {
            get { return ContentReferenceElement != null ? ContentReferenceElement.Value : null; }
            set
            {
                if (value == null)
                  ContentReferenceElement = null; 
                else
                  ContentReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ContentReference");
            }
        }
        
        /// <summary>
        /// Data type and Profile for this element
        /// </summary>
        [FhirElement("type", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ElementDefinition.TypeRefComponent> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.ElementDefinition.TypeRefComponent>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.ElementDefinition.TypeRefComponent> _Type;
        
        /// <summary>
        /// Specified value if missing from instance
        /// </summary>
        [FhirElement("defaultValue", InSummary=true, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.ContactPoint),typeof(Count),typeof(Distance),typeof(Duration),typeof(Hl7.Fhir.Model.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Money),typeof(Hl7.Fhir.Model.Period),typeof(Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Signature),typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; OnPropertyChanged("DefaultValue"); }
        }
        
        private Hl7.Fhir.Model.Element _DefaultValue;
        
        /// <summary>
        /// Implicit meaning when this element is missing
        /// </summary>
        [FhirElement("meaningWhenMissing", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown MeaningWhenMissingElement
        {
            get { return _MeaningWhenMissingElement; }
            set { _MeaningWhenMissingElement = value; OnPropertyChanged("MeaningWhenMissingElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _MeaningWhenMissingElement;
        
        /// <summary>
        /// Implicit meaning when this element is missing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string MeaningWhenMissing
        {
            get { return MeaningWhenMissingElement != null ? MeaningWhenMissingElement.Value : null; }
            set
            {
                if (value == null)
                  MeaningWhenMissingElement = null; 
                else
                  MeaningWhenMissingElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("MeaningWhenMissing");
            }
        }
        
        /// <summary>
        /// What the order of the elements means
        /// </summary>
        [FhirElement("orderMeaning", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OrderMeaningElement
        {
            get { return _OrderMeaningElement; }
            set { _OrderMeaningElement = value; OnPropertyChanged("OrderMeaningElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _OrderMeaningElement;
        
        /// <summary>
        /// What the order of the elements means
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrderMeaning
        {
            get { return OrderMeaningElement != null ? OrderMeaningElement.Value : null; }
            set
            {
                if (value == null)
                  OrderMeaningElement = null; 
                else
                  OrderMeaningElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("OrderMeaning");
            }
        }
        
        /// <summary>
        /// Value must be exactly this
        /// </summary>
        [FhirElement("fixed", InSummary=true, Order=220, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.ContactPoint),typeof(Count),typeof(Distance),typeof(Duration),typeof(Hl7.Fhir.Model.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Money),typeof(Hl7.Fhir.Model.Period),typeof(Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Signature),typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Fixed
        {
            get { return _Fixed; }
            set { _Fixed = value; OnPropertyChanged("Fixed"); }
        }
        
        private Hl7.Fhir.Model.Element _Fixed;
        
        /// <summary>
        /// Value must have at least these property values
        /// </summary>
        [FhirElement("pattern", InSummary=true, Order=230, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.ContactPoint),typeof(Count),typeof(Distance),typeof(Duration),typeof(Hl7.Fhir.Model.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Money),typeof(Hl7.Fhir.Model.Period),typeof(Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Signature),typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; OnPropertyChanged("Pattern"); }
        }
        
        private Hl7.Fhir.Model.Element _Pattern;
        
        /// <summary>
        /// Example value (as defined for type)
        /// </summary>
        [FhirElement("example", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ElementDefinition.ExampleComponent> Example
        {
            get { if(_Example==null) _Example = new List<Hl7.Fhir.Model.ElementDefinition.ExampleComponent>(); return _Example; }
            set { _Example = value; OnPropertyChanged("Example"); }
        }
        
        private List<Hl7.Fhir.Model.ElementDefinition.ExampleComponent> _Example;
        
        /// <summary>
        /// Minimum Allowed Value (for some types)
        /// </summary>
        [FhirElement("minValue", InSummary=true, Order=250, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Quantity))]
        [DataMember]
        public Hl7.Fhir.Model.Element MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; OnPropertyChanged("MinValue"); }
        }
        
        private Hl7.Fhir.Model.Element _MinValue;
        
        /// <summary>
        /// Maximum Allowed Value (for some types)
        /// </summary>
        [FhirElement("maxValue", InSummary=true, Order=260, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Quantity))]
        [DataMember]
        public Hl7.Fhir.Model.Element MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; OnPropertyChanged("MaxValue"); }
        }
        
        private Hl7.Fhir.Model.Element _MaxValue;
        
        /// <summary>
        /// Max length for strings
        /// </summary>
        [FhirElement("maxLength", InSummary=true, Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.Integer MaxLengthElement
        {
            get { return _MaxLengthElement; }
            set { _MaxLengthElement = value; OnPropertyChanged("MaxLengthElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _MaxLengthElement;
        
        /// <summary>
        /// Max length for strings
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? MaxLength
        {
            get { return MaxLengthElement != null ? MaxLengthElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  MaxLengthElement = null; 
                else
                  MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("MaxLength");
            }
        }
        
        /// <summary>
        /// Reference to invariant about presence
        /// </summary>
        [FhirElement("condition", InSummary=true, Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Id> ConditionElement
        {
            get { if(_ConditionElement==null) _ConditionElement = new List<Hl7.Fhir.Model.Id>(); return _ConditionElement; }
            set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
        }
        
        private List<Hl7.Fhir.Model.Id> _ConditionElement;
        
        /// <summary>
        /// Reference to invariant about presence
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Condition
        {
            get { return ConditionElement != null ? ConditionElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ConditionElement = null; 
                else
                  ConditionElement = new List<Hl7.Fhir.Model.Id>(value.Select(elem=>new Hl7.Fhir.Model.Id(elem)));
                OnPropertyChanged("Condition");
            }
        }
        
        /// <summary>
        /// Condition that must evaluate to true
        /// </summary>
        [FhirElement("constraint", InSummary=true, Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ElementDefinition.ConstraintComponent> Constraint
        {
            get { if(_Constraint==null) _Constraint = new List<Hl7.Fhir.Model.ElementDefinition.ConstraintComponent>(); return _Constraint; }
            set { _Constraint = value; OnPropertyChanged("Constraint"); }
        }
        
        private List<Hl7.Fhir.Model.ElementDefinition.ConstraintComponent> _Constraint;
        
        /// <summary>
        /// If the element must supported
        /// </summary>
        [FhirElement("mustSupport", InSummary=true, Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean MustSupportElement
        {
            get { return _MustSupportElement; }
            set { _MustSupportElement = value; OnPropertyChanged("MustSupportElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _MustSupportElement;
        
        /// <summary>
        /// If the element must supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? MustSupport
        {
            get { return MustSupportElement != null ? MustSupportElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  MustSupportElement = null; 
                else
                  MustSupportElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("MustSupport");
            }
        }
        
        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        [FhirElement("isModifier", InSummary=true, Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IsModifierElement
        {
            get { return _IsModifierElement; }
            set { _IsModifierElement = value; OnPropertyChanged("IsModifierElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IsModifierElement;
        
        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IsModifier
        {
            get { return IsModifierElement != null ? IsModifierElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IsModifierElement = null; 
                else
                  IsModifierElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IsModifier");
            }
        }
        
        /// <summary>
        /// Include when _summary = true?
        /// </summary>
        [FhirElement("isSummary", InSummary=true, Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IsSummaryElement
        {
            get { return _IsSummaryElement; }
            set { _IsSummaryElement = value; OnPropertyChanged("IsSummaryElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IsSummaryElement;
        
        /// <summary>
        /// Include when _summary = true?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IsSummary
        {
            get { return IsSummaryElement != null ? IsSummaryElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IsSummaryElement = null; 
                else
                  IsSummaryElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IsSummary");
            }
        }
        
        /// <summary>
        /// ValueSet details if this is coded
        /// </summary>
        [FhirElement("binding", InSummary=true, Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.ElementDefinition.ElementDefinitionBindingComponent Binding
        {
            get { return _Binding; }
            set { _Binding = value; OnPropertyChanged("Binding"); }
        }
        
        private Hl7.Fhir.Model.ElementDefinition.ElementDefinitionBindingComponent _Binding;
        
        /// <summary>
        /// Map element to another set of definitions
        /// </summary>
        [FhirElement("mapping", InSummary=true, Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ElementDefinition.MappingComponent> Mapping
        {
            get { if(_Mapping==null) _Mapping = new List<Hl7.Fhir.Model.ElementDefinition.MappingComponent>(); return _Mapping; }
            set { _Mapping = value; OnPropertyChanged("Mapping"); }
        }
        
        private List<Hl7.Fhir.Model.ElementDefinition.MappingComponent> _Mapping;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ElementDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                if(RepresentationElement != null) dest.RepresentationElement = new List<Code<Hl7.Fhir.Model.ElementDefinition.PropertyRepresentation>>(RepresentationElement.DeepCopy());
                if(SliceNameElement != null) dest.SliceNameElement = (Hl7.Fhir.Model.FhirString)SliceNameElement.DeepCopy();
                if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(Slicing != null) dest.Slicing = (Hl7.Fhir.Model.ElementDefinition.SlicingComponent)Slicing.DeepCopy();
                if(ShortElement != null) dest.ShortElement = (Hl7.Fhir.Model.FhirString)ShortElement.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Markdown)DefinitionElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.Markdown)CommentElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.Markdown)RequirementsElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.UnsignedInt)MinElement.DeepCopy();
                if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                if(Base != null) dest.Base = (Hl7.Fhir.Model.ElementDefinition.BaseComponent)Base.DeepCopy();
                if(ContentReferenceElement != null) dest.ContentReferenceElement = (Hl7.Fhir.Model.FhirUri)ContentReferenceElement.DeepCopy();
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.ElementDefinition.TypeRefComponent>(Type.DeepCopy());
                if(DefaultValue != null) dest.DefaultValue = (Hl7.Fhir.Model.Element)DefaultValue.DeepCopy();
                if(MeaningWhenMissingElement != null) dest.MeaningWhenMissingElement = (Hl7.Fhir.Model.Markdown)MeaningWhenMissingElement.DeepCopy();
                if(OrderMeaningElement != null) dest.OrderMeaningElement = (Hl7.Fhir.Model.FhirString)OrderMeaningElement.DeepCopy();
                if(Fixed != null) dest.Fixed = (Hl7.Fhir.Model.Element)Fixed.DeepCopy();
                if(Pattern != null) dest.Pattern = (Hl7.Fhir.Model.Element)Pattern.DeepCopy();
                if(Example != null) dest.Example = new List<Hl7.Fhir.Model.ElementDefinition.ExampleComponent>(Example.DeepCopy());
                if(MinValue != null) dest.MinValue = (Hl7.Fhir.Model.Element)MinValue.DeepCopy();
                if(MaxValue != null) dest.MaxValue = (Hl7.Fhir.Model.Element)MaxValue.DeepCopy();
                if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                if(ConditionElement != null) dest.ConditionElement = new List<Hl7.Fhir.Model.Id>(ConditionElement.DeepCopy());
                if(Constraint != null) dest.Constraint = new List<Hl7.Fhir.Model.ElementDefinition.ConstraintComponent>(Constraint.DeepCopy());
                if(MustSupportElement != null) dest.MustSupportElement = (Hl7.Fhir.Model.FhirBoolean)MustSupportElement.DeepCopy();
                if(IsModifierElement != null) dest.IsModifierElement = (Hl7.Fhir.Model.FhirBoolean)IsModifierElement.DeepCopy();
                if(IsSummaryElement != null) dest.IsSummaryElement = (Hl7.Fhir.Model.FhirBoolean)IsSummaryElement.DeepCopy();
                if(Binding != null) dest.Binding = (Hl7.Fhir.Model.ElementDefinition.ElementDefinitionBindingComponent)Binding.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.ElementDefinition.MappingComponent>(Mapping.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ElementDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ElementDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
            if( !DeepComparable.Matches(RepresentationElement, otherT.RepresentationElement)) return false;
            if( !DeepComparable.Matches(SliceNameElement, otherT.SliceNameElement)) return false;
            if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Slicing, otherT.Slicing)) return false;
            if( !DeepComparable.Matches(ShortElement, otherT.ShortElement)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.Matches(Base, otherT.Base)) return false;
            if( !DeepComparable.Matches(ContentReferenceElement, otherT.ContentReferenceElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(DefaultValue, otherT.DefaultValue)) return false;
            if( !DeepComparable.Matches(MeaningWhenMissingElement, otherT.MeaningWhenMissingElement)) return false;
            if( !DeepComparable.Matches(OrderMeaningElement, otherT.OrderMeaningElement)) return false;
            if( !DeepComparable.Matches(Fixed, otherT.Fixed)) return false;
            if( !DeepComparable.Matches(Pattern, otherT.Pattern)) return false;
            if( !DeepComparable.Matches(Example, otherT.Example)) return false;
            if( !DeepComparable.Matches(MinValue, otherT.MinValue)) return false;
            if( !DeepComparable.Matches(MaxValue, otherT.MaxValue)) return false;
            if( !DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
            if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.Matches(Constraint, otherT.Constraint)) return false;
            if( !DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.Matches(IsModifierElement, otherT.IsModifierElement)) return false;
            if( !DeepComparable.Matches(IsSummaryElement, otherT.IsSummaryElement)) return false;
            if( !DeepComparable.Matches(Binding, otherT.Binding)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ElementDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
            if( !DeepComparable.IsExactly(RepresentationElement, otherT.RepresentationElement)) return false;
            if( !DeepComparable.IsExactly(SliceNameElement, otherT.SliceNameElement)) return false;
            if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Slicing, otherT.Slicing)) return false;
            if( !DeepComparable.IsExactly(ShortElement, otherT.ShortElement)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.IsExactly(Base, otherT.Base)) return false;
            if( !DeepComparable.IsExactly(ContentReferenceElement, otherT.ContentReferenceElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(DefaultValue, otherT.DefaultValue)) return false;
            if( !DeepComparable.IsExactly(MeaningWhenMissingElement, otherT.MeaningWhenMissingElement)) return false;
            if( !DeepComparable.IsExactly(OrderMeaningElement, otherT.OrderMeaningElement)) return false;
            if( !DeepComparable.IsExactly(Fixed, otherT.Fixed)) return false;
            if( !DeepComparable.IsExactly(Pattern, otherT.Pattern)) return false;
            if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
            if( !DeepComparable.IsExactly(MinValue, otherT.MinValue)) return false;
            if( !DeepComparable.IsExactly(MaxValue, otherT.MaxValue)) return false;
            if( !DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
            if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.IsExactly(Constraint, otherT.Constraint)) return false;
            if( !DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.IsExactly(IsModifierElement, otherT.IsModifierElement)) return false;
            if( !DeepComparable.IsExactly(IsSummaryElement, otherT.IsSummaryElement)) return false;
            if( !DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (PathElement != null) yield return PathElement;
                foreach (var elem in RepresentationElement) { if (elem != null) yield return elem; }
                if (SliceNameElement != null) yield return SliceNameElement;
                if (LabelElement != null) yield return LabelElement;
                foreach (var elem in Code) { if (elem != null) yield return elem; }
                if (Slicing != null) yield return Slicing;
                if (ShortElement != null) yield return ShortElement;
                if (DefinitionElement != null) yield return DefinitionElement;
                if (CommentElement != null) yield return CommentElement;
                if (RequirementsElement != null) yield return RequirementsElement;
                foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
                if (MinElement != null) yield return MinElement;
                if (MaxElement != null) yield return MaxElement;
                if (Base != null) yield return Base;
                if (ContentReferenceElement != null) yield return ContentReferenceElement;
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (DefaultValue != null) yield return DefaultValue;
                if (MeaningWhenMissingElement != null) yield return MeaningWhenMissingElement;
                if (OrderMeaningElement != null) yield return OrderMeaningElement;
                if (Fixed != null) yield return Fixed;
                if (Pattern != null) yield return Pattern;
                foreach (var elem in Example) { if (elem != null) yield return elem; }
                if (MinValue != null) yield return MinValue;
                if (MaxValue != null) yield return MaxValue;
                if (MaxLengthElement != null) yield return MaxLengthElement;
                foreach (var elem in ConditionElement) { if (elem != null) yield return elem; }
                foreach (var elem in Constraint) { if (elem != null) yield return elem; }
                if (MustSupportElement != null) yield return MustSupportElement;
                if (IsModifierElement != null) yield return IsModifierElement;
                if (IsSummaryElement != null) yield return IsSummaryElement;
                if (Binding != null) yield return Binding;
                foreach (var elem in Mapping) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (PathElement != null) yield return new ElementValue("path", PathElement);
                foreach (var elem in RepresentationElement) { if (elem != null) yield return new ElementValue("representation", elem); }
                if (SliceNameElement != null) yield return new ElementValue("sliceName", SliceNameElement);
                if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                if (Slicing != null) yield return new ElementValue("slicing", Slicing);
                if (ShortElement != null) yield return new ElementValue("short", ShortElement);
                if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (MinElement != null) yield return new ElementValue("min", MinElement);
                if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                if (Base != null) yield return new ElementValue("base", Base);
                if (ContentReferenceElement != null) yield return new ElementValue("contentReference", ContentReferenceElement);
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (DefaultValue != null) yield return new ElementValue("defaultValue", DefaultValue);
                if (MeaningWhenMissingElement != null) yield return new ElementValue("meaningWhenMissing", MeaningWhenMissingElement);
                if (OrderMeaningElement != null) yield return new ElementValue("orderMeaning", OrderMeaningElement);
                if (Fixed != null) yield return new ElementValue("fixed", Fixed);
                if (Pattern != null) yield return new ElementValue("pattern", Pattern);
                foreach (var elem in Example) { if (elem != null) yield return new ElementValue("example", elem); }
                if (MinValue != null) yield return new ElementValue("minValue", MinValue);
                if (MaxValue != null) yield return new ElementValue("maxValue", MaxValue);
                if (MaxLengthElement != null) yield return new ElementValue("maxLength", MaxLengthElement);
                foreach (var elem in ConditionElement) { if (elem != null) yield return new ElementValue("condition", elem); }
                foreach (var elem in Constraint) { if (elem != null) yield return new ElementValue("constraint", elem); }
                if (MustSupportElement != null) yield return new ElementValue("mustSupport", MustSupportElement);
                if (IsModifierElement != null) yield return new ElementValue("isModifier", IsModifierElement);
                if (IsSummaryElement != null) yield return new ElementValue("isSummary", IsSummaryElement);
                if (Binding != null) yield return new ElementValue("binding", Binding);
                foreach (var elem in Mapping) { if (elem != null) yield return new ElementValue("mapping", elem); }
 
            } 
        } 
    
    
    }
    
}
