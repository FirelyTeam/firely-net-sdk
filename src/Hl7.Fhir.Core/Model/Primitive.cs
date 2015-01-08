using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Model
{
    public abstract class Primitive : Element
    {
    }

    public abstract class Primitive<T> : Primitive
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }

        internal T _Value;
    }

}
