using Hl7.Fhir.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Hl7.Fhir.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class AllowedTypesAttribute : ValidationAttribute
    {
        [CLSCompliant(false)]
        public AllowedTypesAttribute(params Type[] types)
        {
            Types = types;
        }

        public Type[] Types { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var list = value as IEnumerable;
            ValidationResult result = ValidationResult.Success;

            if (list != null)
            {
                foreach (var item in list)
                {
                    result = validateValue(item);
                    if (result != ValidationResult.Success) break;
                }
            }
            else
            {
                result = validateValue(value);
            }

            return result;
        }

        private ValidationResult validateValue(object item)
        {
            if (item != null)
            {
#if PORTABLE45
				if (!Types.Any(type => type.GetTypeInfo().IsAssignableFrom(item.GetType().GetTypeInfo())))
					return new ValidationResult(String.Format("Value is of type {0}, which is not an allowed choice", item.GetType()));
#else
                if (!Types.Any(type => type.IsAssignableFrom(item.GetType())))
                    return new ValidationResult(String.Format("Value is of type {0}, which is not an allowed choice", item.GetType()));
#endif
			}

            return ValidationResult.Success;
        }

    }
}
