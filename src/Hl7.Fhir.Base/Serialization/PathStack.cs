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

        public void EnterResource(string name)
        {
            _tops.Push(_path.Length);
            _path += RESOURCEPREFIX + name;
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
                _path = _path.Substring(0, top);
        }

        public void EnterElement(string name)
        {
            bool empty = !_tops.Any();
            _tops.Push(_path.Length);
            _path += empty ? name : $".{name}";
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
                _path = _path.Substring(0, top);
        }

        /// <summary>
        /// Return the path. Note: in contained resources, this is just the path within the contained resource.
        /// </summary>
        public string GetPath()
        {
            if (_tops.Count == 0) return "$this";

            var index = _path.LastIndexOf(RESOURCEPREFIX);
            return index != -1 ? _path.Substring(index + 1) : _path;
        }
    }
}

#nullable restore