/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

#nullable enable

namespace Hl7.Fhir.Serialization;

internal sealed class RootPathPart() : PathPart(null!)
{
    public override string GetInstancePath() => "$this";
}

internal sealed class ResourcePathPart(PathPart previous, string resourceName) : PathPart(previous)
{
    public string ResourceName { get; } = resourceName;

    public override string GetInstancePath() => BuildPath(Previous is RootPathPart ? ResourceName : string.Empty);
}

internal sealed class ElementPathPart(PathPart previous, string elementName) : PathPart(previous)
{
    public string ElementName { get; } = elementName;

    public override string GetInstancePath() => BuildPath(ElementName);
}

internal sealed class IndexPathPart(PathPart previous, int index) : PathPart(previous)
{
    public int Index { get; } = index;

    public override string GetInstancePath() => BuildPath($"[{Index}]");
}

/// <summary>
/// Tracks the position within an instance as a dotted path. Used in diagnostics for the parser/serializers.
/// </summary>
/// <remarks>Parts are immutable: entering or leaving an element returns a new part, and an existing part keeps
/// representing the position it was created for. The deserializers depend on that - they hand the
/// <see cref="PathProducer"/> of the current part to the validators, which may run (and thus materialize the
/// path) after the parse has moved on, as is the case for the validation of FHIR primitives, which is delayed
/// until the enclosing object has been read completely.</remarks>
internal abstract class PathPart(PathPart previous)
{
    public PathPart Previous { get; } = previous;

    public PathPart EnterResource(string name) => new ResourcePathPart(this, name);

    public PathPart ExitResource()
    {
        if(this is not ResourcePathPart)
            throw new InvalidOperationException("Can only exit from a resource part.");

        return Previous;
    }

    public PathPart EnterElement(string name) => new ElementPathPart(this, name);

    public PathPart ExitElement()
    {
        // If we are an IndexPathPart, we need to exit this index part first...
        var here = this is IndexPathPart ? this.Previous : this;

        // ...before we can exit the ElementPathPart.
        if(here is not ElementPathPart)
            throw new InvalidOperationException("Can only exit from an element part.");

        return here.Previous;
    }

    public PathPart SetIndex(int index) =>
        this is IndexPathPart ipp ? new IndexPathPart(ipp.Previous, index) : new IndexPathPart(this, index);

    /// <summary>
    /// Return the fhirpath that includes the indexes. Note: in contained resources, this is just the path within the contained resource.
    /// </summary>
    public abstract string GetInstancePath();

    /// <summary>
    /// A delegate producing <see cref="GetInstancePath"/> for this position, for use by the validators.
    /// </summary>
    /// <remarks>A path is only ever materialized when a diagnostic is actually produced, so validation is
    /// handed this producer rather than a path. Creating it once per part - instead of once per validation -
    /// is what keeps it out of the per-element allocation budget of a parse: every validation done at this
    /// position (at least a property and an object validation) shares this single delegate.</remarks>
    public Func<string> PathProducer => _pathProducer ??= GetInstancePath;

    private Func<string>? _pathProducer;

    protected string BuildPath(string me) => Previous is RootPathPart ? me :
        $"{Previous.GetInstancePath()}{(me.Length > 0 && char.IsLetter(me[0]) ? "." : string.Empty)}{me}";
}
