/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Model
{
    // [WMR 20160615]
    // Motivation: we want to enable polymorphism against subclasses of Primitive<T>, especially the Value property
    // However subclasses of Primitive<T> don't implement Value in a consistent way
    // e.g. Code<T> is derived from Primitive<T> but exposes a Value property of type T?
    // => As workaround, subclasses can implement a common IValue<T> interface

    /// <summary>Common generic Value property interface.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    public interface IValue<T>
    {
        /// <summary>Gets or sets the value</summary>
        T Value { get; set; }
    }

    /// <summary>Common generic nullable value property interface.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    public interface INullableValue<T> : IValue<T?> where T : struct { }
}
