/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.FhirPath.Analysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.R4.Tests
{
    /// <summary>
    /// Guards the <see cref="FhirPathAnalyzer"/> against false positives on real-world expressions: every invariant and
    /// search parameter expression in the R4 core specification must analyze without diagnostics (with the ordered
    /// function check on), apart from the known defects in the specification listed below.
    /// </summary>
    [TestClass]
    public class FhirPathAnalyzerCorpusTests
    {
        private static readonly FhirPathAnalyzer _analyzer = new(ModelInfo.ModelInspector);

        /// <summary>
        /// Expressions in the R4 specification that are genuinely invalid (the Java reference engine reports them too),
        /// by "StructureDefinition url|constraint key".
        /// </summary>
        private static readonly HashSet<string> KNOWN_SPEC_DEFECTS =
        [
            // 'name' does not exist on ChargeItemDefinition
            "http://hl7.org/fhir/StructureDefinition/ChargeItemDefinition|cid-0",
            // refers to an extension's 'value' children that do not exist on the types allowed
            "http://hl7.org/fhir/StructureDefinition/allergyintolerance-substanceExposureRisk|inv-1",
        ];

        [TestMethod, TestCategory("LongRunner")]
        public void CoreInvariantsHaveNoSemanticErrors()
        {
            var source = ZipSource.CreateValidationSource();
            var failures = new List<string>();
            var reportedDefects = new HashSet<string>();
            var count = 0;

            foreach (var uri in source.ListResourceUris(ResourceType.StructureDefinition))
            {
                if (source.ResolveByUri(uri) is not StructureDefinition { Snapshot: not null } sd) continue;
                if (sd.Kind == StructureDefinition.StructureDefinitionKind.Logical) continue;

                var resourceTypes = sd.Kind == StructureDefinition.StructureDefinitionKind.Resource ? new[] { sd.Type } : null;

                foreach (var element in sd.Snapshot.Element)
                {
                    foreach (var constraint in element.Constraint.Where(c => !string.IsNullOrEmpty(c.Expression)))
                    {
                        count += 1;
                        var key = $"{sd.Url}|{constraint.Key}";

                        try
                        {
                            var contextTypes = elementTypes(sd.Type, element.Path, failures);
                            var result = _analyzer.Analyze(constraint.Expression, new FhirPathAnalysisContext
                            {
                                InputTypes = contextTypes,
                                ResourceTypes = resourceTypes,
                                CheckOrderedFunctions = true,
                            });

                            if (!result.HasErrors) continue;
                            if (KNOWN_SPEC_DEFECTS.Contains(key))
                                reportedDefects.Add(key);
                            else
                                failures.Add($"{key} @ {element.Path}: {constraint.Expression}{Environment.NewLine}    {string.Join("; ", result.Diagnostics)}");
                        }
                        catch (Exception e)
                        {
                            failures.Add($"{key} @ {element.Path}: {constraint.Expression}{Environment.NewLine}    threw {e.GetType().Name}: {e.Message}");
                        }
                    }
                }
            }

            count.Should().BeGreaterThan(5000, "the R4 specification has thousands of invariants");
            failures.Should().BeEmpty();
            reportedDefects.Should().BeEquivalentTo(KNOWN_SPEC_DEFECTS, "a known defect that is no longer reported should be removed from the list");
        }

        [TestMethod, TestCategory("LongRunner")]
        public void CoreSearchParametersHaveNoSemanticErrors()
        {
            var source = ZipSource.CreateValidationSource();
            var failures = new List<string>();
            var count = 0;

            foreach (var uri in source.ListResourceUris(ResourceType.SearchParameter))
            {
                if (source.ResolveByUri(uri) is not SearchParameter { Expression: { Length: > 0 } expression } sp) continue;
                count += 1;

                var bases = sp.Base.Select(b => b.ToString()).ToArray();
                try
                {
                    var result = _analyzer.Analyze(expression, new FhirPathAnalysisContext
                    {
                        InputTypes = bases,
                        ResourceTypes = bases,
                        CheckOrderedFunctions = true,
                    });

                    if (result.HasErrors)
                        failures.Add($"{sp.Url}: {expression}{Environment.NewLine}    {string.Join("; ", result.Diagnostics)}");
                }
                catch (Exception e)
                {
                    failures.Add($"{sp.Url}: {expression}{Environment.NewLine}    threw {e.GetType().Name}: {e.Message}");
                }
            }

            count.Should().BeGreaterThan(1000, "the R4 specification has over a thousand search parameters");
            failures.Should().BeEmpty();
        }

        /// <summary>
        /// The possible types of the element at a path in a StructureDefinition, found by analyzing the path itself.
        /// </summary>
        private static IReadOnlyCollection<string> elementTypes(string rootType, string path, List<string> failures)
        {
            if (!path.Contains('.')) return [rootType];

            // The first step is the type name, which is not a valid path step for primitive types (e.g. 'string.value').
            var relativePath = path.Substring(path.IndexOf('.') + 1).Replace("[x]", "");
            var result = _analyzer.Analyze(relativePath, new FhirPathAnalysisContext { InputTypes = [rootType] });
            if (result.HasErrors)
                failures.Add($"element path {path} on {rootType}: {string.Join("; ", result.Diagnostics)}");
            return result.ResultTypes;
        }
    }
}
