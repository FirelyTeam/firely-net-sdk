/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;
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
        struct ElementDefnMerger
        {
            /// <summary>Merge two <see cref="ElementDefinition"/> instances. Existing diff properties override associated snap properties.</summary>
            public static void Merge(SnapshotGenerator generator, ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
            {
                var merger = new ElementDefnMerger(generator);
                merger.merge(snap, diff, mergeElementId);
            }

            SnapshotGenerator _generator;

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
                if (snap.Path != diff.Path && snap.IsChoice() && diff.Type.Count() == 1)
                {
                    // [WMR 20160906] WRONG! Must also handle snap.Path="Extension.value[x]" vs. diff.Path="Extension.extension.value[x]
                    // if (snap.Path.Substring(0, snap.Path.Length - 3) + diff.Type.First().Code.ToString().Capitalize() != diff.Path)
                    if (!ElementDefinitionNavigator.IsCandidateBasePath(snap.Path, diff.Path))
                    {
                        throw Error.InvalidOperation($"Invalid operation in snapshot generator. Path cannot be changed from '{snap.Path}' to '{diff.Path}', since the type is sliced to '{diff.Type.First().Code}'");
                    }
                    snap.PathElement = mergePrimitiveAttribute(snap.PathElement, diff.PathElement);
                }

                // [WMR 20170421] Element.Id is NOT inherited!
                // Merge custom Element id value from differential in same profile into snapshot
                // [WMR 20170424] NEW
                snap.ElementId = mergeId(snap, diff, mergeElementId);

                // representation cannot be overridden
                snap.NameElement = mergePrimitiveAttribute(snap.NameElement, diff.NameElement);

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
                snap.CommentsElement = mergePrimitiveAttribute(snap.CommentsElement, diff.CommentsElement, allowAppend: true);
                snap.RequirementsElement = mergePrimitiveAttribute(snap.RequirementsElement, diff.RequirementsElement, allowAppend: true);
                snap.LabelElement = mergePrimitiveAttribute(snap.LabelElement, diff.LabelElement);

                // Aliases are cumulative based on the string value
                snap.AliasElement = mergeCollection(snap.AliasElement, diff.AliasElement, (a, b) => a.Value == b.Value);

                // Mappings are cumulative, but keep unique on full contents
                snap.Mapping = mergeCollection(snap.Mapping, diff.Mapping, (a, b) => a.IsExactly(b));

                snap.MinElement = mergePrimitiveAttribute(snap.MinElement, diff.MinElement);
                snap.MaxElement = mergePrimitiveAttribute(snap.MaxElement, diff.MaxElement);

                // snap.base should already be there, and is not changed by the diff

                // Type collection has different semantics; any change replaces the inherited type (no item merging)
                // i.e. derived profiles can remove inherited types
                if (!diff.Type.IsNullOrEmpty() && !diff.Type.IsExactly(snap.Type))
                {
                    snap.Type = new List<ElementDefinition.TypeRefComponent>(diff.Type.DeepCopy());
                    foreach (var element in snap.Type) { onConstraint(element); }
                }

                // ElementDefinition.nameReference cannot be overridden by a derived profile
                // defaultValue and meaningWhenMissing can only be set in a resource/datatype/extension definition and cannot be overridden

                snap.Fixed = mergeComplexAttribute(snap.Fixed, diff.Fixed);
                snap.Pattern = mergeComplexAttribute(snap.Pattern, diff.Pattern);
                snap.Example = mergeComplexAttribute(snap.Example, diff.Example);
                snap.MinValue = mergeComplexAttribute(snap.MinValue, diff.MinValue);
                snap.MaxValue = mergeComplexAttribute(snap.MaxValue, diff.MaxValue);
                
                // [WMR 20160909] merge defaultValue and meaningWhenMissing, to handle core definitions; validator can detect invalid constraints
                snap.DefaultValue = mergeComplexAttribute(snap.DefaultValue, diff.DefaultValue);
                snap.MeaningWhenMissingElement = mergePrimitiveAttribute(snap.MeaningWhenMissingElement, diff.MeaningWhenMissingElement);

                snap.MaxLengthElement = mergePrimitiveAttribute(snap.MaxLengthElement, diff.MaxLengthElement);

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

                snap.Binding = mergeComplexAttribute(snap.Binding, diff.Binding);

                snap.Slicing = mergeComplexAttribute(snap.Slicing, diff.Slicing);

                // [WMR 20160817] TODO: Merge extensions
                // Debug.WriteLineIf(diff.Extension != null && diff.GetChangedByDiff() == null, "[ElementDefnMerger] Warning: Extension merging is not supported yet...");

                // TODO: What happens to extensions present on an ElementDefinition that is overriding another?
                // [WMR 20160907] Merge extensions... match on url, diff completely overrides snapshot
                snap.Extension = mergeCollection(snap.Extension, diff.Extension, (s, d) => s.Url == d.Url);
            }

            /// <summary>Notify clients about a snapshot element with differential constraints.</summary>
            void onConstraint(Element snap)
            {
                _generator.OnConstraint(snap);
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
            T mergePrimitiveAttribute<T>(T snap, T diff, bool allowAppend = false) where T : Primitive
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

                    onConstraint(result);
                    return result;
                }
                else
                    return snap;
            }

            T mergeComplexAttribute<T>(T snap, T diff) where T : Element
            {
#if true
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
#else
                if (!diff.IsNullOrEmpty() && (snap.IsNullOrEmpty() || !diff.IsExactly(snap)))
                {
                    // [WMR 20170224] WRONG! Must recursively merge missing child properties from base
                    var result = (T)diff.DeepCopy();

                    onConstraint(result);
                    return result;
                }
                else
                    return snap;
#endif
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
                    else if (diff.Name != snap.Name)
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
