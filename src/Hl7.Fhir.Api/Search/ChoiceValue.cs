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
    public class ChoiceValue : ValueExpression
    {
        private const char VALUESEPARATOR = ',';

        public ValueExpression[]  Choices { get; private set; }

        public ChoiceValue(ValueExpression[] choices)
        {
            if (choices == null) throw Error.ArgumentNull("choices");

            Choices = choices;
        }

        public ChoiceValue(IEnumerable<ValueExpression> choices)
        {
            if (choices == null) throw Error.ArgumentNull("choices");

            Choices = choices.ToArray();
        }

        public override string ToString()
        {
            var values = Choices.Select(v => v.ToString());
            return String.Join(VALUESEPARATOR.ToString(),values);
        }

        public static ChoiceValue Parse(string text)
        {
            if (text == null) throw Error.ArgumentNull("text");

            var values = text.SplitNotEscaped(VALUESEPARATOR);

            return new ChoiceValue(values.Select(v => splitIntoComposite(v)));
        }

        private static ValueExpression splitIntoComposite(string text)
        {
            var composite = CompositeValue.Parse(text);

            // If there's only one component, this really was a single value
            if (composite.Components.Length == 1)
                return composite.Components[0];
            else
                return composite;
        }
    }
}