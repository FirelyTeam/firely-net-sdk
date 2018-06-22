/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Support.Utility;
using Hl7.Fhir.Utility;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    [Obsolete("Please use the equivalent functions on the FhirXmlNavigator factory class")]
    public struct XmlDomFhirNavigator
    {
        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(string xml) => FhirXmlNavigator.Untyped(xml);

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(XmlReader reader) => FhirXmlNavigator.Untyped(reader);

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(XDocument doc) => FhirXmlNavigator.Untyped(doc);

        [Obsolete("Use FhirXmlNavigator.Untyped() instead")]
        public static IElementNavigator Create(XElement elem) => FhirXmlNavigator.Untyped(elem);
    }

    public struct FhirXmlNavigator
    {
        public static IElementNavigator Untyped(XmlReader reader, Configuration config=null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            return createInternal(reader, null, null, config);
        }

        public static IElementNavigator Untyped(string xml, Configuration config = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createInternal(reader, null, null, config);
            }
        }

        public static IElementNavigator ForRoot(string xml, IModelMetadataProvider provider, Configuration config=null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createInternal(reader, null, provider, config);
            }
        }

        public static IElementNavigator ForElement(string xml, string type, IModelMetadataProvider provider, Configuration config = null)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createInternal(reader, type, provider, config);
            }
        }


        public static IElementNavigator ForRoot(XmlReader reader, IModelMetadataProvider provider, Configuration config = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createInternal(reader, null, provider, config);
        }

        public static IElementNavigator ForElement(XmlReader reader, string type, IModelMetadataProvider provider, Configuration config = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createInternal(reader, type, provider, config);
        }


        public static IElementNavigator Untyped(XDocument doc, Configuration config = null)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));

            return createInternal(doc.Root, null, null, config);
        }

        public static IElementNavigator ForRoot(XDocument doc, IModelMetadataProvider provider, Configuration config = null)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createInternal(doc.Root, null, provider, config);
        }

        public static IElementNavigator Untyped(XElement elem, Configuration config = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));

            return createInternal(elem, null, null, config);
        }

        public static IElementNavigator ForRoot(XElement elem, IModelMetadataProvider provider, Configuration config = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createInternal(elem, null, provider, config);
        }

        public static IElementNavigator ForElement(XElement elem, string type, IModelMetadataProvider provider, Configuration config = null)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (provider == null) throw Error.ArgumentNull(nameof(provider));

            return createInternal(elem, type, provider, config);
        }

        private static IElementNavigator createInternal(XmlReader reader, string type, IModelMetadataProvider provider, Configuration config)
        {
            XDocument doc = null;

            try
            {
                doc = XDocument.Load(SerializationUtil.WrapXmlReader(reader, ignoreComments: false), 
                    LoadOptions.SetLineInfo);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message);
            }

            return createInternal(doc.Root, type, provider, config);
        }

        private static IElementNavigator createInternal(XElement elem, string type, IModelMetadataProvider provider, Configuration config)
        {
            var untypedNav = new UntypedXmlDomFhirNavigator(elem, config);

            if (provider == null)
                return untypedNav;
            else
            {
                if (type == null)
                    return TypedNavigator.ForRoot(untypedNav, provider);
                else
                    return TypedNavigator.ForElement(untypedNav, type, provider);
            }
        }
    }
}
