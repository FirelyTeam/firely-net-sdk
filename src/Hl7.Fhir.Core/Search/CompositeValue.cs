/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

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
    public class CompositeValue : ValueExpression
    {
        private const char TUPLESEPARATOR = '$';

        public ValueExpression[] Components { get; private set; }

        public CompositeValue(ValueExpression[] components)
        {
            if (components == null) throw Error.ArgumentNull("components");

            Components = components;
        }

        public CompositeValue(IEnumerable<ValueExpression> components)
        {
            if (components == null) throw Error.ArgumentNull("components");

            Components = components.ToArray();
        }

        public override string ToString()
        {
            var values = Components.Select(v => v.ToString());
            return String.Join(TUPLESEPARATOR.ToString(),values);
        }


        public static CompositeValue Parse(string text)
        {
            if (text == null) throw Error.ArgumentNull("text");

            var values = text.SplitNotEscaped(TUPLESEPARATOR);

            return new CompositeValue(values.Select(v => new UntypedValue(v)));
        }
    }
}