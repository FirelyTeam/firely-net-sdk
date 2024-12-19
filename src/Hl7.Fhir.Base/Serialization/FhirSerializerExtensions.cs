/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization;

public static class FhirSerializerExtensions
{
    /// <summary>
    /// Serializes the given POCO into a FHIR Json string.
    /// </summary>
    public static string SerializeToString(this BaseFhirJsonPocoSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteJsonToString(w => ser.Serialize(element, w, filter), pretty);

    /// <summary>
    /// Serializes the given POCO into a FHIR Xml string.
    /// </summary>
    public static string SerializeToString(this BaseFhirXmlPocoSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteXmlToString(w => ser.Serialize(element, w, filter), pretty);

    /// <summary>
    /// Serializes the given POCO into a FHIR Json byte array.
    /// </summary>
    public static byte[] SerializeToBytes(this BaseFhirJsonPocoSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteJsonToBytes(w => ser.Serialize(element, w, filter), pretty);

    /// <summary>
    /// Serializes the given POCO into a FHIR Xml byte array.
    /// </summary>
    public static byte[] SerializeToBytes(this BaseFhirXmlPocoSerializer ser, Base element, bool pretty = false,
        SerializationFilter? filter = null) =>
        SerializationUtil.WriteXmlToBytes(w => ser.Serialize(element, w, filter), pretty);
}