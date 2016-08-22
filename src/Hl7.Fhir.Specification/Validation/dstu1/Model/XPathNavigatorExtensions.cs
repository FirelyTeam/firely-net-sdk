using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace Hl7.Fhir.Specification.Model
{
    public static class XPathNavigatorExtensions
    {
        public static KeyValuePair<string, string> ToKeyValuePair(this XPathNavigator navigator)
        {
            return new KeyValuePair<string, string>(navigator.Name, navigator.Value);
        }
    }
}
