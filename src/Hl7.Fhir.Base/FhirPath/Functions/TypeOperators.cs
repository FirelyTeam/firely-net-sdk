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
        /// The ofType() function: returns the items of the given type. It is an error if no item matched
        /// and the type name is not a known System or FHIR type.
        /// </summary>
        /// <remarks>The result is materialized, so that an unknown type name is reported by this function
        /// rather than by whichever function enumerates the result.</remarks>
        public static IEnumerable<PocoNode> OfType(this IEnumerable<PocoNode> focus, string typeName)
        {
            var items = focus as IReadOnlyCollection<PocoNode> ?? focus.ToList();
            var result = items.Where(item => item.Is(typeName)).ToList();

            if (result.Count == 0 && items.Count > 0)
                ensureKnownType(typeName, items);

            return result;
        }

        /// <summary>
        /// The as() function and the 'as' operator. It is an error if no item matches and the type name is not a
        /// known System or FHIR type. When <paramref name="strict"/> is set, it is also an error if the input
        /// contains more than one item, as the FHIRPath specification requires; otherwise it filters like
        /// ofType(), since FHIR R4 core invariants and search parameters use 'as' on collections
        /// (e.g. <c>Observation.component.value as Quantity</c>).
        /// </summary>
        public static IEnumerable<PocoNode> AsType(this IEnumerable<PocoNode> focus, string typeName, bool strict)
        {
            if (!strict) return focus.OfType(typeName);

            var items = focus.Take(2).ToList();
            if (items.Count > 1)
                throw Error.InvalidOperation("The input collection of 'as' contains more than one item.");
            if (items.Count == 0) return [];

            if (items[0].Is(typeName)) return items;

            ensureKnownType(typeName, items);
            return [];
        }

        // FHIR type names that were verified to exist. Only these are cached: model assemblies can be loaded
        // later, an unknown name raises an error anyway, and names we cannot verify (other namespaces, data
        // of dynamic types) are not cached so user-supplied expressions cannot grow this without bound.
        private static readonly ConcurrentDictionary<string, bool> _knownTypeNames = new(StringComparer.Ordinal);

        // Names of the core FHIR profiles on datatypes, which may be used as type names, but do not have a
        // class of their own.
        private static readonly HashSet<string> _profiledTypeNames = new(StringComparer.Ordinal) { "SimpleQuantity", "MoneyQuantity" };

        // Only called when no item matched the type: when an item matched, the type obviously exists.
        // Note that the result is not specific to a FHIR release: a name is accepted if any loaded model knows
        // it, which errs on the side of leniency.
        private static void ensureKnownType(string typeName, IEnumerable<PocoNode> unmatched)
        {
            if (_knownTypeNames.ContainsKey(typeName)) return;

            var dot = typeName.IndexOf('.');
            var ns = dot < 0 ? null : typeName.Substring(0, dot);
            var name = typeName.Substring(dot + 1);

            // Names in other namespaces (e.g. logical models) cannot be checked, so we accept them.
            if (ns is not (null or "System" or "FHIR")) return;

            if (isKnownType(ns, name, unmatched))
            {
                _knownTypeNames.TryAdd(typeName, true);
                return;
            }

            // Data of dynamic or custom types (e.g. logical models such as CDA) has types no inspector knows,
            // so we cannot tell whether the name is valid.
            if (ns is not "System" && unmatched.Any(n => isUnverifiable(n) || isUnverifiable(rootOf(n)))) return;

            throw Error.InvalidOperation($"Unknown type '{typeName}'.");

            static bool isUnverifiable(PocoNode node) =>
                node.Poco is IDynamicType || !isModelAssembly(node.Poco.GetType().Assembly);
        }

        private static bool isKnownType(string? ns, string name, IEnumerable<PocoNode> unmatched)
        {
            if (ns is null or "System" && P.Any.TryGetSystemTypeByName(name, out _)) return true;
            if (ns is "System") return false;

            if (_profiledTypeNames.Contains(name)) return true;

            // FHIR types: ask the inspectors that belong to the input (the annotated one, and the ones for the
            // assemblies of the first item and its root), then any inspector created so far. Note that
            // Bundle, Parameters and the primitives live in the base assembly, so the item's own assembly
            // is not enough.
            var first = unmatched.First();
            if (first.FindInspector()?.FindClassMapping(name) is not null) return true;
            if (inspectorFor(first.Poco.GetType().Assembly)?.FindClassMapping(name) is not null) return true;
            if (inspectorFor(rootOf(first).Poco.GetType().Assembly)?.FindClassMapping(name) is not null) return true;

            return ModelInspector.AnyInspectorKnowsType(name);

#pragma warning disable CS0618 // ForAssembly is obsolete, but there is no other way to get the inspector for an assembly
            static ModelInspector? inspectorFor(Assembly assembly) =>
                isModelAssembly(assembly) ? ModelInspector.ForAssembly(assembly) : null;
#pragma warning restore CS0618
        }

        private static readonly ConcurrentDictionary<Assembly, bool> _modelAssemblies = new();

        private static bool isModelAssembly(Assembly assembly) =>
            _modelAssemblies.GetOrAdd(assembly, static a => a.GetCustomAttribute<FhirModelAssemblyAttribute>() is not null);

        private static PocoNode rootOf(PocoNode node)
        {
            while (node.Parent is { } parent) node = parent;
            return node;
        }
    }
}