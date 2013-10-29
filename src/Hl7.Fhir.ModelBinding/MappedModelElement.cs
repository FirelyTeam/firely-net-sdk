using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public enum ElementKind
    {
        Primitive,
        Complex,
        Polymorph
    }

    public class MappedModelElement
    {
        public ElementKind Kind { get; set; }
        public string Name { get; set; }
        public bool MayRepeat { get; set; }

        public PropertyInfo ImplementingProperty { get; set; }     

        public static bool TryCreateFromProperty(PropertyInfo prop, out MappedModelElement result)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            result = new MappedModelElement();
            result.Name = prop.Name;
            result.ImplementingProperty = prop;

            result.MayRepeat = ReflectionHelper.IsTypedCollection(prop.PropertyType);

            Type elementType = prop.PropertyType;

            if(result.MayRepeat)
            {
                // If this is a collection, inspect the collection's element type
                elementType = ReflectionHelper.GetCollectionItemType(elementType);
            }

            if (elementType == typeof(Element))
            {
                // Special case: polymorphic (choice) properties are generated to have type Element
                result.Kind = ElementKind.Polymorph;
            }
            else if (MappedModelClass.IsFhirComplexType(elementType))
            {
                result.Kind = ElementKind.Complex;
            }
            else if (MappedModelClass.IsFhirPrimitive(elementType)
                            || isFhirPrimtiveMappedAsNativePrimitive(elementType))
            {
                result.Kind = ElementKind.Primitive;
            }
            else
                return false;

            return true;
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
                        || MappedModelClass.IsFhirComplexType(type)
                        || MappedModelClass.IsFhirPrimitive(type)
                        || isFhirPrimtiveMappedAsNativePrimitive(type);
        }
    }
}
