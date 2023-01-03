#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Rest
{
    internal class FhirClientElementModelSerializationEngine : IFhirSerializationEngine
    {
        private readonly ModelInspector _inspector;
        private readonly Func<ParserSettings?> _settingsRetriever;

        public FhirClientElementModelSerializationEngine(ModelInspector inspector, Func<ParserSettings?> settingsRetriever)
        {
            _inspector = inspector;
            _settingsRetriever = settingsRetriever;
        }

        public Resource DeserializeFromXml(string data)
        {
            var settings = BaseFhirParser.BuildPocoBuilderSettings(_settingsRetriever() ?? ParserSettings.CreateDefault());

            try
            {
                return (Resource)FhirXmlNode.Parse(data).ToPoco(_inspector, null, settings);
            }
            catch(FormatException fe)
            {
                throw new DeserializationFailedException(null, new[] { new ElementModelParserException(fe) });
            }
        }

        public Resource DeserializeFromJson(string data)
        {
            var settings = BaseFhirParser.BuildPocoBuilderSettings(_settingsRetriever() ?? ParserSettings.CreateDefault());

            try
            {
                return (Resource)FhirJsonNode.Parse(data).ToPoco(_inspector, null, settings);
            }
            catch (FormatException fe)
            {
                throw new DeserializationFailedException(null, new[] { new ElementModelParserException(fe) });
            }
        }

        public string SerializeToXml(Resource instance) => new CommonFhirXmlSerializer(_inspector).SerializeToString(instance);

        public string SerializeToJson(Resource instance) => new CommonFhirJsonSerializer(_inspector).SerializeToString(instance);

        internal class ElementModelParserException : CodedException
        {
            public const string ELEMENTMODEL_PARSER_EXCEPTION = "EMP001";

            public ElementModelParserException(FormatException fe) : base(ELEMENTMODEL_PARSER_EXCEPTION, fe.Message, fe)
            {
                // Nothing
            }
        }
    }
}

#nullable restore