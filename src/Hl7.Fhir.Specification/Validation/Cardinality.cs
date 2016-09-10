/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Validation
{

    internal class Cardinality
    {
        public int Min;
        public string Max;

        public static Cardinality FromElementDefinition(ElementDefinition def)
        {
            if (def.Min == null) throw Error.ArgumentNull("def.Min");
            if (def.Max == null) throw Error.ArgumentNull("def.Max");

            return new Cardinality(def.Min.Value, def.Max);
        }

        public Cardinality(int min, string max)
        {
            if (max == null) throw Error.ArgumentNull("max");

            Min = min;
            Max = max;
        }

        public bool InRange(int x)
        {
            if (x < Min)
                return false;

            if (Max == "*")
                return true;

            int max = Convert.ToInt16(Max);
            if (x > max)
                return false;

            return true;
        }

        public override string ToString()
        {
            return Min + ".." + Max;
        }
    }

}