/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Mapping
{
    //TODO: Replace by some real code that enumerates the underlying IEnumerable
    //lazily as far as necessary and stores the already enumerated elements to return
    //whenever a GetEnumerator is done again.
    public class LazyEnumerable<T> : IEnumerable<T>, IEnumerable
    {
        private readonly IEnumerable<T> _source;
        private IEnumerable<T> _cached;

        public LazyEnumerable(IEnumerable<T> source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_cached == null)
                _cached = _source.ToArray();

            return _cached.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
