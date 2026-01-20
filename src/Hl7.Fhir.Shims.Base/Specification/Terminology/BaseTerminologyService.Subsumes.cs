/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using T = System.Threading.Tasks;
using static Hl7.Fhir.Model.CodeSystem;

namespace Hl7.Fhir.Specification.Terminology;

public abstract partial class BaseTerminologyService
{
    async T.Task<Parameters> ICodeValidationTerminologyService.Subsumes(Parameters parameters, string? id, bool useGet)
    {
        try
        {
            var validParams = new SubsumesParameters(parameters.NoDuplicates());
            TerminologyValidationHelpers.ValidateSubsumesParameters(validParams.CodeA, validParams.CodeB, validParams.CodingA, validParams.CodingB, validParams.System, validParams.Version?.Value);
            return await Subsumes(validParams).ConfigureAwait(false);
        }
        catch (Exception e) when (e is not FhirOperationException)
        {
            throw new FhirOperationException(e.Message, HttpStatusCode.InternalServerError);
        }
    }

    protected async virtual T.Task<SubsumesResult> Subsumes(SubsumesParameters parameters)
    {
        var codeA = parameters.CodeA?.Value ?? parameters.CodingA?.Code;
        var codeB = parameters.CodeB?.Value ?? parameters.CodingB?.Code;
        var system = parameters.System?.Value ?? parameters.CodingA?.System ?? parameters.CodingB?.System;
        
        // should already be validated before the call
        if (codeA is null || codeB is null || system is null)
            throw FhirOperationException.InvalidOperationInvocation("Insufficient information to perform subsumption testing.");

        if (codeA == codeB)
            return SubsumesResult.ForOutcome(SubsumptionOutcome.Equivalent);

        var codeSystem = await ResolveCodeSystem(new($"{system}|{parameters.Version?.Value}")).ConfigureAwait(false)
                         ?? throw FhirOperationException.Unresolvable($"The CodeSystem with url '{system}' could not be resolved.");
        
        if (codeSystem.Content != CodeSystemContentMode.Complete)
            throw FhirOperationException.NotSupported("Subsumption testing requires a code system with complete content.");

        if (codeSystem.HierarchyMeaning != CodeSystemHierarchyMeaning.IsA)
            throw FhirOperationException.NotSupported("Subsumption testing is only supported for code systems with 'is-a' hierarchy meaning.");

        if (findConceptSubsumption(codeSystem, codeA, codeB) is {} result)
            return result;
        
        var msg = $"The codes '{codeA}','{codeB}' could not be found in CodeSystem with url '{system}'";
        if (parameters.Version is not null)
            msg += $" and version '{parameters.Version.Value}'";
        
        throw FhirOperationException.CodeNotInSystem(msg);
    }
    
    private static SubsumesResult? findConceptSubsumption(CodeSystem codeSystem, string codeA, string codeB)
    {
        // use PocoNode for Parent/Child traversal
        var tree = ConceptHierarchyTree.Build(codeSystem);

        var nodeA = tree.Get(codeA);
        var nodeB = tree.Get(codeB);

        // If both codes are not present, we notify about invalid request
        if (nodeA is null && nodeB is null)
            return null;

        if(nodeA is null || nodeB is null)
            return SubsumesResult.ForOutcome(SubsumptionOutcome.NotSubsumed);
        
        if (hasParentWithCode(nodeB, nodeA, parentCode: codeA, childCode: codeB))
            return SubsumesResult.ForOutcome(SubsumptionOutcome.Subsumes);
        
        if (hasParentWithCode(nodeA, nodeB, parentCode: codeB, childCode: codeA))
            return SubsumesResult.ForOutcome(SubsumptionOutcome.SubsumedBy);
        
        return SubsumesResult.ForOutcome(SubsumptionOutcome.NotSubsumed);
    }

    private static bool hasParentWithCode(ConceptHierarchyTree.ConceptEntry child, ConceptHierarchyTree.ConceptEntry parent, string parentCode, string childCode)
    {
        // Check if child has parent relationship (transitive)
        if(child.Parents.Contains(parentCode))
            return true;

        // For completeness, check if parent has child relationship
        // This covers cases where the relationship is defined from the parent side
        if(parent.Children.Contains(childCode))
            return true;

        return false;
    }

    protected class ConceptHierarchyTree
    {
        internal ConceptEntry? Get(string code) => Entries.GetValueOrDefault(code);

        private Dictionary<string, ConceptEntry> Entries = new();

        internal record ConceptEntry(PocoNode Node, IEnumerable<string> Parents, IEnumerable<string> Children);

        public static ConceptHierarchyTree Build(CodeSystem codeSystem)
        {
            var entries = new ConceptHierarchyTree();

            var parentProp = codeSystem.Property.FirstOrDefault(x => x is { Uri: "http://hl7.org/fhir/concept-properties#parent", Code: not null })?.Code;
            var childProp = codeSystem.Property.FirstOrDefault(x => x is { Uri: "http://hl7.org/fhir/concept-properties#child", Code: not null })?.Code;

            // use PocoNode to keep parent/child relationship
            var pn = codeSystem.ToPocoNode();
            foreach (var concept in pn.FlatChildren("concept").DescendantsAndSelf().Where(x => x.Name == "concept"))
            {
                if(concept.Poco is not CodeSystem.ConceptDefinitionComponent poco)
                    continue;
                
                if (concept.Child("code") is not PrimitiveNode { Primitive: Code {Value: {} c}})
                    continue;
                
                var parentCodes = poco.Property
                    .Where(x => x.Code == parentProp)
                    .Select(x => (x.Value as PrimitiveType)?.JsonValue)
                    .OfType<string>();
                var childCodes = poco.Property
                    .Where(x => x.Code == childProp)
                    .Select(x => (x.Value as PrimitiveType)?.JsonValue)
                    .OfType<string>();

                entries.add(c, concept, parentCodes, childCodes);
            }

            return entries.Flatten();
        }

        private ConceptHierarchyTree Flatten()
        {
            var flattened = new ConceptHierarchyTree();

            foreach (var entry in Entries)
            {
                flattened.add(entry.Key, entry.Value.Node, getAllParents(entry), getAllChildren(entry));
            }

            return flattened;
            
            IEnumerable<string> getAllParents(KeyValuePair<string, ConceptEntry> entry, HashSet<string>? visited = null)
            {
                visited ??= new();
                foreach(var parentCode in entry.Value.Parents)
                {
                    if (!visited.Add(parentCode))
                        continue; // Skip cycles
                    
                    yield return parentCode;
                    
                    if (!Entries.TryGetValue(parentCode, out var parentEntry))
                        continue;
                    
                    foreach (var ancestor in getAllParents(new(parentCode, parentEntry), visited))
                        yield return ancestor;
                }
            }
            
            IEnumerable<string> getAllChildren(KeyValuePair<string, ConceptEntry> entry, HashSet<string>? visited = null)
            {
                visited ??= new();
                foreach(var childCode in entry.Value.Children)
                {
                    if (!visited.Add(childCode))
                        continue; // Skip cycles
                    
                    yield return childCode;
                    if (!Entries.TryGetValue(childCode, out var childEntry))
                        continue;
                    
                    foreach (var descendant in getAllChildren(new(childCode, childEntry), visited))
                        yield return descendant;
                }
            }
        }

        private void add(string code, PocoNode concept, IEnumerable<string> parentCodes, IEnumerable<string> childCodes)
        {
            Entries.Add(code, new(concept, parentCodes, childCodes));
        }
    }
}