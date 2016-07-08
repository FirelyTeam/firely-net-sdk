/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;

namespace Hl7.Fhir.FluentPath.Binding
{
    //TODO: Can we even make the context lazy (IE the focus is evaluated as late as possible?)
    public delegate IEnumerable<IValueProvider> Invokee(IEvaluationContext context, IEnumerable<IValueProvider> focus, IEnumerable<Evaluator> arguments);  
}