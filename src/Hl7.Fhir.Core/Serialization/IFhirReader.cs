/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    [Obsolete("Use IElementNavigator interface in the Hl7.Fhir.Support assembly instead, and pass it to a Parse() overload which accepts IElementNavigator")]
    public interface IFhirReader : IPositionInfo
    {
        string GetResourceTypeName();

        IEnumerable<Tuple<string, IFhirReader>> GetMembers();

        object GetPrimitiveValue();
    }
}
