/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.FluentPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.FluentPath
{
    public static class PocoNavigatorExtensions
    {
        public static IEnumerable<Base> Select(this Base input, string expression, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            var results = inputNav.Select(expression, resourceNav);
            return results.Select(r => 
            {
                if (r == null)
                    return null;
            
                if (r is Hl7.Fhir.FluentPath.PocoNavigator && (r as Hl7.Fhir.FluentPath.PocoNavigator).FhirValue != null)
                {
                    return ((PocoNavigator)r).FhirValue;
                }
                object result;
                if (r.Value is Hl7.FluentPath.ConstantValue)
                {
                    result = (r.Value as Hl7.FluentPath.ConstantValue).Value;
                }
                else
                {
                    result = r.Value;
                }

                if (result is bool)
                {
                    return new FhirBoolean((bool)result);
                }
                if (result is long)
                {
                    return new Integer((int)(long)result);
                }
                if (result is decimal)
                {
                    return new FhirDecimal((decimal)result);
                }
                if (result is string)
                {
                    return new FhirString((string)result);
                }
                if (result is PartialDateTime)
                {
                    var dt = (PartialDateTime)result;
                    return new FhirDateTime(dt.ToUniversalTime());
                }
                else
                {
                    // This will throw an exception if the type isn't one of the FHIR types!
                    return (Base)result;
                }
            });            
        }

        public static object Scalar(this Base input, string expression, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            return inputNav.Scalar(expression, resourceNav);
        }

        public static bool Predicate(this Base input, string expression, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            return inputNav.Predicate(expression, resourceNav);
        }

        public static bool IsBoolean(this Base input, string expression, bool value, Resource resource = null)
        {
            var inputNav = new PocoNavigator(input);
            var resourceNav = resource != null ? new PocoNavigator(resource) : null;

            return inputNav.IsBoolean(expression, value, resourceNav);
        }

    }
}
