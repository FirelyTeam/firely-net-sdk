/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public partial class ModelTests
    {
        [TestMethod]
        public void TestNamingSystemCanonical()
        {
            NamingSystem ns = new NamingSystem();

            Assert.IsNull(ns.Url);
            Assert.IsNull(ns.UrlElement);

            ns.Url = "http://nu.nl";

            Assert.AreEqual("http://nu.nl", ns.Url);
            Assert.AreEqual("http://nu.nl", ns.UrlElement.Value);
        }

        [TestMethod]
        public void TestCheckMinorVersionCompatibiliy()
        {
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("5.0.0"));
            Assert.IsTrue(ModelInfo.CheckMinorVersionCompatibility("5.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.6.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.6"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.0.1"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("4.0.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.2.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0.1"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3.0.2"));
            Assert.IsFalse(ModelInfo.CheckMinorVersionCompatibility("3"));
        }

        //If failed: change the description of the "STN" in the Currency enum of Money.cs from "SC#o TomC) and PrC-ncipe dobra" to "São Tomé and Príncipe dobra".
        [TestMethod]
        public void TestCorrectCurrencyDescription()
        {
            var currency = Currencies.STN;
            currency.GetDocumentation().Should().Be("São Tomé and Príncipe dobra");
        }
    }
}
