/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Validation;

/// <summary>
/// Extension methods on POCOs to invoke validation.
/// </summary>
public static class PocoValidationExtensions
{
    /// <summary>
    /// Validate an object and its members against any <see cref="ValidationAttribute" />s present.
    /// </summary>
    /// <param name="poco">The POCO to validate</param>
    /// <param name="narrativeValidation">The kind of narrative validation to perform when validating <see cref="XHtml"/>.</param>
    /// <param name="validator"></param>
    public static IReadOnlyCollection<CodedValidationException> Validate(
        this Base poco,
        NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml,
        IPocoValidator? validator = null) => poco.Validate(ModelInfo.ModelInspector, narrativeValidation, validator);
}