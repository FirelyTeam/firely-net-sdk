/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using System;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// An Exception raised while turning an ISourceNode into an ITypedElement.
    /// </summary>
    /// <remarks>For backwards compatibility reasons, this exception is also the base of
    /// DeserializationFailedException, so that existing catch blocks will still catch.</remarks>
    public class StructuralTypeException : FormatException
    {
        public StructuralTypeException() { }
        public StructuralTypeException(string message) : base(message) { }
        public StructuralTypeException(string message, Exception inner) : base(message, inner) { }
    }

}