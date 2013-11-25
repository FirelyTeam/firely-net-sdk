using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class XmlDomFhirReader : IFhirReader
    {
        XNode _current;

        public XmlDomFhirReader(XNode root)
        {
            _current = root;
        }



        public string GetResourceTypeName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFhirReader> GetArrayElements()
        {
            throw new NotImplementedException();
        }

        public object GetPrimitiveValue()
        {
            throw new NotImplementedException();
        }

        public TokenType CurrentToken
        {
            get { throw new NotImplementedException(); }
        }
    }
}
