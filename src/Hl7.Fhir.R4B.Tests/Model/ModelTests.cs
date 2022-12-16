/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests.Model
{
    public partial class ModelTests
    {
        [TestMethod]
        public void TestNamingSystemCanonical()
        {
            NamingSystem ns = new NamingSystem();

            Assert.IsNull(ns.Url);
            Assert.IsNull(ns.UrlElement);

            ns.UniqueId.Add(new NamingSystem.UniqueIdComponent { Value = "http://nu.nl" });
            ns.UniqueId.Add(new NamingSystem.UniqueIdComponent { Value = "http://dan.nl", Preferred = true });

            Assert.AreEqual("http://dan.nl", ns.Url);
            Assert.AreEqual("http://dan.nl", ns.UrlElement.Value);

            ns.UniqueId[1].Preferred = false;

            Assert.AreEqual("http://nu.nl", ns.Url);
            Assert.AreEqual("http://nu.nl", ns.UrlElement.Value);
        }

        [TestMethod]
        public void TestCheckMinorVersionCompatibiliy()
        {
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("4.3.0"));
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("4.3"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.1.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.1"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.2.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0.1"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0.2"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3"));
        }
    }
}
