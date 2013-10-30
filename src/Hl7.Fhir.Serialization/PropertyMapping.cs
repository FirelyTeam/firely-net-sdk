using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class PropertyMapping
    {
        public string Name { get; set; }
        public bool IsPolymorhic { get; set; }
        public bool MayRepeat { get; set; }

        public ClassMapping PropertyTypeMapping { get; set; }

        public PropertyInfo ImplementingProperty { get; set; }     

        public static bool TryCreateFromProperty(PropertyInfo prop, ModelInspector inspector, out PropertyMapping result)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            result = new PropertyMapping();
            result.Name = prop.Name;
            result.ImplementingProperty = prop;

            result.MayRepeat = ReflectionHelper.IsTypedCollection(prop.PropertyType);

            Type elementType = prop.PropertyType;

            if(result.MayRepeat)
            {
                // If this is a collection, inspect the collection's element type
                elementType = ReflectionHelper.GetCollectionItemType(elementType);
            }

            if (elementType == typeof(Element) || elementType == typeof(object))
            {
                // Special case: polymorphic (choice) properties are generated to have type Element
                result.IsPolymorhic = true;
                result.PropertyTypeMapping = null;   // polymorphic, so cannot be known in advance (look at member name in instance)
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

                //TODO: because we don't currently load all the model assemblies first, we are not certain
                //to find the mapped type for all complex types we encounter.
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
