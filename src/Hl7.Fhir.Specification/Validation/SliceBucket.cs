/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation
{
    internal class SliceBucket : IBucket
    {
        protected ElementDefinitionNavigator Constraints;
        protected string[] Discriminators;
        protected Validator Validator;

        public SliceBucket(Validator validator, ElementDefinitionNavigator sliceConstraints, string[] discriminators=null)
        {
            Constraints = sliceConstraints.ShallowCopy();
            Discriminators = discriminators;
            Validator = validator;
        }

        public virtual OperationOutcome Judge(IEnumerable<SliceCandidate> candidates)
        {
            foreach(var candidate in candidates)
            {
                var report = Validator.Validate(candidate.Instance, Constraints);

                // For now, only do discriminator-less matching, which means that
                // if validation succeeds, the instance belongs to the slice, otherwise it does not
                if (report.Success)
                {
                    candidate.Membership = SliceMembership.Member;
                    candidate.Outcome = report;
                }
            }

            return new OperationOutcome();      // details will be collected by grouping
        }
    }



    /// <summary>
    /// The result of trying to match an instance with the constraints of the slice
    /// </summary>
    /// <remarks>For discriminator-based slicing, an instance is a member of a slice when it matches the
    /// constraints pointed to by the discriminator. Any member may still fail validation for the rest of the constraints.
    /// In discriminator-less slicing, a slice is a member only when the outcome of the validation is successful.
    /// </remarks>
    internal class SliceCandidate
    {
        public IElementNavigator Instance;
        public SliceMembership Membership;
        public OperationOutcome Outcome;
    }

    /// <summary>
    /// Whether an instance was determined to be a member of the slice.
    /// </summary>
    /// <remarks>For discriminator-based slicing, membership is based on whether the instance matches the
    /// discriminator. For discriminator-less slicing, membership is determined on whether the instance matches
    /// all the constraints that define the slice</remarks>
    internal enum SliceMembership
    {
        NotMember,
        Member
    }
}
