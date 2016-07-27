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

        public static BaseEvaluationContext Root(IEnumerable<IValueProvider> input)
        {
            var newContext = new BaseEvaluationContext();

            newContext.SetThis(input);
            newContext.SetOriginalContext(input);

            return newContext;
        }

        private Dictionary<string, IEnumerable<IValueProvider>> _namedValues = new Dictionary<string, IEnumerable<IValueProvider>>();

        public virtual void SetValue(string name, IEnumerable<IValueProvider> value)
        {
            _namedValues.Remove(name);

            //if (value is Evaluator)
            //    _namedValues.Add(name, (Evaluator)value);
            //else
            //  _namedValues.Add(name, Eval.Return(value));

             _namedValues.Add(name, value);
        }


        public IEvaluationContext Parent { get; private set; }

        public virtual IEvaluationContext Nest()
        {
            var newContext = new BaseEvaluationContext();
            newContext.Parent = this;

            return newContext;
        }


        public virtual void Trace(string name, object data)
        {
            System.Diagnostics.Trace.WriteLine("=== Trace {0} ===".FormatWith(name));

            if (data == null)
                System.Diagnostics.Trace.WriteLine("(null)");

            else if (data is IEnumerable<IValueProvider>)
            {
                System.Diagnostics.Trace.WriteLine("Collection:".FormatWith(name));
                foreach (var element in (IEnumerable<IValueProvider>)data)
                {
                    if(element.Value != null)
                        System.Diagnostics.Trace.WriteLine("   " + element.Value.ToString());
                }
            }
            else if (data is IValueProvider)
            {
                var element = (IValueProvider)data;
                System.Diagnostics.Trace.WriteLine("Value:".FormatWith(name));

                if (element.Value != null)
                {                    
                    System.Diagnostics.Trace.WriteLine(element.Value.ToString());
                }
            }
            else
                System.Diagnostics.Trace.WriteLine(data.ToString());

            System.Diagnostics.Trace.WriteLine(Environment.NewLine);
        }




        public virtual IEnumerable<IValueProvider> ResolveValue(string name)
        {
            // First, try to directly get "normal" values
            IEnumerable<IValueProvider> result = null;
            _namedValues.TryGetValue(name, out result);

            if (result != null) return result;

            // If that failed, try to see if the parent has it
            if (Parent != null)
            {
                result = Parent.ResolveValue(name);
                if (result != null) return result;
            }

            // If that failed, get the special built-in ones
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

            if (value != null)
                return FhirValueList.Create(value);
            else
                return null;
        }


        public virtual IEnumerable<IValueProvider> InvokeExternalFunction(string name, IEnumerable<IValueProvider> focus, IEnumerable<IEnumerable<IValueProvider>> parameters)
        {
            throw new NotSupportedException("Function '{0}' is unknown".FormatWith(name));
        }
    }
}
