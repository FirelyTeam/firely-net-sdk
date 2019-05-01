/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class CardinalityAttribute : ValidationAttribute
    {
        public CardinalityAttribute()
        {
            Min = 0;
            Max = 1;
        }

        public int Min { get; set; }
        public int Max { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return (Min == 0) ? ValidationResult.Success :
                    DotNetAttributeValidation.BuildResult(validationContext, "Element with min. cardinality {0} cannot be null", Min);

            var count = 1;

            if (value is IList && !ReflectionHelper.IsArray(value))
            {
                var list = value as IList;
                foreach(var elem in list)
                   if(elem == null) return DotNetAttributeValidation.BuildResult(validationContext,"Repeating element cannot have empty/null values");
                count = list.Count;
            }

            if (count < Min) return DotNetAttributeValidation.BuildResult(validationContext,"Element has {0} elements, but min. cardinality is {1}", count, Min);

            if (Max != -1 && count > Max) return DotNetAttributeValidation.BuildResult(validationContext,"Element has {0} elements, but max. cardinality is {1}", count, Max);

            return ValidationResult.Success;
        }
    }
}
