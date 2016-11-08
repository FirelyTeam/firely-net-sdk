using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal static class SliceValidationExtensions
    {
        public static OperationOutcome ValidateRootSliceGroup(this Validator validator, List<IElementNavigator> instanceElements, ElementDefinitionNavigator intro, IElementNavigator parent)
        {
            var outcome = new OperationOutcome();

            var bucket = BucketFactory.Create(intro, validator);

            foreach (var instance in instanceElements)
            {
                var matchOutcome = bucket.Add(instance);

                // For the "root" slice group (=the original core element that was sliced, not resliced)
                // any element that does not match is an error
                if (!matchOutcome.Success)
                {
                    outcome.Add(matchOutcome);
                }
            }

            return outcome;
        }

    }
}
