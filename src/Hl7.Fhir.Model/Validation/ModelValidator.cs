using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Validation
{
    public class ModelValidator
    {
        public static void Validate(Resource resource)
        {
            if (resource == null) throw new ArgumentNullException("resource");
            Validator.ValidateObject(resource, ValidationContextFactory.Create(resource, null), true);
        }

        public static bool TryValidate(Resource resource, ICollection<ValidationResult> validationResults=null)
        {
            if(resource == null) throw new ArgumentNullException("resource");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(resource, ValidationContextFactory.Create(resource, null), results, true);
        }

        public static void Validate(Element element)
        {
            if (element == null) throw new ArgumentNullException("element");
            Validator.ValidateObject(element, ValidationContextFactory.Create(element, null), true);
        }

        public static bool TryValidate(Element element, ICollection<ValidationResult> validationResults = null)
        {
            if (element == null) throw new ArgumentNullException("element");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(element, ValidationContextFactory.Create(element, null), results, true);
        }

        public static void Validate(ResourceEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            Validator.ValidateObject(entry, ValidationContextFactory.Create(entry, null), true);
        }

        public static bool TryValidate(ResourceEntry entry, ICollection<ValidationResult> validationResults = null)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(entry, ValidationContextFactory.Create(entry, null), results, true);
        }

        public static void Validate(Bundle bundle)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");
            Validator.ValidateObject(bundle, ValidationContextFactory.Create(bundle, null), true);
        }

        public static bool TryValidate(Bundle bundle, ICollection<ValidationResult> validationResults = null)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");

            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(bundle, ValidationContextFactory.Create(bundle, null), results, true);
        }
    }

}
