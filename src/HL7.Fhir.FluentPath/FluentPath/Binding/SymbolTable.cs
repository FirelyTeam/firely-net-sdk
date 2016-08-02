/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath;
using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using Hl7.Fhir.FluentPath.Functions;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.FluentPath.Binding
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
            var cnt = _entries.Count();
            if (Parent != null) cnt += Parent.Count();

            return cnt;
        }


        public Invokee First()
        {
            if (_entries.Any())
                return _entries.First().Body;
            else
            {
                if (Parent != null)
                    return Parent.First();
                else
                    return null;
            }
        }

        public SymbolTable Parent { get; private set; }

        private class TableEntry
        {
            public CallSignature Signature { get; private set; }
            public Invokee Body { get; private set; }

            public TableEntry(CallSignature signature, Invokee body)
            {
                Signature = signature;
                Body = body;
            }
        }

        private List<TableEntry> _entries = new List<TableEntry>();

        public void Add(CallSignature signature, Invokee body)
        {
            _entries.Add(new TableEntry(signature, body));
        }

        public SymbolTable Filter(string name, int argCount)
        {
            var result = new SymbolTable();
            result._entries = new List<TableEntry>(_entries.Where(e => e.Signature.Matches(name, argCount)));

            if (Parent != null)
                result.Parent = Parent.Filter(name, argCount);

            return result;
        }


        public Invokee Get(string name, IEnumerable<Type> argumentTypes)
        {
            TableEntry entry = _entries.SingleOrDefault(e => e.Signature.Matches(name, argumentTypes));

            if (entry == null && Parent != null) return Parent.Get(name, argumentTypes);

            return entry != null ? entry.Body : null;
        }

        public Invokee DynamicGet(string name, IEnumerable<object> args)
        {
            TableEntry entry = _entries.FirstOrDefault(e => e.Signature.DynamicMatches(name, args));

            if (entry == null && Parent != null) return Parent.DynamicGet(name, args);

            return entry != null ? entry.Body : null;
        }
    }


    public static class SymbolTableExtensions
    {
        public static Invokee Get(this SymbolTable table, CallSignature signature)
        {
            return table.Get(signature.Name, signature.ArgumentTypes);
        }

        public static void Add<R>(this SymbolTable table, string name, Func<R> func, bool doNullProp=false)
        {
            table.Add(new CallSignature(name, typeof(R)), 
                    doNullProp == false ? InvokeeFactory.Wrap(func) : InvokeeFactory.Wrap(func).NullProp());
        }

        public static void Add<A,R>(this SymbolTable table, string name, Func<A,R> func, bool doNullProp = false)
        {
            table.Add(new CallSignature(name, typeof(R), typeof(A)), 
                doNullProp == false ? InvokeeFactory.Wrap(func) : InvokeeFactory.Wrap(func).NullProp());
        }

        public static void Add<A,B,R>(this SymbolTable table, string name, Func<A,B,R> func, bool doNullProp = false)
        {
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B)), 
                    doNullProp == false ? InvokeeFactory.Wrap(func) : InvokeeFactory.Wrap(func).NullProp());
        }

        public static void Add<A, B, C, R>(this SymbolTable table, string name, Func<A, B,C, R> func, bool doNullProp = false)
        {
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C)),
                doNullProp == false ? InvokeeFactory.Wrap(func) : InvokeeFactory.Wrap(func).NullProp());
        }

        public static void Add<A, B, C, D, R>(this SymbolTable table, string name, Func<A,B,C,D,R> func, bool doNullProp = false)
        {
            table.Add(new CallSignature(name, typeof(R), typeof(A), typeof(B), typeof(C), typeof(D)),
                doNullProp == false ? InvokeeFactory.Wrap(func) : InvokeeFactory.Wrap(func).NullProp());
        }

        public static void AddLogic(this SymbolTable table, string name, Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            table.Add(new CallSignature(name, typeof(bool?), typeof(object), typeof(Func<bool?>), typeof(Func<bool?>)),
                InvokeeFactory.WrapLogic(func));
        }

        public static void AddVar(this SymbolTable table, string name, object value)
        {
            table.AddVar(name, new ConstantValue(value));
        }

        public static void AddVar(this SymbolTable table, string name, IValueProvider value)
        {
            table.Add(new CallSignature(name, typeof(string)), InvokeeFactory.Return(value));
        }

    }
}
