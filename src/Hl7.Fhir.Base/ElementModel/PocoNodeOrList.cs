using Hl7.Fhir.Introspection;
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
using System.Threading;

namespace Hl7.Fhir.ElementModel;

#nullable enable

/// <summary>
/// A singular node in a POCO node tree. This node represents either a repeating or singular POCO instance.
/// </summary>
/// <param name="Name"></param>
public abstract record PocoNodeOrList(string Name) : IEnumerable<PocoNode>
{
    public abstract PocoNode? Parent { get; }
    
    public abstract IEnumerator<PocoNode> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static PocoNode Root(Base @base, string? name = null) => @base switch
    {
        PrimitiveType primitive => new PrimitiveNode(primitive, null, null, name),
        { } b => new PocoNode(b, null, null, name)
    };
}

/// <summary>
/// A singular node in a POCO node tree. This node represents a single POCO instance.
/// </summary>
/// <param name="Poco"></param>
/// <param name="ParentNode"></param>
/// <param name="Index">This Poco's index in a list, if it is contained in one</param>
/// <param name="Name"></param>
public partial record PocoNode(Base Poco, PocoNodeOrList? ParentNode, int? Index, string? Name)
    : PocoNodeOrList(Name ?? Poco.TypeName), IScopedNode, ISourceNode, IFhirValueProvider, IResourceTypeSupplier, IAnnotatable
{
    public override PocoNode? Parent => ParentNode switch
    {
        PocoListNode nodes => nodes[Index!.Value],
        PocoNode node => node,
        _ => null
    };
    
    public IEnumerable<PocoNodeOrList> Children() =>
        Poco.EnumerateElements()
            .Select(ep =>
                nodeFor(ep.Key, ep.Value)
            );

    public PocoNodeOrList? Child(string name) => Poco.TryGetValue(name, out var result)
        ? nodeFor(name, result)
        : null;

    private PocoNodeOrList nodeFor(string name, object value) =>
        value switch
        {
            PrimitiveType primitive => new PrimitiveNode(primitive, null, null, name) { ParentNode = this },
            Base b => new PocoNode(b, this, null, name),
            IEnumerable<PrimitiveType> primitiveList => new PrimitiveListNode(primitiveList.ToList(), null, name) { ParentNode = this },
            IEnumerable<Base> list => new PocoListNode(list.ToList(), this, name),
            _ => throw new InvalidOperationException("Unexpected element in child list")
        };

    private IEnumerable<PocoNode> asList() => [this];

    public override IEnumerator<PocoNode> GetEnumerator() => asList().GetEnumerator();
    
    #region << Annotations >>
    
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

    private AnnotationList? _annotations;

    private AnnotationList Annotations => LazyInitializer.EnsureInitialized(ref _annotations, () => [])!;

    IEnumerable<object> IAnnotated.Annotations(Type type)
    {
        if (type == typeof(ITypedElement) || type == typeof(IShortPathGenerator) || type == typeof(IScopedNode))
            return [this];
        if (type == typeof(IFhirValueProvider))
            return [this];
        if (type == typeof(IResourceTypeSupplier))
            return [this];
        return Annotations.OfType(type);
    }

    void IAnnotatable.AddAnnotation(object annotation) => Annotations.AddAnnotation(annotation);

    void IAnnotatable.RemoveAnnotations(Type type) => Annotations.RemoveAnnotations(type);

    #endregion
}

/// <summary>
/// A single node for a repeating POCO. Note that since a repeating element has a single parent, this cannot be used for grouping "separate" pocos that are not repeating in the specification.
/// </summary>
/// <param name="Pocos"></param>
/// <param name="ParentNode"></param>
/// <param name="Name"></param>
internal record PocoListNode(IReadOnlyList<Base> Pocos, PocoNodeOrList? ParentNode, string Name) : PocoNodeOrList(Name)
{
    public PocoNode this[int index] => new(Pocos[index], Parent, index, Name);
    public override PocoNode? Parent => ParentNode as PocoNode;
    public override IEnumerator<PocoNode> GetEnumerator() => Pocos.Select((poco, index) => new PocoNode(poco, Parent, index, Name)).GetEnumerator();
}