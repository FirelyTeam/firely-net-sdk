/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.FluentPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.Validation
{
    public class OnSnapshotNeededEventArgs : EventArgs
    {
        public OnSnapshotNeededEventArgs(StructureDefinition definition, IArtifactSource source)
        {
            Definition = definition;
            Source = source;
        }

        public StructureDefinition Definition { get; private set; }

        public IArtifactSource Source { get; private set; }
    }


    /// <summary>
    /// TODO: When an extension is used in an instance (not necessarily mandated by the profile I am validating against), I should still trace
    /// the Extension.url to validate it
    /// </summary>
    public class Validator
    {
        public ValidationContext ValidationContext { get; private set;  }
            
        public event EventHandler<OnSnapshotNeededEventArgs> OnSnapshotNeeded;

        private Stack<IElementNavigator> FluentPathContext = new Stack<IElementNavigator>();


        public Validator(ValidationContext context)
        {
            ValidationContext = context;
        }

        public OperationOutcome Validate(string definitionUri, IElementNavigator instance)
        {
            if (definitionUri == null) throw Error.ArgumentNull("definitionUri");

            var outcome = new OperationOutcome();

            StructureDefinition structureDefinition = ValidationContext.ArtifactSource.LoadConformanceResourceByUrl(definitionUri) as StructureDefinition;

            if (outcome.Verify(() => structureDefinition != null, "Unable to resolve reference to profile '{0}'".FormatWith(definitionUri), Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, instance))
                outcome.Add(Validate(structureDefinition, instance));

            return outcome;
        }

        public OperationOutcome Validate(IEnumerable<string> definitionUris, IElementNavigator instance, BatchValidationMode mode)
        {
            List<OperationOutcome> outcomes = new List<OperationOutcome>();

            foreach (var uri in definitionUris)
                outcomes.Add(Validate(uri, instance));

            var outcome = new OperationOutcome();
            outcome.Combine(outcomes, instance, mode);

            return outcome;
        }



        public OperationOutcome Validate(IEnumerable<StructureDefinition> structureDefinitions, IElementNavigator instance, BatchValidationMode mode)
        {
            List<OperationOutcome> outcomes = new List<OperationOutcome>();

            foreach (var sd in structureDefinitions)
                outcomes.Add(Validate(sd, instance));

            var outcome = new OperationOutcome();
            outcome.Combine(outcomes, instance, mode);

            return outcome;
        }


        public OperationOutcome Validate(StructureDefinition structureDefinition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (!structureDefinition.HasSnapshot && ValidationContext.ArtifactSource != null)
            {
                // Note: this modifies an SD that is passed to us. If this comes from a cache that's
                // kept across processes, this could mean trouble, so clone it first
                structureDefinition = (StructureDefinition)structureDefinition.DeepCopy();

                // We'll call out to an external component, so catch any exceptions and include them in our report
                try
                {
                    SnapshotNeeded(structureDefinition, ValidationContext.ArtifactSource);
                }
                catch (Exception e)
                {
                    Trace(outcome, "Snapshot generation failed for {0}. Message: {1}"
                           .FormatWith(structureDefinition.Url, e.Message), Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED, instance);
                }
            }

            if (outcome.Verify(() => structureDefinition.HasSnapshot,
                "Profile '{0}' does not include a snapshot. Instance data has not been validated.".FormatWith(structureDefinition.Url), Issue.UNAVAILABLE_NEED_SNAPSHOT, instance))
            {
                var nav = new ElementDefinitionNavigator(structureDefinition);

                // If the definition has no data, there's nothing to validate against, so we'll consider it "valid"
                if (outcome.Verify(() => !nav.AtRoot || nav.MoveToFirstChild(), "Profile '{0}' has no content in snapshot"
                        .FormatWith(structureDefinition.Url), Issue.PROFILE_ELEMENTDEF_IS_EMPTY, instance))
                {
                    outcome.Add(ValidateElement(nav, instance));
                }

            }

            return outcome;
        }

        internal OperationOutcome ValidateElement(IEnumerable<ElementDefinitionNavigator> definitions, IElementNavigator instance, BatchValidationMode mode)
        {
            List<OperationOutcome> outcomes = new List<OperationOutcome>();

            foreach (var def in definitions)
                outcomes.Add(ValidateElement(def, instance));

            var outcome = new OperationOutcome();
            outcome.Combine(outcomes, instance, mode);

            return outcome;
        }


        internal OperationOutcome ValidateElement(ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            bool isNewFPContext = definition.StructureDefinition.Kind == StructureDefinition.StructureDefinitionKind.Resource &&
                                        definition.Current.IsRootElement();

            try
            {
                Trace(outcome, "Start validation of ElementDefinition at path '{0}'".FormatWith(definition.QualifiedDefinitionPath()), Issue.PROCESSING_PROGRESS, instance);

                if (isNewFPContext)
                {
                    Trace(outcome, "Enter validation context (type {0})".FormatWith(instance.TypeName), Issue.PROCESSING_PROGRESS, instance);
                    FluentPathContext.Push(instance);
                }

                // Any node must either have a value, or children, or both (e.g. extensions on primitives)
                if (!outcome.Verify(() => instance.Value != null || instance.HasChildren(), "Element must not be empty", Issue.CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN, instance))
                    return outcome;

                var elementConstraints = definition.Current;

                if (elementConstraints.IsPrimitiveValueConstraint())
                {
                    // The "value" property of a FHIR Primitive is the bottom of our recursion chain, it does not have a nameReference
                    // nor a <type>, the only thing left to do to validate the content is to validate the string representation of the
                    // primitive against the regex given in the core definition
                    outcome.Add(VerifyPrimitiveContents(elementConstraints, instance));
                }
                else if (definition.HasChildren)
                {
                    // Handle in-lined constraints on children. In a snapshot, these children should be exhaustive,
                    // so there's no point in also validating the <type> or <nameReference>
                    outcome.Add(this.ValidateChildConstraints(definition, instance));
                }
                else
                {
                    // No inline-children, so validation depends on the presence of a <type> or <nameReference>
                    if (outcome.Verify(() => elementConstraints.Type != null || elementConstraints.NameReference != null,
                            "ElementDefinition has no child, nor does it specify a type or nameReference to validate the instance data against", Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF, instance))
                    {
                        outcome.Add(ValidateType(elementConstraints, instance));
                        outcome.Add(ValidateNameReference(elementConstraints, definition, instance));
                    }
                }

                outcome.Add(ValidateSlices(definition, instance));
                // Min/max (cardinality) has been validated by parent, we cannot know down here
                outcome.Add(this.ValidateFixed(elementConstraints, instance));
                outcome.Add(this.ValidatePattern(elementConstraints, instance));
                outcome.Add(ValidateMinValue(elementConstraints, instance));
                // outcome.Add(ValidateMaxValue(elementConstraints, instance));
                outcome.Add(ValidateMaxLength(elementConstraints, instance));

                // Validate Binding

                outcome.Add(ValidateConstraints(elementConstraints, instance));

                return outcome;
            }
            finally
            {
                if (isNewFPContext)
                {
                    var context = FluentPathContext.Pop();
                    Trace(outcome, "Leave validation context (type {0})".FormatWith(context.TypeName), Issue.PROCESSING_PROGRESS, instance);                    
                }
            }
        }


        internal OperationOutcome ValidateConstraints(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            var context = FluentPathContext.Any() ? FluentPathContext.Peek() : null;


            // <constraint>
            //  <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-expression">
            //    <valueString value="reference.startsWith('#').not() or (reference.substring(1).trace('url') in %resource.contained.id.trace('ids'))"/>
            //  </extension>
            //  <key value="ref-1"/>
            //  <severity value="error"/>
            //  <human value="SHALL have a local reference if the resource is provided inline"/>
            //  <xpath value="not(starts-with(f:reference/@value, &#39;#&#39;)) or exists(ancestor::*[self::f:entry or self::f:parameter]/f:resource/f:*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, &#39;#&#39;)]|/*/f:contained/f:*[f:id/@value=substring-after(current()/f:reference/@value, &#39;#&#39;)])"/>
            //</constraint>
            // 

            foreach (var constraintElement in definition.Constraint)
            {
                var fpExpression = constraintElement.GetFluentPathConstraint();
                if(outcome.Verify(() => fpExpression != null, "Encountered an invariant ({0}) that has no FluentPath expression, skipping validation of this constraint"
                            .FormatWith(constraintElement.Key), Issue.UNSUPPORTED_CONSTRAINT_WITHOUT_FLUENTPATH, instance))
                {
                    if (ValidationContext.SkipConstraintValidation)
                        outcome.Verify(() => true, "Instance was not validated against invariant {0}, because constraint validation is disabled", Issue.PROCESSING_CONSTRAINT_VALIDATION_INACTIVE, instance);
                    else
                    {

                        bool success = false;
                        try
                        {
                            success = instance.Predicate(fpExpression, context);

                            var text = "Instance failed constraint " + constraintElement.ConstraintDescription();

                            if (constraintElement.Severity == ElementDefinition.ConstraintSeverity.Error)
                                outcome.Verify(() => success, text, Issue.CONTENT_ELEMENT_FAILS_ERROR_CONSTRAINT, instance);
                            else
                                outcome.Verify(() => success, text, Issue.CONTENT_ELEMENT_FAILS_WARNING_CONSTRAINT, instance);

                        }
                        catch (Exception e)
                        {
                            outcome.Verify(() => true, "Evaluation of FluentPath for constraint '{0}' failed: {1}"
                                            .FormatWith(constraintElement.Key, e.Message), Issue.PROFILE_ELEMENTDEF_INVALID_FLUENTPATH_EXPRESSION, instance);
                        }
                    }
                }
            }
        
            return outcome;
        }

        internal OperationOutcome ValidateSlices(ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.Current.Slicing != null)
            {
                // This is the slicing entry
                // TODO: Find my siblings and try to validate the content against
                // them. There should be exactly one slice validating against the
                // content, otherwise the slicing is ambiguous. If there's no match
                // we fail validation as well. 
                // For now, we do not handle slices
                outcome.Verify(() => definition.Current.Slicing == null, "ElementDefinition uses slicing, which is not yet supported. Instance has not been validated against " +
                            "any of the slices", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, instance);
            }

            return outcome;
        }

        internal OperationOutcome ValidateNameReference(ElementDefinition definition, ElementDefinitionNavigator allDefinitions, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.NameReference != null)
            {
                Trace(outcome, "Start validation of constraints referred to by nameReference '{0}'".FormatWith(definition.NameReference), Issue.PROCESSING_PROGRESS, instance);

                var referencedPositionNav = allDefinitions.ShallowCopy();

                if (outcome.Verify(() => referencedPositionNav.JumpToNameReference(definition.NameReference),
                        "ElementDefinition uses a non-existing nameReference '{0}'".FormatWith(definition.NameReference),
                        Issue.PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE, instance))
                {
                    outcome.Include(ValidateElement(referencedPositionNav, instance));
                }
            }

            return outcome;
        }

        internal OperationOutcome ValidateType(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            Trace(outcome, "Validating against constraints specified by the element's defined type", Issue.PROCESSING_PROGRESS, instance);

            outcome.Verify(() => !definition.Type.Select(tr => tr.Code).Contains(null), "ElementDefinition contains a type with an empty type code", Issue.PROFILE_ELEMENTDEF_CONTAINS_NULL_TYPE, instance);

            // Check if this is a choice: there are multiple distinct Codes to choose from
            var types = definition.Type.Where(tr => tr.Code != null);
            var choices = types.Select(tr => tr.Code.Value).Distinct();
            var hasPolymorphicType = choices.Any(tr => ModelInfo.IsCoreSuperType(tr));       // typerefs like Resource, Element are actually a choice

            if (choices.Count() > 1 || hasPolymorphicType)
            {
                if (outcome.Verify(() => instance.TypeName != null, "ElementDefinition is a choice or contains a polymorphic type constraint, but the instance does not indicate its actual type",
                    Issue.CONTENT_ELEMENT_CHOICE_WITH_NO_ACTUAL_TYPE, instance))
                {
                    // This is a choice type, find out what type is present in the instance data
                    // (e.g. deceased[Boolean], or _resourceType in json). This is exposed by IElementNavigator.TypeName.
                    var instanceType = ModelInfo.FhirTypeNameToFhirType(instance.TypeName);
                    if (outcome.Verify(() => instanceType != null, "Instance indicates the element is of type '{0}', which is not a known FHIR core type."
                                .FormatWith(instance.TypeName), Issue.CONTENT_ELEMENT_CHOICE_WITH_NO_ACTUAL_TYPE, instance))
                    {
                        var applicableChoices = types.Where(tr => ModelInfo.IsInstanceTypeFor(tr.Code.Value, instanceType.Value));

                        // Instance typename must be one of the applicable types in the choice
                        if (outcome.Verify(() => applicableChoices.Any(), "Type specified in the instance ('{0}') is not one of the allowed choices ({1})"
                                    .FormatWith(instance.TypeName, String.Join(",", choices.Select(t => "'" + t.GetLiteral() + "'"))), Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, instance))
                        {
                            var actualChoices = applicableChoices;

                            if (hasPolymorphicType)
                            {
                                // Now, rewrite the typerefs to validate so it will always contain the actual instance type instead of the
                                // abstract super type, e.g. validate Patient, not Resource if the instance is a Patient.
                                actualChoices = applicableChoices
                                    .Select(tr => new ElementDefinition.TypeRefComponent { Code = instanceType, Profile = tr.Profile });

                            }

                            outcome.Include(ValidateTypeReferences(actualChoices, instance));
                        }
                    }
                }
            }
            else if (choices.Count() == 1)
            {
                // Only one type present in list of typerefs, all of the typerefs are candidates
                outcome.Include(ValidateTypeReferences(types, instance));
            }

            return outcome;
        }


        internal OperationOutcome ValidateTypeReferences(IEnumerable<ElementDefinition.TypeRefComponent> typeRefs, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            var normalUris = typeRefs.Where(tr => tr.Code !=  FHIRDefinedType.Reference).Select(tr => tr.ProfileUri());
            var referenceUris = typeRefs.Where(tr => tr.Code == FHIRDefinedType.Reference).Select(tr => tr.ProfileUri());

            if(referenceUris.Any() && ValidationContext.ValidateReferencedResources != ReferenceKind.None)
            {
                //TODO: 1) Create a new Validator, pass it our context
                // 2) Use an event or resolution mechanism to fetch the resource and determine the kind of reference
                // 3) Validate the kind of reference to the aggregation mode
                // 4) Start validation of the referenced resource
                outcome.Verify(() => true, "Validator does not yet support following references", Issue.UNSUPPORTED_FOLLOWING_REFERENCES, instance);
            }
 
            if(normalUris.Any())
            {
                outcome.Add(Validate(normalUris, instance, BatchValidationMode.Any));
            }

            return outcome;
        }

        public OperationOutcome VerifyPrimitiveContents(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            Trace(outcome, "Verifying content of the leaf primitive value attribute", Issue.PROCESSING_PROGRESS, instance);

            // Go look for the primitive type extensions
            //  <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-regex">
            //        <valueString value="-?([0]|([1-9][0-9]*))"/>
            //      </extension>
            //      <code>
            //        <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-json-type">
            //          <valueString value="number"/>
            //        </extension>
            //        <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-xml-type">
            //          <valueString value="int"/>
            //        </extension>
            //      </code>
            // Note that the implementer of IValueProvider may already have outsmarted us and parsed
            // the wire representation (i.e. POCO). If the provider reads xml directly, would it know the
            // type? Would it convert it to a .NET native type? How to check?

            // The spec has no regexes for the primitives mentioned below, so don't check them
            bool hasSingleRegExForValue = definition.Type.Count() == 1 && definition.Type.First().GetPrimitiveValueRegEx() != null;

            if (hasSingleRegExForValue)
            {
                var primitiveRegEx = definition.Type.First().GetPrimitiveValueRegEx();
                var value = toStringRepresentation(instance);
                var success = Regex.Match(value, "^" + primitiveRegEx + "$").Success;
                outcome.Verify(() => success, "Primitive value '{0}' does not match regex '{1}'".FormatWith(value, primitiveRegEx), Issue.CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE, instance);
            }

            return outcome;
        }

        internal void Trace(OperationOutcome outcome, string message, Issue issue, IElementNavigator location)
        {
            if (ValidationContext.Trace)
                outcome.Info(message, issue, location);
        }

        private string toStringRepresentation(IValueProvider vp)
        {
            if (vp == null || vp.Value == null) return null;

            var val = vp.Value;

            if (val is string)
                return (string)val;
            else if (val is long)
                return XmlConvert.ToString((long)val);
            else if (val is decimal)
                return XmlConvert.ToString((decimal)val);
            else if (val is bool)
                return (bool)val ? "true" : "false";
            else if (val is Hl7.FluentPath.Time)
                return ((Hl7.FluentPath.Time)val).ToString();
            else if (val is Hl7.FluentPath.PartialDateTime)
                return ((Hl7.FluentPath.PartialDateTime)val).ToString();
            else
                return val.ToString();
        }


        internal OperationOutcome ValidateMinValue(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            if (definition.MinValue != null)
            {
                // Min/max are only defined for ordered types
                if (outcome.Verify(() => definition.MinValue.GetType().IsOrderedFhirType(),
                    "MinValue was given in ElementDefinition, but type '{0}' is not an ordered type".FormatWith(definition.MinValue.TypeName),
                    Issue.PROFILE_ELEMENTDEF_MIN_USES_UNORDERED_TYPE, instance))
                {
                    //TODO: Define <=, >= on FHIR types and use these
                    //No, use "the" complex types (currently from the FP assembly), so read them into FluentPath.DateTime, then compare.
                    //
                    // return Definition.MinValue <= constructedValueFromNavigator
                    // Or assume acutal implementer of interface is PocoNavigator and get value from there
                    // Or just use Value and not support Quantity ;-)
                }
            }

            return outcome;
        }

        // return t == typeof(FhirDateTime) ||
        //t == typeof(Date) ||
        //t == typeof(Instant) ||
        //t == typeof(Model.Time) ||
        //t == typeof(FhirDecimal) ||
        //t == typeof(Integer) ||
        //t == typeof(Quantity) ||
        //t == typeof(FhirString);


        internal OperationOutcome ValidateMaxLength(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.MaxLength != null)
            {
                var maxLength = definition.MaxLength.Value;

                if (outcome.Verify(() => maxLength > 0, "MaxLength was given in ElementDefinition, but it has a negative value ({0})".FormatWith(maxLength),
                                Issue.PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE, instance))
                {
                    if (instance.Value != null)
                    {
                        //TODO: Is ToString() really the right way to turn (Fhir?) Primitives back into their original representation?
                        //If the source is POCO, hopefully FHIR types have all overloaded ToString() 
                        var serializedValue = instance.Value.ToString();
                        outcome.Verify(() => serializedValue.Length <= maxLength, "Value '{0}' is too long (maximum length is {1})".FormatWith(serializedValue, maxLength),
                            Issue.CONTENT_ELEMENT_VALUE_TOO_LONG, instance);
                    }
                }
            }

            return outcome;
        }

        internal void SnapshotNeeded(StructureDefinition definition, IArtifactSource source)
        {
            // Default implementation: call event
            if (OnSnapshotNeeded != null)
            {
                OnSnapshotNeeded(this, new OnSnapshotNeededEventArgs(definition, source));
                return;
            }

            // Else, expand, depending on our configuration
            if (ValidationContext.GenerateSnapshot)
            {
                SnapshotGeneratorSettings settings = ValidationContext.GenerateSnapshotSettings;

                if (settings == null)
                    settings = SnapshotGeneratorSettings.Default;

                var gen = new SnapshotGenerator(source, settings);
                gen.Update(definition);
            }
        }
    }



    internal static class TypeExtensions
    {
        // This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity. string? why not?
        public static bool IsOrderedFhirType(this Type t)
        {
            return t == typeof(FhirDateTime) ||
                   t == typeof(Date) ||
                   t == typeof(Instant) ||
                   t == typeof(Model.Time) ||
                   t == typeof(FhirDecimal) ||
                   t == typeof(Integer) ||
                   t == typeof(Quantity) ||
                   t == typeof(FhirString);
        }
    }

    internal static class ElementDefinitionNavigatorExtensions
    {
        public static string GetFluentPathConstraint(this ElementDefinition.ConstraintComponent cc)
        {
            return cc.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
        }

        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed)
        {
            var path = ed.Path;
            return path.Count(c => c == '.') == 1 &&
                        path.EndsWith(".value") &&
                        Char.IsLower(path[0]);
        }

        public static string ConstraintDescription(this ElementDefinition.ConstraintComponent cc)
        {
            var desc = cc.Key;

            if (cc.Human != null)
                desc += " \"" + cc.Human + "\"";

            return desc;
        }


        public static string QualifiedDefinitionPath(this ElementDefinitionNavigator nav)
        {
            string path = "";

            if (nav.StructureDefinition != null && nav.StructureDefinition.Url != null)
                path = "{" + nav.StructureDefinition.Url + "}";

            path += nav.Path;

            return path;
        }
    }

    public enum BatchValidationMode
    {
        All,
        Any
    }
}
