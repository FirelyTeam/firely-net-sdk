/*
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// This is an implementation of <see cref="IFhirSerializationEngine"/> which uses the
/// new Poco-based parser and serializer, initialized with the default settings.
/// </summary>
internal partial class PocoSerializationEngine : IFhirSerializationEngine
{
    private delegate (Base?, IEnumerable<CodedException>) TryDeserializer();

    private readonly ModelInspector _inspector;
    internal Predicate<CodedException> IgnoreFilter { get; set; }
        
    internal PocoSerializationEngine(ModelInspector inspector, Predicate<CodedException>? ignoreFilter = null, FhirJsonPocoDeserializerSettings? jsonDeserializerSettings = null, FhirJsonPocoSerializerSettings? jsonSerializerSettings = null, FhirXmlPocoDeserializerSettings? xmlSettings = null)
    {
        _inspector = inspector;
        IgnoreFilter = ignoreFilter ?? (_ => false);
        _jsonDeserializerSettings = jsonDeserializerSettings ?? new FhirJsonPocoDeserializerSettings();
        _jsonSerializerSettings = jsonSerializerSettings ?? new FhirJsonPocoSerializerSettings();
        _xmlSettings = xmlSettings ?? new FhirXmlPocoDeserializerSettings();
    }

    internal PocoSerializationEngine(BaseFhirJsonPocoDeserializer deserializer,
        BaseFhirJsonPocoSerializer serializer)
    {
        _jsonDeserializer = deserializer;
        _jsonSerializer = serializer;
        // dirty, but this constructor is really not supposed to be supported for much longer
        var inspectorfield =
            typeof(BaseFhirJsonPocoDeserializer).GetField("_inspector", BindingFlags.NonPublic | BindingFlags.Instance);
        _inspector = (inspectorfield!.GetValue(_jsonDeserializer) as ModelInspector)!;
        IgnoreFilter = _ => false;
        _xmlSettings = new FhirXmlPocoDeserializerSettings();
    }
        
    private Base deserializeAndFilterErrors(TryDeserializer deserializer)
    {
        var (instance, issues) = deserializer();
        var relevantIssues = issues.Where(i => !IgnoreFilter(i)).ToList();

        return relevantIssues.Any() ? throw new DeserializationFailedException(instance, relevantIssues) : instance!;
    }
}

#nullable restore