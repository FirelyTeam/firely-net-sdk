/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.IO;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    internal static class BundleXmlSerializer
    {
        public static void WriteTo(Bundle bundle, XmlWriter writer, bool summary = false)
        {
            if (bundle == null) throw new ArgumentException("Bundle cannot be null");

            var root = new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_FEED);

            if (!String.IsNullOrWhiteSpace(bundle.Title)) root.Add(xmlCreateTitle(bundle.Title));
            if (SerializationUtil.UriHasValue(bundle.Id)) root.Add(xmlCreateId(bundle.Id));
            if (bundle.LastUpdated != null) root.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_UPDATED, bundle.LastUpdated));

            if (!String.IsNullOrWhiteSpace(bundle.AuthorName))
                root.Add(xmlCreateAuthor(bundle.AuthorName, bundle.AuthorUri));
            if (bundle.TotalResults != null) root.Add(new XElement(BundleXmlParser.XOPENSEARCHNS + BundleXmlParser.XATOM_TOTALRESULTS, bundle.TotalResults));

            if (bundle.Links != null)
            {
                foreach (var l in bundle.Links)
                    root.Add(xmlCreateLink(l.Rel, l.Uri));
            }

            if (bundle.Tags != null)
            {
                foreach (var tag in bundle.Tags)
                    root.Add(TagListSerializer.CreateTagCategoryPropertyXml(tag));
            }

            foreach (var entry in bundle.Entries)
                root.Add(createEntry(entry, summary));

            var result = new XDocument(root);
            result.WriteTo(writer);
        }


        public static void WriteTo(BundleEntry entry, XmlWriter writer, bool summary = false)
        {
            if (entry == null) throw new ArgumentException("Entry cannot be null");

            var result = createEntry(entry,summary);

            var doc = new XDocument(result);
            doc.WriteTo(writer);
        }

      

        private static XElement createEntry(BundleEntry entry, bool summary)
        {
            XElement result = null;

            if (entry is ResourceEntry)
            {
                ResourceEntry re = (ResourceEntry)entry;
                result = new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_ENTRY);

                if (!String.IsNullOrEmpty(re.Title)) result.Add(xmlCreateTitle(re.Title));
                if (SerializationUtil.UriHasValue(entry.Id)) result.Add(xmlCreateId(entry.Id));

                if (re.LastUpdated != null) result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_UPDATED, re.LastUpdated.Value));
                if (re.Published != null) result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_PUBLISHED, re.Published.Value));

                if (!String.IsNullOrWhiteSpace(re.AuthorName))
                    result.Add(xmlCreateAuthor(re.AuthorName, re.AuthorUri));
            }
            else
            {
                result = new XElement(BundleXmlParser.XTOMBSTONE + BundleXmlParser.XATOM_DELETED_ENTRY);
                if (SerializationUtil.UriHasValue(entry.Id))
                    result.Add(new XAttribute(BundleXmlParser.XATOM_DELETED_REF, entry.Id.ToString()));
                if (((DeletedEntry)entry).When != null)
                    result.Add(new XAttribute(BundleXmlParser.XATOM_DELETED_WHEN, ((DeletedEntry)entry).When));
            }

            if(entry.Links != null)
                foreach (var l in entry.Links)
                    if (l.Uri != null) result.Add(xmlCreateLink(l.Rel, l.Uri));

            if (entry.Tags != null)
                foreach (var tag in entry.Tags)
                    result.Add(TagListSerializer.CreateTagCategoryPropertyXml(tag));

            if(entry is ResourceEntry)
            {
                ResourceEntry re = (ResourceEntry)entry;
                if (re.Resource != null)
                    result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_CONTENT,
                        new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "text/xml"),
                        getContentAsXElement(re.Resource)));

                // Note: this is a read-only property, so it is serialized but never parsed
                if (entry.Summary != null)
                {
                    var xelem = XElement.Parse(entry.Summary);
                    xelem.Name = XNamespace.Get(SerializationUtil.XHTMLNS) + xelem.Name.LocalName;

                    result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_SUMMARY,
                            new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "xhtml"), xelem));
                }
            }

            return result;
        }

        private static object getContentAsXElement(Resource resource)
        {
            var xml = FhirSerializer.SerializeResourceToXml(resource);

            return XElement.Parse(xml);
        }

        private static XElement xmlCreateId(Uri id)
        {
            return new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_ID, id.ToString());
        }

        private static XElement xmlCreateLink(string rel, Uri uri)
        {
            return new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_LINK,
                        new XAttribute(BundleXmlParser.XATOM_LINK_REL, rel), new XAttribute(BundleXmlParser.XATOM_LINK_HREF, uri.ToString()));
        }

        private static XElement xmlCreateTitle(string title)
        {
            return new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_TITLE,
                new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "text"), title);
        }


        private static XElement xmlCreateAuthor(string name, string uri)
        {
            var result = new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_AUTHOR);

            if (!String.IsNullOrEmpty(name))
                result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_AUTH_NAME, name));

            if (!String.IsNullOrEmpty(uri))
                result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_AUTH_URI, uri));

            return result;
        }
    }
}
