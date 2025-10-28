using Hl7.Fhir.Model;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath;

internal abstract class CqlPrimitive : DynamicPrimitive;

internal class FPTime : CqlPrimitive
{
    public FPTime(P.Time value)
    {
        DynamicTypeName = "System.Time";
        JsonValue = value;
    }
}

internal class FPDateTime : CqlPrimitive
{
    public FPDateTime(P.DateTime value)
    {
        DynamicTypeName = "System.DateTime";
        JsonValue = value;
    }
}

internal class FPDate : CqlPrimitive
{
    public FPDate(P.Date value)
    {
        DynamicTypeName = "System.Date";
        JsonValue = value;
    }
}

internal class FPBoolean : CqlPrimitive
{
    public FPBoolean(bool value)
    {
        DynamicTypeName = "System.Boolean";
        JsonValue = value;
    }
}

internal class FPInteger : CqlPrimitive
{
    public FPInteger(int value)
    {
        DynamicTypeName = "System.Integer";
        JsonValue = value;
    }
}

internal class FPLong : CqlPrimitive
{
    public FPLong(long value)
    {
        DynamicTypeName = "System.Long";
        JsonValue = value;
    }
}

internal class FPDecimal : CqlPrimitive
{
    public FPDecimal(decimal value)
    {
        DynamicTypeName = "System.Decimal";
        JsonValue = value;
    }
}

internal class FPString : CqlPrimitive
{
    public FPString(string value)
    {
        DynamicTypeName = "System.String";
        JsonValue = value;
    }
}

internal class FPQuantity : CqlPrimitive
{
    public FPQuantity(P.Quantity value)
    {
        DynamicTypeName = "System.Quantity";
        JsonValue = value;
    }
}