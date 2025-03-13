using Hl7.Fhir.Model;
using System.Threading;

namespace Hl7.Fhir.Utility;

#nullable enable

internal static class OverflowNull<T> where T: new()
{
    public static readonly T INSTANCE = new();
}

internal static class OverflowNull
{
    public static bool InOverflow<T>(this object? value) where T : new() => ReferenceEquals(OverflowNull<T>.INSTANCE, value);
}