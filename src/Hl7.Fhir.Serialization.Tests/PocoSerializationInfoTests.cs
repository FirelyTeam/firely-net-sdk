using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.Introspection
{
    [TestClass]
    public class PocoSerializationInfoTests
    {
        //[TestMethod]
        //public void TestResourceInfo()
        //{
        //    var ip = new PocoModelMetadataProvider();
        //    Assert.IsTrue(ip.IsResource("Patient"));
        //    Assert.IsTrue(ip.IsResource("DomainResource"));
        //    Assert.IsTrue(ip.IsResource("Resource"));
        //    Assert.IsFalse(ip.IsResource("Identifier"));
        //}

        [TestMethod]
        public void TestCanLocateTypes() => SerializationInfoTestHelpers.TestCanLocateTypes(new PocoSerializationInfoProvider());

        [TestMethod]
        public void TestCanGetElements() => SerializationInfoTestHelpers.TestCanGetElements(new PocoSerializationInfoProvider());

        [TestMethod]
        public void TestSpecialTypes() => SerializationInfoTestHelpers.TestSpecialTypes(new PocoSerializationInfoProvider());

        [TestMethod]
        public void TestProvidedOrder() => SerializationInfoTestHelpers.TestProvidedOrder(new PocoSerializationInfoProvider());
    }
}
