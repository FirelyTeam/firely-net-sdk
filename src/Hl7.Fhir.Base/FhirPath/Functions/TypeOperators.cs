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
using System.Reflection;
using System.Runtime.CompilerServices;
using P = Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.FhirPath.Functions
{ 
    internal static class TypeOperators
    {
        // The FHIR type names of a POCO class and its base classes, cached per CLR type, so we do not
        // have to walk the type hierarchy on every call.
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

            // The names are taken from the [FhirType] attribute on each class in the hierarchy, which is
            // also where a ModelInspector gets them from. Reading the attribute directly does not need an
            // inspector (the FhirPath entry points create PocoNodes without one) and does not fail on
            // classes from assemblies without FHIR metadata, such as application-defined POCO subclasses.
            // Classes without the attribute are skipped, as they have no class mapping either.
            static string[] getFhirTypeNames(Type t) =>
                _fhirTypeNamesCache.GetOrAdd(t, static type =>
                {
                    var names = new List<string>();
                    for (var current = type; current is not null; current = current.BaseType)
                    {
                        if (current.GetCustomAttribute<FhirTypeAttribute>() is { } fhirType)
                            names.Add(fhirType.Name);
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

        /// <summary>
        /// Lenient type filter, used by the 'as' operator: it accepts any number of items and does not
        /// check whether the type name exists.
        /// </summary>
        /// <remarks>The FHIRPath specification says the 'as' operator should raise an error when the input
        /// has more than one item, but FHIR R4 search parameters rely on it working on collections
        /// (e.g. <c>Observation.component.value as Quantity</c>), so the operator stays lenient.</remarks>
        public static IEnumerable<PocoNode> FilterType(this IEnumerable<PocoNode> focus, string typeName)
            => focus.Where(item => item.Is(typeName));

        /// <summary>
        /// The ofType() function: returns the items of the given type. It is an error if no item matched
        /// and the type name is not a known System or FHIR type.
        /// </summary>
        public static IEnumerable<PocoNode> OfType(this IEnumerable<PocoNode> focus, string typeName)
        {
            var sawItems = false;
            var matched = false;

            foreach (var item in focus)
            {
                sawItems = true;
                if (!item.Is(typeName)) continue;

                matched = true;
                yield return item;
            }

            if (sawItems && !matched)
                ensureKnownType(typeName, focus);
        }

        /// <summary>
        /// The as() function. It is an error if no item matches and the type name is not a known System or FHIR
        /// type. When <paramref name="strict"/> is set, it is also an error if the input contains more than
        /// one item, as the FHIRPath specification requires; otherwise it filters like ofType().
        /// </summary>
        public static IEnumerable<PocoNode> AsType(this IEnumerable<PocoNode> focus, string typeName, bool strict)
        {
            if (!strict) return focus.OfType(typeName);

            var items = focus.Take(2).ToList();
            if (items.Count > 1)
                throw Error.InvalidOperation("The input collection of as() contains more than one item.");
            if (items.Count == 0) return [];

            if (items[0].Is(typeName)) return items;

            ensureKnownType(typeName, items);
            return [];
        }

        // Type names that are known to exist. Only positive results are cached: model assemblies can be
        // loaded later, and an unknown name raises an error anyway.
        private static readonly ConcurrentDictionary<string, bool> _knownTypeNames = new(StringComparer.Ordinal);

        // Only called when no item matched the type: when an item matched, the type obviously exists, which
        // also covers dynamic and custom types that no inspector knows about.
        private static void ensureKnownType(string typeName, IEnumerable<PocoNode> unmatched)
        {
            if (_knownTypeNames.ContainsKey(typeName)) return;

            if (!isKnownType(typeName, unmatched))
                throw Error.InvalidOperation($"Unknown type '{typeName}'.");

            _knownTypeNames.TryAdd(typeName, true);
        }

        private static bool isKnownType(string typeName, IEnumerable<PocoNode> unmatched)
        {
            var dot = typeName.IndexOf('.');
            var ns = dot < 0 ? null : typeName.Substring(0, dot);
            var name = typeName.Substring(dot + 1);

            // Names in other namespaces (e.g. logical models) cannot be checked, so we accept them.
            if (ns is not (null or "System" or "FHIR")) return true;

            if (ns is null or "System" && P.Any.TryGetSystemTypeByName(name, out _)) return true;
            if (ns is "System") return false;

            // FHIR types: ask the inspectors that belong to the input (the annotated one, and the ones for the
            // assemblies of the items and their root), then any inspector created so far. Note that
            // Bundle, Parameters and the primitives live in the base assembly, so the item's own assembly
            // is not enough.
            foreach (var inspector in candidateInspectors(unmatched))
            {
                if (inspector.FindClassMapping(name) is not null) return true;
            }

            return ModelInspector.AnyInspectorKnowsType(name);

            static IEnumerable<ModelInspector> candidateInspectors(IEnumerable<PocoNode> nodes)
            {
                foreach (var node in nodes)
                {
                    if (node.FindInspector() is { } annotated) yield return annotated;
                    if (inspectorFor(node.Poco.GetType().Assembly) is { } own) yield return own;
                    if (inspectorFor(rootOf(node).Poco.GetType().Assembly) is { } root) yield return root;
                }
            }

            static PocoNode rootOf(PocoNode node)
            {
                while (node.Parent is { } parent) node = parent;
                return node;
            }

#pragma warning disable CS0618 // ForAssembly is obsolete, but there is no other way to get the inspector for an assembly
            static ModelInspector? inspectorFor(Assembly assembly) =>
                assembly.GetCustomAttribute<FhirModelAssemblyAttribute>() is not null
                    ? ModelInspector.ForAssembly(assembly)
                    : null;
#pragma warning restore CS0618
        }
    }
}