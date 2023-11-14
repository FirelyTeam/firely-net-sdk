/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Specification;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    internal class ScopedNodeToTypedElementAdapter : ITypedElement
    {
        private readonly IScopedNode _node;

        public ScopedNodeToTypedElementAdapter(IScopedNode node)
        {
            _node = node;
        }

        public string Location => throw new System.NotImplementedException();

        public IElementDefinitionSummary Definition => throw new System.NotImplementedException();

        public string Name => _node.Name;

        public string InstanceType => _node.InstanceType;

        public object Value => _node.Value;

        public IEnumerable<ITypedElement> Children(string? name = null) =>
            _node.Children(name).Select(n => new ScopedNodeToTypedElementAdapter(n));
    }
}

#nullable restore