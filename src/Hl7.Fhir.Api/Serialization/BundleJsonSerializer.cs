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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    internal static class BundleJsonSerializer
    {
        public static void WriteTo(Bundle bundle, JsonWriter writer, bool summary = false)
        {
            if (bundle == null) throw new ArgumentException("Bundle cannot be null");

            JObject result = new JObject();

            result.Add(new JProperty(JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME, "Bundle"));

            if (!String.IsNullOrWhiteSpace(bundle.Title))
                result.Add(new JProperty(BundleXmlParser.XATOM_TITLE, bundle.Title));
            if (SerializationUtil.UriHasValue(bundle.Id)) result.Add(new JProperty(BundleXmlParser.XATOM_ID, bundle.Id));
            if (bundle.LastUpdated != null) 
                result.Add(new JProperty(BundleXmlParser.XATOM_UPDATED, bundle.LastUpdated));

            if (!String.IsNullOrWhiteSpace(bundle.AuthorName))
                result.Add(jsonCreateAuthor(bundle.AuthorName, bundle.AuthorUri));
            if (bundle.TotalResults != null) result.Add(new JProperty(BundleXmlParser.XATOM_TOTALRESULTS, bundle.TotalResults.ToString()));
          
            if (bundle.Links.Count > 0)
                result.Add(new JProperty(BundleXmlParser.XATOM_LINK, jsonCreateLinkArray(bundle.Links)));
            if (bundle.Tags != null && bundle.Tags.Count() > 0)
                result.Add( TagListSerializer.CreateTagCategoryPropertyJson(bundle.Tags));

            var entryArray = new JArray();

            foreach (var entry in bundle.Entries)
                entryArray.Add(createEntry(entry,summary));

            result.Add(new JProperty(BundleXmlParser.XATOM_ENTRY, entryArray));

            result.WriteTo(writer);
        }


        public static void WriteTo(BundleEntry entry, JsonWriter writer, bool summary = false)
        {
            if (entry == null) throw new ArgumentException("Entry cannot be null");

            var result = createEntry(entry, summary);

            result.WriteTo(writer);
        }


        private static JObject createEntry(BundleEntry entry, bool summary)
        {
            JObject result = new JObject();

            if (entry is ResourceEntry)
            {
                ResourceEntry re = (ResourceEntry)entry;
                if (!String.IsNullOrEmpty(re.Title)) result.Add(new JProperty(BundleXmlParser.XATOM_TITLE, re.Title));
                if (SerializationUtil.UriHasValue(entry.Id)) result.Add(new JProperty(BundleXmlParser.XATOM_ID, entry.Id.ToString()));

                if (re.LastUpdated != null) result.Add(new JProperty(BundleXmlParser.XATOM_UPDATED, re.LastUpdated));
                if (re.Published != null) result.Add(new JProperty(BundleXmlParser.XATOM_PUBLISHED, re.Published));

                if (!String.IsNullOrWhiteSpace(re.AuthorName))
                    result.Add(jsonCreateAuthor(re.AuthorName, re.AuthorUri));
            }
            else
            {
                DeletedEntry de = (DeletedEntry)entry;
                if (de.When != null) result.Add(new JProperty(BundleJsonParser.JATOM_DELETED, de.When));
                if (SerializationUtil.UriHasValue(entry.Id)) result.Add(new JProperty(BundleXmlParser.XATOM_ID, entry.Id.ToString()));
            }

            if(entry.Links != null && entry.Links.Count() > 0)
                result.Add(new JProperty(BundleXmlParser.XATOM_LINK, jsonCreateLinkArray(entry.Links)));

            if (entry.Tags != null && entry.Tags.Count() > 0) 
                result.Add(TagListSerializer.CreateTagCategoryPropertyJson(entry.Tags));

            if(entry is ResourceEntry)
            {
                ResourceEntry re = (ResourceEntry)entry;
                if (re.Resource != null)
                    result.Add(new JProperty(BundleXmlParser.XATOM_CONTENT, 
                        getContentsAsJObject(re.Resource, summary)));

                // Note: this is a read-only property, so it is serialized but never parsed
                if (entry.Summary != null)
                    result.Add(new JProperty(BundleXmlParser.XATOM_SUMMARY, entry.Summary));
            }

            return result;
        }



        private static JProperty jsonCreateAuthor(string name, string uri)
        {
            JObject author = new JObject();

            if (!String.IsNullOrEmpty(name))
                author.Add(new JProperty(BundleXmlParser.XATOM_AUTH_NAME, name));
            if (!String.IsNullOrWhiteSpace(uri))
                author.Add(new JProperty(BundleXmlParser.XATOM_AUTH_URI, uri));

            return new JProperty(BundleXmlParser.XATOM_AUTHOR, new JArray(author));
        }

        private static JArray jsonCreateLinkArray(UriLinkList links)
        {
            var result = new JArray();

            foreach (var l in links)
                if (l.Uri != null)
                    result.Add(jsonCreateLink(l.Rel, l.Uri));

            return result;
        }

        private static JObject jsonCreateLink(string rel, Uri link)
        {
            return new JObject(
                new JProperty(BundleXmlParser.XATOM_LINK_REL, rel),
                new JProperty(BundleXmlParser.XATOM_LINK_HREF, link.ToString()));
        }

        private static JObject getContentsAsJObject(Resource resource, bool summary)
        {
            JsonDomFhirWriter writer = new JsonDomFhirWriter();
            ResourceWriter w = new ResourceWriter(writer);
            w.Serialize(resource, summary);

            return writer.Result;
        }
    }
}
