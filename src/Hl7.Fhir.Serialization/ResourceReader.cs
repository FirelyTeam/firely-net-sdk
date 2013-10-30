using Hl7.Fhir.Serialization.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    internal class ResourceReader
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";
        public const string CONTAINED_RESOURCE_MEMBER_NAME = "contained";

        private JObject _data;
        private ModelInspector _inspector;

        public ResourceReader(ModelInspector inspector, JObject data)
        {
            _data = data;
            _inspector = inspector;
        }

        public object Deserialize(object existing=null)
        {
            var resourceType = getResourceNameFromData();
            var mappedType = _inspector.FindMappedClassForResource(resourceType);

            var result = existing;

            if (result == null)
                BindingConfiguration.ModelClassFactories.InvokeFactory(mappedType.ImplementingType);

            // Delegate the actual work to the ComplexTypeReader
            var cplxReader = new ComplexTypeReader(_inspector, _data);

            result = cplxReader.Deserialize(mappedType, result);

            return result;
        }

        private string getResourceNameFromData()
        {
            // If there's no a priori knowledge of the type of data we will encounter,
            // we'll have to determine from the data itself. That's possible by looking
            // for the 'resourceType' property.
            if (_data is JObject)
            {
                var complexData = (JObject)_data;
                var resourceTypeMember = complexData[RESOURCETYPE_MEMBER_NAME];

                if (resourceTypeMember != null)
                {
                    if (resourceTypeMember is JValue)
                    {
                        var memberValue = (JValue)resourceTypeMember;

                        if (memberValue.Type == JTokenType.String)
                        {
                            return (string)memberValue.Value;
                        }
                    }

                    throw Error.InvalidOperation("resourceMember should be a primitive string json value");
                }
            }

            throw Error.InvalidOperation("Cannot determine type to create from input data");
        }

    }
}
