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
using System;
using System.Linq;
using System.Net;
using System.Text;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

public partial class BaseTerminologyService
{
    protected virtual T.Task<ValidateCodeResult> ValidateCode(ValueSet vs, Code? code, string? system, string? display, FhirBoolean? abstractAllowed) => throw new NotImplementedException();
    
    private async T.Task<ValidateCodeResult> validateConcept(ValueSet vs, CodeableConcept codeableConcept, FhirBoolean? abstractAllowed)
    {
        // Maybe just a text, but if there are no codings, that's an error.
        if (!codeableConcept.Coding.Any())
            throw TerminologyServiceOperationExceptionExtensions.IncompleteCodedParameter("CodeableConcept contains no Codings to validate.");

        // If we have just 1 coding, we better handle this by immediately calling validateCoding.
        if (codeableConcept.Coding.Count == 1)
            return await validateCoding(vs, codeableConcept.Coding.Single(), abstractAllowed).ConfigureAwait(false);

        // Else, look for one successful match in any of the codes in the CodeableConcept
        var callResults = await T.Task
            .WhenAll(codeableConcept.Coding.Select(coding => validateCoding(vs, coding, abstractAllowed))).ConfigureAwait(false);

        if (callResults.FirstOrDefault(r => r.Result?.Value == true) is { } successResult)
            return successResult;

        // Return failure result.
        var messages = new StringBuilder();
        messages.AppendLine("None of the Codings in the CodeableConcept were valid. Details follow.");

        // gathering the messages of all calls
        foreach (var msg in callResults.Select(r => r.Message).Where(m => m is not null))
            messages.AppendLine(msg!.Value);

        return ValidateCodeResult.ForResult(false, messages.ToString());
    }
    
    private T.Task<ValidateCodeResult> validateCoding(ValueSet vs, Coding coding, FhirBoolean? abstractAllowed)
    {
        if(string.IsNullOrEmpty(coding.Code) || string.IsNullOrEmpty(coding.System))
            throw TerminologyServiceOperationExceptionExtensions.IncompleteCodedParameter("Must have a Coding/CodeableConcept with both code and system to be validated.");

        return ValidateCode(vs, coding.CodeElement, coding.System, coding.Display, abstractAllowed);
    }

    protected async virtual T.Task<ValidateCodeResult> ValueSetValidateCode(ValidateCodeParameters parameters)
    {
        // Context parameter is not supported
        if (parameters.Context != null)
            throw FhirOperationException.NotSupported("The 'context' parameter is not supported.");
        
        if (parameters.System == null)
            throw FhirOperationException.NotSupported("The 'inferSystem' parameter is not supported."); 
        
        var valueSet = parameters.ValueSet as ValueSet
                       ?? await ResolveValueSet(new($"{parameters.Url!}|{parameters.ValueSetVersion?.Value}")).ConfigureAwait(false)
                       ?? throw FhirOperationException.Unresolvable("Unable to resolve ValueSet.");
        
        ValidateCodeResult result;

        if (parameters.CodeableConcept is not null)
        {
            result = await validateConcept(valueSet, parameters.CodeableConcept, parameters.Abstract).ConfigureAwait(false);
        }
        else if (parameters.Coding is not null)
        {
            result = await validateCoding(valueSet, parameters.Coding, parameters.Abstract).ConfigureAwait(false);
        }
        else if (parameters.Code is not null)
        {
            result = await ValidateCode(valueSet, parameters.Code, parameters.System?.ToString(), parameters.Display?.ToString(), parameters.Abstract).ConfigureAwait(false);
        }
        else
        {
            throw FhirOperationException.InvalidOperationInvocation("Unexpected parameters combination.");
        }

        return result;
    }

    async T.Task<Parameters> ICodeValidationTerminologyService.ValueSetValidateCode(Parameters parameters, string? id, bool useGet)
    {
        try
        {
            var validCodeParams = new ValidateCodeParameters(parameters.NoDuplicates());
            TerminologyValidationHelpers.ValidateValueSetValidateCodeParameters(validCodeParams.Code, validCodeParams.Coding, validCodeParams.CodeableConcept, validCodeParams.System, validCodeParams.InferSystem);
            return await ValueSetValidateCode(validCodeParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }
}