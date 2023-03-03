/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

namespace Hl7.Fhir.Serialization
{
    /// <summary>String constants that represent FHIR resource serialization formats supported by the API.</summary>
    /// <seealso cref="FhirFileFormats"/>
    public class FhirSerializationFormats
    {
        /// <summary>Represents the FHIR XML resource serialization format.</summary>
#pragma warning disable IDE1006 // Naming Styles
        public const string Xml = "xml";
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>Represents the FHIR JSON resource serialization format.</summary>
#pragma warning disable IDE1006 // Naming Styles
        public const string Json = "json";
#pragma warning restore IDE1006 // Naming Styles

        // <summary>Represents the FHIR RDF resource serialization format.</summary>
        // public const string Rdf = "rdf";


        /// <summary>Returns an array of all defined serialization formats.</summary>
#pragma warning disable IDE1006 // Naming Styles
        public static readonly string[] All = new[]
#pragma warning restore IDE1006 // Naming Styles
        {
            Xml,
            Json,
            // Rdf
        };
    }
}
