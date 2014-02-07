using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class Criterium
    {
        public string ParamName { get; set; }

        public Operator Type { get; set; }
        public string Modifier { get; set; }

        public Expression Operand { get; set; }


        public static Criterium Parse(string key, string value)
        {
            if (key == null) throw Error.ArgumentNull("key");
            if (value == null) throw Error.ArgumentNull("value");

            var compVal = findComparator(value);

            var comp = compVal.Item1;
            var expression = compVal.Item2;

            return new Criterium() { ParamName = key, Type = comp, Operand = new UntypedValue(expression) };
        }

        private static Tuple<Operator, string> findComparator(string value)
        {
            Operator comparison = Operator.EQ;

            if (value.StartsWith(">=") && value.Length > 2)
            { comparison = Operator.GTE; value = value.Substring(2); }
            else if (value.StartsWith(">"))
            { comparison = Operator.GT; value = value.Substring(1); }
            else if (value.StartsWith("<=") && value.Length > 2)
            { comparison = Operator.LTE; value = value.Substring(2); }
            else if (value.StartsWith("<"))
            { comparison = Operator.LT; value = value.Substring(1); }
            else if (value.StartsWith("~"))
            { comparison = Operator.APPROX; value = value.Substring(1); }

            return Tuple.Create(comparison,value);
        }

    }


    /// <summary>
    /// Types of comparison operator applicable to searching on integer values
    /// </summary>
    public enum Operator
    {
        LT,     // less than
        LTE,    // less than or equals
        EQ,     // equals (default)
        APPROX, // approximately equals
        GTE,    // greater than or equals
        GT,     // greater than

        ISNULL, // has no value
        NOTNULL, // has value
        IN,      // equals one of a set of values
        REFERS   // refers to resource(s)
    }  
}
