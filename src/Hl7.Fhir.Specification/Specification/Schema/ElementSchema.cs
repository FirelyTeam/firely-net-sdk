using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    public class ElementSchema : Assertion, IGroupAssertion, IMergeableAssertion
    {
        public static readonly ElementSchema Empty = new ElementSchema();

        public readonly string Id;
        public readonly Assertions Members;

        public ElementSchema(Assertions assertions)
        {
            Members = assertions;
        }

        public ElementSchema(params Assertion[] assertions) : this(new Assertions(assertions))  { }

        public ElementSchema(IEnumerable<Assertion> assertions) : this(new Assertions(assertions))    { }

        public ElementSchema(string id, params Assertion[] assertions) : this(assertions)
        {
            Id = id;
        }

        public ElementSchema(string id, IEnumerable<Assertion> assertions) : this(assertions)
        {
            Id = id;
        }

        public ElementSchema(string id, Assertions assertions) : this(assertions)
        {
            Id = id;
        }

        public override IEnumerable<Assertions> Collect()
            => Members
                .Aggregate(Assertions.Success.Collection, (sum, ass) => sum.Product(ass.Collect()));

        public List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            var multiAssertions = Members.OfType<IGroupAssertion>();
            var singleAssertions = Members.OfType<IMemberAssertion>();

            var multiResults = multiAssertions
                                .SelectMany(ma => ma.Validate(input, vc));

            var singleResults = input
                            .Select(nav => (collectPerInstance(nav),nav));

            return multiResults.Union(singleResults).ToList();

            Assertions collectPerInstance(IElementNavigator nav) =>
                collect( from assert in singleAssertions
                        select assert.Validate(nav, vc) );

            Assertions collect(IEnumerable<Assertions> bunch) => bunch.Aggregate((sum, other) => sum += other);
        }

        public override JToken ToJson()
        {
            return new JObject(new JProperty("id", Id))
            {
                Members.Select(mem => nest(mem.ToJson()))
            };

            JToken nest(JToken mem) =>
                mem is JObject ? new JProperty("nested", mem) : mem;
        }

        public IMergeableAssertion Merge(IMergeableAssertion other) =>
            other is ElementSchema schema ?  new ElementSchema(this.Members + schema.Members)
                : throw Error.InvalidOperation($"Internal logic failed: tried to merge an ElementSchema with a {other.GetType().Name}");
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

