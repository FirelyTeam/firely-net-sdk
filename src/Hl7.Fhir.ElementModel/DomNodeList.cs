/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class DomNodeList<T> : IEnumerable<T> where T : DomNode<T>
    {
        private readonly IList<T> _wrapped;

        internal DomNodeList(IEnumerable<T> nodes)
        {
            _wrapped = nodes.ToList();
        }

        public T this[int index] => _wrapped[index];

        public DomNodeList<T> this[string name] =>
            new DomNodeList<T>(_wrapped.SelectMany(c => c.ChildrenInternal(name)));

        public int Count => _wrapped.Count;

        public bool Contains(T item) => _wrapped.Contains(item);

        public IEnumerator<T> GetEnumerator() => _wrapped.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }
}
