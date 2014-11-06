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
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Specification.Expansion;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.IO;

namespace Hl7.Fhir.IO
{
    public static class FhirFile
    {
        static XmlNamespaceManager manager;

        public static XPathNavigator GetFhirNavigator(this IXPathNavigable navigable)
        {
            var navigator = navigable.CreateNavigator();
            manager = new XmlNamespaceManager(navigator.NameTable);
            manager.AddNamespace("f", "http://hl7.org/fhir");
            manager.AddNamespace("atom", "http://www.w3.org/2005/Atom");
            return navigator;
        }

        private static XPathNavigator load(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XPathNavigator nav = GetFhirNavigator(doc);

            return nav;
        }

        public static XPathNavigator LoadFeedResource(string filename, string resource)
        {
            var nav = load(filename);
            return nav.GetResources().Where(x => x.Name == resource).FirstOrDefault();
        }

        public static XPathNavigator LoadResource(string filename)
        {
            var nav = load(filename);
            nav.MoveToFirstChild();
            return nav;
        }

        public static IEnumerable<XPathNavigator> GetResources(this XPathNavigator feed)
        {
            XPathNodeIterator iterator = feed.Select("atom:feed/atom:entry/atom:content/*", manager);
            foreach (XPathNavigator node in iterator)
            {
                    yield return node;
            }
        }

        public static void ExpandProfileFile(string inputfile, string outputfile)
        {
            var source = new CachedArtifactSource(ArtifactResolver.CreateOffline());
            var expander = new ProfileExpander(source);
            
            string xml = File.ReadAllText(inputfile);
            var diff = (Profile)FhirParser.ParseResourceFromXml(xml);
            expander.Expand(diff);
            xml = FhirSerializer.SerializeResourceToXml(diff);
            File.WriteAllText(outputfile, xml);
        }

    }
}
