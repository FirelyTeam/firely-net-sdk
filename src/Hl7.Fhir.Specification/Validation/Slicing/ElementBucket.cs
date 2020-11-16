/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
    internal class ElementBucket : BaseBucket
    {
        public ElementBucket(ElementDefinitionNavigator root, Validator validator) : base(root.Current)
        {
            Root = root.ShallowCopy();
            Validator = validator;
        }

        public ElementDefinitionNavigator Root { get; private set; }

        public Validator Validator { get; private set; }

        public override bool Add(ITypedElement instance)
        {
            Members.Add(instance);
            return true;
        }


        public override OperationOutcome Validate(Validator validator, ITypedElement errorLocation)
        {
            var outcome = base.Validate(validator, errorLocation);

            foreach(var member in Members)
            {
                outcome.Include(Validator.Validate(member, Root));
            }

            return outcome;
        }
    }
}
