/*  
* Copyright (c) 2017, Firely (info@fire.ly) and contributors 
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
        public static IElementNavigator Create(XmlReader reader)
        {
            XDocument doc = null;

            // [WMR 20171017] Why catch and rethrow? Original error info is lost...
            try
            {
                doc = SerializationUtil.XDocumentFromReader(reader);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message);
            }

            return Create(doc.Root);
        }

        public static IElementNavigator Create(XDocument doc)
        {
            return new XmlDomFhirNavigator(doc.Root);
        }

        public static IElementNavigator Create(XElement elem)
        {
            return new XmlDomFhirNavigator(elem);
        }

        public static IElementNavigator Create(string xml)
        {
            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml))
            {
                return Create(reader);
            }
        }
    }
}
