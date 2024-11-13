/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Extension methods for the <see cref="IFhirSerializationEngine"/> interface when the underlying engine is NOT a legacy engine.
/// </summary>
public static class SerializationEngineExtensions
{
    /// <summary>
    /// Deserialize a FHIR Resource from a <see cref="Utf8JsonReader"/>.
    /// </summary>
    /// <returns>The resource, or null if the operation failed</returns>
    /// <exception cref="InvalidOperationException">Thrown if the underlying engine is a legacy engine</exception>
    /// <exception cref="DeserializationFailedException">Thrown if a FHIR error was encountered in the data</exception>
    public static Resource DeserializeFromJsonReader(this IFhirSerializationEngine engine, ref Utf8JsonReader reader)
    {
        if (engine is not PocoSerializationEngine pse)
        {
            throw new InvalidOperationException("stream reading is not supported by legacy engines");
        }

        return pse.DeserializeFromJson(ref reader);
    }

    /// <summary>
    /// Deserialize a FHIR Resource from an <see cref="XmlReader"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the underlying engine is a legacy engine</exception>
    /// <exception cref="DeserializationFailedException">Thrown if a FHIR error was encountered in the data</exception>
    public static Resource DeserializeFromXmlReader(this IFhirSerializationEngine engine, XmlReader reader)
    {
        if (engine is not PocoSerializationEngine pse)
        {
            throw new InvalidOperationException("stream reading is not supported by legacy engines");
        }

        return pse.DeserializeFromXml(reader);
    }

    /// <summary>
    /// Serialize a FHIR Resource to a JSON writer.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static void SerializeToJsonWriter(this IFhirSerializationEngine engine, Resource instance, Utf8JsonWriter writer)
    {
        if (engine is not PocoSerializationEngine pse)
        {
            throw new InvalidOperationException("stream writing is not supported by legacy engines");
        }

        pse.SerializeToJsonWriter(instance, writer);
    }

    /// <summary>
    /// Serialize a FHIR Resource to an <see cref="XmlWriter"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the underlying engine is a legacy engine</exception>
    public static void SerializeToXmlWriter(this IFhirSerializationEngine engine, Resource instance, XmlWriter writer)
    {
        if (engine is not PocoSerializationEngine pse)
        {
            throw new InvalidOperationException("stream writing is not supported by legacy engines");
        }

        pse.SerializeToXmlWriter(instance, writer);
    }

    /// <summary>
    /// Serialize an element (a child of a resource) to a <see cref="Utf8JsonWriter"/>.
    /// </summary>
    /// <remarks>This method allows you to serialize
    /// a subtree of data to XML, which can be useful for debug and display purposes. Note that the
    /// FHIR standard does not prescribe how this is done, so the output of this function is by definition
    /// non standard and should not be used for production purposes.</remarks>
    public static void SerializeElementToXmlWriter(this IFhirSerializationEngine engine, Base instance, XmlWriter writer)
    {
        if (engine is not PocoSerializationEngine pse)
        {
            throw new InvalidOperationException("stream writing is not supported by legacy engines");
        }

        pse.SerializeToXmlWriter(instance, writer);
    }

    /// <summary>
    /// Serialize an element (a child of a resource) to a string of Xml.
    /// </summary>
    /// <remarks>This method allows you to serialize
    /// a subtree of data to XML, which can be useful for debug and display purposes. Note that the
    /// FHIR standard does not prescribe how this is done, so the output of this function is by definition
    /// non standard and should not be used for production purposes.</remarks>
    public static void SerializeElementToXml(this IFhirSerializationEngine engine, Base instance)
    {
        if (engine is not PocoSerializationEngine pse)
        {
            throw new InvalidOperationException("stream writing is not supported by legacy engines");
        }

        pse.SerializeElementToXml(instance);
    }
}