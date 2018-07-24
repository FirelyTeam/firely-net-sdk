using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Xunit;

namespace Hl7.Fhir.Specification.Tests.Validation
{
    [Trait("Category", "Validation")]
    public class ProfilePreprocessorTests
    {
        /// <summary>
        /// Test for issue 556 (https://github.com/ewoutkramer/fhir-net-api/issues/556) 
        /// </summary>
        [Fact]
        public async void RunSnapshotMultiThreaded()
        {
            // Arrange
            var source = new CachedResolver(new ZipSource("specification.zip"));
            var generator = new SnapshotGenerator(source);

            OperationOutcome GenerateSnapshot(StructureDefinition sd)
            {
                generator.Update(sd);
                System.Diagnostics.Debug.WriteLine(sd.HasSnapshot);
                return generator.Outcome ?? new OperationOutcome();
            }

            var nrOfParrallelTasks = 100;
            var results = new ConcurrentBag<OperationOutcome>();
            var buffer = new BufferBlock<StructureDefinition>();
            var processor = new ActionBlock<StructureDefinition>(sd =>
                {
                    // Act
                    var outcome = ProfilePreprocessor.GenerateSnapshots(new[] { sd }, GenerateSnapshot, "unittest");
                    results.Add(outcome);
                }
                ,
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 100
                });
            buffer.LinkTo(processor, new DataflowLinkOptions { PropagateCompletion = true });

            var patientSD = source.ResolveByCanonicalUri("http://hl7.org/fhir/StructureDefinition/Patient") as StructureDefinition;
            // Clear snapshots after initial load
            // This will force the validator to regenerate all snapshots
            if (patientSD.HasSnapshot)
            {
                patientSD.Snapshot = null;
            }

            for (int i = 0; i < nrOfParrallelTasks; i++)
            {
                buffer.Post(patientSD);
            }
            buffer.Complete();
            await processor.Completion;

            // Assert
            var nrOfSuccess = results.Count(r => r.Success);
            Assert.Equal(nrOfParrallelTasks, nrOfSuccess);
        }
    }
}