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

        private const string defaultMessage = "Resource reference not found for url '{0}'";

        public ResourceReferenceNotFoundException(string url) : this(url, defaultMessage.FormatWith(url))
        {
            //
        }

        public ResourceReferenceNotFoundException(string url, string message) : base(message)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            _url = url;
        }

        public string Url { get { return _url; } }
    }
}
