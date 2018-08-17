/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */
 
using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Represents a stream of resources which is both enumerable and enables the user to return to previous positions.
    /// </summary>
    public interface ISeekableEnumerator<T> : IEnumerator<T>
    {
        string Position { get; }

        bool Seek(string position);

        bool MoveNext(string position);
    }

    /// <summary>
    /// Provides a sequence of <see cref="IElementNavigator"/> instances to efficiently
    /// extract information from a raw FHIR resource file, independent of the
    /// underlying resource serialization format. Also supports resource bundles.
    /// </summary>
    /// <remarks>
    /// Implements <see cref="IEnumerator{T}"/>, but not <see cref="IEnumerable{T}"/>.
    /// Iteration state is tied to the stream instance.
    /// Nested enumeration is NOT supported.
    /// </remarks>
    public interface INavigatorStream : ISeekableEnumerator<IElementNavigator>, IDisposable
    {
        // * Bundle: ResourceType returns "Bundle" and Current.Type returns type of current entry
        // * Others: ResourceType and Current.Type return the same value

        /// <summary>The typename of the underlying resource (container).</summary>
        /// <remarks>Call Current.Type to determine the type of the currently enumerated resource.</remarks>
        string ResourceType { get; }

        // [WMR 20171023] Obsolete, to make INavigatorStream more generic (e.g. allow PoCo input)
        // <summary>The full path of the current resource file, or of the containing resource bundle file.</summary>
        // string Path { get; }

        /// <summary>Returns <c>true</c> if the underlying file represents a Bundle resource, or <c>false</c> otherwise.</summary>
        bool IsBundle { get; }
    }
}
