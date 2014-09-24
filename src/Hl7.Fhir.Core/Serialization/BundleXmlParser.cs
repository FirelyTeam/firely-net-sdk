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
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Serialization
{
    internal static class BundleXmlParser
    {
        public const string XATOM_FEED = "feed";
        public const string XATOM_DELETED_ENTRY = "deleted-entry";
        public const string XATOM_DELETED_WHEN = "when";
        public const string XATOM_DELETED_REF = "ref";
        public const string XATOM_LINK = "link";
        public const string XATOM_LINK_REL = "rel";
        public const string XATOM_LINK_HREF = "href";
        public const string XATOM_CONTENT_BINARY = "Binary";
        public const string XATOM_CONTENT_TYPE = "type";
        public const string XATOM_CONTENT_BINARY_TYPE = "contentType";
        public const string XATOM_TITLE = "title";
        public const string XATOM_UPDATED = "updated";
        public const string XATOM_ID = "id";
        public const string XATOM_ENTRY = "entry";
        public const string XATOM_PUBLISHED = "published";
        public const string XATOM_AUTHOR = "author";
        public const string XATOM_AUTH_NAME = "name";
        public const string XATOM_AUTH_URI = "uri";
        public const string XATOM_CATEGORY = "category";
        public const string XATOM_CAT_TERM = "term";
        public const string XATOM_CAT_SCHEME = "scheme";
        public const string XATOM_CAT_LABEL = "label";
        public const string XATOM_CONTENT = "content";
        public const string XATOM_SUMMARY = "summary";
        public const string XATOM_TOTALRESULTS = "totalResults";

        public static string ATOM_CATEGORY_RESOURCETYPE_NS = "http://hl7.org/fhir/resource-types";
        public static string ATOMPUB_TOMBSTONES_NS = "http://purl.org/atompub/tombstones/1.0";
        public static string ATOMPUB_NS = "http://www.w3.org/2005/Atom";
        public static string OPENSEARCH_NS = "http://a9.com/-/spec/opensearch/1.1/";

        public static readonly XNamespace XATOMNS = ATOMPUB_NS;
        public static readonly XNamespace XTOMBSTONE = ATOMPUB_TOMBSTONES_NS;
        public static readonly XNamespace XFHIRNS = SerializationUtil.FHIRNS;
        public static readonly XNamespace XOPENSEARCHNS = OPENSEARCH_NS;


        internal static Bundle Load(XmlReader reader)
        {
            XElement feed;

            var settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;

            try
            {

                var internalReader = XmlReader.Create(reader, settings);
                feed = XDocument.Load(internalReader, LoadOptions.SetLineInfo).Root;
                if (feed.Name != XNamespace.Get(ATOMPUB_NS) + "feed")
                    throw Error.Format("Input data is not an Atom feed", null);
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while loading feed: " + exc.Message, null);
            }

            Bundle result;

            try
            {
                result = new Bundle()
                {
                    Title = SerializationUtil.StringValueOrNull(feed.Element(XATOMNS + XATOM_TITLE)),
                    LastUpdated = SerializationUtil.InstantOrNull(feed.Element(XATOMNS + XATOM_UPDATED)),
                    Id = SerializationUtil.UriValueOrNull(feed.Element(XATOMNS + XATOM_ID)),
                    Links = getLinks(feed.Elements(XATOMNS + XATOM_LINK)),
                    Tags = TagListParser.ParseTags(feed.Elements(XATOMNS + XATOM_CATEGORY)),
                    AuthorName = feed.Elements(XATOMNS + XATOM_AUTHOR).Count() == 0 ? null :
                            SerializationUtil.StringValueOrNull(feed.Element(XATOMNS + XATOM_AUTHOR)
                                .Element(XATOMNS + XATOM_AUTH_NAME)),
                    AuthorUri = feed.Elements(XATOMNS + XATOM_AUTHOR).Count() == 0 ? null :
                            SerializationUtil.StringValueOrNull(feed.Element(XATOMNS + XATOM_AUTHOR)
                                .Element(XATOMNS + XATOM_AUTH_URI)),
                    TotalResults = SerializationUtil.IntValueOrNull(feed.Element(XOPENSEARCHNS + XATOM_TOTALRESULTS))
                };
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while parsing xml feed attributes: " + exc.Message, null);
            }

            result.Entries = loadEntries(feed.Elements().Where(elem =>
                        (elem.Name == XATOMNS + XATOM_ENTRY ||
                         elem.Name == XTOMBSTONE + XATOM_DELETED_ENTRY)), result);

            return result;
        }

        internal static Bundle Load(string xml)
        {
            return Load(FhirParser.XmlReaderFromString(xml));
        }

        private static IList<BundleEntry> loadEntries(IEnumerable<XElement> entries, Bundle parent)
        {
            var result = new List<BundleEntry>();

            foreach (var entry in entries)
            {
                var loaded = loadEntry(entry);
                if (entry != null) result.Add(loaded);
            }

            return result;
        }

        internal static BundleEntry LoadEntry(string xml)
        {
            return LoadEntry(FhirParser.XmlReaderFromString(xml));
        }

        internal static BundleEntry LoadEntry(XmlReader reader)
        {
            XElement entry;

            try
            {
                entry = XDocument.Load(reader).Root;
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while loading entry: " + exc.Message, null);
            }

            return loadEntry(entry);
        }

        private static BundleEntry loadEntry(XElement entry)
        {
            BundleEntry result;

            try
            {
                if (entry.Name == XTOMBSTONE + XATOM_DELETED_ENTRY)
                {
                    result = new DeletedEntry();
                    result.Id = SerializationUtil.UriValueOrNull(entry.Attribute(XATOM_DELETED_REF));
                }
                else
                {
                    XElement content = entry.Element(XATOMNS + XATOM_CONTENT);
                    var id = SerializationUtil.UriValueOrNull(entry.Element(XATOMNS + XATOM_ID));

                    if (content != null)
                    {
                        var parsed = getContents(content);
                        if (parsed != null)
                            result = ResourceEntry.Create(parsed);
                        else
                            throw Error.Format("BundleEntry has a content element without content", XmlDomFhirReader.GetLineInfo(content));
                    }
                    else
                    {
                        result = SerializationUtil.CreateResourceEntryFromId(id);
                    }
                  
                    result.Id = id;
                }

                result.Links = getLinks(entry.Elements(XATOMNS + XATOM_LINK));
                result.Tags = TagListParser.ParseTags(entry.Elements(XATOMNS + XATOM_CATEGORY));

                if (result is DeletedEntry)
                {
                    ((DeletedEntry)result).When = SerializationUtil.InstantOrNull(entry.Attribute(XATOM_DELETED_WHEN));
                }
                else
                {
                    ResourceEntry re = (ResourceEntry)result;
                    re.Title = SerializationUtil.StringValueOrNull(entry.Element(XATOMNS + XATOM_TITLE));
                    re.LastUpdated = SerializationUtil.InstantOrNull(entry.Element(XATOMNS + XATOM_UPDATED));
                    re.Published = SerializationUtil.InstantOrNull(entry.Element(XATOMNS + XATOM_PUBLISHED));
                    re.AuthorName = entry.Elements(XATOMNS + XATOM_AUTHOR).Count() == 0 ? null :
                                SerializationUtil.StringValueOrNull(entry.Element(XATOMNS + XATOM_AUTHOR)
                                    .Element(XATOMNS + XATOM_AUTH_NAME));
                    re.AuthorUri = entry.Elements(XATOMNS + XATOM_AUTHOR).Count() == 0 ? null :
                                SerializationUtil.StringValueOrNull(entry.Element(XATOMNS + XATOM_AUTHOR)
                                    .Element(XATOMNS + XATOM_AUTH_URI));                  
                }
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while reading entry: " + exc.Message, XmlDomFhirReader.GetLineInfo(entry));
            }

            return result;
        }


        private static UriLinkList getLinks(IEnumerable<XElement> links)
        {
            return new UriLinkList(
                links.Select(el => new UriLinkEntry
                {
                    Rel = SerializationUtil.StringValueOrNull(el.Attribute(XATOM_LINK_REL)),
                    Uri = SerializationUtil.UriValueOrNull(el.Attribute(XATOM_LINK_HREF))
                }));
        }


        private static Resource getContents(XElement content)
        {
            string contentType = SerializationUtil.StringValueOrNull(content.Attribute(XATOM_CONTENT_TYPE));

			if (contentType != "text/xml" && contentType != "application/xml+fhir")
            {
				throw Error.Format("Bundle Entry should have contents of type 'text/xml'", XmlDomFhirReader.GetLineInfo(content));
            }

#if DEBUG
			if (contentType == "application/xml+fhir")
			{
				System.Diagnostics.Debug.WriteLine("Bundle Entry should have contents of type 'text/xml'", XmlDomFhirReader.GetLineInfo(content));
			}
#endif

            XElement resource = null;

            try
            {
                resource = content.Elements().Single();
            }
            catch
            {
                throw Error.Format("Entry <content> node should have a single child: the resource", XmlDomFhirReader.GetLineInfo(content));
            }

            return (Resource)(new ResourceReader(new XmlDomFhirReader(resource)).Deserialize());
        }    
    }
}
