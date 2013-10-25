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

            var typeMembers = ReflectionHelper.FindPublicProperties(objectType);
                
            foreach (var memberData in _data)
            {
                var memberName = memberData.Key;

                PropertyInfo propInfo;

                //TODO: Handle _propname case for primitives
                if (typeMembers.TryGetValue(memberName.ToUpperInvariant(), out propInfo))
                {
                    Debug.WriteLine("Handling member " + memberName);
                }
                else
                {
                    if (BindingConfiguration.AcceptUnknownMembers == false)
                        Error.InvalidOperation(Messages.DeserializeUnknownMember, memberName);
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
            if (_data == null) Error.InvalidOperation(Messages.DataToDeserializeNull);
            if (expectedType == null) throw Error.ArgumentNull("expectedType");

            var factory = BindingConfiguration.ModelClassFactories != null ?
                BindingConfiguration.ModelClassFactories.FindFactory(expectedType) : null;
            if (factory == null) Error.InvalidOperation(Messages.NoSuchClassFactory, expectedType.Name);

            object result = factory.Create(expectedType);
            if(result == null) Error.InvalidOperation(Messages.FactoryCreationFailed);

            return Deserialize(result);
        }
    }

}
