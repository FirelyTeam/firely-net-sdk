using FluentAssertions;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Support.Tests
{
    [TestClass]

    public class EnumerableExtensionTests
    {
        [TestMethod]
        public void FiltersNullable()
        {
            var testee = new List<int?> { 1, null, 2 };
            testee.WithValues().Should().BeEquivalentTo(new[] { 1, 2 });

            testee = new List<int?> { null, null };
            testee.WithValues().Should().BeEquivalentTo(Enumerable.Empty<int>());

            var testee2 = new List<FileMode?> { FileMode.Open };
            testee2.WithValues().Should().BeEquivalentTo(new[] { FileMode.Open });
        }
    }
}
