using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass()]
    public class AnnotationListTests
    {
        [TestMethod()]
        public void BasicAnnotationsTests()
        {
            // Arrange
            var list = new AnnotationList();
            var annotation = new Object();

            // Add annotation
            Assert.IsTrue(list.IsEmpty);
            list.AddAnnotation(annotation);
            Assert.IsFalse(list.IsEmpty);

            // Get annontations of type
            var result = list.OfType(typeof(object));
            Assert.AreEqual(annotation, result.FirstOrDefault());

            // Remove annontations of type
            list.RemoveAnnotations(typeof(object));
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod()]
        public void AddAnnotationTest()
        {
            // Arrange
            var list = new AnnotationList();

            list.AddAnnotation(1);
            list.AddAnnotation(2);
            list.AddAnnotation(false);
            list.AddAnnotation(new object());

            Assert.IsFalse(list.IsEmpty);
            Assert.AreEqual(2, list.OfType(typeof(int)).Count());
            Assert.AreEqual(1, list.OfType(typeof(bool)).Count());
            Assert.AreEqual(1, list.OfType(typeof(object)).Count());

            // Remove annontations of type
            list.RemoveAnnotations(typeof(bool));
            Assert.IsFalse(list.IsEmpty);
            Assert.AreEqual(0, list.OfType(typeof(bool)).Count());
            Assert.AreEqual(2, list.OfType(typeof(int)).Count());
            Assert.AreEqual(1, list.OfType(typeof(object)).Count());
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            // Arrange
            var source = new AnnotationList();
            source.AddAnnotation(4);

            var destination = new AnnotationList();
            destination.AddAnnotation(true);

            // Act
            destination.AddRange(source);

            // Assert
            Assert.IsFalse(destination.IsEmpty);
            Assert.IsFalse(destination.OfType(typeof(bool)).Any());
            Assert.IsTrue(destination.OfType(typeof(int)).Any());
            Assert.AreEqual(4, destination.Annotations(typeof(int)).FirstOrDefault());
        }

        private static object innerDictionary(AnnotationList list) =>
            typeof(AnnotationList).GetField("_annotations", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(list);

        [TestMethod]
        public void ReadsAndRemovesOnAFreshListDoNotAllocate()
        {
            var list = new AnnotationList();

            Assert.IsTrue(list.IsEmpty);
            Assert.IsFalse(list.Annotations(typeof(int)).Any());
            Assert.IsFalse(list.OfType(typeof(int)).Any());
            Assert.IsFalse(list.TryGetAnnotation<string>(out _));
            Assert.IsFalse(list.Any());
            list.RemoveAnnotations(typeof(int));

            Assert.IsNull(innerDictionary(list), "Reading or removing should not create the dictionary.");

            list.AddAnnotation(1);
            Assert.IsNotNull(innerDictionary(list));
            Assert.AreEqual(1, list.OfType(typeof(int)).Single());
        }

        [TestMethod]
        public void SetAnnotationOnAFreshListCreatesIt()
        {
            var list = new AnnotationList();
            list.SetAnnotation("a");
            list.SetAnnotation("b");

            Assert.AreEqual("b", list.OfType(typeof(string)).Single());
        }

        [TestMethod]
        public void AddRangeFromEmptySourceCopiesNothing()
        {
            var source = new AnnotationList();
            _ = source.IsEmpty;   // a read must not create anything either

            var destination = new AnnotationList();
            destination.AddRange(source);

            Assert.IsTrue(destination.IsEmpty);
            Assert.IsNull(innerDictionary(source));
            Assert.IsNull(innerDictionary(destination));
        }

        [TestMethod]
        public void AddRangeFromEmptySourceClearsExistingAnnotations()
        {
            var destination = new AnnotationList();
            destination.AddAnnotation(true);

            destination.AddRange(new AnnotationList());

            Assert.IsTrue(destination.IsEmpty);
        }

        [TestMethod]
        public void AddRangeFromEmptiedSourceCopiesNothing()
        {
            var source = new AnnotationList();
            source.AddAnnotation(4);
            source.RemoveAnnotations(typeof(int));

            var destination = new AnnotationList();
            destination.AddRange(source);

            Assert.IsTrue(destination.IsEmpty);
            Assert.IsNull(innerDictionary(destination));
        }

        [TestMethod]
        public void AddRangeCopyIsIndependentOfTheSource()
        {
            var source = new AnnotationList();
            source.AddAnnotation(4);

            var destination = new AnnotationList();
            destination.AddRange(source);

            // Changes to either side do not show up in the other.
            source.AddAnnotation(5);
            source.AddAnnotation("only in source");
            destination.AddAnnotation(6);
            destination.AddAnnotation("only in destination");

            CollectionAssert.AreEqual(new object[] { 4, 5 }, source.OfType(typeof(int)).ToList());
            CollectionAssert.AreEqual(new object[] { 4, 6 }, destination.OfType(typeof(int)).ToList());
            Assert.AreEqual("only in source", source.OfType(typeof(string)).Single());
            Assert.AreEqual("only in destination", destination.OfType(typeof(string)).Single());

            source.RemoveAnnotations(typeof(int));
            Assert.AreEqual(2, destination.OfType(typeof(int)).Count());
        }

        [TestMethod]
        public void EnumerationReturnsAllAnnotations()
        {
            var list = new AnnotationList();
            list.AddAnnotation(1);
            list.AddAnnotation(2);
            list.AddAnnotation("x");

            CollectionAssert.AreEquivalent(new object[] { 1, 2, "x" }, list.ToList());
        }

        [TestMethod]
        public void ConcurrentFirstWritesDoNotLoseAnnotations()
        {
            const int threadCount = 16;
            const int perThread = 100;

            for (var round = 0; round < 50; round++)
            {
                var list = new AnnotationList();
                using var barrier = new Barrier(threadCount);

                var tasks = Enumerable.Range(0, threadCount).Select(t => Task.Factory.StartNew(() =>
                {
                    barrier.SignalAndWait();     // all threads race for the very first write
                    for (var i = 0; i < perThread; i++)
                        list.AddAnnotation(t * perThread + i);
                }, TaskCreationOptions.LongRunning)).ToArray();

                Task.WaitAll(tasks);

                var all = list.OfType(typeof(int)).Cast<int>().OrderBy(i => i).ToList();
                CollectionAssert.AreEqual(Enumerable.Range(0, threadCount * perThread).ToList(), all);
            }
        }

        [TestMethod]
        public void ConcurrentFirstWritesOfDifferentTypesAreAllKept()
        {
            var types = new object[] { 1, "s", 2.0, 3L, true, 'c', (byte)1, (short)2 };

            for (var round = 0; round < 50; round++)
            {
                var list = new AnnotationList();
                using var barrier = new Barrier(types.Length);

                Task.WaitAll(types.Select(a => Task.Factory.StartNew(() =>
                {
                    barrier.SignalAndWait();
                    list.AddAnnotation(a);
                }, TaskCreationOptions.LongRunning)).ToArray());

                foreach (var a in types)
                    Assert.AreEqual(a, list.OfType(a.GetType()).Single());
            }
        }
    }
}