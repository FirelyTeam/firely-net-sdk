/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    [TestClass]
    public class CommonTypeParsingTest
    {
        [TestMethod]
        public void CanConvertPocoToTypedElement()
        {
            Coding c = new Coding("http://nu.nl", "bla");
            var te = c.ToTypedElement(ModelInspector.Common);
            Assert.AreEqual("Coding", te.InstanceType);

            Coding c2 = te.ToPoco<Coding>(ModelInspector.Common);

            Assert.AreEqual(c.Code, c2.Code);
            Assert.AreEqual(c.System, c2.System);
        }

        [TestMethod]
        public void CanConvertPocoToSourceNode()
        {
            Coding c = new Coding("http://nu.nl", "bla");
            var sn = c.ToSourceNode(ModelInspector.Common, "kode");
            Assert.AreEqual("kode", sn.Name);

            Coding c2 = sn.ToPoco<Coding>(ModelInspector.Common);

            Assert.AreEqual(c.Code, c2.Code);
            Assert.AreEqual(c.System, c2.System);
        }
    }
}
