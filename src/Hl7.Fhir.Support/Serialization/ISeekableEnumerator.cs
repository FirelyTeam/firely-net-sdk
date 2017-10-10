/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */
 
using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public interface INavigatorStream : ISeekableEnumerator<IElementNavigator>, IDisposable
    {

    }
}
