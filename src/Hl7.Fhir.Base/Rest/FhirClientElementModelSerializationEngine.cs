#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;

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

        public static bool TryUnpackElementModelException(DeserializationFailedException dfe, out FormatException? fe)
        {
            if(dfe.Exceptions.Count == 1 && dfe.Exceptions.Single() is FhirClientElementModelSerializationEngine.ElementModelParserException empe)
            {
                fe = (FormatException)empe.InnerException!;
                return true;
            }
            else
            {
                fe = null;
                return false;
            }
        }

        public Resource? DeserializeFromXml(string data, out DeserializationFailedException? report) =>
            deserialize(() => FhirXmlNode.Parse(data), out report);

        public Resource? DeserializeFromJson(string data, out DeserializationFailedException? report) =>
            deserialize(() => FhirJsonNode.Parse(data), out report);

        private Resource? deserialize(Func<ISourceNode> deserializer, out DeserializationFailedException? report)
        {
            var settings = BaseFhirParser.BuildPocoBuilderSettings(_settingsRetriever() ?? ParserSettings.CreateDefault());

            try
            {
                report = null;
                return deserializer().ToPoco(_inspector, null, settings) as Resource;
            }
            catch (FormatException fe)
            {
                report = new DeserializationFailedException(null, new ElementModelParserException(fe));
                return null;
            }
            catch (InvalidOperationException ioe)
            {
                // Unfortunately, our ElementModel parsers treats resources without a type indicator with an InvalidOperationException.
                // This should have been a parsing error, so let's turn it into one.
                var fmt = new FormatException(ioe.Message);
                report = new DeserializationFailedException(null, new ElementModelParserException(fmt));
                return null;
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