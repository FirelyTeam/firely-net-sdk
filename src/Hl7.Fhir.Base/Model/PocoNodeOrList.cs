using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.Model;

#nullable enable

/// <summary>
/// A singular node in a POCO node tree. This node represents either a repeating or singular POCO instance.
/// </summary>
/// <param name="Name"></param>
public abstract record PocoNodeOrList(string Name) : IEnumerable<PocoNode>
{
    /// <summary>
    /// The parent of this node. This is always a singular PocoNode. If the Parent field is set to a PocoListNode, this will construct and return the PocoNode at the specified index.
    /// </summary>
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
    : PocoNodeOrList(Name ?? Poco.TypeName), ITypedElement, IShortPathGenerator, ISourceNode, IFhirValueProvider, IResourceTypeSupplier, IAnnotatable
{
    /// <inheritdoc />
    public override PocoNode? Parent => ParentNode switch
    {
        PocoListNode nodes => nodes[Index!.Value],
        PocoNode node => node,
        _ => null
    };
    
    /// <summary>
    /// Enumerates all children of this node. These can each either be singular or repeating PocoNodes.
    /// </summary>
    /// <returns></returns>
    /// <remarks>Since PocoNodeOrList implements IEnumerable of PocoNode, you can consider this to be an IEnumerable of IEnumerable of PocoNode, if you prefer to work with that</remarks>
    public IEnumerable<PocoNodeOrList> Children() =>
        Poco.EnumerateElements()
            .Select(ep =>
                nodeFor(ep.Key, ep.Value)
            );

    /// <summary>
    /// Finds a single child of this node by name. The result is either a singular or repeating PocoNode. The return value can always be used as an IEnumerable of PocoNode.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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
    
    /// <inheritdoc />
    string IShortPathGenerator.ShortPath => (Index, Parent) switch
    {
        // if we have an index, we have a parent.
        ({ } idx, { } parent) => $"{((IShortPathGenerator)parent).ShortPath}.{Name}[{idx}]",
        // Note that we omit indices here.
        (_, { } parent) => $"{((IShortPathGenerator)parent).ShortPath}.{Name}",
        // if we have neither, we are the root. Note that we omit indices here.
        _ => Name
    };

    /// <inheritdoc />
    Base IFhirValueProvider.FhirValue => Poco;

    /// <inheritdoc />
    string? IResourceTypeSupplier.ResourceType => Poco is Resource
        ? ((ITypedElement)this).InstanceType
        : null;

    private AnnotationList? _annotations;

    private AnnotationList Annotations => LazyInitializer.EnsureInitialized(ref _annotations, () => [])!;

    /// <inheritdoc />
    IEnumerable<object> IAnnotated.Annotations(Type type)
    {
        if (type == typeof(PocoNode))
            return [this];
        if (type == typeof(ITypedElement) || type == typeof(IShortPathGenerator) || type == typeof(ISourceNode))
            return [this];
        if (type == typeof(IFhirValueProvider))
            return [this];
        if (type == typeof(IResourceTypeSupplier))
            return [this];
        
        if(Annotations.OfType(type).ToList() is {Count: > 0} annotations)
            return annotations;
        
        return Poco.Annotations(type);
    }

    /// <inheritdoc />
    void IAnnotatable.AddAnnotation(object annotation) => Annotations.AddAnnotation(annotation);

    /// <inheritdoc />
    void IAnnotatable.RemoveAnnotations(Type type) => Annotations.RemoveAnnotations(type);

    #endregion
}

/// <summary>
/// A single node for a repeating element. Note that since a repeating element has a single parent, this cannot be used for grouping "separate" pocos that are not repeating in the specification.
/// </summary>
/// <param name="Pocos"></param>
/// <param name="ParentNode"></param>
/// <param name="Name"></param>
public record PocoListNode(IReadOnlyList<Base> Pocos, PocoNodeOrList? ParentNode, string Name) : PocoNodeOrList(Name)
{
    public PocoNode this[int index] => new(Pocos[index], Parent, index, Name);
    public override PocoNode? Parent => ParentNode as PocoNode; // safe because FHIR knows no nested lists
    public override IEnumerator<PocoNode> GetEnumerator() => Pocos.Select((poco, index) => new PocoNode(poco, Parent, index, Name)).GetEnumerator();
}