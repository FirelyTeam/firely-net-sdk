/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Validation
{
    internal class ConstraintsBucket : BaseBucket
    {
        /// <summary>
        /// Represents a "bucket" that triages instances based on whether they match a slice-specific set of constraints. This is used
        /// for "discriminator-less" validation.
        /// </summary>
        /// <param name="sliceConstraints">The set of constraints that are the criterium for membership of the slice.</param>
        /// <param name="validator">A validator instance that will be invoked to validate the child constraints.</param>
        public ConstraintsBucket(ElementDefinitionNavigator sliceConstraints, Validator validator) : base(sliceConstraints.Current)
        {
            // Keep a copy of the constraints for this slice, so we can use them to validate the instances against later.
            SliceConstraints = sliceConstraints.ShallowCopy();

            Validator = validator;
        }
    
        public ElementDefinitionNavigator SliceConstraints { get; private set; }

        public Validator Validator { get; private set; }

        public override bool Add(ITypedElement candidate)
        {
            OperationOutcome outcome = Validator.Validate(candidate, SliceConstraints);

            if (outcome.Success)
            {
                Members.Add(candidate);
                return true;
            }
            else
                return false;
        }

        public override OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            return base.Validate(validator, errorLocation);

            // Maybe add the warnings coming out of the Add() method?
        }
    }
}
