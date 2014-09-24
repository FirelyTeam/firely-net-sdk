/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public static class WebRequestExtensions
    {
        public static void WriteBody(this WebRequest request, byte[] data)
        {
            Stream outs = getRequestStream(request);

            outs.Write(data, 0, (int)data.Length);
            outs.Flush();
        }

        private static Stream getRequestStream(WebRequest request)
        {
            Stream requestStream = null;
            ManualResetEvent getRequestFinished = new ManualResetEvent(false);

            AsyncCallback callBack = new AsyncCallback(ar =>
            {
                var req = (HttpWebRequest)ar.AsyncState;
                requestStream = req.EndGetRequestStream(ar);
                getRequestFinished.Set();
            });

            var async = request.BeginGetRequestStream(callBack, request);

            getRequestFinished.WaitOne();

            return requestStream;
        }

#if PORTABLE45 || NET45
        internal static async Task WriteBodyAsync(this HttpWebRequest request, byte[] data)
        {
            Stream outs = await getRequestStreamAsync(request);
            await outs.WriteAsync(data, 0, (int)data.Length);
            outs.Flush();
			outs.Dispose();
        }
	
		private static Task<Stream> getRequestStreamAsync(HttpWebRequest request)
		{
			return request.GetRequestStreamAsync();
		}

        internal static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
            {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);

                if (!t.Wait(timeout)) throw new TimeoutException();

                return t.Result;
            });
        }

		//public static Task<WebResponse> GetResponseAsync(this HttpWebRequest req)
		//{
		//	return req.GetResponseAsync();
		//}
#endif

		public static WebResponse EndGetResponseNoEx(this WebRequest req, IAsyncResult ar)
        {
            try
            {
                return (HttpWebResponse)req.EndGetResponse(ar);
            }
            catch (WebException we)
            {
                var resp = we.Response as HttpWebResponse;
                if (resp == null)
                    throw;
                return resp;
            }
        }

        public static WebResponse GetResponseNoEx(this WebRequest req)
        {
            WebResponse result = null;
            ManualResetEvent responseReady = new ManualResetEvent(initialState: false);
            Exception caught = null;

            AsyncCallback callback = new AsyncCallback(ar =>
                {
                    //var request = (WebRequest)ar.AsyncState;
                    try
                    {
                        result = req.EndGetResponseNoEx(ar);
                    }
                    catch(Exception ex)
                    {
                        caught = ex;
                    }
                    finally
                    {
                        responseReady.Set();
                    }
                });

            var async = req.BeginGetResponse(callback, null);

            if (!async.IsCompleted)
            {
                //async.AsyncWaitHandle.WaitOne();
                // Not having thread affinity seems to work better with ManualResetEvent
                // Using AsyncWaitHandle.WaitOne() gave unpredictable results (in the
                // unit tests), when EndGetResponse would return null without any error
                // thrown
                responseReady.WaitOne();
                //async.AsyncWaitHandle.WaitOne();
            }

            if (caught != null) throw caught;

            return result;
        }
    }
}
