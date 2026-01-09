/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FocusCollection = System.Collections.Generic.IEnumerable<Hl7.Fhir.Model.PocoNode>;
// ReSharper disable PossibleMultipleEnumeration

namespace Hl7.FhirPath.Expressions;

internal class DynaDispatcher(string name, SymbolTable scope)
{
    public FocusCollection Dispatcher(Closure context, IEnumerable<Invokee> args)
    {
        var actualArgs = new List<FocusCollection>();

        var focus = args.First()(context, InvokeeFactory.EmptyArgs);
        context.Focus = focus;
        if (!focus.Any()) return [];

        actualArgs.Add(focus);
        var newCtx = context.Nest(focus);

        actualArgs.AddRange(args.Skip(1).Select(a => a(newCtx, InvokeeFactory.EmptyArgs)));
        if (actualArgs.Any(aa => !aa.Any())) return [];

        var entry = scope.DynamicGet(name, actualArgs);

        if (entry != null)
        {
            try
            {
                // The Get() here should never fail, since we already know there's a (dynamic) matching candidate
                // Need to clean up this duplicate logic later
                var argFuncs = actualArgs.Select(InvokeeFactory.Return);
                var result = entry(context, argFuncs);

                // Dynamically dispatched function arguments aren't wrapped
                // for the debug/trace, so need to manually put the focus back
                context.Focus = focus;
                return result;
            }
            catch (TargetInvocationException tie) when (tie.InnerException != null)
            {
                // Unwrap the very non-informative T.I.E, and throw the nested exception instead
                throw tie.InnerException;
            }
        }

        throw Error.Argument(noMatchError(actualArgs));
    }

    private static string noMatchError(IEnumerable<object> arguments)
    {
        if (!arguments.Any())
            return "(no signature)";

        string result = "on focus of type '{0}'".FormatWith(Typecasts.ReadableFhirPathName(arguments.First()));

        if (arguments.Skip(1).Any())
        {
            result = "with parameters of type " +
                     String.Join(" and ",
                         arguments.Skip(1).Select(Typecasts.ReadableFhirPathName));
        }

        return "cannot be called " + result;
    }
}