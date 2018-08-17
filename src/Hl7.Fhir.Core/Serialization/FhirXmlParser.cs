/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Xml;


namespace Hl7.Fhir.Serialization
{
    public class FhirXmlParser : BaseFhirParser
    {
        public FhirXmlParser(ParserSettings settings=null) : base(settings)
        {
            //
        }

        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));

        private FhirXmlNodeSettings buildNodeSettings(ParserSettings settings) =>
                new FhirXmlNodeSettings
                {
                    DisallowSchemaLocation = Settings.DisallowXsiAttributesOnRoot,
                };

        public Base Parse(string xml, Type dataType)
        {
            var xmlReader = FhirXmlNode.Parse(xml, buildNodeSettings(Settings));
            return Parse(xmlReader, dataType);
        }

        public Base Parse(XmlReader reader, Type dataType)
        {
            var xmlReader = FhirXmlNode.Read(reader, buildNodeSettings(Settings));
            return Parse(xmlReader, dataType);
        }
    }

}
