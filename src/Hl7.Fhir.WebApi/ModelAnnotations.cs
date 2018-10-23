/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Hl7.Fhir.WebApi
{
    /// <summary>
    /// The 
    /// </summary>
    public class RequestUri : Uri
    {
        public static implicit operator RequestUri(string uriString)
        {
            return new RequestUri(uriString);
        }
        public RequestUri(string uriString) : base(uriString)
        {

        }

        public override string ToString()
        {
            return base.OriginalString;
        }
    }

    /// <summary>
    /// Used on a resource to indicate if the resource was actually created or updated
    /// in the underlying storage (will result in the 200 or 201 status result).
    /// 200 should only come from a PUT request, however the 201 could occur from either PUT or POST
    /// </summary>
    public enum CreateOrUpate { Create, Update };
}
