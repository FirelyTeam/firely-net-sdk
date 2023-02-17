using System;

namespace Hl7.FhirPath.FhirPath.Functions
{
    internal static class MathOperators
    {
        public static decimal? Sqrt(this decimal focus)
        {
            var result = Math.Sqrt((double)focus);
            return double.IsNaN(result) ? (decimal?)null : (decimal)result;
        }

        public static decimal? Power(this decimal focus, decimal exponent)
        {
            var result = Math.Pow((double)focus, (double)exponent);
            return double.IsNaN(result) ? (decimal?)null : (decimal)result;
        }
    }
}
