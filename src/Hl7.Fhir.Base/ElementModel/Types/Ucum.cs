/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Fhir.Metrics;
using M = Fhir.Metrics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Hl7.Fhir.ElementModel.Types
{
    public static class MetricConfiguration
    {
        [CLSCompliant(false)]
        public static Lazy<IMetricService> MetricService { get; set; } = new(() => FhirMetricService.Instance.Value);
    }
    
    internal static class Ucum
    {
        private static readonly IMetricService METRIC_SERVICE = MetricConfiguration.MetricService.Value;

        /// <summary>
        /// Try to canonicalize the system type quantity to Umum base quantity. So a 1,000 cm will be 10 m. Or an inch will be converted to a meter.
        /// </summary>
        /// <param name="quantity">A system type Quantity of system Ucum</param>
        /// <param name="canonicalizedQuantity">The converted system type Quantity when the conversion was a success.</param>
        /// <returns><c>true</c> when the conversion succeeded. Or <c>false</c> otherwise.</returns>
        internal static bool TryCanonicalize(this Quantity quantity, [NotNullWhen(true)] out Quantity? canonicalizedQuantity)
        {
            var qtyTuple = (
                quantity.Value.ToString(CultureInfo.InvariantCulture),
                quantity.Unit,
                quantity.System == QuantityUnitSystem.UCUM ? "http://unitsofmeasure.org" : ""
            );
            
            if (!METRIC_SERVICE.TryCanonicalize(qtyTuple, out var canonicalized))
            {
                canonicalizedQuantity = null;
                return false;
            }

            canonicalizedQuantity = quantityFromTuple(canonicalized!.Value);

            return true;
        }
        
        internal static bool TryMultiply(this Quantity quantity, Quantity multiplier, [NotNullWhen(true)] out Quantity? result)
        {
            var qty1 = (
                quantity.Value.ToString(CultureInfo.InvariantCulture),
                quantity.Unit,
                quantity.System == QuantityUnitSystem.UCUM ? "http://unitsofmeasure.org" : ""
            );
            var qty2 = (
                multiplier.Value.ToString(CultureInfo.InvariantCulture),
                multiplier.Unit,
                multiplier.System == QuantityUnitSystem.UCUM ? "http://unitsofmeasure.org" : ""
            );

            if (!METRIC_SERVICE.TryMultiply(qty1, qty2, out var resultTuple))
            {
                result = null;
                return false;
            }

            result = quantityFromTuple(resultTuple!.Value);

            return true;
        }
        
        internal static bool TryDivide(this Quantity quantity, Quantity divisor, [NotNullWhen(true)] out Quantity? result)
        {
            var qty1 = (
                quantity.Value.ToString(CultureInfo.InvariantCulture),
                quantity.Unit,
                quantity.System == QuantityUnitSystem.UCUM ? "http://unitsofmeasure.org" : ""
            );
            var qty2 = (
                divisor.Value.ToString(CultureInfo.InvariantCulture),
                divisor.Unit,
                divisor.System == QuantityUnitSystem.UCUM ? "http://unitsofmeasure.org" : ""
            );

            if (!METRIC_SERVICE.TryDivide(qty1, qty2, out var resultTuple))
            {
                result = null;
                return false;
            }

            result = quantityFromTuple(resultTuple!.Value);

            return true;
        }

        private static Quantity quantityFromTuple((string value, string unit, string? codesystem) quantity)
        {
            return new Quantity(
                decimal.Parse(quantity.value, NumberStyles.Any, CultureInfo.InvariantCulture),
                quantity.unit == "" ? "1" : quantity.unit,
                QuantityUnitSystem.UCUM
            );
        }
    }
}

#nullable restore
