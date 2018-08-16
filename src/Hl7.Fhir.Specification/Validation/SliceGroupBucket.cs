/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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
    internal static class BucketFactory
    {
        public static IBucket CreateRoot(ElementDefinitionNavigator root, Validator validator)
        {
            // Create a single bucket
            var entryBucket = new ElementBucket(root, validator);

            if (root.Current.Slicing == null)
                return entryBucket;
            else
                return CreateGroup(root, validator, entryBucket, atRoot: true);
        }

        public static IBucket CreateGroup(ElementDefinitionNavigator root, Validator validator, IBucket entryBucket, bool atRoot)
        {
            var childDiscriminators = root.Current.Slicing.Discriminator.ToArray();
            var slices = root.FindMemberSlices(atRoot);
            var bm = root.Bookmark();
            var subs = new List<IBucket>();

            foreach (var slice in slices)
            {
                root.ReturnToBookmark(slice);

                var subBucket = new SliceBucket(root, validator, childDiscriminators);

                if (root.Current.Slicing == null)
                    subs.Add(subBucket);
                else
                    subs.Add(CreateGroup(root, validator, subBucket, atRoot: false));
            }

            root.ReturnToBookmark(bm);

            return new SliceGroupBucket(root.Current.Slicing, entryBucket, subs);
        }

    }


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
        internal SliceGroupBucket(ElementDefinition.SlicingComponent slicing, IBucket main, List<IBucket> subs)                
        {
            if (slicing == null) throw Error.ArgumentNull(nameof(slicing));

            Entry = main;
            ChildSlices = subs;

            Ordered = slicing.Ordered ?? false;
            Rules = slicing.Rules ?? ElementDefinition.SlicingRules.Open;
        }

        public List<IBucket> ChildSlices { get; private set; }
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
            var outcome = Entry.Validate(validator, errorLocation);   // Validate against entry slice, e.g. cardinality

            var lastMatchingSlice = -1;
            var openTailInUse = false;

            // Go over the elements in the instance, in order
            foreach(var candidate in Entry.Members)
            {
                bool hasSucceeded = false;

                // Try to find the child slice that this element matches
                for(var sliceNumber = 0; sliceNumber < ChildSlices.Count; sliceNumber++)
                {
                    var sliceName = ChildSlices[sliceNumber].Name;
                    var success = ChildSlices[sliceNumber].Add(candidate);

                    if (success)
                    {
                        // The instance matched a slice that we have already passed, if order matters, 
                        // this is not allowed
                        if (sliceNumber < lastMatchingSlice && Ordered)
                            validator.Trace(outcome, $"Element matches slice '{sliceName}', but this " +
                                $"is out of order for group '{Name}', since a previous element already matched slice '{ChildSlices[lastMatchingSlice].Name}'", 
                            Issue.CONTENT_ELEMENT_SLICING_OUT_OF_ORDER, candidate);
                        else 
                            lastMatchingSlice = sliceNumber;

                        if (openTailInUse)
                        {
                            // We found a match while we already added a non-match to a "open at end" slicegroup, that's not allowed
                            validator.Trace(outcome, $"Element matched slice '{sliceName}', but it appears after a non-match, which is not allowed for an open-at-end group",
                                        Issue.CONTENT_ELEMENT_FAILS_SLICING_RULE, candidate);
                        }

                        hasSucceeded = true;
                    }
                }

                // So we found no slice that can take this candidate, let's take a look at the rules
                if (!hasSucceeded)
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
                foreach (var issue in sliceOutcome.Issue)
                {
                    // Only add the issue from the slice outcome if the entry validation did not already catch
                    // the same issue, otherwise the users will see it twice.
                    if(!outcome.Issue.Exists(i => i.Location.First() == issue.Location.First() && i.Details.Text == issue.Details.Text))
                        outcome.AddIssue(issue);
                }
            }

            return outcome;
        }    
    }

}