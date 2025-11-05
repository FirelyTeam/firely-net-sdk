/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// Use alias to avoid conflict with Hl7.Fhir.Model.Task

namespace Hl7.Fhir.Specification.Tests
{
    public partial class SummarySourceTests
    {
        [TestMethod]
        public void GetSomeArtifactsBySummary()
        {
            var fa = source;

            var summaries = fa.ListSummaries();

            var summary = summaries.ResolveByCanonicalUri("http://terminology.hl7.org/ValueSet/v2-0292");
            Assert.IsNotNull(summary);
            var vs = fa.LoadBySummary(summary);
            Assert.IsTrue(vs is ValueSet);
            Assert.EndsWith("v2-tables.xml", vs.GetOrigin());

            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/administrative-gender");
            Assert.IsNotNull(summary);
            vs = fa.LoadBySummary(summary);
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/ValueSet/location-status");
            Assert.IsNotNull(summary);
            vs = fa.LoadBySummary(summary);
            Assert.IsNotNull(vs);
            Assert.IsTrue(vs is ValueSet);

            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Condition");
            Assert.IsNotNull(summary);
            var rs = fa.LoadBySummary(summary);
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);
            Assert.EndsWith("profiles-resources.xml", rs.GetOrigin());

            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/ValueSet");
            Assert.IsNotNull(summary);
            rs = fa.LoadBySummary(summary);
            Assert.IsNotNull(rs);
            Assert.IsTrue(rs is StructureDefinition);

            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Money");
            Assert.IsNotNull(summary);
            var dt = fa.LoadBySummary(summary);
            Assert.IsNotNull(dt);
            Assert.IsTrue(dt is StructureDefinition);

            // Try to find a core extension
            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/valueset-system");
            Assert.IsNotNull(summary);
            var ext = fa.LoadBySummary(summary);
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext is StructureDefinition);

            // Try to find an additional US profile (they are distributed with the spec for now)
            summary = summaries.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/ehrsrle-auditevent");
            Assert.IsNotNull(summary);
            var us = fa.LoadBySummary(summary);
            Assert.IsNotNull(us);
            Assert.IsTrue(us is StructureDefinition);
        }
    }
}