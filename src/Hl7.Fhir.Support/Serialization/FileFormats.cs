using System;
using System.IO;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Provides generic helper methods for different FHIR file serialization formats.
    /// </summary>
    public static class FileFormats
    {
        static StringComparer ExtensionComparer = StringComparer.OrdinalIgnoreCase;

        /// <summary>Default Xml file extension: ".xml"</summary>
        public const string XmlFileExtension = ".xml";

        /// <summary>Default Json file extension: ".json"</summary>
        public const string JsonFileExtension = ".json";

        /// <summary>Determines if the file extension equals ".xml" (case insensitive).</summary>
        public static bool HasXmlExtension(string filePath) => HasExtension(filePath, XmlFileExtension);

        /// <summary>Determines if the file extension equals ".json" (case insensitive).</summary>
        public static bool HasJsonExtension(string filePath) => HasExtension(filePath, JsonFileExtension);

        /// <summary>Determines if the file extension equals ".xml" or ".json" (case insensitive).</summary>
        public static bool HasXmlOrJsonExtension(string filePath)
        {
            var ext = Path.GetExtension(filePath);
            return ExtensionComparer.Equals(ext, XmlFileExtension)
                || ExtensionComparer.Equals(ext, JsonFileExtension);
        }

        /// <summary>Determines if the file extension equals the specified value (case insensitive).</summary>
        public static bool HasExtension(string filePath, string extension)
            => ExtensionComparer.Equals(Path.GetExtension(filePath), extension);

    }
}
