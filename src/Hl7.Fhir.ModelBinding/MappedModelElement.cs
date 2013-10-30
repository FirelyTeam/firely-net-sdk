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

        public static MappedModelElement Create(PropertyInfo prop)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            var result = new MappedModelElement();

            if(prop.PropertyType == typeof(Element))
            {
                // Special case: polymorphic (choice) properties are generated to have type Element
                result.Kind = ElementKind.Polymorph;
            }
            else if(MappedModelClass.IsFhirComplexType(prop.PropertyType) )
            {
                result.Kind = ElementKind.Complex;
            }
            else if(MappedModelClass.IsFhirPrimitive(prop.PropertyType) 
                            || isFhirPrimtiveMappedAsNativePrimitive(prop.PropertyType) )
            {
                result.Kind = ElementKind.Primitive;
            }
            else
                throw Error.Argument("Cannot map property {0}: unable to determine element kind", prop.Name);

            result.Name = prop.Name;

            result.MayRepeat = ReflectionHelper.IsCollection(prop.PropertyType);

            result.ImplementingProperty = prop;

            return result;
        }

        private static bool isFhirPrimtiveMappedAsNativePrimitive(Type type)
        {
            return type == typeof(bool?) ||
                   type == typeof(int?) ||
                   type == typeof(decimal?) ||
                   type == typeof(byte[]) ||
                   type == typeof(DateTimeOffset?) ||
                   type == typeof(string);
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
