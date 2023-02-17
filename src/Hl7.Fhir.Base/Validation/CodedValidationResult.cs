/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable enable

namespace Hl7.Fhir.Validation
{
    public class CodedValidationResult : ValidationResult
    {
        public CodedValidationException ValidationException { get; set; }

        public CodedValidationResult(CodedValidationException validationException) : base(validationException.Message)
        {
            ValidationException = validationException;
        }

        public CodedValidationResult(CodedValidationException validationException, IEnumerable<string>? memberNames)
            : base(validationException.Message, memberNames)
        {
            ValidationException = validationException;
        }

        protected CodedValidationResult(CodedValidationResult validationResult) : base(validationResult)
        {
            ValidationException = validationResult.ValidationException;
        }
    }

}

#nullable restore
