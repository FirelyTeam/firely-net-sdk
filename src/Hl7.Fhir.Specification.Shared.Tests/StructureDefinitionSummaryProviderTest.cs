using FluentAssertions;
using FluentAssertions.Execution;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public partial class StructureDefinitionSummaryProviderTest
    {
        public static IEnumerable<object[]> GetPocoAndSdSummaryProviders()
        {
            IStructureDefinitionSummaryProvider pocoSdProvider = new PocoStructureDefinitionSummaryProvider();
            var resolver = ZipSource.CreateValidationSource();
            IStructureDefinitionSummaryProvider sdProvide = new StructureDefinitionSummaryProvider(resolver);

            foreach (var item in ModelInfo.SupportedResources)
            {
                var canonicalResource = ModelInfo.CanonicalUriForFhirCoreType(item);
                var pocoSummary = pocoSdProvider.Provide(canonicalResource);
                var sdSummary = sdProvide.Provide(canonicalResource);

                yield return new object[] { canonicalResource, pocoSummary, sdSummary };
            }
        }

        public static string GetCanonicalDisplayName(MethodInfo methodInfo, object[] data)
        {
            return data[0].ToString();
        }


        [TestMethod]
        [DynamicData(nameof(GetPocoAndSdSummaryProviders), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetCanonicalDisplayName))]
        public void PocoAndSdSummaryProvidersShouldBeEqual(Canonical canonicalResource, IStructureDefinitionSummary left, IStructureDefinitionSummary right) =>
            areEqual(left, right, new());

        private static void areEqual(IStructureDefinitionSummary left, IStructureDefinitionSummary right, Stack<string> workStack)
        {
            var context = string.Join(".", workStack.Reverse());
            //Safeguard against stack overflows since we are comparing recursively.
            workStack.Count.Should().BeLessOrEqualTo(20, "Too deeply nested equality check: " + string.Join(" - ", workStack));


            if (left is null)
                right.Should().BeNull(context + ": left is null, but right is not");

            if (right is null)
                left.Should().BeNull(context + ": right is null, but left is not.");

            if (left is object && right is object)
            {
                workStack.Push(left.TypeName);
                context = string.Join('.', workStack.Reverse());
                try
                {
                    left.IsAbstract.Should().Be(right.IsAbstract, context + ": Abstract differs");
                    left.IsResource.Should().Be(right.IsResource, context + ": IsResource differs");
                    left.TypeName.Should().Be(right.TypeName, context + ": TypeName differs");
                    areEqual(left.GetElements(), right.GetElements(), workStack);
                }
                finally
                {
                    workStack.Pop();
                }
            }
        }

        private static void areEqual(IEnumerable<IElementDefinitionSummary> left, IEnumerable<IElementDefinitionSummary> right, Stack<string> workStack)
        {
            var context = string.Join('.', workStack);
            if (left is null)
                right.Should().BeNull(context + ": left is null, but right is not");

            if (right is null)
                left.Should().BeNull(context + ": right is null, but left is not.");

            if (left is object && right is object)
            {
                left.Select(x => x.ElementName).Should().BeEquivalentTo(right.Select(x => x.ElementName), because: context + ": left and right have different number of elements.");

                // this implicitly tests the correctness of order, without order having to be exactly
                // the same for left and right.
                var orderedLeft = left.OrderBy(leds => leds.Order).ToList();
                var orderedRight = right.OrderBy(reds => reds.Order).ToList();

                for (var ix = 0; ix < orderedLeft.Count; ix++)
                {
                    var leftItem = orderedLeft[ix];
                    var matchingRight = orderedRight[ix];
                    matchingRight.Should().NotBeNull($"{context}: A match is not found for {leftItem.ElementName} in right list of summaries.");
                    areEqual(leftItem, matchingRight, workStack);
                }
            }
        }

        private static void areEqual(IElementDefinitionSummary left, IElementDefinitionSummary right, Stack<string> workStack)
        {
            if (workStack.Contains(left.ElementName)) return;   // work has already been done (or is underway)

            workStack.Push(left.ElementName);

            var context = string.Join('.', workStack.Reverse());
            if (left is null)
                right.Should().BeNull(context + ": left is null, but right is not");

            if (right is null)
                left.Should().BeNull(context + ": right is null, but left is not.");

            if (left is object && right is object)
            {
                left.ElementName.Should().Be(right.ElementName, context + ": ElementName differs");

                // * modifierExtensions are NOT presented as "in summary" in the
                //   StructureDefinitions, while they are in the Poco's (=> seem to make sense).
                // * The poco generator generates "in summary" for id, but this is not reflected in the
                //   structuredefinition either.
#if !R5
                if (!context.EndsWith(".modifierExtension"))
                {
                    // From 5.0.0-snapshot3 DomainResource.modifierExtension is a summary element. This means
                    // that there is now a difference with StructureDefinitionSummaryProvider, because for STU3/R4/R4B
                    // this correction has not been backported.
                    left.InSummary.Should().Be(right.InSummary, context + ": InSummary differs");
                }
#else
                left.InSummary.Should().Be(right.InSummary, context + ": InSummary differs");
#endif
                left.IsModifier.Should().Be(right.IsModifier, context + ": IsModifier differs");
                left.IsChoiceElement.Should().Be(right.IsChoiceElement, context + ": IsChoiceElement differs");
                left.IsCollection.Should().Be(right.IsCollection, context + ": IsCollection differs");
                left.IsRequired.Should().Be(right.IsRequired, context + ": IsRequired differs");
                left.IsResource.Should().Be(right.IsResource, context + ": IsResource differs");
                //     left.Order.Should().Be(right.Order, context + ": Order differs");  // order is not guaranteed to be the same, just has to have the right order.
                left.Representation.Should().Be(right.Representation, context + ": Representation differs");

                areEqual(left.Type, right.Type, workStack);
            }
            workStack.Pop();
        }

        private static void areEqual(ITypeSerializationInfo[] left, ITypeSerializationInfo[] right, Stack<string> workStack)
        {
            var context = string.Join('.', workStack.Reverse());

            if (left is null)
                right.Should().BeNull(context + ": left is null, but right is not");

            if (right is null)
                left.Should().BeNull(context + ": right is null, but left is not.");

            if (left is object && right is object)
            {
                // This is an exception: parameter.value can have all the types, but [AllowedTypes] is not generated, because not all the types
                // are located in Common.
                if (context == "Parameters.parameter.BackboneElement.value" || context == "Parameters.parameter.BackboneElement.part.BackboneElement.value")
                    return;

                left.Length.Should().Be(right.Length, context + ": nr. of elements differs.");
                foreach (var leftItem in left)
                {
                    if (leftItem is IStructureDefinitionSummary lsds)
                    {
                        var matchingRight = right.FirstOrDefault(r => r is IStructureDefinitionSummary rsds && rsds.TypeName == lsds.TypeName) as IStructureDefinitionSummary;
                        matchingRight.Should().NotBeNull(context + ": No matching IStructureDefinitionSummary found for " + lsds.TypeName);
                        areEqual(lsds, matchingRight!, workStack);
                    }
                    else if (leftItem is IStructureDefinitionReference lref)
                    {
                        var matchingRight = right.FirstOrDefault(r => r is IStructureDefinitionReference rref && rref.ReferredType == lref.ReferredType);
                        matchingRight.Should().NotBeNull(context + ": No matching IStructureDefinitionReference found for " + lref.ReferredType);
                    }
                    else
                    {
                        throw new AssertionFailedException(context + ": leftItem is of an unsupported type: " + leftItem.GetType());
                    }
                }
            }

        }
    }
}
