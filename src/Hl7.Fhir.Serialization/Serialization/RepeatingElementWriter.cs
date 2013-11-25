using Hl7.Fhir.Introspection;
using Hl7.Fhir.Properties;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.Serialization
{
    public class RepeatingElementWriter
    {
        private IFhirWriter _current;
        private ModelInspector _inspector;

        public RepeatingElementWriter(IFhirWriter writer)
        {
            _current = writer;
            _inspector = SerializationConfig.Inspector;
        }

        public void Serialize(PropertyMapping prop, string memberName, object instance)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            var elements = instance as IList;                       
            if(elements == null) throw Error.Argument("existing", "Can only write repeating elements from a type implementing IList");

            foreach(var element in elements)
            {
                _current.WriteStartArrayElement(memberName);
                var writer = new DispatchingWriter(_current, arrayMode: true);

                if(element != null)
                    writer.Serialize(prop, memberName, element);
                else
                    _current.WriteArrayNull();

                _current.WriteEndArrayElement();
            }
        }
    }
}
