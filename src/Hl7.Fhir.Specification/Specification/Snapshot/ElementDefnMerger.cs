/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    internal class ElementDefnMerger
    {
        private bool _markChanges;

        public ElementDefnMerger(bool markChanges)
        {
            _markChanges = markChanges;
        }

        public void Merge(ElementDefinition snap, ElementDefinition diff)
        {
            bool isExtensionConstraint = snap.Path == "Extension" || snap.IsExtension();

            // paths can be changed under one circumstance: the snap is a choice[x] element, and diff limits the type choices
            // to one. The name can then be changed to choiceXXXX, where XXXX is the name of the type.
            if(snap.Path != diff.Path && snap.IsChoice() && diff.Type.Count() == 1)
            {
                if (snap.Path.Substring(0, snap.Path.Length - 3) + diff.Type.First().Code.ToString().Capitalize() != diff.Path)
                    throw Error.InvalidOperation("Path cannot be changed from {0} to {1}, since the type is sliced to {2}"
                            .FormatWith(snap.Path, diff.Path, diff.Type.First().Code));

                snap.PathElement = mergePrimitiveAttribute(snap.PathElement, diff.PathElement);
            }

            // representation cannot be overridden
            snap.NameElement = mergePrimitiveAttribute(snap.NameElement, diff.NameElement);
            
            // Codes are cumulative based on the code value
            snap.Code = mergeCollection(snap.Code, diff.Code, (a, b) => a.Code == b.Code);

            // For extensions, the base definition is irrelevant since they describe infrastructure, and the diff should contain the real meaning for the elements
            // In case the diff doesn't have these, give some generic defaults.
            if (isExtensionConstraint)
            {
                snap.Short = "Extension"; markChange(snap.ShortElement);
                snap.Definition = "An Extension"; markChange(snap.DefinitionElement);
                snap.Comments = null;
                snap.Requirements = null;
                snap.AliasElement = new List<FhirString>();
                snap.Mapping = new List<ElementDefinition.MappingComponent>();
            }

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

            // Type is just overridden
            if (!diff.Type.IsNullOrEmpty() && !diff.IsExactly(snap))
            {
                snap.Type = new List<ElementDefinition.TypeRefComponent>(diff.Type.DeepCopy());
                foreach (var element in snap.Type) markChange(snap);
            }

            // ElementDefinition.nameReference cannot be overridden by a derived profile
            // defaultValue and meaningWhenMissing can only be set in a resource/datatype/extension definition and cannot be overridden

            snap.Fixed = mergeComplexAttribute(snap.Fixed, diff.Fixed);
            snap.Pattern = mergeComplexAttribute(snap.Pattern, diff.Pattern);
            snap.Example = mergeComplexAttribute(snap.Example, diff.Example);
            snap.MinValue = mergeComplexAttribute(snap.MinValue, diff.MinValue);
            snap.MaxValue = mergeComplexAttribute(snap.MaxValue, diff.MaxValue);

            snap.MaxLengthElement = mergePrimitiveAttribute(snap.MaxLengthElement, diff.MaxLengthElement);

            // TODO: [GG] what to do about conditions?  [EK] We have key, so merge Constraint and condition based on that?
            // Constraints are cumulative, so they are always "new" (hence a constant false for the comparer)
            snap.Constraint = mergeCollection(snap.Constraint, diff.Constraint, (a, b) => false);

            snap.MustSupportElement = mergePrimitiveAttribute(snap.MustSupportElement, diff.MustSupportElement);

            // ElementDefinition.isModifier can only be overridden by a derived extension
            if (isExtensionConstraint)
            {
                snap.IsModifierElement = mergePrimitiveAttribute(snap.IsModifierElement, diff.IsModifierElement);
            }

            snap.IsSummaryElement = mergePrimitiveAttribute(snap.IsSummaryElement, diff.IsSummaryElement);

            snap.Binding = mergeComplexAttribute(snap.Binding, diff.Binding);

            snap.Slicing = mergeComplexAttribute(snap.Slicing, diff.Slicing);

            // TODO: What happens to extensions present on an ElementDefinition that is overriding another?
        }

        private void markChange(Element snap)
        {
            if(_markChanges)
                snap.SetExtension(SnapshotGenerator.CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
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
        private T mergePrimitiveAttribute<T>(T snap, T diff, bool allowAppend = false) where T : Primitive
        {
            if (!diff.IsNullOrEmpty() && !diff.IsExactly(snap))
            {
                var result = (T)diff.DeepCopy();

                if (allowAppend && diff.ObjectValue is string)
                {
                    var diffText = diff.ObjectValue as string;

                    if (diffText.StartsWith("..."))
                        diffText = (snap.ObjectValue as string) + "\r\n" + diffText.Substring(3);

                    result.ObjectValue = diffText;
                }

                markChange(result);
                return result;
            }
            else
                return snap;
        }

        private T mergeComplexAttribute<T>(T snap, T diff) where T : Element
        {
            //TODO: The next != null should be IsNullOrEmpty(), but we don't have that yet for complex types

            if (diff != null && !diff.IsExactly(snap))
            {
                var result = (T)diff.DeepCopy();
                markChange(result);
                return result;
            }
            else
                return snap;
        }

        private List<T> mergeCollection<T>(List<T> snap, List<T> diff, Func<T, T, bool> elemComparer) where T : Element
        {
            //TODO: The next != null should be IsNullOrEmpty(), but we don't have that yet for complex types

            if (diff != null && !diff.IsExactly(snap))
            {
                var result = snap == null ? new List<T>() : new List<T>((IEnumerable<T>)snap.DeepCopy());

                // Just add new elements to the result, never replace existing ones
                foreach (var element in diff)
                {
                    if (!result.Any(e => elemComparer(e, element)))
                    {
                        var newElement = (T)element.DeepCopy();
                        markChange(newElement);
                        result.Add(newElement);
                    }
                }

                return result;
            }
            else
                return snap;
        }

    }
}
