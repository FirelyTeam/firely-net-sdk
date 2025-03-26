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
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Introspection;

/// <summary>
/// This attribute is applied to classes that represent FHIR datatypes and resources.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
// Note that this attribute is a ValidationAttribute so that it can be used in the .NET validation mechanism.
// The only thing this attribute does, is delegate the validation to the FhirAttributeValidator.
public sealed class FhirTypeAttribute : ValidationAttribute
{
    public FhirTypeAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public FhirTypeAttribute(string name, string canonical)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Canonical = canonical;
    }

    /// <summary>
    /// The name of the FHIR type this class represents.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The canonical of the StructureDefinition defining this type.
    /// </summary>
    public string? Canonical { get; set; }

    /// <summary>
    /// Indicates whether this class represents the nested complex type for a (backbone) element.
    /// </summary>
    public bool IsBackboneType { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is null) return ValidationResult.Success;

        if(value is not Base objectValue) throw new ArgumentException("This attribute can only be applied to subclasses of Base.", nameof(value));
        if(validationContext.GetService(typeof(IPocoValidator)) is not IPocoValidator validator)
            throw new InvalidOperationException("The validation needs to have access to an IPocoValidator via the validation context's service collection..");
        if(validationContext.GetService(typeof(ModelInspector)) is not ModelInspector inspector)
            throw new InvalidOperationException("The validation needs to have access to a ModelInspector via the validation context's service collection..");
        if(validationContext.GetLocationProducer() is not {} parentLocationProducer)
            throw new InvalidOperationException("The validation context needs to have a location producer set.");

        var classMapping = inspector.FindClassMapping(objectValue.GetType());

        // Step 1: Validate the object properties.
        foreach (var (name,propValue) in objectValue.EnumerateElements())
        {
            string locationProducer() => $"{parentLocationProducer()}.{name}";
            var propMapping = classMapping?.FindMappedElementByName(name);

            var propValidationContext = new PropertyDeserializationContext(objectValue, locationProducer, name, null, null, propMapping, validationContext.GetNarrativeValidationKind());
            validator.ValidateProperty(propValue, propValidationContext, out var reportedErrors);
            if(reportedErrors.Any())
                return new CodedValidationResult(reportedErrors.First(), [name]);

            if (!validationContext.ValidateRecursively()) continue;

            return doNestedValidation(validationContext, name, propValue);
        }

        // Step 2: Validate the object
        if(classMapping is null) return ValidationResult.Success;

        var instanceValidationContext = new InstanceDeserializationContext(parentLocationProducer, null, null, classMapping, validationContext.GetNarrativeValidationKind());
        validator.ValidateInstance(objectValue, instanceValidationContext, out var reportedInstanceErrors);
        if(reportedInstanceErrors.Any())
            return new CodedValidationResult(reportedInstanceErrors.First());

        return ValidationResult.Success;
    }

    private ValidationResult? doNestedValidation(ValidationContext parentValidationContext, string name, object? propValue)
    {
        switch (propValue)
        {
            case IList list:
                {
                    foreach (var element in list)
                    {
                        if (element is not Base b) continue;
                        var nestedContext = parentValidationContext.IntoPath(b, name);
                        return IsValid(b, nestedContext);
                    }

                    break;
                }
            case Base b:
                {
                    var nestedContext = parentValidationContext.IntoPath(b, name);
                    return IsValid(b, nestedContext);
                }
        }

        return ValidationResult.Success;
    }
}