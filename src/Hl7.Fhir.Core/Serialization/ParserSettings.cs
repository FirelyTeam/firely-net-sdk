/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
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
        public static readonly ParserSettings Default = new ParserSettings() { AcceptUnknownMembers = false, AllowUnrecognizedEnums = false, DisallowXsiAttributesOnRoot = true };

        public bool AcceptUnknownMembers { get; set; }

        public bool DisallowXsiAttributesOnRoot { get; set; }

        public bool AllowUnrecognizedEnums { get; set; }
    }
}
