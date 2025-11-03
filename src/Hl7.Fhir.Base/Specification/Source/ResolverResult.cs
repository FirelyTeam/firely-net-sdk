using Hl7.Fhir.Model;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace Hl7.Fhir.Specification.Source;

/// <summary>
/// Choice type representing the current result of resolve operation
/// </summary>
public readonly record struct ResolverResult
{
    /// <summary>
    /// Whether the operation successfully retrieved a resource
    /// </summary>
    public bool Success => Value != null;
      
    /// <summary>
    /// Value retrieved from resource resolver
    /// </summary>
#if NET8_0_OR_GREATER
    [MemberNotNullWhen(true, nameof(Success))]
#endif
    public Resource? Value { get; }
    
    /// <summary>
    /// Error encountered while attempting retrieval of resource
    /// </summary>
#if NET8_0_OR_GREATER
    [MemberNotNullWhen(false, nameof(Success))]
#endif
    public ResolverException? Error { get; private init; }

    /// <summary>
    /// Constructor for successfully resolved resource
    /// </summary>
    /// <param name="value">Resolved resource</param>
    /// <exception cref="ArgumentNullException">Resource provided was null</exception>
#if NET8_0_OR_GREATER
    [SetsRequiredMembers]
#endif
    public ResolverResult(Resource value)
    {
        Value = value ?? throw Utility.Error.ArgumentNull(nameof(value));
        Error = null;
    }

    /// <summary>
    /// Constructor for failure to resolve, resulting in error
    /// </summary>
    /// <param name="error">Error encountered during resolve</param>
    #if NET8_0_OR_GREATER
    [SetsRequiredMembers]
    #endif
    public ResolverResult(ResolverException error)
    {
        Error = error;
        Value = null;
    }
    
    /// <summary>
    /// Constructor for when a resource was successfully resolved, but additional initialization logic failed
    /// </summary>
    /// <param name="value">Resolved resource</param>
    /// <param name="error">Error encountered during additional initialization</param>
    /// <exception cref="ArgumentNullException">Resource provided was null</exception>
#if NET8_0_OR_GREATER
    [SetsRequiredMembers]
#endif
    public ResolverResult(Resource value, ResolverException error) : this(value)
    {
        Error = error;
    }
    
    /// <summary>
    /// Implicit conversion of the <see cref="ResolverResult"/> to boolean.
    /// </summary>
    /// <param name="result">Instance of <see cref="ResolverResult"/></param>
    /// <returns><c>true</c> if choice type has a <see cref="Value"/>. otherwise <c>false</c>.</returns>
    public static implicit operator bool(ResolverResult result) => result.Success;

    /// <summary>
    /// Implicit conversion of a resource to <see cref="ResolverResult"/>
    /// </summary>
    /// <param name="value">Resolved resource</param>
    /// <returns><see cref="ResolverResult"/> representing the successfully retrieved <see cref="Resource"/>.</returns>
    public static implicit operator ResolverResult(Resource value) => new(value);
    /// <summary>
    /// Implicit conversion of an error to <see cref="ResolverResult"/>
    /// </summary>
    /// <param name="error">Error encountered during resolving of resource</param>
    /// <returns><see cref="ResolverResult"/> representing the provided error.</returns>
    public static implicit operator ResolverResult(ResolverException error) => new(error);
}