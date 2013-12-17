using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class Endpoint
    {
        // A lot of extensions will be built upon this class.

        public Uri Uri { get; private set; }
        public Endpoint(Uri uri)
        {
            // todo: test if this uri is an Url.
            this.Uri = uri;
        }
        public Endpoint(string uri)
        {
            this.Uri = new Uri(uri);
        } 
        public RestUrl NewRestUrl()
        {
            return new RestUrl(this.Uri);
        } 
        public ResourceIdentity NewResourceIdentity()
        {
            return new ResourceIdentity(this.Uri);
        }
        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}
