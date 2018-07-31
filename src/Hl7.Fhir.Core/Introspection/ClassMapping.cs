/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Introspection
{
    public class ClassMapping
    {
        private const string RESOURCENAME_SUFFIX = "Resource";

        /// <summary>
        /// Name of the FHIR datatype/resource this class represents
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Profile scope within this Name and mapping are applicable
        /// </summary>
        public string Profile { get; private set; }

        /// <summary>
        /// The .NET class that implements the FHIR datatype/resource
        /// </summary>
        public Type NativeType { get; private set; }

        /// <summary>
        /// Is True when this class represents a Resource datatype and False if it 
        /// represents a normal complex or primitive Fhir Datatype
        /// </summary>
        public bool IsResource { get; private set; }

        public bool IsCodeOfT { get; private set; }

        public bool IsBackbone { get; private set; }

        public bool IsAbstract { get; private set; }

        /// <summary>
        /// PropertyMappings indexed by uppercase name for access speed
        /// </summary>
        private Dictionary<string, PropertyMapping> _propMappings = new Dictionary<string, PropertyMapping>();

        /// <summary>
        /// PropertyMappings in the order as the appear in the reflected class, which is the order
        /// in which they must be serialized.
        /// </summary>
        private IList<PropertyMapping> _orderedMappings;

        /// <summary>
        /// Collection of PropertyMappings that capture information about this classes
        /// properties
        /// </summary>
        public IList<PropertyMapping> PropertyMappings
        {
            get
            {
                return _orderedMappings; 
            }
        }

        /// <summary>
        /// Holds a reference to a property that represents a primitive FHIR value. This
        /// property will also be present in the PropertyMappings collection. If this class has 
        /// no such property, it is null. 
        /// </summary>
        public PropertyMapping PrimitiveValueProperty { get; private set; }

        public bool HasPrimitiveValueMember
        { 
            get { return PrimitiveValueProperty != null; } 
        }

        public PropertyMapping FindMappedElementByName(string name)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));

            var normalizedName = name.ToUpperInvariant();
            bool success = _propMappings.TryGetValue(normalizedName, out PropertyMapping prop);

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

            var result = new ClassMapping
            {
                NativeType = type
            };

            if (IsMappableType(type))
            {
                result.Name = collectTypeName(type);
                result.Profile = getProfile(type);
                result.IsResource = IsFhirResource(type);
                result.IsAbstract = type.GetTypeInfo().IsAbstract;
                result.IsCodeOfT = ReflectionHelper.IsClosedGenericType(type) &&
                                    ReflectionHelper.IsConstructedFromGenericTypeDefinition(type, typeof(Code<>));

                result.IsBackbone = type.CanBeTreatedAsType(typeof(IBackboneElement));

                if (!result.IsResource && !String.IsNullOrEmpty(result.Profile))
                    throw Error.Argument(nameof(type), "Type {0} is not a resource, so its FhirType attribute may not specify a profile".FormatWith(type.Name));

                inspectProperties(result);

                return result;
            }
            else
                throw Error.Argument(nameof(type), "Type {0} is not marked as a Fhir Resource or datatype using [FhirType]".FormatWith(type.Name));
        }


        /// <summary>
        /// Enumerate this class' properties using reflection, create PropertyMappings
        /// for them and add them to the PropertyMappings.
        /// </summary>
        private static void inspectProperties(ClassMapping me)
        {
            foreach (var property in ReflectionHelper.FindPublicProperties(me.NativeType))
            {
                // Skip properties that are marked as NotMapped
                if (ReflectionHelper.GetAttribute<NotMappedAttribute>(property) != null) continue;

                var propMapping = PropertyMapping.Create(property);      
                var propKey = propMapping.Name.ToUpperInvariant();

                if (me._propMappings.ContainsKey(propKey))
                    throw Error.InvalidOperation($"Class has multiple properties that are named '{propKey}'. The property name must be unique");

                me._propMappings.Add(propKey, propMapping);

                // Keep a pointer to this property if this is a primitive value element ("Value" in primitive types)
                if (propMapping.RepresentsValueElement)
                    me.PrimitiveValueProperty = propMapping;
            }

            me._orderedMappings = me._propMappings.Values.OrderBy(prop => prop.Order).ToList();
        }


        private static string getProfile(Type type) => 
            type.GetTypeInfo().GetCustomAttribute<FhirTypeAttribute>()?.Profile;

        private static string collectTypeName(Type type)
        {
            var attr = type.GetTypeInfo().GetCustomAttribute<FhirTypeAttribute>();
            string name = attr?.Name ?? type.Name;
           
            if(ReflectionHelper.IsClosedGenericType(type))
            {
                name += "<";
                name += String.Join(",", type.GetTypeInfo().GenericTypeArguments.Select(arg => arg.FullName));
				name += ">";
			}

            return name;
        }

        public static bool IsFhirResource(Type type)
        {
            var attr = ReflectionHelper.GetAttribute<FhirTypeAttribute>(type.GetTypeInfo());
            return typeof(Resource).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo())
                   || (attr != null && attr.IsResource);
            //var attr = ReflectionHelper.GetAttribute<FhirTypeAttribute>(type);
            //return typeof(Resource).IsAssignableFrom(type)
            //       || (attr != null && attr.IsResource);
        }


        public static bool IsMappableType(Type type)
        {
            var hasAttribute = type.GetTypeInfo().IsDefined(typeof(FhirTypeAttribute), false);

            if (!hasAttribute) return false;

            //if (type.GetTypeInfo().IsAbstract)
            //    throw Error.Argument(nameof(type), "Type {0} is marked as a mappable tpe, but is abstract so cannot be used directly to represent a FHIR datatype".FormatWith(type.Name));

            // Open generic type definitions can never appear as roots of objects
            // to parse. In instances, they will either have been used in closed type definitions
            // or as the closed type of a property. However, the FhirType attribute propagates to
            // these closed definitions, so we will allow having this attribute on an open generic,
            // it's not going to be directly mappable however.
            if (ReflectionHelper.IsOpenGenericTypeDefinition(type))
            {
                Message.Info("Type {0} is marked as a FhirType and is an open generic type, which cannot be used directly to represent a FHIR datatype", type.Name);
                return false;
            }

            return true;
        }
    }
}
