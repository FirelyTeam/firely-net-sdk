/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization
{
    public class ParserSettings
    {
        public static readonly ParserSettings Default = new ParserSettings() { AcceptUnknownMembers = false, AllowUnrecognizedEnums = false, DisallowXsiAttributesOnRoot = true };

        public bool AcceptUnknownMembers { get; set; }

        public bool DisallowXsiAttributesOnRoot { get; set; }

        public bool AllowUnrecognizedEnums { get; set; }

        public ISerializerCustomization CustomSerializer { get; set; }

        public IDeserializerCustomization CustomDeserializer { get; set; }
    }

    public interface ISerializerCustomization
    {
        [Obsolete("The parameter and type IFhirWriter will be replaced with a more flexible solution in the next version of the library. Use at your own peril.")]
        void OnBeforeSerializeComplexType(object instance, IFhirWriter writer);

        [Obsolete("The parameter and type IFhirWriter will be replaced with a more flexible solution in the next version of the library. Use at your own peril.")]
        bool OnBeforeSerializeProperty(string name, object value, IFhirWriter writer);

        [Obsolete("The parameter and type IFhirWriter will be replaced with a more flexible solution in the next version of the library. Use at your own peril.")]
        void OnAfterSerializeComplexType(object instance, IFhirWriter writer);
    }

    public interface IDeserializerCustomization
    {
        bool OnBeforeDeserializeProperty(string name, Base parent, IElementNavigator current);
    }
}
