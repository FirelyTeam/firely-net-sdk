using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public abstract class Assertion
    {
        public static readonly Assertion Succeed = new Succeed();
        public static readonly Assertion Fail = new Fail();
        public static readonly Assertion Undecided = new Undecided();
        //TODO: Move Id here?
        /// <summary>
        /// Tags that this assertion would provide on success.
        /// </summary>
        /// <remarks>
        /// Is a list of SchemaTags, since the assertion (i.e. a slice) may provide multiple
        /// possible outcomes.
        /// </remarks>
        public abstract IEnumerable<SchemaTags> CollectTags();

        public abstract IEnumerable<Assertions> CollectAssertions(Predicate<Assertion> pred);
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
        SchemaTags Validate(IElementNavigator input, ValidationContext vc);
    }

    /// <summary>
    /// Implemented by assertions that work on groups of nodes
    /// </summary>
    /// <remarks>
    /// Examples are subgroups, ref, minItems, slice
    /// </remarks>
    public interface IGroupAssertion
    {
        SchemaTags Validate(IEnumerable<IElementNavigator> input, ValidationContext vc);
    }


    public class Assertions : ReadOnlyCollection<Assertion>
    {
        public static readonly Assertions Empty = new Assertions();

        public Assertions(params Assertion[] assertions) : this(assertions.AsEnumerable())
        {
        }

        public Assertions(IEnumerable<Assertion> assertions) : base(assertions.ToList())
        {
        }

        public IEnumerable<Assertions> Collection => new[] { this };

        public static Assertions operator +(Assertions left, Assertions right)
            => new Assertions(left.Union(right));

        public static Assertions operator +(Assertions left, Assertion right)
                => new Assertions(left.Union(new[] { right }));
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

