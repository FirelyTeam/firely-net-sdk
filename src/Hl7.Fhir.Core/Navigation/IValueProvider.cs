using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    // Motivation:
    // FHIR supports a value on each node - not only on leafs, but also on inner nodes!
    // This concept is not to be confused with XML inner value; in XML representation, FHIR values are represented as attributes

    // [WMR 20151203]
    // Possible improvement: wrap the value in a generic struct
    // This allows an instance to dynamically switch value type
    // http://codeblog.jonskeet.uk/2013/06/22/array-covariance-not-just-ugly-but-slow-too/

    /// <summary>Common interface for objects that provide a strongly typed value.</summary>
    public interface IValueProvider
    {
        /// <summary>
        /// Returns the type of the provided value.
        /// You can access the provided value via the <see cref="IValueProvider{T}"/> interface, where T is the value type.
        /// </summary>
        Type ValueType { get; }
    }

    /// <summary>Common generic interface for objects that provide an immutable strongly typed value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    public interface IValueProvider<out T> : IValueProvider
    {
        /// <summary>Provides a value of type <typeparamref name="T"/>.</summary>
        T Value { get; }

        // Suggestion: implement cast operators from/to T
    }

    /// <summary>Common generic interface for objects that provide a mutable strongly typed value.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    public interface IMutableValueProvider<T> : IValueProvider
    {
        /// <summary>Gets or sets a value of type <typeparamref name="T"/>.</summary>
        T Value { get; set; }
    }

    /// <summary>Extension methods for the <see cref="IValueProvider"/> interface.</summary>
    public static class ValueProviderExtensions
    {
        /// <summary>Retrieves a value of type <typeparamref name="T"/>, if provided by the instance, or <c>default(T)</c> otherwise.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <returns>A value of type <typeparamref name="T"/>.</returns>
        public static T GetValue<T>(this IValueProvider instance)
        {
            return instance.GetValueOrDefault(default(T));
        }

        /// <summary>Retrieves the value of type <typeparamref name="T"/>, if provided by the isntance, or the specified <paramref name="defaultValue"/> otherwise.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <param name="defaultValue">A default value to return in case the current instance does not provide a value of type <typeparamref name="T"/>.</param>
        /// <returns>A value of type <typeparamref name="T"/>.</returns>
        public static T GetValueOrDefault<T>(this IValueProvider instance, T defaultValue)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); } // nameof(instance)
            var v = instance as IValueProvider<T>;
            return v != null ? v.Value : defaultValue;
        }

        /// <summary>Try to retrieve a value of type <typeparamref name="T"/>, if provided by the instance.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="instance">An <see cref="IValueProvider"/> instance.</param>
        /// <param name="value">Output parameter that receives the provided value or <c>default(T)</c>.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public static bool TryGetValue<T>(this IValueProvider instance, out T value)
        {
            var v = instance as IValueProvider<T>;
            var result = v != null;
            value = result ? v.Value : default(T);
            return result;
        }
    }
}
