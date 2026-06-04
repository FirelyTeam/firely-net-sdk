
using Hl7.Fhir.Language.Debugging;

namespace Hl7.FhirPath.Sprache
{
    partial class Parse
    {
        /// <summary>
        /// Construct a parser that will set the position to the position-aware
        /// T on successful match.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static Parser<T> Positioned<T>(this Parser<T> parser) where T : IPositionAware<T>
        {
            return i =>
            {
                var r = parser(i);

                if (r.WasSuccessful)
                {
                    return Result.Success(r.Value.SetPos(Position.FromInput(i), r.Remainder.Position - i.Position), r.Remainder);
                }
                return r;
            };
        }

        /// <summary>
        /// Construct a parser that will set the position to the position-aware
        /// T on successful match.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="positionInfo"></param>
        /// <returns></returns>
        public static T UsePositionFrom<T>(this T me, ISourcePositionInfo positionInfo)
            where T : IPositionAware<T>
        {
            if (positionInfo is Expressions.FhirPathExpressionLocationInfo li)
            {
                Position startPos = new Position(li.RawPosition, li.LineNumber, li.LinePosition);
                me.SetPos(startPos, li.Length);
            }
            return me;
        }

        /// <summary>
        /// Only use in Function Invocation
        /// </summary>
        /// <param name="me"></param>
        /// <param name="positionInfo"></param>
        /// <returns></returns>
        public static void SetPositionFrom(this Hl7.FhirPath.Expressions.Expression me, ISourcePositionInfo positionInfo)
        {
            if (positionInfo is Expressions.FhirPathExpressionLocationInfo li)
            {
                Position startPos = new Position(li.RawPosition, li.LineNumber, li.LinePosition);
                me.SetPos<Expressions.Expression>(startPos, li.Length);
            }
        }
    }
}
