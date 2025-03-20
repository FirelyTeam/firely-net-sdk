#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization;

internal static class PocoDeserializationHelper
{
    internal static void RunPropertyValidation(object? propertyValue, IPocoValidator validator, PropertyDeserializationContext context, ExceptionAggregator aggregator)
    {
        validator.ValidateProperty(propertyValue, context, out var errors);
        aggregator.Add(errors);
    }

    internal static void RunInstanceValidation(Base instance, IPocoValidator validator, InstanceDeserializationContext context, ExceptionAggregator aggregator)
    {
        validator.ValidateInstance(instance, context, out var errors);
        aggregator.Add(errors);
    }
}