using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Properties;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class ResourceReader
    {       
     //   public const string CONTAINED_RESOURCE_MEMBER_NAME = "contained";

        private IFhirReader _reader;
        private ModelInspector _inspector;

        public ResourceReader(IFhirReader reader)
        {
            _reader = reader;
            _inspector = SerializationConfig.Inspector;
        }

        public Resource Deserialize(object existing=null)
        {
            if (_reader.CurrentToken == TokenType.Object)
            {
                // If there's no a priori knowledge of the type of Resource we will encounter,
                // we'll have to determine from the data itself. 
                var resourceType = _reader.GetResourceTypeName();
                var mappedType = _inspector.FindClassMappingForResource(resourceType);

                if (existing == null)
                    existing = SerializationConfig.ModelClassFactories.InvokeFactory(mappedType.NativeType);
                else
                {
                    if (mappedType.NativeType != existing.GetType())
                        throw Error.Argument("existing", "Existing instance is of type {0}, but data indicates resource is a {1}", existing.GetType().Name, resourceType);
                }
               
                // Delegate the actual work to the ComplexTypeReader, since
                // the serialization of Resources and ComplexTypes are virtually the same
                var cplxReader = new ComplexTypeReader(_reader);
                return (Resource)cplxReader.Deserialize(mappedType, existing);
            }
            else
                throw Error.InvalidOperation("Trying to read a resource, but reader is not at the start of an object");
        }
    }
}
