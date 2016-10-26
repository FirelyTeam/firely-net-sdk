using Furore.Support;
using Hl7.ElementModel;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct XmlDomFhirNavigator
    {
        public static IElementNavigator Create(XmlReader reader)
        {
            XDocument doc = null;

            try
            {
                doc = SerializationUtil.XDocumentFromReader(reader);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message);
            }

            return new XmlDomFhirNavigator(doc.Root);
        }

        public static IElementNavigator Create(string xml)
        {
            using (var reader = SerializationUtil.XmlReaderFromXmlText(xml))
            {
                return Create(reader);
            }
        }

        //public static IElementNavigator Create(JsonReader reader, bool disallowXsiAttributesOnRoot = false)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
