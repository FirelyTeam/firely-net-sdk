/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Add support for validating against Base subclasses (instead of IElementNavigator) to the Validator
    /// </summary>
    public static class PocoValidationExtensions
    {
        public static OperationOutcome Validate(this Validator me, string definitionUri, Base instance)
        {
            return me.Validate(definitionUri, new PocoNavigator(instance));
        }

        public static OperationOutcome Validate(this Validator me, StructureDefinition structureDefinition, Base instance)
        {
            return me.Validate(structureDefinition, new PocoNavigator(instance));
        }

        public static OperationOutcome Validate(this Validator me, IEnumerable<string> definitionUris, Base instance, BatchValidationMode mode)
        {
            return me.Validate(definitionUris, new PocoNavigator(instance), mode);
        }

        public static OperationOutcome Validate(this Validator me, IEnumerable<StructureDefinition> structureDefinitions, Base instance, BatchValidationMode mode)
        {
            return me.Validate(structureDefinitions, new PocoNavigator(instance), mode);
        }
    }
}
