using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class MultiValue : ValueExpression
    {
        private const char VALUESEPARATOR = ',';

        public ValueExpression[]  Value { get; private set; }

        public MultiValue(ValueExpression[] values)
        {
            if (values == null) throw Error.ArgumentNull("value");

            Value = values;
        }

        public MultiValue(IEnumerable<ValueExpression> values)
        {
            Value = values.ToArray();
        }

        public override string ToString()
        {
            var values = Value.Select(v => v.ToString());
            return String.Join(VALUESEPARATOR.ToString(),values);
        }


        public static MultiValue Parse(string text)
        {
            if (text == null) throw Error.ArgumentNull("text");

            var values = text.SplitNotEscaped(VALUESEPARATOR);

            return new MultiValue(values.Select(v => new UntypedValue(v)));
        }
    }
}