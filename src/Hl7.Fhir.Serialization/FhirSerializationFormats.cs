/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

namespace Hl7.Fhir.Serialization
{
    /// <summary>String constants that represent FHIR resource serialization formats supported by the API.</summary>
    /// <seealso cref="FhirFileFormats"/>
    public class FhirSerializationFormats
    {
        /// <summary>Represents the FHIR XML resource serialization format.</summary>
        public const string Xml = "xml";

        /// <summary>Represents the FHIR JSON resource serialization format.</summary>
        public const string Json = "json";

        // <summary>Represents the FHIR RDF resource serialization format.</summary>
        // public const string Rdf = "rdf";


        /// <summary>Returns an array of all defined serialization formats.</summary>
        public static readonly string[] All = new[]
        {
            Xml,
            Json,
            // Rdf
        };
    }
}
