using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
            Assert.HasCount(list.OfType(typeof(int)), 2);
            Assert.HasCount(list.OfType(typeof(bool)), 1);
            Assert.HasCount(list.OfType(typeof(object)), 1);

            // Remove annontations of type
            list.RemoveAnnotations(typeof(bool));
            Assert.IsFalse(list.IsEmpty);
            Assert.HasCount(list.OfType(typeof(bool)), 0);
            Assert.HasCount(list.OfType(typeof(int)), 2);
            Assert.HasCount(list.OfType(typeof(object)), 1);
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
    }
}