/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Support
{
    // [WMR 20160721] NEW

    /// <summary>
    /// The exception that is throw when an attempt to resolve an external resource reference fails.
    /// </summary>
    public class ResourceReferenceNotFoundException : Exception
    {
        private readonly string _url;

        // private const string defaultMessage = "Resource reference not found for url '{0}'";
        private const string defaultMessage = "Unresolved resource reference. Cannot find the resource with url '{0}'.";

        public ResourceReferenceNotFoundException(string url) : this(url, defaultMessage.FormatWith(url))
        {
            //
        }

        public ResourceReferenceNotFoundException(string url, string message) : base(message)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            _url = url;
        }

        /// <summary>Returns the url of the unresolved resource reference.</summary>
        public string Url { get { return _url; } }
    }
}
