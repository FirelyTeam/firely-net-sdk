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

        // Names of the core FHIR profiles on datatypes, which may be used as type names, but do not have a
        // class of their own.
        private static readonly HashSet<string> _profiledTypeNames = new(StringComparer.Ordinal) { "SimpleQuantity", "MoneyQuantity" };

        // Only called when no item matched the type: when an item matched, the type obviously exists.
        // The outcome only depends on the input (not on which models were loaded before), so it is not cached.
        private static void ensureKnownType(string typeName, IReadOnlyCollection<PocoNode> unmatched)
        {
            var dot = typeName.IndexOf('.');
            var ns = dot < 0 ? null : typeName.Substring(0, dot);
            var name = typeName.Substring(dot + 1);

            // Names in other namespaces (e.g. logical models) cannot be checked, so we accept them.
            if (ns is not (null or "System" or "FHIR")) return;

            // System types are fully known.
            if (ns is null or "System" && P.Any.TryGetSystemTypeByName(name, out _)) return;
            if (ns is "System") throw unknown();

            if (_profiledTypeNames.Contains(name)) return;

            // FHIR types: we need the model the input belongs to.
            if (modelOf(unmatched) is { } model)
            {
                if (model.FindClassMapping(name) is not null) return;

                // Data of dynamic or custom types (e.g. logical models such as CDA) has types no inspector knows,
                // so we cannot tell whether the name is valid.
                if (unmatched.Any(n => isUnverifiable(n) || isUnverifiable(rootOf(n)))) return;

                throw unknown();
            }

            // The input only contains classes from the base or conformance assembly (e.g. a Bundle or literals built
            // in code), whose model does not include the release-specific types. Only names the base model knows can be
            // confirmed; for anything else we cannot tell, so we accept it.
            return;

            InvalidOperationException unknown() => Error.InvalidOperation($"Unknown type '{typeName}'.");

            // FHIRPath literals (System types) are dynamic too, but their types are known.
            static bool isUnverifiable(PocoNode node) =>
                node.Poco is IDynamicType and not CqlPrimitive || !isModelAssembly(node.Poco.GetType().Assembly);
        }

        // The model of the input: an inspector annotated on the input, or else the inspector for the first
        // release-specific model assembly the items (or their roots) come from. Returns null if the input
        // only has classes from the base or conformance assembly.
        private static ModelInspector? modelOf(IEnumerable<PocoNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.FindInspector() is { } annotated) return annotated;
                if (releaseInspectorFor(node.Poco.GetType().Assembly) is { } own) return own;
                if (releaseInspectorFor(rootOf(node).Poco.GetType().Assembly) is { } root) return root;
            }

            return null;

#pragma warning disable CS0618 // ForAssembly is obsolete, but there is no other way to get the inspector for an assembly
            static ModelInspector? releaseInspectorFor(Assembly assembly) =>
                !isSharedModelAssembly(assembly) && isModelAssembly(assembly) ? ModelInspector.ForAssembly(assembly) : null;
#pragma warning restore CS0618
        }

        // The base assembly and the conformance assembly (shared by R4, R4B, R5 and R6) are used by more than one
        // release, so their classes cannot tell which release-specific model the data belongs to.
        private static bool isSharedModelAssembly(Assembly assembly) =>
            assembly == typeof(Base).Assembly || assembly.GetName().Name == "Hl7.Fhir.Conformance";

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