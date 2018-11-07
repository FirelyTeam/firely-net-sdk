/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    internal class NavigatorPosition 
    {
        public readonly ISourceNode Node;
        public readonly IElementDefinitionSummary SerializationInfo;
        public readonly string Name;
        public readonly string InstanceType;

        public NavigatorPosition(ISourceNode current, IElementDefinitionSummary info, string name, string instanceType)
        {
            SerializationInfo = info;
            Node = current ?? throw Error.ArgumentNull(nameof(current));
            InstanceType = instanceType;
            Name = name ?? throw Error.ArgumentNull(nameof(name));
        }

        public static NavigatorPosition ForRoot(ISourceNode element, IStructureDefinitionSummary elementType, string elementName)
        {
            if (elementName == null) throw Error.ArgumentNull(nameof(elementName));

            var rootElement = elementType != null ? ElementDefinitionSummary.ForRoot(elementName, elementType) : null;
            return new NavigatorPosition(element, rootElement, elementName, elementType?.TypeName);
        }

        public bool IsTracking => SerializationInfo != null;       
    }
}
