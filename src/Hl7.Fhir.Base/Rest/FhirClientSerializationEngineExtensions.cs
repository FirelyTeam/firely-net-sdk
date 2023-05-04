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
        /// Configures the FhirClient to use the legacy serialization behaviour, using the <see cref="FhirClientSettings.ParserSettings"/>.
        /// </summary>
        public static BaseFhirClient WithLegacySerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine =
                FhirSerializationEngineFactory.Legacy.FromParserSettings(client.Inspector, client.Settings.ParserSettings ?? new());
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to parse the incoming data strictly.
        /// </summary>
        public static BaseFhirClient WithStrictSerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Strict(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the legacy serializer, configured to parse the incoming data strictly.
        /// </summary>
        public static BaseFhirClient WithStrictLegacySerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Legacy.Strict(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to ignore recoverable errors in the
        /// the incoming data.
        /// </summary>
        public static BaseFhirClient WithLenientSerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Recoverable(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the legacy serializer, configured to be permissive.
        /// </summary>
        public static BaseFhirClient WithPermissiveLegacySerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Legacy.Permissive(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to ignore errors that can
        /// be caused when encountering data from a newer version of FHIR. NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithBackwardsCompatibleSerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.BackwardsCompatible(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the legacy serializer, configured to ignore errors that can
        /// be caused when encountering data from a newer version of FHIR. NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithBackwardsCompatibleLegacySerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Legacy.BackwardsCompatible(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the newer POCO-based serializer, configured to ignore all errors.
        /// NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithOstrichModeSerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Ostrich(client.Inspector);
            return client;
        }

        /// <summary>
        /// Configures the FhirClient to use the legacy serializer, configured to ignore all errors.
        /// NB: This may cause data loss.
        /// </summary>
        public static BaseFhirClient WithOstrichModeLegacySerializer(this BaseFhirClient client)
        {
            client.Settings.SerializationEngine = FhirSerializationEngineFactory.Legacy.Ostrich(client.Inspector);
            return client;
        }
    }
}

#nullable restore

