﻿/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.FhirPath.Expressions
{
    internal class DynaDispatcher
    {
        public DynaDispatcher(string name, SymbolTable scope)
        {
            _scope = scope;
            _name = name;
        }

        private readonly string _name;
        private readonly SymbolTable _scope;

        public IEnumerable<ITypedElement> Dispatcher(Closure context, IEnumerable<Invokee> args)
        {
            var actualArgs = new List<IEnumerable<ITypedElement>>();

            var focus = args.First()(context, InvokeeFactory.EmptyArgs);
            if (!focus.Any()) return ElementNode.EmptyList;

            actualArgs.Add(focus);
            var newCtx = context.Nest(focus);

            actualArgs.AddRange(args.Skip(1).Select(a => a(newCtx, InvokeeFactory.EmptyArgs)));
            if (actualArgs.Any(aa => !aa.Any())) return ElementNode.EmptyList;

            var entry = _scope.DynamicGet(_name, actualArgs);

            if (entry != null)
            {
                try
                {
                    // The Get() here should never fail, since we already know there's a (dynamic) matching candidate
                    // Need to clean up this duplicate logic later

                    var argFuncs = actualArgs.Select(InvokeeFactory.Return);
                    return entry(context, argFuncs);
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

            if (arguments.Skip(1).Any())
            {
                result = "with parameters of type " +
                        String.Join(" and ",
                        arguments.Skip(1).Select(a => Typecasts.ReadableFhirPathName(a)));
            }

            return "cannot be called " + result;
        }
    }
}
