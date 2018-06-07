/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    internal struct SerializationInfoCache
    {
        public readonly Dictionary<string, IElementSerializationInfo> Elements;

        public static SerializationInfoCache ForType(IComplexTypeSerializationInfo type)
            => new SerializationInfoCache(type.GetChildren().ToDictionary(c => c.ElementName));

        public static SerializationInfoCache Empty = new SerializationInfoCache(null);
        public readonly bool IsEmpty;

        public static SerializationInfoCache ForRoot(IElementSerializationInfo rootInfo)
        {
            if (rootInfo == null) throw new ArgumentNullException(nameof(rootInfo));

            return new SerializationInfoCache(new Dictionary<string, IElementSerializationInfo>
                { { rootInfo.ElementName, rootInfo } });
        }

        private SerializationInfoCache(Dictionary<string, IElementSerializationInfo> elements)
        {
            if (elements == null)
                Elements = new Dictionary<string, IElementSerializationInfo>();
            else
                Elements = elements;

            IsEmpty = !Elements.Any();
        }

        public bool Find(string elementName, out IElementSerializationInfo found, out string instanceType)
        {
            found = null;
            instanceType = null;

            if (IsEmpty) return false;        // nowhere to move -> just return my empty self

            if (!Elements.TryGetValue(elementName, out found))
                found = Elements.Values.FirstOrDefault(e => e.IsChoiceElement && elementName.StartsWith(e.ElementName));

            if (found != null)
            {
                if (found.IsChoiceElement)
                {
                    var suffix = elementName.Substring(found.ElementName.Length);
                    if (String.IsNullOrEmpty(suffix)) throw new FormatException($"Choice element '{found.ElementName}' is not suffixed with a type.");

                    instanceType = found.Type.Select(t => t.TypeName).FirstOrDefault(t => String.Compare(t, suffix, StringComparison.OrdinalIgnoreCase) == 0);
                    if (String.IsNullOrEmpty(instanceType)) throw new FormatException($"Choice element is not suffixed incorrect type '{suffix}'");
                }
                else
                {
                    instanceType = found.Type[0].TypeName;
                }

                return true;
            }
            else
                return false;
        }
    }
}
