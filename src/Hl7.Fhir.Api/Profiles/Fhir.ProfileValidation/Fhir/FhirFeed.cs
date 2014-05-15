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

namespace Fhir
{

    public class ResourceNavigator
    {
        public XPathNavigator navigator { get; private set; }
    }

    public class Feed
    {
        XPathNavigator navigator;
        XmlNamespaceManager manager;

        public Feed(IXPathNavigable navigable)
        {
            navigator = navigable.CreateNavigator();
            manager = new XmlNamespaceManager(navigator.NameTable);
            manager.AddNamespace("f", "http://hl7.org/fhir");
            manager.AddNamespace("atom", "http://www.w3.org/2005/Atom");
        }

        public XPathNodeIterator Select(string xpath)
        {
            return navigator.Select(xpath, manager);
        }

        public IEnumerable<XPathNavigator> Resources
        {
            get {
                XPathNodeIterator iterator = Select("atom:feed/atom:entry/atom:content/*");
                foreach (XPathNavigator node in iterator)
                {
                    yield return node;
                }
            }
        }

        public string Value(XPathNavigator nav, string xpath)
        {
            XPathNavigator node = nav.SelectSingleNode(xpath, manager);
            return node.Value;
        }

        public bool Exists(XPathNavigator nav, string xpath)
        {
            XPathNavigator node = nav.SelectSingleNode(xpath, manager);
            return (node != null);
        }
    }
  
}
