/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Serialization
{
    public class ElementSerializationInfo : IElementSerializationInfo
    {
        private ElementSerializationInfo() { }

        public ElementSerializationInfo(string elementName, bool mayRepeat, bool isChoice, bool isContained, bool isSimple, ITypeSerializationInfo[] type, int order)
        {
            ElementName = elementName ?? throw new ArgumentNullException(nameof(elementName));
            MayRepeat = mayRepeat;
            IsChoiceElement = isChoice;
            IsContainedResource = isContained;
            IsAtomicValue = isSimple;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Order = order;
        }

        public ElementSerializationInfo(IElementSerializationInfo source)
        {
            ElementName = source.ElementName;
            MayRepeat = source.MayRepeat;
            IsChoiceElement = source.IsChoiceElement;
            IsContainedResource = source.IsContainedResource;
            IsAtomicValue = source.IsAtomicValue;
            Type = source.Type;
            Order = source.Order;
        }

        public static ElementSerializationInfo ForRoot(string rootName, ITypeSerializationInfo rootType) =>
            new ElementSerializationInfo(rootName, false, false, false, false, new[] { rootType }, 0);

        public string ElementName { get; private set; }

        public bool MayRepeat { get; private set; }

        public bool IsChoiceElement { get; private set; }
        public bool IsContainedResource { get; private set; }

        public bool IsAtomicValue { get; private set; }

        public int Order { get; private set; }
        public ITypeSerializationInfo[] Type { get; private set; }
    }


    public static class ElementSerializationInfoExtensions
    {
        public static ElementSerializationInfo GetSerializationInfo(this IAnnotated ann) =>
            ann.TryGetAnnotation<ElementSerializationInfo>(out var rt) ? rt : null;

        public static ElementSerializationInfo GetSerializationInfo(this ISourceNavigator navigator) =>
            navigator is IAnnotated ia ? ia.GetSerializationInfo() : null;

        public static ElementSerializationInfo GetSerializationInfo(this IElementNavigator navigator) =>
            navigator is IAnnotated ia ? ia.GetSerializationInfo() : null;
    }
}
