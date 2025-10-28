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

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Introspection;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class FhirElementAttribute : FhirModelAttribute
{
    public FhirElementAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public FhirElementAttribute(string name, ChoiceType choice, XmlRepresentation representation)
    {
        Name = name;
        Choice = choice;
        XmlSerialization = representation;
    }

    /// <summary>
    /// Whether this element allows instances of more than one type.
    /// </summary>
    public ChoiceType Choice { get; set; } = ChoiceType.None;

    /// <summary>
    /// The name of the element in FHIR this property represents.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The element represents the primitive `value` attribute/property in the FHIR serialization
    /// </summary>
    public bool IsPrimitiveValue { get; set; }

    /// <summary>
    /// How this value is represented in XML.
    /// </summary>
    public XmlRepresentation XmlSerialization { get; set; } = XmlRepresentation.None;

    public int Order { get; set; }

    /// <summary>
    /// The order of the element in the Xml representation.
    /// </summary>
    public bool InSummary { get; set; }

    /// <summary>
    /// If this modifies the meaning of other elements
    /// </summary>
    public bool IsModifier { get; set; }

    public string? FiveWs { get; set; }
}