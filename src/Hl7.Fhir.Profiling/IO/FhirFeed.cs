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

    
    
    

    public class Feed
    {
        XPathNavigator navigator;
        XmlNamespaceManager manager;

        public class Entry
        {
            public string Title { get; internal set; }
            public string Id { get; internal set; }
            public XPathNavigator ResourceNode;

            internal XPathNavigator Node;
        }

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

        public IEnumerable<Entry> Entries()
        {
            XPathNodeIterator iterator = Select("atom:feed/atom:entry");
            foreach (XPathNavigator node in iterator)
            {
                Entry entry = new Entry();
                entry.Node = node;
                entry.Title = node.SelectSingleNode("atom:title", manager).Value;
                entry.Id = node.SelectSingleNode("atom:id", manager).Value;
                entry.ResourceNode = node.SelectSingleNode("atom:content/*", manager);
                yield return entry;
            }
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
