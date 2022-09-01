using Firely.Fhir.Packages;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.IO;
using System.Text;

namespace Hl7.Fhir.Specification.Tests
{
    public sealed class PackageResolver : IResourceResolver
    {
        private ParserSettings _parserSettings;

        public PackageResolver(PackageContext context, ParserSettings parserSettings)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ParserSettings = parserSettings;
        }

        public PackageContext Context { get; }

        public ParserSettings ParserSettings
        {
            get => _parserSettings;
            set => _parserSettings = value ?? ParserSettings.CreateDefault();
        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            try
            {
                var content = Context.GetFileContentByCanonical(uri).Result;
                var pathName = GetFileNameByCanonical(uri);

                return content == null || pathName == null
                    ? null
                    : LoadByContent(pathName, content);
            }
            catch
            {
                // GetFileContentByCanonical can throw an exception when the file fhirpkg.lock.json does not exist.
                // Ignore exceptions an return null to indicate package was not found.
                return null;
            }
        }

        public Resource ResolveByUri(string uri)
        {
            var (resource, id) = Splice(uri, '/');

            try
            {
                var content = Context.GetFileContentById(resource, id).WaitResult();

                return content == null
                    ? null
                    : LoadByContent(null, content);
            }
            catch
            {
                return null;
            }
        }

        public string GetFileNameByCanonical(string uri)
        {
            var reference = Context.GetFileReferenceByCanonical(uri);

            return reference != null
                ? Path.Combine(packageContentFolder(reference.Package), reference.FileName)
                : null;
        }

        private static string packageContentFolder(PackageReference reference)
        {
            return Path.Combine(Platform.GetFhirPackageRoot(), packageFolderName(reference), PackageFileNames.PACKAGEFOLDER);
        }

        private static string packageFolderName(PackageReference reference, char glue = '#')
        {
            return reference.Name + glue + reference.Version;
        }

        public static (string left, string right) Splice(string s, char separator)
        {
            var strArray = s.Split(new[] { separator }, 2);
            return (strArray.Length >= 1 ? strArray[0] : null, strArray.Length >= 2 ? strArray[1] : null);
        }

        public static Resource LoadByContent(string filePath, string content) => loadResource(filePath, () => new MemoryStream(Encoding.UTF8.GetBytes(content)));

        private static Resource loadResource(string filePath, Func<Stream> streamFactory)
        {
            using var stream = streamFactory();
            Resource model = null;

            if (FhirFileFormats.HasXmlExtension(filePath))
            {
                model = LoadResourceFromXmlStream(stream);
                model.SetOrigin(filePath);
            }

            if (FhirFileFormats.HasJsonExtension(filePath))
            {
                model = LoadResourceFromJsonStream(stream);
                model.SetOrigin(filePath);
            }

            return model;
        }

        public static Resource LoadResourceFromXmlStream(Stream stream) => LoadResourceFromXmlStream<Resource>(stream);
        public static Resource LoadResourceFromJsonStream(Stream stream) => LoadResourceFromJsonStream<Resource>(stream);

        public static T LoadResourceFromXmlStream<T>(Stream stream) where T : Resource
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            using var xmlReader = SerializationUtil.XmlReaderFromStream(stream);

            var node = FhirXmlNode.Read(xmlReader, CreateDefaultXmlParserSettings());
            var resource = node.ToPoco<T>(CreateDefaultPocoBuilderSettings);
            
            return resource;
        }

        public static T LoadResourceFromJsonStream<T>(Stream stream) where T : Resource
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            using var jsonReader = SerializationUtil.JsonReaderFromStream(stream);

            var node = FhirJsonNode.Read(jsonReader, null, CreateDefaultJsonParserSettings());
            var resource = node.ToPoco<T>(CreateDefaultPocoBuilderSettings);

            return resource;
        }

        public static FhirXmlParsingSettings CreateDefaultXmlParserSettings()
            => new()
            {
                PermissiveParsing = true,
                ValidateFhirXhtml = false,
                DisallowSchemaLocation = false
            };

        public static FhirJsonParsingSettings CreateDefaultJsonParserSettings()
            => new()
            {
                PermissiveParsing = true,
                ValidateFhirXhtml = false,
                AllowJsonComments = true
            };

        public static PocoBuilderSettings CreateDefaultPocoBuilderSettings
            => new()
            {
                IgnoreUnknownMembers = true,
                AllowUnrecognizedEnums = true
            };
    }
}
