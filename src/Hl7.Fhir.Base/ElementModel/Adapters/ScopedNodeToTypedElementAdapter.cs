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
    /// <summary>
    /// An adapter from <see cref="IScopedNode"/> to <see cref="ITypedElement"/>.
    /// </summary>
    /// <remarks>Be careful, this adapter does not implement the <see cref="ITypedElement.Location"/> and 
    /// <see cref="ITypedElement.Definition"/> property.
    /// </remarks>
    internal class ScopedNodeToTypedElementAdapter : ITypedElement
    {
        private readonly IScopedNode _adaptee;

        public ScopedNodeToTypedElementAdapter(IScopedNode adaptee)
        {
            _adaptee = adaptee;
        }

        public string Location => throw new System.NotImplementedException();

        public IElementDefinitionSummary Definition => throw new System.NotImplementedException();

        public string Name => _adaptee.Name;

        public string InstanceType => _adaptee.InstanceType;

        public object Value => _adaptee.Value;

        public IEnumerable<ITypedElement> Children(string? name = null) =>
            _adaptee.Children(name).Select(n => new ScopedNodeToTypedElementAdapter(n));
    }
}

#nullable restore