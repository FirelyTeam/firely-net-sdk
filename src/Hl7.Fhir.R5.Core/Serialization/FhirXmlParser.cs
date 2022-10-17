/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Xml;
using Tasks = System.Threading.Tasks;


namespace Hl7.Fhir.Serialization
{
    public class FhirXmlParser : BaseFhirParser
    {
        public FhirXmlParser(ParserSettings settings=null) : base(settings)
        {
            //
        }

        /// <inheritdoc cref="ParseAsync{T}(XmlReader)" />
        public T Parse<T>(XmlReader reader) where T : Base => (T)Parse(reader, typeof(T));
        
        public async Tasks.Task<T> ParseAsync<T>(XmlReader reader) where T : Base 
            => await ParseAsync(reader, typeof(T)).ConfigureAwait(false) as T;

        /// <inheritdoc cref="ParseAsync{T}(string)" />
        public T Parse<T>(string xml) where T : Base => (T)Parse(xml, typeof(T));
        
        public async Tasks.Task<T> ParseAsync<T>(string xml) where T : Base 
            => await ParseAsync(xml, typeof(T)).ConfigureAwait(false) as T;
        
        private static FhirXmlParsingSettings buildNodeSettings(ParserSettings settings) =>
                new FhirXmlParsingSettings
                {
                    DisallowSchemaLocation = settings.DisallowXsiAttributesOnRoot,
                    PermissiveParsing = settings.PermissiveParsing
                };

        /// <inheritdoc cref="ParseAsync(string, Type)" />
        public Base Parse(string xml, Type dataType = null)
        {
            var xmlReader = FhirXmlNode.Parse(xml, buildNodeSettings(Settings));
            return Parse(xmlReader, dataType);
        }
        
        public async Tasks.Task<Base> ParseAsync(string xml, Type dataType = null)
        {
            var xmlReader = await FhirXmlNode.ParseAsync(xml, buildNodeSettings(Settings)).ConfigureAwait(false);
            return Parse(xmlReader, dataType);
        }

        /// <inheritdoc cref="ParseAsync(XmlReader, Type)" />
        public Base Parse(XmlReader reader, Type dataType = null)
        {
            var xmlReader = FhirXmlNode.Read(reader, buildNodeSettings(Settings));
            return Parse(xmlReader, dataType);
        }

        public async Tasks.Task<Base> ParseAsync(XmlReader reader, Type dataType = null)
        {
            var xmlReader = await FhirXmlNode.ReadAsync(reader, buildNodeSettings(Settings)).ConfigureAwait(false);
            return Parse(xmlReader, dataType);
        }
    }

}
