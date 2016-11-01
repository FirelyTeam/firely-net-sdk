/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.ElementModel;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Linq;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Validation
{

    internal class SliceGroupBucket : SliceBucket
    {

        public SliceGroupBucket(Validator validator, ElementDefinitionNavigator sliceIntro, string[] discriminators)
                :base(validator, sliceIntro, discriminators)
        {
            if (sliceIntro.Current.Slicing == null)
                throw Error.Argument(nameof(sliceIntro), "Argument should point to an ElementDefinition with a slicing component");
        }

        protected ElementDefinitionNavigator Intro => Constraints;

        internal void RebuildChildList()
        {
            var childDiscriminators = Intro.Current.Slicing.Discriminator.ToArray();
            var slices = Constraints.FindMemberSlices().ToList();
            var bm = Intro.Bookmark();

            ChildSlices = new List<IBucket>();
            foreach(var slice in slices)
            {
                Intro.ReturnToBookmark(bm);

                if (Intro.Current.Slicing == null)
                    ChildSlices.Add(new SliceBucket(Validator, Intro, childDiscriminators));
                else
                    ChildSlices.Add(new SliceGroupBucket(Validator, Intro, childDiscriminators));
            }

            Intro.ReturnToBookmark(bm);
        }


        public OperationOutcome Judge(IEnumerable<IElementNavigator> candidates)
        {
            var sliceCandidates = candidates.Select(c => new SliceCandidate { Instance = c, Membership = SliceMembership.NotMember, Outcome = null })
                                    .ToList();

            return Judge(sliceCandidates);
        }


        public override OperationOutcome Judge(IEnumerable<SliceCandidate> candidates)
        {
            var outcome = new OperationOutcome();
            RebuildChildList();

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

            // All this being said, let's now first run the constraints in our intro slice, to select our "group" of instances
            // to work with

            var outcomeX = base.Judge(candidates);

            var memberInstances = candidates.Where(c => c.Membership == SliceMembership.Member).ToList();

            // First, wipe any existing membership info - we're redoing it
            // In reality, we are doing a "nested" match, but copying all the candidacy info is a 
            // waste of cycles, we will re-instate all membership data after calling judgeChildSlices().
            foreach (var memberInstance in memberInstances) memberInstance.Membership = SliceMembership.NotMember;
            judgeChildSlices(outcome, memberInstances);
            foreach (var memberInstance in memberInstances) memberInstance.Membership = SliceMembership.Member;

            return outcome;
        }

        private void judgeChildSlices(OperationOutcome outcome, IList<SliceCandidate> candidates)
        {
            var leftToJudge = candidates;


            foreach(var slice in ChildSlices)
            {
                slice.Judge(leftToJudge);

                // check if the results are in order, i.e. contiguous within leftToJudge
                // check if the # of results matches the cardinality of the slice

                leftToJudge = leftToJudge.Where(c => c.Membership == SliceMembership.NotMember).ToList();
            }

            var open = candidates.Where(c => c.Membership == SliceMembership.NotMember);
            var numOpen = open.Count();
            var rules = Intro.Current.Slicing.Rules;
            var sliceName = $"{Intro.Current.Path} ({Intro.Current.Name ?? "no name"})"; 
            if (numOpen > 0)
            {
                if(rules == ElementDefinition.SlicingRules.Closed)
                {
                    Validator.Trace(outcome, $"Slice '{sliceName}' has {numOpen} non-matches, which are not allowed in a closed slicing group",
                            Issue.CONTENT_ELEMENT_FAILS_SLICING_RULE, open.First().Instance);
                }
                if(rules == ElementDefinition.SlicingRules.Open)
                {
                    Validator.Trace(outcome, $"Slice '{sliceName}' resulted in {numOpen} candidates for the open slice", Issue.PROCESSING_PROGRESS,
                         open.First().Instance);
                }
                else if(rules == ElementDefinition.SlicingRules.OpenAtEnd)
                {
                    // All non matches instances must be at "the end", i.e. goto the first item that did
                    // not match, and check that all following items don't match either
                    var openTail = candidates.SkipWhile(c => c.Membership == SliceMembership.Member);
                    if(openTail.Any(c => c.Membership == SliceMembership.Member))
                    {
                        Validator.Trace(outcome, $"Slice '{sliceName}' resulted in {numOpen} candidates for the open slice, but they are not all at the end",
                                    Issue.CONTENT_ELEMENT_FAILS_SLICING_RULE, openTail.First().Instance);
                    }
                }
            }
        }

        public List<IBucket> ChildSlices { get; private set; }
    }

}