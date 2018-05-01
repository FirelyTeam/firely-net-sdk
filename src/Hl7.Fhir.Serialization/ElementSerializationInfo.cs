/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using System;

namespace Hl7.Fhir.Serialization
{
    public class ElementSerializationInfo : IElementSerializationInfo
    {
        public ElementSerializationInfo(string elementName, bool mayRepeat, bool isChoice, ITypeSerializationInfo[] type)
        {
            ElementName = elementName ?? throw new ArgumentNullException(nameof(elementName));
            MayRepeat = mayRepeat;
            IsChoiceElement = isChoice;
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public ElementSerializationInfo(IElementSerializationInfo source)
        {
            ElementName = source.ElementName;
            MayRepeat = source.MayRepeat;
            IsChoiceElement = source.IsChoiceElement;
            Type = source.Type;
        }

        public string ElementName { get; private set; }

        public bool MayRepeat { get; private set; }

        public bool IsChoiceElement { get; private set; }
        public ITypeSerializationInfo[] Type { get; private set; }
    }
}
