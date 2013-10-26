using Hl7.Fhir.ModelBinding.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public class DispatchingReader
    {
        private JToken _data;

        public DispatchingReader(JToken data)
        {
            _data = data;
        }

        public object Deserialize(object instance)
        {
            Type type = instance.GetType();

            if (ReflectionHelper.IsPrimitive(type))
            {
                // throw Error.NotImplemented("Cannot yet read primitives");
                Debug.WriteLine("Cannot yet handle primitive");
                return instance;
            }
            if(ReflectionHelper.IsTypedCollection(type))
            {
                if (_data is JArray)
                {
                    var reader = new RepeatingMemberReader((JArray)_data);
                    reader.Deserialize((IList)instance);
                    return instance;
                }
                else
                    throw Error.InvalidOperation("Trying to read a collection, but reader is not at a repeating member");
            }
            if (ReflectionHelper.IsComplexType(type))
            {
                if (_data is JObject)
                {
                    var reader = new ComplexMemberReader((JObject)_data);
                    reader.Deserialize(instance);
                    return instance;
                }
                else
                    throw Error.InvalidOperation("Trying to read a complex object, but reader is not at an object member");
            }
            else if (ReflectionHelper.IsEnum(type))
            {
                // throw Error.NotImplemented("Cannot yet read enumerations");
                Debug.WriteLine("Cannot yet handle enumerations");
                return instance;
            }
            else
                throw Error.InvalidOperation("Don't know how to read data for type {0}", type.Name);
        }


        internal Type DetermineTypeFromData()
        {
            // If there's no a priori knowledge of the type of data we will encounter,
            // we'll have to determine from the data itself. That's possible by looking
            // for the 'resourceType' property.
            if (_data is JObject)
            {
                var complexData = (JObject)_data;
                var resourceTypeMember = complexData[ComplexMemberReader.RESOURCETYPE_MEMBER_NAME];

                if (resourceTypeMember != null)
                {
                    if (resourceTypeMember is JValue)
                    {
                        var memberValue = (JValue)resourceTypeMember;

                        if (memberValue.Type == JTokenType.String)
                        {
                            return ReflectionHelper.FindTypeByName((string)memberValue.Value);
                        }
                    }

                    throw Error.InvalidOperation("resourceMember should be a primitive string json value");
                }
            }

            throw Error.InvalidOperation("Cannot determine type to create from input data");
        }


        public object Deserialize(Type type)
        {
            if (type == null) throw Error.ArgumentNull("expectedType");

            //TODO: if type is an interface (e.g. IList<X> member), this won't work
            //Factories have to know to fill IList<T> with a new List<T> ?
            var result = BindingConfiguration.ModelClassFactories.InvokeFactory(type);

            return Deserialize(result);
        }

        public object Deserialize()
        {
            return Deserialize(DetermineTypeFromData());
        }
    }
}
