using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Properties;
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
        private IFhirWriter _current;
        private ModelInspector _inspector;

        public PrimitiveValueWriter(IFhirWriter data)
        {
            _current = data;
            _inspector = SerializationConfig.Inspector;
        }


        internal void Serialize(object value, XmlSerializationHint hint)
        {
            if (value != null)
            {
                var nativeType = value.GetType();

                if (nativeType.IsEnum)
                {
                    var enumMapping = _inspector.FindEnumMappingByType(nativeType);

                    if (enumMapping != null)
                        value = enumMapping.GetLiteral((Enum)value);
                }
            }

            _current.WritePrimitiveContents(value, hint);
        }
    }

}
