/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Validation
{
    public class IncorrectElementDefinitionException : Exception
    {
        public IncorrectElementDefinitionException(string message) : base(message)
        {
        }

        public IncorrectElementDefinitionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
