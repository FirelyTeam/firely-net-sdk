#if !NET9_0_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices;

/// <summary>
/// Polyfill for <see cref="OverloadResolutionPriorityAttribute"/> on runtimes older than .NET 9.
/// </summary>
[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal sealed class OverloadResolutionPriorityAttribute(int priority) : Attribute
{
    /// <summary>
    /// The priority of the overload.
    /// </summary>
    public int Priority => priority;
}
#endif
