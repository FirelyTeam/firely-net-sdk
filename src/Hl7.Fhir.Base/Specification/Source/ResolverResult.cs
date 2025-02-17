using Hl7.Fhir.Model;
using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.Specification.Source;

public readonly record struct ResolverResult
{
    public bool Success => Value != null;
        
#if NET8_0_OR_GREATER
    [MemberNotNullWhen(true, nameof(Success))]
#endif
    public Resource Value { get; private init; }
    
#if NET8_0_OR_GREATER
    [MemberNotNullWhen(false, nameof(Success))]
#endif
    public ResolverException Error { get; private init; }

#if NET8_0_OR_GREATER
    [SetsRequiredMembers]
#endif
    public ResolverResult(Resource value)
    {
        Value = value ?? throw Utility.Error.ArgumentNull(nameof(value));
        Error = null;
    }

    #if NET8_0_OR_GREATER
    [SetsRequiredMembers]
    #endif
    public ResolverResult(ResolverException error)
    {
        Error = error;
        Value = null;
    }
        
    public static implicit operator bool(ResolverResult result) => result.Success;

    public static implicit operator ResolverResult(Resource value) => new(value);
    public static implicit operator ResolverResult(ResolverException error) => new(error);
}