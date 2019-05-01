﻿/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Specification.Snapshot
{
    public partial class SnapshotGenerator
    {
        /// <summary>
        /// Private static helper for <see cref="SnapshotGenerator"/>.
        /// Merge two <see cref="ElementDefinition"/> instances and all their properties.
        /// </summary>
        struct ElementDefnMerger
        {
            /// <summary>Merge two <see cref="ElementDefinition"/> instances. Existing diff properties override associated snap properties.</summary>
            public static void Merge(SnapshotGenerator generator, ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
            {
                var merger = new ElementDefnMerger(generator);
                merger.merge(snap, diff, mergeElementId);
            }

            readonly SnapshotGenerator _generator;

            ElementDefnMerger(SnapshotGenerator generator)
            {
                _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            }

            void merge(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
            {
                // [WMR 20170421] Element.Id is NOT inherited!
                // Merge custom Element id value from differential in same profile into snapshot
                // [WMR 20170424] NEW
                snap.ElementId = mergeId(snap, diff, mergeElementId);

                // [WMR 20160907] Merge extensions, match on url
                snap.Extension = mergeExtensions(snap.Extension, diff.Extension);

                // [WMR 20181211] R4: Also merge ElementDefinition.ModifierExtension
                // Q: What does this mean? How should consumers handle these?
                snap.ModifierExtension = mergeExtensions(snap.ModifierExtension, diff.ModifierExtension);

                // paths can be changed under one circumstance: the snap is a choice[x] element, and diff limits the type choices
                // to one. The name can then be changed to choiceXXXX, where XXXX is the name of the type.

                // [WMR 20171004] Determine *distinct* type codes
                if (snap.Path != diff.Path && snap.IsChoice())
                {
                    var distinctTypeCodes = diff.DistinctTypeCodes();
                    if (distinctTypeCodes.Count == 1)
                    {
                        // [WMR 20160906] WRONG! Must also handle snap.Path="Extension.value[x]" vs. diff.Path="Extension.extension.value[x]
                        // if (snap.Path.Substring(0, snap.Path.Length - 3) + diff.Type.First().Code.ToString().Capitalize() != diff.Path)
                        if (!ElementDefinitionNavigator.IsCandidateBasePath(snap.Path, diff.Path))
                        {
                            throw Error.InvalidOperation($"Invalid operation in snapshot generator. Path cannot be changed from '{snap.Path}' to '{diff.Path}', since the type is sliced to '{diff.Type.First().Code}'");
                        }
                        snap.PathElement = mergePrimitiveElement(snap.PathElement, diff.PathElement);
                    }
                }

                // [EK 20170301] Added this after comparison with Java generated snapshot
                snap.RepresentationElement = mergeCollection(snap.RepresentationElement, diff.RepresentationElement, matchExactly);

                // [EK 20170301] This used to be ambiguous, now (STU3) split in contentReference and sliceName
                snap.SliceNameElement = mergePrimitiveElement(snap.SliceNameElement, diff.SliceNameElement);
                // [WMR 20181211] R4: Also merge ElementDefinition.SliceIsConstraining
                snap.SliceIsConstrainingElement = mergePrimitiveElement(snap.SliceIsConstrainingElement, diff.SliceIsConstrainingElement);

                snap.LabelElement = mergePrimitiveElement(snap.LabelElement, diff.LabelElement);

                // Codes are cumulative based on the code value
                // [WMR 20180611] Use custom matching
                snap.Code = mergeCollection(snap.Code, diff.Code, matchCoding);

                snap.Slicing = mergeComplexAttribute(snap.Slicing, diff.Slicing);

                snap.ShortElement = mergePrimitiveElement(snap.ShortElement, diff.ShortElement);
                snap.Definition = mergePrimitiveElement(snap.Definition, diff.Definition, true);
                snap.Comment = mergePrimitiveElement(snap.Comment, diff.Comment, true);
                snap.Requirements = mergePrimitiveElement(snap.Requirements, diff.Requirements, true);

                // Aliases are cumulative based on the string value
                snap.AliasElement = mergePrimitiveCollection(snap.AliasElement, diff.AliasElement, matchStringValues);

                snap.MinElement = mergePrimitiveElement(snap.MinElement, diff.MinElement);
                snap.MaxElement = mergePrimitiveElement(snap.MaxElement, diff.MaxElement);

                // snap.Base should already be there, and is not changed by the diff
                // ElementDefinition.contentReference cannot be overridden by a derived profile                

                // Type collection has different semantics (no implicit type inheritance)
                // Include all diff types, merged onto matching snap
                // Exclude all snap types w/o matching diff type
                snap.Type = mergeElementTypes(snap.Type, diff.Type);

                snap.DefaultValue = mergeComplexAttribute(snap.DefaultValue, diff.DefaultValue);
                snap.MeaningWhenMissing = mergePrimitiveElement(snap.MeaningWhenMissing, diff.MeaningWhenMissing);
                // [EK 20170301] Added this new STU3 element
                snap.OrderMeaningElement = mergePrimitiveElement(snap.OrderMeaningElement, diff.OrderMeaningElement);

                snap.Fixed = mergeComplexAttribute(snap.Fixed, diff.Fixed);
                snap.Pattern = mergeComplexAttribute(snap.Pattern, diff.Pattern);
                
                // Examples are cumulative based on the full value
                // [EK 20170301] In STU3, this was turned into a collection
                snap.Example = mergeCollection(snap.Example, diff.Example, matchExactly);

                snap.MinValue = mergeComplexAttribute(snap.MinValue, diff.MinValue);
                snap.MaxValue = mergeComplexAttribute(snap.MaxValue, diff.MaxValue);
                
                // [WMR 20160909] merge defaultValue and meaningWhenMissing, to handle core definitions; validator can detect invalid constraints
                snap.MaxLengthElement = mergePrimitiveElement(snap.MaxLengthElement, diff.MaxLengthElement);

                // [WMR 20160907] merge conditions
                snap.ConditionElement = mergePrimitiveCollection(snap.ConditionElement, diff.ConditionElement, matchStringValues);

                // TODO: [GG] what to do about conditions?  [EK] We have key, so merge Constraint and condition based on that?
                // Constraints are cumulative, so they are always "new" (hence a constant false for the comparer)
                // [WMR 20160917] Note: constraint keys must be unique. The validator will detect duplicate keys, so the derived
                // profile author can correct the conflicting constraint key.
                // [WMR 20160918] MUST merge indentical constraints, otherwise each derived profile accumulates
                // additional identical constraints inherited from e.g. BackboneElement.
                // snap.Constraint = mergeCollection(snap.Constraint, diff.Constraint, (a, b) => false);
                snap.Constraint = mergeCollection(snap.Constraint, diff.Constraint, matchExactly);

                snap.MustSupportElement = mergePrimitiveElement(snap.MustSupportElement, diff.MustSupportElement);

                // [WMR 20190131] TODO #295
                snap.IsModifierElement = mergePrimitiveElement(snap.IsModifierElement, diff.IsModifierElement);
                // [WMR 20181211] R4: Also merge ElementDefinition.IsModifierReason
                snap.IsModifierReasonElement = mergePrimitiveElement(snap.IsModifierReasonElement, diff.IsModifierReasonElement);

                snap.IsSummaryElement = mergePrimitiveElement(snap.IsSummaryElement, diff.IsSummaryElement);

                snap.Binding = mergeComplexAttribute(snap.Binding, diff.Binding);

                // Mappings are cumulative, but keep unique on full contents
                snap.Mapping = mergeCollection(snap.Mapping, diff.Mapping, matchExactly);
            }

            /// <summary>Notify clients about a snapshot element with differential constraints.</summary>
            void onConstraint(Element snap)
            {
                _generator.OnConstraint(snap);
            }

            /// <summary>Notify clients about a snapshot collection element with differential constraints.</summary>
            void onConstraint<T>(List<T> snap) where T : Element
            {
                foreach (var item in snap)
                {
                    onConstraint(item);
                }
            }

            // [WMR 20190121] FIX - Issue #827
            // Custom logic for merging element type constraints
            List<ElementDefinition.TypeRefComponent> mergeElementTypes(
                List<ElementDefinition.TypeRefComponent> snap,
                List<ElementDefinition.TypeRefComponent> diff)
            {
                // Type collection has different semantics; any change replaces the inherited type (no item merging)
                // i.e. derived profiles can remove inherited types
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (List<ElementDefinition.TypeRefComponent>)diff.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {

                        // [WMR 20190121] FIX - Issue #827
                        // diff can constraint the list of allowed types
                        // i.e. include only types defined by diff; don't inherit other types from snap
                        // However, we should still merge diff type components with associated type component in base profile
                        // Esp. important for merging special "compiler magic" extensions for json/xml/rdf
                        // on primitive values [primitive].value.type[0].code.extension

                        // Include only types defined by differential
                        result = new List<ElementDefinition.TypeRefComponent>(diff.Count);
                        foreach (var diffType in diff)
                        {
                            // R4: ElementDefinition.type.code.value may be empty for certain elements:
                            // - [Primitive].value
                            // - Extension.url
                            // => Match diff type without code to base type without code

                            // Find matching type in snap
                            var snapType = snap.FirstOrDefault(tr => matchTypeCodes(tr, diffType));

                            // Merge diff type onto snap type
                            var mergedType = mergeElementType(snapType, diffType);
                            onConstraint(mergedType);
                            result.Add(mergedType);
                        }
                    }
                }
                return result;
            }

            // [WMR 20190121] FIX - Issue #827
            // Custom logic for merging constraints for a single element type
            // Merge (don't replace) extensions
            // Especially important for merging special "compiler magic" extensions for json/xml/rdf
            // on primitive values [primitive].value.type[0].code.extension
            ElementDefinition.TypeRefComponent mergeElementType(ElementDefinition.TypeRefComponent snap, ElementDefinition.TypeRefComponent diff)
            {
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (ElementDefinition.TypeRefComponent)diff.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {
                        result = (ElementDefinition.TypeRefComponent)snap.DeepCopy();

                        // TODO: Move logic to MergeTo method on partial class TypeRefComponent

                        // TODO: Copy diff annotations...?
                        if (diff.ElementId != null) { result.ElementId = diff.ElementId; }
                        result.Extension = mergeExtensions(snap.Extension, diff.Extension);
                        result.CodeElement = mergePrimitiveElement(snap.CodeElement, diff.CodeElement);
                        result.ProfileElement = mergeCanonicals(snap.ProfileElement, diff.ProfileElement);
                        result.TargetProfileElement = mergeCanonicals(snap.TargetProfileElement, diff.TargetProfileElement);

                        // diff aggregation flags replace/overwrite flags in snap
                        //result.AggregationElement = mergePrimitiveCollection(snap.AggregationElement, diff.AggregationElement, matchAggregationModes);
                        if (!(diff.AggregationElement is null))
                        {
                            result.AggregationElement = new List<Code<ElementDefinition.AggregationMode>>(diff.AggregationElement.DeepCopy());
                        }
                        else if (!(snap.AggregationElement is null))
                        {
                            result.AggregationElement = (List<Code<ElementDefinition.AggregationMode>>)snap.AggregationElement.DeepCopy();
                        }

                        result.VersioningElement = mergePrimitiveElement(snap.VersioningElement, diff.VersioningElement);

                        onConstraint(result);
                    }
                }
                return result;
            }

            // Merge a collection of primitive values
            List<T> mergePrimitiveCollection<T>(List<T> snap, List<T> diff, Func<T, T, bool> matchItems) where T : Primitive
            {
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (List<T>)diff.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {
                        // Paranoia... List elements are never null
                        result = new List<T>(snap.DeepCopy());

                        foreach (var diffItem in diff)
                        {
                            var idx = result.FindIndex(e => matchItems(e, diffItem));
                            T mergedItem;
                            if (idx < 0)
                            {
                                // No match; add diff item
                                mergedItem = (T)diffItem.DeepCopy();
                                result.Add(mergedItem);
                            }
                            else
                            {
                                // Match; merge diff with snap
                                var snapItem = result[idx];
                                mergedItem = mergePrimitiveElement(snapItem, diffItem, true);
                                result[idx] = mergedItem;
                            }
                            onConstraint(mergedItem);
                        }
                    }
                }
                return result;
            }

            // Merge list of canonical urls (e.g. profiles) in diff with snap
            List<Canonical> mergeCanonicals(List<Canonical> snap, List<Canonical> diff)
                => mergeCollection(snap, diff, matchCanonicals);

            // Merge differential extensions with snapshot extensions
            // Match extensions on url
            List<Extension> mergeExtensions(List<Extension> snap, List<Extension> diff)
                => mergeCollection(snap, diff, matchExtensions);

            // Merge two collections
            // Differential collection items replace/overwrite matching snapshot collection items
            List<T> mergeCollection<T>(List<T> snap, List<T> diff, Func<T, T, bool> matchItems) where T : Element
            {
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (List<T>)diff.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {
                        result = new List<T>(snap.DeepCopy());
                        // Properly merge matching collection items
                        foreach (var diffItem in diff)
                        {
                            var idx = snap.FindIndex(e => matchItems(e, diffItem));
                            T mergedItem;
                            if (idx < 0)
                            {
                                // No match; add diff item
                                mergedItem = (T)diffItem.DeepCopy();
                                result.Add(mergedItem);
                            }
                            else
                            {
                                // Match; merge diff with snap
                                var snapItem = result[idx];
                                mergedItem = mergeComplexAttribute(snapItem, diffItem);
                                result[idx] = mergedItem;
                            }
                            onConstraint(mergedItem);
                        }
                    }
                }
                return result;
            }

            // TODO: Properly *merge* (extension) collections on nested elements
            T mergeComplexAttribute<T>(T snap, T diff) where T : Element
            {
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (T)diff.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {
                        if (snap.GetType().GetTypeInfo().IsAssignableFrom(diff.GetType().GetTypeInfo()))
                        {
                            // [WMR 20170227] Diff type is equal to or derived from snap type
                            // Clone base and recursively copy all non-null diff props over base props
                            // So effectively the result inherits all missing properties from base

                            // [WMR 20190122] Problem: diff constraints overwrite/replace snap constraints
                            // TODO: Properly *merge* (extension) collections on nested elements
                            result = (T)snap.DeepCopy();
                            diff.CopyTo(result);
                        }
                        else
                        {
                            // [WMR 20170227] Diff type is incompatible with snap type (?)
                            // diff fully replaces snap
                            result = (T)diff.DeepCopy();
                        }
                        onConstraint(result);
                    }
                }
                return result;
            }

            /// <summary>
            /// Merges two FHIR primitives.
            /// By default, a differential value replaces/overrides the snapshot value.
            /// If the differential value is a string and starts with ellipsis ('...'),
            /// then append differential text to snapshot text.
            /// Merge differential extensions with snapshot extensions.
            /// </summary>
            T mergePrimitiveElement<T>(T snap, T diff, bool allowAppend = false) where T : Primitive
            {
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (T)diff?.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {
                        result = (T)snap?.DeepCopy();

                        var diffValue = diff.ObjectValue;
                        if (allowAppend && diffValue is string diffText)
                        {
                            if (diffText.StartsWith("..."))
                            {
                                //var prefix = snap != null ? snap.ObjectValue as string : null;
                                var prefix = snap?.ObjectValue as string;
                                if (string.IsNullOrEmpty(prefix))
                                {
                                    diffText = diffText.Substring(3);
                                }
                                else
                                {
                                    diffText = prefix + "\r\n" + diffText.Substring(3);
                                }
                            }

                            result.ObjectValue = diffText;
                        }
                        else
                        {
                            result.ObjectValue = diffValue;
                        }
                        // Also merge extensions on primitives
                        result.Extension = mergeExtensions(snap.Extension, diff.Extension);
                        onConstraint(result);
                    }
                }
                return result;
            }

            static string mergeId(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
            {
                // Note: Element.ElementId is a simple string property (not Element)
                // Cannot call onConstraint to annotate

                if (mergeElementId)
                {
                    // Merge custom elementId from differential, if specified
                    if (diff.ElementId != null)
                    {
                        return diff.ElementId;
                    }
                    // Newly introduced named slices NEVER inherit element id
                    // Must always regenerate new unique identifier for named slices
                    else if (!IsEqualName(diff.SliceName, snap.SliceName))
                    {
                        // Regenerate; don't inherit from snap
                        return null;
                    }
                    // Otherwise inherit existing element id from snap
                    return snap.ElementId;
                }
                else
                {
                    // Don't merge elementId, e.g. for type profiles
                    return null;
                }
            }

            // Functions to match snap collection items to diff collection items
            // Matching key depends on collection type

            static bool matchCoding(Coding c, Coding d)
            {
                // Compare codes, if specified
                if (c.CodeElement != null || d.CodeElement != null)
                {
                    return IsEqualString(c.System, d.System)
                        && IsEqualString(c.Version, d.Version)
                        && IsEqualString(c.Code, d.Code);
                }
                // Codes are empty or missing; compare display values instead
                return IsEqualString(c.Display, d.Display);
            }

            //static bool matchExactly<T>(T x, T y) where T : class, IDeepComparable => !(x is null) && x.IsExactly(y);
            static bool matchExactly(IDeepComparable x, IDeepComparable y) => !(x is null) && x.IsExactly(y);

            //static bool matchExtensions<T>(T x, T y) where T : Extension => !(x is null) && !(y is null) && IsEqualString(x.Url, y.Url);
            static bool matchExtensions(Extension x, Extension y) => !(x is null) && !(y is null) && IsEqualUri(x.Url, y.Url);

            static bool matchTypeCodes(ElementDefinition.TypeRefComponent x, ElementDefinition.TypeRefComponent y)
                => !(x is null) && !(y is null) && IsEqualType(x.Code, y.Code);

            static bool matchCanonicals(Canonical x, Canonical y) => matchStringValues(x, y);

            static bool matchStringValues<T>(T x, T y) where T : Primitive<string>, IStringValue
                => !(x is null) && !(y is null) && IsEqualString(x.Value, y.Value);

            static bool IsEqualString(string x, string y) => StringComparer.Ordinal.Equals(x, y);
        }
    }

}
