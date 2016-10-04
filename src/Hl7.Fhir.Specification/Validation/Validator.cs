/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
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
using Time = Hl7.FluentPath.Time;

namespace Hl7.Fhir.Validation
{
    public class Validator
    {
        public ValidationSettings Settings { get; private set; }

        public event EventHandler<OnSnapshotNeededEventArgs> OnSnapshotNeeded;
        public event EventHandler<OnResolveResourceReferenceEventArgs> OnExternalResolutionNeeded;

        internal ScopeTracker ScopeTracker = new ScopeTracker();  
        public Validator(ValidationSettings settings)
        {
            Settings = settings;
        }


        public OperationOutcome Validate(IElementNavigator instance)
        {
            var profiles = instance.GetChildrenByName("meta").ChildrenValues("profile").Cast<string>().ToList();

            // If the Meta.profile specifies any profiles, use those, else use the base FHIR core profile for the type
            if (profiles.Any())
            {
                return Validate(instance, profiles);
            }
            else
            {
                var uri = ModelInfo.CanonicalUriForFhirCoreType(instance.TypeName);
                return Validate(instance, uri);
            }
        }

        public OperationOutcome Validate(IElementNavigator instance, params string[] definitionUris)
        {
            return Validate(instance, (IEnumerable<string>)definitionUris);
        }

        public OperationOutcome Validate(IElementNavigator instance, IEnumerable<string> definitionUris)
        {
            var result = new OperationOutcome();
            var sds = new List<StructureDefinition>();

            foreach (var uri in definitionUris)
            {
                StructureDefinition structureDefinition = Settings.ResourceResolver.FindStructureDefinition(uri);

                if (result.Verify(() => structureDefinition != null, $"Unable to resolve reference to profile '{uri}'", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, instance))
                    sds.Add(structureDefinition);
            }

            result.Add(Validate(instance, sds));
            return result;
        }

        public OperationOutcome Validate(IElementNavigator instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            if (structureDefinitions.Count() == 1)
                return Validate(instance, structureDefinitions.Single());
            else
            {
                var validators = structureDefinitions.Select(sd => createValidator(sd, instance));
                return this.Combine(BatchValidationMode.All, instance, validators);
            }
        }

        private Func<OperationOutcome> createValidator(StructureDefinition sd, IElementNavigator instance)
        {
            return () => Validate(instance, sd);
        }

        public OperationOutcome Validate(IElementNavigator instance, StructureDefinition structureDefinition)
        {
            // New validation -> reset scope
            //this.ScopeTracker = new ScopeTracker();

            var outcome = new OperationOutcome();

            if (!structureDefinition.HasSnapshot)
            {
                // Note: this modifies an SD that is passed to us and will alter a possibly cached
                // object shared amongst other threads. This is generally useful and saves considerable
                // time when the same snapshot is needed again, but may result in side-effects

                try
                {
                    // We'll call out to an external component, so catch any exceptions and include them in our report
                    SnapshotNeeded(structureDefinition);
                }
                catch (Exception e)
                {
                    Trace(outcome, $"Snapshot generation failed for {structureDefinition.Url}. Message: {e.Message}",
                           Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED, instance);
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

     
        internal OperationOutcome ValidateElement(ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            try
            {
                Trace(outcome, "Start validation of ElementDefinition at path '{0}'".FormatWith(definition.QualifiedDefinitionPath()), Issue.PROCESSING_PROGRESS, instance);

                ScopeTracker.Enter(instance);

                // Any node must either have a value, or children, or both (e.g. extensions on primitives)
                if (!outcome.Verify(() => instance.Value != null || instance.HasChildren(), "Element must not be empty", Issue.CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN, instance))
                    return outcome;

                var elementConstraints = definition.Current;

                // First, a basic check: is the instance type equal to the defined type
                // Only do this when the underlying navigator has supplied a type (from the serialization)
                if (instance.TypeName != null && elementConstraints.IsRootElement() && definition.StructureDefinition != null && definition.StructureDefinition.Id != null)
                {
                    var expected = definition.StructureDefinition.Id;

                    if (!ModelInfo.IsCoreModelType(expected) || ModelInfo.IsProfiledQuantity(expected))
                        expected = definition.StructureDefinition.ConstrainedType?.ToString();

                    if (expected != null)
                    {
                        if (!outcome.Verify(() => instance.TypeName == expected, $"Type mismatch: instance value is of type '{instance.TypeName}', " +
                                $"expected type '{expected}'", Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, instance))
                            return outcome;     // Type mismatch, no use continuing validation
                    }
                }

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
                        outcome.Add(this.ValidateType(elementConstraints, instance));
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

                // If the report only has partial information, no use to show the hierarchy, so flatten it.
                if (Settings.Trace == false) outcome.Flatten();

                return outcome;
            }
            finally
            {
                ScopeTracker.Leave(instance);
            }
        }


        internal OperationOutcome ValidateConstraints(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            var context = ScopeTracker.ResourceContext(instance);

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
                if (outcome.Verify(() => fpExpression != null, "Encountered an invariant ({0}) that has no FluentPath expression, skipping validation of this constraint"
                            .FormatWith(constraintElement.Key), Issue.UNSUPPORTED_CONSTRAINT_WITHOUT_FLUENTPATH, instance))
                {
                    if (Settings.SkipConstraintValidation)
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
            if (Settings.Trace)
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
                var defType = definition.MinValue.GetType();

                if (defType == typeof(Model.Quantity))
                    outcome.Info("Comparing Quantities is not yet implemented", Issue.UNSUPPORTED_MIN_MAX_QUANTITY, instance);

                if (instance.Value != null)
                {              
                    // Min/max are only defined for ordered types
                    if (outcome.Verify(() => defType.IsOrderedFhirType(),
                        "MinValue was given in ElementDefinition, but type '{0}' is not an ordered type".FormatWith(definition.MinValue.TypeName),
                        Issue.PROFILE_ELEMENTDEF_MIN_USES_UNORDERED_TYPE, instance))
                    {
                        if (defType == typeof(FhirDateTime) && instance.Value is PartialDateTime)
                            messageSmallerThan(outcome, () => (PartialDateTime)instance.Value >= ((FhirDateTime)definition.MinValue).ToPartialDateTime(), instance, definition);
                        else if (defType == typeof(Date) && instance.Value is PartialDateTime)
                            messageSmallerThan(outcome, () => (PartialDateTime)instance.Value >= ((Date)definition.MinValue).ToPartialDateTime(), instance, definition);
                        else if (defType == typeof(Instant) && instance.Value is PartialDateTime)
                            messageSmallerThan(outcome, () => (PartialDateTime)instance.Value >= ((Instant)definition.MinValue).ToPartialDateTime(), instance, definition);
                        else if (defType == typeof(Model.Time) && instance.Value is Hl7.FluentPath.Time)
                            messageSmallerThan(outcome, () => (Hl7.FluentPath.Time)instance.Value >= ((Model.Time)definition.MinValue).ToTime(), instance, definition);
                        else if (defType == typeof(FhirDecimal) && instance.Value is decimal)
                            messageSmallerThan(outcome, () => (decimal)instance.Value >= ((FhirDecimal)definition.MinValue).Value, instance, definition);
                        else if (defType == typeof(Integer) && instance.Value is long)
                            messageSmallerThan(outcome, () => (long)instance.Value >= ((Integer)definition.MinValue).Value, instance, definition);
                        else if (defType == typeof(FhirString) && instance.Value is string)
                            messageSmallerThan(outcome, () => String.Compare((string)instance.Value, ((FhirString)definition.MinValue).Value) != -1, instance, definition);
                        else
                        {
                            outcome.Info($"Min value '{definition.MinValue}' and instance value '{instance.Value}' are of incompatible types and can not be compared", Issue.CONTENT_ELEMENT_PRIMITIVE_VALUE_NOT_COMPARABLE, instance);
                        }
                    }
                }
            }

            return outcome;
        }

        private static void messageSmallerThan(OperationOutcome outcome, Condition comparer, IElementNavigator instance, ElementDefinition min)
        {
            outcome.Verify(comparer, 
                    $"Instance value '{instance.Value}' is smaller than the minimal value '{min.MinValue}'", Issue.CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_SMALL, instance);
        }

        //  t == typeof(FhirString);

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


        internal IElementNavigator ExternalReferenceResolutionNeeded(string reference)
        {
            if (!Settings.ResolveExteralReferences) return null;


            // Default implementation: call event
            if (OnExternalResolutionNeeded != null)
            {
                var args = new OnResolveResourceReferenceEventArgs(reference);
                OnExternalResolutionNeeded(this, args);
                return args.Result;
            }

            // Else, try to resolve using the given ResourceResolver
            if (Settings.ResourceResolver != null)
            {
                return new PocoNavigator(Settings.ResourceResolver.ResolveByUri(reference));
            }

            return null;        // Sorry, nothing worked
        }

        internal void SnapshotNeeded(StructureDefinition definition)
        {
            if (!Settings.GenerateSnapshot) return;

            // Default implementation: call event
            if (OnSnapshotNeeded != null)
            {
                OnSnapshotNeeded(this, new OnSnapshotNeededEventArgs(definition, Settings.ResourceResolver));
                return;
            }

            // Else, expand, depending on our configuration
            if (Settings.ResourceResolver != null)
            {
                SnapshotGeneratorSettings settings = Settings.GenerateSnapshotSettings;

                if (settings == null)
                    settings = SnapshotGeneratorSettings.Default;

                var gen = new SnapshotGenerator(Settings.ResourceResolver, settings);
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
                   t == typeof(Model.Quantity) ||
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


    public class OnSnapshotNeededEventArgs : EventArgs
    {
        public OnSnapshotNeededEventArgs(StructureDefinition definition, IResourceResolver resolver)
        {
            Definition = definition;
            Resolver = resolver;
        }

        public StructureDefinition Definition { get; }

        public IResourceResolver Resolver { get; }
    }

    public class OnResolveResourceReferenceEventArgs : EventArgs
    {
        public OnResolveResourceReferenceEventArgs(string reference)
        {
            Reference = reference;
        }

        public string Reference { get; }

        public IElementNavigator Result { get; set; }
    }


    public enum BatchValidationMode
    {
        All,
        Any,
        Once
    }
}
