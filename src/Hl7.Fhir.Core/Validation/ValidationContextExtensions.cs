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
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    public static class ValidationContextExtensions
    {
        private const string RECURSE_ITEM_KEY = "__dotnetapi_recurse__";
        private const string RESOLVER_ITEM_KEY = "__dotnetapi_resolver__";

        /// <summary>
        /// Alters the ValidationContext to indicate that validation should or should not recurse into nested objects
        /// (i.e. validate members of the validated objects complex members recursively)
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="recursively"></param>
        public static void SetValidateRecursively(this ValidationContext ctx, bool recursively)
        {
            ctx.Items[RECURSE_ITEM_KEY] = recursively;
        }


        /// <summary>
        /// Gets the indication from the ValidationContext whether validation should recurse into nested objects
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static bool ValidateRecursively(this ValidationContext ctx)
        {
            if (ctx.Items.TryGetValue(RECURSE_ITEM_KEY, out object result))
                return result is bool b ? b : false;
            else
                return false;
        }

        /// <summary>
        /// Sets the callback to resolve a url to a resource when FhirPath validation encounters 
        /// a call to resolve() in an invariant.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="resolver"></param>
        public static void SetResolver(this ValidationContext ctx, Func<string,Resource> resolver)
        {
            ctx.Items[RESOLVER_ITEM_KEY] = resolver;
        }


        /// <summary>
        /// Gets the callback that is used by FhirPath validation when an invariant invokes the resolve() function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static Func<string,Resource> Resolver(this ValidationContext ctx)
        {
            if (ctx.Items.TryGetValue(RESOLVER_ITEM_KEY, out object result))
                return result is Func<string, Resource> c ? c : null;
            else
                return null;
        }

    }
}
