/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using Hl7.ElementModel;

namespace Hl7.FluentPath
{
    public static class FhirValueList
    {
        public static IEnumerable<IValueProvider> Create(params object[] values)
        {
            if (values != null)
            {
                return values.Select(value => value == null ? null : value is IValueProvider ? (IValueProvider)value : new ConstantValue(value));
            }
            else
                return FhirValueList.Empty;
        }

        public static readonly IEnumerable<IValueProvider> Empty = Enumerable.Empty<IValueProvider>();
    }
}