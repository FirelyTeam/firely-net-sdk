/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support.Poco;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class TypedElementParseExtensions
    {
        # region ParseBindable
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
        public static Element ParseBindable(this ITypedElement instance) => instance.parseBindable();

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
        internal static Element ParseBindable(this IScopedNode instance) => instance.parseBindable();

#pragma warning disable CS0618 // Type or member is obsolete
        private static Element parseBindable<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
        {
            return instance.InstanceType switch
            {
                FhirTypeConstants.CODE => instance.parsePrimitive<Code, T>(),
                FhirTypeConstants.STRING => new Code(instance.parsePrimitive<FhirString, T>()?.Value),
                FhirTypeConstants.URI => new Code(instance.parsePrimitive<FhirUri, T>()?.Value),
                FhirTypeConstants.CODING => instance.parseCoding(),
                FhirTypeConstants.CODEABLE_CONCEPT => instance.parseCodeableConcept(),
                FhirTypeConstants.QUANTITY => parseQuantity(),
                FhirTypeConstants.EXTENSION => parseExtension(),
                _ => null,
            };

            Coding parseQuantity()
            {
                var newCoding = new Coding();
                var q = instance.parseQuantity();
                newCoding.Code = q.Code;
                newCoding.System = q.System ?? "http://unitsofmeasure.org";
                return newCoding;
            }
            Element parseExtension()
            {
                var valueChild = instance.Children("value").FirstOrDefault();
                return valueChild?.parseBindable();
            }
        }
        #endregion

        #region ParseQuantity
        public static Model.Quantity ParseQuantity(this ITypedElement instance) => parseQuantity(instance);

        internal static Model.Quantity ParseQuantity(this IScopedNode instance) => parseQuantity(instance);

#pragma warning disable CS0618 // Type or member is obsolete
        private static Quantity parseQuantity<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
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
            => parsePrimitive<T, ITypedElement>(instance);

        internal static T ParsePrimitive<T>(this IScopedNode instance) where T : PrimitiveType, new()
            => parsePrimitive<T, IScopedNode>(instance);

#pragma warning disable CS0618 // Type or member is obsolete
        private static T parsePrimitive<T, U>(this IBaseElementNavigator<U> instance) where T : PrimitiveType, new() where U : IBaseElementNavigator<U>
#pragma warning restore CS0618 // Type or member is obsolete
                    => new() { ObjectValue = instance.Value };

        #endregion

        #region ParseCoding
        public static Coding ParseCoding(this ITypedElement instance) => parseCoding(instance);

        internal static Coding ParseCoding(this IScopedNode instance) => parseCoding(instance);

#pragma warning disable CS0618 // Type or member is obsolete
        private static Coding parseCoding<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
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
        public static ResourceReference ParseResourceReference(this ITypedElement instance) => instance.parseResourceReference();

        internal static ResourceReference ParseResourceReference(this IScopedNode instance) => instance.parseResourceReference();

#pragma warning disable CS0618 // Type or member is obsolete
        private static ResourceReference parseResourceReference<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
        {
            return new ResourceReference()
            {
                Reference = instance.Children("reference").GetString(),
                Display = instance.Children("display").GetString()
            };
        }
        #endregion

        #region ParseCodeableConcept
        public static CodeableConcept ParseCodeableConcept(this ITypedElement instance) => instance.parseCodeableConcept();
        internal static CodeableConcept ParseCodeableConcept(this IScopedNode instance) => instance.parseCodeableConcept();
#pragma warning disable CS0618 // Type or member is obsolete
        private static CodeableConcept parseCodeableConcept<T>(this IBaseElementNavigator<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
        {
            return new CodeableConcept()
            {
                Coding =
                    instance.Children("coding").Select(codingNav => codingNav.parseCoding()).ToList(),
                Text = instance.Children("text").GetString()
            };
        }
        #endregion

#pragma warning disable CS0618 // Type or member is obsolete
        public static string GetString<T>(this IEnumerable<T> instance) where T : IBaseElementNavigator<T>
#pragma warning restore CS0618 // Type or member is obsolete
            => instance.SingleOrDefault()?.Value as string;
    }
}
