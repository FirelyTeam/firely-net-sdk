/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
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
    internal struct SerializationInfoNavigator
    {
        public readonly IModelMetadataProvider Provider;
        public readonly IElementSerializationInfo[] Elements;
        public readonly IElementSerializationInfo Current;
        public readonly string TypeSuffix;

        public string DefinedName => Current?.ElementName;
        public string TypeName => TypeSuffix ?? (Current.Type.Length == 1 ? Current.Type[0].TypeName : null);

        public static SerializationInfoNavigator ForRoot(IComplexTypeSerializationInfo rootType, IModelMetadataProvider provider)
        {
            if (rootType == null) throw new ArgumentNullException(nameof(rootType));
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var rootElement = new ElementSerializationInfo(rootType.TypeName, false, new[] { rootType });
            return new SerializationInfoNavigator(new[] { rootElement }, provider, rootElement);
        }

        public static SerializationInfoNavigator ForElement(IEnumerable<IElementSerializationInfo> elements, IModelMetadataProvider provider,
            IElementSerializationInfo current = null, string suffix = null) => new SerializationInfoNavigator(elements, provider, current, suffix);

        public static SerializationInfoNavigator ForType(IComplexTypeSerializationInfo type, IModelMetadataProvider provider)
            => new SerializationInfoNavigator(type.GetChildren(), provider);

        public static SerializationInfoNavigator Empty() => new SerializationInfoNavigator(null, null);

        private SerializationInfoNavigator(IEnumerable<IElementSerializationInfo> elements, IModelMetadataProvider provider,
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

        public SerializationInfoNavigator MoveTo(string name)

        {
            if (IsEmpty) return this;        // nowhere to move -> just return my empty self

            var found = Elements.FirstOrDefault(e => e.IsChoiceElement && name.StartsWith(e.ElementName) || name == e.ElementName);
            string typeSuffix = null;

            if (found != null)
            {
                typeSuffix = found.IsChoiceElement ? name.Substring(found.ElementName.Length) : null;
                if (found.IsChoiceElement && String.IsNullOrEmpty(typeSuffix))
                    throw new FormatException($"Found a choice element ('{found.ElementName}') without a type indication in the name");
            }

            return ForElement(this.Elements, this.Provider, found, typeSuffix);
        }

        public bool IsTracking => Current != null;

        public SerializationInfoNavigator Down()
        {
            if (!IsTracking) return SerializationInfoNavigator.Empty();

            IComplexTypeSerializationInfo childType = null;

            if (Current.IsChoiceElement)
            {
                if (TypeSuffix != null)
                    childType = Provider.GetSerializationInfoForType(TypeSuffix);
                else
                    ; // barf
            }
            else if (Current.Type.Single() is ITypeReference tr)
                childType = Provider.GetSerializationInfoForType(Current.Type.Single().TypeName);
            else
                childType = (IComplexTypeSerializationInfo)Current.Type.Single();

            return ForType(childType, Provider);
        }
    }
}
