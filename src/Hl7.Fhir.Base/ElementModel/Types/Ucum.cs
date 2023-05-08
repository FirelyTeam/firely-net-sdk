/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using Fhir.Metrics;
using Hl7.Fhir.Utility;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Base.ElementModel
{
    internal static class Ucum
    {
        private static readonly SystemOfUnits SYSTEM = UCUM.Load();

        public static Result<int> Compare(P.Quantity quantity, P.Quantity other)
        {
            return 0;
        }
    }
}

#nullable restore
