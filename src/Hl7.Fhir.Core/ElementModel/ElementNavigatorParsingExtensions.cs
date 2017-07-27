using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A class to do basic parsing of POCO classes from an IElementNavigator.  Can be replaced by the real
    /// IElementNavigator-based PocoParser when we have that piece of infrastructure ready.
    /// </summary>
    public static class ElementNavigatorParsingExtensions
    {
        public static Model.Quantity ParseQuantity(this IElementNavigator instance)
        {
            var newQuantity = new Quantity();

            newQuantity.Value = instance.Children("value").SingleOrDefault()?.Value as decimal?;

            var comp = instance.Children("comparator").GetString();
            if(comp != null)
                newQuantity.ComparatorElement = new Code<Quantity.QuantityComparator> { ObjectValue = comp };

            newQuantity.Unit = instance.Children("unit").GetString();
            newQuantity.System = instance.Children("system").GetString();
            newQuantity.Code = instance.Children("code").GetString();

            return newQuantity;
        }

        /// <summary>
        /// Parses a bindeable type (code, Coding, CodeableConcept, Quantity, string, uri) into a FHIR coded datatype.
        /// Extensions will be parsed from the 'value' of the (simple) extension.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>An Element of a coded type (code, Coding or CodeableConcept) dependin on the instance type,
        /// or null if no bindable instance data was found</returns>
        /// <remarks>The instance type is mapped to a codable type as follows:
        ///   'code' => code
        ///   'Coding' => Coding
        ///   'CodeableConcept' => CodeableConcept
        ///   'Quantity' => Coding
        ///   'Extension' => depends on value[x]
        ///   'string' => code
        ///   'uri' => code
        /// </remarks>
        public static Element ParseBindable(this IElementNavigator instance)
        {
            var instanceType = ModelInfo.FhirTypeNameToFhirType(instance.Type);

            switch (instanceType)
            {
                case FHIRAllTypes.Code:
                    return instance.ParsePrimitive<Code>();
                case FHIRAllTypes.Coding:
                    return instance.ParseCoding();
                case FHIRAllTypes.CodeableConcept:
                    return instance.ParseCodeableConcept();
                case FHIRAllTypes.Quantity:
                    return parseQuantity(instance);
                case FHIRAllTypes.String:
                    return new Code(instance.ParsePrimitive<FhirString>()?.Value);
                case FHIRAllTypes.Uri:
                    return new Code(instance.ParsePrimitive<FhirUri>()?.Value);
                case FHIRAllTypes.Extension:
                    return parseExtension(instance);
                case null:
                    //HACK: fall through - IElementNav did not provide a type
                    //should not happen, and I have no intention to handle it.
                default:
                    // Not bindable
                    return null;
            }

            Coding parseQuantity(IElementNavigator nav)
            {
                var newCoding = new Coding();
                var q = instance.ParseQuantity();
                newCoding.Code = q.Unit;
                newCoding.System = q.System ?? "http://unitsofmeasure.org";
                return newCoding;
            }

            Element parseExtension(IElementNavigator nav)
            {
                // HACK: For now, assume this is a typed navigator, so we have "value",
                // not the unparsed "valueCode" etc AND we have Type (in ParseBindable())
                var valueChild = instance.Children("value").FirstOrDefault();

                if (valueChild != null)
                    return valueChild.ParseBindable();
                else
                    return null;
            }
        }

        public static T ParsePrimitive<T>(this IElementNavigator instance) where T:Primitive, new()
                    => new T() { ObjectValue = instance.Value };

        public static Coding ParseCoding(this IElementNavigator instance)
        {
            return new Coding()
            {
                System = instance.Children("system").GetString(),
                Version = instance.Children("version").GetString(),
                Code = instance.Children("code").GetString(),
                Display = instance.Children("display").GetString(),
                UserSelected = instance.Children("userSelected").SingleOrDefault()?.Value as bool?
            };
        }

        public static ResourceReference ParseResourceReference(this IElementNavigator instance)
        {
            return new ResourceReference()
            {
                Reference = instance.Children("reference").GetString(),
                Display = instance.Children("display").GetString()           
            };
        }



        public static CodeableConcept ParseCodeableConcept(this IElementNavigator instance)
        {
            return new CodeableConcept()
            {
                Coding =
                    instance.Children("coding").Select(codingNav => codingNav.ParseCoding()).ToList(),
                Text = instance.Children("text").GetString()
            };
        }

        public static string GetString(this IEnumerable<IElementNavigator> instance)
        {
            return instance.SingleOrDefault()?.Value as string;
        }
    }
}
