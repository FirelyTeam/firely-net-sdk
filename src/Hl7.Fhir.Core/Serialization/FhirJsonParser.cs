/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;


namespace Hl7.Fhir.Serialization
{
    public class FhirJsonParser : BaseFhirParser
    {
        public FhirJsonParser(ParserSettings settings = null) : base(settings)
        {
            //
        }

        public T Parse<T>(string json) where T : Base => (T)Parse(json, typeof(T));

        public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));
        
        private static FhirJsonParsingSettings buildNodeSettings(ParserSettings settings) =>
                new FhirJsonParsingSettings
                {
                    // TODO: True for DSTU2, should be false in STU3
                    AllowJsonComments = false,
                    PermissiveParsing = settings.PermissiveParsing
                };

        public Base Parse(string json, Type dataType = null)
        {
            var rootName = dataType != null ? ModelInfo.GetFhirTypeNameForType(dataType) : null;
            var jsonReader = FhirJsonNode.Parse(json, rootName, buildNodeSettings(Settings));
            return Parse(jsonReader, dataType);
        }

        public Base Parse(JsonReader reader, Type dataType = null)
        {
            var rootName = dataType != null ? ModelInfo.GetFhirTypeNameForType(dataType) : null;
            var jsonReader = FhirJsonNode.Read(reader, rootName, buildNodeSettings(Settings));
            return Parse(jsonReader, dataType);
        }
    }
}
