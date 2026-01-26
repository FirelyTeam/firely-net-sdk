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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

public partial class BaseTerminologyService
{
    protected virtual async T.Task<LookupResult> Lookup(LookupParameters parameters)
    {
        var code = parameters.Code?.Value ?? parameters.Coding?.Code;
        var system = parameters.System?.Value ?? parameters.Coding?.System;
        
        if (code is null || system is null)
            throw FhirOperationException.InvalidOperationInvocation("Insufficient information to perform code lookup.");
        
        var codeSystem = await ResolveCodeSystem(new($"{system}|{parameters.Version?.Value}"))
                         ?? throw FhirOperationException.Unresolvable($"The CodeSystem with url '{system}' could not be resolved.");

        var concept = recursiveConcepts(codeSystem.Concept).FirstOrDefault(x => x.Code == code);

        return BuildLookupResult(codeSystem, concept ?? throw FhirOperationException.CodeNotInSystem("Code not found in the specified code system."));
    }

    private IEnumerable<CodeSystem.ConceptDefinitionComponent> recursiveConcepts(List<CodeSystem.ConceptDefinitionComponent> concepts)
    {
        foreach (var concept in concepts)
        {
            yield return concept;
            foreach (var child in recursiveConcepts(concept.Concept))
                yield return child;
        }
    }
    
    async T.Task<Parameters> ICodeSystemTerminologyService.Lookup(Parameters parameters, bool useGet)
    {
        try
        {
            var validParams = new LookupParameters(parameters.NoDuplicates());
            TerminologyValidationHelpers.ValidateLookupParameters(validParams.Code, validParams.Coding, validParams.System);
            return await Lookup(validParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    protected virtual LookupResult BuildLookupResult(CodeSystem codeSystem, CodeSystem.ConceptDefinitionComponent concept) => throw new NotImplementedException();
}
