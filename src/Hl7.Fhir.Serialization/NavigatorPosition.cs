/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    internal class NavigatorPosition<T> where T:class
    {
        public readonly T Node;
        public readonly IElementSerializationInfo SerializationInfo;
        public readonly string Name;
        public readonly string InstanceType;

        public NavigatorPosition(T current, IElementSerializationInfo info, string name, string type)
        {
            SerializationInfo = info;
            Node = current ?? throw Error.ArgumentNull(nameof(current));
            InstanceType = type;
            Name = name ?? throw Error.ArgumentNull(nameof(name));
        }


        public static NavigatorPosition<T> ForRoot(T root, IComplexTypeSerializationInfo rootType, string rootName)
        {
            if (rootName == null) throw Error.ArgumentNull(nameof(rootName));

            var rootElement = rootType != null ? ElementSerializationInfo.ForRoot(rootName, rootType) : null;
            return new NavigatorPosition<T>(root, rootElement, rootName, rootName);
        }

        public bool IsTracking => SerializationInfo != null;       
    }
}
