/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Summary
{
    /// <summary>
    /// Extension methods on <see cref="IElementNavigator"/> to facilitate harvesting summary information
    /// from a FHIR artifact in a forward direction and storing the harvested values in a property bag.
    /// </summary>
    /// <remarks>
    /// The extension methods with element name parameters try to harvest a value from the a matching element
    /// (or from an also matching child element).
    /// If the current element does not match, the methods advance the navigator to the first matching
    /// sibling element in forward direction, if it exists. The boolean return value indicates if a
    /// matching element was found.
    /// After returning, the navigator is positioned on the original element or on a following sibling.
    /// </remarks>
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

        /// <summary>Harvest the value of the current element into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        public static bool HarvestValue(this IElementNavigator nav, IDictionary<string, object> properties, string key)
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

        /// <summary>Harvest the value of the (current or sibling) element with the specified name into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        public static bool HarvestValue(this IElementNavigator nav, IDictionary<string, object> properties, string key, string element)
        {
            return nav.Find(element) && nav.HarvestValue(properties, key);
        }

        /// <summary>Harvest the value of a child element into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool HarvestValue(this IElementNavigator nav, IDictionary<string, object> properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var childNav = nav.Clone();
                return childNav.MoveToFirstChild(childElement) && childNav.HarvestValue(properties, key);
            }
            return false;
        }

        /// <summary>Add the value of the current element to a list, if not missing or empty.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="values">A list of values.</param>
        public static bool HarvestValue(this IElementNavigator nav, IList<string> values)
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

        /// <summary>Harvest an array of child element values into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool HarvestValues(this IElementNavigator nav, IDictionary<string, object> properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var values = new List<string>();
                do
                {
                    var childNav = nav.Clone();
                    if (childNav.MoveToFirstChild(childElement))
                    {
                        HarvestValue(childNav, values);
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

        /// <summary>Harvest extension values into a property bag.</summary>
        /// <param name="nav">An <see cref="IElementNavigator"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="extensionValueHarvester">Callback function called for each individual extension entry.</param>
        public static void HarvestExtensions(this IElementNavigator nav, IDictionary<string, object> properties, Action<IElementNavigator, IDictionary<string, object>, string> extensionValueHarvester)
        {
            const string extension = "extension";

            if (nav.Find(extension))
            {
                do
                {
                    var childNav = nav.Clone();
                    if (childNav.MoveToFirstChild("url"))
                        
                    {
                        if (childNav.Value is string url)
                        {
                            extensionValueHarvester(childNav, properties, url);
                        }
                    }
                // [WMR 20171219] BUG: MoveToNext advances to extension.url (child attribute) instead of the next extension element
                } while (nav.MoveToNext(extension));
            }
        }

    }

}

#endif
