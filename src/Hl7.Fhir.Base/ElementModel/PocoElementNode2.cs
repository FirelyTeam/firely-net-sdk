using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hl7.Fhir.ElementModel;

#nullable enable

public abstract record PocoElementNode2(string Name, SinglePocoElementNode? Parent, object Payload) : IEnumerable<SinglePocoElementNode>
{
    public abstract IEnumerable<PocoElementNode2> Children();
    public abstract PocoElementNode2? Child(string name);
    
    public abstract IEnumerator<SinglePocoElementNode> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record SinglePocoElementNode(string Name, int? Index, SinglePocoElementNode? Parent, Base Poco) : PocoElementNode2(Name, Parent, Poco), IScopedNode, IFhirValueProvider, IResourceTypeSupplier
{
    public override IEnumerable<PocoElementNode2> Children() =>
        Poco.GetElementPairs()
            .Where(kvp => kvp.Key is not "value") // we should be able to throw this check away once we deprecate value
            .Select<KeyValuePair<string, object>, PocoElementNode2>(ep =>
                (ep.Key, ep.Value) switch
                {
                    ({ } key, Base b) => new SinglePocoElementNode(key, null, this, b),
                    ({ } key, IEnumerable<Base> list) => new RepeatingPocoElementNode(key, this, list.ToList()),
                    ("url", string s) when Poco is Extension => new SinglePocoElementNode("url", null, this, new FhirUri(s)),
                    ("id", string s) when Poco is Element => new SinglePocoElementNode("id", null, this, new FhirString(s)),
                    _ => throw new InvalidOperationException("Unexpected system primitive in child list")
                }
            );

    public override PocoElementNode2? Child(string name) => Poco.TryGetValue(name, out var result)
        ? result switch
        {
            Base b => new SinglePocoElementNode(name, null, this, b),
            IEnumerable<Base> list => new RepeatingPocoElementNode(name, this, list.ToList()),
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

public record RepeatingPocoElementNode(string Name, SinglePocoElementNode? Parent, IReadOnlyList<Base> Pocos) : PocoElementNode2(Name, Parent, Pocos)
{
    public SinglePocoElementNode this[int index] => new(Name, index, Parent, Pocos[index]);
    
    public override IEnumerable<PocoElementNode2> Children()
    {
        throw new NotImplementedException();
    }

    public override PocoElementNode2? Child(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SinglePocoElementNode> Where<T>([NotNull] Func<T, bool> predicate) where T : Base =>
        Pocos.OfType<T>().Where(predicate).Select((poco, index) => new SinglePocoElementNode(Name, index, Parent, poco));

    public SinglePocoElementNode? FirstOrDefault<T>([NotNull] Func<T, bool> predicate) where T : Base
    {
        for(int index = 0; index < Pocos.Count(); index++)
        {
            if (Pocos[index] is T item && predicate(item))
                return new SinglePocoElementNode(Name, index, Parent, item);
        }
        return null;
    }

    public override IEnumerator<SinglePocoElementNode> GetEnumerator() => Pocos.Select((poco, index) => new SinglePocoElementNode(Name, index, Parent, poco)).GetEnumerator();
}

public record SinglePrimitiveElementNode<T>(T Primitive, string? Name = null) : SinglePocoElementNode(Name ?? "value", null, null, Primitive) where T : PrimitiveType, new()
{
    private readonly Lazy<object?> _iteValue = new (Primitive.ToITypedElementValue);
    public override object? Value => _iteValue.Value;
    
    public SinglePrimitiveElementNode(object primitive, string? name = null) : this(new T {ObjectValue = primitive}, name){}
}


public record RepeatingPrimitiveElementNode<T>(IReadOnlyList<T> Values, string? Name = null) : RepeatingPocoElementNode(Name ?? "value", null, Values) where T : PrimitiveType, new()
{
    public RepeatingPrimitiveElementNode(params object[] values) : this(values.Select(v => new T { ObjectValue = v }).ToList()){}
}

public static class PocoElementNodeExtensions
{
    public static T? ExtractSingle<T>(this PocoElementNode2? node) where T : Base => node?.Payload as T;

    public static IEnumerable<T> ExtractList<T>(this PocoElementNode2? node) where T : Base => 
        node?.Payload switch
        {
            IEnumerable<T> list => list,
            T single => [single],
            _ => []
        };
    
    public static T? Child<T>(this PocoElementNode2? node, string name) where T : PocoElementNode2 => node?.Child(name) as T;
}