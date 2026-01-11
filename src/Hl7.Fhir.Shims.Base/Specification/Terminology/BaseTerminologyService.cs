/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

//
// IMPLEMENTER'S NOTE:
// If you're inheriting from BaseTerminologyService, scroll down to the 
// "Protected Virtual Members" region (around line 330) to see which methods
// to override. Read the class-level XML documentation below for patterns
// and examples of common implementations.
//

#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Linq;
using System.Net;
using System.Text;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Abstract base class for terminology services that perform local validation and expansion of FHIR terminology.
/// </summary>
/// <remarks>
/// <para>
/// This base class provides common infrastructure for terminology services that:
/// <list type="bullet">
/// <item><description>Perform actual validation logic locally (not delegating to other services)</description></item>
/// <item><description>Need to resolve and expand ValueSets</description></item>
/// <item><description>Implement FHIR specification parameter validation rules</description></item>
/// <item><description>Handle dispatching between code/Coding/CodeableConcept formats</description></item>
/// </list>
/// </para>
/// 
/// <para><b>When to use this base class:</b></para>
/// <para>
/// ✅ Use <see cref="BaseTerminologyService"/> when implementing a service that:
/// <list type="bullet">
/// <item><description>Validates codes against locally resolved/expanded ValueSets</description></item>
/// <item><description>Needs parameter validation per FHIR spec requirements</description></item>
/// <item><description>Benefits from type-safe parameter objects</description></item>
/// <item><description>Implements the full validation workflow</description></item>
/// </list>
/// Example: <c>LocalTerminologyService</c> - validates codes against locally expanded ValueSets
/// </para>
/// 
/// <para><b>When NOT to use this base class:</b></para>
/// <para>
/// ❌ Do NOT use <see cref="BaseTerminologyService"/> when:
/// <list type="bullet">
/// <item><description>Simply delegating/forwarding to another service (HTTP endpoint, wrapped service, etc.)</description></item>
/// <item><description>Implementing decorator patterns (caching, routing, fallback logic)</description></item>
/// <item><description>Need to preserve all parameters (id, useGet) that get discarded during conversion</description></item>
/// </list>
/// Examples: 
/// <list type="bullet">
/// <item><description><c>ExternalTerminologyService</c> - just forwards HTTP requests</description></item>
/// <item><description><c>CachingTerminologyService</c> - decorator that adds caching</description></item>
/// <item><description><c>MultiTerminologyService</c> - router that aggregates services</description></item>
/// </list>
/// For these cases, implement <see cref="ITerminologyService"/> directly.
/// </para>
/// 
/// <para><b>What this base class provides:</b></para>
/// <para>
/// <list type="number">
/// <item><description><b>Parameter Validation:</b> Validates FHIR spec requirements (url/valueSet provided, exactly one code parameter, etc.)</description></item>
/// <item><description><b>Type Conversion:</b> Converts raw Parameters to type-safe parameter objects (ValidateCodeParameters, etc.)</description></item>
/// <item><description><b>Dispatching Logic:</b> Routes CodeableConcept → multiple Codings → individual codes</description></item>
/// <item><description><b>ValueSet Resolution:</b> Calls ResolveValueSet() to obtain ValueSets for validation</description></item>
/// <item><description><b>Exception Handling:</b> Wraps exceptions in FhirOperationException</description></item>
/// </list>
/// </para>
/// 
/// <para><b>How to implement:</b></para>
/// <para>
/// Derive from this class and override the protected virtual methods in the "Protected Virtual Members" region.
/// All protected methods throw <see cref="NotImplementedException"/> by default - override only what you need.
/// </para>
/// 
/// <para><b>Common implementation patterns:</b></para>
/// <para>
/// <b>Pattern 1: ValueSet Validation Service</b> (e.g., LocalTerminologyService)
/// <code>
/// protected override async Task&lt;ValidateCodeResult&gt; ValidateCode(ValidateCodeParameters parameters)
/// {
///     // parameters.ValueSet is already resolved - use it directly
///     var valueSet = parameters.ValueSet!;
///     var code = parameters.Code?.Value;
///     // Implement validation logic against expanded ValueSet
/// }
/// 
/// protected override async Task&lt;ValueSet&gt; ResolveValueSet(FhirUri canonical)
/// {
///     // Resolve from your source (file system, database, etc.)
///     return await _resolver.FindValueSetAsync(canonical);
/// }
/// </code>
/// </para>
/// 
/// <para>
/// <b>Pattern 2: Simple Code Validator</b> (e.g., MimeTypeTerminologyService via CustomValueSetTerminologyService)
/// <code>
/// protected override Task&lt;ValidateCodeResult&gt; ValidateCode(ValidateCodeParameters parameters)
/// {
///     var code = parameters.Code?.Value;
///     bool isValid = MyValidator.IsValid(code);
///     return Task.FromResult(ValidateCodeResult.ForResult(isValid));
/// }
/// 
/// protected override Task&lt;ValueSet&gt; ResolveValueSet(FhirUri canonical)
/// {
///     // Return a minimal ValueSet for the canonical URL you handle
///     return Task.FromResult(new ValueSet { Url = canonical.Value });
/// }
/// </code>
/// </para>
/// 
/// <para><b>Methods to override (in order of importance):</b></para>
/// <para>
/// <list type="number">
/// <item><description><b><see cref="ValidateCode(ValidateCodeParameters)"/></b> - Core validation logic. Most services implement this.</description></item>
/// <item><description><b><see cref="ResolveValueSet(FhirUri)"/></b> - Required for ValueSet operations. Must return a ValueSet or throw.</description></item>
/// <item><description><see cref="Expand(ExpandParameters)"/> - For ValueSet expansion support</description></item>
/// <item><description><see cref="Lookup(LookupParameters)"/> - For code system lookup operations</description></item>
/// <item><description><see cref="Translate(TranslateParameters)"/> - For concept map translations</description></item>
/// <item><description><see cref="CodeSystemValidateCode(ValidateCodeParameters)"/> - For CodeSystem (not ValueSet) validation</description></item>
/// <item><description><see cref="Subsumes(SubsumesParameters)"/> - For subsumption testing</description></item>
/// <item><description><see cref="Closure(ClosureParameters)"/> - For closure table maintenance (rarely needed)</description></item>
/// </list>
/// </para>
/// 
/// <para><b>What the base class handles for you:</b></para>
/// <para>
/// <list type="bullet">
/// <item><description>✅ Parameter parsing and validation per FHIR specification</description></item>
/// <item><description>✅ ValueSet resolution before calling ValidateCode (parameters.ValueSet is never null)</description></item>
/// <item><description>✅ CodeableConcept/Coding dispatching (ValidateCode is called per individual code)</description></item>
/// <item><description>✅ Exception wrapping (non-FhirOperationExceptions become InternalServerError)</description></item>
/// <item><description>✅ Aggregating results for CodeableConcept with multiple codings</description></item>
/// </list>
/// </para>
/// 
/// <para><b>What you need to implement:</b></para>
/// <para>
/// <list type="bullet">
/// <item><description>❌ The actual validation/expansion/lookup logic</description></item>
/// <item><description>❌ ValueSet resolution from your terminology source</description></item>
/// <item><description>❌ Error handling specific to your source (throw FhirOperationException with appropriate status codes)</description></item>
/// </list>
/// </para>
/// </remarks>
public abstract class BaseTerminologyService : ITerminologyService
{
    #region ITerminologyService Implementation
    
    public T.Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        var validCodeParams = new ValidateCodeParameters(parameters);
        return valueSetValidateCodeImpl(validCodeParams).ContinueWith(t => (Parameters)t.Result);
    }

    public T.Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
    {
        var validParams = new SubsumesParameters(parameters.NoDuplicates());
        return subsumesImpl(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    public T.Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        var validParams = new ValidateCodeParameters(parameters.NoDuplicates());
        return codeSystemValidateCodeImpl(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    public T.Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
    {
        var validParams = new LookupParameters(parameters.NoDuplicates());
        return lookupImpl(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    public T.Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
    {
        var validParams = new ExpandParameters(parameters.NoDuplicates());
        return expandImpl(validParams);
    }

    public T.Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
    {
        var validParams = new TranslateParameters(parameters.NoDuplicates());
        return translateImpl(validParams).ContinueWith(t => (Parameters)t.Result);
    }

    public T.Task<Resource> Closure(Parameters parameters, bool useGet = false)
    {
        var validParams = new ClosureParameters(parameters.NoDuplicates());
        return closureImpl(validParams);
    }
    
    #endregion

    #region Private Implementation Methods

    private async T.Task<ValidateCodeResult> valueSetValidateCodeImpl(ValidateCodeParameters parameters)
    {
        // For input params of https://build.fhir.org/valueset-operation-validate-code.html:
        // * (...) one of the in parameters url, context or valueSet must be provided.
        // if(parameters.Context is not null)
        //     throw FhirOperationException.NotSupported("The 'context' parameter is not supported.");

        if(parameters.ValueSet is null && parameters.Url is null)
            throw FhirOperationException.InvalidOperationInvocation("'url' or 'valueset' must be provided.");

        // Resolve the ValueSet if needed
        if (parameters.ValueSet is null)
        {
            var resolved = await ResolveValueSet(parameters.Url!).ConfigureAwait(false);
            parameters = new ValidateCodeParameters(parameters) { ValueSet = resolved };
        }

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
                result = await validateConcept(parameters).ConfigureAwait(false);
            }
            else if (parameters.Coding is not null)
            {
                result = await validateCoding(parameters).ConfigureAwait(false);
            }
            else if (parameters.Code is not null)
            {
                result = await ValidateCode(parameters).ConfigureAwait(false);
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

    private async T.Task<ValidateCodeResult> validateConcept(ValidateCodeParameters parameters)
    {
        var codeableConcept = parameters.CodeableConcept!;
        
        // Maybe just a text, but if there are no codings, that's a positive result
        if (!codeableConcept.Coding.Any())
            throw FhirOperationException.IncompleteCodedParameter("CodeableConcept contains no Codings to validate.");

        // If we have just 1 coding, we better handle this by immediately calling validateCoding.
        if (codeableConcept.Coding.Count == 1)
        {
            var singleCodingParams = new ValidateCodeParameters(parameters)
            {
                Coding = codeableConcept.Coding.Single(),
                CodeableConcept = null
            };
            return await validateCoding(singleCodingParams).ConfigureAwait(false);
        }

        // Else, look for one successful match in any of the codes in the CodeableConcept
        var tasks = codeableConcept.Coding.Select(coding =>
        {
            var codingParams = new ValidateCodeParameters(parameters)
            {
                Coding = coding,
                CodeableConcept = null
            };
            return validateCoding(codingParams);
        });
        
        var callResults = await T.Task.WhenAll(tasks).ConfigureAwait(false);

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

    private T.Task<ValidateCodeResult> validateCoding(ValidateCodeParameters parameters)
    {
        var coding = parameters.Coding!;
        
        if(string.IsNullOrEmpty(coding.Code) || string.IsNullOrEmpty(coding.System))
            throw FhirOperationException.IncompleteCodedParameter("Must have a Coding/CodeableConcept with both code and system to be validated.");

        return ValidateCode(parameters);
    }

    private async T.Task<SubsumesResult> subsumesImpl(SubsumesParameters parameters)
    {
        // Add any parameter validation here if needed
        try
        {
            return await Subsumes(parameters).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async T.Task<ValidateCodeResult> codeSystemValidateCodeImpl(ValidateCodeParameters parameters)
    {
        // Add any parameter validation here if needed
        try
        {
            return await CodeSystemValidateCode(parameters).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async T.Task<LookupResult> lookupImpl(LookupParameters parameters)
    {
        // Add any parameter validation here if needed
        try
        {
            return await Lookup(parameters).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async T.Task<Resource> expandImpl(ExpandParameters parameters)
    {
        // Add any parameter validation here if needed
        try
        {
            return await Expand(parameters).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async T.Task<TranslateResult> translateImpl(TranslateParameters parameters)
    {
        // Add any parameter validation here if needed
        try
        {
            return await Translate(parameters).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async T.Task<Resource> closureImpl(ClosureParameters parameters)
    {
        // Add any parameter validation here if needed
        try
        {
            return await Closure(parameters).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }
    
    #endregion

    #region Protected Virtual Members - Override these to implement terminology operations
    
    // ============================================================================
    // IMPLEMENTER'S GUIDE
    // ============================================================================
    // 
    // This region contains the methods you should override to implement your
    // terminology service. All methods throw NotImplementedException by default.
    //
    // QUICK START:
    // 1. Override ValidateCode() - the core validation method
    // 2. Override ResolveValueSet() - to provide ValueSets from your source
    // 3. Optionally override Expand(), Lookup(), Translate(), etc.
    //
    // IMPORTANT GUARANTEES FROM BASE CLASS:
    // - ValidateCode() is called AFTER parameter validation is complete
    // - The ValueSet in parameters.ValueSet is ALWAYS resolved (never null)
    // - For CodeableConcept, ValidateCode() is called PER CODING, not once per CodeableConcept
    // - All exceptions (except FhirOperationException) are wrapped automatically
    //
    // SEE CLASS-LEVEL DOCUMENTATION for implementation patterns and examples.
    // ============================================================================
    
    /// <summary>
    /// Validates a code, coding, or codeable concept against a ValueSet.
    /// </summary>
    /// <param name="parameters">
    /// The validation parameters containing:
    /// <list type="bullet">
    /// <item><description><see cref="ValidateCodeParameters.ValueSet"/> - The resolved ValueSet to validate against (never null when this method is called)</description></item>
    /// <item><description><see cref="ValidateCodeParameters.Code"/> - The code to validate (when validating a single code)</description></item>
    /// <item><description><see cref="ValidateCodeParameters.System"/> - The code system URI</description></item>
    /// <item><description><see cref="ValidateCodeParameters.Display"/> - The display text for the code</description></item>
    /// <item><description><see cref="ValidateCodeParameters.Coding"/> - The Coding to validate (alternative to Code)</description></item>
    /// <item><description><see cref="ValidateCodeParameters.CodeableConcept"/> - The CodeableConcept to validate (alternative to Code/Coding)</description></item>
    /// <item><description><see cref="ValidateCodeParameters.Abstract"/> - Whether abstract codes are allowed</description></item>
    /// </list>
    /// </param>
    /// <returns>A ValidateCodeResult indicating whether the code is valid and any associated messages</returns>
    /// <remarks>
    /// <para>When this method is called:</para>
    /// <list type="bullet">
    /// <item><description>The ValueSet has already been resolved and is available in parameters.ValueSet</description></item>
    /// <item><description>All FHIR spec parameter validations have been performed</description></item>
    /// <item><description>For CodeableConcept validation, this method is called for individual codings, not the entire CodeableConcept</description></item>
    /// <item><description>Exactly one of Code, Coding, or CodeableConcept will be non-null (validated by base class)</description></item>
    /// </list>
    /// </remarks>
    protected virtual T.Task<ValidateCodeResult> ValidateCode(ValidateCodeParameters parameters) => 
        throw new NotImplementedException();

    /// <summary>
    /// Resolves a ValueSet by its canonical URL.
    /// </summary>
    /// <param name="canonical">The canonical URL of the ValueSet to resolve</param>
    /// <returns>The resolved ValueSet</returns>
    /// <exception cref="FhirOperationException">
    /// Thrown when the ValueSet cannot be found. Use methods from 
    /// <see cref="TerminologyServiceOperationExceptionExtensions"/> to create appropriate exceptions.
    /// </exception>
    /// <remarks>
    /// This method MUST return a ValueSet or throw a FhirOperationException. It should never return null.
    /// </remarks>
    protected virtual T.Task<ValueSet> ResolveValueSet(FhirUri canonical) => 
        throw new NotImplementedException();

    /// <summary>
    /// Tests the subsumption relationship between two codes.
    /// </summary>
    /// <param name="parameters">
    /// The subsumption parameters containing:
    /// <list type="bullet">
    /// <item><description><see cref="SubsumesParameters.CodeA"/> / <see cref="SubsumesParameters.CodingA"/> - The first code</description></item>
    /// <item><description><see cref="SubsumesParameters.CodeB"/> / <see cref="SubsumesParameters.CodingB"/> - The second code</description></item>
    /// <item><description><see cref="SubsumesParameters.System"/> - The code system to use for subsumption testing</description></item>
    /// </list>
    /// </param>
    /// <returns>A SubsumesResult indicating the subsumption relationship</returns>
    protected virtual T.Task<SubsumesResult> Subsumes(SubsumesParameters parameters) => 
        throw new NotImplementedException();

    /// <summary>
    /// Validates a code against a CodeSystem (not a ValueSet).
    /// </summary>
    /// <param name="parameters">The validation parameters (similar structure to ValueSet validation)</param>
    /// <returns>A ValidateCodeResult indicating whether the code exists in the CodeSystem</returns>
    protected virtual T.Task<ValidateCodeResult> CodeSystemValidateCode(ValidateCodeParameters parameters) => 
        throw new NotImplementedException();

    /// <summary>
    /// Looks up details about a code, including definitions, designations, and properties.
    /// </summary>
    /// <param name="parameters">
    /// The lookup parameters containing:
    /// <list type="bullet">
    /// <item><description><see cref="LookupParameters.Code"/> / <see cref="LookupParameters.Coding"/> - The code to look up</description></item>
    /// <item><description><see cref="LookupParameters.System"/> - The code system</description></item>
    /// <item><description><see cref="LookupParameters.Version"/> - The code system version</description></item>
    /// <item><description><see cref="LookupParameters.DisplayLanguage"/> - Preferred language for display</description></item>
    /// </list>
    /// </param>
    /// <returns>A LookupResult with the code details</returns>
    protected virtual T.Task<LookupResult> Lookup(LookupParameters parameters) =>
        throw new NotImplementedException();

    /// <summary>
    /// Expands a ValueSet to a collection of codes.
    /// </summary>
    /// <param name="parameters">
    /// The expansion parameters containing:
    /// <list type="bullet">
    /// <item><description><see cref="ExpandParameters.Url"/> / <see cref="ExpandParameters.ValueSet"/> - The ValueSet to expand</description></item>
    /// <item><description><see cref="ExpandParameters.Filter"/> - Text filter for the expansion</description></item>
    /// <item><description><see cref="ExpandParameters.Count"/> / <see cref="ExpandParameters.Offset"/> - Paging parameters</description></item>
    /// <item><description><see cref="ExpandParameters.IncludeDesignations"/> - Whether to include designations</description></item>
    /// </list>
    /// </param>
    /// <returns>An expanded ValueSet</returns>
    protected virtual T.Task<Resource> Expand(ExpandParameters parameters) =>
        throw new NotImplementedException();

    /// <summary>
    /// Translates a code from one value set to another using concept maps.
    /// </summary>
    /// <param name="parameters">
    /// The translation parameters containing:
    /// <list type="bullet">
    /// <item><description><see cref="TranslateParameters.Code"/> / <see cref="TranslateParameters.Coding"/> / <see cref="TranslateParameters.CodeableConcept"/> - The code to translate</description></item>
    /// <item><description><see cref="TranslateParameters.Source"/> - The source value set</description></item>
    /// <item><description><see cref="TranslateParameters.Target"/> - The target value set</description></item>
    /// <item><description><see cref="TranslateParameters.Url"/> - The ConceptMap to use</description></item>
    /// </list>
    /// </param>
    /// <returns>A TranslateResult with the translation matches</returns>
    protected virtual T.Task<TranslateResult> Translate(TranslateParameters parameters) =>
        throw new NotImplementedException();

    /// <summary>
    /// Maintains a client-side transitive closure table.
    /// </summary>
    /// <param name="parameters">
    /// The closure parameters containing:
    /// <list type="bullet">
    /// <item><description><see cref="ClosureParameters.Name"/> - The name of the closure table</description></item>
    /// <item><description><see cref="ClosureParameters.Concept"/> - Concepts to add to the closure</description></item>
    /// </list>
    /// </param>
    /// <returns>A ConceptMap with new closure table entries</returns>
    protected virtual T.Task<Resource> Closure(ClosureParameters parameters) =>
        throw new NotImplementedException();
    #endregion
}

