/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public static class WebRequestExtensions
    {
        internal static void WriteBody(this HttpWebRequest request, bool CompressRequestBody, byte[] data)
        {
            Stream outs;
            Stream compressor = null;
            if (CompressRequestBody)
            {
                request.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
                compressor = request.GetRequestStream();
                outs = new System.IO.Compression.GZipStream(compressor, System.IO.Compression.CompressionMode.Compress, true);
            }
            else
            {
                outs = request.GetRequestStream();
            }
            outs.Write(data, 0, (int)data.Length);
            outs.Flush();
            outs.Dispose();
            if (compressor != null)
                compressor.Dispose();
        }

        internal static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            var t = Task.Factory.FromAsync<WebResponse>(
                request.BeginGetResponse,
                request.EndGetResponse,
                null);

            return t.ContinueWith(parent =>
            {
                if (parent.IsFaulted)
                {
                    if (parent.Exception.GetBaseException() is WebException wex)
                    {
                        if (!(wex.Response is HttpWebResponse resp))
                            throw t.Exception.GetBaseException();
                        return resp;
                    }
                    throw t.Exception.GetBaseException();
                }
                return parent.Result;
            });
        }
    }
}




