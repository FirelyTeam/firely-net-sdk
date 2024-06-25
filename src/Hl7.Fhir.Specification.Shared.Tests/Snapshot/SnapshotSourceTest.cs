using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass, TestCategory("Snapshot")]
    public class SnapshotSourceTest
    {
        [TestMethod]
        public async Tasks.Task TestElementSnapshot()
        {
            var zipSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(zipSource);
            var snapSource = new SnapshotSource(cachedSource, regenerate:true);
            // Request core Element snapshot; verify recursion handling

            // Assumption: source provides Element structure
            var sdCached = await cachedSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            Assert.IsNotNull(sdCached);
            Assert.IsNotNull(sdCached.Differential);
            var elemCnt = sdCached.Differential.Element.Count;
            Assert.AreEqual(3, elemCnt); // Element | Element.id | Element.extension

            // Generate snapshot by calling SnapshotSource
            // Important! Specify flag to force re-generation (don't trust existing core snapshots...)
            var sd = await snapSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            Assert.IsNotNull(sd);
            Assert.AreEqual(sdCached, sd); // Expecting same (cached) object reference, with updated Snapshot component
            Assert.IsTrue(sd.HasSnapshot);
            Assert.IsTrue(sd.Snapshot.IsCreatedBySnapshotGenerator());

            var elems = sd.Snapshot.Element;
            Assert.AreEqual(elemCnt, elems.Count);

            void assert_ele1(ElementDefinition eld)
            {
                Assert.AreEqual("ele-1", eld.Constraint.FirstOrDefault()?.Key);
#if !R5
                // In R5 ele-1 is not a condition anymore
                Assert.AreEqual("ele-1", eld.Condition.FirstOrDefault());
#endif
            }

            // Assumption: differential introduces constraint "ele-1" on root element (only)
            var diffElems = sd.Differential.Element;
            assert_ele1(diffElems[0]);
            for (int i = 1; i < elemCnt; i++)
            {
                var elem = diffElems[i];
                Assert.IsFalse(elem.Constraint.Any());
#if !R5
                // In R5 ele-1 is not a condition anymore
                Assert.IsFalse(elem.Condition.Any());
#endif
            }

            // Verify explicit inheritance of recursive constraints in snapshot
            //
            // [0] Element
            // [1] Element.id        : id < string < Element
            // [2] Element.extension : Extension < Element
            //
            // Verify that the "ele-1" constraint & condition, introduced by diff root element [0],
            // is correctly propagated to snapshot child elements [1] and [2]
            foreach (var elem in elems)
            {
                // [WMR 20181218] R4 Changed
                // STU3: Element.id has type code "string"
                // R4: Element.id has no type code, only special "compiler magic" extensions
                // => Element.id no longer inherits constraints from "Element", e.g. "ele-1"
                if (elem.Type?.FirstOrDefault()?.Code is string typeName &&
                    !typeName.StartsWith("http://hl7.org/fhirpath/System."))
                {
                    assert_ele1(elem);
                }
            }
        }

        [TestMethod]
        public void CannotCreateSnapshotGeneratorFromSnapshotSource()
        {
            var zipSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(zipSource);
            var snapSource = new SnapshotSource(cachedSource, regenerate:true);

            // Verify that SnapshotGenerator ctor rejects SnapshotSource arguments
            Assert.ThrowsException<ArgumentException>(() => new SnapshotGenerator(snapSource));
        }

        [TestMethod]
        public void CannotCreateNestedSnapshotSource()
        {
            var zipSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(zipSource);
            var snapSource = new SnapshotSource(cachedSource, regenerate:true);

            // Verify that SnapshotSource ctor rejects SnapshotSource arguments
            Assert.ThrowsException<ArgumentException>(() => new SnapshotSource(snapSource));
        }

        [TestMethod]
        public async Tasks.Task Generate_ForceRegenerate_DoesNotReuse_SnapshotCreatedByOthers()
        {
            var zipSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(zipSource);
            var snapSource = new SnapshotSource(cachedSource, regenerate:true);

            var original = await cachedSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            Assert.IsTrue(original.HasSnapshot);
            var originalSnapshot = original.Snapshot;
            var regenerated =
                await snapSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            Assert.IsTrue(regenerated.HasSnapshot);
            Assert.AreNotSame(originalSnapshot, regenerated.Snapshot);
        }

        [TestMethod]
        public async Tasks.Task Generate_ForceRegenerate_Reuses_SelfCreatedSnapshot()
        {
            var zipSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(zipSource);
            var snapSource = new SnapshotSource(cachedSource, regenerate:true);

            var first =
                await snapSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            var firstSnapshot = first.Snapshot;
            var second =
                await snapSource.FindStructureDefinitionForCoreTypeAsync(FHIRAllTypes.Element);
            Assert.AreSame(firstSnapshot, second.Snapshot);
        }
    }
}