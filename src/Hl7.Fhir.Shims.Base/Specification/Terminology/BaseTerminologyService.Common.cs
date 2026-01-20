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
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology;

/// <summary>
/// Abstract base class for terminology services. Provides methods for resolving
/// terminology-related resources such as CodeSystems and ValueSets.
/// </summary>
public abstract partial class BaseTerminologyService : ITerminologyService
{
    /// <summary>
    /// Resolves a CodeSystem by its canonical URL. This method is intended to be overridden
    /// in derived classes to provide the actual implementation for resolving CodeSystems.
    /// </summary>
    /// <param name="canonical">The canonical URL of the CodeSystem to resolve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the
    /// resolved <see cref="CodeSystem"/> or null if the CodeSystem could not be resolved.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented in a derived class.
    /// </exception>
    protected virtual T.Task<CodeSystem?> ResolveCodeSystem(Canonical canonical) => throw new NotImplementedException();

    /// <summary>
    /// Resolves a ValueSet by its canonical URL. This method MUST return a ValueSet or throw
    /// a <see cref="FhirOperationException"/> indicating that the ValueSet could not be found.
    /// Derived classes should override this method to provide the actual implementation.
    /// </summary>
    /// <param name="canonical">The canonical URL of the ValueSet to resolve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the
    /// resolved <see cref="ValueSet"/> or null if the ValueSet could not be resolved.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown if the method is not implemented in a derived class.
    /// </exception>
    protected internal virtual T.Task<ValueSet?> ResolveValueSet(Canonical canonical) => throw new NotImplementedException();
}
