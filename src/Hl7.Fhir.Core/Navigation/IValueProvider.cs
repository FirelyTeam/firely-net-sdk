/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Navigation
{
    // Motivation:
    // FHIR supports a value on each node - not only on leafs, but also on inner nodes.
    // This concept of a node value is not to be confused with the XML inner value.
    // In XML representation, FHIR values are represented as attributes.
    
    /// <summary>Common interface for an object that exposes a strongly typed value.</summary>
    public interface IValueProvider
    {
        /// <summary>Returns the type of the value exposed by the current instance.</summary>
        Type ValueType { get; }

        /// <summary>Gets the instance value as an <see cref="object"/>.</summary>
        object ObjectValue { get; }
    }

    /// <summary>Common interface for an object that exposes a strongly typed mutable value.</summary>
    public interface IMutableValueProvider : IValueProvider
    {
        // object ObjectValue { get; set; }
    }

    /// <summary>Common generic interface for an object that exposes an strongly typed immutable value.</summary>
    /// <typeparam name="V">The value type.</typeparam>
    public interface IValueProvider<out V> : IValueProvider
    {
        /// <summary>Gets a value of type <typeparamref name="V"/>.</summary>
        V Value { get; }

        // Suggestion: implement cast operators from/to T
    }

    /// <summary>Common generic interface for an object that exposes a strongly typed mutable value.</summary>
    /// <typeparam name="V">The value type.</typeparam>
    public interface IMutableValueProvider<V> : IMutableValueProvider
    {
        /// <summary>Gets or sets a value of type <typeparamref name="V"/>.</summary>
        V Value { get; set; }
    }

    /// <summary>Extension methods for the <see cref="IValueProvider"/> interface.</summary>
    public static class ValueProviderExtensions
    {
        /// <summary>
        /// Retrieves a value of type <typeparamref name="V"/>, if supported by the instance, or <c>default(V)</c> otherwise.
        /// </summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <returns>A value of type <typeparamref name="V"/>.</returns>
        public static V GetValue<V>(this IValueProvider instance)
        {
            return instance.GetValueOrDefault(default(V));
        }

        /// <summary>Returns the instance value as a <typeparamref name="V"/>, if supported, or <c>null</c>.</summary>
        /// <typeparam name="V">The target type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <returns>A value of type <typeparamref name="V"/>.</returns>
        public static V GetValueAs<V>(this IValueProvider instance) where V : class
        {
            return instance.GetValueOrDefault<object>(null) as V;
        }

        /// <summary>Returns the instance value cast to <typeparamref name="V"/>.</summary>
        /// <typeparam name="V">The target type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <returns>A value of type <typeparamref name="V"/>.</returns>
        /// <exception cref="InvalidCastException">The specified cast is invalid.</exception>
        public static V CastValue<V>(this IValueProvider instance) where V : class
        {
            return (V)instance.GetValueOrDefault<object>(null);
        }

        /// <summary>
        /// Retrieves the value of type <typeparamref name="V"/>, if supported by the instance,
        /// or the specified <paramref name="defaultValue"/> otherwise.
        /// </summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <param name="defaultValue">
        /// A default value to return in case the current instance does not expose a value of type <typeparamref name="V"/>.
        /// </param>
        /// <returns>A value of type <typeparamref name="V"/>.</returns>
        public static V GetValueOrDefault<V>(this IValueProvider instance, V defaultValue)
        {
            //if (instance == null) { throw new ArgumentNullException("instance"); } // nameof(instance)
            var v = instance as IValueProvider<V>;
            return v != null ? v.Value : defaultValue;
        }

        /// <summary>Try to retrieve a value of type <typeparamref name="V"/>, if exposed by the instance.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <param name="value">Output parameter that receives the exposed value or <c>default(V)</c>.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool TryGetValue<V>(this IValueProvider instance, out V value)
        {
            //if (instance == null) { throw new ArgumentNullException("instance"); } // nameof(instance)
            var v = instance as IValueProvider<V>;
            var result = v != null;
            value = result ? v.Value : default(V);
            return result;
        }
    }
}
