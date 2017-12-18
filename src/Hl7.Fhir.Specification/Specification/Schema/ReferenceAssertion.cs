using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Schema
{
    public class ReferenceAssertion : Assertion, ISingleElementAssertion, IAssertionContainer
    {
        // A symbolic reference, or a reference to another Schema?
        // Or one of the next two?

        public Schema DirectReference;

        public Func<Schema> Dereference;

        private Schema ReferencedSchema => DirectReference ?? Dereference();

        public IEnumerable<Schema> Subschemas() => ReferencedSchema.Subschemas();

        public SchemaAnnotations Validate(IElementNavigator input, ValidationContext vc)
            => ReferencedSchema.Validate(new[] { input }, vc);
    }

}
