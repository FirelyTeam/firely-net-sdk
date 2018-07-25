/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
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
        public static ISourceNavigator Create(string xml) => FhirXmlNavigator.Untyped(xml);

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static ISourceNavigator Create(XmlReader reader) => FhirXmlNavigator.Untyped(reader);

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static ISourceNavigator Create(XDocument doc) => FhirXmlNavigator.Untyped(doc);

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static ISourceNavigator Create(XElement elem) => FhirXmlNavigator.Untyped(elem);
    }

    public partial class FhirXmlNavigator
    {
        public static ISourceNavigator Untyped(XmlReader reader, FhirXmlNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            return createUntyped(reader, settings);
        }

        public static ISourceNavigator Untyped(string xml, FhirXmlNavigatorSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createUntyped(reader, settings);
            }
        }

        public static ISourceNavigator Untyped(XElement elem, FhirXmlNavigatorSettings settings = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));

            return createUntyped(elem, settings);
        }

        public static ISourceNavigator Untyped(XDocument doc, FhirXmlNavigatorSettings settings = null)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));

            return createUntyped(doc.Root, settings);
        }

        public static IElementNavigator ForResource(string xml, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createTyped(reader, null, provider, settings);
            }
        }

        public static IElementNavigator ForElement(string xml, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createTyped(reader, type, provider, settings);
            }
        }


        public static IElementNavigator ForResource(XmlReader reader, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(reader, null, provider, settings);
        }

        public static IElementNavigator ForElement(XmlReader reader, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(reader, type, provider, settings);
        }


        public static IElementNavigator ForResource(XDocument doc, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(doc.Root, null, provider, settings);
        }

        public static IElementNavigator ForResource(XElement elem, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(elem, null, provider, settings);
        }

        public static IElementNavigator ForElement(XElement elem, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createTyped(elem, type, provider, settings);
        }

        private static ISourceNavigator createUntyped(XmlReader reader, FhirXmlNavigatorSettings settings)
        {
            XDocument doc = null;

            try
            {
                doc = XDocument.Load(SerializationUtil.WrapXmlReader(reader, ignoreComments: false),
                    LoadOptions.SetLineInfo);
                return createUntyped(doc.Root, settings);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message);
            }
        }

        private static ISourceNavigator createUntyped(XElement element, FhirXmlNavigatorSettings settings) => new FhirXmlNavigator(element, settings);

        private static IElementNavigator createTyped(XElement elem, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings)
        {
            var untypedNav = createUntyped(elem, settings);
            return untypedNav.AsElementNavigator(type, provider);
        }

        private static IElementNavigator createTyped(XmlReader reader, string type, IStructureDefinitionSummaryProvider provider, FhirXmlNavigatorSettings settings)
        {
            var untypedNav = createUntyped(reader, settings);
            return untypedNav.AsElementNavigator(type, provider);
        }
    }
}
