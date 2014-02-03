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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A set of codes drawn from one or more code systems
    /// </summary>
    [FhirType("ValueSet", IsResource=true)]
    [DataContract]
    public partial class ValueSet : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The lifecycle status of a Value Set or Concept Map
        /// </summary>
        [FhirEnumeration("ValueSetStatus")]
        public enum ValueSetStatus
        {
            [EnumLiteral("draft")]
            Draft, // This valueset is still under development.
            [EnumLiteral("active")]
            Active, // This valueset is ready for normal use.
            [EnumLiteral("retired")]
            Retired, // This valueset has been withdrawn or superceded and should no longer be used.
        }
        
        /// <summary>
        /// The kind of operation to perform as a part of a property based filter
        /// </summary>
        [FhirEnumeration("FilterOperator")]
        public enum FilterOperator
        {
            [EnumLiteral("=")]
            Equal, // The property value has the concept specified by the value.
            [EnumLiteral("is-a")]
            IsA, // The property value has a concept that has an is-a relationship with the value.
            [EnumLiteral("is-not-a")]
            IsNotA, // The property value has a concept that does not have an is-a relationship with the value.
            [EnumLiteral("regex")]
            Regex, // The property value representation matches the regex specified in the value.
            [EnumLiteral("in")]
            In, // The property value is in the set of codes or concepts identified by the value.
            [EnumLiteral("not in")]
            NotIn, // The property value is not in the set of codes or concepts identified by the value.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ValueSetDefineComponent")]
        [DataContract]
        public partial class ValueSetDefineComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// URI to identify the code system
            /// </summary>
            [FhirElement("system", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Version of this system
            /// </summary>
            [FhirElement("version", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// If code comparison is case sensitive
            /// </summary>
            [FhirElement("caseSensitive", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CaseSensitiveElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Concepts in the code system
            /// </summary>
            [FhirElement("concept", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> Concept { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ValueSetExpansionContainsComponent")]
        [DataContract]
        public partial class ValueSetExpansionContainsComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// System value for the code
            /// </summary>
            [FhirElement("system", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Code - if blank, this is not a choosable code
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// User display for the concept
            /// </summary>
            [FhirElement("display", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Codes contained in this concept
            /// </summary>
            [FhirElement("contains", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> Contains { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConceptSetComponent")]
        [DataContract]
        public partial class ConceptSetComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The system the codes come from
            /// </summary>
            [FhirElement("system", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            [FhirElement("version", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Code or concept from system
            /// </summary>
            [FhirElement("code", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Select codes/concepts by their properties (including relationships)
            /// </summary>
            [FhirElement("filter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ConceptSetFilterComponent> Filter { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConceptSetFilterComponent")]
        [DataContract]
        public partial class ConceptSetFilterComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// A property defined by the code system
            /// </summary>
            [FhirElement("property", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code PropertyElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// = | is-a | is-not-a | regex | in | not in
            /// </summary>
            [FhirElement("op", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ValueSet.FilterOperator> OpElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Code from the system, or regex criteria
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code ValueElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ValueSetComposeComponent")]
        [DataContract]
        public partial class ValueSetComposeComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Import the contents of another value set
            /// </summary>
            [FhirElement("import", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> ImportElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<System.Uri> Import
            {
                get { return ImportElement != null ? ImportElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ImportElement = null; 
                    else
                      ImportElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                }
            }
            
            /// <summary>
            /// Include one or more codes from a code system
            /// </summary>
            [FhirElement("include", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> Include { get; set; }
            
            /// <summary>
            /// Explicitly exclude codes
            /// </summary>
            [FhirElement("exclude", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ConceptSetComponent> Exclude { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ValueSetDefineConceptComponent")]
        [DataContract]
        public partial class ValueSetDefineConceptComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Code that identifies concept
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// If this code is not for use as a real concept
            /// </summary>
            [FhirElement("abstract", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AbstractElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Text to Display to the user
            /// </summary>
            [FhirElement("display", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Formal Definition
            /// </summary>
            [FhirElement("definition", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DefinitionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Child Concepts (is-a / contains)
            /// </summary>
            [FhirElement("concept", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> Concept { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ValueSetExpansionComponent")]
        [DataContract]
        public partial class ValueSetExpansionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Uniquely identifies this expansion
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier { get; set; }
            
            /// <summary>
            /// Time valueset expansion happened
            /// </summary>
            [FhirElement("timestamp", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Instant TimestampElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Codes in the value set
            /// </summary>
            [FhirElement("contains", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ValueSet.ValueSetExpansionContainsComponent> Contains { get; set; }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this value set
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Logical id for this version of the value set
        /// </summary>
        [FhirElement("version", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Informal name for this value set
        /// </summary>
        [FhirElement("name", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Human language description of the value set
        /// </summary>
        [FhirElement("description", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// About the value set or its content
        /// </summary>
        [FhirElement("copyright", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ValueSet.ValueSetStatus> StatusElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Whether this is intended to be used with an extensible binding
        /// </summary>
        [FhirElement("extensible", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExtensibleElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Date for given status
        /// </summary>
        [FhirElement("date", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// When value set defines its own codes
        /// </summary>
        [FhirElement("define", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.ValueSet.ValueSetDefineComponent Define { get; set; }
        
        /// <summary>
        /// When value set includes codes from elsewhere
        /// </summary>
        [FhirElement("compose", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent Compose { get; set; }
        
        /// <summary>
        /// When value set is an expansion
        /// </summary>
        [FhirElement("expansion", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.ValueSet.ValueSetExpansionComponent Expansion { get; set; }
        
    }
    
}
