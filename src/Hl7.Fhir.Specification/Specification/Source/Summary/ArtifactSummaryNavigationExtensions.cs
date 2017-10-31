/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source.Summary
{
    /// <summary>Extension methods on <see cref="IElementNavigator"/> to allow easy extraction of summary details from an artifact.</summary>
    public static class ArtifactSummaryNavigationExtensions
    {
        /// <summary>
        /// Try to position the navigator on the current or sibling element with the specified name.
        /// If the current element name matches, then maintain the current position.
        /// Otherwise, navigate to the next matching sibling element (if it exists).
        /// </summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="element">An element name.</param>
        /// <returns><c>true</c> if, upon return, the navigator is positioned on a matching element, or <c>false</c> otherwise.</returns>
        public static bool Find(this IElementNavigator nav, string element)
        {
            return nav.Name == element || nav.MoveToNext(element);
        }

        /// <summary>Extract the value of the current element into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A dictionary to store extracted properties.</param>
        /// <param name="key">A dictionary key.</param>
        public static bool TryExtractValue(this IElementNavigator nav, IDictionary<string, object> properties, string key)
        {
            var value = nav.Value;
            if (value != null)
            {
                var s = PrimitiveTypeConverter.ConvertTo<string>(value);
                properties[key] = s;
                return true;
            }
            return false;
        }

        /// <summary>Extract the value of the (current or sibling) element with the specified name into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A dictionary to store extracted properties.</param>
        /// <param name="key">A dictionary key.</param>
        /// <param name="element">An element name.</param>
        public static bool TryExtractValue(this IElementNavigator nav, IDictionary<string, object> properties, string key, string element)
        {
            return nav.Find(element) && nav.TryExtractValue(properties, key);
        }

        /// <summary>Extract the value of a child element into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A dictionary to store extracted properties.</param>
        /// <param name="key">A dictionary key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool TryExtractValue(this IElementNavigator nav, IDictionary<string, object> properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var childNav = nav.Clone();
                return childNav.MoveToFirstChild(childElement) && childNav.TryExtractValue(properties, key);
            }
            return false;
        }

        /// <summary>Add the value of the current element to a list, if not missing or empty.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="values">A list of values.</param>
        public static bool TryExtractValue(this IElementNavigator nav, List<string> values)
        {
            var value = nav.Value;
            if (value != null)
            {
                var s = PrimitiveTypeConverter.ConvertTo<string>(value);
                values.Add(s);
                return true;
            }
            return false;
        }

        /// <summary>Extract an array of child element values into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A dictionary to store extracted properties.</param>
        /// <param name="key">A dictionary key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool TryExtractValues(this IElementNavigator nav, IDictionary<string, object> properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var values = new List<string>();
                do
                {
                    var childNav = nav.Clone();
                    if (childNav.MoveToFirstChild(childElement))
                    {
                        TryExtractValue(childNav, values);
                    }
                } while (nav.MoveToNext(element));
                if (values.Count > 0)
                {
                    properties[key] = values.ToArray();
                    return true;
                }
            }
            return false;
        }

    }

}
