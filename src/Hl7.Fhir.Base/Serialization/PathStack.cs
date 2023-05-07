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

        private readonly Stack<int> _tops = new();

        // Each entry is a component of the full path (built up as it navigates through - so is the value passed in from the model not a new string
        private readonly Stack<string> _paths = new();
        // the index of the specific path in the stack (where a property is indexed)
        private readonly Stack<int> _indexer = new();

        public void EnterResource(string name)
        {
            _tops.Push(_path.Length);
            _path += RESOURCEPREFIX + name;

            if (!_paths.Any())
                _paths.Push(name);
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
            }

            if (_paths.Count() == 1)
                _paths.Pop();
        }

        public void EnterElement(string name, int? index, bool isPrimitive)
        {
            bool empty = !_tops.Any();
            _tops.Push(_path.Length);
            _indexer.Push(index ?? 0);
            _path += empty ? name : $".{name}";
            if (!isPrimitive)
            {
                if (index.HasValue)
                    _paths.Push($"{name}[{index}]");
                else
                    _paths.Push(name);
            }
            else
            {
                if (name == "value")
                    _paths.Push(String.Empty);
                else
                    _paths.Push(name);
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
                _path = _path.Substring(0, top);
            }

            _paths.Pop();
        }

        public void IncrementIndex(int items = 1)
        {
            var prevVal = _paths.Pop();
            var name = prevVal.Substring(0, prevVal.IndexOf('['));

            var val = _indexer.Pop() + items;
            _indexer.Push(val);

            _paths.Push($"{name}[{val}]");
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
            return String.Join(".", _paths.Reverse().Where(v => !string.IsNullOrEmpty(v)));
        }
    }
}

#nullable restore