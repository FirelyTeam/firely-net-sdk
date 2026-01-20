/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Helper class providing common validation methods for terminology service operations.
/// </summary>
public static class TerminologyValidationHelpers
{
    /// <summary>
    /// Validates that exactly one of the provided code parameters is not null.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateExactlyOneCodeParameter(Code? code, Coding? coding, CodeableConcept? codeableConcept)
    {
        int count = 0;
        if (code != null) count += 1;
        if (coding != null) count += 1;
        if (codeableConcept != null) count += 1;

        if (count != 1)
        {
            throw FhirOperationException.InvalidOperationInvocation($"One (and only one) of 'code', 'coding' or 'codeableConcept' must be provided.");
        }
    }

    /// <summary>
    /// Validates that if a code is provided, either a system or inferSystem is provided.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateSystemForCode(Code? code, FhirUri? system, FhirBoolean? inferSystem = null)
    {
        if (code == null) return;

        if (system == null && inferSystem?.Value != true)
            throw FhirOperationException.InvalidOperationInvocation("If 'code' is provided, either 'system' must be provided, or 'inferSystem' must be true");
    }

    /// <summary>
    /// Validates that a coding parameter has both code and system populated.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateCoding(Coding? coding, [CallerArgumentExpression(nameof(coding))] string parameterName = "Coding")
    {
        if (coding == null) return;

        if (string.IsNullOrEmpty(coding.Code) || string.IsNullOrEmpty(coding.System))
            throw FhirOperationException.IncompleteCodedParameter($"Must have a {parameterName} with both code and system to be validated.");
    }

    /// <summary>
    /// Validates that a coding parameter has both code and system populated.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateConcept(CodeableConcept? codeableConcept)
    {
        if (codeableConcept == null) return;

        if (codeableConcept.Coding.Count == 0 && codeableConcept.Text is null)
            throw FhirOperationException.IncompleteCodedParameter("CodeableConcept must contain a coding or text to be validated.");
    }

    /// <summary>
    /// Validates that exactly one of url, valueSet, or context is provided.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateValueSetReference(FhirUri? url, Resource? valueSet, FhirUri? context)
    {
        int count = 0;
        if (url != null) count += 1;
        if (valueSet != null) count += 1;
        if (context != null) count += 1;

        if (count != 1)
            throw FhirOperationException.InvalidOperationInvocation("One (and only one) of 'url', 'valueSet' or 'context' must be provided.");
    }

    /// <summary>
    /// Validates that exactly one of url or valueSet is provided (for expand operation).
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateExpandValueSetReference(FhirUri? url, Resource? valueSet, FhirUri? context)
    {
        ValidateValueSetReference(url, valueSet, context);
    }

    /// <summary>
    /// Validates subsumes operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateSubsumesParameters(Code? codeA, Code? codeB, Coding? codingA, Coding? codingB, FhirUri? system, string? version)
    {
        // Validate exactly one code parameter type for A
        if (!HasExactlyOne(codeA, codingA))
            throw FhirOperationException.InvalidOperationInvocation("One (and only one) of 'codeA' or 'codingA' must be provided.");

        // Validate exactly one code parameter type for B
        if (!HasExactlyOne(codeB, codingB))
            throw FhirOperationException.InvalidOperationInvocation("One (and only one) of 'codeB' or 'codingB' must be provided.");

        // Validate system requirements for code parameters
        if ((codeA != null || codeB != null) && system == null)
            throw FhirOperationException.InvalidOperationInvocation("If 'codeA' or 'codeB' is provided, 'system' must be provided.");

        // Validate coding parameters have code and system
        ValidateCoding(codingA);
        ValidateCoding(codingB);
    }

    /// <summary>
    /// Validates lookup operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateLookupParameters(Code? code, Coding? coding, FhirUri? system)
    {
        // Validate exactly one code parameter type
        if (!HasExactlyOne(code, coding))
            throw FhirOperationException.InvalidOperationInvocation("One (and only one) of 'code' or 'coding' must be provided.");

        // Validate system requirement for code parameter
        if (code != null && system == null)
            throw FhirOperationException.InvalidOperationInvocation("If 'code' is provided, 'system' must be provided.");

        // Validate coding parameter has code and system
        ValidateCoding(coding);
    }

    /// <summary>
    /// Validates translate operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateTranslateParameters(Code? code, Coding? coding, CodeableConcept? codeableConcept, FhirUri? url, Resource? conceptMap, FhirUri? system)
    {
        // Validate exactly one code parameter type
        ValidateExactlyOneCodeParameter(code, coding, codeableConcept);

        // Validate exactly one concept map reference
        if (url == null && conceptMap == null)
            throw FhirOperationException.InvalidOperationInvocation("One of 'url' or 'conceptMap' must be provided.");

        if (url != null && conceptMap != null)
            throw FhirOperationException.InvalidOperationInvocation("Only one of 'url' or 'conceptMap' can be provided, not both.");

        // Validate system requirement for code parameter
        ValidateSystemForCode(code, system, null);
        
        // Validate codeableConcept parameter has coding or text
        ValidateConcept(codeableConcept);
    }

    /// <summary>
    /// Validates closure operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateClosureParameters(FhirString? name, IEnumerable<Coding>? concepts, FhirString? version)
    {
        // Name is required unless getting updates with version
        if (name == null && version == null)
            throw FhirOperationException.InvalidOperationInvocation("'name' must be provided.");

        // If concept list is provided, validate all codings
        if (concepts != null)
        {
            int index = 0;
            foreach (var coding in concepts)
            {
                ValidateCoding(coding, $"concept[{index}]");
                index++;
            }
        }
    }

    /// <summary>
    /// Validates expand operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateExpandParameters(FhirUri? url, Resource? valueSet, FhirUri? context, ContextDirection? contextDirection, Integer? offset, Integer? count)
    {
        // Validate value set reference
        ValidateExpandValueSetReference(url, valueSet, context);

        // Validate context direction requires context
        if (contextDirection.HasValue && context == null)
            throw FhirOperationException.InvalidOperationInvocation("'contextDirection' requires 'context' to be provided.");

        // Validate paging parameters are non-negative
        if (offset?.Value < 0)
            throw FhirOperationException.UnprocessableParameter("'offset' must be non-negative.");

        if (count?.Value < 0)
            throw FhirOperationException.UnprocessableParameter("'count' must be non-negative.");
    }

    /// <summary>
    /// Validates code system validate code operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateCodeSystemValidateCodeParameters(Code? code, Coding? coding, CodeableConcept? codeableConcept, FhirUri? system)
    {
        // Validate exactly one code parameter type
        ValidateExactlyOneCodeParameter(code, coding, codeableConcept);

        // Validate system requirement for code parameter
        ValidateSystemForCode(code, system);

        // Validate coding parameter has code and system
        ValidateCoding(coding);

        // Validate codeableConcept parameter has coding or text
        ValidateConcept(codeableConcept);
    }

    /// <summary>
    /// Validates code system validate code operation parameters according to FHIR specification.
    /// </summary>
    /// <exception cref="FhirOperationException">Thrown when validation fails.</exception>
    public static void ValidateValueSetValidateCodeParameters(Code? code, Coding? coding, CodeableConcept? codeableConcept, FhirUri? system, FhirBoolean? inferSystem)
    {
        // Validate exactly one code parameter type
        ValidateExactlyOneCodeParameter(code, coding, codeableConcept);

        // Validate system requirement for code parameter
        ValidateSystemForCode(code, system, inferSystem);

        // Validate coding parameter has code and system
        ValidateCoding(coding);
        
        // Validate codeableConcept parameter has coding or text
        ValidateConcept(codeableConcept);
    }

    /// <summary>
    /// Helper method to check if exactly one of the provided parameters is not null.
    /// </summary>
    private static bool HasExactlyOne(params object?[] parameters)
    {
        return parameters.Count(p => p != null) == 1;
    }
}