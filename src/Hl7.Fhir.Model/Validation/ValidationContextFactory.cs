/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Validation
{
    // This class creates ValidationContext objects, since due to bugs and platform
    // differences, creating this context is, unfortunately, version dependent.
    // See http://stackoverflow.com/questions/12762071/why-in-portable-library-classes-i-cant-instantiate-a-validationcontext-and-how
    public static class ValidationContextFactory
    {
        public static ValidationContext Create(object instance, IDictionary<object,object> items, bool recurse = false)
        {
            ValidationContext result = null;

            try
            {
                result = (ValidationContext)Activator.CreateInstance(typeof(ValidationContext),
                    new object[] { instance, null, items });

                result.SetValidateRecursively(recurse);
            }
            catch (InvalidOperationException)
            {
                throw new NotSupportedException("Validation is not supported on this platform");
            }
            return result;
        }
    }
}
