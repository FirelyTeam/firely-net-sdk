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

        // The base type of each FHIR primitive that is a specialization of another primitive, taken from
        // the baseDefinition of the primitive's StructureDefinition (e.g. code, id and markdown are strings,
        // url, canonical, oid and uuid are uris). The POCOs for these types do not derive from each other,
        // so this hierarchy is not visible in the CLR class hierarchy.
        private static readonly Dictionary<string, string> _primitiveBaseTypes = new()
        {
            ["code"] = "string",
            ["id"] = "string",
            ["markdown"] = "string",
            ["url"] = "uri",
            ["canonical"] = "uri",
            ["oid"] = "uri",
            ["uuid"] = "uri",
            ["positiveInt"] = "integer",
            ["unsignedInt"] = "integer",
        };

        /// <summary>
        /// Implements the FhirPath is() function and operator: true if the focus is of the given type or
        /// one of its subtypes, including the specializations of the FHIR primitive types (so a FHIR code
        /// is a FHIR string).
        /// </summary>
        public static bool Is(this PocoNode? focus, string type) => isOfType(focus, type, includePrimitiveBaseTypes: true);

        /// <summary>
        /// Implements the FhirPath as() and ofType() functions. Unlike <see cref="Is(PocoNode?, string)"/>,
        /// these do not treat a specialized FHIR primitive as its base primitive type: the official FHIRPath
        /// test suite expects <c>Patient.gender.as(string)</c> and <c>Patient.gender.ofType(string)</c> to be
        /// empty, while <c>Patient.gender.is(string)</c> is true.
        /// </summary>
        public static IEnumerable<PocoNode> FilterType(this IEnumerable<PocoNode> focus, string typeName)
            => focus.Where(item => isOfType(item, typeName, includePrimitiveBaseTypes: false));

        private static bool isOfType(PocoNode? focus, string type, bool includePrimitiveBaseTypes)
        {
            if (focus is null) return false;

            var instanceType = ((ITypedElement)focus).InstanceType;
            if (Is(instanceType, type)) return true;

            if (includePrimitiveBaseTypes && instanceType is not null)
            {
                for (var current = instanceType; _primitiveBaseTypes.TryGetValue(current, out var baseType); current = baseType)
                {
                    if (Is(baseType, type)) return true;
                }
            }

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
    }
}