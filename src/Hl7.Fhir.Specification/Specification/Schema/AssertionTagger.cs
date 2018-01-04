using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Schema
{
    public class AssertionTagger : Assertion, IMemberAssertion
    {
        public readonly SchemaTags Tags;
        public readonly Assertion Taggee;
        public readonly ValidationResult Result;

        public AssertionTagger(Assertion taggee, ValidationResult result, SchemaTags tags)
        {
            Taggee = taggee ?? throw new ArgumentNullException(nameof(taggee));
            Result = result;
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }

        public override IEnumerable<SchemaTags> CollectTags()
         => (Result == ValidationResult.Success) ? Tags.Collection : new SchemaTags().Collection;

        public SchemaTags Validate(IElementNavigator input, ValidationContext vc)
        {
            var result = Taggee.Validate(input, vc);

            if (result.Result.Result == Result)
                return result + Tags;
            else
                return result;
        }
    }


    public class SuccessTags : AssertionTagger
    {
        public SuccessTags(Assertion taggee, ValidationResult result, SchemaTags tags) : base(taggee, ValidationResult.Success, tags)
        {
        }
    }


    public class FailureTags : AssertionTagger
    {
        public FailureTags(Assertion taggee, ValidationResult result, SchemaTags tags) : base(taggee, ValidationResult.Failure, tags)
        {
        }
    }

    public class UndecidedTags : AssertionTagger
    {
        public UndecidedTags(Assertion taggee, ValidationResult result, SchemaTags tags) : base(taggee, ValidationResult.Undecided, tags)
        {
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

