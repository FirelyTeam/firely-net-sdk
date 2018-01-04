using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class ElementSchema : Assertion, IGroupAssertion
    {
        public readonly string Id;
        public readonly IEnumerable<Assertion> Assertions;

        public ElementSchema(params Assertion[] assertions)
        {
            Assertions = assertions;
        }

        public ElementSchema(IEnumerable<Assertion> assertions) => Assertions = assertions;

        public ElementSchema(string id, params Assertion[] assertions) : this(assertions)
        {
            Id = id;
        }

        public ElementSchema(string id, IEnumerable<Assertion> assertions) : this(assertions)
        {
            Id = id;
        }

        public override IEnumerable<SchemaTags> CollectTags()
            => Assertions
                .Aggregate(SchemaTags.Success.Collection, (sum, ass) => sum.Product(ass.CollectTags()));

        public SchemaTags Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            var multiAssertions = Assertions.OfType<IGroupAssertion>();
            var singleAssertions = Assertions.OfType<IMemberAssertion>();

            var multiResults = collect(multiAssertions
                .Select(assert => assert.Validate(input, vc)));

            var singleResults = collect(
                 from nav in input
                 from assert in singleAssertions
                 select assert.Validate(nav, vc));

            return multiResults + singleResults;

            SchemaTags collect(IEnumerable<SchemaTags> bunch) => bunch.Aggregate((sum, other) => sum += other);
        }
    }
}


/*****

  {   
    define: { identified subschemas which are not evaluated }

    ref: "external reference" 

    {
       // nested schema
    }

	minItems:
	maxItems:

    group { membership condition } : { constraints for this group }

    slice 
    {
        ordered: true/false

		// one or more groups defined by subschemas
		group { membership condition } : { constraints for this group }
		default: { constraints for this group } === group {} : {    }    // no default: {} : { fail }
    }

    children
    {
		"asdfdfs" : { constraints for this name }
		"asdfdfs" : { constraints for this name }
    }

	success { a tag block to execute }
	fail { a tag block to execute }
	undecided { a tag block to execute }
}

****/

