#nullable enable

/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support.Poco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class TypedElementParseExtensions
    {
        #region ParseBindable
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
        ///   'CodeableReference' => CodeableConcept if 'concept' is present, otherwise null
        /// </remarks>
        public static Element? ParseBindable(this ITypedElement instance)
#pragma warning disable CS0618 // Type or member is obsolete
            => instance.ParseBindableInternal();
#pragma warning restore CS0618 // Type or member is obsolete

        /// <inheritdoc cref="ParseBindable"/>
        [Obsolete("WARNING! Intended for internal API usage exclusively, interface IBaseElementNavigator can be changed in " +
            "the near future.")]
        public static Element? ParseBindableInternal<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
        {
            return instance.InstanceType switch
            {
                FhirTypeConstants.CODE => instance.ParsePrimitiveInternal<Code, T>(),
                FhirTypeConstants.STRING => new Code(instance.ParsePrimitiveInternal<FhirString, T>().Value),
                FhirTypeConstants.URI => new Code(instance.ParsePrimitiveInternal<FhirUri, T>().Value),
                FhirTypeConstants.CODING => instance.ParseCodingInternal(),
                FhirTypeConstants.CODEABLE_CONCEPT => instance.ParseCodeableConceptInternal(),
                FhirTypeConstants.QUANTITY => parseQuantity(),
                FhirTypeConstants.EXTENSION => parseExtension(),
                FhirTypeConstants.CODEABLEREFERENCE => parseCodeableReference(), 
                _ => null,
            };

            CodeableConcept? parseCodeableReference()
            {
                var valueChild = instance.Children("concept").FirstOrDefault();
                return valueChild?.ParseCodeableConceptInternal();
            }

            Coding parseQuantity()
            {
                var newCoding = new Coding();
                var q = instance.ParseQuantityInternal();
                newCoding.Code = q.Code;
                newCoding.System = q.System ?? "http://unitsofmeasure.org";
                return newCoding;
            }
            
            Element? parseExtension()
            {
                var valueChild = instance.Children("value").FirstOrDefault();
                return valueChild?.ParseBindableInternal();
            }
        }
        #endregion

        #region ParseQuantity
        public static Quantity ParseQuantity(this ITypedElement instance)
#pragma warning disable CS0618 // Type or member is obsolete
            => ParseQuantityInternal(instance);
#pragma warning restore CS0618 // Type or member is obsolete

        [Obsolete("WARNING! Intended for internal API usage exclusively, interface IBaseElementNavigator can be changed in " +
            "the near future.")]
        public static Quantity ParseQuantityInternal<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
        {
            var newQuantity = new Quantity
            {
                Value = instance.Children("value").SingleOrDefault()?.Value as decimal?,
                Unit = instance.Children("unit").GetString(),
                System = instance.Children("system").GetString(),
                Code = instance.Children("code").GetString()
            };

            var comp = instance.Children("comparator").GetString();
            if (comp != null)
                newQuantity.ComparatorElement = new Code<Quantity.QuantityComparator> { ObjectValue = comp };

            return newQuantity;
        }
        #endregion

        #region ParsePrimitive
        public static T ParsePrimitive<T>(this ITypedElement instance) where T : PrimitiveType, new()
#pragma warning disable CS0618 // Type or member is obsolete
            => ParsePrimitiveInternal<T, ITypedElement>(instance);
#pragma warning restore CS0618 // Type or member is obsolete

        [Obsolete("WARNING! Intended for internal API usage exclusively, interface IBaseElementNavigator can be changed in " +
            "the near future.")]
        public static T ParsePrimitiveInternal<T, U>(this IBaseElementNavigator<U> instance) where T : PrimitiveType, new() where U : IBaseElementNavigator<U>
                    => new() { ObjectValue = instance.Value };

        #endregion

        #region ParseCoding
        public static Coding ParseCoding(this ITypedElement instance)
#pragma warning disable CS0618 // Type or member is obsolete
            => ParseCodingInternal(instance);
#pragma warning restore CS0618 // Type or member is obsolete


        [Obsolete("WARNING! Intended for internal API usage exclusively, interface IBaseElementNavigator can be changed in " +
            "the near future.")]
        public static Coding ParseCodingInternal<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
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
        #endregion

        #region ParseResourceReference
        public static ResourceReference ParseResourceReference(this ITypedElement instance)
#pragma warning disable CS0618 // Type or member is obsolete
            => instance.ParseResourceReferenceInternal();
#pragma warning restore CS0618 // Type or member is obsolete

        [Obsolete("WARNING! Intended for internal API usage exclusively, interface IBaseElementNavigator can be changed in " +
            "the near future.")]
        public static ResourceReference ParseResourceReferenceInternal<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
        {
            return new ResourceReference()
            {
                Reference = instance.Children("reference").GetString(),
                Display = instance.Children("display").GetString()
            };
        }
        #endregion

        #region ParseCodeableConcept
        public static CodeableConcept ParseCodeableConcept(this ITypedElement instance)
#pragma warning disable CS0618 // Type or member is obsolete
            => instance.ParseCodeableConceptInternal();
#pragma warning restore CS0618 // Type or member is obsolete

        [Obsolete("WARNING! Intended for internal API usage exclusively, interface IBaseElementNavigator can be changed in " +
            "the near future.")]
        public static CodeableConcept ParseCodeableConceptInternal<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
        {
            return new CodeableConcept()
            {
                Coding =
                    instance.Children("coding").Select(codingNav => codingNav.ParseCodingInternal()).ToList(),
                Text = instance.Children("text").GetString()
            };
        }
        #endregion

#pragma warning disable CS0618 // Type or member is obsolete
        public static string? GetString<T>(this IEnumerable<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
            => instance.SingleOrDefault()?.Value as string;
    }
}

#nullable restore