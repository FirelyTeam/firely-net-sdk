using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Support.Tests
{
    [Obsolete("The class `ObjectListExtensions` is obsolete and will be removed in the next major release. Obsolete since 2021-09-22")]
    [TestClass]
    public class ObjectListExtensionTests
    {
        [TestMethod]
        public void RemoveOfTypeSucceedsIfNoItemPresent()
        {
            var list = new List<object>();
            try
            {
                list.RemoveOfType(typeof(string));
            }
            catch (Exception ex)
            {
                Assert.Fail($"RemoveOfType should not throw an exception, but it did: {ex.Message}");
            }
        }

        [TestMethod]
        public void RemoveOfTypeFailsIfListIsNull()
        {
            List<object> list = null;
            try
            {
                list.RemoveOfType(typeof(string));
                Assert.Fail($"RemoveOfType should throw an exception if the list is null");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void RemoveOfTypeSucceedsIfNoItemOfTypePresent()
        {
            var list = new List<object> { (Int32)5 };
            try
            {
                list.RemoveOfType(typeof(string));
            }
            catch (Exception ex)
            {
                Assert.Fail($"RemoveOfType should not throw an exception, but it did: {ex.Message}");
            }
        }

        [TestMethod]
        public void RemoveOfTypeSucceedsIfMultipleItemsOfTypePresent_Last()
        {
            var list = new List<object> { (Int32)5, (Int32)7, "foo", "bar" };
            try
            {
                list.RemoveOfType(typeof(string));
                Assert.AreEqual(2, list.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail($"RemoveOfType should not throw an exception, but it did: {ex.Message}");
            }
        }

        [TestMethod]
        public void RemoveOfTypeSucceedsIfMultipleItemsOfTypePresent_First()
        {
            var list = new List<object> { (Int32)5, (Int32)7, "foo", "bar" };
            try
            {
                list.RemoveOfType(typeof(Int32));
                Assert.AreEqual(2, list.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail($"RemoveOfType should not throw an exception, but it did: {ex.Message}");
            }
        }
    }
}
