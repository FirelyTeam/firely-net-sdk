using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class ClassMapping
    {
        internal const string RESOURCENAME_SUFFIX = "Resource";

        public FhirModelConstruct ModelConstruct;

        public string Name { get; private set; }
        public string Profile { get; private set; }

        // The .NET class this is a mapping to and that implements a FHIR type
        public Type ImplementingType { get; private set; }

        // If the implementing type has generic arguments, this will be set to true
        public bool HasGenericArguments { get; private set; }

        // Set to an element from the PropertyMappings collection, which contains the .NET native
        // property with the actual value of the PrimitiveType.
        public PropertyMapping PrimitiveValueProperty { get; private set; }
       
        // Elements indexed by uppercase name for access speed
        private Dictionary<string, PropertyMapping> _propMappings = new Dictionary<string, PropertyMapping>();

        public IEnumerable<PropertyMapping> PropertyMappings
        {
            get
            {
                return _propMappings.Values;
            }
        }


        // Class mappings are built in two phases: first all the classes are mapped, after that their
        // properties. Necessary because to determine the mapped type of a property, all mapped classes 
        // have to be known and already mapped. This internal function will only be called by the
        // Inspector after it has created all class mappings.
        internal void InspectProperties(ModelInspector inspector)
        {
            foreach (var property in ReflectionHelper.FindPublicProperties(ImplementingType))
            {
                // Skip properties that are marked as NotMapped
                if (Attribute.GetCustomAttribute(property, typeof(NotMappedAttribute)) != null) continue;

                var propMapping = PropertyMapping.Create(inspector, property);

                // Keep a pointer to this property if this is a primitive value element ("Value" in primitive types)
                if(propMapping.IsNativeValueProperty)
                    this.PrimitiveValueProperty = propMapping;

                if (propMapping != null)
                    _propMappings.Add(propMapping.Name.ToUpperInvariant(), propMapping);
            }
        }


        public PropertyMapping FindMappedPropertyForElement(string name)
        {
            if (name == null) throw Error.ArgumentNull("name");

            var normalizedName = name.ToUpperInvariant();

            PropertyMapping prop = null;

            bool success = _propMappings.TryGetValue(normalizedName, out prop);

            // Direct success
            if (success) return prop;
            
            // Not found, maybe a polymorphic name
            // TODO: specify possible polymorhpic variations using attributes
            // to speedup look up & aid validation
            return PropertyMappings.SingleOrDefault(p => p.MatchesSuffixedName(name));            
        }


        public static ClassMapping Create(Type type)
        {
            checkMutualExclusiveAttributes(type);

            var result = new ClassMapping();
            result.ImplementingType = type;

            if (ReflectionHelper.IsOpenGenericTypeDefinition(type))
                result.HasGenericArguments = true;

            if (IsFhirResource(type))
            {
                result.ModelConstruct = FhirModelConstruct.Resource;
                result.Name = getMappedResourceName(type);
                result.Profile = getProfile(type);
            }
            else if (IsFhirComplexType(type))
            {
                result.ModelConstruct = FhirModelConstruct.ComplexType;
                result.Name = getMappedComplexTypeName(type);
                result.Profile = null;  // No support for profiled datatypes
            }
            else if (IsFhirPrimitive(type))
            {
                result.ModelConstruct = FhirModelConstruct.PrimitiveType;
                result.Name = getMappedPrimitiveTypeName(type);
                result.Profile = null;  // No support for profiled datatypes           
            }
            else
                throw Error.Argument("type", "Type {0} is not recognized as either a Fhir Resource, complex datatype or primitive", type.Name);

            return result;
        }

        private static void checkMutualExclusiveAttributes(Type type)
        {
            if (ClassMapping.IsFhirResource(type) && ClassMapping.IsFhirComplexType(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Complex datatype", type);
            if (ClassMapping.IsFhirResource(type) && ClassMapping.IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Primitive datatype", type);
            if (ClassMapping.IsFhirComplexType(type) && ClassMapping.IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Complex and a Primitive datatype", type);
        }


        private static object invokeEnumParser(string input, Type enumType)
        {
            object result = null;
            bool success = EnumHelper.TryParseEnum(input, enumType, out result);

            if (!success)
                throw Error.InvalidOperation("Parsing of enum failed");

            return result;
        }

        private static string getProfile(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            return attr != null ? attr.Profile : null;
        }

        private static string getMappedResourceName(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            if (attr != null)
            {
                return attr.Name;
            }                
            else
            {
                var name = type.Name;
                if (name.EndsWith(RESOURCENAME_SUFFIX))
                    name = name.Substring(0, name.Length - RESOURCENAME_SUFFIX.Length);

                return name;
            }
        }

        private static string getMappedComplexTypeName(Type type)
        {
            var attr = (FhirComplexTypeAttribute)Attribute.GetCustomAttribute(type, typeof(FhirComplexTypeAttribute));

            if (attr != null)
                return attr.Name;
            else
                return type.Name;
        }

        private static string getMappedPrimitiveTypeName(Type type)
        {
            var attr = (FhirPrimitiveTypeAttribute)Attribute.GetCustomAttribute(type, typeof(FhirPrimitiveTypeAttribute));

            if (attr != null)
                return attr.Name;
            else
                return type.Name;
        }

        public static bool IsFhirResource(Type type)
        {
            return typeof(Resource).IsAssignableFrom(type)
                    || hasResourceNameSuffix(type)
                    || type.IsDefined(typeof(FhirResourceAttribute),true);
        }

        private static bool hasResourceNameSuffix(Type type)
        {
            // This means it *ends* in Resource, not just "Resource"
            return type.Name.EndsWith(ClassMapping.RESOURCENAME_SUFFIX) && ClassMapping.RESOURCENAME_SUFFIX != type.Name;
        }

        public static bool IsFhirComplexType(Type type)
        {
            return typeof(ComplexElement).IsAssignableFrom(type)
                || type.IsDefined(typeof(FhirComplexTypeAttribute), true);
        }

        public static bool IsFhirPrimitive(Type type)
        {
            return typeof(PrimitiveElement).IsAssignableFrom(type)
                || type.IsDefined(typeof(FhirPrimitiveTypeAttribute), true)
                || type.IsDefined(typeof(FhirEnumerationAttribute), false);
        }    

        public static bool IsMappableClass(Type type)
        {
            return ClassMapping.IsFhirComplexType(type) || ClassMapping.IsFhirPrimitive(type) || ClassMapping.IsFhirResource(type);
        }
    }


    public enum FhirModelConstruct
    {
        PrimitiveType,
        ComplexType,
        Resource
    }

}
