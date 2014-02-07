using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class Criterium
    {
        public const string MISSINGMODIF = "missing";
        public const string MISSINGTRUE = "true";
        public const string MISSINGFALSE = "false";

        public string ParamName { get; set; }

        private Operator _type = Operator.EQ;
        public Operator Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Modifier { get; set; }

        public Expression Operand { get; set; }


        public static Criterium Parse(string key, string value)
        {
            if (key == null) throw Error.ArgumentNull("key");
            if (value == null) throw Error.ArgumentNull("value");

            string modifier = null;
            Operator type = Operator.EQ;

            // First, find modifiers
            var lIndex = key.LastIndexOf(':');

            if (lIndex != -1)
            {
                modifier = key.Substring(lIndex+1);
                key = key.Substring(0,lIndex);
            }

            // :missing modifier is actually not a real modifier and is turned into
            // either a ISNULL or NOTNULL operator
            if (modifier == MISSINGMODIF)
            {
                modifier = null;

                if (value == MISSINGTRUE)
                    type = Operator.ISNULL;
                else if (value == MISSINGFALSE)
                    type = Operator.NOTNULL;
                else
                    throw Error.Argument("value", "For the :missing modifier, only values 'true' and 'false' are allowed");

                value = null;
            }

            // else see if the value starts with a comparator
            else
            {
                var compVal = findComparator(value);

                type = compVal.Item1;
                value = compVal.Item2;
            }

            // Construct the new criterium based on the parsed values
            return new Criterium() 
            {   
                ParamName = key, Type = type, Modifier = modifier, 
                Operand = value != null ? new UntypedValue(value) :null 
            };
        }


        public string BuildKey()
        {
            var result = ParamName;

            // Turn ISNULL and NOTNULL operators into the :missing modifier
            if (Type == Operator.ISNULL || Type == Operator.NOTNULL) return result + ":missing";

            if (!String.IsNullOrEmpty(Modifier)) result = result + ":" + Modifier;

            return result;
        }

        public string BuildValue()
        {
            // Turn ISNULL and NOTNULL operators into either true/or false to match the :missing modifier
            if (Type == Operator.ISNULL) return "true";
            if (Type == Operator.NOTNULL) return "false";


            string value = Operand.ToString();

            // Add comparator if we have one
            switch (Type)
            {
                case Operator.APPROX: return "~" + value;
                case Operator.EQ: return value;
                case Operator.GT: return ">" + value;
                case Operator.GTE: return ">=" + value;
                case Operator.LT: return "<" + value;
                case Operator.LTE: return "<=" + value;
                default:
                    throw Error.NotImplemented("Operator of type '{0}' is not supported",Type.ToString());
            }
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
