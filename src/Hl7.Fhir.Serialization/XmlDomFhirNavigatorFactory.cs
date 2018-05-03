/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct XmlDomFhirNavigator
    {
        private static IElementNavigator createInternal(XmlReader reader, IModelMetadataProvider metadataProvider)
        {
            XDocument doc = null;

            try
            {
                doc = XDocument.Load(SerializationUtil.WrapXmlReader(reader, ignoreComments: false));
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message);
            }

            return XmlDomFhirNavigator.ForRoot(doc.Root, metadataProvider);
        }

        public static IElementNavigator Create(XmlReader reader, IModelMetadataProvider metadataProvider)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));
            if (metadataProvider == null) throw Error.ArgumentNull(nameof(metadataProvider));

            return createInternal(reader, metadataProvider);
        }

        public static IElementNavigator CreateUntyped(XmlReader reader)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            return createInternal(reader, null);
        }

        public static IElementNavigator Create(XDocument doc, IModelMetadataProvider metadataProvider)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));
            if (metadataProvider == null) throw Error.ArgumentNull(nameof(metadataProvider));

            return XmlDomFhirNavigator.ForRoot(doc.Root, metadataProvider);
        }

        public static IElementNavigator CreateUntyped(XDocument doc)
        {
            if (doc == null) throw Error.ArgumentNull(nameof(doc));

            return XmlDomFhirNavigator.ForRoot(doc.Root, null);
        }

        public static IElementNavigator Create(XElement elem, string type, IModelMetadataProvider metadataProvider)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (metadataProvider == null) throw Error.ArgumentNull(nameof(metadataProvider));

            return XmlDomFhirNavigator.ForElement(elem, type, metadataProvider);
        }

        public static IElementNavigator CreateUntyped(XElement elem, string type)
        {
            if (elem == null) throw Error.ArgumentNull(nameof(elem));
            if (type == null) throw Error.ArgumentNull(nameof(type));

            return XmlDomFhirNavigator.ForElement(elem, type, null);
        }


        public static IElementNavigator Create(string xml, IModelMetadataProvider metadataProvider)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));
            if (metadataProvider == null) throw Error.ArgumentNull(nameof(metadataProvider));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createInternal(reader, metadataProvider);
            }
        }

        public static IElementNavigator CreateUntyped(string xml)
        {
            if (xml == null) throw Error.ArgumentNull(nameof(xml));

            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml, ignoreComments: false))
            {
                return createInternal(reader, null);
            }
        }

    }
}
