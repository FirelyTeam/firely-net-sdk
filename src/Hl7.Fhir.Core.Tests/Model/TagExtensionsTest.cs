/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
#if PORTABLE45
	public class PortableTagExtensionsTest
#else
	public class TagExtensionsTest
#endif
    {
        [TestMethod]
        public void TextTagHandling()
        {
            var r = new ResourceEntry<Patient>();
            var text = "<b>Bold! Eeuww  some /\\ url unfriendly stuff !@#$%^& //</b>";

            r.SetTextTag(text);

            var text2 = r.GetTextTag();

            Assert.AreEqual(text, text2);
        }

        [TestMethod]
        public void BundleTypeHandling()
        {
            Bundle b = new Bundle();

            Assert.IsTrue(b.GetBundleType() == BundleType.Unspecified);

            b.SetBundleType(BundleType.Message);
            Assert.AreEqual(BundleType.Message, b.GetBundleType());
            Assert.AreEqual(1,b.Tags.Count());

            b.SetBundleType(BundleType.Document);
            Assert.AreEqual(BundleType.Document, b.GetBundleType());
            Assert.AreEqual(1, b.Tags.Count());

            b.SetBundleType(BundleType.Unspecified);
            Assert.AreEqual(0, b.Tags.Count());
        }

        [TestMethod]
        public void ProfileAssertionHandling()
        {
            ResourceEntry<Patient> p = new ResourceEntry<Patient>();

            var prof1 = new Uri("http://profiles.com/important/profiles/1");
            var prof2 = new Uri("http://profiles.com/important/profiles/2");

            p.AddProfileAssertion(prof1);
            p.AddProfileAssertion(prof2);
            Assert.AreEqual(2, p.Tags.Count());

            Assert.IsTrue(p.GetAssertedProfiles().Contains(prof1));
            Assert.IsTrue(p.GetAssertedProfiles().Contains(prof2));

            p.RemoveProfileAssertion(prof2);
            Assert.IsTrue(p.GetAssertedProfiles().Contains(prof1));
            Assert.IsFalse(p.GetAssertedProfiles().Contains(prof2));
        }

        [TestMethod]
        public void TagFiltering()
        {
            IList<Tag> tl = new List<Tag>();

            tl.Add(new Tag("http://nu.nl", Tag.FHIRTAGSCHEME_GENERAL, "v1"));
            tl.Add(new Tag("http://dan.nl", Tag.FHIRTAGSCHEME_GENERAL, "v2"));
            tl.Add(new Tag("http://dan.nl", "http://crap.com", "v0"));

            Assert.AreEqual(2, tl.FilterByScheme(Tag.FHIRTAGSCHEME_GENERAL).Count());
        }


        [TestMethod]
        public void TagListExclusion()
        {
            IList<Tag> tl = new List<Tag>();
            var n1 = new Tag("http://nu.nl", Tag.FHIRTAGSCHEME_GENERAL, "v1"); 
            tl.Add(n1);
            tl.Add(new Tag("http://dan.nl", Tag.FHIRTAGSCHEME_GENERAL, "v2"));

            IList<Tag> tl2 = new List<Tag>();
            tl2.Add(new Tag("http://nu.nl", Tag.FHIRTAGSCHEME_GENERAL));
            tl2.Add(new Tag("http://nooit.nl", Tag.FHIRTAGSCHEME_GENERAL));

            var result = tl.Exclude(tl2);
            Assert.AreEqual(new Tag("http://dan.nl", Tag.FHIRTAGSCHEME_GENERAL), result.Single());

            result = tl.Exclude(n1);
            Assert.AreEqual(1, result.Count());
        }
    }
}
