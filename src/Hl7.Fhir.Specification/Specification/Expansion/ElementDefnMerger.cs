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


namespace Hl7.Fhir.Specification.Expansion
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
            bool isExtensionConstraint = snap.Path == "Extension" || snap.Path.EndsWith(".extension") || snap.Path.EndsWith(".modifierExtension");

            // For extensions, the base definition is irrelevant since they describe infrastructure, and the diff should contain the real meaning for the elements
            // In case the diff doesn't have these, give some generic defaults.
            if (isExtensionConstraint)
            {
                snap.Definition = "An Extension"; markChange(snap.DefinitionElement);
                snap.Short = "Extension"; markChange(snap.ShortElement);
                snap.Comments = null; markChange(snap.CommentsElement);
                snap.Requirements = null;
                snap.AliasElement = new List<FhirString>();
                snap.Mapping = new List<ElementDefinition.ElementDefinitionMappingComponent>();
            }

            // representation cannot be overridden

            snap.NameElement = mergePrimitiveAttribute(snap.NameElement, diff.NameElement);
            snap.LabelElement = mergePrimitiveAttribute(snap.LabelElement, diff.LabelElement);

            // Codes are cumulative based on the code value
            snap.Code = mergeCollection(snap.Code, diff.Code, (a, b) => a.Code == b.Code);

            snap.ShortElement = mergePrimitiveAttribute(snap.ShortElement, diff.ShortElement);
            snap.DefinitionElement = mergePrimitiveAttribute(snap.DefinitionElement, diff.DefinitionElement, allowAppend: true);
            snap.CommentsElement = mergePrimitiveAttribute(snap.CommentsElement, diff.CommentsElement, allowAppend: true);
            snap.RequirementsElement = mergePrimitiveAttribute(snap.RequirementsElement, diff.RequirementsElement, allowAppend: true);

            // Aliases are cumulative based on the string value
            snap.AliasElement = mergeCollection(snap.AliasElement, diff.AliasElement, (a, b) => a.Value == b.Value);

            snap.MinElement = mergePrimitiveAttribute(snap.MinElement, diff.MinElement);
            snap.MaxElement = mergePrimitiveAttribute(snap.MaxElement, diff.MaxElement);

            // ElementDefinition.nameReference cannot be overridden by a derived profile

            // defaultValue and meaningWhenMissing can only be set in a resource/datatype/extension definition and cannot be overridden

            snap.Fixed = mergeComplexAttribute(snap.Fixed, diff.Fixed);
            snap.Pattern = mergeComplexAttribute(snap.Pattern, diff.Pattern);
            snap.Example = mergeComplexAttribute(snap.Example, diff.Example);

            snap.MaxLengthElement = mergePrimitiveAttribute(snap.MaxLengthElement, diff.MaxLengthElement);

            // TODO: [GG] what to do about conditions?  [EK] We have key, so merge Constraint and condition based on that?

            snap.MustSupportElement = mergePrimitiveAttribute(snap.MustSupportElement, diff.MustSupportElement);

            // ElementDefinition.isModifier can only be overridden by a derived extension
            if (isExtensionConstraint)
            {
                snap.IsModifierElement = mergePrimitiveAttribute(snap.IsModifierElement, diff.IsModifierElement);
            }

            snap.IsSummaryElement = mergePrimitiveAttribute(snap.IsSummaryElement, diff.IsSummaryElement);

            snap.Binding = mergeComplexAttribute(snap.Binding, diff.Binding);

            // Type is just overridden
            if (!diff.Type.IsNullOrEmpty())
            {
                snap.Type = new List<ElementDefinition.TypeRefComponent>(diff.Type.DeepCopy());
                foreach (var element in snap.Type) markChange(snap);
            }

            // Mappings are cumulative based on Mapping.identity
            snap.Mapping = mergeCollection(snap.Mapping, diff.Mapping, (a, b) => a.Identity == b.Identity);

            // Constraints are cumulative bassed on Constraint.id
            snap.Constraint = mergeCollection(snap.Constraint, diff.Constraint, (a, b) => a.Key == b.Key);

            snap.Slicing = mergeComplexAttribute(snap.Slicing, diff.Slicing);

            // TODO: What happens to extensions present on an ElementDefinition that is overriding another?
        }

        private void markChange(Element snap)
        {
            if(_markChanges)
                snap.SetExtension(SnapshotGenerator.CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
        }
        

        private T mergePrimitiveAttribute<T>(T snap, T diff, bool allowAppend = false) where T : Primitive
        {
            if (!diff.IsNullOrEmpty() && !diff.IsExactly(snap))
            {
                var result = (T)diff.DeepCopy();

                if (allowAppend && diff is FhirString)
                {
                    var diffText = (diff as FhirString).Value;

                    if (diffText.StartsWith("..."))
                        diffText = (snap as FhirString).Value + "\r\n" + diffText.Substring(3);

                    (result as FhirString).Value = diffText;
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

                // Add new elements to the result, but replace existing ones
                foreach (var element in diff)
                {
                    result.Remove(diff.FirstOrDefault(e => elemComparer(element, e)));
                    var newElement = (T)element.DeepCopy();
                    markChange(newElement);
                    result.Add(newElement);
                }

                return result;
            }
            else
                return snap;
        }

    }
}
