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
        
        public Type ImplementingType { get; private set; }

        private Func<string, object> _primitiveParsingFunction;
        
        // Elements indexed by uppercase name for access speed
        private Dictionary<string, PropertyMapping> _propMappings = new Dictionary<string, PropertyMapping>();

        public IEnumerable<PropertyMapping> PropertyMappings
        {
            get
            {
                return _propMappings.Values;
            }
        }


        public object Parse(string value)
        {
            if (ModelConstruct == FhirModelConstruct.PrimitiveType)
            {
                return _primitiveParsingFunction(value);
            }
            else
                throw Error.InvalidOperation("Can only invoke Parse on a primitive mapped class");
        }

        // Class mappings are built in two phases: first all the classes are mapped, after that their
        // properties. Necessary because to determine the mapped type of a property, all mapped classes 
        // have to be known and already mapped. This internal function will only be called by the
        // Inspector after it has created all class mappings.
        internal void InspectProperties(ModelInspector inspector)
        {
            foreach (var property in ReflectionHelper.FindPublicProperties(ImplementingType))
            {            
                //Skip the Value property of a PrimitiveElement, this is handled as a special case by the parser
                if (typeof(PrimitiveElement).IsAssignableFrom(property.DeclaringType) && property.Name == "Value")
                    continue;

                var propMapping = processProperty(inspector, property);

                if (propMapping != null) addPropertyMapping(propMapping);
            }
        }

        private void addPropertyMapping(PropertyMapping mapping)
        {
            _propMappings.Add(mapping.Name.ToUpperInvariant(), mapping);
        }


        private PropertyMapping processProperty(ModelInspector inspector, PropertyInfo prop)
        {
            if (Attribute.GetCustomAttribute(prop, typeof(NotMappedAttribute)) != null) return null;

            return PropertyMapping.Create(inspector, prop);

            //PropertyMapping element = null;
            //bool success = PropertyMapping.TryCreate(inspector, property, out element);

            //if (!success)
            //{
            //    Message.Info("Skipped member {0} in type {1} while doing inspection: not a mappable property",
            //            property.Name, property.DeclaringType.Name);
            //    return null;
            //}

            //return element;
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


        public static bool TryCreate(Type type, out ClassMapping mapping)
        {
            mapping = null;

            if (IsMappableClass(type))
            {
                try
                {
                    mapping = Create(type);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }


        public static ClassMapping Create(Type type)
        {
            checkMutualExclusiveAttributes(type);

            var result = new ClassMapping();

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
                result.ImplementingType = type;
                result._primitiveParsingFunction = buildPrimitiveParserInvoker(type);
            }
            else
                throw Error.Argument("type", "Type {0} is not recognized as either a Fhir Resource, complex datatype or primitive", type.Name);

            result.ImplementingType = type;

            return result;
        }


        private static Func<string,object> buildPrimitiveParserInvoker(Type implementingType)
        {
            // Now determine actual .NET primitive used for the ImplementingType's Value property
            //var valueProperty = ReflectionHelper.FindPublicProperty(result.ImplementingType, "Value");
            //if(valueProperty == null) throw Error.InvalidOperation("Expected a Value property on the mapped primitive class {0}", result.ImplementingType.Name);
            //result.PrimitiveType = valueProperty.PropertyType;
            if (implementingType.IsEnum)
            {
                return input => invokeEnumParser(input, implementingType);
            }
            else
            {
                var parseMethod = ReflectionHelper.FindPublicStaticMethod(implementingType, "Parse", typeof(string));
                if (parseMethod == null) throw Error.InvalidOperation("Expected a static Parse(string) function on the mapped primitive class {0}", implementingType.Name);

                return input => parseMethod.Invoke(null, new object[] { input });
            }
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
