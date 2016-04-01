/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.FluentPath
{
    public interface IFluentPathValue
    {
        object Value { get; }
    }

    public interface IFluentPathElement : IFluentPathValue
    {
        // IFluentPathElement Parent { get; }
        // IEnumerable<ChildNode> Children();

        IEnumerable<string> GetChildNames();
        IEnumerable<IFluentPathElement> GetChildrenByName(string name);
    }

    public static class FhirValueList
    {
        public static IEnumerable<IFluentPathValue> Create(params object[] values)
        {
            if (values != null)
            {
                return values.Select(value => value == null ? null : value is IFluentPathValue ? (IFluentPathValue)value : new TypedValue(value));
            }
            else
                return FhirValueList.Empty();
        }

        public static IEnumerable<IFluentPathValue> Empty()
        {
            return Enumerable.Empty<IFluentPathValue>();
        }
    }

}