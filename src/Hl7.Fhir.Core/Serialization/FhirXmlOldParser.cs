/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Xml;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlOldParser : BaseFhirParser
    {
        public FhirXmlOldParser(Model.Version version) : base(version)
        {}

        public FhirXmlOldParser(ParserSettings settings) : base(settings)
        {}

        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));
        
        private FhirXmlParsingSettings buildNodeSettings() =>
            new FhirXmlParsingSettings
            {
                DisallowSchemaLocation = Settings.DisallowXsiAttributesOnRoot,
                PermissiveParsing = Settings.PermissiveParsing
            };

        public Base Parse(string xml, Type dataType = null)
        {
            var xmlReader = FhirXmlNode.Parse(xml, buildNodeSettings());
            return Parse(xmlReader, dataType);
        }

        public Base Parse(XmlReader reader, Type dataType = null)
        {
            var xmlReader = FhirXmlNode.Read(reader, buildNodeSettings());
            return Parse(xmlReader, dataType);
        }
    }

}
