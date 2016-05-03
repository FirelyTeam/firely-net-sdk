/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath
{
    public class BaseEvaluationContext : IEvaluationContext
    {
        public BaseEvaluationContext()
        {
        }

        private Stack<IEnumerable<IFluentPathValue>> _focusStack = new Stack<IEnumerable<IFluentPathValue>>();

        public Stack<IEnumerable<IFluentPathValue>> FocusStack
        {
            get
            {
                return _focusStack;
            }
        }

        public IEnumerable<IFluentPathValue> OriginalContext { get; set; }

        public virtual IEnumerable<IFluentPathValue> InvokeExternalFunction(string name, IEnumerable<IEnumerable<IFluentPathValue>> parameters)
        {
            throw new NotSupportedException("Function '{0}' is unknown".FormatWith(name));
        }

        public virtual void Log(string argument, IEnumerable<IFluentPathValue> focus)
        {
            System.Diagnostics.Trace.WriteLine(argument);

            foreach (var element in focus)
            {
                System.Diagnostics.Trace.WriteLine("=========");
                System.Diagnostics.Trace.WriteLine(element.ToString());
            }
        }

        public virtual IEnumerable<IFluentPathValue> ResolveValue(string name)
        {
            if (name == "context")
                return OriginalContext;

            string value = null;
            if (name.StartsWith("ext-"))
                value = "http://hl7.org/fhir/StructureDefinition/" + name.Substring(4);
            else if (name.StartsWith("vs-"))
                value = "http://hl7.org/fhir/ValueSet/" + name.Substring(3);
            else if (name == "sct")
                value = "http://snomed.info/sct";
            else if (name == "loinc")
                value = "http://loinc.org";
            else if (name == "ucum")
                value = "http://unitsofmeasure.org";

            return value != null ? FhirValueList.Create(value) : null;
        }
    }
}
