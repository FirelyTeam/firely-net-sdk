/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Fhir.Profiling
{
    public static class FhirNamespace
    {
        public static XmlNamespaceManager GetManager(XPathNavigator navigator)
        {
            XmlNamespaceManager nsm = new XmlNamespaceManager(navigator.NameTable);
            nsm.AddNamespace("f", "http://hl7.org/fhir");
            nsm.AddNamespace("atom", "http://www.w3.org/2005/Atom");
            return nsm;

        }
    }
}
