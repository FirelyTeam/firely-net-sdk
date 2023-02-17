/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class RatioTest
    {
        [TestMethod]
        public void RatioConstructor()
        {
            accept("4 'cm':25 'm2'", 4, "cm", 25, "m2");
            accept("4 'cm' : 25 'm2'", 4, "cm", 25, "m2");
            accept("4 'cm':  25 'm2' ", 4, "cm", 25, "m2");
            reject("");
            reject("4 'cm'");
            reject(":");
            reject(": 25 'm2'");
            reject("Hi12:34:44");
            reject("12:34:44there");
            reject("12:34:44+A");
            reject("12:34:44+345:432");
            reject("92:34:44");
            reject("12:34:AM");

            static void accept(string testValue, decimal v1, string u1, decimal v2, string u2)
            {
                Assert.IsTrue(P.Ratio.TryParse(testValue, out var parsed), "TryParse");
                Assert.AreEqual(v1, parsed.Numerator.Value);
                Assert.AreEqual(u1, parsed.Numerator.Unit);
                Assert.AreEqual(v2, parsed.Denominator.Value);
                Assert.AreEqual(u2, parsed.Denominator.Unit);

                Assert.AreEqual(testValue.Replace(" ", ""), parsed.ToString().Replace(" ", ""), "ToString");
            }

            static void reject(string testValue)
            {
                Assert.IsFalse(P.Time.TryParse(testValue, out _));
            }
        }
    }
}