using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class ElementSchema : IAssertion, IGroupValidatable, IMergeable, ICollectable
    {
        public static readonly ElementSchema Empty = new ElementSchema();

        public readonly string Id;
        public readonly Assertions Members;

        public ElementSchema(Assertions assertions)
        {
            Members = assertions;
        }

        public ElementSchema(params IAssertion[] assertions) : this(new Assertions(assertions))  { }

        public ElementSchema(IEnumerable<IAssertion> assertions) : this(new Assertions(assertions))    { }

        public ElementSchema(string id, params IAssertion[] assertions) : this(assertions)
        {
            Id = id;
        }

        public ElementSchema(string id, IEnumerable<IAssertion> assertions) : this(assertions)
        {
            Id = id;
        }

        public ElementSchema(string id, Assertions assertions) : this(assertions)
        {
            Id = id;
        }

        public IEnumerable<Assertions> Collect()
            => Members
                .Aggregate(Assertions.Success.Collection, (sum, ass) => sum.Product(ass.Collect()));

        public List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            var multiAssertions = Members.OfType<IGroupValidatable>();
            var singleAssertions = Members.OfType<IValidatable>();

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

        public JToken ToJson()
        {
            var result = new JObject();
            if (Id != null) result.Add(new JProperty("id", Id));
            result.Add(Members.Select(mem => nest(mem.ToJson())));
            return result;

            JToken nest(JToken mem) =>
                mem is JObject ? new JProperty("nested", mem) : mem;
        }

        public IMergeable Merge(IMergeable other) =>
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

