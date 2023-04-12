/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.IO;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlParser : BaseFhirParser
    {
        public FhirXmlParser(Model.Version version) : base(version)
        {}

        public FhirXmlParser(ParserSettings settings) : base(settings)
        {}

        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));

        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));
        
        public Base Parse(string xml, Type dataType = null)
        {
            var xmlReader = XmlReader.Create(new StringReader(SerializationUtil.SanitizeXml(xml)));
            return Parse(xmlReader, dataType);
        }

        public Base Parse(XmlReader reader, Type dataType = null)
        {
            var origin = new XmlParserOrigin(SerializationUtil.WrapXmlReader(reader), Settings.DisallowXsiAttributesOnRoot);
            var source = new ParserSource(origin, Settings);
            try
            {
                return source.GetRoot(dataType);
            }
            catch (SourceException sourceException)
            {
                throw sourceException.ToFormatException();
            }
        }
    }

}
