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
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath
{
    public interface IEvaluationContext
    {
        /// <summary>
        /// Keeps track of the parent context - this allows scoping of named values
        /// </summary>
        IEvaluationContext Parent { get; }

        /// <summary>
        /// Returns a new scoped context, for which the current context is the parent
        /// </summary>
        /// <returns></returns>
        IEvaluationContext Nest();

        /// <summary>
        /// Provide a named reference to an Evaluator 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns null when the name is not known</returns>
        IEnumerable<IValueProvider> ResolveValue(string name);

        /// <summary>
        /// Sets a name to the given value. If the value is already set, it will be replaced.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// If the value is an object other than an Evaluator, the given value will be
        /// wrapped in an Evaluator that returns the given value.
        /// </remarks>
        void SetValue(string name, IEnumerable<IValueProvider> value);

        /// <summary>
        /// Whenever the engine encountes an unknown function, it will call this method to try to invoke it externally
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters">An ordered set parameters, each a collection of IFhirPathValues representing the value of that parameter. The
        /// first parameter is the focus.</param>
        /// <remarks>Should throw NotSupportedException if the external function is not supported.</remarks>
        IEnumerable<IValueProvider> InvokeExternalFunction(string name, IEnumerable<IValueProvider> focus, IEnumerable<IEnumerable<IValueProvider>> parameters);

        void Trace(string name, object data);
    }


    public static class IEvaluationContextExtensions
    {
        public static void SetThis(this IEvaluationContext ctx, IEnumerable<IValueProvider> value)
        {
            ctx.SetValue("this", value);
        }

        public static IEnumerable<IValueProvider> GetThis(this IEvaluationContext ctx)
        {
            return ctx.ResolveValue("this");
        }

        public static void SetOriginalContext(this IEvaluationContext ctx, IEnumerable<IValueProvider> value)
        {
            ctx.SetValue("context", value);
        }

        public static IEnumerable<IValueProvider> GetOriginalContext(this IEvaluationContext ctx)
        {
            return ctx.ResolveValue("context");
        }

        public static IEvaluationContext Nest(this IEvaluationContext ctx, IEnumerable<IValueProvider> input)
        {
            var nested = ctx.Nest();
            nested.SetThis(input);

            return nested;
        }
    }
}
