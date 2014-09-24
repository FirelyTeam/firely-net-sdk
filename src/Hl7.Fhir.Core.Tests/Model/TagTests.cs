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
using System.ComponentModel.DataAnnotations;


namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class TagTests
    {
        [TestMethod]
        public void TagEquality()
        {
            var t1 = new Tag("dog","http://nu.nl/tag");
            var t2 = new Tag("dog", new Uri("http://knmi.nl/tag") );
            var t3 = new Tag("dog", "http://knmi.nl/tag");

            Assert.AreNotEqual(t1, t2);
            Assert.AreNotEqual(t1, t3);
            Assert.AreEqual(t2, t3);            
        }


        [TestMethod]
        public void TagToString()
        {
            var tag = new Tag("dog", "http://test.nl/tags", "Een hond");
            Assert.AreEqual("dog@http://test.nl/tags (Een hond)", tag.ToString());

            tag = new Tag("dog", "http://test.nl/tags");
            Assert.AreEqual("dog@http://test.nl/tags", tag.ToString());
        }

        private static void verifyTagList(IList<Tag> tl)
        {
            Assert.AreEqual(2, tl.Count);
            Assert.AreEqual("No!", tl[0].Label);
            Assert.AreEqual("http://www.nu.nl/tags", tl[0].Term);
            Assert.AreEqual("Maybe, indeed", tl[1].Label);
            Assert.AreEqual("http://www.furore.com/tags", tl[1].Term);
        }  
    }
}
