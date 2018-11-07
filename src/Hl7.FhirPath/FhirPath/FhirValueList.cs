/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath
{
    internal static class FhirValueList
    {
        //todo: object can now be ITypedElement? --mh
        public static IEnumerable<ITypedElement> Create(params object[] values)
        {
            if (values != null)
            {
                return values.Select(value => value == null ? null : value is ITypedElement ? (ITypedElement)value : new ConstantValue(value));
            }
            else
                return FhirValueList.Empty;
        }

        public static readonly IEnumerable<ITypedElement> Empty = Enumerable.Empty<ITypedElement>();
    }
}