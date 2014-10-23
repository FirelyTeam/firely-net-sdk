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
    /// A set of codes drawn from one or more code systems
    /// </summary>
    [FhirType("ValueSet", IsResource=true)]
    [DataContract]
    public partial class ValueSet : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The lifecycle status of a Value Set or Concept Map
        /// </summary>
        [FhirEnumeration("ValueSetStatus")]
        public enum ValueSetStatus
        {
            /// <summary>
            /// This valueset is still under development.
            /// </summary>
            [EnumLiteral("draft")]
            Draft,
            /// <summary>
            /// This valueset is ready for normal use.
            /// </summary>
            [EnumLiteral("active")]
            Active,
            /// <summary>
            /// This valueset has been withdrawn or superceded and should no longer be used.
            /// </summary>
            [EnumLiteral("retired")]
            Retired,
        }
        
        /// <summary>
        /// The kind of operation to perform as a part of a property based filter
        /// </summary>
        [FhirEnumeration("FilterOperator")]
        public enum FilterOperator
        {
            /// <summary>
            /// The property value has the concept specified by the value.
            /// </summary>
            [EnumLiteral("=")]
            Equal,
            /// <summary>
            /// The property value has a concept that has an is-a relationship with the value.
            /// </summary>
            [EnumLiteral("is-a")]
            IsA,
            /// <summary>
            /// The property value has a concept that does not have an is-a relationship with the value.
            /// </summary>
            [EnumLiteral("is-not-a")]
            IsNotA,
            /// <summary>
            /// The property value representation matches the regex specified in the value.
            /// </summary>
            [EnumLiteral("regex")]
            Regex,
            /// <summary>
            /// The property value is in the set of codes or concepts identified by the value.
            /// </summary>
            [EnumLiteral("in")]
            In,
            /// <summary>
            /// The property value is not in the set of codes or concepts identified by the value.
            /// </summary>
            [EnumLiteral("not in")]
            NotIn,
        }
        
        [FhirType("ValueSetDefineComponent")]
        [DataContract]
        public partial class ValueSetDefineComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// URI to identify the code system
            /// </summary>
            [FhirElement("system", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// URI to identify the code system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("System");
                }
            }
            
            /// <summary>
            /// Version of this system
            /// </summary>
            [FhirElement("version", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version of this system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                    if(value == null)
                      VersionElement = null; 
                    else
                      VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
            
            /// <summary>
            /// If code comparison is case sensitive
            /// </summary>
            [FhirElement("caseSensitive", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CaseSensitiveElement
            {
                get { return _CaseSensitiveElement; }
                set { _CaseSensitiveElement = value; OnPropertyChanged("CaseSensitiveElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _CaseSensitiveElement;
            
            /// <summary>
            /// If code comparison is case sensitive
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? CaseSensitive
            {
                get { return CaseSensitiveElement != null ? CaseSensitiveElement.Value : null; }
                set
                {
                    if(value == null)
                      CaseSensitiveElement = null; 
                    else
                      CaseSensitiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("CaseSensitive");
                }
            }
            
            /// <summary>
            /// Concepts in the code system
            /// </summary>
            [FhirElement("concept", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> Concept
            {
                get { return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> _Concept;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValueSetDefineComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(CaseSensitiveElement != null) dest.CaseSensitiveElement = (Hl7.Fhir.Model.FhirBoolean)CaseSensitiveElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent>(Concept.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValueSetDefineComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValueSetDefineComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValueSetDefineComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ValueSetExpansionContainsComponent")]
        [DataContract]
        public partial class ValueSetExpansionContainsComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// System value for the code
            /// </summary>
            [FhirElement("system", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// System value for the code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("System");
                }
            }
            
            /// <summary>
            /// Code - if blank, this is not a choosable code
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code - if blank, this is not a choosable code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// User display for the concept
            /// </summary>
            [FhirElement("display", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// User display for the concept
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if(value == null)
                      DisplayElement = null; 
                    else
                      DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// Codes contained in this concept
            /// </summary>
            [FhirElement("contains", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> Contains
            {
                get { return _Contains; }
                set { _Contains = value; OnPropertyChanged("Contains"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> _Contains;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValueSetExpansionContainsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(Contains != null) dest.Contains = new List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent>(Contains.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValueSetExpansionContainsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValueSetExpansionContainsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(Contains, otherT.Contains)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValueSetExpansionContainsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(Contains, otherT.Contains)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConceptSetComponent")]
        [DataContract]
        public partial class ConceptSetComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The system the codes come from
            /// </summary>
            [FhirElement("system", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// The system the codes come from
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("System");
                }
            }
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            [FhirElement("version", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                    if(value == null)
                      VersionElement = null; 
                    else
                      VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
            
            /// <summary>
            /// Code or concept from system
            /// </summary>
            [FhirElement("code", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private List<Hl7.Fhir.Model.Code> _CodeElement;
            
            /// <summary>
            /// Code or concept from system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Code
            {
                get { return CodeElement != null ? CodeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Select codes/concepts by their properties (including relationships)
            /// </summary>
            [FhirElement("filter", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ConceptSetFilterComponent> Filter
            {
                get { return _Filter; }
                set { _Filter = value; OnPropertyChanged("Filter"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ConceptSetFilterComponent> _Filter;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptSetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = new List<Hl7.Fhir.Model.Code>(CodeElement.DeepCopy());
                    if(Filter != null) dest.Filter = new List<Hl7.Fhir.Model.ValueSet.ConceptSetFilterComponent>(Filter.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConceptSetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptSetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Filter, otherT.Filter)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptSetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Filter, otherT.Filter)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConceptSetFilterComponent")]
        [DataContract]
        public partial class ConceptSetFilterComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// A property defined by the code system
            /// </summary>
            [FhirElement("property", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code PropertyElement
            {
                get { return _PropertyElement; }
                set { _PropertyElement = value; OnPropertyChanged("PropertyElement"); }
            }
            private Hl7.Fhir.Model.Code _PropertyElement;
            
            /// <summary>
            /// A property defined by the code system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Property
            {
                get { return PropertyElement != null ? PropertyElement.Value : null; }
                set
                {
                    if(value == null)
                      PropertyElement = null; 
                    else
                      PropertyElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Property");
                }
            }
            
            /// <summary>
            /// = | is-a | is-not-a | regex | in | not in
            /// </summary>
            [FhirElement("op", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ValueSet.FilterOperator> OpElement
            {
                get { return _OpElement; }
                set { _OpElement = value; OnPropertyChanged("OpElement"); }
            }
            private Code<Hl7.Fhir.Model.ValueSet.FilterOperator> _OpElement;
            
            /// <summary>
            /// = | is-a | is-not-a | regex | in | not in
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ValueSet.FilterOperator? Op
            {
                get { return OpElement != null ? OpElement.Value : null; }
                set
                {
                    if(value == null)
                      OpElement = null; 
                    else
                      OpElement = new Code<Hl7.Fhir.Model.ValueSet.FilterOperator>(value);
                    OnPropertyChanged("Op");
                }
            }
            
            /// <summary>
            /// Code from the system, or regex criteria
            /// </summary>
            [FhirElement("value", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            private Hl7.Fhir.Model.Code _ValueElement;
            
            /// <summary>
            /// Code from the system, or regex criteria
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if(value == null)
                      ValueElement = null; 
                    else
                      ValueElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptSetFilterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PropertyElement != null) dest.PropertyElement = (Hl7.Fhir.Model.Code)PropertyElement.DeepCopy();
                    if(OpElement != null) dest.OpElement = (Code<Hl7.Fhir.Model.ValueSet.FilterOperator>)OpElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.Code)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConceptSetFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptSetFilterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PropertyElement, otherT.PropertyElement)) return false;
                if( !DeepComparable.Matches(OpElement, otherT.OpElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptSetFilterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PropertyElement, otherT.PropertyElement)) return false;
                if( !DeepComparable.IsExactly(OpElement, otherT.OpElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ValueSetComposeComponent")]
        [DataContract]
        public partial class ValueSetComposeComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Import the contents of another value set
            /// </summary>
            [FhirElement("import", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> ImportElement
            {
                get { return _ImportElement; }
                set { _ImportElement = value; OnPropertyChanged("ImportElement"); }
            }
            private List<Hl7.Fhir.Model.FhirUri> _ImportElement;
            
            /// <summary>
            /// Import the contents of another value set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Import
            {
                get { return ImportElement != null ? ImportElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ImportElement = null; 
                    else
                      ImportElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Import");
                }
            }
            
            /// <summary>
            /// Include one or more codes from a code system
            /// </summary>
            [FhirElement("include", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> Include
            {
                get { return _Include; }
                set { _Include = value; OnPropertyChanged("Include"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> _Include;
            
            /// <summary>
            /// Explicitly exclude codes
            /// </summary>
            [FhirElement("exclude", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> Exclude
            {
                get { return _Exclude; }
                set { _Exclude = value; OnPropertyChanged("Exclude"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> _Exclude;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValueSetComposeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ImportElement != null) dest.ImportElement = new List<Hl7.Fhir.Model.FhirUri>(ImportElement.DeepCopy());
                    if(Include != null) dest.Include = new List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent>(Include.DeepCopy());
                    if(Exclude != null) dest.Exclude = new List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent>(Exclude.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValueSetComposeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValueSetComposeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ImportElement, otherT.ImportElement)) return false;
                if( !DeepComparable.Matches(Include, otherT.Include)) return false;
                if( !DeepComparable.Matches(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValueSetComposeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ImportElement, otherT.ImportElement)) return false;
                if( !DeepComparable.IsExactly(Include, otherT.Include)) return false;
                if( !DeepComparable.IsExactly(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ValueSetDefineConceptComponent")]
        [DataContract]
        public partial class ValueSetDefineConceptComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Code that identifies concept
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code that identifies concept
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// If this code is not for use as a real concept
            /// </summary>
            [FhirElement("abstract", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AbstractElement
            {
                get { return _AbstractElement; }
                set { _AbstractElement = value; OnPropertyChanged("AbstractElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _AbstractElement;
            
            /// <summary>
            /// If this code is not for use as a real concept
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Abstract
            {
                get { return AbstractElement != null ? AbstractElement.Value : null; }
                set
                {
                    if(value == null)
                      AbstractElement = null; 
                    else
                      AbstractElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Abstract");
                }
            }
            
            /// <summary>
            /// Text to Display to the user
            /// </summary>
            [FhirElement("display", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Text to Display to the user
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if(value == null)
                      DisplayElement = null; 
                    else
                      DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// Formal Definition
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DefinitionElement;
            
            /// <summary>
            /// Formal Definition
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if(value == null)
                      DefinitionElement = null; 
                    else
                      DefinitionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// Child Concepts (is-a / contains)
            /// </summary>
            [FhirElement("concept", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> Concept
            {
                get { return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> _Concept;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValueSetDefineConceptComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirString)DefinitionElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent>(Concept.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValueSetDefineConceptComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValueSetDefineConceptComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValueSetDefineConceptComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ValueSetExpansionComponent")]
        [DataContract]
        public partial class ValueSetExpansionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Uniquely identifies this expansion
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Time valueset expansion happened
            /// </summary>
            [FhirElement("timestamp", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Instant TimestampElement
            {
                get { return _TimestampElement; }
                set { _TimestampElement = value; OnPropertyChanged("TimestampElement"); }
            }
            private Hl7.Fhir.Model.Instant _TimestampElement;
            
            /// <summary>
            /// Time valueset expansion happened
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? Timestamp
            {
                get { return TimestampElement != null ? TimestampElement.Value : null; }
                set
                {
                    if(value == null)
                      TimestampElement = null; 
                    else
                      TimestampElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("Timestamp");
                }
            }
            
            /// <summary>
            /// Codes in the value set
            /// </summary>
            [FhirElement("contains", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> Contains
            {
                get { return _Contains; }
                set { _Contains = value; OnPropertyChanged("Contains"); }
            }
            private List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> _Contains;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValueSetExpansionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(TimestampElement != null) dest.TimestampElement = (Hl7.Fhir.Model.Instant)TimestampElement.DeepCopy();
                    if(Contains != null) dest.Contains = new List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent>(Contains.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValueSetExpansionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValueSetExpansionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(TimestampElement, otherT.TimestampElement)) return false;
                if( !DeepComparable.Matches(Contains, otherT.Contains)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValueSetExpansionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(TimestampElement, otherT.TimestampElement)) return false;
                if( !DeepComparable.IsExactly(Contains, otherT.Contains)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this value set
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement
        {
            get { return _IdentifierElement; }
            set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
        }
        private Hl7.Fhir.Model.FhirString _IdentifierElement;
        
        /// <summary>
        /// Logical id to reference this value set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Identifier
        {
            get { return IdentifierElement != null ? IdentifierElement.Value : null; }
            set
            {
                if(value == null)
                  IdentifierElement = null; 
                else
                  IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Identifier");
            }
        }
        
        /// <summary>
        /// Logical id for this version of the value set
        /// </summary>
        [FhirElement("version", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical id for this version of the value set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Informal name for this value set
        /// </summary>
        [FhirElement("name", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name for this value set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom
        {
            get { return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        private List<Hl7.Fhir.Model.Contact> _Telecom;
        
        /// <summary>
        /// Human language description of the value set
        /// </summary>
        [FhirElement("description", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Human language description of the value set
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
        /// About the value set or its content
        /// </summary>
        [FhirElement("copyright", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        private Hl7.Fhir.Model.FhirString _CopyrightElement;
        
        /// <summary>
        /// About the value set or its content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if(value == null)
                  CopyrightElement = null; 
                else
                  CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ValueSet.ValueSetStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if(value == null)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Whether this is intended to be used with an extensible binding
        /// </summary>
        [FhirElement("extensible", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExtensibleElement
        {
            get { return _ExtensibleElement; }
            set { _ExtensibleElement = value; OnPropertyChanged("ExtensibleElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ExtensibleElement;
        
        /// <summary>
        /// Whether this is intended to be used with an extensible binding
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Extensible
        {
            get { return ExtensibleElement != null ? ExtensibleElement.Value : null; }
            set
            {
                if(value == null)
                  ExtensibleElement = null; 
                else
                  ExtensibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Extensible");
            }
        }
        
        /// <summary>
        /// Date for given status
        /// </summary>
        [FhirElement("date", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for given status
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// When value set defines its own codes
        /// </summary>
        [FhirElement("define", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.ValueSet.ValueSetDefineComponent Define
        {
            get { return _Define; }
            set { _Define = value; OnPropertyChanged("Define"); }
        }
        private Hl7.Fhir.Model.ValueSet.ValueSetDefineComponent _Define;
        
        /// <summary>
        /// When value set includes codes from elsewhere
        /// </summary>
        [FhirElement("compose", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent Compose
        {
            get { return _Compose; }
            set { _Compose = value; OnPropertyChanged("Compose"); }
        }
        private Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent _Compose;
        
        /// <summary>
        /// When value set is an expansion
        /// </summary>
        [FhirElement("expansion", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.ValueSet.ValueSetExpansionComponent Expansion
        {
            get { return _Expansion; }
            set { _Expansion = value; OnPropertyChanged("Expansion"); }
        }
        private Hl7.Fhir.Model.ValueSet.ValueSetExpansionComponent _Expansion;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ValueSet;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(ExtensibleElement != null) dest.ExtensibleElement = (Hl7.Fhir.Model.FhirBoolean)ExtensibleElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Define != null) dest.Define = (Hl7.Fhir.Model.ValueSet.ValueSetDefineComponent)Define.DeepCopy();
                if(Compose != null) dest.Compose = (Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent)Compose.DeepCopy();
                if(Expansion != null) dest.Expansion = (Hl7.Fhir.Model.ValueSet.ValueSetExpansionComponent)Expansion.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ValueSet());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ValueSet;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(ExtensibleElement, otherT.ExtensibleElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Define, otherT.Define)) return false;
            if( !DeepComparable.Matches(Compose, otherT.Compose)) return false;
            if( !DeepComparable.Matches(Expansion, otherT.Expansion)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ValueSet;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(ExtensibleElement, otherT.ExtensibleElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Define, otherT.Define)) return false;
            if( !DeepComparable.IsExactly(Compose, otherT.Compose)) return false;
            if( !DeepComparable.IsExactly(Expansion, otherT.Expansion)) return false;
            
            return true;
        }
        
    }
    
}
