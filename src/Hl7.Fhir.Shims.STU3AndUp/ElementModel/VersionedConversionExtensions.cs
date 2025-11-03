/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.ElementModel;

/// <summary>
/// A collection of all extension methods which require version-specific metadata included in the assembly they are compiled into.
/// </summary>
public static class VersionedConversionExtensions
{
    /// <summary>
    /// Creates an adapter which implements ITypedElement on top of a POCO instance, using the legacy ElementNode stack.
    /// </summary>
    /// <param name="base">The POCO instance</param>
    /// <param name="rootName">The name you wish to have at the root of the tree. This will determine e.g. the root element name for serialization.
    /// If none is given, the type of the underlying poco will be used.</param>
    public static ITypedElement ToTypedElementLegacy(this Base @base, string? rootName = null)
        => @base.ToTypedElementLegacy(ModelInfo.ModelInspector, rootName);

    /// <summary>
    /// Creates an adapter which implements ITypedElement on top of a POCO instance
    /// </summary>
    /// <param name="base">The POCO instance</param>
    /// <param name="rootName">The name you wish to have at the root of the tree. This will determine e.g. the root element name for serialization.
    /// If none is given, the type of the underlying poco will be used.</param>
    /// <remarks>The implementation of this method has changed. If you notice regressions, please let the SDK team know.
    /// In the meantime, you can restore the old behaviour with a call to <see cref="ToTypedElementLegacy"/></remarks>
#if NETSTANDARD2_1
        [Obsolete("The implementation of this method has changed to use our new model stack. If you want to try the new behaviour, "+
                  "either ignore this warning or call ToPocoNode(). For reverting to the old behaviour, call .ToTypedElementLegacy()")]
#else
    [Experimental("SDK0001")]
#endif
    public static ITypedElement ToTypedElement(this Base @base, string? rootName = null) =>
        @base.ToTypedElement(ModelInfo.ModelInspector, rootName);
    
    public static Base ToPoco(this ISourceNode source, Type? pocoType = null, PocoBuilderSettings? settings = null) =>
        source.ToPoco(ModelInfo.ModelInspector, pocoType, settings);

    public static T ToPoco<T>(this ISourceNode source, PocoBuilderSettings? settings = null) where T : Base
    {
        if (source is PocoNode { Poco: T {} poco })
            return poco;

        return (T)source.ToPoco(ModelInfo.ModelInspector, typeof(T), settings);
    }

    public static Base ToPoco(this ITypedElement element, Type? pocoType = null, PocoBuilderSettings? settings = null) =>
        element.ToPoco(ModelInfo.ModelInspector, pocoType, settings);

    public static T ToPoco<T>(this ITypedElement element, PocoBuilderSettings? settings = null) where T : Base
    {
        if (element is PocoNode { Poco: T {} poco })
            return poco;

        return (T)element.ToPoco(ModelInfo.ModelInspector, typeof(T), settings);
    }

    public static PocoNode ToPocoNode(this ITypedElement node) =>
        node.ToPocoNode(ModelInfo.ModelInspector, node.Name);
}