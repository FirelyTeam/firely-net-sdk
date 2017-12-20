using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;

namespace Hl7.Fhir.Specification.Schema
{
    public class SliceAssertion : Assertion, IGroupAssertion, ITagSource
    {
        public readonly bool Ordered;
        public readonly Schema Default; 
        public readonly IEnumerable<ConditionalAssertion> Slices;

        public SliceAssertion(bool ordered, Schema @default, params ConditionalAssertion[] slices) : this(ordered, @default, slices.AsEnumerable())
        {
        }

        public SliceAssertion(bool ordered, params ConditionalAssertion[] slices) : this(ordered, slices.AsEnumerable())
        {
        }

        public SliceAssertion(bool ordered, IEnumerable<ConditionalAssertion> slices)
            :this(ordered, Schema.Empty, slices)
        {
        }

        public SliceAssertion(bool ordered, Schema @default, IEnumerable<ConditionalAssertion> slices)
        {
            Ordered = ordered;
            Default = @default ?? throw new ArgumentNullException(nameof(@default));
            Slices = slices ?? throw new ArgumentNullException(nameof(slices));
        }

        public IEnumerable<SchemaTags> CollectTags()
        {
            throw new NotImplementedException();
        }

        public SchemaTags Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            throw new NotImplementedException();
        }

        // default?

    }

}
