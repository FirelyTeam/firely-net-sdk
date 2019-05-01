﻿/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
// Use alias to avoid conflict with Hl7.Fhir.Model.Task
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class SummarySourceTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static ISummarySource source = null;


        [TestMethod]
        public void GetSomeArtifactsBySummary()
        {
            var fa = source;

            var summaries = fa.ListSummaries();

            var summary = summaries.ResolveByCanonicalUri("http://terminology.hl7.org/ValueSet/v2-0292");
            Assert.IsNotNull(summary);
            var vs = fa.LoadBySummary(summary);
            Assert.IsTrue(vs is ValueSet);
            Assert.IsTrue(vs.GetOrigin().EndsWith("v2-tables.xml"));

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
            Assert.IsTrue(rs.GetOrigin().EndsWith("profiles-resources.xml"));

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

        [TestMethod]
        public void ListSummaries()
        {
            var source = new DirectorySource(Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings { IncludeSubDirectories = true });

            var sd = source.ListSummaries(ResourceType.StructureDefinition); Assert.IsTrue(sd.Any());
            var sm = source.ListSummaries(ResourceType.StructureMap); Assert.IsTrue(sd.Any());
            var cf = source.ListSummaries(ResourceType.CapabilityStatement); Assert.IsTrue(cf.Any());
            var md = source.ListSummaries(ResourceType.MessageDefinition); Assert.IsFalse(md.Any());
            var od = source.ListSummaries(ResourceType.OperationDefinition); Assert.IsTrue(od.Any());
            var sp = source.ListSummaries(ResourceType.SearchParameter); Assert.IsFalse(sp.Any());
            var cd = source.ListSummaries(ResourceType.CompartmentDefinition); Assert.IsFalse(md.Any());
            var ig = source.ListSummaries(ResourceType.ImplementationGuide); Assert.IsFalse(ig.Any());

            var cs = source.ListSummaries(ResourceType.CodeSystem); Assert.IsFalse(cs.Any());
            var vs = source.ListSummaries(ResourceType.ValueSet); Assert.IsTrue(vs.Any());
            var cm = source.ListSummaries(ResourceType.ConceptMap); Assert.IsFalse(cm.Any());
            // [WMR 20181218] R4 OBSOLETE - ExpansionProfile resource no longer exists
            // var ep = source.ListSummaries(ResourceType.ExpansionProfile); Assert.IsFalse(ep.Any());
            var ns = source.ListSummaries(ResourceType.NamingSystem); Assert.IsFalse(ns.Any());

            var all = source.ListSummaries();

            Assert.AreEqual(sd.Count() + sm.Count() + cf.Count() + md.Count() + od.Count() +
                        sp.Count() + cd.Count() + ig.Count() + cs.Count() + vs.Count() + cm.Count() +
                        /*ep.Count() +*/ ns.Count(), all.Count());
        }

        [TestMethod]
        public async Tasks.Task TestThreadSafety()
        {
            // Verify thread safety by resolving same uri simultaneously from different threads
            // DirectorySource should synchronize access and only call prepare once.

            const int threadCount = 25;
            const string uri = @"http://example.org/fhir/StructureDefinition/human-group";

            var source = new DirectorySource(Path.Combine(DirectorySource.SpecificationDirectory, "TestData", "snapshot-test"),
                new DirectorySourceSettings { IncludeSubDirectories = true });

            var tasks = new Tasks.Task[threadCount];
            var results = new(Resource resource, ArtifactSummary summary, int threadId, TimeSpan start, TimeSpan stop)[threadCount];

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < threadCount; i++)
            {
                var idx = i;
                tasks[i] = Tasks.Task.Run(
                    () =>
                    {
                        var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var start = sw.Elapsed;
                        var resource = source.ResolveByCanonicalUri(uri);
                        var summary = source.ListSummaries().ResolveByUri(uri);
                        var stop = sw.Elapsed;
                        results[idx] = (resource, summary, threadId, start, stop);
                    }
                );
            }

            await Tasks.Task.WhenAll(tasks);
            sw.Stop();

            var first = results[0];
            for (int i = 0; i < threadCount; i++)
            {
                var result = results[i];
                var duration = result.stop.Subtract(result.start);
                Debug.WriteLine($"{i:0#} Thread: {result.threadId:00#} | Start: {result.start.TotalMilliseconds:0000.00} | Stop: {result.stop.TotalMilliseconds:0000.00} | Duration: {duration.TotalMilliseconds:0000.00}");
                Assert.IsNotNull(result.resource);
                Assert.IsNotNull(result.summary);
                // Verify that all threads return the same summary instances
                Assert.AreSame(first.summary, result.summary);
            }
        }

    }
}