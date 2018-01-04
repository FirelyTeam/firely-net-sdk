using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;

namespace Hl7.Fhir.Specification.Schema
{
    public class ReferenceAssertion : Assertion, IGroupAssertion
    {
        // A symbolic reference, or a reference to another Schema?
        // Or one of the next two?

        public ElementSchema DirectReference;

        public Func<ElementSchema> Dereference;

        private ElementSchema ReferencedSchema => DirectReference ?? Dereference();

        public override IEnumerable<Assertions> CollectAssertions(Predicate<Assertion> pred)
            => ReferencedSchema.CollectAssertions(pred);

        // TODO: This will loop infinitely on Identifier (or any schema referring indirectly to itself)
        public override IEnumerable<SchemaTags> CollectTags() => ReferencedSchema.CollectTags();

        public SchemaTags Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
            => ReferencedSchema.Validate(input, vc);
    }

}
