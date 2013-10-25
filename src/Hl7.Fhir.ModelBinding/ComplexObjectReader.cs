using Hl7.Fhir.ModelBinding.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.ModelBinding
{
    public class ComplexObjectReader
    {
        private JObject _data;

        public ComplexObjectReader(JObject data)
        {
            _data = data;
        }

        public object Deserialize(object existingInstance)
        {
            if (existingInstance == null) throw Error.ArgumentNull("existingInstance");
           
            Type objectType = existingInstance.GetType();
            if (objectType.IsPrimitive) throw Error.InvalidOperation(Messages.CannotDeserializePrimitive, objectType.Name);

            var typeMembers = ReflectionHelper.FindPublicProperties(objectType);
                
            //TODO: is reader on complex object?

            foreach (var memberData in _data)
            {
                var memberName = memberData.Key;

                PropertyInfo propInfo;

                //TODO: Handle _propname case for primitives
                if (typeMembers.TryGetValue(memberName.ToUpperInvariant(), out propInfo))
                {
                    Debug.WriteLine("Handling member " + memberName);
                    if( memberData.Value is JObject )
                    {
                        //TODO: check whether property is not a primitive
                        var subReader = new ComplexObjectReader((JObject)memberData.Value);
                        var propValue = subReader.Deserialize(propInfo.PropertyType);
                    }
                    else if (memberData.Value is JArray)
                    {
                        //TODO: check whether property is indeed enumerable

                    }
                }
                else
                {
                    if (BindingConfiguration.AcceptUnknownMembers == false)
                        throw Error.InvalidOperation(Messages.DeserializeUnknownMember, memberName);
                    else
                        Debug.WriteLine("Skipping unknown member " + memberName);
                }

                //TODO: get the member's type
                //TODO: find a reader that handles the type and execute it
                //TODO: assign value

                //TODO: handle extension array in complex object
            }

            return existingInstance;
        }

        public object Deserialize(Type expectedType)
        {
            if (expectedType == null) throw Error.ArgumentNull("expectedType");
            if (expectedType.IsPrimitive) throw Error.InvalidOperation(Messages.CannotDeserializePrimitive, expectedType.Name);

            var factory = BindingConfiguration.ModelClassFactories != null ?
                BindingConfiguration.ModelClassFactories.FindFactory(expectedType) : null;
            if (factory == null) throw Error.InvalidOperation(Messages.NoSuchClassFactory, expectedType.Name);

            object result = factory.Create(expectedType);
            if(result == null) throw Error.InvalidOperation(Messages.FactoryCreationFailed);

            return Deserialize(result);
        }
    }

}
