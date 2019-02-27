/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Expressions
{
    internal class DynaDispatcher
    {
        public DynaDispatcher(string name, SymbolTable scope)
        {
            _scope = scope;
            _name = name;
        }


        private string _name;
        private SymbolTable _scope;

        public IEnumerable<ITypedElement> Dispatcher(EvaluationContext ctx, IList<Invokee> args)
        {
            var actualArgs = new List<IEnumerable<ITypedElement>>();

            if (args.Count > 1)
            {
                var focus = args[0](ctx, InvokeeFactory.EmptyArgs);
                if (!focus.Any()) return FhirValueList.Empty;

                // HACK! Run all arguments just to get the type and select the right method,
                // which will then run the arguments again :-(
                // Need to fix this!!!
                actualArgs.Add(focus);
                actualArgs.AddRange(args.Skip(1).Select(a => a(ctx, InvokeeFactory.EmptyArgs)));
                if (actualArgs.Any(aa => !aa.Any())) return FhirValueList.Empty;
            }

            var entry = _scope.DynamicGet(_name, actualArgs);

            if (entry != null)
            {
                try
                {
                    // The Get() here should never fail, since we already know there's a (dynamic) matching candidate
                    // Need to clean up this duplicate logic later
                    return entry(ctx, args);
                }
                catch (TargetInvocationException tie)
                {
                    // Unwrap the very non-informative T.I.E, and throw the nested exception instead
                    throw tie.InnerException;
                }
            }
            else
            {
                //TODO: Make error reporting better
                throw Error.Argument(noMatchError(actualArgs));
            }
        }


        private string noMatchError(IEnumerable<object> arguments)
        {
            string result;

            if (!arguments.Any())
                return "(no signature)";

            result = "on focus of type '{0}'".FormatWith(Typecasts.ReadableFhirPathName(arguments.First()));
            
            if(arguments.Skip(1).Any())
            {
                result = "with parameters of type " +
                        String.Join(" and ", 
                        arguments.Skip(1).Select(a => Typecasts.ReadableFhirPathName(a)));
            }

            return "cannot be called " + result;
        }     
    }
}
