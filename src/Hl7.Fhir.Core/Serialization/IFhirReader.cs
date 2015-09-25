/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    public interface IFhirReader : IPostitionInfo
    {
        string GetResourceTypeName();

        IEnumerable<Tuple<string, IFhirReader>> GetMembers();

        object GetPrimitiveValue();
    }
}
