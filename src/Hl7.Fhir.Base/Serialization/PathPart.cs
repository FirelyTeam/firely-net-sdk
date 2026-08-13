/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Serialization;

internal record RootPathPart() : PathPart(null!, string.Empty)
{
    public override string GetInstancePath() => "$this";
}

internal record ResourcePathPart(PathPart Previous, string ResourceName) : PathPart(Previous, string.Empty)
{
    public override string GetInstancePath() => BuildPath(Previous is RootPathPart ? ResourceName : string.Empty);
}

internal record ElementPathPart(PathPart Previous, string ElementName) : PathPart(Previous, string.Empty)
 {
    public override string GetInstancePath() => BuildPath(ElementName);
 }

internal record IndexPathPart(PathPart Previous, int Index) : PathPart(Previous, string.Empty)
{
    public override string GetInstancePath() => BuildPath($"[{Index}]");
}

/// <summary>
/// Tracks the position within an instance as a dotted path. Used in diagnostics for the parser/serializers.
/// </summary>
internal abstract record PathPart(PathPart Previous, string _)
{
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

    public PathPart SetIndex(int index)
    {
        if (this is not IndexPathPart ipp)
            return new IndexPathPart(this, index);

        return ipp with { Index = index };
    }

    /// <summary>
    /// Return the fhirpath that includes the indexes. Note: in contained resources, this is just the path within the contained resource.
    /// </summary>
    public abstract string GetInstancePath();

    protected string BuildPath(string me) => Previous is RootPathPart ? me :
        $"{Previous.GetInstancePath()}{(me.Length > 0 && char.IsLetter(me[0]) ? "." : string.Empty)}{me}";
}