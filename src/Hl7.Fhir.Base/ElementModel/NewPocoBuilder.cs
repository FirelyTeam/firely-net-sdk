/*
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.ElementModel;

/// <summary>
/// Traverses an <see cref="ITypedElement"/> or <see cref="ISourceNode"/> tree and constructs a POCO from it.
/// </summary>
/// <param name="inspector">The inspector providing the necessary metadata about the FHIR POCO classes used in the construction.</param>
/// <param name="settings">Configuration for building the POCO.</param>
internal partial class NewPocoBuilder(ModelInspector inspector, PocoBuilderSettings? settings = null)
{
    /// <summary>
    /// Build a POCO from an <see cref="ITypedElement"/>.
    /// </summary>
    public Base BuildFrom(ITypedElement source, Type? typeHint = null)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        // The source may be a SourceNode that was bound to a provider which could not resolve its type
        // information (e.g. typed using ModelInspector.Base, which does not contain the resources of a
        // specific FHIR release). In that case none of the values have been parsed: they would be passed
        // through as raw strings and end up unparsed inside the POCOs built here. Since this builder does
        // have the full metadata available, rebind the underlying source node to our inspector, so the
        // data is correctly typed and parsed after all.
        if (source is TypedElementOnSourceNode { Definition: null } unresolved && !ReferenceEquals(unresolved.Provider, inspector))
            source = unresolved.ReTypeWith(inspector, unresolved.InstanceType ?? rootTypeFromHint());

        var classMapping = classMappingForElement(source, null, typeHint);
        return readFromElement(source, classMapping);

        // Derive the name of the root type from the type hint, but only if it maps to a
        // concrete type, since the root of a typed tree cannot be abstract.
        string? rootTypeFromHint() =>
            typeHint is not null && inspector.FindClassMapping(typeHint) is { NativeType.IsAbstract: false } hintMapping
                ? hintMapping.Name
                : null;
    }

    /// <summary>
    /// Build a POCO from an <see cref="ISourceNode"/>.
    /// </summary>
    public Base BuildFrom(ISourceNode source, Type? typeHint = null)
    {
        switch (source)
        {
            case null:
                throw Error.ArgumentNull(nameof(source));
            case PocoNode { Poco: { } poco } when
                (typeHint is null || typeHint.IsInstanceOfType(poco)):
                return poco;
        }

        if (typeHint is null &&
            source.Annotation<IResourceTypeSupplier>()?.ResourceType is null &&
            inspector.FindClassMapping(source.Name) is { } rootMapping &&
            typeof(Base).IsAssignableFrom(rootMapping.NativeType))
        {
            return readFromElement(source, rootMapping);
        }

        var classMapping = classMappingForElement(source, null, isAbstractResourceType(typeHint) ? null : typeHint);
        return readFromElement(source, classMapping);
    }

    private static bool isAbstractResourceType(Type? typeHint) =>
        typeHint is not null && typeof(Resource).IsAssignableFrom(typeHint) && typeHint.IsAbstract;
}