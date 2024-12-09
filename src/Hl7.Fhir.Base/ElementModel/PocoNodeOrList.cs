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

public abstract record PocoNodeOrList(string Name) : IEnumerable<PocoNode>
{
    public abstract PocoNode? Parent { get; }
    
    public abstract IEnumerator<PocoNode> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static PocoNode Root(Base @base, string? name = null) => @base switch
    {
        PrimitiveType primitive => new PrimitiveNode(primitive, name),
        { } b => new PocoNode(b, null, null, name)
    };
    
    public static PocoNode ForPrimitive(PrimitiveType primitive) => 
        new PrimitiveNode(primitive);
    
    public static PocoNode ForPrimitive<T>(object value) where T : PrimitiveType, new() => 
        new PrimitiveNode(new T { ObjectValue = value });
    
    public static IEnumerable<PocoNode> FromList(IEnumerable<PrimitiveType> primitives, string? name = null) => 
        primitives.Select(ForPrimitive);
    
    public static IEnumerable<PocoNode> FromList<T>(IEnumerable<object> values) where T : PrimitiveType, new() => 
        values.Select(ForPrimitive<T>);
}

public record PocoNode(Base Poco, PocoNodeOrList? ParentNode, int? Index, string? Name)
    : PocoNodeOrList(Name ?? Poco.TypeName), IScopedNode, IFhirValueProvider, IResourceTypeSupplier, IAnnotated
{
    public IEnumerable<PocoNodeOrList> Children() =>
        Poco.GetElementPairs()
            .Select(ep =>
                nodeFor(ep.Key, ep.Value)
            );

    public PocoNodeOrList? Child(string name) => Poco.TryGetValue(name, out var result)
        ? nodeFor(name, result)
        : null;

    private PocoNodeOrList nodeFor(string name, object value) =>
        value switch
        {
            PrimitiveType primitive => new PrimitiveNode(primitive, name) { ParentNode = this },
            Base b => new PocoNode(b, this, null, name),
            IEnumerable<PrimitiveType> primitiveList => new PrimitiveListNode(primitiveList.ToList(), name) { ParentNode = this },
            IEnumerable<Base> list => new PocoListNode(list.ToList(), this, name),
            _ => throw new InvalidOperationException("Unexpected element in child list")
        };

    private IEnumerable<PocoNode> asList() => [this];

    public override IEnumerator<PocoNode> GetEnumerator() => asList().GetEnumerator();

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
    
    IEnumerable<ITypedElement> ITypedElement.Children(string? name) => name is null
        ? Children().SelectMany(node => node)
        : Child(name) ?? Enumerable.Empty<PocoNode>();
    
    #endregion
    
    #region IScopedNode
    
    public override PocoNode? Parent => ParentNode switch
    {
        PocoListNode rpen => rpen[Index!.Value],
        PocoNode spen => spen,
        _ => null
    };
    
    #endregion
}

internal record PocoListNode(IReadOnlyList<Base> Pocos, PocoNodeOrList? ParentNode, string Name) : PocoNodeOrList(Name)
{
    public PocoNode this[int index] => new(Pocos[index], Parent, index, Name);

    public IEnumerable<PocoNode> Where<T>(Func<T, bool> predicate) where T : Base =>
        Pocos.OfType<T>().Where(predicate).Select((poco, index) => new PocoNode(poco, Parent, index, Name));

    public PocoNode? FirstOrDefault<T>(Func<T, bool> predicate) where T : Base
    {
        for (int index = 0; index < Pocos.Count; index++)
        {
            if (Pocos[index] is T item && predicate(item))
                return new PocoNode(item, Parent, index, Name);
        }

        return null;
    }

    public bool Any() => Pocos.Any();

    public override PocoNode? Parent => ParentNode as PocoNode;
    public override IEnumerator<PocoNode> GetEnumerator() => Pocos.Select((poco, index) => new PocoNode(poco, Parent, index, Name)).GetEnumerator();
}

public record PrimitiveNode(PrimitiveType Primitive, string? Name = null) : PocoNode(Primitive, null, null, Name)
{
    protected override object? ValueInternal => Primitive.ToITypedElementValue();
    internal object? Value => Primitive.ObjectValue;
}

internal record PrimitiveListNode(IReadOnlyList<PrimitiveType> Primitives, string? Name = null) : PocoListNode(Primitives, null, Name ?? "value")
{
    public override IEnumerator<PocoNode> GetEnumerator() =>
        Primitives.Select((primitive, index) => new PrimitiveNode(primitive, Name) { Index = index }).GetEnumerator();

    internal IEnumerable<object?> Values => Primitives.Select(p => p.ObjectValue);
}

public static class PocoElementNodeExtensions
{
    public static T? Child<T>(this PocoNode? node, string name) where T : PocoNodeOrList => node?.Child(name) as T;
}