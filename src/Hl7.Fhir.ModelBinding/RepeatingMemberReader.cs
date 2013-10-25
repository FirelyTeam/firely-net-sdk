using Hl7.Fhir.ModelBinding.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.ModelBinding
{
    public class RepeatingMemberReader
    {
        private JObject _data;

        public RepeatingMemberReader(JObject data)
        {
            _data = data;
        }

        public object Deserialize(IList existingInstance)
        {
            if (existingInstance == null) throw Error.ArgumentNull("existingInstance");

            Type objectType = existingInstance.GetType();
            if (!typeof(IList).IsAssignableFrom(objectType)) throw Error.InvalidOperation(Messages.CanOnlyDeserializeIList, objectType.Name);
               
            //TODO: is reader on array?
            
            
            return existingInstance;
        }

        public object Deserialize(Type expectedType)
        {
            if (expectedType == null) throw Error.ArgumentNull("expectedType");
            if (!typeof(IList).IsAssignableFrom(expectedType)) throw Error.InvalidOperation(Messages.CanOnlyDeserializeIList, expectedType.Name);

            var factory = BindingConfiguration.ModelClassFactories != null ?
                BindingConfiguration.ModelClassFactories.FindFactory(expectedType) : null;
            if (factory == null) throw Error.InvalidOperation(Messages.NoSuchClassFactory, expectedType.Name);

            IList result = factory.Create(expectedType) as IList;
            if(result == null) throw Error.InvalidOperation(Messages.FactoryCreationFailed);

            return Deserialize(result);
        }
    }

}
