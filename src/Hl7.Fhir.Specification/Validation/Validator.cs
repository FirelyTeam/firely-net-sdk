using Hl7.Fhir.Core.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    public class Validator
    {
        OperationOutcome Report = new OperationOutcome();

        IElementNavigator Instance;
        ElementDefinitionNavigator DefinitionNavigator;

        Lazy<IList<IElementNavigator>> _instanceChildren;
        IList<IElementNavigator> InstanceChildren { get { return _instanceChildren.Value; } }

        ElementDefinition Definition { get { return DefinitionNavigator.Current; } }

        public Validator(ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            Instance = instance;
            DefinitionNavigator = definition;

            _instanceChildren = new Lazy<IList<IElementNavigator>>(() => Instance.Children().ToList());
        }

        public bool Validate()
        {
            // Any node must either have a value, or children, or both (e.g. extensions on primitives)
            if (!verify(() => Instance.Value != null || InstanceChildren.Any(), "Element must not be empty", Issue.STRUCT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN))
                return false;

            //TODO: Probably better to first start with <type> section, then we have figured out the type in the instance
            //only if that is correct, it makes sense to compare to fixed, min/max values etc.
            var types = Definition.Type.Select(tr => tr.Code.Value).Distinct();
            if(!Definition.Type.Any())
            {
                throw new NotImplementedException("ElementDefs without <type>");
                // Can only happen at the "value" element of a FHIR Primitive
                if(isPrimitiveValuePath(Definition.Path))
                {

                }
            }

            if(DefinitionNavigator.HasChildren)
            {
                // Handle in-lined constraints on children
                var matchResult = InstanceToProfileMatcher.Match(DefinitionNavigator, Instance);

                verify(() => !matchResult.UnmatchedInstanceElements.Any(), "Encountered unknown child elements {0}".
                                FormatWith(String.Join(",", matchResult.UnmatchedInstanceElements.Select(e => "'" + e.Name + "'"))),
                                Issue.STRUCT_ELEMENT_HAS_UNKNOWN_CHILDREN);

                //TODO: Give warnings for out-of order children

                //TODO: Validate cardinality

                // Recursively validate my children
                foreach(Match match in matchResult.Matches)
                {
                    foreach (IElementNavigator element in match.InstanceElements)
                    {
                        var validator = new Validator(match.Definition, element);
                        validator.Validate();
                    }
                }
            }


            // Min/max (cardinality) has been validated by parent, we cannot know down here
            ValidateFixed();
            ValidatePattern();
            ValidateMinValue();
            // result |= ValidateMaxValue();
            ValidateMaxLength();

            // Validate Binding
            // Validate Constraint

            return Report.Success();
        }

        private static bool isPrimitiveValuePath(string path)
        {
            return path.Count(c => c == '.') == 1 &&
                        path.EndsWith(".value") &&
                        Char.IsLower(path[0]);
        }


        internal void ValidateFixed()
        {
            if (Definition.Fixed == null) return;

            // Construct an IValueProvider based on the POCO parsed from profileElement.fixed/pattern etc.
            IElementNavigator fixedValueNav = new PocoNavigator(Definition.Fixed);
            //return  Compare(fixedValueNav, Instance, mode: Fixed);
        }

        internal void ValidatePattern()
        {
            if (Definition.Pattern == null) return;

            // Construct an IValueProvider based on the POCO parsed from profileElement.fixed/pattern etc.
            IElementNavigator fixedValueNav = new PocoNavigator(Definition.Pattern);
            //return  Compare(fixedValueNav, Instance, mode: Pattern);
        }

        internal void ValidateMinValue()
        {
            if (Definition.MinValue == null) return;

            // Min/max are only defined for ordered types
            if(!verify(() => Definition.MinValue.GetType().IsOrderedFhirType(),
                "MinValue was given in ElementDefinition, but type '{0}' is not an ordered type".FormatWith(Definition.MinValue.TypeName),
                Issue.PROFILE_ELEMENTDEF_MIN_USES_UNORDERED_TYPE)) return;

            //TODO: Define <=, >= on FHIR types and use these
            // return Definition.MinValue <= constructedValueFromNavigator
            // Or assume acutal implementer of interface is PocoNavigator and get value from there
            // Or just use Value and not support Quantity ;-)
        }

        internal void ValidateMaxLength()
        {
            if (Definition.MaxLength == null) return;

            var maxLength = Definition.MaxLength.Value;

            if (!verify(() => maxLength > 0, "MaxLength was given in ElementDefinition, but it has a negative value ({0})".FormatWith(maxLength), 
                Issue.PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE)) return;

            if(Instance.Value != null)
            {
                //TODO: Is ToString() really the right way to turn (Fhir?) Primitives back into their original representation?
                //If the source is POCO, hopefully FHIR types have all overloaded ToString() 
                var serializedValue = Instance.Value.ToString();
                verify(() => serializedValue.Length > maxLength, "Value '{0}' is too long (maximum length is {1})".FormatWith(serializedValue, maxLength),
                    Issue.VALUE_ELEMENT_VALUE_TOO_LONG);
            }
        }

        private bool verify(Condition condition, string message, Issue issue, IPositionProvider location = null)                                
        {
            if (!condition())
            {
                var ic = new OperationOutcome.IssueComponent() { Severity = issue.Severity, Code = issue.Type, Diagnostics = message };
                ic.Details = issue.ToCodeableConcept();

                //TODO: add position info 
                if (location == null) location = Instance;

                Report.Issue.Add(ic);

                return false;
            }
            else
                return true;
        }
    }


    public delegate bool Condition();

    internal class ValidationContext
    {
        // Resolver, Containing Bundle, parent Resource?
        // Options: continue on non-resolvable
    }


    public class Issue
    {
        public int Code;
        public OperationOutcome.IssueSeverity Severity;
        public OperationOutcome.IssueType Type;

        public CodeableConcept ToCodeableConcept()
        {
            return new CodeableConcept("http://hl7.org/fhir/validation-operation-outcome", Code.ToString());
        }

        private static Issue def(int code, OperationOutcome.IssueSeverity severity, OperationOutcome.IssueType type)
        {
            return new Issue() { Code = code, Severity = severity, Type = type };
        }

        // Structural
        public static readonly Issue STRUCT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN = def(100, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);
        public static readonly Issue STRUCT_ELEMENT_HAS_UNKNOWN_CHILDREN = def(101, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        // Value
        public static readonly Issue VALUE_ELEMENT_VALUE_TOO_LONG = def(500, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Value);

        // Profile problems
        public static readonly Issue PROFILE_ELEMENTDEF_MIN_USES_UNORDERED_TYPE = def(1000, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAX_USES_UNORDERED_TYPE = def(1001, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
        public static readonly Issue PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE = def(1010, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.BusinessRule);
    }


    public static class OperationOutcomeExtensions
    {
        public static bool Success(this OperationOutcome.IssueComponent ic)
        {
            return ic.Severity != null && ic.Severity.Value.IsSuccess();
        }

        public static bool Failure(this OperationOutcome.IssueComponent ic)
        {
            return !ic.Success();
        }

        public static bool Success(this OperationOutcome results)
        {
            return !results.Failure();
        }

        public static bool Failure(this OperationOutcome results)
        {
            return results.Issue.Any(i => i.Failure());
        }


        public static bool IsSuccess(this OperationOutcome.IssueSeverity sev)
        {
            return sev == OperationOutcome.IssueSeverity.Information ||
                    sev == OperationOutcome.IssueSeverity.Warning;
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

}
