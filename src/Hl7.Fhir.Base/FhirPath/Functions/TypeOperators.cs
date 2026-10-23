/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Runtime.CompilerServices;

#nullable enable

namespace Hl7.FhirPath.Functions
{ 
    internal static class TypeOperators
    {
        // The FHIR type names of a POCO class and all of its base classes (that have a class mapping), 
        // cached per CLR type, so we do not have to walk the type hierarchy on every call.
        private static readonly ConcurrentDictionary<Type, string[]> _fhirTypeNamesCache = new();

        public static bool Is(this PocoNode? focus, string type)
        {
            if (focus is null) return false;

            if (Is(((ITypedElement)focus).InstanceType, type)) return true;

            foreach (var typeName in getFhirTypeNames(focus.Poco.GetType()))
            {
                if (Is(typeName, type)) return true;
            }

            return false;

            static string[] getFhirTypeNames(Type t) =>
                _fhirTypeNamesCache.GetOrAdd(t, static type =>
                {
                    var names = new List<string>();
                    for (var current = type; current?.BaseType is not null; current = current.BaseType)
                    {
#pragma warning disable CS0618 // Type or member is obsolete
                        if (ModelInspector.ForAssembly(current.Assembly).GetFhirTypeNameForType(current) is { } name)
#pragma warning restore CS0618 // Type or member is obsolete
                            names.Add(name);
                    }
                    return names.ToArray();
                });
        }

        public static bool Is(string? instanceType, string declaredType)
        {
            if (instanceType is null) return false;

            // Split both names into a namespace and a local name at the first '.' (no allocations).
            // Bit of a hack, this hardwires the FhirPath implementation to FHIR: an instance type
            // without a namespace is assumed to be in the FHIR namespace.
            var instanceDot = instanceType.IndexOf('.');
            var declaredDot = declaredType.IndexOf('.');
            var instanceNamespace = instanceDot < 0 ? "FHIR".AsSpan() : instanceType.AsSpan(0, instanceDot);

            if (!instanceType.AsSpan(instanceDot + 1).SequenceEqual(declaredType.AsSpan(declaredDot + 1)))
                return false;

            // A declared type without a namespace matches both the System and the FHIR namespace.
            return declaredDot < 0
                ? instanceNamespace.SequenceEqual("System") || instanceNamespace.SequenceEqual("FHIR")
                : instanceNamespace.SequenceEqual(declaredType.AsSpan(0, declaredDot));
        }

        public static IEnumerable<PocoNode> FilterType(this IEnumerable<PocoNode> focus, string typeName)
            => focus.Where(item => item.Is(typeName));
    }
}