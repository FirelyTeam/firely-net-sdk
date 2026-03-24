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
/// Traverses an <see cref="ITypedElement"/> tree and constructs a POCO from it.
/// </summary>
internal partial class NewPocoBuilder
{
    private readonly ModelInspector _inspector;
    private readonly PocoBuilderSettings? _settings;

    /// <summary>
    /// Initializes a builder that can traverse typed or source nodes and construct POCO instances.
    /// </summary>
    /// <param name="inspector">The inspector providing the necessary metadata about the FHIR POCO classes used in the construction.</param>
    /// <param name="settings">Configuration for building the POCO.</param>
    public NewPocoBuilder(ModelInspector inspector, PocoBuilderSettings? settings = null)
    {
        this._inspector = inspector ?? throw Error.ArgumentNull(nameof(inspector));
        this._settings = settings;
    }

    /// <summary>
    /// Build a POCO from an <see cref="ITypedElement"/>.
    /// </summary>
    public Base BuildFrom(ITypedElement source, Type? typeHint = null)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        var classMapping = classMappingForElement(source, null, typeHint);
        return readFromElement(source, classMapping);
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
            _inspector.FindClassMapping(source.Name) is { } rootMapping &&
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