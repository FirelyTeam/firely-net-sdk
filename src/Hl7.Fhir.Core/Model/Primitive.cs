using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Model
{
    public abstract class Primitive : Element
    {
        [NotMapped]
        public object ObjectValue { get; set; }

        public override string ToString()
        {
            return PrimitiveTypeConverter.ConvertTo<string>(this.ObjectValue);
        }
    }

    [InvokeIValidatableObject]
    public abstract class Primitive<T> : Primitive
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }     
    }

}
