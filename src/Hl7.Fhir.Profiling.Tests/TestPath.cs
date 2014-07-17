using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Fhir.Profiling;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class PathTesting
    {
        [TestMethod]
        public void FollowPath()
        {
            /* 
            Profile profile = Profiles.ValueSet;

            Structure valueset = t.profile.GetStructureByName("ValueSet");
            
            Element target = t.profile.FollowPath(valueset.Root, new Path("version"));
            Assert.AreEqual("version", target.Name);

            target = t.profile.FollowPath(valueset.Root, new Path("define.concept.extension"));
            Assert.AreEqual("extension", target.Name);
            */
        }
    }
}
