using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Common value interface. Represents a strongly typed value.</summary>
    public interface IValue
    {
        /// <summary>
        /// Returns the type of the value.
        /// You can access the value via the <see cref="IValue{T}"/> interface, where T is the returned value type.
        /// </summary>
        Type ValueType { get; }
    }

    /// <summary>Common generic interface for a strongly typed value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    public interface IValue<out T> : IValue
    {
        /// <summary>Gets a value of type <typeparamref name="T"/>.</summary>
        T Value { get; }

        // Suggestion: implement cast operators from/to T
    }

    /// <summary>Extension methods for the <see cref="IValue"/> interface.</summary>
    public static class ValueExtensions
    {
        /// <summary>Retrieves the value of type <typeparamref name="T"/>, if supported by the instance, or <c>default(T)</c> otherwise.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValue"/> instance.</param>
        /// <returns>A value of type <typeparamref name="T"/>.</returns>
        public static T GetValue<T>(this IValue instance)
        {
            return instance.GetValueOrDefault(default(T));
        }

        /// <summary>Retrieves the value of type <typeparamref name="T"/>, if supported by the instance, or the specified <paramref name="defaultValue"/> otherwise.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValue"/> instance.</param>
        /// <param name="defaultValue">A default value to return if the current instance does not support values of type <typeparamref name="T"/>.</param>
        /// <returns>A value of type <typeparamref name="T"/>.</returns>
        public static T GetValueOrDefault<T>(this IValue instance, T defaultValue)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); } // nameof(instance)
            var v = instance as IValue<T>;
            return v != null ? v.Value : defaultValue;
        }

        /// <summary>Try to retrieve a value of type <typeparamref name="T"/> from the current instance, if supported.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValue"/> instance.</param>
        /// <param name="value">Output parameter that receives the instance value, if supported, or <c>default(T)</c> otherwise.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool TryGetValue<T>(this IValue instance, out T value)
        {
            var v = instance as IValue<T>;
            var result = v != null;
            value = result ? v.Value : default(T);
            return result;
        }
    }
}
