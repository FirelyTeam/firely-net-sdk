using FluentAssertions;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EM= Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel
{
    [TestClass]
    public class TypedElementExtensionsTests
    {
        private readonly IStructureDefinitionSummaryProvider _provider = new NoTypeProvider();

        [DynamicData(nameof(getEqualityData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void IsExactlyEqualToTest(IReadOnlyDictionary<string, object> left, IReadOnlyDictionary<string, object> right, bool ignoreOrder, bool result)
        {
            // Act
            toTypedElement(left).IsExactlyEqualTo(toTypedElement(right), ignoreOrder).Should().Be(result);
        }

        [DynamicData(nameof(getMatchesData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void MatchesTest(IReadOnlyDictionary<string, object> input, IReadOnlyDictionary<string, object> pattern, bool result)
        {
            // Act
            toTypedElement(input).Matches(toTypedElement(pattern)).Should().Be(result);
        }

        [DynamicData(nameof(getValueEqualityData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void ValueEqualityTest(object left, object right, bool result)
        {
            // Act
            TypedElementExtensions.ValueEquality(left, right).Should().Be(result);
        }

        private static IEnumerable<object[]> getEqualityData()
        {
            yield return new object[]
            {
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "B", false },
                    { "C", 0 },
                    { "D", 2 },
                },
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "B", false },
                    { "C", 0 },
                    { "D", 2 },
                },
                false,
                true
            };

            yield return new object[]
            {
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "B", false },
                    { "C", 0 },
                    { "D", 2 },
                },
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "D", 2 },
                    { "B", false },
                    { "C", 0 },
                },
                false,
                false
            };

            yield return new object[]
            {
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "B", false },
                    { "C", 0 },
                    { "D", 2 },
                },
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "D", 2 },
                    { "B", false },
                    { "C", 0 },
                },
                true,
                true
            };
        }

        private static IEnumerable<object[]> getMatchesData()
        {
            yield return new object[]
            {
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "B", false },
                    { "C", 0 },
                    { "D", 2 },
                },
                new Dictionary<string, object>()
                {
                    { "A", true }
                },
                true
            };

            yield return new object[]
            {
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "B", false },
                    { "C", 0 },
                    { "D", 2 },
                },
                new Dictionary<string, object>()
                {
                    { "A", true },
                    { "D", 2 }
                },
                true
            };

            yield return new object[]
            {
                new Dictionary<string, object>()
                { { "A", true } },
                new Dictionary<string, object>()
                { { "A", 0 },
                  { "B", 1 } },
                false
            };
        }

        private static IEnumerable<object[]> getValueEqualityData()
        {
            yield return new object[] { null, null, true };
            yield return new object[] { null, 1, false };
            yield return new object[] { 1, null, false };
            yield return new object[] { 3, 3, true };
            yield return new object[] { 3, 2, false };
            yield return new object[] { 3, 'a', false };
            yield return new object[] { 3, 3.0f, false };
            yield return new object[] { EM.Date.Parse("2019-01-01"), EM.Date.Parse("2019-01-01"), true }; 
            yield return new object[] { EM.Date.Parse("2019-01-01"), EM.Date.Parse("2019-01-02"), false };
            yield return new object[] { EM.Date.Parse("2019-01-01"), EM.Date.Parse("2019-01"), false };
            yield return new object[] { EM.Time.Parse("13:00:00"), EM.Time.Parse("13:00:00"), true }; 
            yield return new object[] { EM.Time.Parse("13:00:00"), EM.Time.Parse("13:00:01"), false };
            yield return new object[] { EM.Time.Parse("13:00:00"), EM.Time.Parse("13:00"), false };
            yield return new object[] { EM.DateTime.Parse("1972-11-30T15:15:00"), EM.DateTime.Parse("1972-11-30T15:15:00"), true }; 
            yield return new object[] { EM.DateTime.Parse("1972-11-30T15:15:00Z"), EM.DateTime.Parse("1972-11-30T15:15:00+00:00"), true };
            yield return new object[] { EM.DateTime.Parse("1972-11-30T15:15:00"), EM.DateTime.Parse("1972-11-29T15:15:00"), false };
            yield return new object[] { EM.DateTime.Parse("1972-11-30T15:15:00"), EM.DateTime.Parse("1972-11-30T15:15"), false };
            yield return new object[] { EM.DateTime.Parse("1972-11-30T15:15:00+01:00"), EM.DateTime.Parse("1972-11-30T15:15+00:00"), false };
            
            yield return new object[] { EM.Time.Parse("13:00:00"), EM.Date.Parse("2019-01-01"), false };
            yield return new object[] { EM.Time.Parse("13:00:00"), null, false };
        }


        private ITypedElement toTypedElement(IReadOnlyDictionary<string, object> dict)
        {
            var root = ElementNode.Root(_provider, "root");

            foreach (var kv in dict)
            {
                var element = root.Add(_provider, kv.Key, kv.Value);
            }

            return root;
        }

        private class NoTypeProvider : IStructureDefinitionSummaryProvider
        {
            public IStructureDefinitionSummary Provide(string canonical) => null;
        }

    }
}