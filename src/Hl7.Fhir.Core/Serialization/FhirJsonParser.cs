/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;


namespace Hl7.Fhir.Serialization
{
    public class FhirJsonParser : BaseFhirParser
    {
        public FhirJsonParser(ParserSettings settings=null) : base(settings)
        {
            //
        }

        public T Parse<T>(string json) where T : Base => (T)Parse(json, typeof(T));

        public T Parse<T>(JsonReader reader) where T : Base => (T)Parse(reader, typeof(T));

        // TODO: True for DSTU2, should be false in STU3
        private readonly FhirJsonNodeSettings jsonNodeSettings = new FhirJsonNodeSettings { AllowJsonComments = true };

        public Base Parse(string json, Type dataType)
        {
            var jsonReader =
                FhirJsonNode.Parse(json, ModelInfo.GetFhirTypeNameForType(dataType), jsonNodeSettings);
            return Parse(jsonReader, dataType);
        }

        public Base Parse(JsonReader reader, Type dataType)
        {
            var jsonReader =
                FhirJsonNode.Read(reader, ModelInfo.GetFhirTypeNameForType(dataType), jsonNodeSettings);
            return Parse(jsonReader, dataType);
        }
    }

}
