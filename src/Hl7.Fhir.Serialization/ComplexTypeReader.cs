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

        internal object Deserialize(ClassMapping mapping, PropertyMapping prop, object existing=null)
        {
            if (mapping == null) throw Error.ArgumentNull("type");
           
            //TODO: is 'existing' compatible with the mapping?

            if (existing == null)
            {
                var creationType = mapping.ImplementingType;

                // Special case, we have one class mapping that is a generic (Code<>)
                // now we know the actual closed generic type of the property,
                // create the specific code Code<T>
                if (prop != null && prop.IsCodeOfTProperty)
                    creationType = mapping.ImplementingType.MakeGenericType(prop.CodeOfTEnumType);

                existing = BindingConfiguration.ModelClassFactories.InvokeFactory(creationType);
            }

            IEnumerable<Tuple<string, IFhirReader>> members = null;

            if (_current.CurrentToken == TokenType.Object)
            {
                members = _current.GetMembers();
            }
            else if(_current.IsPrimitive())
            {
                // Ok, we expected a complex type, but we found a primitive instead. This may happen
                // in Json where the value property and the other elements are separately put into
                // member and _member. In this case, we will parse the primitive into the Value property
                // of the complex type
                if (mapping.PrimitiveValueProperty == null)
                    throw Error.InvalidOperation("Complex object does not have a value property, yet the reader is at a primitive");

                // Simulate this as actually receiving a member "Value" in a normal complex object,
                // and resume normally
                var valueMember = Tuple.Create(mapping.PrimitiveValueProperty.Name, _current);
                members = new List<Tuple<string, IFhirReader>> { valueMember };
            }
            else
                throw Error.InvalidOperation("Trying to read a complex object, but reader is not at the start of an object or primitive");

            if(prop != null && prop.IsCodeOfTProperty)
                read(mapping, members, existing);
            else
                read(mapping, members, existing);

            return existing;

        }


        private void read(ClassMapping mapping, IEnumerable<Tuple<string,IFhirReader>> members, object existing)
        {
            bool hasMember = false;

            foreach (var memberData in members)
            {
                hasMember = true;
                var memberName = memberData.Item1;  // tuple: first is name of member

                // Find a property on the instance that matches the element found in the data
                // NB: This function knows how to handle suffixed names (e.g. xxxxBoolean) (for choice types).
                var mappedProperty = mapping.FindMappedPropertyForElement(memberName);

                if (mappedProperty != null)
                {
                    Message.Info("Handling member {0}.{1}", mapping.Name, memberName);
                  
                    // Handle primitive member => no getter necessary
                    if (mappedProperty.IsNativeValueProperty)
                    {
                        var nativeType = mappedProperty.NativeType;
                        var prop = mappedProperty.ImplementingProperty;

                        // If the property used an open generic type while inspecting, we don't
                        // know the actual property & type yet. Take a look at the actual instance to get it.
                        if (nativeType == null)
                        {
                            prop = ReflectionHelper.FindPublicProperty(existing.GetType(),prop.Name);
                            nativeType = ReflectionHelper.GetInstantiableNativeType(prop.PropertyType);
                        }

                        var reader = new PrimitiveValueReader(_inspector, memberData.Item2);
                        var value = reader.Deserialize(nativeType);
                        prop.SetValue(existing, value, null);
                    }
                    else
                    {
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
