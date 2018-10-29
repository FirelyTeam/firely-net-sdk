/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class PocoBuilderExtensions
    {
        /// <summary>Execute a <see cref="PocoBuilder"/> operation using the specified settings and exception handler (if any).</summary>
        static T Execute<T>(Func<PocoBuilder, T> operation, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            var builder = new PocoBuilder(settings);
            if (handler != null)
            {
                builder.ExceptionHandler += handler;
            }

            try
            {
                return operation(builder);
            }
            finally
            {
                if (handler != null)
                {
                    builder.ExceptionHandler -= handler;
                }
            }
        }

        /// <summary>Create a PoCo instance from a <see cref="ISourceNode"/>.</summary>
        /// <param name="source">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="pocoType">The type of the result.</param>
        /// <param name="settings">Optional settings that control the behavior of the parser.</param>
        /// <param name="handler">An optional <see cref="ExceptionNotificationHandler"/> delegate to handle runtime exceptions.</param>
        /// <returns>An instance of type <paramref name="pocoType"/>.</returns>
        public static Base ToPoco(this ISourceNode source, Type pocoType = null, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null)
            => Execute(b => b.BuildFrom(source, pocoType), settings, handler);

        /// <summary>Create a PoCo instance from a <see cref="ISourceNode"/>.</summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="source">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="settings">Optional settings that control the behavior of the parser.</param>
        /// <param name="handler">An optional <see cref="ExceptionNotificationHandler"/> delegate to handle runtime exceptions.</param>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        public static T ToPoco<T>(this ISourceNode source, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null) where T : Base
            => (T)source.ToPoco(typeof(T), settings, handler);

        /// <summary>Create a PoCo instance from a <see cref="ITypedElement"/>.</summary>
        /// <param name="element">An <see cref="ITypedElement"/> instance.</param>
        /// <param name="settings">Optional settings that control the behavior of the parser.</param>
        /// <returns>A <see cref="Base"/> instance.</returns>
        public static Base ToPoco(this ITypedElement element, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null)
            => Execute(b => b.BuildFrom(element), settings, handler);

        /// <summary>Create a PoCo instance from a <see cref="ITypedElement"/>.</summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="element">An <see cref="ITypedElement"/> instance.</param>
        /// <param name="settings">Optional settings that control the behavior of the parser.</param>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        public static T ToPoco<T>(this ITypedElement element, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null) where T : Base =>
               (T)element.ToPoco(settings, handler);

#pragma warning disable 612, 618

        /// <summary>Create a PoCo instance from a <see cref="IElementNavigator"/>.</summary>
        /// <param name="navigator">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="pocoType">The type of the result.</param>
        /// <param name="settings">Optional settings that control the behavior of the parser.</param>
        /// <returns>An instance of type <paramref name="pocoType"/>.</returns>
        public static Base ToPoco(this IElementNavigator navigator, Type pocoType = null, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null) =>
            Execute(b => b.BuildFrom(navigator.ToSourceNode(), pocoType), settings, handler);

        /// <summary>Create a PoCo instance from a <see cref="IElementNavigator"/>.</summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="navigator">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="settings">Optional settings that control the behavior of the parser.</param>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        public static T ToPoco<T>(this IElementNavigator navigator, PocoBuilderSettings settings = null, ExceptionNotificationHandler handler = null) where T : Base =>
               (T)navigator.ToPoco(typeof(T), settings, handler);

#pragma warning restore 612, 618


        [Obsolete("Use ParseQuantity(this ITypedElement instance) instead")]
        public static Quantity ParseQuantity(this IElementNavigator instance)
        {
            return ParseQuantity(instance.ToTypedElement());
        }

        /// <summary>Parse the specified <see cref="ITypedElement"/> element into a <see cref="Quantity"/>.</summary>
        public static Quantity ParseQuantity(this ITypedElement instance)
        {
            var newQuantity = new Quantity
            {
                Value = instance.Children("value").SingleOrDefault()?.Value as decimal?,
                Unit = instance.Children("unit").GetString(),
                System = instance.Children("system").GetString(),
                Code = instance.Children("code").GetString()
            };

            var comp = instance.Children("comparator").GetString();
            if(comp != null)
                newQuantity.ComparatorElement = new Code<Quantity.QuantityComparator> { ObjectValue = comp };

            return newQuantity;
        }

        /// <summary>This method is obsolete. Use <see cref="ParseBindable(ITypedElement)"/> instead.</summary>
        [Obsolete("Use ParseBindable(this ITypedElement instance) instead")]
        public static Element ParseBindable(this IElementNavigator instance)
        {
            return ParseBindable(instance.ToTypedElement());
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
        public static Element ParseBindable(this ITypedElement instance)
        {
            var instanceType = ModelInfo.FhirTypeNameToFhirType(instance.InstanceType);

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

            Coding parseQuantity(ITypedElement nav)
            {
                var newCoding = new Coding();
                var q = instance.ParseQuantity();
                newCoding.Code = q.Unit;
                newCoding.System = q.System ?? "http://unitsofmeasure.org";
                return newCoding;
            }

            Element parseExtension(ITypedElement nav)
            {
                // HACK: For now, assume this is a typed navigator, so we have "value",
                // not the unparsed "valueCode" etc AND we have Type (in ParseBindable())
                var valueChild = instance.Children("value").FirstOrDefault();
                return valueChild?.ParseBindable();
            }
        }

        /// <summary>This method is obsolete. Use <see cref="ParsePrimitive{T}(ITypedElement)"/> instead.</summary>
        [Obsolete("Use ParsePrimitive<T>(this ITypedElement instance) instead")]
        public static T ParsePrimitive<T>(this IElementNavigator instance) where T : Primitive, new()
            => ParsePrimitive<T>(instance.ToTypedElement());

        /// <summary>Parse the specified <see cref="ITypedElement"/> element into a <see cref="Primitive"/> value.</summary>
        public static T ParsePrimitive<T>(this ITypedElement instance) where T:Primitive, new()
                    => new T() { ObjectValue = instance.Value };

        /// <summary>This method is obsolete. Use <see cref="ParseCoding(ITypedElement)"/> instead.</summary>
        [Obsolete("Use ParseCoding(this ITypedElement instance) instead")]
        public static Coding ParseCoding(this IElementNavigator instance)
            => ParseCoding(instance.ToTypedElement());

        /// <summary>Parse the specified <see cref="ITypedElement"/> element into a <see cref="Coding"/> value.</summary>
        public static Coding ParseCoding(this ITypedElement instance)
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

        /// <summary>This method is obsolete. Use <see cref="ParseResourceReference(ITypedElement)"/> instead.</summary>
        [Obsolete("Use ParseResourceReference(this ITypedElement instance) instead")]
        public static ResourceReference ParseResourceReference(this IElementNavigator instance)
             => ParseResourceReference(instance.ToTypedElement());

        /// <summary>Parse the specified <see cref="ITypedElement"/> element into a <see cref="ResourceReference"/> value.</summary>
        public static ResourceReference ParseResourceReference(this ITypedElement instance)
        {
            return new ResourceReference()
            {
                Reference = instance.Children("reference").GetString(),
                Display = instance.Children("display").GetString()
            };
        }

        /// <summary>This method is obsolete. Use <see cref="ParseCoding(ITypedElement)"/> instead.</summary>
        [Obsolete("Use ParseCodeableConcept(this ITypedElement instance) instead")]
        public static CodeableConcept ParseCodeableConcept(this IElementNavigator instance)
            => ParseCodeableConcept(instance.ToTypedElement());

        /// <summary>Parse the specified <see cref="ITypedElement"/> element into a <see cref="CodeableConcept"/> value.</summary>
        public static CodeableConcept ParseCodeableConcept(this ITypedElement instance)
        {
            return new CodeableConcept()
            {
                Coding =
                    instance.Children("coding").Select(codingNav => codingNav.ParseCoding()).ToList(),
                Text = instance.Children("text").GetString()
            };
        }

        /// <summary>This method is obsolete. Use <see cref="GetString(IEnumerable{ITypedElement})"/> instead.</summary>
        [Obsolete("Use GetString(this IEnumerable<ITypedElement> instance) instead")]
        public static string GetString(this IEnumerable<IElementNavigator> instance)
            => instance.SingleOrDefault()?.Value as string;

        /// <summary>Parse (the first entry of) the specified <see cref="ITypedElement"/> sequence into a string value.</summary>
        public static string GetString(this IEnumerable<ITypedElement> instance)
            => instance.SingleOrDefault()?.Value as string;

    }
}
