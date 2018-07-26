/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Serialization
{
    public class MissingTypeInformationException : Exception
    {
        public MissingTypeInformationException() { }
        public MissingTypeInformationException(string message) : base(message) { }
        public MissingTypeInformationException(string message, Exception inner) : base(message, inner) { }
    }
}
