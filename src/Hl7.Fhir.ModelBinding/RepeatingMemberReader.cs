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
        private JArray _data;

        public RepeatingMemberReader(JArray data)
        {
            _data = data;
        }

        public void Deserialize(IList existingInstance)
        {
            if (existingInstance == null) throw Error.ArgumentNull("existingInstance");

            Type objectType = existingInstance.GetType();
            if (!ReflectionHelper.IsTypedCollection(objectType)) throw Error.InvalidOperation(Messages.CanOnlyDeserializeTypedCollections, objectType.Name);

            Type elementType = ReflectionHelper.GetCollectionItemType(objectType);

            foreach(var element in _data)
            {
                var reader = new DispatchingReader(element);
                var result = reader.Deserialize(elementType);

                existingInstance.Add(result);
            }
        }
    }
}
