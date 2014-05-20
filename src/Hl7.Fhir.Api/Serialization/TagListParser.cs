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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hl7.Fhir.Serialization
{
    internal static class TagListParser
    {
        //internal static IList<Tag> ParseTags(XmlReader xr)
        //{
        //    xr.MoveToContent();

        //    var taglist = (XElement)XElement.ReadFrom(xr);

        //    if (taglist.Name == BundleXmlParser.XFHIRNS + TagListSerializer.TAGLIST_TYPE)
        //    {
        //        if (taglist.Elements().All(xe => xe.Name == BundleXmlParser.XFHIRNS + BundleXmlParser.XATOM_CATEGORY))
        //            return ParseTags(taglist.Elements());
        //        else
        //            throw Error.Format("TagList contains unexpected child elements");
        //    }
        //    else
        //        throw Error.Format("Unexpected element name {0} found at start of TagList", taglist.Name);
        //}

        //internal static IList<Tag> ParseTags(JsonReader xr)
        //{
        //    var tagObj = JObject.Load(xr);

        //    var tagType = tagObj[SerializationConfig.RESOURCETYPE_MEMBER_NAME];
        //    if(tagType == null || tagType.Value<string>() != TagListSerializer.TAGLIST_TYPE)
        //        throw Error.Format("TagList should start with a resourceType member TagList");

        //    var categoryArray = tagObj[BundleXmlParser.XATOM_CATEGORY] as JArray;
        //    if (categoryArray != null)
        //        return ParseTags(categoryArray);
        //    else
        //        return new List<Tag>();
        //}

        internal static IList<Tag> ParseTags(IEnumerable<XElement> tags)
        {
            var result = new List<Tag>();

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    var scheme = SerializationUtil.StringValueOrNull(tag.Attribute(BundleXmlParser.XATOM_CAT_SCHEME));
                    var term = SerializationUtil.StringValueOrNull(tag.Attribute(BundleXmlParser.XATOM_CAT_TERM));
                    var label = SerializationUtil.StringValueOrNull(tag.Attribute(BundleXmlParser.XATOM_CAT_LABEL));

                    result.Add(new Tag(term,scheme,label));
                }
            }

            return result;
        }


        internal static IList<Tag> ParseTags(JToken token)
        {
            var result = new List<Tag>();
            var tags = token as JArray;

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    var scheme = tag.Value<string>(BundleXmlParser.XATOM_CAT_SCHEME);
                    var term = tag.Value<string>(BundleXmlParser.XATOM_CAT_TERM);
                    var label = tag.Value<string>(BundleXmlParser.XATOM_CAT_LABEL);
                    
                    result.Add(new Tag(term,scheme,label));
                }
            }

            return result;
        }
    }
}
