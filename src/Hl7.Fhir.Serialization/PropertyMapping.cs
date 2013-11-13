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

        private ICollection<ChoiceAttribute> _choices = null;

        public bool HasChoices
        {
            get { return _choices != null; }
        }

        public bool IsCollection { get; private set; }

        public bool HoldsFhirPrimitiveValue { get; private set; }

        public Type ReturnType { get; private set; }
        public Type ElementType { get; private set; }

        public static PropertyMapping Create(PropertyInfo prop)
        {
            IEnumerable<Type> dummy;

            return Create(prop, out dummy);
        }

        
        internal static PropertyMapping Create(PropertyInfo prop, out IEnumerable<Type> referredTypes)        
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            var foundTypes = new List<Type>();

            PropertyMapping result = new PropertyMapping();
            result.Name = getMappedElementName(prop);
            result.ReturnType = prop.PropertyType;
            result.ElementType = result.ReturnType;

            foundTypes.Add(result.ElementType);

            result.IsCollection = ReflectionHelper.IsTypedCollection(prop.PropertyType) && !prop.PropertyType.IsArray;

            // Get to the actual (native) type representing this element
            if (result.IsCollection) result.ElementType = ReflectionHelper.GetCollectionItemType(prop.PropertyType);
            if (ReflectionHelper.IsNullableType(result.ElementType)) result.ElementType = ReflectionHelper.GetNullableArgument(result.ElementType);

            // Check wether this property represents a native .NET type
            // marked to receive the class' primitive value in the fhir serialization
            // (e.g. the value from the Xml 'value' attribute or the Json primitive member value)
            result.HoldsFhirPrimitiveValue = isPrimitiveValueElement(prop);

            result._choices = ReflectionHelper.GetAttributes<ChoiceAttribute>(prop);

            if (result.HasChoices)
                foundTypes.AddRange(result._choices.Select(cattr => cattr.Type));

            referredTypes = foundTypes;
            return result;
        }


        private static string buildQualifiedPropName(PropertyInfo prop)
        {
            return prop.DeclaringType.Name + "." + prop.Name;
        }


        private static bool isPrimitiveValueElement(PropertyInfo prop)
        {
            var valueElement = (FhirElementAttribute)Attribute.GetCustomAttribute(prop, typeof(FhirElementAttribute));
            var isPrimitive = valueElement != null && valueElement.IsPrimitiveValue;

            if(isPrimitive && !isAllowedNativeTypeForDataTypeValue(prop.PropertyType))
                throw Error.Argument("prop", "Property {0} is marked for use as a primitive element value, but its .NET type is not supported by the serializer.", buildQualifiedPropName(prop));

            return isPrimitive;
        }


         //// Special case: this is a member that uses the closed generic Code<T> type - 
         //       // do mapping for its open, defining type instead
         //       if (elementType.IsGenericType)
         //       {
         //           if (ReflectionHelper.IsClosedGenericType(elementType) &&  
         //               ReflectionHelper.IsConstructedFromGenericTypeDefinition(elementType, typeof(Code<>)) )
         //           {
         //               result.CodeOfTEnumType = elementType.GetGenericArguments()[0];
         //               elementType = elementType.GetGenericTypeDefinition();
         //           }
         //           else
         //               throw Error.NotSupported("Property {0} on type {1} uses an open generic type, which is not yet supported", prop.Name, prop.DeclaringType.Name);
         //       }

        public bool MatchesSuffixedName(string suffixedName)
        {
            if (suffixedName == null) throw Error.ArgumentNull("suffixedName");

            return this.HasChoices && suffixedName.ToUpperInvariant().StartsWith(Name.ToUpperInvariant());
        }

        public string GetChoiceSuffixFromName(string suffixedName)
        {
            if (suffixedName == null) throw Error.ArgumentNull("suffixedName");

            if (MatchesSuffixedName(suffixedName))
                return suffixedName.Remove(0, Name.Length);
            else
                throw Error.Argument("suffixedName", "The given suffixed name {0} does not match this property's name {1}",
                                            suffixedName, Name);
        }

        public bool IsAllowedChoice(string choiceSuffix)
        {
            var suffix = choiceSuffix.ToUpperInvariant();
            return HasChoices && _choices.Any(cattr => cattr.TypeName.ToUpperInvariant() == suffix);
        }

        public Type GetChoiceType(string choiceSuffix)
        {
            string suffix = choiceSuffix.ToUpperInvariant();

            if(!HasChoices) return null;

            return _choices
                        .Where(cattr => cattr.TypeName.ToUpperInvariant() == suffix)
                        .Select(cattr => cattr.Type)
                        .FirstOrDefault(); 
        }

        private static string getMappedElementName(PropertyInfo prop)
        {
            var attr = (FhirElementAttribute)Attribute.GetCustomAttribute(prop, typeof(FhirElementAttribute));

            if (attr != null)
                return attr.Name;
            else
                return prop.Name;
        }

        private static bool isAllowedNativeTypeForDataTypeValue(Type type)
        {
            // Special case, allow Nullable<enum>
            if (ReflectionHelper.IsNullableType(type))
                type = ReflectionHelper.GetNullableArgument(type);

            // We support all primitive .NET types in the serializer
            if (type.IsPrimitive) return true;

            // And some specific complex native types
            if(  type == typeof(byte[]) ||
                 type == typeof(string) ||
                 type == typeof(DateTimeOffset?) ||
                 type == typeof(Uri) )
                return true;

            // And enumerations
            if (type.IsEnum) return true;

            return false;
        }
    }
}
