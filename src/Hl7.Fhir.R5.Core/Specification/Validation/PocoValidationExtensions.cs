/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Add support for validating against Base subclasses (instead of ITypedElement) to the Validator
    /// </summary>
    public static class PocoValidationExtensions
    {
        public static OperationOutcome Validate(this Validator me, Base instance)
        {
            return me.Validate(instance.ToTypedElement(ModelInfo.ModelInspector), ModelInfo.ModelInspector);
        }

        public static OperationOutcome Validate(this Validator me, Base instance, params string[] definitionUri)
        {
            return me.Validate(instance.ToTypedElement(ModelInfo.ModelInspector), ModelInfo.ModelInspector, definitionUri);
        }

        public static OperationOutcome Validate(this Validator me, Base instance, StructureDefinition structureDefinition)
        {
            return me.Validate(instance.ToTypedElement(ModelInfo.ModelInspector), ModelInfo.ModelInspector, structureDefinition);
        }

        public static OperationOutcome Validate(this Validator me, Base instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            return me.Validate(instance.ToTypedElement(ModelInfo.ModelInspector), ModelInfo.ModelInspector, structureDefinitions);
        }
    }
}
#nullable restore