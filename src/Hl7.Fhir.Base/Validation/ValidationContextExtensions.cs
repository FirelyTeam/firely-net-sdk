/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Extension methods on <see cref="ValidationContext" /> to support POCO validation.
    /// </summary>
    public static class ValidationContextExtensions
    {
        private const string RECURSE_ITEM_KEY = "__dotnetapi_recurse__";
        private const string NARRATIVE_VALIDATION_KIND_ITEM_KEY = "__dotnetapi_narrative_validation_kind__";
        private const string POSITIONINFO_ITEM_KEY = "__dotnetapi_positioninfo__";
        private const string LOCATION_ITEM_KEY = "__dotnetapi_location__";

        /// <summary>
        /// Alters the ValidationContext to indicate that validation should or should not recurse into nested objects
        /// (i.e. validate members of the validated objects complex members recursively)
        /// </summary>
        public static ValidationContext SetValidateRecursively(this ValidationContext ctx, bool recursively)
        {
            ctx.Items[RECURSE_ITEM_KEY] = recursively;
            return ctx;
        }

        /// <summary>
        /// Gets the indication from the ValidationContext whether validation should recurse into nested objects
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static bool ValidateRecursively(this ValidationContext ctx) =>
            ctx.Items.TryGetValue(RECURSE_ITEM_KEY, out var result) && result is bool b && b;

        /// <summary>
        /// Alters the ValidationContext to indicate the kind of narrative validation the
        /// <see cref="NarrativeXhtmlPatternAttribute"/> should perform.
        /// </summary>
        public static ValidationContext SetNarrativeValidationKind(this ValidationContext ctx, NarrativeValidationKind kind)
        {
            ctx.Items[NARRATIVE_VALIDATION_KIND_ITEM_KEY] = kind;
            return ctx;
        }

        /// <summary>
        /// Gets the kind of narrative validation the <see cref="NarrativeXhtmlPatternAttribute"/> should perform
        /// from the ValidationContext.
        /// </summary>
        public static NarrativeValidationKind GetNarrativeValidationKind(this ValidationContext ctx) =>
            ctx.Items.TryGetValue(NARRATIVE_VALIDATION_KIND_ITEM_KEY, out var result) && result is NarrativeValidationKind k ?
                    k : NarrativeValidationKind.FhirXhtml;

        /// <summary>
        /// Alters the ValidationContext to include line/position information for the validation errors to use.
        /// </summary>
        public static ValidationContext SetPositionInfo(this ValidationContext ctx, IPositionInfo position)
        {
            ctx.Items[POSITIONINFO_ITEM_KEY] = position;
            return ctx;
        }

        /// <summary>
        /// Gets the position information for the validation errors from the ValidationContext.
        /// </summary>
        public static IPositionInfo? GetPositionInfo(this ValidationContext ctx) =>
            ctx.Items.TryGetValue(POSITIONINFO_ITEM_KEY, out var result) && result is IPositionInfo pi ?
                    pi : null;

        /// <summary>
        /// Alters the ValidationContext to include the human-readable location for the validation errors to use.
        /// </summary>
        public static ValidationContext SetLocation(this ValidationContext ctx, string location)
        {
            ctx.Items[LOCATION_ITEM_KEY] = location;
            return ctx;
        }

        /// <summary>
        /// Gets the human-readable location for the validation errors from the ValidationContext.
        /// </summary>
        public static string? GetLocation(this ValidationContext ctx) =>
            ctx.Items.TryGetValue(LOCATION_ITEM_KEY, out var result) && result is string loc ?
                    loc : null;

    }
}

#nullable restore