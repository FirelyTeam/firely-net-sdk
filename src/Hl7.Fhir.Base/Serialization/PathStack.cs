/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Tracks the position within an instance as a dotted path. Used in diagnostics for the parser/serializers.
    /// </summary>
    internal class PathStack
    {
        private const char RESOURCEPREFIX = '資';
        private string _path = string.Empty;
        private string _fhirpath = string.Empty;

        private readonly Stack<int> _tops = new();
        private readonly Stack<int> _tops2 = new();
        private readonly Stack<int> _indexer = new();

        public void EnterResource(string name)
        {
            _tops.Push(_path.Length);
            _path += RESOURCEPREFIX + name;

            _tops2.Push(_fhirpath.Length);
            _fhirpath += RESOURCEPREFIX + name;
        }

        public void ExitResource()
        {
            if (_tops.Count == 0)
                throw new InvalidOperationException("No resource or path parts are present on the stack.");

            var top = _tops.Pop();

            if (_path[top] != RESOURCEPREFIX)
            {
                _tops.Push(top);
                throw new InvalidOperationException("Cannot exit a resource while inside a property.");
            }
            else
            {
                _path = _path.Substring(0, top);
                var top2 = _tops2.Pop();
                _fhirpath = _fhirpath.Substring(0, top2);
            }
        }

        public void EnterElement(string name, int? index, bool isPrimitive)
        {
            bool empty = !_tops.Any();
            _tops.Push(_path.Length);
            _indexer.Push(index ?? 0);
            _path += empty ? name : $".{name}";
            if (!isPrimitive)
            {
                _tops2.Push(_fhirpath.Length);
                if (index.HasValue)
                    _fhirpath += empty ? name : $".{name}[{index}]";
                else
                    _fhirpath += empty ? name : $".{name}";
            }
        }

        public void ExitElement()
        {
            if (_tops.Count == 0)
                throw new InvalidOperationException("No resource or path parts are present on the stack.");


            var top = _tops.Pop();

            if (_path[top] == RESOURCEPREFIX)
            {
                _tops.Push(top);
                throw new InvalidOperationException("Cannot exit a property while inside a resource.");
            }
            else
            {
                _indexer.Pop();
                if (_tops2.Count == _tops.Count + 1) // primitives don't add
                {
                    var top2 = _tops2.Pop();
                    _fhirpath = _fhirpath.Substring(0, top2);
                }
                _path = _path.Substring(0, top);
            }
        }

        public void IncrementIndex()
        {
            var val = _indexer.Pop()+1;
            _indexer.Push(val);
            _fhirpath = _fhirpath.Substring(0, _fhirpath.LastIndexOf('[') + 1) + val + "]";
        }

        /// <summary>
        /// Return the definition path (has no indexes). Note: in contained resources, this is just the path within the contained resource.
        /// </summary>
        public string GetPath()
        {
            if (_tops.Count == 0) return "$this";

            var index = _path.LastIndexOf(RESOURCEPREFIX);
            return index != -1 ? _path.Substring(index + 1) : _path;
        }

        /// <summary>
        /// Return the fhirpath that includes the indexes. Note: in contained resources, this is just the path within the contained resource.
        /// </summary>
        public string GetInstancePath()
        {
            if (_tops.Count == 0) return "$this";

            var index = _fhirpath.LastIndexOf(RESOURCEPREFIX);
            return index != -1 ? _fhirpath.Substring(index + 1) : _fhirpath;
        }
    }
}

#nullable restore