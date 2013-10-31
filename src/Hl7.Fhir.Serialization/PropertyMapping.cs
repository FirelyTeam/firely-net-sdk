using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class PropertyMapping
    {
        public string Name { get; private set; }
        public bool IsPolymorhic { get; private set; }
        public Type PolymorphicBase { get; private set; }
        public bool MayRepeat { get; private set; }

        public ClassMapping PropertyTypeMapping { get; private set; }
        public IEnumerable<Type> GenericParams { get; private set; }

        public PropertyInfo ImplementingProperty { get; private set; }     

        public static bool TryCreateFromProperty(PropertyInfo prop, ModelInspector inspector, out PropertyMapping result)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            result = new PropertyMapping();
            result.Name = getMappedElementName(prop);
            result.ImplementingProperty = prop;

            result.MayRepeat = ReflectionHelper.IsTypedCollection(prop.PropertyType);

            Type elementType = prop.PropertyType;

            if(result.MayRepeat)
            {
                // If this is a collection, inspect the collection's element type
                elementType = ReflectionHelper.GetCollectionItemType(elementType);
            }

            //TODO: a profiled class may have a single fixed type left as choice, so
            //could map this to a fixed type.
            if (elementType == typeof(Element) || elementType == typeof(Resource) || elementType == typeof(object))
            {
                // Special case: polymorphic (choice) properties are generated to have type Element
                // Special case: the contained property of resources can have any Resource
                result.IsPolymorhic = true;
                result.PropertyTypeMapping = null;   // polymorphic, so cannot be known in advance (look at member name in instance)
                result.PolymorphicBase = elementType;  // keep the type, so we know whether to expect any element or any resource (contained)
                return true;
            }
            else
            {
                if(isFhirPrimtiveMappedAsNativePrimitive(elementType))
                {
                    //TODO: pick up an attribute to see which fhirtype maps to this primitive .NET
                    Message.Info("Property {0} on type {1}: mappings to .NET native types are not yet supported", prop.Name, prop.DeclaringType.Name);
                    return false;
                }
               
                // Special case: this is a closed generic type try to find the mapping for
                // its open, defining type instead
                if (elementType.IsGenericType)
                {
                    if (ReflectionHelper.IsClosedGenericType(elementType))
                    {
                        result.GenericParams = elementType.GetGenericArguments();
                        elementType = elementType.GetGenericTypeDefinition();
                    }
                    else
                    {
                        Message.Info("Property {0} on type {1}: property has an open generic type, which is not yet supported", prop.Name, prop.DeclaringType.Name);
                        return false;
                    }

                }

                // pre-fetch the mapping for this property, saves lookups while parsing instance
                var mappedPropertyType = inspector.FindClassMappingByImplementingType(elementType);
                if(mappedPropertyType == null)
                {
                    Message.Info("Property {0} on type {1}: property maps to a type that is not recognized as a mapped FHIR type", prop.Name, prop.DeclaringType.Name);
                    return false;
                }

                result.PropertyTypeMapping = mappedPropertyType;
                return true;
            }
        }


        private static string getMappedElementName(PropertyInfo prop)
        {
            var attr = (FhirElementAttribute)Attribute.GetCustomAttribute(prop, typeof(FhirElementAttribute));

            if (attr != null)
                return attr.Name;
            else
                return prop.Name;
        }

        private static bool isFhirPrimtiveMappedAsNativePrimitive(Type type)
        {
            if (type == typeof(bool?) ||
                   type == typeof(int?) ||
                   type == typeof(decimal?) ||
                   type == typeof(byte[]) ||
                   type == typeof(DateTimeOffset?) ||
                   type == typeof(string))
                return true;

            // Special case, allow Nullable<enum>
            if (ReflectionHelper.IsNullableType(type))
            {
                var nullable = ReflectionHelper.GetNullableArgument(type);
                if (nullable.IsEnum) return true;
            }

            return false;
        }

        internal static bool IsMappableElement(PropertyInfo prop)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            var type = prop.PropertyType;

            return type == typeof(Element)
                        || ClassMapping.IsFhirComplexType(type)
                        || ClassMapping.IsFhirPrimitive(type)
                        || isFhirPrimtiveMappedAsNativePrimitive(type);
        }
    }
}
