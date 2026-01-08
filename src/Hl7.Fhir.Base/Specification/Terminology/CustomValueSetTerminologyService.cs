/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Base class for checking terminology of codes that are part of specific value sets (e.g., MIME types, languages).
/// </summary>
public abstract class CustomValueSetTerminologyService : ITerminologyService
{
    private readonly string _terminologyType;
    private readonly string _codeSystem;
    private readonly string[] _codeValueSets;

    /// <summary>
    /// Base class for checking terminology of codes that are part of a value set.
    /// </summary>
    /// <param name="terminologyType">String representation of the code type which is being checked. Exclusively used for error messages</param>
    /// <param name="codeSystem">Name of the specification defining the members of the value set</param>
    /// <param name="codeValueSets">uri's of the definitions of the code system. This can be multiple, if a FHIR version has changed this at some point.</param>
    protected CustomValueSetTerminologyService(string terminologyType, string codeSystem, string[] codeValueSets)
    {
        _terminologyType = terminologyType;
        _codeSystem = codeSystem;
        _codeValueSets = codeValueSets;
    }

    // Not supported operations - these throw NotImplementedException
    public Task<Resource> Closure(Parameters parameters, bool useGet = false) =>
        throw new NotImplementedException();

    public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false) =>
        throw new NotImplementedException();

    public Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false) =>
        throw new NotImplementedException();

    public Task<Parameters> Lookup(Parameters parameters, bool useGet = false) =>
        throw new NotImplementedException();

    public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false) =>
        throw new NotImplementedException();

    public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false) =>
        throw new NotImplementedException();

    /// <summary>
    /// Validate that a coded value is in the set of codes allowed by a value set.
    /// Only supports value sets that this service is configured to handle.
    /// </summary>
    public Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        var validCodeParams = new ValidateCodeParameters(parameters);
        var valueSetUri = validCodeParams.Url?.Value != null
            ? new Canonical(validCodeParams.Url?.Value).Uri
            : null;

        // Check if this is one of the value sets we handle
        if (_codeValueSets.All(valueSet => valueSet != valueSetUri))
        {
            throw new FhirOperationException($"Cannot find valueset '{validCodeParams.Url?.Value}'",
                HttpStatusCode.NotFound);
        }

        // Dispatch to appropriate validation method based on what's provided
        // (This logic is similar to BaseTerminologyService but must be duplicated due to assembly constraints)
        return validCodeParams switch
        {
            { CodeableConcept: not null } => validateCodeableConcept(validCodeParams.CodeableConcept),
            { Coding: not null } => validateCoding(validCodeParams.Coding),
            _ => validateCode(validCodeParams.Code?.Value, validCodeParams.System?.Value)
        };
    }

    private Task<Parameters> validateCoding(Coding coding) =>
        validateCode(coding.Code, coding.System);

    private async Task<Parameters> validateCodeableConcept(CodeableConcept cc)
    {
        // If there are no codings, that's a positive result (just text is allowed)
        if (!cc.Coding.Any())
            return createResult(true);

        // If we have just 1 coding, use the simpler version
        if (cc.Coding.Count == 1)
            return await validateCoding(cc.Coding.Single()).ConfigureAwait(false);

        // Multiple codings: look for one successful match
        var callResults = await Task.WhenAll(cc.Coding.Select(validateCoding)).ConfigureAwait(false);
        var anySuccessful = callResults.Any(p => p.GetSingleValue<FhirBoolean>("result")?.Value == true);

        if (!anySuccessful)
        {
            var messages = new StringBuilder();
            messages.AppendLine("None of the Codings in the CodeableConcept were valid for the binding. Details follow.");
            foreach (var msg in callResults.Select(cr => cr.GetSingleValue<FhirString>("message")?.Value).Where(m => m is not null))
                messages.AppendLine(msg);

            return createResult(false, messages.ToString());
        }

        return createResult(true);
    }

    private Task<Parameters> validateCode(string? code, string? system)
    {
        var systemUri = system != null ? new Canonical(system).Uri : null;

        // Check if system matches what we expect
        if (systemUri != _codeSystem && systemUri != null)
            throw new FhirOperationException($"Unknown system '{systemUri}'", HttpStatusCode.NotFound);

        if (code is null)
            return Task.FromResult(createResult(false, "No code supplied."));

        // Call the abstract method that derived classes implement
        var success = ValidateCodeType(code);

        return Task.FromResult(success
            ? createResult(true)
            : createResult(false, $"'{code}' is not a valid {_terminologyType}."));
    }

    private static Parameters createResult(bool success, string? message = null)
    {
        var result = new Parameters();
        result.Add("result", new FhirBoolean(success));
        if (!string.IsNullOrWhiteSpace(message))
            result.Add("message", new FhirString(message));
        return result;
    }

    /// <summary>
    /// Abstract method that derived classes must implement to validate if a code is valid for this terminology type.
    /// </summary>
    /// <param name="code">The code to validate</param>
    /// <returns>True if the code is valid, false otherwise</returns>
    protected abstract bool ValidateCodeType(string code);
}
