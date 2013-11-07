using Hl7.Fhir.Model;
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
        private IFhirReader _current;
        private ModelInspector _inspector;

        public ComplexTypeReader(ModelInspector inspector, IFhirReader reader)
        {
            _current = reader;
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
           
            //if(!isResourceOrComplexMapping(mapping))
            //    throw Error.InvalidOperation(Messages.CanOnlyDeserializeResourceAndComplex, mapping.ModelConstruct);
            //TODO: is 'existing' compatible with the mapping?

            if (_current.CurrentToken == TokenType.Object)
            {
                if (existing == null)
                    existing = BindingConfiguration.ModelClassFactories.InvokeFactory(mapping.ImplementingType);

                read(mapping, existing);

                return existing;
            }
            else
                throw Error.InvalidOperation("Trying to read a complex object, but reader is not at the start of an object");
        }


        private void read(ClassMapping mapping, object existing)
        {
            bool hasMember = false;

            foreach (var memberData in _current.GetMembers())
            {
                hasMember = true;
                var memberName = memberData.Item1;  // tuple: first is name of member

                // Find a property on the instance that matches the element found in the data
                // NB: This function knows how to handle suffixed names (e.g. xxxxBoolean) (for choice types).
                var mappedProperty = mapping.FindMappedPropertyForElement(memberName);

                if (mappedProperty != null)
                {
                    Message.Info("Handling member {0}.{1}", mapping.Name, memberName);
                                    
                    var value = mappedProperty.ImplementingProperty.GetValue(existing, null);

                    ClassMapping propTypeMapping;

                    // For Element properties, determine the actual type of the element using
                    // the suffix of the membername (i.e. deceasedBoolean, deceasedDate)
                    if (mappedProperty.PolymorphicBase == typeof(Element))
                        propTypeMapping = determineElementPropertyType(mappedProperty, memberName);

                    // For Resources, the type is not yet known more precisely than Resource,
                    // the specific Resource type is determined by Resource.resourceType later on.
                    else if (mappedProperty.PolymorphicBase == typeof(Resource))
                        propTypeMapping = _inspector.FindClassMappingByImplementingType(typeof(Resource));

                    else
                        propTypeMapping = mappedProperty.MappedPropertyType;

                    if (mappedProperty.MayRepeat)
                    {
                        var reader = new RepeatingElementReader(_inspector, memberData.Item2);
                        value = reader.Deserialize(propTypeMapping, mappedProperty, value);
                    }
                    else
                    {
                        var reader = new DispatchingReader(_inspector, memberData.Item2);
                        value = reader.Deserialize(propTypeMapping, mappedProperty, value);
                    }

                    mappedProperty.ImplementingProperty.SetValue(existing, value, null);
                }
                else
                {
                    if (BindingConfiguration.AcceptUnknownMembers == false)
                        throw Error.InvalidOperation(Messages.DeserializeUnknownMember, memberName);
                    else
                        Message.Info("Skipping unknown member " + memberName);
                }
            }

            if (!hasMember)
                throw Error.NotSupported("Fhir serialization does not allow nor support empty elements");
        }

        private ClassMapping determineElementPropertyType(PropertyMapping mappedProperty, string memberName)
        {
            ClassMapping result = null;

            var typeName = mappedProperty.GetSuffixFromName(memberName);

            // Exception: valueResource actually means the element is of type ResourceReference
            if (typeName == "Resource") typeName = "ResourceReference";

            result = _inspector.FindClassMappingForFhirDataType(typeName);
            if (result == null)
                throw Error.InvalidOperation("Encountered polymorph member {0}, which uses unknown datatype {1}", memberName, typeName);

            return result;
        }
    }

}
