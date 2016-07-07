using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Sprache;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Binding
{
    //TODO: Can we even make the context lazy (IE the focus is evaluated as late as possible?)
    public delegate IEnumerable<IValueProvider> Invokee(IEvaluationContext context, IEnumerable<IValueProvider> focus, IEnumerable<Evaluator> arguments);  
}