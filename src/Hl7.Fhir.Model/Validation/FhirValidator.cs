using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Validation
{
    public class FhirValidator
    {
        public static void Validate(object resource, bool recurse = false)
        {
            if (resource == null) throw new ArgumentNullException("resource");
            Validator.ValidateObject(resource, ValidationContextFactory.Create(resource, null, recurse), true);
        }

        public static bool TryValidate(object resource, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        {
            if (resource == null) throw new ArgumentNullException("resource");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(resource, ValidationContextFactory.Create(resource, null, recurse), results, true);
        }


        public static void Validate(Resource resource, bool recurse = false)
        {
            if (resource == null) throw new ArgumentNullException("resource");
            Validator.ValidateObject(resource, ValidationContextFactory.Create(resource, null, recurse), true);
        }

        public static bool TryValidate(Resource resource, ICollection<ValidationResult> validationResults=null, bool recurse = false)
        {
            if(resource == null) throw new ArgumentNullException("resource");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(resource, ValidationContextFactory.Create(resource, null, recurse), results, true);
        }

        public static void Validate(Element element, bool recurse = false)
        {
            if (element == null) throw new ArgumentNullException("element");
            Validator.ValidateObject(element, ValidationContextFactory.Create(element, null, recurse), true);
        }

        public static bool TryValidate(Element element, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        {
            if (element == null) throw new ArgumentNullException("element");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(element, ValidationContextFactory.Create(element, null, recurse), results, true);
        }

        public static void Validate(ResourceEntry entry, bool recurse = false)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            Validator.ValidateObject(entry, ValidationContextFactory.Create(entry, null, recurse), true);
        }

        public static bool TryValidate(ResourceEntry entry, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(entry, ValidationContextFactory.Create(entry, null, recurse), results, true);
        }

        public static void Validate(Bundle bundle, bool recurse = false)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");
            Validator.ValidateObject(bundle, ValidationContextFactory.Create(bundle, null, recurse), true);
        }

        public static bool TryValidate(Bundle bundle, ICollection<ValidationResult> validationResults = null, bool recurse = false)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(bundle, ValidationContextFactory.Create(bundle, null, recurse), results, true);
        }
    }

}
