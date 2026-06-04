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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.FhirPath.Expressions;

/// <summary>
/// Holds the functions and constants available for the FhirPath engine to bind to.
/// </summary>
public class SymbolTable
{
    /// <summary>
    /// An empty symbol table.
    /// </summary>
    public SymbolTable()
    {
        // Nothing
    }

    /// <summary>
    /// A local symbol table inside a parent scope.
    /// </summary>
    public SymbolTable(SymbolTable parent)
    {
        Parent = parent;
    }

    /// <summary>
    /// The number of entries in the symbol table, including the parent scope (if any).
    /// </summary>
    public int Count()
    {
        var cnt = _entries.Count;
        if (Parent != null) cnt += Parent.Count();

        return cnt;
    }

    internal Invokee? First() => _entries.Any() ? _entries.First().Body : (Parent?.First());

    /// <summary>
    /// The parent scope for this symbol table.
    /// </summary>
    public SymbolTable? Parent { get; private set; }

    [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplayValue()}}")]
    private class TableEntry(CallSignature signature, Invokee body)
    {
        public string DebuggerDisplayValue()
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

        public CallSignature Signature { get; } = signature;
        public Invokee Body { get; } = body;
    }

    private ConcurrentBag<TableEntry> _entries = [];

    internal void Add(CallSignature signature, Invokee body)
    {
        _entries.Add(new TableEntry(signature, body));
    }

    public SymbolTable Filter(string name, int argCount)
    {
        var result = new SymbolTable
        {
            _entries = new ConcurrentBag<TableEntry>(_entries.Where(e => e.Signature.Matches(name, argCount)))
        };

        if (Parent != null)
            result.Parent = Parent.Filter(name, argCount);

        return result;
    }

    internal Invokee? DynamicGet(string name, IEnumerable<object> args)
    {
        var exactMatches = _entries.Where(e => e.Signature.DynamicExactMatches(name, args));
        var entry = exactMatches.Union(_entries.Where(e => e.Signature.DynamicMatches(name, args))).FirstOrDefault();

        if (entry == null && Parent != null) return Parent.DynamicGet(name, args);

        return entry?.Body;
    }
}