using Hl7.Fhir.Serialization.Properties;
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
    public class RepeatingElementReader
    {
        private JToken _data;
        private ModelInspector _inspector;
        private bool _inExtensionArrayMode = false;

        public RepeatingElementReader(ModelInspector inspector, JToken data, bool inExtensionArrayMode = false)
        {
            _data = data;
            _inspector = inspector;
            _inExtensionArrayMode = inExtensionArrayMode;
        }

        public object Deserialize(ClassMapping mapping, object existing=null)
        {
            if (mapping == null) throw Error.ArgumentNull("mapping");

            if (_data is JArray)
            {
                var array = (JArray)_data;
                IList result;

                if (existing == null) 
                    result = ReflectionHelper.CreateGenericList(mapping.ImplementingType);
                else
                {
                    result = existing as IList;
                        
                    if(result == null) throw Error.Argument("existing", "Can only read repeating elements into a type implementing IList");
                }

                foreach (var element in array)
                {
                    var reader = new DispatchingReader(_inspector, element);
                    var item = reader.Deserialize(mapping, repeating:false); // repeating elements cannot be themselves repeating

                    result.
                    result.Add(item);
                }

                return result;
            }
            else
                throw Error.InvalidOperation("Trying to read a collection, but reader is not at a repeating member");          
        }
    }
}
