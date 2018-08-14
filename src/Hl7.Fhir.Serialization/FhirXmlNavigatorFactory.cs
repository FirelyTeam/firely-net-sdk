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
    [Obsolete("Please use the equivalent functions on the FhirXmlNavigator factory class")]
    public struct XmlDomFhirNavigator
    {
        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(string xml) => FhirXmlNavigator.Untyped(xml).ToElementNavigator();

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(XmlReader reader) => FhirXmlNavigator.Untyped(reader).ToElementNavigator();

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(XDocument doc) => FhirXmlNavigator.Untyped(doc).ToElementNavigator();

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(XElement elem) => FhirXmlNavigator.Untyped(elem).ToElementNavigator();
    }

    public partial class FhirXmlNavigator
    {
        public static ISourceNode Untyped(XmlReader reader, FhirXmlNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            return createUntyped(reader, settings);
        }

        public static ISourceNode Untyped(string xml, FhirXmlNavigatorSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createUntyped(reader, settings);
            }
        }

        public static ISourceNode Untyped(XElement elem, FhirXmlNavigatorSettings settings = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));

            return createUntyped(elem, settings);
        }

        public static ISourceNode Untyped(XDocument doc, FhirXmlNavigatorSettings settings = null)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));

            return createUntyped(doc.Root, settings);
        }

        public static IElementNode ForResource(string xml, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null, TypedNodeSettings tnSettings=null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createTyped(reader, null, provider, settings, tnSettings);
            }
        }

        public static IElementNode ForElement(string xml, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createTyped(reader, type, provider, settings);
            }
        }


        public static IElementNode ForResource(XmlReader reader, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(reader, null, provider, settings);
        }

        public static IElementNode ForElement(XmlReader reader, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(reader, type, provider, settings);
        }


        public static IElementNode ForResource(XDocument doc, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(doc.Root, null, provider, settings);
        }

        public static IElementNode ForResource(XElement elem, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(elem, null, provider, settings);
        }

        public static IElementNode ForElement(XElement elem, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(elem, type, provider, settings);
        }

        private static ISourceNode createUntyped(XmlReader reader, FhirXmlNavigatorSettings settings)
        {
            var doc = SerializationUtil.XDocumentFromReader(reader, ignoreComments: false);
            return createUntyped(doc.Root, settings);
        }

        private static ISourceNode createUntyped(XElement element, FhirXmlNavigatorSettings settings) =>
            new FhirXmlNode(element, settings);

        private static IElementNode createTyped(XElement elem, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings, TypedNodeSettings tnSettings = null) =>
            createUntyped(elem, settings).ToElementNode(provider, type, tnSettings);

        private static IElementNode createTyped(XmlReader reader, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings, TypedNodeSettings tnSettings = null) =>
            createUntyped(reader, settings).ToElementNode(provider, type, tnSettings);
    }
}
