/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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

            root.WriteTo(writer);
            //var result = new XDocument(root);
            //result.WriteTo(writer);
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
                        getContentAsXElement(re.Resource, summary)));

                // Note: this is a read-only property, so it is serialized but never parsed
                if (entry.Summary != null)
                {
                    var xelem = XElement.Parse(entry.Summary);
                    xelem.Name = XHtml.XHTMLNS + xelem.Name.LocalName;

                    result.Add(new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_SUMMARY,
                            new XAttribute(BundleXmlParser.XATOM_CONTENT_TYPE, "xhtml"), xelem));
                }
            }

            return result;
        }

        private static object getContentAsXElement(Resource resource, bool summary)
        {
            var xml = FhirSerializer.SerializeResourceToXml(resource, summary);

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
