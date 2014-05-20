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
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    public static class ValidationContextExtensions
    {
        private const string RECURSE_ITEM_KEY = "__dotnetapi_recurse__";

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
            object result;

            if (ctx.Items.TryGetValue(RECURSE_ITEM_KEY, out result))
                return result is bool ? (bool)result : false;
            else
                return false;
        }
    }
}
