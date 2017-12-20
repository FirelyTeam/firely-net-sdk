using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;

namespace Hl7.Fhir.Specification.Schema
{
    public class ReferenceAssertion : Assertion, IGroupAssertion, ITagSource
    {
        // A symbolic reference, or a reference to another Schema?
        // Or one of the next two?

        public Schema DirectReference;

        public Func<Schema> Dereference;

        private Schema ReferencedSchema => DirectReference ?? Dereference();

        public IEnumerable<SchemaTags> CollectTags() => ReferencedSchema.CollectTags();

        public SchemaTags Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
            => ReferencedSchema.Validate(input, vc);
    }

}
