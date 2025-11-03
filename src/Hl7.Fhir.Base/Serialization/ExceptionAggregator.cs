/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System.Collections;
using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This class is used by the deserializers to collect errors while performing deserialization.
    /// </summary>
    /// <remarks>Is probably going to be used by the (future)XmlDynamicDeserializer too.</remarks>
    internal class ExceptionAggregator : IEnumerable<CodedException>
    {
        public List<CodedException> _aggregated = new();

        public void Add(CodedException? e)
        {
            if (e is not null)
                _aggregated.Add(e);
        }

        public void Add(IEnumerable<CodedException>? es)
        {
            if (es is not null)
                _aggregated.AddRange(es);
        }

        public bool HasExceptions => _aggregated.Count > 0;

        public int Count => _aggregated.Count;

        public IEnumerator<CodedException> GetEnumerator() => ((IEnumerable<CodedException>)_aggregated).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_aggregated).GetEnumerator();
    }


}

#nullable restore