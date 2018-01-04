using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public abstract class Assertion
    {
        //TODO: Move Id here?

        /// <summary>
        /// Tags that this assertion would provide on success.
        /// </summary>
        /// <remarks>
        /// Is a list of SchemaTags, since the assertion (i.e. a slice) may provide multiple
        /// possible outcomes.
        /// </remarks>
        public abstract IEnumerable<Assertions> Collect();

        public static ElementSchema operator +(Assertion left, Assertion right)
            => new ElementSchema(left, right);

        public abstract JToken ToJson();

    }

    /// <summary>
    /// Implemented by assertions that work on a single node (IElementNavigator)
    /// </summary>
    /// <remarks>
    /// Examples are fixed, binding, working on a single IElementNavigator.Value, and
    /// children, working on the children of a single IElementNavigator
    /// </remarks>
    public interface IMemberAssertion
    {
        Assertions Validate(IElementNavigator input, ValidationContext vc);
    }

    /// <summary>
    /// Implemented by assertions that work on groups of nodes
    /// </summary>
    /// <remarks>
    /// Examples are subgroups, ref, minItems, slice
    /// </remarks>
    public interface IGroupAssertion
    {
        List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc);
    }


    public interface IMergeableAssertion
    {
        IMergeableAssertion Merge(IMergeableAssertion other);
    }

    public class Assertions : ReadOnlyCollection<Assertion>
    {
        public static readonly Assertions Success = new Assertions(ResultAssertion.Success);
        public static readonly Assertions Failure = new Assertions(ResultAssertion.Failure);
        public static readonly Assertions Undecided = new Assertions(ResultAssertion.Undecided);
        public static readonly Assertions Empty = new Assertions();

        public Assertions(params Assertion[] assertions) : this(assertions.AsEnumerable())
        {
        }

        public Assertions(IEnumerable<Assertion> assertions) : base(merge(assertions).ToList())
        {
        }

        public IEnumerable<Assertions> Collection => new[] { this };

        public static Assertions operator +(Assertions left, Assertions right)
            => new Assertions(left.Union(right));

        public static Assertions operator +(Assertions left, Assertion right)
                => new Assertions(left.Union(new[] { right }));

        private static IEnumerable<Assertion> merge(IEnumerable<Assertion> assertions)
        {
            var mergeable = assertions.OfType<IMergeableAssertion>();
            var nonMergeable = assertions.Where(a => !(a is IMergeableAssertion));

            var merged =
                from sa in mergeable
                group sa by sa.GetType() into grp
                select (Assertion)grp.Aggregate((sum, other) => sum.Merge(other));

            return nonMergeable.Union(merged);
        }

        public ResultAssertion Result => this.OfType<ResultAssertion>().Single();
    }

    public static class AssertionsExtensions
    {
        public static IEnumerable<Assertions> Product(this IEnumerable<Assertions> left, IEnumerable<Assertions> right)
            => from leftST in left
               from rightST in right
               select leftST + rightST;
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

