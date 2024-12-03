using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.ElementModel;

#nullable enable

public abstract record PocoElementNode2(PocoElementNode2? Parent, string Name) : IEnumerable<SinglePocoElementNode>
{
    public abstract IEnumerator<SinglePocoElementNode> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static SinglePocoElementNode Root(Base @base, string? name = null) => @base switch
    {
        PrimitiveType primitive => new SinglePrimitiveElementNode(primitive, name),
        { } b => new SinglePocoElementNode(b, null, null, name)
    };
    
    public static SinglePocoElementNode ForPrimitive(PrimitiveType primitive) => 
        new SinglePrimitiveElementNode(primitive);
    
    public static SinglePocoElementNode ForPrimitive<T>(object value) where T : PrimitiveType, new() => 
        new SinglePrimitiveElementNode(new T { ObjectValue = value });
    
    public static IEnumerable<SinglePocoElementNode> FromList(IEnumerable<PrimitiveType> primitives, string? name = null) => 
        primitives.Select(ForPrimitive);
    
    public static IEnumerable<SinglePocoElementNode> FromList<T>(IEnumerable<object> values) where T : PrimitiveType, new() => 
        values.Select(ForPrimitive<T>);
}

public record SinglePocoElementNode(Base Poco, PocoElementNode2? Parent, int? Index, string? Name)
    : PocoElementNode2(Parent, Name ?? Poco.TypeName), IScopedNode, IFhirValueProvider, IResourceTypeSupplier, IAnnotated
{
    public IEnumerable<PocoElementNode2> Children() =>
        Poco.GetElementPairs()
            .Select(ep =>
                nodeFor(ep.Key, ep.Value)
            );

    public PocoElementNode2? Child(string name) => Poco.TryGetValue(name, out var result)
        ? nodeFor(name, result)
        : null;

    private PocoElementNode2 nodeFor(string name, object value) =>
        value switch
        {
            PrimitiveType primitive => new SinglePrimitiveElementNode(primitive, name) { Parent = this },
            Base b => new SinglePocoElementNode(b, this, null, name),
            IEnumerable<PrimitiveType> primitiveList => new RepeatingPrimitiveElementNode(primitiveList.ToList(), name) { Parent = this },
            IEnumerable<Base> list => new RepeatingPocoElementNode(list.ToList(), this, name),
            _ => throw new InvalidOperationException("Unexpected element in child list")
        };

    private IEnumerable<SinglePocoElementNode> asList() => [this];

    public override IEnumerator<SinglePocoElementNode> GetEnumerator() => asList().GetEnumerator();

    string IShortPathGenerator.ShortPath => (Index, Parent) switch
    {
        // if we have an index, we have a parent.
        ({ } idx, { } parent) => $"{((IShortPathGenerator)parent).ShortPath}.{Name}[{idx}]",
        // Note that we omit indices here.
        (_, { } parent) => $"{((IShortPathGenerator)parent).ShortPath}.{Name}",
        // if we have neither, we are the root. Note that we omit indices here.
        _ => Name
    };

    Base IFhirValueProvider.FhirValue => Poco;

    string? IResourceTypeSupplier.ResourceType => Poco is Resource
        ? ((ITypedElement)this).InstanceType
        : null;

    IEnumerable<object> IAnnotated.Annotations(Type type)
    {
        if (type == typeof(ITypedElement) || type == typeof(IShortPathGenerator) || type == typeof(IScopedNode))
            return [this];
        if (type == typeof(IFhirValueProvider))
            return [this];
        if (type == typeof(IResourceTypeSupplier))
            return [this];
        return Poco.Annotations(type);
    }
    
    #region ITypedElement
    
    string ITypedElement.InstanceType =>
        Poco switch
        {
            BackboneElement => "BackboneElement",
            Element when Poco.TypeName.Contains('.') => "Element",
            _ => Poco.TypeName
        };

    object? ITypedElement.Value => ValueInternal;

    // needed for ITE
    protected virtual object? ValueInternal => null;

    string ITypedElement.Location => (Index, Parent) switch
    {
        // if we have an index, write it
        ({ } idx, { } parent) => $"{((ITypedElement)parent).Location}.{Name}[{idx}]",
        // if we do not, write 0 as idx
        (_, { } parent) => $"{((ITypedElement)parent).Location}.{Name}[0]",
        // if we have neither, we are the root.
        _ => Name
    };

    // needed for ITE
    IElementDefinitionSummary? ITypedElement.Definition => null;
    
    IEnumerable<ITypedElement> ITypedElement.Children(string? name) => (this as IScopedNode).Children(name);
    
    #endregion
    
    #region IScopedNode
    
    IScopedNode? IScopedNode.Parent => Parent switch
    {
        RepeatingPocoElementNode rpen => rpen[Index!.Value],
        SinglePocoElementNode spen => spen,
        _ => null
    };

    IEnumerable<IScopedNode> IScopedNode.Children(string? name) => name is null
        ? Children().SelectMany(node => node)
        : Child(name) ?? Enumerable.Empty<SinglePocoElementNode>();
    
    [TemporarilyChanged] // we should investigate whether we want to even use this anymore. If we do, we should make this implementation explicit.
    NodeType IScopedNode.Type => Poco switch
    {
        Bundle => NodeType.Bundle | NodeType.Resource,
        PrimitiveType => NodeType.Primitive,
        DomainResource => NodeType.DomainResource | NodeType.Resource,
        Resource => NodeType.Resource,
        ResourceReference or Canonical or CodeableReference => NodeType.Reference,
        Quantity => NodeType.Quantity,
        _ => 0
    };
    
    bool IScopedNode.TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result)
    {
        result = Poco is Bundle
            ? this
                .Child<RepeatingPocoElementNode>("entry")
                ?.FirstOrDefault<Bundle.EntryComponent>(entry =>
                    entry.FullUrl == fullUrl)
                ?.Child<SinglePocoElementNode>("resource")
            : null;
        return result is not null;
    }

    bool IScopedNode.TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result)
    {
        result = Poco is DomainResource
            ? this
                .Child<RepeatingPocoElementNode>("contained")
                ?.FirstOrDefault<Resource>(contained => $"#{contained.Id}" == id)
            : null;
        return result is not null;
    }
    
    #endregion
}

internal record RepeatingPocoElementNode(IReadOnlyList<Base> Pocos, PocoElementNode2? Parent, string Name) : PocoElementNode2(Parent, Name)
{
    public SinglePocoElementNode this[int index] => new(Pocos[index], Parent, index, Name);

    public IEnumerable<SinglePocoElementNode> Where<T>(Func<T, bool> predicate) where T : Base =>
        Pocos.OfType<T>().Where(predicate).Select((poco, index) => new SinglePocoElementNode(poco, Parent, index, Name));

    public SinglePocoElementNode? FirstOrDefault<T>(Func<T, bool> predicate) where T : Base
    {
        for (int index = 0; index < Pocos.Count; index++)
        {
            if (Pocos[index] is T item && predicate(item))
                return new SinglePocoElementNode(item, Parent, index, Name);
        }

        return null;
    }

    public override IEnumerator<SinglePocoElementNode> GetEnumerator() => Pocos.Select((poco, index) => new SinglePocoElementNode(poco, Parent, index, Name)).GetEnumerator();
}

public record SinglePrimitiveElementNode(PrimitiveType Primitive, string? Name = null) : SinglePocoElementNode(Primitive, null, null, Name)
{
    protected override object? ValueInternal => Primitive.ToITypedElementValue();
}

internal record RepeatingPrimitiveElementNode(IReadOnlyList<PrimitiveType> Primitives, string? Name = null) : RepeatingPocoElementNode(Primitives, null, Name ?? "value")
{
    public override IEnumerator<SinglePocoElementNode> GetEnumerator() =>
        Primitives.Select((primitive, index) => new SinglePrimitiveElementNode(primitive, Name) { Index = index }).GetEnumerator();
}

public static class PocoElementNodeExtensions
{
    public static T? Child<T>(this SinglePocoElementNode? node, string name) where T : PocoElementNode2 => node?.Child(name) as T;
}