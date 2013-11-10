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
        private const string RESOURCENAME_SUFFIX = "Resource";

        /// <summary>
        /// Name of the FHIR datatype/resource this class represents
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The .NET class that implements the FHIR datatype/resource
        /// </summary>
        public Type NativeType { get; private set; }

        /// <summary>
        /// Is True when this class represents a Resource datatype and False if it 
        /// represents a normal complex or primitive Fhir Datatype
        /// </summary>
        public bool IsResource { get; private set; }

        /// <summary>
        /// PropertyMappings indexed by uppercase name for access speed
        /// </summary>
        private Dictionary<string, PropertyMapping> _propMappings = new Dictionary<string, PropertyMapping>();

        /// <summary>
        /// Collection of PropertyMappings that capture information about this classes
        /// properties
        /// </summary>
        public IEnumerable<PropertyMapping> PropertyMappings
        {
            get
            {
                return _propMappings.Values;
            }
        }

        /// <summary>
        /// Holds a reference to a property that represents a primitive FHIR value. This
        /// property will also be present in the PropertyMappings collection. If this class has 
        /// no such property, it is null. 
        /// </summary>
        public PropertyMapping PrimitiveValueProperty { get; private set; }


        /// <summary>
        /// Enumerate this class' properties using reflection, create PropertyMappings
        /// for them and add them to the PropertyMappings.
        /// </summary>
        private void inspectProperties()
        {
            foreach (var property in ReflectionHelper.FindPublicProperties(NativeType))
            {
                // Skip properties that are marked as NotMapped
                if (ReflectionHelper.GetAttribute<NotMappedAttribute>(property) != null) continue;

                var propMapping = PropertyMapping.Create(property);

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
           // checkMutualExclusiveAttributes(type);

            var result = new ClassMapping();
            result.NativeType = type;

            if (IsFhirType(type))
            {
                // Ignore generic type definitions, they can never appear as roots of objects
                // to parse, in which case they will either have been used in closed type definitions
                // or as the closed type of a property.
                if (ReflectionHelper.IsOpenGenericTypeDefinition(type))
                    throw Error.Argument("type", "Type {0} is a open generic type and cannot be used directly to represent a FHIR datatype", type.Name);

                result.Name = collectTypeName(type);
                result.IsResource = IsFhirResource(type);
            }
            else
                throw Error.Argument("type", "Type {0} is not marked as a Fhir Resource or datatype using [FhirType]", type.Name);

            return result;
        }

        //private static void checkMutualExclusiveAttributes(Type type)
        //{
        //    if (ClassMapping.IsFhirResource(type) && ClassMapping.IsFhirComplexType(type))
        //        throw Error.Argument("type", "Type {0} cannot be both a Resource and a Complex datatype", type);
        //    if (ClassMapping.IsFhirResource(type) && ClassMapping.IsFhirPrimitive(type))
        //        throw Error.Argument("type", "Type {0} cannot be both a Resource and a Primitive datatype", type);
        //    if (ClassMapping.IsFhirComplexType(type) && ClassMapping.IsFhirPrimitive(type))
        //        throw Error.Argument("type", "Type {0} cannot be both a Complex and a Primitive datatype", type);
        //}


        //private static object invokeEnumParser(string input, Type enumType)
        //{
        //    object result = null;
        //    bool success = EnumHelper.TryParseEnum(input, enumType, out result);

        //    if (!success)
        //        throw Error.InvalidOperation("Parsing of enum failed");

        //    return result;
        //}


        private static string getProfile(Type type)
        {
            var attr = ReflectionHelper.GetAttribute<FhirTypeAttribute>(type);
         
            return attr != null ? attr.Profile : null;
        }

        private static string collectTypeName(Type type)
        {
            var attr = ReflectionHelper.GetAttribute<FhirTypeAttribute>(type);
                
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

        public static bool IsFhirResource(Type type)
        {
            var attr = ReflectionHelper.GetAttribute<FhirTypeAttribute>(type);

            return typeof(Resource).IsAssignableFrom(type)
                    || type.Name.EndsWith(RESOURCENAME_SUFFIX)
                    || (attr != null && attr.IsResource);
        }

        public static bool IsFhirType(Type type)
        {
            return type.IsDefined(typeof(FhirTypeAttribute),false);
        }
    }
}
