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
        private JToken _data;
        private ModelInspector _inspector;

        public ComplexTypeReader(ModelInspector inspector, JToken data)
        {
            _data = data;
            _inspector = inspector;
        }


        private bool isResourceOrComplexMapping(ClassMapping mapping)
        {
            return mapping.ModelConstruct == FhirModelConstruct.ComplexType || mapping.ModelConstruct == FhirModelConstruct.Resource;
        }


        internal object Deserialize(Type type, object existing=null)
        {
            if (type == null) throw Error.ArgumentNull("type");

            var mapping = _inspector.FindClassMappingByImplementingType(type);

            if (mapping == null) throw Error.Argument("Cannot find class mapping for type {0} while deserializing a complex type", type.Name);

            return Deserialize(mapping, existing);
        }

        internal object Deserialize(ClassMapping mapping, object existing=null)
        {
            if (mapping == null) throw Error.ArgumentNull("type");
           
            if(!isResourceOrComplexMapping(mapping))
                throw Error.InvalidOperation(Messages.CanOnlyDeserializeResourceAndComplex, mapping.ModelConstruct);
            //TODO: is 'existing' compatible with the mapping?

            if (_data is JObject)
            {
                if (existing == null)
                    existing = BindingConfiguration.ModelClassFactories.InvokeFactory(mapping.ImplementingType);

                read(mapping, (JObject)_data, existing);

                return existing;
            }
            else
                throw Error.InvalidOperation("Trying to read a complex object, but reader is not at the start of an object");
        }


        private void read(ClassMapping mapping, JObject source, object existing)
        {
            foreach (var memberData in source)
            {
                var memberName = memberData.Key;

                // Find a property on the instance that matches the element found in the data
                // NB: This function knows how to handle suffixed names (e.g. xxxxBoolean) (for choice types).
                var mappedProperty = mapping.FindMappedPropertyForElement(memberName);

                if (mappedProperty != null)
                {
                    Message.Info("Handling member {0}.{1}", mapping.Name, memberName);

                    var value = mappedProperty.ImplementingProperty.GetValue(existing, null);
                   
                    ClassMapping propMapping = null;

                    if(!mappedProperty.IsPolymorhic)
                    {
                        propMapping = mappedProperty.MappedPropertyType;
                    }
                    else
                    {
                        var typeName = mappedProperty.GetSuffixFromName(memberName);
                        propMapping = _inspector.FindClassMappingForFhirDataType(typeName);
                        if (propMapping == null)
                            throw Error.InvalidOperation("Encountered polymorph member {0}, which uses unknown datatype {1}", memberName, typeName);
                    }

                    var reader = new DispatchingReader(_inspector, source[memberName]);
                    value = reader.Deserialize(propMapping, mappedProperty.MayRepeat, value);

                    mappedProperty.ImplementingProperty.SetValue(existing, value, null);
                }
                else if (mapping.ModelConstruct == FhirModelConstruct.Resource &&
                        memberName.Equals(ResourceReader.RESOURCETYPE_MEMBER_NAME, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Ignore type discriminator in Resources, that member is used in the 
                    // ResourceReader to figure out the actual type when this is unknown
                    // from the context

                    //TODO: Detecting whether this is a special, serialization format-specific member should be
                    //done in an abstraction around the json or xml readers.
                }
                //else if (mapping.ModelConstruct == FhirModelConstruct.Resource &&
                //            memberName.Equals(ResourceReader.CONTAINED_RESOURCE_MEMBER_NAME, StringComparison.InvariantCultureIgnoreCase))
                //{
                //    //TODO: Handle contained resourcs
                //    Message.Info("Skipped parsing contained resources - not yet supported");
                //}
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

        }
    }

}
