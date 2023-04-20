#nullable enable

/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// A set of extension methods to configure the serialization/deserialization engine used by the client to some common defaults.
    /// </summary>
    public static class FhirClientSerializationEngineExtensions
    {
        /// <summary>
        /// Configures the FhirClient to use the original serialization behaviour, using the <see cref="FhirClientSettings.ParserSettings"/>.
        /// </summary>
        public static BaseFhirClient WithOriginalDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = 
                FhirSerializationEngineFactory.ElementModel.FromParserSettings(client.Inspector, client.Settings.ParserSettings ?? new());
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to parse the incoming data strictly.
        /// </summary>
        public static BaseFhirClient WithStrictPocoDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Poco.Strict(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the original serializer, configured to parse the incoming data strictly.
        /// </summary>
        public static BaseFhirClient WithStrictOriginalDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.ElementModel.Strict(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to ignore recoverable errors in the
        /// the incoming data.
        /// </summary>
        public static BaseFhirClient WithLenientPocoDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Poco.Recoverable(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the original serializer, configured to be permissive.
        /// </summary>
        public static BaseFhirClient WithPermissiveOriginalDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.ElementModel.Permissive(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to ignore errors that can
        /// be caused when encountering data from a newer version of FHIR. NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithBackwardsCompatiblePocoDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Poco.BackwardsCompatible(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the original serializer, configured to ignore errors that can
        /// be caused when encountering data from a newer version of FHIR. NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithBackwardsCompatibleOriginalDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.ElementModel.BackwardsCompatible(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to ignore all errors.
        /// NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithOstrichModePocoDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Poco.Ostrich(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the original serializer, configured to ignore all errors.
        /// NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithOstrichModeOriginalDeserializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.ElementModel.Ostrich(client.Inspector);
            return client;
        }
    }
}

#nullable restore

