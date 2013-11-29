using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using Hl7.Fhir.Support;
using Hl7.Fhir.Client;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class TagParsingTests
    {
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

            string cat = HttpUtil.BuildCategoryHeader(parsedTags.FilterFhirTags());

            Assert.AreEqual(@"http://furore.com/tags/test1; label=""yes""; scheme=""http://hl7.org/fhir/tag"", http://furore.com/tags/test1; " +
                        @"label=""confusion, abounds - beyond!""; scheme=""http://hl7.org/fhir/tag""", cat);
        }


        [TestMethod]
        public void TagEquality()
        {
            var t1 = new Tag("dog");
            var t2 = new Tag("dog", new Uri("http://knmi.nl") );
            var t3 = new Tag("dog", "http://knmi.nl");

            Assert.AreNotEqual(t1, t2);
            Assert.AreNotEqual(t1, t3);
            Assert.AreEqual(t2, t3);            
        }


        [TestMethod]
        public void SerializeAndDeserializeTagList()
        {
            IList<Tag> tl = new List<Tag>();

            tl.Add(new Tag("http://www.nu.nl/tags", Tag.FHIRTAGSCHEME, "No!"));
            tl.Add(new Tag("http://www.furore.com/tags", Tag.FHIRTAGSCHEME, "Maybe, indeed" ));

            string json = FhirSerializer.SerializeTagListToJson(tl);
            Assert.AreEqual(jsonTagList, json);

            string xml = FhirSerializer.SerializeTagListToXml(tl);
            Assert.AreEqual(xmlTagList, xml);

            tl = FhirParser.ParseTagListFromXml(xml);
            verifyTagList(tl);

            tl = FhirParser.ParseTagListFromJson(json);
            verifyTagList(tl);
        }

        [TestMethod]
        public void CatchTagListParseErrors()
        {
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromXml(xmlTagListE1));           
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromXml(xmlTagListE2));
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromJson(jsonTagListE1));
            formatExceptionOrFail(()=> FhirParser.ParseTagListFromJson(jsonTagListE2));
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

        private string jsonTagList = @"{""taglist"":{""category"":[" +
            @"{""term"":""http://www.nu.nl/tags"",""label"":""No!"",""scheme"":""http://hl7.org/fhir/tag""}," +
            @"{""term"":""http://www.furore.com/tags"",""label"":""Maybe, indeed"",""scheme"":""http://hl7.org/fhir/tag""}]}}";

        private string jsonTagListE1 = @"{""Xtaglist"":{""category"":[" +
            @"{""term"":""http://www.nu.nl/tags"",""label"":""No!"",""scheme"":""http://hl7.org/fhir/tag""}," +
            @"{""term"":""http://www.furore.com/tags"",""label"":""Maybe"",""scheme"":""http://hl7.org/fhir/tag""}]}}";
        private string jsonTagListE2 = @"{""taglist"":{""Xcategory"":[" +
            @"{""term"":""http://www.nu.nl/tags"",""label"":""No!"",""scheme"":""http://hl7.org/fhir/tag""}," +
            @"{""term"":""http://www.furore.com/tags"",""label"":""Maybe"",""scheme"":""http://hl7.org/fhir/tag""}]}}";


        private string xmlTagList = @"<?xml version=""1.0"" encoding=""utf-16""?><taglist xmlns=""http://hl7.org/fhir"">" +
            @"<category term=""http://www.nu.nl/tags"" label=""No!"" scheme=""http://hl7.org/fhir/tag"" />" +
            @"<category term=""http://www.furore.com/tags"" label=""Maybe, indeed"" scheme=""http://hl7.org/fhir/tag"" /></taglist>";
        private string xmlTagListE1 = @"<?xml version=""1.0"" encoding=""utf-16""?><Xtaglist xmlns=""http://hl7.org/fhir"">" +
            @"<category term=""http://www.nu.nl/tags"" label=""No!"" scheme=""http://hl7.org/fhir/tag"" />" +
            @"<category term=""http://www.furore.com/tags"" label=""Maybe"" scheme=""http://hl7.org/fhir/tag"" /></taglist>";
        private string xmlTagListE2 = @"<?xml version=""1.0"" encoding=""utf-16""?><taglist xmlns=""http://hl7.org/fhir"">" +
            @"<category term=""http://www.nu.nl/tags"" label=""No!"" scheme=""http://hl7.org/fhir/tag"" />" +
            @"<categoryX term=""http://www.furore.com/tags"" label=""Maybe"" scheme=""http://hl7.org/fhir/tag"" /></taglist>";


    }
}
