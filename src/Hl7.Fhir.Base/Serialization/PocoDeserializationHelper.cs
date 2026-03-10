#nullable enable

namespace Hl7.Fhir.Serialization
{
    internal static class PocoDeserializationHelper
    {
        internal static void RunPropertyValidation(ref object? instance, IDeserializationValidator validator, PropertyDeserializationContext context, ExceptionAggregator aggregator)
        {
            validator.ValidateProperty(ref instance, context, out var errors);
            aggregator.Add(errors);
            return;
        }

        internal static void RunInstanceValidation(object? instance, IDeserializationValidator validator, InstanceDeserializationContext context, ExceptionAggregator aggregator)
        {
            validator.ValidateInstance(instance, context, out var errors);
            aggregator.Add(errors);
            return;
        }
    }
}

#nullable restore