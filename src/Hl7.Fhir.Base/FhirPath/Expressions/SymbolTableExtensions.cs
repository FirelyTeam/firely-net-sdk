/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;

namespace Hl7.FhirPath.Expressions;

public static class SymbolTableExtensions
{
    public static void Add<R>(this SymbolTable table, string name, Func<R> func)
    {
        table.Add(new CallSignature(name, typeof(R)), InvokeeFactory.Wrap(func));
    }

    public static void Add<A, R>(this SymbolTable table, string name, Func<A, R> func, bool doNullProp = false)
    {
        table.Add(
            typeof(A) != typeof(EvaluationContext)
                ? new CallSignature(name, typeof(R), typeof(A))
                : new CallSignature(name, typeof(R)), InvokeeFactory.Wrap(func, doNullProp));
    }

    public static void Add<A, B, R>(this SymbolTable table, string name, Func<A, B, R> func, bool doNullProp = false)
    {
        if (typeof(B) != typeof(EvaluationContext))
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B)), InvokeeFactory.Wrap(func, doNullProp));
        else
            table.Add(new CallSignature(name, typeof(R), typeof(A)), InvokeeFactory.Wrap(func, doNullProp));
    }

    public static void Add<A, B, C, R>(this SymbolTable table, string name, Func<A, B, C, R> func,
        bool doNullProp = false)
    {
        if (typeof(C) != typeof(EvaluationContext))
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C)),
                InvokeeFactory.Wrap(func, doNullProp));
        else
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B)), InvokeeFactory.Wrap(func, doNullProp));
    }

    public static void Add<A, B, C, D, R>(this SymbolTable table, string name, Func<A, B, C, D, R> func,
        bool doNullProp = false)
    {
        if (typeof(D) != typeof(EvaluationContext))
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C), typeof(D)),
                InvokeeFactory.Wrap(func, doNullProp));
        else
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C)),
                InvokeeFactory.Wrap(func, doNullProp));
    }

    public static void AddLogic(this SymbolTable table, string name, Func<Func<bool?>, Func<bool?>, bool?> func)
    {
        table.Add(new CallSignature(name, typeof(bool?), typeof(object), typeof(Func<bool?>), typeof(Func<bool?>)),
            InvokeeFactory.WrapLogic(func));
    }

    public static void AddVar(this SymbolTable table, string name, object value)
    {
        table.AddVar(name, PocoNode.ForAnyPrimitive(value));
    }

    public static void AddVar(this SymbolTable table, string name, PocoNode value)
    {
        table.Add(new CallSignature(name, typeof(string)), InvokeeFactory.Return(value));
    }
}