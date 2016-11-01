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
        public static OperationOutcome ValidateSlices(this Validator validator, List<IElementNavigator> instanceElements, ElementDefinitionNavigator intro)
        {
            var outcome = new OperationOutcome();

            var slices = intro.FindMemberSlices().ToList();

            validator.Trace(outcome, $"Encountered a slice group with {slices.Count} member slices at '{intro.Path}'. ", Issue.PROCESSING_PROGRESS, instanceElements.First());
            
            foreach (IElementNavigator element in instanceElements)
            {
                // All instances should at least conform to slice intro
                var introResult = validator.Validate(element, intro);
                outcome.Include(introResult);

                // Only go on validating member slices when the instance at least matches the intro's constraints
                if (introResult.Success)
                {
                    var slice = new ElementDefinitionNavigator(intro);

                    foreach (var bm in slices)
                    {
                        slice.ReturnToBookmark(bm);

                     //   var sliceValidationResults = ValidateSlices()
                    }
                }
            }

            // If this is a closed 

            return outcome;
        }

    }
}
