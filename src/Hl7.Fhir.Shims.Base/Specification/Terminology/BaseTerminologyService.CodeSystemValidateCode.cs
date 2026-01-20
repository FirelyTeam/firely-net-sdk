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
    protected virtual T.Task<ValidateCodeResult> ValidateCode(CodeSystem vs, Code? code, string? system, string? display, FhirBoolean? abstractAllowed) => throw new NotImplementedException();
    
    protected virtual async T.Task<ValidateCodeResult> CodeSystemValidateCode(CodeSystemValidateCodeParameters parameters)
    {
        var codeSystem = parameters.CodeSystem as CodeSystem
                       ?? await ResolveCodeSystem(new($"{parameters.Url!}|{parameters.CodeSystemVersion?.Value}")).ConfigureAwait(false)
                       ?? throw FhirOperationException.Unresolvable("Unable to resolve CodeSystem.");

        if (parameters.CodeableConcept is not null)
            return await validateConcept(codeSystem, parameters.CodeableConcept, parameters.Abstract).ConfigureAwait(false);
        
        if (parameters.Coding is not null)
            return await validateCoding(codeSystem, parameters.Coding, parameters.Abstract).ConfigureAwait(false);
        
        if (parameters.Code is not null)
            return await ValidateCode(codeSystem, parameters.Code, parameters.Url?.ToString(), parameters.Display?.ToString(), parameters.Abstract).ConfigureAwait(false);
         
        throw FhirOperationException.InvalidOperationInvocation("Unexpected parameters combination.");
    }
    
    private async T.Task<ValidateCodeResult> validateConcept(CodeSystem codeSystem, CodeableConcept codeableConcept, FhirBoolean? abstractAllowed)
    {
        // Maybe just a text, but if there are no codings, that's an error.
        if (!codeableConcept.Coding.Any())
            throw FhirOperationException.IncompleteCodedParameter("CodeableConcept contains no Codings to validate.");

        // If we have just 1 coding, we better handle this by immediately calling validateCoding.
        if (codeableConcept.Coding.Count == 1)
            return await validateCoding(codeSystem, codeableConcept.Coding.Single(), abstractAllowed).ConfigureAwait(false);

        // Else, look for one successful match in any of the codes in the CodeableConcept
        var callResults = await T.Task
            .WhenAll(codeableConcept.Coding.Select(coding => validateCoding(codeSystem, coding, abstractAllowed))).ConfigureAwait(false);

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
    
    private T.Task<ValidateCodeResult> validateCoding(CodeSystem codeSystem, Coding coding, FhirBoolean? abstractAllowed)
    {
        if(string.IsNullOrEmpty(coding.Code) || string.IsNullOrEmpty(coding.System))
            throw FhirOperationException.IncompleteCodedParameter("Must have a Coding/CodeableConcept with both code and system to be validated.");

        return ValidateCode(codeSystem, coding.CodeElement, coding.System, coding.Display, abstractAllowed);
    }

    async T.Task<Parameters> ICodeSystemTerminologyService.CodeSystemValidateCode(Parameters parameters, string? id, bool useGet)
    {
        try
        {
            var validParams = new CodeSystemValidateCodeParameters(parameters.NoDuplicates());
            TerminologyValidationHelpers.ValidateCodeSystemValidateCodeParameters(validParams.Code, validParams.Coding, validParams.CodeableConcept, validParams.Url);
            return await CodeSystemValidateCode(validParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }
}