/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using Hl7.Fhir.Support;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using System.IO;

namespace Hl7.Fhir.Test
{
    [TestClass]
#if PORTABLE45
	public class PortableTagParsingTests
#else
	public class TagParsingTests
#endif
    {
        [TestMethod]
        public void TestUseFhirParserToTagList()
        {
            TagList l = new TagList();
            l.Category = new List<Tag>();

            l.Category.Add(new Tag("http://www.nu.nl/tags", Tag.FHIRTAGSCHEME_GENERAL, "No!"));
            l.Category.Add(new Tag("http://www.furore.com/tags", Tag.FHIRTAGSCHEME_GENERAL, "Maybe, indeed"));

            var xml = FhirSerializer.SerializeToXml(l);
            Assert.AreEqual(xmlTagList, xml);
        }


        [TestMethod]
        public void TagHeaderParsing()
        {
            string tag1 = @"http://furore.com/tags/test1; label = ""yes""; scheme=""http://hl7.org/fhir/tag""";
            string tag2 = @"http://furore.com/tags/test1; scheme=""http://hl7.org/fhir/tag""; label = ""confusion, abounds - beyond!"";";
            string tag3 = @"dog; label=""Canine""; scheme=""http://purl.org/net/animals""";

            string tags = tag1 + ", " + tag2 + " , " + tag3;

            var parsedTags = HttpUtil.ParseCategoryHeader(tags);
            Assert.AreEqual(3, parsedTags.Count());
            
            var t1 = parsedTags.First();
            Assert.AreEqual("yes", t1.Label);
            Assert.AreEqual("http://furore.com/tags/test1", t1.Term);
            
            var t2 = parsedTags.Skip(1).First();
            Assert.AreEqual(@"confusion, abounds - beyond!", t2.Label);
            Assert.AreEqual("http://furore.com/tags/test1", t2.Term);

            string cat = HttpUtil.BuildCategoryHeader(parsedTags.FilterByScheme(Tag.FHIRTAGSCHEME_GENERAL));

            Assert.AreEqual(@"http://furore.com/tags/test1; label=""yes""; scheme=""http://hl7.org/fhir/tag"", http://furore.com/tags/test1; " +
                        @"label=""confusion, abounds - beyond!""; scheme=""http://hl7.org/fhir/tag""", cat);
        }

        [TestMethod]
        public void AcceptsEmptyTagList()
        {
            var parsedTags = FhirParser.ParseTagListFromJson(jsonTagListEmpty);

            Assert.IsNotNull(parsedTags);
            Assert.AreEqual(0, parsedTags.Category.Count);
        }

        [TestMethod]
        public void TagEquality()
        {
            var t1 = new Tag("dog", new Uri("http://nu.nl"));
            var t2 = new Tag("dog", new Uri("http://knmi.nl") );
            var t3 = new Tag("dog", "http://knmi.nl");

            Assert.AreNotEqual(t1, t2);
            Assert.AreNotEqual(t1, t3);
            Assert.AreEqual(t2, t3);            
        }


        [TestMethod]
        public void SerializeAndDeserializeTagList()
        {
            TagList tl = new TagList();

            tl.Category.Add(new Tag("http://www.nu.nl/tags", Tag.FHIRTAGSCHEME_GENERAL, "No!"));
            tl.Category.Add(new Tag("http://www.furore.com/tags", Tag.FHIRTAGSCHEME_GENERAL, "Maybe, indeed" ));

            string json = FhirSerializer.SerializeTagListToJson(tl);
            Assert.AreEqual(jsonTagList, json);
         
            string xml = FhirSerializer.SerializeTagListToXml(tl);
            Assert.AreEqual(xmlTagList, xml);


            tl = FhirParser.ParseTagListFromXml(xml);
            verifyTagList(tl.Category);

            tl = FhirParser.ParseTagListFromJson(json);
            verifyTagList(tl.Category);
        }

        [TestMethod]
        public void CatchTagListParseErrors()
        {
            SerializationConfig.AcceptUnknownMembers = false;
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromXml(xmlTagListE1));           
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromXml(xmlTagListE2));
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromJson(jsonTagListE1));

            try
            {
                FhirParser.ParseTagListFromJson(jsonTagListE2);
                Assert.Fail();
            }
            catch (FormatException) { }
        }

        private static void formatExceptionOrFail(Action a)
        {
            try
            {
                a();
                Assert.Fail();
            }
            catch (FormatException)
            {
                // ok!
            }
        }


        private static void verifyTagList(IList<Tag> tl)
        {
            Assert.AreEqual(2, tl.Count);
            Assert.AreEqual("No!", tl[0].Label);
            Assert.AreEqual("http://www.nu.nl/tags", tl[0].Term);
            Assert.AreEqual("Maybe, indeed", tl[1].Label);
            Assert.AreEqual("http://www.furore.com/tags", tl[1].Term);
        }

        private string jsonTagList = @"{""resourceType"":""TagList"",""category"":[" +
            @"{""term"":""http://www.nu.nl/tags"",""scheme"":""http://hl7.org/fhir/tag"",""label"":""No!""}," +
            @"{""term"":""http://www.furore.com/tags"",""scheme"":""http://hl7.org/fhir/tag"",""label"":""Maybe, indeed""}]" +
            @"}";
        private string jsonTagListEmpty = @"{""resourceType"":""TagList""}";


        // Error: misses the resourceType member
        private string jsonTagListE1 = @"{""category"":[" +
            @"{""term"":""http://www.nu.nl/tags"",""label"":""No!"",""scheme"":""http://hl7.org/fhir/tag""}," +
            @"{""term"":""http://www.furore.com/tags"",""label"":""Maybe"",""scheme"":""http://hl7.org/fhir/tag""}" +
            @"]}";

        // Error: has the wrong the resourceType member
        private string jsonTagListE2 = @"{""resourceType"":""XTagList"", ""category"":[" +
            @"{""term"":""http://www.nu.nl/tags"",""label"":""No!"",""scheme"":""http://hl7.org/fhir/tag""}," +
            @"{""term"":""http://www.furore.com/tags"",""label"":""Maybe"",""scheme"":""http://hl7.org/fhir/tag""}" +
            @"]}";


        private string xmlTagList = 
            //@"<?xml version=""1.0"" encoding=""utf-16""?>" +
            @"<TagList xmlns=""http://hl7.org/fhir"">" +
            @"<category term=""http://www.nu.nl/tags"" scheme=""http://hl7.org/fhir/tag"" label=""No!"" />" +
            @"<category term=""http://www.furore.com/tags"" scheme=""http://hl7.org/fhir/tag"" label=""Maybe, indeed"" /></TagList>";
        private string xmlTagListE1 = @"<?xml version=""1.0"" encoding=""utf-16""?><Xtaglist xmlns=""http://hl7.org/fhir"">" +
            @"<category term=""http://www.nu.nl/tags"" label=""No!"" scheme=""http://hl7.org/fhir/tag"" />" +
            @"<category term=""http://www.furore.com/tags"" label=""Maybe"" scheme=""http://hl7.org/fhir/tag"" /></TagList>";
        private string xmlTagListE2 = @"<?xml version=""1.0"" encoding=""utf-16""?><TagList xmlns=""http://hl7.org/fhir"">" +
            @"<category term=""http://www.nu.nl/tags"" label=""No!"" scheme=""http://hl7.org/fhir/tag"" />" +
            @"<categoryX term=""http://www.furore.com/tags"" label=""Maybe"" scheme=""http://hl7.org/fhir/tag"" /></TagList>";


    }
}
