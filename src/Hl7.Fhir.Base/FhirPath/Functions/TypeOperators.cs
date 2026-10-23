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
        // The FHIR type name (or null, if there is no class mapping) of a POCO class, cached per CLR type,
        // so we do not have to consult the ModelInspector on every call. Only names that were resolved
        // successfully are cached: if the metadata of a class' assembly cannot be retrieved, the exception
        // propagates (and nothing is cached) just as it did before.
        private static readonly ConcurrentDictionary<Type, string?> _fhirTypeNameCache = new();

        public static bool Is(this PocoNode? focus, string type)
        {
            if (focus is null) return false;

            return Is(((ITypedElement)focus).InstanceType, type) || isBaseClassOf(focus.Poco.GetType(), type);

            // Tests the class and its base classes, starting at the root of the hierarchy and stopping at the first match.
            // This order is deliberate: a class from an application assembly without FHIR metadata (e.g. 
            // "class MyObservation : Observation") is only reached after all of its ancestors have been tried,
            // so asking for an ancestor (like "Resource") does not need - and does not fail on - that assembly.
            static bool isBaseClassOf(Type t, string type)
            {
                if (t.BaseType is not { } baseType) return false;  // System.Object has no FHIR type
                return isBaseClassOf(baseType, type) || Is(getFhirTypeName(t), type);
            }

            static string? getFhirTypeName(Type t) =>
                _fhirTypeNameCache.GetOrAdd(t, static type =>
#pragma warning disable CS0618 // Type or member is obsolete
                    ModelInspector.ForAssembly(type.Assembly).GetFhirTypeNameForType(type));
#pragma warning restore CS0618 // Type or member is obsolete
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