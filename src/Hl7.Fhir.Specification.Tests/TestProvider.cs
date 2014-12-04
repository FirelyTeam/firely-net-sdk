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
using System.Xml.XPath;
using System.Xml;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Specification.IO;

namespace Fhir.Profiling.Tests
{
    public static class TestProvider
    {
        public static XPathNavigator LoadResource(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            var nav = document.CreateNavigator();
            nav.MoveToFirstChild();
            XmlNamespaceManager manager = new XmlNamespaceManager(nav.NameTable);
            manager.AddNamespace("f", "http://hl7.org/fhir");

            return nav;
        }
    }

}
