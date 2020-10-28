/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Collections.Generic;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Validation
{
    // It's important to realize that the notion of "closed" and "open" for slicing groups is defined with
    // regards to the constraints of the slicing intro. Instances that do not match the slicing intro's constraints
    // are considered not to be part of that group at all and will not be part of the actual slicing within that
    // slicing group. This is evident when considering re-slicing: before re-slicing the slice had a set of constraints
    // that would -in concert with the discriminator- determine whether an instance was part of that slice. If these are
    // thrown away or disregarded when re-slicing, the re-sliced slice would effectively become a black hole for all instances,
    // and all instances would either be part of its "open" slot or erroneous (when closed). None
    // of the other sibling slices of the now re-sliced group would be considered as candidates for that instance anymore.
    // Another way to put it: the "discriminating" criteria for the slice may not be altered by the fact that it has been
    // resliced - this means the original constraints should be present on the re-sliced slice's intro. 
    //
    // This also implies that a discriminator-less slicing group with an "open" rule will swallow all and never produce errors.

    internal class SliceGroupBucket : IBucket
    {
        internal SliceGroupBucket(ElementDefinition.SlicingComponent slicing, IBucket entry, IEnumerable<IBucket> subs)                
        {
            if (slicing == null) throw Error.ArgumentNull(nameof(slicing));

            Entry = entry;
            ChildSlices = subs.ToArray();

            Ordered = slicing.Ordered ?? false;
            Rules = slicing.Rules ?? ElementDefinition.SlicingRules.Open;
        }

        public IBucket[] ChildSlices { get; private set; }
        public IBucket Entry { get; private set; }

        public bool Ordered { get; private set; }

        public ElementDefinition.SlicingRules Rules { get; private set; }

        public string Name => Entry.Name;

        public IList<ITypedElement> Members => Entry.Members;

        public bool Add(ITypedElement candidate)
        {
            return Entry.Add(candidate);
        }

        public OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            var outcome = Entry.Validate(validator, errorLocation);   // Validate against entry slice, e.g. total cardinality

            var overallLastMatchingSlice = -1;
            var openTailInUse = false;

            // Go over the elements in the instance, in order
            foreach(var candidate in Entry.Members)
            {
                int lastMatchingSlide = -1;

                // Try to find the child slice that this element matches
                for(var sliceNumber = 0; sliceNumber < ChildSlices.Length; sliceNumber++)
                {
                    var sliceName = ChildSlices[sliceNumber].Name;
                    var success = ChildSlices[sliceNumber].Add(candidate);

                    if (success)
                    {
                        // The instance matched a slice that we have already passed, if order matters, 
                        // this is not allowed
                        if (sliceNumber < overallLastMatchingSlice && Ordered)
                            validator.Trace(outcome, $"Element matches slice '{sliceName}', but this " +
                                $"is out of order for group '{Name}', since a previous element already matched slice '{ChildSlices[overallLastMatchingSlice].Name}'", 
                            Issue.CONTENT_ELEMENT_SLICING_OUT_OF_ORDER, candidate);
                        else 
                            overallLastMatchingSlice = sliceNumber;

                        if (openTailInUse)
                        {
                            // We found a match while we already added a non-match to a "open at end" slicegroup, that's not allowed
                            validator.Trace(outcome, $"Element matched slice '{sliceName}', but it appears after a non-match, which is not allowed for an open-at-end group",
                                        Issue.CONTENT_ELEMENT_FAILS_SLICING_RULE, candidate);
                        }

                        if (lastMatchingSlide != -1)   // oops, already matched another slice
                        {
                            validator.Trace(outcome, $"Element matches both slice '{ChildSlices[lastMatchingSlide].Name}' " +
                                $"and '{ChildSlices[sliceNumber].Name}', which signals a problem with the slice definition. This may result in spurious validation messages for this element.",
                                Issue.PROFILE_INSTANCE_MATCHES_MULTIPLE_SLICES, candidate);
                            break;
                        }

                        lastMatchingSlide = sliceNumber;
                    }
                }

                // So we found no slice that can take this candidate, let's take a look at the rules
                if (lastMatchingSlide == -1)
                {
                    if (Rules == ElementDefinition.SlicingRules.Open)
                        validator.Trace(outcome, $"Element was determined to be in the open slice for group '{Name}'", Issue.PROCESSING_PROGRESS, candidate);
                    else if (Rules == ElementDefinition.SlicingRules.OpenAtEnd)
                        openTailInUse = true;
                    else
                    {
                        // Sorry, won't fly
                        validator.Trace(outcome, $"Element does not match any slice, but the group at '{Name}' is closed.",
                                Issue.CONTENT_ELEMENT_FAILS_SLICING_RULE, candidate);
                    }
                }
            }

            // Finally, add any validation items on the elements that made it into the child slices
            foreach (var slice in ChildSlices)
            {
                var sliceOutcome = slice.Validate(validator, errorLocation);                  
                sliceOutcome.Issue.ForEach( i =>outcome.AddIssue(i));                
            }

            return outcome;
        }    
    }

}