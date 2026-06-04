
using Hl7.Fhir.Language.Debugging;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Sprache;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Parser
{
    internal static class WhitespaceAndComments
    {
        /// <summary>
        /// Attach any whitespace and comments before the expression to the expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parser"></param>
        /// <param name="leading">the collection of Whitespace/comment sub-tokens to attach</param>
        /// <returns></returns>
        public static Parser<T> WithLeadingWS<T>(this Parser<T> parser, IEnumerable<WhitespaceSubToken> leading) where T : Hl7.FhirPath.Expressions.Expression
        {
            return i =>
            {
                var r = parser(i);

                if (r.WasSuccessful)
                {
                    if (leading != null & leading.Any())
                        r.Value.LeadingWhitespace = leading;
                }
                return r;
            };
        }

        public static Parser<T> SubTokenWithLeadingWS<T>(this Parser<T> parser, IEnumerable<WhitespaceSubToken> leading) where T : Hl7.FhirPath.Expressions.SubToken
        {
            return i =>
            {
                var r = parser(i);

                if (r.WasSuccessful)
                {
                    if (leading != null && leading.Any())
                        r.Value.LeadingWhitespace = leading;
                }
                return r;
            };
        }

        public static T WithLeadingWS<T>(this T expr, IEnumerable<WhitespaceSubToken> leading) where T : Hl7.FhirPath.Expressions.Expression
        {
            if (leading != null && leading.Any())
                expr.LeadingWhitespace = leading;
            return expr;
        }

        public static SubToken WithLeadingWS(this SubToken expr, IEnumerable<WhitespaceSubToken> leading)
        {
            if (leading != null && leading.Any())
                expr.LeadingWhitespace = leading;
            return expr;
        }

        public static T WithTrailingWS<T>(this T expr, IEnumerable<WhitespaceSubToken> trailing) where T : Hl7.FhirPath.Expressions.Expression
        {
            if (trailing != null && trailing.Any())
                expr.TrailingWhitespace = trailing;
            return expr;
        }

        public static T CaptureWhitespaceAndComments<T>(this T expr, IEnumerable<WhitespaceSubToken> leading, IEnumerable<WhitespaceSubToken> trailing) where T : Hl7.FhirPath.Expressions.Expression
        {
            if (leading != null && leading.Any())
                expr.LeadingWhitespace = leading;
            if (trailing != null && trailing.Any())
                expr.TrailingWhitespace = trailing;
            return expr;
        }
    }
}
