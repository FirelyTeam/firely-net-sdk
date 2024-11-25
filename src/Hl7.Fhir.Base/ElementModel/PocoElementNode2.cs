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

public abstract record PocoElementNode2(string Name, PocoElementNode2? Parent, object Payload) : IScopedNode, IFhirValueProvider, IResourceTypeSupplier
{
    public abstract SinglePocoElementNode this[int index] { get; }
    
    public abstract IEnumerable<PocoElementNode2> Children();
    public abstract PocoElementNode2? Child(string name);

    public abstract IEnumerable<PocoElementNode2> ListMembers();
    public abstract NodeType Type { get; }
    public abstract object? Value { get; }
    public abstract string Location { get; }
    public abstract bool TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result);

    public abstract bool TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result);

    public IElementDefinitionSummary? Definition => null;

    IScopedNode? IScopedNode.Parent => Parent;
    IEnumerable<IScopedNode> IScopedNode.Children(string? name) => name is null 
        ? Children().SelectMany(node => node.ListMembers())
        : Child(name) is {} child ? [child] : [];

    IEnumerable<ITypedElement> ITypedElement.Children(string? name) => (this as IScopedNode).Children();

    public abstract string? InstanceType { get; }
    public abstract string ShortPath { get; }
    public Base FhirValue { get; }
    public string ResourceType { get; }
}

public record SinglePocoElementNode(string Name, int? Index, PocoElementNode2? Parent, Base Poco) : PocoElementNode2(Name, Parent, Poco)
{
    public override SinglePocoElementNode this[int index] => index == 0
        ? this
        : throw new ArgumentException("Index out of range", nameof(index));

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

    public override IEnumerable<PocoElementNode2> ListMembers() => [this];

    public override NodeType Type => Poco switch
    {
        Bundle => NodeType.Bundle | NodeType.Resource,
        PrimitiveType => NodeType.Primitive,
        DomainResource => NodeType.DomainResource | NodeType.Resource,
        Resource => NodeType.Resource,
        ResourceReference or Canonical or CodeableReference => NodeType.Reference,
        Quantity => NodeType.Quantity,
        _ => 0
    };

    public override string? InstanceType =>
        Poco switch
        {
            BackboneElement => "BackboneElement",
            Element when Poco.TypeName.Contains('.') => "Element",
            _ => Poco.TypeName
        };

    private Lazy<object?> _iteValue = new (() => Poco is PrimitiveType ? Poco.ToITypedElementValue() : null);
    public override object? Value => _iteValue.Value;

    public override string Location => (Index, Parent) switch
    {
        // if we have an index, write it
        ({ } idx, { } parent) => $"{parent.Location}.{Name}[{idx}]",
        // if we do not, write 0 as idx
        (_, { } parent) => $"{parent.Location}.{Name}[0]",
        // if we have neither, we are the root.
        _ => Name
    };

    public override bool TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result)
    {
        result = Poco is Bundle ? this
            .Child<RepeatingPocoElementNode>("entry")?
            .FirstOrDefault<Bundle.EntryComponent>(entry => 
                entry.FullUrl == fullUrl)
            ?.Child("resource") : null;
        return result is not null;
    }

    public override bool TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result) => throw new NotImplementedException();

    public override string ShortPath => (Index, Parent) switch
    {
        // if we have an index, we have a parent.
        ({ } idx, { } parent) => $"{parent.ShortPath}.{Name}[{idx}]",
        // Note that we omit indices here.
        (_, { } parent) => $"{parent.ShortPath}.{Name}",
        // if we have neither, we are the root. Note that we omit indices here.
        _ => Name
    };
}

public record RepeatingPocoElementNode(string Name, PocoElementNode2? Parent, IReadOnlyList<Base> Pocos) : PocoElementNode2(Name, Parent, Pocos)
{
    public override SinglePocoElementNode this[int index] => new(Name, index, Parent, Pocos[index]);
    
    public override IEnumerable<PocoElementNode2> Children()
    {
        throw new NotImplementedException();
    }

    public override PocoElementNode2? Child(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SinglePocoElementNode> Where<T>([NotNull] Func<T, bool> predicate) where T : Base =>
        Pocos.OfType<T>().Where(predicate).Select((poco, index) => new SinglePocoElementNode(Name, index, this, poco));

    public SinglePocoElementNode? FirstOrDefault<T>([NotNull] Func<T, bool> predicate) where T : Base
    {
        for(int index = 0; index < Pocos.Count(); index++)
        {
            if (Pocos[index] is T item && predicate(item))
                return new SinglePocoElementNode(Name, index, Parent, item);
        }
        return null;
    }

    public override IEnumerable<PocoElementNode2> ListMembers() => Pocos.Select((poco, index) => new SinglePocoElementNode(Name, index, Parent, poco));
    public override NodeType Type => throw new NotImplementedException();
    public override string? InstanceType { get; } // derive from classMapping?
    public override string ShortPath => throw new NotImplementedException();

    public override object? Value => throw new NotImplementedException();

    public override string Location => throw new NotImplementedException();
    public override bool TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result) => throw new NotImplementedException();

    public override bool TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result) => throw new NotImplementedException();
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