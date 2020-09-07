/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    internal static class ParametersExtensions
    {
        public static void AddParameterComponent(this Parameters parameters, string name, DataType value)
        {
            parameters.Parameter.Add(new Parameters.ParameterComponent
            {
                Name = name,
                Value = value,
            });
        }

        public static void AddParameterComponent(this Parameters parameters, string name, Resource resource)
        {
            parameters.Parameter.Add(new Parameters.ParameterComponent
            {
                Name = name,
                Resource = resource,
            });
        }
    }
}
