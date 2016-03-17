/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Support
{  
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

}
