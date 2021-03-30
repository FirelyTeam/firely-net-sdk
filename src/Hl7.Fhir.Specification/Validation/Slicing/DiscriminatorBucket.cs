/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class DiscriminatorBucket : BaseBucket
    {
        /// <summary>
        /// Represents a "bucket" that triages instances based on a discriminator and validates matching instances against a
        /// slice-specific set of constraints.
        /// </summary>
        /// <param name="sliceConstraints">The set of constraints that will be validated for members of the bucket.</param>
        /// <param name="validator">A validator instance that will be invoked to validate the child constraints.</param>
        /// <param name="discriminators">A set of discriminators that determine whether or not an instance is part of this bucket.</param>
        public DiscriminatorBucket(ElementDefinitionNavigator sliceConstraints, Validator validator, IDiscriminator[] discriminators) : base(sliceConstraints.Current)
        {
            if (discriminators == null || discriminators.Length == 0)
                throw Error.InvalidOperation($"Discriminator bucket requires at least one discriminator. Otherwise, use the ConstraintsBucket instead.");

            // Keep a copy of the constraints for this slice, so we can use them to validate the instances against later.
            SliceConstraints = sliceConstraints.ShallowCopy();

            Validator = validator;
            Discriminators = discriminators;
        }

        public ElementDefinitionNavigator SliceConstraints { get; private set; }

        public Validator Validator { get; private set; }

        public IDiscriminator[] Discriminators { get; private set; }

        public override bool Add(ITypedElement candidate)
        {
            if (Discriminators.All(d => d.Matches(candidate)))
            {
                Members.Add(candidate);
                return true;
            }
            else
                return false;
        }

        public override OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            OperationOutcome outcome = new OperationOutcome();

            // Simply validate all members and report errors
            foreach (var member in Members)
                outcome.Add(Validator.Validate(member, SliceConstraints));

            // include errors reported by our base as well
            outcome.Add(base.Validate(validator, errorLocation));

            return outcome;
        }
    }
}
