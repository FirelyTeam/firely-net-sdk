/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Introspection;

public abstract class FhirModelAttribute : Attribute
{
    public FhirRelease Since { get; set; } = (FhirRelease)int.MinValue;

    /// <summary>
    /// Determines whether the given attribute applies to a given FHIR release.
    /// </summary>
    /// <remarks>An attribute is applicable to a given <see cref="FhirRelease"/> if
    /// the attribute has a <see cref="FhirModelAttribute.Since"/> value that
    /// equivalent to or older than <paramref name="release"/> or has no <c>Since</c>
    /// value at all.</remarks>
    public bool AppliesToRelease(FhirRelease release) => Since <= release;
}

public abstract class ValidatingFhirModelAttribute : FhirModelAttribute
{
    public abstract IReadOnlyCollection<CodedValidationException> Validate(object? value, PocoValidationContext validationContext);

}