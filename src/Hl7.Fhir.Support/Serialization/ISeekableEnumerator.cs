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
    }
}
