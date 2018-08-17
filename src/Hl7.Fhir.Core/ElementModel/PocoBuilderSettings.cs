/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */



namespace Hl7.Fhir.Serialization
{
    public class PocoBuilderSettings
    {
        public bool AllowUnrecognizedEnums { get; set; }
        public bool IgnoreUnknownMembers { get; set; }

        public PocoBuilderSettings Clone() =>
            new PocoBuilderSettings
            {
                AllowUnrecognizedEnums = AllowUnrecognizedEnums,
                IgnoreUnknownMembers = IgnoreUnknownMembers
            };
    }
}
