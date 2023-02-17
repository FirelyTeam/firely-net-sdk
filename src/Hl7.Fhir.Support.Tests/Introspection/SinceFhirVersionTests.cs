/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class SinceFhirVersionTests
    {
        [TestMethod]
        public void TestSinceParameterOnAttribute()
        {
            var fea = new FhirElementAttribute("test") { Since = FhirRelease.STU3 };
            Assert.IsFalse(fea.AppliesToRelease(FhirRelease.DSTU1));
            Assert.IsFalse(fea.AppliesToRelease(FhirRelease.DSTU2));
            Assert.IsTrue(fea.AppliesToRelease(FhirRelease.STU3));
            Assert.IsTrue(fea.AppliesToRelease(FhirRelease.R4));
            Assert.IsTrue(fea.AppliesToRelease((FhirRelease)int.MaxValue));

            fea = new FhirElementAttribute("test2") { };
            Assert.IsTrue(fea.AppliesToRelease(FhirRelease.DSTU1));
            Assert.IsTrue(fea.AppliesToRelease(FhirRelease.DSTU2));
            Assert.IsTrue(fea.AppliesToRelease(FhirRelease.STU3));
            Assert.IsTrue(fea.AppliesToRelease((FhirRelease)int.MaxValue));

            var fra = new ReferencesAttribute() { Since = FhirRelease.STU3 };
            Assert.IsFalse(fra.AppliesToRelease(FhirRelease.DSTU1));
            Assert.IsFalse(fra.AppliesToRelease(FhirRelease.DSTU2));
            Assert.IsTrue(fra.AppliesToRelease(FhirRelease.STU3));
            Assert.IsTrue(fra.AppliesToRelease(FhirRelease.R4));
            Assert.IsTrue(fra.AppliesToRelease((FhirRelease)int.MaxValue));
        }
    }
}
