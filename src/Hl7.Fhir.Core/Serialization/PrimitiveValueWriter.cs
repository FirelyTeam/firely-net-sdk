/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;


namespace Hl7.Fhir.Serialization
{
    internal class PrimitiveValueWriter
    {
        private IFhirWriter _writer;
        private ModelInspector _inspector;

        public PrimitiveValueWriter(IFhirWriter data)
        {
            _writer = data;
            _inspector = SerializationConfig.Inspector;
        }


        internal void Serialize(object value, XmlSerializationHint hint)
        {
            if (value != null)
            {
                var nativeType = value.GetType();

                if (nativeType.IsEnum())
                {
                    var enumMapping = _inspector.FindEnumMappingByType(nativeType);

                    if (enumMapping != null)
                        value = enumMapping.GetLiteral((Enum)value);
                }
            }

            _writer.WritePrimitiveContents(value, hint);
        }
    }

}
