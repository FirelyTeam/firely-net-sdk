/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using System;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Abstract base class for terminology services. Provides methods for resolving
/// terminology-related resources such as CodeSystems and ValueSets, and overloads with a pre-validated Parameters based payload.
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
/// <item><description>Implementing simple single-value validators</description></item>
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
/// Derive from this class and override the protected virtual methods relevant to your implementation.
/// If a method necessary for determining a valid result of an operation is not overridden, a <see cref="NotImplementedException"/> will be thrown at runtime.
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
/// <item><description><b><see cref="ValidateCode(ValueSet, Code, string?, bool?, string?, FhirBoolean?)"/></b> - Core validation logic. Most services implement this.</description></item>
/// <item><description><b><see cref="ValidateCode(CodeSystem, Code, string?, string?, FhirBoolean?)"/></b> - Core validation logic. Most services implement this.</description></item>
/// <item><description><b><see cref="ResolveValueSet(Canonical)"/></b> - Required for ValueSet operations. Must return a ValueSet or throw.</description></item>
/// <item><description><b><see cref="ResolveCodeSystem(Canonical)"/></b> - Required for CodeSystem operations. Must return a CodeSystem or throw.</description></item>
/// <item><description><see cref="Expand(ExpandParameters)"/> - For ValueSet expansion support</description></item>
/// <item><description><see cref="BuildLookupResult(CodeSystem, CodeSystem.ConceptDefinitionComponent)"/> - For code system lookup operations</description></item>
/// <item><description><see cref="Translate(TranslateParameters)"/> - For concept map translations</description></item>
/// <item><description><see cref="Closure(ClosureParameters)"/> - For closure table maintenance (rarely needed)</description></item>
/// <item><description><see cref="Subsumes(SubsumesParameters)"/> - For subsumption testing</description></item>
/// </list>
/// </para>
/// 
/// <para><b>What the base class handles for you:</b></para>
/// <para>
/// <list type="bullet">
/// <item><description>✅ Parameter parsing and validation per FHIR specification</description></item>
/// <item><description>✅ ValueSet/CodeSystem resolution before calling ValidateCode</description></item>
/// <item><description>✅ CodeableConcept/Coding dispatching (ValidateCode is called per individual code)</description></item>
/// <item><description>✅ Exception wrapping (non-FhirOperationExceptions become InternalServerError)</description></item>
/// <item><description>✅ Aggregating results for CodeableConcept with multiple codings</description></item>
/// </list>
/// </para>
/// </remarks>
public abstract partial class BaseTerminologyService : ITerminologyService
{
    /// <summary>
    /// Resolves a CodeSystem by its canonical URL. This method is intended to be overridden
    /// in derived classes to provide the actual implementation for resolving CodeSystems.
    /// </summary>
    /// <param name="canonical">The canonical URL of the CodeSystem to resolve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result should contain the resolved <see cref="CodeSystem"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented in a derived class.
    /// </exception>
    protected virtual T.Task<CodeSystem?> ResolveCodeSystem(Canonical canonical) => throw new NotImplementedException();

    /// <summary>
    /// Resolves a ValueSet by its canonical URL.
    /// Derived classes should override this method to provide the actual implementation.
    /// </summary>
    /// <param name="canonical">The canonical URL of the ValueSet to resolve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result should contain the resolved <see cref="ValueSet"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented in a derived class.
    /// </exception>
    protected internal virtual T.Task<ValueSet?> ResolveValueSet(Canonical canonical) => throw new NotImplementedException();
}
