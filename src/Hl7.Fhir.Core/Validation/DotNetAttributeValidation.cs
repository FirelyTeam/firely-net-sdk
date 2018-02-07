/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Validation
{
    public class DotNetAttributeValidation
    {
        public static ValidationContext BuildContext(object value=null)
        {
#if NET40
            return new ValidationContext(value, null, null);
#else
            return new ValidationContext(value);
#endif
        }

        public static void Validate(object value, bool recurse = false, Func<string, Resource> resolver = null)
        {
            if (value == null) throw new ArgumentNullException("value");
            //    assertSupportedInstanceType(value);

            var validationContext = BuildContext(value);
            validationContext.SetValidateRecursively(recurse);
            validationContext.SetResolver(resolver);

            Validator.ValidateObject(value, validationContext, true);
        }

        public static bool TryValidate(object value, ICollection<ValidationResult> validationResults = null, bool recurse = false, Func<string,Resource> resolver=null)
        {
            if (value == null) throw new ArgumentNullException("value");
          // assertSupportedInstanceType(value);

            var results = validationResults ?? new List<ValidationResult>();
            var validationContext = BuildContext(value);
            validationContext.SetValidateRecursively(recurse);
            validationContext.SetResolver(resolver);
            return Validator.TryValidateObject(value, validationContext, results, true);

            // Note, if you pass a null validationResults, you will *not* get results (it's not an out param!)
        }
     

        internal static ValidationResult BuildResult(ValidationContext context, string message, params object[] messageArgs)
        {
            var resultMessage = String.Format(message, messageArgs);

            if(context != null && context.MemberName != null)
                return new ValidationResult(resultMessage, new string[] { context.MemberName });
            else
                return new ValidationResult(resultMessage);
        }

        //internal static ValidationResult BuildResult(ValidationContext context, string message)
        //{
        //    return BuildResult(context, message, null);
        //}

        //public static void Validate(Resource resource, bool recurse = false)
        //{
        //    if (resource == null) throw new ArgumentNullException("resource");
        //    Validator.ValidateObject(resource, ValidationContextFactory.Create(resource, null, recurse), true);
        //}

        //public static bool TryValidate(Resource resource, ICollection<ValidationResult> validationResults=null, bool recurse = false)
        //{
        //    if(resource == null) throw new ArgumentNullException("resource");

        //    var results = validationResults ?? new List<ValidationResult>();
        //    return Validator.TryValidateObject(resource, ValidationContextFactory.Create(resource, null, recurse), results, true);
        //}

        //public static void Validate(Element element, bool recurse = false)
        //{
        //    if (element == null) throw new ArgumentNullException("element");
        //    Validator.ValidateObject(element, ValidationContextFactory.Create(element, null, recurse), true);
        //}

        //public static bool TryValidate(Element element, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        //{
        //    if (element == null) throw new ArgumentNullException("element");

        //    var results = validationResults ?? new List<ValidationResult>();
        //    return Validator.TryValidateObject(element, ValidationContextFactory.Create(element, null, recurse), results, true);
        //}

        //public static void Validate(ResourceEntry entry, bool recurse = false)
        //{
        //    if (entry == null) throw new ArgumentNullException("entry");
        //    Validator.ValidateObject(entry, ValidationContextFactory.Create(entry, null, recurse), true);
        //}

        //public static bool TryValidate(ResourceEntry entry, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        //{
        //    if (entry == null) throw new ArgumentNullException("entry");

        //    var results = validationResults ?? new List<ValidationResult>();
        //    return Validator.TryValidateObject(entry, ValidationContextFactory.Create(entry, null, recurse), results, true);
        //}

        //public static void Validate(Bundle bundle, bool recurse = false)
        //{
        //    if (bundle == null) throw new ArgumentNullException("bundle");
        //    Validator.ValidateObject(bundle, ValidationContextFactory.Create(bundle, null, recurse), true);
        //}

        //public static bool TryValidate(Bundle bundle, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        //{
        //    if (bundle == null) throw new ArgumentNullException("bundle");

        //    var results = validationResults ?? new List<ValidationResult>();
        //    return Validator.TryValidateObject(bundle, ValidationContextFactory.Create(bundle, null, recurse), results, true);
        //}
    }

}
