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

            var result = inputNav.Select(expression, resourceNav);
            return result.Select(r => ((PocoNavigator)r).FhirValue);            
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
