using Hl7.Fhir.Model;
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
    public class ResourceReader
    {
       
        public const string CONTAINED_RESOURCE_MEMBER_NAME = "contained";

        private IFhirReader _reader;
        private ModelInspector _inspector;

        public ResourceReader(ModelInspector inspector, IFhirReader reader)
        {
            _reader = reader;
            _inspector = inspector;
        }

        public object Deserialize(object existing=null)
        {
            if (_reader.CurrentToken == TokenType.Object)
            {
                // If there's no a priori knowledge of the type of Resource we will encounter,
                // we'll have to determine from the data itself. 
                var resourceType = _reader.GetResourceTypeName();
                var mappedType = _inspector.FindClassMappingForResource(resourceType);
                //TODO: if existing != null -> compatible with mapped type?

                if (existing == null)
                    existing = BindingConfiguration.ModelClassFactories.InvokeFactory(mappedType.NativeType);
               
                // Delegate the actual work to the ComplexTypeReader, since
                // the serialization of Resources and ComplexTypes are virtuall the same
                var cplxReader = new ComplexTypeReader(_inspector, _reader);
                return cplxReader.Deserialize(mappedType, null, existing);
            }
            else
                throw Error.InvalidOperation("Trying to read a resource, but reader is not at the start of an object");
        }
    }
}
