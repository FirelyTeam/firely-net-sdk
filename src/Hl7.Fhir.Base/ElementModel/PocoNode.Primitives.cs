using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Date = Hl7.Fhir.Model.Date;
using Integer = Hl7.Fhir.Model.Integer;
using Quantity = Hl7.Fhir.Model.Quantity;
using Time = Hl7.Fhir.Model.Time;

#nullable enable

namespace Hl7.Fhir.ElementModel;

public partial record PocoNode
{
    public static PocoNode ForPrimitive(PrimitiveType primitive) => 
        new PrimitiveNode(primitive);

    public static PocoNode ForAnyPrimitive(object value)
    {
        if (value is Types.Quantity quantity)
        {
            return forQuantity(quantity);
        }
        return ForPrimitive(PrimitiveNode.InferFromValue(value));
    }

    private static PocoNode forQuantity(Types.Quantity quantity) =>
        new PocoNode(new Quantity(quantity), null, null, "quantity");
    
    public static PocoNode ForPrimitive<T>(object value) where T : PrimitiveType, new() => 
        new PrimitiveNode(new T { ObjectValue = value });
    
    public static IEnumerable<PocoNode> FromList(IEnumerable<PrimitiveType> primitives, string? name = null) => 
        primitives.Select(PocoNode.ForPrimitive);

    public static IEnumerable<PocoNode> FromList<T>(IEnumerable<object> values) where T : PrimitiveType, new() => 
        values.Select(PocoNode.ForPrimitive<T>);

    public static IEnumerable<PocoNode> FromAnyList(IEnumerable<object> values) => 
        values.Select(v => v as PocoNode ?? ForAnyPrimitive(v));
}

public record PrimitiveNode(PrimitiveType Primitive, string? Name = null) : PocoNode(Primitive, null, null, Name)
{
    protected override object? ValueInternal => Primitive.ToITypedElementValue();
    internal object? Value => ValueInternal;
    
    internal static PrimitiveType InferFromValue(object value) => value switch
    {
        Types.DateTime dt => new FhirDateTime(dt),
        Types.Date d => new Date(d),
        Types.Time t => new Time(t),
        decimal dec => new FhirDecimal(dec),
        float f => new FhirDecimal((decimal)f),
        double d => new FhirDecimal((decimal)d),
        bool b => new FhirBoolean(b),
        int i => new Integer(i),
        long l => new Integer64(l),
        string s => new FhirString(s),
        _ => throw new ArgumentException("Cannot infer primitive type from value", nameof(value))
    };
    
    protected override string? TextInternal => Primitive.ToString();
}

internal record PrimitiveListNode(IReadOnlyList<PrimitiveType> Primitives, string? Name = null) : PocoListNode(Primitives, null, Name ?? "value")
{
    public override IEnumerator<PocoNode> GetEnumerator() =>
        Primitives.Select((primitive, index) => new PrimitiveNode(primitive, Name) { Index = index }).GetEnumerator();

    internal IEnumerable<object?> Values => Primitives.Select(p => p.ObjectValue);
}