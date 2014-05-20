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

namespace Hl7.Fhir.Test
{
    [TestClass]
#if PORTABLE45
	public class PortableBundleExtensionsTest
#else
	public class BundleExtensionsTest
#endif
    {
        [TestMethod]
        public void ResourceListFiltering()
        {
            var rl = new List<BundleEntry>();

            rl.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/Patient/1"), SelfLink = new Uri("http://x.com/Patient/1/_history/1") });
            rl.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/Patient/1"), SelfLink = new Uri("http://x.com/Patient/1/_history/2") });
            rl.Add(new ResourceEntry<CarePlan> { Id = new Uri("http://x.com/Patient/2"), SelfLink = new Uri("http://x.com/Patient/2/_history/1") });
            rl.Add(new DeletedEntry() { Id = new Uri("http://x.com/Patient/2"), SelfLink = new Uri("http://x.com/Patient/2/_history/2") });

            var tr = rl.ByResourceType<Patient>();
            Assert.AreEqual(2, tr.Count());
            var tr2 = rl.ByResourceType<CarePlan>();
            Assert.AreEqual(1, tr2.Count());

            var ur = rl.ById(new Uri("http://x.com/Patient/1"));
            Assert.AreEqual(2, ur.Count());
            Assert.AreEqual(2, ur.ByResourceType<Patient>().Count());

            Assert.IsNotNull(ur.BySelfLink(new Uri("http://x.com/Patient/1/_history/1")));
            Assert.IsNotNull(rl.BySelfLink(new Uri("http://x.com/Patient/2/_history/2")));
        }

        [TestMethod]
        public void BundleEntryByReference()
        {
            Bundle b = new Bundle();

            b.Entries.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/Patient/1"), SelfLink = new Uri("http://x.com/Patient/1/_history/1") });
            b.Entries.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/Patient/1"), SelfLink = new Uri("http://x.com/Patient/1/_history/2") });
            b.Entries.Add(new ResourceEntry<Patient> { Id = new Uri("http://y.com/Patient/1"), SelfLink = new Uri("http://y.com/Patient/1") });

            b.Links.Base = new Uri("http://x.com");

            Assert.AreEqual(2, b.FindEntryByReference(new Uri("Patient/1", UriKind.Relative)).Count());
            Assert.AreEqual(1, b.FindEntryByReference(new Uri("Patient/1/_history/1", UriKind.Relative)).Count());
            Assert.AreEqual(2, b.FindEntryByReference(new Uri("http://y.com/Patient/1")).Count());
        }
    }
}
