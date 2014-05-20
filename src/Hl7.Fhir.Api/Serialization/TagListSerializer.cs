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
    internal static class TagListSerializer
    {
        public const string TAGLIST_TYPE = "taglist";

        //internal static void SerializeTagList(IEnumerable<Tag> list, JsonWriter writer)
        //{
        //    JObject jTagList = new JObject(
        //        new JProperty(SerializationConfig.RESOURCETYPE_MEMBER_NAME, TAGLIST_TYPE),
        //        CreateTagCategoryPropertyJson(list));

        //    jTagList.WriteTo(writer);
        //}


        internal static JProperty CreateTagCategoryPropertyJson(IEnumerable<Tag> tagList)
        {
            JArray jTags = new JArray();
            JProperty result = new JProperty(BundleXmlParser.XATOM_CATEGORY, jTags);

            foreach (Tag tag in tagList)
            {
                JObject jTag = new JObject();
                if (!String.IsNullOrEmpty(tag.Term))
                    jTag.Add(new JProperty(BundleXmlParser.XATOM_CAT_TERM, tag.Term));
                if (!String.IsNullOrEmpty(tag.Label))
                    jTag.Add(new JProperty(BundleXmlParser.XATOM_CAT_LABEL, tag.Label));
                jTag.Add(new JProperty(BundleXmlParser.XATOM_CAT_SCHEME, tag.Scheme.ToString()));
                jTags.Add(jTag);
            }

            return result;
        }

        //internal static void SerializeTagList(IEnumerable<Tag> list, XmlWriter writer)
        //{
        //    XElement xTagList = new XElement(BundleXmlParser.XFHIRNS + TAGLIST_TYPE);

        //    foreach (var tag in list)
        //        xTagList.Add(CreateTagCategoryPropertyXml(tag, useAtomNs: false));

        //    xTagList.WriteTo(writer);
        //}

        internal static XElement CreateTagCategoryPropertyXml(Tag tag, bool useAtomNs = true)
        {           
            XElement result = useAtomNs ?
                new XElement(BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_CATEGORY) :
                new XElement(BundleXmlParser.XFHIRNS + BundleXmlParser.XATOM_CATEGORY);

            if (!String.IsNullOrEmpty(tag.Term))
                result.Add(new XAttribute(BundleXmlParser.XATOM_CAT_TERM, tag.Term));
            if (!String.IsNullOrEmpty(tag.Label))
                result.Add(new XAttribute(BundleXmlParser.XATOM_CAT_LABEL, tag.Label));
            result.Add(new XAttribute(BundleXmlParser.XATOM_CAT_SCHEME, tag.Scheme.ToString()));

            return result;
        }

    
    }
}
