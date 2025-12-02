/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests
{
    public partial class ConformanceSourceTests
    {

        [TestMethod]
        public void GetSomeArtifactsById()
        {
            var fa = ZipSource.CreateValidationSource();

            var vs = fa.ResolveByUri("http://hl7.org/fhir/ValueSet/v2-0292");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);
            Assert.EndsWith("v2-tables.xml", vs.GetOrigin());

            vs = fa.ResolveByUri("http://hl7.org/fhir/ValueSet/administrative-gender");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            vs = fa.ResolveByUri("http://hl7.org/fhir/ValueSet/location-status");
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            var rs = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/Condition");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);
            Assert.EndsWith("profiles-resources.xml", rs.GetOrigin());

            rs = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/ValueSet");
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);

            var dt = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/Money");
            Assert.IsNotNull(dt);
            Assert.IsTrue(dt is StructureDefinition);

            // Try to find a core extension
            var ext = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/valueset-system");
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Try to find an additional non - hl7 profile(they are distributed with the spec for now)
            var us = fa.ResolveByUri("http://hl7.org/fhir/StructureDefinition/ehrsrle-auditevent");
            Assert.IsNotNull(us);
            Assert.IsTrue(us is StructureDefinition);
        }
    }
}
