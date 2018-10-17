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

        public IEnumerable<ITypedElement> Dispatcher(Closure context, IEnumerable<Invokee> args)
        {
            var actualArgs = new List<IEnumerable<ITypedElement>>();

            var focus = args.First()(context, InvokeeFactory.EmptyArgs);
            if (!focus.Any()) return FhirValueList.Empty;

            actualArgs.Add(focus);
            var newCtx = context.Nest(focus);

            actualArgs.AddRange(args.Skip(1).Select(a => a(newCtx, InvokeeFactory.EmptyArgs)));
            if (actualArgs.Any(aa=>!aa.Any())) return FhirValueList.Empty;

            var entry = _scope.DynamicGet(_name, actualArgs);

            if (entry != null)
            {
                try
                {
                    // The Get() here should never fail, since we already know there's a (dynamic) matching candidate
                    // Need to clean up this duplicate logic later
                    return entry(context, args);
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

            result = "on focus of type '{0}'".FormatWith(Typecasts.ReadableFhirPathName(arguments.First().GetType()));
            
            if(arguments.Skip(1).Any())
            {
                result = "with parameters of type '{0}' "
                        .FormatWith(String.Join(",", arguments.Skip(1).Select(a => Typecasts.ReadableFhirPathName(a.GetType()))), result);
            }

            return "Function cannot be called " + result;
        }     
    }
}
