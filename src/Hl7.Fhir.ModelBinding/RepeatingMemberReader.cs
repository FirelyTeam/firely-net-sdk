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

        public IList Deserialize(Type type, IList instance=null)
        {
            if (type == null) throw Error.ArgumentNull("type");

            if (!ReflectionHelper.IsTypedCollection(type)) throw Error.InvalidOperation(Messages.CanOnlyDeserializeTypedCollections, type.Name);

            Type elementType = ReflectionHelper.GetCollectionItemType(type);

            if(instance == null)
                instance = (IList)BindingConfiguration.ModelClassFactories.InvokeFactory(type);

            foreach(var element in _data)
            {
                var reader = new DispatchingReader(element);
                var result = reader.Deserialize(elementType);

                instance.Add(result);
            }

            return instance;
        }
    }
}
