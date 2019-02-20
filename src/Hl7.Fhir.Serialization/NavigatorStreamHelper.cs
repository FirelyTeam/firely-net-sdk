/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Serialization
{
    /// <summary>Common utility methods for <see cref="INavigatorStream"/> implementations.</summary>
    public static class NavigatorStreamHelper
    {
        /// <summary>Default base url for generating canonical urls for Bundle entries.</summary>
        public static readonly string DefaultCanonicalBaseUrlForBundleEntry = "http://example.org/";

        /// <summary>Generate a canonical url for a Bundle entry.</summary>
        /// <param name="resourceType">A resource type.</param>
        /// <param name="resourceId">A unique resource Id.</param>
        /// <returns>A fully qualified canonical url string.</returns>
        /// <remarks>The generated url starts with the <see cref="DefaultCanonicalBaseUrlForBundleEntry"/>.</remarks>
        public static string FormatCanonicalUrlForBundleEntry(string resourceType, string resourceId)
            => FormatCanonicalUrl(DefaultCanonicalBaseUrlForBundleEntry, resourceType, resourceId);

        /// <summary>Generate a canonical url from the specified base Url, resource type and id.</summary>
        /// <param name="baseUrl">A base url.</param>
        /// <param name="resourceType">A resource type.</param>
        /// <param name="resourceId">A unique resource Id.</param>
        /// <returns>A fully qualified canonical url string.</returns>
        public static string FormatCanonicalUrl(string baseUrl, string resourceType, string resourceId)
            => baseUrl + resourceType + "/" + resourceId;

    }
}
