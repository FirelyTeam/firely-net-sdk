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

        public Validator() : this(ValidationSettings.Default)
        {

        }

        public OperationOutcome Validate(IElementNavigator instance, params string[] definitionUris)
        {
            return Validate(instance, (IEnumerable<string>)definitionUris);
        }

        public OperationOutcome Validate(IElementNavigator instance, IEnumerable<string> definitionUris)
        {
            var outcome = new OperationOutcome();

            var allDefinitions = collectApplicableProfiles(outcome, instance, references: definitionUris);
            var definitionNavs = allDefinitions.Select(sd => navigatorFromStructureDefinition(sd, outcome, instance)).Where(nav => nav != null);

            outcome.Add(Validate(instance, definitionNavs));

            return outcome;
        }

        public OperationOutcome Validate(IElementNavigator instance, StructureDefinition structureDefinition)
        {
            return Validate(instance, new[] { structureDefinition });
        }

        public OperationOutcome Validate(IElementNavigator instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            var outcome = new OperationOutcome();

            var allDefinitions = collectApplicableProfiles(outcome, instance, sds: structureDefinitions);
            var definitionNavs = allDefinitions.Select(sd => navigatorFromStructureDefinition(sd, outcome, instance)).Where(nav => nav != null);

            outcome.Add(Validate(instance, definitionNavs));

            return outcome;
        }


        private List<StructureDefinition> collectApplicableProfiles(OperationOutcome outcome, IElementNavigator instance, 
                    IEnumerable<StructureDefinition> sds=null, IEnumerable<string> references=null)
        {
            if (sds == null) sds = Enumerable.Empty<StructureDefinition>();
            if (references == null) references = Enumerable.Empty<string>();

            var metaUris = extractMetaUris(instance);
            var sdUris = sds.Select(sd => sd.Url).ToList();
            var allUris = metaUris
                            .Concat(sdUris)
                            .Concat(references)
                            .Concat(instance.TypeName != null ? new[] { ModelInfo.CanonicalUriForFhirCoreType(instance.TypeName) }  : Enumerable.Empty<string>())
                            .Distinct();

            // Avoid fetching sds that we have already been passed (though they may have been cached anyway, it's good behaviour)
            var urisToFetch = allUris.Where(uri => !sdUris.Contains(uri));
            var fetchedSds = ProfileResolutionNeeded(urisToFetch, outcome, instance);

            // Create the full list of unique SDs that might be applicable. We will purge this list later and return it,
            // so create a new list, don't alter any of the stuff we have been passed.
            var applicableSds = sds.Concat(fetchedSds).ToList();

            // TODO: Now, do consistency checks
            // * The profiles may not be inconsistent (for different resources/datatype/not abstract base for....)
            // * Purge duplicates (base for derived)

            // If done correctly, the previous will make sure this code is not needed anymore
            // First, a basic check: is the instance type equal to the defined type
            // Only do this when the underlying navigator has supplied a type (from the serialization)
            //if (instance.TypeName != null && elementConstraints.IsRootElement() && definition.StructureDefinition != null && definition.StructureDefinition.Id != null)
            //{
            //    var expected = definition.StructureDefinition.Id;

            //    if (!ModelInfo.IsCoreModelType(expected) || ModelInfo.IsProfiledQuantity(expected))
            //        expected = definition.StructureDefinition.ConstrainedType?.ToString();

            //    if (expected != null)
            //    {
            //        if (!outcome.Verify(() => instance.TypeName == expected, $"Type mismatch: instance value is of type '{instance.TypeName}', " +
            //                $"expected type '{expected}'", Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, instance))
            //            return outcome;     // Type mismatch, no use continuing validation
            //    }
            //}

            // For now, a quick algorithm
            applicableSds.RemoveAll(sd => sd.Abstract == true);
            if (applicableSds.Count > 1)
                applicableSds.RemoveAll(sd => ModelInfo.IsCoreModelType(sd.Id));

            if (!applicableSds.Any())
            {
               outcome.Info("There is no definition to validate against - nothing to validate", Issue.PROFILE_NO_PROFILE_TO_VALIDATE_AGAINST, instance);
                return applicableSds;
            }

            return applicableSds;
        }

        internal OperationOutcome Validate(IElementNavigator instance, ElementDefinitionNavigator definition)
        {
            return Validate(instance, new[] { definition });
        }


        // This is the one and only main internal entry point for all validations
        internal OperationOutcome Validate(IElementNavigator instance, IEnumerable<ElementDefinitionNavigator> definitions)
        {
            var outcome = new OperationOutcome();

            try
            {
                List<ElementDefinitionNavigator> allDefinitions = new List<ElementDefinitionNavigator>(definitions);

                if (allDefinitions.Count() == 1)
                    outcome.Add(validateElement(allDefinitions.Single(), instance));
                else
                {
                    var validators = allDefinitions.Select(nav => createValidator(nav, instance));
                    outcome.Add(this.Combine(BatchValidationMode.All, instance, validators));
                }
            }
            catch (Exception e)
            {
                outcome.Info($"Internal logic failure: {e.Message}", Issue.PROCESSING_CATASTROPHIC_FAILURE, instance);
            }

            return outcome;
        }

        private List<string> extractMetaUris(IElementNavigator instance)
        {
            // This is only for resources, but I don't bother checking, since this will return empty anyway
            return instance.GetChildrenByName("meta").ChildrenValues("profile").Cast<string>().ToList();
        }

        private Func<OperationOutcome> createValidator(ElementDefinitionNavigator nav, IElementNavigator instance)
        {
            return () => validateElement(nav, instance);
        }


        private OperationOutcome validateElement(ElementDefinitionNavigator definition, IElementNavigator instance)
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
                    // TODO: Check whether this is even true when the <type> has a profile?
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

                outcome.Add(this.ValidateFixed(elementConstraints, instance));
                outcome.Add(this.ValidatePattern(elementConstraints, instance));
                outcome.Add(this.ValidateMinMaxValue(elementConstraints, instance));
                outcome.Add(ValidateMaxLength(elementConstraints, instance));
                outcome.Add(ValidateConstraints(elementConstraints, instance));
           //     outcome.Add(ValidateBinding(elementConstraints, instance));

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


        internal OperationOutcome ValidateBinding(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.Binding != null)
            {
                var binding = definition.Binding;
                var shouldCheck = binding.Strength == BindingStrength.Required || binding.Strength == BindingStrength.Preferred;

                if(shouldCheck)
                    outcome.Info("ElementDefinition has a binding, which is not yet supported. Instance has not been validated against this binding",
                        Issue.UNSUPPORTED_BINDING_NOT_SUPPORTED, instance);
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
                    outcome.Include(Validate(instance, referencedPositionNav));
                }
            }

            return outcome;
        }
        
        internal OperationOutcome VerifyPrimitiveContents(ElementDefinition definition, IElementNavigator instance)
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


        internal void Trace(OperationOutcome outcome, string message, Issue issue, IElementNavigator location)
        {
            if (Settings.Trace || issue.Severity != OperationOutcome.IssueSeverity.Information)
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

        internal IElementNavigator ExternalReferenceResolutionNeeded(string reference, OperationOutcome outcome, IElementNavigator instance)
        {
            if (!Settings.ResolveExteralReferences) return null;

            try
            {
                // Default implementation: call event
                if (OnExternalResolutionNeeded != null)
                {
                    var args = new OnResolveResourceReferenceEventArgs(reference);
                    OnExternalResolutionNeeded(this, args);
                    return args.Result;
                }
            }
            catch(Exception e)
            {
                outcome.Info("External resolution of '{reference}' caused an error: " + e.Message, Issue.UNAVAILABLE_EXTERNAL_REFERENCE, instance);
            }

            // Else, try to resolve using the given ResourceResolver 
            // (note: this also happens when the external resolution above threw an exception)
            if (Settings.ResourceResolver != null)
            {
                try
                {
                    var poco = Settings.ResourceResolver.ResolveByUri(reference);
                    if(poco != null)
                        return new PocoNavigator(poco);
                }
                catch(Exception e)
                {
                    outcome.Info($"Resolution of reference '{reference}' using the Resolver API failed: " + e.Message, Issue.UNAVAILABLE_EXTERNAL_REFERENCE, instance);
                }
            }

            return null;        // Sorry, nothing worked
        }

        internal List<StructureDefinition> ProfileResolutionNeeded(IEnumerable<string> canonicals, OperationOutcome outcome, IElementNavigator instance)
        {
            var result = new List<StructureDefinition>();

            foreach (var uri in canonicals)
            {
                StructureDefinition structureDefinition = null;

                try
                {
                    // Once FindStructureDefinition() gets an overload to resolve multiple at the same time,
                    // use that one
                    structureDefinition = Settings.ResourceResolver.FindStructureDefinition(uri);

                    if (outcome.Verify(() => structureDefinition != null, $"Unable to resolve reference to profile '{uri}'", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, instance))
                    {
                        result.Add(structureDefinition);
                    }

                }
                catch (Exception e)
                {
                    outcome.Info($"Resolution of profile at '{uri}' failed: {e.Message}", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, instance);
                    continue;
                }
            }

            return result;
        }

        private ElementDefinitionNavigator navigatorFromStructureDefinition(StructureDefinition definition, OperationOutcome outcome, IElementNavigator instance)
        {
            if (!definition.HasSnapshot)
                SnapshotGenerationNeeded(definition, outcome, instance);

            if (outcome.Verify(() => definition.HasSnapshot,
                "Profile '{0}' does not include a snapshot. Instance data has not been validated.".FormatWith(definition.Url), Issue.UNAVAILABLE_NEED_SNAPSHOT, instance))
            {
                var nav = new ElementDefinitionNavigator(definition);

                // If the definition has no data, there's nothing to validate against, so we'll consider it "valid"
                if (outcome.Verify(() => !nav.AtRoot || nav.MoveToFirstChild(), "Profile '{0}' has no content in snapshot"
                        .FormatWith(definition.Url), Issue.PROFILE_ELEMENTDEF_IS_EMPTY, instance))
                {
                    return nav;
                }
            }

            return null;
        }

        // Note: this modifies an SD that is passed to us and will alter a possibly cached
        // object shared amongst other threads. This is generally useful and saves considerable
        // time when the same snapshot is needed again, but may result in side-effects
        internal void SnapshotGenerationNeeded(StructureDefinition definition, OperationOutcome outcome, IElementNavigator instance)
        {
            if (!Settings.GenerateSnapshot) return;

            try
            {
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
            catch (Exception e)
            {
                Trace(outcome, $"Snapshot generation failed for '{definition.Url}'. Message: {e.Message}",
                       Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED, instance);
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
