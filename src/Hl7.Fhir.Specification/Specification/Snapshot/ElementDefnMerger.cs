/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Reflection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Snapshot
{
    public partial class SnapshotGenerator
    {
        /// <summary>
        /// Private static helper for <see cref="SnapshotGenerator"/>.
        /// Merge two <see cref="ElementDefinition"/> instances and all their properties.
        /// </summary>
        internal struct ElementDefnMerger
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
                _generator = generator;
            }

            void merge(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
            {
                // [WMR 20160915] Important! Derived profiles should never inherit the ChangedByDiff extension
                // Caller should make sure that existing extensions have been removed from snap,
                // otherwise associated diff elems will be considered as changed (because they don't have the extension yet).

                // bool isExtensionConstraint = snap.Path == "Extension" || snap.IsExtension();

                // paths can be changed under one circumstance: the snap is a choice[x] element, and diff limits the type choices
                // to one. The name can then be changed to choiceXXXX, where XXXX is the name of the type.

                // [WMR 20171004] Determine *distinct* type codes
                // if (snap.Path != diff.Path && snap.IsChoice() && diff.Type.Count() == 1)
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
                        snap.PathElement = mergePrimitiveAttribute(snap.PathElement, diff.PathElement);
                    }
                }

                // [WMR 20170421] Element.Id is NOT inherited!
                // Merge custom Element id value from differential in same profile into snapshot
                // [WMR 20170424] NEW
                snap.ElementId = mergeId(snap, diff, mergeElementId);

                // [EK 20170301] This used to be ambiguous, now (STU3) split in contentReference and sliceName
                snap.SliceNameElement = mergePrimitiveAttribute(snap.SliceNameElement, diff.SliceNameElement);

                // Codes are cumulative based on the code value
                // [WMR 20180611] WRONG! Invalid elementComparer
                // snap.Code = mergeCollection(snap.Code, diff.Code, (a, b) => a.Code == b.Code);
                snap.Code = mergeCollection(snap.Code, diff.Code, isEqualCoding);

                // For extensions, the base definition is irrelevant since they describe infrastructure, and the diff should contain the real meaning for the elements
                // In case the diff doesn't have these, give some generic defaults.
                // [WMR 20160906] Wrong! Merge extension element properties from base extension element
                //if (isExtensionConstraint)
                //{
                //    snap.Short = "Extension"; OnConstraint(snap.ShortElement);
                //    snap.Definition = "An Extension"; OnConstraint(snap.DefinitionElement);
                //    snap.Comments = null;
                //    snap.Requirements = null;
                //    snap.AliasElement = new List<FhirString>();
                //    snap.Mapping = new List<ElementDefinition.MappingComponent>();
                //}

                snap.ShortElement = mergePrimitiveAttribute(snap.ShortElement, diff.ShortElement);
                snap.DefinitionElement = mergePrimitiveAttribute(snap.DefinitionElement, diff.DefinitionElement, allowAppend: true);
                snap.CommentElement = mergePrimitiveAttribute(snap.CommentElement, diff.CommentElement, allowAppend: true);
                snap.RequirementsElement = mergePrimitiveAttribute(snap.RequirementsElement, diff.RequirementsElement, allowAppend: true);
                snap.LabelElement = mergePrimitiveAttribute(snap.LabelElement, diff.LabelElement);

                // Aliases are cumulative based on the string value
                snap.AliasElement = mergeCollection(snap.AliasElement, diff.AliasElement, (a, b) => a.Value == b.Value);

                // Mappings are cumulative, but keep unique on full contents
                snap.Mapping = mergeCollection(snap.Mapping, diff.Mapping, (a, b) => a.IsExactly(b));

                snap.MinElement = mergePrimitiveAttribute(snap.MinElement, diff.MinElement);
                //snap.MaxElement = mergePrimitiveAttribute(snap.MaxElement, diff.MaxElement);
                snap.MaxElement = mergeMax(snap.MaxElement, diff.MaxElement);

                // snap.base should already be there, and is not changed by the diff

                // Type collection has different semantics; any change replaces the inherited type (no item merging)
                // i.e. derived profiles can remove inherited types
                if (!diff.Type.IsNullOrEmpty() && !diff.Type.IsExactly(snap.Type))
                {
                    snap.Type = new List<ElementDefinition.TypeRefComponent>(diff.Type.DeepCopy());
                    foreach (var element in snap.Type) { onConstraint(element); }
                }

                // ElementDefinition.contentReference cannot be overridden by a derived profile                

                snap.Fixed = mergeComplexAttribute(snap.Fixed, diff.Fixed);
                snap.Pattern = mergeComplexAttribute(snap.Pattern, diff.Pattern);

                // Examples are cumulative based on the full value
                snap.MaxLengthElement = mergePrimitiveAttribute(snap.MaxLengthElement, diff.MaxLengthElement);

                // [EK 20170301] In STU3, this was turned into a collection
                snap.Example = mergeCollection(snap.Example, diff.Example, (a, b) => a.IsExactly(b));

                snap.MinValue = mergeComplexAttribute(snap.MinValue, diff.MinValue);
                snap.MaxValue = mergeComplexAttribute(snap.MaxValue, diff.MaxValue);
                
                // [WMR 20160909] merge defaultValue and meaningWhenMissing, to handle core definitions; validator can detect invalid constraints
                snap.DefaultValue = mergeComplexAttribute(snap.DefaultValue, diff.DefaultValue);
                snap.MeaningWhenMissingElement = mergePrimitiveAttribute(snap.MeaningWhenMissingElement, diff.MeaningWhenMissingElement);
                snap.MaxLengthElement = mergePrimitiveAttribute(snap.MaxLengthElement, diff.MaxLengthElement);

                // [EK 20170301] Added this new STU3 element
                snap.OrderMeaningElement = mergePrimitiveAttribute(snap.OrderMeaningElement, diff.OrderMeaningElement);

                // TODO: [GG] what to do about conditions?  [EK] We have key, so merge Constraint and condition based on that?
                // Constraints are cumulative, so they are always "new" (hence a constant false for the comparer)
                // [WMR 20160917] Note: constraint keys must be unique. The validator will detect duplicate keys, so the derived
                // profile author can correct the conflicting constraint key.
                // [WMR 20160918] MUST merge indentical constraints, otherwise each derived profile accumulates
                // additional identical constraints inherited from e.g. BackboneElement.
                // snap.Constraint = mergeCollection(snap.Constraint, diff.Constraint, (a, b) => false);
                snap.Constraint = mergeCollection(snap.Constraint, diff.Constraint, (a, b) => a.IsExactly(b));

                // [WMR 20160907] merge conditions
                snap.ConditionElement = mergeCollection(snap.ConditionElement, diff.ConditionElement, (a, b) => a.Value == b.Value);

                snap.MustSupportElement = mergePrimitiveAttribute(snap.MustSupportElement, diff.MustSupportElement);

                // [WMR 20160907] Validator should catch this
                // ElementDefinition.isModifier can only be overridden by a derived extension
                // if (isExtensionConstraint)
                // {
                snap.IsModifierElement = mergePrimitiveAttribute(snap.IsModifierElement, diff.IsModifierElement);
                // }

                snap.IsSummaryElement = mergePrimitiveAttribute(snap.IsSummaryElement, diff.IsSummaryElement);

                snap.Binding = mergeBinding(snap.Binding, diff.Binding);

                // [AE 20200129] Merging only fails for lists on a nested level. Slicing.Discriminator is the only case where this happens
                var originalDiscriminator = snap.Slicing?.Discriminator;
                snap.Slicing = mergeComplexAttribute(snap.Slicing, diff.Slicing);
                correctListMerge(originalDiscriminator, diff.Slicing?.Discriminator, list => snap.Slicing.Discriminator = list);

                // [WMR 20160817] TODO: Merge extensions
                // Debug.WriteLineIf(diff.Extension != null && diff.GetChangedByDiff() == null, "[ElementDefnMerger] Warning: Extension merging is not supported yet...");

                // TODO: What happens to extensions present on an ElementDefinition that is overriding another?
                // [WMR 20160907] Merge extensions... match on url, diff completely overrides snapshot
                snap.Extension = mergeCollection(snap.Extension, diff.Extension, (s, d) => s.Url == d.Url);

                // [EK 20170301] Added this after comparison with Java generated snapshot
                snap.RepresentationElement = mergeCollection(snap.RepresentationElement, diff.RepresentationElement, (s, d) => s.IsExactly(d));
            }

            private void correctListMerge<T>(List<T> originalBase, List<T> replacement, Action<List<T>> setBase)
            {
                if (replacement is List<T> list && !list.Any())
                {
                    // list has been replaced inadvertently. Change it back
                    setBase(originalBase);
                }
            }

            /// <summary>Notify clients about a snapshot element with differential constraints.</summary>
            void onConstraint(Element snap)
            {
                _generator?.OnConstraint(snap);
            }

            /// <summary>
            /// Merges two FHIR primitives. Normally this means the diff overrides the snap, but if the diffd is a
            /// string, and it start with ellipsis ('...'), the diff is appended to the snap.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="snap"></param>
            /// <param name="diff"></param>
            /// <param name="allowAppend"></param>
            /// <returns></returns>
            T mergePrimitiveAttribute<T>(T snap, T diff, bool allowAppend = false) where T : PrimitiveType
            {
                // [WMR 20160718] Handle snap == null
                // if (!diff.IsNullOrEmpty() && !diff.IsExactly(snap))
                // if (!diff.IsNullOrEmpty() && (snap == null || !diff.IsExactly(snap)))
                if (!diff.IsNullOrEmpty() && (snap.IsNullOrEmpty() || !diff.IsExactly(snap)))
                {
                    var result = (T)diff.DeepCopy();

                    if (allowAppend && diff.ObjectValue is string)
                    {
                        var diffText = diff.ObjectValue as string;

                        if (diffText.StartsWith("..."))
                        {
                            // [WMR 20160719] Handle snap == null
                            // diffText = (snap.ObjectValue as string) + "\r\n" + diffText.Substring(3);
                            var prefix = snap != null ? snap.ObjectValue as string : null;
                            diffText = string.IsNullOrEmpty(prefix) ? 
                                diffText.Substring(3) 
                                : prefix + "\r\n" + diffText.Substring(3);
                        }

                        result.ObjectValue = diffText;
                    }

                    onConstraint(result);
                    return result;
                }
                else
                    return snap;
            }

            // The diamond problem is especially painful for min/max -
            // most datatypes roots have a cardinality of 0..*, so
            // a non-repeating element referring to a profiled datatype
            // (with 0..*) onto an element which needs to be expanded
            // and which has no diff constraints on max will get the 
            // max from the base -> a non-repeating element will become repeating.
            internal FhirString mergeMax(FhirString snap, FhirString diff)
            {
                if (!diff.IsNullOrEmpty() && !snap.IsNullOrEmpty() && !diff.IsExactly(snap))
                {
                    // If diff is unlimited, we can just take the original snap as the lowest limit
                    if (diff.Value == "*")
                        return snap;

                    // Now, diff has a numeric limit
                    // So, if snap has no limit, take the diff
                    if(snap.Value == "*")
                        return deepCopyAndRaiseOnConstraint(diff);

                    // snap and diff both have a numeric value
                    if (int.TryParse(snap.Value, out var sv) &&
                        int.TryParse(diff.Value, out var dv))
                    {
                        // compare them if they are both numerics
                        return dv < sv ? deepCopyAndRaiseOnConstraint(diff) : snap;
                    }
                    
                    // one of the two values cannot be parsed, just don't
                    // do anything to not break it any further.
                    return snap;
                }
                else if (!diff.IsNullOrEmpty() && (snap.IsNullOrEmpty() || !diff.IsExactly(snap)))
                {
                    return deepCopyAndRaiseOnConstraint(diff);
                }
                else
                    return snap;
            }

            private FhirString deepCopyAndRaiseOnConstraint(FhirString elt)
            {
                var result = (FhirString)elt.DeepCopy();
                onConstraint(result);
                return result;
            }

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

            List<T> mergeCollection<T>(List<T> snap, List<T> diff, Func<T, T, bool> elemComparer) where T : Element
            {
                if (!diff.IsNullOrEmpty() && !diff.IsExactly(snap))
                {
                    var result = snap == null ? new List<T>() : new List<T>(snap.DeepCopy());

                    // Just add new elements to the result, never replace existing ones
                    foreach (var element in diff)
                    {
                        if (!result.Any(e => elemComparer(e, element)))
                        {
                            var newElement = (T)element.DeepCopy();
                            onConstraint(newElement);
                            result.Add(newElement);
                        }
                    }

                    return result;
                }
                else
                    return snap;
            }


            //[MS 20201211] Separate function introduced to make sure that introduced extensions on Binding.Valueset in the diff are merged with the base.
            // This is a very specific fix and might be replaced by a more general merging method using ITypedElement in the future.
            private ElementDefinition.ElementDefinitionBindingComponent mergeBinding(ElementDefinition.ElementDefinitionBindingComponent snap, ElementDefinition.ElementDefinitionBindingComponent diff)
            {
                var result = snap;
                if (!diff.IsNullOrEmpty())
                {
                    if (snap.IsNullOrEmpty())
                    {
                        result = (ElementDefinition.ElementDefinitionBindingComponent)diff.DeepCopy();
                        onConstraint(result);
                    }
                    else if (!diff.IsExactly(snap))
                    {
                        snap.StrengthElement = mergePrimitiveAttribute(snap.StrengthElement, diff.StrengthElement);
                        snap.DescriptionElement = mergePrimitiveAttribute(snap.DescriptionElement, diff.DescriptionElement);
                        snap.ValueSet = mergeComplexAttribute(snap.ValueSet, diff.ValueSet);
                        onConstraint(result);
                    }
                }
                return result;
            }

            string mergeId(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
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
                    else if (diff.SliceName != snap.SliceName)
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

            // [WMR 20180611] NEW
            static bool isEqualCoding(Coding c, Coding d)
            {
                // Compare codes, if specified
                if (c.CodeElement != null || d.CodeElement != null)
                {
                    return c.System == d.System
                        && c.Version == d.Version
                        && c.Code == d.Code;
                }
                // Codes are empty or missing; compare display values instead
                return c.Display == d.Display;
            }

        }
    }

}
