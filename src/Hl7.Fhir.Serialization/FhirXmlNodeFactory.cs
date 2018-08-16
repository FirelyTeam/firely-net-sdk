/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial class FhirXmlNode
    {
        public static ISourceNode Read(XmlReader reader, FhirXmlNodeSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            var doc = SerializationUtil.XDocumentFromReader(reader, ignoreComments: false);
            return new FhirXmlNode(doc.Root, settings);
        }

        public static ISourceNode Parse(string xml, FhirXmlNodeSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return Read(reader, settings);
            }
        }

        public static ISourceNode Create(XElement root, FhirXmlNodeSettings settings = null) => new FhirXmlNode(root, settings);

        public static ISourceNode Create(XDocument root, FhirXmlNodeSettings settings = null) => Create(root.Root, settings);
    }

    [Obsolete("Please use the equivalent functions on the FhirXmlNavigator factory class")]
    public struct XmlDomFhirNavigator
    {
        [Obsolete("Use FhirXmlNode.Parse() instead.")]
        public static IElementNavigator Create(string xml) => FhirXmlNode.Parse(xml).ToElementNavigator();

        [Obsolete("Use FhirXmlNode.Read() instead.")]
        public static IElementNavigator Create(XmlReader reader) => FhirXmlNode.Read(reader).ToElementNavigator();

        [Obsolete("Use FhirXmlNode.Create() instead.")]
        public static IElementNavigator Create(XDocument doc) => FhirXmlNode.Create(doc).ToElementNavigator();

        [Obsolete("Use FhirXmlNode.Create() instead.")]
        public static IElementNavigator Create(XElement elem) => FhirXmlNode.Create(elem).ToElementNavigator();
    }

}
