using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hl7.Fhir.ElementModel;

#nullable enable

public abstract record PocoElementNode2(SinglePocoElementNode? Parent, string Name) : IEnumerable<SinglePocoElementNode>
{
    public abstract IEnumerator<SinglePocoElementNode> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record SinglePocoElementNode(Base Poco, SinglePocoElementNode? Parent, int? Index, string? Name) : PocoElementNode2(Parent, Name ?? Poco.TypeName), IScopedNode, IFhirValueProvider, IResourceTypeSupplier
{
    public IEnumerable<PocoElementNode2> Children() =>
        Poco.GetElementPairs()
            .Select<KeyValuePair<string, object>, PocoElementNode2>(ep =>
                (ep.Key, ep.Value) switch
                {
                    ({ } key, PrimitiveType primitive) => new SinglePrimitiveElementNode<PrimitiveType>(primitive, key) {Parent = this},
                    ({ } key, Base b) => new SinglePocoElementNode(b, this, null, key),
                    ({ } key, IEnumerable<PrimitiveType> primitiveList) => new RepeatingPrimitiveElementNode<PrimitiveType>(primitiveList.ToList(), key) {Parent = this},
                    ({ } key, IEnumerable<Base> list) => new RepeatingPocoElementNode(list.ToList(), this, key),
                    _ => throw new InvalidOperationException("Unexpected system primitive in child list")
                }
            );

    public PocoElementNode2? Child(string name) => Poco.TryGetValue(name, out var result)
        ? result switch
        {
            PrimitiveType primitive => new SinglePrimitiveElementNode<PrimitiveType>(primitive, name) {Parent = this},
            Base b => new SinglePocoElementNode(b, this, null, name),
            IEnumerable<PrimitiveType> primitiveList => new RepeatingPrimitiveElementNode<PrimitiveType>(primitiveList.ToList(), name) {Parent = this},
            IEnumerable<Base> list => new RepeatingPocoElementNode(list.ToList(), this, name),
            _ => throw new InvalidOperationException("Unexpected system primitive in child list")
        }
        : null;

    private IEnumerable<SinglePocoElementNode> asList() => [this];

    public NodeType Type => Poco switch
    {
        Bundle => NodeType.Bundle | NodeType.Resource,
        PrimitiveType => NodeType.Primitive,
        DomainResource => NodeType.DomainResource | NodeType.Resource,
        Resource => NodeType.Resource,
        ResourceReference or Canonical or CodeableReference => NodeType.Reference,
        Quantity => NodeType.Quantity,
        _ => 0
    };

    public string? InstanceType =>
        Poco switch
        {
            BackboneElement => "BackboneElement",
            Element when Poco.TypeName.Contains('.') => "Element",
            _ => Poco.TypeName
        };

    public virtual object? Value => null;

    public string Location => (Index, Parent) switch
    {
        // if we have an index, write it
        ({ } idx, { } parent) => $"{parent.Location}.{Name}[{idx}]",
        // if we do not, write 0 as idx
        (_, { } parent) => $"{parent.Location}.{Name}[0]",
        // if we have neither, we are the root.
        _ => Name
    };

    public IElementDefinitionSummary? Definition => null;

    public bool TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result)
    {
        result = Poco is Bundle ? this
            .Child<RepeatingPocoElementNode>("entry")?
            .FirstOrDefault<Bundle.EntryComponent>(entry => 
                entry.FullUrl == fullUrl)
            ?.Child<SinglePocoElementNode>("resource") : null;
        return result is not null;
    }

    public bool TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result) => throw new NotImplementedException();
    public override IEnumerator<SinglePocoElementNode> GetEnumerator() => asList().GetEnumerator();

    public string ShortPath => (Index, Parent) switch
    {
        // if we have an index, we have a parent.
        ({ } idx, { } parent) => $"{parent.ShortPath}.{Name}[{idx}]",
        // Note that we omit indices here.
        (_, { } parent) => $"{parent.ShortPath}.{Name}",
        // if we have neither, we are the root. Note that we omit indices here.
        _ => Name
    };

    public Base FhirValue => Poco;
    
    public string? ResourceType => Poco is Resource 
        ? InstanceType
        : null;
    
    IScopedNode? IScopedNode.Parent => Parent;
    IEnumerable<IScopedNode> IScopedNode.Children(string? name) => name is null 
        ? Children().SelectMany(node => node)
        : Child(name) ?? Enumerable.Empty<SinglePocoElementNode>();

    IEnumerable<ITypedElement> ITypedElement.Children(string? name) => (this as IScopedNode).Children();
}

public record RepeatingPocoElementNode(IReadOnlyList<Base> Pocos, SinglePocoElementNode? Parent, string Name) : PocoElementNode2(Parent, Name)
{
    public SinglePocoElementNode this[int index] => new(Pocos[index], Parent, index, Name);

    public IEnumerable<SinglePocoElementNode> Where<T>([NotNull] Func<T, bool> predicate) where T : Base =>
        Pocos.OfType<T>().Where(predicate).Select((poco, index) => new SinglePocoElementNode(poco, Parent, index, Name));

    public SinglePocoElementNode? FirstOrDefault<T>([NotNull] Func<T, bool> predicate) where T : Base
    {
        for(int index = 0; index < Pocos.Count(); index++)
        {
            if (Pocos[index] is T item && predicate(item))
                return new SinglePocoElementNode(item, Parent, index, Name);
        }
        return null;
    }

    public override IEnumerator<SinglePocoElementNode> GetEnumerator() => Pocos.Select((poco, index) => new SinglePocoElementNode(poco, Parent, index, Name)).GetEnumerator();
}

public record SinglePrimitiveElementNode<T> : SinglePocoElementNode where T : PrimitiveType
{
    public static SinglePrimitiveElementNode<T> FromSystemPrimitive<TTo>(object primitive, string? name = null) where TTo : T, new()
    {
        return new SinglePrimitiveElementNode<T>(new TTo { ObjectValue = primitive }, name);
    }

    public SinglePrimitiveElementNode(T primitive, string? name = null) : base(primitive, null, null, name ?? "value"){}
    private T Primitive => (T)Poco;
    public override object? Value => Primitive.ToITypedElementValue();
}


public record RepeatingPrimitiveElementNode<T> : RepeatingPocoElementNode where T : PrimitiveType
{
    public RepeatingPrimitiveElementNode(IReadOnlyList<T> primitives, string? name = null) : base(primitives, null, name ?? "value") { }

    public static RepeatingPrimitiveElementNode<T> FromSystemPrimitives<TTo>(IEnumerable<object> values, string? name = null) where TTo: T, new()
    {
        return new RepeatingPrimitiveElementNode<T>(values.Select(v => new TTo { ObjectValue = v }).ToList(), name);
    }

    public override IEnumerator<SinglePocoElementNode> GetEnumerator() => Primitives.Select((primitive, index) => new SinglePrimitiveElementNode<T>(primitive, Name) {Index = index}).GetEnumerator();

    private IReadOnlyList<T> Primitives => (IReadOnlyList<T>)Pocos;
}

public static class PocoElementNodeExtensions
{
    public static T? Child<T>(this SinglePocoElementNode? node, string name) where T : PocoElementNode2 => node?.Child(name) as T;
}