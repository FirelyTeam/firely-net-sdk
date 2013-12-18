using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    internal class Endpoint
    {
        // A lot of extensions will be built upon this class.

        public Uri Uri { get; private set; }
        public Endpoint(Uri uri)
        {
            if (uri.Scheme != "http")
                Error.Argument("uri", "Endpoint must be based on an url (start with http://)");

            this.Uri = uri;
        }

        public Endpoint(string uri)
        {
            this.Uri = new Uri(uri);
        }

        public RestUrl AsRestUrl()
        {
            return new RestUrl(this.Uri);
        }


        public bool IsWithinEndpoint(Uri other)
        {
            return this.Uri.IsBaseOf(other);
        }

        public ResourceIdentity ToResourceIdentity()
        {
            return new ResourceIdentity(this.Uri);
        }

        public ResourceIdentity ToResourceIdentity(string collection, string id)
        {
            return ResourceIdentity.Build(this.Uri, collection, id);
        }

        public ResourceIdentity ToResourceIdentity(string collection, string id, string vid)
        {
            return ResourceIdentity.Build(this.Uri, collection, id, vid);
        }

        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}
