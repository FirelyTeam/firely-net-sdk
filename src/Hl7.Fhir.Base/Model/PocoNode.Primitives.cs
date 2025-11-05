#nullable enable

using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

public partial record PocoNode
{
    /// <summary>
    /// Constructs a PocoNode from a PrimitiveType
    /// </summary>
    /// <returns></returns>
    public static PocoNode ForPrimitive(PrimitiveType primitive) => 
        new PrimitiveNode(primitive, null, null);

    
    /// <summary>
    /// Constructs a PocoNode from an object. Allowed objects are those that can be converted to a PrimitiveType, and are not yet PrimitiveTypes.
    /// </summary>
    /// <returns></returns>
    public static PocoNode ForAnyPrimitive(object value)
    {
        return ForPrimitive(PrimitiveNode.InferFromValue(value));
    }
    
    /// <summary>
    /// Constructs a PocoNode from a value and a type. The type must be a PrimitiveType.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static PocoNode ForPrimitive<T>(object value) where T : PrimitiveType, new() => 
        new PrimitiveNode(new T { JsonValue = value }, null, null);
    
    /// <summary>
    /// Constructs a PocoNode from a list of PrimitiveTypes
    /// </summary>
    /// <param name="primitives"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> FromList(IEnumerable<PrimitiveType> primitives, string? name = null) => 
        primitives.Select(ForPrimitive);
    
    /// <summary>
    /// Constructs multiple PocoNodes from a list of values and a type. The type must be a PrimitiveType.
    /// </summary>
    /// <param name="values"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<PocoNode> FromList<T>(IEnumerable<object> values) where T : PrimitiveType, new() => 
        values.Select(ForPrimitive<T>);

    /// <summary>
    /// Constructs multiple PocoNodes from a list of objects. Allowed objects are those that can be converted to a PrimitiveType, and are not yet PrimitiveTypes.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> FromAnyList(IEnumerable<object> values) => 
        values.Select(v => v as PocoNode ?? ForAnyPrimitive(v));
}

public record PrimitiveNode(PrimitiveType Primitive, PocoNode? Parent, int? Index, string? Name = null) : PocoNode(Primitive, Parent, Index, Name)
{
    protected override object? ValueInternal => Primitive.ToITypedElementValue();
    internal object? Value => ValueInternal;
    
    internal static PrimitiveType InferFromValue(object value) => value switch
    {
        ElementModel.Types.Quantity qt => new FPQuantity(qt),
        ElementModel.Types.DateTime dt => new FPDateTime(dt),
        ElementModel.Types.Date d => new FPDate(d),
        ElementModel.Types.Time t => new FPTime(t),
        decimal dec => new FPDecimal(dec),
        float f => new FPDecimal((decimal)f),
        double d => new FPDecimal((decimal)d),
        bool b => new FPBoolean(b),
        int i => new FPInteger(i),
        long l => new FPLong(l),
        string s => new FPString(s),
        _ => throw new ArgumentException("Cannot infer primitive type from value", nameof(value))
    };
    
    protected override string? TextInternal => Primitive.ToString();
}

internal record PrimitiveListNode(IReadOnlyList<PrimitiveType> Primitives, PocoNode? Parent, string? Name = null) : PocoListNode(Primitives, Parent, Name ?? "value")
{
    public override IEnumerator<PocoNode> GetEnumerator() =>
        Primitives.Select((primitive, index) => new PrimitiveNode(primitive, Parent, index, Name)).GetEnumerator();

    internal IEnumerable<object?> Values => Primitives.Select(p => p.JsonValue);
}