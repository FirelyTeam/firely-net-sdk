/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.FhirPath
{
    internal class ConstantValueProvider : IValueProvider
    {
        public ConstantValueProvider(object value)
        {
            ObjectValue = value;
        }

        public object ObjectValue { get; private set; }

        public Type ValueType
        {
            get
            {
                return ObjectValue != null ? ObjectValue.GetType() : null;
            }
        }
    }
}
