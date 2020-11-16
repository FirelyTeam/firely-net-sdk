/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Validation
{
    internal class ValueDiscriminator : PathBasedDiscriminator
    {
        public ValueDiscriminator(Element fixedValue, string path, Validator validator) : base(path)
        {
            Validator = validator;
            FixedValue = fixedValue ?? throw new System.ArgumentNullException(nameof(fixedValue));
        }

        public readonly Validator Validator;
        public readonly Element FixedValue;

        protected override bool MatchInternal(ITypedElement instance)
        {
            var result = Validator.ValidateFixed(FixedValue, instance);
            return result.Success;
        }
    }
}
