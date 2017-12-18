using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Schema
{
    public class ConditionalAssertion : Assertion, IAssertionContainer, ISingleElementAssertion
    {
        Schema Condition;

        Schema Assertion;

        public IEnumerable<Schema> Subschemas() => new[] { Assertion };

        public ResultAnnotation IsMember(IElementNavigator input, ValidationContext vc) 
            => Condition.Validate(input, vc).Result;

        public SchemaAnnotations Validate(IElementNavigator input, ValidationContext vc)
        {
            if(IsMember(input,vc).Result == ValidationResult.Success)
                return Assertion.Validate(input, vc);

            // yeah, return...ehm....
            throw new NotImplementedException();
        }
    }

}
