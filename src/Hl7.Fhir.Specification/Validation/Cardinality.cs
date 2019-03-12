/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Validation
{

    internal class Cardinality
    {
        public int? Min;
        public string Max;

        public static Cardinality FromElementDefinition(ElementDefinition def)
        {
            return new Cardinality(def.Min, def.Max);
        }

        public Cardinality(int? min, string max)
        {
            Min = min;
            Max = max;
        }

        public bool InRange(int x)
        {
            if (Min != null && x < Min)
                return false;

            if (Max == "*" || Max == null)
                return true;

            int max = Convert.ToInt16(Max);
            return x <= max;
        }

        public override string ToString()
        {
            return (Min?.ToString()??"<-") + ".." + (Max ?? "->");
        }
    }

}