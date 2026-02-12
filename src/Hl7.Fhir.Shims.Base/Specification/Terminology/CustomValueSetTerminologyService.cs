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
using Task=System.Threading.Tasks.Task;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Base class for checking Code terminology
/// </summary>
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
    /// Returns an uninitialized ValueSet, as the implementations will verify only codes known to them. If ValueSet is needed, override this method.
    /// </summary>
    /// <param name="canonical"></param>
    /// <returns></returns>
    protected internal override Task<ValueSet> ResolveValueSet(Canonical canonical) => Task.FromResult(new ValueSet());

    /// <summary>
    /// Returns an uninitialized CodeSystem, as the implementations will verify only codes known to them. If CodeSystem is needed, override this method.
    /// </summary>
    /// <param name="canonical"></param>
    /// <returns></returns>
    protected override Task<CodeSystem> ResolveCodeSystem(Canonical canonical) => Task.FromResult(new CodeSystem());


    protected override Task<ValidateCodeResult> ValueSetValidateCode(ValidateCodeParameters parameters)
    {
        var providedUrl = parameters.Url?.Value ?? (parameters.ValueSet as ValueSet)?.Url;
        var valueSetUri = parameters.Url?.Value != null
            ? new Canonical(providedUrl).Uri
            : null;

        if (_codeValueSets.All(valueSet => valueSet != valueSetUri))
            throw FhirOperationException.InvalidOperationInvocation($"Cannot find valueset '{providedUrl}'");
        
        return base.ValueSetValidateCode(parameters);
    }

    protected override Task<ValidateCodeResult> ValidateCode(ValueSet vs, Code code, string? system, bool? inferSystem, string? display, FhirBoolean? abstractAllowed)
    {
        if (system is null && inferSystem is not true)
            throw FhirOperationException.IncompleteCodedParameter("System is not supplied, and inferSystem is not set to true.");
        
        if (system is not null && system != _codeSystem)
            throw FhirOperationException.InvalidOperationInvocation($"This service only supports code system '{_codeSystem}'.");
        
        if (ValidateCodeType(code.Value!))
            return Task.FromResult(ValidateCodeResult.ForResult(true, code: code.Value, system: _codeSystem));
        
        return Task.FromResult(ValidateCodeResult.ForResult(false, $"'{code}' is not a valid {_terminologyType}."));
    }

    protected override Task<ValidateCodeResult> ValidateCode(CodeSystem cs, Code code, string? system, string? display, FhirBoolean? abstractAllowed)
    {
        if (system is null)
            throw FhirOperationException.IncompleteCodedParameter("System is not supplied.");
        
        if (system != _codeSystem)
            throw FhirOperationException.InvalidOperationInvocation($"This service only supports code system '{_codeSystem}'.");
        
        if (ValidateCodeType(code.Value!))
            return Task.FromResult(ValidateCodeResult.ForResult(true, code: code.Value, system: _codeSystem));
        
        return Task.FromResult(ValidateCodeResult.ForResult(false, $"'{code}' is not a valid {_terminologyType}."));
    }

    protected override Task<ValidateCodeResult> CodeSystemValidateCode(CodeSystemValidateCodeParameters parameters)
    {
        var providedUrl = parameters.Url?.Value ?? (parameters.CodeSystem as CodeSystem)?.Url;
        
        if (providedUrl is not null && providedUrl != _codeSystem)
            throw FhirOperationException.InvalidOperationInvocation($"This service only supports code system '{_codeSystem}'.");
        
        return base.CodeSystemValidateCode(parameters);
    }

    abstract protected bool ValidateCodeType(string code);
}