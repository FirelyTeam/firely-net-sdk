/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Hl7.Fhir.WebApi
{
    public class InterceptBodyHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                var data = await request.Content.ReadAsByteArrayAsync();

                if (data != null && data.Length > 0 && request.Content.Headers.ContentType != null)
                    request.SaveBody(request.Content.Headers.ContentType.MediaType, data);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}