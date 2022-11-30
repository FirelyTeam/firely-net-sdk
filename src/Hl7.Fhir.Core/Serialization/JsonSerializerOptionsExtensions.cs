using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public static class JsonSerializerOptionsExtensions
    {
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, Model.Version version)
            => ForFhirPrimitive(options, new ParserSettings(version));

        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, ParserSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            return ForFhirPrimitive(options, settings.Clone());
        }

        private static JsonSerializerOptions ForFhirPrimitive(this JsonSerializerOptions options, ParserSettings settings)
        {
            var result = new JsonSerializerOptions(options);
            result.Converters.Add(new FhirJsonConverter(settings));
            if (settings.PermissiveParsing)
            {
                // The old parser always allowed commas after the last element in an array or object, here we do that only in PermissiveParsing mode
                result.AllowTrailingCommas = true;
            }
            return result;
        }
    }

    internal class FhirJsonConverter : JsonConverter<Resource>
    {
        public FhirJsonConverter(ParserSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Resource).IsAssignableFrom(typeToConvert);
        }

        public override Resource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var source = new JsonSource(ref reader, _settings);
            try
            {
                var result = source.GetResource(typeToConvert);
                if (!typeToConvert.IsAssignableFrom(result.GetType()))
                {
                    throw source.CreateWrongResourceTypeException(typeToConvert, result);
                }
                source.GetReader(ref reader);
                return result;
            }
            catch (JsonSourceException jsonSourceException)
            {
                throw new JsonException(jsonSourceException.Message, jsonSourceException.Path, jsonSourceException.LineNumber, jsonSourceException.BytePositionInLine);
            }
        }

        public override void Write(Utf8JsonWriter writer, Resource value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private readonly ParserSettings _settings;
    }
}
