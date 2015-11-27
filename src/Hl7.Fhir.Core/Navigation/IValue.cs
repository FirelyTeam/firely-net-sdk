using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    public interface IValue
    {
        /// <summary>
        /// Returns the type of the value.
        /// You can access the value via the <see cref="IValue{T}"/> interface, where T is the returned value type.
        /// </summary>
        Type ValueType { get; }
    }

    /// <summary>Generic interface for a strongly-typed value.</summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface IValue<out TValue> : IValue
    {
        /// <summary>Gets a value of type <typeparamref name="TValue"/>.</summary>
        TValue Value { get; }
    }

    public static class ValueExtensions
    {
        public static TValue GetValue<TValue>(this IValue instance)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); } // nameof(instance)
            var v = instance as IValue<TValue>;
            return v != null ? v.Value : default(TValue);
        }
    }
}
