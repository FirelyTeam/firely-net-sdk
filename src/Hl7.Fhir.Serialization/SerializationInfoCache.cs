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
    /// <summary>
    /// Internal class to optimize access to a list of children, basically a dictionary of
    /// (element name, IElementSerializationInfo) pairs, optimized for quick access.
    /// </summary>
    internal class SerializationInfoCache : Dictionary<string, IElementSerializationInfo>
    {
        public static SerializationInfoCache ForType(IComplexTypeSerializationInfo type)
            => new SerializationInfoCache(type.GetChildren().ToDictionary(c => c.ElementName));

        public static SerializationInfoCache Empty = new SerializationInfoCache();

        public static SerializationInfoCache ForRoot(IElementSerializationInfo rootInfo)
        {
            if (rootInfo == null) throw new ArgumentNullException(nameof(rootInfo));

            return new SerializationInfoCache(new Dictionary<string, IElementSerializationInfo>
                { { rootInfo.ElementName, rootInfo } });
        }

        public bool TryGetBySuffixedName(string name, out IElementSerializationInfo info)
        {
            // Simplest case, one on one match between name and element name
            if (TryGetValue(name, out info))
                return true;

            info = this.Where(kvp => name.StartsWith(kvp.Key)).Select(kvp => kvp.Value).FirstOrDefault();

            // False hit -> we matched the prefix, but the property is not actually
            // a choice element (simply misspelled).
            if (info != null && !info.IsChoiceElement && name != info.ElementName)
                info = null;

            return info != null;
        }

        private SerializationInfoCache() : base(new Dictionary<string, IElementSerializationInfo>())
        {
        }

        private SerializationInfoCache(IDictionary<string, IElementSerializationInfo> elements) : base(elements)
        {
        }
    }
}
