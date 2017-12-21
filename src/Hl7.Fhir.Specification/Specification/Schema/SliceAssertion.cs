using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;

namespace Hl7.Fhir.Specification.Schema
{
    public class SliceAssertion : Assertion, IGroupAssertion
    {
        public readonly bool Ordered;
        public readonly Assertion Default; 
        public readonly IEnumerable<ConditionalAssertion> Slices;

        public SliceAssertion(bool ordered, Schema @default, params ConditionalAssertion[] slices) : this(ordered, @default, slices.AsEnumerable())
        {
        }

        public SliceAssertion(bool ordered, params ConditionalAssertion[] slices) : this(ordered, slices.AsEnumerable())
        {
        }

        public SliceAssertion(bool ordered, IEnumerable<ConditionalAssertion> slices)
            :this(ordered, null, slices)
        {
        }

        public SliceAssertion(bool ordered, Schema @default, IEnumerable<ConditionalAssertion> slices)
        {
            Ordered = ordered;
            Default = @default ?? Assertion.Fail
                            .OnFailure(new TraceTag("Instance did match any slice, and there is no default"));
            Slices = slices ?? throw new ArgumentNullException(nameof(slices));
        }

        public override IEnumerable<SchemaTags> CollectTags()
            => Slices.Aggregate(Default.CollectTags(), (sum, slice) => sum.Union(slice.CollectTags()));

        public SchemaTags Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            // Something we copy from the current validator, validating against slices, activating default, checking order
            throw new NotImplementedException();
        }
    }

}
