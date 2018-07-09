/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    internal class NavigatorPosition 
    {
        public readonly IElementNavigator Node;
        public readonly IElementSerializationInfo SerializationInfo;
        public readonly string Name;
        public readonly string InstanceType;

        public NavigatorPosition(IElementNavigator current, IElementSerializationInfo info, string name, string type)
        {
            SerializationInfo = info;
            Node = current ?? throw Error.ArgumentNull(nameof(current));
            InstanceType = type;
            Name = name ?? throw Error.ArgumentNull(nameof(name));
        }

        public static NavigatorPosition ForElement(IElementNavigator element, IComplexTypeSerializationInfo elementType, string elementName)
        {
            if (elementName == null) throw Error.ArgumentNull(nameof(elementName));

            var rootElement = elementType != null ? ElementSerializationInfo.ForRoot(elementName, elementType) : null;
            return new NavigatorPosition(element, rootElement, elementName, elementName);
        }

        public bool IsTracking => SerializationInfo != null;       
    }
}
