/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class PocoSerializationExtensions
    {
        public static string ToJson(this Base source, FhirJsonWriterSettings settings = null) =>
            SerializationUtil.WriteJsonToString(writer => source.ToElementNode().WriteTo(writer, settings));

        public static string ToXml(this Base source, FhirXmlWriterSettings settings = null, string rootName = null)
                => SerializationUtil.WriteXmlToString(writer => source.ToElementNode().WriteTo(writer, settings, rootName));

    }

    /// <summary>
    /// A class to do basic parsing of POCO classes from an IElementNavigator.  Can be replaced by the real
    /// IElementNavigator-based PocoParser when we have that piece of infrastructure ready.
    /// </summary>
    /// <remarks>
    /// Update: we have the infrastructure ready! So, using ToPoco() could replace the other methods in
    /// this class now. But I'll wait until I need to.
    /// </remarks>
    public static class PocoParsingExtensions
    {
        public static Base ToPoco(this IElementNavigator navigator, Type pocoType) => 
            (new FhirJsonParser()).Parse(navigator, pocoType);

        public static T ToPoco<T>(this IElementNavigator navigator) where T : Base =>
               (T)navigator.ToPoco(typeof(T));

        public static Base ToPoco(this ITypedElement navigator, Type pocoType) =>
            (new FhirJsonParser()).Parse(navigator, pocoType);

        public static T ToPoco<T>(this ITypedElement navigator) where T : Base =>
               (T)navigator.ToPoco(typeof(T));

        public static Base ToPoco(this ISourceNode navigator, Type pocoType) => 
            (new FhirJsonParser()).Parse(navigator, pocoType);

        public static T ToPoco<T>(this ISourceNode navigator) where T : Base =>
               (T)navigator.ToPoco(typeof(T));

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
                case FHIRDefinedType.Code:
                    return instance.ParsePrimitive<Code>();
                case FHIRDefinedType.Coding:
                    return instance.ParseCoding();
                case FHIRDefinedType.CodeableConcept:
                    return instance.ParseCodeableConcept();
                case FHIRDefinedType.Quantity:
                    return parseQuantity(instance);
                case FHIRDefinedType.String:
                    return new Code(instance.ParsePrimitive<FhirString>()?.Value);
                case FHIRDefinedType.Uri:
                    return new Code(instance.ParsePrimitive<FhirUri>()?.Value);
                case FHIRDefinedType.Extension:
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
