using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Validation.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Schema
{
    internal class ValidatorBuilder
    {
        private readonly List<IValidatable> _validators = new List<IValidatable>();
        private readonly ValidationContext _validationContext;

        public ValidatorBuilder(ValidationContext vc)
        {
            _validationContext = vc;
        }

        public OperationOutcome Validate(ITypedElement input)
        {
            var outcome = new OperationOutcome();
            try
            {
                //_validators.ForEach(v => outcome.Add(v.Validate(input, _validationContext)));
            }
            catch (IncorrectElementDefinitionException iede)
            {
                outcome.AddIssue("Incorrect ElementDefinition: " + iede.Message, Issue.PROFILE_ELEMENTDEF_INCORRECT);
            }
            
            return outcome;
        }


        public void Add(IValidatable validator)
        {
            _validators.Add(validator);
        }
    }
}
