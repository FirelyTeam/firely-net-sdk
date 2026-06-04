/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial class FhirXmlNode
    {
        /// <inheritdoc cref="ReadAsync(XmlReader, FhirXmlParsingSettings)" />
        public static ISourceNode Read(XmlReader reader, FhirXmlParsingSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            var doc = SerializationUtil.XDocumentFromReader(reader, ignoreComments: false);
            return new FhirXmlNode(doc.Root, settings);
        }
        
        public static async Task<ISourceNode> ReadAsync(XmlReader reader, FhirXmlParsingSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            var doc = await SerializationUtil.XDocumentFromReaderAsync(reader, ignoreComments: false).ConfigureAwait(false);
            return new FhirXmlNode(doc.Root, settings);
        }

        /// <inheritdoc cref="ParseAsync(string, FhirXmlParsingSettings)" />
        public static ISourceNode Parse(string xml, FhirXmlParsingSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return Read(reader, settings);
            }
        }
        
        public static async Task<ISourceNode> ParseAsync(string xml, FhirXmlParsingSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = await SerializationUtil.XmlReaderFromXmlTextAsync(xml, ignoreComments: false))
            {
                return await ReadAsync(reader, settings).ConfigureAwait(false);
            }
        }

        public static ISourceNode Create(XElement root, FhirXmlParsingSettings settings = null) => new FhirXmlNode(root, settings);

        public static ISourceNode Create(XDocument root, FhirXmlParsingSettings settings = null) => Create(root.Root, settings);
    }
}
