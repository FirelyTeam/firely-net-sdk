/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;

namespace Hl7.Fhir.Serialization
{
    public class ParserSettings
    {
        [Obsolete("Due to a bug, the Default has always been ignored, so it is now officially deprecated")]
        public static readonly ParserSettings Default = new ParserSettings() { AcceptUnknownMembers = false, AllowUnrecognizedEnums = false, DisallowXsiAttributesOnRoot = true };

        public bool DisallowXsiAttributesOnRoot { get; set; }

        public bool AllowUnrecognizedEnums { get; set; }
        public bool AcceptUnknownMembers { get; set; }

        public ParserSettings Clone() =>
            new ParserSettings
            {
                DisallowXsiAttributesOnRoot = DisallowXsiAttributesOnRoot,
                AllowUnrecognizedEnums = AllowUnrecognizedEnums,
                AcceptUnknownMembers = AllowUnrecognizedEnums
            };
    }
}
