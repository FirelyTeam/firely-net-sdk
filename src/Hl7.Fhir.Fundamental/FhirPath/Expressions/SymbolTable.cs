/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.FhirPath.Expressions
{

    public class SymbolTable
    {
        public SymbolTable()
        {

        }

        public SymbolTable(SymbolTable parent)
        {
            Parent = parent;
        }

        public int Count()
        {
            var cnt = _entries.Count;
            if (Parent != null) cnt += Parent.Count();

            return cnt;
        }

        internal Invokee First()
        {
            return _entries.Any() ? _entries.First().Body : (Parent?.First());
        }

        public SymbolTable Parent { get; private set; }

        [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplayValue()}}")]
        private class TableEntry
        {
            public string DebuggerDisplayValue()
            {
                if (Signature != null)
                {
                    var sb = new StringBuilder();
                    sb.Append(Signature.ReturnType.Name);
                    sb.Append(' ');
                    sb.Append(Signature.Name);
                    sb.Append(" (");
                    bool b = false;
                    foreach (var item in Signature.ArgumentTypes)
                    {
                        if (b)
                            sb.Append(", ");
                        sb.Append(item.Name);
                        b = true;
                    }
                    sb.Append(')');
                    return sb.ToString();
                }
                return null;
            }

            public CallSignature Signature { get; private set; }
            public Invokee Body { get; private set; }

            public TableEntry(CallSignature signature, Invokee body)
            {
                Signature = signature;
                Body = body;
            }
        }

        private ConcurrentBag<TableEntry> _entries = new();

        internal void Add(CallSignature signature, Invokee body)
        {
            _entries.Add(new TableEntry(signature, body));
        }

        public SymbolTable Filter(string name, int argCount)
        {
            var result = new SymbolTable
            {
                _entries = new(_entries.Where(e => e.Signature.Matches(name, argCount)))
            };

            if (Parent != null)
                result.Parent = Parent.Filter(name, argCount);

            return result;
        }

        internal Invokee DynamicGet(string name, IEnumerable<object> args)
        {
            var exactMatches = _entries.Where(e => e.Signature.DynamicExactMatches(name, args));
            TableEntry entry = exactMatches.Union(_entries.Where(e => e.Signature.DynamicMatches(name, args))).FirstOrDefault();

            if (entry == null && Parent != null) return Parent.DynamicGet(name, args);

            return entry?.Body;
        }
    }


    public static class SymbolTableExtensions
    {
        public static void Add<R>(this SymbolTable table, string name, Func<R> func)
        {
            table.Add(new CallSignature(name, typeof(R)), InvokeeFactory.Wrap(func));
        }

        public static void Add<A, R>(this SymbolTable table, string name, Func<A, R> func, bool doNullProp = false)
        {
            if (typeof(A) != typeof(EvaluationContext))
                table.Add(new CallSignature(name, typeof(R), typeof(A)), InvokeeFactory.Wrap(func, doNullProp));
            else
                table.Add(new CallSignature(name, typeof(R)), InvokeeFactory.Wrap(func, doNullProp));
        }

        public static void Add<A, B, R>(this SymbolTable table, string name, Func<A, B, R> func, bool doNullProp = false)
        {
            if (typeof(B) != typeof(EvaluationContext))
                table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B)), InvokeeFactory.Wrap(func, doNullProp));
            else
                table.Add(new CallSignature(name, typeof(R), typeof(A)), InvokeeFactory.Wrap(func, doNullProp));
        }

        public static void Add<A, B, C, R>(this SymbolTable table, string name, Func<A, B, C, R> func, bool doNullProp = false)
        {
            if (typeof(C) != typeof(EvaluationContext))
                table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C)), InvokeeFactory.Wrap(func, doNullProp));
            else
                table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B)), InvokeeFactory.Wrap(func, doNullProp));
        }

        public static void Add<A, B, C, D, R>(this SymbolTable table, string name, Func<A, B, C, D, R> func, bool doNullProp = false)
        {
            if (typeof(D) != typeof(EvaluationContext))
                table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C), typeof(D)), InvokeeFactory.Wrap(func, doNullProp));
            else
                table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C)), InvokeeFactory.Wrap(func, doNullProp));

        }

        public static void AddLogic(this SymbolTable table, string name, Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            table.Add(new CallSignature(name, typeof(bool?), typeof(object), typeof(Func<bool?>), typeof(Func<bool?>)),
                InvokeeFactory.WrapLogic(func));
        }

        public static void AddVar(this SymbolTable table, string name, object value)
        {
            table.AddVar(name, ElementNode.ForPrimitive(value));
        }

        public static void AddVar(this SymbolTable table, string name, ITypedElement value)
        {
            table.Add(new CallSignature(name, typeof(string)), InvokeeFactory.Return(value));
        }
    }
}
