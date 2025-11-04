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
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
// Use alias to avoid conflict with Hl7.Fhir.Model.Task
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public partial class SummarySourceTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static ISummarySource source = null;

        [TestMethod]
        public void ListSummaries()
        {
            var sd = source.ListSummaries(ResourceType.StructureDefinition); Assert.IsNotEmpty(sd);
            var sm = source.ListSummaries(ResourceType.StructureMap); Assert.IsNotEmpty(sd);
            var cf = source.ListSummaries(ResourceType.CapabilityStatement); Assert.IsNotEmpty(cf);
            var md = source.ListSummaries(ResourceType.MessageDefinition); Assert.IsEmpty(md);
            var od = source.ListSummaries(ResourceType.OperationDefinition); Assert.IsNotEmpty(od);
            var sp = source.ListSummaries(ResourceType.SearchParameter); Assert.IsNotEmpty(sp);
            var cd = source.ListSummaries(ResourceType.CompartmentDefinition); Assert.IsEmpty(md);
#if R5
            // In specification.zip for R5 are now also ImplementationGuides
            var ig = source.ListSummaries(ResourceType.ImplementationGuide); Assert.IsNotEmpty(ig);
#else
            var ig = source.ListSummaries(ResourceType.ImplementationGuide); Assert.IsEmpty(ig);
#endif
            var cs = source.ListSummaries(ResourceType.CodeSystem); Assert.IsNotEmpty(cs);
            var vs = source.ListSummaries(ResourceType.ValueSet); Assert.IsNotEmpty(vs);
            var cm = source.ListSummaries(ResourceType.ConceptMap); Assert.IsNotEmpty(cm);
            // [WMR 20181218] R4 OBSOLETE - ExpansionProfile resource no longer exists
            // var ep = source.ListSummaries(ResourceType.ExpansionProfile); Assert.IsEmpty(ep);
#if !R5
            // In specification.zip for R5 there are no NamingSystems anymore.
            var ns = source.ListSummaries(ResourceType.NamingSystem);
            Assert.IsNotEmpty(ns);
#endif
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
            var results = new (Resource resource, ArtifactSummary summary, int threadId, TimeSpan start, TimeSpan stop)[threadCount];

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