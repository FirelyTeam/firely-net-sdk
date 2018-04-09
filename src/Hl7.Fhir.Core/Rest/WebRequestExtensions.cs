/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
        internal static void WriteBody(this HttpWebRequest request, bool CompressRequestBody, byte[] data)
        {
#if !DOTNETFW
            Stream outs = null;
            //outs = request.GetRequestStreamAsync().Result;
            //outs.Write(data, 0, (int)data.Length);
            //outs.Flush();
            //outs.Dispose();

            ManualResetEvent requestReady = new ManualResetEvent(initialState: false);
            Exception caught = null;

            AsyncCallback callback = new AsyncCallback(ar =>
            {
                //var request = (WebRequest)ar.AsyncState;
                try
                {
                    outs = request.EndGetRequestStream(ar);
                }
                catch (Exception ex)
                {
                    caught = ex;
                }
                finally
                {
                    requestReady.Set();
                }
            });

            var async = request.BeginGetRequestStream(callback, null);

            if (!async.IsCompleted)
            {
                //async.AsyncWaitHandle.WaitOne();
                // Not having thread affinity seems to work better with ManualResetEvent
                // Using AsyncWaitHandle.WaitOne() gave unpredictable results (in the
                // unit tests), when EndGetResponse would return null without any error
                // thrown
                requestReady.WaitOne();
                //async.AsyncWaitHandle.WaitOne();
            }
            else
            {
                // If the async wasn't finished, then we need to wait anyway
                if (!async.CompletedSynchronously)
                    requestReady.WaitOne();
            }

            if (caught != null) throw caught;

            outs.Write(data, 0, (int)data.Length);
            outs.Flush();
            outs.Dispose();
#else
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
#endif
        }

        internal static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
            {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);

                if (t.IsFaulted)
                {
                    WebException wex = t.Exception.GetBaseException() as WebException;
                    if (wex != null)
                    {
                        var resp = wex.Response as HttpWebResponse;
                        if (resp == null)
                            throw t.Exception.GetBaseException();
                        return resp;
                    }
                    throw t.Exception.GetBaseException();
                }
                else
                {
                    try
                    {
                        if (!t.Wait(timeout))
                            throw new TimeoutException();
                    }
                    catch (AggregateException we)
                    {
                        WebException wex = we.GetBaseException() as WebException;
                        if (wex != null)
                        {
                            var resp = wex.Response as HttpWebResponse;
                            if (resp == null)
                                throw we.GetBaseException();
                            return resp;
                        }
                        throw we.GetBaseException();
                    }
                }
                return t.Result;
            });
        }


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
#if DOTNETFW
                ThreadPool.RegisterWaitForSingleObject(async.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), req, req.Timeout, true);
#endif

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


        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest request = state as HttpWebRequest;
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

    }
}




