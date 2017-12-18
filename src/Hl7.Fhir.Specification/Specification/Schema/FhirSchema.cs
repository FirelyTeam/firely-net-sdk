using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Schema
{
    public abstract class Assertion
    {
        SchemaAnnotations OnSuccess { get; }
        SchemaAnnotations OnFailure { get; }
        SchemaAnnotations OnUndecided { get; }
    }

    public interface ISingleElementAssertion
    {
        // Who adds the annotations when?
        SchemaAnnotations Validate(IElementNavigator input, ValidationContext vc);
    }

    public interface IMultipleElementAssertion
    {
        // Who adds the annotations when?
        SchemaAnnotations Validate(IEnumerable<IElementNavigator> input, ValidationContext vc);
    }

    public static class SomeExtensions
    {
        public static SchemaAnnotations Validate(this IMultipleElementAssertion assertion, IElementNavigator input, ValidationContext vc)
            => assertion.Validate(new[] { input }, vc);
    }


    public interface IAssertionContainer
    {
        IEnumerable<Schema> Subschemas();
    }

    public class Schema : Assertion, IMultipleElementAssertion, IAssertionContainer
    {
        string Id { get; }

        ReadOnlyCollection<Assertion> Assertions { get; }

        public IEnumerable<Schema> Subschemas() 
            => new[] { this }
                .Union(Assertions.OfType<IAssertionContainer>()
                    .SelectMany(sec => sec.Subschemas()));

        // I think a schema should split the inputS into single input IElementNavs
        // No, there are multiple kinds of assertions, depending on which you have a 
        // different Validate()!
        public SchemaAnnotations Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            var multiAssertions = Assertions.OfType<IMultipleElementAssertion>();
            var singleAssertions = Assertions.OfType<ISingleElementAssertion>();

            var multiResults = collect(multiAssertions
                .Select(assert => assert.Validate(input, vc)));

            var singleResults = collect(
                 from nav in input
                 from assert in singleAssertions
                 select assert.Validate(nav, vc));

            return multiResults + singleResults;

            SchemaAnnotations collect(IEnumerable<SchemaAnnotations> bunch) =>
                bunch
                .Aggregate((sum, other) => sum += other)
                .Merge();
        }
    }
}

/*****
  {   
    define: { identified subschemas which are not evaluated }

    ref: { included set of schemas }

	group { membership condition } : { constraints for this group }

	minItems:
	maxItems:
	
    slice 
    {
        ordered: true/false
        
		// one or more groups defined by subschemas
		group { membership condition } : { constraints for this group }
		default: { constraints for this group } === group {} : {    }
    }

    children
    {
		"asdfdfs" : { constraints for this name }
		"asdfdfs" : { constraints for this name }
    }

	annotations { a tag block to execute on success }

	success { a tag block to execute }
	fail { a tag block to execute }
	undecided { a tag block to execute }
}


**/
