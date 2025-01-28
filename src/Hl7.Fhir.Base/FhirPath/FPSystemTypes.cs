using Hl7.Fhir.Model;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath;

internal abstract class CqlPrimitive : DynamicPrimitive;

internal class FPTime : CqlPrimitive
{
    
    public FPTime(P.Time value)
    {
        DynamicTypeName = "System.Time";
        ObjectValue = value;
    }
}

internal class FPDateTime : CqlPrimitive
{
    public FPDateTime(P.DateTime value)
    {
        DynamicTypeName = "System.DateTime";
        ObjectValue = value;
    }
}

internal class FPBoolean : CqlPrimitive
{
    public FPBoolean(P.Boolean value)
    {
        DynamicTypeName = "System.Boolean";
        ObjectValue = value;
    }
}

internal class FPInteger : CqlPrimitive
{
    public FPInteger(int value)
    {
        DynamicTypeName = "System.Integer";
        ObjectValue = value;
    }
}

internal class FPDecimal : CqlPrimitive
{
    public FPDecimal(decimal value)
    {
        DynamicTypeName = "System.Decimal";
        ObjectValue = value;
    }
}

internal class FPString : CqlPrimitive
{
    public FPString(P.String value)
    {
        DynamicTypeName = "System.String";
        ObjectValue = value;
    }
}

internal class FPQuantity : CqlPrimitive
{
    public FPQuantity(P.Quantity value)
    {
        DynamicTypeName = "System.Quantity";
        ObjectValue = value;
    }
}