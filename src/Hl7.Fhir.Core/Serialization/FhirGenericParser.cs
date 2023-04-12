/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public class FhirGenericParser : BaseFhirParser
    {
        public FhirGenericParser(Model.Version version) : base(version)
        {}

        public FhirGenericParser(ParserSettings settings) : base(settings)
        {}

        public T Parse<T>(IParserOrigin origin) where T : Base => (T)Parse(origin, typeof(T));

        public Base Parse(IParserOrigin origin, Type dataType = null)
        {
            var source = new ParserSource(origin, Settings);
            try
            {
                return source.GetRoot(dataType);
            }
            catch (SourceException sourceException)
            {
                throw sourceException.ToFormatException();
            }
        }

        /// <summary>
        /// Name of the special 'element' containing the value of primitive data types (FhriString, FhirBoolean, FhirDateTime etc.)
        /// </summary>
        public const string PrimitiveValueElementName = "value";
    }

}
