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

        public static IEnumerable<PocoNode> FilterType(this IEnumerable<PocoNode> focus, string typeName)
            => focus.Where(item => item.Is(typeName));

        /// <summary>
        /// Implements the FhirPath <c>type()</c> reflection function: returns a TypeInfo for each item in the focus.
        /// </summary>
        /// <remarks>
        /// <para>The TypeInfo has a <c>namespace</c> and a <c>name</c> element. System primitives (such as
        /// System.String) get a SimpleTypeInfo in the System namespace. All other values get a ClassInfo, including
        /// FHIR primitives such as FHIR.boolean, which are classes with id, extension and value elements.</para>
        /// <para>Backbone elements are named after their path, e.g. <c>Patient.contact</c>, as the Java reference
        /// engine does. Note that <c>is</c> and <c>ofType()</c> know these as BackboneElement or Element instead.
        /// Types from logical models, which are named by their canonical URL, are named by the last segment of that
        /// URL, in a namespace that is the rest of it.</para>
        /// <para>The <c>baseType</c> and <c>element</c> parts of the TypeInfo are not supported.</para>
        /// </remarks>
        public static IEnumerable<PocoNode> Type(this IEnumerable<PocoNode> focus)
            => focus.Select(getTypeInfo);

        private static PocoNode getTypeInfo(PocoNode item)
        {
            var (ns, name) = splitTypeName(item.Poco.TypeName);

            var typeInfo = new DynamicDataType
            {
                DynamicTypeName = ns == "System" && item.Poco is PrimitiveType ? "System.SimpleTypeInfo" : "System.ClassInfo"
            };
            typeInfo.SetValue("namespace", new FPString(ns));
            typeInfo.SetValue("name", new FPString(name));

            return PocoNode.Root(typeInfo);
        }

        // The type name of a POCO is its FHIR type name ("boolean", "Patient", "Patient.contact"), except for:
        // - the FhirPath System types, whose name is prefixed with their namespace ("System.String");
        // - types from logical models (such as CDA), which are named by their canonical URL. These are named by the
        //   last segment of the canonical, in a namespace that is the rest of the canonical. A canonical of the FHIR
        //   core model is in the FHIR namespace.
        private static (string Namespace, string Name) splitTypeName(string typeName)
        {
            const string SYSTEM_PREFIX = "System.";
            const string FHIR_CORE_BASE = "http://hl7.org/fhir/StructureDefinition";

            if (typeName.StartsWith(SYSTEM_PREFIX, StringComparison.Ordinal))
                return ("System", typeName.Substring(SYSTEM_PREFIX.Length));

            var lastSlash = typeName.LastIndexOf('/');
            if (lastSlash > 0 && lastSlash < typeName.Length - 1 && Uri.IsWellFormedUriString(typeName, UriKind.Absolute))
            {
                var @base = typeName.Substring(0, lastSlash);
                return (@base == FHIR_CORE_BASE ? "FHIR" : @base, typeName.Substring(lastSlash + 1));
            }

            return ("FHIR", typeName);
        }
    }
}