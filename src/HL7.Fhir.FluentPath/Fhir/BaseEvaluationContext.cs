/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.FluentPath.InstanceTree;
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
     
        public IEnumerable<IFluentPathValue> OriginalContext { get; set; }

        public IFluentPathElement OriginalResource { get; set; }

        public virtual void InvokeExternalFunction(string name, IList<IEnumerable<IFluentPathValue>> parameters)
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

        public virtual IFluentPathValue ResolveConstant(string name)
        {
            return null;
        }
    }
}
