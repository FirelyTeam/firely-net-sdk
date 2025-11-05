using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
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
/// <param name="Parent"></param>
public abstract record PocoNodeOrList(string Name, PocoNode? Parent) : IEnumerable<PocoNode>
{
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
/// <param name="Parent"></param>
/// <param name="Index">This Poco's index in a list, if it is contained in one</param>
/// <param name="Name"></param>
public partial record PocoNode(Base Poco, PocoNode? Parent, int? Index, string? Name)
    : PocoNodeOrList(Name ?? Poco.TypeName, Parent), ITypedElement, IShortPathGenerator, ISourceNode, IFhirValueProvider, IResourceTypeSupplier, IAnnotatable
{
    /// <summary>
    /// Enumerates all children of this node. These can each either be singular or repeating PocoNodes.
    /// </summary>
    /// <returns></returns>
    /// <remarks>Since PocoNodeOrList implements IEnumerable of PocoNode, you can consider this to be an IEnumerable of IEnumerable of PocoNode, if you prefer to work with that</remarks>
    public IEnumerable<PocoNodeOrList> Children()
    {
        var elements = Poco.EnumerateElements();

        // if we are a DynamicDataType, we hide the resourceType member from the children - it's probably a custom type
        // and will be serialized via IResourceTypeSupplier.ResourceType
        if (Poco is DynamicDataType)
            elements = elements.Where(x => x is not { Key: JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME, Value: PrimitiveType });
                
        return elements.Select(ep =>
                nodeFor(ep.Key, ep.Value)
            );
    }

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
            PrimitiveType primitive => new PrimitiveNode(primitive, this, null, name),
            Base b => new PocoNode(b, this, null, name),
            IReadOnlyList<PrimitiveType> primitiveList => new PrimitiveListNode(primitiveList.ToList(), this, name),
            IReadOnlyList<Base> list => new PocoListNode(list.ToList(), this, name),
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
    string? IResourceTypeSupplier.ResourceType => Poco switch
    {
        Resource r => r.TypeName,
        // handle the case of json serializer not being aware of the data it's serializing - a custom type that is not in ModelInspector
        // we will build a DynamicDataType, since a custom resource with a nested resource that is not in contained should be an exceptional case
        DynamicDataType when Poco.TryGetValue(JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME, out var type) && type is PrimitiveType pt => pt.JsonValue as string,
        _ => null
    };

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
/// <param name="Parent"></param>
/// <param name="Name"></param>
public record PocoListNode(IReadOnlyList<Base> Pocos, PocoNode? Parent, string Name) : PocoNodeOrList(Name, Parent)
{
    public PocoNode this[int index] => new(Pocos[index], Parent, index, Name);
    public override IEnumerator<PocoNode> GetEnumerator() => Pocos.Select((poco, index) => new PocoNode(poco, Parent, index, Name)).GetEnumerator();
}