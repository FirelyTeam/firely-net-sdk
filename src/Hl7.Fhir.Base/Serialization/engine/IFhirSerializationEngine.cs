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

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Represents an object that can serialize/deserialize FHIR data from the supported
    /// serialization formats.
    /// </summary>
    public interface IFhirSerializationEngine
    {
        /// <summary>
        /// Deserialize an XML string to a FHIR Resource POCO.
        /// </summary>
        /// <exception cref="DeserializationFailedException">Thrown when the deserializer encountered one or more errors in the FHIR Xml format.</exception>
        /// <returns>Null if the data did not contain a resource, but another FHIR datatype.</returns>
        public Resource DeserializeFromXml(string data);

        /// <summary>
        /// Deserialize a Json string to a FHIR Resource POCO.
        /// </summary>
        /// <exception cref="DeserializationFailedException">Thrown when the deserializer encountered one or more errors in the FHIR Json format.</exception>
        public Resource DeserializeFromJson(string data);

        /// <summary>
        /// Serialize a FHIR Resource POCO into a string of Xml.
        /// </summary>
        public string SerializeToXml(Resource instance);

        /// <summary>
        /// Serialize a FHIR Resource POCO into a string of Json.
        /// </summary>
        public string SerializeToJson(Resource instance);
    }


    /// <summary>
    /// Factory methods for creating the default implementation of <see cref="IFhirSerializationEngine"/>, as used by the
    /// FhirClient.
    /// </summary>
    public static class FhirSerializationEngineFactory
    {
        /// <summary>
        /// A named scope for the factory methods that use the ElementModel deserializers.
        /// </summary>
        public static class ElementModel
        {
            /// <summary>
            /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the "old" TypedElement-based parser and serializer
            /// using <see cref="ParserSettings.PermissiveParsing"/> set to <c>true</c>.
            /// </summary>
            public static IFhirSerializationEngine Permissive(ModelInspector inspector) =>
                new ElementModelSerializationEngine(inspector, ParserSettings.CreateDefault());

            /// <summary>
            /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the "old" TypedElement-based parser and serializer
            /// with <see cref="ParserSettings.PermissiveParsing"/> set to <c>false</c>.
            /// </summary>
            public static IFhirSerializationEngine Strict(ModelInspector inspector) =>
                new ElementModelSerializationEngine(inspector, new ParserSettings { PermissiveParsing = false });

            /// <summary>
            /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to allow errors that 
            /// could occur when reading data from newer releases of FHIR. Note that this parser may drop data
            /// that cannot be captured in the POCO model, such as new elements in future FHIR releases.
            /// </summary>
            public static IFhirSerializationEngine BackwardsCompatible(ModelInspector inspector) =>
                new ElementModelSerializationEngine(inspector, new ParserSettings {  AcceptUnknownMembers = true, AllowUnrecognizedEnums = true });
        }

        /// <summary>
        /// A named scope for the factory methods that use the Poco deserializers.
        /// </summary>
        public static class Poco
        {           
            /// <summary>
            /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to flag all parsing errors, 
            /// which uses the new Poco-based parser and serializer.
            /// </summary>
            /// <param name="inspector"></param>
            /// <returns></returns>
            public static IFhirSerializationEngine Strict(ModelInspector inspector) => new PocoSerializationEngine(inspector);

            /// <summary>
            /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to ignore recoverable errors, 
            /// which uses the new Poco-based parser and serializer.
            /// </summary>
            /// <param name="inspector"></param>
            /// <returns></returns>
            public static IFhirSerializationEngine Recoverable(ModelInspector inspector) => 
                new PocoSerializationEngine(inspector, isRecoverableIssue);

            /// <summary>
            /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to allow errors that 
            /// could occur when reading data from newer releases of FHIR. Note that this parser may drop data
            /// that cannot be captured in the POCO model, such as new elements in future FHIR releases.
            /// </summary>
            public static IFhirSerializationEngine BackwardsCompatible(ModelInspector inspector) => 
                new PocoSerializationEngine(inspector, isAllowedForBackwardsCompatibility);

            private static bool isRecoverableIssue(CodedException ce) =>
              ce switch
              {
                  FhirXmlException xmle => FhirXmlException.IsRecoverableIssue(xmle),
                  FhirJsonException jsone => FhirJsonException.IsRecoverableIssue(jsone),
                  _ => false
              };

            private static bool isAllowedForBackwardsCompatibility(CodedException ce) =>
               FhirXmlException.AllowedForBackwardsCompatibility(ce) || FhirJsonException.AllowedForBackwardsCompatibility(ce);
        }
    }
}

#nullable restore
