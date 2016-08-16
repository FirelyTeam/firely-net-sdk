using Hl7.Fhir.Core.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    public class Validator
    {
        public OperationOutcome Report = new OperationOutcome();

        public IElementNavigator Instance;
        public ElementDefinitionNavigator DefinitionNavigator;
        public IValidationContext ValidationContext;

        internal ElementDefinition Definition { get { return DefinitionNavigator.Current; } }

        public Validator(ElementDefinitionNavigator definition, IElementNavigator instance, IValidationContext context)
        {
            Instance = instance;
            DefinitionNavigator = definition;
            ValidationContext = context;
        }

        public bool Validate()
        {
            // Any node must either have a value, or children, or both (e.g. extensions on primitives)
            if (!verify(() => Instance.Value != null || Instance.HasChildren(), "Element must not be empty", Issue.CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN))
                return false;

            if (DefinitionNavigator.HasChildren)
            {
                // Handle in-lined constraints on children
                ValidateChildConstraints();
            }

            if (Definition.Slicing != null)
            {
                // This is the slicing entry
                // TODO: Find my siblings and try to validate the content against
                // them. There should be exactly one slice validating against the
                // content, otherwise the slicing is ambiguous. If there's no match
                // we fail validation as well. 
                // For now, we do not handle slices
                verify(() => Definition.Slicing == null, "ElementDefinition uses slicing, which is not yet supported. Instance has not been validated against " +
                            "one of the slices", Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE);
            }


            ValidateType();
            ValidateNameReference();

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

        internal void ValidateChildConstraints()
        {
            var matchResult = InstanceToProfileMatcher.Match(DefinitionNavigator, Instance);

            verify(() => !matchResult.UnmatchedInstanceElements.Any(), "Encountered unknown child elements {0}".
                            FormatWith(String.Join(",", matchResult.UnmatchedInstanceElements.Select(e => "'" + e.Name + "'"))),
                            Issue.CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN);

            //TODO: Give warnings for out-of order children

            ValidateCardinality(matchResult);

            // Recursively validate my children
            foreach (Match match in matchResult.Matches)
            {
                foreach (IElementNavigator element in match.InstanceElements)
                {
                    var validator = new Validator(match.Definition, element, ValidationContext);
                    validator.Validate();
                }
            }
        }

        internal void ValidateCardinality(MatchResult matchResult)
        {
            foreach(var match in matchResult.Matches)
            {
                var definition = match.Definition.Current;
                var occurs = match.InstanceElements.Count;
                var cardinality = Cardinality.FromElementDefinition(definition);

                if (verify(() => definition.Min != null && definition.Max != null, "ElementDefinition does not specify cardinality", Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING))
                {
                    verify(() => cardinality.InRange(occurs), "Element '{0}' occurs {1} times, which is not within the specified cardinality of {2}"
                                .FormatWith(match.Definition.Current.Name, occurs, cardinality.ToString()), Issue.CONTENT_ELEMENT_INCORRECT_OCCURRENCE);
                }
            }
        }

        internal void ValidateNameReference()
        {
            if (Definition.NameReference != null)
            {
                var referencedPositionNav = DefinitionNavigator.ShallowCopy();

                if (verify(() => referencedPositionNav.JumpToNameReference(Definition.NameReference),
                        "ElementDefinition uses a non-existing nameReference '{0}'".FormatWith(Definition.NameReference),
                        Issue.PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE))
                {
                    var validator = new Validator(referencedPositionNav, Instance, ValidationContext);
                    validator.Validate();
                }

            }
        }

        internal void ValidateType()
        {
            if (Definition.IsPrimitiveValuePath())
            {
                // The "value" property of a FHIR Primitive is the bottom of our recursion chain, it does not have a nameReference
                // nor a <type>, the only thing left to do to validate the content is to validate the string representation of the
                // primitive against the regex given in the core definition
                VerifyPrimitiveContents();
                return;
            }

            verify(() => !Definition.Type.Select(tr => tr.Code).Contains(null), "ElementDefinition contains a type with an empty type code", Issue.PROFILE_ELEMENTDEF_CONTAINS_NULL_TYPE);

            // Make a list of unique Codes in the typerefs (remember, there may be more TypeRefs for a single type, 
            // each with a different profile on that type)
            var allowedTypes = Definition.Type.Select(tr => tr.Code).Where(c => c != null).Distinct();
            IEnumerable<ElementDefinition.TypeRefComponent> typeRefsToValidate;

            if (allowedTypes.Count() > 1)
            {
                // This is a choice type, find out what type is present in the instance data
                // (e.g. deceased[Boolean]). This is exposed by IElementNavigator.TypeName.
                FHIRDefinedType instanceType;
                bool isKnownType = Enum.TryParse<FHIRDefinedType>(Instance.TypeName, out instanceType);
                if (!verify(() => isKnownType, "Instance data uses an unknown FHIR type '{0}'".
                                    FormatWith(Instance.TypeName), Issue.CONTENT_ELEMENT_HAS_UNKNOWN_TYPE)) return;

                // Instance typename must be one of the allowed types in the choice
                verify(() => allowedTypes.Contains(instanceType), "Type specified in the instance ('{0}') is not one of the allowed choices ({1})"
                            .FormatWith(Instance.TypeName, String.Join(",", allowedTypes.Select(t => "'" + t + "'"))), Issue.CONTENT_ELEMENT_HAS_UNALLOWED_TYPE);

                // Validate against one or more typerefs for this type
                typeRefsToValidate = Definition.Type.Where(tr => tr.Code == instanceType);
            }
            else if (allowedTypes.Count() == 1)
            {
                // Only one type present in list of typerefs, all of the typerefs are candidates
                typeRefsToValidate = Definition.Type;
            }
            else
            {
                verify(() => allowedTypes.Any(), "ElementDefinition does not specify a type to validate the instance data against", Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE);
                return;
            }

            // The instance must validate against one of the typerefs in the list (it's an OR)
            bool success = false;
            foreach (var typeRef in typeRefsToValidate)
            {
                // Try to get the structure definition for this type. 
                // Unprofiled types will resolve the core structure definition
                var uri = typeRef.ProfileUri();
                var typeDefinition = ValidationContext.ArtifactSource.LoadConformanceResourceByUrl(uri) as StructureDefinition;

                if (verify(() => typeDefinition != null, "Unable to resolve referenced profile '{0}'".FormatWith(uri), Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE))
                {
                    if (verify(() => typeDefinition.Snapshot != null && typeDefinition.Snapshot.Element != null && typeDefinition.Snapshot.Element.Any(),
                        "Referenced profile '{0}' does not include a snapshot".FormatWith(uri), Issue.UNSUPPORTED_NEED_SNAPSHOT))
                    {
                        // We'll have success when we match one of the profiles in the list
                        var validator = new Validator(new ElementDefinitionNavigator(typeDefinition.Snapshot.Element), Instance, ValidationContext);
                        success |= validator.Validate();
                    }
                }
            }

            if (!success)
            {
                string message = "Contents of the element could not be validated against ";

                if (typeRefsToValidate.Count() == 1)
                {
                    message += "the type '{0}'".FormatWith(typeRefsToValidate.Single().ToHumanReadable());
                }
                else
                {
                    message += "any of the '{0}' types given in the choice".FormatWith(typeRefsToValidate.First().Code.Value);
                }

                verify(() => success, message, Issue.CONTENT_ELEMENT_MUST_MATCH_TYPE);
            }

            //return success;
        }

        public void VerifyPrimitiveContents()
        {
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

            bool hasSingleRegExForValue = Definition.Type.Count() == 1 && Definition.Type.First().GetPrimitiveValueRegEx() != null;

            if (verify(() => hasSingleRegExForValue, "Core specification for datatype '{0}' does not contain the regex to validate the value"
                        .FormatWith(DefinitionNavigator.ParentPath), Issue.PROFILE_ELEMENTDEF_NO_PRIMITIVE_REGEX))
            {
                var primitiveRegEx = Definition.Type.First().GetPrimitiveValueRegEx();
                var value = Instance.Value.ToString();
                var success = Regex.Match(value, primitiveRegEx).Success;
                verify(() => success, "Primitive value '{0}' does not match regex '{1}'".FormatWith(value, primitiveRegEx), Issue.CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE);
            }
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
            if (!verify(() => Definition.MinValue.GetType().IsOrderedFhirType(),
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

            if (Instance.Value != null)
            {
                //TODO: Is ToString() really the right way to turn (Fhir?) Primitives back into their original representation?
                //If the source is POCO, hopefully FHIR types have all overloaded ToString() 
                var serializedValue = Instance.Value.ToString();
                verify(() => serializedValue.Length > maxLength, "Value '{0}' is too long (maximum length is {1})".FormatWith(serializedValue, maxLength),
                    Issue.CONTENT_ELEMENT_VALUE_TOO_LONG);
            }
        }

        private delegate bool Condition();

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



    internal class Cardinality
    {
        public int Min;
        public string Max;


        public static Cardinality FromElementDefinition(ElementDefinition def)
        {
            if (def.Min == null) throw Error.ArgumentNull("def.Min");
            if (def.Max == null) throw Error.ArgumentNull("def.Max");

            return new Cardinality(def.Min.Value, def.Max);
        }

        public Cardinality(int min, string max)
        {
            if (max == null) throw Error.ArgumentNull("max");

            Min = min;
            Max = max;
        }

        public bool InRange(int x)
        {
            if (x < Min)
                return false;

            if (Max == "*")
                return true;

            int max = Convert.ToInt16(Max);
            if (x > max)
                return false;

            return true;
        }

        public override string ToString()
        {
            return Min + ".." + Max;
        }
    }

    public interface IValidationContext
    {
        IArtifactSource ArtifactSource { get; }

        // Resolver, Containing Bundle, parent Resource?
        // Options: validate_across_references
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
       public static bool IsPrimitiveValuePath(this ElementDefinition ed)
        {
            var path = ed.Path;
            return path.Count(c => c == '.') == 1 &&
                        path.EndsWith(".value") &&
                        Char.IsLower(path[0]);
        }

    }

}
