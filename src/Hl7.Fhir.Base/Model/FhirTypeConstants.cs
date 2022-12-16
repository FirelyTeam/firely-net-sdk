/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

namespace Hl7.Fhir.Support.Poco
{
    /// <summary>
    /// List of Fhir types constants. This will be removed when we introduce ModelSpace, but for now we will use this.
    /// </summary>
    internal class FhirTypeConstants
    {
        // primitive types
        public const string BOOLEAN = "boolean";
        public const string INTEGER = "integer";
        public const string INTEGER64 = "integer64";
        public const string UNSIGNED_INT = "unsignedInt";
        public const string POSITIVE_INT = "positiveInt";
        public const string TIME = "time";
        public const string DATE = "date";
        public const string INSTANT = "instant";
        public const string DATE_TIME = "dateTime";
        public const string DECIMAL = "decimal";
        public const string STRING = "string";
        public const string CODE = "code";
        public const string ID = "id";
        public const string URI = "uri";
        public const string OID = "oid";
        public const string UUID = "uuid";
        public const string CANONICAL = "canonical";
        public const string URL = "url";
        public const string MARKDOWN = "markdown";
        public const string BASE64_BINARY = "base64Binary";

        // General-Purpose Data types
        public const string CODING = "Coding";
        public const string CODEABLE_CONCEPT = "CodeableConcept";
        public const string QUANTITY = "Quantity";

        // Special Purpose Data types
        public const string EXTENSION = "Extension";
        public const string REFERENCE = "Reference";
        public const string XHTML = "xhtml";

        // Resource type
        public const string BUNDLE = "Bundle";
    }
}