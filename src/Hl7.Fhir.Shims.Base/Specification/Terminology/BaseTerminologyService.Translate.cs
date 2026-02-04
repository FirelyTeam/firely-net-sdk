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
using System.Net;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

public partial class BaseTerminologyService
{
    async T.Task<Parameters> IMappingTerminologyService.Translate(Parameters parameters, string? id, bool useGet)
    {
        try
        {
            var validParams = new TranslateParameters(parameters.NoDuplicates());
            TerminologyValidationHelpers.ValidateTranslateParameters(validParams.Code, validParams.Coding, validParams.CodeableConcept, validParams.Url, validParams.ConceptMap, validParams.System);
            return await Translate(validParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }
    protected virtual T.Task<TranslateResult> Translate(TranslateParameters parameters) => throw new NotImplementedException();
}