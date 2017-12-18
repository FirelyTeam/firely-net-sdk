using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Schema
{
    class SliceAssertion : Assertion, IMultipleElementAssertion, IAssertionContainer
    {
        bool Ordered;

        List<ConditionalAssertion> Slices;

        public IEnumerable<Schema> Subschemas()
        {
            throw new NotImplementedException();
        }

        public SchemaAnnotations Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            throw new NotImplementedException();
        }

        // default?

    }

}
