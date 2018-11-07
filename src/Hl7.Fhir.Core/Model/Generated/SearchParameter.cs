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
    /// Search Parameter for a resource
    /// </summary>
    [FhirType("SearchParameter", IsResource=true)]
    [DataContract]
    public partial class SearchParameter : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SearchParameter; } }
        [NotMapped]
        public override string TypeName { get { return "SearchParameter"; } }
        
        /// <summary>
        /// How a search parameter relates to the set of elements returned by evaluating its xpath query.
        /// (url: http://hl7.org/fhir/ValueSet/search-xpath-usage)
        /// </summary>
        [FhirEnumeration("XPathUsageType")]
        public enum XPathUsageType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-xpath-usage)
            /// </summary>
            [EnumLiteral("normal", "http://hl7.org/fhir/search-xpath-usage"), Description("Normal")]
            Normal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-xpath-usage)
            /// </summary>
            [EnumLiteral("phonetic", "http://hl7.org/fhir/search-xpath-usage"), Description("Phonetic")]
            Phonetic,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-xpath-usage)
            /// </summary>
            [EnumLiteral("nearby", "http://hl7.org/fhir/search-xpath-usage"), Description("Nearby")]
            Nearby,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-xpath-usage)
            /// </summary>
            [EnumLiteral("distance", "http://hl7.org/fhir/search-xpath-usage"), Description("Distance")]
            Distance,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-xpath-usage)
            /// </summary>
            [EnumLiteral("other", "http://hl7.org/fhir/search-xpath-usage"), Description("Other")]
            Other,
        }

        /// <summary>
        /// What Search Comparator Codes are supported in search
        /// (url: http://hl7.org/fhir/ValueSet/search-comparator)
        /// </summary>
        [FhirEnumeration("SearchComparator")]
        public enum SearchComparator
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("eq", "http://hl7.org/fhir/search-comparator"), Description("Equals")]
            Eq,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("ne", "http://hl7.org/fhir/search-comparator"), Description("Not Equals")]
            Ne,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("gt", "http://hl7.org/fhir/search-comparator"), Description("Greater Than")]
            Gt,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("lt", "http://hl7.org/fhir/search-comparator"), Description("Less Then")]
            Lt,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("ge", "http://hl7.org/fhir/search-comparator"), Description("Greater or Equals")]
            Ge,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("le", "http://hl7.org/fhir/search-comparator"), Description("Less of Equal")]
            Le,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("sa", "http://hl7.org/fhir/search-comparator"), Description("Starts After")]
            Sa,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("eb", "http://hl7.org/fhir/search-comparator"), Description("Ends Before")]
            Eb,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-comparator)
            /// </summary>
            [EnumLiteral("ap", "http://hl7.org/fhir/search-comparator"), Description("Approximately")]
            Ap,
        }

        /// <summary>
        /// A supported modifier for a search parameter.
        /// (url: http://hl7.org/fhir/ValueSet/search-modifier-code)
        /// </summary>
        [FhirEnumeration("SearchModifierCode")]
        public enum SearchModifierCode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("missing", "http://hl7.org/fhir/search-modifier-code"), Description("Missing")]
            Missing,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("exact", "http://hl7.org/fhir/search-modifier-code"), Description("Exact")]
            Exact,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("contains", "http://hl7.org/fhir/search-modifier-code"), Description("Contains")]
            Contains,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("not", "http://hl7.org/fhir/search-modifier-code"), Description("Not")]
            Not,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("text", "http://hl7.org/fhir/search-modifier-code"), Description("Text")]
            Text,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("in", "http://hl7.org/fhir/search-modifier-code"), Description("In")]
            In,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("not-in", "http://hl7.org/fhir/search-modifier-code"), Description("Not In")]
            NotIn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("below", "http://hl7.org/fhir/search-modifier-code"), Description("Below")]
            Below,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("above", "http://hl7.org/fhir/search-modifier-code"), Description("Above")]
            Above,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/search-modifier-code)
            /// </summary>
            [EnumLiteral("type", "http://hl7.org/fhir/search-modifier-code"), Description("Type")]
            Type,
        }

        [FhirType("ComponentComponent")]
        [DataContract]
        public partial class ComponentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ComponentComponent"; } }
            
            /// <summary>
            /// Defines how the part works
            /// </summary>
            [FhirElement("definition", Order=40)]
            [CLSCompliant(false)]
			[References("SearchParameter")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Definition;
            
            /// <summary>
            /// Subexpression relative to main expression
            /// </summary>
            [FhirElement("expression", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// Subexpression relative to main expression
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComponentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ComponentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Definition != null) yield return Definition;
                    if (ExpressionElement != null) yield return ExpressionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Definition != null) yield return new ElementValue("definition", Definition);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Logical URI to reference this search parameter (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Logical URI to reference this search parameter (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Business version of the search parameter
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the search parameter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this search parameter (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this search parameter (computer friendly)
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
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date this was last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date this was last changed
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
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for search parameter (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this search parameter is defined
        /// </summary>
        [FhirElement("purpose", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Purpose;
        
        /// <summary>
        /// Code used in URL
        /// </summary>
        [FhirElement("code", InSummary=true, Order=200)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code CodeElement
        {
            get { return _CodeElement; }
            set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _CodeElement;
        
        /// <summary>
        /// Code used in URL
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
                  CodeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Code");
            }
        }
        
        /// <summary>
        /// The resource type(s) this search parameter applies to
        /// </summary>
        [FhirElement("base", InSummary=true, Order=210)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ResourceType>> BaseElement
        {
            get { if(_BaseElement==null) _BaseElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(); return _BaseElement; }
            set { _BaseElement = value; OnPropertyChanged("BaseElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ResourceType>> _BaseElement;
        
        /// <summary>
        /// The resource type(s) this search parameter applies to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ResourceType?> Base
        {
            get { return BaseElement != null ? BaseElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  BaseElement = null; 
                else
                  BaseElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>(elem)));
                OnPropertyChanged("Base");
            }
        }
        
        /// <summary>
        /// number | date | string | token | reference | composite | quantity | uri
        /// </summary>
        [FhirElement("type", InSummary=true, Order=220)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SearchParamType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SearchParamType> _TypeElement;
        
        /// <summary>
        /// number | date | string | token | reference | composite | quantity | uri
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SearchParamType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.SearchParamType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Original Definition for the search parameter
        /// </summary>
        [FhirElement("derivedFrom", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri DerivedFromElement
        {
            get { return _DerivedFromElement; }
            set { _DerivedFromElement = value; OnPropertyChanged("DerivedFromElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _DerivedFromElement;
        
        /// <summary>
        /// Original Definition for the search parameter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DerivedFrom
        {
            get { return DerivedFromElement != null ? DerivedFromElement.Value : null; }
            set
            {
                if (value == null)
                  DerivedFromElement = null; 
                else
                  DerivedFromElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("DerivedFrom");
            }
        }
        
        /// <summary>
        /// Natural language description of the search parameter
        /// </summary>
        [FhirElement("description", InSummary=true, Order=240)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// FHIRPath expression that extracts the values
        /// </summary>
        [FhirElement("expression", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ExpressionElement
        {
            get { return _ExpressionElement; }
            set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ExpressionElement;
        
        /// <summary>
        /// FHIRPath expression that extracts the values
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
        /// XPath that extracts the values
        /// </summary>
        [FhirElement("xpath", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString XpathElement
        {
            get { return _XpathElement; }
            set { _XpathElement = value; OnPropertyChanged("XpathElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _XpathElement;
        
        /// <summary>
        /// XPath that extracts the values
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
        /// normal | phonetic | nearby | distance | other
        /// </summary>
        [FhirElement("xpathUsage", Order=270)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SearchParameter.XPathUsageType> XpathUsageElement
        {
            get { return _XpathUsageElement; }
            set { _XpathUsageElement = value; OnPropertyChanged("XpathUsageElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SearchParameter.XPathUsageType> _XpathUsageElement;
        
        /// <summary>
        /// normal | phonetic | nearby | distance | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SearchParameter.XPathUsageType? XpathUsage
        {
            get { return XpathUsageElement != null ? XpathUsageElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  XpathUsageElement = null; 
                else
                  XpathUsageElement = new Code<Hl7.Fhir.Model.SearchParameter.XPathUsageType>(value);
                OnPropertyChanged("XpathUsage");
            }
        }
        
        /// <summary>
        /// Types of resource (if a resource reference)
        /// </summary>
        [FhirElement("target", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ResourceType>> TargetElement
        {
            get { if(_TargetElement==null) _TargetElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(); return _TargetElement; }
            set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ResourceType>> _TargetElement;
        
        /// <summary>
        /// Types of resource (if a resource reference)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ResourceType?> Target
        {
            get { return TargetElement != null ? TargetElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  TargetElement = null; 
                else
                  TargetElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>(elem)));
                OnPropertyChanged("Target");
            }
        }
        
        /// <summary>
        /// eq | ne | gt | lt | ge | le | sa | eb | ap
        /// </summary>
        [FhirElement("comparator", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.SearchParameter.SearchComparator>> ComparatorElement
        {
            get { if(_ComparatorElement==null) _ComparatorElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParameter.SearchComparator>>(); return _ComparatorElement; }
            set { _ComparatorElement = value; OnPropertyChanged("ComparatorElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.SearchParameter.SearchComparator>> _ComparatorElement;
        
        /// <summary>
        /// eq | ne | gt | lt | ge | le | sa | eb | ap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.SearchParameter.SearchComparator?> Comparator
        {
            get { return ComparatorElement != null ? ComparatorElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ComparatorElement = null; 
                else
                  ComparatorElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParameter.SearchComparator>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParameter.SearchComparator>(elem)));
                OnPropertyChanged("Comparator");
            }
        }
        
        /// <summary>
        /// missing | exact | contains | not | text | in | not-in | below | above | type
        /// </summary>
        [FhirElement("modifier", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.SearchParameter.SearchModifierCode>> ModifierElement
        {
            get { if(_ModifierElement==null) _ModifierElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParameter.SearchModifierCode>>(); return _ModifierElement; }
            set { _ModifierElement = value; OnPropertyChanged("ModifierElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.SearchParameter.SearchModifierCode>> _ModifierElement;
        
        /// <summary>
        /// missing | exact | contains | not | text | in | not-in | below | above | type
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.SearchParameter.SearchModifierCode?> Modifier
        {
            get { return ModifierElement != null ? ModifierElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ModifierElement = null; 
                else
                  ModifierElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParameter.SearchModifierCode>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SearchParameter.SearchModifierCode>(elem)));
                OnPropertyChanged("Modifier");
            }
        }
        
        /// <summary>
        /// Chained names supported
        /// </summary>
        [FhirElement("chain", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ChainElement
        {
            get { if(_ChainElement==null) _ChainElement = new List<Hl7.Fhir.Model.FhirString>(); return _ChainElement; }
            set { _ChainElement = value; OnPropertyChanged("ChainElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ChainElement;
        
        /// <summary>
        /// Chained names supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Chain
        {
            get { return ChainElement != null ? ChainElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ChainElement = null; 
                else
                  ChainElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Chain");
            }
        }
        
        /// <summary>
        /// For Composite resources to define the parts
        /// </summary>
        [FhirElement("component", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SearchParameter.ComponentComponent> Component
        {
            get { if(_Component==null) _Component = new List<Hl7.Fhir.Model.SearchParameter.ComponentComponent>(); return _Component; }
            set { _Component = value; OnPropertyChanged("Component"); }
        }
        
        private List<Hl7.Fhir.Model.SearchParameter.ComponentComponent> _Component;
        

        public static ElementDefinition.ConstraintComponent SearchParameter_SPD_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "xpath.empty() or xpathUsage.exists()",
            Key = "spd-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If an xpath is present, there SHALL be an xpathUsage",
            Xpath = "not(exists(f:xpath)) or exists(f:xpathUsage)"
        };

        public static ElementDefinition.ConstraintComponent SearchParameter_SPD_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "chain.empty() or type = 'reference'",
            Key = "spd-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Search parameters can only have chain names when the search parameter type is 'reference'",
            Xpath = "not(exists(f:chain)) or (f:type/@value = 'reference')"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(SearchParameter_SPD_1);
            InvariantConstraints.Add(SearchParameter_SPD_2);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SearchParameter;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
                if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                if(BaseElement != null) dest.BaseElement = new List<Code<Hl7.Fhir.Model.ResourceType>>(BaseElement.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.SearchParamType>)TypeElement.DeepCopy();
                if(DerivedFromElement != null) dest.DerivedFromElement = (Hl7.Fhir.Model.FhirUri)DerivedFromElement.DeepCopy();
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                if(XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
                if(XpathUsageElement != null) dest.XpathUsageElement = (Code<Hl7.Fhir.Model.SearchParameter.XPathUsageType>)XpathUsageElement.DeepCopy();
                if(TargetElement != null) dest.TargetElement = new List<Code<Hl7.Fhir.Model.ResourceType>>(TargetElement.DeepCopy());
                if(ComparatorElement != null) dest.ComparatorElement = new List<Code<Hl7.Fhir.Model.SearchParameter.SearchComparator>>(ComparatorElement.DeepCopy());
                if(ModifierElement != null) dest.ModifierElement = new List<Code<Hl7.Fhir.Model.SearchParameter.SearchModifierCode>>(ModifierElement.DeepCopy());
                if(ChainElement != null) dest.ChainElement = new List<Hl7.Fhir.Model.FhirString>(ChainElement.DeepCopy());
                if(Component != null) dest.Component = new List<Hl7.Fhir.Model.SearchParameter.ComponentComponent>(Component.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SearchParameter());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SearchParameter;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
            if( !DeepComparable.Matches(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(DerivedFromElement, otherT.DerivedFromElement)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
            if( !DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
            if( !DeepComparable.Matches(XpathUsageElement, otherT.XpathUsageElement)) return false;
            if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
            if( !DeepComparable.Matches(ComparatorElement, otherT.ComparatorElement)) return false;
            if( !DeepComparable.Matches(ModifierElement, otherT.ModifierElement)) return false;
            if( !DeepComparable.Matches(ChainElement, otherT.ChainElement)) return false;
            if( !DeepComparable.Matches(Component, otherT.Component)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SearchParameter;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
            if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(DerivedFromElement, otherT.DerivedFromElement)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
            if( !DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
            if( !DeepComparable.IsExactly(XpathUsageElement, otherT.XpathUsageElement)) return false;
            if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
            if( !DeepComparable.IsExactly(ComparatorElement, otherT.ComparatorElement)) return false;
            if( !DeepComparable.IsExactly(ModifierElement, otherT.ModifierElement)) return false;
            if( !DeepComparable.IsExactly(ChainElement, otherT.ChainElement)) return false;
            if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Purpose != null) yield return Purpose;
				if (CodeElement != null) yield return CodeElement;
				foreach (var elem in BaseElement) { if (elem != null) yield return elem; }
				if (TypeElement != null) yield return TypeElement;
				if (DerivedFromElement != null) yield return DerivedFromElement;
				if (Description != null) yield return Description;
				if (ExpressionElement != null) yield return ExpressionElement;
				if (XpathElement != null) yield return XpathElement;
				if (XpathUsageElement != null) yield return XpathUsageElement;
				foreach (var elem in TargetElement) { if (elem != null) yield return elem; }
				foreach (var elem in ComparatorElement) { if (elem != null) yield return elem; }
				foreach (var elem in ModifierElement) { if (elem != null) yield return elem; }
				foreach (var elem in ChainElement) { if (elem != null) yield return elem; }
				foreach (var elem in Component) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                foreach (var elem in BaseElement) { if (elem != null) yield return new ElementValue("base", elem); }
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (DerivedFromElement != null) yield return new ElementValue("derivedFrom", DerivedFromElement);
                if (Description != null) yield return new ElementValue("description", Description);
                if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                if (XpathElement != null) yield return new ElementValue("xpath", XpathElement);
                if (XpathUsageElement != null) yield return new ElementValue("xpathUsage", XpathUsageElement);
                foreach (var elem in TargetElement) { if (elem != null) yield return new ElementValue("target", elem); }
                foreach (var elem in ComparatorElement) { if (elem != null) yield return new ElementValue("comparator", elem); }
                foreach (var elem in ModifierElement) { if (elem != null) yield return new ElementValue("modifier", elem); }
                foreach (var elem in ChainElement) { if (elem != null) yield return new ElementValue("chain", elem); }
                foreach (var elem in Component) { if (elem != null) yield return new ElementValue("component", elem); }
            }
        }

    }
    
}
