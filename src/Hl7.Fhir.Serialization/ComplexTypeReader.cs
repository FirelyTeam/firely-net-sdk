using Hl7.Fhir.Serialization.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.Serialization
{
    internal class ComplexTypeReader
    {
        private JObject _data;
        private ModelInspector _inspector;

        public ComplexTypeReader(ModelInspector inspector, JObject data)
        {
            _data = data;
            _inspector = inspector;
        }

        internal object Deserialize(MappedModelClass type, object existing=null)
        {
            if (type == null) throw Error.ArgumentNull("type");
           
            if (type.ModelConstruct != FhirModelConstruct.ComplexType && type.ModelConstruct != FhirModelConstruct.Resource  ) 
                throw Error.InvalidOperation(Messages.CanOnlyDeserializeResourceAndComplex, type.Name);

            if (existing == null)
                existing = BindingConfiguration.ModelClassFactories.InvokeFactory(type.ImplementingType);

            foreach (var memberData in _data)
            {
                var memberName = memberData.Key;
                
                var mappedProperty = type.FindMappedPropertyForElement(memberName);

                if (mappedProperty != null)
                {
                    Debug.WriteLine("Handling member " + memberName);

                    var value = mappedProperty.ImplementingProperty.GetValue(existing,null);

                    //var reader = new DispatchingReader(_data[memberName]);

                    //value = reader.Deserialize(propInfo.PropertyType,value);

                    mappedProperty.ImplementingProperty.SetValue(existing, value, null);
                }
                else if (type.ModelConstruct == FhirModelConstruct.Resource &&
                        memberName.Equals(ResourceReader.RESOURCETYPE_MEMBER_NAME, StringComparison.InvariantCultureIgnoreCase))
                {
                    //TODO: Detecting whether this is a special, serialization format-specific member should be
                    //done in an abstraction around the json or xml readers.

                    // Ignore type discriminator in Resources, that member is used in the 
                    // ResourceReader to figure out the actual type when this is unknown
                    // from the context
                }
                else if(type.ModelConstruct == FhirModelConstruct.Resource &&
                            memberName.Equals(ResourceReader.CONTAINED_RESOURCE_MEMBER_NAME, StringComparison.InvariantCultureIgnoreCase))
                {
                    //TODO: Handle contained resourcs
                }
                else
                {
                    if (BindingConfiguration.AcceptUnknownMembers == false)
                        throw Error.InvalidOperation(Messages.DeserializeUnknownMember, memberName);
                    else
                        Debug.WriteLine("Skipping unknown member " + memberName);
                }

                //TODO: handle extension array in complex object
                //TODO: handle _name containing id/extensions for primitive members
            }

            return existing;
        }
    }

}
