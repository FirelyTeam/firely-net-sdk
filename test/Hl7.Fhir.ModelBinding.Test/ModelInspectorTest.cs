using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.ModelBinding.Test
{
    [TestClass]
    public class ModelInspectorTest
    {
        [TestMethod]
        public void TestResourceClassInspection()
        {
            var inspector = new ModelInspector();

            inspector.Inspect(typeof(RoadResource));
            inspector.Inspect(typeof(Way));
            inspector.Inspect(typeof(ProfiledWay));

            var road = inspector.GetMappedClassForResource("roAd");
            Assert.IsNotNull(road);
            Assert.AreEqual(FhirModelConstruct.Resource, road.ModelConstruct);
            Assert.AreEqual("Road", road.Name);
            Assert.IsNull(road.Profile);
            Assert.AreEqual(road.ImplementingType, typeof(RoadResource));

            var way = inspector.GetMappedClassForResource("Way");
            Assert.IsNotNull(way);
            Assert.AreEqual("Way", way.Name);
            Assert.IsNull(way.Profile);
            Assert.AreEqual(way.ImplementingType, typeof(Way));

            var pway = inspector.GetMappedClassForResource("way", "http://nu.nl/profile#street");
            Assert.IsNotNull(pway);
            Assert.AreEqual("Way", pway.Name);
            Assert.AreEqual("http://nu.nl/profile#street", pway.Profile);
            Assert.AreEqual(pway.ImplementingType, typeof(ProfiledWay));

            var pway2 = inspector.GetMappedClassForResource("way", "http://nux.nl/profile#street");
            Assert.IsNotNull(pway2);
            Assert.AreEqual("Way", pway2.Name);
            Assert.IsNull(pway2.Profile);
            Assert.AreEqual(pway2.ImplementingType, typeof(Way));
            
            var noway = inspector.GetMappedClassForResource("nonexistent");
            Assert.IsNull(noway);
        }
    }



    public class RoadResource
    {

    }

    public class Way : Resource
    {

    }

    [FhirResource("Way", Profile="http://nu.nl/profile#street")]
    public class ProfiledWay
    {
    }

    
}
