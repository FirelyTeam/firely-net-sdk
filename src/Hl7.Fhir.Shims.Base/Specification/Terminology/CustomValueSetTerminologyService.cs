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
using System.Linq;
using System.Net;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Base class for checking terminology of codes that are part of specific value sets (e.g., MIME types, languages).
/// </summary>
/// <remarks>
/// This class inherits from BaseTerminologyService to leverage parameter validation, dispatching, and exception handling.
/// Subclasses only need to implement the validation logic for their specific code type.
/// </remarks>
public abstract class CustomValueSetTerminologyService : BaseTerminologyService
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

    /// <summary>
    /// Resolve ValueSet - checks if the requested ValueSet is one that this service handles.
    /// </summary>
    protected override T.Task<ValueSet> ResolveValueSet(FhirUri canonical)
    {
        var canonicalUri = new Canonical(canonical.Value).Uri;
        
        // Check if this is one of the value sets we handle
        if (_codeValueSets.Any(vs => vs == canonicalUri))
        {
            // Return a minimal ValueSet - we don't need full expansion for simple validation
            return T.Task.FromResult(new ValueSet { Url = canonical.Value });
        }
        
        throw new FhirOperationException($"Cannot find valueset '{canonical.Value}'", HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Validate a single code using the custom validation logic.
    /// Automatically injects the system if not provided since we know which system we handle.
    /// </summary>
    protected override T.Task<ValidateCodeResult> ValidateCode(ValidateCodeParameters parameters)
    {
        // If code is provided but system is not, inject our system
        if (parameters.Code is not null && parameters.System is null)
        {
            parameters.System = new FhirUri(_codeSystem);
        }
        
        var code = parameters.Code;
        var system = parameters.System?.Value;
        
        // Extract system from URI if needed
        var systemUri = system != null ? new Canonical(system).Uri : null;
        
        // Check if system matches what we expect
        if (systemUri != _codeSystem && systemUri != null)
            throw new FhirOperationException($"Unknown system '{systemUri}'", HttpStatusCode.NotFound);

        if (code is null)
            return T.Task.FromResult(ValidateCodeResult.ForResult(false, "No code supplied."));

        // Call the abstract method that derived classes implement
        var success = ValidateCodeType(code.Value!);

        return T.Task.FromResult(success
            ? ValidateCodeResult.ForResult(true)
            : ValidateCodeResult.ForResult(false, $"'{code.Value}' is not a valid {_terminologyType}."));
    }

    /// <summary>
    /// Abstract method that derived classes must implement to validate if a code is valid for this terminology type.
    /// </summary>
    /// <param name="code">The code to validate</param>
    /// <returns>True if the code is valid, false otherwise</returns>
    protected abstract bool ValidateCodeType(string code);
}

