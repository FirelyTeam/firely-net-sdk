/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.FhirPath
{
    public enum ValueType
    {
        Unknown,
        Boolean,
        String, 
        Integer,
        Decimal,
        DateTime
    }


    public interface IFhirPathValue
    {
        ValueType Type { get;  }

        object Value { get; }
    }

    public interface IFhirPathElement : IFhirPathValue
    {
        IFhirPathElement Parent { get; }

        IEnumerable<IFhirPathElement> Children();
        bool HasChildren();

        string Name { get; }
    }
}