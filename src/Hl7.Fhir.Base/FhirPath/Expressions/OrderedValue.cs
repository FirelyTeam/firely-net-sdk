/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.FhirPath.Expressions
{
    internal class OrderedValue : ITypedElement
    {
        public bool Descending;
        public ITypedElement value;

        public string Location => value.Location;

        public IElementDefinitionSummary Definition => value.Definition;

        public string Name => value.Name;

        public string InstanceType => value.InstanceType;

        public object Value => value.Value;

        public IEnumerable<ITypedElement> Children(string name = null) => value.Children(name);
    }
}
