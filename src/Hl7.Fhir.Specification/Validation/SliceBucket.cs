/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification.Validation;
using System;

namespace Hl7.Fhir.Validation
{

    internal class SliceBucket : BaseBucket
    {
        public SliceBucket(ElementDefinitionNavigator sliceConstraints, Validator validator, string[] discriminators) : base(sliceConstraints.Current)
        {
            //TODO: discriminator-less validation?
            if (discriminators == null) throw Error.ArgumentNull(nameof(discriminators));
            if(discriminators.Length == 0)
                throw Error.NotImplemented($"Discriminator-less slicing is not implemented. Must pass at least one discriminator - none found at '{sliceConstraints.QualifiedDefinitionPath()}'");

            SliceConstraints = sliceConstraints.ShallowCopy();
            Validator = validator;

            //TODO: the resourceresolver should really include a snapshot-generator resolver to make this safe.
            Discriminators = discriminators.Select(d => new ValueDiscriminator(d, SliceConstraints, validator)).ToArray();
        }

        public ElementDefinitionNavigator SliceConstraints { get; private set; }

        public Validator Validator { get; private set; }

        public ValueDiscriminator[] Discriminators { get; private set; }

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
