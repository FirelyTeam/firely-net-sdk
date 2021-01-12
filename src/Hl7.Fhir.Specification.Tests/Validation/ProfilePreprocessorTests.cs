using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Xunit;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests.Validation
{
    public class ProfilePreprocessorTests
    {
        /// <summary>
        /// Test for issue 556 (https://github.com/FirelyTeam/firely-net-sdk/issues/556) 
        /// </summary>
        [Fact, Trait("Category", "LongRunner")]
        public async T.Task RunSnapshotMultiThreaded()
        {
            // Arrange
            var source = new CachedResolver(new ZipSource("specification.zip"));
            var generator = new SnapshotGenerator(source);

            OperationOutcome GenerateSnapshot(StructureDefinition sd)
            {
// We don't want to update ProfilePreprocessor right now
#pragma warning disable CS0618 // Type or member is obsolete
                generator.Update(sd);
#pragma warning restore CS0618 // Type or member is obsolete
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

            var patientSD = await source.ResolveByCanonicalUriAsync("http://hl7.org/fhir/StructureDefinition/Patient") as StructureDefinition;
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