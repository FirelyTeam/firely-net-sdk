/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    internal struct SerializationInfoCache
    {
        public readonly IModelMetadataProvider Provider;
        public readonly IElementSerializationInfo[] Elements;
        public readonly IElementSerializationInfo Current;
        public readonly string TypeSuffix;

        public string DefinedName => Current?.ElementName;
        public string TypeName => TypeSuffix ?? (Current.Type.Length == 1 ? Current.Type[0].TypeName : null);

        public static SerializationInfoCache ForRoot(IComplexTypeSerializationInfo rootType, string rootName, IModelMetadataProvider provider)
        {
            if (rootType == null) throw new ArgumentNullException(nameof(rootType));
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var rootElement = new ElementSerializationInfo(rootName, false, false, new[] { rootType });
            return new SerializationInfoCache(new[] { rootElement }, provider, rootElement);
        }

        public static SerializationInfoCache ForType(IComplexTypeSerializationInfo type, IModelMetadataProvider provider)
            => new SerializationInfoCache(type.GetChildren().ToArray(), provider);

        public static SerializationInfoCache Empty = new SerializationInfoCache(null, null);

        private SerializationInfoCache(IElementSerializationInfo[] elements, IModelMetadataProvider provider,
            IElementSerializationInfo current = null, string suffix = null)
        {
            if (elements == null)
                Elements = new IElementSerializationInfo[0];
            else
                Elements = elements.ToArray();

            Current = current;
            TypeSuffix = suffix;
            Provider = provider;
        }

        public bool IsEmpty => !Elements.Any();

        public SerializationInfoCache MoveTo(string name)
        {
            if (IsEmpty) return this;        // nowhere to move -> just return my empty self

            var found = Elements.FirstOrDefault(e => e.IsChoiceElement && name.StartsWith(e.ElementName) || name == e.ElementName);
            string typeSuffix = null;

            if (found != null && found.IsChoiceElement)
            {
                var suffix = name.Substring(found.ElementName.Length);
                if (String.IsNullOrEmpty(suffix)) throw new FormatException($"Choice element '{found.ElementName}' is not suffixed with a type.");

                typeSuffix = found.Type.Select(t => t.TypeName).FirstOrDefault(t => String.Compare(t, suffix, StringComparison.OrdinalIgnoreCase) == 0);
                if (String.IsNullOrEmpty(typeSuffix)) throw new FormatException($"Choice element is not suffixed incorrect type '{suffix}'");
            }

            return new SerializationInfoCache(this.Elements, this.Provider, found, typeSuffix);
        }

        public bool IsTracking => Current != null;
    }
}
