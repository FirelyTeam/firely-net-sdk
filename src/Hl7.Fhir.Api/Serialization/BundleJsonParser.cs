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
using Hl7.Fhir.Rest;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Serialization
{
    internal static class BundleJsonParser
    {
        internal const string JATOM_VERSION = "version";
        internal const string JATOM_DELETED = "deleted";

        private static int? intValueOrNull(JToken attr)
        {
            if (attr == null) return null;

            return attr.Value<int?>();
        }

      
        private static DateTimeOffset? instantOrNull(JToken attr)
        {
            if (attr == null) return null;

            return PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(attr.Value<string>());
        }

        internal static Bundle Load(JsonReader reader)
        {
            JObject feed;

            try
            {
                reader.DateParseHandling = DateParseHandling.None;
                reader.FloatParseHandling = FloatParseHandling.Decimal;
                feed = JObject.Load(reader);

                if( feed.Value<string>(JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME) != "Bundle")
                    throw Error.Format("Input data is not an json FHIR bundle", null);

            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while parsing feed: " + exc.Message, null);
            }

            Bundle result;

            try
            {
                result = new Bundle()
                {
                    Title = feed.Value<string>(BundleXmlParser.XATOM_TITLE),
                    LastUpdated = instantOrNull(feed[BundleXmlParser.XATOM_UPDATED]),
                    Id = SerializationUtil.UriValueOrNull(feed[BundleXmlParser.XATOM_ID]),
                    Links = getLinks(feed[BundleXmlParser.XATOM_LINK]),
                    Tags = TagListParser.ParseTags(feed[BundleXmlParser.XATOM_CATEGORY]),
                    AuthorName = feed[BundleXmlParser.XATOM_AUTHOR] as JArray != null ?
                                feed[BundleXmlParser.XATOM_AUTHOR]
                                    .Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_NAME))
                                    .FirstOrDefault()
                                : null,
                    AuthorUri = feed[BundleXmlParser.XATOM_AUTHOR] as JArray != null ?
                                feed[BundleXmlParser.XATOM_AUTHOR]
                                    .Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_URI))
                                    .FirstOrDefault() : null,
                    TotalResults = intValueOrNull(feed[BundleXmlParser.XATOM_TOTALRESULTS])
                };
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while parsing json feed attributes: " + exc.Message, null);                
            }

            var entries = feed[BundleXmlParser.XATOM_ENTRY];
            if (entries != null)
            {
                if (!(entries is JArray))
                {
                    throw Error.Format("The json feed contains a single entry, instead of an array", null);
                }

                result.Entries = loadEntries((JArray)entries, result);
            }

            return result;
        }

        private static IList<BundleEntry> loadEntries(JArray entries, Bundle parent)
        {
            var result = new List<BundleEntry>();

            foreach (var entry in entries)
            {
                if (entry.Type != JTokenType.Object)
                    throw Error.Format("Expected a complex object when reading an entry", JsonDomFhirReader.GetLineInfo(entries));

                var loaded = loadEntry((JObject)entry);
                if(entry != null) result.Add(loaded);
            }

            return result;
        }

        internal static BundleEntry LoadEntry(JsonReader reader)
        {
            JObject entry;
            reader.DateParseHandling = DateParseHandling.None;
            reader.FloatParseHandling = FloatParseHandling.Decimal;

            try
            {
                entry = JObject.Load(reader);
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while parsing json for entry: " + exc.Message, null);
            }

            return loadEntry(entry);
        }


        private static BundleEntry loadEntry(JObject entry)
        {
            BundleEntry result;

            try
            {
                if (entry[JATOM_DELETED] != null)
                {
                    result = new DeletedEntry();
                    result.Id = SerializationUtil.UriValueOrNull(entry[BundleXmlParser.XATOM_ID]);
                }
                else
                {
                    var content = entry[BundleXmlParser.XATOM_CONTENT];

                    var id = SerializationUtil.UriValueOrNull(entry[BundleXmlParser.XATOM_ID]);
                    if (id == null) throw Error.Format("BundleEntry found without an id", null);

                    if (content != null)
                    {
                        var parsed = getContents(content);
                        if (parsed != null)
                            result = ResourceEntry.Create(parsed);
                        else
                            throw Error.Format("BundleEntry {0} has a content element without content", null, id);
                    }
                    else
                    {
                        result = SerializationUtil.CreateResourceEntryFromId(id);
                    }

                    result.Id = id;
                }
                
                result.Links = getLinks(entry[BundleXmlParser.XATOM_LINK]);
                result.Tags = TagListParser.ParseTags(entry[BundleXmlParser.XATOM_CATEGORY]);

                if (result is DeletedEntry)
                    ((DeletedEntry)result).When = instantOrNull(entry[JATOM_DELETED]);
                else
                {
                    var re = (ResourceEntry)result;
                    re.Title = entry.Value<string>(BundleXmlParser.XATOM_TITLE);
                    re.LastUpdated = instantOrNull(entry[BundleXmlParser.XATOM_UPDATED]);
                    re.Published = instantOrNull(entry[BundleXmlParser.XATOM_PUBLISHED]);
                    re.AuthorName = entry[BundleXmlParser.XATOM_AUTHOR] as JArray != null ?
                        entry[BundleXmlParser.XATOM_AUTHOR]
                            .Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_NAME))
                            .FirstOrDefault() : null;
                    re.AuthorUri = entry[BundleXmlParser.XATOM_AUTHOR] as JArray != null ?
                        entry[BundleXmlParser.XATOM_AUTHOR]
                            .Select(auth => auth.Value<string>(BundleXmlParser.XATOM_AUTH_URI))
                            .FirstOrDefault() : null;
                }
            }
            catch (Exception exc)
            {
                throw Error.Format("Exception while reading entry: " + exc.Message, null);
            }

            return result;
        }

      
        private static UriLinkList getLinks(JToken token)
        {
            var result = new UriLinkList();
            var links = token as JArray;

            if (links != null)
            {
                foreach (var link in links)
                {
                    var uri = SerializationUtil.UriValueOrNull(link[BundleXmlParser.XATOM_LINK_HREF]);

                    if (uri != null)
                        result.Add(new UriLinkEntry
                        {
                            Rel = link.Value<string>(BundleXmlParser.XATOM_LINK_REL),
                            Uri = uri
                        });
                }
            }

            return result;
        }

        private static Resource getContents(JToken token)
        {
            return (Resource)(new ResourceReader(new JsonDomFhirReader(token)).Deserialize());
        }
    }
}
