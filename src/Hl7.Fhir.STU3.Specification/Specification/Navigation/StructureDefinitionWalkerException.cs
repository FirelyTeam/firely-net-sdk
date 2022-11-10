/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Specification
{
    public class StructureDefinitionWalkerException : InvalidOperationException
    {
        public StructureDefinitionWalkerException()
        {
        }

        public StructureDefinitionWalkerException(string message) : base(message)
        {
        }

        public StructureDefinitionWalkerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
