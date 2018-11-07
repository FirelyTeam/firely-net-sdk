/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Summary
{
    /// <summary>
    /// Extension methods on <see cref="ISourceNode"/> to facilitate harvesting summary information
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
        /// <summary>Harvest the value of the current element into a property bag.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        public static bool HarvestValue(this ISourceNode nav, IDictionary<string, object> properties, string key)
        {
            var value = nav.Text;
            if (value != null)
            {
                var s = PrimitiveTypeConverter.ConvertTo<string>(value);
                properties[key] = s;
                return true;
            }
            return false;
        }

        /// <summary>Harvest the value of the (current or sibling) element with the specified name into a property bag.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        public static bool HarvestValue(this ISourceNode nav, IDictionary<string, object> properties, string key, string element)
        {
            var elementNode = nav.Children(element).FirstOrDefault();
            return elementNode != null && HarvestValue(elementNode, properties, key);
        }

        /// <summary>Harvest the value of a child element into a property bag.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool HarvestValue(this ISourceNode nav, IDictionary<string, object> properties, string key, string element, string childElement)
        {
            var elementNode = nav.Children(element).FirstOrDefault();
            var childNode = elementNode?.Children(childElement).FirstOrDefault();
            return childNode != null && HarvestValue(childNode, properties, key);
        }

        /// <summary>Add the value of the current element to a list, if not missing or empty.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="values">A list of values.</param>
        public static bool HarvestValue(this ISourceNode nav, IList<string> values)
        {
            var value = nav.Text;
            if (value != null)
            {
                var s = PrimitiveTypeConverter.ConvertTo<string>(value);
                values.Add(s);
                return true;
            }
            return false;
        }

        /// <summary>Harvest an array of element values into a property bag.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        public static bool HarvestValues(this ISourceNode nav, IDictionary<string, object> properties, string key, string element)
        {
            var values = new List<string>();
            foreach (var elementNode in nav.Children(element))
            {
                HarvestValue(elementNode, values);
            }
            if (values.Count > 0)
            {
                properties[key] = values.ToArray();
                return true;
            }
            return false;
        }

        /// <summary>Harvest an array of child element values into a property bag.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="key">A property key.</param>
        /// <param name="element">An element name.</param>
        /// <param name="childElement">A child element name.</param>
        public static bool HarvestValues(this ISourceNode nav, IDictionary<string, object> properties, string key, string element, string childElement)
        {
            var values = new List<string>();
            foreach (var elementNode in nav.Children(element))
            {
                foreach(var childNode in elementNode.Children(childElement))
                {
                    HarvestValue(childNode, values);
                }
            }
            if (values.Count > 0)
            {
                properties[key] = values.ToArray();
                return true;
            }
            return false;
        }

        /// <summary>Harvest extension values into a property bag.</summary>
        /// <param name="nav">An <see cref="ISourceNode"/> instance.</param>
        /// <param name="properties">A property bag to store harvested summary information.</param>
        /// <param name="extensionValueHarvester">Callback function called for each individual extension entry.</param>
        public static void HarvestExtensions(this ISourceNode nav, IDictionary<string, object> properties, Action<ISourceNode, IDictionary<string, object>, string> extensionValueHarvester)
        {
            const string extension = "extension";

            foreach(var child in nav.Children(extension))
            {
                var url = child.Children("url").FirstOrDefault();
                if(url != null)
                {
                    extensionValueHarvester(child, properties, url.Text);
                }
            }
        }

    }

}