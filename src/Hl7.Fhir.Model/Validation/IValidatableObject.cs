using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Validation
{
    public interface IValidatableObject
    {
        IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
