/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class TypeDiscriminator : PathBasedDiscriminator
    {
        public TypeDiscriminator(string[] types, string path, Validator validator) : base(path)
        {
            Validator = validator;
            Types = types ?? throw new System.ArgumentNullException(nameof(types));
        }

        public readonly Validator Validator;
        public readonly string[] Types;

        // This discriminator matches if the type in the instance is *compatible* with ANY of the 
        // possible multiple types in the ElementDefinition.Type.Code (since Type repeats).
        protected override bool MatchInternal(ITypedElement instance) =>
            Types.Any(type => ModelInfo.IsInstanceTypeFor(type, instance.InstanceType));
    }
}
