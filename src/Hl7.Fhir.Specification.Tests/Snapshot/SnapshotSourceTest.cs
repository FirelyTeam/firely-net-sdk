using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Snapshot;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class SnapshotSourceTest
    {
        [TestMethod]
        public void TestElementSnapshot()
        {
            // Request core Element snapshot; verify recursion handling

            var orgSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(orgSource);

            // Assumption: source provides Element structure
            var sdCached = cachedSource.FindStructureDefinitionForCoreType(FHIRDefinedType.Element);
            Assert.IsNotNull(sdCached);
            Assert.IsNotNull(sdCached.Differential);
            var elemCnt = sdCached.Differential.Element.Count;
            Assert.AreEqual(3, elemCnt); // Element | Element.id | Element.extension

            // Generate snapshot by calling SnapshotSource
            // Important! Specify flag to force re-generation (don't trust existing core snapshots...)
            var snapSource = new SnapshotSource(cachedSource, true);

            var sd = snapSource.FindStructureDefinitionForCoreType(FHIRDefinedType.Element);
            Assert.IsNotNull(sd);
            Assert.AreEqual(sdCached, sd);   // Expecting same (cached) object reference, with updated Snapshot component
            Assert.IsTrue(sd.HasSnapshot);
            Assert.IsTrue(sd.Snapshot.IsCreatedBySnapshotGenerator());

            var elems = sd.Snapshot.Element;
            Assert.AreEqual(elemCnt, elems.Count);

            void assert_ele1(ElementDefinition eld)
            {
                Assert.AreEqual("ele-1", eld.Constraint.FirstOrDefault()?.Key);
                Assert.AreEqual("ele-1", eld.Condition.FirstOrDefault());
            }

            // Assumption: differential introduces constraint "ele-1" on root element (only)
            var diffElems = sd.Differential.Element;
            assert_ele1(diffElems[0]);
            for (int i = 1; i < elemCnt; i++)
            {
                var elem = diffElems[i];
                Assert.IsFalse(elem.Constraint.Any());
                Assert.IsFalse(elem.Condition.Any());
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
                assert_ele1(elem);
            }
        }

        [TestMethod]
        public void CannotCreateSnapshotGeneratorFromSnapshotSource()
        {
            var orgSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(orgSource);
            var src = new SnapshotSource(cachedSource);

            // Verify that SnapshotGenerator ctor rejects SnapshotSource arguments
            Assert.ThrowsException<ArgumentException>(() => new SnapshotGenerator(src));
        }

        [TestMethod]
        public void CannotCreateNestedSnapshotSource()
        {
            var orgSource = ZipSource.CreateValidationSource();
            var cachedSource = new CachedResolver(orgSource);
            var src = new SnapshotSource(cachedSource);

            // Verify that SnapshotSource ctor rejects SnapshotSource arguments
            Assert.ThrowsException<ArgumentException>(() => new SnapshotSource(src));
        }

    }
}
