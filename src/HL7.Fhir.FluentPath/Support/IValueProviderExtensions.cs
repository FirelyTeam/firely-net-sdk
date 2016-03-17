using System;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.Support
{
    /// <summary>Extension methods for the <see cref="IValueProvider"/> interface.</summary>
    public static class IValueProviderExtensions
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