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
    public class BundleExtensionsTest
    {
        [TestMethod]
        public void ResourceListFiltering()
        {
            var rl = new List<BundleEntry>();

            rl.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/@1"), SelfLink = new Uri("http://x.com/@1/history/@1") });
            rl.Add(new ResourceEntry<Patient> { Id = new Uri("http://x.com/@1"), SelfLink = new Uri("http://x.com/@1/history/@2") });
            rl.Add(new ResourceEntry<CarePlan> { Id = new Uri("http://x.com/@2"), SelfLink = new Uri("http://x.com/@2/history/@1") });
            rl.Add(new DeletedEntry() { Id = new Uri("http://x.com/@2"), SelfLink = new Uri("http://x.com/@2/history/@2") });

            var tr = rl.ByResourceType<Patient>();
            Assert.AreEqual(2, tr.Count());
            var tr2 = rl.ByResourceType<CarePlan>();
            Assert.AreEqual(1, tr2.Count());

            var ur = rl.ById(new Uri("http://x.com/@1"));
            Assert.AreEqual(2, ur.Count());
            Assert.AreEqual(2, ur.ByResourceType<Patient>().Count());

            Assert.IsNotNull(ur.BySelfLink(new Uri("http://x.com/@1/history/@1")));
            Assert.IsNotNull(rl.BySelfLink(new Uri("http://x.com/@2/history/@2")));
        }
    }
}
