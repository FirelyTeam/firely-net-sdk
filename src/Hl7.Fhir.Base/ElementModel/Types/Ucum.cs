/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using Fhir.Metrics;
using System;
using System.Collections.Generic;
using M = Fhir.Metrics;

namespace Hl7.Fhir.ElementModel.Types
{
    internal static class Ucum
    {
        private static readonly Lazy<SystemOfUnits> SYSTEM = new(() => UCUM.Load());

        /// <summary>
        /// Try to canonicalize the system type quantity to Umum base quantity. So a 1,000 cm will be 10 m. Or an inch will be converted to a meter.
        /// </summary>
        /// <param name="quantity">A system type Quantity of system Ucum</param>
        /// <param name="canonicalizedQuantity">The converted system type Quantity when the conversion was a success.</param>
        /// <returns><c>true</c> when the conversion succeeded. Or <c>false</c> otherwise.</returns>
        internal static bool TryCanonicalize(this Quantity quantity, out Quantity? canonicalizedQuantity)
        {
            try
            {
                M.Quantity metricsQuantity = quantity.Value.toUnitsOfMeasureQuantity(quantity.Unit);
                metricsQuantity = SYSTEM.Value.Canonical(metricsQuantity);
                canonicalizedQuantity = new(metricsQuantity.Value.ToDecimal(), metricsQuantity.Metric.ToString(), QuantityUnitSystem.UCUM);
                return true;
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidCastException)
            {
                canonicalizedQuantity = null;
                return false;
            }
        }

        private static M.Quantity toUnitsOfMeasureQuantity(this decimal value, string unit)
        {
            Metric metric = (unit != null) ? SYSTEM.Value.Metric(unit) : new Metric(new List<Metric.Axis>());
            return new M.Quantity(value, metric);
        }
    }
}

#nullable restore
