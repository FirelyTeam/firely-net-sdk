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

namespace Hl7.Fhir.Serialization;

/// <summary>
/// This is an implementation of <see cref="IFhirSerializationEngine"/> which uses the
/// new Poco-based parser and serializer, initialized with the default settings.
/// </summary>
public class PocoSerializationEngine(BaseFhirJsonDeserializer jsonDeserializer, BaseFhirJsonSerializer jsonSerializer,
    BaseFhirXmlDeserializer xmlDeserializer, BaseFhirXmlSerializer xmlSerializer) : IFhirSerializationEngine
{
    /// <inheritdoc />
    public string SerializeToJson(Resource instance) => jsonSerializer.SerializeToString(instance);

    /// <inheritdoc />
    public Resource? DeserializeFromJson(string data) => jsonDeserializer.DeserializeResource(data);

    /// <inheritdoc />
    public Resource? DeserializeFromXml(string data) => xmlDeserializer.DeserializeResource(data);

    /// <inheritdoc />
    public string SerializeToXml(Resource instance) => xmlSerializer.SerializeToString(instance);
}