/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class BindingDiscriminator : PathBasedDiscriminator
    {
        public BindingDiscriminator(ElementDefinition.ElementDefinitionBindingComponent binding, string path, string errorLocation, Validator validator) : base(path)
        {
            Validator = validator;
            ErrorLocation = errorLocation;
            Binding = binding;
        }

        public readonly string ErrorLocation;
        public readonly Validator Validator;
        public readonly ElementDefinition.ElementDefinitionBindingComponent Binding;

        protected override bool MatchInternal(ITypedElement instance)
        {
            var result = Validator.ValidateBinding(Binding, instance, ErrorLocation);
            return result.Success;
        }
    }
}
