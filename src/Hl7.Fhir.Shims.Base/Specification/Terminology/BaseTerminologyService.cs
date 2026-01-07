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

public abstract class BaseTerminologyService : ITerminologyService
{
    T.Task<Parameters> ICodeValidationTerminologyService.ValueSetValidateCode(Parameters parameters, string? id, bool useGet)
    {
        var validCodeParams = new ValidateCodeParameters(parameters);
        return ValueSetValidateCode(validCodeParams).ContinueWith(t => (Parameters)t.Result);
    }

    protected async virtual T.Task<ValidateCodeResult> ValueSetValidateCode(ValidateCodeParameters parameters)
    {
        // For input params of https://build.fhir.org/valueset-operation-validate-code.html:
        // * (...) one of the in parameters url, context or valueSet must be provided.
        if(parameters.Context is not null)
            throw FhirOperationException.NotSupported("The 'context' parameter is not supported.");

        if(parameters.ValueSet is null && parameters.Url is null)
            throw FhirOperationException.InvalidOperationInvocation("'url' or 'valueset' must be provided.");

        var valueSet = (ValueSet?)parameters.ValueSet
                       ?? await ResolveValueSet(parameters.Url!).ConfigureAwait(false);

        // * One (and only one) of the in parameters code, coding, or codeableConcept must be provided.
        if(!exactlyOneCodeParam(parameters))
            throw FhirOperationException.InvalidOperationInvocation("One (and only one) of 'code', 'coding' or 'codeableConcept' must be provided.");

        // TODO: Cross ref with  src/Vonk.Plugins.Terminology/VonkTerminologyHost.cs, as this also has a similar
        // parameter validation code.

        static bool exactlyOneCodeParam(ValidateCodeParameters p)
        {
            int count = 0;
            if (p.Code is not null) count += 1;
            if (p.Coding is not null) count += 1;
            if (p.CodeableConcept is not null) count += 1;
            return count == 1;
        }

        // * If a code is provided, either a system or inferSystem SHOULD be provided.
        // (but we don't support inferSystem).
        if(parameters.InferSystem?.Value == true)
            throw FhirOperationException.NotSupported("The 'inferSystem' parameter is not supported.");

        if (parameters.Code is not null && parameters.System is null)
            throw FhirOperationException.InvalidOperationInvocation("If 'code' is provided, 'system' must be provided.");

        try
        {
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
                result = await ValidateCode(valueSet, parameters.Code, parameters.System?.Value, parameters.Display?.Value, parameters.Abstract).ConfigureAwait(false);
            }
            else
            {
                throw new InvalidOperationException("Unexpected parameters combination.");
            }

            return result;
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async T.Task<ValidateCodeResult> validateConcept(
        ValueSet vs,
        CodeableConcept codeableConcept,
        FhirBoolean? abstractAllowed)
    {
        // Maybe just a text, but if there are no codings, that's a positive result
        if (!codeableConcept.Coding.Any())
            throw FhirOperationException.IncompleteCodedParameter("CodeableConcept contains no Codings to validate.");

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
        foreach (var msg in callResults.Select(r=>r.Message).Where(m => m is not null))
            messages.AppendLine(msg!.Value);

        return ValidateCodeResult.ForResult(false, messages.ToString());
    }

    private T.Task<ValidateCodeResult> validateCoding(ValueSet vs, Coding coding, FhirBoolean? abstractAllowed)
    {
        if(string.IsNullOrEmpty(coding.Code) || string.IsNullOrEmpty(coding.System))
            throw FhirOperationException.IncompleteCodedParameter("Must have a Coding/CodeableConcept with both code and system to be validated.");

        return ValidateCode(vs, coding.CodeElement, coding.System, coding.Display, abstractAllowed);
    }


    protected virtual T.Task<ValidateCodeResult> ValidateCode(
        ValueSet vs,
        Code? code, string? system, string? display,
        FhirBoolean? abstractAllowed) => throw new NotImplementedException();

    /// <summary>
    /// Resolve a ValueSet by its canonical URL. This method MUST return a valueset, or else throw
    /// a FhirOperationException indicating the ValueSet could not be found using one of the
    /// methods in <see cref="TerminologyServiceOperationExceptionExtensions"/>.
    /// </summary>
    protected virtual T.Task<ValueSet> ResolveValueSet(FhirUri canonical) => throw new NotImplementedException();

    T.Task<Parameters> ICodeValidationTerminologyService.Subsumes(Parameters parameters, string? id, bool useGet)
    {
        var validParams = new SubsumesParameters(parameters.NoDuplicates());
        return Subsumes(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    protected virtual T.Task<SubsumesResult> Subsumes(SubsumesParameters parameters) => throw new NotImplementedException();

    T.Task<Parameters> ICodeSystemTerminologyService.CodeSystemValidateCode(Parameters parameters, string? id,
        bool useGet)
    {
        var validParams = new ValidateCodeParameters(parameters.NoDuplicates());
        return CodeSystemValidateCode(validParams).ContinueWith(t => (Parameters)t.Result);
    }


    public virtual T.Task<ValidateCodeResult> CodeSystemValidateCode(ValidateCodeParameters parameters) => throw new NotImplementedException();

    T.Task<Parameters> ICodeSystemTerminologyService.Lookup(Parameters parameters, bool useGet)
    {
        var validParams = new LookupParameters(parameters.NoDuplicates());
        return Lookup(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    public virtual T.Task<LookupResult> Lookup(LookupParameters parameters) =>
        throw new NotImplementedException();

    T.Task<Resource> IExpandingTerminologyService.Expand(Parameters parameters, string? id, bool useGet)
    {
        var validParams = new ExpandParameters(parameters.NoDuplicates());
        return Expand(validParams);
    }

    public virtual T.Task<Resource> Expand(ExpandParameters parameters) =>
        throw new NotImplementedException();

    T.Task<Parameters> IMappingTerminologyService.Translate(Parameters parameters, string? id, bool useGet)
    {
        var validParams = new TranslateParameters(parameters.NoDuplicates());
        return Translate(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    public virtual T.Task<TranslateResult> Translate(TranslateParameters parameters) =>
        throw new NotImplementedException();

    T.Task<Resource> ITerminologyServiceWithClosure.Closure(Parameters parameters, bool useGet)
    {
        var validParams = new ClosureParameters(parameters.NoDuplicates());
        return Closure(validParams);
    }

    public virtual T.Task<Resource> Closure(ClosureParameters parameters) =>
        throw new NotImplementedException();
}