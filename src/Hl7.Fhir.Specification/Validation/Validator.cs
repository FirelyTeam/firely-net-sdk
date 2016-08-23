using Hl7.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.FluentPath;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    public class Vector
    {
        public ElementDefinitionNavigator DefinitionNavigator { get; private set; }
        public IElementNavigator Instance { get; private set; }

        public ElementDefinition Definition
        {
            get
            {
                return DefinitionNavigator.Current;
            }
        }

        public Vector(ElementDefinitionNavigator definitions, IElementNavigator instance)
        {
            if (definitions.Current == null) throw Error.Argument("definitions", "Navigator is not positioned at a node");

            DefinitionNavigator = definitions;
            Instance = instance;
        }
    }



    public class Validator
    {
        public IValidationContext ValidationContext;

        public Validator(IValidationContext context)
        {
            ValidationContext = context;
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



        public OperationOutcome Validate(string definitionUri, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            StructureDefinition structureDefinition = ValidationContext.ArtifactSource.LoadConformanceResourceByUrl(definitionUri) as StructureDefinition;

            if (outcome.Verify(() => structureDefinition != null, "Unable to resolve reference to profile '{0}'".FormatWith(definitionUri), Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, instance))
                outcome.Add(Validate(structureDefinition, instance));

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

            if (outcome.Verify(() => structureDefinition.Snapshot != null && structureDefinition.Snapshot.Element != null && structureDefinition.Snapshot.Element.Any(),
                "Profile '{0}' does not include a snapshot".FormatWith(structureDefinition.Url), Issue.UNSUPPORTED_NEED_SNAPSHOT, instance))
            {
                var nav = new ElementDefinitionNavigator(structureDefinition);

                // If the definition has no data, there's nothing to validate against, so we'll consider it "valid"
                if (outcome.Verify(() => !nav.AtRoot || nav.MoveToFirstChild(), "Profile '{0}' has no content in snapshot"
                        .FormatWith(structureDefinition.Url), Issue.PROFILE_ELEMENTDEF_IS_EMPTY, instance))
                {
                    outcome.Add(ValidateElement(nav, instance));
                }

            }

            if (outcome.Success)
                outcome.Info("Validation succeeded", Issue.PROCESSING_PROGRESS, instance);
            else
                outcome.Info("Validation failed with {0} errors".FormatWith(outcome.ListErrors().Count()), Issue.PROCESSING_PROGRESS, instance);

            return outcome;
        }


        public OperationOutcome ValidateElement(IEnumerable<ElementDefinitionNavigator> definitions, IElementNavigator instance, BatchValidationMode mode)
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

            outcome.Info("Start validation of ElementDefinition at path '{0}'".FormatWith(definition.QualifiedDefinitionPath()), Issue.PROCESSING_PROGRESS, instance);

            // Any node must either have a value, or children, or both (e.g. extensions on primitives)
            if (!outcome.Verify(() => instance.Value != null || instance.HasChildren(), "Element must not be empty", Issue.CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN, instance))
                return outcome;

            // Handle in-lined constraints on children
            outcome.Add(ValidateChildConstraints(definition, instance));

            var elementConstraints = definition.Current;

            outcome.Add(ValidateSlices(definition, instance));

            if (elementConstraints.IsPrimitiveValueConstraint())
            {
                // The "value" property of a FHIR Primitive is the bottom of our recursion chain, it does not have a nameReference
                // nor a <type>, the only thing left to do to validate the content is to validate the string representation of the
                // primitive against the regex given in the core definition
                outcome.Add(VerifyPrimitiveContents(elementConstraints, instance));
            }
            else
            {
                outcome.Add(ValidateType(elementConstraints, instance));
                outcome.Add(ValidateNameReference(elementConstraints, definition, instance));
            }

            // Min/max (cardinality) has been validated by parent, we cannot know down here
            outcome.Add(ValidateFixed(elementConstraints, instance));
            outcome.Add(ValidatePattern(elementConstraints, instance));
            outcome.Add(ValidateMinValue(elementConstraints, instance));
            // outcome.Add(ValidateMaxValue(elementConstraints, instance));
            outcome.Add(ValidateMaxLength(elementConstraints, instance));

            // Validate Binding
            // Validate Constraint

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

        internal OperationOutcome ValidateChildConstraints(ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            if (!definition.HasChildren) return outcome;

            outcome.Info("Start validation of inlined child constraints for '{0}'".FormatWith(definition.Path), Issue.PROCESSING_PROGRESS, instance);

            var matchResult = ChildNameMatcher.Match(definition, instance);

            outcome.Verify(() => !matchResult.UnmatchedInstanceElements.Any(), "Encountered unknown child elements {0}".
                            FormatWith(String.Join(",", matchResult.UnmatchedInstanceElements.Select(e => "'" + e.Name + "'"))),
                            Issue.CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN, instance);

            //TODO: Give warnings for out-of order children.  Really? That's an xml artifact, no such thing in Json!

            outcome.Add(ValidateCardinality(matchResult, instance));

            // Recursively validate my children
            foreach (Match match in matchResult.Matches)
            {
                foreach (IElementNavigator element in match.InstanceElements)
                {
                    var validator = new Validator(ValidationContext);
                    outcome.Include(validator.ValidateElement(match.Definition, element));
                }
            }

            return outcome;
        }

        internal OperationOutcome ValidateCardinality(MatchResult matchResult, INamedNode parent)
        {
            var outcome = new OperationOutcome();

            foreach (var match in matchResult.Matches)
            {
                var definition = match.Definition.Current;
                var occurs = match.InstanceElements.Count;
                var cardinality = Cardinality.FromElementDefinition(definition);

                if (outcome.Verify(() => definition.Min != null && definition.Max != null, "ElementDefinition does not specify cardinality", Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING, parent))
                {
                    outcome.Verify(() => cardinality.InRange(occurs), "Element '{0}' occurs {1} times, which is not within the specified cardinality of {2}"
                                .FormatWith(match.Definition.PathName, occurs, cardinality.ToString()), Issue.CONTENT_ELEMENT_INCORRECT_OCCURRENCE, parent);
                }
            }

            return outcome;
        }

        internal OperationOutcome ValidateNameReference(ElementDefinition definition, ElementDefinitionNavigator allDefinitions, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.NameReference != null)
            {
                outcome.Info("Start validation of constraints referred to by nameReference '{0}'".FormatWith(definition.NameReference), Issue.PROCESSING_PROGRESS, instance);

                var referencedPositionNav = allDefinitions.ShallowCopy();

                if (outcome.Verify(() => referencedPositionNav.JumpToNameReference(definition.NameReference),
                        "ElementDefinition uses a non-existing nameReference '{0}'".FormatWith(definition.NameReference),
                        Issue.PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE, instance))
                {
                    var validator = new Validator(ValidationContext);
                    outcome.Include(validator.ValidateElement(referencedPositionNav, instance));
                }
            }

            return outcome;
        }

        internal OperationOutcome ValidateType(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            outcome.Info("Validating against constraints specified by the element's defined type", Issue.PROCESSING_PROGRESS, instance);

            outcome.Verify(() => !definition.Type.Select(tr => tr.Code).Contains(null), "ElementDefinition contains a type with an empty type code", Issue.PROFILE_ELEMENTDEF_CONTAINS_NULL_TYPE, instance);

            if (definition.IsRootElementDefinition())
            {
                // This is a root element, in which the specified type is its base, and this snapshot already contains those constraints
                // as children, so we can save on processing time
                outcome.Info("This is a root ElementDefinition, skipped validation of base type ", Issue.PROCESSING_PROGRESS, instance);
                return outcome;                
            }

            // Check if this is a choice: there are multiple distinct Codes to choose from
            var types = definition.Type.Where(tr => tr.Code != null);
            var choices = types.Select(tr => tr.Code.GetLiteral()).Distinct().ToList();

            if (choices.Count() > 1)
            {
                // This is a choice type, find out what type is present in the instance data
                // (e.g. deceased[Boolean]). This is exposed by IElementNavigator.TypeName.
                var applicableChoices = types.Where(tr => tr.Code.GetLiteral() == instance.TypeName).Select(t => t.ProfileUri());

                // Instance typename must be one of the applicable types in the choice
                outcome.Verify(() => applicableChoices.Any(), "Type specified in the instance ('{0}') is not one of the allowed choices ({1})"
                            .FormatWith(instance.TypeName, String.Join(",", choices.Select(t => "'" + t + "'"))), Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE, instance);

                outcome.Include(Validate(applicableChoices, instance, BatchValidationMode.Any));
            }
            else if (choices.Count() == 1)
            {
                // Only one type present in list of typerefs, all of the typerefs are candidates
                var applicableChoices = types.Select(t => t.ProfileUri());
                outcome.Include(Validate(applicableChoices, instance, BatchValidationMode.Any));
            }
            else
            {
                outcome.Verify(() => choices.Any(), "ElementDefinition does not specify a type to validate the instance data against", Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE, instance);
            }

            //if (!outcome.Success)
            //{
            //    string message = "Contents of the element could not be validated against ";

            //    if (choices.Count() == 1)
            //        message += "any of the given type/profiles for the type '{0}'".FormatWith(choices.Single());
            //    else
            //        message += "any of the '{0}' types given in the choice".FormatWith(instance.TypeName);

            //    outcome.Verify(() => outcome.Success, message, Issue.CONTENT_ELEMENT_MUST_MATCH_TYPE, instance);
            //}

            return outcome;
        }

        public OperationOutcome VerifyPrimitiveContents(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            outcome.Info("Verifying content of the leaf primitive value attribute", Issue.PROCESSING_PROGRESS, instance);

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
            bool hasSingleRegExForValue = definition.Type.Count() == 1 && definition.Type.First().GetPrimitiveValueRegEx() != null;

            if (outcome.Verify(() => hasSingleRegExForValue, "No single regex to validate the primitive value against found at '{0}'"
                        .FormatWith(definition.Path), Issue.PROFILE_ELEMENTDEF_NO_PRIMITIVE_REGEX, instance))
            {
                var primitiveRegEx = definition.Type.First().GetPrimitiveValueRegEx();
                var value = instance.Value.ToString();
                var success = Regex.Match(value, primitiveRegEx).Success;
                outcome.Verify(() => success, "Primitive value '{0}' does not match regex '{1}'".FormatWith(value, primitiveRegEx), Issue.CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE, instance);
            }

            return outcome;
        }

        internal OperationOutcome ValidateFixed(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            if (definition.Fixed != null)
            {
                // Construct an IValueProvider based on the POCO parsed from profileElement.fixed/pattern etc.
                //IElementNavigator fixedValueNav = new PocoNavigator(Definition.Fixed);
                //return  Compare(fixedValueNav, Instance, mode: Fixed);

            }

            return outcome;
        }

        internal OperationOutcome ValidatePattern(ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            if (definition.Pattern != null)
            {
                // Construct an IValueProvider based on the POCO parsed from profileElement.fixed/pattern etc.
                //IElementNavigator fixedValueNav = new PocoNavigator(Definition.Fixed);
                //return  Compare(fixedValueNav, Instance, mode: Fixed);

            }

            return outcome;
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
                    // return Definition.MinValue <= constructedValueFromNavigator
                    // Or assume acutal implementer of interface is PocoNavigator and get value from there
                    // Or just use Value and not support Quantity ;-)
                }
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

    }



    public interface IValidationContext
    {
        IArtifactSource ArtifactSource { get; }

        // Resolver, Containing Bundle, parent Resource?
        // Options: validate_across_references, log verbosity
        // FP SymbolTable
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
        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed)
        {
            var path = ed.Path;
            return path.Count(c => c == '.') == 1 &&
                        path.EndsWith(".value") &&
                        Char.IsLower(path[0]);
        }

        public static bool IsRootElementDefinition(this ElementDefinition ed)
        {
            return !ed.Path.Contains('.');
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

    internal delegate bool Condition();


    public enum BatchValidationMode
    {
        All,
        Any
    }


    internal static class OperationOutcomeValidationExtensions
    {
        public static bool Verify(this OperationOutcome outcome, Condition condition, string message, Issue issue, INamedNode location)
        {
            if (!condition())
            {
                outcome.AddIssue(issue.ToIssueComponent(message, location));
                return false;
            }
            else
                return true;
        }

        public static void Info(this OperationOutcome outcome, string message, Issue infoIssue, INamedNode location)
        {
            outcome.AddIssue(infoIssue.ToIssueComponent(message, location));
        }


        public static OperationOutcome MakeInformational(this OperationOutcome outcome)
        {
            var result = (OperationOutcome)outcome.DeepCopy();
            foreach (var issue in result.Issue)
                issue.Severity = OperationOutcome.IssueSeverity.Information;

            return result;
        }



        public static void Combine(this OperationOutcome outcome, IEnumerable<OperationOutcome> inputs, INamedNode location, BatchValidationMode mode)
        {
            var successful = inputs.Where(i => i.Success);

            outcome.Info("Combined output of {0} validation reports follow. {1} validation(s) succeeded".FormatWith(inputs.Count(), successful.Count()), Issue.PROCESSING_PROGRESS, location);

            List<OperationOutcome> toInclude = new List<OperationOutcome>();



            if (mode == BatchValidationMode.Any && successful.Any())
            {
                foreach (var report in inputs)
                {
                    if (report.Success)
                        toInclude.Add(report);
                    else
                        toInclude.Add(report.MakeInformational());
                }
            }
            else
            {
                toInclude.AddRange(inputs);
            }

            int index = 1;

            foreach (var input in inputs)
            {
                outcome.Info("Report {0}: {1}".FormatWith(index, input.Success ? "SUCCESS" : "FAILURE"), Issue.PROCESSING_PROGRESS, location);
                outcome.Include(input);
                index += 1;
            }
        }
    }
}
