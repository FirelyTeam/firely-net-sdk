/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.FluentPath.Support;

namespace Hl7.FluentPath
{
    public class Quantity
    {
        public decimal Value { get; }
        public string Unit { get; }

        public Quantity(double value, string unit) : this((decimal)value, unit)
        {
            // call other constructor
        }

        public Quantity(decimal value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public static bool operator <(Quantity a, Quantity b)
        {
            enforceSameUnits(a,b);

            return a.Value < b.Value;
        }

        public static bool operator <=(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value <= b.Value;
        }


        public static bool operator >(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value > b.Value;
        }

        public static bool operator >=(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value >= b.Value;
        }


        public static bool operator ==(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return Object.Equals(a, b);
        }

        public static bool operator !=(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return !Object.Equals(a, b);
        }


        private static void enforceSameUnits(Quantity a, Quantity b)
        {
            if (a.Unit != b.Unit)
                throw Error.NotSupported("Comparing quantities with different units is not yet supported");
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj)) return true;

            var q = obj as Quantity;
            if (Object.ReferenceEquals(q,null)) return false;

            return q.Unit == this.Unit && q.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Value.GetHashCode();
        }


    }
}
