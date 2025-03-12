using Hl7.Fhir.Model;

namespace Hl7.Fhir.Utility;

#nullable enable

internal static class OverflowNull<T> where T: new()
{
    public static readonly T INSTANCE = new();
    public static bool InOverflow(object? x) => ReferenceEquals(x, INSTANCE);
}