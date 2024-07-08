// <auto-generated/>
// Contents of: hl7.fhir.r5.expansions#5.0.0, hl7.fhir.r5.core#5.0.0

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

namespace Hl7.Fhir.Model
{
  /// <summary>
  /// Indication of the degree of conformance expectations associated with a binding.
  /// (url: http://hl7.org/fhir/ValueSet/binding-strength)
  /// (system: http://hl7.org/fhir/binding-strength)
  /// </summary>
  [FhirEnumeration("BindingStrength", "http://hl7.org/fhir/ValueSet/binding-strength", "http://hl7.org/fhir/binding-strength")]
  public enum BindingStrength
  {
    /// <summary>
    /// To be conformant, the concept in this element SHALL be from the specified value set.
    /// (system: http://hl7.org/fhir/binding-strength)
    /// </summary>
    [EnumLiteral("required"), Description("Required")]
    Required,
    /// <summary>
    /// To be conformant, the concept in this element SHALL be from the specified value set if any of the codes within the value set can apply to the concept being communicated.  If the value set does not cover the concept (based on human review), alternate codings (or, data type allowing, text) may be included instead.
    /// (system: http://hl7.org/fhir/binding-strength)
    /// </summary>
    [EnumLiteral("extensible"), Description("Extensible")]
    Extensible,
    /// <summary>
    /// Instances are encouraged to draw from the specified codes for interoperability purposes but are not required to do so to be considered conformant.
    /// (system: http://hl7.org/fhir/binding-strength)
    /// </summary>
    [EnumLiteral("preferred"), Description("Preferred")]
    Preferred,
    /// <summary>
    /// Instances are not expected or even encouraged to draw from the specified value set.  The value set merely provides examples of the types of concepts intended to be included.
    /// (system: http://hl7.org/fhir/binding-strength)
    /// </summary>
    [EnumLiteral("example"), Description("Example")]
    Example,
  }

  /// <summary>
  /// How a capability statement is intended to be used.
  /// (url: http://hl7.org/fhir/ValueSet/capability-statement-kind)
  /// (system: http://hl7.org/fhir/capability-statement-kind)
  /// </summary>
  [FhirEnumeration("CapabilityStatementKind", "http://hl7.org/fhir/ValueSet/capability-statement-kind", "http://hl7.org/fhir/capability-statement-kind")]
  public enum CapabilityStatementKind
  {
    /// <summary>
    /// The CapabilityStatement instance represents the present capabilities of a specific system instance.  This is the kind returned by /metadata for a FHIR server end-point.
    /// (system: http://hl7.org/fhir/capability-statement-kind)
    /// </summary>
    [EnumLiteral("instance"), Description("Instance")]
    Instance,
    /// <summary>
    /// The CapabilityStatement instance represents the capabilities of a system or piece of software, independent of a particular installation.
    /// (system: http://hl7.org/fhir/capability-statement-kind)
    /// </summary>
    [EnumLiteral("capability"), Description("Capability")]
    Capability,
    /// <summary>
    /// The CapabilityStatement instance represents a set of requirements for other systems to meet; e.g. as part of an implementation guide or 'request for proposal'.
    /// (system: http://hl7.org/fhir/capability-statement-kind)
    /// </summary>
    [EnumLiteral("requirements"), Description("Requirements")]
    Requirements,
  }

  /// <summary>
  /// The extent of the content of the code system (the concepts and codes it defines) are represented in a code system resource.
  /// (url: http://hl7.org/fhir/ValueSet/codesystem-content-mode)
  /// (system: http://hl7.org/fhir/codesystem-content-mode)
  /// </summary>
  [FhirEnumeration("CodeSystemContentMode", "http://hl7.org/fhir/ValueSet/codesystem-content-mode", "http://hl7.org/fhir/codesystem-content-mode")]
  public enum CodeSystemContentMode
  {
    /// <summary>
    /// None of the concepts defined by the code system are included in the code system resource.
    /// (system: http://hl7.org/fhir/codesystem-content-mode)
    /// </summary>
    [EnumLiteral("not-present"), Description("Not Present")]
    NotPresent,
    /// <summary>
    /// A subset of the valid externally defined concepts are included in the code system resource. There is no specific purpose or documented intent other than for illustrative purposes.
    /// (system: http://hl7.org/fhir/codesystem-content-mode)
    /// </summary>
    [EnumLiteral("example"), Description("Example")]
    Example,
    /// <summary>
    /// A subset of the code system concepts are included in the code system resource. This is a curated subset released for a specific purpose under the governance of the code system steward, and that the intent, bounds and consequences of the fragmentation are clearly defined in the fragment or the code system documentation. Fragments are also known as partitions.
    /// (system: http://hl7.org/fhir/codesystem-content-mode)
    /// </summary>
    [EnumLiteral("fragment"), Description("Fragment")]
    Fragment,
    /// <summary>
    /// All the concepts defined by the code system are included in the code system resource.
    /// (system: http://hl7.org/fhir/codesystem-content-mode)
    /// </summary>
    [EnumLiteral("complete"), Description("Complete")]
    Complete,
    /// <summary>
    /// The resource doesn't define any new concepts; it just provides additional designations and properties to another code system.
    /// (system: http://hl7.org/fhir/codesystem-content-mode)
    /// </summary>
    [EnumLiteral("supplement"), Description("Supplement")]
    Supplement,
  }

  /// <summary>
  /// SHALL applications comply with this constraint?
  /// (url: http://hl7.org/fhir/ValueSet/constraint-severity)
  /// (system: http://hl7.org/fhir/constraint-severity)
  /// </summary>
  [FhirEnumeration("ConstraintSeverity", "http://hl7.org/fhir/ValueSet/constraint-severity", "http://hl7.org/fhir/constraint-severity")]
  public enum ConstraintSeverity
  {
    /// <summary>
    /// If the constraint is violated, the resource is not conformant.
    /// (system: http://hl7.org/fhir/constraint-severity)
    /// </summary>
    [EnumLiteral("error"), Description("Error")]
    Error,
    /// <summary>
    /// If the constraint is violated, the resource is conformant, but it is not necessarily following best practice.
    /// (system: http://hl7.org/fhir/constraint-severity)
    /// </summary>
    [EnumLiteral("warning"), Description("Warning")]
    Warning,
  }

  /// <summary>
  /// Data types allowed to be used for search parameters.
  /// (url: http://hl7.org/fhir/ValueSet/search-param-type)
  /// (system: http://hl7.org/fhir/search-param-type)
  /// </summary>
  [FhirEnumeration("SearchParamType", "http://hl7.org/fhir/ValueSet/search-param-type", "http://hl7.org/fhir/search-param-type")]
  public enum SearchParamType
  {
    /// <summary>
    /// Search parameter SHALL be a number (a whole number, or a decimal).
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("number"), Description("Number")]
    Number,
    /// <summary>
    /// Search parameter is on a date/time. The date format is the standard XML format, though other formats may be supported.
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("date"), Description("Date/DateTime")]
    Date,
    /// <summary>
    /// Search parameter is a simple string, like a name part. Search is case-insensitive and accent-insensitive. May match just the start of a string. String parameters may contain spaces.
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("string"), Description("String")]
    String,
    /// <summary>
    /// Search parameter on a coded element or identifier. May be used to search through the text, display, code and code/codesystem (for codes) and label, system and key (for identifier). Its value is either a string or a pair of namespace and value, separated by a "|", depending on the modifier used.
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("token"), Description("Token")]
    Token,
    /// <summary>
    /// A reference to another resource (Reference or canonical).
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("reference"), Description("Reference")]
    Reference,
    /// <summary>
    /// A composite search parameter that combines a search on two values together.
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("composite"), Description("Composite")]
    Composite,
    /// <summary>
    /// A search parameter that searches on a quantity.
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("quantity"), Description("Quantity")]
    Quantity,
    /// <summary>
    /// A search parameter that searches on a URI (RFC 3986).
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("uri"), Description("URI")]
    Uri,
    /// <summary>
    /// Special logic applies to this parameter per the description of the search parameter.
    /// (system: http://hl7.org/fhir/search-param-type)
    /// </summary>
    [EnumLiteral("special"), Description("Special")]
    Special,
  }

}

