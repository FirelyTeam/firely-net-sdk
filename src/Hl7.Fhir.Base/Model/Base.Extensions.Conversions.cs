using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.Model;

#nullable enable

public static partial class BaseExtensions
{
    /// <summary>
    /// Converts a Poco to an ITypedElement.
    /// </summary>
    /// <param name="base">The Poco that should be converted to an <see cref="ITypedElement"/>.</param>
    /// <param name="modelInspector">The <see cref="ModelInspector"/> containing the POCO classes to be used for deserialization.</param>
    /// <param name="rootName"></param>
    /// <returns></returns>
    public static ITypedElement ToTypedElementLegacy(this Base @base, ModelInspector modelInspector, string? rootName = null)
        => new PocoElementNode(modelInspector, @base, rootName: rootName);
        

    /// <summary>
    /// Creates an adapter which implements ITypedElement on top of a POCO instance, with explicit version-specific metadata.
    /// </summary>
    /// <param name="base">The POCO instance</param>
    /// <param name="inspector">The ModelInspector instance supplying version-specific metadata for the instance</param>
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
    public static ITypedElement ToTypedElement(this Base @base, ModelInspector inspector, string? rootName = null) =>
        @base.ToPocoNode(inspector, rootName);

    /// <summary>
    /// Converts a Poco to a PocoNode.
    /// </summary>
    /// <param name="base">The Poco that should be converted to an <see cref="ITypedElement"/>.</param>
    /// <param name="inspector">An optional <see cref="ModelInspector"/> that should be used to access metadata about the resource.</param>
    /// <param name="rootName">An optional nome for the node at the root of the tree.</param>
    public static PocoNode ToPocoNode(this Base @base, ModelInspector? inspector = null, string? rootName = null)
    {
        var result = PocoNodeOrList.Root(@base, rootName);
        if(inspector is not null)
            ((IAnnotatable)result).AddAnnotation(inspector);

        return result;
    }
    
    public static ISourceNode ToSourceNode(this Base @base, ModelInspector inspector, string? rootName = null) =>
        @base.ToPocoNode(inspector, rootName);
}