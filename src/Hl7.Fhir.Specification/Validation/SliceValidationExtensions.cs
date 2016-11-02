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

            var bucket = new SliceGroupBucket(validator, intro, discriminators: null);
            var sliceCandidates = instanceElements.Select(c => new SliceCandidate { Instance = c, Membership = SliceMembership.NotMember, Outcome = null })
                        .ToList();

            outcome.Add(bucket.Judge(sliceCandidates));

            // If any of our instances did not make it into the slice, this is actually an error
            // since in a non-resliced slice group (=the original slice) ALL instances should be part
            // of the slice. This is another way of saying: all instances should at least validate against
            // the original constraints of the element before slicing (i.e. a "normal" element).
            var failedCandidates = sliceCandidates.Where(c => c.Membership == SliceMembership.NotMember);
            if (failedCandidates.Any())
            {
                foreach (var failedCandidate in failedCandidates)
                    outcome.Include(failedCandidate.Outcome);
            }

            return outcome;
        }

    }
}
